namespace QuiXoT
{
    partial class OPgrapher
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OPgrapher));
            this.lblZoom = new System.Windows.Forms.Label();
            this.pbxGraph = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbxGraph)).BeginInit();
            this.SuspendLayout();
            // 
            // lblZoom
            // 
            this.lblZoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblZoom.AutoSize = true;
            this.lblZoom.BackColor = System.Drawing.SystemColors.HighlightText;
            this.lblZoom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblZoom.Location = new System.Drawing.Point(0, 377);
            this.lblZoom.Name = "lblZoom";
            this.lblZoom.Size = new System.Drawing.Size(54, 13);
            this.lblZoom.TabIndex = 1;
            this.lblZoom.Text = "zoom on";
            this.lblZoom.Visible = false;
            // 
            // pbxGraph
            // 
            this.pbxGraph.Location = new System.Drawing.Point(0, 0);
            this.pbxGraph.Name = "pbxGraph";
            this.pbxGraph.Size = new System.Drawing.Size(449, 390);
            this.pbxGraph.TabIndex = 2;
            this.pbxGraph.TabStop = false;
            this.pbxGraph.MouseMove += new System.Windows.Forms.MouseEventHandler(this.zoomMove);
            this.pbxGraph.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mouse_Click);
            this.pbxGraph.MouseUp += new System.Windows.Forms.MouseEventHandler(this.zoomEnd);
            // 
            // OPgrapher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(449, 390);
            this.Controls.Add(this.lblZoom);
            this.Controls.Add(this.pbxGraph);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OPgrapher";
            this.Tag = "";
            this.Text = "OPgrapher";
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.zoomEnd);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mouse_Click);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.closing);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.zoomMove);
            ((System.ComponentModel.ISupportInitialize)(this.pbxGraph)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblZoom;
        private System.Windows.Forms.PictureBox pbxGraph;
    }
}