using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QuiXoT
{
    public partial class OPcalcVarPreview : Form
    {

        private bool okPressed;
        private QuiXoT.statistics.statOptionsStrt options;
        private string previousColXs;
        private string previousColVs;
        private string previousFilter;
        private string Kconstant;
        private bool colXsSelected = false;
        private bool colVsSelected = false;
        
        public OPcalcVarPreview(DataView _dvSampleConstants)
        {
            okPressed = false;
                   
            try
            {
                previousFilter = _dvSampleConstants[0].Row["Filter"].ToString();
            }
            catch { previousFilter = ""; }

            try
            {
                previousColXs = _dvSampleConstants[0].Row["col_Xs"].ToString();
            }
            catch { previousColXs = ""; }

            try
            {
                previousColVs = _dvSampleConstants[0].Row["col_Vs"].ToString();
            }
            catch { previousColVs = ""; }

            try
            {
                Kconstant = _dvSampleConstants[0].Row["ct_k"].ToString();
            }
            catch { Kconstant = ""; }

            InitializeComponent();

            options.calSigmas = this.calSigmas.Checked;
            options.calSigmap = this.calSigmap.Checked;
            options.calSigmaq = this.calSigmaq.Checked;

            sigma2smin.Text = QuiXoT.Properties.Settings.Default.fits2smin.ToString();
            sigma2smax.Text = QuiXoT.Properties.Settings.Default.fits2smax.ToString();
            sigma2sDelta.Text = QuiXoT.Properties.Settings.Default.fits2sdelta.ToString();
            sigma2sCicles.Text = QuiXoT.Properties.Settings.Default.fits2scicles.ToString();
            sigma2pmin.Text = QuiXoT.Properties.Settings.Default.fits2pmin.ToString();
            sigma2pmax.Text = QuiXoT.Properties.Settings.Default.fits2pmax.ToString();
            sigma2pdelta.Text = QuiXoT.Properties.Settings.Default.fits2pdelta.ToString();
            sigma2pcicles.Text = QuiXoT.Properties.Settings.Default.fits2pcicles.ToString();
            sigma2qmin.Text=QuiXoT.Properties.Settings.Default.fits2qmin.ToString();
            sigma2qmax.Text=QuiXoT.Properties.Settings.Default.fits2qmax.ToString();
            sigma2qdelta.Text=QuiXoT.Properties.Settings.Default.fits2qdelta.ToString();
            sigma2qcicles.Text=QuiXoT.Properties.Settings.Default.fits2qcicles.ToString();
               
        }


        public void SetSourceColumns(DataColumnCollection Columns)
        {
            try
            {
                foreach (DataColumn col in Columns)
                {
                    if (col.DataType.IsValueType)
                    {
                        cmbXs.Items.Add(col.ColumnName.ToString());
                        cmbVs.Items.Add(col.ColumnName.ToString());
                    }
                }

            }
            catch (System.Exception a_Ex)
            {
                MessageBox.Show(a_Ex.Message);
            }
        }


        public QuiXoT.statistics.statOptionsStrt getSelectedOptions(out bool okPr)
        {
            okPr =okPressed;

            return options;

        }


        private void btnOk_Click(object sender, EventArgs e)
        {

            options.filter = this.filterTxt.Text.Trim();
            double vs_thres = 0;
            double wp_thres =0;
            double wq_thres = 0;
            double k = 0;

            options.colXs = this.cmbXs.Text;
            options.colVs = this.cmbVs.Text;

            if (!options.calSigmas)
            {
                double sigma_def = 0;
                bool trysigma = double.TryParse(this.sigmas.Text.Trim(), out sigma_def);
                options.sigmas_default = sigma_def;
                if (!trysigma)
                {
                    MessageBox.Show("Scan variance value is not valid! Check the advanced options.");
                    return;
                }
 
            }
            if (!options.calSigmap)
            {
                double sigma_def = 0;
                bool trysigma = double.TryParse(this.sigmap.Text.Trim(), out sigma_def);
                options.sigmap_default = sigma_def;
                if (!trysigma)
                {
                    MessageBox.Show("Peptide variance value is not valid! Check the advanced options.");
                    return;
                }

            }
            if (!options.calSigmaq)
            {
                double sigma_def = 0;
                bool trysigma = double.TryParse(this.sigmaq.Text.Trim(), out sigma_def);
                options.sigmaq_default = sigma_def;
                if (!trysigma)
                {
                    MessageBox.Show("Protein variance value is not valid! Check the advanced options.");
                    return;
                }

            }


            if (!this.colXsSelected)
            {
                MessageBox.Show("You have not selected the column for Xs values.");
                return;
            }

            if (!this.colVsSelected)
            {
                MessageBox.Show("You have not selected the column for Vs values.");
                return;
            }

            bool kOk = double.TryParse(this.kTxt.Text.Trim(), out k);

            if (!kOk)
            {
                MessageBox.Show("K constant is not a number! You must choose a value for k.", "Error");
                return;
            }

            options.k = k;

            
            if (this.vscheck.Checked)
            {
                bool vsOk = double.TryParse(this.vsTxt.Text.Trim(), out vs_thres);
                if (!vsOk)
                {
                    MessageBox.Show("Vs threshold is not a number!", "Error");
                    return;
                }
                options.vs_thres = vs_thres;
                
            }
            if (this.wpcheck.Checked)
            {
                bool wpOk = double.TryParse(this.wpTxt.Text.Trim(), out wp_thres);
                if (!wpOk)
                {
                    MessageBox.Show("Wp threshold is not a number!", "Error");
                    return;
                }
                options.wp_thres = wp_thres;
            }
            if (this.wqcheck.Checked)
            {
                bool wqOk = double.TryParse(this.wqTxt.Text.Trim(), out wq_thres);
                if (!wqOk)
                {
                    MessageBox.Show("Wq threshold is not a number!", "Error");
                    return;
                }
                options.wq_thres = wq_thres;
            }

            
            bool parseOk = false;
            double s2smin;
            double s2smax;
            double s2sdelta;
            int s2sCicles;
            double s2pmin;
            double s2pmax;
            double s2pdelta;
            int s2pCicles;
            double s2qmin;
            double s2qmax;
            double s2qdelta;
            int s2qCicles;


            parseOk=double.TryParse(sigma2smin.Text.Trim(), out s2smin);
            if (!parseOk)
            {
                MessageBox.Show("incorrect parameter!");
                return;
            }
            parseOk = double.TryParse(sigma2smax.Text.Trim(), out s2smax);
            if (!parseOk)
            {
                MessageBox.Show("incorrect parameter!");
                return;
            }
            parseOk = double.TryParse(sigma2sDelta.Text.Trim(), out s2sdelta);
            if (!parseOk)
            {
                MessageBox.Show("incorrect parameter!");
                return;
            }
            parseOk = int.TryParse(sigma2sCicles.Text.Trim(), out s2sCicles);
            if (!parseOk)
            {
                MessageBox.Show("incorrect parameter!");
                return;
            }
            parseOk = double.TryParse(sigma2pmin.Text.Trim(), out s2pmin);
            if (!parseOk)
            {
                MessageBox.Show("incorrect parameter!");
                return;
            }
            parseOk = double.TryParse(sigma2pmax.Text.Trim(), out s2pmax);
            if (!parseOk)
            {
                MessageBox.Show("incorrect parameter!");
                return;
            }
            parseOk = double.TryParse(sigma2pdelta.Text.Trim(), out s2pdelta);
            if (!parseOk)
            {
                MessageBox.Show("incorrect parameter!");
                return;
            }
            parseOk = int.TryParse(sigma2pcicles.Text.Trim(), out s2pCicles);
            if (!parseOk)
            {
                MessageBox.Show("incorrect parameter!");
                return;
            }
            parseOk = double.TryParse(sigma2qmin.Text.Trim(), out s2qmin);
            if (!parseOk)
            {
                MessageBox.Show("incorrect parameter!");
                return;
            }
            parseOk = double.TryParse(sigma2qmax.Text.Trim(), out s2qmax);
            if (!parseOk)
            {
                MessageBox.Show("incorrect parameter!");
                return;
            }
            parseOk = double.TryParse(sigma2qdelta.Text.Trim(), out s2qdelta);
            if (!parseOk)
            {
                MessageBox.Show("incorrect parameter!");
                return;
            }
            parseOk = int.TryParse(sigma2qcicles.Text.Trim(), out s2qCicles);
            if (!parseOk)
            {
                MessageBox.Show("incorrect parameter!");
                return;
            }


            options.s2smin = s2smin;
            options.s2smax = s2smax;
            options.s2sdelta = s2sdelta;
            options.s2sCicles = s2sCicles;
            options.s2pmin = s2pmin;
            options.s2pmax = s2pmax;
            options.s2pdelta = s2pdelta;
            options.s2pCicles = s2pCicles;
            options.s2qmin = s2qmin;
            options.s2qmax = s2qmax;
            options.s2qdelta = s2qdelta;
            options.s2qCicles = s2qCicles;
                        
            options.calVariances = true;
            okPressed = true;
          
            this.Dispose();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            okPressed = false;
            this.Dispose();
        }

        private void vscheck_CheckedChanged(object sender, EventArgs e)
        {
            this.vsTxt.Enabled = this.vscheck.Checked;
        }

        private void wpcheck_CheckedChanged(object sender, EventArgs e)
        {
            this.wpTxt.Enabled = this.wpcheck.Checked;
        }

        private void wqcheck_CheckedChanged(object sender, EventArgs e)
        {
            this.wqTxt.Enabled = this.wqcheck.Checked;
        }

        private void btnLoadPrevCols_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.cmbXs.Items.Count; i++)
            {
                string item = this.cmbXs.Items[i].ToString();
                if (item == previousColXs)
                {
                    this.cmbXs.SelectedIndex = i;
                    break;
                }
            }
            for (int i = 0; i < this.cmbVs.Items.Count; i++)
            {
                string item = this.cmbVs.Items[i].ToString();
                if (item == previousColVs)
                {
                    this.cmbVs.SelectedIndex = i;
                    break;
                }
            }


        }

        private void cmbXs_TextChanged(object sender, EventArgs e)
        {
            string text = this.cmbXs.Text.ToUpper();

            for (int i = 0; i < this.cmbXs.Items.Count; i++)
            {
                string item = this.cmbXs.Items[i].ToString().ToUpper();
                if (item == text)
                {
                    this.cmbXs.SelectedIndex = i;
                    this.colXsSelected = true;
                    return;
                }
            }
            this.colXsSelected = false;

        }

        private void cmbVs_TextChanged(object sender, EventArgs e)
        {
            string text = this.cmbVs.Text.ToUpper();

            for (int i = 0; i < this.cmbVs.Items.Count; i++)
            {
                string item = this.cmbVs.Items[i].ToString().ToUpper();
                if (item == text)
                {
                    this.cmbVs.SelectedIndex = i;
                    this.colVsSelected = true;
                    return;
                }
            }
            this.colVsSelected = false;

        }

    
 
        private void btnLoadPrevFilter_Click(object sender, EventArgs e)
        {
            this.filterTxt.Text = previousFilter;
            this.kTxt.Text = Kconstant;
        }


        private void calSigmas_CheckedChanged(object sender, EventArgs e)
        {
            options.calSigmas = calSigmas.Checked;
            this.lblsigmas.Enabled = !calSigmas.Checked;
            this.sigmas.Enabled = !calSigmas.Checked;
        }

        private void calSigmap_CheckedChanged(object sender, EventArgs e)
        {
            options.calSigmap = calSigmap.Checked;
            this.lblsigmap.Enabled = !calSigmap.Checked;
            this.sigmap.Enabled = !calSigmap.Checked;
        }

        private void calSigmaq_CheckedChanged(object sender, EventArgs e)
        {
            options.calSigmaq = calSigmaq.Checked;
            this.lblSigmaq.Enabled = !calSigmaq.Checked;
            this.sigmaq.Enabled = !calSigmaq.Checked;
        }

        private void saveDefault_Click(object sender, EventArgs e)
        {
            bool parseOk = false;
            double s2smin;
            double s2smax;
            double s2sdelta;
            int s2sCicles;
            double s2pmin;
            double s2pmax;
            double s2pdelta;
            int s2pCicles;
            double s2qmin;
            double s2qmax;
            double s2qdelta;
            int s2qCicles;


            parseOk=double.TryParse(sigma2smin.Text.Trim(), out s2smin);
            if (!parseOk) 
            {
                MessageBox.Show("incorrect parameter!");
                return;
            }
            parseOk=double.TryParse(sigma2smax.Text.Trim(), out s2smax);
            if (!parseOk)
            {
                MessageBox.Show("incorrect parameter!");
                return;
            }
            parseOk=double.TryParse(sigma2sDelta.Text.Trim(), out s2sdelta);
            if (!parseOk)
            {
                MessageBox.Show("incorrect parameter!");
                return;
            }
            parseOk=int.TryParse(sigma2sCicles.Text.Trim(), out s2sCicles);
            if (!parseOk)
            {
                MessageBox.Show("incorrect parameter!");
                return;
            }
            parseOk=double.TryParse(sigma2pmin.Text.Trim(), out s2pmin);
            if (!parseOk)
            {
                MessageBox.Show("incorrect parameter!");
                return;
            }
            parseOk=double.TryParse(sigma2pmax.Text.Trim(), out s2pmax);
            if (!parseOk)
            {
                MessageBox.Show("incorrect parameter!");
                return;
            }
            parseOk=double.TryParse(sigma2pdelta.Text.Trim(), out s2pdelta);
            if (!parseOk)
            {
                MessageBox.Show("incorrect parameter!");
                return;
            }
            parseOk=int.TryParse(sigma2pcicles.Text.Trim(), out s2pCicles);
            if (!parseOk)
            {
                MessageBox.Show("incorrect parameter!");
                return;
            }
            parseOk=double.TryParse(sigma2qmin.Text.Trim(), out s2qmin);
            if (!parseOk)
            {
                MessageBox.Show("incorrect parameter!");
                return;
            }
            parseOk=double.TryParse(sigma2qmax.Text.Trim(), out s2qmax);
            if (!parseOk)
            {
                MessageBox.Show("incorrect parameter!");
                return;
            }
            parseOk=double.TryParse(sigma2qdelta.Text.Trim(), out s2qdelta);
            if (!parseOk)
            {
                MessageBox.Show("incorrect parameter!");
                return;
            }
            parseOk=int.TryParse(sigma2qcicles.Text.Trim(), out s2qCicles);
            if (!parseOk)
            {
                MessageBox.Show("incorrect parameter!");
                return;
            }

            QuiXoT.Properties.Settings.Default.fits2smin = s2smin;
            QuiXoT.Properties.Settings.Default.fits2smax = s2smax;
            QuiXoT.Properties.Settings.Default.fits2sdelta = s2sdelta;
            QuiXoT.Properties.Settings.Default.fits2scicles = s2sCicles;
            QuiXoT.Properties.Settings.Default.fits2pmin = s2pmin;
            QuiXoT.Properties.Settings.Default.fits2pmax = s2pmax;
            QuiXoT.Properties.Settings.Default.fits2pdelta = s2pdelta;
            QuiXoT.Properties.Settings.Default.fits2pcicles = s2pCicles;
            QuiXoT.Properties.Settings.Default.fits2qmin = s2qmin;
            QuiXoT.Properties.Settings.Default.fits2qmax = s2qmax;
            QuiXoT.Properties.Settings.Default.fits2qdelta = s2qdelta;
            QuiXoT.Properties.Settings.Default.fits2qcicles = s2qCicles;


        }

 


    }
}
