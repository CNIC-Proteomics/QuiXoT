using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace QuiXoT
{
    public partial class OPImage : Form
    {
        Timer Clock;
        FileInfo[] jpgFiles;
        string mgFolder;
        Random rnd;

        public OPImage()
        {
            InitializeComponent();

            Clock = new Timer();
            Clock.Interval = 3000;
            Clock.Start();
           
            mgFolder = QuiXoT.Properties.Settings.Default.imagesFolder;

            DirectoryInfo dir = new DirectoryInfo(@mgFolder);

            jpgFiles = dir.GetFiles("*.jpg");
            rnd = new Random();

            if (jpgFiles.Length > 0)
            {
                Clock.Tick += new EventHandler(Timer_Tick);
            }


           
        }


        public void Timer_Tick(object sender, EventArgs eArgs)
        {
            if (sender == Clock)
            {
               
                                
                this.Text = mgFolder + jpgFiles[rnd.Next(0, jpgFiles.Length)].Name;
                //pbxImg.Image = Image.FromFile(strImgName);
                //SizeF size = new SizeF(this.Width*0.3F, this.Height*0.3F);
                 
                //pictureBox1.Scale(size);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.Image = Image.FromFile(mgFolder + jpgFiles[rnd.Next(0, jpgFiles.Length)].Name);
                
                
                  //lbTime.Text = GetTime();
            }
        }
    
    }
}