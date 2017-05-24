using System.Windows.Forms;
using System.Data;
namespace QuiXoT
{
    partial class OPquan
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OPquan));
            this.loadIdfileBtn = new System.Windows.Forms.Button();
            this.idfileTxt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDeployRed = new System.Windows.Forms.Button();
            this.NGbtn = new System.Windows.Forms.Button();
            this.writeCSVbtn = new System.Windows.Forms.Button();
            this.stGraphBtn = new System.Windows.Forms.Button();
            this.writeXMLBtn = new System.Windows.Forms.Button();
            this.btnChangeColVal = new System.Windows.Forms.Button();
            this.quanPercenttxt = new System.Windows.Forms.Label();
            this.quanPrtxt = new System.Windows.Forms.Label();
            this.quantifPrBar = new System.Windows.Forms.ProgressBar();
            this.graphBtn = new System.Windows.Forms.Button();
            this.ButtonDataFilter = new System.Windows.Forms.Button();
            this.ButtonColumnFilter = new System.Windows.Forms.Button();
            this.idsGrid = new System.Windows.Forms.DataGrid();
            this.btnStats = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnVarConf = new System.Windows.Forms.Button();
            this.txtValPhi = new System.Windows.Forms.Label();
            this.txtPhi = new System.Windows.Forms.Label();
            this.btnVarCalc = new System.Windows.Forms.Button();
            this.txtValSigma2Q = new System.Windows.Forms.Label();
            this.txtValSigma2P = new System.Windows.Forms.Label();
            this.txtValSigma2S = new System.Windows.Forms.Label();
            this.txtValK = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lnPeptCts = new System.Windows.Forms.LinkLabel();
            this.filterTxt = new System.Windows.Forms.TextBox();
            this.filterBtn = new System.Windows.Forms.Button();
            this.statusLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checklist = new System.Windows.Forms.CheckedListBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.barsPBox = new System.Windows.Forms.PictureBox();
            this.btnLookForIdXml = new System.Windows.Forms.Button();
            this.btnSort = new System.Windows.Forms.Button();
            this.grbStats = new System.Windows.Forms.GroupBox();
            this.lblNq = new System.Windows.Forms.Label();
            this.lblNp = new System.Windows.Forms.Label();
            this.lblNs = new System.Windows.Forms.Label();
            this.lblX = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.lblFDRq = new System.Windows.Forms.Label();
            this.lblZq = new System.Windows.Forms.Label();
            this.lblWq = new System.Windows.Forms.Label();
            this.lblXq = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.lblFDRp = new System.Windows.Forms.Label();
            this.lblZp = new System.Windows.Forms.Label();
            this.lblWp = new System.Windows.Forms.Label();
            this.lblXp = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.lblFDRs = new System.Windows.Forms.Label();
            this.lblZs = new System.Windows.Forms.Label();
            this.lblWs = new System.Windows.Forms.Label();
            this.lblXs = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.grbPepId = new System.Windows.Forms.GroupBox();
            this.lblF = new System.Windows.Forms.Label();
            this.titF = new System.Windows.Forms.Label();
            this.lblVs = new System.Windows.Forms.Label();
            this.titVs = new System.Windows.Forms.Label();
            this.lblFDR = new System.Windows.Forms.Label();
            this.titFDR = new System.Windows.Forms.Label();
            this.lblDCn = new System.Windows.Forms.Label();
            this.titDCn = new System.Windows.Forms.Label();
            this.lblXcorr = new System.Windows.Forms.Label();
            this.titXcorr = new System.Windows.Forms.Label();
            this.sortTxt = new System.Windows.Forms.TextBox();
            this.chkShowStats = new System.Windows.Forms.CheckBox();
            this.chkHideBadQuality = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblSpecCount = new System.Windows.Forms.Label();
            this.reloadConfFilesBtn = new System.Windows.Forms.Button();
            this.grpSampleData = new System.Windows.Forms.GroupBox();
            this.lblNproteins = new System.Windows.Forms.Label();
            this.lblNpeptides = new System.Windows.Forms.Label();
            this.lblNscans = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.btnScans = new System.Windows.Forms.Button();
            this.btnProteins = new System.Windows.Forms.Button();
            this.btnPeptides = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.lblSelectedRows = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.idsGrid)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barsPBox)).BeginInit();
            this.grbStats.SuspendLayout();
            this.grbPepId.SuspendLayout();
            this.grpSampleData.SuspendLayout();
            this.SuspendLayout();
            // 
            // loadIdfileBtn
            // 
            this.loadIdfileBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.loadIdfileBtn.Location = new System.Drawing.Point(561, 6);
            this.loadIdfileBtn.Name = "loadIdfileBtn";
            this.loadIdfileBtn.Size = new System.Drawing.Size(36, 20);
            this.loadIdfileBtn.TabIndex = 3;
            this.loadIdfileBtn.Text = "load";
            this.loadIdfileBtn.UseVisualStyleBackColor = true;
            this.loadIdfileBtn.Click += new System.EventHandler(this.loadIdfileBtn_Click);
            // 
            // idfileTxt
            // 
            this.idfileTxt.Location = new System.Drawing.Point(208, 6);
            this.idfileTxt.Name = "idfileTxt";
            this.idfileTxt.Size = new System.Drawing.Size(316, 20);
            this.idfileTxt.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(138, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "QuiXML file:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnDeployRed);
            this.panel1.Controls.Add(this.NGbtn);
            this.panel1.Controls.Add(this.writeCSVbtn);
            this.panel1.Controls.Add(this.stGraphBtn);
            this.panel1.Controls.Add(this.writeXMLBtn);
            this.panel1.Controls.Add(this.btnChangeColVal);
            this.panel1.Controls.Add(this.quanPercenttxt);
            this.panel1.Controls.Add(this.quanPrtxt);
            this.panel1.Controls.Add(this.quantifPrBar);
            this.panel1.Controls.Add(this.graphBtn);
            this.panel1.Controls.Add(this.ButtonDataFilter);
            this.panel1.Controls.Add(this.ButtonColumnFilter);
            this.panel1.Controls.Add(this.idsGrid);
            this.panel1.Location = new System.Drawing.Point(12, 152);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(940, 407);
            this.panel1.TabIndex = 23;
            // 
            // btnDeployRed
            // 
            this.btnDeployRed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDeployRed.Location = new System.Drawing.Point(142, 377);
            this.btnDeployRed.Name = "btnDeployRed";
            this.btnDeployRed.Size = new System.Drawing.Size(67, 23);
            this.btnDeployRed.TabIndex = 3;
            this.btnDeployRed.Text = "deploy red";
            this.btnDeployRed.UseVisualStyleBackColor = true;
            this.btnDeployRed.Click += new System.EventHandler(this.btnDeployRed_Click);
            // 
            // NGbtn
            // 
            this.NGbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.NGbtn.Location = new System.Drawing.Point(73, 377);
            this.NGbtn.Name = "NGbtn";
            this.NGbtn.Size = new System.Drawing.Size(66, 23);
            this.NGbtn.TabIndex = 2;
            this.NGbtn.Text = "quantitate";
            this.NGbtn.UseVisualStyleBackColor = true;
            this.NGbtn.Click += new System.EventHandler(this.NGbtn_Click);
            // 
            // writeCSVbtn
            // 
            this.writeCSVbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.writeCSVbtn.Location = new System.Drawing.Point(567, 375);
            this.writeCSVbtn.Name = "writeCSVbtn";
            this.writeCSVbtn.Size = new System.Drawing.Size(63, 23);
            this.writeCSVbtn.TabIndex = 6;
            this.writeCSVbtn.Text = "write XLS";
            this.writeCSVbtn.UseVisualStyleBackColor = true;
            this.writeCSVbtn.Click += new System.EventHandler(this.writeCSVbtn_Click);
            // 
            // stGraphBtn
            // 
            this.stGraphBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.stGraphBtn.Location = new System.Drawing.Point(210, 377);
            this.stGraphBtn.Name = "stGraphBtn";
            this.stGraphBtn.Size = new System.Drawing.Size(56, 23);
            this.stGraphBtn.TabIndex = 4;
            this.stGraphBtn.Text = "graphs";
            this.stGraphBtn.UseVisualStyleBackColor = true;
            this.stGraphBtn.Click += new System.EventHandler(this.stGraphBtn_Click);
            // 
            // writeXMLBtn
            // 
            this.writeXMLBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.writeXMLBtn.Location = new System.Drawing.Point(496, 375);
            this.writeXMLBtn.Name = "writeXMLBtn";
            this.writeXMLBtn.Size = new System.Drawing.Size(65, 23);
            this.writeXMLBtn.TabIndex = 5;
            this.writeXMLBtn.Text = "write XML";
            this.writeXMLBtn.UseVisualStyleBackColor = true;
            this.writeXMLBtn.Click += new System.EventHandler(this.writeXMLBtn_Click);
            // 
            // btnChangeColVal
            // 
            this.btnChangeColVal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChangeColVal.Location = new System.Drawing.Point(656, 376);
            this.btnChangeColVal.Name = "btnChangeColVal";
            this.btnChangeColVal.Size = new System.Drawing.Size(101, 23);
            this.btnChangeColVal.TabIndex = 7;
            this.btnChangeColVal.Text = "change col value";
            this.btnChangeColVal.UseVisualStyleBackColor = false;
            this.btnChangeColVal.Click += new System.EventHandler(this.btnChangeColVal_Click);
            // 
            // quanPercenttxt
            // 
            this.quanPercenttxt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.quanPercenttxt.AutoSize = true;
            this.quanPercenttxt.Location = new System.Drawing.Point(345, 383);
            this.quanPercenttxt.Name = "quanPercenttxt";
            this.quanPercenttxt.Size = new System.Drawing.Size(27, 13);
            this.quanPercenttxt.TabIndex = 30;
            this.quanPercenttxt.Text = "(0%)";
            this.quanPercenttxt.Visible = false;
            // 
            // quanPrtxt
            // 
            this.quanPrtxt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.quanPrtxt.AutoSize = true;
            this.quanPrtxt.Location = new System.Drawing.Point(270, 383);
            this.quanPrtxt.Name = "quanPrtxt";
            this.quanPrtxt.Size = new System.Drawing.Size(67, 13);
            this.quanPrtxt.TabIndex = 29;
            this.quanPrtxt.Text = "quantifying...";
            this.quanPrtxt.Visible = false;
            // 
            // quantifPrBar
            // 
            this.quantifPrBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.quantifPrBar.Location = new System.Drawing.Point(383, 381);
            this.quantifPrBar.Name = "quantifPrBar";
            this.quantifPrBar.Size = new System.Drawing.Size(104, 15);
            this.quantifPrBar.TabIndex = 28;
            this.quantifPrBar.UseWaitCursor = true;
            this.quantifPrBar.Visible = false;
            // 
            // graphBtn
            // 
            this.graphBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.graphBtn.Location = new System.Drawing.Point(3, 377);
            this.graphBtn.Name = "graphBtn";
            this.graphBtn.Size = new System.Drawing.Size(67, 23);
            this.graphBtn.TabIndex = 1;
            this.graphBtn.Text = "spectrum";
            this.graphBtn.UseVisualStyleBackColor = true;
            this.graphBtn.Click += new System.EventHandler(this.graphBtn_Click);
            // 
            // ButtonDataFilter
            // 
            this.ButtonDataFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonDataFilter.Location = new System.Drawing.Point(846, 375);
            this.ButtonDataFilter.Name = "ButtonDataFilter";
            this.ButtonDataFilter.Size = new System.Drawing.Size(88, 24);
            this.ButtonDataFilter.TabIndex = 9;
            this.ButtonDataFilter.Text = "Data Filter ";
            this.ButtonDataFilter.Click += new System.EventHandler(this.ButtonDataFilter_Click);
            // 
            // ButtonColumnFilter
            // 
            this.ButtonColumnFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonColumnFilter.Location = new System.Drawing.Point(757, 376);
            this.ButtonColumnFilter.Name = "ButtonColumnFilter";
            this.ButtonColumnFilter.Size = new System.Drawing.Size(88, 23);
            this.ButtonColumnFilter.TabIndex = 8;
            this.ButtonColumnFilter.Text = "Column Filter";
            this.ButtonColumnFilter.Click += new System.EventHandler(this.ButtonColumnFilter_Click);
            // 
            // idsGrid
            // 
            this.idsGrid.BackColor = System.Drawing.Color.Silver;
            this.idsGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.idsGrid.DataMember = "";
            this.idsGrid.Dock = System.Windows.Forms.DockStyle.Top;
            this.idsGrid.ForeColor = System.Drawing.Color.Black;
            this.idsGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.idsGrid.Location = new System.Drawing.Point(0, 0);
            this.idsGrid.Name = "idsGrid";
            this.idsGrid.Size = new System.Drawing.Size(940, 370);
            this.idsGrid.TabIndex = 0;
            this.idsGrid.MouseClick += new System.Windows.Forms.MouseEventHandler(this.idsGrid_MouseClick);
            this.idsGrid.DoubleClick += new System.EventHandler(this.idsGrid_DoubleClick);
            // 
            // btnStats
            // 
            this.btnStats.Location = new System.Drawing.Point(155, 58);
            this.btnStats.Name = "btnStats";
            this.btnStats.Size = new System.Drawing.Size(62, 23);
            this.btnStats.TabIndex = 2;
            this.btnStats.Text = "stats";
            this.btnStats.UseVisualStyleBackColor = true;
            this.btnStats.Click += new System.EventHandler(this.btnStats_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnVarConf);
            this.groupBox2.Controls.Add(this.txtValPhi);
            this.groupBox2.Controls.Add(this.txtPhi);
            this.groupBox2.Controls.Add(this.btnVarCalc);
            this.groupBox2.Controls.Add(this.txtValSigma2Q);
            this.groupBox2.Controls.Add(this.txtValSigma2P);
            this.groupBox2.Controls.Add(this.btnStats);
            this.groupBox2.Controls.Add(this.txtValSigma2S);
            this.groupBox2.Controls.Add(this.txtValK);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.lnPeptCts);
            this.groupBox2.Location = new System.Drawing.Point(721, 565);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(231, 88);
            this.groupBox2.TabIndex = 24;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "statistics";
            // 
            // btnVarConf
            // 
            this.btnVarConf.Location = new System.Drawing.Point(10, 59);
            this.btnVarConf.Name = "btnVarConf";
            this.btnVarConf.Size = new System.Drawing.Size(54, 23);
            this.btnVarConf.TabIndex = 40;
            this.btnVarConf.Text = "var conf";
            this.btnVarConf.UseVisualStyleBackColor = true;
            this.btnVarConf.Click += new System.EventHandler(this.btnVarConf_Click);
            // 
            // txtValPhi
            // 
            this.txtValPhi.AutoSize = true;
            this.txtValPhi.Location = new System.Drawing.Point(33, 44);
            this.txtValPhi.Name = "txtValPhi";
            this.txtValPhi.Size = new System.Drawing.Size(33, 13);
            this.txtValPhi.TabIndex = 39;
            this.txtValPhi.Text = "value";
            this.txtValPhi.Visible = false;
            // 
            // txtPhi
            // 
            this.txtPhi.AutoSize = true;
            this.txtPhi.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhi.Location = new System.Drawing.Point(7, 44);
            this.txtPhi.Name = "txtPhi";
            this.txtPhi.Size = new System.Drawing.Size(26, 13);
            this.txtPhi.TabIndex = 38;
            this.txtPhi.Text = "Φ :";
            this.txtPhi.Visible = false;
            // 
            // btnVarCalc
            // 
            this.btnVarCalc.Location = new System.Drawing.Point(155, 30);
            this.btnVarCalc.Name = "btnVarCalc";
            this.btnVarCalc.Size = new System.Drawing.Size(62, 23);
            this.btnVarCalc.TabIndex = 1;
            this.btnVarCalc.Text = "var calc";
            this.btnVarCalc.UseVisualStyleBackColor = true;
            this.btnVarCalc.Click += new System.EventHandler(this.btnVarCalc_Click);
            // 
            // txtValSigma2Q
            // 
            this.txtValSigma2Q.AutoSize = true;
            this.txtValSigma2Q.Location = new System.Drawing.Point(106, 64);
            this.txtValSigma2Q.Name = "txtValSigma2Q";
            this.txtValSigma2Q.Size = new System.Drawing.Size(33, 13);
            this.txtValSigma2Q.TabIndex = 37;
            this.txtValSigma2Q.Text = "value";
            this.txtValSigma2Q.Visible = false;
            // 
            // txtValSigma2P
            // 
            this.txtValSigma2P.AutoSize = true;
            this.txtValSigma2P.Location = new System.Drawing.Point(106, 44);
            this.txtValSigma2P.Name = "txtValSigma2P";
            this.txtValSigma2P.Size = new System.Drawing.Size(33, 13);
            this.txtValSigma2P.TabIndex = 36;
            this.txtValSigma2P.Text = "value";
            this.txtValSigma2P.Visible = false;
            // 
            // txtValSigma2S
            // 
            this.txtValSigma2S.AutoSize = true;
            this.txtValSigma2S.Location = new System.Drawing.Point(106, 25);
            this.txtValSigma2S.Name = "txtValSigma2S";
            this.txtValSigma2S.Size = new System.Drawing.Size(33, 13);
            this.txtValSigma2S.TabIndex = 35;
            this.txtValSigma2S.Text = "value";
            this.txtValSigma2S.Visible = false;
            // 
            // txtValK
            // 
            this.txtValK.AutoSize = true;
            this.txtValK.Location = new System.Drawing.Point(33, 25);
            this.txtValK.Name = "txtValK";
            this.txtValK.Size = new System.Drawing.Size(33, 13);
            this.txtValK.TabIndex = 34;
            this.txtValK.Text = "value";
            this.txtValK.Visible = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(67, 64);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(34, 13);
            this.label15.TabIndex = 33;
            this.label15.Text = "σ²q :";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(67, 44);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(34, 13);
            this.label13.TabIndex = 32;
            this.label13.Text = "σ²p :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(69, 25);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(33, 13);
            this.label10.TabIndex = 31;
            this.label10.Text = "σ²s :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(7, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(23, 13);
            this.label8.TabIndex = 30;
            this.label8.Text = "K :";
            // 
            // lnPeptCts
            // 
            this.lnPeptCts.AutoSize = true;
            this.lnPeptCts.Location = new System.Drawing.Point(147, 10);
            this.lnPeptCts.Name = "lnPeptCts";
            this.lnPeptCts.Size = new System.Drawing.Size(77, 13);
            this.lnPeptCts.TabIndex = 0;
            this.lnPeptCts.TabStop = true;
            this.lnPeptCts.Text = "change values";
            this.lnPeptCts.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnPeptCts_LinkClicked);
            // 
            // filterTxt
            // 
            this.filterTxt.ForeColor = System.Drawing.Color.Red;
            this.filterTxt.Location = new System.Drawing.Point(139, 89);
            this.filterTxt.Name = "filterTxt";
            this.filterTxt.Size = new System.Drawing.Size(468, 20);
            this.filterTxt.TabIndex = 6;
            this.filterTxt.TextChanged += new System.EventHandler(this.filterTxt_TextChanged);
            this.filterTxt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.filterTxt_KeyPress);
            // 
            // filterBtn
            // 
            this.filterBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.filterBtn.Location = new System.Drawing.Point(613, 89);
            this.filterBtn.Name = "filterBtn";
            this.filterBtn.Size = new System.Drawing.Size(38, 21);
            this.filterBtn.TabIndex = 7;
            this.filterBtn.Text = "filter";
            this.filterBtn.UseVisualStyleBackColor = true;
            this.filterBtn.Click += new System.EventHandler(this.filterBtn_Click);
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusLabel.Location = new System.Drawing.Point(113, 34);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(84, 24);
            this.statusLabel.TabIndex = 32;
            this.statusLabel.Text = "Status...";
            this.statusLabel.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.checklist);
            this.groupBox1.Location = new System.Drawing.Point(796, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(154, 106);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Load status";
            // 
            // checklist
            // 
            this.checklist.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(197)))), ((int)(((byte)(222)))));
            this.checklist.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.checklist.CausesValidation = false;
            this.checklist.FormattingEnabled = true;
            this.checklist.Location = new System.Drawing.Point(12, 22);
            this.checklist.Name = "checklist";
            this.checklist.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.checklist.Size = new System.Drawing.Size(138, 75);
            this.checklist.TabIndex = 0;
            this.checklist.ThreeDCheckBoxes = true;
            this.checklist.UseCompatibleTextRendering = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Help;
            this.pictureBox1.Image = global::QuiXoT.Properties.Resources.quijote105x82;
            this.pictureBox1.Location = new System.Drawing.Point(15, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(92, 105);
            this.pictureBox1.TabIndex = 40;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Tag = "click to know about...";
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // barsPBox
            // 
            this.barsPBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.barsPBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(197)))), ((int)(((byte)(222)))));
            this.barsPBox.Location = new System.Drawing.Point(530, 565);
            this.barsPBox.Name = "barsPBox";
            this.barsPBox.Size = new System.Drawing.Size(185, 92);
            this.barsPBox.TabIndex = 35;
            this.barsPBox.TabStop = false;
            // 
            // btnLookForIdXml
            // 
            this.btnLookForIdXml.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLookForIdXml.Location = new System.Drawing.Point(525, 6);
            this.btnLookForIdXml.Name = "btnLookForIdXml";
            this.btnLookForIdXml.Size = new System.Drawing.Size(32, 20);
            this.btnLookForIdXml.TabIndex = 2;
            this.btnLookForIdXml.Text = "...";
            this.btnLookForIdXml.UseVisualStyleBackColor = true;
            this.btnLookForIdXml.Click += new System.EventHandler(this.btnLookForIdXml_Click);
            // 
            // btnSort
            // 
            this.btnSort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSort.Location = new System.Drawing.Point(613, 62);
            this.btnSort.Name = "btnSort";
            this.btnSort.Size = new System.Drawing.Size(38, 21);
            this.btnSort.TabIndex = 5;
            this.btnSort.Text = "sort";
            this.btnSort.UseVisualStyleBackColor = true;
            this.btnSort.Click += new System.EventHandler(this.btnSort_Click);
            // 
            // grbStats
            // 
            this.grbStats.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.grbStats.Controls.Add(this.lblNq);
            this.grbStats.Controls.Add(this.lblNp);
            this.grbStats.Controls.Add(this.lblNs);
            this.grbStats.Controls.Add(this.lblX);
            this.grbStats.Controls.Add(this.label31);
            this.grbStats.Controls.Add(this.label32);
            this.grbStats.Controls.Add(this.label33);
            this.grbStats.Controls.Add(this.label34);
            this.grbStats.Controls.Add(this.lblFDRq);
            this.grbStats.Controls.Add(this.lblZq);
            this.grbStats.Controls.Add(this.lblWq);
            this.grbStats.Controls.Add(this.lblXq);
            this.grbStats.Controls.Add(this.label27);
            this.grbStats.Controls.Add(this.label28);
            this.grbStats.Controls.Add(this.label29);
            this.grbStats.Controls.Add(this.label30);
            this.grbStats.Controls.Add(this.lblFDRp);
            this.grbStats.Controls.Add(this.lblZp);
            this.grbStats.Controls.Add(this.lblWp);
            this.grbStats.Controls.Add(this.lblXp);
            this.grbStats.Controls.Add(this.label23);
            this.grbStats.Controls.Add(this.label24);
            this.grbStats.Controls.Add(this.label25);
            this.grbStats.Controls.Add(this.label26);
            this.grbStats.Controls.Add(this.lblFDRs);
            this.grbStats.Controls.Add(this.lblZs);
            this.grbStats.Controls.Add(this.lblWs);
            this.grbStats.Controls.Add(this.lblXs);
            this.grbStats.Controls.Add(this.label18);
            this.grbStats.Controls.Add(this.label17);
            this.grbStats.Controls.Add(this.label16);
            this.grbStats.Controls.Add(this.label1);
            this.grbStats.Location = new System.Drawing.Point(12, 565);
            this.grbStats.Name = "grbStats";
            this.grbStats.Size = new System.Drawing.Size(395, 92);
            this.grbStats.TabIndex = 45;
            this.grbStats.TabStop = false;
            this.grbStats.Text = "Stats";
            // 
            // lblNq
            // 
            this.lblNq.AutoSize = true;
            this.lblNq.Location = new System.Drawing.Point(325, 63);
            this.lblNq.Name = "lblNq";
            this.lblNq.Size = new System.Drawing.Size(33, 13);
            this.lblNq.TabIndex = 31;
            this.lblNq.Text = "value";
            this.lblNq.Visible = false;
            // 
            // lblNp
            // 
            this.lblNp.AutoSize = true;
            this.lblNp.Location = new System.Drawing.Point(325, 46);
            this.lblNp.Name = "lblNp";
            this.lblNp.Size = new System.Drawing.Size(33, 13);
            this.lblNp.TabIndex = 30;
            this.lblNp.Text = "value";
            this.lblNp.Visible = false;
            // 
            // lblNs
            // 
            this.lblNs.AutoSize = true;
            this.lblNs.Location = new System.Drawing.Point(325, 31);
            this.lblNs.Name = "lblNs";
            this.lblNs.Size = new System.Drawing.Size(33, 13);
            this.lblNs.TabIndex = 29;
            this.lblNs.Text = "value";
            this.lblNs.Visible = false;
            // 
            // lblX
            // 
            this.lblX.AutoSize = true;
            this.lblX.Location = new System.Drawing.Point(325, 16);
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(33, 13);
            this.lblX.TabIndex = 28;
            this.lblX.Text = "value";
            this.lblX.Visible = false;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.Location = new System.Drawing.Point(300, 63);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(31, 13);
            this.label31.TabIndex = 27;
            this.label31.Text = "Nq :";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(300, 46);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(31, 13);
            this.label32.TabIndex = 26;
            this.label32.Text = "Np :";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.Location = new System.Drawing.Point(301, 31);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(30, 13);
            this.label33.TabIndex = 25;
            this.label33.Text = "Ns :";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.Location = new System.Drawing.Point(307, 16);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(23, 13);
            this.label34.TabIndex = 24;
            this.label34.Text = "X :";
            // 
            // lblFDRq
            // 
            this.lblFDRq.AutoSize = true;
            this.lblFDRq.Location = new System.Drawing.Point(240, 63);
            this.lblFDRq.Name = "lblFDRq";
            this.lblFDRq.Size = new System.Drawing.Size(33, 13);
            this.lblFDRq.TabIndex = 23;
            this.lblFDRq.Text = "value";
            this.lblFDRq.Visible = false;
            // 
            // lblZq
            // 
            this.lblZq.AutoSize = true;
            this.lblZq.Location = new System.Drawing.Point(240, 46);
            this.lblZq.Name = "lblZq";
            this.lblZq.Size = new System.Drawing.Size(33, 13);
            this.lblZq.TabIndex = 22;
            this.lblZq.Text = "value";
            this.lblZq.Visible = false;
            // 
            // lblWq
            // 
            this.lblWq.AutoSize = true;
            this.lblWq.Location = new System.Drawing.Point(240, 31);
            this.lblWq.Name = "lblWq";
            this.lblWq.Size = new System.Drawing.Size(33, 13);
            this.lblWq.TabIndex = 21;
            this.lblWq.Text = "value";
            this.lblWq.Visible = false;
            // 
            // lblXq
            // 
            this.lblXq.AutoSize = true;
            this.lblXq.Location = new System.Drawing.Point(240, 16);
            this.lblXq.Name = "lblXq";
            this.lblXq.Size = new System.Drawing.Size(33, 13);
            this.lblXq.TabIndex = 20;
            this.lblXq.Text = "value";
            this.lblXq.Visible = false;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(197, 63);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(47, 13);
            this.label27.TabIndex = 19;
            this.label27.Text = "FDRq :";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(212, 46);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(30, 13);
            this.label28.TabIndex = 18;
            this.label28.Text = "Zq :";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(208, 31);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(34, 13);
            this.label29.TabIndex = 17;
            this.label29.Text = "Wq :";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.Location = new System.Drawing.Point(212, 16);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(30, 13);
            this.label30.TabIndex = 16;
            this.label30.Text = "Xq :";
            // 
            // lblFDRp
            // 
            this.lblFDRp.AutoSize = true;
            this.lblFDRp.Location = new System.Drawing.Point(139, 63);
            this.lblFDRp.Name = "lblFDRp";
            this.lblFDRp.Size = new System.Drawing.Size(33, 13);
            this.lblFDRp.TabIndex = 15;
            this.lblFDRp.Text = "value";
            this.lblFDRp.Visible = false;
            // 
            // lblZp
            // 
            this.lblZp.AutoSize = true;
            this.lblZp.Location = new System.Drawing.Point(139, 46);
            this.lblZp.Name = "lblZp";
            this.lblZp.Size = new System.Drawing.Size(33, 13);
            this.lblZp.TabIndex = 14;
            this.lblZp.Text = "value";
            this.lblZp.Visible = false;
            // 
            // lblWp
            // 
            this.lblWp.AutoSize = true;
            this.lblWp.Location = new System.Drawing.Point(139, 31);
            this.lblWp.Name = "lblWp";
            this.lblWp.Size = new System.Drawing.Size(33, 13);
            this.lblWp.TabIndex = 13;
            this.lblWp.Text = "value";
            this.lblWp.Visible = false;
            // 
            // lblXp
            // 
            this.lblXp.AutoSize = true;
            this.lblXp.Location = new System.Drawing.Point(139, 16);
            this.lblXp.Name = "lblXp";
            this.lblXp.Size = new System.Drawing.Size(33, 13);
            this.lblXp.TabIndex = 12;
            this.lblXp.Text = "value";
            this.lblXp.Visible = false;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(98, 63);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(47, 13);
            this.label23.TabIndex = 11;
            this.label23.Text = "FDRp :";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(113, 46);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(30, 13);
            this.label24.TabIndex = 10;
            this.label24.Text = "Zp :";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(109, 31);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(34, 13);
            this.label25.TabIndex = 9;
            this.label25.Text = "Wp :";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(113, 16);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(30, 13);
            this.label26.TabIndex = 8;
            this.label26.Text = "Xp :";
            // 
            // lblFDRs
            // 
            this.lblFDRs.AutoSize = true;
            this.lblFDRs.Location = new System.Drawing.Point(46, 63);
            this.lblFDRs.Name = "lblFDRs";
            this.lblFDRs.Size = new System.Drawing.Size(33, 13);
            this.lblFDRs.TabIndex = 7;
            this.lblFDRs.Text = "value";
            this.lblFDRs.Visible = false;
            // 
            // lblZs
            // 
            this.lblZs.AutoSize = true;
            this.lblZs.Location = new System.Drawing.Point(46, 46);
            this.lblZs.Name = "lblZs";
            this.lblZs.Size = new System.Drawing.Size(33, 13);
            this.lblZs.TabIndex = 6;
            this.lblZs.Text = "value";
            this.lblZs.Visible = false;
            // 
            // lblWs
            // 
            this.lblWs.AutoSize = true;
            this.lblWs.Location = new System.Drawing.Point(46, 31);
            this.lblWs.Name = "lblWs";
            this.lblWs.Size = new System.Drawing.Size(33, 13);
            this.lblWs.TabIndex = 5;
            this.lblWs.Text = "value";
            this.lblWs.Visible = false;
            // 
            // lblXs
            // 
            this.lblXs.AutoSize = true;
            this.lblXs.Location = new System.Drawing.Point(46, 16);
            this.lblXs.Name = "lblXs";
            this.lblXs.Size = new System.Drawing.Size(33, 13);
            this.lblXs.TabIndex = 4;
            this.lblXs.Text = "value";
            this.lblXs.Visible = false;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(6, 63);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(46, 13);
            this.label18.TabIndex = 3;
            this.label18.Text = "FDRs :";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(21, 44);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(29, 13);
            this.label17.TabIndex = 2;
            this.label17.Text = "Zs :";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(17, 31);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(33, 13);
            this.label16.TabIndex = 1;
            this.label16.Text = "Ws :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(21, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Xs :";
            // 
            // grbPepId
            // 
            this.grbPepId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.grbPepId.Controls.Add(this.lblF);
            this.grbPepId.Controls.Add(this.titF);
            this.grbPepId.Controls.Add(this.lblVs);
            this.grbPepId.Controls.Add(this.titVs);
            this.grbPepId.Controls.Add(this.lblFDR);
            this.grbPepId.Controls.Add(this.titFDR);
            this.grbPepId.Controls.Add(this.lblDCn);
            this.grbPepId.Controls.Add(this.titDCn);
            this.grbPepId.Controls.Add(this.lblXcorr);
            this.grbPepId.Controls.Add(this.titXcorr);
            this.grbPepId.Location = new System.Drawing.Point(413, 565);
            this.grbPepId.Name = "grbPepId";
            this.grbPepId.Size = new System.Drawing.Size(111, 92);
            this.grbPepId.TabIndex = 5;
            this.grbPepId.TabStop = false;
            this.grbPepId.Text = "Pep. Id.";
            // 
            // lblF
            // 
            this.lblF.AutoSize = true;
            this.lblF.Location = new System.Drawing.Point(53, 72);
            this.lblF.Name = "lblF";
            this.lblF.Size = new System.Drawing.Size(33, 13);
            this.lblF.TabIndex = 14;
            this.lblF.Text = "value";
            this.lblF.Visible = false;
            // 
            // titF
            // 
            this.titF.AutoSize = true;
            this.titF.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titF.Location = new System.Drawing.Point(6, 72);
            this.titF.Name = "titF";
            this.titF.Size = new System.Drawing.Size(19, 13);
            this.titF.TabIndex = 13;
            this.titF.Text = "f :";
            // 
            // lblVs
            // 
            this.lblVs.AutoSize = true;
            this.lblVs.Location = new System.Drawing.Point(53, 60);
            this.lblVs.Name = "lblVs";
            this.lblVs.Size = new System.Drawing.Size(33, 13);
            this.lblVs.TabIndex = 12;
            this.lblVs.Text = "value";
            this.lblVs.Visible = false;
            // 
            // titVs
            // 
            this.titVs.AutoSize = true;
            this.titVs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titVs.Location = new System.Drawing.Point(6, 60);
            this.titVs.Name = "titVs";
            this.titVs.Size = new System.Drawing.Size(29, 13);
            this.titVs.TabIndex = 11;
            this.titVs.Text = "Vs :";
            // 
            // lblFDR
            // 
            this.lblFDR.AutoSize = true;
            this.lblFDR.Location = new System.Drawing.Point(53, 42);
            this.lblFDR.Name = "lblFDR";
            this.lblFDR.Size = new System.Drawing.Size(33, 13);
            this.lblFDR.TabIndex = 10;
            this.lblFDR.Text = "value";
            this.lblFDR.Visible = false;
            // 
            // titFDR
            // 
            this.titFDR.AutoSize = true;
            this.titFDR.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titFDR.Location = new System.Drawing.Point(6, 42);
            this.titFDR.Name = "titFDR";
            this.titFDR.Size = new System.Drawing.Size(40, 13);
            this.titFDR.TabIndex = 9;
            this.titFDR.Text = "FDR :";
            // 
            // lblDCn
            // 
            this.lblDCn.AutoSize = true;
            this.lblDCn.Location = new System.Drawing.Point(53, 29);
            this.lblDCn.Name = "lblDCn";
            this.lblDCn.Size = new System.Drawing.Size(33, 13);
            this.lblDCn.TabIndex = 8;
            this.lblDCn.Text = "value";
            this.lblDCn.Visible = false;
            // 
            // titDCn
            // 
            this.titDCn.AutoSize = true;
            this.titDCn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titDCn.Location = new System.Drawing.Point(6, 29);
            this.titDCn.Name = "titDCn";
            this.titDCn.Size = new System.Drawing.Size(39, 13);
            this.titDCn.TabIndex = 9;
            this.titDCn.Text = "DCn :";
            // 
            // lblXcorr
            // 
            this.lblXcorr.AutoSize = true;
            this.lblXcorr.Location = new System.Drawing.Point(53, 16);
            this.lblXcorr.Name = "lblXcorr";
            this.lblXcorr.Size = new System.Drawing.Size(33, 13);
            this.lblXcorr.TabIndex = 2;
            this.lblXcorr.Text = "value";
            this.lblXcorr.Visible = false;
            // 
            // titXcorr
            // 
            this.titXcorr.AutoSize = true;
            this.titXcorr.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titXcorr.Location = new System.Drawing.Point(6, 16);
            this.titXcorr.Name = "titXcorr";
            this.titXcorr.Size = new System.Drawing.Size(45, 13);
            this.titXcorr.TabIndex = 1;
            this.titXcorr.Text = "Xcorr :";
            // 
            // sortTxt
            // 
            this.sortTxt.Location = new System.Drawing.Point(139, 61);
            this.sortTxt.Name = "sortTxt";
            this.sortTxt.Size = new System.Drawing.Size(468, 20);
            this.sortTxt.TabIndex = 4;
            this.sortTxt.TextChanged += new System.EventHandler(this.sortTxt_TextChanged);
            this.sortTxt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.sortTxt_KeyPress);
            // 
            // chkShowStats
            // 
            this.chkShowStats.AutoSize = true;
            this.chkShowStats.Location = new System.Drawing.Point(12, 129);
            this.chkShowStats.Name = "chkShowStats";
            this.chkShowStats.Size = new System.Drawing.Size(159, 17);
            this.chkShowStats.TabIndex = 8;
            this.chkShowStats.Text = "show only data used in stats";
            this.chkShowStats.UseVisualStyleBackColor = true;
            this.chkShowStats.CheckedChanged += new System.EventHandler(this.chkShowStats_CheckedChanged);
            // 
            // chkHideBadQuality
            // 
            this.chkHideBadQuality.AutoSize = true;
            this.chkHideBadQuality.Location = new System.Drawing.Point(179, 129);
            this.chkHideBadQuality.Name = "chkHideBadQuality";
            this.chkHideBadQuality.Size = new System.Drawing.Size(139, 17);
            this.chkHideBadQuality.TabIndex = 9;
            this.chkHideBadQuality.Text = "hide manually discarded";
            this.chkHideBadQuality.UseVisualStyleBackColor = true;
            this.chkHideBadQuality.CheckedChanged += new System.EventHandler(this.chkHideBadQuality_CheckedChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(622, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "# of rows : ";
            // 
            // lblSpecCount
            // 
            this.lblSpecCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSpecCount.AutoSize = true;
            this.lblSpecCount.Location = new System.Drawing.Point(677, 133);
            this.lblSpecCount.Name = "lblSpecCount";
            this.lblSpecCount.Size = new System.Drawing.Size(13, 13);
            this.lblSpecCount.TabIndex = 0;
            this.lblSpecCount.Text = "--";
            // 
            // reloadConfFilesBtn
            // 
            this.reloadConfFilesBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.reloadConfFilesBtn.Location = new System.Drawing.Point(858, 118);
            this.reloadConfFilesBtn.Name = "reloadConfFilesBtn";
            this.reloadConfFilesBtn.Size = new System.Drawing.Size(91, 21);
            this.reloadConfFilesBtn.TabIndex = 10;
            this.reloadConfFilesBtn.Text = "reload conf files";
            this.reloadConfFilesBtn.UseVisualStyleBackColor = true;
            this.reloadConfFilesBtn.Click += new System.EventHandler(this.reloadConfFilesBtn_Click);
            // 
            // grpSampleData
            // 
            this.grpSampleData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grpSampleData.Controls.Add(this.lblNproteins);
            this.grpSampleData.Controls.Add(this.lblNpeptides);
            this.grpSampleData.Controls.Add(this.lblNscans);
            this.grpSampleData.Controls.Add(this.label7);
            this.grpSampleData.Controls.Add(this.label9);
            this.grpSampleData.Controls.Add(this.label11);
            this.grpSampleData.Location = new System.Drawing.Point(666, 6);
            this.grpSampleData.Name = "grpSampleData";
            this.grpSampleData.Size = new System.Drawing.Size(124, 77);
            this.grpSampleData.TabIndex = 54;
            this.grpSampleData.TabStop = false;
            this.grpSampleData.Text = "sample data";
            this.grpSampleData.Visible = false;
            // 
            // lblNproteins
            // 
            this.lblNproteins.AutoSize = true;
            this.lblNproteins.Location = new System.Drawing.Point(81, 54);
            this.lblNproteins.Name = "lblNproteins";
            this.lblNproteins.Size = new System.Drawing.Size(33, 13);
            this.lblNproteins.TabIndex = 37;
            this.lblNproteins.Text = "value";
            // 
            // lblNpeptides
            // 
            this.lblNpeptides.AutoSize = true;
            this.lblNpeptides.Location = new System.Drawing.Point(81, 37);
            this.lblNpeptides.Name = "lblNpeptides";
            this.lblNpeptides.Size = new System.Drawing.Size(33, 13);
            this.lblNpeptides.TabIndex = 36;
            this.lblNpeptides.Text = "value";
            // 
            // lblNscans
            // 
            this.lblNscans.AutoSize = true;
            this.lblNscans.Location = new System.Drawing.Point(81, 22);
            this.lblNscans.Name = "lblNscans";
            this.lblNscans.Size = new System.Drawing.Size(33, 13);
            this.lblNscans.TabIndex = 35;
            this.lblNscans.Text = "value";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(3, 54);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 13);
            this.label7.TabIndex = 34;
            this.label7.Text = "# proteins :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(3, 37);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(75, 13);
            this.label9.TabIndex = 33;
            this.label9.Text = "# peptides :";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(3, 22);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(60, 13);
            this.label11.TabIndex = 32;
            this.label11.Text = "# scans :";
            // 
            // btnScans
            // 
            this.btnScans.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnScans.FlatAppearance.BorderColor = System.Drawing.Color.Brown;
            this.btnScans.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.btnScans.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.btnScans.Location = new System.Drawing.Point(329, 125);
            this.btnScans.Margin = new System.Windows.Forms.Padding(1);
            this.btnScans.Name = "btnScans";
            this.btnScans.Size = new System.Drawing.Size(55, 22);
            this.btnScans.TabIndex = 55;
            this.btnScans.Text = "scans";
            this.btnScans.UseVisualStyleBackColor = false;
            this.btnScans.Click += new System.EventHandler(this.btnScans_Click);
            // 
            // btnProteins
            // 
            this.btnProteins.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnProteins.FlatAppearance.BorderColor = System.Drawing.Color.Brown;
            this.btnProteins.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.btnProteins.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.btnProteins.Location = new System.Drawing.Point(443, 125);
            this.btnProteins.Margin = new System.Windows.Forms.Padding(1);
            this.btnProteins.Name = "btnProteins";
            this.btnProteins.Size = new System.Drawing.Size(55, 22);
            this.btnProteins.TabIndex = 56;
            this.btnProteins.Text = "proteins";
            this.btnProteins.UseVisualStyleBackColor = false;
            this.btnProteins.Click += new System.EventHandler(this.btnProteins_Click);
            // 
            // btnPeptides
            // 
            this.btnPeptides.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnPeptides.FlatAppearance.BorderColor = System.Drawing.Color.Brown;
            this.btnPeptides.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.btnPeptides.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.btnPeptides.Location = new System.Drawing.Point(386, 125);
            this.btnPeptides.Margin = new System.Windows.Forms.Padding(1);
            this.btnPeptides.Name = "btnPeptides";
            this.btnPeptides.Size = new System.Drawing.Size(55, 22);
            this.btnPeptides.TabIndex = 57;
            this.btnPeptides.Text = "peptides";
            this.btnPeptides.UseVisualStyleBackColor = false;
            this.btnPeptides.Click += new System.EventHandler(this.btnPeptides_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(714, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "# of  selected rows : ";
            // 
            // lblSelectedRows
            // 
            this.lblSelectedRows.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSelectedRows.AutoSize = true;
            this.lblSelectedRows.Location = new System.Drawing.Point(819, 133);
            this.lblSelectedRows.Name = "lblSelectedRows";
            this.lblSelectedRows.Size = new System.Drawing.Size(13, 13);
            this.lblSelectedRows.TabIndex = 0;
            this.lblSelectedRows.Text = "--";
            // 
            // OPquan
            // 
            this.AllowDrop = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(197)))), ((int)(((byte)(222)))));
            this.ClientSize = new System.Drawing.Size(962, 665);
            this.Controls.Add(this.btnPeptides);
            this.Controls.Add(this.btnProteins);
            this.Controls.Add(this.btnScans);
            this.Controls.Add(this.grpSampleData);
            this.Controls.Add(this.reloadConfFilesBtn);
            this.Controls.Add(this.lblSelectedRows);
            this.Controls.Add(this.lblSpecCount);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chkHideBadQuality);
            this.Controls.Add(this.chkShowStats);
            this.Controls.Add(this.sortTxt);
            this.Controls.Add(this.grbPepId);
            this.Controls.Add(this.grbStats);
            this.Controls.Add(this.btnSort);
            this.Controls.Add(this.btnLookForIdXml);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.barsPBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.filterBtn);
            this.Controls.Add(this.filterTxt);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.loadIdfileBtn);
            this.Controls.Add(this.idfileTxt);
            this.Controls.Add(this.label2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(900, 410);
            this.Name = "OPquan";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "QuiXoT";
            this.Load += new System.EventHandler(this.OPquan_Load);
            this.ResizeBegin += new System.EventHandler(this.OPquan_ResizeBegin);
            this.SizeChanged += new System.EventHandler(this.OPquan_SizeChanged);
            this.DoubleClick += new System.EventHandler(this.OPquan_DoubleClick);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.OPquan_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.OPquan_DragEnter);
            this.ResizeEnd += new System.EventHandler(this.OPquan_ResizeEnd);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.idsGrid)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barsPBox)).EndInit();
            this.grbStats.ResumeLayout(false);
            this.grbStats.PerformLayout();
            this.grbPepId.ResumeLayout(false);
            this.grbPepId.PerformLayout();
            this.grpSampleData.ResumeLayout(false);
            this.grpSampleData.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

    

      




        #endregion

        private System.Windows.Forms.Button loadIdfileBtn;
        private System.Windows.Forms.TextBox idfileTxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGrid idsGrid;
        private System.Windows.Forms.Button ButtonDataFilter;
        private System.Windows.Forms.Button ButtonColumnFilter;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button filterBtn;
        private System.Windows.Forms.Button graphBtn;
        private System.Windows.Forms.Label quanPrtxt;
        private System.Windows.Forms.ProgressBar quantifPrBar;
        private System.Windows.Forms.Label quanPercenttxt;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Button writeXMLBtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckedListBox checklist;
        private System.Windows.Forms.Button btnStats;
        private System.Windows.Forms.PictureBox barsPBox;
        private System.Windows.Forms.Button stGraphBtn;
        private System.Windows.Forms.Button writeCSVbtn;
        private System.Windows.Forms.Button NGbtn;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnDeployRed;
        private System.Windows.Forms.LinkLabel lnPeptCts;
        private System.Windows.Forms.Button btnChangeColVal;
        private System.Windows.Forms.Button btnLookForIdXml;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label txtValSigma2Q;
        private System.Windows.Forms.Label txtValSigma2P;
        private System.Windows.Forms.Label txtValSigma2S;
        private System.Windows.Forms.Label txtValK;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label10;
        private Button btnSort;
        private GroupBox grbStats;
        private Label lblXs;
        private Label label18;
        private Label label17;
        private Label label16;
        private Label label1;
        private Label lblNq;
        private Label lblNp;
        private Label lblNs;
        private Label lblX;
        private Label label31;
        private Label label32;
        private Label label33;
        private Label label34;
        private Label lblFDRq;
        private Label lblZq;
        private Label lblWq;
        private Label lblXq;
        private Label label27;
        private Label label28;
        private Label label29;
        private Label label30;
        private Label lblFDRp;
        private Label lblZp;
        private Label lblWp;
        private Label lblXp;
        private Label label23;
        private Label label24;
        private Label label25;
        private Label label26;
        private Label lblFDRs;
        private Label lblZs;
        private Label lblWs;
        private GroupBox grbPepId;
        private Label lblF;
        private Label titF;
        private Label lblVs;
        private Label titVs;
        private Label lblFDR;
        private Label titFDR;
        private Label lblDCn;
        private Label titDCn;
        private Label lblXcorr;
        private Label titXcorr;
        private Button btnVarCalc;
        private TextBox sortTxt;
        private CheckBox chkShowStats;
        private CheckBox chkHideBadQuality;
        public TextBox filterTxt;
        private Label label3;
        private Label lblSpecCount;
        private Button reloadConfFilesBtn;
        private GroupBox grpSampleData;
        private Label lblNproteins;
        private Label lblNpeptides;
        private Label lblNscans;
        private Label label7;
        private Label label9;
        private Label label11;
        private Button btnScans;
        private Button btnProteins;
        private Button btnPeptides;
        private Label label4;
        private Label lblSelectedRows;
        private Label txtValPhi;
        private Label txtPhi;
        private Button btnVarConf;
      
        

    }
}