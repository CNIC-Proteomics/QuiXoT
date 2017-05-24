using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QuiXoT
{
    public partial class OPredPreference : Form
    {

        public bool OKpressed;
        public bool onlyFilteredData;
        public string txtPref;

        public OPredPreference()
        {
            InitializeComponent();
            OKpressed = false;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this.txtPrefer.Text.Trim() != "")
            {
                txtPref = this.txtPrefer.Text;
                OKpressed = true;
                this.Dispose();
            }
            else 
            { 
                OKpressed = false;
                MessageBox.Show("You have not written any text", "Error", MessageBoxButtons.OK);
            
            }
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            onlyFilteredData = checkBox1.Checked;

        }


    }
}
