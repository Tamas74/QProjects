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
    public partial class frmAdminProgress : Form
    {
        public frmAdminProgress()
        {
            InitializeComponent();
        }

        static bool blnTimer = false;
        private void timer1_Tick(object sender, EventArgs e)
        {

            if (blnTimer == false)
            {
                blnTimer = true;
                btnUpdate_Click(sender, null);
                timer1.Stop();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            clsAus objaus = new clsAus();
            pnlUpdateProgress.Visible = true;
            lblStatusUpdate.Text="Updating "+clsAus.strAdminProductType.ToUpper()+"";
            Application.DoEvents();
            clsAus.gloAusAdminLog("Admin update process started");
            try
            {
                string strPathtotemp = Path.GetTempPath();
                string strFullPathtoMSI = string.Empty;
                if (!String.IsNullOrEmpty(strPathtotemp))
                {
                    lblStatusUpdate.Text = "Copying updates to temporary folder";
                    objaus.XcopyData(clsAus.strAdminupdateMSI, strPathtotemp);
                    Application.DoEvents();
                    lblStatusUpdate.Text = "Copied successfully to temp folder";
                }
                else
                {
                    clsAus.gloAusAdminLog("Unable to find the Temp path");
                }
                lblProductType.Visible = true;
                if (clsAus.strAdminProductType.ToLower() == "emradmin")
                {
                   
                    lblProductType.Text = "gloEMRAdmin";
                    Application.DoEvents();
                    strFullPathtoMSI = strPathtotemp + "gloEMRAdmininstall.msi";
                    if (System.IO.File.Exists(strFullPathtoMSI))
                    {
                        lblStatusUpdate.Text = "Updating latest version of gloEMRAdmin";
                        Application.DoEvents();
                        if (objaus.InstallgloEMRAdmin(strFullPathtoMSI, clsAus.strAdminupdatePath))
                        {
                            lblStatusUpdate.Text = "Update of gloEMRAdmin installed successfully";
                        }
                        Application.DoEvents();
                    }
                    else
                    {
                        clsAus.gloAusAdminLog("Unable to find the path to gloEmrAdmin msi");
                    }
                }
                else if (clsAus.strAdminProductType.ToLower() == "pmadmin")
                {
                    strFullPathtoMSI = strPathtotemp + "gloPMAdminInstall.msi";
                    lblProductType.Text = "gloPMAdmin";
                    lblStatusUpdate.Text = "Updating latest version of gloPMAdmin";
                    Application.DoEvents();
                    if (System.IO.File.Exists(strFullPathtoMSI))
                    {
                        if (objaus.InstallgloPMAdmin(strFullPathtoMSI, clsAus.strAdminupdatePath))
                        {
                            lblStatusUpdate.Text = "Update of gloPMAdmin installed successfully";
                            Application.DoEvents();
                        }
                        
                    }
                    else
                    {
                        clsAus.gloAusAdminLog("Unable to find the path to PMAdminmsi");
                    }
                }
                else if (clsAus.strAdminProductType.ToLower() == "cm")
                {
                    strFullPathtoMSI = strPathtotemp + "CMSetup.msi";
                    lblProductType.Text = "ClaimManager";
                    lblStatusUpdate.Text = "Updating latest version of ClaimManager";
                    Application.DoEvents();
                    if (System.IO.File.Exists(strFullPathtoMSI))
                    {
                        if (objaus.InstallClaimManager(strFullPathtoMSI, clsAus.strAdminupdatePath))
                        {
                            lblStatusUpdate.Text = "Update of ClaimManager installed successfully";
                            Application.DoEvents();
                        }
                       
                    }
                    else
                    {
                        clsAus.gloAusAdminLog("Unable to find the path to CM msi");
                    }
                }
            }
            catch (Exception ex)
            {
                clsAus.gloAusAdminLog("Exception in btnUpdate_Click(Admin update) "+ex.Message.ToString()+"");
            }
            finally
            {
                pnlUpdateProgress.Visible = false;
                lblProductType.Visible = false;
                Application.DoEvents();
                objaus = null;
            }
            this.Close();
        }
    }
}
