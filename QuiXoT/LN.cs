using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace QuiXoT
{



    static class LN
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
                        
            Application.Run(new sleeve());
                Application.Run(new OPquan());
            
            Application.Exit();
                      
        }

    }
}