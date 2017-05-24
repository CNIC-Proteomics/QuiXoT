using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DataGridFilter;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Drawing.Drawing2D;
using System.Security.Cryptography;    
using System.Web;
using System.Linq;

using QuiXoT.math;
using QuiXoT.lookUp;
using QuiXoT.statistics;
using QuiXoT.DA_Raw;
using QuiXoT.DA_stackRAW;
using TimeSync;
using System.Threading;
using System.Data.Common;

namespace QuiXoT
{
    public partial class OPquan : Form, Iobserver
    {

        
        #region global variables of OPquan

        
        QuiXoT.DA_stackRAW.binStack[] stackIndex;
        QuiXoT.DA_stackRAW.binFrame[] StackFrames;
   
        isotList[][] isotopes;
        bool isotLoaded = false;
        AminoacidList[] aas;
        bool aminoListLoaded = false;
        private math.qMethodsSchema.Quanmethods quantitationMeths=new QuiXoT.math.qMethodsSchema.Quanmethods();
  
        private string methodChosen;
        private LNquantitate.quantitationStrategy qStrategy;

        //DA_raw DA_R = new DA_raw();
          
        double dK;
        double dSigma2S;
        double dSigma2P;
        double dSigma2Q;
        double dPhi;

        //About window
        AboutBox aboutForm = new AboutBox();
        
   
        //Graph window
        OPviewer viewer = new OPviewer();
        bool viewerShow = false;
                
        //Fit window
        bool fitwindowShow = false;
        OPfitWindow fitWindow = new OPfitWindow();

        //private System.Windows.Forms.Panel panelrecords;
        private System.Windows.Forms.DataGrid DataGridRecords;
        private DataColumnCollection TableColumnCollection;
        private DataTable TableFilterData;

        private DataSet DataSetRecords;
        private DataView ViewRecords;
        private string sortPreview="";
        
        private DataTable TableRecords;
        private CheckedListBox CheckedColumns;
        
        // OPquan size
        private int OPquanWidth;
        private int OPquanHeight;
        private int idfileTxtWidth;
        private int sortTxtWidth;
        private int filterTxtWidth;
        private int quantifPrBarWidth;
        private int panel1Width;
        private int panel1Height;
        private int idsGridWidth;
        private int idsGridHeight;
        private int barsPBoxWidth;

        private int barsPBoxMaxWidth;
        private int OPquanMinWidth;
        private int OPquanMinHeight;

        private bool resizingOPquan = false;

        //memory for filters
        bool memColFilter = false;
        
        public string OPquanFilter = "";
        public string OPgrapherFilter= "";

        #endregion
  
        public OPquan()
        {
            InitializeComponent();

            OPquanWidth = this.Width;
            OPquanHeight = this.Height;
            idfileTxtWidth = idfileTxt.Width;
            sortTxtWidth = sortTxt.Width;
            filterTxtWidth = filterTxt.Width;
            quantifPrBarWidth = quantifPrBar.Width;
            panel1Width = panel1.Width;
            panel1Height = panel1.Height;
            idsGridWidth = idsGrid.Width;
            idsGridHeight = idsGrid.Height;
            barsPBoxWidth = barsPBox.Width;

            barsPBoxMaxWidth = 250;

            OPquanMinHeight = 410;
            OPquanMinWidth = 900;

            this.Text = "QuiXoT v" + QuiXoT.Properties.Settings.Default.version.Trim();            
        }

        private void OPquan_Load(object sender, EventArgs e)
        {

            DataGridRecords=new DataGrid();
            idsGrid.CurrentCellChanged += new EventHandler(idsGrid_CurrentCellChanged);
            
            
            viewer.Disposed += new EventHandler(viewer_Disposed);
            fitWindow.Disposed += new EventHandler(fitWindow_Disposed);
     
            loadConfFiles();
        }

        #region load necessary files (isotopes, aminoacids and instr parameters)
        
        private void loadIsotopesXML()
        {
            try
            {

                isotopes = Isotopes.readXML(QuiXoT.Properties.Settings.Default.isotopesFile.Trim());
                isotLoaded = true;

                //Round isotopes mass  (only for low resolution spectra!) 
                for (int i = 0; i <= isotopes.GetUpperBound(0); i++)
                {
                    for (int j = 0; j <= isotopes[i].GetUpperBound(0); j++)
                    {
                        isotopes[i][j].DMass = Math.Round(isotopes[i][j].DMass, 0);
                    }
                }

                if (!this.checklist.Items.Contains((object)"Isotopes file"))
                {
                    this.checklist.Items.Add((object)"Isotopes file", true);
                }


            }
            catch
            {
                if (this.checklist.Items.Contains((object)"Isotopes file"))
                {
                    this.checklist.Items.Remove((object)"Isotopes file");
                }
                MessageBox.Show("Unable to load the isotopes XML file.");
                isotLoaded = false;
            }
        }
        private void loadaminoacidsXML()
        {
            try
            {
                //Read aminoacids list
                aas = AminoacidList.readXML(QuiXoT.Properties.Settings.Default.aminoacidsFile.Trim());
                aminoListLoaded = true;

                if (!this.checklist.Items.Contains((object)"Aminoacids file"))
                {
                    this.checklist.Items.Add((object)"Aminoacids file", true);
                }

            }
            catch
            {

                if (this.checklist.Items.Contains((object)"Aminoacids file"))
                {
                    this.checklist.Items.Remove((object)"Aminoacids file");
                }

                MessageBox.Show("Unable to load the aminoacids XML file");
            }
        }

        private void loadaminoacidsXML(string _methodChosen)
        {
            try
            {

                //get the aminoacids filename
                string aaFileName = LNquantitate.getAminoacidesFileName(_methodChosen, quantitationMeths);


                //Read aminoacids list
                aas = AminoacidList.readXML(aaFileName);
                aminoListLoaded = true;

                if (!this.checklist.Items.Contains((object)"Aminoacids file"))
                {
                    this.checklist.Items.Add((object)"Aminoacids file", true);
                }

            }
            catch
            {

                if (this.checklist.Items.Contains((object)"Aminoacids file"))
                {
                    this.checklist.Items.Remove((object)"Aminoacids file");
                }

                //MessageBox.Show("Unable to load the aminoacids XML file");
            }
        }


        private void loadQuanMethodsXML()
        {
            try
            {
                quantitationMeths = new QuiXoT.math.qMethodsSchema.Quanmethods();
                quantitationMeths.ReadXml(QuiXoT.Properties.Settings.Default.quanMethodsFile);

                if (!this.checklist.Items.Contains((object)"quan Methods file"))
                {
                    this.checklist.Items.Add((object)"quan Methods file", true);
                }
            }
            catch
            {
                if (this.checklist.Items.Contains((object)"quan Methods file"))
                {
                    this.checklist.Items.Remove((object)"quan Methods file");
                }
                
                MessageBox.Show("Unable to load the quantitation methods XML file");
                                
            }
        }


        private void reloadConfFilesBtn_Click(object sender, EventArgs e)
        {
           
            if (this.checklist.Items.Contains((object)"Isotopes file"))
            {
                this.checklist.Items.Remove((object)"Isotopes file");
            }
            if (this.checklist.Items.Contains((object)"Aminoacids file"))
            {
                this.checklist.Items.Remove((object)"Aminoacids file");
            }
            if (this.checklist.Items.Contains((object)"quan Methods file"))
            {
                this.checklist.Items.Remove((object)"quan Methods file");
            }

            Application.DoEvents();
            Thread.Sleep(300);

            loadConfFiles(methodChosen);


        }

        
        private void loadConfFiles()
        {
            loadIsotopesXML();
            //loadaminoacidsXML();
            loadQuanMethodsXML();            
        }


        private void loadConfFiles(string _methodChosen)
        {
            loadIsotopesXML();
            loadaminoacidsXML(_methodChosen);
            loadQuanMethodsXML();
        }


        #endregion
  

        #region viewers and graphics buttons
        void viewer_Disposed(object sender, EventArgs e)
        {
            viewerShow = false;
        }
        void fitWindow_Disposed(object sender, EventArgs e)
        {
            fitwindowShow = false;
        }
        private void graphBtn_Click(object sender, EventArgs e)
        {
            //this must be changed: spectrum type depends on the quantitationMethods.xml
            LNquantitate.spectrumType specType=LNquantitate.spectrumType.profile;
            switch (qStrategy)
            {
                case LNquantitate.quantitationStrategy.iTRAQ:
                    specType = LNquantitate.spectrumType.centroid;
                    break;
                default:
                    specType = LNquantitate.spectrumType.profile;
                    break;
            }

            viewer = null;

            double width = 0;
            try
            {
                width = double.Parse(ViewRecords[idsGrid.CurrentRowIndex].Row["q_Width"].ToString());
            }
            catch { }

            viewer = new OPviewer(specType, qStrategy, width, this.idfileTxt.Text);
            viewer.Show();
            viewer.Activate();
            viewer.Owner = this;
            viewerShow = true;
        
        }
        private void fitWindowBtn_Click(object sender, EventArgs e)
        {
            if (fitwindowShow == false)
            {
                fitwindowShow = true;
            }
            else
            {
                fitwindowShow = false;
            }


        }
        #endregion

        #region quantitation methods
        
        
        void idsGrid_CurrentCellChanged(object sender, EventArgs e)
        {

            try
            {
                //Write values at Stats group and Pep.Id. Group
                fillPropertyLabels(qStrategy);
           
                //Clean quantification bars
                switch(qStrategy)
                {
                    case LNquantitate.quantitationStrategy.O18_ZS :
                        drawBars(0, 0, 0, barsPBox);
                        break;
                    case LNquantitate.quantitationStrategy.O18_MSMS :
                        drawBars(0, 0, 0, barsPBox);
                        break;
                    case LNquantitate.quantitationStrategy.O18_HR:
                        drawBars(0, 0, 0, barsPBox);
                        break;
                    case LNquantitate.quantitationStrategy.SILAC_HR:
                        drawBars(0, 0, 0, barsPBox);
                        break;
                }
  
                if (!isotLoaded || !aminoListLoaded) return;
                
 
                //string rawPath = this.rawdirTxt.Text.Trim() + "\\" + ViewRecords[idsGrid.CurrentRowIndex].Row["RAWFileName"].ToString().Trim();
                int iScanNumber = int.Parse(ViewRecords[idsGrid.CurrentRowIndex].Row["spectrumIndex"].ToString());
                string rawFile = ViewRecords[idsGrid.CurrentRowIndex].Row["RAWFileName"].ToString().Trim();


                //Read the experimental data from the stack
                Comb.mzI[] data = null;
                string idFileXml = this.idfileTxt.Text.Trim();
                string framesPath = idFileXml.Substring(0, idFileXml.LastIndexOf(@"\")) + "\\binStack\\";
                try
                {
                    data = binStack.peakSpectrum(stackIndex, StackFrames, framesPath, rawFile, iScanNumber);
                }
                catch
                {
                    return;
                }


 
                IQuantitation _quantitator = null;

                //define the strategy to use
                switch (qStrategy)
                {
                    case LNquantitate.quantitationStrategy.O18_ZS:
                        _quantitator = new Q180ZS();
                        break;
                    case LNquantitate.quantitationStrategy.SILAC:
                        _quantitator = new QSilac();
                        break;
                    case LNquantitate.quantitationStrategy.iTRAQ:
                        _quantitator = new QiTraq();
                        break;
                    case LNquantitate.quantitationStrategy.O18_HR:
                        _quantitator = new Q18OHR();
                        break;
                    case LNquantitate.quantitationStrategy.SILAC_HR:
                        _quantitator = new QSilacHR();
                        break;
                    //case LNquantitate.quantitationStrategy.O18_MSMS:
                    //    _quantitator = new Q18OMSMS();
                    //    break;
                }

                if (_quantitator == null)
                    return;



                _quantitator.config(quantitationMeths, aas, isotopes, methodChosen);
                
                try
                {
    
 
                    //Calculate the isotopic envelope
                    Comb.mzI[] envData = _quantitator.getFittedSpectrum(ViewRecords[idsGrid.CurrentRowIndex].Row, data);
                    
                    //Draw quantification bars
                    try
                    {
                        double q_A = double.Parse(ViewRecords[idsGrid.CurrentRowIndex].Row["q_A"].ToString());
                        double q_B = double.Parse(ViewRecords[idsGrid.CurrentRowIndex].Row["q_B"].ToString());
                        double q_f = double.Parse(ViewRecords[idsGrid.CurrentRowIndex].Row["q_f"].ToString());

                        
                        drawBars(q_A, q_B, q_f, barsPBox);
                    }
                    catch { }

                    if (viewerShow && !viewer.IsDisposed)
                    {
                        viewer.Show();
                        viewer.Activate();
                        viewer.Owner = this;

                        double monoisotMass = _quantitator.getMonoisotMass(ViewRecords[idsGrid.CurrentRowIndex].Row);

                        string title = rawFile.Trim() + " -- " + iScanNumber.ToString().Trim();

                        viewer.flushGraph();
                        viewer.addGraph(0, data, monoisotMass);
                        viewer.addGraph(1, envData);
                        viewer.DrawGraph(0, title);
                        viewer.DrawGraph(1);
                    }
                }
                catch
                {

                    if (viewerShow && !viewer.IsDisposed)
                    {
                        viewer.Show();
                        viewer.Activate();
                        viewer.Owner = this;


                        string title = rawFile.Trim() + " -- " + iScanNumber.ToString().Trim();

                        double monoisotMass = _quantitator.getMonoisotMass(ViewRecords[idsGrid.CurrentRowIndex].Row);


                        viewer.flushGraph();
                        viewer.addGraph(0, data, monoisotMass);
                        viewer.DrawGraph(0, title);

                    }
                }
                

            

                this.Focus();
            }
            catch { }

        }

        private void fillPropertyLabels(LNquantitate.quantitationStrategy qStrategy)
        {

            

            string format = "0.####";

            DataView dvConstants = new DataView();
            dvConstants = DataSetRecords.Tables["IdentificationArchive"].DefaultView;


            try
            {
                double d = double.Parse(ViewRecords[idsGrid.CurrentRowIndex].Row["XC1D"].ToString());
                lblXcorr.Text = d.ToString(format);
                lblXcorr.Visible = true;
                titXcorr.Text = "Xcorr :";
            }
            catch
            {
                try
                {
                    double d = double.Parse(ViewRecords[idsGrid.CurrentRowIndex].Row["MascotScore"].ToString());
                    lblXcorr.Text = d.ToString(format);
                    lblXcorr.Visible = true;
                    titXcorr.Text = "score : ";
                }
                catch
                {
                    lblXcorr.Text = "value";
                    lblXcorr.Visible = false;
                }
            }

            try
            {
                double d = double.Parse(ViewRecords[idsGrid.CurrentRowIndex].Row["deltaCn"].ToString());
                lblDCn.Text = d.ToString(format);
                lblDCn.Visible = true;
                titDCn.Text = "DCn :";
            }
            catch
            {
                try
                {
                    double d = double.Parse(ViewRecords[idsGrid.CurrentRowIndex].Row["expect"].ToString());
                    lblDCn.Text = d.ToString(format);
                    lblDCn.Visible = true;
                    titDCn.Text = "expect :";
                }
                catch
                {
                    lblDCn.Text = "value";
                    lblDCn.Visible = false;
                }
            }

            try
            {
                double d = double.Parse(ViewRecords[idsGrid.CurrentRowIndex].Row["FDR"].ToString());
                lblFDR.Text = d.ToString(format);
                lblFDR.Visible = true;
                titFDR.Visible = true;
            }
            catch
            {
                lblFDR.Text = "value";
                lblFDR.Visible = false;
                titFDR.Visible = false;
            }
            try
            {
                double d = double.Parse(ViewRecords[idsGrid.CurrentRowIndex].Row["Vs"].ToString());
                lblVs.Text = d.ToString(format);
                lblVs.Visible = true;
            }
            catch
            {
                lblVs.Text = "value";
                lblVs.Visible = false;
            }
            try
            {
                if (qStrategy == LNquantitate.quantitationStrategy.SILAC || qStrategy == LNquantitate.quantitationStrategy.SILAC_HR)
                {
                    titF.Visible = false;
                    lblF.Visible = false;
                }
                else
                {
                    double d = double.Parse(ViewRecords[idsGrid.CurrentRowIndex].Row["q_f"].ToString());
                    lblF.Text = d.ToString(format);
                    lblF.Visible = true;
                    titF.Visible = true;
                }

            }
            catch
            {
                lblF.Text = "value";
                lblF.Visible = false;
                titF.Visible = false;
            }
            try
            {
                double d = double.Parse(ViewRecords[idsGrid.CurrentRowIndex].Row["Xs"].ToString());
                lblXs.Text = d.ToString(format);
                lblXs.Visible = true;
            }
            catch
            {
                try
                {
                    double d = double.Parse(ViewRecords[idsGrid.CurrentRowIndex].Row["q_log2Ratio"].ToString());
                    lblXs.Text = d.ToString(format);
                    lblXs.Visible = true;
                }
                catch
                {
                    lblXs.Text = "value";
                    lblXs.Visible = false;
                }
            }
            try
            {
                double d = double.Parse(ViewRecords[idsGrid.CurrentRowIndex].Row["Ws"].ToString());
                lblWs.Text = d.ToString(format);
                lblWs.Visible = true;
            }
            catch
            {
                lblWs.Text = "value";
                lblWs.Visible = false;
            }
            try
            {
                double d = double.Parse(ViewRecords[idsGrid.CurrentRowIndex].Row["Zs"].ToString());
                lblZs.Text = d.ToString(format);
                lblZs.Visible = true;
            }
            catch
            {
                lblZs.Text = "value";
                lblZs.Visible = false;
            }
            try
            {
                double d = double.Parse(ViewRecords[idsGrid.CurrentRowIndex].Row["FDRs"].ToString());
                lblFDRs.Text = d.ToString(format);
                lblFDRs.Visible = true;
            }
            catch
            {
                lblFDRs.Text = "value";
                lblFDRs.Visible = false;
            }
            try
            {
                double d = double.Parse(ViewRecords[idsGrid.CurrentRowIndex].Row["Xp"].ToString());
                lblXp.Text = d.ToString(format);
                lblXp.Visible = true;
            }
            catch
            {
                lblXp.Text = "value";
                lblXp.Visible = false;
            }
            try
            {
                double d = double.Parse(ViewRecords[idsGrid.CurrentRowIndex].Row["Wp"].ToString());
                lblWp.Text = d.ToString(format);
                lblWp.Visible = true;
            }
            catch
            {
                lblWp.Text = "value";
                lblWp.Visible = false;
            }
            try
            {
                double d = double.Parse(ViewRecords[idsGrid.CurrentRowIndex].Row["Zp"].ToString());
                lblZp.Text = d.ToString(format);
                lblZp.Visible = true;
            }
            catch
            {
                lblZp.Text = "value";
                lblZp.Visible = false;
            }
            try
            {
                double d = double.Parse(ViewRecords[idsGrid.CurrentRowIndex].Row["FDRp"].ToString());
                lblFDRp.Text = d.ToString(format);
                lblFDRp.Visible = true;
            }
            catch
            {
                lblFDRp.Text = "value";
                lblFDRp.Visible = false;
            }
            try
            {
                double d = double.Parse(ViewRecords[idsGrid.CurrentRowIndex].Row["Xq"].ToString());
                lblXq.Text = d.ToString(format);
                lblXq.Visible = true;
            }
            catch
            {
                lblXq.Text = "value";
                lblXq.Visible = false;
            }
            try
            {
                double d = double.Parse(ViewRecords[idsGrid.CurrentRowIndex].Row["Wq"].ToString());
                lblWq.Text = d.ToString(format);
                lblWq.Visible = true;
            }
            catch
            {
                lblWq.Text = "value";
                lblWq.Visible = false;
            }
            try
            {
                double d = double.Parse(ViewRecords[idsGrid.CurrentRowIndex].Row["Zq"].ToString());
                lblZq.Text = d.ToString(format);
                lblZq.Visible = true;
            }
            catch
            {
                lblZq.Text = "value";
                lblZq.Visible = false;
            }
            try
            {
                double d = double.Parse(ViewRecords[idsGrid.CurrentRowIndex].Row["FDRq"].ToString());
                lblFDRq.Text = d.ToString(format);
                lblFDRq.Visible = true;
            }
            catch
            {
                lblFDRq.Text = "value";
                lblFDRq.Visible = false;
            }
            try
            {
                double d = double.Parse(dvConstants[0].Row["X"].ToString());
                lblX.Text = d.ToString(format);
                lblX.Visible = true;
            }
            catch
            {
                lblX.Text = "value";
                lblX.Visible = false;
            }
            try
            {
                double d = double.Parse(dvConstants[0].Row["Ns"].ToString());
                lblNs.Text = d.ToString(format);
                lblNs.Visible = true;
            }
            catch
            {
                lblNs.Text = "value";
                lblNs.Visible = false;
            }
            try
            {
                double d = double.Parse(dvConstants[0].Row["Np"].ToString());
                lblNp.Text = d.ToString(format);
                lblNp.Visible = true;
            }
            catch
            {
                lblNp.Text = "value";
                lblNp.Visible = false;
            }
            try
            {
                double d = double.Parse(dvConstants[0].Row["Nq"].ToString());
                lblNq.Text = d.ToString(format);
                lblNq.Visible = true;
            }
            catch
            {
                lblNq.Text = "value";
                lblNq.Visible = false;
            }
        }

        private void NGbtn_Click(object sender, EventArgs e)
        {

            //this.DataGridRecords.AllowSorting = false;
            //this.idsGrid.AllowSorting = false;

            IQuantitation _quantitator = null;

            try
            {
                //OPImage inmaImages = new OPImage();
                //inmaImages.Show();
                //this.Focus();

                ArrayList alSelectedRows = new ArrayList();
                CurrencyManager cm = (CurrencyManager)this.BindingContext[idsGrid.DataSource,
                idsGrid.DataMember];
                DataView dv = (DataView)cm.List;                
              
                for (int i = 0; i < dv.Count; ++i)
                {
                    if (idsGrid.IsSelected(i))
                        alSelectedRows.Add(i);                    
                }

    
                
                //Check whether quantitation is possible.
                if (!isotLoaded || !aminoListLoaded || quantitationMeths == null)
                {
                    return; 
                }

                if (!(qStrategy == LNquantitate.quantitationStrategy.AveragineModel) && stackIndex == null)
                {
                    return;
                }
                

                //define the strategy to use
                switch (qStrategy)
                {
                    case LNquantitate.quantitationStrategy.O18_ZS:
                    _quantitator = new Q180ZS();
                    break;
                    case LNquantitate.quantitationStrategy.O18_HR:
                    _quantitator = new Q18OHR();
                    break;
                    case LNquantitate.quantitationStrategy.O18_MSMS:
                    _quantitator = new Q18OMSMS();
                    break;
                    case LNquantitate.quantitationStrategy.SILAC:
                    _quantitator = new QSilac();
                    break;
                    case LNquantitate.quantitationStrategy.SILAC_HR:
                    _quantitator = new QSilacHR();
                    break;
                    case LNquantitate.quantitationStrategy.iTRAQ:
                    _quantitator = new QiTraq();
                    break;
                    case LNquantitate.quantitationStrategy.AveragineModel:
                    _quantitator = new AveragineModel();
                    break;
                }

                if (_quantitator == null)
                    return;

                try
                {
                    _quantitator.config(quantitationMeths, aas, isotopes, methodChosen);
                    _quantitator.addDataTable(dv.Table);
                }
                catch (System.Exception excep) { 
                    MessageBox.Show(excep.Message);
                    return;
                }
                                
                this.quanPrtxt.Visible = true;
                this.quantifPrBar.Visible = true;
                this.quanPercenttxt.Visible = true;
                
                int iTotalRows = alSelectedRows.Count; //ViewRecords.Count;   
                int iRowsQuantified = 0;
                double Percent;
                int iPercent;

                foreach (int rowIndex in alSelectedRows) //ViewRecords.Table.Rows 
                {
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    Application.DoEvents();

                    Percent = ((double)iRowsQuantified / (double)iTotalRows) * 100;
                    iPercent = (int)(Math.Floor(Percent));
                    if (iPercent > 100) iPercent = 100;
                    this.quantifPrBar.Value = iPercent;
                    this.quanPercenttxt.Text = "(" + iPercent.ToString() + "%)";
                                        
                    
                    Comb.mzI[] data = null;
                    bool DAReadOK = false;

                    //Read the spectrum from scansbyRaw matrix
                    string rawFile = dv[rowIndex].Row["RAWFileName"].ToString().Trim();
                    int iScanNumber = int.Parse(dv[rowIndex].Row["spectrumIndex"].ToString()) ;

                    string idFileXml = this.idfileTxt.Text.Trim();
                    string framesPath = idFileXml.Substring(0, idFileXml.LastIndexOf(@"\")) + "\\binStack\\";

                    try
                    {
                        data = binStack.peakSpectrum(stackIndex, StackFrames, framesPath, rawFile, iScanNumber);
                        DAReadOK = true;
                    }
                    catch
                    {
                        DAReadOK = false;
                    }

                    if (qStrategy == LNquantitate.quantitationStrategy.AveragineModel)
                    {
                        data = null;
                        DAReadOK = true;
                    }

                    if (DAReadOK)  //If the read of the spectrum is OK, then quantify 
                    {
                        DataRow drow = dv[rowIndex].Row;
                        DataRow[] quanResult = _quantitator.quantitate(drow, data);


                        object[] quanResultArray = quanResult[0].ItemArray;
                        for (int i = 0; i <= quanResultArray.GetUpperBound(0); i++)
                        {
                            drow[i] = quanResultArray[i];
                        }

                        for (int i = 1; i < quanResult.Length; i++)
                        {
                            quanResultArray = quanResult[i].ItemArray;
                            dv.AddNew();
                            
                            
                            for(int j=0;j<quanResultArray.GetUpperBound(0); j++)
                            {
                                //preserve the primary key
                                if (dv[dv.Count - 1].Row[j] != dv[dv.Count - 1].Row["peptide_match_Id"])
                                {
                                    dv[dv.Count - 1].Row[j] = quanResultArray[j];
                                }
                            }
                         
                            //Primary key must be unique 
                            dv[dv.Count - 1].Row["peptide_match_Id"] = int.Parse(dv[dv.Count - 2].Row["peptide_match_Id"].ToString()) + 1;
                         
                        }

                    }              

                    fitwindowShow = false;

                    iRowsQuantified++;
                }

                

                this.quanPrtxt.Visible = false;
                this.quantifPrBar.Visible = false;
                this.quanPercenttxt.Visible = false;

                //dv.Sort = sortBeforeQuan;


                           

            }
            catch(Exception ex)
            {
                quanPercenttxt.Visible = false;
                quantifPrBar.Visible = false;
                quanPrtxt.Visible = false;

                MessageBox.Show("Error while quantification: " + ex.Message);
            }

            //this.DataGridRecords.AllowSorting = true;
            //this.idsGrid.AllowSorting = true;
            

        }
        
       

        #endregion

        #region statistics 
        private void btnVarCalc_Click(object sender, EventArgs e)
        {
            //Variance calculations must be done without any filter!!
            string filter = "";
            try
            {
                filter = OPquanFilter;
                reFilter("");
            }
            catch
            {
                MessageBox.Show("No experiment was loaded!", "Error");
                return;
            }

            //try
            //{
                this.statusLabel.Visible = true;
                this.statusLabel.Text = "Calculating general variances... wait, please.";


                Application.DoEvents();

                //DataSet ds = GetSelectedRowsDataSet(idsGrid);

                CurrencyManager cm = (CurrencyManager)this.BindingContext[idsGrid.DataSource,
                                      idsGrid.DataMember];

                DataView dv = (DataView)cm.List;

                DataView dvConstants = new DataView();
                dvConstants = DataSetRecords.Tables["IdentificationArchive"].DefaultView;

                OPcalcVarPreview calVarPreview = new OPcalcVarPreview(dvConstants);

                calVarPreview.SetSourceColumns(TableColumnCollection);

                calVarPreview.ShowDialog();
             
                bool okPr;
                QuiXoT.statistics.statOptionsStrt options = calVarPreview.getSelectedOptions(out okPr);


                if (okPr)
                {

                    Application.DoEvents();

                    QuiXoT.statistics.variancesStrt variances= new variancesStrt();
                    try
                    {
                        variances = Stats.calVariances(dv, options);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("problem calculating the variances! " + ex.Message);
                    }
                    
                    OPcalcVarEnd calcVarEnd = new OPcalcVarEnd(variances);
                    
                    calcVarEnd.ShowDialog();
                    bool changeVarValuesOK = calcVarEnd.okPressed;

                    if (changeVarValuesOK)
                    {

                        
                        DataView dvPeptCt = new DataView();

                        try
                        {
                            dvPeptCt = DataSetRecords.Tables["IdentificationArchive"].DefaultView;
                            dvPeptCt[0].Row["ct_sigma2S"] = variances.sigma2S;
                            dvPeptCt[0].Row["ct_sigma2P"] = variances.sigma2P;
                            dvPeptCt[0].Row["ct_sigma2Q"] = variances.sigma2Q;
                            dvPeptCt[0].Row["ct_k"] = variances.k;

                            dvPeptCt[0].Row["X_varCalc"] = variances.X;

                            dvPeptCt[0].Row["Ns_varCalc"] = variances.Ns;
                            dvPeptCt[0].Row["Np_varCalc"] = variances.Np;
                            dvPeptCt[0].Row["Nq_varCalc"] = variances.Nq;
                            
                            this.txtValSigma2S.Text = variances.sigma2S.ToString("0.####");
                            this.txtValSigma2P.Text = variances.sigma2P.ToString("0.####");
                            this.txtValSigma2Q.Text = variances.sigma2Q.ToString("0.####");
                            this.txtValK.Text = variances.k.ToString("0.####");

                            this.txtValSigma2S.Visible = true;
                            this.txtValSigma2P.Visible = true;
                            this.txtValSigma2Q.Visible = true;
                            this.txtValK.Visible = true;
                        }
                        catch 
                        {
                            this.txtValSigma2S.Text = "value";
                            this.txtValSigma2P.Text = "value";
                            this.txtValSigma2Q.Text = "value";
                            this.txtValK.Text = "value";

                            this.txtValSigma2S.Visible = false;
                            this.txtValSigma2P.Visible = false;
                            this.txtValSigma2Q.Visible = false;
                            this.txtValK.Visible = false;
                        }


                        try
                        {
                            //save the used filter and the superMean
                            dvConstants[0].Row["Filter"] = options.filter;
                            dvConstants[0].Row["col_Xs"] = options.colXs;
                            dvConstants[0].Row["col_Vs"] = options.colVs;
                        }
                        catch { }
                    }
                  

                }

                //turn to the original filter
                reFilter(filter);

            //}
            //catch (Exception ex)
            //{
            //   //turn to the original filter
            //    reFilter(filter);
            //    MessageBox.Show("Error in statistics. Maybe you have missed something... " + ex.Message);
            //}

            this.statusLabel.Text = "";
            this.statusLabel.Visible = false;
        }
        
        private void btnStats_Click(object sender, EventArgs e)
        {
            //Statistics must be done without any filter!!
            string filter="";
            try
            {
                filter = ViewRecords.RowFilter;
                reFilter("");
                
                //Sorting may delay the stats process... We don't use any previous sort!
                this.sortTxt.Text = "";
                this.btnSort.PerformClick();

            }
            catch 
            {
                MessageBox.Show("No experiment was loaded!", "Error");
                return;
            }


            try
            {
                
                
                Application.DoEvents();

                //DataSet ds = GetSelectedRowsDataSet(idsGrid);

                CurrencyManager cm = (CurrencyManager)this.BindingContext[idsGrid.DataSource,
                                      idsGrid.DataMember];

                DataView dv = (DataView)cm.List;
                DataView dvConstants = new DataView();
                dvConstants = DataSetRecords.Tables["IdentificationArchive"].DefaultView;


                OPstatsPreview statsPreview = new OPstatsPreview(dvConstants);

                statsPreview.setStrategy(qStrategy);

                statsPreview.SetSourceColumns(TableColumnCollection);
                

                statsPreview.ShowDialog();

                bool okPressed;
                QuiXoT.statistics.statOptionsStrt options = statsPreview.getSelectedOptions(out okPressed);
            
               
                if (okPressed)
                {
                    this.statusLabel.Visible = true;
                    this.statusLabel.Text = "Calculating means... wait, please.";

                    Application.DoEvents();

                    DataView dvTest = Stats.calStatistics(dv,dvConstants, options, qStrategy);

                    if (txtPhi.Visible) 
                    {
                        bool bPhi = double.TryParse(dvConstants[0].Row["Phi"].ToString(),out dPhi);
                        if (bPhi)
                        {
                            txtValPhi.Text = dPhi.ToString("#.####");
                        }
                    }

                    //save the used filter and the superMean
                    dvConstants[0].Row["Filter"] = options.filter;
                    dvConstants[0].Row["col_Xs"] = options.colXs;
                    dvConstants[0].Row["col_Vs"] = options.colVs;

                    //Filter by the filter used in statistics
                    reFilter(options.filter);

                    this.sortTxt.Text = "FDRq, Wq DESC, Wp DESC, Ws DESC";
                    this.btnSort.PerformClick();
                }

                
                
                
            }
            catch(Exception ex) 
            {
                //turn to the original filter
                reFilter(filter);
                MessageBox.Show("Error in statistics. Maybe you have missed something... " + ex.Message); 
            }

            this.statusLabel.Text = "";
            this.statusLabel.Visible = false;

        }
         
        private void stGraphBtn_Click(object sender, EventArgs e)
        {
            //QuiXoT.statistics.OPgraph statGraph = new OPgraph();
            //statGraph.Show();

            OPgrapherPreview prevGraph = new OPgrapherPreview();
            Application.DoEvents();
            //DataSet ds = GetSelectedRowsDataSet(idsGrid);
            CurrencyManager cm = (CurrencyManager)this.BindingContext[idsGrid.DataSource,
                                  idsGrid.DataMember];
            DataView dv = (DataView)cm.List;

            dgo dgo = new dgo(this.idsGrid);
            
            prevGraph.SetSourceColumns(TableColumnCollection);

            prevGraph.ShowDialog();

            string scolX;
            string scolY1;
            string scolY2;
            string scolY3;
            bool normGraph = false;
            bool sigmoidGraph = false;

            double[] extraDataForOPgrapher = new double[4];
            extraDataForOPgrapher[0] = dK;
            extraDataForOPgrapher[1] = dSigma2S;
            extraDataForOPgrapher[2] = dSigma2P;
            extraDataForOPgrapher[3] = dSigma2Q;

            try { scolX = prevGraph.colX.ToString();}
            catch { scolX = null; }
            try { scolY1 = prevGraph.colY1.ToString(); }
            catch { scolY1 = null; }
            try { scolY2 = prevGraph.colY2.ToString(); }
            catch { scolY2 = null; }
            try { scolY3 = prevGraph.colY3.ToString(); }
            catch { scolY3 = null; }
            try { normGraph = prevGraph.normalityZGraph; }
            catch { normGraph = false; }
            try { sigmoidGraph = prevGraph.sigmoidalGraph; }
            catch { sigmoidGraph = false; }

            if (prevGraph.okPressed)
            {
                OPgrapher graph1 = new OPgrapher(dv, scolX, scolY1, scolY2, scolY3, normGraph, sigmoidGraph, extraDataForOPgrapher);

                if(!normGraph)
                    dgo.attach(graph1);

                graph1.Owner = this;
                if (!graph1.IsDisposed)
                {
                    // it might be disposed if Zs has not been calculated,
                    // as in this case the window is violently closed,
                    // so that is why there is this "if"
                    graph1.Show();
                    graph1.OPgrapher_gotFocus(sender, e);
                    //graph1.Focus();
                }
            }

        }
        #endregion

        #region load identifications

        //load identifications' XML file
        private void loadIdfileBtn_Click(object sender, EventArgs e)
        {
            dPhi = 0;
            this.statusLabel.Text = "Loading QuiXML file. Please wait...";
            this.statusLabel.Visible = true;
            this.grpSampleData.Visible = false;

            dgo dgo = new dgo(this.idsGrid);
            dgoReset(dgo);

            if (!this.checklist.Items.Contains((object)"QuiXML file"))
            {

                activateSilac(false);
                       
                Application.DoEvents();

                OPselectMethod opSelMeth = new OPselectMethod(quantitationMeths);
                opSelMeth.Focus();
                opSelMeth.ShowDialog();
                opSelMeth.Focus();

                if (opSelMeth.method_id_name_chosen == null)
                {
                    statusLabel.Text = "No method chosen.";
                    return;
                }

                methodChosen = opSelMeth.method_id_name_chosen.Trim(); //opSelMeth.keyChosen;
                
                switch (opSelMeth.keyChosen)
                {
                    case "iTRAQ":
                        qStrategy = LNquantitate.quantitationStrategy.iTRAQ;
                        break;
                    case "O18_ZS":
                        qStrategy = LNquantitate.quantitationStrategy.O18_ZS;
                        break;
                    case "SILAC":
                        qStrategy = LNquantitate.quantitationStrategy.SILAC;
                        activateSilac(true);
                        break;
                    case "O18_MSMS":
                        qStrategy = LNquantitate.quantitationStrategy.O18_MSMS;
                        break;
                    case "O18_HR":
                        qStrategy = LNquantitate.quantitationStrategy.O18_HR;
                        break;
                    case "SILAC_HR":
                        qStrategy = LNquantitate.quantitationStrategy.SILAC_HR;
                        activateSilac(true);
                        break;
                    case "Averagine":
                        qStrategy = LNquantitate.quantitationStrategy.AveragineModel;
                        break;
                }
                
            }



            var query =
             from t1 in quantitationMeths.method
             where t1.Field<string>("method_id_name") == methodChosen
             select new { spectrum = t1.q_spectrum, 
                            schema = t1.associated_schema, 
                       spectrumPos = t1.q_spectrum_position};
            
            //var query = from table1 in DataSetRecords.Tables["peptide_match"].AsEnumerable()
            //            from table2 in DataSetRecords.Tables["rankings"].AsEnumerable()
            //            where table1.Field<int>("peptide_match_Id") == table2.Field<int>("peptide_match_Id")
            //            select new { id = table1.Field<int>("peptide_match_Id"), sequence = table1.Field<string>("Sequence"), ranking = table2.Field<double>("RnkXc1D") };
            ////select table2;

            string idSchema = "";
            string spectrumForQuan = "";
            string spectrumPosForQuan = "";

            foreach (var m in query)
            {
                idSchema = m.schema;
                spectrumForQuan = m.spectrum;
                spectrumPosForQuan = m.spectrumPos;
            }


            reloadConfFilesBtn_Click(null, null);

            Application.DoEvents();

            try
            {
                
                DataSetRecords = new DataSet("DataSetRecords");
              
                DataSetRecords.ReadXmlSchema(idSchema);
                DataSetRecords.ReadXml(idfileTxt.Text.Trim(), XmlReadMode.Auto);
             
                
                this.idsGrid.DataSource = null;
                this.idsGrid.DataSource = DataSetRecords.Tables["peptide_match"]; //scanData[]


                //Get the Table Column Collection
                TableColumnCollection = DataSetRecords.Tables["peptide_match"].Columns;

                ViewRecords = DataSetRecords.Tables["peptide_match"].DefaultView;
                ViewRecords.ListChanged += new ListChangedEventHandler(ViewRecords_ListChanged);  
               
              

                //GridStyle
                DataGridTableStyle GridStyle = new DataGridTableStyle();
                GridStyle.MappingName = DataSetRecords.Tables["peptide_match"].TableName;
                GridStyle.AlternatingBackColor = System.Drawing.Color.AliceBlue;
                GridStyle.GridLineColor = System.Drawing.Color.MediumSlateBlue;
                DataGridTextBoxColumn dgTextBcol = new DataGridTextBoxColumn();
                dgTextBcol.NullText = "";

                idsGrid.TableStyles.Add(GridStyle);

                
                //Bind the dataset to the datagrid
                idsGrid.SetDataBinding(ViewRecords, "");
                TableRecords = ViewRecords.Table;
                  
                
 
                //Prepare the data in the grid
                int Ns=0;
                int Np=0;
                int Nq=0;
                Stats.setDataGrid(ViewRecords, aas,out Ns,out Np,out Nq);
                try
                {
                    this.lblNscans.Text = Ns.ToString();
                    this.lblNpeptides.Text = Np.ToString();
                    this.lblNproteins.Text = Nq.ToString();

                    this.grpSampleData.Visible = true;


                }
                catch 
                {
                    this.grpSampleData.Visible = false;
                }

                if (!this.checklist.Items.Contains((object)"QuiXML file"))
                {
                    this.checklist.Items.Add((object)"QuiXML file", true);
                }


                
                //Check if any peptide constant is defined
                try
                {
                    
                    DataView dvPeptCt = new DataView();
                    dvPeptCt = DataSetRecords.Tables["IdentificationArchive"].DefaultView;
                    dK = (double)dvPeptCt[0].Row["ct_k"];
                    dSigma2S = (double)dvPeptCt[0].Row["ct_sigma2S"];
                    dSigma2P = (double)dvPeptCt[0].Row["ct_sigma2P"];
                    dSigma2Q = (double)dvPeptCt[0].Row["ct_sigma2Q"];
                    try 
                    { 
                        dPhi = (double)dvPeptCt[0].Row["Phi"];
                        this.txtValPhi.Text = dPhi.ToString("##.####");
                        activateSilac(true);
                    
                    }
                    catch { }

                    this.txtValK.Text = dK.ToString("##.####");
                    this.txtValSigma2S.Text = dSigma2S.ToString("##.####");
                    this.txtValSigma2P.Text = dSigma2P.ToString("##.####");
                    this.txtValSigma2Q.Text = dSigma2Q.ToString("##.####");

                    this.txtValK.Visible = true;
                    this.txtValSigma2S.Visible = true;
                    this.txtValSigma2P.Visible = true;
                    this.txtValSigma2Q.Visible = true;
                
        
                }
                catch 
                {

                    this.txtValK.Text = "value";
                    this.txtValSigma2S.Text = "value";
                    this.txtValSigma2P.Text = "value";
                    this.txtValSigma2Q.Text = "value";
                    this.txtValK.Visible = false;
                    this.txtValSigma2S.Visible = false;
                    this.txtValSigma2P.Visible = false;
                    this.txtValSigma2Q.Visible = false;

                }

                dgo.attach(this);


                //Check if a binary stack exists.
                string idFileXml = this.idfileTxt.Text.Trim();
                string stackIndexFolder = idFileXml.Substring(0, idFileXml.LastIndexOf(@"\")) + "\\binStack\\";
                string stackIndexFile = stackIndexFolder + "index.idx";

                if (File.Exists(stackIndexFile))
                {
                    loadStack();
                }
                else 
                {

                    MessageBox.Show("There is not any spectra binary stack created for this xml. You need it if you want to quantify (or see quantified spectra). Use the corresponding program to get the binStack (RawToBinStack, mgfToBinStack...)");


                    //OBSOLETE: the Xcalibur libraries are no more used in the QuiXoT: refer to RawToBinStack program

                    /*
                    DialogResult dlgRes = MessageBox.Show("There is not any spectra binary stack created for this xml. You need it if you want to quantify (or see quantified spectra). Do you want to create it now?", "Create a binary stack", MessageBoxButtons.YesNo);

                    if (dlgRes == DialogResult.Yes)
                    {
                        System.Windows.Forms.FolderBrowserDialog selBrowserDlg = new FolderBrowserDialog();

                        selBrowserDlg.ShowNewFolderButton = false;
                        selBrowserDlg.Description = "Choose the RAWs folder";
                        selBrowserDlg.ShowDialog();


                        bool hasRaws = false;

                        if (Directory.Exists(selBrowserDlg.SelectedPath))
                        {
                            string[] files = Directory.GetFiles(selBrowserDlg.SelectedPath);

                            foreach (string fil in files)
                            {
                                if (fil.Contains(".raw") || fil.Contains(".RAW"))
                                {
                                    hasRaws = true;
                                    break;
                                }
                            }
                        }

                        if (selBrowserDlg.SelectedPath != "" && hasRaws)
                        {
                            try
                            {
                                genStack(selBrowserDlg.SelectedPath.Trim(),spectrumForQuan,spectrumPosForQuan);
                                loadStack();
                            }
                            catch { }
                        }
                        else
                        {
                            MessageBox.Show("There is not any raw file in this folder.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("If you want to create a binary stack, you should reload the xml."); 
                    }

                    */
                                        
                }


            }
            catch 
            {
                dgoReset(dgo);
            }


            this.statusLabel.Text = "";
            this.statusLabel.Visible = false;
         
        }

        private void dgoReset(dgo dgo)
        {
            dgo.detach(this);

            this.txtValK.Text = "value";
            this.txtValSigma2S.Text = "value";
            this.txtValSigma2P.Text = "value";
            this.txtValSigma2Q.Text = "value";
            this.txtValK.Visible = false;
            this.txtValSigma2S.Visible = false;
            this.txtValSigma2P.Visible = false;
            this.txtValSigma2Q.Visible = false;


            this.idsGrid.DataSource = null;
            this.idsGrid.TableStyles.Clear();

            DataSetRecords = null;
            ViewRecords = null;
            TableColumnCollection = null;

            stackIndex = null;
            StackFrames = null;

            if (this.checklist.Items.Contains((object)"QuiXML file"))
            {
                this.checklist.Items.Remove((object)"QuiXML file");
            }
            if (this.checklist.Items.Contains((object)"stack index"))
            {
                this.checklist.Items.Remove((object)"stack index");
            }
        }

        private void activateSilac(bool _activate)
        {
            txtPhi.Visible = _activate;
            txtValPhi.Visible = _activate;
            if(txtValPhi.Text.Trim()=="value") txtValPhi.Text = "";
        }



        void ViewRecords_ListChanged(object sender, ListChangedEventArgs e)
        {

            if (ViewRecords.Sort.Trim() != sortPreview.Trim())
            {
                this.sortTxt.Text = ViewRecords.Sort.Trim();
                sortPreview = ViewRecords.Sort.Trim();
            }

            
            lblSpecCount.Text = ViewRecords.Count.ToString();

            //throw new NotImplementedException();
        }

         
        #endregion

        #region save dataGrid methods
        //write quantifications' XML file
        private void writeXMLBtn_Click(object sender, EventArgs e)
        {
            //Filename 
            
            string fileXMLids = this.idfileTxt.Text.Trim();
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileXMLids);
            string folderName = Path.GetDirectoryName(fileXMLids);
            string fileXMLwrite = string.Concat(folderName, Path.DirectorySeparatorChar, fileNameWithoutExtension, ".xml");

            if (File.Exists(fileXMLwrite))
            {
                // Find out if the user wants to replace the existing file
                System.Windows.Forms.DialogResult answer = MessageBox.Show(string.Concat("The XML quantification report exists already.",
                                                                       "Do you want to replace it?"),
                                                                    "Save XML quantification report",
                                                                    MessageBoxButtons.YesNo);

                // If the user wants to replace it
                if (answer == DialogResult.Yes)
                {
                    DataSetRecords.WriteXml(fileXMLwrite);
                }
            }
            else 
            {
                DataSetRecords.WriteXml(fileXMLwrite);
            }
                        

        }
        private void writeCSVbtn_Click(object sender, EventArgs e)
        {
            // Create the CSV file to which grid data will be exported.
            string idFileXml = this.idfileTxt.Text.Trim();
            string folderName = Path.GetDirectoryName(idFileXml);
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(idFileXml);
            string fileCSV = string.Concat(folderName, Path.DirectorySeparatorChar, fileNameWithoutExtension, ".xls");

            StreamWriter sw = new StreamWriter(fileCSV, false);


            DataTable dtPreview = DataSetRecords.Tables["IdentificationArchive"];
            int iPreviewValCount = dtPreview.Columns.Count;
            for (int i = 0; i < iPreviewValCount; i++)
            {
                sw.Write(dtPreview.Columns[i]);
                sw.Write(" : ");

                foreach (DataRow dr in dtPreview.Rows)
                {
                    sw.Write("\t");
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        sw.Write(dr[i].ToString());
                    }
                }
                sw.Write(sw.NewLine);

            }

            sw.Write(sw.NewLine);
            sw.Write(sw.NewLine);
            sw.Write(sw.NewLine);
            
            
            // First we will write the headers.
            DataTable dt = DataSetRecords.Tables["peptide_match"];
            int iColCount = dt.Columns.Count;
            for (int i = 0; i < iColCount; i++)
            {
                sw.Write(dt.Columns[i]);
                if (i < iColCount - 1)
                {
                    sw.Write("\t");
                }
            }
            sw.Write(sw.NewLine);
            // Now write all the rows.
            foreach (DataRow dr in dt.Rows)
            {
                for (int i = 0; i < iColCount; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        sw.Write(dr[i].ToString());
                    }
                    if (i < iColCount - 1)
                    {
                        sw.Write("\t");
                    }
                }
                sw.Write(sw.NewLine);            
            }

            dt = null;
            sw.Close();


        }        
        #endregion

        #region binary files
     
        private void loadStack()
        {
            this.statusLabel.Text = "Loading stack index, please wait...";
            this.statusLabel.Visible = true;

            Application.DoEvents();

            string idFileXml = this.idfileTxt.Text.Trim();
            string stackIndexFolder = idFileXml.Substring(0, idFileXml.LastIndexOf(@"\")) + "\\binStack\\";
            string stackIndexFile = stackIndexFolder + "index.idx";

            try
            {
                FileStream q = new FileStream(stackIndexFile, FileMode.Open, FileAccess.Read);
                BinaryFormatter b = new BinaryFormatter();
                stackIndex = (QuiXoT.DA_stackRAW.binStack[])b.Deserialize(q);
                q.Close();
                if (!this.checklist.Items.Contains((object)"stack index"))
                {
                    this.checklist.Items.Add((object)"stack index", true);
                }

                //calculate number of frames and prepare frames collector
                //int scbyframe = 100;
                //int inumFrames = binStack.countFrames(idFileXml, scbyframe);
                int inumFrames = binStack.countFramesFromIndex(stackIndex);

                StackFrames = new binFrame[inumFrames];

            }
            catch
            {
                if (this.checklist.Items.Contains((object)"stack index"))
                {
                    this.checklist.Items.Remove((object)"stack index");
                }
                MessageBox.Show("No stack index or corrupted.", "stack index not loaded");

            }

            this.statusLabel.Text = "";
            this.statusLabel.Visible = false;
        }

        //OBSOLETE: the Xcalibur libraries are no more used in the QuiXoT: refer to RawToBinStack program
        /* 
        private void genStack(string rawPath, string spectrumType, string spectrumPosition)
        {

            Application.DoEvents();

            try
            {
                this.statusLabel.Text = "Generating binaries stack... Creating index...";
                this.statusLabel.Visible = true;

                string idFileXml = this.idfileTxt.Text.Trim();
                string stackIndexFolder = idFileXml.Substring(0, idFileXml.LastIndexOf(@"\")) + "\\binStack\\";
                string stackIndexFile = stackIndexFolder + "index.idx";
                if (!Directory.Exists(stackIndexFolder))
                {
                    Directory.CreateDirectory(stackIndexFolder);
                }

                //scans by frame
                int scbyframe = 100;

                //calculate number of frames
                int inumFrames = binStack.countFrames(idFileXml, scbyframe);

                //MessageBox.Show(inumFrames.ToString());

                //generate and save index
                binStack[] stackIndex = binStack.genIndex(idFileXml, rawPath, scbyframe);

                FileStream q = new FileStream(stackIndexFile, FileMode.Create, FileAccess.Write);
                BinaryFormatter b = new BinaryFormatter();
                b.Serialize(q, stackIndex);
                q.Close();


                //generate and save frames
                for (int i = 1; i <= inumFrames; i++)
                {
                    Application.DoEvents();
                    this.statusLabel.Text = "Generating binaries stack... generating frame " + i.ToString() + "/" + inumFrames.ToString();
                    binFrame currFrame = binStack.genFrame(stackIndex, i, scbyframe, rawPath,spectrumType,spectrumPosition);
                    string frameFile = stackIndexFolder + currFrame.frame.ToString() + ".bfr";
                    FileStream qFr = new FileStream(frameFile, FileMode.Create, FileAccess.Write);
                    BinaryFormatter bFr = new BinaryFormatter();
                    bFr.Serialize(qFr, currFrame);
                    qFr.Close();
                }
            }
            catch
            {
                MessageBox.Show("Unable to generate binaries stack");
            }

            this.statusLabel.Text = "";
            this.statusLabel.Visible = false;

        }
        */

        #endregion
        
        #region Rows' and columns' tools
        
        private void ButtonColumnFilter_Click(object sender, EventArgs e)
        {
            string idFileXml = this.idfileTxt.Text.Trim();
            string path = idFileXml.Substring(0, idFileXml.LastIndexOf(@"\"))+"\\";

            ColumnFilterForm FilterForm = new ColumnFilterForm(path);

            //Set the Column Collection to the checklist object
            if (!memColFilter)
            {
                FilterForm.SetSourceColumns(TableColumnCollection);
                memColFilter = true;
            }
            else 
            {
                FilterForm.memFilteredColumns(TableColumnCollection,CheckedColumns);
            }

            FilterForm.ShowDialog();

            //Get the column check list
            CheckedColumns = FilterForm.GetSelectedColumns();

            //Set The mapping Data Grid Table Style according to the selected coulmns;
            SetTableBySelectedColumns(DataSetRecords.Tables["peptide_match"].TableName);

            idsGrid.SetDataBinding(ViewRecords, null);
            TableRecords = ViewRecords.Table;
		    
        }
        private void ButtonDataFilter_Click(object sender, EventArgs e)
        {
            DataFilterForm DataFilter = new DataFilterForm();

            //Set the Column Collection to the filter Table
            DataFilter.SetSourceColumns(TableColumnCollection);

            DataFilter.ShowDialog();

            //The TableFilterData Table contains the user restriction
            TableFilterData = DataFilter.GetFilterDataTable();

            SetTableByDataFilter();
        }
        private void SetTableBySelectedColumns(string TableMappingName)
        {


            DataGridTableStyle GridStyle = new DataGridTableStyle();

            DataGridColumnStyle TextBoxStyle; //Use for System.Boolean only
            DataGridColumnStyle BoolStyle; //Use for all Data Types which different form System.Boolean

            string ColumnDataType;  // hold the column Data Type 

            try
            {
                //clear the previous Table Styles
                idsGrid.TableStyles.Clear();
                GridStyle.MappingName = TableMappingName;

                foreach (DataColumn Column in TableColumnCollection)
                {
                    ColumnDataType = Column.DataType.ToString();

                    // The "CheckedColumns" contains the column which the user select to show 
                    // Column that not belong to the mapping will not show on the grid

                    if (CheckedColumns.CheckedItems.Contains(Column.ColumnName))
                    {
                        switch (ColumnDataType)
                        {
                            // The DataGrid Coulmn Style support two major column types: Bool and Text 
                            case ("System.Boolean"):
                                {
                                    BoolStyle = new DataGridBoolColumn();
                                    BoolStyle.HeaderText = Column.ColumnName;
                                    BoolStyle.MappingName = Column.ColumnName;
                                    BoolStyle.Width = 100;
                                    GridStyle.GridColumnStyles.Add(BoolStyle);
                                }
                                
                                break;

                            default:
                                {
                                    TextBoxStyle = new DataGridTextBoxColumn();
                                    TextBoxStyle.HeaderText = Column.ColumnName;
                                    TextBoxStyle.MappingName = Column.ColumnName;
                                    TextBoxStyle.Width = 100;
                                    // The NUllText attribute enable to replace the default null value for empty cells 
                                    TextBoxStyle.NullText = string.Empty;
                                    GridStyle.GridColumnStyles.Add(TextBoxStyle);
                                }
                                break;
                        }

                    }
                    else
                    {
                        switch (ColumnDataType)
                        {
                            // The DataGrid Coulmn Style support two major column types: Bool and Text 
                            case ("System.Boolean"):
                                {
                                    BoolStyle = new DataGridBoolColumn();
                                    BoolStyle.HeaderText = Column.ColumnName;
                                    BoolStyle.MappingName = Column.ColumnName;
                                    BoolStyle.Width = 0;
                                    GridStyle.GridColumnStyles.Add(BoolStyle);
                                }

                                break;

                            default:
                                {
                                    TextBoxStyle = new DataGridTextBoxColumn();
                                    TextBoxStyle.HeaderText = Column.ColumnName;
                                    TextBoxStyle.MappingName = Column.ColumnName;
                                    TextBoxStyle.Width = 0;
                                    // The NUllText attribute enable to replace the default null value for empty cells 
                                    TextBoxStyle.NullText = string.Empty;
                                    GridStyle.GridColumnStyles.Add(TextBoxStyle);
                                }
                                break;
                        }

                    }

                }

                //Set the Grid Style & Color
                GridStyle.AlternatingBackColor = System.Drawing.Color.AliceBlue;
                GridStyle.GridLineColor = System.Drawing.Color.MediumSlateBlue;
                idsGrid.TableStyles.Add(GridStyle);

            }
            catch (System.Exception a_Ex)
            {
                MessageBox.Show(a_Ex.Message);
            }

        }

        private void SetTableBySelectedColumns(string colsToHide, string TableMappingName)
        {

            char[] seps = new char[1];
            seps[0] = ',';

            string[] hideColumns = colsToHide.Trim().Split(seps);

            DataGridTableStyle GridStyle = new DataGridTableStyle();

            DataGridColumnStyle TextBoxStyle; //Use for System.Boolean only
            DataGridColumnStyle BoolStyle; //Use for all Data Types which different form System.Boolean

            string ColumnDataType;  // hold the column Data Type 

            try
            {
                //clear the previous Table Styles
                idsGrid.TableStyles.Clear();
                GridStyle.MappingName = TableMappingName;

                foreach (DataColumn Column in TableColumnCollection)
                {
                    ColumnDataType = Column.DataType.ToString();

                    // The "CheckedColumns" contains the column which the user select to show 
                    // Column that not belong to the mapping will not show on the grid

                    if (!hideColumns.Contains<string>(Column.ColumnName))
                    {
                        switch (ColumnDataType)
                        {
                            // The DataGrid Coulmn Style support two major column types: Bool and Text 
                            case ("System.Boolean"):
                                {
                                    BoolStyle = new DataGridBoolColumn();
                                    BoolStyle.HeaderText = Column.ColumnName;
                                    BoolStyle.MappingName = Column.ColumnName;
                                    BoolStyle.Width = 100;
                                    GridStyle.GridColumnStyles.Add(BoolStyle);
                                }

                                break;

                            default:
                                {
                                    TextBoxStyle = new DataGridTextBoxColumn();
                                    TextBoxStyle.HeaderText = Column.ColumnName;
                                    TextBoxStyle.MappingName = Column.ColumnName;
                                    TextBoxStyle.Width = 100;
                                    // The NUllText attribute enable to replace the default null value for empty cells 
                                    TextBoxStyle.NullText = string.Empty;
                                    GridStyle.GridColumnStyles.Add(TextBoxStyle);
                                }
                                break;
                        }

                    }
                    else
                    {
                        switch (ColumnDataType)
                        {
                            // The DataGrid Coulmn Style support two major column types: Bool and Text 
                            case ("System.Boolean"):
                                {
                                    BoolStyle = new DataGridBoolColumn();
                                    BoolStyle.HeaderText = Column.ColumnName;
                                    BoolStyle.MappingName = Column.ColumnName;
                                    BoolStyle.Width = 0;
                                    GridStyle.GridColumnStyles.Add(BoolStyle);
                                }

                                break;

                            default:
                                {
                                    TextBoxStyle = new DataGridTextBoxColumn();
                                    TextBoxStyle.HeaderText = Column.ColumnName;
                                    TextBoxStyle.MappingName = Column.ColumnName;
                                    TextBoxStyle.Width = 0;
                                    // The NUllText attribute enable to replace the default null value for empty cells 
                                    TextBoxStyle.NullText = string.Empty;
                                    GridStyle.GridColumnStyles.Add(TextBoxStyle);
                                }
                                break;
                        }

                    }

                }

                //Set the Grid Style & Color
                GridStyle.AlternatingBackColor = System.Drawing.Color.AliceBlue;
                GridStyle.GridLineColor = System.Drawing.Color.MediumSlateBlue;
                idsGrid.TableStyles.Add(GridStyle);

            }
            catch (System.Exception a_Ex)
            {
                MessageBox.Show(a_Ex.Message);
            }


        }
        
        
        private void SetTableByDataFilter()
        {


            ViewRecords = new DataView(DataSetRecords.Tables["peptide_match"]);

            try
            {
                // Build the RowFilter statement according to the user restriction 
                foreach (DataRow FilterRow in TableFilterData.Rows)
                {

                    if (FilterRow["Operation"].ToString() != string.Empty && FilterRow["ColumnData"].ToString() != string.Empty)
                    {
                        // Add the "AND" operator only from the second filter condition 
                        // The RowFilter get statement which simallar to the Where condition in sql query
                        // For example "GroupID = '6' AND GroupName LIKE 'A%' 
                        if (ViewRecords.RowFilter == string.Empty)
                        {
                            if(FilterRow["Operation"].ToString()!="LIKE")
                            {   
                                ViewRecords.RowFilter = FilterRow["ColumnName"].ToString() + " " + FilterRow["Operation"].ToString() + " '          " + FilterRow["ColumnData"].ToString().Trim() + "          ' ";
                            }
                            else
                            {
                                ViewRecords.RowFilter = FilterRow["ColumnName"].ToString() + " " + FilterRow["Operation"].ToString() + " '%" + FilterRow["ColumnData"].ToString().Trim() + "%' ";
                            }
                        
                        }   
                        else
                        {
                            if(FilterRow["Operation"].ToString()!="LIKE")
                            {
                                ViewRecords.RowFilter += " AND " + FilterRow["ColumnName"].ToString() + " " + FilterRow["Operation"].ToString() + " '          " + FilterRow["ColumnData"].ToString().Trim() + "          ' ";
                            }
                            else
                            {
                                ViewRecords.RowFilter += " AND " + FilterRow["ColumnName"].ToString() + " " + FilterRow["Operation"].ToString() + " '%" + FilterRow["ColumnData"].ToString().Trim() + "%' ";
                            }

                        }

                    }
                }



                if (ViewRecords.RowFilter != "")
                {
                    this.filterTxt.Text = ViewRecords.RowFilter.Trim();
                }
                else
                {
                    this.filterTxt.Text = "";
                }

                idsGrid.SetDataBinding(ViewRecords, "");
                TableRecords = ViewRecords.Table;
            }


            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);


                if (ViewRecords.RowFilter != "")
                {
                    this.filterTxt.Text = ViewRecords.RowFilter.Trim();
                }
                else
                {
                    this.filterTxt.Text = "";
                }

            }

        }



        public void filterBtn_Click(object sender, EventArgs e)
        {
            reFilter(filterTxt.Text.Trim());
        }

        private void reFilter(string filterToUse)
        {

            try
            {
                ViewRecords.RowFilter = bothFilters(filterToUse, OPgrapherFilter);
                idsGrid.SetDataBinding(ViewRecords, "");
                TableRecords = ViewRecords.Table;
                OPquanFilter = filterToUse;

                if (OPquanFilter != "")
                {
                    this.filterTxt.Text = OPquanFilter.Trim() + " ";
                    filterTxt_TextChanged(null, null);
                    //this.filterTxt.ForeColor = Color.BlueViolet;
                }
                else
                {
                    this.filterTxt.Text = "";
                    //this.filterTxt.ForeColor = Color.Red;
                }

                OPquanFilter = filterToUse;
                
            }
            catch
            {
                try
                {
                    ViewRecords.RowFilter = "";
                    idsGrid.SetDataBinding(ViewRecords, "");
                    TableRecords = ViewRecords.Table;


                if (ViewRecords.RowFilter != "")
                {
                    this.filterTxt.Text = ViewRecords.RowFilter.Trim();
                }

            }
            catch { }
                
            }
        }

        public string bothFilters(string filter1, string filter2)
        {
            if (filter1 == "")
                return filter2;
            else
            {
                if (filter2 == "")
                    return filter1;
                else
                    return filter1 + " AND " + filter2;
            }

        }

        private string fixAnds(string filter)
        {
            if (filter.Trim().Length > 0)
            {
                try
                {
                    int fixAndFirst = filter.ToUpper().IndexOf("AND", 0, 5);
                    if (fixAndFirst > -1)
                    {
                        int end = filter.ToUpper().IndexOf(" ", 1);
                        filter = filter.Remove(0, end);
                    }                    
                }
                catch { }

                try
                {
                    int start = filter.ToUpper().IndexOf("AND", filter.Length - 5);
                    if (start > -1)
                    {
                        int end = filter.Length;
                        filter = filter.Remove(start, end - start);
                    }
                }
                catch { }

            }
            return filter;
        }


        private void statFilterBtn_Click(object sender, EventArgs e)
        {

            try
            {
                DataView dvFilter = new DataView();
                dvFilter = DataSetRecords.Tables["IdentificationArchive"].DefaultView;
                string filterUsed = (string)dvFilter[0].Row["Filter"];

                reFilter(filterUsed);
            }
            catch { }

        }


        void idsGrid_DoubleClick(object sender, System.EventArgs e)
        {
            try
            {
                int iRows = ViewRecords.Count;
                for (int i = 0; i < iRows; i++)
                {
                    try
                    {
                        idsGrid.Select(i);

                    }
                    catch { }
                }
            }
            catch { }

        }
        public static ArrayList GetSelectedRowsArray(DataGrid aGrid)
        {
            ArrayList al = new ArrayList();
            CurrencyManager cm = (CurrencyManager)aGrid.BindingContext[aGrid.DataSource, aGrid.DataMember];
            DataView dv = (DataView)cm.List;
            for (int i = 0; i < dv.Count; ++i)
            {
                if (aGrid.IsSelected(i))
                    al.Add(i);
            }
            return al;
        }
        public static DataRow GetCurrentDataRow(DataGrid aGrid)
        // Gets Currently Selected Row in a Datagrid.
        {
            CurrencyManager xCM = (CurrencyManager)aGrid.BindingContext[aGrid.DataSource, aGrid.DataMember];
            DataRowView xDRV = (DataRowView)xCM.Current;
            return xDRV.Row;
        }

    
        
        #endregion
                
        private void drawBars(double A, double B, double f,PictureBox pb)
        {

            
            int width = pb.Width;
            int height = pb.Height;

            int barsWidth = (int)Math.Round((double)(20 * width) / 100,0);
            int spaceLeft = (int)Math.Round((double)(10 * width) / 100, 0);
            int spaceMiddle = (int)Math.Round((double)(10 * width) / 100, 0);
            int spaceBottom = (int)Math.Round((double)(10 * width) / 100, 0);

            float scaleX = 1.0F;

            int iA = (int)Math.Round(A, 0);
            int iB0 = (int)Math.Round(B * (1 - f) * (1 - f),0);
            int iB1 = (int)Math.Round(2 * B * f * (1 - f),0);
            int iB2 = (int)Math.Round(B * f * f,0);
            

            int maxY = Math.Max(iA, iB0 + iB1 + iB2);
           
            int iF = (int)Math.Round(f*maxY, 0);
            

            float scaleY = (float)((float)height / (float)maxY);

            // Get the graphics object
            Graphics gfx;
            gfx = pb.CreateGraphics();

            //clear bars
            gfx.Clear(pb.BackColor);

            Color colorA = Color.FromArgb(41, 145, 132);
            Color colorB0 = Color.FromArgb(255, 0, 0);
            Color colorB1 = Color.FromArgb(216, 206, 47);
            Color colorB2 = Color.FromArgb(109, 64, 144);
            Color goodeff = Color.FromArgb(0, 200, 50);
            Color badeff = Color.FromArgb(255, 0, 0);
            
            Brush brushA = new SolidBrush(colorA);
            Brush brushB0 = new SolidBrush(colorB0);
            Brush brushB1 = new SolidBrush(colorB1);
            Brush brushB2 = new SolidBrush(colorB2);
            Brush brushGoodeff = new SolidBrush(goodeff);
            Brush brushBadeff = new SolidBrush(badeff);

            Rectangle rectA = new Rectangle(spaceLeft + 2*(barsWidth+spaceMiddle),0,barsWidth,iA);
            Rectangle rectB0 = new Rectangle(spaceLeft + barsWidth+spaceMiddle,0,barsWidth,iB0);
            Rectangle rectB1 = new Rectangle(spaceLeft + barsWidth+spaceMiddle,iB0, barsWidth, iB1);
            Rectangle rectB2 = new Rectangle(spaceLeft + barsWidth+spaceMiddle,iB0+iB1, barsWidth, iB2);
            Rectangle rectF = new Rectangle(spaceLeft, 0, barsWidth, iF);

            gfx.RotateTransform(180);
            gfx.TranslateTransform(width, height, System.Drawing.Drawing2D.MatrixOrder.Append);
            try
            {
                gfx.ScaleTransform(scaleX, scaleY, System.Drawing.Drawing2D.MatrixOrder.Prepend);
            }
            catch { }
                           
            gfx.FillRectangle(brushA, rectA);
            gfx.FillRectangle(brushB0, rectB0);
            gfx.FillRectangle(brushB1, rectB1);
            gfx.FillRectangle(brushB2, rectB2);

            if (f <= 0.6)
            {
                gfx.FillRectangle(brushBadeff, rectF);
            }
            else 
            {
                gfx.FillRectangle(brushGoodeff, rectF);
            }


            gfx.Dispose();
            
        }
       
        private void pictureBox1_Click(object sender, EventArgs e)
        {

            aboutForm = null;
            aboutForm = new AboutBox();
            aboutForm.Show();
            aboutForm.Activate();
            aboutForm.Owner = this;
                                    
        }

        private void btnDeployRed_Click(object sender, EventArgs e)
        {

            try
            {
                               
                this.statusLabel.Visible = true;
                this.statusLabel.Text = "Deploying redundances... wait, please.";


                System.Windows.Forms.DialogResult answer = MessageBox.Show(string.Concat("Deploying redundances may cause a  statistically significant headache.",
                                                       "Do you really want to deploy them?"),
                                                    "Deploy of redundances",
                                                    MessageBoxButtons.YesNo);

                // If the user wants to do it
                if (answer == DialogResult.Yes)
                {
                    Application.DoEvents();
                    //DataSet ds = GetSelectedRowsDataSet(idsGrid);
                    CurrencyManager cm = (CurrencyManager)this.BindingContext[idsGrid.DataSource,
                                          idsGrid.DataMember];
                    DataView dv = (DataView)cm.List;
                    DataView dvDeployed = Stats.DeployRedundances(dv);
                }
                
            }
            catch
            {
                MessageBox.Show("Error in deployment.");
            }

            this.statusLabel.Text = "";
            this.statusLabel.Visible = false;

        }

        private void lnPeptCts_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            bool dKOk = double.TryParse(this.txtValK.Text,out dK);
            bool dSigma2SOk = double.TryParse(this.txtValSigma2S.Text, out dSigma2S);
            bool dSigma2POk = double.TryParse(this.txtValSigma2P.Text, out dSigma2P);
            bool dSigma2QOk = double.TryParse(this.txtValSigma2Q.Text, out dSigma2Q);

            if (!dKOk)
            {
                dK = 0;
            }
            if (!dSigma2SOk)
            {
                dSigma2S = 0;
            }
            if (!dSigma2POk)
            {
                dSigma2P = 0;
            } 
            if (!dSigma2QOk)
            {
                dSigma2Q = 0;
            }


            OPpeptCtChange pepChange = new OPpeptCtChange(dK, dSigma2S, dSigma2P, dSigma2Q);
            pepChange.ShowDialog();
                       
            dK = pepChange.k;
            dSigma2S = pepChange.sigma2S;
            dSigma2P = pepChange.sigma2P;
            dSigma2Q = pepChange.sigma2Q;

            DataView dvPeptCt = new DataView();
 
            try
            {

                dvPeptCt = DataSetRecords.Tables["IdentificationArchive"].DefaultView;
                dvPeptCt[0].Row["ct_k"] = pepChange.k;
                dvPeptCt[0].Row["ct_sigma2S"] = pepChange.sigma2S;
                dvPeptCt[0].Row["ct_sigma2P"] = pepChange.sigma2P;
                dvPeptCt[0].Row["ct_sigma2Q"] = pepChange.sigma2Q;

                double sigma2Stmp = pepChange.sigma2S;
                double sigma2Ptmp = pepChange.sigma2P;
                double sigma2Qtmp = pepChange.sigma2Q;

                this.txtValK.Text = dK.ToString("##.####");
                this.txtValSigma2S.Text = sigma2Stmp.ToString("##.####");
                this.txtValSigma2P.Text = sigma2Ptmp.ToString("##.####");
                this.txtValSigma2Q.Text = sigma2Qtmp.ToString("##.####");

                this.txtValK.Visible = true;
                this.txtValSigma2S.Visible = true;
                this.txtValSigma2P.Visible = true;
                this.txtValSigma2Q.Visible = true;
                
            }
            catch
            {
                this.txtValK.Text = "value";
                this.txtValSigma2S.Text = "value";
                this.txtValSigma2P.Text = "value";
                this.txtValSigma2Q.Text = "value";

                this.txtValK.Visible = false;
                this.txtValSigma2S.Visible = false;
                this.txtValSigma2P.Visible = false;
                this.txtValSigma2Q.Visible = false;

            }
            
        }

        private void btnChangeColVal_Click(object sender, EventArgs e)
        {
            //Prevent sort problems!
            string prevSort = this.ViewRecords.Sort;
            this.ViewRecords.Sort = "";

            OPchangeColV changeCol = new OPchangeColV();

            Application.DoEvents();
            //DataSet ds = GetSelectedRowsDataSet(idsGrid);
            CurrencyManager cm = (CurrencyManager)this.BindingContext[idsGrid.DataSource,
                                  idsGrid.DataMember];
            DataView dv = (DataView)cm.List;

            changeCol.SetSourceColumns(TableColumnCollection);

            changeCol.ShowDialog();

            if (!changeCol.okChange)
            {
                this.ViewRecords.Sort = prevSort;
                return;
            }
            


            #region Formula
            double[] valPosition; 
            
            if (changeCol.valIsAOperation)
            {
               valPosition = new double[changeCol.position.Length];



                for (int iRows = 0; iRows < dv.Count; iRows++)
                {

                    int numOp = 0;

                    try
                    {
                    
                        double op = 0;

                        foreach (string pos in changeCol.position)
                        {
                           
                            if (changeCol.alVariables.Contains(pos.Trim()))
                            {
                                //en los cálculos, pasar el valor double.Parse(row[pos.Trim()])
                                valPosition[numOp] =double.Parse(dv[iRows].Row[pos.Trim()].ToString());
                                numOp++;
                            }
                            else
                            {
                                try
                                {
                                    double val = double.Parse(pos.Trim());
                                    valPosition[numOp] = val;
                                    numOp++;
                                }
                                catch { }
                            }
                        }


                
                        bool firstCharIsOp = (changeCol.value.Substring(1, 1).IndexOfAny(changeCol.operations) != -1);
                        int myPos = 0;

                        switch (firstCharIsOp)
                        {
                            case true:
                                op = 0;
                                myPos = 0;
                                while (myPos <= valPosition.GetUpperBound(0))
                                {
                                    op = changeCol.operate(op, valPosition[myPos], (string)changeCol.alOperators[myPos]);
                                    myPos++;
                                }

                                break;
                            case false:
                                op = valPosition[0];
                                myPos = 1;
                                while (myPos <= valPosition.GetUpperBound(0))
                                {
                                    op = changeCol.operate(op, valPosition[myPos], (string)changeCol.alOperators[myPos - 1]);
                                    myPos++;
                                }
                                break;
                        }


                        dv[iRows].Row[changeCol.selectedColumn] =op;

                    }
                    catch { }

                }
            }
            #endregion


            if (changeCol.changeOpt == changeOption.newValue && !changeCol.valIsAOperation)
            {
                try
                {

                    if (changeCol.value.ToLower().Contains("rank"))
                    {
                        //Here the data must be sorted up!
                        this.ViewRecords.Sort = prevSort;


                        for (int iRows = 0; iRows < dv.Count; iRows++)
                        {
                            dv[iRows].Row[changeCol.selectedColumn] = iRows + 1;
                        }

                    }
                    else
                    {

                        //Type colType = dv[0].Row[changeCol.selectedColumn].GetType();
                        for (int iRows = 0; iRows < dv.Count; iRows++)
                        {
                            dv[iRows].Row[changeCol.selectedColumn] = changeCol.value;
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Value type is not valid for this column... have you typed a string for a numeric value?");
                }
            }

            if (changeCol.changeOpt == changeOption.copyValues)
            {

                try
                {
                    Type targetDataType = dv.Table.Columns[changeCol.selectedColumn].DataType;
                    if (!targetDataType.IsValueType)
                        MessageBox.Show("Target column is not numeric!!");
                            
                    for (int iRows = 0; iRows < dv.Count; iRows++)
                    {
                        if(!changeCol.log)
                            dv[iRows].Row[changeCol.selectedColumn] = dv[iRows].Row[changeCol.copyFromColumn];
                        if (changeCol.log)
                        {
                            dv[iRows].Row[changeCol.selectedColumn] = (double)Math.Log((double)dv[iRows].Row[changeCol.copyFromColumn], changeCol.logbase);
                        }
                    }
                }
                catch 
                {
                    MessageBox.Show("Source and column types mismatch... Are you sure you are not trying to copy string values into a numeric column?");
                }
            }

            if (changeCol.changeOpt == changeOption.concatenateFields)
            {
                try
                {
                    Type targetDataType = dv.Table.Columns[changeCol.selectedColumn].DataType;
                    if (!(targetDataType == typeof(string)))
                    {
                        MessageBox.Show("Target column does not accept text!!");
                        return;
                    }

                    for (int iRows = 0; iRows < dv.Count; iRows++)
                    {
                        string concatenatedString = "";
                        for (int i = 0; i < changeCol.concatColumns.Length; i++)
                            concatenatedString += dv[iRows].Row[changeCol.concatColumns[i]].ToString();

                        dv[iRows].Row[changeCol.selectedColumn] = concatenatedString;
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Problem while concatenating fields." +
                        "\nError was: " + ex.Message.ToString());
                }
            }

            if (changeCol.changeOpt == changeOption.calculateDMass)
            {
                try 
                {
                    char[] symbols = changeCol.symbols.ToCharArray();
                    double label=0;


                    if (qStrategy == LNquantitate.quantitationStrategy.SILAC || qStrategy== LNquantitate.quantitationStrategy.SILAC_HR)
                    {
                        var query =
                           from meth in quantitationMeths.method
                           join instr in quantitationMeths.instrument
                           on meth.method_Id equals instr.method_Id
                           join inFit in quantitationMeths.initialFitParams
                           on instr.instrument_Id equals inFit.instrument_Id
                           join param in quantitationMeths.if_parameter
                           on inFit.initialFitParams_Id equals param.initialFitParams_Id
                           where meth.method_id_name == methodChosen
                           select new
                           {
                               paramId = param.IsidNull() ? "no id" : param.id,
                               paramString = param.Is_stringNull() ? "no string" : param._string,
                               paramValue = param.IsvalueNull() ? Double.NaN : param.value
                           };

                        foreach (var m in query)
                        {
                            if (m.paramId == "labeled amino acids")
                            {
                                label = m.paramValue;
                            }
                        }
                    }

                    if (qStrategy == LNquantitate.quantitationStrategy.O18_HR || qStrategy == LNquantitate.quantitationStrategy.O18_ZS)
                    {
                        var query =
                          from meth in quantitationMeths.method
                          join instr in quantitationMeths.instrument
                          on meth.method_Id equals instr.method_Id
                          join inFit in quantitationMeths.initialFitParams
                          on instr.instrument_Id equals inFit.instrument_Id
                          join param in quantitationMeths.if_parameter
                          on inFit.initialFitParams_Id equals param.initialFitParams_Id
                          where meth.method_id_name == methodChosen
                          select new
                          {
                              paramId = param.IsidNull() ? "no id" : param.id,
                              paramString = param.Is_stringNull() ? "no string" : param._string,
                              paramValue = param.IsvalueNull() ? Double.NaN : param.value
                          };

                        foreach (var m in query)
                        {
                            if (m.paramId == "deltaR") { label = m.paramValue * 2.0; }
                        }
                    }

                    for (int iRows = 0; iRows < dv.Count; iRows++)
                    {
                        string seq = dv[iRows]["Sequence"].ToString();
                        int labelCounter = LNquantitate.countAminoacides(seq,symbols);
                        double precMass = (double)dv[iRows].Row["PrecursorMass"];
                        double pepMass = (double)dv[iRows].Row["q_peptide_Mass"];
                        double massCorrection = 0;

                        if (precMass > 2100)
                        {
                            massCorrection = -1.0;
                        }
                        if (precMass > 4000)
                        {
                            massCorrection = -2.0;
                        }
                        dv[iRows].Row[changeCol.selectedColumn] = precMass - pepMass - massCorrection - label * labelCounter;
                    }
                }
                catch { }
            }

            this.ViewRecords.Sort = prevSort;
        }

        private void btnLookForIdXml_Click(object sender, EventArgs e)
        {

            
            System.Windows.Forms.OpenFileDialog selFileDlg = new OpenFileDialog();
            selFileDlg.Filter = "XML files (*.xml)|*.xml";
            selFileDlg.InitialDirectory = @"c:\";
            selFileDlg.Title = "Select a XML identifications file";
            selFileDlg.RestoreDirectory = true;
            
            selFileDlg.ShowDialog(this);

            if (selFileDlg.ShowDialog() == DialogResult.OK)
            {
                idfileTxt.Text = selFileDlg.FileName;
                this.loadIdfileBtn.PerformClick();
               
            }
            
        }

        private void OPquan_DragDrop(object sender, DragEventArgs e)
        {
        
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            idfileTxt.Text = files[0].ToString().Trim();
            this.loadIdfileBtn.PerformClick();

        }

        private void OPquan_DragEnter(object sender, DragEventArgs e)
        {
            // make sure they're actually dropping files (not text or anything else)
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
            {
                // make sure the file is a xml file and is unique.
                // (without this, the cursor stays a "NO" symbol)
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.GetUpperBound(0) == 0)
                {
                    string fileExtension = Path.GetExtension(files[0]);
                    if (fileExtension == ".xml") e.Effect = DragDropEffects.All;
                }
                
            }
        }



        private void idsGrid_MouseClick(object sender, MouseEventArgs e)
        {
            MenuItem miChangeCol = new MenuItem();
            MenuItem miHide = new MenuItem();
            MenuItem miRedPref = new MenuItem();
            
            miChangeCol.Text = "change order";
            miHide.Text = "hide";
            miRedPref.Text = "make a redundance preference";
            
            ContextMenu colMenu = new ContextMenu();
            //colMenu.MenuItems.Add(miChangeCol);
            colMenu.MenuItems.Add(miHide);
            colMenu.MenuItems.Add(miRedPref);
            
            miHide.Click += new EventHandler(miHide_Click);
            miRedPref.Click += new EventHandler(miRedPref_Click);
            System.Drawing.Point pt = new System.Drawing.Point(e.X, e.Y);
            DataGrid.HitTestInfo hti = this.idsGrid.HitTest(pt);
            
            // If right mouse button clicked
            if (e.Button == MouseButtons.Right)
            {
                if (hti.Type == DataGrid.HitTestType.ColumnHeader)
                {
                    this.idsGrid.ContextMenu = colMenu;
                    //DataGridTableStyle gridStyle = idsGrid.TableStyles["peptide_match"];
                    ////DataGridTableStyle gridStyle = dataGrid1.TableStyles["Customers"];
                    //string colHeadName = gridStyle.GridColumnStyles[hti.Column].MappingName.ToString();
                    ////dcStyle = gridStyle.GridColumnStyles[hti.Column];

                    //////Get the column check list
                    ////CheckedColumns = FilterForm.GetSelectedColumns();

                    ////Set The mapping Data Grid Table Style according to the selected coulmns;
                    //SetTableBySelectedCoulmns(DataSetRecords.Tables["peptide_match"].TableName);

                    //idsGrid.SetDataBinding(ViewRecords, null);
                    //TableRecords = ViewRecords.Table;
                }
            }


        }

        void miRedPref_Click(object sender, EventArgs e)
        {

            OPredPreference dlgRedPref = new OPredPreference();
            dlgRedPref.ShowDialog();

            if (dlgRedPref.OKpressed)
            {
                        
                //DataSet ds = GetSelectedRowsDataSet(idsGrid);

                CurrencyManager cm = (CurrencyManager)this.BindingContext[idsGrid.DataSource,
                                      idsGrid.DataMember];

                DataView dv = (DataView)cm.List;
                DataView dvResult;

                switch(dlgRedPref.onlyFilteredData)
                {
                    case true:
                         dvResult = QuiXoT.statistics.Stats.choosePreferentRedundance(dv,dlgRedPref.txtPref,true);
                    break;
                    case false:
                         dvResult = QuiXoT.statistics.Stats.choosePreferentRedundance(dv, dlgRedPref.txtPref,false);
                    break;
                }


                string message = "Preferential string " + dlgRedPref.txtPref + " was selected."; 
                MessageBox.Show(message,"Done",MessageBoxButtons.OK);
                            

            }

            //throw new NotImplementedException();
        }

        void miHide_Click(object sender, EventArgs e)
        {



            //throw new Exception("The method or operation is not implemented.");
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            try 
            {
                string sortMode = this.sortTxt.Text;

                if (sortMode != "")
                {
                    ViewRecords.Sort = sortMode;
                    sortTxt_TextChanged(null, null);
                }

            }
            catch 
            {
            }
        }

        private void OPquan_DoubleClick(object sender, EventArgs e)
        {
            //try
            //{
            //    byte red = this.backcolor.r;
            //    byte green = this.backcolor.g;
            //    byte blue = this.backcolor.b;

            //    opcolor dlgcolor = new opcolor(red, green, blue);
            //    dlgcolor.showdialog();

            //    this.backcolor = color.fromargb((int)dlgcolor.red, (int)dlgcolor.green, (int)dlgcolor.blue);
            //    checklist.backcolor = color.fromargb((int)dlgcolor.red, (int)dlgcolor.green, (int)dlgcolor.blue);
            //}
            //catch { }
            
        }

        public void filterTxt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.filterTxt.Text.Trim() == OPquanFilter.Trim())
                {
                    if (OPgrapherFilter == "")
                        this.filterTxt.ForeColor = Color.BlueViolet;
                    else
                        this.filterTxt.ForeColor = Color.Green;
                }
                else { this.filterTxt.ForeColor = Color.Red; }
            }
            catch { }
        }

        private void chkShowStats_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                switch (chkShowStats.Checked)
                {
                    case true:
                        if (OPquanFilter.Trim() != "")
                        {
                            OPquanFilter += " AND st_excluded='' ";

                        }
                        else
                        {
                            OPquanFilter += " st_excluded='' ";

                        }

                        ViewRecords.RowFilter = bothFilters(OPquanFilter, OPgrapherFilter);
                        break;
                    case false:
                        string[] operators = new string[6];
                        operators[0] = "AND";
                        operators[1] = "and";
                        operators[2] = "OR";
                        operators[3] = "or";
                        operators[4] = "NOT";
                        operators[5] = "not";

                         string stFilter = (string)OPquanFilter.Clone();
                         if ((OPquanFilter.Trim().Contains("AND st_excluded=''")))
                         {
                            string[] sRemove=new string[1];
                            sRemove[0]="AND st_excluded=''";
                            string[] sVar=stFilter.Split(sRemove,StringSplitOptions.RemoveEmptyEntries);
                            stFilter="";
                            for(int i=0;i<=sVar.GetUpperBound(0);i++)
                            {
                                stFilter+=sVar[i];
                            }
                         }
                         if ((stFilter.Contains("st_excluded=''")))
                         {
                            string[] sRemove=new string[1];
                            sRemove[0]="st_excluded=''";
                            string[] sVar=stFilter.Split(sRemove,StringSplitOptions.RemoveEmptyEntries);
                            stFilter="";
                            for(int i=0;i<=sVar.GetUpperBound(0);i++)
                            {
                                stFilter+=sVar[i];
                            }
                         }
                        
                        //remove a operator, if it is pointed at first position 
                        //(example: RowFilter = "AND Charge=0" is not a valid filter")
                         stFilter = stFilter.Trim();
                         try
                         {
                             string sFirstCharsFilter = stFilter.Substring(0, 3);
                             for (int i = 0; i <= operators.GetUpperBound(0); i++)
                             {
                                 if (sFirstCharsFilter.Trim() == operators[i])
                                 {
                                     stFilter = stFilter.Substring(4, stFilter.Length - 4);
                                     break;
                                 }
                             }
                         }
                         catch { }

                         OPquanFilter = stFilter;
                         ViewRecords.RowFilter = bothFilters(OPquanFilter, OPgrapherFilter);
                         break;
                }

                this.filterTxt.Text = OPquanFilter.Trim();
            }
            catch 
            {
                chkShowStats.Checked = false;
            }

        }

        private void chkHideBadQuality_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                switch (chkHideBadQuality.Checked)
                {
                    case true:
                        if (ViewRecords.RowFilter.Trim() != "")
                        {
                            ViewRecords.RowFilter += " AND numLabel1<>0 ";

                        }
                        else
                        {
                            ViewRecords.RowFilter += " numLabel1<>0 ";

                        }
                        break;
                    case false:
                        string[] operators = new string[6];
                        operators[0] = "AND";
                        operators[1] = "and";
                        operators[2] = "OR";
                        operators[3] = "or";
                        operators[4] = "NOT";
                        operators[5] = "not";

                        string stFilter = (string)ViewRecords.RowFilter.Clone();
                        if ((ViewRecords.RowFilter.Trim().Contains("AND numLabel1<>0")))
                        {
                            string[] sRemove = new string[1];
                            sRemove[0] = "AND numLabel1<>0";
                            string[] sVar = stFilter.Split(sRemove, StringSplitOptions.RemoveEmptyEntries);
                            stFilter = "";
                            for (int i = 0; i <= sVar.GetUpperBound(0); i++)
                            {
                                stFilter += sVar[i];
                            }
                        }
                        if ((ViewRecords.RowFilter.Trim().Contains("numLabel1<>0")))
                        {
                            string[] sRemove = new string[1];
                            sRemove[0] = "numLabel1<>0";
                            string[] sVar = stFilter.Split(sRemove, StringSplitOptions.RemoveEmptyEntries);
                            stFilter = "";
                            for (int i = 0; i <= sVar.GetUpperBound(0); i++)
                            {
                                stFilter += sVar[i];
                            }
                        }

                        //remove a operator, if it is pointed at first position 
                        //(example: RowFilter = "AND Charge=0" is not a valid filter")
                        stFilter = stFilter.Trim();
                        try
                        {
                            string sFirstCharsFilter = stFilter.Substring(0, 3);
                            for (int i = 0; i <= operators.GetUpperBound(0); i++)
                            {
                                if (sFirstCharsFilter.Trim() == operators[i])
                                {
                                    stFilter = stFilter.Substring(4, stFilter.Length - 4);
                                    break;
                                }
                            }
                        }
                        catch { }

                        ViewRecords.RowFilter = stFilter;
                        break;
                }
                this.filterTxt.Text = ViewRecords.RowFilter.Trim();
            }
            catch
            {
                chkHideBadQuality.Checked = false;
            }
        }

        private void sortTxt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.sortTxt.Text.Trim() == ViewRecords.Sort.Trim())
                {
                    this.sortTxt.ForeColor = Color.BlueViolet;
                }
                else { this.sortTxt.ForeColor = Color.Red; }
            }
            catch { }

        }



        #region Miembros de Iobserver

        public void Update(ArrayList selectedRows)
        {
            this.lblSelectedRows.Text = selectedRows.Count.ToString();

            //throw new NotImplementedException();
        }

        #endregion

        private void btnScans_Click(object sender, EventArgs e)
        {

            string sort="";
            string rowFilter = filterTxt.Text.Trim();
            string preRowFilter = "";
            string hideColumns="";
            ArrayList filtersOld=new ArrayList();

            filtersOld.Add("s_index");
            filtersOld.Add("p_index");
            filtersOld.Add("q_index");

            //disable previous q_index, p_index or s_index in filter

            foreach (string fil in filtersOld)
            {
                int start = rowFilter.ToUpper().IndexOf("AND " + fil.ToUpper().Trim());
                if (start == -1) start = rowFilter.ToUpper().IndexOf(fil.ToUpper().Trim());
                if (start > -1)
                {
                    int last = rowFilter.ToUpper().IndexOf("AND", start + 1);
                    if (last == -1) last = rowFilter.Length + 4;
                    if (last > rowFilter.Length - start) last = rowFilter.Length - start;

                    rowFilter = rowFilter.Remove(start, last);
                }
            }
            

                var query =
                  from meth in quantitationMeths.method
                  join scanButton in quantitationMeths.scans_button
                  on meth.method_Id equals scanButton.method_Id
                  where meth.method_id_name == methodChosen
                  select new
                  {
                      rowFilter = scanButton.rowFilter.ToString(),
                      sort = scanButton.sort.ToString(),
                      columns = scanButton.hide_columns.ToString()
                  };

                foreach (var m in query)
                {
                    sort = m.sort;
                    preRowFilter = m.rowFilter;
                    hideColumns = m.columns;
                }

            
                if (rowFilter.Trim() == "")
                {
                    rowFilter += preRowFilter;
                }
                else
                {
                    rowFilter += " AND " + preRowFilter;
                }

            
                rowFilter = fixAnds(rowFilter);

                reFilter(rowFilter);

                if (sort.Trim() != "")
                {
                    try
                    {
                        ViewRecords.Sort = sort;
                        sortTxt_TextChanged(null, null);
                    }
                    catch{}
                }

                SetTableBySelectedColumns(hideColumns, DataSetRecords.Tables["peptide_match"].TableName);
 

        }

        private void btnPeptides_Click(object sender, EventArgs e)
        {
            string sort="";
            string rowFilter = filterTxt.Text.Trim();
            string preRowFilter = "";
            string hideColumns="";
            ArrayList filtersOld = new ArrayList();

            filtersOld.Add("s_index");
            filtersOld.Add("p_index");
            filtersOld.Add("q_index");

            //disable previous q_index, p_index or s_index in filter

            foreach (string fil in filtersOld)
            {
                int start = rowFilter.ToUpper().IndexOf("AND " + fil.ToUpper().Trim());
                if (start == -1) start = rowFilter.ToUpper().IndexOf(fil.ToUpper().Trim());
                if (start > -1)
                {
                    int last = rowFilter.ToUpper().IndexOf("AND", start + 1);
                    if (last == -1) last = rowFilter.Length + 4;
                    if (last > rowFilter.Length - start) last = rowFilter.Length - start;

                    rowFilter = rowFilter.Remove(start, last);
                    rowFilter = fixAnds(rowFilter);
                }
            }


            var query =
              from meth in quantitationMeths.method
              join peptideButton in quantitationMeths.peptides_button
              on meth.method_Id equals peptideButton.method_Id
              where meth.method_id_name == methodChosen
              select new
              {
                  rowFilter = peptideButton.rowFilter.ToString(),
                  sort = peptideButton.sort.ToString(),
                  columns = peptideButton.hide_columns.ToString()
              };

            foreach (var m in query)
            {
                sort = m.sort;
                preRowFilter = m.rowFilter;
                hideColumns = m.columns;
            }


            if (rowFilter.Trim() == "")
            {
                rowFilter += preRowFilter;
            }
            else
            {
                rowFilter += " AND " + preRowFilter;
            }


            rowFilter = fixAnds(rowFilter);

            reFilter(rowFilter);

            if (sort.Trim() != "")
            {
                try
                {
                    ViewRecords.Sort = sort;
                    sortTxt_TextChanged(null, null);
                }
                catch { }
            }

            SetTableBySelectedColumns(hideColumns, DataSetRecords.Tables["peptide_match"].TableName);


        }

        private void btnProteins_Click(object sender, EventArgs e)
        {
            string sort="";
            string rowFilter = filterTxt.Text.Trim();
            string preRowFilter = "";
            string hideColumns="";
            ArrayList filtersOld = new ArrayList();

            filtersOld.Add("s_index");
            filtersOld.Add("p_index");
            filtersOld.Add("q_index");

            //disable previous q_index, p_index or s_index in filter

            foreach (string fil in filtersOld)
            {
                int start = rowFilter.ToUpper().IndexOf("AND " + fil.ToUpper().Trim());
                if (start == -1) start = rowFilter.ToUpper().IndexOf(fil.ToUpper().Trim());
                if (start > -1)
                {
                    int last = rowFilter.ToUpper().IndexOf("AND", start + 1);
                    if (last == -1) last = rowFilter.Length + 4;
                    if (last > rowFilter.Length - start) last = rowFilter.Length - start;

                    rowFilter = rowFilter.Remove(start, last);
                    rowFilter = fixAnds(rowFilter);
                }
            }


            var query =
              from meth in quantitationMeths.method
              join proteinButton in quantitationMeths.proteins_button
              on meth.method_Id equals proteinButton.method_Id
              where meth.method_id_name == methodChosen
              select new
              {
                  rowFilter = proteinButton.rowFilter.ToString(),
                  sort = proteinButton.sort.ToString(),
                  columns = proteinButton.hide_columns.ToString()
              };

            foreach (var m in query)
            {
                sort = m.sort;
                preRowFilter = m.rowFilter;
                hideColumns = m.columns;
            }


            if (rowFilter.Trim() == "")
            {
                rowFilter += preRowFilter;
            }
            else
            {
                rowFilter += " AND " + preRowFilter;
            }


            rowFilter = fixAnds(rowFilter);

            reFilter(rowFilter);

            if (sort.Trim() != "")
            {
                try
                {
                    ViewRecords.Sort = sort;
                    sortTxt_TextChanged(null, null);
                }
                catch { }
            }

            SetTableBySelectedColumns(hideColumns, DataSetRecords.Tables["peptide_match"].TableName);

        }

        private void filterTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                reFilter(filterTxt.Text.Trim());
                //MessageBox.Show("Enter pressed", "Attention");
            }
        }

        private void sortTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.btnSort.PerformClick();
                //MessageBox.Show("Enter pressed", "Attention");
            }

        }

        private void OPquan_SizeChanged(object sender, EventArgs e)
        {
            resizeOPquan();
        }

        private void resizeOPquan()
        {
            resizeOPquan(resizingOPquan);
        }

        private void resizeOPquan(bool resizing)
        {
            // (160, 31) is exactly the size adopted while minimising and maximising
            if (resizing || this.Size == new Size(160, 31)) return;

            resizingOPquan = true;

            if (this.Size.Width < OPquanMinWidth)
            {
                this.Size = new Size(OPquanMinWidth, this.Height);
            }

            if (this.Size.Height < OPquanMinHeight)
                this.Size = new Size(this.Width, OPquanMinHeight);

            int extraHeight = this.Height - OPquanHeight;
            int extraWidth = this.Width - OPquanWidth;

            idfileTxt.Size = new Size(idfileTxtWidth + extraWidth, idfileTxt.Height);
            filterTxt.Size = new Size(filterTxtWidth + extraWidth, filterTxt.Height);
            sortTxt.Size = new Size(sortTxtWidth + extraWidth, sortTxt.Height);
            quantifPrBar.Size = new Size(quantifPrBarWidth + extraWidth, quantifPrBar.Height);
            panel1.Size = new Size(panel1Width + extraWidth, panel1Height + extraHeight);
            idsGrid.Size = new Size(idsGridWidth + extraWidth, idsGridHeight + extraHeight);

            if (barsPBoxWidth + extraWidth <= barsPBoxMaxWidth)
            {
                barsPBox.Size = new Size(barsPBoxWidth + extraWidth, barsPBox.Height);
            }


            try
            {
                double q_A = double.Parse(ViewRecords[idsGrid.CurrentRowIndex].Row["q_A"].ToString());
                double q_B = double.Parse(ViewRecords[idsGrid.CurrentRowIndex].Row["q_B"].ToString());
                double q_f = double.Parse(ViewRecords[idsGrid.CurrentRowIndex].Row["q_f"].ToString());


                drawBars(q_A, q_B, q_f, barsPBox);
            }
            catch { }

            resizingOPquan = false;
        }

        private void OPquan_ResizeBegin(object sender, EventArgs e)
        {
            resizeOPquan();
        }

        private void OPquan_ResizeEnd(object sender, EventArgs e)
        {
            resizeOPquan();
        }

        private void btnVarConf_Click(object sender, EventArgs e)
        {
            if (DataSetRecords != null)
            {
                OPvarConf varConf = new OPvarConf(DataSetRecords, dSigma2S, dSigma2P, dSigma2Q, idfileTxt.Text);
                varConf.Show();
            }
            else
            {
                MessageBox.Show("Not enough data.");
                Application.DoEvents();
            }
        }
    }


    public class dgo : Object, IBeObserved
    {
      
        public DataGrid dgDatagrid = null;
        ArrayList observers;
        
        //public int Find (Object key) 
        //{
        //   return delegated.Find(key);
        //}
        
        public dgo(DataGrid dg)
        {
            observers = new ArrayList();
            dgDatagrid = dg;
            dg.Click += new EventHandler(dg_Click);
            dg.DoubleClick += new EventHandler(dg_Click);
        }

        void dg_Click(object sender, EventArgs e)
        {
            try
            {
                ArrayList alSelectedRows = new ArrayList();
                CurrencyManager cm = (CurrencyManager)dgDatagrid.BindingContext[dgDatagrid.DataSource,
                                                                                dgDatagrid.DataMember];
                DataView dv = (DataView)cm.List;

                
                for (int i = 0; i < dv.Count; ++i)
                {
                    if (dgDatagrid.IsSelected(i))
                    {
                        int idx = int.Parse(dv[i].Row["SpectrumIndex"].ToString());


                        alSelectedRows.Add(idx);
                    }
                }


                NotifyObservers(alSelectedRows);
            }
            catch { }
        }

     
        #region Miembros de IBeObserved


        public void attach(Iobserver observer)
        {
            //if observer is not in the list, add
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }
        }

        public void detach(Iobserver observer)
        {
            // if observer is in the list, remove
            if (observers.Contains(observer))
            {
                observers.Remove(observer);
            }

        }


        public void NotifyObservers(ArrayList selRows) 
        {


            // call update method for every observer
            foreach (QuiXoT.Iobserver observer in observers)
            {
                observer.Update(selRows);
            }

        }

        #endregion


    }

   
    public interface IBeObserved
    {
        // Registers an observer to the subject's notification list
        void attach(Iobserver observer);
  
        // Removes a registered observer from the subject's notification list
        void detach(Iobserver observer);

        // Notifies the observers in the notification list of any change that occurred in the subject
        void NotifyObservers(ArrayList selectedRows);

    }

    public class Syncro
    {
        private static readonly string[] TimeServer;

        static Syncro()
        {
			// Modify the server name as desired
            TimeServer = new string[14];
			TimeServer[0] = "tick.usno.navy.mila";
            TimeServer[1] = "time-a.nist.gova";
            TimeServer[2] = "time-b.nist.gova";
            TimeServer[3] = "time-a.timefreq.bldrdoc.gov";
            TimeServer[4] = "time-b.timefreq.bldrdoc.gov";
            TimeServer[5] = "time-c.timefreq.bldrdoc.gov";
            TimeServer[6] = "utcnist.colorado.edu";
            TimeServer[7] = "utcnist.colorado.edu";
            TimeServer[8] = "time.nist.gov";
            TimeServer[9] = "time-nw.nist.gov";
            TimeServer[10] = "nist1.datum.com";
            TimeServer[11] = "nist1.dc.certifiedtime.com";
            TimeServer[12] = "nist1.nyc.certifiedtime.com";
            TimeServer[13] = "nist1.sjc.certifiedtime.com";
        }

        public static DateTime getTime()
        {			
			NTPClient client;
            DateTime dt = new DateTime();
            
            
            for (int i = 0; i <= TimeServer.GetUpperBound(0); i++)
            {
                try
                {
                    client = new NTPClient(TimeServer[i]);
                    client.Connect(false);
                    if (client.IsResponseValid())
                    {
                        dt = client.TransmitTimestamp;
                        break;
                    }
                }
                catch
                {
                }                
            }
           
            
            return dt;
        }
    }
}