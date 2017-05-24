using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QuiXoT
{
    public partial class sleeve : Form
    {
        public sleeve()
        {
            InitializeComponent();

            timer1.Enabled = true;

            timer1.Tick += new EventHandler(timer1_Tick);
        }

        void timer1_Tick(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}