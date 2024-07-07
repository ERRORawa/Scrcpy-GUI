namespace Scrcpy_GUI
{
    partial class More
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(More));
            this.WindowTitleLabel = new System.Windows.Forms.Label();
            this.Key = new System.Windows.Forms.Label();
            this.FileFolderLabel = new System.Windows.Forms.Label();
            this.Enter = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.FileFolder = new System.Windows.Forms.TextBox();
            this.WindowTitle = new System.Windows.Forms.TextBox();
            this.StandOn = new System.Windows.Forms.CheckBox();
            this.EnableAudio = new System.Windows.Forms.CheckBox();
            this.ScreenOff = new System.Windows.Forms.CheckBox();
            this.DisableControl = new System.Windows.Forms.CheckBox();
            this.CutpadSync = new System.Windows.Forms.CheckBox();
            this.DisableScreenProtect = new System.Windows.Forms.CheckBox();
            this.OnTop = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.AlwaysShowToolBar = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // WindowTitleLabel
            // 
            this.WindowTitleLabel.AutoSize = true;
            this.WindowTitleLabel.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WindowTitleLabel.Location = new System.Drawing.Point(38, 152);
            this.WindowTitleLabel.Name = "WindowTitleLabel";
            this.WindowTitleLabel.Size = new System.Drawing.Size(249, 20);
            this.WindowTitleLabel.TabIndex = 6;
            this.WindowTitleLabel.Text = "更改窗口标题（置空则为默认标题）";
            // 
            // Key
            // 
            this.Key.AutoSize = true;
            this.Key.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Key.Location = new System.Drawing.Point(12, 274);
            this.Key.Name = "Key";
            this.Key.Size = new System.Drawing.Size(362, 210);
            this.Key.TabIndex = 9;
            this.Key.Text = resources.GetString("Key.Text");
            // 
            // FileFolderLabel
            // 
            this.FileFolderLabel.AutoSize = true;
            this.FileFolderLabel.Font = new System.Drawing.Font("微软雅黑", 11.25F);
            this.FileFolderLabel.Location = new System.Drawing.Point(387, 152);
            this.FileFolderLabel.Name = "FileFolderLabel";
            this.FileFolderLabel.Size = new System.Drawing.Size(129, 20);
            this.FileFolderLabel.TabIndex = 10;
            this.FileFolderLabel.Text = "更改文件存放目录";
            // 
            // Enter
            // 
            this.Enter.AutoSize = true;
            this.Enter.Enabled = false;
            this.Enter.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Enter.Location = new System.Drawing.Point(498, 454);
            this.Enter.Name = "Enter";
            this.Enter.Size = new System.Drawing.Size(57, 30);
            this.Enter.TabIndex = 12;
            this.Enter.Text = "确定";
            this.Enter.UseVisualStyleBackColor = true;
            this.Enter.Click += new System.EventHandler(this.Enter_Click);
            // 
            // Cancel
            // 
            this.Cancel.AutoSize = true;
            this.Cancel.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Cancel.Location = new System.Drawing.Point(561, 454);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(57, 30);
            this.Cancel.TabIndex = 13;
            this.Cancel.Text = "取消";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // FileFolder
            // 
            this.FileFolder.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Scrcpy_GUI.Settings.Default, "文件存放目录", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.FileFolder.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.FileFolder.Location = new System.Drawing.Point(381, 189);
            this.FileFolder.Name = "FileFolder";
            this.FileFolder.Size = new System.Drawing.Size(141, 25);
            this.FileFolder.TabIndex = 11;
            this.FileFolder.Text = global::Scrcpy_GUI.Settings.Default.文件存放目录;
            this.FileFolder.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.FileFolder.TextChanged += new System.EventHandler(this.FileFolder_TextChanged);
            // 
            // WindowTitle
            // 
            this.WindowTitle.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Scrcpy_GUI.Settings.Default, "窗口标题", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.WindowTitle.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.WindowTitle.Location = new System.Drawing.Point(106, 189);
            this.WindowTitle.MaxLength = 15;
            this.WindowTitle.Name = "WindowTitle";
            this.WindowTitle.Size = new System.Drawing.Size(100, 25);
            this.WindowTitle.TabIndex = 7;
            this.WindowTitle.Text = global::Scrcpy_GUI.Settings.Default.窗口标题;
            this.WindowTitle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.WindowTitle.TextChanged += new System.EventHandler(this.WindowTitle_TextChanged);
            this.WindowTitle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.WindowTitle_KeyPress);
            // 
            // StandOn
            // 
            this.StandOn.Appearance = System.Windows.Forms.Appearance.Button;
            this.StandOn.AutoSize = true;
            this.StandOn.Checked = global::Scrcpy_GUI.Settings.Default.保持唤醒;
            this.StandOn.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.StandOn.Location = new System.Drawing.Point(170, 61);
            this.StandOn.Name = "StandOn";
            this.StandOn.Size = new System.Drawing.Size(84, 31);
            this.StandOn.TabIndex = 5;
            this.StandOn.Text = "保持唤醒";
            this.StandOn.UseVisualStyleBackColor = true;
            this.StandOn.CheckedChanged += new System.EventHandler(this.StandOn_CheckedChanged);
            this.StandOn.MouseHover += new System.EventHandler(this.StandOn_MouseHover);
            // 
            // EnableAudio
            // 
            this.EnableAudio.Appearance = System.Windows.Forms.Appearance.Button;
            this.EnableAudio.AutoSize = true;
            this.EnableAudio.Checked = global::Scrcpy_GUI.Settings.Default.音频流转;
            this.EnableAudio.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.EnableAudio.Location = new System.Drawing.Point(472, 24);
            this.EnableAudio.Name = "EnableAudio";
            this.EnableAudio.Size = new System.Drawing.Size(84, 31);
            this.EnableAudio.TabIndex = 0;
            this.EnableAudio.Text = "音频流转";
            this.EnableAudio.UseVisualStyleBackColor = true;
            this.EnableAudio.CheckedChanged += new System.EventHandler(this.EnableAudio_CheckedChanged);
            this.EnableAudio.MouseHover += new System.EventHandler(this.EnableAudio_MouseHover);
            // 
            // ScreenOff
            // 
            this.ScreenOff.Appearance = System.Windows.Forms.Appearance.Button;
            this.ScreenOff.AutoSize = true;
            this.ScreenOff.Checked = global::Scrcpy_GUI.Settings.Default.结束后关闭屏幕;
            this.ScreenOff.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.ScreenOff.Location = new System.Drawing.Point(32, 61);
            this.ScreenOff.Name = "ScreenOff";
            this.ScreenOff.Size = new System.Drawing.Size(132, 31);
            this.ScreenOff.TabIndex = 4;
            this.ScreenOff.Text = "结束后关闭屏幕";
            this.ScreenOff.UseVisualStyleBackColor = true;
            this.ScreenOff.CheckedChanged += new System.EventHandler(this.ScreenOff_CheckedChanged);
            this.ScreenOff.MouseHover += new System.EventHandler(this.ScreenOff_MouseHover);
            // 
            // DisableControl
            // 
            this.DisableControl.Appearance = System.Windows.Forms.Appearance.Button;
            this.DisableControl.AutoSize = true;
            this.DisableControl.Checked = global::Scrcpy_GUI.Settings.Default.禁用控制;
            this.DisableControl.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.DisableControl.Location = new System.Drawing.Point(382, 24);
            this.DisableControl.Name = "DisableControl";
            this.DisableControl.Size = new System.Drawing.Size(84, 31);
            this.DisableControl.TabIndex = 2;
            this.DisableControl.Text = "禁用控制";
            this.DisableControl.UseVisualStyleBackColor = true;
            this.DisableControl.CheckedChanged += new System.EventHandler(this.DisableControl_CheckedChanged);
            this.DisableControl.MouseHover += new System.EventHandler(this.DisableControl_MouseHover);
            // 
            // CutpadSync
            // 
            this.CutpadSync.Appearance = System.Windows.Forms.Appearance.Button;
            this.CutpadSync.AutoSize = true;
            this.CutpadSync.BackColor = System.Drawing.SystemColors.Control;
            this.CutpadSync.Checked = global::Scrcpy_GUI.Settings.Default.剪切板同步;
            this.CutpadSync.Cursor = System.Windows.Forms.Cursors.Default;
            this.CutpadSync.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.CutpadSync.Location = new System.Drawing.Point(276, 24);
            this.CutpadSync.Name = "CutpadSync";
            this.CutpadSync.Size = new System.Drawing.Size(100, 31);
            this.CutpadSync.TabIndex = 1;
            this.CutpadSync.Text = "剪切板同步";
            this.CutpadSync.UseVisualStyleBackColor = true;
            this.CutpadSync.CheckedChanged += new System.EventHandler(this.CutpadSync_CheckedChanged);
            this.CutpadSync.MouseHover += new System.EventHandler(this.CutpadSync_MouseHover);
            // 
            // DisableScreenProtect
            // 
            this.DisableScreenProtect.Appearance = System.Windows.Forms.Appearance.Button;
            this.DisableScreenProtect.AutoSize = true;
            this.DisableScreenProtect.Checked = global::Scrcpy_GUI.Settings.Default.禁用屏幕保护程序;
            this.DisableScreenProtect.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.DisableScreenProtect.Location = new System.Drawing.Point(122, 24);
            this.DisableScreenProtect.Name = "DisableScreenProtect";
            this.DisableScreenProtect.Size = new System.Drawing.Size(148, 31);
            this.DisableScreenProtect.TabIndex = 1;
            this.DisableScreenProtect.Text = "禁用屏幕保护程序";
            this.DisableScreenProtect.UseVisualStyleBackColor = true;
            this.DisableScreenProtect.CheckedChanged += new System.EventHandler(this.DisableScreenProtect_CheckedChanged);
            this.DisableScreenProtect.MouseHover += new System.EventHandler(this.DisableScreenProtect_MouseHover);
            // 
            // OnTop
            // 
            this.OnTop.Appearance = System.Windows.Forms.Appearance.Button;
            this.OnTop.AutoSize = true;
            this.OnTop.BackColor = System.Drawing.SystemColors.Control;
            this.OnTop.Checked = global::Scrcpy_GUI.Settings.Default.置顶;
            this.OnTop.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.OnTop.Location = new System.Drawing.Point(32, 24);
            this.OnTop.Name = "OnTop";
            this.OnTop.Size = new System.Drawing.Size(84, 31);
            this.OnTop.TabIndex = 0;
            this.OnTop.Text = "置于顶层";
            this.OnTop.UseVisualStyleBackColor = true;
            this.OnTop.CheckedChanged += new System.EventHandler(this.OnTop_CheckedChanged);
            this.OnTop.MouseHover += new System.EventHandler(this.OnTop_MouseHover);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(274, 235);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 20);
            this.label1.TabIndex = 14;
            // 
            // AlwaysShowToolBar
            // 
            this.AlwaysShowToolBar.Appearance = System.Windows.Forms.Appearance.Button;
            this.AlwaysShowToolBar.AutoSize = true;
            this.AlwaysShowToolBar.Checked = global::Scrcpy_GUI.Settings.Default.保持唤醒;
            this.AlwaysShowToolBar.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.AlwaysShowToolBar.Location = new System.Drawing.Point(260, 61);
            this.AlwaysShowToolBar.Name = "AlwaysShowToolBar";
            this.AlwaysShowToolBar.Size = new System.Drawing.Size(132, 31);
            this.AlwaysShowToolBar.TabIndex = 15;
            this.AlwaysShowToolBar.Text = "始终显示工具栏";
            this.AlwaysShowToolBar.UseVisualStyleBackColor = true;
            this.AlwaysShowToolBar.CheckedChanged += new System.EventHandler(this.AlwaysShowToolBar_CheckedChanged);
            this.AlwaysShowToolBar.MouseHover += new System.EventHandler(this.AlwaysShowToolBar_MouseHover);
            // 
            // More
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 493);
            this.Controls.Add(this.AlwaysShowToolBar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Enter);
            this.Controls.Add(this.FileFolder);
            this.Controls.Add(this.FileFolderLabel);
            this.Controls.Add(this.Key);
            this.Controls.Add(this.WindowTitle);
            this.Controls.Add(this.WindowTitleLabel);
            this.Controls.Add(this.StandOn);
            this.Controls.Add(this.EnableAudio);
            this.Controls.Add(this.ScreenOff);
            this.Controls.Add(this.DisableControl);
            this.Controls.Add(this.CutpadSync);
            this.Controls.Add(this.DisableScreenProtect);
            this.Controls.Add(this.OnTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "More";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "更多";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.More_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox OnTop;
        private System.Windows.Forms.CheckBox DisableScreenProtect;
        private System.Windows.Forms.CheckBox EnableAudio;
        private System.Windows.Forms.CheckBox ScreenOff;
        private System.Windows.Forms.CheckBox DisableControl;
        private System.Windows.Forms.CheckBox CutpadSync;
        private System.Windows.Forms.CheckBox StandOn;
        private System.Windows.Forms.Label WindowTitleLabel;
        private System.Windows.Forms.TextBox WindowTitle;
        private System.Windows.Forms.Label Key;
        private System.Windows.Forms.Label FileFolderLabel;
        private System.Windows.Forms.TextBox FileFolder;
        private System.Windows.Forms.Button Enter;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox AlwaysShowToolBar;
    }
}