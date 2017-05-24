using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using QuiXoT.math;
using System.Collections;
using System.Globalization;

namespace QuiXoT
{
 
    public partial class OPviewer : Form
    {
        Bitmap bm;
        Graphics gr;

        Comb.mzI[][] dataReal = new Comb.mzI[3][];
        Comb.mzI[][] dataPaint = new Comb.mzI[3][];
        double MonoisotMass = 0; 

        private string workFolder;

        private double maxX;
        private double maxY;
        private double minX;
        private double minY;
        // so the maximum does not line up with the lid of a peak
        private double maxMargin = 1.05;

        // id the zooming pixel distance is lower than this,
        // the zoom device will operate only along the x axis
        private int minimumYZoom = 10;

        private double scaleX;
        private double scaleY;

        private float pixUsedwidth;
        private float pixUsedheight;

        int maxNumOfTicksX = 20;
        int maxNumOfTicksY = 20;
        string formatNumX = "#.###";
        string formatNumY = "#.###";

        float heightPercent=0.8F;
        float heightPercent2 = 0.95F;
        float widthPercent = 0.8F;
        float MarginGraph = 0.11F;

        // important to convert to and from screen pixels
        private int borderTop = 31;
        private int borderRight = 5;

        // this can be only "centroid" or "profile"
        LNquantitate.spectrumType methodTypeUsed;
        LNquantitate.quantitationStrategy quantStrategy;
        double width = 0;

        bool originalMargins = true;
        bool horizontalZoom = false;
        bool precursorLineOn = true;
        bool peakToleranceShown = false;
        int minLidPix = 6; // use an even number for this

        public OPviewer()
        {
            InitializeComponent();

            this.Resize += new EventHandler(OPviewer_Resize);
            this.GotFocus += new EventHandler(OPviewer_gotFocus);
            this.Disposed += new System.EventHandler(OPviewer_Disposed);
            this.Move += new EventHandler(OPviewer_Move);

            methodTypeUsed = LNquantitate.spectrumType.profile;

        }


        public OPviewer(LNquantitate.spectrumType spectType,
            LNquantitate.quantitationStrategy strategy,
            double widthForHR,
            string _workFolder)
        {
            InitializeComponent();

            this.Resize += new EventHandler(OPviewer_Resize);
            this.GotFocus += new EventHandler(OPviewer_gotFocus);
            this.Disposed += new System.EventHandler(OPviewer_Disposed);
            this.Move += new EventHandler(OPviewer_Move);

            workFolder = _workFolder;

            methodTypeUsed = spectType;
            quantStrategy = strategy;
            width = widthForHR;
        }

        void OPviewer_Disposed(object sender, System.EventArgs e)
        {
            //OPviewer viewer = new OPviewer();
            //throw new System.Exception
            //      ("The method or operation is not implemented.");
            sender = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public void OPviewer_Resize(object sender, EventArgs ee)
        {
            pbxGraph.Size = new Size(this.ClientSize.Width - this.DefaultMargin.Left,
                this.ClientSize.Height- this.DefaultMargin.Top);
            
            clearGraph();
        
            for (int i = 0; i <= dataReal.GetUpperBound(0); i++) 
            {
                if (dataReal[i] != null) DrawGraph(i);
            }

        }
        public void OPviewer_gotFocus(object sender, EventArgs ee) 
        {
            //clearGraph();

            //for (int i = 0; i <= dataReal.GetUpperBound(0); i++)
            //{
            //    if (dataReal[i] != null) DrawGraph(i);
            //}
        }
        public void OPviewer_Move(object sender, EventArgs ee)
        {
            //clearGraph();

            //for (int i = 0; i <= dataReal.GetUpperBound(0); i++)
            //{
            //    if (dataReal[i] != null) DrawGraph(i);
            //}
        }

        public void addGraph(int channel, Comb.mzI[] datagraph) 
        {
            dataReal[channel] = (Comb.mzI[])datagraph.Clone();
        }

        public void addGraph(int channel, Comb.mzI[] datagraph,
                                double _monoisotMass)
        {
            dataReal[channel] = (Comb.mzI[])datagraph.Clone();
            MonoisotMass = _monoisotMass;
        }

        public void delGraph(int channel)
        {
   
            dataReal[channel] = null;
            dataPaint[channel] = null;

        }
        public void flushGraph() 
        {
            clearGraph();
            for (int i = 0; i <= dataReal.GetUpperBound(0); i++) 
            {
                dataReal[i] = null;
                dataPaint[i] = null;
            }
        }
        public void clearGraph() 
        {
            bm = new Bitmap(pbxGraph.Width, pbxGraph.Height);
            gr = Graphics.FromImage(bm);
            pbxGraph.BackgroundImage = bm;
        }

        public void DrawGraph(int channel, string title)
        {
            if (title.Trim().Length > 0) this.Text = title.Trim();
            lblPeakShown.Visible = peakToleranceShown;

            // Get the graphics object
            gr = Graphics.FromImage(bm);

            Point xy1 = new Point();
            Point xy2 = new Point();

            Color brushColor = Color.FromArgb(0, 0, 125);
            Brush myBrush = new SolidBrush(brushColor);
            Color brushColor2 = Color.FromArgb(255, 0, 0);
            Brush myBrush2 = new SolidBrush(brushColor2);
            Color brushColor3 = Color.FromArgb(0, 125, 0);
            Brush myBrush3 = new SolidBrush(brushColor3);


            Pen[] penGrph = new Pen[3];
            penGrph[0] = new Pen(myBrush);
            penGrph[1] = new Pen(myBrush2);
            penGrph[2] = new Pen(myBrush3);
            for (int i = 0; i <= penGrph.GetUpperBound(0); i++)
            {
                penGrph[i].Width = 1;
            }

            if (horizontalZoom)
            {
                this.lblZoom.Text = "h zoom on";
            }
            else
            {
                this.lblZoom.Text = "zoom on";
            }

            bool primChannelFound = false;
            int primChannel = dataReal.GetUpperBound(0) + 1;
            for (int i = 0; i <= dataReal.GetUpperBound(0); i++)
            {
                if (!primChannelFound && dataReal[i] != null)
                {
                    primChannel = i;
                    primChannelFound = true;
                }
            }

            scale(primChannel);

            // used for HR lids
            Comb.mzI[] centroided = null;
            Point lid1 = new Point();
            Point lid2 = new Point();
            bool drawLid = false;
            bool drawCentroid = false;

            for (int i = 0; i < dataPaint[channel].GetUpperBound(0); i++)
            {
                // __Marco: I took out the absolute value for X,
                // as it was the reasons for reflections
                // instead of that, now it uses drawLineInsideFrame
                if (methodTypeUsed == LNquantitate.spectrumType.profile)
                {
                    xy1.X = (int)dataPaint[channel][i].mz;
                    xy1.Y = (int)(dataPaint[channel][i].I
                        + this.Height * heightPercent2);
                    xy2.X = (int)dataPaint[channel][i + 1].mz;
                    xy2.Y = (int)(dataPaint[channel][i + 1].I
                        + this.Height * heightPercent2);
                }

                if (methodTypeUsed == LNquantitate.spectrumType.centroid)
                {
                    drawCentroid = true;
                    if ((dataReal[channel][i].mz > 113.5 && dataReal[channel][i].mz < 117.5)
                        && quantStrategy == LNquantitate.quantitationStrategy.iTRAQ)
                        drawLid = true;
                    else
                        drawLid = false;
                }

                if ((channel == 1) &&
                        (quantStrategy == LNquantitate.quantitationStrategy.O18_HR ||
                        quantStrategy == LNquantitate.quantitationStrategy.SILAC_HR))
                {
                    drawCentroid = true;
                    drawLid = true;

                    if (dataReal[1] != null && dataReal[0] != null) // this means it has been quantified
                    {
                        IQuantitation quan = null;
                        if (quantStrategy == LNquantitate.quantitationStrategy.O18_HR)
                            quan = new Q18OHR();
                        if (quantStrategy == LNquantitate.quantitationStrategy.SILAC_HR)
                            quan = new QSilacHR();

                        centroided = quan.centroiding((Comb.mzI[])dataReal[0],
                                                (Comb.mzI[])dataReal[1], width);
                    }
                }

                if (drawCentroid)
                {
                    xy1.X = (int)dataPaint[channel][i].mz;
                    xy1.Y = (int)(dataToPix(minX, minY).Y);
                    xy2.X = xy1.X;
                    xy2.Y = (int)(Math.Min(dataPaint[channel][i].I
                        + this.Height * heightPercent2,
                        dataToPix(minX, minY).Y));

                    drawLineInsideFrame(gr, penGrph[channel], xy1, xy2);

                    if (drawLid)
                    {
                        lid1 = dataToPix(dataReal[channel][i].mz - width / 2, dataReal[channel][i].I);
                        lid2 = dataToPix(dataReal[channel][i].mz + width / 2, dataReal[channel][i].I);

                        // this is calculated only for the first lid
                        // otherwise it might lead to errors, due to the fact that
                        // sometimes small changes can exist for the number of pixels
                        if (i == 0)
                        {
                            double lidLength = XdataToPixDouble(minX + width) - XdataToPixDouble(minX);
                            if (lidLength < minLidPix)
                                peakToleranceShown = false;
                            else
                                peakToleranceShown = true;
                        }

                        if (!peakToleranceShown)
                        {
                            lid1 = dataToPix(dataReal[channel][i].mz, dataReal[channel][i].I);
                            lid2 = dataToPix(dataReal[channel][i].mz, dataReal[channel][i].I);
                            lid1.X = lid1.X - minLidPix / 2;
                            lid2.X = lid2.X + minLidPix / 2;
                        }

                        checkLidMargins(ref lid1, ref lid2);

                        drawLineInsideFrame(gr, penGrph[channel], lid1, lid2);
                    }
                }
                else
                {
                    drawLineInsideFrame(gr, penGrph[channel], xy1, xy2);
                }

                lblPeakShown.Visible = peakToleranceShown;

                //drawLineInsideFrame(gfx, penGrph[channel], xy1, xy2); 
                //xy1.X = (int)dataPaint[channel][i].mz;
                //xy1.Y = (int)(dataPaint[channel][i].I + this.Height * heightPercent2);
                //xy2.X = (int)dataPaint[channel][i + 1].mz;
                //xy2.Y = (int)(dataPaint[channel][i + 1].I + this.Height * heightPercent2);

                //gfx.DrawLine(penGrph[channel], xy1, xy2);

            }

            if (drawLid)
            {
                int ch = 0;
                if (quantStrategy == LNquantitate.quantitationStrategy.iTRAQ)
                {
                    centroided = (Comb.mzI[])dataReal[channel].Clone();
                    ch = 1;
                }

                for (int i = 0; i < centroided.Length; i++)
                {
                    if (peakToleranceShown)
                    {
                        lid1 = dataToPix(centroided[i].mz - width / 2, centroided[i].I);
                        lid2 = dataToPix(centroided[i].mz + width / 2, centroided[i].I);
                    }
                    else
                    {
                        lid1 = dataToPix(centroided[i].mz, centroided[i].I);
                        lid2 = dataToPix(centroided[i].mz, centroided[i].I);
                        lid1.X = lid1.X - minLidPix / 2;
                        lid2.X = lid2.X + minLidPix / 2;
                    }

                    checkLidMargins(ref lid1, ref lid2);

                    drawLineInsideFrame(gr, penGrph[ch], lid1, lid2);
                }
            }
            
            //Draw the last point of the array for the centroid method
            if (methodTypeUsed == LNquantitate.spectrumType.centroid)
            {
                xy1.X = (int)dataPaint[channel][dataPaint[channel].GetUpperBound(0)].mz;
                xy1.Y = (int)(dataToPix(minX, minY).Y);
                xy2.X = xy1.X;
                xy2.Y = (int)(Math.Min(dataPaint[channel]
                    [dataPaint[channel].GetUpperBound(0)].I
                    + this.Height * heightPercent2,
                    dataToPix(minX, minY).Y));

                drawLineInsideFrame(gr, penGrph[channel], xy1, xy2);
            }


            drawScales();

            if (MonoisotMass != 0)
            {
                drawPrecMassLine();
            }

        }

        private void checkLidMargins(ref Point lid1, ref Point lid2)
        {
            int minPixX = dataToPix(minX, 0).X;
            int maxPixX = dataToPix(maxX, 0).X;
            if (lid1.X < minPixX && !(lid2.X < minPixX)) lid1.X = minPixX;
            if (lid2.X < minPixX && !(lid1.X < minPixX)) lid2.X = minPixX;
            if (lid1.X > maxPixX && !(lid2.X > maxPixX)) lid1.X = maxPixX;
            if (lid2.X > maxPixX && !(lid1.X > maxPixX)) lid2.X = maxPixX;
        }

        public void DrawGraph(int channel)
        {
            DrawGraph(channel, "");
        }

        public void DrawGraph()
        {
            for (int i = 0; i <= dataReal.GetUpperBound(0); i++)
            {
                if (dataReal[i] != null) DrawGraph(i);
            }
        }

        public void drawPrecMassLine()
        {
            
            //check wether monoisotopic mass is in the window or not
            precursorOut.Visible = false;
            if (MonoisotMass < minX || MonoisotMass > maxX)
            {
                precursorOut.Visible = true;
                return;
            }


            if (precursorLineOn)
            {
                // Get the graphics object
                gr = Graphics.FromImage(bm);

                Point xy1 = new Point();
                Point xy2 = new Point();

                Color brushColor = Color.Red;
                Brush myBrush = new SolidBrush(brushColor);
                Pen penGrph = new Pen(myBrush);
                penGrph.Width = 0.4F;

                xy1 = dataToPix(MonoisotMass, minY);
                xy2 = dataToPix(MonoisotMass, minY);
                xy2.Y += 20;

                gr.DrawLine(penGrph, xy1, xy2);

                xy1 = dataToPix(MonoisotMass, maxY);
                xy2 = dataToPix(MonoisotMass, maxY);
                xy2.Y -= 10;

                gr.DrawLine(penGrph, xy1, xy2);
            }
        }

        private enum axis
        {
            X,
            Y
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

             /*
             *  Ticks at maximum and minimum values 
             * 
             
            //X minimum
            drawTick(minX, axis.X, 8);
            //X maximum
            drawTick(maxX, axis.X, 8);
            //Y minimum
            drawTick(minY, axis.Y, 8);
            //Y maximum
            drawTick(maxY, axis.Y, 8);
                    
            */

            //Y maximum
            drawTick(maxY, axis.Y, 8);

            //middle ticks
            double rangeX = maxX - minX;
            double rangeY = maxY - minY;

            double logRangeX = Math.Round(Math.Log10(rangeX), 0);
            double logRangeY = Math.Round(Math.Log10(rangeY), 0);

            int stepTicks = 1;

            ArrayList alTicksX = new ArrayList();
            ArrayList alTicksY = new ArrayList();

            int minValOfTickX =
                (int)Math.Round(minX * Math.Pow(10, -logRangeX + 1), 0);
            int minValOfTickY =
                (int)Math.Round(minY * Math.Pow(10, -logRangeY + 1), 0);
            int maxValOfTickX =
                (int)Math.Round(maxX * Math.Pow(10, -logRangeX + 1), 0);
            int maxValOfTickY =
                (int)Math.Round(maxY * Math.Pow(10, -logRangeY + 1), 0);


            double valForTick = 0;

            for (int i = minValOfTickX ; i <= maxValOfTickX; i++)
            {
                valForTick = i * Math.Pow(10, logRangeX - 1);
                alTicksX.Add(valForTick);
            }

            stepTicks = (int)Math.Round(
                (double)alTicksX.Count / (double)maxNumOfTicksX);
            if (stepTicks == 0) stepTicks = 1;

            for (int step = 0; step < alTicksX.Count - 1; step = step + stepTicks)
            {
                double vft = (double)alTicksX[step];
                drawTick(vft, axis.X, 8);
            }


            for (int i = minValOfTickY + 1; i <= maxValOfTickY; i++)
            {
                valForTick = i * Math.Pow(10, logRangeY - 1);
                alTicksY.Add(valForTick);
            }

            stepTicks = (int)Math.Round(
                (double)alTicksY.Count / (double)maxNumOfTicksY);
            if (stepTicks == 0) stepTicks = 1;

            for (int step = 0; step < alTicksY.Count - 1; step = step + stepTicks)
            {
                double vft = (double)alTicksY[step];
                drawTick(vft, axis.Y, 8);
            }

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
                    fTextPosX = (float)tickPos.X - 15;
                    fTextPosY = (float)tickPos.Y + 7.0F;
                    ptToTick.X = tickPos.X;
                    formatNum = formatNumX;
                    break;
                case axis.Y:
                    tickPos = dataToPix(minX, value);
                    fTextPosX = (float)tickPos.X - 45;
                    fTextPosY = (float)tickPos.Y - 8;
                    ptToTick.Y = tickPos.Y;
                    formatNum = formatNumY;
                    break;
            }



            drawLine(tickPos.X, tickPos.Y, ptToTick.X, ptToTick.Y);
            writeText(fTextPosX,
                        fTextPosY,
                        fontPx,
                        value.ToString(formatNum,
                            CultureInfo.InvariantCulture));


        }
        public void drawLine(int x1,int y1, int x2, int y2) 
        {
            // Get the graphics object
            gr = Graphics.FromImage(bm);
            
            Point xy1 = new Point(x1,y1);
            Point xy2 = new Point(x2,y2);
            Color brushColor = Color.FromArgb(0, 125, 125);
            Brush myBrush = new SolidBrush(brushColor);
            Pen penGraph = new Pen(myBrush);
            penGraph.Width = 1;

            gr.DrawLine(penGraph, xy1, xy2);
        }

        private void writeText(float xPos, float yPos, int fontSize, string text)
        {
            gr = Graphics.FromImage(bm);
            PointF pf = new PointF(xPos, yPos);
            Color brushColor = Color.FromArgb(0, 0, 0);
            Brush myBrush = new SolidBrush(brushColor);

            gr.DrawString(text, new Font("Verdana", fontSize, FontStyle.Regular), myBrush, pf);
        }

        private double pixToData(int pix, axis ax)
        {
            double val = 0;

            switch (ax)
            {
                case axis.X:
                    val = minX + scaleX / pixUsedwidth * (pix - Math.Abs(Width * MarginGraph));
                    break;
                case axis.Y:
                    // __Marco: I had to fix this,
                    // as it gave back wrong data for some values (2009-03-12)
                    double innerVal = 
                       (pix + Math.Abs(Height * MarginGraph) - Height * heightPercent2)
                       * scaleY / pixUsedheight;
                    // next line is a fix for Windows 7
                    int innerValSign = Math.Sign(pix + Math.Abs(Height * MarginGraph) - Height * heightPercent2);
                    val = -(innerVal + minY * innerValSign);
                    break;
            }


            return val;
        }

        protected Point dataToPix(double x, double y)
        {
            Point p = new Point();

            p.Y = (int)Math.Truncate(((-(y - minY) * pixUsedheight / scaleY)
                - Math.Abs(this.Height * MarginGraph))
                    + this.Height * heightPercent2);
            p.X = (int)Math.Truncate(((x - minX) * pixUsedwidth / scaleX)
                + Math.Abs(this.Width * MarginGraph));

            return p;
        }

        protected double XdataToPixDouble(double x)
        {
            return (((x - minX) * pixUsedwidth / scaleX)
                + Math.Abs((double)this.Width * MarginGraph));
        }
 
        protected void scale(int primChannel) 
        {
            
            pixUsedwidth = this.Width * widthPercent;
            pixUsedheight = this.Height * heightPercent;



            //Positioning the channels.
            for (int channel = 0; channel <= dataReal.GetUpperBound(0); channel++)
            {
                if (dataReal[channel] != null)
                {
                    //initialize datapaint
                    dataPaint[channel] =
                        new Comb.mzI[dataReal[channel].GetUpperBound(0)+1];

                    //Get bounds for the channel
                    if (originalMargins && (!horizontalZoom))
                    {
                        maxX = getMaxX(dataReal[channel]);
                        minX = getMinX(dataReal[channel]);
                        maxY = getMaxY(dataReal);
                        minY = getMinY(dataReal);
                    }

                    if (horizontalZoom)
                    {
                        maxY = getMaxY(dataReal[channel]);
                        minY = getMinY(dataReal);
                    }


                    for (int i = 0; i <= dataReal[channel].GetUpperBound(0); i++)
                    {
                        dataPaint[channel][i].I = -(dataReal[channel][i].I - maxY);
                        dataPaint[channel][i].mz = (dataReal[channel][i].mz - minX);
                    }
                }
            }

            //Scaling the channels

            if (originalMargins && (!horizontalZoom))
            {
                maxX = getMaxX(dataReal[primChannel]);
                minX = getMinX(dataReal[primChannel]);
                maxY = getMaxY(dataReal);
                minY = getMinY(dataReal);
            }

            if (horizontalZoom)
            {
                maxY = getMaxY(dataReal);
                minY = getMinY(dataReal);
            }

            scaleX = maxX - minX;
            scaleY = maxY - minY;
            if (scaleY == 0) scaleY += 100;
            if (scaleX == 0) scaleX += 100;

            for (int channel = 0; channel <= dataReal.GetUpperBound(0); channel++)
            {
                if (dataReal[channel] != null)
                {
                    for (int i = 0; i <= dataReal[channel].GetUpperBound(0); i++)
                    {
                        dataPaint[channel][i].I =
                            (-(dataReal[channel][i].I - minY) * pixUsedheight / scaleY)
                            - Math.Abs(this.Height * MarginGraph);
                        dataPaint[channel][i].mz =
                            ((dataReal[channel][i].mz - minX) * pixUsedwidth / scaleX)
                            + Math.Abs(this.Width * MarginGraph);
                    }
                }
            }

        }

        protected double getMaxY(Comb.mzI[][] dta)
        {
            int totDim = dta.Length;
            double max = 0;
            for (int i = 0; i < totDim; i++)
            {
                if (dta[i] != null)
                {
                    double maxProv = getMaxY(dta[i]);
                    if (maxProv > max)
                        max = maxProv;
                }
            }

            // it is not the maximum intensity, but the maximum to draw the spectrum,
            // which is max * maxMargin
            return max;
        }

        protected double getMaxY(Comb.mzI[] dta)
        {
            double mx = 0;

            // __MTH: checks the maximum _between_ minX and MaxX
            // (which should have been calculated previously)
            int lim = dta.GetUpperBound(0);
            int lowerDta = 1;
            int upperDta = lim;


            for (int a = 0; a <= lim; a++)
            {
                if (dta[a].mz >= minX)
                {
                    lowerDta = a;
                    break;
                }
            }

            for (int a = lowerDta + 1; a <= lim; a++)
            {
                if (dta[a].mz > maxX)
                {
                    upperDta = a;
                    break;
                }
            }


            for (int i = lowerDta; i <= upperDta; i++)
            {

                if (dta[i].I >= mx)
                {
                    mx = dta[i].I;
                }
            }

            return mx * maxMargin;
        }

        protected double getMinY(Comb.mzI[][] dta)
        {
            int totDim = dta.Length;
            double min = double.MaxValue;
            for (int i = 0; i < totDim; i++)
            {
                if (dta[i] != null)
                {
                    double minProv = getMinY(dta[i]);
                    if (minProv < min)
                        min = minProv;
                }
            }

            return min;
        }

        protected double getMinY(Comb.mzI[] dta)
        {

            //for centroid mode, the minimum intensity must be zero.
            if (methodTypeUsed ==
                QuiXoT.math.LNquantitate.spectrumType.centroid)
                return 0;


            double min = double.MaxValue;

            int lim = dta.GetUpperBound(0);
            for (int i = 1; i <= lim; i++) 
            {
                if (dta[i].I <= min)
                {
                    min = dta[i].I;
                }
            }
            return min;
        }
        protected double getMaxX(Comb.mzI[] dta)
        {
            double mx = 0;

            int lim = dta.GetUpperBound(0);
            for (int i = 0; i <= lim; i++)
            {

                if (dta[i].mz >= mx)
                {
                    mx = dta[i].mz;
                }
            }

            //Test to give a small margin to X 
            mx = mx * 1.001;

            return mx;        
        }
        protected double getMinX(Comb.mzI[] dta)
        {
            double min = double.MaxValue;

            int lim = dta.GetUpperBound(0);
            for (int i = 0; i <= lim; i++)
            {

                if (dta[i].mz <= min)
                {
                    min = dta[i].mz;
                }
            }

            //Test to give a small margin to X 
            min = min * 0.999;

            return min;
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

        Point originalMousePosition = new Point();
        Point currentMousePosition = new Point();
        Boolean zooming = false;

        private void startZoom(object sender, MouseEventArgs e)
        {
            originalMousePosition = MousePosition;
            currentMousePosition = MousePosition;

            if (dataInsideFrame(screenPixToData(originalMousePosition.X, axis.X), screenPixToData(originalMousePosition.Y, axis.Y)))
            {
                // first rectangle has size = 0
                //Rectangle startRectangle = new Rectangle(originalMousePosition, new Size(new Point(0, 0)));
                //ControlPaint.DrawReversibleFrame(startRectangle, Color.White, FrameStyle.Thick);

                zooming = true;
                //originalMargins = false;
            }
        }

        private void zoomEnd(object sender, MouseEventArgs e)
        {
            if (zooming)
            {

                // the next if is important as when we click without dragging we go back to the original zoom
                bool mouseMoved = false;

                if (Math.Abs(currentMousePosition.X - originalMousePosition.X) > 1 ||
                    Math.Abs(currentMousePosition.Y - originalMousePosition.Y) > 1)
                    mouseMoved = true;


                if (mouseMoved)
                {
                    Rectangle startRectangle = new Rectangle(originalMousePosition, new Size(currentMousePosition.X - originalMousePosition.X, currentMousePosition.Y - originalMousePosition.Y));
                    ControlPaint.DrawReversibleFrame(startRectangle, Color.White, FrameStyle.Thick);

                    // Redraw graph

                    originalMargins = false;
                    this.lblZoom.Visible = true;

                    // different minX and minY must be defiined previously, as the converter will use them to calculate the max
                    double minX2 = Math.Min(screenPixToData(originalMousePosition.X, axis.X), screenPixToData(currentMousePosition.X, axis.X));
                    double minY2 = Math.Min(screenPixToData(originalMousePosition.Y + 1, axis.Y), screenPixToData(currentMousePosition.Y + 1, axis.Y));
                    maxX = Math.Max(screenPixToData(originalMousePosition.X, axis.X), screenPixToData(currentMousePosition.X, axis.X));
                    if (currentMousePosition.Y - originalMousePosition.Y != 1) { maxY = Math.Max(screenPixToData(originalMousePosition.Y, axis.Y), screenPixToData(currentMousePosition.Y, axis.Y)); }

                    minX = minX2;
                    if (currentMousePosition.Y - originalMousePosition.Y != 1)
                    {
                        minY = minY2;
                        
                        // next line is because there is no need to use values of Y under 0 for spectra
                        if (minY < 0) minY = 0;
                        horizontalZoom = false;
                    }
                    else
                    {
                        horizontalZoom = true;
                    }


                    clearGraph();
                    DrawGraph();

                }
                //else
                //{
                //    originalMargins = true;
                //    this.lblZoom.Visible = false;

                //    clearGraph();
                //    DrawGraph();
                //}

                zooming = false;
            }
        }

        private void zoomMove(object sender, MouseEventArgs e)
        {
            if (zooming)
            {
                // deletes the previous rectangle
                Rectangle startRectangle = new Rectangle(originalMousePosition, new Size(-originalMousePosition.X + currentMousePosition.X, -originalMousePosition.Y + currentMousePosition.Y));
                currentMousePosition = MousePosition;

                if (Math.Abs(currentMousePosition.Y - originalMousePosition.Y) < minimumYZoom)
                {
                    currentMousePosition.Y = originalMousePosition.Y + 1;
                }

                //checks if mouse is outside form
                if (currentMousePosition.X > dataToScreenPix(maxX, maxY).X)
                {
                    currentMousePosition.X = dataToScreenPix(maxX, maxY).X;
                }

                if (currentMousePosition.Y < dataToScreenPix(maxX, maxY).Y)
                {
                    currentMousePosition.Y = dataToScreenPix(maxX, maxY).Y;
                }

                if (currentMousePosition.X < dataToScreenPix(minX, minY).X)
                {
                    currentMousePosition.X = dataToScreenPix(minX, minY).X;
                }

                if (currentMousePosition.Y > dataToScreenPix(minX, minY).Y)
                {
                    currentMousePosition.Y = dataToScreenPix(minX, minY).Y;
                }

                // draws new rectangle
                Rectangle endRectangle = new Rectangle(originalMousePosition, new Size(-originalMousePosition.X + currentMousePosition.X, -originalMousePosition.Y + currentMousePosition.Y));

                ControlPaint.DrawReversibleFrame(startRectangle, Color.White, FrameStyle.Thick);
                ControlPaint.DrawReversibleFrame(endRectangle, Color.White, FrameStyle.Thick);

                // Idea to show better when are we zooming in one dimension
                //if (currentMousePosition.Y - originalMousePosition.Y == 1)
                //{
                //    ControlPaint.DrawReversibleLine(new Point(startRectangle.X-10, startRectangle.Y - 10), new Point(startRectangle.X, startRectangle.Y -1), Color.White);
                //    ControlPaint.DrawReversibleLine(new Point(startRectangle.X-10, startRectangle.Y + 1), new Point(startRectangle.X, startRectangle.Y + 10), Color.White);
                //    ControlPaint.DrawReversibleLine(new Point(endRectangle.X+10, endRectangle.Y - 10), new Point(endRectangle.X, endRectangle.Y - 1), Color.White);
                //    ControlPaint.DrawReversibleLine(new Point(endRectangle.X+10, endRectangle.Y + 1), new Point(endRectangle.X, endRectangle.Y + 10), Color.White);
                //}

            }


        }

        void mh_RightClick(object sender, EventArgs e)
        {
            MenuItem zoomOut = new MenuItem("zoom out");
            MenuItem specToCSV = new MenuItem("export to CSV");
            MenuItem showPrec = new MenuItem("show precursor line");

            //miZoom.Checked = this.zoomChecked;

            ContextMenu cmMenu = new ContextMenu();
            if (lblZoom.Visible)
            {
                cmMenu.MenuItems.Add(zoomOut);
                zoomOut.Click += new EventHandler(zoomOut_Click);
            }

            if (!precursorOut.Visible)
            {
                cmMenu.MenuItems.Add(showPrec);
                showPrec.Checked = precursorLineOn;
                showPrec.Click += new EventHandler(showPrec_Click);
            }

            cmMenu.MenuItems.Add(specToCSV);
            specToCSV.Click += new EventHandler(specToCSV_Click);
            this.ContextMenu = cmMenu;
        }

        ///print data to a file

        void showPrec_Click(object sender, EventArgs e)
        {
            if (precursorLineOn) precursorLineOn = false;
            else precursorLineOn = true;

            clearGraph();
            DrawGraph();
        }

        void specToCSV_Click(object sender, EventArgs e)
        {

            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.InitialDirectory = workFolder;
            saveFile.AddExtension = true;
            saveFile.DefaultExt = "csv";
            saveFile.Filter = "CSV Files (*.csv)|*.csv";

            saveFile.ShowDialog(this);

            string file = saveFile.FileName;
                
            try
                {
                    Comb.writeSpectrum(dataReal, file, this.Text);
                 
                }
                catch { }
          
           

            //throw new NotImplementedException();
        }

        void zoomOut_Click(object sender, EventArgs e)
        {
            if (this.lblZoom.Visible)
            {
                this.lblZoom.Visible = false;

                originalMargins = true;
                horizontalZoom = false;

                clearGraph();
                DrawGraph();
            }
        }

        #endregion

        private bool dataInsideFrame(double X,double Y)
        {
            return (X < maxX) && (Y < maxY) && (X > minX) && (Y > minY);
        }

        public void drawLineInsideFrame(Graphics graph,
                                        Pen penGraph,
                                        Point point1,
                                        Point point2)
        {
            // This is just to make sure we are drawing data only inside the frame

            if (dataInsideFrame(pixToData(point1.X,axis.X),
                pixToData(point1.Y,axis.Y))
                || dataInsideFrame(pixToData(point2.X,axis.X),
                pixToData(point2.Y,axis.Y)))
            {
                graph.DrawLine(penGraph, point1, point2);
            }
        }

        private void mouse_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                mh_RightClick(sender, e);
            }

            if (e.Button == MouseButtons.Left)
            {
                startZoom(sender, e);
            }
        }
    }
}