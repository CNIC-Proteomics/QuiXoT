using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QuiXoT
{
    public partial class OPselectBoundsGraph : Form
    {
  
       public reqOPselectBounds selections;
       public bool btnOKpressed = false;


        public bool fixedXmin ;
        public bool fixedXmax ;
        public bool fixedYmin ;
        public bool fixedYmax ;
                         
        public double xLowLimit ;
        public double xUpLimit ;
        public double yLowLimit ;
        public double yUpLimit ;
                         
        public string xTickFormat;
        public string yTickFormat;
                         
        public int xMaxTicks;
        public int yMaxTicks;


        public struct reqOPselectBounds 
        {
            public double xLowLimit;
            public double xUpLimit;
            public double yLowLimit;
            public double yUpLimit;

            public string xTickFormat;
            public string yTickFormat;

            public int xMaxTicks;
            public int yMaxTicks;

            public bool fixedXmin;
            public bool fixedXmax;
            public bool fixedYmin;
            public bool fixedYmax;


        } 

        public OPselectBoundsGraph(reqOPselectBounds data)
        {            
            InitializeComponent();

            selections = data;

            this.txtXlowLimit.Text = selections.xLowLimit.ToString();
            this.txtXupLimit.Text = selections.xUpLimit.ToString();
            this.txtYlowLimit.Text = selections.yLowLimit.ToString();
            this.txtYupLimit.Text = selections.yUpLimit.ToString();
           
            this.txtXtick.Text = selections.xTickFormat.ToString();
            this.txtYtick.Text = selections.yTickFormat.ToString();

            this.txtMaxNumTicksX.Text = selections.xMaxTicks.ToString();
            this.txtMaxNumTicksY.Text = selections.yMaxTicks.ToString();
            
            this.rbXLowLimit.Checked = selections.fixedXmin;
            this.rbXupLimit.Checked = selections.fixedXmax;
            this.rbYlowLimit.Checked = selections.fixedYmin;
            this.rbYupLimit.Checked = selections.fixedYmax;
           
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

            try
            {
                fixedXmin = this.rbXLowLimit.Checked;
                fixedXmax = this.rbXupLimit.Checked;
                fixedYmin = this.rbYlowLimit.Checked;
                fixedYmax = this.rbYupLimit.Checked;

                xLowLimit = double.Parse(this.txtXlowLimit.Text);
                xUpLimit = double.Parse(this.txtXupLimit.Text);
                yLowLimit = double.Parse(this.txtYlowLimit.Text);
                yUpLimit = double.Parse(this.txtYupLimit.Text);

                xTickFormat = this.txtXtick.Text;
                yTickFormat = this.txtYtick.Text;

                xMaxTicks = int.Parse(this.txtMaxNumTicksX.Text);
                yMaxTicks = int.Parse(this.txtMaxNumTicksY.Text);    
                                
                btnOKpressed = true;
            }
            catch
            {
                btnOKpressed = false;
                MessageBox.Show("Bad selection of parameters.", "error", MessageBoxButtons.OK); 
            }

            this.Dispose();
        }

        private void XlowTxtChanged(object sender, EventArgs e)
        {
            this.rbXLowLimit.Checked = true;
        }

        private void YlowTxtChanged(object sender, EventArgs e)
        {
            this.rbYlowLimit.Checked = true;
        }

        private void XupTxtChanged(object sender, EventArgs e)
        {
            this.rbXupLimit.Checked = true;
        }

        private void YupTxtChanged(object sender, EventArgs e)
        {
            this.rbYupLimit.Checked = true;
        }



        #region keyPressForOK

        private void txtXlowLimit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.btnOK.PerformClick();
            }

        }

        private void txtXupLimit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.btnOK.PerformClick();
            }

        }

        private void txtXtick_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.btnOK.PerformClick();
            }

        }

        private void txtMaxNumTicksX_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.btnOK.PerformClick();
            }

        }

        private void txtYlowLimit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.btnOK.PerformClick();
            }

        }

        private void txtYupLimit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.btnOK.PerformClick();
            }

        }

        private void txtYtick_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.btnOK.PerformClick();
            }

        }

        private void txtMaxNumTicksY_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.btnOK.PerformClick();
            }

        }

        private void OPselectBoundsGraph_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.btnOK.PerformClick();
            }
        }
        #endregion

    }
}
