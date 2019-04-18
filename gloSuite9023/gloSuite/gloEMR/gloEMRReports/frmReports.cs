using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloAuditTrail;
using gloEMRGeneralLibrary.gloEMRDatabase;
using gloUserControlLibrary;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.IO;
using System.Text.RegularExpressions;

namespace gloEMRReports
{
    public partial class frmReports : Form
    {
        // Report Objects
        Rpt_PatientDiabetesStatus objrptPatientDiabSt = new Rpt_PatientDiabetesStatus();
        Rpt_PateintsBMI objrptPateintsBMI = new Rpt_PateintsBMI();
        RPT_PatientsBP objrptPateintsBP = new RPT_PatientsBP();
        RPTPatientAlerts objrptAlerts = new RPTPatientAlerts();
        Rpt_PatientHistory objrptPateintHist = new Rpt_PatientHistory();
        Rpt_PatientsHealthPlan objrptPateintDM = new Rpt_PatientsHealthPlan();
        RptPatientRx objrptPatientRx = new RptPatientRx();
        Rpt_dgDataPrint objrptdgDataPrint = new Rpt_dgDataPrint();

        //

        ////''For Age combobox
        string FOR_ALL = "For All";
        string FOR_AGE = "For Age";
        string FOR_LESSTHAN_AGE = "Less Than";
        string FOR_GREATERTHAN_AGE = "Greater Than";
        string FROMTO_AGE = "Between";
        ////'

        private CustomTask dgCustomGrid;
        System.Windows.Forms.Panel pnlcustomTask;
        private int Col_Check = 2;
        private int Col_Name = 0;
        //private int Col_Dosage = 1;
        private int Col_Count = 3;
        //string _TempRx;
        //DataTable oDiag = new DataTable();
        //DataTable oCPT = new DataTable();
        string strLst = "";
        Boolean bHistory = false;
        Boolean bDM = false;
        Boolean bNoData = false;
        DataTable dt=null;

        private DateTime mFromDate = DateTime.Now;
        private DateTime mToDate = DateTime.Now;
        private string _databaseconnectionstring = "";
        private DataTable _dt=null;
        private string _ReportNM = "";
        private string gstrMessageBoxCaption = "gloEMR";
        private string _UserName = String.Empty;
        private bool _DefaultPrinter = false;

        private bool _ICD10Transition = false;  //added for ICd10 implemantation

        private System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        [DllImport("user32.dll")]
        private static extern IntPtr GetActiveWindow();
        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern IntPtr GetParent(IntPtr hWnd);

        public DateTime FromDate
        {
            get { return mFromDate; }
            set { mFromDate = value; }
        }

        public DateTime ToDate
        {
            get { return mToDate; }
            set { mToDate = value; }
        }
        //changes made to show both ICD9 and 10 filter, for bugid 67217  v8020  
        public bool ICD10Transition //added for ICd10 implemantation
        {
            get { return _ICD10Transition; }
            set { _ICD10Transition = value; }
        }
        public frmReports(string databaseconnectionstring, string ReportNm)
        {
            _databaseconnectionstring = databaseconnectionstring;
            _ReportNM = ReportNm;


            //Sandip Darade  20100604
            #region " Retrieve UserName from AppSettings "
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

            try
            {
                if (appSettings["DefaultPrinter"] != null)
                {
                    if (appSettings["DefaultPrinter"] != "")
                    {
                        _DefaultPrinter = Convert.ToBoolean(appSettings["DefaultPrinter"]);
                    }
                    else
                    {
                        _DefaultPrinter = false;
                    }
                }
                else
                { _DefaultPrinter = false; }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ActivityModule.Exam, ActivityCategory.None, ActivityType.Open, ex.ToString(), ActivityOutCome.Failure);
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            #endregion
            InitializeComponent();
            //Attaching the event Handler            

        }

        public frmReports(DataTable dt1, string ReportNm)
        {
            _dt = dt1;
            _ReportNM = ReportNm;
            InitializeComponent();
            //Attaching the event Handler            

        }

        #region "Form Load"
        private void frmReports_Load(object sender, EventArgs e)
        {
            try
            {
                //
                if (_ReportNM == "auditreport")
                {
                    pnlDrugDiagnosis.Visible = false;
                    pnlProvider.Visible = false;
                    Panel2.Visible = false;
                }
                else
                {
                    //
                    Tblbtn_More.Visible = false;
                    pnlProvider.Visible = false;

                    //

                    //  Fill Provider          
                    Fill_Provider();

                    //  set Dates
                    dtpicFrom.Value = System.DateTime.Now.Date;
                    dtpicTo.Value = System.DateTime.Now.Date;
                    dtpicFrom.Checked = false;
                    dtpicTo.Enabled = false;
                    if (!dtpicFrom.Checked)
                    {
                        mFromDate = dtpicFrom.MinDate.Date;
                        //String.Format("{0:MM/dd/yyyy}",dtpicFrom.MinDate.ToString() );
                        mToDate = System.DateTime.Now.Date;
                        //String.Format("{0:MM/dd/yyyy}", System.DateTime.Now.Date.ToString()); 
                    }
                    else
                    {
                        mFromDate = dtpicFrom.Value;
                        //String.Format("{0:MM/dd/yyyy}", dtpicFrom.Value.ToString());
                        mToDate = dtpicTo.Value;
                        //String.Format("{0:MM/dd/yyyy}", dtpicTo.Value.ToString());
                    }
                }

                switch (_ReportNM.ToLower())
                {
                    case "alerts":
                        {
                            //form caption
                            this.Text = "Prescription Alert History";
                            //
                            Tblbtn_More.Visible = false;
                            pnlProvider.Visible = true;
                            // Set Age Criteria
                            SetAgeCr(false);
                            //
                            FillAlertReport(0, mFromDate, mToDate);
                            break;
                        }
                    case "bmi":
                        {
                            //form caption
                            this.Text = "Patient with Recorded BMI";
                            //
                            Tblbtn_More.Visible = false;
                            pnlProvider.Visible = false;
                            //
                            FillPatientBMIReport(0, 0, 0, 0, "", "", "");
                            break;
                        }
                    case "bp": //actually bp report
                        {
                            //form caption
                            this.Text = "Patient based on BP";
                            //
                            Tblbtn_More.Visible = true;
                            pnlProvider.Visible = true;
                            // Set Age Criteria
                            SetAgeCr(true);
                            //
                            //  FillPatientBMIReport(0, 0, 0, 0, "", "", "",0);
                            //lblDate.Dock = DockStyle.Left; 
                            lblDate.Text = "Visit Date : ";
                            pnlCheckBoxes.Visible = true;                            
                            pnlTreat.Visible = false;
                            pnlMed.Visible = false;
                            cmbAge.Text = "Greater Than";
                            cmbAgeFrom.Text = "18";
                            chkShowDeatal.Checked = true;
                            chkShowPieChart.Checked = true;
                            chkDiagnosis.Checked = true;
                            //int nyear = DateTime.Now.Year;
                            //nyear = nyear - 1;
                            //dtpicFrom.Value.Year = nyear;
                            dtpicFrom.Checked = true;
                            DateTime oneYearBefore = DateTime.Now.AddYears(-1);
                            dtpicFrom.Value = oneYearBefore;

                            FillBPReportPatient(0, "");
                            cmbProvider.Focus();
                            break;
                        }
                    case "diabetes":
                        {
                            #region "diabetes"
                            //form caption
                            this.Text = "Patient With ICD9 and Prescription";
                            //
                            Tblbtn_More.Visible = true;
                            pnlProvider.Visible = true;
                            //

                            // Set Age Criteria
                            SetAgeCr(true);
                            //
                            FillPatientDiabetesStatusReport(mFromDate, mToDate, 0, 0, 0, 0, "", "", "", 0);
                            #endregion "diabetes"
                            break;
                        }
                    case "history":
                        {
                            //form caption
                            this.Text = "Patient based on History";
                            //
                            Tblbtn_Hide.Visible = true;
                            pnlProvider.Visible = true;
                            pnlDrugDiagnosis.Visible = true;

                            // set Dates off
                            lblDate.Visible = false;
                            lblFrom.Visible = false;
                            lblTo.Visible = false;
                            dtpicFrom.Visible = false;
                            dtpicTo.Visible = false;
                            rbtnAllMedications.Visible = false;
                            rbtnPresByClinic.Visible = false;

                            //
                            // Set Age Criteria
                            SetAgeCr(false);
                            //
                            // Set Category labels
                            CRViewer.Visible = false;
                            pnlMessage.Visible = true;

                            lblMedication.Text = "Category: ";
                            this.ToolTip1.SetToolTip(this.btnBrowseDrug, "Browse Category");
                            this.ToolTip1.SetToolTip(this.btnClearDrug, "Clear Category");
                            this.ToolTip1.SetToolTip(this.BtnClearAllDrg, "Clear All Category");


                            lblDiagnosis.Text = "History Items: ";
                            this.ToolTip1.SetToolTip(this.btnBrowseDiag, "Browse History Items");
                            this.ToolTip1.SetToolTip(this.btnClearDiag, "Clear History Items");
                            this.ToolTip1.SetToolTip(this.btnClearAllDiag, "Clear All History Items");

                            bHistory = true;
                            //
                            pnlMed.Visible = true;
                            pnlDiag.Visible = true;
                            pnlTreat.Visible = false;
                            //FillPatientHistoryReport(0, "", "");
                       
                            break;
                        }
                    case "dm":
                        {
                            //form caption
                            this.Text = "Patient based on DM";
                            //
                            Tblbtn_More.Visible = true;
                            pnlProvider.Visible = false;

                            // set Dates off
                            lblDate.Visible = false;
                            lblFrom.Visible = false;
                            lblTo.Visible = false;
                            dtpicFrom.Visible = false;
                            dtpicTo.Visible = false;
                            rbtnAllMedications.Visible = false;
                            rbtnPresByClinic.Visible = false;
                            //
                            // Set Age Criteria
                            SetAgeCr(false);
                            //
                            // Set Category labels
                            lblMedication.Text = "DM Criteria:";

                            this.ToolTip1.SetToolTip(this.btnBrowseDrug, "Browse DM Criteria");
                            this.ToolTip1.SetToolTip(this.btnClearDrug, "Clear DM Criteria");
                            this.ToolTip1.SetToolTip(this.BtnClearAllDrg, "Clear All DM Criteria");

                            bDM = true;
                            //

                            pnlMed.Visible = true;
                            pnlDiag.Visible = false;
                            pnlTreat.Visible = false;
                            FillPatientHealthPlanReport("");
                            break;
                        }
                    case "rx": //beers list
                        {
                            #region "Rx"
                            //form caption
                            //this.Text = "Patient With Rx";
                            this.Text = "Patient Prescriptions";
                            //
                            Tblbtn_More.Visible = false;
                            Tblbtn_Hide.Visible = true;
                            pnlProvider.Visible = true;
                            //

                            // Set Age Criteria
                            SetAgeCr(true);
                            //
                            pnlMed.Visible = true;
                            pnlDrugDiagnosis.Visible = true; 
                            pnlDiag.Visible = false;
                            pnlTreat.Visible = false;
                            dtpicFrom.Checked = true;
                            CRViewer.Visible = false;
                            pnlMessage.Visible = true;
                            //added in 6020 for optimization because it takes too much time when we take all records
                            DateTime oneYearBefore = DateTime.Now.AddYears(-1);
                            dtpicFrom.Value = oneYearBefore;
                            mFromDate = dtpicFrom.Value;
                            //FillPatientRxReport(mFromDate, mToDate, 0, 0, 0, 0, "", 0);
                            cmbProvider.Focus();
                            #endregion "Rx"
                            break;
                        }
                    case "auditreport": //Audit Report Printing
                        {
                            this.Text = " Patient With Audit Report";
                            FillAuditReport(_dt);
                            break;
                        }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ActivityModule.Exam, ActivityCategory.None, ActivityType.Open, ex.ToString(), ActivityOutCome.Failure);
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion "Form Load"

        #region "Fill Dropdowns"
        public DataTable FillDrugs()
        {
            DataBaseLayer oDB = new DataBaseLayer();
            string _strSQL = "";
            try
            {
                //convert(varchar(18),nDrugsID)+' : '+
                //_strSQL = "select nDrugsID, sDrugName,isnull(sDosage,'') as sDosage FROM Drugs_MST order by sDrugname "
                _strSQL = "select isnull(sDrugName,'') + ' : '+ isnull(sDosage,'')as [DrugName] FROM Drugs_MST order by sDrugname ";
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
        // code added by pradeep 0n 12/06/2010
        public DataTable FillDrugs(Boolean blist)
        {
            DataBaseLayer oDB = new DataBaseLayer();
            string _strSQL = "";
            try
            {
                if (blist == true)
                {
                    // _strSQL = "SELECT DISTINCT isnull(Drugs_MST.sDrugName,'') + ' : '+ isnull(Drugs_MST.sDosage,'') FROM Medication INNER JOIN Drugs_MST ON Medication.sNDCCode = Drugs_MST.sNDCCode WHERE(Drugs_MST.nDrugType = 1)";
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
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return null;
            }
            finally
            {
                oDB.Dispose();
                oDB = null;
            }
        }

        //public DataTable FillDiagnosis()
        //{
        //    DataBaseLayer oDB = new DataBaseLayer();
        //    string _strSQL = "";

        //    try
        //    {
        //        //' _strSQL = "select Distinct sICD9Code ,(isNull(sICD9Code,'') + ' : ' + isNull(sICD9Description,'')) as sICD9Display, (isNull(sICD9Code,'') + ' ' + isNull(sICD9Description,'')) as sICD9Values from ExamICD9CPT Where sICD9Code <>'' AND sICD9Description<>''"
        //        //'_strSQL = "select Distinct (isNull(sICD9Code,'') + ' : ' + isNull(sICD9Description,'')) as sICD9Display from ExamICD9CPT Where sICD9Code <>'' AND sICD9Description<>''"
        //        _strSQL = "select Distinct (isNull(rtrim(sICD9Code),'') + ' - ' + isNull(ltrim(sDescription),'')) as [Diagnosis] from ICD9 Where sICD9Code <>'' AND sDescription<>''";
        //        oDiag = oDB.GetDataTable_Query(_strSQL);
        //        if (oDiag != null)
        //        {
        //            return oDiag;
        //        }
        //        else
        //        {

        //            return null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //        return null;
        //    }
        //    finally
        //    {
        //        oDB = null;
        //    }
        //}

        public DataTable FillCPT()
        {
            DataBaseLayer oDB = new DataBaseLayer();
            string _strSQL = "";
            try
            {
                //' _strSQL = "select Distinct sICD9Code ,(isNull(sICD9Code,'') + ' : ' + isNull(sICD9Description,'')) as sICD9Display, (isNull(sICD9Code,'') + ' ' + isNull(sICD9Description,'')) as sICD9Values from ExamICD9CPT Where sICD9Code <>'' AND sICD9Description<>''"
                //'_strSQL = "select Distinct (isNull(sICD9Code,'') + ' : ' + isNull(sICD9Description,'')) as sICD9Display from ExamICD9CPT Where sICD9Code <>'' AND sICD9Description<>''"
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
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                oDB.Dispose();
                oDB = null;
            }
        }

        public void Fill_Provider()
        {
            SqlConnection objCon = new SqlConnection(_databaseconnectionstring);
            SqlCommand objCmd = new SqlCommand();
            if (objCon.State == 0)
                objCon.Open();
            try
            {
                SqlDataReader objSQLDataReader;
                // _strSQL = "Select ISNULL(sFirstName,'')+Space(2)+ISNULL(sMiddleName,'')+Space(2)+ISNULL(sLastName,'') FROM Provider_MST WHERE bIsBlocked = 0 Order by sFirstName,sMiddleName,sLastName ";                
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "gsp_ScanProviderName";
                objCmd.Connection = objCon;
                objSQLDataReader = objCmd.ExecuteReader();
                //////////
                cmbProvider.Items.Clear();
                cmbProvider.Items.Add("All");
                //////////
                while (objSQLDataReader.Read())
                {
                    cmbProvider.Items.Add(objSQLDataReader.GetValue(0));
                }
                objSQLDataReader.Close();
                objSQLDataReader.Dispose();
                objSQLDataReader = null;
                if (cmbProvider.Items.Count > 0)
                {
                    cmbProvider.SelectedIndex = 0;
                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                throw ex;
            }
            finally
            {
                objCmd.Parameters.Clear();
                objCmd.Dispose();
                objCmd = null;
                
                objCon.Close();
                objCon.Dispose();
                objCon = null;
            }
        }

        //for History
        public DataTable FillCategory()
        {
            DataBaseLayer oDB = new DataBaseLayer();
            string _strSQL = "";
            try
            {
                if (bHistory == true)
                {
                    _strSQL = " select sDescription as Description from Category_Mst where sCategoryType='history' and  nCategoryID<>-1 ORDER BY sDescription ";
                }
                else
                {
                    if (bDM == true)
                    {
                        // _strSQL = " select dm_mst_criterianame,dm_mst_id FROM dm_criteria_mst ORDER BY dm_mst_criterianame ";
                        _strSQL = " select isnull(dm_mst_criterianame,'') as [DM_Criteria] FROM dm_criteria_mst   ORDER BY dm_mst_criterianame ";
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

        //for HistoryDetails
        public DataTable FillCategDetails(string sCateg)
        {
            DataBaseLayer oDB = new DataBaseLayer();
            string _strSQL = "";
            try
            {
                sCateg = sCateg.Replace(" or ", " or Category_MST.sDescription= ");


                //Developer:Yatin Bhagat
                //Date:6th Dec 2011
                //Bug ID/PRD Name/Salesforce Case:Bug Nio. 17418:Reports >> Advanced Reports >> History Report>> Application is showing an Allergic Drug for Each category
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

        #endregion


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
                objCmd.Parameters.Clear();
                objCmd.Dispose();
                objCmd = null;

                objCon.Close();
                objCon.Dispose();
                objCon = null;
            }
            return nProviderID;
        }
        #endregion


        #region "Set Age Criteria"
        private void SetAgeCr(Boolean AgeCr)
        {
            if (AgeCr == true)
            {
                cmbAgeFrom.Text = "";
                cmbAgeTo.Text = "";
                cmbAge.Items.Clear();
                cmbAge.Items.Add(FOR_ALL);
                cmbAge.Items.Add(FOR_AGE);
                cmbAge.Items.Add(FOR_LESSTHAN_AGE);
                cmbAge.Items.Add(FOR_GREATERTHAN_AGE);
                cmbAge.Items.Add(FROMTO_AGE);
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
            else
            {
                Lblage.Visible = false;
                lblAgeFrom.Visible = false;
                lblAgeTo.Visible = false;
                cmbAge.Visible = false;
                cmbAgeFrom.Visible = false;
                cmbAgeTo.Visible = false;
            }
        }
        #endregion "Set Age Criteria"


        private void tblbtnGenReport_Click(object sender, EventArgs e)
        {
            try
            {
                Int64 nProviderID = 0;
                System.DateTime Sdt = default(System.DateTime);
                System.DateTime Edt = default(System.DateTime);

                //' Get Provider            
                nProviderID = RetrieveProviderID(cmbProvider.Text);

                // Get date Range
                if (dtpicFrom.Checked == true)
                {
                    if (dtpicFrom.Value.Date > dtpicTo.Value.Date)
                    {
                        MessageBox.Show("Invalid Date Criteria, 'From date' should be less than or equal to 'To date'", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }

                if (!dtpicFrom.Checked)
                {
                    Sdt = dtpicFrom.MinDate.Date;
                    //String.Format("{0:MM/dd/yyyy}", dtpicFrom.MinDate.ToString());
                    Edt = System.DateTime.Now.Date;
                    //String.Format("{0:MM/dd/yyyy}", System.DateTime.Now.Date.ToString());
                }
                else
                {
                    Sdt = dtpicFrom.Value;
                    //String.Format("{0:MM/dd/yyyy}", dtpicFrom.Value.ToString());
                    Edt = dtpicTo.Value;
                    //String.Format("{0:MM/dd/yyyy}", dtpicTo.Value.ToString());
                }
                pnlMessage.Visible = false;
                CRViewer.Visible = true; 
                switch (_ReportNM.ToLower())
                {
                    case "diabetes":
                        {
                            #region "diabetes"
                            int nAgeType = 0;
                            int nAgeFrom = 0;
                            int nAgeTo = 0;
                            //string sAgeRange = "";
                            //string sAgeFrom = "";
                            //string sAgeTo = "";

                            //sAgeRange = cmbAge.Text;

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
                                nAgeFrom = Convert.ToInt32(cmbAgeFrom.Text);
                                nAgeType = 1;
                                if (cmbAgeFrom.Text.Trim() == "")
                                {
                                    MessageBox.Show("Please select the Age ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    cmbAgeFrom.Focus();
                                    return;
                                }
                            }
                            // for less than Given Age
                            else if (cmbAge.Text == FOR_LESSTHAN_AGE)
                            {
                                nAgeFrom = Convert.ToInt32(cmbAgeFrom.Text);
                                nAgeType = 2;
                                if (cmbAgeFrom.Text.Trim() == "")
                                {
                                    MessageBox.Show("Please select the Age ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    cmbAgeFrom.Focus();
                                    return;
                                }
                            }
                            // for Greater than Given Age
                            else if (cmbAge.Text == FOR_GREATERTHAN_AGE)
                            {
                                nAgeFrom = Convert.ToInt32(cmbAgeFrom.Text);
                                nAgeType = 3;
                                if (cmbAgeFrom.Text.Trim() == "")
                                {
                                    MessageBox.Show("Please select the Age ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    cmbAgeFrom.Focus();
                                    return;
                                }
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

                            /// 'Medication
                            string strDrugs = "";
                            int nDrugList;
                            string[] sDrug = new string[3];
                            char[] sep1 = new char[] { ':' };
                            char[] sep2 = new char[] { '-' };
                            string sval = "";
                            int i = 0;

                            LstMedication.Refresh();

                            // collect the selected data of check list
                            for (i = 0; i <= LstMedication.Items.Count - 1; i++)
                            {
                                sval = LstMedication.Items[i].ToString();
                                sDrug = sval.Split(sep1);
                                //LstMedication.Items[i].ToString().Split(":");
                                if (i == 0)
                                {
                                    strDrugs = "'" + sDrug[0].ToString().Trim().Replace("'", "''") + "'";
                                }
                                else
                                {
                                    strDrugs = strDrugs + " or '" + sDrug[0].ToString().Trim().Replace("'", "''") + "'";
                                }
                            }
                            if (rbtnAllMedications.Checked == true)
                            {
                                nDrugList = 1;
                            }
                            else
                            {
                                nDrugList = 2;
                            }
                            //' MessageBox.Show(chkMedication.Items(i).ToString)

                            //Diagnosis
                            string strDiagnosisICD9 = "";
                            StringBuilder sbDiagnosis = new StringBuilder();
                            string[] sDiag = new string[3];
                            // collect the selected data of check list
                            for (i = 0; i <= LstDiagnosis.Items.Count - 1; i++)
                            {
                                sval = LstDiagnosis.Items[i].ToString();
                                sDiag = sval.Split(sep2);
                                if (i == 0)
                                {
                                    sbDiagnosis.Append("'");
                                    sbDiagnosis.Append(sDiag[0].ToString());
                                    sbDiagnosis.Append("'");
                                }
                                else
                                {
                                    sbDiagnosis.Append(" or '");
                                    sbDiagnosis.Append(sDiag[0].ToString());
                                    sbDiagnosis.Append("'");
                                }
                            }
                            strDiagnosisICD9 = sbDiagnosis.ToString();

                            //CPT
                            string strCPT = "";
                            StringBuilder sbCPT = new StringBuilder();
                            string[] sCPT = new string[3];
                            // collect the selected data of check list
                            for (i = 0; i <= LstTreatment.Items.Count - 1; i++)
                            {
                                sval = LstTreatment.Items[i].ToString();
                                sCPT = sval.Split(sep2);
                                if (i == 0)
                                {
                                    sbCPT.Append("'");
                                    sbCPT.Append(sCPT[0].ToString());
                                    sbCPT.Append("'");
                                }
                                else
                                {
                                    sbCPT.Append(" or '");
                                    sbCPT.Append(sCPT[0].ToString());
                                    sbCPT.Append("'");
                                }
                            }
                            strCPT = sbCPT.ToString();

                            FillPatientDiabetesStatusReport(Sdt, Edt, nProviderID, nAgeType, nAgeFrom, nAgeTo, strDiagnosisICD9, strCPT, strDrugs, nDrugList);
                            #endregion "diabetes"
                            break;
                        }
                    case "bmi":
                        {
                            FillPatientBMIReport(0, 0, 0, 0, "", "", "");
                            break;
                        }
                    case "bp":
                        {
                            #region "bp"
                            //Age                           
                          //  int nAgeType = 0;
                            int nAgeFrom = 0;
                            int nAgeTo = 0;

                            //'For No Age Mentioned
                            if (cmbAge.Text.Trim() == "" || cmbAge.Text == FOR_ALL)
                            {
                               // nAgeType = 0;
                                nAgeFrom = 0;
                                nAgeTo = 0;
                            }
                            //' for particular Age
                            else if (cmbAge.Text == FOR_AGE)
                            {
                                nAgeFrom = Convert.ToInt32(cmbAgeFrom.Text);
                                //nAgeType = 1;
                                if (cmbAgeFrom.Text.Trim() == "")
                                {
                                    MessageBox.Show("Please select the Age ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    cmbAgeFrom.Focus();
                                    return;
                                }
                            }
                            // for less than Given Age
                            else if (cmbAge.Text == FOR_LESSTHAN_AGE)
                            {
                                nAgeFrom = Convert.ToInt32(cmbAgeFrom.Text);
                                //nAgeType = 2;
                                if (cmbAgeFrom.Text.Trim() == "")
                                {
                                    MessageBox.Show("Please select the Age ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    cmbAgeFrom.Focus();
                                    return;
                                }
                            }
                            // for Greater than Given Age
                            else if (cmbAge.Text == FOR_GREATERTHAN_AGE)
                            {
                                nAgeFrom = Convert.ToInt32(cmbAgeFrom.Text);
                                //nAgeType = 3;
                                if (cmbAgeFrom.Text.Trim() == "")
                                {
                                    MessageBox.Show("Please select the Age ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    cmbAgeFrom.Focus();
                                    return;
                                }
                            }
                            // for given age range
                            else if (cmbAge.Text == FROMTO_AGE)
                            {
                                //nAgeType = 4;
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

                            /// 'Medication
                            string strDrugs = "";
                       //     int nDrugList;
                            string[] sDrug = new string[3];
                            char[] sep1 = new char[] { ':' };
                            char[] sep2 = new char[] { '-' };
                            string sval = "";
                            int i = 0;

                            LstMedication.Refresh();

                            // collect the selected data of check list
                            for (i = 0; i <= LstMedication.Items.Count - 1; i++)
                            {
                                sval = LstMedication.Items[i].ToString();
                                sDrug = sval.Split(sep1);
                                //LstMedication.Items[i].ToString().Split(":");
                                if (i == 0)
                                {
                                    strDrugs = "'" + sDrug[0].ToString().Trim() + "'";
                                }
                                else
                                {
                                    strDrugs = strDrugs + " or '" + sDrug[0].ToString().Trim() + "'";
                                }
                            }
                            if (rbtnAllMedications.Checked == true)
                            {
                                //nDrugList = 1;
                            }
                            else
                            {
                                //nDrugList = 2;
                            }
                            //' MessageBox.Show(chkMedication.Items(i).ToString)

                            //Diagnosis
                            string strDiagnosisICD9 = "";
                            StringBuilder sbDiagnosis = new StringBuilder();
                            string[] sDiag = new string[3];
                            // collect the selected data of check list
                            for (i = 0; i <= LstDiagnosis.Items.Count - 1; i++)
                            {
                                sval = LstDiagnosis.Items[i].ToString();
                                sDiag = sval.Split(sep2);
                                if (i == 0)
                                {
                                    sbDiagnosis.Append("'");
                                    sbDiagnosis.Append(sDiag[0].ToString());
                                    sbDiagnosis.Append("'");
                                }
                                else
                                {
                                    sbDiagnosis.Append(" or '");
                                    sbDiagnosis.Append(sDiag[0].ToString());
                                    sbDiagnosis.Append("'");
                                }
                            }
                            strDiagnosisICD9 = sbDiagnosis.ToString();

                            //CPT
                            string strCPT = "";
                            StringBuilder sbCPT = new StringBuilder();
                            string[] sCPT = new string[3];
                            // collect the selected data of check list
                            for (i = 0; i <= LstTreatment.Items.Count - 1; i++)
                            {
                                sval = LstTreatment.Items[i].ToString();
                                sCPT = sval.Split(sep2);
                                if (i == 0)
                                {
                                    sbCPT.Append("'");
                                    sbCPT.Append(sCPT[0].ToString());
                                    sbCPT.Append("'");
                                }
                                else
                                {
                                    sbCPT.Append(" or '");
                                    sbCPT.Append(sCPT[0].ToString());
                                    sbCPT.Append("'");
                                }
                            }
                            strCPT = sbCPT.ToString();

                            //
                            // FillPatientBMIReport(nProviderID, nAgeType, nAgeFrom, nAgeTo, strDiagnosisICD9, strCPT, strDrugs, nDrugList);


                            string strDiagForBP = "";
                            string[] sDiagForBP = new string[3];
                            char[] sep_1 = new char[] { '-' };
                            string sva_l = "";
                            i = 0;
                            // string[] sDrug = new string[3];
                            for (i = 0; i <= LstDiagnosis.Items.Count - 1; i++)
                            {
                                sva_l = LstDiagnosis.Items[i].ToString();
                                sDiagForBP = sva_l.Split(sep_1);
                                //LstMedication.Items[i].ToString().Split(":");
                                if (i == 0)
                                {
                                    strDiagForBP = "'" + sDiagForBP[0].ToString().Trim().Replace("'", "''") + "'";
                                }
                                else
                                {
                                    strDiagForBP = strDiagForBP + " or '" + sDiagForBP[0].ToString().Trim().Replace("'", "''") + "'";
                                }


                            }
                            //FillPatientBMIReport(nProviderID, nAgeType, nAgeFrom, nAgeTo, strDiagnosisICD9, strCPT, strDrugs);
                            FillBPReportPatient(nProviderID, strDiagForBP);

                            #endregion "bp"
                            break;
                        }
                    case "alerts":
                        {
                            FillAlertReport(nProviderID, Sdt, Edt);
                            break;
                        }
                    case "history":
                        {
                            if (LstMedication.Items.Count <= 0 && LstDiagnosis.Items.Count > 0)
                            {
                                MessageBox.Show("Please select Category first", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LstMedication.Focus();
                                return;
                            }
                            #region "History"
                            // 'Get Categ
                            string strHisCateg = "";
                            int i = 0;
                            LstMedication.Refresh();
                            // collect the selected data of check list
                            for (i = 0; i <= LstMedication.Items.Count - 1; i++)
                            {
                                if (i == 0)
                                {
                                    strHisCateg = "'" + LstMedication.Items[i].ToString().Trim() + "'";
                                }
                                else
                                {
                                    strHisCateg = strHisCateg + " or '" + LstMedication.Items[i].ToString().Trim() + "'";
                                }
                            }

                            // 'Get Categ Details
                            string strHistDet = "";
                            LstDiagnosis.Refresh();
                            // collect the selected data of check list
                            for (i = 0; i <= LstDiagnosis.Items.Count - 1; i++)
                            {
                                if (i == 0)
                                {
                                    strHistDet = "'" + LstDiagnosis.Items[i].ToString().Trim() + "'";
                                }
                                else
                                {
                                    strHistDet = strHistDet + " or '" + LstDiagnosis.Items[i].ToString().Trim() + "'";
                                }
                            }
                            FillPatientHistoryReport(nProviderID, strHisCateg, strHistDet);
                            #endregion "History"
                            break;
                        }
                    case "dm":
                        {
                            // 'Get Categ
                            string strDM = "";
                            int i = 0;
                            LstMedication.Refresh();
                            // collect the selected data of check list
                            for (i = 0; i <= LstMedication.Items.Count - 1; i++)
                            {
                                if (i == 0)
                                {
                                    strDM = "'" + LstMedication.Items[i].ToString().Trim().Replace("'", "''") + "'";
                                }
                                else
                                {
                                    strDM = strDM + " or '" + LstMedication.Items[i].ToString().Trim().Replace("'", "''") + "'";
                                }
                            }
                            FillPatientHealthPlanReport(strDM);
                            break;
                        }
                    case "rx": //beers list
                        {
                            #region "Rx"
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

                            /// 'Medication

                            string strDrugs = "";
                            int nDrugList;
                            string[] sDrug = new string[3];
                            string sval = "";
                            int i = 0;

                            LstMedication.Refresh();

                            // collect the selected data of check list
                            for (i = 0; i <= LstMedication.Items.Count - 1; i++)
                            {
                                sval = LstMedication.Items[i].ToString();
                                sDrug = sval.Split(':');
                                //LstMedication.Items[i].ToString().Split(":");
                                if (i == 0)
                                {
                                    strDrugs = "'" + sDrug[0].ToString().Trim() + "'";
                                }
                                else
                                {
                                    strDrugs = strDrugs + " or '" + sDrug[0].ToString().Trim() + "'";
                                }
                            }
                            if (rbtnAllMedications.Checked == true)
                            {
                                nDrugList = 1;
                            }
                            else
                            {
                                nDrugList = 2;
                            }

                            FillPatientRxReport(Sdt, Edt, nProviderID, nAgeType, nAgeFrom, nAgeTo, strDrugs, nDrugList);
                            #endregion "Rx"
                            break;
                        }

                }
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Exam, ActivityCategory.None, ActivityType.View, "Patient's Diabetes Status Report viewed..", 0, 0, 0, ActivityOutCome.Success);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ActivityModule.Exam, ActivityCategory.None, ActivityType.View, ex.ToString(), ActivityOutCome.Failure);
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #region "Fill Reports"
        private void FillPatientDiabetesStatusReport(System.DateTime StartDT, System.DateTime EndDT, Int64 nproviderID, Int64 ageTp, Int64 age1, Int64 age2, string sDiag, string sCPT, string sDrugs, Int32 sDrugList)
        {
            DsCCHITRPTs dsCCHIT = new DsCCHITRPTs();
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();

            try
            {

                oConnection.ConnectionString = _databaseconnectionstring;
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = "Rpt_PatientDiabetesStatus";
                _sqlcommand.Connection = oConnection;

                //'// ** Add Param

                _sqlcommand.Parameters.Add("@nProviderID", System.Data.SqlDbType.BigInt);
                _sqlcommand.Parameters["@nProviderID"].Value = nproviderID;

                _sqlcommand.Parameters.Add("@AgeType", System.Data.SqlDbType.Int);
                _sqlcommand.Parameters["@AgeType"].Value = ageTp;

                _sqlcommand.Parameters.Add("@nAgeFrom", System.Data.SqlDbType.Int);
                _sqlcommand.Parameters["@nAgeFrom"].Value = age1;

                _sqlcommand.Parameters.Add("@nAgeTo", System.Data.SqlDbType.Int);
                _sqlcommand.Parameters["@nAgeTo"].Value = age2;

                _sqlcommand.Parameters.Add("@sDiagnosis", System.Data.SqlDbType.VarChar);
                _sqlcommand.Parameters["@sDiagnosis"].Value = sDiag;

                _sqlcommand.Parameters.Add("@sCPT", System.Data.SqlDbType.VarChar);
                _sqlcommand.Parameters["@sCPT"].Value = sCPT;

                _sqlcommand.Parameters.Add("@sDrugs", System.Data.SqlDbType.VarChar);
                _sqlcommand.Parameters["@sDrugs"].Value = sDrugs;

                _sqlcommand.Parameters.Add("@StartDt", System.Data.SqlDbType.DateTime);
                _sqlcommand.Parameters["@StartDt"].Value = StartDT;

                _sqlcommand.Parameters.Add("@EndDt", System.Data.SqlDbType.DateTime);
                _sqlcommand.Parameters["@EndDt"].Value = EndDT;

                _sqlcommand.Parameters.Add("@drugList", System.Data.SqlDbType.Int);
                _sqlcommand.Parameters["@drugList"].Value = sDrugList;

                SqlDataAdapter da = new SqlDataAdapter(_sqlcommand);
                //try
                //{
                //    da.Fill(dsCCHIT, "dt_PatientBP");
                //}
                //catch
                {
                    da.Fill(dsCCHIT, "dt_PatientDiabetesST");
                }

                da.Dispose();

                if (dsCCHIT.dt_PatientDiabetesST.Rows.Count == 0)
                {
                    bNoData = true;
                }
                else
                {
                    bNoData = false;
                }

                objrptPatientDiabSt.SetDataSource(dsCCHIT);

                //Binds the Report to the Report viewer
                // objrptPatientDiabSt.Refresh();
                //Commenetd on 20100503 by Mayuri to hide Group Tree
                //CRViewer.DisplayGroupTree = true;
                CRViewer.ShowGroupTreeButton = false;
                CRViewer.ReportSource = objrptPatientDiabSt;



                objrptPatientDiabSt.SetParameterValue("UserName", _UserName);//Sandip Darade 20100604


                //Sandip Darade 20100608
                string _strAge = cmbAge.Text;
                if (cmbAgeFrom.Visible == true)
                {
                    _strAge += " " + cmbAgeFrom.Text.Trim();
                }
                if (cmbAgeTo.Visible == true)
                {
                    _strAge += " To " + cmbAgeTo.Text.Trim();
                }
                //Sandip Darade 20100608

                string _strDate = string.Empty;
                if (dtpicFrom.Checked == true)
                {
                    _strDate = dtpicFrom.Value.Date.ToString("MM/dd/yyyy") + "  To  " + dtpicTo.Value.Date.ToString("MM/dd/yyyy");
                }
                else
                {
                    _strDate = "All";
                }

                objrptPatientDiabSt.SetParameterValue("Date", _strDate);//Sandip Darade 20100608
                objrptPatientDiabSt.SetParameterValue("Age", _strAge);//Sandip Darade 20100608
                objrptPatientDiabSt.SetParameterValue("Provider", cmbProvider.Text.Trim());//Sandip Darade 20100608

                foreach (CrystalDecisions.Shared.ParameterField pf in objrptPatientDiabSt.ParameterFields)
                {
                    pf.HasCurrentValue = true;
                }
                CRViewer.ParameterFieldInfo = objrptPatientDiabSt.ParameterFields;

            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                _sqlcommand.Parameters.Clear();
                _sqlcommand.Dispose();
                _sqlcommand = null;

                oConnection.Close();
                oConnection.Dispose();
                oConnection = null;
            }
        }

        private void FillAuditReport(DataTable dtAuditReports)
        //System.DateTime dtFrom, System.DateTime dtTo, string sCategory, string strUser, [System.Runtime.InteropServices.OptionalAttribute, System.Runtime.InteropServices.DefaultParameterValueAttribute("")],[System.Runtime.InteropServices.OptionalAttribute, System.Runtime.InteropServices.DefaultParameterValueAttribute("")], [System.Runtime.InteropServices.OptionalAttribute, System.Runtime.InteropServices.DefaultParameterValueAttribute("")]) 
        {

            //SqlConnection objcon = new SqlConnection();
            //SqlCommand objcmd = new SqlCommand();

            //DataSet dsAuditReports = new DataSet();
            //objCon.ConnectionString = _databaseconnectionstring;
            //SqlDataReader objSQLDataReader = new SqlDataReader();

            //try
            //{
            //    objcmd.CommandType = CommandType.StoredProcedure;
            //    objcmd.CommandText = "gsp_ScanAuditTrails";
            //    objcmd.Parameters.Clear();

            //    SqlParameter objParaFromDate = new SqlParameter();
            //    objParaFromDate.ParameterName = "@FromDate";
            //    objParaFromDate.Value = dtFrom.Date;
            //    objParaFromDate.Direction = ParameterDirection.Input;
            //    objParaFromDate.SqlDbType = SqlDbType.DateTime;

            //    objcmd.Parameters.Add(objParaFromDate);

            //    SqlParameter objParaToDate = new SqlParameter();
            //    objParaToDate.ParameterName = "@ToDate";
            //    objParaToDate.Value = dtTo.Date;
            //    objParaToDate.Direction = ParameterDirection.Input;
            //    objParaToDate.SqlDbType = SqlDbType.DateTime;

            //    objcmd.Parameters.Add(objParaToDate);

            //    SqlParameter objParaCategory = new SqlParameter();
            //    objParaCategory.ParameterName = "@Category";
            //    objParaCategory.Value = sCategory;
            //    objParaCategory.Direction = ParameterDirection.Input;
            //    objParaCategory.SqlDbType = SqlDbType.VarChar;

            //    objcmd.Parameters.Add(objParaCategory);

            //    SqlParameter objParaUser = new SqlParameter();
            //    objParaUser.ParameterName = "@UserName";
            //    objParaUser.Value = strUser;
            //    objParaUser.Direction = ParameterDirection.Input;
            //    objParaUser.SqlDbType = SqlDbType.VarChar;

            //    objcmd.Parameters.Add(objParaUser);

            //   if (!string.IsNullOrEmpty(nPatientID))
            //     { 
            //      SqlParameter objParaPatientID = new SqlParameter(); 
            //       { 
            //        objParaPatientID.ParameterName = "@PatientCode"; 
            //        objParaPatientID.Value = nPatientID; 
            //        objParaPatientID.Direction = ParameterDirection.Input; 
            //        objParaPatientID.SqlDbType = SqlDbType.VarChar; 
            //       } 
            //      objCmd.Parameters.Add(objParaPatientID); 
            //    } 

            //    if (!string.IsNullOrEmpty(strPatientFirstName))
            //      {
            //       SqlParameter objParaPatientFirstName = new SqlParameter(); 
            //        { 
            //         objParaPatientFirstName.ParameterName = "@PatientFirstName"; 
            //         objParaPatientFirstName.Value = strPatientFirstName; 
            //         objParaPatientFirstName.Direction = ParameterDirection.Input; 
            //         objParaPatientFirstName.SqlDbType = SqlDbType.VarChar; 
            //        } 
            //       objCmd.Parameters.Add(objParaPatientFirstName); 
            //     } 

            //    if (!string.IsNullOrEmpty(strPatientLastName))
            //      { 
            //       SqlParameter objParaPatientLastName = new SqlParameter(); 
            //        { 
            //        objParaPatientLastName.ParameterName = "@PatientLastName"; 
            //        objParaPatientLastName.Value = strPatientLastName; 
            //        objParaPatientLastName.Direction = ParameterDirection.Input; 
            //        objParaPatientLastName.SqlDbType = SqlDbType.VarChar; 
            //        } 
            //      objCmd.Parameters.Add(objParaPatientLastName); 
            //      } 

            // objCmd.Connection = objCon; 
            // objCon.Open(); 
            try
            {
                //DsCCHITRPTs dsCCHIT = new DsCCHITRPTs();
                ////dsAuditReports=dt1 ;
                //SqlDataAdapter objDA = new SqlDataAdapter();
                //objCon.Close(); 

                //try
                //{
                //    objDA.Fill(dsCCHIT, "dsAuditReports");
                //}
                //catch
                //{
                //    objDA.Fill(dsCCHIT, "dsAuditReports");

                //}
                //dsCCHIT.dtAuditReports.Load(); 
                //objCon = null;


                objrptdgDataPrint.SetDataSource(dtAuditReports);
                //Binds the Report to the Report viewer
                objrptdgDataPrint.Refresh();
                objrptdgDataPrint.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape;
                objrptdgDataPrint.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperLetter;

                //objrptdgDataPrint.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSource.Auto.ToString();

                // objrptdgDataPrint.PrintOptions.PageContentWidth=c



                //objrptdgDataPrint.PrintOptions.PageMargins.rightMargin.ToString();

                //objrptdgDataPrint.PrintOptions.CustomPaperSource.SourceName.Length.ToString(); 

                CRViewer.ReportSource = objrptdgDataPrint;

                //CRViewer.PrintReport();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "gloEMR Admin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {

            }

        }



        private void FillPatientBMIReport(Int64 nproviderID, Int64 ageTp, Int64 age1, Int64 age2, string sDiag, string sCPT, string sDrugs)
        {

            DsCCHITRPTs DsCCHITRPT = new DsCCHITRPTs();
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();


            try
            {
                oConnection.ConnectionString = _databaseconnectionstring;
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = "Rpt_PatientBMI";
                _sqlcommand.Connection = oConnection;

                if (_ReportNM == "bp")
                {
                    // ** Add Param
                    _sqlcommand.Parameters.Add("@nProviderID", System.Data.SqlDbType.BigInt);
                    _sqlcommand.Parameters["@nProviderID"].Value = nproviderID;

                    _sqlcommand.Parameters.Add("@AgeType", System.Data.SqlDbType.Int);
                    _sqlcommand.Parameters["@AgeType"].Value = ageTp;

                    _sqlcommand.Parameters.Add("@nAgeFrom", System.Data.SqlDbType.Int);
                    _sqlcommand.Parameters["@nAgeFrom"].Value = age1;

                    _sqlcommand.Parameters.Add("@nAgeTo", System.Data.SqlDbType.Int);
                    _sqlcommand.Parameters["@nAgeTo"].Value = age2;

                    _sqlcommand.Parameters.Add("@sDiagnosis", System.Data.SqlDbType.VarChar);
                    _sqlcommand.Parameters["@sDiagnosis"].Value = sDiag;

                    _sqlcommand.Parameters.Add("@sReport", System.Data.SqlDbType.VarChar);
                    _sqlcommand.Parameters["@sReport"].Value = _ReportNM;

                    _sqlcommand.Parameters.Add("@sCPT", System.Data.SqlDbType.VarChar);
                    _sqlcommand.Parameters["@sCPT"].Value = sCPT;

                    _sqlcommand.Parameters.Add("@sDrugs", System.Data.SqlDbType.VarChar);
                    _sqlcommand.Parameters["@sDrugs"].Value = sDrugs;

                    ////code added by pradeep on 15/06/2010
                    //_sqlcommand.Parameters.Add("@druglist", System.Data.SqlDbType.Int);
                    //_sqlcommand.Parameters["@druglist"].Value = drugList;

                }


                {
                    Boolean bErr = false;
                    SqlDataAdapter da = new SqlDataAdapter(_sqlcommand);
                    try
                    {
                        da.Fill(DsCCHITRPT, "dt_PatientBP");
                    }
                    catch
                    {
                        da.Fill(DsCCHITRPT, "dt_PatientBMI");
                        bErr = true;
                    }
                    finally
                    {
                        if (bErr == false)
                        {
                            da.Dispose();
                            da = null;
                            da = new SqlDataAdapter(_sqlcommand);
                            da.Fill(DsCCHITRPT, "dt_PatientBMI");
                        }
                    }


                    da.Dispose();
                    da = null;
                }
                if (DsCCHITRPT.dt_PatientBMI.Rows.Count == 0)
                {
                    bNoData = true;
                }
                else
                {
                    bNoData = false;
                }
                if (_ReportNM == "bmi")
                {
                    objrptPateintsBMI.SetDataSource(DsCCHITRPT);
                    //Binds the Report to the Report viewer
                    objrptPateintsBMI.Refresh();
                    CRViewer.ReportSource = objrptPateintsBMI;
                    objrptPateintsBMI.SetParameterValue("UserName", _UserName);
                }
                else
                {
                    objrptPateintsBP.SetDataSource(DsCCHITRPT);

                    //Binds the Report to the Report viewer
                    objrptPateintsBP.Refresh();
                    CRViewer.ReportSource = objrptPateintsBP;
                    objrptPateintsBP.SetParameterValue("UserName", _UserName);//Sandip Darade 20100604


                    //Sandip Darade 20100608
                    string _strAge = cmbAge.Text;
                    if (cmbAgeFrom.Visible == true)
                    {
                        _strAge += " " + cmbAgeFrom.Text.Trim();
                    }
                    if (cmbAgeTo.Visible == true)
                    {
                        _strAge += " To " + cmbAgeTo.Text.Trim();
                    }

                    objrptPateintsBP.SetParameterValue("Age", _strAge);//Sandip Darade 20100608
                    objrptPateintsBP.SetParameterValue("Provider", cmbProvider.Text.Trim());//Sandip Darade 20100608
                }
                // 

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                DsCCHITRPT = null;
                _sqlcommand.Parameters.Clear();
                _sqlcommand.Dispose();
                _sqlcommand = null;
                oConnection.Close();
                oConnection.Dispose();
                oConnection = null;

            }
        }

        private void FillAlertReport(Int64 nproviderID, System.DateTime stDate, System.DateTime endDate)
        {
            DsCCHITRPTs DsCCHITRPT = new DsCCHITRPTs();
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();
            try
            {
                oConnection.ConnectionString = _databaseconnectionstring;
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = "Rpt_Alerts";
                _sqlcommand.Connection = oConnection;

                // ** Add Param
                if (nproviderID != 0)
                {
                    _sqlcommand.Parameters.Add("@nProviderID", System.Data.SqlDbType.BigInt);
                    _sqlcommand.Parameters["@nProviderID"].Value = nproviderID;

                }

                _sqlcommand.Parameters.Add("@StartDt", System.Data.SqlDbType.DateTime);
                _sqlcommand.Parameters["@StartDt"].Value = stDate;

                _sqlcommand.Parameters.Add("@EndDt", System.Data.SqlDbType.DateTime);
                _sqlcommand.Parameters["@EndDt"].Value = endDate;


                SqlDataAdapter da = new SqlDataAdapter(_sqlcommand);
                da.Fill(DsCCHITRPT, "dt_Alerts");


                da.Dispose();

                if (DsCCHITRPT.dt_Alerts.Rows.Count == 0)
                {
                    bNoData = true;
                }
                else
                {
                    bNoData = false;
                }

                //objrptPatientBMI.ReportHeaderSection1.ReportObjects.items("Text5").text = " Patients with Recorded BMI (%)";
                objrptAlerts.SetDataSource(DsCCHITRPT);

                //Binds the Report to the Report viewer
                //objrptAlerts.Refresh();
                CRViewer.ReportSource = objrptAlerts;
                objrptAlerts.SetParameterValue("UserName", _UserName);//Sandip Darade 20100604

                //Sandip Darade 20100608
                string _strDate = string.Empty;
                if (dtpicFrom.Checked == true)
                {
                    _strDate = dtpicFrom.Value.Date.ToString("MM/dd/yyyy") + "  To  " + dtpicTo.Value.Date.ToString("MM/dd/yyyy");
                }
                else
                {
                    _strDate = "All";
                }

                objrptAlerts.SetParameterValue("Date", _strDate);//Sandip Darade 20100608
                objrptAlerts.SetParameterValue("Provider", cmbProvider.Text.Trim());//Sandip Darade 20100608

                foreach (CrystalDecisions.Shared.ParameterField pf in objrptAlerts.ParameterFields)
                {
                    pf.HasCurrentValue = true;
                }
                CRViewer.ParameterFieldInfo = objrptAlerts.ParameterFields;

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                _sqlcommand.Parameters.Clear();
                _sqlcommand.Dispose();
                _sqlcommand = null;
                oConnection.Close();
                oConnection.Dispose();
                oConnection = null;
            }
        }


        private void FillPatientHistoryReport(Int64 nproviderID, string sHistCat, string sHistItem)
        {

            DsCCHITRPTs DsCCHITRPT = new DsCCHITRPTs();
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();

            try
            {
                oConnection.ConnectionString = _databaseconnectionstring;
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = "Rpt_PatientHistory";
                _sqlcommand.Connection = oConnection;

                // ** Add Param
                _sqlcommand.Parameters.Add("@nProviderID", System.Data.SqlDbType.BigInt);
                _sqlcommand.Parameters["@nProviderID"].Value = nproviderID;

                _sqlcommand.Parameters.Add("@sHistCat", System.Data.SqlDbType.VarChar);
                _sqlcommand.Parameters["@sHistCat"].Value = sHistCat;

                _sqlcommand.Parameters.Add("@sHistItem", System.Data.SqlDbType.VarChar);
                _sqlcommand.Parameters["@sHistItem"].Value = sHistItem;

                Boolean bErr = false;
                SqlDataAdapter da = new SqlDataAdapter(_sqlcommand);

                try
                {
                    da.Fill(DsCCHITRPT, "dt_PatientBP");
                    // da.Fill(DsCCHITRPT, "dt_PatientBP");
                }
                catch
                {
                    da.Fill(DsCCHITRPT, "dt_PatientHistory");
                    bErr = true;
                }
                finally
                {
                    if (bErr == false)
                    {
                        da.Dispose();
                        da = null;
                        da = new SqlDataAdapter(_sqlcommand);
                        da.Fill(DsCCHITRPT, "dt_PatientHistory");
                    }
                }

                da.Dispose();

                if (DsCCHITRPT.dt_PatientHistory.Rows.Count == 0)
                {
                    bNoData = true;
                }
                else
                {
                    bNoData = false;
                }
                objrptPateintHist.SetDataSource(DsCCHITRPT);
                //Binds the Report to the Report viewer
                objrptPateintHist.Refresh();
                //CRViewer.DisplayGroupTree = false;
                CRViewer.ShowGroupTreeButton = false;


                CRViewer.ReportSource = objrptPateintHist;
                objrptPateintHist.SetParameterValue(0, _UserName);//Sandip Darade 20100607

                // 

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                DsCCHITRPT = null;
                _sqlcommand.Parameters.Clear();
                _sqlcommand.Dispose();
                _sqlcommand = null;
                oConnection.Close();
                oConnection.Dispose();
                oConnection = null;
            }
        }

        private void FillPatientHealthPlanReport(string sHealthPn)
        {

            //DsCCHITRPTs DsCCHITRPT = new DsCCHITRPTs();
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();

            try
            {
                oConnection.ConnectionString = _databaseconnectionstring;
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = "Rpt_PatientDM";
                _sqlcommand.Connection = oConnection;

                // ** Add Param
                _sqlcommand.Parameters.Add("@sHealthPlan", System.Data.SqlDbType.VarChar);
                _sqlcommand.Parameters["@sHealthPlan"].Value = sHealthPn;

                SqlDataAdapter da = new SqlDataAdapter(_sqlcommand);

               DataSet ds = new DataSet ();
               ds.Tables.Add("dt_PatientsDM");
               da.Fill(ds, "dt_PatientsDM");

                //try
                //{
                //    da.Fill(DsCCHITRPT, "dt_PatientBP");
                //}
                //catch
                //{
                //    da.Fill(DsCCHITRPT, "dt_PatientsDM");
                //}

                da.Dispose();
                da = null;

                if (ds.Tables["dt_PatientsDM"].Rows.Count == 0)
                {
                    bNoData = true;
                }
                else
                {
                    bNoData = false;
                }

                //objrptPateintDM.SetDataSource(DsCCHITRPT);

                objrptPateintDM.SetDataSource(ds);

                //ds.Dispose (); /?SLR:Why disposed after settting datasource?/
                //ds = null;

                //Binds the Report to the Report viewer
                objrptPateintDM.Refresh();
                //CRViewer.DisplayGroupTree = false;
                CRViewer.ShowGroupTreeButton = false;
                CRViewer.ReportSource = objrptPateintDM;
                objrptPateintDM.SetParameterValue(0, _UserName);//Sandip Darade 20100607


                // 

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                //DsCCHITRPT = null;
                _sqlcommand.Parameters.Clear();
                _sqlcommand.Dispose();
                _sqlcommand = null;
                oConnection.Close();
                oConnection.Dispose();
                oConnection = null;
            }
        }

        private void FillPatientRxReport(System.DateTime StartDT, System.DateTime EndDT, Int64 nproviderID, Int64 ageTp, Int64 age1, Int64 age2, string sDrugs, Int64 drugList)
        {
            DsCCHITRPTs dsCCHIT = new DsCCHITRPTs();
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();

            try
            {

                oConnection.ConnectionString = _databaseconnectionstring;
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = "Rpt_PatientDiabetesStatus";
                _sqlcommand.Connection = oConnection;

                //'// ** Add Param

                _sqlcommand.Parameters.Add("@nProviderID", System.Data.SqlDbType.BigInt);
                _sqlcommand.Parameters["@nProviderID"].Value = nproviderID;

                _sqlcommand.Parameters.Add("@AgeType", System.Data.SqlDbType.Int);
                _sqlcommand.Parameters["@AgeType"].Value = ageTp;

                _sqlcommand.Parameters.Add("@nAgeFrom", System.Data.SqlDbType.Int);
                _sqlcommand.Parameters["@nAgeFrom"].Value = age1;

                _sqlcommand.Parameters.Add("@nAgeTo", System.Data.SqlDbType.Int);
                _sqlcommand.Parameters["@nAgeTo"].Value = age2;

                _sqlcommand.Parameters.Add("@sDrugs", System.Data.SqlDbType.VarChar);
                _sqlcommand.Parameters["@sDrugs"].Value = sDrugs;

                _sqlcommand.Parameters.Add("@StartDt", System.Data.SqlDbType.DateTime);
                _sqlcommand.Parameters["@StartDt"].Value = StartDT;

                _sqlcommand.Parameters.Add("@EndDt", System.Data.SqlDbType.DateTime);
                _sqlcommand.Parameters["@EndDt"].Value = EndDT;

                _sqlcommand.Parameters.Add("@sReport", System.Data.SqlDbType.VarChar);
                _sqlcommand.Parameters["@sReport"].Value = _ReportNM;

                //for beerslist
                _sqlcommand.Parameters.Add("@druglist", System.Data.SqlDbType.Int);
                _sqlcommand.Parameters["@druglist"].Value = drugList;


                Boolean bErr = false;
                SqlDataAdapter da = new SqlDataAdapter(_sqlcommand);
                try
                {
                    da.Fill(dsCCHIT, "dt_PatientBP");
                }
                catch
                {
                    da.Fill(dsCCHIT, "dt_PatientDiabetesST");
                    bErr = true;
                }
                finally
                {
                    if (bErr == false)
                    {
                        da.Dispose();
                        da = null;
                        da = new SqlDataAdapter(_sqlcommand);
                        da.Fill(dsCCHIT, "dt_PatientDiabetesST");
                    }
                }

                da.Dispose();

                if (dsCCHIT.dt_PatientDiabetesST.Rows.Count == 0)
                {
                    bNoData = true;
                }
                else
                {
                    bNoData = false;
                }

                if (objrptPatientRx != null)
                    objrptPatientRx.Dispose();
                objrptPatientRx = new RptPatientRx(); 

                objrptPatientRx.SetDataSource(dsCCHIT);

                //Binds the Report to the Report viewer
                objrptPatientRx.Refresh();
              //  CRViewer.DisplayGroupTree = false;
                CRViewer.ShowGroupTreeButton = false;
                CRViewer.ReportSource = objrptPatientRx;

                objrptPatientRx.SetParameterValue("UserName", _UserName);//Sandip Darade 20100604


                //Sandip Darade 20100608
                string _strAge = cmbAge.Text;
                if (cmbAgeFrom.Visible == true)
                {
                    _strAge += " " + cmbAgeFrom.Text.Trim();
                }
                if (cmbAgeTo.Visible == true)
                {
                    _strAge += " To " + cmbAgeTo.Text.Trim();
                }
                //Sandip Darade 20100608

                string _strDate = string.Empty;
                if (dtpicFrom.Checked == true)
                {
                    _strDate = dtpicFrom.Value.Date.ToString("MM/dd/yyyy") + "  To  " + dtpicTo.Value.Date.ToString("MM/dd/yyyy");
                }
                else
                {
                    _strDate = "All";
                }

                objrptPatientRx.SetParameterValue("Date", _strDate);//Sandip Darade 20100608
                objrptPatientRx.SetParameterValue("Age", _strAge);//Sandip Darade 20100608
                objrptPatientRx.SetParameterValue("Provider", cmbProvider.Text.Trim());//Sandip Darade 20100608
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {

                _sqlcommand.Parameters.Clear();
                _sqlcommand.Dispose();
                _sqlcommand = null;
                oConnection.Close();
                oConnection.Dispose();
                oConnection = null;
            }
        }
        #endregion "Fill Reports"


        private void tblbtn_Close_32_Click(object sender, EventArgs e)
        {
            pnlMessage.Visible = false;
            this.Close();
        }

        private void cmbAge_TextChanged(object sender, EventArgs e)
        {
            if (cmbAge.Text == FOR_AGE)
            {
                lblAgeFrom.Visible = true;
                lblAgeFrom.Text = "Select";
                cmbAgeFrom.Visible = true;
                cmbAgeTo.Visible = false;
                lblAgeTo.Visible = false;
            }
            else if (cmbAge.Text == FOR_LESSTHAN_AGE)
            {
                lblAgeFrom.Visible = true;
                lblAgeFrom.Text = "Select";
                cmbAgeFrom.Visible = true;
                cmbAgeTo.Visible = false;
                lblAgeTo.Visible = false;
            }
            else if (cmbAge.Text == FOR_GREATERTHAN_AGE)
            {
                lblAgeFrom.Visible = true;
                lblAgeFrom.Text = "Select";
                cmbAgeFrom.Visible = true;
                cmbAgeTo.Visible = false;
                lblAgeTo.Visible = false;
            }
            else if (cmbAge.Text == FROMTO_AGE)
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

        private void tblbtn_Print_32_Click(object sender, EventArgs e)
        {
            if (CRViewer.Visible == true)
            {
                ConverttoPDF();               
            }
            else
            {
                MessageBox.Show("Report is not generated. Generate report before print.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
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


        #region "Custom Task"

        // Load Customgrid control
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

        //Remove customGrid control to form 
        private void RemoveControl()
        {
            if ((dgCustomGrid != null))
            {
                //pnlWordObj.Controls.Remove(dgCustomGrid)
                if (pnlcustomTask.Contains(dgCustomGrid))
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
                    try
                    {
                        dgCustomGrid.rbtnICD9Click -= new gloUserControlLibrary.CustomTask.rbtnICD9ClickEventHandler(dgCustomGrid_rbtnICD9Click);
                        //added for ICd10 implemantation
                        dgCustomGrid.rbtnICD10Click -= new gloUserControlLibrary.CustomTask.rbtnICD10ClickEventHandler(dgCustomGrid_rbtnICD10Click);
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

        //Add customGrid control to form 
        private void AddControl()
        {

            if ((dgCustomGrid != null))
            {
                RemoveControl();
            }
            dgCustomGrid = new CustomTask();

            pnlcustomTask.Controls.Add(dgCustomGrid);
            dgCustomGrid.Dock = DockStyle.Fill;  
            dgCustomGrid.SelectAllClick += new CustomTask.SelectAllClickEventHandler(dgCustomGrid_SelectAllClick);
            dgCustomGrid.DeSelectAllClick += new CustomTask.DeSelectAllClickEventHandler(dgCustomGrid_DeSelectAllClick);
            dgCustomGrid.OKClick += new CustomTask.OKClickEventHandler(dgCustomGrid_OKClick);
            dgCustomGrid.CloseClick += new CustomTask.CloseClickEventHandler(dgCustomGrid_CloseClick);
            dgCustomGrid.SearchChanged += new CustomTask.SearchChangedEventHandler(dgCustomGrid_SearchChanged);
            
            //changes made to show both ICD9 and 10 filter, for bugid 67217  v8020  
            if (strLst == "diag")
            {
                dgCustomGrid.rbtnICD9Click += new gloUserControlLibrary.CustomTask.rbtnICD9ClickEventHandler(dgCustomGrid_rbtnICD9Click);
                //added for ICd10 implemantation
                dgCustomGrid.rbtnICD10Click += new gloUserControlLibrary.CustomTask.rbtnICD10ClickEventHandler(dgCustomGrid_rbtnICD10Click);  
            }
            pnlcustomTask.BringToFront();

            int y = 0;
            int x = 0;

            if (strLst == "drugs")
            {
                if ((this.Text == "Patient based on History") || (this.Text == "Patient based on DM"))
                {
                    dgCustomGrid.setPnlVisible = false;
                }
                else
                {
                    //dgCustomGrid.setPnlSize(60,30);
                    //dgCustomGrid.chkBeersListClick += new CustomTask.chkBeersListClickEventHandler(dgCustomGrid_chkBeersListClick);
                    dgCustomGrid.setPnlVisible = true;
                    dgCustomGrid.rbtnBeersListClick += new CustomTask.rbtnBeersListClickEventHandler(dgCustomGrid_rbtnBeersListClick);
                }
                y = 141;
                x = 16;
            }
            else if (strLst == "cpt")
            {
                //dgCustomGrid.setPnlSize(30, 20);
                y = 141;
                x = 844;
            }
            else if (strLst == "diag")
            {
                //dgCustomGrid.setPnlSize(30, 20);           
                y = 141;
                x = 426;
            }

            pnlcustomTask.Location = new Point(x, y);
            pnlcustomTask.Visible = true;
            dgCustomGrid.Visible = true;
            pnlcustomTask.BringToFront();
            dgCustomGrid.BringToFront();
        }
        //code added by pradeep on 11/06/2010
        //void dgCustomGrid_chkBeersListClick(object sender, EventArgs e)
        //{

        //    try
        //    {
        //       if (dgCustomGrid.IsChecked == true )  
        //       {

        //               BindUserGrid(true);
        //               dgCustomGrid.datasource(dt.DefaultView);

        //       }
        //       else
        //        {
        //            //dgCustomGrid.enablerbtnPresByClinic = false;
        //            //dgCustomGrid.enablerbtnAllMedication = false;
        //            BindUserGrid();
        //            dgCustomGrid.datasource(dt.DefaultView);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    finally
        //    {
        //    }
        //}
        ////code added by pradeep on 11/06/2010

        //added for ICd10 implementation
        private void dgCustomGrid_rbtnICD9Click(object sender, EventArgs e)
        {
            dgCustomGrid.SearchText = "";
            FillICD9Type(9);
        }
        //added for ICd10 implementation
        private void dgCustomGrid_rbtnICD10Click(object sender, EventArgs e)
        {
            dgCustomGrid.SearchText = "";
            FillICD9Type(10);
        }

        //changes made to show both ICD9 and 10 filter, for bugid 67217  v8020  
        public void FillICD9Type(int ICDType)
        {

            this.Cursor = Cursors.WaitCursor;
            DataBaseLayer oDB = new DataBaseLayer();
            string _strSQL = "";
            try
            {
                //added for ICd10 implemantation
               
                
                    if (ICDType == 9)
                        _strSQL = "select Distinct (isNull(rtrim(sICD9Code),'') + ' - ' + isNull(ltrim(sDescription),'')) as [Diagnosis] from ICD9 Where sICD9Code <>'' AND sDescription<>''  AND ISNULL(nICDRevision,9)=9 order by [Diagnosis]";
                    else
                        _strSQL = "select Distinct (isNull(rtrim(sICD9Code),'') + ' - ' + isNull(ltrim(sDescription),'')) as [Diagnosis] from ICD9 Where sICD9Code <>'' AND sDescription<>''  AND ISNULL(nICDRevision,9)=10  AND nCodeType=1 order by [Diagnosis]";
                        
             
               
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
                    oDiag.Dispose();
                    oDiag = null;
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

        void dgCustomGrid_OKClick(object sender, EventArgs e)
        {
            try
            {
                //Sanjog -Added on 20101029 for don't allow repeated items in listview
                Int32 i = 0;
                Int32 j = 0;
                bool blnPresent = false;
                if (dgCustomGrid.C1Task.Rows.Count > 1)
                {
                    for (i = 0; i <= dgCustomGrid.GetTotalRows - 1; i++)
                    {
                        if (Convert.ToBoolean(dgCustomGrid.CurrentID.ToString()) == true)
                        {
                            if (dgCustomGrid.get_GetIsChecked(i, 0) == true)// .C1Grid.GetCellCheck(i, 0) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                            {
                                if (strLst == "drugs")
                                {
                                    for (j = 0; j < LstMedication.Items.Count ; j++)
                                    {
                                        blnPresent = false;
                                        if (LstMedication.Items[j].ToString() == dgCustomGrid.get_GetItem(i, 1).ToString())
                                        {
                                            blnPresent = true;
                                            break;
                                        }
                                    }
                                    if (blnPresent==false )
                                    LstMedication.Items.Add(dgCustomGrid.get_GetItem(i, 1).ToString());
                                }
                                else if (strLst == "cpt")
                                {
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
                                    LstTreatment.Items.Add(dgCustomGrid.get_GetItem(i, 1).ToString());
                                }
                                else if (strLst == "diag")
                                {
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
                                    LstDiagnosis.Items.Add(dgCustomGrid.get_GetItem(i, 1).ToString());
                                }
                            }
                            //Sanjog -Added on 20101029 for don't allow repeated items in listview
                        }
                    }
                    pnlcustomTask.Visible = false;
                }
                else
                {
                    pnlcustomTask.Visible = false;
                }

                //'  LstDiagnosis.Enabled = True


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
                DataView dvPatient =  (DataView)dgCustomGrid.GridDatasource;
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
                //Added by Mayuri:20100507-To implement startwith search for drugs and for diagnosis,procdeures instring search
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
        //code added by pradeep on 11/06/2010
        private void BindUserGrid(Boolean beerslist)
        {
            try
            {
                dt = FillDrugs(beerslist);
                CustomDrugsGridStyle();
                DataColumn col = new DataColumn();
                col.ColumnName = "Select Data";
                col.DataType = System.Type.GetType("System.Boolean");

                col.DefaultValue = false;
                if (!(dt.Columns.Contains(col.ColumnName)))
                {
                    dt.Columns.Add(col);
                }

                if ((dt != null))
                {
                    //'dt.Columns("sICD9Display").Caption = "Diagnosis Name"
                    dgCustomGrid.datasource(dt.DefaultView);
                }
                //'Reset the grid
                //////////
                float _TotalWidth = dgCustomGrid.Gridwidth - 5;
                dgCustomGrid.C1Grid.Cols.Move(dgCustomGrid.C1Grid.Cols.Count - 1, 0);
                dgCustomGrid.C1Grid.AllowEditing = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Show Reports", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Bind Customgrid control
        private void BindUserGrid()
        {
            try
            {
                dgCustomGrid.setPnlICDVisible = false ;   
                switch (strLst.ToLower())
                {
                    case "drugs":
                        {
                            if (bHistory == true || bDM == true) // history or DM
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
                                /// 'Get Categ
                                string strHisCateg = "";
                                int i = 0;

                                LstMedication.Refresh();

                                // collect the selected data of check list
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
                                dgCustomGrid.setPnlICDVisible = true;
                                //dt = FillICD9Type(9);
                                if (ICD10Transition == true)
                                {
                                    dgCustomGrid.rbICD10Transition = true; 
                                    FillICD9Type(10);
                                }
                                else
                                {
                                    dgCustomGrid.rbICD10Transition = false ; 
                                    FillICD9Type(9);
                                }
                                   dt = null;
                            }
                            break;




                        }
                }

              

              
                if (dt != null)
                {
                    CustomDrugsGridStyle();
                    DataColumn col = new DataColumn();
                    col.ColumnName = "Select Data";
                    col.DataType = System.Type.GetType("System.Boolean");

                    col.DefaultValue = false;
                    if (!(dt.Columns.Contains(col.ColumnName)))
                    {
                        dt.Columns.Add(col);
                    }

                    if ((dt != null))
                    {
                        //'dt.Columns("sICD9Display").Caption = "Diagnosis Name"
                        dgCustomGrid.datasource(dt.DefaultView);
                    }
                    //'Reset the grid
                    //////////
                    float _TotalWidth = dgCustomGrid.Gridwidth - 5;
                    dgCustomGrid.C1Grid.Cols.Move(dgCustomGrid.C1Grid.Cols.Count - 1, 0);
                    dgCustomGrid.C1Grid.AllowEditing = true;
                    try
                    {
                        if (dgCustomGrid.C1Grid.Cols[1].Name == "Description")
                        {
                            dgCustomGrid.C1Grid.Cols["Description"].AllowEditing = false;
                        }
                        else if (dgCustomGrid.C1Grid.Cols[1].Name == "DM_Criteria") 
                        {
                            dgCustomGrid.C1Grid.Cols["DM_Criteria"].AllowEditing = false;
                        }
                    }
                    catch
                    { }
                    
                }
               }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Show Reports", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Set grid style
        public void CustomDrugsGridStyle()
        {
            float _TotalWidth = dgCustomGrid.C1Grid.Width - 5;
            // '' Show Drugs Info           
            dgCustomGrid.C1Grid.Cols.Fixed = 0;
            dgCustomGrid.C1Grid.Rows.Fixed = 1;
            dgCustomGrid.C1Grid.Cols.Count = Col_Count;
            dgCustomGrid.C1Grid.AllowEditing = true;
            dgCustomGrid.C1Grid.SetData(0, Col_Check, "Select");
            dgCustomGrid.C1Grid.SetData(0, Col_Name, "Name");
        }

        #endregion "Custom Task"


        #region "Diagnosis Combo"
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
            SetCheckValues(LstDiagnosis);
        }



        //changes made to show both ICD9 and 10 filter, for bugid 67217  v8020  
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
        #endregion "Diagnosis Combo"

        #region "Drug Combo"

        private void btnBrowseDrug_Click(object sender, EventArgs e)
        {
            strLst = "drugs";
            LoadUserGrid();
            pnlcustomTask.BringToFront();

        }

        private void btnClearDrug_Click(object sender, EventArgs e)
        {
            while (LstMedication.SelectedItems.Count > 0)
            {
                LstMedication.Items.Remove(LstMedication.SelectedItems[0]);
            }
        }

        private void BtnClearAllDrg_Click(object sender, EventArgs e)
        {
            LstMedication.Items.Clear();
        }
        #endregion "Drug Combo"

        #region "CPT Combo"
        private void btnBrowseCPT_Click(object sender, EventArgs e)
        {
            strLst = "cpt";
            LoadUserGrid();
            pnlcustomTask.BringToFront();
        }

        private void btnClearCPT_Click(object sender, EventArgs e)
        {
            while (LstTreatment.SelectedItems.Count > 0)
            {
                LstTreatment.Items.Remove(LstTreatment.SelectedItems[0]);
            }
        }

        private void btnClearAllCPT_Click(object sender, EventArgs e)
        {
            LstTreatment.Items.Clear();
        }

        #endregion "CPT Combo"

        private void Tblbtn_More_Click(object sender, EventArgs e)
        {
            pnlDrugDiagnosis.Visible = true;
            Tblbtn_More.Visible = false;
            Tblbtn_Hide.Visible = true;
        }

        private void Tblbtn_Hide_Click(object sender, EventArgs e)
        {
            pnlDrugDiagnosis.Visible = false;
            Tblbtn_More.Visible = true;
            Tblbtn_Hide.Visible = false;
        }

        #region "ReplaceSpecialCharacters"
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
        #endregion //ReplaceSpecialCharacters



        private void tblbtn_Export_Click(object sender, EventArgs e)
        {
            if (CRViewer.Visible == true)
            {
                CrystalDecisions.Shared.ExportFormatType ExptFrmt = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat;
                if (bNoData == true)
                {
                    MessageBox.Show("No Data to Export", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                SaveFileDialog Sfd1 = new SaveFileDialog();

                Sfd1.Filter = "Microsoft Word (*.doc) |*.doc |RichText Format (*.rtf) |*.rtf |Microsoft Excel (*.xls) |*.xls |Microsoft Excel [Data Only] (*.xls) |*.xls |CrystalReport Format (*.rpt) |*.rpt |Acrobat (PDF) file (*.pdf)|*.pdf";
                //Sfd1.Filter = "*.pdf";
                Sfd1.FilterIndex = 1;
                Sfd1.RestoreDirectory = true;
                Sfd1.CreatePrompt = false;
                string[] Nm = CRViewer.ReportSource.ToString().ToLower().Split('.');

                Sfd1.FileName = Nm[1].ToString();

                if (Sfd1.ShowDialog(this) == DialogResult.OK)
                {
                    switch (Sfd1.FilterIndex)
                    {
                        case 1:
                            ExptFrmt = CrystalDecisions.Shared.ExportFormatType.WordForWindows;
                            break;
                        case 2:
                            ExptFrmt = CrystalDecisions.Shared.ExportFormatType.RichText;
                            break;
                        case 3:
                            ExptFrmt = CrystalDecisions.Shared.ExportFormatType.Excel;
                            break;
                        case 4:
                            ExptFrmt = CrystalDecisions.Shared.ExportFormatType.ExcelRecord;
                            break;
                        case 5:
                            ExptFrmt = CrystalDecisions.Shared.ExportFormatType.CrystalReport;
                            break;
                        case 6:
                            ExptFrmt = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat;
                            break;
                    }


                    switch (Nm[1])
                    {
                        case "rptpatientrx":
                            objrptPatientRx.ExportToDisk(ExptFrmt, Sfd1.FileName);
                            break;
                        case "rptpatientalerts":
                            objrptAlerts.ExportToDisk(ExptFrmt, Sfd1.FileName);
                            break;
                        case "rpt_patientsbp":
                            objrptPateintsBP.ExportToDisk(ExptFrmt, Sfd1.FileName);
                            break;
                        case "rpt_pateintsbmi":
                            objrptPateintsBMI.ExportToDisk(ExptFrmt, Sfd1.FileName);
                            break;
                        case "rpt_patientdiabetesstatus":
                            objrptPatientDiabSt.ExportToDisk(ExptFrmt, Sfd1.FileName);
                            break;
                        case "rpt_patienthistory":
                            objrptPateintHist.ExportToDisk(ExptFrmt, Sfd1.FileName);
                            break;
                        case "rpt_patientshealthplan":
                            objrptPateintDM.ExportToDisk(ExptFrmt, Sfd1.FileName);
                            break;
                    }
                    MessageBox.Show("Report Exported Successfully", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                Sfd1.Dispose();
                Sfd1 = null;

            }
            else 
            {
                MessageBox.Show("Report is not generated. Generate report before export.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void ConverttoPDF()
        {
            ReportDocument crReportDocument = (ReportDocument)CRViewer.ReportSource;
            DiskFileDestinationOptions rptFileDestOption = new DiskFileDestinationOptions();
            PdfRtfWordFormatOptions rptFormatOption = new PdfRtfWordFormatOptions();
            string[] Nm = CRViewer.ReportSource.ToString().ToLower().Split('.');
            string reportFileName = gloSettings.FolderSettings.AppTempFolderPath + Nm[1].ToString() + ".pdf";
            if (bNoData == true)
            {
                MessageBox.Show("No Data to Print", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


            try
            {


              

                //MessageBox.Show(Nm[1].ToString());

                if (Nm[1].ToString() == "rpt_patienthistory" || Nm[1].ToString() == "rpt_patientsbp")
                {
                    if (_DefaultPrinter == true)
                    {
                        crReportDocument.PrintToPrinter(1, false, 0, 0);
                        //crReportDocument.PrintOptions.PrinterName = pd.PrinterName;
                        //crReportDocument.PrintToPrinter(pd.Copies, false, pd.FromPage, pd.ToPage);
                    }

                    else
                    {
                        PrintDialog printDialog = new PrintDialog();


                        
                        if (printDialog.ShowDialog() == DialogResult.OK)
                        {
                            System.Drawing.Printing.PrinterSettings pd = printDialog.PrinterSettings;
                            crReportDocument.PrintOptions.PrinterName = pd.PrinterName;
                            crReportDocument.PrintToPrinter(pd.Copies, false, pd.FromPage, pd.ToPage);
                        }
                        if (printDialog != null)
                        {
                            printDialog.Dispose();
                            printDialog = null;
                        }

                    }
                }
                else
                {
                    rptFileDestOption.DiskFileName = reportFileName;
                    crReportDocument.ExportToDisk(ExportFormatType.PortableDocFormat, reportFileName);
                    Print(reportFileName, Nm[1].ToString(), "Success",!_DefaultPrinter);
                }



            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {

            }         
        }

        private void Print(string _PDFFileName, string rptName, string msgboxcaption, Boolean blnPrintDialog)
        {
            gloPrintDialog.gloPrintProgressController ogloPrintProgressController = null;
            try
            {
                using (gloPrintDialog.gloPrintDialog oDialog = new gloPrintDialog.gloPrintDialog())
                {
                    oDialog.ConnectionString = _databaseconnectionstring;
                    oDialog.TopMost = true;
                    oDialog.ShowPrinterProfileDialog = true;


                    oDialog.ModuleName = "CrystalReports";
                    oDialog.RegistryModuleName = "CrystalReports";
                    if (!gloGlobal.gloTSPrint.isCopyPrint)
                    {
                        System.Drawing.Printing.PrinterSettings _printsettings = new PrinterSettings();
                        oDialog.PrinterSettings = _printsettings;
                    }
                    if (blnPrintDialog == false)
                    {
                        oDialog.bUseDefaultPrinter = true;
                    }
                    if (oDialog != null)
                    {


                        IntPtr handle = GetActiveWindow();
                        Control NetControl = ControlFromHandle(handle);
                        if (!gloGlobal.gloTSPrint.isCopyPrint)
                        {
                            try
                            {
                                FileStream fs = new FileStream(_PDFFileName, FileMode.Open, FileAccess.Read);
                                StreamReader r = new StreamReader(fs);
                                string pdfText = r.ReadToEnd();
                                Regex rx1 = new Regex(@"/Type\s*/Page[^s]");
                                MatchCollection matches = rx1.Matches(pdfText);
                                fs.Close();
                                fs.Dispose();
                                r.Close();
                                r.Dispose();
                                fs = null;
                                r = null;
                                pdfText = null;
                                if (matches.Count > 0)
                                {
                                    oDialog.AllowSomePages = true;
                                    oDialog.PrinterSettings.ToPage = matches.Count;
                                    oDialog.PrinterSettings.FromPage = 1;
                                }
                            }
                            catch
                            {
                            }
                        }
                        if (oDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            if (Convert.ToBoolean(appSettings["DefaultPrinter"]))
                            {
                                oDialog.CustomPrinterExtendedSettings.IsBackGroundPrint = true;
                                oDialog.CustomPrinterExtendedSettings.IsShowProgress = true;
                            }
                            if (!gloGlobal.gloTSPrint.isCopyPrint)
                            {
                                if (rptName.Contains("rptPatientPrescriptions") || rptName.Contains("rpteRx") || rptName.Contains("rptFinanciaDetails") || rptName.Contains("rptCPTAnalysisDetailsReport") || rptName.Contains("PrescriptionUsageReport") || rptName.Contains("rptPatientList") || rptName.Contains("rptMU") || rptName.Contains("rptMU_stage1") || rptName.Contains("rptMU_stage2") || rptName.Contains("rptLabFlowSheet") || rptName.Contains("RptPatientImmunizationSummary") || rptName.Contains("rptPatientImmSummaryByTrade") || rptName.Contains("RptVaccineInventory") || rptName.Contains("rpt_InsurancePymtLog") || rptName.Contains("%ExpCollectionReport") || rptName.Contains("rptAgingReport") || rptName.Contains("rptFinancialReport") || rptName.Contains("rptAvailableReserves") || rptName.Contains("rptDailyCloseSummary") || rptName.Contains("rptMonthlyCloseSummary") || rptName.Contains("rptFinProReport") || rptName.Contains("rptFinProReport - ICD") || rptName.Contains("rptMissOppReport") || rptName.Contains("RptBatchEligiility") || rptName.Contains("rptPriorReport") || rptName.Contains("rptExcludePatientDue") || rptName.Contains("rptBatchReport") || rptName.Contains("rptFeeScheduleUnderReimbursement") || rptName.Contains("rptBatchLagReport"))
                                {

                                    oDialog.PrinterSettings.DefaultPageSettings.Landscape = true;
                                }
                            }
                            ogloPrintProgressController = new gloPrintDialog.gloPrintProgressController(_PDFFileName, oDialog.PrinterSettings, oDialog.CustomPrinterExtendedSettings);
                            ogloPrintProgressController.showTSPrinterSelection = blnPrintDialog;
                            ogloPrintProgressController.ShowProgress(NetControl);
                            
                        }

                    }
                    else
                    {
                        string _ErrorMessage = "Error in Showing Print Dialog";

                        if (_ErrorMessage.Trim() != "")
                        {
                            string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                            _MessageString = "";
                        }
                        MessageBox.Show(_ErrorMessage, msgboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }


            }
            catch (Exception ex)
            {
                #region " Make Log Entry "

                string _ErrorMessage = ex.ToString();
                //Code added on 7rd October 2008 By - Sagar Ghodke
                //Make Log entry in DMSExceptionLog file for any exceptions found
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    //  gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }

                //End Code add
                #endregion " Make Log Entry "

                MessageBox.Show(ex.Message, msgboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ex = null;
            }
            finally
            {

            }

        }
        private static Control ControlFromHandle(IntPtr hWND)
        {
            while (hWND != IntPtr.Zero)
            {
                Control control = Control.FromChildHandle(hWND);
                if (control != null)
                {
                    if (control is Form)
                    {
                        Control childControl = ((Form)control).ActiveControl;
                        if (childControl != null)
                        {
                            control = childControl;
                        }
                    }
                }
                if (control != null)
                    return control;

                hWND = GetParent(hWND);

                //  Control control2 = System.Windows.Forms.Control.FromChildHandle(hWND);
                // IntPtr hwnd = (IntPtr)this.Handle.ToPointer();
            }

            return null;
        }

        private void FillBPReportPatient(Int64 nproviderID, string sDiag)
        {
            //gloDatabaseLayer.DBLayer odb = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //odb.Connect(false);

            int age1 = 18;
            int age2 = 18;
            //string _strsql = " SELECT DISTINCT  Patient.nPatientId FROM Prescription INNER JOIN"
            //+ " Medication ON Prescription.nPrescriptionID = Medication.nPrescriptionID INNER JOIN  "
            //+ " Patient ON Prescription.nPatientID = Patient.nPatientID INNER JOIN Provider_MST ON Prescription.nProviderId = Provider_MST.nProviderID  "
            //+ " WHERE  ";
            string _strsql = " SELECT DISTINCT  Patient.nPatientId FROM Patient  ";

            string _sqlDiag = string.Empty;
            string _sqlHTCodes = string.Empty;
            if (sDiag != "" && chkDiagnosis.Checked != true)
            {
                //sDrugs = sDrugs.Replace("'", "''");
                //                  set @sHealthPlan=replace (@sHealthPlan,' or ',' or dm_mst_criterianame = ')   
                //sDiag = " smedication = " + sDiag;
                //sDiag = sDiag.Replace("or", " or smedication = ");
                sDiag = " sICD9Code = " + sDiag;
                sDiag = sDiag.Replace("or", " or sICD9Code = ");
                //_sqldrugs = "  Patient.npatientid in ((select npatientid from prescription where (smedication IN ( '" + sDrugs + "' ))) union (select npatientid from medication where (smedication in  ( '" + sDrugs + "' )))) ";
                // _sqlDiag = "  Patient.npatientid in ((select npatientid from prescription where ( " + sDiag + " )) union (select npatientid from medication where ( " + sDiag + " )) )";
                _sqlDiag = " WHERE  Patient.npatientid in (select npatientid from ExamICD9cpt where ( " + sDiag + " ))";

            }
            else
            {
                if (chkDiagnosis.Checked == true)
                {

                    _sqlHTCodes = " SELECT distinct sICD9Code FROM ICD9  WHERE  sICD9Code in (SELECT ISNULL(sICD9Code,'') FROM ICD9 where ISNUMERIC(sICD9Code) <> 0  AND  sICD9Code like ('40%')) "
                                        + " AND  ( convert(decimal(18,2 ),sICD9Code) >= 401) AND (convert(decimal(18,2 ),sICD9Code) < 406 ) ";
                    _sqlDiag = " WHERE  Patient.npatientid in (select npatientid from ExamICD9cpt where sICD9Code IN ( " + _sqlHTCodes + " ))   ";

                }
                else
                {
                    _strsql = " SELECT DISTINCT  Patient.nPatientId FROM   Patient  WHERE  ";
                }
            }
            _strsql += _sqlDiag;
            string _sqlage = string.Empty;
            if (cmbAge.Text != "For All")
            {
                if (cmbAgeFrom.Visible == true)
                {
                    age1 = Convert.ToInt16(cmbAgeFrom.Text.Trim());
                }
                else
                {
                    age1 = 18;
                }
                if (cmbAgeTo.Visible == true)
                {
                    age2 = Convert.ToInt16(cmbAgeTo.Text.Trim());
                }
                else if (cmbAgeFrom.Visible == true)
                {
                    age2 = age1;
                }

                //if (cmbAgeFrom.Visible == false && cmbAgeTo.Visible == false)
                //{
                //    age1 = 18;
                //    age2 = age1;
                //}

                if (_sqlDiag != "")
                {
                    _sqlage = " AND  ";
                }



                if (cmbAge.Text == "Less Than")
                {
                    _sqlage += "    Patient.npatientid in (select  distinct npatientid FROM Patient WHERE (SELECT (CONVERT (INT,dbo.GetPatientAgeInYears(Patient.npatientid,dbo.gloGetDate())))) <  " + age1 + "  ) ";
                    //  + " AND  (SELECT (CONVERT (INT,dbo.GetPatientAgeInYears(Patient.npatientid,dbo.gloGetDate())))) > 18  ";

                }
                else if (cmbAge.Text == "Greater Than")
                {
                    _sqlage += "    Patient.npatientid in (select  distinct npatientid FROM Patient WHERE (SELECT (CONVERT (INT,dbo.GetPatientAgeInYears(Patient.npatientid,dbo.gloGetDate())))) >  " + age1 + "  )";

                }
                else if (cmbAge.Text == "For Age" || age1 == age2)
                {
                    _sqlage += "    Patient.npatientid in (select  distinct npatientid FROM Patient WHERE (SELECT (CONVERT (INT,dbo.GetPatientAgeInYears(Patient.npatientid,dbo.gloGetDate())))) =  " + age1 + "  )";


                }
                else if (cmbAge.Text == "Between")
                {
                    _sqlage += "    Patient.npatientid in (select  distinct npatientid FROM Patient WHERE (SELECT (CONVERT (INT,dbo.GetPatientAgeInYears(Patient.npatientid,dbo.gloGetDate())))) >=  " + age1 + "  "
                        + " AND  (SELECT (CONVERT (INT,dbo.GetPatientAgeInYears(Patient.npatientid,dbo.gloGetDate())))) <= " + age2 + "  ) ";

                }
                else
                {

                    _sqlage += "    Patient.npatientid in (select  distinct npatientid FROM Patient WHERE (SELECT (CONVERT (INT,dbo.GetPatientAgeInYears(Patient.npatientid,dbo.gloGetDate())))) >=  " + age1 + "  "
             + " OR (SELECT (CONVERT (INT,dbo.GetPatientAgeInYears(Patient.npatientid,dbo.gloGetDate())))) = " + age2 + "  ) ";


                }
                _strsql += _sqlage + "AND ISNULL(patient.nExemptFromReport,0) <> 1";            //GLO2010-0006070 : Added this condition (Patient.nExemptFromReport <> 1) so that if Exempt from Report is check-on Patient will not display on report
            }
            //else 
            //{
            //    _strsql = _strsql.Replace("WHERE", "");
            //}
            if (nproviderID != 0)
            {
                if (_sqlage != "" || _sqlHTCodes != "")
                {
                    _strsql += "   AND ";
                }
                _strsql += "   Patient.npatientid in ((select npatientid from patient where nproviderid IN (" + nproviderID + "))) ";

            }
            // _strsql = "SELECT  DISTINCT nVisitId  FROM vitals WHERE dtvitaldate >='2010-06-02 16:54:17.000' "
            //     +"  AND nPatientId IN( " + _strsql + ")" ;

            //DataTable dt =new DataTable();
            //odb.Retrive_Query(_strsql,out dt);
            //string _PatientIds = string.Empty;
            //for (int i = 0; i < dt.Rows.Count ; i++)
            //{
            //    if (i == 0)
            //    {
            //        _PatientIds = Convert.ToString(dt.Rows[i]);
            //    }
            //    else
            //    {
            //   _PatientIds += "," +  Convert.ToString(dt.Rows[i]);
            //    }
            //}
            if (_sqlage == "" && _sqlDiag == "" && nproviderID == 0)
            {
                _strsql = _strsql.Replace("WHERE", "");
            }
            else if (_sqlage == "" && nproviderID == 0 && _sqlHTCodes != "")
            {

            }
            FillBPRepors(_strsql);



        }
        private void FillBPRepors(string QueryForPatientIds)
        {
            DsCCHITRPTs DsCCHITRPT = new DsCCHITRPTs();
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();
            DateTime _dtFrom;
            DateTime _dtTo;

            if (dtpicFrom.Checked == true)
            {
                _dtFrom = dtpicFrom.Value;
                _dtTo = dtpicTo.Value;

                _dtFrom = Convert.ToDateTime(dtpicFrom.Value.Date.ToShortDateString());
                _dtTo = Convert.ToDateTime(dtpicTo.Value.Date.ToShortDateString());
            }
            else
            {
                //_dtFrom = Convert.ToDateTime(DateTime.Now.Date.ToShortDateString());
                //_dtTo = Convert.ToDateTime(DateTime.Now.Date.ToShortDateString());
                _dtTo = Convert.ToDateTime(dtpicTo.Value.Date.ToShortDateString());
                _dtFrom = Convert.ToDateTime(dtpicTo.Value.Date.ToShortDateString());
                //_dtTo = DateTime.Now;
            }

            try
            {
                oConnection.ConnectionString = _databaseconnectionstring;
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = "gsp_GetPatientBP";
                _sqlcommand.Connection = oConnection;
                _sqlcommand.CommandTimeout = 0;

                // ** Add Param


                _sqlcommand.Parameters.Add("@QueryForPatientIds ", System.Data.SqlDbType.VarChar);
                _sqlcommand.Parameters["@QueryForPatientIds "].Value = QueryForPatientIds;

                _sqlcommand.Parameters.Add("@dtVitalDateFrom", System.Data.SqlDbType.DateTime);
                _sqlcommand.Parameters["@dtVitalDateFrom"].Value = Convert.ToDateTime(_dtFrom);

                _sqlcommand.Parameters.Add("@dtVitalDateTo", System.Data.SqlDbType.DateTime);
                _sqlcommand.Parameters["@dtVitalDateTo"].Value = Convert.ToDateTime(_dtTo);

                _sqlcommand.Parameters.Add("@blnDateselected", System.Data.SqlDbType.Bit);
                _sqlcommand.Parameters["@blnDateselected"].Value = dtpicFrom.Checked;

                SqlDataAdapter da = new SqlDataAdapter(_sqlcommand);
                //DataTable dt = new DataTable();
                //da.Fill(dt);
                da.Fill(DsCCHITRPT, "dt_PatientBP1");
                da.Dispose();
                int Rowcount = 0;
               
                if (DsCCHITRPT.Tables["dt_PatientBP1"] != null && DsCCHITRPT.Tables["dt_PatientBP1"].Rows.Count > 0)
                {
                    DataTable dt1 =  DsCCHITRPT.Tables["dt_PatientBP1"].DefaultView.ToTable(true, "ProviderID");
                    DataColumn colToalvisits = new DataColumn("TotalVisits");
                    DataColumn colVisitsWBP = new DataColumn("VisitsWOBP");
                    dt1.Columns.Add(colToalvisits);
                    dt1.Columns.Add(colVisitsWBP);
                    Rowcount = 0;
                    foreach (DataRow dr in dt1.Rows)
                    {
                        DataRow[] drprov = DsCCHITRPT.Tables["dt_PatientBP1"].Select("ProviderId='" + dr[0].ToString() + "'");
                        dt1.Rows[Rowcount]["TotalVisits"] = drprov.Length;
                        int _count = 0;
                        foreach (DataRow drtemp in drprov)
                        {
                            //dt1.Rows.Find(drtemp.ItemArray[1]);

                            if (drtemp.ItemArray[11] == DBNull.Value && drtemp.ItemArray[12] == DBNull.Value && drtemp.ItemArray[13] == DBNull.Value && drtemp.ItemArray[14] == DBNull.Value)
                            {
                                _count = _count + 1;

                            }

                        }
                        dt1.Rows[Rowcount]["VisitsWOBP"] = _count;
                        Rowcount = Rowcount + 1;
                    }
                    //DataColumn Toalvisits = new DataColumn("TotalVisits");
                    //DataColumn VisitsWBP = new DataColumn("VisitsWOBP");
                    //DsCCHITRPT.Tables["dt_PatientBP1"].Columns.Add(Toalvisits);
                    //DsCCHITRPT.Tables["dt_PatientBP1"].Columns.Add(VisitsWBP);
                    for (int i = 0; i <= dt1.Rows.Count - 1; i++)
                    {
                        for (int k = 0; k <= DsCCHITRPT.Tables["dt_PatientBP1"].Rows.Count - 1; k++)
                        {
                            if (Convert.ToInt64(DsCCHITRPT.Tables["dt_PatientBP1"].Rows[k]["Providerid"]) == Convert.ToInt64(dt1.Rows[i]["Providerid"]))
                            {
                                DsCCHITRPT.Tables["dt_PatientBP1"].Rows[k]["TotalVisits"] = dt1.Rows[i]["TotalVisits"];
                                DsCCHITRPT.Tables["dt_PatientBP1"].Rows[k]["VisitsWOBP"] = dt1.Rows[i]["VisitsWOBP"];
                            }

                        }

                    }
                    dt1.Dispose();
                    dt1 = null;
                }

                //     da.Fill(DsCCHITRPT, "dt_PatientBP1");


                //if (DsCCHITRPT.dt_PatientBP1 .Rows.Count != 0)

                {
                    objrptPateintsBP.SetDataSource(DsCCHITRPT);

                    //Binds the Report to the Report viewer
                    objrptPateintsBP.Refresh();
                    CRViewer.ReportSource = objrptPateintsBP;
                    objrptPateintsBP.SetParameterValue("UserName", _UserName);//Sandip Darade 20100604


                    //Sandip Darade 20100608
                    string _strAge = cmbAge.Text;
                    if (cmbAgeFrom.Visible == true)
                    {
                        if (_strAge != "For Age")
                        {
                            _strAge += " " + cmbAgeFrom.Text.Trim();

                        }
                        else
                        {
                            _strAge = cmbAgeFrom.Text.Trim();
                        }

                    }
                    if (cmbAgeTo.Visible == true)
                    {
                        _strAge += " To " + cmbAgeTo.Text.Trim();
                    }

                    string _strDate = string.Empty;
                    if (dtpicFrom.Checked == true)
                    {

                        _strDate = dtpicFrom.Value.Date.ToString("MM/dd/yyyy") + "  To  " + dtpicTo.Value.Date.ToString("MM/dd/yyyy");
                    }
                    else
                    {
                        //  _strDate = dtpicFrom.Value.Date.ToString("MM/dd/yyyy") + "  To  " + dtpicTo.Value.Date.ToString("MM/dd/yyyy");
                        _strDate = "All ";
                    }

                    bool blnShowDetail = true;
                    bool blnShowPieChart = true;
                    if (chkShowDeatal.Checked == true)
                    {
                        blnShowDetail = true;
                    }
                    else
                    {
                        blnShowDetail = false;
                    }
                    if (chkShowPieChart.Checked == true)
                    {
                        blnShowPieChart = true;
                    }
                    else
                    {
                        blnShowPieChart = false;
                    }
                    string strDiagnosisICD9 = "";
                    StringBuilder sbDiagnosis = new StringBuilder();
                    string[] sDiag = new string[3];
                    char[] sep2 = new char[] { '-' };
                    string sval = "";
                    // collect the selected data of check list
                    for (int i = 0; i <= LstDiagnosis.Items.Count - 1; i++)
                    {
                        sval = LstDiagnosis.Items[i].ToString();
                        sDiag = sval.Split(sep2);
                        if (i == 0)
                        {

                            sbDiagnosis.Append(sDiag[0].ToString());

                        }
                        else
                        {
                            sbDiagnosis.Append(", ");
                            sbDiagnosis.Append(sDiag[0].ToString());

                        }
                    }
                    strDiagnosisICD9 = sbDiagnosis.ToString();
                    if (chkDiagnosis.Checked == true)
                    {
                        strDiagnosisICD9 = "Included all hypertension codes ICD 401 – 405";
                    }

                    if (strDiagnosisICD9.Trim() == "")
                    {
                        strDiagnosisICD9 = "None";
                    }

                    objrptPateintsBP.SetParameterValue("ShowDetail", blnShowDetail);//Sandip Darade 20100608
                    objrptPateintsBP.SetParameterValue("ShowPieChart", blnShowPieChart);//
                    objrptPateintsBP.SetParameterValue("Date", _strDate);//Sandip Darade 20100618
                    objrptPateintsBP.SetParameterValue("Age", _strAge);//Sandip Darade 20100618
                    objrptPateintsBP.SetParameterValue("Diagnosis", strDiagnosisICD9);//Sandip Darade 20100618
                }
                // 

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                _sqlcommand.Parameters.Clear();
                _sqlcommand.Dispose();
                _sqlcommand = null;
                oConnection.Close();
                oConnection.Dispose();
                oConnection = null;
            }
        }

        private void CRViewer_Load(object sender, EventArgs e)
        {


        }

        private void chkShowDeatal_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowDeatal.Checked == false)
            {
                if (chkShowPieChart.Checked == false)
                {
                    chkShowPieChart.Checked = true;
                }
            }

        }

        private void chkShowPieChart_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowPieChart.Checked == false)
            {
                if (chkShowDeatal.Checked == false)
                {
                    chkShowDeatal.Checked = true;
                }
            }
        }

        private void chkDiagnosis_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDiagnosis.Checked == true)
            {
                pnlDiag.Enabled = false;
            }
            else
            {
                pnlDiag.Enabled = true;
            }

        }

        private void cmbAge_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAge.Text == FOR_AGE)
            {
                lblAgeFrom.Visible = true;
                lblAgeFrom.Text = "Select";
                cmbAgeFrom.Visible = true;
                cmbAgeTo.Visible = false;
                lblAgeTo.Visible = false;
            }
            else if (cmbAge.Text == FOR_LESSTHAN_AGE)
            {
                lblAgeFrom.Visible = true;
                lblAgeFrom.Text = "Select";
                cmbAgeFrom.Visible = true;
                cmbAgeTo.Visible = false;
                lblAgeTo.Visible = false;
            }
            else if (cmbAge.Text == FOR_GREATERTHAN_AGE)
            {
                lblAgeFrom.Visible = true;
                lblAgeFrom.Text = "Select";
                cmbAgeFrom.Visible = true;
                cmbAgeTo.Visible = false;
                lblAgeTo.Visible = false;
            }
            else if (cmbAge.Text == FROMTO_AGE)
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

        private void frmReports_FormClosed(object sender, FormClosedEventArgs e)
        {
            RemoveControl();
            objrptPatientDiabSt.Dispose();
            objrptPateintsBMI.Dispose();
            objrptPateintsBP.Dispose();
            objrptAlerts.Dispose();
            objrptPateintHist.Dispose();
            objrptPateintDM.Dispose();
            objrptPatientRx.Dispose();
            objrptdgDataPrint.Dispose();

            if (dt != null)
            {
                dt.Dispose();
                dt = null;
            }
        }


    }
}