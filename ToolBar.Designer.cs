namespace scrcpy_gui
{
    partial class ToolBar
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
            this.FollowScrcpy = new System.Windows.Forms.Timer(this.components);
            this.CheckMouse = new System.Windows.Forms.Timer(this.components);
            this.More = new System.Windows.Forms.Button();
            this.MuliTask = new System.Windows.Forms.Button();
            this.Home = new System.Windows.Forms.Button();
            this.Back = new System.Windows.Forms.Button();
            this.Screenshot = new System.Windows.Forms.Button();
            this.VolumeDown = new System.Windows.Forms.Button();
            this.VolumeUp = new System.Windows.Forms.Button();
            this.Power = new System.Windows.Forms.Button();
            this.TopForm = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // FollowScrcpy
            // 
            this.FollowScrcpy.Enabled = true;
            this.FollowScrcpy.Interval = 1;
            this.FollowScrcpy.Tick += new System.EventHandler(this.FollowScrcpy_Tick);
            // 
            // CheckMouse
            // 
            this.CheckMouse.Enabled = true;
            this.CheckMouse.Interval = 8000;
            this.CheckMouse.Tick += new System.EventHandler(this.CheckMouse_Tick);
            // 
            // More
            // 
            this.More.BackgroundImage = global::scrcpy_gui.Resource.more;
            this.More.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.More.Location = new System.Drawing.Point(5, 320);
            this.More.Name = "More";
            this.More.Size = new System.Drawing.Size(40, 40);
            this.More.TabIndex = 7;
            this.More.UseVisualStyleBackColor = true;
            this.More.Click += new System.EventHandler(this.More_Click);
            // 
            // MuliTask
            // 
            this.MuliTask.BackgroundImage = global::scrcpy_gui.Resource.task;
            this.MuliTask.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.MuliTask.Location = new System.Drawing.Point(5, 275);
            this.MuliTask.Name = "MuliTask";
            this.MuliTask.Size = new System.Drawing.Size(40, 40);
            this.MuliTask.TabIndex = 6;
            this.MuliTask.UseVisualStyleBackColor = true;
            this.MuliTask.Click += new System.EventHandler(this.MuliTask_Click);
            // 
            // Home
            // 
            this.Home.BackgroundImage = global::scrcpy_gui.Resource.home;
            this.Home.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Home.Location = new System.Drawing.Point(5, 230);
            this.Home.Name = "Home";
            this.Home.Size = new System.Drawing.Size(40, 40);
            this.Home.TabIndex = 5;
            this.Home.UseVisualStyleBackColor = true;
            this.Home.Click += new System.EventHandler(this.Home_Click);
            // 
            // Back
            // 
            this.Back.BackgroundImage = global::scrcpy_gui.Resource.back;
            this.Back.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Back.Location = new System.Drawing.Point(5, 185);
            this.Back.Name = "Back";
            this.Back.Size = new System.Drawing.Size(40, 40);
            this.Back.TabIndex = 4;
            this.Back.UseVisualStyleBackColor = true;
            this.Back.Click += new System.EventHandler(this.Back_Click);
            // 
            // Screenshot
            // 
            this.Screenshot.BackgroundImage = global::scrcpy_gui.Resource.screenshot;
            this.Screenshot.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Screenshot.Location = new System.Drawing.Point(5, 140);
            this.Screenshot.Name = "Screenshot";
            this.Screenshot.Size = new System.Drawing.Size(40, 40);
            this.Screenshot.TabIndex = 3;
            this.Screenshot.UseVisualStyleBackColor = true;
            this.Screenshot.Click += new System.EventHandler(this.Screenshot_Click);
            // 
            // VolumeDown
            // 
            this.VolumeDown.BackgroundImage = global::scrcpy_gui.Resource.volumeDown;
            this.VolumeDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.VolumeDown.Location = new System.Drawing.Point(5, 95);
            this.VolumeDown.Name = "VolumeDown";
            this.VolumeDown.Size = new System.Drawing.Size(40, 40);
            this.VolumeDown.TabIndex = 2;
            this.VolumeDown.UseVisualStyleBackColor = true;
            this.VolumeDown.Click += new System.EventHandler(this.VolumeDown_Click);
            // 
            // VolumeUp
            // 
            this.VolumeUp.BackgroundImage = global::scrcpy_gui.Resource.volumeUp;
            this.VolumeUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.VolumeUp.Location = new System.Drawing.Point(5, 50);
            this.VolumeUp.Name = "VolumeUp";
            this.VolumeUp.Size = new System.Drawing.Size(40, 40);
            this.VolumeUp.TabIndex = 1;
            this.VolumeUp.UseVisualStyleBackColor = true;
            this.VolumeUp.Click += new System.EventHandler(this.VolumeUp_Click);
            // 
            // Power
            // 
            this.Power.BackgroundImage = global::scrcpy_gui.Resource.power;
            this.Power.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Power.Location = new System.Drawing.Point(5, 5);
            this.Power.Name = "Power";
            this.Power.Size = new System.Drawing.Size(40, 40);
            this.Power.TabIndex = 0;
            this.Power.UseVisualStyleBackColor = true;
            this.Power.Click += new System.EventHandler(this.Power_Click);
            // 
            // TopForm
            // 
            this.TopForm.Enabled = true;
            this.TopForm.Interval = 10;
            this.TopForm.Tick += new System.EventHandler(this.TopForm_Tick);
            // 
            // ToolBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(50, 365);
            this.Controls.Add(this.More);
            this.Controls.Add(this.MuliTask);
            this.Controls.Add(this.Home);
            this.Controls.Add(this.Back);
            this.Controls.Add(this.Screenshot);
            this.Controls.Add(this.VolumeDown);
            this.Controls.Add(this.VolumeUp);
            this.Controls.Add(this.Power);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ToolBar";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "ToolBar";
            this.TopMost = true;
            this.Click += new System.EventHandler(this.ToolBar_Click);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Power;
        private System.Windows.Forms.Button VolumeUp;
        private System.Windows.Forms.Button VolumeDown;
        private System.Windows.Forms.Button Screenshot;
        private System.Windows.Forms.Button Back;
        private System.Windows.Forms.Button Home;
        private System.Windows.Forms.Button MuliTask;
        private System.Windows.Forms.Timer TopForm;
        private System.Windows.Forms.Timer FollowScrcpy;
        private System.Windows.Forms.Timer CheckMouse;
        private System.Windows.Forms.Button More;
    }
}