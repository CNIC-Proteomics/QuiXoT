namespace QuiXoT
{
    partial class OPgrapherPreview
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
            this.cmbX = new System.Windows.Forms.ComboBox();
            this.cmbY1 = new System.Windows.Forms.ComboBox();
            this.cmbY2 = new System.Windows.Forms.ComboBox();
            this.cmbY3 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSigmoidalZi = new System.Windows.Forms.Button();
            this.btnLinearZi = new System.Windows.Forms.Button();
            this.gbxNormalityPlot = new System.Windows.Forms.GroupBox();
            this.gbxNormalityPlot.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbX
            // 
            this.cmbX.FormattingEnabled = true;
            this.cmbX.Location = new System.Drawing.Point(86, 30);
            this.cmbX.Name = "cmbX";
            this.cmbX.Size = new System.Drawing.Size(175, 21);
            this.cmbX.TabIndex = 0;
            this.cmbX.TextChanged += new System.EventHandler(this.cmbX_TextChanged);
            // 
            // cmbY1
            // 
            this.cmbY1.FormattingEnabled = true;
            this.cmbY1.Location = new System.Drawing.Point(86, 122);
            this.cmbY1.Name = "cmbY1";
            this.cmbY1.Size = new System.Drawing.Size(175, 21);
            this.cmbY1.TabIndex = 2;
            this.cmbY1.TextChanged += new System.EventHandler(this.cmbY1_TextChanged);
            // 
            // cmbY2
            // 
            this.cmbY2.FormattingEnabled = true;
            this.cmbY2.Location = new System.Drawing.Point(86, 149);
            this.cmbY2.Name = "cmbY2";
            this.cmbY2.Size = new System.Drawing.Size(175, 21);
            this.cmbY2.TabIndex = 3;
            this.cmbY2.TextChanged += new System.EventHandler(this.cmbY2_TextChanged);
            // 
            // cmbY3
            // 
            this.cmbY3.FormattingEnabled = true;
            this.cmbY3.Location = new System.Drawing.Point(86, 176);
            this.cmbY3.Name = "cmbY3";
            this.cmbY3.Size = new System.Drawing.Size(175, 21);
            this.cmbY3.TabIndex = 4;
            this.cmbY3.TextChanged += new System.EventHandler(this.cmbY3_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "X values :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Y values :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 179);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Y values :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 152);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Y values :";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(105, 216);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 24);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(186, 216);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 24);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSigmoidalZi
            // 
            this.btnSigmoidalZi.Enabled = false;
            this.btnSigmoidalZi.Location = new System.Drawing.Point(174, 16);
            this.btnSigmoidalZi.Name = "btnSigmoidalZi";
            this.btnSigmoidalZi.Size = new System.Drawing.Size(75, 24);
            this.btnSigmoidalZi.TabIndex = 1;
            this.btnSigmoidalZi.Text = "sigmoidal";
            this.btnSigmoidalZi.UseVisualStyleBackColor = true;
            this.btnSigmoidalZi.Click += new System.EventHandler(this.btnSigmoidalZi_Click);
            // 
            // btnLinearZi
            // 
            this.btnLinearZi.Enabled = false;
            this.btnLinearZi.Location = new System.Drawing.Point(93, 16);
            this.btnLinearZi.Name = "btnLinearZi";
            this.btnLinearZi.Size = new System.Drawing.Size(75, 24);
            this.btnLinearZi.TabIndex = 0;
            this.btnLinearZi.Text = "linear";
            this.btnLinearZi.UseVisualStyleBackColor = true;
            this.btnLinearZi.Click += new System.EventHandler(this.btnLinearZi_Click);
            // 
            // gbxNormalityPlot
            // 
            this.gbxNormalityPlot.Controls.Add(this.btnLinearZi);
            this.gbxNormalityPlot.Controls.Add(this.btnSigmoidalZi);
            this.gbxNormalityPlot.Location = new System.Drawing.Point(12, 57);
            this.gbxNormalityPlot.Name = "gbxNormalityPlot";
            this.gbxNormalityPlot.Size = new System.Drawing.Size(261, 46);
            this.gbxNormalityPlot.TabIndex = 1;
            this.gbxNormalityPlot.TabStop = false;
            this.gbxNormalityPlot.Text = "normality plot";
            // 
            // OPgrapherPreview
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(288, 262);
            this.Controls.Add(this.gbxNormalityPlot);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbY3);
            this.Controls.Add(this.cmbY2);
            this.Controls.Add(this.cmbY1);
            this.Controls.Add(this.cmbX);
            this.Name = "OPgrapherPreview";
            this.Text = "Columns\' selection";
            this.gbxNormalityPlot.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbX;
        private System.Windows.Forms.ComboBox cmbY1;
        private System.Windows.Forms.ComboBox cmbY2;
        private System.Windows.Forms.ComboBox cmbY3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSigmoidalZi;
        private System.Windows.Forms.Button btnLinearZi;
        private System.Windows.Forms.GroupBox gbxNormalityPlot;
    }
}