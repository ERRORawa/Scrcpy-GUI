using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace scrcpy_gui
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
        public struct RECT      //定义窗口位置
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
        public void SetWindowRegion()       //开始绘制窗体
        {
            Rectangle rect = new Rectangle(0, 0, this.Width - 87, this.Height);     //绘制窗体大小
            GraphicsPath FormPath = GetRoundedRectPath(rect, 30);       //设置圆角
            this.Region = new Region(FormPath);
        }

        SelectDevices selectDevices = new SelectDevices();      //调用SelectDevices窗体

        public string device;       //获取当前设备

        bool flag = false;      //判断是否显示了Scrcpy窗口的flag

        bool flag1 = false;     //执行一次异步，防止多次执行

        string formTitle = Settings.Default.窗口标题;

        IntPtr hWnd = FindWindow(null, Settings.Default.窗口标题);       //获取Scrcpy的句柄

        Point mousePoint = Control.MousePosition;       //获取鼠标的位置


        public ToolBar()
        {
            InitializeComponent();
        }

        private void FollowScrcpy_Tick(object sender, EventArgs e)        //设置跟随Scrcpy窗体
        {
            hWnd = FindWindow(null, formTitle);      //获取Scrcpy句柄
            RECT fx = new RECT();       //定义窗口位置
            GetWindowRect(hWnd, ref fx);        //获取窗口位置
            IntPtr foregroundForm = GetForegroundWindow();      //获取活动窗口句柄
            StringBuilder windowName = new StringBuilder(512);      //用来存放活动窗口标题的
            GetWindowText(foregroundForm, windowName, windowName.Capacity);     //获取活动窗口标题
            if (windowName.ToString() == "更多" || windowName.ToString() == "成功")       //如果当前活动窗口是 More ，禁止设置ToolBar为活动窗口
            {
                TopForm.Enabled = false;
            }
            else
            {
                TopForm.Enabled = true;
            }
            if (fx.Left == 0 && fx.Top == 0 && flag && windowName.ToString() != "更多")        //如果成功获取过Scrcpy的位置并且Scrcpy已关闭
            {
                Console.WriteLine("exit");
                Environment.Exit(0);        //退出程序
            }
            else if (fx.Left == -8 && fx.Top == -8)         //如果Scrcpy全屏了
            {
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
                            FollowScrcpy.Enabled = false;
                            CheckMouse.Enabled = false;
                            MessageBox.Show("长时间未投屏成功\n判断为手机已断开连接\n请重试", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            selectDevices.Cmd(this.GetType().Assembly.Location);        //再启动一个该程序
                            await Task.Delay(500);      //等待0.5秒
                            Environment.Exit(0);        //退出程序
                        }
                    });
                }
            }
            else
            {
                flag = true;        //设为已获取到Scrcpy
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
            IntPtr foregroundForm = GetForegroundWindow();      //获取活动窗口句柄
            StringBuilder windowName = new StringBuilder(512);      //用来存放活动窗口标题的
            GetWindowText(foregroundForm, windowName, windowName.Capacity);     //获取活动窗口标题
            if (windowName.ToString() == formTitle)        //判断窗口标题是ToolBar或者Scrcpy
            {
                this.TopMost = true;        //ToolBar窗口置顶
            }
            else
            {
                this.TopMost = false;       //取消ToolBar的置顶
            }
        }

        private void Power_Click(object sender, EventArgs e)        //点击电源键
        {
            SetForegroundWindow(hWnd);
            selectDevices.Cmd("adb -s " + device + " shell input keyevent 26");
        }

        private void VolumeUp_Click(object sender, EventArgs e)     //点击音量加
        {
            SetForegroundWindow(hWnd);
            selectDevices.Cmd("adb -s " + device + " shell input keyevent 24");
        }

        private void VolumeDown_Click(object sender, EventArgs e)       //点击音量键
        {
            SetForegroundWindow(hWnd);
            selectDevices.Cmd("adb -s " + device + " shell input keyevent 25");
        }

        private void Screenshot_Click(object sender, EventArgs e)       //点击截图键
        {
            SetForegroundWindow(hWnd);
            string time = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();      //设置文件名
            selectDevices.Cmd("adb -s " + device + " shell screencap /sdcard/" + time + ".png");
            var task = Task.Run(async delegate
            {
                await Task.Delay(1000);     //判断有没有把截图丢到桌面上
                while (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\" + time + ".png"))
                {
                    selectDevices.Cmd("adb -s " + device + " pull /sdcard/" + time + ".png %USERPROFILE%\\Desktop\\" + time + ".png");
                    await Task.Delay(1000);
                }
                selectDevices.Cmd("adb -s " + device + " shell rm -rf /sdcard/" + time + ".png");
                MessageBox.Show("成功将截图保存到桌面","截图成功",MessageBoxButtons.OK);
            });

        }

        private void Back_Click(object sender, EventArgs e)     //点击返回键
        {
            SetForegroundWindow(hWnd);
            selectDevices.Cmd("adb -s " + device + " shell input keyevent 4");
        }

        private void Home_Click(object sender, EventArgs e)      //点击主页键
        {
            SetForegroundWindow(hWnd);
            selectDevices.Cmd("adb -s " + device + " shell input keyevent 3");
        }

        private void MuliTask_Click(object sender, EventArgs e)     //点击多任务视图键
        {
            SetForegroundWindow(hWnd);
            selectDevices.Cmd("adb -s " + device + " shell input keyevent 187");
        }

        private void More_Click(object sender, EventArgs e)         //点击更多键
        {
            IntPtr hasMore = FindWindow(null, "更多");      //获取More句柄
            if (hasMore.ToString() == "0")
            {
                More more = new More();
                more.Show();        //显示More
            }
            else
            {
                SetForegroundWindow(hasMore);
            }
        }

        private void ToolBar_Click(object sender, EventArgs e)      //点击ToolBar窗体
        {
            SetForegroundWindow(hWnd);      //设置Scrcpy为活动窗口
        }
    }
}
    