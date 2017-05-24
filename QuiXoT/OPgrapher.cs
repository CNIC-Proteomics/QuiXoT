using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Globalization;

namespace QuiXoT
{
    public partial class OPgrapher : Form, Iobserver
    {
        Bitmap bm;
        Graphics gr;

        private DataView dvData;
        private double[] extraData;
        OPquan opq;

        private string colX;
        private string colY1;
        private string colY2;
        private string colY3;
        private bool normGraph;
        private bool sigmoidalGraph;

        private ArrayList NZi;
        private ArrayList NZiTheor;
        private ArrayList Zi;
        private ArrayList ZiTheor;
        
        private ArrayList rowsSelected;
        private ArrayList idxRowsSelected;

        private double maxX;
        private double maxY;
        private double minX;
        private double minY;

        private double scaleX;
        private double scaleY;

        private float pixUsedwidth;
        private float pixUsedheight;

        private dotSize currentDotSize = dotSize.normal;
        private int unselectedDot = 3;
        private int selectedDot = 4;
        private int unselectedDotBig = 9;
        private int selectedDotBig = 20;
        private int unselectedDotCurrent = 3;
        private int selectedDotCurrent = 4;
        private int tickFontNormal = 8;
        private int tickFontBig = 20;
        private int tickFontCurrent = 8;
        // important to convert to and from screen pixels
        private int borderTop = 31;
        private int borderRight = 5;

        // Not used now, as they were used in the intelligent zoom
        //private bool selectEnabled=false;
        ////private bool zoomChecked = false;
        //Point MouseSel1;
        //Point MouseSel2;

        double[][] dataX = new double[3][];
        double[][] dataY = new double[3][];

        double[][] paintX = new double[3][];
        double[][] paintY = new double[3][];


        float heightPercent = 0.8F;
        float heightPercent2 = 0.95F;
        float widthPercent = 0.8F;
        float MarginGraph = 0.11F; //0.07F;

        private int maxNumOfTicksX = 20 ;
        private int maxNumOfTicksY = 20;
        private string formatNumX = "0.#E+0";
        private string formatNumY = "0.#E+0";
        // fixed by boundaries
        private bool fixedXmin = false;
        private bool fixedXmax = false;
        private bool fixedYmin = false;
        private bool fixedYmax = false;
        // fixed by zoom
        private bool zfixedXmin = false;
        private bool zfixedXmax = false;
        private bool zfixedYmin = false;
        private bool zfixedYmax = false;
        private double maxXfixed;
        private double maxYfixed;
        private double minXfixed;
        private double minYfixed;
        private bool logX_on = false;
        private bool logY_on = false;

            
        private Color[] brushcolor;
        private Brush[] brush;
        private Brush[] brushSel;
        private Pen[] penGrph;
        private Pen[] penGrphSel;

        private double graphExtraMargin = 0.02;

        // variables for zoom device
        Point originalMousePosition = new Point();
        Point currentMousePosition = new Point();
        Boolean zooming = false;
        int extraBorderForZoomingBoxLimit= 2;

        public OPgrapher(   DataView dv,
                            string scX, 
                            string scY1, 
                            string scY2, 
                            string scY3,
                            bool nGraph,
                            bool sigmGraph,
                            double[] xData)
        {
            //bm = new Bitmap(pbxGraph.Width, pbxGraph.Height, graph);

            //graph.DrawEllpise(Pens.Blue, new Rectangle(10, 10, 10, 10));

            //pbxGraph.Image = bm;

            opq = (OPquan)this.Owner;
            extraData = xData;

            InitializeComponent();

            bm = new Bitmap(pbxGraph.Width, pbxGraph.Height);
            gr = Graphics.FromImage(bm);

            dvData = dv;
            rowsSelected = new ArrayList();

            maxNumOfTicksX = 6;
            maxNumOfTicksY = 6;
            formatNumX = "0.#E+0";
            formatNumY = "0.#E+0";

            setDotSize(currentDotSize);

            this.colX = scX;
            this.colY1 = scY1;
            this.colY2 = scY2;
            this.colY3 = scY3;
            this.normGraph = nGraph;
            this.sigmoidalGraph = sigmGraph;

            if (normGraph)
            {
                if (sigmoidalGraph)
                {
                    this.colY1 = "rank/N (theor)";
                    this.colY2 = "rank/N";
                }
                else
                {
                    this.colY1 = this.colX + " (theor)";
                    this.colY2 = this.colX;
                }

                // cambio de filtros por un momento***
                bool ZiWellCalculated = ZiCalculation();

                if (!ZiWellCalculated)
                    this.Close();
            }
                       
            brushcolor = new Color[3];
            brushcolor[0] = Color.FromArgb(0, 0, 125);
            brushcolor[1] = Color.FromArgb(125, 0, 0);
            brushcolor[2] = Color.FromArgb(0, 125, 0);

            brush = new Brush[3];
            brush[0] = new SolidBrush(brushcolor[0]);
            brush[1] = new SolidBrush(brushcolor[1]);
            brush[2] = new SolidBrush(brushcolor[2]);
                        
            penGrph = new Pen[3];
            penGrph[0] = new Pen(brush[0]);
            penGrph[1] = new Pen(brush[1]);
            penGrph[2] = new Pen(brush[2]);
            
            for (int i = 0; i <= penGrph.GetUpperBound(0); i++)
            {
                penGrph[i].Width = 1;
            }

            brushcolor[0] = Color.FromArgb(255, 20, 20);
            brushcolor[1] = Color.FromArgb(255, 130, 0);
            brushcolor[2] = Color.FromArgb(0, 255, 130);

            brushSel = new Brush[3];
            brushSel[0] = new SolidBrush(brushcolor[0]);
            brushSel[1] = new SolidBrush(brushcolor[1]);
            brushSel[2] = new SolidBrush(brushcolor[2]);
           

            penGrphSel = new Pen[3];
            penGrphSel[0] = new Pen(brushSel[0]);
            penGrphSel[1] = new Pen(brushSel[1]);
            penGrphSel[2] = new Pen(brushSel[2]);

            for (int i = 0; i <= penGrph.GetUpperBound(0); i++)
            {
                penGrphSel[i].Width = 2;
            }

            QuiXoT.statistics.Events.MouseHelper mh = new QuiXoT.statistics.Events.MouseHelper(this);
            mh.AddControl(this);

            //mh.LeftMouseDown += new MouseEventHandler(OnLeftMouseDown);
            //mh.LeftMouseUp += new MouseEventHandler(OnLeftMouseUp);
            //mh.RightClick += new EventHandler(mh_RightClick);
            //this.MouseMove += new MouseEventHandler(OPgrapher_MouseMove);

            this.Resize += new System.EventHandler(OPgrapher_Resize);
            this.GotFocus += new System.EventHandler(OPgrapher_gotFocus);
            this.Disposed += new System.EventHandler(OPgrapher_Disposed);
            this.Move += new System.EventHandler(OPgrapher_Move);

            clearGraph();
            DrawGraph();
        }

        private bool ZiCalculation()
        {
            NZi = new ArrayList();
            Zi = new ArrayList();
            NZiTheor = new ArrayList();
            ZiTheor = new ArrayList();

            MathNet.Numerics.Distributions.NormalDistribution normDist = new MathNet.Numerics.Distributions.NormalDistribution(0, 1);

            DataRow[] rowsToUse = new DataRow[0];

            if (colX == "Zs")
                rowsToUse = dvData.Table.Select("st_excluded = '' and scan_per_peptide > 1");

            if (colX == "Zp")
                rowsToUse = dvData.Table.Select("st_excluded = '' and s_index=1 and pep_per_protein > 1");

            if (colX == "Zq")
                rowsToUse = dvData.Table.Select("st_excluded = '' and s_index=1 and p_index=1");

            foreach (DataRow dr in rowsToUse)
            {
                try
                {
                    Zi.Add(double.Parse(dr[colX].ToString()));
                }
                catch(Exception e)
                {
                    MessageBox.Show("Error while parsing " + colX +
                            ".\nPlease, confirm that " +
                            colX + " has been properly calculated." +
                            ".\n\nProgram message: " + e.Message.ToString());
                    return false;
                }
            }

            Zi.Sort();
            //double mu = QuiXoT.math.Utilities.mean(Zi);
            //double sigma = QuiXoT.math.Utilities.standardDev(Zi);

            for (int i = 0; i < Zi.Count; i++)
            {
                double nth = (double)(i + 1) / (double)Zi.Count;
                double zth = normDist.InverseCumulativeDistribution(nth);
                ZiTheor.Add(zth);
                NZiTheor.Add(nth);
            }

            for (int i = 0; i < Zi.Count; i++)
            {
                double nn = (double)Zi[i];
                double z = normDist.CumulativeDistribution(nn);
                NZi.Add(z);
            }

            return true;
        }

        private void setDotSize(dotSize currentSize)
        {
            if (currentSize == dotSize.normal)
            {
                unselectedDotCurrent = unselectedDot;
                selectedDotCurrent = selectedDot;
                tickFontCurrent = tickFontNormal;
            }
            else
            {
                unselectedDotCurrent = unselectedDotBig;
                selectedDotCurrent = selectedDotBig;
                tickFontCurrent = tickFontBig;
            }
        }

        void mh_RightClick(object sender, EventArgs e)
        {
            MenuItem justRefresh = new MenuItem("refresh graph");
            MenuItem miExport = new MenuItem("export to CSV");
            MenuItem miSelLimits = new MenuItem("select bounds");
            MenuItem logX = new MenuItem("logarithmic scale for X");
            MenuItem logY = new MenuItem("logarithmic scale for Y");
            MenuItem toggleDotSize = new MenuItem("toggle dot size");
            MenuItem zoomOut = new MenuItem("zoom out");
            //MenuItem changeIndex = new MenuItem("select a new index for highlighting");

            //miZoom.Checked = this.zoomChecked;

            ContextMenu cmMenu = new ContextMenu();
            cmMenu.MenuItems.Add(justRefresh);
            cmMenu.MenuItems.Add("-");
            cmMenu.MenuItems.Add(miExport);
            cmMenu.MenuItems.Add(miSelLimits);
            cmMenu.MenuItems.Add(logX);
            cmMenu.MenuItems.Add(logY);
            cmMenu.MenuItems.Add(toggleDotSize);

            if(!normGraph)
                cmMenu.MenuItems.Add(zoomOut);
            //cmMenu.MenuItems.Add(changeIndex);

            if(!normGraph)
                zoomOut.Enabled = lblZoom.Visible;

            if (minX <= 0 || maxX <= 0)
            {
                logX.Enabled = false;
                logX_on = false;
            }

            if (minY <= 0 || maxY <= 0)
            {
                logY.Enabled = false;
                logY_on = false;
            }

            logX.Checked = logX_on;
            logY.Checked = logY_on;

            justRefresh.Click += new EventHandler(justRefresh_Click);
            miExport.Click += new EventHandler(miExport_Click);
            miSelLimits.Click += new EventHandler(miSelLimits_Click);

            if(!normGraph)
                zoomOut.Click += new EventHandler(zoomOut_Click);

            logX.Click += new EventHandler(logX_Click);
            logY.Click += new EventHandler(logY_Click);

            toggleDotSize.Click += new EventHandler(toggleDotSize_Click);

            this.ContextMenu = cmMenu;
            
            //throw new NotImplementedException();
        }

        //void OPgrapher_MouseMove(object sender, MouseEventArgs e)
        //{
        //    MouseSel2.X = e.X;
        //    MouseSel2.Y = e.Y;

        //    if (selectEnabled)
        //    {
        //        ZoomSelection(MouseSel1, MouseSel2);
        //    }
        //    //throw new NotImplementedException();
        //}

        //public void OnLeftMouseDown(object sender, MouseEventArgs e)
        //{
        //    if (zoomChecked)
        //    {
        //        MouseSel1.X = e.X;
        //        MouseSel1.Y = e.Y;

        //        selectEnabled = true;
        //    }
        //}

        //public void OnLeftMouseUp(object sender, MouseEventArgs e)
        //{
        //    if(zoomChecked)
        //    {
        //        MouseSel2.X = e.X;
        //        MouseSel2.Y = e.Y;

        //        int diffX = Math.Abs(MouseSel1.X - MouseSel2.X);
        //        int diffY = Math.Abs(MouseSel1.Y - MouseSel2.Y);

        //        int maxDiff = diffX >= diffY ? diffX : diffY;

        //        if (selectEnabled && maxDiff > 10)
        //        {
        //            //ZoomSelection(MouseSel1, MouseSel2);
        //            double valX1 = pixToData(MouseSel1.X, axis.X);
        //            double valX2 = pixToData(MouseSel2.X, axis.X);
        //            double valY1 = pixToData(MouseSel1.Y, axis.Y);
        //            double valY2 = pixToData(MouseSel2.Y, axis.Y);

        //            fixedXmax = true;
        //            fixedXmin = true;
        //            fixedYmax = true;
        //            fixedYmin = true;

        //            zoomFilter = preZoomFilter.ToString();

        //            if (zoomFilter.Trim() != "") zoomFilter += " AND ";
                    
        //            //X axis
        //            switch (valX1 <= valX2)
        //            {
        //                case true:
        //                    minX = valX1;
        //                    maxX = valX2;
        //                    zoomFilter +=  colX.ToString() + " >= " + valX1.ToString() + " AND " + colX.ToString() + " < " + valX2.ToString(); 
        //                    break;
        //                case false:
        //                    minX = valX2;
        //                    maxX = valX1;
        //                    zoomFilter +=  colX.ToString() + " < " + valX1.ToString() + " AND " + colX.ToString() + " >= " + valX2.ToString(); 
        //                    break;
        //            }

        //            //Y axis
        //            switch (valY1 <= valY2)
        //            {
        //                case true:
        //                    minY = valY1;
        //                    maxY = valY2;
        //                    zoomFilter += " AND " + colY1.ToString() + " >= " + valY1.ToString() + " AND " + colY1.ToString() + " < " + valY2.ToString();
        //                    break;
        //                case false:
        //                    minY = valY2;
        //                    maxY = valY1;
        //                    zoomFilter += " AND " + colY1.ToString() + " < " + valY1.ToString() + " AND " + colY1.ToString() + " >= " + valY2.ToString();
        //                    break;
        //            }

        //            dvData.RowFilter = zoomFilter;

        //            clearGraph();
        //            DrawGraph();
        //        }



        //        selectEnabled = false;
        //    }

        //}

        //private void ZoomSelection(Point xy1, Point xy2)
        //{

        //    Graphics gfx;
        //    gfx = this.CreateGraphics();
        //    gfx.Clear(Color.White);
        //    Random rnd = new Random();
        //    int r = rnd.Next(100);
        //    if (r <= 70)
        //    {
        //        DrawGraph();
        //    }

        //    Color brushColor = Color.FromArgb(0, 0, 125);
        //    Brush myBrush = new SolidBrush(brushColor);
        //    Pen penSel = new Pen(myBrush);
            
        //    int rectWidth = (int)Math.Abs(xy2.X - xy1.X);
        //    int rectHeight = (int)Math.Abs(xy2.Y - xy1.Y);

        //    Rectangle rect = new Rectangle(Math.Min(xy1.X, xy2.X), Math.Min(xy1.Y, xy2.Y), rectWidth, rectHeight);
        //    gfx.DrawRectangle(penSel, rect);

        //}

        void zoomOut_Click(object sender, EventArgs e)
        {
            if (lblZoom.Visible)
            {
                zfixedXmax = false;
                zfixedXmin = false;
                zfixedYmax = false;
                zfixedYmin = false;

                double mxx = getMax(dataX[0]);
                double mxy = getMax(dataY[0]);
                double mnx = getMin(dataX[0]);
                double mny = getMin(dataY[0]);

                double xdif = mxx - mnx;
                double ydif = mxy - mny;

                mxx += xdif * graphExtraMargin;
                mnx -= xdif * graphExtraMargin;
                mxy += ydif * graphExtraMargin;
                mny -= ydif * graphExtraMargin;

                if (!fixedXmax)
                    maxX = mxx;
                else
                    maxX = maxXfixed;

                if (!fixedXmin)
                    minX = mnx;
                else
                    minX = minXfixed;

                if (!fixedYmax)
                    maxY = mxy;
                else
                    maxY = maxYfixed;

                if (!fixedYmin)
                    minY = mny;
                else
                    minY = minYfixed;


                opq = (OPquan)this.Owner;
                opq.OPgrapherFilter = "";
                opq.filterBtn_Click(null, null);

                this.lblZoom.Visible = false;

                clearGraph();
                DrawGraph();
            }
        }

        //void miZoom_Click(object sender, EventArgs e)
        //{

            
        //    switch (zoomChecked)
        //    {
        //        case true:
        //            dvData.RowFilter = preZoomFilter;
        //            fixedXmax = false;
        //            fixedXmin = false;
        //            fixedYmax = false;
        //            fixedYmin = false;
        //            zoomChecked = false;
        //            clearGraph();
        //            DrawGraph();
        //            break;
        //        case false:
        //            preZoomFilter = dvData.RowFilter;
        //            zoomChecked = true;
        //            clearGraph();
        //            DrawGraph();
        //            break;
 
        //    }
        //    //throw new NotImplementedException();
        //}

        void miSelLimits_Click(object sender, EventArgs e)
        {

            OPselectBoundsGraph.reqOPselectBounds req = new OPselectBoundsGraph.reqOPselectBounds();
            
            req.xLowLimit = minX;
            req.xUpLimit = maxX;
            req.yLowLimit = minY;
            req.yUpLimit = maxY;
            req.fixedXmax = fixedXmax;
            req.fixedXmin = fixedXmin;
            req.fixedYmax = fixedYmax;
            req.fixedYmin = fixedYmin;
            req.xTickFormat = formatNumX;
            req.yTickFormat = formatNumY;
            req.xMaxTicks = maxNumOfTicksX;
            req.yMaxTicks = maxNumOfTicksY;

            OPselectBoundsGraph ops = new OPselectBoundsGraph(req);
            ops.ShowDialog();

            if (ops.btnOKpressed)
            {

                minX = ops.xLowLimit;
                maxX = ops.xUpLimit;
                minY = ops.yLowLimit;
                maxY = ops.yUpLimit;

                fixedXmin = ops.fixedXmin;
                fixedXmax = ops.fixedXmax;
                fixedYmin = ops.fixedYmin;
                fixedYmax = ops.fixedYmax;

                if (fixedXmax)
                    maxXfixed = maxX;
                else
                    zfixedXmax = true;

                if (fixedYmax)
                    maxYfixed = maxY;
                else
                    zfixedYmax = true;
                    
                if (fixedXmin)
                    minXfixed = minX;
                else
                    zfixedXmin = true;

                if (fixedYmin)
                    minYfixed = minY;
                else
                    zfixedYmin = true;

                if (fixedXmax && fixedXmin && fixedYmax && fixedYmin)
                    lblZoom.Visible = false;
                else
                {
                    double mxx = getMax(dataX[0]);
                    double mxy = getMax(dataY[0]);
                    double mnx = getMin(dataX[0]);
                    double mny = getMin(dataY[0]);

                    double xdif = mxx - mnx;
                    double ydif = mxy - mny;

                    mxx += xdif * graphExtraMargin;
                    mnx -= xdif * graphExtraMargin;
                    mxy += ydif * graphExtraMargin;
                    mny -= ydif * graphExtraMargin;

                    if (fixedXmax) mxx = maxX;
                    if (fixedYmax) mxy = maxY;
                    if (fixedXmin) mnx = minX;
                    if (fixedYmin) mny = minY;

                    bool maxXoK = Math.Abs(maxX) >= Math.Abs(mxx * 0.999) && Math.Abs(maxX) <= Math.Abs(mxx * 1.001);
                    bool maxYoK = Math.Abs(maxY) >= Math.Abs(mxy * 0.999) && Math.Abs(maxY) <= Math.Abs(mxy * 1.001);
                    bool minXoK = Math.Abs(minX) >= Math.Abs(mnx * 0.999) && Math.Abs(minX) <= Math.Abs(mnx * 1.001);
                    bool minYoK = Math.Abs(minY) >= Math.Abs(mny * 0.999) && Math.Abs(minY) <= Math.Abs(mny * 1.001);

                    if (maxXoK && maxYoK && minXoK && minYoK)
                        lblZoom.Visible = false;
                    else
                        lblZoom.Visible = true;
                }

                maxNumOfTicksX = ops.xMaxTicks;
                maxNumOfTicksY = ops.yMaxTicks;

                formatNumX = ops.xTickFormat;
                formatNumY = ops.yTickFormat;

                clearGraph();
                DrawGraph();
            }

        }

        void miExport_Click(object sender, EventArgs e)
        {
            exportToCSV();
        }

        void exportToCSV()
        {

            SaveFileDialog saveFD = new SaveFileDialog();

            //saveFD.InitialDirectory="c:\\";
            saveFD.Filter = "xls files in CSV format (*.xls)|*.xls|All files (*.*)|*.*";
            saveFD.FilterIndex =1;
            saveFD.RestoreDirectory = true;
            saveFD.Title = "Select a file name to save";
            saveFD.CheckPathExists=true;
            saveFD.AutoUpgradeEnabled=true;
            saveFD.AddExtension=true;

            if (saveFD.ShowDialog() == DialogResult.OK && saveFD.FileName != "")
            {
                if (normGraph)
                {
                    try 
                    {

                        StreamWriter sw = new StreamWriter(saveFD.FileName, false);

                        sw.Write(sw.NewLine);
                        sw.Write(sw.NewLine);
                        sw.Write(sw.NewLine);
                        sw.Write(sw.NewLine);


                        // First we will write the headers.
                        if (colX != null)
                        {
                            sw.Write(colX);
                            sw.Write("\t");
                        }

                        if (colY1 != null)
                        {
                            sw.Write(colY1);
                            sw.Write("\t");
                            sw.Write(colY2);
                            sw.Write("\t");
                        }

                        sw.Write(sw.NewLine);

                        //write the data
                        for (int i = 1; i < dataX[0].GetUpperBound(0); i++)
                        {
                            sw.Write(dataX[0][i + 1].ToString());
                            sw.Write("\t");
                            sw.Write(dataY[0][i + 1].ToString());
                            sw.Write("\t");
                            sw.Write(dataY[1][i + 1].ToString());
                            sw.Write("\t");
                            sw.Write(sw.NewLine);
                        }

                        sw.Close();

                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("Error: Could not save file to disk. Error: " + ex.Message);
                    }

                    return;
                }

                try
                {
                    // Create the CSV file to which grid data will be exported.

                    StreamWriter sw = new StreamWriter(saveFD.FileName, false);

                    sw.Write(sw.NewLine);
                    sw.Write(sw.NewLine);
                    sw.Write(sw.NewLine);
                    sw.Write(sw.NewLine);
                    
                    
                    // First we will write the headers.
                    int iColCount = this.dvData.Table.Columns.Count;
                    
                    for(int i=0;i<iColCount;i++)
                    {
                        sw.Write("\t");
                    }

                    if (colX != null)
                    {
                        sw.Write("  X  ");
                        sw.Write("\t");
                    }

                    if (colY1 != null)
                    {
                        sw.Write("  Y1  ");
                        sw.Write("\t");
                        sw.Write("  Y1 enlighted  ");
                        sw.Write("\t");
                    }

                    if (colY1 != null)
                    {

                        sw.Write("  Y2  ");
                        sw.Write("\t");
                        sw.Write("  Y2 enlighted  ");
                        sw.Write("\t");
                    }

                    if (colY1 != null)
                    {
                        sw.Write("  Y3  ");
                        sw.Write("\t");
                        sw.Write("  Y3 enlighted  ");
                        sw.Write("\t");
                    }


                    sw.Write(sw.NewLine);
                   
                    for (int i = 0; i < iColCount; i++)
                    {
                        sw.Write(dvData.Table.Columns[i].ColumnName);
                        sw.Write("\t");                        
                    }

                    if (colX != null)
                    {
                        sw.Write(dvData.Table.Columns[colX].ColumnName.ToString().ToLower());
                        sw.Write("\t");
                    }
                    if (colY1 != null)
                    {
                        sw.Write(dvData.Table.Columns[colY1].ColumnName.ToString().ToLower());
                        sw.Write("\t");
                        sw.Write(dvData.Table.Columns[colY1].ColumnName.ToString().ToLower());
                        sw.Write("\t");
                    }
                    if (colY2 != null)
                    {
                        sw.Write(dvData.Table.Columns[colY2].ColumnName.ToString().ToLower());
                        sw.Write("\t");
                        sw.Write(dvData.Table.Columns[colY2].ColumnName.ToString().ToLower());
                        sw.Write("\t");
                    }
                    if (colY3 != null)
                    {
                        sw.Write(dvData.Table.Columns[colY3].ColumnName.ToString().ToLower());
                        sw.Write("\t");
                        sw.Write(dvData.Table.Columns[colY3].ColumnName.ToString().ToLower());
                    }

                    sw.Write(sw.NewLine);


                    // Now write all the rows.
                    
                foreach (DataRowView dr in dvData)
                    {
                      
                        for (int i = 0; i < iColCount; i++)
                        {
                            if (!Convert.IsDBNull(dr[i]))
                            {
                                sw.Write(dr[i].ToString());
                            }
                            string cccc = dr[i].ToString();
 
                             sw.Write("\t");                            
                        }

                        if (colX != null)
                        {
                            if (!Convert.IsDBNull(dr[colX]))
                            {
                                sw.Write(dr[colX].ToString());
                            }
                        }
                        sw.Write("\t");

                        // Fill the Y1 columns
                        if (colY1 != null)
                        {
                            if (!Convert.IsDBNull(dr[colY1]))
                            {
                                int enlighted = -1;
                                try
                                {
                                    enlighted = int.Parse(dr["SpectrumIndex"].ToString());
                                }
                                catch { }

                                if (!rowsSelected.Contains(enlighted))
                                {
                                    sw.Write(dr[colY1].ToString());
                                    sw.Write("\t");
                                    sw.Write("\t");

                                }
                                else
                                {
                                    sw.Write("\t");
                                    sw.Write(dr[colY1].ToString());
                                    sw.Write("\t");
                                }

                            }
                            else
                            {
                                sw.Write("\t");
                                sw.Write("\t");

                            }
                        }


                        // Fill the Y2 columns
                        if(colY2!=null)
                        {
                            if (!Convert.IsDBNull(dr[colY2]))
                            {
                                int enlighted = -1;
                                try
                                {
                                    enlighted = int.Parse(dr["SpectrumIndex"].ToString());
                                }
                                catch { }

                                if (!idxRowsSelected.Contains(enlighted))
                                {
                                    sw.Write(dr[colY2].ToString());
                                    sw.Write("\t");
                                    sw.Write("\t");

                                }
                                else
                                {
                                    sw.Write("\t");
                                    sw.Write(dr[colY2].ToString());
                                    sw.Write("\t");
                                }

                            }
                            else
                            {
                                sw.Write("\t");
                                sw.Write("\t");

                            }
                        }
                        // Fill the Y3 columns
                        if (colY3 != null)
                        {

                            if (!Convert.IsDBNull(dr[colY3]))
                            {
                                int enlighted = -1;
                                try
                                {
                                    enlighted = int.Parse(dr["SpectrumIndex"].ToString());
                                }
                                catch { }

                                if (!idxRowsSelected.Contains(enlighted))
                                {
                                    sw.Write(dr[colY3].ToString());
                                    sw.Write("\t");
                                    sw.Write("\t");

                                }
                                else
                                {
                                    sw.Write("\t");
                                    sw.Write(dr[colY3].ToString());
                                    sw.Write("\t");
                                }

                            }
                            else
                            {
                                sw.Write("\t");
                                sw.Write("\t");

                            }
                        }
                      
                        sw.Write(sw.NewLine);
                    }
                

                    sw.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not save file to disk. Original error: " + ex.Message);
                }

            }


        }


        #region Miembros de Iobserver

        void Iobserver.Update(ArrayList selectedRows)
        {
            this.rowsSelected =(ArrayList) selectedRows.Clone();
            //this.originalFilter = originalFilter;

            try
            {
                clearGraph();
                DrawGraph();
            }
            catch { }
        }

        #endregion
 
        void OPgrapher_Disposed(object sender, System.EventArgs e)
        {
            sender = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public void OPgrapher_Resize(object sender, EventArgs ee)
        {
            pbxGraph.Size = new Size(this.Size.Width - 8, this.Size.Height - 34);
            clearGraph();
            DrawGraph();
        }

        public void OPgrapher_gotFocus(object sender, EventArgs ee)
        {
            //clearGraph();
            //DrawGraph();
        }

        public void OPgrapher_Move(object sender, EventArgs ee)
        {
            //clearGraph();
            //DrawGraph();
        }
         
        public void clearGraph() 
        {
            //next lines before bm = ...,
            // are only to avoid error when minimising window

            int thisHeight = pbxGraph.Height;
            int thisWidth = pbxGraph.Width;

            if (thisHeight < 1) thisHeight = 1;
            if (thisWidth < 1) thisWidth = 1;

            bm = new Bitmap(thisWidth, thisHeight);
            Graphics gr = Graphics.FromImage(bm);
            pbxGraph.BackgroundImage = bm;
        }

  
        public void DrawGraph()
        {
            #region Generate graph title

            if (!normGraph)
            {
                string xVar = colX.ToString();
                string yVar = "";

                if (colY2 != null | colY3 != null)
                {
                    yVar += "( ";

                }

                yVar += colY1.ToString();

                if (colY2 != null)
                {
                    yVar += " , " + colY2.ToString();
                }

                if (colY3 != null)
                {
                    yVar += " , " + colY3.ToString();
                }

                if (colY2 != null | colY3 != null)
                {
                    yVar += " )";

                }

                // usually it is written as y vs x
                this.Text = yVar + " vs " + xVar;
                //this.Text+= " vs " + colX.ToString();
            }
            else
            {
                if (sigmoidalGraph)
                    this.Text = "Sigmoidal ";
                else
                    this.Text = "Linear ";

                this.Text += "normality plot for " + colX.ToString();
                this.Text += " (K=" + extraData[0].ToString() + ", ";
                switch (colX.ToString().ToLower().Trim())
                {
                    case "zs":
                        {
                            this.Text += "s2S=" + extraData[1].ToString() + ")";
                            break;
                        }
                    case "zp":
                        {
                            this.Text += "s2P=" + extraData[2].ToString() + ")";
                            break;
                        }
                    case "zq":
                        {
                            this.Text += "s2Q=" + extraData[3].ToString() + ")";
                            break;
                        }
                }

            }

            #endregion


            // Get the graphics object
            Graphics gr = Graphics.FromImage(bm);

            Point xy1 = new Point();

            scale();

            idxRowsSelected.Sort();

            System.Drawing.Size pointSize = new Size(unselectedDotCurrent, unselectedDotCurrent);
            System.Drawing.Size pointSelSize = new Size(selectedDotCurrent, selectedDotCurrent);


            ArrayList alChannels = new ArrayList();
            for (int i = 0; i <= paintX.GetUpperBound(0); i++)
            {
                if (paintX[i] != null) alChannels.Add(i);
            }

            foreach (int channel in alChannels)
            {
                int catchedPoints = 0;
                for (int i = 1; i <= paintX[channel].GetUpperBound(0); i++)
                {
                    xy1.X = (int)paintX[channel][i]; // __Marco: I removed an apparently useless Math.Abs that was here
                    xy1.Y = (int)paintY[channel][i];

                    bool dotInsideFrame = xy1.X > dataToPix(minX, minY).X
                                            && xy1.X < dataToPix(maxX, maxY).X
                                            && xy1.Y < dataToPix(minX, minY).Y
                                            && xy1.Y > dataToPix(maxX, maxY).Y;

                    if (dotInsideFrame)
                    {
                        Rectangle rect = new Rectangle(xy1, pointSize);
                        Rectangle rectSel = new Rectangle(xy1, pointSelSize);

                        switch (idxRowsSelected.Contains(i))
                        {
                            case true:
                                try
                                {
                                    if (catchedPoints < 10)
                                    {
                                        //Pedro: Now we draw the selected points after drawing the 
                                        //non-selected points, in order to viwe them correctly
                                        //gfx.DrawArc(penGrphSel[channel], rectSel, 0, 360);
                                        //gfx.DrawRectangle(penGrphSel[channel], rectSel);
                                    }

                                }
                                catch { catchedPoints++; }
                                break;
                            case false:
                                try
                                {
                                    if (catchedPoints < 10)
                                    {
                                        gr.FillEllipse(penGrph[channel].Brush, rect);
                                    }
                                }
                                catch { catchedPoints++; }
                                break;
                        }
                    }
                }
                for (int i = 1; i <= paintX[channel].GetUpperBound(0); i++)
                {
                    xy1.X = (int)paintX[channel][i]; // __Marco: I removed an apparently useless Math.Abs that was here
                    xy1.Y = (int)paintY[channel][i];

                    bool dotInsideFrame = xy1.X > dataToPix(minX, minY).X
                                            && xy1.X < dataToPix(maxX, maxY).X
                                            && xy1.Y < dataToPix(minX, minY).Y
                                            && xy1.Y > dataToPix(maxX, maxY).Y;

                    if (dotInsideFrame)
                    {
                        Rectangle rect = new Rectangle(xy1, pointSize);
                        Rectangle rectSel = new Rectangle(xy1, pointSelSize);

                        switch (idxRowsSelected.Contains(i))
                        {
                            case true:
                                try
                                {
                                    if (catchedPoints < 10)
                                    {
                                        gr.FillEllipse(penGrphSel[channel].Brush, rectSel);
                                        //gfx.DrawRectangle(penGrphSel[channel], rectSel);
                                    }

                                }
                                catch { catchedPoints++; }
                                break;
                            case false:
                                try
                                {
                                    if (catchedPoints < 10)
                                    {
                                        //gfx.DrawRectangle(penGrph[channel], rect);
                                    }
                                }
                                catch { catchedPoints++; }
                                break;
                        }
                    }
                }
            }

            try
            {
                drawScales();
            }
            catch { }

            pbxGraph.BackgroundImage = bm;
        }

        public void drawScales()
        {

            double scaleX = maxX - minX;
            double scaleY = maxY - minY;

            Point ptMinXY = dataToPix(minX, minY);
            Point ptMaxXY = dataToPix(maxX, maxY);

            drawLine(ptMinXY.X, ptMinXY.Y, ptMaxXY.X, ptMinXY.Y);
            drawLine(ptMinXY.X, ptMinXY.Y, ptMinXY.X, ptMaxXY.Y);
            drawLine(ptMaxXY.X, ptMinXY.Y, ptMaxXY.X, ptMaxXY.Y);
            drawLine(ptMinXY.X, ptMaxXY.Y, ptMaxXY.X, ptMaxXY.Y);

            //X minimum
            //drawTick(minX, axis.X, 8);
            //X maximum
            //drawTick(maxX, axis.X, 8);
            //Y minimum
            //drawTick(minY, axis.Y, 8);
            //Y maximum
            //drawTick(maxY, axis.Y, 8);


            //middle ticks
            double rangeX = maxX - minX;
            double rangeY = maxY - minY;

            double logRangeX = Math.Round(Math.Log10(rangeX),0);
            double logRangeY = Math.Round(Math.Log10(rangeY), 0);

            int stepTicks = 1;

            ArrayList alTicksX = new ArrayList();
            ArrayList alTicksY = new ArrayList();

            int minValOfTickX = (int)Math.Round(minX * Math.Pow(10, -logRangeX + 1),0);
            int minValOfTickY = (int)Math.Round(minY * Math.Pow(10, -logRangeY + 1), 0);
            int maxValOfTickX = (int)Math.Round(maxX * Math.Pow(10, -logRangeX + 1), 0);
            int maxValOfTickY = (int)Math.Round(maxY * Math.Pow(10, -logRangeY + 1), 0);


            double valForTick = 0;

            for (int i = minValOfTickX + 1; i <= maxValOfTickX; i++)
            {
                valForTick = i * Math.Pow(10, logRangeX - 1);
                alTicksX.Add(valForTick);           
            }

            stepTicks = (int) Math.Round((double)alTicksX.Count /(double) maxNumOfTicksX);
            if (stepTicks == 0) stepTicks = 1;

            for (int step = 0; step < alTicksX.Count - 1; step = step + stepTicks)
            {
                double vft = (double)alTicksX[step];
                drawTick(vft, axis.X, tickFontCurrent);                
            }


            for (int i = minValOfTickY + 1; i <= maxValOfTickY; i++)
            {
                valForTick = i * Math.Pow(10, logRangeY - 1);
                alTicksY.Add(valForTick);
            }

            stepTicks = (int)Math.Round((double)alTicksY.Count / (double)maxNumOfTicksY);
            if (stepTicks == 0) stepTicks = 1;

            for (int step = 0; step < alTicksY.Count - 1; step = step + stepTicks)
            {
                double vft = (double)alTicksY[step];
                drawTick(vft, axis.Y, tickFontCurrent);
            }
           
        }

        private enum axis 
        {
            X,
            Y
        }

        private void drawTick(double value, axis ax, int fontPx)
        {
            float fTextPosX = 0.0F;
            float fTextPosY = 0.0F;

            Point ptMinXY = dataToPix(minX, minY);
            Point ptMaxXY = dataToPix(maxX, maxY);


            Point tickPos = new Point(ptMinXY.X, ptMinXY.Y);
            Point ptToTick = new Point(ptMinXY.X - 5, ptMinXY.Y + 5);

            string formatNum = "";

            switch (ax)
            {
                case axis.X:
                    tickPos = dataToPix(value, minY);
                    fTextPosX = (float)tickPos.X - (float)fontPx * 2;
                    fTextPosY = (float)tickPos.Y + (float)fontPx;
                    ptToTick.X = tickPos.X;
                    formatNum = formatNumX;
                    break;
                case axis.Y:
                    tickPos = dataToPix(minX, value);
                    fTextPosX = (float)tickPos.X - (float)fontPx * (float)5.6;
                    fTextPosY = (float)tickPos.Y - (float)fontPx;
                    ptToTick.Y = tickPos.Y;
                    formatNum = formatNumY;
                    break;
            }

            drawLine(tickPos.X, tickPos.Y, ptToTick.X, ptToTick.Y);
            writeText(fTextPosX, fTextPosY, fontPx, value.ToString(formatNum, CultureInfo.InvariantCulture));
        }

        public void drawLine(int x1, int y1, int x2, int y2) 
        {
            gr = Graphics.FromImage(bm);

            Point xy1 = new Point(x1, y1);
            Point xy2 = new Point(x2, y2);
            Color brushColor = Color.FromArgb(0, 0, 0);
            Brush myBrush = new SolidBrush(brushColor);
            Pen penGraph = new Pen(myBrush);
            penGraph.Width = 1;

            int catchedGfx = 0;

            try
            {
                if (catchedGfx < 10)
                {
                    gr.DrawLine(penGraph, xy1, xy2);
                }
            }
            catch 
            {
                catchedGfx++;
            }

            pbxGraph.BackgroundImage = bm;
        }

        private void writeText(float xPos, float yPos, int fontSize,string text)
        {
            Graphics gr = Graphics.FromImage(bm);

            PointF pf = new PointF(xPos, yPos);
            Color brushColor = Color.FromArgb(0,0,0);
            Brush myBrush = new SolidBrush(brushColor);
            
            gr.DrawString(text,new Font("Verdana",fontSize),myBrush,pf);

            pbxGraph.BackgroundImage = bm;
        }
 
        protected void scale()
        {

            pixUsedwidth = this.Width * widthPercent;
            pixUsedheight = this.Height * heightPercent;

            idxRowsSelected = new ArrayList();
            rowsSelected.Sort();

            //Positioning the channels.
            ArrayList nameChannels = new ArrayList(3);
            ArrayList numChannels = new ArrayList(3);

            if (colY1 != null)
            {
                nameChannels.Add(colY1);
                numChannels.Add(0);
            }

            if (colY2 != null)
            {
                nameChannels.Add(colY2);
                numChannels.Add(1);
            }
            if (colY3 != null)
            {
                nameChannels.Add(colY3);
                numChannels.Add(2);
            }


            foreach (int i in numChannels)
            {
                dataX[i] = new double[dvData.Count + 1];
                dataY[i] = new double[dvData.Count + 1];
            }


            bool oneChannelProcessed = false;
            foreach (int iChannel in numChannels)
            {
                string sChannel="";
                switch (iChannel)
                {
                    case 0:
                        sChannel = colY1;
                        break;
                    case 1:
                        sChannel = colY2;
                        break;
                    case 2:
                        sChannel = colY3;
                        break;
                    default:
                        sChannel = null;
                        break;
                }

                if (sChannel != null)
                {
                    int catchCounter = 0;                        
                    for (int j = 0; j < dvData.Count; j++)
                    {

                        try
                        {
                            if (catchCounter < 10)
                            {

                                if (!normGraph)
                                {
                                    dataX[iChannel][j + 1] = double.Parse(dvData[j].Row[colX].ToString());
                                    dataY[iChannel][j + 1] = double.Parse(dvData[j].Row[sChannel].ToString());
                                }
                                else
                                {
                                    dataX[iChannel][j + 1] = (double)Zi[j];

                                    if (sigmoidalGraph)
                                    {
                                        if (iChannel == 0)
                                            dataY[iChannel][j + 1] = (double)NZiTheor[j];

                                        if (iChannel == 1)
                                            dataY[iChannel][j + 1] = (double)NZi[j];
                                    }
                                    else
                                    {
                                        if (iChannel == 0)
                                            dataY[iChannel][j + 1] = (double)ZiTheor[j];

                                        if (iChannel == 1)
                                            dataY[iChannel][j + 1] = dataX[iChannel][j + 1];
                                    }

                                    

                                    //if (sigmoidalGraph)
                                    //    dataX[iChannel][j + 1] = (double)Zi[j];
                                    //else
                                    //    dataX[iChannel][j + 1] = double.Parse(NZiTheor[j].ToString());

                                    //if (iChannel == 0)
                                    //    dataY[iChannel][j + 1] = double.Parse(NZi[j].ToString());

                                    //if (iChannel == 1)
                                    //    dataY[iChannel][j + 1] = double.Parse(NZiTheor[j].ToString());
                                }
                            }
                        }
                        catch 
                        {
                            catchCounter++;
                        }

                        int tmpIndex = int.Parse(dvData[j].Row["SpectrumIndex"].ToString());

                        if (!oneChannelProcessed) 
                        {
                            if (rowsSelected.Contains(tmpIndex))
                            {
                                idxRowsSelected.Add((int)j + 1);
                            }
                        }
                        
                    }
                }

                oneChannelProcessed = true;

            }


            for (int iChannel = 0; iChannel <= dataX.GetUpperBound(0); iChannel++)
            {
                if (dataX[iChannel] != null)
                {
                    //initialize datapaint
                    paintX[iChannel] = new double[dataX[iChannel].GetUpperBound(0) + 1];
                    paintY[iChannel] = new double[dataY[iChannel].GetUpperBound(0) + 1];

                }
            }

            //Scaling the channels

            double mxx = getMax(dataX[0]);
            double mxy = getMax(dataY[0]);
            double mnx = getMin(dataX[0]);
            double mny = getMin(dataY[0]);

            double xdif = mxx - mnx;
            double ydif = mxy - mny;

            mxx += xdif * graphExtraMargin;
            mnx -= xdif * graphExtraMargin;
            mxy += ydif * graphExtraMargin;
            mny -= ydif * graphExtraMargin;

            if (!zfixedXmax)
                if(!fixedXmax)
                    maxX = mxx;
            else
                maxX = maxXfixed;

            if (!zfixedXmin)
                if(!fixedXmin)
                    minX = mnx;
            else
                minX = minXfixed;

            if (!zfixedYmax)
                if(!fixedYmax)
                    maxY = mxy;
            else
                maxY = maxYfixed;

            if (!zfixedYmin)
                if(!fixedYmin)
                    minY = mny;
            else
                minY = minYfixed;

            scaleX = maxX - minX;
            scaleY = maxY - minY;
            if (scaleY == 0) scaleY += 100;
            if (scaleX == 0) scaleX += 100;

            for (int iChannel = 0; iChannel <= dataX.GetUpperBound(0); iChannel++)
            {
                if (dataX[iChannel] != null)
                {
                    for (int i = 1; i <= dataX[iChannel].GetUpperBound(0); i++)
                    {
                        paintY[iChannel][i] = dataToPix(dataX[iChannel][i], dataY[iChannel][i]).Y; // -(int)(this.Height * heightPercent2); //(-(dataY[iChannel][i] - minY) * pixUsedheight / scaleY) - Math.Abs(this.Height * MarginGraph);
                        paintX[iChannel][i] = dataToPix(dataX[iChannel][i], dataY[iChannel][i]).X; //((dataX[iChannel][i] - minX) * pixUsedwidth / scaleX) + Math.Abs(this.Width * MarginGraph);

                        //p.Y = (int)(((-(y - minY) * pixUsedheight / scaleY) - Math.Abs(this.Height * MarginGraph)) + this.Height * heightPercent2);
                        //p.X = (int)Math.Abs(((x - minX) * pixUsedwidth / scaleX) + Math.Abs(this.Width * MarginGraph));

                    }
                }
            }

        }

        protected double getMax(double[] dta)
        {
            double mx = 0;

            int lim = dta.GetUpperBound(0);
            for (int i = 1; i <= lim; i++)
            {

                if (dta[i] >= mx)
                {
                    mx = dta[i];
                }
            }

            return mx;        
        }

        protected double getMin(double[] dta)
        {
            double mn = 1e50;

            int lim = dta.GetUpperBound(0);
            for (int i = 1; i <= lim; i++)
            {

                if (dta[i] <= mn)
                {
                    mn = dta[i];
                }
            }

            return mn;
        }


        private double pixToData(int pix, axis ax)
        {
            double val = 0;

            switch (ax)
            {
                case axis.X:
                    val = minX + scaleX / pixUsedwidth * (pix - Math.Abs(Width * MarginGraph));
                    val = toExpIfNeeded(val, logX_on, ax);
                    break;
                case axis.Y:
                    // __Marco: I had to fix this, as it gave back wrong data for some values (2009-03-12)
                    double innerVal = (pix + Math.Abs(Height * MarginGraph) - Height * heightPercent2) * scaleY / pixUsedheight;
                    // next line is a fix for Windows 7
                    int innerValSign = Math.Sign(pix + Math.Abs(Height * MarginGraph) - Height * heightPercent2);
                    val = -(innerVal + minY * innerValSign);
                    val = toExpIfNeeded(val, logY_on, ax);
                    break;
            }

            return val; 
        }

        protected Point dataToPix(double x, double y)
        {
            Point p = new Point();

            x = toLogIfNeeded(x, logX_on, axis.X);
            y = toLogIfNeeded(y, logY_on, axis.Y);

            p.Y = (int)((-(y - minY) * pixUsedheight / scaleY) - Math.Abs(this.Height * MarginGraph) + this.Height * heightPercent2);
            p.X = (int)((x - minX) * pixUsedwidth / scaleX + Math.Abs(this.Width * MarginGraph));

            return p;
        }

        public void changeXYcols(   string scX, 
                                    string scY1, 
                                    string scY2, 
                                    string scY3)
        {

            this.colX = scX;
            this.colY1 = scY1;
            this.colY2 = scY2;
            this.colY3 = scY3;
        }

        protected Point dataToScreenPix(double x, double y)
        {
            Point p = new Point();

            p.X = dataToPix(x, y).X + this.Bounds.Location.X + borderRight;
            p.Y = dataToPix(x, y).Y + this.Bounds.Location.Y + borderTop;

            return p;
        }

        private double screenPixToData(int pix, axis ax)
        {
            double val = 0;

            if (ax == axis.X)
            {
                val = pixToData(pix - this.Bounds.Location.X - borderRight, ax);
            }
            else
            {
                val = pixToData(pix - this.Bounds.Location.Y - borderTop, ax);
            }

            return val;

        }

        #region zoom device

        private void zoomStart(object sender, MouseEventArgs e)
        {
            originalMousePosition = MousePosition;
            currentMousePosition = MousePosition;

            if (Math.Abs(currentMousePosition.Y - originalMousePosition.Y) < 10)
            {
                currentMousePosition.Y = originalMousePosition.Y;
            }

            bool mouseClickedInsideBox;

            // useful to check them independently
            //int a1 = dataToScreenPix(maxX, maxY).X;
            //int a2 = dataToScreenPix(maxX, maxY).Y;
            //int a3 = dataToScreenPix(minX, minY).X;
            //int a4 = dataToScreenPix(minX, minY).Y;

            mouseClickedInsideBox =
                (originalMousePosition.X < dataToScreenPix(maxX, maxY).X + extraBorderForZoomingBox().X)
                && (originalMousePosition.Y > dataToScreenPix(maxX, maxY).Y - extraBorderForZoomingBox().Y)
                && (originalMousePosition.X > dataToScreenPix(minX, minY).X - extraBorderForZoomingBox().X)
                && (originalMousePosition.Y < dataToScreenPix(minX, minY).Y + extraBorderForZoomingBox().Y);

            if (mouseClickedInsideBox)
            {
                // first rectangle has size = 0
                Rectangle startRectangle = new Rectangle(originalMousePosition, new Size(new Point(0, 0)));
                ControlPaint.DrawReversibleFrame(startRectangle, Color.White, FrameStyle.Thick);

                zooming = true;
            }
        }

        private void zoomEnd(object sender, MouseEventArgs e)
        {
            if (!normGraph)
            {
                // the next if is important as when we click without dragging we go back to the original zoom
                if ((currentMousePosition != originalMousePosition) && zooming)
                {
                    Rectangle startRectangle = new Rectangle(originalMousePosition, new Size(currentMousePosition.X - originalMousePosition.X, currentMousePosition.Y - originalMousePosition.Y));
                    ControlPaint.DrawReversibleFrame(startRectangle, Color.White, FrameStyle.Thick);

                    // Redraw graph

                    zfixedXmax = true;
                    zfixedXmin = true;
                    zfixedYmax = true;
                    zfixedYmin = true;

                    // different minX and minY must be defiined previously, as the converter will use them to calculate the max
                    double minX2 = Math.Min(screenPixToData(originalMousePosition.X, axis.X), screenPixToData(currentMousePosition.X, axis.X));
                    double minY2 = Math.Min(screenPixToData(originalMousePosition.Y, axis.Y), screenPixToData(currentMousePosition.Y, axis.Y));
                    maxX = Math.Max(screenPixToData(originalMousePosition.X, axis.X), screenPixToData(currentMousePosition.X, axis.X));
                    maxY = Math.Max(screenPixToData(originalMousePosition.Y, axis.Y), screenPixToData(currentMousePosition.Y, axis.Y));

                    minX = minX2;
                    minY = minY2;

                    filterData();

                    this.lblZoom.Visible = true;

                    clearGraph();
                    DrawGraph();
                }

                zooming = false;
            }
        }

        private void filterData()
        {
            opq = (OPquan)this.Owner;
            opq.OPgrapherFilter = "";
            opq.OPgrapherFilter += colX.ToString() + " >= " + minX.ToString() + " AND " + colX.ToString() + " < " + maxX.ToString();

            if(!normGraph)
                opq.OPgrapherFilter += " AND " + colY1.ToString() + " >= " + minY.ToString() + " AND " + colY1.ToString() + " < " + maxY.ToString();

            dvData.RowFilter = opq.bothFilters(opq.OPquanFilter, opq.OPgrapherFilter);

            opq.filterTxt_TextChanged(null, null);
        }

        private void zoomMove(object sender, MouseEventArgs e)
        {
            if (zooming)
            {
                // deletes the previous rectangle
                Rectangle startRectangle = new Rectangle(originalMousePosition, new Size(-originalMousePosition.X + currentMousePosition.X, -originalMousePosition.Y + currentMousePosition.Y));
                currentMousePosition = MousePosition;

                if (Math.Abs(currentMousePosition.Y - originalMousePosition.Y) < 10)
                {
                    currentMousePosition.Y = originalMousePosition.Y;
                }

                //checks if mouse is outside form
                if (currentMousePosition.X > dataToScreenPix(maxX, maxY).X + extraBorderForZoomingBox().X)
                {
                    currentMousePosition.X = dataToScreenPix(maxX, maxY).X + extraBorderForZoomingBox().X;
                }

                if (currentMousePosition.Y < dataToScreenPix(maxX, maxY).Y - extraBorderForZoomingBox().Y)
                {
                    currentMousePosition.Y = dataToScreenPix(maxX, maxY).Y - extraBorderForZoomingBox().Y;
                }

                if (currentMousePosition.X < dataToScreenPix(minX, minY).X - extraBorderForZoomingBox().X)
                {
                    currentMousePosition.X = dataToScreenPix(minX, minY).X - extraBorderForZoomingBox().X;
                }

                if (currentMousePosition.Y > dataToScreenPix(minX, minY).Y + extraBorderForZoomingBox().Y)
                {
                    currentMousePosition.Y = dataToScreenPix(minX, minY).Y + extraBorderForZoomingBox().Y;
                }

                // draws new rectangle
                Rectangle endRectangle = new Rectangle(originalMousePosition, new Size(-originalMousePosition.X + currentMousePosition.X, -originalMousePosition.Y + currentMousePosition.Y));

                ControlPaint.DrawReversibleFrame(startRectangle, Color.White, FrameStyle.Thick);
                ControlPaint.DrawReversibleFrame(endRectangle, Color.White, FrameStyle.Thick);
            }

        }

        private Point extraBorderForZoomingBox()
        {
            Point p = new Point(extraBorderForZoomingBoxLimit, extraBorderForZoomingBoxLimit);

            if (logX_on)
            {
                p.X = 0;
            }

            if (logY_on)
            {
                p.Y = 0;
            }

            return p;
        }

        #endregion

        # region logarithmic scale

        private double toLogIfNeeded(double data, bool isLog, axis ax)
        {
            double max;
            double min;
            double valA;
            double valB;

            if (isLog)
            {
                if (ax == axis.X)
                {
                    max = maxX;
                    min = minX;
                }
                else
                {
                    max = maxY;
                    min = minY;
                }

                valA = (max - min) / (Math.Log(max, 10) - Math.Log(min, 10));
                valB = (min * Math.Log(max, 10) - max * Math.Log(min, 10)) / (Math.Log(max, 10) - Math.Log(min, 10));

                data = valA * Math.Log(data, 10) + valB;
            }

            return data;
        }

        private double toExpIfNeeded(double data, bool isLog, axis ax)
        {
            double max;
            double min;
            double valA;
            double valB;

            if (isLog)
            {
                if (ax == axis.X)
                {
                    max = maxX;
                    min = minX;
                }
                else
                {
                    max = maxY;
                    min = minY;
                }

                valA = (max - min) / (Math.Log(max, 10) - Math.Log(min, 10));
                valB = (min * Math.Log(max, 10) - max * Math.Log(min, 10)) / (Math.Log(max, 10) - Math.Log(min, 10));

                data = Math.Pow(10, (data - valB) / valA);
            }

            return data;
        }

        void logX_Click(object sender, EventArgs e)
        {
            logX_on = !(minX <= 0) && !(maxX <= 0) && !logX_on;
            clearGraph();
            DrawGraph();
        }

        void logY_Click(object sender, EventArgs e)
        {
            logY_on = !(minY <= 0) && !(maxY <= 0) && !logY_on;
            clearGraph();
            DrawGraph();
        }

        void justRefresh_Click(object sender, EventArgs e)
        {
            clearGraph();
            DrawGraph();
        }

        void toggleDotSize_Click(object sender, EventArgs e)
        {
            if (currentDotSize == dotSize.normal)
                currentDotSize = dotSize.big;
            else
                currentDotSize = dotSize.normal;

            setDotSize(currentDotSize);

            clearGraph();
            DrawGraph();
        }

        #endregion


        private void closing(object sender, FormClosingEventArgs e)
        {

        // __Marco: uncommenting these lines would restart the zoom filter after closing it
        // as it might be useful to select some data, I am going to leave it as it is

            if (lblZoom.Visible)
            {

                // ok
                fixedXmax = false;
                fixedXmin = false;
                fixedYmax = false;
                fixedYmin = false;

                dvData.RowFilter = opq.OPquanFilter;
                opq.OPgrapherFilter = "";
                opq.filterBtn_Click(null, null);

                this.lblZoom.Visible = false;
            }
        }

        private void mouse_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                mh_RightClick(sender, e);
            }

            if (e.Button == MouseButtons.Left && !normGraph)
            {
                zoomStart(sender, e);
            }
            
        }

        enum dotSize
        {
            normal,
            big
        }
    }

 
    public interface Iobserver
    {
        void Update(ArrayList selectedRows);
    }

}