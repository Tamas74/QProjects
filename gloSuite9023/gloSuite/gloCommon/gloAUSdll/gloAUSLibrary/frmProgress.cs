using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace gloAUSLibrary
{
    public partial class frmProgress : Form
    {

        public string strConnectionString = null;

        public string strLocalAppName = null;
        public frmProgress(string strConn, string strAppName)
        {
            InitializeComponent();

            strConnectionString = strConn;
            strLocalAppName = strAppName;
        } //frmProgress
    
        static bool blnTimer = false;

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (blnTimer == false)
            {
                blnTimer = true;
                btnUpdate_Click(sender, null);
                timer1.Stop();
            }
        } //timer1_Tick

        public void DeleteDirectory(string location)
        {
            try
            {
                string strExelocation = location + "\\glosuiteclient.exe";
                if (File.Exists(strExelocation))
                {
                    File.Delete(strExelocation);
                }
            }
            catch (Exception ex)
            {
                clsAus.gloAusLog("Exception in DeleteDirectory " + ex.Message.ToString() + "");
            }
        } //DeleteDirectory

        private void btnUpdate_Click(object sender, EventArgs e)    
        {
            clsAus objaus = null;
            string strPathtoInstaller = string.Empty;
            string strPathtotemp = Path.GetTempPath();
            string strDestination = string.Empty;
            clsInstallUpdates objInstallUpdate = null;
            DataTable dtAvailableUpdate = null;
            try
            {
                objInstallUpdate = new clsInstallUpdates(strConnectionString);
                objaus = new clsAus();
             
                clsAus.gloAusLog("Came btnUpdate_Click");
                //clsAus.gloAusLog("ConnectionString : " + strConnectionString);

                dtAvailableUpdate = objInstallUpdate.GetAvailableUpdateList();

                if (dtAvailableUpdate != null)
                {
                    if (dtAvailableUpdate.Rows.Count > 0)
                    {
                        clsAus.gloAusLog("Client Update process started");
                        pnlUpdateProgress.Visible = true;
                        clsAus.gloAusLog("Application Name:" + strLocalAppName);
                        objInstallUpdate.InstallUpdates(dtAvailableUpdate, strLocalAppName);
                    }
                    else
                    {
                        clsAus.gloAusLog("No client update available");
                    }
                }
                else
                {
                    clsAus.gloAusLog("No client update available");
                }

                pnlUpdateProgress.Visible = false;
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                clsAus.gloAusLog("Exception in frmProgress_Load " + ex.Message.ToString() + "");
            }
            finally
            {
                pnlUpdateProgress.Visible = false;
                Application.DoEvents();
                objInstallUpdate.Dispose();
                objInstallUpdate = null;
                objaus = null;
            }
            this.Close();
        } //btnUpdate_Click

    }
}
