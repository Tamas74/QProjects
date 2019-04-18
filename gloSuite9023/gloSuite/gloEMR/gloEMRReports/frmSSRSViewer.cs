using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using gloUserControlLibrary;
using gloEMRGeneralLibrary.gloEMRDatabase;
using gloSSRSApplication;
using System.IO;
using Microsoft.Reporting.WinForms;
using gloSnoMed;

//using gloUserControlLibrary;



namespace gloEMRReports
{
    public partial class frmSSRSViewer : Form
    {
        DataTable dtTemp = null;
        string FOR_ALL = "For All";
        string FOR_AGE = "For Age";
        string FOR_LESSTHAN_AGE = "Less Than";
        string FOR_GREATERTHAN_AGE = "Greater Than";
        string FROMTO_AGE = "Between";
        string LabTestResult = string.Empty;
        string LabTestResult1 = string.Empty;
        //string[] ColoumNames;
        //string[] ResultCondition;
//        DataTable oResult = new DataTable();

        string strReportProtocol = string.Empty;
        string strReportServer = string.Empty;
        string strReportFolder = string.Empty;
        string strVirtualDir = string.Empty;

        string _reportName = string.Empty;
        string _ParaName = string.Empty;
        string _ParaValue = string.Empty;

        string strParameter = string.Empty;
        string _UserName = string.Empty;
        string _connectionstring = string.Empty;

        Int64 _UserID = 0;
        Int64 _ClinicID = 0;
        string _conn = string.Empty;
        string _reportTitle = string.Empty;
        string reportParam = string.Empty;
        private Int64 _DefaultProviderID = 0;
        private bool IsICD9Checked = false;  
        bool _IsgloStreamReport;
        System.Uri SSRSReportURL;
        string _databaseconnectionstring = string.Empty;

        string strLst = "";
        // chetan added for patient reminder on 20 oct 2010
        public string gstrProblemtype = "";
        private string _Medications = "";
        private string _MedicationsDisplay = "";

        private string _ProblemLists = "";
        public string sPatientIDs { get; set; }


        private string _DMCriteriaIDs = "";

        //8020 Code Review: Dummy events written, email dt: 05-05-2014 Sub: Please update
        //public delegate void GenerateReportClick(object sender, EventArgs e, SSRSArgs eSSRS);
        //public event GenerateReportClick GenerateReport_Click;
        private const int COL_Select = 0;
        private const int COL_PatientCode = 1;
        private const int COL_PatientID = 2;
        private const int COL_PatientFName = 3;
        private const int COL_PatientMName = 4;
        private const int COL_PatientLName = 5;
        private const int COL_DOB = 6;
        private const int COL_age = 7;
        private const int COL_Gender = 8;
        private const int COL_ProviderID = 9;
        private const int COL_PRoviderName = 10;
        private const int COL_sCommunicationPreference = 11;
        private const int COL_ApptStatus = 12;

        // chetan added for patient reminder on 20 oct 2010
        System.Collections.ArrayList arrdm = new System.Collections.ArrayList();
        public delegate void SendReminderLetter(object sender, EventArgs e, PatientArgs eDT);

        public event SendReminderLetter SendReminder_Letter;
        private CustomTask dgCustomGrid = null;
        Boolean bHistory = false;
        private string gstrMessageBoxCaption = "gloEMR";
        DataTable dt = null;
        private int Col_Check = 2;
        private int Col_Name = 0;
        //private int Col_Dosage = 1;
        private int Col_NDCCode = 3;
        private int Col_Count = 4;

        Boolean bDM = false;
  //      DataTable oDiag = new DataTable();
    //    DataTable oCPT = new DataTable();
        DataSet dsDemographic = null;
        private string gstrSQLServerName;
        private string gstrDatabaseName;
        private bool gblnSQLAuthentication;
        private string gstrSQLUser;
        private string gstrSQLPassword;
        private string RptName = "";
        private bool _ICD10Transition = false;  //added for ICd10 implemantation
        Int64 ProviderID = 0;
        string AgeCri = "For All";
        int FromAge = 0;
        int ToAge = 0;
        string DateSelect = "False";
        string includedetails = "True";
        string OnlyDrugAllergy = "";

        List<Microsoft.Reporting.WinForms.ReportParameter> paramList = new List<Microsoft.Reporting.WinForms.ReportParameter>();
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private string _strSnoConnstring;
        Dictionary<string, string> _DictionaryICD9 = new Dictionary<string, string>();
        Dictionary<string, string> _DictionaryClaimCPT = new Dictionary<string, string>();
        Dictionary<string, string> _DictionaryClaimDx = new Dictionary<string, string>();
        Dictionary<string, string> _DictionarySnomedCodeCT = new Dictionary<string, string>();
        Dictionary<string, string> _DictionaryFacility = new Dictionary<string, string>();
        private string strShowingdisplayoption = "";
        Dictionary<string, string> _DictionaryPatInsPlan = new Dictionary<string, string>();
    //    private string strgetShowingdisplayoption = "";

        public Boolean gblnPatientPortalEnabled = false;
        public Boolean gblnIntuitCommunication = false;

        //Parameter for OO-Outstanding Orders
        public String sOOTabName = "";
        public long nOOProviderId = 0;
        public String sOODateCategory = "";
        public DateTime dtOOFromdate;
        public DateTime dtOOTodate;
        public Int32 nOOOrderStatusNumber = 0;
        public bool IsOOResultedOrderDate = false;

      


        public string StrSnoConnstring
        {
            get { return _strSnoConnstring; }
            set { _strSnoConnstring = value; }
        }

        public string reportName
        {
            get { return _reportName; }
            set { _reportName = value; }
        }

        public string Conn
        {
            get { return _conn; }
            set { _conn = value; }
        }

        public bool ICD10Transition //added for ICd10 implemantation
        {
            get { return _ICD10Transition; }
            set { _ICD10Transition = value; }
        }
        public string reportTitle
        {
            get { return _reportTitle; }
            set { _reportTitle = value; }
        }

        public bool IsgloStreamReport
        {
            get { return _IsgloStreamReport; }
            set { _IsgloStreamReport = value; }
        }

        public string databaseconnectionstring
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }
        }

        public Int64 DefaultProviderID
        {
            get { return _DefaultProviderID; }
            set { _DefaultProviderID = value; }
        }

        public frmSSRSViewer(string _gstrSQLServerName, string _gstrDatabaseName, bool _gblnSQLAuthentication, string _gstrSQLUser, string _gstrSQLPassword)
        {

            //SANDEEP DARADE-20100604

            gstrSQLServerName = _gstrSQLServerName;
            gstrDatabaseName = _gstrDatabaseName;
            gblnSQLAuthentication = _gblnSQLAuthentication;
            gstrSQLUser = _gstrSQLUser;
            gstrSQLPassword = _gstrSQLPassword;
            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                {
                    _UserName = Convert.ToString(appSettings["UserName"]);
                }
                else
                {
                    _UserName = "";
                }
            }
            else
            { _UserName = ""; }


            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                {
                    _UserID = Convert.ToInt64(appSettings["UserID"]);
                }
                else
                {
                    _UserID = 0;
                }
            }
            else
            { _UserID = 0; }


            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                {
                    _ClinicID = Convert.ToInt64(appSettings["ClinicID"]);
                }
                else
                {
                    _ClinicID = 0;
                }
            }
            else
            { _ClinicID = 0; }


            if (appSettings["DataBaseConnectionString"] != null)
            {
                if (appSettings["DataBaseConnectionString"] != "")
                {
                    _connectionstring = Convert.ToString(appSettings["DataBaseConnectionString"]);
                }
                else
                {
                    _connectionstring = string.Empty;
                }
            }
            else
            { _connectionstring = string.Empty; }

            InitializeComponent();
        }

        private void frmSSRSViewer_Load(object sender, EventArgs e)
        {
            this.Text = _reportTitle;
            dtpicFrom.Text = (Convert.ToDateTime(dtpicTo.Value.Date.AddMonths(-3).ToShortDateString())).ToString();
            dtpicFrom.Checked = true;
            Tblbtn_More.Visible = false;
            pnlDrugDiagnosis.Visible = false;
            _databaseconnectionstring = Conn;
            SSRSViewer.Visible = false;

            pnlWarning.Visible = false;

            if (_reportName == "AllergyUsageReport")
            {
                RptName = "AllergyUsageReport";
                chkOnlyDrug.Visible = true;
                pnlDemo.Visible = false;
                chkShowUsageDeatal.Checked = false;
            }

            else if (_reportName == "rptPatientVitalUsage")
            {
                RptName = "VitalUsageReport";
                chkShowUsageDeatal.Checked = false;
                chkOnlyDrug.Visible = false;
                pnlDemo.Visible = false;
                SetAgeCr();
                //pnlMessage.Visible = false;
            }
            else if (_reportName == "DemographicUsageReport")
            {
                RptName = "DemographicUsageReport";
                chkOnlyDrug.Visible = false;
                pnlDemo.Visible = true;
                chkAll.Checked = true;
                chkShowUsageDeatal.Checked = false;
                //pnlMessage.Visible = false;
            }

            else if (_reportName == "ProblemUsageReport")
            {
                RptName = "ProblemUsageReport";
                chkOnlyDrug.Visible = false;
                pnlDemo.Visible = false;
                chkShowUsageDeatal.Checked = false;
                //pnlMessage.Visible = false;
            }

            else if (_reportName == "rptMedicationMU")
            {
                RptName = "MedicationUsageReport";
                chkOnlyDrug.Visible = false;
                pnlDemo.Visible = false;
                chkShowUsageDeatal.Checked = false;
                //pnlMessage.Visible = false;
            }

            else if (_reportName == "rpteRx")
            {
                RptName = "PrescriptionUsageReport";
                chkOnlyDrug.Visible = false;
                pnlDemo.Visible = false;
                pnlDiag.Visible = false;
                chkShowUsageDeatal.Checked = false;
                Tblbtn_More.Visible = true;
            }
            else if (_reportName == "rptPatientPrescriptions")
            {

                Panel3.Visible = false;
                lblMedication.Text = "Medication :";
                //lblMedication.Location = new Point(10, 0);
                //pnlMed.Location = new Point(0, 0);
                RptName = "PrescriptionUsageReport";
                chkOnlyDrug.Visible = false;
                pnlDemo.Visible = false;
                pnlDiag.Visible = false;
                chkShowUsageDeatal.Checked = false;
                Tblbtn_More.Visible = true;
                rbtnAllMedications.Visible = true;
                rbtnPresByClinic.Visible = true;
            }

            else if (_reportName == "rptPatientHistoryUsage")
            {
                RptName = "HistoryUsageReport";
                chkOnlyDrug.Visible = false;
                pnlDemo.Visible = false;
                Tblbtn_More.Visible = true;
                lblMedication.Text = "Category: ";
                lblDiagnosis.Text = "History Items: ";
                //chkShowUsageDeatal.Checked = false;
                //lblDiagnosis.Left = 200;
                bHistory = true;
                chkShowUsageDeatal.Checked = false;
                //pnlMessage.Visible = false;
            }
            //Roopali
            else if (_reportName == "rptPatientList")
            {
                RptName = "rptPatientList";
                Panel3.Visible = false;
                Tblbtn_More.Visible = true;
                pnlPatientList.Visible = false;
                pnlClaimDetails.Visible = false;
                chkShowAllProviders.Visible = true;
                cmb3rdPat.SelectedIndex = 0;
                //dtpicFrom.Visible = false;
                //dtpicTo.Visible = false;
                //lblDate.Visible = false;
                //lblFrom.Visible = false;
                //lblTo.Visible = false;
                cmbFstPat.SelectedIndex = 0;
                cmbSndPat.SelectedIndex = 0;
                cmbThiPat.SelectedIndex = 0;
                cmbPatCondition.SelectedIndex = 0;
                cmbMediAll.SelectedIndex = 0;
                tsb_ReminderLetters.Visible = false;

                //chkPrblLDOB.Checked = true;
                //chkPrblLAge.Checked = true;
                //chkPrblLstGender.Checked = true;
                //chkPrblmLstOthrProblemList.Checked = true;
                //chkPrblmLstOthrMedication.Checked = true;
                //chkPrblmLstOthrImmunization.Checked = true;
                //chkPrblmLstOthrLabResult.Checked = true;  
                setDemoFilterData();
                Getuserdisplayoption();
                //cmbLabResult.BackColor = Color.White;  


            }

            else if (_reportName == "rptPatientOB")
            {
                RptName = "rptPatientOB";
                Panel3.Visible = false;
                Tblbtn_More.Visible = true;
                pnlPatientList.Visible = false;
                pnlClaimDetails.Visible = false;
                cmb3rdPat.SelectedIndex = 0;
                cmbAge.Visible = false;
                Lblage.Visible = false;
                Tblbtn_More.Visible = false;   
                //dtpicFrom.Visible = false;
                //dtpicTo.Visible = false;
                //lblDate.Visible = false;
                //lblFrom.Visible = false;
                //lblTo.Visible = false;
                cmbFstPat.SelectedIndex = 0;
                cmbSndPat.SelectedIndex = 0;
                cmbThiPat.SelectedIndex = 0;
                cmbPatCondition.SelectedIndex = 0;
                cmbMediAll.SelectedIndex = 0;
                tsb_ReminderLetters.Visible = false;

                //chkPrblLDOB.Checked = true;
                //chkPrblLAge.Checked = true;
                //chkPrblLstGender.Checked = true;
                //chkPrblmLstOthrProblemList.Checked = true;
                //chkPrblmLstOthrMedication.Checked = true;
                //chkPrblmLstOthrImmunization.Checked = true;
                //chkPrblmLstOthrLabResult.Checked = true;  
                setDemoFilterData();
                Getuserdisplayoption();
                //cmbLabResult.BackColor = Color.White;  
                dtpicFrom.Value = DateTime.Now;
                dtpicTo.Value = DateTime.Now.AddYears(1);  
              
            }
            // chetan added for patient reminder report on 20 oct 2010
            else if (_reportName == "rptPatientListDM")
            {
                RptName = "rptPatientListDM";
                EnableDisableDueDate(false);
                Panel3.Visible = false;
                Tblbtn_More.Visible = true;
                tblButtonShow.Visible = true;
                // pnlPatientList.Visible = false;
                pnlDmPatientList.Visible = false;
                dtFromAppt.Enabled = false;
                dtToAppt.Enabled = false;
                SSRSViewer.Visible = true;
                //dtpicFrom.Checked = false;
                tblbtn_Print_32.Visible = false;
                tblbtn_Export.Visible = false;
                pnlWarning.Visible = true;
                FillApptStatus();
            }
            else if (_reportName == "rptReconciliationFile")
            {
                RptName = "rptReconciliationFile";
                Panel3.Visible = false;
                Tblbtn_More.Visible = false;
                tblButtonShow.Visible = false;
                // pnlPatientList.Visible = false;
                pnlDmPatientList.Visible = false;
                dtFromAppt.Enabled = false;
                dtToAppt.Enabled = false;
                SSRSViewer.Visible = true;
                dtpicFrom.Checked = false;
                pnlProvider.Visible = false;
                pnlWarning.Visible = false;
                pnlMessage.Visible = false;

                tblbtnGenReport.Visible = false;
                tblbtn_Print_32.Visible = false;
                tblbtn_Export.Visible = false;
                Tblbtn_More.Visible = false;

                pnlToolStrip.Visible = true;


                Loadreport();
            }
            else if (_reportName == "rptReconciliationList")
            {
                RptName = "rptReconciliationList";
                Panel3.Visible = false;
                Tblbtn_More.Visible = false;
                tblButtonShow.Visible = false;
                // pnlPatientList.Visible = false;
                pnlDmPatientList.Visible = false;
                dtFromAppt.Enabled = false;
                dtToAppt.Enabled = false;
                SSRSViewer.Visible = true;
                dtpicFrom.Checked = false;
                pnlProvider.Visible = false;
                pnlWarning.Visible = false;
                pnlMessage.Visible = false;
                pnlWarning.Visible = false;

                tblbtnGenReport.Visible = false;
                tblbtn_Print_32.Visible = true;
                tblbtn_Export.Visible = false;
                Tblbtn_More.Visible = false;

                pnlToolStrip.Visible = true;

                Loadreport();
            }

                //show the medicaid census report
            else if (_reportName == "rptMedicaidCensus")
            {
                RptName = "rptMedicaidCensus";
                Panel3.Visible = false;
                Tblbtn_More.Visible = false;
                tblButtonShow.Visible = false;
                // pnlPatientList.Visible = false;
                pnlDmPatientList.Visible = false;
                dtFromAppt.Enabled = false;
                dtToAppt.Enabled = false;
                SSRSViewer.Visible = true;
                dtpicFrom.Checked = false;
                pnlProvider.Visible = false;
                pnlWarning.Visible = false;
                pnlMessage.Visible = false;
                pnlWarning.Visible = false;

                tblbtnGenReport.Visible = false;
                tblbtn_Print_32.Visible = false;
                tblbtn_Export.Visible = false;
                Tblbtn_More.Visible = false;

                pnlToolStrip.Visible = true;

                Loadreport();
            }

                       //show the Outstanding order report
            else if (_reportName == "rpt_OutstandingOrder")
            {
                RptName = "rpt_OutstandingOrder";
                Panel3.Visible = false;
                Tblbtn_More.Visible = false;
                tblButtonShow.Visible = false;
                // pnlPatientList.Visible = false;
                pnlDmPatientList.Visible = false;
                dtFromAppt.Enabled = false;
                dtToAppt.Enabled = false;
                SSRSViewer.Visible = true;
                dtpicFrom.Checked = false;
                pnlProvider.Visible = false;
                pnlWarning.Visible = false;
                pnlMessage.Visible = false;
                pnlWarning.Visible = false;

                tblbtnGenReport.Visible = false;
                tblbtn_Print_32.Visible = false;
                tblbtn_Export.Visible = false;
                Tblbtn_More.Visible = false;

                pnlToolStrip.Visible = true;
                this.MinimizeBox = false;
                Loadreport();
            }

             //show the medicaid census report
            else if (_reportName == "rptAppCensusReport")
            {
                RptName = "rptAppCensusReport";
                Panel3.Visible = false;
                Tblbtn_More.Visible = false;
                tblButtonShow.Visible = false;
                // pnlPatientList.Visible = false;
                pnlDmPatientList.Visible = false;
                dtFromAppt.Enabled = false;
                dtToAppt.Enabled = false;
                SSRSViewer.Visible = true;
                dtpicFrom.Checked = false;
                pnlProvider.Visible = false;
                pnlWarning.Visible = false;
                pnlMessage.Visible = false;
                pnlWarning.Visible = false;

                tblbtnGenReport.Visible = false;
               // tblbtn_Print_32.Visible = false;
                tblbtn_Export.Visible = false;
                Tblbtn_More.Visible = false;

                pnlToolStrip.Visible = true;

                Loadreport();
            }
            else if (_reportName == "rptCPTActiveDates")
            {
                RptName = "rptCPTActiveDates";
                Panel3.Visible = false;
                Tblbtn_More.Visible = false;
                tblButtonShow.Visible = false;                
                pnlDmPatientList.Visible = false;
                dtFromAppt.Enabled = false;
                dtToAppt.Enabled = false;
                SSRSViewer.Visible = true;
                dtpicFrom.Checked = false;
                pnlProvider.Visible = false;
                pnlWarning.Visible = false;
                pnlMessage.Visible = false;
                pnlWarning.Visible = false;

                tblbtnGenReport.Visible = false;
                tblbtn_Print_32.Visible = true;
                tblbtn_Export.Visible = false;
                Tblbtn_More.Visible = false;

                pnlToolStrip.Visible = true;

                Loadreport();
            }

             //show the Exam Finish Rate Report
            else if (_reportName == "rptExamFinishRateReport")
            {
                RptName = "rptExamFinishRateReport";
                Panel3.Visible = false;
                Tblbtn_More.Visible = false;
                tblButtonShow.Visible = false;
                // pnlPatientList.Visible = false;
                pnlDmPatientList.Visible = false;
                dtFromAppt.Enabled = false;
                dtToAppt.Enabled = false;
                SSRSViewer.Visible = true;
                dtpicFrom.Checked = false;
                pnlProvider.Visible = false;
                pnlWarning.Visible = false;
                pnlMessage.Visible = false;
                pnlWarning.Visible = false;

                tblbtnGenReport.Visible = false;
                tblbtn_Print_32.Visible = false;
                tblbtn_Export.Visible = false;
                Tblbtn_More.Visible = false;

                pnlToolStrip.Visible = true;

                Loadreport();
            }

            else if (_reportName == "rpt_gsdd_migration")
            {
                RptName = "rpt_gsdd_migration";
                Panel3.Visible = false;
                Tblbtn_More.Visible = false;
                tblButtonShow.Visible = false;           
                pnlDmPatientList.Visible = false;
                dtFromAppt.Enabled = false;
                dtToAppt.Enabled = false;
                SSRSViewer.Visible = true;
                dtpicFrom.Checked = false;
                pnlProvider.Visible = false;
                pnlWarning.Visible = false;
                pnlMessage.Visible = false;
                pnlWarning.Visible = false;

                tblbtnGenReport.Visible = false;
                tblbtn_Print_32.Visible = false;
                tblbtn_Export.Visible = false;
                Tblbtn_More.Visible = false;

                pnlToolStrip.Visible = true;

                Loadreport();
            }
            else if (_reportName == "rptBDOAudit")
            {
                RptName = "rptBDOAudit";
                chkOnlyDrug.Visible = false;
                Panel3.Visible = false;
                pnlDemo.Visible = false;
                pnlDiag.Visible = false;
                chkShowUsageDeatal.Checked = false;
                Tblbtn_More.Visible = false;
                cmbAge.Visible = false;
                cmbProvider.Visible = false;
                Label5.Visible = false;
                Lblage.Visible = false;
               
            }
            else if (_reportName == "rptERXControlledSubstance")
            {
                RptName = "rptERXControlledSubstance";
                chkOnlyDrug.Visible = false;
                Panel3.Visible = false;
                pnlDemo.Visible = false;
                pnlDiag.Visible = false;
                chkShowUsageDeatal.Checked = false;
                Tblbtn_More.Visible = false;              

            }
            else if (_reportName == "rptPDRPrograms")
            {                
                RptName = "rptPDRPrograms";
                pnlMedicationDate.Visible = true;
                pnlDate.Visible = false;
                chkOnlyDrug.Visible = false;
                Panel3.Visible = false;
                pnlDemo.Visible = false;
                pnlDiag.Visible = false;
                chkShowUsageDeatal.Checked = false;
                Tblbtn_More.Visible = false;
                cmbAge.Visible = false;
                cmbProvider.Visible = false;
                Label5.Visible = false;
                Lblage.Visible = false;

            }

            SetAgeCr();
            Fill_Provider();

            //if (_reportName == "ProblemUsageReport" || _reportName == "DemographicUsageReport" || _reportName == "rptMedicationMU" || _reportName == "rptPatientHistoryUsage" )
            //{
            //    Loadreport();
            //}
            //commented by shubhangi 20110421
            //if (_reportName == "rptPatientListDM")
            //{
            //SSRSViewer.Visible = false;
            //}
            //else
            //{
            //    Loadreport();
            //}
        }

        private void FillApptStatus()
        {
//            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);

            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Eno");
                dt.Columns.Add("AppName");
                dt.Rows.Add("-1", "All");
                dt.Rows.Add("1", "Follow-up");
                dt.Rows.Add("3", "New Patient");
                cmbapptst.DataSource = dt;
                cmbapptst.DisplayMember = "AppName";
                cmbapptst.ValueMember = "Eno";
            }
            catch
            {
            }
        }

        private void Loadreport()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                gloSettings.GeneralSettings oSetting = new gloSettings.GeneralSettings(_databaseconnectionstring);
                object oValue = new object();


                oSetting.GetSetting("ReportProtocol", out oValue);
                if (oValue != null)
                {
                    strReportProtocol = oValue.ToString();
                    oValue = null;
                }

                oSetting.GetSetting("ReportServer", out oValue);
                if (oValue != null)
                {
                    strReportServer = oValue.ToString();
                    oValue = null;
                }

                oSetting.GetSetting("ReportFolder", out oValue);
                if (oValue != null)
                {
                    strReportFolder = oValue.ToString();
                    oValue = null;
                }

                oSetting.GetSetting("ReportVirtualDirectory", out oValue);
                if (oValue != null)
                {
                    strVirtualDir = oValue.ToString();
                    oValue = null;
                }
                oSetting.Dispose();
                oSetting = null;
                if (strReportProtocol == "" || strReportServer == "" || strReportFolder == "" || strVirtualDir == "")
                {
                    MessageBox.Show("SSRS Settings not set. Set the Report Server settings and then deploy the reports.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }



                if (_IsgloStreamReport == true)
                {
                    DateTime _dtFrom;
                    DateTime _dtTo;

                    if (dtpicFrom.Checked == true)
                    {
                        _dtFrom = dtpicFrom.Value;
                        _dtTo = dtpicTo.Value;
                        DateSelect = "True";
                        _dtFrom = Convert.ToDateTime(dtpicFrom.Value.Date.ToShortDateString());
                        _dtTo = Convert.ToDateTime(dtpicTo.Value.Date.ToShortDateString());
                    }
                    else
                    {
                        // IF THE DATE CRITERIA IS REMOVED THEN WE NEED TO SHOW mEDICATIONS SHOWN FOR ALL THE PATIENTS, THEREFORE THE FROMDATE WILL BE "01/01/1900"
                        // sanjog-20100731 FOR DISPLAY DATE, SINCE DATE CRITERIA IS ABSENT

                        _dtFrom = Convert.ToDateTime("01/01/1900");
                        _dtTo = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
                        dtpicFrom.Checked = false;
                        dtpicTo.Enabled = false;
                        DateSelect = "False";
                    }

                    if (chkShowUsageDeatal.Checked)
                    {
                        includedetails = "Yes";
                    }
                    else
                    {
                        includedetails = "No";
                    }

                    if (Convert.ToInt64(cmbProvider.SelectedValue) == 0)
                    {
                        ProviderID = 0;
                    }
                    else
                    {
                        ProviderID = Convert.ToInt64(cmbProvider.SelectedValue);

                    }

                    if (dtpicFrom.Checked == true)
                    {
                        if (dtpicFrom.Value.Date > dtpicTo.Value.Date)
                        {
                            MessageBox.Show("Invalid Date Criteria, 'From date' should be less than or equal to 'To date'", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }

                    if (cmbAge.Text.Trim() == "" || cmbAge.Text == "For All")
                    {
                    }
                    // FOR PARTICULAR AGE
                    else if (cmbAge.Text == "For Age")
                    {
                        if (cmbAgeFrom.Text.Trim() == "")
                        {
                            MessageBox.Show("Please select the Age ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cmbAgeFrom.Focus();
                            return;
                        }
                    }

                    // FOR LESS THAN GIVEN AGE
                    else if (cmbAge.Text == "Less Than")
                    {
                        if (cmbAgeFrom.Text.Trim() == "")
                        {
                            MessageBox.Show("Please select the Age ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cmbAgeFrom.Focus();
                            return;
                        }
                    }

                    // FOR GREATER THAN GIVEN AGE
                    else if (cmbAge.Text == "Greater Than")
                    {
                        if (cmbAgeFrom.Text.Trim() == "")
                        {
                            MessageBox.Show("Please select the Age ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cmbAgeFrom.Focus();
                            return;
                        }
                    }
                    else if (cmbAge.Text == "Between")
                    {
                        if (cmbAgeFrom.Text.Trim() == "")
                        {
                            MessageBox.Show("Please select From Age ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cmbAgeFrom.Focus();
                            return;
                        }
                        if (cmbAgeTo.Text.Trim() == "")
                        {
                            MessageBox.Show("Please select To Age ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cmbAgeTo.Focus();
                            return;
                        }
                        if (Convert.ToInt32(cmbAgeFrom.Text) > Convert.ToInt32(cmbAgeTo.Text))
                        {
                            MessageBox.Show(" From-age should be less than To-Age ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cmbAgeFrom.Focus();
                            return;
                        }
                    }


                    if ((_reportName == "rptPatientList" || _reportName == "rptPatientListDM"))
                    {

                        if (cmbProvider.SelectedIndex == 0)
                        {
                            if (MessageBox.Show("Provider with 'ALL' option may take long time to generate the report. Do you want to continue?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                return;
                            }
                        }

                    }

                    SSRSViewer.Visible = true;

                    try
                    {
                        this.Text = _reportTitle;
                        reportParam = "Conn=" + _conn;
                        SSRSReportURL = new Uri(strReportProtocol + "://" + strReportServer + "/" + strVirtualDir);
                        SSRSViewer.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Remote;
                        SSRSViewer.ServerReport.ReportServerUrl = SSRSReportURL;
                    }
                    catch (Exception ex)
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("SSRS Reporting Service is not available.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                        return;
                    }


                    AgeCri = cmbAge.Text;
                    if (cmbAgeFrom.Text == "")
                    {
                        FromAge = 0;
                    }
                    else
                    {
                        FromAge = Convert.ToInt32(cmbAgeFrom.Text);
                    }

                    if (cmbAgeTo.Text == "")
                    {
                        ToAge = 0;
                    }
                    else
                    {
                        ToAge = Convert.ToInt32(cmbAgeTo.Text);
                    }

                    if (chkOnlyDrug.Checked)
                    {
                        OnlyDrugAllergy = "1";
                    }
                    else
                    {
                        OnlyDrugAllergy = "0";
                    }

                    gloSSRS.Create_Datasource("dsEMR", "gloEMR", _databaseconnectionstring, gstrSQLServerName, gstrDatabaseName, gblnSQLAuthentication, gstrSQLUser, gstrSQLPassword, true);
                    SSRSViewer.ServerReport.ReportPath = "/" + strReportFolder + "/" + _reportName;// +reportParam;
                    _ParaName = string.Empty;
                    _ParaValue = string.Empty;

                    switch (_reportName)
                    {

                        case "rpt_OutstandingOrder":
                           
                            //_ParaName = "user,Flag,ProviderID,DateCategory,Fromdate,Todate,OrderStatusNumber,IsResultedOrderDate";
                            //_ParaValue = _UserName + "," + sOOTabName + "," + nOOProviderId + "," + sOODateCategory + "," + dtOOFromdate.ToShortDateString() + "," + dtOOTodate.ToShortDateString() + "," + nOOOrderStatusNumber + "," + IsOOResultedOrderDate;
                            SetParameter("user,Flag,ProviderID,DateCategory,Fromdate,Todate,OrderStatusNumber,IsResultedOrderDate", "" + _UserName + "," + sOOTabName + "," + nOOProviderId + "," + sOODateCategory + "," + dtOOFromdate.ToShortDateString() + "," + dtOOTodate.ToShortDateString() + "," + nOOOrderStatusNumber + "," + IsOOResultedOrderDate);

                            break;
                        case "rptPatientVitalUsage":
                            SetParameter("user,AgeCri,AgeFrom,AgeTo,providerid,dtVitalDateFrom,dtVitalDateTo,blnDateselected,ShowSummary,GrowthChartOnly", "" + _UserName + "," + AgeCri + "," + FromAge.ToString() + "," + ToAge.ToString() + "," + ProviderID.ToString() + "," + _dtFrom.ToString() + "," + _dtTo.ToString() + "," + DateSelect + "," + Convert.ToString(!(chkShowUsageDeatal.Checked)) + "," + Convert.ToString(false) + "");
                 
                            _ParaName = "user,AgeCri,AgeFrom,AgeTo,providerid,dtVitalDateFrom,dtVitalDateTo,blnDateselected,ShowSummary,GrowthChartOnly";
                            _ParaValue = _UserName + "," + AgeCri + "," + FromAge.ToString() + "," + ToAge.ToString() + "," + ProviderID.ToString() + "," + _dtFrom.ToString() + "," + _dtTo.ToString() + "," + DateSelect + "," + Convert.ToString(!(chkShowUsageDeatal.Checked)) + "," + Convert.ToString(false) + "";
                          
                            break;

                        case "rptReconciliationFile":
                            SetParameter("suser", "" + _UserName);

                             _ParaName = "suser";
                             _ParaValue = _UserName;

                            break;
                        case "rptReconciliationList":
                            SetParameter("suser", "" + _UserName);

                            _ParaName = "suser";
                            _ParaValue = _UserName;

                            break;

                        case "rptMedicaidCensus": // Resolved Bug #75835 in 8030 It-3
                            SetParameter("suser", "" + _UserName);

                            _ParaName = "suser";
                            _ParaValue = _UserName;

                            break;
                        case "rptAppCensusReport":
                            //SetParameter("suser", "" + _UserName);

                            //_ParaName = "suser";
                            //_ParaValue = _UserName;

                            break;
                        case "rptExamFinishRateReport":
                            SetParameter("suser", "" + _UserName);

                            _ParaName = "suser";
                            _ParaValue = _UserName;

                            break;

                        case "rpt_gsdd_migration":
                            SetParameter("suser", "" + _UserName);

                            _ParaName = "suser";
                            _ParaValue = _UserName;

                            break;

                        case "AllergyUsageReport":
                            SetParameter("user,AgeCri,AgeFrom,AgeTo,Provider,FromDate,ToDate,IncludeUsageDetails,OnlyDrugAllergy,IsDateSelected", "" + _UserName + "," + AgeCri + "," + FromAge.ToString() + "," + ToAge.ToString() + "," + ProviderID.ToString() + "," + _dtFrom.ToString() + "," + _dtTo.ToString() + "," + includedetails + "," + OnlyDrugAllergy + "," + DateSelect + "");

                            _ParaName = "user,AgeCri,AgeFrom,AgeTo,Provider,FromDate,ToDate,IncludeUsageDetails,OnlyDrugAllergy,IsDateSelected";
                            _ParaValue = "" + _UserName + "," + AgeCri + "," + FromAge.ToString() + "," + ToAge.ToString() + "," + ProviderID.ToString() + "," + _dtFrom.ToString() + "," + _dtTo.ToString() + "," + includedetails + "," + OnlyDrugAllergy + "," + DateSelect + "";

                            break;


                        case "ProblemUsageReport":
                            SetParameter("user,AgeCri,AgeFrom,AgeTo,Provider,FromDate,ToDate,IncludeUsageDetails,IsDateSelected", "" + _UserName + "," + AgeCri + "," + FromAge.ToString() + "," + ToAge.ToString() + "," + ProviderID.ToString() + "," + _dtFrom.ToString() + "," + _dtTo.ToString() + "," + includedetails + "," + DateSelect + "");

                            _ParaName = "user,AgeCri,AgeFrom,AgeTo,Provider,FromDate,ToDate,IncludeUsageDetails,IsDateSelected";
                            _ParaValue = "" + _UserName + "," + AgeCri + "," + FromAge.ToString() + "," + ToAge.ToString() + "," + ProviderID.ToString() + "," + _dtFrom.ToString() + "," + _dtTo.ToString() + "," + includedetails + "," + DateSelect + "";

                            break;


                        case "DemographicUsageReport":
                            SetParameter("user,AgeCri,AgeFrom,AgeTo,Provider,FromDate,ToDate,IncludeUsageDetails,Language,Insurance,Gender,Race,Ethnicity,DOB,IsDateSelected", "" + _UserName + "," + AgeCri + "," + FromAge.ToString() + "," + ToAge.ToString() + "," + ProviderID.ToString() + "," + _dtFrom.ToString() + "," + _dtTo.ToString() + "," + includedetails + "," + Convert.ToString(chkLanguage.Checked) + "," + Convert.ToString(chkInsurance.Checked) + "," + Convert.ToString(chkGender.Checked) + "," + Convert.ToString(chkRace.Checked) + "," + Convert.ToString(chkEthnicity.Checked) + "," + Convert.ToString(chkDOB.Checked) + "," + DateSelect + "");

                            _ParaName= "user,AgeCri,AgeFrom,AgeTo,Provider,FromDate,ToDate,IncludeUsageDetails,Language,Insurance,Gender,Race,Ethnicity,DOB,IsDateSelected";
                            _ParaValue = "" + _UserName + "," + AgeCri + "," + FromAge.ToString() + "," + ToAge.ToString() + "," + ProviderID.ToString() + "," + _dtFrom.ToString() + "," + _dtTo.ToString() + "," + includedetails + "," + Convert.ToString(chkLanguage.Checked) + "," + Convert.ToString(chkInsurance.Checked) + "," + Convert.ToString(chkGender.Checked) + "," + Convert.ToString(chkRace.Checked) + "," + Convert.ToString(chkEthnicity.Checked) + "," + Convert.ToString(chkDOB.Checked) + "," + DateSelect + "";
                           
                            break;


                        case "rptMedicationMU":
                            string blnDateselected = "true";
                            if (_dtFrom.ToString() == "1/1/1900 12:00:00 AM")
                            {
                                blnDateselected = "false";
                            }
                            SetParameter("user,AgeCri,AgeFrom,AgeTo,Provider,FromDate,ToDate,IncludeUsageDetails,blnDateselected", "" + _UserName + "," + AgeCri + "," + FromAge.ToString() + "," + ToAge.ToString() + "," + ProviderID.ToString() + "," + _dtFrom.ToString() + "," + _dtTo.ToString() + "," + includedetails + "," + blnDateselected + "");

                            _ParaName = "user,AgeCri,AgeFrom,AgeTo,Provider,FromDate,ToDate,IncludeUsageDetails,blnDateselected";
                            _ParaValue = "" + _UserName + "," + AgeCri + "," + FromAge.ToString() + "," + ToAge.ToString() + "," + ProviderID.ToString() + "," + _dtFrom.ToString() + "," + _dtTo.ToString() + "," + includedetails + "," + blnDateselected + "";
                            
                            break;

                        // chetan added for Reminder Report

                        case "rptPatientListDM":

                            tblbtn_Print_32.Visible = true;
                            tblbtn_Export.Visible = true;

                            _ProblemLists = "";
                            _Medications = "";
                            _MedicationsDisplay = "";

                            for (int lenprb = 0; lenprb < lstDMProblemList.Items.Count; lenprb++)
                                _ProblemLists += lstDMProblemList.Items[lenprb].ToString().Trim() .Replace("'", "''") + "|";
                            if (_ProblemLists.Length > 1)
                                _ProblemLists = _ProblemLists.Substring(0, _ProblemLists.Length - 1).Replace(",", "$");

                            for (int lenmed = 0; lenmed < lstmeddmsetup.Items.Count; lenmed++)
                            {
                                if (lstmeddmsetup.Items[lenmed].ToString().IndexOf(":") > -1)
                                {
                                    _Medications += lstmeddmsetup.Items[lenmed].ToString().Substring(0, lstmeddmsetup.Items[lenmed].ToString().IndexOf(":") - 1).Replace("'", "''") + "|";
                                }
                                else
                                {
                                    _Medications += lstmeddmsetup.Items[lenmed].ToString().Replace("'", "''") + "|";
                                }

                                if (_MedicationsDisplay == "")
                                {
                                    _MedicationsDisplay = lstmeddmsetup.Items[lenmed].ToString();
                                }
                                else
                                {
                                    _MedicationsDisplay = _MedicationsDisplay + "| " + lstmeddmsetup.Items[lenmed].ToString();
                                }
                            }

                            if (_Medications.Length > 2)
                                _Medications = _Medications.Substring(0, _Medications.Length - 1);


                            Int32 FromAgeDM = 1;
                            Int32 ToAgeDM = 1;

                            if (cmbAgeFrom.Text != "")
                                FromAgeDM = Convert.ToInt32(cmbAgeFrom.Text);
                            if (cmbAgeTo.Text != "")
                                ToAgeDM = Convert.ToInt32(cmbAgeTo.Text);

                            C1Patients.Visible = false;

                            _DMCriteriaIDs = "";

                            for (int len = 0; len < lstdmsetup.Items.Count; len++)
                            {
                                if (lstdmsetup.Items[len].ToString().Trim() != "")
                                {
                                    if (chkDueDate.Checked == true)
                                    {
                                        sPatientIDs = GetDMCriteriaPatientID(lstdmsetup.Items[len].ToString().Trim() ,true, dtFromDueDate.Value , dtToDueDate.Value);
                                    }
                                    else 
                                    {
                                        sPatientIDs = GetDMCriteriaPatientID(lstdmsetup.Items[len].ToString().Trim(), false, dtFromDueDate.Value, dtToDueDate.Value);
                                    }

                                     _DMCriteriaIDs += lstdmsetup.Items[len].ToString() + ",";
                                }
                            }


                            if (_DMCriteriaIDs.Length >= 2)
                            {
                                _DMCriteriaIDs = _DMCriteriaIDs.Substring(0, _DMCriteriaIDs.Length - 1);
                            }


                            //SSRSArgs _e = new SSRSArgs();
                            //_e.sCriteriaID = _DMCriteriaIDs.Replace("|", ",");
                            //GenerateReport_Click(null, null, _e);

                            //if (_DMCriteriaIDs != "")
                            //{
                            //    sPatientIDs = GetDMCriteriaPatientID(_DMCriteriaIDs.Replace("|", ","));
                            //}
                            //else
                            //{
                            //    sPatientIDs = "";
                            //}

                            if (_DMCriteriaIDs=="")
                            {
                                sPatientIDs = "";
                            }


                            if (_Medications == "")
                            {
                                _Medications = " ";
                                _MedicationsDisplay = " ";
                            }
                            if (_ProblemLists == "")
                            {
                                _ProblemLists = " ";
                            }

                            if (sPatientIDs == "")
                            {
                                sPatientIDs = " ";
                            }

                            string strapptfrdate = "";
                            string strappttodate = "";
                            string strapptflg = " ";

                            string strDueFromDate = "";
                            string strDueToDate = "";
                            //string strDueFlag = " ";

                            string strdtpicFrom = "";
                            string strdtpicTo = " ";

                            if (dtpicFrom.Checked)
                            {
                                strdtpicFrom = dtpicFrom.Text;
                                strdtpicTo = dtpicTo.Text;
                            }
                            else
                            {
                                strdtpicFrom = " ";
                                strdtpicTo = " ";
                            }


                            if (ChkAppt.Checked)
                            {
                                strapptfrdate = dtFromAppt.Text;
                                strappttodate = dtToAppt.Text;
                                strapptflg = cmbapptst.SelectedValue.ToString();

                            }
                            else
                            {
                                strapptfrdate = " ";
                                strappttodate = " ";
                            }


                            if (chkDueDate.Checked)
                            {
                                strDueFromDate = dtFromDueDate.Text;
                                strDueToDate = dtToDueDate.Text;
                                //strDueFlag = "True";

                            }
                            else
                            {
                                strDueFromDate = " ";
                                //strDueFlag = "False";
                            }


                            string _DMCriteria = "";

                            if (lstdmsetup.Items.Count > 0)
                            {
                                lstdmsetup.SelectedIndex = 0;
                                for (int p = 0; p <= lstdmsetup.Items.Count - 1; p++)
                                {
                                    if (p == 0)
                                    {

                                        _DMCriteria = lstdmsetup.Items[p].ToString().Replace("'", "''");
                                    }
                                    else
                                    {
                                        _DMCriteria = _DMCriteria + "| " + lstdmsetup.Items[p].ToString().Trim().Replace("'", "''");
                                    }
                                }
                            }
                            else
                            {
                                _DMCriteria = " ";
                            }

                            if (sPatientIDs.ToString().Trim() == "")
                                sPatientIDs = "-1";
                            if (lstdmsetup.Items.Count == 0)
                                sPatientIDs = " ";

                            SetParameter("user,nProviderID,AgeType,nAgeFrom,nAgeTo,sMedication,sProblemList,sPatientId,apptfrdate,appttodate,frdate,todate,apptflg,DMCriteria,sMedicationDisplay,DueFromDate,DueToDate", "" +
                             _UserName + "," + ProviderID + "," + cmbAge.SelectedIndex + "," + FromAgeDM + "," + ToAgeDM + "," + _Medications + "," + _ProblemLists + "," + sPatientIDs + "," + strapptfrdate + "," + strappttodate + "," + strdtpicFrom + "," + strdtpicTo + "," + strapptflg + "," + _DMCriteria.ToString() + "," + _MedicationsDisplay + "," + strDueFromDate + "," + strDueToDate);

                            _ParaName = "user,nProviderID,AgeType,nAgeFrom,nAgeTo,sMedication,sProblemList,sPatientId,apptfrdate,appttodate,frdate,todate,apptflg,DMCriteria,sMedicationDisplay,DueFromDate,DueToDate";
                            _ParaValue = "" + _UserName + "," + ProviderID + "," + cmbAge.SelectedIndex + "," + FromAgeDM + "," + ToAgeDM + "," + _Medications + "," + _ProblemLists + "," + sPatientIDs + "," + strapptfrdate + "," + strappttodate + "," + strdtpicFrom + "," + strdtpicTo + "," + strapptflg + "," + _DMCriteria.ToString() + "," + _MedicationsDisplay + "," + strDueFromDate + "," + strDueToDate;

                            break;

                        case "rptPatientList":
                            //Age                   

                            int nAgeType = 0;
                            int nAgeFrom = 0;
                            int nAgeTo = 0;
                            //'For No Age Mentioned
                            if (cmbAge.Text.Trim() == "" || cmbAge.Text == FOR_ALL)
                            {
                                nAgeType = 0;
                                nAgeFrom = 0;
                                nAgeTo = 0;
                            }
                            //' for particular Age
                            else if (cmbAge.Text == FOR_AGE)
                            {
                                nAgeType = 1;
                                if (cmbAgeFrom.Text.Trim() == "")
                                {
                                    MessageBox.Show("Please select the Age ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    cmbAgeFrom.Focus();
                                    return;
                                }
                                nAgeFrom = Convert.ToInt32(cmbAgeFrom.Text);
                            }
                            // for less than Given Age
                            else if (cmbAge.Text == FOR_LESSTHAN_AGE)
                            {
                                nAgeType = 2;
                                if (cmbAgeFrom.Text.Trim() == "")
                                {
                                    MessageBox.Show("Please select the Age ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    cmbAgeFrom.Focus();
                                    return;
                                }
                                nAgeFrom = Convert.ToInt32(cmbAgeFrom.Text);
                            }
                            // for Greater than Given Age
                            else if (cmbAge.Text == FOR_GREATERTHAN_AGE)
                            {
                                nAgeType = 3;
                                if (cmbAgeFrom.Text.Trim() == "")
                                {
                                    MessageBox.Show("Please select the Age ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    cmbAgeFrom.Focus();
                                    return;
                                }
                                nAgeFrom = Convert.ToInt32(cmbAgeFrom.Text);
                            }
                            // for given age range
                            else if (cmbAge.Text == FROMTO_AGE)
                            {
                                nAgeType = 4;
                                // select the From and To age
                                if (cmbAgeFrom.Text.Trim() == "")
                                {
                                    MessageBox.Show("Please select From Age ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    cmbAgeFrom.Focus();
                                    return;
                                }
                                if (cmbAgeTo.Text.Trim() == "")
                                {
                                    MessageBox.Show("Please select To Age ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    cmbAgeTo.Focus();
                                    return;
                                }
                                nAgeFrom = Convert.ToInt32(cmbAgeFrom.Text);
                                nAgeTo = Convert.ToInt32(cmbAgeTo.Text);
                                if (nAgeFrom > nAgeTo)
                                {
                                    MessageBox.Show(" From-age should be less than To-Age ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    cmbAgeFrom.Focus();
                                    return;
                                }
                            }

                            string strMedication = "";
                            _MedicationsDisplay = "";
                            string[] sDrug = new string[3];
                            char[] sep1 = new char[] { ':' };
                            char[] sep2 = new char[] { '-' };
                            string sval = "";
                            int j = 0;

                            lstPatMedication.Refresh();

                            // collect the selected data of check list
                            for (j = 0; j <= lstPatMedication.Items.Count - 1; j++)
                            {
                                sval = lstPatMedication.Items[j].ToString();
                                sDrug = sval.Split(sep1);
                                //LstMedication.Items[i].ToString().Split(":");
                                if (j == 0)
                                {
                                    strMedication = sDrug[0].ToString().Trim();//+ "'";
                                    _MedicationsDisplay = lstPatMedication.Items[j].ToString();
                                }
                                else
                                {
                                    strMedication = strMedication + "|" + sDrug[0].ToString().Trim();//+ "'";
                                    _MedicationsDisplay = _MedicationsDisplay + "| " + lstPatMedication.Items[j].ToString();
                                }

                            }

                            string strProblemList = "";
                            if (lstProblemList.Items.Count > 0)
                            {
                                lstProblemList.SelectedIndex = 0;
                                if (_DictionaryICD9.Count > 0)
                                {
                                    for (int p = 0; p <= lstProblemList.Items.Count - 1; p++)
                                    {
                                        if (p == 0 || strProblemList == "")
                                        {
                                            if (_DictionaryICD9.ContainsKey(lstProblemList.Items[p].ToString()))
                                            {
                                                strProblemList = _DictionaryICD9[lstProblemList.Items[p].ToString()].Trim();
                                            }
                                        }
                                        else
                                        {
                                            //LstMedication.SelectedIndex = p;
                                            if (_DictionaryICD9.ContainsKey(lstProblemList.Items[p].ToString()))
                                            {
                                                strProblemList = strProblemList + "|" + _DictionaryICD9[lstProblemList.Items[p].ToString()].Trim();
                                            }


                                        }
                                    }
                                }

                            }

                            string strSnomedCTProblemList = "";
                            if (_DictionarySnomedCodeCT.Count > 0)
                            {

                                if (lstProblemList.Items.Count > 0)
                                {
                                    lstProblemList.SelectedIndex = 0;

                                    for (int p = 0; p <= lstProblemList.Items.Count - 1; p++)
                                    {
                                        if (p == 0 || strSnomedCTProblemList == "")
                                        {
                                            if (_DictionarySnomedCodeCT.ContainsKey(lstProblemList.Items[p].ToString()))
                                            {
                                                strSnomedCTProblemList = _DictionarySnomedCodeCT[lstProblemList.Items[p].ToString()].Trim();
                                            }
                                        }
                                        else
                                        {
                                            //LstMedication.SelectedIndex = p;
                                            if (_DictionarySnomedCodeCT.ContainsKey(lstProblemList.Items[p].ToString()))
                                            {
                                                strSnomedCTProblemList = strSnomedCTProblemList + "|" + _DictionarySnomedCodeCT[lstProblemList.Items[p].ToString()].Trim();
                                            }


                                        }
                                    }


                                }
                            }

                            string strPatientListshowingElement = "";

                            if (chkPtFirstName.Checked)
                            {
                                if (strPatientListshowingElement == "")
                                {
                                    strPatientListshowingElement = "PtFirstName";

                                }
                                else
                                {
                                    strPatientListshowingElement = strPatientListshowingElement + "," + "PtFirstName";
                                }
                            }

                            if (chkPtMiddleName.Checked)
                            {
                                if (strPatientListshowingElement == "")
                                {
                                    strPatientListshowingElement = "PtMiddleName";

                                }
                                else
                                {
                                    strPatientListshowingElement = strPatientListshowingElement + "," + "PtMiddleName";
                                }
                            }

                            if (chkPtLastName.Checked)
                            {
                                if (strPatientListshowingElement == "")
                                {
                                    strPatientListshowingElement = "PtLastName";

                                }
                                else
                                {
                                    strPatientListshowingElement = strPatientListshowingElement + "," + "PtLastName";
                                }
                            }

                            if (chkPrblLAge.Checked)
                            {
                                if (strPatientListshowingElement == "")
                                {
                                    strPatientListshowingElement = "Age";

                                }
                                else
                                {
                                    strPatientListshowingElement = strPatientListshowingElement + "," + "Age";
                                }
                            }
                            if (chkPriInsPlan.Checked)
                            {
                                if (strPatientListshowingElement == "")
                                {
                                    strPatientListshowingElement = "PriInsPlan";

                                }
                                else
                                {
                                    strPatientListshowingElement = strPatientListshowingElement + "," + "PriInsPlan";
                                }
                            }
                            if (ChkSecInsPlan.Checked)
                            {
                                if (strPatientListshowingElement == "")
                                {
                                    strPatientListshowingElement = "SecInsPlan";

                                }
                                else
                                {
                                    strPatientListshowingElement = strPatientListshowingElement + "," + "SecInsPlan";
                                }
                            }
                            if (chkPCP.Checked)
                            {
                                if (strPatientListshowingElement == "")
                                {
                                    strPatientListshowingElement = "PCP";

                                }
                                else
                                {
                                    strPatientListshowingElement = strPatientListshowingElement + "," + "PCP";
                                }
                            }
                            if (chkPrblLstGender.Checked)
                            {
                                if (strPatientListshowingElement == "")
                                {
                                    strPatientListshowingElement = "Gender";

                                }
                                else
                                {
                                    strPatientListshowingElement = strPatientListshowingElement + "," + "Gender";
                                }
                            }

                            if (chkPrblLstRace.Checked)
                            {
                                if (strPatientListshowingElement == "")
                                {
                                    strPatientListshowingElement = "Race";

                                }
                                else
                                {
                                    strPatientListshowingElement = strPatientListshowingElement + "," + "Race";
                                }
                            }

                            if (chkPrblLEthnicity.Checked)
                            {
                                if (strPatientListshowingElement == "")
                                {
                                    strPatientListshowingElement = "Ethnicity";

                                }
                                else
                                {
                                    strPatientListshowingElement = strPatientListshowingElement + "," + "Ethnicity";
                                }
                            }

                            if (chkPrblLDOB.Checked)
                            {
                                if (strPatientListshowingElement == "")
                                {
                                    strPatientListshowingElement = "DOB";

                                }
                                else
                                {
                                    strPatientListshowingElement = strPatientListshowingElement + "," + "DOB";
                                }
                            }
                            if (chkPrblLLangauge.Checked)
                            {
                                if (strPatientListshowingElement == "")
                                {
                                    strPatientListshowingElement = "Langauge";

                                }
                                else
                                {
                                    strPatientListshowingElement = strPatientListshowingElement + "," + "Langauge";
                                }
                            }
                            if (chkPatNotes.Checked)
                            {
                                if (strPatientListshowingElement == "")
                                {
                                    strPatientListshowingElement = "PatientNotes";

                                }
                                else
                                {
                                    strPatientListshowingElement = strPatientListshowingElement + "," + "PatientNotes";
                                }
                            }

                            if (chkAddress.Checked)
                            {
                                if (strPatientListshowingElement == "")
                                {
                                    strPatientListshowingElement = "sAddressLine3";

                                }
                                else
                                {
                                    strPatientListshowingElement = strPatientListshowingElement + "," + "sAddressLine3";
                                }
                            }

                            if (chksAddressLine1.Checked)
                            {
                                if (strPatientListshowingElement == "")
                                {
                                    strPatientListshowingElement = "sAddressLine1";

                                }
                                else
                                {
                                    strPatientListshowingElement = strPatientListshowingElement + "," + "sAddressLine1";
                                }
                            }

                            if (chksAddressLine2.Checked)
                            {
                                if (strPatientListshowingElement == "")
                                {
                                    strPatientListshowingElement = "sAddressLine2";

                                }
                                else
                                {
                                    strPatientListshowingElement = strPatientListshowingElement + "," + "sAddressLine2";
                                }
                            }

                            if (chksCity.Checked)
                            {
                                if (strPatientListshowingElement == "")
                                {
                                    strPatientListshowingElement = "sCity";

                                }
                                else
                                {
                                    strPatientListshowingElement = strPatientListshowingElement + "," + "sCity";
                                }
                            }


                            if (chksState.Checked)
                            {
                                if (strPatientListshowingElement == "")
                                {
                                    strPatientListshowingElement = "sState";

                                }
                                else
                                {
                                    strPatientListshowingElement = strPatientListshowingElement + "," + "sState";
                                }
                            }

                            if (chksZip.Checked)
                            {
                                if (strPatientListshowingElement == "")
                                {
                                    strPatientListshowingElement = "sZip";

                                }
                                else
                                {
                                    strPatientListshowingElement = strPatientListshowingElement + "," + "sZip";
                                }
                            }



                            if (chkPhone.Checked)
                            {
                                if (strPatientListshowingElement == "")
                                {
                                    strPatientListshowingElement = "Phone";

                                }
                                else
                                {
                                    strPatientListshowingElement = strPatientListshowingElement + "," + "Phone";
                                }
                            }

                            if (chkMobile.Checked)
                            {
                                if (strPatientListshowingElement == "")
                                {
                                    strPatientListshowingElement = "Mobile";

                                }
                                else
                                {
                                    strPatientListshowingElement = strPatientListshowingElement + "," + "Mobile";
                                }
                            }

                            if (chkEmail.Checked)
                            {
                                if (strPatientListshowingElement == "")
                                {
                                    strPatientListshowingElement = "Email";

                                }
                                else
                                {
                                    strPatientListshowingElement = strPatientListshowingElement + "," + "Email";
                                }
                            }

                            if (chkPrblLMedicalCategory.Checked)
                            {
                                if (strPatientListshowingElement == "")
                                {
                                    strPatientListshowingElement = "MedicalCategory";

                                }
                                else
                                {
                                    strPatientListshowingElement = strPatientListshowingElement + "," + "MedicalCategory";
                                }
                            }
                            if (chkPrblLCommPref.Checked)
                            {
                                if (strPatientListshowingElement == "")
                                {
                                    strPatientListshowingElement = "CommPref";

                                }
                                else
                                {
                                    strPatientListshowingElement = strPatientListshowingElement + "," + "CommPref";
                                }
                            }
                           
                            if (chksortlistwthnpatnt.Checked)
                            {
                                if (strPatientListshowingElement == "")
                                {
                                    strPatientListshowingElement = "Applygroupsort";

                                }
                                else
                                {
                                    strPatientListshowingElement = strPatientListshowingElement + "," + "Applygroupsort";
                                }
                            }
                            
                            if (chkPrblmLstOthrProblemList.Checked)
                            {
                                if (strPatientListshowingElement == "")
                                {
                                    strPatientListshowingElement = "ProblemList";

                                }
                                else
                                {
                                    strPatientListshowingElement = strPatientListshowingElement + "," + "ProblemList";
                                }
                            }
                            if (chkPrblmLstOthrMedication.Checked)
                            {
                                if (strPatientListshowingElement == "")
                                {
                                    strPatientListshowingElement = "Medication";

                                }
                                else
                                {
                                    strPatientListshowingElement = strPatientListshowingElement + "," + "Medication";
                                }
                            }
                            if (chkPrblmLstOthrImmunization.Checked)
                            {
                                if (strPatientListshowingElement == "")
                                {
                                    strPatientListshowingElement = "Immunization";

                                }
                                else
                                {
                                    strPatientListshowingElement = strPatientListshowingElement + "," + "Immunization";
                                }
                            }
                            if (chkPrblmLstOthrLabResult.Checked)
                            {
                                if (strPatientListshowingElement == "")
                                {
                                    strPatientListshowingElement = "LabResult";

                                }
                                else
                                {
                                    strPatientListshowingElement = strPatientListshowingElement + "," + "LabResult";
                                }
                            }
                            if (chkPrblmLstOthrAllergy.Checked)
                            {
                                if (strPatientListshowingElement == "")
                                {
                                    strPatientListshowingElement = "MedAllergy";

                                }
                                else
                                {
                                    strPatientListshowingElement = strPatientListshowingElement + "," + "MedAllergy";
                                }
                            }

                            if (chkClaimCPT.Checked)
                            {
                                if (strPatientListshowingElement == "")
                                {
                                    strPatientListshowingElement = "ClaimCPT";

                                }
                                else
                                {
                                    strPatientListshowingElement = strPatientListshowingElement + "," + "ClaimCPT";
                                }
                            }

                            if (chkClaimDX.Checked)
                            {
                                if (strPatientListshowingElement == "")
                                {
                                    strPatientListshowingElement = "ClaimDX";

                                }
                                else
                                {
                                    strPatientListshowingElement = strPatientListshowingElement + "," + "ClaimDX";
                                }
                            }

                            if (chkFacility.Checked)
                            {
                                if (strPatientListshowingElement == "")
                                {
                                    strPatientListshowingElement = "Facility";

                                }
                                else
                                {
                                    strPatientListshowingElement = strPatientListshowingElement + "," + "Facility";
                                }
                            }

                            string strmedicationallergy = "";

                            if (lstAllergy.Items.Count > 0)
                            {
                                lstAllergy.SelectedIndex = 0;
                                for (int p = 0; p <= lstAllergy.Items.Count - 1; p++)
                                {
                                    if (p == 0)
                                    {

                                        strmedicationallergy = lstAllergy.Items[p].ToString();
                                    }
                                    else
                                    {
                                        //LstMedication.SelectedIndex = p;
                                        strmedicationallergy = strmedicationallergy + "|" + lstAllergy.Items[p].ToString().Trim();
                                    }
                                }

                            }

                            string strgender = "";
                            if (cmbGender.Items.Count > 0)
                            {
                                cmbGender.SelectedIndex = 0;
                                for (int p = 0; p <= cmbGender.Items.Count - 1; p++)
                                {
                                    if (p == 0)
                                    {

                                        strgender = cmbGender.Items[p].ToString();
                                    }
                                    else
                                    {
                                        //LstMedication.SelectedIndex = p;
                                        strgender = strgender + "|" + cmbGender.Items[p].ToString().Trim();
                                    }
                                }

                            }

                            string strRace = "";
                            if (cmbRace.Items.Count > 0)
                            {
                                cmbRace.SelectedIndex = 0;
                                for (int p = 0; p <= cmbRace.Items.Count - 1; p++)
                                {
                                    if (p == 0)
                                    {

                                        strRace = cmbRace.Items[p].ToString();
                                    }
                                    else
                                    {
                                        //LstMedication.SelectedIndex = p;
                                        strRace = strRace + "|" + cmbRace.Items[p].ToString().Trim();
                                    }
                                }

                            }

                            string strEthnicity = "";
                            if (cmbethnicity.Items.Count > 0)
                            {
                                cmbethnicity.SelectedIndex = 0;
                                for (int p = 0; p <= cmbethnicity.Items.Count - 1; p++)
                                {
                                    if (p == 0)
                                    {

                                        strEthnicity = cmbethnicity.Items[p].ToString();
                                    }
                                    else
                                    {
                                        //LstMedication.SelectedIndex = p;
                                        strEthnicity = strEthnicity + "|" + cmbethnicity.Items[p].ToString().Trim();
                                    }
                                }

                            }

                            string strLanguage = "";
                            if (cmblanguage.Items.Count > 0)
                            {
                                cmblanguage.SelectedIndex = 0;
                                for (int p = 0; p <= cmblanguage.Items.Count - 1; p++)
                                {
                                    if (p == 0)
                                    {

                                        strLanguage = cmblanguage.Items[p].ToString();
                                    }
                                    else
                                    {
                                        //LstMedication.SelectedIndex = p;
                                        strLanguage = strLanguage + "|" + cmblanguage.Items[p].ToString().Trim();
                                    }
                                }

                            }

                            string strMedicalCategory = "";
                            if (cmbMedicalCategory.Items.Count > 0)
                            {
                                cmbMedicalCategory.SelectedIndex = 0;
                                for (int p = 0; p <= cmbMedicalCategory.Items.Count - 1; p++)
                                {
                                    if (p == 0)
                                    {

                                        strMedicalCategory = cmbMedicalCategory.Items[p].ToString();
                                    }
                                    else
                                    {
                                        //LstMedication.SelectedIndex = p;
                                        strMedicalCategory = strMedicalCategory + "|" + cmbMedicalCategory.Items[p].ToString().Trim();
                                    }
                                }

                            }


                            string strComPrefrrence = "";
                            if (cmbComPre.Items.Count > 0)
                            {
                                cmbComPre.SelectedIndex = 0;
                                for (int p = 0; p <= cmbComPre.Items.Count - 1; p++)
                                {
                                    if (p == 0)
                                    {

                                        strComPrefrrence = cmbComPre.Items[p].ToString();
                                    }
                                    else
                                    {
                                        //LstMedication.SelectedIndex = p;
                                        strComPrefrrence = strComPrefrrence + "|" + cmbComPre.Items[p].ToString().Trim();
                                    }
                                }

                            }


                            string strClaimCpt = "";

                            if (lstClaimCpt.Items.Count > 0)
                            {
                                lstClaimCpt.SelectedIndex = 0;
                                if (_DictionaryClaimCPT.Count > 0)
                                {
                                    for (int p = 0; p <= lstClaimCpt.Items.Count - 1; p++)
                                    {
                                        if (p == 0 || strClaimCpt == "")
                                        {
                                            if (_DictionaryClaimCPT.ContainsKey(lstClaimCpt.Items[p].ToString()))
                                            {
                                                strClaimCpt = _DictionaryClaimCPT[lstClaimCpt.Items[p].ToString()].Trim();
                                            }
                                        }
                                        else
                                        {
                                           
                                            if (_DictionaryClaimCPT.ContainsKey(lstClaimCpt.Items[p].ToString()))
                                            {
                                                strClaimCpt = strClaimCpt + "|" + _DictionaryClaimCPT[lstClaimCpt.Items[p].ToString()].Trim();
                                            }


                                        }
                                    }
                                }

                            }

                             string strInsPlan = "";

                            if (cmbPatInsPlan.Items.Count > 0)
                            {
                                //cmbPatInsPlan.SelectedIndex = 0;
                                if (_DictionaryPatInsPlan.Count > 0)
                                {
                                    for (int p = 0; p <= cmbPatInsPlan.Items.Count - 1; p++)
                                    {
                                        if (p == 0 || strInsPlan == "")
                                        {
                                            if (_DictionaryPatInsPlan.ContainsKey(((System.Collections.Generic.KeyValuePair<string, string>)(cmbPatInsPlan.Items[p])).Key.ToString()))
                                            {

                                                strInsPlan = Convert.ToString(((System.Collections.Generic.KeyValuePair<string, string>)(cmbPatInsPlan.Items[p])).Key).Trim();
                                            }
                                        }
                                        else
                                        {

                                            if (_DictionaryPatInsPlan.ContainsKey(((System.Collections.Generic.KeyValuePair<string, string>)(cmbPatInsPlan.Items[p])).Key.ToString()))
                                            {
                                                strInsPlan = strInsPlan + "|" + Convert.ToString(((System.Collections.Generic.KeyValuePair<string, string>)(cmbPatInsPlan.Items[p])).Key).Trim();
                                            }
                                        }
                                    }
                                }

                            }

                            String sInsFlag="ALL";

                            if (chkPriInsPlan.Checked && ChkSecInsPlan.Checked)
                            {
                                sInsFlag = "ALL";
                            }
                            else if (chkPriInsPlan.Checked)
                            {
                                sInsFlag = "P";
                            }
                            else if (ChkSecInsPlan.Checked)
                            {
                                sInsFlag = "S";
                            }


                             string strClaimDx = "";
                             if (lstClaimDx.Items.Count > 0)
                             {
                                 lstClaimDx.SelectedIndex = 0;
                                 if (_DictionaryClaimDx.Count > 0)
                                 {
                                     for (int p = 0; p <= lstClaimDx.Items.Count - 1; p++)
                                     {
                                         if (p == 0 || strClaimDx == "")
                                         {
                                             if (_DictionaryClaimDx.ContainsKey(lstClaimDx.Items[p].ToString()))
                                             {
                                                 strClaimDx = _DictionaryClaimDx[lstClaimDx.Items[p].ToString()].Trim();
                                             }
                                         }
                                         else
                                         {

                                             if (_DictionaryClaimDx.ContainsKey(lstClaimDx.Items[p].ToString()))
                                             {
                                                 strClaimDx = strClaimDx + "|" + _DictionaryClaimDx[lstClaimDx.Items[p].ToString()].Trim();
                                             }


                                         }
                                     }
                                 }

                             }

                             string strFacility = "";
                             string strFacilityName = "";
                             if (lstFacility.Items.Count > 0)
                             {
                                 lstFacility.SelectedIndex = 0;
                                 if (_DictionaryFacility.Count > 0)
                                 {
                                     for (int p = 0; p <= lstFacility.Items.Count - 1; p++)
                                     {
                                         if (p == 0 || strFacility == "")
                                         {
                                             if (_DictionaryFacility.ContainsKey(lstFacility.Items[p].ToString()))
                                             {
                                                 strFacility = _DictionaryFacility[lstFacility.Items[p].ToString()].Trim();
                                                 strFacilityName= lstFacility.Items[p].ToString().Trim();
                                             }
                                         }
                                         else
                                         {

                                             if (_DictionaryFacility.ContainsKey(lstFacility.Items[p].ToString()))
                                             {
                                                 strFacility = strFacility + "|" + _DictionaryFacility[lstFacility.Items[p].ToString()].Trim();
                                                 strFacilityName = strFacilityName + "|" + lstFacility.Items[p].ToString().Trim();
                                             }


                                         }
                                     }
                                 }

                             }


                            Int64 nProviderID = RetrieveProviderID(cmbProvider.Text);


                            string ConditionOne = cmbFstPat.SelectedItem.ToString();
                            string ConditionTwo = cmbSndPat.SelectedItem.ToString();
                            string ConditionThree = cmb3rdPat.SelectedItem.ToString();
                            string ConditionFour = cmbMediAll.SelectedItem.ToString();

                            string strImmunizatation = "";

                            if (lstImmunization.Items.Count > 0)
                            {
                                lstImmunization.SelectedIndex = 0;
                                for (int p = 0; p <= lstImmunization.Items.Count - 1; p++)
                                {
                                    if (p == 0)
                                    {

                                        strImmunizatation = lstImmunization.SelectedItem.ToString();
                                    }
                                    else
                                    {
                                        lstImmunization.SelectedIndex = p;
                                        strImmunizatation = strImmunizatation + "|" + lstImmunization.SelectedItem.ToString().Trim();
                                    }
                                }

                            }


                            LabTestResult = string.Empty;
                            string LstValue = string.Empty;
                            string TableColoumn = string.Empty;
                            if (lstLabResult.Items.Count > 0)
                            {
                                lstLabResult.SelectedIndex = 0;
                                for (int p = 0; p <= lstLabResult.Items.Count - 1; p++)
                                {
                                    LstValue = lstLabResult.Items[p].ToString();
                                    string coloum = LstValue.Substring(0, LstValue.Length - 3);
                                    string NextCondition = LstValue.Substring(LstValue.Length - 3, 3);
                                    string[] LabResultsLess = new string[3];
                                    string[] LabResultsGreater = new string[3];
                                    string[] LabResultsBitween = new string[3];
                                    string[] LabResultsequalTo = new string[3];
                                    string[] LabResults = new string[2];
                                    string condition = string.Empty;
                                    char[] sep3 = new char[] { '>' };
                                    char[] sep4 = new char[] { '<' };
                                    char[] sep5 = new char[] { '=' };

                                    LabResultsBitween = coloum.Split(sep5);
                                    LabResultsLess = coloum.Split(sep4);
                                    LabResultsGreater = coloum.Split(sep3);
                                    LabResultsequalTo = coloum.Split(sep5);

                                    if (LabResultsBitween.Length == 3)
                                    {
                                        LabResults = LabResultsBitween;
                                        condition = "<";
                                    }
                                    else if (LabResultsLess.Length == 2)
                                    {
                                        LabResults = LabResultsLess;
                                        condition = "<";
                                    }
                                    else if (LabResultsGreater.Length == 2)
                                    {
                                        LabResults = LabResultsGreater;
                                        condition = ">";
                                    }
                                    else if (LabResultsequalTo.Length == 2)
                                    {
                                        LabResults = LabResultsequalTo;
                                        condition = "=";
                                    }

                                    if (LabResults.Length == 2)
                                    {
                                        LabTestResult = LabTestResult + " " + "(Sum([" + LabResults[0].Trim() + "])" + condition + LabResults[1].Trim() + ") " + NextCondition;
                                        TableColoumn = TableColoumn.Trim() + LabResults[0].Trim() + "|";
                                    }
                                    else
                                    {
                                        LabTestResult = LabTestResult + " " + "(Sum([" + LabResults[0].Substring(0, LabResults[0].Length - 1).Trim() + "]) >= " + LabResults[1].Substring(0, LabResults[1].Length - 1) + " AND " + "Sum([" + LabResults[0].Substring(0, LabResults[0].Length - 1).Trim() + "]) <= " + LabResults[2].Substring(0, LabResults[2].Length - 1).Trim() + ") " + NextCondition;
                                        TableColoumn = TableColoumn.Trim() + LabResults[0].Substring(0, LabResults[0].Length - 1).Trim() + "|";
                                    }
                                }

                            }

                            if (LabTestResult != "")
                            {
                                LabTestResult = LabTestResult.Substring(0, LabTestResult.Length - 3);
                                TableColoumn = TableColoumn.Substring(0, TableColoumn.Length - 1);
                            }

                            if (LabTestResult == "")
                                LabTestResult = " ";
                            if (strMedication == "")
                                strMedication = " ";

                            if (_MedicationsDisplay == "")
                                _MedicationsDisplay = " ";

                            if (strProblemList == "")
                                strProblemList = " ";

                            if (strSnomedCTProblemList == "")
                                strSnomedCTProblemList = " ";
                            if (strmedicationallergy == "")
                                strmedicationallergy = " ";
                            if (strPatientListshowingElement == "")
                                strPatientListshowingElement = " ";
                            if (strgender == "")
                                strgender = " ";

                            if (strRace == "")
                                strRace = " ";

                            if (strEthnicity == "")
                                strEthnicity = " ";

                            if (strLanguage == "")
                                strLanguage = " ";

                            if (strMedicalCategory  == "")
                                strMedicalCategory = " ";


                            if (strComPrefrrence == "")
                                strComPrefrrence = " ";

                            if (TableColoumn == "")
                                TableColoumn = " ";
                            if (strImmunizatation == "")
                                strImmunizatation = " ";

                            bool ImGiven = false;

                            if (rbImGiven.Checked == true)
                                ImGiven = true;
                            else if (rbImNotGiven.Checked == true)
                                ImGiven = false;

                            if (chkDateTime.Checked)
                            {
                                if (strPatientListshowingElement == "")
                                {
                                    strPatientListshowingElement = "ShowDateTime";

                                }
                                else
                                {
                                    strPatientListshowingElement = strPatientListshowingElement + "," + "ShowDateTime";
                                }
                            }
                            

                            string StartDate = string.Empty;
                            string EndDate = string.Empty;

                            if (dtpicFrom.Checked == true)
                            {
                                StartDate = dtpicFrom.Text;
                                EndDate = dtpicTo.Text;
                            }
                            else
                            {
                                StartDate = " ";
                                EndDate = " ";
                            }

                            paramList = new List<Microsoft.Reporting.WinForms.ReportParameter>();
                            paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("nProviderID", nProviderID.ToString(), false));
                            paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("AgeType", nAgeType.ToString(), false));
                            paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("nAgeFrom", nAgeFrom.ToString(), false));
                            paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("nAgeTo", nAgeTo.ToString(), false));
                            paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("sMedication", strMedication.ToString(), false));
                            paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("sProblemList", strProblemList, false));
                            paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("ConditionOne", ConditionOne, false));
                            paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("ConditionTwo", ConditionTwo, false));
                            paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("ConditionThree", ConditionThree, false));
                            paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("sLabTestResult", LabTestResult, false));
                            paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("COLNAMES", TableColoumn, false));
                            paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("sImmunization", strImmunizatation, false));
                            paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("bImGiven", ImGiven.ToString(), false));
                            paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("FromDate", StartDate, false));
                            paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("ToDate", EndDate, false));

                            paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("sMedicalCategory", strMedicalCategory, false));
                            paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("sHistory", strmedicationallergy, false));
                            paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("sLanguage", strLanguage, false));
                            paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("sGender", strgender, false));
                            paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("sRace", strRace, false));
                            paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("sethnicity", strEthnicity, false));
                            paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("sCommPreference", strComPrefrrence, false));
                            paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("ConditionFour", ConditionFour, false));
                            paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("strSnomedCTProblemList", strSnomedCTProblemList, false));
                            paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("strPatientListshowingElement", strPatientListshowingElement, false));
                           

                            paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("user", _UserName, false));
                            paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("sMedicationDisplay", _MedicationsDisplay.ToString(), false));

                            paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("strClaimCpt", strClaimCpt, false));
                            paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("strClaimDx", strClaimDx, false));
                            paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("strFacility", strFacility, false));
                            paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("FacilityNames", strFacilityName, false));
                            paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("InsPlan", strInsPlan, false));
                            paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("InsFlag", sInsFlag, false)); 

                            Int64 _ProviderID = RetrieveProviderID(cmbProvider.Text);
                            SetLastPatientListCreatedSetting(_ProviderID);

                            _ParaName = "nProviderID,AgeType,nAgeFrom,nAgeTo,sMedication,sProblemList,ConditionOne,ConditionTwo,ConditionThree,sLabTestResult,COLNAMES,sImmunization,bImGiven,FromDate,ToDate,sMedicalCategory,sHistory,sLanguage,sGender,sRace,sethnicity,sCommPreference,ConditionFour,strSnomedCTProblemList,strPatientListshowingElement,user,sMedicationDisplay,strClaimCpt,strClaimDx,strFacility,FacilityNames,InsPlan,InsFlag";
                            _ParaValue = nProviderID.ToString() + "," + nAgeType.ToString() + "," + nAgeFrom.ToString() + "," + nAgeTo.ToString() + "," + strMedication.ToString() + "," + strProblemList + "," + ConditionOne + "," + ConditionTwo + "," + ConditionThree + "," + LabTestResult + "," + TableColoumn + "," + strImmunizatation + "," + ImGiven.ToString() + "," + StartDate + "," + EndDate + "," + strMedicalCategory + "," + strmedicationallergy + "," + strLanguage + "," + strgender + "," + strRace + "," + strEthnicity + "," + strComPrefrrence + "," + ConditionFour + "," + strSnomedCTProblemList + "," + strPatientListshowingElement + "," + _UserName + "," + _MedicationsDisplay.ToString() + "," + strClaimCpt.ToString() + "," + strClaimDx.ToString() + "," + strFacility.ToString() + "," + strFacilityName.ToString() + "," + strInsPlan.ToString() + "," + sInsFlag;

                            break;


                        case "rptPatientOB" :

                                 StartDate = string.Empty;
                             EndDate = string.Empty;

                              nProviderID = RetrieveProviderID(cmbProvider.Text);

                         
                            if (dtpicFrom.Checked == true)
                            {
                                StartDate = dtpicFrom.Text;
                                EndDate = dtpicTo.Text;
                            }
                            else
                            {
                                //added for bugid 88765 
                                StartDate = "1/1/1900 12:00:00 AM";
                                EndDate = Convert.ToDateTime(System.DateTime.Now).ToShortDateString();
                            }

                            paramList = new List<Microsoft.Reporting.WinForms.ReportParameter>();
                            paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("frmestmdt", StartDate, false));
                            paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("toestmdt", EndDate, false));

                            paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("nProviderID", nProviderID.ToString(), false));
                            paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("user", _UserName, false));
                            _ParaName = "frmestmdt,toestmdt,nProviderID,user";
                            _ParaValue = StartDate.ToString() + "," + EndDate.ToString() + "," + nProviderID.ToString() + "," + _UserName;

                            break;
                        case "rpteRx":
                            chkExcControlledSubstance.Visible = true; // this check box will be shown only when we want to see the eRx MU report,else while design time it is set to visible false
                            string strNDCCode = "";
                            string sIncControlledSubs = "false";
                            string blnRxDateselected = "true";
                            ////if (LstMedication.Items.Count > 0)
                            ////{
                            ////    LstMedication.SelectedIndex = 0;
                            ////    for (int i = 0; i <= LstMedication.Items.Count - 1; i++)
                            ////    {
                            ////        if (i == 0)
                            ////        {
                            ////            strNDCCode = LstMedication.SelectedValue.ToString();
                            ////        }
                            ////        else
                            ////        {
                            ////            LstMedication.SelectedIndex = i;
                            ////            strNDCCode = strNDCCode + "|" + LstMedication.SelectedValue.ToString().Trim();
                            ////        }
                            ////    }
                            ////}
                            ////
                            //Modified by Mayuri:20120416-To increase performance while binding.
                            if (LstMedication.Items.Count > 0)
                            {

                                LstMedication.BeginUpdate();
                                LstMedication.SelectedIndex = 0;
                                StringBuilder sb = new StringBuilder();
                                char[] characters = new char[] { ' ', '|' };
                                for (int i = 0; i <= dtTemp.Rows.Count - 1; i++)
                                {
                                    sb.Append(dtTemp.Rows[i][1].ToString() + "|");
                                }

                                strNDCCode = sb.ToString().TrimEnd(characters);
                                ////
                                LstMedication.EndUpdate();
                            }
                            if (strNDCCode == "")
                            {
                                strNDCCode = "0";
                            }
                            else
                            {
                                // DO NOTHING
                            }

                            if (chkExcControlledSubstance.Checked == true)/////the Exclude control substance parameter will be set according to the checked value
                            {
                                sIncControlledSubs = "true";
                            }
                            else
                            {
                                sIncControlledSubs = "false";
                            }

                            if (_dtFrom.ToString() == "1/1/1900 12:00:00 AM")
                            {
                                blnRxDateselected = "false";
                            }
                            SetParameter("user,providerid,dtDateFrom,dtDateTo,blnExcControlledSubs,sDrugLsttoExc,IncludeUsageDetails,blnRxDateselected,AgeCri,AgeFrom,AgeTo", "" + _UserName + "," + ProviderID.ToString() + "," + _dtFrom.ToString() + "," + _dtTo.ToString() + "," + sIncControlledSubs + "," + strNDCCode + "," + includedetails + "," + blnRxDateselected + "," + AgeCri + "," + FromAge.ToString() + "," + ToAge.ToString() + "");

                            _ParaName = "user,providerid,dtDateFrom,dtDateTo,blnExcControlledSubs,sDrugLsttoExc,IncludeUsageDetails,blnRxDateselected,AgeCri,AgeFrom,AgeTo";
                            _ParaValue = "" + _UserName + "," + ProviderID.ToString() + "," + _dtFrom.ToString() + "," + _dtTo.ToString() + "," + sIncControlledSubs + "," + strNDCCode + "," + includedetails + "," + blnRxDateselected + "," + AgeCri + "," + FromAge.ToString() + "," + ToAge.ToString() + "";

                            break;

                        case "rptPatientPrescriptions":
                            int nDrugList;
                            string strDrugs = "";

                            if (LstMedication.Items.Count > 0)
                            {

                                LstMedication.BeginUpdate();
                                LstMedication.SelectedIndex = 0;
                                StringBuilder sb = new StringBuilder();
                                char[] characters = new char[] { ' ', '|' };
                                for (int i = 0; i <= dtTemp.Rows.Count - 1; i++)
                                {
                                    // strDrugs = dtTemp.Rows[i][1].ToString();
                                    //LstMedication.SelectedIndex = i;
                                    sb.Append(dtTemp.Rows[i][1].ToString() + "|");
                                }

                                strDrugs = sb.ToString().TrimEnd(characters);
                                ////
                                LstMedication.EndUpdate();
                            }

                            if (rbtnAllMedications.Checked == true)
                            {
                                nDrugList = 1;
                            }
                            else
                            {
                                nDrugList = 2;
                            }
                            SetParameter("nProviderID,AgeType,nAgeFrom,nAgeTo,sDrugs,StartDt,EndDt,druglist,User", "" + ProviderID.ToString() + "," + AgeCri + "," + FromAge.ToString() + "," + ToAge.ToString() + "," + strDrugs + "," + _dtFrom.ToString() + "," + _dtTo.ToString() + "," + nDrugList + "," + _UserName + "");

                            _ParaName = "nProviderID,AgeType,nAgeFrom,nAgeTo,sDrugs,StartDt,EndDt,druglist,User";
                            _ParaValue = "" + ProviderID.ToString() + "," + AgeCri + "," + FromAge.ToString() + "," + ToAge.ToString() + "," + strDrugs + "," + _dtFrom.ToString() + "," + _dtTo.ToString() + "," + nDrugList + "," + _UserName + "";

                            break;
                        case "rptPatientHistoryUsage":
                            if (LstMedication.Items.Count <= 0 && LstDiagnosis.Items.Count > 0)
                            {
                                MessageBox.Show("Please select Category first", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LstMedication.Focus();
                                return;
                            }


                            if (LstDiagnosis.Items.Count <= 0 && LstMedication.Items.Count > 0)
                            {
                                MessageBox.Show("Please select history item", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LstDiagnosis.Focus();
                                return;
                            }

                            string strHisCateg = "";
                            int k = 0;
                            LstMedication.Refresh();

                            for (k = 0; k <= LstMedication.Items.Count - 1; k++)
                            {
                                if (k == 0)
                                {
                                    strHisCateg = LstMedication.Items[k].ToString().Trim();
                                    if (LstMedication.Items.Count != 1)
                                    {
                                        strHisCateg += ",";
                                    }
                                }
                                else
                                {
                                    strHisCateg = strHisCateg + LstMedication.Items[k].ToString().Trim();
                                    if (k != LstMedication.Items.Count - 1)
                                    {
                                        strHisCateg += ",";
                                    }
                                }
                            }

                            string strHistDet = "";
                            LstDiagnosis.Refresh();

                            for (k = 0; k <= LstDiagnosis.Items.Count - 1; k++)
                            {
                                if (k == 0)
                                {
                                    strHistDet = LstDiagnosis.Items[k].ToString().Trim();
                                    if (LstDiagnosis.Items.Count != 1)
                                    {
                                        strHistDet += ",";
                                    }
                                }
                                else
                                {
                                    strHistDet = strHistDet + LstDiagnosis.Items[k].ToString().Trim();
                                    if (k != LstDiagnosis.Items.Count - 1)
                                    {
                                        strHistDet += ",";
                                    }
                                }
                            }

                            SetParametersForHx("user,AgeCri,AgeFrom,AgeTo,providerid,dtDateFrom,dtDateTo,blnDateselected,IncludeUsageDetails,History,HistoryCat",
                                "" + _UserName + "~" + AgeCri + "~" + FromAge.ToString() + "~" + ToAge.ToString() + "~" + ProviderID.ToString() + "~" + _dtFrom.ToString() + "~"
                                + _dtTo.ToString() + "~" + Convert.ToString(dtpicFrom.Checked) + "~" + Convert.ToString((chkShowUsageDeatal.Checked)) + "~" + strHistDet + "~" + strHisCateg + "");

                            _ParaName = "user,AgeCri,AgeFrom,AgeTo,providerid,dtDateFrom,dtDateTo,blnDateselected,IncludeUsageDetails,History,HistoryCat";
                            _ParaValue = "" + _UserName + "," + AgeCri + "," + FromAge.ToString() + "," + ToAge.ToString() + "," + ProviderID.ToString() + "," + _dtFrom.ToString() + ","
                                + _dtTo.ToString() + "," + Convert.ToString(dtpicFrom.Checked) + "," + Convert.ToString((chkShowUsageDeatal.Checked)) + "," + strHistDet + "," + strHisCateg + "";

                            break;
                        case "rptCPTActiveDates":
                             SetParameter("UserName", "" + _UserName);

                             _ParaName = "UserName";
                            _ParaValue = _UserName;

                            break;
                        case "rptBDOAudit":
                             
                            chkExcControlledSubstance.Visible = true; 
                            if (LstMedication.Items.Count > 0)
                            {

                                LstMedication.BeginUpdate();
                                LstMedication.SelectedIndex = 0;
                                StringBuilder sb = new StringBuilder();
                                char[] characters = new char[] { ' ', '|' };
                                for (int i = 0; i <= dtTemp.Rows.Count - 1; i++)
                                {
                                    sb.Append(dtTemp.Rows[i][1].ToString() + "|");
                                }
                                                               
                                LstMedication.EndUpdate();
                            }                          
                           
                            if (_dtFrom.ToString() == "1/1/1900 12:00:00 AM")
                            {
                                blnRxDateselected = "false";
                            }
                            SetParameter("user,FromDate,ToDate", "" + _UserName + "," + _dtFrom.ToString() + "," + _dtTo.ToString());

                            _ParaName = "user,FromDate,ToDate";
                            _ParaValue = "" + _UserName + "," + _dtFrom.ToString() + "," + _dtTo.ToString() + "";

                            break;
                        case "rptERXControlledSubstance":
                                                                                    
                            if (LstMedication.Items.Count > 0)
                            {

                                LstMedication.BeginUpdate();
                                LstMedication.SelectedIndex = 0;
                                StringBuilder sb = new StringBuilder();
                                char[] characters = new char[] { ' ', '|' };
                                for (int i = 0; i <= dtTemp.Rows.Count - 1; i++)
                                {
                                    sb.Append(dtTemp.Rows[i][1].ToString() + "|");
                                }
                                LstMedication.EndUpdate();
                            }                         
                          
                            SetParameter("user,providerid,dtDateFrom,dtDateTo,AgeCri,AgeFrom,AgeTo", "" + _UserName + "," + ProviderID.ToString() + "," + _dtFrom.ToString() + "," + _dtTo.ToString()+ "," + AgeCri + "," + FromAge.ToString() + "," + ToAge.ToString() + "");

                            _ParaName = "user,providerid,dtDateFrom,dtDateTo,AgeCri,AgeFrom,AgeTo";
                            _ParaValue = "" + _UserName + "," + ProviderID.ToString() + "," + _dtFrom.ToString() + "," + _dtTo.ToString()+ "," + AgeCri + "," + FromAge.ToString() + "," + ToAge.ToString() + "";

                            break;
                        case "rptPDRPrograms":


                            DateTime dtStartDate = dtMedicationStartDate.Value;
                            DateTime dtEndDate = dtMedicationEndDate.Value;

                            SetParameter("user,dtStartDate,dtEndDate", "" + _UserName + "," + dtStartDate.ToString() + "," + dtEndDate.ToString());

                            _ParaName = "user,dtStartDate,dtEndDate";
                            _ParaValue = "" + _UserName + "," + dtStartDate.ToString() + "," + dtEndDate.ToString();

                            break;
                    }
                    this.SSRSViewer.ServerReport.SetParameters(paramList);
                }
                else
                {
                    SSRSViewer.ServerReport.ReportPath = "/" + strReportFolder + "/" + _reportName;// +reportParam;
                }
                this.SSRSViewer.RefreshReport();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("rsItemNotFound"))
                {
                    if (_reportTitle.Contains("report") || _reportTitle.Contains("Report"))
                    {
                        MessageBox.Show(_reportTitle + " is not available on the report server " + strReportServer + ".", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(_reportTitle + " Report is not available on the report server " + strReportServer + ".", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else if (ex.Message.Contains("The remote name could not be resolved"))
                {
                    MessageBox.Show("Report server is not available. Please check report server settings.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (ex.Message.Contains("The Report Server Windows service 'ReportServer' is not running"))
                {
                    MessageBox.Show("SQL Server Reporting Service is not installed or Report Server is not running.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else if (ex.Message == "Unable to connect to the remote server" || ex.Message == "The request failed with HTTP status 404: ." || ex.Message == "The underlying connection was closed: An unexpected error occurred on a send.")
                {
                    MessageBox.Show("Unable to connect to the report server. Please check report settings.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

       

        public string GetDMCriteriaPatientID(string DMCriteriaName, bool GetFuturePatients, DateTime dtFromDueDate, DateTime dtToDueDate )
        {
            DataSet dsPatients = null;
            dsPatients = new DataSet();

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBPara = null;
            oDBPara = new gloDatabaseLayer.DBParameters();

            oDBPara.Add("@DMCriteria", DMCriteriaName, ParameterDirection.Input, SqlDbType.NVarChar);
            oDBPara.Add("@GetFutureDuePatients", GetFuturePatients, ParameterDirection.Input, SqlDbType.Bit);
            oDBPara.Add("@FromDueDate", dtFromDueDate, ParameterDirection.Input, SqlDbType.Date);
            oDBPara.Add("@ToDueDate", dtToDueDate, ParameterDirection.Input, SqlDbType.Date);

            oDB.Connect(false);
            oDB.Retrive("GetRecommendationsofPatient", oDBPara, out dsPatients);
            oDB.Disconnect();
            oDB.Dispose();
            oDB = null;
            try
            {
                oDBPara.Dispose();
                oDBPara = null;
            }
            catch
            {
            }
            String myReturnString = "";
            if (dsPatients != null)
            {
              myReturnString =   dsPatients.Tables[0].Rows[0][0].ToString();
              dsPatients.Dispose();
              dsPatients = null;
            }

            return myReturnString;
        }

        //
        #region "Settings"
        private void SetLastPatientListCreatedSetting(Int64 ProviderID)
        {
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();

            try
            {

                oConnection.ConnectionString = _databaseconnectionstring;
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = "gsp_InUpPatientListSettings";
                _sqlcommand.Connection = oConnection;

                //'// ** Add Param
                _sqlcommand.Parameters.Add("@sSettingsName", System.Data.SqlDbType.VarChar);
                _sqlcommand.Parameters["@sSettingsName"].Value = "LastPatientListCreated |" + ProviderID;

                _sqlcommand.Parameters.Add("@sSettingsValue", System.Data.SqlDbType.VarChar);
                _sqlcommand.Parameters["@sSettingsValue"].Value = Convert.ToString(DateTime.Now);

                if (oConnection.State == ConnectionState.Closed)
                {
                    oConnection.Open();
                }


                _sqlcommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Parameters.Clear();
                    _sqlcommand.Dispose();
                    _sqlcommand = null;
                }
                oConnection.Close();
                oConnection.Dispose();
                oConnection = null;
            }

        }
        #endregion
        //
        #region "Get ProviderID"
        public Int64 RetrieveProviderID(string strProviderName)
        {
            if (strProviderName.ToLower().Trim() == "all")
                return 0;
            Int64 nProviderID = 0;
            SqlConnection objCon = new SqlConnection(_databaseconnectionstring);
            SqlCommand objCmd = new SqlCommand();
            if (objCon.State == 0)
                objCon.Open();
            try
            {
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "gsp_RetrieveProviderID";
                objCmd.Connection = objCon;

                SqlParameter objParaProviderName = new SqlParameter();

                objParaProviderName.ParameterName = "@ProviderName";
                objParaProviderName.Value = strProviderName;
                objParaProviderName.Direction = ParameterDirection.Input;
                objParaProviderName.SqlDbType = SqlDbType.VarChar;

                objCmd.Parameters.Add(objParaProviderName);
                nProviderID = Convert.ToInt64(objCmd.ExecuteScalar());
                objParaProviderName = null;
                //if (nProviderID == null)
                //{
                //    nProviderID = 0;
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objCmd != null)
                {   
                    objCmd.Parameters.Clear();
                    objCmd.Dispose();
                    objCmd = null;
                }
                objCon.Close();
                objCon.Dispose();
                objCon = null;
            }
            return nProviderID;
        }
        #endregion

        private void SetParameter(string ParameterName, string ParameterValue)
        {
            paramList.Clear();
            string[] PName = ParameterName.Split(',');
            string[] Pvalue = ParameterValue.Split(',');

            int count = PName.Length;
            for (int i = 0; i <= count - 1; i++)
            {
                if (_reportName != "rptPatientListDM")  // condition added by chetan for reminder report on oct20 2010
                {
                    Pvalue[i] = Pvalue[i].Replace("|", ",");
                }
                paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter(PName[i], Pvalue[i], false));
            }
        }

        private void SetParametersForHx(string ParameterName, string ParameterValue)
        {
            paramList.Clear();
            string[] PName = ParameterName.Split(',');
            string[] Pvalue = ParameterValue.Split('~');

            int count = PName.Length;
            for (int i = 0; i <= count - 1; i++)
            {
                paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter(PName[i], Pvalue[i], false));
            }
        }

        private void Fill_Provider()
        {
            try
            {
                DataTable dt;
                gloAppointmentBook.Books.Resource oProvider = new gloAppointmentBook.Books.Resource(_databaseconnectionstring);
               
                if (_reportName == "rptERXControlledSubstance")          
                 dt = oProvider.GetEPCSProvider();
                else
                    dt = oProvider.GetAllActiveInactiveProviders(chkShowAllProviders.Checked);  
                    
                if (dt != null)
                {
                    DataRow dr = dt.NewRow();
                    dr["nProviderID"] = 0;
                    dr["ProviderName"] = "All";
                    dt.Rows.InsertAt(dr, 0);
                    dt.AcceptChanges();

                    cmbProvider.DataSource = dt.Copy();
                    cmbProvider.ValueMember = dt.Columns["nProviderID"].ColumnName;
                    cmbProvider.DisplayMember = dt.Columns["ProviderName"].ColumnName;
                    cmbProvider.Refresh();

                    if (_reportName == "rptPatientList" || _reportName == "rptPatientListDM")
                    {
                        if (_DefaultProviderID > 0)
                        {
                            cmbProvider.SelectedValue = _DefaultProviderID;
                        }
                        else
                        {
                            if (dt.Rows.Count > 1)
                            {
                                cmbProvider.SelectedIndex = 1;
                            }
                            else
                            {
                                cmbProvider.SelectedIndex = 0;
                            }
                        }
                    }
                    else
                    {
                        cmbProvider.SelectedIndex = 0;
                    }
                    dt.Dispose();
                    dt = null;
                }
                
                oProvider.Dispose();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void SetAgeCr()
        {
            cmbAgeFrom.Text = "";
            cmbAgeTo.Text = "";
            cmbAge.Items.Clear();
            cmbAge.Items.Add("For All");
            cmbAge.Items.Add("For Age");
            cmbAge.Items.Add("Less Than");
            cmbAge.Items.Add("Greater Than");
            cmbAge.Items.Add("Between");
            cmbAge.SelectedIndex = 0;
            int i = 0;

            for (i = 0; i <= 124; i++)
            {
                cmbAgeFrom.Items.Add(i + 1);
            }

            for (i = 0; i <= 124; i++)
            {
                cmbAgeTo.Items.Add(i + 1);
            }
        }

        private void cmbAge_TextChanged(object sender, EventArgs e)
        {
            if (cmbAge.Text == "For Age")
            {
                lblAgeFrom.Visible = true;
                lblAgeFrom.Text = "Select";
                cmbAgeFrom.Visible = true;
                cmbAgeTo.Visible = false;
                lblAgeTo.Visible = false;
            }
            else if (cmbAge.Text == "Less Than")
            {
                lblAgeFrom.Visible = true;
                lblAgeFrom.Text = "Select";
                cmbAgeFrom.Visible = true;
                cmbAgeTo.Visible = false;
                lblAgeTo.Visible = false;
            }
            else if (cmbAge.Text == "Greater Than")
            {
                lblAgeFrom.Visible = true;
                lblAgeFrom.Text = "Select";
                cmbAgeFrom.Visible = true;
                cmbAgeTo.Visible = false;
                lblAgeTo.Visible = false;
            }
            else if (cmbAge.Text == "Between")
            {
                lblAgeFrom.Visible = true;
                lblAgeTo.Visible = true;
                lblAgeFrom.Text = "From";
                lblAgeTo.Text = "To";
                cmbAgeFrom.Visible = true;
                cmbAgeTo.Visible = true;
            }
            else
            {
                cmbAgeFrom.Visible = false;
                cmbAgeTo.Visible = false;
                lblAgeFrom.Visible = false;
                lblAgeTo.Visible = false;
            }
        }

        private void cmbAge_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lblAgeFrom.Location = new System.Drawing.Point(830, 14);
            if (cmbAge.Text == "For Age")
            {
                lblAgeFrom.Visible = true;
                lblAgeFrom.Text = "Select";
                cmbAgeFrom.Visible = true;
                cmbAgeTo.Visible = false;
                lblAgeTo.Visible = false;
            }
            else if (cmbAge.Text == "Less Than")
            {
                lblAgeFrom.Visible = true;
                lblAgeFrom.Text = "Select";
                cmbAgeFrom.Visible = true;
                cmbAgeTo.Visible = false;
                lblAgeTo.Visible = false;
            }
            else if (cmbAge.Text == "Greater Than")
            {
                lblAgeFrom.Visible = true;
                lblAgeFrom.Text = "Select";
                cmbAgeFrom.Visible = true;
                cmbAgeTo.Visible = false;
                lblAgeTo.Visible = false;
            }
            else if (cmbAge.Text == "Between")
            {
                lblAgeFrom.Visible = true;
                lblAgeTo.Visible = true;
                this.lblAgeFrom.Location = new System.Drawing.Point(839, 14);
                lblAgeFrom.Text = "From";
                lblAgeTo.Text = "To";
                cmbAgeFrom.Visible = true;
                cmbAgeTo.Visible = true;
            }
            else
            {
                cmbAgeFrom.Visible = false;
                cmbAgeTo.Visible = false;
                lblAgeFrom.Visible = false;
                lblAgeTo.Visible = false;
            }

        }

        private void dtpicFrom_ValueChanged(object sender, EventArgs e)
        {
            if (dtpicFrom.Checked == true)
            {
                dtpicTo.Enabled = true;
            }
            else
            {
                dtpicTo.Enabled = false;
            }
        }

        private void tblStrip_32_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

            HideAllControls();

            switch (e.ClickedItem.Tag.ToString())
            {

                case "Generate Report":
                    {
                        Loadreport();
                    }
                    break;

                case "Print":
                    //added by shubhangi 20110421 
                  // string _PDFFileName = "";

                    if (SSRSViewer.Visible == false)
                    {
                        MessageBox.Show("Report is not generated. Generate report before print.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        printReport();
                        //try
                        //{
                        //    ReportViewerStatus CurrentStatus = SSRSViewer.CurrentStatus;
                        //    if (CurrentStatus.CanPrint)
                        //    {
                        //        _PDFFileName = ConvertSSRStoPDF();
                        //        Print(_PDFFileName);
                              
                        //    }
                        //}
                        //catch (Exception ex)
                        //{
                        //    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //}
                       
                    }
                    break;

                case "Export":
                    if (SSRSViewer.Visible == false)
                    {
                        MessageBox.Show("Report is not generated. Generate report before Export.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        ExportReport();
                    }
                    break;

                case "More":
                    break;

                case "Hide":
                    break;

                case "ShowPatients":
                    this.Cursor = Cursors.WaitCursor;
                    try
                    {
                        FillPatients();
                        pnlWarning.Visible = true;

                    }
                    catch (Exception ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                    }

                    this.Cursor = Cursors.Default;
                    break;
                case "SendReminders":
                    tsb_ReminderLetters.Visible = true;
                    SendRemiderLetters();
                    break;

                case "Close":
                    this.Close();
                    break;
            }
        }
        private void printReport()
        {
            string sqlServerName = string.Empty;
            string sqlDatabaseName = string.Empty;
            string sqlUser = string.Empty;
            string sqlPwd = string.Empty;
            gloSSRSApplication.clsPrintReport clsPrntRpt = null;
            string PDFFileName ="";
            try
            {
                sqlServerName = Convert.ToString(appSettings["SQLServerName"]);
                sqlDatabaseName = Convert.ToString(appSettings["DataBaseName"]);
                sqlUser = Convert.ToString(appSettings["SQLLoginName"]);
                sqlPwd = Convert.ToString(appSettings["SQLPassword"]);

                bool blSQLAuth = !(Convert.ToBoolean(appSettings["WindowAuthentication"]));
                bool gblnIsDefaultPrinter = !(Convert.ToBoolean(appSettings["DefaultPrinter"]));

                clsPrntRpt = new gloSSRSApplication.clsPrintReport(sqlServerName, sqlDatabaseName, blSQLAuth, sqlUser, sqlPwd);
                if (!(gloGlobal.gloTSPrint.isCopyPrint && gloGlobal.gloTSPrint.UseEMFForSSRS))
                {
                    PDFFileName = ConvertSSRStoPDF();
                }
                clsPrntRpt.PrintReport(_reportName, _ParaName, _ParaValue, gblnIsDefaultPrinter, "", PDFFileName, SSRSViewer.ServerReport);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;

            }
            finally
            {
                //  Cleanup for used variables under this method.
                sqlServerName = string.Empty;
                sqlDatabaseName = string.Empty;
                sqlUser = string.Empty;
                sqlPwd = string.Empty;

                if (clsPrntRpt != null)
                {
                    clsPrntRpt.Dispose();
                    clsPrntRpt = null;
                }
            }
        }
        private string ConvertSSRStoPDF() 
        {
            try
            {
                Warning[] warnings = null;
                string[] streamids = null;
                string mimeType = null;
                string encoding = null;
                string extension = null;
                byte[] bytes = null;
                string Format = null;


                Format = "PDF";
                bytes = this.SSRSViewer.ServerReport.Render(Format, null, out mimeType, out encoding, out extension, out streamids, out warnings);
                gloSettings.DatabaseSetting.DataBaseSetting oSettings = new gloSettings.DatabaseSetting.DataBaseSetting();
                string _FileName = "";
                _FileName = gloSettings.FolderSettings.AppTempFolderPath + Guid.NewGuid().ToString() + ".PDF";

                FileStream fs = new FileStream(_FileName, FileMode.Create);
                fs.Write(bytes, 0, bytes.Length);
                fs.Close();
                fs.Dispose();
                fs = null;
                return _FileName;
            }
            catch (Exception ex)
            {
               
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return "";
            }

        }
        //private void Print(string _PDFFileName)
        //{
           
           

        //    gloPrintDialog.gloPrintProgressController ogloPrintProgressController = null;

        //    try
        //    {
        //        using (gloPrintDialog.gloPrintDialog oDialog = new gloPrintDialog.gloPrintDialog())
        //        {
        //            oDialog.ConnectionString = _conn;
        //            oDialog.TopMost = true;
        //            oDialog.ShowPrinterProfileDialog = true;

        //            oDialog.ModuleName = "SSRSReports";
        //            oDialog.RegistryModuleName = "SSRSReports";
                   
        //            if (oDialog != null)
        //            {

        //                oDialog.PrinterSettings = SSRSViewer.PrinterSettings;

        //                //if (bIsSelectedPages)
        //                //{
        //                //    oDialog.AllowSelection = true;
        //                //    PrintDialog1.AllowSelection = true;
        //                //}
        //                //else
        //                //{
        //                //    oDialog.AllowSomePages = true;

        //                //    oDialog.PrinterSettings.ToPage = maxPage;
        //                //    oDialog.PrinterSettings.FromPage = 1;
        //                //    oDialog.PrinterSettings.MaximumPage = maxPage;
        //                //    oDialog.PrinterSettings.MinimumPage = 1;

        //                //    PrintDialog1.AllowSomePages = true;
        //                //    PrintDialog1.PrinterSettings.ToPage = maxPage;
        //                //    PrintDialog1.PrinterSettings.FromPage = 1;
        //                //    PrintDialog1.PrinterSettings.MaximumPage = maxPage;
        //                //    PrintDialog1.PrinterSettings.MinimumPage = 1;
        //                //}

        //                if (oDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //                {

        //                    SSRSViewer.PrinterSettings = oDialog.PrinterSettings;
        //                    if (RptName == "rptPatientList" || RptName =="PrescriptionUsageReport")
        //                    {
        //                        oDialog.PrinterSettings.DefaultPageSettings.Landscape = true;
        //                    }
        //                    else
        //                    {
        //                        oDialog.PrinterSettings.DefaultPageSettings.Landscape = false;
        //                    }
        //                    if (Convert.ToBoolean(appSettings["DefaultPrinter"]))
        //                    {
        //                        oDialog.CustomPrinterExtendedSettings.IsBackGroundPrint = true;
        //                        oDialog.CustomPrinterExtendedSettings.IsShowProgress = true;
        //                    }
        //                    ogloPrintProgressController = new gloPrintDialog.gloPrintProgressController(_PDFFileName, SSRSViewer.PrinterSettings, oDialog.CustomPrinterExtendedSettings);

        //                    ogloPrintProgressController.ShowProgress(this);



        //                }//if

        //            }
        //            else
        //            {
        //                string _ErrorMessage = "Error in Showing Print Dialog";

        //                if (_ErrorMessage.Trim() != "")
        //                {
        //                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;

        //                    _MessageString = "";
        //                }


        //                MessageBox.Show(_ErrorMessage, gstrMessageBoxCaption , MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            }

        //        }
        //        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Reports, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.Print, RptName + " Usage Report Printed..", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success);

        //    }
        //    catch (Exception ex)
        //    {
        //        #region " Make Log Entry "

        //        string _ErrorMessage = ex.ToString();
        //        //Code added on 7rd October 2008 By - Sagar Ghodke
        //        //Make Log entry in DMSExceptionLog file for any exceptions found
        //        if (_ErrorMessage.Trim() != "")
        //        {
        //            string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
        //            //  gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
        //            _MessageString = "";
        //        }

        //        //End Code add
        //        #endregion " Make Log Entry "

        //        MessageBox.Show(ex.Message, gstrMessageBoxCaption , MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        ex = null;
        //    }
        //    finally
        //    {

        //    }

        //}
        private void HideAllControls()
        {
            if (dgCustomGrid != null)
            {
                dgCustomGrid.Visible = false;
            }
            pnlcustomTask.Visible = false;
            pnlLabTestResult.Visible = false;

            tsb_ReminderLetters.Visible = false;
            pnlMessage.Visible = false;
            pnlWarning.Visible = false;
        }

        private void SendRemiderLetters()
        {
            DataTable dtPatints = new DataTable();
            DataRow oRow;
            dtPatints.Columns.Add("Select");
            dtPatints.Columns.Add("PatientID");
            dtPatints.Columns.Add("Code");
            dtPatints.Columns.Add("Patient Name");
            dtPatints.Columns.Add("Comm. Preference");
            if (gblnPatientPortalEnabled == true && gblnIntuitCommunication == true)
            {
                dtPatints.Columns.Add("RegisteredOnPortal");
            }
            try
            {
                for (int iRow = 1; iRow < C1Patients.Rows.Count; iRow++)
                {
                    if (C1Patients.GetCellCheck(iRow, COL_Select) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                    {
                        oRow = dtPatints.NewRow();
                        oRow["Select"] = 1;
                        oRow["PatientID"] = C1Patients.GetData(iRow, COL_PatientID);
                        oRow["Code"] = C1Patients.GetData(iRow, COL_PatientCode);

                        string sPatFName = C1Patients.GetData(iRow, COL_PatientFName).ToString();
                        string sPatMName = C1Patients.GetData(iRow, COL_PatientMName).ToString();
                        string sPatLName = C1Patients.GetData(iRow, COL_PatientLName).ToString();
                        string sPatientName = "";
                        if (sPatMName != "")
                        {
                            sPatientName = sPatFName + " " + sPatMName + " " + sPatLName;
                        }
                        else
                        {
                            sPatientName = sPatFName + " " + sPatLName;
                        }

                        oRow["Patient Name"] = sPatientName;
                        oRow["Comm. Preference"] = C1Patients.GetData(iRow, COL_sCommunicationPreference);
                        if (gblnPatientPortalEnabled == true && gblnIntuitCommunication == true)
                        {
                            if (C1Patients.Cols.Contains("RegisteredOnPortal"))
                            {
                                oRow["RegisteredOnPortal"] = C1Patients.GetData(iRow, "RegisteredOnPortal");
                            }
                        }
                        dtPatints.Rows.Add(oRow);
                    }
                }


                PatientArgs _ePat = new PatientArgs();
                _ePat.dtPatients = dtPatints;
                SendReminder_Letter(null, null, _ePat);
                if (_ePat.dtPatients != null)
                {
                    _ePat.dtPatients.Dispose();
                    _ePat.dtPatients = null;
                }
                _ePat = null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }


        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked == true)
            {
                chkDOB.Checked = true;
                chkEthnicity.Checked = true;
                chkGender.Checked = true;
                chkInsurance.Checked = true;
                chkLanguage.Checked = true;
                chkRace.Checked = true;

                chkDOB.Enabled = false;
                chkEthnicity.Enabled = false;
                chkGender.Enabled = false;
                chkInsurance.Enabled = false;
                chkLanguage.Enabled = false;
                chkRace.Enabled = false;
            }
            else
            {
                chkDOB.Checked = false;
                chkEthnicity.Checked = false;
                chkGender.Checked = false;
                chkInsurance.Checked = false;
                chkLanguage.Checked = false;
                chkRace.Checked = false;

                chkDOB.Enabled = true;
                chkEthnicity.Enabled = true;
                chkGender.Enabled = true;
                chkInsurance.Enabled = true;
                chkLanguage.Enabled = true;
                chkRace.Enabled = true;

            }
        }

        // SANJOG-20100726 EXPORT FUNCTIONALITY
        private void ExportReport()
        {
            saveFileDialog1.Filter = "XML file with report data (*.xml)|*.xml|CSV (comma delimited) (*.csv)|*.csv|TIFF (*.tif)|*.tif|Acrobat (PDF) file (*.pdf)|*.pdf|Web archive(*.mhtml)|*.mhtml|Excel (*.xls)|*.xls";
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.FileName = RptName;
            saveFileDialog1.ShowDialog(this);
        }

        // SANJOG-20100726 EXPORT FUNCTIONALITY
        private void LoadUserGrid()
        {
            try
            {
                AddControl();
                if ((dgCustomGrid != null))
                {
                    dgCustomGrid.Visible = true;
                    if ((_reportName == "rpteRx") || (_reportName == "rptPatientPrescriptions"))
                    {
                        dgCustomGrid.SetSelectAllVisible = true;
                    }
                    else { dgCustomGrid.SetSelectAllVisible = false; }

                    dgCustomGrid.Width = pnlcustomTask.Width;
                    dgCustomGrid.Height = pnlcustomTask.Height;
                    dgCustomGrid.C1Grid.AllowEditing = false;
                    dgCustomGrid.BringToFront();
                    dgCustomGrid.SetVisible = false;
                    if ((_reportName == "rptPatientList") && (strLst == "patientproblem")) //added for ICd10 implemantation
                    {
                        dgCustomGrid.setPnlICDVisible = true;
                        dgCustomGrid.rbICD10Transition = ICD10Transition;
                    }

                    if ((_reportName == "rptPatientListDM") && (strLst == "PatientDMProblem")) //added for ICd10 implemantation
                    {
                        dgCustomGrid.setPnlICDVisible = true;
                     
                    }


                    BindUserGrid();
                    // string[] strdata;
                    //Added and commnetd by Mayuri:20120416
                    ////string[] strdata = new string[LstMedication.Items.Count];

                    ////if (_reportName == "rpteRx")
                    ////{

                    ////    if (LstMedication != null)
                    ////    {
                    ////        if (LstMedication.Items.Count > 0)
                    ////        {
                    ////            for (int j = 0; j <= LstMedication.Items.Count - 1; j++)
                    ////            {
                    ////                LstMedication.SelectedIndex = j;
                    ////                strdata[j] = LstMedication.SelectedValue.ToString();
                    ////            }
                    ////            for (int i = 0; i < dgCustomGrid.C1Task.Rows.Count - 1; i++)
                    ////            {
                    ////                if (Array.IndexOf(strdata, (dgCustomGrid.C1Task.GetData(i, 2).ToString ().Trim ())) >= 0)
                    ////                {
                    ////                    dgCustomGrid.C1Task.SetCellCheck(i, 0, C1.Win.C1FlexGrid.CheckEnum.Checked);

                    ////                }

                    ////            }
                    ////        }
                    ////    }
                    ////}
                    dgCustomGrid.Visible = true;
                    dgCustomGrid.Selectsearch(CustomTask.enmcontrol.Search);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Patient Messages", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddControl()
        {
            if ((dgCustomGrid != null))
            {
                RemoveControl();
            }

            dgCustomGrid = new CustomTask();
            if (strLst == "gender")
            {
                dgCustomGrid.SetSearchTextWidth = 100;
            }
          
            pnlcustomTask.Controls.Add(dgCustomGrid);
            dgCustomGrid.Dock = DockStyle.Fill;
            dgCustomGrid.SelectAllClick += new CustomTask.SelectAllClickEventHandler(dgCustomGrid_SelectAllClick);
            dgCustomGrid.DeSelectAllClick += new CustomTask.DeSelectAllClickEventHandler(dgCustomGrid_DeSelectAllClick);
            dgCustomGrid.OKClick += new CustomTask.OKClickEventHandler(dgCustomGrid_OKClick);
            dgCustomGrid.CloseClick += new CustomTask.CloseClickEventHandler(dgCustomGrid_CloseClick);
            dgCustomGrid.SearchChanged += new CustomTask.SearchChangedEventHandler(dgCustomGrid_SearchChanged);
            dgCustomGrid.GridDoubleClick += new CustomTask.GridDoubleClickEventHandler(dgCustomGrid_GridDoubleClick);
            dgCustomGrid.rbtnICD9Click+=new gloUserControlLibrary.CustomTask.rbtnICD9ClickEventHandler(dgCustomGrid_rbtnICD9Click);
            //added for ICd10 implemantation
            dgCustomGrid.rbtnICD10Click+=new gloUserControlLibrary.CustomTask.rbtnICD10ClickEventHandler(dgCustomGrid_rbtnICD10Click);  
            pnlcustomTask.BringToFront();

            int y = 0;
            int x = 0;

            if (strLst == "drugs")
            {
                if ((_reportName == "rptPatientHistoryUsage") || (_reportName == "rpteRx") || (_reportName == "rptPatientPrescriptions"))
                {
                    if (_reportName == "rptPatientPrescriptions")
                    {
                        dgCustomGrid.setPnlVisible = true;
                        dgCustomGrid.rbtnBeersListClick += new CustomTask.rbtnBeersListClickEventHandler(dgCustomGrid_rbtnBeersListClick);
                    }
                    else
                    {
                        dgCustomGrid.setPnlVisible = false;
                    }
                    if (_reportName == "rpteRx" || _reportName == "rptPatientPrescriptions")
                    {
                        dgCustomGrid.SetSelectAllVisible = true;
                    }
                }
                else
                {
                    dgCustomGrid.setPnlVisible = true;
                    dgCustomGrid.rbtnBeersListClick += new CustomTask.rbtnBeersListClickEventHandler(dgCustomGrid_rbtnBeersListClick);
                }

                y = 141;
                x = 16;
            }

            else if (strLst == "cpt")
            {
                y = 141;
                x = 844;
            }

            else if (strLst == "PatDMCriteria")
            {
                y = 90;
                x = 10;
            }
            else if (strLst == "PatientMedication")
            {
                y = pnlPatientList.Bottom - 20;
                x = 270;
            }
            else if (strLst == "PatientDMMedication")
            {
                y = 90;
                x = 150;
            }

            else if (strLst == "diag")
            {
                y = 141;
                x = 426;
            }
            else if (strLst == "allergies")
            {
                y = pnlPatientList.Bottom - 20;
                x = 750;
            }
            else if (strLst == "gender")
            {
                y = 133;
                x = 1040;
            }
            else if (strLst == "race")
            {
                y = 133;
                x = 430;
            }
            else if (strLst == "InsPlan")
            {
                y = 133;
                x = 90;
            }
            else if (strLst == "MedicalCategory")
            {
                y = 158;
                x = 787;
            }
                
            else if (strLst == "ethnicity")
            {
                y = 133;
                x = 787;
            }
            else if (strLst == "language")
            {
                y = 158;
                x = 90;
            }
            else if (strLst == "com prefrrence")
            {
                y = 158;
                x = 430;
            }
            else if (strLst == "PatientDMProblem")
            {
                y = 90;
                x = 190;
            }
            else if (strLst == "PatientProblem".ToLower())
            {
                y = pnlPatientList.Bottom - 20;
                x = 10;
            }
            else if (strLst == "PatientImmunization")
            {
                y = pnlPatientList.Bottom - 20;
                x = 520;
            }
            else if (strLst == "PatientLabResults")
            {
                y = pnlPatientList.Bottom-20;
                x = 700;
            }
            else if (strLst == "ClaimCpt")
            {
                y = 390;
                x = 10;
            }

            else if (strLst == "ClaimDx")
            {
                y = 390;
                x = 250;
            }
            else if (strLst == "Facility")
            {
                y = 390;
                x = 500;
            }
            pnlcustomTask.Location = new Point(x, y);
            pnlcustomTask.Visible = true;
            dgCustomGrid.Visible = true;
            pnlcustomTask.BringToFront();
            dgCustomGrid.BringToFront();
        }
        //added for ICd10 implementation
        private void dgCustomGrid_rbtnICD9Click(object sender, EventArgs e)
        {
            FillICD9Type(9);
        }
        //added for ICd10 implementation
        private void dgCustomGrid_rbtnICD10Click(object sender, EventArgs e)
        {
            FillICD9Type(10);
        }
        void dgCustomGrid_GridDoubleClick(object sender, EventArgs e)
        {
            if (strLst.ToLower() == "patientlabresults")
            {
                string str = dgCustomGrid.get_GetItem(dgCustomGrid.GetCurrentrowIndex, 0).ToString();
                cmbLabResult.Text = "";
                cmbLabResult.Text = str;
                dgCustomGrid_CloseClick(sender, e);
            }
        }
       
        private void RemoveControl()
        {
            if ((dgCustomGrid != null))
            {
                if (pnlcustomTask.Controls.Contains(dgCustomGrid))
                {
                    pnlcustomTask.Controls.Remove(dgCustomGrid);
                }
                dgCustomGrid.Visible = false;
                try
                {
                    dgCustomGrid.SelectAllClick -= new CustomTask.SelectAllClickEventHandler(dgCustomGrid_SelectAllClick);
                    dgCustomGrid.DeSelectAllClick -= new CustomTask.DeSelectAllClickEventHandler(dgCustomGrid_DeSelectAllClick);
                    dgCustomGrid.OKClick -= new CustomTask.OKClickEventHandler(dgCustomGrid_OKClick);
                    dgCustomGrid.CloseClick -= new CustomTask.CloseClickEventHandler(dgCustomGrid_CloseClick);
                    dgCustomGrid.SearchChanged -= new CustomTask.SearchChangedEventHandler(dgCustomGrid_SearchChanged);
                    dgCustomGrid.GridDoubleClick -= new CustomTask.GridDoubleClickEventHandler(dgCustomGrid_GridDoubleClick);
                    dgCustomGrid.rbtnICD9Click -= new gloUserControlLibrary.CustomTask.rbtnICD9ClickEventHandler(dgCustomGrid_rbtnICD9Click);
                    //added for ICd10 implemantation
                    dgCustomGrid.rbtnICD10Click -= new gloUserControlLibrary.CustomTask.rbtnICD10ClickEventHandler(dgCustomGrid_rbtnICD10Click);
                    try
                    {
                        dgCustomGrid.rbtnBeersListClick -= new CustomTask.rbtnBeersListClickEventHandler(dgCustomGrid_rbtnBeersListClick);
  
                    }
                    catch
                    {
                    }
                    dgCustomGrid.Dispose();
                    dgCustomGrid = null;
                }
                catch
                {
                }
    
                dgCustomGrid = null;
            }
        }

        private void btnBrowseDiag_Click(object sender, EventArgs e)
        {
            if (bHistory == true)
            {
                if (LstMedication.Items.Count == 0)
                {
                    MessageBox.Show("Please select Category first", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LstMedication.Focus();
                    return;
                }
            }
            strLst = "diag";
            LoadUserGrid();
            pnlcustomTask.BringToFront();
        }

        private void btnClearDiag_Click(object sender, EventArgs e)
        {
            while (LstDiagnosis.SelectedItems.Count > 0)
            {
                LstDiagnosis.Items.Remove(LstDiagnosis.SelectedItems[0]);
            }
        }

        private void btnClearAllDiag_Click(object sender, EventArgs e)
        {
            LstDiagnosis.Items.Clear();
        }

        void dgCustomGrid_SelectAllClick(object sender, EventArgs e)
        {
            if (dgCustomGrid.C1Task.Rows.Count > 1)
            {
                dgCustomGrid.SetDeSelectAllVisible = true;
                dgCustomGrid.SetSelectAllVisible = false;
                for (int i = 1; i < dgCustomGrid.C1Task.Rows.Count; i++)
                {
                    dgCustomGrid.C1Task.SetCellCheck(i, 0, C1.Win.C1FlexGrid.CheckEnum.Checked);
                }
            }
        }

        void dgCustomGrid_DeSelectAllClick(object sender, EventArgs e)
        {
            dgCustomGrid.SetSelectAllVisible = true;
            dgCustomGrid.SetDeSelectAllVisible = false;
            for (int i = 1; i < dgCustomGrid.C1Task.Rows.Count; i++)
            {
                dgCustomGrid.C1Task.SetCellCheck(i, 0, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
            }
        }

        void EnableDisableDueDate(bool DueDateState)
        { 
            if (DueDateState==true)
            {
                if (chkDueDate.Enabled == false)
                {
                    chkDueDate.Enabled = true;
                    //dtFromDueDate.Enabled = true;
                    //dtToDueDate.Enabled = true;
                }
            }
            else if (DueDateState == false)
            {
                if (chkDueDate.Enabled == true)
                {
                    chkDueDate.Enabled = false;
                    dtFromDueDate.Enabled = false;
                    dtToDueDate.Enabled = false;
                    chkDueDate.Checked = false;
                }
            }
        }

        void dgCustomGrid_OKClick(object sender, EventArgs e)
        {
            //////////


            //////
            try
            {
                //if (_reportName == "rpteRx")
                //{
                //    DataRow dra;
                //    DataTable dtTemp = new DataTable();

                //    LstMedication.DataSource = null;
                //    dtTemp.Columns.Add("Drug", typeof(string));
                //    dtTemp.Columns.Add("NDCCode", typeof(string));


                //    if (dgCustomGrid.C1Task.Rows.Count > 1)
                //    {

                //        for (int i = 0; i <= dgCustomGrid.GetTotalRows - 1; i++)
                //        {
                //            if (Convert.ToBoolean(dgCustomGrid.CurrentID.ToString()) == true)
                //            {
                //                if (dgCustomGrid.get_GetIsChecked(i, 0) == true)// .C1Grid.GetCellCheck(i, 0) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                //                {
                //                    if (strLst == "drugs")
                //                    {
                //                        dra = dtTemp.NewRow();
                //                        dra[0] = dgCustomGrid.get_GetItem(i, 1).ToString();
                //                        dra[1] = dgCustomGrid.get_GetItem(i, 2).ToString();
                //                        dtTemp.Rows.Add(dra);
                //                    }
                //                }
                //            }
                //        }
                //    }
                //    LstMedication.DataSource = dtTemp;
                //    LstMedication.DisplayMember = "Drug";
                //    LstMedication.ValueMember = "NDCCode";
                //    pnlcustomTask.Visible = false;
                //}
                //else
                {
                    
                    //DataTable dtTemp = new DataTable();
                    if (LstMedication.Items.Count == 0)
                    {
                        dtTemp = new DataTable();
                        dtTemp.Columns.Add("Drug", typeof(string));
                        dtTemp.Columns.Add("NDCCode", typeof(string));
                    }
                    else
                    {
                        dtTemp = (DataTable)LstMedication.DataSource;
                    }
                    Int32 i = 0;

                    if (dgCustomGrid.C1Task.Rows.Count > 1)
                    {
                        // chetan added for reminder report for on 20 Oct 2010
                        switch (strLst)
                        {
                            case "PatientDMProblem": lstDMProblemList.Items.Clear();
                                IsICD9Checked = dgCustomGrid.IsICD9Checked;  
                                break;
                            case "PatientDMMedication": lstmeddmsetup.Items.Clear();
                                break;
                            case "gender": cmbGender.Items.Clear();
                                break;
                            case "race": cmbRace.Items.Clear();
                                break;
                            case "ethnicity": cmbethnicity.Items.Clear();
                                break;
                            case "language": cmblanguage.Items.Clear();
                                break;
                            case "MedicalCategory": cmbMedicalCategory.Items.Clear();
                                break;
                            case "com prefrrence": cmbComPre.Items.Clear();
                                break;
                            case "PatDMCriteria": lstdmsetup.Items.Clear();
                                arrdm.Clear();
                                break;
                            case "ClaimCpt": lstClaimCpt.Items.Clear();
                                _DictionaryClaimCPT.Clear();
                                break;
                            case "ClaimDx": lstClaimDx.Items.Clear();
                                _DictionaryClaimDx.Clear();
                                break;
                            case "Facility": lstFacility.Items.Clear();
                                _DictionaryFacility.Clear();
                                break;
                            case "InsPlan":
                               
                                cmbPatInsPlan.DataSource = null;
                                cmbPatInsPlan.Items.Clear();
                                _DictionaryPatInsPlan.Clear();
                                break;

                        }


                        if (strLst == "PatDMCriteria")
                        {
                            arrdm.Clear();
                            EnableDisableDueDate(false);
                        }
                        if (strLst != "PatientLabResults")
                        {


                            for (i = 0; i <= dgCustomGrid.GetTotalRows - 1; i++)
                            {


                                if ((strLst == "PatDMCriteria") && (dgCustomGrid.C1Task.GetCellCheck(i, 0) == C1.Win.C1FlexGrid.CheckEnum.Checked))
                                {

                                    lstdmsetup.Items.Add(dgCustomGrid.get_GetItem(i, 1).ToString());
                                    arrdm.Add(dgCustomGrid.get_GetItem(i, 2).ToString());
                                    EnableDisableDueDate(true);
                                }

                                else if ((strLst == "PatientDMMedication") && (dgCustomGrid.C1Task.GetCellCheck(i, 0) == C1.Win.C1FlexGrid.CheckEnum.Checked))
                                {
                                    // string str = dgCustomGrid.get_GetItem(i, 1).ToString();
                                    // if (!lstmeddmsetup.Items.Contains(str))

                                    lstmeddmsetup.Items.Add(dgCustomGrid.get_GetItem(i, 1).ToString());
                                }

                                else if ((strLst == "PatientDMProblem") && (dgCustomGrid.C1Task.GetCellCheck(i, 0) == C1.Win.C1FlexGrid.CheckEnum.Checked))
                                {
                                    // string str = dgCustomGrid.get_GetItem(i, 1).ToString();
                                    // if (!lstDMProblemList.Items.Contains(str))

                                    lstDMProblemList.Items.Add(dgCustomGrid.get_GetItem(i, 1).ToString());
                                }
                              
                                else
                                {
                                    //Commeneted to Fix issue related to adding selected items into listbox after check-uncheck
                                    //if (Convert.ToBoolean(dgCustomGrid.CurrentID.ToString()) == true)
                                    //{
                                    if (dgCustomGrid.get_GetIsChecked(i, 0) == true)
                                    {
                                        if (strLst == "drugs")
                                        {
                                            if ((_reportName == "rptMedicationMU") || (_reportName == "rpteRx") || (_reportName == "rptPatientPrescriptions"))
                                            {
                                                int cnt = 0;
                                                DataRow dra;
                                                dra = dtTemp.NewRow();
                                                dra[0] = dgCustomGrid.get_GetItem(i, 1).ToString();
                                                dra[1] = dgCustomGrid.get_GetItem(i, 2).ToString();
                                                for (int j = 0; j < dtTemp.Rows.Count; j++)
                                                {

                                                    if (dtTemp.Rows[j][1].ToString() == dra[1].ToString())
                                                    {
                                                        cnt++;
                                                    }


                                                }
                                                if (cnt == 0)
                                                {
                                                    dtTemp.Rows.Add(dra);
                                                }
                                            }
                                            else
                                            {
                                                //Sanjog Added on 20101101 for dont allow the repeated item
                                                Int32 j = 0;
                                                bool blnPresent = false;
                                                for (j = 0; j < LstMedication.Items.Count; j++)
                                                {
                                                    blnPresent = false;
                                                    if (LstMedication.Items[j].ToString() == dgCustomGrid.get_GetItem(i, 1).ToString())
                                                    {
                                                        blnPresent = true;
                                                        break;
                                                    }
                                                }
                                                if (blnPresent == false)
                                                    //Sanjog Added on 20101101 for dont allow the repeated item
                                                    LstMedication.Items.Add(dgCustomGrid.get_GetItem(i, 1).ToString());
                                            }
                                        }
                                        else if (strLst == "cpt")
                                        {
                                            //Sanjog Added on 20101101 for dont allow the repeated item
                                            Int32 j = 0;
                                            bool blnPresent = false;
                                            if (LstTreatment == null)
                                            {
                                                LstTreatment = new ListBox();
                                            }
                                            for (j = 0; j < LstTreatment.Items.Count; j++)
                                            {
                                                blnPresent = false;
                                                if (LstTreatment.Items[j].ToString() == dgCustomGrid.get_GetItem(i, 1).ToString())
                                                {
                                                    blnPresent = true;
                                                    break;
                                                }
                                            }
                                            if (blnPresent == false)
                                                //Sanjog Added on 20101101 for dont allow the repeated item
                                                LstTreatment.Items.Add(dgCustomGrid.get_GetItem(i, 1).ToString());
                                        }
                                        else if (strLst == "diag")
                                        {
                                            //Sanjog Added on 20101101 for dont allow the repeated item
                                            Int32 j = 0;
                                            bool blnPresent = false;
                                            for (j = 0; j < LstDiagnosis.Items.Count; j++)
                                            {
                                                blnPresent = false;
                                                if (LstDiagnosis.Items[j].ToString() == dgCustomGrid.get_GetItem(i, 1).ToString())
                                                {
                                                    blnPresent = true;
                                                    break;
                                                }
                                            }
                                            if (blnPresent == false)
                                                //Sanjog Added on 20101101 for dont allow the repeated item
                                                LstDiagnosis.Items.Add(dgCustomGrid.get_GetItem(i, 1).ToString());
                                        }
                                        else if (strLst == "PatientMedication")
                                        {
                                            string str = dgCustomGrid.get_GetItem(i, 1).ToString();
                                            if (!lstPatMedication.Items.Contains(str))
                                                lstPatMedication.Items.Add(dgCustomGrid.get_GetItem(i, 1).ToString());

                                            if ((!chkPrblmLstOthrMedication.Checked))
                                            {
                                                chkPrblmLstOthrMedication.Checked = true;
                                            }
                                        }
                                        else if (strLst == "PatientImmunization")
                                        {
                                            string str = dgCustomGrid.get_GetItem(i, 1).ToString();
                                            if (!lstImmunization.Items.Contains(str))
                                                lstImmunization.Items.Add(str);


                                            if ((!chkPrblmLstOthrImmunization.Checked))
                                            {
                                                chkPrblmLstOthrImmunization.Checked = true;
                                            }
                                        }

                                        else if (strLst == "PatientProblem".ToLower())
                                        {
                                            string str = dgCustomGrid.get_GetItem(i, 1).ToString();
                                            if (!lstProblemList.Items.Contains(str))
                                            {
                                                _DictionaryICD9.Add(dgCustomGrid.get_GetItem(i, 1).ToString(), dgCustomGrid.get_GetItem(i, 2).ToString());
                                                lstProblemList.Items.Add(dgCustomGrid.get_GetItem(i, 1).ToString());

                                                if ((!chkPrblmLstOthrProblemList.Checked))
                                                {
                                                    chkPrblmLstOthrProblemList.Checked = true;
                                                }
                                            }
                                        }

                                        else if (strLst == "allergies")
                                        {
                                            string str = dgCustomGrid.get_GetItem(i, 1).ToString();
                                            if (!lstAllergy.Items.Contains(str))
                                                lstAllergy.Items.Add(dgCustomGrid.get_GetItem(i, 1).ToString());

                                            if ((!chkPrblmLstOthrAllergy.Checked))
                                            {
                                                chkPrblmLstOthrAllergy.Checked = true;
                                            }
                                        }

                                        else if (strLst == "gender")
                                        {
                                            string str = dgCustomGrid.get_GetItem(i, 1).ToString();

                                            if (str != "" && str != null)
                                            {
                                                cmbGender.Items.Add(dgCustomGrid.get_GetItem(i, 1).ToString());
                                                cmbGender.SelectedIndex = 0;
                                                if ((!chkPrblLstGender.Checked))
                                                {
                                                    chkPrblLstGender.Checked = true;
                                                }
                                            }
                                        }
                                        else if (strLst == "InsPlan")
                                        {
                                            string str = dgCustomGrid.get_GetItem(i, 1).ToString();
                                            if (str != "" && str != null)
                                            {
                                                if (!cmbPatInsPlan.Items.Contains(str))
                                                {
                                                    _DictionaryPatInsPlan.Add(dgCustomGrid.get_GetItem(i, 1).ToString(), dgCustomGrid.get_GetItem(i, 2).ToString());
                                                    cmbPatInsPlan.DataSource = new BindingSource(_DictionaryPatInsPlan, null);
                                                    cmbPatInsPlan.DisplayMember = "Value";
                                                    cmbPatInsPlan.ValueMember = "Key"; 

                                                }
                                                //if ((!chkPriInsPlan.Checked))
                                                //{
                                                //    chkPriInsPlan.Checked = true;
                                                //}
                                                //if ((!ChkSecInsPlan.Checked))
                                                //{
                                                //    ChkSecInsPlan.Checked = true;
                                                //}
                                                cmbPatInsPlan.SelectedIndex = 0;
                                            }
                                        }
                                        else if (strLst == "race")
                                        {
                                            string str = dgCustomGrid.get_GetItem(i, 1).ToString();

                                            if (str != "" && str != null)
                                            {
                                                cmbRace.Items.Add(dgCustomGrid.get_GetItem(i, 1).ToString());
                                                cmbRace.SelectedIndex = 0;

                                                if ((!chkPrblLstRace.Checked))
                                                {
                                                    chkPrblLstRace.Checked = true;
                                                }
                                            }
                                        }

                                        else if (strLst == "ethnicity")
                                        {

                                            string str = dgCustomGrid.get_GetItem(i, 1).ToString();

                                            if (str != "" && str != null)
                                            {
                                                cmbethnicity.Items.Add(dgCustomGrid.get_GetItem(i, 1).ToString());
                                                cmbethnicity.SelectedIndex = 0;

                                                if ((!chkPrblLEthnicity.Checked))
                                                {
                                                    chkPrblLEthnicity.Checked = true;
                                                }
                                            }
                                        }

                                        else if (strLst == "language")
                                        {

                                            string str = dgCustomGrid.get_GetItem(i, 1).ToString();

                                            if (str != "" && str != null)
                                            {
                                                cmblanguage.Items.Add(dgCustomGrid.get_GetItem(i, 1).ToString());
                                                cmblanguage.SelectedIndex = 0;

                                                if ((!chkPrblLLangauge.Checked))
                                                {
                                                    chkPrblLLangauge.Checked = true;
                                                }
                                            }
                                        }
                                        else if (strLst == "MedicalCategory")
                                        {

                                            string str = dgCustomGrid.get_GetItem(i, 1).ToString();

                                            if (str != "" && str != null)
                                            {
                                                cmbMedicalCategory.Items.Add(dgCustomGrid.get_GetItem(i, 1).ToString());
                                                cmbMedicalCategory.SelectedIndex = 0;

                                                if ((!chkPrblLMedicalCategory.Checked))
                                                {
                                                    chkPrblLMedicalCategory.Checked = true;
                                                }
                                            }
                                        }

                                        else if (strLst == "com prefrrence")
                                        {

                                            string str = dgCustomGrid.get_GetItem(i, 1).ToString();
                                            if (str != "" && str != null)
                                            {
                                                cmbComPre.Items.Add(dgCustomGrid.get_GetItem(i, 1).ToString());
                                                cmbComPre.SelectedIndex = 0;

                                                if ((!chkPrblLCommPref.Checked))
                                                {
                                                    chkPrblLCommPref.Checked = true;
                                                }
                                            }
                                        }
                                        else if (strLst == "ClaimCpt")
                                        {


                                            string str = dgCustomGrid.get_GetItem(i, 1).ToString();
                                            if (str != "" && str != null)
                                            {
                                                if (!lstClaimCpt.Items.Contains(str))
                                                {
                                                    _DictionaryClaimCPT.Add(dgCustomGrid.get_GetItem(i, 1).ToString(), dgCustomGrid.get_GetItem(i, 2).ToString());
                                                    lstClaimCpt.Items.Add(dgCustomGrid.get_GetItem(i, 1).ToString());

                                                }
                                                if ((!chkClaimCPT.Checked))
                                                {
                                                    chkClaimCPT.Checked = true;
                                                }
                                            }

                                        }
                                        else if (strLst == "ClaimDx")
                                        {
                                            string str = dgCustomGrid.get_GetItem(i, 1).ToString();
                                            if (str != "" && str != null)
                                            {
                                                if (!lstClaimDx.Items.Contains(str))
                                                {
                                                    _DictionaryClaimDx.Add(dgCustomGrid.get_GetItem(i, 1).ToString(), dgCustomGrid.get_GetItem(i, 2).ToString());
                                                    lstClaimDx.Items.Add(dgCustomGrid.get_GetItem(i, 1).ToString());
                                                }
                                                if ((!chkClaimDX.Checked))
                                                {
                                                    chkClaimDX.Checked = true;
                                                }
                                            }
                                        }
                                        else if (strLst == "Facility")
                                        {


                                            string str = dgCustomGrid.get_GetItem(i, 1).ToString();
                                            if (str != "" && str != null)
                                            {
                                                if (!lstClaimCpt.Items.Contains(str))
                                                {
                                                    _DictionaryFacility.Add(dgCustomGrid.get_GetItem(i, 1).ToString(), dgCustomGrid.get_GetItem(i, 2).ToString());
                                                    lstFacility.Items.Add(dgCustomGrid.get_GetItem(i, 1).ToString());

                                                }
                                                if ((!chkFacility.Checked))
                                                {
                                                    chkFacility.Checked = true;
                                                }
                                            }

                                        }


                                    }
                                    //}
                                }

                            }
                        }
                        else
                        {

                            string str = dgCustomGrid.get_GetItem(dgCustomGrid.GetCurrentrowIndex, 0).ToString();
                            cmbLabResult.Text = "";
                            cmbLabResult.Text = str;


                        }

                        if ((_reportName == "rptMedicationMU") || (_reportName == "rpteRx") || (_reportName == "rptPatientPrescriptions"))
                        {
                            LstMedication.DataSource = dtTemp;
                            LstMedication.DisplayMember = "Drug";
                            LstMedication.ValueMember = "NDCCode";
                        }
                        pnlcustomTask.Visible = false;
                    }

                    else
                    {
                        pnlcustomTask.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dgCustomGrid.Visible = false;
            }

        }

        void dgCustomGrid_CloseClick(object sender, EventArgs e)
        {
            dgCustomGrid.Visible = false;
            pnlcustomTask.Visible = false;
        }

        void dgCustomGrid_SearchChanged(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DataView dvPatient  = (DataView)dgCustomGrid.GridDatasource;
                if (dvPatient == null)
                {
                    this.Cursor = Cursors.Default;
                    return;
                }

                string strPatientSearchDetails = "";
                if (dgCustomGrid.SearchText.ToString().Trim() != "")
                {
                    strPatientSearchDetails = dgCustomGrid.SearchText.ToString().Replace("'", "''");
                    strPatientSearchDetails = strPatientSearchDetails.Replace("[", "") + "";
                    strPatientSearchDetails = ReplaceSpecialCharacters(strPatientSearchDetails);
                }
                else
                {
                    strPatientSearchDetails = "";
                }

                // Mayuri-20100507 - START WITH SEARCH FOR DRUG AND FOR DIAGNOSIS, PROCEDURES INSERTING SEARCH
                if (strLst == "drugs")
                {
                    dvPatient.RowFilter = "[" + dvPatient.Table.Columns[0].ColumnName + "]" + " Like '" + strPatientSearchDetails + "%'  ";
                }
                else if (strLst == "InsPlan")
                {
                    dvPatient.RowFilter = "[" + dvPatient.Table.Columns[1].ColumnName + "]" + " Like '" + strPatientSearchDetails + "%'  ";
                }
                else
                {
                    dvPatient.RowFilter = "[" + dvPatient.Table.Columns[0].ColumnName + "]" + " Like '%" + strPatientSearchDetails + "%' ";
                }

                dgCustomGrid.Enabled = false;
                dgCustomGrid.datasource(dvPatient);
                dgCustomGrid.Enabled = true;
                this.Cursor = Cursors.Default;

                if ((strLst.ToLower()) == "insplan")
                {
                    dgCustomGrid.C1Grid.Cols[1].Width = 0;
                }
                else
                {
                    if (dgCustomGrid.C1Task.Cols.Count > 2)
                        dgCustomGrid.C1Task.Cols[2].Visible = false;
                }
                dgCustomGrid.Selectsearch(CustomTask.enmcontrol.Search);
            }
            catch (Exception objErr)
            {
                this.Cursor = Cursors.Default;
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Select, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                MessageBox.Show(objErr.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK);
            }
        }

        void dgCustomGrid_rbtnBeersListClick(object sender, EventArgs e)
        {
            DataView dv = dt.DefaultView;
            if (dgCustomGrid.IsCheckrbtnBeersList == true)
            {
                BindUserGrid(true);
                dgCustomGrid.SetSelectAllVisible = true;
                //to deselect deselectall button if user changes option radio button
                dgCustomGrid.SetDeSelectAllVisible = false;
                dgCustomGrid.datasource(dt.DefaultView);
            }
            else
            {
                if (_reportName == "rptPatientPrescriptions")
                {
                    dgCustomGrid.SetSelectAllVisible = true;
                    dgCustomGrid.SetDeSelectAllVisible = false;

                }
                else
                {
                    dgCustomGrid.SetSelectAllVisible = false;
                    dgCustomGrid.SetDeSelectAllVisible = false;
                }

                BindUserGrid();
                dgCustomGrid.datasource(dt.DefaultView);
            }
        }

        public string ReplaceSpecialCharacters(string strSpecialChar)
        {
            try
            {
                strSpecialChar = strSpecialChar.Replace("#", "[#]") + "";
                strSpecialChar = strSpecialChar.Replace("$", "[$]") + "";
                strSpecialChar = strSpecialChar.Replace("%", "[%]") + "";
                strSpecialChar = strSpecialChar.Replace("^", "[^]") + "";
                strSpecialChar = strSpecialChar.Replace("&", "[&]") + "";
                strSpecialChar = strSpecialChar.Replace("~", "[~]") + "";
                strSpecialChar = strSpecialChar.Replace("!", "[!]") + "";
                strSpecialChar = strSpecialChar.Replace("*", "[*]") + "";
                strSpecialChar = strSpecialChar.Replace(";", "[;]") + "";
                strSpecialChar = strSpecialChar.Replace("/", "[/]") + "";
                strSpecialChar = strSpecialChar.Replace("?", "[?]") + "";
                strSpecialChar = strSpecialChar.Replace(">", "[>]") + "";
                strSpecialChar = strSpecialChar.Replace("<", "[<]") + "";
                strSpecialChar = strSpecialChar.Replace("\\", "[\\]") + "";
                strSpecialChar = strSpecialChar.Replace("|", "[|]") + "";
                strSpecialChar = strSpecialChar.Replace("{", "[{]") + "";
                strSpecialChar = strSpecialChar.Replace("}", "[}]") + "";
                strSpecialChar = strSpecialChar.Replace("-", "[-]") + "";
                strSpecialChar = strSpecialChar.Replace("_", "[_]") + "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return strSpecialChar;
        }

        private void BindUserGrid(Boolean beerslist)
        {
            try
            {
                if (_reportName == "rptPatientPrescriptions")
                {
                    dt = FillDrugs_Rx(beerslist);
                }
                else
                {
                    dt = FillDrugs(beerslist);
                }

                CustomDrugsGridStyle();
                DataColumn col = new DataColumn();
                col.ColumnName = "Select Data";
                col.DataType = System.Type.GetType("System.Boolean");
                dgCustomGrid.C1Grid.SetData(0, Col_Name, 2);
                col.DefaultValue = false;
                if (!(dt.Columns.Contains(col.ColumnName)))
                {
                    dt.Columns.Add(col);
                }

                if ((dt != null))
                {
                    dgCustomGrid.datasource(dt.DefaultView);
                }
                //if (_reportName == "rptPatientPrescriptions")
                //{
                //    //float _TotalWidth = dgCustomGrid.Gridwidth - 5;
                //    dgCustomGrid.C1Grid.Cols.Move(dgCustomGrid.C1Grid.Cols.Count - 1, 0);
                //    dgCustomGrid.C1Grid.AllowEditing = true;
                //    dgCustomGrid.C1Grid.Cols[1].AllowEditing = false;
                //    if (dgCustomGrid.C1Grid.Cols.Count > 2)
                //    {
                //        dgCustomGrid.C1Grid.Cols[2].Width = 0;
                //    }

                //}
                // RESET THE GRID

                float _TotalWidth = dgCustomGrid.Gridwidth - 5;
                dgCustomGrid.C1Grid.Cols.Move(dgCustomGrid.C1Grid.Cols.Count - 1, 0);
                dgCustomGrid.C1Grid.AllowEditing = true;
                if (_reportName == "rptPatientPrescriptions")
                {
                    dgCustomGrid.C1Grid.Cols[1].AllowEditing = false;
                    if (dgCustomGrid.C1Grid.Cols.Count > 2)
                    {
                        dgCustomGrid.C1Grid.Cols[2].Width = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Show Reports", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CustomDrugsGridStyle()
        {
            float _TotalWidth = dgCustomGrid.C1Grid.Width - 5;

            // SHOW DRUG INFO
            dgCustomGrid.C1Grid.Cols.Fixed = 0;
            dgCustomGrid.C1Grid.Rows.Fixed = 1;
            dgCustomGrid.C1Grid.Cols.Count = Col_Count;
            dgCustomGrid.C1Grid.AllowEditing = true;
            dgCustomGrid.C1Grid.SetData(0, Col_Check, "Select");
            dgCustomGrid.C1Grid.SetData(0, Col_Name, "Name");
            dgCustomGrid.C1Grid.SetData(0, Col_NDCCode, "NDCCode");
            dgCustomGrid.C1Grid.Cols[3].Width = 0;
            dgCustomGrid.Height = 400;
            dgCustomGrid.Width = 500;
        }

        private void BindUserGrid()
        {
            try
            {
                switch (strLst.ToLower())
                {
                    case "patientmedication":
                        {
                            dt = FillDrugs();
                            break;
                        }
                    case "patientimmunization":
                        {
                            dt = FillImmunization();
                            break;
                        }
                    case "patientlabresults":
                        {
                            dt = GetLabTestResults();
                            break;
                        }
                    case "drugs":
                        {
                            if (bHistory == true || bDM == true) // HISTORY OR DM
                            {
                                dt = FillCategory();
                            }
                            else
                            {
                                dt = FillDrugs();
                            }
                            break;
                        }

                    case "cpt":
                        {
                            dt = FillCPT();
                            break;
                        }

                    case "diag":
                        {
                            if (bHistory == true) // history
                            {
                                // GET CATEG
                                string strHisCateg = "";
                                int i = 0;
                                LstMedication.Refresh();

                                // COLLECT THE SELECTED DATA OF CHECK LIST
                                for (i = 0; i <= LstMedication.Items.Count - 1; i++)
                                {
                                    if (i == 0)
                                    {
                                        strHisCateg = "'" + LstMedication.Items[i].ToString().Trim().Replace("'", "''") + "'";
                                    }
                                    else
                                    {
                                        strHisCateg = strHisCateg + " or '" + LstMedication.Items[i].ToString().Trim().Replace("'", "''") + "'";
                                    }
                                }
                                dt = FillCategDetails(strHisCateg);
                            }
                            else
                            {
                                dt = FillDiagnosis();
                            }
                            break;
                        }

                    case "allergies":
                        {
                            dt = FillCategDetails("'Allergies'");
                            break;
                        }
                    case "gender":
                        {
                            dt = FillGender();
                            break;
                        }

                    case "medicalcategory":
                        {
                            if (dsDemographic != null && dsDemographic.Tables[4] != null)
                            {
                                dt = null;
                                dt = dsDemographic.Tables[4].Copy();
                            }
                            break;
                        }

                    case "race":
                        {
                            if (dsDemographic != null && dsDemographic.Tables[3] != null)
                            {
                                dt = null;
                                dt = dsDemographic.Tables[3].Copy();
                            }

                            break;
                        }
                    case "ethnicity":
                        {
                            if (dsDemographic != null && dsDemographic.Tables[0] != null)
                            {
                                dt = null;
                                dt = dsDemographic.Tables[0].Copy();
                            }

                            break;
                        }
                    case "language":
                        {
                            if (dsDemographic != null && dsDemographic.Tables[1] != null)
                            {
                                dt = null;
                                dt = dsDemographic.Tables[1].Copy();
                            }

                            break;
                        }
                    case "insplan":
                        {
                            dt = FillInsurancePlan();
                            break;
                        }
                    case "com prefrrence":
                        {
                            if (dsDemographic != null && dsDemographic.Tables[2] != null)
                            {
                                dt = null;
                                dt = dsDemographic.Tables[2].Copy();
                            }

                            break;
                        }
                    // chetan added for patient reminder on 20 oct 2010
                    case "patientdmmedication":
                        {
                            dt = FillDrugs();
                            break;
                        }

                    case "patientdmproblem":
                        {
                            if (_reportName == "rptPatientList")
                            {
                                dt = FillICD9Code();
                            }
                            else
                            {
                                dt = FillChiefComplaint();
                            }
                            break;
                        }
                    case "patientproblem":
                        {
                            if (_reportName == "rptPatientList")
                            {
                                dt = FillICD9Code();
                            }
                            else
                            {
                                dt = FillChiefComplaint();
                            }
                            break;
                        }
                    case "patdmcriteria":
                        {
                            dt = FillDM();
                            break;
                        }
                    case "claimcpt":
                        {
                            dt = FillClaimCPT();
                            break;
                        }
                    case "claimdx":
                        {
                            dt = FillClaimDx();
                            break;
                        }
                    case "facility":
                        {
                            dt = FillClaimFacility();
                            break;
                        }
                    // chetan added for patient reminder on 20 oct 2010

                }

                CustomDrugsGridStyle();
                if (strLst.ToLower() != "patientlabresults")
                {
                    DataColumn col = new DataColumn();
                    col.ColumnName = "Select Data";
                    col.DataType = System.Type.GetType("System.Boolean");
                    col.DefaultValue = false;

                    if (!(dt.Columns.Contains(col.ColumnName)))
                    {
                        dt.Columns.Add(col);
                    }
                }

                if ((dt != null))
                {
                    dgCustomGrid.datasource(dt.DefaultView);
                }

                //RESET THE GRID
                float _TotalWidth = dgCustomGrid.Gridwidth - 5;
                dgCustomGrid.C1Grid.Cols.Move(dgCustomGrid.C1Grid.Cols.Count - 1, 0);
                dgCustomGrid.C1Grid.AllowEditing = true;

                if (dgCustomGrid.C1Grid.Cols.Count > 0)
                {
                    dgCustomGrid.C1Grid.Cols[0].AllowSorting = false;
                }

                if (strLst.ToLower() != "patientlabresults")
                {
                    dgCustomGrid.C1Grid.Cols[1].AllowEditing = false;
                }
                else
                {
                    dgCustomGrid.C1Grid.Cols[0].AllowEditing = false;
                }

                if ((strLst.ToLower()) == "insplan")
                {
                    dgCustomGrid.C1Grid.Cols[1].Width = 0;
                }
                else
                {
                    if (dgCustomGrid.C1Grid.Cols.Count > 2)
                    {
                        dgCustomGrid.C1Grid.Cols[2].Width = 0;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Show Reports", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public DataTable FillImmunization()
        {
            DataBaseLayer oDB = new DataBaseLayer();
            string _strSQL = "";
            try
            {
                //convert(varchar(18),nDrugsID)+' : '+
                //_strSQL = "select nDrugsID, sDrugName,isnull(sDosage,'') as sDosage FROM Drugs_MST order by sDrugname "
                _strSQL = "select im_svaccine as Immunization from IM_Mst union select im_item_name as Immunization from IM_Trn_Dtl order by Immunization";//Developer:Pradeep/Date:03/02/2012/Bug ID:20575/Reason:table changes done in 6060
                DataTable oDiag = oDB.GetDataTable_Query(_strSQL);
                if (oDiag != null)
                {
                    return oDiag;
                }
                else
                {
                    return null;

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return null;
            }
            finally
            {
                oDB.Dispose();
                oDB = null;
            }
        }

        //     Private Sub SetCheckValues(ByVal LstBx As ListBox)
        //    Dim k As Integer
        //    For k = 0 To LstBx.Items.Count - 1
        //        For i As Int32 = 0 To dgCustomGrid.C1Task.Rows.Count - 1
        //            If dgCustomGrid.GetItem(i, 1).ToString.Trim = LstBx.Items(k).ToString.Trim Then
        //                dgCustomGrid.C1Task.SetCellCheck(i, 0, C1.Win.C1FlexGrid.CheckEnum.Checked)
        //                Exit For
        //            End If
        //        Next
        //    Next
        //End Sub


        private void SetCheckValues(ListBox lst)
        {
            int cnt = 0;
            int lstitem = 0;
            for (lstitem = 0; lstitem < lst.Items.Count; lstitem++)
            {
                for (cnt = 0; cnt < dgCustomGrid.C1Task.Rows.Count; cnt++)
                {
                    if (dgCustomGrid.get_GetItem(cnt, 1).ToString().Trim() == lst.Items[lstitem].ToString().Trim())
                    {
                        dgCustomGrid.C1Task.SetCellCheck(cnt, 0, C1.Win.C1FlexGrid.CheckEnum.Checked);

                        break;
                    }
                }
            }
        }

        private void SetCheckValues(ComboBox lst)
        {
            int cnt = 0;
            int lstitem = 0;
            for (lstitem = 0; lstitem < lst.Items.Count; lstitem++)
            {
                for (cnt = 1; cnt < dgCustomGrid.C1Task.Rows.Count; cnt++)
                {
                    if (strLst.ToLower() == "insplan")
                    {
                        if (dgCustomGrid.get_GetItem(cnt, 1).ToString().Trim() == ((System.Collections.Generic.KeyValuePair<string, string>)(lst.Items[lstitem])).Key.ToString().Trim())
                        {
                            dgCustomGrid.C1Task.SetCellCheck(cnt, 0, C1.Win.C1FlexGrid.CheckEnum.Checked);

                            break;
                        }
                    }
                    else
                    {

                        if (dgCustomGrid.get_GetItem(cnt, 1).ToString().Trim() == lst.Items[lstitem].ToString().Trim())
                        {
                            dgCustomGrid.C1Task.SetCellCheck(cnt, 0, C1.Win.C1FlexGrid.CheckEnum.Checked);

                            break;
                        }
                    }
                }
            }
        }

        // chetan added for patient reminder on 20 oct 2010

        public DataTable FillDM()
        {
            DataBaseLayer oDB = new DataBaseLayer();


            string _strSQL = "";
            try
            {
                //convert(varchar(18),nDrugsID)+' : '+
                //_strSQL = "select nDrugsID, sDrugName,isnull(sDosage,'') as sDosage FROM Drugs_MST order by sDrugname "
                _strSQL = " select isnull(dm_mst_criterianame,'') as [Disease Management] ,dm_mst_id FROM dm_criteria_mst WHERE dm_mst_PatientID = 0  ORDER BY dm_mst_criterianame ";

                DataTable oDiag = oDB.GetDataTable_Query(_strSQL);
                if (oDiag != null)
                {
                    return oDiag;
                }
                else
                {

                    return null;

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return null;
            }
            finally
            {
                oDB.Dispose();
                oDB = null;
            }


        }


        public Boolean ChkSnomedSetting()
        {
            DataBaseLayer oDB = new DataBaseLayer();

            string _strSQL = "";
            DataTable oDiag = null;
            try
            {
                //convert(varchar(18),nDrugsID)+' : '+
                //_strSQL = "select nDrugsID, sDrugName,isnull(sDosage,'') as sDosage FROM Drugs_MST order by sDrugname "
                _strSQL = " select isnull(sSettingsValue,'False') as SMDBSetting  FROM Settings  where sSettingsName='GLOSMDBSETTING'";

                oDiag = oDB.GetDataTable_Query(_strSQL);
                if (oDiag != null)
                {
                    if (oDiag.Rows.Count > 0)
                    {
                        if (oDiag.Rows[0][0].ToString() == "True")
                            return true;
                        else
                            return false;
                    }
                    else
                        return false;
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
                oDB.Dispose();
                oDB = null;
                if (oDiag != null)
                {
                    oDiag.Dispose();
                    oDiag = null;
                }
            }
        }
        // chetan added for patient reminder on 20 oct 2010



        public DataTable FillCategory()
        {
            DataBaseLayer oDB = new DataBaseLayer();
            string _strSQL = "";
            try
            {
                if (bHistory == true)
                {
                    //Sanjog Added on 20101101 to show "Description" in header rather than the sDescription
                    _strSQL = " select sDescription As Description from Category_Mst where sCategoryType='history' and  nCategoryID<>-1 ORDER BY sDescription ";
                    //Sanjog Added on 20101101 to show "Description" in header rather than the sDescription
                }
                else
                {
                    if (bDM == true)
                    {
                        _strSQL = " select isnull(dm_mst_criterianame,'') as [DM_Criteria] FROM dm_criteria_mst ORDER BY dm_mst_criterianame ";
                    }
                }
                DataTable oDiag = oDB.GetDataTable_Query(_strSQL);
                if (oDiag != null)
                {
                    return oDiag;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                oDB.Dispose();
                oDB = null;
            }
        }

        public DataTable FillDrugs(Boolean blist)
        {
            DataBaseLayer oDB = new DataBaseLayer();
            string _strSQL = "";
            try
            {
                if (blist == true)
                {
                    _strSQL = "SELECT DISTINCT isnull(sDrugName,'') + ' : '+ isnull(sDosage,'')as [DrugName] FROM beerslist_mst";
                }
                else
                {
                    _strSQL = "SELECT DISTINCT isnull(Drugs_MST.sDrugName,'') + ' : '+ isnull(Drugs_MST.sDosage,'')as [DrugName] FROM Medication INNER JOIN Drugs_MST ON Medication.sNDCCode = Drugs_MST.sNDCCode WHERE(Drugs_MST.nDrugType = 1)AND (Medication.nPrescriptionID <> 0) ";
                }

                DataTable oDiag = oDB.GetDataTable_Query(_strSQL);
                if (oDiag != null)
                {
                    return oDiag;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                oDB.Dispose();
                oDB = null;
            }
        }
        public DataTable FillDrugs_Rx(Boolean blist)
        {
            DataBaseLayer oDB = new DataBaseLayer();
            string _strSQL = "";
            try
            {
                if (blist == true)
                {
                    _strSQL = "select *,(select top 1 ISNULL(sNDCCode,'') AS NDCCode FROM beerslist_mst WHERE isnull(sDrugName,'') + ' : '+ isnull(sDosage,'')=MyTable.DrugName) as NDCCode  from( SELECT DISTINCT isnull(sDrugName,'') + ' : '+ isnull(sDosage,'')as [DrugName] FROM beerslist_mst)MyTable order by [DrugName] ";
                }
                else
                {
                    _strSQL = "SELECT DISTINCT isnull(Drugs_MST.sDrugName,'') + ' : '+ isnull(Drugs_MST.sDosage,'')as [DrugName],ISNULL(Drugs_MST.sNDCCode,'') AS NDCCode FROM Medication INNER JOIN Drugs_MST ON Medication.sNDCCode = Drugs_MST.sNDCCode WHERE(Drugs_MST.nDrugType = 1)AND (Medication.nPrescriptionID <> 0) ";
                }

                DataTable oDiag = oDB.GetDataTable_Query(_strSQL);
                if (oDiag != null)
                {
                    return oDiag;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                oDB.Dispose();
                oDB = null;
            }
        }
        public DataTable FillDrugs()
        {
            DataBaseLayer oDB = new DataBaseLayer();
            string _strSQL = "";
            try
            {
                _strSQL = ";WITH    temp AS ( SELECT   nVisitID ,nPatientID FROM     Visits AS V WHERE    dtVisitDate IN ( SELECT MAX(dtVisitDate)  FROM   dbo.Visits GROUP BY nPatientID) )SELECT distinct M.sMedication ,m.sNDCCode FROM    temp INNER JOIN Medication M ON M.nVisitID = temp.nVisitID WHERE   ( M.sStatus = 'Active'  OR M.sStatus = '') ORDER BY m.sMedication ";
                DataTable oDiag = oDB.GetDataTable_Query(_strSQL);
                if (oDiag != null)
                {
                    return oDiag;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                oDB.Dispose();
                oDB = null;
            }
        }


        public DataTable FillChiefComplaint()
        {
            DataBaseLayer oDB = new DataBaseLayer();
            string _strSQL = "";
            try
            {
                //added for ICd10 implemantation
                if (lstDMProblemList.Items.Count == 0) //if no item selected then 
                {
                    dgCustomGrid.rbICD10Transition = ICD10Transition;
                    if (ICD10Transition == false) //for ICD9
                    {
                        _strSQL = "select Distinct(ISNULL(sCheifComplaint,''))as  Description  FROM Problemlist where ISNULL(nICDRevision,9)=9 order by Description ";
                       
                    }
                    else  //for ICD10
                    {
                        _strSQL = "select Distinct(ISNULL(sCheifComplaint,''))as  Description  FROM Problemlist where ISNULL(nICDRevision,9)=10 order by Description ";

                    }
                }
                else
                {


                    if (IsICD9Checked == true ) //for ICD9
                    {
                        _strSQL = "select Distinct(ISNULL(sCheifComplaint,''))as  Description  FROM Problemlist where ISNULL(nICDRevision,9)=9 order by Description ";
                        dgCustomGrid.rbICD10Transition = false; 
                    }
                    else  //for ICD10
                    {
                        _strSQL = "select Distinct(ISNULL(sCheifComplaint,''))as  Description  FROM Problemlist where ISNULL(nICDRevision,9)=10 order by Description ";
                        dgCustomGrid.rbICD10Transition = true;
                    }

                }
                DataTable    oDiag = oDB.GetDataTable_Query(_strSQL);
                if (oDiag != null)
                {
                    return oDiag;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return null;
            }
            finally
            {
                oDB.Dispose();
                oDB = null;
            }
        }
        // chetan added for filling Problemlist 
        public DataTable FillICD9Code()
        {
            DataBaseLayer oDB = new DataBaseLayer();
            string _strSQL = "";
            try
            {
                //added for ICd10 implemantation
                    if (ICD10Transition == false) //for ICD9
                        _strSQL = " select distinct LTRIM(RTRIM(sICD9code))+' - '+LTRIM(RTRIM(sDescription)) AS [ICD9 Code] ,LTRIM(RTRIM(sICD9code)) As ICD9 from ICD9 where isnull(sICD9Code,'') != '' AND ISNULL(nICDRevision,9)=9 order by [ICD9 Code]";
                    else
                        _strSQL = " select distinct LTRIM(RTRIM(sICD9code))+' - '+LTRIM(RTRIM(sDescription)) AS [ICD9 Code] ,LTRIM(RTRIM(sICD9code)) As ICD9 from ICD9 where isnull(sICD9Code,'') != '' AND ISNULL(nICDRevision,9)=10  AND nCodeType=1 order by [ICD9 Code]";
                

                DataTable oDiag = oDB.GetDataTable_Query(_strSQL);
                if (oDiag != null)
                {
                    return oDiag;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return null;
            }
            finally
            {
                oDB.Dispose();
                oDB = null;
            }
        }


        public void  FillICD9Type(int ICDType)
        {
        
             this.Cursor = Cursors.WaitCursor  ;
            DataBaseLayer oDB = new DataBaseLayer();
            string _strSQL = "";
            try
            {
                //added for ICd10 implemantation
                if (_reportName == "rptPatientList")
                {
                    if (ICDType == 9)
                        _strSQL = " select distinct LTRIM(RTRIM(sICD9code))+' - '+LTRIM(RTRIM(sDescription)) AS [ICD9 Code] ,LTRIM(RTRIM(sICD9code)) As ICD9 from ICD9 where isnull(sICD9Code,'') != '' AND ISNULL(nICDRevision,9)=9 order by [ICD9 Code]";
                    else
                        _strSQL = " select distinct LTRIM(RTRIM(sICD9code))+' - '+LTRIM(RTRIM(sDescription)) AS [ICD9 Code] ,LTRIM(RTRIM(sICD9code)) As ICD9 from ICD9 where isnull(sICD9Code,'') != '' AND ISNULL(nICDRevision,9)=10  AND nCodeType=1 order by [ICD9 Code]";
                }
                if (_reportName == "rptPatientListDM")
                {
                    if (ICDType  == 9) //for ICD9
                    {
                        _strSQL = "select Distinct(ISNULL(sCheifComplaint,''))as  Description  FROM Problemlist where ISNULL(nICDRevision,9)=9 order by Description ";
                    }
                    else  //for ICD10
                    {
                        _strSQL = "select Distinct(ISNULL(sCheifComplaint,''))as  Description  FROM Problemlist where ISNULL(nICDRevision,9)=10 order by Description ";

                    }
                }
                dgCustomGrid.datasource(null);
              
                DataTable oDiag = oDB.GetDataTable_Query(_strSQL);
                CustomDrugsGridStyle();
               
                    DataColumn col = new DataColumn();
                    col.ColumnName = "Select Data";
                    col.DataType = System.Type.GetType("System.Boolean");
                    col.DefaultValue = false;

                    if (!(oDiag.Columns.Contains(col.ColumnName)))
                    {
                        oDiag.Columns.Add(col);
                    }
               

                if ((oDiag != null))
                {
                    dgCustomGrid.datasource(oDiag.DefaultView);
                }

                //RESET THE GRID
                float _TotalWidth = dgCustomGrid.Gridwidth - 5;
                dgCustomGrid.C1Grid.Cols.Move(dgCustomGrid.C1Grid.Cols.Count - 1, 0);
                dgCustomGrid.C1Grid.AllowEditing = true;
               dgCustomGrid.C1Grid.Cols[1].AllowEditing = false;
                if (dgCustomGrid.C1Grid.Cols.Count > 2)
                {
                    dgCustomGrid.C1Grid.Cols[2].Width = 0;
                }
              
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
               
            }
            finally
            {
                oDB.Dispose();
                oDB = null;
                this.Cursor = Cursors.Default;
            }
        }

        public DataTable FillCPT()
        {
            DataBaseLayer oDB = new DataBaseLayer();
            string _strSQL = "";
            try
            {
                _strSQL = "select Distinct (isNull(rtrim(sCPTCode),'') + ' - ' + isNull(ltrim(sDescription),'')) as [CPT] from CPT_MST Where sCPTCode <>'' AND sDescription<>''";
                DataTable oCPT = oDB.GetDataTable_Query(_strSQL);
                if (oCPT != null)
                {
                    return oCPT;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return null;
            }
            finally
            {
                oDB.Dispose();
                oDB = null;
            }
        }

        public DataTable FillCategDetails(string sCateg)
        {
            DataBaseLayer oDB = new DataBaseLayer();
            string _strSQL = "";
            try
            {
                sCateg = sCateg.Replace(" or ", " or Category_MST.sDescription= ");

                //Developer:Yatin Bhagat
                //Date:6th Dec 2011
                //Bug ID/PRD Name/Salesforce Case:Mail subjected 'History Usage REport' by aniket sir
                //Reason: <If Any>
                //Condition Of If is Changed

                if (!sCateg.ToLower().Contains("allergies"))
                {
                    _strSQL = " SELECT History_MST.sDescription AS 'Description' " +
                    " FROM History_MST INNER JOIN Category_MST ON History_MST.nCategoryID=Category_MST.nCategoryID " +
                    " WHERE Category_MST.sDescription=" + sCateg + " AND Category_MST.sCategoryType='History' ORDER BY History_MST.sDescription ";
                }
                else
                {
                    _strSQL = " SELECT ISNULL(History_MST.sDescription,'') AS 'Description' FROM History_MST INNER JOIN Category_MST ON History_MST.nCategoryID=Category_MST.nCategoryID " +
                    " WHERE Category_MST.sDescription=" + sCateg + " AND Category_MST.sCategoryType='History' UNION  " +
                    " select ISNULL(Drugs_MST.sDrugName,'') AS 'Description' FROM DRUGS_MST WHERE  bIsAllergicDrug=1 ORDER BY Description ";
                }
                DataTable oDiag = oDB.GetDataTable_Query(_strSQL);
                if (oDiag != null)
                {
                    return oDiag;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return null;
            }
            finally
            {
                oDB.Dispose();
                oDB = null;
            }
        }

        public DataTable FillGender()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Gender");
            DataRow dr = dt.NewRow(); //Adding the new row int the Datatable

            try
            {
                dr["Gender"] = "Male";
                dt.Rows.InsertAt(dr, 0); //Need to be inserted in the speicfied location

                dr = dt.NewRow(); //Adding the new row int the Datatable

                dr["Gender"] = "Female";
                dt.Rows.InsertAt(dr, 1); //Need to be inserted in the speicfied location

                dr = dt.NewRow(); //Adding the new row int the Datatable

                dr["Gender"] = "Other";
                dt.Rows.InsertAt(dr, 2); //Need to be inserted in the speicfied location

                dt.AcceptChanges();  //After adding the row in the datatable accept the changes

                if (dt != null)
                {
                    return dt;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return null;
            }
            finally
            {
                if (dr != null)
                {
                    dr = null;
                }
            }

        }
        public DataTable FillDiagnosis()
        {
            DataBaseLayer oDB = new DataBaseLayer();
            string _strSQL = "";
            try
            {
                _strSQL = "select Distinct (isNull(rtrim(sICD9Code),'') + ' - ' + isNull(ltrim(sDescription),'')) as [Diagnosis] from ICD9 Where sICD9Code <>'' AND sDescription<>''";
                DataTable oDiag = oDB.GetDataTable_Query(_strSQL);
                if (oDiag != null)
                {
                    return oDiag;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return null;
            }
            finally
            {
                oDB.Dispose();
                oDB = null;
            }
        }

        public DataTable FillInsurancePlan()
        {
            DataBaseLayer oDB = new DataBaseLayer();
            string _strSQL = "";
            try
            {
                _strSQL = "SELECT nContactID,sName AS [Insurance Plan] FROM dbo.Contacts_MST WHERE sContactType='Insurance'";
                DataTable oDiag = oDB.GetDataTable_Query(_strSQL);
                if (oDiag != null)
                {
                    return oDiag;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return null;
            }
            finally
            {
                oDB.Dispose();
                oDB = null;
            }
        }

        public DataTable FillClaimCPT()
        {
            DataBaseLayer oDB = new DataBaseLayer();
            string _strSQL = "";
            try
            {
                _strSQL = "SELECT distinct CPT,sCPTCode FROM (SELECT DISTINCT sCPTCode + ' - ' + sCPTDescription AS CPT ,sCPTCode,CONVERT(DATETIME, CONVERT(CHAR(10), BL_Transaction_Lines.nFromDate, 101)) AS dos FROM dbo.BL_Transaction_Lines ) AS CPT WHERE DOS BETWEEN '" + dtpicFrom.Value + "' AND '" + dtpicTo.Value + "' ORDER BY CPT.CPT ASC";
                DataTable oDiag = oDB.GetDataTable_Query(_strSQL);
                if (oDiag != null)
                {
                    return oDiag;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return null;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
            }
        }

        public DataTable FillClaimDx()
        {
            DataBaseLayer oDB = new DataBaseLayer();
            string _strSQL = "";
            try
            {
                _strSQL = "SELECT distinct ClaimDx,sDx1Code FROM (SELECT  CONVERT(DATETIME, CONVERT(CHAR(10), BL_Transaction_Lines.nFromDate, 101)) AS dos ,BL_Transaction_Diagnosis.sDx1Code +' - ' +BL_Transaction_Diagnosis.sDx1Description AS ClaimDx,BL_Transaction_Diagnosis.sDx1Code FROM    BL_Transaction_Diagnosis INNER JOIN BL_Transaction_Lines ON BL_Transaction_Diagnosis.nTransactionID = BL_Transaction_Lines.nTransactionID ) AS ClaimDx  WHERE   DOS BETWEEN '" + dtpicFrom.Value + "' AND     '" + dtpicTo.Value + "' ORDER BY ClaimDx.ClaimDx ASC";
                DataTable oDiag = oDB.GetDataTable_Query(_strSQL);
                if (oDiag != null)
                {
                    return oDiag;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return null;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
            }
        }

        public DataTable FillClaimFacility()
        {
            DataBaseLayer oDB = new DataBaseLayer();
            string _strSQL = "";
            try
            {
                _strSQL = "SELECT  sFacilityName as Facility,sFacilityCode as FacilityCode FROM dbo.BL_FACILITY_MST ORDER BY sFacilityName ASC";
                DataTable oDiag = oDB.GetDataTable_Query(_strSQL);
                if (oDiag != null)
                {
                    return oDiag;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return null;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
            }
        }

        private void Tblbtn_More_Click(object sender, EventArgs e)
        {
            if (RptName == "rptPatientList")
            {
                pnlPatientList.Visible = true;
                pnlDemoFilter.Visible = true;
                pnlClaimDetails.Visible = true;
                pnlClaimDetails.BringToFront();
                //  pnlIncludeDemographics.Visible = true;
                pnlSSRSRpt.BringToFront();
                Tblbtn_DisplayOptn.Visible = true;


            }
            // chetan added for patient reminder on 20 oct 2010
            else if (RptName == "rptPatientListDM")
            {
                pnlDmPatientList.Visible = true;
            }
            // chetan added for patient reminder on 20 oct 2010
            else
            {
                pnlDrugDiagnosis.Visible = true;
            }
            Tblbtn_More.Visible = false;
            Tblbtn_Hide.Visible = true;
        }

        private void Tblbtn_Hide_Click(object sender, EventArgs e)
        {
            if (RptName == "rptPatientList")
            {
                pnlPatientList.Visible = false;
                pnlDemoFilter.Visible = false;
                pnlClaimDetails.Visible = false;
                pnlIncludeDemographics.Visible = false;
                Tblbtn_DisplayOptn.Visible = false;
                Tblbtn_DisplayOptn.Text = "Display Option";
                Tblbtn_DisplayOptn.Tag = "Display Option";
                Tblbtn_DisplayOptn.ToolTipText = "Display Option";
                pnlIncludeDemographics.Visible = false;
            }
            // chetan added for patient reminder on 20 oct 2010
            else if (RptName == "rptPatientListDM")
            {
                pnlDmPatientList.Visible = false;
            }
            // chetan added for patient reminder on 20 oct 2010
            else
            {
                pnlDrugDiagnosis.Visible = false;
            }
            Tblbtn_More.Visible = true;
            Tblbtn_Hide.Visible = false;
        }

        private void btnClearAllDiag_Click_1(object sender, EventArgs e)
        {
            LstDiagnosis.Items.Clear();
        }

        private void btnBrowseDrug_Click(object sender, EventArgs e)
        {
            strLst = "drugs";
            LoadUserGrid();
            pnlcustomTask.BringToFront();
        }

        private void btnClearDrug_Click(object sender, EventArgs e)
        {
            try
            {
                if (LstMedication.SelectedItems.Count > 0)
                {
                    if ((_reportName == "rptMedicationMU") || (_reportName == "rpteRx") || (_reportName == "rptPatientPrescriptions"))
                    {
                        DataTable dt = (DataTable)LstMedication.DataSource;
                        if (dt != null)
                        {
                            dt.Rows.RemoveAt(LstMedication.SelectedIndex);
                            LstMedication.DataSource = dt;
                        }
                    }
                    else
                    {
                        LstMedication.Items.Remove(LstMedication.SelectedItems[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClearAllDrg_Click(object sender, EventArgs e)
        {
            try
            {
               // LstMedication.Items.Clear();
                LstMedication.DataSource = null;
                LstMedication.Items.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBrowseDiag_Click_1(object sender, EventArgs e)
        {
            if (bHistory == true)
            {
                if (LstMedication.Items.Count == 0)
                {
                    MessageBox.Show("Please select Category first", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LstMedication.Focus();
                    return;
                }
            }
            strLst = "diag";
            LoadUserGrid();
            pnlcustomTask.BringToFront();
        }

        private void btnClearDiag_Click_1(object sender, EventArgs e)
        {
            while (LstDiagnosis.SelectedItems.Count > 0)
            {
                LstDiagnosis.Items.Remove(LstDiagnosis.SelectedItems[0]);
            }
        }

        //SANJOG-20100726 EXPORT FUNCTIONALITY
        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                Warning[] warnings = null;
                string[] streamids = null;
                string mimeType = null;
                string encoding = null;
                string extension = null;
                byte[] bytes = null;
                string Format = null;
                int intindex;
                intindex = saveFileDialog1.FilterIndex;

                if (intindex == 1)
                    Format = "XML";
                else if (intindex == 2)
                    Format = "CSV";
                else if (intindex == 3)
                    Format = "Image";
                else if (intindex == 4)
                    Format = "PDF";
                else if (intindex == 5)
                    Format = "MHTML";
                else if (intindex == 6)
                    Format = "Excel";

                bytes = this.SSRSViewer.ServerReport.Render(Format, null, out mimeType, out encoding, out extension, out streamids, out warnings);

                FileStream fs = new FileStream(saveFileDialog1.FileName, FileMode.Create);
                fs.Write(bytes, 0, bytes.Length);
                fs.Close();
                fs.Dispose();
                fs = null;
                MessageBox.Show("File Saved Successfully ....!", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Reports, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.ExportReport, RptName + " Usage Report Exported..", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBrowseDMSetup_Click(object sender, EventArgs e)
        {
            strLst = "PatDMCriteria";
            pnlcustomTask.Height = 400;
            pnlcustomTask.Width = 500;
            pnlcustomTask.BringToFront();
            LoadUserGrid();
            SetCheckValues(lstdmsetup);

        }
        private void cmbPatCondition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPatCondition.Text == "Between")
            {
                lblPatFrom.Text = "From";
                lblPatTo.Visible = true;
                txtPatTo.Visible = true;
                lblToBlank.Visible = true;


            }
            else
            {
                lblPatFrom.Text = "Value";
                lblPatTo.Visible = false;
                txtPatTo.Visible = false;
                lblToBlank.Visible = false;
            }
        }


        // chetan added for patient reminder report
        private void FillPatients()
        {

            if (dtpicFrom.Checked == true)
            {
                if (dtpicFrom.Value.Date > dtpicTo.Value.Date)
                {
                    MessageBox.Show("Invalid Visit Date Criteria. 'From Date' should be less than or equal to 'To Date'.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }

 

         

            if (cmbAge.Text.Trim() == "" || cmbAge.Text == "For All")
            {
            }
            // FOR PARTICULAR AGE
            else if (cmbAge.Text == "For Age")
            {
                if (cmbAgeFrom.Text.Trim() == "")
                {
                    MessageBox.Show("Please select the Age ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbAgeFrom.Focus();
                    return;
                }
            }

            // FOR LESS THAN GIVEN AGE
            else if (cmbAge.Text == "Less Than")
            {
                if (cmbAgeFrom.Text.Trim() == "")
                {
                    MessageBox.Show("Please select the Age ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbAgeFrom.Focus();
                    return;
                }
            }

            // FOR GREATER THAN GIVEN AGE
            else if (cmbAge.Text == "Greater Than")
            {
                if (cmbAgeFrom.Text.Trim() == "")
                {
                    MessageBox.Show("Please select the Age ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbAgeFrom.Focus();
                    return;
                }
            }
            else if (cmbAge.Text == "Between")
            {
                if (cmbAgeFrom.Text.Trim() == "")
                {
                    MessageBox.Show("Please select From Age ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbAgeFrom.Focus();
                    return;
                }
                if (cmbAgeTo.Text.Trim() == "")
                {
                    MessageBox.Show("Please select To Age ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbAgeTo.Focus();
                    return;
                }
                if (Convert.ToInt32(cmbAgeFrom.Text) > Convert.ToInt32(cmbAgeTo.Text))
                {
                    MessageBox.Show(" From-age should be less than To-Age ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbAgeFrom.Focus();
                    return;
                }
            }

            if (_reportName == "rptPatientListDM")
            {

                if (cmbProvider.SelectedIndex == 0)
                {
                    if (MessageBox.Show("Provider with 'ALL' option may take long time to generate the report. Do you want to continue?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }
                }

            }

            SSRSViewer.Visible = false;
            C1Patients.Visible = true;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBPara = null;
            DataTable dtPatients = null;
            //SSRSArgs _e = new SSRSArgs();
            try
            {
                _ProblemLists = "";
                _Medications = "";

                for (int lenprb = 0; lenprb < lstDMProblemList.Items.Count; lenprb++)
                    _ProblemLists += lstDMProblemList.Items[lenprb].ToString() + "|";
                if (_ProblemLists.Length > 1)
                    _ProblemLists = _ProblemLists.Substring(0, _ProblemLists.Length - 1);


                //for (int lenmed = 0; lenmed < lstmeddmsetup.Items.Count; lenmed++)
                //    _Medications += lstmeddmsetup.Items[lenmed].ToString() + "|";
                //if (_Medications.Length > 1)
                //    _Medications = _Medications.Substring(0, _Medications.Length - 1);


                for (int lenmed = 0; lenmed < lstmeddmsetup.Items.Count; lenmed++)
                {
                    if (lstmeddmsetup.Items[lenmed].ToString().IndexOf(":") > -1)
                    {
                        _Medications += lstmeddmsetup.Items[lenmed].ToString().Substring(0, lstmeddmsetup.Items[lenmed].ToString().IndexOf(":") - 1) + "|";
                    }
                    else
                    {
                        _Medications += lstmeddmsetup.Items[lenmed].ToString() + "|";
                    }
                }

                if (_Medications.Length > 2)
                    _Medications = _Medications.Substring(0, _Medications.Length - 1);



                C1Patients.Rows.Fixed = 1;

                Int32 AgeFrom = 1;
                Int32 AgeTo = 1;

                if (cmbAgeFrom.Text != "")
                    AgeFrom = Convert.ToInt32(cmbAgeFrom.Text);
                if (cmbAgeTo.Text != "")
                    AgeTo = Convert.ToInt32(cmbAgeTo.Text);



                string strapptfrdate = "";
                string strappttodate = "";

                string strdtpicFrom = "";
                string strdtpicTo = "";

                if (dtpicFrom.Checked)
                {
                    strdtpicFrom = dtpicFrom.Text;
                    strdtpicTo = dtpicTo.Text;
                }
                else
                {
                    strdtpicFrom = " ";
                    strdtpicTo = " ";
                }


                if (ChkAppt.Checked)
                {
                    strapptfrdate = dtFromAppt.Text;
                    strappttodate = dtToAppt.Text;
                }
                else
                {
                    strapptfrdate = " ";
                    strappttodate = " ";
                }

                //_e.sCriteriaID = _DMCriteriaIDs.Replace("|", ",");
                //GenerateReport_Click(null, null, _e);




                _DMCriteriaIDs = "";

                for (int len = 0; len < lstdmsetup.Items.Count; len++)
                {
                    if (lstdmsetup.Items[len].ToString().Trim() != "")
                    {
                        if (chkDueDate.Checked == true)
                        {
                            sPatientIDs = GetDMCriteriaPatientID(lstdmsetup.Items[len].ToString().Trim(), true, dtFromDueDate.Value, dtToDueDate.Value);
                        }
                        else
                        {
                            sPatientIDs = GetDMCriteriaPatientID(lstdmsetup.Items[len].ToString().Trim(), false, dtFromDueDate.Value, dtToDueDate.Value);
                        }
                        
                        _DMCriteriaIDs += lstdmsetup.Items[len].ToString() + ",";
                    }
                }
                if (_DMCriteriaIDs.Length >= 2)
                {
                    _DMCriteriaIDs = _DMCriteriaIDs.Substring(0, _DMCriteriaIDs.Length - 1);
                }


                if (_DMCriteriaIDs == "")
                {
                    sPatientIDs = "";
                }
                //if (_DMCriteriaIDs != "")
                //{
                //    sPatientIDs = GetDMCriteriaPatientID(_DMCriteriaIDs.Replace("|", ","));
                //}
                //else
                //{
                //    sPatientIDs = "";
                //}


                //rpt_GetPatientDetailsonCriteriaMedPrbAppt
                sPatientIDs = sPatientIDs.Replace("|", ",");
                if (sPatientIDs.ToString().Trim() == "")
                    sPatientIDs = "-1";

                if (lstdmsetup.Items.Count == 0)
                    sPatientIDs = " ";

                oDBPara = new gloDatabaseLayer.DBParameters();
                if (Convert.ToInt64(cmbProvider.SelectedIndex) == 0)
                {
                    ProviderID = 0;
                }
                else
                {
                    ProviderID = Convert.ToInt64(cmbProvider.SelectedValue);

                }
                oDBPara.Add("@nProviderID", ProviderID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBPara.Add("@AgeType", cmbAge.SelectedIndex, ParameterDirection.Input, SqlDbType.Int);
                oDBPara.Add("@nAgeFrom", AgeFrom, ParameterDirection.Input, SqlDbType.BigInt);
                oDBPara.Add("@nAgeTo", AgeTo, ParameterDirection.Input, SqlDbType.BigInt);
                oDBPara.Add("@frDate", strdtpicFrom, ParameterDirection.Input, SqlDbType.VarChar);
                oDBPara.Add("@ToDate", strdtpicTo, ParameterDirection.Input, SqlDbType.VarChar);
                oDBPara.Add("@apptfrdate", strapptfrdate, ParameterDirection.Input, SqlDbType.VarChar);
                oDBPara.Add("@appttodate", strappttodate, ParameterDirection.Input, SqlDbType.VarChar);
                oDBPara.Add("@sMedication", _Medications, ParameterDirection.Input, SqlDbType.VarChar);
                oDBPara.Add("@sProblemList", _ProblemLists, ParameterDirection.Input, SqlDbType.VarChar);
                oDBPara.Add("@sPatientId", sPatientIDs, ParameterDirection.Input, SqlDbType.VarChar);
                if (ChkAppt.Checked == true)
                {
                    oDBPara.Add("@apptflg", cmbapptst.SelectedValue.ToString(), ParameterDirection.Input, SqlDbType.VarChar);

                }
                oDB.Connect(false);
                //  oDB.Retrive("rpt_GetPatientDetailsonCriteriaMedPrbAppt", oDBPara, out dtPatients);

                oDB.Retrive("rpt_GetPatientDetailsonCriteriaMedPrbAppt", oDBPara, out dtPatients);
                oDB.Disconnect();
                if (dtPatients != null)
                {
                    if ((cmbapptst.Text.Trim() != "All") && (ChkAppt.Checked == true))
                    {
                        DataColumn dc = new DataColumn();
                        dc.ColumnName = "Appointment Status";
                        dtPatients.Columns.Add(dc);
                        for (int len = 0; len < dtPatients.Rows.Count; len++)
                            dtPatients.Rows[len]["Appointment Status"] = cmbapptst.Text.Trim();
                        C1Patients.DataSource = dtPatients;
                    }
                    else
                        C1Patients.DataSource = dtPatients;


                    //06-May-23: Aniket: Fixing Bug #50162: 
                    C1Patients.Cols.Fixed = 0;

                    C1Patients.Cols[COL_Select].Visible = true;
                    C1Patients.Cols[COL_PatientCode].Visible = true; // Patient Code //
                    C1Patients.Cols[COL_PatientCode].AllowEditing = false;

                    C1Patients.Cols[COL_PatientID].Visible = false; // PatientID //

                    C1Patients.Cols[COL_PatientFName].Visible = true; // Patient First Name //
                    C1Patients.Cols[COL_PatientFName].AllowEditing = false;

                    C1Patients.Cols[COL_PatientMName].Visible = true; // Patient Middle Name //
                    C1Patients.Cols[COL_PatientMName].AllowEditing = false;

                    C1Patients.Cols[COL_PatientLName].Visible = true; // Patient Last Name //
                    C1Patients.Cols[COL_PatientLName].AllowEditing = false;

                    C1Patients.Cols[COL_DOB].Visible = true; // DOB //
                    C1Patients.Cols[COL_DOB].AllowEditing = false;


                    C1Patients.Cols[COL_age].Visible = true; // Age //
                    C1Patients.Cols[COL_age].AllowEditing = false;

                    C1Patients.Cols[COL_Gender].Visible = true; // Gender //
                    C1Patients.Cols[COL_Gender].AllowEditing = false;

                    C1Patients.Cols[COL_ProviderID].Visible = false; // ProviderID //
                    C1Patients.Cols[COL_PRoviderName].Visible = false; // Provider Name //
                    C1Patients.Cols[COL_sCommunicationPreference].Visible = true; // Provider Name //
                    C1Patients.Cols[COL_sCommunicationPreference].AllowEditing = false;

                    if ((cmbapptst.Text.Trim() != "All") && (ChkAppt.Checked == true))
                    {
                        C1Patients.Cols[COL_ApptStatus].Visible = true; //Appt Status //
                    }


                    C1Patients.Cols[COL_Select].DataType = typeof(bool);

                    C1Patients.SetData(0, COL_Select, "Select");
                    C1Patients.SetData(0, COL_PatientCode, "Patient Code");
                    C1Patients.SetData(0, COL_PatientFName, "Patient First Name");
                    C1Patients.SetData(0, COL_PatientMName, "Middle Name");
                    C1Patients.SetData(0, COL_PatientLName, "Patient Last Name");
                    C1Patients.SetData(0, COL_DOB, "DOB");
                    C1Patients.SetData(0, COL_age, "Age");
                    C1Patients.SetData(0, COL_Gender, "Gender");
                    C1Patients.SetData(0, COL_sCommunicationPreference, "Communication Preference");


                    int _width = C1Patients.Width;
                    C1Patients.Cols[COL_PatientCode].Width = (int)(_width * 0.1);
                    C1Patients.Cols[COL_PatientFName].Width = (int)(_width * 0.12);
                    C1Patients.Cols[COL_PatientMName].Width = (int)(_width * 0.08);
                    C1Patients.Cols[COL_PatientLName].Width = (int)(_width * 0.12);
                    C1Patients.Cols[COL_DOB].Width = (int)(_width * 0.1);
                    C1Patients.Cols[COL_age].Width = (int)(_width * 0.1);
                    C1Patients.Cols[COL_Gender].Width = (int)(_width * 0.1);
                    // C1Patients.Cols[COL_sCommunicationPreference].Width = (int)(_width * 0.2);

                    if ((cmbapptst.Text.Trim() != "All") && (ChkAppt.Checked == true))
                    {
                        C1Patients.SetData(0, COL_ApptStatus, "Appointment Status");
                        C1Patients.Cols[COL_ApptStatus].Width = (int)(_width * 0.2);
                        C1Patients.Cols[COL_sCommunicationPreference].Width = (int)(_width * 0.1);

                    }
                    else
                        C1Patients.Cols[COL_sCommunicationPreference].Width = (int)(_width * 0.3);

                    C1Patients.Visible = true;

                    tsb_ReminderLetters.Visible = true;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
                if (oDBPara != null) { oDBPara.Dispose(); oDBPara = null; }

            }


        }
        private void btnBrowsePatientDrug_Click(object sender, EventArgs e)
        {
            strLst = "PatientMedication";
            //lstPatMedication.Items.Clear();
            LoadUserGrid();
            pnlcustomTask.BringToFront();
            pnlcustomTask.Height = 400;
            pnlcustomTask.Width = 500;
            SetCheckValues(lstPatMedication);
        }

        private void btnClearPatientDrug_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstPatMedication.Items.Count > 0)
                {
                    if (lstPatMedication.SelectedItem != null)
                    {
                        lstPatMedication.Items.Remove(lstPatMedication.SelectedItems[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClearAllPatientDrug_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstPatMedication.Items.Count > 0)
                {
                   // lstPatMedication.Items.Clear();
                    lstPatMedication.DataSource = null;
                    lstPatMedication.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClearPatientprb_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstProblemList.Items.Count > 0)
                {
                    if (lstProblemList.SelectedItem != null)
                    {
                        if (_DictionaryICD9.ContainsKey(lstProblemList.SelectedItem.ToString()))
                        {
                            _DictionaryICD9.Remove(lstProblemList.SelectedItem.ToString());
                        }

                        if (_DictionarySnomedCodeCT.ContainsKey(lstProblemList.SelectedItem.ToString()))
                        {
                            _DictionarySnomedCodeCT.Remove(lstProblemList.SelectedItem.ToString());
                        }
                        lstProblemList.Items.Remove(lstProblemList.SelectedItems[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClearAllPatientprb_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstProblemList.Items.Count > 0)
                {
                 //   lstProblemList.Items.Clear();
                    lstProblemList.DataSource = null;
                    lstProblemList.Items.Clear();
                    _DictionaryICD9.Clear();
                    _DictionarySnomedCodeCT.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnClearPatientLab_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstLabResult.Items.Count > 0)
                {
                    if (lstLabResult.SelectedItem != null)
                    {
                        lstLabResult.Items.Remove(lstLabResult.SelectedItems[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClearAllPatientLab_Click(object sender, EventArgs e)
        {
            LabTestResult = string.Empty;
            try
            {
                if (lstLabResult.Items.Count > 0)
                {
                    //lstLabResult.Items.Clear();
                    lstLabResult.DataSource = null;
                    lstLabResult.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public DataTable GetLabTestResults()
        {
            DataBaseLayer oDB = new DataBaseLayer();
            string _strSQL = "";
            try
            {
                //' _strSQL = "select Distinct sICD9Code ,(isNull(sICD9Code,'') + ' : ' + isNull(sICD9Description,'')) as sICD9Display, (isNull(sICD9Code,'') + ' ' + isNull(sICD9Description,'')) as sICD9Values from ExamICD9CPT Where sICD9Code <>'' AND sICD9Description<>''"
                //'_strSQL = "select Distinct (isNull(sICD9Code,'') + ' : ' + isNull(sICD9Description,'')) as sICD9Display from ExamICD9CPT Where sICD9Code <>'' AND sICD9Description<>''"
                _strSQL = "select distinct labtrd_ResultName As ResultName from Lab_Test_ResultDtl";
                DataTable oResult = oDB.GetDataTable_Query(_strSQL);
                if (oResult != null)
                {
                    return oResult;
                }
                else
                {
                    return null;

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return null;
            }
            finally
            {
                oDB.Dispose();
                oDB = null;
            }
        }


        private void btnAddResultCond_Click(object sender, EventArgs e)
        {
            //sum([HDL-CHOLESTEROL])=100 and sum([LDL-CHOLESTEROL]
            string Operator = string.Empty;
            string DisplayValue = string.Empty;
            if (cmbLabResult.Text.ToString() == "")
            {
                MessageBox.Show("Enter lab test results.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (cmbPatCondition.SelectedItem.ToString() == "Less Than")
            {
                if (txtPatFrom.Text.ToString() != "")
                    Operator = " < " + txtPatFrom.Text.ToString();
                else
                    MessageBox.Show("Enter value.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (cmbPatCondition.SelectedItem.ToString() == "Greater Than")
            {
                if (txtPatFrom.Text.ToString() != "")
                    Operator = " > " + txtPatFrom.Text.ToString();
                else
                    MessageBox.Show("Enter value.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (cmbPatCondition.SelectedItem.ToString() == "Equal To")
            {
                if (txtPatFrom.Text.ToString() != "")
                    Operator = " = " + txtPatFrom.Text.ToString();
                else
                    MessageBox.Show("Enter value.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (txtPatFrom.Text.ToString() != "" && txtPatTo.Text.ToString() != "")
                    Operator = " >= " + txtPatFrom.Text.ToString() + " <= " + txtPatTo.Text.ToString();
                else
                    MessageBox.Show("Enter value.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }



            if (Operator != "" && cmbLabResult.Text.ToString() != "")
            {
                //if (lstLabResult.Items.Count == 0)
                //{
                //    LabTestResult = "sum([" + cmbLabResult.Text.ToString() + "])" + Operator;
                //    LabTestResult1 = "sum([" + cmbLabResult.Text.ToString() + "])" + Operator;
                //}
                //else
                //{
                //    LabTestResult = LabTestResult + " " +cmbThiPat.SelectedItem.ToString() +" " + "sum([" + cmbLabResult.Text.ToString() + "])" + Operator;
                //    LabTestResult1 = LabTestResult1 + " " + cmbThiPat.SelectedItem.ToString() + " " + "sum([" + cmbLabResult.Text.ToString() + "])" + Operator;
                //}

                DisplayValue = cmbLabResult.Text.ToString() + "  " + Operator + "   " + cmbThiPat.SelectedItem.ToString();

                if (lstLabResult.Items.Count == 0)
                {
                    lstLabResult.Items.Add(DisplayValue.ToString());
                    pnlLabTestResult.Visible = false;


                    if ((!chkPrblmLstOthrLabResult.Checked))
                    {
                        chkPrblmLstOthrLabResult.Checked = true;
                    }
                }
                else
                {
                    bool foundItem = false;
                    for (int i = 0; i < lstLabResult.Items.Count; i++)
                    {
                        string strItem = lstLabResult.Items[i].ToString();
                        if (strItem.Contains(cmbLabResult.Text.ToString()))
                        {
                            foundItem = true;
                            break;
                            // lstLabResult.Items.Add(DisplayValue.ToString());
                            //pnlLabTestResult.Visible = false;
                        }
                    }
                    if (foundItem == false)
                    {
                        lstLabResult.Items.Add(DisplayValue.ToString());
                        pnlLabTestResult.Visible = false;
                        if (pnlIncludeDemographics.Visible && (!chkPrblmLstOthrLabResult.Checked))
                        {
                            chkPrblmLstOthrLabResult.Checked = true;
                        }
                    }
                }

                //if (!lstLabResult.Items.Contains(cmbLabResult.Text.ToString()))
                //{
                //    lstLabResult.Items.Add(DisplayValue.ToString());
                //    pnlLabTestResult.Visible = false;
                //}
            }
            //int len = LabTestResult.Length  
            //LabTestResult = LabTestResult.Substring(0, LabTestResult.Length - 3);
        }




        private void btnBrowsePatientPrb_Click(object sender, EventArgs e)
        {

            //frmSelectProblem frm = new frmSelectProblem(GetGLOSMCONNECTIONSTR());
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.ShowInTaskbar = false;
            //frm.ShowDialog(this);
            //string strSelectedProblem = frm.strProblem.ToString();
            //if (strSelectedProblem != "" && !lstProblemList.Items.Contains(strSelectedProblem))
            //    lstProblemList.Items.Add(strSelectedProblem);


            if (gstrProblemtype.Trim() != "")
            {

                if (ChkSnomedSetting() == true)
                {

                    FrmSelectProblem frm = new FrmSelectProblem("Select Problem", StrSnoConnstring, _conn);

                   // frm.StartPosition = FormStartPosition.CenterScreen;
                   //frm.ShowInTaskbar = false;

                    frm.ShowDialog(this);

                    string strSelectedProblem = frm.strProblem.ToString();
                    if (strSelectedProblem != "" && !lstDMProblemList.Items.Contains(strSelectedProblem))
                        lstProblemList.Items.Add(strSelectedProblem);
                    frm.Dispose();
                    frm = null;
                }
                else
                {
                    strLst = "patientproblem";

                    pnlcustomTask.Height = 400;
                    pnlcustomTask.Width = 500;
                
                    LoadUserGrid();
                    SetCheckValues(lstProblemList);
                    pnlcustomTask.BringToFront();
                }

            }

            else
            {
                strLst = "patientproblem";
                pnlcustomTask.Height = 400;
                pnlcustomTask.Width = 500;
                LoadUserGrid();
                SetCheckValues(lstProblemList);
                pnlcustomTask.BringToFront();
            }




        }

        public string GetGLOSMCONNECTIONSTR()
        {
            DataBaseLayer oDB = new DataBaseLayer();
           
            string _strSQL = "";
            string _DbSnoMed = string.Empty;
            try
            {
                //' _strSQL = "select Distinct sICD9Code ,(isNull(sICD9Code,'') + ' : ' + isNull(sICD9Description,'')) as sICD9Display, (isNull(sICD9Code,'') + ' ' + isNull(sICD9Description,'')) as sICD9Values from ExamICD9CPT Where sICD9Code <>'' AND sICD9Description<>''"
                //'_strSQL = "select Distinct (isNull(sICD9Code,'') + ' : ' + isNull(sICD9Description,'')) as sICD9Display from ExamICD9CPT Where sICD9Code <>'' AND sICD9Description<>''"
                _strSQL = "select sSettingsValue from  settings where sSettingsName ='GLOSMCONNECTIONSTR'";
                DataTable Dt = oDB.GetDataTable_Query(_strSQL);
                if (Dt != null)
                {
                    if (Dt.Rows.Count > 0)
                        _DbSnoMed = Dt.Rows[0][0].ToString();
                    Dt.Dispose();
                    Dt = null;
                }
                else
                {
                    _DbSnoMed = "";

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                _DbSnoMed = "";
            }
            finally
            {
                oDB.Dispose();
                oDB = null;
            }
            return _DbSnoMed;

        }

        private void txtPatFrom_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtPatTo_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        private void btnBrowseDMMedication_Click(object sender, EventArgs e)
        {
            strLst = "PatientDMMedication";
            //lstPatMedication.Items.Clear();
            pnlcustomTask.Height = 400;
            pnlcustomTask.Width = 500;
            LoadUserGrid();
            SetCheckValues(lstmeddmsetup);
            pnlcustomTask.BringToFront();

        }
        private void btnBrowseDMPatientPrb_Click(object sender, EventArgs e)
        {
            //frmSelectProblem frm = new frmSelectProblem(StrSnoConnstring);
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.ShowInTaskbar = false;
            //frm.ShowDialog(this);

            //string strSelectedProblem = frm.strProblem.ToString();
            //if (strSelectedProblem != "" && !lstDMProblemList.Items.Contains(strSelectedProblem))
            //    lstDMProblemList.Items.Add(strSelectedProblem);
            if (gstrProblemtype.Trim() != "")
            {



                if (ChkSnomedSetting() == true)
                {

                    FrmSelectProblem frm = new FrmSelectProblem("Select Problem", StrSnoConnstring, _conn);

                   //frm.StartPosition = FormStartPosition.CenterScreen;
                    //frm.ShowInTaskbar = false;
                    frm.ShowDialog(this);

                    string strSelectedProblem = frm.strProblem.ToString();
                    if (strSelectedProblem != "" && !lstDMProblemList.Items.Contains(strSelectedProblem))
                        lstDMProblemList.Items.Add(strSelectedProblem);
                    frm.Dispose();
                    frm = null;
                }
                else
                {
                    strLst = "PatientDMProblem";

                    pnlcustomTask.Height = 400;
                    pnlcustomTask.Width = 500;
                    LoadUserGrid();
                    SetCheckValues(lstDMProblemList);
                    pnlcustomTask.BringToFront();
                }
            }
            else
            {
                strLst = "PatientDMProblem";
                pnlcustomTask.Height = 400;
                pnlcustomTask.Width = 500;
                LoadUserGrid();
                SetCheckValues(lstDMProblemList);
                pnlcustomTask.BringToFront();
            }



        }

        private void btnSearchImmunization_Click(object sender, EventArgs e)
        {
            strLst = "PatientImmunization";
            //lstPatMedication.Items.Clear();
            LoadUserGrid();
            pnlcustomTask.BringToFront();
            SetCheckValues(lstImmunization);
            pnlcustomTask.Height = 400;
            pnlcustomTask.Width = 500;
        }

        private void btnBrowseLabResults_Click(object sender, EventArgs e)
        {
            strLst = "PatientLabResults";
            //lstPatMedication.Items.Clear();
            LoadUserGrid();
            pnlcustomTask.BringToFront();
            pnlcustomTask.Height = 400;
            pnlcustomTask.Width = 500;
        }

        private void btnClearImmunization_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstImmunization.Items.Count > 0)
                {
                    if (lstImmunization.SelectedItem != null)
                    {
                        lstImmunization.Items.Remove(lstImmunization.SelectedItems[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClearAllImmunization_Click(object sender, EventArgs e)
        {

            try
            {
                //lstImmunization.Items.Clear();
                lstImmunization.DataSource = null;
                lstImmunization.Items.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBrowseLabTestResult_Click(object sender, EventArgs e)
        {
            pnlLabTestResult.Visible = true;
            pnlLabTestResult.BringToFront();
            pnlLabTestResult.Location = new System.Drawing.Point(699, 260);
        }

        private void btlCloseLabResult_Click(object sender, EventArgs e)
        {
            pnlLabTestResult.Visible = false;
        }

        private void cmbAgeFrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "([0-9\b])"))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void cmbAgeTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "([0-9\b])"))
                e.Handled = false;
            else
                e.Handled = true;
        }


        private void ChkAppt_CheckedChanged(object sender, EventArgs e)
        {
            dtFromAppt.Enabled = ChkAppt.Checked;
            dtToAppt.Enabled = ChkAppt.Checked;
            cmbapptst.Enabled = ChkAppt.Checked;

            dtToAppt.MinDate = dtFromAppt.Value;
        }

        private void btnClearDMSetup_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstdmsetup.Items.Count > 0)
                {
                    if (lstdmsetup.SelectedItem != null)
                    {
                        lstdmsetup.Items.Remove(lstdmsetup.SelectedItems[0]);
                    }
                }

                if (lstdmsetup.Items.Count == 0)
                {
                    EnableDisableDueDate(false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClearAllDMSetup_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstdmsetup.Items.Count > 0)
                {
                   // lstdmsetup.Items.Clear();
                    lstdmsetup.DataSource = null;
                    lstdmsetup.Items.Clear();
                    //Resolving Bug #90095: gloEMR: Patient Reminder Report- Application should not activate Due date checkbox as there are no items present in Disease management
                    EnableDisableDueDate(false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnClearDMMedication_Click(object sender, EventArgs e)
        {

            try
            {
                if (lstmeddmsetup.Items.Count > 0)
                {
                    if (lstmeddmsetup.SelectedItem != null)
                    {
                        lstmeddmsetup.Items.Remove(lstmeddmsetup.SelectedItems[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnClearAllDMMedication_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstmeddmsetup.Items.Count > 0)
                {
                 //   lstmeddmsetup.Items.Clear();
                    lstmeddmsetup.DataSource = null;
                    lstmeddmsetup.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClearDMPatientprb_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstDMProblemList.Items.Count > 0)
                {
                    if (lstDMProblemList.SelectedItem != null)
                    {
                        lstDMProblemList.Items.Remove(lstDMProblemList.SelectedItems[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClearAllDMPatientprb_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstDMProblemList.Items.Count > 0)
                {
                   // lstDMProblemList.Items.Clear();
                    lstDMProblemList.DataSource = null;
                    lstDMProblemList.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPatTo_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            #region " Allow decimal amount only "

            bool hasDecimal = false;
            if (e.KeyChar == (char)46)
            {
                if (txtPatTo.Text.Contains(Convert.ToString(e.KeyChar)))
                { hasDecimal = true; }
                else
                { hasDecimal = false; }
            }

            if (hasDecimal == true)
            {
                if (!Char.IsDigit(e.KeyChar) & e.KeyChar != (char)8)
                { e.Handled = true; }
            }
            else
            {
                if (!Char.IsDigit(e.KeyChar) & e.KeyChar != (char)46 & e.KeyChar != (char)8)
                { e.Handled = true; }
            }

            #endregion

        }

        private void txtPatFrom_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            #region " Allow decimal amount only "

            bool hasDecimal = false;
            if (e.KeyChar == (char)46)
            {
                if (txtPatFrom.Text.Contains(Convert.ToString(e.KeyChar)))
                { hasDecimal = true; }
                else
                { hasDecimal = false; }
            }

            if (hasDecimal == true)
            {
                if (!Char.IsDigit(e.KeyChar) & e.KeyChar != (char)8)
                { e.Handled = true; }
            }
            else
            {
                if (!Char.IsDigit(e.KeyChar) & e.KeyChar != (char)46 & e.KeyChar != (char)8)
                { e.Handled = true; }
            }

            #endregion
        }

        private void dtFromAppt_ValueChanged(object sender, EventArgs e)
        {
            dtToAppt.MinDate = dtFromAppt.Value;
        }

        private void setDemoFilterData()
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);

            ////string itm="";
            try
            {
                if (oDB != null)
                {



                    //   string _sqlQuery = "SELECT  nCategoryid id,sDescription as name,UPPER(sCategoryType)  as sCategoryType  FROM category_mst WHERE UPPER(sCategoryType) IN ('ETHNICITY','LANGUAGE','COMMUNICATION PREFERENCE','RACE')  AND bIsBlocked = '" + false + "' ";

                    oDB.Connect(false);
                    oDB.Retrive("getPatientlistDemoDetail", out dsDemographic);
                    oDB.Disconnect();
                    //         if (dsDemo!=null)
                    //         {
                    //         dtDemo = dsDemo.Tables[0];
                    //         if (dtDemo != null)
                    //        {
                    //            DataRow dr = dtDemo.NewRow(); //Adding the new row int the Datatable
                    //            dr["id"] = 0;
                    //            dr["name"] = "";
                    //            dtDemo.Rows.InsertAt(dr, 0); //Need to be inserted in the speicfied location
                    //            dtDemo.AcceptChanges();  //After adding the row in the datatable accept the changes

                    //            cmbethnicity.DataSource = dtDemo;  //Binding the Datasource to the 

                    //            cmbethnicity.DisplayMember = "name";
                    //            //cmbCommPref.SelectedIndex  = cmbPALang.FindString("English") ;
                    //        }

                    //         dtDemo = dsDemo.Tables[1];
                    //        if (dtDemo != null)
                    //        {
                    //            DataRow dr = dtDemo.NewRow(); //Adding the new row int the Datatable
                    //            dr["id"] = 0;
                    //            dr["name"] = "";
                    //            dtDemo.Rows.InsertAt(dr, 0); //Need to be inserted in the speicfied location
                    //            dtDemo.AcceptChanges();  //After adding the row in the datatable accept the changes

                    //            cmblanguage.DataSource = dtDemo;  //Binding the Datasource to the 

                    //            cmblanguage.DisplayMember = "name";
                    //            //cmbCommPref.SelectedIndex  = cmbPALang.FindString("English") ;
                    //        }


                    //        dtDemo = dsDemo.Tables[2];
                    //        if (dtDemo != null)
                    //        {
                    //            DataRow dr = dtDemo.NewRow(); //Adding the new row int the Datatable
                    //            dr["id"] = 0;
                    //            dr["name"] = "";
                    //            dtDemo.Rows.InsertAt(dr, 0); //Need to be inserted in the speicfied location
                    //            dtDemo.AcceptChanges();  //After adding the row in the datatable accept the changes

                    //            cmbComPre.DataSource = dtDemo;  //Binding the Datasource to the 

                    //            cmbComPre.DisplayMember = "name";
                    //            //cmbCommPref.SelectedIndex  = cmbPALang.FindString("English") ;
                    //        }

                    //        dtDemo =dsDemo.Tables[3];
                    //        if (dtDemo != null)
                    //        {
                    //            DataRow dr = dtDemo.NewRow(); //Adding the new row int the Datatable
                    //            dr["id"] = 0;
                    //            dr["name"] = "";
                    //            dtDemo.Rows.InsertAt(dr, 0); //Need to be inserted in the speicfied location
                    //            dtDemo.AcceptChanges();  //After adding the row in the datatable accept the changes

                    //            cmbRace.DataSource = dtDemo;  //Binding the Datasource to the 

                    //            cmbRace.DisplayMember = "name";
                    //            //cmbCommPref.SelectedIndex  = cmbPALang.FindString("English") ;
                    //        }


                    //      }

                    //cmbGender.Items.Add("Male");
                    //cmbGender.Items.Add("Female");
                    //cmbGender.Items.Add("Other");


                }
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);

            }
            finally
            {
                ////cmbCommPref.Text = itm;
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
            }
        }

        private void BtnBrowserAllergy_Click(object sender, EventArgs e)
        {

            strLst = "allergies";
            pnlcustomTask.Height = 400;
            pnlcustomTask.Width = 500;
            LoadUserGrid();
            SetCheckValues(lstAllergy);
            pnlcustomTask.BringToFront();

        }

        private void btnBrowseGender_Click(object sender, EventArgs e)
        {
            strLst = "gender";
            pnlcustomTask.Height = 225;
            pnlcustomTask.Width = 200;

            LoadUserGrid();
            SetCheckValues(cmbGender);
            pnlcustomTask.BringToFront();
        }

        private void btncleargender_Click(object sender, EventArgs e)
        {
            cmbGender.Items.Clear();
        }

        private void btnBrowseRace_Click(object sender, EventArgs e)
        {
            strLst = "race";
            pnlcustomTask.Height = 225;
            pnlcustomTask.Width = 373;
            LoadUserGrid();
            SetCheckValues(cmbRace);
            pnlcustomTask.BringToFront();
        }

        private void btnCleaseRace_Click(object sender, EventArgs e)
        {
            cmbRace.Items.Clear();
        }

        private void btnBrowseEthnicity_Click(object sender, EventArgs e)
        {
            strLst = "ethnicity";
            pnlcustomTask.Height = 225;
            pnlcustomTask.Width = 373;
            LoadUserGrid();
            SetCheckValues(cmbethnicity);
            pnlcustomTask.BringToFront();
        }

        private void btnclearethnicity_Click(object sender, EventArgs e)
        {
            cmbethnicity.Items.Clear();
        }

        private void btnBrowseLanguage_Click(object sender, EventArgs e)
        {
            strLst = "language";
            pnlcustomTask.Height = 225;
            pnlcustomTask.Width = 373;
            LoadUserGrid();
            SetCheckValues(cmblanguage);
            pnlcustomTask.BringToFront();
        }

        private void btnClearLanguage_Click(object sender, EventArgs e)
        {
            cmblanguage.Items.Clear();
        }

        private void btnBrowseCommPref_Click(object sender, EventArgs e)
        {
            strLst = "com prefrrence";
            pnlcustomTask.Height = 225;
            pnlcustomTask.Width = 373;
            LoadUserGrid();
            //07-Oct-14 Aniket: Resolving Bug #74769: gloEMR: Patient List Report-After save operation selected data gets unchecked
            SetCheckValues(cmbComPre);
            pnlcustomTask.BringToFront();
        }

        private void btnClearCommPref_Click(object sender, EventArgs e)
        {
            cmbComPre.Items.Clear();
        }

        private void btnBrowseMedCategory_Click_1(object sender, EventArgs e)
        {
            strLst = "MedicalCategory";
            pnlcustomTask.Height = 225;
            pnlcustomTask.Width = 373;
            LoadUserGrid();
            SetCheckValues(cmbMedicalCategory);
            pnlcustomTask.BringToFront();
        }

        private void btnClearMedCategory_Click(object sender, EventArgs e)
        {
            cmbMedicalCategory.Items.Clear();
        }

        private void BtnClearAllergy_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstAllergy.Items.Count > 0)
                {
                    if (lstAllergy.SelectedItem != null)
                    {
                        lstAllergy.Items.Remove(lstAllergy.SelectedItems[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClearAllAllergy_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstAllergy.Items.Count > 0)
                {
                    //lstAllergy.Items.Clear();
                    lstAllergy.DataSource = null;
                    lstAllergy.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBrowseSnomedCT_Click(object sender, EventArgs e)
        {
            FrmSelectProblem frm = new FrmSelectProblem("Select Problem", StrSnoConnstring, _conn);

           // frm.StartPosition = FormStartPosition.CenterScreen;
           // frm.ShowInTaskbar = false;
            frm.blnIsProblem = true;
            frm.ShowDialog(this);

            if (frm._DialogResult)
            {

                string strSelectedProblem = frm.strProblem.ToString();
                if (strSelectedProblem != "" && !lstProblemList.Items.Contains(frm.strConceptID.ToString() + " - " + strSelectedProblem.ToString()))
                {
                    _DictionarySnomedCodeCT.Add(frm.strConceptID.ToString() + " - " + strSelectedProblem.ToString(), frm.strConceptID.ToString());
                    lstProblemList.Items.Add(frm.strConceptID.ToString() + " - " + strSelectedProblem.ToString());
                }
            }
            frm.Dispose();
            frm = null;
        }

        private void chkPrblLstDemographicAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPrblLstDemographicAll.Checked)
            {
                chkPrblLAge.Checked = true;
                chkPrblLstGender.Checked = true;
                chkPrblLstRace.Checked = true;
                chkPrblLEthnicity.Checked = true;
                chkPrblLDOB.Checked = true;
                chkPrblLLangauge.Checked = true;
                chkPrblLCommPref.Checked = true;
                chkPrblLMedicalCategory.Checked = true;
                chkAddress.Checked = true;
                chksAddressLine1.Checked = true ;
                chksAddressLine2.Checked = true ;
                chksCity.Checked = true ;
                chksState.Checked = true;
                chksZip.Checked = true;


                chkPhone.Checked = true;
                chkEmail.Checked = true;
                chkMobile.Checked = true;
                chkPriInsPlan.Checked = true;
                ChkSecInsPlan.Checked = true;
                chkPCP.Checked = true;
                chkPatNotes.Checked = true;
                chkPtFirstName.Checked = true;
                chkPtMiddleName.Checked = true;
                chkPtLastName.Checked = true;

                chkPrblLAge.Enabled = false;
                chkPrblLstGender.Enabled = false;
                chkPrblLstRace.Enabled = false;
                chkPrblLEthnicity.Enabled = false;
                chkPrblLDOB.Enabled = false;
                chkPrblLLangauge.Enabled = false;
                chkPrblLCommPref.Enabled = false;
                chkPrblLMedicalCategory.Enabled = false;
                chkAddress.Enabled = false;

                chksAddressLine1.Enabled = false;
                chksAddressLine2.Enabled = false;
                chksCity.Enabled = false;
                chksState.Enabled = false;
                chksZip.Enabled = false;


                chkPhone.Enabled = false;
                chkEmail.Enabled = false;
                chkMobile.Enabled = false;
                chkPriInsPlan.Enabled = false;
                ChkSecInsPlan.Enabled = false;
                chkPatNotes.Enabled = false;
                chkPCP.Enabled = false;
                chkPtFirstName.Enabled = false;
                chkPtMiddleName.Enabled = false;
                chkPtLastName.Enabled = false;
            }
            else
            {
                chkPrblLAge.Checked = false;
                chkPrblLstGender.Checked = false;
                chkPrblLstRace.Checked = false;
                chkPrblLEthnicity.Checked = false;
                chkPrblLDOB.Checked = false;
                chkPrblLLangauge.Checked = false;
                chkPrblLCommPref.Checked = false;
                chkPrblLMedicalCategory.Checked = false;
                chkAddress.Checked = false;

                chksAddressLine1.Checked = false;
                chksAddressLine2.Checked = false;
                chksCity.Checked = false;
                chksState.Checked = false;
                chksZip.Checked = false;

                chkPhone.Checked = false;
                chkEmail.Checked = false;
                chkMobile.Checked = false;
                chkPriInsPlan.Checked = false;
                ChkSecInsPlan.Checked = false;
                chkPCP.Checked = false;
                chkPatNotes.Checked = false;
                chkPtFirstName.Checked = false;
                chkPtMiddleName.Checked = false;
                chkPtLastName.Checked = false;

                chkPrblLAge.Enabled = true;
                chkPrblLstGender.Enabled = true;
                chkPrblLstRace.Enabled = true;
                chkPrblLEthnicity.Enabled = true;
                chkPrblLDOB.Enabled = true;
                chkPrblLLangauge.Enabled = true;
                chkPrblLCommPref.Enabled = true;
                chkPrblLMedicalCategory.Enabled = true;
                chkAddress.Enabled = true;

                chksAddressLine1.Enabled = true;
                chksAddressLine2.Enabled = true;
                chksCity.Enabled = true;
                chksState.Enabled = true;
                chksZip.Enabled = true;

                chkPhone.Enabled = true ;
                chkEmail.Enabled = true ;
                chkMobile.Enabled = true;
                chkPriInsPlan.Enabled = true;
                ChkSecInsPlan.Enabled = true;
                chkPCP.Enabled = true;
                chkPatNotes.Enabled = true;
                chkPtFirstName.Enabled = true;
                chkPtMiddleName.Enabled = true;
                chkPtLastName.Enabled = true;
            }
        }

        private void chkPrblmLstOthrElement_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPrblmLstOthrElement.Checked)
            {
                chkPrblmLstOthrProblemList.Checked = true;
                chkPrblmLstOthrMedication.Checked = true;
                chkPrblmLstOthrImmunization.Checked = true;
                chkPrblmLstOthrLabResult.Checked = true;
                chkPrblmLstOthrAllergy.Checked = true;
                chkClaimCPT.Checked = true;
                chkClaimDX.Checked = true;
                chkFacility.Checked = true;


                chkPrblmLstOthrProblemList.Enabled = false;
                chkPrblmLstOthrMedication.Enabled = false;
                chkPrblmLstOthrImmunization.Enabled = false;
                chkPrblmLstOthrLabResult.Enabled = false;
                chkPrblmLstOthrAllergy.Enabled = false;
                chkClaimCPT.Enabled = false;
                chkClaimDX.Enabled = false;
                chkFacility.Enabled = false;
            }
            else
            {
                chkPrblmLstOthrProblemList.Checked = false;
                chkPrblmLstOthrMedication.Checked = false;
                chkPrblmLstOthrImmunization.Checked = false;
                chkPrblmLstOthrLabResult.Checked = false;
                chkPrblmLstOthrAllergy.Checked = false;
                chkClaimCPT.Checked = false;
                chkClaimDX.Checked = false;
                chkFacility.Checked = false;

                chkPrblmLstOthrProblemList.Enabled = true;
                chkPrblmLstOthrMedication.Enabled = true;
                chkPrblmLstOthrImmunization.Enabled = true;
                chkPrblmLstOthrLabResult.Enabled = true;
                chkPrblmLstOthrAllergy.Enabled = true;
                chkClaimCPT.Enabled = true;
                chkClaimDX.Enabled = true;
                chkFacility.Enabled = true;
            }
        }

        private void Tblbtn_DisplayOptn_Click(object sender, EventArgs e)
        {

            if (Tblbtn_DisplayOptn.Tag.ToString()=="Display Option")
            {
                Tblbtn_DisplayOptn.Text = "Hide Display Option";
                Tblbtn_DisplayOptn.Tag = "Hide Display Option";
                Tblbtn_DisplayOptn.ToolTipText = "Hide Display Option";
                pnlIncludeDemographics.Visible = true;
                pnlIncludeDemographics.BringToFront();
                pnlSSRSRpt.BringToFront();
            }
            else
            {
                Tblbtn_DisplayOptn.Text = "Display Option";
                Tblbtn_DisplayOptn.Tag = "Display Option";
                Tblbtn_DisplayOptn.ToolTipText = "Display Option";
                pnlIncludeDemographics.Visible = false;
            }
        }

        private void frmSSRSViewer_FormClosing(object sender, FormClosingEventArgs e)
        {


            if (chkPrblLstDemographicAll.Checked)
            {
                if (strShowingdisplayoption == "")
                {
                    strShowingdisplayoption = "Demographic";
                }
                else
                {
                    strShowingdisplayoption = strShowingdisplayoption + "," + "Demographic";
                }
            }

            if (chkPrblmLstOthrElement.Checked)
            {
                if (strShowingdisplayoption == "")
                {
                    strShowingdisplayoption = "OthrElement";
                }
                else
                {
                    strShowingdisplayoption = strShowingdisplayoption + "," + "OthrElement";
                }
            }
            if (chkDateTime.Checked)
            {
                if (strShowingdisplayoption == "")
                {
                    strShowingdisplayoption = "showdatetime";
                }
                else
                {
                    strShowingdisplayoption = strShowingdisplayoption + "," + "showdatetime";
                }
            }
            if (chkPrblLAge.Checked)
            {
                if (strShowingdisplayoption == "")
                {
                    strShowingdisplayoption = "age";
                }
                else
                {
                    strShowingdisplayoption = strShowingdisplayoption + "," + "age";
                }
            }

            if (chkPtFirstName.Checked)
            {
                if (strShowingdisplayoption == "")
                {
                    strShowingdisplayoption = "PtFirstName";
                }
                else
                {
                    strShowingdisplayoption = strShowingdisplayoption + "," + "PtFirstName";
                }
            }

            if (chkPtMiddleName.Checked)
            {
                if (strShowingdisplayoption == "")
                {
                    strShowingdisplayoption = "PtMiddleName";
                }
                else
                {
                    strShowingdisplayoption = strShowingdisplayoption + "," + "PtMiddleName";
                }
            }

            if (chkPtLastName.Checked)
            {
                if (strShowingdisplayoption == "")
                {
                    strShowingdisplayoption = "PtLastName";
                }
                else
                {
                    strShowingdisplayoption = strShowingdisplayoption + "," + "PtLastName";
                }
            }

            if (chkPriInsPlan.Checked)
            {
                if (strShowingdisplayoption == "")
                {
                    strShowingdisplayoption = "priinsplan";
                }
                else
                {
                    strShowingdisplayoption = strShowingdisplayoption + "," + "priinsplan";
                }
            }

            if (ChkSecInsPlan.Checked)
            {
                if (strShowingdisplayoption == "")
                {
                    strShowingdisplayoption = "secinsplan";
                }
                else
                {
                    strShowingdisplayoption = strShowingdisplayoption + "," + "secinsplan";
                }
            }
            if (chkPCP.Checked)
            {
                if (strShowingdisplayoption == "")
                {
                    strShowingdisplayoption = "PCP";
                }
                else
                {
                    strShowingdisplayoption = strShowingdisplayoption + "," + "PCP";
                }
            }
            if (chkPrblLDOB.Checked)
            {
                if (strShowingdisplayoption == "")
                {
                    strShowingdisplayoption = "dob";
                }
                else
                {
                    strShowingdisplayoption = strShowingdisplayoption + "," + "dob";
                }
            }



            if (chkPrblLstGender.Checked)
            {
                if (strShowingdisplayoption == "")
                {
                    strShowingdisplayoption = "gender";
                }
                else
                {
                    strShowingdisplayoption = strShowingdisplayoption + "," + "gender";
                }
            }

            if (chkPrblLstRace.Checked)
            {
                if (strShowingdisplayoption == "")
                {
                    strShowingdisplayoption = "race";
                }
                else
                {
                    strShowingdisplayoption = strShowingdisplayoption + "," + "race";
                }
            }

            if (chkPrblLEthnicity.Checked)
            {
                if (strShowingdisplayoption == "")
                {
                    strShowingdisplayoption = "ethnicity";
                }
                else
                {
                    strShowingdisplayoption = strShowingdisplayoption + "," + "ethnicity";
                }
            }


            if (chkPrblLLangauge.Checked)
            {
                if (strShowingdisplayoption == "")
                {
                    strShowingdisplayoption = "langauge";
                }
                else
                {
                    strShowingdisplayoption = strShowingdisplayoption + "," + "langauge";
                }
            }

            if (chkPatNotes.Checked)
            {
                if (strShowingdisplayoption == "")
                {
                    strShowingdisplayoption = "patientnotes";
                }
                else
                {
                    strShowingdisplayoption = strShowingdisplayoption + "," + "patientnotes";
                }
            }

            if (chkAddress.Checked)
            {
                if (strShowingdisplayoption == "")
                {
                    strShowingdisplayoption = "sAddressLine3";

                }
                else
                {
                    strShowingdisplayoption = strShowingdisplayoption + "," + "sAddressLine3"; 
                }
            }


            if (chksAddressLine1.Checked)
            {
                if (strShowingdisplayoption == "")
                {
                    strShowingdisplayoption = "sAddressLine1";

                }
                else
                {
                    strShowingdisplayoption = strShowingdisplayoption + "," + "sAddressLine1";
                }
            }


            if (chksAddressLine2.Checked)
            {
                if (strShowingdisplayoption == "")
                {
                    strShowingdisplayoption = "sAddressLine2";

                }
                else
                {
                    strShowingdisplayoption = strShowingdisplayoption + "," + "sAddressLine2";
                }
            }

            if (chksCity.Checked)
            {
                if (strShowingdisplayoption == "")
                {
                    strShowingdisplayoption = "sCity";

                }
                else
                {
                    strShowingdisplayoption = strShowingdisplayoption + "," + "sCity";
                }
            }

            if (chksState.Checked)
            {
                if (strShowingdisplayoption == "")
                {
                    strShowingdisplayoption = "sState";

                }
                else
                {
                    strShowingdisplayoption = strShowingdisplayoption + "," + "sState";
                }
            }

            if (chksZip.Checked)
            {
                if (strShowingdisplayoption == "")
                {
                    strShowingdisplayoption = "sZip";

                }
                else
                {
                    strShowingdisplayoption = strShowingdisplayoption + "," + "sZip";
                }
            }


            if (chkPhone.Checked)
            {
                if (strShowingdisplayoption == "")
                {
                    strShowingdisplayoption = "Phone";

                }
                else
                {
                    strShowingdisplayoption = strShowingdisplayoption + "," + "Phone";
                }
            }

            if (chkMobile.Checked)
            {
                if (strShowingdisplayoption == "")
                {
                    strShowingdisplayoption = "Mobile";

                }
                else
                {
                    strShowingdisplayoption = strShowingdisplayoption + "," + "Mobile";
                }
            }

            if (chkEmail.Checked)
            {
                if (strShowingdisplayoption == "")
                {
                    strShowingdisplayoption = "Email";

                }
                else
                {
                    strShowingdisplayoption = strShowingdisplayoption + "," + "Email";
                }
            }

            if (chkPrblLMedicalCategory.Checked)
            {
                if (strShowingdisplayoption == "")
                {
                    strShowingdisplayoption = "MedicalCategory";
                }
                else
                {
                    strShowingdisplayoption = strShowingdisplayoption + "," + "MedicalCategory";
                }
            }

            if (chkPrblLCommPref.Checked)
            {
                if (strShowingdisplayoption == "")
                {
                    strShowingdisplayoption = "commpreference";
                }
                else
                {
                    strShowingdisplayoption = strShowingdisplayoption + "," + "commpreference";
                }
            }
            if (chkPrblmLstOthrProblemList.Checked)
            {
                if (strShowingdisplayoption == "")
                {
                    strShowingdisplayoption = "problemlist";
                }
                else
                {
                    strShowingdisplayoption = strShowingdisplayoption + "," + "problemlist";
                }
            }

            if (chkPrblmLstOthrMedication.Checked)
            {
                if (strShowingdisplayoption == "")
                {
                    strShowingdisplayoption = "medication";
                }
                else
                {
                    strShowingdisplayoption = strShowingdisplayoption + "," + "medication";
                }
            }

            if (chkPrblmLstOthrImmunization.Checked)
            {
                if (strShowingdisplayoption == "")
                {
                    strShowingdisplayoption = "immunization";
                }
                else
                {
                    strShowingdisplayoption = strShowingdisplayoption + "," + "immunization";
                }
            }


            if (chkPrblmLstOthrLabResult.Checked)
            {
                if (strShowingdisplayoption == "")
                {
                    strShowingdisplayoption = "labresult";
                }
                else
                {
                    strShowingdisplayoption = strShowingdisplayoption + "," + "labresult";
                }
            }

            if (chkPrblmLstOthrAllergy.Checked)
            {
                if (strShowingdisplayoption == "")
                {
                    strShowingdisplayoption = "allergy";
                }
                else
                {
                    strShowingdisplayoption = strShowingdisplayoption + "," + "allergy";
                }
            }

            if (chkClaimCPT.Checked)
            {
                if (strShowingdisplayoption == "")
                {
                    strShowingdisplayoption = "ClaimCPT";
                }
                else
                {
                    strShowingdisplayoption = strShowingdisplayoption + "," + "ClaimCPT";
                }
            }
            if (chkClaimDX.Checked)
            {
                if (strShowingdisplayoption == "")
                {
                    strShowingdisplayoption = "ClaimDX";
                }
                else
                {
                    strShowingdisplayoption = strShowingdisplayoption + "," + "ClaimDX";
                }
            }
            if (chkFacility.Checked)
            {
                if (strShowingdisplayoption == "")
                {
                    strShowingdisplayoption = "Facility";
                }
                else
                {
                    strShowingdisplayoption = strShowingdisplayoption + "," + "Facility";
                }
            }

       //     if (strShowingdisplayoption != "")
           // {
                setuserdisplayoption();
           // }
        }//odbConnectFalse

        public void setuserdisplayoption()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_connectionstring);
            gloDatabaseLayer.DBParameters oParamater = new gloDatabaseLayer.DBParameters();
            try
            {

                if (oDB != null)
                {
                    if (oDB.Connect(false))
                    {
                        oParamater.Add("@sSettingsName", "showdisplayoption", System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                        oParamater.Add("@sSettingsValue", strShowingdisplayoption, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                        oParamater.Add("@nClinicID", _ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                        oParamater.Add("@nUserID", _UserID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                        oParamater.Add("@nUserClinicFlag", 2, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                        oDB.Execute("gsp_InUpSettings", oParamater);

                        //_strSQL = " DELETE  FROM dbo.Settings WHERE sSettingsName='showdisplayoption' AND nUserID="+_UserID;
                        // oDB.Delete_Query(_strSQL);

                        // _strSQL = "INSERT INTO Settings( nSettingsID , sSettingsName , sSettingsValue , nClinicID , nUserID , nUserClinicFlag)" +
                        //         "VALUES  ( dbo.GetUniqueID_V2() ,'showdisplayoption' , '" + strShowingdisplayoption + "' ,1 , " + _UserID + " , 2 )";

                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);

            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
                if (oParamater != null)
                {
                    oParamater.Dispose();
                    oParamater = null;
                }
            }


        }


        public void Getuserdisplayoption()
        {
            DataBaseLayer oDB = new DataBaseLayer();
            DataTable _dt = null;
            string _strSQL = "";
            try
            {
                //convert(varchar(18),nDrugsID)+' : '+
                //_strSQL = "select nDrugsID, sDrugName,isnull(sDosage,'') as sDosage FROM Drugs_MST order by sDrugname "
                _strSQL = " SELECT sSettingsValue as SettingsValue FROM dbo.Settings WHERE sSettingsName='showdisplayoption' AND nUserID=" + _UserID;
                _dt = oDB.GetDataTable_Query(_strSQL);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        chkPrblmLstOthrElement.Checked = false;
                        if (_dt.Rows[0]["SettingsValue"].ToString().Contains("age"))
                        {
                            chkPrblLAge.Checked = true;
                        }
                        if (_dt.Rows[0]["SettingsValue"].ToString().Contains("PtFirstName"))
                        {
                            chkPtFirstName.Checked = true;
                        }
                        if (_dt.Rows[0]["SettingsValue"].ToString().Contains("PtMiddleName"))
                        {
                            chkPtMiddleName.Checked = true;
                        }
                        if (_dt.Rows[0]["SettingsValue"].ToString().Contains("PtLastName"))
                        {
                            chkPtLastName.Checked = true;
                        }
                        if (_dt.Rows[0]["SettingsValue"].ToString().Contains("priinsplan"))
                        {
                            chkPriInsPlan.Checked = true;
                        }
                        if (_dt.Rows[0]["SettingsValue"].ToString().Contains("secinsplan"))
                        {
                            ChkSecInsPlan.Checked = true;
                        }
                        if (_dt.Rows[0]["SettingsValue"].ToString().Contains("PCP"))
                        {
                            chkPCP.Checked = true;
                        }
                        if (_dt.Rows[0]["SettingsValue"].ToString().Contains("dob"))
                        {
                            chkPrblLDOB.Checked = true;
                        }
                        if (_dt.Rows[0]["SettingsValue"].ToString().Contains("gender"))
                        {
                            chkPrblLstGender.Checked = true;
                        }

                        if (_dt.Rows[0]["SettingsValue"].ToString().Contains("race"))
                        {
                            chkPrblLstRace.Checked = true;
                        }

                        if (_dt.Rows[0]["SettingsValue"].ToString().Contains("ethnicity"))
                        {
                            chkPrblLEthnicity.Checked = true;
                        }

                        if (_dt.Rows[0]["SettingsValue"].ToString().Contains("langauge"))
                        {
                            chkPrblLLangauge.Checked = true;
                        }
                        if (_dt.Rows[0]["SettingsValue"].ToString().Contains("patientnotes"))
                        {
                            chkPatNotes.Checked = true;
                        }

                        if (_dt.Rows[0]["SettingsValue"].ToString().Contains("MedicalCategory"))
                        {
                            chkPrblLMedicalCategory.Checked = true;
                        }

                        if (_dt.Rows[0]["SettingsValue"].ToString().Contains("commpreference"))
                        {
                            chkPrblLCommPref.Checked = true;
                        }
                        if (_dt.Rows[0]["SettingsValue"].ToString().Contains("sAddressLine3"))
                        {
                            chkAddress.Checked = true;
                        }
                        if (_dt.Rows[0]["SettingsValue"].ToString().Contains("sAddressLine1"))
                        {
                            chksAddressLine1.Checked = true;
                        }
                        if (_dt.Rows[0]["SettingsValue"].ToString().Contains("sAddressLine2"))
                        {
                            chksAddressLine2.Checked = true;
                        }

                        if (_dt.Rows[0]["SettingsValue"].ToString().Contains("sCity"))
                        {
                            chksCity.Checked = true;
                        }
                        if (_dt.Rows[0]["SettingsValue"].ToString().Contains("sState"))
                        {
                            chksState.Checked = true;
                        }
                        if (_dt.Rows[0]["SettingsValue"].ToString().Contains("sZip"))
                        {
                            chksZip.Checked = true;
                        }




                        if (_dt.Rows[0]["SettingsValue"].ToString().Contains("Phone"))
                        {
                            chkPhone.Checked = true;
                        }
                        if (_dt.Rows[0]["SettingsValue"].ToString().Contains("Mobile"))
                        {
                            chkMobile.Checked = true;
                        }
                        if (_dt.Rows[0]["SettingsValue"].ToString().Contains("Email"))
                        {
                            chkEmail.Checked = true;
                        }
                        if (_dt.Rows[0]["SettingsValue"].ToString().Contains("problemlist"))
                        {
                            chkPrblmLstOthrProblemList.Checked = true;
                        }

                        if (_dt.Rows[0]["SettingsValue"].ToString().Contains("medication"))
                        {
                            chkPrblmLstOthrMedication.Checked = true;

                        }
                        if (_dt.Rows[0]["SettingsValue"].ToString().Contains("immunization"))
                        {
                            chkPrblmLstOthrImmunization.Checked = true;
                        }
                        if (_dt.Rows[0]["SettingsValue"].ToString().Contains("labresult"))
                        {
                            chkPrblmLstOthrLabResult.Checked = true;
                        }
                        if (_dt.Rows[0]["SettingsValue"].ToString().Contains("allergy"))
                        {
                            chkPrblmLstOthrAllergy.Checked = true;
                        }

                        if (_dt.Rows[0]["SettingsValue"].ToString().Contains("Demographic"))
                        {
                            chkPrblLstDemographicAll.Checked = true;
                        }

                        if (_dt.Rows[0]["SettingsValue"].ToString().Contains("OthrElement"))
                        {
                            chkPrblmLstOthrElement.Checked = true;
                        }
                        if (_dt.Rows[0]["SettingsValue"].ToString().Contains("showdatetime"))
                        {
                            chkDateTime.Checked = true;
                        }
                        if (_dt.Rows[0]["SettingsValue"].ToString().Contains("ClaimCPT"))
                        {
                            chkClaimCPT.Checked = true;
                        }
                        if (_dt.Rows[0]["SettingsValue"].ToString().Trim().Contains("ClaimDX"))
                        {
                            chkClaimDX.Checked  = true;
                        }
                        if (_dt.Rows[0]["SettingsValue"].ToString().Contains("Facility"))
                        {
                            chkFacility.Checked = true;
                        }
                    }
                    else
                    {
                        chkPrblmLstOthrElement.Checked = true;
                    }
                }
                else
                {
                    chkPrblmLstOthrElement.Checked = true;
                }


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);

            }
            finally
            {
                oDB.Dispose();
                oDB = null;
                if (_dt != null)
                {
                    _dt.Dispose();
                    _dt = null;
                }
            }

        }

        private void frmSSRSViewer_FormClosed(object sender, FormClosedEventArgs e)
        {
            RemoveControl();
           

            if (dt != null)
            {
                dt.Dispose();
                dt = null;
            }
            if (dtTemp != null)
            {
                dtTemp.Dispose();
                dtTemp = null;
            }
            if (dsDemographic != null)
            {
                dsDemographic.Dispose();
                dsDemographic = null;
            }
            
        }

        private void rbImGiven_CheckedChanged(object sender, EventArgs e)
        {
            if (rbImGiven.Checked == true)
            {
                rbImGiven.Font = gloGlobal.clsgloFont.gFont_BOLD; //new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {
                rbImGiven.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
            }

        }

        private void rbImNotGiven_CheckedChanged(object sender, EventArgs e)
        {
            if (rbImNotGiven.Checked == true)
            {
                rbImNotGiven.Font = gloGlobal.clsgloFont.gFont_BOLD;// new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {
                rbImNotGiven.Font = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
            }

        }

        private void btnBrowseClaimCpt_Click(object sender, EventArgs e)
        {
            strLst = "ClaimCpt";
            pnlcustomTask.Height = 400;
            pnlcustomTask.Width = 500;
            LoadUserGrid();
            SetCheckValues(lstClaimCpt);
            pnlcustomTask.BringToFront();
        }

        private void btnBrowseClaimDx_Click(object sender, EventArgs e)
        {
            strLst = "ClaimDx";
            pnlcustomTask.Height = 400;
            pnlcustomTask.Width = 500;
            LoadUserGrid();
            SetCheckValues(lstClaimDx);
            pnlcustomTask.BringToFront();
        }

        private void btnClearClaimCpt_Click(object sender, EventArgs e)
        {
            try
            {

                if (lstClaimCpt.Items.Count > 0)
                {
                    if (lstClaimCpt.SelectedItem != null)
                    {
                        if (_DictionaryClaimCPT.ContainsKey(lstClaimCpt.SelectedItem.ToString()))
                        {
                            _DictionaryClaimCPT.Remove(lstClaimCpt.SelectedItem.ToString());
                        }
                        lstClaimCpt.Items.Remove(lstClaimCpt.SelectedItems[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClearClaimDx_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstClaimDx.Items.Count > 0)
                {
                    if (lstClaimDx.SelectedItem != null)
                    {
                        if (_DictionaryClaimDx.ContainsKey(lstClaimDx.SelectedItem.ToString()))
                        {
                            _DictionaryClaimDx.Remove(lstClaimDx.SelectedItem.ToString());
                        }
                        lstClaimDx.Items.Remove(lstClaimDx.SelectedItems[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClearAllClaimCpt_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstClaimCpt.Items.Count > 0)
                {
                    //lstClaimCpt.Items.Clear();
                    lstClaimCpt.DataSource = null;
                    lstClaimCpt.Items.Clear();
                    _DictionaryClaimCPT.Clear();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClearAllClaimDx_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstClaimDx.Items.Count > 0)
                {
                   // lstClaimDx.Items.Clear();
                    lstClaimDx.DataSource = null;
                    lstClaimDx.Items.Clear();
                    _DictionaryClaimDx.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBrowseFacility_Click(object sender, EventArgs e)
        {
            strLst = "Facility";
            pnlcustomTask.Height = 400;
            pnlcustomTask.Width = 500;
            LoadUserGrid();
            SetCheckValues(lstFacility);
            pnlcustomTask.BringToFront();

        }

        private void btnClearFacility_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstFacility.Items.Count > 0)
                {
                    if (lstFacility.SelectedItem != null)
                    {
                        if (_DictionaryClaimDx.ContainsKey(lstFacility.SelectedItem.ToString()))
                        {
                            _DictionaryFacility.Remove(lstFacility.SelectedItem.ToString());
                        }
                        lstFacility.Items.Remove(lstFacility.SelectedItems[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClearAllFacility_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstFacility.Items.Count > 0)
                {
                 //   lstFacility.Items.Clear();
                    lstFacility.DataSource = null;
                    lstFacility.Items.Clear();
                    _DictionaryFacility.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBrowseInsPlan_Click(object sender, EventArgs e)
        {
            strLst = "InsPlan";
            pnlcustomTask.Height = 225;
            pnlcustomTask.Width = 373;
            LoadUserGrid();
            SetCheckValues(cmbPatInsPlan);
            pnlcustomTask.BringToFront();
        }

        private void btnClearInsPlan_Click(object sender, EventArgs e)
        {
            if (cmbPatInsPlan.Items.Count > 0)
            {
                if (cmbPatInsPlan.SelectedItem != null)
                {
                    if (_DictionaryPatInsPlan.ContainsKey(((System.Collections.Generic.KeyValuePair<string, string>)(cmbPatInsPlan.SelectedItem)).Key.ToString()))
                    {
                        _DictionaryPatInsPlan.Remove(((System.Collections.Generic.KeyValuePair<string, string>)(cmbPatInsPlan.SelectedItem)).Key.ToString());
                    }
                    
                }
            }
           
            cmbPatInsPlan.DataSource = null;
            cmbPatInsPlan.Items.Clear();
        }

        private void chkDueDate_CheckedChanged(object sender, EventArgs e)
        {
            dtFromDueDate.Enabled = chkDueDate.Checked;
            dtToDueDate.Enabled = chkDueDate.Checked;

            dtToDueDate.MinDate = dtFromDueDate.Value;
        }

        private void dtFromDueDate_ValueChanged(object sender, EventArgs e)
        {
            dtToDueDate.MinDate = dtFromDueDate.Value;
        }

        private void pnlPtntLstDemo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void chkShowAllProviders_CheckedChanged(object sender, EventArgs e)
        {
            Fill_Provider();
        }

      
    
    }

    // chetan added for patient reminder 

    public class SSRSArgs : EventArgs
    {
        public string sCriteriaID { get; set; }
    }
    public class PatientArgs : EventArgs
    {
        public DataTable dtPatients { get; set; }
    }



}
