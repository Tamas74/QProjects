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
    public partial class frm_DBsetting : Form
    {
        public frm_DBsetting()
        {
            InitializeComponent();
        }
        #region "Close Screen"
       
            private void Btn_Cancel_Click(object sender, EventArgs e)
            {
                this.Close();
            }
        #endregion

        #region "Save Report Settings"

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (Txt_ServerName.Text == "")
                    {
                        MessageBox.Show("Please Enter Server Name.");
                        Txt_ServerName.Focus();
                        return; 
                    }
             if (Txt_ReportFolderName.Text == "")
                    {
                        MessageBox.Show("Please Enter Report Folder Name.");
                        Txt_ReportFolderName.Focus();
                        return;
                    }

            ModifyRegistry myRegistry = new ModifyRegistry();

            //Bug #39752: 00000312 : EMR Settings - Hosting Item : Reading and Wrinting a Registry from HKEY_CURRENT_USER
            if (gloSettings.gloRegistrySetting.IsServerOS)
            {
                myRegistry.BaseRegistryKey = Registry.CurrentUser;
            }
            else
            {
                myRegistry.BaseRegistryKey = Registry.LocalMachine;
            }
            myRegistry.SubKey = "SOFTWARE\\" + gloSettings.gloRegistrySetting.gstrSoftPM;  //\\REPORTPATH
           // string sPath = "http://" + Txt_ServerName.Text.ToString().Trim() + "/ReportServer?/" + Txt_ReportFolderName.Text.ToString().Trim() + "/";
            string sReportserver = Txt_ServerName.Text.ToString().Trim();
            string sReportfolder = Txt_ReportFolderName.Text.ToString().Trim();
            myRegistry.Write("REPORTSERVER", sReportserver);
            myRegistry.Write("REPORTFOLDER", sReportfolder);

            myRegistry = null;
            sReportserver = null;
            sReportfolder = null;

            this.Hide();
            MdiMain frm = new MdiMain();
            frm.Show();

        }
        #endregion
    }
}
