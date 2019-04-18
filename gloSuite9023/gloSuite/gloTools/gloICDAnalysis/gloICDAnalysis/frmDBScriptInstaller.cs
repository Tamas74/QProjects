using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Diagnostics;
using gloICDAnalysis.ClassLib;


namespace gloICDAnalysis
{
    public partial class frmDBScriptInstaller : Form
    {
        BackgroundWorker worker;

        //public DBSetting SelectedDB { get; set; }
        private Int32 SelectedDBVersion { get; set; }

        public frmDBScriptInstaller(Int32 _dbVersion)
        {
            SelectedDBVersion = _dbVersion;

            InitializeComponent();
        }

        private void frmInstallDBChanges_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                lbLog.Items.Clear(); 
                worker = new System.ComponentModel.BackgroundWorker();
                worker.WorkerReportsProgress = true;
                worker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
                worker.DoWork += new System.ComponentModel.DoWorkEventHandler(worker_DoWork);
                worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
                worker.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                clsICDAnalysis.UpdateLog(ex.ToString(), true);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void worker_ProgressChanged(object sender,ProgressChangedEventArgs e)
        {          
            lblProcess.Text = "Status : " + e.UserState;
            pbStatus.Value = e.ProgressPercentage;
            lbLog.Items.Add(e.UserState);            
        }

        void worker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            ExecuteDBUpdates();
        }

        void worker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }

        public bool ExecuteDBUpdates()
        {
            bool _result = false;
            string strICD10TableScriptPath = null;
            string strICDCPTGalleryImportScriptPath = null;
            string strTableScriptLog = Application.StartupPath + "\\InstallationLogs\\ICD10CreateTableScript";
            string strICDCPTGalleryImportScriptLog = Application.StartupPath + "\\InstallationLogs\\ICDCPTGalleryImportScriptLog";
            string strICD10OrderCodeFilePath = null;
            string strICD10GemFilePath = null;
            string strICD10UtilityScriptFilePath = null;
            string strICD10UtilityScriptFilePath_8010 = null;
            string _connstring = "Server=" + clsDBSettings.CurrentDBInfo.SqlServerName + ";Database=" + clsDBSettings.CurrentDBInfo.DatabaseName + ";Uid=" + clsDBSettings.CurrentDBInfo.SqlUserName + ";Pwd=" + clsDBSettings.CurrentDBInfo.SqlPassword + ";";
 
            worker.ReportProgress(10, "Started Script Execution...");

            try
            {
                strICD10TableScriptPath = Application.StartupPath + @"\DBScripts\ICD10CreateTable.sql";
                strICDCPTGalleryImportScriptPath = Application.StartupPath + @"\DBScripts\ICD10GalleryImport.sql";
                strICD10OrderCodeFilePath = Application.StartupPath + @"\CodeFiles\icd10cm_order.txt";
                strICD10GemFilePath = Application.StartupPath + @"\CodeFiles\ICD10_GEM_File.txt";
                strICD10UtilityScriptFilePath = Application.StartupPath + @"\DBScripts\gloICDUtilitySPs.sql";
                strICD10UtilityScriptFilePath_8010 = Application.StartupPath + @"\DBScripts\gloICDUtilitySPs_8010.sql";

                clsInstallDBScripts.gloServerLog("Stated executing script to create Icd 10 table.");
                if (clsInstallDBScripts.ScriptsNonReplication(strICD10TableScriptPath, "ICD10CreateTable_" + clsDBSettings.CurrentDBInfo.DatabaseName, clsDBSettings.CurrentDBInfo.SqlServerName, clsDBSettings.CurrentDBInfo.DatabaseName, clsDBSettings.CurrentDBInfo.SqlUserName, clsDBSettings.CurrentDBInfo.SqlPassword, clsDBSettings.CurrentDBInfo.IsWindowsAuthentication.ToString(), strTableScriptLog))
                {
                    clsInstallDBScripts.gloServerLog("ICD 10 table created successfully.");
                    worker.ReportProgress(20, "ICD 10 table created successfully.");

                    if (clsInstallDBScripts.ImportTextFileData("ICD10CM_order", strICD10OrderCodeFilePath, _connstring) == true)
                    {
                        clsInstallDBScripts.gloServerLog("Completed Import Icd 10 Order File data successfully "); 
                        worker.ReportProgress(40, "Completed Import Icd 10 Order File data successfully ");
                        
                        if (clsInstallDBScripts.ImportTextFileData("ICD10_GEM_File", strICD10GemFilePath, _connstring) == true)
                        {
                            clsInstallDBScripts.gloServerLog("Completed Import Icd 10 GEM File data successfully ");
                            worker.ReportProgress(60, "Completed Import Icd 10 GEM File data successfully ");

                            if (clsInstallDBScripts.ScriptsNonReplication(strICDCPTGalleryImportScriptPath, "ICD10GalleryImport_" + clsDBSettings.CurrentDBInfo.DatabaseName, clsDBSettings.CurrentDBInfo.SqlServerName, clsDBSettings.CurrentDBInfo.DatabaseName, clsDBSettings.CurrentDBInfo.SqlUserName, clsDBSettings.CurrentDBInfo.SqlPassword, clsDBSettings.CurrentDBInfo.IsWindowsAuthentication.ToString(), strICDCPTGalleryImportScriptLog))
                            {
                                clsInstallDBScripts.gloServerLog("Updated Icd 10 codes to ICD9Gallery Table");
                                worker.ReportProgress(70, "Updated Icd 10 codes to ICD9Gallery Table");

                                if (clsInstallDBScripts.ScriptsNonReplication(strICD10UtilityScriptFilePath, "ICDUtilitySPs" + clsDBSettings.CurrentDBInfo.DatabaseName, clsDBSettings.CurrentDBInfo.SqlServerName, clsDBSettings.CurrentDBInfo.DatabaseName, clsDBSettings.CurrentDBInfo.SqlUserName, clsDBSettings.CurrentDBInfo.SqlPassword, clsDBSettings.CurrentDBInfo.IsWindowsAuthentication.ToString(), strICDCPTGalleryImportScriptLog))
                                {
                                    clsInstallDBScripts.gloServerLog("Executed ICD Analysis Utility Scripts for 7022 and above");
                                    worker.ReportProgress(80, "Executed ICD Analysis Utility Scripts for 7022 and above");

                                    _result = true;
                                    if (SelectedDBVersion >= 8010)
                                    {
                                        if (clsInstallDBScripts.ScriptsNonReplication(strICD10UtilityScriptFilePath_8010, "ICDUtilitySP_8010" + clsDBSettings.CurrentDBInfo.DatabaseName, clsDBSettings.CurrentDBInfo.SqlServerName, clsDBSettings.CurrentDBInfo.DatabaseName, clsDBSettings.CurrentDBInfo.SqlUserName, clsDBSettings.CurrentDBInfo.SqlPassword, clsDBSettings.CurrentDBInfo.IsWindowsAuthentication.ToString(), strICDCPTGalleryImportScriptLog))
                                        {
                                            clsInstallDBScripts.gloServerLog("Executed ICD Analysis Utility Scripts for 8010 and above");
                                            worker.ReportProgress(99, "Executed ICD Analysis Utility Scripts for 8010 and above");
                                            _result = true;
                                        }
                                    }
                                    worker.ReportProgress(100, "Script Execution Completed. ");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsICDAnalysis.UpdateLog(ex.ToString(), true);
            }
            return _result;
        }
    }
}
