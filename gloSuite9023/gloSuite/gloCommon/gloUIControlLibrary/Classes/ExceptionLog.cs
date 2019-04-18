using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace gloUIControlLibrary.Classes.ICD10
{
    class LogException
    {
        public static void ExceptionLog(string strException, bool ShowMessageBox)
        {
            try
            {
                if (Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "\\Log\\ExceptionLog") == false)
                {
                    Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + "\\Log\\ExceptionLog");
                }

                string strLogMessage = Environment.NewLine + "" +
                                       System.DateTime.Now + ":" + System.DateTime.Now.Millisecond + Environment.NewLine +
                                       strException + Environment.NewLine;

                // string _fileName = "ExceptionLog " + DateTime.Now.Date.ToString("MM-dd-yyyy") + ".log";
                string _fileName = Environment.MachineName.ToString() + "-" + DateTime.Now.Date.ToString("yyyyMMdd") + ".log";
                File.AppendAllText(System.AppDomain.CurrentDomain.BaseDirectory + "\\Log\\ExceptionLog\\" + _fileName, strLogMessage);

                if (ShowMessageBox == true)
                {
                    MessageBox.Show(strException, ProductInformation.GetProductName, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch// (Exception ex)
            {
                //ExceptionLog(ex.ToString(), false);
                //Aniket Commented because it was going in recursion
            }
        }       
    }

    class ProductInformation
    {
        public static string GetProductName
        {
            get
            {
                if (System.Windows.Application.ResourceAssembly != null
                    &&
                    System.Windows.Application.ResourceAssembly.FullName != null)
                    {
                        if (System.Windows.Application.ResourceAssembly.FullName.Contains("EMR"))
                        { return "gloEMR"; } 
                        else 
                        { return "gloPM"; }
                    }
                else
                { return ""; }


            }
        }
    }
}
