﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Scrcpy_GUI
{
    public partial class Preview2 : Form
    {
        public int vWidth;

        public int vHeight;

        public Preview2()
        {
            InitializeComponent();
        }

        public void SetLabel()
        {
            label1.Top = this.ClientRectangle.Height / 2 - label1.Height / 2;
            label1.Left = this.ClientRectangle.Width / 2 - label1.Width / 2;
            button1.Top = label1.Bottom + 20;
            button1.Left = this.ClientRectangle.Width / 2 - button1.Width / 2;
        }
        public void SetForm(Size size)
        {
            this.Size = new Size(size.Width / 3 + vWidth, size.Height / 3 + vHeight);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Size = new Size(300, 600);
        }
        private void Preview2_Load(object sender, EventArgs e)
        {
            vHeight = this.Height - this.ClientRectangle.Height;
            vWidth = this.Width - this.ClientRectangle.Width;
            this.MinimumSize = new Size(vWidth + 100, vHeight + 100);
            this.MaximumSize = new Size(vWidth + 1365, vHeight + 1365);
        }
    }
}
