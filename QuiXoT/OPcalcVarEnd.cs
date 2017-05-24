using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QuiXoT
{
    public partial class OPcalcVarEnd : Form
    {

        public bool okPressed;

        public OPcalcVarEnd(QuiXoT.statistics.variancesStrt variances)
        {
            InitializeComponent();

            okPressed = false;

            this.txtValSigma2S.Text = variances.sigma2S.ToString("0.######");
            this.txtValSigma2P.Text = variances.sigma2P.ToString("0.######");
            this.txtValSigma2Q.Text = variances.sigma2Q.ToString("0.######");
            this.K.Text = variances.k.ToString("0.####");


            this.txtNsVal.Text = variances.Ns.ToString();
            this.txtNpVal.Text = variances.Np.ToString();
            this.txtNqVal.Text = variances.Nq.ToString();

            this.txtXVal.Text = variances.X.ToString("0.#####");

            this.txtValFs.Text = variances.Fs.ToString("0.#####");
            this.txtValFp.Text = variances.Fp.ToString("0.#####");
            this.txtValFq.Text = variances.Fq.ToString("0.#####");

            this.warningFs.Visible = !variances.Fscut;
            this.warningFp.Visible = !variances.Fpcut;
            this.warningFq.Visible = !variances.Fqcut;

            this.filter.Text = variances.filter;

        }

        private void btnOk_Click(object sender, EventArgs e)
        {

            okPressed = true;
            
            this.Dispose();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            okPressed = false;

            this.Dispose();

        }
    }
}
