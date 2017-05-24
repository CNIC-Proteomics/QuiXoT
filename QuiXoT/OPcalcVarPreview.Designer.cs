namespace QuiXoT
{
    partial class OPcalcVarPreview
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OPcalcVarPreview));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.vscheck = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.wqTxt = new System.Windows.Forms.TextBox();
            this.wqcheck = new System.Windows.Forms.CheckBox();
            this.wpTxt = new System.Windows.Forms.TextBox();
            this.wpcheck = new System.Windows.Forms.CheckBox();
            this.vsTxt = new System.Windows.Forms.TextBox();
            this.kTxt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.filterTxt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbXs = new System.Windows.Forms.ComboBox();
            this.btnLoadPrevCols = new System.Windows.Forms.Button();
            this.cmbVs = new System.Windows.Forms.ComboBox();
            this.btnLoadPrevFilter = new System.Windows.Forms.Button();
            this.tabOptions = new System.Windows.Forms.TabControl();
            this.basic = new System.Windows.Forms.TabPage();
            this.advanced = new System.Windows.Forms.TabPage();
            this.saveDefault = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.sigma2qdelta = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.sigma2qcicles = new System.Windows.Forms.MaskedTextBox();
            this.sigma2qmax = new System.Windows.Forms.TextBox();
            this.sigma2qmin = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.sigma2pdelta = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.sigma2pcicles = new System.Windows.Forms.MaskedTextBox();
            this.sigma2pmax = new System.Windows.Forms.TextBox();
            this.sigma2pmin = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.sigma2sDelta = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.sigma2sCicles = new System.Windows.Forms.MaskedTextBox();
            this.sigma2smax = new System.Windows.Forms.TextBox();
            this.sigma2smin = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblSigmaq = new System.Windows.Forms.Label();
            this.sigmaq = new System.Windows.Forms.TextBox();
            this.calSigmas = new System.Windows.Forms.CheckBox();
            this.lblsigmap = new System.Windows.Forms.Label();
            this.lblsigmas = new System.Windows.Forms.Label();
            this.sigmap = new System.Windows.Forms.TextBox();
            this.sigmas = new System.Windows.Forms.TextBox();
            this.calSigmap = new System.Windows.Forms.CheckBox();
            this.calSigmaq = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.tabOptions.SuspendLayout();
            this.basic.SuspendLayout();
            this.advanced.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(374, 189);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(68, 26);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(300, 189);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(68, 26);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // vscheck
            // 
            this.vscheck.AutoSize = true;
            this.vscheck.Location = new System.Drawing.Point(12, 19);
            this.vscheck.Name = "vscheck";
            this.vscheck.Size = new System.Drawing.Size(160, 17);
            this.vscheck.TabIndex = 6;
            this.vscheck.Text = "use a minimum value for Vs :";
            this.vscheck.UseVisualStyleBackColor = true;
            this.vscheck.CheckedChanged += new System.EventHandler(this.vscheck_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.wqTxt);
            this.groupBox1.Controls.Add(this.wqcheck);
            this.groupBox1.Controls.Add(this.wpTxt);
            this.groupBox1.Controls.Add(this.wpcheck);
            this.groupBox1.Controls.Add(this.vsTxt);
            this.groupBox1.Controls.Add(this.vscheck);
            this.groupBox1.Location = new System.Drawing.Point(6, 151);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(266, 100);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "options";
            this.groupBox1.Visible = false;
            // 
            // wqTxt
            // 
            this.wqTxt.Enabled = false;
            this.wqTxt.Location = new System.Drawing.Point(178, 40);
            this.wqTxt.Name = "wqTxt";
            this.wqTxt.Size = new System.Drawing.Size(73, 20);
            this.wqTxt.TabIndex = 13;
            // 
            // wqcheck
            // 
            this.wqcheck.AutoSize = true;
            this.wqcheck.Location = new System.Drawing.Point(12, 42);
            this.wqcheck.Name = "wqcheck";
            this.wqcheck.Size = new System.Drawing.Size(165, 17);
            this.wqcheck.TabIndex = 12;
            this.wqcheck.Text = "use a minimum value for Wq :";
            this.wqcheck.UseVisualStyleBackColor = true;
            this.wqcheck.CheckedChanged += new System.EventHandler(this.wqcheck_CheckedChanged);
            // 
            // wpTxt
            // 
            this.wpTxt.Enabled = false;
            this.wpTxt.Location = new System.Drawing.Point(178, 63);
            this.wpTxt.Name = "wpTxt";
            this.wpTxt.Size = new System.Drawing.Size(73, 20);
            this.wpTxt.TabIndex = 9;
            // 
            // wpcheck
            // 
            this.wpcheck.AutoSize = true;
            this.wpcheck.Location = new System.Drawing.Point(12, 66);
            this.wpcheck.Name = "wpcheck";
            this.wpcheck.Size = new System.Drawing.Size(165, 17);
            this.wpcheck.TabIndex = 8;
            this.wpcheck.Text = "use a minimum value for Wp :";
            this.wpcheck.UseVisualStyleBackColor = true;
            this.wpcheck.CheckedChanged += new System.EventHandler(this.wpcheck_CheckedChanged);
            // 
            // vsTxt
            // 
            this.vsTxt.Enabled = false;
            this.vsTxt.Location = new System.Drawing.Point(178, 17);
            this.vsTxt.Name = "vsTxt";
            this.vsTxt.Size = new System.Drawing.Size(73, 20);
            this.vsTxt.TabIndex = 7;
            // 
            // kTxt
            // 
            this.kTxt.Location = new System.Drawing.Point(139, 85);
            this.kTxt.Name = "kTxt";
            this.kTxt.Size = new System.Drawing.Size(73, 20);
            this.kTxt.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Value for the constant K :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(241, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Filter :";
            // 
            // filterTxt
            // 
            this.filterTxt.Location = new System.Drawing.Point(282, 85);
            this.filterTxt.Name = "filterTxt";
            this.filterTxt.Size = new System.Drawing.Size(433, 20);
            this.filterTxt.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(82, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(194, 15);
            this.label4.TabIndex = 34;
            this.label4.Text = "Choose the field to be used as Xs :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(82, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(193, 15);
            this.label6.TabIndex = 35;
            this.label6.Text = "Choose the field to be used as Vs :";
            // 
            // cmbXs
            // 
            this.cmbXs.FormattingEnabled = true;
            this.cmbXs.Location = new System.Drawing.Point(281, 23);
            this.cmbXs.Name = "cmbXs";
            this.cmbXs.Size = new System.Drawing.Size(174, 21);
            this.cmbXs.TabIndex = 31;
            this.cmbXs.TextChanged += new System.EventHandler(this.cmbXs_TextChanged);
            // 
            // btnLoadPrevCols
            // 
            this.btnLoadPrevCols.Location = new System.Drawing.Point(465, 35);
            this.btnLoadPrevCols.Name = "btnLoadPrevCols";
            this.btnLoadPrevCols.Size = new System.Drawing.Size(123, 21);
            this.btnLoadPrevCols.TabIndex = 33;
            this.btnLoadPrevCols.Text = "load previous columns";
            this.btnLoadPrevCols.UseVisualStyleBackColor = true;
            this.btnLoadPrevCols.Click += new System.EventHandler(this.btnLoadPrevCols_Click);
            // 
            // cmbVs
            // 
            this.cmbVs.FormattingEnabled = true;
            this.cmbVs.Location = new System.Drawing.Point(281, 49);
            this.cmbVs.Name = "cmbVs";
            this.cmbVs.Size = new System.Drawing.Size(174, 21);
            this.cmbVs.TabIndex = 32;
            this.cmbVs.TextChanged += new System.EventHandler(this.cmbVs_TextChanged);
            // 
            // btnLoadPrevFilter
            // 
            this.btnLoadPrevFilter.Location = new System.Drawing.Point(611, 111);
            this.btnLoadPrevFilter.Name = "btnLoadPrevFilter";
            this.btnLoadPrevFilter.Size = new System.Drawing.Size(104, 21);
            this.btnLoadPrevFilter.TabIndex = 36;
            this.btnLoadPrevFilter.Text = "load previous filter";
            this.btnLoadPrevFilter.UseVisualStyleBackColor = true;
            this.btnLoadPrevFilter.Click += new System.EventHandler(this.btnLoadPrevFilter_Click);
            // 
            // tabOptions
            // 
            this.tabOptions.Controls.Add(this.basic);
            this.tabOptions.Controls.Add(this.advanced);
            this.tabOptions.Location = new System.Drawing.Point(3, 3);
            this.tabOptions.Name = "tabOptions";
            this.tabOptions.SelectedIndex = 0;
            this.tabOptions.Size = new System.Drawing.Size(774, 180);
            this.tabOptions.TabIndex = 37;
            // 
            // basic
            // 
            this.basic.Controls.Add(this.label4);
            this.basic.Controls.Add(this.btnLoadPrevFilter);
            this.basic.Controls.Add(this.kTxt);
            this.basic.Controls.Add(this.label1);
            this.basic.Controls.Add(this.label6);
            this.basic.Controls.Add(this.filterTxt);
            this.basic.Controls.Add(this.label2);
            this.basic.Controls.Add(this.cmbVs);
            this.basic.Controls.Add(this.cmbXs);
            this.basic.Controls.Add(this.btnLoadPrevCols);
            this.basic.Location = new System.Drawing.Point(4, 22);
            this.basic.Name = "basic";
            this.basic.Padding = new System.Windows.Forms.Padding(3);
            this.basic.Size = new System.Drawing.Size(766, 154);
            this.basic.TabIndex = 0;
            this.basic.Text = "basic options";
            this.basic.UseVisualStyleBackColor = true;
            // 
            // advanced
            // 
            this.advanced.Controls.Add(this.saveDefault);
            this.advanced.Controls.Add(this.groupBox5);
            this.advanced.Controls.Add(this.groupBox4);
            this.advanced.Controls.Add(this.groupBox3);
            this.advanced.Controls.Add(this.groupBox2);
            this.advanced.Controls.Add(this.groupBox1);
            this.advanced.Location = new System.Drawing.Point(4, 22);
            this.advanced.Name = "advanced";
            this.advanced.Padding = new System.Windows.Forms.Padding(3);
            this.advanced.Size = new System.Drawing.Size(766, 154);
            this.advanced.TabIndex = 1;
            this.advanced.Text = "advanced";
            this.advanced.UseVisualStyleBackColor = true;
            // 
            // saveDefault
            // 
            this.saveDefault.Location = new System.Drawing.Point(459, 131);
            this.saveDefault.Name = "saveDefault";
            this.saveDefault.Size = new System.Drawing.Size(142, 20);
            this.saveDefault.TabIndex = 38;
            this.saveDefault.Text = "save fit values as default";
            this.saveDefault.UseVisualStyleBackColor = true;
            this.saveDefault.Click += new System.EventHandler(this.saveDefault_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.sigma2qdelta);
            this.groupBox5.Controls.Add(this.label13);
            this.groupBox5.Controls.Add(this.label14);
            this.groupBox5.Controls.Add(this.label15);
            this.groupBox5.Controls.Add(this.label16);
            this.groupBox5.Controls.Add(this.sigma2qcicles);
            this.groupBox5.Controls.Add(this.sigma2qmax);
            this.groupBox5.Controls.Add(this.sigma2qmin);
            this.groupBox5.Location = new System.Drawing.Point(607, 17);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(151, 111);
            this.groupBox5.TabIndex = 51;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "fit  σ²q";
            // 
            // sigma2qdelta
            // 
            this.sigma2qdelta.Location = new System.Drawing.Point(74, 79);
            this.sigma2qdelta.Name = "sigma2qdelta";
            this.sigma2qdelta.Size = new System.Drawing.Size(63, 20);
            this.sigma2qdelta.TabIndex = 49;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 82);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(68, 13);
            this.label13.TabIndex = 48;
            this.label13.Text = "Δσ²q,1s run :";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(7, 58);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(62, 13);
            this.label14.TabIndex = 47;
            this.label14.Text = "# of cicles :";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 36);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(51, 13);
            this.label15.TabIndex = 46;
            this.label15.Text = "σ²q,max :";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(7, 14);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(48, 13);
            this.label16.TabIndex = 45;
            this.label16.Text = "σ²q,min :";
            // 
            // sigma2qcicles
            // 
            this.sigma2qcicles.Location = new System.Drawing.Point(74, 56);
            this.sigma2qcicles.Mask = "0";
            this.sigma2qcicles.Name = "sigma2qcicles";
            this.sigma2qcicles.Size = new System.Drawing.Size(20, 20);
            this.sigma2qcicles.TabIndex = 44;
            // 
            // sigma2qmax
            // 
            this.sigma2qmax.Location = new System.Drawing.Point(74, 33);
            this.sigma2qmax.Name = "sigma2qmax";
            this.sigma2qmax.Size = new System.Drawing.Size(63, 20);
            this.sigma2qmax.TabIndex = 42;
            // 
            // sigma2qmin
            // 
            this.sigma2qmin.Location = new System.Drawing.Point(74, 10);
            this.sigma2qmin.Name = "sigma2qmin";
            this.sigma2qmin.Size = new System.Drawing.Size(63, 20);
            this.sigma2qmin.TabIndex = 41;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.sigma2pdelta);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.sigma2pcicles);
            this.groupBox4.Controls.Add(this.sigma2pmax);
            this.groupBox4.Controls.Add(this.sigma2pmin);
            this.groupBox4.Location = new System.Drawing.Point(450, 17);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(151, 111);
            this.groupBox4.TabIndex = 50;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "fit  σ²p";
            // 
            // sigma2pdelta
            // 
            this.sigma2pdelta.Location = new System.Drawing.Point(74, 79);
            this.sigma2pdelta.Name = "sigma2pdelta";
            this.sigma2pdelta.Size = new System.Drawing.Size(63, 20);
            this.sigma2pdelta.TabIndex = 49;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 82);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(68, 13);
            this.label9.TabIndex = 48;
            this.label9.Text = "Δσ²p,1s run :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 58);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 13);
            this.label10.TabIndex = 47;
            this.label10.Text = "# of cicles :";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 36);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(51, 13);
            this.label11.TabIndex = 46;
            this.label11.Text = "σ²p,max :";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(7, 14);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(48, 13);
            this.label12.TabIndex = 45;
            this.label12.Text = "σ²p,min :";
            // 
            // sigma2pcicles
            // 
            this.sigma2pcicles.Location = new System.Drawing.Point(74, 56);
            this.sigma2pcicles.Mask = "0";
            this.sigma2pcicles.Name = "sigma2pcicles";
            this.sigma2pcicles.Size = new System.Drawing.Size(20, 20);
            this.sigma2pcicles.TabIndex = 44;
            // 
            // sigma2pmax
            // 
            this.sigma2pmax.Location = new System.Drawing.Point(74, 33);
            this.sigma2pmax.Name = "sigma2pmax";
            this.sigma2pmax.Size = new System.Drawing.Size(63, 20);
            this.sigma2pmax.TabIndex = 42;
            // 
            // sigma2pmin
            // 
            this.sigma2pmin.Location = new System.Drawing.Point(74, 10);
            this.sigma2pmin.Name = "sigma2pmin";
            this.sigma2pmin.Size = new System.Drawing.Size(63, 20);
            this.sigma2pmin.TabIndex = 41;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.sigma2sDelta);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.sigma2sCicles);
            this.groupBox3.Controls.Add(this.sigma2smax);
            this.groupBox3.Controls.Add(this.sigma2smin);
            this.groupBox3.Location = new System.Drawing.Point(293, 17);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(151, 111);
            this.groupBox3.TabIndex = 35;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "fit  σ²s";
            // 
            // sigma2sDelta
            // 
            this.sigma2sDelta.Location = new System.Drawing.Point(74, 79);
            this.sigma2sDelta.Name = "sigma2sDelta";
            this.sigma2sDelta.Size = new System.Drawing.Size(63, 20);
            this.sigma2sDelta.TabIndex = 49;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 82);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 13);
            this.label8.TabIndex = 48;
            this.label8.Text = "Δσ²s,1s run :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 58);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 13);
            this.label7.TabIndex = 47;
            this.label7.Text = "# of cicles :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 46;
            this.label5.Text = "σ²s,max :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 45;
            this.label3.Text = "σ²s,min :";
            // 
            // sigma2sCicles
            // 
            this.sigma2sCicles.Location = new System.Drawing.Point(74, 56);
            this.sigma2sCicles.Mask = "0";
            this.sigma2sCicles.Name = "sigma2sCicles";
            this.sigma2sCicles.Size = new System.Drawing.Size(20, 20);
            this.sigma2sCicles.TabIndex = 44;
            // 
            // sigma2smax
            // 
            this.sigma2smax.Location = new System.Drawing.Point(74, 33);
            this.sigma2smax.Name = "sigma2smax";
            this.sigma2smax.Size = new System.Drawing.Size(63, 20);
            this.sigma2smax.TabIndex = 42;
            // 
            // sigma2smin
            // 
            this.sigma2smin.Location = new System.Drawing.Point(74, 10);
            this.sigma2smin.Name = "sigma2smin";
            this.sigma2smin.Size = new System.Drawing.Size(63, 20);
            this.sigma2smin.TabIndex = 41;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblSigmaq);
            this.groupBox2.Controls.Add(this.sigmaq);
            this.groupBox2.Controls.Add(this.calSigmas);
            this.groupBox2.Controls.Add(this.lblsigmap);
            this.groupBox2.Controls.Add(this.lblsigmas);
            this.groupBox2.Controls.Add(this.sigmap);
            this.groupBox2.Controls.Add(this.sigmas);
            this.groupBox2.Controls.Add(this.calSigmap);
            this.groupBox2.Controls.Add(this.calSigmaq);
            this.groupBox2.Location = new System.Drawing.Point(18, 17);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(269, 111);
            this.groupBox2.TabIndex = 34;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Variance calculation sequence";
            // 
            // lblSigmaq
            // 
            this.lblSigmaq.AutoSize = true;
            this.lblSigmaq.Enabled = false;
            this.lblSigmaq.Location = new System.Drawing.Point(109, 65);
            this.lblSigmaq.Name = "lblSigmaq";
            this.lblSigmaq.Size = new System.Drawing.Size(63, 13);
            this.lblSigmaq.TabIndex = 40;
            this.lblSigmaq.Text = "use as σ²q :";
            // 
            // sigmaq
            // 
            this.sigmaq.Enabled = false;
            this.sigmaq.Location = new System.Drawing.Point(178, 61);
            this.sigmaq.Name = "sigmaq";
            this.sigmaq.Size = new System.Drawing.Size(86, 20);
            this.sigmaq.TabIndex = 39;
            // 
            // calSigmas
            // 
            this.calSigmas.AutoSize = true;
            this.calSigmas.Checked = true;
            this.calSigmas.CheckState = System.Windows.Forms.CheckState.Checked;
            this.calSigmas.Location = new System.Drawing.Point(6, 19);
            this.calSigmas.Name = "calSigmas";
            this.calSigmas.Size = new System.Drawing.Size(88, 17);
            this.calSigmas.TabIndex = 38;
            this.calSigmas.Text = "Calculate σ²s";
            this.calSigmas.UseVisualStyleBackColor = true;
            this.calSigmas.CheckedChanged += new System.EventHandler(this.calSigmas_CheckedChanged);
            // 
            // lblsigmap
            // 
            this.lblsigmap.AutoSize = true;
            this.lblsigmap.Enabled = false;
            this.lblsigmap.Location = new System.Drawing.Point(110, 42);
            this.lblsigmap.Name = "lblsigmap";
            this.lblsigmap.Size = new System.Drawing.Size(63, 13);
            this.lblsigmap.TabIndex = 37;
            this.lblsigmap.Text = "use as σ²p :";
            // 
            // lblsigmas
            // 
            this.lblsigmas.AutoSize = true;
            this.lblsigmas.Enabled = false;
            this.lblsigmas.Location = new System.Drawing.Point(110, 20);
            this.lblsigmas.Name = "lblsigmas";
            this.lblsigmas.Size = new System.Drawing.Size(62, 13);
            this.lblsigmas.TabIndex = 36;
            this.lblsigmas.Text = "use as σ²s :";
            // 
            // sigmap
            // 
            this.sigmap.Enabled = false;
            this.sigmap.Location = new System.Drawing.Point(178, 39);
            this.sigmap.Name = "sigmap";
            this.sigmap.Size = new System.Drawing.Size(86, 20);
            this.sigmap.TabIndex = 35;
            // 
            // sigmas
            // 
            this.sigmas.Enabled = false;
            this.sigmas.Location = new System.Drawing.Point(178, 17);
            this.sigmas.Name = "sigmas";
            this.sigmas.Size = new System.Drawing.Size(86, 20);
            this.sigmas.TabIndex = 34;
            // 
            // calSigmap
            // 
            this.calSigmap.AutoSize = true;
            this.calSigmap.Checked = true;
            this.calSigmap.CheckState = System.Windows.Forms.CheckState.Checked;
            this.calSigmap.Location = new System.Drawing.Point(6, 41);
            this.calSigmap.Name = "calSigmap";
            this.calSigmap.Size = new System.Drawing.Size(92, 17);
            this.calSigmap.TabIndex = 8;
            this.calSigmap.Text = "Calculate σ²p ";
            this.calSigmap.UseVisualStyleBackColor = true;
            this.calSigmap.CheckedChanged += new System.EventHandler(this.calSigmap_CheckedChanged);
            // 
            // calSigmaq
            // 
            this.calSigmaq.AutoSize = true;
            this.calSigmaq.Checked = true;
            this.calSigmaq.CheckState = System.Windows.Forms.CheckState.Checked;
            this.calSigmaq.Location = new System.Drawing.Point(6, 64);
            this.calSigmaq.Name = "calSigmaq";
            this.calSigmaq.Size = new System.Drawing.Size(89, 17);
            this.calSigmaq.TabIndex = 33;
            this.calSigmaq.Text = "Calculate σ²q";
            this.calSigmaq.UseVisualStyleBackColor = true;
            this.calSigmaq.CheckedChanged += new System.EventHandler(this.calSigmaq_CheckedChanged);
            // 
            // OPcalcVarPreview
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(789, 222);
            this.Controls.Add(this.tabOptions);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "OPcalcVarPreview";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Variance calculations";
            this.TopMost = true;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabOptions.ResumeLayout(false);
            this.basic.ResumeLayout(false);
            this.basic.PerformLayout();
            this.advanced.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.CheckBox vscheck;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox vsTxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox filterTxt;
        private System.Windows.Forms.TextBox wpTxt;
        private System.Windows.Forms.CheckBox wpcheck;
        private System.Windows.Forms.TextBox kTxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbXs;
        private System.Windows.Forms.Button btnLoadPrevCols;
        private System.Windows.Forms.ComboBox cmbVs;
        private System.Windows.Forms.TextBox wqTxt;
        private System.Windows.Forms.CheckBox wqcheck;
        private System.Windows.Forms.Button btnLoadPrevFilter;
        private System.Windows.Forms.TabControl tabOptions;
        private System.Windows.Forms.TabPage basic;
        private System.Windows.Forms.TabPage advanced;
        private System.Windows.Forms.CheckBox calSigmap;
        private System.Windows.Forms.CheckBox calSigmaq;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblsigmas;
        private System.Windows.Forms.TextBox sigmap;
        private System.Windows.Forms.TextBox sigmas;
        private System.Windows.Forms.Label lblsigmap;
        private System.Windows.Forms.CheckBox calSigmas;
        private System.Windows.Forms.Label lblSigmaq;
        private System.Windows.Forms.TextBox sigmaq;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MaskedTextBox sigma2sCicles;
        private System.Windows.Forms.TextBox sigma2smax;
        private System.Windows.Forms.TextBox sigma2smin;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox sigma2sDelta;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox sigma2qdelta;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.MaskedTextBox sigma2qcicles;
        private System.Windows.Forms.TextBox sigma2qmax;
        private System.Windows.Forms.TextBox sigma2qmin;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox sigma2pdelta;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.MaskedTextBox sigma2pcicles;
        private System.Windows.Forms.TextBox sigma2pmax;
        private System.Windows.Forms.TextBox sigma2pmin;
        private System.Windows.Forms.Button saveDefault;
    }
}