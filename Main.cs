using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Compression;

namespace scrcpy_gui
{
    public partial class Main : Form
    {
        SelectDevices selectDevices = new SelectDevices();    //调用选择窗体

        WirelessDebugging wirelessDebugging = new WirelessDebugging();    //调用无线调试窗体

        string[] devices = null;    //初始化设备列表

        public string command = null;

        string appPath = Directory.GetCurrentDirectory();

        public void WriteFile(string fileName,string content)    //写入文件（文件名，内容）
        {
            StreamWriter sw = new StreamWriter(fileName, false, Encoding.GetEncoding("GB2312"));
            sw.WriteLine(content);
            sw.Close();
        }
 
        public string[] ReadFile(string fileName)     //读取文件（文件名）
        {
            StreamReader sr = new StreamReader(fileName,Encoding.GetEncoding("GB2312"));
            string str = string.Empty;
            string content = string.Empty;
            while ((str = sr.ReadLine()) != null)
            {
                content = content + str + "·";
            }
            sr.Close();
            return content.Split('·');
        }
 
        public string[] Cmd(string command,string fileName)    //执行命令（命令，输出文件名）   需等待程序退出
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
            p.StandardInput.AutoFlush = true;
            string strOutput = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            p.Close();

            WriteFile(fileName, strOutput);

            return ReadFile(fileName);
        }

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)    //窗体控件对齐，初始化设置项
        {
            disableToolBar.Checked = Settings.Default.关闭工具栏;
            if (!File.Exists(appPath + "\\bin\\scrcpy.exe"))
            {
                DialogResult dialogResult = MessageBox.Show("首次启动需要下载环境配置，是否继续？","下载？",MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        WebClient webClient = new WebClient();
                        webClient.DownloadFile("https://gitdl.cn/https://github.com/Genymobile/scrcpy/releases/download/v2.4/scrcpy-win64-v2.4.zip", appPath + "\\scrcpy.zip");
                    }
                    catch
                    {
                        MessageBox.Show("环境配置下载失败，请检查网络状态", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Environment.Exit(1);
                    }
                    ZipFile.ExtractToDirectory(appPath + "\\scrcpy.zip", appPath);
                    Directory.Move(appPath + "\\scrcpy-win64-v2.4", appPath + "\\bin");
                    File.Delete(appPath + "scrcpy.zip");
                    Process.Start(this.GetType().Assembly.Location);
                    Environment.Exit(0);
                }
                else
                {
                    Environment.Exit(1);
                }
            }
            Start.Left = this.ClientRectangle.Width / 3 / 2 - Start.Width / 2;
            disableToolBar.Left = Start.Left + Start.Width / 2 - disableToolBar.Width /2;
            UsbToWifi.Left = this.ClientRectangle.Width / 3 / 2 - UsbToWifi.Width / 2;
            WirelessDebug.Left = this.ClientRectangle.Width / 3 / 2 - WirelessDebug.Width / 2;
            ConnectedTitle.Left = this.ClientRectangle.Width / 3 * 2 - this.ClientRectangle.Width / 3 / 2 - ConnectedTitle.Width / 2;
            UnauthTitle.Left = this.ClientRectangle.Width - this.ClientRectangle.Width / 3 / 2 - UnauthTitle.Width / 2;
            ConnectedDevices.Left = ConnectedTitle.Left;
            UnauthDevices.Left = UnauthTitle.Left;
            Reset.Left = pictureBox1.Left - Reset.Width;

            Start.Top = this.ClientRectangle.Height / 3 / 2 - Start.Height / 2;
            disableToolBar.Top = Start.Bottom + 5;
            UsbToWifi.Top = this.ClientRectangle.Height / 3 * 2 - this.ClientRectangle.Height / 3 / 2 - UsbToWifi.Height / 2;
            WirelessDebug.Top = this.ClientRectangle.Height - this.ClientRectangle.Height / 3 / 2 - WirelessDebug.Height / 2;
            ConnectedTitle.Top = this.ClientRectangle.Height / 3 / 2 - ConnectedTitle.Height / 2 - 10;
            UnauthTitle.Top = this.ClientRectangle.Height / 3 / 2 - UnauthTitle.Height / 2 - 10;
            ConnectedDevices.Top = ConnectedTitle.Bottom + 5;
            UnauthDevices.Top = UnauthTitle.Bottom + 5;
            Reset.Top = WirelessDebug.Bottom - Reset.Height;


            if (Settings.Default.用OTG)
            {
                MessageBox.Show("当前配置正在使用OTG模式\n所有功能将无法使用\nOTG模式下可以使用Alt跳出设备", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                command = " --otg";
            }
            if (Settings.Default.置顶)
            {
                command = command + " --always-on-top";
            }
            if (Settings.Default.禁用屏幕保护程序)
            {
                command = command + " --disable-screensaver";
            }
            if (!Settings.Default.剪切板同步)
            {
                command = command + " --no-clipboard-autosync";
            }
            if (Settings.Default.禁用控制)
            {
                command = command + " --no-control";
            }
            if (!Settings.Default.音频流转)
            {
                command = command + " --no-audio";
            }
            if (Settings.Default.结束后关闭屏幕)
            {
                command = command + " --power-off-on-close";
            }
            if (Settings.Default.保持唤醒)
            {
                command = command + " --stay-awake";
            }
            if (Settings.Default.窗口标题 != "")
            {
                command = command + " --window-title=" + Settings.Default.窗口标题;
            }
            if (Settings.Default.文件存放目录 != "")
            {
                command = command + " --push-target=" + Settings.Default.文件存放目录;
            }
        }

        private void CheckDevices_Tick(object sender, EventArgs e)    //定时检查devices
        {
            CheckDevices.Interval = 2000;
            string[] output = Cmd("bin\\adb devices", "command");    //获取devices信息
            ConnectedDevices.Text = "";
            UnauthDevices.Text = "";
            for (int i = 5; ; i++)
            {
                if (output[i] == "")     //无设备
                {
                    break;
                }
                else if (output[i].Remove(0, output[i].Length - 6) == "device")    //可用设备
                {
                    ConnectedDevices.Text = ConnectedDevices.Text + output[i].Substring(0, output[i].Length - 6) + Environment.NewLine;
                }
                else if (output[i].Remove(0, output[i].Length - 12) == "unauthorized")    //未授权设备
                {
                    UnauthDevices.Text = UnauthDevices.Text + output[i].Substring(0, output[i].Length - 13) + Environment.NewLine;
                }
            }
            WriteFile("devices", ConnectedDevices.Text);    //写入文件
            devices = ReadFile("devices");    //获取devices
            if (ConnectedDevices.Text == "")
            {
                ConnectedDevices.Text = "无设备";
            }
            if (UnauthDevices.Text == "")
            {
                UnauthDevices.Text = "无设备";
            }
            ConnectedTitle.Text = "已连接的设备：";
            UnauthTitle.Text = "未授权的设备：";
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)    //关闭窗口
        {
            selectDevices.Close();
            wirelessDebugging.Close();
            Environment.Exit(0);    //退出程序
        }

        private void Reset_Click(object sender, EventArgs e)    //重置连接状态
        {
            UnauthTitle.Text = "";
            ConnectedDevices.Text = "";
            UnauthDevices.Text = "";
            ConnectedTitle.Text = "正在重置设备连接状态…\n重置时会无响应一小会";
            CheckDevices.Enabled = false;
            _ = Cmd("bin\\adb kill-server", "reset");
            CheckDevices.Enabled = true;
        }

        private void Start_Click(object sender, EventArgs e)    //开始投屏
        {
            if (devices.Length - 2 > 1)    //有多个设备时
            {
                selectDevices.devices = devices;    //给予设备列表
                selectDevices.arg = 0;    //选项为投屏
                selectDevices.Show();    //显示选择窗口
                this.Hide();    //隐藏当前窗口
            }
            else if (devices.Length - 2 == 0)    //无设备时
            {
                MessageBox.Show("暂无已连接的设备\n请检查是否安装adb驱动或者连接到手机", "无设备连接", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else    //单设备时
            {                              //开启投屏
                ToolBar toolBar = new ToolBar();
                toolBar.device = devices[0];
                toolBar.disableToolBar = disableToolBar.Checked;
                toolBar.Show();
                selectDevices.Cmd("bin\\scrcpy -s " + devices[0] + " --shortcut-mod lctrl,rctrl" + command);
                this.Hide();    //隐藏当前窗体
            }
        }

        private void UsbToWifi_Click(object sender, EventArgs e)    //usb连接转wifi连接
        {
            if (devices.Length - 2 > 1)    //多设备时
            {
                selectDevices.devices = devices;    //给予设备列表
                selectDevices.arg = 1;    //选项为usb转wifi
                selectDevices.Show();    //显示选择窗体
                CheckDevices.Enabled = false;
                this.Hide();    //隐藏当前窗体
            }
            else if (devices.Length - 2 == 0)    //无设备时
            {
                MessageBox.Show("暂无已连接的设备\n请检查是否安装adb驱动或者连接到手机", "无设备连接", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else    //单设备时
            {
                bool flag = false;
                string ip = "127.0.0.1";    //初始化ip
                try
                {
                    string[] output = Cmd("bin\\adb shell ip addr show wlan0", "ip");    //获取设备ip信息
                    int start1 = output[6].LastIndexOf("inet ");    //获取inet位置
                    int end1 = output[6].LastIndexOf("/");    //获取/位置
                    ip = output[6].Substring(start1 + 5, end1 - start1 - 5);    //获取两个位置间的ip地址
                    flag = true;    //成功获取到ip
                }
                catch    //获取失败时
                {
                    MessageBox.Show("未找到设备的IP地址\n可能是设备没有连接WiFi", "未找到IP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                if (flag)    //获取到ip时
                {
                    _ = Cmd("bin\\adb tcpip 1324", "command");    //设备监听1324端口
                    MessageBox.Show("请拔出数据线\n若长时间未成功连接到设备，请不要使用无线调试", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    selectDevices.Cmd("bin\\adb connect " + ip + ":" + "1324");    //连接设备的1324端口
                }
            }
        }

        private void WirelessDebug_Click(object sender, EventArgs e)    //跳转无线调试窗体
        {
            CheckDevices.Enabled = false;
            wirelessDebugging.Show();
            this.Hide();
        }

        private void disableToolBar_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default["关闭工具栏"] = disableToolBar.Checked;
            Settings.Default.Save();
        }
    }
}
