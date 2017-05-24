namespace QuiXoT
{
    partial class OPchangeColV
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
            this.btnChange = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cmbColumn = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.cmbCopyVals = new System.Windows.Forms.ComboBox();
            this.optNewVal = new System.Windows.Forms.RadioButton();
            this.optCopyVals = new System.Windows.Forms.RadioButton();
            this.optDeltaMass = new System.Windows.Forms.RadioButton();
            this.txtSymbols = new System.Windows.Forms.TextBox();
            this.lblSymbols = new System.Windows.Forms.Label();
            this.logScale = new System.Windows.Forms.CheckBox();
            this.logBase = new System.Windows.Forms.TextBox();
            this.txtLogBase = new System.Windows.Forms.Label();
            this.optConcatenateVal = new System.Windows.Forms.RadioButton();
            this.txtConcat = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnChange
            // 
            this.btnChange.Location = new System.Drawing.Point(135, 243);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(58, 22);
            this.btnChange.TabIndex = 12;
            this.btnChange.Text = "change";
            this.btnChange.UseVisualStyleBackColor = true;
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(199, 243);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(58, 22);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cmbColumn
            // 
            this.cmbColumn.FormattingEnabled = true;
            this.cmbColumn.Location = new System.Drawing.Point(185, 15);
            this.cmbColumn.Name = "cmbColumn";
            this.cmbColumn.Size = new System.Drawing.Size(195, 21);
            this.cmbColumn.TabIndex = 0;
            this.cmbColumn.TextChanged += new System.EventHandler(this.cmbColumn_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(99, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "target column : ";
            // 
            // txtValue
            // 
            this.txtValue.Enabled = false;
            this.txtValue.Location = new System.Drawing.Point(185, 49);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(194, 20);
            this.txtValue.TabIndex = 2;
            // 
            // cmbCopyVals
            // 
            this.cmbCopyVals.Enabled = false;
            this.cmbCopyVals.FormattingEnabled = true;
            this.cmbCopyVals.Location = new System.Drawing.Point(185, 139);
            this.cmbCopyVals.Name = "cmbCopyVals";
            this.cmbCopyVals.Size = new System.Drawing.Size(195, 21);
            this.cmbCopyVals.TabIndex = 6;
            this.cmbCopyVals.TextChanged += new System.EventHandler(this.cmbCopyVals_TextChanged);
            // 
            // optNewVal
            // 
            this.optNewVal.AutoSize = true;
            this.optNewVal.Location = new System.Drawing.Point(31, 49);
            this.optNewVal.Name = "optNewVal";
            this.optNewVal.Size = new System.Drawing.Size(141, 17);
            this.optNewVal.TabIndex = 1;
            this.optNewVal.TabStop = true;
            this.optNewVal.Text = "new value (for any row) :";
            this.optNewVal.UseVisualStyleBackColor = true;
            this.optNewVal.CheckedChanged += new System.EventHandler(this.optNewVal_CheckedChanged);
            // 
            // optCopyVals
            // 
            this.optCopyVals.AutoSize = true;
            this.optCopyVals.Location = new System.Drawing.Point(31, 140);
            this.optCopyVals.Name = "optCopyVals";
            this.optCopyVals.Size = new System.Drawing.Size(148, 17);
            this.optCopyVals.TabIndex = 5;
            this.optCopyVals.TabStop = true;
            this.optCopyVals.Text = "copy values from column :";
            this.optCopyVals.UseVisualStyleBackColor = true;
            this.optCopyVals.CheckedChanged += new System.EventHandler(this.optCopyVals_CheckedChanged);
            // 
            // optDeltaMass
            // 
            this.optDeltaMass.AutoSize = true;
            this.optDeltaMass.Location = new System.Drawing.Point(31, 207);
            this.optDeltaMass.Name = "optDeltaMass";
            this.optDeltaMass.Size = new System.Drawing.Size(108, 17);
            this.optDeltaMass.TabIndex = 9;
            this.optDeltaMass.TabStop = true;
            this.optDeltaMass.Text = "calculate Δmass :";
            this.optDeltaMass.UseVisualStyleBackColor = true;
            this.optDeltaMass.CheckedChanged += new System.EventHandler(this.optDeltaMass_CheckedChanged);
            // 
            // txtSymbols
            // 
            this.txtSymbols.Enabled = false;
            this.txtSymbols.Location = new System.Drawing.Point(299, 207);
            this.txtSymbols.Name = "txtSymbols";
            this.txtSymbols.Size = new System.Drawing.Size(80, 20);
            this.txtSymbols.TabIndex = 11;
            this.txtSymbols.TextChanged += new System.EventHandler(this.txtSymbols_TextChanged);
            // 
            // lblSymbols
            // 
            this.lblSymbols.AutoSize = true;
            this.lblSymbols.Enabled = false;
            this.lblSymbols.Location = new System.Drawing.Point(165, 210);
            this.lblSymbols.Name = "lblSymbols";
            this.lblSymbols.Size = new System.Drawing.Size(134, 13);
            this.lblSymbols.TabIndex = 10;
            this.lblSymbols.Text = "label modification symbols :";
            // 
            // logScale
            // 
            this.logScale.AutoSize = true;
            this.logScale.Enabled = false;
            this.logScale.Location = new System.Drawing.Point(73, 167);
            this.logScale.Name = "logScale";
            this.logScale.Size = new System.Drawing.Size(79, 17);
            this.logScale.TabIndex = 7;
            this.logScale.Text = "in log scale";
            this.logScale.UseVisualStyleBackColor = true;
            // 
            // logBase
            // 
            this.logBase.Enabled = false;
            this.logBase.Location = new System.Drawing.Point(290, 164);
            this.logBase.Name = "logBase";
            this.logBase.Size = new System.Drawing.Size(50, 20);
            this.logBase.TabIndex = 8;
            // 
            // txtLogBase
            // 
            this.txtLogBase.AutoSize = true;
            this.txtLogBase.Location = new System.Drawing.Point(158, 167);
            this.txtLogBase.Name = "txtLogBase";
            this.txtLogBase.Size = new System.Drawing.Size(132, 13);
            this.txtLogBase.TabIndex = 12;
            this.txtLogBase.Text = "base (empty for neperian) :";
            // 
            // optConcatenateVal
            // 
            this.optConcatenateVal.Location = new System.Drawing.Point(31, 83);
            this.optConcatenateVal.Name = "optConcatenateVal";
            this.optConcatenateVal.Size = new System.Drawing.Size(121, 41);
            this.optConcatenateVal.TabIndex = 3;
            this.optConcatenateVal.TabStop = true;
            this.optConcatenateVal.Text = "concatenate fields (comma separated)";
            this.optConcatenateVal.UseVisualStyleBackColor = true;
            this.optConcatenateVal.CheckedChanged += new System.EventHandler(this.optConcatenateVal_CheckedChanged);
            // 
            // txtConcat
            // 
            this.txtConcat.Enabled = false;
            this.txtConcat.Location = new System.Drawing.Point(185, 94);
            this.txtConcat.Name = "txtConcat";
            this.txtConcat.Size = new System.Drawing.Size(194, 20);
            this.txtConcat.TabIndex = 4;
            // 
            // OPchangeColV
            // 
            this.AcceptButton = this.btnChange;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(411, 286);
            this.Controls.Add(this.optConcatenateVal);
            this.Controls.Add(this.txtConcat);
            this.Controls.Add(this.txtLogBase);
            this.Controls.Add(this.logBase);
            this.Controls.Add(this.logScale);
            this.Controls.Add(this.lblSymbols);
            this.Controls.Add(this.txtSymbols);
            this.Controls.Add(this.optDeltaMass);
            this.Controls.Add(this.optCopyVals);
            this.Controls.Add(this.optNewVal);
            this.Controls.Add(this.cmbCopyVals);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbColumn);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnChange);
            this.Name = "OPchangeColV";
            this.Text = "change column value";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnChange;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cmbColumn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.ComboBox cmbCopyVals;
        private System.Windows.Forms.RadioButton optNewVal;
        private System.Windows.Forms.RadioButton optCopyVals;
        private System.Windows.Forms.RadioButton optDeltaMass;
        private System.Windows.Forms.TextBox txtSymbols;
        private System.Windows.Forms.Label lblSymbols;
        private System.Windows.Forms.CheckBox logScale;
        private System.Windows.Forms.TextBox logBase;
        private System.Windows.Forms.Label txtLogBase;
        private System.Windows.Forms.RadioButton optConcatenateVal;
        private System.Windows.Forms.TextBox txtConcat;
    }
}