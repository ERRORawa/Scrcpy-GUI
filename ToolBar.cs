﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scrcpy_GUI
{
    public partial class ToolBar : Form
    {
        [DllImport("user32.dll",EntryPoint = "FindWindow")]             //调用DLL —— 查找窗口 句柄
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);   //查找窗口 句柄（Class,窗体标题）

        [DllImport("user32.dll")]       //调用DLL
        static extern IntPtr GetForegroundWindow();     //获取当前活动窗口 句柄

        [DllImport("user32.dll")]       //调用DLL
        static extern void GetWindowText(IntPtr hwnd, StringBuilder lpString, int cch);     //获取句柄标题（句柄，存标题用的变量，存标题用的变量.Capacity）

        [DllImport("user32.dll")]       //调用DLL
        static extern void SetForegroundWindow(IntPtr hWnd);      //给句柄设为活动窗口（句柄）

        [DllImport("user32.dll")]       //调用DLL
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);     //获取句柄窗口位置（句柄，存窗口位置的变量）

        [StructLayout(LayoutKind.Sequential)]
        struct RECT      //定义窗口位置
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
            public bool Focused;
        }
        protected override void OnResize(EventArgs e)       //重新绘制ToolBar窗体
        {
            this.Region = null;
            SetWindowRegion();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rect">窗体大小</param>
        /// <param name="radius">圆角大小</param>
        /// <returns></returns>
        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)     //设置窗体圆角（窗体大小，圆角大小）
        {
            Rectangle arcRect = new Rectangle(rect.Location, new Size(radius, radius));     //获取窗体的大小和位置
            GraphicsPath path = new GraphicsPath();
            path.AddArc(arcRect, 180, 90);              //设置四个角落的圆角程度
            arcRect.X = rect.Right - radius;
            path.AddArc(arcRect, 270, 90);
            arcRect.Y = rect.Bottom - radius;
            path.AddArc(arcRect, 0, 90);
            arcRect.X = rect.Left;
            path.AddArc(arcRect, 90, 90);
            path.CloseFigure(); 
            return path;
        }
        private void SetWindowRegion()       //开始绘制窗体
        {
            Rectangle rect = new Rectangle(0, 0, this.Width - 87, this.Height);     //绘制窗体大小
            GraphicsPath FormPath = GetRoundedRectPath(rect, 30);       //设置圆角
            this.Region = new Region(FormPath);
        }

        SelectDevices selectDevices = new SelectDevices();      //调用SelectDevices窗体

        public string device;       //获取当前设备

        bool flag = false;      //判断是否显示了Scrcpy窗口的flag

        bool flag1 = false;     //执行一次异步，防止多次执行

        bool isTop = false;     //判断是否置顶

        bool isFocus = true;    //判断活动窗口是否为Scrcpy

        public bool alwaysOnTop = false;    //判断是否置顶

        public bool disableToolBar = false;     //判断工具栏是否开启

        public bool multiEnabled = false;   //判断是否多任务模式

        public string[] formNames = null;     //多任务模式所有窗口名

        string formTitle = Settings.Default.窗口标题;       //获取Scrcpy的标题

        string lastFormName = null;     //最后活动的Scrcpy窗口名

        IntPtr hWnd = FindWindow(null, Settings.Default.窗口标题);       //获取Scrcpy的句柄

        StringBuilder windowName = new StringBuilder(512);      //定义活动窗体名称

        Point mousePoint = Control.MousePosition;       //获取鼠标的位置

        public ToolBar()
        {
            InitializeComponent();
        }

        private void ToolBar_Load(object sender, EventArgs e)
        {
            if(multiEnabled)
            {
                lastFormName = formNames[0];    //初始化多任务模式窗口名
            }
            if (alwaysOnTop)
            {
                this.TopMost = true;        //设置置顶
            }
            if (Settings.Default.始终显示工具栏)
            {
                CheckMouse.Enabled = false;     //始终显示
            }
        }

        private void FollowScrcpy_Tick(object sender, EventArgs e)        //设置跟随Scrcpy窗体
        {
            if (multiEnabled)
            {
                hWnd = FindWindow(null, lastFormName);      //获取Scrcpy句柄
            }
            else
            {
                hWnd = FindWindow(null, formTitle);      //获取Scrcpy句柄
            }
            RECT fx = new RECT();       //定义窗口位置
            GetWindowRect(hWnd, ref fx);        //获取窗口位置
            if (fx.Left == 0 && fx.Top == 0 && flag && windowName.ToString() != "更多")        //如果成功获取过Scrcpy的位置并且Scrcpy已关闭
            {
                Main main = new Main()     //拉起Main（只用作执行cmd命令）
                {
                    notAtMain = true
                };
                if (multiEnabled)       //多任务模式
                {
                    bool exitFlag = true;
                    for (int i = 0; i < formNames.Length; i++)      //判断窗口是否全部关闭
                    {
                        GetWindowRect(FindWindow(null, formNames[i]), ref fx);        //获取窗口位置
                        if (fx.Left != 0 || fx.Top != 0)
                        {
                            exitFlag = false;
                        }
                    }
                    if(exitFlag)        //删除虚拟显示屏
                    {
                        _ = main.Cmd("bin\\adb -s " + device + " shell settings delete global overlay_display_devices", "deleteDisplay");
                    }
                    else
                    {
                        return;
                    }
                }
                _ = main.Cmd("taskkill /F /fi \"windowtitle eq ScrcpyScreenOff\"", "quit");     //取消息屏
                Environment.Exit(0);        //退出程序
            }
            else if (fx.Left == -8 && fx.Top == -8)         //如果Scrcpy全屏了
            {
                if (alwaysOnTop)        //判断始终置顶
                {
                    return;
                }
                if (windowName.ToString() == formTitle)       //判断活动窗体是否Scrcpy
                {
                    if (!isTop)     //置顶工具栏
                    {
                        isTop = true;
                        this.TopMost = true;
                    }
                }
                else
                {
                    if(isTop)       //取消置顶
                    {
                        isTop = false;
                        this.TopMost = false;
                    }
                }
                this.Left = fx.Left + 20;       //ToolBar的位置设为屏幕左侧
                this.Top = (fx.Bottom - fx.Top) / 2 + fx.Top - this.Height / 2;
            }
            else if (fx.Left == 0 && fx.Top == 0 && !flag)      //如果还没获取到Scrcpy的位置
            {
                this.Left = 0 - this.Width;           //ToolBar的位置设为屏幕外
                if (!flag1)         //如果没执行过Task
                {
                    flag1 = true;       //设为Task已执行
                    _ = Task.Run(async delegate     //执行Task异步
                    {
                        await Task.Delay(8000);     //等待8秒
                        if (!flag)      //若还未获取到Scrcpy，当作投屏失败
                        {
                            MessageBox.Show("长时间未投屏成功\n判断为手机已断开连接\n请重试", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Process.Start(this.GetType().Assembly.Location);       //再启动一个该程序
                            await Task.Delay(500);      //等待0.5秒
                            Environment.Exit(0);        //退出程序
                        }
                    });
                }
            }
            else                //跟随Scrcpy
            {
                if (disableToolBar)         //若设置了不开启工具栏 则退出程序
                {
                    Environment.Exit(0);
                }
                flag = true;        //设为已获取到Scrcpy
                if (isTop && !alwaysOnTop)      //从全屏还原时取消置顶
                {
                    isTop = false;
                    this.TopMost = false;
                }
                this.Left = fx.Left - this.Width + 87;          //跟随Scrcpy
                this.Top = (fx.Bottom - fx.Top) / 2 + fx.Top - this.Height / 2;
            }
        }
        private void CheckMouse_Tick(object sender, EventArgs e)        //检测鼠标
        {
            if (mousePoint == Control.MousePosition)     //如果鼠标没有移动
            {
                FollowScrcpy.Enabled = false;       //隐藏ToolBar
                this.Left = 0 - this.Width;
                CheckMouse.Interval = 100;
            }
            else     //如果鼠标在动
            {
                FollowScrcpy.Enabled = true;        //显示ToolBar
                CheckMouse.Interval = 3000;
                mousePoint = Control.MousePosition;
            }

        }

        private void TopForm_Tick(object sender, EventArgs e)
        {
            if (alwaysOnTop)    //判断始终置顶
            {
                TopForm.Enabled = false;
            }
            IntPtr foregroundForm = GetForegroundWindow();      //获取活动窗口句柄
            GetWindowText(foregroundForm, windowName, windowName.Capacity);     //获取活动窗口标题
            if (multiEnabled)       //多任务模式
            {
                if (windowName.ToString() == formNames[0] || windowName.ToString() == formNames[1] || windowName.ToString() == formNames[2] || windowName.ToString() == formNames[3] || windowName.ToString() == "ToolBar")        //判断窗口标题是ToolBar或者Scrcpy
                {
                    if (!isFocus)       //显示工具栏
                    {
                        isFocus = true;
                        IntPtr toolBarhWnd = FindWindow(null, "ToolBar");       //获取Scrcpy的句柄
                        SetForegroundWindow(toolBarhWnd);       //显示工具栏
                        SetForegroundWindow(hWnd);
                        lastFormName = windowName.ToString();       //设置最后活动的Scrcpy窗口名
                    }
                }
                else
                {
                    if (isFocus)        //重置判断
                    {
                        isFocus = false;
                    }
                }
            }
            else
            {
                if (windowName.ToString() == formTitle || windowName.ToString() == "ToolBar")        //判断窗口标题是ToolBar或者Scrcpy
                {
                    if (!isFocus)       //显示工具栏
                    {
                        isFocus = true;
                        IntPtr toolBarhWnd = FindWindow(null, "ToolBar");       //获取Scrcpy的句柄
                        SetForegroundWindow(toolBarhWnd);       //显示工具栏
                        SetForegroundWindow(hWnd);
                    }
                }
                else
                {
                    if (isFocus)        //重置判断
                    {
                        isFocus = false;
                    }
                }
            }
        }

        private void Power_Click(object sender, EventArgs e)        //点击电源键
        {
            SetForegroundWindow(hWnd);
            selectDevices.Cmd("bin\\adb -s " + device + " shell input keyevent 26");
        }

        private void VolumeUp_Click(object sender, EventArgs e)     //点击音量加
        {
            SetForegroundWindow(hWnd);
            selectDevices.Cmd("bin\\adb -s " + device + " shell input keyevent 24");
        }

        private void VolumeDown_Click(object sender, EventArgs e)       //点击音量键
        {
            SetForegroundWindow(hWnd);
            selectDevices.Cmd("bin\\adb -s " + device + " shell input keyevent 25");
        }

        private void Screenshot_Click(object sender, EventArgs e)       //点击截图键
        {
            SetForegroundWindow(hWnd);
            string time = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();      //设置文件名
            selectDevices.Cmd("bin\\adb -s " + device + " shell screencap /sdcard/" + time + ".png");
            var task = Task.Run(async delegate
            {
                await Task.Delay(1000);     //判断有没有把截图丢到桌面上
                while (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\" + time + ".png"))
                {       //复制截图到电脑桌面
                    selectDevices.Cmd("bin\\adb -s " + device + " pull /sdcard/" + time + ".png " + Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\" + time + ".png");
                    await Task.Delay(100);
                }
                selectDevices.Cmd("bin\\adb -s " + device + " shell rm -rf /sdcard/" + time + ".png");
                MessageBox.Show("成功将截图保存到桌面","截图成功",MessageBoxButtons.OK);
            });

        }

        private void Back_Click(object sender, EventArgs e)     //点击返回键
        {
            SetForegroundWindow(hWnd);
            selectDevices.Cmd("bin\\adb -s " + device + " shell input keyevent 4");
        }

        private void Home_Click(object sender, EventArgs e)      //点击主页键
        {
            SetForegroundWindow(hWnd);
            selectDevices.Cmd("bin\\adb -s " + device + " shell input keyevent 3");
        }

        private void MuliTask_Click(object sender, EventArgs e)     //点击多任务视图键
        {
            SetForegroundWindow(hWnd);
            selectDevices.Cmd("bin\\adb -s " + device + " shell input keyevent 187");
        }

        private void More_Click(object sender, EventArgs e)         //点击更多键
        {
            IntPtr hasMore = FindWindow(null, "更多");      //获取More句柄
            if (hasMore.ToString() == "0")
            {
                More more = new More
                {
                    device = device
                };
                more.Show();        //显示More
            }
            else
            {
                SetForegroundWindow(hasMore);        //显示More
            }
        }

        private void ToolBar_Click(object sender, EventArgs e)      //点击ToolBar窗体
        {
            SetForegroundWindow(hWnd);      //设置Scrcpy为活动窗口
        }

        private void ScreenOn_CheckedChanged(object sender, EventArgs e)        //手机息屏
        {
            SetForegroundWindow(hWnd);
            IntPtr SO = FindWindow(null, "ScrcpyScreenOff");
            if (SO.ToString() == "0")     //创建新Scrcpy窗口
            {
                Process p = new Process();
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                p.StartInfo.FileName = "bin\\scrcpy.exe";
                p.StartInfo.Arguments = "-s " + device + " -S --window-title=ScrcpyScreenOff";  //添加息屏参数
                p.Start();
            }
            CheckScreenOn.Enabled = true;   //定时判断息屏
            if (ScreenOn.Checked)   //切换工具栏图标
            {
                ScreenOn.BackgroundImage = Resource.ScreenOn;
            }
            else
            {
                ScreenOn.BackgroundImage = Resource.ScreenOff;
            }
        }

        private void CheckScreenOn_Tick(object sender, EventArgs e)     //判断息屏
        {
            IntPtr SO = FindWindow(null, "ScrcpyScreenOff");
            if (SO.ToString() != "0")   //成功
            {
                if (ScreenOn.Checked)   //取消息屏
                {
                    selectDevices.Cmd("taskkill /F /fi \"windowtitle eq ScrcpyScreenOff\"");    //退出息屏Scrcpy
                    SetForegroundWindow(hWnd);
                }
                CheckScreenOn.Enabled = false;
            }
        }
    }
}
    