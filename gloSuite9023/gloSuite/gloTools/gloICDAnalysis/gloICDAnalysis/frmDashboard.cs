using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

using C1.Win.C1FlexGrid;
using System.Diagnostics;
using gloICDAnalysis.ClassLib;

namespace gloICDAnalysis
{
    public partial class frmDashboard : Form
    {
        BackgroundWorker worker;
        BackgroundWorker unusedICD10Worker = null;

        public Int32 DbVersion;
        DataTable dtProviders = null;
        DataTable dtDiagnosis = null;

        DataTable dtUnusedICD10Codes = new DataTable();
        DataTable dtTVPUnusedICD10Codes = null;
        DataTable dtICD10Mappings = null;

        gloICDAnalysis.ClassLib.DBSetting applicationDBSetting = null;
        CellStyle csTextCaption;

        #region Import Unused ICD 10 C1 Columns
        private int COL_Blank = 0;
        private int COL_Select = 1;
        private int COL_ICD10Code = 2;
        private int COL_Description = 3;
        private int COL_ICD9Code = 4;
        private int COL_ICD10CodeImported = 5;
        private int COL_TotalCount = 6;
        #endregion

        #region "Property and variables"

        //public DBSetting CurrentDBSettings { get; set; }

        public DBSetting.ApplicationType CurrentApplication
        {
            get
            {
                if (rbByClaim.Checked)
                {
                    return DBSetting.ApplicationType.gloPM;
                }
                else if (rbByExam.Checked)
                {
                    return DBSetting.ApplicationType.gloEMR;
                }

                return DBSetting.ApplicationType.gloEMR;
            }
            set
            {
                rbByExam.CheckedChanged -= new EventHandler(rbByExam_CheckedChanged);
                rbByClaim.CheckedChanged -= new EventHandler(rbByClaim_CheckedChanged);

                rbICD9.CheckedChanged -= new EventHandler(rbICD9_CheckedChanged);
                rbICD10.CheckedChanged -= new EventHandler(rbICD10_CheckedChanged);
                

                if (value == DBSetting.ApplicationType.gloEMR)
                { rbByExam.Checked = true; }
                else
                { rbByClaim.Checked = true; }

                rbByExam.CheckedChanged += new EventHandler(rbByExam_CheckedChanged);
                rbByClaim.CheckedChanged += new EventHandler(rbByClaim_CheckedChanged);

                rbICD9.CheckedChanged += new EventHandler(rbICD9_CheckedChanged);
                rbICD10.CheckedChanged += new EventHandler(rbICD10_CheckedChanged);                
            }

        }

        public Int64 ClinicID { get; set; }

        Int64 selectedProviderID;
        public Int64 SelectedProviderID
        {
            get { return selectedProviderID; }
            set { selectedProviderID = value; }
        }

        DateTime startDate;
        public DateTime StartDate
        {
            get { return dtpStartDate.Value; }
            set
            {
                dtpStartDate.ValueChanged -= new EventHandler(dtpStartDate_ValueChanged);
                dtpStartDate.Value = value;
                dtpStartDate.ValueChanged += new EventHandler(dtpStartDate_ValueChanged);
            }
        }

        DateTime endDate;
        public DateTime EndDate
        {
            get { return dtpEndDate.Value; }
            set
            {
                dtpEndDate.ValueChanged -= new EventHandler(dtpEndDate_ValueChanged);
                dtpEndDate.Value = value;
                dtpEndDate.ValueChanged += new EventHandler(dtpEndDate_ValueChanged);
            }
        }
               
        public Int32 ICDType
        {
            get 
            {
                if (rbICD9.Checked)
                { return 9; }
                else
                { return 10; }
            }
            set
            {
                 rbICD9.CheckedChanged -= new EventHandler(rbICD9_CheckedChanged);
                 rbICD10.CheckedChanged -= new EventHandler(rbICD10_CheckedChanged);

                if (value == 9)
                { rbICD9.Checked = true; }
                else
                { rbICD10.Checked = true ;}

                rbICD9.CheckedChanged += new EventHandler(rbICD9_CheckedChanged);
                rbICD10.CheckedChanged += new EventHandler(rbICD10_CheckedChanged);
            }
        }

        public clsICDAnalysis.ProviderType ProviderType
        {
            get
            {
                if (rbByClaim.Checked)
                { return clsICDAnalysis.ProviderType.Claim; }
                else
                { return clsICDAnalysis.ProviderType.Exam; }
            }
            set
            {
                rbByExam.CheckedChanged -= new EventHandler(rbByExam_CheckedChanged);
                rbByClaim.CheckedChanged -= new EventHandler(rbByClaim_CheckedChanged);

                if (value == clsICDAnalysis.ProviderType.Exam)
                { rbByExam.Checked = true; }
                else
                { rbByClaim.Checked = true; }

                rbByExam.CheckedChanged += new EventHandler(rbByExam_CheckedChanged);
                rbByClaim.CheckedChanged += new EventHandler(rbByClaim_CheckedChanged);

                LoadProviders(dtProviders);
            }
        }

        public gloICDAnalysis.ClassLib.DBSetting.ApplicationType Application_Type { get; set; }

        DBSetting AppDBSettings = new DBSetting(DBSetting.ApplicationType.Utility);
        #endregion

        public frmDashboard(gloICDAnalysis.ClassLib.DBSetting.ApplicationType AppType) // DBSetting DbConnection
        {
            InitializeComponent();
            Application_Type = AppType;
        }

        public frmDashboard(gloICDAnalysis.ClassLib.DBSetting.ApplicationType AppType, gloICDAnalysis.ClassLib.DBSetting appDBSetting) // DBSetting DbConnection
        {
            InitializeComponent();
            Application_Type = AppType;

            applicationDBSetting = appDBSetting;
            clsDBSettings.CurrentDBInfo = applicationDBSetting;
                        
        }

        private void SetApplicationEnviornment()
        {
            if (Application_Type != DBSetting.ApplicationType.Utility)
            {
                tsConnectDB.Visible = false;
                slblServerName.Visible = false;
                slblDatabase.Visible = false;
                CurrentApplication = Application_Type; 
            }
            
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                SetApplicationEnviornment();
                pnlPleasewait.Visible = true;

                if (Application_Type == DBSetting.ApplicationType.Utility)
                {
                    #region "Initialize Database"

                    if (!InitializeDBSettings())
                    {
                        this.Close();
                    }

                    #endregion

                    #region Script check & Execution

                    if (!CheckIsDBUpdateExist())
                    {
                        //pnlPleasewait.Visible = false;
                        //tspButtons.Enabled = true;
                        //this.Cursor = Cursors.Default;
                        this.Close();
                        return;
                    }

                    #endregion
                }
                else
                {
                    DbVersion = clsDBSettings.GetDBVersion();
                }
                
                

                #region "Setup Default Filters"
                selectedProviderID = 0;
                StartDate = DateTime.Now.Date.AddMonths(-6);
                EndDate = DateTime.Now.Date;
                LoadProviders(dtProviders);               
                #endregion

                this.DesignImportICD10Grid();

                worker = new System.ComponentModel.BackgroundWorker();
                worker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
                worker.DoWork += new System.ComponentModel.DoWorkEventHandler(worker_DoWork);
                worker.RunWorkerAsync();

                unusedICD10Worker = new System.ComponentModel.BackgroundWorker();
                unusedICD10Worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(unusedICD10Worker_RunWorkerCompleted);
                unusedICD10Worker.DoWork += new DoWorkEventHandler(unusedICD10Worker_DoWork);


                Cursor = Cursors.WaitCursor;
                tspButtons.Enabled = false;
            }
            catch (Exception ex)
            {
                clsICDAnalysis.UpdateLog(ex.ToString(),true);
            }
        }

        private bool CheckIsDBUpdateExist()
        {
            try
            {
                //clsDBSettings.CurrentDBInfo.DatabaseName
                if (!clsDBSettings.IsDBUpdatesExists())
                {
                    DialogResult oDlgResult = DialogResult.None;
                    oDlgResult = MessageBox.Show("Database updates are required on " + clsDBSettings.CurrentDBInfo.DatabaseName + " to use this utility." + Environment.NewLine + "Please click ‘Yes’ to install the updates or click ‘No’ to cancel.  ", "ICD Utility", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (oDlgResult == DialogResult.Yes)
                    {
                        frmDBScriptInstaller frm = new frmDBScriptInstaller(DbVersion);
                        frm.ShowDialog(this);
                        frm.Dispose();
                        frm = null;
                    }
                    else
                    {
                      
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                clsICDAnalysis.UpdateLog(ex.ToString(), true);
                return false;
            }
        }

        void worker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            dtProviders = clsICDAnalysis.GetAllProvider(ProviderType);          
            dtDiagnosis = clsICDAnalysis.GetICDsByFilter(ProviderType, SelectedProviderID, StartDate, EndDate, DbVersion, ICDType);
        }

        void worker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                LoadList(dtDiagnosis);
            }
            catch (Exception ex)
            {
                clsICDAnalysis.UpdateLog(ex.ToString());
            }
            finally
            {
                pnlPleasewait.Visible = false;
                tspButtons.Enabled = true;
                this.Cursor = Cursors.Default;               
            }
        }

        private void LoadProviders(DataTable dtProvider)
        {
            try
            {
                cmbProvider.SelectedIndexChanged -= new EventHandler(cmbProvider_SelectedIndexChanged);
                dtProvider = clsICDAnalysis.GetAllProvider(ProviderType);
                cmbProvider.DisplayMember = dtProvider.Columns["Provider"].ToString();
                cmbProvider.ValueMember = dtProvider.Columns["nProviderID"].ToString();
                cmbProvider.DataSource = dtProvider;
                cmbProvider.SelectedValue = 0;
            }
            catch (Exception ex)
            {
                clsICDAnalysis.UpdateLog(ex.ToString());
            }
            finally
            {
                cmbProvider.SelectedIndexChanged += new EventHandler(cmbProvider_SelectedIndexChanged);
            }
        }

        private void LoadList(DataTable dtICDList)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                FillUsageData(dtICDList);
                
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

        private void FillUsageData(DataTable dt)
        {
            try
            {
                c1UsageGrid.Redraw = false;
                c1UsageGrid.BeginUpdate();

                c1UsageGrid.DataSource = dt;
                c1UsageGrid.Cols[1].Width = 90;
                c1UsageGrid.Cols[3].Width = 110;
                c1UsageGrid.Cols[4].Width = 550;
                c1UsageGrid.AllowSorting = AllowSortingEnum.None;
                c1UsageGrid.Cols[2].AllowEditing = false;
                c1UsageGrid.Cols[3].AllowEditing = false;
                c1UsageGrid.Cols[4].AllowEditing = false;
                c1UsageGrid.Cols[5].AllowEditing = false;
                c1UsageGrid.Cols[6].AllowEditing = false;
                c1UsageGrid.Cols[7].AllowEditing = false;
                c1UsageGrid.Styles.Normal.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
               

                if (dt.Rows.Count > 0)
                {
                    lblRecord.Text = dt.Rows.Count.ToString() + " Record(s) found";
                }
                else
                {
                    lblRecord.Text = "No Record found ";
                }
                tslb_SelectAll.Text = "Select All";
            }
            catch (Exception ex)
            {
                clsICDAnalysis.UpdateLog(ex.ToString(), true);
            }
            finally
            {
                c1UsageGrid.EndUpdate();
                c1UsageGrid.Redraw = true;                
            }
        }

        private void rbByExam_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbByExam.Checked)
                {
                    if (Application_Type != DBSetting.ApplicationType.Utility)
                    {
                        //Bug no. 77342 :: Tools - Patient ICD9 Usage And ICD10 Mapping Report - Application is not able to Connect SQL server if switch to By Claim and exception is also coming.
                        //if (Application_Type != CurrentApplication)
                        //{
                        //    if (!InitializeDBSettings())
                        //    {
                        //        return;
                        //    }
                        //}
                        //else
                        //{
                            clsDBSettings.CurrentDBInfo = applicationDBSetting;
                        //}
                    }
                    else
                    {
                        if (!InitializeDBSettings())
                        {
                            return;
                        }
                    }

                    ProviderType = clsICDAnalysis.ProviderType.Exam;
                    RefreshList();

                }
            }
            catch (Exception ex)
            {
                clsICDAnalysis.UpdateLog(ex.ToString(), true);
            }
        }

        private void rbByClaim_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbByClaim.Checked)
                {
                    if (Application_Type != DBSetting.ApplicationType.Utility)
                    {
                        //Bug no. 77342 :: Tools - Patient ICD9 Usage And ICD10 Mapping Report - Application is not able to Connect SQL server if switch to By Claim and exception is also coming.
                        //if (Application_Type != CurrentApplication)
                        //{
                        //    if (!InitializeDBSettings())
                        //    {
                        //        return;
                        //    }
                        //}
                        //else
                        //{
                            clsDBSettings.CurrentDBInfo = applicationDBSetting;
                        //}
                    }
                    else
                    {
                        if (!InitializeDBSettings())
                        {
                            return;
                        }
                    }

                    ProviderType = clsICDAnalysis.ProviderType.Claim;
                    RefreshList();
                }
            }
            catch (Exception ex)
            {
                clsICDAnalysis.UpdateLog(ex.ToString(), true);
            }
        }

        private void rbICD9_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbICD9.Checked)
                {
                    tsImportICD10Codes.Visible = true;
                    tsShowReport.Visible = true;
                    ICDType = 9;
                    RefreshList();
                }
                else
                {
                    tsImportICD10Codes.Visible = false;
                    tsShowReport.Visible = false;
                    ICDType = 10;
                    RefreshList();   
                }
            }
            catch (Exception ex)
            {
                clsICDAnalysis.UpdateLog(ex.ToString(), true);
            }
        }

        private void rbICD10_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbICD10.Checked)
                {
                    tsImportICD10Codes.Visible = false;
                    tsShowReport.Visible = false;
                    ICDType = 10;
                    RefreshList();
                }
                else
                {
                    tsImportICD10Codes.Visible = true;
                    tsShowReport.Visible = true;
                    ICDType = 9;
                    RefreshList();
                }
            }
            catch (Exception ex)
            {
                clsICDAnalysis.UpdateLog(ex.ToString(), true);
            }
        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            dtpStartDate.MaxDate = dtpEndDate.Value;    
            RefreshList();
        }

        private void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {
            dtpStartDate.MaxDate = dtpEndDate.Value;    
            RefreshList();
        }

        private void cmbProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void tsGenerateList_Click(object sender, EventArgs e)
        {
            // LoadList();
        }


        private void RefreshList()
        {
            //pnlPleasewait.Visible = true;
            try
            {
                //tspButtons.Enabled = false;
                //tslb_SelectAll.Enabled = false;
                // Cursor = Cursors.WaitCursor;
                startDate = dtpStartDate.Value;
                endDate = dtpEndDate.Value;
                selectedProviderID = Convert.ToInt64(cmbProvider.SelectedValue);

                if (!worker.IsBusy)
                {
                    worker.RunWorkerAsync();
                    Cursor = Cursors.WaitCursor;
                    tspButtons.Enabled = false;                   
                }
                else
                {
                    pnlPleasewait.Visible = true;
                }
            }
            catch (Exception ex)
            {
                clsICDAnalysis.UpdateLog(ex.ToString(), true);
            }
            finally
            {
                //tspButtons.Enabled = true;
                //tslb_SelectAll.Enabled = true;
                //this.Cursor = Cursors.Default;
            }
        }

        private void tsConnectDB_Click(object sender, EventArgs e)
        {
            try
            {
                LoadDatabaseForm();
                if (!InitializeDBSettings())
                {
                    return;
                }
                RefreshList();
                //ShowConnectionForm_PM();
                //slblServerName.Text = _sqlservername;
                //slblDatabase.Text = _databasename;
            }
            catch (Exception ex)
            {
                clsICDAnalysis.UpdateLog(ex.ToString(), true);
            }
        }


        private void tsShowReport_Click(object sender, EventArgs e)
        {
            frmMappingReport ofrmReport=null;

            try
            {
                this.Cursor = Cursors.WaitCursor;
                DataTable tvpCodes = new DataTable("tvp_ICDCodes");
                tvpCodes.Columns.Add("sICDCode");

                for (int i = 1; i <= c1UsageGrid.Rows.Count - 1; i++)
                {
                    if (Convert.ToBoolean(c1UsageGrid.GetData(i, 1)) == true)
                    {
                        tvpCodes.Rows.Add(c1UsageGrid.GetData(i, 2));
                    }
                }

                if (tvpCodes.Rows.Count > 0)
                {
                    ofrmReport = new frmMappingReport(tvpCodes);
                   
                    ofrmReport.ShowDialog(this);
                    if (ofrmReport!=null)
                    {
                        ofrmReport.Dispose();
                        ofrmReport = null;
                    }
                }
                else
                {
                    MessageBox.Show("Please select ICD-9 Codes. ", "ICD Utility", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                clsICDAnalysis.UpdateLog(ex.ToString(), true);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                if (ofrmReport != null)
                {
                    ofrmReport.Dispose();
                    ofrmReport = null;
                }
            }
        }

        private void tsExit_Click(object sender, EventArgs e)
        {            
            this.Close();
        }

        private bool InitializeDBSettings()
        {
            try
            {
                clsDBSettings.CurrentDBInfo = clsDBSettings.GetDatabaseSettings(CurrentApplication);

                this.Cursor = Cursors.WaitCursor;

                bool isInitialized = false;

                if (clsDBSettings.CurrentDBInfo == null)
                {
                    if (LoadDatabaseForm() == false)
                    {
                        Application.ExitThread();
                        return false;
                    }
                }
                else
                {
                    isInitialized = clsDBSettings.ValidateDatabaseSettings();

                    this.Cursor = Cursors.Default;
                    if (!isInitialized)
                    {
                        DialogResult oDlgResult = DialogResult.None;
                        oDlgResult = MessageBox.Show("Unable to connect to SQL Server " + clsDBSettings.CurrentDBInfo.SqlServerName + " and Database " + clsDBSettings.CurrentDBInfo.DatabaseName + Environment.NewLine + "Do you want to change Database Settings?  ", "ICD Utility", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (oDlgResult == DialogResult.Yes)
                        {
                            if (LoadDatabaseForm() == false)
                            {
                                Application.ExitThread();
                                return false;
                            }
                        }
                        else if (oDlgResult == DialogResult.No)
                        {
                            Application.ExitThread();
                            return false;
                        }
                    }
                }


                DbVersion = clsDBSettings.GetDBVersion();

                slblServerName.Text = clsDBSettings.CurrentDBInfo.SqlServerName;
                slblDatabase.Text = clsDBSettings.CurrentDBInfo.DatabaseName;

                return true;
            }
            catch (Exception ex)
            {
                clsICDAnalysis.UpdateLog(ex.ToString(), true);
                return false;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private bool LoadDatabaseForm()
        {
            bool _result = true;
            try
            {
                frmDBSettings oFrmDB = new frmDBSettings(CurrentApplication);
               // oFrmDB.ShowDialog();
                if (oFrmDB.ShowDialog(this) == DialogResult.Cancel)
                {

                    _result= false;
                }
                
                if (_result)
                {
                    if (clsDBSettings.CurrentDBInfo.IsDBChanged)
                    {
                        if (!CheckIsDBUpdateExist())
                        {
                            MessageBox.Show("DB Updates are not available or missing for ICD Utility.", "ICD Utility", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _result = false;
                        }
                    }
                }
                oFrmDB.Dispose();
                oFrmDB = null;
            }
            catch (Exception ex)
            {
                clsICDAnalysis.UpdateLog(ex.ToString(), true);
            }
            return _result;
        }


        private void tslb_SelectAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (c1UsageGrid.Rows.Count > 1)
                {
                    bool bSelect = false;
                    if (tslb_SelectAll.Text == "Select All")
                    {
                        bSelect = true;
                        tslb_SelectAll.Text = "Deselect All";
                        tslb_SelectAll.Image = Properties.Resources.DeSelect_All;
                    }
                    else
                    {
                        bSelect = false;
                        tslb_SelectAll.Text = "Select All";
                        tslb_SelectAll.Image = Properties.Resources.Select_All;
                    }


                    for (int i = 1; i <= c1UsageGrid.Rows.Count - 1; i++)
                    {
                        c1UsageGrid.SetData(i, 1, bSelect);
                    }
                }
            }
            catch (Exception ex)
            {
                clsICDAnalysis.UpdateLog(ex.ToString(), true);
            }
        }

        private void frmDashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (worker != null)
            {
                if (!worker.IsBusy)
                {
                    //if (worker != null)
                    //{
                    worker.Dispose();
                    worker = null;
                    //}
                }
                else
                {
                    e.Cancel = true;
                }
            }

            if (unusedICD10Worker != null)
            {
                if (!unusedICD10Worker.IsBusy)
                {
                    unusedICD10Worker.Dispose();
                    unusedICD10Worker = null;
                }
                else { e.Cancel = true; }
            }

            if (dtUnusedICD10Codes != null)
            {
                dtUnusedICD10Codes.Dispose();
                dtUnusedICD10Codes = null;
            }

            if (dtTVPUnusedICD10Codes != null)
            {
                dtTVPUnusedICD10Codes.Dispose();
                dtTVPUnusedICD10Codes = null;
            }

            if (dtICD10Mappings != null)
            {
                dtICD10Mappings.Dispose();
                dtICD10Mappings = null;
            }

            csTextCaption = null;

        }

        private void tsRefresh_Click(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void frmDashboard_Shown(object sender, EventArgs e)
        {

        }

        #region "Import ICD 10 Codes"
        private void tsImportICD10Codes_Click(object sender, EventArgs e)
        {           
            try
            {
                if (pnlICD9Usage.Visible)
                {
                    bool bImportValidate = false;
                    for (int i = 1; i <= c1UsageGrid.Rows.Count - 1; i++)
                    {
                        if (Convert.ToBoolean(c1UsageGrid.GetData(i, 1)) == true)
                        {
                            bImportValidate = true;
                            break;
                        }
                    }

                    if (!bImportValidate)
                    {
                        MessageBox.Show("Please select ICD 9 code(s).", "ICD Utility", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                this.pnlICD9Usage.Visible = false;
                this.pnlPleasewait.Visible = true;

                if (this.dtTVPUnusedICD10Codes == null)
                {
                    this.dtTVPUnusedICD10Codes = new DataTable();
                    dtTVPUnusedICD10Codes.Columns.Add("sICDCode");
                }
                else
                { this.dtTVPUnusedICD10Codes.Clear(); }

                if (this.dtUnusedICD10Codes == null)
                { this.dtUnusedICD10Codes = new DataTable(); }
                else
                { this.dtUnusedICD10Codes.Clear(); }

                for (int i = 1; i <= c1UsageGrid.Rows.Count - 1; i++)
                {
                    if (Convert.ToBoolean(c1UsageGrid.GetData(i, COL_Select)) == true)
                    {
                        dtTVPUnusedICD10Codes.Rows.Add(c1UsageGrid.GetData(i, 2));
                    }
                }

                //30-Aug=16 Aniket: Handling Bug #99240: gloEMR : ICD9 Usage and ICD10 Mapping Report : Application showing Exception.
                try
                {
                    this.unusedICD10Worker.RunWorkerAsync();
                }

                catch (Exception ex)
                {
                    clsICDAnalysis.UpdateLog(ex.ToString(), false);
                }
            }
            catch (Exception ex)
            {
                clsICDAnalysis.UpdateLog(ex.ToString(), true);
            }           
        }

        private void DesignImportICD10Grid()
        {
            try
            {
                this.c1Import10Codes.AutoGenerateColumns = false;
                this.c1Import10Codes.Cols.Count = COL_TotalCount;
                //bugid Bug #99229
                this.c1Import10Codes.Cols[COL_Select].AllowEditing = true;
                this.c1Import10Codes.Cols[COL_Blank].AllowEditing = false;
                this.c1Import10Codes.Cols[COL_ICD10Code].AllowEditing = false;
                this.c1Import10Codes.Cols[COL_Description].AllowEditing = false;
                this.c1Import10Codes.Cols[COL_ICD9Code].AllowEditing = false;
                this.c1Import10Codes.Cols[COL_ICD10CodeImported].AllowEditing = false; 

                this.c1Import10Codes.Cols[COL_Blank].Caption = "";
                this.c1Import10Codes.Cols[COL_Select].Caption = "Select";
                this.c1Import10Codes.Cols[COL_ICD10Code].Caption = "ICD 10 Code";
                this.c1Import10Codes.Cols[COL_Description].Caption = "Description";
                this.c1Import10Codes.Cols[COL_ICD9Code].Caption = "ICD 9 Code";
                this.c1Import10Codes.Cols[COL_ICD10CodeImported].Caption = "ICD 10 Code Status";

                this.c1Import10Codes.Cols[COL_Blank].Name = "";
                this.c1Import10Codes.Cols[COL_Select].Name = "Select";
                this.c1Import10Codes.Cols[COL_ICD10Code].Name = "sICD10Code";
                this.c1Import10Codes.Cols[COL_Description].Name = "sDescriptionLong";
                this.c1Import10Codes.Cols[COL_ICD9Code].Name = "sICD9DecimalCode";
                this.c1Import10Codes.Cols[COL_ICD10CodeImported].Name = "Code Present";

                this.c1Import10Codes.Cols[COL_Blank].Width = 0;
                this.c1Import10Codes.Cols[COL_Select].Width = 50;
                this.c1Import10Codes.Cols[COL_ICD10Code].Width = 200;
                this.c1Import10Codes.Cols[COL_Description].Width = 700;
                this.c1Import10Codes.Cols[COL_ICD9Code].Width = 100;
                this.c1Import10Codes.Cols[COL_ICD10CodeImported].Width = 145;

                this.c1Import10Codes.Cols[COL_Select].DataType = typeof(System.Boolean);
                this.c1Import10Codes.Cols[COL_ICD10Code].DataType = typeof(System.String);
                this.c1Import10Codes.Cols[COL_ICD9Code].DataType = typeof(System.String);


                try
                {
                    if (this.c1Import10Codes.Styles.Contains("ImportCheck"))
                    {
                        csTextCaption = this.c1Import10Codes.Styles["ImportCheck"];
                    }
                    else
                    {
                        csTextCaption = this.c1Import10Codes.Styles.Add("ImportCheck");
                        csTextCaption.Font = new System.Drawing.Font("Webdings", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        csTextCaption.TextEffect = TextEffectEnum.Flat;
                        csTextCaption.ForeColor = System.Drawing.Color.Green;
                        csTextCaption.TextAlign = TextAlignEnum.CenterCenter;
                    }
                }
                catch
                {
                    csTextCaption = this.c1Import10Codes.Styles.Add("ImportCheck");
                    csTextCaption.Font = new System.Drawing.Font("Webdings", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    csTextCaption.TextEffect = TextEffectEnum.Flat;
                    csTextCaption.ForeColor = System.Drawing.Color.Green;
                    csTextCaption.TextAlign = TextAlignEnum.CenterCenter;
                }
            }
            catch (Exception ex)
            {
                clsICDAnalysis.UpdateLog(ex.ToString(), true);
            }
        }

        #region "Unused ICD10 Worker"
        void unusedICD10Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.c1Import10Codes.DataSource = this.dtUnusedICD10Codes;

            this.pnlDateRanges.Visible = false;

            this.pnlPleasewait.Visible = false;
            this.tsImportICD10Codes.Visible = false;
            this.tsShowReport.Visible = false;
            this.btnPrint.Visible = false;
            this.tsRefresh.Visible = false;
            this.tslb_SelectAll.Visible = false;
            this.tsConnectDB.Visible = false;

            this.pnlImport10Codes.Visible = true;
            this.tsSaveICD10Codes.Visible = true;
            this.tlBack.Visible = true;

            for (int i = 1; i <= c1Import10Codes.Rows.Count - 1; i++)
            {
                if (Convert.ToString(c1Import10Codes.GetData(i, COL_ICD10CodeImported)) == "")
                {
                    c1Import10Codes.SetData(i, COL_Select, true);
                }
            }

            if (c1Import10Codes != null && c1Import10Codes.Rows.Count > 1)
            {
                CellRange statusIconRange;
                statusIconRange = c1Import10Codes.GetCellRange(1, COL_ICD10CodeImported, c1Import10Codes.Rows.Count - 1, COL_ICD10CodeImported);
                statusIconRange.Style = csTextCaption;
            }
        }

        void unusedICD10Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                this.dtICD10Mappings = clsICDAnalysis.GetMappings(dtTVPUnusedICD10Codes).DefaultView.ToTable(false, new string[] { "sICD9Code", "sICD10Code" });
                this.dtUnusedICD10Codes = clsICDAnalysis.GetICD10ImportData(dtICD10Mappings);
            }
            catch (Exception ex)
            {
                clsICDAnalysis.UpdateLog(ex.ToString(), true);
            }
        }
        #endregion

        private void tsSaveICD10Codes_Click(object sender, EventArgs e)
        {
            DataTable dtTVPSaveICD10Codes = null;

            try
            {
                dtTVPSaveICD10Codes = new DataTable();

                var cols = dtTVPSaveICD10Codes.Columns;
                cols.Add(new DataColumn("sICD9Code", System.Type.GetType("System.String")));
                cols.Add(new DataColumn("sDescription", System.Type.GetType("System.String")));
                cols.Add(new DataColumn("nSpecialtyID", System.Type.GetType("System.Int64")));

                cols.Add(new DataColumn("nClinicID", System.Type.GetType("System.Int64")));
                cols.Add(new DataColumn("bIsBlocked", System.Type.GetType("System.Boolean")));
                cols.Add(new DataColumn("bInActive", System.Type.GetType("System.Boolean")));

                cols.Add(new DataColumn("nImmediacyDefault", System.Type.GetType("System.Int32")));
                cols.Add(new DataColumn("nICDRevision", System.Type.GetType("System.Int16")));
                cols.Add(new DataColumn("nCodeType", System.Type.GetType("System.Int16")));

                cols.Add(new DataColumn("sConceptID", System.Type.GetType("System.String")));
                cols.Add(new DataColumn("sDescriptionID", System.Type.GetType("System.String")));
                cols.Add(new DataColumn("sSnomedID", System.Type.GetType("System.String")));

                cols.Add(new DataColumn("sSnomedDescription", System.Type.GetType("System.String")));
                cols.Add(new DataColumn("sSnomedDefination", System.Type.GetType("System.String")));



                for (int i = 1; i <= c1Import10Codes.Rows.Count - 1; i++)
                {
                    if (
                        Convert.ToBoolean(c1Import10Codes.GetData(i, COL_Select)) == true
                        &&
                        Convert.ToString(c1Import10Codes.GetData(i, COL_ICD10CodeImported)) == ""
                        )
                    {
                        DataRow dRow = dtTVPSaveICD10Codes.NewRow();

                        dRow["sICD9Code"] = Convert.ToString(c1Import10Codes.GetData(i, COL_ICD10Code));
                        dRow["sDescription"] = Convert.ToString(c1Import10Codes.GetData(i, COL_Description));
                        dRow["nSpecialtyID"] = 0;

                        dRow["nClinicID"] = this.ClinicID;
                        dRow["bIsBlocked"] = DBNull.Value;
                        dRow["bInActive"] = false;

                        dRow["nImmediacyDefault"] = 3;
                        dRow["nICDRevision"] = 10;
                        dRow["nCodeType"] = 1;

                        dRow["sConceptID"] = DBNull.Value;
                        dRow["sDescriptionID"] = DBNull.Value;
                        dRow["sSnomedID"] = DBNull.Value;

                        dRow["sSnomedDescription"] = DBNull.Value;
                        dRow["sSnomedDefination"] = DBNull.Value;

                        dtTVPSaveICD10Codes.Rows.Add(dRow);
                    }
                }

                if (dtTVPSaveICD10Codes.Rows.Count > 0)
                {
                    clsICDAnalysis.SaveICD10Codes(dtTVPSaveICD10Codes);
                    MessageBox.Show(dtTVPSaveICD10Codes.Rows.Count + " ICD10 codes imported.", "ICD Utility", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                { MessageBox.Show("Please select ICD10 code(s) to import that are not present in the master.", "ICD Utility", MessageBoxButtons.OK, MessageBoxIcon.Information); }

            }
            catch (Exception ex)
            {
                clsICDAnalysis.UpdateLog(ex.ToString(), true);
            }
            finally
            {
                if (dtTVPSaveICD10Codes != null)
                {
                    dtTVPSaveICD10Codes.Dispose();
                    dtTVPSaveICD10Codes = null;
                }
            }
        }

        private void tlBack_Click(object sender, EventArgs e)
        {
            try
            {
                this.pnlICD9Usage.Visible = true;
                this.pnlImport10Codes.Visible = true;
                this.pnlDateRanges.Visible = true;

                this.tsImportICD10Codes.Visible = true;
                this.tsShowReport.Visible = true;
                this.btnPrint.Visible = true;
                this.tsRefresh.Visible = true;

                if (Application_Type == DBSetting.ApplicationType.Utility)
                {
                    this.tsConnectDB.Visible = true;
                }


                this.tsSaveICD10Codes.Visible = false;
                this.tlBack.Visible = false;
                this.tslb_SelectAll.Visible = true;

                if (dtUnusedICD10Codes != null) { dtUnusedICD10Codes.Clear(); }

                if (dtTVPUnusedICD10Codes != null) { dtTVPUnusedICD10Codes.Clear(); }
            }
            catch (Exception ex)
            {
                clsICDAnalysis.UpdateLog(ex.ToString(), true);
            }

        }

        #endregion

        private void btnPrint_Click(object sender, EventArgs e)
        {
            bool flag = true;
            this.Cursor = Cursors.WaitCursor;
           frmMappingReport frm = null;      
     
           try
            {
                if (c1UsageGrid.Rows.Count <= 1)
                {
                    MessageBox.Show("No Record to Print Report.", "ICD Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    flag = false;
                }

                if (flag == true)
                {
                    frm = new frmMappingReport(ProviderType, SelectedProviderID, StartDate, EndDate, ICDType, "P");
                    frm.ShowInTaskbar = false;
                    frm.ShowDialog(this);

                    if (frm != null)
                    {
                        frm.Dispose();
                        frm = null;
                    }
                }
            }
                                      
            catch (Exception ex)
            {
                clsICDAnalysis.UpdateLog(ex.ToString(), true);
            }

           finally
           {
               this.Cursor = Cursors.Default;
               if (frm != null)
               {
                   frm.Dispose();
                   frm = null;
               }
           }
                 
        }

        private void c1UsageGrid_MouseMove(object sender, MouseEventArgs e)
        {

           gloC1FlexStyles.ShowToolTip(ref C1SuperTooltip2, ref this.c1UsageGrid  , e.Location);
        }

    }
}
