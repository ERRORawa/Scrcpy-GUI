namespace scrcpy_gui
{
    partial class WirelessDebugging
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
            this.components = new System.ComponentModel.Container();
            this.EnterIP = new System.Windows.Forms.Label();
            this.IP = new System.Windows.Forms.TextBox();
            this.Port = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Enter = new System.Windows.Forms.Button();
            this.CheckPortLabel = new System.Windows.Forms.Label();
            this.CheckPortTimer = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // EnterIP
            // 
            this.EnterIP.AutoSize = true;
            this.EnterIP.Font = new System.Drawing.Font("微软雅黑", 18F);
            this.EnterIP.Location = new System.Drawing.Point(91, 27);
            this.EnterIP.Name = "EnterIP";
            this.EnterIP.Size = new System.Drawing.Size(204, 31);
            this.EnterIP.TabIndex = 0;
            this.EnterIP.Text = "请输入无线调试IP";
            // 
            // IP
            // 
            this.IP.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.IP.ForeColor = System.Drawing.Color.Silver;
            this.IP.Location = new System.Drawing.Point(12, 86);
            this.IP.Name = "IP";
            this.IP.Size = new System.Drawing.Size(183, 36);
            this.IP.TabIndex = 1;
            this.IP.Text = "192.168.X.X";
            this.IP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.IP.Enter += new System.EventHandler(this.IP_Enter);
            this.IP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IP_KeyPress);
            this.IP.Leave += new System.EventHandler(this.IP_Leave);
            // 
            // Port
            // 
            this.Port.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.Port.ForeColor = System.Drawing.Color.Silver;
            this.Port.Location = new System.Drawing.Point(224, 86);
            this.Port.Name = "Port";
            this.Port.Size = new System.Drawing.Size(91, 36);
            this.Port.TabIndex = 2;
            this.Port.Text = "XXXXX";
            this.Port.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Port.TextChanged += new System.EventHandler(this.Port_TextChanged);
            this.Port.Enter += new System.EventHandler(this.Port_Enter);
            this.Port.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Port_KeyPress);
            this.Port.Leave += new System.EventHandler(this.Port_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(201, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 27);
            this.label1.TabIndex = 3;
            this.label1.Text = ":";
            // 
            // Enter
            // 
            this.Enter.Font = new System.Drawing.Font("微软雅黑", 14.25F);
            this.Enter.Location = new System.Drawing.Point(156, 175);
            this.Enter.Name = "Enter";
            this.Enter.Size = new System.Drawing.Size(100, 40);
            this.Enter.TabIndex = 4;
            this.Enter.Text = "连接";
            this.Enter.UseVisualStyleBackColor = true;
            this.Enter.Click += new System.EventHandler(this.Enter_Click);
            // 
            // CheckPortLabel
            // 
            this.CheckPortLabel.AutoSize = true;
            this.CheckPortLabel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CheckPortLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.CheckPortLabel.Location = new System.Drawing.Point(220, 125);
            this.CheckPortLabel.Name = "CheckPortLabel";
            this.CheckPortLabel.Size = new System.Drawing.Size(106, 21);
            this.CheckPortLabel.TabIndex = 5;
            this.CheckPortLabel.Text = "只能输入数字";
            // 
            // CheckPortTimer
            // 
            this.CheckPortTimer.Interval = 1;
            this.CheckPortTimer.Tick += new System.EventHandler(this.CheckPortTimer_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::scrcpy_gui.Resource.e06a4bb27b4416f;
            this.pictureBox1.Location = new System.Drawing.Point(301, 27);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(128, 132);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // WirelessDebugging
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 227);
            this.Controls.Add(this.CheckPortLabel);
            this.Controls.Add(this.Enter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Port);
            this.Controls.Add(this.IP);
            this.Controls.Add(this.EnterIP);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "WirelessDebugging";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "无线调试";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.WirelessDebugging_FormClosed);
            this.Load += new System.EventHandler(this.WirelessDebugging_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label EnterIP;
        private System.Windows.Forms.TextBox IP;
        private System.Windows.Forms.TextBox Port;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Enter;
        private System.Windows.Forms.Label CheckPortLabel;
        private System.Windows.Forms.Timer CheckPortTimer;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}