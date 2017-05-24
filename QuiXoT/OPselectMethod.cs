using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace QuiXoT
{
    public partial class OPselectMethod : Form
    {

        public string keyChosen;
        public string method_id_name_chosen;
        ArrayList alKeyMethods;
        
        public OPselectMethod(QuiXoT.math.qMethodsSchema.Quanmethods quanMethods)
        {
            InitializeComponent();
            alKeyMethods = new ArrayList();
           
            //qMethods =(QuiXoT.math.qMethodsSchema.Quanmethods)quanMethods.Clone();

            var query =
                        from q in quanMethods.method
                        select new { key = q.method_id_key, name = q.method_id_name};


            listBox1.Items.Clear();

            foreach(var m in query)
            {
                listBox1.Items.Add(m.name);
                alKeyMethods.Add(m.key);

            }

            keyChosen = "";
  
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (keyChosen == "")
            {
                MessageBox.Show("You must select a method");
                Application.DoEvents();
            }
            else
            {
                this.Dispose();
            }
        }


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                keyChosen = alKeyMethods[listBox1.SelectedIndex].ToString();
                method_id_name_chosen = listBox1.Items[listBox1.SelectedIndex].ToString();
               
            }
        }


    }
}
