using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SampleLibrary
{
    
        static class Program
        {
            static public string Dir = "";

            /// <summary>
            /// The main entry point for the application.
            /// </summary>
            [STAThread]
            static void Main()
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new DataBaseInfo());
            }
        }
    
}
