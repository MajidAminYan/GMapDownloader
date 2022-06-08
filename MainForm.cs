using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Net;

namespace GoogleMapDownLoader
{
    public partial class MainForm : Form
    {
        readonly ArrayList _waittodownload = new ArrayList();  //مجموعه ای از تصاویر برای دانلود
        string _path=""; //ذخیره مسیر
        int _thread = 0;  //تعداد تاپیک های دانلود
        int _downloadnum = 0;  //تعداد تصاویر دانلود شده
        int _zoom=0;  //سطح زوم

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
            int zomm = 0;  //سطح زوم
            int thread = 4;  //تعداد تاپیک های دانلود
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
                        threadId = threadId,  //از تاپیک های مختلف دانلود شده
                        url = BuildURL(serverId),
                        x = x,
                        y = y,
                        z = zomm,
                        bComplete = false
                    };
                    _waittodownload.Add(ri);  //هر مربع کوچک را در مجموعه ای که باید دانلود کنید قرار دهید
                    serverId = (serverId + 1) % 4;   //دانلود تصاویر از 4 سرور مختلف
                    threadId = (threadId + 1) % thread;  //دانلود تصاویر از تاپیک های مختلف
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
                groupBox2.Text = "خروجی(" + _waittodownload.Count + "باز کن)";
                _startTime = DateTime.Now;
                for (int i = 1; i <= thread; ++i)
                {
                    Thread t = new Thread(new ParameterizedThreadStart(DownloadThreadProc));
                    t.Start(i);  //تاپیک دانلود را شروع کنید
                }
            }
        }
        /// <summary>
        /// نقشه را مرور کنید
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
                    g.DrawImage(rf.Bitmap, new Point((rf.x - startx) * 256, (rf.y - starty) * 256));
                g.Dispose();
                b.Save(_path + "\\" + _zoom + "_total.jpg");
                b.Dispose();
                System.Diagnostics.Process.Start(_path + "\\" + _zoom + "_total.jpg");
            }
        }
        #region help methods
        /// <summary>
        /// یک پیشوند برای آدرس دانلود نقشه بر اساس شناسه سرور ایجاد کنید
        /// </summary>
        /// <param name="serverId"></param>
        /// <returns></returns>
        private string BuildURL(int serverId)
        {
            if (radioButton7.Checked)  //مشابه سرورهای خارجی http://mts0.googleapis.com/vt?lyrs=m&x=0&y=0&z=0
            {
                string url = "http://mts";
                url += serverId.ToString();
                url += ".googleapis.com/vt?lyrs=";
                if (radioButton1.Checked)  //نقشه راه
                    url += "m";
                if (radioButton2.Checked)  //تصویربرداری ماهواره ای
                    url += "s";
                if (radioButton3.Checked)  //تصاویر ماهواره ای با برچسب
                    url += "y";
                if (radioButton4.Checked)  //نقشه توپوگرافی
                    url += "t";
                if (radioButton5.Checked)  //نقشه توپوگرافی با برچسب
                    url += "p";
                return url;
            }
            if (radioButton6.Checked)  //سرور داخلی مشابه http://mt0.google.cn/vt/lyrs=m@234000000&hl=zh-CN&gl=CN&src=app&x=0&y=0&z=0
            {
                string url = "http://mt";
                url += serverId.ToString();
                url += ".google.cn/vt/lyrs=";
                if (radioButton1.Checked)  //نقشه راه
                    url += "m";
                if (radioButton2.Checked)  //تصویربرداری ماهواره ای
                    url += "s";
                if (radioButton3.Checked)  //تصاویر ماهواره ای با برچسب
                    url += "y";
                if (radioButton4.Checked)  //نقشه توپوگرافی
                    url += "t";
                if (radioButton5.Checked)  //نقشه توپوگرافی با برچسب
                    url += "p";
                url += "@234000000&hl=zh-CN&gl=CN&src=app";
                return url;
            }
            return "";
        }
        /// <summary>
        /// نقشه را با توجه به آدرس اینترنتی دانلود کنید
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private Bitmap DownloadImage(string url)
        {
            Bitmap bitmap = null;
            Stream stream = DownloadResource(url);
            if (stream != null)
                bitmap = new Bitmap(stream);
            return bitmap;
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
        /// روش تاپیک دانلود
        /// </summary>
        /// <param name="param"></param>
        public void DownloadThreadProc(object param)
        {
            int threadId = (int)param;  //شناسه موضوع فعلی
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
                        string url = ri.url;
                        //url را با توجه به سطر، ستون، سطح زوم هر تصویر اصلاح کنید
                        url += "&x=" + ri.x.ToString().Trim();  //فهرست کنید
                        url += "&y=" + ri.y.ToString().Trim();  //ردیف
                        url += "&z=" + ri.z.ToString().Trim();  //سطح زوم
                        Bitmap map = DownloadImage(url);
                        string file = _path + "\\" + ri.z.ToString() + "_" + ri.x.ToString() + "_" + ri.y.ToString() + ".jpg";
                        ri.Bitmap = map;
                        //فرمت ذخیره فایل "zoom_column_row.jpg"
                        map.Save(file, System.Drawing.Imaging.ImageFormat.Jpeg);
                        Invoke((Action)delegate()  //خروجی
                        {
                            rchOuput.SelectionColor = Color.Green;
                            rchOuput.AppendText(DateTime.Now.ToLongTimeString() + " شماره 1" + threadId + "عکس دانلود شماره تاپیک" + ri.z.ToString() + "_" + ri.x.ToString() + "_" + ri.y.ToString() + ".jpg\r\n");
                        });
                        _downloadnum++;
                        ri.bComplete = true;
                    }
                    catch
                    {
                        Invoke((Action)delegate()  //خروجی
                        {
                            rchOuput.SelectionColor = Color.Red;
                            rchOuput.AppendText(DateTime.Now.ToLongTimeString() + " شماره 1" + threadId + "عکس دانلود شماره تاپیک" + ri.z.ToString() + "_" + ri.x.ToString() + "_" + ri.y.ToString() + ".jpgمردود شدن！\r\n");
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
            if (_thread == 0) //تمام تاپیک ها به پایان می رسد
            {
                Invoke((Action)delegate()  //خروجی
                {
                    rchOuput.SelectionColor = Color.Blue;
                    rchOuput.AppendText(DateTime.Now.ToLongTimeString() + " دانلود تصویر تمام شد! مجموع دانلودها" + _downloadnum + "ژانگ، کل زمان" + (DateTime.Now-_startTime).TotalSeconds + "دومین");
                    groupBox1.Enabled = true;
                    BtnStart.Enabled = true;
                    BtnPreview.Enabled = true;
                });
            }
        }

        /// <summary>
        /// یک نقطه در سیستم مختصات طول و عرض جغرافیایی را به یک نقطه در یک نقشه دوبعدی مسطح (مبدا در گوشه سمت چپ بالای صفحه است) با توجه به زوم سطح زوم تبدیل کنید.
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
        public int serverId;  //سرور هدف
        public int threadId;  //موضوع دانلود هدف
        public string url;  //آدرس دانلود
        public int x;  //فهرست کنید
        public int y;  //ردیف
        public int z;  //سطح زوم
        public bool bComplete;  //آیا انجام شده است
        public Bitmap Bitmap; //تصویر
    }
}
