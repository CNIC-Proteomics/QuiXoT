namespace QuiXoT
{
    partial class OPstatsPreview
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
            this.chkMethionines = new System.Windows.Forms.CheckBox();
            this.chkCTerminal = new System.Windows.Forms.CheckBox();
            this.chkPartialDig = new System.Windows.Forms.CheckBox();
            this.chkQuality = new System.Windows.Forms.CheckBox();
            this.chkEfficiency = new System.Windows.Forms.CheckBox();
            this.chkWs = new System.Windows.Forms.CheckBox();
            this.txtF = new System.Windows.Forms.TextBox();
            this.chkFDR = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtWs = new System.Windows.Forms.TextBox();
            this.txtFdr = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.chkPartialDigSubp = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbXs = new System.Windows.Forms.ComboBox();
            this.cmbVs = new System.Windows.Forms.ComboBox();
            this.filterTxt = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnLoadPrevFilter = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkMSCwithSubPep = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnLoadPrevCols = new System.Windows.Forms.Button();
            this.chkArgPro = new System.Windows.Forms.CheckBox();
            this.lblSilacFDRq = new System.Windows.Forms.Label();
            this.txtSilacFdrq = new System.Windows.Forms.TextBox();
            this.grpSilac = new System.Windows.Forms.GroupBox();
            this.forceX = new System.Windows.Forms.CheckBox();
            this.forceXtxt = new System.Windows.Forms.TextBox();
            this.basicOptions = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ignoreS_s2plbl = new System.Windows.Forms.Label();
            this.ignoreS_s2ptxt = new System.Windows.Forms.TextBox();
            this.colWqcmb = new System.Windows.Forms.ComboBox();
            this.colWpcmb = new System.Windows.Forms.ComboBox();
            this.ignScans = new System.Windows.Forms.CheckBox();
            this.ignPeptides = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.grpSilac.SuspendLayout();
            this.basicOptions.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkMethionines
            // 
            this.chkMethionines.AutoSize = true;
            this.chkMethionines.Location = new System.Drawing.Point(8, 51);
            this.chkMethionines.Name = "chkMethionines";
            this.chkMethionines.Size = new System.Drawing.Size(217, 17);
            this.chkMethionines.TabIndex = 0;
            this.chkMethionines.Text = "exclude peptides containing methionines";
            this.chkMethionines.UseVisualStyleBackColor = true;
            this.chkMethionines.CheckedChanged += new System.EventHandler(this.chkMethionines_CheckedChanged);
            // 
            // chkCTerminal
            // 
            this.chkCTerminal.AutoSize = true;
            this.chkCTerminal.Location = new System.Drawing.Point(8, 75);
            this.chkCTerminal.Name = "chkCTerminal";
            this.chkCTerminal.Size = new System.Drawing.Size(119, 17);
            this.chkCTerminal.TabIndex = 1;
            this.chkCTerminal.Text = "C-Terminal peptides";
            this.chkCTerminal.UseVisualStyleBackColor = true;
            this.chkCTerminal.CheckedChanged += new System.EventHandler(this.chkCTerminal_CheckedChanged);
            // 
            // chkPartialDig
            // 
            this.chkPartialDig.AutoSize = true;
            this.chkPartialDig.Location = new System.Drawing.Point(5, 22);
            this.chkPartialDig.Name = "chkPartialDig";
            this.chkPartialDig.Size = new System.Drawing.Size(110, 17);
            this.chkPartialDig.TabIndex = 0;
            this.chkPartialDig.Text = "missed cleavages";
            this.chkPartialDig.UseVisualStyleBackColor = true;
            this.chkPartialDig.CheckedChanged += new System.EventHandler(this.chkPartialDig_CheckedChanged);
            // 
            // chkQuality
            // 
            this.chkQuality.AutoSize = true;
            this.chkQuality.Location = new System.Drawing.Point(279, 51);
            this.chkQuality.Name = "chkQuality";
            this.chkQuality.Size = new System.Drawing.Size(187, 17);
            this.chkQuality.TabIndex = 2;
            this.chkQuality.Text = "bad quality scans (numLabel1 = 0)";
            this.chkQuality.UseVisualStyleBackColor = true;
            this.chkQuality.CheckedChanged += new System.EventHandler(this.chkQuality_CheckedChanged);
            // 
            // chkEfficiency
            // 
            this.chkEfficiency.AutoSize = true;
            this.chkEfficiency.Location = new System.Drawing.Point(279, 74);
            this.chkEfficiency.Name = "chkEfficiency";
            this.chkEfficiency.Size = new System.Drawing.Size(185, 17);
            this.chkEfficiency.TabIndex = 3;
            this.chkEfficiency.Text = "scans with efficiency lower than : ";
            this.chkEfficiency.UseVisualStyleBackColor = true;
            this.chkEfficiency.CheckedChanged += new System.EventHandler(this.chkEfficiency_CheckedChanged);
            // 
            // chkWs
            // 
            this.chkWs.AutoSize = true;
            this.chkWs.Location = new System.Drawing.Point(279, 97);
            this.chkWs.Name = "chkWs";
            this.chkWs.Size = new System.Drawing.Size(149, 17);
            this.chkWs.TabIndex = 5;
            this.chkWs.Text = "scans with Vs lower than :";
            this.chkWs.UseVisualStyleBackColor = true;
            this.chkWs.CheckedChanged += new System.EventHandler(this.chkWs_CheckedChanged);
            // 
            // txtF
            // 
            this.txtF.Enabled = false;
            this.txtF.Location = new System.Drawing.Point(512, 71);
            this.txtF.Name = "txtF";
            this.txtF.Size = new System.Drawing.Size(50, 20);
            this.txtF.TabIndex = 4;
            this.txtF.TextChanged += new System.EventHandler(this.txtF_TextChanged);
            // 
            // chkFDR
            // 
            this.chkFDR.AutoSize = true;
            this.chkFDR.Location = new System.Drawing.Point(279, 120);
            this.chkFDR.Name = "chkFDR";
            this.chkFDR.Size = new System.Drawing.Size(227, 17);
            this.chkFDR.TabIndex = 7;
            this.chkFDR.Text = "scans identified with an FDR greater than :";
            this.chkFDR.UseVisualStyleBackColor = true;
            this.chkFDR.CheckedChanged += new System.EventHandler(this.chkFDR_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(32, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 15);
            this.label2.TabIndex = 14;
            this.label2.Text = "peptide properties";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(323, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 15);
            this.label3.TabIndex = 15;
            this.label3.Text = "scan properties";
            // 
            // txtWs
            // 
            this.txtWs.Enabled = false;
            this.txtWs.Location = new System.Drawing.Point(512, 94);
            this.txtWs.Name = "txtWs";
            this.txtWs.Size = new System.Drawing.Size(50, 20);
            this.txtWs.TabIndex = 6;
            this.txtWs.TextChanged += new System.EventHandler(this.txtWs_TextChanged);
            // 
            // txtFdr
            // 
            this.txtFdr.Enabled = false;
            this.txtFdr.Location = new System.Drawing.Point(512, 117);
            this.txtFdr.Name = "txtFdr";
            this.txtFdr.Size = new System.Drawing.Size(50, 20);
            this.txtFdr.TabIndex = 8;
            this.txtFdr.TextChanged += new System.EventHandler(this.txtFdr_TextChanged);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(230, 477);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(106, 25);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "calculate statistics";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // chkPartialDigSubp
            // 
            this.chkPartialDigSubp.AutoSize = true;
            this.chkPartialDigSubp.Location = new System.Drawing.Point(5, 45);
            this.chkPartialDigSubp.Name = "chkPartialDigSubp";
            this.chkPartialDigSubp.Size = new System.Drawing.Size(191, 17);
            this.chkPartialDigSubp.TabIndex = 1;
            this.chkPartialDigSubp.Text = "missed cleavages and subpeptides";
            this.chkPartialDigSubp.UseVisualStyleBackColor = true;
            this.chkPartialDigSubp.CheckedChanged += new System.EventHandler(this.chkPartialDigSubp_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(194, 15);
            this.label4.TabIndex = 28;
            this.label4.Text = "Choose the field to be used as Xs :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(193, 15);
            this.label6.TabIndex = 30;
            this.label6.Text = "Choose the field to be used as Vs :";
            // 
            // cmbXs
            // 
            this.cmbXs.FormattingEnabled = true;
            this.cmbXs.Location = new System.Drawing.Point(205, 20);
            this.cmbXs.Name = "cmbXs";
            this.cmbXs.Size = new System.Drawing.Size(174, 21);
            this.cmbXs.TabIndex = 0;
            this.cmbXs.TextChanged += new System.EventHandler(this.cmbXs_TextChanged);
            // 
            // cmbVs
            // 
            this.cmbVs.FormattingEnabled = true;
            this.cmbVs.Location = new System.Drawing.Point(205, 46);
            this.cmbVs.Name = "cmbVs";
            this.cmbVs.Size = new System.Drawing.Size(174, 21);
            this.cmbVs.TabIndex = 1;
            this.cmbVs.TextChanged += new System.EventHandler(this.cmbVs_TextChanged);
            // 
            // filterTxt
            // 
            this.filterTxt.Location = new System.Drawing.Point(83, 306);
            this.filterTxt.Name = "filterTxt";
            this.filterTxt.Size = new System.Drawing.Size(492, 20);
            this.filterTxt.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 309);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 34;
            this.label5.Text = "filter for stats :";
            // 
            // btnLoadPrevFilter
            // 
            this.btnLoadPrevFilter.Location = new System.Drawing.Point(471, 332);
            this.btnLoadPrevFilter.Name = "btnLoadPrevFilter";
            this.btnLoadPrevFilter.Size = new System.Drawing.Size(104, 21);
            this.btnLoadPrevFilter.TabIndex = 4;
            this.btnLoadPrevFilter.Text = "load previous filter";
            this.btnLoadPrevFilter.UseVisualStyleBackColor = true;
            this.btnLoadPrevFilter.Click += new System.EventHandler(this.btnLoadPrevFilter_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkMSCwithSubPep);
            this.groupBox1.Controls.Add(this.chkPartialDig);
            this.groupBox1.Controls.Add(this.chkPartialDigSubp);
            this.groupBox1.Location = new System.Drawing.Point(5, 99);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(268, 108);
            this.groupBox1.TabIndex = 36;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "partial digestions";
            // 
            // chkMSCwithSubPep
            // 
            this.chkMSCwithSubPep.AutoSize = true;
            this.chkMSCwithSubPep.Location = new System.Drawing.Point(4, 68);
            this.chkMSCwithSubPep.Name = "chkMSCwithSubPep";
            this.chkMSCwithSubPep.Size = new System.Drawing.Size(257, 17);
            this.chkMSCwithSubPep.TabIndex = 2;
            this.chkMSCwithSubPep.Text = "missed cleavages with subpeptides in the sample";
            this.chkMSCwithSubPep.UseVisualStyleBackColor = true;
            this.chkMSCwithSubPep.CheckedChanged += new System.EventHandler(this.chkMSCwithSubPep_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Controls.Add(this.txtFdr);
            this.groupBox2.Controls.Add(this.txtWs);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.chkFDR);
            this.groupBox2.Controls.Add(this.txtF);
            this.groupBox2.Controls.Add(this.chkWs);
            this.groupBox2.Controls.Add(this.chkEfficiency);
            this.groupBox2.Controls.Add(this.chkQuality);
            this.groupBox2.Controls.Add(this.chkCTerminal);
            this.groupBox2.Controls.Add(this.chkMethionines);
            this.groupBox2.Location = new System.Drawing.Point(6, 73);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(569, 224);
            this.groupBox2.TabIndex = 37;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Filter suggestions";
            // 
            // btnLoadPrevCols
            // 
            this.btnLoadPrevCols.Location = new System.Drawing.Point(389, 32);
            this.btnLoadPrevCols.Name = "btnLoadPrevCols";
            this.btnLoadPrevCols.Size = new System.Drawing.Size(123, 21);
            this.btnLoadPrevCols.TabIndex = 2;
            this.btnLoadPrevCols.Text = "load previous columns";
            this.btnLoadPrevCols.UseVisualStyleBackColor = true;
            this.btnLoadPrevCols.Click += new System.EventHandler(this.btnLoadPrevCols_Click);
            // 
            // chkArgPro
            // 
            this.chkArgPro.AutoSize = true;
            this.chkArgPro.Location = new System.Drawing.Point(10, 19);
            this.chkArgPro.Name = "chkArgPro";
            this.chkArgPro.Size = new System.Drawing.Size(285, 17);
            this.chkArgPro.TabIndex = 38;
            this.chkArgPro.Text = "SILAC : correction based on the Arg --> Pro conversion";
            this.chkArgPro.UseVisualStyleBackColor = true;
            this.chkArgPro.Visible = false;
            this.chkArgPro.CheckedChanged += new System.EventHandler(this.chkArgPro_CheckedChanged);
            // 
            // lblSilacFDRq
            // 
            this.lblSilacFDRq.AutoSize = true;
            this.lblSilacFDRq.Location = new System.Drawing.Point(301, 20);
            this.lblSilacFDRq.Name = "lblSilacFDRq";
            this.lblSilacFDRq.Size = new System.Drawing.Size(204, 13);
            this.lblSilacFDRq.TabIndex = 39;
            this.lblSilacFDRq.Text = "FDRq threshold to define corr population :";
            this.lblSilacFDRq.Visible = false;
            // 
            // txtSilacFdrq
            // 
            this.txtSilacFdrq.Enabled = false;
            this.txtSilacFdrq.Location = new System.Drawing.Point(503, 17);
            this.txtSilacFdrq.Name = "txtSilacFdrq";
            this.txtSilacFdrq.Size = new System.Drawing.Size(57, 20);
            this.txtSilacFdrq.TabIndex = 40;
            this.txtSilacFdrq.Visible = false;
            // 
            // grpSilac
            // 
            this.grpSilac.Controls.Add(this.chkArgPro);
            this.grpSilac.Controls.Add(this.txtSilacFdrq);
            this.grpSilac.Controls.Add(this.lblSilacFDRq);
            this.grpSilac.Location = new System.Drawing.Point(8, 359);
            this.grpSilac.Name = "grpSilac";
            this.grpSilac.Size = new System.Drawing.Size(569, 73);
            this.grpSilac.TabIndex = 41;
            this.grpSilac.TabStop = false;
            this.grpSilac.Text = "SILAC corrections";
            // 
            // forceX
            // 
            this.forceX.AutoSize = true;
            this.forceX.Location = new System.Drawing.Point(16, 28);
            this.forceX.Name = "forceX";
            this.forceX.Size = new System.Drawing.Size(173, 17);
            this.forceX.TabIndex = 42;
            this.forceX.Text = "force this value as supermean :";
            this.forceX.UseVisualStyleBackColor = true;
            this.forceX.CheckedChanged += new System.EventHandler(this.forceX_CheckedChanged);
            // 
            // forceXtxt
            // 
            this.forceXtxt.Enabled = false;
            this.forceXtxt.Location = new System.Drawing.Point(194, 25);
            this.forceXtxt.Name = "forceXtxt";
            this.forceXtxt.Size = new System.Drawing.Size(50, 20);
            this.forceXtxt.TabIndex = 43;
            this.forceXtxt.Text = "0";
            // 
            // basicOptions
            // 
            this.basicOptions.Controls.Add(this.tabPage1);
            this.basicOptions.Controls.Add(this.tabPage2);
            this.basicOptions.Location = new System.Drawing.Point(2, 0);
            this.basicOptions.Name = "basicOptions";
            this.basicOptions.SelectedIndex = 0;
            this.basicOptions.Size = new System.Drawing.Size(588, 471);
            this.basicOptions.TabIndex = 44;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.grpSilac);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.btnLoadPrevFilter);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.filterTxt);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.cmbXs);
            this.tabPage1.Controls.Add(this.btnLoadPrevCols);
            this.tabPage1.Controls.Add(this.cmbVs);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(580, 445);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "basic options";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Controls.Add(this.forceXtxt);
            this.tabPage2.Controls.Add(this.forceX);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(580, 445);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "advanced";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ignoreS_s2plbl);
            this.groupBox3.Controls.Add(this.ignoreS_s2ptxt);
            this.groupBox3.Controls.Add(this.colWqcmb);
            this.groupBox3.Controls.Add(this.colWpcmb);
            this.groupBox3.Controls.Add(this.ignScans);
            this.groupBox3.Controls.Add(this.ignPeptides);
            this.groupBox3.Location = new System.Drawing.Point(16, 56);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(550, 143);
            this.groupBox3.TabIndex = 47;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "partialStatistics";
            // 
            // ignoreS_s2plbl
            // 
            this.ignoreS_s2plbl.AutoSize = true;
            this.ignoreS_s2plbl.Enabled = false;
            this.ignoreS_s2plbl.Location = new System.Drawing.Point(246, 47);
            this.ignoreS_s2plbl.Name = "ignoreS_s2plbl";
            this.ignoreS_s2plbl.Size = new System.Drawing.Size(141, 13);
            this.ignoreS_s2plbl.TabIndex = 49;
            this.ignoreS_s2plbl.Text = "calculated with a σ²p value :";
            // 
            // ignoreS_s2ptxt
            // 
            this.ignoreS_s2ptxt.Enabled = false;
            this.ignoreS_s2ptxt.Location = new System.Drawing.Point(393, 44);
            this.ignoreS_s2ptxt.Name = "ignoreS_s2ptxt";
            this.ignoreS_s2ptxt.Size = new System.Drawing.Size(74, 20);
            this.ignoreS_s2ptxt.TabIndex = 48;
            // 
            // colWqcmb
            // 
            this.colWqcmb.Enabled = false;
            this.colWqcmb.FormattingEnabled = true;
            this.colWqcmb.Location = new System.Drawing.Point(393, 92);
            this.colWqcmb.Name = "colWqcmb";
            this.colWqcmb.Size = new System.Drawing.Size(151, 21);
            this.colWqcmb.TabIndex = 47;
            this.colWqcmb.TextChanged += new System.EventHandler(this.colWqcmb_TextChanged);
            // 
            // colWpcmb
            // 
            this.colWpcmb.Enabled = false;
            this.colWpcmb.FormattingEnabled = true;
            this.colWpcmb.Location = new System.Drawing.Point(393, 17);
            this.colWpcmb.Name = "colWpcmb";
            this.colWpcmb.Size = new System.Drawing.Size(151, 21);
            this.colWpcmb.TabIndex = 46;
            this.colWpcmb.TextChanged += new System.EventHandler(this.colWpcmb_TextChanged);
            // 
            // ignScans
            // 
            this.ignScans.AutoSize = true;
            this.ignScans.Location = new System.Drawing.Point(6, 19);
            this.ignScans.Name = "ignScans";
            this.ignScans.Size = new System.Drawing.Size(381, 17);
            this.ignScans.TabIndex = 44;
            this.ignScans.Text = "ignore scans -- calculate statistics from the peptide level, assuming Wp as : ";
            this.ignScans.UseVisualStyleBackColor = true;
            this.ignScans.CheckedChanged += new System.EventHandler(this.ignScans_CheckedChanged);
            // 
            // ignPeptides
            // 
            this.ignPeptides.AutoSize = true;
            this.ignPeptides.Location = new System.Drawing.Point(6, 94);
            this.ignPeptides.Name = "ignPeptides";
            this.ignPeptides.Size = new System.Drawing.Size(387, 17);
            this.ignPeptides.TabIndex = 45;
            this.ignPeptides.Text = "ignore peptides -- calculate statistics from the protein level, assuming Wq as :";
            this.ignPeptides.UseVisualStyleBackColor = true;
            this.ignPeptides.CheckedChanged += new System.EventHandler(this.ignPeptides_CheckedChanged);
            // 
            // OPstatsPreview
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 510);
            this.Controls.Add(this.basicOptions);
            this.Controls.Add(this.btnOk);
            this.Name = "OPstatsPreview";
            this.Text = "Calculate statistics preview";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.grpSilac.ResumeLayout(false);
            this.grpSilac.PerformLayout();
            this.basicOptions.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chkMethionines;
        private System.Windows.Forms.CheckBox chkCTerminal;
        private System.Windows.Forms.CheckBox chkPartialDig;
        private System.Windows.Forms.CheckBox chkQuality;
        private System.Windows.Forms.CheckBox chkEfficiency;
        private System.Windows.Forms.CheckBox chkWs;
        private System.Windows.Forms.TextBox txtF;
        private System.Windows.Forms.CheckBox chkFDR;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtWs;
        private System.Windows.Forms.TextBox txtFdr;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.CheckBox chkPartialDigSubp;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbXs;
        private System.Windows.Forms.ComboBox cmbVs;
        private System.Windows.Forms.TextBox filterTxt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnLoadPrevFilter;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkMSCwithSubPep;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnLoadPrevCols;
        private System.Windows.Forms.CheckBox chkArgPro;
        private System.Windows.Forms.Label lblSilacFDRq;
        private System.Windows.Forms.TextBox txtSilacFdrq;
        private System.Windows.Forms.GroupBox grpSilac;
        private System.Windows.Forms.CheckBox forceX;
        private System.Windows.Forms.TextBox forceXtxt;
        private System.Windows.Forms.TabControl basicOptions;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox ignScans;
        private System.Windows.Forms.CheckBox ignPeptides;
        private System.Windows.Forms.ComboBox colWqcmb;
        private System.Windows.Forms.ComboBox colWpcmb;
        private System.Windows.Forms.Label ignoreS_s2plbl;
        private System.Windows.Forms.TextBox ignoreS_s2ptxt;
    }
}