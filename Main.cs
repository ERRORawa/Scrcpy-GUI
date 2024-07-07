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
using System.Xml;
using System.Reflection;

namespace Scrcpy_GUI
{
    public partial class Main : Form
    {
        SelectDevices selectDevices = new SelectDevices();    //调用选择窗体

        WirelessDebugging wirelessDebugging = new WirelessDebugging();    //调用无线调试窗体

        string[] devices = null;

        public string command = null;

        public string appPath = Directory.GetCurrentDirectory();       //设置程序位置

        string[] args = null;

        bool alwaysOnTop = false;

        bool multiTask = false;

        public void WriteFile(string fileName,string content)    //写入文件（文件名，内容）
        {
            Debug.Print("写入文件：" + fileName);
            StreamWriter sw = new StreamWriter(fileName, false, Encoding.UTF8);
            sw.WriteLine(content);
            sw.Close();
        }
 
        public string[] ReadFile(string fileName)     //读取文件（文件名）
        {
            Debug.Print("读取文件：" + fileName);
            StreamReader sr = new StreamReader(fileName,Encoding.UTF8);
            string str = string.Empty;
            string content = string.Empty;
            while ((str = sr.ReadLine()) != null)
            {
                content = content + str + "ㅤ";
            }
            sr.Close();
            return content.Split('ㅤ');
        }
 
        public string[] Cmd(string command,string fileName)    //执行命令（命令，输出文件名）   需等待程序退出
        {
            Debug.Print("执行命令：" + command);
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.StandardOutputEncoding = Encoding.UTF8;
            p.Start(); 
            p.StandardInput.WriteLine(command + " & exit");
            p.StandardInput.AutoFlush = true;
            string strOutput = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            p.Close();
            WriteFile(fileName, strOutput);
            string[] fileContent = ReadFile(fileName);
            File.Delete(appPath + "\\" + fileName);
            return fileContent;
        }

        private void SetArgs()      //设置Scrcpy参数
        {
            if (Settings.Default.用OTG && !multiTask)
            {
                MessageBox.Show("使用OTG模式时\n所有功能将无法使用\n按下Alt或切换窗口即可释放焦点", "你莫得选择！", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                command = " --otg";
            }
            else
            {
                if (Settings.Default.置顶 && !multiTask)
                {
                    command += " --always-on-top";
                    alwaysOnTop = true;
                }
                if (Settings.Default.禁用屏幕保护程序 && !multiTask)
                {
                    command += " --disable-screensaver";
                }
                if (!Settings.Default.剪切板同步)
                {
                    command += " --no-clipboard-autosync";
                }
                if (Settings.Default.禁用控制 && !multiTask)
                {
                    command += " --no-control";
                }
                if (!Settings.Default.音频流转)
                {
                    command += " --no-audio";
                }
                if (Settings.Default.结束后关闭屏幕 && !multiTask)
                {
                    command += " --power-off-on-close";
                }
                if (Settings.Default.保持唤醒 && !multiTask)
                {
                    command += " --stay-awake";
                }
            }
            if (!multiTask)
            {
                command = command + " --window-title=" + Settings.Default.窗口标题;
            }
            command = command + " --push-target=" + Settings.Default.文件存放目录;
            Debug.Print("设置参数：" + command);
        }

        public Main()
        {
            InitializeComponent();
        }

        public Main(string[] args)
        {
            InitializeComponent();
            this.args = args;
        }
        private void Main_FormClosed(object sender, FormClosedEventArgs e)    //关闭窗口
        {
            Debug.Print("关闭程序");
            Environment.Exit(0);    //退出程序
        }

        private void Main_Load(object sender, EventArgs e)
        {
            if (Path.GetFileName(Application.ExecutablePath) != "Scrcpy-GUI.exe" && Path.GetFileName(Application.ExecutablePath) != "scrcpy-gui.exe")       //修正App名字
            {
                Debug.Print("程序名错误：" + Path.GetFileName(Application.ExecutablePath) + "  正在修正");
                try
                {
                    File.Delete(appPath + "Scrcpy-GUI.exe");
                    File.Delete(appPath + "scrcpy-gui.exe");
                    File.Copy(appPath + "\\" + Path.GetFileName(Application.ExecutablePath), appPath + "\\Scrcpy-GUI.exe");
                    Process rename = new Process();
                    rename.StartInfo.FileName = appPath + "\\Scrcpy-GUI.exe";
                    rename.StartInfo.Arguments = " -D " + Path.GetFileName(Application.ExecutablePath);
                    rename.Start();
                    Debug.Print("修正完成，重启程序");
                    Environment.Exit(0);
                }
                catch
                {
                    Debug.Print("修正失败");
                }
            }
            if (!File.Exists(appPath + "\\bin\\scrcpy.exe") || !Directory.Exists(appPath + "\\MultiModeSh"))        //检查文件完整性
            {
                DialogResult dialogResult = MessageBox.Show("缺少环境配置文件，是否下载？","下载？",MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.Yes)
                {
                    if (!File.Exists(appPath + "\\bin\\scrcpy.exe"))
                    {
                        try         //下载Scrcpy
                        {
                            new WebClient().DownloadFile("https://gitdl.cn/https://github.com/Genymobile/scrcpy/releases/download/v2.4/scrcpy-win64-v2.4.zip", appPath + "\\scrcpy.zip");
                        }
                        catch
                        {
                            MessageBox.Show("Scrcpy下载失败，请检查网络状态", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Environment.Exit(1);
                        }
                        ZipFile.ExtractToDirectory(appPath + "\\scrcpy.zip", appPath);      //解压Scrcpy
                        Directory.Move(appPath + "\\scrcpy-win64-v2.4", appPath + "\\bin");
                        File.Delete(appPath + "\\scrcpy.zip");
                        Debug.Print("Scrcpy下载完成");
                    }
                    if (!Directory.Exists(appPath + "\\MultiModeSh"))
                    {
                        Directory.CreateDirectory(appPath + "\\MultiModeSh");
                        try         //下载多任务模式所需配置文件
                        {
                            new WebClient().DownloadFile("https://gitdl.cn/https://github.com/Calsign/APDE/raw/fdc22eb31048862e1484f4b6eca229accda61466/APDE/src/main/assets/aapt-binaries/aapt-arm-pie", appPath + "\\MultiModeSh\\aapt");
                            new WebClient().DownloadFile("https://gitdl.cn/https://raw.githubusercontent.com/ERRORawa/Scrcpy-GUI/main/MultiModeSh/pA.sh", appPath + "\\MultiModeSh\\pA.sh");
                            new WebClient().DownloadFile("https://gitdl.cn/https://raw.githubusercontent.com/ERRORawa/Scrcpy-GUI/main/MultiModeSh/pL.sh", appPath + "\\MultiModeSh\\pL.sh");
                            new WebClient().DownloadFile("https://gitdl.cn/https://raw.githubusercontent.com/ERRORawa/Scrcpy-GUI/main/MultiModeSh/div.sh", appPath + "\\MultiModeSh\\div.sh");
                        }
                        catch
                        {
                            MessageBox.Show("多任务模式配置下载失败，请检查网络状态", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Environment.Exit(1);
                        }
                        Debug.Print("多任务模式环境配置下载完成");
                    }
                    Process.Start(this.GetType().Assembly.Location);        //重启程序防止窗体错位
                    Environment.Exit(0);
                }
                else
                {
                    Debug.Print("已取消下载，退出程序");
                    Environment.Exit(1);
                }
            }
            Task task = Task.Run(() =>      //检查App更新
            {
                try
                {
                    new WebClient().DownloadFile("https://gitdl.cn/https://raw.githubusercontent.com/ERRORawa/Scrcpy-GUI/main/Version", appPath + "\\ver");
                    if (ReadFile("ver")[0] != Application.ProductVersion)
                    {
                        Debug.Print("发现新版本：" + ReadFile("ver")[0]);
                        DialogResult update = MessageBox.Show("检查到新版本：v" + ReadFile("ver")[0] + "\n是否立即更新？", "要更新吗？", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (update == DialogResult.Yes)
                        {
                            try         //更新程序
                            {
                                new WebClient().DownloadFile("https://gitdl.cn/https://github.com/ERRORawa/Scrcpy-GUI/releases/download/v" + ReadFile("ver")[0] + "/Scrcpy-GUI.exe", appPath + "\\updated.exe");
                                Process.Start(appPath + "\\updated.exe");
                                Debug.Print("更新完毕，重启程序");
                                Environment.Exit(1);
                            }
                            catch
                            {
                                Debug.Print("下载失败");
                                MessageBox.Show("下载失败，请检查网络状态", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    File.Delete(appPath + "\\ver");
                }
                catch
                {
                    Debug.Print("无法检查更新");
                }
            });
            if (args != null)       //获取到参数
            {
                if (args[0] == "-D")     //更新App使用的参数
                {
                    File.Delete(appPath + "\\" + args[1]);
                    Process.Start(this.GetType().Assembly.Location);
                    Environment.Exit(0);
                }
                SetArgs();      //应用Scrcpy参数
                ToolBar toolBar = new ToolBar
                {
                    alwaysOnTop = alwaysOnTop,
                    device = args[0],
                    disableToolBar = disableToolBar.Checked    //启动投屏
                };
                toolBar.Show();
                selectDevices.Cmd("bin\\scrcpy -s " + args[0] + " --shortcut-mod lctrl,rctrl" + command);
            }
            Start.Left = this.ClientRectangle.Width / 3 / 2 - Start.Width / 2;        //窗体对齐
            disableToolBar.Left = Start.Left + Start.Width / 2 - disableToolBar.Width /2;
            OTG.Left = disableToolBar.Left + disableToolBar.Width / 2 - OTG.Width / 2;
            UsbToWifi.Left = Start.Left;
            WirelessDebug.Left = UsbToWifi.Left;
            MultiTaskMode.Left = WirelessDebug.Left;
            ConnectedTitle.Left = this.ClientRectangle.Width / 3 * 2 - this.ClientRectangle.Width / 3 / 2 - ConnectedTitle.Width / 2;
            UnauthTitle.Left = this.ClientRectangle.Width - this.ClientRectangle.Width / 3 / 2 - UnauthTitle.Width / 2;
            ConnectedDevices.Left = ConnectedTitle.Left;
            UnauthDevices.Left = UnauthTitle.Left;
            Reset.Left = pictureBox1.Left - Reset.Width;

            Start.Top = this.ClientRectangle.Height / 3 / 2 - Start.Height / 2;
            disableToolBar.Top = Start.Bottom + 5;
            OTG.Top = disableToolBar.Bottom + 5;
            UsbToWifi.Top = OTG.Bottom + 20;
            WirelessDebug.Top = UsbToWifi.Bottom + 20;
            MultiTaskMode.Top = WirelessDebug.Bottom + 20;
            ConnectedTitle.Top = this.ClientRectangle.Height / 3 / 2 - ConnectedTitle.Height / 2 - 10;
            UnauthTitle.Top = this.ClientRectangle.Height / 3 / 2 - UnauthTitle.Height / 2 - 10;
            ConnectedDevices.Top = ConnectedTitle.Bottom + 5;
            UnauthDevices.Top = UnauthTitle.Bottom + 5;
            Reset.Top = MultiTaskMode.Bottom - Reset.Height;
        }

        private void CheckDevices_Tick(object sender, EventArgs e)    //检查连接的设备
        {
            CheckDevices.Interval = 2000;
            if(args != null)    //获取到参数时隐藏主菜单
            {
                this.Hide();
            }
            string[] output = Cmd("bin\\adb devices", "command");    //获取设备信息
            ConnectedDevices.Text = "";
            UnauthDevices.Text = "";
            Debug.Print("识别到的设备：");
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
                Debug.Print(output[i]);
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

        private void Reset_Click(object sender, EventArgs e)    //重置连接状态
        {
            Debug.Print("重置adb");
            UnauthTitle.Text = "";
            ConnectedDevices.Text = "";
            UnauthDevices.Text = "";
            ConnectedTitle.Text = "正在重置设备连接状态…\n重置时会无响应一小会";
            CheckDevices.Enabled = false;
            _ = Cmd("bin\\adb kill-server", "reset");
            CheckDevices.Enabled = true;
        }

        private void disableToolBar_CheckedChanged(object sender, EventArgs e)
        {
            Debug.Print("关闭工具栏：" + disableToolBar.Checked);
            Settings.Default["关闭工具栏"] = disableToolBar.Checked;     //记住设置
            if (!disableToolBar.Checked)
            {
                OTG.Checked = false;
            }
            Settings.Default.Save();
        }

        private void OTG_CheckedChanged(object sender, EventArgs e)
        {
            Debug.Print("启用OTG：" + OTG.Checked);
            Settings.Default["用OTG"] = OTG.Checked;     //记住设置
            disableToolBar.Checked = OTG.Checked;
            Settings.Default.Save();
        }

        private void OTG_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip
            {
                AutoPopDelay = 5000,
                InitialDelay = 100,
                ReshowDelay = 500,
                ShowAlways = true
            };        //显示OTG提示
            Debug.Print("显示OTG提示");
            toolTip.SetToolTip(this.OTG, "只有选择的设备处于有线连接时可用\n若是选择了无线连接的设备将会导致程序闪退");
        }

        private void Start_Click(object sender, EventArgs e)    //开始投屏
        {
            if (devices.Length - 2 > 1)    //有多个设备时
            {
                Debug.Print("有多个可用设备");
                SetArgs();
                selectDevices.devices = devices;
                selectDevices.arg = 0;
                selectDevices.command = command;
                selectDevices.Show();           //显示选择窗体
                this.Hide();
            }
            else if (devices.Length - 2 == 0)    //无设备时
            {
                MessageBox.Show("暂无已连接的设备\n请检查是否安装adb驱动或者连接到手机", "无设备连接", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else    //单设备时
            {                              //开启投屏
                SetArgs();
                ToolBar toolBar = new ToolBar
                {
                    alwaysOnTop = alwaysOnTop,
                    device = devices[0],
                    disableToolBar = disableToolBar.Checked
                };
                Debug.Print("启动投屏");
                toolBar.Show();
                selectDevices.Cmd("bin\\scrcpy -s " + devices[0] + " --shortcut-mod lctrl,rctrl" + command);       //启动Scrcpy
                this.Hide();
            }
        }

        private void UsbToWifi_Click(object sender, EventArgs e)    //usb连接转wifi连接
        {
            if (devices.Length - 2 > 1)    //多设备时
            {
                Debug.Print("有多个可用设备");
                selectDevices.devices = devices;
                selectDevices.arg = 1;
                selectDevices.Show();            //显示选择窗体
                CheckDevices.Enabled = false;
                this.Hide();
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
                    int start1 = output[6].LastIndexOf("inet ");
                    int end1 = output[6].LastIndexOf("/");
                    ip = output[6].Substring(start1 + 5, end1 - start1 - 5);
                    Debug.Print("成功获取到设备IP：" + ip);
                    flag = true;    //成功获取到ip
                }
                catch    //获取失败时
                {
                    MessageBox.Show("未找到设备的IP地址\n可能是设备没有连接WiFi", "未找到IP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                if (flag)    //若获取到ip
                {
                    _ = Cmd("bin\\adb tcpip 1324", "command");    //设备监听1324端口
                    MessageBox.Show("请拔出数据线\n若长时间未成功连接到设备，那你还是用有线连接吧。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void MultiTaskMode_Click(object sender, EventArgs e)
        {
            if (devices.Length - 2 > 1)    //有多个设备时
            {
                Debug.Print("有多个可用设备");
                SetArgs();
                selectDevices.devices = devices;
                selectDevices.arg = 2;
                selectDevices.command = command;
                selectDevices.Show();             //显示选择窗体
                this.Hide();
            }
            else if (devices.Length - 2 == 0)    //无设备时
            {
                MessageBox.Show("暂无已连接的设备\n请检查是否安装adb驱动或者连接到手机", "无设备连接", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else    //单设备时
            {
                multiTask = true;
                SetArgs();
                MultiTaskMode multiTaskMode = new MultiTaskMode
                {
                    device = devices[0],
                    command = command
                };
                multiTaskMode.Show();           //显示多任务模式设置窗体
                this.Hide();
            }
        }
        private void version_Click(object sender, EventArgs e)
        {
            Debug.Print("打开项目地址");
            Process.Start("https://github.com/ERRORawa/Scrcpy-GUI");       //打开项目地址
        }

        private void version_MouseMove(object sender, MouseEventArgs e)
        {
            version.ForeColor = Color.Black;
            version.Font = new Font("微软雅黑",10.5f, FontStyle.Bold);
        }

        private void version_MouseLeave(object sender, EventArgs e)
        {
            version.ForeColor = Color.DarkGray;
            version.Font = new Font("微软雅黑", 10.5f,FontStyle.Regular);
        }
    }
}
