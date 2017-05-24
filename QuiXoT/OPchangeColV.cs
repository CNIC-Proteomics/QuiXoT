using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Globalization;

namespace QuiXoT
{
    public partial class OPchangeColV : Form
    {

        public string selectedColumn;
        public string copyFromColumn;
        public string value;
        public string symbols;
        public bool okChange;
        public changeOption changeOpt;
        public bool valIsAOperation;
        public char[] operations;
        public double op;
        public ArrayList alVariables;
        public ArrayList alOperators;
        public string[] position;
        bool isSelectedColumn;
        bool isCopyFromColumn;
        public string[] concatColumns;

        //logarithm function
        public bool log;
        public double logbase = 0;

        //changeOption may be: 
        //                      0 -- no option selected
        //                      1 -- constant value for any row or formula
        //                      2 -- copy values from another column
        //                      3 -- calculate DeltaMass

        public OPchangeColV()
        {
            InitializeComponent();
            alVariables = new ArrayList();
            alOperators = new ArrayList();
            okChange = false;
            changeOpt = changeOption.none;
            value = "";
            valIsAOperation = false;
            txtValue.KeyUp += new KeyEventHandler(txtValue_KeyUp);
            operations = new char[5];
            operations[0] = '+';
            operations[1] = '-';
            operations[2] = '*';
            operations[3] = '/';
            operations[4] = '^';
            isSelectedColumn = false;
            isCopyFromColumn = false;

        }

        void txtValue_KeyUp(object sender, KeyEventArgs e)
        {

            alOperators.Clear();

            try
            {
                if (txtValue.Text.IndexOf('=', 0, 1) != -1) valIsAOperation = true;
                else valIsAOperation = false;
            }
            catch { valIsAOperation = false; }
            if (valIsAOperation)
            {
                int numOp = 0;
                op = 0;

                position = txtValue.Text.Substring(1).Split(operations, StringSplitOptions.RemoveEmptyEntries);

                double[] valPosition = new double[position.Length];


                int idx = 0;
                while (idx != -1)
                {
                    idx = txtValue.Text.IndexOfAny(operations, idx + 1);
                    try
                    {
                        alOperators.Add(txtValue.Text.Substring(idx, 1));
                    }
                    catch { }
                }

                foreach (string pos in position)
                {
                    if (alVariables.Contains(pos.Trim()))
                    {
                        //en los cálculos, pasar el valor double.Parse(row[pos.Trim()])
                        valPosition[numOp] = numOp + 1.4;
                        numOp++;
                    }
                    else
                    {
                        try
                        {
                            double val = double.Parse(pos.Trim());
                            valPosition[numOp] = val;
                            numOp++;
                        }
                        catch { valIsAOperation = false; }
                    }
                }

                if (valIsAOperation)
                {
                    try
                    {
                        bool firstCharIsOp = (txtValue.Text.Substring(1, 1).IndexOfAny(operations) != -1);
                        int myPos = 0;

                        switch (firstCharIsOp)
                        {
                            case true:
                                op = 0;
                                myPos = 0;
                                while (myPos <= valPosition.GetUpperBound(0))
                                {
                                    op = operate(op, valPosition[myPos], (string)alOperators[myPos]);
                                    myPos++;
                                }

                                break;
                            case false:
                                op = valPosition[0];
                                myPos = 1;
                                while (myPos <= valPosition.GetUpperBound(0))
                                {
                                    op = operate(op, valPosition[myPos], (string)alOperators[myPos - 1]);
                                    myPos++;
                                }
                                break;
                        }
                    }
                    catch { }
                }

            }

        }

        public double operate(double prevVal, double var, char operation)
        {
            double result = prevVal;
            switch (operation)
            {
                case '+':
                    result += var;
                    break;
                case '-':
                    result -= var;
                    break;
                case '*':
                    result *= var;
                    break;
                case '/':
                    result /= var;
                    break;
                case '^':
                    result = Math.Pow(prevVal, var);
                    break;
            }

            return result;

        }
        public double operate(double prevVal, double var, string operation)
        {
            double result = prevVal;
            switch (operation)
            {
                case "+":
                    result += var;
                    break;
                case "-":
                    result -= var;
                    break;
                case "*":
                    result *= var;
                    break;
                case "/":
                    result /= var;
                    break;
                case "^":
                    result = Math.Pow(prevVal, var);
                    break;
            }

            return result;

        }

        public void SetSourceColumns(DataColumnCollection Columns)
        {
            try
            {
                foreach (DataColumn col in Columns)
                {
                    cmbColumn.Items.Add(col.ColumnName.ToString());
                    cmbCopyVals.Items.Add(col.ColumnName.ToString());

                    if (col.DataType.IsValueType)
                    {
                        alVariables.Add(col.ColumnName.ToString());
                    }

                }

            }
            catch (System.Exception a_Ex)
            {
                MessageBox.Show(a_Ex.Message);
            }
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isSelectedColumn)
                {
                    MessageBox.Show("You have not selected the target column", "Error");
                    return;
                }

                selectedColumn = cmbColumn.Text;
                if (changeOpt == changeOption.copyValues)
                {
                    if (!isCopyFromColumn)
                    {
                        MessageBox.Show("You have not selected the /'copy from/' column", "Error");
                        return;
                    }

                    if (this.logScale.Checked)
                    {
                        log = true;
                        bool tryBase = double.TryParse(logBase.Text.Trim(), NumberStyles.Any, CultureInfo.InvariantCulture.NumberFormat, out logbase);
                        if (!tryBase)
                        {
                            logbase = Math.E;
                        }
                    }

                    copyFromColumn = cmbCopyVals.Text;
                }

                if (changeOpt == changeOption.concatenateFields)
                {
                    string[] concatColumnArray = txtConcat.Text.Split(',');
                    for (int i = 0; i < concatColumnArray.Length; i++)
                        concatColumnArray[i] = concatColumnArray[i].Trim();

                    this.concatColumns = concatColumnArray;
                }

                value = txtValue.Text;
                okChange = true;
                this.Dispose();
            }
            catch
            {
                MessageBox.Show("You have not selected any column");
                okChange = false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void optConcatenateVal_CheckedChanged(object sender, EventArgs e)
        {
            changeEnabled();

            if (optConcatenateVal.Checked)
                changeOpt = changeOption.concatenateFields;
        }

        private void optNewVal_CheckedChanged(object sender, EventArgs e)
        {
            changeEnabled();

            if (optNewVal.Checked)
                this.changeOpt = changeOption.newValue;
        }

        private void optCopyVals_CheckedChanged(object sender, EventArgs e)
        {
            changeEnabled();

            if (optCopyVals.Checked)
                this.changeOpt = changeOption.copyValues;
        }

        private void changeEnabled()
        {
            this.cmbCopyVals.Enabled = optCopyVals.Checked;
            this.logBase.Enabled = optCopyVals.Checked;
            this.logScale.Enabled = optCopyVals.Checked;
            this.txtLogBase.Enabled = optCopyVals.Checked;
            this.txtSymbols.Enabled = optDeltaMass.Checked;
            this.txtConcat.Enabled = optConcatenateVal.Checked;
            this.txtValue.Enabled = optNewVal.Checked;
        }

        private void optDeltaMass_CheckedChanged(object sender, EventArgs e)
        {

            changeEnabled();

            if (optDeltaMass.Checked)
                this.changeOpt = changeOption.calculateDMass;
        }


        private void cmbColumn_TextChanged(object sender, EventArgs e)
        {
            string text = this.cmbColumn.Text.ToUpper();

            for (int i = 0; i < this.cmbColumn.Items.Count; i++)
            {
                string item = this.cmbColumn.Items[i].ToString().ToUpper();
                if (item == text)
                {
                    this.cmbColumn.SelectedIndex = i;
                    isSelectedColumn = true;
                    break;
                }
            }

        }

        private void cmbCopyVals_TextChanged(object sender, EventArgs e)
        {
            string text = this.cmbCopyVals.Text.ToUpper();

            for (int i = 0; i < this.cmbCopyVals.Items.Count; i++)
            {
                string item = this.cmbCopyVals.Items[i].ToString().ToUpper();
                if (item == text)
                {
                    this.cmbCopyVals.SelectedIndex = i;
                    isCopyFromColumn = true;
                    break;
                }
            }

        }

        private void txtSymbols_TextChanged(object sender, EventArgs e)
        {
            this.symbols = this.txtSymbols.Text.Trim();
        }
    }

    public enum changeOption
    {
        none,
        newValue,
        concatenateFields,
        copyValues,
        calculateDMass
    }
}