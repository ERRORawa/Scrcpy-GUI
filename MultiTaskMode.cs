﻿using System;
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
    public partial class MultiTaskMode : Form
    {
        public string device;

        public string command;
        public MultiTaskMode()
        {
            InitializeComponent();
        }

        private void Enter_Click(object sender, EventArgs e)
        {

        }

        private void MultiTaskMode_Load(object sender, EventArgs e)
        {
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
    }
}
