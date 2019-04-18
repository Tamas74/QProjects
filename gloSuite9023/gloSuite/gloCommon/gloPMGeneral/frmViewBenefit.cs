using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Edidev.FrameworkEDI;
//using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using gloSettings;
using gloPatientStripControl;
using gloPMGeneral;
using C1.Win.C1FlexGrid;
using System.Linq;

using System.Collections;
using System.Globalization;
using System.Resources;

namespace gloPMGeneral
{
    public partial class frmViewBenefit : Form
    {

        #region " Private and Public Variables "
     
        gloPatient.PatientCards oPatientCards = new gloPatient.PatientCards();
        private string _databaseConnectionString = "";
        private Int64 _PatientId = 0;
        private Int64 _InsuranceId = 0;
        private Int64 _ClinicId = 0;
        private Int64 _UserId = 0;
        private string _messageBoxCaption = "";
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
     //   private ToolTip oToolTip = null;
      //  private string _DataBaseConnectionString = "";
      //  private string _MessageBoxCaption = "";          
   

        private Int64 _nPatientID = 0;
        private string _PatientCode = "";
        private string _PatientName = "";
        private string _PatientMedicalCategory = "";
        private string _NextAppointment = "";
        private string _EMRAlert = "";
        private Int64 _nProviderID = 0;
        private string _ProviderName = "";
        private string _Gender = "";      
        private string _PatientsMaritalStatus = "";
        private DateTime _DateOfBirth;
        private Image _patientPhoto;
        Boolean _isValidated = true;
        private clsviewBenefits oclsViewBenefits ;
      
    //    static readonly int myTrackMax = 44;   
      //  private 
        private Decimal _nCopay = 0;
        private Decimal _Deductible = 0;
        private Decimal _CoInsurance = 0;
        private DateTime  _dtStartDate ;
        private DateTime _dtendDate;
        private bool _IsNotStartDate = false;
        private bool _IsNotEndDate = false;
        private string _sUser = "";
        private DateTime _dtReviewedDateTime;
        public int width;
        private string _sInsuranceNote = "";
     //   ediDocument oEdiDoc1 = null;
    //    ediDataSegment oSegment = null;
     //   ediSchemas oSchemas = null;
     //   Boolean _IsCloseClick = false;
        Boolean _IsSaveClick = false;
        Boolean _IsFromModifiedPateint = false;
        DateTime _LasteEligibilityCheckDateTime;
        DateTime _LastChanged;
        #endregion " Private and Public Variables "
       
        #region "Constants For Grid"

        private const int COL_SRNO = 0;
        private const int COL_SERVICETYPE = 1;
        private const int COL_COVERAGE_LEVEL = 2;
        private const int COL_IN_NET = 3;
        private const int COL_AUTH_REQUIRE = 4;
        private const int COL_BENEFIT = 5;
        private const int COL_BENEFITAMOUNT = 6;
        private const int COL_DATES = 7;
        private const int COL_MESSAGE = 8;

        private const int COL_COUNT = 9;

        #endregion "Constants For Grid"       
        
        #region " Constructors "

        public frmViewBenefit(Int64 PatientID, String DatabaseConnectionString)
        {
            InitializeComponent();
            _databaseConnectionString = DatabaseConnectionString;
            _PatientId = PatientID;
            _InsuranceId = 0;
            oclsViewBenefits = new clsviewBenefits(_databaseConnectionString);
            #region " Retrieve ClinicID from AppSettings "

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicId = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicId = 0; }
            }
            else
            { _ClinicId = 0; }
            #endregion


            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                { _UserId = Convert.ToInt64(appSettings["UserID"]); }
                else { _UserId = 0; }
            }
            else
            { _ClinicId = 0; }
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
        }
        //}
        public frmViewBenefit(Int64 PatientID, Int64 InsuranceID, String DatabaseConnectionString)
        {

            InitializeComponent();          
            _PatientId = PatientID;
            _InsuranceId = InsuranceID;
            _databaseConnectionString = DatabaseConnectionString;
            oclsViewBenefits = new clsviewBenefits(_databaseConnectionString);
            #region " Retrieve ClinicID from AppSettings "

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicId = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicId = 0; }
            }
            else
            { _ClinicId = 0; }
            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                { _UserId = Convert.ToInt64(appSettings["UserID"]); }
                else { _UserId = 0; }
            }
            #endregion

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
        }

        public frmViewBenefit(String DatabaseConnectionString)
        {
            InitializeComponent();
            _databaseConnectionString = DatabaseConnectionString;
            oclsViewBenefits = new clsviewBenefits(_databaseConnectionString);
            #region " Retrieve ClinicID from AppSettings "

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicId = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicId = 0; }
            }
            else
            { _ClinicId = 0; }
            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                { _UserId = Convert.ToInt64(appSettings["UserID"]); }
                else { _UserId = 0; }
            }

            #endregion

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
        }

        #endregion " Constructors "

        #region "From Load"
        private void frmViewBenefit_Load(object sender, EventArgs e)
        {
            try
            {
                btn_ModifyPatient.BackgroundImage = global::gloPMGeneral.Properties.Resources.Patient;
                btn_ModifyPatient.BackgroundImageLayout = ImageLayout.Center;

                picPatient_Cards.OnZoomChanged += new gloPictureBox.gloPictureBox.ZoomChanged(TrackBarZoomChange);

                LoadPatientStrip(_PatientId, 0, true);
                FillInsuranceData();
                FillCardInformation();
               _IsFromModifiedPateint = false;
               changeHeightAsPerResolution();
                Refresh();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);

            }
        }
        bool semaPhoreTosuppressValueChange = false;
        void TrackBarZoomChange(object sender, gloPictureBox.gloPictureEventArgs e)
        {
            semaPhoreTosuppressValueChange = true;

            TrackBar.Value = picPatient_Cards.ZoomValueForTrackBar;
            
            semaPhoreTosuppressValueChange = false;
        }
        #endregion

        #region " Private Methods "
        private void Refresh()
        {
            lblPateintBenefitUptoDate.Visible = false;
            FillInsuranceData();
            FillPatientInfo();
            FillPlanEligibilityDetails();
            FilleEligibilityArea();
            DesignGrid();
            btnDown.Visible = false;
            btnUp.Visible = true;
            if (_LasteEligibilityCheckDateTime != DateTime.MinValue && _LastChanged.Date != DateTime.MinValue && _LasteEligibilityCheckDateTime.Date > _LastChanged.Date)
            {
                lblLastChanged.Text = _sUser;
                this.lblLastchngedDatetime.ForeColor = System.Drawing.Color.Red;
                lblPateintBenefitUptoDateNew.Visible = true;

            }
            else
            {
                lblPateintBenefitUptoDateNew.Visible = false;
                this.lblLastchngedDatetime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            }
            if (_InsuranceId != 0)
                lblPateintBenefitUptoDate.Text = "Patient’s insurance, " + cmbInsurance.Text + ", may not be up to date. Please verify and update system.";
            if(!_IsFromModifiedPateint)
            chkMarkReviewed.Checked = false;
            
           
        }
        private void LoadPatientStrip(Int64 PatientId, Int64 PatientProviderId, bool SearchEnable)
        {
            try
            {
                DisplayPatient(PatientId);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }
        public bool LoadGridColumnWidth(C1.Win.C1FlexGrid.C1FlexGrid oControlGrid, ModuleOfGridColumn ModuleName, Int64 UserID)
        {
            bool _result = false;
            object _SettingValueObj = null;
            try
            {
                string _SettingValue = "";                
                string[] _SettingValueList = null;

                string _SettingName = ModuleName.ToString().ToUpper() + "_" + oControlGrid.Name.ToString().ToUpper();
                _SettingValue = "";
                GetSetting(_SettingName, UserID, _ClinicId, out _SettingValueObj);

                if (_SettingValueObj != null && _SettingValueObj.ToString().Trim() != "")
                {
                    _SettingValue = _SettingValueObj.ToString();
                }
                if (_SettingValue.Trim().Length > 0)
                {
                    _SettingValueList = _SettingValue.Split(',');
                }

                if (_SettingValueList != null)
                {
                    if (_SettingValueList.Length > 0)
                    {
                        for (int i = 0; i <= _SettingValueList.Length - 1; i++)
                        {
                            int _ColWidth = 0;
                            _ColWidth = Convert.ToInt32(_SettingValueList[i].ToString());
                            oControlGrid.Cols[i].Width = _ColWidth;
                        }
                        _result = true;
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
                //SLR: Free SettingValueObj
                if (_SettingValueObj != null)
                {
                    _SettingValueObj = null;
                }
            }
            return _result;
        }
        public void GetSetting(string SettingName, Int64 UserID, Int64 ClinicID, out object Value)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            try
            {
                oDB.Connect(false);
                Value = oDB.ExecuteScalar_Query("SELECT ISNULL(sSettingsValue,'') AS sSettingsValue FROM Settings WITH (NOLOCK) WHERE sSettingsName = '" + SettingName + "' AND nUserID = " + UserID + " AND nClinicID = " + ClinicID + "");
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                Value = null;
                DBErr.ERROR_Log(DBErr.Message);
            }
            catch (Exception ex)
            {
                Value = null;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
        }
      //  private int _nZoom = 0;
        private byte[] _myPictureBoxControl = null;
        private System.Drawing.Bitmap _iPhoto = null;
        public byte[] MyPictureBoxControl
        {
            get { return _myPictureBoxControl; }
            set { _myPictureBoxControl = value; }
        }
        public System.Drawing.Bitmap PatientPhoto
        {
            get { return _iPhoto; }
            set { _iPhoto = value; }
        }
        private void FillCardInformation()
        {
            oPatientCards.Clear();

            picPatient_Cards.Image = null;
            picPatient_Cards.Tag = null;
            picPatient_Cards.PictBoxHeight = 217;
            picPatient_Cards.PictBoxWidth = 360;
            picPatient_Cards.sZoomVersion = "Insurance";
            gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(_databaseConnectionString);
            //oPatientCards = new gloPatient.PatientCards(); //SLR: new not needed            
            try
            {
                oPatientCards = ogloPatient.GetPatientCards(_PatientId);
                //vijay
                if (oPatientCards.Count > 0)
                {
                    MyPictureBoxControl = imageToByteArray((Image)oPatientCards[0].CardImage);
                    picPatient_Cards.byteImage = MyPictureBoxControl;
                    picPatient_Cards.Tag = 0;
                    if (oPatientCards[0].ScannedDateTime != null)
                    {
                        DateTime _dtScannedDate = oPatientCards[0].ScannedDateTime.Date;
                        lblScanDate.Text = _dtScannedDate.ToString("MM/dd/yyyy");

                    }
                    else
                    {
                        lblScanDate.Text = "";
                    }

                }
                else
                {
                    lblScanDate.Text = "";
                }
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

                ogloPatient.Dispose();
            }
        }

        Dictionary<string, string> sortedDict = null;
        public Dictionary<string, string> DishtblMedcatClr;
        private global::System.Globalization.CultureInfo resourceCulture;
        [System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal global::System.Globalization.CultureInfo Culture
        {
            get { return resourceCulture; }
            set { resourceCulture = value; }
        }

        private void FillMedicalCategoryHashTable()
        {
            if ((DishtblMedcatClr == null))
            {
                DishtblMedcatClr = new Dictionary<string, string>();
                DishtblMedcatClr.Add("MedicalCategoryImages_1_Brown_TopBrown", "MedicalCategoryImages_1_Brown_BottomBrown");
                DishtblMedcatClr.Add("MedicalCategoryImages_2_Blue_TopSkyBlue", "MedicalCategoryImages_2_Blue_BottomSkyBlue");
                DishtblMedcatClr.Add("MedicalCategoryImages_3_Gray_TopGray", "MedicalCategoryImages_3_Gray_BottomGray");
                DishtblMedcatClr.Add("MedicalCategoryImages_4_GreenTopLightGreen", "MedicalCategoryImages_4_Green_BottomLightGreen");
                DishtblMedcatClr.Add("MedicalCategoryImages_5_TopOrange", "MedicalCategoryImages_5_BottomOrange");

                DishtblMedcatClr.Add("MedicalCategoryImages_6_Pink_TopPink", "MedicalCategoryImages_6_Pink_BottomPink");
                DishtblMedcatClr.Add("MedicalCategoryImages_7_Red_TopRed", "MedicalCategoryImages_7_Red_BottomRed");
                DishtblMedcatClr.Add("MedicalCategoryImages_8_Violet_TopViolet", "MedicalCategoryImages_8_Violet_BottomViolet");
                DishtblMedcatClr.Add("MedicalCategoryImages_9_Yellow_TopYellow", "MedicalCategoryImages_9_Yellow_BottomYellow");
                DishtblMedcatClr.Add("MedicalCategoryImages_91_TopDark_Blue", "MedicalCategoryImages_91_BottomDark_Blue");
                //   DishtblMedcatClr.  
                sortedDict = (from entry in DishtblMedcatClr orderby entry.Key ascending select entry).ToDictionary(pair => pair.Key, pair => pair.Value);
            }
        }
        private static ResourceSet resourceSet = gloPMGeneral.Properties.Resources.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);

        private void GetMedicalCategoryImage(DataTable dtMedCat = null)
        {

            DataSet DS = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oPara = null;
            DataTable dtMedColor = null;
            string strcolor = "";
            string strborderColor = string.Empty;
            string strbottompanelcolr = string.Empty;
            try
            {
                var _with1 = oDB;
                oDB.Connect(false);
                oPara = new gloDatabaseLayer.DBParameters();
                oPara.Add("@tvpMedicalCategory", dtMedCat, ParameterDirection.Input, SqlDbType.Structured);
                oPara.Add("@PatientId", _nPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("gsp_GetPatientMedicalCategoryColor", oPara, out dtMedColor);
                oDB.Disconnect();
                if ((dtMedColor != null))
                {
                    if ((dtMedColor.Rows.Count > 0))
                    {
                        strcolor = Convert.ToString(dtMedColor.Rows[0]["ImageColor"]);
                        strborderColor = Convert.ToString(dtMedColor.Rows[0]["BorderColor"]);
                        strbottompanelcolr = Convert.ToString(dtMedColor.Rows[0]["BottomPanelColor"]);
                    }
                }


                if ((!string.IsNullOrEmpty(strcolor.Trim())))
                {
                    foreach (KeyValuePair<string, string> di in sortedDict)
                    {
                        if ((di.Key.ToString().Contains(strcolor.Trim().Replace(" ", "_"))))
                        {
                            pnlHeaderInner.BackgroundImage = (Image)resourceSet.GetObject(Convert.ToString(di.Key));
                            pnlLeft.BackgroundImage = (Image)resourceSet.GetObject(Convert.ToString(di.Value));
                            lblViewBenefitLeft.BackColor = System.Drawing.ColorTranslator.FromHtml(strborderColor);
                            lblViewBenefitRight.BackColor = System.Drawing.ColorTranslator.FromHtml(strborderColor);
                            lblViewBenefitBottom.BackColor = System.Drawing.ColorTranslator.FromHtml(strborderColor);
                            lblViewBenefitTopHeader.BackColor = System.Drawing.ColorTranslator.FromHtml(strborderColor);
                            lblViewBenefitLeftHeader.BackColor = System.Drawing.ColorTranslator.FromHtml(strborderColor);
                            lblViewBenefitRightHeader.BackColor = System.Drawing.ColorTranslator.FromHtml(strborderColor);
                            lblViewBenefitBottomHeader.BackColor = System.Drawing.ColorTranslator.FromHtml(strborderColor);
                            break; // TODO: might not be correct. Was : Exit For
                        }
                    }
                }
                else
                {
                   // ResourceSet resourceSet = gloPMGeneral.Properties.Resources.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
                    pnlHeaderInner.BackgroundImage = (Image)resourceSet.GetObject(Convert.ToString("MedicalCategoryImages_5_TopOrange"));
                    pnlLeft.BackgroundImage = (Image)resourceSet.GetObject(Convert.ToString("MedicalCategoryImages_5_BottomOrange"));
                    lblViewBenefitLeft.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF8B8E7B");
                    lblViewBenefitRight.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF8B8E7B");
                    lblViewBenefitBottom.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF8B8E7B");
                    lblViewBenefitTopHeader.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF8B8E7B");
                    lblViewBenefitLeftHeader.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF8B8E7B");
                    lblViewBenefitRightHeader.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF8B8E7B");
                    lblViewBenefitBottomHeader.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF8B8E7B");
                }

                if ((strcolor.Contains("Pink") || strcolor.Contains("Red") || strcolor.Contains("Violet") || strcolor.Contains("Dark")))
                {
                    lblPatientCode.ForeColor = Color.White;
                    lblPatientCodeCaption.ForeColor = Color.White;
                    lblGender.ForeColor = Color.White;
                    lblGenderCaption.ForeColor = Color.White;
                    lblDOB.ForeColor = Color.White;
                    lblDOBCaption.ForeColor = Color.White;
                    lblPatientName.ForeColor = Color.White;
                }
                else
                {
                    lblPatientCode.ForeColor = Color.Black;
                    lblPatientCodeCaption.ForeColor = Color.Black;
                    lblGender.ForeColor = Color.Black;
                    lblGenderCaption.ForeColor = Color.Black;
                    lblDOB.ForeColor = Color.Black;
                    lblDOBCaption.ForeColor = Color.Black;
                    lblPatientName.ForeColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
            finally
            {
                if ((oDB != null))
                {
                    oDB.Dispose();
                    oDB = null;
                }
                if ((dtMedColor != null))
                {
                    dtMedColor.Dispose();
                    dtMedColor = null;
                }
                if ((dtMedCat != null))
                {
                    dtMedCat.Dispose();
                    dtMedCat = null;
                }
                if ((DS != null))
                {
                    DS.Tables.Clear();
                    DS.Dispose();
                    DS = null;
                }
                if (oPara != null)
                {
                    oPara.Dispose();
                    oPara = null;
                }
            }


        }


        private bool ShowEMRAlertsOnPatientBanner()
        {
            bool isAllowed = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string _sqlQuery = "";
            DataTable _dt = null;
            try
            {
                oDB.Connect(false);
                _sqlQuery = "SELECT ISNULL(sSettingsValue, '') AS sSettingsValue FROM Settings  WHERE sSettingsName = 'ShowEMRAlertsOnPMPatientBanner'  AND nClinicID =1  ";
                oDB.Retrive_Query(_sqlQuery, out _dt);
                oDB.Disconnect();
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    if (Convert.ToString(_dt.Rows[0]["sSettingsValue"]) != "")
                    {
                        isAllowed = Convert.ToBoolean(_dt.Rows[0]["sSettingsValue"]);
                    }
                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true); }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_dt != null) { _dt.Dispose(); }
            }
            return isAllowed;
        }


        private void DisplayPatient(Int64 PatientID)
        {
            this.SuspendLayout();

            DataSet dtSet = null;
            DataTable dtInitialdemographic = null;
            DataTable dtAlerts = null;
            DataTable dtPatientNotes = null;
            DataTable dtStatementNotes = null;
            DataTable dtCopay = null;

            try
            {

                //Display Selected Patients Details
                if (PatientID > 0)
                {

                    dtSet = oclsViewBenefits.GetPatientDemographicInformation(PatientID);  //Collect all the information in Data Set

                    if (dtSet.Tables.Count > 0)
                    {
                        dtInitialdemographic = dtSet.Tables[0];   //Table 0 for patient name ,DOB,patientCode,Provider,Gender
                        dtAlerts = dtSet.Tables[1];   // Table 1 nAlertID, sAlertName
                        dtPatientNotes = dtSet.Tables[2];  // Table 2 Note
                        dtStatementNotes = dtSet.Tables[3];   // Table 3 nFromDate, nToDate,sStatementNote 
                        dtCopay = dtSet.Tables[4]; //Table 4 To Fetch Copay
                    }

                    //Assign patient name ,DOB,patientCode,Provider,Gender to Variables
                    #region  "Patient Demographics"



                    //decimal nExpectedCopay = 0;

                    if (dtInitialdemographic != null && dtInitialdemographic.Rows.Count > 0)
                    {
                        _nPatientID = PatientID;
                        _PatientName = dtInitialdemographic.Rows[0]["PatientName"] == DBNull.Value ? string.Empty : dtInitialdemographic.Rows[0]["PatientName"].ToString();
                        _PatientCode = dtInitialdemographic.Rows[0]["PatientCode"] == DBNull.Value ? string.Empty : dtInitialdemographic.Rows[0]["PatientCode"].ToString();
                        _DateOfBirth = dtInitialdemographic.Rows[0]["DOB"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dtInitialdemographic.Rows[0]["DOB"]);
                        _Gender = dtInitialdemographic.Rows[0]["Gender"] == DBNull.Value ? string.Empty : dtInitialdemographic.Rows[0]["Gender"].ToString();
                        //_PatientCode = dtInitialdemographic.Rows[0]["PatientCode"] == DBNull.Value ? string.Empty : dtInitialdemographic.Rows[0]["PatientCode"].ToString();

                        _nProviderID = dtInitialdemographic.Rows[0]["nProviderID"] == DBNull.Value ? 0 : Convert.ToInt64(dtInitialdemographic.Rows[0]["nProviderID"]);
                        _ProviderName = dtInitialdemographic.Rows[0]["Provider"] == DBNull.Value ? string.Empty : dtInitialdemographic.Rows[0]["Provider"].ToString();
                        //nExpectedCopay = dtInitialdemographic.Rows[0]["ExpectedCopay"] == DBNull.Value ? 0 : Convert.ToDecimal(dtInitialdemographic.Rows[0]["ExpectedCopay"].ToString());
                        _PatientsMaritalStatus = dtInitialdemographic.Rows[0]["Provider"] == DBNull.Value ? string.Empty : dtInitialdemographic.Rows[0]["MaritalStatus"].ToString();
                        _PatientMedicalCategory = dtInitialdemographic.Rows[0]["MedicalCategory"] == DBNull.Value ? string.Empty : dtInitialdemographic.Rows[0]["MedicalCategory"].ToString();

                        _NextAppointment = dtInitialdemographic.Rows[0]["NextAppointment"] == DBNull.Value ? string.Empty : dtInitialdemographic.Rows[0]["NextAppointment"].ToString();
                        _EMRAlert = dtInitialdemographic.Rows[0]["EMRAlerts"] == DBNull.Value ? string.Empty : dtInitialdemographic.Rows[0]["EMRAlerts"].ToString();

                        if (dtInitialdemographic.Rows[0]["PatientPhoto"] != DBNull.Value)
                        {
                            Byte[] patPicture = (Byte[])dtInitialdemographic.Rows[0]["PatientPhoto"];
                            try
                            {
                                _patientPhoto = byteArrayToImage(patPicture);
                            }
                            catch //(Exception ex)
                            {
                                _patientPhoto = null;
                            }
                            patPicture = null;
                        }
                        else
                        {
                            _patientPhoto = null;
                        }
                    }
                    else
                    {
                        _nPatientID = 0;
                        _PatientName = "";
                        _PatientCode = "";
                        _DateOfBirth = DateTime.MinValue;
                        _Gender = "";
                        _PatientCode = "";
                        _nProviderID = 0;
                        _ProviderName = "";
                        _PatientMedicalCategory = "";
                        //nExpectedCopay = 0;
                        _patientPhoto = null;
                    }

                    lblPatientName.Text = _PatientName;
                    lblDOB.Text = Convert.ToString(_DateOfBirth.ToString("MM/dd/yyyy")) + "(" + CalculateAge(_DateOfBirth) + "y)";
                    lblGender.Text = _Gender;
                    lblPatientCode.Text = _PatientCode;
                    lblDemoProvider.Text = _ProviderName;
                    lblDemoPatient.Text = _PatientCode + "-" + _PatientName;
                    if (_PatientMedicalCategory == "")
                    {
                        pnlMedCategory.Visible = false;
                        lblDemoMedCat.Text = "";
                    }
                    else
                    {
                        pnlMedCategory.Visible = true;
                        lblDemoMedCat.Text = _PatientMedicalCategory;
                    }

                    if (ShowEMRAlertsOnPatientBanner())
                    {
                        if (_EMRAlert != "")
                        {
                            lblDemoEMRAlerts.Visible = true;
                            lblDemogloEMRAlertsCaption.Visible = true;
                            lblDemoEMRAlerts.ForeColor = Color.FromArgb(212, 0, 114);
                        }
                        else
                        {
                            lblDemoEMRAlerts.Visible = false;
                            //lblDemogloEMRAlertsCaption.Visible = false;
                        }
                        //lblDemogloEMRAlertsCaption.ForeColor = Color.FromArgb(212, 0, 114);
                    }
                    else
                    {
                        lblDemoEMRAlerts.Visible = false;
                        lblDemogloEMRAlertsCaption.Visible = false;
                    }
                    lblDemoEMRAlerts.Text = _EMRAlert;
                    lblDemoNextAppt.Text = _NextAppointment;

                    
                    this.toolTip1.SetToolTip(lblDemoNextAppt, _NextAppointment);
                    this.toolTip1.SetToolTip(lblDemoEMRAlerts, _EMRAlert);
                    this.toolTip1.SetToolTip(lblPatientName, _PatientName);
                    this.toolTip1.SetToolTip(lblDemoProvider, _ProviderName);




                    if (dtCopay != null && dtCopay.Rows.Count > 0)
                    {
                        StringBuilder _sCopayAmount = new StringBuilder();
                        StringBuilder _sCopayDesc = new StringBuilder();

                        if (dtCopay.Rows.Count > 1)
                        {
                            for (int i = 0; i <= dtCopay.Rows.Count - 1; i++)
                            {
                                if ((dtCopay.Rows[i]["nCopay"] != DBNull.Value))
                                {
                                    if (i == dtCopay.Rows.Count - 1)
                                    {
                                        _sCopayAmount.Append(Convert.ToString(dtCopay.Rows[i]["sInsuranceFlag"]) + " [$ " + Convert.ToDecimal(dtCopay.Rows[i]["nCopay"]).ToString("#0.00") + "]");
                                        _sCopayDesc.Append(Convert.ToString(dtCopay.Rows[i]["sInsuranceName"]) + " [$ " + Convert.ToDecimal(dtCopay.Rows[i]["nCopay"]).ToString("#0.00") + "]");
                                    }
                                    else
                                    {
                                        _sCopayAmount.Append(Convert.ToString(dtCopay.Rows[i]["sInsuranceFlag"]) + " [$ " + Convert.ToDecimal(dtCopay.Rows[i]["nCopay"]).ToString("#0.00") + "], ");
                                        _sCopayDesc.Append(Convert.ToString(dtCopay.Rows[i]["sInsuranceName"]) + " [$ " + Convert.ToDecimal(dtCopay.Rows[i]["nCopay"]).ToString("#0.00") + "], ");
                                    }
                                }
                            }
                        }
                        else
                        {
                            _sCopayAmount.Append(" $ " + Convert.ToDecimal(dtCopay.Rows[0]["nCopay"]).ToString("#0.00"));
                            _sCopayDesc.Append(Convert.ToString(dtCopay.Rows[0]["sInsuranceName"]) + " [$ " + Convert.ToDecimal(dtCopay.Rows[0]["nCopay"]).ToString("#0.00") + "]");
                        }
                        if (_sCopayAmount.Length > 35)
                        {
                            lblDemoCopay.Text = _sCopayAmount.ToString().Substring(0, 30) + " . . . ";
                        }
                        else
                        {
                            lblDemoCopay.Text = _sCopayAmount.ToString();
                        }
                        lblDemoCopay.Tag = _sCopayDesc.ToString();
                        toolTip1.SetToolTip(lblDemoCopay, lblDemoCopay.Tag.ToString());
                    }
                    else
                    {
                        lblDemoCopay.Text = "$ 0.00";
                        lblDemoCopay.Tag = "";
                    }




                    if (_patientPhoto != null)
                    {
                        picPAPhoto.Image = _patientPhoto;
                        picPAPhoto.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    else //Default photo will show.
                    {
                        if (_Gender.ToLower() == "male")
                        {
                            this.picPAPhoto.Image = global::gloPMGeneral.Properties.Resources.MalePatient;
                        }
                        else if (_Gender.ToLower() == "female")
                        {
                            this.picPAPhoto.Image = global::gloPMGeneral.Properties.Resources.FemalePatient;
                        }
                        else
                        {
                            this.picPAPhoto.Image = null;
                        }
                        picPAPhoto.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    FillMedicalCategoryHashTable();
                    GetMedicalCategoryImage();
                    #endregion "Patient Demographics"

                    #region "Alerts"

                    if (dtAlerts != null && dtAlerts.Rows.Count > 0)
                    {

                        lblAlerts.Text = dtAlerts.Rows[0]["sAlertName"] == DBNull.Value ? string.Empty : dtAlerts.Rows[0]["sAlertName"].ToString();

                        this.toolTip1.SetToolTip(lblAlerts, dtAlerts.Rows[0]["sAlertName"].ToString());


                        if (dtAlerts.Rows.Count > 1)
                            lblAlertsCap.Text = "PM Alerts (" + dtAlerts.Rows.Count + ") :";
                        else
                            lblAlertsCap.Text = "PM Alerts :";
                    }
                    else
                    {
                        lblAlerts.Text = "";
                        this.toolTip1.SetToolTip(lblAlerts, "");
                        lblAlertsCap.Text = "PM Alerts :";
                    }


                    #endregion

                    #region "Patient Notes"

                    if (dtPatientNotes != null && dtPatientNotes.Rows.Count > 0)
                    {
                        if (dtPatientNotes.Rows[0]["Note"] != DBNull.Value)
                        {
                            lblNotes.Text = dtPatientNotes.Rows[0]["Note"] == DBNull.Value ? string.Empty : dtPatientNotes.Rows[0]["Note"].ToString();
                            this.toolTip1.SetToolTip(lblNotes, dtPatientNotes.Rows[0]["Note"].ToString());
                        }

                    }


                    #endregion

                }
                else
                {

                    _nPatientID = 0;
                    _PatientName = "";
                    _PatientCode = "";
                    _DateOfBirth = DateTime.MinValue;
                    _Gender = "";
                    _PatientCode = "";
                    lblPatientName.Text = _PatientName;
                    lblDOB.Text = Convert.ToString(_DateOfBirth.ToString("MM/dd/yyyy")) + "(" + CalculateAge(_DateOfBirth) + "y)";
                    lblGender.Text = _Gender;
                    lblPatientCode.Text = _PatientCode;
                    lblDemoPatient.Text = "";
                    lblDemoEMRAlerts.Text = "";
                    lblDemoNextAppt.Text = "";

                }


            }
            catch (gloDatabaseLayer.DBException dbEX)
            {
                dbEX.ERROR_Log(dbEX.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {

                if (dtInitialdemographic != null)
                    dtInitialdemographic.Dispose();
                if (dtPatientNotes != null)
                    dtPatientNotes.Dispose();
                if (dtStatementNotes != null)
                    dtStatementNotes.Dispose();
                if (dtAlerts != null)
                    dtAlerts.Dispose();
                //SLR: Free dtSet
                if (dtSet != null)
                {dtSet.Dispose();}
            }
            this.ResumeLayout(false);
        }
        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            //SLR: Added a static function in gloPictureBox
            return gloPictureBox.gloImage.GetImage(byteArrayIn, true);
            //gloPictureBox.gloPictureBox myPicBx = new gloPictureBox.gloPictureBox();
            //System.Drawing.Image _ResultImage = null;
            //try
            //{
            //    myPicBx.byteImage = byteArrayIn;
            //    _ResultImage = (Bitmap)myPicBx.copyFrame(true);
            //}
            //catch (Exception)
            //{

            //}
            //finally
            //{
            //    myPicBx.Dispose();
            //    myPicBx = null;

            //}
            //return _ResultImage;
        }
        public Int32 CalculateAge(DateTime birthDate)
        {
            DateTime now = DateTime.Today;
            Int32 years = now.Year - birthDate.Year;

            if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day))
                --years;
            return years;
        }
        private void FillPatientInfo()
        {
            DataTable dt_PatientInfo=null; //SLR: new not needed
            try
            {
                
                if (!_IsFromModifiedPateint)
                {
                    this.txtCopay.TextChanged -= new System.EventHandler(this.txtCopay_TextChanged);
                    this.txtDeductableAmount.TextChanged -= new System.EventHandler(this.txtDeductableAmount_TextChanged);
                    this.txtCoveragePercent.TextChanged -= new System.EventHandler(this.txtCoveragePercent_TextChanged);
                    this.txtPatientRecordedBenefitsNote.TextChanged -= new System.EventHandler(this.txtPatientRecordedBenefitsNote_TextChanged);
                    this.mskStartDate.TextChanged -= new System.EventHandler(this.mskStartDate_TextChanged);
                    this.mskEndDate.TextChanged -= new System.EventHandler(this.mskEndDate_TextChanged);
                    lblUserAndDateTime.Text = AppSettings.UserName.ToString() + "  " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"); 
                }
                dt_PatientInfo = oclsViewBenefits.getPatientInsuranceDetails(_PatientId, _InsuranceId);
                if (dt_PatientInfo != null && dt_PatientInfo.Rows.Count > 0)
                {
                    txtCopay.Text = Convert.ToString(dt_PatientInfo.Rows[0]["CoPay"]); ;
                    txtDeductableAmount.Text = Convert.ToString(dt_PatientInfo.Rows[0]["Deductableamount"]);
                    txtCoveragePercent.Text = Convert.ToString(dt_PatientInfo.Rows[0]["CoveragePercent"]);
                    if (Convert.ToDecimal(txtCoveragePercent.Text.Trim()) == (Convert.ToInt32(Convert.ToDecimal(txtCoveragePercent.Text.Trim()))))
                    {
                        txtCoveragePercent.Text = (Convert.ToDecimal(txtCoveragePercent.Text.Trim())).ToString("#0");
                    }
                    _sUser = Convert.ToString(dt_PatientInfo.Rows[0]["sUserName"]);
                    if (dt_PatientInfo.Rows[0]["dtReviewedDateTime"] != DBNull.Value)
                        _dtReviewedDateTime = Convert.ToDateTime(dt_PatientInfo.Rows[0]["dtReviewedDateTime"]);
                    if (_sUser == "" && dt_PatientInfo.Rows[0]["dtReviewedDateTime"] == DBNull.Value)
                    {

                        lblLastChanged.Text = "";
                        lblLastchngedDatetime.Text = "";
                        _LastChanged = DateTime.MinValue;
                    }
                    else
                    {
                        //lblLastChanged.Text = _sUser+"  "+_dtReviewedDateTime.ToString("MM/dd/yyyy hh:mm tt") ;
                        //lblLastchngedDatetime.Text =_dtReviewedDateTime.ToString("MM/dd/yyyy hh:mm tt");

                        lblLastChanged.Text = _sUser;
                        this.lblLastchngedDatetime.Location = new System.Drawing.Point(lblLastChanged.Location.X + Convert.ToInt16(_sUser.Length * 9), 81);
                        lblLastchngedDatetime.Text = _dtReviewedDateTime.ToString("MM/dd/yyyy hh:mm tt");
                        _LastChanged = _dtReviewedDateTime.Date;
                    }
                    //if (txtCoveragePercent.Text != "")
                    //{
                    //    txtCoveragePercent.Text = (Convert.ToDecimal(txtCoveragePercent.Text.Trim())).ToString("#0");
                    //}

                    if (dt_PatientInfo.Rows[0]["dtStartdate"] != DBNull.Value)
                    {
                        if (Convert.ToDateTime(dt_PatientInfo.Rows[0]["dtStartdate"]).Date > DateTime.Today.Date)
                            lblPateintBenefitUptoDate.Visible = true;
                        mskStartDate.Text = Convert.ToDateTime(dt_PatientInfo.Rows[0]["dtStartdate"]).ToString("MM/dd/yyyy");
                    }
                    else
                        mskStartDate.Clear();
                    if (dt_PatientInfo.Rows[0]["dtEndDate"] != DBNull.Value)
                    {
                        if (Convert.ToDateTime(dt_PatientInfo.Rows[0]["dtEndDate"]).Date < DateTime.Today.Date)
                            lblPateintBenefitUptoDate.Visible = true;
                        mskEndDate.Text = Convert.ToDateTime(dt_PatientInfo.Rows[0]["dtEndDate"]).ToString("MM/dd/yyyy");
                    }
                    else
                        mskEndDate.Clear();
                    if (dt_PatientInfo.Rows[0]["dtStartdate"] != DBNull.Value && dt_PatientInfo.Rows[0]["dtEndDate"] != DBNull.Value)
                    {
                        if (Convert.ToDateTime(dt_PatientInfo.Rows[0]["dtStartdate"]).Date > DateTime.Today.Date && Convert.ToDateTime(dt_PatientInfo.Rows[0]["dtEndDate"]).Date < DateTime.Today.Date)
                        {
                            lblPateintBenefitUptoDate.Visible = true;
                        }
                    }
                    txtPatientRecordedBenefitsNote.Text = Convert.ToString(dt_PatientInfo.Rows[0]["InsuranceNote"]);
                    if (Convert.ToString(dt_PatientInfo.Rows[0]["sRelationShip"]).ToLower() != "self".ToLower() && Convert.ToBoolean(dt_PatientInfo.Rows[0]["bIsCompnay"]) == true)
                    {
                        lblSubsroberName.Text = Convert.ToString(dt_PatientInfo.Rows[0]["sCompany"]);
                        lblSubscriberDOB.Text = "";

                    }
                    else
                    {
                        lblSubsroberName.Text = Convert.ToString(dt_PatientInfo.Rows[0]["SubFName"]) + "  " + Convert.ToString(dt_PatientInfo.Rows[0]["SubLName"]);
                        // lblName.Text = Convert.ToString(dt_PatientInfo.Rows[0]["SubFName"]) + "  " + Convert.ToString(dt_PatientInfo.Rows[0]["SubLName"]);                   
                        // lblID.Text = Convert.ToString(dt_PatientInfo.Rows[0]["SubscriberID"]);
                        DateTime SubscriberDOB = dt_PatientInfo.Rows[0]["dtDOB"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dt_PatientInfo.Rows[0]["dtDOB"]); ;
                        lblSubscriberDOB.Text = Convert.ToString(_DateOfBirth.ToString("MM/dd/yyyy"));
                    }
                    lblSubscriberID.Text = Convert.ToString(dt_PatientInfo.Rows[0]["SubscriberID"]) + "  " + Convert.ToString(dt_PatientInfo.Rows[0]["sGroup"]);
                    //  lblSubscbrDOB.Text = Convert.ToString(_DateOfBirth.ToString("MM/dd/yyyy"));
                    if (!_IsFromModifiedPateint)
                    {
                        this.txtCopay.TextChanged += new System.EventHandler(this.txtCopay_TextChanged);
                        this.txtDeductableAmount.TextChanged += new System.EventHandler(this.txtDeductableAmount_TextChanged);
                        this.txtCoveragePercent.TextChanged += new System.EventHandler(this.txtCoveragePercent_TextChanged);
                        this.txtPatientRecordedBenefitsNote.TextChanged += new System.EventHandler(this.txtPatientRecordedBenefitsNote_TextChanged);
                        this.mskStartDate.TextChanged += new System.EventHandler(this.mskStartDate_TextChanged);
                        this.mskEndDate.TextChanged += new System.EventHandler(this.mskEndDate_TextChanged);
                    }
                }
                else
                {
                    lblSubsroberName.Text = "";
                    lblSubscriberID.Text = "";
                    lblSubscriberDOB.Text = "";
                }

            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (dt_PatientInfo != null)
                {
                    dt_PatientInfo.Dispose();
                    dt_PatientInfo = null;
                }
            }
        }       
        private void FillInsuranceData()
        {
            try
            {
                this.cmbInsurance.SelectedIndexChanged -= new System.EventHandler(this.cmbInsurance_SelectedIndexChanged);
                DataTable dtInsurances = null;
                dtInsurances = oclsViewBenefits.getPatientInsurance(_PatientId);
                if (dtInsurances != null && dtInsurances.Rows.Count > 0)
                {
                    cmbInsurance.BeginUpdate();
                    cmbInsurance.DataSource = dtInsurances;
                    cmbInsurance.DisplayMember = dtInsurances.Columns["InsuranceName"].ColumnName;
                    cmbInsurance.ValueMember = dtInsurances.Columns["InsuranceID"].ColumnName;
                    cmbInsurance.EndUpdate();
                    if (_InsuranceId != 0)
                        cmbInsurance.SelectedValue = _InsuranceId;
                    else
                        cmbInsurance.SelectedIndex = 0;

                    if (cmbInsurance.SelectedItem !=null)
                        _InsuranceId = Convert.ToInt64(cmbInsurance.SelectedValue);
                    if (_InsuranceId != 0)
                    {
                        if (oclsViewBenefits.getPatientInsurance(_nPatientID, _InsuranceId) > 0)
                        {
                            lblInactiveInsurance.Visible = false;
                        }
                        else
                        {
                            lblInactiveInsurance.Visible = true;
                        }
                    }
                }                
                else
                {
                    _InsuranceId = 0;
                    
                    cmbInsurance.DataSource = null;
                    cmbInsurance.Items.Clear();
                }
                this.cmbInsurance.SelectedIndexChanged += new System.EventHandler(this.cmbInsurance_SelectedIndexChanged);
            }
            catch //(Exception ex)
            {
                return;
            }
        }
        private void FillPlanEligibilityDetails()
        {
            DataTable dt_EligibilityPlanInfo = null; //SLR: new is not needed
            try
            {


                dt_EligibilityPlanInfo = oclsViewBenefits.getPlaneligibilityInsurance(_PatientId, _InsuranceId);
                if (dt_EligibilityPlanInfo != null && dt_EligibilityPlanInfo.Rows.Count > 0)
                {

                    lblEligibilityInsuranceName.Text = Convert.ToString(dt_EligibilityPlanInfo.Rows[0]["InsuranceName"]);
                    labelContact.Text = Convert.ToString(dt_EligibilityPlanInfo.Rows[0]["InsEligibiltyContact"]);
                    lblPhone.Text = Convert.ToString(dt_EligibilityPlanInfo.Rows[0]["InsEligibiltyPhone"]);
                    lblClickable.Links.Clear();
                    lblClickable.Text = Convert.ToString(dt_EligibilityPlanInfo.Rows[0]["InsEligibiltyWesite"]);
                    lblClickable.Links.Add(0, lblClickable.Text.Length, lblClickable.Text);
                    lblPlanEligibilityNote.Text = Convert.ToString(dt_EligibilityPlanInfo.Rows[0]["InsEligibiltyNote"]);                    
                }
                else
                {
                    lblEligibilityInsuranceName.Text = "";
                    labelContact.Text = "";
                    lblPhone.Text="";
                    lblClickable.Text = "";
                    lblPlanEligibilityNote.Text = "";

                }

            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (dt_EligibilityPlanInfo != null)
                {
                    dt_EligibilityPlanInfo.Dispose();
                    dt_EligibilityPlanInfo = null;
                }
            }
        }
        private void FilleEligibilityArea()
        {
            DataSet dtSet = null;
            DataTable dteEligibilityArea = null;
            DataTable dtResponseGrid = null;
            if (_nPatientID > 0 && _InsuranceId > 0)
            {

                dtSet = oclsViewBenefits.GeteEligibilityAreaDetails(_nPatientID, _InsuranceId);
                if (dtSet.Tables.Count > 0)
                {
                    dteEligibilityArea = dtSet.Tables[0];
                    dtResponseGrid = dtSet.Tables[1];
                }
                #region "FilleEligibilityArea"
                if (dteEligibilityArea.Rows.Count > 0)
                {
                    DateTime dtEligibilityCheck = dteEligibilityArea.Rows[0]["dtEligibilityCheck"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dteEligibilityArea.Rows[0]["dtEligibilityCheck"]);
                    _LasteEligibilityCheckDateTime = dtEligibilityCheck.Date;
                    lblDateTimeofLstRsp.Text = Convert.ToString(dtEligibilityCheck.ToString("MM/dd/yyyy hh:mm tt"));
                    labelPayer.Text = dteEligibilityArea.Rows[0]["sPayerName"] == DBNull.Value ? string.Empty : dteEligibilityArea.Rows[0]["sPayerName"].ToString() + "[" + (dteEligibilityArea.Rows[0]["sPayerID"] == DBNull.Value ? string.Empty : dteEligibilityArea.Rows[0]["sPayerID"].ToString())+"]";
                    lblInsuranceType.Text = dteEligibilityArea.Rows[0]["sInsuranceTypeDesc"] == DBNull.Value ? string.Empty : dteEligibilityArea.Rows[0]["sInsuranceTypeDesc"].ToString();
                    lblName.Text = dteEligibilityArea.Rows[0]["sSubscriberName"] == DBNull.Value ? string.Empty : dteEligibilityArea.Rows[0]["sSubscriberName"].ToString();
                    lblID.Text = dteEligibilityArea.Rows[0]["sSubscriberID"] == DBNull.Value ? string.Empty : dteEligibilityArea.Rows[0]["sSubscriberID"].ToString();
                    if (dteEligibilityArea.Rows[0]["nSubscriberDOB"] != null && dteEligibilityArea.Rows[0]["nSubscriberDOB"].ToString() != "0")
                    {
                        DateTime SubscriberDOB = dteEligibilityArea.Rows[0]["nSubscriberDOB"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(gloDateMaster.gloDate.DateAsDateString(Convert.ToInt64(dteEligibilityArea.Rows[0]["nSubscriberDOB"])));
                        if (SubscriberDOB != DateTime.MinValue)
                            lblSubscbrDOB.Text = Convert.ToString(SubscriberDOB.ToString("MM/dd/yyyy"));
                        else
                            lblSubscbrDOB.Text = Convert.ToString(SubscriberDOB.ToString("MM/dd/yyyy"));
                    }
                    lblastRewdDateTime.Text = Convert.ToString(dtEligibilityCheck.ToString("MM/dd/yyyy hh:mm tt"));
                    lblBenefitAsOfDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                    lblPrimaryCareProvider.Text = dteEligibilityArea.Rows[0]["sPrimaryCarePhysicainName"] == DBNull.Value ? string.Empty : dteEligibilityArea.Rows[0]["sPrimaryCarePhysicainName"].ToString();
                    lblErrorNote.Text = dteEligibilityArea.Rows[0]["sErrorNote"] == DBNull.Value ? string.Empty : dteEligibilityArea.Rows[0]["sErrorNote"].ToString();
                  //  lblPrimaryCareProvider.Text = "";
                }
                else
                {
                    lblName.Text = "";
                    lblID.Text = "";
                    lblSubscbrDOB.Text = "";
                    labelPayer.Text = "";
                    lblInsuranceType.Text = "";
                    lblDateTimeofLstRsp.Text = "";
                    lblastRewdDateTime.Text = "";
                    lblPrimsryCareProvider.Text = "";
                    lblastRewdDateTime.Text = "";
                    lblBenefitAsOfDate.Text = "";
                    lblPrimaryCareProvider.Text = "";
                    lblErrorNote.Text = "";
                    _LasteEligibilityCheckDateTime = DateTime.MinValue;
                }
                #endregion
                #region Bind Response Grid
               
                c1Response.BeginUpdate();
                 c1Response.DataSource = dtResponseGrid.DefaultView;
                c1Response.EndUpdate();
                c1Response.Sort(SortFlags.Ascending, COL_BENEFITAMOUNT);
                c1Response.Sort(SortFlags.Ascending, COL_COVERAGE_LEVEL);
                c1Response.Sort(SortFlags.Ascending, COL_SERVICETYPE);              
                #endregion
            }
            else
            {
                lblName.Text = "";
                lblID.Text = "";
                lblSubscbrDOB.Text = "";
                labelPayer.Text = "";
                lblInsuranceType.Text = "";
                lblDateTimeofLstRsp.Text = "";
                lblastRewdDateTime.Text = "";
                lblPrimsryCareProvider.Text = "";
                lblastRewdDateTime.Text = "";
                lblBenefitAsOfDate.Text = "";
                lblPrimaryCareProvider.Text = "";
                lblErrorNote.Text = "";
                _LasteEligibilityCheckDateTime = DateTime.MinValue;
            }
            //SLR: Finally free dteligibilityarea, dtset
            if (dteEligibilityArea != null)
            { dteEligibilityArea.Dispose(); }
            if (dtSet != null)
            { dtSet.Dispose();}
        }
        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {

            if (imageIn == null)
            {
                return null;
            }
                // Image MyImage = Image;
            bool isSaved = false;
            byte[] _myByte = null;//
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {

                try
                {
                    imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                    isSaved = true;
                    _myByte = ms.ToArray();
                }
                catch
                {
                    isSaved = false;

                    try
                    {
                        Bitmap newBmp = new Bitmap(imageIn);
                        if (newBmp != null)
                        {
                            newBmp.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                            _myByte = ms.ToArray();
                            newBmp.Dispose();
                            isSaved = true;
                        }
                    }
                    catch
                    {
                        isSaved = false;
                    }
                }

                if (isSaved == false)
                {
                    System.Text.Encoding enc = System.Text.Encoding.ASCII;
                    _myByte = enc.GetBytes(imageIn.ToString());
                }
            }//using

            return _myByte;            
        }
        private void DesignGrid()
        {
            try
            {
                //c1Response.Rows.Count = 1;
                c1Response.Cols.Count = COL_COUNT;
                c1Response.Rows.Fixed = 1;
               // c1Response.Cols.Fixed = 0;

                c1Response.SetData(0, COL_SRNO, "Sr.No.");
                c1Response.SetData(0, COL_SERVICETYPE, "Service Type");
                c1Response.SetData(0, COL_COVERAGE_LEVEL, "Coverage Type");
                c1Response.SetData(0, COL_IN_NET, "In Net");
                c1Response.SetData(0, COL_AUTH_REQUIRE, "Auth Req.");
                c1Response.SetData(0, COL_BENEFIT, "Benefit");
                c1Response.SetData(0, COL_BENEFITAMOUNT, "Benefit Amount");
                c1Response.SetData(0, COL_DATES, "Dates");
                c1Response.SetData(0, COL_MESSAGE, "Message");

                c1Response.Cols[COL_SRNO].Visible = false;
                c1Response.Cols[COL_BENEFIT].Width = 105;
                c1Response.Cols[COL_COVERAGE_LEVEL].Width = 100;
                c1Response.Cols[COL_SERVICETYPE].Width = 180;
                c1Response.Cols[COL_BENEFITAMOUNT].Width = 150;
                c1Response.Cols[COL_MESSAGE].Width = 150;
                c1Response.Cols[COL_IN_NET].Width = 50;
                c1Response.Cols[COL_AUTH_REQUIRE].Width = 68;
                c1Response.Cols[COL_DATES].Width = 100;

                bool _designWidth = false;
                try
                {
                    gloSettings.GeneralSettings oSetting = new gloSettings.GeneralSettings(_databaseConnectionString);
                    _designWidth = oSetting.LoadGridColumnWidth(c1Response, gloSettings.ModuleOfGridColumn.ViewBenefit, _UserId);
                    oSetting.Dispose();
                }
                catch (Exception) // ex)
                {
                    //ex.ToString();
                    //ex = null;
                }
                
                c1Response.Cols[COL_COVERAGE_LEVEL].DataType = typeof(System.String);
                c1Response.Cols[COL_DATES].DataType = typeof(System.DateTime);
                c1Response.Cols[COL_DATES].Format = "MM/dd/yyyy";

                c1Response.Sort(SortFlags.Ascending, COL_BENEFITAMOUNT);
                c1Response.Sort(SortFlags.Ascending, COL_COVERAGE_LEVEL);
                c1Response.Sort(SortFlags.Ascending, COL_SERVICETYPE);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }
        public static bool ValidDate(string datevalue)
        {
            bool _result = false;
            try
            {

                DateTime _dt = Convert.ToDateTime(datevalue);
                _result = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);

            }
            return _result;
        }
        private bool IsValidDate(object strDate)
        {
            bool Success;
            try
            {
                DateTime validatedDate;
                Success = DateTime.TryParseExact(strDate.ToString(), "MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo, System.Globalization.DateTimeStyles.None, out validatedDate);
                if (validatedDate != null && Success == true)
                {
                    if (validatedDate < DateTime.MaxValue && validatedDate >= Convert.ToDateTime("01/01/1900"))
                    {
                        Success = true;
                    }
                    else
                    {
                        Success = false;
                    }

                }
            }
            catch (FormatException)
            {
                Success = false;

            }
            return Success;
        }
        private int getWidthofListItems(string _text, ComboBox combo)
        {
            //Code Review Changes: Dispose Graphics object
            Graphics g = this.CreateGraphics();
            if (g!=null)
            {
                SizeF s = g.MeasureString(_text, combo.Font);
                width = Convert.ToInt32(s.Width);
                //Dispose graphics object
                g.Dispose();
            }
            
            return width;
        }
        #endregion " Private Methods "

        #region "Form Events"
        private void changeHeightAsPerResolution()
        {
            //this.Height = 
            Int32 myScreenHeight = Convert.ToInt32(Screen.PrimaryScreen.WorkingArea.Height * 0.99);
            if (myScreenHeight < this.Height)
            {
                this.Height = myScreenHeight;
            }
            //Int32 myScreenWidth = Convert.ToInt32(Screen.PrimaryScreen.WorkingArea.Width * 1.1);
            //if (myScreenWidth > this.width)
            //{
            //    this.width = myScreenWidth;
            //}
        }
        private void frmViewBenefit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_IsSaveClick)
            {
                DialogResult res = MessageBox.Show("Do you want to save changes to this record? ", _messageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                    if (res == DialogResult.Yes)
                    {
                        this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.frmViewBenefit_FormClosing); 
                        tsb_OK_Click(sender, e);
                    }
                    else if (res == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        picPatient_Cards.OnZoomChanged -= new gloPictureBox.gloPictureBox.ZoomChanged(TrackBarZoomChange);

                        this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.frmViewBenefit_FormClosing);                       
                        this.Close();
                    }
                
            }
            else
            {
                picPatient_Cards.OnZoomChanged -= new gloPictureBox.gloPictureBox.ZoomChanged(TrackBarZoomChange);

                this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.frmViewBenefit_FormClosing);
                this.Close();
            }
            //SLR: Free oPatientCards, oClsViewBenefits
            if (oPatientCards != null)
            { 

                oPatientCards.Dispose();
                oPatientCards = null;
            }
            if (oclsViewBenefits != null)
            {
                oclsViewBenefits.Dispose();
                oclsViewBenefits = null;
            }
        }
         private void tls_btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
           
        }      
         private void tsb_OK_Click(object sender, EventArgs e)
        {
            this.mskStartDate.TextChanged -= new System.EventHandler(this.mskStartDate_TextChanged);
            this.mskEndDate.TextChanged -= new System.EventHandler(this.mskEndDate_TextChanged);
            try
            {
                bool _IsSave = false;
                _isValidated = true;
                pnlToolStrip.Select();
                if (!_isValidated)
                    return;
                mskEndDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                mskStartDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                if (mskEndDate.Text.Replace("/","").Replace("_","").Trim().Length > 0)   // masking for start date is already done... 
                {
                    // Comparision between start date and End date...
                    mskEndDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;                   
                    
                    if (mskStartDate.Text != "")
                    {
                        mskStartDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;   
                        if (Convert.ToDateTime((mskEndDate.Text)) < Convert.ToDateTime((mskStartDate.Text)) && Convert.ToDateTime(mskEndDate.Text) != Convert.ToDateTime(mskStartDate.Text))
                        {
                            MessageBox.Show("Start date should be less than End date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mskEndDate.Focus();
                            return;
                        }
                    }
                }

                mskEndDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                mskStartDate.TextMaskFormat = MaskFormat.IncludeLiterals;

                _nCopay = Convert.ToDecimal(txtCopay.Text == string.Empty ? "0" : txtCopay.Text);
                _Deductible = Convert.ToDecimal(txtDeductableAmount.Text == string.Empty ? "0" : txtDeductableAmount.Text);
                _CoInsurance = Convert.ToDecimal(txtCoveragePercent.Text == string.Empty ? "0" : txtCoveragePercent.Text);
                if (IsValidDate(mskStartDate.Text))
                    _dtStartDate = Convert.ToDateTime(mskStartDate.Text);
                else
                {
                    _dtStartDate = DateTime.Now;
                    _IsNotStartDate = true;
                }

                if (IsValidDate(mskEndDate.Text))
                    _dtendDate = Convert.ToDateTime(mskEndDate.Text);
                else
                {
                    _dtendDate = DateTime.Now;
                    _IsNotEndDate = true;
                }
                _sInsuranceNote = txtPatientRecordedBenefitsNote.Text;
                if (chkMarkReviewed.Checked)
                {
                    if (_InsuranceId == 0)
                    {
                        MessageBox.Show("Select insurance plan.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    _IsSave = oclsViewBenefits.UpdatePatientInsurancesBenefit(_nPatientID, _InsuranceId, _nCopay, _Deductible, _CoInsurance, _dtStartDate, _dtendDate, _IsNotStartDate, _IsNotEndDate, _sInsuranceNote, AppSettings.UserName.ToString(), DateTime.Now);
                }
                else
                {
                    if (_InsuranceId == 0)
                    {
                        MessageBox.Show("Select insurance plan.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    _IsSave = oclsViewBenefits.UpdatePatientInsurancesBenefit(_nPatientID, _InsuranceId, _nCopay, _Deductible, _CoInsurance, _dtStartDate, _dtendDate, _IsNotStartDate, _IsNotEndDate, _sInsuranceNote);
                }
                    _IsSaveClick = true;
                    this.Close();
               
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);

            }


        }        
         private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
         private void InsCardMoveLast_Click(object sender, EventArgs e)
        {
            if (!(oPatientCards == null))
            {
                if (oPatientCards.Count > 0)
                {
                    if (oPatientCards.Count == 1)
                    {
                        oPatientCards.Clear();
                        //SLR:This line can be moved to after 'oPatientCards.Clear() and also freed after getpatientcards instead of at end.
                        gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(_databaseConnectionString); 
                        oPatientCards = ogloPatient.GetPatientCards(_PatientId);
                        if (ogloPatient != null)
                        {
                            ogloPatient.Dispose();
                        }
                    }
                    //SLR: if count >=1, then only..
                    if (oPatientCards.Count > 0)
                    {
                        MyPictureBoxControl = imageToByteArray((Image)oPatientCards[oPatientCards.Count - 1].CardImage);
                        picPatient_Cards.Visible = false;
                        picPatient_Cards.byteImage = MyPictureBoxControl;
                        picPatient_Cards.Visible = true;
                        lblScanDate.Text = oPatientCards[oPatientCards.Count - 1].ScannedDateTime.ToString("MM/dd/yyyy");
                        picPatient_Cards.Tag = oPatientCards.Count - 1;
                        picPatient_Cards.Refresh();
                    }
                }
            }            
        }

         private void InsCardMoveNext_Click(object sender, EventArgs e)
        {

            
            if (!(oPatientCards == null))
            {
                if (oPatientCards.Count > 0)
                {
                    if (oPatientCards.Count == 1)
                    {
                        oPatientCards.Clear();
                        gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(_databaseConnectionString);
                        oPatientCards = ogloPatient.GetPatientCards(_PatientId);
                        if (ogloPatient != null)
                        {
                            ogloPatient.Dispose();
                        }
                    }
                    if ((oPatientCards.Count > 0) && (Convert.ToInt16(picPatient_Cards.Tag.ToString()) + 1) <= (oPatientCards.Count - 1)) //SLR: && count >=1
                    {
                        MyPictureBoxControl = imageToByteArray((Image)oPatientCards[Convert.ToInt16(picPatient_Cards.Tag.ToString()) + 1].CardImage);
                        picPatient_Cards.Visible = false;
                        picPatient_Cards.byteImage = MyPictureBoxControl;
                        picPatient_Cards.Visible = true;
                        lblScanDate.Text = oPatientCards[Convert.ToInt16(picPatient_Cards.Tag.ToString()) + 1].ScannedDateTime.ToString("MM/dd/yyyy");
                        picPatient_Cards.Tag = Convert.ToInt16(picPatient_Cards.Tag.ToString()) + 1;
                    }
                }
            }
        }

         private void InsCardMovePrevious_Click(object sender, EventArgs e)
        {

            
            if (!(oPatientCards == null))
            {
                if (oPatientCards.Count > 0)
                {
                    if (oPatientCards.Count == 1)
                    {
                        oPatientCards.Clear();
                        gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(_databaseConnectionString);
                        oPatientCards = ogloPatient.GetPatientCards(_PatientId);
                        if (ogloPatient != null)
                        {
                            ogloPatient.Dispose();
                        }
                    }
                    if ((oPatientCards.Count > 0) && (Convert.ToInt16(picPatient_Cards.Tag.ToString()) - 1) >= 0) //SLR: && count >=1
                    {
                        MyPictureBoxControl = imageToByteArray((Image)oPatientCards[Convert.ToInt16(picPatient_Cards.Tag.ToString()) - 1].CardImage);
                        picPatient_Cards.Visible = false;
                        picPatient_Cards.byteImage = MyPictureBoxControl;
                        picPatient_Cards.Visible = true;
                        lblScanDate.Text = oPatientCards[Convert.ToInt16(picPatient_Cards.Tag.ToString()) - 1].ScannedDateTime.ToString("MM/dd/yyyy");
                        picPatient_Cards.Tag = Convert.ToInt16(picPatient_Cards.Tag.ToString()) - 1;

                    }
                }
            }
            
        }

         private void InsCardMovefirst_Click(object sender, EventArgs e)
        {
            if (!(oPatientCards == null))
            {
                if (oPatientCards.Count > 0)
                {
                    if (oPatientCards.Count == 1)
                    {
                        oPatientCards.Clear();
                        gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(_databaseConnectionString);
                        oPatientCards = ogloPatient.GetPatientCards(_PatientId);
                        if (ogloPatient != null)
                        {
                            ogloPatient.Dispose();
                        }
                    }
                    //SLR: IF count >=1 then
                    if (oPatientCards.Count > 0)
                    {
                        MyPictureBoxControl = imageToByteArray((Image)oPatientCards[0].CardImage);
                        picPatient_Cards.Visible = false;
                        picPatient_Cards.byteImage = MyPictureBoxControl;
                        picPatient_Cards.Visible = true;
                        lblScanDate.Text = oPatientCards[0].ScannedDateTime.ToString("MM/dd/yyyy");
                        picPatient_Cards.Tag = 0;
                    }

                }
            }
        }
         private void TrackBar_ValueChanged(object sender, EventArgs e)
        {
            if (semaPhoreTosuppressValueChange) return;
            if (TrackBar != null)
            {
                

           
                    picPatient_Cards.ZoomValueForTrackBar=TrackBar.Value;
                
            }
        }

         private void TrackBar_ValueChanged_1(object sender, EventArgs e)
        {
            if (semaPhoreTosuppressValueChange) return;
            if (TrackBar != null)
            {



                picPatient_Cards.ZoomValueForTrackBar = TrackBar.Value;

            }
        }

         private void TrackbarPlus_Click(object sender, EventArgs e)
        {
            if (TrackBar != null)
            {
                if (TrackBar.Value < 44)
                {
                    TrackBar.Value++;
                }
            }
        }

         private void TrackbarMinus_Click(object sender, EventArgs e)
        {
            if (TrackBar != null)
            {
                if (TrackBar.Value > -44)
                {
                    TrackBar.Value--;
                }
            }
        }

         private void btn_ModifyPatient_Click(object sender, EventArgs e)
        {
            gloSecurity.gloSecurity oSecurity = new gloSecurity.gloSecurity(_databaseConnectionString);

            try
            {
                if (oSecurity.isPatientLock(_nPatientID, true) == false && _nPatientID > 0)
                {
                    gloPatient.frmSetupPatient ofrmSetupPatient = new gloPatient.frmSetupPatient(_nPatientID, _databaseConnectionString);
                    ofrmSetupPatient.ShowDialog(this);
                    ofrmSetupPatient.Dispose();
                    _IsFromModifiedPateint = true;
                    DisplayPatient(_nPatientID);
                    Refresh();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oSecurity != null)
                    oSecurity.Dispose();
            }
        }

         private void btn_Alerts_Click(object sender, EventArgs e)
        {
            if (_nPatientID > 0)
            {
                try
                {
                    frmPatientAlerts ofrmPatientAlerts = new frmPatientAlerts(_databaseConnectionString, _nPatientID);
                    ofrmPatientAlerts.ShowDialog(this);
                    ofrmPatientAlerts.Dispose();
                    DisplayPatient(_nPatientID);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }

         private void lblClickable_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }

         private void cmbInsurance_SelectedIndexChanged(object sender, EventArgs e)
        {
            _InsuranceId = Convert.ToInt64(cmbInsurance.SelectedValue);
            chkMarkReviewed.Checked = false;
            _IsFromModifiedPateint = false;
            if (oclsViewBenefits.getPatientInsurance(_nPatientID, _InsuranceId) > 0)
            {
                lblInactiveInsurance.Visible = false;
            }
            else
            {
                lblInactiveInsurance.Visible = true;
            }
             Refresh();
        }

         private void txtCopay_Validating(object sender, CancelEventArgs e)
        {
            if ((txtCopay.Text.Trim() == "") || (txtCopay.Text.Trim() == "."))
            {
                if (txtCopay.Text.Trim() == ".")
                {
                    txtCopay.Text = "0.00";
                    _isValidated = true;
                }
            }
            else
            {
                if ((Convert.ToDecimal(txtCopay.Text.Trim())).ToString("#0.00").Length > 6 && txtCopay.Text.Contains(".") == false)
                {
                    //txtCopay.Text = (Convert.ToDecimal(txtCopay.Text.Trim())).ToString("#0.00");
                    MessageBox.Show(this, "Enter valid amount of copay.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCopay.Focus();
                    _isValidated = false;
                    return;
                }
                else
                {
                    if (txtCopay.Text.EndsWith(".") == false && txtCopay.Text.Length <= 6)
                    {
                        txtCopay.Text = (Convert.ToDecimal(txtCopay.Text.Trim())).ToString("#0.00");
                        _isValidated = true;
                    }
                    else
                    {
                        MessageBox.Show(this, "Enter valid amount of copay.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCopay.Focus();
                        _isValidated = false;
                        return;
                    }
                }


            }
        }

         private void txtCopay_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!((e.KeyChar >= Convert.ToChar(48) && e.KeyChar <= Convert.ToChar(57)) || e.KeyChar == Convert.ToChar(8)))
            //{
            //    e.Handled = true;
            //}
            if (e.KeyChar != Convert.ToChar(8) && e.KeyChar != Convert.ToChar(46))
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), @"^[0-9]*$") == false)
                {
                    e.Handled = true;
                }
                else
                {
                    if (e.KeyChar != Convert.ToChar(46) && txtCopay.Text.Length >= 3 && txtCopay.SelectedText == "")
                    {
                        if (txtCopay.Text.Contains(".") == false)
                            e.Handled = true;

                    }
                }
            }
            else
                if (txtCopay.Text.Contains(".") && e.KeyChar == Convert.ToChar(46))
                {
                    e.Handled = true;
                }

            //if (e.KeyChar == Convert.ToChar(13))
            //{
            //    CalculateRemainingAmount();
            //    //txtCheckAmount.Select();
            //    //txtCheckAmount.Focus();
            //}
        }

         private void txtDeductableAmount_Validating(object sender, CancelEventArgs e)
        {
            if ((txtDeductableAmount.Text.Trim() == "") || (txtDeductableAmount.Text.Trim() == "."))
            {
                if (txtDeductableAmount.Text.Trim() == ".")
                {
                    txtDeductableAmount.Text = "0.00";
                }
            }
            else
            {
                // resolving TFS issue id - 1435
                //txtDeductableAmount.Text = (Convert.ToDecimal(txtDeductableAmount.Text.Trim())).ToString("#0.00");

                if ((Convert.ToDecimal(txtDeductableAmount.Text.Trim())).ToString("#0.00").Length > 8 && txtDeductableAmount.Text.Contains(".") == false)
                {

                    MessageBox.Show(this, "Enter valid amount of deductable.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDeductableAmount.Focus();
                    _isValidated = false;
                    return;
                }
                else
                {
                    if (txtDeductableAmount.Text.EndsWith(".") == false && txtDeductableAmount.Text.Length <= 8)
                    {
                        txtDeductableAmount.Text = (Convert.ToDecimal(txtDeductableAmount.Text.Trim())).ToString("#0.00");
                        _isValidated = true;
                    }
                    else
                    {
                        MessageBox.Show(this, "Enter valid amount of deductable.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDeductableAmount.Focus();
                        _isValidated = false;
                        return;
                    }// end
                }

            }
        }

         private void txtDeductableAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!((e.KeyChar >= Convert.ToChar(48) && e.KeyChar <= Convert.ToChar(57)) || e.KeyChar == Convert.ToChar(8)))
            //{
            //    e.Handled = true;
            //}
            if (e.KeyChar != Convert.ToChar(8) && e.KeyChar != Convert.ToChar(46))
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), @"^[0-9]*$") == false)
                {
                    e.Handled = true;
                }
                else
                {
                    if (e.KeyChar != Convert.ToChar(46) && txtDeductableAmount.Text.Length >= 5 && txtDeductableAmount.SelectedText == "")
                    {
                        if (txtDeductableAmount.Text.Contains(".") == false)
                            e.Handled = true;

                    }
                }
            }
            else
                if (txtDeductableAmount.Text.Contains(".") && e.KeyChar == Convert.ToChar(46))
                {
                    e.Handled = true;
                }
        }

         private void txtCoveragePercent_Validating(object sender, CancelEventArgs e)
        {
            this.txtCoveragePercent.TextChanged -= new System.EventHandler(this.txtCoveragePercent_TextChanged);
            if ((txtCoveragePercent.Text.Trim() == "") || (txtCoveragePercent.Text.Trim() == "."))
            {
                if (txtCoveragePercent.Text.Trim() == ".")
                {
                    txtCoveragePercent.Text = "0.00";
                }
            }
            else if (Convert.ToDecimal(txtCoveragePercent.Text) > 100)
            {
                MessageBox.Show(this, "Coverage % should be less than 100.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCoveragePercent.Focus();
                //return false;
                _isValidated = false; 
            }
            else
            {
                txtCoveragePercent.Text = (Convert.ToDecimal(txtCoveragePercent.Text.Trim())).ToString("#0.00");

                if (Convert.ToDecimal(txtCoveragePercent.Text.Trim()) == (Convert.ToInt32(Convert.ToDecimal(txtCoveragePercent.Text.Trim()))))
                {
                    txtCoveragePercent.Text = (Convert.ToDecimal(txtCoveragePercent.Text.Trim())).ToString("#0");
                }
            }
            this.txtCoveragePercent.TextChanged += new System.EventHandler(this.txtCoveragePercent_TextChanged);
        }

         private void txtCoveragePercent_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= Convert.ToChar(48) && e.KeyChar <= Convert.ToChar(57)) || e.KeyChar == Convert.ToChar(8) || e.KeyChar == Convert.ToChar(46)))
            {
                e.Handled = true;
            }
            else if (!(e.KeyChar == Convert.ToChar(8))) //if not backspsace
            {
                if (txtCoveragePercent.SelectionStart > txtCoveragePercent.Text.IndexOf("."))
                {
                    if (txtCoveragePercent.Text.Contains(".") == true)
                    {
                        if (txtCoveragePercent.Text.Substring(txtCoveragePercent.Text.IndexOf(".") + 1, txtCoveragePercent.Text.Length - (txtCoveragePercent.Text.IndexOf(".") + 1)).Length == 2 && txtCoveragePercent.SelectedText == "")
                        {
                            e.Handled = true;
                        }
                    }
                    else
                    {
                        if (txtCoveragePercent.Text.Length >= 3 && txtCoveragePercent.SelectedText == "")
                        {
                            e.Handled = true;
                        }
                    }
                }
            }
            if (e.KeyChar == Convert.ToChar(46) && txtCoveragePercent.Text.Contains("."))
            {
                e.Handled = true;
            }
        }

         private void mskStartDate_MouseClick(object sender, MouseEventArgs e)
         {
             this.mskStartDate.TextChanged -= new System.EventHandler(this.mskStartDate_TextChanged);
             this.mskStartDate.TextChanged -= new System.EventHandler(this.mskStartDate_TextChanged);
             ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
             if (((MaskedTextBox)sender).Text.Trim() == "")
             {
                 ((MaskedTextBox)sender).SelectionStart = 0;
                 ((MaskedTextBox)sender).SelectionLength = 0;
             }
             this.mskStartDate.TextChanged += new System.EventHandler(this.mskStartDate_TextChanged);
         }

         private void mskEndDate_Validating(object sender, CancelEventArgs e)
         {
             this.mskEndDate.TextChanged -= new System.EventHandler(this.mskEndDate_TextChanged);
             mskEndDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;

             if (mskEndDate.Text.Length > 0 && mskEndDate.MaskCompleted == false)
             {                
                 if (!ValidDate(mskEndDate.Text))
                 {
                     MessageBox.Show("Please enter valid end date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                     _isValidated = false;
                     e.Cancel = true;
                 }
             }
             else
             {
                 _isValidated = true;
                 if (mskEndDate.MaskCompleted == true)
                 {
                     try
                     {
                         mskEndDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                         DateTime dtTemp = Convert.ToDateTime(mskEndDate.Text);
                         if (Convert.ToDateTime(mskEndDate.Text.Trim()) == DateTime.MinValue || Convert.ToDateTime(mskEndDate.Text.Trim()) < Convert.ToDateTime("01/01/1900"))
                         {
                             MessageBox.Show("Please enter valid end date.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                             _isValidated = false;
                             mskEndDate.Focus();
                             e.Cancel = true;
                         }
                         else
                         {
                             if (mskStartDate.MaskCompleted == true)
                             {
                                 if (Convert.ToDateTime(mskStartDate.Text.Trim()).Date > DateTime.Today.Date && Convert.ToDateTime(mskEndDate.Text.Trim()).Date < DateTime.Today.Date)
                                 {
                                     lblPateintBenefitUptoDate.Visible = true;
                                     this.mskEndDate.TextChanged += new System.EventHandler(this.mskEndDate_TextChanged);

                                     return;
                                 }
                             }
                             if (Convert.ToDateTime(mskEndDate.Text.Trim()).Date < DateTime.Today.Date)
                             {
                                 lblPateintBenefitUptoDate.Visible = true;
                                 this.mskEndDate.TextChanged += new System.EventHandler(this.mskEndDate_TextChanged);
                                 return;
                             }
                             else
                                 lblPateintBenefitUptoDate.Visible = false;
                             if (mskStartDate.MaskCompleted == true && Convert.ToDateTime(mskStartDate.Text.Trim()).Date > DateTime.Today.Date)
                             {
                                
                                 lblPateintBenefitUptoDate.Visible = true;
                                 this.mskEndDate.TextChanged += new System.EventHandler(this.mskEndDate_TextChanged);
                                 return;
                             }
                             else
                                 lblPateintBenefitUptoDate.Visible = false;
                         }
                     }
                     catch (Exception) // ex)
                     {
                         MessageBox.Show("Please enter valid end date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                         _isValidated = false;
                         mskEndDate.Focus();
                         e.Cancel = true;
                         //ex.ToString();
                         //ex = null;
                     }
                 }
                 else
                 {
                     mskStartDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                     if (mskStartDate.MaskCompleted == true)
                     {
                         if (Convert.ToDateTime(mskStartDate.Text.Trim()).Date > DateTime.Today.Date)
                             lblPateintBenefitUptoDate.Visible = true;
                         else
                             lblPateintBenefitUptoDate.Visible = false;
                     }
                     else
                       lblPateintBenefitUptoDate.Visible = false;
                 }
             }
             this.mskEndDate.TextChanged += new System.EventHandler(this.mskEndDate_TextChanged);
         }

         private void MaskTextBox_MouseClick(object sender, MouseEventArgs e)
         {
             this.mskEndDate.TextChanged -= new System.EventHandler(this.mskEndDate_TextChanged);
             this.mskEndDate.TextChanged -= new System.EventHandler(this.mskEndDate_TextChanged);
             ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
             if (((MaskedTextBox)sender).Text.Trim() == "")
             {
                 ((MaskedTextBox)sender).SelectionStart = 0;
                 ((MaskedTextBox)sender).SelectionLength = 0;
             }
             this.mskEndDate.TextChanged += new System.EventHandler(this.mskEndDate_TextChanged);
         }

         private void txtCopay_TextChanged(object sender, EventArgs e)
         {
             chkMarkReviewed.Checked = true;
             lblUserAndDateTime.Text = AppSettings.UserName.ToString() + "  " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"); ;
             // lblLastChanged.Text = _sUser + Environment.NewLine + _dtReviewedDateTime.ToShortDateString();
         }

         private void txtDeductableAmount_TextChanged(object sender, EventArgs e)
         {
             chkMarkReviewed.Checked = true;
             lblUserAndDateTime.Text = AppSettings.UserName.ToString() + "  " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"); ;
            // lblLastChanged.Text = _sUser + Environment.NewLine + _dtReviewedDateTime.ToShortDateString("");
         }

         private void txtPatientRecordedBenefitsNote_TextChanged(object sender, EventArgs e)
         {
             chkMarkReviewed.Checked = true;
             lblUserAndDateTime.Text = AppSettings.UserName.ToString() + "  " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"); ;
            //   lblLastChanged.Text = _sUser + Environment.NewLine + _dtReviewedDateTime.ToShortDateString(MM/dd/yyyy hh:mm tt");
         }

         private void chkMarkReviewed_CheckedChanged(object sender, EventArgs e)
         {
             DateTime todayDate = DateTime.Now;
            // if (chkMarkReviewed.Checked == true)
                 lblUserAndDateTime.Text = AppSettings.UserName.ToString() + "  " + todayDate.ToString("MM/dd/yyyy hh:mm tt");
            // else
                 //lblUserAndDateTime.Text = "";
         }

         private void mskStartDate_Validating(object sender, CancelEventArgs e)
         {
             this.mskStartDate.TextChanged -= new System.EventHandler(this.mskStartDate_TextChanged);
             mskStartDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;

             if (mskStartDate.Text.Length > 0 && mskStartDate.MaskCompleted == false)
             {                
                 if (!ValidDate(mskStartDate.Text))
                 {
                     MessageBox.Show("Please enter valid start date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                     _isValidated = false;
                     e.Cancel = true;
                 }
             }
             else
             {
                 _isValidated = true;
                 if (mskStartDate.MaskCompleted == true)
                 {
                     try
                     {
                         mskStartDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                         DateTime dtTemp = Convert.ToDateTime(mskStartDate.Text);
                         if (Convert.ToDateTime(mskStartDate.Text.Trim()) == DateTime.MinValue || Convert.ToDateTime(mskStartDate.Text.Trim()) < Convert.ToDateTime("01/01/1900"))
                         {
                             MessageBox.Show("Please enter valid start date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                             _isValidated = false;
                             mskStartDate.Focus();
                             e.Cancel = true;
                         }
                         else
                         {
                             if (mskEndDate.MaskCompleted == true)
                             {
                                 if (Convert.ToDateTime(mskStartDate.Text.Trim()).Date > DateTime.Today.Date && Convert.ToDateTime(mskEndDate.Text.Trim()).Date < DateTime.Today.Date)
                                 {
                                     lblPateintBenefitUptoDate.Visible = true;
                                     this.mskStartDate.TextChanged += new System.EventHandler(this.mskStartDate_TextChanged);
                                     return;
                                 }
                             }

                             if (Convert.ToDateTime(mskStartDate.Text.Trim()).Date > DateTime.Today.Date)
                             {
                                 lblPateintBenefitUptoDate.Visible = true;
                                 this.mskStartDate.TextChanged += new System.EventHandler(this.mskStartDate_TextChanged);
                                 return;
                             }
                             else
                                 lblPateintBenefitUptoDate.Visible = false;
                             if ((mskEndDate.MaskCompleted == true && Convert.ToDateTime(mskEndDate.Text.Trim()).Date < DateTime.Today.Date))
                             {
                                 lblPateintBenefitUptoDate.Visible = true;
                                 this.mskStartDate.TextChanged += new System.EventHandler(this.mskStartDate_TextChanged);
                                 return;
                             }
                             else
                                 lblPateintBenefitUptoDate.Visible = false;
                         }
                     }
                     catch (Exception) // ex)
                     {
                         MessageBox.Show("Please enter valid start date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                         _isValidated = false;
                         mskStartDate.Focus();
                         e.Cancel = true;
                         //ex.ToString();
                         //ex = null;
                     }
                 }
                 else
                 {
                     if (mskEndDate.MaskCompleted == true)
                     {
                         mskEndDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                         if (Convert.ToDateTime(mskEndDate.Text.Trim()).Date < DateTime.Today.Date)
                             lblPateintBenefitUptoDate.Visible = true;
                         else
                             lblPateintBenefitUptoDate.Visible = false;
                     }
                     else
                         lblPateintBenefitUptoDate.Visible = false;
                 }
             }
             this.mskStartDate.TextChanged += new System.EventHandler(this.mskStartDate_TextChanged);
         }

         private void txtCoveragePercent_TextChanged(object sender, EventArgs e)
         {
             chkMarkReviewed.Checked = true;
             lblUserAndDateTime.Text = AppSettings.UserName.ToString() + "  " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");
         }

         private void mskEndDate_TextChanged(object sender, EventArgs e)
         {
             chkMarkReviewed.Checked = true;
             lblUserAndDateTime.Text = AppSettings.UserName.ToString() + "  " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");
           //  this.mskEndDate.TextChanged -= new System.EventHandler(this.mskEndDate_TextChanged);
             if (mskEndDate.MaskCompleted == true)
             {
                 try
                 {
                     mskEndDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                     DateTime dtTemp = Convert.ToDateTime(mskEndDate.Text);

                     {
                         if (mskStartDate.MaskCompleted == true)
                         {
                             if (Convert.ToDateTime(mskStartDate.Text.Trim()).Date > DateTime.Today.Date && Convert.ToDateTime(mskEndDate.Text.Trim()).Date < DateTime.Today.Date)
                             {
                                 lblPateintBenefitUptoDate.Visible = true;
                                 return;
                             }
                         }
                         if (Convert.ToDateTime(mskEndDate.Text.Trim()).Date < DateTime.Today.Date)
                         {
                             lblPateintBenefitUptoDate.Visible = true;                            
                             return;
                         }
                         else
                             lblPateintBenefitUptoDate.Visible = false;
                         if (mskStartDate.MaskCompleted == true && Convert.ToDateTime(mskStartDate.Text.Trim()).Date > DateTime.Today.Date)
                         {

                             lblPateintBenefitUptoDate.Visible = true;                           
                             return;
                         }
                         else
                             lblPateintBenefitUptoDate.Visible = false;
                     }
                 }
                 catch //(Exception ex)
                 {

                     _isValidated = false;
                     mskEndDate.Focus();
                     return;
                     //ex.ToString();
                     //ex = null;
                 }
             }
             else
             {
                 mskStartDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                 if (mskStartDate.MaskCompleted == true)
                 {
                     if (Convert.ToDateTime(mskStartDate.Text.Trim()).Date > DateTime.Today.Date)
                         lblPateintBenefitUptoDate.Visible = true;
                     else
                         lblPateintBenefitUptoDate.Visible = false;
                 }
                 else
                     lblPateintBenefitUptoDate.Visible = false;
             }
             //this.mskEndDate.TextChanged += new System.EventHandler(this.mskEndDate_TextChanged);
         }

         private void mskStartDate_TextChanged(object sender, EventArgs e)
        {
             chkMarkReviewed.Checked = true;
             lblUserAndDateTime.Text = AppSettings.UserName.ToString() + "  " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");
           //  this.mskStartDate.TextChanged -= new System.EventHandler(this.mskStartDate_TextChanged);
             if (mskStartDate.MaskCompleted == true)
             {
                 try
                 {
                     mskStartDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                     DateTime dtTemp = Convert.ToDateTime(mskStartDate.Text);
                     {
                         if (mskEndDate.MaskCompleted == true)
                         {
                             if (Convert.ToDateTime(mskStartDate.Text.Trim()).Date > DateTime.Today.Date && Convert.ToDateTime(mskEndDate.Text.Trim()).Date < DateTime.Today.Date)
                             {
                                 lblPateintBenefitUptoDate.Visible = true;
                                 return;
                             }
                         }

                         if (Convert.ToDateTime(mskStartDate.Text.Trim()).Date > DateTime.Today.Date)
                         {
                             lblPateintBenefitUptoDate.Visible = true;
                             return;
                         }
                         else
                             lblPateintBenefitUptoDate.Visible = false;
                         if ((mskEndDate.MaskCompleted == true && Convert.ToDateTime(mskEndDate.Text.Trim()).Date < DateTime.Today.Date))
                         {
                             lblPateintBenefitUptoDate.Visible = true;
                             return;
                         }
                         else
                             lblPateintBenefitUptoDate.Visible = false;
                     }
                 }
                 catch //(Exception ex)
                 {

                     _isValidated = false;
                     mskStartDate.Focus();
                     return;
                     //ex.ToString();
                     //ex = null;
                 }
             }
             else
             {
                 mskEndDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                 if (mskEndDate.MaskCompleted == true)                 {
                   
                     if (Convert.ToDateTime(mskEndDate.Text.Trim()).Date < DateTime.Today.Date)
                         lblPateintBenefitUptoDate.Visible = true;
                     else
                         lblPateintBenefitUptoDate.Visible = false;
                 }
                 else
                     lblPateintBenefitUptoDate.Visible = false;
             }
            // this.mskStartDate.TextChanged += new System.EventHandler(this.mskStartDate_TextChanged);
         }
         private void tsb_Save_Click(object sender, EventArgs e)
         {
             this.mskStartDate.TextChanged -= new System.EventHandler(this.mskStartDate_TextChanged);
             this.mskEndDate.TextChanged -= new System.EventHandler(this.mskEndDate_TextChanged);
             try
             {
                 _IsNotEndDate = false;
                 _IsNotStartDate = false;
                 bool _IsSave = false;
                 pnlToolStrip.Select();
                 if (!_isValidated)
                 {
                     this.mskStartDate.TextChanged += new System.EventHandler(this.mskStartDate_TextChanged);
                     this.mskEndDate.TextChanged += new System.EventHandler(this.mskEndDate_TextChanged);
                     return; //SLR: before retun do we not to add the handler again?
                 }
                 this.mskStartDate.TextChanged -= new System.EventHandler(this.mskStartDate_TextChanged);
                 this.mskEndDate.TextChanged -= new System.EventHandler(this.mskEndDate_TextChanged);
                 _nCopay = Convert.ToDecimal(txtCopay.Text == string.Empty ? "0" : txtCopay.Text);
                 _Deductible = Convert.ToDecimal(txtDeductableAmount.Text == string.Empty ? "0" : txtDeductableAmount.Text);
                 _CoInsurance = Convert.ToDecimal(txtCoveragePercent.Text == string.Empty ? "0" : txtCoveragePercent.Text);
                 if (IsValidDate(mskStartDate.Text))
                     _dtStartDate = Convert.ToDateTime(mskStartDate.Text);
                 else
                 {
                     _dtStartDate = DateTime.Now;
                     _IsNotStartDate = true;
                 }

                 if (IsValidDate(mskEndDate.Text))
                     _dtendDate = Convert.ToDateTime(mskEndDate.Text);
                 else
                 {
                     _dtendDate = DateTime.Now;
                     _IsNotEndDate = true;
                 }
                 mskEndDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                 mskStartDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                 if (mskEndDate.Text.Length > 0)   // masking for start date is already done... 
                 {
                     // Comparision between start date and End date...
                     mskEndDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                     if (!IsValidDate(mskEndDate.Text) && !IsValidDate(mskStartDate.Text))
                     {
                         MessageBox.Show("Please enter valid end date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                         mskEndDate.Focus();
                         return;
                     }
                     if (mskStartDate.Text != "")
                     {
                         mskStartDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                         if (Convert.ToDateTime((mskEndDate.Text)) < Convert.ToDateTime((mskStartDate.Text)) && Convert.ToDateTime(mskEndDate.Text) != Convert.ToDateTime(mskStartDate.Text))
                         {
                             MessageBox.Show("Start date should be less than End date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                             mskEndDate.Focus();
                             return;
                         }
                     }
                 }

                 mskEndDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                 mskStartDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                 _sInsuranceNote = txtPatientRecordedBenefitsNote.Text;
                 if (chkMarkReviewed.Checked)
                 {
                     if (_InsuranceId == 0)
                     {
                         MessageBox.Show("Select insurance plan.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                         return;
                     }
                     _IsSave = oclsViewBenefits.UpdatePatientInsurancesBenefit(_nPatientID, _InsuranceId, _nCopay, _Deductible, _CoInsurance, _dtStartDate, _dtendDate, _IsNotStartDate, _IsNotEndDate, _sInsuranceNote, AppSettings.UserName.ToString(), DateTime.Now);
                 }
                 else
                 {
                     if (_InsuranceId == 0)
                     {
                         MessageBox.Show("Select insurance plan.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                         return;
                     }
                     _IsSave = oclsViewBenefits.UpdatePatientInsurancesBenefit(_nPatientID, _InsuranceId, _nCopay, _Deductible, _CoInsurance, _dtStartDate, _dtendDate, _IsNotStartDate, _IsNotEndDate, _sInsuranceNote);
                 }

                 _IsSaveClick = true;
                 Refresh();
             }
             catch (Exception ex)
             {
                 gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);

             }
             finally
             {
                 this.mskStartDate.TextChanged += new System.EventHandler(this.mskStartDate_TextChanged);
                 this.mskEndDate.TextChanged += new System.EventHandler(this.mskEndDate_TextChanged);
             }
         }

         private void c1Response_MouseMove(object sender, MouseEventArgs e)
         {
             gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, (C1FlexGrid)sender, e.Location);
         }
         
         private void btnDown_Click(object sender, EventArgs e)
         {
             btnUp.Visible = true; ;
             btnDown.Visible = false;
             pnlPatStripBottom.Visible = true;
             panel18.Dock = DockStyle.Top;
             //panel2.Visible = true;
           //  panel5.Visible = true;
             //panel3.Visible = true;
             
         }

         private void btnUp_Click(object sender, EventArgs e)
         {
             btnUp.Visible = false;
             btnDown.Visible = true;
             pnlPatStripBottom.Visible = false;
             panel18.Dock = DockStyle.Fill;
             //panel2.Visible = false;
         //    panel5.Visible = false;
             //panel3.Visible = false;
             
         }
         private void cmbInsurance_MouseEnter(object sender, EventArgs e)
         {
             if (cmbInsurance.SelectedItem != null)
             {
                 if (getWidthofListItems(Convert.ToString(((DataRowView)cmbInsurance.Items[cmbInsurance.SelectedIndex])["InsuranceName"]), cmbInsurance) >= cmbInsurance.DropDownWidth - 18)
                     this.toolTip1.SetToolTip(cmbInsurance, Convert.ToString(((DataRowView)cmbInsurance.Items[cmbInsurance.SelectedIndex])["InsuranceName"]));//, cmbReportingCategory, 0, cmbReportingCategory.Bottom - 98);
                 else
                     this.toolTip1.SetToolTip(cmbInsurance, "");

             }
         }

         private void tsbEligibilityCheck_Click(object sender, EventArgs e)
         {
             ////Code to check eligibility of Provider to Insurance.
             //frmEligibilityForm ofrmEligibilityForm = new frmEligibilityForm(gloPMGlobal.DatabaseConnectionString, _CurrentPatientId, tempInsId);
             //ofrmEligibilityForm.ShowDialog(Me);
             //ofrmEligibilityForm.Dispose();

             gloPatient.gloPatientEiligibility ogloEiligibility = null;
             gloPatient.EiligiblityData EData = null;
             Int64 _patientProviderid = 0;
             Int64 _insuranceContactid = 0;
             gloSettings.GeneralSettings oSettings = null;
             Object _objResult = null;
             string _result = "";
             int nANSIVersion = 0;

             try
             {
                 tsbEligibilityCheck.Enabled = false;
                 this.Cursor = Cursors.WaitCursor;
                 ogloEiligibility = new gloPatient.gloPatientEiligibility(_databaseConnectionString);
                 _patientProviderid = ogloEiligibility.GetPatientProviderID(_PatientId);
                 _insuranceContactid = ogloEiligibility.GetInsuranceContactID(_InsuranceId, _PatientId);


                 
                 if (Convert.ToString(_PatientId).Trim() != "" && Convert.ToString(_patientProviderid).Trim() != ""
                     && Convert.ToString(_InsuranceId).Trim() != "" && Convert.ToString(_insuranceContactid).Trim() != "")
                 {
                     EData = ogloEiligibility.GetEiligibilityData(_PatientId, _patientProviderid, _InsuranceId, _insuranceContactid);

                     oSettings = new gloSettings.GeneralSettings(_databaseConnectionString);
                     oSettings.GetSetting("INSURANCEELIGIBILITY", out _objResult);
                     _result = Convert.ToString(_objResult);

                     nANSIVersion = oSettings.getANSIVersion(_insuranceContactid, "ELIGIBILITY", _ClinicId);
                     //SLR: Free objResult, oSettings
                     _objResult = null;                     
                     if (oSettings !=null)
                     {
                         oSettings.Dispose();                         
                     }
                     if (EData != null)
                     {
                         if (_result == "" || _result == "BYCODE")
                         {
                             if (nANSIVersion == 0)
                             {
                                 MessageBox.Show("Eligibility Requests ANSI Version has not been set. Eligibility may not proceed. Please review in gloPM Admin.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                             }
                             else if (nANSIVersion == (int)ANSIVersions.ANSI_4010)
                             {
                                 ogloEiligibility.EDIGeneration_270(EData);
                                 Refresh();
                             }
                             else if (nANSIVersion == (int)ANSIVersions.ANSI_5010)
                             {
                                 ogloEiligibility.EDI5010Generation_270(EData, ANSIVersions.ANSI_5010);
                                 Refresh();
                             }
                         }
                         else if (_result == "BYSERVICE")
                         {

                             if (nANSIVersion == 0)
                             {
                                 MessageBox.Show("Eligibility Requests ANSI Version has not been set.Eligibility may not proceed.Please review in gloPM Admin.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                             }
                             else if (nANSIVersion == (Int64)ANSIVersions.ANSI_4010)
                             {
                                 ogloEiligibility.EDIGeneration_270(EData, _InsuranceId, _patientProviderid);
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
                 tsbEligibilityCheck.Enabled = true;
                 this.Cursor = Cursors.Default;
                 if (ogloEiligibility != null) { ogloEiligibility.Dispose(); }
                 if (EData != null) { EData.Dispose(); }
             }
         }
         private void c1Response_AfterResizeColumn(object sender, RowColEventArgs e)
         {
             gloSettings.GeneralSettings oSetting = new gloSettings.GeneralSettings(_databaseConnectionString);
             oSetting.SaveGridColumnWidth(c1Response, gloSettings.ModuleOfGridColumn.ViewBenefit, _UserId);
             oSetting.Dispose();
         }
        #endregion "Form Events"

         private void btnInsuranceUp_Click(object sender, EventArgs e)
         {
             btnInsuranceDown.Visible = true; ;
             btnInsuranceUp.Visible = false;
             panel2.Visible = false;
            
             
         }

         private void btnInsuranceDown_Click(object sender, EventArgs e)
         {
             btnInsuranceDown.Visible = false; ;
             btnInsuranceUp.Visible = true;
             panel2.Visible = true;
            
         }

         private void btn_ModityPatient_MouseHover(object sender, EventArgs e)
         {
             btn_ModifyPatient.BackgroundImage = global::gloPMGeneral.Properties.Resources.PatientHover;
             btn_ModifyPatient.BackgroundImageLayout = ImageLayout.Center;
             if (toolTip1 == null)
             {
                 toolTip1 = new ToolTip();
             }
             toolTip1.SetToolTip(btn_ModifyPatient, "Modify Patient");
         }

         private void btn_ModityPatient_MouseLeave(object sender, EventArgs e)
         {
             btn_ModifyPatient.BackgroundImage = global::gloPMGeneral.Properties.Resources.Patient;
             btn_ModifyPatient.BackgroundImageLayout = ImageLayout.Center;
         }
         
    }
  
}
