using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;



namespace DataGridFilter
{
	/// <summary>
	/// Summary description for ColumnFilterForm.
	/// </summary>

   
    
    public class ColumnFilterForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckedListBox ClbShowColumn;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button buttonSelect;
        private Button buttonLoad;
        private Button buttonSave;
        private string workPath;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ColumnFilterForm(string path)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

            workPath = path;

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ClbShowColumn = new System.Windows.Forms.CheckedListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.buttonSelect = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ClbShowColumn);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(228, 386);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Show Column";
            // 
            // ClbShowColumn
            // 
            this.ClbShowColumn.CheckOnClick = true;
            this.ClbShowColumn.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ClbShowColumn.Location = new System.Drawing.Point(8, 16);
            this.ClbShowColumn.Name = "ClbShowColumn";
            this.ClbShowColumn.Size = new System.Drawing.Size(214, 356);
            this.ClbShowColumn.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.buttonSave);
            this.panel1.Controls.Add(this.buttonLoad);
            this.panel1.Controls.Add(this.buttonSelect);
            this.panel1.Location = new System.Drawing.Point(8, 400);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(228, 62);
            this.panel1.TabIndex = 22;
            // 
            // buttonSave
            // 
            this.buttonSave.BackColor = System.Drawing.Color.LightSteelBlue;
            this.buttonSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonSave.Location = new System.Drawing.Point(127, 32);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 2;
            this.buttonSave.Text = "Save filters";
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonLoad
            // 
            this.buttonLoad.BackColor = System.Drawing.Color.LightSteelBlue;
            this.buttonLoad.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonLoad.Location = new System.Drawing.Point(19, 32);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(75, 23);
            this.buttonLoad.TabIndex = 1;
            this.buttonLoad.Text = "Load filters";
            this.buttonLoad.UseVisualStyleBackColor = false;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // buttonSelect
            // 
            this.buttonSelect.BackColor = System.Drawing.Color.LightSteelBlue;
            this.buttonSelect.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonSelect.Location = new System.Drawing.Point(76, 3);
            this.buttonSelect.Name = "buttonSelect";
            this.buttonSelect.Size = new System.Drawing.Size(72, 23);
            this.buttonSelect.TabIndex = 0;
            this.buttonSelect.Text = "Select";
            this.buttonSelect.UseVisualStyleBackColor = false;
            this.buttonSelect.Click += new System.EventHandler(this.buttonSelect_Click);
            // 
            // ColumnFilterForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(248, 470);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Name = "ColumnFilterForm";
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		public void SetSourceColumns (DataColumnCollection Columns)
		{
			try
			{
				foreach (DataColumn col in Columns)
				{
					ClbShowColumn.Items.Add(col.ColumnName.ToString());
                    ClbShowColumn.SetItemChecked(ClbShowColumn.Items.Count - 1, true);
				}
			
			}
			catch (System.Exception a_Ex)
			{
				MessageBox.Show(a_Ex.Message);
			}
		}

        public void memFilteredColumns(DataColumnCollection Columns, CheckedListBox memoryColumns)
        {
            try
            {
                foreach (DataColumn col in Columns)
                {
                    ClbShowColumn.Items.Add(col.ColumnName.ToString());
                    ClbShowColumn.SetItemChecked(ClbShowColumn.Items.Count - 1, false);
                }

              
                for (int i = 0; i < memoryColumns.CheckedItems.Count; i++)
                {
                    object bValue = memoryColumns.CheckedItems[i];
                    string sValue = bValue.ToString();
                    int iVal=0;
                    for(int j=0;j<ClbShowColumn.Items.Count;j++)
                    {
                        if(sValue==ClbShowColumn.Items[j].ToString())
                        {
                            iVal=j;
                        }
                    }


                    ClbShowColumn.SetItemChecked(iVal, true);
 
                                        
                }

            }
            catch (System.Exception a_Ex)
            {
                MessageBox.Show(a_Ex.Message);
            }
        }

		public CheckedListBox GetSelectedColumns()
		{
			return ClbShowColumn;
		}

        private void buttonSelect_Click(object sender, EventArgs e)
        {

        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
 
            string filterFileName = workPath + "columns.ftr";
            FileMemCols colsFromFile;
 
            try
            {
                FileStream q = new FileStream(filterFileName, FileMode.Open, FileAccess.Read);
                BinaryFormatter b = new BinaryFormatter();
                colsFromFile = (FileMemCols)b.Deserialize(q);
                q.Close();

                //Uncheck all fields 
                for (int i = 0; i <= ClbShowColumn.Items.Count - 1; i++)
                {
                    ClbShowColumn.SetItemChecked(i, false);
                }

                //Check the correct fields (from the saved file)
                for (int j = 0; j <= colsFromFile.size() - 1;j++ )
                {
                    for (int i = 0; i <= ClbShowColumn.Items.Count - 1; i++)
                    {
                        if (colsFromFile.peek(j) == ClbShowColumn.Items[i].ToString())
                        {
                            ClbShowColumn.SetItemChecked(i, true);
                        }
                    }
                }

            }
            catch
            {
                MessageBox.Show("Error: filters couldn't be loaded. Have you saved any filter previously?");
            }

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            FileMemCols fCols = new FileMemCols(ClbShowColumn.CheckedItems.Count);

            for(int i=0;i<=ClbShowColumn.CheckedItems.Count-1;i++)
            {

                fCols.insert(ClbShowColumn.CheckedItems[i].ToString());
            }


            string filterFileName = workPath + "columns.ftr";
            try
            {

                FileStream fsFilter = new FileStream(filterFileName, FileMode.Create, FileAccess.Write);
                BinaryFormatter bFilter = new BinaryFormatter();
                bFilter.Serialize(fsFilter, fCols);
                fsFilter.Close();

                MessageBox.Show("filters saved.");

            }
            catch 
            {
                MessageBox.Show("Filters couldn't be saved", "Error", MessageBoxButtons.OK);
            }
 

        }



		
	}

    [Serializable]
    public class FileMemCols
    {
        //declare the class properties
        protected int start, end, theSize;
        public string[] cols;
        
        #region general methods of FileMemCols class
        /// <summary>
        /// construct a new list given the capacity
        /// </summary>
        /// <param name="capacity">(int)number of cols</param>
        public FileMemCols(int capacity)
        {
            //allocate memory for components' list
            cols = new string[capacity];

            //start, end and size are 0 (list is empty)
            start = end = theSize = 0;  
                       
        }
        /// <summary>
        /// check whether this list is empty
        /// </summary>
        /// <returns>(bool)true if the list is empty</returns>
        public bool isEmpty()
        {
            return theSize == 0;
        }
        /// <summary>
        /// check whether this list is full
        /// </summary>
        /// <returns>(bool)true if the list is full</returns>
        public bool isFull() 
        {
            return theSize >= cols.Length;
        }
        /// <summary>
        /// get the size of this list
        /// </summary>
        /// <returns>(int)size of list</returns>
        public int size() 
        {
            return theSize;
        }
        /// <summary>
        /// insert a new scan spectrum
        /// </summary>
        /// <param name="newScan">(QuiXoT.DA_Raw.scanStrt)scan</param>
        public void insert(string newCol)
        {

            // if insert won't overflow list
            if (theSize < cols.Length)
            {

                // increment start and set element
                cols[start = (start + 1) % cols.Length] = newCol;

                // increment list size (we've added an element)
                theSize++;
            }
 
        }
        /// <summary>
        /// peek at an element in the list 
        /// </summary>
        /// <param name="offset">(int)array index to point</param>
        /// <returns>(string)selected column</returns>
        public string peek(int offset)
        {
            string ret="";

            // is someone trying to peek beyond our size?
            if (offset >= theSize)
                return ret;

            // get object we're peeking at (do not remove it)
            return cols[(end + offset + 1) % cols.Length];
        }
        #endregion
        

    }


}
