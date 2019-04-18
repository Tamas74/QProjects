using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Web.Services.Protocols;
using gloSSRSApplication;
using gloSSRSApplication.SSRS;
using System.Xml;
using gloDatabaseLayer;
using Utility.ModifyRegistry;
using Microsoft.Win32;
using System.Collections;
using gloSecurity;
using Project_Reportview;
using gloSettings;



namespace SSRSApplication
{
    public partial class frm_DeployReports : Form
    {
        #region "Declaration"

        private string sReportProtocol = string.Empty;
        private string sReportfolder = string.Empty  ;
        private string sReportserver = string.Empty;
        private string sVirtualDir = string.Empty;

        private string _databaseconnectionstring = "";
        private const string _SqlEncryptionKey = "20gloStreamInc08";
        private const string constEncryptDecryptKey = "12345678";
        private string _sReportType = string.Empty;
        private string DS = string.Empty; 
        private System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private string gstrSQLServerName;
        private string gstrDatabaseName;
        private bool gblnSQLAuthentication;
        private string gstrSQLUser;
        private string gstrSQLPassword;
         
        #endregion

        public frm_DeployReports(string sReporttype, string _gstrSQLServerName,string _gstrDatabaseName,bool  _gblnSQLAuthentication,string _gstrSQLUser,string _gstrSQLPassword)
        {
            InitializeComponent();
            _sReportType = sReporttype;
            _databaseconnectionstring = appSettings["DataBaseConnectionString"].ToString();
            gstrSQLServerName = _gstrSQLServerName;
            gstrDatabaseName = _gstrDatabaseName;
            gblnSQLAuthentication = _gblnSQLAuthentication;
            gstrSQLUser = _gstrSQLUser;
            gstrSQLPassword = _gstrSQLPassword;
            
        }
  //private string _databaseconnectionstring = "";
  // string folderPath = "/gloSuiteSSRS";
 

 #region "File Open Click"

            private void tls_btn_OpenFile_Click(object sender, EventArgs e)
        {
            try
            {

                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Report files (*.rdl)|*.rdl"; //+
                //"All files (*.*)|*.*";
                ofd.FilterIndex = 1;
                ofd.CheckFileExists = true;
                ofd.Multiselect = true;
                if (ofd.ShowDialog(this) == DialogResult.OK)
                {
                   // Create_Datasource("DS", sReportfolder, true);
                    Int16 i = 0;

                    //ArrayList FName = new ArrayList();
                    //ArrayList FPath = new ArrayList();

                    foreach (string file in ofd.FileNames)
                    {   
                        Add_Files(ofd.SafeFileNames[i].ToString(), file);
                        Application.DoEvents();
                       // DeployReport(ofd.SafeFileNames[i].ToString(), file, sReportfolder, true);
                        i++;
                    }

                }
                ofd.Dispose();
                ofd = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString())  ;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }
 #endregion

 #region "Create Report"
     public string DeployReport(string fileName, string filePath, string folderPath, bool overwrite)
        {
            ReportingService2005 rs = new ReportingService2005();
            Byte[] definition = null;
            Warning[] warnings = null;
            CatalogItem[] items = null;
            SearchCondition condition = new SearchCondition();
            SearchCondition[] conditions = null;
            try
            {
                rs.Credentials = System.Net.CredentialCache.DefaultCredentials;

                rs.Url = sReportProtocol + "://" + sReportserver + "/" + sVirtualDir + "/ReportService2005.asmx";



                FileStream stream = File.OpenRead(filePath);
                definition = new Byte[stream.Length];
                stream.Read(definition, 0, (int)stream.Length);
                stream.Close();


                //Search condition ,Duplicate Check
                condition.Condition = ConditionEnum.Equals;
                condition.ConditionSpecified = true;
                condition.Name = "Name";
                condition.Value = fileName;

                conditions = new SearchCondition[1];
                conditions[0] = condition;



                items = rs.FindItems("/" + sReportfolder, BooleanOperatorEnum.Or, conditions);
                bool flgRDL = false;
                if (items != null)
                {
                    foreach (CatalogItem ci in items)
                    {
                        flgRDL = true;
                    }
                }
                if (flgRDL == true)
                {
                    //if (MessageBox.Show("Report is already present. Do you want to overwrite the report? ", _sReportType, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)

                    rs.DeleteItem("/" + sReportfolder + "/" + fileName);
                }


                //--------xx-------



                warnings = rs.CreateReport(fileName, "/" + sReportfolder, overwrite, definition, null);

                if (warnings != null)
                {

                    foreach (Warning warning in warnings)
                    {
                        Console.WriteLine(warning.Message);
                    }

                }
                return "Success";
            }


            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

                if (ex.Message == "Unable to connect to the remote server" || ex.Message == "The request failed with HTTP status 404: ." || ex.Message == "The underlying connection was closed: An unexpected error occurred on a send.")
                {
                    return "Unable to connect to the remote server";
                }
                else
                {
                    return "Error"; //String.Format("Report:Deploy successfully with no warnings", fileName);
                }

            }
            finally
            {
                if (rs != null) { rs.Dispose(); rs = null; }
                definition = null;
                warnings = null;
                items = null;
                condition = null;
                conditions = null;
            }
           
        }
#endregion

 
 #region "Form load"


        private void frm_DeployReports_Load(object sender, EventArgs e)
        {
            
            gloSettings.GeneralSettings oSetting = new gloSettings.GeneralSettings(_databaseconnectionstring);
            object oValue = new object();

            oSetting.GetSetting("ReportProtocol", out oValue);
            if (oValue != null)
            {   sReportProtocol = oValue.ToString();
                oValue = null;
            }

            oSetting.GetSetting("ReportServer", out oValue);
            if (oValue != null)
            {   sReportserver = oValue.ToString();
                oValue = null;
            }
          
            oSetting.GetSetting("ReportFolder", out oValue);
            if (oValue != null)
            {   sReportfolder = oValue.ToString();
                oValue = null;
            }
            
            oSetting.GetSetting("ReportVirtualDirectory", out oValue);
            if (oValue != null)
            {   sVirtualDir = oValue.ToString();
                oValue = null;
            }
            
            if (_sReportType == "gloPM")
            {
                DS = "dsPM";
            }
            else
            {
                DS = "dsEMR";
            }


            ColumnHeader colReportName = new ColumnHeader();
            ColumnHeader colReportStatus = new ColumnHeader();
            ColumnHeader colFilepath = new ColumnHeader();

            colReportName.Text = "Report Name";
            colFilepath.Text = "File Path";
            colReportStatus.Text = "Deploy Status";

            colReportName.Width = 170;
            colFilepath.Width = 270;
            colReportStatus.Width = 159;

            lstReport.Columns.Add(colReportName);
            lstReport.Columns.Add(colFilepath);
            lstReport.Columns.Add(colReportStatus);

            if (oSetting != null) { oSetting.Dispose(); oSetting = null; }
            colReportName = null;
            colReportStatus = null;
            colFilepath = null; 

        }
#endregion

 #region "Close Screen"

 private void tls_btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
 #endregion

 #region "Report Fill"
 private void Add_Files(string sFilename,string sFilepath)
        {
           
            //lstReport.Items.Add(sFilename);

            ListViewItem listItem = new ListViewItem(sFilename);
            //listItem.ImageIndex = 0;
            
            listItem.SubItems.Add(sFilepath);
            listItem.SubItems.Add("-");

            lstReport.Items.Add(listItem);
        }
 #endregion

 #region Deploy Reports
        private void tls_btn_Deploy_Click(object sender, EventArgs e)
        {
            Int16 i;
            Cursor.Current  = Cursors.WaitCursor;

            if (lstReport.Items.Count == 0)
            {
                MessageBox.Show("Select the reports which are to be deployed.", _sReportType, MessageBoxButtons.OK, MessageBoxIcon.Information  );
                return; 
            }
            pnl_pbReport.Visible = true;


            if (sReportProtocol == "" || sReportserver == "" || sReportfolder == "" || sVirtualDir == "")
            {
                Cursor.Current = Cursors.Default ;
                MessageBox.Show("SSRS Settings not set. Set the Report Server settings and then deploy the reports.", _sReportType, MessageBoxButtons.OK, MessageBoxIcon.Warning );
                return;
            }


            gloSSRS.Create_Datasource(DS, _sReportType, _databaseconnectionstring, gstrSQLServerName, gstrDatabaseName, gblnSQLAuthentication, gstrSQLUser, gstrSQLPassword, true);    

            pbReport.Maximum = lstReport.Items.Count;
            for (i = 0; i <= lstReport.Items.Count - 1; i++)
            {
               
                string[] sNewRptName = lstReport.Items[i].SubItems[0].Text.Split('.');  
                Int16 j = 1;
               lstReport.Items[i].SubItems[2].Text = DeployReport(sNewRptName[0], lstReport.Items[i].SubItems[1].Text, sReportfolder, true);

               if (lstReport.Items[i].SubItems[2].Text == "Unable to connect to the remote server")
               {
                   Cursor.Current = Cursors.Default;
                   lstReport.Items[i].ForeColor = System.Drawing.Color.Red;
                   lstReport.Items[i].SubItems[2].Text = "Error";
                   MessageBox.Show("Unable to connect to the report server. Please check report settings.", _sReportType, MessageBoxButtons.OK, MessageBoxIcon.Information);
                   return;
               }
               else if (lstReport.Items[i].SubItems[2].Text == "Error")
               {
                   lstReport.Items[i].ForeColor = System.Drawing.Color.Red;
               }
               else
               {
                   lstReport.Items[i].ForeColor = System.Drawing.Color.Black;
               }

               pbReport.Value =j + i;
               Application.DoEvents();  
            }
            pnl_pbReport.Visible = false;
            Cursor.Current = Cursors.Default;
            MessageBox.Show("Reports deployment process completed.", _sReportType, MessageBoxButtons.OK, MessageBoxIcon.Information );
        }
#endregion

        }
    }

