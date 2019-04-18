using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using gloSSRSApplication.SSRS;
using Microsoft.Win32;
using Utility.ModifyRegistry;
using gloSecurity;


namespace Project_Reportview
{
    public partial class MdiMain : Form
    {
        public MdiMain()
        {
            InitializeComponent();
        }
       private string sReportfolder = null;
       private string sReportserver = null;
       private string sInstancename = null;
       private const string _SqlEncryptionKey = "20gloStreamInc08";
        #region "Menu Item Click Event"           

            private void MenuItemClickHandler(object sender, EventArgs e)
            {
                ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
               // MessageBox.Show("dfdd"); 
                // = clickedItem.Name;
                
                frmRpt_Viewer childForm = new frmRpt_Viewer(clickedItem.Name.ToString());   
                //Project_Reportview.Form1 childForm = new Project_Reportview.Form1(clickedItem.Name.ToString());   
                childForm.MdiParent = this;          
                childForm.Show() ;
            }
        #endregion

        #region "Form Load Event"
        private void MdiMain_Load(object sender, EventArgs e)
                {
                    //get_reports();
                    //ModifyRegistry myRegistry = new ModifyRegistry();
                    //myRegistry.BaseRegistryKey = Registry.CurrentUser;
                    //myRegistry.SubKey = "SOFTWARE\\gloPM\\ReportPath";
                    //myRegistry.Write("ReportPath", "http://DEV24/ReportServer?/gloSuiteReports/");

                 

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
                    myRegistry.SubKey = "SOFTWARE\\" + gloSettings.gloRegistrySetting.gstrSoftPM;
                    //myRegistry.BaseRegistryKey = Registry.LocalMachine;
                    //myRegistry.SubKey = "SOFTWARE\\gloPM";   //\\REPORTPATH
                    sReportfolder = myRegistry.Read("ReportFolder");
                    sReportserver = myRegistry.Read("RPT_SERVER");
                    sInstancename = myRegistry.Read("RPT_InstanceName").ToString();
                    Create_Datasource("DS", sReportfolder, true);
                    if (sReportfolder == null || sReportserver == null)
                    {
                        frm_DBsetting frm = new frm_DBsetting();
                        frm.ShowDialog(this);
                        frm.Dispose();
                        frm = null;
                        return; 
                    }
                    
                   mnuFill();

                   myRegistry = null;

                }
        #endregion

        #region "Create Datasource"

        private void Create_Datasource(string fileName, string folderPath, bool overwrite)
        {
            ReportingService2005 rs = new ReportingService2005();
            DataSourceDefinition Def = new DataSourceDefinition();
            //DataSourceReference reference = new DataSourceReference();
            ModifyRegistry myRegistry = new ModifyRegistry();
            ClsEncryption oEncrypt = new ClsEncryption();
            string sPassword = null;
            try
            {

                rs.Credentials = System.Net.CredentialCache.DefaultCredentials;

                rs.Url = "http://" + sReportserver + "/ReportServer/ReportService2005.asmx"; //"http://DEV54/ReportServer/ReportService2005.asmx";


                Def.CredentialRetrieval = CredentialRetrievalEnum.Store;

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
                //myRegistry.SubKey = "SOFTWARE\\gloPM";

                // Def.ConnectString = "Data Source=" + sReportserver + ";Initial Catalog=" + myRegistry.Read("RPT_Database");
                Def.ConnectString = "Data Source=" + sInstancename + ";Initial Catalog=" + myRegistry.Read("RPT_Database");
                //Def.ConnectString = "Data Source=Dev24;Password=sadev24;Persist Security Info=True;User ID=sa;Initial Catalog=5031Reports;";
                Def.UserName = myRegistry.Read("RPT_SQLUSERNAME");
                sPassword = oEncrypt.DecryptFromBase64String(myRegistry.Read("RPT_SQLPASSWORD"), _SqlEncryptionKey);

                Def.Password = sPassword;
                Def.Extension = "SQL";
                Def.WindowsCredentials = false;

                rs.CreateDataSource(fileName, folderPath, true, Def, null);

            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
            finally
            {
                if (rs != null) { rs.Dispose(); rs = null; }
                if (oEncrypt != null) { oEncrypt.Dispose(); oEncrypt = null; }
                Def = null;
                myRegistry = null;
                sPassword = null;
            }
        }

        #endregion

        #region  " Fill Menu Strip "

        private void mnuFill()
        {


            ReportingService2005 rs = new ReportingService2005();
            CatalogItem[] itemReports = null;
            string lnk = null;
            string sFolder = null;
            ToolStripMenuItem Toolitems = default(ToolStripMenuItem);
            //CatalogItem itm = default(CatalogItem);
            //System.Net.ICredentials cred = null;
            try
            {
                rs.Credentials = System.Net.CredentialCache.DefaultCredentials;
                // rs.Url =  "http://DEV24/ReportServer/ReportService2005.asmx";
                lnk = "http://" + sReportserver + "/reportserver/ReportService2005.asmx";
                rs.Url = lnk;

                sFolder = "/" + sReportfolder;
                itemReports = rs.ListChildren(sFolder, true);


                if (itemReports.Count() == 0)
                {
                    MessageBox.Show("No Reports Found.");
                    return;
                }

                //ToolStripMenuItem oCurrentMenu = (ToolStripMenuItem)sender;
                foreach (CatalogItem itms in itemReports)  //(int i = 0; i < Toolitems.Length; i++)
                {
                    if (itms.Type.ToString() == "Report")
                    {
                        Toolitems = new ToolStripMenuItem();
                        string[] sReportName = itms.Name.ToString().Substring(3, itms.Name.ToString().Length - 3).Split('.'); //SplitReportName();  //itms.Name.ToString()

                        Toolitems.Name = itms.Name.ToString(); //SplitReportName(sReportName);
                        Toolitems.Tag = itms.Name.ToString();
                        Toolitems.Text = SplitReportName(sReportName[0]);
                        Toolitems.Click += new EventHandler(MenuItemClickHandler);
                        mnugloReporting.DropDownItems.Add(Toolitems);
                        sReportName = null;
                    }
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
            finally
            {
                if (rs != null) { rs.Dispose(); rs = null; }
                if (Toolitems != null) { Toolitems.Dispose(); Toolitems = null; }
                itemReports = null;
                lnk = null;
                sFolder = null;
            }

        }

        
        #endregion

        #region "Convert Report Name"
        public string SplitReportName(string sInput)    //BreakUpperCB
        {
            StringBuilder[] sReturn = new StringBuilder[1];
            const string CUPPER = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int iArrayCount = 0;
            string strReturn = "";
            try
            {
                sReturn[0] = new StringBuilder(sInput.Length);
                for (int iIndex = 0; iIndex < sInput.Length; iIndex++)
                {
                    string sChar = sInput.Substring(iIndex, 1); // get a char
                    if ((CUPPER.Contains(sChar)) && (iIndex > 0))
                    {
                        iArrayCount++;
                        System.Text.StringBuilder[] sTemp = new System.Text.StringBuilder[iArrayCount + 1];
                        Array.Copy(sReturn, 0, sTemp, 0, iArrayCount);
                        sTemp[iArrayCount] = new StringBuilder(sInput.Length);
                        sReturn = sTemp;
                    }
                    sReturn[iArrayCount].Append(sChar);
                }
                //string[] sReturnString = new string[iArrayCount + 1];
                //for (int iIndex = 0; iIndex < sReturn.Length; iIndex++)
                //{
                //    sReturnString[iIndex] = sReturn[iIndex].ToString();
                //}
                for (int iIndex = 0; iIndex < sReturn.Length; iIndex++)
                {
                    // sReturnString[iIndex] = sReturn[iIndex].ToString();
                    strReturn = strReturn + " " + sReturn[iIndex].ToString();
                }
                // return sReturnString;
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
            finally
            {
                sReturn = null;
            }
            return strReturn;
        }
        #endregion

        #region "Form Close"
        private   void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {

                if (MessageBox.Show("Are you sure you want to close the application?", Program.gMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        e.Cancel = true;
                        
                    }
                    else
                    {
                        //Application.Exit();
                        System.Environment.Exit(0);
                     
                    }
                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }

        }
        #endregion

    }
}
