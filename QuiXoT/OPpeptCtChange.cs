using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QuiXoT
{
    public partial class OPpeptCtChange : Form
    {

        public double k;
        public double sigma2S;
        public double sigma2P;
        public double sigma2Q;


        public OPpeptCtChange(double sigmaPr2, double Kp, double alpha)
        {
            InitializeComponent();



        }

        public OPpeptCtChange(  double kval, 
                                double sigma2Sval, 
                                double sigma2Pval, 
                                double sigma2Qval)
        {
            InitializeComponent();

            this.txtK.Text  = kval.ToString();
            this.txtSigma2S.Text = sigma2Sval.ToString();
            this.txtSigma2P.Text = sigma2Pval.ToString();
            this.txtSigma2Q.Text = sigma2Qval.ToString();

        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            try
            {                
                k = double.Parse(this.txtK.Text, System.Globalization.CultureInfo.InvariantCulture);
                sigma2S = double.Parse(this.txtSigma2S.Text, System.Globalization.CultureInfo.InvariantCulture);
                sigma2P = double.Parse(this.txtSigma2P.Text, System.Globalization.CultureInfo.InvariantCulture);
                sigma2Q = double.Parse(this.txtSigma2Q.Text, System.Globalization.CultureInfo.InvariantCulture); 
                
            }
            catch
            {
                MessageBox.Show("Invalid value(s)");
            }

            this.Dispose();
        }
    }
}