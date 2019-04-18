using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;


namespace gloEDocumentV3.Forms
{

    partial class frmEDocEvent_Migrate : Form
    {
        private DataTable  _dtMigrateTable = new DataTable ();
                public DataTable  dtMigrateTable
        {
            get { return _dtMigrateTable ; }
            set { _dtMigrateTable =  value; }
        }
	
        public frmEDocEvent_Migrate(Int64 PatientID, string DMSPath, bool StartAutomatic)
        {
            this.Hide();
            InitializeComponent();
            _PatientID = PatientID;
            _DMSPath = DMSPath;
            _StartAutomatic = StartAutomatic;
            tmr_Auto.Stop();
            tmr_Auto.Enabled = false;
            if (_StartAutomatic == true) { tmr_Auto.Start(); tmr_Auto.Enabled = true; }
        }

        #region "Page Manuipulation Variables"

        public bool oDialogResultIsOK = false;
        private Int64 _PatientID = 0;
        private string _DMSPath = "";
        bool _StartAutomatic = false;
        public string _ErrorMessage = "";
        #endregion

        private void tlb_Migrate_Click(object sender, EventArgs e)
        {
            gloEDocumentV3.eDocManager.eDocManager oDocManager = new gloEDocumentV3.eDocManager.eDocManager();
            gloDMSMigration.ClsDMSMigrationGeneral.BufferSize = gloEDocV3Admin.gBufferSize;
            gloDMSMigration.ClsDMSMigrationGeneral.ConnectionString = gloEDocV3Admin.gDatabaseConnectionString;
            gloDMSMigration.ClsDMSMigrationGeneral.DeleteAfterMigration = gloEDocV3Admin.gDeleteAfterMigration;
            gloDMSMigration.ClsDMSMigrationGeneral.DMSOutputFilepathV1toV3 = _DMSPath;
            gloDMSMigration.ClsDMSMigrationGeneral.ApplicationStartupPath = gloEDocV3Admin.gTemporaryProcessPath;
            gloDMSMigration.ClsDMSMigrationGeneral.UseCompression = gloEDocV3Admin.gUseCompressionForMigration;
            //COMMENTED BY SHUBHANGI 20100507 B'COZ WE ARE NOT USING SPLIT DOCUMENT FUNCTIONALITY
           //gloDMSMigration.ClsDMSMigrationGeneral.gblnFileSplit = gloEDocumentV3.eDocManager.eDocValidator.IsSplitDocument();
            gloDMSMigration.ClsDMSMigrationGeneral.gblnFileSplit = false;

            gloDMSMigration.ClsDMSMigrationGeneral.gFileSizeMax = gloEDocV3Admin.gnDMSV3FileSizeMax ;
            gloDMSMigration.ClsDMSMigrationGeneral.gFileSizeMin = gloEDocV3Admin.gnDMSV3FileSizeMin;

            gloDMSMigration.ClseDocV3MigrationV1ToV3 oMigrationV1ToV3 = new gloDMSMigration.ClseDocV3MigrationV1ToV3();
            gloDMSMigration.ClseDocV3MigrationV2ToV3 oMigrationV2ToV3 = new gloDMSMigration.ClseDocV3MigrationV2ToV3(gloEDocV3Admin.gDatabaseConnectionString);

            oMigrationV1ToV3.AfterDocumentMigrated += new gloDMSMigration.ClseDocV3MigrationV1ToV3.DocumentMigrated(MigrationV1toV3_AfterDocumentMigrated);
            oMigrationV2ToV3.AfterDocumentMigrated += new gloDMSMigration.ClseDocV3MigrationV2ToV3.DocumentMigrated(MigrationV1toV3_AfterDocumentMigrated);
                   
            // If DMS V2 Tables are not Exists then Return True so that the DMSV3 Scan Form
            if (gloDMSMigration.ClsDMSMigrationGeneral.IsTableExists("eDocument_Container") == false)
            { oDialogResultIsOK = true; }

            if (gloDMSMigration.ClsDMSMigrationGeneral.IsTableExists("eDocument_Details") == false)
            { oDialogResultIsOK = true; }
            //

            try
            {
                //this.Hide();
                Application.DoEvents();
                tlb_Migrate.Enabled = false;
                tlb_Cancel.Enabled = false;
                Application.DoEvents();
              //  oDocManager.DocumentProgressEvent += new gloEDocumentV3.eDocManager.eDocManager.DocumentProgress(oDocManager_DocumentProgressEvent);

                DataTable dt = null;
                DataTable dtV1ToV3 = new DataTable();
                DataTable dtV2ToV3 = null;

                //dt =  oMigrationV2ToV3.CanMigrateDocuments(_PatientID);
                dt = _dtMigrateTable;

                //-- 
                dtV1ToV3.Columns.Add("PatientID");
                dtV1ToV3.Columns.Add("PatientCode");
                dtV1ToV3.Columns.Add("PatientName");
                dtV1ToV3.Columns.Add("MigrationFlag");
                //--

                dtV2ToV3 = dtV1ToV3.Clone();

                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Int64 PatientID = 0;
                        String PatientCode = "";
                        String PatientName = "";
                        String MigrationFlag = "";
                        DataRow r;

                        PatientID = Convert.ToInt64(dt.Rows[i]["PatientID"].ToString());
                        PatientCode = dt.Rows[i]["sPatientCode"].ToString();
                        PatientName = dt.Rows[i]["FName"].ToString() + " " + dt.Rows[i]["MName"].ToString() + " " + dt.Rows[i]["LName"].ToString();
                        MigrationFlag = dt.Rows[i]["MigrationFlag"].ToString();

                        if (dt.Rows[i]["MigrationFlag"].ToString() == "V1V3")
                        {
                            r = dtV1ToV3.NewRow();
                            r["PatientID"] = _PatientID;
                            r["PatientCode"] = PatientCode;
                            r["PatientName"] = PatientName;
                            r["MigrationFlag"] = MigrationFlag;
                            dtV1ToV3.Rows.Add(r);
                        }
                        else if (dt.Rows[i]["MigrationFlag"].ToString() == "V2V3")
                        {
                            r = dtV2ToV3.NewRow();
                            r["PatientID"] = _PatientID;
                            r["PatientCode"] = PatientCode;
                            r["PatientName"] = PatientName;
                            r["MigrationFlag"] = MigrationFlag;
                            dtV2ToV3.Rows.Add(r);
                        }
                    }
                }

                if (dtV1ToV3.Rows.Count > 0)
                {
                    this.Show();
                    if (oMigrationV1ToV3.MigrateV1ToV3(dtV1ToV3) == 0)
                    { // if Unsuccessfull Migartion count is 0 
                        oDialogResultIsOK = true; 
                    }
                }
                else
                { // Migartion Already done or no record found for Migration 
                    oDialogResultIsOK = true; 
                }

                if (dtV2ToV3.Rows.Count > 0)
                {
                    this.Show();
                    if (oMigrationV2ToV3.MigrateV2toV3(dtV2ToV3) == 0)
                    { // if Unsuccessfull Migartion count is 0
                        oDialogResultIsOK = true; 
                    }
                }
                else
                { // Migartion Already done or no record found for Migration 
                    oDialogResultIsOK = true; 
                }
                            
                this.Close();
            }
            catch (Exception ex)
            {
                #region " Make Log Entry "

                _ErrorMessage = ex.ToString();
                //Code added on 7rd October 2008 By - Sagar Ghodke
                //Make Log entry in DMSExceptionLog file for any exceptions found
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }

                //End Code add
                #endregion " Make Log Entry "

                MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                tlb_Migrate.Enabled = true;
                tlb_Cancel.Enabled = true;
            //    oDocManager.DocumentProgressEvent -= new gloEDocumentV3.eDocManager.eDocManager.DocumentProgress(oDocManager_DocumentProgressEvent);
                oDocManager.Dispose();
                //oDocManager.Dispose();
                //SLR: Free other memories like dt2v2 .. etc
            }
        }

        private void MigrationV1toV3_AfterDocumentMigrated(string ePatientName,string Category,string DocumentName)
        {
            //lblMigratingPatient.Text = "Migrating Documents for " + ePatientName + " ...";
            lblMigratingPatient.Text = "Migrating Category " + Category  + Environment.NewLine  + "Document " + DocumentName + " ...";
          
            lblMigratingPatient.Refresh();
            progressBar1.Increment(1);
            progressBar1.Refresh();
            Application.DoEvents();
        }

        void oDocManager_DocumentProgressEvent(int Percentage, string Message)
        {
            txtProgress.AppendText(Environment.NewLine + Message);
        }

        public  void tmr_Auto_Tick(object sender, EventArgs e)
        {
            tmr_Auto.Stop();
            tmr_Auto.Enabled = false;
            tlb_Migrate_Click(null, null);
        }

        private void tlb_Cancel_Click(object sender, EventArgs e)
        {
            oDialogResultIsOK = false;
            this.Close();
        }


    }
}
