using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QuiXoT
{
    public partial class OPgrapherPreview : Form
    {
        public bool okPressed;
        public bool normalityZGraph = false;
        public bool sigmoidalGraph = true;
        public string colX;
        public string colY1;
        public string colY2;
        public string colY3;

        public void SetSourceColumns(DataColumnCollection Columns)
        {
            try
            {
                foreach (DataColumn col in Columns)
                {
                    if(col.DataType.IsValueType)
                    {
                        cmbX.Items.Add(col.ColumnName.ToString());
                        cmbY1.Items.Add(col.ColumnName.ToString());
                        cmbY2.Items.Add(col.ColumnName.ToString());
                        cmbY3.Items.Add(col.ColumnName.ToString());
                    }
                }

            }
            catch (System.Exception a_Ex)
            {
                MessageBox.Show(a_Ex.Message);
            }
        }


        public OPgrapherPreview()
        {
            InitializeComponent();
            okPressed = false;
        }

        public OPgrapherPreview(string prevColX,
                                string prevColY1,
                                string prevColY2,
                                string prevColY3)
        {
            InitializeComponent();
            okPressed = false;
            colX = prevColX;
            colY1 = prevColY1;
            colY2 = prevColY2;
            colY3 = prevColY3;
        }



        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {

                colX = cmbX.Text;
                colY1 = cmbY1.Text;

                if (colX.Trim() == "" || colY1.Trim() == "") 
                {
                    MessageBox.Show("You have not selected any column");
                    okPressed = false;
                }

                colY2 = cmbY2.Text;
                if (colY2 == "") colY2 = null;
                colY3 = cmbY3.Text;
                if (colY3 == "") colY3 = null;

                okPressed = true;
                this.Dispose();

            }
            catch
            {
                MessageBox.Show("You have not selected any column");
                okPressed = false;
            }
        }

        private void cmbX_TextChanged(object sender, EventArgs e)
            {
            string text = this.cmbX.Text.ToUpper();

            for (int i = 0; i < this.cmbX.Items.Count; i++)
            {
                string item = this.cmbX.Items[i].ToString().ToUpper();
                if (item == text)
                {
                    this.cmbX.SelectedIndex = i;
                    break;
                }
            }

            if (this.cmbX.Text == "Zq" || this.cmbX.Text == "Zp" || this.cmbX.Text == "Zs")
            {
                btnSigmoidalZi.Enabled = true;
                btnLinearZi.Enabled = true;
            }
            else
            {
                btnSigmoidalZi.Enabled = false;
                btnLinearZi.Enabled = false;
            }

        }

        private void cmbY1_TextChanged(object sender, EventArgs e)
        {
            string text = this.cmbY1.Text.ToUpper();

            for (int i = 0; i < this.cmbY1.Items.Count; i++)
            {
                string item = this.cmbY1.Items[i].ToString().ToUpper();
                if (item == text)
                {
                    this.cmbY1.SelectedIndex = i;
                    break;
                }
            }

        }

        private void cmbY2_TextChanged(object sender, EventArgs e)
        {
            string text = this.cmbY2.Text.ToUpper();

            for (int i = 0; i < this.cmbY2.Items.Count; i++)
            {
                string item = this.cmbY2.Items[i].ToString().ToUpper();
                if (item == text)
                {
                    this.cmbY2.SelectedIndex = i;
                    break;
                }
            }

        }

        private void cmbY3_TextChanged(object sender, EventArgs e)
        {
            string text = this.cmbY3.Text.ToUpper();

            for (int i = 0; i < this.cmbY3.Items.Count; i++)
            {
                string item = this.cmbY3.Items[i].ToString().ToUpper();
                if (item == text)
                {
                    this.cmbY3.SelectedIndex = i;
                    break;
                }
            }

        }

        private void btnSigmoidalZi_Click(object sender, EventArgs e)
        {
            sigmoidalGraph = true;
            compareZi();
        }

        private void btnLinearZi_Click(object sender, EventArgs e)
        {
            sigmoidalGraph = false;
            compareZi();
        }

        private void compareZi()
        {
            colX = cmbX.Text;
            colY1 = null;
            colY2 = null;
            colY3 = null;
            okPressed = true;
            normalityZGraph = true;
            this.Dispose();
        }
    }
}
