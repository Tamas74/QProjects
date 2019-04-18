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
using gloUserControlLibrary;



namespace gloReports
{
    public partial class frmSSRSViewer : Form
    {

        string FOR_ALL = "For All";
        string FOR_AGE = "For Age";
        string FOR_LESSTHAN_AGE = "Less Than";
        string FOR_GREATERTHAN_AGE = "Greater Than";
        string FROMTO_AGE = "Between";
        string LabTestResult = string.Empty;
        string LabTestResult1 = string.Empty;
        string[] ColoumNames;
        string[] ResultCondition;
        DataTable oResult = new DataTable();
        string strReportServer = string.Empty;
        string strReportFolder = string.Empty;
        string strVirtualDir = string.Empty;
        string _reportName = string.Empty;
        string strParameter = string.Empty;
        string _UserName = string.Empty;
        string _conn = string.Empty;
        string _reportTitle = string.Empty;
        string reportParam = string.Empty;

        bool _IsgloStreamReport ;
        System.Uri SSRSReportURL;
        string _databaseconnectionstring = string.Empty;

        string strLst = "";
        // chetan added for patient reminder on 20 oct 2010
        private string _Medications = "";
        private string _ProblemLists = "";
        public string sPatientIDs { get; set; }
    

        private string _DMCriteriaIDs = "";
        public delegate void GenerateReportClick(object sender, EventArgs e, SSRSArgs eSSRS);
        public event GenerateReportClick GenerateReport_Click;
        private const int COL_Select = 0;
        private const int COL_PatientCode = 1;
        private const int COL_PatientID = 2;
        private const int COL_PatientName = 3;
        private const int COL_DOB = 4;
        private const int COL_age = 5;
        private const int COL_Gender = 6;
        private const int COL_ProviderID = 7;
        private const int COL_PRoviderName = 8;
        private const int COL_sCommunicationPreference = 9;
        private const int COL_ApptStatus = 10;

        // chetan added for patient reminder on 20 oct 2010
        System.Collections.ArrayList arrdm = new System.Collections.ArrayList();
        public delegate void SendReminderLetter(object sender, EventArgs e, PatientArgs eDT);
 
        public event SendReminderLetter SendReminder_Letter;
        private CustomTask dgCustomGrid;
        Boolean bHistory = false;
        private string gstrMessageBoxCaption = "gloEMR";
        DataTable dt;
        private int Col_Check = 2;
        private int Col_Name = 0;
        private int Col_Dosage = 1;
        private int Col_NDCCode = 3;
        private int Col_Count = 4;
        
        Boolean bDM = false;
        DataTable oDiag = new DataTable();
        DataTable oCPT = new DataTable();

        private string gstrSQLServerName;
        private string gstrDatabaseName;
        private bool gblnSQLAuthentication;
        private string gstrSQLUser;
        private string gstrSQLPassword;
        private string RptName = "";

        Int64  ProviderID =0 ;
        string AgeCri = "For All";
        int FromAge = 0;
        int ToAge = 0;
        string DateSelect = "False";
        string includedetails = "True";
        string OnlyDrugAllergy = "";

        List<Microsoft.Reporting.WinForms.ReportParameter> paramList = new List<Microsoft.Reporting.WinForms.ReportParameter>();
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
     
 
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

        public string reportTitle
        {
            get { return _reportTitle; }
            set { _reportTitle = value; }
        }

        public bool  IsgloStreamReport
        {
            get { return _IsgloStreamReport; }
            set { _IsgloStreamReport = value; }
        }

         public string databaseconnectionstring
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }
        }
        
        public frmSSRSViewer(string _gstrSQLServerName,string _gstrDatabaseName,bool  _gblnSQLAuthentication,string _gstrSQLUser,string _gstrSQLPassword)
        {

            //SANDEEP DARADE-20100604

            gstrSQLServerName = _gstrSQLServerName;
            gstrDatabaseName = _gstrDatabaseName;
            gblnSQLAuthentication = _gblnSQLAuthentication;
            gstrSQLUser = _gstrSQLUser;
            gstrSQLPassword = _gstrSQLPassword;
            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationSettings.AppSettings;

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

            InitializeComponent();
        }

        private void frmSSRSViewer_Load(object sender, EventArgs e)
        {
            dtpicFrom.Text = (Convert.ToDateTime(dtpicTo.Value.Date.AddMonths(-3).ToShortDateString())).ToString();
            dtpicFrom.Checked = true;
            Tblbtn_More.Visible = false;
            pnlDrugDiagnosis.Visible = false;
            _databaseconnectionstring = Conn;

            if (_reportName == "AllergyUsageReport")
            {
                RptName = "AllergyUsageReport";
                chkOnlyDrug.Visible = true;
                pnlDemo.Visible = false;
            }

            else if (_reportName == "rptPatientVitalUsage")
            {
                RptName = "VitalUsageReport";
                chkOnlyDrug.Visible = false;
                pnlDemo.Visible = false;
            }

            else if (_reportName == "DemographicUsageReport")
            {
                RptName = "DemographicUsageReport";
                chkOnlyDrug.Visible = false;
                pnlDemo.Visible = true;
                chkAll.Checked = true;
            }

            else if (_reportName == "ProblemUsageReport")
            {
                RptName = "ProblemUsageReport";
                chkOnlyDrug.Visible = false;
                pnlDemo.Visible = false;
            }

            else if (_reportName == "rptMedicationMU")
            {
                RptName = "MedicationUsageReport";
                chkOnlyDrug.Visible = false;
                pnlDemo.Visible = false;
            }

            else if (_reportName == "rpteRx")
            {
                RptName = "PrescriptionUsageReport";
                chkOnlyDrug.Visible = false;
                pnlDemo.Visible = false;
                pnlDiag.Visible = false;
                Tblbtn_More.Visible = true;
            }

            else if (_reportName == "rptPatientHistoryUsage")
            {
                RptName = "HistoryUsageReport";
                chkOnlyDrug.Visible = false;
                pnlDemo.Visible = false;
                Tblbtn_More.Visible = true;
                lblMedication.Text = "Category: ";
                lblDiagnosis.Text = "History Items: ";
                bHistory = true;
            }
                //Roopali
            else if (_reportName == "rptPatientList")
            {
                RptName = "rptPatientList";
                Panel3.Visible = false;
                Tblbtn_More.Visible = true;
                pnlPatientList.Visible = false;
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
                tsb_ReminderLetters.Visible = false;
                //cmbLabResult.BackColor = Color.White;  


            }
                // chetan added for patient reminder report on 20 oct 2010
            else if (_reportName == "rptPatientListDM")
            {
                RptName = "rptPatientListDM";
                Panel3.Visible = false;
                Tblbtn_More.Visible = true;
             tblButtonShow.Visible = true;
              // pnlPatientList.Visible = false;
                pnlDmPatientList.Visible = false;
                dtFromAppt.Enabled = false;
                dtToAppt.Enabled = false;
                SSRSViewer.Visible = true ;
                dtpicFrom.Checked = false;
                tblbtn_Print_32.Visible = false;
                tblbtn_Export.Visible = false;
                FillApptStatus();
            }
            
            SetAgeCr();
            Fill_Provider();
            if (_reportName == "rptPatientListDM" || _reportName == "rptPatientList")
            {
                //SSRSViewer.Visible = false;
            }
            else
            {
                Loadreport();
            }
        }
        private void FillApptStatus()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);

            try
            {

                DataTable dt = new DataTable();
                dt.Columns.Add("Eno");
                dt.Columns.Add("AppName");
                dt.Rows.Add("-1", "All");
                dt.Rows.Add("1", "Followup");
                dt.Rows.Add("3", "NewPatient");
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
                gloSettings.GeneralSettings oSetting = new gloSettings.GeneralSettings(_databaseconnectionstring);
                object oValue = new object();
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

                if (strReportServer == "" || strReportFolder == "" || strVirtualDir == "")
                {
                    MessageBox.Show("SSRS Settings not set. Set the Report Server settings and then deploy the reports.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    this.Text = _reportTitle;
                    reportParam =  "Conn=" + _conn;
                    SSRSReportURL = new Uri("http://" + strReportServer + "/" + strVirtualDir);
                    SSRSViewer.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Remote;
                    SSRSViewer.ServerReport.ReportServerUrl = SSRSReportURL;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("SSRS Reporting Service is not available.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
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

                    if (Convert.ToInt64(cmbProvider.SelectedValue)  == 0)
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
                            MessageBox.Show(" From-age should be less than To-Age ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            cmbAgeFrom.Focus();
                            return;
                        }
                    }

                    AgeCri = cmbAge.Text ; 
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

                    gloSSRS.Create_Datasource("dsEMR","gloEMR" , _databaseconnectionstring, gstrSQLServerName, gstrDatabaseName, gblnSQLAuthentication, gstrSQLUser, gstrSQLPassword, true);      
                    SSRSViewer.ServerReport.ReportPath = "/" + strReportFolder + "/" + _reportName;// +reportParam;
                    switch(_reportName)
                    {

                        case "rptPatientVitalUsage" :
                            SetParameter("user,AgeCri,AgeFrom,AgeTo,providerid,dtVitalDateFrom,dtVitalDateTo,blnDateselected,ShowSummary,GrowthChartOnly", "" + _UserName + "," + AgeCri + "," + FromAge.ToString() + "," + ToAge.ToString() + "," + ProviderID.ToString() + "," + _dtFrom.ToString() + "," + _dtTo.ToString() + "," + DateSelect + "," + Convert.ToString(!(chkShowUsageDeatal.Checked)) + "," + Convert.ToString(false) + "");
                            break;


                        case "AllergyUsageReport":
                            SetParameter("user,AgeCri,AgeFrom,AgeTo,Provider,FromDate,ToDate,IncludeUsageDetails,OnlyDrugAllergy,IsDateSelected", "" + _UserName + "," + AgeCri + "," + FromAge.ToString() + "," + ToAge.ToString() + "," + ProviderID.ToString() + "," + _dtFrom.ToString() + "," + _dtTo.ToString() + "," + includedetails + "," + OnlyDrugAllergy + ","+DateSelect+"");
                            break;


                        case "ProblemUsageReport":
                            SetParameter("user,AgeCri,AgeFrom,AgeTo,Provider,FromDate,ToDate,IncludeUsageDetails,IsDateSelected", "" + _UserName + "," + AgeCri + "," + FromAge.ToString() + "," + ToAge.ToString() + "," + ProviderID.ToString() + "," + _dtFrom.ToString() + "," + _dtTo.ToString() + "," + includedetails + "," + DateSelect + "");
                            break;


                        case "DemographicUsageReport":
                            SetParameter("user,AgeCri,AgeFrom,AgeTo,Provider,FromDate,ToDate,IncludeUsageDetails,Language,Insurance,Gender,Race,Ethnicity,DOB,IsDateSelected", "" + _UserName + "," + AgeCri + "," + FromAge.ToString() + "," + ToAge.ToString() + "," + ProviderID.ToString() + "," + _dtFrom.ToString() + "," + _dtTo.ToString() + "," + includedetails + "," + Convert.ToString(chkLanguage.Checked) + "," + Convert.ToString(chkInsurance.Checked) + "," + Convert.ToString(chkGender.Checked) + "," + Convert.ToString(chkRace.Checked) + "," + Convert.ToString(chkEthnicity.Checked) + "," + Convert.ToString(chkDOB.Checked) + "," + DateSelect + "");
                            break;


                        case "rptMedicationMU":
                            string blnDateselected = "true";
                            if (_dtFrom.ToString() == "1/1/1900 12:00:00 AM")
                            {
                                blnDateselected = "false";
                            }
                            SetParameter("user,AgeCri,AgeFrom,AgeTo,Provider,FromDate,ToDate,IncludeUsageDetails,blnDateselected", "" + _UserName + "," + AgeCri + "," + FromAge.ToString() + "," + ToAge.ToString() + "," + ProviderID.ToString() + "," + _dtFrom.ToString() + "," + _dtTo.ToString() + "," + includedetails + "," + blnDateselected + "");
                            break;

                           // chetan added for Reminder Report

                        case "rptPatientListDM":

                            tblbtn_Print_32.Visible = true;
                            tblbtn_Export.Visible = true;

                            _ProblemLists = "";
                            _Medications = "";

                            for (int lenprb = 0; lenprb < lstDMProblemList.Items.Count; lenprb++)
                                _ProblemLists += lstDMProblemList.Items[lenprb].ToString().Replace("'", "''") + "|";
                            if (_ProblemLists.Length > 1)
                                _ProblemLists = _ProblemLists.Substring(0, _ProblemLists.Length - 1);


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

                                    _DMCriteriaIDs += "'" + lstdmsetup.Items[len].ToString().Replace("'", "''") + "'" + ",";
                                }
                            }


                            if (_DMCriteriaIDs.Length >= 2)
                            {
                                _DMCriteriaIDs = _DMCriteriaIDs.Substring(0, _DMCriteriaIDs.Length - 1);
                            }


                            SSRSArgs _e = new SSRSArgs();
                            _e.sCriteriaID = _DMCriteriaIDs.Replace("|", ",");
                            GenerateReport_Click(null, null, _e);

                            if (_Medications == "")
                            {
                                _Medications = " ";
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

                            SetParameter("nProviderID,AgeType,nAgeFrom,nAgeTo,sMedication,sProblemList,sPatientId,apptfrdate,appttodate,frdate,todate,apptflg,DMCriteria", "" +
                             ProviderID + "," + cmbAge.SelectedIndex + "," + FromAgeDM + "," + ToAgeDM + "," + _Medications + "," + _ProblemLists + "," + sPatientIDs + "," + strapptfrdate + "," + strappttodate + "," + strdtpicFrom + "," + strdtpicTo + "," + strapptflg + "," + _DMCriteria.ToString());


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
                                    MessageBox.Show(" From-age should be less than To-Age ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    cmbAgeFrom.Focus();
                                    return;
                                }
                            }

                            string strMedication = "";
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
                                }
                                else
                                {
                                    strMedication = strMedication + "|" + sDrug[0].ToString().Trim();//+ "'";
                                }
                            }

                            string strProblemList = "";
                            if (lstProblemList.Items.Count > 0)
                            {
                                lstProblemList.SelectedIndex = 0;
                                for (int p = 0; p <= lstProblemList.Items.Count - 1; p++)
                                {
                                    if (p == 0)
                                    {

                                        strProblemList = lstProblemList.Items[p].ToString();
                                    }
                                    else
                                    {
                                        //LstMedication.SelectedIndex = p;
                                        strProblemList = strProblemList + "|" + lstProblemList.Items[p].ToString().Trim();
                                    }
                                }

                            }



                            Int64 nProviderID = RetrieveProviderID(cmbProvider.Text);


                            string ConditionOne = cmbFstPat.SelectedItem.ToString();
                            string ConditionTwo = cmbSndPat.SelectedItem.ToString();
                            string ConditionThree = cmb3rdPat.SelectedItem.ToString();

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
                                    string[] LabResults = new string[2];
                                    string condition = string.Empty;
                                    char[] sep3 = new char[] { '>' };
                                    char[] sep4 = new char[] { '<' };
                                    char[] sep5 = new char[] { '=' };

                                    LabResultsBitween = coloum.Split(sep5);
                                    LabResultsLess = coloum.Split(sep4);
                                    LabResultsGreater = coloum.Split(sep3);

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
                            if (strProblemList == "")
                                strProblemList = " ";
                            if (TableColoumn == "")
                                TableColoumn = " ";
                            if (strImmunizatation == "")
                                strImmunizatation = " ";

                            bool ImGiven = false;

                            if (rbImGiven.Checked == true)
                                ImGiven = true;
                            else if (rbImNotGiven.Checked == true)
                                ImGiven = false;

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
                           

                            break;


                        
                        
                        case "rpteRx":
                            chkExcControlledSubstance.Visible = true; // this check box will be shown only when we want to see the eRx MU report,else while design time it is set to visible false
                            string strNDCCode = "";
                            string sIncControlledSubs = "false";
                            string blnRxDateselected = "true";
                            if (LstMedication.Items.Count > 0)
                            {
                                LstMedication.SelectedIndex = 0;
                                 for (int i = 0; i <= LstMedication.Items.Count - 1; i++)
                                {
                                    if (i == 0)
                                    {
                                        strNDCCode = LstMedication.SelectedValue.ToString() ;
                                    }
                                    else
                                    {
                                        LstMedication.SelectedIndex = i;
                                        strNDCCode = strNDCCode + "|" + LstMedication.SelectedValue.ToString().Trim();
                                    }
                                }
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
                                    strHisCateg = LstMedication.Items[k].ToString().Trim() ;
                                    if (LstMedication.Items.Count != 1)
                                    {
                                        strHisCateg += ",";
                                    }
                                }
                                else
                                {
                                    strHisCateg = strHisCateg + LstMedication.Items[k].ToString().Trim() ;
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
                                    strHistDet = LstDiagnosis.Items[k].ToString().Trim() ;
                                    if (LstDiagnosis.Items.Count != 1)
                                    {
                                        strHistDet += ",";
                                    }
                                }
                                else
                                {
                                    strHistDet = strHistDet  + LstDiagnosis.Items[k].ToString().Trim() ;
                                    if (k != LstDiagnosis.Items.Count - 1)
                                    {
                                        strHistDet += ",";
                                    }
                                }
                            }
                      
                            SetParametersForHx ("user,AgeCri,AgeFrom,AgeTo,providerid,dtDateFrom,dtDateTo,blnDateselected,IncludeUsageDetails,History,HistoryCat",
                                "" + _UserName + "~" + AgeCri + "~" + FromAge.ToString() + "~" + ToAge.ToString() + "~" + ProviderID.ToString() + "~" + _dtFrom.ToString() + "~"
                                + _dtTo.ToString() + "~" + Convert.ToString( dtpicFrom.Checked) + "~" + Convert.ToString((chkShowUsageDeatal.Checked)) + "~" + strHistDet + "~" + strHisCateg + "");

                            break;
                    }
                    this.SSRSViewer.ServerReport.SetParameters(paramList);
                }
                else
                {
                    SSRSViewer.ServerReport.ReportPath = "/" + strReportFolder + "/" + _reportName;// +reportParam;
                }
                this.SSRSViewer.RefreshReport();
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
                else
                {
                    MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
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
                objCmd.CommandText = "sp_RetrieveProviderID";
                objCmd.Connection = objCon;

                SqlParameter objParaProviderName = new SqlParameter();

                objParaProviderName.ParameterName = "@ProviderName";
                objParaProviderName.Value = strProviderName;
                objParaProviderName.Direction = ParameterDirection.Input;
                objParaProviderName.SqlDbType = SqlDbType.VarChar;

                objCmd.Parameters.Add(objParaProviderName);
                nProviderID = Convert.ToInt64(objCmd.ExecuteScalar());

                if (nProviderID == null)
                {
                    nProviderID = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objCmd = null;
                objCon.Close();
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
                   paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter(PName[i] , Pvalue[i], false));
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
                dt = oProvider.GetProviders();

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
                    cmbProvider.SelectedIndex = 0;
                }
                dt = null;
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
            tsb_ReminderLetters.Visible = false;

            switch (e.ClickedItem.Tag.ToString())
            {

                case "Generate Report":
                    {
                        SSRSViewer.Visible = true; 
                        Loadreport();
                    }
                    break;

                case "Print":
                    SSRSViewer.PrintDialog(); 
                    break;

                case "Export":
                    ExportReport();
                    break;

                case "More":
                    break;

                case "Hide":
                    break;

                case "ShowPatients":
                    this.Cursor = Cursors.WaitCursor;
                    try
                    {
                        SSRSViewer.Visible = false;
                        C1Patients.Visible = true;
                        FillPatients();
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


        private void SendRemiderLetters()
        {
            DataTable dtPatints = new DataTable();
            DataRow oRow;
            dtPatints.Columns.Add("Select");
            dtPatints.Columns.Add("PatientID");
            dtPatints.Columns.Add("PatientCode");
            dtPatints.Columns.Add("PatientName");

            try
            {
                for (int iRow = 1; iRow < C1Patients.Rows.Count; iRow++)
                {
                    if (C1Patients.GetCellCheck(iRow, COL_Select) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                    {
                        oRow = dtPatints.NewRow();
                        oRow["Select"] = 1;
                        oRow["PatientID"] = C1Patients.GetData(iRow, COL_PatientID);
                        oRow["PatientCode"] = C1Patients.GetData(iRow, COL_PatientCode);
                        oRow["PatientName"] = C1Patients.GetData(iRow, COL_PatientName);
                        dtPatints.Rows.Add(oRow);
                    }
                }


                PatientArgs _ePat = new PatientArgs();
                _ePat.dtPatients = dtPatints;
                SendReminder_Letter(null, null, _ePat);
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
                chkDOB.Checked = false ;
                chkEthnicity.Checked = false;
                chkGender.Checked = false;
                chkInsurance.Checked = false;
                chkLanguage.Checked = false;
                chkRace.Checked = false;
                
                chkDOB.Enabled = true ;
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
            saveFileDialog1.FileName = RptName ;
            saveFileDialog1.ShowDialog();
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
                    dgCustomGrid.SetSelectAllVisible = false;
                    dgCustomGrid.Width = pnlcustomTask.Width;
                    dgCustomGrid.Height = pnlcustomTask.Height;
                    dgCustomGrid.C1Grid.AllowEditing = false;
                    dgCustomGrid.BringToFront();
                    dgCustomGrid.SetVisible = false;
                    BindUserGrid();
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
            pnlcustomTask.Controls.Add(dgCustomGrid);
            dgCustomGrid.SelectAllClick += new CustomTask.SelectAllClickEventHandler(dgCustomGrid_SelectAllClick);
            dgCustomGrid.DeSelectAllClick += new CustomTask.DeSelectAllClickEventHandler(dgCustomGrid_DeSelectAllClick);
            dgCustomGrid.OKClick += new CustomTask.OKClickEventHandler(dgCustomGrid_OKClick);
            dgCustomGrid.CloseClick += new CustomTask.CloseClickEventHandler(dgCustomGrid_CloseClick);
            dgCustomGrid.SearchChanged += new CustomTask.SearchChangedEventHandler(dgCustomGrid_SearchChanged);
            dgCustomGrid.GridDoubleClick += new CustomTask.GridDoubleClickEventHandler(dgCustomGrid_GridDoubleClick);
            pnlcustomTask.BringToFront();

            int y = 0;
            int x = 0;

            if (strLst == "drugs")
            {
                if ((_reportName == "rptPatientHistoryUsage") || (_reportName == "rpteRx"))
                {
                    dgCustomGrid.setPnlVisible = false;
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
                y = 205;
                x = 330;
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

            else if (strLst == "PatientDMProblem")
            {
                y = 90;
                x = 190;
            }
            else if (strLst == "PatientImmunization")
            {
                y = 205;
                x = 630;
            }
            else if (strLst == "PatientLabResults")
            {
                y = 195;
                x = 680;
            }
            pnlcustomTask.Location = new Point(x, y);
            pnlcustomTask.Visible = true;
            dgCustomGrid.Visible = true;
            pnlcustomTask.BringToFront();
            dgCustomGrid.BringToFront();
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
                pnlcustomTask.Controls.Remove(dgCustomGrid);
                dgCustomGrid.Visible = false;
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

        void dgCustomGrid_OKClick(object sender, EventArgs e)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                if (LstMedication.Items.Count == 0)
                {
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
                    if (strLst == "PatDMCriteria")
                        arrdm.Clear();
                    if (strLst != "PatientLabResults")
                    {
                        for (i = 0; i <= dgCustomGrid.GetTotalRows - 1; i++)
                        {
                            if (Convert.ToBoolean(dgCustomGrid.CurrentID.ToString()) == true)
                            {
                                if (dgCustomGrid.get_GetIsChecked(i, 0) == true)
                                {
                                    if (strLst == "drugs")
                                    {
                                        if ((_reportName == "rptMedicationMU") || (_reportName == "rpteRx"))
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
                                            LstMedication.Items.Add(dgCustomGrid.get_GetItem(i, 1).ToString());
                                        }
                                    }
                                    else if (strLst == "cpt")
                                    {
                                        LstTreatment.Items.Add(dgCustomGrid.get_GetItem(i, 1).ToString());
                                    }
                                    else if (strLst == "diag")
                                    {
                                        LstDiagnosis.Items.Add(dgCustomGrid.get_GetItem(i, 1).ToString());
                                    }
                                    else if (strLst == "PatientMedication")
                                    {
                                        string str = dgCustomGrid.get_GetItem(i, 1).ToString();
                                        if (!lstPatMedication.Items.Contains(str))
                                            lstPatMedication.Items.Add(dgCustomGrid.get_GetItem(i, 1).ToString());
                                    }
                                    else if (strLst == "PatientImmunization")
                                    {
                                        string str = dgCustomGrid.get_GetItem(i, 1).ToString();
                                        if (!lstImmunization.Items.Contains(str))
                                            lstImmunization.Items.Add(str);
                                    }
                                    // chetan added for patient reminder on 20 oct 2010
                                    else if (strLst == "PatDMCriteria")
                                    {
                                        string str = dgCustomGrid.get_GetItem(i, 1).ToString();
                                        if (!lstdmsetup.Items.Contains(str))
                                        {
                                            lstdmsetup.Items.Add(dgCustomGrid.get_GetItem(i, 1).ToString());
                                            arrdm.Add(dgCustomGrid.get_GetItem(i, 2).ToString());
                                        }
                                    }

                                    else if (strLst == "PatientDMMedication")
                                    {
                                        string str = dgCustomGrid.get_GetItem(i, 1).ToString();
                                        if (!lstmeddmsetup.Items.Contains(str))

                                            lstmeddmsetup.Items.Add(dgCustomGrid.get_GetItem(i, 1).ToString());
                                    }

                                    else if (strLst == "PatientDMProblem")
                                    {
                                        string str = dgCustomGrid.get_GetItem(i, 1).ToString();
                                        if (!lstDMProblemList.Items.Contains(str))

                                            lstDMProblemList.Items.Add(dgCustomGrid.get_GetItem(i, 1).ToString());
                                    }


                                }
                            }


                        }
                    }
                    else
                    {

                        string str = dgCustomGrid.get_GetItem(dgCustomGrid.GetCurrentrowIndex, 0).ToString();
                        cmbLabResult.Text = "";
                        cmbLabResult.Text = str;


                    }

                    if ((_reportName == "rptMedicationMU") || (_reportName == "rpteRx"))
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
                DataView dvPatient = new DataView();
                dvPatient = (DataView)dgCustomGrid.GridDatasource;
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
                    dvPatient.RowFilter = dvPatient.Table.Columns[0].ColumnName + " Like '" + strPatientSearchDetails + "%'  ";
                }
                else
                {
                    dvPatient.RowFilter = dvPatient.Table.Columns[0].ColumnName + " Like '%" + strPatientSearchDetails + "%' ";
                }

                dgCustomGrid.Enabled = false;
                dgCustomGrid.datasource(dvPatient);
                dgCustomGrid.Enabled = true;
                this.Cursor = Cursors.Default;
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
                dgCustomGrid.datasource(dt.DefaultView);
            }
            else
            {
                dgCustomGrid.SetSelectAllVisible = false;
                dgCustomGrid.SetDeSelectAllVisible = false;
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
                dt = FillDrugs(beerslist);
                CustomDrugsGridStyle();
                DataColumn col = new DataColumn();
                col.ColumnName = "Select Data";
                col.DataType = System.Type.GetType("System.Boolean");
                dgCustomGrid.C1Grid.SetData(0, Col_Name,2);
                col.DefaultValue = false;
                if (!(dt.Columns.Contains(col.ColumnName)))
                {
                    dt.Columns.Add(col);
                }

                if ((dt != null))
                {
                    dgCustomGrid.datasource(dt.DefaultView);
                }

                // RESET THE GRID
                float _TotalWidth = dgCustomGrid.Gridwidth - 5;
                dgCustomGrid.C1Grid.Cols.Move(dgCustomGrid.C1Grid.Cols.Count - 1, 0);
                dgCustomGrid.C1Grid.AllowEditing = true;
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
            dgCustomGrid.C1Grid.AllowEditing = true ;
            dgCustomGrid.C1Grid.SetData(0, Col_Check, "Select");
            dgCustomGrid.C1Grid.SetData(0, Col_Name, "Name");
            dgCustomGrid.C1Grid.SetData(0, Col_NDCCode , "NDCCode");
            dgCustomGrid.C1Grid.Cols[3].Width=0;
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



                   // chetan added for patient reminder on 20 oct 2010
                    case "patientdmmedication":
                        {
                            dt = FillDrugs();
                            break;
                        }

                    case "patientdmproblem":
                        {
                            dt = FillChiefComplaint();
                            break;
                        }

                    case "patdmcriteria":
                        {
                            dt = FillDM();
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
                dgCustomGrid.C1Grid.AllowEditing = true ;
                if (strLst.ToLower() != "patientlabresults")
                {
                dgCustomGrid.C1Grid.Cols[1].AllowEditing = false;
                }
                else
                {
                    dgCustomGrid.C1Grid.Cols[0].AllowEditing = false;
                }

                if (dgCustomGrid.C1Grid.Cols.Count > 2)
                {
                    dgCustomGrid.C1Grid.Cols[2].Width = 0;
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
                _strSQL = "select isnull(im_item_Name,'') As Immunization  FROM IM_Mst order by im_item_Name ";
                oDiag = oDB.GetDataTable_Query(_strSQL);
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
                return null;
            }
            finally
            {
                oDB = null;
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
                _strSQL = " select isnull(dm_mst_criterianame,'') as DM ,dm_mst_id FROM dm_criteria_mst ORDER BY dm_mst_criterianame ";

                oDiag = oDB.GetDataTable_Query(_strSQL);
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
                return null;
            }
            finally
            {
                oDB = null;
            }


        }


        public Boolean  ChkSnomedSetting()
        {
            DataBaseLayer oDB = new DataBaseLayer();
        
            string _strSQL = "";
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
                        if (oDiag.Rows[0][0].ToString()  == "True")
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
                return false; 
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
                    _strSQL = " select sDescription from Category_Mst where sCategoryType='history' and  nCategoryID<>-1 ORDER BY sDescription ";
                }
                else
                {
                    if (bDM == true)
                    {
                        _strSQL = " select isnull(dm_mst_criterianame,'') as [DM_Criteria] FROM dm_criteria_mst ORDER BY dm_mst_criterianame ";
                    }
                }
                oDiag = oDB.GetDataTable_Query(_strSQL);
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
                return null;
            }
            finally
            {
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

                oDiag = oDB.GetDataTable_Query(_strSQL);
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
                return null;
            }
            finally
            {
                oDB = null;
            }
        }

        public DataTable FillDrugs()
        {
            DataBaseLayer oDB = new DataBaseLayer();
            string _strSQL = "";
            try
            {
                _strSQL = "select isnull(sDrugName,'') + ' : '+ isnull(sDosage,'')as [DrugName], sNDCCODE FROM Drugs_MST order by sDrugname ";
                oDiag = oDB.GetDataTable_Query(_strSQL);
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
                return null;
            }
            finally
            {
                oDB = null;
            }
        }


        // chetan added for filling Problemlist 
        public DataTable FillChiefComplaint()
        {
            DataBaseLayer oDB = new DataBaseLayer();
            string _strSQL = "";
            try
            {
                _strSQL = "select Distinct(ISNULL(sCheifComplaint,''))as  CheifComplaint   FROM Problemlist order by  CheifComplaint";
                oDiag = oDB.GetDataTable_Query(_strSQL);
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
                return null;
            }
            finally
            {
                oDB = null;
            }
        }



        public DataTable FillCPT()
        {
            DataBaseLayer oDB = new DataBaseLayer();
            string _strSQL = "";
            try
            {
                _strSQL = "select Distinct (isNull(rtrim(sCPTCode),'') + ' - ' + isNull(ltrim(sDescription),'')) as [CPT] from CPT_MST Where sCPTCode <>'' AND sDescription<>''";
                oCPT = oDB.GetDataTable_Query(_strSQL);
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
                return null;
            }
            finally
            {
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
                if (sCateg.ToLower().Contains("allergies"))
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
                oDiag = oDB.GetDataTable_Query(_strSQL);
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
                return null;
            }
            finally
            {
                oDB = null;
            }
        }

        public DataTable FillDiagnosis()
        {
            DataBaseLayer oDB = new DataBaseLayer();
            string _strSQL = "";
            try
            {
                _strSQL = "select Distinct (isNull(rtrim(sICD9Code),'') + ' - ' + isNull(ltrim(sDescription),'')) as [Diagnosis] from ICD9 Where sICD9Code <>'' AND sDescription<>''";
                oDiag = oDB.GetDataTable_Query(_strSQL);
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
                return null;
            }
            finally
            {
                oDB = null;
            }
        }

        private void Tblbtn_More_Click(object sender, EventArgs e)
        {
            if (RptName == "rptPatientList")
            {
                pnlPatientList.Visible = true;
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
                if(LstMedication.SelectedItems.Count > 0)
                {
                    if ((_reportName == "rptMedicationMU") || (_reportName == "rpteRx"))
                    {
                        DataTable dt = (DataTable)LstMedication.DataSource;

                        dt.Rows.RemoveAt(LstMedication.SelectedIndex);
                        LstMedication.DataSource = dt;
                    }
                    else
                    {
                        LstMedication.Items.Remove(LstMedication.SelectedItems[0]);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClearAllDrg_Click(object sender, EventArgs e)
        {
            try
            {
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

                MessageBox.Show("File Saved Successfully ....!", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Reports, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.ExportReport, RptName + " Usage Report Exported..", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message , gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error) ;
            }
        }

        private void btnBrowseDMSetup_Click(object sender, EventArgs e)
        {
            strLst = "PatDMCriteria";
            pnlcustomTask.Height = 400;
            pnlcustomTask.Width = 500;
            LoadUserGrid();
            pnlcustomTask.BringToFront();
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
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBPara = null;
            DataTable dtPatients = null;
            SSRSArgs _e = new SSRSArgs();
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



                _DMCriteriaIDs = "";

                for (int len = 0; len < lstdmsetup.Items.Count; len++)
                {
                    if (lstdmsetup.Items[len].ToString().Trim() != "")
                    {

                        _DMCriteriaIDs += "'" + lstdmsetup.Items[len].ToString().Replace("'","''")    + "'" + ",";
                    }
                }
                if (_DMCriteriaIDs.Length >= 2)
                {
                    _DMCriteriaIDs = _DMCriteriaIDs.Substring(0, _DMCriteriaIDs.Length - 1);
                }

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

                _e.sCriteriaID = _DMCriteriaIDs.Replace("|", ",");
                GenerateReport_Click(null, null, _e);

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
                    C1Patients.Cols.Fixed = 0;

                    C1Patients.Cols[COL_Select].Visible = true;
                    C1Patients.Cols[COL_PatientCode].Visible = true; // Patient Code //
                    C1Patients.Cols[COL_PatientID].Visible = false; // PatientID //
                    C1Patients.Cols[COL_PatientName].Visible = true; // Patient Name //
                    C1Patients.Cols[COL_DOB].Visible = true; // DOB //
                    C1Patients.Cols[COL_age].Visible = true; // Age //
                    C1Patients.Cols[COL_Gender].Visible = true; // Gender //
                    C1Patients.Cols[COL_ProviderID].Visible = false; // ProviderID //
                    C1Patients.Cols[COL_PRoviderName].Visible = false; // Provider Name //
                    C1Patients.Cols[COL_sCommunicationPreference].Visible = true; // Provider Name //
                    if ((cmbapptst.Text.Trim() != "All") && (ChkAppt.Checked == true))
                    {
                        C1Patients.Cols[COL_ApptStatus].Visible = true; //Appt Status //
                    }


                    C1Patients.Cols[COL_Select].DataType = typeof(bool);

                    C1Patients.SetData(0, COL_Select, "Select");
                    C1Patients.SetData(0, COL_PatientCode, "Patient Code");
                    C1Patients.SetData(0, COL_PatientName, "Patient Name");
                    C1Patients.SetData(0, COL_DOB, "DOB");
                    C1Patients.SetData(0, COL_age, "Age");
                    C1Patients.SetData(0, COL_Gender, "Gender");
                    C1Patients.SetData(0, COL_sCommunicationPreference, "Communication Preference");


                    int _width = C1Patients.Width;
                    C1Patients.Cols[COL_PatientCode].Width = (int)(_width * 0.2);
                    C1Patients.Cols[COL_PatientName].Width = (int)(_width * 0.2);
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
                    lstProblemList.DataSource = null;
                    lstProblemList.Items.Clear();
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
                oResult = oDB.GetDataTable_Query(_strSQL);
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
                return null;
            }
            finally
            {
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

            frmSelectProblem frm = new frmSelectProblem(GetGLOSMCONNECTIONSTR());
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowInTaskbar = false;
            frm.ShowDialog();
            string strSelectedProblem = frm.strProblem.ToString();
            if (strSelectedProblem != "" && !lstProblemList.Items.Contains(strSelectedProblem))
                lstProblemList.Items.Add(strSelectedProblem);


        }

        public string GetGLOSMCONNECTIONSTR()
        {
            DataBaseLayer oDB = new DataBaseLayer();
            DataTable Dt = new DataTable();
            string _strSQL = "";
            string _DbSnoMed = string.Empty;
            try
            {
                //' _strSQL = "select Distinct sICD9Code ,(isNull(sICD9Code,'') + ' : ' + isNull(sICD9Description,'')) as sICD9Display, (isNull(sICD9Code,'') + ' ' + isNull(sICD9Description,'')) as sICD9Values from ExamICD9CPT Where sICD9Code <>'' AND sICD9Description<>''"
                //'_strSQL = "select Distinct (isNull(sICD9Code,'') + ' : ' + isNull(sICD9Description,'')) as sICD9Display from ExamICD9CPT Where sICD9Code <>'' AND sICD9Description<>''"
                _strSQL = "select sSettingsValue from  settings where sSettingsName ='GLOSMCONNECTIONSTR'";
                Dt = oDB.GetDataTable_Query(_strSQL);
                if (Dt != null)
                {
                    if (Dt.Rows.Count > 0)
                        _DbSnoMed = Dt.Rows[0][0].ToString();

                }
                else
                {
                    _DbSnoMed = "";

                }
            }
            catch (Exception ex)
            {
                _DbSnoMed = "";
            }
            finally
            {
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
            pnlcustomTask.BringToFront();

        }
        private void btnBrowseDMPatientPrb_Click(object sender, EventArgs e)
        {
            frmSelectProblem frm = new frmSelectProblem(GetGLOSMCONNECTIONSTR());
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowInTaskbar = false;
            frm.ShowDialog();

            string strSelectedProblem = frm.strProblem.ToString();
            if (strSelectedProblem != "" && !lstDMProblemList.Items.Contains(strSelectedProblem))
                lstDMProblemList.Items.Add(strSelectedProblem);


        }

        private void btnSearchImmunization_Click(object sender, EventArgs e)
        {
            strLst = "PatientImmunization";
            //lstPatMedication.Items.Clear();
            LoadUserGrid();
            pnlcustomTask.BringToFront();
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
            pnlLabTestResult.Location = new System.Drawing.Point(699, 214);
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
                    lstdmsetup.DataSource = null;
                    lstdmsetup.Items.Clear();
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
            if (System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "([0-9\b])"))
                e.Handled = false;
            else
                e.Handled = true;

        }

        private void txtPatFrom_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "([0-9\b])"))
                e.Handled = false;
            else
                e.Handled = true;
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
