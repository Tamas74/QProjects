using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Edidev.FrameworkEDI;
using gloAuditTrail;
using C1.Win.C1FlexGrid;
using gloPatient;
using gloOffice;
using System.Collections;

namespace gloPMGeneral
{

    public enum PaymentMode
    {
        None = 0,
        Cash = 1,
        Check = 2,
        MoneyOrder = 3,
        CreditCard = 4
    }

    public enum PayerMode
    {
        None = 0,
        Self = 1,
        Insurance = 2
    }


    public partial class frmCheckIn : Form
    {
        #region " Variable Declarations "

        Int64 _patientId = 0;
        Int64 _masterAppointmentID = 0;
        Int64 _detailAppointmentID = 0;
        // string strEligibilityDetails;
        private string _messageBoxCaption = String.Empty;
        private bool _IsChekinappointment = false;
        private Int32 _LastrowSelect = 0;
        private Int64 _nInsuranceIDfrViewBenefit = 0;
        //InsuranceID for CoPay
        private Int64 _InsId = 0;
        private bool _IsColored = false;
        private bool _IsAOFColored = false;
        private bool _IsPatConsentColored = false;
        ArrayList nBlinkingCells = new ArrayList();
        ArrayList nAOFBlinkingCells = new ArrayList();
        ArrayList nPatConsentBlinkingCells = new ArrayList();

        // GLO2010-0010515 : check in template patient information sheet not filling out history items once history is checked (Bug #4427)
        // Variable declared for visitID to hold current visitID 
        private long _nVisitID = 0;

        public long VisitID
        {
            get { return _nVisitID; }
            set { _nVisitID = value; }
        }

        public Int64 InsuranceID
        {
            get { return _InsId; }
            set { _InsId = value; }
        }
        public bool CheckInAppointment
        {
            get { return _IsChekinappointment; }
            set { _IsChekinappointment = value; }
        }



        //   DataTable dtClearingHouse;
        string _databaseconnectionstring = "";
        //   gloPatientStripControl.gloPatientStripControl oPatientStripControl = null;
        //string _messageBoxCaption = "gloPM";
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _clinicId = 0;

        #endregion

        #region " C1 Copay Grid Constants "

        private const int COL_CopayInsuranceID = 0;
        private const int COL_CopayInsuranceSelect = 1;
        private const int COL_CopayInsuranceName = 2;
        private const int COL_Copay = 3;
        private const int COL_Type = 4;
        private const int COL_CheckcreditCardMONo = 5;
        private const int COL_CheckCreditCardMODate = 6;
        // private const int COL_CreditCardno = 7;
        private const int COL_CardType = 7;
        //private const int COL_ExpiryDate = 9;
        private const int COL_SecurityNo = 8;
        //private const int COL_MoneyOrderNo = 11;
        //private const int COL_MoneyOrderdate = 12;
        private const int COL_CopayColCOUNT = 9;

        #endregion

        #region " C1 Insurance Grid Constants "

        private const int COL_InsuranceID = 0;
        private const int COL_InsuranceSelect = 1;
        private const int COL_InsuranceName = 2;
        private const int COL_SubscriberID = 3;
        private const int COL_SubscriberName = 4;
        private const int COL_Policy = 5;
        private const int COL_Group = 6;
        private const int COL_InsurancePhone = 7;
        private const int COL_IsPrimary = 8;
        private const int COL_ServiceType = 9;
        private const int COL_SerBtn = 10;
        private const int COL_Service = 11;
        private const int COL_COUNT = 12;

        #endregion

        #region " C1 Template Grid Constants "

        private const int COL_TemplateSelect_TEMP = 0;
        private const int COL_TemplateCategoryID_TEMP = 1;
        private const int COL_TemplateCategory_TEMP = 2;
        private const int COL_TemplateID_TEMP = 3;
        private const int COL_TemplateName_TEMP = 4;
        private const int COL_ProviderID_TEMP = 5;
        private const int COL_ProviderName_TEMP = 6;
        private const int COL_Count_TEMP = 7;

        #endregion

        #region " Constructor "

        public frmCheckIn(Int64 patientId, Int64 MasterAppointmentID, Int64 DetailAppointmentID, string databaseconnectionstring)
        {
            InitializeComponent();

            #region " Retrieve ClinicID from AppSettings "

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _clinicId = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _clinicId = 0; }
            }
            else
            { _clinicId = 0; }

            #endregion
            //Sandip Darade  20090428
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

            _databaseconnectionstring = databaseconnectionstring;
            _patientId = patientId;
            _masterAppointmentID = MasterAppointmentID;
            _detailAppointmentID = DetailAppointmentID;
            ClearGage.clsCleargage oclsCleargage = new ClearGage.clsCleargage();
            ssoHelper = oclsCleargage.InitiateSOSHelper(_databaseconnectionstring);
        }

        #endregion " Constructor "

        private void frmCheckIn_Load(object sender, EventArgs e)
        {




            gloC1FlexStyle.Style(c1PatientDetails, false);
            gloC1FlexStyle.Style(c1Templates, false);

            FillPatientInformation();
            FillPatientInsurance(_patientId);
            FillTemplates();
            FillCopayAlert();
            try
            {
                gloPatient.gloPatient.GetWindowTitle(this, _patientId, _databaseconnectionstring, _messageBoxCaption);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            DisplayGlobalPeriodAlert(_patientId);
            c1PatientDetails.Select();
            if (gloGlobal.gloPMGlobal.IsCleargageEnable)
            {
                ts_btnAccountOnFile.Visible = true;
                ts_btnPatientConsent.Visible = false;
                pnlAccountOnFile.Visible = true;
                getAccountOnFileStatus();
                GetPatientConsent();
            }
            else
            {
                ts_btnAccountOnFile.Visible = false;
                ts_btnPatientConsent.Visible = false;
                pnlAccountOnFile.Visible = false;
            }
        }
        private void DisplayGlobalPeriodAlert(long _PatientID)
        {
            PatientStatus objPatientStatus = new PatientStatus(_databaseconnectionstring);
            DataTable dtGlobalPeriod = objPatientStatus.GetLastGlobalPeriods_ForAlter(_PatientID);

            if (dtGlobalPeriod != null)
            {
                if (dtGlobalPeriod.Rows.Count > 0)
                {
                    lblGlobalPeriodAlert.Visible = true;
                    lblGlobalPeriodAlert.Text = "Global Period in Effect : " + dtGlobalPeriod.Rows[0]["Dates"].ToString();

                }
                else
                    lblGlobalPeriodAlert.Visible = false;
                dtGlobalPeriod.Dispose();
                dtGlobalPeriod = null;
            }
            else
                lblGlobalPeriodAlert.Visible = false;
            objPatientStatus.Dispose();
            objPatientStatus = null;
        }
        private void DisplayGlobalPeriodMessage(long _patientId)
        {
            PatientStatus objPatientStatus = new PatientStatus(_databaseconnectionstring);

            DataTable _dtGlobalPeriod = null;
            _dtGlobalPeriod = objPatientStatus.GetGlobalPeriods_ForAlter(_patientId);

            if (_dtGlobalPeriod != null)
            {
                if (_dtGlobalPeriod.Rows.Count == 1)
                {
                    String _strMessage = "Today’s visit falls within a Global Period:"
                                     + Environment.NewLine + "CPT : " + _dtGlobalPeriod.Rows[0]["CPT"].ToString().Trim()
                                     + Environment.NewLine + "Dates : " + _dtGlobalPeriod.Rows[0]["Dates"].ToString().Trim();
                    if (_dtGlobalPeriod.Rows[0]["Provider"].ToString().Trim() != "")
                        _strMessage = _strMessage + Environment.NewLine + "Provider : " + _dtGlobalPeriod.Rows[0]["Provider"].ToString().Trim();
                    if (_dtGlobalPeriod.Rows[0]["Insurance"].ToString().Trim() != "")
                        _strMessage = _strMessage + Environment.NewLine + "Insurance : " + _dtGlobalPeriod.Rows[0]["Insurance"].ToString().Trim();
                    if (_dtGlobalPeriod.Rows[0]["Reminder"].ToString().Trim() != "")
                        _strMessage = _strMessage + Environment.NewLine + "Reminder : " + _dtGlobalPeriod.Rows[0]["Reminder"].ToString().Trim();
                    if (_dtGlobalPeriod.Rows[0]["Notes"].ToString().Trim() != "")
                        _strMessage = _strMessage + Environment.NewLine + "Comment : " + _dtGlobalPeriod.Rows[0]["Notes"].ToString().Trim();
                    MessageBox.Show(_strMessage, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (_dtGlobalPeriod.Rows.Count > 1)
                {
                    MessageBox.Show("Today’s visit falls within MULTIPLE Global Periods.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                _dtGlobalPeriod.Dispose();
                _dtGlobalPeriod = null;

            }
            objPatientStatus.Dispose();
            objPatientStatus = null;


        }

        private void FillPatientInformation()
        {
            lblPatientName.Text = "";
            lblPatientCode.Text = "";
            lblProviderName.Text = "";
            lblPatientDOB.Text = "";
            lblPatientAge.Text = "";
            lblToday.Text = "";

            try
            {
                DataTable dtPatient = null;

                dtPatient = getPatientDemo(_patientId);
                if (dtPatient != null && dtPatient.Rows.Count > 0)
                {

                    lblPatientName.Text = Convert.ToString(dtPatient.Rows[0]["sPatientName"]);
                    lblPatientCode.Text = Convert.ToString(dtPatient.Rows[0]["sPatientCode"]);
                    lblProviderName.Text = Convert.ToString(dtPatient.Rows[0]["sProviderName"]);
                    lblPatientDOB.Text = Convert.ToString(dtPatient.Rows[0]["dtDOB"]);
                    lblPatientAge.Text = FormatAge(Convert.ToDateTime(dtPatient.Rows[0]["dtDOB"]));
                    lblToday.Text = DateTime.Now.ToString("MM/dd/yyyy");

                }
                if (dtPatient != null)
                {
                    dtPatient.Dispose();
                    dtPatient = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void FillPatientInsurance(Int64 PatientID)
        {
            try
            {
                this.c1PatientDetails.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1PatientDetails_CellChanged);

                DataTable dt_ins = null;
                dt_ins = getPatientInsurances(PatientID);
                string _ServiceTypes = "|    |Aetna|Medicaid California|Medicare|Mail Handlers|Mutual of Omaha|Tricare";

                if (dt_ins != null)
                {
                    c1PatientDetails.Clear();

                    c1PatientDetails.Rows.Count = 1;
                    c1PatientDetails.Rows.Fixed = 1;
                    c1PatientDetails.Cols.Count = COL_COUNT;
                    c1PatientDetails.Cols.Fixed = 0;

                    c1PatientDetails.SetData(0, COL_InsuranceSelect, "");
                    c1PatientDetails.SetData(0, COL_InsuranceID, "InsuranceID");
                    c1PatientDetails.SetData(0, COL_InsuranceName, "Insurance Name");
                    c1PatientDetails.SetData(0, COL_SubscriberID, "SubscriberID");
                    c1PatientDetails.SetData(0, COL_SubscriberName, "Subscriber Name");
                    c1PatientDetails.SetData(0, COL_Policy, "Policy");
                    c1PatientDetails.SetData(0, COL_Group, "Group");
                    c1PatientDetails.SetData(0, COL_InsurancePhone, "Insurance Phone");
                    //c1PatientDetails.SetData(0, COL_IsPrimary, "Is Primary");
                    c1PatientDetails.SetData(0, COL_IsPrimary, "Type");
                    c1PatientDetails.SetData(0, COL_ServiceType, "Service Type");
                    c1PatientDetails.SetData(0, COL_SerBtn, "  ");
                    c1PatientDetails.SetData(0, COL_Service, "Service");

                    c1PatientDetails.Cols[COL_InsuranceSelect].DataType = typeof(System.Boolean);

                    c1PatientDetails.Cols[COL_SerBtn].ComboList = "...";
                    c1PatientDetails.Cols[COL_SerBtn].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                    c1PatientDetails.Cols[COL_ServiceType].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    c1PatientDetails.Cols[COL_InsuranceName].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                    c1PatientDetails.Cols[COL_InsuranceSelect].AllowEditing = true;
                    c1PatientDetails.Cols[COL_InsuranceID].AllowEditing = false;
                    c1PatientDetails.Cols[COL_InsuranceName].AllowEditing = false;
                    c1PatientDetails.Cols[COL_SubscriberID].AllowEditing = false;
                    c1PatientDetails.Cols[COL_SubscriberName].AllowEditing = false;
                    c1PatientDetails.Cols[COL_Policy].AllowEditing = false;
                    c1PatientDetails.Cols[COL_Group].AllowEditing = false;
                    c1PatientDetails.Cols[COL_InsurancePhone].AllowEditing = false;
                    c1PatientDetails.Cols[COL_IsPrimary].AllowEditing = false;
                    c1PatientDetails.Cols[COL_ServiceType].AllowEditing = true;
                    c1PatientDetails.Cols[COL_SerBtn].AllowEditing = true;
                    c1PatientDetails.Cols[COL_Service].AllowEditing = false;

                    c1PatientDetails.Cols[COL_InsuranceSelect].Visible = true;
                    c1PatientDetails.Cols[COL_InsuranceID].Visible = false;
                    c1PatientDetails.Cols[COL_InsuranceName].Visible = true;
                    c1PatientDetails.Cols[COL_SubscriberID].Visible = false;
                    c1PatientDetails.Cols[COL_SubscriberName].Visible = true;
                    c1PatientDetails.Cols[COL_Policy].Visible = false;
                    c1PatientDetails.Cols[COL_Group].Visible = false;
                    c1PatientDetails.Cols[COL_InsurancePhone].Visible = false;
                    c1PatientDetails.Cols[COL_IsPrimary].Visible = true;
                    //c1PatientDetails.Cols[COL_SerBtn].Visible = true;
                    //c1PatientDetails.Cols[COL_ServiceType].Visible = true;
                    //c1PatientDetails.Cols[COL_Service].Visible = true;

                    c1PatientDetails.Cols[COL_SerBtn].Visible = false;
                    c1PatientDetails.Cols[COL_ServiceType].Visible = false;
                    c1PatientDetails.Cols[COL_Service].Visible = false;

                    c1PatientDetails.Cols[COL_InsuranceSelect].Width = 20;
                    c1PatientDetails.Cols[COL_InsuranceName].Width = 300;
                    c1PatientDetails.Cols[COL_SubscriberName].Width = 300;
                    c1PatientDetails.Cols[COL_IsPrimary].Width = 50;
                    c1PatientDetails.Cols[COL_ServiceType].Width = 140;
                    c1PatientDetails.Cols[COL_SerBtn].Width = 20;
                    c1PatientDetails.Cols[COL_Service].Width = 200;

                    int rowIndex = 0;
                    for (int i = 0; i < dt_ins.Rows.Count; i++)
                    {

                        c1PatientDetails.Rows.Add();
                        rowIndex = c1PatientDetails.Rows.Count - 1;

                        c1PatientDetails.SetCellCheck(rowIndex, COL_InsuranceSelect, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                        c1PatientDetails.SetData(rowIndex, COL_InsuranceID, Convert.ToString(dt_ins.Rows[i]["nInsuranceID"]));
                        c1PatientDetails.SetData(rowIndex, COL_InsuranceName, Convert.ToString(dt_ins.Rows[i]["InsuranceName"]));
                        c1PatientDetails.SetData(rowIndex, COL_SubscriberID, Convert.ToString(dt_ins.Rows[i]["sSubscriberID"]));
                        c1PatientDetails.SetData(rowIndex, COL_SubscriberName, Convert.ToString(dt_ins.Rows[i]["sSubscriberName"]));
                        c1PatientDetails.SetData(rowIndex, COL_Policy, Convert.ToString(dt_ins.Rows[i]["sSubscriberPolicy#"]));
                        c1PatientDetails.SetData(rowIndex, COL_Group, Convert.ToString(dt_ins.Rows[i]["sGroup"]));
                        c1PatientDetails.SetData(rowIndex, COL_InsurancePhone, Convert.ToString(dt_ins.Rows[i]["sPhone"]));
                        //c1PatientDetails.SetData(rowIndex, COL_IsPrimary, Convert.ToString(dt_ins.Rows[i]["bPrimaryFlag"]));
                        c1PatientDetails.SetData(rowIndex, COL_IsPrimary, Convert.ToString(dt_ins.Rows[i]["sInsuranceFlag"]));

                    }
                    c1PatientDetails.Cols[COL_ServiceType].ComboList = _ServiceTypes;
                    dt_ins.Dispose();
                    dt_ins = null;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.c1PatientDetails.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1PatientDetails_CellChanged);
            }

        }

        private void FillTemplates()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dt_ins = null;

            try
            {

                oDB.Connect(false);
                dt_ins = new DataTable();

                //string _sqlQuery = "SELECT TemplateGallery_MST.nTemplateID, ISNULL(TemplateGallery_MST.sTemplateName, '') AS sTemplateName, " +
                //      " ISNULL(TemplateGallery_MST.nCategoryID, 0) AS nCategoryID, ISNULL(TemplateGallery_MST.nProviderID, 0) AS nProviderID, " +
                //     " ISNULL(Provider_MST.sFirstName, '') + SPACE(1) + ISNULL(Provider_MST.sMiddleName, '') + SPACE(1) + ISNULL(Provider_MST.sLastName, '') " +
                //     " AS ProviderName, ISNULL(Category_MST.sDescription, '') AS sCategory  " +
                //     " FROM TemplateGallery_MST INNER JOIN " +
                //     " Category_MST ON TemplateGallery_MST.sCategoryName = Category_MST.sDescription LEFT OUTER JOIN " +
                //     " Provider_MST ON TemplateGallery_MST.nProviderID = Provider_MST.nProviderID ";

                //string _sqlQuery = "SELECT TemplateGallery_MST.nTemplateID, ISNULL(TemplateGallery_MST.sTemplateName, '') AS sTemplateName, " +
                //      " ISNULL(TemplateGallery_MST.nCategoryID, 0) AS nCategoryID, ISNULL(TemplateGallery_MST.nProviderID, 0) AS nProviderID, " +
                //     " ISNULL(Provider_MST.sFirstName, '') + SPACE(1) + ISNULL(Provider_MST.sMiddleName, '') + SPACE(1) + ISNULL(Provider_MST.sLastName, '') " +
                //     " AS ProviderName, ISNULL(Category_MST.sDescription, '') AS sCategory  " +
                //     " FROM TemplateGallery_MST INNER JOIN " +
                //     " Category_MST ON TemplateGallery_MST.sCategoryName = Category_MST.sDescription LEFT OUTER JOIN " +
                //     " Provider_MST ON TemplateGallery_MST.nProviderID = Provider_MST.nProviderID ORDER BY sCategory,sTemplateName";

                // Added on 20100612 by sandip dhakane




                string _sqlQuery = " SELECT TemplateGallery_Association.nTemplateID," +
                                   " ISNULL(TemplateGallery_MST.sTemplateName,'') AS sTemplateName," +
                                   "  ISNULL(TemplateGallery_MST.nCategoryID, 0) AS nCategoryID," +
                                   " ISNULL(TemplateGallery_Association.nProviderID, 0) AS nProviderID, " +
                                   "ISNULL(Provider_MST.sFirstName, '') + SPACE(1) +ISNULL(Provider_MST.sMiddleName, '') + SPACE(1) + ISNULL(Provider_MST.sLastName,'')as ProviderName, " +
                                  " ISNULL(TemplateGallery_Association.sTemplateCategoryName, '') AS sCategory  FROM         TemplateGallery_Association WITH (NOLOCK) INNER JOIN  TemplateGallery_MST WITH (NOLOCK) ON TemplateGallery_Association.nTemplateID = TemplateGallery_MST.nTemplateID " +
                                   " LEFT OUTER JOIN Provider_MST WITH (NOLOCK) ON TemplateGallery_MST.nProviderID = Provider_MST.nProviderID " +
                                    "WHERE (TemplateGallery_Association.nClinicID = 1)  AND (TemplateGallery_Association.nAssociatedCategoryID = 1) ORDER BY sCategory,sTemplateName";





                oDB.Retrive_Query(_sqlQuery, out dt_ins);

                if (dt_ins != null)
                {
                    c1Templates.Clear();

                    c1Templates.Rows.Count = 1;
                    c1Templates.Rows.Fixed = 1;
                    c1Templates.Cols.Count = COL_Count_TEMP;
                    c1Templates.Cols.Fixed = 0;


                    c1Templates.SetData(0, COL_TemplateSelect_TEMP, "");
                    c1Templates.SetData(0, COL_TemplateCategoryID_TEMP, "CategoryID");
                    c1Templates.SetData(0, COL_TemplateCategory_TEMP, "Category");
                    c1Templates.SetData(0, COL_TemplateID_TEMP, "TemplateID");
                    c1Templates.SetData(0, COL_TemplateName_TEMP, "Template");
                    c1Templates.SetData(0, COL_ProviderID_TEMP, "ProviderID");
                    c1Templates.SetData(0, COL_ProviderName_TEMP, "Provider");


                    c1Templates.Cols[COL_TemplateSelect_TEMP].DataType = typeof(System.Boolean);

                    c1Templates.Cols[COL_TemplateSelect_TEMP].AllowEditing = true;
                    c1Templates.Cols[COL_TemplateID_TEMP].AllowEditing = false;
                    c1Templates.Cols[COL_TemplateCategoryID_TEMP].AllowEditing = false;
                    c1Templates.Cols[COL_TemplateCategory_TEMP].AllowEditing = false;
                    c1Templates.Cols[COL_ProviderID_TEMP].AllowEditing = false;
                    c1Templates.Cols[COL_ProviderName_TEMP].AllowEditing = false;
                    c1Templates.Cols[COL_TemplateName_TEMP].AllowEditing = false;
                    c1Templates.Cols[COL_TemplateName_TEMP].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    c1Templates.Cols[COL_ProviderName_TEMP].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                    c1Templates.Cols[COL_TemplateSelect_TEMP].Visible = true;
                    c1Templates.Cols[COL_TemplateID_TEMP].Visible = false;
                    c1Templates.Cols[COL_TemplateCategoryID_TEMP].Visible = false;
                    c1Templates.Cols[COL_TemplateCategory_TEMP].Visible = true;
                    c1Templates.Cols[COL_TemplateName_TEMP].Visible = true;
                    c1Templates.Cols[COL_ProviderID_TEMP].Visible = false;
                    c1Templates.Cols[COL_ProviderName_TEMP].Visible = true;

                    c1Templates.Cols[COL_TemplateSelect_TEMP].Width = 20;
                    c1Templates.Cols[COL_TemplateID_TEMP].Width = 0;
                    c1Templates.Cols[COL_TemplateCategory_TEMP].Width = 250;
                    c1Templates.Cols[COL_TemplateName_TEMP].Width = 250;
                    c1Templates.Cols[COL_ProviderID_TEMP].Width = 0;
                    c1Templates.Cols[COL_ProviderName_TEMP].Width = 260;

                    int rowIndex = 0;
                    for (int i = 0; i < dt_ins.Rows.Count; i++)
                    {

                        c1Templates.Rows.Add();
                        rowIndex = c1Templates.Rows.Count - 1;
                        c1Templates.SetData(rowIndex, COL_TemplateSelect_TEMP, false);
                        // c1Templates.SetData(rowIndex, COL_TemplateSelect_TEMP, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                        c1Templates.SetData(rowIndex, COL_TemplateID_TEMP, Convert.ToString(dt_ins.Rows[i]["nTemplateID"]));
                        c1Templates.SetData(rowIndex, COL_TemplateCategoryID_TEMP, Convert.ToInt64(dt_ins.Rows[i]["nCategoryID"]));
                        c1Templates.SetData(rowIndex, COL_TemplateCategory_TEMP, Convert.ToString(dt_ins.Rows[i]["sCategory"]));
                        c1Templates.SetData(rowIndex, COL_TemplateName_TEMP, Convert.ToString(dt_ins.Rows[i]["sTemplateName"]));
                        c1Templates.SetData(rowIndex, COL_ProviderID_TEMP, Convert.ToString(dt_ins.Rows[i]["nProviderID"]));
                        c1Templates.SetData(rowIndex, COL_ProviderName_TEMP, Convert.ToString(dt_ins.Rows[i]["ProviderName"]));
                    }

                }

            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (dt_ins != null) { dt_ins.Dispose(); }
            }

        }

        private void FillCopayAlert()
        {
            DataSet _dsAlerts = null;
            DataTable _dtCopayAlerts = null;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParam = new gloDatabaseLayer.DBParameters();



            decimal damount = 0;

            if (nBlinkingCells != null)
            {
                nBlinkingCells = null;
            }
            nBlinkingCells = new ArrayList();


            try
            {
                oDB.Connect(false);
                oParam.Add("@PatientID", _patientId, System.Data.ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("GET_DashboardAlerts", oParam, out _dsAlerts);
                if (_dsAlerts != null && _dsAlerts.Tables.Count > 0)
                {
                    _dtCopayAlerts = _dsAlerts.Tables[3];
                }


                AddStyle();


                if (_dtCopayAlerts != null && _dtCopayAlerts.Rows.Count > 0)
                {

                    if (_dtCopayAlerts.Rows.Count > 1)
                    {
                        //if (Convert.ToDecimal(_dtCopayAlerts.Compute("sum(nCopay)", "")) > 0)
                        //{
                        //    // c1CopayAlert.Height = c1CopayAlert.Height + 19;
                        //    // c1CopayAlert.Rows.Add();
                        //    //c1CopayAlert.SetCellStyle(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "Node");
                        //    // c1CopayAlert.SetData(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "Copay");

                        //}

                        for (int iCount = 0; iCount <= _dtCopayAlerts.Rows.Count - 1; iCount++)
                        {
                            if (Convert.ToDecimal(_dtCopayAlerts.Rows[iCount]["nCopay"]) != 0)
                            {

                                damount = Convert.ToDecimal(_dtCopayAlerts.Rows[iCount]["nCopay"]);
                                lblCopayAlert.Text = "Expected Copay :  $" + (damount).ToString("N2").Trim();
                                nBlinkingCells.Add(lblCopayAlert);
                                lblCopayAlert.Visible = true;
                                break;
                            }


                        }
                    }
                    else
                    {
                        if (Convert.ToDecimal(_dtCopayAlerts.Rows[0]["nCopay"]) != 0)
                        {

                            damount = Convert.ToDecimal(_dtCopayAlerts.Rows[0]["nCopay"]);
                            lblCopayAlert.Text = "Expected Copay :  $" + (damount).ToString("N2").Trim();
                            nBlinkingCells.Add(lblCopayAlert);
                            lblCopayAlert.Visible = true;
                        }

                    }
                }


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
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

        private void AddStyle()
        {
            gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
            Color AlertColor = Color.Red;
            object oValue = new object();

            oSettings.GetSetting("BlinkingAlert", gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.ClinicID, out oValue);
            if (oValue != null && Convert.ToString(oValue) != "")
            {
                if (Convert.ToBoolean(oValue)) { tmrCopayAlertBlink.Start(); }
                else
                {
                    tmrCopayAlertBlink.Stop();
                    _IsColored = false;
                }
            }
            else
            {
                tmrCopayAlertBlink.Stop();
                _IsColored = false;
            }

            oSettings.GetSetting("AlertColor", gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.ClinicID, out oValue);
            oSettings.Dispose();
            oSettings = null;
            if (oValue != null && Convert.ToString(oValue) != "")
            {
                if (oValue.ToString() == "-1")  //code added to replace while color with blue for PM Alert, v8022 PRD change 
                {
                    oValue = "-14726787";
                }

                AlertColor = Color.FromArgb(Convert.ToInt32(oValue));
                lblCopayAlert.ForeColor = Color.FromArgb(Convert.ToInt32(oValue));
            }
            else
            {
                AlertColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
                lblCopayAlert.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            }

        }
        private void AddAOFStyle(string sStatus)
        {
            try
            {
                tmrAOFAlertBlink.Start();
                lblAOFAlert.Visible = false;
                switch (sStatus)
                {
                    case "ACTV":
                        {
                            lblAOFAgreementStatus.ForeColor = Color.Green;
                            //nAOFBlinkingCells.Add(lblAOFAgreementStatus);
                            break;
                        }
                    case "CANC":
                        {
                            lblAOFAgreementStatus.ForeColor = Color.Red;
                            //nAOFBlinkingCells.Add(lblAOFAgreementStatus);
                            break;
                        }
                    case "SUSP":
                        {
                            lblAOFAgreementStatus.ForeColor = Color.OrangeRed;
                            //nAOFBlinkingCells.Add(lblAOFAgreementStatus);
                            break;
                        }
                    case "PEND":
                        {
                            lblAOFAgreementStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
                            //nAOFBlinkingCells.Add(lblAOFAgreementStatus);
                            break;
                        }
                    case "":
                        {
                            lblAOFAlert.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
                            break;
                        }

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }
        private DataTable getPatientInsurances(Int64 PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

            DataTable dtInsurance = new DataTable();
            try
            {
                oDB.Connect(false);
                oDBParameters.Clear();
                oDBParameters.Add("@nPatientId", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@bExludedInActiveInsurances", chkExcludeInactiveInsurance.Checked ? 1 : 0, ParameterDirection.Input, SqlDbType.Bit);
                oDB.Retrive("gsp_GetPatientInsurances_CheckIn", oDBParameters, out dtInsurance);


                //string _strQuery = " SELECT PatientInsurance_DTL.nInsuranceID, " +
                //                    " ISNULL(PatientInsurance_DTL.sInsuranceName, '') AS InsuranceName, " +
                //                    " ISNULL(PatientInsurance_DTL.sSubscriberID, '')  AS sSubscriberID, " +
                //                    " CASE bIsSameAsPatient " +
                //                    "      WHEN 0 " +
                //                    "      THEN CASE bIsCompnay " +
                //                    "             WHEN 0 " +
                //                    "             THEN PatientInsurance_DTL.sSubFName + SPACE(1) + PatientInsurance_DTL.sSubMName + SPACE(1) + PatientInsurance_DTL.sSubLName " +
                //                    "             ELSE sCompanyName " +
                //                    "           END " +
                //                    "      WHEN 1 " +
                //                    "      THEN PatientInsurance_DTL.sSubFName + SPACE(1) + PatientInsurance_DTL.sSubMName + SPACE(1) + PatientInsurance_DTL.sSubLName " +
                //                    "    END AS sSubscriberName, " +  
                //                    " ISNULL(PatientInsurance_DTL.sSubscriberPolicy#, '') AS sSubscriberPolicy#, " +
                //                    " ISNULL(PatientInsurance_DTL.sGroup, '') AS sGroup,  " +
                //                    " PatientInsurance_DTL.sPhone, " +
                //                    " PatientInsurance_DTL.dtDOB,  " +
                //                    " PatientInsurance_DTL.dtEffectiveDate,  " +
                //                    " PatientInsurance_DTL.dtExpiryDate,  " +
                //                    " ISNULL(PatientInsurance_DTL.sSubFName, '') AS SubFName,  " +
                //                    " ISNULL(PatientInsurance_DTL.sSubMName, '') AS SubMName, " +
                //                    " ISNULL(PatientInsurance_DTL.sSubLName, '') AS SubLName,   " +
                //                    " ISNULL(PatientInsurance_DTL.nRelationShipID, 0) AS RelationShipID,  " +
                //                    " ISNULL(PatientInsurance_DTL.sRelationShip, '') AS RelationShip,  " +
                //                    " ISNULL(PatientInsurance_DTL.nDeductableamount, 0) AS Deductableamount, " +
                //                    " ISNULL(PatientInsurance_DTL.nCoveragePercent, 0) AS CoveragePercent,  " +
                //                    " ISNULL(PatientInsurance_DTL.nCoPay, 0) AS CoPay,  " +
                //                    " ISNULL(PatientInsurance_DTL.bAssignmentofBenifit, 0) AS AssignmentofBenifit,  " +
                //                    " PatientInsurance_DTL.dtStartDate, PatientInsurance_DTL.dtEndDate,  " +
                //                    " ISNULL(PatientInsurance_DTL.nInsuranceFlag, 0) AS nInsuranceFlag, " +
                //                    " PatientInsurance_DTL.sSubscriberGender,  " +
                //                    " PatientInsurance_DTL.sPayerID,  " +
                //                    " ISNULL(Patient.sCity, '') AS sCity, " +
                //                    " ISNULL(Patient.sState, '') AS sState,  " +
                //                    " ISNULL(Patient.sZIP, '') AS sZIP,   " +
                //                    " ISNULL(Patient.sAddressLine1, '') AS sAddress1, " +
                //                    " ISNULL(Patient.sAddressLine2, '') AS sAddress2, " +
                //                    " ISNULL(PatientRelationship.sRelationshipCode, '')   AS RelationshipCode, " +
                //                    " ISNULL(PatientInsurance_DTL.nContactID,0) AS nContactID, " +
                //                    " ISNULL(PatientInsurance_DTL.sInsuranceTypeCode, '') AS InsuranceTypeCode, " +
                //                    " ISNULL(PatientInsurance_DTL.sPayerId, '') AS PayerID, " +
                //                    " ISNULL(PatientInsurance_DTL.sSubscriberAddr1, '') AS SubscriberAddr1,  " +
                //                    " ISNULL(PatientInsurance_DTL.sSubscriberAddr2, '') AS SubscriberAddr2,  " +
                //                    " ISNULL(PatientInsurance_DTL.sSubscriberCity, '') AS SubscriberCity,  " +
                //                    " ISNULL(PatientInsurance_DTL.sSubscriberState, '') AS SubscriberState,  " +
                //                    " ISNULL(PatientInsurance_DTL.sSubscriberZip, '') AS SubscriberZip,  " +
                //                    " ISNULL(PatientInsurance_DTL.sZip, '') AS PayerZip,  " +
                //                    " ISNULL(PatientInsurance_DTL.sCity, '') AS PayerCity,  " +
                //                    " ISNULL(PatientInsurance_DTL.sState, '') AS PayerState,  " +
                //                    " ISNULL(PatientInsurance_DTL.sAddressLine1, '') AS PayerAddress1, " +
                //                    " ISNULL(PatientInsurance_DTL.sAddressLine2, '') AS PayerAddress2, " +
                //                    " CASE ISNULL(PatientInsurance_DTL.nInsuranceFlag,0) " +
                //                    " WHEN 0 THEN 'InActive' WHEN 1 THEN 'Primary'  " +
                //                    " WHEN 2 THEN 'Secondary' WHEN 3 THEN 'Tertiary'   " +
                //                    " ELSE '' END  AS sInsuranceFlag  ,ISNULL(PatientInsurance_DTL.bIsCompnay,0) AS bIsCompnay,ISNULL(PatientInsurance_DTL.sCompanyName,'') AS sCompanyName "+
                //                    " FROM PatientInsurance_DTL  WITH (NOLOCK) " +
                //                    " INNER JOIN Patient WITH (NOLOCK) ON PatientInsurance_DTL.nPatientID = Patient.nPatientID  " +
                //                    " INNER JOIN PatientRelationship WITH (NOLOCK) ON  " +
                //                    " PatientInsurance_DTL.nRelationShipID = PatientRelationship.nPatientRelID  " +
                //                    " WHERE PatientInsurance_DTL.nPatientID='" + PatientID + "'   ORDER BY nInsuranceFlag ";


                //oDB.Connect(false);
                //oDB.Retrive_Query(_strQuery, out dtInsurance);
            }
            catch (gloDatabaseLayer.DBException dbEX)
            {
                dbEX.ERROR_Log(dbEX.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oDBParameters.Dispose();
            }
            return dtInsurance;
        }

        private DataTable getPatientDemo(Int64 PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);

            DataTable dtPatientDemo = new DataTable();
            try
            {

                string _strQuery = "SELECT nPatientID, sPatientCode AS  sPatientCode,CONVERT(varchar,dtDOB,101) AS  dtDOB, "
                + " CASE WHEN ISNULL(Patient.sMiddleName,'') = '' "
                + " THEN Patient.sFirstName + ' ' + Patient.sLastName  "
                + " ELSE Patient.sFirstName + ' ' + Patient.sMiddleName + '' + Patient.sLastName  "
                + " END AS sPatientName, "
                + " CASE WHEN ISNULL(Provider_MST.sMiddleName,'') = '' "
                + " THEN Provider_MST.sFirstName + ' ' + Provider_MST.sLastName  "
                + " ELSE Provider_MST.sFirstName + ' ' + Provider_MST.sMiddleName + '' + Provider_MST.sLastName  "
                + " END AS sProviderName "
                + " FROM Patient WITH (NOLOCK) INNER JOIN Provider_MST "
                + " ON Patient.nProviderID = Provider_MST.nProviderID "
                + " WHERE Patient.nPatientID = " + PatientID + " ";

                oDB.Connect(false);
                oDB.Retrive_Query(_strQuery, out dtPatientDemo);
            }
            catch (gloDatabaseLayer.DBException dbEX)
            {
                dbEX.ERROR_Log(dbEX.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return dtPatientDemo;
        }

        public string FormatAge(DateTime BirthDate)
        {
            DateTime _BDate = BirthDate;
            // Compute the difference between BirthDate 'CODE FROM gloPM
            //year and end year. 
            bool IsBirthDateLeap = false;
            int years = DateTime.Now.Year - BirthDate.Year;
            int months = 0;
            int days = 0;
            //Test if BirthDay for LeapYear.
            if (BirthDate.Day == 29 & BirthDate.Month == 2)
            {
                IsBirthDateLeap = true;
            }
            // Check if the last year was a full year. 
            if (DateTime.Now < BirthDate.AddYears(years) && years != 0)
            {
                years -= 1;
            }
            BirthDate = BirthDate.AddYears(years);
            // Now we know BirthDate <= end and the diff between them 
            // is < 1 year. 
            if (BirthDate.Year == DateTime.Now.Year)
            {
                months = DateTime.Now.Month - BirthDate.Month;
            }
            else
            {
                months = (12 - BirthDate.Month) + DateTime.Now.Month;
            }
            // Check if the last month was a full month. 
            if (DateTime.Now < BirthDate.AddMonths(months) && months != 0)
            {
                months -= 1;
            }
            BirthDate = BirthDate.AddMonths(months);
            // Now we know that BirthDate < end and is within 1 month 
            // of each other. 
            days = (DateTime.Now - BirthDate).Days;

            //To Adjust Age if BirthDate is 29th Feb in leap year
            if (IsBirthDateLeap == true)
            {
                //'Sequence of following IF code is too important.. DON'T MODIFY
                days -= 1;
                if (DateTime.Now.Day == 29 & DateTime.Now.Month == 2)
                {
                    days += 1;
                }
                else if (DateTime.Now.Year % 4 == 0)
                {
                    days += 1;
                }
                if (days < 0 & DateTime.Now.Year % 4 != 0)
                {
                    days = 30;
                    months = months - 1;
                    if (months < 0)
                    {
                        months = 11;
                        years = years - 1;
                    }
                }
                if (months == 12)
                {
                    days = 30;
                    months = 11;
                }
            }

            //Return years & " years " & months & " months " & days & " days"
            //Following code to display age in Numeric and Text
            //Dim age As New AgeDetail
            //age.Age = years & " Years " & months & " Months " & days & " Days"
            //' Cases

            //'20081119   ''Following Code to Store ExactAge in String
            string _AgeStr = "";
            //if (gblShowAgeInDays == true & gblAgeLimit >= DateDiff(DateInterval.Day, (System.DateTime)_BDate, System.DateTime.Now.Date))
            //{
            if (years == 0)
            {
                if (months == 0)
                {


                    if (days <= 1)
                    {
                        _AgeStr = days + " Day";
                    }
                    else
                    {
                        _AgeStr = days + " Days";
                    }
                }
                else if (months == 1)
                {
                    if (days == 0)
                    {
                        _AgeStr = months + " Month";
                    }
                    else if (days == 1)
                    {
                        _AgeStr = months + " Month " + days + " Day";
                    }
                    else
                    {
                        _AgeStr = months + " Month " + days + " Days";
                    }
                }
                else if (months > 1)
                {
                    if (days == 0)
                    {
                        _AgeStr = months + " Months";
                    }
                    else if (days == 1)
                    {
                        _AgeStr = months + " Months " + days + " Day";
                    }
                    else
                    {
                        _AgeStr = months + " Months " + days + " Days";
                    }
                }
            }
            else if (years == 1)
            {
                if (months == 0)
                {
                    if (days == 0)
                    {
                        _AgeStr = years + " Year ";
                    }
                    else if (days == 1)
                    {
                        _AgeStr = years + " Year " + days + " Day";
                    }
                    else
                    {
                        _AgeStr = years + " Year " + days + " Days";
                    }
                }
                else if (months == 1)
                {
                    if (days == 0)
                    {
                        _AgeStr = years + " Year " + months + " Month ";
                    }
                    else if (days == 1)
                    {
                        _AgeStr = years + " Year " + months + " Month " + days + " Day";
                    }
                    else
                    {
                        _AgeStr = years + " Year " + months + " Month " + days + " Days";
                    }
                }
                else if (months > 1)
                {
                    if (days == 0)
                    {
                        _AgeStr = years + " Year " + months + " Months ";
                    }
                    else if (days == 1)
                    {
                        _AgeStr = years + " Year " + months + " Months " + days + " Day";
                    }
                    else
                    {
                        _AgeStr = years + " Year " + months + " Months " + days + " Days";
                    }
                }
            }
            else if (years > 1)
            {
                if (months == 0)
                {
                    if (days == 0)
                    {
                        _AgeStr = years + " Years ";
                    }
                    else if (days == 1)
                    {
                        _AgeStr = years + " Years " + days + " Day";
                    }
                    else
                    {
                        _AgeStr = years + " Years " + days + " Days";
                    }
                }
                else if (months == 1)
                {
                    if (days == 0)
                    {
                        _AgeStr = years + " Years " + months + " Month";
                    }
                    else if (days == 1)
                    {
                        _AgeStr = years + " Years " + months + " Month " + days + " Day";
                    }
                    else
                    {
                        _AgeStr = years + " Years " + months + " Month " + days + " Days";
                    }
                }
                else if (months > 1)
                {
                    if (days == 0)
                    {
                        _AgeStr = years + " Years " + months + " Months";
                    }
                    else if (days == 1)
                    {
                        _AgeStr = years + " Years " + months + " Months " + days + " Day";
                    }
                    else
                    {
                        _AgeStr = years + " Years " + months + " Months " + days + " Days";
                    }
                }
            }

            return _AgeStr;
        }


        #region " Tool Strip Button's Click Events "

        private void ts_btnSave_Click(object sender, EventArgs e)
        {
            Int64 _patInsId = 0;

            try
            {

                //if (ValidateForm() == true)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    bool _CheckElegibility = false;
                    for (int i = 1; i <= c1PatientDetails.Rows.Count - 1; i++)
                    {
                        if (c1PatientDetails.GetCellCheck(i, COL_InsuranceSelect) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                        {
                            _CheckElegibility = true;

                            if (c1PatientDetails.GetData(i, COL_InsuranceID) != null && Convert.ToString(c1PatientDetails.GetData(i, COL_InsuranceID)).Trim() != "")
                            { _patInsId = Convert.ToInt64(c1PatientDetails.GetData(i, COL_InsuranceID).ToString()); }
                            if (!ValidateInsuranceCoverage(_patInsId, Convert.ToString(c1PatientDetails.GetData(i, COL_InsuranceName))))
                            {
                                return;
                            }
                            FillInsurances();
                            break;
                        }
                    }

                    if (_CheckElegibility == true) { CheckEligibility(_patInsId); }

                    CheckInPatient();
                    this.DialogResult = DialogResult.OK;
                    PrintTemplate();

                    //ToDo: Add new method to save copay, this is the old method commentted by Anil on 20090617
                    //New method with new table structure
                    //SaveCopay();
                    /**************************************/

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void ts_btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.DialogResult = DialogResult.Cancel;
        }

        private void ts_btnPriorAuthorization_Click(object sender, EventArgs e)
        {
            //frmPriorAuthorization ofrmPriorAuthorization = null;
            //string _InsuranceName = "";
            //Int64 _InsuranceId = 0;


            try
            {
                if (_patientId > 0 && c1PatientDetails.Row > 0)
                {
                    frmSetupAuthorization objSetupAuth = new frmSetupAuthorization(_patientId);
                    objSetupAuth.ShowDialog(this);
                    objSetupAuth.Dispose();
                    objSetupAuth = null;
                }
                else
                {
                    MessageBox.Show("There is no eligibility verification.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {

            }
        }

        #endregion

        #region "Check into Database"
        private void CheckInPatient()
        {
            // gloPatient.PatientStatus oPatientStatus = new gloPatient.PatientStatus(_databaseconnectionstring);
            PatientStatus oPatientStatus = new PatientStatus(_databaseconnectionstring);
            oPatientStatus.PatientID = _patientId;
            oPatientStatus.patientStatusDate = DateTime.Now;
            oPatientStatus.TimeIn = DateTime.Now.ToLocalTime().ToShortTimeString();
            oPatientStatus.TimeOut = "";
            oPatientStatus.Location = "";
            oPatientStatus.Status = "";
            oPatientStatus.TrackingStatus = 3;// 0=None ,3=CheckIn, 4=CheckOut
            oPatientStatus.MasterAppointmentID = _masterAppointmentID;
            oPatientStatus.DetailAppointmentID = _detailAppointmentID;
            oPatientStatus.PatientCheckIn();
            oPatientStatus.Dispose();
            oPatientStatus = null;

        }
        #endregion

        #region "Print Templates"
        //Bug ID: 00000295 (Printing - EMR)
        //Reason: Modify PrintTemplate() method and create backup of original method.
        //Description: Performance improvement for Check-In Printing issue
        private void PrintTemplate()
        {
            List<gloOffice.gloTemplate> templateList = new List<gloOffice.gloTemplate>(); ;

            if (c1Templates.Rows.Count > 1)
            {
                gloOffice.gloTemplate template = null;

                for (int i = 1; i <= c1Templates.Rows.Count - 1; i++)
                {
                    if (c1Templates.GetCellCheck(i, COL_TemplateSelect_TEMP) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                    {
                        Int64 _TemplateID = Convert.ToInt64(c1Templates.GetData(i, COL_TemplateID_TEMP).ToString());
                        String _TemplateName = Convert.ToString(c1Templates.GetData(i, COL_TemplateName_TEMP).ToString());
                        Int64 _CategoryID = Convert.ToInt64(c1Templates.GetData(i, COL_TemplateCategoryID_TEMP));
                        String _CategoryName = Convert.ToString(c1Templates.GetData(i, COL_TemplateCategory_TEMP).ToString());
                        Int64 _ProviderID = Convert.ToInt64(c1Templates.GetData(i, COL_ProviderID_TEMP).ToString());

                        gloOffice.Supporting.DataBaseConnectionString = _databaseconnectionstring;

                        gloOffice.Supporting.PrimaryID = _TemplateID;
                        gloOffice.Supporting.isFromBatchPrint = true;
                        String fileName = gloOffice.Supporting.GenerateDocumentFile();

                        template = new gloTemplate(_databaseconnectionstring);
                        template.FromDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.Date.ToString("MM/dd/yyyy"));//added for incident CAS-07903-V1Y1P8 
                        template.AppointmentID = 0;
                        template.CategoryID = _CategoryID;  //Convert.ToInt64(oCategoryNode.Tag);
                        template.CategoryName = _CategoryName;  //oCategoryNode.Text;
                        template.TemplateID = _TemplateID;  //Convert.ToInt64(oTemplateNode.Tag);
                        template.TemplateName = _TemplateName;  //oTemplateNode.Text;
                        //template.PrimeryID = _masterAppointmentID;//Patient.AppointmentID;//  Convert.ToInt64(c1ChkInPatients.Rows[i][COL_DTLAPPOINTMENTID]); // Convert.ToInt64(oTemplateNode.Tag);
                        template.PrimeryID = _detailAppointmentID;
                        template.ClinicID = _clinicId;
                        template.DocumentCategory = 0;
                        template.VisitID = _nVisitID;
                        template.PatientID = _patientId; //Patient.patientID;
                        template.TemplateFilePath = fileName;

                        templateList.Add(template);
                    }
                }

                if (templateList.Count > 0)
                {
                    try
                    {
                        clsPatientTemplate PatientTemplateHelper = new clsPatientTemplate(_databaseconnectionstring);
                        PatientTemplateHelper.ParentForm = this;
                        PatientTemplateHelper.Print(templateList, printDocument1);
                        //  PatientTemplateHelper = null;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        //if (templateList != null)
                        //{
                        //    templateList.Clear();
                        //    templateList = null;
                        //}
                    }
                }
            }
        }

        private void PrintTemplate_BackUP()
        {

            if (c1Templates.Rows.Count > 1)
            {
                Int64 _AccountID = 0;
                for (int i = 1; i <= c1Templates.Rows.Count - 1; i++)
                {
                    if (c1Templates.GetCellCheck(i, COL_TemplateSelect_TEMP) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                    {
                        Int64 _TemplateID = Convert.ToInt64(c1Templates.GetData(i, COL_TemplateID_TEMP).ToString());
                        String _TemplateName = Convert.ToString(c1Templates.GetData(i, COL_TemplateName_TEMP).ToString());
                        Int64 _CategoryID = Convert.ToInt64(c1Templates.GetData(i, COL_TemplateCategoryID_TEMP));
                        String _CategoryName = Convert.ToString(c1Templates.GetData(i, COL_TemplateCategory_TEMP).ToString());
                        Int64 _ProviderID = Convert.ToInt64(c1Templates.GetData(i, COL_ProviderID_TEMP).ToString());
                        //_patientId


                        gloOffice.frmWd_PatientTemplate ofrm;
                        try
                        {
                            gloOffice.gloTemplate _gloTemplate = new gloOffice.gloTemplate(_databaseconnectionstring);
                            _gloTemplate.AppointmentID = 0;
                            _gloTemplate.CategoryID = _CategoryID;
                            _gloTemplate.CategoryName = _CategoryName;
                            _gloTemplate.TemplateID = Convert.ToInt64(_TemplateID);
                            //_gloTemplate.PrimeryID = Convert.ToInt64(_TemplateID);
                            _gloTemplate.PrimeryID = 0;
                            _gloTemplate.TemplateName = _TemplateName;
                            _gloTemplate.ClinicID = _clinicId;
                            _gloTemplate.DocumentCategory = 0;
                            _gloTemplate.PatientID = _patientId;

                            // GLO2010-0010515 : check in template patient information sheet not filling out history items once history is checked (Bug #4427)
                            // We were not sending Visit Id, so history liquid links was not populating in check-in template
                            _gloTemplate.VisitID = _nVisitID;

                            ofrm = new gloOffice.frmWd_PatientTemplate(_databaseconnectionstring, _gloTemplate, true);
                            ofrm._AccountID = _AccountID;
                            ofrm.ShowDialog(this);
                            if (ofrm._AccountID > 0)
                            {
                                _AccountID = ofrm._AccountID;
                            }
                            ofrm.Dispose();
                            ofrm = null;
                            if (_gloTemplate != null)
                            {
                                _gloTemplate.Dispose();
                                _gloTemplate = null;
                            }
                        }
                        catch (Exception)
                        {
                            ofrm = null;
                        }
                    }

                }
            }
        }
        #endregion

        #region "Check Eligibility"

        #region " Variables for Eligibility "

        //gloPatient.gloPatient ogloPatient;
        //gloPatient.Patient oPatient;

        // string sPath = "";
        //  string sSEFFile = "";
        //    string sEdiFile = "";
        //   ediDocument oEdiDoc = null;
        //   ediInterchange oInterchange = null;
        //   ediGroup oGroup = null;
        //   ediTransactionSet oTransactionset = null;
        //   ediDataSegment oSegment = null;
        //   ediSchema oSchema = null;
        //   ediSchemas oSchemas = null;
        //  ediWarnings oWarnings = null;
        //  ediWarning oWarning = null;


        private string _SubscriberFName = "";
        private string _SubscriberLName = "";
        private string _SubscriberMName = "";
        private string _SubscriberDOB = "";
        private string _SubscriberCity = "";
        private string _SubscriberState = "";
        private string _SubscriberZip = "";
        private string _SubscriberGender = "";
        private string _PayerName = "";
        private string _SubscriberPrimaryID = "";
        private string _SubscriberAdditionalID = "";
        //private string _SubscriberAdditionalIDQualifier = "1L";
        private string _PayerID = "";
        private string _SubscriberAddress = "";
        private string _SubscriberCardIssueDate = "";
        //  private string _ProviderID = "";
        //  private string _ProviderFName = "";
        //  private string _ProviderLName = "";
        //  private string _ProviderMName = "";
        //  private string _ProviderCity = "";
        //  private string _ProviderSSN = "";
        //  private string _ProviderNPI = "";
        //  private string _ProviderState = "";
        //  private string _ProviderZip = "";
        //  private string _ProviderAddress = "";
        private string _InsurancePlanCode = "";
        //  private string _ServiceTypeCode = "";
        private Int64 _InsuranceId = 0;
        //Insurance Types
        // string strMedicaidCalifornia = "";
        // string strMailHandlers = "";
        // string strMutualOfOmaha = "";
        // string strAetna = "";

        //Patient Variables
        //  private string _PatientLName = "";
        //  private string _PatientMName = "";
        //  private string _PatientFName = "";
        //   private string _PatientCity = "";
        //   private string _PatientDOB = "";
        //  private string _PatientAddress1 = "";
        //  private string _PatientAddress2 = "";
        //  private string _PatientState = "";
        //  private string _PatientZip = "";
        //  private string _PatientGender = "";
        //  private string _PatientSSN = "";
        //private string 


        #endregion " Variables for Eligibility "

        private void CheckEligibility(Int64 PatientInsuranceid)
        {
            gloPatient.gloPatientEiligibility ogloEiligibility = null;
            gloPatient.EiligiblityData EData = null;
            Int64 _patientProviderid = 0;
            Int64 _insuranceContactid = 0;
            Int64 tempInsId = 0;
            gloSettings.GeneralSettings oSettings = null;
            Object _objResult = null;
            string _result = "";
            int nANSIVersion = 0;

            try
            {
                tempInsId = PatientInsuranceid;

                ogloEiligibility = new gloPatient.gloPatientEiligibility(_databaseconnectionstring);
                _patientProviderid = ogloEiligibility.GetPatientProviderID(_patientId);
                _insuranceContactid = ogloEiligibility.GetInsuranceContactID(tempInsId, _patientId);

                oSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
                oSettings.GetSetting("INSURANCEELIGIBILITY", out _objResult);
                _result = Convert.ToString(_objResult);


                if (Convert.ToString(_patientId).Trim() != "" && Convert.ToString(_patientProviderid).Trim() != ""
                    && Convert.ToString(tempInsId).Trim() != "" && Convert.ToString(_insuranceContactid).Trim() != "")
                {
                    EData = ogloEiligibility.GetEiligibilityData(_patientId, _patientProviderid, tempInsId, _insuranceContactid);

                    nANSIVersion = oSettings.getANSIVersion(_insuranceContactid, "ELIGIBILITY", _clinicId);

                    if (EData != null)
                    {
                        if (_result == "" || _result == "BYCODE")
                        {
                            if (nANSIVersion == 0)
                            {
                                MessageBox.Show("Eligibility Requests ANSI Version has not been set. Eligibility may not proceed. Please review in gloPM Admin.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else if (nANSIVersion == (int)gloSettings.ANSIVersions.ANSI_4010)
                            {
                                ogloEiligibility.EDIGeneration_270(EData);
                            }
                            else if (nANSIVersion == (int)gloSettings.ANSIVersions.ANSI_5010)
                            {
                                ogloEiligibility.EDI5010Generation_270(EData, gloSettings.ANSIVersions.ANSI_5010);
                            }
                        }
                        else if (_result == "BYSERVICE")
                        {
                            if (nANSIVersion == 0)
                            {
                                MessageBox.Show("Eligibility Requests ANSI Version has not been set.Eligibility may not proceed.Please review in gloPM Admin.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else if (nANSIVersion == (Int64)gloSettings.ANSIVersions.ANSI_4010)
                            {
                                ogloEiligibility.EDIGeneration_270(EData, tempInsId, _patientProviderid);
                            }
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ogloEiligibility != null) { ogloEiligibility.Dispose(); }
                if (EData != null) { EData.Dispose(); }
                if (oSettings != null)
                {
                    oSettings.Dispose();
                    oSettings = null;
                }
            }
        }

        private void FillInsurances()
        {
            DataTable dtPatientInsurances = null;
            try
            {
                for (int i = 0; i <= c1PatientDetails.Rows.Count - 1; i++)
                {
                    if (c1PatientDetails.GetCellCheck(i, COL_InsuranceSelect) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                    {
                        _InsuranceId = Convert.ToInt64(c1PatientDetails.GetData(i, COL_InsuranceID).ToString());
                        dtPatientInsurances = getPatientInsurances(Convert.ToInt64(_patientId));
                        if (dtPatientInsurances != null)
                        {
                            if (dtPatientInsurances.Rows.Count > 0)
                            {
                                if (Convert.ToInt64(dtPatientInsurances.Rows[0]["nInsuranceID"].ToString()) == _InsuranceId)
                                {
                                    _PayerName = dtPatientInsurances.Rows[0]["InsuranceName"].ToString();
                                    _InsurancePlanCode = GetInsurancePlanCode(_PayerName);
                                    _PayerID = Convert.ToString(dtPatientInsurances.Rows[0]["PayerID"]);
                                    _SubscriberAdditionalID = Convert.ToString(dtPatientInsurances.Rows[0]["sGroup"]);
                                    _SubscriberAddress = Convert.ToString(dtPatientInsurances.Rows[0]["SubscriberAddr1"]) + " " + Convert.ToString(dtPatientInsurances.Rows[0]["SubscriberAddr2"]);
                                    _SubscriberCity = Convert.ToString(dtPatientInsurances.Rows[0]["SubscriberCity"]);
                                    _SubscriberState = Convert.ToString(dtPatientInsurances.Rows[0]["SubscriberState"]);
                                    _SubscriberZip = Convert.ToString(dtPatientInsurances.Rows[0]["SubscriberZip"]);
                                    _SubscriberGender = Convert.ToString(dtPatientInsurances.Rows[0]["sSubscriberGender"]);
                                    if (Convert.ToString(dtPatientInsurances.Rows[0]["dtStartDate"]) != "")
                                    {
                                        _SubscriberCardIssueDate = (Convert.ToString(gloDateMaster.gloDate.DateAsNumber(Convert.ToDateTime(dtPatientInsurances.Rows[0]["dtStartDate"]).ToShortDateString())));
                                    }
                                    else
                                    {
                                        _SubscriberCardIssueDate = "";
                                    }
                                    string[] SubsName = dtPatientInsurances.Rows[0]["sSubscriberName"].ToString().Split(' ');
                                    if (SubsName.Length > 0)
                                    {
                                        if (SubsName.Length > 1 && SubsName.Length < 2)
                                        {
                                            _SubscriberFName = SubsName[0].ToString();
                                            _SubscriberLName = SubsName[1].ToString();
                                        }
                                        if (SubsName.Length > 2)
                                        {
                                            _SubscriberFName = SubsName[0].ToString();
                                            _SubscriberMName = SubsName[1].ToString();
                                            _SubscriberLName = SubsName[2].ToString();
                                        }
                                    }
                                    if (Convert.ToString(dtPatientInsurances.Rows[0]["dtDOB"]) != "")
                                    {
                                        _SubscriberDOB = Convert.ToString(gloDateMaster.gloDate.DateAsNumber(Convert.ToDateTime(dtPatientInsurances.Rows[0]["dtDOB"]).ToShortDateString()));
                                    }
                                    else
                                    {
                                        if (Convert.ToBoolean(dtPatientInsurances.Rows[0]["bIsCompnay"]) == false)
                                            MessageBox.Show("Please enter a date of birth for the subscriber.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    _SubscriberPrimaryID = Convert.ToString(dtPatientInsurances.Rows[0]["sSubscriberID"]);
                                }
                            }
                            dtPatientInsurances.Dispose();
                            dtPatientInsurances = null;
                        }
                    }
                }

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }


        }

        private string GetInsurancePlanCode(string InsuranceName)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strSQL = "";
            Object _result = null;
            try
            {
                strSQL = " SELECT BL_InsurancePlanCodes_MST.sPlanCode FROM Contacts_MST WITH (NOLOCK) INNER JOIN " +
                         " BL_InsurancePlanCodes_MST WITH (NOLOCK) ON Contacts_MST.sState = BL_InsurancePlanCodes_MST.sState " +
                         " WHERE (Contacts_MST.sName = '" + InsuranceName + "') AND (Contacts_MST.nClinicID = " + 1 + ") "; //clinic id
                oDB.Connect(false);
                _result = oDB.ExecuteScalar_Query(strSQL);
                oDB.Disconnect();
                return Convert.ToString(_result);
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
                return "";
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

        #endregion

        private void c1PatientDetails_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

        private void c1Copay_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

        private void c1Templates_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

        private void c1PatientBillingInfo_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

        private void c1BalanceInfo_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

        private void c1PatientDetails_CellChanged(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {

            try
            {


                this.c1PatientDetails.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1PatientDetails_CellChanged);
                for (int i = 1; i < c1PatientDetails.Rows.Count; i++)
                {
                    if (i != e.Row)
                    {
                        if (c1PatientDetails.GetCellCheck(i, COL_InsuranceSelect) == CheckEnum.Checked)
                        {
                            c1PatientDetails.SetCellCheck(i, COL_InsuranceSelect, CheckEnum.Unchecked);
                        }
                    }
                }

                if (e.Row == _LastrowSelect)
                {
                    c1PatientDetails.SetCellCheck(e.Row, COL_InsuranceSelect, CheckEnum.Unchecked);
                    _LastrowSelect = 0;
                }
                else
                {
                    c1PatientDetails.SetCellCheck(e.Row, COL_InsuranceSelect, CheckEnum.Checked);
                    _LastrowSelect = e.Row;
                }
                if (c1PatientDetails.GetData(e.Row, COL_InsuranceID) != null && Convert.ToString(c1PatientDetails.GetData(e.Row, COL_InsuranceID)).Trim() != "")
                { _nInsuranceIDfrViewBenefit = Convert.ToInt64(c1PatientDetails.GetData(e.Row, COL_InsuranceID).ToString()); }
                //c1PatientDetails.SetCellCheck(0, COL_InsuranceSelect, CheckEnum.Unchecked);
            }
            catch (Exception)
            {

            }
            finally
            {
                this.c1PatientDetails.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1PatientDetails_CellChanged);
            }

        }

        private void btn_ModityPatient_Click(object sender, EventArgs e)
        {

            gloSecurity.gloSecurity oSecurity = new gloSecurity.gloSecurity(_databaseconnectionstring);

            try
            {
                if (oSecurity.isPatientLock(_patientId, true) == false && _patientId > 0)
                {
                    gloPatient.frmSetupPatient ofrmSetupPatient = new gloPatient.frmSetupPatient(_patientId, _databaseconnectionstring);
                    ofrmSetupPatient.ShowDialog(this);
                    ofrmSetupPatient.Dispose();
                    FillPatientInformation();
                    FillPatientInsurance(_patientId);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oSecurity != null)
                {
                    oSecurity.Dispose();
                    oSecurity = null;
                }
            }

        }

        private void frmCheckIn_Shown(object sender, EventArgs e)
        {
            DisplayGlobalPeriodMessage(_patientId);
        }

        private void ts_btnViewBenefit_Click(object sender, EventArgs e)
        {
            frmViewBenefit ofrm = new frmViewBenefit(_patientId, _nInsuranceIDfrViewBenefit, _databaseconnectionstring);
            ofrm.ShowDialog(this);
            ofrm.Dispose();
            FillPatientInsurance(_patientId);
            FillCopayAlert();
        }

        public void GetInsuranceCovrageDates(Int64 _PatientID, Int64 _nCurrentInsuranceID, out DataTable dtInsuranceCovrageDates)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            dtInsuranceCovrageDates = new DataTable();

            try
            {
                oDB.Connect(false);
                strQuery = "SELECT dtStartDate,dtEndDate  FROM dbo.PatientInsurance_DTL where nPatientID=" + _PatientID + " AND nInsuranceID=" + _nCurrentInsuranceID + " AND PatientInsurance_DTL.nInsuranceFlag !=0";
                oDB.Retrive_Query(strQuery, out dtInsuranceCovrageDates);

            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();

            }
        }
        private Boolean ValidateInsuranceCoverage(Int64 nCurrentInsuranceID, string sInsuranceName)
        {
            DataTable _dtInsuranceCoverageDates = new DataTable();
            GetInsuranceCovrageDates(_patientId, nCurrentInsuranceID, out _dtInsuranceCoverageDates);
            try
            {
                if (_dtInsuranceCoverageDates.Rows.Count > 0)
                {
                    if (_dtInsuranceCoverageDates.Rows[0]["dtStartdate"] != DBNull.Value)
                    {
                        if (Convert.ToDateTime(_dtInsuranceCoverageDates.Rows[0]["dtStartdate"]).Date > dtpCheckInDate.Value.Date)
                        {
                            if (MessageBox.Show("Patient’s insurance, " + sInsuranceName + ",  may not be up to date." + Environment.NewLine + "Please verify and update system." + Environment.NewLine + "Continue?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            {

                                return false;

                            }
                        }

                    }

                    if (_dtInsuranceCoverageDates.Rows[0]["dtEndDate"] != DBNull.Value)
                    {
                        if (Convert.ToDateTime(_dtInsuranceCoverageDates.Rows[0]["dtEndDate"]).Date < dtpCheckInDate.Value.Date)
                        {
                            if (MessageBox.Show("Patient’s insurance,  " + sInsuranceName + ",  may not be up to date. " + Environment.NewLine + "Please verify and update system." + Environment.NewLine + "Continue?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            {

                                return false;

                            }
                        }
                    }

                    if (_dtInsuranceCoverageDates.Rows[0]["dtStartdate"] != DBNull.Value && _dtInsuranceCoverageDates.Rows[0]["dtEndDate"] != DBNull.Value)
                    {
                        if (Convert.ToDateTime(_dtInsuranceCoverageDates.Rows[0]["dtStartdate"]).Date > dtpCheckInDate.Value.Date && Convert.ToDateTime(_dtInsuranceCoverageDates.Rows[0]["dtEndDate"]).Date < dtpCheckInDate.Value.Date)
                        {
                            if (MessageBox.Show("Patient’s insurance, " + sInsuranceName + ",  may not be up to date. " + Environment.NewLine + "Please verify and update system." + Environment.NewLine + "Continue?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            {

                                return false;

                            }
                        }
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                if (_dtInsuranceCoverageDates != null)
                {
                    _dtInsuranceCoverageDates.Dispose();
                    _dtInsuranceCoverageDates = null;
                }
            }
        }

        private void chkExcludeInactiveInsurance_CheckedChanged(object sender, EventArgs e)
        {
            FillPatientInsurance(_patientId);
        }

        private void tmrCopayAlertBlink_Tick(object sender, EventArgs e)
        {
            tmrCopayAlertBlink.Enabled = false;

            //if (Convert.ToString(lblCopayAlert.Text)) != "")
            //{
            if (_IsColored)
            {
                for (int iCount = 0; iCount <= nBlinkingCells.Count - 1; iCount++)
                {
                    lblCopayAlert.Visible = false;
                }
                _IsColored = false;
            }
            else
            {
                for (int iCount = 0; iCount <= nBlinkingCells.Count - 1; iCount++)
                {
                    // c1CopayAlert.SetCellStyle(Convert.ToInt16(nBlinkingCells[iCount]), COL_COPAYALERT_ALERTTEXT, "Default");
                    lblCopayAlert.Visible = true;
                }
                _IsColored = true;
            }
            //}
            tmrCopayAlertBlink.Enabled = true;
        }
        private ClearGage.SSO.SsoHelper ssoHelper;
        private void ts_btnAccountOnFile_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            ClearGage.SSO.Patient oPatient = null;
            try
            {
                oPatient = GetPatientInfo(_patientId);
                string content = ssoHelper.GetAccountOnFileDialogHtml(oPatient);
                DisplayWebBrowser(content);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oPatient!=null)
                {
                    oPatient = null;
                }
                Cursor.Current = Cursors.Default;
            }
        }
        private void getAccountOnFileStatus()
        {
            Cursor.Current = Cursors.WaitCursor;
            ClearGage.SSO.Patient oPatient = null;
            try
            {
                oPatient = GetPatientInfo(_patientId);
                ClearGage.SSO.Response.AutoPayAgreement oAutoPayAgreement = ssoHelper.GetAutoPayAgreement(oPatient);
                AddAOFStyle(oAutoPayAgreement.AgreementStatus);
                switch (oAutoPayAgreement.AgreementStatus)
                {
                    case "ACTV":
                        {
                            lblAOFAgreementStatus.Text = "Active";
                            lblAOFAlert.Text = "Active";
                            break;
                        }
                    case "CANC":
                        {
                            lblAOFAgreementStatus.Text = "Canceled";
                            lblAOFAlert.Text = "Canceled";
                            break;
                        }
                    case "SUSP":
                        {
                            lblAOFAgreementStatus.Text = "Suspended";
                            lblAOFAlert.Text = "Suspended";
                            break;
                        }
                    case "PEND":
                        {
                            lblAOFAgreementStatus.Text = "Pending";
                            lblAOFAlert.Text = "Pending";
                            break;
                        }
                    case "":
                        {
                            lblAOFAgreementStatus.Text = "";
                            lblAOFAlert.Text = "";
                            break;
                        }
                }
                lblAOFEffectiveDate.Text = oAutoPayAgreement.EffectiveDate == null ? "" : oAutoPayAgreement.EffectiveDate.Value.Date.ToString("MM/dd/yyyy");
                lblAOFAgreementDurationMax.Text = oAutoPayAgreement.AgreementDurationMax == null ? "" : oAutoPayAgreement.AgreementDurationMax.ToString();
                lblAOFEventCountMax.Text = oAutoPayAgreement.EventCountMax == null ? "" : oAutoPayAgreement.EventCountMax.ToString();
                lblAOFPerEventAmountMax.Text = oAutoPayAgreement.PerEventAmountMax == null ? "" : oAutoPayAgreement.PerEventAmountMax.ToString();
                if (oAutoPayAgreement.AgreementStatus == "SUSP")
                {
                    lblAOFUnsuspendDate.Text = oAutoPayAgreement.UnsuspendDate == null ? "" : oAutoPayAgreement.UnsuspendDate.Value.Date.ToString("MM/dd/yyyy");
                    lblAOFUnsuspendDate.Visible = true;
                    label39.Visible = true;
                }
                else
                {
                    lblAOFUnsuspendDate.Text = "";
                    lblAOFUnsuspendDate.Visible = false;
                    label39.Visible = false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oPatient != null)
                {
                    oPatient = null;
                }
                Cursor.Current = Cursors.Default;
            }
        }
        private void DisplayWebBrowser(string content)
        {
            try
            {
                ClearGage.frmCGWebBrowser oWeb = new ClearGage.frmCGWebBrowser(ssoHelper);

                //frmCGWebBrowser oWebUI = new frmCGWebBrowser(ssoHelper);
                oWeb.Text = "Account On File";
                oWeb.Icon = Properties.Resources.AccountonFile;
                oWeb.LoadContent(content);
                oWeb.ShowDialog();
                getAccountOnFileStatus();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }
        private ClearGage.SSO.Patient GetPatientInfo(long nPatientID)
        {
            Cursor.Current = Cursors.WaitCursor;
            ClearGage.SSO.Patient oPat = null;
            DataTable dt = null;
            gloPatient.gloPatient oPatient = null;
            try
            {
                oPat = new ClearGage.SSO.Patient();
                oPatient = new gloPatient.gloPatient(_databaseconnectionstring);
                Patient oPatientInfo = oPatient.GetPatient(nPatientID);
                if (oPatientInfo != null)
                {
                    oPat.FirstName = oPatientInfo.DemographicsDetail.PatientFirstName;
                    oPat.LastName = oPatientInfo.DemographicsDetail.PatientLastName;
                    oPat.BirthDate = oPatientInfo.DemographicsDetail.PatientDOB.ToString("MM/dd/yyyy");
                    oPat.Gender = Convert.ToString(oPatientInfo.DemographicsDetail.PatientGender).Substring(0, 1);
                    oPat.Address1 = Convert.ToString(oPatientInfo.DemographicsDetail.PatientAddress1);
                    oPat.Address2 = Convert.ToString(oPatientInfo.DemographicsDetail.PatientAddress2);
                    oPat.City = Convert.ToString(oPatientInfo.DemographicsDetail.PatientCity);
                    oPat.State = Convert.ToString(oPatientInfo.DemographicsDetail.PatientState);
                    oPat.Zip = Convert.ToString(oPatientInfo.DemographicsDetail.PatientZip);
                    oPat.Ssn = Convert.ToString(oPatientInfo.DemographicsDetail.PatientSSN);
                    oPat.EmailAddress = Convert.ToString(oPatientInfo.DemographicsDetail.PatientEmail);
                    oPat.MobilePhone = Convert.ToString(oPatientInfo.DemographicsDetail.PatientMobile);
                    oPat.Phone = Convert.ToString(oPatientInfo.DemographicsDetail.PatientPhone);
                    oPat.DriversLicenseNumber = "";
                    oPat.DriversLicenseState = "";
                    oPat.PatientId = Convert.ToString(oPatientInfo.DemographicsDetail.PatientCode);

                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        oPat.FirstName = Convert.ToString(dr["sFirstName"]);
                        oPat.LastName = Convert.ToString(dr["sLastName"]);
                        oPat.BirthDate = Convert.ToString(dr["dtDOB"]);
                        oPat.Gender = Convert.ToString(dr["sGender"]).Substring(0, 1);
                        oPat.Address1 = Convert.ToString(dr["sAddressLine1"]);
                        oPat.Address2 = Convert.ToString(dr["sAddressLine2"]);
                        oPat.City = Convert.ToString(dr["sCity"]);
                        oPat.State = Convert.ToString(dr["sState"]);
                        oPat.Zip = Convert.ToString(dr["sZip"]);
                        oPat.Ssn = Convert.ToString(dr["nSSN"]);
                        oPat.EmailAddress = Convert.ToString(dr["sEmail"]);
                        oPat.MobilePhone = Convert.ToString(dr["sMobile"]);
                        oPat.Phone = Convert.ToString(dr["sPhone"]);
                        oPat.DriversLicenseNumber = "";
                        oPat.DriversLicenseState = "";
                        oPat.PatientId = Convert.ToString(dt.Rows[0]["sPatientCode"]);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
            return oPat;
        }

        private void tmrAOFAlertBlink_Tick(object sender, EventArgs e)
        {
            try
            {
                tmrAOFAlertBlink.Enabled = false;

                //if (Convert.ToString(lblCopayAlert.Text)) != "")
                //{
                if (_IsAOFColored)
                {
                    for (int iCount = 0; iCount <= nAOFBlinkingCells.Count - 1; iCount++)
                    {
                        lblAOFAgreementStatus.Visible = false;
                    }
                    _IsAOFColored = false;
                }
                else
                {
                    for (int iCount = 0; iCount <= nAOFBlinkingCells.Count - 1; iCount++)
                    {
                        // c1CopayAlert.SetCellStyle(Convert.ToInt16(nBlinkingCells[iCount]), COL_COPAYALERT_ALERTTEXT, "Default");
                        lblAOFAgreementStatus.Visible = true;
                    }
                    _IsAOFColored = true;
                }
                //}
                tmrAOFAlertBlink.Enabled = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void btnRefreshStatus_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                getAccountOnFileStatus();
                GetPatientConsent();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void ts_btnPatientConsent_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            GetPatientConsent();
            Cursor.Current = Cursors.Default;
        }

        private bool GetPatientConsent()
        {
            ClearGage.SSO.Patient oPatient = null;
            ClearGage.SSO.Response.PatientConsent oConsent = null;
            bool bIsEsign = false;
            bool bIsEmail = false;
            bool bIsSMS = false;
            try
            {
                oPatient = GetPatientInfo(_patientId);
                oConsent = ssoHelper.GetPatientConsent(oPatient.PatientId);
                bIsEsign = oConsent.Esign == false ? false : Convert.ToBoolean(oConsent.Esign);
                bIsEmail = oConsent.Email == false ? false : Convert.ToBoolean(oConsent.Email);
                bIsSMS = oConsent.Sms == false ? false : Convert.ToBoolean(oConsent.Sms);
                
                lblConsentEsign.Text = bIsEsign == true ? "Yes" : "No";
                lblConsentEmail.Text = bIsEmail == true ? "Yes" : "No";
                lblConsentSMS.Text = bIsSMS == true ? "Yes" : "No";

                lblConsentEsign.ForeColor = bIsEsign == true ? Color.Green : Color.Red;
                lblConsentEmail.ForeColor = bIsEmail == true ? Color.Green : Color.Red;
                lblConsentSMS.ForeColor = bIsSMS == true ? Color.Green : Color.Red;
               
                //string sMessage = string.Format("Consent details for Patient code: {7}\nEmail:{0}\nEmail Address: {1}\nEsign: {2}\nMobile Phone: {3}\nSms:{4}\nRequest Sent Date:{5}\nResponse Received Date:{6}", oConsent.Email.ToString(), oConsent.EmailAddress.ToString(), oConsent.Esign.ToString(), oConsent.MobilePhone==null?"Null":oConsent.MobilePhone.ToString(), oConsent.Sms.ToString(), oConsent.RequestSentDate.ToString(), oConsent.ResponseReceivedDate.ToString(), oPatient.PatientId.ToString());
                //MessageBox.Show(sMessage);
                //DisplayWebBrowser(content);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oPatient != null)
                {
                    oPatient = null;
                }
                if (oConsent!=null)
                {
                    oConsent = null;
                }
            }
            return bIsEsign;
        }

        private void lnklblPatientConsent_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ts_btnAccountOnFile_Click(null, null);
        }

    }
}