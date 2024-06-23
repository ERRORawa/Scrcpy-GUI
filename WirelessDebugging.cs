using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace scrcpy_gui
{
    public partial class WirelessDebugging : Form
    {
        int color = 240;    //CheckPortLabel的颜色

        public WirelessDebugging()
        {
            InitializeComponent();
        }

        private void WirelessDebugging_Load(object sender, EventArgs e)    //窗体控件对齐
        {
            int all = IP.Width + label1.Width + Port.Width;

            EnterIP.Left = this.ClientRectangle.Width / 2 - EnterIP.Width / 2 - 20;
            IP.Left = this.ClientRectangle.Width / 2 - all / 2 - 50;
            label1.Left = IP.Right;
            Port.Left = label1.Right;
            Enter.Left = this.ClientRectangle.Width / 2 - Enter.Width / 2;
            CheckPortLabel.Left = Port.Left + Port.Width / 2 - CheckPortLabel.Width / 2;

            EnterIP.Top = this.ClientRectangle.Height / 3 / 2 - EnterIP.Height / 2;
            IP.Top = this.ClientRectangle.Height / 3 * 2 - this.ClientRectangle.Height / 3 / 2 - IP.Height / 2;
            label1.Top = IP.Top;
            Port.Top = label1.Top;
            Enter.Top = this.ClientRectangle.Height - this.ClientRectangle.Height / 3 / 2 - Enter.Height / 2;
            CheckPortLabel.Top = Port.Bottom + 5;
        }

        private void WirelessDebugging_FormClosed(object sender, FormClosedEventArgs e)    //关闭窗体时
        {
            Main main = new Main();
            main.Show();
            this.Hide();
        }

        private void IP_Enter(object sender, EventArgs e)    //（获取焦点）更改IP控件的颜色
        {
            if (IP.Text == "192.168.X.X" && IP.ForeColor == Color.Silver)
            {
                IP.Text = "";
                IP.ForeColor = Color.Black;
            }
        }

        private void IP_Leave(object sender, EventArgs e)    //（失去焦点）更改IP控件的颜色
        {
            if(IP.Text == "" && IP.ForeColor == Color.Black)
            {
                IP.Text = "192.168.X.X";
                IP.ForeColor = Color.Silver;
            }
        }

        private void Port_Enter(object sender, EventArgs e)    //（获取焦点）更改Port控件的颜色
        {
            if (Port.Text == "XXXXX" && Port.ForeColor == Color.Silver)
            {
                Port.Text = "";
                Port.ForeColor = Color.Black;
            }
        }

        private void Port_Leave(object sender, EventArgs e)    //（失去焦点）更改Port控件的颜色
        {
            if (Port.Text == "" && Port.ForeColor == Color.Black)
            {
                Port.Text = "XXXXX";
                Port.ForeColor = Color.Silver;
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
                IP.Enabled = false;         //禁用所有控件 防止误操作
                Port.Enabled = false;
                Enter.Enabled = false;
                CheckPortLabel.Text = "正在连接…";          //直接用CheckPortLabel作提示得了（
                CheckPortLabel.Left = this.ClientRectangle.Width / 2 - CheckPortLabel.Width / 2;
                CheckPortLabel.Top = CheckPortLabel.Top + 5;
                CheckPortLabel.ForeColor = Color.Black;
                Main main = new Main();
                var task1 = Task.Run(async delegate         //异步执行连接设备
                {
                    await Task.Delay(1000);     //延迟1秒
                    string[] output = main.Cmd("bin\\adb connect " + IP.Text + ":" + Port.Text, "wireless");     //执行连接
                    if (output[4].Substring(0, 6) == "cannot")      //判断连接失败
                    {
                        MessageBox.Show("无法连接到设备\n请检查是否已授权adb连接");
                        return "失败";
                    }
                    else
                    {
                        return "成功";
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
                    CheckPortLabel.Text = "只能输入数字";
                    CheckPortLabel.Left = Port.Left + Port.Width / 2 - CheckPortLabel.Width / 2;
                    CheckPortLabel.Top = CheckPortLabel.Top - 5;
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
            int max = 65534;
            if(Port.Text.Length != 0)
            {
                if(int.Parse(Port.Text) > max)
                {
                    Port.Text = max.ToString();
                    CheckPortLabel.Text = "端口超过上限";
                    CheckPortTimer.Enabled= true;
                    color = 36;
                }
            }
        }

    }
}
