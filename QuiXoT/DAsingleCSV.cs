using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using QuiXoT.math;
using System.Globalization;

namespace QuiXoT
{
      
    public class DAsingleCSV
    {

        public Comb.mzI[] extData;  //this array will contain the data from the external source (.txt or .raw , and so on)
                
        public Boolean readCSV(string sFile)
        {
            //mzI[] dta;
                      
            try 
            {
                char [] temp = new char[4];
                StreamReader sr = new StreamReader(File.OpenRead(sFile));
                ArrayList myAL = new ArrayList();
                
                try
			    {
				    while(sr.Peek() != -1) 
				    {
					    myAL.Add(sr.ReadLine()); 
				    }
			    }
			    catch(EndOfStreamException e)
			    {
				    Console.WriteLine(" Parse error: " + e);
			    }
			    int r =myAL.Count;
                extData = new Comb.mzI[r + 1];
                string[] str;
                int n = 1;

                System.Globalization.NumberFormatInfo nfi = new System.Globalization.NumberFormatInfo();
                nfi.NumberGroupSeparator = ",";
                nfi.NumberDecimalSeparator = ".";
                


                foreach (object o in myAL)
                {
                    str = Regex.Split(o.ToString(), "\t");
                    System.Collections.IEnumerator myEnumerator = str.GetEnumerator();
                
                    
                    myEnumerator.MoveNext();
                    int iSeparator = myEnumerator.Current.ToString().IndexOf(",");
                    string sData = myEnumerator.Current.ToString().Substring(0, iSeparator);
                    string sData2 = myEnumerator.Current.ToString().Substring(iSeparator+1);
                   
                    extData[n].mz = double.Parse(sData,nfi);
                    extData[n].I = double.Parse((string)sData2, nfi);
                   
                    n++; 
                              
                }
                return true;
                
            } 
            catch 
            {
                if (File.Exists(sFile))
                {
                    MessageBox.Show("read error");
                    return false;
                }
                else 
                {
                    MessageBox.Show("No file");
                    return false;
                }
            }

        }
    }
}
