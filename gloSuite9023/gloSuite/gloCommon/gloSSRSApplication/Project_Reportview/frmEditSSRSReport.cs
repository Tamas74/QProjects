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
using gloSSRSApplication.SSRS;
using System.Xml;
using gloDatabaseLayer;
using Utility.ModifyRegistry;
using Microsoft.Win32;
using System.Collections;
using gloSecurity;
using Project_Reportview;
using gloSettings;
using gloSSRSApplication;



namespace Project_Reportview
{
    public partial class frmEditSSRSReport : Form
    {
        #region "Declaration"

        private string sReportProtocol = string.Empty;
        private string sReportfolder = string.Empty;
        private string sReportserver = string.Empty;
        private string sVirtualDir = string.Empty;

        string _sRptID = String.Empty;

        //private string sInstancename = null;
        //private const string _SqlEncryptionKey = "20gloStreamInc08";
        //private const string constEncryptDecryptKey  = "12345678";
        
        private string _databaseconnectionstring = "";
        string _sFileName = String.Empty;
        string _sMode = String.Empty;
        TreeView _trv;
        Int32 _ReportsParentID;
        string _sReportType;
        string _pID = String.Empty;
        public string _ReportName = "";
        string _oldReportName =string.Empty ;
        private string DS =string.Empty ;

        private string gstrSQLServerName;
        private string gstrDatabaseName;
        private bool gblnSQLAuthentication;
        private string gstrSQLUser;
        private string gstrSQLPassword;
        #endregion

        #region "Property"
        public string sReportName
        {
            get { return _ReportName; }
            set { _ReportName = value; }
        }
        #endregion

        public frmEditSSRSReport(string sRptID, string sMode, string databaseconnectionstring, TreeView trv,string sReportType, string _gstrSQLServerName,string _gstrDatabaseName,bool  _gblnSQLAuthentication,string _gstrSQLUser,string _gstrSQLPassword)
        {
            InitializeComponent();
            _trv = trv;
            _sRptID = sRptID;
            _sMode = sMode;
            _sReportType = sReportType;
           // _pID = ParentID; 
            _databaseconnectionstring = databaseconnectionstring;
            gstrSQLServerName = _gstrSQLServerName;
            gstrDatabaseName = _gstrDatabaseName;
            gblnSQLAuthentication = _gblnSQLAuthentication;
            gstrSQLUser = _gstrSQLUser;
            gstrSQLPassword = _gstrSQLPassword;



        }


        #region "Open File Click"
        private void btn_OpenFile_Click(object sender, EventArgs e)
        {
            try
            {

                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Report files (*.rdl)|*.rdl"; //+
                //"All files (*.*)|*.*";
                ofd.FilterIndex = 1;
                ofd.CheckFileExists = true;
                ofd.Multiselect = false;
                if (ofd.ShowDialog(this) == DialogResult.OK)
                {
                    
                    txtFileName.Text = ofd.FileName.ToString();
                    _sFileName = ofd.SafeFileName.ToString(); 
                    

                }
                ofd.Dispose();
                ofd = null;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }
        #endregion

        #region "Clear File textbox"
        private void btn_ClearFile_Click(object sender, EventArgs e)
        {
            txtFileName.Text = string.Empty;
        }
        #endregion

        #region "Form Load"
        private void frmEditSSRSReport_Load(object sender, EventArgs e)
        {
            gloDatabaseLayer.DBLayer ODB = null;
            DataTable dtReport = null;
            gloSettings.GeneralSettings oSetting = new gloSettings.GeneralSettings(_databaseconnectionstring);
            try
            {

                object oValue = new object();

                oSetting.GetSetting("ReportProtocol", out oValue);
                if (oValue != null)
                {
                    sReportProtocol = oValue.ToString();
                    oValue = null;
                }

                oSetting.GetSetting("ReportServer", out oValue);
                if (oValue != null)
                {
                    sReportserver = oValue.ToString();
                    oValue = null;
                }

                oSetting.GetSetting("ReportFolder", out oValue);
                if (oValue != null)
                {
                    sReportfolder = oValue.ToString();
                    oValue = null;
                }

                oSetting.GetSetting("ReportVirtualDirectory", out oValue);
                if (oValue != null)
                {
                    sVirtualDir = oValue.ToString();
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


                if (_sMode == "Edit")
                {
                    chk_Submenu.Enabled = false;
                    btn_OpenFile.Enabled = false;
                    // txtFileName.ReadOnly = true; 
                    ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    ODB.Connect(false);
                    string _strquery = "SELECT   ReportName, ReportFileName,ReportsParentID   FROM   SSRSReports_MST where ReportsID= '" + _sRptID + "' and ReportType= '" + _sReportType + "'";
                    //dtReport = new DataTable();
                    ODB.Retrive_Query(_strquery, out dtReport);
                    if (dtReport != null && dtReport.Rows.Count > 0)
                    {
                        txtFileName.Text = Convert.ToString(dtReport.Rows[0]["ReportFileName"]);
                        _oldReportName = Convert.ToString(dtReport.Rows[0]["ReportName"]);
                        txtReportName.Text = Convert.ToString(dtReport.Rows[0]["ReportName"]);
                        _ReportsParentID = Convert.ToInt32(dtReport.Rows[0]["ReportsParentID"]);
                    }
                    ODB = null;
                    _strquery = null;
                }
                else
                {
                    txtFileName.Text = "";
                    txtReportName.Text = "";

                    chk_Submenu.Enabled = true;
                    btn_OpenFile.Enabled = true;
                    // txtFileName.ReadOnly  = false ; 
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (dtReport != null) { dtReport.Dispose(); dtReport = null; }
                if (oSetting != null) { oSetting.Dispose(); oSetting = null; }
            }

        }
        #endregion

        #region "Save & Close"
        private void tls_btn_SaveCls_Click(object sender, EventArgs e)
        {
            gloDatabaseLayer.DBLayer ODB = null;
            string _pID=string.Empty;
            try
            {
                if (sReportserver == "" || sReportfolder == "" || sVirtualDir == "")
                {
                    MessageBox.Show("SSRS Settings not set. Set the Report Server settings and then deploy the reports.", _sReportType, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_sMode == "Edit")
                {
                    if (txtReportName.Text == "")
                    {
                        MessageBox.Show("Enter report name. ", _sReportType, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtReportName.Focus();
                        return;
                    }

                    ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    ODB.Connect(false);
                    string _sQuery = "Select Count(*) as ReportName  from SSRSReports_MST where ReportName= '" + txtReportName.Text.Replace("'", "''") + "' and ReportsID <> '" + _sRptID + "' and ReportType= '" + _sReportType + "' ";
                    int intCount = (int)ODB.ExecuteScalar_Query(_sQuery);
                    _sQuery = null;

                    if (intCount != 0)
                    {
                        MessageBox.Show("Report name already present. ", _sReportType, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }



                    if (_sFileName == "")
                    {
                        _sFileName = txtFileName.Text;
                    }

                    gloDatabaseLayer.DBLayer objDbLayer = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    gloDatabaseLayer.DBParameters objDbParameters = new gloDatabaseLayer.DBParameters();
                    try
                    {



                        objDbLayer.Connect(false);
                        objDbParameters.Add("@ReportsID", _sRptID, ParameterDirection.Input, SqlDbType.BigInt);
                        objDbParameters.Add("@ReportName", txtReportName.Text, ParameterDirection.Input, SqlDbType.VarChar, 255);
                        objDbParameters.Add("@ReportFileName", _sFileName, ParameterDirection.Input, SqlDbType.VarChar, 255);
                        objDbParameters.Add("@ReportSortOrder", 0, ParameterDirection.Input, SqlDbType.Int);
                        objDbParameters.Add("@ReportsParentID", _ReportsParentID, ParameterDirection.Input, SqlDbType.BigInt);
                        objDbParameters.Add("@IsgloStreamReport", "N", ParameterDirection.Input, SqlDbType.Char, 1);
                        objDbParameters.Add("@ReportType", _sReportType, ParameterDirection.Input, SqlDbType.VarChar, 10);
                        objDbParameters.Add("@Mode", "Edit", ParameterDirection.Input, SqlDbType.VarChar, 10);

                        objDbLayer.Execute("INUP_SSRSReports_MST", objDbParameters);
                        objDbLayer.Disconnect();
                        sReportName = txtReportName.Text;
                        if (_sFileName != "")
                        {
                            EditReport(_oldReportName, txtReportName.Text);
                        }

                    }
                    catch (Exception ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    }
                    finally
                    {
                        if (objDbLayer != null)
                        {
                            objDbLayer.Dispose();
                            ODB = null;
                        }
                        if (objDbParameters != null) { objDbParameters.Dispose(); objDbParameters = null; }
                        
                        this.Close();
                    }

                }
                else
                {
                    if (txtReportName.Text == "")
                    {
                        MessageBox.Show("Enter report name. ", _sReportType, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtReportName.Focus();
                        return;
                    }
                    if (chk_Submenu.Checked != true)
                    {

                        if (txtFileName.Text == "")
                        {
                            MessageBox.Show("Please select file. ", _sReportType, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txtFileName.Focus();
                            return;
                        }
                    }


                    ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    ODB.Connect(false);
                    string _sQuery = "Select Count(*) as ReportName  from SSRSReports_MST where ReportName= '" + txtReportName.Text.Replace("'", "''") + "' and ReportType= '" + _sReportType + "'";
                    int intCount = (int)ODB.ExecuteScalar_Query(_sQuery);
                    _sQuery = null;

                    if (intCount != 0)
                    {
                        MessageBox.Show("Report name already present. ", _sReportType, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    ODB.Connect(false);
                    string _strquery = "Select max(isnull(ReportsID,0)) + 1 as RID from SSRSReports_MST";
                    string sCnt = ODB.ExecuteScalar_Query(_strquery).ToString();
                    _strquery = null;

                    ODB = null;
                    ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    ODB.Connect(false);
                    string _strqry = "SELECT   ReportsParentID,ReportFileName   FROM   SSRSReports_MST where ReportsID= '" + _sRptID + "' and ReportType= '" + _sReportType + "'";
                    DataTable dtReport = null;
                    ODB.Retrive_Query(_strqry, out dtReport);
                    _strqry = null;

                    gloDatabaseLayer.DBLayer objDbLayer = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    gloDatabaseLayer.DBParameters objDbParameters = new gloDatabaseLayer.DBParameters();
                    try
                    {
                        if (Convert.ToString(dtReport.Rows[0]["ReportFileName"]) == "" || (Convert.ToString(dtReport.Rows[0]["ReportsParentID"]) == "0"))
                        {
                            _pID = _sRptID; //Convert.ToString(dtReport.Rows[0]["ReportsParentID"]);
                        }
                        else
                        {
                            _pID = Convert.ToString(dtReport.Rows[0]["ReportsParentID"]); //Convert.ToString(_trv.Parent.Tag);
                        }


                        objDbLayer.Connect(false);
                        objDbParameters.Add("@ReportsID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                        objDbParameters.Add("@ReportName", txtReportName.Text, ParameterDirection.Input, SqlDbType.VarChar, 255);
                        objDbParameters.Add("@ReportFileName", _sFileName, ParameterDirection.Input, SqlDbType.VarChar, 255);
                        objDbParameters.Add("@ReportSortOrder", Convert.ToInt32(sCnt), ParameterDirection.Input, SqlDbType.Int);
                        objDbParameters.Add("@ReportsParentID", _pID, ParameterDirection.Input, SqlDbType.BigInt);
                        objDbParameters.Add("@IsgloStreamReport", "N", ParameterDirection.Input, SqlDbType.Char, 1);
                        objDbParameters.Add("@ReportType", _sReportType, ParameterDirection.Input, SqlDbType.VarChar, 10);
                        objDbParameters.Add("@Mode", "Add", ParameterDirection.Input, SqlDbType.VarChar, 10);


                        if (chk_Submenu.Checked != true)
                        {
                            if (MessageBox.Show("Do you want to deploy the report? ", _sReportType, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                gloSSRS.Create_Datasource(DS, _sReportType, _databaseconnectionstring, gstrSQLServerName, gstrDatabaseName, gblnSQLAuthentication, gstrSQLUser, gstrSQLPassword, true);

                                if (DeployReport(txtReportName.Text, txtFileName.Text, sReportfolder, true) == "Successfully")
                                {
                                    objDbLayer.Execute("INUP_SSRSReports_MST", objDbParameters);
                                    objDbLayer.Disconnect();
                                }
                                else
                                {
                                    MessageBox.Show("Error in report deployment. Please check the SSRS Reporting Service is available or not. ", _sReportType, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }

                            }
                            else
                            {
                                //MessageBox.Show("Error In Report Deployment. ", _sReportType, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                objDbLayer.Execute("INUP_SSRSReports_MST", objDbParameters);
                                objDbLayer.Disconnect();
                            }
                        }
                        else
                        {
                            objDbLayer.Execute("INUP_SSRSReports_MST", objDbParameters);
                            objDbLayer.Disconnect();
                        }



                    }
                    catch (Exception ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    }
                    finally
                    {
                        if (objDbLayer != null)
                        {
                            objDbLayer.Dispose();
                            ODB.Dispose();
                        }
                        if (dtReport != null) { dtReport.Dispose(); dtReport = null; }
                        if (objDbParameters != null) { objDbParameters.Dispose(); objDbParameters = null; }
                        this.Close();
                    }



                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                _pID = null;
            }
        }
        #endregion

        #region "cancel click"
        private void tls_btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region "Submenu Checked Changed"
        private void chk_Submenu_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Submenu.Checked == true)
            {
              txtFileName.Text  = string.Empty;
              _sFileName = string.Empty;
                btn_OpenFile.Enabled = false ;
            }
            else
            {
                //txtFileName.ReadOnly  = true ;
                btn_OpenFile.Enabled = true;
            }

        }
        #endregion

        #region "Create or Edit Report"
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

                rs.Url = sReportProtocol + "://" + sReportserver + "/"+ sVirtualDir + "/ReportService2005.asmx";  

                //Search condition
                condition.Condition = ConditionEnum.Contains;
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
                        //MessageBox.Show("Item {0} found at {1}  " + ci.Name + "--" + ci.Path);
                        flgRDL = true; 
                    }
                }
                if (flgRDL == true)
                {
                   // MessageBox.Show("Report already present. ", Program.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    if (MessageBox.Show("Report is already present. Do you want to overwrite the report? ", _sReportType, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return "Successfully";
                    }

                }


                //--------



                FileStream stream = File.OpenRead(filePath);
                definition = new Byte[stream.Length];
                stream.Read(definition, 0, (int)stream.Length);
                stream.Close();

                warnings = rs.CreateReport(fileName, "/" + sReportfolder, overwrite, definition, null);

            
                if (warnings != null)
                {

                    foreach (Warning warning in warnings)
                    {
                        Console.WriteLine(warning.Message);
                    }

                }
                return "Successfully";
            }


            catch (Exception ex)
            {
                //MessageBox.Show("SSRS Reporting Service is not available.", _sReportType , MessageBoxButtons.OK, MessageBoxIcon.Error);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return "Error"; //String.Format("Report:Deploy successfully with no warnings", fileName);
            }
            finally
            {
                if (rs != null) { rs.Dispose(); rs = null; }
                definition = null;
                warnings = null;
                items = null;
                condition = null;
                conditions = null;
                // return String.Format("Report: {0} created successfully with no warnings", fileName);
            }
        }

        private string EditReport(string sOldName, string sNewName)
        {
            ReportingService2005 rs = new ReportingService2005();
            try
            {
                rs.Credentials = System.Net.CredentialCache.DefaultCredentials;

                rs.Url = sReportProtocol + "://" + sReportserver + "/" + sVirtualDir + "/ReportService2005.asmx";  
              
                                
                rs.MoveItem("/" + sReportfolder + "/" + sOldName, "/" + sReportfolder + "/" + sNewName);
                
               
                return "Successfully";
            }


            catch (Exception ex)
            {
                MessageBox.Show("SSRS Reporting Service is not available.", _sReportType, MessageBoxButtons.OK, MessageBoxIcon.Error);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return "Error"; //String.Format("Report:Deploy successfully with no warnings", fileName);
            }
            finally
            {
                if (rs != null) { rs.Dispose(); rs = null; }
            }
        }

        #endregion

      

    }
}
