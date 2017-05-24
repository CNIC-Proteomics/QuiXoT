namespace QuiXoT
{
    partial class OPviewer
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
            this.lblZoom = new System.Windows.Forms.Label();
            this.precursorOut = new System.Windows.Forms.Label();
            this.lblPeakShown = new System.Windows.Forms.Label();
            this.pbxGraph = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbxGraph)).BeginInit();
            this.SuspendLayout();
            // 
            // lblZoom
            // 
            this.lblZoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblZoom.AutoSize = true;
            this.lblZoom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblZoom.Location = new System.Drawing.Point(0, 287);
            this.lblZoom.Name = "lblZoom";
            this.lblZoom.Size = new System.Drawing.Size(54, 13);
            this.lblZoom.TabIndex = 0;
            this.lblZoom.Text = "zoom on";
            this.lblZoom.Visible = false;
            // 
            // precursorOut
            // 
            this.precursorOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.precursorOut.AutoSize = true;
            this.precursorOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.precursorOut.Location = new System.Drawing.Point(256, 0);
            this.precursorOut.Name = "precursorOut";
            this.precursorOut.Size = new System.Drawing.Size(151, 13);
            this.precursorOut.TabIndex = 1;
            this.precursorOut.Text = "precursor out of window !";
            this.precursorOut.Visible = false;
            // 
            // lblPeakShown
            // 
            this.lblPeakShown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPeakShown.AutoSize = true;
            this.lblPeakShown.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPeakShown.Location = new System.Drawing.Point(60, 287);
            this.lblPeakShown.Name = "lblPeakShown";
            this.lblPeakShown.Size = new System.Drawing.Size(100, 13);
            this.lblPeakShown.TabIndex = 2;
            this.lblPeakShown.Text = "tolerance shown";
            this.lblPeakShown.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblPeakShown.Visible = false;
            // 
            // pbxGraph
            // 
            this.pbxGraph.BackColor = System.Drawing.Color.White;
            this.pbxGraph.Location = new System.Drawing.Point(0, 0);
            this.pbxGraph.Name = "pbxGraph";
            this.pbxGraph.Size = new System.Drawing.Size(407, 301);
            this.pbxGraph.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbxGraph.TabIndex = 3;
            this.pbxGraph.TabStop = false;
            this.pbxGraph.MouseMove += new System.Windows.Forms.MouseEventHandler(this.zoomMove);
            this.pbxGraph.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mouse_Click);
            this.pbxGraph.MouseUp += new System.Windows.Forms.MouseEventHandler(this.zoomEnd);
            // 
            // OPviewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(407, 300);
            this.Controls.Add(this.lblPeakShown);
            this.Controls.Add(this.precursorOut);
            this.Controls.Add(this.lblZoom);
            this.Controls.Add(this.pbxGraph);
            this.Name = "OPviewer";
            this.Text = "viewer";
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.zoomEnd);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mouse_Click);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.zoomMove);
            ((System.ComponentModel.ISupportInitialize)(this.pbxGraph)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
  
        #endregion

        private System.Windows.Forms.Label lblZoom;
        private System.Windows.Forms.Label precursorOut;
        private System.Windows.Forms.Label lblPeakShown;
        private System.Windows.Forms.PictureBox pbxGraph;


    }
}