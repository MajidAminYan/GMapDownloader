namespace GoogleMapDownLoader
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.BtnBrowseSaveLocation = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.numThread = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.numZoom = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.txtsecondlat = new System.Windows.Forms.TextBox();
            this.txtsecondlng = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtfirstlat = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtfirstlng = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rchOuput = new System.Windows.Forms.RichTextBox();
            this.BtnPreview = new System.Windows.Forms.LinkLabel();
            this.BtnStart = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numThread)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numZoom)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Location = new System.Drawing.Point(18, 19);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(990, 331);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "تنظیمات پارامتر";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtUrl);
            this.groupBox3.Location = new System.Drawing.Point(21, 30);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Size = new System.Drawing.Size(949, 78);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "آدرس اینترنتی سرور تصاویر";
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(14, 30);
            this.txtUrl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.ReadOnly = true;
            this.txtUrl.Size = new System.Drawing.Size(911, 27);
            this.txtUrl.TabIndex = 15;
            this.txtUrl.Text = resources.GetString("txtUrl.Text");
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.BtnBrowseSaveLocation);
            this.groupBox5.Controls.Add(this.txtPath);
            this.groupBox5.Location = new System.Drawing.Point(21, 238);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox5.Size = new System.Drawing.Size(949, 78);
            this.groupBox5.TabIndex = 2;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "محل ذخیره تصاویر";
            // 
            // BtnBrowseSaveLocation
            // 
            this.BtnBrowseSaveLocation.Location = new System.Drawing.Point(14, 24);
            this.BtnBrowseSaveLocation.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnBrowseSaveLocation.Name = "BtnBrowseSaveLocation";
            this.BtnBrowseSaveLocation.Size = new System.Drawing.Size(170, 36);
            this.BtnBrowseSaveLocation.TabIndex = 1;
            this.BtnBrowseSaveLocation.Text = "مرور کردن";
            this.BtnBrowseSaveLocation.UseVisualStyleBackColor = true;
            this.BtnBrowseSaveLocation.Click += new System.EventHandler(this.BtnBrowseSaveLocation_Click);
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(192, 30);
            this.txtPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtPath.Name = "txtPath";
            this.txtPath.ReadOnly = true;
            this.txtPath.Size = new System.Drawing.Size(733, 27);
            this.txtPath.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.numThread);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.numZoom);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.txtsecondlat);
            this.groupBox4.Controls.Add(this.txtsecondlng);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.txtfirstlat);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.txtfirstlng);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Location = new System.Drawing.Point(21, 113);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox4.Size = new System.Drawing.Size(949, 115);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "محدوده";
            // 
            // numThread
            // 
            this.numThread.Location = new System.Drawing.Point(92, 72);
            this.numThread.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numThread.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numThread.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numThread.Name = "numThread";
            this.numThread.Size = new System.Drawing.Size(148, 27);
            this.numThread.TabIndex = 11;
            this.numThread.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(248, 74);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 19);
            this.label6.TabIndex = 10;
            this.label6.Text = "تعداد رشته";
            // 
            // numZoom
            // 
            this.numZoom.Location = new System.Drawing.Point(92, 34);
            this.numZoom.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numZoom.Maximum = new decimal(new int[] {
            17,
            0,
            0,
            0});
            this.numZoom.Name = "numZoom";
            this.numZoom.Size = new System.Drawing.Size(148, 27);
            this.numZoom.TabIndex = 9;
            this.numZoom.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(248, 36);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 19);
            this.label5.TabIndex = 8;
            this.label5.Text = "سطح زوم";
            // 
            // txtsecondlat
            // 
            this.txtsecondlat.Location = new System.Drawing.Point(406, 70);
            this.txtsecondlat.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtsecondlat.Name = "txtsecondlat";
            this.txtsecondlat.Size = new System.Drawing.Size(148, 27);
            this.txtsecondlat.TabIndex = 7;
            this.txtsecondlat.Text = "-90";
            // 
            // txtsecondlng
            // 
            this.txtsecondlng.Location = new System.Drawing.Point(406, 33);
            this.txtsecondlng.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtsecondlng.Name = "txtsecondlng";
            this.txtsecondlng.Size = new System.Drawing.Size(148, 27);
            this.txtsecondlng.TabIndex = 5;
            this.txtsecondlng.Text = "180";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(397, 9);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(132, 19);
            this.label3.TabIndex = 4;
            this.label3.Text = "سمت راست پایین";
            // 
            // txtfirstlat
            // 
            this.txtfirstlat.Location = new System.Drawing.Point(562, 70);
            this.txtfirstlat.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtfirstlat.Name = "txtfirstlat";
            this.txtfirstlat.Size = new System.Drawing.Size(148, 27);
            this.txtfirstlat.TabIndex = 3;
            this.txtfirstlat.Text = "90";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(712, 74);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "عرض جغرافیایی";
            // 
            // txtfirstlng
            // 
            this.txtfirstlng.Location = new System.Drawing.Point(562, 33);
            this.txtfirstlng.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtfirstlng.Name = "txtfirstlng";
            this.txtfirstlng.Size = new System.Drawing.Size(148, 27);
            this.txtfirstlng.TabIndex = 1;
            this.txtfirstlng.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(609, 9);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(105, 19);
            this.label7.TabIndex = 0;
            this.label7.Text = "سمت چپ بالا";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(718, 33);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "طول جغرافیایی";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rchOuput);
            this.groupBox2.Controls.Add(this.BtnPreview);
            this.groupBox2.Location = new System.Drawing.Point(23, 419);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Size = new System.Drawing.Size(990, 297);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "خروجی";
            // 
            // rchOuput
            // 
            this.rchOuput.BackColor = System.Drawing.Color.PapayaWhip;
            this.rchOuput.Location = new System.Drawing.Point(21, 33);
            this.rchOuput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rchOuput.Name = "rchOuput";
            this.rchOuput.ReadOnly = true;
            this.rchOuput.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rchOuput.Size = new System.Drawing.Size(949, 244);
            this.rchOuput.TabIndex = 0;
            this.rchOuput.Text = "";
            // 
            // BtnPreview
            // 
            this.BtnPreview.AutoSize = true;
            this.BtnPreview.Location = new System.Drawing.Point(0, 0);
            this.BtnPreview.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.BtnPreview.Name = "BtnPreview";
            this.BtnPreview.Size = new System.Drawing.Size(162, 19);
            this.BtnPreview.TabIndex = 2;
            this.BtnPreview.TabStop = true;
            this.BtnPreview.Text = "مشاهده نقشه ترکیبی";
            this.BtnPreview.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.BtnPreview_Clicked);
            // 
            // BtnStart
            // 
            this.BtnStart.Location = new System.Drawing.Point(23, 360);
            this.BtnStart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnStart.Name = "BtnStart";
            this.BtnStart.Size = new System.Drawing.Size(990, 49);
            this.BtnStart.TabIndex = 3;
            this.BtnStart.Text = "دانلود را شروع کن";
            this.BtnStart.UseVisualStyleBackColor = true;
            this.BtnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1026, 787);
            this.Controls.Add(this.BtnStart);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "دانلود نقشه گوگل";
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numThread)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numZoom)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox rchOuput;
        private System.Windows.Forms.LinkLabel BtnPreview;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.NumericUpDown numZoom;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtsecondlat;
        private System.Windows.Forms.TextBox txtsecondlng;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtfirstlat;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtfirstlng;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button BtnBrowseSaveLocation;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button BtnStart;
        private System.Windows.Forms.NumericUpDown numThread;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtUrl;
    }
}

