using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QuiXoT.statistics
{

    public partial class OPgraph : Form
    {

        //IContainer container;
        ContextMenuStrip cMenuStrip = new ContextMenuStrip();

        Point[] dataReal;
      
        Point MouseSel1;
        Point MouseSel2;

        bool selectEnabled = false;

        public OPgraph()
        {
            InitializeComponent();

            QuiXoT.statistics.Events.MouseHelper mh = new QuiXoT.statistics.Events.MouseHelper(this);
            mh.AddControl(pBox);
            //mh.LeftClick += new EventHandler(OnLeftClick);
            //mh.LeftDoubleClick += new EventHandler(OnLeftDoubleClick);
            //mh.MiddleDoubleClick += new EventHandler(OnMiddleDoubleClick);
            //mh.RightDoubleClick += new EventHandler(OnRightDoubleClick);
            mh.LeftMouseDown += new MouseEventHandler(OnLeftMouseDown);
            mh.LeftMouseUp += new MouseEventHandler(OnLeftMouseUp);
            //mh.MiddleClick += new EventHandler(OnMiddleClick);
            //mh.MiddleMouseDown += new MouseEventHandler(OnMiddleMouseDown);
            //mh.MiddleMouseUp += new MouseEventHandler(OnMiddleMouseUp);
            //mh.RightClick += new EventHandler(OnRightClick);
            //mh.RightMouseDown += new MouseEventHandler(OnRightMouseDown);
            //mh.RightMouseUp += new MouseEventHandler(OnRightMouseUp);
            //mh.WheelBackward += new MouseEventHandler(OnWheelBackward);
            //mh.WheelForward += new MouseEventHandler(OnWheelForward);
            selLbl.Visible = false;
        }

        public void OnLeftMouseDown(object sender, MouseEventArgs e)
        {
            MouseSel1.X = e.X;            
            MouseSel1.Y = e.Y;

            selectEnabled = true;
            selLbl.Visible = true;
            selLbl.Text = selectEnabled.ToString();

        }

        public void OnLeftMouseUp(object sender, MouseEventArgs e)
        {
            MouseSel2.X = e.X;
            MouseSel2.Y = e.Y;

            if (selectEnabled)
            {
                selection(MouseSel1, MouseSel2);
            }

            selectEnabled = false;
            selLbl.Visible = true;
            selLbl.Text = selectEnabled.ToString();
        }

        public void addGraph(Point[] datagraph)
        {
            dataReal = (Point[])datagraph.Clone();
        }

        public void delGraph()
        {
            dataReal = null;           
        }
            

        void pBox_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            MouseSel2.X = e.X;
            MouseSel2.Y = e.Y;

            if (selectEnabled)
            {
                selection(MouseSel1, MouseSel2);
            }
        }

        private void selection(Point xy1,Point xy2)
        {
            Graphics gfx;
            gfx = this.pBox.CreateGraphics();
            gfx.Clear(Color.White);

            Color brushColor = Color.FromArgb(0, 0, 125);
            Brush myBrush = new SolidBrush(brushColor);
            Pen penSel = new Pen(myBrush);

            int rectWidth = (int)Math.Abs(xy2.X - xy1.X);
            int rectHeight = (int)Math.Abs(xy2.Y - xy1.Y);
            
            Rectangle rect = new Rectangle(Math.Min(xy1.X,xy2.X),Math.Min(xy1.Y,xy2.Y), rectWidth, rectHeight);
            gfx.DrawRectangle(penSel, rect);

        }


    }
}