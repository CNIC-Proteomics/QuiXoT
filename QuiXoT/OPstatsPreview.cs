using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using QuiXoT.math;

namespace QuiXoT
{

    public partial class OPstatsPreview : Form
    {

        private QuiXoT.statistics.statOptionsStrt options;
        bool okBtn=false;
        string previousFilter;
        string previousColXs;
        string previousColVs;
        bool colXsSelected = false;
        bool colVsSelected = false;
        bool colWpSelected = false;
        bool colWqSelected = false;

        LNquantitate.quantitationStrategy strategy;
  
        public OPstatsPreview(DataView _dvSampleConstants)
        {
            okBtn = false;
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


            InitializeComponent();
        }

        public void setStrategy(LNquantitate.quantitationStrategy dvStrategy)
        {
            strategy = dvStrategy;

            if (strategy == LNquantitate.quantitationStrategy.SILAC||strategy == LNquantitate.quantitationStrategy.SILAC_HR)
            {
                this.grpSilac.Visible = true;
                this.chkArgPro.Visible = true;
                this.lblSilacFDRq.Visible = true;
                this.txtSilacFdrq.Visible = true;
                this.txtSilacFdrq.Text = "0";
            }
            else { this.grpSilac.Visible = false; }
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
                        colWpcmb.Items.Add(col.ColumnName.ToString());
                        colWqcmb.Items.Add(col.ColumnName.ToString());                        
                    }
                }

            }
            catch (System.Exception a_Ex)
            {
                MessageBox.Show(a_Ex.Message);
            }
        }


        private void btnOk_Click(object sender, EventArgs e)
        {

           options.colXs = this.cmbXs.Text;                               
           options.colVs = this.cmbVs.Text;

           options.silacCorrection = false;

           options.forceX = this.forceX.Checked;
           if (this.forceX.Checked)
           {
               try
               {
                   options.forcedX = double.Parse(this.forceXtxt.Text.Trim());
               }
               catch 
               {
                   MessageBox.Show("Forced value for the super-mean is not a number!");
                   return;
               }
           }

            //ignore scans or peptides?
           options.ignScans = this.ignScans.Checked;
           if (this.ignScans.Checked)
           {
               if (!this.colWpSelected)
               {
                   MessageBox.Show("You have not selected a column for Wp (advanced options --> ignore scans)");
                   return;
               }
               double ignsc_s2p;
               bool tp = double.TryParse(this.ignoreS_s2ptxt.Text.Trim(),out ignsc_s2p);

               if (tp)
               {
                   options.ignScans_s2p = ignsc_s2p;
               }
               else 
               {
                   MessageBox.Show("Value for older sigma2P is not valid! (advanced options --> ignore scans)");
                   return;
               }


               options.colWp = this.colWpcmb.Text;
           }

           options.ignPeptides = this.ignPeptides.Checked;
           if (this.ignPeptides.Checked)
           {
               if (!this.colWqSelected)
               {
                   MessageBox.Show("You have not selected a column for Wq (advanced options)");
                   return;
               }
               options.colWq = this.colWqcmb.Text;
           }


           if (strategy == LNquantitate.quantitationStrategy.SILAC|| strategy==LNquantitate.quantitationStrategy.SILAC_HR)
           {
               options.silacCorrection = this.chkArgPro.Checked;
               try
               {
                   options.silacFDRq = double.Parse(this.txtSilacFdrq.Text.Trim());
               }
               catch
               {
                   MessageBox.Show("FDRq for SILAC Arg-->Pro correction is not a number!");
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

            options.filter = this.filterTxt.Text;


            bool okFi;
            double dVal=0;
  
            okFi = double.TryParse(this.txtF.Text, out dVal);
            options.n_efficiency = dVal;
            if (!okFi && options.efficiency)
            {
                MessageBox.Show("Efficiency parameter not correct.");
                return;
            }
            okFi = double.TryParse(this.txtFdr.Text, out dVal);
            options.n_FDR = dVal;
            if (!okFi && options.FDR)
            {
                MessageBox.Show("FDR parameter not correct.");
                return;
            }
            okFi = double.TryParse(this.txtWs.Text, out dVal);
            options.n_ws= dVal;
            if (!okFi && options.ws)
            {
                MessageBox.Show("Vs parameter not correct.");
                return;
            }
            

 
            okBtn = true;
            this.Dispose();


        }


        public QuiXoT.statistics.statOptionsStrt getSelectedOptions(out bool okPressed)
        {
            okPressed = okBtn;

            return options;
 
        }



        private void btnLoadPrevFilter_Click(object sender, EventArgs e)
        {
            this.filterTxt.Text = previousFilter;
        }
        private void btnLoadPrevCols_Click(object sender, EventArgs e)
        {
            for(int i=0;i<this.cmbXs.Items.Count;i++)
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

            string text=this.cmbXs.Text.ToUpper();

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



        private void chkPartialDig_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPartialDig.Checked)
            {

                //uncheck others
                chkPartialDigSubp.Checked = false;
                chkMSCwithSubPep.Checked = false;

                switch (filterTxt.Text.Trim())
                {
                    case "":
                        filterTxt.Text = "(st_PartialDig = 0 or st_PartialDig = 2)";
                        break;
                    default:
                        if (!filterTxt.Text.ToUpper().Contains("ST_PARTIALDIG"))
                        {
                            filterTxt.Text = filterTxt.Text + " and (st_PartialDig = 0 or st_PartialDig = 2)";
                        }
                        else
                        {
                            int start = filterTxt.Text.ToUpper().IndexOf("ST_PARTIALDIG");
                            if (start == -1) return;
                            int last = filterTxt.Text.ToUpper().IndexOf("AND", start+1);
                            if (last == -1) last = filterTxt.Text.Length;
                            filterTxt.Text = filterTxt.Text.Remove(start, last - start);

                            
                            if (filterTxt.Text == "")
                            {
                                filterTxt.Text = "(st_PartialDig = 0 or st_PartialDig = 2)";
                            }
                            else { filterTxt.Text = filterTxt.Text + " and (st_PartialDig = 0 or st_PartialDig = 2)"; }

                        }
                        break;
                }
            }
            else 
            {
                int start = filterTxt.Text.ToUpper().IndexOf("AND (ST_PARTIALDIG = 0 OR ST_PARTIALDIG = 2)");
                if (start == -1) start = filterTxt.Text.ToUpper().IndexOf("(ST_PARTIALDIG = 0 OR ST_PARTIALDIG = 2)");
                if (start == -1) return;
                int last = filterTxt.Text.ToUpper().IndexOf("AND", start+1);
                if (last == -1) last = filterTxt.Text.Length;

                filterTxt.Text = filterTxt.Text.Remove(start, last - start);
  
            }

            fixAndFirst();

        }
        private void chkPartialDigSubp_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPartialDigSubp.Checked)
            {
                //uncheck others
                chkPartialDig.Checked = false;
                chkMSCwithSubPep.Checked = false;


                switch (filterTxt.Text.Trim())
                {
                    case "":
                        filterTxt.Text = "st_PartialDig = 0";
                        break;
                    default:
                        if (!filterTxt.Text.ToUpper().Contains("ST_PARTIALDIG"))
                        {
                            filterTxt.Text = filterTxt.Text + " and st_PartialDig = 0";
                        }
                        else
                        {
                            int start = filterTxt.Text.ToUpper().IndexOf("ST_PARTIALDIG");
                            if (start == -1) return;
                            int last = filterTxt.Text.ToUpper().IndexOf("AND", start+1);
                            if (last == -1) last = filterTxt.Text.Length;
                            filterTxt.Text = filterTxt.Text.Remove(start, last - start);

                            if (filterTxt.Text == "")
                            {
                                filterTxt.Text = "st_PartialDig = 0";
                            }
                            else { filterTxt.Text = filterTxt.Text + " and st_PartialDig = 0"; }
                        }
                        break;
                }
            }
            else
            {
                int start = filterTxt.Text.ToUpper().IndexOf("AND ST_PARTIALDIG");
                if (start == -1) start = filterTxt.Text.ToUpper().IndexOf("ST_PARTIALDIG");
                if (start == -1) return;
                int last = filterTxt.Text.ToUpper().IndexOf("AND", start+1);
                if (last == -1) last = filterTxt.Text.Length;


                filterTxt.Text = filterTxt.Text.Remove(start, last - start);

            }

            fixAndFirst();

        }
        private void chkMSCwithSubPep_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMSCwithSubPep.Checked)
            {
                //uncheck others
                chkPartialDig.Checked = false;
                chkPartialDigSubp.Checked = false;
               
                switch (filterTxt.Text.Trim())
                {
                    case "":
                        filterTxt.Text = "st_PartialDig < 2";
                        break;
                    default:
                        if (!filterTxt.Text.ToUpper().Contains("ST_PARTIALDIG"))
                        {
                            filterTxt.Text = filterTxt.Text + " and st_PartialDig < 2";
                        }
                        else
                        {
                            int start = filterTxt.Text.ToUpper().IndexOf("AND ST_PARTIALDIG");
                            if (start == -1) start = filterTxt.Text.ToUpper().IndexOf("ST_PARTIALDIG");
                            if (start == -1) return;
                            int last = filterTxt.Text.ToUpper().IndexOf("AND", start+1);
                            if (last == -1) last = filterTxt.Text.Length;
                            filterTxt.Text = filterTxt.Text.Remove(start, last - start);

                            filterTxt.Text = filterTxt.Text + " and st_PartialDig < 2";

                        }
                        break;
                }
            }
            else
            {
                int start = filterTxt.Text.ToUpper().IndexOf("AND ST_PARTIALDIG");
                if (start == -1) start = filterTxt.Text.ToUpper().IndexOf("ST_PARTIALDIG");
                if (start == -1) return;
                int last = filterTxt.Text.ToUpper().IndexOf("AND", start+1);
                if (last == -1) last = filterTxt.Text.Length;

            
                filterTxt.Text = filterTxt.Text.Remove(start, last - start);

            }

            fixAndFirst();

        }
        
        
        
        private void chkMethionines_CheckedChanged(object sender, EventArgs e)
        {

            if (chkMethionines.Checked)
            {
                switch (filterTxt.Text.Trim())
                {
                    case "":
                        filterTxt.Text = "st_Meth = 0";
                        break;
                    default:
                        if (!filterTxt.Text.ToUpper().Contains("ST_METH"))
                        {
                            filterTxt.Text = filterTxt.Text + " and st_Meth = 0";
                        }
                        break;
                }
            }
            else 
            {

                int start = filterTxt.Text.ToUpper().IndexOf("AND ST_METH");
                if (start == -1) start = filterTxt.Text.ToUpper().IndexOf("ST_METH");
                if (start == -1) return;
                int last = filterTxt.Text.ToUpper().IndexOf("AND", start+1);
                if (last == -1) last = filterTxt.Text.Length;


                filterTxt.Text = filterTxt.Text.Remove(start, last - start);

                

            }

            fixAndFirst();

        }
        private void chkCTerminal_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCTerminal.Checked)
            {
                switch (filterTxt.Text.Trim())
                {
                    case "":
                        filterTxt.Text = "st_CTerm = 0";
                        break;
                    default:
                        if (!filterTxt.Text.ToUpper().Contains("ST_CTERM"))
                        {
                            filterTxt.Text = filterTxt.Text + " and st_Cterm = 0";
                        }
                        break;
                }
            }
            else
            {

                int start = filterTxt.Text.ToUpper().IndexOf("AND ST_CTERM");
                if (start == -1) start = filterTxt.Text.ToUpper().IndexOf("ST_CTERM");
                if (start == -1) return;
                int last = filterTxt.Text.ToUpper().IndexOf("AND", start + 1);
                if (last == -1) last = filterTxt.Text.Length;


                filterTxt.Text = filterTxt.Text.Remove(start, last - start);



            }

            fixAndFirst();


        }
        private void chkQuality_CheckedChanged(object sender, EventArgs e)
        {
            if (chkQuality.Checked)
            {
                switch (filterTxt.Text.Trim())
                {
                    case "":
                        filterTxt.Text = "numLabel1 > 0";
                        break;
                    default:
                        if (!filterTxt.Text.ToUpper().Contains("NUMLABEL1"))
                        {
                            filterTxt.Text = filterTxt.Text + " and numLabel1 > 0";
                        }
                        break;
                }
            }
            else
            {

                int start = filterTxt.Text.ToUpper().IndexOf("AND NUMLABEL1");
                if (start == -1) start = filterTxt.Text.ToUpper().IndexOf("NUMLABEL1");
                if (start == -1) return;
                int last = filterTxt.Text.ToUpper().IndexOf("AND", start + 1);
                if (last == -1) last = filterTxt.Text.Length;


                filterTxt.Text = filterTxt.Text.Remove(start, last - start);



            }

            fixAndFirst();



        }
        private void chkEfficiency_CheckedChanged(object sender, EventArgs e)
        {
            if (!txtF.Enabled)
            {
                txtF.Enabled = true;
            }
            else
            {
                string currFilter = this.filterTxt.Text.Trim();
                if (currFilter.ToUpper().Contains("Q_F"))
                {
                    int start = currFilter.ToUpper().IndexOf("AND Q_F");
                    if (start == -1) start = 0;
                    int end = currFilter.ToUpper().IndexOf("AND", start + 1);
                    if (end == -1) end = currFilter.Length;

                    try
                    {
                        currFilter = currFilter.Remove(start, end - start);
                    }
                    catch { }

                    this.filterTxt.Text = currFilter;

                }
                fixAndFirst();
                txtF.Text = "";
                txtF.Enabled = false;
            }
        }
        private void chkWs_CheckedChanged(object sender, EventArgs e)
        {

            string filSel = this.cmbVs.Text.Trim();

            if (!txtWs.Enabled)
            {
                string vsSelected = this.cmbVs.Text;
                if (!this.colVsSelected) 
                {
                    if (chkWs.Checked)
                    {
                        MessageBox.Show("You must select a column for Vs first!", "Error");
                        chkWs.Checked = false;
                    }
                    return;
                }
                txtWs.Enabled = true;
            }
            else
            {
                string currFilter = this.filterTxt.Text.Trim();
                if (currFilter.ToUpper().Contains(filSel.ToUpper()))
                {
                    int start = currFilter.ToUpper().IndexOf("AND " + filSel.ToUpper());
                    if (start == -1) start = 0;
                    int end = currFilter.ToUpper().IndexOf("AND", start + 1);
                    if (end == -1) end = currFilter.Length;

                    try
                    {
                        currFilter = currFilter.Remove(start, end - start);
                    }
                    catch { }

                    this.filterTxt.Text = currFilter;

                }
                fixAndFirst();
                txtWs.Text = "";
                txtWs.Enabled = false;
            }



        }
        private void chkFDR_CheckedChanged(object sender, EventArgs e)
        {

            string filSel = "FDR";

            if (!txtFdr.Enabled)
            {
                txtFdr.Enabled = true;
            }
            else
            {
                string currFilter = this.filterTxt.Text.Trim();
                if (currFilter.ToUpper().Contains(filSel.ToUpper()))
                {
                    int start = currFilter.ToUpper().IndexOf("AND " + filSel.ToUpper());
                    if (start == -1) start = 0;
                    int end = currFilter.ToUpper().IndexOf("AND", start + 1);
                    if (end == -1) end = currFilter.Length;

                    try
                    {
                        currFilter = currFilter.Remove(start, end - start);
                    }
                    catch { }

                    this.filterTxt.Text = currFilter;

                }
                fixAndFirst();
                txtFdr.Text = "";
                txtFdr.Enabled = false;
            }


        }

 
        private void txtF_TextChanged(object sender, EventArgs e)
        {
            string currFilter = this.filterTxt.Text.Trim();

            if (txtF.Text.Trim() == "")
            {
                return;
            }

            if (!currFilter.ToUpper().Contains("Q_F"))
            {
                currFilter += " and q_f > " + txtF.Text.Trim();
                this.filterTxt.Text = currFilter;
                fixAndFirst();
                return;
            }
            else 
            {
                int start = currFilter.ToUpper().IndexOf("Q_F");
                int signPos = currFilter.ToUpper().IndexOf(">", start + 1);

                int end = currFilter.ToUpper().IndexOf("AND", signPos + 1);
                if (end == -1) end = currFilter.Length;

                currFilter = currFilter.Remove(signPos+1, end - signPos-1);
                try
                {
                    currFilter = currFilter.Insert(signPos + 1, this.txtF.Text + " ");
                }
                catch 
                {
                    currFilter = currFilter + " " + this.txtF.Text;
                }

            }

            this.filterTxt.Text = currFilter;
            
            fixAndFirst();

        }
        private void txtWs_TextChanged(object sender, EventArgs e)
        {
            string currFilter = this.filterTxt.Text.Trim();
            string filSel = this.cmbVs.Text.Trim();

            if (txtWs.Text.Trim() == "")
            {
                return;
            }

            if (!currFilter.ToUpper().Contains(filSel.ToUpper()))
            {
                currFilter += " and " + filSel + " > " + txtWs.Text.Trim();
                this.filterTxt.Text = currFilter;
                fixAndFirst();
                return;
            }
            else
            {
                int start = currFilter.ToUpper().IndexOf(filSel.ToUpper());
                int signPos = currFilter.ToUpper().IndexOf(">", start + 1);

                int end = currFilter.ToUpper().IndexOf("AND", signPos + 1);
                if (end == -1) end = currFilter.Length;

                currFilter = currFilter.Remove(signPos + 1, end - signPos - 1);
                try
                {
                    currFilter = currFilter.Insert(signPos + 1, this.txtWs.Text + " ");
                }
                catch
                {
                    currFilter = currFilter + " " + this.txtWs.Text;
                }

            }

            this.filterTxt.Text = currFilter;

            fixAndFirst();

        }
        private void txtFdr_TextChanged(object sender, EventArgs e)
        {

            string currFilter = this.filterTxt.Text.Trim();
            string filSel = "FDR";

            if (txtFdr.Text.Trim() == "")
            {
                return;
            }

            if (!currFilter.ToUpper().Contains(filSel.ToUpper()))
            {
                currFilter += " and " + filSel + " < " + txtFdr.Text.Trim();
                this.filterTxt.Text = currFilter;
                fixAndFirst();
                return;
            }
            else
            {
                int start = currFilter.ToUpper().IndexOf(filSel.ToUpper());
                int signPos = currFilter.ToUpper().IndexOf("<", start + 1);

                int end = currFilter.ToUpper().IndexOf("AND", signPos + 1);
                if (end == -1) end = currFilter.Length;

                currFilter = currFilter.Remove(signPos + 1, end - signPos - 1);
                try
                {
                    currFilter = currFilter.Insert(signPos + 1, this.txtFdr.Text + " ");
                }
                catch
                {
                    currFilter = currFilter + " " + this.txtFdr.Text;
                }

            }

            this.filterTxt.Text = currFilter;

            fixAndFirst();

        }

        private void fixAndFirst()
        {
            if (this.filterTxt.Text.Trim().Length > 0)
            {
                try
                {
                    int fixAndFirst = this.filterTxt.Text.ToUpper().IndexOf("AND", 0, 5);
                    if (fixAndFirst > -1)
                    {
                        int end = this.filterTxt.Text.ToUpper().IndexOf(" ", 1);
                        this.filterTxt.Text = this.filterTxt.Text.Remove(0, end);
                    }
                }
                catch { }
            }
        }

        private void chkArgPro_CheckedChanged(object sender, EventArgs e)
        {
            this.txtSilacFdrq.Enabled = this.chkArgPro.Checked;
        }
        private void forceX_CheckedChanged(object sender, EventArgs e)
        {
            forceXtxt.Enabled = forceX.Checked;
        }
        private void ignScans_CheckedChanged(object sender, EventArgs e)
        {
            colWpcmb.Enabled = ignScans.Checked;
            ignoreS_s2plbl.Enabled = ignScans.Checked;
            ignoreS_s2ptxt.Enabled = ignScans.Checked;

        }
        private void ignPeptides_CheckedChanged(object sender, EventArgs e)
        {
            colWqcmb.Enabled = ignPeptides.Checked;
        }


        private void colWpcmb_TextChanged(object sender, EventArgs e)
        {
            string text = this.colWpcmb.Text.ToUpper();

            for (int i = 0; i < this.colWpcmb.Items.Count; i++)
            {
                string item = this.colWpcmb.Items[i].ToString().ToUpper();
                if (item == text)
                {
                    this.colWpcmb.SelectedIndex = i;
                    this.colWpSelected = true;
                    return;
                }
            }
            this.colWpSelected = false;

        }

        private void colWqcmb_TextChanged(object sender, EventArgs e)
        {
            string text = this.colWqcmb.Text.ToUpper();

            for (int i = 0; i < this.colWqcmb.Items.Count; i++)
            {
                string item = this.colWqcmb.Items[i].ToString().ToUpper();
                if (item == text)
                {
                    this.colWqcmb.SelectedIndex = i;
                    this.colWqSelected = true;
                    return;
                }
            }
            this.colWqSelected = false;

        }

     

 
 
 


       
    }



}