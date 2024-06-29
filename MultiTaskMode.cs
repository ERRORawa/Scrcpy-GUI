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
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace scrcpy_gui
{
    public partial class MultiTaskMode : Form
    {
        [DllImport("user32.dll", EntryPoint = "FindWindow")]             //调用DLL —— 查找窗口 句柄
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);   //查找窗口 句柄（Class,窗体标题）

        Main main = new Main();

        public string device;

        public string command;

        ArrayList packNameAr = new ArrayList();

        string[] packName;

        string[] appLabel;

        int[] id;

        int idx;

        ArrayList packActivityAr = new ArrayList();

        string[] packActivity;
        public MultiTaskMode()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }
        public void Cmd1(string command)    //执行命令（异步），无输出
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.StandardInput.WriteLine(command);
        }
        public void Cmd2(string command)    //执行命令（异步），无输出
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.StandardInput.WriteLine(command);
        }
        public void Cmd3(string command)    //执行命令（异步），无输出
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.StandardInput.WriteLine(command);
        }
        public void Cmd4(string command)    //执行命令（异步），无输出
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.StandardInput.WriteLine(command);
        }
        private void Enter_Click(object sender, EventArgs e)
        {
            string displayInfo = "";
            bool flag = false;
            int num = 0;
            if (checkBox1.Checked)
            {
                if(res1l.Text != "" && res1r.Text != "" && dpi1.Text != "" && app1.SelectedItem.ToString() != "选择软件")
                {
                    displayInfo = displayInfo + res1l.Text + "x" + res1r.Text + "/" + dpi1.Text;
                    flag = true;
                }
                num++;
            }
            if (checkBox2.Checked)
            {
                if (flag)
                {
                    displayInfo = displayInfo + "\\;";
                }
                if (res2l.Text != "" && res2r.Text != "" && dpi2.Text != "" && app2.SelectedItem.ToString() != "选择软件")
                {
                    displayInfo = displayInfo + res2l.Text + "x" + res2r.Text + "/" + dpi2.Text;
                    flag = true;
                }
                num++;
            }
            if (checkBox3.Checked)
            {
                if (flag)
                {
                    displayInfo = displayInfo + "\\;";
                }
                if (res3l.Text != "" && res3r.Text != "" && dpi3.Text != "" && app3.SelectedItem.ToString() != "选择软件")
                {
                    displayInfo = displayInfo + res3l.Text + "x" + res3r.Text + "/" + dpi3.Text;
                    flag = true;
                }
                num++;
            }
            if (checkBox4.Checked)
            {
                if (flag)
                {
                    displayInfo = displayInfo + "\\;";
                }
                if (res4l.Text != "" && res4r.Text != "" && dpi4.Text != "" && app4.SelectedItem.ToString() != "选择软件")
                {
                    displayInfo = displayInfo + res4l.Text + "x" + res4r.Text + "/" + dpi4.Text;
                    flag = true;
                }
                num++;
            }
            Debug.Print(displayInfo);
            _ = main.Cmd("bin\\adb -s " + device + " shell settings put global overlay_display_devices \"" + displayInfo + "\"", "createDisplay");
            Thread.Sleep(200);
            string[] displayID = main.Cmd("bin\\adb -s " + device + " shell dumpsys display ^| grep \"\\ \\ \\ \\ mDisplayId=\"", "displayID");
            ArrayList idAr = new ArrayList();
            for (int i = 4; ; i++)
            {
                if (displayID[i] == "")
                {
                    break;
                }
                else
                {
                    Debug.Print(displayID[i].ToString());
                    idAr.Add(int.Parse(displayID[i].Substring(15, displayID[i].Length - 15)));
                }
            }
            id = (int[])idAr.ToArray(typeof(int));
            idx = id.Length - num;
            check.Enabled = true;
        }

        private void MultiTaskMode_Load(object sender, EventArgs e)
        {
            _ = main.Cmd("bin\\adb -s " + device + " shell settings delete global overlay_display_devices", "command");
            app1.SelectedItem = app1.Items[0];
            app2.SelectedItem = app2.Items[0];
            app3.SelectedItem = app3.Items[0];
            app4.SelectedItem = app4.Items[0];
            _ = main.Cmd("bin\\adb -s " + device + " push aapt /data/local/tmp/aapt","command");
            _ = main.Cmd("bin\\adb -s " + device + " shell chmod 777 /data/local/tmp/aapt", "command");
            Task task = Task.Run(() =>
            {
                _ = main.Cmd("bin\\adb -s " + device + " shell sh /data/local/tmp/div.sh", "shell");
                _ = main.Cmd("bin\\adb -s " + device + " pull /data/local/tmp/packageInfo " + main.appPath,"shell");
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
                        packNameAr.Add(name[i]);
                        packActivityAr.Add(activity[i]);
                        app1.Items.Add(appLabel[i]);
                        app2.Items.Add(appLabel[i]);
                        app3.Items.Add(appLabel[i]);
                        app4.Items.Add(appLabel[i]);
                    }
                }
                packName = (string[])packNameAr.ToArray(typeof(string));
                packActivity = (string[])packActivityAr.ToArray(typeof(string));
                app1.SelectedItem = app1.Items[0];
                app2.SelectedItem = app2.Items[0];
                app3.SelectedItem = app3.Items[0];
                app4.SelectedItem = app4.Items[0];
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

        }

        private void check_Tick(object sender, EventArgs e)
        {
            int idxx = idx;
            bool flag = true;
            if (checkBox1.Checked)
            {
                IntPtr hWnd = FindWindow(null, app1.SelectedItem.ToString());
                if (hWnd == IntPtr.Zero)
                {
                    flag = false;
                }
                    string[] appActivity = packActivity[app1.SelectedIndex].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    _ = main.Cmd("bin\\adb -s " + device + " shell am start-activity -S --display " + id[idx] + " --windowingMode 1 " + appActivity[1], "displayApp");
                    Cmd1("bin\\scrcpy -s " + device + " --display-id=" + id[idx] + " --shortcut-mod lctrl,rctrl --window-title=\"" + app1.SelectedItem.ToString() + "\" " + command + " > multi1");
                
                idx++;
            }
            if (checkBox2.Checked)
            {
                IntPtr hWnd = FindWindow(null, app2.SelectedItem.ToString());
                if (hWnd == IntPtr.Zero)
                {
                    flag = false;
                }
                    string[] appActivity = packActivity[app2.SelectedIndex].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    _ = main.Cmd("bin\\adb -s " + device + " shell am start-activity -S --display " + id[idx] + " --windowingMode 1 " + appActivity[1], "displayApp");
                    Cmd2("bin\\scrcpy -s " + device + " --display-id=" + id[idx] + " --shortcut-mod lctrl,rctrl --window-title=\"" + app2.SelectedItem.ToString() + "\" " + command + " > multi2");
                
                idx++;
            }
            if (checkBox3.Checked)
            {
                IntPtr hWnd = FindWindow(null, app3.SelectedItem.ToString());
                if (hWnd == IntPtr.Zero)
                {
                    flag = false;
                }
                    string[] appActivity = packActivity[app3.SelectedIndex].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    _ = main.Cmd("bin\\adb -s " + device + " shell am start-activity -S --display " + id[idx] + " --windowingMode 1 " + appActivity[1], "displayApp");
                    Cmd3("bin\\scrcpy -s " + device + " --display-id=" + id[idx] + " --shortcut-mod lctrl,rctrl --window-title=\"" + app3.SelectedItem.ToString() + "\" " + command + " > multi3");
                
                idx++;
            }
            if (checkBox4.Checked)
            {
                IntPtr hWnd = FindWindow(null, app4.SelectedItem.ToString());
                if (hWnd == IntPtr.Zero)
                {
                    flag = false;
                }
                    string[] appActivity = packActivity[app4.SelectedIndex].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    _ = main.Cmd("bin\\adb -s " + device + " shell am start-activity -S --display " + id[idx] + " --windowingMode 1 " + appActivity[1], "displayApp");
                    Cmd4("bin\\scrcpy -s " + device + " --display-id=" + id[idx] + " --shortcut-mod lctrl,rctrl --window-title=\"" + app4.SelectedItem.ToString() + "\" " + command + " > multi4");
                
            }
            if (flag)
            {
                check.Enabled = false;
            }
            idx = idxx;
        }
    }
}
