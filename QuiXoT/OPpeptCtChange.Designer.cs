namespace QuiXoT
{
    partial class OPpeptCtChange
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OPpeptCtChange));
            this.btnChange = new System.Windows.Forms.Button();
            this.txtK = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSigma2S = new System.Windows.Forms.TextBox();
            this.txtSigma2P = new System.Windows.Forms.TextBox();
            this.txtSigma2Q = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnChange
            // 
            this.btnChange.Location = new System.Drawing.Point(73, 140);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(61, 26);
            this.btnChange.TabIndex = 4;
            this.btnChange.Text = "change";
            this.btnChange.UseVisualStyleBackColor = true;
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // txtK
            // 
            this.txtK.Location = new System.Drawing.Point(95, 16);
            this.txtK.Name = "txtK";
            this.txtK.Size = new System.Drawing.Size(100, 20);
            this.txtK.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(70, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 13);
            this.label3.TabIndex = 31;
            this.label3.Text = "K :";
            // 
            // txtSigma2S
            // 
            this.txtSigma2S.Location = new System.Drawing.Point(95, 42);
            this.txtSigma2S.Name = "txtSigma2S";
            this.txtSigma2S.Size = new System.Drawing.Size(100, 20);
            this.txtSigma2S.TabIndex = 1;
            // 
            // txtSigma2P
            // 
            this.txtSigma2P.Location = new System.Drawing.Point(95, 69);
            this.txtSigma2P.Name = "txtSigma2P";
            this.txtSigma2P.Size = new System.Drawing.Size(100, 20);
            this.txtSigma2P.TabIndex = 2;
            // 
            // txtSigma2Q
            // 
            this.txtSigma2Q.Location = new System.Drawing.Point(95, 99);
            this.txtSigma2Q.Name = "txtSigma2Q";
            this.txtSigma2Q.Size = new System.Drawing.Size(100, 20);
            this.txtSigma2Q.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(37, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 35;
            this.label4.Text = "sigma2S :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(37, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 36;
            this.label5.Text = "sigma2P :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(37, 102);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 37;
            this.label6.Text = "sigma2Q :";
            // 
            // OPpeptCtChange
            // 
            this.AcceptButton = this.btnChange;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(258, 190);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtSigma2Q);
            this.Controls.Add(this.txtSigma2P);
            this.Controls.Add(this.txtSigma2S);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtK);
            this.Controls.Add(this.btnChange);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OPpeptCtChange";
            this.Text = "Change peptide constants";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnChange;
        private System.Windows.Forms.TextBox txtK;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSigma2S;
        private System.Windows.Forms.TextBox txtSigma2P;
        private System.Windows.Forms.TextBox txtSigma2Q;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}