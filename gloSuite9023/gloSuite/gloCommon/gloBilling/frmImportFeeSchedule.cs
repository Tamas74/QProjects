using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace gloBilling
{
    public partial class frmImportFeeSchedule : Form
    {

        #region " Declarations "

        private string _databaseconnectionstring = "";
        private string _messageBoxCaption = String.Empty;

        private string _DatabaseName = "";
        private string _SQLServerName = "";
        private string _SQLLoginName = "";
        private string _SQLPassword = "";
        private bool _IsWindowAuthentication = false;  

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;

        #endregion " Declarations "

        #region "Contructor"

        public frmImportFeeSchedule(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageBoxCaption = "gloPM";
                }
            }
            else
            { _messageBoxCaption = "gloPM"; }

            #endregion


            if (appSettings["SQLServerName"] != null && Convert.ToString(appSettings["SQLServerName"]) != "")
            {
                _SQLServerName = Convert.ToString(appSettings["SQLServerName"]);
            }

            if (appSettings["DatabaseName"] != null && Convert.ToString(appSettings["DatabaseName"]) != "")
            {
                _DatabaseName = Convert.ToString(appSettings["DatabaseName"]);
            }

            if (appSettings["SQLLoginName"] != null && Convert.ToString(appSettings["SQLLoginName"]) != "")
            {
                _SQLLoginName = Convert.ToString(appSettings["SQLLoginName"]);
            }
            if (appSettings["SQLPassword"] != null && Convert.ToString(appSettings["SQLPassword"]) != "")
            {
                _SQLPassword = Convert.ToString(appSettings["SQLPassword"]);
            }
            if (appSettings["WindowAuthentication"] != null && Convert.ToString(appSettings["WindowAuthentication"]) != "")
            {
                _IsWindowAuthentication = Convert.ToBoolean(appSettings["WindowAuthentication"]);
            }

            InitializeComponent();
        } 

        #endregion

        private void frmImportFeeSchedule_Load(object sender, EventArgs e)
        {

        }

        #region "Button Click Events"

        private void btn_Browse_Click(object sender, EventArgs e)
        {
            try
            {
                dlgBrowseFile.Title = " Browse File ";
                dlgBrowseFile.Filter = "Excel Files(*.csv)|*.csv";
                dlgBrowseFile.CheckFileExists = true;
                dlgBrowseFile.Multiselect = false;
                dlgBrowseFile.ShowHelp = false;
                dlgBrowseFile.ShowReadOnly = false;

                if (dlgBrowseFile.ShowDialog(this) == DialogResult.OK)
                {

                    txtImportFile.Text = dlgBrowseFile.FileName;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void tsb_Import_Click(object sender, EventArgs e)
        {
            bool _Result = false; 
            try
            {
                if (ValidateData() == true)
                {
                    this.Cursor = Cursors.WaitCursor;
                    Int64 nFeeScheduleID = 0;
                    nFeeScheduleID = GetFeeScheduleID();
                    
                    string sFormatedCSVFileName = FormatCSVFile(txtImportFile.Text.Trim(), nFeeScheduleID);

                    if (sFormatedCSVFileName.Trim() != "")
                    {
                        string sBatchFilePath = CreateBatchFile(sFormatedCSVFileName);

                        if (sBatchFilePath.Trim() != "")
                        {
                            System.Diagnostics.Process proc = new System.Diagnostics.Process();
                            proc.EnableRaisingEvents = false;
                            proc.StartInfo.FileName = sBatchFilePath;
                            proc.Start();
                            proc.WaitForExit();
                            if (proc.HasExited == true)
                            {
                                if (proc.ExitCode == 0)
                                {
                                    SaveFeeSchedule(nFeeScheduleID);
                                    _Result = true; 
                                }
                                else
                                {
                                    this.Cursor = Cursors.Default;
                                    MessageBox.Show("Error while inserting data.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }

                            if (File.Exists(sBatchFilePath) == true)
                            {
                                File.Delete(sBatchFilePath);
                            }
                        }
                        else
                        {
                            this.Cursor = Cursors.Default;
                            MessageBox.Show("Error while creating batch file.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);      
                        }


                        if (File.Exists(sFormatedCSVFileName) == true)
                        {
                            File.Delete(sFormatedCSVFileName);
                        }
                    }
                    else
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Error while reading source file.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);      
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

            if (_Result == true)
            {
                this.Close();
            }
        }

        private void tsb_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        private bool ValidateData()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                if (txtFeeScheduleName.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter fee schedule name.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtFeeScheduleName.Focus();  
                    return false;
                }
                if (txtImportFile.Text.Trim() == "")
                {
                    MessageBox.Show("Please select file to import.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btn_Browse.Focus();  
                    return false;
                }

                if (File.Exists(txtImportFile.Text.Trim()) == false)
                {
                    MessageBox.Show("Source file does not exists.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btn_Browse.Focus();
                    return false;
                }

                oDB.Connect(false);   
                string _sqlQuery = " SELECT Count(*) " +
                            " FROM   BL_FeeSchedule_MST " +
                            " WHERE  (sFeeScheduleName = '" + txtFeeScheduleName.Text.Trim() + "') AND (nClinicID = " + _ClinicID + ")";
                object _Name = oDB.ExecuteScalar_Query(_sqlQuery);
                oDB.Disconnect();
  
                if (Convert.ToInt64(_Name) > 0)
                {
                    MessageBox.Show("Fee schedule name is already exists.  Please select a unique name.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            catch (gloDatabaseLayer.DBException odbEx)
            {
                odbEx.ERROR_Log(odbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Dispose();
            }
            return true; 

        }

        private string FormatCSVFile(string sFileName, Int64 nFeeScheduleID)
        {
            string _result = "";

            try
            {

                string sNewFileName =  sFileName.ToUpper().Replace(".CSV", "_New.CSV");
                StreamReader oReader = new StreamReader(sFileName);
                if (File.Exists(sNewFileName))
                {
                    File.Delete(sNewFileName);
                }
                StreamWriter oWriter = new StreamWriter(sNewFileName);
                string sLineToWrite = "";
                string sSourceLine = "";

                while (oReader.EndOfStream != true)
                {
                    sSourceLine = oReader.ReadLine();

                    // Line Format 
                    //--------------------- 
                    // nFeeScheduleID, + SourceFileLine + sSpecialtyID,nClinicCharges,nLimitCharges,nAllowedCharges,nClinicID,nChargePercentage,nVariantAmount   
                    // SourceFileLine =  (sYear,sCarrierNumber,sLocality,sHCPCS,sModifier,nNonFacilityFeeScheduleAmount,nFacilityFeeScheduleAmount,nPCTCIndicator,sStatusCode)
                    //----------------------

                    sLineToWrite = nFeeScheduleID + "," + sSourceLine.Replace("\"", "") + ",0,0,0,0," + _ClinicID + "," + Convert.ToInt32(numChargePercentage.Value) + ",0";
                    oWriter.WriteLine(sLineToWrite.ToCharArray());
                }
                oReader.Close();
                oWriter.Close();

                _result = sNewFileName;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {

            }
            return _result;
        }

        private string CreateBatchFile(string sSourceFileName)
        {
            string _result = "";
            try
            {

                if (File.Exists(sSourceFileName) == false)
                {
                    return _result; 
                }

                string sBatchFileName = sSourceFileName.ToUpper().Replace(".CSV", ".bat");
                string sBCPUtilityPath = "C:\\Program Files\\Microsoft SQL Server\\90\\Tools\\Binn\\bcp.exe";

                if (File.Exists(sBatchFileName) == true)
                {
                    File.Delete(sBatchFileName);
                }

                if (File.Exists(sBCPUtilityPath) == false)
                {
                    MessageBox.Show("Please select BCP utility path.  ",_messageBoxCaption,MessageBoxButtons.OK,MessageBoxIcon.Information);  

                    dlgBrowseFile.Title = " Browse File ";
                    dlgBrowseFile.Filter = "(*.exe)|*.exe";
                    dlgBrowseFile.CheckFileExists = true;
                    dlgBrowseFile.Multiselect = false;
                    dlgBrowseFile.ShowHelp = false;
                    dlgBrowseFile.ShowReadOnly = false;

                    if (dlgBrowseFile.ShowDialog(this) == DialogResult.OK)
                    {

                        sBCPUtilityPath = dlgBrowseFile.FileName;
                    }
                    else
                    {
                        return _result; 
                    }
                }

                sBCPUtilityPath = sBCPUtilityPath.ToUpper().Replace(".EXE", "");   

                StreamWriter oWriter = new StreamWriter(sBatchFileName);
                string sLineToWrite = "";

                // Batch command i.e
                //-------------------------------------------------------------------
                // (SQL Authentication)
                // "C:\Program Files\Microsoft SQL Server\90\Tools\Binn\bcp" ImportTesting.dbo.BL_FeeSchedule_DTL in "E:\BCP\PFALL09A_New.csv" -c -C RAW -t "," -r "\n" -U sa -P sadev13 -S dev13 -a 8192

                // (Win Authentication)
                // "C:\Program Files\Microsoft SQL Server\90\Tools\Binn\bcp" ImportTesting.dbo.BL_FeeSchedule_DTL in "E:\BCP\PFALL09A_New.csv" -c -C RAW -t "," -r "\n" -S dev13 -T -a 8192
                //-------------------------------------------------------------------


                if(_IsWindowAuthentication == false)
                    sLineToWrite = " \"" + sBCPUtilityPath + "\" " + _DatabaseName + ".dbo.BL_FeeSchedule_DTL in \"" + sSourceFileName + "\" -c -C RAW -t \",\" -r \"\\n\"  -U " + _SQLLoginName + " -P " + _SQLPassword + " -S " + _SQLServerName + " -a 8192 ";   
                else
                    sLineToWrite = " \"" + sBCPUtilityPath + "\" " + _DatabaseName + ".dbo.BL_FeeSchedule_DTL in \"" + sSourceFileName + "\" -c -C RAW -t \",\" -r \"\\n\"  -S " + _SQLServerName + " -T -a 8192 ";   

                oWriter.WriteLine(sLineToWrite.ToCharArray());
                oWriter.Close();

                _result = sBatchFileName;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            return _result; 
        }

        private long GetFeeScheduleID()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            
            try
            {
                oDB.Connect(false);
                string _sqlQuery = " SELECT ISNULL(MAX(nFeeScheduleID),0) + 20 FROM BL_FeeSchedule_MST";
                object _retID = oDB.ExecuteScalar_Query(_sqlQuery);
                oDB.Disconnect();  


                if (_retID != null && Convert.ToString(_retID) != "")
                    return Convert.ToInt64(_retID);
            }
            catch (gloDatabaseLayer.DBException odbEx)
            {
                odbEx.ERROR_Log(odbEx.ToString());    
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Dispose();  
            }
            return 0;
        }

        private void SaveFeeSchedule(long nFeeScheduleID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);

            try
            {
                oDB.Connect(false);

                string _sqlQuery = "INSERT INTO BL_FeeSchedule_MST (nFeeScheduleID, nFeeScheduleType, sFeeScheduleName, nClinicID) " +
                                    " VALUES (" + nFeeScheduleID + ", 0, '" + txtFeeScheduleName.Text.Trim() + "', " + _ClinicID + ")";
                oDB.Execute_Query(_sqlQuery);

                oDB.Disconnect();

            }
            catch (gloDatabaseLayer.DBException odbEx)
            {
                odbEx.ERROR_Log(odbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Dispose();
            }
        }

       
    }
}