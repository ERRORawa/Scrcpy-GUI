namespace scrcpy_gui
{
    partial class SelectDevices
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
            this.SelectDevice = new System.Windows.Forms.ListBox();
            this.SelectDeviceTitle = new System.Windows.Forms.Label();
            this.Cancel = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // SelectDevice
            // 
            this.SelectDevice.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SelectDevice.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.SelectDevice.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SelectDevice.FormattingEnabled = true;
            this.SelectDevice.ItemHeight = 30;
            this.SelectDevice.Location = new System.Drawing.Point(12, 67);
            this.SelectDevice.Name = "SelectDevice";
            this.SelectDevice.Size = new System.Drawing.Size(266, 166);
            this.SelectDevice.TabIndex = 0;
            this.SelectDevice.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.SelectDevice_DrawItem);
            this.SelectDevice.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.SelectDevice_MouseDoubleClick);
            // 
            // SelectDeviceTitle
            // 
            this.SelectDeviceTitle.AutoSize = true;
            this.SelectDeviceTitle.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SelectDeviceTitle.Location = new System.Drawing.Point(141, 21);
            this.SelectDeviceTitle.Name = "SelectDeviceTitle";
            this.SelectDeviceTitle.Size = new System.Drawing.Size(129, 29);
            this.SelectDeviceTitle.TabIndex = 1;
            this.SelectDeviceTitle.Text = "选择设备";
            // 
            // Cancel
            // 
            this.Cancel.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Cancel.Location = new System.Drawing.Point(146, 255);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(100, 40);
            this.Cancel.TabIndex = 2;
            this.Cancel.Text = "取消";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::scrcpy_gui.Resource.OIP_C__2_;
            this.pictureBox1.Location = new System.Drawing.Point(284, 182);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(138, 126);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // SelectDevices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(419, 307);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.SelectDeviceTitle);
            this.Controls.Add(this.SelectDevice);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SelectDevices";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择设备";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Cancel_Click);
            this.Load += new System.EventHandler(this.SelectDevices_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox SelectDevice;
        private System.Windows.Forms.Label SelectDeviceTitle;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}