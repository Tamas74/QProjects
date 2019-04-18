﻿using System;
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
    public partial class frmRpt_Viewer : Form
            {

        //#region  " Report Name "

        //public string _ReportName
        //{
        //    get { return _ReportName; }
        //    set { _ReportName = value; }
        //}

        //#endregion  " Report Name  "
        string _ReportName;
       
       
        public frmRpt_Viewer(string sReportName)
        {
            _ReportName = sReportName;

            InitializeComponent();
        }
        #region  "Fill Report Data  "

                private void frmRpt_Viewer_Load(object sender, EventArgs e)
                {
                    ModifyRegistry myRegistry = new ModifyRegistry();
                    try
                    {
                        //Bug #39752: 00000312 : EMR Settings - Hosting Item : Reading and Wrinting a Registry from HKEY_CURRENT_USER
                        if (gloSettings.gloRegistrySetting.IsServerOS)
                        {
                            myRegistry.BaseRegistryKey = Registry.CurrentUser;
                        }
                        else
                        {
                            myRegistry.BaseRegistryKey = Registry.LocalMachine;
                        }
                        myRegistry.SubKey = "SOFTWARE\\" + gloSettings.gloRegistrySetting.gstrSoftPM;
                        //myRegistry.BaseRegistryKey = Registry.LocalMachine;
                        //myRegistry.SubKey = "SOFTWARE\\gloPM";   //\\REPORTPATH
                        // string sPath = myRegistry.Read("REPORTPATH");
                        string sReportserver = myRegistry.Read("RPT_SERVER");
                        string sReportfolder = myRegistry.Read("ReportFolder");
                        //string str = "&ReportFlag=3&EndDate=10-oct-2010&Providers=40205435944020501,40198388234019801";
                        string sPath = "http://" + sReportserver.ToString().Trim() + "/ReportServer?/" + sReportfolder.ToString().Trim() + "/";
                        //wbReportViewer.Navigate(sPath + _ReportName + "&rs:Command=Render&rs:Format=HTML4.0&rc:Toolbar=true");
                        // wbReportViewer.Navigate("http://Dev60/ReportServer?/MiteshTesting/rptAgingDetails.rdl&rs:Command=Render&rs:Format=HTML4.0&rc:Toolbar=true");
                        sReportserver = null;
                        sReportfolder = null;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        ex = null;
                    }
                    finally
                    {
                        myRegistry = null;
                    }
                   
                }

        #endregion


     }
       
}
