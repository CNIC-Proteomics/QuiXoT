using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QuiXoT
{
    public partial class OPcolor : Form
    {

        public byte red;
        public byte green;
        public byte blue;


        public OPcolor(byte oldRed, byte oldGreen, byte oldBlue)
        {
            InitializeComponent();

            red = oldRed;
            green = oldGreen;
            blue = oldBlue;

            txtRed.Text = red.ToString();
            txtGreen.Text = green.ToString();
            txtBlue.Text = blue.ToString();


        }

        private void txtRed_TextChanged(object sender, EventArgs e)
        {
            changeColor();
        }

        private void txtGreen_TextChanged(object sender, EventArgs e)
        {
            changeColor();
        }

        private void txtBlue_TextChanged(object sender, EventArgs e)
        {
            changeColor();

        }

        private void changeColor()
        {
            try
            {
                int test_red = int.Parse(this.txtRed.Text);
                int test_green = int.Parse(this.txtGreen.Text);
                int test_blue = int.Parse(this.txtBlue.Text);

                this.txtTest.BackColor = Color.FromArgb(test_red, test_green, test_blue);
            }
            catch { }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            try
            {
                red = byte.Parse(this.txtRed.Text);
                green = byte.Parse(this.txtGreen.Text);
                blue = byte.Parse(this.txtBlue.Text);
            }
            catch 
            { }

            this.Dispose();

        }

    }
}