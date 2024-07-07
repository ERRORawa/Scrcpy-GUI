using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scrcpy_GUI
{
    public partial class More : Form
    {

        SelectDevices selectDevices = new SelectDevices();

        public string device;

        public More()
        {
            InitializeComponent();
        }

        private void More_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
        }

        private void OnTop_MouseHover(object sender, EventArgs e)       //设置OnTop悬浮提示
        {
            ToolTip toolTip = new ToolTip();
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 500;
            toolTip.ReshowDelay = 500;
            toolTip.ShowAlways = true;
            toolTip.SetToolTip(this.OnTop, "窗口置顶");
        }

        private void DisableScreenProtect_MouseHover(object sender, EventArgs e)       //设置DisableScreenProtect悬浮提示
        {
            ToolTip toolTip = new ToolTip();
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 500;
            toolTip.ReshowDelay = 500;
            toolTip.ShowAlways = true;
            toolTip.SetToolTip(this.DisableScreenProtect, "不知道有什么用");
        }

        private void CutpadSync_MouseHover(object sender, EventArgs e)       //设置CutpadSync悬浮提示
        {
            ToolTip toolTip = new ToolTip();
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 500;
            toolTip.ReshowDelay = 500;
            toolTip.ShowAlways = true;
            toolTip.SetToolTip(this.CutpadSync, "电脑上复制的东西可以粘贴到设备上，反之亦然");
        }

        private void DisableControl_MouseHover(object sender, EventArgs e)       //设置DisableControl悬浮提示
        {
            ToolTip toolTip = new ToolTip();
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 500;
            toolTip.ReshowDelay = 500;
            toolTip.ShowAlways = true;
            toolTip.SetToolTip(this.DisableControl, "电脑上只显示设备的画面，不可控制");
        }
        private void StandOn_MouseHover(object sender, EventArgs e)       //设置StandOn悬浮提示
        {
            ToolTip toolTip = new ToolTip();
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 500;
            toolTip.ReshowDelay = 500;
            toolTip.ShowAlways = true;
            toolTip.SetToolTip(this.StandOn, "不让设备自动息屏");
        }

        private void EnableAudio_MouseHover(object sender, EventArgs e)       //设置EnableAudio悬浮提示
        {
            ToolTip toolTip = new ToolTip();
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 500;
            toolTip.ReshowDelay = 500;
            toolTip.ShowAlways = true;
            toolTip.SetToolTip(this.EnableAudio, "设备的声音将通过电脑播放");
        }

        private void ScreenOff_MouseHover(object sender, EventArgs e)       //设置ScreenOff悬浮提示
        {
            ToolTip toolTip = new ToolTip();
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 500;
            toolTip.ReshowDelay = 500;
            toolTip.ShowAlways = true;
            toolTip.SetToolTip(this.ScreenOff, "关闭投屏后，设备将会关闭屏幕");
        }
        private void AlwaysShowToolBar_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 500;
            toolTip.ReshowDelay = 500;
            toolTip.ShowAlways = true;
            toolTip.SetToolTip(this.ScreenOff, "默认10秒不移动隐藏工具栏，开启此项以禁用");
        }
        private void Enter_Click(object sender, EventArgs e)
        {
            Settings.Default["置顶"] = OnTop.Checked;
            Settings.Default["禁用屏幕保护程序"] = DisableScreenProtect.Checked;
            Settings.Default["剪切板同步"] = CutpadSync.Checked;
            Settings.Default["禁用控制"] = DisableControl.Checked;
            Settings.Default["音频流转"] = EnableAudio.Checked;
            Settings.Default["结束后关闭屏幕"] = ScreenOff.Checked;
            Settings.Default["保持唤醒"] = StandOn.Checked;
            Settings.Default["始终显示工具栏"] = AlwaysShowToolBar.Checked;
            if (WindowTitle.Text == "")
            {
                Settings.Default["窗口标题"] = "Untitled";
            }
            else
            {
                Settings.Default["窗口标题"] = WindowTitle.Text;
            }
            if (FileFolder.Text == "")
            {
                Settings.Default["文件存放目录"] = "/sdcard/Download/";
            }
            else
            {
                Settings.Default["文件存放目录"] = FileFolder.Text;
            }
            Settings.Default.Save();
            this.Hide();
            selectDevices.Cmd("taskkill /F /IM scrcpy.exe");
            selectDevices.Cmd(this.GetType().Assembly.Location + " " + device);
        }

        private void OnTop_CheckedChanged(object sender, EventArgs e)
        {
            Enter.Enabled = true;
        }

        private void DisableScreenProtect_CheckedChanged(object sender, EventArgs e)
        {
            Enter.Enabled = true;
        }

        private void CutpadSync_CheckedChanged(object sender, EventArgs e)
        {
            Enter.Enabled = true;
        }

        private void DisableControl_CheckedChanged(object sender, EventArgs e)
        {
            if (DisableControl.Checked)
            {
                StandOn.Checked = false;
            }
            Enter.Enabled = true;
        }

        private void EnableAudio_CheckedChanged(object sender, EventArgs e)
        {
            Enter.Enabled = true;
        }

        private void ScreenOff_CheckedChanged(object sender, EventArgs e)
        {
            Enter.Enabled = true;
        }

        private void StandOn_CheckedChanged(object sender, EventArgs e)
        {
            if (StandOn.Checked)
            {
                DisableControl.Checked = false;
            }
            Enter.Enabled = true;
        }
        private void AlwaysShowToolBar_CheckedChanged(object sender, EventArgs e)
        {
            Enter.Enabled = true;
        }
        private void WindowTitle_TextChanged(object sender, EventArgs e)
        {
            label1.Text = WindowTitle.Text;
            WindowTitle.Width = label1.Width + 10;
            WindowTitle.Left = WindowTitleLabel.Left + WindowTitleLabel.Width / 2 - WindowTitle.Width / 2;
            Enter.Enabled = true;
        }

        private void FileFolder_TextChanged(object sender, EventArgs e)
        {
            label1.Text= FileFolder.Text;
            if(FileFolder.Right <= this.ClientRectangle.Right - 30)
            {
                FileFolder.Width = label1.Width + 10;
            }
            FileFolder.Left = FileFolderLabel.Left + FileFolderLabel.Width / 2 - FileFolder.Width / 2;
            Enter.Enabled = true;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void WindowTitle_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (WindowTitle.Left <= this.ClientRectangle.Left + 30)
            {
                e.Handled = true;
            }
        }
    }
}
