namespace QuiXoT.statistics
{
    partial class OPgraph
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        //private System.ComponentModel.IContainer components = null;

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.pBox = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.selLbl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pBox)).BeginInit();
            this.SuspendLayout();
            // 
            // pBox
            // 
            this.pBox.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pBox.Location = new System.Drawing.Point(17, 14);
            this.pBox.MinimumSize = new System.Drawing.Size(20, 20);
            this.pBox.Name = "pBox";
            this.pBox.Size = new System.Drawing.Size(599, 379);
            this.pBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pBox.TabIndex = 0;
            this.pBox.TabStop = false;
            this.pBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pBox_MouseMove);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(383, 407);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "selection: ";
            // 
            // selLbl
            // 
            this.selLbl.AutoSize = true;
            this.selLbl.Location = new System.Drawing.Point(444, 407);
            this.selLbl.Name = "selLbl";
            this.selLbl.Size = new System.Drawing.Size(33, 13);
            this.selLbl.TabIndex = 2;
            this.selLbl.Text = "value";
            // 
            // OPgraph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 440);
            this.Controls.Add(this.selLbl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pBox);
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "OPgraph";
            this.Text = "OPgraph";
            ((System.ComponentModel.ISupportInitialize)(this.pBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.PictureBox pBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label selLbl;
    }
}