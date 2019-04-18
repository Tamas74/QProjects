using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using Utility.ModifyRegistry;
namespace Project_Reportview
{
    public partial class Form1 : Form
    {
        string _ReportName;
        public Form1(string sReportName)
        {
            _ReportName = sReportName;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ModifyRegistry myRegistry = new ModifyRegistry();
            string sReportserver = null;
            string sReportfolder = null;
            string sPath = null;
            try
            {
                myRegistry.BaseRegistryKey = Registry.LocalMachine;
                myRegistry.SubKey = "SOFTWARE\\gloPM";   //\\REPORTPATH
                // string sPath = myRegistry.Read("REPORTPATH");
                sReportserver = myRegistry.Read("RPT_SERVER");
                sReportfolder = myRegistry.Read("ReportFolder");
                //string str = "&ReportFlag=3&EndDate=10-oct-2010&Providers=40205435944020501,40198388234019801";
                sPath = "http://" + sReportserver.ToString().Trim() + "/ReportServer?/" + sReportfolder.ToString().Trim() + "/";
                wbReportViewer.Navigate(sPath + _ReportName + "&rs:Command=Render&rs:Format=HTML4.0&rc:Toolbar=true");
                //wbReportViewer.Navigate("http://Dev60/ReportServer?/MiteshTesting/rptAgingDetails.rdl&rs:Command=Render&rs:Format=HTML4.0&rc:Toolbar=true");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                ex = null;
            }
            finally
            {
                sReportserver = null;
                sReportfolder = null;
                sPath = null;
            }
        }

    
    }
}
