using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloAuditTrail;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using gloCardScanning;
using gloSSRSApplication;
using gloPatientPortalCommon;
using System.Collections; 
namespace gloPatient
{
     
    public partial class frmSetupPatient : gloAUSLibrary.MasterForm
    {
        string sPatientInboundHospital = string.Empty;
        string sPatientInboundTranCare = string.Empty;
        public static Hashtable hshPatData = new Hashtable(); 
        #region "Global Declarations for Variables"
        private bool gblnYesNoLab = false;
        private string _EMRdatabaseconnectionstring = "";
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        private bool _AddPatientToEMR = false;

        private Int64 _ClinicID = 1;
        private string _databaseconnectionstring = "";
        private string _messageboxcaption = String.Empty;

        public string _UserName = string.Empty;

        private Int64 _UserID = 1;

        private Int64 _PatientID = 0;
        public Int64 ReturnPatientID;
        public bool ReturnIsClose = false;
        private ScanedPatient _ScannedPatient;
        private ScanedInsurance _ScanedInsurance;
        private bool _FillPatientFromScan = false;
        private bool isSaveAndClose = false;
        private string _MessageBoxCaption;

        private CardScanType _CardScanType = CardScanType.None;
        public bool _IsSaveAsCopy = false;

        public bool _IsPatientAccountFeature = false;
        public bool _IsAllowMultipleGuarantors = false;

        public Boolean _IsRequireBusinessCenterOnPAccounts = false;
       //Patient Portal
        public Boolean gblnPatientPortalSendActivationEmail = false;
        public Boolean gblnPatientPortalActivationEmailAlreadySent = false;
        //Patient Portal
        //API
        public Boolean gblnAPIActivation = false;
       

        #endregion "Global Declarations for Variables"

        #region "Property Procedure"
        public Boolean ShowSaveAsCopyButton { get; set; }
        public bool GBlnYesNoLab
        {
            get { return gblnYesNoLab; }
            set { gblnYesNoLab = value; }
        }

       
        //private System.Drawing.Image _iPhoto = null;
        //public System.Drawing.Image PatientPhoto
        //{
        //    get { return _iPhoto; }
        //    set { _iPhoto = value; }
        //}

        

        #endregion "Property Procedure"

        #region "Delegates"

        public delegate void SaveandCloseHandler(Int64 PatientID);
        public event SaveandCloseHandler EvntSaveandClose;

        #endregion

        Int64 nProviderAssociationID = 0;
        string sProviderTaxID = "";

        Patient oPatient = new Patient();
        PatientDemographics oPatientHistoryDemographics;
        gloPatientDemographicsControl oPatientDemographicsControl;

        gloPatient oPatientTrans = null;

        #region "Constructor"

        public frmSetupPatient(Int64 PatientID, String databaseconnectionstring)
        {
            InitializeComponent();
            _databaseconnectionstring = databaseconnectionstring;
            _PatientID = PatientID;
            ReturnPatientID = _PatientID;
            oPatientDemographicsControl = new gloPatientDemographicsControl(_PatientID, _databaseconnectionstring, _IsSaveAsCopy);
            oPatientDemographicsControl.onDemographicControl_Enter += new gloPatientDemographicsControl.onDemographicControlEnter(oPatientDemographicsControl_onDemographicControl_Enter);
            oPatientDemographicsControl.onDemographicControl_Leave += new gloPatientDemographicsControl.onDemographicControlLeave(oPatientDemographicsControl_onDemographicControl_Leave);
            oPatientDemographicsControl.Dock = DockStyle.Fill;
            oPatientTrans = new gloPatient(_databaseconnectionstring);
            
            //17-Nov-15 Aniket: Resolving Bug #90191: gloEMR: OB vitals- View OB vitals winow looses focus and does not allow to perform any operation, user has to kill application
            oPatientTrans.ParentForm = this;

            _ScannedPatient = new ScanedPatient(_databaseconnectionstring); // not necessary
            _ScanedInsurance = new ScanedInsurance(_databaseconnectionstring); // not necessary
            _FillPatientFromScan = false;

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 1; }
            }
            else
            { _ClinicID = 1; }

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageboxcaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageboxcaption = "gloPM";
                }
            }
            else
            { _messageboxcaption = "gloPM"; }

            #endregion

            #region "Get EMRConnectionString"

            if (_messageboxcaption == "gloPM")
            {
                if (appSettings["EMRConnectionString"] != null)
                { _EMRdatabaseconnectionstring = appSettings["EMRConnectionString"].ToString(); }
                else
                { _EMRdatabaseconnectionstring = ""; }

                if (appSettings["Add Patient To gloEMR"] != null)
                {
                    _AddPatientToEMR = Convert.ToBoolean(appSettings["Add Patient To gloEMR"]);
                }
                else
                {
                    _AddPatientToEMR = false;
                }
            }
            else
            {
                if (appSettings["PMConnectionString"] != null)
                { _EMRdatabaseconnectionstring = appSettings["PMConnectionString"].ToString(); }
                else
                { _EMRdatabaseconnectionstring = ""; }

                if (appSettings["Add Patient To gloPM"] != null)
                {
                    _AddPatientToEMR = Convert.ToBoolean(appSettings["Add Patient To gloPM"]);
                }
                else
                {
                    _AddPatientToEMR = false;
                }
            }
            #endregion

            #region " Retrive UserName from appSettings "

            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                { _UserID = Convert.ToInt64(appSettings["UserID"]); }
            }

            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                {
                    _UserName = Convert.ToString(appSettings["UserName"]);
                }
            }
            else
            {
                _UserName = "";
            }

            #endregion

            if (PatientID > 0)
            { ts_btnDemoHx.Visible = true; }
            else
            { ts_btnDemoHx.Visible = false; }
        }
        public frmSetupPatient(Int64 PatientID, String databaseconnectionstring, bool IsSaveAsCopy)
        {
            InitializeComponent();
            _databaseconnectionstring = databaseconnectionstring;
            _IsSaveAsCopy = IsSaveAsCopy;
            _PatientID = PatientID;
            ReturnPatientID = _PatientID;
            oPatientDemographicsControl = new gloPatientDemographicsControl(_PatientID, _databaseconnectionstring, _IsSaveAsCopy);
            oPatientDemographicsControl.onDemographicControl_Enter += new gloPatientDemographicsControl.onDemographicControlEnter(oPatientDemographicsControl_onDemographicControl_Enter);
            oPatientDemographicsControl.onDemographicControl_Leave += new gloPatientDemographicsControl.onDemographicControlLeave(oPatientDemographicsControl_onDemographicControl_Leave);

            oPatientDemographicsControl.Dock = DockStyle.Fill;
            oPatientTrans = new gloPatient(_databaseconnectionstring);

            //17-Nov-15 Aniket: Resolving Bug #90191: gloEMR: OB vitals- View OB vitals winow looses focus and does not allow to perform any operation, user has to kill application
            oPatientTrans.ParentForm = this;

            _ScannedPatient = new ScanedPatient(_databaseconnectionstring); // not necessary
            _ScanedInsurance = new ScanedInsurance(_databaseconnectionstring); // not necessary
            _FillPatientFromScan = false;


            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 1; }
            }
            else
            { _ClinicID = 1; }

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageboxcaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageboxcaption = "gloPM";
                }
            }
            else
            { _messageboxcaption = "gloPM"; }

            #endregion

            #region "Get EMRConnectionString"

            if (_messageboxcaption == "gloPM")
            {
                if (appSettings["EMRConnectionString"] != null)
                { _EMRdatabaseconnectionstring = appSettings["EMRConnectionString"].ToString(); }
                else
                { _EMRdatabaseconnectionstring = ""; }

                if (appSettings["Add Patient To gloEMR"] != null)
                {
                    _AddPatientToEMR = Convert.ToBoolean(appSettings["Add Patient To gloEMR"]);
                }
                else
                {
                    _AddPatientToEMR = false;
                }
            }
            else
            {
                if (appSettings["PMConnectionString"] != null)
                { _EMRdatabaseconnectionstring = appSettings["PMConnectionString"].ToString(); }
                else
                { _EMRdatabaseconnectionstring = ""; }

                if (appSettings["Add Patient To gloPM"] != null)
                {
                    _AddPatientToEMR = Convert.ToBoolean(appSettings["Add Patient To gloPM"]);
                }
                else
                {
                    _AddPatientToEMR = false;
                }
            }
            #endregion

            #region " Retrive UserName from appSettings "

            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                {
                    _UserName = Convert.ToString(appSettings["UserName"]);
                }
            }
            else
            {
                _UserName = "";
            }

            #endregion

            if (PatientID > 0)
            { ts_btnDemoHx.Visible = true; }
            else
            { ts_btnDemoHx.Visible = false; }

        }
        public frmSetupPatient(Int64 PatientID, ModifyPatientDetailType oModificationDetail, String databaseconnectionstring)
        {
            InitializeComponent();
            _databaseconnectionstring = databaseconnectionstring;
            _PatientID = PatientID;
            ReturnPatientID = _PatientID;
            oPatientDemographicsControl = new gloPatientDemographicsControl(_PatientID, _databaseconnectionstring, _IsSaveAsCopy);
            oPatientDemographicsControl.onDemographicControl_Enter += new gloPatientDemographicsControl.onDemographicControlEnter(oPatientDemographicsControl_onDemographicControl_Enter);
            oPatientDemographicsControl.onDemographicControl_Leave += new gloPatientDemographicsControl.onDemographicControlLeave(oPatientDemographicsControl_onDemographicControl_Leave);
            oPatientDemographicsControl.ModificationDetail = oModificationDetail;
            oPatientDemographicsControl.Dock = DockStyle.Fill;
            oPatientTrans = new gloPatient(_databaseconnectionstring);
            _ScannedPatient = new ScanedPatient(_databaseconnectionstring); // not necessary
            _ScanedInsurance = new ScanedInsurance(_databaseconnectionstring); // not necessary
            _FillPatientFromScan = false;


            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 1; }
            }
            else
            { _ClinicID = 1; }

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageboxcaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageboxcaption = "gloPM";
                }
            }
            else
            { _messageboxcaption = "gloPM"; }

            #endregion

            #region "Get EMRConnectionString"

            if (_messageboxcaption == "gloPM")
            {
                if (appSettings["EMRConnectionString"] != null)
                { _EMRdatabaseconnectionstring = appSettings["EMRConnectionString"].ToString(); }
                else
                { _EMRdatabaseconnectionstring = ""; }

                if (appSettings["Add Patient To gloEMR"] != null)
                {
                    _AddPatientToEMR = Convert.ToBoolean(appSettings["Add Patient To gloEMR"]);
                }
                else
                {
                    _AddPatientToEMR = false;
                }
            }
            else
            {
                if (appSettings["PMConnectionString"] != null)
                { _EMRdatabaseconnectionstring = appSettings["PMConnectionString"].ToString(); }
                else
                { _EMRdatabaseconnectionstring = ""; }

                if (appSettings["Add Patient To gloPM"] != null)
                {
                    _AddPatientToEMR = Convert.ToBoolean(appSettings["Add Patient To gloPM"]);
                }
                else
                {
                    _AddPatientToEMR = false;
                }
            }
            #endregion

            #region " Retrive UserName from appSettings "

            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                {
                    _UserName = Convert.ToString(appSettings["UserName"]);
                }
            }
            else
            {
                _UserName = "";
            }

            #endregion
        }

        public frmSetupPatient(String databaseconnectionstring, ScanedPatient oScannedPatient, bool FillPatientFromScan)
        {
            InitializeComponent();
            _databaseconnectionstring = databaseconnectionstring;
            _PatientID = 0;
            ReturnPatientID = _PatientID;
            oPatientDemographicsControl = new gloPatientDemographicsControl(_PatientID, _databaseconnectionstring, _IsSaveAsCopy);
            oPatientDemographicsControl.onDemographicControl_Enter += new gloPatientDemographicsControl.onDemographicControlEnter(oPatientDemographicsControl_onDemographicControl_Enter);
            oPatientDemographicsControl.onDemographicControl_Leave += new gloPatientDemographicsControl.onDemographicControlLeave(oPatientDemographicsControl_onDemographicControl_Leave);
            oPatientDemographicsControl.Dock = DockStyle.Fill;
            oPatientTrans = new gloPatient(_databaseconnectionstring);
            _ScannedPatient = new ScanedPatient(_databaseconnectionstring);
            _ScanedInsurance = new ScanedInsurance(_databaseconnectionstring);
            _ScannedPatient = oScannedPatient;
            _FillPatientFromScan = FillPatientFromScan;

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 1; }
            }
            else
            { _ClinicID = 1; }

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageboxcaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageboxcaption = "gloPM";
                }
            }
            else
            { _messageboxcaption = "gloPM"; }

            #endregion

            #region "Get EMRConnectionString"

            if (_messageboxcaption == "gloPM")
            {
                if (appSettings["EMRConnectionString"] != null)
                { _EMRdatabaseconnectionstring = appSettings["EMRConnectionString"].ToString(); }
                else
                { _EMRdatabaseconnectionstring = ""; }

                if (appSettings["Add Patient To gloEMR"] != null)
                {
                    _AddPatientToEMR = Convert.ToBoolean(appSettings["Add Patient To gloEMR"]);
                }
                else
                {
                    _AddPatientToEMR = false;
                }
            }
            else
            {
                if (appSettings["PMConnectionString"] != null)
                { _EMRdatabaseconnectionstring = appSettings["PMConnectionString"].ToString(); }
                else
                { _EMRdatabaseconnectionstring = ""; }

                if (appSettings["Add Patient To gloPM"] != null)
                {
                    _AddPatientToEMR = Convert.ToBoolean(appSettings["Add Patient To gloPM"]);
                }
                else
                {
                    _AddPatientToEMR = false;
                }
            }
            #endregion

            #region " Retrive UserName from appSettings "

            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                {
                    _UserName = Convert.ToString(appSettings["UserName"]);
                }
            }
            else
            {
                _UserName = "";
            }

            #endregion
        }

        public frmSetupPatient(String databaseconnectionstring, ScanedPatient oScannedPatient, ScanedInsurance oScannedInsurance, bool FillPatientFromScan)
        {
            InitializeComponent();
            _databaseconnectionstring = databaseconnectionstring;
            _PatientID = 0;
            ReturnPatientID = _PatientID;
            oPatientDemographicsControl = new gloPatientDemographicsControl(_PatientID, _databaseconnectionstring, _IsSaveAsCopy);
            oPatientDemographicsControl.onDemographicControl_Enter += new gloPatientDemographicsControl.onDemographicControlEnter(oPatientDemographicsControl_onDemographicControl_Enter);
            oPatientDemographicsControl.onDemographicControl_Leave += new gloPatientDemographicsControl.onDemographicControlLeave(oPatientDemographicsControl_onDemographicControl_Leave);
            oPatientDemographicsControl.Dock = DockStyle.Fill;
            oPatientTrans = new gloPatient(_databaseconnectionstring);
            _ScannedPatient = new ScanedPatient(_databaseconnectionstring);
            _ScanedInsurance = new ScanedInsurance(_databaseconnectionstring);
            _ScannedPatient = oScannedPatient;
            _ScanedInsurance = oScannedInsurance;
            _FillPatientFromScan = FillPatientFromScan;


            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 1; }
            }
            else
            { _ClinicID = 1; }


            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageboxcaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageboxcaption = "gloPM";
                }
            }
            else
            { _messageboxcaption = "gloPM"; }

            #endregion

            #region "Get EMRConnectionString"

            if (_messageboxcaption == "gloPM")
            {
                if (appSettings["EMRConnectionString"] != null)
                { _EMRdatabaseconnectionstring = appSettings["EMRConnectionString"].ToString(); }
                else
                { _EMRdatabaseconnectionstring = ""; }

                if (appSettings["Add Patient To gloEMR"] != null)
                {
                    _AddPatientToEMR = Convert.ToBoolean(appSettings["Add Patient To gloEMR"]);
                }
                else
                {
                    _AddPatientToEMR = false;
                }
            }
            else
            {
                if (appSettings["PMConnectionString"] != null)
                { _EMRdatabaseconnectionstring = appSettings["PMConnectionString"].ToString(); }
                else
                { _EMRdatabaseconnectionstring = ""; }

                if (appSettings["Add Patient To gloPM"] != null)
                {
                    _AddPatientToEMR = Convert.ToBoolean(appSettings["Add Patient To gloPM"]);
                }
                else
                {
                    _AddPatientToEMR = false;
                }
            }
            #endregion

            #region " Retrive UserName from appSettings "

            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                {
                    _UserName = Convert.ToString(appSettings["UserName"]);
                }
            }
            else
            {
                _UserName = "";
            }

            #endregion
        }

        #endregion "Constructor"

        //Bug #62022: 00000606 : EMR PATIENT RELATED LIQUID LINKS CREATE FALSE WARNING WHEN TRIPLE-CLICKED 
        //added form level locking for patient registration.
        #region"added form level locking for patient registration"
        
        public string _MachineName = System.Windows.Forms.SystemInformation.ComputerName;

        private void Scan_n_Unlock_FormLevel(Int64 nFormLevelID, Int64 nPatientID)
        {
            SqlConnection con = new SqlConnection(_databaseconnectionstring);
            SqlCommand cmd = null;
            SqlParameter sqlParam=null;
            try
            {
                cmd = new SqlCommand("Delete_Lock_FormLevel", con);
                cmd.CommandType = CommandType.StoredProcedure;

              

                sqlParam = cmd.Parameters.Add("@FormLockingID", SqlDbType.BigInt);
                sqlParam.Direction = ParameterDirection.Input;
                sqlParam.Value = nFormLevelID;

                sqlParam = cmd.Parameters.Add("@PatientID", SqlDbType.BigInt);
                sqlParam.Direction = ParameterDirection.Input;
                sqlParam.Value = nPatientID;

                sqlParam = cmd.Parameters.Add("@ProcessID", SqlDbType.BigInt);
                sqlParam.Direction = ParameterDirection.Input;
                sqlParam.Value = System.Diagnostics.Process.GetCurrentProcess().Id;

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Dispose();
                con = null;
                if (cmd != null)
                {
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                    cmd = null;
                }
                if (sqlParam != null)
                {
                    sqlParam = null;
                }
            }
        }
        
        private DataTable Scan_n_Lock_FormLevel(Int64 nPatientID, Int64 nVisitID, Int64 nTransactionID, string sFormName)
        {
            DataTable dt=new DataTable();
            SqlConnection con = new SqlConnection(_databaseconnectionstring);
            SqlCommand cmd = null;
            SqlParameter sqlParam=null;
            SqlParameter sqlparamLockingID = new SqlParameter();
            try
            {
                cmd = new SqlCommand("Scan_n_Lock_FormLevel", con);
                cmd.CommandType = CommandType.StoredProcedure;

                sqlparamLockingID = cmd.Parameters.Add("@FormLockingID", SqlDbType.BigInt);
                sqlparamLockingID.Direction = ParameterDirection.InputOutput;
                sqlparamLockingID.Value = 0;

                sqlParam = cmd.Parameters.Add("@PatientID", SqlDbType.BigInt);
                sqlParam.Direction = ParameterDirection.Input;
                sqlParam.Value = nPatientID;

                sqlParam = cmd.Parameters.Add("@VisitID", SqlDbType.BigInt);
                sqlParam.Direction = ParameterDirection.Input;
                sqlParam.Value = nVisitID;

                sqlParam = cmd.Parameters.Add("@TransactionID", SqlDbType.BigInt);
                sqlParam.Direction = ParameterDirection.Input;
                sqlParam.Value = nTransactionID;

                sqlParam = cmd.Parameters.Add("@FormName", SqlDbType.VarChar);
                sqlParam.Direction = ParameterDirection.Input;
                sqlParam.Value = sFormName;

                sqlParam = cmd.Parameters.Add("@ProcessID", SqlDbType.BigInt);
                sqlParam.Direction = ParameterDirection.Input;
                sqlParam.Value = System.Diagnostics.Process.GetCurrentProcess().Id;

                sqlParam = cmd.Parameters.Add("@TmpFormLocks", SqlDbType.Structured);
                sqlParam.Direction = ParameterDirection.Input;
                sqlParam.Value = getAllInstances(sFormName);

                sqlParam = cmd.Parameters.Add("@MachineName", SqlDbType.VarChar, 50);
                sqlParam.Direction = ParameterDirection.Input;
                sqlParam.Value = _MachineName;

                sqlParam = cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 50);
                sqlParam.Direction = ParameterDirection.Input;
                sqlParam.Value = _UserName;
                
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                da.Dispose();
                da = null;
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
            
                }
                con.Dispose();
                con = null;
                if (cmd != null)
                {
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                    cmd = null;
                }
                if (sqlParam != null)
                {
                    sqlParam = null;
                }
                if (sqlparamLockingID != null)
                {
                    sqlparamLockingID = null;
                }
            }
            return dt;
        }

        private DataTable getAllInstances(string sFormName)
        {
            DataTable dtTemp = new DataTable("Processes");
            try
            {
                DataColumn dcProcessID = new DataColumn("ProcessID");
                dcProcessID.DataType = typeof(Int64);

                dtTemp.Columns.Add(dcProcessID);

              //  Int32 currentSessionID=System.Diagnostics.Process.GetCurrentProcess().SessionId;
                string currentProcessName=System.Diagnostics.Process.GetCurrentProcess().ProcessName;
               // Int32 currentProcessID=System.Diagnostics.Process.GetCurrentProcess().Id;

                System.Diagnostics.Process[] _process=System.Diagnostics.Process.GetProcessesByName(currentProcessName);

                foreach (System.Diagnostics.Process _proc in _process)
	            {
                    DataRow dr = dtTemp.NewRow();
                    dr[dcProcessID]=_proc.Id;
                    dtTemp.Rows.Add(dr);
	            }
                return dtTemp;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK);
            }
            finally
            {
                //if (dtTemp!=null)
                //{
                //    dtTemp.Dispose();
                //    dtTemp=null;
                //}
            }
            return dtTemp;
        }

        private bool formLock = false;
        private Int64 LockID = 0;
        private bool lockform(Int64 _patientID)
        {
            bool _result = false;
            try
            {
                if (formLock == false)
                {
                    DataTable _dt = null;
                    _dt = Scan_n_Lock_FormLevel(_PatientID, 0, 0, "Patient Registration");
                    if (_dt != null)
                    {
                        if (Convert.ToInt64(_dt.Rows[0]["IsOpen"]) == 1)
                        {
                            formLock = true;
                            if (MessageBox.Show("This Patient is being modified by " + Convert.ToString(_dt.Rows[0]["UserName"]) + " on " + Convert.ToString(_dt.Rows[0]["MachineName"]) + ". You cannot modify this patient now. Do you want to open it?", _messageboxcaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                _result = true;
                            }
                            else
                            {
                                if (LockID != 0)
                                {
                                    Scan_n_Unlock_FormLevel(LockID, _patientID);
                                }
                                _result = false;
                            }
                        }
                        else
                        {
                            formLock = false;
                            LockID = Convert.ToInt64(_dt.Rows[0]["FormLevelID"]);
                            _result = true;
                        }
                        _dt.Dispose();
                        _dt = null;
                    }
                    else
                    {
                        _result = false;
                    }
                }
            }
            catch (Exception)
            {
                _result= false;
            }
            return _result;
        }
        #endregion

        #region "Form Load Event"
        private void closeWebcam(bool bCaptureAndClose)
        {
            oPatientDemographicsControl.closeWebcam(bCaptureAndClose); 
        }



        private void AddrecentPatient(Int64 PatID, Int64 UserID, ref Hashtable hashtbl)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oPara = null;
            string _ProcName;
            Int64 deletedpatid = -1;
            try
            {

                // 'Retrieve the Image for Patient
                // _strSQL = "select iphoto,sGender from patient where npatientid=" & PatientID
                _ProcName = "gsp_InsertRecentPatientUserwise";
                oDB.Connect(false);

                oPara = new gloDatabaseLayer.DBParameters();
                oPara.Add("@UserID", UserID, ParameterDirection.Input, SqlDbType.BigInt);
                oPara.Add("@PatientID", PatID, ParameterDirection.Input, SqlDbType.BigInt);



                deletedpatid = Convert.ToInt64(oDB.ExecuteScalar(_ProcName, oPara));

                if ((deletedpatid != -1))
                {
                    if ((hashtbl.Contains(deletedpatid)))
                        hashtbl.Remove(deletedpatid);
                }
            }

            catch
            {
            }
            finally
            {
                if (oDB != null)
                {

                    oDB.Dispose();
                    oDB = null/* TODO Change to default(_) if this is not a reference type */;
                }
                if (oPara != null)
                {
                    oPara.Clear();
                }
            }
        }


        private void frmSetupPatient_Load(object sender, EventArgs e)
        {
            gloAccount objgloAccount = null;
            try
            {
                changeHeightAsPerResolution();
                //if (Screen.PrimaryScreen.Bounds.Height < 900)
                //{
                //    this.Size = new System.Drawing.Size(810, 700);
                //    pnlContainer.AutoScroll = true;
                //}
                //else
                //{
                //    pnlContainer.AutoScroll = false;
                //}
                if (_PatientID != 0)
                {
                    Object obj = hshPatData[_PatientID];
                    if (obj == null)
                    {
                        hshPatData.Add(_PatientID, gloGlobal.gloPMGlobal.UserID);
                        AddrecentPatient(_PatientID, gloGlobal.gloPMGlobal.UserID, ref hshPatData);
                    }
                }
                //double DesignScreenWidth = Convert.ToDouble(1280);
                //double DesignScreenHeight = Convert.ToDouble(1024);
                //double CurrentScreenWidth = Convert.ToDouble(Screen.PrimaryScreen.Bounds.Width);
                //double CurrentScreenHeight = Convert.ToDouble(Screen.PrimaryScreen.Bounds.Height);
                //double RatioX = CurrentScreenWidth / DesignScreenWidth;
                //float RatioY = Convert.ToSingle(CurrentScreenHeight / DesignScreenHeight);
                ////Me.Width = Me.Width * RatioX
                //this.Height = Convert.ToInt32(this.Height * RatioY);
                //this.Top = Convert.ToInt32(this.Top * RatioX);
                //this.Left = Convert.ToInt32(this.Left * RatioY);
                
                //this.AutoScroll = true;
                //pnlContainer.AutoScroll = true;
                //oPatientDemographicsControl.AutoScroll = true;
                
                //if (RatioY != 1)
                //{
                //    foreach (Control cnt in oPatientDemographicsControl.Controls)
                //    {
                //        if (cnt.Name.Contains("pnl"))
                //        {
                //            cnt.Height = Convert.ToInt32(cnt.Height * RatioY);
                //        }
                //    }
                //}




                //Bug #62022: 00000606 : EMR PATIENT RELATED LIQUID LINKS CREATE FALSE WARNING WHEN TRIPLE-CLICKED 
                //added form level locking for patient registration.
                
                //Bug #62857: gloEMR - New Patient - Application unnecessary display modify patient message and does not allow to save new patient 
                //Added condition for message while saving new patient.
                if (_PatientID != 0)
                {
                    if (lockform(_PatientID))
                    {
                        if (formLock == true)
                        {
                            tsb_OK.Enabled = false;
                            tsb_Print.Enabled = false;
                        }
                    }
                    else
                    {
                        this.Close();
                        return;
                    }
                }

                if (appSettings["MessageBOXCaption"] != null)
                {
                    if (appSettings["MessageBOXCaption"] != "")
                    {
                        _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                    }
                    else
                    {
                        _MessageBoxCaption = "gloPM";
                    }
                }
                else
                { _MessageBoxCaption = "gloPM"; }

              
                objgloAccount = new gloAccount(_databaseconnectionstring);
                _IsPatientAccountFeature = objgloAccount.GetPatientAccountFeatureSetting();
                _IsAllowMultipleGuarantors = objgloAccount.GetAllowMultipleGuarantorsFeatureSetting();
                _IsRequireBusinessCenterOnPAccounts = gloGlobal.gloPMGlobal.GetBusinessCenterSettings("BusinessCenter_PatientAccount");
                oPatientDemographicsControl._IsPatientAccountFeature = _IsPatientAccountFeature;
                oPatientDemographicsControl._IsAllowMultipleGuarantors = _IsAllowMultipleGuarantors;
                oPatientDemographicsControl._IsRequireBusinessCenterOnPAccounts = _IsRequireBusinessCenterOnPAccounts;

                //Patient Portal
                IsPatientPortalEnabled();
                oPatientDemographicsControl.gblnPatientPortalEnabled = blnPatientPortalEnabled;
                //oPatientDemographicsControl.SetPatientPortalEmailFacility(_IsSaveAsCopy);

                //Patient Portal

                if (_PatientID == 0)
                {
                    this.Text = "New Patient";
                    this.Icon = Properties.Resources.New_Patient;

                    object oResult;
                    gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
                    ogloSettings.GetSetting("UseSitePrefix", out oResult);
                    ogloSettings.Dispose();
                    ogloSettings = null;
                    Int32 _UseSitePrefix = 0;
                    if (oResult != null && oResult.ToString() != "")
                    {
                        _UseSitePrefix = Convert.ToInt32(oResult);
                    }

                    if (_UseSitePrefix != 0)
                    {
                        if (appSettings["PatientPrefix"] != null)
                        {
                            if (appSettings["PatientPrefix"] != "")
                            {
                                oPatientDemographicsControl.txtPatientPrefix.Text = Convert.ToString(appSettings["PatientPrefix"]);
                                oPatientDemographicsControl.txtPACode.Text = Convert.ToString(appSettings["PatientPrefix"]);
                            }
                        }
                        if (oPatientDemographicsControl.txtPatientPrefix.Text.Trim() == "")
                        {
                            DataTable dtPrefix = null;
                            gloPatient oPatient = new gloPatient(_databaseconnectionstring);
                            dtPrefix = oPatient.GetPrefix();
                            if (dtPrefix != null)
                            {
                                if (dtPrefix.Rows.Count > 0)
                                {
                                    oPatientDemographicsControl.txtPatientPrefix.Text = Convert.ToString(dtPrefix.Rows[0]["sPreFix"]);
                                    oPatientDemographicsControl.txtPACode.Text = Convert.ToString(dtPrefix.Rows[0]["sPreFix"]);
                                    appSettings["PatientPrefix"] = Convert.ToString(dtPrefix.Rows[0]["sPreFix"]);
                                }
                            }
                            if (dtPrefix != null) { dtPrefix.Dispose(); }
                            if (oPatient != null) { oPatient.Dispose(); }
                        }
                        if (oPatientDemographicsControl.txtPatientPrefix.Text.Trim() == "")
                        {
                            MessageBox.Show("Site Prefix is not set up for this site. Please contact your administrator to set up the site prefix before registering  patient.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Opacity = 0;
                            isSaveAndClose = true;
                            this.Close();
                            return;
                        }
                    }
                    else
                    {
                        oPatientDemographicsControl.txtPatientPrefix.Text = "";
                        oPatientDemographicsControl.txtPACode.Mask = "AAAAAAAAAAAAA";
                    }
                    oPatientDemographicsControl.GBlnYesNoLab = gblnYesNoLab;
                    pnlContainer.Controls.Add(oPatientDemographicsControl);

                }
                else
                {
                    if (_IsSaveAsCopy == false)
                    {
                        this.Text = "Modify Patient";
                        this.Icon = Properties.Resources.Modify_Patient;
                    }
                    else
                    {
                        this.Icon = Properties.Resources.Save_as_Copy;
                        ts_btnDemoHx.Enabled = false;

                        //..Code added by Sagar Ghodke on Sep. 01 2011 - Version : 6040 implementation
                        //..Code added to solve the copy patient - account save issue
                        //..Ref. Bug# 8431
                        //..Disabled the Print functionality when the Patient Registration is under "SaveAsCopy" mode
                        tsb_Print.Enabled = false;
                        //..End code - Sagar Ghodke on Sep. 01 2011 - Version : 6040 implementation

                    }
                    if (oPatient != null)
                    {
                        oPatient.Dispose();
                        oPatient = null;
                    }
                    oPatient = oPatientTrans.GetPatient(_PatientID);
                    oPatientDemographicsControl.PatientDemographicsDetails = oPatient.DemographicsDetail;
                    oPatientDemographicsControl.PatientGuardianDetails = oPatient.GuardianDetail;
                    oPatientDemographicsControl.PatientOccupationDetails = oPatient.OccupationDetail;
                    oPatientDemographicsControl.PatientInsuranceDetails = oPatient.InsuranceDetails;
                    oPatientDemographicsControl.PatientDemographicOtherInfo = oPatient.PatientDemographicOtherInfo;
                    oPatientDemographicsControl.PatientPharmacies = oPatient.PatientPharmacies;
                    oPatientDemographicsControl.PatientReferrals = oPatient.PatientReferrals;
                    oPatientDemographicsControl.PatientCareTeam = oPatient.PatientCareTeam;
                    oPatientDemographicsControl.PrimaryCarePhysicians = oPatient.PrimaryCarePhysicians;
                    oPatientDemographicsControl.PatientWorkersComps = oPatient.PatientWorkersComp;

                    oPatientDemographicsControl.PatientGuarantors = oPatient.PatientGuarantors;

                    oPatientDemographicsControl.PatientOtherGuarantors = oPatient.PatientOtherGuarantors;
                    oPatientDemographicsControl.PatientRepresentatives = oPatient.PatientRepresentatives;
                    oPatientDemographicsControl.PatientPortalAccount = oPatient.PatientPortalAccount;
                    oPatientDemographicsControl.APIRepresentatives = oPatient.APIRepresentatives;
                    oPatientDemographicsControl.APIAccount = oPatient.APIAccount;
                    oPatientDemographicsControl.PatientAccounts = oPatient.PatientAccounts;

                    object oResult;
                    object oResultAllowEditablePatientCode;
                    gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
                    ogloSettings.GetSetting("Auto-Generate Patient Code", out oResult);
                    ogloSettings.GetSetting("Allow-Editable Patient Code", out oResultAllowEditablePatientCode);
                    ogloSettings.Dispose();
                    ogloSettings = null;
                    Int32 _AutoGenerate = 0;
                    Int32 _AllowEditablePatientCode = 0;
                    if (oResult != null && oResult.ToString() != "" && oResultAllowEditablePatientCode != null && oResultAllowEditablePatientCode.ToString() != "")
                    {
                        _AutoGenerate = Convert.ToInt32(oResult);
                        _AllowEditablePatientCode = Convert.ToInt32(oResultAllowEditablePatientCode);
                        if (_AutoGenerate != 0 && _AllowEditablePatientCode == 0) //Auto generate is true
                        {
                            oPatientDemographicsControl.txtPACode.Enabled = false;
                        }
                        else if (_AutoGenerate != 0 || _AllowEditablePatientCode != 0)
                        {
                            oPatientDemographicsControl.txtPACode.Enabled = true;
                        }
                        else
                        {
                            oPatientDemographicsControl.txtPACode.Enabled = true;
                        }
                    }
                    if (_AllowEditablePatientCode != 0 && _IsSaveAsCopy == false)
                    {
                        tsb_SaveAsCopy.Visible = true;
                        if (ShowSaveAsCopyButton == true)
                        {
                            tsb_SaveAsCopy.Visible = true;

                        }
                        else
                        {
                            tsb_SaveAsCopy.Visible = false;
                        }
                    }

                    if (_AllowEditablePatientCode != 0 && _IsSaveAsCopy == false)
                    {
                        tsb_SaveAsCopy.Visible = true;
                        if (ShowSaveAsCopyButton == true)
                        {
                            tsb_SaveAsCopy.Visible = true;

                        }
                        else
                        {
                            tsb_SaveAsCopy.Visible = false;
                        }
                    }

                    if (oPatientDemographicsControl.PatientDemographicsDetails.PatientPrefix.Trim() != "")
                    {
                        oPatientDemographicsControl.txtPACode.Mask = "AAA-AAAAAAAAAA";
                    }
                    else
                    {
                        oPatientDemographicsControl.txtPACode.Mask = "AAAAAAAAAAAAA";

                    }
                    oPatientDemographicsControl.GBlnYesNoLab = gblnYesNoLab;
                    pnlContainer.Controls.Add(oPatientDemographicsControl);

                    if (oPatientHistoryDemographics != null)
                    {
                        oPatientHistoryDemographics.Dispose();
                        oPatientHistoryDemographics = null;
                    }
                    oPatientHistoryDemographics = new PatientDemographics();
                    oPatientHistoryDemographics.PatientFirstName = oPatient.DemographicsDetail.PatientFirstName;
                    oPatientHistoryDemographics.PatientMiddleName = oPatient.DemographicsDetail.PatientMiddleName;
                    oPatientHistoryDemographics.PatientLastName = oPatient.DemographicsDetail.PatientLastName;
                    oPatientHistoryDemographics.PatientGender = oPatient.DemographicsDetail.PatientGender;
                    oPatientHistoryDemographics.PatientDOB = oPatient.DemographicsDetail.PatientDOB;
                    oPatientHistoryDemographics.PatientAddress1 = oPatient.DemographicsDetail.PatientAddress1;
                    oPatientHistoryDemographics.PatientAddress2 = oPatient.DemographicsDetail.PatientAddress2;
                    oPatientHistoryDemographics.PatientCity = oPatient.DemographicsDetail.PatientCity;
                    oPatientHistoryDemographics.PatientState = oPatient.DemographicsDetail.PatientState;
                    oPatientHistoryDemographics.PatientZip = oPatient.DemographicsDetail.PatientZip;
                    oPatientHistoryDemographics.PatientCounty = oPatient.DemographicsDetail.PatientCounty;
                    oPatientHistoryDemographics.PatientPhone = oPatient.DemographicsDetail.PatientPhone;
                    oPatientHistoryDemographics.PatientLanguage = oPatient.DemographicsDetail.PatientLanguage;
                    oPatientHistoryDemographics.PatientMaritalStatus = oPatient.DemographicsDetail.PatientMaritalStatus;
                    oPatientHistoryDemographics.PatientCommunicationPrefence = oPatient.DemographicsDetail.PatientCommunicationPrefence;
                }

                //Patient Portal
                //IsPatientPortalEnabled();
                //getPatientPortalSettings();
                //oPatientDemographicsControl.gblnPatientPortalEnabled = blnPatientPortalEnabled;
                oPatientDemographicsControl.SetPatientPortalEmailFacility(_IsSaveAsCopy);

                //Patient Portal


                gloSettings.DatabaseSetting.DataBaseSetting oSettings = new gloSettings.DatabaseSetting.DataBaseSetting();
                string sVal = oSettings.ReadSettings_XML("CardScannerSettings", "ScanPatientPhoto");
                if (sVal == "1")
                {
                    tsb_ScanPatient.Visible = true;
                }
                else
                {
                    tsb_ScanPatient.Visible = false;
                }
                oSettings.Dispose();
                // License Check
                List<object> _ToolStrip = new List<object>();
                _ToolStrip.Add(this.tsb_OK);
                _ToolStrip.Add(this.tsb_SaveAsCopy);
                base.FormControls = null;
                base.FormControls = _ToolStrip.ToArray();
                base.SetChildFormControls();
                _ToolStrip = null;
                // end License Check
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Patient, ActivityCategory.SetupPatient, ActivityType.Modify, "Add/Modify Patient", _PatientID, 0, 0, ActivityOutCome.Failure);
            }
            finally
            {
                if (objgloAccount != null) { objgloAccount.Dispose(); }

            }
        }
        private void changeHeightAsPerResolution()
        {
            Int32 myScreenHeight = Convert.ToInt32(Screen.PrimaryScreen.WorkingArea.Height * 0.99);
            if (myScreenHeight < this.Height)
            {
                this.Height = myScreenHeight;
                Int32 myScreenWidth = Convert.ToInt32(Screen.PrimaryScreen.WorkingArea.Width * 0.63);
                if (myScreenWidth > this.Width)
                {
                    this.Width = myScreenWidth;
                }
            }
            
        }

        #endregion "Form Load Event"

        #region "Main Tool Strip Events "

        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
             
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "OK":
                        {
                            closeWebcam(true);
                            SavePatientDetails();
                           
                        }
                        break;

                    case "Cancel":
                        {
                            closeWebcam(false);
                            this.Close();

                        }
                        break;

                    case "Print":
                        {
                            
                            DialogResult res = MessageBox.Show("The patient record needs to be saved before printing. Do you want to continue?", _messageboxcaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            if (res == DialogResult.Yes)
                            {
                                if (SavePatient() == true)
                                {
                                    closeWebcam(true);
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.SetupPatient, gloAuditTrail.ActivityType.Print, "Patient details printed", _PatientID, 0, 0, ActivityOutCome.Success, SoftwareComponent.gloEMR);
                                    PrintPatientRegistration();
                                    if (EvntSaveandClose != null)
                                        EvntSaveandClose(ReturnPatientID);

                                }
                                else
                                {
                                    closeWebcam(false);
                                }
                            }
                            else
                            {
                                return;
                            }
                        }
                        break;

                    case "DemoHx":
                        closeWebcam(false);
                        if (_PatientID > 0)
                        {

                            frmPatientChangeHistory ofrmHistory = new frmPatientChangeHistory(_PatientID);
                            ofrmHistory.ShowDialog(this);
                            ofrmHistory.Dispose();
                        }
                        break;

                    case "ScanPatient":
                        closeWebcam(false);
                        {
                            if (_PatientID == 0)
                            {
                                frmScanCard_New oCardScanning = new frmScanCard_New(_PatientID, _databaseconnectionstring, "New");
                                oCardScanning.PatientCode = oPatientDemographicsControl.txtPACode.Text;
                                oCardScanning.ShowDialog(this);

                                if (oCardScanning.oDialogResult)
                                {
                                    oPatientDemographicsControl.GetFirstName = oCardScanning.FirstName;
                                    oPatientDemographicsControl.GetLastName = oCardScanning.LastName;
                                    oPatientDemographicsControl.GetMIName = oCardScanning.MiddleName;
                                    oPatientDemographicsControl.GetDOB = gloDateMaster.gloDate.DateAsDateString(oCardScanning.DOB);
                                    oPatientDemographicsControl.GetAddress1 = oCardScanning.Address1;
                                    oPatientDemographicsControl.GetAddress2 = oCardScanning.Address2;
                                    oPatientDemographicsControl.GetCity = oCardScanning.City;
                                    oPatientDemographicsControl.GetCounty = oCardScanning.County;
                                    oPatientDemographicsControl.flgOCR = true;
                                    oPatientDemographicsControl.GetZip = oCardScanning.Zip;
                                    oPatientDemographicsControl.flgOCR = false;
                                    oPatientDemographicsControl.GetGender = oCardScanning.Sex;
                                    oPatientDemographicsControl.GetState = oCardScanning.State;
                                    oPatientDemographicsControl.GetCountry = oCardScanning.CountryShort;

                                    if (oCardScanning.PhotoImage != null)
                                    {
                                        oPatientDemographicsControl.GetPatPhoto = (Image)(oCardScanning.PhotoImage.Clone());
                                    }
                                    if (oCardScanning.cardFrontImage != null)
                                    {
                                        oPatientDemographicsControl.GetcardFrontImage = (Image)(oCardScanning.cardFrontImage.Clone());
                                    }
                                    if (oCardScanning.cardBackImage != null)
                                    {
                                        oPatientDemographicsControl.GetcardBackImage = (Image)(oCardScanning.cardBackImage.Clone());
                                    }

                                    oPatientDemographicsControl.GetSameAsPatientGuarantor();
                                }
                                if (oCardScanning != null)
                                {
                                    oCardScanning.DisposeCardImages();
                                    oCardScanning.Dispose();
                                    oCardScanning = null;
                                }
                            }
                            else
                            {

                                frmScanCard_New oCardScanning = new frmScanCard_New(_PatientID, _databaseconnectionstring, "Modify");
                                oCardScanning._ErrorMessage = "";
                                
                                oCardScanning.PatientCode = oPatientDemographicsControl.txtPACode.Text;
                                oCardScanning.FirstName = oPatientDemographicsControl.GetFirstName;
                                oCardScanning.LastName = oPatientDemographicsControl.GetLastName;
                                oCardScanning.MiddleName = oPatientDemographicsControl.GetMIName;
                                if (gloDateMaster.gloDate.IsValidDate(oPatientDemographicsControl.GetDOB))
                                {
                                    oCardScanning.DOB = gloDateMaster.gloDate.DateAsNumber(oPatientDemographicsControl.GetDOB);
                                }
                                oCardScanning.Address1 = oPatientDemographicsControl.GetAddress1;
                                oCardScanning.Address2 = oPatientDemographicsControl.GetAddress2;
                                oCardScanning.City = oPatientDemographicsControl.GetCity;
                                oCardScanning.County = oPatientDemographicsControl.GetCounty;
                                oCardScanning.Zip = oPatientDemographicsControl.GetZip;
                                oCardScanning.Sex = oPatientDemographicsControl.GetGender;
                                oCardScanning.State = oPatientDemographicsControl.GetState;
                                oCardScanning.CountryShort = oPatientDemographicsControl.GetCountry;
                                if (oCardScanning.PhotoImage != null)
                                {
                                    oCardScanning.PhotoImage = oPatientDemographicsControl.GetPatPhoto ;
                                }
                                if (oCardScanning.cardFrontImage != null)
                                {
                                    oCardScanning.cardFrontImage = oPatientDemographicsControl.GetcardFrontImage;
                                }
                                if (oCardScanning.cardBackImage != null)
                                {
                                    oCardScanning.cardBackImage = oPatientDemographicsControl.GetcardBackImage;
                                }


                                oCardScanning.ShowDialog(this);
                                if (oCardScanning.oDialogResult)
                                {
                                    oPatientDemographicsControl.GetFirstName = oCardScanning.FirstName;
                                    oPatientDemographicsControl.GetLastName = oCardScanning.LastName;
                                    oPatientDemographicsControl.GetMIName = oCardScanning.MiddleName;
                                    oPatientDemographicsControl.GetDOB = gloDateMaster.gloDate.DateAsDateString(oCardScanning.DOB);
                                    oPatientDemographicsControl.GetAddress1 = oCardScanning.Address1;
                                    oPatientDemographicsControl.GetAddress2 = oCardScanning.Address2;
                                    oPatientDemographicsControl.GetCity = oCardScanning.City;
                                    oPatientDemographicsControl.GetCounty = oCardScanning.County;

                                    oPatientDemographicsControl.flgOCR = true;
                                    oPatientDemographicsControl.GetZip = oCardScanning.Zip;
                                    oPatientDemographicsControl.flgOCR = false;
                                    oPatientDemographicsControl.GetGender = oCardScanning.Sex;
                                    oPatientDemographicsControl.GetState = oCardScanning.State;
                                    oPatientDemographicsControl.GetCountry = oCardScanning.CountryShort;
                                    if (oCardScanning.PhotoImage != null)
                                    {
                                        oPatientDemographicsControl.GetPatPhoto = (Image)(oCardScanning.PhotoImage.Clone());
                                    }
                                    if (oCardScanning.cardFrontImage != null)
                                    {
                                        oPatientDemographicsControl.GetcardFrontImage = (Image)(oCardScanning.cardFrontImage.Clone());
                                    }
                                    if (oCardScanning.cardBackImage != null)
                                    {
                                        oPatientDemographicsControl.GetcardBackImage = (Image)(oCardScanning.cardBackImage.Clone());
                                    }
                                }
                                if (oCardScanning != null)
                                {
                                    oCardScanning.DisposeCardImages();
                                    oCardScanning.Dispose();
                                    oCardScanning = null;
                                }

                            }
                        }
                        break;
                    case "SaveAsCopy":
                        closeWebcam(true);
                        {


                            if (oPatientDemographicsControl.PatientGuarantors != null && oPatientDemographicsControl.PatientGuarantors.Count == 0)
                            {
                                MessageBox.Show("Select guarantor for Account.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                break;
                            }
                            //..Code added by Sagar Ghodke on Sep. 01 2011 - Version : 6040 implementation
                            //..Code added to solve the copy patient - account save issue
                            //..Ref. Bug# 8431
                            //..Disabled the Print functionality when the Patient Registration is under "SaveAsCopy" mode
                            tsb_Print.Enabled = false;
                            //..End code - Sagar Ghodke on Sep. 01 2011 - Version : 6040 implementation


                            this.Text = "New Patient : copy of" + " " + oPatientHistoryDemographics.PatientFirstName + " " + oPatientHistoryDemographics.PatientLastName;
                            this.Icon = Properties.Resources.Save_as_Copy;
                            gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
                            object oResult;
                            object oResultAllowEditablePatientCode;
                            _IsSaveAsCopy = true;

                            ogloSettings.GetSetting("Auto-Generate Patient Code", out oResult);
                            ogloSettings.GetSetting("Allow-Editable Patient Code", out oResultAllowEditablePatientCode);
                            ogloSettings.Dispose();
                            ogloSettings = null;
                            Int32 _AutoGenerate = 0;
                            Int32 _AllowEditablePatientCode = 0;
                            if (_IsSaveAsCopy == true)
                            {
                                if (oResult != null && oResult.ToString() != "" && oResultAllowEditablePatientCode != null && oResultAllowEditablePatientCode.ToString() != "")
                                {
                                    _AutoGenerate = Convert.ToInt32(oResult);
                                    _AllowEditablePatientCode = Convert.ToInt32(oResultAllowEditablePatientCode);
                                    if (_AllowEditablePatientCode != 0) //Auto generate is true
                                    {
                                        oPatientDemographicsControl.txtPACode.Enabled = true;
                                        oPatientDemographicsControl.txtPACode.ReadOnly = false;

                                    }
                                }
                                oPatientDemographicsControl._IsSaveAsCopy = true;
                                oPatientDemographicsControl.IsInsuranceModified = true;
                                tsb_SaveAsCopy.Enabled = false;
                                oPatientDemographicsControl.txtPACode.Focus();
                                ts_btnDemoHx.Enabled = false;
                                //changed for Bug #76184: gloEMR: Modify Patient - Print button under view mode should be disabled 
                                tsb_OK.Enabled = true;
                                oPatientDemographicsControl._Id = _PatientID;
                                oPatientDemographicsControl.GetAccountDataForSaveAsCopyPatient();
                                
                            }
                            //Patient Portal
                            oPatientDemographicsControl.SetPatientPortalEmailFacility(_IsSaveAsCopy);
                            //Patient Portal

                        }
                        break;
                    default:
                        {

                        }
                        break;
                }

                if (oPatientDemographicsControl._IsAuditLogGetData == true || oPatientDemographicsControl._IsAuditLogModified == true)
                {
                    gloPatient oPat = new gloPatient(_databaseconnectionstring);
                    DataTable dtGurdianInfo = oPat.GetGuardianInformation(_PatientID);
                    
                    if (dtGurdianInfo != null && dtGurdianInfo.Rows.Count > 0)
                    {
                        //Mother's Information
                        if (dtGurdianInfo.Rows[0]["sMother_fName"].ToString() != oPatientDemographicsControl.PatientGuardianDetails.PatientMotherFirstName || dtGurdianInfo.Rows[0]["sMother_mName"].ToString() != oPatientDemographicsControl.PatientGuardianDetails.PatientMotherMiddleName || dtGurdianInfo.Rows[0]["sMother_lName"].ToString() != oPatientDemographicsControl.PatientGuardianDetails.PatientMotherLastName)
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientRecordModified, gloAuditTrail.ActivityType.Modify, "Mothers name modified", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);

                        if (dtGurdianInfo.Rows[0]["sMother_maidenfName"].ToString() != oPatientDemographicsControl.PatientGuardianDetails.PatientMotherMaidenFirstName || dtGurdianInfo.Rows[0]["sMother_maidenmName"].ToString() != oPatientDemographicsControl.PatientGuardianDetails.PatientMotherMaidenMiddleName || dtGurdianInfo.Rows[0]["sMother_maidenlName"].ToString() != oPatientDemographicsControl.PatientGuardianDetails.PatientMotherMaidenLastName)
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientRecordModified, gloAuditTrail.ActivityType.Modify, "Mothers Maiden name modified", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                       
                        if (dtGurdianInfo.Rows[0]["sMother_Address1"].ToString() != oPatientDemographicsControl.PatientGuardianDetails.PatientMotherAddress1 || dtGurdianInfo.Rows[0]["sMother_Address2"].ToString() != oPatientDemographicsControl.PatientGuardianDetails.PatientMotherAddress2 || dtGurdianInfo.Rows[0]["sMother_City"].ToString() != oPatientDemographicsControl.PatientGuardianDetails.PatientMotherCity || dtGurdianInfo.Rows[0]["sMother_State"].ToString() != oPatientDemographicsControl.PatientGuardianDetails.PatientMotherState || dtGurdianInfo.Rows[0]["sMother_ZIP"].ToString() != oPatientDemographicsControl.PatientGuardianDetails.PatientMotherZip || dtGurdianInfo.Rows[0]["sMother_County"].ToString() != oPatientDemographicsControl.PatientGuardianDetails.PatientMotherCounty || dtGurdianInfo.Rows[0]["sMother_Country"].ToString() != oPatientDemographicsControl.PatientGuardianDetails.PatientMotherCountry)
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientRecordModified, gloAuditTrail.ActivityType.Modify, "Mothers address modified", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);

                        if (dtGurdianInfo.Rows[0]["sMother_Phone"].ToString() != oPatientDemographicsControl.PatientGuardianDetails.PatientMotherPhone || dtGurdianInfo.Rows[0]["sMother_Mobile"].ToString() != oPatientDemographicsControl.PatientGuardianDetails.PatientMotherMobile || dtGurdianInfo.Rows[0]["sMother_FAX"].ToString() != oPatientDemographicsControl.PatientGuardianDetails.PatientMotherFAX || dtGurdianInfo.Rows[0]["sMother_Email"].ToString() != oPatientDemographicsControl.PatientGuardianDetails.PatientMotherEmail)
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientRecordModified, gloAuditTrail.ActivityType.Modify, "Mothers contact modified", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                        //End

                        //Father's Information
                        if (dtGurdianInfo.Rows[0]["sFather_fName"].ToString() != oPatientDemographicsControl.PatientGuardianDetails.PatientFatherFirstName || dtGurdianInfo.Rows[0]["sFather_mName"].ToString() != oPatientDemographicsControl.PatientGuardianDetails.PatientFatherMiddleName || dtGurdianInfo.Rows[0]["sFather_lName"].ToString() != oPatientDemographicsControl.PatientGuardianDetails.PatientFatherLastName)
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientRecordModified, gloAuditTrail.ActivityType.Modify, "Fathers name modified", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);

                        if (dtGurdianInfo.Rows[0]["sFather_Address1"].ToString() != oPatientDemographicsControl.PatientGuardianDetails.PatientFatherAddress1 || dtGurdianInfo.Rows[0]["sFather_Address2"].ToString() != oPatientDemographicsControl.PatientGuardianDetails.PatientFatherAddress2 || dtGurdianInfo.Rows[0]["sFather_City"].ToString() != oPatientDemographicsControl.PatientGuardianDetails.PatientFatherCity || dtGurdianInfo.Rows[0]["sFather_State"].ToString() != oPatientDemographicsControl.PatientGuardianDetails.PatientFatherState || dtGurdianInfo.Rows[0]["sFather_ZIP"].ToString() != oPatientDemographicsControl.PatientGuardianDetails.PatientFatherZip || dtGurdianInfo.Rows[0]["sFather_County"].ToString() != oPatientDemographicsControl.PatientGuardianDetails.PatientFatherCounty || dtGurdianInfo.Rows[0]["sFather_Country"].ToString() != oPatientDemographicsControl.PatientGuardianDetails.PatientFatherCountry)
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientRecordModified, gloAuditTrail.ActivityType.Modify, "Fathers address modified", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);

                        if (dtGurdianInfo.Rows[0]["sFather_Phone"].ToString() != oPatientDemographicsControl.PatientGuardianDetails.PatientFatherPhone || dtGurdianInfo.Rows[0]["sFather_Mobile"].ToString() != oPatientDemographicsControl.PatientGuardianDetails.PatientFatherMobile || dtGurdianInfo.Rows[0]["sFather_FAX"].ToString() != oPatientDemographicsControl.PatientGuardianDetails.PatientFatherFAX || dtGurdianInfo.Rows[0]["sFather_Email"].ToString() != oPatientDemographicsControl.PatientGuardianDetails.PatientFatherEmail)
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientRecordModified, gloAuditTrail.ActivityType.Modify, "Fathers contact modified", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                        //End

                        //Guardian's Information
                        if (dtGurdianInfo.Rows[0]["sGuardian_fName"].ToString() != oPatientDemographicsControl.PatientGuardianDetails.PatientGuardianFirstName || dtGurdianInfo.Rows[0]["sGuardian_mName"].ToString() != oPatientDemographicsControl.PatientGuardianDetails.PatientGuardianMiddleName || dtGurdianInfo.Rows[0]["sGuardian_lName"].ToString() != oPatientDemographicsControl.PatientGuardianDetails.PatientGuardianLastName)
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientRecordModified, gloAuditTrail.ActivityType.Modify, "Guardians name modified", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);

                        if (dtGurdianInfo.Rows[0]["sGuardian_Address1"].ToString() != oPatientDemographicsControl.PatientGuardianDetails.PatientGuardianAddress1 || dtGurdianInfo.Rows[0]["sGuardian_Address2"].ToString() != oPatientDemographicsControl.PatientGuardianDetails.PatientGuardianAddress2 || dtGurdianInfo.Rows[0]["sGuardian_City"].ToString() != oPatientDemographicsControl.PatientGuardianDetails.PatientGuardianCity || dtGurdianInfo.Rows[0]["sGuardian_State"].ToString() != oPatientDemographicsControl.PatientGuardianDetails.PatientGuardianState || dtGurdianInfo.Rows[0]["sGuardian_ZIP"].ToString() != oPatientDemographicsControl.PatientGuardianDetails.PatientGuardianZip || dtGurdianInfo.Rows[0]["sGuardian_County"].ToString() != oPatientDemographicsControl.PatientGuardianDetails.PatientGuardianCounty || dtGurdianInfo.Rows[0]["sGuardian_Country"].ToString() != oPatientDemographicsControl.PatientGuardianDetails.PatientGuardianCountry)
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientRecordModified, gloAuditTrail.ActivityType.Modify, "Guardians address modified", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);

                        if (dtGurdianInfo.Rows[0]["sGuardian_Phone"].ToString() != oPatientDemographicsControl.PatientGuardianDetails.PatientGuardianPhone || dtGurdianInfo.Rows[0]["sGuardian_Mobile"].ToString() != oPatientDemographicsControl.PatientGuardianDetails.PatientGuardianMobile || dtGurdianInfo.Rows[0]["sGuardian_FAX"].ToString() != oPatientDemographicsControl.PatientGuardianDetails.PatientGuardianFAX || dtGurdianInfo.Rows[0]["sGuardian_Email"].ToString() != oPatientDemographicsControl.PatientGuardianDetails.PatientGuardianEmail)
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientRecordModified, gloAuditTrail.ActivityType.Modify, "Guardians contact modified", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);

                        if (dtGurdianInfo.Rows[0]["sGuardian_RelationshipDS"].ToString() != oPatientDemographicsControl.PatientGuardianDetails.PatientGuardianRelationDS)
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientRecordModified, gloAuditTrail.ActivityType.Modify, "Guardians relation modified", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                        //End
                    }

                    if (dtGurdianInfo != null) { dtGurdianInfo.Dispose(); }
                    if (oPat != null) { oPat.Dispose(); }

                  
                    
                }

             
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

      
        private void SavePatientDetails()
        {
            gloPatient ogloDPatient = new gloPatient(_databaseconnectionstring);
            try
            {
                isSaveAndClose = true;
                bool bIsInboundHospitalPresent = false;
                bool bIsInboundTransactionOfCarePresent = false;
                bool bIsTrainingProvided = false, bIsAPITrainingProvided = false;
                string sInboundHospitals = string.Empty, sInboundTransactionOfCare = string.Empty;
               
                oPatientDemographicsControl.CheckforaddCategory();
                if (oPatientDemographicsControl.GetData() == true)
                {
                    if (base.SetChildFormModules("SavePatientDetails", "Save Patient", oPatientDemographicsControl.PatientDemographicsDetails.PatientProviderID.ToString()) == true)
                    {
                        return;
                    }
                    oPatient.DemographicsDetail = oPatientDemographicsControl.PatientDemographicsDetails;
                    oPatient.OccupationDetail = oPatientDemographicsControl.PatientOccupationDetails;
                    oPatient.GuardianDetail = oPatientDemographicsControl.PatientGuardianDetails;
                    oPatient.InsuranceDetails = oPatientDemographicsControl.PatientInsuranceDetails;
                    oPatient.PatientDemographicOtherInfo = oPatientDemographicsControl.PatientDemographicOtherInfo;
                    oPatient.PatientPharmacies = oPatientDemographicsControl.PatientPharmacies;
                    oPatient.PatientReferrals = oPatientDemographicsControl.PatientReferrals;
                    oPatient.PatientCareTeam = oPatientDemographicsControl.PatientCareTeam;
                    oPatient.PrimaryCarePhysicians = oPatientDemographicsControl.PrimaryCarePhysicians;
                    oPatient.PatientWorkersComp = oPatientDemographicsControl.PatientWorkersComps;
                    oPatient.PatientGuarantors = oPatientDemographicsControl.PatientGuarantors;
                    //Patient Portal
                    gblnPatientPortalSendActivationEmail = oPatientDemographicsControl.gblnPatientPortalSendActivationEmail;
                    gblnPatientPortalActivationEmailAlreadySent = oPatientDemographicsControl.gblnPatientPortalActivationEmailAlreadySent;
                    //Patient Portal
                    //API
                    gblnAPIActivation = oPatientDemographicsControl.gblnPatientAPISendActivationEmail;

                    //InboundtranCare
                   


                        sPatientInboundTranCare = "";
                        if (oPatientDemographicsControl.dtReturn != null && oPatientDemographicsControl.dtReturn.Rows.Count > 0)
                        {
                            bIsInboundTransactionOfCarePresent = true;
                            DataRow[] row = oPatientDemographicsControl.dtReturn.Select("MuCheckBox");
                            for (int i = 0; i < row.Length; i++)
                            {
                                sPatientInboundTranCare = sPatientInboundTranCare + Convert.ToString(row[i]["Description"]) + ",";
                            }
                            sPatientInboundTranCare = sPatientInboundTranCare.Trim(',');
                        }
                        else
                        {
                            sPatientInboundTranCare = "-1";
                        }

                    //PatientInboundHospital
                   

                        sPatientInboundHospital  = "";
                        if (oPatientDemographicsControl.dtPathosp_data != null && oPatientDemographicsControl.dtPathosp_data.Rows.Count > 0)
                        {

                            bIsInboundHospitalPresent = true;
                            DataRow[] row = oPatientDemographicsControl.dtPathosp_data.Select("chkStatus");
                            for (int i = 0; i < row.Length; i++)
                            {
                                sPatientInboundHospital = sPatientInboundHospital + Convert.ToString(row[i]["Description"]) + ",";
                            }
                            sPatientInboundHospital = sPatientInboundHospital.Trim(',');

                        }
                        else
                        {
                            sPatientInboundHospital = "-1";
                        }


                    SaveHospData();
                    // For Resolving Bug ID :7627 
                    //Batch >> Send >>Application is showing un-necessary validation message for subscriber address details while sending batch
                    if (oPatient.InsuranceDetails.InsurancesDetails.Count > 0)
                    {
                        for (int i = 0; i <= oPatient.InsuranceDetails.InsurancesDetails.Count - 1; i++)
                        {
                            if (Convert.ToString(oPatient.InsuranceDetails.InsurancesDetails[i].RelationshipName).ToUpper() == "SELF")
                            {
                                if (oPatient.DemographicsDetail.PatientDOB != null)
                                {
                                    oPatient.InsuranceDetails.InsurancesDetails[i].IsNotDOB = false;
                                }
                                else
                                {
                                    oPatient.InsuranceDetails.InsurancesDetails[i].IsNotDOB = true;
                                }

                                oPatient.IsInsuranceModified = true;
                            }
                            else
                            {
                                // For Resolving Bug ID :7627 
                                //Batch >> Send >>Application is showing un-necessary validation message for subscriber address details while sending batch
                                oPatient.IsInsuranceModified = oPatientDemographicsControl.IsInsuranceModified;
                            }
                        }
                    }

                    oPatient.DeletedInsurances = oPatientDemographicsControl.DeletedInsurances;
                    //oPatient.IsInsuranceModified = oPatientDemographicsControl.IsInsuranceModified;

                    oPatient.Account = oPatientDemographicsControl.Account;
                    oPatient.PatientAccount = oPatientDemographicsControl.PatientAccount;
                    oPatient.IsPatientAccountFeature = _IsPatientAccountFeature;
                    oPatient._IsPatientDataModified = oPatientDemographicsControl._IsPatientDataModified;
                    oPatient._IsPatientCodeModified = oPatientDemographicsControl._IsPatientCodeModified;
                    if (_IsPatientAccountFeature == false)
                    {
                        oPatient.nPAccountId = oPatientDemographicsControl.nPAccountId;
                        oPatient.nGuarantorId = oPatientDemographicsControl.nGuarantorId;
                    }
                    oPatient.PatientOtherGuarantors = oPatientDemographicsControl.PatientOtherGuarantors;
                    oPatient.PatientRepresentatives = oPatientDemographicsControl.PatientRepresentatives;
                    oPatient.PatientPortalAccount = oPatientDemographicsControl.PatientPortalAccount;
                    oPatient.APIRepresentatives = oPatientDemographicsControl.APIRepresentatives;
                    oPatient.APIAccount = oPatientDemographicsControl.APIAccount;
                    // oPatientDemographicsControl.APIRepresentatives = oPatient.APIRepresentatives;
                    // oPatientDemographicsControl.APIAccount = oPatient.APIAccount;

                    Boolean _IsExists = false;

                    if (_PatientID == 0 || _IsSaveAsCopy == true)
                    {
                        _IsExists = ogloDPatient.IsPatientCodeExists(oPatient.DemographicsDetail.PatientCode);

                        if (_IsExists == true)
                        {
                            if (ogloDPatient.IsAutoGeneratePatientCode() == true && _IsSaveAsCopy == false)
                            {
                                _IsExists = false;
                                gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Patient, ActivityCategory.SetupPatient, ActivityType.Add, "For new patient, patient code '" + oPatient.DemographicsDetail.PatientCode + "' already exists.", ActivityOutCome.Success);
                            }
                            if ((_messageboxcaption == "gloPM") && (oPatientDemographicsControl.txtPACode.ReadOnly == true))
                            {
                                _IsExists = false;
                                gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Patient, ActivityCategory.SetupPatient, ActivityType.Add, "For new patient, patient code '" + oPatient.DemographicsDetail.PatientCode + "' already exists.", ActivityOutCome.Success);
                            }
                        }
                    }
                    if (_IsExists == false)
                    {
                        if (_IsSaveAsCopy == true)
                        {
                            oPatient.DemographicsDetail.PatientID = 0;
                            for (int i = 0; i <= oPatient.InsuranceDetails.InsurancesDetails.Count - 1; i++)
                            {
                                oPatient.InsuranceDetails.InsurancesDetails[i].InsuranceID = 0;
                            }
                        }
                        if (oPatientDemographicsControl.IsModified() && oPatientDemographicsControl.PatientReferrals != null && oPatientDemographicsControl.PatientReferrals.Count > 0)
                        {
                            for (int i = 0; i <= oPatientDemographicsControl.PatientReferrals.Count-1; i++)
                            {
                                if (oPatientDemographicsControl.PatientReferrals[i].MUCheckBox==true)
                                {
                                    bIsInboundTransactionOfCarePresent = true;
                                    break;
                                }
                            }
                            
                        }
                        if (oPatientDemographicsControl.PatientPortalAccount != null && oPatientDemographicsControl.PatientPortalAccount.IsTrainingProvided == true)
                        {
                            bIsTrainingProvided = true;
                        }
                        if (oPatientDemographicsControl.APIAccount != null && oPatientDemographicsControl.APIAccount.IsTrainingProvided == true)
                        {
                            bIsAPITrainingProvided = true;
                        }
                        if (gblnPatientPortalSendActivationEmail == true || bIsInboundHospitalPresent == true || bIsInboundTransactionOfCarePresent == true || gblnAPIActivation == true || bIsTrainingProvided == true || bIsAPITrainingProvided == true)
                        {
                            //if (gblnPatientPortalSendActivationEmail)
                            //{
                            //    if (!getProviderTaxID(oPatient.DemographicsDetail.PatientProviderID))
                            //    {
                            //        return;
                            //    } 
                            //}
                            //else 
                            //{
                            if (!getProviderTaxID(gloGlobal.gloPMGlobal.LoginProviderID == 0 ? oPatient.DemographicsDetail.PatientProviderID : gloGlobal.gloPMGlobal.LoginProviderID))
                            {
                                return;
                            }
                            //}
                        }

                        ReturnPatientID = oPatientTrans.Add(oPatient);
                        oPatientDemographicsControl.PatientId = ReturnPatientID;
                        
                        SendPatientPortalEmails(ReturnPatientID);
                        APIActivation(ReturnPatientID, oPatient.DemographicsDetail.PatientEmail);

                        if (gblnPatientPortalSendActivationEmail == true || bIsInboundHospitalPresent == true || bIsInboundTransactionOfCarePresent == true || gblnAPIActivation == true || bIsTrainingProvided==true || bIsAPITrainingProvided==true)
                        {
                            Int64 ProviderID = gloGlobal.gloPMGlobal.LoginProviderID == 0 ? oPatient.DemographicsDetail.PatientProviderID : gloGlobal.gloPMGlobal.LoginProviderID;
                            if (gblnPatientPortalSendActivationEmail)
                            {
                                string sMessageQueueIDs = getIDs(ReturnPatientID, 0);
                                gloGlobal.TIN.clsSelectProviderTaxID oclsselectProviderTaxID = new gloGlobal.TIN.clsSelectProviderTaxID(ProviderID);
                                oclsselectProviderTaxID.InsertProviderTaxID(nProviderAssociationID, Convert.ToInt64(sMessageQueueIDs), sProviderTaxID, ProviderID, 0, gloGlobal.TIN.clsSelectProviderTaxID.TransactionType.PatientPortalInvitation);
                                oclsselectProviderTaxID = null; 
                            }

                            if (bIsInboundTransactionOfCarePresent)
                            {
                                string[] nIDs = getIDs(ReturnPatientID, 1).Split(',');
                                foreach (var item in nIDs)
                                {
                                    if (item != "" && item != null)
                                    {
                                        //Int64 ProviderID = gloGlobal.gloPMGlobal.LoginProviderID == 0 ? oPatient.DemographicsDetail.PatientProviderID : gloGlobal.gloPMGlobal.LoginProviderID;
                                        gloGlobal.TIN.clsSelectProviderTaxID oclsselectProviderTaxID = new gloGlobal.TIN.clsSelectProviderTaxID(ProviderID);
                                        oclsselectProviderTaxID.InsertProviderTaxID(nProviderAssociationID, Convert.ToInt64(item), sProviderTaxID, ProviderID, 0, gloGlobal.TIN.clsSelectProviderTaxID.TransactionType.InboundTrasactionOfCare);
                                        oclsselectProviderTaxID = null;
                                    }
                                }
                                
                            }

                            if (bIsInboundHospitalPresent)
                            {
                                string[] nIDs= getIDs(ReturnPatientID, 2).Split(',');
                                foreach (var item in nIDs)
                                {
                                    if (item != "" && item != null)
                                    {
                                        //Int64 ProviderID = gloGlobal.gloPMGlobal.LoginProviderID == 0 ? oPatient.DemographicsDetail.PatientProviderID : gloGlobal.gloPMGlobal.LoginProviderID;
                                        gloGlobal.TIN.clsSelectProviderTaxID oclsselectProviderTaxID = new gloGlobal.TIN.clsSelectProviderTaxID(ProviderID);
                                        oclsselectProviderTaxID.InsertProviderTaxID(nProviderAssociationID, Convert.ToInt64(item), sProviderTaxID, ProviderID, 0, gloGlobal.TIN.clsSelectProviderTaxID.TransactionType.InboundHospital);
                                        oclsselectProviderTaxID = null;
                                    }
                                }
                                
                            }

                            if (gblnAPIActivation)
                            {
                                string sAPIAccessIDs = getIDs(ReturnPatientID, 3);
                                gloGlobal.TIN.clsSelectProviderTaxID oclsselectProviderTaxID = new gloGlobal.TIN.clsSelectProviderTaxID(ProviderID);
                                oclsselectProviderTaxID.InsertProviderTaxID(nProviderAssociationID, Convert.ToInt64(sAPIAccessIDs), sProviderTaxID, ProviderID, 0, gloGlobal.TIN.clsSelectProviderTaxID.TransactionType.APIAccessActivate);
                                oclsselectProviderTaxID = null;
                            }

                            if (bIsTrainingProvided)
                            {
                                gloGlobal.TIN.clsSelectProviderTaxID oclsselectProviderTaxID = new gloGlobal.TIN.clsSelectProviderTaxID(ProviderID);
                                oclsselectProviderTaxID.InsertProviderTaxID(nProviderAssociationID, ReturnPatientID, sProviderTaxID, ProviderID, 0, gloGlobal.TIN.clsSelectProviderTaxID.TransactionType.PatientPortalAllowManualAccess);
                                oclsselectProviderTaxID = null;
                            }
                            
                            if (bIsAPITrainingProvided)
                            {
                                gloGlobal.TIN.clsSelectProviderTaxID oclsselectProviderTaxID = new gloGlobal.TIN.clsSelectProviderTaxID(ProviderID);
                                oclsselectProviderTaxID.InsertProviderTaxID(nProviderAssociationID, ReturnPatientID, sProviderTaxID, ProviderID, 0, gloGlobal.TIN.clsSelectProviderTaxID.TransactionType.APIAccessAllowManually);
                                oclsselectProviderTaxID = null;
                            }
                        }

                        if (oPatientDemographicsControl.GetcardFrontImage != null)
                        {
                            SaveScanData(ReturnPatientID, oPatientDemographicsControl.GetcardFrontImage, oPatientDemographicsControl.GetcardBackImage, DateTime.Now, _CardScanType.GetHashCode(), "", _UserID);
                            oPatientDemographicsControl.GetcardFrontImage.Dispose();
                            oPatientDemographicsControl.GetcardFrontImage = null;
                            oPatientDemographicsControl.GetcardBackImage.Dispose();
                            oPatientDemographicsControl.GetcardBackImage = null;
                        }
                    }
                    else
                    {
                        if (MessageBox.Show("Duplicate patient code, do you want to generate new patient code?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            gloPatient ogloPatient = new gloPatient(_databaseconnectionstring);


                            oPatientDemographicsControl.txtPACode.Text = ogloPatient.GeneratePatientCode();
                            if (ogloPatient._UseSitePrefix != 0)
                            {
                                oPatientDemographicsControl.txtPACode.Mask = "AAA-AAAAAAAAAA";
                                oPatientDemographicsControl.txtPatientPrefix.Text = oPatientDemographicsControl.txtPACode.Text.Substring(0, 3);
                            }
                            else
                            {
                                oPatientDemographicsControl.txtPACode.Mask = "AAAAAAAAAAAAA";
                                oPatientDemographicsControl.txtPatientPrefix.Text = "";

                            }
                            ogloPatient.Dispose();
                            ogloPatient = null;
                            return;
                        }
                        else
                        {
                            return;
                        }
                    }

                    if (_IsSaveAsCopy == false)
                    {
                        if (_PatientID > 0)
                        {
                            SavePatientHistory();
                        }
                    }

                    string userAction = "";
                    long AUditTrailId=0;
                  
                    if (ReturnPatientID > 0)
                    {

                        if (_EMRdatabaseconnectionstring != "" && _AddPatientToEMR == true)
                        {
                            MigratePatient(ReturnPatientID, oPatient.DemographicsDetail.PatientCode);
                        }

                        if (_PatientID == 0 || _IsSaveAsCopy == true)
                        {
                            AUditTrailId = gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Patient, ActivityCategory.SetupPatient, ActivityType.Add, "Add Patient", ReturnPatientID, 0, 0, ActivityOutCome.Success);
                            userAction = "Add Patient";
                        }
                        else
                        {
                            AUditTrailId = gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Patient, ActivityCategory.SetupPatient, ActivityType.Modify, "Modify Patient", ReturnPatientID, 0, 0, ActivityOutCome.Success);
                            userAction = "Modify Patient";
                        }

                        if (_IsSaveAsCopy == true)
                        {
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Patient, ActivityCategory.SetupPatient, ActivityType.Copy, "Copy Patient", ReturnPatientID, 0, 0, ActivityOutCome.Success);
                        }

                        if (EvntSaveandClose != null)
                            EvntSaveandClose(ReturnPatientID);
                    }
                    else
                    {
                        AUditTrailId = gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Patient, ActivityCategory.SetupPatient, ActivityType.Add, "Add Patient", ReturnPatientID, 0, 0, ActivityOutCome.Success);
                        userAction = "Add Patient";
                    }


                    clsgloPatientAudit oAudit = new clsgloPatientAudit(_databaseconnectionstring);
                    oAudit.SavePatientAuditDetails(oPatientDemographicsControl.PatientId, AUditTrailId, userAction, sPatientInboundHospital, sPatientInboundTranCare);
                    oAudit.Dispose();
                    oAudit = null;


                    this.Close();
               
                    //  if (oPatientTrans != null) { oPatientTrans.Dispose(); }


                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ogloDPatient != null) { ogloDPatient.Dispose(); }
            }
        }
        private void SaveHospData()
        {
            if (oPatientDemographicsControl.dtPathosp_data != null)
            {
                gloPatient objglopat = new gloPatient(_databaseconnectionstring);
                if (oPatientDemographicsControl.dtPathosp_data.Columns.Contains("Description"))
                {
                    oPatientDemographicsControl.dtPathosp_data.Columns.Remove("Description"); 
                }

                objglopat.SaveMUPatHosp(_PatientID, oPatientDemographicsControl.dtPathosp_data);
                oPatientDemographicsControl.dtPathosp_data.Dispose();
                oPatientDemographicsControl.dtPathosp_data = null; //Bug #109463: Inbound Hospital: Records showing blank Isuuse Resolved

            }
         }
        private bool SavePatient()
        {
            if (oPatientDemographicsControl.GetData() == true)
            {
                oPatient.DemographicsDetail = oPatientDemographicsControl.PatientDemographicsDetails;
                oPatient.OccupationDetail = oPatientDemographicsControl.PatientOccupationDetails;
                oPatient.GuardianDetail = oPatientDemographicsControl.PatientGuardianDetails;
                oPatient.InsuranceDetails = oPatientDemographicsControl.PatientInsuranceDetails;

                // For Resolving Bug Reported by Mahesh Nawal for EDI.
                if (oPatient.InsuranceDetails.InsurancesDetails.Count > 0 ) 
                {
                    for(int i=0;i <= oPatient.InsuranceDetails.InsurancesDetails.Count-1 ; i++)
                    {
                        if (Convert.ToString(oPatient.InsuranceDetails.InsurancesDetails[i].RelationshipName).ToUpper() == "SELF")
                        {
                            if (oPatient.DemographicsDetail.PatientDOB != null)
                            {
                                oPatient.InsuranceDetails.InsurancesDetails[i].IsNotDOB = false;
                            }
                            else
                            {
                                oPatient.InsuranceDetails.InsurancesDetails[i].IsNotDOB = true;
                            }
                            //Bug ID :8034
                            oPatient.InsuranceDetails.InsurancesDetails[i].IsSameAsPatient = true;

                            oPatient.IsInsuranceModified = true;

                            //Bug ID :8034
                            break;
                        }
                        else
                        {
                            // For Resolving Bug ID :8267 
                            // Insurances were not printed while printing the Patient Details when insurance type was Other then Self.
                            // For eg: If no insurance is available, add a new insurance (other than self), try to print
                            oPatient.IsInsuranceModified = oPatientDemographicsControl.IsInsuranceModified;
                        }
                    }
                }
                oPatient.PatientDemographicOtherInfo = oPatientDemographicsControl.PatientDemographicOtherInfo;
                oPatient.PatientPharmacies = oPatientDemographicsControl.PatientPharmacies;
                oPatient.PatientReferrals = oPatientDemographicsControl.PatientReferrals;
                oPatient.PatientCareTeam = oPatientDemographicsControl.PatientCareTeam;
                oPatient.PrimaryCarePhysicians = oPatientDemographicsControl.PrimaryCarePhysicians;
                oPatient.PatientWorkersComp = oPatientDemographicsControl.PatientWorkersComps;
                oPatient.PatientGuarantors = oPatientDemographicsControl.PatientGuarantors;

                //Start -for Resolving Bug ID : 8034 
                oPatient.DeletedInsurances = oPatientDemographicsControl.DeletedInsurances;

                if (oPatientDemographicsControl.IsInsuranceModified == true)
                {
                    oPatientDemographicsControl.IsInsuranceModified = false;
                }
                 //End - for Resolving Bug ID : 8034 

                oPatient.Account = oPatientDemographicsControl.Account;
                oPatient.PatientAccount = oPatientDemographicsControl.PatientAccount;
                oPatient.IsPatientAccountFeature = _IsPatientAccountFeature;
                oPatient._IsPatientDataModified = oPatientDemographicsControl._IsPatientDataModified;
                oPatient._IsPatientCodeModified = oPatientDemographicsControl._IsPatientCodeModified;
                if (_IsPatientAccountFeature == false)
                {
                    oPatient.nPAccountId = oPatientDemographicsControl.nPAccountId;
                    oPatient.nGuarantorId = oPatientDemographicsControl.nGuarantorId;

                }
                oPatient.PatientOtherGuarantors = oPatientDemographicsControl.PatientOtherGuarantors;
                oPatient.PatientRepresentatives = oPatientDemographicsControl.PatientRepresentatives;
                oPatient.PatientPortalAccount = oPatientDemographicsControl.PatientPortalAccount;
                oPatient.APIRepresentatives = oPatientDemographicsControl.APIRepresentatives;
                oPatient.APIAccount = oPatientDemographicsControl.APIAccount; 
                Boolean _IsExists = false;

                if (_PatientID == 0 || _IsSaveAsCopy == true)
                {
                    gloPatient ogloDPatient = new gloPatient(_databaseconnectionstring);
                    _IsExists = ogloDPatient.IsPatientCodeExists(oPatient.DemographicsDetail.PatientCode);

                    if (_IsExists == true)
                    {
                        if (ogloDPatient.IsAutoGeneratePatientCode() == true && _IsSaveAsCopy == false)
                        {
                            _IsExists = false;
                            gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Patient, ActivityCategory.SetupPatient, ActivityType.Add, "For new patient, patient code '" + oPatient.DemographicsDetail.PatientCode + "' already exists.", ActivityOutCome.Success);
                        }
                        if ((_messageboxcaption == "gloPM") && (oPatientDemographicsControl.txtPACode.ReadOnly == true))
                        {
                            _IsExists = false;
                            gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Patient, ActivityCategory.SetupPatient, ActivityType.Add, "For new patient, patient code '" + oPatient.DemographicsDetail.PatientCode + "' already exists.", ActivityOutCome.Success);
                        }
                    }
                    ogloDPatient.Dispose();
                    ogloDPatient = null;

                }
                if (_IsExists == false)
                {
                    if (_IsSaveAsCopy == true)
                    {
                        oPatient.DemographicsDetail.PatientID = 0;

                        for (int i = 0; i <= oPatient.InsuranceDetails.InsurancesDetails.Count - 1; i++)
                        {
                            oPatient.InsuranceDetails.InsurancesDetails[i].InsuranceID = 0;
                        }

                    }
                    ReturnPatientID = oPatientTrans.Add(oPatient);
                    oPatientDemographicsControl.PatientId = ReturnPatientID;
                    

                    if (oPatientDemographicsControl.GetcardFrontImage != null)
                    {
                        SaveScanData(ReturnPatientID, oPatientDemographicsControl.GetcardFrontImage, oPatientDemographicsControl.GetcardBackImage, DateTime.Now, _CardScanType.GetHashCode(), "",_UserID);
                        oPatientDemographicsControl.GetcardFrontImage.Dispose();
                        oPatientDemographicsControl.GetcardFrontImage = null;
                        oPatientDemographicsControl.GetcardBackImage.Dispose();
                        oPatientDemographicsControl.GetcardBackImage = null;
                    }
                }
                else
                {
                    MessageBox.Show("Patient code is assigned to another patient. Please enter a unique patient code.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                
                if (_PatientID > 0)
                {
                    SavePatientHistory();
                }

                string userAction = "";
                long AUditTrailId = 0;

                if (ReturnPatientID > 0)
                {
                    if (_EMRdatabaseconnectionstring != "" && _AddPatientToEMR == true)
                    {
                        MigratePatient(ReturnPatientID, oPatient.DemographicsDetail.PatientCode);
                    }
                   AUditTrailId= gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Patient, ActivityCategory.SetupPatient, ActivityType.Modify, "Modify Patient", ReturnPatientID, 0, 0, ActivityOutCome.Success);
                    userAction="Modify Patient";
                }
                else
                {
                   AUditTrailId= gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Patient, ActivityCategory.SetupPatient, ActivityType.Add, "Add Patient", ReturnPatientID, 0, 0, ActivityOutCome.Success);
                   userAction = "Add Patient";
                }
                
                clsgloPatientAudit oAudit = new clsgloPatientAudit(_databaseconnectionstring);
                oAudit.SavePatientAuditDetails(_PatientID, AUditTrailId, userAction);
                oAudit.Dispose();
                oAudit = null;

                oPatientDemographicsControl._IsSaveAsCopy = false;
                _IsSaveAsCopy = false;
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion "Main Tool Strip Events "

        private void MigratePatient(long PMPatientID, string PMPatientCode)
        {
            try
            {
                bool _isValidConnection = false;
                if (PMPatientID > 0 && _EMRdatabaseconnectionstring != "" && _AddPatientToEMR == true)
                {
                    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_EMRdatabaseconnectionstring);
                    _isValidConnection = oDB.CheckConnection();
                    oDB.Disconnect();
                    oDB.Dispose();
                }

                if (_isValidConnection == true)
                {
                    if (_messageboxcaption.Trim().ToUpper() == "gloPM".ToUpper())
                    {
                        string sMigrationType = "";

                        object _value = null;
                        gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
                        oSettings.GetSetting("MigrateToEMRType", out _value);
                        oSettings.Dispose();
                        oSettings = null;
                        if (_value != null && Convert.ToString(_value) != "")
                        {
                            sMigrationType = Convert.ToString(_value);
                        }
                        else
                        {
                            sMigrationType = "gloEMR40SP2"; 
                        }

                        if (sMigrationType == "gloEMR40SP2")
                        {
                            gloPatientMigration.gloPatientMigration ogloPatientMigration = new gloPatientMigration.gloPatientMigration(_EMRdatabaseconnectionstring, _databaseconnectionstring, 1);
                            ogloPatientMigration.SendPatientPMtoEMR(PMPatientID, gloPatientMigration.EnumPatientConflict.None);
                            ogloPatientMigration.Dispose();
                        }
                        else if (sMigrationType == "gloEMR2.8") //Migrate Patient  to EMR2.8
                        {
                            gloPatientMigration.gloPatientMigration ogloPatientMigration = new gloPatientMigration.gloPatientMigration(_EMRdatabaseconnectionstring, _databaseconnectionstring, 1);
                            ogloPatientMigration.SendPatientPMtoEMR28(PMPatientID, gloPatientMigration.EnumPatientConflict.None);
                            ogloPatientMigration.Dispose();
                        }
                        else if (sMigrationType == "gloEMR50") //Migrate Patient  to EMR50
                        {

                        }
                    }
                    else
                    {

                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private bool CloseForm()
        {
            //Bug #62022: 00000606 : EMR PATIENT RELATED LIQUID LINKS CREATE FALSE WARNING WHEN TRIPLE-CLICKED 
            //added form level locking for patient registration.
            if (oPatientDemographicsControl.IsModified() == true && tsb_OK.Enabled==true)
            {
                DialogResult res = MessageBox.Show("Do you want to save changes to this record? ", _messageboxcaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                if (res == DialogResult.Yes)
                {
                    if (oPatientDemographicsControl.GetData() == true)
                    {
                        oPatient.DemographicsDetail = oPatientDemographicsControl.PatientDemographicsDetails;
                        oPatient.OccupationDetail = oPatientDemographicsControl.PatientOccupationDetails;
                        oPatient.GuardianDetail = oPatientDemographicsControl.PatientGuardianDetails;
                        oPatient.InsuranceDetails = oPatientDemographicsControl.PatientInsuranceDetails;
                        oPatient.PatientDemographicOtherInfo = oPatientDemographicsControl.PatientDemographicOtherInfo;
                        oPatient.PatientPharmacies = oPatientDemographicsControl.PatientPharmacies;
                        oPatient.PatientReferrals = oPatientDemographicsControl.PatientReferrals;
                        oPatient.PatientCareTeam = oPatientDemographicsControl.PatientCareTeam;
                        oPatient.PrimaryCarePhysicians = oPatientDemographicsControl.PrimaryCarePhysicians;
                        oPatient.PatientGuarantors = oPatientDemographicsControl.PatientGuarantors;

                        oPatient.Account = oPatientDemographicsControl.Account;
                        oPatient.PatientAccount = oPatientDemographicsControl.PatientAccount;
                        oPatient.IsPatientAccountFeature = _IsPatientAccountFeature;
                        oPatient._IsPatientDataModified = oPatientDemographicsControl._IsPatientDataModified;
                        oPatient._IsPatientCodeModified = oPatientDemographicsControl._IsPatientCodeModified;
                        oPatient.IsInsuranceModified = oPatientDemographicsControl.IsInsuranceModified;
                        if (_IsPatientAccountFeature == false)
                        {
                            oPatient.nPAccountId = oPatientDemographicsControl.nPAccountId;
                            oPatient.nGuarantorId = oPatientDemographicsControl.nGuarantorId;
                        }
                        oPatient.PatientOtherGuarantors = oPatientDemographicsControl.PatientOtherGuarantors;

                        Boolean _IsExists = false;

                        if (_PatientID == 0 || _IsSaveAsCopy == true)
                        {
                            gloPatient ogloDPatient = new gloPatient(_databaseconnectionstring);
                            _IsExists = ogloDPatient.IsPatientCodeExists(oPatient.DemographicsDetail.PatientCode);

                            if (_IsExists == true)
                            {
                                if (ogloDPatient.IsAutoGeneratePatientCode() == true && _IsSaveAsCopy == false)
                                {
                                    _IsExists = false;
                                    gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Patient, ActivityCategory.SetupPatient, ActivityType.Add, "For new patient, patient code '" + oPatient.DemographicsDetail.PatientCode + "' already exists.", ActivityOutCome.Success);
                                }
                                if ((_messageboxcaption == "gloPM") && (oPatientDemographicsControl.txtPACode.ReadOnly == true))
                                {
                                    _IsExists = false;
                                    gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Patient, ActivityCategory.SetupPatient, ActivityType.Add, "For new patient, patient code '" + oPatient.DemographicsDetail.PatientCode + "' already exists.", ActivityOutCome.Success);
                                }
                            }
                            ogloDPatient.Dispose();
                            ogloDPatient = null;

                        }
                        if (_IsExists == false)
                        {
                            if (_IsSaveAsCopy == true)
                            {
                                oPatient.DemographicsDetail.PatientID = 0;
                                for (int i = 0; i <= oPatient.InsuranceDetails.InsurancesDetails.Count - 1; i++)
                                {
                                    oPatient.InsuranceDetails.InsurancesDetails[i].InsuranceID = 0;
                                }

                            }
                            ReturnPatientID = oPatientTrans.Add(oPatient);

                            if (oPatientDemographicsControl.GetcardFrontImage != null)
                            {
                                SaveScanData(ReturnPatientID, oPatientDemographicsControl.GetcardFrontImage, oPatientDemographicsControl.GetcardBackImage, DateTime.Now, _CardScanType.GetHashCode(), "", _UserID);
                                oPatientDemographicsControl.GetcardFrontImage.Dispose();
                                oPatientDemographicsControl.GetcardFrontImage = null;
                                oPatientDemographicsControl.GetcardBackImage.Dispose();
                                oPatientDemographicsControl.GetcardBackImage = null;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Patient code is assigned to another patient. Please enter a unique patient code.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return false;
                        }

                        if (_PatientID > 0)
                        {
                            SavePatientHistory();
                        }

                        string userAction = "";
                        long AUditTrailId = 0;

                        if (ReturnPatientID > 0)
                        {
                            if (_EMRdatabaseconnectionstring != "" && _AddPatientToEMR == true)
                            {
                                MigratePatient(ReturnPatientID, oPatient.DemographicsDetail.PatientCode);
                            }
                            AUditTrailId = gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Patient, ActivityCategory.SetupPatient, ActivityType.Modify, "Modify Patient", ReturnPatientID, 0, 0, ActivityOutCome.Success);
                            userAction = "Modify Patient";
                            if (EvntSaveandClose != null)
                                EvntSaveandClose(ReturnPatientID);
                        }
                        else
                        {
                            AUditTrailId = gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Patient, ActivityCategory.SetupPatient, ActivityType.Add, "Add Patient", ReturnPatientID, 0, 0, ActivityOutCome.Success);
                            userAction = "Add Patient";
                        }

                        clsgloPatientAudit oAudit = new clsgloPatientAudit(_databaseconnectionstring);
                        oAudit.SavePatientAuditDetails(_PatientID, AUditTrailId, userAction);
                        oAudit.Dispose();
                        oAudit = null;

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                if (res == DialogResult.No)
                {
                    oPatientDemographicsControl.txtPACode.Clear();
                    ReturnIsClose = true;
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Patient, ActivityCategory.SetupPatient, ActivityType.View, "View Patient", _PatientID, 0, 0, ActivityOutCome.Success);
                    return true;

                }
                if (res == DialogResult.Cancel)
                {
                    return false;
                }
            }
            else
            {
                ReturnIsClose = true;
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Patient, ActivityCategory.SetupPatient, ActivityType.View, "View Patient", _PatientID, 0, 0, ActivityOutCome.Success);
            }
            return true;
        }
        private bool GetPatientChangedHistory()
        {
            bool _isModified = false;

            //First Name
            if (oPatientHistoryDemographics.PatientFirstName != oPatient.DemographicsDetail.PatientFirstName)
                _isModified = true;
            else
                oPatientHistoryDemographics.PatientFirstName = "";

            //Middle Name
            if (oPatientHistoryDemographics.PatientMiddleName != oPatient.DemographicsDetail.PatientMiddleName)
                _isModified = true;
            else
                oPatientHistoryDemographics.PatientMiddleName = "";

            //Last Name
            if (oPatientHistoryDemographics.PatientLastName != oPatient.DemographicsDetail.PatientLastName)
                _isModified = true;
            else
                oPatientHistoryDemographics.PatientLastName = "";

            //Gender
            if (oPatientHistoryDemographics.PatientGender != oPatient.DemographicsDetail.PatientGender)
                _isModified = true;
            else
                oPatientHistoryDemographics.PatientGender = "";


            //Date Of Birth
            if (oPatientHistoryDemographics.PatientDOB.ToShortDateString() != oPatient.DemographicsDetail.PatientDOB.ToShortDateString())
                _isModified = true;

            //Address 1
            if (oPatientHistoryDemographics.PatientAddress1 != oPatient.DemographicsDetail.PatientAddress1)
                _isModified = true;
            else
                oPatientHistoryDemographics.PatientAddress1 = "";

            //Address 2
            if (oPatientHistoryDemographics.PatientAddress2 != oPatient.DemographicsDetail.PatientAddress2)
                _isModified = true;
            else
                oPatientHistoryDemographics.PatientAddress2 = "";

            //City
            if (oPatientHistoryDemographics.PatientCity != oPatient.DemographicsDetail.PatientCity)
                _isModified = true;
            else
                oPatientHistoryDemographics.PatientCity = "";

            //State
            if (oPatientHistoryDemographics.PatientState != oPatient.DemographicsDetail.PatientState)
                _isModified = true;
            else
                oPatientHistoryDemographics.PatientState = "";


            //Zip
            if (oPatientHistoryDemographics.PatientZip != oPatient.DemographicsDetail.PatientZip)
                _isModified = true;
            else
                oPatientHistoryDemographics.PatientZip = "";

            //County
            if (oPatientHistoryDemographics.PatientCounty != oPatient.DemographicsDetail.PatientCounty)
                _isModified = true;
            else
                oPatientHistoryDemographics.PatientCounty = "";


            //Phone
            if (oPatientHistoryDemographics.PatientPhone != oPatient.DemographicsDetail.PatientPhone)
                _isModified = true;
            else
                oPatientHistoryDemographics.PatientPhone = "";


            //SSN ** added by vishal on 26th june
            if (oPatientHistoryDemographics.PatientSSN != oPatient.DemographicsDetail.PatientSSN)
                _isModified = true;
            else
                oPatientHistoryDemographics.PatientSSN = "";


            //Communcication Preference 
            if (oPatientHistoryDemographics.PatientCommunicationPrefence != oPatient.DemographicsDetail.PatientCommunicationPrefence)
                _isModified = true;
            else
                oPatientHistoryDemographics.PatientCommunicationPrefence = "";

            return _isModified;
        }

        private void SavePatientHistory()
        {
            bool _isModified = false;
            bool _isDOBModified = false;

            //First Name
            if (oPatientHistoryDemographics.PatientFirstName != "" &&
                oPatientHistoryDemographics.PatientFirstName != oPatient.DemographicsDetail.PatientFirstName)
                _isModified = true;
            else
                oPatientHistoryDemographics.PatientFirstName = "";

            //Middle Name
            if (oPatientHistoryDemographics.PatientMiddleName != "" &&
                oPatientHistoryDemographics.PatientMiddleName != oPatient.DemographicsDetail.PatientMiddleName)
                _isModified = true;
            else
                oPatientHistoryDemographics.PatientMiddleName = "";

            //Last Name
            if (oPatientHistoryDemographics.PatientLastName != "" &&
                oPatientHistoryDemographics.PatientLastName != oPatient.DemographicsDetail.PatientLastName)
                _isModified = true;
            else
                oPatientHistoryDemographics.PatientLastName = "";

            //Gender
            if (oPatientHistoryDemographics.PatientGender != "" &&
                oPatientHistoryDemographics.PatientGender != oPatient.DemographicsDetail.PatientGender)
                _isModified = true;
            else
                oPatientHistoryDemographics.PatientGender = "";
            //* added by vishal 26th june 2009
            //SSN
            if (oPatientHistoryDemographics.PatientSSN != "" &&
                oPatientHistoryDemographics.PatientSSN != oPatient.DemographicsDetail.PatientSSN)
                _isModified = true;
            else
                oPatientHistoryDemographics.PatientSSN = "";


            //Date Of Birth
            if (oPatientHistoryDemographics.PatientDOB.ToShortDateString() != oPatient.DemographicsDetail.PatientDOB.ToShortDateString())
            {
                _isModified = true;
                _isDOBModified = true;
            }
            else
            {
                _isDOBModified = false;
            }

            //Address 1
            if (oPatientHistoryDemographics.PatientAddress1 != "" &&
                oPatientHistoryDemographics.PatientAddress1 != oPatient.DemographicsDetail.PatientAddress1)
                _isModified = true;
            else
                oPatientHistoryDemographics.PatientAddress1 = "";

            //Address 2
            if (oPatientHistoryDemographics.PatientAddress2 != "" &&
                oPatientHistoryDemographics.PatientAddress2 != oPatient.DemographicsDetail.PatientAddress2)
                _isModified = true;
            else
                oPatientHistoryDemographics.PatientAddress2 = "";

            //City
            if (oPatientHistoryDemographics.PatientCity != "" &&
                oPatientHistoryDemographics.PatientCity != oPatient.DemographicsDetail.PatientCity)
                _isModified = true;
            else
                oPatientHistoryDemographics.PatientCity = "";

            //State
            if (oPatientHistoryDemographics.PatientState != "" &&
                oPatientHistoryDemographics.PatientState != oPatient.DemographicsDetail.PatientState)
                _isModified = true;
            else
                oPatientHistoryDemographics.PatientState = "";


            //Zip
            if (oPatientHistoryDemographics.PatientZip != "" &&
                oPatientHistoryDemographics.PatientZip != oPatient.DemographicsDetail.PatientZip)
                _isModified = true;
            else
                oPatientHistoryDemographics.PatientZip = "";

            //County
            if (oPatientHistoryDemographics.PatientCounty != "" &&
                oPatientHistoryDemographics.PatientCounty != oPatient.DemographicsDetail.PatientCounty)
                _isModified = true;
            else
                oPatientHistoryDemographics.PatientCounty = "";


            //Phone
            if (oPatientHistoryDemographics.PatientPhone != "" &&
                oPatientHistoryDemographics.PatientPhone != oPatient.DemographicsDetail.PatientPhone)
                _isModified = true;
            else
                oPatientHistoryDemographics.PatientPhone = "";


            //Communcication Preference 
            if (oPatientHistoryDemographics.PatientCommunicationPrefence != "" &&
               oPatientHistoryDemographics.PatientCommunicationPrefence != oPatient.DemographicsDetail.PatientCommunicationPrefence)
                _isModified = true;
            else
                oPatientHistoryDemographics.PatientCommunicationPrefence = "";

            //patient language
            if (oPatientHistoryDemographics.PatientLanguage != "" &&
               oPatientHistoryDemographics.PatientLanguage != oPatient.DemographicsDetail.PatientLanguage)
                _isModified = true;
            else
                oPatientHistoryDemographics.PatientLanguage = "";

            //Patient Marital status
            if (oPatientHistoryDemographics.PatientMaritalStatus != "" &&
                oPatientHistoryDemographics.PatientMaritalStatus != oPatient.DemographicsDetail.PatientMaritalStatus)
                _isModified = true;
            else
                oPatientHistoryDemographics.PatientMaritalStatus = "";
           
            //PatientInboundHospital
            if (sPatientInboundHospital != "")
            {
                _isModified = true;
            }
            else
            {
                sPatientInboundHospital = "";
            }
          


             //InboundTransactionOfCare
            if (sPatientInboundTranCare != "")
            {
                _isModified = true;
            }
            else
            {
                sPatientInboundTranCare = "";
            }
             

            if (_isModified == true)
            {
                oPatientHistoryDemographics.PatientID = _PatientID;
                oPatientTrans.AddPatientChangeHistory(oPatientHistoryDemographics, _isDOBModified,sPatientInboundHospital:sPatientInboundHospital ,sPatientInboundTranCare :sPatientInboundTranCare );
            }

        }

        private void oPatientDemographicsControl_onDemographicControl_Enter(object sender, EventArgs e)
        {
            ts_Commands.Visible = true;
            pnlTOP.Dock = DockStyle.Top;
            panel1.Visible = true;
            pnlContainer.Dock = DockStyle.Top;
            //pnlContainer.Dock = DockStyle.Fill;
            this.Refresh();
            //if (Screen.PrimaryScreen.Bounds.Height < 900)
            //{
            //    pnlContainer.AutoScroll = true;
            //}
        }

        private void oPatientDemographicsControl_onDemographicControl_Leave(object sender, EventArgs e)
        {
            
            ts_Commands.Visible = false;
            pnlTOP.Dock = DockStyle.None;
            panel1.Visible = false;
            pnlContainer.Dock = DockStyle.Fill;
            this.Refresh();
            //Button btn = (Button)sender;
            //if (btn.Name != "btnGuardianSelect")
            //{
            //    if (Screen.PrimaryScreen.Bounds.Height < 900)
            //    {
            //        pnlContainer.AutoScroll = false;
            //    }
            //}
        }

        #region "Print PatientRegistration Report"

        private void PrintPatientRegistration()
        {
            Callprint(ReturnPatientID.ToString());
        }

        private DataTable Getdata(string PatientID)
        {
            DataTable dt = new DataTable();
           
            try
            {
                string str = "select iPhoto  from VWRptPatientInfo where PatientId  = '" + PatientID + "'";
                if (_databaseconnectionstring != "")
                {
                    string sqlConn = _databaseconnectionstring;
                    System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(sqlConn);
                    System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(str, conn);
                    da.Fill(dt);
                    da.Dispose();
                    da = null;
                    conn.Close();
                    conn.Dispose();
                    conn=null;
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                Ex = null;
            }
            finally
            {
                //if (dt != null)
                //{
                //    dt.Dispose();
                //    dt = null;
                //}
            }
            return dt;
        }
        private byte[] SaveImage(string PatientID, int patientPhotoWidth, int patientPhotoHeight)
        {
            DataTable dt_GetPatientImage = null;
          
            try
            {
                //PatientPhoto = null;
                dt_GetPatientImage = Getdata(PatientID);
                if (dt_GetPatientImage != null)
                {
                    if (dt_GetPatientImage.Rows.Count > 0)
                    {
                        if (dt_GetPatientImage.Rows[0][0].ToString() != "")
                        {
                         //   byte[] value = (byte[])(dt_GetPatientImage.Rows[0][0]);

                            return gloPictureBox.gloImage.GetImageByteArray((byte[])(dt_GetPatientImage.Rows[0]["iPhoto"]), patientPhotoWidth, patientPhotoHeight);

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {
                if (dt_GetPatientImage != null)
                {
                    dt_GetPatientImage.Dispose();
                    dt_GetPatientImage = null;
                }
            }
            return null;

        }

        //private static byte[] GetImageByteArray(int patientPhotoWidth, int patientPhotoHeight,byte[] byteArray)
        //{
             
        //    byte[] arrByteImage = null;
        //    using (gloPictureBox.gloPictureBox myPixBx = new gloPictureBox.gloPictureBox())
        //    {
        //        myPixBx.byteImage = byteArray;

        //        using (System.Drawing.Image PatientPhoto = myPixBx.copyFrame(new Rectangle(0, 0, patientPhotoWidth, patientPhotoHeight), true, true))
        //        {

        //            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
        //            {

        //                try
        //                {
        //                    PatientPhoto.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
        //                }
        //                catch (Exception ex)
        //                {
        //                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
        //                    ex = null;
        //                }
        //                ms.Close();
        //                arrByteImage = ms.ToArray();
        //            }
        //        }
                
        //    }
        //    return arrByteImage;
        //}

        private void Callprint(string PatientID)
        {
            //ReportDocument oRpt = default(ReportDocument);
            //PatientImage dsImgRp = new PatientImage();
            //PatientImage.ImageRow dr = null;
            try
            {
                //oRpt = new ReportDocument();

                //if (System.IO.Directory.Exists(Application.StartupPath + "\\Reports") == true)
                //{
                //    oRpt.Load(Application.StartupPath + "\\Reports\\rptPatientRegDeta.rpt");

                //}
                //else
                //{
                //    MessageBox.Show("Reports Directory does not exists.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}

                //TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                //TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                //ConnectionInfo crConnectionInfo = new ConnectionInfo();
                //Tables CrTables;

                //{
                //    crConnectionInfo.ServerName = Convert.ToString(appSettings["SQLServerName"]);

                //    crConnectionInfo.DatabaseName = Convert.ToString(appSettings["DatabaseName"]);

                //    if (Convert.ToBoolean(appSettings["WindowAuthentication"]) == false)
                //    {
                //        crConnectionInfo.IntegratedSecurity = false; //Convert.ToBoolean(appSettings["WindowAuthentication"]);
                //        crConnectionInfo.UserID = Convert.ToString(appSettings["SQLLoginName"]);
                //        crConnectionInfo.Password = Convert.ToString(appSettings["SQLPassword"]);
                //    }
                //    else
                //    {
                //        crConnectionInfo.IntegratedSecurity = true;
                //    }
                //}

                //CrTables = oRpt.Database.Tables;

                //int patientPhotoHeight = 2025; // oRpt.ReportDefinition.Sections["GroupHeaderSection1"].ReportObjects["Image1"].Height;
                //int patientPhotoWidth = 1440; //oRpt.ReportDefinition.Sections["GroupHeaderSection1"].ReportObjects["Image1"].Width;
                ////dr = dsImgRp.Image.NewImageRow();
                //byte[] myBytes = SaveImage(PatientID, patientPhotoWidth, patientPhotoHeight);

                ////Inserting modified image into PatientPhoto table to access in report
                //if (myBytes != null)
                //{
                //    InsertpatientPhoto(myBytes);
                //}
                //else
                //{
                //    DeletepatientPhoto();
                //}

                PrintReport();

                //dr.Image = myBytes;
                //dsImgRp.Image.Rows.Add(dr);

                //foreach (Table CrTable in CrTables)
                //{
                //    if (CrTable.Name.ToUpper() == "Image".ToUpper())
                //    {

                //        if (dsImgRp.Tables[0].Rows.Count > 0)
                //        {
                //            oRpt.Database.Tables["Image"].SetDataSource(dsImgRp.Tables[0]); //SetDataSource(dsImgRp);
                //        }
                //    }
                //    else
                //    {
                //        crtableLogoninfo = CrTable.LogOnInfo;
                //        crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                //        CrTable.ApplyLogOnInfo(crtableLogoninfo);

                //        CrTable.Location = "" + ".dbo." + CrTable.Name;
                //    }
                //}

                //oRpt.SetParameterValue("PatientID", PatientID);

                //bool _DefaultPrinter = false;
                //if (appSettings["DefaultPrinter"] != null)
                //{
                //    if (appSettings["DefaultPrinter"] != "")
                //    {
                //        _DefaultPrinter = Convert.ToBoolean(appSettings["DefaultPrinter"]);
                //    }
                //    else
                //    {
                //        _DefaultPrinter = false;
                //    }
                //}
                //else
                //{
                //    _DefaultPrinter = false;
                //}
                //if (oRpt != null)
                //{
                //    if (_DefaultPrinter == false)
                //    {
                //        PrintDialog1 = new PrintDialog();
                //        if (PrintDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                //        {
                //            oRpt.PrintOptions.PrinterName = PrintDialog1.PrinterSettings.PrinterName;
                //            oRpt.PrintToPrinter(1, false, 0, 0);
                //        }
                //    }
                //    else
                //    {
                //        oRpt.PrintToPrinter(1, false, 0, 0);
                //    }
                //}
            }

            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {
                //if (dsImgRp != null)
                //{
                //    dsImgRp.Dispose();
                //    dsImgRp = null;
                //}
            }


        }

        private void InsertpatientPhoto(byte[] image)
        {
            DeletepatientPhoto();

            string query = "INSERT INTO PatientPhoto (Photo) VALUES (@Content)";
            using (System.Data.SqlClient.SqlConnection _con = new System.Data.SqlClient.SqlConnection(_databaseconnectionstring))
            using (System.Data.SqlClient.SqlCommand _cmd = new System.Data.SqlClient.SqlCommand(query, _con))
            {
                System.Data.SqlClient.SqlParameter param = _cmd.Parameters.Add("@Content", SqlDbType.VarBinary);
                param.Value = image;

                _con.Open();
                _cmd.ExecuteNonQuery();
                _con.Close();
                _cmd.Parameters.Clear();
            }
        }

        private void DeletepatientPhoto()
        {
            string query = "delete from PatientPhoto";
            using (System.Data.SqlClient.SqlConnection _con = new System.Data.SqlClient.SqlConnection(_databaseconnectionstring))
            using (System.Data.SqlClient.SqlCommand _cmd = new System.Data.SqlClient.SqlCommand(query, _con))
            {
                _con.Open();
                _cmd.ExecuteNonQuery();
                _con.Close();
                _cmd.Parameters.Clear();
            }
        }

        private void PrintReport()
        {
            gloSSRSApplication.clsPrintReport clsPrntRpt = null;
            string _MessageBoxCaption = string.Empty;
            string _databaseConnectionString = string.Empty;
            string _LoginName = string.Empty;
            string gstrSQLServerName = string.Empty;
            string gstrDatabaseName = string.Empty;
            string gstrSQLUserEMR = string.Empty;
            string gstrSQLPasswordEMR = string.Empty;
            string ParameterValue = null;
            string ParameterName = null;

            bool gblnDefaultPrinter = false;
            bool gblnSQLAuthentication = false; 

            try
            {
                if (!string.IsNullOrEmpty(appSettings["DataBaseConnectionString"]))
                {
                    _databaseConnectionString = Convert.ToString(appSettings["DataBaseConnectionString"]);
                }

                if (!string.IsNullOrEmpty(appSettings["UserName"]))
                {
                    _LoginName = Convert.ToString(appSettings["UserName"]);
                }

                if (!string.IsNullOrEmpty(appSettings["SQLServerName"]))
                {
                    gstrSQLServerName = Convert.ToString(appSettings["SQLServerName"]);
                }

                if (!string.IsNullOrEmpty(appSettings["DatabaseName"]))
                {
                    gstrDatabaseName = Convert.ToString(appSettings["DatabaseName"]);
                }

                if (!string.IsNullOrEmpty(appSettings["SQLLoginName"]))
                {
                    gstrSQLUserEMR = Convert.ToString(appSettings["SQLLoginName"]);
                }

                if (!string.IsNullOrEmpty(appSettings["SQLPassword"]))
                {
                    gstrSQLPasswordEMR = Convert.ToString(appSettings["SQLPassword"]);
                }

                if (!string.IsNullOrEmpty(appSettings["DefaultPrinter"]))
                {
                    gblnDefaultPrinter = !Convert.ToBoolean(appSettings["DefaultPrinter"]);
                }
                else 
                {
                    gblnDefaultPrinter = true;
                }

                if (!string.IsNullOrEmpty(appSettings["WindowAuthentication"]))
                {
                    gblnSQLAuthentication = !Convert.ToBoolean(appSettings["WindowAuthentication"]);
                }
               
                ParameterValue = _LoginName + "," + ReturnPatientID.ToString();
                ParameterName = "user,nPatientID";

                clsPrntRpt = new gloSSRSApplication.clsPrintReport(gstrSQLServerName, gstrDatabaseName, gblnSQLAuthentication, gstrSQLUserEMR, gstrSQLPasswordEMR);
                clsPrntRpt.PrintReport("rptPatientDetail", ParameterName, ParameterValue, gblnDefaultPrinter,"" );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (clsPrntRpt != null)
                { clsPrntRpt.Dispose();
                  clsPrntRpt = null;
                }

                _MessageBoxCaption = null;
                _databaseConnectionString = null;
                _LoginName = null;
                gstrSQLServerName = null;
                gstrDatabaseName = null;
                gstrSQLUserEMR = null;
                gstrSQLPasswordEMR = null;
                ParameterValue = null;
                ParameterName = null;
            }
        }

        #endregion

        private void frmSetupPatient_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isSaveAndClose == false)
            {
                //Bug #31871: Copy Patient > Duplicate patient code message displayed twice.
                if (CloseForm() == false)
                {               
                    e.Cancel = true;
                }
            }                        
        }

        private void tsb_Cancel_Click(object sender, EventArgs e)
        {

        }

        #region "Save Card Data"
        public bool SaveScanData(Int64 PatientID, System.Drawing.Image ScanImage, System.Drawing.Image BackImage, DateTime ScanDate, Int64 CardTypeId, string ScannedData, Int64 nUserId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            try
            {
                if (oDB != null)
                {
                    oDB.Connect(false);
                    oParameters.Add("@nPatientID", PatientID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                    System.Drawing.Image ilogo = (System.Drawing.Image)ScanImage.Clone();
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    if (ms != null)
                    {
                        ilogo.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        try
                        {
                            ms.Flush();
                        }
                        catch
                        {
                        }
                        Byte[] arrImage = ms.ToArray();
                        oParameters.Add("@iCard", arrImage, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Image);
                        try
                        {
                            ms.Close();


                            ms.Dispose();
                            ms = null;
                        }
                        catch
                        {
                        }
                    }

                    if (ilogo != null)
                    {
                        ilogo.Dispose();
                        ilogo = null;
                    }

                    if (BackImage == null)
                    {
                        oParameters.Add("@iCardBack", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Image);
                    }
                    else
                    {
                        System.Drawing.Image iBack = (System.Drawing.Image) BackImage.Clone();
                        System.IO.MemoryStream msImageBack = new System.IO.MemoryStream();
                        if (msImageBack != null)
                        {
                            iBack.Save(msImageBack, System.Drawing.Imaging.ImageFormat.Jpeg);
                            try
                            {
                                msImageBack.Flush();
                            }
                            catch
                            {
                            }
                            Byte[] arrImageBack = msImageBack.ToArray();
                            oParameters.Add("@iCardBack", arrImageBack, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Image);
                            try
                            {
                                msImageBack.Close();


                                msImageBack.Dispose();
                                msImageBack = null;
                            }
                            catch
                            {
                            }
                        }
                        if (iBack != null)
                        {
                            iBack.Dispose();
                            iBack = null;
                        }
                    }


                    oParameters.Add("@dtScanDateTime", ScanDate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.DateTime);
                    oParameters.Add("@nCardTypeID", CardTypeId, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                    oParameters.Add("@sScannedData", ScannedData, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);

                    oParameters.Add("@nUserId", nUserId, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);

                    int _res = oDB.Execute("gsp_IN_PatientCards", oParameters);

                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oParameters.Dispose();
            }
        }
        #endregion

        //Bug #31871: Copy Patient > Duplicate patient code message displayed twice.
        private void frmSetupPatient_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Bug #62022: 00000606 : EMR PATIENT RELATED LIQUID LINKS CREATE FALSE WARNING WHEN TRIPLE-CLICKED 
            //added form level locking for patient registration.
            if (LockID!=0)
            {
                Scan_n_Unlock_FormLevel(LockID, _PatientID);
            }
            oPatientDemographicsControl.onDemographicControl_Enter -= new gloPatientDemographicsControl.onDemographicControlEnter(oPatientDemographicsControl_onDemographicControl_Enter);
            oPatientDemographicsControl.onDemographicControl_Leave -= new gloPatientDemographicsControl.onDemographicControlLeave(oPatientDemographicsControl_onDemographicControl_Leave);
  
            oPatientDemographicsControl.DisposeAllControls();
            if (oPatientDemographicsControl != null) { oPatientDemographicsControl.Dispose(); oPatientDemographicsControl = null; }
            if (oPatientHistoryDemographics != null) { oPatientHistoryDemographics.Dispose(); oPatientHistoryDemographics = null; }
            if (oPatientTrans != null) { oPatientTrans.Dispose(); oPatientTrans = null; }
            if (oPatient != null) { oPatient.Dispose(); oPatient = null; }
            if (_ScanedInsurance != null) { _ScanedInsurance.Dispose(); _ScanedInsurance = null; }
            if (_ScannedPatient != null) { _ScannedPatient.Dispose(); _ScannedPatient = null; }
        }


        //Patient Portal
        Boolean blnPatientPortalEnabled = false;
        ClsMessageQueue oclsMessageQueue;
        public void IsPatientPortalEnabled()
        {
            
            if ((oclsMessageQueue != null))
            {
                oclsMessageQueue = null;
            }

            oclsMessageQueue = new ClsMessageQueue(_databaseconnectionstring, DateTime.Now, 0);
            blnPatientPortalEnabled= oclsMessageQueue.IsPatientPortalEnabled();
            oclsMessageQueue = null;
        }
        
        private void SendPatientPortalEmails(Int64 PatientID)
        {
            if (blnPatientPortalEnabled && gblnPatientPortalSendActivationEmail)
            {
                if ((oclsMessageQueue != null))
                {
                    oclsMessageQueue = null;
                }

                oclsMessageQueue = new ClsMessageQueue(_databaseconnectionstring, DateTime.Now, PatientID);
                oclsMessageQueue.SendPortalEmails("PatientPortal", gblnPatientPortalSendActivationEmail, gblnPatientPortalActivationEmailAlreadySent);
                oclsMessageQueue = null;
            }
        }
        //Patient Portal
        clsAPIAcceess objclsAPIAcceess = new clsAPIAcceess();
        private void APIActivation(Int64 PatientID, string sEmail)
        {
            if (gblnAPIActivation)
            {
                if ((objclsAPIAcceess != null))
                {
                    objclsAPIAcceess = null;
                }

                APIAccess[] arrAPIAccess = new APIAccess[1];

                APIAccess objAPIAccess = new APIAccess();
                objAPIAccess.APIUserID = PatientID;
                objAPIAccess.UserName = sEmail;
                objAPIAccess.Password = "";

                arrAPIAccess[0] = objAPIAccess;

                objclsAPIAcceess = new clsAPIAcceess();
                Int64 _result = -1;
                _result =  objclsAPIAcceess.APIAccessProceess(_databaseconnectionstring, arrAPIAccess, 1, 1, "", DateTime.Now);
                objclsAPIAcceess = null;
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.API, gloAuditTrail.ActivityCategory.APIUser, gloAuditTrail.ActivityType.Activate, "API activated for patient ", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);

            }
        }

        private string getIDs(Int64 nPatientID, int nIDFor)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dt = null;
            string _sqlQuery = "";
            string sIDs=string.Empty;
            try
            {
                oDB.Connect(false);
                if (nPatientID > 0) 
                {
                    switch (nIDFor)
                    {
                        case 0://nMessageQueueID
                            {
                                _sqlQuery = " SELECT nMessageID as nID FROM dbo.Gl_Messagequeue WHERE nPatientID =  " + nPatientID + "AND sMessageName='PATIENTINVITATION' AND sServiceName = 'PatientPortal' AND nStatus IN (1, 0) ";
                                break;
                            }
                        case 1://nPatientDetailID
                            {
                                _sqlQuery = " SELECT nPatientDetailID as nID FROM dbo.Patient_DTL AS PD WHERE  PD.nContactFlag=3 AND ISNULL(bMuCheckBox,0)=1 AND PD.nPatientID=  " + nPatientID;
                                break;
                            }
                        case 2://nID from MU_Pat_Hos
                            {
                                _sqlQuery = " SELECT ID as nID FROM dbo.MU_Pat_Hosp AS MPH WHERE ISNULL(chkStatus,0)=1 AND MPH.PatientID=  " + nPatientID;
                                break;
                            }
                        case 3://nAPIAccessID
                            {
                                _sqlQuery = " SELECT nAPIAccessID as nID  FROM dbo.APIAccess WHERE nAPIAccessUserID =  " + nPatientID + "AND bAllowAccess=1";
                                break;
                            }
                    }
                    oDB.Retrive_Query(_sqlQuery, out dt);
                }

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (sIDs == "")
                        {
                            sIDs = Convert.ToString(dr["nID"]);
                            //nReturnID = Convert.ToInt64(dr["nID"]);
                        }
                        else
                        {
                            sIDs += "," + Convert.ToString(dr["nID"]);
                        }
                    }
                }

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return "";
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }

            }
            return sIDs;
        }

        public bool getProviderTaxID(Int64 nProviderID = 0)
        {
            sProviderTaxID = "";
            nProviderAssociationID = 0;
            try
            {
                DialogResult oResult = System.Windows.Forms.DialogResult.OK;
                gloGlobal.frmSelectProviderTaxID oForm = new gloGlobal.frmSelectProviderTaxID(Convert.ToInt64(nProviderID));
                if (oForm.dtProviderTaxIDs != null && oForm.dtProviderTaxIDs.Rows.Count > 1)
                {
                    oForm.ShowDialog(this);
                    oResult = oForm.DialogResult;
                    nProviderAssociationID = oForm.nAssociationID;
                    sProviderTaxID = oForm.sProviderTaxID;

                    oForm = null;
                }
                else if (oForm.dtProviderTaxIDs != null && oForm.dtProviderTaxIDs.Rows.Count == 1)
                {
                    //'oResult = oForm.DialogResult
                    nProviderAssociationID = Convert.ToInt64(oForm.dtProviderTaxIDs.Rows[0]["nAssociationID"]);
                    sProviderTaxID = Convert.ToString(oForm.dtProviderTaxIDs.Rows[0]["sTIN"]);
                    oForm = null;
                }
                else
                {
                    nProviderAssociationID = 0;
                    sProviderTaxID = "";
                }

                if (oResult == System.Windows.Forms.DialogResult.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;

            }
            finally
            {
            }
        }
    }
}
