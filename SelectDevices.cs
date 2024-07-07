using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Scrcpy_GUI
{
    public partial class SelectDevices : Form
    {
        public int arg;

        public string[] devices;

        public string command;

        public bool alwaysOnTop = false;
        public void Cmd(string command)    //执行命令（异步），无输出
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.StandardInput.WriteLine(command + " & exit");
        }
 
        public SelectDevices()
        {
            InitializeComponent();
        }

        private void SelectDevices_Load(object sender, EventArgs e)    //初始化内容，窗体控件对齐
        {
            for(int i = 0; i < devices.Length-2; i++)    //添加devices信息到listBox
            {
                SelectDevice.Items.Add(this.devices[i]);
            }
            SelectDevice.Top = this.ClientRectangle.Height / 2 - SelectDevice.Height / 2;    //窗体内容对齐
            SelectDevice.Left = this.ClientRectangle.Width / 2 - SelectDevice.Width / 2 - 60;
            Cancel.Top = SelectDevice.Bottom + (this.ClientRectangle.Height - SelectDevice.Bottom) / 2 - Cancel.Height / 2;
            Cancel.Left = this.ClientRectangle.Width / 2 - Cancel.Width / 2;
            SelectDeviceTitle.Top = SelectDevice.Top - SelectDevice.Top / 2 - SelectDeviceTitle.Height / 2;
            SelectDeviceTitle.Left = this.ClientRectangle.Width / 2 - SelectDeviceTitle.Width / 2;
        }

        private void SelectDevice_DrawItem(object sender, DrawItemEventArgs e)    //重新绘制listBox（设置居中）
        {
            e.DrawBackground();
            e.DrawFocusRectangle();
            StringFormat strFmt = new StringFormat();
            strFmt.Alignment = StringAlignment.Center;
            strFmt.LineAlignment = StringAlignment.Center;
            e.Graphics.DrawString(SelectDevice.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds, strFmt);
        }

        private void Cancel_Click(object sender, EventArgs e)    //取消按钮
        {
            this.Hide();
            Main main = new Main();
            main.Show();
        }

        private void SelectDevice_MouseDoubleClick(object sender, MouseEventArgs e)    //双击listBox内容
        {
            switch (arg) 
            {
                case 0:    //投屏
                    ToolBar toolBar = new ToolBar();
                    toolBar.alwaysOnTop = alwaysOnTop;
                    toolBar.device = SelectDevice.SelectedItem.ToString();
                    toolBar.Show();
                    Cmd("bin\\scrcpy -s " + SelectDevice.SelectedItem.ToString() + " --shortcut-mod lctrl,rctrl" + command);
                    this.Hide();
                    break;
                case 1:    //usb转wifi
                    Main main = new Main();
                    bool flag = false;
                    string ip = "127.0.0.1";    //初始化ip
                    try
                    {
                        string[] output = main.Cmd("bin\\adb -s " + SelectDevice.SelectedItem.ToString() + " shell ip addr show wlan0", "ip");    //获取设备ip信息
                        int start1 = output[6].LastIndexOf("inet ");    //获取inet位置
                        int end1 = output[6].LastIndexOf("/");    //获取/位置
                        ip = output[6].Substring(start1 + 5, end1 - start1 - 5);    //获取两个位置间的ip地址
                        flag = true;   //成功获取到ip
                    }
                    catch    //获取失败时
                    {
                        MessageBox.Show("未找到设备的IP地址\n可能是设备没有连接WiFi", "未找到IP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    if (flag)    //获取到ip时
                    {
                        _ = main.Cmd("bin\\adb -s " + SelectDevice.SelectedItem.ToString() + " tcpip 1324", "tcpip");    //设备监听1324端口
                        MessageBox.Show("请拔出数据线\n若长时间未成功连接到设备，那你还是用有线连接吧。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Cmd("bin\\adb connect " + ip + ":" + "1324");    //连接设备的1324端口
                        main.Show();
                        this.Hide();
                    }
                    break;
                case 2:
                    MultiTaskMode multiTaskMode = new MultiTaskMode();
                    multiTaskMode.device = SelectDevice.SelectedItem.ToString();
                    multiTaskMode.command = command;
                    multiTaskMode.Show();
                    this.Hide();    //隐藏当前窗体
                    break;
            }
        }
    }
}
