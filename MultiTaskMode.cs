using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scrcpy_GUI
{
    public partial class MultiTaskMode : Form
    {
        [DllImport("user32.dll", EntryPoint = "FindWindow")]             //调用DLL —— 查找窗口 句柄
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);   //查找窗口 句柄（Class,窗体标题）

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

        Main main = new Main
        {
            notAtMain = true
        };

        public string device;

        public string command;

        ArrayList packActivityAr = new ArrayList();

        string[] appLabel;

        int[] id;

        int idx;

        int wait = 0;

        string nowApp;

        bool flag = true;

        bool success = false;

        Preview1 preview1 = new Preview1();

        Preview2 preview2 = new Preview2();

        Preview3 preview3 = new Preview3();

        Preview4 preview4 = new Preview4();

        string[] packActivity;
        public MultiTaskMode()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }
        public void Cmd(string command)    //执行命令（异步），无输出
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\n[执行命令]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(command);
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
        }
        private void Enter_Click(object sender, EventArgs e)
        {
            if (app1.Items[0].ToString() == "正在获取应用列表")
            {
                return;
            }
            this.Hide();
            string displayInfo = "";
            bool flag = false;
            int num = 0;
            if (checkBox1.Checked)
            {
                displayInfo += res1l.Text + "x" + res1r.Text + "/" + dpi1.Text;
                flag = true;
                num++;
            }
            if (checkBox2.Checked)
            {
                if (flag)
                {
                    displayInfo += "\\;";
                }
                displayInfo += res2l.Text + "x" + res2r.Text + "/" + dpi2.Text;
                flag = true;
                num++;
            }
            if (checkBox3.Checked)
            {
                if (flag)
                {
                    displayInfo += "\\;";
                }
                displayInfo += res3l.Text + "x" + res3r.Text + "/" + dpi3.Text;
                flag = true;
                num++;
            }
            if (checkBox4.Checked)
            {
                if (flag)
                {
                    displayInfo += "\\;";
                }
                displayInfo += res4l.Text + "x" + res4r.Text + "/" + dpi4.Text;
                num++;
            }
            _ = main.Cmd("bin\\adb -s " + device + " shell settings put global overlay_display_devices \"" + displayInfo + "\"", "createDisplay");
            Thread.Sleep(200);
            string[] displayID = main.Cmd("bin\\adb -s " + device + " shell dumpsys display ^| grep \"\\ \\ \\ \\ mDisplayId=\"", "displayID");
            ArrayList idAr = new ArrayList();
            for (int k = 4; ; k++)
            {
                if (displayID[k] == "")
                {
                    break;
                }
                else
                {
                    idAr.Add(int.Parse(displayID[k].Substring(15, displayID[k].Length - 15)));
                }
            }
            id = (int[])idAr.ToArray(typeof(int));
            idx = id.Length - num;
            int[] ida = new int[4];
            int i = 0;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\n[虚拟显示屏ID]");
            Console.ForegroundColor = ConsoleColor.White;
            if (checkBox1.Checked)
            {
                ida[0] = id[idx];
                Console.Write(" " + id[idx + i].ToString());
                i++;
            }
            if (checkBox2.Checked)
            {
                ida[1] = id[idx + i];
                Console.Write(" " + id[idx + i].ToString());
                i++;
            }
            if (checkBox3.Checked)
            {
                ida[2] = id[idx + i];
                Console.Write(" " + id[idx + i].ToString());
                i++;
            }
            if (checkBox4.Checked)
            {
                ida[3] = id[idx + i];
                Console.Write(" " + id[idx + i].ToString());
            }
            id = ida;
            check.Enabled = true;
            Settings.Default.多开1[0] = checkBox1.Checked.ToString();
            Settings.Default.多开2[0] = checkBox2.Checked.ToString();
            Settings.Default.多开3[0] = checkBox3.Checked.ToString();
            Settings.Default.多开4[0] = checkBox4.Checked.ToString();
            Settings.Default.多开1[1] = res1l.Text;
            Settings.Default.多开2[1] = res2l.Text;
            Settings.Default.多开3[1] = res3l.Text;
            Settings.Default.多开4[1] = res4l.Text;
            Settings.Default.多开1[2] = res1r.Text;
            Settings.Default.多开2[2] = res2r.Text;
            Settings.Default.多开3[2] = res3r.Text;
            Settings.Default.多开4[2] = res4r.Text;
            Settings.Default.多开1[3] = dpi1.Text;
            Settings.Default.多开2[3] = dpi2.Text;
            Settings.Default.多开3[3] = dpi3.Text;
            Settings.Default.多开4[3] = dpi4.Text;
            Settings.Default.Save();
        }

        private void MultiTaskMode_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = Convert.ToBoolean(Settings.Default.多开1[0]);
            checkBox2.Checked = Convert.ToBoolean(Settings.Default.多开2[0]);
            checkBox3.Checked = Convert.ToBoolean(Settings.Default.多开3[0]);
            checkBox4.Checked = Convert.ToBoolean(Settings.Default.多开4[0]);
            res1l.Text = Settings.Default.多开1[1];
            res2l.Text = Settings.Default.多开2[1];
            res3l.Text = Settings.Default.多开3[1];
            res4l.Text = Settings.Default.多开4[1];
            res1r.Text = Settings.Default.多开1[2];
            res2r.Text = Settings.Default.多开2[2];
            res3r.Text = Settings.Default.多开3[2];
            res4r.Text = Settings.Default.多开4[2];
            dpi1.Text = Settings.Default.多开1[3];
            dpi2.Text = Settings.Default.多开2[3];
            dpi3.Text = Settings.Default.多开3[3];
            dpi4.Text = Settings.Default.多开4[3];
            Checked_Changed(this, new EventArgs());
            _ = main.Cmd("bin\\adb -s " + device + " shell settings delete global overlay_display_devices", "deleteDisplay");
            _ = main.Cmd("taskkill /F /IM scrcpy.exe","killScrcpy");
            app1.SelectedItem = app1.Items[0];
            app2.SelectedItem = app2.Items[0];
            app3.SelectedItem = app3.Items[0];
            app4.SelectedItem = app4.Items[0];
            _ = main.Cmd("bin\\adb -s " + device + " push MultiModeSh /data/local/tmp", "pushShFile");
            string[] cpuabi = main.Cmd("bin\\adb -s " + device + " shell getprop ro.product.cpu.abi","getCPUabi");
            if (cpuabi[4] == "arm64-v8a")
            {
                _ = main.Cmd("bin\\adb -s " + device + " shell mv /data/local/tmp/MultiModeSh/aapt-arm64-v8a /data/local/tmp/MultiModeSh/aapt", "pushaapt");
            }
            else
            {
                _ = main.Cmd("bin\\adb -s " + device + " shell mv /data/local/tmp/MultiModeSh/aapt-armeabi-v7a /data/local/tmp/MultiModeSh/aapt", "pushaapt");
            }
            _ = main.Cmd("bin\\adb -s " + device + " shell chmod 777 /data/local/tmp/MultiModeSh/aapt", "chmodaapt");
            Task task = Task.Run(() =>
            {
                _ = main.Cmd("bin\\adb -s " + device + " shell sh /data/local/tmp/MultiModeSh/div.sh", "shdiv");
                _ = main.Cmd("bin\\adb -s " + device + " pull /data/local/tmp/MultiModeSh/packageInfo " + Directory.GetCurrentDirectory(), "pullPackInfo");
                appLabel = main.ReadFile("packageInfo\\appLabel");
                string[] activity = main.ReadFile("packageInfo\\activitys");
                string[] name = main.ReadFile("packageInfo\\packName");
                app1.Items.Clear();
                app2.Items.Clear();
                app3.Items.Clear();
                app4.Items.Clear();
                for (int i = 0; i < appLabel.Length - 1; i++)
                {
                    if (appLabel[i] != "NoLabel" && activity[i] != "NoActivity")
                    {
                        packActivityAr.Add(activity[i]);
                        app1.Items.Add(appLabel[i]);
                        app2.Items.Add(appLabel[i]);
                        app3.Items.Add(appLabel[i]);
                        app4.Items.Add(appLabel[i]);
                    }
                }
                packActivity = (string[])packActivityAr.ToArray(typeof(string));
                app1.SelectedItem = app1.Items[0];
                app2.SelectedItem = app2.Items[1];
                app3.SelectedItem = app3.Items[2];
                app4.SelectedItem = app4.Items[3];
                _ = main.Cmd("rmdir /S /Q packageInfo", "delPackInfo");
                _ = main.Cmd("bin\\adb -s " + device + " shell rm -rf /data/local/tmp/MultiModeSh", "rmShFile");
            });

            int mid1 = this.ClientRectangle.Width / 4 / 2 - 10;
            int mid2 = this.ClientRectangle.Width / 4 + this.ClientRectangle.Width / 4 / 2;
            int mid3 = this.ClientRectangle.Width / 4 * 2 + this.ClientRectangle.Width / 4 / 2;
            int mid4 = this.ClientRectangle.Width / 4 * 3 + this.ClientRectangle.Width / 4 / 2;
            checkBox1.Left = mid1 - checkBox1.Width / 2 + 10;
            checkBox2.Left = mid2 - checkBox2.Width / 2 + 10;
            checkBox3.Left = mid3 - checkBox3.Width / 2 + 10;
            checkBox4.Left = mid4 - checkBox4.Width / 2 + 10;
            r1.Left = mid1 - r1.Width / 2;
            r2.Left = mid2 - r2.Width / 2;
            r3.Left = mid3 - r3.Width / 2;
            r4.Left = mid4 - r4.Width / 2;
            res1l.Left = mid1 - ( res1l.Width + x1.Width + res1r.Width + ui1.Width ) / 2 + 10;
            res2l.Left = mid2 - (res2l.Width + x2.Width + res2r.Width + ui2.Width) / 2 + 10;
            res3l.Left = mid3 - (res3l.Width + x3.Width + res3r.Width + ui3.Width) / 2 + 10;
            res4l.Left = mid4 - (res4l.Width + x4.Width + res4r.Width + ui4.Width) / 2 + 10;
            x1.Left = res1l.Right;
            x2.Left = res2l.Right;
            x3.Left = res3l.Right;
            x4.Left = res4l.Right;
            res1r.Left = x1.Right;
            res2r.Left = x2.Right;
            res3r.Left = x3.Right;
            res4r.Left = x4.Right;
            ui1.Left = res1r.Right;
            ui2.Left = res2r.Right;
            ui3.Left = res3r.Right;
            ui4.Left = res4r.Right;
            d1.Left = mid1 - d1.Width / 2;
            d2.Left = mid2 - d2.Width / 2;
            d3.Left = mid3 - d3.Width / 2;
            d4.Left = mid4 - d4.Width / 2;
            dpi1.Left = mid1 - dpi1.Width / 2;
            dpi2.Left = mid2 - dpi2.Width / 2;
            dpi3.Left = mid3 - dpi3.Width / 2;
            dpi4.Left = mid4 - dpi4.Width / 2;
            app1.Left = mid1 - app1.Width / 2;
            app2.Left = mid2 - app2.Width / 2;
            app3.Left = mid3 - app3.Width / 2;
            app4.Left = mid4 - app4.Width / 2;
            Enter.Left = this.ClientRectangle.Width / 2 - Enter.Width / 2;

            checkBox1.Top = this.ClientRectangle.Height / 5 / 2 - checkBox1.Height / 2;
            checkBox2.Top = checkBox1.Top;
            checkBox3.Top = checkBox1.Top;
            checkBox4.Top = checkBox1.Top;
            r1.Top = this.ClientRectangle.Height / 5 + this.ClientRectangle.Height / 5 / 2 - ( r1.Height + res1l.Height ) / 2 - 10;
            r2.Top = r1.Top;
            r3.Top = r1.Top;
            r4.Top = r1.Top;
            res1l.Top = r1.Bottom;
            res2l.Top = res1l.Top;
            res3l.Top = res1l.Top;
            res4l.Top = res1l.Top;
            x1.Top = res1l.Bottom - res1l.Height / 2 - x1.Height / 2;
            x2.Top = x1.Top;
            x3.Top = x1.Top;
            x4.Top = x1.Top;
            res1r.Top = res1l.Top;
            res2r.Top = res1l.Top;
            res3r.Top = res1l.Top;
            res4r.Top = res1l.Top;
            ui1.Top = res1l.Top;
            ui2.Top = res1l.Top;
            ui3.Top = res1l.Top;
            ui4.Top = res1l.Top;
            d1.Top = this.ClientRectangle.Height / 5 * 2 + this.ClientRectangle.Height / 5 / 2 - ( d1.Height + dpi1.Height ) / 2;
            d2.Top = d1.Top;
            d3.Top = d1.Top;
            d4.Top = d1.Top;
            dpi1.Top = d1.Bottom;
            dpi2.Top = dpi1.Top;
            dpi3.Top = dpi1.Top;
            dpi4.Top = dpi1.Top;
            app1.Top = this.ClientRectangle.Height / 5 * 3 + this.ClientRectangle.Height / 5 / 2 - app1.Height / 2;
            app2.Top = app1.Top;
            app3.Top = app1.Top;
            app4.Top = app1.Top;
            Enter.Top = this.ClientRectangle.Height / 5 * 4 + this.ClientRectangle.Height / 5 / 2 - Enter.Height / 2;
        }

        private void MultiTaskMode_FormClosed(object sender, FormClosedEventArgs e)
        {
            Main main = new Main();
            main.Show();
            this.Hide();
        }

        private void ui1_Click(object sender, EventArgs e)
        {
            try
            {
                preview1.Show();
                preview1.SetForm(new Size(int.Parse(res1l.Text), int.Parse(res1r.Text)));
                getResolution.Enabled = true;
            }
            catch
            { 
                preview1 = new Preview1();
                preview1.Show();
                preview1.SetForm(new Size(int.Parse(res1l.Text), int.Parse(res1r.Text)));
                getResolution.Enabled = true;
            }
        }

        private void ui2_Click(object sender, EventArgs e)
        {
            try
            {
                preview2.Show();
                preview2.SetForm(new Size(int.Parse(res2l.Text), int.Parse(res2r.Text)));
                getResolution.Enabled = true;
            }
            catch
            {
                preview2 = new Preview2();
                preview2.Show();
                preview2.SetForm(new Size(int.Parse(res1l.Text), int.Parse(res1r.Text)));
                getResolution.Enabled = true;
            }
        }

        private void ui3_Click(object sender, EventArgs e)
        {
            try
            {
                preview3.Show();
                preview3.SetForm(new Size(int.Parse(res3l.Text), int.Parse(res3r.Text)));
                getResolution.Enabled = true;
            }
            catch
            {
                preview3 = new Preview3();
                preview3.Show();
                preview3.SetForm(new Size(int.Parse(res1l.Text), int.Parse(res1r.Text)));
                getResolution.Enabled = true;
            }
        }

        private void ui4_Click(object sender, EventArgs e)
        {
            try
            {
                preview4.Show();
                preview4.SetForm(new Size(int.Parse(res4l.Text), int.Parse(res4r.Text)));
                getResolution.Enabled = true;
            }
            catch
            {
                preview4 = new Preview4();
                preview4.Show();
                preview4.SetForm(new Size(int.Parse(res1l.Text), int.Parse(res1r.Text)));
                getResolution.Enabled = true;
            }
        }

        private void check_Tick(object sender, EventArgs e)
        {
            wait++;
            if (wait == 5)
            {
                if (flag)
                {
                    if (success)
                    {
                        _ = main.Cmd("bin\\adb -s " + device + " shell settings delete global overlay_display_devices", "deleteDisplay");
                        Environment.Exit(0);
                    }
                    success = true;
                }
                else
                {
                    check.Enabled = true;
                }
                wait = 1;
                flag = true;
            }
            if (checkBox1.Checked && wait == 1)
            {
                IntPtr hWnd = FindWindow(null, app1.SelectedItem.ToString());
                if (hWnd == IntPtr.Zero && !success)
                {
                    flag = false;
                    string[] appActivity = packActivity[app1.SelectedIndex].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    Console.WriteLine("\n\n当前虚拟显示屏ID：" + id[0] + " 要启动的包名：" + appActivity[1] + " 应用名：" + app1.SelectedItem.ToString());
                    _ = main.Cmd("bin\\adb -s " + device + " shell am start-activity -S --display " + id[0] + " --windowingMode 1 " + appActivity[1], "displayApp1");
                    Task task1 = Task.Run(() =>
                    {
                        Cmd("bin\\scrcpy -s " + device + " --display-id=" + id[0] + " --shortcut-mod lctrl,rctrl --window-title=\"" + app1.SelectedItem.ToString() + "\" " + command);
                    });
                    nowApp = app1.SelectedItem.ToString();
                    waitScrcpy.Enabled = true;
                    Console.WriteLine(app1.SelectedItem.ToString() + " " + id[0] + " " + appActivity[1]);
                    check.Enabled = false;
                    return;
                }
                if (hWnd != IntPtr.Zero && success)
                {
                    flag = false;
                }
            }
            if (checkBox2.Checked && wait == 2)
            {
                IntPtr hWnd = FindWindow(null, app2.SelectedItem.ToString());
                if (hWnd == IntPtr.Zero && !success)
                {
                    flag = false;
                    string[] appActivity = packActivity[app2.SelectedIndex].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    Console.WriteLine("\n\n当前虚拟显示屏ID：" + id[1] + " 要启动的包名：" + appActivity[1] + " 应用名：" + app2.SelectedItem.ToString());
                    _ = main.Cmd("bin\\adb -s " + device + " shell am start-activity -S --display " + id[1] + " --windowingMode 1 " + appActivity[1], "displayApp2");
                    Task task2 = Task.Run(() =>
                    {
                        Cmd("bin\\scrcpy -s " + device + " --display-id=" + id[1] + " --shortcut-mod lctrl,rctrl --window-title=\"" + app2.SelectedItem.ToString() + "\" " + command);
                    });
                    nowApp = app2.SelectedItem.ToString();
                    waitScrcpy.Enabled = true;
                    Console.WriteLine(app2.SelectedItem.ToString() + " " + id[1] + " " + appActivity[1]);
                    check.Enabled = false;
                    return;
                }
                if (hWnd != IntPtr.Zero && success)
                {
                    flag = false;
                }
            }
            if (checkBox3.Checked && wait == 3)
            {
                IntPtr hWnd = FindWindow(null, app3.SelectedItem.ToString());
                if (hWnd == IntPtr.Zero && !success)
                {
                    flag = false;
                    string[] appActivity = packActivity[app3.SelectedIndex].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    Console.WriteLine("\n\n当前虚拟显示屏ID：" + id[2] + " 要启动的包名：" + appActivity[1] + " 应用名：" + app3.SelectedItem.ToString());
                    _ = main.Cmd("bin\\adb -s " + device + " shell am start-activity -S --display " + id[2] + " --windowingMode 1 " + appActivity[1], "displayApp3");
                    Task task3 = Task.Run(() =>
                    {
                        Cmd("bin\\scrcpy -s " + device + " --display-id=" + id[2] + " --shortcut-mod lctrl,rctrl --window-title=\"" + app3.SelectedItem.ToString() + "\" " + command);
                    });
                    nowApp = app3.SelectedItem.ToString();
                    waitScrcpy.Enabled = true;
                    Console.WriteLine(app3.SelectedItem.ToString() + " " + id[2] + " " + appActivity[1]);
                    check.Enabled = false;
                    return;
                }
                if (hWnd != IntPtr.Zero && success)
                {
                    flag = false;
                }
            }
            if (checkBox4.Checked && wait == 4)
            {
                IntPtr hWnd = FindWindow(null, app4.SelectedItem.ToString());
                if (hWnd == IntPtr.Zero && !success)
                {
                    flag = false;
                    string[] appActivity = packActivity[app4.SelectedIndex].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    Console.WriteLine("\n\n当前虚拟显示屏ID：" + id[3] + " 要启动的包名：" + appActivity[1] + " 应用名：" + app4.SelectedItem.ToString());
                    _ = main.Cmd("bin\\adb -s " + device + " shell am start-activity -S --display " + id[3] + " --windowingMode 1 " + appActivity[1], "displayApp4");
                    Task task4 = Task.Run(() =>
                    {
                        Cmd("bin\\scrcpy -s " + device + " --display-id=" + id[3] + " --shortcut-mod lctrl,rctrl --window-title=\"" + app4.SelectedItem.ToString() + "\" " + command);
                    });
                    nowApp = app4.SelectedItem.ToString();
                    waitScrcpy.Enabled = true;
                    Console.WriteLine(app4.SelectedItem.ToString() + " " + id[3] + " " + appActivity[1]);
                    check.Enabled = false;
                    return;
                }
                if (hWnd != IntPtr.Zero && success)
                {
                    flag = false;
                }
            }
        }

        private void getResolution_Tick(object sender, EventArgs e)
        {
            getResolution.Interval = 10;
            bool flag = false;
            IntPtr hWnd = FindWindow(null, "预览窗口#1");
            if (hWnd != IntPtr.Zero)
            {
                RECT fx = new RECT();       //定义窗口位置
                GetWindowRect(hWnd, ref fx);        //获取窗口位置
                res1l.Text = Convert.ToString((fx.Right - fx.Left - preview1.vWidth) * 3);
                res1r.Text = Convert.ToString((fx.Bottom - fx.Top - preview1.vHeight) * 3);
                preview1.SetLabel();
                flag = true;
            }
            hWnd = FindWindow(null, "预览窗口#2");
            if (hWnd != IntPtr.Zero)
            {
                RECT fx = new RECT();       //定义窗口位置
                GetWindowRect(hWnd, ref fx);        //获取窗口位置
                res2l.Text = Convert.ToString((fx.Right - fx.Left - preview2.vWidth) * 3);
                res2r.Text = Convert.ToString((fx.Bottom - fx.Top - preview2.vHeight) * 3);
                preview2.SetLabel();
                flag = true;
            }
            hWnd = FindWindow(null, "预览窗口#3");
            if (hWnd != IntPtr.Zero)
            {
                RECT fx = new RECT();       //定义窗口位置
                GetWindowRect(hWnd, ref fx);        //获取窗口位置
                res3l.Text = Convert.ToString((fx.Right - fx.Left - preview3.vWidth) * 3);
                res3r.Text = Convert.ToString((fx.Bottom - fx.Top - preview3.vHeight) * 3);
                preview3.SetLabel();
                flag = true;
            }
            hWnd = FindWindow(null, "预览窗口#4");
            if (hWnd != IntPtr.Zero)
            {
                RECT fx = new RECT();       //定义窗口位置
                GetWindowRect(hWnd, ref fx);        //获取窗口位置
                res4l.Text = Convert.ToString((fx.Right - fx.Left - preview4.vWidth) * 3);
                res4r.Text = Convert.ToString((fx.Bottom - fx.Top - preview4.vHeight) * 3);
                preview4.SetLabel();
                flag = true;
            }
            if (!flag)
            {
                getResolution.Interval = 300;
                getResolution.Enabled = false;
            }
            Console.Write("?");
        }

        private void Input_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8 && e.KeyChar != 46 && e.KeyChar != 13 && e.KeyChar != 27))
            {
                e.Handled = true;
            }
        }

        private void Input_Leave(object sender, EventArgs e)
        {
            if (res1l.Text.Length == 0)
            {
                res1l.Text = "300";
            }
            if (int.Parse(res1l.Text) < 300)
            {
                res1l.Text = "300";
            }
            if (int.Parse(res1l.Text) > 4096)
            {
                res1l.Text = "4096";
            }
            if (res2l.Text.Length == 0)
            {
                res2l.Text = "300";
            }
            if (int.Parse(res2l.Text) < 300)
            {
                res2l.Text = "300";
            }
            if (int.Parse(res2l.Text) > 4096)
            {
                res2l.Text = "4096";
            }
            if (res3l.Text.Length == 0)
            {
                res3l.Text = "300";
            }
            if (int.Parse(res3l.Text) < 300)
            {
                res3l.Text = "300";
            }
            if (int.Parse(res3l.Text) > 4096)
            {
                res3l.Text = "4096";
            }
            if (res4l.Text.Length == 0)
            {
                res4l.Text = "300";
            }
            if (int.Parse(res4l.Text) < 300)
            {
                res4l.Text = "300";
            }
            if (int.Parse(res4l.Text) > 4096)
            {
                res4l.Text = "4096";
            }
            if (res1r.Text.Length == 0)
            {
                res1r.Text = "300";
            }
            if (int.Parse(res1r.Text) < 300)
            {
                res1r.Text = "300";
            }
            if (int.Parse(res1r.Text) > 4096)
            {
                res1r.Text = "4096";
            }
            if (res2r.Text.Length == 0)
            {
                res2r.Text = "300";
            }
            if (int.Parse(res2r.Text) < 300)
            {
                res2r.Text = "300";
            }
            if (int.Parse(res2r.Text) > 4096)
            {
                res2r.Text = "4096";
            }
            if (res3r.Text.Length == 0)
            {
                res3r.Text = "300";
            }
            if (int.Parse(res3r.Text) < 300)
            {
                res3r.Text = "300";
            }
            if (int.Parse(res3r.Text) > 4096)
            {
                res3r.Text = "4096";
            }
            if (res4r.Text.Length == 0)
            {
                res4r.Text = "300";
            }
            if (int.Parse(res4r.Text) < 300)
            {
                res4r.Text = "300";
            }
            if (int.Parse(res4r.Text) > 4096)
            {
                res4r.Text = "4096";
            }
            if (dpi1.Text.Length == 0)
            {
                dpi1.Text = "120";
            }
            if (int.Parse(dpi1.Text) < 300)
            {
                dpi1.Text = "120";
            }
            if (int.Parse(dpi1.Text) > 640)
            {
                dpi1.Text = "640";
            }
            if (dpi2.Text.Length == 0)
            {
                dpi2.Text = "120";
            }
            if (int.Parse(dpi2.Text) < 300)
            {
                dpi2.Text = "120";
            }
            if (int.Parse(dpi2.Text) > 640)
            {
                dpi2.Text = "640";
            }
            if (dpi3.Text.Length == 0)
            {
                dpi3.Text = "120";
            }
            if (int.Parse(dpi3.Text) < 300)
            {
                dpi3.Text = "120";
            }
            if (int.Parse(dpi3.Text) > 640)
            {
                dpi3.Text = "640";
            }
            if (dpi4.Text.Length == 0)
            {
                dpi4.Text = "120";
            }
            if (int.Parse(dpi4.Text) < 300)
            {
                dpi4.Text = "120";
            }
            if (int.Parse(dpi4.Text) > 640)
            {
                dpi4.Text = "640";
            }
        }

        private void Checked_Changed(object sender, EventArgs e)
        {
            if (!checkBox1.Checked)
            {
                res1l.Enabled = false;
                res1r.Enabled = false;
                dpi1.Enabled = false;
                ui1.Enabled = false;
            }
            else
            {
                res1l.Enabled = true;
                res1r.Enabled = true;
                dpi1.Enabled = true;
                ui1.Enabled = true;
            }
            if (!checkBox2.Checked)
            {
                res2l.Enabled = false;
                res2r.Enabled = false;
                dpi2.Enabled = false;
                ui2.Enabled = false;
            }
            else
            {
                res2l.Enabled = true;
                res2r.Enabled = true;
                dpi2.Enabled = true;
                ui2.Enabled = true;
            }
            if (!checkBox3.Checked)
            {
                res3l.Enabled = false;
                res3r.Enabled = false;
                dpi3.Enabled = false;
                ui3.Enabled = false;
            }
            else
            {
                res3l.Enabled = true;
                res3r.Enabled = true;
                dpi3.Enabled = true;
                ui3.Enabled = true;
            }
            if (!checkBox4.Checked)
            {
                res4l.Enabled = false;
                res4r.Enabled = false;
                dpi4.Enabled = false;
                ui4.Enabled = false;
            }
            else
            {
                res4l.Enabled = true;
                res4r.Enabled = true;
                dpi4.Enabled = true;
                ui4.Enabled = true;
            }
        }

        private void waitScrcpy_Tick(object sender, EventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("等待[");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(nowApp);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("]窗口出现");
            Console.ForegroundColor = ConsoleColor.White;
            IntPtr hWnd = FindWindow(null, nowApp);
            if (hWnd != IntPtr.Zero)
            {
                Console.ForegroundColor= ConsoleColor.Green;
                Console.WriteLine("成功\n");
                Console.ForegroundColor = ConsoleColor.White;
                waitScrcpy.Enabled = false;
                check.Enabled = true;
            }
        }

        private void app_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (app4.Items[0].ToString() == "正在获取应用列表" || app4.Text.Length == 0)
            {
                return;
            }
            if (app2.SelectedItem == app1.SelectedItem) 
            {
                checkBox2.Enabled = false;
                checkBox2.Checked = false;
            }
            else
            {
                checkBox2.Enabled= true;
            }
            if (app3.SelectedItem == app1.SelectedItem || app3.SelectedItem == app2.SelectedItem)
            {
                checkBox3.Enabled = false;
                checkBox3.Checked = false;
            }
            else
            {
                checkBox3.Enabled = true;
            }
            if (app4.SelectedItem == app1.SelectedItem || app4.SelectedItem == app2.SelectedItem || app4.SelectedItem == app3.SelectedItem)
            {
                checkBox4.Enabled = false;
                checkBox4.Checked = false;
            }
            else
            {
                checkBox4.Enabled = true;
            }
        }
    }
}
