namespace QuiXoT
{
    partial class OPselectBoundsGraph
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
            this.txtXlowLimit = new System.Windows.Forms.TextBox();
            this.txtXupLimit = new System.Windows.Forms.TextBox();
            this.txtYlowLimit = new System.Windows.Forms.TextBox();
            this.txtYupLimit = new System.Windows.Forms.TextBox();
            this.txtXtick = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtYtick = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMaxNumTicksY = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMaxNumTicksX = new System.Windows.Forms.TextBox();
            this.rbYlowLimit = new System.Windows.Forms.CheckBox();
            this.rbXupLimit = new System.Windows.Forms.CheckBox();
            this.rbXLowLimit = new System.Windows.Forms.CheckBox();
            this.rbYupLimit = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // txtXlowLimit
            // 
            this.txtXlowLimit.Location = new System.Drawing.Point(119, 31);
            this.txtXlowLimit.Name = "txtXlowLimit";
            this.txtXlowLimit.Size = new System.Drawing.Size(95, 20);
            this.txtXlowLimit.TabIndex = 0;
            this.txtXlowLimit.TextChanged += new System.EventHandler(this.XlowTxtChanged);
            this.txtXlowLimit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtXlowLimit_KeyPress);
            // 
            // txtXupLimit
            // 
            this.txtXupLimit.Location = new System.Drawing.Point(119, 54);
            this.txtXupLimit.Name = "txtXupLimit";
            this.txtXupLimit.Size = new System.Drawing.Size(95, 20);
            this.txtXupLimit.TabIndex = 1;
            this.txtXupLimit.TextChanged += new System.EventHandler(this.XupTxtChanged);
            this.txtXupLimit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtXupLimit_KeyPress);
            // 
            // txtYlowLimit
            // 
            this.txtYlowLimit.Location = new System.Drawing.Point(353, 31);
            this.txtYlowLimit.Name = "txtYlowLimit";
            this.txtYlowLimit.Size = new System.Drawing.Size(95, 20);
            this.txtYlowLimit.TabIndex = 2;
            this.txtYlowLimit.TextChanged += new System.EventHandler(this.YlowTxtChanged);
            this.txtYlowLimit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtYlowLimit_KeyPress);
            // 
            // txtYupLimit
            // 
            this.txtYupLimit.Location = new System.Drawing.Point(353, 54);
            this.txtYupLimit.Name = "txtYupLimit";
            this.txtYupLimit.Size = new System.Drawing.Size(95, 20);
            this.txtYupLimit.TabIndex = 3;
            this.txtYupLimit.TextChanged += new System.EventHandler(this.YupTxtChanged);
            this.txtYupLimit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtYupLimit_KeyPress);
            // 
            // txtXtick
            // 
            this.txtXtick.Location = new System.Drawing.Point(119, 92);
            this.txtXtick.Name = "txtXtick";
            this.txtXtick.Size = new System.Drawing.Size(95, 20);
            this.txtXtick.TabIndex = 4;
            this.txtXtick.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtXtick_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "X tick format :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(273, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Y tick format :";
            // 
            // txtYtick
            // 
            this.txtYtick.Location = new System.Drawing.Point(353, 92);
            this.txtYtick.Name = "txtYtick";
            this.txtYtick.Size = new System.Drawing.Size(95, 20);
            this.txtYtick.TabIndex = 6;
            this.txtYtick.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtYtick_KeyPress);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOK.Location = new System.Drawing.Point(308, 155);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(67, 24);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(381, 155);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(67, 24);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(256, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "max # of Y ticks :";
            // 
            // txtMaxNumTicksY
            // 
            this.txtMaxNumTicksY.Location = new System.Drawing.Point(353, 117);
            this.txtMaxNumTicksY.Name = "txtMaxNumTicksY";
            this.txtMaxNumTicksY.Size = new System.Drawing.Size(95, 20);
            this.txtMaxNumTicksY.TabIndex = 7;
            this.txtMaxNumTicksY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMaxNumTicksY_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "max # of X ticks :";
            // 
            // txtMaxNumTicksX
            // 
            this.txtMaxNumTicksX.Location = new System.Drawing.Point(119, 117);
            this.txtMaxNumTicksX.Name = "txtMaxNumTicksX";
            this.txtMaxNumTicksX.Size = new System.Drawing.Size(95, 20);
            this.txtMaxNumTicksX.TabIndex = 5;
            this.txtMaxNumTicksX.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMaxNumTicksX_KeyPress);
            // 
            // rbYlowLimit
            // 
            this.rbYlowLimit.AutoSize = true;
            this.rbYlowLimit.Location = new System.Drawing.Point(257, 34);
            this.rbYlowLimit.Name = "rbYlowLimit";
            this.rbYlowLimit.Size = new System.Drawing.Size(90, 17);
            this.rbYlowLimit.TabIndex = 12;
            this.rbYlowLimit.Text = "Y lower limit  :";
            this.rbYlowLimit.UseVisualStyleBackColor = true;
            // 
            // rbXupLimit
            // 
            this.rbXupLimit.AutoSize = true;
            this.rbXupLimit.Location = new System.Drawing.Point(28, 58);
            this.rbXupLimit.Name = "rbXupLimit";
            this.rbXupLimit.Size = new System.Drawing.Size(89, 17);
            this.rbXupLimit.TabIndex = 11;
            this.rbXupLimit.Text = "X upper limit :";
            this.rbXupLimit.UseVisualStyleBackColor = true;
            // 
            // rbXLowLimit
            // 
            this.rbXLowLimit.AutoSize = true;
            this.rbXLowLimit.Location = new System.Drawing.Point(28, 35);
            this.rbXLowLimit.Name = "rbXLowLimit";
            this.rbXLowLimit.Size = new System.Drawing.Size(90, 17);
            this.rbXLowLimit.TabIndex = 10;
            this.rbXLowLimit.Text = "X lower limit  :";
            this.rbXLowLimit.UseVisualStyleBackColor = true;
            // 
            // rbYupLimit
            // 
            this.rbYupLimit.AutoSize = true;
            this.rbYupLimit.Location = new System.Drawing.Point(257, 56);
            this.rbYupLimit.Name = "rbYupLimit";
            this.rbYupLimit.Size = new System.Drawing.Size(89, 17);
            this.rbYupLimit.TabIndex = 13;
            this.rbYupLimit.Text = "Y upper limit :";
            this.rbYupLimit.UseVisualStyleBackColor = true;
            // 
            // OPselectBoundsGraph
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(475, 202);
            this.Controls.Add(this.rbYupLimit);
            this.Controls.Add(this.rbXLowLimit);
            this.Controls.Add(this.rbXupLimit);
            this.Controls.Add(this.rbYlowLimit);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtMaxNumTicksY);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtMaxNumTicksX);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtYtick);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtXtick);
            this.Controls.Add(this.txtYupLimit);
            this.Controls.Add(this.txtYlowLimit);
            this.Controls.Add(this.txtXupLimit);
            this.Controls.Add(this.txtXlowLimit);
            this.Name = "OPselectBoundsGraph";
            this.Text = "Select bounds of graph";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OPselectBoundsGraph_KeyPress);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtXlowLimit;
        private System.Windows.Forms.TextBox txtXupLimit;
        private System.Windows.Forms.TextBox txtYlowLimit;
        private System.Windows.Forms.TextBox txtYupLimit;
        private System.Windows.Forms.TextBox txtXtick;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtYtick;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMaxNumTicksY;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMaxNumTicksX;
        private System.Windows.Forms.CheckBox rbYlowLimit;
        private System.Windows.Forms.CheckBox rbXupLimit;
        private System.Windows.Forms.CheckBox rbXLowLimit;
        private System.Windows.Forms.CheckBox rbYupLimit;
    }
}