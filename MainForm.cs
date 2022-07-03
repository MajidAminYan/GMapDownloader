using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Net;
using System.Drawing.Imaging;

namespace GoogleMapDownLoader
{
    public partial class MainForm : Form
    {
        readonly ArrayList _waittodownload = new ArrayList();  //مجموعه ای از تصاویر برای دانلود
        string _path=""; //ذخیره مسیر
        int _thread = 0;  //تعداد رشته های دانلود
        int _downloadnum = 0;  //تعداد تصاویر دانلود شده
        int _zoom=0;  //میزان بزرگنمایی

        int _totalwidth = 0;  //عرض نقشه پس از ادغام
        int _totalheight = 0;  //ارتفاع نقشه ادغام شده

        DateTime _startTime = DateTime.Now;  //شروع زمان دانلود
        public MainForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// انتخاب محل ذخیره تصاویر
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBrowseSaveLocation_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fb = new FolderBrowserDialog())
            {
                if (fb.ShowDialog() == DialogResult.OK)
                    txtPath.Text = fb.SelectedPath;
            }
        }
        /// <summary>
        /// دانلود را شروع کن
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnStart_Click(object sender, EventArgs e)
        {
            double flng = 0;  //طول جغرافیایی سمت چپ بالا
            double flat = 0;  //عرض جغرافیایی سمت چپ بالا
            double slng = 0;  //طول جغرافیایی سمت راست پایین
            double slat = 0;  //عرض جغرافیایی پایین سمت راست
            int zomm = 0;  //میزان بزرگنمایی
            int thread = 4;  //تعداد رشته های دانلود
            try
            {
                _path = txtPath.Text;
                if (_path == "")
                    throw new Exception();
                zomm = (int)numZoom.Value;
                thread = (int)numThread.Value;
                flng = double.Parse(txtfirstlng.Text);
                flat = double.Parse(txtfirstlat.Text);
                slng = double.Parse(txtsecondlng.Text);
                slat = double.Parse(txtsecondlat.Text);
            }
            catch
            {
                MessageBox.Show("تنظیم پارامتر غیر طبیعی است!");
            }
            Point p = LatLongToPixel(flat, flng, zomm); //طول و عرض جغرافیایی نقطه اول را به مختصات دو بعدی مسطح تبدیل کنید
            Point p2 = LatLongToPixel(slat, slng, zomm);  //طول و عرض جغرافیایی نقطه دوم را به مختصات مسطح دو بعدی تبدیل کنید
            int startX = p.X / 256;  //ستون شروع
            int endX = p2.X / 256;   //ستون انتهایی
            if (endX == Math.Pow(2, zomm))  //ستون انتهایی خارج از محدوده
                endX--;
            int startY = p.Y / 256;  //خط شروع
            int endY = p2.Y / 256;   //خط پایان
            if (endY == Math.Pow(2, zomm))  //خط انتهایی خارج از محدوده
                endY--;
            //ناحیه فوق که توسط startX endX startY endY احاطه شده است ناحیه ای است که باید دانلود شود.این ناحیه از تعداد زیادی مربع 256*256 تشکیل شده است.

            _totalwidth = (endX - startX + 1) * 256;  //عرض نمودار ادغام شده
            _totalheight = (endY - startY + 1) * 256;  //ارتفاع نمودار ادغام شده

            int serverId = 0;
            int threadId = 0;
            _waittodownload.Clear();
            for (int y = startY; y <= endY; y++)
            {
                for (int x = startX; x <= endX; x++)
                {
                    RectInfo ri = new RectInfo
                    {
                        serverId = serverId,  //دانلود از سرورهای مختلف
                        threadId = threadId,  //از رشته های مختلف دانلود شده
                        //url = BuildURL(serverId),
                        url = txtUrl.Text,
                        x = x,
                        y = y,
                        z = zomm,
                        bComplete = false
                    };
                    _waittodownload.Add(ri);  //هر مربع کوچک را در مجموعه ای که باید دانلود کنید قرار دهید
                    serverId = (serverId + 1) % 4;   //دانلود تصاویر از 4 سرور مختلف
                    threadId = (threadId + 1) % thread;  //دانلود تصاویر از رشته های مختلف
                }
            }

            if (MessageBox.Show("به اشتراک گذاشته شده است" + _waittodownload.Count + "یک عکس باید دانلود شود، آیا مطمئن هستید که آن را دانلود می کنید؟", "اشاره", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
            {
                _thread = thread;
                _zoom = zomm;
                _downloadnum = 0;
                groupBox1.Enabled = false;
                BtnStart.Enabled = false;
                BtnPreview.Enabled = false;
                rchOuput.Clear();
                groupBox2.Text = "خروجی(" + _waittodownload.Count + "مورد)";
                _startTime = DateTime.Now;
                for (int i = 1; i <= thread; ++i)
                {
                    Thread t = new Thread(new ParameterizedThreadStart(DownloadThreadProc));
                    t.Start(i);  //اجرای رشته را شروع کنید
                }
            }
        }
        /// <summary>
        /// پیش نمایش کلیه نقشه ها
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPreview_Clicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_downloadnum == _waittodownload.Count && _downloadnum != 0)  //همه دانلود شدند
            {
                Bitmap b = new Bitmap(_totalwidth, _totalheight);
                Graphics g = Graphics.FromImage(b);
                int startx = ((RectInfo)(_waittodownload[0])).x;
                int starty = ((RectInfo)(_waittodownload[0])).y;
                foreach (RectInfo rf in _waittodownload)
                    if (rf.Bitmap != null)
                    g.DrawImage(rf.Bitmap, new Point((rf.x - startx) * 256, (rf.y - starty) * 256));
                g.Dispose();
                b.Save(_path + "\\" + _zoom + "_total.jpg");
                b.Dispose();
                System.Diagnostics.Process.Start(_path + "\\" + _zoom + "_total.jpg");
            }
        }
        #region help methods

        /// <summary>
        /// دانلود نقشه با توجه براساس آدرس اینترنتی
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private Image DownloadImage(string url)
        {
            Image result = null;
            Stream stream = DownloadResource(url);
            if (stream != null)
                result = Image.FromStream(stream);
            return result;
        }
        /// <summary>
        /// از webclient برای دانلود منابع وب با توجه به url استفاده کنید
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public Stream DownloadResource(string url)
        {
            MemoryStream stream = null;
            try
            {
                WebClient client = new WebClient();
                client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                byte[] data = client.DownloadData(url);
                stream = new MemoryStream(data);
                client.Dispose();
            }
            catch (WebException)
            {
            }
            return stream;
        }
        /// <summary>
        /// اجرای دانلود توسط رشته
        /// </summary>
        /// <param name="param"></param>
        public void DownloadThreadProc(object param)
        {
            int threadId = (int)param;  //شماره رشته فعلی
            Invoke((Action)delegate()  //خروجی
            {
                rchOuput.SelectionColor = Color.Blue;
                rchOuput.AppendText(DateTime.Now.ToLongTimeString() + " شماره 1" + threadId + "Thread شروع به اجرا می کند\r\n");
            });
            for (int i = 0; i < _waittodownload.Count; i++)
            {
                RectInfo ri = (RectInfo)_waittodownload[i];
                if ((!ri.bComplete) &&(ri.threadId + 1 == threadId))
                {
                    try
                    {
                        string url = ri.url.Replace("${x}", ri.x.ToString()).Replace("${y}", ri.y.ToString()).Replace("${z}", ri.z.ToString()).Replace("{x}", ri.x.ToString()).Replace("{y}", ri.y.ToString()).Replace("{z}", ri.z.ToString());
                        Image map = DownloadImage(url);
                        if (map != null)
                        {
                            var imgExt = ".jpg";
                            var imgFormat = ImageFormat.Jpeg;
                            if (ImageFormat.Png.Equals(map.RawFormat))
                            {
                                imgExt = ".png";
                                imgFormat = ImageFormat.Png;
                            }
                            else if (ImageFormat.Gif.Equals(map.RawFormat))
                            {
                                imgExt = ".gif";
                                imgFormat = ImageFormat.Gif;
                            }
                            Directory.CreateDirectory(_path + "\\" + ri.z.ToString());
                            string file = _path + "\\" + ri.z.ToString() + "\\" + ri.x.ToString() + "_" + ri.y.ToString() + imgExt;
                            ri.Bitmap = (Bitmap)map;
                            //فرمت ذخیره فایل "zoom_column_row.jpg"
                            map.Save(file, imgFormat);
                            Invoke((Action)delegate ()  //خروجی
                            {
                                rchOuput.SelectionColor = Color.Green;
                                rchOuput.AppendText(DateTime.Now.ToLongTimeString() + " شماره 1" + threadId + "عکس دانلود شماره رشته" + ri.z.ToString() + "_" + ri.x.ToString() + "_" + ri.y.ToString() + ".jpg\r\n");
                            });
                        }
                        _downloadnum++;
                        ri.bComplete = true;
                    }
                    catch
                    {
                        Invoke((Action)delegate()  //خروجی
                        {
                            rchOuput.SelectionColor = Color.Red;
                            rchOuput.AppendText(DateTime.Now.ToLongTimeString() + " شماره 1" + threadId + "عکس دانلود شماره رشته" + ri.z.ToString() + "_" + ri.x.ToString() + "_" + ri.y.ToString() + ".jpgمردود شدن！\r\n");
                        });
                    }
                }
            }
            Invoke((Action)delegate()  //خروجی
            {
                rchOuput.SelectionColor = Color.Blue;
                rchOuput.AppendText(DateTime.Now.ToLongTimeString() + " شماره 1" + threadId + "اجرای Thread تکمیل شد\r\n");
            });
            _thread--; //تعداد نخ های کارگر را یک بار کاهش دهید
            if (_thread == 0) //تمام رشته ها به پایان می رسد
            {
                Invoke((Action)delegate()  //خروجی
                {
                    rchOuput.SelectionColor = Color.Blue;
                    rchOuput.AppendText(DateTime.Now.ToLongTimeString() + " دانلود تصویر تمام شد! مجموع دانلودها" + _downloadnum + "، کل زمان" + (DateTime.Now-_startTime).TotalSeconds + "دومین");
                    groupBox1.Enabled = true;
                    BtnStart.Enabled = true;
                    BtnPreview.Enabled = true;
                });
            }
        }

        /// <summary>
        /// یک نقطه در سیستم مختصات طول و عرض جغرافیایی را به یک نقطه در یک نقشه دوبعدی مسطح (مبدا در گوشه سمت چپ بالای صفحه است) با توجه به میزان بزرگنمایی تبدیل کنید.
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="zoom"></param>
        /// <returns></returns>
        public Point LatLongToPixel(double latitude, double longitude, double zoom)
        {
            _ = new Point();

            double centerPoint = Math.Pow(2, zoom + 7);
            double totalPixels = 2 * centerPoint;
            double pixelsPerLngDegree = totalPixels / 360;
            double pixelsPerLngRadian = totalPixels / (2 * Math.PI);
            double siny = Math.Min(Math.Max(Math.Sin(latitude * (Math.PI / 180)), -0.9999), 0.9999);
            Point point = new Point((int)Math.Round(centerPoint + longitude * pixelsPerLngDegree), (int)Math.Round(centerPoint - 0.5 * Math.Log((1 + siny) / (1 - siny)) * pixelsPerLngRadian));
            return point;
        }
        #endregion
    }

    /// <summary>
    /// هر بلوک با اندازه 256*256 در نقشه
    /// </summary>
    public class RectInfo
    {
        public int serverId;  //سرور
        public int threadId;  //شماره رشته
        public string url;  //آدرس دانلود
        public int x;  //طول جغرافیایی
        public int y;  //عرض جغرافیایی
        public int z;  //میزان بزرگنمایی
        public bool bComplete;  //آیا انجام شده است
        public Bitmap Bitmap; //تصویر
    }
}
