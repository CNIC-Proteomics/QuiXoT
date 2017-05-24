using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace DataGridFilter
{
	/// <summary>
	/// Summary description for DataFilter.
	/// </summary>
	public class DataFilterForm : System.Windows.Forms.Form
	{

		private DataTable FilterDataTable;
		private DataTable OperationDataTable;
		
		// Add new oprrations according to the operation which support by the database you connect
		private string [] Operations = {"<","<=",">",">=","=","<>","LIKE"};

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.DataGrid DataGridFilterData;
		private System.Windows.Forms.Button ButtonSelect;
		private System.Windows.Forms.Panel panel2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DataFilterForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

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
            this.panel1 = new System.Windows.Forms.Panel();
            this.DataGridFilterData = new System.Windows.Forms.DataGrid();
            this.ButtonSelect = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridFilterData)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.DataGridFilterData);
            this.panel1.Location = new System.Drawing.Point(8, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(472, 328);
            this.panel1.TabIndex = 0;
            // 
            // DataGridFilterData
            // 
            this.DataGridFilterData.DataMember = "";
            this.DataGridFilterData.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.DataGridFilterData.Location = new System.Drawing.Point(8, 8);
            this.DataGridFilterData.Name = "DataGridFilterData";
            this.DataGridFilterData.Size = new System.Drawing.Size(456, 312);
            this.DataGridFilterData.TabIndex = 0;
            // 
            // ButtonSelect
            // 
            this.ButtonSelect.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ButtonSelect.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ButtonSelect.Location = new System.Drawing.Point(368, 4);
            this.ButtonSelect.Name = "ButtonSelect";
            this.ButtonSelect.Size = new System.Drawing.Size(96, 23);
            this.ButtonSelect.TabIndex = 1;
            this.ButtonSelect.Text = "Select";
            this.ButtonSelect.UseVisualStyleBackColor = false;
            this.ButtonSelect.Click += new System.EventHandler(this.ButtonSelect_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Gainsboro;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.ButtonSelect);
            this.panel2.Location = new System.Drawing.Point(8, 344);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(472, 32);
            this.panel2.TabIndex = 23;
            // 
            // DataFilterForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(488, 390);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "DataFilterForm";
            this.Load += new System.EventHandler(this.DataFilterForm_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridFilterData)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void DataFilterForm_Load(object sender, System.EventArgs e)
		{
			DataGridTableStyle    GridStyle = new DataGridTableStyle();
			
			
			GridStyle.MappingName = "FilterData" ;




			DataGridColumnStyle  ColumnName= new DataGridTextBoxColumn();
			ColumnName.HeaderText="Column Name";
			ColumnName.MappingName="ColumnName";
			ColumnName.Width=150;
			ColumnName.ReadOnly = true;
			GridStyle.GridColumnStyles.Add(ColumnName);

			
			DataGridColumnStyle  ColumnOperation= new DataGridComboBoxColumn("Operation", OperationDataTable, "ColumnOperation", "ColumnOperation", DataGridFilterData);
			
			ColumnOperation.Width=70;
			ColumnOperation.NullText = string.Empty;
			GridStyle.GridColumnStyles.Add(ColumnOperation);

			DataGridColumnStyle  ColumnData= new DataGridTextBoxColumn();
			ColumnData.HeaderText="Data";
			ColumnData.MappingName="ColumnData";
			ColumnData.NullText= string.Empty;
			ColumnData.Width=180;
			GridStyle.GridColumnStyles.Add(ColumnData);

			
			GridStyle.AlternatingBackColor=System.Drawing.Color.AliceBlue ;
			GridStyle.GridLineColor = System.Drawing.Color.MediumSlateBlue;
				
			DataGridFilterData.TableStyles.Add(GridStyle);

			DataGridFilterData.DataSource = FilterDataTable;
			
		}


		public void SetSourceColumns (DataColumnCollection Columns)
		{

			DataRow FilterRow;
			DataRow OperationRow;
			try
			{

				OperationDataTable =new DataTable("OperationDataTable");
				DataColumn CloumnOperation = new DataColumn("ColumnOperation",System.Type.GetType("System.String"));
				OperationDataTable.Columns.Add(CloumnOperation);
			
                 

				foreach (string Oper in Operations)
				{
                  OperationRow = OperationDataTable.NewRow();
					OperationRow["ColumnOperation"] = Oper;
				  OperationDataTable.Rows.Add(OperationRow);
				}
				 

				FilterDataTable = new DataTable("FilterData");
			
				DataColumn CloumnName = new DataColumn("ColumnName",System.Type.GetType("System.String"));
				DataColumn ColumnOperation = new DataColumn("Operation",System.Type.GetType("System.String"));
				DataColumn ColumnFilterData = new DataColumn("ColumnData",System.Type.GetType("System.String"));
				
				FilterDataTable.Columns.Add(CloumnName);
				FilterDataTable.Columns.Add(ColumnOperation);				
				FilterDataTable.Columns.Add(ColumnFilterData);
				

				foreach (DataColumn col in Columns)
				{
					FilterRow = FilterDataTable.NewRow();
					FilterRow["ColumnName"] = col.ColumnName.ToString();
					FilterDataTable.Rows.Add(FilterRow);

				}

				
			}

			catch (System.Exception a_Ex)
			{
				MessageBox.Show(a_Ex.Message);
			}
		}

		public DataTable GetFilterDataTable()
		{
			return FilterDataTable;
		}

        private void ButtonSelect_Click(object sender, EventArgs e)
        {

        }
	}
}
