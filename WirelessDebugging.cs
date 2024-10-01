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
using System.IO;

namespace Scrcpy_GUI
{
    public partial class WirelessDebugging : Form
    {
        int color = 240;    //CheckPortLabel的颜色
        public bool disableMulti = false;

        public WirelessDebugging()
        {
            InitializeComponent();
        }

        private void WirelessDebugging_Load(object sender, EventArgs e)    //窗体控件对齐
        {
            EnterIP.Left = this.ClientRectangle.Width / 2 - EnterIP.Width / 2;
            IP.Left = this.ClientRectangle.Width / 2 - (IP.Width + label1.Width + Port.Width) / 2;
            label1.Left = IP.Right;
            Port.Left = label1.Right;
            Enter.Left = this.ClientRectangle.Width / 2 - Enter.Width / 2;
            PairingLabel.Left = this.ClientRectangle.Width / 2 - PairingLabel.Width / 2;
            PairingCode.Left = this.ClientRectangle.Width / 2 - (PairingCode.Width + PairingPort.Width + 20) / 2;
            PairingPort.Left = PairingCode.Right + 10;
            CheckPortLabel.Left = this.ClientRectangle.Width / 2 - CheckPortLabel.Width / 2;

            EnterIP.Top = this.ClientRectangle.Height / 3 / 2 - EnterIP.Height / 2;
            IP.Top = EnterIP.Bottom + 20;
            label1.Top = IP.Top;
            Port.Top = label1.Top;
            Enter.Top = this.ClientRectangle.Height - this.ClientRectangle.Height / 3 / 2 - Enter.Height / 2;
            PairingLabel.Top = IP.Bottom + 10;
            PairingCode.Top = PairingLabel.Bottom + 10;
            PairingPort.Top = PairingCode.Top;
            CheckPortLabel.Top = PairingCode.Bottom + 5;

        }

        private void WirelessDebugging_FormClosed(object sender, FormClosedEventArgs e)    //关闭窗体时
        {
            if (disableMulti)
            {
                string[] arg = new string[] { "--disableMulti" };
                Main main = new Main(arg);
                main.Show();
            }
            else
            {
                Main main = new Main();
                main.Show();
            }
            this.Hide();
        }

        private void IP_Enter(object sender, EventArgs e)    //（获取焦点）
        {
            if (IP.Text == "192.168.X.X" && IP.ForeColor == Color.Silver)
            {
                IP.Text = "";
                IP.ForeColor = Color.Black;
            }
        }

        private void IP_Leave(object sender, EventArgs e)    //（失去焦点）
        {
            if(IP.Text == "" && IP.ForeColor == Color.Black)
            {
                IP.Text = "192.168.X.X";
                IP.ForeColor = Color.Silver;
            }
        }

        private void Port_Enter(object sender, EventArgs e)    //（获取焦点）
        {
            if (Port.Text == "XXXXX" && Port.ForeColor == Color.Silver)
            {
                Port.Text = "";
                Port.ForeColor = Color.Black;
            }
        }

        private void Port_Leave(object sender, EventArgs e)    //（失去焦点）
        {
            if (Port.Text == "" && Port.ForeColor == Color.Black)
            {
                Port.Text = "XXXXX";
                Port.ForeColor = Color.Silver;
            }
        }

        private void PairingCode_Enter(object sender, EventArgs e)    //（获取焦点）
        {
            if (PairingCode.Text == "配对码" && PairingCode.ForeColor == Color.Silver)
            {
                PairingCode.Text = "";
                PairingCode.ForeColor = Color.Black;
            }
        }

        private void PairingCode_Leave(object sender, EventArgs e)    //（失去焦点）
        {
            if (PairingCode.Text == "" && PairingCode.ForeColor == Color.Black)
            {
                PairingCode.Text = "配对码";
                PairingCode.ForeColor = Color.Silver;
            }
        }

        private void PairingPort_Enter(object sender, EventArgs e)    //（获取焦点）
        {
            if (PairingPort.Text == "端口" && PairingPort.ForeColor == Color.Silver)
            {
                PairingPort.Text = "";
                PairingPort.ForeColor = Color.Black;
            }
        }

        private void PairingPort_Leave(object sender, EventArgs e)    //（失去焦点）
        {
            if (PairingPort.Text == "" && PairingPort.ForeColor == Color.Black)
            {
                PairingPort.Text = "端口";
                PairingPort.ForeColor = Color.Silver;
            }
        }

        private void Enter_Click(object sender, EventArgs e)    //点击确定
        {
            if (IP.ForeColor == Color.Silver || Port.ForeColor == Color.Silver)    //判断两个控件的颜色
            {
                MessageBox.Show("未填写IP或者端口");
            }
            else
            {
                PairingCode.Enabled = false;         //禁用所有控件 防止误操作
                PairingPort.Enabled = false;
                IP.Enabled = false;
                Port.Enabled = false;
                Enter.Enabled = false;
                CheckPortLabel.Text = "正在连接…";          //直接用CheckPortLabel作提示得了（
                CheckPortLabel.Left = this.ClientRectangle.Width / 2 - CheckPortLabel.Width / 2;
                CheckPortLabel.ForeColor = Color.Black;
                Main main = new Main();
                var task1 = Task.Run(async delegate         //异步执行连接设备
                {
                    await Task.Delay(500);     //延迟0.5秒
                    if (PairingCode.ForeColor == Color.Silver && PairingPort.ForeColor == Color.Silver)     //判断是否使用配对码连接
                    {
                        Debug.Print("不使用配对模式");
                        string[] output = main.Cmd("bin\\adb connect " + IP.Text + ":" + Port.Text, "wireless");     //执行连接
                        if (output[4].Substring(0, 6) == "cannot")      //判断连接失败
                        {
                            MessageBox.Show("请检查IP或端口是否正确\n\n错误信息：\n" + output[4], "无法连接到设备");
                            return "失败";
                        }
                        else if (output[4].Substring(0, 6) == "failed")      //已配对设备里没有你电脑就会触发这个
                        {
                            MessageBox.Show("判断：此电脑并未连接过该设备，未被信任\n请使用配对码连接 让设备信任此电脑\n\n错误信息：\n" + output[4], "无法连接到设备");
                            return "失败";
                        }
                        else
                        {
                            return "成功";
                        }
                    }
                    else if(PairingCode.ForeColor == Color.Black && PairingPort.ForeColor == Color.Black)
                    {
                        Debug.Print("使用配对模式");
                        Debug.Print("执行命令：" + "bin\\adb pair " + IP.Text + ":" + PairingPort.Text);
                        Debug.Print("配对码：" + PairingCode.Text);
                        Process p = new Process();
                        p.StartInfo.FileName = "cmd.exe";
                        p.StartInfo.UseShellExecute = false;
                        p.StartInfo.RedirectStandardInput = true;
                        p.StartInfo.RedirectStandardOutput = true;
                        p.StartInfo.RedirectStandardError = true;
                        p.StartInfo.CreateNoWindow = true;
                        p.StartInfo.StandardOutputEncoding = Encoding.UTF8;
                        p.Start();
                        p.StandardInput.WriteLine("bin\\adb pair " + IP.Text + ":" + PairingPort.Text + " & exit");     //分别向cmd输入配对命令和配对码
                        p.StandardInput.WriteLine(PairingCode.Text);
                        p.StandardInput.AutoFlush = true;
                        string strOutput = p.StandardOutput.ReadToEnd();
                        p.WaitForExit();
                        p.Close();
                        main.WriteFile("wireless", strOutput);
                        string[] output = main.ReadFile("wireless");
                        File.Delete(main.appPath + "\\" + "wireless");
                        try
                        {
                            if (output[4].Substring(20, 12) == "Successfully")      //判断连接成功
                            {
                                Debug.Print("配对成功");
                                main.Cmd("bin\\adb connect " + IP.Text + ":" + Port.Text, "wireless");     //执行连接;
                                return "成功";
                            }
                            return "";
                        }
                        catch       //因为连接失败会导致程序错误，所以使用try catch
                        {
                            Debug.Print("配对失败");
                            MessageBox.Show("请检查IP、端口、配对端口或配对码是否正确", "无法连接到设备");
                            return "失败";
                        }
                    }
                    else
                    {
                        Debug.Print("配对模式有东西漏填了");
                        MessageBox.Show("你下面配对模式漏了个没填欸！", "真粗心！");
                        return "失败";
                    }
                });
                while (task1.Result == "")      //等待连接设备的结果
                {
                }
                if (task1.Result == "失败")       //重新启用所有控件并恢复原样
                {
                    IP.Enabled = true;
                    Port.Enabled = true;
                    Enter.Enabled = true;
                    PairingCode.Enabled = true;
                    PairingPort.Enabled = true;
                    CheckPortLabel.Text = "只能输入数字";
                    CheckPortLabel.ForeColor = Color.FromArgb(240, 240, 240);
                }
                else        //回到主菜单
                {
                    main.Show();
                    this.Hide();
                }
            }
        }

        private void IP_KeyPress(object sender, KeyPressEventArgs e)    //检测IP控件输入情况
        {
            if(e.KeyChar == 32)         //防止输入空格
            {
                e.Handled = true;
            }
        }

        private void Port_KeyPress(object sender, KeyPressEventArgs e)    //检测Port控件输入情况
        {       //防止输入除数字外的内容
            if ((e.KeyChar < 48 || e.KeyChar > 57 ) && (e.KeyChar != 8 && e.KeyChar != 46 && e.KeyChar != 13 && e.KeyChar != 27))
            {
                CheckPortLabel.Text = "只能输入数字";
                CheckPortTimer.Enabled = true;
                color = 36;
                e.Handled = true;
            }
        }

        private void CheckPortTimer_Tick(object sender, EventArgs e)    //CheckPortLabel渐变消失
        {
            CheckPortLabel.ForeColor = Color.FromArgb(color,color,color);
            if(color >= 240) 
            {
                CheckPortTimer.Enabled = false; 
            }
            color+=6;
        }

        private void Port_TextChanged(object sender, EventArgs e)      //检测Port控件输入数字大小
        {
            if(Port.Text.Length != 0 && Port.Text != "XXXXX")
            {
                if(int.Parse(Port.Text) > 65534)
                {
                    Port.Text = "65534";
                    CheckPortLabel.Text = "端口超过上限";
                    CheckPortTimer.Enabled= true;
                    color = 36;
                }
            }
        }

        private void PairingPort_KeyPress(object sender, KeyPressEventArgs e)
        {       //防止输入除数字外的内容
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8 && e.KeyChar != 46 && e.KeyChar != 13 && e.KeyChar != 27))
            {
                CheckPortLabel.Text = "只能输入数字";
                CheckPortTimer.Enabled = true;
                color = 36;
                e.Handled = true;
            }
        }

        private void PairingCode_KeyPress(object sender, KeyPressEventArgs e)
        {       //防止输入除数字外的内容
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8 && e.KeyChar != 46 && e.KeyChar != 13 && e.KeyChar != 27))
            {
                CheckPortLabel.Text = "只能输入数字";
                CheckPortTimer.Enabled = true;
                color = 36;
                e.Handled = true;
            }
        }
    }
}
