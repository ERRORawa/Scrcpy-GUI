﻿namespace scrcpy_gui
{
    partial class Main
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Start = new System.Windows.Forms.Button();
            this.UsbToWifi = new System.Windows.Forms.Button();
            this.WirelessDebug = new System.Windows.Forms.Button();
            this.ConnectedTitle = new System.Windows.Forms.Label();
            this.Reset = new System.Windows.Forms.Button();
            this.CheckDevices = new System.Windows.Forms.Timer(this.components);
            this.ConnectedDevices = new System.Windows.Forms.Label();
            this.UnauthTitle = new System.Windows.Forms.Label();
            this.UnauthDevices = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Start
            // 
            this.Start.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Start.Location = new System.Drawing.Point(21, 15);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(150, 60);
            this.Start.TabIndex = 0;
            this.Start.Text = "开始投屏";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // UsbToWifi
            // 
            this.UsbToWifi.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.UsbToWifi.Location = new System.Drawing.Point(21, 115);
            this.UsbToWifi.Name = "UsbToWifi";
            this.UsbToWifi.Size = new System.Drawing.Size(150, 60);
            this.UsbToWifi.TabIndex = 1;
            this.UsbToWifi.Text = "USB转WiFi";
            this.UsbToWifi.UseVisualStyleBackColor = true;
            this.UsbToWifi.Click += new System.EventHandler(this.UsbToWifi_Click);
            // 
            // WirelessDebug
            // 
            this.WirelessDebug.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.WirelessDebug.Location = new System.Drawing.Point(21, 215);
            this.WirelessDebug.Name = "WirelessDebug";
            this.WirelessDebug.Size = new System.Drawing.Size(150, 60);
            this.WirelessDebug.TabIndex = 2;
            this.WirelessDebug.Text = "无线调试连接";
            this.WirelessDebug.UseVisualStyleBackColor = true;
            this.WirelessDebug.Click += new System.EventHandler(this.WirelessDebug_Click);
            // 
            // ConnectedTitle
            // 
            this.ConnectedTitle.AutoSize = true;
            this.ConnectedTitle.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.ConnectedTitle.Location = new System.Drawing.Point(195, 20);
            this.ConnectedTitle.Name = "ConnectedTitle";
            this.ConnectedTitle.Size = new System.Drawing.Size(122, 21);
            this.ConnectedTitle.TabIndex = 4;
            this.ConnectedTitle.Text = "已连接的设备：\r\n";
            // 
            // Reset
            // 
            this.Reset.Location = new System.Drawing.Point(324, 284);
            this.Reset.Name = "Reset";
            this.Reset.Size = new System.Drawing.Size(93, 35);
            this.Reset.TabIndex = 5;
            this.Reset.Text = "重置连接状态";
            this.Reset.UseVisualStyleBackColor = true;
            this.Reset.Click += new System.EventHandler(this.Reset_Click);
            // 
            // CheckDevices
            // 
            this.CheckDevices.Enabled = true;
            this.CheckDevices.Interval = 10;
            this.CheckDevices.Tick += new System.EventHandler(this.CheckDevices_Tick);
            // 
            // ConnectedDevices
            // 
            this.ConnectedDevices.AutoSize = true;
            this.ConnectedDevices.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.ConnectedDevices.Location = new System.Drawing.Point(200, 50);
            this.ConnectedDevices.Name = "ConnectedDevices";
            this.ConnectedDevices.Size = new System.Drawing.Size(51, 20);
            this.ConnectedDevices.TabIndex = 6;
            this.ConnectedDevices.Text = "无设备";
            // 
            // UnauthTitle
            // 
            this.UnauthTitle.AutoSize = true;
            this.UnauthTitle.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.UnauthTitle.Location = new System.Drawing.Point(380, 20);
            this.UnauthTitle.Name = "UnauthTitle";
            this.UnauthTitle.Size = new System.Drawing.Size(122, 21);
            this.UnauthTitle.TabIndex = 7;
            this.UnauthTitle.Text = "未授权的设备：\r\n";
            // 
            // UnauthDevices
            // 
            this.UnauthDevices.AutoSize = true;
            this.UnauthDevices.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.UnauthDevices.Location = new System.Drawing.Point(385, 50);
            this.UnauthDevices.Name = "UnauthDevices";
            this.UnauthDevices.Size = new System.Drawing.Size(51, 20);
            this.UnauthDevices.TabIndex = 8;
            this.UnauthDevices.Text = "无设备";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::scrcpy_gui.Resource._1;
            this.pictureBox1.Location = new System.Drawing.Point(384, 176);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(201, 186);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.UnauthDevices);
            this.Controls.Add(this.UnauthTitle);
            this.Controls.Add(this.ConnectedDevices);
            this.Controls.Add(this.Reset);
            this.Controls.Add(this.ConnectedTitle);
            this.Controls.Add(this.WirelessDebug);
            this.Controls.Add(this.UsbToWifi);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "菜单";
            this.TransparencyKey = System.Drawing.Color.Transparent;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_FormClosed);
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.Button UsbToWifi;
        private System.Windows.Forms.Button WirelessDebug;
        private System.Windows.Forms.Label ConnectedTitle;
        private System.Windows.Forms.Button Reset;
        private System.Windows.Forms.Timer CheckDevices;
        private System.Windows.Forms.Label ConnectedDevices;
        private System.Windows.Forms.Label UnauthTitle;
        private System.Windows.Forms.Label UnauthDevices;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

