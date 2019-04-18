
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using gloBilling.Common;
using gloSettings;
using C1.Win.C1FlexGrid;
using System.Linq;
using gloGlobal;
namespace gloBilling
{
    public partial class gloBillingTransaction : UserControl
    {

        #region  " Public & Private Variables "
        //**private TextBox oTextBox;
        private const string _POSName = "Office";

        public Int32 _NoOfDiagnosis = 8;
        public Int32 _NoOfServiceLines = 30;
        private Int32 _NoOfModifiers = 2;

        private Int32 _InitialNoOfLines = 0;
        private Int64 _DefaultTOSID = 0;
        private string _DefaultTOSCode = "";
        private string _DefaultTOSDesc = "";

        private Int64 _DefaultPOSID = 0;
        private string _DefaultPOSCode = "";
        private string _DefaultPOSDesc = "";

        private Int64 _DefaultRenderingProviderID = 0;
        private string _DefaultRenderringProviderName = "";

        public Int64 _PatientProviderID = 0;
        private string _PatientProviderName = "";
        public Int64 _nContactID = 0;
        public Int64 _nCurrentContactID = 0;
        public Int64 _ClinicID = 0;
        public string _DatabaseConnectionString = "";
        private string _messageBoxCaption = "gloPM";

        public Int64 _PatientId = 0;

        public Int64 BillingProviderID = 0;
        public Int64 _facilityID = 0;

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        public gloGridListControl ogloGridListControl = null;

        //    C1.Win.C1FlexGrid.CellStyle csCurrency;

        string _dxCodeForDistinct = "";
        string _dxDescriptionForDistinct = "";
        //     bool _IsNonFaclity = false;

        private Int64 _transactionLineNo = 0;
        private Int64 _transactionDetailId = 0;

        private bool _IsCheckInvalidICD9 = false;
        private bool _IsScrubber = false;
        private bool _IsReferralCPT = false;

        TrnCtrlColValChangeEventArg evtModifyDx = new TrnCtrlColValChangeEventArg();
        RowColEventArgs evtModifyDxRowCol = null;

        private bool _showLabColumn = false;
        private string _facilityCLIANo = "";
        private Int64 _facilityPOS = 0;

        private bool _showFeeScheduleWarning = true;
        private FacilityType _DefaultChargesType = FacilityType.None;

        private bool _showAllowedColumn = false;
        private bool _showPOSColumn = false;
        private bool _showEMGColumn = false;
        private bool _showTilldateColumn = false;
        private bool _showTillDateColumnUseNullDate = true;
        private bool _showSplitClaimToPatient = false;

        private bool _showInsurance = false;

        private bool _AllowDragDrop = true;

        private bool _isItemSelectionCall = false;

        //**private bool _isLastAddedRowSorted = false;
        //**private int _sortedRowIndex = 0;

        private bool _isControlInDesignMode = false;

        private bool _IsFeeSchedule = false;
        private Int64 _FeeScheduleID = 0;

        private bool _AllowChargeModification = true;

        private bool _AutoSort = true;

        private bool _IsOpenForModify = false;

        private bool _IsFormloading = false;

        public Boolean _bISCptBlank = false;

        private string _sServiceDate = null;
        private string nColseDate;
        //Hold Fee Schedule
        private FeeScheduleType _Fee_ScheduleType = FeeScheduleType.None;
        private Int64 _Fee_ScheduleID = 0;

        private int iPreCol = 0;
        private int iPreRow = 0;
        public bool bIsDxMsgShown = false;

        public bool IsMODLoadedFromScrubber = false;

        public bool IsDXLoadedFromScrubber = false;

        public int IcdCodeType = gloGlobal.gloICD.CodeRevision.ICD9.GetHashCode();
        private string _DefaultCPT_CLIAno = "";
        private bool _bDefaultSelf = false;
        private bool _bDuplicateClaimWarning = false; //Admin setting added for duplicate claim warning
        private DataTable _dtCPTActivationDates = null;
        private string _sMammogramCertNo = "";
        #endregion " Public & Private Variables "

        #region " Property Procedures "        

        public gloICD.CodeRevision SelectedICD
        {
            get
            {
                if (IcdCodeType == 9)
                {
                    return gloICD.CodeRevision.ICD9;
                }
                else if (IcdCodeType == 10)
                {
                    return gloICD.CodeRevision.ICD10;
                }

                return gloICD.CodeRevision.ICD9;
            }
        }

        public string ColseDate
        {
            get { return nColseDate; }
            set
            {
                nColseDate = value;
            }
        }
        public bool IsDosChange { get; set; }
        public bool IsFirstDosChange { get; set; }
        public string MaxPaymentDate
        {
            get;
            set;
        }

        public bool LineAlteration
        {
            get { return _AllowChargeModification; }
            set
            {
                _AllowChargeModification = value;
            }
        }

        public bool PerformValidation { get; set; }
        public bool IsPrimarySelfPay { get; set; }

        public Int64 FacilityID
        {
            get { return _facilityID; }
            set
            {
                _facilityID = value;

                #region " Check for facility/non-facility "

                try
                {

                    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
                    Object retVal = new object();
                    string _sqlQuery = "";
                    oDB.Connect(false);
                    _sqlQuery = "select ISNULL(nFacilityType,0) AS nFacilityType from BL_Facility_MST WITH(NOLOCK) where nFacilityID = " + _facilityID + " ";
                    retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                    if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }

                    if (retVal != null && Convert.ToString(retVal).Trim() != "")
                    {
                        switch (((FacilityType)Convert.ToInt32(retVal)))
                        {
                            case FacilityType.None:
                                //    _IsNonFaclity = false;
                                break;
                            case FacilityType.Facility:
                                // _IsNonFaclity = false;
                                break;
                            case FacilityType.NonFacility:
                                //  _IsNonFaclity = true;
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                    ex = null;
                }

                #endregion
            }
        }
        public DateTime CurrentDOS { get; set; }

        public Int64 DefaultTOSID
        {
            get { return _DefaultTOSID; }
            set { _DefaultTOSID = value; }
        }

        public Int64 DefaultPOSID
        {
            get { return _DefaultPOSID; }
            set { _DefaultPOSID = value; }
        }

        public Int64 DefaultRenderingProviderID
        {
            get { return _DefaultRenderingProviderID; }
            set { _DefaultRenderingProviderID = value; }
        }

        public string DefaultRenderringProviderName
        {
            get { return _DefaultRenderringProviderName; }
            set { _DefaultRenderringProviderName = value; }
        }

        public Int32 InitialNofRows
        {
            get { return _InitialNoOfLines; }
            set { _InitialNoOfLines = value; ROW_COUNT = value; }
        }

        public Int32 CurrentTransactionLine
        {
            get { return c1Transaction.RowSel; }
        }

        public Int32 CurrentColumn
        {
            get { return c1Transaction.ColSel; }
        }
        private string _sFirstCPTLineCLIANo = "";
        public String FirstCLIANo 
        {
            get { return _sFirstCPTLineCLIANo; }
            set { _sFirstCPTLineCLIANo = value; }
        }

        public Int64 PatientID
        {
            get { return _PatientId; }
            set { _PatientId = value; }
        }

        public Int64 PatientProviderID
        {
            get { return _PatientProviderID; }
            set { _PatientProviderID = value; }
        }

        public string PatientProviderName
        {
            get { return _PatientProviderName; }
            set { _PatientProviderName = value; }
        }

        public Int64 TransactionLineNo
        {
            get { return _transactionLineNo; }
            set { _transactionLineNo = value; }
        }

        public Int64 TransactionDetailID
        {
            get { return _transactionDetailId; }
            set { _transactionDetailId = value; }
        }

        public bool ShowLabColumn
        {
            get { return _showLabColumn; }
            set { _showLabColumn = value; }
        }


        public bool ShowAllowedColumn
        {
            get
            {
                return _showAllowedColumn;
            }
        }

        public bool ShowTillDateColumn
        {
            get
            {
                return _showTilldateColumn;
            }
        }

        public bool ShowPOSColumn
        {
            get
            {
                return _showPOSColumn;
            }
        }

        public bool ShowEMGColumn
        {
            get
            {
                return _showEMGColumn;
            }
        }

        public bool ShowFeeScheduleWarning
        {
            get { return _showFeeScheduleWarning; }
            set { _showFeeScheduleWarning = value; }
        }

        public string FacilityCLIANo
        {
            get { return _facilityCLIANo; }
            set { _facilityCLIANo = value; }
        }

        public Int64 FacilityPOS
        {
            get { return _facilityPOS; }
            set { _facilityPOS = value; }
        }

        public FacilityType DefaultChargesType
        {
            get { return _DefaultChargesType; }
            set { _DefaultChargesType = value; }
        }

        public bool IsFeeSchedule
        {
            get { return _IsFeeSchedule; }
            set { _IsFeeSchedule = value; }
        }

        public Int64 FeeScheduleID
        {
            get { return _FeeScheduleID; }
            set { _FeeScheduleID = value; }
        }

        public bool ShowInsurance
        {
            get { return _showInsurance; }
            set { _showInsurance = value; }
        }

        public bool IsSettingsForProvider { get; set; }

        public ExternalChargesType TreatmentType { get; set; }

        public bool bAllowDragDrop
        {
            get { return _AllowDragDrop; }
            set
            {
                if (value == true)
                {
                    c1Transaction.DragMode = DragModeEnum.AutomaticMove;
                    c1Transaction.AllowDragging = AllowDraggingEnum.Rows;
                    c1Transaction.AllowSorting = AllowSortingEnum.MultiColumn;
                }
                else
                {
                    c1Transaction.DragMode = DragModeEnum.Manual;
                    c1Transaction.AllowDragging = AllowDraggingEnum.None;
                    c1Transaction.AllowSorting = AllowSortingEnum.None;
                }
                _AllowDragDrop = value;
            }
        }


        public bool AutoSort
        {
            get { return _AutoSort; }
            set { _AutoSort = value; }
        }

        public bool IsOpenForModify
        {
            get { return _IsOpenForModify; }
            set { _IsOpenForModify = value; }
        }

        public bool IsFormLoading
        {
            get { return _IsFormloading; }
            set { _IsFormloading = value; }
        }
        public FeeScheduleType Fee_ScheduleType
        {
            get { return _Fee_ScheduleType; }
            set { _Fee_ScheduleType = value; }
        }

        public Int64 Fee_ScheduleID
        {
            get { return _Fee_ScheduleID; }
            set { _Fee_ScheduleID = value; }
        }
        public Int64 Col_Dos { get { return COL_DATEFROM; } }
        public Int32 Col_DxCode1 { get { return COL_DX1_CODE; } }
        public Int32 Col_DxCode2 { get { return COL_DX2_CODE; } }
        public Int32 Col_DxCode3 { get { return COL_DX3_CODE; } }
        public Int32 Col_DxCode4 { get { return COL_DX4_CODE; } }

        public Int32 Col_DxDescription1 { get { return COL_DX1_DESC; } }
        public Int32 Col_DxDescription2 { get { return COL_DX2_DESC; } }
        public Int32 Col_DxDescription3 { get { return COL_DX3_DESC; } }
        public Int32 Col_DxDescription4 { get { return COL_DX4_DESC; } }


        public Int64 Col_Selected { get { return c1Transaction.ColSel; } }
        public Boolean IsPlanOrAdminEPSDTEnabled { get; set; }

        public bool ShowSplitClaimToPatient
        {
            get
            {
                return _showSplitClaimToPatient;
            }
        }
        //Admin setting added for duplicate claim warning
        public bool bDuplicateClaimWarning
        {
            get { return _bDuplicateClaimWarning; }
            set { _bDuplicateClaimWarning = value; }
        }

        public string sMammogramCertNo
        {
            get { return _sMammogramCertNo; }
            set { _sMammogramCertNo = value; }

        }
        
        #endregion " Property Procedures "

        #region " Constructor "

        public gloBillingTransaction()
        {
            InitializeComponent();

            try
            {
                ReadApplicationSettings();
                ReadBillingSettings();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void ReadApplicationSettings()
        {
            #region " Retrieve ClinicID from AppSettings "

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

            #endregion

            #region " Retrive Database Connection String for appSettings "

            if (appSettings["DataBaseConnectionString"] != null)
            {
                if (appSettings["DataBaseConnectionString"] != "")
                {
                    _DatabaseConnectionString = Convert.ToString(appSettings["DataBaseConnectionString"]);
                }
                else
                {
                    _DatabaseConnectionString = "";
                }
            }
            else
            {
                _DatabaseConnectionString = "";
            }

            #endregion
        }

        private void ReadBillingSettings()
        {
            gloDatabaseLayer.DBLayer oDB = null;
            DataTable _dtBillingControlSettings = null;

            try
            {
                oDB = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
                oDB.Connect(false);
                oDB.Retrive("BL_GET_BillingControlSettings", out _dtBillingControlSettings);
                oDB.Disconnect();

                if (_dtBillingControlSettings != null && _dtBillingControlSettings.Rows.Count > 0)
                {
                    foreach (DataRow dr in _dtBillingControlSettings.Rows)
                    {
                        switch (Convert.ToString(dr["sSettingsName"]).Trim())
                        {
                            case "SHOWLABCOL":
                                {
                                    if (dr["sSettingsValue"] != DBNull.Value && Convert.ToString(dr["sSettingsValue"]).Trim() != "")
                                    {
                                        if (Convert.ToString(dr["sSettingsValue"]).Trim() == "1")
                                        { _showLabColumn = true; }
                                    }
                                }
                                break;
                            case "DEFAULTFEECHARGES":
                                {
                                    if (dr["sSettingsValue"] != DBNull.Value && Convert.ToString(dr["sSettingsValue"]).Trim() != "")
                                    {
                                        int _chargestype = 0;
                                        _chargestype = Convert.ToInt32(dr["sSettingsValue"]);
                                        if (_chargestype == FacilityType.Facility.GetHashCode())
                                        { DefaultChargesType = FacilityType.Facility; }
                                        else if (_chargestype == FacilityType.NonFacility.GetHashCode())
                                        { DefaultChargesType = FacilityType.NonFacility; }
                                        else if (_chargestype == FacilityType.None.GetHashCode())
                                        { DefaultChargesType = FacilityType.None; }
                                    }
                                }
                                break;
                            case "ShowPOSColumn":
                                {
                                    if (dr["sSettingsValue"] != DBNull.Value && Convert.ToString(dr["sSettingsValue"]).Trim() != "")
                                    {
                                        if (Convert.ToString(dr["sSettingsValue"]).Trim() == "1") { _showPOSColumn = true; }
                                    }
                                }
                                break;
                            case "ShowAllowedAmount":
                                {
                                    if (dr["sSettingsValue"] != DBNull.Value && Convert.ToString(dr["sSettingsValue"]).Trim() != "")
                                    {
                                        if (Convert.ToString(dr["sSettingsValue"]).Trim() == "1") { _showAllowedColumn = true; }
                                    }
                                }
                                break;
                            case "ShowEMGColumn":
                                {
                                    if (dr["sSettingsValue"] != DBNull.Value && Convert.ToString(dr["sSettingsValue"]).Trim() != "")
                                    {
                                        if (Convert.ToString(dr["sSettingsValue"]).Trim() == "1") { _showEMGColumn = true; }
                                    }
                                }
                                break;
                            case "NoOfModifiers":
                                {
                                    if (dr["sSettingsValue"] != DBNull.Value && Convert.ToString(dr["sSettingsValue"]).Trim() != "")
                                    { _NoOfModifiers = Convert.ToInt16(dr["sSettingsValue"]); }
                                }
                                break;
                            case "ShowTillDateColumn":
                                {
                                    if (dr["sSettingsValue"] != DBNull.Value && Convert.ToString(dr["sSettingsValue"]).Trim() != "")
                                    {
                                        if (Convert.ToString(dr["sSettingsValue"]).Trim() == "1") { _showTilldateColumn = true; }
                                    }
                                }
                                break;
                            case "IsReferralCPT":
                                {
                                    if (dr["sSettingsValue"] != DBNull.Value && Convert.ToString(dr["sSettingsValue"]).Trim() != "")
                                    {
                                        _IsReferralCPT = Convert.ToBoolean(dr["sSettingsValue"]);
                                    }
                                }
                                break;
                            case "IsUseScrubber":
                                {
                                    if (dr["sSettingsValue"] != DBNull.Value && Convert.ToString(dr["sSettingsValue"]).Trim() != "")
                                    {
                                        _IsScrubber = Convert.ToBoolean(dr["sSettingsValue"]);
                                    }
                                }
                                break;
                            case "IsCheckInvalidICD9":
                                {
                                    if (dr["sSettingsValue"] != DBNull.Value && Convert.ToString(dr["sSettingsValue"]).Trim() != "")
                                    {
                                        _IsCheckInvalidICD9 = Convert.ToBoolean(dr["sSettingsValue"]);
                                    }
                                }
                                break;
                            case "SplitClaimToPatient":
                                {
                                    if (dr["sSettingsValue"] != DBNull.Value && Convert.ToString(dr["sSettingsValue"]).Trim() != "")
                                    {
                                        if (Convert.ToString(dr["sSettingsValue"]).Trim() == "True") { _showSplitClaimToPatient = true; }
                                    }
                                }
                                break;
                            //Admin setting added for duplicate claim warning
                            case "bDuplicateClaimWarning":
                                {
                                    if (dr["sSettingsValue"] != DBNull.Value && Convert.ToString(dr["sSettingsValue"]).Trim() != "")
                                    {
                                        if (Convert.ToString(dr["sSettingsValue"]).Trim() == "True") { _bDuplicateClaimWarning = true; }
                                    }
                                }
                                break;
                        }
                    }
                }

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                throw dbEx;
            }
            finally
            {
                if (_dtBillingControlSettings != null) { _dtBillingControlSettings.Dispose(); }
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }
        }

        #endregion " Constructor "

        #region "Columns"
        const int COL_NO = 0;
        const int COL_TRANSACTIONID = 1;

        const int COL_DATEFROM = 2;
        const int COL_DATETO = 3;

        const int COL_POSCODE = 4;
        const int COL_POSDESC = 5;
        const int COL_POS_BTN = 6;

        const int COL_TOSCODE = 7;
        const int COL_TOSDESC = 8;
        const int COL_TOS_BTN = 9;

        //Added By Pramod Nair For EMG
        const int COL_ISEMG = 10;


        const int COL_CPT_CODE = 11;
        const int COL_CPT_DESC = 12;
        const int COL_CPT_BTN = 13;

        public const int COL_DX1_CODE = 14;
        public const int COL_DX1_DESC = 15;
        const int COL_DX1_BTN = 16;
        public const int COL_DX2_CODE = 17;
        public const int COL_DX2_DESC = 18;
        const int COL_DX2_BTN = 19;
        public const int COL_DX3_CODE = 20;
        public const int COL_DX3_DESC = 21;
        const int COL_DX3_BTN = 22;
        public const int COL_DX4_CODE = 23;
        public const int COL_DX4_DESC = 24;
        const int COL_DX4_BTN = 25;
        const int COL_DX5_CODE = 26;
        const int COL_DX5_DESC = 27;
        const int COL_DX5_BTN = 28;
        const int COL_DX6_CODE = 29;
        const int COL_DX6_DESC = 30;
        const int COL_DX6_BTN = 31;
        const int COL_DX7_CODE = 32;
        const int COL_DX7_DESC = 33;
        const int COL_DX7_BTN = 34;
        const int COL_DX8_CODE = 35;
        const int COL_DX8_DESC = 36;
        const int COL_DX8_BTN = 37;

        const int COL_DX1_PTR = 38;
        const int COL_DX2_PTR = 39;
        const int COL_DX3_PTR = 40;
        const int COL_DX4_PTR = 41;
        const int COL_DX5_PTR = 42;
        const int COL_DX6_PTR = 43;
        const int COL_DX7_PTR = 44;
        const int COL_DX8_PTR = 45;

        const int COL_MOD1_CODE = 46;
        const int COL_MOD1_DESC = 47;
        const int COL_MOD1_BTN = 48;
        const int COL_MOD2_CODE = 49;
        const int COL_MOD2_DESC = 50;
        const int COL_MOD2_BTN = 51;
        const int COL_MOD3_CODE = 52;
        const int COL_MOD3_DESC = 53;
        const int COL_MOD3_BTN = 54;
        const int COL_MOD4_CODE = 55;
        const int COL_MOD4_DESC = 56;
        const int COL_MOD4_BTN = 57;

        const int COL_CHARGES = 58;
        const int COL_UNIT = 59;
        const int COL_TOTAL = 60;
        const int COL_ALLOWED = 61;
        const int COL_PROVIDER_ID = 62;
        const int COL_PROVIDER = 63;
        const int COL_PROVIDER_BTN = 64;
        const int COL_NOTE_BTN = 65;
        const int COL_NOTE_DATA = 66;
        const int COL_NOTE_TYPE = 67;

        //20080917
        //COL_INSURANCEID,COL_INSURANCENAME

        const int COL_INSURANCEID = 68;
        const int COL_INSURANCENAME = 69;
        const int COL_INSSELF_PAYMODE = 70;
        //

        const int COL_TRANSACTION_DETAIL_ID = 71;

        //Code added on 20090511 by - Sagar Ghodke
        //Columns added to implement CLIA & sent to claim file funcationality

        const int COL_ISLABCPT = 72;
        const int COL_AUTHORIZATIONNO = 73;
        const int COL_SENTTOCLAIM = 74;

        const int COL_HOLD = 75;
        const int COL_HOLD_REASON = 76;

        const int COL_LINEPRIMARY_DXCODE = 77;
        const int COL_LINEPRIMARY_DXDESC = 78;

        //End Code add 20090511,Sagar Ghodke

        //..** Code added on 20090624 by Sagar Ghodke
        const int COL_ACTUAL_ALLOWED = 79;
        //..** End code add on 20090624 by Sagar Ghodke


        const int COL_MST_TRANSACTIONID = 80;
        const int COL_MST_TRANSACTIONDTLID = 81;

        const int COL_BILLEDAMOUNT = 82;

        const int COL_FEESCHEDULETYPE = 83;
        const int COL_FEESCHEDULEID = 84;
        const int COL_FACILITYTYPE = 85;


        const int COL_NDCCODE = 86;
        const int COL_NDCUNIT = 87;
        const int COL_NDCUNITCODE = 88;
        const int COL_NDCUNITDESCRITION = 89;
        const int COL_PRESCRIPTION = 90;
        const int COL_PRESCRIPTIONDESC = 91;

        const int COL_SERVICESCREENING = 92;
        const int COL_SERVICERESULTSCREENING = 93;
        const int COL_FAMILYPLANNINGINDICATOR = 94;

        //const int COL_COUNT = 95;

        #region "Anesthesia"

        const int COL_ANES_ID = 95;
        const int COL_ANES_STARTDATE = 96;
        const int COL_ANES_ENDDATE = 97;
        const int COL_ANES_TOTALMIN = 98;
        const int COL_ANES_MINPERUNIT = 99;
        const int COL_ANES_TIMEUNITS = 100;
        const int COL_ANES_BASEUNITS = 101;
        const int COL_ANES_OTHERUNITS = 102;
        const int COL_ANES_TOTALUNITS = 103;
        const int COL_ANES_ISANESTHESIA = 104;
        const int COL_ANES_ISAUTOCALCULATE = 105;

        #endregion


        const int COL_EMRTREATMENTLINENO = 106;
        //line added by sameer for split claim option on New Charges Entry  11-11-2013
        const int COL_SELFCLAIM = 107;

        const int COL_COUNT = 108;

        const int VISIBLE_Dx_COUNT = 8;
        const int VISIBLE_DxPTR_COUNT = 8;

        const int VISIBLE_MOD_COUNT = 2;
        //const int VISIBLE_CPT_COUNT = 6;
        const int VISIBLE_CPT_COUNT = 30;

        const int INITIAL_INNERCONTROL_WIDTH = 337;
        private int ROW_COUNT = 9;
        #endregion

        #region "Delegate"

        //public delegate void onGridAfterEdit(object sender, RowColEventArgs e);
        //public event onGridAfterEdit onGrid_AfterEdit;

        public delegate void onGridCellChanged(object sender, RowColEventArgs e);
        public event onGridCellChanged onGrid_CellChanged;

        public delegate void CLIAEnter();
        public event CLIAEnter CLIA_Enter;

        public delegate void onInsCPTDxModChanged(object sender, RowColEventArgs e, TrnCtrlColValChangeEventArg e2);
        public event onInsCPTDxModChanged onInsCPTDxMod_Changed;

        public delegate void onGridSelChanged(object sender, RangeEventArgs e);
        public event onGridSelChanged onGrid_SelChanged;

        public delegate void showLineNote(object sender, RowColEventArgs e, GeneralNotes Notes);
        public event showLineNote show_LineNotes;

        public delegate void dateChanged(object sender, RowColEventArgs e);
        public event dateChanged date_Changed;

        //public delegate void onCPTChargesLoaded(object sender, FacilityType ChargesType);
        //public event onCPTChargesLoaded onCPTCharges_Load;

        #endregion "Delegate"

        #region " Control Load Event "

        private void gloBillingTransaction_Load(object sender, EventArgs e)
        {
            DesignTransactionGrid();
            DesignTotalGrid();
            //GetAlphaIISettings();
        }

        #endregion

        #region " Design & ReInitialize Grid "

        //public void ReinitilizeControl()
        //{
        //    DesignTransactionGrid();
        //}

        public void ReinitilizeControl()
        {

            _isControlInDesignMode = true;

            try
            {
                if (c1Transaction.Rows.Count > 1)
                { c1Transaction.Rows.RemoveRange(1, c1Transaction.Rows.Count - 1); }

                c1Transaction.Rows.Count = ROW_COUNT;
                c1Transaction.Rows.Fixed = 1;
                c1Transaction.Cols.Count = COL_COUNT;
                c1Transaction.Cols.Fixed = 1;

                for (int i = 0; i <= c1Transaction.Rows.Count - 1; i++)
                {
                    c1Transaction.Rows[i].Height = 23;
                    if (i > 0)
                    {
                        c1Transaction.SetData(i, 0, i.ToString());
                        c1Transaction.SetData(i, COL_DATEFROM, DateTime.Now.Date.ToShortDateString());
                        //c1Transaction.SetData(i, 0, i.ToString()); 
                        c1Transaction.SetData(i, COL_DATEFROM, DateTime.Now.Date.ToShortDateString());

                        GetDefaultTOSPOS();
                        c1Transaction.SetData(i, COL_TOSCODE, _DefaultTOSCode);
                        c1Transaction.SetData(i, COL_TOSDESC, _DefaultTOSDesc);
                        c1Transaction.SetData(i, COL_POSCODE, _DefaultPOSCode);
                        c1Transaction.SetData(i, COL_POSDESC, _DefaultPOSDesc);


                        c1Transaction.SetData(i, COL_ISEMG, false);

                        c1Transaction.SetData(i, COL_ISLABCPT, false);
                        c1Transaction.SetData(i, COL_AUTHORIZATIONNO, "");
                        c1Transaction.SetData(i, COL_SENTTOCLAIM, true);

                        if (_DefaultRenderingProviderID > 0)
                        {
                            c1Transaction.SetData(i, COL_PROVIDER_ID, _DefaultRenderingProviderID);
                            c1Transaction.SetData(i, COL_PROVIDER, _DefaultRenderringProviderName);
                        }

                        SetCurrencyCellValue(i);
                    }
                }
            }
            catch //(Exception ex)
            {
                throw;
            }
            finally
            {
                _isControlInDesignMode = false;
            }


        }

        public void ReinitilizeControlOnModifyPatient()
        {

            _isControlInDesignMode = true;

            try
            {

                for (int i = 0; i <= c1Transaction.Rows.Count - 1; i++)
                {
                    c1Transaction.Rows[i].Height = 23;
                    if (i > 0)
                    {
                        GetDefaultTOSPOS();
                        c1Transaction.SetData(i, COL_TOSCODE, _DefaultTOSCode);
                        c1Transaction.SetData(i, COL_TOSDESC, _DefaultTOSDesc);
                        //c1Transaction.SetData(i, COL_POSCODE, _DefaultPOSCode);
                        // c1Transaction.SetData(i, COL_POSDESC, _DefaultPOSDesc);
                        //c1Transaction.SetData(i, COL_ISEMG, false);
                        // c1Transaction.SetData(i, COL_ISLABCPT, false);
                        //c1Transaction.SetData(i, COL_AUTHORIZATIONNO, "");
                        c1Transaction.SetData(i, COL_SENTTOCLAIM, true);

                        if (_DefaultRenderingProviderID > 0)
                        {
                            c1Transaction.SetData(i, COL_PROVIDER_ID, _DefaultRenderingProviderID);
                            c1Transaction.SetData(i, COL_PROVIDER, _DefaultRenderringProviderName);
                        }


                    }
                }
            }
            catch //(Exception ex)
            {
                throw;
            }
            finally
            {
                _isControlInDesignMode = false;
            }


        }

        #region " Commented Code Existing Design Method "

        //private void DesignTransactionGrid()
        //{
        //    c1Transaction.Clear();
        //    c1Transaction.Rows.Count = ROW_COUNT;
        //    c1Transaction.Rows.Fixed = 1;
        //    c1Transaction.Cols.Count = COL_COUNT;
        //    c1Transaction.Cols.Fixed = 1;

        //    c1Transaction.AllowSorting = AllowSortingEnum.None;
        //    c1Transaction.Cols[COL_ALLOWED].AllowSorting = true;
        //    c1Transaction.Cols[COL_ALLOWED].Sort = SortFlags.Descending;


        //    #region "Data Type"
        //    c1Transaction.Cols[COL_NO].DataType = typeof(System.Int32);
        //    c1Transaction.Cols[COL_TRANSACTIONID].DataType = typeof(System.Int64);

        //    c1Transaction.Cols[COL_DATEFROM].DataType = typeof(System.DateTime);
        //    c1Transaction.Cols[COL_DATETO].DataType = typeof(System.DateTime);

        //    c1Transaction.Cols[COL_POSCODE].DataType = typeof(System.String);
        //    c1Transaction.Cols[COL_POSDESC].DataType = typeof(System.String);
        //    c1Transaction.Cols[COL_POS_BTN].DataType = typeof(System.String);

        //    c1Transaction.Cols[COL_TOSCODE].DataType = typeof(System.String);
        //    c1Transaction.Cols[COL_TOSDESC].DataType = typeof(System.String);
        //    c1Transaction.Cols[COL_TOS_BTN].DataType = typeof(System.String);

        //    c1Transaction.Cols[COL_CPT_CODE].DataType = typeof(System.String);
        //    c1Transaction.Cols[COL_CPT_DESC].DataType = typeof(System.String);
        //    c1Transaction.Cols[COL_CPT_BTN].DataType = typeof(System.String);

        //    c1Transaction.Cols[COL_DX1_CODE].DataType = typeof(System.String);
        //    c1Transaction.Cols[COL_DX1_DESC].DataType = typeof(System.String);
        //    c1Transaction.Cols[COL_DX1_BTN].DataType = typeof(System.String);
        //    c1Transaction.Cols[COL_DX2_CODE].DataType = typeof(System.String);
        //    c1Transaction.Cols[COL_DX2_DESC].DataType = typeof(System.String);
        //    c1Transaction.Cols[COL_DX2_BTN].DataType = typeof(System.String);
        //    c1Transaction.Cols[COL_DX3_CODE].DataType = typeof(System.String);
        //    c1Transaction.Cols[COL_DX3_DESC].DataType = typeof(System.String);
        //    c1Transaction.Cols[COL_DX3_BTN].DataType = typeof(System.String);
        //    c1Transaction.Cols[COL_DX4_CODE].DataType = typeof(System.String);
        //    c1Transaction.Cols[COL_DX4_DESC].DataType = typeof(System.String);
        //    c1Transaction.Cols[COL_DX4_BTN].DataType = typeof(System.String);
        //    c1Transaction.Cols[COL_DX5_CODE].DataType = typeof(System.String);
        //    c1Transaction.Cols[COL_DX5_DESC].DataType = typeof(System.String);
        //    c1Transaction.Cols[COL_DX5_BTN].DataType = typeof(System.String);
        //    c1Transaction.Cols[COL_DX6_CODE].DataType = typeof(System.String);
        //    c1Transaction.Cols[COL_DX6_DESC].DataType = typeof(System.String);
        //    c1Transaction.Cols[COL_DX6_BTN].DataType = typeof(System.String);
        //    c1Transaction.Cols[COL_DX7_CODE].DataType = typeof(System.String);
        //    c1Transaction.Cols[COL_DX7_DESC].DataType = typeof(System.String);
        //    c1Transaction.Cols[COL_DX7_BTN].DataType = typeof(System.String);
        //    c1Transaction.Cols[COL_DX8_CODE].DataType = typeof(System.String);
        //    c1Transaction.Cols[COL_DX8_DESC].DataType = typeof(System.String);
        //    c1Transaction.Cols[COL_DX8_BTN].DataType = typeof(System.String);

        //    c1Transaction.Cols[COL_DX1_PTR].DataType = typeof(System.Boolean);
        //    c1Transaction.Cols[COL_DX2_PTR].DataType = typeof(System.Boolean);
        //    c1Transaction.Cols[COL_DX3_PTR].DataType = typeof(System.Boolean);
        //    c1Transaction.Cols[COL_DX4_PTR].DataType = typeof(System.Boolean);
        //    c1Transaction.Cols[COL_DX5_PTR].DataType = typeof(System.Boolean);
        //    c1Transaction.Cols[COL_DX6_PTR].DataType = typeof(System.Boolean);
        //    c1Transaction.Cols[COL_DX7_PTR].DataType = typeof(System.Boolean);
        //    c1Transaction.Cols[COL_DX8_PTR].DataType = typeof(System.Boolean);

        //    c1Transaction.Cols[COL_MOD1_CODE].DataType = typeof(System.String);
        //    c1Transaction.Cols[COL_MOD1_DESC].DataType = typeof(System.String);
        //    c1Transaction.Cols[COL_MOD1_BTN].DataType = typeof(System.String);
        //    c1Transaction.Cols[COL_MOD2_CODE].DataType = typeof(System.String);
        //    c1Transaction.Cols[COL_MOD2_DESC].DataType = typeof(System.String);
        //    c1Transaction.Cols[COL_MOD2_BTN].DataType = typeof(System.String);
        //    c1Transaction.Cols[COL_MOD3_CODE].DataType = typeof(System.String);
        //    c1Transaction.Cols[COL_MOD3_DESC].DataType = typeof(System.String);
        //    c1Transaction.Cols[COL_MOD3_BTN].DataType = typeof(System.String);
        //    c1Transaction.Cols[COL_MOD4_CODE].DataType = typeof(System.String);
        //    c1Transaction.Cols[COL_MOD4_DESC].DataType = typeof(System.String);
        //    c1Transaction.Cols[COL_MOD4_BTN].DataType = typeof(System.String);


        //    c1Transaction.Cols[COL_CHARGES].DataType = typeof(System.Decimal);
        //    c1Transaction.Cols[COL_UNIT].DataType = typeof(System.Decimal);
        //    c1Transaction.Cols[COL_TOTAL].DataType = typeof(System.Decimal);
        //    c1Transaction.Cols[COL_ALLOWED].DataType = typeof(System.Decimal);
        //    c1Transaction.Cols[COL_PROVIDER_ID].DataType = typeof(System.Int64);
        //    c1Transaction.Cols[COL_PROVIDER].DataType = typeof(System.String);
        //    c1Transaction.Cols[COL_PROVIDER_BTN].DataType = typeof(System.String);
        //    c1Transaction.Cols[COL_NOTE_BTN].DataType = typeof(System.String);
        //    c1Transaction.Cols[COL_NOTE_DATA].DataType = typeof(System.Object);
        //    c1Transaction.Cols[COL_NOTE_TYPE].DataType = typeof(System.Int32);

        //    c1Transaction.Cols[COL_INSURANCEID].DataType = typeof(System.Int64);
        //    c1Transaction.Cols[COL_INSURANCENAME].DataType = typeof(System.String);

        //    c1Transaction.Cols[COL_INSSELF_PAYMODE].DataType = typeof(System.Int32);

        //    #endregion

        //    #region "Button Design"
        //    c1Transaction.Cols[COL_POS_BTN].ComboList = "...";
        //    c1Transaction.Cols[COL_TOS_BTN].ComboList = "...";
        //    c1Transaction.Cols[COL_CPT_BTN].ComboList = "...";
        //    c1Transaction.Cols[COL_DX1_BTN].ComboList = "...";
        //    c1Transaction.Cols[COL_DX2_BTN].ComboList = "...";
        //    c1Transaction.Cols[COL_DX3_BTN].ComboList = "...";
        //    c1Transaction.Cols[COL_DX4_BTN].ComboList = "...";
        //    c1Transaction.Cols[COL_MOD1_BTN].ComboList = "...";
        //    c1Transaction.Cols[COL_MOD2_BTN].ComboList = "...";
        //    c1Transaction.Cols[COL_PROVIDER_BTN].ComboList = "...";
        //    c1Transaction.Cols[COL_NOTE_BTN].ComboList = "...";
        //    #endregion

        //    #region "Button Design"
        //    c1Transaction.Cols[COL_POS_BTN].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
        //    c1Transaction.Cols[COL_TOS_BTN].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
        //    c1Transaction.Cols[COL_CPT_BTN].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;

        //    c1Transaction.Cols[COL_DX1_BTN].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
        //    c1Transaction.Cols[COL_DX2_BTN].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
        //    c1Transaction.Cols[COL_DX3_BTN].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
        //    c1Transaction.Cols[COL_DX4_BTN].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
        //    c1Transaction.Cols[COL_DX5_BTN].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
        //    c1Transaction.Cols[COL_DX6_BTN].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
        //    c1Transaction.Cols[COL_DX7_BTN].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
        //    c1Transaction.Cols[COL_DX8_BTN].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;

        //    c1Transaction.Cols[COL_MOD1_BTN].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
        //    c1Transaction.Cols[COL_MOD2_BTN].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
        //    c1Transaction.Cols[COL_MOD3_BTN].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
        //    c1Transaction.Cols[COL_MOD4_BTN].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;

        //    c1Transaction.Cols[COL_PROVIDER_BTN].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
        //    c1Transaction.Cols[COL_NOTE_BTN].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
        //    #endregion

        //    #region "Width"

        //    int nWidth = pnlMain.Width;

        //    //c1Transaction.Cols[COL_NO].Width = 20;
        //    c1Transaction.Cols[COL_NO].Width = 29;//Convert.ToInt32(nWidth * 0.02);
        //    c1Transaction.Cols[COL_TRANSACTIONID].Width = 0;

        //    c1Transaction.Cols[COL_DATEFROM].Width = 84;//Convert.ToInt32(nWidth * 0.08);
        //    c1Transaction.Cols[COL_DATETO].Width = 0;

        //    c1Transaction.Cols[COL_POSCODE].Width = 51;//Convert.ToInt32(nWidth * 0.05);
        //    c1Transaction.Cols[COL_POSDESC].Width = 0;
        //    c1Transaction.Cols[COL_POS_BTN].Width = 0;

        //    c1Transaction.Cols[COL_TOSCODE].Width = 51;//Convert.ToInt32(nWidth * 0.05);
        //    c1Transaction.Cols[COL_TOSDESC].Width = 0;
        //    c1Transaction.Cols[COL_TOS_BTN].Width = 0;

        //    c1Transaction.Cols[COL_CPT_CODE].Width = 93;//Convert.ToInt32(nWidth * 0.05);
        //    c1Transaction.Cols[COL_CPT_DESC].Width = 0;
        //    c1Transaction.Cols[COL_CPT_BTN].Width = 0;

        //    c1Transaction.Cols[COL_DX1_CODE].Width = 84;//Convert.ToInt32(nWidth * 0.06);
        //    c1Transaction.Cols[COL_DX1_DESC].Width = 0;
        //    c1Transaction.Cols[COL_DX1_BTN].Width = 0;
        //    c1Transaction.Cols[COL_DX2_CODE].Width = 84;//Convert.ToInt32(nWidth * 0.06);
        //    c1Transaction.Cols[COL_DX2_DESC].Width = 0;
        //    c1Transaction.Cols[COL_DX2_BTN].Width = 0;
        //    c1Transaction.Cols[COL_DX3_CODE].Width = 84;//Convert.ToInt32(nWidth * 0.06);
        //    c1Transaction.Cols[COL_DX3_DESC].Width = 0;
        //    c1Transaction.Cols[COL_DX3_BTN].Width = 0;
        //    c1Transaction.Cols[COL_DX4_CODE].Width = 84;//Convert.ToInt32(nWidth * 0.06);
        //    c1Transaction.Cols[COL_DX4_DESC].Width = 0;
        //    c1Transaction.Cols[COL_DX4_BTN].Width = 0;
        //    c1Transaction.Cols[COL_DX5_CODE].Width = 0;
        //    c1Transaction.Cols[COL_DX5_DESC].Width = 0;
        //    c1Transaction.Cols[COL_DX5_BTN].Width = 0;
        //    c1Transaction.Cols[COL_DX6_CODE].Width = 0;
        //    c1Transaction.Cols[COL_DX6_DESC].Width = 0;
        //    c1Transaction.Cols[COL_DX6_BTN].Width = 0;
        //    c1Transaction.Cols[COL_DX7_CODE].Width = 0;
        //    c1Transaction.Cols[COL_DX7_DESC].Width = 0;
        //    c1Transaction.Cols[COL_DX7_BTN].Width = 0;
        //    c1Transaction.Cols[COL_DX8_CODE].Width = 0;
        //    c1Transaction.Cols[COL_DX8_DESC].Width = 0;
        //    c1Transaction.Cols[COL_DX8_BTN].Width = 0;

        //    c1Transaction.Cols[COL_DX1_PTR].Width = 21;//Convert.ToInt32(nWidth * 0.03);
        //    c1Transaction.Cols[COL_DX2_PTR].Width = 21;//Convert.ToInt32(nWidth * 0.03);
        //    c1Transaction.Cols[COL_DX3_PTR].Width = 21;//Convert.ToInt32(nWidth * 0.03);
        //    c1Transaction.Cols[COL_DX4_PTR].Width = 21;//Convert.ToInt32(nWidth * 0.03);
        //    c1Transaction.Cols[COL_DX5_PTR].Width = 0;
        //    c1Transaction.Cols[COL_DX6_PTR].Width = 0;
        //    c1Transaction.Cols[COL_DX7_PTR].Width = 0;
        //    c1Transaction.Cols[COL_DX8_PTR].Width = 0;

        //    c1Transaction.Cols[COL_MOD1_CODE].Width = 60;//Convert.ToInt32(nWidth * 0.05);
        //    c1Transaction.Cols[COL_MOD1_DESC].Width = 0;
        //    c1Transaction.Cols[COL_MOD1_BTN].Width = 0;
        //    c1Transaction.Cols[COL_MOD2_CODE].Width = 60;//Convert.ToInt32(nWidth * 0.05);
        //    c1Transaction.Cols[COL_MOD2_DESC].Width = 0;
        //    c1Transaction.Cols[COL_MOD2_BTN].Width = 0;
        //    c1Transaction.Cols[COL_MOD3_CODE].Width = 0;
        //    c1Transaction.Cols[COL_MOD3_DESC].Width = 0;
        //    c1Transaction.Cols[COL_MOD3_BTN].Width = 0;
        //    c1Transaction.Cols[COL_MOD4_CODE].Width = 0;
        //    c1Transaction.Cols[COL_MOD4_DESC].Width = 0;
        //    c1Transaction.Cols[COL_MOD4_BTN].Width = 0;

        //    c1Transaction.Cols[COL_CHARGES].Width = 61;//Convert.ToInt32(nWidth * 0.06);
        //    c1Transaction.Cols[COL_UNIT].Width = 34;//Convert.ToInt32(nWidth * 0.03);
        //    c1Transaction.Cols[COL_TOTAL].Width = 77;// Convert.ToInt32(nWidth * 0.08);
        //    c1Transaction.Cols[COL_ALLOWED].Width = 65;// Convert.ToInt32(nWidth * 0.07);
        //    c1Transaction.Cols[COL_PROVIDER_ID].Width = 0;
        //    c1Transaction.Cols[COL_PROVIDER].Width = 180;//Convert.ToInt32(nWidth * 0.20);
        //    c1Transaction.Cols[COL_PROVIDER_BTN].Width = 0;
        //    c1Transaction.Cols[COL_NOTE_BTN].Width = 0;
        //    c1Transaction.Cols[COL_NOTE_DATA].Width = 0;
        //    c1Transaction.Cols[COL_NOTE_TYPE].Width = 0;

        //    c1Transaction.Cols[COL_INSURANCEID].Width = 0;
        //    c1Transaction.Cols[COL_INSURANCENAME].Width = 150;

        //    c1Transaction.Cols[COL_INSSELF_PAYMODE].Width = 0;

        //    #endregion

        //    #region "Show/Hide"
        //    c1Transaction.Cols[COL_NO].Visible = true;
        //    c1Transaction.Cols[COL_TRANSACTIONID].Visible = false;

        //    c1Transaction.Cols[COL_DATEFROM].Visible = true;
        //    c1Transaction.Cols[COL_DATETO].Visible = false;

        //    c1Transaction.Cols[COL_POSCODE].Visible = true;
        //    c1Transaction.Cols[COL_POSDESC].Visible = false;
        //    //c1Transaction.Cols[COL_POS_BTN].Visible = true;
        //    c1Transaction.Cols[COL_POS_BTN].Visible = false;

        //    c1Transaction.Cols[COL_TOSCODE].Visible = true;
        //    c1Transaction.Cols[COL_TOSDESC].Visible = false;
        //    //c1Transaction.Cols[COL_TOS_BTN].Visible = true;
        //    c1Transaction.Cols[COL_TOS_BTN].Visible = false;

        //    c1Transaction.Cols[COL_CPT_CODE].Visible = true;
        //    c1Transaction.Cols[COL_CPT_DESC].Visible = false;
        //    //c1Transaction.Cols[COL_CPT_BTN].Visible = true;
        //    c1Transaction.Cols[COL_CPT_BTN].Visible = false;

        //    c1Transaction.Cols[COL_DX1_CODE].Visible = true;
        //    c1Transaction.Cols[COL_DX1_DESC].Visible = false;
        //    //c1Transaction.Cols[COL_DX1_BTN].Visible = true;
        //    c1Transaction.Cols[COL_DX1_BTN].Visible = false;
        //    c1Transaction.Cols[COL_DX2_CODE].Visible = true;
        //    c1Transaction.Cols[COL_DX2_DESC].Visible = false;
        //    //c1Transaction.Cols[COL_DX2_BTN].Visible = true;
        //    c1Transaction.Cols[COL_DX2_BTN].Visible = false;
        //    c1Transaction.Cols[COL_DX3_CODE].Visible = true;
        //    c1Transaction.Cols[COL_DX3_DESC].Visible = false;
        //    //c1Transaction.Cols[COL_DX3_BTN].Visible = true;
        //    c1Transaction.Cols[COL_DX3_BTN].Visible = false;
        //    c1Transaction.Cols[COL_DX4_CODE].Visible = true;
        //    c1Transaction.Cols[COL_DX4_DESC].Visible = false;
        //    //c1Transaction.Cols[COL_DX4_BTN].Visible = true;
        //    c1Transaction.Cols[COL_DX4_BTN].Visible = false;
        //    c1Transaction.Cols[COL_DX5_CODE].Visible = false; //
        //    c1Transaction.Cols[COL_DX5_DESC].Visible = false;
        //    c1Transaction.Cols[COL_DX5_BTN].Visible = false;
        //    c1Transaction.Cols[COL_DX6_CODE].Visible = false; //
        //    c1Transaction.Cols[COL_DX6_DESC].Visible = false;
        //    c1Transaction.Cols[COL_DX6_BTN].Visible = false;
        //    c1Transaction.Cols[COL_DX7_CODE].Visible = false; //
        //    c1Transaction.Cols[COL_DX7_DESC].Visible = false;
        //    c1Transaction.Cols[COL_DX7_BTN].Visible = false;
        //    c1Transaction.Cols[COL_DX8_CODE].Visible = false; //
        //    c1Transaction.Cols[COL_DX8_DESC].Visible = false;
        //    c1Transaction.Cols[COL_DX8_BTN].Visible = false;

        //    c1Transaction.Cols[COL_DX1_PTR].Visible = true;
        //    c1Transaction.Cols[COL_DX2_PTR].Visible = true;
        //    c1Transaction.Cols[COL_DX3_PTR].Visible = true;
        //    c1Transaction.Cols[COL_DX4_PTR].Visible = true;
        //    c1Transaction.Cols[COL_DX5_PTR].Visible = false;
        //    c1Transaction.Cols[COL_DX6_PTR].Visible = false;
        //    c1Transaction.Cols[COL_DX7_PTR].Visible = false;
        //    c1Transaction.Cols[COL_DX8_PTR].Visible = false;


        //    c1Transaction.Cols[COL_MOD1_CODE].Visible = true;
        //    c1Transaction.Cols[COL_MOD1_DESC].Visible = false;
        //    //c1Transaction.Cols[COL_MOD1_BTN].Visible = true;
        //    c1Transaction.Cols[COL_MOD1_BTN].Visible = false;
        //    c1Transaction.Cols[COL_MOD2_CODE].Visible = true;
        //    c1Transaction.Cols[COL_MOD2_DESC].Visible = false;
        //    //c1Transaction.Cols[COL_MOD2_BTN].Visible = true;
        //    c1Transaction.Cols[COL_MOD2_BTN].Visible = false;
        //    c1Transaction.Cols[COL_MOD3_CODE].Visible = false;
        //    c1Transaction.Cols[COL_MOD3_DESC].Visible = false;
        //    c1Transaction.Cols[COL_MOD3_BTN].Visible = false;
        //    c1Transaction.Cols[COL_MOD4_CODE].Visible = false;
        //    c1Transaction.Cols[COL_MOD4_DESC].Visible = false;
        //    c1Transaction.Cols[COL_MOD4_BTN].Visible = false;

        //    c1Transaction.Cols[COL_CHARGES].Visible = true;
        //    c1Transaction.Cols[COL_UNIT].Visible = true;
        //    c1Transaction.Cols[COL_TOTAL].Visible = true;
        //    c1Transaction.Cols[COL_ALLOWED].Visible = true;
        //    c1Transaction.Cols[COL_PROVIDER_ID].Visible = false;
        //    c1Transaction.Cols[COL_PROVIDER].Visible = true;
        //    //c1Transaction.Cols[COL_PROVIDER_BTN].Visible = true;
        //    c1Transaction.Cols[COL_PROVIDER_BTN].Visible = false;
        //    c1Transaction.Cols[COL_NOTE_BTN].Visible = false;
        //    c1Transaction.Cols[COL_NOTE_DATA].Visible = false;
        //    c1Transaction.Cols[COL_NOTE_TYPE].Visible = false;

        //    c1Transaction.Cols[COL_INSURANCEID].Visible = false;
        //    c1Transaction.Cols[COL_INSURANCENAME].Visible = true;

        //    c1Transaction.Cols[COL_INSSELF_PAYMODE].Visible = false;

        //    #endregion

        //    #region "Header"
        //    c1Transaction.SetData(0, COL_NO, "No.");
        //    c1Transaction.SetData(0, COL_TRANSACTIONID, "TransactionId");

        //    c1Transaction.SetData(0, COL_DATEFROM, "Date");
        //    c1Transaction.SetData(0, COL_DATETO, "Date To");

        //    c1Transaction.SetData(0, COL_POSCODE, "POS");
        //    c1Transaction.SetData(0, COL_POSDESC, "POS Description");
        //    c1Transaction.SetData(0, COL_POS_BTN, "");

        //    c1Transaction.SetData(0, COL_TOSCODE, "TOS");
        //    c1Transaction.SetData(0, COL_TOSDESC, "TOS Description");
        //    c1Transaction.SetData(0, COL_TOS_BTN, "");

        //    c1Transaction.SetData(0, COL_CPT_CODE, "CPT");
        //    c1Transaction.SetData(0, COL_CPT_DESC, "CPT Desc");
        //    c1Transaction.SetData(0, COL_CPT_BTN, "");

        //    c1Transaction.SetData(0, COL_DX1_CODE, "Dx1");
        //    c1Transaction.SetData(0, COL_DX1_DESC, "Dx1 Desc");
        //    c1Transaction.SetData(0, COL_DX1_BTN, "");
        //    c1Transaction.SetData(0, COL_DX2_CODE, "Dx2");
        //    c1Transaction.SetData(0, COL_DX2_DESC, "Dx2 Desc");
        //    c1Transaction.SetData(0, COL_DX2_BTN, "");
        //    c1Transaction.SetData(0, COL_DX3_CODE, "Dx3");
        //    c1Transaction.SetData(0, COL_DX3_DESC, "Dx3 Desc");
        //    c1Transaction.SetData(0, COL_DX3_BTN, "");
        //    c1Transaction.SetData(0, COL_DX4_CODE, "Dx4");
        //    c1Transaction.SetData(0, COL_DX4_DESC, "Dx4 Desc");
        //    c1Transaction.SetData(0, COL_DX4_BTN, "");
        //    c1Transaction.SetData(0, COL_DX5_CODE, "Dx5");
        //    c1Transaction.SetData(0, COL_DX5_DESC, "Dx5 Desc");
        //    c1Transaction.SetData(0, COL_DX5_BTN, "");
        //    c1Transaction.SetData(0, COL_DX6_CODE, "Dx6");
        //    c1Transaction.SetData(0, COL_DX6_DESC, "Dx6 Desc");
        //    c1Transaction.SetData(0, COL_DX6_BTN, "");
        //    c1Transaction.SetData(0, COL_DX7_CODE, "Dx7");
        //    c1Transaction.SetData(0, COL_DX7_DESC, "Dx7 Desc");
        //    c1Transaction.SetData(0, COL_DX7_BTN, "");
        //    c1Transaction.SetData(0, COL_DX8_CODE, "Dx8");
        //    c1Transaction.SetData(0, COL_DX8_DESC, "Dx8 Desc");
        //    c1Transaction.SetData(0, COL_DX8_BTN, "");


        //    c1Transaction.SetData(0, COL_DX1_PTR, "D1");
        //    c1Transaction.SetData(0, COL_DX2_PTR, "D2");
        //    c1Transaction.SetData(0, COL_DX3_PTR, "D3");
        //    c1Transaction.SetData(0, COL_DX4_PTR, "D4");
        //    c1Transaction.SetData(0, COL_DX5_PTR, "D5");
        //    c1Transaction.SetData(0, COL_DX6_PTR, "D6");
        //    c1Transaction.SetData(0, COL_DX7_PTR, "D7");
        //    c1Transaction.SetData(0, COL_DX8_PTR, "D8");


        //    c1Transaction.SetData(0, COL_MOD1_CODE, "M1");
        //    c1Transaction.SetData(0, COL_MOD1_DESC, "M1 Desc");
        //    c1Transaction.SetData(0, COL_MOD1_BTN, "");
        //    c1Transaction.SetData(0, COL_MOD2_CODE, "M2");
        //    c1Transaction.SetData(0, COL_MOD2_DESC, "M2 Desc");
        //    c1Transaction.SetData(0, COL_MOD2_BTN, "");
        //    c1Transaction.SetData(0, COL_MOD3_CODE, "M3");
        //    c1Transaction.SetData(0, COL_MOD3_DESC, "M3 Desc");
        //    c1Transaction.SetData(0, COL_MOD3_BTN, "");
        //    c1Transaction.SetData(0, COL_MOD4_CODE, "M4");
        //    c1Transaction.SetData(0, COL_MOD4_DESC, "M4 Desc");
        //    c1Transaction.SetData(0, COL_MOD4_BTN, "");



        //    c1Transaction.SetData(0, COL_CHARGES, "Charges");
        //    c1Transaction.SetData(0, COL_UNIT, "Unit");
        //    c1Transaction.SetData(0, COL_TOTAL, "Total");
        //    c1Transaction.SetData(0, COL_ALLOWED, "Allowed");
        //    c1Transaction.SetData(0, COL_PROVIDER_ID, "ProviderID");
        //    c1Transaction.SetData(0, COL_PROVIDER, "Provider");
        //    c1Transaction.SetData(0, COL_PROVIDER_BTN, "");
        //    c1Transaction.SetData(0, COL_NOTE_BTN, "Note");
        //    c1Transaction.SetData(0, COL_NOTE_DATA, "Note");
        //    c1Transaction.SetData(0, COL_NOTE_TYPE, "Note Type");

        //    c1Transaction.SetData(0, COL_INSURANCEID, "InsuranceID");
        //    c1Transaction.SetData(0, COL_INSURANCENAME, "Insurance");

        //    c1Transaction.SetData(0, COL_INSSELF_PAYMODE, "PayMode");

        //    #endregion

        //    #region "Styles"

        //    CellStyle csStyle = c1Transaction.Styles.Add("rsTransactionLine");
        //    c1Transaction.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        //    c1Transaction.BackColor = Color.FromArgb(222, 231, 250);
        //    c1Transaction.ForeColor = Color.FromArgb(21, 66, 139);
        //    c1Transaction.Height = 300;

        //    CellStyle csCurrency = c1Transaction.Styles.Add("csCurrencyCell");
        //    csCurrency.DataType = typeof(System.Decimal);
        //    csCurrency.Format = "c";
        //    csCurrency.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

        //    #endregion "Styles"

        //    c1Transaction.Cols[COL_TOTAL].AllowEditing = false;
        //    c1Transaction.Cols[COL_INSURANCENAME].AllowEditing = false;  

        //    c1Transaction.Cols[COL_CHARGES].Style = csCurrency;
        //    c1Transaction.Cols[COL_TOTAL].Style = csCurrency;
        //    c1Transaction.Cols[COL_ALLOWED].Style = csCurrency;

        //    for (int i = 0; i <= c1Transaction.Rows.Count - 1; i++)
        //    {
        //        c1Transaction.Rows[i].Height = 23;
        //        if (i > 0)
        //        {
        //            c1Transaction.SetData(i, 0, i.ToString());
        //            c1Transaction.SetData(i, COL_DATEFROM, DateTime.Now.Date.ToShortDateString());
        //            //c1Transaction.SetData(i, 0, i.ToString()); 
        //            c1Transaction.SetData(i, COL_DATEFROM, DateTime.Now.Date.ToShortDateString());

        //            GetDefaultTOSPOS();
        //            c1Transaction.SetData(i, COL_TOSCODE, _DefaultTOSCode);
        //            c1Transaction.SetData(i, COL_TOSDESC, _DefaultTOSDesc);
        //            c1Transaction.SetData(i, COL_POSCODE, _DefaultPOSCode);
        //            c1Transaction.SetData(i, COL_POSDESC, _DefaultPOSDesc);


        //            //**************************************************************************
        //            //Code Changes made on 20081017 By - Sagar Ghodke
        //            //Below commented code is previous code

        //            ////Set the Default Renderring Provider 
        //            //if (_DefaultRenderingProviderID > 0)
        //            //{
        //            //    c1Transaction.SetData(i, COL_PROVIDER_ID, _DefaultRenderingProviderID);
        //            //    c1Transaction.SetData(i, COL_PROVIDER, _DefaultRenderringProviderName);
        //            //}

        //            //Set the Patient Provider as the Default Renderring Provider 
        //            if (_DefaultRenderingProviderID > 0)
        //            {
        //                c1Transaction.SetData(i, COL_PROVIDER_ID, _DefaultRenderingProviderID);
        //                c1Transaction.SetData(i, COL_PROVIDER, _DefaultRenderringProviderName);
        //            }
        //            else
        //            { 

        //            }


        //            //End Code Chages - 20081017 
        //            //**************************************************************************
        //            SetCurrencyCellValue(i);


        //        }
        //    }


        //}

        #endregion " Commented Code Existing Design Method "

        private void DesignTransactionGrid()
        {
            try
            {
                this.c1Transaction.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Transaction_CellChanged);

                //c1Transaction.ScrollBars = ScrollBars.None;
                _isControlInDesignMode = true;
                c1Transaction.ExtendLastCol = true;
                c1Transaction.Clear();
                c1Transaction.Rows.Count = ROW_COUNT;
                c1Transaction.Rows.Fixed = 1;
                c1Transaction.Cols.Count = COL_COUNT;
                c1Transaction.Cols.Fixed = 1;

                c1Transaction.AllowSorting = AllowSortingEnum.None;

                //c1Transaction.Cols[COL_ALLOWED].AllowSorting = true;
                //c1Transaction.Cols[COL_ALLOWED].Sort = SortFlags.Descending;

                //Added By Pramod Nair to sort the Total amount column instead of Allowed amount
                c1Transaction.Cols[COL_TOTAL].AllowSorting = true;
                c1Transaction.Cols[COL_TOTAL].Sort = SortFlags.Descending;

                c1Transaction.AllowResizing = AllowResizingEnum.None;
                c1Transaction.AllowDragging = AllowDraggingEnum.None;
                c1Transaction.Cols[COL_NO].Selected = false;

                #region "Data Type"
                c1Transaction.Cols[COL_NO].DataType = typeof(System.Int32);
                c1Transaction.Cols[COL_TRANSACTIONID].DataType = typeof(System.Int64);

                c1Transaction.Cols[COL_DATEFROM].DataType = typeof(System.DateTime);
                c1Transaction.Cols[COL_DATETO].DataType = typeof(System.DateTime);

                c1Transaction.Cols[COL_POSCODE].DataType = typeof(System.String);
                c1Transaction.Cols[COL_POSDESC].DataType = typeof(System.String);
                c1Transaction.Cols[COL_POS_BTN].DataType = typeof(System.String);

                c1Transaction.Cols[COL_TOSCODE].DataType = typeof(System.String);
                c1Transaction.Cols[COL_TOSDESC].DataType = typeof(System.String);
                c1Transaction.Cols[COL_TOS_BTN].DataType = typeof(System.String);


                c1Transaction.Cols[COL_ISEMG].DataType = typeof(System.Boolean);


                c1Transaction.Cols[COL_CPT_CODE].DataType = typeof(System.String);
                c1Transaction.Cols[COL_CPT_DESC].DataType = typeof(System.String);
                c1Transaction.Cols[COL_CPT_BTN].DataType = typeof(System.String);

                c1Transaction.Cols[COL_DX1_CODE].DataType = typeof(System.String);
                c1Transaction.Cols[COL_DX1_DESC].DataType = typeof(System.String);
                c1Transaction.Cols[COL_DX1_BTN].DataType = typeof(System.String);
                c1Transaction.Cols[COL_DX2_CODE].DataType = typeof(System.String);
                c1Transaction.Cols[COL_DX2_DESC].DataType = typeof(System.String);
                c1Transaction.Cols[COL_DX2_BTN].DataType = typeof(System.String);
                c1Transaction.Cols[COL_DX3_CODE].DataType = typeof(System.String);
                c1Transaction.Cols[COL_DX3_DESC].DataType = typeof(System.String);
                c1Transaction.Cols[COL_DX3_BTN].DataType = typeof(System.String);
                c1Transaction.Cols[COL_DX4_CODE].DataType = typeof(System.String);
                c1Transaction.Cols[COL_DX4_DESC].DataType = typeof(System.String);
                c1Transaction.Cols[COL_DX4_BTN].DataType = typeof(System.String);
                c1Transaction.Cols[COL_DX5_CODE].DataType = typeof(System.String);
                c1Transaction.Cols[COL_DX5_DESC].DataType = typeof(System.String);
                c1Transaction.Cols[COL_DX5_BTN].DataType = typeof(System.String);
                c1Transaction.Cols[COL_DX6_CODE].DataType = typeof(System.String);
                c1Transaction.Cols[COL_DX6_DESC].DataType = typeof(System.String);
                c1Transaction.Cols[COL_DX6_BTN].DataType = typeof(System.String);
                c1Transaction.Cols[COL_DX7_CODE].DataType = typeof(System.String);
                c1Transaction.Cols[COL_DX7_DESC].DataType = typeof(System.String);
                c1Transaction.Cols[COL_DX7_BTN].DataType = typeof(System.String);
                c1Transaction.Cols[COL_DX8_CODE].DataType = typeof(System.String);
                c1Transaction.Cols[COL_DX8_DESC].DataType = typeof(System.String);
                c1Transaction.Cols[COL_DX8_BTN].DataType = typeof(System.String);

                c1Transaction.Cols[COL_DX1_PTR].DataType = typeof(System.Boolean);
                c1Transaction.Cols[COL_DX2_PTR].DataType = typeof(System.Boolean);
                c1Transaction.Cols[COL_DX3_PTR].DataType = typeof(System.Boolean);
                c1Transaction.Cols[COL_DX4_PTR].DataType = typeof(System.Boolean);
                c1Transaction.Cols[COL_DX5_PTR].DataType = typeof(System.Boolean);
                c1Transaction.Cols[COL_DX6_PTR].DataType = typeof(System.Boolean);
                c1Transaction.Cols[COL_DX7_PTR].DataType = typeof(System.Boolean);
                c1Transaction.Cols[COL_DX8_PTR].DataType = typeof(System.Boolean);

                c1Transaction.Cols[COL_MOD1_CODE].DataType = typeof(System.String);
                c1Transaction.Cols[COL_MOD1_DESC].DataType = typeof(System.String);
                c1Transaction.Cols[COL_MOD1_BTN].DataType = typeof(System.String);
                c1Transaction.Cols[COL_MOD2_CODE].DataType = typeof(System.String);
                c1Transaction.Cols[COL_MOD2_DESC].DataType = typeof(System.String);
                c1Transaction.Cols[COL_MOD2_BTN].DataType = typeof(System.String);
                c1Transaction.Cols[COL_MOD3_CODE].DataType = typeof(System.String);
                c1Transaction.Cols[COL_MOD3_DESC].DataType = typeof(System.String);
                c1Transaction.Cols[COL_MOD3_BTN].DataType = typeof(System.String);
                c1Transaction.Cols[COL_MOD4_CODE].DataType = typeof(System.String);
                c1Transaction.Cols[COL_MOD4_DESC].DataType = typeof(System.String);
                c1Transaction.Cols[COL_MOD4_BTN].DataType = typeof(System.String);


                c1Transaction.Cols[COL_CHARGES].DataType = typeof(System.Decimal);
                c1Transaction.Cols[COL_BILLEDAMOUNT].DataType = typeof(System.Decimal);
                c1Transaction.Cols[COL_UNIT].DataType = typeof(System.Decimal);
                c1Transaction.Cols[COL_TOTAL].DataType = typeof(System.Decimal);
                c1Transaction.Cols[COL_ALLOWED].DataType = typeof(System.Decimal);
                c1Transaction.Cols[COL_PROVIDER_ID].DataType = typeof(System.Int64);
                c1Transaction.Cols[COL_PROVIDER].DataType = typeof(System.String);
                c1Transaction.Cols[COL_PROVIDER_BTN].DataType = typeof(System.String);
                c1Transaction.Cols[COL_NOTE_BTN].DataType = typeof(System.String);
                c1Transaction.Cols[COL_NOTE_DATA].DataType = typeof(System.Object);
                c1Transaction.Cols[COL_NOTE_TYPE].DataType = typeof(System.Int32);

                c1Transaction.Cols[COL_INSURANCEID].DataType = typeof(System.Int64);
                c1Transaction.Cols[COL_INSURANCENAME].DataType = typeof(System.String);

                c1Transaction.Cols[COL_INSSELF_PAYMODE].DataType = typeof(System.Int32);

                c1Transaction.Cols[COL_HOLD].DataType = typeof(System.Boolean);
                c1Transaction.Cols[COL_HOLD_REASON].DataType = typeof(System.String);

                c1Transaction.Cols[COL_TRANSACTION_DETAIL_ID].DataType = typeof(System.Int64);

                c1Transaction.Cols[COL_ISLABCPT].DataType = typeof(System.Boolean);
                c1Transaction.Cols[COL_AUTHORIZATIONNO].DataType = typeof(System.String);
                c1Transaction.Cols[COL_SENTTOCLAIM].DataType = typeof(System.Boolean);

                c1Transaction.Cols[COL_LINEPRIMARY_DXCODE].DataType = typeof(System.String);
                c1Transaction.Cols[COL_LINEPRIMARY_DXDESC].DataType = typeof(System.String);
                c1Transaction.Cols[COL_ACTUAL_ALLOWED].DataType = typeof(System.Decimal);

                c1Transaction.Cols[COL_MST_TRANSACTIONID].DataType = typeof(System.Int64);
                c1Transaction.Cols[COL_MST_TRANSACTIONDTLID].DataType = typeof(System.Int64);


                c1Transaction.Cols[COL_NDCCODE].DataType = typeof(System.String);
                c1Transaction.Cols[COL_NDCUNIT].DataType = typeof(System.String);
                c1Transaction.Cols[COL_NDCUNITCODE].DataType = typeof(System.String);
                c1Transaction.Cols[COL_NDCUNITDESCRITION].DataType = typeof(System.String);
                c1Transaction.Cols[COL_PRESCRIPTION].DataType = typeof(System.String);
                c1Transaction.Cols[COL_PRESCRIPTIONDESC].DataType = typeof(System.String);
                //c1Transaction.Cols[COL_NDCDESCRIPTION].DataType = typeof(System.String);
                //c1Transaction.Cols[COL_NDCUNITCODE].DataType = typeof(System.String);
                //c1Transaction.Cols[COL_NDCUNITDESCRIPTION].DataType = typeof(System.String);

                //c1Transaction.Cols[COL_NDCUNIT].DataType = typeof(System.String);
                //c1Transaction.Cols[COL_NDCUNITPRICING].DataType = typeof(System.String);
                //c1Transaction.Cols[COL_DISPLAYNDCCODE_HCFA].DataType = typeof(System.String);

                c1Transaction.Cols[COL_SERVICESCREENING].DataType = typeof(System.Boolean);
                c1Transaction.Cols[COL_SERVICERESULTSCREENING].DataType = typeof(System.Boolean);
                c1Transaction.Cols[COL_FAMILYPLANNINGINDICATOR].DataType = typeof(System.Boolean);


                c1Transaction.Cols[COL_ANES_ID].DataType = typeof(System.Int64);
                c1Transaction.Cols[COL_ANES_STARTDATE].DataType = typeof(System.DateTime);
                c1Transaction.Cols[COL_ANES_ENDDATE].DataType = typeof(System.DateTime);
                c1Transaction.Cols[COL_ANES_TOTALMIN].DataType = typeof(System.Int64);
                c1Transaction.Cols[COL_ANES_MINPERUNIT].DataType = typeof(System.Int64);
                c1Transaction.Cols[COL_ANES_TIMEUNITS].DataType = typeof(System.Decimal);
                c1Transaction.Cols[COL_ANES_BASEUNITS].DataType = typeof(System.Decimal);
                c1Transaction.Cols[COL_ANES_OTHERUNITS].DataType = typeof(System.Decimal);
                c1Transaction.Cols[COL_ANES_TOTALUNITS].DataType = typeof(System.Decimal);
                c1Transaction.Cols[COL_ANES_ISANESTHESIA].DataType = typeof(System.Boolean);
                c1Transaction.Cols[COL_ANES_ISAUTOCALCULATE].DataType = typeof(System.Boolean);
                // line added by sameer for split claim option on New Charges Entry  11-11-2013
                c1Transaction.Cols[COL_SELFCLAIM].DataType = typeof(System.Boolean);
                #endregion

                #region "Button Design"
                c1Transaction.Cols[COL_POS_BTN].ComboList = "...";
                c1Transaction.Cols[COL_TOS_BTN].ComboList = "...";
                c1Transaction.Cols[COL_CPT_BTN].ComboList = "...";
                c1Transaction.Cols[COL_DX1_BTN].ComboList = "...";
                c1Transaction.Cols[COL_DX2_BTN].ComboList = "...";
                c1Transaction.Cols[COL_DX3_BTN].ComboList = "...";
                c1Transaction.Cols[COL_DX4_BTN].ComboList = "...";
                c1Transaction.Cols[COL_MOD1_BTN].ComboList = "...";
                c1Transaction.Cols[COL_MOD2_BTN].ComboList = "...";
                c1Transaction.Cols[COL_PROVIDER_BTN].ComboList = "...";
                c1Transaction.Cols[COL_NOTE_BTN].ComboList = "...";
                #endregion

                #region "Button Design"
                c1Transaction.Cols[COL_POS_BTN].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                c1Transaction.Cols[COL_TOS_BTN].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                c1Transaction.Cols[COL_CPT_BTN].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;

                c1Transaction.Cols[COL_DX1_BTN].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                c1Transaction.Cols[COL_DX2_BTN].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                c1Transaction.Cols[COL_DX3_BTN].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                c1Transaction.Cols[COL_DX4_BTN].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                c1Transaction.Cols[COL_DX5_BTN].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                c1Transaction.Cols[COL_DX6_BTN].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                c1Transaction.Cols[COL_DX7_BTN].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                c1Transaction.Cols[COL_DX8_BTN].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;

                c1Transaction.Cols[COL_MOD1_BTN].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                c1Transaction.Cols[COL_MOD2_BTN].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                c1Transaction.Cols[COL_MOD3_BTN].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                c1Transaction.Cols[COL_MOD4_BTN].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;

                c1Transaction.Cols[COL_PROVIDER_BTN].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                c1Transaction.Cols[COL_NOTE_BTN].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                #endregion

                #region "Width"

                int nWidth = pnlMain.Width;

                //c1Transaction.Cols[COL_NO].Width = 20;
                c1Transaction.Cols[COL_NO].Width = 37;//Convert.ToInt32(nWidth * 0.02);
                c1Transaction.Cols[COL_TRANSACTIONID].Width = 0;

                c1Transaction.Cols[COL_DATEFROM].Width = 84;//Convert.ToInt32(nWidth * 0.08);


                if (_showTilldateColumn == true)
                {
                    c1Transaction.Cols[COL_DATETO].Width = 84;
                }
                else
                {
                    c1Transaction.Cols[COL_DATETO].Width = 0;
                }

                if (_showPOSColumn == true)
                {
                    c1Transaction.Cols[COL_POSCODE].Width = 50;//Convert.ToInt32(nWidth * 0.05);
                }
                else
                {
                    c1Transaction.Cols[COL_POSCODE].Width = 0;//Convert.ToInt32(nWidth * 0.05);
                }
                // Vijay
                if (_showSplitClaimToPatient == true)
                {
                    c1Transaction.Cols[COL_SELFCLAIM].Width = 32;//Convert.ToInt32(nWidth * 0.05);
                }
                else
                {
                    c1Transaction.Cols[COL_SELFCLAIM].Width = 0;//Convert.ToInt32(nWidth * 0.05);
                }
                //End 
                c1Transaction.Cols[COL_POSDESC].Width = 0;
                c1Transaction.Cols[COL_POS_BTN].Width = 0;

                c1Transaction.Cols[COL_TOSCODE].Width = 61;//Convert.ToInt32(nWidth * 0.05);
                c1Transaction.Cols[COL_TOSDESC].Width = 0;
                c1Transaction.Cols[COL_TOS_BTN].Width = 0;

                if (_showEMGColumn == true)
                {
                    c1Transaction.Cols[COL_ISEMG].Width = 38;//Convert.ToInt32(nWidth * 0.05);
                }
                else
                {
                    c1Transaction.Cols[COL_ISEMG].Width = 0;//Convert.ToInt32(nWidth * 0.05);
                }


                c1Transaction.Cols[COL_CPT_CODE].Width = 80;//Convert.ToInt32(nWidth * 0.05);
                c1Transaction.Cols[COL_CPT_DESC].Width = 0;
                c1Transaction.Cols[COL_CPT_BTN].Width = 0;

                c1Transaction.Cols[COL_DX1_CODE].Width = 70;//Convert.ToInt32(nWidth * 0.06);
                c1Transaction.Cols[COL_DX1_DESC].Width = 0;
                c1Transaction.Cols[COL_DX1_BTN].Width = 0;
                c1Transaction.Cols[COL_DX2_CODE].Width = 70;//Convert.ToInt32(nWidth * 0.06);
                c1Transaction.Cols[COL_DX2_DESC].Width = 0;
                c1Transaction.Cols[COL_DX2_BTN].Width = 0;
                c1Transaction.Cols[COL_DX3_CODE].Width = 70;//Convert.ToInt32(nWidth * 0.06);
                c1Transaction.Cols[COL_DX3_DESC].Width = 0;
                c1Transaction.Cols[COL_DX3_BTN].Width = 0;
                c1Transaction.Cols[COL_DX4_CODE].Width = 70;//Convert.ToInt32(nWidth * 0.06);
                c1Transaction.Cols[COL_DX4_DESC].Width = 0;
                c1Transaction.Cols[COL_DX4_BTN].Width = 0;
                c1Transaction.Cols[COL_DX5_CODE].Width = 60;
                c1Transaction.Cols[COL_DX5_DESC].Width = 0;
                c1Transaction.Cols[COL_DX5_BTN].Width = 0;
                c1Transaction.Cols[COL_DX6_CODE].Width = 60;
                c1Transaction.Cols[COL_DX6_DESC].Width = 0;
                c1Transaction.Cols[COL_DX6_BTN].Width = 0;
                c1Transaction.Cols[COL_DX7_CODE].Width = 60;
                c1Transaction.Cols[COL_DX7_DESC].Width = 0;
                c1Transaction.Cols[COL_DX7_BTN].Width = 0;
                c1Transaction.Cols[COL_DX8_CODE].Width = 60;
                c1Transaction.Cols[COL_DX8_DESC].Width = 0;
                c1Transaction.Cols[COL_DX8_BTN].Width = 0;

                c1Transaction.Cols[COL_DX1_PTR].Width = 21;//Convert.ToInt32(nWidth * 0.03);
                c1Transaction.Cols[COL_DX2_PTR].Width = 21;//Convert.ToInt32(nWidth * 0.03);
                c1Transaction.Cols[COL_DX3_PTR].Width = 21;//Convert.ToInt32(nWidth * 0.03);
                c1Transaction.Cols[COL_DX4_PTR].Width = 21;//Convert.ToInt32(nWidth * 0.03);
                c1Transaction.Cols[COL_DX5_PTR].Width = 21;
                c1Transaction.Cols[COL_DX6_PTR].Width = 21;
                c1Transaction.Cols[COL_DX7_PTR].Width = 21;
                c1Transaction.Cols[COL_DX8_PTR].Width = 21;

                c1Transaction.Cols[COL_MOD1_CODE].Width = 40;//Convert.ToInt32(nWidth * 0.05);
                c1Transaction.Cols[COL_MOD1_DESC].Width = 0;
                c1Transaction.Cols[COL_MOD1_BTN].Width = 0;
                c1Transaction.Cols[COL_MOD2_CODE].Width = 40;//Convert.ToInt32(nWidth * 0.05);
                c1Transaction.Cols[COL_MOD2_DESC].Width = 0;
                c1Transaction.Cols[COL_MOD2_BTN].Width = 0;
                c1Transaction.Cols[COL_MOD3_CODE].Width = 40;
                c1Transaction.Cols[COL_MOD3_DESC].Width = 0;
                c1Transaction.Cols[COL_MOD3_BTN].Width = 0;
                c1Transaction.Cols[COL_MOD4_CODE].Width = 40;
                c1Transaction.Cols[COL_MOD4_DESC].Width = 0;
                c1Transaction.Cols[COL_MOD4_BTN].Width = 0;

                c1Transaction.Cols[COL_CHARGES].Width = 130;//Convert.ToInt32(nWidth * 0.06);
                c1Transaction.Cols[COL_BILLEDAMOUNT].Width = 120;//Convert.ToInt32(nWidth * 0.06);
                c1Transaction.Cols[COL_UNIT].Width = 60;//Convert.ToInt32(nWidth * 0.03);
                c1Transaction.Cols[COL_TOTAL].Width = 130;// Convert.ToInt32(nWidth * 0.08);

                if (_showAllowedColumn == true)
                {
                    c1Transaction.Cols[COL_ALLOWED].Width = 120;// Convert.ToInt32(nWidth * 0.07);
                }
                else
                {
                    c1Transaction.Cols[COL_ALLOWED].Width = 0;// Convert.ToInt32(nWidth * 0.07);
                }
                c1Transaction.Cols[COL_PROVIDER_ID].Width = 0;
                c1Transaction.Cols[COL_PROVIDER].Width = 210;//Convert.ToInt32(nWidth * 0.20);                 
                c1Transaction.Cols[COL_PROVIDER_BTN].Width = 0;
                c1Transaction.Cols[COL_NOTE_BTN].Width = 0;
                c1Transaction.Cols[COL_NOTE_DATA].Width = 0;
                c1Transaction.Cols[COL_NOTE_TYPE].Width = 0;

                c1Transaction.Cols[COL_INSURANCEID].Width = 0;
                c1Transaction.Cols[COL_INSURANCENAME].Width = 150;

                c1Transaction.Cols[COL_INSSELF_PAYMODE].Width = 0;

                c1Transaction.Cols[COL_HOLD].Width = 40;
                c1Transaction.Cols[COL_HOLD_REASON].Width = 120;

                c1Transaction.Cols[COL_TRANSACTION_DETAIL_ID].Width = 0;

                c1Transaction.Cols[COL_ISLABCPT].Width = 28;
                c1Transaction.Cols[COL_AUTHORIZATIONNO].Width = 100;
                c1Transaction.Cols[COL_SENTTOCLAIM].Width = 60;

                c1Transaction.Cols[COL_LINEPRIMARY_DXCODE].Width = 60;
                c1Transaction.Cols[COL_LINEPRIMARY_DXDESC].Width = 60;

                c1Transaction.Cols[COL_ACTUAL_ALLOWED].Width = 0;

                c1Transaction.Cols[COL_MST_TRANSACTIONID].Width = 0;
                c1Transaction.Cols[COL_MST_TRANSACTIONDTLID].Width = 0;

                c1Transaction.Cols[COL_NDCCODE].Width = 0;
                c1Transaction.Cols[COL_NDCUNIT].Width = 0;
                c1Transaction.Cols[COL_NDCUNITCODE].Width = 0;
                c1Transaction.Cols[COL_NDCUNITDESCRITION].Width = 0;
                c1Transaction.Cols[COL_PRESCRIPTION].Width = 0;
                c1Transaction.Cols[COL_PRESCRIPTIONDESC].Width = 0;
                //c1Transaction.Cols[COL_NDCDESCRIPTION].Width = 0;
                //c1Transaction.Cols[COL_NDCUNITCODE].Width = 0;
                //c1Transaction.Cols[COL_NDCUNITDESCRIPTION].Width = 0;

                //c1Transaction.Cols[COL_NDCUNIT].Width = 0;
                //c1Transaction.Cols[COL_NDCUNITPRICING].Width = 0;
                //c1Transaction.Cols[COL_DISPLAYNDCCODE_HCFA].Width = 0;


                c1Transaction.Cols[COL_ANES_ID].Width = 0;
                c1Transaction.Cols[COL_ANES_STARTDATE].Width = 0;
                c1Transaction.Cols[COL_ANES_ENDDATE].Width = 0;
                c1Transaction.Cols[COL_ANES_TOTALMIN].Width = 0;
                c1Transaction.Cols[COL_ANES_MINPERUNIT].Width = 0;
                c1Transaction.Cols[COL_ANES_TIMEUNITS].Width = 0;
                c1Transaction.Cols[COL_ANES_BASEUNITS].Width = 0;
                c1Transaction.Cols[COL_ANES_OTHERUNITS].Width = 0;
                c1Transaction.Cols[COL_ANES_TOTALUNITS].Width = 0;
                c1Transaction.Cols[COL_ANES_ISANESTHESIA].Width = 0;
                c1Transaction.Cols[COL_ANES_ISAUTOCALCULATE].Width = 0;

                #endregion

                #region "Show/Hide"
                c1Transaction.Cols[COL_NO].Visible = true;
                c1Transaction.Cols[COL_TRANSACTIONID].Visible = false;

                c1Transaction.Cols[COL_DATEFROM].Visible = true;


                if (_showTilldateColumn == true)
                {
                    c1Transaction.Cols[COL_DATETO].Visible = true;
                }
                else
                {
                    c1Transaction.Cols[COL_DATETO].Visible = false;
                }
                ////vijay
                //if (_showSplitClaimToPatient == true)
                //{
                //    c1Total.Cols[COL_SELFCLAIM].Visible = true ;
                //}
                //else
                //{
                //    c1Transaction.Cols[COL_SELFCLAIM].Visible = false;
                //}
                ////end 

                if (_showPOSColumn == true)
                {
                    c1Transaction.Cols[COL_POSCODE].Visible = true;
                }
                else
                {
                    c1Transaction.Cols[COL_POSCODE].Visible = false;
                }
                //Vijay
                if (_showSplitClaimToPatient == true)
                {
                    c1Transaction.Cols[COL_SELFCLAIM].Visible = true;
                }
                else
                {
                    c1Transaction.Cols[COL_SELFCLAIM].Visible = false;
                }
                //End
                if (_showEMGColumn == true)
                {
                    c1Transaction.Cols[COL_ISEMG].Visible = true;
                }
                else
                {
                    c1Transaction.Cols[COL_ISEMG].Visible = false;
                }


                c1Transaction.Cols[COL_POSDESC].Visible = false;
                //c1Transaction.Cols[COL_POS_BTN].Visible = true;
                c1Transaction.Cols[COL_POS_BTN].Visible = false;

                c1Transaction.Cols[COL_TOSCODE].Visible = false;
                c1Transaction.Cols[COL_TOSDESC].Visible = false;
                //c1Transaction.Cols[COL_TOS_BTN].Visible = true;
                c1Transaction.Cols[COL_TOS_BTN].Visible = false;


                c1Transaction.Cols[COL_ISEMG].Visible = true;

                c1Transaction.Cols[COL_CPT_CODE].Visible = true;
                c1Transaction.Cols[COL_CPT_DESC].Visible = false;
                //c1Transaction.Cols[COL_CPT_BTN].Visible = true;
                c1Transaction.Cols[COL_CPT_BTN].Visible = false;

                //Make all Diagnosis Columns false
                c1Transaction.Cols[COL_DX1_CODE].Visible = false;
                c1Transaction.Cols[COL_DX1_DESC].Visible = false;
                //c1Transaction.Cols[COL_DX1_BTN].Visible = true;
                c1Transaction.Cols[COL_DX1_BTN].Visible = false;
                c1Transaction.Cols[COL_DX2_CODE].Visible = false;
                c1Transaction.Cols[COL_DX2_DESC].Visible = false;
                //c1Transaction.Cols[COL_DX2_BTN].Visible = true;
                c1Transaction.Cols[COL_DX2_BTN].Visible = false;
                c1Transaction.Cols[COL_DX3_CODE].Visible = false;
                c1Transaction.Cols[COL_DX3_DESC].Visible = false;
                //c1Transaction.Cols[COL_DX3_BTN].Visible = true;
                c1Transaction.Cols[COL_DX3_BTN].Visible = false;
                c1Transaction.Cols[COL_DX4_CODE].Visible = false;
                c1Transaction.Cols[COL_DX4_DESC].Visible = false;
                //c1Transaction.Cols[COL_DX4_BTN].Visible = true;
                c1Transaction.Cols[COL_DX4_BTN].Visible = false;
                c1Transaction.Cols[COL_DX5_CODE].Visible = false; //
                c1Transaction.Cols[COL_DX5_DESC].Visible = false;
                c1Transaction.Cols[COL_DX5_BTN].Visible = false;
                c1Transaction.Cols[COL_DX6_CODE].Visible = false; //
                c1Transaction.Cols[COL_DX6_DESC].Visible = false;
                c1Transaction.Cols[COL_DX6_BTN].Visible = false;
                c1Transaction.Cols[COL_DX7_CODE].Visible = false; //
                c1Transaction.Cols[COL_DX7_DESC].Visible = false;
                c1Transaction.Cols[COL_DX7_BTN].Visible = false;
                c1Transaction.Cols[COL_DX8_CODE].Visible = false; //
                c1Transaction.Cols[COL_DX8_DESC].Visible = false;
                c1Transaction.Cols[COL_DX8_BTN].Visible = false;

                int _DiagnosisPointer = 0;
                for (int i = COL_DX1_CODE; _DiagnosisPointer < 4 && i <= COL_DX8_CODE; i += 3)
                {
                    c1Transaction.Cols[i].Visible = true;
                    _DiagnosisPointer = _DiagnosisPointer + 1;
                }


                c1Transaction.Cols[COL_DX1_PTR].Visible = false;
                c1Transaction.Cols[COL_DX2_PTR].Visible = false;
                c1Transaction.Cols[COL_DX3_PTR].Visible = false;
                c1Transaction.Cols[COL_DX4_PTR].Visible = false;
                c1Transaction.Cols[COL_DX5_PTR].Visible = false;
                c1Transaction.Cols[COL_DX6_PTR].Visible = false;
                c1Transaction.Cols[COL_DX7_PTR].Visible = false;
                c1Transaction.Cols[COL_DX8_PTR].Visible = false;

                int _DiagnosisPointerCounter = 0;
                for (int i = COL_DX1_PTR; _DiagnosisPointerCounter < 4 && i <= COL_DX8_PTR; i++)
                {
                    c1Transaction.Cols[i].Visible = true;
                    _DiagnosisPointerCounter = _DiagnosisPointerCounter + 1;
                }



                c1Transaction.Cols[COL_MOD1_CODE].Visible = false;
                c1Transaction.Cols[COL_MOD1_DESC].Visible = false;
                c1Transaction.Cols[COL_MOD1_BTN].Visible = false;
                c1Transaction.Cols[COL_MOD2_CODE].Visible = false;
                c1Transaction.Cols[COL_MOD2_DESC].Visible = false;
                c1Transaction.Cols[COL_MOD2_BTN].Visible = false;
                c1Transaction.Cols[COL_MOD3_CODE].Visible = false;
                c1Transaction.Cols[COL_MOD3_DESC].Visible = false;
                c1Transaction.Cols[COL_MOD3_BTN].Visible = false;
                c1Transaction.Cols[COL_MOD4_CODE].Visible = false;
                c1Transaction.Cols[COL_MOD4_DESC].Visible = false;
                c1Transaction.Cols[COL_MOD4_BTN].Visible = false;

                int _ModifiersCounter = 0;
                for (int i = COL_MOD1_CODE; _ModifiersCounter < _NoOfModifiers && i <= COL_MOD4_CODE; i += 3)
                {
                    c1Transaction.Cols[i].Visible = true;
                    _ModifiersCounter = _ModifiersCounter + 1;
                }


                c1Transaction.Cols[COL_CHARGES].Visible = true;
                c1Transaction.Cols[COL_UNIT].Visible = true;
                c1Transaction.Cols[COL_TOTAL].Visible = true;
                if (_showAllowedColumn == true)
                {
                    c1Transaction.Cols[COL_ALLOWED].Visible = true;
                }
                else
                {
                    c1Transaction.Cols[COL_ALLOWED].Visible = false;
                }
                c1Transaction.Cols[COL_PROVIDER_ID].Visible = false;
                c1Transaction.Cols[COL_PROVIDER].Visible = true;
                //c1Transaction.Cols[COL_PROVIDER_BTN].Visible = true;
                c1Transaction.Cols[COL_PROVIDER_BTN].Visible = false;
                c1Transaction.Cols[COL_NOTE_BTN].Visible = false;
                c1Transaction.Cols[COL_NOTE_DATA].Visible = false;
                c1Transaction.Cols[COL_NOTE_TYPE].Visible = false;

                c1Transaction.Cols[COL_INSURANCEID].Visible = false;
                if (_showInsurance == true)
                {
                    c1Transaction.Cols[COL_INSURANCENAME].Visible = true;
                }
                else
                {
                    c1Transaction.Cols[COL_INSURANCENAME].Visible = false;
                }

                c1Transaction.Cols[COL_INSSELF_PAYMODE].Visible = false;

                //c1Transaction.Cols[COL_HOLD].Visible = true;
                //c1Transaction.Cols[COL_HOLD_REASON].Visible = true;
                c1Transaction.Cols[COL_HOLD].Visible = false;
                c1Transaction.Cols[COL_HOLD_REASON].Visible = false;

                c1Transaction.Cols[COL_TRANSACTION_DETAIL_ID].Visible = false;

                //c1Transaction.Cols[COL_ISLABCPT].Visible = true;
                //c1Transaction.Cols[COL_AUTHORIZATIONNO].Visible = true;

                c1Transaction.Cols[COL_ISLABCPT].Visible = ShowLabColumn;
                c1Transaction.Cols[COL_AUTHORIZATIONNO].Visible = ShowLabColumn;

                c1Transaction.Cols[COL_SENTTOCLAIM].Visible = false;
                //c1Transaction.Cols[COL_SENTTOCLAIM].Visible = true;

                c1Transaction.Cols[COL_LINEPRIMARY_DXCODE].Visible = false;
                c1Transaction.Cols[COL_LINEPRIMARY_DXDESC].Visible = false;

                c1Transaction.Cols[COL_ACTUAL_ALLOWED].Visible = false;

                c1Transaction.Cols[COL_MST_TRANSACTIONID].Visible = false;
                c1Transaction.Cols[COL_MST_TRANSACTIONDTLID].Visible = false;

                c1Transaction.Cols[COL_BILLEDAMOUNT].Visible = false;


                c1Transaction.Cols[COL_FEESCHEDULEID].Visible = false;
                c1Transaction.Cols[COL_FEESCHEDULETYPE].Visible = false;
                c1Transaction.Cols[COL_FACILITYTYPE].Visible = false;


                c1Transaction.Cols[COL_NDCCODE].Visible = false;
                c1Transaction.Cols[COL_NDCUNIT].Visible = false;
                c1Transaction.Cols[COL_NDCUNITCODE].Visible = false;
                c1Transaction.Cols[COL_NDCUNITDESCRITION].Visible = false;
                c1Transaction.Cols[COL_PRESCRIPTION].Visible = false;
                c1Transaction.Cols[COL_PRESCRIPTIONDESC].Visible = false;

                //c1Transaction.Cols[COL_NDCDESCRIPTION].Visible = false;
                //c1Transaction.Cols[COL_NDCUNITCODE].Visible = false;
                //c1Transaction.Cols[COL_NDCUNITDESCRIPTION].Visible = false;

                //c1Transaction.Cols[COL_NDCUNIT].Visible = false;
                //c1Transaction.Cols[COL_NDCUNITPRICING].Visible = false;
                //c1Transaction.Cols[COL_DISPLAYNDCCODE_HCFA].Visible = false;

                c1Transaction.Cols[COL_SERVICESCREENING].Visible = false;
                c1Transaction.Cols[COL_SERVICERESULTSCREENING].Visible = false;
                c1Transaction.Cols[COL_FAMILYPLANNINGINDICATOR].Visible = false;



                c1Transaction.Cols[COL_ANES_ID].Visible = false;
                c1Transaction.Cols[COL_ANES_STARTDATE].Visible = false;
                c1Transaction.Cols[COL_ANES_ENDDATE].Visible = false;
                c1Transaction.Cols[COL_ANES_TOTALMIN].Visible = false;
                c1Transaction.Cols[COL_ANES_MINPERUNIT].Visible = false;
                c1Transaction.Cols[COL_ANES_TIMEUNITS].Visible = false;
                c1Transaction.Cols[COL_ANES_BASEUNITS].Visible = false;
                c1Transaction.Cols[COL_ANES_OTHERUNITS].Visible = false;
                c1Transaction.Cols[COL_ANES_TOTALUNITS].Visible = false;
                c1Transaction.Cols[COL_ANES_ISANESTHESIA].Visible = false;
                c1Transaction.Cols[COL_ANES_ISAUTOCALCULATE].Visible = false;

                c1Transaction.Cols[COL_EMRTREATMENTLINENO].Visible = false;
                // Line added by sameer for split claim option on New Charges Entry  11-11-2013

                if (_IsOpenForModify == true || _showSplitClaimToPatient == false)
                {
                    c1Transaction.Cols[COL_SELFCLAIM].Visible = false;
                }



                #endregion

                #region "Header"
                c1Transaction.SetData(0, COL_NO, "No.");
                c1Transaction.SetData(0, COL_TRANSACTIONID, "TransactionId");

                c1Transaction.SetData(0, COL_DATEFROM, "DOS");
                c1Transaction.SetData(0, COL_DATETO, "To DOS");

                c1Transaction.SetData(0, COL_POSCODE, "POS");
                c1Transaction.SetData(0, COL_POSDESC, "POS Description");
                c1Transaction.SetData(0, COL_POS_BTN, "");

                c1Transaction.SetData(0, COL_TOSCODE, "TOS");
                c1Transaction.SetData(0, COL_TOSDESC, "TOS Description");
                c1Transaction.SetData(0, COL_TOS_BTN, "");


                c1Transaction.SetData(0, COL_ISEMG, "EMG");

                c1Transaction.SetData(0, COL_CPT_CODE, "CPT");
                c1Transaction.SetData(0, COL_CPT_DESC, "CPT Desc");
                c1Transaction.SetData(0, COL_CPT_BTN, "");

                c1Transaction.SetData(0, COL_DX1_CODE, "Dx1");
                c1Transaction.SetData(0, COL_DX1_DESC, "Dx1 Desc");
                c1Transaction.SetData(0, COL_DX1_BTN, "");
                c1Transaction.SetData(0, COL_DX2_CODE, "Dx2");
                c1Transaction.SetData(0, COL_DX2_DESC, "Dx2 Desc");
                c1Transaction.SetData(0, COL_DX2_BTN, "");
                c1Transaction.SetData(0, COL_DX3_CODE, "Dx3");
                c1Transaction.SetData(0, COL_DX3_DESC, "Dx3 Desc");
                c1Transaction.SetData(0, COL_DX3_BTN, "");
                c1Transaction.SetData(0, COL_DX4_CODE, "Dx4");
                c1Transaction.SetData(0, COL_DX4_DESC, "Dx4 Desc");
                c1Transaction.SetData(0, COL_DX4_BTN, "");
                c1Transaction.SetData(0, COL_DX5_CODE, "Dx5");
                c1Transaction.SetData(0, COL_DX5_DESC, "Dx5 Desc");
                c1Transaction.SetData(0, COL_DX5_BTN, "");
                c1Transaction.SetData(0, COL_DX6_CODE, "Dx6");
                c1Transaction.SetData(0, COL_DX6_DESC, "Dx6 Desc");
                c1Transaction.SetData(0, COL_DX6_BTN, "");
                c1Transaction.SetData(0, COL_DX7_CODE, "Dx7");
                c1Transaction.SetData(0, COL_DX7_DESC, "Dx7 Desc");
                c1Transaction.SetData(0, COL_DX7_BTN, "");
                c1Transaction.SetData(0, COL_DX8_CODE, "Dx8");
                c1Transaction.SetData(0, COL_DX8_DESC, "Dx8 Desc");
                c1Transaction.SetData(0, COL_DX8_BTN, "");


                c1Transaction.SetData(0, COL_DX1_PTR, "D1");
                c1Transaction.SetData(0, COL_DX2_PTR, "D2");
                c1Transaction.SetData(0, COL_DX3_PTR, "D3");
                c1Transaction.SetData(0, COL_DX4_PTR, "D4");
                c1Transaction.SetData(0, COL_DX5_PTR, "D5");
                c1Transaction.SetData(0, COL_DX6_PTR, "D6");
                c1Transaction.SetData(0, COL_DX7_PTR, "D7");
                c1Transaction.SetData(0, COL_DX8_PTR, "D8");


                c1Transaction.SetData(0, COL_MOD1_CODE, "M1");
                c1Transaction.SetData(0, COL_MOD1_DESC, "M1 Desc");
                c1Transaction.SetData(0, COL_MOD1_BTN, "");
                c1Transaction.SetData(0, COL_MOD2_CODE, "M2");
                c1Transaction.SetData(0, COL_MOD2_DESC, "M2 Desc");
                c1Transaction.SetData(0, COL_MOD2_BTN, "");
                c1Transaction.SetData(0, COL_MOD3_CODE, "M3");
                c1Transaction.SetData(0, COL_MOD3_DESC, "M3 Desc");
                c1Transaction.SetData(0, COL_MOD3_BTN, "");
                c1Transaction.SetData(0, COL_MOD4_CODE, "M4");
                c1Transaction.SetData(0, COL_MOD4_DESC, "M4 Desc");
                c1Transaction.SetData(0, COL_MOD4_BTN, "");



                c1Transaction.SetData(0, COL_CHARGES, "Charges");
                c1Transaction.SetData(0, COL_BILLEDAMOUNT, "Billed Amt");
                c1Transaction.SetData(0, COL_UNIT, "Unit");
                c1Transaction.SetData(0, COL_TOTAL, "Total");
                c1Transaction.SetData(0, COL_ALLOWED, "Allowed");
                c1Transaction.SetData(0, COL_PROVIDER_ID, "ProviderID");
                c1Transaction.SetData(0, COL_PROVIDER, "Provider");
                c1Transaction.SetData(0, COL_PROVIDER_BTN, "");
                c1Transaction.SetData(0, COL_NOTE_BTN, "Note");
                c1Transaction.SetData(0, COL_NOTE_DATA, "Note");
                c1Transaction.SetData(0, COL_NOTE_TYPE, "Note Type");

                c1Transaction.SetData(0, COL_INSURANCEID, "InsuranceID");
                c1Transaction.SetData(0, COL_INSURANCENAME, "Insurance");

                c1Transaction.SetData(0, COL_INSSELF_PAYMODE, "PayMode");

                c1Transaction.SetData(0, COL_HOLD, "Hold");
                c1Transaction.SetData(0, COL_HOLD_REASON, "Hold Reason");

                c1Transaction.SetData(0, COL_TRANSACTION_DETAIL_ID, "TransactionDetail ID");

                c1Transaction.SetData(0, COL_ISLABCPT, "Lab");
                c1Transaction.SetData(0, COL_AUTHORIZATIONNO, "CLIA No.");
                c1Transaction.SetData(0, COL_SENTTOCLAIM, "SentToClaim");

                c1Transaction.SetData(0, COL_LINEPRIMARY_DXCODE, "PrimaryDxCode");
                c1Transaction.SetData(0, COL_LINEPRIMARY_DXDESC, "PrimaryDxDesc");

                c1Transaction.SetData(0, COL_ACTUAL_ALLOWED, "ActualAllowed");

                c1Transaction.SetData(0, COL_MST_TRANSACTIONID, "MasterTranID");
                c1Transaction.SetData(0, COL_MST_TRANSACTIONDTLID, "MasterTranDtlID");

                c1Transaction.SetData(0, COL_NDCCODE, "NDC Code");
                c1Transaction.SetData(0, COL_NDCUNIT, "NDC Unit");
                c1Transaction.SetData(0, COL_NDCUNITCODE, "NDC Unit Code");
                c1Transaction.SetData(0, COL_NDCUNITDESCRITION, "NDC Unit Description");
                c1Transaction.SetData(0, COL_PRESCRIPTION, "Prescription");
                c1Transaction.SetData(0, COL_PRESCRIPTION, "Prescription Description");
                //c1Transaction.SetData(0, COL_NDCDESCRIPTION, "NDC Description");
                //c1Transaction.SetData(0, COL_NDCUNITCODE, "NDC Unit Code");
                //c1Transaction.SetData(0, COL_NDCUNITDESCRIPTION, "NDC Unit Description");
                //c1Transaction.SetData(0, COL_NDCUNIT, "NDC Unit");
                //c1Transaction.SetData(0, COL_NDCUNITPRICING, "NDC Unit Pricing");
                //c1Transaction.SetData(0, COL_DISPLAYNDCCODE_HCFA, "NDC Display NDC Code HCFA");

                c1Transaction.SetData(0, COL_SERVICESCREENING, "Service Screening");
                c1Transaction.SetData(0, COL_SERVICERESULTSCREENING, "Service Result Screening");
                c1Transaction.SetData(0, COL_FAMILYPLANNINGINDICATOR, "Family Planning Indicator");

                c1Transaction.SetData(0, COL_ANES_ID, "AnesthesiaID");

                c1Transaction.SetData(0, COL_ANES_STARTDATE, "Anesthesia StartDate");
                c1Transaction.SetData(0, COL_ANES_ENDDATE, "Anesthesia EndDate");
                c1Transaction.SetData(0, COL_ANES_TOTALMIN, "Anesthesia TotalMin");
                c1Transaction.SetData(0, COL_ANES_MINPERUNIT, "Anesthesia MinPerMin");
                c1Transaction.SetData(0, COL_ANES_TIMEUNITS, "Anesthesia TimeUnits");
                c1Transaction.SetData(0, COL_ANES_BASEUNITS, "Anesthesia BaseUnits");
                c1Transaction.SetData(0, COL_ANES_OTHERUNITS, "Anesthesia OtherUnits");
                c1Transaction.SetData(0, COL_ANES_TOTALUNITS, "Anesthesia TotalUnits");
                c1Transaction.SetData(0, COL_ANES_ISANESTHESIA, "IsAnesthesia");
                c1Transaction.SetData(0, COL_ANES_ISAUTOCALCULATE, "IsAutoCalculate");
                c1Transaction.SetData(0, COL_SELFCLAIM, "Self");
                #endregion

                #region "Styles"
                c1Transaction.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                c1Transaction.BackColor = Color.FromArgb(222, 231, 250);
                c1Transaction.ForeColor = Color.FromArgb(21, 66, 139);
                c1Transaction.Height = 300;

                CellStyle csStyle;// = c1Transaction.Styles.Add("rsTransactionLine");
                try
                {
                    if (c1Transaction.Styles.Contains("rsTransactionLine"))
                    {
                        csStyle = c1Transaction.Styles["rsTransactionLine"];
                    }
                    else
                    {
                        csStyle = c1Transaction.Styles.Add("rsTransactionLine");

                    }

                }
                catch
                {
                    csStyle = c1Transaction.Styles.Add("rsTransactionLine");


                }


                CellStyle csCurrency;// = c1Transaction.Styles.Add("csCurrencyCell");
                try
                {
                    if (c1Transaction.Styles.Contains("csCurrencyCell"))
                    {
                        csCurrency = c1Transaction.Styles["csCurrencyCell"];
                    }
                    else
                    {
                        csCurrency = c1Transaction.Styles.Add("csCurrencyCell");
                        csCurrency.DataType = typeof(System.Decimal);
                        csCurrency.Format = "c";
                        csCurrency.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                    }

                }
                catch
                {
                    csCurrency = c1Transaction.Styles.Add("csCurrencyCell");
                    csCurrency.DataType = typeof(System.Decimal);
                    csCurrency.Format = "c";
                    csCurrency.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));


                }

                #endregion "Styles"




                c1Transaction.Cols[COL_NO].AllowEditing = false;
                c1Transaction.Cols[COL_TOTAL].AllowEditing = false;
                c1Transaction.Cols[COL_INSURANCENAME].AllowEditing = false;
                c1Transaction.Cols[COL_LINEPRIMARY_DXCODE].AllowEditing = false;
                c1Transaction.Cols[COL_LINEPRIMARY_DXDESC].AllowEditing = false;


                c1Transaction.Cols[COL_DX1_PTR].AllowEditing = false;
                c1Transaction.Cols[COL_DX2_PTR].AllowEditing = false;
                c1Transaction.Cols[COL_DX3_PTR].AllowEditing = false;
                c1Transaction.Cols[COL_DX4_PTR].AllowEditing = false;
                c1Transaction.Cols[COL_ALLOWED].AllowEditing = false;

                c1Transaction.Cols[COL_CHARGES].Style = csCurrency;
                c1Transaction.Cols[COL_BILLEDAMOUNT].Style = csCurrency;
                c1Transaction.Cols[COL_TOTAL].Style = csCurrency;
                c1Transaction.Cols[COL_ALLOWED].Style = csCurrency;
                c1Transaction.Cols[COL_ACTUAL_ALLOWED].Style = csCurrency;

                for (int i = 0; i <= c1Transaction.Rows.Count - 1; i++)
                {
                    c1Transaction.Rows[i].Height = 23;
                    if (i > 0)
                    {
                        c1Transaction.SetData(i, 0, i.ToString());
                        c1Transaction.SetData(i, COL_DATEFROM, DateTime.Now.Date.ToShortDateString());
                        //c1Transaction.SetData(i, 0, i.ToString()); 
                        c1Transaction.SetData(i, COL_DATEFROM, DateTime.Now.Date.ToShortDateString());

                        GetDefaultTOSPOS();
                        c1Transaction.SetData(i, COL_TOSCODE, _DefaultTOSCode);
                        c1Transaction.SetData(i, COL_TOSDESC, _DefaultTOSDesc);
                        c1Transaction.SetData(i, COL_POSCODE, _DefaultPOSCode);
                        c1Transaction.SetData(i, COL_POSDESC, _DefaultPOSDesc);


                        c1Transaction.SetData(i, COL_ISEMG, false);

                        c1Transaction.SetData(i, COL_ISLABCPT, false);
                        c1Transaction.SetData(i, COL_AUTHORIZATIONNO, "");
                        c1Transaction.SetData(i, COL_SENTTOCLAIM, true);

                        //**************************************************************************
                        //Code Changes made on 20081017 By - Sagar Ghodke
                        //Below commented code is previous code

                        ////Set the Default Renderring Provider 
                        //if (_DefaultRenderingProviderID > 0)
                        //{
                        //    c1Transaction.SetData(i, COL_PROVIDER_ID, _DefaultRenderingProviderID);
                        //    c1Transaction.SetData(i, COL_PROVIDER, _DefaultRenderringProviderName);
                        //}

                        //Set the Patient Provider as the Default Renderring Provider 
                        if (_DefaultRenderingProviderID > 0)
                        {
                            c1Transaction.SetData(i, COL_PROVIDER_ID, _DefaultRenderingProviderID);
                            c1Transaction.SetData(i, COL_PROVIDER, _DefaultRenderringProviderName);
                        }
                        else
                        {

                        }
                        // Line added for split claim option on New Charges Entry  11-11-2013
                        c1Transaction.SetData(i, COL_SELFCLAIM, false);
                        //End Code Chages - 20081017 
                        //**************************************************************************
                        SetCurrencyCellValue(i);


                    }
                }
                //c1Transaction.Cols[COL_DATEFROM].Format = "d";
                c1Transaction.Cols[COL_DATEFROM].Format = "MM/dd/yyyy";
                c1Transaction.Cols[COL_DATETO].Format = "MM/dd/yyyy";
                //c1Transaction.Cols[COL_UNIT].Format = "#############0.####";
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                this.c1Transaction.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Transaction_CellChanged);
                //c1Transaction.ScrollBars = ScrollBars.Both; 
                _isControlInDesignMode = false;
            }

        }
        private void oTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //int IndexofDecimal = (((TextBox)sender).Text.Contains(".") == false ? -1 : ((TextBox)sender).Text.Trim().Substring(((TextBox)sender).Text.Trim().IndexOf(".")).Length);
                //if (((((TextBox)sender).Text.Contains(".")) && (e.KeyChar == 46)))
                //{
                //    e.Handled = true;
                //}
                //else
                //{
                //    e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != 46;
                //}

                if (((TextBox)sender).Text.Contains(".") == true)
                {
                    int iTextLength = ((TextBox)sender).Text.Length;
                    int iDecimalLength = ((TextBox)sender).Text.Trim().Substring(((TextBox)sender).Text.Trim().IndexOf(".")).Length;
                    if (iTextLength - iDecimalLength > 4)
                    {

                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);

            }
        }

        private void DesignTotalGrid()
        {
            try
            {
                //c1Total.ScrollBars = ScrollBars.None;

                c1Total.Clear();
                c1Total.Rows.Count = 1;
                c1Total.Rows.Fixed = 1;
                c1Total.Cols.Count = COL_COUNT;
                c1Total.Cols.Fixed = 1;

                c1Total.Rows[0].Height = 23;

                c1Total.AllowSorting = AllowSortingEnum.None;
                //c1Total.Cols[COL_ALLOWED].AllowSorting = true;
                //c1Total.Cols[COL_ALLOWED].Sort = SortFlags.Descending;

                //Added By Pramod Nair to sort the Total amount column instead of Allowed amount
                c1Total.Cols[COL_TOTAL].AllowSorting = true;
                c1Total.Cols[COL_TOTAL].Sort = SortFlags.Descending;

                c1Total.AllowDragging = AllowDraggingEnum.None;
                c1Total.AllowResizing = AllowResizingEnum.None;
                c1Total.AllowSorting = AllowSortingEnum.None;
                c1Total.AllowMerging = AllowMergingEnum.None;


                #region "Data Type"
                c1Total.Cols[COL_NO].DataType = typeof(System.Int32);
                c1Total.Cols[COL_TRANSACTIONID].DataType = typeof(System.Int64);

                c1Total.Cols[COL_DATEFROM].DataType = typeof(System.DateTime);
                c1Total.Cols[COL_DATETO].DataType = typeof(System.DateTime);

                c1Total.Cols[COL_POSCODE].DataType = typeof(System.String);
                c1Total.Cols[COL_POSDESC].DataType = typeof(System.String);
                c1Total.Cols[COL_POS_BTN].DataType = typeof(System.String);

                c1Total.Cols[COL_TOSCODE].DataType = typeof(System.String);
                c1Total.Cols[COL_TOSDESC].DataType = typeof(System.String);
                c1Total.Cols[COL_TOS_BTN].DataType = typeof(System.String);

                c1Total.Cols[COL_CPT_CODE].DataType = typeof(System.String);
                c1Total.Cols[COL_CPT_DESC].DataType = typeof(System.String);
                c1Total.Cols[COL_CPT_BTN].DataType = typeof(System.String);

                c1Total.Cols[COL_DX1_CODE].DataType = typeof(System.String);
                c1Total.Cols[COL_DX1_DESC].DataType = typeof(System.String);
                c1Total.Cols[COL_DX1_BTN].DataType = typeof(System.String);
                c1Total.Cols[COL_DX2_CODE].DataType = typeof(System.String);
                c1Total.Cols[COL_DX2_DESC].DataType = typeof(System.String);
                c1Total.Cols[COL_DX2_BTN].DataType = typeof(System.String);
                c1Total.Cols[COL_DX3_CODE].DataType = typeof(System.String);
                c1Total.Cols[COL_DX3_DESC].DataType = typeof(System.String);
                c1Total.Cols[COL_DX3_BTN].DataType = typeof(System.String);
                c1Total.Cols[COL_DX4_CODE].DataType = typeof(System.String);
                c1Total.Cols[COL_DX4_DESC].DataType = typeof(System.String);
                c1Total.Cols[COL_DX4_BTN].DataType = typeof(System.String);
                c1Total.Cols[COL_DX5_CODE].DataType = typeof(System.String);
                c1Total.Cols[COL_DX5_DESC].DataType = typeof(System.String);
                c1Total.Cols[COL_DX5_BTN].DataType = typeof(System.String);
                c1Total.Cols[COL_DX6_CODE].DataType = typeof(System.String);
                c1Total.Cols[COL_DX6_DESC].DataType = typeof(System.String);
                c1Total.Cols[COL_DX6_BTN].DataType = typeof(System.String);
                c1Total.Cols[COL_DX7_CODE].DataType = typeof(System.String);
                c1Total.Cols[COL_DX7_DESC].DataType = typeof(System.String);
                c1Total.Cols[COL_DX7_BTN].DataType = typeof(System.String);
                c1Total.Cols[COL_DX8_CODE].DataType = typeof(System.String);
                c1Total.Cols[COL_DX8_DESC].DataType = typeof(System.String);
                c1Total.Cols[COL_DX8_BTN].DataType = typeof(System.String);

                c1Total.Cols[COL_DX1_PTR].DataType = typeof(System.Boolean);
                c1Total.Cols[COL_DX2_PTR].DataType = typeof(System.Boolean);
                c1Total.Cols[COL_DX3_PTR].DataType = typeof(System.Boolean);
                c1Total.Cols[COL_DX4_PTR].DataType = typeof(System.Boolean);
                c1Total.Cols[COL_DX5_PTR].DataType = typeof(System.Boolean);
                c1Total.Cols[COL_DX6_PTR].DataType = typeof(System.Boolean);
                c1Total.Cols[COL_DX7_PTR].DataType = typeof(System.Boolean);
                c1Total.Cols[COL_DX8_PTR].DataType = typeof(System.Boolean);

                c1Total.Cols[COL_MOD1_CODE].DataType = typeof(System.String);
                c1Total.Cols[COL_MOD1_DESC].DataType = typeof(System.String);
                c1Total.Cols[COL_MOD1_BTN].DataType = typeof(System.String);
                c1Total.Cols[COL_MOD2_CODE].DataType = typeof(System.String);
                c1Total.Cols[COL_MOD2_DESC].DataType = typeof(System.String);
                c1Total.Cols[COL_MOD2_BTN].DataType = typeof(System.String);
                c1Total.Cols[COL_MOD3_CODE].DataType = typeof(System.String);
                c1Total.Cols[COL_MOD3_DESC].DataType = typeof(System.String);
                c1Total.Cols[COL_MOD3_BTN].DataType = typeof(System.String);
                c1Total.Cols[COL_MOD4_CODE].DataType = typeof(System.String);
                c1Total.Cols[COL_MOD4_DESC].DataType = typeof(System.String);
                c1Total.Cols[COL_MOD4_BTN].DataType = typeof(System.String);


                c1Total.Cols[COL_CHARGES].DataType = typeof(System.Decimal);
                c1Total.Cols[COL_UNIT].DataType = typeof(System.Decimal);
                c1Total.Cols[COL_TOTAL].DataType = typeof(System.Decimal);
                c1Total.Cols[COL_ALLOWED].DataType = typeof(System.Decimal);
                c1Total.Cols[COL_PROVIDER_ID].DataType = typeof(System.Int64);
                c1Total.Cols[COL_PROVIDER].DataType = typeof(System.String);
                c1Total.Cols[COL_PROVIDER_BTN].DataType = typeof(System.String);
                c1Total.Cols[COL_NOTE_BTN].DataType = typeof(System.String);
                c1Total.Cols[COL_NOTE_DATA].DataType = typeof(System.Object);
                c1Total.Cols[COL_NOTE_TYPE].DataType = typeof(System.Int32);

                c1Total.Cols[COL_INSURANCEID].DataType = typeof(System.Int64);
                c1Total.Cols[COL_INSURANCENAME].DataType = typeof(System.String);

                c1Total.Cols[COL_INSSELF_PAYMODE].DataType = typeof(System.Int32);

                #endregion

                #region "Button Design"
                c1Total.Cols[COL_POS_BTN].ComboList = "...";
                c1Total.Cols[COL_TOS_BTN].ComboList = "...";
                c1Total.Cols[COL_CPT_BTN].ComboList = "...";
                c1Total.Cols[COL_DX1_BTN].ComboList = "...";
                c1Total.Cols[COL_DX2_BTN].ComboList = "...";
                c1Total.Cols[COL_DX3_BTN].ComboList = "...";
                c1Total.Cols[COL_DX4_BTN].ComboList = "...";
                c1Total.Cols[COL_MOD1_BTN].ComboList = "...";
                c1Total.Cols[COL_MOD2_BTN].ComboList = "...";
                c1Total.Cols[COL_PROVIDER_BTN].ComboList = "...";
                c1Total.Cols[COL_NOTE_BTN].ComboList = "...";
                #endregion

                #region "Button Design"
                c1Total.Cols[COL_POS_BTN].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                c1Total.Cols[COL_TOS_BTN].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                c1Total.Cols[COL_CPT_BTN].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;

                c1Total.Cols[COL_DX1_BTN].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                c1Total.Cols[COL_DX2_BTN].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                c1Total.Cols[COL_DX3_BTN].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                c1Total.Cols[COL_DX4_BTN].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                c1Total.Cols[COL_DX5_BTN].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                c1Total.Cols[COL_DX6_BTN].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                c1Total.Cols[COL_DX7_BTN].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                c1Total.Cols[COL_DX8_BTN].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;

                c1Total.Cols[COL_MOD1_BTN].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                c1Total.Cols[COL_MOD2_BTN].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                c1Total.Cols[COL_MOD3_BTN].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                c1Total.Cols[COL_MOD4_BTN].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;

                c1Total.Cols[COL_PROVIDER_BTN].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                c1Total.Cols[COL_NOTE_BTN].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                #endregion

                #region "Width"

                int nWidth = pnlMain.Width;

                //c1Total.Cols[COL_NO].Width = 20;
                c1Total.Cols[COL_NO].Width = 37;//Convert.ToInt32(nWidth * 0.02);
                c1Total.Cols[COL_TRANSACTIONID].Width = 0;

                c1Total.Cols[COL_DATEFROM].Width = 84;//Convert.ToInt32(nWidth * 0.08);


                if (_showTilldateColumn == true)
                {
                    c1Total.Cols[COL_DATETO].Width = 84;
                }
                else
                {
                    c1Total.Cols[COL_DATETO].Width = 0;
                }

                if (_showPOSColumn == true)
                {
                    c1Total.Cols[COL_POSCODE].Width = 50;//Convert.ToInt32(nWidth * 0.05);
                }
                else
                {
                    c1Total.Cols[COL_POSCODE].Width = 0;
                }

                if (_showEMGColumn == true)
                {
                    c1Total.Cols[COL_ISEMG].Width = 38;//Convert.ToInt32(nWidth * 0.05);
                }
                else
                {
                    c1Total.Cols[COL_ISEMG].Width = 0;
                }

                c1Total.Cols[COL_POSDESC].Width = 0;
                c1Total.Cols[COL_POS_BTN].Width = 0;

                c1Total.Cols[COL_TOSCODE].Width = 61;//Convert.ToInt32(nWidth * 0.05);
                c1Total.Cols[COL_TOSDESC].Width = 0;
                c1Total.Cols[COL_TOS_BTN].Width = 0;

                c1Total.Cols[COL_CPT_CODE].Width = 80;//Convert.ToInt32(nWidth * 0.05);
                c1Total.Cols[COL_CPT_DESC].Width = 0;
                c1Total.Cols[COL_CPT_BTN].Width = 0;

                c1Total.Cols[COL_DX1_CODE].Width = 70;//Convert.ToInt32(nWidth * 0.06);
                c1Total.Cols[COL_DX1_DESC].Width = 0;
                c1Total.Cols[COL_DX1_BTN].Width = 0;
                c1Total.Cols[COL_DX2_CODE].Width = 70;//Convert.ToInt32(nWidth * 0.06);
                c1Total.Cols[COL_DX2_DESC].Width = 0;
                c1Total.Cols[COL_DX2_BTN].Width = 0;
                c1Total.Cols[COL_DX3_CODE].Width = 70;//Convert.ToInt32(nWidth * 0.06);
                c1Total.Cols[COL_DX3_DESC].Width = 0;
                c1Total.Cols[COL_DX3_BTN].Width = 0;
                c1Total.Cols[COL_DX4_CODE].Width = 70;//Convert.ToInt32(nWidth * 0.06);
                c1Total.Cols[COL_DX4_DESC].Width = 0;
                c1Total.Cols[COL_DX4_BTN].Width = 0;
                c1Total.Cols[COL_DX5_CODE].Width = 60;
                c1Total.Cols[COL_DX5_DESC].Width = 0;
                c1Total.Cols[COL_DX5_BTN].Width = 0;
                c1Total.Cols[COL_DX6_CODE].Width = 60;
                c1Total.Cols[COL_DX6_DESC].Width = 0;
                c1Total.Cols[COL_DX6_BTN].Width = 0;
                c1Total.Cols[COL_DX7_CODE].Width = 60;
                c1Total.Cols[COL_DX7_DESC].Width = 0;
                c1Total.Cols[COL_DX7_BTN].Width = 0;
                c1Total.Cols[COL_DX8_CODE].Width = 60;
                c1Total.Cols[COL_DX8_DESC].Width = 0;
                c1Total.Cols[COL_DX8_BTN].Width = 0;

                c1Total.Cols[COL_DX1_PTR].Width = 21;//Convert.ToInt32(nWidth * 0.03);
                c1Total.Cols[COL_DX2_PTR].Width = 21;//Convert.ToInt32(nWidth * 0.03);
                c1Total.Cols[COL_DX3_PTR].Width = 21;//Convert.ToInt32(nWidth * 0.03);
                c1Total.Cols[COL_DX4_PTR].Width = 21;//Convert.ToInt32(nWidth * 0.03);
                c1Total.Cols[COL_DX5_PTR].Width = 21;
                c1Total.Cols[COL_DX6_PTR].Width = 21;
                c1Total.Cols[COL_DX7_PTR].Width = 21;
                c1Total.Cols[COL_DX8_PTR].Width = 21;

                c1Total.Cols[COL_MOD1_CODE].Width = 40;//Convert.ToInt32(nWidth * 0.05);
                c1Total.Cols[COL_MOD1_DESC].Width = 0;
                c1Total.Cols[COL_MOD1_BTN].Width = 0;
                c1Total.Cols[COL_MOD2_CODE].Width = 40;//Convert.ToInt32(nWidth * 0.05);
                c1Total.Cols[COL_MOD2_DESC].Width = 0;
                c1Total.Cols[COL_MOD2_BTN].Width = 0;
                c1Total.Cols[COL_MOD3_CODE].Width = 40;
                c1Total.Cols[COL_MOD3_DESC].Width = 0;
                c1Total.Cols[COL_MOD3_BTN].Width = 0;
                c1Total.Cols[COL_MOD4_CODE].Width = 40;
                c1Total.Cols[COL_MOD4_DESC].Width = 0;
                c1Total.Cols[COL_MOD4_BTN].Width = 0;

                c1Total.Cols[COL_CHARGES].Width = 130;//Convert.ToInt32(nWidth * 0.06);
                c1Total.Cols[COL_UNIT].Width = 60;//Convert.ToInt32(nWidth * 0.03);
                c1Total.Cols[COL_TOTAL].Width = 130;// Convert.ToInt32(nWidth * 0.08);
                if (_showAllowedColumn == true)
                {
                    c1Total.Cols[COL_ALLOWED].Width = 120;//Convert.ToInt32(nWidth * 0.06);
                }
                else
                {
                    c1Total.Cols[COL_ALLOWED].Width = 0;//Convert.ToInt32(nWidth * 0.06);
                }

                c1Total.Cols[COL_PROVIDER_ID].Width = 0;
                c1Total.Cols[COL_PROVIDER].Width = 210;//Convert.ToInt32(nWidth * 0.20);               
                c1Total.Cols[COL_PROVIDER_BTN].Width = 0;
                c1Total.Cols[COL_NOTE_BTN].Width = 0;
                c1Total.Cols[COL_NOTE_DATA].Width = 0;
                c1Total.Cols[COL_NOTE_TYPE].Width = 0;

                c1Total.Cols[COL_INSURANCEID].Width = 0;
                c1Total.Cols[COL_INSURANCENAME].Width = 150;

                c1Total.Cols[COL_INSSELF_PAYMODE].Width = 0;

                c1Total.Cols[COL_HOLD].Width = 40;
                c1Total.Cols[COL_HOLD_REASON].Width = 120;
                c1Total.Cols[COL_ISLABCPT].Width = 28;
                c1Total.Cols[COL_AUTHORIZATIONNO].Width = 100;

                if (_showSplitClaimToPatient == true)
                {
                    c1Total.Cols[COL_SELFCLAIM].Width = 32;
                }
                #endregion

                #region "Show/Hide"
                c1Total.Cols[COL_NO].Visible = true;
                c1Total.Cols[COL_TRANSACTIONID].Visible = false;

                c1Total.Cols[COL_DATEFROM].Visible = true;


                if (_showTilldateColumn == true)
                {
                    c1Total.Cols[COL_DATETO].Visible = true;
                }
                else
                {
                    c1Total.Cols[COL_DATETO].Visible = false;
                }

                c1Total.Cols[COL_POSCODE].Visible = true;
                c1Total.Cols[COL_ISEMG].Visible = true;
                c1Total.Cols[COL_POSDESC].Visible = false;
                //c1Total.Cols[COL_POS_BTN].Visible = true;
                c1Total.Cols[COL_POS_BTN].Visible = false;

                c1Total.Cols[COL_TOSCODE].Visible = false;
                c1Total.Cols[COL_TOSDESC].Visible = false;
                //c1Total.Cols[COL_TOS_BTN].Visible = true;
                c1Total.Cols[COL_TOS_BTN].Visible = false;

                c1Total.Cols[COL_CPT_CODE].Visible = true;
                c1Total.Cols[COL_CPT_DESC].Visible = false;
                //c1Total.Cols[COL_CPT_BTN].Visible = true;
                c1Total.Cols[COL_CPT_BTN].Visible = false;

                //Make all Diagnosis Columns false
                c1Total.Cols[COL_DX1_CODE].Visible = false;
                c1Total.Cols[COL_DX1_DESC].Visible = false;
                //c1Total.Cols[COL_DX1_BTN].Visible = true;
                c1Total.Cols[COL_DX1_BTN].Visible = false;
                c1Total.Cols[COL_DX2_CODE].Visible = false;
                c1Total.Cols[COL_DX2_DESC].Visible = false;
                //c1Total.Cols[COL_DX2_BTN].Visible = true;
                c1Total.Cols[COL_DX2_BTN].Visible = false;
                c1Total.Cols[COL_DX3_CODE].Visible = false;
                c1Total.Cols[COL_DX3_DESC].Visible = false;
                //c1Total.Cols[COL_DX3_BTN].Visible = true;
                c1Total.Cols[COL_DX3_BTN].Visible = false;
                c1Total.Cols[COL_DX4_CODE].Visible = false;
                c1Total.Cols[COL_DX4_DESC].Visible = false;
                //c1Total.Cols[COL_DX4_BTN].Visible = true;
                c1Total.Cols[COL_DX4_BTN].Visible = false;
                c1Total.Cols[COL_DX5_CODE].Visible = false; //
                c1Total.Cols[COL_DX5_DESC].Visible = false;
                c1Total.Cols[COL_DX5_BTN].Visible = false;
                c1Total.Cols[COL_DX6_CODE].Visible = false; //
                c1Total.Cols[COL_DX6_DESC].Visible = false;
                c1Total.Cols[COL_DX6_BTN].Visible = false;
                c1Total.Cols[COL_DX7_CODE].Visible = false; //
                c1Total.Cols[COL_DX7_DESC].Visible = false;
                c1Total.Cols[COL_DX7_BTN].Visible = false;
                c1Total.Cols[COL_DX8_CODE].Visible = false; //
                c1Total.Cols[COL_DX8_DESC].Visible = false;
                c1Total.Cols[COL_DX8_BTN].Visible = false;

                int _DiagnosisPointer = 0;
                for (int i = COL_DX1_CODE; _DiagnosisPointer < 4 && i <= COL_DX8_CODE; i += 3)
                {
                    c1Total.Cols[i].Visible = true;
                    _DiagnosisPointer = _DiagnosisPointer + 1;
                }


                c1Total.Cols[COL_DX1_PTR].Visible = false;
                c1Total.Cols[COL_DX2_PTR].Visible = false;
                c1Total.Cols[COL_DX3_PTR].Visible = false;
                c1Total.Cols[COL_DX4_PTR].Visible = false;
                c1Total.Cols[COL_DX5_PTR].Visible = false;
                c1Total.Cols[COL_DX6_PTR].Visible = false;
                c1Total.Cols[COL_DX7_PTR].Visible = false;
                c1Total.Cols[COL_DX8_PTR].Visible = false;

                int _DiagnosisPointerCounter = 0;
                for (int i = COL_DX1_PTR; _DiagnosisPointerCounter < 4 && i <= COL_DX8_PTR; i++)
                {
                    c1Total.Cols[i].Visible = true;
                    _DiagnosisPointerCounter = _DiagnosisPointerCounter + 1;
                }



                c1Total.Cols[COL_MOD1_CODE].Visible = false;
                c1Total.Cols[COL_MOD1_DESC].Visible = false;
                c1Total.Cols[COL_MOD1_BTN].Visible = false;
                c1Total.Cols[COL_MOD2_CODE].Visible = false;
                c1Total.Cols[COL_MOD2_DESC].Visible = false;
                c1Total.Cols[COL_MOD2_BTN].Visible = false;
                c1Total.Cols[COL_MOD3_CODE].Visible = false;
                c1Total.Cols[COL_MOD3_DESC].Visible = false;
                c1Total.Cols[COL_MOD3_BTN].Visible = false;
                c1Total.Cols[COL_MOD4_CODE].Visible = false;
                c1Total.Cols[COL_MOD4_DESC].Visible = false;
                c1Total.Cols[COL_MOD4_BTN].Visible = false;

                int _ModifiersCounter = 0;
                for (int i = COL_MOD1_CODE; _ModifiersCounter < _NoOfModifiers && i <= COL_MOD4_CODE; i += 3)
                {
                    c1Total.Cols[i].Visible = true;
                    _ModifiersCounter = _ModifiersCounter + 1;
                }


                c1Total.Cols[COL_CHARGES].Visible = true;
                c1Total.Cols[COL_UNIT].Visible = true;
                c1Total.Cols[COL_TOTAL].Visible = true;
                if (_showAllowedColumn == true)
                {
                    c1Total.Cols[COL_ALLOWED].Visible = true;
                }
                else
                {
                    c1Total.Cols[COL_ALLOWED].Visible = false;
                }
                c1Total.Cols[COL_ISLABCPT].Visible = ShowLabColumn;
                c1Total.Cols[COL_AUTHORIZATIONNO].Visible = ShowLabColumn;

                c1Total.Cols[COL_PROVIDER_ID].Visible = false;
                c1Total.Cols[COL_PROVIDER].Visible = true;
                //c1Total.Cols[COL_PROVIDER_BTN].Visible = true;
                c1Total.Cols[COL_PROVIDER_BTN].Visible = false;
                c1Total.Cols[COL_NOTE_BTN].Visible = false;
                c1Total.Cols[COL_NOTE_DATA].Visible = false;
                c1Total.Cols[COL_NOTE_TYPE].Visible = false;

                c1Total.Cols[COL_INSURANCEID].Visible = false;
                if (_showInsurance == true)
                { c1Total.Cols[COL_INSURANCENAME].Visible = false; }
                else
                { c1Total.Cols[COL_INSURANCENAME].Visible = true; }

                c1Total.Cols[COL_INSSELF_PAYMODE].Visible = false;

                #endregion

                #region "Header"
                c1Total.SetData(0, COL_NO, "");
                c1Total.SetData(0, COL_TRANSACTIONID, "");

                c1Total.SetData(0, COL_DATEFROM, "");
                c1Total.SetData(0, COL_DATETO, "");

                c1Total.SetData(0, COL_POSCODE, "");
                c1Total.SetData(0, COL_ISEMG, "");
                c1Total.SetData(0, COL_POSDESC, "");
                c1Total.SetData(0, COL_POS_BTN, "");

                c1Total.SetData(0, COL_TOSCODE, "");
                c1Total.SetData(0, COL_TOSDESC, "");
                c1Total.SetData(0, COL_TOS_BTN, "");

                c1Total.SetData(0, COL_CPT_CODE, "");
                c1Total.SetData(0, COL_CPT_DESC, "");
                c1Total.SetData(0, COL_CPT_BTN, "");

                c1Total.SetData(0, COL_DX1_CODE, "");
                c1Total.SetData(0, COL_DX1_DESC, "");
                c1Total.SetData(0, COL_DX1_BTN, "");
                c1Total.SetData(0, COL_DX2_CODE, "");
                c1Total.SetData(0, COL_DX2_DESC, "");
                c1Total.SetData(0, COL_DX2_BTN, "");
                c1Total.SetData(0, COL_DX3_CODE, "");
                c1Total.SetData(0, COL_DX3_DESC, "");
                c1Total.SetData(0, COL_DX3_BTN, "");
                c1Total.SetData(0, COL_DX4_CODE, "");
                c1Total.SetData(0, COL_DX4_DESC, "");
                c1Total.SetData(0, COL_DX4_BTN, "");
                c1Total.SetData(0, COL_DX5_CODE, "");
                c1Total.SetData(0, COL_DX5_DESC, "");
                c1Total.SetData(0, COL_DX5_BTN, "");
                c1Total.SetData(0, COL_DX6_CODE, "");
                c1Total.SetData(0, COL_DX6_DESC, "");
                c1Total.SetData(0, COL_DX6_BTN, "");
                c1Total.SetData(0, COL_DX7_CODE, "");
                c1Total.SetData(0, COL_DX7_DESC, "");
                c1Total.SetData(0, COL_DX7_BTN, "");
                c1Total.SetData(0, COL_DX8_CODE, "");
                c1Total.SetData(0, COL_DX8_DESC, "");
                c1Total.SetData(0, COL_DX8_BTN, "");


                c1Total.SetData(0, COL_DX1_PTR, "");
                c1Total.SetData(0, COL_DX2_PTR, "");
                c1Total.SetData(0, COL_DX3_PTR, "");
                c1Total.SetData(0, COL_DX4_PTR, "");
                c1Total.SetData(0, COL_DX5_PTR, "");
                c1Total.SetData(0, COL_DX6_PTR, "");
                c1Total.SetData(0, COL_DX7_PTR, "");
                c1Total.SetData(0, COL_DX8_PTR, "");


                c1Total.SetData(0, COL_MOD1_CODE, "");
                c1Total.SetData(0, COL_MOD1_DESC, "");
                c1Total.SetData(0, COL_MOD1_BTN, "");
                c1Total.SetData(0, COL_MOD2_CODE, "");
                c1Total.SetData(0, COL_MOD2_DESC, "");
                c1Total.SetData(0, COL_MOD2_BTN, "");
                c1Total.SetData(0, COL_MOD3_CODE, "");
                c1Total.SetData(0, COL_MOD3_DESC, "");
                c1Total.SetData(0, COL_MOD3_BTN, "");
                c1Total.SetData(0, COL_MOD4_CODE, "");
                c1Total.SetData(0, COL_MOD4_DESC, "");
                c1Total.SetData(0, COL_MOD4_BTN, "");



                c1Total.SetData(0, COL_CHARGES, "0");
                c1Total.SetData(0, COL_UNIT, "");
                c1Total.SetData(0, COL_TOTAL, "0");
                c1Total.SetData(0, COL_ALLOWED, "0");
                c1Total.SetData(0, COL_PROVIDER_ID, "");
                c1Total.SetData(0, COL_PROVIDER, "");
                c1Total.SetData(0, COL_PROVIDER_BTN, "");
                c1Total.SetData(0, COL_NOTE_BTN, "");
                c1Total.SetData(0, COL_NOTE_DATA, "");
                c1Total.SetData(0, COL_NOTE_TYPE, "");

                c1Total.SetData(0, COL_INSURANCEID, "");
                c1Total.SetData(0, COL_INSURANCENAME, "");

                c1Total.SetData(0, COL_INSSELF_PAYMODE, "");

                #endregion

                #region "Styles"

                CellStyle csCurrency;// = c1Transaction.Styles.Add("csCurrencyCell");
                try
                {
                    if (c1Transaction.Styles.Contains("csCurrencyCell"))
                    {
                        csCurrency = c1Transaction.Styles["csCurrencyCell"];
                    }
                    else
                    {
                        csCurrency = c1Transaction.Styles.Add("csCurrencyCell");
                        csCurrency.DataType = typeof(System.Decimal);
                        csCurrency.Format = "c";
                        csCurrency.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                    }

                }
                catch
                {
                    csCurrency = c1Transaction.Styles.Add("csCurrencyCell");
                    csCurrency.DataType = typeof(System.Decimal);
                    csCurrency.Format = "c";
                    csCurrency.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));


                }


                #endregion "Styles"

                CellRange rg = c1Total.GetCellRange(0, COL_CHARGES, 0, COL_ALLOWED);
                rg.Style = csCurrency;

                //c1Total.Cols[COL_CHARGES].Style = csCurrency;
                //c1Total.Cols[COL_TOTAL].Style = csCurrency;
                //c1Total.Cols[COL_ALLOWED].Style = csCurrency;
                c1Total.AllowEditing = false;

                c1Total.SetData(0, COL_MOD2_CODE, "Total : ");
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {
                // c1Total.ScrollBars = ScrollBars.Both;
            }

        }

        #endregion " Design & ReInitialize Grid "

        #region " Get Set Transaction Lines Methods "

        public TransactionLine GetLineTransaction(int LineTransactionNumber)
        {
            //       bool _returnResult = false;
            TransactionLine LineTransactionData = null;
            int rowIndex = LineTransactionNumber;
            try
            {
                LineTransactionData = new TransactionLine();

                LineTransactionData.TransactionLineId = Convert.ToInt64(c1Transaction.GetData(rowIndex, COL_NO).ToString());
                LineTransactionData.EMRTreatmentLineNo = Convert.ToInt64(c1Transaction.GetData(rowIndex, COL_EMRTREATMENTLINENO).ToString());
                LineTransactionData.TransactionId = Convert.ToInt64(c1Transaction.GetData(rowIndex, COL_TRANSACTIONID).ToString());
                LineTransactionData.TransactionDetailID = Convert.ToInt64(c1Transaction.GetData(rowIndex, COL_TRANSACTION_DETAIL_ID).ToString());

                LineTransactionData.DateServiceFrom = Convert.ToDateTime(c1Transaction.GetData(rowIndex, COL_DATEFROM).ToString());

                //LineTransactionData.DateServiceTill = Convert.ToDateTime(c1Transaction.GetData(rowIndex, COL_DATETO).ToString());
                //new implementation for to date 
                if (_showTilldateColumn == true)
                {
                    if (_showTillDateColumnUseNullDate == true)
                    {
                        if (c1Transaction.GetData(rowIndex, COL_DATETO) != null)
                        {
                            LineTransactionData.DateServiceTill = Convert.ToDateTime(c1Transaction.GetData(rowIndex, COL_DATETO).ToString());
                        }
                        else
                        {
                            //to date will return null value that's why for handle error we are setting from date value 
                            //otherwise there is no logic
                            LineTransactionData.DateServiceTill = Convert.ToDateTime(c1Transaction.GetData(rowIndex, COL_DATEFROM).ToString());
                            LineTransactionData.DateServiceTillIsNull = true;
                        }
                    }
                    else
                    {
                        if (c1Transaction.GetData(rowIndex, COL_DATETO) != null && Convert.ToString(c1Transaction.GetData(rowIndex, COL_DATETO)).Trim() != "")
                        {
                            LineTransactionData.DateServiceTill = Convert.ToDateTime(c1Transaction.GetData(rowIndex, COL_DATETO).ToString());
                        }
                        else
                        {
                            LineTransactionData.DateServiceTill = Convert.ToDateTime(c1Transaction.GetData(rowIndex, COL_DATEFROM).ToString());
                        }
                    }
                }
                else
                {
                    LineTransactionData.DateServiceTillIsNull = true;
                    //DOS To will be null that is why we have to send from date as DOS To also
                    LineTransactionData.DateServiceTill = Convert.ToDateTime(c1Transaction.GetData(rowIndex, COL_DATEFROM).ToString());
                }


                LineTransactionData.POSCode = c1Transaction.GetData(rowIndex, COL_POSCODE).ToString();
                LineTransactionData.POSDescription = c1Transaction.GetData(rowIndex, COL_POSDESC).ToString();

                LineTransactionData.TOSCode = c1Transaction.GetData(rowIndex, COL_TOSCODE).ToString();
                LineTransactionData.TOSDescription = c1Transaction.GetData(rowIndex, COL_TOSDESC).ToString();

                LineTransactionData.CPTCode = c1Transaction.GetData(rowIndex, COL_CPT_CODE).ToString();
                LineTransactionData.CPTDescription = c1Transaction.GetData(rowIndex, COL_CPT_DESC).ToString();

                //LineTransactionData.Dx1Code = c1Transaction.GetData(rowIndex, COL_DX1_CODE).ToString();
                //LineTransactionData.Dx1Description = c1Transaction.GetData(rowIndex, COL_DX1_DESC).ToString();
                //LineTransactionData.Dx2Code = c1Transaction.GetData(rowIndex, COL_DX2_CODE).ToString();
                //LineTransactionData.Dx2Description = c1Transaction.GetData(rowIndex, COL_DX2_DESC).ToString();
                //LineTransactionData.Dx3Code = c1Transaction.GetData(rowIndex, COL_DX3_CODE).ToString();
                //LineTransactionData.Dx3Description = c1Transaction.GetData(rowIndex, COL_DX3_DESC).ToString();
                //LineTransactionData.Dx4Code = c1Transaction.GetData(rowIndex, COL_DX4_CODE).ToString();
                //LineTransactionData.Dx4Description = c1Transaction.GetData(rowIndex, COL_DX4_DESC).ToString();
                //LineTransactionData.Dx5Code = c1Transaction.GetData(rowIndex, COL_DX5_CODE).ToString();
                //LineTransactionData.Dx5Description = c1Transaction.GetData(rowIndex, COL_DX5_DESC).ToString();
                //LineTransactionData.Dx6Code = c1Transaction.GetData(rowIndex, COL_DX6_CODE).ToString();
                //LineTransactionData.Dx6Description = c1Transaction.GetData(rowIndex, COL_DX6_DESC).ToString();
                //LineTransactionData.Dx7Code = c1Transaction.GetData(rowIndex, COL_DX7_CODE).ToString();
                //LineTransactionData.Dx7Description = c1Transaction.GetData(rowIndex, COL_DX7_DESC).ToString();
                //LineTransactionData.Dx8Code = c1Transaction.GetData(rowIndex, COL_DX8_CODE).ToString();
                //LineTransactionData.Dx8Description = c1Transaction.GetData(rowIndex, COL_DX8_DESC).ToString();

                if (c1Transaction.GetCellCheck(rowIndex, COL_DX1_PTR) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                {
                    LineTransactionData.Dx1Ptr = true;
                    LineTransactionData.Dx1Code = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX1_CODE));
                    LineTransactionData.Dx1Description = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX1_DESC));
                }
                else
                {
                    LineTransactionData.Dx1Ptr = false;
                    LineTransactionData.Dx1Code = "";
                    LineTransactionData.Dx1Description = "";
                }

                if (c1Transaction.GetCellCheck(rowIndex, COL_DX2_PTR) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                {
                    LineTransactionData.Dx2Ptr = true;
                    LineTransactionData.Dx2Code = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX2_CODE));
                    LineTransactionData.Dx2Description = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX2_DESC));
                }
                else
                {
                    LineTransactionData.Dx2Ptr = false;
                    LineTransactionData.Dx2Code = "";
                    LineTransactionData.Dx2Description = "";
                }

                if (c1Transaction.GetCellCheck(rowIndex, COL_DX3_PTR) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                {
                    LineTransactionData.Dx3Ptr = true;
                    LineTransactionData.Dx3Code = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX3_CODE));
                    LineTransactionData.Dx3Description = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX3_DESC));
                }
                else
                {
                    LineTransactionData.Dx3Ptr = false;
                    LineTransactionData.Dx3Code = "";
                    LineTransactionData.Dx3Description = "";
                }

                if (c1Transaction.GetCellCheck(rowIndex, COL_DX4_PTR) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                {
                    LineTransactionData.Dx4Ptr = true;
                    LineTransactionData.Dx4Code = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX4_CODE));
                    LineTransactionData.Dx4Description = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX4_DESC));
                }
                else
                {
                    LineTransactionData.Dx4Ptr = false;
                    LineTransactionData.Dx4Code = "";
                    LineTransactionData.Dx4Description = "";
                }
                if (c1Transaction.GetCellCheck(rowIndex, COL_DX5_PTR) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                {
                    LineTransactionData.Dx5Ptr = true;
                    LineTransactionData.Dx5Code = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX5_CODE));
                    LineTransactionData.Dx5Description = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX5_DESC));
                }
                else
                {
                    LineTransactionData.Dx5Ptr = false;
                    LineTransactionData.Dx5Code = "";
                    LineTransactionData.Dx5Description = "";
                }

                if (c1Transaction.GetCellCheck(rowIndex, COL_DX6_PTR) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                {
                    LineTransactionData.Dx6Ptr = true;
                    LineTransactionData.Dx6Code = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX6_CODE));
                    LineTransactionData.Dx6Description = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX6_DESC));
                }
                else
                {
                    LineTransactionData.Dx6Ptr = false;
                    LineTransactionData.Dx6Code = "";
                    LineTransactionData.Dx6Description = "";
                }

                if (c1Transaction.GetCellCheck(rowIndex, COL_DX7_PTR) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                {
                    LineTransactionData.Dx7Ptr = true;
                    LineTransactionData.Dx7Code = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX7_CODE));
                    LineTransactionData.Dx7Description = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX7_DESC));
                }
                else
                {
                    LineTransactionData.Dx7Ptr = false;
                    LineTransactionData.Dx7Code = "";
                    LineTransactionData.Dx7Description = "";
                }

                if (c1Transaction.GetCellCheck(rowIndex, COL_DX8_PTR) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                {
                    LineTransactionData.Dx8Ptr = true;
                    LineTransactionData.Dx8Code = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX8_CODE));
                    LineTransactionData.Dx8Description = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX8_DESC));
                }
                else
                {
                    LineTransactionData.Dx8Ptr = false;
                    LineTransactionData.Dx8Code = "";
                    LineTransactionData.Dx8Description = "";
                }

                if (c1Transaction.GetCellCheck(rowIndex, COL_ISEMG) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                {
                    LineTransactionData.EMG = true;
                }
                else
                {
                    LineTransactionData.EMG = false;
                }

                LineTransactionData.Mod1Code = Convert.ToString(c1Transaction.GetData(rowIndex, COL_MOD1_CODE));
                LineTransactionData.Mod1Description = Convert.ToString(c1Transaction.GetData(rowIndex, COL_MOD1_DESC));
                LineTransactionData.Mod2Code = Convert.ToString(c1Transaction.GetData(rowIndex, COL_MOD2_CODE));
                LineTransactionData.Mod2Description = Convert.ToString(c1Transaction.GetData(rowIndex, COL_MOD2_DESC));
                LineTransactionData.Mod3Code = Convert.ToString(c1Transaction.GetData(rowIndex, COL_MOD3_CODE));
                LineTransactionData.Mod3Description = Convert.ToString(c1Transaction.GetData(rowIndex, COL_MOD3_DESC));
                LineTransactionData.Mod4Code = Convert.ToString(c1Transaction.GetData(rowIndex, COL_MOD4_CODE));
                LineTransactionData.Mod4Description = Convert.ToString(c1Transaction.GetData(rowIndex, COL_MOD4_DESC));

                LineTransactionData.Charges = Convert.ToDecimal(c1Transaction.GetData(rowIndex, COL_CHARGES).ToString());
                LineTransactionData.Unit = Convert.ToDecimal(c1Transaction.GetData(rowIndex, COL_UNIT).ToString());
                LineTransactionData.Total = Convert.ToDecimal(c1Transaction.GetData(rowIndex, COL_TOTAL).ToString());
                //LineTransactionData.AllowedCharges = Convert.ToDecimal(c1Transaction.GetData(rowIndex, COL_ALLOWED).ToString());

                if (_showAllowedColumn == true)
                {
                    LineTransactionData.AllowedCharges = Convert.ToDecimal(c1Transaction.GetData(rowIndex, COL_ALLOWED));
                }
                else
                {
                    if (Convert.ToDecimal(c1Transaction.GetData(rowIndex, COL_CHARGES)) > 0)
                    {
                        LineTransactionData.AllowedCharges = Convert.ToDecimal(c1Transaction.GetData(rowIndex, COL_TOTAL));
                    }
                }

                LineTransactionData.RefferingProvider = Convert.ToString(c1Transaction.GetData(rowIndex, COL_PROVIDER));
                LineTransactionData.ClinicID = _ClinicID;
                //20080918 Anil
                LineTransactionData.InsuranceID = Convert.ToInt64(c1Transaction.GetData(rowIndex, COL_INSURANCEID));
                LineTransactionData.InsuranceName = Convert.ToString(c1Transaction.GetData(rowIndex, COL_INSURANCENAME));
                if (Convert.ToString(c1Transaction.GetData(rowIndex, COL_INSSELF_PAYMODE)) != "")
                {
                    LineTransactionData.InsuranceSelfMode = (PayerMode)Convert.ToInt32(c1Transaction.GetData(rowIndex, COL_INSSELF_PAYMODE).ToString());
                    LineTransactionData.InsuranceName = PayerMode.Self.ToString();
                }
                else
                {
                    LineTransactionData.InsuranceSelfMode = PayerMode.None;
                }

                #region " Get Notes "
                GeneralNotes oNotes = new GeneralNotes();
                if (c1Transaction.GetData(rowIndex, COL_NOTE_DATA) != null)
                {
                    oNotes = (GeneralNotes)(c1Transaction.GetData(rowIndex, COL_NOTE_DATA));
                    LineTransactionData.LineNotes = oNotes;
                }
                if (oNotes != null) { oNotes.Dispose(); }

                #endregion " Get Notes "

                ////Functionality need to be implemented for notes
                //GeneralNotes oNotes = new GeneralNotes();
                //GeneralNote oNote = new GeneralNote();
                //oNote.NoteID = 1;//Temp Hard Coded
                //oNote.NoteDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString());
                //oNote.NoteDescription = Convert.ToString(c1Transaction.GetData(rowIndex, COL_NOTE_DATA));
                //oNote.NoteType = NoteType.GeneralNote;
                //oNote.TransactionID = Convert.ToInt64(c1Transaction.GetData(rowIndex, COL_TRANSACTIONID).ToString());
                //oNote.TransactionLineId = Convert.ToInt64(c1Transaction.GetData(rowIndex, COL_NO).ToString());
                //oNote.ClinicID = _ClinicID; //temp Hard Code
                //oNote.UserID = 1;//temp Hard Code
                //oNotes.Add(oNote);
                //LineTransactionData.LineNotes = oNotes; 
                ////

                LineTransactionData.LineStatus = TransactionStatus.Transacted;//**Vinayak

                //Code added on 20090511 by - Sagar Ghodke
                //Code added to implement CLIA number functionality & senttoclaim for transaction lines
                if (c1Transaction.GetCellCheck(rowIndex, COL_ISLABCPT) == CheckEnum.Checked)
                { LineTransactionData.IsLabCPT = true; }
                if (c1Transaction.GetData(rowIndex, COL_AUTHORIZATIONNO) != null && Convert.ToString(c1Transaction.GetData(rowIndex, COL_AUTHORIZATIONNO)).Trim() != "")
                { LineTransactionData.AuthorizationNo = Convert.ToString(c1Transaction.GetData(rowIndex, COL_AUTHORIZATIONNO)).Trim(); }
                if (c1Transaction.GetCellCheck(rowIndex, COL_SENTTOCLAIM) == CheckEnum.Checked)
                { LineTransactionData.SendToClaim = true; }
                //End code add 20090511,Sagar Ghodke 

                if (c1Transaction.GetData(rowIndex, COL_LINEPRIMARY_DXCODE) != null && Convert.ToString(c1Transaction.GetData(rowIndex, COL_LINEPRIMARY_DXCODE)).Trim() != "")
                {
                    LineTransactionData.LinePrimaryDxCode = Convert.ToString(c1Transaction.GetData(rowIndex, COL_LINEPRIMARY_DXCODE)).Trim();
                    LineTransactionData.LinePrimaryDxDesc = Convert.ToString(c1Transaction.GetData(rowIndex, COL_LINEPRIMARY_DXDESC)).Trim();
                }

                if (c1Transaction.GetCellCheck(rowIndex, COL_HOLD) == CheckEnum.Checked)
                { LineTransactionData.IsHold = true; }
                else { LineTransactionData.IsHold = false; }

                LineTransactionData.HoldReason = Convert.ToString(c1Transaction.GetData(rowIndex, COL_HOLD_REASON));
                if ((c1Transaction.GetData(rowIndex, COL_NDCCODE) != null) && (c1Transaction.GetData(rowIndex, COL_NDCCODE).ToString().Trim() != ""))
                {

                    LineTransactionData.NDCCode = Convert.ToString(c1Transaction.GetData(rowIndex, COL_NDCCODE));
                    LineTransactionData.NDCUnit = Convert.ToString(c1Transaction.GetData(rowIndex, COL_NDCUNIT));
                    LineTransactionData.NDCUnitCode = Convert.ToString(c1Transaction.GetData(rowIndex, COL_NDCUNITCODE));
                    LineTransactionData.NDCUnitDescription = Convert.ToString(c1Transaction.GetData(rowIndex, COL_NDCUNITDESCRITION));
                    LineTransactionData.Prescription = Convert.ToString(c1Transaction.GetData(rowIndex, COL_PRESCRIPTION)); //Prescription number
                    LineTransactionData.PrescriptionDescription = Convert.ToString(c1Transaction.GetData(rowIndex, COL_PRESCRIPTIONDESC)); //Prescription number

                    //LineTransactionData.NDCID = Convert.ToString(c1Transaction.GetData(rowIndex, COL_HOLD_REASON));
                    //LineTransactionData.NDCUnit = Convert.ToString(c1Transaction.GetData(rowIndex, COL_HOLD_REASON));
                    //LineTransactionData.NDCUnitCode = Convert.ToString(c1Transaction.GetData(rowIndex, COL_HOLD_REASON));
                    //LineTransactionData.NDCUnitDescription= Convert.ToString(c1Transaction.GetData(rowIndex, COL_HOLD_REASON));
                    LineTransactionData.NDCUnitPricing = "0";
                    //LineTransactionData. = Convert.ToString(c1Transaction.GetData(rowIndex, COL_HOLD_REASON));
                    LineTransactionData.NDCCodeQualifier = "N4";
                }


                if ((c1Transaction.GetData(rowIndex, COL_ANES_ISANESTHESIA) != null) && (Convert.ToBoolean(c1Transaction.GetData(rowIndex, COL_ANES_ISANESTHESIA))))
                {

                    LineTransactionData.bIsAneshtesia = Convert.ToBoolean(c1Transaction.GetData(rowIndex, COL_ANES_ISANESTHESIA));
                    LineTransactionData.AnesthesiaID = Convert.ToInt64(c1Transaction.GetData(rowIndex, COL_ANES_ID));
                    LineTransactionData.AnesthesiaStartTime = Convert.ToDateTime(c1Transaction.GetData(rowIndex, COL_ANES_STARTDATE));
                    LineTransactionData.AnesthesiaEndTime = Convert.ToDateTime(c1Transaction.GetData(rowIndex, COL_ANES_ENDDATE));
                    LineTransactionData.AnesthesiaTotalMinutes = Convert.ToInt64(c1Transaction.GetData(rowIndex, COL_ANES_TOTALMIN));

                    LineTransactionData.AnesthesiaTimeUnits = Convert.ToDecimal(c1Transaction.GetData(rowIndex, COL_ANES_TIMEUNITS));
                    LineTransactionData.AnesthesiaBaseUnits = Convert.ToDecimal(c1Transaction.GetData(rowIndex, COL_ANES_BASEUNITS));
                    LineTransactionData.AnesthesiaOtherUnits = Convert.ToDecimal(c1Transaction.GetData(rowIndex, COL_ANES_OTHERUNITS));
                    LineTransactionData.AnesthesiaTotalUnits = Convert.ToDecimal(c1Transaction.GetData(rowIndex, COL_ANES_TOTALUNITS));
                    LineTransactionData.bIsAutoCalculateAnesthesia = Convert.ToBoolean(c1Transaction.GetData(rowIndex, COL_ANES_ISAUTOCALCULATE));
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {

            }
            return LineTransactionData;
        }

        public TransactionLines GetLineTransactions()
        {
            //   bool _returnResult = false;
            TransactionLines LineTransactionsData = new TransactionLines();
            TransactionLine LineTransactionData = null;
            int rowIndex = 0;
            try
            {
                if (c1Transaction.Rows.Count > 0)
                {
                    for (int i = 1; i <= c1Transaction.Rows.Count - 1; i++)
                    {
                        rowIndex = i;
                        LineTransactionData = new TransactionLine();


                        LineTransactionData.TransactionLineId = Convert.ToInt64(c1Transaction.GetData(rowIndex, COL_NO).ToString());
                        if (Convert.ToString(c1Transaction.GetData(rowIndex, COL_EMRTREATMENTLINENO)) != "")
                        {
                            LineTransactionData.EMRTreatmentLineNo = Convert.ToInt64(c1Transaction.GetData(rowIndex, COL_EMRTREATMENTLINENO).ToString());
                        }
                        else
                        {
                            LineTransactionData.EMRTreatmentLineNo = 0;
                        }

                        LineTransactionData.TransactionId = Convert.ToInt64(c1Transaction.GetData(rowIndex, COL_TRANSACTIONID));
                        LineTransactionData.TransactionDetailID = Convert.ToInt64(c1Transaction.GetData(rowIndex, COL_TRANSACTION_DETAIL_ID));

                        LineTransactionData.TransactionMasterID = Convert.ToInt64(c1Transaction.GetData(rowIndex, COL_MST_TRANSACTIONID));
                        LineTransactionData.TransactionMasterDetailID = Convert.ToInt64(c1Transaction.GetData(rowIndex, COL_MST_TRANSACTIONDTLID));

                        LineTransactionData.DateServiceFrom = Convert.ToDateTime(c1Transaction.GetData(rowIndex, COL_DATEFROM).ToString());
                        //PREVIOUS CODE - START
                        //Please refer detail comment for ECET on same line changes
                        //temp code need to set the date till
                        //LineTransactionData.DateServiceTill = Convert.ToDateTime(c1Transaction.GetData(rowIndex, COL_DATEFROM).ToString());
                        //LineTransactionData.DateServiceTill = Convert.ToDateTime(c1Transaction.GetData(rowIndex, COL_DATETO).ToString());
                        //PREVIOUS CODE - FINISH

                        //LineTransactionData.DateServiceTill = Convert.ToDateTime(c1Transaction.GetData(rowIndex, COL_DATETO).ToString());
                        //new implementation for to date 
                        if (_showTilldateColumn == true)
                        {
                            if (_showTillDateColumnUseNullDate == true)
                            {
                                if (c1Transaction.GetData(rowIndex, COL_DATETO) != null)
                                {
                                    LineTransactionData.DateServiceTill = Convert.ToDateTime(c1Transaction.GetData(rowIndex, COL_DATETO).ToString());
                                }
                                else
                                {
                                    //to date will return null value that's why for handle error we are setting from date value 
                                    //otherwise there is no logic
                                    LineTransactionData.DateServiceTill = Convert.ToDateTime(c1Transaction.GetData(rowIndex, COL_DATEFROM).ToString());
                                    LineTransactionData.DateServiceTillIsNull = true;
                                }
                            }
                            else
                            {
                                if (c1Transaction.GetData(rowIndex, COL_DATETO) != null && Convert.ToString(c1Transaction.GetData(rowIndex, COL_DATETO)).Trim() != "")
                                {
                                    LineTransactionData.DateServiceTill = Convert.ToDateTime(c1Transaction.GetData(rowIndex, COL_DATETO).ToString());
                                }
                                else
                                {
                                    LineTransactionData.DateServiceTill = Convert.ToDateTime(c1Transaction.GetData(rowIndex, COL_DATEFROM).ToString());
                                }
                            }
                        }
                        else
                        {
                            LineTransactionData.DateServiceTillIsNull = true;
                            //DOS To will be null that is why we have to send from date as DOS To also
                            LineTransactionData.DateServiceTill = Convert.ToDateTime(c1Transaction.GetData(rowIndex, COL_DATEFROM).ToString());
                        }



                        LineTransactionData.POSCode = Convert.ToString(c1Transaction.GetData(rowIndex, COL_POSCODE));
                        LineTransactionData.POSDescription = Convert.ToString(c1Transaction.GetData(rowIndex, COL_POSDESC));

                        LineTransactionData.TOSCode = Convert.ToString(c1Transaction.GetData(rowIndex, COL_TOSCODE));
                        LineTransactionData.TOSDescription = Convert.ToString(c1Transaction.GetData(rowIndex, COL_TOSDESC));

                        LineTransactionData.CPTCode = Convert.ToString(c1Transaction.GetData(rowIndex, COL_CPT_CODE));
                        LineTransactionData.CPTDescription = Convert.ToString(c1Transaction.GetData(rowIndex, COL_CPT_DESC));

                        #region " Commented Code - 20081017 - Sagar Ghodke

                        //LineTransactionData.Dx1Code = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX1_CODE));
                        //LineTransactionData.Dx1Description = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX1_DESC));
                        //LineTransactionData.Dx2Code = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX2_CODE));
                        //LineTransactionData.Dx2Description = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX2_DESC));
                        //LineTransactionData.Dx3Code = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX3_CODE));
                        //LineTransactionData.Dx3Description = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX3_DESC));
                        //LineTransactionData.Dx4Code = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX4_CODE));
                        //LineTransactionData.Dx4Description = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX4_DESC));
                        //LineTransactionData.Dx5Code = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX5_CODE));
                        //LineTransactionData.Dx5Description = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX5_DESC));
                        //LineTransactionData.Dx6Code = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX6_CODE));
                        //LineTransactionData.Dx6Description = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX6_DESC));
                        //LineTransactionData.Dx7Code = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX7_CODE));
                        //LineTransactionData.Dx7Description = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX7_DESC));
                        //LineTransactionData.Dx8Code = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX8_CODE));
                        //LineTransactionData.Dx8Description = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX8_DESC));

                        //if (c1Transaction.GetCellCheck(rowIndex, COL_DX1_PTR) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                        //{
                        //    LineTransactionData.Dx1Ptr = true;
                        //}
                        //else
                        //{
                        //    LineTransactionData.Dx1Ptr = false;
                        //}

                        //if (c1Transaction.GetCellCheck(rowIndex, COL_DX2_PTR) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                        //{
                        //    LineTransactionData.Dx2Ptr = true;
                        //}
                        //else
                        //{
                        //    LineTransactionData.Dx2Ptr = false;
                        //}

                        //if (c1Transaction.GetCellCheck(rowIndex, COL_DX3_PTR) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                        //{
                        //    LineTransactionData.Dx3Ptr = true;
                        //}
                        //else
                        //{
                        //    LineTransactionData.Dx3Ptr = false;
                        //}

                        //if (c1Transaction.GetCellCheck(rowIndex, COL_DX4_PTR) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                        //{
                        //    LineTransactionData.Dx4Ptr = true;
                        //}
                        //else
                        //{
                        //    LineTransactionData.Dx4Ptr = false;
                        //}
                        //if (c1Transaction.GetCellCheck(rowIndex, COL_DX5_PTR) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                        //{
                        //    LineTransactionData.Dx5Ptr = true;
                        //}
                        //else
                        //{
                        //    LineTransactionData.Dx5Ptr = false;
                        //}

                        //if (c1Transaction.GetCellCheck(rowIndex, COL_DX6_PTR) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                        //{
                        //    LineTransactionData.Dx6Ptr = true;
                        //}
                        //else
                        //{
                        //    LineTransactionData.Dx6Ptr = false;
                        //}

                        //if (c1Transaction.GetCellCheck(rowIndex, COL_DX7_PTR) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                        //{
                        //    LineTransactionData.Dx7Ptr = true;
                        //}
                        //else
                        //{
                        //    LineTransactionData.Dx7Ptr = false;
                        //}

                        //if (c1Transaction.GetCellCheck(rowIndex, COL_DX8_PTR) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                        //{
                        //    LineTransactionData.Dx8Ptr = true;
                        //}
                        //else
                        //{
                        //    LineTransactionData.Dx8Ptr = false;
                        //}

                        #endregion

                        if (c1Transaction.GetCellCheck(rowIndex, COL_DX1_PTR) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                        {
                            LineTransactionData.Dx1Ptr = true;
                            LineTransactionData.Dx1Code = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX1_CODE));
                            LineTransactionData.Dx1Description = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX1_DESC));
                        }
                        else
                        {
                            LineTransactionData.Dx1Ptr = false;
                            LineTransactionData.Dx1Code = "";
                            LineTransactionData.Dx1Description = "";
                        }

                        if (c1Transaction.GetCellCheck(rowIndex, COL_DX2_PTR) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                        {
                            LineTransactionData.Dx2Ptr = true;
                            LineTransactionData.Dx2Code = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX2_CODE));
                            LineTransactionData.Dx2Description = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX2_DESC));
                        }
                        else
                        {
                            LineTransactionData.Dx2Ptr = false;
                            LineTransactionData.Dx2Code = "";
                            LineTransactionData.Dx2Description = "";
                        }

                        if (c1Transaction.GetCellCheck(rowIndex, COL_DX3_PTR) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                        {
                            LineTransactionData.Dx3Ptr = true;
                            LineTransactionData.Dx3Code = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX3_CODE));
                            LineTransactionData.Dx3Description = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX3_DESC));
                        }
                        else
                        {
                            LineTransactionData.Dx3Ptr = false;
                            LineTransactionData.Dx3Code = "";
                            LineTransactionData.Dx3Description = "";
                        }

                        if (c1Transaction.GetCellCheck(rowIndex, COL_DX4_PTR) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                        {
                            LineTransactionData.Dx4Ptr = true;
                            LineTransactionData.Dx4Code = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX4_CODE));
                            LineTransactionData.Dx4Description = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX4_DESC));
                        }
                        else
                        {
                            LineTransactionData.Dx4Ptr = false;
                            LineTransactionData.Dx4Code = "";
                            LineTransactionData.Dx4Description = "";
                        }
                        if (c1Transaction.GetCellCheck(rowIndex, COL_DX5_PTR) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                        {
                            LineTransactionData.Dx5Ptr = true;
                            LineTransactionData.Dx5Code = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX5_CODE));
                            LineTransactionData.Dx5Description = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX5_DESC));
                        }
                        else
                        {
                            LineTransactionData.Dx5Ptr = false;
                            LineTransactionData.Dx5Code = "";
                            LineTransactionData.Dx5Description = "";
                        }

                        if (c1Transaction.GetCellCheck(rowIndex, COL_DX6_PTR) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                        {
                            LineTransactionData.Dx6Ptr = true;
                            LineTransactionData.Dx6Code = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX6_CODE));
                            LineTransactionData.Dx6Description = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX6_DESC));
                        }
                        else
                        {
                            LineTransactionData.Dx6Ptr = false;
                            LineTransactionData.Dx6Code = "";
                            LineTransactionData.Dx6Description = "";
                        }

                        if (c1Transaction.GetCellCheck(rowIndex, COL_DX7_PTR) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                        {
                            LineTransactionData.Dx7Ptr = true;
                            LineTransactionData.Dx7Code = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX7_CODE));
                            LineTransactionData.Dx7Description = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX7_DESC));
                        }
                        else
                        {
                            LineTransactionData.Dx7Ptr = false;
                            LineTransactionData.Dx7Code = "";
                            LineTransactionData.Dx7Description = "";
                        }

                        if (c1Transaction.GetCellCheck(rowIndex, COL_DX8_PTR) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                        {
                            LineTransactionData.Dx8Ptr = true;
                            LineTransactionData.Dx8Code = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX8_CODE));
                            LineTransactionData.Dx8Description = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX8_DESC));
                        }
                        else
                        {
                            LineTransactionData.Dx8Ptr = false;
                            LineTransactionData.Dx8Code = "";
                            LineTransactionData.Dx8Description = "";
                        }

                        if (c1Transaction.GetCellCheck(rowIndex, COL_ISEMG) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                        {
                            LineTransactionData.EMG = true;
                        }
                        else
                        {
                            LineTransactionData.EMG = false;
                        }

                        LineTransactionData.Mod1Code = Convert.ToString(c1Transaction.GetData(rowIndex, COL_MOD1_CODE));
                        LineTransactionData.Mod1Description = Convert.ToString(c1Transaction.GetData(rowIndex, COL_MOD1_DESC));
                        LineTransactionData.Mod2Code = Convert.ToString(c1Transaction.GetData(rowIndex, COL_MOD2_CODE));
                        LineTransactionData.Mod2Description = Convert.ToString(c1Transaction.GetData(rowIndex, COL_MOD2_DESC));
                        LineTransactionData.Mod3Code = Convert.ToString(c1Transaction.GetData(rowIndex, COL_MOD3_CODE));
                        LineTransactionData.Mod3Description = Convert.ToString(c1Transaction.GetData(rowIndex, COL_MOD3_DESC));
                        LineTransactionData.Mod4Code = Convert.ToString(c1Transaction.GetData(rowIndex, COL_MOD4_CODE));
                        LineTransactionData.Mod4Description = Convert.ToString(c1Transaction.GetData(rowIndex, COL_MOD4_DESC));

                        LineTransactionData.Charges = Convert.ToDecimal(c1Transaction.GetData(rowIndex, COL_CHARGES));
                        LineTransactionData.Unit = Convert.ToDecimal(c1Transaction.GetData(rowIndex, COL_UNIT));
                        LineTransactionData.Total = Convert.ToDecimal(c1Transaction.GetData(rowIndex, COL_TOTAL));
                        //LineTransactionData.AllowedCharges = 125;// Convert.ToDecimal(c1Transaction.GetData(rowIndex, COL_ALLOWED));
                        //dBilliedAmount
                        LineTransactionData.BilledAmount = Convert.ToDecimal(c1Transaction.GetData(rowIndex, COL_BILLEDAMOUNT));

                        if (_showAllowedColumn == true)
                        {
                            LineTransactionData.AllowedCharges = Convert.ToDecimal(c1Transaction.GetData(rowIndex, COL_ALLOWED));
                        }
                        else
                        {
                            if (Convert.ToDecimal(c1Transaction.GetData(rowIndex, COL_CHARGES)) > 0)
                            {
                                LineTransactionData.AllowedCharges = Convert.ToDecimal(c1Transaction.GetData(rowIndex, COL_TOTAL));
                            }
                        }

                        LineTransactionData.RefferingProvider = Convert.ToString(c1Transaction.GetData(rowIndex, COL_PROVIDER));
                        if (Convert.ToString(c1Transaction.GetData(rowIndex, COL_PROVIDER_ID)) != "")
                            LineTransactionData.RefferingProviderId = Convert.ToInt64(c1Transaction.GetData(rowIndex, COL_PROVIDER_ID));

                        LineTransactionData.ClinicID = _ClinicID;

                        //20080918 Anil
                        LineTransactionData.InsuranceID = Convert.ToInt64(c1Transaction.GetData(rowIndex, COL_INSURANCEID));
                        LineTransactionData.InsuranceName = Convert.ToString(c1Transaction.GetData(rowIndex, COL_INSURANCENAME));

                        if (Convert.ToString(c1Transaction.GetData(rowIndex, COL_INSSELF_PAYMODE)) != "")
                        {
                            LineTransactionData.InsuranceSelfMode = (PayerMode)Convert.ToInt32(c1Transaction.GetData(rowIndex, COL_INSSELF_PAYMODE).ToString());
                            LineTransactionData.InsuranceName = PayerMode.Self.ToString();
                        }
                        else
                        {
                            LineTransactionData.InsuranceSelfMode = PayerMode.None;
                        }

                        ////Functionality need to be implemented for notes
                        //GeneralNotes oNotes = new GeneralNotes();
                        //GeneralNote oNote = new GeneralNote();
                        //oNote.NoteID = 1;//Temp Hard Coded
                        //oNote.NoteDate =   gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString());   //Convert.ToInt64(DateTime.Now.ToShortDateString("MMddYYYhhmmtt"));
                        //oNote.NoteDescription = Convert.ToString(c1Transaction.GetData(rowIndex, COL_NOTE_DATA));
                        //oNote.NoteType = NoteType.GeneralNote;
                        //oNote.TransactionID = Convert.ToInt64(c1Transaction.GetData(rowIndex, COL_TRANSACTIONID));
                        //oNote.TransactionLineId = Convert.ToInt64(c1Transaction.GetData(rowIndex, COL_NO));
                        //oNote.ClinicID = _ClinicID; //temp Hard Code
                        //oNote.UserID = 1;//temp Hard Code
                        //oNotes.Add(oNote);
                        //LineTransactionData.LineNotes = oNotes;
                        ////

                        #region " Get Notes "
                        GeneralNotes oNotes = new GeneralNotes();
                        if (c1Transaction.GetData(rowIndex, COL_NOTE_DATA) != null)
                        {
                            oNotes = (GeneralNotes)(c1Transaction.GetData(rowIndex, COL_NOTE_DATA));
                            LineTransactionData.LineNotes = oNotes;
                        }
                        if (oNotes != null) { oNotes.Dispose(); }

                        #endregion " Get Notes "

                        LineTransactionData.LineStatus = TransactionStatus.Transacted;

                        //Code added on 20090511 by - Sagar Ghodke
                        //Code added to implement CLIA number functionality & senttoclaim for transaction lines
                        if (c1Transaction.GetCellCheck(rowIndex, COL_ISLABCPT) == CheckEnum.Checked)
                        { LineTransactionData.IsLabCPT = true; }
                        if (c1Transaction.GetData(rowIndex, COL_AUTHORIZATIONNO) != null && Convert.ToString(c1Transaction.GetData(rowIndex, COL_AUTHORIZATIONNO)).Trim() != "")
                        { LineTransactionData.AuthorizationNo = Convert.ToString(c1Transaction.GetData(rowIndex, COL_AUTHORIZATIONNO)).Trim(); }
                        if (c1Transaction.GetCellCheck(rowIndex, COL_SENTTOCLAIM) == CheckEnum.Checked)
                        { LineTransactionData.SendToClaim = true; }
                        //End code add 20090511,Sagar Ghodke 

                        if (c1Transaction.GetData(rowIndex, COL_LINEPRIMARY_DXCODE) != null && Convert.ToString(c1Transaction.GetData(rowIndex, COL_LINEPRIMARY_DXCODE)).Trim() != "")
                        {
                            LineTransactionData.LinePrimaryDxCode = Convert.ToString(c1Transaction.GetData(rowIndex, COL_LINEPRIMARY_DXCODE)).Trim();
                            LineTransactionData.LinePrimaryDxDesc = Convert.ToString(c1Transaction.GetData(rowIndex, COL_LINEPRIMARY_DXDESC)).Trim();
                        }

                        if (c1Transaction.GetCellCheck(rowIndex, COL_HOLD) == CheckEnum.Checked)
                        { LineTransactionData.IsHold = true; }
                        else { LineTransactionData.IsHold = false; }

                        LineTransactionData.HoldReason = Convert.ToString(c1Transaction.GetData(rowIndex, COL_HOLD_REASON));


                        #region " Fee-Schedule "

                        LineTransactionData.FeeScheduleType = _Fee_ScheduleType;
                        LineTransactionData.FeeScheduleID = _Fee_ScheduleID;

                        LineTransactionData.FacilityType = DefaultChargesType;

                        #endregion " Fee-Schedule "


                        if ((c1Transaction.GetData(rowIndex, COL_NDCCODE) != null) && (c1Transaction.GetData(rowIndex, COL_NDCCODE).ToString().Trim() != ""))
                        {

                            LineTransactionData.NDCCode = Convert.ToString(c1Transaction.GetData(rowIndex, COL_NDCCODE));
                            LineTransactionData.NDCUnit = Convert.ToString(c1Transaction.GetData(rowIndex, COL_NDCUNIT));
                            LineTransactionData.NDCUnitCode = Convert.ToString(c1Transaction.GetData(rowIndex, COL_NDCUNITCODE));
                            LineTransactionData.NDCUnitDescription = Convert.ToString(c1Transaction.GetData(rowIndex, COL_NDCUNITDESCRITION));
                            LineTransactionData.Prescription = Convert.ToString(c1Transaction.GetData(rowIndex, COL_PRESCRIPTION));//Prescription Number
                            //LineTransactionData.NDCID = Convert.ToString(c1Transaction.GetData(rowIndex, COL_HOLD_REASON));
                            //LineTransactionData.NDCUnit = Convert.ToString(c1Transaction.GetData(rowIndex, COL_HOLD_REASON));
                            //LineTransactionData.NDCUnitCode = Convert.ToString(c1Transaction.GetData(rowIndex, COL_HOLD_REASON));
                            //LineTransactionData.NDCUnitDescription= Convert.ToString(c1Transaction.GetData(rowIndex, COL_HOLD_REASON));
                            LineTransactionData.NDCUnitPricing = "0";
                            //LineTransactionData. = Convert.ToString(c1Transaction.GetData(rowIndex, COL_HOLD_REASON));
                            LineTransactionData.NDCCodeQualifier = "N4";
                        }

                        #region "Anesthesia"

                        if ((c1Transaction.GetData(rowIndex, COL_ANES_ISANESTHESIA) != null))
                        {

                            LineTransactionData.bIsAneshtesia = Convert.ToBoolean(c1Transaction.GetData(rowIndex, COL_ANES_ISANESTHESIA));
                            LineTransactionData.AnesthesiaID = Convert.ToInt64(c1Transaction.GetData(rowIndex, COL_ANES_ID));
                            LineTransactionData.AnesthesiaStartTime = Convert.ToDateTime(c1Transaction.GetData(rowIndex, COL_ANES_STARTDATE));
                            LineTransactionData.AnesthesiaEndTime = Convert.ToDateTime(c1Transaction.GetData(rowIndex, COL_ANES_ENDDATE));
                            LineTransactionData.AnesthesiaTotalMinutes = Convert.ToInt64(c1Transaction.GetData(rowIndex, COL_ANES_TOTALMIN));

                            LineTransactionData.AnesthesiaMinPerUnit = Convert.ToInt64(c1Transaction.GetData(rowIndex, COL_ANES_MINPERUNIT));
                            LineTransactionData.AnesthesiaTimeUnits = Convert.ToDecimal(c1Transaction.GetData(rowIndex, COL_ANES_TIMEUNITS));
                            LineTransactionData.AnesthesiaBaseUnits = Convert.ToDecimal(c1Transaction.GetData(rowIndex, COL_ANES_BASEUNITS));
                            LineTransactionData.AnesthesiaOtherUnits = Convert.ToDecimal(c1Transaction.GetData(rowIndex, COL_ANES_OTHERUNITS));
                            LineTransactionData.AnesthesiaTotalUnits = Convert.ToDecimal(c1Transaction.GetData(rowIndex, COL_ANES_TOTALUNITS));
                            LineTransactionData.bIsAutoCalculateAnesthesia = Convert.ToBoolean(c1Transaction.GetData(rowIndex, COL_ANES_ISAUTOCALCULATE));
                        }

                        #endregion

                        LineTransactionData.PrescriptionDescription = Convert.ToString(c1Transaction.GetData(rowIndex, COL_PRESCRIPTIONDESC));//Prescription Description

                        bool bServiceIsTheScreening = false;
                        Boolean.TryParse(Convert.ToString(c1Transaction.GetData(rowIndex, COL_SERVICESCREENING)), out bServiceIsTheScreening);
                        LineTransactionData.ServiceIsTheScreening = bServiceIsTheScreening;

                        bool bServiceIsTheResultScreening = false;
                        Boolean.TryParse(Convert.ToString(c1Transaction.GetData(rowIndex, COL_SERVICERESULTSCREENING)), out bServiceIsTheResultScreening);
                        LineTransactionData.ServiceIsTheResultOfScreening = bServiceIsTheResultScreening;

                        bool bServiceFamilyPlaningIndicator = false;
                        Boolean.TryParse(Convert.ToString(c1Transaction.GetData(rowIndex, COL_FAMILYPLANNINGINDICATOR)), out bServiceFamilyPlaningIndicator);
                        LineTransactionData.ServiceFamilyPlanningIndicator = bServiceFamilyPlaningIndicator;

                        if ((c1Transaction.GetData(rowIndex, COL_SELFCLAIM) != null) && _nContactID > 0)
                        {

                            LineTransactionData.bIsSelfClaim = Convert.ToBoolean(c1Transaction.GetData(rowIndex, COL_SELFCLAIM));
                        }

                        LineTransactionsData.Add(LineTransactionData);

                        if (LineTransactionData != null)
                        {
                            LineTransactionData.Dispose();
                            LineTransactionData = null;
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {

            }

            return LineTransactionsData;
        }

        public TransactionLines GetLineTransactions(DateTime DateValue)
        {
            //    bool _returnResult = false;
            TransactionLines LineTransactionsData = new TransactionLines();
            TransactionLine LineTransactionData = null;
            int rowIndex = 0;
            Int64 _ReadDate = 0;

            try
            {
                if (c1Transaction.Rows.Count > 0)
                {
                    _ReadDate = gloDateMaster.gloDate.DateAsNumber(DateValue.ToShortDateString());

                    for (int i = 1; i <= c1Transaction.Rows.Count - 1; i++)
                    {
                        rowIndex = i;
                        LineTransactionData = new TransactionLine();

                        Int64 _ServiceDate = gloDateMaster.gloDate.DateAsNumber(Convert.ToDateTime(c1Transaction.GetData(rowIndex, COL_DATEFROM).ToString()).ToShortDateString());


                        if (_ReadDate == _ServiceDate)
                        {

                            LineTransactionData.TransactionLineId = Convert.ToInt64(c1Transaction.GetData(rowIndex, COL_NO).ToString());
                            LineTransactionData.EMRTreatmentLineNo = Convert.ToInt64(c1Transaction.GetData(rowIndex, COL_EMRTREATMENTLINENO).ToString());

                            LineTransactionData.TransactionId = Convert.ToInt64(c1Transaction.GetData(rowIndex, COL_TRANSACTIONID));
                            LineTransactionData.TransactionDetailID = Convert.ToInt64(c1Transaction.GetData(rowIndex, COL_TRANSACTION_DETAIL_ID));

                            LineTransactionData.DateServiceFrom = Convert.ToDateTime(c1Transaction.GetData(rowIndex, COL_DATEFROM).ToString());


                            //Previouslly to date default return value of from date, we dont know the impact but
                            //now we are implementing the to date field into charge entry for ECET implmentation
                            //we have to check impact throughlly as we complete this one
                            //now we are assigning actual to date value to till date column

                            //---*** PREVIOUS CODE---***STAT----
                            //This first line was not commented and second one was commeted
                            //1 Line LineTransactionData.DateServiceTill = Convert.ToDateTime(c1Transaction.GetData(rowIndex, COL_DATEFROM).ToString());
                            //2 Line LineTransactionData.DateServiceTill = Convert.ToDateTime(c1Transaction.GetData(rowIndex, COL_DATETO).ToString());
                            //---*** PREVIOUS CODE---***FINISH----

                            //LineTransactionData.DateServiceTill = Convert.ToDateTime(c1Transaction.GetData(rowIndex, COL_DATETO).ToString());
                            //new implementation for to date 
                            if (_showTilldateColumn == true)
                            {
                                if (_showTillDateColumnUseNullDate == true)
                                {
                                    if (c1Transaction.GetData(rowIndex, COL_DATETO) != null)
                                    {
                                        LineTransactionData.DateServiceTill = Convert.ToDateTime(c1Transaction.GetData(rowIndex, COL_DATETO).ToString());
                                    }
                                    else
                                    {
                                        //to date will return null value that's why for handle error we are setting from date value 
                                        //otherwise there is no logic
                                        LineTransactionData.DateServiceTill = Convert.ToDateTime(c1Transaction.GetData(rowIndex, COL_DATEFROM).ToString());
                                        LineTransactionData.DateServiceTillIsNull = true;
                                    }
                                }
                                else
                                {
                                    if (c1Transaction.GetData(rowIndex, COL_DATETO) != null && Convert.ToString(c1Transaction.GetData(rowIndex, COL_DATETO)).Trim() != "")
                                    {
                                        LineTransactionData.DateServiceTill = Convert.ToDateTime(c1Transaction.GetData(rowIndex, COL_DATETO).ToString());
                                    }
                                    else
                                    {
                                        LineTransactionData.DateServiceTill = Convert.ToDateTime(c1Transaction.GetData(rowIndex, COL_DATEFROM).ToString());
                                    }
                                }
                            }
                            else
                            {
                                LineTransactionData.DateServiceTillIsNull = true;
                                //DOS To will be null that is why we have to send from date as DOS To also
                                LineTransactionData.DateServiceTill = Convert.ToDateTime(c1Transaction.GetData(rowIndex, COL_DATEFROM).ToString());
                            }

                            LineTransactionData.POSCode = Convert.ToString(c1Transaction.GetData(rowIndex, COL_POSCODE));
                            LineTransactionData.POSDescription = Convert.ToString(c1Transaction.GetData(rowIndex, COL_POSDESC));

                            LineTransactionData.TOSCode = Convert.ToString(c1Transaction.GetData(rowIndex, COL_TOSCODE));
                            LineTransactionData.TOSDescription = Convert.ToString(c1Transaction.GetData(rowIndex, COL_TOSDESC));

                            LineTransactionData.CPTCode = Convert.ToString(c1Transaction.GetData(rowIndex, COL_CPT_CODE));
                            LineTransactionData.CPTDescription = Convert.ToString(c1Transaction.GetData(rowIndex, COL_CPT_DESC));

                            #region " Commented Code  - 20081017 , Sagar Ghodke "
                            //LineTransactionData.Dx1Code = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX1_CODE));
                            //LineTransactionData.Dx1Description = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX1_DESC));
                            //LineTransactionData.Dx2Code = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX2_CODE));
                            //LineTransactionData.Dx2Description = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX2_DESC));
                            //LineTransactionData.Dx3Code = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX3_CODE));
                            //LineTransactionData.Dx3Description = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX3_DESC));
                            //LineTransactionData.Dx4Code = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX4_CODE));
                            //LineTransactionData.Dx4Description = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX4_DESC));
                            //LineTransactionData.Dx5Code = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX5_CODE));
                            //LineTransactionData.Dx5Description = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX5_DESC));
                            //LineTransactionData.Dx6Code = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX6_CODE));
                            //LineTransactionData.Dx6Description = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX6_DESC));
                            //LineTransactionData.Dx7Code = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX7_CODE));
                            //LineTransactionData.Dx7Description = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX7_DESC));
                            //LineTransactionData.Dx8Code = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX8_CODE));
                            //LineTransactionData.Dx8Description = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX8_DESC));

                            //if (c1Transaction.GetCellCheck(rowIndex, COL_DX1_PTR) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                            //{
                            //    LineTransactionData.Dx1Ptr = true;
                            //}
                            //else
                            //{
                            //    LineTransactionData.Dx1Ptr = false;
                            //}

                            //if (c1Transaction.GetCellCheck(rowIndex, COL_DX2_PTR) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                            //{
                            //    LineTransactionData.Dx2Ptr = true;
                            //}
                            //else
                            //{
                            //    LineTransactionData.Dx2Ptr = false;
                            //}

                            //if (c1Transaction.GetCellCheck(rowIndex, COL_DX3_PTR) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                            //{
                            //    LineTransactionData.Dx3Ptr = true;
                            //}
                            //else
                            //{
                            //    LineTransactionData.Dx3Ptr = false;
                            //}

                            //if (c1Transaction.GetCellCheck(rowIndex, COL_DX4_PTR) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                            //{
                            //    LineTransactionData.Dx4Ptr = true;
                            //}
                            //else
                            //{
                            //    LineTransactionData.Dx4Ptr = false;
                            //}
                            //if (c1Transaction.GetCellCheck(rowIndex, COL_DX5_PTR) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                            //{
                            //    LineTransactionData.Dx5Ptr = true;
                            //}
                            //else
                            //{
                            //    LineTransactionData.Dx5Ptr = false;
                            //}

                            //if (c1Transaction.GetCellCheck(rowIndex, COL_DX6_PTR) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                            //{
                            //    LineTransactionData.Dx6Ptr = true;
                            //}
                            //else
                            //{
                            //    LineTransactionData.Dx6Ptr = false;
                            //}

                            //if (c1Transaction.GetCellCheck(rowIndex, COL_DX7_PTR) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                            //{
                            //    LineTransactionData.Dx7Ptr = true;
                            //}
                            //else
                            //{
                            //    LineTransactionData.Dx7Ptr = false;
                            //}

                            //if (c1Transaction.GetCellCheck(rowIndex, COL_DX8_PTR) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                            //{
                            //    LineTransactionData.Dx8Ptr = true;
                            //}
                            //else
                            //{
                            //    LineTransactionData.Dx8Ptr = false;
                            //} 
                            #endregion

                            if (c1Transaction.GetCellCheck(rowIndex, COL_DX1_PTR) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                            {
                                LineTransactionData.Dx1Ptr = true;
                                LineTransactionData.Dx1Code = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX1_CODE));
                                LineTransactionData.Dx1Description = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX1_DESC));
                            }
                            else
                            {
                                LineTransactionData.Dx1Ptr = false;
                                LineTransactionData.Dx1Code = "";
                                LineTransactionData.Dx1Description = "";
                            }

                            if (c1Transaction.GetCellCheck(rowIndex, COL_DX2_PTR) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                            {
                                LineTransactionData.Dx2Ptr = true;
                                LineTransactionData.Dx2Code = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX2_CODE));
                                LineTransactionData.Dx2Description = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX2_DESC));
                            }
                            else
                            {
                                LineTransactionData.Dx2Ptr = false;
                                LineTransactionData.Dx2Code = "";
                                LineTransactionData.Dx2Description = "";
                            }

                            if (c1Transaction.GetCellCheck(rowIndex, COL_DX3_PTR) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                            {
                                LineTransactionData.Dx3Ptr = true;
                                LineTransactionData.Dx3Code = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX3_CODE));
                                LineTransactionData.Dx3Description = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX3_DESC));
                            }
                            else
                            {
                                LineTransactionData.Dx3Ptr = false;
                                LineTransactionData.Dx3Code = "";
                                LineTransactionData.Dx3Description = "";
                            }

                            if (c1Transaction.GetCellCheck(rowIndex, COL_DX4_PTR) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                            {
                                LineTransactionData.Dx4Ptr = true;
                                LineTransactionData.Dx4Code = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX4_CODE));
                                LineTransactionData.Dx4Description = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX4_DESC));
                            }
                            else
                            {
                                LineTransactionData.Dx4Ptr = false;
                                LineTransactionData.Dx4Code = "";
                                LineTransactionData.Dx4Description = "";
                            }
                            if (c1Transaction.GetCellCheck(rowIndex, COL_DX5_PTR) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                            {
                                LineTransactionData.Dx5Ptr = true;
                                LineTransactionData.Dx5Code = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX5_CODE));
                                LineTransactionData.Dx5Description = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX5_DESC));
                            }
                            else
                            {
                                LineTransactionData.Dx5Ptr = false;
                                LineTransactionData.Dx5Code = "";
                                LineTransactionData.Dx5Description = "";
                            }

                            if (c1Transaction.GetCellCheck(rowIndex, COL_DX6_PTR) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                            {
                                LineTransactionData.Dx6Ptr = true;
                                LineTransactionData.Dx6Code = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX6_CODE));
                                LineTransactionData.Dx6Description = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX6_DESC));
                            }
                            else
                            {
                                LineTransactionData.Dx6Ptr = false;
                                LineTransactionData.Dx6Code = "";
                                LineTransactionData.Dx6Description = "";
                            }

                            if (c1Transaction.GetCellCheck(rowIndex, COL_DX7_PTR) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                            {
                                LineTransactionData.Dx7Ptr = true;
                                LineTransactionData.Dx7Code = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX7_CODE));
                                LineTransactionData.Dx7Description = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX7_DESC));
                            }
                            else
                            {
                                LineTransactionData.Dx7Ptr = false;
                                LineTransactionData.Dx7Code = "";
                                LineTransactionData.Dx7Description = "";
                            }

                            if (c1Transaction.GetCellCheck(rowIndex, COL_DX8_PTR) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                            {
                                LineTransactionData.Dx8Ptr = true;
                                LineTransactionData.Dx8Code = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX8_CODE));
                                LineTransactionData.Dx8Description = Convert.ToString(c1Transaction.GetData(rowIndex, COL_DX8_DESC));
                            }
                            else
                            {
                                LineTransactionData.Dx8Ptr = false;
                                LineTransactionData.Dx8Code = "";
                                LineTransactionData.Dx8Description = "";
                            }

                            if (c1Transaction.GetCellCheck(rowIndex, COL_ISEMG) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                            {
                                LineTransactionData.EMG = true;
                            }
                            else
                            {
                                LineTransactionData.EMG = false;
                            }

                            LineTransactionData.Mod1Code = Convert.ToString(c1Transaction.GetData(rowIndex, COL_MOD1_CODE));
                            LineTransactionData.Mod1Description = Convert.ToString(c1Transaction.GetData(rowIndex, COL_MOD1_DESC));
                            LineTransactionData.Mod2Code = Convert.ToString(c1Transaction.GetData(rowIndex, COL_MOD2_CODE));
                            LineTransactionData.Mod2Description = Convert.ToString(c1Transaction.GetData(rowIndex, COL_MOD2_DESC));
                            LineTransactionData.Mod3Code = Convert.ToString(c1Transaction.GetData(rowIndex, COL_MOD3_CODE));
                            LineTransactionData.Mod3Description = Convert.ToString(c1Transaction.GetData(rowIndex, COL_MOD3_DESC));
                            LineTransactionData.Mod4Code = Convert.ToString(c1Transaction.GetData(rowIndex, COL_MOD4_CODE));
                            LineTransactionData.Mod4Description = Convert.ToString(c1Transaction.GetData(rowIndex, COL_MOD4_DESC));

                            LineTransactionData.Charges = Convert.ToDecimal(c1Transaction.GetData(rowIndex, COL_CHARGES));
                            LineTransactionData.Unit = Convert.ToDecimal(c1Transaction.GetData(rowIndex, COL_UNIT));
                            LineTransactionData.Total = Convert.ToDecimal(c1Transaction.GetData(rowIndex, COL_TOTAL));
                            //LineTransactionData.AllowedCharges = 125;// Convert.ToDecimal(c1Transaction.GetData(rowIndex, COL_ALLOWED));
                            if (_showAllowedColumn == true)
                            {
                                LineTransactionData.AllowedCharges = Convert.ToDecimal(c1Transaction.GetData(rowIndex, COL_ALLOWED));
                            }
                            else
                            {
                                if (Convert.ToDecimal(c1Transaction.GetData(rowIndex, COL_CHARGES)) > 0)
                                {
                                    LineTransactionData.AllowedCharges = Convert.ToDecimal(c1Transaction.GetData(rowIndex, COL_TOTAL));
                                }
                            }



                            LineTransactionData.RefferingProvider = Convert.ToString(c1Transaction.GetData(rowIndex, COL_PROVIDER));
                            if (Convert.ToString(c1Transaction.GetData(rowIndex, COL_PROVIDER_ID)) != "")
                                LineTransactionData.RefferingProviderId = Convert.ToInt64(c1Transaction.GetData(rowIndex, COL_PROVIDER_ID));

                            LineTransactionData.ClinicID = _ClinicID;

                            //20080918 Anil
                            LineTransactionData.InsuranceID = Convert.ToInt64(c1Transaction.GetData(rowIndex, COL_INSURANCEID));
                            LineTransactionData.InsuranceName = Convert.ToString(c1Transaction.GetData(rowIndex, COL_INSURANCENAME));

                            if (Convert.ToString(c1Transaction.GetData(rowIndex, COL_INSSELF_PAYMODE)) != "")
                            {
                                LineTransactionData.InsuranceSelfMode = (PayerMode)Convert.ToInt32(c1Transaction.GetData(rowIndex, COL_INSSELF_PAYMODE).ToString());
                                LineTransactionData.InsuranceName = PayerMode.Self.ToString();
                            }
                            else
                            {
                                LineTransactionData.InsuranceSelfMode = PayerMode.None;
                            }



                            ////Functionality need to be implemented for notes
                            //GeneralNotes oNotes = new GeneralNotes();
                            //GeneralNote oNote = new GeneralNote();
                            //oNote.NoteID = 1;//Temp Hard Coded
                            //oNote.NoteDate =   gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString());   //Convert.ToInt64(DateTime.Now.ToShortDateString("MMddYYYhhmmtt"));
                            //oNote.NoteDescription = Convert.ToString(c1Transaction.GetData(rowIndex, COL_NOTE_DATA));
                            //oNote.NoteType = NoteType.GeneralNote;
                            //oNote.TransactionID = Convert.ToInt64(c1Transaction.GetData(rowIndex, COL_TRANSACTIONID));
                            //oNote.TransactionLineId = Convert.ToInt64(c1Transaction.GetData(rowIndex, COL_NO));
                            //oNote.ClinicID = _ClinicID; //temp Hard Code
                            //oNote.UserID = 1;//temp Hard Code
                            //oNotes.Add(oNote);
                            //LineTransactionData.LineNotes = oNotes;
                            ////

                            #region " Get Notes "
                            GeneralNotes oNotes = new GeneralNotes();
                            if (c1Transaction.GetData(rowIndex, COL_NOTE_DATA) != null)
                            {
                                oNotes = (GeneralNotes)(c1Transaction.GetData(rowIndex, COL_NOTE_DATA));
                                LineTransactionData.LineNotes = oNotes;
                            }
                            if (oNotes != null) { oNotes.Dispose(); }

                            #endregion " Get Notes "


                            LineTransactionData.LineStatus = TransactionStatus.Transacted;

                            //Code added on 20090511 by - Sagar Ghodke
                            //Code added to implement CLIA number functionality & senttoclaim for transaction lines
                            if (c1Transaction.GetCellCheck(rowIndex, COL_ISLABCPT) == CheckEnum.Checked)
                            { LineTransactionData.IsLabCPT = true; }
                            if (c1Transaction.GetData(rowIndex, COL_AUTHORIZATIONNO) != null && Convert.ToString(c1Transaction.GetData(rowIndex, COL_AUTHORIZATIONNO)).Trim() != "")
                            { LineTransactionData.AuthorizationNo = Convert.ToString(c1Transaction.GetData(rowIndex, COL_AUTHORIZATIONNO)).Trim(); }
                            if (c1Transaction.GetCellCheck(rowIndex, COL_SENTTOCLAIM) == CheckEnum.Checked)
                            { LineTransactionData.SendToClaim = true; }
                            //End code add 20090511,Sagar Ghodke 

                            if (c1Transaction.GetData(rowIndex, COL_LINEPRIMARY_DXCODE) != null && Convert.ToString(c1Transaction.GetData(rowIndex, COL_LINEPRIMARY_DXCODE)).Trim() != "")
                            {
                                LineTransactionData.LinePrimaryDxCode = Convert.ToString(c1Transaction.GetData(rowIndex, COL_LINEPRIMARY_DXCODE)).Trim();
                                LineTransactionData.LinePrimaryDxDesc = Convert.ToString(c1Transaction.GetData(rowIndex, COL_LINEPRIMARY_DXDESC)).Trim();
                            }

                            if (c1Transaction.GetCellCheck(rowIndex, COL_HOLD) == CheckEnum.Checked)
                            { LineTransactionData.IsHold = true; }
                            else { LineTransactionData.IsHold = false; }

                            LineTransactionData.HoldReason = Convert.ToString(c1Transaction.GetData(rowIndex, COL_HOLD_REASON));


                            if ((c1Transaction.GetData(rowIndex, COL_NDCCODE) != null) && (c1Transaction.GetData(rowIndex, COL_NDCCODE).ToString().Trim() != ""))
                            {

                                LineTransactionData.NDCCode = Convert.ToString(c1Transaction.GetData(rowIndex, COL_NDCCODE));
                                LineTransactionData.NDCUnit = Convert.ToString(c1Transaction.GetData(rowIndex, COL_NDCUNIT));
                                LineTransactionData.NDCUnitCode = Convert.ToString(c1Transaction.GetData(rowIndex, COL_NDCUNITCODE));
                                LineTransactionData.NDCUnitDescription = Convert.ToString(c1Transaction.GetData(rowIndex, COL_NDCUNITDESCRITION));
                                LineTransactionData.Prescription = Convert.ToString(c1Transaction.GetData(rowIndex, COL_PRESCRIPTION));
                                LineTransactionData.PrescriptionDescription = Convert.ToString(c1Transaction.GetData(rowIndex, COL_PRESCRIPTIONDESC));
                                //LineTransactionData.NDCID = Convert.ToString(c1Transaction.GetData(rowIndex, COL_HOLD_REASON));
                                //LineTransactionData.NDCUnit = Convert.ToString(c1Transaction.GetData(rowIndex, COL_HOLD_REASON));
                                //LineTransactionData.NDCUnitCode = Convert.ToString(c1Transaction.GetData(rowIndex, COL_HOLD_REASON));
                                //LineTransactionData.NDCUnitDescription= Convert.ToString(c1Transaction.GetData(rowIndex, COL_HOLD_REASON));
                                LineTransactionData.NDCUnitPricing = "0";
                                //LineTransactionData. = Convert.ToString(c1Transaction.GetData(rowIndex, COL_HOLD_REASON));
                                LineTransactionData.NDCCodeQualifier = "N4";
                            }

                            #region "Anesthesia"

                            if ((c1Transaction.GetData(rowIndex, COL_ANES_ID) != null) && (c1Transaction.GetData(rowIndex, COL_ANES_ID).ToString().Trim() != ""))
                            {

                                LineTransactionData.bIsAneshtesia = Convert.ToBoolean(c1Transaction.GetData(rowIndex, COL_ANES_ISANESTHESIA));
                                LineTransactionData.AnesthesiaID = Convert.ToInt64(c1Transaction.GetData(rowIndex, COL_ANES_ID));
                                LineTransactionData.AnesthesiaStartTime = Convert.ToDateTime(c1Transaction.GetData(rowIndex, COL_ANES_STARTDATE));
                                LineTransactionData.AnesthesiaEndTime = Convert.ToDateTime(c1Transaction.GetData(rowIndex, COL_ANES_ENDDATE));
                                LineTransactionData.AnesthesiaTotalMinutes = Convert.ToInt64(c1Transaction.GetData(rowIndex, COL_ANES_TOTALMIN));

                                LineTransactionData.AnesthesiaMinPerUnit = Convert.ToInt64(c1Transaction.GetData(rowIndex, COL_ANES_MINPERUNIT));
                                LineTransactionData.AnesthesiaTimeUnits = Convert.ToDecimal(c1Transaction.GetData(rowIndex, COL_ANES_TIMEUNITS));
                                LineTransactionData.AnesthesiaBaseUnits = Convert.ToDecimal(c1Transaction.GetData(rowIndex, COL_ANES_BASEUNITS));
                                LineTransactionData.AnesthesiaOtherUnits = Convert.ToDecimal(c1Transaction.GetData(rowIndex, COL_ANES_OTHERUNITS));
                                LineTransactionData.AnesthesiaTotalUnits = Convert.ToDecimal(c1Transaction.GetData(rowIndex, COL_ANES_TOTALUNITS));
                                LineTransactionData.bIsAutoCalculateAnesthesia = Convert.ToBoolean(c1Transaction.GetData(rowIndex, COL_ANES_ISAUTOCALCULATE));

                            }
                            #endregion
                            if ((c1Transaction.GetData(rowIndex, COL_SELFCLAIM) != null))
                            {

                                LineTransactionData.bIsSelfClaim = Convert.ToBoolean(c1Transaction.GetData(rowIndex, COL_SELFCLAIM));
                            }



                            LineTransactionsData.Add(LineTransactionData);

                            if (LineTransactionData != null)
                            {
                                LineTransactionData.Dispose();
                                LineTransactionData = null;
                            }
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {

            }

            return LineTransactionsData;
        }

        public ArrayList GetUniqueDates()
        {
            ArrayList _Result = new ArrayList();

            try
            {
                for (int i = 1; i <= c1Transaction.Rows.Count - 1; i++)
                {
                    DateTime _ServiceDate = Convert.ToDateTime(c1Transaction.GetData(i, COL_DATEFROM).ToString());
                    if (_Result.Contains(_ServiceDate) == false)
                    {
                        _Result.Add(_ServiceDate);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _Result = null;
            }
            return _Result;

        }

        public DateTime GetOldestSeriveDate()
        {
            ArrayList _Result = new ArrayList();
            DateTime _RetDateTime = DateTime.MinValue;
            try
            {
                for (int i = 1; i <= c1Transaction.Rows.Count - 1; i++)
                {
                    DateTime _ServiceDate = Convert.ToDateTime(c1Transaction.GetData(i, COL_DATEFROM).ToString());
                    if (_Result.Contains(_ServiceDate) == false)
                    {
                        _Result.Add(_ServiceDate);
                    }
                }
                _Result.Sort();
                if (_Result.Count > 0)
                {
                    _RetDateTime = Convert.ToDateTime(_Result[0].ToString());
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _Result = null;
            }
            return _RetDateTime;

        }

        public decimal GetInsurancePayment(Int64 InsuranceId)
        {
            decimal _InsurancePaymentTotal = 0;
            try
            {
                if (c1Transaction != null && c1Transaction.Rows.Count > 0)
                {
                    for (int i = 1; i <= c1Transaction.Rows.Count - 1; i++)
                    {
                        if (Convert.ToString(c1Transaction.GetData(i, COL_INSURANCENAME)) != "")
                        {
                            if (Convert.ToString(c1Transaction.GetData(i, COL_INSURANCEID)) != "")
                            {
                                Int64 _LineInsuranceID = 0;
                                _LineInsuranceID = Convert.ToInt64(c1Transaction.GetData(i, COL_INSURANCEID).ToString());
                                if (_LineInsuranceID > 0)
                                {
                                    if (_LineInsuranceID == InsuranceId)
                                    {

                                        decimal _LineCharges = 0;

                                        if (Convert.ToString(c1Transaction.GetData(i, COL_CHARGES)) != "")
                                        {
                                            _LineCharges = Convert.ToDecimal(c1Transaction.GetData(i, COL_CHARGES).ToString());
                                            if (_LineCharges >= 0)
                                            {
                                                _InsurancePaymentTotal = _InsurancePaymentTotal + _LineCharges;
                                            }
                                        }
                                    }
                                }
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

            }
            _InsurancePaymentTotal = Convert.ToDecimal(_InsurancePaymentTotal.ToString("#0.00"));
            return _InsurancePaymentTotal;
        }

        public Int64 GetRowInsuranceId(int rowIndex)
        {
            Int64 _InsuranceId = 0;

            try
            {
                if (HasInsurance(rowIndex) == true)
                {
                    _InsuranceId = Convert.ToInt64(c1Transaction.GetData(rowIndex, COL_INSURANCEID));
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _InsuranceId = 0;
            }
            finally
            {

            }
            return _InsuranceId;

        }

        public string GetRowPrimaryDxCode(int rowIndex)
        {
            string _PrimaryDxCode = "";

            try
            {
                if (HasPrimaryDx(rowIndex) == true)
                {
                    _PrimaryDxCode = Convert.ToString(c1Transaction.GetData(rowIndex, COL_LINEPRIMARY_DXCODE)).Trim();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _PrimaryDxCode = "";
            }
            finally
            {

            }
            return _PrimaryDxCode;

        }

        public string GetRowInsuranceName(int rowIndex)
        {
            string _InsuranceName = "";
            try
            {
                if (HasInsurance(rowIndex) == true)
                {
                    _InsuranceName = Convert.ToString(c1Transaction.GetData(rowIndex, COL_INSURANCENAME));
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            { }
            return _InsuranceName;
        }

        public int GetLinesCount()
        {
            int _rowsCount = 0;

            try
            {
                if (c1Transaction != null)
                {
                    _rowsCount = c1Transaction.Rows.Count;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {

            }
            return _rowsCount;
        }

        public int GetLinesCountNew()
        {
            int _rowsCount = 0;

            try
            {
                if (c1Transaction != null)
                {
                    foreach (C1.Win.C1FlexGrid.Row dRow in c1Transaction.Rows)
                    {
                        if (dRow.Index > 0)
                        {

                            if (Convert.ToBoolean(dRow[COL_SELFCLAIM]) == false)
                            {
                                _rowsCount++;
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

            }
            return _rowsCount;
        }

        public Object GetItem(int rowIndex, int colIndex)
        {
            Object oItem = null;
            try
            {
                if (c1Transaction != null && c1Transaction.Rows.Count > 0)
                {
                    if (rowIndex >= 0 && c1Transaction.Rows.Count > rowIndex && COL_COUNT > colIndex)
                    {
                        oItem = new object();
                        oItem = c1Transaction.GetData(rowIndex, colIndex);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {

            }

            return oItem;
        }

        public bool SetLineTransaction(TransactionLine LineTransactionData)
        {
            int rowIndex = 0;
            bool _returnResult = false;
            try
            {
                if (LineTransactionData != null)
                {
                    rowIndex = c1Transaction.Rows.Count;
                    c1Transaction.Rows.Add();

                    c1Transaction.SetData(rowIndex, COL_NO, LineTransactionData.TransactionLineId);
                    c1Transaction.SetData(rowIndex, COL_EMRTREATMENTLINENO, LineTransactionData.EMRTreatmentLineNo);
                    c1Transaction.SetData(rowIndex, COL_TRANSACTIONID, LineTransactionData.TransactionId);
                    c1Transaction.SetData(rowIndex, COL_TRANSACTION_DETAIL_ID, LineTransactionData.TransactionDetailID);

                    c1Transaction.SetData(rowIndex, COL_DATEFROM, LineTransactionData.DateServiceFrom.ToString("dd-MMM-yy"));

                    if (_showTilldateColumn == true)
                    {
                        if (_showTillDateColumnUseNullDate == true)
                        {
                            if (LineTransactionData.DateServiceTillIsNull == true)
                            {
                                c1Transaction.SetData(rowIndex, COL_DATETO, null);
                            }
                            else
                            {
                                c1Transaction.SetData(rowIndex, COL_DATETO, LineTransactionData.DateServiceTill.ToString("dd-MMM-yy"));
                            }
                        }
                        else
                        {
                            c1Transaction.SetData(rowIndex, COL_DATETO, LineTransactionData.DateServiceTill.ToString("dd-MMM-yy"));
                        }
                    }
                    else
                    {
                        c1Transaction.SetData(rowIndex, COL_DATETO, LineTransactionData.DateServiceTill.ToString("dd-MMM-yy"));
                    }

                    c1Transaction.SetData(rowIndex, COL_POSCODE, LineTransactionData.POSCode.ToString());
                    c1Transaction.SetData(rowIndex, COL_POSDESC, LineTransactionData.POSDescription.ToString());

                    c1Transaction.SetData(rowIndex, COL_TOSCODE, LineTransactionData.TOSCode.ToString());
                    c1Transaction.SetData(rowIndex, COL_TOSDESC, LineTransactionData.TOSDescription.ToString());

                    c1Transaction.SetData(rowIndex, COL_CPT_CODE, LineTransactionData.CPTCode.ToString());
                    c1Transaction.SetData(rowIndex, COL_CPT_DESC, LineTransactionData.CPTDescription.ToString());
                    //SetCellNote(rowIndex, COL_CPT_DESC);

                    c1Transaction.SetData(rowIndex, COL_DX1_CODE, LineTransactionData.Dx1Code.ToString());
                    c1Transaction.SetData(rowIndex, COL_DX1_DESC, LineTransactionData.Dx1Description.ToString());
                    //SetCellNote(rowIndex, COL_DX1_DESC);

                    c1Transaction.SetData(rowIndex, COL_DX2_CODE, LineTransactionData.Dx2Code.ToString());
                    c1Transaction.SetData(rowIndex, COL_DX2_DESC, LineTransactionData.Dx2Description.ToString());
                    //SetCellNote(rowIndex, COL_DX2_DESC);

                    c1Transaction.SetData(rowIndex, COL_DX3_CODE, LineTransactionData.Dx3Code.ToString());
                    c1Transaction.SetData(rowIndex, COL_DX3_DESC, LineTransactionData.Dx3Description.ToString());
                    //SetCellNote(rowIndex, COL_DX3_DESC);

                    c1Transaction.SetData(rowIndex, COL_DX4_CODE, LineTransactionData.Dx4Code.ToString());
                    c1Transaction.SetData(rowIndex, COL_DX4_DESC, LineTransactionData.Dx4Description.ToString());
                    //SetCellNote(rowIndex, COL_DX4_DESC);

                    c1Transaction.SetData(rowIndex, COL_DX5_CODE, LineTransactionData.Dx5Code.ToString());
                    c1Transaction.SetData(rowIndex, COL_DX5_DESC, LineTransactionData.Dx5Description.ToString());
                    //SetCellNote(rowIndex, COL_DX5_DESC);

                    c1Transaction.SetData(rowIndex, COL_DX6_CODE, LineTransactionData.Dx6Code.ToString());
                    c1Transaction.SetData(rowIndex, COL_DX6_DESC, LineTransactionData.Dx6Description.ToString());
                    //SetCellNote(rowIndex, COL_DX6_DESC);

                    c1Transaction.SetData(rowIndex, COL_DX7_CODE, LineTransactionData.Dx7Code.ToString());
                    c1Transaction.SetData(rowIndex, COL_DX7_DESC, LineTransactionData.Dx7Description.ToString());
                    //SetCellNote(rowIndex, COL_DX7_DESC);

                    c1Transaction.SetData(rowIndex, COL_DX8_CODE, LineTransactionData.Dx8Code.ToString());
                    c1Transaction.SetData(rowIndex, COL_DX8_DESC, LineTransactionData.Dx8Description.ToString());
                    //SetCellNote(rowIndex, COL_DX8_DESC);


                    if (LineTransactionData.Dx1Ptr)
                    {
                        c1Transaction.SetCellCheck(rowIndex, COL_DX1_PTR, C1.Win.C1FlexGrid.CheckEnum.Checked);
                    }
                    else
                    {
                        c1Transaction.SetCellCheck(rowIndex, COL_DX1_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                    }

                    if (LineTransactionData.Dx1Ptr)
                    {
                        c1Transaction.SetCellCheck(rowIndex, COL_DX2_PTR, C1.Win.C1FlexGrid.CheckEnum.Checked);
                    }
                    else
                    {
                        c1Transaction.SetCellCheck(rowIndex, COL_DX2_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                    }

                    if (LineTransactionData.Dx1Ptr)
                    {
                        c1Transaction.SetCellCheck(rowIndex, COL_DX3_PTR, C1.Win.C1FlexGrid.CheckEnum.Checked);
                    }
                    else
                    {
                        c1Transaction.SetCellCheck(rowIndex, COL_DX3_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                    }

                    if (LineTransactionData.Dx1Ptr)
                    {
                        c1Transaction.SetCellCheck(rowIndex, COL_DX4_PTR, C1.Win.C1FlexGrid.CheckEnum.Checked);
                    }
                    else
                    {
                        c1Transaction.SetCellCheck(rowIndex, COL_DX4_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                    }
                    if (LineTransactionData.Dx5Ptr)
                    {
                        c1Transaction.SetCellCheck(rowIndex, COL_DX5_PTR, C1.Win.C1FlexGrid.CheckEnum.Checked);
                    }
                    else
                    {
                        c1Transaction.SetCellCheck(rowIndex, COL_DX5_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                    }
                    if (LineTransactionData.Dx6Ptr)
                    {
                        c1Transaction.SetCellCheck(rowIndex, COL_DX6_PTR, C1.Win.C1FlexGrid.CheckEnum.Checked);
                    }
                    else
                    {
                        c1Transaction.SetCellCheck(rowIndex, COL_DX6_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                    }
                    if (LineTransactionData.Dx7Ptr)
                    {
                        c1Transaction.SetCellCheck(rowIndex, COL_DX7_PTR, C1.Win.C1FlexGrid.CheckEnum.Checked);
                    }
                    else
                    {
                        c1Transaction.SetCellCheck(rowIndex, COL_DX7_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                    }
                    if (LineTransactionData.Dx8Ptr)
                    {
                        c1Transaction.SetCellCheck(rowIndex, COL_DX8_PTR, C1.Win.C1FlexGrid.CheckEnum.Checked);
                    }
                    else
                    {
                        c1Transaction.SetCellCheck(rowIndex, COL_DX8_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                    }

                    if (LineTransactionData.EMG)
                    {
                        c1Transaction.SetCellCheck(rowIndex, COL_ISEMG, C1.Win.C1FlexGrid.CheckEnum.Checked);
                    }
                    else
                    {
                        c1Transaction.SetCellCheck(rowIndex, COL_ISEMG, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                    }


                    c1Transaction.SetData(rowIndex, COL_MOD1_CODE, LineTransactionData.Mod1Code.ToString());
                    c1Transaction.SetData(rowIndex, COL_MOD1_DESC, LineTransactionData.Mod1Description.ToString());
                    //SetCellNote(rowIndex, COL_MOD1_DESC);

                    c1Transaction.SetData(rowIndex, COL_MOD2_CODE, LineTransactionData.Mod2Code.ToString());
                    c1Transaction.SetData(rowIndex, COL_MOD2_DESC, LineTransactionData.Mod2Description.ToString());
                    //SetCellNote(rowIndex, COL_MOD2_DESC);

                    c1Transaction.SetData(rowIndex, COL_MOD3_CODE, LineTransactionData.Mod3Code.ToString());
                    c1Transaction.SetData(rowIndex, COL_MOD3_DESC, LineTransactionData.Mod3Description.ToString());
                    //SetCellNote(rowIndex, COL_MOD3_DESC);

                    c1Transaction.SetData(rowIndex, COL_MOD4_CODE, LineTransactionData.Mod4Code.ToString());
                    c1Transaction.SetData(rowIndex, COL_MOD4_DESC, LineTransactionData.Mod4Description.ToString());
                    //SetCellNote(rowIndex, COL_MOD4_DESC);

                    c1Transaction.SetData(rowIndex, COL_CHARGES, LineTransactionData.Charges.ToString());

                    //c1Transaction.SetData(rowIndex, COL_UNIT, LineTransactionData.Unit.ToString());
                    //c1Transaction.SetData(c1Transaction.RowSel, COL_UNIT, 1);
                    c1Transaction.SetData(c1Transaction.RowSel, COL_UNIT, gloCharges.FormatNumber(LineTransactionData.Unit));

                    c1Transaction.SetData(rowIndex, COL_TOTAL, LineTransactionData.Total.ToString());
                    c1Transaction.SetData(rowIndex, COL_ALLOWED, LineTransactionData.AllowedCharges.ToString());

                    //..**Code added on 20090624 By - Sagar Ghodke
                    //..**Code added to store the actual Allowed amount for single Unit for future calculations
                    //.. First set the allowed amount to the actual allowed check if the allowed charges & units
                    //.. are not zero 

                    c1Transaction.SetData(rowIndex, COL_ACTUAL_ALLOWED, LineTransactionData.AllowedCharges);

                    decimal _actualAllowed = 0;
                    if (LineTransactionData.AllowedCharges > 0 && LineTransactionData.Unit > 0)
                    {
                        _actualAllowed = (LineTransactionData.AllowedCharges / LineTransactionData.Unit);
                        c1Transaction.SetData(rowIndex, COL_ACTUAL_ALLOWED, _actualAllowed);
                    }


                    //..**End - Code added on 20090624 By - Sagar Ghodke

                    c1Transaction.SetData(rowIndex, COL_PROVIDER, LineTransactionData.RefferingProvider.ToString());

                    //20080918 Anil
                    //c1Transaction.SetData(rowIndex, COL_INSURANCEID,LineTransactionData.InsuranceID.ToString() );
                    //c1Transaction.SetData(rowIndex, COL_INSURANCENAME, LineTransactionData.InsuranceName.ToString());
                    //if (LineTransactionData.InsuranceSelfMode.GetHashCode() > 0)
                    //{
                    //    c1Transaction.SetData(rowIndex, COL_INSSELF_PAYMODE, LineTransactionData.InsuranceSelfMode.GetHashCode());
                    //    c1Transaction.SetData(rowIndex, COL_INSURANCENAME, LineTransactionData.InsuranceSelfMode.ToString());
                    //}

                    if (LineTransactionData.InsuranceSelfMode == PayerMode.Self)
                    {
                        c1Transaction.SetData(rowIndex, COL_INSURANCEID, LineTransactionData.InsuranceID.ToString());
                        c1Transaction.SetData(rowIndex, COL_INSURANCENAME, LineTransactionData.InsuranceSelfMode.ToString());
                        c1Transaction.SetData(rowIndex, COL_INSSELF_PAYMODE, LineTransactionData.InsuranceSelfMode.GetHashCode());
                    }
                    else if (LineTransactionData.InsuranceSelfMode == PayerMode.Insurance)
                    {
                        c1Transaction.SetData(rowIndex, COL_INSURANCEID, LineTransactionData.InsuranceID.ToString());
                        c1Transaction.SetData(rowIndex, COL_INSURANCENAME, LineTransactionData.InsuranceName.ToString());
                        c1Transaction.SetData(rowIndex, COL_INSSELF_PAYMODE, LineTransactionData.InsuranceSelfMode.GetHashCode());
                    }

                    //**

                    //Notes functionality to be implemented
                    //c1Transaction.SetData(rowIndex, COL_NOTE_DATA, LineTransactionData.Note.ToString());
                    //c1Transaction.SetData(rowIndex, COL_NOTE_TYPE, LineTransactionData.NoteOfType);
                    //

                    //** Code added on 16 Jan 2009 By - Sagar Ghodke
                    //To implement notes functionality
                    if (LineTransactionData.LineNotes != null && LineTransactionData.LineNotes.Count > 0)
                    {
                        for (int i = 0; i < LineTransactionData.LineNotes.Count; i++)
                        {
                            SetNotes(rowIndex, LineTransactionData.LineNotes[i]);
                        }
                    }
                    else
                    { c1Transaction.SetData(rowIndex, COL_NOTE_DATA, null); }
                    //** End Code add


                    //**Code added on 20090511 by - Sagar Ghodke
                    //**Code added to implement CLIA functionality

                    if (LineTransactionData.IsLabCPT == true)
                    {
                        c1Transaction.SetCellCheck(rowIndex, COL_ISLABCPT, CheckEnum.Checked);
                        c1Transaction.SetData(rowIndex, COL_AUTHORIZATIONNO, LineTransactionData.AuthorizationNo);
                    }
                    if (LineTransactionData.SendToClaim == true)
                    { c1Transaction.SetCellCheck(rowIndex, COL_SENTTOCLAIM, CheckEnum.Checked); }


                    //**End code add 20090511,Sagar Ghodke

                    if (LineTransactionData.LinePrimaryDxCode.Trim() != "")
                    { c1Transaction.SetData(rowIndex, COL_LINEPRIMARY_DXCODE, LineTransactionData.LinePrimaryDxCode.Trim()); }

                    if (LineTransactionData.LinePrimaryDxDesc.Trim() != "")
                    { c1Transaction.SetData(rowIndex, COL_LINEPRIMARY_DXDESC, LineTransactionData.LinePrimaryDxDesc.Trim()); }


                    if (LineTransactionData.IsHold == true)
                    {
                        c1Transaction.SetCellCheck(rowIndex, COL_HOLD, CheckEnum.Checked);
                        c1Transaction.SetData(rowIndex, COL_HOLD_REASON, LineTransactionData.HoldReason);
                    }

                    c1Transaction.SetData(rowIndex, COL_NDCCODE, LineTransactionData.NDCCode);
                    c1Transaction.SetData(rowIndex, COL_NDCUNIT, LineTransactionData.NDCUnit);
                    c1Transaction.SetData(rowIndex, COL_NDCUNITCODE, LineTransactionData.NDCUnitCode);
                    c1Transaction.SetData(rowIndex, COL_NDCUNITDESCRITION, LineTransactionData.NDCUnitDescription);
                    c1Transaction.SetData(rowIndex, COL_PRESCRIPTION, LineTransactionData.Prescription);
                    c1Transaction.SetData(rowIndex, COL_PRESCRIPTIONDESC, LineTransactionData.PrescriptionDescription);


                    #region " Show Total "

                    c1Total.SetData(0, COL_CHARGES, GetTotalCharges());
                    c1Total.SetData(0, COL_ALLOWED, GetTotalAllowed());
                    c1Total.SetData(0, COL_TOTAL, GetGrandTotal());

                    #endregion


                    _returnResult = true;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return _returnResult;
            }
            finally
            {
                if (LineTransactionData != null)
                {
                    LineTransactionData.Dispose();
                    LineTransactionData = null;
                }
            }
            return _returnResult;
        }

        public bool SetLineTransaction(string CPTCode, string CPTDesc, string Dx1Code, string Dx1Desc, string MOD1Code, string MOD1Desc, Decimal Unit, Int64 InsuranceId, string InsuranceName, int InsuranceSelfMode)
        {
            int rowIndex = 0;
            bool _returnResult = false;
            try
            {
                //if (CPTCode != "" || CPTDesc != "")
                //{
                rowIndex = GetLastDataRowNo();// c1Transaction.Rows.Count;
                if (rowIndex > c1Transaction.Rows.Count - 1)
                {
                    c1Transaction.Rows.Add();
                }

                c1Transaction.SetData(rowIndex, COL_NO, rowIndex);
                c1Transaction.SetData(rowIndex, COL_TRANSACTIONID, 0);

                c1Transaction.SetData(rowIndex, COL_DATEFROM, DateTime.Now.Date);


                //DOS To Implementation
                if (_showTilldateColumn == true)
                {
                    if (_showTillDateColumnUseNullDate == true)
                    {
                        c1Transaction.SetData(rowIndex, COL_DATETO, null);
                    }
                    else
                    {
                        c1Transaction.SetData(rowIndex, COL_DATETO, DateTime.Now.Date);
                    }
                }
                else
                {
                    c1Transaction.SetData(rowIndex, COL_DATETO, DateTime.Now.Date);
                }


                if (_DefaultRenderingProviderID > 0)
                {
                    c1Transaction.SetData(rowIndex, COL_PROVIDER_ID, _DefaultRenderingProviderID);
                    c1Transaction.SetData(rowIndex, COL_PROVIDER, _DefaultRenderringProviderName);
                }

                AddInsurance(rowIndex, InsuranceId, InsuranceName, InsuranceSelfMode);
                SetCurrencyCellValue(rowIndex);

                #region " Set Default TOS & POS "

                GetDefaultTOSPOS();
                c1Transaction.SetData(rowIndex, COL_TOSCODE, _DefaultTOSCode);
                c1Transaction.SetData(rowIndex, COL_TOSDESC, _DefaultTOSDesc);
                c1Transaction.SetData(rowIndex, COL_POSCODE, _DefaultPOSCode);
                c1Transaction.SetData(rowIndex, COL_POSDESC, _DefaultPOSDesc);

                #endregion

                c1Transaction.SetData(rowIndex, COL_CPT_CODE, CPTCode);
                c1Transaction.SetData(rowIndex, COL_CPT_DESC, CPTDesc);

                #region " Set Diagnosis "

                c1Transaction.SetData(rowIndex, COL_DX1_CODE, Dx1Code);
                c1Transaction.SetData(rowIndex, COL_DX1_DESC, Dx1Desc);
                c1Transaction.SetData(rowIndex, COL_DX2_CODE, "");
                c1Transaction.SetData(rowIndex, COL_DX2_DESC, "");
                c1Transaction.SetData(rowIndex, COL_DX3_CODE, "");
                c1Transaction.SetData(rowIndex, COL_DX3_DESC, "");
                c1Transaction.SetData(rowIndex, COL_DX4_CODE, "");
                c1Transaction.SetData(rowIndex, COL_DX4_DESC, "");
                c1Transaction.SetData(rowIndex, COL_DX5_CODE, "");
                c1Transaction.SetData(rowIndex, COL_DX5_DESC, "");
                c1Transaction.SetData(rowIndex, COL_DX6_CODE, "");
                c1Transaction.SetData(rowIndex, COL_DX6_DESC, "");
                c1Transaction.SetData(rowIndex, COL_DX7_CODE, "");
                c1Transaction.SetData(rowIndex, COL_DX7_DESC, "");
                c1Transaction.SetData(rowIndex, COL_DX8_CODE, "");
                c1Transaction.SetData(rowIndex, COL_DX8_DESC, "");

                #endregion

                #region " Set Diagnosis Pointer "

                if (Dx1Code != "")
                { c1Transaction.SetCellCheck(rowIndex, COL_DX1_PTR, C1.Win.C1FlexGrid.CheckEnum.Checked); }
                else { c1Transaction.SetCellCheck(rowIndex, COL_DX1_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked); }

                #region "Sagar Ghodke: Code review changes "
                //Unreachable "if" condition code is removed, see below

                //if (false)
                //{ c1Transaction.SetCellCheck(rowIndex, COL_DX2_PTR, C1.Win.C1FlexGrid.CheckEnum.Checked); }
                //else
                //{ c1Transaction.SetCellCheck(rowIndex, COL_DX2_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked); }

                //if (false)
                //{ c1Transaction.SetCellCheck(rowIndex, COL_DX3_PTR, C1.Win.C1FlexGrid.CheckEnum.Checked); }
                //else
                //{ c1Transaction.SetCellCheck(rowIndex, COL_DX3_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked); }

                //if (false)
                //{ c1Transaction.SetCellCheck(rowIndex, COL_DX4_PTR, C1.Win.C1FlexGrid.CheckEnum.Checked); }
                //else
                //{ c1Transaction.SetCellCheck(rowIndex, COL_DX4_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked); }

                //if (false)
                //{ c1Transaction.SetCellCheck(rowIndex, COL_DX5_PTR, C1.Win.C1FlexGrid.CheckEnum.Checked); }
                //else
                //{ c1Transaction.SetCellCheck(rowIndex, COL_DX5_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked); }

                //if (false)
                //{ c1Transaction.SetCellCheck(rowIndex, COL_DX6_PTR, C1.Win.C1FlexGrid.CheckEnum.Checked); }
                //else
                //{ c1Transaction.SetCellCheck(rowIndex, COL_DX6_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked); }

                //if (false)
                //{ c1Transaction.SetCellCheck(rowIndex, COL_DX7_PTR, C1.Win.C1FlexGrid.CheckEnum.Checked); }
                //else
                //{ c1Transaction.SetCellCheck(rowIndex, COL_DX7_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked); }

                //if (false)
                //{ c1Transaction.SetCellCheck(rowIndex, COL_DX8_PTR, C1.Win.C1FlexGrid.CheckEnum.Checked); }
                //else
                //{ c1Transaction.SetCellCheck(rowIndex, COL_DX8_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked); }


                c1Transaction.SetCellCheck(rowIndex, COL_DX2_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                c1Transaction.SetCellCheck(rowIndex, COL_DX3_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                c1Transaction.SetCellCheck(rowIndex, COL_DX4_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                c1Transaction.SetCellCheck(rowIndex, COL_DX5_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                c1Transaction.SetCellCheck(rowIndex, COL_DX6_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                c1Transaction.SetCellCheck(rowIndex, COL_DX7_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                c1Transaction.SetCellCheck(rowIndex, COL_DX8_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);

                #endregion "Sagar Ghodke: Code review changes "

                #endregion

                #region " Set Modifiers "

                c1Transaction.SetData(rowIndex, COL_MOD1_CODE, MOD1Code);
                c1Transaction.SetData(rowIndex, COL_MOD1_DESC, MOD1Desc);
                c1Transaction.SetData(rowIndex, COL_MOD2_CODE, "");
                c1Transaction.SetData(rowIndex, COL_MOD2_DESC, "");
                c1Transaction.SetData(rowIndex, COL_MOD3_CODE, "");
                c1Transaction.SetData(rowIndex, COL_MOD3_DESC, "");
                c1Transaction.SetData(rowIndex, COL_MOD4_CODE, "");
                c1Transaction.SetData(rowIndex, COL_MOD4_DESC, "");

                #endregion

                if (Unit > 0)
                {
                    //c1Transaction.SetData(rowIndex, COL_UNIT, Unit);
                    //c1Transaction.SetData(rowIndex, COL_UNIT, 1);
                    c1Transaction.SetData(rowIndex, COL_UNIT, gloCharges.FormatNumber(Unit));
                }

                c1Transaction.SetData(rowIndex, COL_ISLABCPT, false);
                c1Transaction.SetData(rowIndex, COL_ISEMG, false);
                c1Transaction.SetData(rowIndex, COL_AUTHORIZATIONNO, "");
                c1Transaction.SetData(rowIndex, COL_SENTTOCLAIM, true);

                _returnResult = true;
                //}
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return _returnResult;
            }
            finally
            {
            }
            return _returnResult;
        }

        public bool SetLineTransaction(TransactionLines LineTransactionsData)
        {
            int rowIndex = 0;
            bool _returnResult = false;
            TransactionLine LineTransactionData = null;
            ClsFeeSchedule oclsfeeShedule = null;


            try
            {
                if (LineTransactionsData != null)
                {
                    if (LineTransactionsData.Count > 0)
                    {
                        for (int i = 0; i < LineTransactionsData.Count; i++)
                        {
                            // LineTransactionData = new TransactionLine();
                            LineTransactionData = LineTransactionsData[i];

                            rowIndex = c1Transaction.Rows.Count;
                            c1Transaction.Rows.Add();

                            #region " Set Transaction Id's "

                            c1Transaction.SetData(rowIndex, COL_NO, LineTransactionData.TransactionLineId);
                            c1Transaction.SetData(rowIndex, COL_EMRTREATMENTLINENO, LineTransactionData.EMRTreatmentLineNo);
                            c1Transaction.SetData(rowIndex, COL_TRANSACTIONID, LineTransactionData.TransactionId);
                            c1Transaction.SetData(rowIndex, COL_TRANSACTION_DETAIL_ID, LineTransactionData.TransactionDetailID);

                            c1Transaction.SetData(rowIndex, COL_MST_TRANSACTIONID, LineTransactionData.TransactionMasterID);
                            c1Transaction.SetData(rowIndex, COL_MST_TRANSACTIONDTLID, LineTransactionData.TransactionMasterDetailID);

                            #endregion " Set Transaction Id's "

                            #region " Set Date Of Service "

                            c1Transaction.SetData(rowIndex, COL_DATEFROM, LineTransactionData.DateServiceFrom.ToString("dd-MMM-yy"));

                            if (_showTilldateColumn == true)
                            {
                                if (_showTillDateColumnUseNullDate == true)
                                {
                                    if (LineTransactionData.DateServiceTillIsNull == true)
                                    {
                                        c1Transaction.SetData(rowIndex, COL_DATETO, null);
                                    }
                                    else
                                    {
                                        c1Transaction.SetData(rowIndex, COL_DATETO, LineTransactionData.DateServiceTill.ToString("dd-MMM-yy"));
                                    }
                                }
                                else
                                {
                                    c1Transaction.SetData(rowIndex, COL_DATETO, LineTransactionData.DateServiceTill.ToString("dd-MMM-yy"));
                                }
                            }
                            else
                            {
                                c1Transaction.SetData(rowIndex, COL_DATETO, LineTransactionData.DateServiceTill.ToString("dd-MMM-yy"));
                            }

                            #endregion " Set Date Of Service "

                            #region " Set POS & TOS "

                            c1Transaction.SetData(rowIndex, COL_POSCODE, LineTransactionData.POSCode.ToString());
                            c1Transaction.SetData(rowIndex, COL_POSDESC, LineTransactionData.POSDescription.ToString());

                            c1Transaction.SetData(rowIndex, COL_TOSCODE, LineTransactionData.TOSCode.ToString());
                            c1Transaction.SetData(rowIndex, COL_TOSDESC, LineTransactionData.TOSDescription.ToString());

                            #endregion

                            #region " Set Claim CPT & Diagnosis "

                            c1Transaction.SetData(rowIndex, COL_CPT_CODE, LineTransactionData.CPTCode.ToString());
                            c1Transaction.SetData(rowIndex, COL_CPT_DESC, LineTransactionData.CPTDescription.ToString());
                            //SetCellNote(rowIndex, COL_CPT_DESC);

                            c1Transaction.SetData(rowIndex, COL_DX1_CODE, LineTransactionData.Dx1Code.ToString());
                            c1Transaction.SetData(rowIndex, COL_DX1_DESC, LineTransactionData.Dx1Description.ToString());
                            //SetCellNote(rowIndex, COL_DX1_DESC);

                            c1Transaction.SetData(rowIndex, COL_DX2_CODE, LineTransactionData.Dx2Code.ToString());
                            c1Transaction.SetData(rowIndex, COL_DX2_DESC, LineTransactionData.Dx2Description.ToString());
                            //SetCellNote(rowIndex, COL_DX2_DESC);

                            c1Transaction.SetData(rowIndex, COL_DX3_CODE, LineTransactionData.Dx3Code.ToString());
                            c1Transaction.SetData(rowIndex, COL_DX3_DESC, LineTransactionData.Dx3Description.ToString());
                            //SetCellNote(rowIndex, COL_DX3_DESC);

                            c1Transaction.SetData(rowIndex, COL_DX4_CODE, LineTransactionData.Dx4Code.ToString());
                            c1Transaction.SetData(rowIndex, COL_DX4_DESC, LineTransactionData.Dx4Description.ToString());
                            //SetCellNote(rowIndex, COL_DX4_DESC);

                            c1Transaction.SetData(rowIndex, COL_DX5_CODE, LineTransactionData.Dx5Code.ToString());
                            c1Transaction.SetData(rowIndex, COL_DX5_DESC, LineTransactionData.Dx5Description.ToString());
                            //SetCellNote(rowIndex, COL_DX5_DESC);

                            c1Transaction.SetData(rowIndex, COL_DX6_CODE, LineTransactionData.Dx6Code.ToString());
                            c1Transaction.SetData(rowIndex, COL_DX6_DESC, LineTransactionData.Dx6Description.ToString());
                            //SetCellNote(rowIndex, COL_DX6_DESC);

                            c1Transaction.SetData(rowIndex, COL_DX7_CODE, LineTransactionData.Dx7Code.ToString());
                            c1Transaction.SetData(rowIndex, COL_DX7_DESC, LineTransactionData.Dx7Description.ToString());
                            //SetCellNote(rowIndex, COL_DX7_DESC);

                            c1Transaction.SetData(rowIndex, COL_DX8_CODE, LineTransactionData.Dx8Code.ToString());
                            c1Transaction.SetData(rowIndex, COL_DX8_DESC, LineTransactionData.Dx8Description.ToString());
                            //SetCellNote(rowIndex, COL_DX8_DESC);

                            #endregion

                            #region " Set Diagnosis Pointer "

                            if (LineTransactionData.Dx1Ptr)
                            {
                                c1Transaction.SetCellCheck(rowIndex, COL_DX1_PTR, C1.Win.C1FlexGrid.CheckEnum.Checked);
                            }
                            else
                            {
                                c1Transaction.SetCellCheck(rowIndex, COL_DX1_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                            }

                            if (LineTransactionData.Dx2Ptr)
                            {
                                c1Transaction.SetCellCheck(rowIndex, COL_DX2_PTR, C1.Win.C1FlexGrid.CheckEnum.Checked);
                            }
                            else
                            {
                                c1Transaction.SetCellCheck(rowIndex, COL_DX2_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                            }

                            if (LineTransactionData.Dx3Ptr)
                            {
                                c1Transaction.SetCellCheck(rowIndex, COL_DX3_PTR, C1.Win.C1FlexGrid.CheckEnum.Checked);
                            }
                            else
                            {
                                c1Transaction.SetCellCheck(rowIndex, COL_DX3_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                            }

                            if (LineTransactionData.Dx4Ptr)
                            {
                                c1Transaction.SetCellCheck(rowIndex, COL_DX4_PTR, C1.Win.C1FlexGrid.CheckEnum.Checked);
                            }
                            else
                            {
                                c1Transaction.SetCellCheck(rowIndex, COL_DX4_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                            }
                            if (LineTransactionData.Dx5Ptr)
                            {
                                c1Transaction.SetCellCheck(rowIndex, COL_DX5_PTR, C1.Win.C1FlexGrid.CheckEnum.Checked);
                            }
                            else
                            {
                                c1Transaction.SetCellCheck(rowIndex, COL_DX5_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                            }
                            if (LineTransactionData.Dx6Ptr)
                            {
                                c1Transaction.SetCellCheck(rowIndex, COL_DX6_PTR, C1.Win.C1FlexGrid.CheckEnum.Checked);
                            }
                            else
                            {
                                c1Transaction.SetCellCheck(rowIndex, COL_DX6_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                            }
                            if (LineTransactionData.Dx7Ptr)
                            {
                                c1Transaction.SetCellCheck(rowIndex, COL_DX7_PTR, C1.Win.C1FlexGrid.CheckEnum.Checked);
                            }
                            else
                            {
                                c1Transaction.SetCellCheck(rowIndex, COL_DX7_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                            }
                            if (LineTransactionData.Dx8Ptr)
                            {
                                c1Transaction.SetCellCheck(rowIndex, COL_DX8_PTR, C1.Win.C1FlexGrid.CheckEnum.Checked);
                            }
                            else
                            {
                                c1Transaction.SetCellCheck(rowIndex, COL_DX8_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                            }

                            #endregion

                            if (LineTransactionData.EMG)
                            {
                                c1Transaction.SetCellCheck(rowIndex, COL_ISEMG, C1.Win.C1FlexGrid.CheckEnum.Checked);
                            }
                            else
                            {
                                c1Transaction.SetCellCheck(rowIndex, COL_ISEMG, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                            }


                            #region " Set Claim Modifiers "

                            c1Transaction.SetData(rowIndex, COL_MOD1_CODE, LineTransactionData.Mod1Code.ToString());
                            c1Transaction.SetData(rowIndex, COL_MOD1_DESC, LineTransactionData.Mod1Description.ToString());
                            //SetCellNote(rowIndex, COL_MOD1_DESC);

                            c1Transaction.SetData(rowIndex, COL_MOD2_CODE, LineTransactionData.Mod2Code.ToString());
                            c1Transaction.SetData(rowIndex, COL_MOD2_DESC, LineTransactionData.Mod2Description.ToString());
                            //SetCellNote(rowIndex, COL_MOD2_DESC);

                            c1Transaction.SetData(rowIndex, COL_MOD3_CODE, LineTransactionData.Mod3Code.ToString());
                            c1Transaction.SetData(rowIndex, COL_MOD3_DESC, LineTransactionData.Mod3Description.ToString());
                            //SetCellNote(rowIndex, COL_MOD3_DESC);

                            c1Transaction.SetData(rowIndex, COL_MOD4_CODE, LineTransactionData.Mod4Code.ToString());
                            c1Transaction.SetData(rowIndex, COL_MOD4_DESC, LineTransactionData.Mod4Description.ToString());
                            //SetCellNote(rowIndex, COL_MOD4_DESC);

                            #endregion

                            c1Transaction.SetData(rowIndex, COL_CHARGES, LineTransactionData.Charges.ToString());
                            c1Transaction.SetData(rowIndex, COL_INSURANCEID, LineTransactionData.InsuranceID.ToString());

                            //c1Transaction.SetData(rowIndex, COL_UNIT, LineTransactionData.Unit.ToString());
                            //c1Transaction.SetData(rowIndex, COL_UNIT, 1);
                            c1Transaction.SetData(rowIndex, COL_UNIT, gloCharges.FormatNumber(LineTransactionData.Unit));

                            c1Transaction.SetData(rowIndex, COL_TOTAL, LineTransactionData.Total.ToString());
                            // c1Transaction.SetData(rowIndex, COL_ALLOWED, LineTransactionData.AllowedCharges.ToString());
                            c1Transaction.SetData(rowIndex, COL_BILLEDAMOUNT, LineTransactionData.BilledAmount.ToString());
                            //..**Code added on 20090624 By - Sagar Ghodke
                            //..**Code added to store the actual Allowed amount for single Unit for future calculations
                            //.. First set the allowed amount to the actual allowed check if the allowed charges & units
                            //.. are not zero 

                            c1Transaction.SetData(rowIndex, COL_ACTUAL_ALLOWED, LineTransactionData.AllowedCharges);

                            decimal _actualAllowed = 0;
                            if (LineTransactionData.AllowedCharges > 0 && LineTransactionData.Unit > 0)
                            {
                                _actualAllowed = (LineTransactionData.AllowedCharges / LineTransactionData.Unit);
                                c1Transaction.SetData(rowIndex, COL_ACTUAL_ALLOWED, _actualAllowed);
                            }
                            //..**End - Code added on 20090624 By - Sagar Ghodke


                            //c1Transaction.SetData(rowIndex, COL_PROVIDER, LineTransactionData.RefferingProvider.ToString());
                            if (LineTransactionData.RefferingProviderId > 0)
                            {
                                string _RefferringProvider = "";
                                gloBilling ogloBilling = new gloBilling(_DatabaseConnectionString, "");
                                _RefferringProvider = ogloBilling.GetProvider(LineTransactionData.RefferingProviderId);
                                c1Transaction.SetData(rowIndex, COL_PROVIDER_ID, LineTransactionData.RefferingProviderId);
                                c1Transaction.SetData(rowIndex, COL_PROVIDER, _RefferringProvider);
                                ogloBilling.Dispose();
                                _RefferringProvider = null;
                            }

                            //20080918 Anil
                            //c1Transaction.SetData(rowIndex, COL_INSURANCEID, LineTransactionData.InsuranceID.ToString());
                            //c1Transaction.SetData(rowIndex, COL_INSURANCENAME, LineTransactionData.InsuranceName.ToString());
                            //if (LineTransactionData.InsuranceSelfMode.GetHashCode() > 0)
                            //{
                            //    c1Transaction.SetData(rowIndex, COL_INSSELF_PAYMODE, LineTransactionData.InsuranceSelfMode.GetHashCode());
                            //    c1Transaction.SetData(rowIndex, COL_INSURANCENAME, LineTransactionData.InsuranceSelfMode.ToString());
                            //}

                            if (LineTransactionData.InsuranceSelfMode == PayerMode.Self)
                            {
                                c1Transaction.SetData(rowIndex, COL_INSURANCEID, LineTransactionData.InsuranceID.ToString());
                                c1Transaction.SetData(rowIndex, COL_INSURANCENAME, LineTransactionData.InsuranceSelfMode.ToString());
                                c1Transaction.SetData(rowIndex, COL_INSSELF_PAYMODE, LineTransactionData.InsuranceSelfMode.GetHashCode());
                            }
                            else if (LineTransactionData.InsuranceSelfMode == PayerMode.Insurance)
                            {
                                c1Transaction.SetData(rowIndex, COL_INSURANCEID, LineTransactionData.InsuranceID.ToString());
                                c1Transaction.SetData(rowIndex, COL_INSURANCENAME, LineTransactionData.InsuranceName.ToString());
                                c1Transaction.SetData(rowIndex, COL_INSSELF_PAYMODE, LineTransactionData.InsuranceSelfMode.GetHashCode());
                            }
                            //**

                            //** Code added on 16 Jan 2009 By Sagar Ghodke
                            //..To implement Notes functionality
                            if (LineTransactionData.LineNotes != null && LineTransactionData.LineNotes.Count > 0)
                            {
                                for (int noteIndex = 0; noteIndex < LineTransactionData.LineNotes.Count; noteIndex++)
                                {
                                    SetNotes(rowIndex, LineTransactionData.LineNotes[noteIndex]);
                                }

                            }
                            else
                            { c1Transaction.SetData(rowIndex, COL_NOTE_DATA, null); }


                            //**Code added on 20090511 by - Sagar Ghodke
                            //**Code added to implement CLIA functionality

                            if (LineTransactionData.IsLabCPT == true)
                            {
                                c1Transaction.SetCellCheck(rowIndex, COL_ISLABCPT, CheckEnum.Checked);
                                c1Transaction.SetData(rowIndex, COL_AUTHORIZATIONNO, LineTransactionData.AuthorizationNo);
                            }
                            if (LineTransactionData.SendToClaim == true)
                            { c1Transaction.SetCellCheck(rowIndex, COL_SENTTOCLAIM, CheckEnum.Checked); }


                            //**End code add 20090511,Sagar Ghodke

                            if (LineTransactionData.LinePrimaryDxCode.Trim() != "")
                            { c1Transaction.SetData(rowIndex, COL_LINEPRIMARY_DXCODE, LineTransactionData.LinePrimaryDxCode.Trim()); }

                            if (LineTransactionData.LinePrimaryDxDesc.Trim() != "")
                            { c1Transaction.SetData(rowIndex, COL_LINEPRIMARY_DXDESC, LineTransactionData.LinePrimaryDxDesc.Trim()); }

                            if (LineTransactionData.IsHold == true)
                            {
                                c1Transaction.SetCellCheck(rowIndex, COL_HOLD, CheckEnum.Checked);
                                c1Transaction.SetData(rowIndex, COL_HOLD_REASON, LineTransactionData.HoldReason);
                            }

                            c1Transaction.SetData(rowIndex, COL_NDCCODE, LineTransactionData.NDCCode);
                            c1Transaction.SetData(rowIndex, COL_NDCUNIT, LineTransactionData.NDCUnit);
                            c1Transaction.SetData(rowIndex, COL_NDCUNITCODE, LineTransactionData.NDCUnitCode);
                            c1Transaction.SetData(rowIndex, COL_NDCUNITDESCRITION, LineTransactionData.NDCUnitDescription);
                            c1Transaction.SetData(rowIndex, COL_PRESCRIPTION, LineTransactionData.Prescription);
                            c1Transaction.SetData(rowIndex, COL_PRESCRIPTIONDESC, LineTransactionData.PrescriptionDescription);


                            #region "Anesthesia"

                            c1Transaction.SetData(rowIndex, COL_ANES_ISANESTHESIA, LineTransactionData.bIsAneshtesia);
                            c1Transaction.SetData(rowIndex, COL_ANES_ID, LineTransactionData.AnesthesiaID);
                            c1Transaction.SetData(rowIndex, COL_ANES_STARTDATE, LineTransactionData.AnesthesiaStartTime);
                            c1Transaction.SetData(rowIndex, COL_ANES_ENDDATE, LineTransactionData.AnesthesiaEndTime);
                            c1Transaction.SetData(rowIndex, COL_ANES_TOTALMIN, LineTransactionData.AnesthesiaTotalMinutes);


                            c1Transaction.SetData(rowIndex, COL_ANES_MINPERUNIT, LineTransactionData.AnesthesiaMinPerUnit);
                            c1Transaction.SetData(rowIndex, COL_ANES_TIMEUNITS, LineTransactionData.AnesthesiaTimeUnits);
                            c1Transaction.SetData(rowIndex, COL_ANES_BASEUNITS, LineTransactionData.AnesthesiaBaseUnits);
                            c1Transaction.SetData(rowIndex, COL_ANES_OTHERUNITS, LineTransactionData.AnesthesiaOtherUnits);
                            c1Transaction.SetData(rowIndex, COL_ANES_TOTALUNITS, LineTransactionData.AnesthesiaTotalUnits);
                            c1Transaction.SetData(rowIndex, COL_ANES_ISAUTOCALCULATE, LineTransactionData.bIsAutoCalculateAnesthesia);

                            #endregion


                            c1Transaction.SetData(rowIndex, COL_SERVICESCREENING, LineTransactionData.ServiceIsTheScreening);
                            c1Transaction.SetData(rowIndex, COL_SERVICERESULTSCREENING, LineTransactionData.ServiceIsTheResultOfScreening);
                            c1Transaction.SetData(rowIndex, COL_FAMILYPLANNINGINDICATOR, LineTransactionData.ServiceFamilyPlanningIndicator);


                            //if (c1Transaction.GetData(rowIndex, COL_NDCCODE) != null && c1Transaction.GetData(rowIndex, COL_NDCCODE).ToString() != "")
                            //    SetEPSDTNotesNDCCodeFlag(rowIndex);
                            //else
                            SetEPSDTNotesNDCCodeFlag(rowIndex);
                            decimal _AllowedCharges;
                            bool _isAllowedAmount = false;
                            oclsfeeShedule = new ClsFeeSchedule(_DatabaseConnectionString);
                            String sMode = "";
                            if (c1Transaction.GetData(rowIndex, COL_MOD1_CODE) != null && Convert.ToString(c1Transaction.GetData(rowIndex, COL_MOD1_CODE)).Length > 0)
                            {
                                sMode = Convert.ToString(c1Transaction.GetData(rowIndex, COL_MOD1_CODE));
                            }
                            if (c1Transaction.GetData(rowIndex, COL_MOD2_CODE) != null && Convert.ToString(c1Transaction.GetData(rowIndex, COL_MOD2_CODE)).Length > 0)
                            {
                                if (sMode.Length == 0)
                                    sMode = Convert.ToString(c1Transaction.GetData(rowIndex, COL_MOD2_CODE));
                                else
                                    sMode = sMode + "," + Convert.ToString(c1Transaction.GetData(rowIndex, COL_MOD2_CODE));
                            }
                            if (c1Transaction.GetData(rowIndex, COL_MOD3_CODE) != null && Convert.ToString(c1Transaction.GetData(rowIndex, COL_MOD3_CODE)).Length > 0)
                            {
                                if (sMode.Length == 0)
                                    sMode = Convert.ToString(c1Transaction.GetData(rowIndex, COL_MOD3_CODE));
                                else
                                    sMode = sMode + "," + Convert.ToString(c1Transaction.GetData(rowIndex, COL_MOD3_CODE));
                            }
                            if (c1Transaction.GetData(rowIndex, COL_MOD4_CODE) != null && Convert.ToString(c1Transaction.GetData(rowIndex, COL_MOD4_CODE)).Length > 0)
                            {
                                if (sMode.Length == 0)
                                    sMode = Convert.ToString(c1Transaction.GetData(rowIndex, COL_MOD4_CODE));
                                else
                                    sMode = sMode + "," + Convert.ToString(c1Transaction.GetData(rowIndex, COL_MOD4_CODE));
                            }
                            _AllowedCharges = oclsfeeShedule.GetAllowedAmountForCharges(_nContactID, LineTransactionData.TransactionMasterDetailID, LineTransactionData.CPTCode, sMode, DefaultChargesType.GetHashCode(), ref _isAllowedAmount, gloDateMaster.gloDate.DateAsNumber(LineTransactionData.DateServiceTill.ToString()));
                            if (_isAllowedAmount)
                                c1Transaction.SetData(rowIndex, COL_ALLOWED, _AllowedCharges * LineTransactionData.Unit);
                            LineTransactionData.AllowedCharges = _AllowedCharges;
                            //Bug #87335: 00000965 : Claim line shows payment note of other claim line
                            if (i == 0)
                            {
                                _transactionDetailId = LineTransactionData.TransactionDetailID;
                                _transactionLineNo = LineTransactionData.TransactionLineId;
                            }

                            //Dispose the Temporary Object
                            if (LineTransactionData != null)
                            {
                                LineTransactionData.Dispose();
                                LineTransactionData = null;
                            }



                        }//end - for (int i = 0; i < LineTransactionsData.Count; i++)

                        //Start Sagar G: 23Apr2014, Code not in use commented
                        //CellNotes.CellNoteManager mgr = new CellNotes.CellNoteManager(c1Transaction);
                        //End Sagar G: 23Apr2014, Code not in use commented


                        _returnResult = true;

                        #region " Show Total "

                        c1Total.SetData(0, COL_CHARGES, GetTotalCharges());
                        c1Total.SetData(0, COL_ALLOWED, GetTotalAllowed());
                        c1Total.SetData(0, COL_TOTAL, GetGrandTotal());

                        #endregion

                    } //end - if (LineTransactionsData.Count > 0)

                }// end - if (LineTransactionsData != null)
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return _returnResult;
            }
            finally
            {
                //SLR: Why are we freeing here. Instead of it should be freed only after the function call.
                //if (LineTransactionsData != null)
                //{
                //    LineTransactionsData.Dispose();
                //    LineTransactionsData = null;
                //}
                if (oclsfeeShedule != null)
                {
                    oclsfeeShedule.Dispose();
                    oclsfeeShedule = null;
                }
            }
            SortControl();
            return _returnResult;
        }

        //private void SetCellNote(int rowIndex, int colIndex)
        //{
        //    CellNote oNote;
        //    CellRange rg;
        //    string _Note = "";

        //    try
        //    {
        //        _Note = Convert.ToString(c1Transaction.GetData(rowIndex, colIndex));
        //        if (_Note.Trim() != "")
        //        {
        //            oNote = new CellNote(_Note);
        //            rg = c1Transaction.GetCellRange(rowIndex, colIndex - 1);
        //            rg.UserData = oNote;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
        //        ex = null;
        //    }
        //}

        public bool SetEMRExamLineTransaction(TransactionLines LineTransactionsData)
        {
            int rowIndex = 0;
            bool _returnResult = false;
            TransactionLine LineTransactionData = null;
            ClsFeeSchedule oclsFeeSchedule = null;
            #region "Custom Event"
            TrnCtrlColValChangeEventArg e2 = new TrnCtrlColValChangeEventArg();
            RowColEventArgs e1 = null;
            DataTable _dtProvider = null;
            #endregion

            try
            {
                if (LineTransactionsData != null)
                {
                    if (LineTransactionsData.Count > 0)
                    {
                        _dtProvider = gloGlobal.gloPMMasters.GetProviders();
                        oclsFeeSchedule = new ClsFeeSchedule(_DatabaseConnectionString);
                        if (c1Transaction.Rows.Count > 1)
                        {
                            c1Transaction.Rows.Remove(1);
                        }

                        for (int i = 0; i < LineTransactionsData.Count; i++)
                        {
                            rowIndex = i + 1;
                            //if (!rowIndex.Equals(1))
                            //{
                            c1Transaction.Rows.Add();
                            //}
                            c1Transaction.RowSel = rowIndex;
                            #region " Set Service Line "

                            //LineTransactionData = new TransactionLine();
                            LineTransactionData = LineTransactionsData[i];



                            c1Transaction.SetData(rowIndex, COL_NO, LineTransactionData.TransactionLineId);
                            c1Transaction.SetData(rowIndex, COL_EMRTREATMENTLINENO, LineTransactionData.EMRTreatmentLineNo);
                            c1Transaction.SetData(rowIndex, COL_TRANSACTIONID, LineTransactionData.TransactionId);
                            c1Transaction.SetData(rowIndex, COL_TRANSACTION_DETAIL_ID, LineTransactionData.TransactionDetailID);

                            c1Transaction.SetData(rowIndex, COL_DATEFROM, LineTransactionData.DateServiceFrom.ToString("dd-MMM-yy"));

                            //Commented on 24th Jan 2010
                            //Should fill Blank while Loading EMR Treatment.
                            //c1Transaction.SetData(rowIndex, COL_DATETO, LineTransactionData.DateServiceTill.ToString("dd-MMM-yy"));
                            //*****

                            c1Transaction.SetData(rowIndex, COL_POSCODE, _DefaultPOSCode);  //LineTransactionData.POSCode.ToString());
                            c1Transaction.SetData(rowIndex, COL_POSDESC, _DefaultPOSDesc);  // LineTransactionData.POSDescription.ToString());

                            c1Transaction.SetData(rowIndex, COL_TOSCODE, _DefaultTOSCode); //LineTransactionData.TOSCode.ToString());
                            c1Transaction.SetData(rowIndex, COL_TOSDESC, _DefaultTOSDesc);   //LineTransactionData.TOSDescription.ToString());

                            c1Transaction.SetData(rowIndex, COL_CPT_CODE, LineTransactionData.CPTCode.ToString());
                            c1Transaction.SetData(rowIndex, COL_CPT_DESC, LineTransactionData.CPTDescription.ToString());
                            if (LineTransactionData.CPTCode.Trim() != "")
                            {
                                e2 = new TrnCtrlColValChangeEventArg();
                                e2.code = LineTransactionData.CPTCode;
                                e2.description = LineTransactionData.CPTDescription;
                                e2.oType = TransactionLineColumnType.CPT;
                                e2.isdeleted = false;
                                onInsCPTDxMod_Changed(null, e1, e2);
                            }

                            c1Transaction.SetData(rowIndex, COL_DX1_CODE, LineTransactionData.Dx1Code.ToString());
                            c1Transaction.SetData(rowIndex, COL_DX1_DESC, LineTransactionData.Dx1Description.ToString());
                            if (LineTransactionData.Dx1Code.Trim() != "")
                            {
                                e2 = new TrnCtrlColValChangeEventArg();
                                e2.code = LineTransactionData.Dx1Code;
                                e2.description = LineTransactionData.Dx1Description;
                                e2.oType = TransactionLineColumnType.Diagnosis;
                                e2.isdeleted = false;
                                onInsCPTDxMod_Changed(null, e1, e2);
                            }
                            c1Transaction.SetData(rowIndex, COL_DX2_CODE, LineTransactionData.Dx2Code.ToString());
                            c1Transaction.SetData(rowIndex, COL_DX2_DESC, LineTransactionData.Dx2Description.ToString());
                            if (LineTransactionData.Dx2Code.Trim() != "")
                            {
                                e2 = new TrnCtrlColValChangeEventArg();
                                e2.code = LineTransactionData.Dx2Code;
                                e2.description = LineTransactionData.Dx2Description;
                                e2.oType = TransactionLineColumnType.Diagnosis;
                                e2.isdeleted = false;
                                onInsCPTDxMod_Changed(null, e1, e2);
                            }
                            c1Transaction.SetData(rowIndex, COL_DX3_CODE, LineTransactionData.Dx3Code.ToString());
                            c1Transaction.SetData(rowIndex, COL_DX3_DESC, LineTransactionData.Dx3Description.ToString());
                            if (LineTransactionData.Dx3Code.Trim() != "")
                            {
                                e2 = new TrnCtrlColValChangeEventArg();
                                e2.code = LineTransactionData.Dx3Code;
                                e2.description = LineTransactionData.Dx3Description;
                                e2.oType = TransactionLineColumnType.Diagnosis;
                                e2.isdeleted = false;
                                onInsCPTDxMod_Changed(null, e1, e2);
                            }
                            c1Transaction.SetData(rowIndex, COL_DX4_CODE, LineTransactionData.Dx4Code.ToString());
                            c1Transaction.SetData(rowIndex, COL_DX4_DESC, LineTransactionData.Dx4Description.ToString());
                            if (LineTransactionData.Dx4Code.Trim() != "")
                            {
                                e2 = new TrnCtrlColValChangeEventArg();
                                e2.code = LineTransactionData.Dx4Code;
                                e2.description = LineTransactionData.Dx4Description;
                                e2.oType = TransactionLineColumnType.Diagnosis;
                                e2.isdeleted = false;
                                onInsCPTDxMod_Changed(null, e1, e2);
                            }
                            c1Transaction.SetData(rowIndex, COL_DX5_CODE, LineTransactionData.Dx5Code.ToString());
                            c1Transaction.SetData(rowIndex, COL_DX5_DESC, LineTransactionData.Dx5Description.ToString());
                            c1Transaction.SetData(rowIndex, COL_DX6_CODE, LineTransactionData.Dx6Code.ToString());
                            c1Transaction.SetData(rowIndex, COL_DX6_DESC, LineTransactionData.Dx6Description.ToString());
                            c1Transaction.SetData(rowIndex, COL_DX7_CODE, LineTransactionData.Dx7Code.ToString());
                            c1Transaction.SetData(rowIndex, COL_DX7_DESC, LineTransactionData.Dx7Description.ToString());
                            c1Transaction.SetData(rowIndex, COL_DX8_CODE, LineTransactionData.Dx8Code.ToString());
                            c1Transaction.SetData(rowIndex, COL_DX8_DESC, LineTransactionData.Dx8Description.ToString());


                            if (LineTransactionData.Dx1Ptr)
                            {
                                c1Transaction.SetCellCheck(rowIndex, COL_DX1_PTR, C1.Win.C1FlexGrid.CheckEnum.Checked);
                            }
                            else
                            {
                                c1Transaction.SetCellCheck(rowIndex, COL_DX1_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                            }

                            if (LineTransactionData.Dx2Ptr)
                            {
                                c1Transaction.SetCellCheck(rowIndex, COL_DX2_PTR, C1.Win.C1FlexGrid.CheckEnum.Checked);
                            }
                            else
                            {
                                c1Transaction.SetCellCheck(rowIndex, COL_DX2_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                            }

                            if (LineTransactionData.Dx3Ptr)
                            {
                                c1Transaction.SetCellCheck(rowIndex, COL_DX3_PTR, C1.Win.C1FlexGrid.CheckEnum.Checked);
                            }
                            else
                            {
                                c1Transaction.SetCellCheck(rowIndex, COL_DX3_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                            }

                            if (LineTransactionData.Dx4Ptr)
                            {
                                c1Transaction.SetCellCheck(rowIndex, COL_DX4_PTR, C1.Win.C1FlexGrid.CheckEnum.Checked);
                            }
                            else
                            {
                                c1Transaction.SetCellCheck(rowIndex, COL_DX4_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                            }
                            if (LineTransactionData.Dx5Ptr)
                            {
                                c1Transaction.SetCellCheck(rowIndex, COL_DX5_PTR, C1.Win.C1FlexGrid.CheckEnum.Checked);
                            }
                            else
                            {
                                c1Transaction.SetCellCheck(rowIndex, COL_DX5_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                            }
                            if (LineTransactionData.Dx6Ptr)
                            {
                                c1Transaction.SetCellCheck(rowIndex, COL_DX6_PTR, C1.Win.C1FlexGrid.CheckEnum.Checked);
                            }
                            else
                            {
                                c1Transaction.SetCellCheck(rowIndex, COL_DX6_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                            }
                            if (LineTransactionData.Dx7Ptr)
                            {
                                c1Transaction.SetCellCheck(rowIndex, COL_DX7_PTR, C1.Win.C1FlexGrid.CheckEnum.Checked);
                            }
                            else
                            {
                                c1Transaction.SetCellCheck(rowIndex, COL_DX7_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                            }
                            if (LineTransactionData.Dx8Ptr)
                            {
                                c1Transaction.SetCellCheck(rowIndex, COL_DX8_PTR, C1.Win.C1FlexGrid.CheckEnum.Checked);
                            }
                            else
                            {
                                c1Transaction.SetCellCheck(rowIndex, COL_DX8_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                            }


                            if (LineTransactionData.EMG)
                            {
                                c1Transaction.SetCellCheck(rowIndex, COL_ISEMG, C1.Win.C1FlexGrid.CheckEnum.Checked);
                            }
                            else
                            {
                                c1Transaction.SetCellCheck(rowIndex, COL_ISEMG, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                            }

                            //20090825
                            if (LineTransactionData.LinePrimaryDxCode.Trim() != "")
                            { c1Transaction.SetData(rowIndex, COL_LINEPRIMARY_DXCODE, LineTransactionData.LinePrimaryDxCode.Trim()); }

                            if (LineTransactionData.LinePrimaryDxDesc.Trim() != "")
                            { c1Transaction.SetData(rowIndex, COL_LINEPRIMARY_DXDESC, LineTransactionData.LinePrimaryDxDesc.Trim()); }


                            c1Transaction.SetData(rowIndex, COL_MOD1_CODE, LineTransactionData.Mod1Code.ToString());
                            c1Transaction.SetData(rowIndex, COL_MOD1_DESC, LineTransactionData.Mod1Description.ToString());
                            c1Transaction.SetData(rowIndex, COL_MOD2_CODE, LineTransactionData.Mod2Code.ToString());
                            c1Transaction.SetData(rowIndex, COL_MOD2_DESC, LineTransactionData.Mod2Description.ToString());
                            c1Transaction.SetData(rowIndex, COL_MOD3_CODE, LineTransactionData.Mod3Code.ToString());
                            c1Transaction.SetData(rowIndex, COL_MOD3_DESC, LineTransactionData.Mod3Description.ToString());
                            c1Transaction.SetData(rowIndex, COL_MOD4_CODE, LineTransactionData.Mod4Code.ToString());
                            c1Transaction.SetData(rowIndex, COL_MOD4_DESC, LineTransactionData.Mod4Description.ToString());


                            c1Transaction.SetData(rowIndex, COL_UNIT, gloCharges.FormatNumber(LineTransactionData.Unit));

                            // Set CPT Defaults After applying EMR Treatment
                            SetCPTDefaults(CPTChangedFrom.EMRTreatment);

                            //Get Fee Schedule

                            #region  " .Load Fee Scheduele "

                            string _whencpt = "";
                            string _whenmod = "";
                            Int64 _wheninsid = 0;
                            Int64 _fromDOS = 0;
                            Int64 _toDOS = 0;

                            _whencpt = LineTransactionData.CPTCode.ToString();
                            _whenmod = Convert.ToString(c1Transaction.GetData(rowIndex, COL_MOD1_CODE));
                            _wheninsid = LineTransactionData.InsuranceID;
                            _fromDOS = gloDateMaster.gloDate.DateAsNumber(LineTransactionData.DateServiceFrom.ToString());
                            _toDOS = gloDateMaster.gloDate.DateAsNumber(LineTransactionData.DateServiceTill.ToString());


                            decimal _AllowedCharges = 0;
                            decimal _Charges = 0;
                            bool _isAllowedAmount = false;
                            bool _isChargesAmount = false;
                            if (_IsOpenForModify == false)
                            {
                                _bDefaultSelf = oclsFeeSchedule.GetDefaultSelf(_whencpt);
                                #region "Set defaut Self claim of CPT"
                                if (_bDefaultSelf == true && _showSplitClaimToPatient == true)
                                {
                                    c1Transaction.SetCellCheck(rowIndex, COL_SELFCLAIM, CheckEnum.Checked);
                                }
                                else
                                {
                                    c1Transaction.SetCellCheck(rowIndex, COL_SELFCLAIM, CheckEnum.Unchecked);
                                }
                            }
                            if (c1Transaction.GetCellCheck(rowIndex, COL_SELFCLAIM) == CheckEnum.Checked)
                            {
                                oclsFeeSchedule.GetCPTFees(_whencpt, _whenmod, _transactionDetailId, _fromDOS, _toDOS, _FeeScheduleID, _PatientProviderID, 0, DefaultChargesType.GetHashCode(), out _AllowedCharges, out _isAllowedAmount, out _Charges, out _isChargesAmount, out _Fee_ScheduleID);
                            }
                            else
                            {
                                oclsFeeSchedule.GetCPTFees(_whencpt, _whenmod, _transactionDetailId, _fromDOS, _toDOS, _FeeScheduleID, _PatientProviderID, _nContactID, DefaultChargesType.GetHashCode(), out _AllowedCharges, out _isAllowedAmount, out _Charges, out _isChargesAmount, out _Fee_ScheduleID);
                            }

                            _DefaultCPT_CLIAno = oclsFeeSchedule.GetDefaultCPT_CLIAno(_whencpt);
                            #region "Set Default CLIA# for Selected CPT"
                            if (_DefaultCPT_CLIAno.Trim() != "" && _showLabColumn == true)  //changes for refer admin settings ShowLabColumns Sameer 15-May-2015
                            {
                                c1Transaction.SetCellCheck(rowIndex, COL_ISLABCPT, CheckEnum.Checked);
                                c1Transaction.SetData(rowIndex, COL_AUTHORIZATIONNO, _DefaultCPT_CLIAno);
                                c1Transaction.Rows[rowIndex].UserData = true;
                            }
                            else
                            {
                                c1Transaction.SetCellCheck(rowIndex, COL_ISLABCPT, CheckEnum.Unchecked);
                                c1Transaction.SetData(rowIndex, COL_AUTHORIZATIONNO, "");
                            }
                            #endregion

                                #endregion
                            //  decimal nCPTunits = 0;

                            //if (_isChargesAmount == false)
                            //{
                            //    #region " Retrive CPT Charges If Exist Override the Fee Schedule "



                            //    if (oclsFeeSchedule.GetCPTCharges(_whencpt, out _Charges, out nCPTunits) == true)
                            //    {
                            //        _Fee_ScheduleType = FeeScheduleType.CPT;
                            //        c1Transaction.SetData(rowIndex, COL_CHARGES, _Charges);
                            //        onCPTCharges_Load(null, this.DefaultChargesType);
                            //    }
                            //    #endregion " Retrive CPT Charges If Exist Override the Fee Schedule "
                            //}
                            c1Transaction.SetData(rowIndex, COL_CHARGES, _Charges);
                            decimal units = Convert.ToDecimal(c1Transaction.GetData(rowIndex, COL_UNIT));

                            if (units > 0)
                            {
                                _AllowedCharges = _AllowedCharges * units;
                            }
                            // Bugid #53895:
                            //problem: allowed amount showing 0.00 instead of blank if multiple service line exist
                            //solution if condition added to solve the problem, _isAllowedAmount check .
                            if (_isAllowedAmount)
                            {
                                c1Transaction.SetData(rowIndex, COL_ALLOWED, _AllowedCharges);
                            }
                            else
                            {
                                c1Transaction.SetData(rowIndex, COL_ALLOWED, null);
                            }
                            LineTransactionData.Total = _Charges * units;
                            c1Transaction.SetData(rowIndex, COL_TOTAL, LineTransactionData.Total.ToString());


                            c1Transaction.SetData(rowIndex, COL_ACTUAL_ALLOWED, _AllowedCharges);
                            decimal _actualAllowed = 0;
                            if (_AllowedCharges > 0 && units > 0 && _isAllowedAmount)
                            {
                                _actualAllowed = (_AllowedCharges / units);
                                c1Transaction.SetData(rowIndex, COL_ACTUAL_ALLOWED, _actualAllowed);
                            }

                            #endregion  " .Load Fee Scheduele "

                            if (LineTransactionData.RefferingProviderId > 0)
                            {
                                string _RefferringProvider = "";


                                if (_dtProvider != null && _dtProvider.Rows.Count > 0)
                                {
                                    DataRow[] dr = null;
                                    dr = _dtProvider.Select(" nProviderID = " + Convert.ToInt64(LineTransactionData.RefferingProviderId) + "");
                                    if (dr != null && dr.Length > 0)
                                    {
                                        _RefferringProvider = Convert.ToString(dr[0]["sProviderName"]);
                                    }
                                    dr = null;
                                }

                                // _RefferringProvider = ogloBilling.GetProvider(LineTransactionData.RefferingProviderId);
                                c1Transaction.SetData(rowIndex, COL_PROVIDER_ID, LineTransactionData.RefferingProviderId);
                                c1Transaction.SetData(rowIndex, COL_PROVIDER, _RefferringProvider);
                                _RefferringProvider = null;
                            }
                            else
                            {

                                if (LineTransactionData.RenderingProviderID > 0)
                                {
                                    if (!IsSettingsForProvider)
                                    {
                                        //set the default renderring provider for the ling
                                        c1Transaction.SetData(rowIndex, COL_PROVIDER_ID, LineTransactionData.RenderingProviderID);
                                        c1Transaction.SetData(rowIndex, COL_PROVIDER, LineTransactionData.RenderingProviderName);
                                    }
                                    else
                                    {
                                        //set the default renderring provider for the ling
                                        c1Transaction.SetData(rowIndex, COL_PROVIDER_ID, _DefaultRenderingProviderID);
                                        c1Transaction.SetData(rowIndex, COL_PROVIDER, _DefaultRenderringProviderName);

                                    }
                                }
                                else
                                {
                                    //set the default renderring provider for the ling
                                    c1Transaction.SetData(rowIndex, COL_PROVIDER_ID, _DefaultRenderingProviderID);
                                    c1Transaction.SetData(rowIndex, COL_PROVIDER, _DefaultRenderringProviderName);
                                }

                            }


                            if (LineTransactionData.InsuranceSelfMode == PayerMode.Self)
                            {
                                c1Transaction.SetData(rowIndex, COL_INSURANCEID, LineTransactionData.InsuranceID.ToString());
                                c1Transaction.SetData(rowIndex, COL_INSURANCENAME, LineTransactionData.InsuranceSelfMode.ToString());
                                c1Transaction.SetData(rowIndex, COL_INSSELF_PAYMODE, LineTransactionData.InsuranceSelfMode.GetHashCode());
                            }
                            else if (LineTransactionData.InsuranceSelfMode == PayerMode.Insurance)
                            {
                                c1Transaction.SetData(rowIndex, COL_INSURANCEID, LineTransactionData.InsuranceID.ToString());
                                c1Transaction.SetData(rowIndex, COL_INSURANCENAME, LineTransactionData.InsuranceName.ToString());
                                c1Transaction.SetData(rowIndex, COL_INSSELF_PAYMODE, LineTransactionData.InsuranceSelfMode.GetHashCode());
                            }

                            //**

                            //** Code added on 16 Jan 2009 By Sagar Ghodke
                            //..To implement Notes functionality
                            if (LineTransactionData.LineNotes != null && LineTransactionData.LineNotes.Count > 0)
                            {
                                for (int noteIndex = 0; noteIndex < LineTransactionData.LineNotes.Count; noteIndex++)
                                {
                                    SetNotes(rowIndex, LineTransactionData.LineNotes[noteIndex]);
                                }

                            }
                            else
                            { c1Transaction.SetData(rowIndex, COL_NOTE_DATA, null); }


                            //**Code added on 20090511 by - Sagar Ghodke
                            //**Code added to implement CLIA functionality

                            if (LineTransactionData.IsLabCPT == true)
                            {
                                c1Transaction.SetCellCheck(rowIndex, COL_ISLABCPT, CheckEnum.Checked);
                                c1Transaction.SetData(rowIndex, COL_AUTHORIZATIONNO, LineTransactionData.AuthorizationNo);
                            }
                            c1Transaction.SetCellCheck(rowIndex, COL_SENTTOCLAIM, CheckEnum.Checked);

                            //**End code add 20090511,Sagar Ghodke

                            //Dispose the Temporary Object
                            //SLR: it is only a reference and hence need not to be disposed.
                            //if (LineTransactionData != null)
                            //{
                            //    LineTransactionData.Dispose();
                            //    LineTransactionData = null;
                            //}

                            #endregion
                            
                        }//end - for (int i = 0; i < LineTransactionsData.Count; i++)
                        SortControl();
                        
                        _returnResult = true;

                    } //end - if (LineTransactionsData.Count > 0)

                }// end - if (LineTransactionsData != null)
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return _returnResult;
            }
            finally
            {
                //SLR: This should be freed only from the calling module.
                //if (LineTransactionsData != null)
                //{
                //    LineTransactionsData.Dispose();
                //    LineTransactionsData = null;
                //}
                if (oclsFeeSchedule != null)
                {
                    oclsFeeSchedule.Dispose();
                    oclsFeeSchedule = null;
                }
                if (_dtProvider != null)
                {
                    _dtProvider.Dispose();
                    _dtProvider = null;
                }
            }
            return _returnResult;
        }


        public bool SetEMRExamLineTransactionForSplit(TransactionLines LineTransactionsData, Int32 nLinesCount)
        {
            int rowIndex = 0;
            bool _returnResult = false;
            TransactionLine LineTransactionData = null;
            ClsFeeSchedule oclsFeeSchedule = null;
            #region "Custom Event"
            TrnCtrlColValChangeEventArg e2 = new TrnCtrlColValChangeEventArg();
            RowColEventArgs e1 = null;
            #endregion

            try
            {
                if (LineTransactionsData != null)
                {
                    if (LineTransactionsData.Count > 0)
                    {
                        oclsFeeSchedule = new ClsFeeSchedule(_DatabaseConnectionString);
                        if (c1Transaction.Rows.Count - 1 < nLinesCount)
                        {
                            for (int i = 0; i < LineTransactionsData.Count; i++)
                            {
                                if (c1Transaction.Rows.Count - 1 < nLinesCount)
                                {
                                    c1Transaction.Rows.Add();
                                    rowIndex = c1Transaction.Rows.Count - 1;
                                    #region " Set Service Line "

                                    // LineTransactionData = new TransactionLine();
                                    LineTransactionData = LineTransactionsData[i];



                                    c1Transaction.SetData(rowIndex, COL_NO, LineTransactionData.TransactionLineId);
                                    c1Transaction.SetData(rowIndex, COL_EMRTREATMENTLINENO, LineTransactionData.EMRTreatmentLineNo);
                                    c1Transaction.SetData(rowIndex, COL_TRANSACTIONID, LineTransactionData.TransactionId);
                                    c1Transaction.SetData(rowIndex, COL_TRANSACTION_DETAIL_ID, LineTransactionData.TransactionDetailID);

                                    c1Transaction.SetData(rowIndex, COL_DATEFROM, LineTransactionData.DateServiceFrom.ToString("dd-MMM-yy"));


                                    c1Transaction.SetData(rowIndex, COL_POSCODE, _DefaultPOSCode);
                                    c1Transaction.SetData(rowIndex, COL_POSDESC, _DefaultPOSDesc);

                                    c1Transaction.SetData(rowIndex, COL_TOSCODE, _DefaultTOSCode);
                                    c1Transaction.SetData(rowIndex, COL_TOSDESC, _DefaultTOSDesc);

                                    c1Transaction.SetData(rowIndex, COL_CPT_CODE, LineTransactionData.CPTCode.ToString());
                                    c1Transaction.SetData(rowIndex, COL_CPT_DESC, LineTransactionData.CPTDescription.ToString());


                                    #region " Dx "

                                    c1Transaction.SetData(rowIndex, COL_DX1_CODE, LineTransactionData.Dx1Code.ToString());
                                    c1Transaction.SetData(rowIndex, COL_DX1_DESC, LineTransactionData.Dx1Description.ToString());
                                    if (LineTransactionData.Dx1Code.Trim() != "")
                                    {
                                        e2 = new TrnCtrlColValChangeEventArg();
                                        e2.code = LineTransactionData.Dx1Code;
                                        e2.description = LineTransactionData.Dx1Description;
                                        e2.oType = TransactionLineColumnType.Diagnosis;
                                        e2.isdeleted = false;
                                        onInsCPTDxMod_Changed(null, e1, e2);
                                    }
                                    c1Transaction.SetData(rowIndex, COL_DX2_CODE, LineTransactionData.Dx2Code.ToString());
                                    c1Transaction.SetData(rowIndex, COL_DX2_DESC, LineTransactionData.Dx2Description.ToString());
                                    if (LineTransactionData.Dx2Code.Trim() != "")
                                    {
                                        e2 = new TrnCtrlColValChangeEventArg();
                                        e2.code = LineTransactionData.Dx2Code;
                                        e2.description = LineTransactionData.Dx2Description;
                                        e2.oType = TransactionLineColumnType.Diagnosis;
                                        e2.isdeleted = false;
                                        onInsCPTDxMod_Changed(null, e1, e2);
                                    }
                                    c1Transaction.SetData(rowIndex, COL_DX3_CODE, LineTransactionData.Dx3Code.ToString());
                                    c1Transaction.SetData(rowIndex, COL_DX3_DESC, LineTransactionData.Dx3Description.ToString());
                                    if (LineTransactionData.Dx3Code.Trim() != "")
                                    {
                                        e2 = new TrnCtrlColValChangeEventArg();
                                        e2.code = LineTransactionData.Dx3Code;
                                        e2.description = LineTransactionData.Dx3Description;
                                        e2.oType = TransactionLineColumnType.Diagnosis;
                                        e2.isdeleted = false;
                                        onInsCPTDxMod_Changed(null, e1, e2);
                                    }
                                    c1Transaction.SetData(rowIndex, COL_DX4_CODE, LineTransactionData.Dx4Code.ToString());
                                    c1Transaction.SetData(rowIndex, COL_DX4_DESC, LineTransactionData.Dx4Description.ToString());
                                    if (LineTransactionData.Dx4Code.Trim() != "")
                                    {
                                        e2 = new TrnCtrlColValChangeEventArg();
                                        e2.code = LineTransactionData.Dx4Code;
                                        e2.description = LineTransactionData.Dx4Description;
                                        e2.oType = TransactionLineColumnType.Diagnosis;
                                        e2.isdeleted = false;
                                        onInsCPTDxMod_Changed(null, e1, e2);
                                    }
                                    c1Transaction.SetData(rowIndex, COL_DX5_CODE, LineTransactionData.Dx5Code.ToString());
                                    c1Transaction.SetData(rowIndex, COL_DX5_DESC, LineTransactionData.Dx5Description.ToString());
                                    c1Transaction.SetData(rowIndex, COL_DX6_CODE, LineTransactionData.Dx6Code.ToString());
                                    c1Transaction.SetData(rowIndex, COL_DX6_DESC, LineTransactionData.Dx6Description.ToString());
                                    c1Transaction.SetData(rowIndex, COL_DX7_CODE, LineTransactionData.Dx7Code.ToString());
                                    c1Transaction.SetData(rowIndex, COL_DX7_DESC, LineTransactionData.Dx7Description.ToString());
                                    c1Transaction.SetData(rowIndex, COL_DX8_CODE, LineTransactionData.Dx8Code.ToString());
                                    c1Transaction.SetData(rowIndex, COL_DX8_DESC, LineTransactionData.Dx8Description.ToString());




                                    if (LineTransactionData.Dx1Ptr)
                                    {
                                        c1Transaction.SetCellCheck(rowIndex, COL_DX1_PTR, C1.Win.C1FlexGrid.CheckEnum.Checked);
                                    }
                                    else
                                    {
                                        c1Transaction.SetCellCheck(rowIndex, COL_DX1_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                                    }

                                    if (LineTransactionData.Dx2Ptr)
                                    {
                                        c1Transaction.SetCellCheck(rowIndex, COL_DX2_PTR, C1.Win.C1FlexGrid.CheckEnum.Checked);
                                    }
                                    else
                                    {
                                        c1Transaction.SetCellCheck(rowIndex, COL_DX2_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                                    }

                                    if (LineTransactionData.Dx3Ptr)
                                    {
                                        c1Transaction.SetCellCheck(rowIndex, COL_DX3_PTR, C1.Win.C1FlexGrid.CheckEnum.Checked);
                                    }
                                    else
                                    {
                                        c1Transaction.SetCellCheck(rowIndex, COL_DX3_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                                    }

                                    if (LineTransactionData.Dx4Ptr)
                                    {
                                        c1Transaction.SetCellCheck(rowIndex, COL_DX4_PTR, C1.Win.C1FlexGrid.CheckEnum.Checked);
                                    }
                                    else
                                    {
                                        c1Transaction.SetCellCheck(rowIndex, COL_DX4_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                                    }
                                    if (LineTransactionData.Dx5Ptr)
                                    {
                                        c1Transaction.SetCellCheck(rowIndex, COL_DX5_PTR, C1.Win.C1FlexGrid.CheckEnum.Checked);
                                    }
                                    else
                                    {
                                        c1Transaction.SetCellCheck(rowIndex, COL_DX5_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                                    }
                                    if (LineTransactionData.Dx6Ptr)
                                    {
                                        c1Transaction.SetCellCheck(rowIndex, COL_DX6_PTR, C1.Win.C1FlexGrid.CheckEnum.Checked);
                                    }
                                    else
                                    {
                                        c1Transaction.SetCellCheck(rowIndex, COL_DX6_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                                    }
                                    if (LineTransactionData.Dx7Ptr)
                                    {
                                        c1Transaction.SetCellCheck(rowIndex, COL_DX7_PTR, C1.Win.C1FlexGrid.CheckEnum.Checked);
                                    }
                                    else
                                    {
                                        c1Transaction.SetCellCheck(rowIndex, COL_DX7_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                                    }
                                    if (LineTransactionData.Dx8Ptr)
                                    {
                                        c1Transaction.SetCellCheck(rowIndex, COL_DX8_PTR, C1.Win.C1FlexGrid.CheckEnum.Checked);
                                    }
                                    else
                                    {
                                        c1Transaction.SetCellCheck(rowIndex, COL_DX8_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                                    }


                                    if (LineTransactionData.EMG)
                                    {
                                        c1Transaction.SetCellCheck(rowIndex, COL_ISEMG, C1.Win.C1FlexGrid.CheckEnum.Checked);
                                    }
                                    else
                                    {
                                        c1Transaction.SetCellCheck(rowIndex, COL_ISEMG, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                                    }

                                    //20090825
                                    if (LineTransactionData.LinePrimaryDxCode.Trim() != "")
                                    { c1Transaction.SetData(rowIndex, COL_LINEPRIMARY_DXCODE, LineTransactionData.LinePrimaryDxCode.Trim()); }

                                    if (LineTransactionData.LinePrimaryDxDesc.Trim() != "")
                                    { c1Transaction.SetData(rowIndex, COL_LINEPRIMARY_DXDESC, LineTransactionData.LinePrimaryDxDesc.Trim()); }

                                    #endregion

                                    c1Transaction.SetData(rowIndex, COL_MOD1_CODE, LineTransactionData.Mod1Code.ToString());
                                    c1Transaction.SetData(rowIndex, COL_MOD1_DESC, LineTransactionData.Mod1Description.ToString());
                                    c1Transaction.SetData(rowIndex, COL_MOD2_CODE, LineTransactionData.Mod2Code.ToString());
                                    c1Transaction.SetData(rowIndex, COL_MOD2_DESC, LineTransactionData.Mod2Description.ToString());
                                    c1Transaction.SetData(rowIndex, COL_MOD3_CODE, LineTransactionData.Mod3Code.ToString());
                                    c1Transaction.SetData(rowIndex, COL_MOD3_DESC, LineTransactionData.Mod3Description.ToString());
                                    c1Transaction.SetData(rowIndex, COL_MOD4_CODE, LineTransactionData.Mod4Code.ToString());
                                    c1Transaction.SetData(rowIndex, COL_MOD4_DESC, LineTransactionData.Mod4Description.ToString());
                                    c1Transaction.SetData(rowIndex, COL_UNIT, gloCharges.FormatNumber(LineTransactionData.Unit));
                                    SetCPTDefaults(CPTChangedFrom.EMRTreatment);

                                    #region  " .Load Fee Scheduele "
                                    string _whencpt = "";
                                    string _whenmod = "";
                                    Int64 _wheninsid = 0;
                                    Int64 _fromDOS = 0;
                                    Int64 _toDOS = 0;
                                    _whencpt = LineTransactionData.CPTCode.ToString();
                                    _whenmod = Convert.ToString(c1Transaction.GetData(rowIndex, COL_MOD1_CODE));
                                    _wheninsid = LineTransactionData.InsuranceID;
                                    _fromDOS = gloDateMaster.gloDate.DateAsNumber(LineTransactionData.DateServiceFrom.ToString());
                                    _toDOS = gloDateMaster.gloDate.DateAsNumber(LineTransactionData.DateServiceTill.ToString());
                                    decimal _AllowedCharges = 0;
                                    decimal _Charges = 0;
                                    bool _isAllowedAmount = false;
                                    bool _isChargesAmount = false;
                                    oclsFeeSchedule.GetCPTFees(_whencpt, _whenmod, _transactionDetailId, _fromDOS, _toDOS, _FeeScheduleID, _PatientProviderID, _nContactID, DefaultChargesType.GetHashCode(), out _AllowedCharges, out _isAllowedAmount, out _Charges, out _isChargesAmount, out _Fee_ScheduleID);
                                    //       decimal nCPTunits = 0;                                
                                    //if (_Charges == 0)
                                    //{
                                    //    #region " Retrive CPT Charges If Exist Override the Fee Schedule "
                                    //    if (oclsFeeSchedule.GetCPTCharges(_whencpt, out _Charges, out nCPTunits) == true)
                                    //    {
                                    //        _Fee_ScheduleType = FeeScheduleType.CPT;
                                    //        onCPTCharges_Load(null, this.DefaultChargesType);
                                    //    }
                                    //    #endregion " Retrive CPT Charges If Exist Override the Fee Schedule "
                                    //}
                                    c1Transaction.SetData(rowIndex, COL_CHARGES, _Charges);
                                    decimal units = Convert.ToDecimal(c1Transaction.GetData(rowIndex, COL_UNIT));
                                    if (units > 0)
                                    {
                                        _AllowedCharges = _AllowedCharges * units;
                                    }
                                    c1Transaction.SetData(rowIndex, COL_ALLOWED, _AllowedCharges);
                                    LineTransactionData.Total = _Charges * units;
                                    c1Transaction.SetData(rowIndex, COL_TOTAL, LineTransactionData.Total.ToString());
                                    c1Transaction.SetData(rowIndex, COL_ACTUAL_ALLOWED, _AllowedCharges);
                                    decimal _actualAllowed = 0;
                                    if (_AllowedCharges > 0 && units > 0)
                                    {
                                        _actualAllowed = (_AllowedCharges / units);
                                        c1Transaction.SetData(rowIndex, COL_ACTUAL_ALLOWED, _actualAllowed);
                                    }
                                    #endregion  " .Load Fee Scheduele "


                                    #region " Provider "


                                    if (LineTransactionData.RefferingProviderId > 0)
                                    {
                                        string _RefferringProvider = "";
                                        gloBilling ogloBilling = new gloBilling(_DatabaseConnectionString, "");
                                        _RefferringProvider = ogloBilling.GetProvider(LineTransactionData.RefferingProviderId);
                                        c1Transaction.SetData(rowIndex, COL_PROVIDER_ID, LineTransactionData.RefferingProviderId);
                                        c1Transaction.SetData(rowIndex, COL_PROVIDER, _RefferringProvider);
                                        ogloBilling.Dispose();
                                        _RefferringProvider = null;
                                    }
                                    else
                                    {

                                        if (LineTransactionData.RenderingProviderID > 0)
                                        {
                                            if (!IsSettingsForProvider)
                                            {
                                                //set the default renderring provider for the ling
                                                c1Transaction.SetData(rowIndex, COL_PROVIDER_ID, LineTransactionData.RenderingProviderID);
                                                c1Transaction.SetData(rowIndex, COL_PROVIDER, LineTransactionData.RenderingProviderName);
                                            }
                                            else
                                            {
                                                //set the default renderring provider for the ling
                                                c1Transaction.SetData(rowIndex, COL_PROVIDER_ID, _DefaultRenderingProviderID);
                                                c1Transaction.SetData(rowIndex, COL_PROVIDER, _DefaultRenderringProviderName);

                                            }
                                        }
                                        else
                                        {
                                            //set the default renderring provider for the ling
                                            c1Transaction.SetData(rowIndex, COL_PROVIDER_ID, _DefaultRenderingProviderID);
                                            c1Transaction.SetData(rowIndex, COL_PROVIDER, _DefaultRenderringProviderName);
                                        }

                                    }

                                    #endregion


                                    if (LineTransactionData.InsuranceSelfMode == PayerMode.Self)
                                    {
                                        c1Transaction.SetData(rowIndex, COL_INSURANCEID, LineTransactionData.InsuranceID.ToString());
                                        c1Transaction.SetData(rowIndex, COL_INSURANCENAME, LineTransactionData.InsuranceSelfMode.ToString());
                                        c1Transaction.SetData(rowIndex, COL_INSSELF_PAYMODE, LineTransactionData.InsuranceSelfMode.GetHashCode());
                                    }
                                    else if (LineTransactionData.InsuranceSelfMode == PayerMode.Insurance)
                                    {
                                        c1Transaction.SetData(rowIndex, COL_INSURANCEID, LineTransactionData.InsuranceID.ToString());
                                        c1Transaction.SetData(rowIndex, COL_INSURANCENAME, LineTransactionData.InsuranceName.ToString());
                                        c1Transaction.SetData(rowIndex, COL_INSSELF_PAYMODE, LineTransactionData.InsuranceSelfMode.GetHashCode());
                                    }


                                    //..To implement Notes functionality
                                    if (LineTransactionData.LineNotes != null && LineTransactionData.LineNotes.Count > 0)
                                    {
                                        for (int noteIndex = 0; noteIndex < LineTransactionData.LineNotes.Count; noteIndex++)
                                        {
                                            SetNotes(rowIndex, LineTransactionData.LineNotes[noteIndex]);
                                        }

                                    }
                                    else
                                    { c1Transaction.SetData(rowIndex, COL_NOTE_DATA, null); }


                                    //**Code added on 20090511 by - Sagar Ghodke
                                    //**Code added to implement CLIA functionality

                                    if (LineTransactionData.IsLabCPT == true)
                                    {
                                        c1Transaction.SetCellCheck(rowIndex, COL_ISLABCPT, CheckEnum.Checked);
                                        c1Transaction.SetData(rowIndex, COL_AUTHORIZATIONNO, LineTransactionData.AuthorizationNo);
                                    }
                                    c1Transaction.SetCellCheck(rowIndex, COL_SENTTOCLAIM, CheckEnum.Checked);

                                    //**End code add 20090511,Sagar Ghodke


                                    c1Transaction.SetData(rowIndex, COL_NO, rowIndex);
                                    c1Transaction.SetData(rowIndex, COL_EMRTREATMENTLINENO, LineTransactionData.EMRTreatmentLineNo);
                                    //Dispose the Temporary Object
                                    if (LineTransactionData != null)
                                    {
                                        LineTransactionData.Dispose();
                                        LineTransactionData = null;
                                    }

                                    #endregion


                                }
                            }//end - for (int i = 0; i < LineTransactionsData.Count; i++)
                        }

                        //Commented To Identify the Row No In Case of Same CPT and DX
                        //SortControl();
                        _returnResult = true;

                    } //end - if (LineTransactionsData.Count > 0)

                }// end - if (LineTransactionsData != null)
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return _returnResult;
            }
            finally
            {
                //SLR: This should be freed from calling module..
                //if (LineTransactionsData != null)
                //{
                //    LineTransactionsData.Dispose();
                //    LineTransactionsData = null;
                //}
            }
            return _returnResult;
        }

        #endregion

        # region Set NDC Fields
        public void SetNDCFields(TransactionLine oLine)
        {
            try
            {
                if (oLine != null && c1Transaction.RowSel > 0)
                {
                    //if (Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_NOTE_DATA)) != "")
                    c1Transaction.SetData(c1Transaction.RowSel, COL_NDCCODE, oLine.NDCCode);
                    c1Transaction.SetData(c1Transaction.RowSel, COL_NDCUNIT, oLine.NDCUnit);
                    c1Transaction.SetData(c1Transaction.RowSel, COL_NDCUNITCODE, oLine.NDCUnitCode);
                    c1Transaction.SetData(c1Transaction.RowSel, COL_NDCUNITDESCRITION, oLine.NDCUnitDescription);
                    c1Transaction.SetData(c1Transaction.RowSel, COL_PRESCRIPTION, oLine.Prescription);
                    c1Transaction.SetData(c1Transaction.RowSel, COL_PRESCRIPTIONDESC, oLine.PrescriptionDescription);
                    //if (c1Transaction.GetData(c1Transaction.RowSel, COL_NDCCODE)!=null && c1Transaction.GetData(c1Transaction.RowSel, COL_NDCCODE).ToString()!="")
                    //    SetEPSDTNotesNDCCodeFlag(c1Transaction.RowSel);
                    //else
                    SetEPSDTNotesNDCCodeFlag(c1Transaction.RowSel);
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
        # endregion

        #region " Set CPT Defaults"

        public void SetCPTDefaults(int rowIndex, CPTChangedFrom ChangesFrom)
        {
            bool isMofiersEntered = false;
            bool isUnitsEntered = false;

            string CPTCode = Convert.ToString(c1Transaction.GetData(rowIndex, COL_CPT_CODE));
            string Mod1Code = Convert.ToString(c1Transaction.GetData(rowIndex, COL_MOD1_CODE));
            string Mod2Code = Convert.ToString(c1Transaction.GetData(rowIndex, COL_MOD2_CODE));
            string Mod3Code = Convert.ToString(c1Transaction.GetData(rowIndex, COL_MOD3_CODE));
            string Mod4Code = Convert.ToString(c1Transaction.GetData(rowIndex, COL_MOD4_CODE));

            decimal Unit = Convert.ToDecimal(c1Transaction.GetData(rowIndex, COL_UNIT));

            //if (ChangesFrom == CPTChangedFrom.EMRTreatment || ChangesFrom == CPTChangedFrom.SmartTreatment)
            //{
            //    isMofiersEntered = true;
            //}
            //else if(IsMODLoadedFromScrubber)
            //{
            //    isMofiersEntered = true;
            //}
            //else
            //{
            //    //if (!string.IsNullOrEmpty(Mod1Code) || !string.IsNullOrEmpty(Mod2Code) || !string.IsNullOrEmpty(Mod3Code) || !string.IsNullOrEmpty(Mod4Code))
            //    //{
            //    //    isMofiersEntered = true;
            //    //}
            //    //else
            //    //{
            //        isMofiersEntered = false;
            //    //}
            //}


            if (!string.IsNullOrEmpty(Mod1Code) || !string.IsNullOrEmpty(Mod2Code) || !string.IsNullOrEmpty(Mod3Code) || !string.IsNullOrEmpty(Mod4Code))
            { isMofiersEntered = true; }

            else
            { isMofiersEntered = false; }



            //if (ChangesFrom == CPTChangedFrom.EMRTreatment || ChangesFrom == CPTChangedFrom.ManualEntry)
            //{
            //    if (Unit != 1)
            //    { isUnitsEntered = true; }
            //    else
            //    { isUnitsEntered = false; }
            //}
            //else
            //{
            //    if (Unit > 1)
            //    { isUnitsEntered = true; }
            //    else
            //    { isUnitsEntered = false; }
            //}

            if (ChangesFrom == CPTChangedFrom.EMRTreatment)
            {
                if (Unit <= 0)
                { isUnitsEntered = false; }
                else
                { isUnitsEntered = true; }
            }
            else
            {
                isUnitsEntered = false;
            }


            CPT oCPT = new CPT(_DatabaseConnectionString);
            using (DataTable dtCPT = oCPT.getCPT(CPTCode))
            {
                foreach (DataRow row in dtCPT.Rows)
                {
                    Unit = Convert.ToDecimal(row["nUnits"]);

                    Mod1Code = Convert.ToString(row["Modifier1Code"]);
                    Mod2Code = Convert.ToString(row["Modifier2Code"]);
                    Mod3Code = Convert.ToString(row["Modifier3Code"]);
                    Mod4Code = Convert.ToString(row["Modifier4Code"]);
                }
            }

            ArrayList mods = new ArrayList();
            if (!string.IsNullOrEmpty(Mod1Code))
            { mods.Add(Mod1Code); }
            if (!string.IsNullOrEmpty(Mod2Code))
            { mods.Add(Mod2Code); }
            if (!string.IsNullOrEmpty(Mod3Code))
            { mods.Add(Mod3Code); }
            if (!string.IsNullOrEmpty(Mod4Code))
            { mods.Add(Mod4Code); }

            // If Units are not defined, set the default units 
            if (!isUnitsEntered)
            {
                if (!Unit.Equals(0))
                {
                    //c1Transaction.SetData(rowIndex, COL_UNIT, Unit); 
                    //c1Transaction.SetData(rowIndex, COL_UNIT, 1);
                    c1Transaction.SetData(rowIndex, COL_UNIT, gloCharges.FormatNumber(Unit));
                }
                else
                {
                    c1Transaction.SetData(rowIndex, COL_UNIT, 1);
                }
            }

            // If Units are not defined, set the default modifiers 
            if (!isMofiersEntered)
            {
                int ctr = 1;
                foreach (string mod in mods)
                {
                    if (ctr.Equals(1))
                    { c1Transaction.SetData(rowIndex, COL_MOD1_CODE, mod); }
                    else if (ctr.Equals(2))
                    { c1Transaction.SetData(rowIndex, COL_MOD2_CODE, mod); }
                    else if (ctr.Equals(3))
                    { c1Transaction.SetData(rowIndex, COL_MOD3_CODE, mod); }
                    else if (ctr.Equals(4))
                    { c1Transaction.SetData(rowIndex, COL_MOD4_CODE, mod); }

                    ctr = ctr + 1;
                }
            }
        }

        public enum CPTChangedFrom
        {
            ManualEntry,
            SmartTreatment,
            EMRTreatment
        }

        public void SetCPTDefaults(CPTChangedFrom ChangesFrom)
        {
            if (c1Transaction != null && c1Transaction.Rows.Count > 0)
            {
                string cptCODE = string.Empty;

                for (int i = 1; i <= c1Transaction.Rows.Count - 1; i++)
                {
                    if (c1Transaction.GetData(i, COL_CPT_CODE) != null || Convert.ToString(c1Transaction.GetData(i, COL_CPT_CODE)).Trim() != "")
                    {
                        SetCPTDefaults(i, ChangesFrom);
                    }
                }
            }
        }

        #endregion

        #region " Get Set Note Methods "

        public void SetNotes(Common.GeneralNotes Notes)
        {
            try
            {
                if (Notes != null && c1Transaction.RowSel > 0)
                {
                    c1Transaction.SetData(c1Transaction.RowSel, COL_NOTE_DATA, Notes);
                    SetEPSDTNotesNDCCodeFlag(c1Transaction.RowSel);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            { }
        }

        public void SetNotes(int TransactionLineNumber, Common.GeneralNote oNote)
        {
            Common.GeneralNotes oNotes = null;
            try
            {
                //if (Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_NOTE_DATA)) != "")
                if (Convert.ToString(c1Transaction.GetData(TransactionLineNumber, COL_NOTE_DATA)) != "")
                {
                    oNotes = (Common.GeneralNotes)c1Transaction.GetData(TransactionLineNumber, COL_NOTE_DATA);
                    oNotes.Add(oNote);
                    c1Transaction.SetData(TransactionLineNumber, COL_NOTE_DATA, oNotes);

                }
                else
                {
                    oNotes = new GeneralNotes();
                    oNotes.Add(oNote);
                    c1Transaction.SetData(TransactionLineNumber, COL_NOTE_DATA, oNotes);
                    SetEPSDTNotesNDCCodeFlag(TransactionLineNumber);
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

        //public void SetNotesFlag(int TransactionLineNumber, bool SetFlag)
        //{

        //    try
        //    {
        //        //System.Drawing.Image imgFlag = global::gloBilling.Properties.Resources.FlagAcknowledge;
        //        System.Drawing.Image imgFlagNDC = global::gloBilling.Properties.Resources.NDCCodeGrid;//for ndc
        //        System.Drawing.Image imgFlagBoth = global::gloBilling.Properties.Resources.NotesNDCCode;//for both
        //        System.Drawing.Image imgFlagNotes = global::gloBilling.Properties.Resources.Notes;
        //        if (SetFlag == true)
        //        {
        //            if (c1Transaction.GetData(TransactionLineNumber, COL_NDCCODE) != null && c1Transaction.GetData(TransactionLineNumber, COL_NDCCODE).ToString() != "")
        //                c1Transaction.SetCellImage(TransactionLineNumber, COL_NO, imgFlagBoth);
        //            else
        //                c1Transaction.SetCellImage(TransactionLineNumber, COL_NO, imgFlagNotes);

        //        }
        //        else
        //        {
        //            if (c1Transaction.GetData(TransactionLineNumber, COL_NDCCODE) != null && c1Transaction.GetData(TransactionLineNumber, COL_NDCCODE).ToString() != "")
        //                c1Transaction.SetCellImage(TransactionLineNumber, COL_NO, imgFlagNDC); 
        //            else
        //                c1Transaction.SetCellImage(TransactionLineNumber, COL_NO, null); 

        //        }

        //        c1Transaction.Select(TransactionLineNumber, COL_CPT_CODE, true);

        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
        //    }
        //}
        //public void SetNDCCodeFlag(int TransactionLineNumber, bool SetFlag)
        //{

        //    try
        //    {
        //        //System.Drawing.Image imgFlag = global::gloBilling.Properties.Resources.FlagAcknowledge;
        //        System.Drawing.Image imgFlagNDC = global::gloBilling.Properties.Resources.NDCCodeGrid;//for ndc
        //        System.Drawing.Image imgFlagBoth = global::gloBilling.Properties.Resources.NotesNDCCode;//for both
        //        System.Drawing.Image imgFlagNotes = global::gloBilling.Properties.Resources.Notes;
        //        if (SetFlag == true)
        //        {
        //            if (HasNote(TransactionLineNumber))
        //                c1Transaction.SetCellImage(TransactionLineNumber, COL_NO, imgFlagBoth); 
        //            else
        //                c1Transaction.SetCellImage(TransactionLineNumber, COL_NO, imgFlagNDC); 
        //        }
        //        else
        //        {
        //            if (HasNote(TransactionLineNumber))
        //                c1Transaction.SetCellImage(TransactionLineNumber, COL_NO, imgFlagNotes);
        //            else
        //                c1Transaction.SetCellImage(TransactionLineNumber, COL_NO, null);  
        //        }

        //        //c1Transaction.Select(TransactionLineNumber, COL_CPT_CODE, true);

        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
        //    }
        //}

        public void SetEPSDTNotesNDCCodeFlag(int TransactionLineNumber)
        {

            try
            {
                //System.Drawing.Image imgFlag = global::gloBilling.Properties.Resources.FlagAcknowledge;
                System.Drawing.Image imgFlagNDC = global::gloBilling.Properties.Resources.NDCCodeGrid;//for ndc
                System.Drawing.Image imgFlagNotes = global::gloBilling.Properties.Resources.Notes;
                System.Drawing.Image imgFlagEPSDT = global::gloBilling.Properties.Resources.EPSDT_Family_planning_fields;//for EPSDT
                System.Drawing.Image imgFlagNotesNDC = global::gloBilling.Properties.Resources.NotesNDCCode;//for both
                System.Drawing.Image imgFlagNotesEPSDT = global::gloBilling.Properties.Resources.EPSDT_Note;//for both
                System.Drawing.Image imgFlagNDCEPSDT = global::gloBilling.Properties.Resources.EPSDT_NDC;
                System.Drawing.Image imgFlagNotesNDCEPSDT = global::gloBilling.Properties.Resources.NDC_Note_EPSDT;

                System.Drawing.Image imgANS = global::gloBilling.Properties.Resources.Anesthesia;
                System.Drawing.Image imgANS_Note = global::gloBilling.Properties.Resources.Anesthesia___Note;
                System.Drawing.Image imgANS_EPST = global::gloBilling.Properties.Resources.Anesthesia___EPSDT;
                System.Drawing.Image imgANS_NDC = global::gloBilling.Properties.Resources.Anesthesia___NDC;
                System.Drawing.Image imgANS_EPST_NOTE = global::gloBilling.Properties.Resources.Anesthesia___Note___EPST;
                System.Drawing.Image imgANS_NOTE_NDC = global::gloBilling.Properties.Resources.Anesthesia___Note__NDC;
                System.Drawing.Image imgANS_NDC_EPSDT = global::gloBilling.Properties.Resources.Anesthesia___EPSDT;
                System.Drawing.Image imgANS_NDC_EPSDT_NOTE = global::gloBilling.Properties.Resources.Anesthesia___EPSDT__NDC___Note;



                Boolean _isAnesthesia = false;
                if (c1Transaction.GetData(TransactionLineNumber, COL_ANES_ISANESTHESIA) != null)
                {
                    if (Convert.ToBoolean(c1Transaction.GetData(TransactionLineNumber, COL_ANES_ISANESTHESIA)))
                    {
                        _isAnesthesia = true;
                    }
                }

                Boolean resOutput = false;
                Boolean.TryParse(Convert.ToString(c1Transaction.GetData(TransactionLineNumber, COL_SERVICESCREENING)), out resOutput);
                if (resOutput == false)
                {
                    Boolean.TryParse(Convert.ToString(c1Transaction.GetData(TransactionLineNumber, COL_SERVICERESULTSCREENING)), out resOutput);
                    if (resOutput == false)
                    {
                        Boolean.TryParse(Convert.ToString(c1Transaction.GetData(TransactionLineNumber, COL_FAMILYPLANNINGINDICATOR)), out resOutput);
                    }
                }
                if (_isAnesthesia)
                {
                    if (HasNote(TransactionLineNumber))
                    {
                        if ((resOutput) && ((c1Transaction.GetData(TransactionLineNumber, COL_NDCCODE) != null && c1Transaction.GetData(TransactionLineNumber, COL_NDCCODE).ToString() != "")))
                        {
                            c1Transaction.SetCellImage(TransactionLineNumber, COL_NO, imgANS_NDC_EPSDT_NOTE);
                        }
                        else if ((c1Transaction.GetData(TransactionLineNumber, COL_NDCCODE) != null && c1Transaction.GetData(TransactionLineNumber, COL_NDCCODE).ToString() != ""))
                        {
                            c1Transaction.SetCellImage(TransactionLineNumber, COL_NO, imgANS_NOTE_NDC);
                        }
                        else if (resOutput)
                        {
                            c1Transaction.SetCellImage(TransactionLineNumber, COL_NO, imgANS_EPST_NOTE);
                        }
                        else
                        {
                            c1Transaction.SetCellImage(TransactionLineNumber, COL_NO, imgANS_Note);
                        }
                    }
                    else
                    {
                        if ((resOutput) && ((c1Transaction.GetData(TransactionLineNumber, COL_NDCCODE) != null && c1Transaction.GetData(TransactionLineNumber, COL_NDCCODE).ToString() != "")))
                        {
                            c1Transaction.SetCellImage(TransactionLineNumber, COL_NO, imgANS_NDC_EPSDT);
                        }
                        else if ((c1Transaction.GetData(TransactionLineNumber, COL_NDCCODE) != null && c1Transaction.GetData(TransactionLineNumber, COL_NDCCODE).ToString() != ""))
                        {
                            c1Transaction.SetCellImage(TransactionLineNumber, COL_NO, imgANS_NDC);
                        }
                        else if (resOutput)
                        {
                            c1Transaction.SetCellImage(TransactionLineNumber, COL_NO, imgANS_EPST);
                        }
                        else
                        {
                            c1Transaction.SetCellImage(TransactionLineNumber, COL_NO, imgANS);
                        }
                    }
                }
                else if (resOutput)
                {
                    if (HasNote(TransactionLineNumber))
                    {
                        if (c1Transaction.GetData(TransactionLineNumber, COL_NDCCODE) != null && c1Transaction.GetData(TransactionLineNumber, COL_NDCCODE).ToString() != "")
                        {
                            c1Transaction.SetCellImage(TransactionLineNumber, COL_NO, imgFlagNotesNDCEPSDT);
                        }
                        else
                        {
                            c1Transaction.SetCellImage(TransactionLineNumber, COL_NO, imgFlagNotesEPSDT);
                        }
                    }
                    else
                    {
                        if (c1Transaction.GetData(TransactionLineNumber, COL_NDCCODE) != null && c1Transaction.GetData(TransactionLineNumber, COL_NDCCODE).ToString() != "")
                        {
                            c1Transaction.SetCellImage(TransactionLineNumber, COL_NO, imgFlagNDCEPSDT);
                        }
                        else
                        {
                            c1Transaction.SetCellImage(TransactionLineNumber, COL_NO, imgFlagEPSDT);
                        }
                    }
                }
                else
                {
                    if (HasNote(TransactionLineNumber))
                    {
                        if (c1Transaction.GetData(TransactionLineNumber, COL_NDCCODE) != null && c1Transaction.GetData(TransactionLineNumber, COL_NDCCODE).ToString() != "")
                        {
                            c1Transaction.SetCellImage(TransactionLineNumber, COL_NO, imgFlagNotesNDC);
                        }
                        else
                        {
                            c1Transaction.SetCellImage(TransactionLineNumber, COL_NO, imgFlagNotes);
                        }
                    }
                    else
                    {
                        if (c1Transaction.GetData(TransactionLineNumber, COL_NDCCODE) != null && c1Transaction.GetData(TransactionLineNumber, COL_NDCCODE).ToString() != "")
                        {
                            c1Transaction.SetCellImage(TransactionLineNumber, COL_NO, imgFlagNDC);
                        }
                        else
                        {
                            c1Transaction.SetCellImage(TransactionLineNumber, COL_NO, null);
                        }
                    }
                }

                //c1Transaction.Select(TransactionLineNumber, COL_CPT_CODE, true);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        public bool HasNote(int TransactionLineNumber)
        {
            bool _HasNote = false;
            try
            {
                if (TransactionLineNumber > 0)
                {
                    if (c1Transaction != null && c1Transaction.Rows.Count > 0)
                    {
                        if (c1Transaction.GetData(TransactionLineNumber, COL_NOTE_DATA) != null)
                        {
                            if (((Common.GeneralNotes)c1Transaction.GetData(TransactionLineNumber, COL_NOTE_DATA)) != null &&
                                ((Common.GeneralNotes)c1Transaction.GetData(TransactionLineNumber, COL_NOTE_DATA)).Count > 0)
                            {
                                _HasNote = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            return _HasNote;
        }

        public GeneralNotes GetNotes(Int64 TransactionLineNumber)
        {
            Common.GeneralNotes oNotes = null;

            try
            {
                if (c1Transaction != null && c1Transaction.Rows.Count > 0)
                {

                    if (Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_NOTE_DATA)) != "")
                    {
                        oNotes = (Common.GeneralNotes)c1Transaction.GetData(c1Transaction.RowSel, COL_NOTE_DATA);
                    }
                    //if (Convert.ToString(c1Transaction.GetData(TransactionLineNumber, COL_NOTE_DATA)) != "")
                    //{
                    //    oNotes = (Common.GeneralNotes)c1Transaction.GetData(TransactionLineNumber, COL_NOTE_DATA);
                    //}
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {

            }
            return oNotes;
        }

        #endregion

        #region " Delegated Events of Internal Control "

        void ogloGridListControl_ItemSelected(object sender, EventArgs e)
        {
            int _codeColIndex = 0;
            int _descColIndex = 0;
            int _rowIndex = 0;
            //       int _cellNoteColumn = 0;

            IsMODLoadedFromScrubber = false;
            IsDXLoadedFromScrubber = false;

            #region "Custom Event"
            TrnCtrlColValChangeEventArg e2 = new TrnCtrlColValChangeEventArg();
            RowColEventArgs e1 = null;

            //  int _evnid = 0;
            string _evncode = "";
            string _evndescription = "";
            //   bool _evnisdeleted = true;
            #endregion
            ClsFeeSchedule oclsFeeSchedule;
            try
            {
                // _cellNoteColumn = COL_CPT_CODE; 
                _isItemSelectionCall = true;
                oclsFeeSchedule = new ClsFeeSchedule(_DatabaseConnectionString);

                if (ogloGridListControl.SelectedItems != null)
                {
                    //_rowIndex = ogloGridListControl.ParentRowIndex;
                    // _rowIndex = c1Transaction.RowSel;

                    CellRange cr = c1Transaction.Selection;

                    _rowIndex = cr.r2;


                    //Added By PramodNair For Resoving the CPT Getting Removed After Making Insurance Patient
                    //if (_AllowChargeModification)
                    //{
                    //    //Clear existing cell data or search data
                    //    if (c1Transaction.GetData(_rowIndex, c1Transaction.ColSel) != null && Convert.ToString(c1Transaction.GetData(_rowIndex, c1Transaction.ColSel)) != "")
                    //    {
                    //        c1Transaction.Clear(C1.Win.C1FlexGrid.ClearFlags.All, _rowIndex, c1Transaction.ColSel);
                    //    }
                    //}


                    if (ogloGridListControl.SelectedItems.Count > 0)
                    {
                        #region  " Retrive the Code & Description Column Index for Current Control Type "

                        switch (ogloGridListControl.ControlType)
                        {
                            case gloGridListControlType.Providers:
                                _codeColIndex = ogloGridListControl.ParentColIndex - 1;
                                _descColIndex = ogloGridListControl.ParentColIndex;
                                e2.oType = TransactionLineColumnType.None;
                                break;
                            case gloGridListControlType.CPT:
                                _codeColIndex = ogloGridListControl.ParentColIndex;
                                _descColIndex = ogloGridListControl.ParentColIndex + 1;
                                e2.oType = TransactionLineColumnType.CPT;
                                break;
                            case gloGridListControlType.ICD9:
                                _codeColIndex = ogloGridListControl.ParentColIndex;
                                _descColIndex = ogloGridListControl.ParentColIndex + 1;
                                e2.oType = TransactionLineColumnType.Diagnosis;
                                break;
                            case gloGridListControlType.Modifier:
                                _codeColIndex = ogloGridListControl.ParentColIndex;
                                _descColIndex = ogloGridListControl.ParentColIndex + 1;
                                e2.oType = TransactionLineColumnType.Modifier;
                                break;
                            case gloGridListControlType.PatientInsurance:
                                _codeColIndex = ogloGridListControl.ParentColIndex;
                                _descColIndex = ogloGridListControl.ParentColIndex + 1;
                                e2.oType = TransactionLineColumnType.Insurance;
                                break;
                            case gloGridListControlType.POS:
                                _codeColIndex = ogloGridListControl.ParentColIndex;
                                _descColIndex = ogloGridListControl.ParentColIndex + 1;
                                e2.oType = TransactionLineColumnType.None;
                                break;
                            case gloGridListControlType.TOS:
                                _codeColIndex = ogloGridListControl.ParentColIndex;
                                _descColIndex = ogloGridListControl.ParentColIndex + 1;
                                e2.oType = TransactionLineColumnType.None;
                                break;
                            default:
                                break;
                        }

                        #endregion

                        if (ogloGridListControl.ParentColIndex == COL_DX2_CODE ||
                           ogloGridListControl.ParentColIndex == COL_DX3_CODE ||
                           ogloGridListControl.ParentColIndex == COL_DX4_CODE ||
                           ogloGridListControl.ParentColIndex == COL_DX5_CODE ||
                           ogloGridListControl.ParentColIndex == COL_DX6_CODE ||
                           ogloGridListControl.ParentColIndex == COL_DX7_CODE ||
                           ogloGridListControl.ParentColIndex == COL_DX8_CODE)
                        {
                            FindFirstEmptyDxSlot(_rowIndex, ogloGridListControl.ParentColIndex, out _codeColIndex, out _descColIndex);


                        }
                        else if (ogloGridListControl.ParentColIndex == COL_MOD2_CODE || ogloGridListControl.ParentColIndex == COL_MOD3_CODE || ogloGridListControl.ParentColIndex == COL_MOD4_CODE)
                        {
                            FindFirstEmptyModSlot(_rowIndex, ogloGridListControl.ParentColIndex, out _codeColIndex, out _descColIndex);


                        }

                        ArrayList _ChkDupDx = new ArrayList();

                        #region "First Check Whether the Dignosis is more than 4"
                        for (int d = 1; d <= c1Transaction.Rows.Count - 1; d++)
                        {
                            for (int c = COL_DX1_CODE; c <= COL_DX8_CODE; c += 3)
                            {
                                if (c1Transaction.GetData(d, c) != null && c1Transaction.GetData(d, c).ToString() != "")
                                {
                                    string _curdx = Convert.ToString(c1Transaction.GetData(d, c)).Trim();
                                    if (_ChkDupDx.Contains(_curdx) == false) { _ChkDupDx.Add(_curdx); }
                                }
                            }
                        }

                        #endregion


                        e1 = new RowColEventArgs(_rowIndex, _codeColIndex);

                        //e2.id = 0;
                        //e2.code = ogloGridListControl.SelectedItems[0].Code.ToString();
                        //e2.description = ogloGridListControl.SelectedItems[0].Description.ToString();

                        //   _evnid = 0;
                        _evncode = ogloGridListControl.SelectedItems[0].Code.ToString();
                        _evndescription = ogloGridListControl.SelectedItems[0].Description.ToString();

                        if (_codeColIndex >= COL_DX1_CODE && _codeColIndex <= COL_DX8_CODE)
                        {
                            //ICD9 oICD9 = new ICD9(_DatabaseConnectionString);
                            if (_IsCheckInvalidICD9 == true)
                            {
                                //if (oICD9.IsInValidICD(ogloGridListControl.SelectedItems[0].Code.ToString()) == true)
                                if (ogloGridListControl.SelectedItems[0].Status.Trim() != "" && Convert.ToBoolean(ogloGridListControl.SelectedItems[0].Status.Trim()) == true)
                                {
                                    MessageBox.Show("The selected Diagnosis (Code : " + ogloGridListControl.SelectedItems[0].Code.ToString() + ") is invalid for EDI ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }

                            if (_ChkDupDx.Count < _NoOfDiagnosis)
                            {
                                c1Transaction.SetData(_rowIndex, _codeColIndex, ogloGridListControl.SelectedItems[0].Code.ToString());
                                c1Transaction.SetData(_rowIndex, _descColIndex, ogloGridListControl.SelectedItems[0].Description.ToString());
                                //c1Transaction.SetCellCheck(_rowIndex, _codeColIndex, CheckEnum.Checked);
                                //CellNote oNote = new CellNote(c1Transaction[_rowIndex, _descColIndex].ToString());
                                //CellRange rg = c1Transaction.GetCellRange(_rowIndex, _codeColIndex);
                                //rg.UserData = oNote;
                            }
                            else
                            {
                                if (_ChkDupDx.Contains(ogloGridListControl.SelectedItems[0].Code.ToString().Trim()) == true)
                                {
                                    c1Transaction.SetData(_rowIndex, _codeColIndex, ogloGridListControl.SelectedItems[0].Code.ToString());
                                    c1Transaction.SetData(_rowIndex, _descColIndex, ogloGridListControl.SelectedItems[0].Description.ToString());
                                    //CellNote oNote = new CellNote(c1Transaction[_rowIndex, _descColIndex].ToString());
                                    //CellRange rg = c1Transaction.GetCellRange(_rowIndex, _codeColIndex);
                                    //rg.UserData = oNote;
                                }
                                else if (_dxCodeForDistinct.Trim() != "" && _dxDescriptionForDistinct.Trim() != "")
                                {
                                    MessageBox.Show("Claim has a max limit of " + _NoOfDiagnosis + " diagnoses.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    bIsDxMsgShown = true;

                                    c1Transaction.SetData(_rowIndex, _codeColIndex, _dxCodeForDistinct);
                                    c1Transaction.SetData(_rowIndex, _descColIndex, _dxDescriptionForDistinct);
                                    //CellNote oNote = new CellNote(c1Transaction[_rowIndex, _descColIndex].ToString());
                                    //CellRange rg = c1Transaction.GetCellRange(_rowIndex, _codeColIndex);
                                    //rg.UserData = oNote;
                                }
                                else
                                {
                                    MessageBox.Show("Claim has a max limit of " + _NoOfDiagnosis + " diagnosis.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    bIsDxMsgShown = true;
                                }
                            }
                        }
                        else
                        {
                            c1Transaction.SetData(_rowIndex, _codeColIndex, ogloGridListControl.SelectedItems[0].Code.ToString());
                            c1Transaction.SetData(_rowIndex, _descColIndex, ogloGridListControl.SelectedItems[0].Description.ToString());
                            //CellNote oNote = new CellNote(c1Transaction[_rowIndex, _descColIndex].ToString());
                            //CellRange rg = c1Transaction.GetCellRange(_rowIndex, _codeColIndex);
                            //rg.UserData = oNote;
                        }
                        _ChkDupDx = null;


                        #region " ** Load scrubber setting for CPT ** "

                        if (ogloGridListControl.ControlType == gloGridListControlType.CPT)
                        {
                            gloScrubber ogloScrubber = new gloScrubber(_DatabaseConnectionString);
                            Scrubber oScrubber = null;
                            string _cptCode = "";

                            try
                            {
                                if (Convert.ToString(c1Transaction.GetData(_rowIndex, COL_CPT_CODE)) != "")
                                {
                                    _cptCode = Convert.ToString(c1Transaction.GetData(_rowIndex, COL_CPT_CODE));

                                    //..Code added on 20090425 Sagar Ghodke
                                    //GetReferralCPTSetting();
                                    if (_IsReferralCPT == true)
                                    {
                                        //if (IsReferralRequire(_cptCode) == true)
                                        if (ogloGridListControl.SelectedItems[0].Status.Trim() != "" && Convert.ToBoolean(ogloGridListControl.SelectedItems[0].Status) == true)
                                        {
                                            MessageBox.Show("Selected CPT Code requires Referral Physician.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                    }

                                    //End Code add 20090425

                                    if (c1Transaction.GetData(1, COL_EMRTREATMENTLINENO) != null && Convert.ToInt32(c1Transaction.GetData(1, COL_EMRTREATMENTLINENO)) > 0)
                                    {
                                    }
                                    else
                                    {
                                        oScrubber = ogloScrubber.GetScrubber(_cptCode);
                                        if (oScrubber != null)
                                        {
                                            #region " Load POS & TOS "

                                            if (oScrubber.POSCode.Trim() != "")
                                            {
                                                c1Transaction.SetData(_rowIndex, COL_POSCODE, oScrubber.POSCode);
                                                c1Transaction.SetData(_rowIndex, COL_POSDESC, oScrubber.POSDesc);
                                            }

                                            if (oScrubber.TOSCode.Trim() != "")
                                            {
                                                c1Transaction.SetData(_rowIndex, COL_TOSCODE, oScrubber.TOSCode);
                                                c1Transaction.SetData(_rowIndex, COL_TOSDESC, oScrubber.TOSDesc);
                                            }

                                            #endregion

                                            #region " Load Diagnosis "

                                            if (oScrubber.Diagnosis != null && oScrubber.Diagnosis.Count > 0)
                                            {

                                                IsDXLoadedFromScrubber = true;

                                                string _dxCode = "";
                                                string _dxDesc = "";
                                                int _dxSetCount = 0;
                                                //CellNote oNote = null;
                                                //CellRange rg;

                                                for (int dxIndex = 0; dxIndex < oScrubber.Diagnosis.Count; dxIndex++)
                                                {
                                                    _dxCode = oScrubber.Diagnosis[dxIndex].Code;
                                                    _dxDesc = oScrubber.Diagnosis[dxIndex].Description;
                                                    //oNote = null;
                                                    //rg = new CellRange();

                                                    if (_dxCode.Trim() != "")
                                                    {
                                                        switch (dxIndex)
                                                        {
                                                            case 0:
                                                                c1Transaction.SetData(_rowIndex, COL_DX1_CODE, _dxCode);
                                                                c1Transaction.SetData(_rowIndex, COL_DX1_DESC, _dxDesc);
                                                                c1Transaction.SetCellCheck(_rowIndex, COL_DX1_PTR, CheckEnum.Checked);
                                                                e1 = new RowColEventArgs(_rowIndex, COL_DX1_CODE);
                                                                e2.oType = TransactionLineColumnType.Diagnosis;
                                                                e2.code = _dxCode;
                                                                e2.description = _dxDesc;
                                                                e2.isdeleted = false;
                                                                onInsCPTDxMod_Changed(null, e1, e2);
                                                                e2.oType = TransactionLineColumnType.CPT;
                                                                //oNote = new CellNote(c1Transaction[_rowIndex, COL_DX1_DESC].ToString());
                                                                //rg = c1Transaction.GetCellRange(_rowIndex, COL_DX1_CODE);
                                                                //rg.UserData = oNote;
                                                                _dxSetCount = 1;
                                                                break;
                                                            case 1:
                                                                c1Transaction.SetData(_rowIndex, COL_DX2_CODE, _dxCode);
                                                                c1Transaction.SetData(_rowIndex, COL_DX2_DESC, _dxDesc);
                                                                c1Transaction.SetCellCheck(_rowIndex, COL_DX2_PTR, CheckEnum.Checked);
                                                                e1 = new RowColEventArgs(_rowIndex, COL_DX2_CODE);
                                                                e2.oType = TransactionLineColumnType.Diagnosis;
                                                                e2.code = _dxCode;
                                                                e2.description = _dxDesc;
                                                                e2.isdeleted = false;
                                                                onInsCPTDxMod_Changed(null, e1, e2);
                                                                e2.oType = TransactionLineColumnType.CPT;
                                                                //oNote = new CellNote(c1Transaction[_rowIndex, COL_DX2_DESC].ToString());
                                                                //rg = c1Transaction.GetCellRange(_rowIndex, COL_DX2_CODE);
                                                                //rg.UserData = oNote;
                                                                _dxSetCount = 2;
                                                                break;
                                                            case 2:
                                                                c1Transaction.SetData(_rowIndex, COL_DX3_CODE, _dxCode);
                                                                c1Transaction.SetData(_rowIndex, COL_DX3_DESC, _dxDesc);
                                                                c1Transaction.SetCellCheck(_rowIndex, COL_DX3_PTR, CheckEnum.Checked);
                                                                e1 = new RowColEventArgs(_rowIndex, COL_DX3_CODE);
                                                                e2.oType = TransactionLineColumnType.Diagnosis;
                                                                e2.code = _dxCode;
                                                                e2.description = _dxDesc;
                                                                e2.isdeleted = false;
                                                                onInsCPTDxMod_Changed(null, e1, e2);
                                                                e2.oType = TransactionLineColumnType.CPT;
                                                                //oNote = new CellNote(c1Transaction[_rowIndex, COL_DX3_DESC].ToString());
                                                                //rg = c1Transaction.GetCellRange(_rowIndex, COL_DX3_CODE);
                                                                //rg.UserData = oNote;
                                                                _dxSetCount = 3;
                                                                break;
                                                            case 3:
                                                                c1Transaction.SetData(_rowIndex, COL_DX4_CODE, _dxCode);
                                                                c1Transaction.SetData(_rowIndex, COL_DX4_DESC, _dxDesc);
                                                                c1Transaction.SetCellCheck(_rowIndex, COL_DX4_PTR, CheckEnum.Checked);
                                                                e1 = new RowColEventArgs(_rowIndex, COL_DX4_CODE);
                                                                e2.oType = TransactionLineColumnType.Diagnosis;
                                                                e2.code = _dxCode;
                                                                e2.description = _dxDesc;
                                                                e2.isdeleted = false;
                                                                onInsCPTDxMod_Changed(null, e1, e2);
                                                                e2.oType = TransactionLineColumnType.CPT;
                                                                //oNote = new CellNote(c1Transaction[_rowIndex, COL_DX4_DESC].ToString());
                                                                //rg = c1Transaction.GetCellRange(_rowIndex, COL_DX4_CODE);
                                                                //rg.UserData = oNote;
                                                                _dxSetCount = 4;
                                                                break;
                                                        }
                                                        if (_NoOfDiagnosis == _dxSetCount) { break; }
                                                    }
                                                    _dxCode = "";
                                                    _dxDesc = "";
                                                }
                                            }

                                            #endregion

                                            #region " Load Modifiers "

                                            c1Transaction.SetData(_rowIndex, COL_MOD1_CODE, "");
                                            c1Transaction.SetData(_rowIndex, COL_MOD1_DESC, "");
                                            c1Transaction.SetData(_rowIndex, COL_MOD2_CODE, "");
                                            c1Transaction.SetData(_rowIndex, COL_MOD2_DESC, "");
                                            c1Transaction.SetData(_rowIndex, COL_MOD3_CODE, "");
                                            c1Transaction.SetData(_rowIndex, COL_MOD3_DESC, "");
                                            c1Transaction.SetData(_rowIndex, COL_MOD4_CODE, "");
                                            c1Transaction.SetData(_rowIndex, COL_MOD4_DESC, "");


                                            if (oScrubber.Modifiers != null && oScrubber.Modifiers.Count > 0)
                                            {
                                                IsMODLoadedFromScrubber = true;

                                                string _modCode = "";
                                                string _modDesc = "";
                                                int _modSetCount = 0;
                                                //CellNote oNote = null;
                                                //CellRange rg;

                                                for (int modIndex = 0; modIndex < oScrubber.Modifiers.Count; modIndex++)
                                                {
                                                    _modCode = oScrubber.Modifiers[modIndex].Code;
                                                    _modDesc = oScrubber.Modifiers[modIndex].Description;
                                                    //oNote = null;
                                                    //rg = new CellRange();

                                                    if (_modCode.Trim() != "")
                                                    {
                                                        switch (modIndex)
                                                        {
                                                            case 0:
                                                                c1Transaction.SetData(_rowIndex, COL_MOD1_CODE, _modCode);
                                                                c1Transaction.SetData(_rowIndex, COL_MOD1_DESC, _modDesc);
                                                                //oNote = new CellNote(c1Transaction[_rowIndex, COL_MOD1_DESC].ToString());
                                                                //rg = c1Transaction.GetCellRange(_rowIndex, COL_MOD1_CODE);
                                                                //rg.UserData = oNote;
                                                                _modSetCount = 1;
                                                                break;
                                                            case 1:
                                                                c1Transaction.SetData(_rowIndex, COL_MOD2_CODE, _modCode);
                                                                c1Transaction.SetData(_rowIndex, COL_MOD2_DESC, _modDesc);
                                                                //oNote = new CellNote(c1Transaction[_rowIndex, COL_MOD2_DESC].ToString());
                                                                //rg = c1Transaction.GetCellRange(_rowIndex, COL_MOD2_CODE);
                                                                //rg.UserData = oNote;
                                                                _modSetCount = 2;
                                                                break;
                                                            case 2:
                                                                c1Transaction.SetData(_rowIndex, COL_MOD3_CODE, _modCode);
                                                                c1Transaction.SetData(_rowIndex, COL_MOD3_DESC, _modDesc);
                                                                //oNote = new CellNote(c1Transaction[_rowIndex, COL_MOD3_DESC].ToString());
                                                                //rg = c1Transaction.GetCellRange(_rowIndex, COL_MOD3_CODE);
                                                                //rg.UserData = oNote;
                                                                _modSetCount = 3;
                                                                break;
                                                            case 3:
                                                                c1Transaction.SetData(_rowIndex, COL_MOD4_CODE, _modCode);
                                                                c1Transaction.SetData(_rowIndex, COL_MOD4_DESC, _modDesc);
                                                                //oNote = new CellNote(c1Transaction[_rowIndex, COL_MOD4_DESC].ToString());
                                                                //rg = c1Transaction.GetCellRange(_rowIndex, COL_MOD4_CODE);
                                                                //rg.UserData = oNote;
                                                                _modSetCount = 4;
                                                                break;
                                                        }

                                                        if (_NoOfModifiers == _modSetCount) { break; }
                                                    }
                                                    _modCode = "";
                                                    _modDesc = "";

                                                }
                                            }


                                            #endregion " Load Modifiers "

                                        }
                                    }
                                }

                                if (c1Transaction.GetData(1, COL_EMRTREATMENTLINENO) != null)
                                {
                                    if (Convert.ToInt32(c1Transaction.GetData(1, COL_EMRTREATMENTLINENO)) > 0)
                                    {
                                        SetCPTDefaults(_rowIndex, CPTChangedFrom.EMRTreatment);
                                    }
                                    else
                                    {
                                        SetCPTDefaults(_rowIndex, CPTChangedFrom.ManualEntry);
                                    }

                                }
                                else
                                {
                                    SetCPTDefaults(_rowIndex, CPTChangedFrom.ManualEntry);
                                }

                                sMammogramCertNo = oclsFeeSchedule.GetFacilityMammogramNumber(_cptCode, _facilityID);
                                

                            }
                            catch (Exception ex)
                            {
                                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                            }
                        }
                        #endregion " ** Load scrubber setting for CPT ** "

                        if (ogloGridListControl.ControlType == gloGridListControlType.CPT || ogloGridListControl.ControlType == gloGridListControlType.Modifier)
                        {
                            if (_AllowChargeModification == true)
                            {
                                #region " Load Fee Schedule "

                                string _whenSpecility = "";
                                string _whencpt = "";
                                string _whenmod = "";
                                Int64 _wheninsid = 0;
                                Int64 _fromDOS = 0;
                                Int64 _toDOS = 0;

                                if (c1Transaction.GetData(_rowIndex, COL_POSCODE) != null)
                                {
                                    _whenSpecility = c1Transaction.GetData(_rowIndex, COL_POSCODE).ToString();
                                }

                                if (_whenSpecility.Trim() == "") { _whenSpecility = "01"; }

                                if (c1Transaction.GetData(_rowIndex, COL_CPT_CODE) != null)
                                {
                                    _whencpt = c1Transaction.GetData(_rowIndex, COL_CPT_CODE).ToString();
                                }

                                if (c1Transaction.GetData(_rowIndex, COL_MOD1_CODE) != null)
                                {
                                    _whenmod = c1Transaction.GetData(_rowIndex, COL_MOD1_CODE).ToString();
                                }

                                if (c1Transaction.GetData(_rowIndex, COL_INSURANCEID) != null)
                                {
                                    if (Convert.ToString(c1Transaction.GetData(_rowIndex, COL_INSURANCEID)) != "")
                                    {
                                        _wheninsid = Convert.ToInt64(c1Transaction.GetData(_rowIndex, COL_INSURANCEID).ToString());
                                    }
                                }

                                if (c1Transaction.GetData(_rowIndex, COL_DATEFROM) != null)
                                {
                                    if (Convert.ToString(c1Transaction.GetData(_rowIndex, COL_DATEFROM)) != "")
                                    {
                                        _fromDOS = gloDateMaster.gloDate.DateAsNumber(c1Transaction.GetData(_rowIndex, COL_DATEFROM).ToString());
                                    }
                                }

                                if (c1Transaction.GetData(_rowIndex, COL_DATETO) != null)
                                {
                                    if (Convert.ToString(c1Transaction.GetData(_rowIndex, COL_DATETO)) != "")
                                    {
                                        _toDOS = gloDateMaster.gloDate.DateAsNumber(c1Transaction.GetData(_rowIndex, COL_DATETO).ToString());
                                    }
                                }

                                decimal _AllowedCharges = 0;
                                decimal _Actual_AllowedCharges = 0;
                                bool _isAllowedAmount = false;
                                bool _isChargeAmount = false;
                                decimal _Charges = 0;

                                if (_showTilldateColumn == true)
                                {
                                    if (_showTillDateColumnUseNullDate == true)
                                    {
                                        if (c1Transaction.GetData(_rowIndex, COL_DATETO) != null)
                                        {
                                            _toDOS = gloDateMaster.gloDate.DateAsNumber(c1Transaction.GetData(_rowIndex, COL_DATETO).ToString());
                                        }
                                        else
                                        {
                                            //to date will return null value that's why for handle error we are setting from date value 
                                            //otherwise there is no logic
                                            _toDOS = 0;
                                        }
                                    }
                                    else
                                    {
                                        if (c1Transaction.GetData(_rowIndex, COL_DATETO) != null && Convert.ToString(c1Transaction.GetData(_rowIndex, COL_DATETO)).Trim() != "")
                                        {
                                            _toDOS = gloDateMaster.gloDate.DateAsNumber(c1Transaction.GetData(_rowIndex, COL_DATETO).ToString());
                                        }
                                        else
                                        {
                                            _toDOS = gloDateMaster.gloDate.DateAsNumber(c1Transaction.GetData(_rowIndex, COL_DATEFROM).ToString());
                                        }
                                    }
                                }
                                else
                                {

                                    _toDOS = gloDateMaster.gloDate.DateAsNumber(c1Transaction.GetData(_rowIndex, COL_DATEFROM).ToString());
                                }

                                oclsFeeSchedule.GetCPTFees(_whencpt, _whenmod, _transactionDetailId, _fromDOS, _toDOS, _FeeScheduleID, _PatientProviderID, _nContactID, DefaultChargesType.GetHashCode(), out _AllowedCharges, out _isAllowedAmount, out _Charges, out _isChargeAmount, out _Fee_ScheduleID);

                                _DefaultCPT_CLIAno = oclsFeeSchedule.GetDefaultCPT_CLIAno(_whencpt);

                                _bDefaultSelf = oclsFeeSchedule.GetDefaultSelf(_whencpt);
                                Decimal _Unit = 0;

                                if (c1Transaction.GetData(c1Transaction.RowSel, COL_UNIT) != null && Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_UNIT)).Trim() != "")
                                { _Unit = Convert.ToDecimal(c1Transaction.GetData(c1Transaction.RowSel, COL_UNIT)); }


                                if (_Unit > 0)
                                {
                                    _Actual_AllowedCharges = _AllowedCharges;
                                    _AllowedCharges = _AllowedCharges * _Unit;
                                }

                                if (_codeColIndex >= COL_MOD1_CODE && _codeColIndex <= COL_MOD4_CODE)
                                {
                                    //Do not change sequence of setting Allowed and Charge Amount
                                    //Creates a sorting issue
                                    if (_isAllowedAmount)
                                    {
                                        c1Transaction.SetData(_rowIndex, COL_ALLOWED, _AllowedCharges);
                                        c1Transaction.SetData(_rowIndex, COL_ACTUAL_ALLOWED, _Actual_AllowedCharges);
                                    }
                                    else
                                    {
                                        c1Transaction.SetData(_rowIndex, COL_ALLOWED, null);
                                        c1Transaction.SetData(_rowIndex, COL_ACTUAL_ALLOWED, null);
                                    }

                                    if (_isChargeAmount == true && ChangeAmount(_rowIndex, _Charges) == true)
                                    {
                                        { c1Transaction.SetData(_rowIndex, COL_CHARGES, _Charges); }
                                    }


                                }
                                else
                                {
                                    //Do not change sequence of setting Allowed and Charge Amount
                                    //Creates a sorting issue
                                    if (_isAllowedAmount)
                                    {
                                        c1Transaction.SetData(_rowIndex, COL_ALLOWED, _AllowedCharges);
                                        c1Transaction.SetData(_rowIndex, COL_ACTUAL_ALLOWED, _Actual_AllowedCharges);
                                    }
                                    else
                                    {
                                        c1Transaction.SetData(_rowIndex, COL_ALLOWED, null);
                                        c1Transaction.SetData(_rowIndex, COL_ACTUAL_ALLOWED, null);
                                    }
                                    if (_isChargeAmount == true && ChangeAmount(_rowIndex, _Charges) == true)
                                    {
                                        c1Transaction.SetData(_rowIndex, COL_CHARGES, _Charges);
                                    }


                                }

                                #endregion

                                #region " Retrive CPT Charges If Exist Override the Fee Schedule "

                                //decimal _cptcharges = 0;

                                //decimal _cptunits = 0;

                                //if (_Charges == 0)
                                //{
                                //    if (oclsFeeSchedule.GetCPTCharges(_whencpt, out _cptcharges, out _cptunits) == true)
                                //    {
                                //        _Fee_ScheduleID = 0;
                                //        _Fee_ScheduleType = FeeScheduleType.CPT;
                                //        c1Transaction.SetData(_rowIndex, COL_CHARGES, _cptcharges);
                                //        c1Transaction.SetData(_rowIndex, COL_UNIT,FormatNumber(_cptunits)); 
                                //        onCPTCharges_Load(null, this.DefaultChargesType);
                                //    }
                                //}
                                #endregion " Retrive CPT Charges If Exist Override the Fee Schedule "

                                if (ogloGridListControl.ControlType == gloGridListControlType.CPT)
                                {
                                    #region "Set Default CLIA# for Selected CPT"
                                    if (_DefaultCPT_CLIAno.Trim() != "" && _showLabColumn == true) //changes for refer admin settings ShowLabColumns Sameer 15-May-2015
                                    {
                                        c1Transaction.SetCellCheck(_rowIndex, COL_ISLABCPT, CheckEnum.Checked);
                                        c1Transaction.SetData(_rowIndex, COL_AUTHORIZATIONNO, _DefaultCPT_CLIAno);
                                        c1Transaction.Rows[_rowIndex].UserData = true;
                                    }
                                    else
                                    {
                                        if (Convert.ToBoolean(c1Transaction.Rows[_rowIndex].UserData))
                                        {
                                            c1Transaction.SetCellCheck(_rowIndex, COL_ISLABCPT, CheckEnum.Unchecked);
                                            c1Transaction.SetData(_rowIndex, COL_AUTHORIZATIONNO, "");
                                        }

                                    }
                                    #endregion
                                    if (_IsOpenForModify == false)
                                    {
                                        #region "Set defaut Self claim of CPT"
                                        if (_bDefaultSelf == true && _showSplitClaimToPatient == true)
                                        {
                                            c1Transaction.SetCellCheck(_rowIndex, COL_SELFCLAIM, CheckEnum.Checked);
                                        }
                                        else
                                        {
                                            c1Transaction.SetCellCheck(_rowIndex, COL_SELFCLAIM, CheckEnum.Unchecked);
                                        }
                                    }
                                        #endregion
                                }
                            }
                        }
                        else if (ogloGridListControl.ControlType == gloGridListControlType.ICD9)
                        {
                            #region " Set Diagnosis Pointers "

                            if (_codeColIndex == COL_DX1_CODE)
                            {
                                c1Transaction.SetCellCheck(_rowIndex, COL_DX1_PTR, CheckEnum.Checked);
                            }
                            else if (_codeColIndex == COL_DX2_CODE)
                            {
                                c1Transaction.SetCellCheck(_rowIndex, COL_DX2_PTR, CheckEnum.Checked);
                            }
                            else if (_codeColIndex == COL_DX3_CODE)
                            {
                                c1Transaction.SetCellCheck(_rowIndex, COL_DX3_PTR, CheckEnum.Checked);
                            }
                            else if (_codeColIndex == COL_DX4_CODE)
                            {
                                c1Transaction.SetCellCheck(_rowIndex, COL_DX4_PTR, CheckEnum.Checked);
                            }
                            else if (_codeColIndex == COL_DX5_CODE)
                            {
                                c1Transaction.SetCellCheck(_rowIndex, COL_DX5_PTR, CheckEnum.Checked);
                            }
                            else if (_codeColIndex == COL_DX6_CODE)
                            {
                                c1Transaction.SetCellCheck(_rowIndex, COL_DX6_PTR, CheckEnum.Checked);
                            }
                            else if (_codeColIndex == COL_DX7_CODE)
                            {
                                c1Transaction.SetCellCheck(_rowIndex, COL_DX7_PTR, CheckEnum.Checked);
                            }
                            else if (_codeColIndex == COL_DX8_CODE)
                            {
                                c1Transaction.SetCellCheck(_rowIndex, COL_DX8_PTR, CheckEnum.Checked);
                            }

                            #endregion


                        }
                        //Load Default Allowed Charges and Clinic Fees - Vinayak - 26th Sep 2008

                        //CloseInternalControl();

                        //Code added on 20081013 By - Sagar Ghodke
                        MoveNext();
                        //End code added - 20081013,Sagar Ghodke


                        //...*** Code added on 20090819 by - Sagar Ghodke for sort control issue 
                        SortControl();
                        //...*** End code add on 20090819  by - Sagar Ghodke for sort control issue


                    }
                    else
                    {
                        string _SearchText = "";
                        if (ogloGridListControl.ControlSearchText != null && ogloGridListControl.ControlSearchText.Trim() != "")
                        {
                            _SearchText = ogloGridListControl.ControlSearchText.Trim();
                        }

                        if (ogloGridListControl.ControlType == gloGridListControlType.CPT)
                        {
                            _codeColIndex = ogloGridListControl.ParentColIndex;
                            _descColIndex = ogloGridListControl.ParentColIndex + 1;
                            //CloseInternalControl();

                            #region "Add CPT"
                            frmSetupCPT ofrmCPT = new frmSetupCPT(_DatabaseConnectionString);
                            ofrmCPT.CPTCode = _SearchText;
                            ofrmCPT.ShowDialog(this);
                            if (ofrmCPT.CPTID > 0)
                            {
                                c1Transaction.SetData(_rowIndex, _codeColIndex, ofrmCPT.CPTCode);
                                c1Transaction.SetData(_rowIndex, _descColIndex, ofrmCPT.CPTDescription);

                                //solving issue TFSID-1612(mantis id-134)
                                //After Adding New CPT from Service Line,Units,Charges,Allowed assigned to that CPT

                                //Condition Added By Debasish (Issue ID:4701 Under 5070)
                                if (ofrmCPT.Units > 0)
                                {
                                    //c1Transaction.SetData(_rowIndex, COL_UNIT, ofrmCPT.Units);
                                    //c1Transaction.SetData(_rowIndex, COL_UNIT, 1);
                                    c1Transaction.SetData(_rowIndex, COL_UNIT, gloCharges.FormatNumber(ofrmCPT.Units));
                                }
                                else
                                    c1Transaction.SetData(_rowIndex, COL_UNIT, 1);
                                //**

                                c1Transaction.SetData(_rowIndex, COL_CHARGES, ofrmCPT.Charges);

                                //************* end
                                _evncode = ofrmCPT.CPTCode;
                                _evndescription = ofrmCPT.CPTDescription;
                                e2.oType = TransactionLineColumnType.CPT;
                            }
                            _DefaultCPT_CLIAno = oclsFeeSchedule.GetDefaultCPT_CLIAno(ofrmCPT.CPTCode);

                            #region "Set Default CLIA# for Selected CPT"
                            if (_DefaultCPT_CLIAno.Trim() != "" && _showLabColumn == true) //changes for refer admin settings ShowLabColumns Sameer 15-May-2015
                            {
                                c1Transaction.SetCellCheck(_rowIndex, COL_ISLABCPT, CheckEnum.Checked);
                                c1Transaction.SetData(_rowIndex, COL_AUTHORIZATIONNO, _DefaultCPT_CLIAno);
                                c1Transaction.Rows[_rowIndex].UserData = true;
                            }
                            else
                            {
                                if (Convert.ToBoolean(c1Transaction.Rows[_rowIndex].UserData))
                                {
                                    c1Transaction.SetCellCheck(_rowIndex, COL_ISLABCPT, CheckEnum.Unchecked);
                                    c1Transaction.SetData(_rowIndex, COL_AUTHORIZATIONNO, "");
                                }

                            }
                            #endregion
                            if (_IsOpenForModify == false)
                            {
                                _bDefaultSelf = oclsFeeSchedule.GetDefaultSelf(ofrmCPT.CPTCode);
                                #region "Set defaut Self claim of CPT"
                                if (_bDefaultSelf == true && _showSplitClaimToPatient == true)
                                {
                                    c1Transaction.SetCellCheck(_rowIndex, COL_SELFCLAIM, CheckEnum.Checked);
                                }
                                else
                                {
                                    c1Transaction.SetCellCheck(_rowIndex, COL_SELFCLAIM, CheckEnum.Unchecked);
                                }
                            }
                                #endregion
                            ofrmCPT.Dispose();
                            #endregion

                        }
                        else if (ogloGridListControl.ControlType == gloGridListControlType.ICD9)
                        {
                            _codeColIndex = ogloGridListControl.ParentColIndex;
                            _descColIndex = ogloGridListControl.ParentColIndex + 1;
                            //CloseInternalControl();

                            #region "Add ICD9"
                            //(Int16)IcdCodeType                            
                            frmSetupICD9 ofrmICD9 = new frmSetupICD9(SelectedICD, _DatabaseConnectionString);
                            ofrmICD9.tsb_save.Visible = false;
                            ofrmICD9.ICD9Code = _SearchText;
                            ofrmICD9.ShowDialog(this);
                            if (ofrmICD9.nICD9ID > 0)
                            {
                                c1Transaction.SetData(_rowIndex, _codeColIndex, ofrmICD9.ICD9Code);
                                c1Transaction.SetData(_rowIndex, _descColIndex, ofrmICD9.ICD9Description);

                                //
                                #region " Set Diagnosis Pointers "

                                if (_codeColIndex == COL_DX1_CODE)
                                {
                                    c1Transaction.SetCellCheck(_rowIndex, COL_DX1_PTR, CheckEnum.Checked);
                                }
                                else if (_codeColIndex == COL_DX2_CODE)
                                {
                                    c1Transaction.SetCellCheck(_rowIndex, COL_DX2_PTR, CheckEnum.Checked);
                                }
                                else if (_codeColIndex == COL_DX3_CODE)
                                {
                                    c1Transaction.SetCellCheck(_rowIndex, COL_DX3_PTR, CheckEnum.Checked);
                                }
                                else if (_codeColIndex == COL_DX4_CODE)
                                {
                                    c1Transaction.SetCellCheck(_rowIndex, COL_DX4_PTR, CheckEnum.Checked);
                                }
                                else if (_codeColIndex == COL_DX5_CODE)
                                {
                                    c1Transaction.SetCellCheck(_rowIndex, COL_DX5_PTR, CheckEnum.Checked);
                                }
                                else if (_codeColIndex == COL_DX6_CODE)
                                {
                                    c1Transaction.SetCellCheck(_rowIndex, COL_DX6_PTR, CheckEnum.Checked);
                                }
                                else if (_codeColIndex == COL_DX7_CODE)
                                {
                                    c1Transaction.SetCellCheck(_rowIndex, COL_DX7_PTR, CheckEnum.Checked);
                                }
                                else if (_codeColIndex == COL_DX8_CODE)
                                {
                                    c1Transaction.SetCellCheck(_rowIndex, COL_DX8_PTR, CheckEnum.Checked);
                                }

                                #endregion

                                //

                                //MaheshB 20091212
                                _evncode = ofrmICD9.ICD9Code;
                                _evndescription = ofrmICD9.ICD9Description;
                                e2.oType = TransactionLineColumnType.Diagnosis;

                            }
                            ofrmICD9.Dispose();
                            #endregion
                        }
                        else if (ogloGridListControl.ControlType == gloGridListControlType.Modifier)
                        {
                            _codeColIndex = ogloGridListControl.ParentColIndex;
                            _descColIndex = ogloGridListControl.ParentColIndex + 1;
                            //CloseInternalControl();

                            #region "Add Modifier"
                            frmSetupModifier ofrmModifier = new frmSetupModifier(_DatabaseConnectionString);
                            ofrmModifier.ModifierCode = _SearchText;
                            ofrmModifier.ShowDialog(this);
                            if (ofrmModifier.ModifierID > 0)
                            {
                                c1Transaction.SetData(_rowIndex, _codeColIndex, ofrmModifier.ModifierCode);
                                c1Transaction.SetData(_rowIndex, _descColIndex, ofrmModifier.ModifierDescription);
                                _evncode = ofrmModifier.ModifierCode;
                                _evndescription = ofrmModifier.ModifierDescription;
                                e2.oType = TransactionLineColumnType.Modifier;
                            }
                            ofrmModifier.Dispose();
                            #endregion ""
                        }

                    }

                    //Start Sagar G: 23Apr2014, Code not in use commented
                    //CellNotes.CellNoteManager mgr = new CellNotes.CellNoteManager(c1Transaction);
                    //End Sagar G: 23Apr2014, Code not in use commented

                }

                e2.code = _evncode;
                e2.description = _evndescription;
                e2.isdeleted = false;
                onInsCPTDxMod_Changed(null, e1, e2);

                //if (c1Transaction.GetData(c1Transaction.Row, COL_LINEPRIMARY_DXCODE) == null
                //    || Convert.ToString(c1Transaction.GetData(c1Transaction.Row, COL_LINEPRIMARY_DXCODE)).Trim() == "")
                //{
                string _tempDxCode = "";
                string _tempDxDesc = "";

                _tempDxCode = Convert.ToString(c1Transaction.GetData(c1Transaction.Row, COL_DX1_CODE));
                _tempDxDesc = Convert.ToString(c1Transaction.GetData(c1Transaction.Row, COL_DX1_DESC));

                c1Transaction.SetData(c1Transaction.Row, COL_LINEPRIMARY_DXCODE, _tempDxCode);
                c1Transaction.SetData(c1Transaction.Row, COL_LINEPRIMARY_DXDESC, _tempDxDesc);

                onGrid_SelChanged(null, null);
                //}

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                IsInternalControlActive = false; //CloseInternalControl();
                _isItemSelectionCall = false;
            }
        }

        void ogloGridListControl_InternalGridKeyDown(object sender, EventArgs e)
        {
            try
            {
                IsInternalControlActive = false; //CloseInternalControl();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            { }
        }

        private void FindFirstEmptyDxSlot(int rowIndex, int editedColIndex, out int firstemptyDxCodeColIndex, out int firstemptyDxDescColIndex)
        {
            firstemptyDxCodeColIndex = editedColIndex;
            firstemptyDxDescColIndex = editedColIndex + 1;

            try
            {
                if (editedColIndex == COL_DX1_CODE)
                {
                    firstemptyDxCodeColIndex = COL_DX1_CODE;
                    firstemptyDxDescColIndex = COL_DX1_CODE + 1;
                }
                else if (editedColIndex > COL_DX1_CODE && editedColIndex <= COL_DX8_CODE)
                {
                    for (int dxColIndex = COL_DX1_CODE; dxColIndex <= editedColIndex; dxColIndex = (dxColIndex + 3))
                    {
                        if (c1Transaction.GetData(rowIndex, dxColIndex) == null || Convert.ToString(c1Transaction.GetData(rowIndex, dxColIndex)).Trim() == "")
                        {
                            firstemptyDxCodeColIndex = dxColIndex;
                            firstemptyDxDescColIndex = dxColIndex + 1;
                            break;
                        }
                    }
                }
            }
            catch (Exception)
            {
                firstemptyDxCodeColIndex = editedColIndex;
                firstemptyDxDescColIndex = editedColIndex + 1;
            }
        }

        private void FindFirstEmptyModSlot(int rowIndex, int editedColIndex, out int firstemptyModCodeColIndex, out int firstemptyModDescColIndex)
        {
            firstemptyModCodeColIndex = editedColIndex;
            firstemptyModDescColIndex = editedColIndex + 1;

            try
            {
                if (editedColIndex == COL_MOD1_CODE)
                {
                    firstemptyModCodeColIndex = COL_MOD1_CODE;
                    firstemptyModDescColIndex = COL_MOD1_CODE + 1;
                }
                else if (editedColIndex > COL_MOD1_CODE && editedColIndex <= COL_MOD4_CODE)
                {
                    for (int dxColIndex = COL_MOD1_CODE; dxColIndex <= editedColIndex; dxColIndex = (dxColIndex + 3))
                    {
                        if (c1Transaction.GetData(rowIndex, dxColIndex) == null || Convert.ToString(c1Transaction.GetData(rowIndex, dxColIndex)).Trim() == "")
                        {
                            firstemptyModCodeColIndex = dxColIndex;
                            firstemptyModDescColIndex = dxColIndex + 1;
                            break;
                        }
                    }
                }
            }
            catch (Exception)
            {
                firstemptyModCodeColIndex = editedColIndex;
                firstemptyModDescColIndex = editedColIndex + 1;
            }
        }



        #endregion " Delegated Events of Internal Control "

        #region " C1 Grid Events "


        private void c1Transaction_CellChanged(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            //    int _colCheckedIndex = 0;
            try
            {

                if (e.Row == c1Transaction.RowSel)
                {
                    if (onGrid_CellChanged != null)
                    {
                        onGrid_CellChanged(sender, e);
                    }
                    if (e.Col == COL_AUTHORIZATIONNO)
                    {
                        if (e.Row == 1) 
                        {
                            if (Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_AUTHORIZATIONNO)) != "")
                            {
                                _sFirstCPTLineCLIANo = Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_AUTHORIZATIONNO));
                                CLIA_Enter();
                            }
                            else
                            { _sFirstCPTLineCLIANo = ""; }
                        }
                        
                    }
                    if (e.Col == COL_CHARGES || e.Col == COL_UNIT)
                    {
                        decimal _Total = 0;
                        decimal _Charges = 0;
                        decimal _Unit = 0;
                        decimal _Allowed = 0;
                        decimal _TotalAllowed = 0;
                        decimal _ActualAllowed = 0;

                        if (c1Transaction.GetData(c1Transaction.RowSel, COL_CHARGES) != null && Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_CHARGES)).Trim() != "")
                        {
                            _Charges = Convert.ToDecimal(c1Transaction.GetData(c1Transaction.RowSel, COL_CHARGES));
                        }

                        if (c1Transaction.GetData(c1Transaction.RowSel, COL_UNIT) != null && Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_UNIT)).Trim() != "")
                        { _Unit = Convert.ToDecimal(c1Transaction.GetData(c1Transaction.RowSel, COL_UNIT)); }

                        if (c1Transaction.GetData(c1Transaction.RowSel, COL_ALLOWED) != null && Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_ALLOWED)).Trim() != "")
                        { _Allowed = Convert.ToDecimal(c1Transaction.GetData(c1Transaction.RowSel, COL_ALLOWED)); }

                        if (c1Transaction.GetData(c1Transaction.RowSel, COL_ACTUAL_ALLOWED) != null && Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_ACTUAL_ALLOWED)).Trim() != "")
                        { _ActualAllowed = Convert.ToDecimal(c1Transaction.GetData(c1Transaction.RowSel, COL_ACTUAL_ALLOWED)); }


                        if (_Unit <= 0)
                        {
                            _Unit = 1;
                            _Total = _Charges;
                            _TotalAllowed = _Charges;
                        }
                        else
                        {
                            _Total = _Charges * _Unit;
                            //_TotalAllowed = _Allowed * _Unit;
                            if (_ActualAllowed > 0 && _Unit > 0)
                            { _TotalAllowed = _ActualAllowed * _Unit; }
                            else
                            { _TotalAllowed = _Allowed * _Unit; }
                        }


                        c1Transaction.SetData(c1Transaction.RowSel, COL_TOTAL, _Total);

                        if (Convert.ToDecimal(c1Transaction.GetData(c1Transaction.RowSel, COL_ALLOWED)) > 0)
                            c1Transaction.SetData(c1Transaction.RowSel, COL_ALLOWED, _TotalAllowed);

                        if (_isItemSelectionCall == false && _isControlInDesignMode == false)
                        { SortControl(); }


                    }
                    if (e.Col == COL_ALLOWED)
                    {
                        decimal _allowedAmt = 0;
                        decimal _actualAmt = 0;
                        decimal _unit = 1;

                        if (c1Transaction.GetData(c1Transaction.RowSel, COL_ALLOWED) != null && Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_ALLOWED)).Trim() != "")
                        { _allowedAmt = Convert.ToDecimal(c1Transaction.GetData(c1Transaction.RowSel, COL_ALLOWED)); }

                        if (c1Transaction.GetData(c1Transaction.RowSel, COL_UNIT) != null && Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_UNIT)).Trim() != "")
                        { _unit = Convert.ToDecimal(c1Transaction.GetData(c1Transaction.RowSel, COL_UNIT)); }

                        if (_allowedAmt > 0 && _unit > 0)
                        { _actualAmt = (_allowedAmt / _unit); }
                        else
                        { _actualAmt = _allowedAmt; }

                        c1Transaction.SetData(c1Transaction.RowSel, COL_ACTUAL_ALLOWED, _actualAmt);

                        if (_isItemSelectionCall == false && _isControlInDesignMode == false)
                        { SortControl(); }
                    }

                    switch (e.Col)
                    {
                        case COL_DX1_PTR:
                            SetPointer(e.Row, e.Col, COL_DX1_CODE);
                            break;
                        case COL_DX2_PTR:
                            SetPointer(e.Row, e.Col, COL_DX2_CODE);
                            break;
                        case COL_DX3_PTR:
                            SetPointer(e.Row, e.Col, COL_DX3_CODE);
                            break;
                        case COL_DX4_PTR:
                            SetPointer(e.Row, e.Col, COL_DX4_CODE);
                            break;
                        case COL_DX5_PTR:
                            SetPointer(e.Row, e.Col, COL_DX5_CODE);
                            break;
                        case COL_DX6_PTR:
                            SetPointer(e.Row, e.Col, COL_DX6_CODE);
                            break;
                        case COL_DX7_PTR:
                            SetPointer(e.Row, e.Col, COL_DX7_CODE);
                            break;
                        case COL_DX8_PTR:
                            SetPointer(e.Row, e.Col, COL_DX8_CODE);
                            break;
                        case COL_ISLABCPT:
                            {
                                ClsFeeSchedule oclsFeeSchedule = new ClsFeeSchedule(_DatabaseConnectionString);
                                try
                                {
                                    if (Convert.ToString(c1Transaction.GetData(e.Row, COL_CPT_CODE)) == "")
                                    {
                                        if (c1Transaction.GetCellCheck(e.Row, e.Col) == CheckEnum.Unchecked)
                                        { c1Transaction.SetData(e.Row, COL_AUTHORIZATIONNO, ""); }
                                        else if (c1Transaction.GetCellCheck(e.Row, e.Col) == CheckEnum.Checked)
                                        {
                                            c1Transaction.SetData(e.Row, COL_AUTHORIZATIONNO, FacilityCLIANo);
                                            c1Transaction.Rows[e.Row].UserData = false;                                            
                                        }
                                    }
                                    else
                                    {
                                        _DefaultCPT_CLIAno = oclsFeeSchedule.GetDefaultCPT_CLIAno(Convert.ToString(c1Transaction.GetData(e.Row, COL_CPT_CODE)));
                                        if (_DefaultCPT_CLIAno != "" && _showLabColumn == true) //changes for refer admin settings ShowLabColumns Sameer 15-May-2015
                                        {
                                            if (c1Transaction.GetCellCheck(e.Row, e.Col) == CheckEnum.Unchecked)
                                            { c1Transaction.SetData(e.Row, COL_AUTHORIZATIONNO, ""); }
                                            else if (c1Transaction.GetCellCheck(e.Row, e.Col) == CheckEnum.Checked)
                                            {
                                                c1Transaction.SetData(e.Row, COL_AUTHORIZATIONNO, _DefaultCPT_CLIAno);
                                                c1Transaction.Rows[e.Row].UserData = true;
                                            }                                            
                                        }
                                        else
                                        {
                                            if (c1Transaction.GetCellCheck(e.Row, e.Col) == CheckEnum.Unchecked)
                                            { c1Transaction.SetData(e.Row, COL_AUTHORIZATIONNO, ""); }
                                            else if (c1Transaction.GetCellCheck(e.Row, e.Col) == CheckEnum.Checked)
                                            {
                                                c1Transaction.SetData(e.Row, COL_AUTHORIZATIONNO, FacilityCLIANo);
                                                c1Transaction.Rows[e.Row].UserData = false;                                                                                                
                                            }
                                        }
                                        // code added for Default Self Self Claim for Selected CPT
                                        if (_IsOpenForModify == false)
                                        {
                                            _bDefaultSelf = oclsFeeSchedule.GetDefaultSelf(Convert.ToString(c1Transaction.GetData(e.Row, COL_CPT_CODE)));
                                            #region "Set defaut Self claim of CPT"
                                            if (_bDefaultSelf == true && _showSplitClaimToPatient == true)
                                            {
                                                c1Transaction.SetCellCheck(e.Row, COL_SELFCLAIM, CheckEnum.Checked);
                                            }
                                            else
                                            {
                                                c1Transaction.SetCellCheck(e.Row, COL_SELFCLAIM, CheckEnum.Unchecked);
                                            }
                                        }
                                            #endregion

                                    }
                                    CLIA_Enter();
                                }
                                catch (Exception ex)
                                {
                                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                                    ex = null;
                                }

                                finally
                                {
                                    if (oclsFeeSchedule != null)
                                    {
                                        oclsFeeSchedule.Dispose();
                                        oclsFeeSchedule = null;
                                    }
                                }

                            }
                            break;
                        case COL_ISEMG:
                            SetPointer(e.Row, e.Col, COL_ISEMG);
                            break;
                        case COL_SELFCLAIM:
                            setnewAllowedAmount();
                            break;
                    }

                    #region " Show Total "

                    c1Total.SetData(0, COL_CHARGES, GetTotalCharges());
                    c1Total.SetData(0, COL_ALLOWED, GetTotalAllowed());
                    c1Total.SetData(0, COL_TOTAL, GetGrandTotal());

                    #endregion

                } // onGrid_CellChanged(sender, e);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void c1Transaction_StartEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            try
            {
                //if (e.Row > 0)
                //{
                //    CellNote _cellNote = null;
                //    CellRange _cellRange = c1Transaction.GetCellRange(e.Row, e.Col);
                //    _cellRange.UserData = _cellNote;
                //}

                switch (e.Col)
                {

                    case COL_POSCODE:
                        {

                            //e.Cancel = true;
                            OpenInternalControl(gloGridListControlType.POS, "Place Of Service", false, e.Row, e.Col, "");
                            string _SearchText = "";
                            if (c1Transaction != null && c1Transaction.Rows.Count > 0)
                            {
                                _SearchText = Convert.ToString(c1Transaction.GetData(CurrentTransactionLine, e.Col));
                                if (_SearchText != "" && ogloGridListControl != null)
                                {
                                    ogloGridListControl.AdvanceSearch(_SearchText);
                                }
                            }
                        }
                        break;
                    case COL_TOSCODE:
                        {
                            //e.Cancel = true;
                            OpenInternalControl(gloGridListControlType.TOS, "Type Of Service", false, e.Row, e.Col, "");
                        }
                        break;
                    case COL_CPT_CODE:
                        {
                            //e.Cancel = true;
                            OpenInternalControl(gloGridListControlType.CPT, "CPT", false, e.Row, e.Col, "");
                            string _SearchText = "";
                            if (c1Transaction != null && c1Transaction.Rows.Count > 0)
                            {
                                _SearchText = Convert.ToString(c1Transaction.GetData(CurrentTransactionLine, COL_CPT_CODE));
                                if (_SearchText != "" && ogloGridListControl != null)
                                {
                                    ogloGridListControl.FillControl(_SearchText);
                                }
                            }
                        }
                        break;
                    case COL_DX1_CODE:
                    case COL_DX2_CODE:
                    case COL_DX3_CODE:
                    case COL_DX4_CODE:
                    case COL_DX5_CODE:
                    case COL_DX6_CODE:
                    case COL_DX7_CODE:
                    case COL_DX8_CODE:
                        {
                            //e.Cancel = true;
                            DxModified(e.Row, e.Col);

                            string _sControlheader = "ICD9";
                            if (IcdCodeType == gloGlobal.gloICD.CodeRevision.ICD10.GetHashCode())
                            {
                                _sControlheader = "ICD10";
                            }

                            OpenInternalControl(gloGridListControlType.ICD9, _sControlheader, false, e.Row, e.Col, "", Convert.ToString(GetItem(e.Row, e.Col)), Convert.ToString(GetItem(e.Row, e.Col + 1)), true);
                            string _SearchText = "";
                            if (c1Transaction != null && c1Transaction.Rows.Count > 0)
                            {
                                _SearchText = Convert.ToString(c1Transaction.GetData(CurrentTransactionLine, e.Col));
                                if (_SearchText != "" && ogloGridListControl != null)
                                {
                                    ogloGridListControl.FillControl(_SearchText);
                                }
                            }
                        }
                        break;
                    case COL_MOD1_CODE:
                    case COL_MOD2_CODE:
                    case COL_MOD3_CODE:
                    case COL_MOD4_CODE:
                        {
                            //e.Cancel = true;
                            //OpenInternalControl(gloGridListControlType.Modifier, "Modifier", false, e.Row, e.Col, "");
                            string _cptCode = "";
                            string _facilityCode = "";

                            if (c1Transaction != null && c1Transaction.Rows.Count > 0)
                            {
                                _cptCode = Convert.ToString(c1Transaction.GetData(CurrentTransactionLine, COL_CPT_CODE));
                                _facilityCode = Convert.ToString(c1Transaction.GetData(CurrentTransactionLine, COL_POSCODE));
                            }
                            OpenInternalControl(gloGridListControlType.Modifier, "Modifier", false, e.Row, e.Col, "", _cptCode, _facilityCode);
                            string _SearchText = "";
                            if (c1Transaction != null && c1Transaction.Rows.Count > 0)
                            {
                                _SearchText = Convert.ToString(c1Transaction.GetData(CurrentTransactionLine, e.Col));
                                if (_SearchText != "" && ogloGridListControl != null)
                                {
                                    //Commented and Added By Debasish Das
                                    //ogloGridListControl.AdvanceSearch(_SearchText);
                                    ogloGridListControl.FillControl(_SearchText);
                                    //***
                                }
                            }
                        }
                        break;
                    case COL_PROVIDER:
                        {
                            //e.Cancel = true;
                            OpenInternalControl(gloGridListControlType.Providers, "Provider", false, e.Row, e.Col, "");
                            string _SearchText = "";
                            if (c1Transaction != null && c1Transaction.Rows.Count > 0)
                            {
                                _SearchText = Convert.ToString(c1Transaction.GetData(CurrentTransactionLine, e.Col));
                                if (_SearchText != "" && ogloGridListControl != null)
                                {
                                    ogloGridListControl.AdvanceSearch(_SearchText);
                                }
                            }
                        }
                        break;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
            }
        }

        private void tmrChangeEditSearch_Tick(object sender, EventArgs e)
        {
            try
            {
                if (this.tmrChangeEditSearch != null)
                {
                    this.tmrChangeEditSearch.Stop();
                }

                OnChangeEdit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {

            }

        }

        private volatile bool isSearchEventFired = false;
        private void OnChangeEdit()
        {
            string _strSearchString = "";
            try
            {
                if (c1Transaction.Editor == null)
                { _strSearchString = volatileSearchString; }
                else
                { _strSearchString = c1Transaction.Editor.Text; }

                if (ogloGridListControl != null)
                {
                    if (c1Transaction.Col == COL_CPT_CODE || c1Transaction.Col == COL_DX1_CODE || c1Transaction.Col == COL_DX2_CODE ||
                        c1Transaction.Col == COL_DX3_CODE || c1Transaction.Col == COL_DX4_CODE || c1Transaction.Col == COL_DX5_CODE ||
                        c1Transaction.Col == COL_DX6_CODE || c1Transaction.Col == COL_DX7_CODE || c1Transaction.Col == COL_DX8_CODE ||
                        c1Transaction.Col == COL_MOD1_CODE || c1Transaction.Col == COL_MOD2_CODE || c1Transaction.Col == COL_MOD3_CODE ||
                        c1Transaction.Col == COL_MOD4_CODE)
                    {
                        string _cptCode = "";
                        string _facilityCode = "";

                        if (c1Transaction != null && c1Transaction.Rows.Count > 0)
                        {
                            _cptCode = Convert.ToString(c1Transaction.GetData(c1Transaction.Row, COL_CPT_CODE));
                            _facilityCode = Convert.ToString(c1Transaction.GetData(c1Transaction.Row, COL_POSCODE));
                            ogloGridListControl.SelectedCPTCode = _cptCode;
                            ogloGridListControl.SelectedFacilityCode = _facilityCode;                            
                        }

                        ogloGridListControl.FillControl(_strSearchString);
                    }
                    else
                    {
                        ogloGridListControl.AdvanceSearch(_strSearchString);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                isSearchEventFired = true;
            }
        }

        private volatile string volatileSearchString = "";
        private void c1Transaction_ChangeEdit(object sender, EventArgs e)
        {
            //string _strSearchString = "";

            if (c1Transaction.Editor != null)
            { volatileSearchString = c1Transaction.Editor.Text; }
            else
            { volatileSearchString = ""; }


            if (ogloGridListControl != null)
            {
                if (c1Transaction.Col == COL_DX1_CODE || c1Transaction.Col == COL_DX2_CODE ||
                    c1Transaction.Col == COL_DX3_CODE || c1Transaction.Col == COL_DX4_CODE || c1Transaction.Col == COL_DX5_CODE ||
                    c1Transaction.Col == COL_DX6_CODE || c1Transaction.Col == COL_DX7_CODE || c1Transaction.Col == COL_DX8_CODE
                    )
                {
                    if (this.tmrChangeEditSearch != null)
                    {
                        this.tmrChangeEditSearch.Stop();
                        this.tmrChangeEditSearch.Start();
                        isSearchEventFired = false;
                    }
                }
                else if (c1Transaction.Col == COL_CPT_CODE || c1Transaction.Col == COL_MOD1_CODE || c1Transaction.Col == COL_MOD2_CODE ||
                     c1Transaction.Col == COL_MOD3_CODE || c1Transaction.Col == COL_MOD4_CODE)
                {
                    if (this.tmrChangeEditSearch != null)
                    { this.tmrChangeEditSearch.Stop(); }

                    OnChangeEdit();
                }
                else
                {
                    //_strSearchString = "";
                    //_strSearchString = c1Transaction.Editor.Text;
                    ogloGridListControl.AdvanceSearch(volatileSearchString);
                }
            }
        }

        private void c1Transaction_KeyUpEdit(object sender, KeyEditEventArgs e)
        {
            //#region "Numeric Validation"
            //if (c1Transaction.ColSel == COL_CHARGES || c1Transaction.ColSel == COL_UNIT || c1Transaction.ColSel == COL_TOTAL || c1Transaction.ColSel == COL_ALLOWED)
            //{
            //    decimal _result = Convert.ToDecimal(c1Transaction.GetData(c1Transaction.RowSel, c1Transaction.ColSel));
            //    //MessageBox.Show(e.KeyCode.ToString());
            //    if (e.KeyCode == Keys.OemMinus || e.KeyCode == Keys.Subtract)
            //    {
            //        e.Handled = false;
            //    }

            //}
            //#endregion
        }

        private void c1Transaction_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {

            }
        }

        private void c1Transaction_KeyUp(object sender, KeyEventArgs e)
        {
            //     int _id = 0;
            string _code = "";
            string _description = "";
            bool _isdeleted = true;

            TrnCtrlColValChangeEventArg e2 = new TrnCtrlColValChangeEventArg();
            RowColEventArgs e1 = null;

            try
            {
                //if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
                //{
                //    e.SuppressKeyPress = true;
                //    if (pnlInternalControl.Visible)
                //    {
                //        if (ogloGridListControl != null)
                //        {
                //            ogloGridListControl.Focus();
                //        }
                //    }
                //}

                //if (e.KeyValue == Convert.ToChar(Keys.Tab))
                //{
                //    if (ogloGridListControl != null)
                //    {
                //        ogloGridListControl.GetCurrentSelectedItem();

                //        //ogloGridListControl_ItemSelected(null, null);
                //    }
                //}

                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    #region "Enter / Tab Key"

                    if (pnlInternalControl.Visible)
                    {
                        if (ogloGridListControl != null)
                        {
                            bool _IsItemSelected = ogloGridListControl.GetCurrentSelectedItem();
                            if (_IsItemSelected)
                            {

                                //If Item is Selected Move to nextcell
                                //MoveNext();

                                //********* Code Commented shifted to ogloGridListControl_ItemSelected
                            }
                        }
                    }


                    else
                    {
                        //For Columns withour Internal List directly move next
                        if (CurrentColumn == COL_CHARGES || CurrentColumn == COL_UNIT || CurrentColumn == COL_TOTAL || CurrentColumn == COL_ALLOWED)
                        {
                            if (((c1Transaction.RowSel >= 0) && (c1Transaction.RowSel < c1Transaction.Rows.Count)) && ((c1Transaction.ColSel >= 0) && (c1Transaction.ColSel < c1Transaction.Cols.Count)))
                            {
                                if (Convert.ToDecimal(c1Transaction.GetData(c1Transaction.RowSel, c1Transaction.ColSel)) < 0)
                                {
                                    c1Transaction.SetData(c1Transaction.RowSel, c1Transaction.ColSel, 0);
                                    //MoveNext();
                                }
                                else
                                {
                                    MoveNext();
                                }
                            }
                        }
                        else if (CurrentColumn == COL_INSURANCENAME)
                        {
                            //*************************************************************************************************************
                            //Code added on 20081105 By - Sagar Ghodke
                            //Code added to implement Sorting Logic
                            if (c1Transaction != null && c1Transaction.Rows.Count > 1)
                            {
                                bool ok = c1Transaction.Cols.Count > COL_ISLABCPT;
                                if (ok)
                                {
                                    if (c1Transaction.Cols[COL_ISLABCPT].Visible == true)
                                    {
                                        MoveNext();
                                    }
                                    else
                                    {
                                        ok = false;
                                    }
                                }
                                if (!ok)
                                {
                                    //if (c1Transaction.Rows.Count <= 6)
                                    //if (c1Transaction.Rows.Count <= 30)
                                    if ((c1Transaction.Rows.Count <= _NoOfServiceLines) && (c1Transaction.Cols.Count > COL_INSURANCENAME) && (c1Transaction.Cols.Count > COL_INSURANCEID) && (c1Transaction.Cols.Count > COL_INSSELF_PAYMODE))
                                    {
                                        SortControl();
                                        AddTransactionLine();
                                        string _fillInsuranceName = "";
                                        Int64 _fillInsuranceID = 0;
                                        Int32 _fillInsSelfMode = 0;
                                        _fillInsuranceName = Convert.ToString(c1Transaction.GetData(c1Transaction.Rows.Count - 2, COL_INSURANCENAME));
                                        _fillInsuranceID = Convert.ToInt64(c1Transaction.GetData(c1Transaction.Rows.Count - 2, COL_INSURANCEID));
                                        _fillInsSelfMode = Convert.ToInt32(c1Transaction.GetData(c1Transaction.Rows.Count - 2, COL_INSSELF_PAYMODE));

                                        AddInsurance(this.CurrentTransactionLine, _fillInsuranceID, _fillInsuranceName, _fillInsSelfMode);
                                        SelectTransactionLine(this.CurrentTransactionLine);
                                        //c1Transaction_SelChange(null, null);
                                    }
                                }
                            }
                            //End Code add - 20081105
                            //*************************************************************************************************************
                        }
                        else if (CurrentColumn == COL_AUTHORIZATIONNO && _AllowChargeModification == true)
                        {
                            //if (c1Transaction.Rows.Count <= 6)
                            //if (c1Transaction.Rows.Count <= 30)
                            if ((c1Transaction.Rows.Count > 1) && (c1Transaction.Rows.Count <= _NoOfServiceLines) && (c1Transaction.Cols.Count > COL_INSURANCENAME) && (c1Transaction.Cols.Count > COL_INSURANCEID) && (c1Transaction.Cols.Count > COL_INSSELF_PAYMODE))
                            {
                                SortControl();
                                AddTransactionLine();
                                string _fillInsuranceName = "";
                                Int64 _fillInsuranceID = 0;
                                Int32 _fillInsSelfMode = 0;
                                _fillInsuranceName = Convert.ToString(c1Transaction.GetData(c1Transaction.Rows.Count - 2, COL_INSURANCENAME));
                                _fillInsuranceID = Convert.ToInt64(c1Transaction.GetData(c1Transaction.Rows.Count - 2, COL_INSURANCEID));
                                _fillInsSelfMode = Convert.ToInt32(c1Transaction.GetData(c1Transaction.Rows.Count - 2, COL_INSSELF_PAYMODE));

                                AddInsurance(this.CurrentTransactionLine, _fillInsuranceID, _fillInsuranceName, _fillInsSelfMode);
                                SelectTransactionLine(this.CurrentTransactionLine);
                                //c1Transaction_SelChange(null, null);
                            }
                        }

                    }
                    #endregion

                    //if (c1Transaction.RowSel > 0)
                    //{
                    //    if (c1Transaction.GetData(c1Transaction.RowSel, COL_CPT_CODE) == null || Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_CPT_CODE)).Trim() == "")
                    //    {
                    //        _bISCptBlank = true;
                    //        c1Transaction.SetData(c1Transaction.RowSel, COL_NDCCODE, "");
                    //        c1Transaction.SetData(c1Transaction.RowSel, COL_NDCUNIT, null);
                    //        c1Transaction.SetData(c1Transaction.RowSel, COL_NDCUNITCODE, null);
                    //        c1Transaction.SetData(c1Transaction.RowSel, COL_NDCUNITDESCRITION, null);
                    //        SetNDCCodeFlag(c1Transaction.RowSel, false);
                    //        onGrid_CellChanged(null, null);
                    //    }
                    //}
                }
                else if (e.KeyCode == Keys.Down)
                {
                    e.SuppressKeyPress = true;
                    #region "Down Key"
                    if (pnlInternalControl.Visible)
                    {
                        if (ogloGridListControl != null)
                        {
                            ogloGridListControl.Focus();
                        }
                    }
                    #endregion

                    if (!pnlInternalControl.Visible) { adjustDxMod(); e.SuppressKeyPress = false; }
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;

                    #region "Escape Key"
                    if (pnlInternalControl.Visible)
                    {
                        if (ogloGridListControl != null)
                        {
                            IsInternalControlActive = false;
                            //CloseInternalControl();

                            //if (c1Transaction.RowSel > 0)
                            //{
                            //    if (c1Transaction.ColSel >= COL_CPT_CODE && c1Transaction.ColSel <= COL_DX8_CODE
                            //        || c1Transaction.ColSel >= COL_MOD1_CODE && c1Transaction.ColSel <= COL_MOD4_CODE)
                            //    {
                            //        if (c1Transaction.GetData(c1Transaction.RowSel, c1Transaction.ColSel) != null &&
                            //            Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, c1Transaction.ColSel)).Trim() != "")
                            //        {
                            //            CellNote oNote = new CellNote(c1Transaction[c1Transaction.RowSel, (c1Transaction.ColSel + 1)].ToString());
                            //            CellRange rg = c1Transaction.GetCellRange(c1Transaction.RowSel, c1Transaction.ColSel);
                            //            rg.UserData = oNote;
                            //        }
                            //    }
                            //}
                        }
                    }

                    if ((c1Transaction.RowSel > 0) && (c1Transaction.Cols.Count > COL_CPT_CODE) && (c1Transaction.Cols.Count > COL_NDCCODE) && (c1Transaction.Cols.Count > COL_NDCUNIT) && (c1Transaction.Cols.Count > COL_NDCUNITCODE) && (c1Transaction.Cols.Count > COL_NDCUNITDESCRITION) && (c1Transaction.Cols.Count > COL_PRESCRIPTION) && (c1Transaction.Cols.Count > COL_PRESCRIPTIONDESC))
                    {
                        if (c1Transaction.GetData(c1Transaction.RowSel, COL_CPT_CODE) == null || Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_CPT_CODE)).Trim() == "")
                        {
                            _bISCptBlank = true;
                            c1Transaction.SetData(c1Transaction.RowSel, COL_NDCCODE, "");
                            c1Transaction.SetData(c1Transaction.RowSel, COL_NDCUNIT, null);
                            c1Transaction.SetData(c1Transaction.RowSel, COL_NDCUNITCODE, null);
                            c1Transaction.SetData(c1Transaction.RowSel, COL_NDCUNITDESCRITION, null);
                            c1Transaction.SetData(c1Transaction.RowSel, COL_PRESCRIPTION, null);
                            c1Transaction.SetData(c1Transaction.RowSel, COL_PRESCRIPTIONDESC, null);
                            SetEPSDTNotesNDCCodeFlag(c1Transaction.RowSel);
                            onGrid_CellChanged(null, null);

                        }
                    }


                    #endregion
                }
                else if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Up)
                {

                    pnlInternalControl.Visible = false;
                    pnlInternalControl.SendToBack();

                    IsInternalControlActive = false;
                    if ((c1Transaction.Rows.Count > iPreRow) && (c1Transaction.Cols.Count > (iPreCol + 1)))
                    {
                        if (Convert.ToString(c1Transaction.GetData(iPreRow, iPreCol)) == "")
                        {
                            if (c1Transaction.GetData(iPreRow, iPreCol) != null)
                            {
                                _code = c1Transaction.GetData(iPreRow, iPreCol).ToString();
                            }
                            if (c1Transaction.GetData(iPreRow, iPreCol + 1) != null)
                            {
                                _description = c1Transaction.GetData(iPreRow, iPreCol + 1).ToString();
                            }

                            e2.oType = TransactionLineColumnType.None;

                            e.SuppressKeyPress = true;

                            //CellNote oCellNotes = null;

                            switch (iPreCol)
                            {
                                case COL_DX1_CODE:
                                    {
                                        if (c1Transaction.Cols[COL_DX1_CODE].AllowEditing)
                                        {
                                            c1Transaction.SetData(iPreRow, iPreCol, "");
                                            c1Transaction.SetData(iPreRow, iPreCol + 1, "");

                                            //CellRange rg = c1Transaction.GetCellRange(iPreRow, iPreCol);
                                            //rg.UserData = oCellNotes;

                                            c1Transaction.SetCellCheck(iPreRow, COL_DX1_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                                            ShiftRecords(iPreRow, COL_DX1_CODE);
                                            e2.oType = TransactionLineColumnType.Diagnosis;

                                            c1Transaction.SetData(iPreRow, COL_LINEPRIMARY_DXCODE, c1Transaction.GetData(iPreRow, COL_DX1_CODE));
                                            c1Transaction.SetData(iPreRow, COL_LINEPRIMARY_DXDESC, c1Transaction.GetData(iPreRow, COL_DX1_DESC));
                                        }
                                    }
                                    break;
                                case COL_DX2_CODE:
                                    {
                                        if (c1Transaction.Cols[COL_DX2_CODE].AllowEditing)
                                        {
                                            c1Transaction.SetData(iPreRow, iPreCol, "");
                                            c1Transaction.SetData(iPreRow, iPreCol + 1, "");

                                            //CellRange rg = c1Transaction.GetCellRange(iPreRow, iPreCol);
                                            //rg.UserData = oCellNotes;

                                            c1Transaction.SetCellCheck(iPreRow, COL_DX2_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                                            ShiftRecords(iPreRow, COL_DX2_CODE);

                                            e2.oType = TransactionLineColumnType.Diagnosis;
                                        }
                                    }
                                    break;
                                case COL_DX3_CODE:
                                    {
                                        if (c1Transaction.Cols[COL_DX3_CODE].AllowEditing)
                                        {
                                            c1Transaction.SetData(iPreRow, iPreCol, "");
                                            c1Transaction.SetData(iPreRow, iPreCol + 1, "");

                                            //CellRange rg = c1Transaction.GetCellRange(iPreRow, iPreCol);
                                            //rg.UserData = oCellNotes;

                                            c1Transaction.SetCellCheck(iPreRow, COL_DX3_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                                            ShiftRecords(iPreRow, COL_DX3_CODE);
                                            e2.oType = TransactionLineColumnType.Diagnosis;
                                        }
                                    }
                                    break;
                                case COL_DX4_CODE:
                                    {
                                        if (c1Transaction.Cols[COL_DX4_CODE].AllowEditing)
                                        {
                                            c1Transaction.SetData(iPreRow, iPreCol, "");
                                            c1Transaction.SetData(iPreRow, iPreCol + 1, "");

                                            //CellRange rg = c1Transaction.GetCellRange(iPreRow, iPreCol);
                                            //rg.UserData = oCellNotes;

                                            c1Transaction.SetCellCheck(iPreRow, COL_DX4_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                                            ShiftRecords(iPreRow, COL_DX4_CODE);
                                            e2.oType = TransactionLineColumnType.Diagnosis;
                                        }
                                    }
                                    break;
                                case COL_DX5_CODE:
                                    {
                                        c1Transaction.SetData(iPreRow, iPreCol, "");
                                        c1Transaction.SetData(iPreRow, iPreCol + 1, "");

                                        //CellRange rg = c1Transaction.GetCellRange(iPreRow, iPreCol);
                                        //rg.UserData = oCellNotes;

                                        c1Transaction.SetCellCheck(iPreRow, COL_DX5_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                                        ShiftRecords(iPreRow, COL_DX5_CODE);
                                        e2.oType = TransactionLineColumnType.Diagnosis;
                                    }
                                    break;
                                case COL_DX6_CODE:
                                    {
                                        c1Transaction.SetData(iPreRow, iPreCol, "");
                                        c1Transaction.SetData(iPreRow, iPreCol + 1, "");

                                        //CellRange rg = c1Transaction.GetCellRange(iPreRow, iPreCol);
                                        //rg.UserData = oCellNotes;

                                        c1Transaction.SetCellCheck(iPreRow, COL_DX6_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                                        ShiftRecords(iPreRow, COL_DX6_CODE);
                                        e2.oType = TransactionLineColumnType.Diagnosis;
                                    }
                                    break;
                                case COL_DX7_CODE:
                                    {
                                        c1Transaction.SetData(iPreRow, iPreCol, "");
                                        c1Transaction.SetData(iPreRow, iPreCol + 1, "");

                                        //CellRange rg = c1Transaction.GetCellRange(iPreRow, iPreCol);
                                        //rg.UserData = oCellNotes;

                                        c1Transaction.SetCellCheck(iPreRow, COL_DX7_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                                        ShiftRecords(iPreRow, COL_DX7_CODE);
                                        e2.oType = TransactionLineColumnType.Diagnosis;
                                    }
                                    break;
                                case COL_DX8_CODE:
                                    {
                                        c1Transaction.SetData(iPreRow, iPreCol, "");
                                        c1Transaction.SetData(iPreRow, iPreCol + 1, "");

                                        //CellRange rg = c1Transaction.GetCellRange(iPreRow, iPreCol);
                                        //rg.UserData = oCellNotes;

                                        c1Transaction.SetCellCheck(iPreRow, COL_DX8_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                                        ShiftRecords(iPreRow, COL_DX8_CODE);
                                        e2.oType = TransactionLineColumnType.Diagnosis;
                                    }
                                    break;
                                case COL_MOD1_CODE:
                                case COL_MOD2_CODE:
                                case COL_MOD3_CODE:
                                case COL_MOD4_CODE:
                                    {
                                        c1Transaction.SetData(iPreRow, iPreCol, "");
                                        c1Transaction.SetData(iPreRow, iPreCol + 1, "");

                                        //CellRange rg = c1Transaction.GetCellRange(iPreRow, iPreCol);
                                        //rg.UserData = oCellNotes;

                                        ShiftRecords(iPreRow, iPreCol);
                                        e2.oType = TransactionLineColumnType.Diagnosis;
                                    }
                                    break;
                                default:
                                    break;

                            }

                            e1 = new RowColEventArgs(iPreRow, iPreCol);
                            e2.code = _code;
                            e2.description = _description;
                            e2.isdeleted = true;


                            //**Check if the primary diagnosis set to the line was deleted 
                            //**from the line , if yes also remove the primary diagnosis
                            if (e2.isdeleted == true)
                            {
                                if (e2.oType == TransactionLineColumnType.Diagnosis)
                                {
                                    if (c1Transaction.GetData(iPreRow, COL_LINEPRIMARY_DXCODE) != null && Convert.ToString(c1Transaction.GetData(iPreRow, COL_LINEPRIMARY_DXCODE)).Trim() != "")
                                    {
                                        if (Convert.ToString(c1Transaction.GetData(iPreRow, COL_LINEPRIMARY_DXCODE)).Trim().ToUpper() == e2.code.Trim().ToUpper())
                                        {
                                            //c1Transaction.SetData(iPreRow, COL_LINEPRIMARY_DXCODE, "");
                                            //c1Transaction.SetData(iPreRow, COL_LINEPRIMARY_DXDESC, "");
                                            c1Transaction.SetData(iPreRow, COL_LINEPRIMARY_DXCODE, Convert.ToString(c1Transaction.GetData(iPreRow, COL_DX1_CODE)).Trim());
                                            c1Transaction.SetData(iPreRow, COL_LINEPRIMARY_DXDESC, Convert.ToString(c1Transaction.GetData(iPreRow, COL_DX1_DESC)).Trim());
                                        }
                                    }
                                }
                            }
                            //**

                            int _dxcntr = 0;

                            if (e2.oType == TransactionLineColumnType.Diagnosis)
                            {
                                for (int i = 1; i <= c1Transaction.Rows.Count - 1; i++)
                                {
                                    for (int j = COL_DX1_CODE; j <= COL_DX8_CODE; j += 3)
                                    {
                                        if (c1Transaction.GetData(i, j) != null && c1Transaction.GetData(i, j).ToString().Trim() != "")
                                        {
                                            if (c1Transaction.GetData(i, j).ToString().Trim() == _code.Trim())
                                            {
                                                _dxcntr = _dxcntr + 1;
                                                if (_dxcntr >= 1)
                                                {
                                                    _code = "";
                                                    _description = "";
                                                    _isdeleted = false;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }


                            e2.code = _code;
                            e2.description = _description;
                            e2.isdeleted = _isdeleted;
                            onInsCPTDxMod_Changed(null, e1, e2);

                            bool _IsLineDxPresent = false;
                            for (int i = COL_DX1_PTR; i <= COL_DX8_PTR; i++)
                            {
                                if (c1Transaction.GetCellCheck(iPreRow, i) == CheckEnum.Checked)
                                { _IsLineDxPresent = true; break; }
                            }
                            if (_IsLineDxPresent == false)
                            {
                                c1Transaction.SetData(iPreRow, COL_LINEPRIMARY_DXCODE, "");
                                c1Transaction.SetData(iPreRow, COL_LINEPRIMARY_DXDESC, "");
                            }

                            //**



                            //if ((e.KeyCode == Keys.Tab || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down) && _AllowChargeModification == true)
                            if (e.KeyCode == Keys.Tab)
                            {
                                if (CurrentColumn >= COL_MOD1_CODE && CurrentColumn <= COL_MOD4_CODE)
                                {
                                    if (c1Transaction.GetData(iPreRow, iPreCol) != null)
                                    {
                                        int iCurrentRow = this.CurrentTransactionLine;
                                        //this.SelectTransactionLine(iPreRow);
                                        c1Transaction.RowSel = iPreRow;
                                        SetFNFCharges();
                                        //this.SelectTransactionLine(iCurrentRow);
                                        c1Transaction.RowSel = iCurrentRow;
                                    }
                                }
                            }
                        }
                    }
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    //CellNote oCellNotes = null;
                    if (((c1Transaction.RowSel >= 0) && (c1Transaction.RowSel < c1Transaction.Rows.Count)) && ((c1Transaction.ColSel >= 0) && (c1Transaction.ColSel < c1Transaction.Cols.Count)))
                    {

                        if (c1Transaction.GetData(c1Transaction.RowSel, c1Transaction.ColSel) != null)
                        {
                            _code = c1Transaction.GetData(c1Transaction.RowSel, c1Transaction.ColSel).ToString();
                        }
                        if (c1Transaction.GetData(c1Transaction.RowSel, c1Transaction.ColSel + 1) != null)
                        {
                            _description = c1Transaction.GetData(c1Transaction.RowSel, c1Transaction.ColSel + 1).ToString();
                        }

                        e2.oType = TransactionLineColumnType.None;

                        e.SuppressKeyPress = true;

                        #region "Delete Key"
                        switch (c1Transaction.ColSel)
                        {
                            case COL_POSCODE:
                                {
                                    if (_AllowChargeModification == true)
                                    {
                                        c1Transaction.SetData(c1Transaction.RowSel, c1Transaction.ColSel, "");
                                        c1Transaction.SetData(c1Transaction.RowSel, c1Transaction.ColSel + 1, "");

                                        //CellRange rg = c1Transaction.GetCellRange(c1Transaction.RowSel, c1Transaction.ColSel);
                                        //rg.UserData = oCellNotes;
                                    }
                                }
                                break;
                            case COL_TOSCODE:
                                {
                                    c1Transaction.SetData(c1Transaction.RowSel, c1Transaction.ColSel, "");
                                    c1Transaction.SetData(c1Transaction.RowSel, c1Transaction.ColSel + 1, "");

                                    //CellRange rg = c1Transaction.GetCellRange(c1Transaction.RowSel, c1Transaction.ColSel);
                                    //rg.UserData = oCellNotes;
                                }
                                break;
                            case COL_CPT_CODE:
                                {
                                    if (_AllowChargeModification == true)
                                    {
                                        c1Transaction.SetData(c1Transaction.RowSel, c1Transaction.ColSel, "");
                                        c1Transaction.SetData(c1Transaction.RowSel, c1Transaction.ColSel + 1, "");

                                        //CellRange rg = c1Transaction.GetCellRange(c1Transaction.RowSel, c1Transaction.ColSel);
                                        //rg.UserData = oCellNotes;
                                        e2.oType = TransactionLineColumnType.CPT;
                                    }
                                }
                                break;
                            case COL_DX1_CODE:
                                {
                                    if (c1Transaction.Cols[COL_DX1_CODE].AllowEditing)
                                    {
                                        c1Transaction.SetData(c1Transaction.RowSel, c1Transaction.ColSel, "");
                                        c1Transaction.SetData(c1Transaction.RowSel, c1Transaction.ColSel + 1, "");

                                        //CellRange rg = c1Transaction.GetCellRange(c1Transaction.RowSel, c1Transaction.ColSel);
                                        //rg.UserData = oCellNotes;

                                        c1Transaction.SetCellCheck(c1Transaction.RowSel, COL_DX1_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                                        ShiftRecords(c1Transaction.RowSel, COL_DX1_CODE);
                                        e2.oType = TransactionLineColumnType.Diagnosis;

                                        c1Transaction.SetData(c1Transaction.RowSel, COL_LINEPRIMARY_DXCODE, c1Transaction.GetData(c1Transaction.RowSel, COL_DX1_CODE));
                                        c1Transaction.SetData(c1Transaction.RowSel, COL_LINEPRIMARY_DXDESC, c1Transaction.GetData(c1Transaction.RowSel, COL_DX1_DESC));
                                    }
                                }
                                break;
                            case COL_DX2_CODE:
                                {
                                    if (c1Transaction.Cols[COL_DX2_CODE].AllowEditing)
                                    {
                                        c1Transaction.SetData(c1Transaction.RowSel, c1Transaction.ColSel, "");
                                        c1Transaction.SetData(c1Transaction.RowSel, c1Transaction.ColSel + 1, "");

                                        //CellRange rg = c1Transaction.GetCellRange(c1Transaction.RowSel, c1Transaction.ColSel);
                                        //rg.UserData = oCellNotes;

                                        c1Transaction.SetCellCheck(c1Transaction.RowSel, COL_DX2_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                                        ShiftRecords(c1Transaction.RowSel, COL_DX2_CODE);

                                        e2.oType = TransactionLineColumnType.Diagnosis;
                                    }
                                }
                                break;
                            case COL_DX3_CODE:
                                {
                                    if (c1Transaction.Cols[COL_DX3_CODE].AllowEditing)
                                    {
                                        c1Transaction.SetData(c1Transaction.RowSel, c1Transaction.ColSel, "");
                                        c1Transaction.SetData(c1Transaction.RowSel, c1Transaction.ColSel + 1, "");

                                        //CellRange rg = c1Transaction.GetCellRange(c1Transaction.RowSel, c1Transaction.ColSel);
                                        //rg.UserData = oCellNotes;

                                        c1Transaction.SetCellCheck(c1Transaction.RowSel, COL_DX3_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                                        ShiftRecords(c1Transaction.RowSel, COL_DX3_CODE);
                                        e2.oType = TransactionLineColumnType.Diagnosis;
                                    }
                                }
                                break;
                            case COL_DX4_CODE:
                                {
                                    if (c1Transaction.Cols[COL_DX4_CODE].AllowEditing)
                                    {
                                        c1Transaction.SetData(c1Transaction.RowSel, c1Transaction.ColSel, "");
                                        c1Transaction.SetData(c1Transaction.RowSel, c1Transaction.ColSel + 1, "");

                                        //CellRange rg = c1Transaction.GetCellRange(c1Transaction.RowSel, c1Transaction.ColSel);
                                        //rg.UserData = oCellNotes;

                                        c1Transaction.SetCellCheck(c1Transaction.RowSel, COL_DX4_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                                        ShiftRecords(c1Transaction.RowSel, COL_DX4_CODE);
                                        e2.oType = TransactionLineColumnType.Diagnosis;
                                    }
                                }
                                break;
                            case COL_DX5_CODE:
                                {
                                    c1Transaction.SetData(c1Transaction.RowSel, c1Transaction.ColSel, "");
                                    c1Transaction.SetData(c1Transaction.RowSel, c1Transaction.ColSel + 1, "");

                                    //CellRange rg = c1Transaction.GetCellRange(c1Transaction.RowSel, c1Transaction.ColSel);
                                    //rg.UserData = oCellNotes;

                                    c1Transaction.SetCellCheck(c1Transaction.RowSel, COL_DX5_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                                    ShiftRecords(c1Transaction.RowSel, COL_DX5_CODE);
                                    e2.oType = TransactionLineColumnType.Diagnosis;
                                }
                                break;
                            case COL_DX6_CODE:
                                {
                                    c1Transaction.SetData(c1Transaction.RowSel, c1Transaction.ColSel, "");
                                    c1Transaction.SetData(c1Transaction.RowSel, c1Transaction.ColSel + 1, "");

                                    //CellRange rg = c1Transaction.GetCellRange(c1Transaction.RowSel, c1Transaction.ColSel);
                                    //rg.UserData = oCellNotes;

                                    c1Transaction.SetCellCheck(c1Transaction.RowSel, COL_DX6_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                                    ShiftRecords(c1Transaction.RowSel, COL_DX6_CODE);
                                    e2.oType = TransactionLineColumnType.Diagnosis;
                                }
                                break;
                            case COL_DX7_CODE:
                                {
                                    c1Transaction.SetData(c1Transaction.RowSel, c1Transaction.ColSel, "");
                                    c1Transaction.SetData(c1Transaction.RowSel, c1Transaction.ColSel + 1, "");

                                    //CellRange rg = c1Transaction.GetCellRange(c1Transaction.RowSel, c1Transaction.ColSel);
                                    //rg.UserData = oCellNotes;

                                    c1Transaction.SetCellCheck(c1Transaction.RowSel, COL_DX7_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                                    ShiftRecords(c1Transaction.RowSel, COL_DX7_CODE);
                                    e2.oType = TransactionLineColumnType.Diagnosis;
                                }
                                break;
                            case COL_DX8_CODE:
                                {
                                    c1Transaction.SetData(c1Transaction.RowSel, c1Transaction.ColSel, "");
                                    c1Transaction.SetData(c1Transaction.RowSel, c1Transaction.ColSel + 1, "");

                                    //CellRange rg = c1Transaction.GetCellRange(c1Transaction.RowSel, c1Transaction.ColSel);
                                    //rg.UserData = oCellNotes;

                                    c1Transaction.SetCellCheck(c1Transaction.RowSel, COL_DX8_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                                    ShiftRecords(c1Transaction.RowSel, COL_DX8_CODE);
                                    e2.oType = TransactionLineColumnType.Diagnosis;
                                }
                                break;
                            case COL_MOD1_CODE:
                            case COL_MOD2_CODE:
                            case COL_MOD3_CODE:
                            case COL_MOD4_CODE:
                                {
                                    c1Transaction.SetData(c1Transaction.RowSel, c1Transaction.ColSel, "");
                                    c1Transaction.SetData(c1Transaction.RowSel, c1Transaction.ColSel + 1, "");

                                    //CellRange rg = c1Transaction.GetCellRange(c1Transaction.RowSel, c1Transaction.ColSel);
                                    //rg.UserData = oCellNotes;

                                    ShiftRecords(c1Transaction.RowSel, c1Transaction.ColSel);
                                    e2.oType = TransactionLineColumnType.Diagnosis;
                                }
                                break;
                            case COL_PROVIDER:
                                {
                                    if (_AllowChargeModification == true)
                                    {
                                        c1Transaction.SetData(c1Transaction.RowSel, c1Transaction.ColSel, "");
                                    }
                                }
                                break;
                            case COL_CHARGES:
                                {
                                    if (_AllowChargeModification == true)
                                    {
                                        c1Transaction.SetData(c1Transaction.RowSel, c1Transaction.ColSel, 0);
                                    }
                                }
                                break;
                            //case COL_ALLOWED:
                            //    {
                            //        if (_AllowChargeModification == true)
                            //        {
                            //            c1Transaction.SetData(c1Transaction.RowSel, c1Transaction.ColSel, 0);
                            //        }
                            //    }
                            //    break;
                            case COL_UNIT:
                                {
                                    if (_AllowChargeModification == true)
                                    {
                                        c1Transaction.SetData(c1Transaction.RowSel, c1Transaction.ColSel, 1);
                                    }
                                }
                                break;
                            case COL_AUTHORIZATIONNO:
                                {
                                    c1Transaction.SetCellCheck(c1Transaction.RowSel, COL_ISLABCPT, CheckEnum.Unchecked);
                                    c1Transaction.SetData(c1Transaction.RowSel, c1Transaction.ColSel, "");
                                }
                                break;
                            case COL_DATETO:
                                {
                                    c1Transaction.SetData(c1Transaction.RowSel, c1Transaction.ColSel, null);
                                }
                                break;
                        }

                        e1 = new RowColEventArgs(c1Transaction.RowSel, c1Transaction.ColSel);
                        e2.code = _code;
                        e2.description = _description;
                        e2.isdeleted = true;


                        //**Check if the primary diagnosis set to the line was deleted 
                        //**from the line , if yes also remove the primary diagnosis
                        if (e2.isdeleted == true)
                        {
                            if (e2.oType == TransactionLineColumnType.Diagnosis)
                            {
                                if (c1Transaction.GetData(c1Transaction.RowSel, COL_LINEPRIMARY_DXCODE) != null && Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_LINEPRIMARY_DXCODE)).Trim() != "")
                                {
                                    if (Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_LINEPRIMARY_DXCODE)).Trim().ToUpper() == e2.code.Trim().ToUpper())
                                    {
                                        //c1Transaction.SetData(c1Transaction.RowSel, COL_LINEPRIMARY_DXCODE, "");
                                        //c1Transaction.SetData(c1Transaction.RowSel, COL_LINEPRIMARY_DXDESC, "");
                                        c1Transaction.SetData(c1Transaction.RowSel, COL_LINEPRIMARY_DXCODE, Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_DX1_CODE)).Trim());
                                        c1Transaction.SetData(c1Transaction.RowSel, COL_LINEPRIMARY_DXDESC, Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_DX1_DESC)).Trim());
                                    }
                                }
                            }
                        }
                    }
                    //**

                    int _dxcntr = 0;

                    if (e2.oType == TransactionLineColumnType.Diagnosis)
                    {
                        for (int i = 1; i <= c1Transaction.Rows.Count - 1; i++)
                        {
                            for (int j = COL_DX1_CODE; j <= COL_DX8_CODE; j += 3)
                            {
                                if (c1Transaction.GetData(i, j) != null && c1Transaction.GetData(i, j).ToString().Trim() != "")
                                {
                                    if (c1Transaction.GetData(i, j).ToString().Trim() == _code.Trim())
                                    {
                                        _dxcntr = _dxcntr + 1;
                                        if (_dxcntr >= 1)
                                        {
                                            _code = "";
                                            _description = "";
                                            _isdeleted = false;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }


                    e2.code = _code;
                    e2.description = _description;
                    e2.isdeleted = _isdeleted;
                    onInsCPTDxMod_Changed(null, e1, e2);

                    ////**Check if the primary diagnosis set to the line was deleted 
                    ////**from the line , if yes also remove the primary diagnosis
                    //if (e2.isdeleted == true)
                    //{
                    //    if (e2.oType == TransactionLineColumnType.Diagnosis)
                    //    {
                    //        if (c1Transaction.GetData(c1Transaction.RowSel, COL_LINEPRIMARY_DXCODE) != null && Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_LINEPRIMARY_DXCODE)).Trim() != "")
                    //        {
                    //            if (Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_LINEPRIMARY_DXCODE)).Trim().ToUpper() == e2.code.Trim().ToUpper())
                    //            {
                    //                c1Transaction.SetData(c1Transaction.RowSel, COL_LINEPRIMARY_DXCODE, "");
                    //                c1Transaction.SetData(c1Transaction.RowSel, COL_LINEPRIMARY_DXDESC, "");
                    //            }
                    //        }
                    //    }
                    //}
                    ////**

                    //**Check if all the diagnosis from the line are deleted 
                    //if yes reset the primary diagnosis for the line /
                    //we are checking this by looking at the diagnosis pointers
                    //if all the active diagnosis pointers are unchecked that means there is no 
                    //diagnosis allocated to the line in this case reset the primary diagnosis of the line if any setup
                    bool _IsLineDxPresent = false;
                    for (int i = COL_DX1_PTR; i <= COL_DX8_PTR; i++)
                    {
                        if (c1Transaction.GetCellCheck(c1Transaction.RowSel, i) == CheckEnum.Checked)
                        { _IsLineDxPresent = true; break; }
                    }
                    if (_IsLineDxPresent == false)
                    {
                        c1Transaction.SetData(c1Transaction.RowSel, COL_LINEPRIMARY_DXCODE, "");
                        c1Transaction.SetData(c1Transaction.RowSel, COL_LINEPRIMARY_DXDESC, "");
                    }

                    //**

                        #endregion
                }
                if (e.KeyCode == Keys.Delete && _AllowChargeModification == true)
                {
                    if (CurrentColumn == COL_MOD1_CODE || CurrentColumn == COL_MOD2_CODE || CurrentColumn == COL_MOD3_CODE || CurrentColumn == COL_MOD4_CODE)
                    {
                        if (((c1Transaction.RowSel >= 0) && (c1Transaction.RowSel < c1Transaction.Rows.Count)) && ((c1Transaction.ColSel >= 0) && (c1Transaction.ColSel < c1Transaction.Cols.Count)))
                        {
                            if (c1Transaction.GetData(c1Transaction.RowSel, c1Transaction.ColSel) != null)
                            {
                                SetFNFCharges();
                            }
                        }
                    }
                }
                //e.SuppressKeyPress = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {

            }
        }

        private void adjustDxMod()
        {
            //   int _id = 0;
            string _code = "";
            string _description = "";
            bool _isdeleted = true;

            TrnCtrlColValChangeEventArg e2 = new TrnCtrlColValChangeEventArg();
            RowColEventArgs e1 = null;

            try
            {
                //pnlInternalControl.Visible = false;
                //pnlInternalControl.SendToBack();
                if (iPreRow >= 0)
                {
                    if (c1Transaction.GetData(iPreRow, iPreCol) != null)
                    {

                        if (c1Transaction.GetData(iPreRow, iPreCol).ToString() == "")
                        {
                            if (c1Transaction.GetData(iPreRow, iPreCol) != null)
                            {
                                _code = c1Transaction.GetData(iPreRow, iPreCol).ToString();
                            }
                            if (c1Transaction.GetData(iPreRow, iPreCol + 1) != null)
                            {
                                _description = c1Transaction.GetData(iPreRow, iPreCol + 1).ToString();
                            }

                            e2.oType = TransactionLineColumnType.None;

                            //CellNote oCellNotes = null;

                            switch (iPreCol)
                            {
                                case COL_DX1_CODE:
                                    {
                                        if (c1Transaction.Cols[COL_DX1_CODE].AllowEditing)
                                        {
                                            c1Transaction.SetData(iPreRow, iPreCol, "");
                                            c1Transaction.SetData(iPreRow, iPreCol + 1, "");

                                            //CellRange rg = c1Transaction.GetCellRange(iPreRow, iPreCol);
                                            //rg.UserData = oCellNotes;

                                            c1Transaction.SetCellCheck(iPreRow, COL_DX1_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                                            ShiftRecords(iPreRow, COL_DX1_CODE);
                                            e2.oType = TransactionLineColumnType.Diagnosis;

                                            c1Transaction.SetData(iPreRow, COL_LINEPRIMARY_DXCODE, c1Transaction.GetData(iPreRow, COL_DX1_CODE));
                                            c1Transaction.SetData(iPreRow, COL_LINEPRIMARY_DXDESC, c1Transaction.GetData(iPreRow, COL_DX1_DESC));
                                        }
                                    }
                                    break;
                                case COL_DX2_CODE:
                                    {
                                        if (c1Transaction.Cols[COL_DX2_CODE].AllowEditing)
                                        {
                                            c1Transaction.SetData(iPreRow, iPreCol, "");
                                            c1Transaction.SetData(iPreRow, iPreCol + 1, "");

                                            //CellRange rg = c1Transaction.GetCellRange(iPreRow, iPreCol);
                                            //rg.UserData = oCellNotes;

                                            c1Transaction.SetCellCheck(iPreRow, COL_DX2_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                                            ShiftRecords(iPreRow, COL_DX2_CODE);

                                            e2.oType = TransactionLineColumnType.Diagnosis;
                                        }
                                    }
                                    break;
                                case COL_DX3_CODE:
                                    {
                                        if (c1Transaction.Cols[COL_DX3_CODE].AllowEditing)
                                        {
                                            c1Transaction.SetData(iPreRow, iPreCol, "");
                                            c1Transaction.SetData(iPreRow, iPreCol + 1, "");

                                            //CellRange rg = c1Transaction.GetCellRange(iPreRow, iPreCol);
                                            //rg.UserData = oCellNotes;

                                            c1Transaction.SetCellCheck(iPreRow, COL_DX3_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                                            ShiftRecords(iPreRow, COL_DX3_CODE);
                                            e2.oType = TransactionLineColumnType.Diagnosis;
                                        }
                                    }
                                    break;
                                case COL_DX4_CODE:
                                    {
                                        if (c1Transaction.Cols[COL_DX4_CODE].AllowEditing)
                                        {
                                            c1Transaction.SetData(iPreRow, iPreCol, "");
                                            c1Transaction.SetData(iPreRow, iPreCol + 1, "");

                                            //CellRange rg = c1Transaction.GetCellRange(iPreRow, iPreCol);
                                            //rg.UserData = oCellNotes;

                                            c1Transaction.SetCellCheck(iPreRow, COL_DX4_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                                            ShiftRecords(iPreRow, COL_DX4_CODE);
                                            e2.oType = TransactionLineColumnType.Diagnosis;
                                        }
                                    }
                                    break;
                                case COL_DX5_CODE:
                                    {
                                        c1Transaction.SetData(iPreRow, iPreCol, "");
                                        c1Transaction.SetData(iPreRow, iPreCol + 1, "");

                                        //CellRange rg = c1Transaction.GetCellRange(iPreRow, iPreCol);
                                        //rg.UserData = oCellNotes;

                                        c1Transaction.SetCellCheck(iPreRow, COL_DX5_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                                        ShiftRecords(iPreRow, COL_DX5_CODE);
                                        e2.oType = TransactionLineColumnType.Diagnosis;
                                    }
                                    break;
                                case COL_DX6_CODE:
                                    {
                                        c1Transaction.SetData(iPreRow, iPreCol, "");
                                        c1Transaction.SetData(iPreRow, iPreCol + 1, "");

                                        //CellRange rg = c1Transaction.GetCellRange(iPreRow, iPreCol);
                                        //rg.UserData = oCellNotes;

                                        c1Transaction.SetCellCheck(iPreRow, COL_DX6_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                                        ShiftRecords(iPreRow, COL_DX6_CODE);
                                        e2.oType = TransactionLineColumnType.Diagnosis;
                                    }
                                    break;
                                case COL_DX7_CODE:
                                    {
                                        c1Transaction.SetData(iPreRow, iPreCol, "");
                                        c1Transaction.SetData(iPreRow, iPreCol + 1, "");

                                        //CellRange rg = c1Transaction.GetCellRange(iPreRow, iPreCol);
                                        //rg.UserData = oCellNotes;

                                        c1Transaction.SetCellCheck(iPreRow, COL_DX7_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                                        ShiftRecords(iPreRow, COL_DX7_CODE);
                                        e2.oType = TransactionLineColumnType.Diagnosis;
                                    }
                                    break;
                                case COL_DX8_CODE:
                                    {
                                        c1Transaction.SetData(iPreRow, iPreCol, "");
                                        c1Transaction.SetData(iPreRow, iPreCol + 1, "");

                                        //CellRange rg = c1Transaction.GetCellRange(iPreRow, iPreCol);
                                        //rg.UserData = oCellNotes;

                                        c1Transaction.SetCellCheck(iPreRow, COL_DX8_PTR, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                                        ShiftRecords(iPreRow, COL_DX8_CODE);
                                        e2.oType = TransactionLineColumnType.Diagnosis;
                                    }
                                    break;
                                case COL_MOD1_CODE:
                                case COL_MOD2_CODE:
                                case COL_MOD3_CODE:
                                case COL_MOD4_CODE:
                                    {
                                        c1Transaction.SetData(iPreRow, iPreCol, "");
                                        c1Transaction.SetData(iPreRow, iPreCol + 1, "");

                                        //CellRange rg = c1Transaction.GetCellRange(iPreRow, iPreCol);
                                        //rg.UserData = oCellNotes;

                                        ShiftRecords(iPreRow, iPreCol);
                                        e2.oType = TransactionLineColumnType.Diagnosis;
                                    }
                                    break;
                                default:
                                    break;

                            }

                            e1 = new RowColEventArgs(iPreRow, iPreCol);
                            e2.code = _code;
                            e2.description = _description;
                            e2.isdeleted = true;


                            //**Check if the primary diagnosis set to the line was deleted 
                            //**from the line , if yes also remove the primary diagnosis
                            if (e2.isdeleted == true)
                            {
                                if (e2.oType == TransactionLineColumnType.Diagnosis)
                                {
                                    if (c1Transaction.GetData(iPreRow, COL_LINEPRIMARY_DXCODE) != null && Convert.ToString(c1Transaction.GetData(iPreRow, COL_LINEPRIMARY_DXCODE)).Trim() != "")
                                    {
                                        if (Convert.ToString(c1Transaction.GetData(iPreRow, COL_LINEPRIMARY_DXCODE)).Trim().ToUpper() == e2.code.Trim().ToUpper())
                                        {
                                            //c1Transaction.SetData(iPreRow, COL_LINEPRIMARY_DXCODE, "");
                                            //c1Transaction.SetData(iPreRow, COL_LINEPRIMARY_DXDESC, "");
                                            c1Transaction.SetData(iPreRow, COL_LINEPRIMARY_DXCODE, Convert.ToString(c1Transaction.GetData(iPreRow, COL_DX1_CODE)).Trim());
                                            c1Transaction.SetData(iPreRow, COL_LINEPRIMARY_DXDESC, Convert.ToString(c1Transaction.GetData(iPreRow, COL_DX1_DESC)).Trim());
                                        }
                                    }
                                }
                            }
                            //**

                            int _dxcntr = 0;

                            if (e2.oType == TransactionLineColumnType.Diagnosis)
                            {
                                for (int i = 1; i <= c1Transaction.Rows.Count - 1; i++)
                                {
                                    for (int j = COL_DX1_CODE; j <= COL_DX8_CODE; j += 3)
                                    {
                                        if (c1Transaction.GetData(i, j) != null && c1Transaction.GetData(i, j).ToString().Trim() != "")
                                        {
                                            if (c1Transaction.GetData(i, j).ToString().Trim() == _code.Trim())
                                            {
                                                _dxcntr = _dxcntr + 1;
                                                if (_dxcntr >= 1)
                                                {
                                                    _code = "";
                                                    _description = "";
                                                    _isdeleted = false;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }


                            e2.code = _code;
                            e2.description = _description;
                            e2.isdeleted = _isdeleted;
                            onInsCPTDxMod_Changed(null, e1, e2);

                            bool _IsLineDxPresent = false;
                            for (int i = COL_DX1_PTR; i <= COL_DX8_PTR; i++)
                            {
                                if (c1Transaction.GetCellCheck(iPreRow, i) == CheckEnum.Checked)
                                { _IsLineDxPresent = true; break; }
                            }
                            if (_IsLineDxPresent == false)
                            {
                                c1Transaction.SetData(iPreRow, COL_LINEPRIMARY_DXCODE, "");
                                c1Transaction.SetData(iPreRow, COL_LINEPRIMARY_DXDESC, "");
                            }


                        }
                    }
                }
            }
            catch //(Exception ex)
            {

                throw;
            }

        }

        public bool IsInternalControlActive
        {
            get { return pnlInternalControl.Visible; }
            set
            {
                pnlInternalControl.Visible = value;

                if (!pnlInternalControl.Visible)
                {
                    CloseInternalControl();

                    if (c1Transaction.Rows.Count > 1)
                    {
                        if (c1Transaction.RowSel > 0)
                        {
                            //c1Transaction.Row = c1Transaction.RowSel;
                            chkCPTExists(c1Transaction.RowSel);
                        }
                    }
                }
            }
        }

        private void c1Transaction_AfterRowColChange(object sender, C1.Win.C1FlexGrid.RangeEventArgs e)
        {
            try
            {
                //if ((e.OldRange.c1 == COL_POS_BTN) && (e.NewRange.c1 != COL_POS_BTN) ||
                //    (e.OldRange.c1 == COL_TOS_BTN) && (e.NewRange.c1 != COL_TOS_BTN) ||
                //    (e.OldRange.c1 == COL_CPT_BTN) && (e.NewRange.c1 != COL_CPT_BTN) ||
                //    (e.OldRange.c1 == COL_DX1_BTN) && (e.NewRange.c1 != COL_DX1_BTN) ||
                //    (e.OldRange.c1 == COL_DX2_BTN) && (e.NewRange.c1 != COL_DX2_BTN) ||
                //    (e.OldRange.c1 == COL_DX3_BTN) && (e.NewRange.c1 != COL_DX3_BTN) ||
                //    (e.OldRange.c1 == COL_DX4_BTN) && (e.NewRange.c1 != COL_DX4_BTN) ||
                //    (e.OldRange.c1 == COL_MOD1_BTN) && (e.NewRange.c1 != COL_MOD1_BTN) ||
                //    (e.OldRange.c1 == COL_MOD2_BTN) && (e.NewRange.c1 != COL_MOD2_BTN))
                //{ CloseInternalControl(); }

                if ((e.OldRange.c1 == COL_POSCODE) && (e.NewRange.c1 != COL_POSCODE) ||
                    (e.OldRange.c1 == COL_TOSCODE) && (e.NewRange.c1 != COL_TOSCODE) ||
                    (e.OldRange.c1 == COL_CPT_CODE) && (e.NewRange.c1 != COL_CPT_CODE) ||
                    (e.OldRange.c1 == COL_DX1_CODE) && (e.NewRange.c1 != COL_DX1_CODE) ||
                    (e.OldRange.c1 == COL_DX2_CODE) && (e.NewRange.c1 != COL_DX2_CODE) ||
                    (e.OldRange.c1 == COL_DX3_CODE) && (e.NewRange.c1 != COL_DX3_CODE) ||
                    (e.OldRange.c1 == COL_DX4_CODE) && (e.NewRange.c1 != COL_DX4_CODE) ||

                    (e.OldRange.c1 == COL_DX5_CODE) && (e.NewRange.c1 != COL_DX5_CODE) ||
                    (e.OldRange.c1 == COL_DX6_CODE) && (e.NewRange.c1 != COL_DX6_CODE) ||
                    (e.OldRange.c1 == COL_DX7_CODE) && (e.NewRange.c1 != COL_DX7_CODE) ||
                    (e.OldRange.c1 == COL_DX8_CODE) && (e.NewRange.c1 != COL_DX8_CODE) ||

                    (e.OldRange.c1 == COL_MOD1_CODE) && (e.NewRange.c1 != COL_MOD1_CODE) ||
                    (e.OldRange.c1 == COL_MOD2_CODE) && (e.NewRange.c1 != COL_MOD2_CODE) ||

                    (e.OldRange.c1 == COL_MOD3_CODE) && (e.NewRange.c1 != COL_MOD3_CODE) ||
                    (e.OldRange.c1 == COL_MOD4_CODE) && (e.NewRange.c1 != COL_MOD4_CODE) ||

                    (e.OldRange.c1 == COL_PROVIDER) && (e.NewRange.c1 != COL_PROVIDER))
                {
                    //CloseInternalControl();
                }
                else if ((e.OldRange.c1 == e.NewRange.c1))
                {
                    ///work not complete need to implement code to move the control down
                    //temp code..

                }
                else if (e.OldRange.r1 != e.NewRange.r1)
                {
                    ////...*** Code added on 20090821 by - Sagar Ghodke
                    ////...*** for set the last sorted row flag
                    // _isLastAddedRowSorted = false;
                    ////...*** End code added on 20090821 by - Sagar Ghodke

                }

                #region " Set the Transaction Line Number & Transaction Detail ID for selected Line "

                _transactionLineNo = 0;
                _transactionDetailId = 0;

                if (e.NewRange.r1 > 0)
                {
                    if (c1Transaction.GetData(e.NewRange.r1, COL_NO) != null && Convert.ToString(c1Transaction.GetData(e.NewRange.r1, COL_NO)) != "")
                    { _transactionLineNo = Convert.ToInt64(c1Transaction.GetData(e.NewRange.r1, COL_NO)); }
                    if (c1Transaction.GetData(e.NewRange.r1, COL_TRANSACTION_DETAIL_ID) != null && Convert.ToString(c1Transaction.GetData(e.NewRange.r1, COL_TRANSACTION_DETAIL_ID)) != "")
                    { _transactionDetailId = Convert.ToInt64(c1Transaction.GetData(e.NewRange.r1, COL_TRANSACTION_DETAIL_ID)); }
                }

                #endregion " Set the Transaction Line Number & Transaction Detail ID for selected Line "
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {

            }
        }

        private void c1Transaction_LeaveEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            try
            {
                ///here .... .. . .
                ///
                switch (e.Col)
                {
                    case COL_POSCODE:
                    case COL_TOSCODE:
                    case COL_CPT_CODE:
                    case COL_DX1_CODE:
                    case COL_DX2_CODE:
                    case COL_DX3_CODE:
                    case COL_DX4_CODE:
                    case COL_DX5_CODE:
                    case COL_DX6_CODE:
                    case COL_DX7_CODE:
                    case COL_DX8_CODE:
                    case COL_MOD1_CODE:
                    case COL_MOD2_CODE:
                    case COL_MOD3_CODE:
                    case COL_MOD4_CODE:
                    case COL_PROVIDER:
                        if (c1Transaction.Editor != null)
                        {
                            c1Transaction.ChangeEdit -= new System.EventHandler(this.c1Transaction_ChangeEdit);
                            c1Transaction.Editor.Text = "";
                            c1Transaction.ChangeEdit += new System.EventHandler(this.c1Transaction_ChangeEdit);
                        }
                        break;
                }

                if (tmrChangeEditSearch != null) { tmrChangeEditSearch.Stop(); }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void c1Transaction_BeforeSelChange(object sender, C1.Win.C1FlexGrid.RangeEventArgs e)
        {
            try
            {
                if (ogloGridListControl != null)
                {
                    if (e.OldRange.r1 != e.NewRange.r1)
                    {
                        e.Cancel = true;
                    }
                }

                iPreCol = e.OldRange.c1;
                iPreRow = e.OldRange.r1;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void c1Transaction_AfterDragRow(object sender, DragRowColEventArgs e)
        {
            _AutoSort = false;
            SortControl();
        }

        private void c1Transaction_Click(object sender, EventArgs e)
        {

            if (CurrentTransactionLine > 0)
            {
                CurrentDOS = Convert.ToDateTime(c1Transaction.GetData(CurrentTransactionLine, COL_DATEFROM));
            }

            IsInternalControlActive = false;
        }

        private void c1Transaction_AfterEdit(object sender, RowColEventArgs e)
        {
            gloBilling ogloBilling = null;
            try
            {

                ogloBilling = new gloBilling(_DatabaseConnectionString, "");
                //Check if the service date is present in the Chekin Table(i.e PatientTracking)
                //if (e.Col == COL_DATEFROM && e.Row == 1)
                //{
                //    IsFirstDosChange = true;
                //    switch (ogloBilling.GetICDCodeType(_nContactID, getfirstservicelineDos()))
                //    {
                //        case gloGlobal.gloICD.CodeRevision.ICD10:
                //            IcdCodeType = gloGlobal.gloICD.CodeRevision.ICD10.GetHashCode();
                //            break;
                //        case gloGlobal.gloICD.CodeRevision.ICD9:
                //            IcdCodeType = gloGlobal.gloICD.CodeRevision.ICD9.GetHashCode();
                //            break;
                //        default:
                //            IcdCodeType = gloGlobal.gloICD.CodeRevision.ICD9.GetHashCode();
                //            break;
                //    }

                //}

                if (e.Col == COL_DATEFROM)
                {
                    _sServiceDate = Convert.ToString(c1Transaction.GetData(CurrentTransactionLine, CurrentColumn));
                    IsDosChange = true;
                    if (_sServiceDate != null && _sServiceDate != "")
                    {
                        DateTime _dtServiveDate = Convert.ToDateTime(_sServiceDate);

                    }




                    if (ogloBilling.IsServiceDatePresent(PatientID, _sServiceDate) == false)
                    {
                        string _Message = "No service given for patient : " + ogloBilling.GetPatient(PatientID) + " on selected date.  ";
                        //MessageBox.Show(_Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    if (date_Changed != null) { date_Changed(this, e); }
                }
                else if (e.Col == COL_DATETO)
                {
                    if (date_Changed != null) { date_Changed(this, e); }
                }
                else if (e.Col == COL_POSCODE)  //Check for POS CODE if blank then change POS DEsc to blank
                {
                    if (c1Transaction.GetData(c1Transaction.RowSel, COL_POSCODE) != null)
                    {
                        if (Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_POSCODE)) == "")
                        {
                            c1Transaction.SetData(c1Transaction.RowSel, COL_POSDESC, "");
                        }
                    }
                }
                else if (e.Col == COL_UNIT)
                {
                    if (c1Transaction.GetData(c1Transaction.RowSel, COL_UNIT) == null || Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_UNIT)) == ""
                        || Convert.ToDecimal(c1Transaction.GetData(c1Transaction.RowSel, COL_UNIT)) <= 0)
                    {
                        c1Transaction.SetData(c1Transaction.RowSel, COL_UNIT, 1);
                    }
                    else
                    {
                        decimal _unit = gloCharges.FormatNumber(Convert.ToDecimal(c1Transaction.GetData(c1Transaction.RowSel, COL_UNIT)));
                        //c1Transaction.SetData(c1Transaction.RowSel, COL_UNIT,0 );
                        c1Transaction.SetData(c1Transaction.RowSel, COL_UNIT, _unit);
                    }
                }
                else if (e.Col >= COL_DX1_CODE && e.Col <= COL_DX8_CODE)
                {

                    if (e.Col == COL_DX1_CODE)  //Check for Diagnosis  if blank then change Diagnosis DEsc to blank
                    {
                        if (c1Transaction.GetData(c1Transaction.RowSel, COL_DX1_CODE) != null)
                        {
                            if (Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_DX1_CODE)) == "")
                            {
                                c1Transaction.SetData(c1Transaction.RowSel, COL_DX1_DESC, "");
                                c1Transaction.SetData(c1Transaction.RowSel, COL_DX1_PTR, false);
                                c1Transaction.SetData(c1Transaction.RowSel, COL_LINEPRIMARY_DXCODE, "");
                                c1Transaction.SetData(c1Transaction.RowSel, COL_LINEPRIMARY_DXDESC, "");
                            }
                        }
                    }
                    else if (e.Col == COL_DX2_CODE)  //Check for Diagnosis  if blank then change Diagnosis DEsc to blank
                    {
                        if (c1Transaction.GetData(c1Transaction.RowSel, COL_DX2_CODE) != null)
                        {
                            if (Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_DX2_CODE)) == "")
                            {
                                c1Transaction.SetData(c1Transaction.RowSel, COL_DX2_DESC, "");
                                c1Transaction.SetData(c1Transaction.RowSel, COL_DX2_PTR, false);
                            }
                        }
                    }
                    else if (e.Col == COL_DX3_CODE)  //Check for Diagnosis  if blank then change Diagnosis DEsc to blank
                    {
                        if (c1Transaction.GetData(c1Transaction.RowSel, COL_DX3_CODE) != null)
                        {
                            if (Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_DX3_CODE)) == "")
                            {
                                c1Transaction.SetData(c1Transaction.RowSel, COL_DX3_DESC, "");
                                c1Transaction.SetData(c1Transaction.RowSel, COL_DX3_PTR, false);
                            }
                        }
                    }
                    else if (e.Col == COL_DX4_CODE)  //Check for Diagnosis  if blank then change Diagnosis DEsc to blank
                    {
                        if (c1Transaction.GetData(c1Transaction.RowSel, COL_DX4_CODE) != null)
                        {
                            if (Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_DX4_CODE)) == "")
                            {
                                c1Transaction.SetData(c1Transaction.RowSel, COL_DX4_DESC, "");
                                c1Transaction.SetData(c1Transaction.RowSel, COL_DX4_PTR, false);
                            }
                        }
                    }


                    RemoveDxModified();
                    evtModifyDxRowCol = null;
                    evtModifyDx = new TrnCtrlColValChangeEventArg();

                }
                else if (e.Col == COL_CHARGES)  //Check for max value create it zero
                {
                    if (c1Transaction.GetData(c1Transaction.RowSel, COL_CHARGES) != null)
                    {
                        if (Convert.ToDecimal(c1Transaction.GetData(c1Transaction.RowSel, COL_CHARGES)) == 79228162514264300000000000000M)
                        {
                            c1Transaction.SetData(c1Transaction.RowSel, COL_CHARGES, 0);
                        }
                    }
                }
                else if (e.Col == COL_ALLOWED)  //Check for max value create it zero
                {
                    if (c1Transaction.GetData(c1Transaction.RowSel, COL_ALLOWED) != null)
                    {
                        if (Convert.ToDecimal(c1Transaction.GetData(c1Transaction.RowSel, COL_ALLOWED)) == 79228162514264300000000000000M)
                        {
                            c1Transaction.SetData(c1Transaction.RowSel, COL_ALLOWED, 0);
                        }
                    }
                }
                else if (e.Col == COL_CPT_CODE)  //Check for CPT CODE if blank then change CPT DEsc to blank
                {
                    if (c1Transaction.GetData(c1Transaction.RowSel, COL_CPT_CODE) != null)
                    {
                        if (Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_CPT_CODE)) == "")
                        {
                            c1Transaction.SetData(c1Transaction.RowSel, COL_CPT_DESC, "");
                        }
                    }
                }
                else if (e.Col == COL_MOD1_CODE)  //Check for Modifier CODE if blank then change Modifier DEsc to blank
                {
                    if (c1Transaction.GetData(c1Transaction.RowSel, COL_MOD1_CODE) != null)
                    {
                        if (Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_MOD1_CODE)) == "")
                        {
                            c1Transaction.SetData(c1Transaction.RowSel, COL_MOD1_DESC, "");
                        }
                    }
                }
                else if (e.Col == COL_MOD2_CODE)  //Check for Modifier CODE if blank then change Modifier DEsc to blank
                {
                    if (c1Transaction.GetData(c1Transaction.RowSel, COL_MOD2_CODE) != null)
                    {
                        if (Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_MOD2_CODE)) == "")
                        {
                            c1Transaction.SetData(c1Transaction.RowSel, COL_MOD2_DESC, "");
                        }
                    }
                }
                else if (e.Col == COL_AUTHORIZATIONNO)
                {
                    if (e.Row == 1)
                    {
                        CLIA_Enter();
                    }
                }


                if ((CurrentColumn >= COL_MOD1_CODE && CurrentColumn <= COL_MOD4_CODE) || CurrentColumn == Col_Dos)
                {
                    if (c1Transaction.GetData(c1Transaction.RowSel, iPreCol) != null)
                    {

                        int iCurrentRow = this.CurrentTransactionLine;
                        //if (_AllowChargeModification == true)
                        //{
                        //    //this.SelectTransactionLine(iPreRow);                        
                        //    SetFNFCharges();
                        //}
                        //this.SelectTransactionLine(iCurrentRow);
                        //c1Transaction.RowSel = iCurrentRow;
                        c1Transaction.RowSel = this.CurrentTransactionLine;
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
                if (ogloBilling != null)
                {
                    ogloBilling.Dispose();
                    ogloBilling = null;
                }
            }
        }

        private void c1Transaction_AfterSelChange(object sender, RangeEventArgs e)
        {
            try
            {
                //Event delegated for the purpose to set Insurances as per row selection chage
                //on the frmBillingTransaction form
                onGrid_SelChanged(sender, e);
            }
            catch (Exception)// ex)
            {
                //gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                //ex.ToString();
                //ex = null;
            }


        }

        private void c1Transaction_CellButtonClick(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            try
            {

                switch (e.Col)
                {
                    case COL_POS_BTN:
                        {
                            OpenInternalControl(gloGridListControlType.POS, "Place Of Service", false, e.Row, e.Col);
                        }
                        break;
                    case COL_TOS_BTN:
                        {
                            OpenInternalControl(gloGridListControlType.TOS, "Type Of Service", false, e.Row, e.Col);
                        }
                        break;
                    case COL_CPT_BTN:
                        {
                            OpenInternalControl(gloGridListControlType.CPT, "CPT", false, e.Row, e.Col);
                        }
                        break;
                    case COL_DX1_BTN:
                    case COL_DX2_BTN:
                    case COL_DX3_BTN:
                    case COL_DX4_BTN:
                    case COL_DX5_BTN:
                    case COL_DX6_BTN:
                    case COL_DX7_BTN:
                    case COL_DX8_BTN:
                        {
                            OpenInternalControl(gloGridListControlType.ICD9, "ICD9", false, e.Row, e.Col);
                        }
                        break;
                    case COL_MOD1_BTN:
                    case COL_MOD2_BTN:
                    case COL_MOD3_BTN:
                    case COL_MOD4_BTN:
                        {
                            OpenInternalControl(gloGridListControlType.Modifier, "Modifier", false, e.Row, e.Col);
                        }
                        break;
                    case COL_PROVIDER_BTN:
                        {
                            OpenInternalControl(gloGridListControlType.Providers, "Provider", false, e.Row, e.Col);
                        }
                        break;
                }



            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {

            }
        }

        private void c1Transaction_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                HitTestInfo hitInfo = c1Transaction.HitTest(e.X, e.Y);
                c1Transaction.RowSel = hitInfo.Row;
                if (hitInfo.Column != COL_NO)
                {
                    c1Transaction.ColSel = hitInfo.Column;
                    c1Transaction.Select(hitInfo.Row, hitInfo.Column);
                }

                //if (hitInfo.Column != iPreCol)
                //{
                //    adjustDxMod();
                //}
                if (hitInfo.Row > 0)
                {
                    pnlInternalControl.Visible = false;
                    pnlInternalControl.SendToBack();
                    IsInternalControlActive = false;
                    adjustDxMod();
                }

                if (e.Button == MouseButtons.Right)
                {
                    if (c1Transaction.Rows.Count > 0)
                    {
                        if ((hitInfo.Column >= COL_DX1_CODE && hitInfo.Column <= COL_DX8_CODE) && hitInfo.Row > 0)
                        {
                            if (c1Transaction.GetData(hitInfo.Row, hitInfo.Column) != null
                                && Convert.ToString(c1Transaction.GetData(hitInfo.Row, hitInfo.Column)) != "")
                            {
                                c1Transaction.ContextMenuStrip = cmnu_Apply;

                                //cmnu_Apply.Items["tls_cmnu_Self"].Visible = false;
                                cmnu_Apply.Items["tls_cmnu_ApplyAll"].Visible = true;
                                cmnu_Apply.Items["tls_cmnu_OverwriteApplyAll"].Visible = true;
                            }

                            else
                            { c1Transaction.ContextMenuStrip = null; }
                        }

                        else
                        { c1Transaction.ContextMenuStrip = null; }
                    }
                }

                //...*** Code added on 20090731 by - Sagar Ghodke
                //...*** Code added to select the first line if mouse click is done
                //...*** on empty area of control
                if (c1Transaction.RowSel <= 0)
                {
                    if (c1Transaction != null && c1Transaction.Rows.Count > 1)
                    {
                        //c1Transaction.Focus();
                        c1Transaction.Select(c1Transaction.Rows.Count - 1, hitInfo.Column, true);
                    }
                }
                //...*** End code add 20090731,Sagar Ghodke
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void c1Transaction_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            HitTestInfo hitInfo = c1Transaction.HitTest(e.X, e.Y);
            GeneralNotes oNotes = null;
            try
            {
                if (hitInfo.Column == COL_NO && hitInfo.Row > 0)
                {
                    oNotes = GetNotes(hitInfo.Row);
                    if (oNotes != null && oNotes.Count > 0)
                    {
                        RowColEventArgs rcEvt = new RowColEventArgs(hitInfo.Row, hitInfo.Column);
                        show_LineNotes(sender, rcEvt, oNotes);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void c1Transaction_MouseClick(object sender, MouseEventArgs e)
        {
            HitTestInfo hitInfo = c1Transaction.HitTest(e.X, e.Y);
            //  GeneralNotes oNotes = null;
            try
            {
                //if (hitInfo.Column == COL_NO && hitInfo.Row > 0)
                //{
                //    oNotes = GetNotes(hitInfo.Row);
                //    if (oNotes != null && oNotes.Count > 0)
                //    {
                //        RowColEventArgs rcEvt = new RowColEventArgs(hitInfo.Row, hitInfo.Column);
                //        show_LineNotes(sender, rcEvt, oNotes);
                //    }
                //}
                //else 
                if (hitInfo.Row == 0)
                {
                    _AutoSort = false;
                    if (_AllowDragDrop)
                    {
                        SortFlags sr = c1Transaction.Cols[hitInfo.Column].Sort;
                        if (sr == SortFlags.None || sr == SortFlags.Descending)
                        { c1Transaction.Sort(SortFlags.Ascending, hitInfo.Column); }
                        else if (sr == SortFlags.Ascending)
                        { c1Transaction.Sort(SortFlags.Descending, hitInfo.Column); }
                        SortControl();
                    }
                    if (c1Transaction.Rows.Count > 1)
                    {
                        if (c1Transaction.RowSel > 0)
                        {
                            //c1Transaction.Row = c1Transaction.RowSel;
                            chkCPTExists(c1Transaction.RowSel);
                        }

                    }
                }
                if (c1Transaction.Rows.Count > 1)
                {
                    if (hitInfo.Row > 0)
                    {
                        //c1Transaction.Row = hitInfo.Row;
                        chkCPTExists(c1Transaction.RowSel);
                    }

                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void chkCPTExists(int rowIndex)
        {
            try
            {
                if (rowIndex > 0)
                {
                    for (int i = 1; i <= c1Transaction.Rows.Count - 1; i++)
                    {
                        if (c1Transaction.GetData(i, COL_CPT_CODE) == null || Convert.ToString(c1Transaction.GetData(i, COL_CPT_CODE)).Trim() == "")
                        {
                            c1Transaction.SetData(i, COL_NDCCODE, "");
                            c1Transaction.SetData(i, COL_NDCUNIT, null);
                            c1Transaction.SetData(i, COL_NDCUNITCODE, null);
                            c1Transaction.SetData(i, COL_NDCUNITDESCRITION, null);
                            c1Transaction.SetData(i, COL_PRESCRIPTION, null);
                            SetEPSDTNotesNDCCodeFlag(i);
                        }
                    }
                    c1Transaction.Row = rowIndex;
                    onGrid_SelChanged(null, null);

                }
            }
            catch
            {

            }

        }

        private void gloBillingTransaction_Scroll(object sender, ScrollEventArgs e)
        {
            try
            {
                RePositionInternalControl();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void gloBillingTransaction_Leave(object sender, EventArgs e)
        {
            IsInternalControlActive = false;
        }

        private void c1Transaction_AfterScroll(object sender, RangeEventArgs e)
        {
            try
            {
                RePositionInternalControl();
                int x = c1Transaction.ScrollPosition.X;
                c1Total.ScrollPosition = new Point(x, c1Total.ScrollPosition.X);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void c1Transaction_MouseMove(object sender, MouseEventArgs e)
        {
            //if (c1Transaction.HitTest(e.X, e.Y).Column >= COL_POSCODE && c1Transaction.HitTest(e.X, e.Y).Column <= COL_DX8_CODE )
            //Mahesh Nawal 20100513 Change the if condition because of the Modifier ToolTip
            if (c1Transaction.HitTest(e.X, e.Y).Column >= COL_POSCODE && c1Transaction.HitTest(e.X, e.Y).Column <= COL_MOD4_CODE && c1Transaction.HitTest(e.X, e.Y).Column != COL_ISEMG)
            {
                //gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, (C1FlexGrid)sender, e.Location, true);
                gloC1FlexStyle.ShowToolTipForBillingServiceLine(C1SuperTooltip1, (C1FlexGrid)sender, e.Location, true);

            }
            else
            {
                gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, (C1FlexGrid)sender, e.Location);
                //gloC1FlexStyle.ShowToolTipForBillingServiceLine(C1SuperTooltip1, (C1FlexGrid)sender, e.Location);

            }
        }

        private void c1Transaction_KeyPressEdit(object sender, KeyPressEditEventArgs e)
        {
            #region "Numeric Validation"
            if (c1Transaction.ColSel == COL_CHARGES || c1Transaction.ColSel == COL_UNIT || c1Transaction.ColSel == COL_TOTAL || c1Transaction.ColSel == COL_ALLOWED)
            {
                decimal _result = Convert.ToDecimal(c1Transaction.GetData(c1Transaction.RowSel, c1Transaction.ColSel));
                //MessageBox.Show(e.KeyCode.ToString());
                if (e.KeyChar == Convert.ToChar("-"))
                {
                    e.Handled = true;
                }

            }

            if (c1Transaction.ColSel == COL_AUTHORIZATIONNO)
            {
                c1Transaction.Rows[c1Transaction.RowSel].UserData = false;                
            }
            #endregion
        }

        private void c1Transaction_KeyDownEdit(object sender, KeyEditEventArgs e)
        {
            #region "Numeric Validation"
            if (c1Transaction.ColSel == COL_CHARGES || c1Transaction.ColSel == COL_UNIT || c1Transaction.ColSel == COL_TOTAL || c1Transaction.ColSel == COL_ALLOWED)
            {
                decimal _result = Convert.ToDecimal(c1Transaction.GetData(c1Transaction.RowSel, c1Transaction.ColSel));
                //MessageBox.Show(e.KeyCode.ToString());
                if (e.KeyCode == Keys.OemMinus || e.KeyCode == Keys.Subtract)
                {
                    e.Handled = true;
                }

            }
            else if (c1Transaction.Col == COL_DX1_CODE || c1Transaction.Col == COL_DX2_CODE ||
                   c1Transaction.Col == COL_DX3_CODE || c1Transaction.Col == COL_DX4_CODE || c1Transaction.Col == COL_DX5_CODE ||
                   c1Transaction.Col == COL_DX6_CODE || c1Transaction.Col == COL_DX7_CODE || c1Transaction.Col == COL_DX8_CODE
                   )
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (isSearchEventFired == false)
                    {
                        e.Handled = true;
                        OnChangeEdit();
                    }
                }
            }
            #endregion
        }

        private void c1Transaction_SetupEditor(object sender, RowColEventArgs e)
        {

        }

        #endregion " C1 Grid Events "

        #region " Public & Private Methods "


        public void ShowTotal()
        {
            #region " Show Total "

            c1Total.SetData(0, COL_CHARGES, GetTotalCharges());
            c1Total.SetData(0, COL_ALLOWED, GetTotalAllowed());
            c1Total.SetData(0, COL_TOTAL, GetGrandTotal());

            #endregion
        }

        public void AddTransactionLine()
        {
            c1Transaction.Rows.Add();
            c1Transaction.SetData(c1Transaction.Rows.Count - 1, COL_NO, Convert.ToString(c1Transaction.Rows.Count - 1));

            GetDefaultTOSPOS();
            c1Transaction.SetData(c1Transaction.Rows.Count - 1, COL_DATEFROM, DateTime.Now.ToShortDateString());

            //DOS To Implementation
            if (_showTilldateColumn == true)
            {
                if (_showTillDateColumnUseNullDate == true)
                {
                    c1Transaction.SetData(c1Transaction.Rows.Count - 1, COL_DATETO, null);
                }
                else
                {
                    c1Transaction.SetData(c1Transaction.Rows.Count - 1, COL_DATETO, DateTime.Now.ToShortDateString());
                }
            }
            else
            {
                c1Transaction.SetData(c1Transaction.Rows.Count - 1, COL_DATETO, DateTime.Now.ToShortDateString());
            }

            // line added for split claim option on New Charges Entry  11-11-2013
            c1Transaction.SetData(c1Transaction.Rows.Count - 1, COL_SELFCLAIM, false);

            c1Transaction.SetData(c1Transaction.Rows.Count - 1, COL_TOSCODE, _DefaultTOSCode);
            c1Transaction.SetData(c1Transaction.Rows.Count - 1, COL_TOSDESC, _DefaultTOSDesc);
            c1Transaction.SetData(c1Transaction.Rows.Count - 1, COL_POSCODE, _DefaultPOSCode);
            c1Transaction.SetData(c1Transaction.Rows.Count - 1, COL_POSDESC, _DefaultPOSDesc);

            if (c1Transaction.Rows.Count > 2)
            {
                if (Convert.ToString(c1Transaction.GetData(c1Transaction.Rows.Count - 2, COL_POSCODE)) != "")
                {
                    c1Transaction.SetData(c1Transaction.Rows.Count - 1, COL_POSCODE, Convert.ToString(c1Transaction.GetData(c1Transaction.Rows.Count - 2, COL_POSCODE)));
                    c1Transaction.SetData(c1Transaction.Rows.Count - 1, COL_POSDESC, Convert.ToString(c1Transaction.GetData(c1Transaction.Rows.Count - 2, COL_POSDESC)));
                }
                //*******************
                // solving issue TFSID-1586 (mantis id-1450) 
                //if we delete POS value and add new service line then it must show last line POS value i.e blank
                else
                {
                    c1Transaction.SetData(c1Transaction.Rows.Count - 1, COL_POSCODE, "");
                    c1Transaction.SetData(c1Transaction.Rows.Count - 1, COL_POSDESC, "");
                }
                //****************** end code

            }

            if (_DefaultRenderingProviderID > 0)
            {
                //20100503 - Hot Fix Referral Provider Issue
                if (c1Transaction.Rows.Count > 2)
                {
                    c1Transaction.SetData(c1Transaction.Rows.Count - 1, COL_PROVIDER_ID, Convert.ToInt64(c1Transaction.GetData(c1Transaction.Rows.Count - 2, COL_PROVIDER_ID)));
                    c1Transaction.SetData(c1Transaction.Rows.Count - 1, COL_PROVIDER, Convert.ToString(c1Transaction.GetData(c1Transaction.Rows.Count - 2, COL_PROVIDER)));
                }
                else
                {
                    c1Transaction.SetData(c1Transaction.Rows.Count - 1, COL_PROVIDER_ID, _DefaultRenderingProviderID);
                    c1Transaction.SetData(c1Transaction.Rows.Count - 1, COL_PROVIDER, _DefaultRenderringProviderName);
                }
            }
            c1Transaction.Select(c1Transaction.Rows.Count - 1, COL_DATEFROM);

            //**.Code added on 20090511 by - Sagar Ghodke

            c1Transaction.SetCellCheck(c1Transaction.Rows.Count - 1, COL_ISLABCPT, CheckEnum.Unchecked);
            c1Transaction.SetCellCheck(c1Transaction.Rows.Count - 1, COL_ISEMG, CheckEnum.Unchecked);
            c1Transaction.SetData(c1Transaction.Rows.Count - 1, COL_AUTHORIZATIONNO, "");
            c1Transaction.SetCellCheck(c1Transaction.Rows.Count - 1, COL_SENTTOCLAIM, CheckEnum.Checked);

            //**.End Code add 20090511,Sagar Ghodke 

            //Add the previous Line Modifiers to newly added line

            if (c1Transaction.Rows.Count > 2)
            {
                int _LastLineNumber = c1Transaction.Rows.Count - 1;
                int _SecondLastLineNumber = _LastLineNumber - 1;

                ////...*** Code added on 20090821 by - Sagar Ghodke
                ////...*** Code added to pull data from last added line for DOS,TOS,POS etc.

                //if (_isLastAddedRowSorted == true)
                //{ _SecondLastLineNumber = _sortedRowIndex; }

                ////...*** End code add on 20090821 by - Sagar Ghodke

                if (Convert.ToString(c1Transaction.GetData(_SecondLastLineNumber, COL_DX1_CODE)) != "")
                {
                    c1Transaction.SetData(_LastLineNumber, COL_DX1_CODE, c1Transaction.GetData(_SecondLastLineNumber, COL_DX1_CODE));
                    c1Transaction.SetData(_LastLineNumber, COL_DX1_DESC, c1Transaction.GetData(_SecondLastLineNumber, COL_DX1_DESC));
                    c1Transaction.SetCellCheck(_LastLineNumber, COL_DX1_PTR, CheckEnum.Checked);

                    //**..Set first Diagnosis as Primary Line Diagnosis
                    c1Transaction.SetData(_LastLineNumber, COL_LINEPRIMARY_DXCODE, c1Transaction.GetData(_SecondLastLineNumber, COL_DX1_CODE));
                    c1Transaction.SetData(_LastLineNumber, COL_LINEPRIMARY_DXDESC, c1Transaction.GetData(_SecondLastLineNumber, COL_DX1_DESC));

                }
                if (Convert.ToString(c1Transaction.GetData(_SecondLastLineNumber, COL_DX2_CODE)) != "")
                {
                    c1Transaction.SetData(_LastLineNumber, COL_DX2_CODE, c1Transaction.GetData(_SecondLastLineNumber, COL_DX2_CODE));
                    c1Transaction.SetData(_LastLineNumber, COL_DX2_DESC, c1Transaction.GetData(_SecondLastLineNumber, COL_DX2_DESC));
                    c1Transaction.SetCellCheck(_LastLineNumber, COL_DX2_PTR, CheckEnum.Checked);
                }
                if (Convert.ToString(c1Transaction.GetData(_SecondLastLineNumber, COL_DX3_CODE)) != "")
                {
                    c1Transaction.SetData(_LastLineNumber, COL_DX3_CODE, c1Transaction.GetData(_SecondLastLineNumber, COL_DX3_CODE));
                    c1Transaction.SetData(_LastLineNumber, COL_DX3_DESC, c1Transaction.GetData(_SecondLastLineNumber, COL_DX3_DESC));
                    c1Transaction.SetCellCheck(_LastLineNumber, COL_DX3_PTR, CheckEnum.Checked);
                }
                if (Convert.ToString(c1Transaction.GetData(_SecondLastLineNumber, COL_DX4_CODE)) != "")
                {
                    c1Transaction.SetData(_LastLineNumber, COL_DX4_CODE, c1Transaction.GetData(_SecondLastLineNumber, COL_DX4_CODE));
                    c1Transaction.SetData(_LastLineNumber, COL_DX4_DESC, c1Transaction.GetData(_SecondLastLineNumber, COL_DX4_DESC));
                    c1Transaction.SetCellCheck(_LastLineNumber, COL_DX4_PTR, CheckEnum.Checked);
                }
                if (Convert.ToString(c1Transaction.GetData(_SecondLastLineNumber, COL_DX5_CODE)) != "")
                {
                    c1Transaction.SetData(_LastLineNumber, COL_DX5_CODE, c1Transaction.GetData(_SecondLastLineNumber, COL_DX5_CODE));
                    c1Transaction.SetData(_LastLineNumber, COL_DX5_DESC, c1Transaction.GetData(_SecondLastLineNumber, COL_DX5_DESC));
                    c1Transaction.SetCellCheck(_LastLineNumber, COL_DX5_PTR, CheckEnum.Checked);
                }
                if (Convert.ToString(c1Transaction.GetData(_SecondLastLineNumber, COL_DX6_CODE)) != "")
                {
                    c1Transaction.SetData(_LastLineNumber, COL_DX6_CODE, c1Transaction.GetData(_SecondLastLineNumber, COL_DX6_CODE));
                    c1Transaction.SetData(_LastLineNumber, COL_DX6_DESC, c1Transaction.GetData(_SecondLastLineNumber, COL_DX6_DESC));
                    c1Transaction.SetCellCheck(_LastLineNumber, COL_DX6_PTR, CheckEnum.Checked);
                }
                if (Convert.ToString(c1Transaction.GetData(_SecondLastLineNumber, COL_DX7_CODE)) != "")
                {
                    c1Transaction.SetData(_LastLineNumber, COL_DX7_CODE, c1Transaction.GetData(_SecondLastLineNumber, COL_DX7_CODE));
                    c1Transaction.SetData(_LastLineNumber, COL_DX7_DESC, c1Transaction.GetData(_SecondLastLineNumber, COL_DX7_DESC));
                    c1Transaction.SetCellCheck(_LastLineNumber, COL_DX7_PTR, CheckEnum.Checked);
                }
                if (Convert.ToString(c1Transaction.GetData(_SecondLastLineNumber, COL_DX8_CODE)) != "")
                {
                    c1Transaction.SetData(_LastLineNumber, COL_DX8_CODE, c1Transaction.GetData(_SecondLastLineNumber, COL_DX8_CODE));
                    c1Transaction.SetData(_LastLineNumber, COL_DX8_DESC, c1Transaction.GetData(_SecondLastLineNumber, COL_DX8_DESC));
                    c1Transaction.SetCellCheck(_LastLineNumber, COL_DX8_PTR, CheckEnum.Checked);
                }

                //..*** Code added on 20090620 by Sagar Ghodke
                //..*** Code added to set the DOS of previous line
                if (Convert.ToString(c1Transaction.GetData(_SecondLastLineNumber, COL_DATEFROM)) != "")
                {
                    c1Transaction.SetData(_LastLineNumber, COL_DATEFROM, c1Transaction.GetData(_SecondLastLineNumber, COL_DATEFROM));
                    //c1Transaction.SetData(_LastLineNumber, COL_DATEFROM, c1Transaction.GetData(_SecondLastLineNumber, COL_DATEFROM));
                }

                if (Convert.ToString(c1Transaction.GetData(_SecondLastLineNumber, COL_DATETO)) != "")
                {
                    c1Transaction.SetData(_LastLineNumber, COL_DATETO, c1Transaction.GetData(_SecondLastLineNumber, COL_DATETO));
                    //c1Transaction.SetData(_LastLineNumber, COL_DATETO, c1Transaction.GetData(_SecondLastLineNumber, COL_DATETO));
                }
                //..*** End Code add on 20090620 by Sagar Ghodke

            }


            //Code added on 20090710 to re index line no for display purpose - Vinayak Gadekar
            for (int i = 1; i <= c1Transaction.Rows.Count - 1; i++)
            {
                c1Transaction.SetData(i, COL_NO, i.ToString());
            }

            SetCurrencyCellValue(c1Transaction.Rows.Count - 1);
            c1Transaction.SetData(c1Transaction.Rows.Count - 1, COL_ALLOWED, null);
            c1Transaction.SetData(c1Transaction.Rows.Count - 1, COL_ACTUAL_ALLOWED, null);
        }

        public void AddTransactionLine(DateTime ServiceDate)
        {
            //This method is used from Missing Charges Report

            c1Transaction.Rows.Add();
            c1Transaction.SetData(c1Transaction.Rows.Count - 1, COL_NO, Convert.ToString(c1Transaction.Rows.Count - 1));

            GetDefaultTOSPOS();
            c1Transaction.SetData(c1Transaction.Rows.Count - 1, COL_DATEFROM, ServiceDate);

            //DOS To Implementation
            if (_showTilldateColumn == true)
            {
                if (_showTillDateColumnUseNullDate == true)
                {
                    c1Transaction.SetData(c1Transaction.Rows.Count - 1, COL_DATETO, null);
                }
                else
                {
                    c1Transaction.SetData(c1Transaction.Rows.Count - 1, COL_DATETO, ServiceDate);
                }
            }
            else
            {
                c1Transaction.SetData(c1Transaction.Rows.Count - 1, COL_DATETO, ServiceDate);
            }


            c1Transaction.SetData(c1Transaction.Rows.Count - 1, COL_TOSCODE, _DefaultTOSCode);
            c1Transaction.SetData(c1Transaction.Rows.Count - 1, COL_TOSDESC, _DefaultTOSDesc);
            c1Transaction.SetData(c1Transaction.Rows.Count - 1, COL_POSCODE, _DefaultPOSCode);
            c1Transaction.SetData(c1Transaction.Rows.Count - 1, COL_POSDESC, _DefaultPOSDesc);

            if (c1Transaction.Rows.Count > 2)
            {
                if (Convert.ToString(c1Transaction.GetData(c1Transaction.Rows.Count - 2, COL_POSCODE)) != "")
                {
                    c1Transaction.SetData(c1Transaction.Rows.Count - 1, COL_POSCODE, Convert.ToString(c1Transaction.GetData(c1Transaction.Rows.Count - 2, COL_POSCODE)));
                    c1Transaction.SetData(c1Transaction.Rows.Count - 1, COL_POSDESC, Convert.ToString(c1Transaction.GetData(c1Transaction.Rows.Count - 2, COL_POSDESC)));
                }


            }

            if (_DefaultRenderingProviderID > 0)
            {
                c1Transaction.SetData(c1Transaction.Rows.Count - 1, COL_PROVIDER_ID, _DefaultRenderingProviderID);
                c1Transaction.SetData(c1Transaction.Rows.Count - 1, COL_PROVIDER, _DefaultRenderringProviderName);
            }
            c1Transaction.Select(c1Transaction.Rows.Count - 1, COL_DATEFROM);

            //**..Code added on 20090511 by - Sagar Ghodke

            c1Transaction.SetCellCheck(c1Transaction.Rows.Count - 1, COL_ISLABCPT, CheckEnum.Unchecked);
            c1Transaction.SetCellCheck(c1Transaction.Rows.Count - 1, COL_ISEMG, CheckEnum.Unchecked);
            c1Transaction.SetData(c1Transaction.Rows.Count - 1, COL_AUTHORIZATIONNO, "");
            c1Transaction.SetCellCheck(c1Transaction.Rows.Count - 1, COL_SENTTOCLAIM, CheckEnum.Checked);

            //**..End Code add 20090511,Sagar Ghodke


            //Add the previous Line Modifiers to newly added line

            if (c1Transaction.Rows.Count > 2)
            {
                int _LastLineNumber = c1Transaction.Rows.Count - 1;
                int _SecondLastLineNumber = _LastLineNumber - 1;

                if (Convert.ToString(c1Transaction.GetData(_SecondLastLineNumber, COL_DX1_CODE)) != "")
                {
                    c1Transaction.SetData(_LastLineNumber, COL_DX1_CODE, c1Transaction.GetData(_SecondLastLineNumber, COL_DX1_CODE));
                    c1Transaction.SetData(_LastLineNumber, COL_DX1_DESC, c1Transaction.GetData(_SecondLastLineNumber, COL_DX1_DESC));
                    c1Transaction.SetCellCheck(_LastLineNumber, COL_DX1_PTR, CheckEnum.Checked);
                }
                if (Convert.ToString(c1Transaction.GetData(_SecondLastLineNumber, COL_DX2_CODE)) != "")
                {
                    c1Transaction.SetData(_LastLineNumber, COL_DX2_CODE, c1Transaction.GetData(_SecondLastLineNumber, COL_DX2_CODE));
                    c1Transaction.SetData(_LastLineNumber, COL_DX2_DESC, c1Transaction.GetData(_SecondLastLineNumber, COL_DX2_DESC));
                    c1Transaction.SetCellCheck(_LastLineNumber, COL_DX2_PTR, CheckEnum.Checked);
                }
                if (Convert.ToString(c1Transaction.GetData(_SecondLastLineNumber, COL_DX3_CODE)) != "")
                {
                    c1Transaction.SetData(_LastLineNumber, COL_DX3_CODE, c1Transaction.GetData(_SecondLastLineNumber, COL_DX3_CODE));
                    c1Transaction.SetData(_LastLineNumber, COL_DX3_DESC, c1Transaction.GetData(_SecondLastLineNumber, COL_DX3_DESC));
                    c1Transaction.SetCellCheck(_LastLineNumber, COL_DX3_PTR, CheckEnum.Checked);
                }
                if (Convert.ToString(c1Transaction.GetData(_SecondLastLineNumber, COL_DX4_CODE)) != "")
                {
                    c1Transaction.SetData(_LastLineNumber, COL_DX4_CODE, c1Transaction.GetData(_SecondLastLineNumber, COL_DX4_CODE));
                    c1Transaction.SetData(_LastLineNumber, COL_DX4_DESC, c1Transaction.GetData(_SecondLastLineNumber, COL_DX4_DESC));
                    c1Transaction.SetCellCheck(_LastLineNumber, COL_DX4_PTR, CheckEnum.Checked);
                }
                if (Convert.ToString(c1Transaction.GetData(_SecondLastLineNumber, COL_DX5_CODE)) != "")
                {
                    c1Transaction.SetData(_LastLineNumber, COL_DX5_CODE, c1Transaction.GetData(_SecondLastLineNumber, COL_DX5_CODE));
                    c1Transaction.SetData(_LastLineNumber, COL_DX5_DESC, c1Transaction.GetData(_SecondLastLineNumber, COL_DX5_DESC));
                    c1Transaction.SetCellCheck(_LastLineNumber, COL_DX5_PTR, CheckEnum.Checked);
                }
                if (Convert.ToString(c1Transaction.GetData(_SecondLastLineNumber, COL_DX6_CODE)) != "")
                {
                    c1Transaction.SetData(_LastLineNumber, COL_DX6_CODE, c1Transaction.GetData(_SecondLastLineNumber, COL_DX6_CODE));
                    c1Transaction.SetData(_LastLineNumber, COL_DX6_DESC, c1Transaction.GetData(_SecondLastLineNumber, COL_DX6_DESC));
                    c1Transaction.SetCellCheck(_LastLineNumber, COL_DX6_PTR, CheckEnum.Checked);
                }
                if (Convert.ToString(c1Transaction.GetData(_SecondLastLineNumber, COL_DX7_CODE)) != "")
                {
                    c1Transaction.SetData(_LastLineNumber, COL_DX7_CODE, c1Transaction.GetData(_SecondLastLineNumber, COL_DX7_CODE));
                    c1Transaction.SetData(_LastLineNumber, COL_DX7_DESC, c1Transaction.GetData(_SecondLastLineNumber, COL_DX7_DESC));
                    c1Transaction.SetCellCheck(_LastLineNumber, COL_DX7_PTR, CheckEnum.Checked);
                }
                if (Convert.ToString(c1Transaction.GetData(_SecondLastLineNumber, COL_DX8_CODE)) != "")
                {
                    c1Transaction.SetData(_LastLineNumber, COL_DX8_CODE, c1Transaction.GetData(_SecondLastLineNumber, COL_DX8_CODE));
                    c1Transaction.SetData(_LastLineNumber, COL_DX8_DESC, c1Transaction.GetData(_SecondLastLineNumber, COL_DX8_DESC));
                    c1Transaction.SetCellCheck(_LastLineNumber, COL_DX8_PTR, CheckEnum.Checked);
                }

            }

            for (int i = 1; i <= c1Transaction.Rows.Count - 1; i++)
            {
                c1Transaction.SetData(i, COL_NO, i.ToString());
            }

            SetCurrencyCellValue(c1Transaction.Rows.Count - 1);
        }

        public void AddTransactionLine(ArrayList SelectedTreatment)
        {
            //This method is used from Missing Charges Report

            for (int i = 0; i < SelectedTreatment.Count; i++)
            {


                c1Transaction.Rows.Add();

                int rowIndex = c1Transaction.Rows.Count - 1;

                c1Transaction.SetData(rowIndex, COL_NO, Convert.ToString(rowIndex));

                GetDefaultTOSPOS();
                c1Transaction.SetData(rowIndex, COL_DATEFROM, DateTime.Now.ToShortDateString());

                //DOS To Implementation
                if (_showTilldateColumn == true)
                {
                    if (_showTillDateColumnUseNullDate == true)
                    {
                        c1Transaction.SetData(rowIndex, COL_DATETO, null);
                    }
                    else
                    {
                        c1Transaction.SetData(rowIndex, COL_DATETO, DateTime.Now.ToShortDateString());
                    }
                }
                else
                {
                    c1Transaction.SetData(rowIndex, COL_DATETO, DateTime.Now.ToShortDateString());
                }

                string _scPTCode = Convert.ToString(SelectedTreatment[i]);

                c1Transaction.SetData(rowIndex, COL_CPT_CODE, _scPTCode);
                c1Transaction.SetData(rowIndex, COL_CPT_DESC, _scPTCode);

                c1Transaction.SetData(rowIndex, COL_TOSCODE, _DefaultTOSCode);
                c1Transaction.SetData(rowIndex, COL_TOSDESC, _DefaultTOSDesc);
                c1Transaction.SetData(rowIndex, COL_POSCODE, _DefaultPOSCode);
                c1Transaction.SetData(rowIndex, COL_POSDESC, _DefaultPOSDesc);

                if (c1Transaction.Rows.Count > 2)
                {
                    if (Convert.ToString(c1Transaction.GetData(c1Transaction.Rows.Count - 2, COL_POSCODE)) != "")
                    {
                        c1Transaction.SetData(rowIndex, COL_POSCODE, Convert.ToString(c1Transaction.GetData(c1Transaction.Rows.Count - 2, COL_POSCODE)));
                        c1Transaction.SetData(rowIndex, COL_POSDESC, Convert.ToString(c1Transaction.GetData(c1Transaction.Rows.Count - 2, COL_POSDESC)));
                    }


                }

                if (_DefaultRenderingProviderID > 0)
                {
                    c1Transaction.SetData(rowIndex, COL_PROVIDER_ID, _DefaultRenderingProviderID);
                    c1Transaction.SetData(rowIndex, COL_PROVIDER, _DefaultRenderringProviderName);
                }
                c1Transaction.Select(rowIndex, COL_DATEFROM);

                //**..Code added on 20090511 by - Sagar Ghodke

                c1Transaction.SetCellCheck(rowIndex, COL_ISLABCPT, CheckEnum.Unchecked);
                c1Transaction.SetCellCheck(rowIndex, COL_ISEMG, CheckEnum.Unchecked);
                c1Transaction.SetData(rowIndex, COL_AUTHORIZATIONNO, "");
                c1Transaction.SetCellCheck(rowIndex, COL_SENTTOCLAIM, CheckEnum.Checked);

                //**..End Code add 20090511,Sagar Ghodke


                //Add the previous Line Modifiers to newly added line

                if (c1Transaction.Rows.Count > 2)
                {
                    int _LastLineNumber = rowIndex;
                    int _SecondLastLineNumber = _LastLineNumber - 1;

                    if (Convert.ToString(c1Transaction.GetData(_SecondLastLineNumber, COL_DX1_CODE)) != "")
                    {
                        c1Transaction.SetData(_LastLineNumber, COL_DX1_CODE, c1Transaction.GetData(_SecondLastLineNumber, COL_DX1_CODE));
                        c1Transaction.SetData(_LastLineNumber, COL_DX1_DESC, c1Transaction.GetData(_SecondLastLineNumber, COL_DX1_DESC));
                        c1Transaction.SetCellCheck(_LastLineNumber, COL_DX1_PTR, CheckEnum.Checked);
                    }
                    if (Convert.ToString(c1Transaction.GetData(_SecondLastLineNumber, COL_DX2_CODE)) != "")
                    {
                        c1Transaction.SetData(_LastLineNumber, COL_DX2_CODE, c1Transaction.GetData(_SecondLastLineNumber, COL_DX2_CODE));
                        c1Transaction.SetData(_LastLineNumber, COL_DX2_DESC, c1Transaction.GetData(_SecondLastLineNumber, COL_DX2_DESC));
                        c1Transaction.SetCellCheck(_LastLineNumber, COL_DX2_PTR, CheckEnum.Checked);
                    }
                    if (Convert.ToString(c1Transaction.GetData(_SecondLastLineNumber, COL_DX3_CODE)) != "")
                    {
                        c1Transaction.SetData(_LastLineNumber, COL_DX3_CODE, c1Transaction.GetData(_SecondLastLineNumber, COL_DX3_CODE));
                        c1Transaction.SetData(_LastLineNumber, COL_DX3_DESC, c1Transaction.GetData(_SecondLastLineNumber, COL_DX3_DESC));
                        c1Transaction.SetCellCheck(_LastLineNumber, COL_DX3_PTR, CheckEnum.Checked);
                    }
                    if (Convert.ToString(c1Transaction.GetData(_SecondLastLineNumber, COL_DX4_CODE)) != "")
                    {
                        c1Transaction.SetData(_LastLineNumber, COL_DX4_CODE, c1Transaction.GetData(_SecondLastLineNumber, COL_DX4_CODE));
                        c1Transaction.SetData(_LastLineNumber, COL_DX4_DESC, c1Transaction.GetData(_SecondLastLineNumber, COL_DX4_DESC));
                        c1Transaction.SetCellCheck(_LastLineNumber, COL_DX4_PTR, CheckEnum.Checked);
                    }
                    if (Convert.ToString(c1Transaction.GetData(_SecondLastLineNumber, COL_DX5_CODE)) != "")
                    {
                        c1Transaction.SetData(_LastLineNumber, COL_DX5_CODE, c1Transaction.GetData(_SecondLastLineNumber, COL_DX5_CODE));
                        c1Transaction.SetData(_LastLineNumber, COL_DX5_DESC, c1Transaction.GetData(_SecondLastLineNumber, COL_DX5_DESC));
                        c1Transaction.SetCellCheck(_LastLineNumber, COL_DX5_PTR, CheckEnum.Checked);
                    }
                    if (Convert.ToString(c1Transaction.GetData(_SecondLastLineNumber, COL_DX6_CODE)) != "")
                    {
                        c1Transaction.SetData(_LastLineNumber, COL_DX6_CODE, c1Transaction.GetData(_SecondLastLineNumber, COL_DX6_CODE));
                        c1Transaction.SetData(_LastLineNumber, COL_DX6_DESC, c1Transaction.GetData(_SecondLastLineNumber, COL_DX6_DESC));
                        c1Transaction.SetCellCheck(_LastLineNumber, COL_DX6_PTR, CheckEnum.Checked);
                    }
                    if (Convert.ToString(c1Transaction.GetData(_SecondLastLineNumber, COL_DX7_CODE)) != "")
                    {
                        c1Transaction.SetData(_LastLineNumber, COL_DX7_CODE, c1Transaction.GetData(_SecondLastLineNumber, COL_DX7_CODE));
                        c1Transaction.SetData(_LastLineNumber, COL_DX7_DESC, c1Transaction.GetData(_SecondLastLineNumber, COL_DX7_DESC));
                        c1Transaction.SetCellCheck(_LastLineNumber, COL_DX7_PTR, CheckEnum.Checked);
                    }
                    if (Convert.ToString(c1Transaction.GetData(_SecondLastLineNumber, COL_DX8_CODE)) != "")
                    {
                        c1Transaction.SetData(_LastLineNumber, COL_DX8_CODE, c1Transaction.GetData(_SecondLastLineNumber, COL_DX8_CODE));
                        c1Transaction.SetData(_LastLineNumber, COL_DX8_DESC, c1Transaction.GetData(_SecondLastLineNumber, COL_DX8_DESC));
                        c1Transaction.SetCellCheck(_LastLineNumber, COL_DX8_PTR, CheckEnum.Checked);
                    }

                }

                for (int j = 1; j <= rowIndex; j++)
                {
                    c1Transaction.SetData(j, COL_NO, j.ToString());
                }

                SetCurrencyCellValue(rowIndex);
            }
        }

        public void InsertTransactionLine(Int32 LineWhere)
        {
            c1Transaction.Rows.Insert(LineWhere);
            for (int i = 1; i <= c1Transaction.Rows.Count - 1; i++)
            {
                c1Transaction.SetData(i, COL_NO, i.ToString());
            }
        }

        public void DeleteTransactionLine(Int32 LineNo)
        {
            if (LineNo > 0)
            {
                if (c1Transaction.Rows.Count > LineNo)
                {
                    IsInternalControlActive = false;//CloseInternalControl();
                    c1Transaction.Rows.Remove(LineNo);
                    c1Transaction.Rows[0].AllowEditing = false;
                    c1Total.SetData(0, COL_CHARGES, GetTotalCharges());
                    c1Total.SetData(0, COL_ALLOWED, GetTotalAllowed());
                    c1Total.SetData(0, COL_TOTAL, GetGrandTotal());
                }

                for (int i = 1; i <= c1Transaction.Rows.Count - 1; i++)
                {
                    c1Transaction.SetData(i, COL_NO, i.ToString());
                }
            }
        }

        public void DeleteTransactionLine(string sCPTCode, Int32 LineNo, Boolean _IsICD9Driven)
        {
            if (LineNo > 0)
            {
                if (c1Transaction.Rows.Count > 0)
                {
                    IsInternalControlActive = false;

                    if (_IsICD9Driven)
                    {
                        int iRow = c1Transaction.FindRow(sCPTCode.ToString(), 1, COL_CPT_CODE, false, true, false);
                        if (iRow > 0)
                        {
                            c1Transaction.Rows.Remove(iRow);
                        }
                    }
                    else
                    {
                        int iRow = c1Transaction.FindRow(LineNo.ToString(), 1, COL_EMRTREATMENTLINENO, false, true, false);
                        if (iRow > 0)
                        {
                            c1Transaction.Rows.Remove(iRow);
                        }
                    }


                    c1Transaction.Rows[0].AllowEditing = false;
                    c1Total.SetData(0, COL_CHARGES, GetTotalCharges());
                    c1Total.SetData(0, COL_ALLOWED, GetTotalAllowed());
                    c1Total.SetData(0, COL_TOTAL, GetGrandTotal());
                }

                for (int i = 1; i <= c1Transaction.Rows.Count - 1; i++)
                {
                    c1Transaction.SetData(i, COL_NO, i.ToString());
                }
            }
        }

        public void DeleteBlankCPTLines()
        {
            if (c1Transaction.Rows.Count > 0)
            {
                IsInternalControlActive = false;
                int iRow = c1Transaction.FindRow(null, 1, COL_CPT_CODE, false);
                if (iRow > 0)
                {
                    c1Transaction.Rows.Remove(iRow);

                    c1Transaction.Rows[0].AllowEditing = false;
                    c1Total.SetData(0, COL_CHARGES, GetTotalCharges());
                    c1Total.SetData(0, COL_ALLOWED, GetTotalAllowed());
                    c1Total.SetData(0, COL_TOTAL, GetGrandTotal());
                }
            }

            for (int i = 1; i <= c1Transaction.Rows.Count - 1; i++)
            {
                c1Transaction.SetData(i, COL_NO, i.ToString());
            }

        }

        public bool ValidateTransaction(Transaction oTransaction)
        {
            DateTime dtDateFrom;
            //decimal _lineallowed = 0;
            //decimal _linetotalChargeAmt = 0;
            try
            {
                c1Transaction.FinishEditing();
                DataTable dtCPTInfo = GetCPTEPSDTPromptInfo();

                _dtCPTActivationDates = new DataTable();
                _dtCPTActivationDates.Columns.Add("sCPTCode");
                _dtCPTActivationDates.Columns.Add("nFromDate");
                _dtCPTActivationDates.Columns.Add("nToDate");
                for (int i = 1; i <= c1Transaction.Rows.Count - 1; i++)
                {
                    _dtCPTActivationDates.Rows.Add();

                    _dtCPTActivationDates.Rows[i - 1]["sCPTCode"] = Convert.ToString(c1Transaction.GetData(i, COL_CPT_CODE));
                    _dtCPTActivationDates.Rows[i - 1]["nFromDate"] = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1Transaction.GetData(i, COL_DATEFROM)));

                    if (_showTilldateColumn == true)
                    {

                        if (c1Transaction.GetData(i, COL_DATETO) != null)
                        {
                            _dtCPTActivationDates.Rows[i - 1]["nToDate"] = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1Transaction.GetData(i, COL_DATETO)));
                        }
                        else
                        {
                            _dtCPTActivationDates.Rows[i - 1]["nToDate"] = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1Transaction.GetData(i, COL_DATEFROM)));

                        }

                    }
                    else
                    {
                        _dtCPTActivationDates.Rows[i - 1]["nToDate"] = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1Transaction.GetData(i, COL_DATEFROM)));
                    }


                    dtDateFrom = Convert.ToDateTime(c1Transaction.GetData(i, COL_DATEFROM));


                    #region " DOS Validation "

                    ////if (oTransaction.Lines.Count >= i)
                    ////{
                    ////    if (dtDateFrom != oTransaction.Lines[i - 1].DateServiceFrom)
                    ////    {
                    ////        if (MaxPaymentDate != null && MaxPaymentDate != "")
                    ////        {
                    ////            if (dtDateFrom > Convert.ToDateTime(MaxPaymentDate))
                    ////            {
                    ////                if (MessageBox.Show("Charge Date of Service [" + dtDateFrom.ToShortDateString() + "] for line number " + i.ToString() + " is after Payment Close Date [" + MaxPaymentDate + "]" + Environment.NewLine + "Continue?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    ////                {

                    ////                    c1Transaction.Focus();
                    ////                    c1Transaction.Select(i, COL_DATEFROM);
                    ////                    return false;
                    ////                }
                    ////            }
                    ////        }

                    ////        if (ColseDate != "" && ColseDate != null)
                    ////        {
                    ////            DateTime dt = Convert.ToDateTime(ColseDate);
                    ////            if (dt.Date < dtDateFrom.Date)
                    ////            {
                    ////                if (MessageBox.Show("Charge Date of Service [" + dtDateFrom.ToShortDateString() + "] for line number " + i.ToString() + " is after Charge Close Date [" + dt.ToShortDateString() + "]." + Environment.NewLine + "Continue?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    ////                {
                    ////                    c1Transaction.SetData(i, COL_DATEFROM, dt);
                    ////                    c1Transaction.Focus();
                    ////                    c1Transaction.Select(i, COL_DATEFROM);
                    ////                    return false;

                    ////                }
                    ////            }
                    ////        }

                    ////        if (dtDateFrom.Date > DateTime.Now.Date)
                    ////        {
                    ////            if (MessageBox.Show("Charge Date of Service [" + dtDateFrom.ToShortDateString() + "] for line number " + i.ToString() + " is in the future." + Environment.NewLine + "Continue?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    ////            {
                    ////                c1Transaction.SetData(i, COL_DATEFROM, DateTime.Now);
                    ////                c1Transaction.Focus();
                    ////                c1Transaction.Select(i, COL_DATEFROM);
                    ////                return false;
                    ////            }
                    ////        }
                    ////    }
                    ////    else if (ColseDate != "" && ColseDate != null)
                    ////    {
                    ////        if (oTransaction.TransactionDate != gloDateMaster.gloDate.DateAsNumber(ColseDate))
                    ////        {
                    ////            DateTime dt = Convert.ToDateTime(ColseDate);
                    ////            if (dt.Date < dtDateFrom.Date)
                    ////            {
                    ////                if (MessageBox.Show("Charge Date of Service [" + dtDateFrom.ToShortDateString() + "] for line number " + i.ToString() + " is after Charge Close Date [" + dt.ToShortDateString() + "]." + Environment.NewLine + "Continue?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    ////                {
                    ////                    c1Transaction.SetData(i, COL_DATEFROM, dt);
                    ////                    c1Transaction.Focus();
                    ////                    c1Transaction.Select(i, COL_DATEFROM);
                    ////                    return false;

                    ////                }
                    ////            }
                    ////        }
                    ////    }
                    ////}
                    ////else
                    ////{
                    ////    if (MaxPaymentDate != null && MaxPaymentDate != "")
                    ////    {
                    ////        if (dtDateFrom > Convert.ToDateTime(MaxPaymentDate))
                    ////        {
                    ////            if (MessageBox.Show("Charge Date of Service [" + dtDateFrom.ToShortDateString() + "] for line number " + i.ToString() + " is after Payment Close Date [" + MaxPaymentDate + "]" + Environment.NewLine + "Continue?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    ////            {

                    ////                c1Transaction.Focus();
                    ////                c1Transaction.Select(i, COL_DATEFROM);
                    ////                return false;
                    ////            }
                    ////        }
                    ////    }

                    ////    if (ColseDate != "" && ColseDate != null)
                    ////    {
                    ////        DateTime dt = Convert.ToDateTime(ColseDate);
                    ////        if (dt.Date < dtDateFrom.Date)
                    ////        {
                    ////            if (MessageBox.Show("Charge Date of Service [" + dtDateFrom.ToShortDateString() + "] for line number " + i.ToString() + " is after Charge Close Date [" + dt.ToShortDateString() + "]." + Environment.NewLine + "Continue?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    ////            {
                    ////                c1Transaction.SetData(i, COL_DATEFROM, dt);
                    ////                c1Transaction.Focus();
                    ////                c1Transaction.Select(i, COL_DATEFROM);
                    ////                return false;

                    ////            }
                    ////        }
                    ////    }

                    ////    if (dtDateFrom.Date > DateTime.Now.Date)
                    ////    {
                    ////        if (MessageBox.Show("Charge Date of Service [" + dtDateFrom.ToShortDateString() + "] for line number " + i.ToString() + " is in the future." + Environment.NewLine + "Continue?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    ////        {
                    ////            c1Transaction.SetData(i, COL_DATEFROM, DateTime.Now);
                    ////            c1Transaction.Focus();
                    ////            c1Transaction.Select(i, COL_DATEFROM);
                    ////            return false;
                    ////        }
                    ////    }
                    ////} 

                    #endregion


                    if (DateTime.TryParse(Convert.ToString(c1Transaction.GetData(i, COL_DATEFROM)), out dtDateFrom) == false)
                    {
                        MessageBox.Show("Please select valid date of service for line number " + i.ToString() + ".  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        c1Transaction.Focus();
                        c1Transaction.Select(i, COL_DATEFROM);
                        return false;
                    }
                    else if (dtDateFrom.Date.ToShortDateString() == "")
                    {
                        //dtDateFrom = Convert.ToDateTime(c1Transaction.GetData(i, COL_DATEFROM));
                        MessageBox.Show("Please select date of service for line number " + i.ToString() + ".  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        c1Transaction.Focus();
                        c1Transaction.Select(i, COL_DATEFROM);
                        return false;
                    }
                    //Do not allow to set DOS greater than current date************************
                    //else if (dtDateFrom.Date > DateTime.Now.Date) 
                    //{
                    //    //    MessageBox.Show("Date of service for line number " + i.ToString() + " cannot be greater than today's date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    //    c1Transaction.Focus();
                    //    //    c1Transaction.Select(i, COL_DATEFROM);
                    //    //    return false;
                    //}
                    //************

                    else if (Convert.ToString(c1Transaction.GetData(i, COL_POSCODE)).Trim() == "")
                    {
                        MessageBox.Show("Please enter a place of service code for line number " + i.ToString() + ".  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        c1Transaction.Focus();
                        c1Transaction.Select(i, COL_POSCODE, true);
                        return false;
                    }
                    else if (Convert.ToString(c1Transaction.GetData(i, COL_TOSCODE)).Trim() == "")
                    {
                        MessageBox.Show("Please enter a type of service code for line number " + i.ToString() + ".  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        c1Transaction.Focus();
                        c1Transaction.Select(i, COL_TOSCODE, true);
                        return false;
                    }
                    else if (Convert.ToString(c1Transaction.GetData(i, COL_CPT_CODE)) == "")
                    {
                        MessageBox.Show("Please enter a CPT code for line number " + i.ToString() + ".  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        c1Transaction.Focus();
                        c1Transaction.Select(i, COL_CPT_BTN);
                        return false;
                    }

                    else if (Convert.ToString(c1Transaction.GetData(i, COL_DX1_CODE)) == "")
                    {
                        MessageBox.Show("Please enter diagnosis for line number " + i.ToString() + ".  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        c1Transaction.Focus();
                        c1Transaction.Select(i, COL_DX1_BTN);
                        return false;
                    }
                    else if (c1Transaction.GetData(i, COL_LINEPRIMARY_DXCODE) == null || Convert.ToString(c1Transaction.GetData(i, COL_LINEPRIMARY_DXCODE)).Trim() == "")
                    {
                        MessageBox.Show("Please select the primary diagnosis for line " + i.ToString() + " ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        c1Transaction.Focus();
                        c1Transaction.Select(i, COL_DATEFROM);
                        return false;
                    }
                    else if (Convert.ToDecimal(c1Transaction.GetData(i, COL_CHARGES)) < 0)
                    {
                        MessageBox.Show("Please enter valid charges for line number " + i.ToString() + ".  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        c1Transaction.Focus();
                        c1Transaction.Select(i, COL_CHARGES);
                        return false;
                    }
                    //..Code changes done on 20100803 by Sagar Ghodke
                    //..BUG# : 3066 UC# : 5060.125
                    else if (Convert.ToDecimal(c1Transaction.GetData(i, COL_UNIT)) > 999999.9999M)
                    {
                        string _messageStr = " Units entered for line number " + i.ToString() + " exceeds the max limit.";
                        MessageBox.Show(_messageStr, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        c1Transaction.Select(i, COL_UNIT, true);
                        return false;
                    }
                    //..End code comment on 20100803 by Sagar Ghodke
                    else if (Convert.ToDecimal(c1Transaction.GetData(i, COL_CHARGES)) == 0)
                    {
                        DialogResult DlgRst = DialogResult.None;
                        string _messageStr = " Charges are not entered for line number " + i.ToString() + ".  Do you want to make zero charge entry? ";
                        DlgRst = MessageBox.Show(_messageStr, _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (DlgRst == DialogResult.No)
                        {
                            //MessageBox.Show("Please enter charges.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            c1Transaction.Focus();
                            c1Transaction.Select(i, COL_CHARGES);
                            return false;
                        }
                    }
                    else if (_showTilldateColumn && c1Transaction.GetData(i, COL_DATETO) != null && Convert.ToDateTime(c1Transaction.GetData(i, COL_DATEFROM)) > Convert.ToDateTime(c1Transaction.GetData(i, COL_DATETO)))
                    {
                        if (Convert.ToDateTime(c1Transaction.GetData(i, COL_DATEFROM)) > Convert.ToDateTime(c1Transaction.GetData(i, COL_DATETO)))
                        {
                            MessageBox.Show("From DOS cannot be greater than To DOS for line number " + i.ToString() + ".  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            c1Transaction.Focus();
                            c1Transaction.Select(i, COL_DATETO);
                            return false;
                        }
                    }
                    if (Convert.ToString(c1Transaction.GetData(i, COL_CPT_CODE)) != "")
                    {
                        if (IsCPTDrugChecked(Convert.ToString(c1Transaction.GetData(i, COL_CPT_CODE))))
                        {
                            if (Convert.ToString(c1Transaction.GetData(i, COL_NDCCODE)) == "")
                            {
                                //DialogResult DlgRst = DialogResult.None;
                                //string _messageStr = " NDC Code is not entered for line number " + i.ToString() + ".  Do you want to Continue? ";
                                //DlgRst = MessageBox.Show(_messageStr, _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                //if (DlgRst == DialogResult.No)
                                //{
                                //    //MessageBox.Show("Please enter charges.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //    c1Transaction.Focus();
                                //    c1Transaction.Select(i, COL_CPT_CODE);
                                //    return false;
                                //}
                                c1Transaction.Focus();
                                c1Transaction.Select(i, COL_CPT_CODE);
                                DialogResult DlgRst = DialogResult.None;
                                //string _messageStr = " NDC Code is not entered for line number " + i.ToString() + ".  Do you want to Continue? ";
                                string _messageStr = string.Format("Missing NDC:{0}CPT code \"{1}\" requires NDC for billing. Do you want to Continue?{0}{0}Yes: Save Claim{0} No: Enter NDC details for CPT Code.", Environment.NewLine, Convert.ToString(c1Transaction.GetData(i, COL_CPT_CODE)));
                                DlgRst = MessageBox.Show(_messageStr, _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                                if (DlgRst == DialogResult.No)
                                {
                                    string sCPTCode = Convert.ToString(c1Transaction.GetData(i, COL_CPT_CODE));
                                    string sCPTDesc = Convert.ToString(c1Transaction.GetData(i, COL_CPT_DESC));
                                    TrnCtrlColValChangeEventArg e2 = null;
                                    e2 = new TrnCtrlColValChangeEventArg();
                                    e2.code = sCPTCode;
                                    e2.description = sCPTDesc;
                                    e2.oType = TransactionLineColumnType.CPT;
                                    e2.isdeleted = false;
                                    onInsCPTDxMod_Changed(null, null, e2);
                                    e2 = null;
                                    //MessageBox.Show("Please enter charges.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return false;
                                }
                            }
                        }

                        var result = (from dt in dtCPTInfo.AsEnumerable()
                                      where dt.Field<String>("sCPTCode") == Convert.ToString(c1Transaction.GetData(i, COL_CPT_CODE))
                                      select dt.Field<Boolean>("bIsPromptforEpsdtFamPlan")).FirstOrDefault();

                        if (result && IsPlanOrAdminEPSDTEnabled)
                        {
                            if (!Convert.ToBoolean(c1Transaction.GetData(i, COL_SERVICESCREENING))
                                && !Convert.ToBoolean(c1Transaction.GetData(i, COL_SERVICERESULTSCREENING))
                                && !Convert.ToBoolean(c1Transaction.GetData(i, COL_FAMILYPLANNINGINDICATOR)))
                            {
                                DialogResult DlgRst = DialogResult.None;
                                string _messageStr = "EPSDT/Family planning information is not entered for line number " + i.ToString() + "." + Environment.NewLine + "Do you want to Continue? ";
                                DlgRst = MessageBox.Show(_messageStr, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                if (DlgRst == DialogResult.No)
                                {
                                    c1Transaction.Focus();
                                    c1Transaction.Select(i, COL_CPT_CODE);
                                    return false;
                                }
                            }
                        }
                    }

                }

                string Msg = "";
                Msg = gloGlobal.gloPMGlobal.getCPTDeativatedCPT(_dtCPTActivationDates);
                if (Msg != "")
                {
                    if (MessageBox.Show(Msg, _messageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
                    {
                        return false;
                    }

                }

                if (_nCurrentContactID > 0)
                {
                    if (ValidateICD10DOS() == false)
                        return false;
                }
                return true;
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

        private DataTable GetCPTEPSDTPromptInfo()
        {
            gloDatabaseLayer.DBLayer ODB = null;
            string _sqlQuery = "";
            DataTable _dt = null;

            try
            {
                string sCPT = string.Empty;
                _dt = GetCPTList();
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    for (int iCount = 0; iCount <= _dt.Rows.Count - 1; iCount++)
                    {
                        if (sCPT == string.Empty)
                        {
                            sCPT = "'" + Convert.ToString(_dt.Rows[iCount]["sCPT"]).Replace("'", "''") + "'";
                        }
                        else
                        {
                            sCPT += "," + "'" + Convert.ToString(_dt.Rows[iCount]["sCPT"]).Replace("'", "''") + "'";
                        }
                    }
                }

                _sqlQuery = "SELECT UPPER(sCPTCode) AS sCPTCode, ISNULL(bIsPromptforEpsdtFamPlan,'FALSE') AS bIsPromptforEpsdtFamPlan FROM CPT_MST WITH(NOLOCK) WHERE UPPER(sCPTCode) IN (" + sCPT.Trim().ToUpper() + ")";
                ODB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                ODB.Connect(false);
                ODB.Retrive_Query(_sqlQuery, out _dt);
                ODB.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ODB != null) { ODB.Disconnect(); ODB.Dispose(); }
            }
            return _dt;
        }

        public bool ValidateDOS(TransactionLines objCurrTransLine, Transaction objInitialTrans)
        {
            try
            {

                #region " DOS Validation "

                if (objCurrTransLine.Count > 0)
                {
                    for (int iCount = 0; iCount <= objCurrTransLine.Count - 1; iCount++)
                    {
                        if (objCurrTransLine[iCount].TransactionId == 0)
                        {
                            if (MaxPaymentDate != null && MaxPaymentDate != "")
                            {
                                if (objCurrTransLine[iCount].DateServiceFrom > Convert.ToDateTime(MaxPaymentDate))
                                {
                                    if (MessageBox.Show("Charge Date of Service [" + objCurrTransLine[iCount].DateServiceFrom.ToShortDateString() + "] for line number " + objCurrTransLine[iCount].TransactionLineId + " is after Payment Close Date [" + MaxPaymentDate + "]" + Environment.NewLine + "Continue?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                    {
                                        c1Transaction.Focus();
                                        c1Transaction.Select(Convert.ToInt16(objCurrTransLine[iCount].TransactionLineId), COL_DATEFROM);
                                        return false;
                                    }
                                }
                            }

                            if (ColseDate != "" && ColseDate != null)
                            {
                                DateTime dt = Convert.ToDateTime(ColseDate);
                                if (dt.Date < objCurrTransLine[iCount].DateServiceFrom.Date)
                                {
                                    if (MessageBox.Show("Charge Date of Service [" + objCurrTransLine[iCount].DateServiceFrom.ToShortDateString() + "] for line number " + objCurrTransLine[iCount].TransactionLineId + " is after Charge Close Date [" + dt.ToShortDateString() + "]." + Environment.NewLine + "Continue?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                    {
                                        //c1Transaction.SetData(Convert.ToInt16(objCurrTransLine[iCount].TransactionLineId), COL_DATEFROM, dt);
                                        c1Transaction.Focus();
                                        c1Transaction.Select(Convert.ToInt16(objCurrTransLine[iCount].TransactionLineId), COL_DATEFROM);
                                        return false;

                                    }
                                }
                            }

                            if (objCurrTransLine[iCount].DateServiceFrom.Date > DateTime.Now.Date)
                            {
                                if (MessageBox.Show("Charge Date of Service [" + objCurrTransLine[iCount].DateServiceFrom.ToShortDateString() + "] for line number " + objCurrTransLine[iCount].TransactionLineId + " is in the future." + Environment.NewLine + "Continue?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                {
                                    //c1Transaction.SetData(Convert.ToInt16(objCurrTransLine[iCount].TransactionLineId), COL_DATEFROM, DateTime.Now);
                                    c1Transaction.Focus();
                                    c1Transaction.Select(Convert.ToInt16(objCurrTransLine[iCount].TransactionLineId), COL_DATEFROM);
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            for (int Count = 0; Count <= objInitialTrans.Lines.Count - 1; Count++)
                            {
                                if (objInitialTrans.Lines[Count].TransactionDetailID == objCurrTransLine[iCount].TransactionDetailID)
                                {
                                    if (objInitialTrans.Lines[Count].DateServiceFrom != objCurrTransLine[iCount].DateServiceFrom)
                                    {

                                        if (MaxPaymentDate != null && MaxPaymentDate != "")
                                        {
                                            if (objCurrTransLine[iCount].DateServiceFrom > Convert.ToDateTime(MaxPaymentDate))
                                            {
                                                if (MessageBox.Show("Charge Date of Service [" + objCurrTransLine[iCount].DateServiceFrom.ToString("MM/dd/yyyy") + "] for line number " + objCurrTransLine[iCount].TransactionLineId + " is after Payment Close Date [" + MaxPaymentDate + "]" + Environment.NewLine + "Continue?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                                {
                                                    c1Transaction.Focus();
                                                    c1Transaction.Select(Convert.ToInt16(objCurrTransLine[iCount].TransactionLineId), COL_DATEFROM);
                                                    return false;
                                                }
                                            }
                                        }

                                        if (ColseDate != "" && ColseDate != null)
                                        {
                                            DateTime dt = Convert.ToDateTime(ColseDate);
                                            if (dt.Date < objCurrTransLine[iCount].DateServiceFrom.Date)
                                            {
                                                if (MessageBox.Show("Charge Date of Service [" + objCurrTransLine[iCount].DateServiceFrom.ToString("MM/dd/yyyy") + "] for line number " + objCurrTransLine[iCount].TransactionLineId + " is after Charge Close Date [" + dt.ToString("MM/dd/yyyy") + "]." + Environment.NewLine + "Continue?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                                {
                                                    //c1Transaction.SetData(Convert.ToInt16(objCurrTransLine[iCount].TransactionLineId), COL_DATEFROM, dt);
                                                    c1Transaction.Focus();
                                                    c1Transaction.Select(Convert.ToInt16(objCurrTransLine[iCount].TransactionLineId), COL_DATEFROM);
                                                    return false;

                                                }
                                            }
                                        }

                                        if (objCurrTransLine[iCount].DateServiceFrom.Date > DateTime.Now.Date)
                                        {
                                            if (MessageBox.Show("Charge Date of Service [" + objCurrTransLine[iCount].DateServiceFrom.ToString("MM/dd/yyyy") + "] for line number " + objCurrTransLine[iCount].TransactionLineId + " is in the future." + Environment.NewLine + "Continue?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                            {
                                                //c1Transaction.SetData(Convert.ToInt16(objCurrTransLine[iCount].TransactionLineId), COL_DATEFROM, DateTime.Now);
                                                c1Transaction.Focus();
                                                c1Transaction.Select(Convert.ToInt16(objCurrTransLine[iCount].TransactionLineId), COL_DATEFROM);
                                                return false;
                                            }
                                        }

                                    }
                                    else if (objInitialTrans.TransactionDate != gloDateMaster.gloDate.DateAsNumber(ColseDate))
                                    {
                                        DateTime dt = Convert.ToDateTime(ColseDate);
                                        if (dt.Date < objCurrTransLine[iCount].DateServiceFrom.Date)
                                        {
                                            if (MessageBox.Show("Charge Date of Service [" + objCurrTransLine[iCount].DateServiceFrom.ToString("MM/dd/yyyy") + "] for line number " + objCurrTransLine[iCount].TransactionLineId + " is after Charge Close Date [" + dt.ToString("MM/dd/yyyy") + "]." + Environment.NewLine + "Continue?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                            {
                                                //c1Transaction.SetData(Convert.ToInt16(objCurrTransLine[iCount].TransactionLineId), COL_DATEFROM, dt);
                                                c1Transaction.Focus();
                                                c1Transaction.Select(Convert.ToInt16(objCurrTransLine[iCount].TransactionLineId), COL_DATEFROM);
                                                return false;

                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                #endregion

                return true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
        }

        public bool ValidateTransaction()
        {
            DateTime dtDateFrom;
            try
            {
                c1Transaction.FinishEditing();
                DataTable dtCPTInfo = GetCPTEPSDTPromptInfo();
                // Code Commented on 05-09-2014 for alowing single line for self claim
                //int SelfClaimCount = 1;    //Code Added for self option selection Limit > Lines  -Sameer 11-15-2013
                for (int i = 1; i <= c1Transaction.Rows.Count - 1; i++)
                {
                    dtDateFrom = Convert.ToDateTime(c1Transaction.GetData(i, COL_DATEFROM));


                    if (MaxPaymentDate != null && MaxPaymentDate != "")
                    {
                        if (dtDateFrom > Convert.ToDateTime(MaxPaymentDate))
                        {
                            if (MessageBox.Show("Charge Date of Service [" + dtDateFrom.ToShortDateString() + "] for line number " + i.ToString() + " is after Payment Close Date [" + MaxPaymentDate + "]" + Environment.NewLine + "Continue?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            {

                                c1Transaction.Focus();
                                c1Transaction.Select(i, COL_DATEFROM);
                                return false;
                            }
                        }
                    }

                    if (ColseDate != "" && ColseDate != null)
                    {
                        DateTime dt = Convert.ToDateTime(ColseDate);
                        if (dt.Date < dtDateFrom.Date)
                        {
                            if (MessageBox.Show("Charge Date of Service [" + dtDateFrom.ToShortDateString() + "] for line number " + i.ToString() + " is after Charge Close Date [" + dt.ToShortDateString() + "]." + Environment.NewLine + "Continue?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            {
                                //c1Transaction.SetData(i, COL_DATEFROM, dt);
                                c1Transaction.Focus();
                                c1Transaction.Select(i, COL_DATEFROM);
                                return false;

                            }
                        }
                    }

                    if (dtDateFrom.Date > DateTime.Now.Date)
                    {
                        if (MessageBox.Show("Charge Date of Service [" + dtDateFrom.ToShortDateString() + "] for line number " + i.ToString() + " is in the future." + Environment.NewLine + "Continue?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        {
                            //c1Transaction.SetData(i, COL_DATEFROM, DateTime.Now);
                            c1Transaction.Focus();
                            c1Transaction.Select(i, COL_DATEFROM);
                            return false;
                        }
                    }



                    if (DateTime.TryParse(Convert.ToString(c1Transaction.GetData(i, COL_DATEFROM)), out dtDateFrom) == false)
                    {
                        MessageBox.Show("Please select valid date of service for line number " + i.ToString() + ".  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        c1Transaction.Focus();
                        c1Transaction.Select(i, COL_DATEFROM);
                        return false;
                    }
                    else if (dtDateFrom.Date.ToShortDateString() == "")
                    {
                        //dtDateFrom = Convert.ToDateTime(c1Transaction.GetData(i, COL_DATEFROM));
                        MessageBox.Show("Please select date of service for line number " + i.ToString() + ".  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        c1Transaction.Focus();
                        c1Transaction.Select(i, COL_DATEFROM);
                        return false;
                    }
                    else if (_showTilldateColumn && c1Transaction.GetData(i, COL_DATETO) != null && Convert.ToDateTime(c1Transaction.GetData(i, COL_DATEFROM)) > Convert.ToDateTime(c1Transaction.GetData(i, COL_DATETO)))
                    {
                        if (Convert.ToDateTime(c1Transaction.GetData(i, COL_DATEFROM)) > Convert.ToDateTime(c1Transaction.GetData(i, COL_DATETO)))
                        {
                            MessageBox.Show("From DOS cannot be greater than To DOS for line number " + i.ToString() + ".  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            c1Transaction.Focus();
                            c1Transaction.Select(i, COL_DATETO);
                            return false;
                        }
                    }
                    //Do not allow to set DOS greater than current date************************
                    //else if (dtDateFrom.Date > DateTime.Now.Date) 
                    //{
                    //    //    MessageBox.Show("Date of service for line number " + i.ToString() + " cannot be greater than today's date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    //    c1Transaction.Focus();
                    //    //    c1Transaction.Select(i, COL_DATEFROM);
                    //    //    return false;
                    //}
                    //************

                    else if (Convert.ToString(c1Transaction.GetData(i, COL_POSCODE)).Trim() == "")
                    {
                        MessageBox.Show("Please enter a place of service code for line number " + i.ToString() + ".  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        c1Transaction.Focus();
                        c1Transaction.Select(i, COL_POSCODE, true);
                        return false;
                    }
                    else if (Convert.ToString(c1Transaction.GetData(i, COL_TOSCODE)).Trim() == "")
                    {
                        MessageBox.Show("Please enter a type of service code for line number " + i.ToString() + ".  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        c1Transaction.Focus();
                        c1Transaction.Select(i, COL_TOSCODE, true);
                        return false;
                    }
                    else if (Convert.ToString(c1Transaction.GetData(i, COL_CPT_CODE)) == "")
                    {
                        MessageBox.Show("Please enter a CPT code for line number " + i.ToString() + ".  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        c1Transaction.Focus();
                        //c1Transaction.Select(i, COL_CPT_BTN);
                        c1Transaction.Select(i, COL_CPT_CODE);
                        return false;
                    }

                    else if (Convert.ToString(c1Transaction.GetData(i, COL_DX1_CODE)) == "")
                    {
                        MessageBox.Show("Please enter diagnosis for line number " + i.ToString() + ".  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        c1Transaction.Focus();
                        //c1Transaction.Select(i, COL_DX1_BTN);
                        c1Transaction.Select(i, COL_DX1_CODE);
                        return false;
                    }

                    else if (c1Transaction.GetData(i, COL_LINEPRIMARY_DXCODE) == null || Convert.ToString(c1Transaction.GetData(i, COL_LINEPRIMARY_DXCODE)).Trim() == "")
                    {
                        MessageBox.Show("Please select the primary diagnosis for line " + i.ToString() + " ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        c1Transaction.Focus();
                        c1Transaction.Select(i, COL_DATEFROM);
                        return false;
                    }
                    else if (Convert.ToDecimal(c1Transaction.GetData(i, COL_CHARGES)) < 0)
                    {
                        MessageBox.Show("Please enter valid charges for line number " + i.ToString() + ".  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        c1Transaction.Focus();
                        c1Transaction.Select(i, COL_CHARGES);
                        return false;
                    }
                    //..Code changes done on 20100803 by Sagar Ghodke
                    //..BUG# : 3066 UC# : 5060.125
                    else if (Convert.ToDecimal(c1Transaction.GetData(i, COL_UNIT)) > 999999.9999M)
                    {
                        string _messageStr = " Units entered for line number " + i.ToString() + " exceeds the max limit.";
                        MessageBox.Show(_messageStr, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        c1Transaction.Select(i, COL_UNIT, true);
                        return false;
                    }
                    else if (Convert.ToDecimal(c1Transaction.GetData(i, COL_CHARGES)) == 0)
                    {
                        DialogResult DlgRst = DialogResult.None;
                        string _messageStr = " Charges are not entered for line number " + i.ToString() + ".  Do you want to make zero charge entry? ";
                        DlgRst = MessageBox.Show(_messageStr, _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (DlgRst == DialogResult.No)
                        {
                            //MessageBox.Show("Please enter charges.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            c1Transaction.Focus();
                            c1Transaction.Select(i, COL_CHARGES);
                            return false;
                        }
                    }


                    else if (Convert.ToString(c1Transaction.GetData(i, COL_PROVIDER_ID)) == string.Empty || Convert.ToString(c1Transaction.GetData(i, COL_PROVIDER)) == string.Empty)
                    {
                        string _messageStr = " Please select rendering provider for line number " + i.ToString() + ".";
                        MessageBox.Show(_messageStr, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        c1Transaction.Focus();
                        c1Transaction.Select(i, COL_PROVIDER, true);
                        return false;
                    }

                    if (Convert.ToString(c1Transaction.GetData(i, COL_CPT_CODE)) != "")
                    {
                        if (IsCPTDrugChecked(Convert.ToString(c1Transaction.GetData(i, COL_CPT_CODE)).Replace("'", "''")))
                        {
                            if (Convert.ToString(c1Transaction.GetData(i, COL_NDCCODE)) == "")
                            {
                                c1Transaction.Focus();
                                c1Transaction.Select(i, COL_CPT_CODE);
                                DialogResult DlgRst = DialogResult.None;
                                //string _messageStr = " NDC Code is not entered for line number " + i.ToString() + ".  Do you want to Continue? ";
                                string _messageStr = string.Format("Missing NDC:{0}CPT code \"{1}\" requires NDC for billing. Do you want to Continue?{0}{0}Yes: Save Claim{0} No: Enter NDC details for CPT Code.", Environment.NewLine, Convert.ToString(c1Transaction.GetData(i, COL_CPT_CODE)));
                                DlgRst = MessageBox.Show(_messageStr, _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                                if (DlgRst == DialogResult.No)
                                {
                                    string sCPTCode = Convert.ToString(c1Transaction.GetData(i, COL_CPT_CODE));
                                    string sCPTDesc = Convert.ToString(c1Transaction.GetData(i, COL_CPT_DESC));
                                    TrnCtrlColValChangeEventArg e2 = null;
                                    e2 = new TrnCtrlColValChangeEventArg();
                                    e2.code = sCPTCode;
                                    e2.description = sCPTDesc;
                                    e2.oType = TransactionLineColumnType.CPT;
                                    e2.isdeleted = false;
                                    onInsCPTDxMod_Changed(null, null, e2);
                                    e2 = null;
                                    //MessageBox.Show("Please enter charges.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return false;
                                }
                            }
                        }

                        var result = (from dt in dtCPTInfo.AsEnumerable()
                                      where dt.Field<String>("sCPTCode") == Convert.ToString(c1Transaction.GetData(i, COL_CPT_CODE)).Replace("'", "''")
                                      select dt.Field<Boolean>("bIsPromptforEpsdtFamPlan")).FirstOrDefault();

                        if (result && IsPlanOrAdminEPSDTEnabled)
                        {
                            if (!Convert.ToBoolean(c1Transaction.GetData(i, COL_SERVICESCREENING))
                                && !Convert.ToBoolean(c1Transaction.GetData(i, COL_SERVICERESULTSCREENING))
                                && !Convert.ToBoolean(c1Transaction.GetData(i, COL_FAMILYPLANNINGINDICATOR)))
                            {
                                DialogResult DlgRst = DialogResult.None;
                                string _messageStr = "EPSDT/Family planning information is not entered for line number " + i.ToString() + "." + Environment.NewLine + "Do you want to Continue? ";
                                DlgRst = MessageBox.Show(_messageStr, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                if (DlgRst == DialogResult.No)
                                {
                                    c1Transaction.Focus();
                                    c1Transaction.Select(i, COL_CPT_CODE);
                                    return false;
                                }
                            }
                        }

                    }



                    //Code Added for self option selection Limit > Lines  -Sameer 11-15-2013
                    // Code Commented on 05-09-2014 for alowing single line for self claim
                    //if (Convert.ToBoolean(  c1Transaction.GetData(i, COL_SELFCLAIM)) == true)
                    //{
                    //    SelfClaimCount++;
                    //}



                }
                //Code Added for self option selection Limit > Lines  -Sameer 11-15-2013
                // Code Commented on 05-09-2014 for alowing single line for self claim
                //if (SelfClaimCount == c1Transaction.Rows.Count && _nContactID>0)
                //{
                //    MessageBox.Show("Not all charges on claim can be marked for \"Self\". Claim must have atleast one charge responsible to insurance, else create self responsible claim.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //   return false;
                //}
                if (_nCurrentContactID > 0)
                {
                    if (ValidateICD10DOS() == false)
                        return false;
                }

                //Code Added for Duplicate claim warning 09-02-2014
                if (_bDuplicateClaimWarning == true) //Admin setting added for duplicate claim warning
                {
                    string[] _DosList = new string[c1Transaction.Rows.Count - 1];
                    for (int i = 1; i < c1Transaction.Rows.Count; i++)
                    {
                        _DosList[i - 1] = (gloDateMaster.gloDate.DateAsNumber(c1Transaction.GetData(i, COL_DATEFROM).ToString()).ToString());
                    }
                    string DosList = string.Join(",", _DosList);


                    string Msg = CheckduplicateClaims(PatientID, DosList);


                    if (Msg != "")
                    {
                        if (MessageBox.Show(Msg, _messageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
                        {
                            return false;
                        }

                    }
                }

                return true;
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

        public bool ValidateDx()
        {
            try
            {
                if (TreatmentType == ExternalChargesType.HL7InboundCharges) // To Validate only for Inbound Because it might contain Invalid Dx's
                {
                    for (int i = 1; i <= c1Transaction.Rows.Count - 1; i++)
                    {
                        if (Convert.ToString(c1Transaction.GetData(i, COL_DX1_CODE)) != "")
                        {
                            if (!gloCharges.IsValidDX(Convert.ToString(c1Transaction.GetData(i, COL_DX1_CODE))))
                            {
                                MessageBox.Show("Diagnosis code " + Convert.ToString(c1Transaction.GetData(i, COL_DX1_CODE)).Trim() + " for line number " + i.ToString() + " not present in system.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                c1Transaction.Focus();
                                c1Transaction.Select(i, COL_DX1_CODE);
                                return false;
                            }
                            else if (Convert.ToString(c1Transaction.GetData(i, COL_DX2_CODE)) != "")
                            {
                                if (!gloCharges.IsValidDX(Convert.ToString(c1Transaction.GetData(i, COL_DX2_CODE))))
                                {
                                    MessageBox.Show("Diagnosis code " + Convert.ToString(c1Transaction.GetData(i, COL_DX2_CODE)).Trim() + " for line number " + i.ToString() + " not present in system.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    c1Transaction.Focus();
                                    c1Transaction.Select(i, COL_DX2_CODE);
                                    return false;
                                }
                            }
                            else if (Convert.ToString(c1Transaction.GetData(i, COL_DX3_CODE)) != "")
                            {
                                if (!gloCharges.IsValidDX(Convert.ToString(c1Transaction.GetData(i, COL_DX3_CODE))))
                                {
                                    MessageBox.Show("Diagnosis code " + Convert.ToString(c1Transaction.GetData(i, COL_DX3_CODE)).Trim() + " for line number " + i.ToString() + " not present in system.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    c1Transaction.Focus();
                                    c1Transaction.Select(i, COL_DX3_CODE);
                                    return false;
                                }
                            }
                            else if (Convert.ToString(c1Transaction.GetData(i, COL_DX4_CODE)) != "")
                            {
                                if (!gloCharges.IsValidDX(Convert.ToString(c1Transaction.GetData(i, COL_DX4_CODE))))
                                {
                                    MessageBox.Show("Diagnosis code " + Convert.ToString(c1Transaction.GetData(i, COL_DX4_CODE)).Trim() + " for line number " + i.ToString() + " not present in system.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    c1Transaction.Focus();
                                    c1Transaction.Select(i, COL_DX4_CODE);
                                    return false;
                                }
                            }
                        }
                    }


                }

                for (int i = 1; i <= c1Transaction.Rows.Count - 1; i++)
                {
                    if (Convert.ToString(c1Transaction.GetData(i, COL_DX1_CODE)) != "")
                    {
                        if (gloCharges.IsValidDxCodeForBilling(Convert.ToString(c1Transaction.GetData(i, COL_DX1_CODE))))
                        {
                            MessageBox.Show("Invalid diagnosis " + Convert.ToString(c1Transaction.GetData(i, COL_DX1_CODE)).Trim() + " for line number " + i.ToString() + ".  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            c1Transaction.Focus();
                            c1Transaction.Select(i, COL_DX1_CODE);
                            return false;
                        }
                        if (Convert.ToString(c1Transaction.GetData(i, COL_DX2_CODE)) != "")
                        {
                            if (gloCharges.IsValidDxCodeForBilling(Convert.ToString(c1Transaction.GetData(i, COL_DX2_CODE))))
                            {
                                MessageBox.Show("Invalid diagnosis " + Convert.ToString(c1Transaction.GetData(i, COL_DX2_CODE)).Trim() + " for line number " + i.ToString() + ".  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                c1Transaction.Focus();
                                c1Transaction.Select(i, COL_DX2_CODE);
                                return false;
                            }
                        }
                        if (Convert.ToString(c1Transaction.GetData(i, COL_DX3_CODE)) != "")
                        {
                            if (gloCharges.IsValidDxCodeForBilling(Convert.ToString(c1Transaction.GetData(i, COL_DX3_CODE))))
                            {
                                MessageBox.Show("Invalid diagnosis " + Convert.ToString(c1Transaction.GetData(i, COL_DX3_CODE)).Trim() + " for line number " + i.ToString() + ".  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                c1Transaction.Focus();
                                c1Transaction.Select(i, COL_DX3_CODE);
                                return false;
                            }
                        }
                        if (Convert.ToString(c1Transaction.GetData(i, COL_DX4_CODE)) != "")
                        {
                            if (gloCharges.IsValidDxCodeForBilling(Convert.ToString(c1Transaction.GetData(i, COL_DX4_CODE))))
                            {
                                MessageBox.Show("Invalid diagnosis " + Convert.ToString(c1Transaction.GetData(i, COL_DX4_CODE)).Trim() + " for line number " + i.ToString() + ".  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                c1Transaction.Focus();
                                c1Transaction.Select(i, COL_DX4_CODE);
                                return false;
                            }
                        }
                    }
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
            return true;
        }


        public bool ValidateICD10DOS()
        {
            DateTime _LeastDOS;
            gloBilling ogloBilling = new gloBilling(_DatabaseConnectionString, _DatabaseConnectionString);
            bool _result = true;
            string msg = string.Empty;
            DialogResult _dltRst = DialogResult.None;
            try
            {

                {
                    if (GetLinesCount() > 1)
                    {
                        _LeastDOS = getLeastServiceLineDos();

                        gloGlobal.gloICD.CodeRevision _CodeRevision = ogloBilling.GetICDCodeType(_nCurrentContactID, gloDateMaster.gloDate.DateAsNumber(_LeastDOS.ToString()));

                        if (_CodeRevision == gloGlobal.gloICD.CodeRevision.ICD9 && IcdCodeType == gloGlobal.gloICD.CodeRevision.ICD10.GetHashCode())
                        {
                            //msg = "Transition date for using ICD-10 codes is in future, billing ICD-10 codes on claims may cause payer rejection.\n Continue?";
                            msg = "ICD-10 Warning:\n\nDate Of Service requires  ICD-9 codes.\n\nBilling ICD-10 codes prior to the transition date may cause payer rejections.\n\nContinue?";
                            _dltRst = MessageBox.Show(msg, _messageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);

                        }
                        else if (_CodeRevision == gloGlobal.gloICD.CodeRevision.ICD10 && IcdCodeType == gloGlobal.gloICD.CodeRevision.ICD9.GetHashCode())
                        {
                            //msg = "Transition date for using ICD-9 codes has elasped, billing ICD-9 codes on claims may cause payer rejection.\n Continue?";
                            msg = "ICD-9 Warning:\n\nDate Of Service requires  ICD-10 codes.\n\nBilling ICD-9 codes may cause payer rejections.\n\nContinue?";
                            _dltRst = MessageBox.Show(msg, _messageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                        }

                        switch (_dltRst)
                        {
                            case DialogResult.OK:
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ICD, gloAuditTrail.ActivityCategory.Validation, gloAuditTrail.ActivityType.OK, msg, PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                                break;
                            case DialogResult.Cancel:
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ICD, gloAuditTrail.ActivityCategory.Validation, gloAuditTrail.ActivityType.Cancle, msg, PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                                _result = false;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
                _result = false;
            }
            finally
            {
                if (ogloBilling != null)
                {
                    ogloBilling.Dispose();
                    ogloBilling = null;
                }
            }
            return _result;
        }
        private bool IsCPTDrugChecked(string sCPTCode)
        {
            gloDatabaseLayer.DBLayer ODB = null;
            string _sqlQuery = "";
            DataTable _dt = new DataTable();
            bool _bReturn = false;
            try
            {
                _sqlQuery = "SELECT TOP 1 ISNULL(bIsCPTDrug,0) AS bIsCPTDrug FROM CPT_MST WITH(NOLOCK) WHERE UPPER(sCPTCode) = '" + sCPTCode.Replace("'", "''").Trim().ToUpper() + "'";
                ODB = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
                ODB.Connect(false);
                ODB.Retrive_Query(_sqlQuery, out _dt);
                ODB.Disconnect();

                if (_dt.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(_dt.Rows[0]["bIsCPTDrug"]))
                    {
                        _bReturn = true;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ODB != null) { ODB.Disconnect(); ODB.Dispose(); }
            }
            return _bReturn;
        }

        public DataTable GetCPTList()
        {
            DataTable _dt = null;
            DataRow _dr = null;
            try
            {
                _dt = new DataTable();

                DataColumn _dc = new DataColumn();
                _dc.ColumnName = "nSlNo";
                _dc.DataType = typeof(System.Int32);
                _dt.Columns.Add(_dc);

                _dc = new DataColumn();
                _dc.ColumnName = "sCPT";
                _dc.DataType = typeof(System.String);
                _dt.Columns.Add(_dc);

                _dc = new DataColumn();
                _dc.ColumnName = "dtStartDate";
                _dc.DataType = typeof(System.DateTime);
                _dt.Columns.Add(_dc);

                for (int iCount = 1; iCount <= c1Transaction.Rows.Count - 1; iCount++)
                {
                    _dr = _dt.NewRow();
                    _dr["nSlNo"] = iCount;
                    _dr["sCPT"] = c1Transaction.GetData(iCount, COL_CPT_CODE);
                    _dr["dtStartDate"] = c1Transaction.GetData(iCount, COL_DATEFROM);
                    _dt.Rows.Add(_dr);
                }
                _dt.AcceptChanges();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

            return _dt;
        }

        public bool ValidateDxList(ArrayList _dxList, bool IsSplit)
        {
            bool _result = true;
            bool _val = false;
            ArrayList arrDx = new ArrayList();

            for (int i = 1; i < c1Transaction.Rows.Count; i++)
            {
                _val = false;
                for (int j = 1; j <= 4; j++)
                {
                    String _dxCode = String.Empty;
                    switch (j)
                    {
                        case 1:
                            _dxCode = Convert.ToString(c1Transaction.GetData(i, COL_DX1_CODE)).Trim();
                            break;
                        case 2:
                            _dxCode = Convert.ToString(c1Transaction.GetData(i, COL_DX2_CODE)).Trim();
                            break;
                        case 3:
                            _dxCode = Convert.ToString(c1Transaction.GetData(i, COL_DX3_CODE)).Trim();
                            break;
                        case 4:
                            _dxCode = Convert.ToString(c1Transaction.GetData(i, COL_DX4_CODE)).Trim();
                            break;
                    }
                    if (_dxList.IndexOf(_dxCode.Trim()) >= 0)
                    { _val = true; break; }
                }
                if (_val == false)
                {
                    MessageBox.Show("Please select diagnosis for line number " + i.ToString() + ".  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                else
                {
                    for (int iRowCount = 1; iRowCount < c1Transaction.Rows.Count; iRowCount++)
                    {
                        for (int iColCount = 1; iColCount <= 4; iColCount++)
                        {
                            String _dxCode = String.Empty;
                            switch (iColCount)
                            {
                                case 1:
                                    _dxCode = Convert.ToString(c1Transaction.GetData(iRowCount, COL_DX1_CODE)).Trim();
                                    if (!arrDx.Contains(_dxCode) && _dxCode != "" && _dxCode != null) { arrDx.Add(_dxCode); }
                                    break;
                                case 2:
                                    _dxCode = Convert.ToString(c1Transaction.GetData(iRowCount, COL_DX2_CODE)).Trim();
                                    if (!arrDx.Contains(_dxCode) && _dxCode != "" && _dxCode != null) { arrDx.Add(_dxCode); }
                                    break;
                                case 3:
                                    _dxCode = Convert.ToString(c1Transaction.GetData(iRowCount, COL_DX3_CODE)).Trim();
                                    if (!arrDx.Contains(_dxCode) && _dxCode != "" && _dxCode != null) { arrDx.Add(_dxCode); }
                                    break;
                                case 4:
                                    _dxCode = Convert.ToString(c1Transaction.GetData(iRowCount, COL_DX4_CODE)).Trim();
                                    if (!arrDx.Contains(_dxCode) && _dxCode != "" && _dxCode != null) { arrDx.Add(_dxCode); }
                                    break;
                            }
                        }
                    }
                    //if (arrDx.Count > 8)
                    //if (arrDx.Count > _NoOfDiagnosis)
                    //{
                    //    if (IsSplit == false)
                    //    {
                    //        if (IsOpenForModify == true)
                    //        {
                    //            //MessageBox.Show("Claim has a max limit of " + _NoOfDiagnosis + " diagnosis.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //            //return true;
                    //        }
                    //        else
                    //        {
                    //            //MessageBox.Show("Claim has a max limit of " + _NoOfDiagnosis + " diagnosis.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //            //return false;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        return true;
                    //    }
                    //}
                }
            }

            return _result;
        }

        public bool IsTransactionDataPresent()
        {
            try
            {

                for (int i = 1; i <= c1Transaction.Rows.Count - 1; i++)
                {
                    if (Convert.ToString(c1Transaction.GetData(i, COL_CPT_CODE)) != "")
                    {
                        return true;
                    }
                    else if (Convert.ToString(c1Transaction.GetData(i, COL_DX1_CODE)) != "")
                    {
                        return true;
                    }
                    else if (Convert.ToDecimal(c1Transaction.GetData(i, COL_CHARGES)) > 0)
                    {
                        return true;
                    }
                    else if (Convert.ToDecimal(c1Transaction.GetData(i, COL_UNIT)) <= 0)
                    {

                    }
                }
                return false;
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

        public void ClearTransactionLine(int TransactionLineNumber)
        {
            if (TransactionLineNumber > 0 && TransactionLineNumber < c1Transaction.Rows.Count)
            {
                c1Transaction.SetData(TransactionLineNumber, COL_CPT_CODE, "");
                c1Transaction.SetData(TransactionLineNumber, COL_DX1_CODE, "");
                c1Transaction.SetData(TransactionLineNumber, COL_DX2_CODE, "");
                c1Transaction.SetData(TransactionLineNumber, COL_DX3_CODE, "");
                c1Transaction.SetData(TransactionLineNumber, COL_DX4_CODE, "");
                c1Transaction.SetData(TransactionLineNumber, COL_DX5_CODE, "");
                c1Transaction.SetData(TransactionLineNumber, COL_DX6_CODE, "");
                c1Transaction.SetData(TransactionLineNumber, COL_DX7_CODE, "");
                c1Transaction.SetData(TransactionLineNumber, COL_DX8_CODE, "");
                c1Transaction.SetCellCheck(TransactionLineNumber, COL_DX1_PTR, CheckEnum.Unchecked);
                c1Transaction.SetCellCheck(TransactionLineNumber, COL_DX2_PTR, CheckEnum.Unchecked);
                c1Transaction.SetCellCheck(TransactionLineNumber, COL_DX3_PTR, CheckEnum.Unchecked);
                c1Transaction.SetCellCheck(TransactionLineNumber, COL_DX4_PTR, CheckEnum.Unchecked);
                c1Transaction.SetCellCheck(TransactionLineNumber, COL_DX5_PTR, CheckEnum.Unchecked);
                c1Transaction.SetCellCheck(TransactionLineNumber, COL_DX6_PTR, CheckEnum.Unchecked);
                c1Transaction.SetCellCheck(TransactionLineNumber, COL_DX7_PTR, CheckEnum.Unchecked);
                c1Transaction.SetCellCheck(TransactionLineNumber, COL_DX8_PTR, CheckEnum.Unchecked);
                c1Transaction.SetData(TransactionLineNumber, COL_MOD1_CODE, "");
                c1Transaction.SetData(TransactionLineNumber, COL_MOD2_CODE, "");
                c1Transaction.SetData(TransactionLineNumber, COL_MOD3_CODE, "");
                c1Transaction.SetData(TransactionLineNumber, COL_MOD4_CODE, "");
                c1Transaction.SetData(TransactionLineNumber, COL_CHARGES, 0);
                c1Transaction.SetData(TransactionLineNumber, COL_UNIT, 0);
                //  c1Transaction.SetData(TransactionLineNumber, COL_ALLOWED, 0);
                c1Transaction.SetCellCheck(TransactionLineNumber, COL_HOLD, CheckEnum.Unchecked);
                c1Transaction.SetData(TransactionLineNumber, COL_HOLD_REASON, "");
                c1Transaction.SetCellCheck(TransactionLineNumber, COL_ISEMG, CheckEnum.Unchecked);
            }
        }

        private Int32 GetLastDataRowNo()
        {
            Int32 _result = c1Transaction.Rows.Count - 1;
            if (c1Transaction.Rows.Count == 1)
            {
                _result = 1;
            }
            else
            {
                for (int i = c1Transaction.Rows.Count - 1; i >= 1; i--)
                {
                    //if (Convert.ToString(c1Transaction.GetData(i, COL_CPT_CODE)) != "")
                    //{
                    if (Convert.ToString(c1Transaction.GetData(i, COL_DX1_CODE)) != "")
                    {
                        //    if (Convert.ToString(c1Transaction.GetData(i, COL_MOD1_CODE)) != "")
                        //    {
                        _result = i + 1;
                        break;
                        //}
                    }

                    //}
                }
            }
            return _result;
        }

        private Int32 GetLastDataRowNo(bool useCPTasBase)
        {
            Int32 _result = c1Transaction.Rows.Count - 1;
            if (c1Transaction.Rows.Count == 1)
            {
                _result = 1;
            }
            else
            {
                if (useCPTasBase == true)
                {
                    for (int i = c1Transaction.Rows.Count - 1; i >= 1; i--)
                    {
                        if (Convert.ToString(c1Transaction.GetData(i, COL_CPT_CODE)) != "")
                        {
                            _result = i + 1;
                            break;
                        }
                    }
                }

                ////Sagar Ghodke: Code review changes, method GetLastDataRowNo(bool useCPTasBase) is always called by "useCPTasBase=True"
                ////so else condition will never satisfied as on today. There is already a overload for the method which is in use so 
                ////for fixing i am commenting the else which is unreachable section and not changing the signature of method

                //else
                //{
                //    for (int i = c1Transaction.Rows.Count - 1; i >= 1; i--)
                //    {
                //        //if (Convert.ToString(c1Transaction.GetData(i, COL_DX1_CODE)) != "")
                //        //{
                //        _result = i + 1;
                //        break;
                //        //}
                //    }
                //}

                ////End of code review changes


            }
            return _result;
        }

        private Int32 GetTransactionGridCount()
        {
            Int32 _result = 1;

            if (c1Transaction.Rows.Count == 1)
            {
                _result = 1;
            }
            else
            {
                for (int i = c1Transaction.Rows.Count - 1; i >= 1; i--)
                {
                    if (Convert.ToString(c1Transaction.GetData(i, COL_CPT_CODE)).Trim() == "")
                    {
                        c1Transaction.Rows.Remove(i);
                    }
                }
                _result = c1Transaction.Rows.Count - 1;
            }
            return _result;
        }

        public string GetLastDOS()
        {
            string _result = "";
            if (c1Transaction.Rows.Count == 1)
            {
                _result = string.Empty;
            }
            else
            {
                if (c1Transaction.GetData(c1Transaction.Rows.Count - 1, COL_DATEFROM) != null && Convert.ToString(c1Transaction.GetData(c1Transaction.Rows.Count - 1, COL_DATEFROM)) != "")
                {
                    _result = Convert.ToString(c1Transaction.GetData(c1Transaction.Rows.Count - 1, COL_DATEFROM));
                }


            }
            return _result;
        }

        //5060
        public Int64 GetLastMaxDOS()
        {
            Int64 _MaxDate = 0;
            if (c1Transaction.Rows.Count == 1)
            {
                _MaxDate = 0;
            }
            else
            {
                for (int _Count = 1; _Count < c1Transaction.Rows.Count; _Count++)
                {
                    if (Convert.ToString(c1Transaction.GetData(_Count, COL_DATEFROM)) != "")
                    {
                        if ((gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1Transaction.GetData(_Count, COL_DATEFROM)))) > _MaxDate)
                        {
                            _MaxDate = (gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1Transaction.GetData(_Count, COL_DATEFROM))));
                        }
                    }
                }
            }
            return _MaxDate;
        }

        public Int64 GetMinDOS()
        {
            Int64 _MinDate = 0;
            if (c1Transaction.Rows.Count == 1)
            {
                _MinDate = 0;
            }
            else
            {
                for (int _Count = 1; _Count < c1Transaction.Rows.Count; _Count++)
                {
                    if (Convert.ToString(c1Transaction.GetData(_Count, COL_DATEFROM)) != "")
                    {
                        if ((gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1Transaction.GetData(_Count, COL_DATEFROM)))) < _MinDate || _MinDate == 0)
                        {
                            _MinDate = (gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1Transaction.GetData(_Count, COL_DATEFROM))));
                        }
                    }
                }
            }
            return _MinDate;
        }

        public Int64 GetMaxToDOS()
        {
            Int64 _MaxDate = 0;
            if (c1Transaction.Rows.Count == 1)
            {
                _MaxDate = 0;
            }
            else
            {
                for (int _Count = 1; _Count < c1Transaction.Rows.Count; _Count++)
                {
                    if (Convert.ToString(c1Transaction.GetData(_Count, COL_DATETO)) != "")
                    {
                        if ((gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1Transaction.GetData(_Count, COL_DATETO)))) > _MaxDate || _MaxDate == 0)
                        {
                            _MaxDate = (gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1Transaction.GetData(_Count, COL_DATETO))));
                        }
                    }
                }
            }
            return _MaxDate;
        }

        public void FillTreatmentInGrid(Int64 SmartTreatmentID)
        {
            DataTable dtTreatmentCPT = new DataTable();
            DataTable dtTreatmentICD9 = new DataTable();
            DataTable dtTreatmentModifier = new DataTable();

            string strSQL = "";
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
            int rowIndex = 0;
            try
            {
                oDB.Connect(false);

                if (SmartTreatmentID != 0)
                {
                    strSQL = "SELECT nTreatmentID, sCPTCode, sCPTDescription FROM BL_SmartTreatmentCPT WITH(NOLOCK) WHERE nTreatmentID=" + SmartTreatmentID + " ";
                    oDB.Retrive_Query(strSQL, out dtTreatmentCPT);

                    if (dtTreatmentCPT != null)
                    {
                        for (int i = 0; i < dtTreatmentCPT.Rows.Count; i++)
                        {
                            rowIndex = GetLastDataRowNo();// c1Transaction.Rows.Count;

                            if (rowIndex > c1Transaction.Rows.Count - 1)
                            {
                                c1Transaction.Rows.Add();
                            }

                            c1Transaction.SetData(rowIndex, COL_NO, rowIndex);
                            c1Transaction.SetData(rowIndex, COL_DATEFROM, DateTime.Now.Date);
                            c1Transaction.SetData(rowIndex, COL_DATETO, DateTime.Now.Date);

                            if (_DefaultRenderingProviderID > 0)
                            {
                                c1Transaction.SetData(rowIndex, COL_PROVIDER_ID, _DefaultRenderingProviderID);
                                c1Transaction.SetData(rowIndex, COL_PROVIDER, _DefaultRenderringProviderName);
                            }
                            SetCurrencyCellValue(rowIndex);

                            GetDefaultTOSPOS();
                            c1Transaction.SetData(rowIndex, COL_TOSCODE, _DefaultTOSCode);
                            c1Transaction.SetData(rowIndex, COL_TOSDESC, _DefaultTOSDesc);
                            c1Transaction.SetData(rowIndex, COL_POSCODE, _DefaultPOSCode);
                            c1Transaction.SetData(rowIndex, COL_POSDESC, _DefaultPOSDesc);

                            //1. Add the CPT 
                            c1Transaction.SetData(rowIndex, COL_CPT_CODE, Convert.ToString(dtTreatmentCPT.Rows[i]["sCPTCode"]));
                            c1Transaction.SetData(rowIndex, COL_CPT_DESC, Convert.ToString(dtTreatmentCPT.Rows[i]["sCPTDescription"]));

                            //2.Get the associated ICD9
                            strSQL = "SELECT nTreatmentID, sCPTCode, sCPTDescription, sICD9Code, sICD9Description, dCharges, nUnits FROM BL_SmartTreatmentICD9 WITH(NOLOCK) WHERE nTreatmentID=" + SmartTreatmentID + " AND sCPTCode='" + dtTreatmentCPT.Rows[i]["sCPTCode"].ToString() + "'";
                            oDB.Retrive_Query(strSQL, out dtTreatmentICD9);

                            if (dtTreatmentICD9 != null)
                            {
                                int _ColCode = 0;
                                int _ColDesc = 0;
                                int _ColPtr = 0;
                                for (int k = 0; k < dtTreatmentICD9.Rows.Count; k++)
                                {
                                    switch (k)
                                    {
                                        case 0:
                                            { _ColCode = COL_DX1_CODE; _ColDesc = COL_DX1_DESC; _ColPtr = COL_DX1_PTR; }
                                            break;
                                        case 1:
                                            { _ColCode = COL_DX2_CODE; _ColDesc = COL_DX2_DESC; _ColPtr = COL_DX2_PTR; }
                                            break;
                                        case 2:
                                            { _ColCode = COL_DX3_CODE; _ColDesc = COL_DX3_DESC; _ColPtr = COL_DX3_PTR; }
                                            break;
                                        case 3:
                                            { _ColCode = COL_DX4_CODE; _ColDesc = COL_DX4_DESC; _ColPtr = COL_DX4_PTR; }
                                            break;
                                    }

                                    c1Transaction.SetData(rowIndex, _ColCode, Convert.ToString(dtTreatmentICD9.Rows[k]["sICD9Code"]));
                                    c1Transaction.SetData(rowIndex, _ColDesc, Convert.ToString(dtTreatmentICD9.Rows[k]["sICD9Description"]));

                                    //also set the pointers
                                    c1Transaction.SetCellCheck(rowIndex, _ColPtr, CheckEnum.Checked);

                                    //

                                    if (k == 3)
                                    { break; }

                                    strSQL = "SELECT nTreatmentID, sCPTCode, sCPTDescription, sICD9Code, sICD9Description, sModifierCode, sModifierDesc FROM BL_SmartTreatmentModifier WITH(NOLOCK) WHERE nTreatmentID=" + SmartTreatmentID + " AND sICD9Code='" + dtTreatmentICD9.Rows[k]["sICD9Code"].ToString() + "'";
                                    oDB.Retrive_Query(strSQL, out dtTreatmentModifier);
                                    if (dtTreatmentModifier != null)
                                    {
                                        int _ColModCode = 0;
                                        int _ColModDesc = 0;

                                        for (int j = 0; j < dtTreatmentModifier.Rows.Count; j++)
                                        {

                                            switch (k)
                                            {
                                                case 0:
                                                    { _ColModCode = COL_MOD1_CODE; _ColModDesc = COL_MOD1_DESC; }
                                                    break;
                                                case 1:
                                                    { _ColModCode = COL_MOD2_CODE; _ColModDesc = COL_MOD2_DESC; }
                                                    break;
                                                case 2:
                                                    { _ColModCode = COL_MOD3_CODE; _ColModDesc = COL_MOD3_DESC; }
                                                    break;
                                                case 3:
                                                    { _ColModCode = COL_MOD4_CODE; _ColModDesc = COL_MOD4_DESC; }
                                                    break;
                                            }

                                            c1Transaction.SetData(rowIndex, _ColModCode, Convert.ToString(dtTreatmentModifier.Rows[j]["sModifierCode"]));
                                            c1Transaction.SetData(rowIndex, _ColModDesc, Convert.ToString(dtTreatmentModifier.Rows[j]["sModifierDesc"]));
                                            if (j == 1)
                                            { break; }
                                        }
                                    }
                                }

                            }//end - if (dtTreatmentICD9 != null)
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
                if (dtTreatmentCPT != null)
                {
                    dtTreatmentCPT.Dispose();
                }
                if (dtTreatmentICD9 != null)
                {
                    dtTreatmentICD9.Dispose();
                }
                if (dtTreatmentModifier != null)
                {
                    dtTreatmentModifier.Dispose();
                }
                if (oDB.Connect(false))
                { oDB.Disconnect(); oDB.Dispose(); }

            }
        }

        public void FillTreatmentInGrid(Int64 SmartTreatmentID, Int64 InsuranceId, string InsuranceName, int InsuranceSelfMode, DateTime CloseDate, out string errormessage)
        {
            DataTable dtTreatmentCPT = new DataTable();
            DataTable dtTreatmentICD9 = new DataTable();
            DataTable dtTreatmentModifier = new DataTable();
            //   string _errormessage = "";
            string strSQL = "";
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
            int rowIndex = 0;
            ArrayList arrDx = new ArrayList();
            TrnCtrlColValChangeEventArg e2 = new TrnCtrlColValChangeEventArg();
            RowColEventArgs e1 = null;
            int iGridRowCount = 0;
            ArrayList arDxList = new ArrayList();
            try
            {
                errormessage = "";
                oDB.Connect(false);
                pnlInternalControl.SendToBack();

                if (SmartTreatmentID != 0)
                {
                    string defaultDX1 = string.Empty; string defaultDX1Desc = string.Empty;
                    string defaultDX2 = string.Empty; string defaultDX2Desc = string.Empty;
                    string defaultDX3 = string.Empty; string defaultDX3Desc = string.Empty;
                    string defaultDX4 = string.Empty; string defaultDX4Desc = string.Empty;

                    if (c1Transaction.Rows.Count >= 2)
                    {
                        defaultDX1 = Convert.ToString(c1Transaction.GetData(1, COL_DX1_CODE));
                        defaultDX2 = Convert.ToString(c1Transaction.GetData(1, COL_DX2_CODE));
                        defaultDX3 = Convert.ToString(c1Transaction.GetData(1, COL_DX3_CODE));
                        defaultDX4 = Convert.ToString(c1Transaction.GetData(1, COL_DX4_CODE));

                        defaultDX1Desc = Convert.ToString(c1Transaction.GetData(1, COL_DX1_DESC));
                        defaultDX2Desc = Convert.ToString(c1Transaction.GetData(1, COL_DX2_DESC));
                        defaultDX3Desc = Convert.ToString(c1Transaction.GetData(1, COL_DX3_DESC));
                        defaultDX4Desc = Convert.ToString(c1Transaction.GetData(1, COL_DX4_DESC));
                        for (int i = 1; i <= c1Transaction.Rows.Count - 1; i++)
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(c1Transaction.GetData(i, COL_POSCODE))))
                            {
                                _DefaultPOSCode = Convert.ToString(c1Transaction.GetData(i, COL_POSCODE));
                                _DefaultPOSDesc = Convert.ToString(c1Transaction.GetData(i, COL_POSDESC));
                                break;
                            }
                        }

                        if (string.IsNullOrEmpty(Convert.ToString(c1Transaction.GetData(1, COL_CPT_CODE))))
                        {
                            // Reset All the DX first
                            c1Transaction.SetData(1, COL_DX1_CODE, null); c1Transaction.SetData(1, COL_DX1_DESC, null); c1Transaction.SetCellCheck(1, COL_DX1_PTR, CheckEnum.Unchecked);
                            c1Transaction.SetData(1, COL_DX2_CODE, null); c1Transaction.SetData(1, COL_DX2_DESC, null); c1Transaction.SetCellCheck(1, COL_DX2_PTR, CheckEnum.Unchecked);
                            c1Transaction.SetData(1, COL_DX3_CODE, null); c1Transaction.SetData(1, COL_DX3_DESC, null); c1Transaction.SetCellCheck(1, COL_DX3_PTR, CheckEnum.Unchecked);
                            c1Transaction.SetData(1, COL_DX4_CODE, null); c1Transaction.SetData(1, COL_DX4_DESC, null); c1Transaction.SetCellCheck(1, COL_DX4_PTR, CheckEnum.Unchecked);
                        }
                    }

                    strSQL = "SELECT nTreatmentID, sCPTCode, sCPTDescription FROM BL_SmartTreatmentCPT WITH(NOLOCK) WHERE nTreatmentID=" + SmartTreatmentID + " ";
                    oDB.Retrive_Query(strSQL, out dtTreatmentCPT);

                    if (dtTreatmentCPT != null)
                    {
                        //*********************************************************************
                        //commented By Debasish Das on 30th Dec 2010 while merging 5073 to 6002
                        //Reason: while loading Smart Treatment Grid was reseting.(Breking 5073 Functionality)

                        //c1Transaction.Rows.Count = 1;

                        //*********************************************************************

                        iGridRowCount = GetTransactionGridCount();
                        arrDx = GetUniqueDx();

                        for (int i = 0; i < dtTreatmentCPT.Rows.Count; i++)
                        {
                            //rowIndex = GetLastDataRowNo(true);// c1Transaction.Rows.Count; Wil Check for Blank CPT  - Buse of this more than 6 service lines  message was showing although there less than 6 service lines

                            //rowIndex = GetLastDataRowNo(false);// If False Wil not Check for Blank CPT --

                            // Check for the blank CPT
                            rowIndex = GetLastDataRowNo(true);


                            //if (rowIndex <= VISIBLE_CPT_COUNT )
                            //if ((rowIndex > c1Transaction.Rows.Count - 1 || rowIndex == 1) && rowIndex <= VISIBLE_CPT_COUNT)
                            if (rowIndex <= _NoOfServiceLines)
                            {
                                if (rowIndex > c1Transaction.Rows.Count - 1)
                                {
                                    c1Transaction.Rows.Add();
                                }

                                #region "Add Smart Treatment"
                                c1Transaction.Focus();
                                c1Transaction.Select(rowIndex, COL_CPT_CODE);
                                c1Transaction.SetData(rowIndex, COL_NO, rowIndex);

                                #region "DOS, POS, TOS"


                                //added on 28/04/2010 for case: Smart Treatment DOS is wrong --> if close date not given set today's date to service line DOS 
                                // this close date check and set on frmBillingtransaction form

                                c1Transaction.SetData(rowIndex, COL_DATEFROM, CloseDate.ToShortDateString());

                                if (c1Transaction.Rows.Count - 2 > 0 && Convert.ToString(c1Transaction.GetData(c1Transaction.Rows.Count - 2, COL_DATETO)) != "")
                                {
                                    c1Transaction.SetData(c1Transaction.Rows.Count - 1, COL_DATETO, c1Transaction.GetData(c1Transaction.Rows.Count - 2, COL_DATETO));
                                }

                                //Commented on 24th Jan 2010
                                //Should fill Blank while SmartTreatment.
                                //c1Transaction.SetData(rowIndex, COL_DATETO, CloseDate.ToShortDateString());
                                //****

                                //c1Transaction.SetData(rowIndex, COL_DATEFROM, DateTime.Now.Date);
                                //c1Transaction.SetData(rowIndex, COL_DATETO, DateTime.Now.Date);

                                if (_DefaultRenderingProviderID > 0)
                                {
                                    c1Transaction.SetData(rowIndex, COL_PROVIDER_ID, _DefaultRenderingProviderID);
                                    c1Transaction.SetData(rowIndex, COL_PROVIDER, _DefaultRenderringProviderName);
                                }

                                AddInsurance(rowIndex, InsuranceId, InsuranceName, InsuranceSelfMode);

                                SetCurrencyCellValue(rowIndex);
                                if (string.IsNullOrEmpty(_DefaultPOSCode) || string.IsNullOrEmpty(_DefaultTOSCode))
                                    GetDefaultTOSPOS();
                                //_DefaultPOSCode = Convert.ToString(c1Transaction.GetData(1, COL_POSCODE));
                                //_DefaultPOSDesc = Convert.ToString(c1Transaction.GetData(1, COL_POSDESC));
                                c1Transaction.SetData(rowIndex, COL_TOSCODE, _DefaultTOSCode);
                                c1Transaction.SetData(rowIndex, COL_TOSDESC, _DefaultTOSDesc);
                                c1Transaction.SetData(rowIndex, COL_POSCODE, _DefaultPOSCode);
                                c1Transaction.SetData(rowIndex, COL_POSDESC, _DefaultPOSDesc);

                                #endregion

                                #region "CPT"
                                //1. Add the CPT 
                                c1Transaction.SetData(rowIndex, COL_CPT_CODE, Convert.ToString(dtTreatmentCPT.Rows[i]["sCPTCode"]));
                                c1Transaction.SetData(rowIndex, COL_CPT_DESC, Convert.ToString(dtTreatmentCPT.Rows[i]["sCPTDescription"]));
                                
                                if (Convert.ToString(dtTreatmentCPT.Rows[i]["sCPTCode"]) != "")
                                {
                                    e2 = new TrnCtrlColValChangeEventArg();
                                    e2.code = Convert.ToString(dtTreatmentCPT.Rows[i]["sCPTCode"]);
                                    e2.description = Convert.ToString(dtTreatmentCPT.Rows[i]["sCPTDescription"]);
                                    e2.oType = TransactionLineColumnType.CPT;
                                    e2.isdeleted = false;
                                    onInsCPTDxMod_Changed(null, e1, e2);
                                }
                                #endregion


                                //2.Get the associated ICD9
                                strSQL = "SELECT nTreatmentID, sCPTCode, sCPTDescription, sICD9Code, sICD9Description, dCharges, nUnits FROM BL_SmartTreatmentICD9 WITH(NOLOCK) WHERE nTreatmentID=" + SmartTreatmentID + " AND sCPTCode='" + dtTreatmentCPT.Rows[i]["sCPTCode"].ToString().Replace("'", "''") + "'";
                                oDB.Retrive_Query(strSQL, out dtTreatmentICD9);

                                #region "Diagnosis and Modifier - if its from database"
                                if (dtTreatmentICD9 != null && dtTreatmentICD9.Rows.Count > 0)
                                {
                                    if (dtTreatmentICD9.Rows.Count != 0)
                                    {
                                        int _ColCode = 0;
                                        int _ColDesc = 0;
                                        int _ColPtr = 0;
                                        for (int k = 0; k < dtTreatmentICD9.Rows.Count; k++)
                                        {
                                            if (!arDxList.Contains(Convert.ToString(dtTreatmentICD9.Rows[k]["sICD9Code"])))
                                            {
                                                arDxList.Add(Convert.ToString(dtTreatmentICD9.Rows[k]["sICD9Code"]));
                                            }

                                            //if (arDxList.Count > _NoOfDiagnosis)
                                            //{
                                            //    c1Transaction.Rows.Remove(rowIndex);

                                            //}

                                            switch (k)
                                            {
                                                case 0:
                                                    { _ColCode = COL_DX1_CODE; _ColDesc = COL_DX1_DESC; _ColPtr = COL_DX1_PTR; }
                                                    break;
                                                case 1:
                                                    { _ColCode = COL_DX2_CODE; _ColDesc = COL_DX2_DESC; _ColPtr = COL_DX2_PTR; }
                                                    break;
                                                case 2:
                                                    { _ColCode = COL_DX3_CODE; _ColDesc = COL_DX3_DESC; _ColPtr = COL_DX3_PTR; }
                                                    break;
                                                case 3:
                                                    { _ColCode = COL_DX4_CODE; _ColDesc = COL_DX4_DESC; _ColPtr = COL_DX4_PTR; }
                                                    break;
                                            }

                                            c1Transaction.SetData(rowIndex, _ColCode, Convert.ToString(dtTreatmentICD9.Rows[k]["sICD9Code"]));
                                            c1Transaction.SetData(rowIndex, _ColDesc, Convert.ToString(dtTreatmentICD9.Rows[k]["sICD9Description"]));

                                            //also set the pointers
                                            c1Transaction.SetCellCheck(rowIndex, _ColPtr, CheckEnum.Checked);

                                            if (Convert.ToString(dtTreatmentICD9.Rows[k]["sICD9Code"]) != "")
                                            {
                                                e2 = new TrnCtrlColValChangeEventArg();
                                                e2.code = Convert.ToString(dtTreatmentICD9.Rows[k]["sICD9Code"]);
                                                e2.description = Convert.ToString(dtTreatmentICD9.Rows[k]["sICD9Description"]);
                                                e2.oType = TransactionLineColumnType.Diagnosis;
                                                e2.isdeleted = false;
                                                onInsCPTDxMod_Changed(null, e1, e2);
                                            }

                                            //



                                            //strSQL = "SELECT nTreatmentID, sCPTCode, sCPTDescription, sICD9Code, sICD9Description, sModifierCode, sModifierDesc FROM BL_SmartTreatmentModifier WHERE nTreatmentID=" + SmartTreatmentID + " AND sICD9Code='" + dtTreatmentICD9.Rows[k]["sICD9Code"].ToString().Replace("'","''") + "'";
                                            strSQL = "SELECT sModifierCode,sModifierDesc FROM BL_SmartTreatmentModifier WITH(NOLOCK) WHERE nTreatmentID=" + SmartTreatmentID + " AND sICD9Code='" + dtTreatmentICD9.Rows[k]["sICD9Code"].ToString().Replace("'", "''") + "' " +
                                              " AND sCPTCode='" + dtTreatmentCPT.Rows[i]["sCPTCode"].ToString().Replace("'", "''") + "'";

                                            oDB.Retrive_Query(strSQL, out dtTreatmentModifier);
                                            if (dtTreatmentModifier != null)
                                            {
                                                int _ColModCode = 0;
                                                int _ColModDesc = 0;


                                                for (int j = 0; j < dtTreatmentModifier.Rows.Count; j++)
                                                {

                                                    //switch (k)
                                                    switch (j)
                                                    {
                                                        case 0:
                                                            { _ColModCode = COL_MOD1_CODE; _ColModDesc = COL_MOD1_DESC; }
                                                            break;
                                                        case 1:
                                                            { _ColModCode = COL_MOD2_CODE; _ColModDesc = COL_MOD2_DESC; }
                                                            break;
                                                        case 2:
                                                            { _ColModCode = COL_MOD3_CODE; _ColModDesc = COL_MOD3_DESC; }
                                                            break;
                                                        case 3:
                                                            { _ColModCode = COL_MOD4_CODE; _ColModDesc = COL_MOD4_DESC; }
                                                            break;
                                                    }

                                                    c1Transaction.SetData(rowIndex, _ColModCode, Convert.ToString(dtTreatmentModifier.Rows[j]["sModifierCode"]));
                                                    c1Transaction.SetData(rowIndex, _ColModDesc, Convert.ToString(dtTreatmentModifier.Rows[j]["sModifierDesc"]));
                                                    if (j == 1)
                                                    { break; }
                                                }
                                            }

                                            if (k == 3)
                                            { break; }

                                        }
                                    }

                                }//end - if (dtTreatmentICD9 != null)

                                #endregion

                                #region "Diagnosis and Modifier - if its from upper row"
                                else
                                {
                                    #region "For only Cpt & modifiers "

                                    strSQL = "SELECT sModifierCode,sModifierDesc FROM BL_SmartTreatmentModifier WITH(NOLOCK) WHERE nTreatmentID=" + SmartTreatmentID + " AND isnull(sICD9Code,'') ='' " +
                                              " AND sCPTCode='" + dtTreatmentCPT.Rows[i]["sCPTCode"].ToString().Replace("'", "''") + "'";

                                    oDB.Retrive_Query(strSQL, out dtTreatmentModifier);
                                    if (dtTreatmentModifier != null && dtTreatmentModifier.Rows.Count > 0)
                                    {
                                        int _ColModCode = 0;
                                        int _ColModDesc = 0;


                                        for (int j = 0; j < dtTreatmentModifier.Rows.Count; j++)
                                        {

                                            //switch (k)
                                            switch (j)
                                            {
                                                case 0:
                                                    { _ColModCode = COL_MOD1_CODE; _ColModDesc = COL_MOD1_DESC; }
                                                    break;
                                                case 1:
                                                    { _ColModCode = COL_MOD2_CODE; _ColModDesc = COL_MOD2_DESC; }
                                                    break;
                                                case 2:
                                                    { _ColModCode = COL_MOD3_CODE; _ColModDesc = COL_MOD3_DESC; }
                                                    break;
                                                case 3:
                                                    { _ColModCode = COL_MOD4_CODE; _ColModDesc = COL_MOD4_DESC; }
                                                    break;
                                            }

                                            c1Transaction.SetData(rowIndex, _ColModCode, Convert.ToString(dtTreatmentModifier.Rows[j]["sModifierCode"]));
                                            c1Transaction.SetData(rowIndex, _ColModDesc, Convert.ToString(dtTreatmentModifier.Rows[j]["sModifierDesc"]));
                                            //if (j == 1)
                                            //{ break; }
                                        }


                                    }
                                    #endregion"End For only Cpt & modifiers "


                                    if (!string.IsNullOrEmpty(defaultDX1))
                                    {
                                        // Set primary DX
                                        c1Transaction.SetData(rowIndex, COL_LINEPRIMARY_DXCODE, defaultDX1);
                                        c1Transaction.SetData(rowIndex, COL_LINEPRIMARY_DXDESC, defaultDX1Desc);

                                        c1Transaction.SetData(rowIndex, COL_DX1_CODE, defaultDX1);
                                        c1Transaction.SetData(rowIndex, COL_DX1_DESC, defaultDX1Desc);
                                        c1Transaction.SetCellCheck(rowIndex, COL_DX1_PTR, CheckEnum.Checked);
                                    }
                                    if (!string.IsNullOrEmpty(defaultDX2))
                                    {
                                        c1Transaction.SetData(rowIndex, COL_DX2_CODE, defaultDX2);
                                        c1Transaction.SetData(rowIndex, COL_DX2_DESC, defaultDX2Desc);
                                        c1Transaction.SetCellCheck(rowIndex, COL_DX2_PTR, CheckEnum.Checked);
                                    }
                                    if (!string.IsNullOrEmpty(defaultDX3))
                                    {
                                        c1Transaction.SetData(rowIndex, COL_DX3_CODE, defaultDX3);
                                        c1Transaction.SetData(rowIndex, COL_DX3_DESC, defaultDX3Desc);
                                        c1Transaction.SetCellCheck(rowIndex, COL_DX3_PTR, CheckEnum.Checked);
                                    }
                                    if (!string.IsNullOrEmpty(defaultDX4))
                                    {
                                        c1Transaction.SetData(rowIndex, COL_DX4_CODE, defaultDX4);
                                        c1Transaction.SetData(rowIndex, COL_DX4_DESC, defaultDX4Desc);
                                        c1Transaction.SetCellCheck(rowIndex, COL_DX4_PTR, CheckEnum.Checked);
                                    }

                                    #region "Set Tooltip Event as well as add into master diagnosis list on charges form"
                                    //e2 = new TrnCtrlColValChangeEventArg();
                                    //e2.code = defaultDX1;
                                    //e2.description = defaultDX1Desc;
                                    //e2.oType = TransactionLineColumnType.Diagnosis;
                                    //e2.isdeleted = false;
                                    //onInsCPTDxMod_Changed(null, e1, e2);
                                    #endregion

                                }
                                #endregion



                                #endregion

                                // Set CPT Defaults after applying the Smart Treatment
                                SetCPTDefaults(gloBillingTransaction.CPTChangedFrom.SmartTreatment);

                                #region " Load fee schedule "

                                _AutoSort = false;

                                c1Transaction.Select(rowIndex, COL_CPT_CODE);
                                SelectTransactionLine(rowIndex);
                                SetFNFCharges();

                                _AutoSort = true;



                                #endregion
                                c1Transaction.SetData(rowIndex, COL_LINEPRIMARY_DXCODE, c1Transaction.GetData(rowIndex, COL_DX1_CODE));
                                c1Transaction.SetData(rowIndex, COL_LINEPRIMARY_DXDESC, c1Transaction.GetData(rowIndex, COL_DX1_DESC));


                            }
                            //else
                            //{
                            //    //_errormessage = "You cannot add more than " + _NoOfServiceLines + " service lines per claim";
                            //    if (errormessage == string.Empty)
                            //    {
                            //        _errormessage = "Claim is limited to " + _NoOfServiceLines + " charges and " + _NoOfDiagnosis + " diagnoses. A portion of the Smart Treatment cannot be included.";
                            //    }
                            //}
                        }
                        SortControl();
                    }

                }



            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (dtTreatmentCPT != null)
                {
                    dtTreatmentCPT.Dispose();
                }
                if (dtTreatmentICD9 != null)
                {
                    dtTreatmentICD9.Dispose();
                }
                if (dtTreatmentModifier != null)
                {
                    dtTreatmentModifier.Dispose();
                }
                if (oDB.Connect(false))
                { oDB.Disconnect(); oDB.Dispose(); }

            }
            errormessage = ReturnMessageServicelineDx(SmartTreatmentID, iGridRowCount, arrDx);
        }

        public string ReturnMessageServicelineDx(Int64 nTreatmentID, int iPrevRowCount, ArrayList arrDx)
        {
            DataTable dtTreatmentCPT = new DataTable();
            DataTable dtTreatmentICD9 = new DataTable();
            string _errormessageCPT = string.Empty;
            string _errormessageICD9 = string.Empty;
            string strSQL = "";
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
            string _errormessage = string.Empty;

            ArrayList arrFiltered = new ArrayList();
            try
            {
                oDB.Connect(false);


                if (nTreatmentID != 0)
                {
                    strSQL = "SELECT DISTINCT sCPTCode FROM BL_SmartTreatmentCPT WITH(NOLOCK) WHERE nTreatmentID=" + nTreatmentID;
                    oDB.Retrive_Query(strSQL, out dtTreatmentCPT);

                    if (dtTreatmentCPT != null)
                    {
                        if ((dtTreatmentCPT.Rows.Count + iPrevRowCount - 1) > _NoOfServiceLines)
                        {
                            _errormessageCPT = "Claim is limited to " + _NoOfServiceLines + " charges";
                        }
                    }

                    strSQL = "SELECT DISTINCT sICD9Code FROM BL_SmartTreatmentICD9 WITH(NOLOCK) WHERE nTreatmentID=" + nTreatmentID;
                    oDB.Retrive_Query(strSQL, out dtTreatmentICD9);
                    for (int iCount = 0; iCount <= dtTreatmentICD9.Rows.Count - 1; iCount++)
                    {
                        if (!arrDx.Contains(Convert.ToString(dtTreatmentICD9.Rows[iCount]["sICD9Code"])))
                        {
                            arrFiltered.Add(Convert.ToString(dtTreatmentICD9.Rows[iCount]["sICD9Code"]));
                        }
                    }

                    if (dtTreatmentICD9 != null)
                    {
                        if (arrFiltered.Count + arrDx.Count > _NoOfDiagnosis)
                        {
                            if (_errormessageCPT == string.Empty)
                            {
                                _errormessageICD9 = "Claim is limited to " + _NoOfDiagnosis + " diagnoses";
                            }
                            else
                            {
                                _errormessageCPT += " and " + _NoOfDiagnosis + " diagnoses";
                            }
                        }
                    }

                    if (_errormessageCPT != string.Empty)
                    {
                        _errormessage += _errormessageCPT + ".  A portion of the Smart Treatment cannot be included.";
                    }
                    else
                    {

                        _errormessage += _errormessageICD9;
                    }

                }


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB.Connect(false)) { oDB.Disconnect(); oDB.Dispose(); }
            }
            return _errormessage;
        }

        public ArrayList GetUniqueDx()
        {
            ArrayList arrDX = new ArrayList();
            try
            {
                for (int iCount = 1; iCount <= c1Transaction.Rows.Count - 1; iCount++)
                {
                    for (int idxCount = COL_DX1_CODE; idxCount <= COL_DX4_CODE; idxCount++)
                    {
                        switch (idxCount)
                        {
                            case COL_DX1_CODE:
                                if (Convert.ToString(c1Transaction.GetData(iCount, idxCount)) != "")
                                {
                                    if (!arrDX.Contains(Convert.ToString(c1Transaction.GetData(iCount, idxCount)).Trim()))
                                    {
                                        arrDX.Add(Convert.ToString(c1Transaction.GetData(iCount, idxCount)).Trim());
                                    }
                                }
                                break;
                            case COL_DX2_CODE:
                                if (Convert.ToString(c1Transaction.GetData(iCount, idxCount)) != "")
                                {
                                    if (!arrDX.Contains(Convert.ToString(c1Transaction.GetData(iCount, idxCount)).Trim()))
                                    {
                                        arrDX.Add(Convert.ToString(c1Transaction.GetData(iCount, idxCount)).Trim());
                                    }
                                }
                                break;
                            case COL_DX3_CODE:
                                if (Convert.ToString(c1Transaction.GetData(iCount, idxCount)) != "")
                                {
                                    if (!arrDX.Contains(Convert.ToString(c1Transaction.GetData(iCount, idxCount)).Trim()))
                                    {
                                        arrDX.Add(Convert.ToString(c1Transaction.GetData(iCount, idxCount)).Trim());
                                    }
                                }
                                break;
                            case COL_DX4_CODE:
                                if (Convert.ToString(c1Transaction.GetData(iCount, idxCount)) != "")
                                {
                                    if (!arrDX.Contains(Convert.ToString(c1Transaction.GetData(iCount, idxCount)).Trim()))
                                    {
                                        arrDX.Add(Convert.ToString(c1Transaction.GetData(iCount, idxCount)).Trim());
                                    }
                                }
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

            return arrDX;
        }

        public void FillTreatmentInGrid(int LineIndex, Int64 SmartTreatmentID)
        {
            //Note : Method not in use work not completed

            DataTable dtTreatmentCPT = new DataTable();
            DataTable dtTreatmentICD9 = new DataTable();
            DataTable dtTreatmentModifier = new DataTable();

            string strSQL = "";
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
            int rowIndex = 0;
            try
            {
                oDB.Connect(false);
                rowIndex = LineIndex;

                if (SmartTreatmentID != 0)
                {
                    strSQL = "SELECT sCPTCode,sCPTDescription FROM BL_SmartTreatmentCPT WITH(NOLOCK) WHERE nTreatmentID=" + SmartTreatmentID + " ";
                    oDB.Retrive_Query(strSQL, out dtTreatmentCPT);

                    if (dtTreatmentCPT != null)
                    {
                        for (int i = 0; i < dtTreatmentCPT.Rows.Count; i++)
                        {
                            if (c1Transaction.Rows.Count <= LineIndex)
                            {
                                c1Transaction.Rows.Add();
                            }

                            //1. Add the CPT 
                            c1Transaction.SetData(rowIndex, COL_CPT_CODE, Convert.ToString(dtTreatmentCPT.Rows[i]["sCPTCode"]));
                            c1Transaction.SetData(rowIndex, COL_CPT_DESC, Convert.ToString(dtTreatmentCPT.Rows[i]["sCPTDescription"]));

                            //2.Get the associated ICD9
                            strSQL = "SELECT sICD9Code,sICD9Description FROM BL_SmartTreatmentICD9 WITH(NOLOCK) WHERE nTreatmentID=" + SmartTreatmentID + " AND sCPTCode='" + dtTreatmentCPT.Rows[i]["sCPTCode"].ToString() + "'";
                            oDB.Retrive_Query(strSQL, out dtTreatmentICD9);

                            if (dtTreatmentICD9 != null)
                            {
                                int _ColCode = 0;
                                int _ColDesc = 0;
                                for (int k = 0; k < dtTreatmentICD9.Rows.Count; k++)
                                {
                                    switch (k)
                                    {
                                        case 0:
                                            { _ColCode = COL_DX1_CODE; _ColDesc = COL_DX1_DESC; }
                                            break;
                                        case 1:
                                            { _ColCode = COL_DX2_CODE; _ColDesc = COL_DX2_DESC; }
                                            break;
                                        case 2:
                                            { _ColCode = COL_DX3_CODE; _ColDesc = COL_DX3_DESC; }
                                            break;
                                        case 3:
                                            { _ColCode = COL_DX4_CODE; _ColDesc = COL_DX4_DESC; }
                                            break;
                                    }

                                    c1Transaction.SetData(rowIndex, _ColCode, Convert.ToString(dtTreatmentICD9.Rows[k]["sICD9Code"]));
                                    c1Transaction.SetData(rowIndex, _ColDesc, Convert.ToString(dtTreatmentICD9.Rows[k]["sICD9Description"]));
                                    if (k == 3)
                                    { break; }

                                    strSQL = "SELECT sModifierCode,sModifierDesc FROM BL_SmartTreatmentModifier WITH(NOLOCK) WHERE nTreatmentID=" + SmartTreatmentID + " AND sICD9Code='" + dtTreatmentICD9.Rows[k]["sICD9Code"].ToString() + "'";


                                    oDB.Retrive_Query(strSQL, out dtTreatmentModifier);
                                    if (dtTreatmentModifier != null)
                                    {
                                        int _ColModCode = 0;
                                        int _ColModDesc = 0;

                                        for (int j = 0; j < dtTreatmentModifier.Rows.Count; j++)
                                        {

                                            switch (j)
                                            {
                                                case 0:
                                                    { _ColModCode = COL_MOD1_CODE; _ColModDesc = COL_MOD1_DESC; }
                                                    break;
                                                case 1:
                                                    { _ColModCode = COL_MOD2_CODE; _ColModDesc = COL_MOD2_DESC; }
                                                    break;
                                                case 2:
                                                    { _ColModCode = COL_MOD3_CODE; _ColModDesc = COL_MOD3_DESC; }
                                                    break;
                                                case 3:
                                                    { _ColModCode = COL_MOD4_CODE; _ColModDesc = COL_MOD4_DESC; }
                                                    break;
                                            }

                                            c1Transaction.SetData(rowIndex, _ColModCode, Convert.ToString(dtTreatmentModifier.Rows[j]["sModifierCode"]));
                                            c1Transaction.SetData(rowIndex, _ColModDesc, Convert.ToString(dtTreatmentModifier.Rows[j]["sModifierDesc"]));
                                            if (j == 1)
                                            { break; }
                                        }
                                    }
                                }

                            }//end - if (dtTreatmentICD9 != null)
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
                if (dtTreatmentCPT != null)
                {
                    dtTreatmentCPT.Dispose();
                }
                if (dtTreatmentICD9 != null)
                {
                    dtTreatmentICD9.Dispose();
                }
                if (dtTreatmentModifier != null)
                {
                    dtTreatmentModifier.Dispose();
                }
                if (oDB.Connect(false))
                { oDB.Disconnect(); oDB.Dispose(); }

            }
        }

        public void SetPointer(int rowIndex, int PtrColIndex, int CodeColIndex)
        {
            try
            {
                if (CodeColIndex > 0)
                {
                    if (c1Transaction.GetData(rowIndex, CodeColIndex) == null || Convert.ToString(c1Transaction.GetData(rowIndex, CodeColIndex)) == "")
                    {
                        c1Transaction.SetCellCheck(rowIndex, PtrColIndex, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        public void SetServiceLineDate(int rowIndex, DateTime ServiceDate)
        {
            try
            {
                if (c1Transaction != null && c1Transaction.Rows.Count > 0)
                {
                    if (rowIndex > 0)
                    {
                        c1Transaction.SetData(rowIndex, COL_DATEFROM, ServiceDate.ToShortDateString());
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }
        public void SetServiceLineProvider(int rowIndex, Int64 ProviderID, string Provider)
        {
            try
            {
                if (c1Transaction != null && c1Transaction.Rows.Count > 0)
                {
                    for (rowIndex = 1; rowIndex <= c1Transaction.Rows.Count - 1; rowIndex++)
                    {
                        c1Transaction.SetData(rowIndex, COL_PROVIDER_ID, ProviderID);
                        c1Transaction.SetData(rowIndex,COL_PROVIDER,Provider);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }
        public void SetServiceLineDateForMissingCharge(int rowIndex, DateTime ServiceDate)
        {
            try
            {
                if (c1Transaction != null && c1Transaction.Rows.Count > 0)
                {
                    for (rowIndex = 1; rowIndex <= c1Transaction.Rows.Count - 1; rowIndex++)
                    {
                        c1Transaction.SetData(rowIndex, COL_DATEFROM, ServiceDate.ToShortDateString());
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        public void AddInsurance(int rowIndex, Int64 InsuranceId, string InsuranceName, Int32 PaymentMode_InsSelfMode)
        {
            try
            {
                if (c1Transaction != null && c1Transaction.Rows.Count > 0)
                {
                    if (rowIndex > 0)
                    {
                        c1Transaction.SetData(rowIndex, COL_INSURANCEID, InsuranceId);
                        c1Transaction.SetData(rowIndex, COL_INSURANCENAME, InsuranceName);
                        c1Transaction.SetData(rowIndex, COL_INSSELF_PAYMODE, PaymentMode_InsSelfMode);

                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        public bool AddLinePrimaryDx(int rowIndex, string PrimaryDxCode, string PrimaryDxDesc)
        {
            bool _isLineDxSet = true;

            try
            {
                if (c1Transaction != null && c1Transaction.Rows.Count > 0)
                {
                    if (rowIndex > 0)
                    {
                        bool _isDxIsInLine = false;
                        for (int colIndex = COL_DX1_CODE; colIndex <= COL_DX8_CODE; colIndex = colIndex + 3)
                        {
                            if (c1Transaction.GetData(rowIndex, colIndex) != null && Convert.ToString(c1Transaction.GetData(rowIndex, colIndex)).Trim() != ""
                                && Convert.ToString(c1Transaction.GetData(rowIndex, colIndex)).Trim() == PrimaryDxCode.Trim())
                            {
                                _isDxIsInLine = true;
                                break;
                            }
                        }

                        if (_isDxIsInLine)
                        {
                            c1Transaction.SetData(rowIndex, COL_LINEPRIMARY_DXCODE, PrimaryDxCode);
                            c1Transaction.SetData(rowIndex, COL_LINEPRIMARY_DXDESC, PrimaryDxDesc);
                        }
                        else
                        {
                            MessageBox.Show("Cannot set primary diagnosis as diagnosis not present in selected line. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _isLineDxSet = false;
                        }
                    }
                }

                //commented on 20100522 for Fixing the Primary Dx Pointer Getting Removed
                //onGrid_SelChanged(null, null);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            return _isLineDxSet;
        }

        public bool IsDxInLine(int rowIndex, string PrimaryDxCode, string PrimaryDxDesc)
        {
            bool _isDxIsInLine = false;

            try
            {
                if (c1Transaction != null && c1Transaction.Rows.Count > 0)
                {
                    if (rowIndex > 0)
                    {
                        for (int colIndex = COL_DX1_CODE; colIndex <= COL_DX8_CODE; colIndex = colIndex + 3)
                        {
                            if (c1Transaction.GetData(rowIndex, colIndex) != null && Convert.ToString(c1Transaction.GetData(rowIndex, colIndex)).Trim() != ""
                                && Convert.ToString(c1Transaction.GetData(rowIndex, colIndex)).Trim() == PrimaryDxCode.Trim())
                            {
                                _isDxIsInLine = true;
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

            return _isDxIsInLine;
        }

        public void RemoveLinePrimaryDx(int rowIndex)
        {
            try
            {
                if (c1Transaction != null && c1Transaction.Rows.Count > 0)
                {
                    if (rowIndex > 0)
                    {
                        c1Transaction.SetData(rowIndex, COL_LINEPRIMARY_DXCODE, "");
                        c1Transaction.SetData(rowIndex, COL_LINEPRIMARY_DXDESC, "");
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        public bool ValidateInsurance(out int rowIndex)
        {
            bool _result = true;
            int _retindex = 1;

            try
            {
                if (c1Transaction != null && c1Transaction.Rows.Count > 0)
                {
                    for (int i = 1; i <= c1Transaction.Rows.Count - 1; i++)
                    {
                        if (c1Transaction.GetData(i, COL_INSSELF_PAYMODE) == null)
                        {
                            _result = false;
                            _retindex = i;
                            break;
                        }
                        else if ((PayerMode)Convert.ToInt32(c1Transaction.GetData(i, COL_INSSELF_PAYMODE).ToString()) == PayerMode.None)
                        {
                            _result = false;
                            _retindex = i;
                            break;
                        }
                        else
                        {
                            if ((PayerMode)Convert.ToInt32(c1Transaction.GetData(i, COL_INSSELF_PAYMODE).ToString()) == PayerMode.Insurance)
                            {
                                if (c1Transaction.GetData(i, COL_INSURANCEID) == null)
                                {
                                    _result = false;
                                    _retindex = i;
                                    break;
                                }
                                else if (c1Transaction.GetData(i, COL_INSURANCEID).ToString().Trim() == "")
                                {
                                    _result = false;
                                    _retindex = i;
                                    break;
                                }
                                else if (Convert.ToInt64(c1Transaction.GetData(i, COL_INSURANCEID).ToString().Trim()) <= 0)
                                {
                                    _result = false;
                                    _retindex = i;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            rowIndex = _retindex;
            return _result;
        }
        //Code Added for self option availability on lines as per current Resposibility -Sameer 11-15-2013
        public void SplitClaimToPatientSetting(Boolean flag)
        {
            if (_IsOpenForModify == false && _showSplitClaimToPatient == true)
            {
                c1Transaction.Cols[COL_SELFCLAIM].Visible = flag;
            }
        }
        //Code Added for self option availability
        //private void MoveNext()
        //{
        //    int _NextColumn = 0;
        //    try
        //    {
        //        switch (CurrentColumn)
        //        {
        //            case COL_POSCODE  : { _NextColumn = COL_TOSCODE;}   break;
        //            case COL_TOSCODE  : { _NextColumn = COL_CPT_CODE;}  break;
        //            case COL_CPT_CODE : { _NextColumn = COL_DX1_CODE;}  break;
        //            case COL_DX1_CODE : { _NextColumn = COL_DX2_CODE;}  break;
        //            case COL_DX2_CODE : { _NextColumn = COL_DX3_CODE;}  break;
        //            case COL_DX3_CODE : { _NextColumn = COL_DX4_CODE;}  break;
        //            case COL_DX4_CODE : { _NextColumn = COL_MOD1_CODE;} break;
        //            case COL_MOD1_CODE: { _NextColumn = COL_MOD2_CODE; } break;
        //            case COL_MOD2_CODE: { _NextColumn = COL_CHARGES; } break;
        //            case COL_CHARGES  : { _NextColumn = COL_UNIT; } break;
        //            case COL_UNIT     : { _NextColumn = COL_ALLOWED; } break;
        //            case COL_ALLOWED  : { _NextColumn = COL_PROVIDER; } break;
        //            case COL_PROVIDER : { _NextColumn = COL_INSURANCENAME; } break;
        //        }

        //        if (_NextColumn > 0)
        //        {
        //            c1Transaction.Select(CurrentTransactionLine, _NextColumn);
        //            c1Transaction.ColSel = _NextColumn;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}

        private void MoveNext()
        {
            int _NextColumn = 0;
            try
            {
                switch (CurrentColumn)
                {
                    case COL_POSCODE: { _NextColumn = COL_CPT_CODE; } break;
                    case COL_TOSCODE: { _NextColumn = COL_CPT_CODE; } break;
                    case COL_CPT_CODE: { _NextColumn = COL_DX1_CODE; } break;
                    case COL_DX1_CODE: { _NextColumn = COL_DX2_CODE; } break;
                    case COL_DX2_CODE: { _NextColumn = COL_DX3_CODE; } break;
                    case COL_DX3_CODE: { _NextColumn = COL_DX4_CODE; } break;
                    case COL_DX4_CODE: { _NextColumn = COL_MOD1_CODE; } break;
                    case COL_MOD1_CODE: { _NextColumn = COL_MOD2_CODE; } break;
                    case COL_MOD2_CODE:
                        {
                            if (_NoOfModifiers > 2) { _NextColumn = COL_MOD3_CODE; }
                            else { _NextColumn = COL_CHARGES; }
                        } break;
                    case COL_MOD3_CODE:
                        {
                            if (_NoOfModifiers > 3) { _NextColumn = COL_MOD4_CODE; }
                            else { _NextColumn = COL_CHARGES; }
                        } break;
                    case COL_MOD4_CODE: { _NextColumn = COL_CHARGES; } break;
                    case COL_CHARGES: { _NextColumn = COL_UNIT; } break;
                    case COL_UNIT: { _NextColumn = COL_ALLOWED; } break;
                    case COL_ALLOWED: { _NextColumn = COL_PROVIDER; } break;
                    case COL_PROVIDER:
                        {
                            if (_showInsurance == true)
                            { _NextColumn = COL_INSURANCENAME; }
                            else
                            { _NextColumn = COL_ISLABCPT; }
                        }
                        break;
                    case COL_INSURANCENAME:
                        {
                            if (ShowLabColumn)
                            { _NextColumn = COL_ISLABCPT; }
                        } break;
                    case COL_ISLABCPT: { _NextColumn = COL_AUTHORIZATIONNO; } break;

                }

                if (_NextColumn > 0)
                {
                    c1Transaction.Select(CurrentTransactionLine, _NextColumn);
                    c1Transaction.ColSel = _NextColumn;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        public void SelectTransactionLine(int rowIndex)
        {
            try
            {
                c1Transaction.Focus();
                c1Transaction.RowSel = rowIndex;
                c1Transaction.Select(rowIndex, COL_CPT_CODE);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            { }
        }

        private void GetDefaultTOSPOS_Old()
        {
            try
            {
                CLsBL_TOSPOS oTOSPOS = new CLsBL_TOSPOS(_DatabaseConnectionString);
                DataTable dt;

                dt = oTOSPOS.GetTOS(_DefaultTOSID);
                if (dt != null && dt.Rows.Count > 0)
                {
                    _DefaultTOSCode = Convert.ToString(dt.Rows[0]["sTOSCode"]);
                    _DefaultTOSDesc = Convert.ToString(dt.Rows[0]["sDescription"]);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (Convert.ToString(dt.Rows[i]["sTOSCode"]).Trim() == "9")
                        {
                            _DefaultTOSCode = Convert.ToString(dt.Rows[i]["sTOSCode"]);
                            _DefaultTOSDesc = Convert.ToString(dt.Rows[i]["sDescription"]);
                            break;
                        }
                    }
                }
                else
                {
                    _DefaultTOSCode = "";
                    _DefaultTOSDesc = "";
                }
                dt = null;



                //dt = oTOSPOS.GetPOS(_POSName);

                //if (dt == null || dt.Rows.Count == 0)
                //{
                //    dt = oTOSPOS.GetPOS(_DefaultPOSID);
                //}

                dt = oTOSPOS.GetPOS(_DefaultPOSID);

                if (dt == null || dt.Rows.Count == 0)
                {
                    dt = oTOSPOS.GetPOS(_POSName);
                }


                if (dt != null && dt.Rows.Count > 0)
                {
                    _DefaultPOSCode = Convert.ToString(dt.Rows[0]["sPOSCode"]);
                    _DefaultPOSDesc = Convert.ToString(dt.Rows[0]["sPOSName"]);
                }
                else
                {
                    _DefaultPOSCode = "";
                    _DefaultPOSDesc = "";
                }
                dt = null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }

        private void GetDefaultTOSPOS()
        {
            DataTable dt = null;
            DataRow[] _dr = null;

            try
            {
                dt = gloGlobal.gloPMMasters.GetTypeOfServices();

                if (dt != null && dt.Rows.Count > 0)
                {
                    _DefaultTOSCode = Convert.ToString(dt.Rows[0]["sTOSCode"]);
                    _DefaultTOSDesc = Convert.ToString(dt.Rows[0]["sDescription"]);

                    ////...need to verify with this logic 
                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                    //    if (Convert.ToString(dt.Rows[i]["sTOSCode"]).Trim() == "9")
                    //    {
                    //        _DefaultTOSCode = Convert.ToString(dt.Rows[i]["sTOSCode"]);
                    //        _DefaultTOSDesc = Convert.ToString(dt.Rows[i]["sDescription"]);
                    //        break;
                    //    }
                    //}

                    _dr = dt.Select("sTOSCode = '9'");
                    if (_dr != null && _dr.Length > 0)
                    {
                        if (_dr[0]["sTOSCode"] != DBNull.Value && Convert.ToString(_dr[0]["sTOSCode"]).Trim() != "")
                        {
                            _DefaultTOSCode = Convert.ToString(_dr[0]["sTOSCode"]);
                            _DefaultTOSDesc = Convert.ToString(_dr[0]["sDescription"]);
                        }
                    }


                }
                else
                {
                    _DefaultTOSCode = "";
                    _DefaultTOSDesc = "";
                }

                dt = null;
                dt = gloGlobal.gloPMMasters.GetPlaceOfServices();

                if (dt != null && dt.Rows.Count > 0)
                {
                    _dr = null;
                    _dr = dt.Select("nPOSID = " + _DefaultPOSID + "");

                    if (_dr.Length > 0)
                    {
                        if (_dr[0]["sPOSCode"] != DBNull.Value && Convert.ToString(_dr[0]["sPOSCode"]).Trim() != "")
                        {
                            _DefaultPOSCode = Convert.ToString(_dr[0]["sPOSCode"]).Trim();
                            _DefaultPOSDesc = Convert.ToString(_dr[0]["sPOSName"]).Trim();
                        }
                    }
                    else
                    {
                        _dr = dt.Select("sPOSName = '" + _POSName + "'");
                        if (_dr.Length > 0)
                        {
                            if (_dr[0]["sPOSCode"] != DBNull.Value && Convert.ToString(_dr[0]["sPOSCode"]).Trim() != "")
                            {
                                _DefaultPOSCode = Convert.ToString(_dr[0]["sPOSCode"]).Trim();
                                _DefaultPOSDesc = Convert.ToString(_dr[0]["sPOSName"]).Trim();
                            }
                        }
                    }
                    _dr = null;
                }
                else
                {
                    _DefaultPOSCode = "";
                    _DefaultPOSDesc = "";
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (dt != null) { dt.Dispose(); }
            }
        }

        public bool HasInsurance(int rowIndex)
        {
            bool _HasInsurance = false;

            try
            {
                if (c1Transaction != null && c1Transaction.Rows.Count > 0 && rowIndex > 0)
                {
                    if (rowIndex < c1Transaction.Rows.Count)
                    {
                        if (Convert.ToString(c1Transaction.GetData(rowIndex, COL_INSURANCEID)) != "")
                        {
                            _HasInsurance = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _HasInsurance = false;
            }
            finally
            { }
            return _HasInsurance;
        }

        public bool HasPrimaryDx(int rowIndex)
        {
            bool _HasPrimaryDx = false;

            try
            {
                if (c1Transaction != null && c1Transaction.Rows.Count > 0 && rowIndex > 0)
                {
                    if (rowIndex < c1Transaction.Rows.Count)
                    {
                        if (Convert.ToString(c1Transaction.GetData(rowIndex, COL_LINEPRIMARY_DXCODE)).Trim() != "")
                        {
                            _HasPrimaryDx = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _HasPrimaryDx = false;
            }
            finally
            { }
            return _HasPrimaryDx;
        }

        private bool OpenInternalControl(gloGridListControlType ControlType, string ControlHeader, bool IsMultiSelect, int RowIndex, int ColIndex)
        {
            bool _result = false;
            try
            {
                _dxCodeForDistinct = "";
                _dxDescriptionForDistinct = "";

                if (ogloGridListControl != null)
                {
                    CloseInternalControl();
                }
                ogloGridListControl = new gloGridListControl(ControlType, false, pnlInternalControl.Width, RowIndex, ColIndex);
                ogloGridListControl.ItemSelected += new gloGridListControl.Item_Selected(ogloGridListControl_ItemSelected);
                ogloGridListControl.InternalGridKeyDown += new gloGridListControl.Key_Down(ogloGridListControl_InternalGridKeyDown);
                ogloGridListControl.ControlHeader = ControlHeader;
                pnlInternalControl.Controls.Add(ogloGridListControl);
                ogloGridListControl.Dock = DockStyle.Fill;
                ogloGridListControl.Show();
                int _x = c1Transaction.Cols[ColIndex].Left;
                int _y = c1Transaction.Rows[RowIndex].Bottom;
                int _width = pnlInternalControl.Width;
                int _height = pnlInternalControl.Height;
                int _diffFactor = Screen.PrimaryScreen.Bounds.Width - _x;

                if (_diffFactor < _width)
                {
                    _x = _x - _diffFactor;
                    pnlInternalControl.SetBounds(_x, _y, 0, 0, BoundsSpecified.Location);
                }
                else
                {
                    pnlInternalControl.SetBounds(_x, _y, 0, 0, BoundsSpecified.Location);
                }
                pnlInternalControl.Visible = true;
                pnlInternalControl.BringToFront();
                ogloGridListControl.Focus();
                _result = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _result = false;
            }
            finally
            {

            }
            return _result;
        }

        private bool OpenInternalControl(gloGridListControlType ControlType, string ControlHeader, bool IsMultiSelect, int RowIndex, int ColIndex, string SearchText)
        {
            bool _result = false;
            try
            {
                // COL_ALLOWED
                if (c1Transaction.ColSel == COL_PROVIDER)
                {
                    if (c1Transaction.Cols[COL_ALLOWED].Visible == true)
                    {
                        pnlInternalControl.Width = c1Transaction.Cols[COL_PROVIDER].Width;
                    }
                    else if (c1Transaction.Cols[COL_ISLABCPT].Visible == false)
                    {
                        pnlInternalControl.Width = c1Transaction.Width - c1Transaction.Cols[ColIndex].Left; //c1Transaction.Cols[COL_PROVIDER].Width ;//337;                                    
                    }
                    else
                    {
                        pnlInternalControl.Width = c1Transaction.Cols[COL_PROVIDER].Width;
                    }
                }
                else
                {
                    pnlInternalControl.Width = INITIAL_INNERCONTROL_WIDTH;
                }

                string test = c1Transaction.Cols[COL_PROVIDER].Width.ToString();
                _dxCodeForDistinct = "";
                _dxDescriptionForDistinct = "";

                if (ogloGridListControl != null)
                {
                    CloseInternalControl();
                }

                if (ColIndex == COL_PROVIDER)
                {

                    ogloGridListControl = new gloGridListControl(ControlType, false, pnlInternalControl.Width, RowIndex, ColIndex);
                    //ogloGridListControl = new gloGridListControl(ControlType, false, pnlInternalControl.Width, RowIndex, ColIndex);
                    ogloGridListControl.ItemSelected += new gloGridListControl.Item_Selected(ogloGridListControl_ItemSelected);
                    ogloGridListControl.InternalGridKeyDown += new gloGridListControl.Key_Down(ogloGridListControl_InternalGridKeyDown);
                    ogloGridListControl.ControlHeader = ControlHeader;
                    //pnlInternalControl.Width = c1Transaction.Width - c1Transaction.Cols[ColIndex].Left;
                    if (c1Transaction.Cols[COL_ALLOWED].Visible == true)
                    {
                        pnlInternalControl.Width = c1Transaction.Cols[COL_PROVIDER].Width;
                    }
                    else if (c1Transaction.Cols[COL_ISLABCPT].Visible == false)
                    {
                        pnlInternalControl.Width = c1Transaction.Width - c1Transaction.Cols[ColIndex].Left; //c1Transaction.Cols[COL_PROVIDER].Width ;//337;                                    
                    }
                    else
                    {
                        pnlInternalControl.Width = c1Transaction.Cols[COL_PROVIDER].Width;
                    }
                    pnlInternalControl.Controls.Add(ogloGridListControl);
                    ogloGridListControl.Dock = DockStyle.Fill;

                }
                else
                {
                    ogloGridListControl = new gloGridListControl(ControlType, false, pnlInternalControl.Width, RowIndex, ColIndex);
                    ogloGridListControl.ItemSelected += new gloGridListControl.Item_Selected(ogloGridListControl_ItemSelected);
                    ogloGridListControl.InternalGridKeyDown += new gloGridListControl.Key_Down(ogloGridListControl_InternalGridKeyDown);
                    ogloGridListControl.ControlHeader = ControlHeader;
                    pnlInternalControl.Controls.Add(ogloGridListControl);
                    ogloGridListControl.Dock = DockStyle.Fill;
                }
                if (SearchText != "")
                {
                    ogloGridListControl.Search(SearchText, SearchColumn.Code);
                }
                ogloGridListControl.Show();

                int _x = c1Transaction.Cols[ColIndex].Left;

                int _y = c1Transaction.Rows[RowIndex].Bottom;
                int _width = pnlInternalControl.Width;
                int _height = pnlInternalControl.Height;
                int _parentleft = this.Parent.Bounds.Left;
                int _parentwidth = this.Parent.Bounds.Width;
                int _diffFactor = _parentwidth - _x;

                if (_diffFactor < _width)
                {
                    _x = this.Parent.Bounds.Width + (_diffFactor);
                    pnlInternalControl.SetBounds(_x, _y, 0, 0, BoundsSpecified.Location);
                }
                else
                {
                    pnlInternalControl.SetBounds(_x, _y, 0, 0, BoundsSpecified.Location);
                }

                //pnlInternalControl.SetBounds(c1Transaction.Cols[ColIndex].Left, c1Transaction.Rows[RowIndex].Bottom, 0, 0, BoundsSpecified.Location);
                pnlInternalControl.Visible = true;
                pnlInternalControl.BringToFront();
                ogloGridListControl.Focus();
                _result = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _result = false;
            }
            finally
            {
                RePositionInternalControl();
            }
            return _result;
        }

        private bool OpenInternalControl(gloGridListControlType ControlType, string ControlHeader, bool IsMultiSelect, int RowIndex, int ColIndex, string SearchText, string DxCode, string DxDescription, bool IsDiagnosis)
        {
            bool _result = false;
            try
            {
                _dxCodeForDistinct = DxCode;
                _dxDescriptionForDistinct = DxDescription;

                if (ogloGridListControl != null)
                {
                    CloseInternalControl();
                }
                ogloGridListControl = new gloGridListControl(ControlType, false, pnlInternalControl.Width, RowIndex, ColIndex);
                ogloGridListControl.ItemSelected += new gloGridListControl.Item_Selected(ogloGridListControl_ItemSelected);
                ogloGridListControl.InternalGridKeyDown += new gloGridListControl.Key_Down(ogloGridListControl_InternalGridKeyDown);
                ogloGridListControl.ControlHeader = ControlHeader;
                if (IcdCodeType == gloGlobal.gloICD.CodeRevision.ICD10.GetHashCode())
                {
                    ogloGridListControl.IcdCodeType = gloGlobal.gloICD.CodeRevision.ICD10.GetHashCode();
                }
                else
                {
                    ogloGridListControl.IcdCodeType = gloGlobal.gloICD.CodeRevision.ICD9.GetHashCode();
                }
                pnlInternalControl.Controls.Add(ogloGridListControl);
                ogloGridListControl.Dock = DockStyle.Fill;
                if (SearchText != "")
                {
                    ogloGridListControl.Search(SearchText, SearchColumn.Code);
                }
                ogloGridListControl.Show();

                int _x = c1Transaction.Cols[ColIndex].Left;
                int _y = c1Transaction.Rows[RowIndex].Bottom;
                int _width = pnlInternalControl.Width;
                int _height = pnlInternalControl.Height;
                int _parentleft = this.Parent.Bounds.Left;
                int _parentwidth = this.Parent.Bounds.Width;
                int _diffFactor = _parentwidth - _x;

                if (_diffFactor < _width)
                {
                    _x = this.Parent.Bounds.Width + (_diffFactor);
                    pnlInternalControl.SetBounds(_x, _y, 0, 0, BoundsSpecified.Location);
                }
                else
                {
                    pnlInternalControl.SetBounds(_x, _y, 0, 0, BoundsSpecified.Location);
                }

                //pnlInternalControl.SetBounds(c1Transaction.Cols[ColIndex].Left, c1Transaction.Rows[RowIndex].Bottom, 0, 0, BoundsSpecified.Location);
                pnlInternalControl.Visible = true;
                pnlInternalControl.BringToFront();
                ogloGridListControl.Focus();
                _result = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _result = false;
            }
            finally
            {
                RePositionInternalControl();
            }
            return _result;
        }

        private bool OpenInternalControl(gloGridListControlType ControlType, string ControlHeader, bool IsMultiSelect, int RowIndex, int ColIndex, string SearchText, string CPTCode, string FacilityCode)
        {
            bool _result = false;
            try
            {
                _dxCodeForDistinct = "";
                _dxDescriptionForDistinct = "";

                if (ogloGridListControl != null)
                {
                    CloseInternalControl();
                }
                ogloGridListControl = new gloGridListControl(ControlType, false, pnlInternalControl.Width, RowIndex, ColIndex);
                ogloGridListControl.ItemSelected += new gloGridListControl.Item_Selected(ogloGridListControl_ItemSelected);
                ogloGridListControl.InternalGridKeyDown += new gloGridListControl.Key_Down(ogloGridListControl_InternalGridKeyDown);
                ogloGridListControl.ControlHeader = ControlHeader;
                pnlInternalControl.Controls.Add(ogloGridListControl);
                ogloGridListControl.Dock = DockStyle.Fill;
                ogloGridListControl.SelectedCPTCode = CPTCode;
                ogloGridListControl.SelectedFacilityCode = FacilityCode;
                if (SearchText != "")
                {
                    ogloGridListControl.Search(SearchText, SearchColumn.Code);
                }
                ogloGridListControl.Show();

                int _x = c1Transaction.Cols[ColIndex].Left;
                int _y = c1Transaction.Rows[RowIndex].Bottom;
                int _width = pnlInternalControl.Width;
                int _height = pnlInternalControl.Height;
                int _parentleft = this.Parent.Bounds.Left;
                int _parentwidth = this.Parent.Bounds.Width;
                int _diffFactor = _parentwidth - _x;

                if (_diffFactor < _width)
                {
                    _x = this.Parent.Bounds.Width + (_diffFactor);
                    pnlInternalControl.SetBounds(_x, _y, 0, 0, BoundsSpecified.Location);
                }
                else
                {
                    pnlInternalControl.SetBounds(_x, _y, 0, 0, BoundsSpecified.Location);
                }

                //pnlInternalControl.SetBounds(c1Transaction.Cols[ColIndex].Left, c1Transaction.Rows[RowIndex].Bottom, 0, 0, BoundsSpecified.Location);
                pnlInternalControl.Visible = true;
                pnlInternalControl.BringToFront();
                ogloGridListControl.Focus();
                _result = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _result = false;
            }
            finally
            {
                RePositionInternalControl();
            }
            return _result;
        }

        private bool CloseInternalControl()
        {
            bool _result = false;
            try
            {
                //SLR: Changed on 2/4/2014
                for (int i = pnlInternalControl.Controls.Count - 1; i >= 0; i--)
                {
                    pnlInternalControl.Controls.RemoveAt(i);
                }
                if (ogloGridListControl != null)
                {
                    try
                    {
                        ogloGridListControl.ItemSelected -= new gloGridListControl.Item_Selected(ogloGridListControl_ItemSelected);
                        ogloGridListControl.InternalGridKeyDown -= new gloGridListControl.Key_Down(ogloGridListControl_InternalGridKeyDown);

                        if (tmrChangeEditSearch != null)
                        {
                            tmrChangeEditSearch.Stop();
                        }
                    }
                    catch { }
                    ogloGridListControl.Dispose();
                    ogloGridListControl = null;
                }
                pnlInternalControl.Visible = false;
                pnlInternalControl.SendToBack();
                _result = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _result = false;
            }
            finally
            { }
            return _result;
        }

        private void RePositionInternalControl()
        {
            try
            {
                if (pnlInternalControl.Visible == true && ogloGridListControl != null)
                {
                    //if (c1Transaction.Rows[CurrentTransactionLine].Bottom + c1Transaction.ScrollPosition.Y > 220)
                    //{
                    //    pnlInternalControl.SetBounds((c1Transaction.Cols[CurrentColumn].Left + c1Transaction.ScrollPosition.X), (c1Transaction.Rows[CurrentTransactionLine].Bottom + c1Transaction.ScrollPosition.Y - 230), 0, 0, BoundsSpecified.Location);
                    //}
                    //else
                    //{
                    //    pnlInternalControl.SetBounds((c1Transaction.Cols[CurrentColumn].Left + c1Transaction.ScrollPosition.X), (c1Transaction.Rows[CurrentTransactionLine].Bottom + c1Transaction.ScrollPosition.Y), 0, 0, BoundsSpecified.Location);
                    //}

                    if ((this.Bottom - c1Transaction.Rows[CurrentTransactionLine].Bottom) - c1Transaction.ScrollPosition.Y > (c1Transaction.Rows[CurrentTransactionLine].Top - this.Top) + c1Transaction.ScrollPosition.Y)
                    {
                        if ((this.Bottom - c1Transaction.Rows[CurrentTransactionLine].Bottom) - c1Transaction.ScrollPosition.Y < pnlInternalControl.Height) { pnlInternalControl.Height = (this.Bottom - c1Transaction.Rows[CurrentTransactionLine].Bottom) - c1Transaction.ScrollPosition.Y; }
                        //pnlInternalControl.Height = (this.Bottom - c1Transaction.Rows[CurrentTransactionLine].Bottom) - c1Transaction.ScrollPosition.Y;
                        pnlInternalControl.SetBounds((c1Transaction.Cols[CurrentColumn].Left + c1Transaction.ScrollPosition.X), (c1Transaction.Rows[CurrentTransactionLine].Bottom + c1Transaction.ScrollPosition.Y), 0, 0, BoundsSpecified.Location);

                    }
                    else
                    {
                        if ((c1Transaction.Rows[CurrentTransactionLine].Top - this.Top) + c1Transaction.ScrollPosition.Y < pnlInternalControl.Height) { pnlInternalControl.Height = (c1Transaction.Rows[CurrentTransactionLine].Top - this.Top) + c1Transaction.ScrollPosition.Y; }
                        //pnlInternalControl.Height = (c1Transaction.Rows[CurrentTransactionLine].Top - this.Top) - c1Transaction.ScrollPosition.Y;
                        pnlInternalControl.SetBounds((c1Transaction.Cols[CurrentColumn].Left + c1Transaction.ScrollPosition.X), (c1Transaction.Rows[CurrentTransactionLine].Top - pnlInternalControl.Height) + c1Transaction.ScrollPosition.Y, 0, 0, BoundsSpecified.Location);
                    }

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void SetCurrencyCellValue(int rowIndex)
        {
            try
            {
                if (c1Transaction != null && c1Transaction.Rows.Count > 0)
                {
                    if (rowIndex > 0 && rowIndex < c1Transaction.Rows.Count)
                    {
                        c1Transaction.SetData(rowIndex, COL_CHARGES, 0.00);
                        c1Transaction.SetData(rowIndex, COL_TOTAL, 0.00);


                        c1Transaction.SetData(rowIndex, COL_UNIT, 1);
                        //c1Transaction.SetData(rowIndex, COL_ACTUAL_ALLOWED, 0.00);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        //public void SortControl()
        //{
        //    try
        //    {
        //        //...** Check if allowed column is visible if yes sort amount on allowed
        //        //...** or else sort on total amount
        //        if (_showAllowedColumn == true)
        //        { c1Transaction.Sort(SortFlags.Descending, COL_ALLOWED); }
        //        else
        //        { c1Transaction.Sort(SortFlags.Descending, COL_TOTAL); }

        //        //_isLastAddedRowSorted = true;
        //        //_sortedRowIndex = c1Transaction.RowSel;

        //        //Code added on 20090710 to re index line no for display purpose - Vinayak Gadekar
        //        for (int i = 1; i <= c1Transaction.Rows.Count - 1; i++)
        //        {
        //            c1Transaction.SetData(i, COL_NO, i.ToString());
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
        //    }
        //    finally
        //    {
        //    }
        //}

        public void SortControl()
        {
            try
            {
                c1Transaction.DragMode = DragModeEnum.AutomaticMove;
                c1Transaction.AllowDragging = AllowDraggingEnum.Rows;

                if (_AutoSort == true)
                {
                    if (_showAllowedColumn == true)
                    {
                        //c1Transaction.Sort(SortFlags.Descending, COL_ALLOWED); 
                        c1Transaction.Sort(SortFlags.Descending, COL_TOTAL);
                    }
                    else
                    {
                        c1Transaction.Sort(SortFlags.Descending, COL_TOTAL);
                    }
                }
                //Code added on 20090710 to re index line no for display purpose - Vinayak Gadekar
                for (int i = 1; i <= c1Transaction.Rows.Count - 1; i++)
                {
                    c1Transaction.SetData(i, COL_NO, i.ToString());
                }
                getfirstservicelineCLIA();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
            }
        }

        public void ShiftRecords(int rowIndex, int ColCodeIndex)
        {
            //CellNote oCellNote = null;
            //CellRange rg;

            try
            {
                int _colDxPointer = 37; //default to first pointer



                if (ColCodeIndex >= COL_DX1_CODE && ColCodeIndex <= COL_DX8_CODE)
                {
                    #region "Find Pointer Counter"
                    switch (ColCodeIndex)
                    {
                        case COL_DX1_CODE: _colDxPointer = COL_DX1_PTR; break;
                        case COL_DX2_CODE: _colDxPointer = COL_DX2_PTR; break;
                        case COL_DX3_CODE: _colDxPointer = COL_DX3_PTR; break;
                        case COL_DX4_CODE: _colDxPointer = COL_DX4_PTR; break;
                        case COL_DX5_CODE: _colDxPointer = COL_DX5_PTR; break;
                        case COL_DX6_CODE: _colDxPointer = COL_DX6_PTR; break;
                        case COL_DX7_CODE: _colDxPointer = COL_DX7_PTR; break;
                        case COL_DX8_CODE: _colDxPointer = COL_DX8_PTR; break;
                    }
                    _colDxPointer = _colDxPointer - 1;
                    #endregion

                    for (int i = ColCodeIndex + 3; i <= COL_DX8_CODE; i += 3)
                    {
                        c1Transaction.SetData(rowIndex, i - 3, Convert.ToString(c1Transaction.GetData(rowIndex, i)));
                        c1Transaction.SetData(rowIndex, i - 2, Convert.ToString(c1Transaction.GetData(rowIndex, i + 1)));

                        //if (c1Transaction[rowIndex, i - 2] != null && Convert.ToString(c1Transaction[rowIndex, i - 2]) != "")
                        //{
                        //    oCellNote = new CellNote(c1Transaction[rowIndex, i - 2].ToString());
                        //    if (oCellNote.Text.Trim() != "")
                        //    {
                        //        rg = c1Transaction.GetCellRange(rowIndex, i - 3);
                        //        rg.UserData = oCellNote;
                        //    }
                        //}

                        if (Convert.ToString(c1Transaction.GetData(rowIndex, i - 3)).Trim() != "")
                        {
                            c1Transaction.SetCellCheck(rowIndex, _colDxPointer + 1, CheckEnum.Checked);
                        }


                        c1Transaction.SetData(rowIndex, i, "");
                        c1Transaction.SetData(rowIndex, i + 1, "");
                        //oCellNote = null;
                        //rg = c1Transaction.GetCellRange(rowIndex, i);
                        //rg.UserData = oCellNote;

                        c1Transaction.SetCellCheck(rowIndex, _colDxPointer + 2, CheckEnum.Unchecked);
                        _colDxPointer = _colDxPointer + 1;
                    }
                }
                else if (ColCodeIndex >= COL_MOD1_CODE && ColCodeIndex <= COL_MOD4_CODE)
                {
                    for (int i = ColCodeIndex + 3; i <= COL_MOD4_CODE; i += 3)
                    {
                        c1Transaction.SetData(rowIndex, i - 3, Convert.ToString(c1Transaction.GetData(rowIndex, i)));
                        c1Transaction.SetData(rowIndex, i - 2, Convert.ToString(c1Transaction.GetData(rowIndex, i + 1)));

                        //if (c1Transaction.GetData(rowIndex, i - 2) != null && Convert.ToString(c1Transaction.GetData(rowIndex, i - 2)) != "")
                        //{
                        //    oCellNote = new CellNote(c1Transaction[rowIndex, i - 2].ToString());
                        //    if (oCellNote.Text.Trim() != "")
                        //    {
                        //        rg = c1Transaction.GetCellRange(rowIndex, i - 3);
                        //        rg.UserData = oCellNote;
                        //    }
                        //}

                        c1Transaction.SetData(rowIndex, i, "");
                        c1Transaction.SetData(rowIndex, i + 1, "");
                        //oCellNote = null;
                        //rg = c1Transaction.GetCellRange(rowIndex, i);
                        //rg.UserData = oCellNote;
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        public decimal GetTotalCharges()
        {
            decimal _totalCharges = 0;
            try
            {
                if (c1Transaction != null && c1Transaction.Rows.Count > 0)
                {
                    for (int i = 1; i <= c1Transaction.Rows.Count - 1; i++)
                    {
                        if (Convert.ToString(c1Transaction.GetData(i, COL_CHARGES)) != "")
                        {
                            _totalCharges = _totalCharges + Convert.ToDecimal(c1Transaction.GetData(i, COL_CHARGES));

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _totalCharges = 0;
            }
            _totalCharges = Convert.ToDecimal(_totalCharges.ToString("#0.00"));
            return _totalCharges;
        }

        public decimal GetTotalAllowed()
        {
            decimal _totalAllowed = 0;
            try
            {
                if (c1Transaction != null && c1Transaction.Rows.Count > 0)
                {
                    for (int i = 1; i <= c1Transaction.Rows.Count - 1; i++)
                    {
                        if (Convert.ToString(c1Transaction.GetData(i, COL_ALLOWED)) != "")
                        {
                            _totalAllowed = _totalAllowed + Math.Round(Convert.ToDecimal(c1Transaction.GetData(i, COL_ALLOWED)), 2, MidpointRounding.AwayFromZero);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _totalAllowed = 0;
            }
            _totalAllowed = Convert.ToDecimal(_totalAllowed.ToString("#0.00"));
            return _totalAllowed;
        }

        public decimal GetGrandTotal()
        {
            decimal _GrandTotal = 0;
            try
            {
                if (c1Transaction != null && c1Transaction.Rows.Count > 0)
                {
                    for (int i = 1; i <= c1Transaction.Rows.Count - 1; i++)
                    {
                        if (Convert.ToString(c1Transaction.GetData(i, COL_TOTAL)) != "")
                        {
                            _GrandTotal = _GrandTotal + Math.Round(Convert.ToDecimal(c1Transaction.GetData(i, COL_TOTAL)), 2, MidpointRounding.AwayFromZero);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _GrandTotal = 0;
            }
            _GrandTotal = Convert.ToDecimal(_GrandTotal.ToString("#0.00"));
            return _GrandTotal;
        }

        private void GetReferralCPTSetting()
        {
            gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_DatabaseConnectionString);
            object value = new object();
            try
            {
                ogloSettings.GetSetting("IsReferralCPT", out value);
                if (value != null && Convert.ToString(value) != "")
                {
                    _IsReferralCPT = Convert.ToBoolean(value);
                    value = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (ogloSettings != null) { ogloSettings.Dispose(); }
                value = null;
            }
        }

        private bool IsReferralRequire(string CPTCode)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
            object value = new object();
            string _strSQL = "";
            bool _result = false;
            try
            {
                oDB.Connect(false);
                //_strSQL = "Select Count(nCPTID) FROM BL_ReferralCPT_MST Where ";
                _strSQL = "select count(nRefCPTID) FROM BL_ReferralCPT_MST WITH(NOLOCK) where sCPTCode = '" + CPTCode.Replace("'", "''") + "' ";
                value = oDB.ExecuteScalar_Query(_strSQL);
                if (value != null)
                {
                    if (Convert.ToInt64(value) > 0)
                    {
                        _result = true;
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                value = null;
            }
            return _result;
        }

        private void DxModified(int rowIndex, int colIndex)
        {

            //     int _id = 0;
            string _code = "";
            string _description = "";
            bool _isdeleted = true;

            //TrnCtrlColValChangeEventArg e2 = new TrnCtrlColValChangeEventArg();
            //RowColEventArgs e1 = null;

            try
            {
                if (rowIndex > 0 && (colIndex >= COL_DX1_CODE && colIndex <= COL_DX8_CODE))
                {

                    if (c1Transaction.GetData(rowIndex, colIndex) != null)
                    {
                        _code = c1Transaction.GetData(rowIndex, colIndex).ToString();
                    }
                    if (c1Transaction.GetData(rowIndex, colIndex + 1) != null)
                    {
                        _description = c1Transaction.GetData(rowIndex, colIndex + 1).ToString();
                    }

                    evtModifyDx.oType = TransactionLineColumnType.Diagnosis;
                    evtModifyDxRowCol = new RowColEventArgs(rowIndex, colIndex);
                    evtModifyDx.code = _code;
                    evtModifyDx.description = _description;
                    evtModifyDx.isdeleted = true;

                    //int _dxcntr = 0;

                    //if (evtModifyDx.oType == TransactionLineColumnType.Diagnosis)
                    //{
                    //    for (int i = 1; i <= c1Transaction.Rows.Count - 1; i++)
                    //    {
                    //        for (int j = COL_DX1_CODE; j <= COL_DX8_CODE; j += 3)
                    //        {
                    //            if (c1Transaction.GetData(i, j) != null && c1Transaction.GetData(i, j).ToString().Trim() != "")
                    //            {
                    //                if (c1Transaction.GetData(i, j).ToString().Trim() == _code.Trim())
                    //                {
                    //                    _dxcntr = _dxcntr + 1;
                    //                    if (_dxcntr >= 1)
                    //                    {
                    //                        _code = "";
                    //                        _description = "";
                    //                        _isdeleted = false;
                    //                        break;
                    //                    }
                    //                }
                    //            }
                    //        }
                    //    }
                    //}


                    evtModifyDx.code = _code;
                    evtModifyDx.description = _description;
                    evtModifyDx.isdeleted = _isdeleted;
                    //onInsCPTDxMod_Changed(null, e1, e2);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void RemoveDxModified()
        {
            //    int _id = 0;
            bool _isdeleted = true;

            try
            {
                int _dxcntr = 0;

                if (evtModifyDx.code.Trim() != "")
                {

                    if (evtModifyDx.oType == TransactionLineColumnType.Diagnosis)
                    {
                        for (int i = 1; i <= c1Transaction.Rows.Count - 1; i++)
                        {
                            for (int j = COL_DX1_CODE; j <= COL_DX8_CODE; j += 3)
                            {
                                if (c1Transaction.GetData(i, j) != null && c1Transaction.GetData(i, j).ToString().Trim() != "")
                                {
                                    if (c1Transaction.GetData(i, j).ToString().Trim() == evtModifyDx.code.Trim())
                                    {
                                        _dxcntr = _dxcntr + 1;
                                        if (_dxcntr >= 1)
                                        {
                                            _isdeleted = false;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (_isdeleted == true)
                    { onInsCPTDxMod_Changed(null, evtModifyDxRowCol, evtModifyDx); }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void SetFacilityCLIA()
        {
            try
            {
                if (c1Transaction != null && c1Transaction.Rows.Count > 0)
                {
                    c1Transaction.BeginUpdate();
                    for (int i = 1; i < c1Transaction.Rows.Count; i++)
                    {
                        if (c1Transaction.GetCellCheck(i, COL_ISLABCPT) == CheckEnum.Checked)
                        {
                            if (c1Transaction.GetData(i, COL_AUTHORIZATIONNO).ToString() == "")
                            {
                                c1Transaction.SetData(i, COL_AUTHORIZATIONNO, FacilityCLIANo);
                            }

                        }
                    }
                    c1Transaction.EndUpdate();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }



        public void SetFacilityPOS(Boolean IsFacilityChangeFromModifyCharges = false)
        {
            int nMatchCount = 0;
            try
            {
                if (c1Transaction != null && c1Transaction.Rows.Count > 0 && FacilityPOS > 0)
                {
                    CLsBL_TOSPOS oPOS = new CLsBL_TOSPOS(_DatabaseConnectionString);

                    DataTable dtPOS = oPOS.GetPOS(FacilityPOS);
                    if (IsFacilityChangeFromModifyCharges)
                    {
                        for (nMatchCount = 1; nMatchCount < c1Transaction.Rows.Count; nMatchCount++)
                        {
                            if (dtPOS != null && dtPOS.Rows.Count > 0)
                            {
                                if (Convert.ToString(dtPOS.Rows[0]["sPOSCode"]) != Convert.ToString(c1Transaction.GetData(nMatchCount, COL_POSCODE)))
                                {
                                    break;
                                }
                            }
                        }

                        //int a = 0;
                        //c1Transaction.GetCellRange(1, COL_POSCODE);
                        //a =c1Transaction.FindRow(Convert.ToString(dtPOS.Rows[0]["sPOSCode"]), 1, COL_POSCODE,true);

                        if (nMatchCount != c1Transaction.Rows.Count)
                        {
                            if (DialogResult.Yes != MessageBox.Show("Facility change requires POS changes in the claim" + Environment.NewLine + Environment.NewLine + "Continue?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1))
                            {
                                return;
                            }

                        }
                    }

                    c1Transaction.BeginUpdate();
                    for (int i = 1; i < c1Transaction.Rows.Count; i++)
                    {

                        if (dtPOS != null && dtPOS.Rows.Count > 0)
                        {
                            c1Transaction.SetData(i, COL_POSCODE, Convert.ToString(dtPOS.Rows[0]["sPOSCode"]));
                            c1Transaction.SetData(i, COL_POSDESC, Convert.ToString(dtPOS.Rows[0]["sPOSName"]));
                        }
                    }
                    c1Transaction.EndUpdate();

                    dtPOS = null;
                    oPOS.Dispose();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }



        public void SetFNFCharges()
        {
            int _rowIndex = 0;
            ClsFeeSchedule oclsFeeSchedule = null;
            try
            {
                oclsFeeSchedule = new ClsFeeSchedule(_DatabaseConnectionString);
                if (c1Transaction != null && c1Transaction.Rows.Count > 0)
                {
                    _rowIndex = this.CurrentTransactionLine;


                    string _whencpt = "";
                    string _whenmod = "";
                    Int64 _wheninsid = 0;
                    Int64 _fromDOS = 0;
                    Int64 _toDOS = 0;
                    decimal _AllowedCharges = 0;
                    decimal _Charges = 0;
                    bool _isAllowedAmount = false;
                    bool _isChargeAmount = false;

                    if (_rowIndex > 0)
                    {

                        _transactionDetailId = Convert.ToInt64(c1Transaction.GetData(_rowIndex, COL_MST_TRANSACTIONDTLID));

                        _whencpt = "";
                        _whenmod = "";
                        _wheninsid = 0;
                        _fromDOS = 0;
                        _toDOS = 0;





                        if (c1Transaction.GetData(_rowIndex, COL_CPT_CODE) != null)
                        {
                            _whencpt = c1Transaction.GetData(_rowIndex, COL_CPT_CODE).ToString();
                        }

                        if (c1Transaction.GetData(_rowIndex, COL_MOD1_CODE) != null && c1Transaction.GetData(_rowIndex, COL_MOD1_CODE).ToString().Length > 0)
                        {
                            _whenmod = c1Transaction.GetData(_rowIndex, COL_MOD1_CODE).ToString();
                        }

                        if (c1Transaction.GetData(_rowIndex, COL_MOD2_CODE) != null && c1Transaction.GetData(_rowIndex, COL_MOD2_CODE).ToString().Length > 0)
                        {
                            if (_whenmod.Trim().Length > 0)
                                _whenmod = _whenmod + "," + c1Transaction.GetData(_rowIndex, COL_MOD2_CODE).ToString();
                            else
                                _whenmod = c1Transaction.GetData(_rowIndex, COL_MOD2_CODE).ToString();
                        }

                        if (c1Transaction.GetData(_rowIndex, COL_MOD3_CODE) != null && c1Transaction.GetData(_rowIndex, COL_MOD3_CODE).ToString().Length > 0)
                        {
                            if (_whenmod.Trim().Length > 0)
                                _whenmod = _whenmod + "," + c1Transaction.GetData(_rowIndex, COL_MOD3_CODE).ToString();
                            else
                                _whenmod = c1Transaction.GetData(_rowIndex, COL_MOD3_CODE).ToString();
                        }

                        if (c1Transaction.GetData(_rowIndex, COL_MOD4_CODE) != null && c1Transaction.GetData(_rowIndex, COL_MOD4_CODE).ToString().Length > 0)
                        {
                            if (_whenmod.Trim().Length > 0)
                                _whenmod = _whenmod + "," + c1Transaction.GetData(_rowIndex, COL_MOD4_CODE).ToString();
                            else
                                _whenmod = c1Transaction.GetData(_rowIndex, COL_MOD4_CODE).ToString();
                        }

                        if (c1Transaction.GetData(_rowIndex, COL_INSURANCEID) != null)
                        {
                            if (Convert.ToString(c1Transaction.GetData(_rowIndex, COL_INSURANCEID)) != "")
                            {
                                _wheninsid = Convert.ToInt64(c1Transaction.GetData(_rowIndex, COL_INSURANCEID).ToString());
                            }
                        }


                        if (c1Transaction.GetData(_rowIndex, COL_DATEFROM) != null)
                        {
                            if (Convert.ToString(c1Transaction.GetData(_rowIndex, COL_DATEFROM)) != "")
                            {
                                _fromDOS = gloDateMaster.gloDate.DateAsNumber(c1Transaction.GetData(_rowIndex, COL_DATEFROM).ToString());
                            }
                        }

                        if (c1Transaction.GetData(_rowIndex, COL_DATETO) != null)
                        {
                            if (Convert.ToString(c1Transaction.GetData(_rowIndex, COL_DATETO)) != "")
                            {
                                _toDOS = gloDateMaster.gloDate.DateAsNumber(c1Transaction.GetData(_rowIndex, COL_DATETO).ToString());
                            }
                        }

                        _AllowedCharges = 0;
                        _Charges = 0;

                        #region "Set defaut Self claim of CPT"
                        if (_IsOpenForModify == false)
                        {
                            _bDefaultSelf = oclsFeeSchedule.GetDefaultSelf(_whencpt);
                            if (_bDefaultSelf == true && _showSplitClaimToPatient == true)
                            {
                                c1Transaction.SetCellCheck(_rowIndex, COL_SELFCLAIM, CheckEnum.Checked);
                            }
                            else
                            {
                                c1Transaction.SetCellCheck(_rowIndex, COL_SELFCLAIM, CheckEnum.Unchecked);
                            }
                        }
                        #endregion

                        if (c1Transaction.GetCellCheck(_rowIndex, COL_SELFCLAIM) == CheckEnum.Checked)
                        {
                            oclsFeeSchedule.GetCPTFees(_whencpt, _whenmod, _transactionDetailId, _fromDOS, _toDOS, _FeeScheduleID, _PatientProviderID, 0, DefaultChargesType.GetHashCode(), out _AllowedCharges, out _isAllowedAmount, out _Charges, out _isChargeAmount, out _Fee_ScheduleID);
                        }
                        else
                        {
                            oclsFeeSchedule.GetCPTFees(_whencpt, _whenmod, _transactionDetailId, _fromDOS, _toDOS, _FeeScheduleID, _PatientProviderID, _nContactID, DefaultChargesType.GetHashCode(), out _AllowedCharges, out _isAllowedAmount, out _Charges, out _isChargeAmount, out _Fee_ScheduleID);
                        }


                        #region " Retrive CPT Charges If Exist Override the Fee Schedule "



                        //decimal _cptcharges = 0;

                        //decimal _cptunits = 0;


                        ////Load Charges from CPT Master if Modifier is Empty and Provider Charges is 0
                        //if (_isChargeAmount == false)
                        //{
                        //    if (oclsFeeSchedule.GetCPTCharges(_whencpt, out _cptcharges, out _cptunits) == true)
                        //    {

                        //        _Fee_ScheduleID = 0;
                        //        _Fee_ScheduleType = FeeScheduleType.CPT;

                        //        c1Transaction.SetData(_rowIndex, COL_CHARGES, _cptcharges);
                        //        _Charges = _cptcharges;

                        //        Decimal _Unit = 0;

                        //        if (c1Transaction.GetData(c1Transaction.RowSel, COL_UNIT) != null && Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_UNIT)).Trim() != "")
                        //        { _Unit = Convert.ToDecimal(c1Transaction.GetData(c1Transaction.RowSel, COL_UNIT)); }

                        //        onCPTCharges_Load(null, this.DefaultChargesType);
                        //    }
                        //}

                        #endregion " Retrive CPT Charges If Exist Override the Fee Schedule "

                        _DefaultCPT_CLIAno = oclsFeeSchedule.GetDefaultCPT_CLIAno(_whencpt);

                        #region "Set Default CLIA# for Selected CPT"
                        if (_DefaultCPT_CLIAno.Trim() != "" && _showLabColumn == true)  //changes for refer admin settings ShowLabColumns Sameer 15-May-2015
                        {
                            c1Transaction.SetCellCheck(_rowIndex, COL_ISLABCPT, CheckEnum.Checked);
                            c1Transaction.SetData(_rowIndex, COL_AUTHORIZATIONNO, _DefaultCPT_CLIAno);
                            c1Transaction.Rows[_rowIndex].UserData = true;
                        }
                        else
                        {
                            c1Transaction.SetCellCheck(_rowIndex, COL_ISLABCPT, CheckEnum.Unchecked);
                            c1Transaction.SetData(_rowIndex, COL_AUTHORIZATIONNO, "");
                        }
                        #endregion






                        //Set Charge Amount
                        //if ( _isChargeAmount == true && ChangeAmount(_rowIndex, _Charges))
                        //{

                        //    c1Transaction.SetData(_rowIndex, COL_CHARGES, _Charges);
                        //}


                        //Set Allowed Amount
                        if (_AllowedCharges > 0)
                        {
                            if (_isAllowedAmount)
                                c1Transaction.SetData(_rowIndex, COL_ACTUAL_ALLOWED, _AllowedCharges);

                            Decimal _Unit = 0;

                            if (c1Transaction.GetData(c1Transaction.RowSel, COL_UNIT) != null && Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_UNIT)).Trim() != "")
                            { _Unit = Convert.ToDecimal(c1Transaction.GetData(c1Transaction.RowSel, COL_UNIT)); }


                            if (_Unit > 0)
                            {
                                _AllowedCharges = _AllowedCharges * _Unit;
                            }

                            if (_isAllowedAmount)
                            {
                                c1Transaction.SetData(_rowIndex, COL_ALLOWED, _AllowedCharges);
                            }
                            else
                            {
                                c1Transaction.SetData(_rowIndex, COL_ACTUAL_ALLOWED, null);
                                c1Transaction.SetData(_rowIndex, COL_ALLOWED, null);
                            }
                        }
                        else
                        {
                            if (_isAllowedAmount)
                            {
                                c1Transaction.SetData(_rowIndex, COL_ALLOWED, _AllowedCharges);
                                c1Transaction.SetData(_rowIndex, COL_ACTUAL_ALLOWED, _AllowedCharges);
                            }
                            else
                            {
                                c1Transaction.SetData(_rowIndex, COL_ALLOWED, null);
                                c1Transaction.SetData(_rowIndex, COL_ACTUAL_ALLOWED, null);
                            }

                        }
                        //Set Allowed Amount-------------------END

                        //Do not change sequence of setting Allowed and Charge Amount
                        //Creates a sorting issue

                        if (_isChargeAmount == true && ChangeAmount(_rowIndex, _Charges))
                        {

                            c1Transaction.SetData(_rowIndex, COL_CHARGES, _Charges);
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
                if (oclsFeeSchedule != null)
                {
                    oclsFeeSchedule.Dispose();
                    oclsFeeSchedule = null;
                }
            }
        }

        private bool ChangeAmount(int _rowIndex, Decimal Charges)
        {
            decimal oldCharges = 0;
            bool _result = true;
            if (_IsOpenForModify == true && IsFormLoading == false)
            {
                if (c1Transaction.GetData(_rowIndex, COL_CHARGES) != null)
                {
                    oldCharges = Convert.ToDecimal(c1Transaction.GetData(_rowIndex, COL_CHARGES));
                }

                if (oldCharges > 0 && oldCharges != Charges && _showFeeScheduleWarning == true)
                {
                    if (oldCharges.ToString("0.00") != Charges.ToString("0.00"))
                    {
                        DialogResult _msg = MessageBox.Show("Old Charge Amount is $" + oldCharges.ToString("0.00") + ". New Charge Amount is $" + Charges.ToString("0.00") + ". \nUse new Charge Amount of $" + Charges.ToString("0.00") + "?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (_msg == DialogResult.Yes)
                        {
                            _result = true;
                        }
                        else
                        {
                            _result = false;
                        }
                    }
                }
                else
                {
                    _result = true;
                }
            }
            return _result;
        }

        public void setLineInsuranceId(Int64 nInsuranceId)
        {
            for (int iCount = 1; iCount < c1Transaction.Rows.Count; iCount++)
            {
                c1Transaction.SetData(iCount, COL_INSURANCEID, nInsuranceId);
            }
        }

        public void setModifyAllowedAmount(Int64 nInsuranceId)
        {
            for (int iCount = 1; iCount < c1Transaction.Rows.Count; iCount++)
            {
                Int64 _fromDOS = 0;

                if (c1Transaction.GetData(iCount, COL_DATEFROM) != null)
                {
                    if (Convert.ToString(c1Transaction.GetData(iCount, COL_DATEFROM)) != "")
                    {
                        _fromDOS = gloDateMaster.gloDate.DateAsNumber(c1Transaction.GetData(iCount, COL_DATEFROM).ToString());
                    }
                }



                decimal AllowedCharges = 0;
                bool _isAllowedAmont = false;
                ClsFeeSchedule oClsFeeSchedule = new ClsFeeSchedule(_DatabaseConnectionString);
                Int64 _MsttransactionId = 0;
                Int64 _MsttransactionDetailId = 0;
                Int64 _transactionId = 0;
                Int64 _transactionDetailId = 0;
                String _sCPT = "";
                String sMode = "";
                if (c1Transaction.GetData(iCount, COL_MST_TRANSACTIONID) != null && Convert.ToInt64(c1Transaction.GetData(iCount, COL_MST_TRANSACTIONID)) > 0)
                    _MsttransactionId = Convert.ToInt64(c1Transaction.GetData(iCount, COL_MST_TRANSACTIONID));
                if (c1Transaction.GetData(iCount, COL_MST_TRANSACTIONDTLID) != null && Convert.ToInt64(c1Transaction.GetData(iCount, COL_MST_TRANSACTIONDTLID)) > 0)
                    _MsttransactionDetailId = Convert.ToInt64(c1Transaction.GetData(iCount, COL_MST_TRANSACTIONDTLID));

                if (c1Transaction.GetData(iCount, COL_TRANSACTIONID) != null && Convert.ToInt64(c1Transaction.GetData(iCount, COL_TRANSACTIONID)) > 0)
                    _transactionId = Convert.ToInt64(c1Transaction.GetData(iCount, COL_TRANSACTIONID));
                if (c1Transaction.GetData(iCount, COL_TRANSACTION_DETAIL_ID) != null && Convert.ToInt64(c1Transaction.GetData(iCount, COL_TRANSACTION_DETAIL_ID)) > 0)
                    _transactionDetailId = Convert.ToInt64(c1Transaction.GetData(iCount, COL_TRANSACTION_DETAIL_ID));

                if (c1Transaction.GetData(iCount, COL_CPT_CODE) != null && Convert.ToString(c1Transaction.GetData(iCount, COL_CPT_CODE)).Length > 0)
                    _sCPT = Convert.ToString(c1Transaction.GetData(iCount, COL_CPT_CODE));
                if (c1Transaction.GetData(iCount, COL_MOD1_CODE) != null && Convert.ToString(c1Transaction.GetData(iCount, COL_MOD1_CODE)).Length > 0)
                    sMode = Convert.ToString(c1Transaction.GetData(iCount, COL_MOD1_CODE));

                //  getAllowedAmount(Int64 _MsttransactionId, Int64 _transactionId, Int64 _transactionDetailId, Int64 _PatientId, string whencpt, string whenmod, int nFacilityType, ref Boolean bHasAllowedAmt,Int64 fromDOS)

                AllowedCharges = oClsFeeSchedule.GetAllowedAmountForCharges(_nContactID, _MsttransactionDetailId, _sCPT, sMode, DefaultChargesType.GetHashCode(), ref _isAllowedAmont, _fromDOS);

                if (_isAllowedAmont)
                {
                    decimal _nunit = 1;
                    if (c1Transaction.GetData(iCount, COL_UNIT) != null && Convert.ToDecimal(c1Transaction.GetData(iCount, COL_UNIT)) > 0)
                    { _nunit = Convert.ToDecimal(c1Transaction.GetData(iCount, COL_UNIT)); }

                    c1Transaction.SetData(iCount, COL_ALLOWED, AllowedCharges * _nunit);
                }
                else
                    c1Transaction.SetData(iCount, COL_ALLOWED, null);
            }

            c1Total.SetData(0, COL_ALLOWED, GetTotalAllowed());
        }

        public void setnewAllowedAmount()
        {
            for (int iCount = 1; iCount < c1Transaction.Rows.Count; iCount++)
            {
                Int64 _fromDOS = 0;
                if (c1Transaction.GetData(iCount, COL_DATEFROM) != null)
                {
                    if (Convert.ToString(c1Transaction.GetData(iCount, COL_DATEFROM)) != "")
                    {
                        _fromDOS = gloDateMaster.gloDate.DateAsNumber(c1Transaction.GetData(iCount, COL_DATEFROM).ToString());
                    }
                }

                String _sCPT = "";
                String sMode = "";
                decimal AllowedCharges = 0;
                bool _isAllowedAmont = false;
                ClsFeeSchedule oClsFeeSchedule = new ClsFeeSchedule(_DatabaseConnectionString);

                if (c1Transaction.GetData(iCount, COL_CPT_CODE) != null && Convert.ToString(c1Transaction.GetData(iCount, COL_CPT_CODE)).Length > 0)
                    _sCPT = Convert.ToString(c1Transaction.GetData(iCount, COL_CPT_CODE));
                if (c1Transaction.GetData(iCount, COL_MOD1_CODE) != null && Convert.ToString(c1Transaction.GetData(iCount, COL_MOD1_CODE)).Length > 0)
                    sMode = Convert.ToString(c1Transaction.GetData(iCount, COL_MOD1_CODE));
                if (c1Transaction.GetData(iCount, COL_MOD2_CODE) != null && Convert.ToString(c1Transaction.GetData(iCount, COL_MOD2_CODE)).Length > 0)
                    if (sMode.Length == 0)
                        sMode = Convert.ToString(c1Transaction.GetData(iCount, COL_MOD2_CODE));
                    else
                        sMode = sMode + "," + Convert.ToString(c1Transaction.GetData(iCount, COL_MOD2_CODE));

                if (c1Transaction.GetData(iCount, COL_MOD3_CODE) != null && Convert.ToString(c1Transaction.GetData(iCount, COL_MOD3_CODE)).Length > 0)
                    if (sMode.Length == 0)
                        sMode = Convert.ToString(c1Transaction.GetData(iCount, COL_MOD3_CODE));
                    else
                        sMode = sMode + "," + Convert.ToString(c1Transaction.GetData(iCount, COL_MOD3_CODE));

                if (c1Transaction.GetData(iCount, COL_MOD4_CODE) != null && Convert.ToString(c1Transaction.GetData(iCount, COL_MOD4_CODE)).Length > 0)
                    if (sMode.Length == 0)
                        sMode = Convert.ToString(c1Transaction.GetData(iCount, COL_MOD4_CODE));
                    else
                        sMode = sMode + "," + Convert.ToString(c1Transaction.GetData(iCount, COL_MOD4_CODE));

                if (c1Transaction.GetCellCheck(iCount, COL_SELFCLAIM) == CheckEnum.Checked)
                {
                    AllowedCharges = oClsFeeSchedule.GetAllowedAmountForCharges(0, _transactionDetailId, _sCPT, sMode, DefaultChargesType.GetHashCode(), ref _isAllowedAmont, _fromDOS);
                }
                else
                {
                    AllowedCharges = oClsFeeSchedule.GetAllowedAmountForCharges(_nContactID, _transactionDetailId, _sCPT, sMode, DefaultChargesType.GetHashCode(), ref _isAllowedAmont, _fromDOS);
                }

                if (_isAllowedAmont)
                {
                    decimal _nunit = 1;
                    if (c1Transaction.GetData(iCount, COL_UNIT) != null && Convert.ToDecimal(c1Transaction.GetData(iCount, COL_UNIT)) > 0)
                        _nunit = Convert.ToDecimal(c1Transaction.GetData(iCount, COL_UNIT));
                    c1Transaction.SetData(iCount, COL_ALLOWED, AllowedCharges * _nunit);
                }
                else
                    c1Transaction.SetData(iCount, COL_ALLOWED, null);
            }
            c1Total.SetData(0, COL_ALLOWED, GetTotalAllowed());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IsAutoSortRequired">True for New Charges</param>
        /// <param name="IsAutoSortRequired">False for Modify Charges</param>
        public void SetFNFLineCharges(bool IsAutoSortRequired = true)
        {
            try
            {
                if (IsAutoSortRequired) { _AutoSort = false; }

                if (c1Transaction != null && c1Transaction.Rows.Count > 0)
                {
                    for (int i = 1; i < c1Transaction.Rows.Count; i++)
                    {

                        SelectTransactionLine(i);
                        SetFNFCharges();
                    }
                }
                if (IsAutoSortRequired) { _AutoSort = true; SortControl(); }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void GetAlphaIISettings()
        {
            gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_DatabaseConnectionString);
            object value = new object();
            try
            {

                ogloSettings.GetSetting("IsCheckInvalidICD9", out value);
                if (value != null && Convert.ToString(value) != "")
                {
                    _IsCheckInvalidICD9 = Convert.ToBoolean(value);
                    value = null;
                }
                ogloSettings.GetSetting("IsUseScrubber", out value);
                if (value != null && Convert.ToString(value) != "")
                {
                    _IsScrubber = Convert.ToBoolean(value);
                    value = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ogloSettings != null) { ogloSettings.Dispose(); }
                value = null;
            }
        }



        private bool CheckCPTValidation()
        {
            string _cptCode = "";
            Int64 _dos = 0;
            string _mod1 = "";
            string _mod2 = "";
            string _mod3 = "";
            string _mod4 = "";
            int _currentRowindex = 0;
            bool _retVal = true;

            try
            {
                if (c1Transaction != null && c1Transaction.Rows.Count > 1)
                {
                    //...first retive the current rows data cpt,dos,mod
                    for (int i = 1; i < c1Transaction.Rows.Count; i++)
                    {
                        _currentRowindex = i; //c1Transaction.RowSel;

                        _cptCode = Convert.ToString(c1Transaction.GetData(_currentRowindex, COL_CPT_CODE));
                        _dos = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1Transaction.GetData(_currentRowindex, COL_DATEFROM)));
                        _mod1 = Convert.ToString(c1Transaction.GetData(_currentRowindex, COL_MOD1_CODE));
                        _mod2 = Convert.ToString(c1Transaction.GetData(_currentRowindex, COL_MOD2_CODE));
                        _mod3 = Convert.ToString(c1Transaction.GetData(_currentRowindex, COL_MOD3_CODE));
                        _mod4 = Convert.ToString(c1Transaction.GetData(_currentRowindex, COL_MOD4_CODE));

                        for (int k = 1; k < c1Transaction.Rows.Count; k++)
                        {
                            if (k != _currentRowindex)
                            {
                                if (
                                    _cptCode == Convert.ToString(c1Transaction.GetData(k, COL_CPT_CODE)) &&
                                    _dos == gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1Transaction.GetData(k, COL_DATEFROM))) &&
                                    _mod1 == Convert.ToString(c1Transaction.GetData(k, COL_MOD1_CODE)) &&
                                    _mod2 == Convert.ToString(c1Transaction.GetData(k, COL_MOD2_CODE)) &&
                                    _mod3 == Convert.ToString(c1Transaction.GetData(k, COL_MOD3_CODE)) &&
                                    _mod4 == Convert.ToString(c1Transaction.GetData(k, COL_MOD4_CODE))
                                   )
                                {
                                    MessageBox.Show("Cannot add CPT Code with same Date of service & Modifiers", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    _retVal = false;
                                    break;
                                }
                            }
                        }
                        if (_retVal == false) { break; }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            return _retVal;
        }

        public bool FinishEditing()
        {
            bool _result = false;
            try
            {
                if (c1Transaction != null && c1Transaction.Rows.Count > 0)
                {
                    c1Transaction.FinishEditing();
                }
                _result = true;
            }
            catch
            {
                _result = false;
            }
            return _result;
        }

        public void EnableChargeModification()
        {

            c1Transaction.Cols[COL_DATEFROM].AllowEditing = true;
            c1Transaction.Cols[COL_POSCODE].AllowEditing = true;

            c1Transaction.Cols[COL_CPT_CODE].AllowEditing = true;
            c1Transaction.Cols[COL_CHARGES].AllowEditing = true;
            c1Transaction.Cols[COL_UNIT].AllowEditing = true;
            c1Transaction.Cols[COL_ALLOWED].AllowEditing = false;
            c1Transaction.Cols[COL_PROVIDER].AllowEditing = true;

            _AllowChargeModification = true;
        }

        public void DisableChargeModification()
        {
            //Commented By Debasish Das on 7th July 2010 (5060)
            //c1Transaction.Cols[COL_DATEFROM].AllowEditing = false;
            //c1Transaction.Cols[COL_POSCODE].AllowEditing = false;
            //**

            c1Transaction.Cols[COL_CPT_CODE].AllowEditing = false;
            c1Transaction.Cols[COL_CHARGES].AllowEditing = false;
            c1Transaction.Cols[COL_UNIT].AllowEditing = false;
            c1Transaction.Cols[COL_ALLOWED].AllowEditing = false;

            //Un Commented By Debasish Das on 7th July 2010
            //c1Transaction.Cols[COL_PROVIDER].AllowEditing = false;
            c1Transaction.Cols[COL_PROVIDER].AllowEditing = false;
            //**

            _AllowChargeModification = false;
        }

        public void DisableFullChargeModification()
        {

            c1Transaction.Cols[COL_DATEFROM].AllowEditing = false;
            c1Transaction.Cols[COL_POSCODE].AllowEditing = false;
            c1Transaction.Cols[COL_CPT_CODE].AllowEditing = false;
            c1Transaction.Cols[COL_CHARGES].AllowEditing = false;
            c1Transaction.Cols[COL_UNIT].AllowEditing = false;
            c1Transaction.Cols[COL_ALLOWED].AllowEditing = false;
            c1Transaction.Cols[COL_PROVIDER].AllowEditing = false;

            c1Transaction.Cols[COL_DX1_CODE].AllowEditing = false;
            c1Transaction.Cols[COL_DX2_CODE].AllowEditing = false;
            c1Transaction.Cols[COL_DX3_CODE].AllowEditing = false;
            c1Transaction.Cols[COL_DX4_CODE].AllowEditing = false;

            c1Transaction.Cols[COL_HOLD].AllowEditing = false;
            c1Transaction.Cols[COL_HOLD_REASON].AllowEditing = false;
            c1Transaction.Cols[COL_INSURANCENAME].AllowEditing = false;
            c1Transaction.Cols[COL_TOSCODE].AllowEditing = false;


            c1Transaction.Cols[COL_MOD1_CODE].AllowEditing = false;
            c1Transaction.Cols[COL_MOD2_CODE].AllowEditing = false;

            _AllowChargeModification = false;
        }

        public void SetEPSDTFields(TransactionLine oLine)
        {
            try
            {
                if (oLine != null && c1Transaction.RowSel > 0)
                {
                    c1Transaction.SetData(c1Transaction.RowSel, COL_SERVICESCREENING, oLine.ServiceIsTheScreening);
                    c1Transaction.SetData(c1Transaction.RowSel, COL_SERVICERESULTSCREENING, oLine.ServiceIsTheResultOfScreening);
                    c1Transaction.SetData(c1Transaction.RowSel, COL_FAMILYPLANNINGINDICATOR, oLine.ServiceFamilyPlanningIndicator);
                    SetEPSDTNotesNDCCodeFlag(c1Transaction.RowSel);
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }

        public void SetAnesthesiaFields(TransactionLine LineTransactionData, Boolean IsStatus = false)
        {
            try
            {
                if (LineTransactionData != null && c1Transaction.RowSel > 0)
                {
                    c1Transaction.SetData(c1Transaction.RowSel, COL_ANES_ISANESTHESIA, LineTransactionData.bIsAneshtesia);
                    c1Transaction.SetData(c1Transaction.RowSel, COL_ANES_ID, LineTransactionData.AnesthesiaID);
                    c1Transaction.SetData(c1Transaction.RowSel, COL_ANES_STARTDATE, LineTransactionData.AnesthesiaStartTime);
                    c1Transaction.SetData(c1Transaction.RowSel, COL_ANES_ENDDATE, LineTransactionData.AnesthesiaEndTime);
                    c1Transaction.SetData(c1Transaction.RowSel, COL_ANES_TOTALMIN, LineTransactionData.AnesthesiaTotalMinutes);

                    c1Transaction.SetData(c1Transaction.RowSel, COL_ANES_MINPERUNIT, LineTransactionData.AnesthesiaMinPerUnit);
                    c1Transaction.SetData(c1Transaction.RowSel, COL_ANES_TIMEUNITS, LineTransactionData.AnesthesiaTimeUnits);
                    c1Transaction.SetData(c1Transaction.RowSel, COL_ANES_BASEUNITS, LineTransactionData.AnesthesiaBaseUnits);
                    c1Transaction.SetData(c1Transaction.RowSel, COL_ANES_OTHERUNITS, LineTransactionData.AnesthesiaOtherUnits);
                    c1Transaction.SetData(c1Transaction.RowSel, COL_ANES_TOTALUNITS, LineTransactionData.AnesthesiaTotalUnits);
                    c1Transaction.SetData(c1Transaction.RowSel, COL_ANES_ISAUTOCALCULATE, LineTransactionData.bIsAutoCalculateAnesthesia);
                    SetEPSDTNotesNDCCodeFlag(c1Transaction.RowSel);
                    if (c1Transaction.Cols[COL_UNIT].AllowEditing == true)
                    {
                        if (Convert.ToDecimal(LineTransactionData.AnesthesiaTotalUnits) > 0)
                        {
                            Decimal ChargeUnits = Convert.ToDecimal(LineTransactionData.AnesthesiaTotalUnits);
                            c1Transaction.SetData(c1Transaction.RowSel, COL_UNIT, ChargeUnits);
                        }
                        else
                        {
                            if (IsStatus)
                            {
                                c1Transaction.SetData(c1Transaction.RowSel, COL_UNIT, 1);
                            }
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }

        public void SetCaseDiagonosisintoChargeLines(DataTable _dt)
        {
            TrnCtrlColValChangeEventArg e2 = new TrnCtrlColValChangeEventArg();
            RowColEventArgs e1 = null;

            string Dx1 = "", Dx2 = "", Dx3 = "", Dx4 = "", Dx1Desc = "", Dx2Desc = "", Dx3Desc = "", Dx4Desc = "";
            try
            {
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    for (int iCount = 0; iCount <= (_dt.Rows.Count > 4 ? 3 : _dt.Rows.Count - 1); iCount++)
                    {
                        switch (iCount)
                        {
                            case 0:
                                Dx1 = Convert.ToString(_dt.Rows[iCount]["sdxCode"]);
                                Dx1Desc = Convert.ToString(_dt.Rows[iCount]["sDxDescription"]);
                                break;
                            case 1:
                                Dx2 = Convert.ToString(_dt.Rows[iCount]["sdxCode"]);
                                Dx2Desc = Convert.ToString(_dt.Rows[iCount]["sDxDescription"]);
                                break;
                            case 2:
                                Dx3 = Convert.ToString(_dt.Rows[iCount]["sdxCode"]);
                                Dx3Desc = Convert.ToString(_dt.Rows[iCount]["sDxDescription"]);
                                break;
                            case 3:
                                Dx4 = Convert.ToString(_dt.Rows[iCount]["sdxCode"]);
                                Dx4Desc = Convert.ToString(_dt.Rows[iCount]["sDxDescription"]);
                                break;

                        }

                    }

                    for (int iRow = 1; iRow <= c1Transaction.Rows.Count - 1; iRow++)
                    {
                        for (int iCol = COL_DX1_CODE; iCol <= COL_DX8_PTR; iCol++)
                        {
                            c1Transaction.SetData(iRow, iCol, null);
                            c1Transaction.SetData(iRow, COL_LINEPRIMARY_DXCODE, null);
                            c1Transaction.SetData(iRow, COL_LINEPRIMARY_DXDESC, null);
                        }

                        if (Dx1 != string.Empty && Dx1Desc != string.Empty)
                        {
                            c1Transaction.SetData(iRow, COL_DX1_CODE, Dx1);
                            c1Transaction.SetData(iRow, COL_DX1_DESC, Dx1Desc);
                            c1Transaction.SetData(iRow, COL_LINEPRIMARY_DXCODE, Dx1);
                            c1Transaction.SetData(iRow, COL_LINEPRIMARY_DXDESC, Dx1Desc);
                            c1Transaction.SetCellCheck(iRow, COL_DX1_PTR, CheckEnum.Checked);
                            e2.oType = TransactionLineColumnType.Diagnosis;
                            e1 = new RowColEventArgs(iRow, COL_DX1_CODE);
                            e2.code = Dx1;
                            e2.description = Dx1Desc;
                            e2.isdeleted = false;
                            onInsCPTDxMod_Changed(null, e1, e2);
                        }

                        if (Dx2 != string.Empty && Dx2Desc != string.Empty)
                        {
                            c1Transaction.SetData(iRow, COL_DX2_CODE, Dx2);
                            c1Transaction.SetData(iRow, COL_DX2_DESC, Dx2Desc);
                            c1Transaction.SetCellCheck(iRow, COL_DX2_PTR, CheckEnum.Checked);
                            e2.oType = TransactionLineColumnType.Diagnosis;
                            e1 = new RowColEventArgs(iRow, COL_DX2_CODE);
                            e2.code = Dx2;
                            e2.description = Dx2Desc;
                            e2.isdeleted = false;
                            onInsCPTDxMod_Changed(null, e1, e2);
                        }

                        if (Dx3 != string.Empty && Dx3Desc != string.Empty)
                        {
                            c1Transaction.SetData(iRow, COL_DX3_CODE, Dx3);
                            c1Transaction.SetData(iRow, COL_DX3_DESC, Dx3Desc);
                            c1Transaction.SetCellCheck(iRow, COL_DX3_PTR, CheckEnum.Checked);
                            e2.oType = TransactionLineColumnType.Diagnosis;
                            e1 = new RowColEventArgs(iRow, COL_DX3_CODE);
                            e2.code = Dx3;
                            e2.description = Dx3Desc;
                            e2.isdeleted = false;
                            onInsCPTDxMod_Changed(null, e1, e2);
                        }

                        if (Dx4 != string.Empty && Dx4Desc != string.Empty)
                        {
                            c1Transaction.SetData(iRow, COL_DX4_CODE, Dx4);
                            c1Transaction.SetData(iRow, COL_DX4_DESC, Dx4Desc);
                            c1Transaction.SetCellCheck(iRow, COL_DX4_PTR, CheckEnum.Checked);
                            e2.oType = TransactionLineColumnType.Diagnosis;
                            e1 = new RowColEventArgs(iRow, COL_DX4_CODE);
                            e2.code = Dx4;
                            e2.description = Dx4Desc;
                            e2.isdeleted = false;
                            onInsCPTDxMod_Changed(null, e1, e2);
                        }

                    }
                }
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
        }

        public void SetLastLoadedChargesintoServiceLines(DataTable _dt)
        {
            TrnCtrlColValChangeEventArg e2 = new TrnCtrlColValChangeEventArg();
            RowColEventArgs e1 = null;

            string Dx1 = "", Dx2 = "", Dx3 = "", Dx4 = "", Dx1Desc = "", Dx2Desc = "", Dx3Desc = "", Dx4Desc = "";
            try
            {
                if (_dt != null && _dt.Rows.Count > 0)
                {

                    int iCount = 0;

                    if (_dt.Rows[iCount]["sdx1Code"] != null)
                    {
                        Dx1 = Convert.ToString(_dt.Rows[iCount]["sdx1Code"]);
                        Dx1Desc = Convert.ToString(_dt.Rows[iCount]["sDx1Description"]);
                    }

                    if (_dt.Rows[iCount]["sdx2Code"] != null)
                    {
                        Dx2 = Convert.ToString(_dt.Rows[iCount]["sdx2Code"]);
                        Dx2Desc = Convert.ToString(_dt.Rows[iCount]["sDx2Description"]);
                    }

                    if (_dt.Rows[iCount]["sdx3Code"] != null)
                    {
                        Dx3 = Convert.ToString(_dt.Rows[iCount]["sdx3Code"]);
                        Dx3Desc = Convert.ToString(_dt.Rows[iCount]["sDx3Description"]);
                    }

                    if (_dt.Rows[iCount]["sdx4Code"] != null)
                    {
                        Dx4 = Convert.ToString(_dt.Rows[iCount]["sdx4Code"]);
                        Dx4Desc = Convert.ToString(_dt.Rows[iCount]["sDx4Description"]);
                    }

                    int iRow = c1Transaction.Rows.Count - 1;

                    for (int iCol = COL_DX1_CODE; iCol <= COL_DX8_PTR; iCol++)
                    {
                        c1Transaction.SetData(iRow, iCol, null);
                        c1Transaction.SetData(iRow, COL_LINEPRIMARY_DXCODE, null);
                        c1Transaction.SetData(iRow, COL_LINEPRIMARY_DXDESC, null);
                    }

                    if (Dx1 != string.Empty && Dx1Desc != string.Empty)
                    {
                        c1Transaction.SetData(iRow, COL_DX1_CODE, Dx1);
                        c1Transaction.SetData(iRow, COL_DX1_DESC, Dx1Desc);
                        c1Transaction.SetData(iRow, COL_LINEPRIMARY_DXCODE, Dx1);
                        c1Transaction.SetData(iRow, COL_LINEPRIMARY_DXDESC, Dx1Desc);
                        c1Transaction.SetCellCheck(iRow, COL_DX1_PTR, CheckEnum.Checked);
                        e2.oType = TransactionLineColumnType.Diagnosis;
                        e1 = new RowColEventArgs(iRow, COL_DX1_CODE);
                        e2.code = Dx1;
                        e2.description = Dx1Desc;
                        e2.isdeleted = false;
                        onInsCPTDxMod_Changed(null, e1, e2);
                    }

                    if (Dx2 != string.Empty && Dx2Desc != string.Empty)
                    {
                        c1Transaction.SetData(iRow, COL_DX2_CODE, Dx2);
                        c1Transaction.SetData(iRow, COL_DX2_DESC, Dx2Desc);
                        c1Transaction.SetCellCheck(iRow, COL_DX2_PTR, CheckEnum.Checked);
                        e2.oType = TransactionLineColumnType.Diagnosis;
                        e1 = new RowColEventArgs(iRow, COL_DX2_CODE);
                        e2.code = Dx2;
                        e2.description = Dx2Desc;
                        e2.isdeleted = false;
                        onInsCPTDxMod_Changed(null, e1, e2);
                    }

                    if (Dx3 != string.Empty && Dx3Desc != string.Empty)
                    {
                        c1Transaction.SetData(iRow, COL_DX3_CODE, Dx3);
                        c1Transaction.SetData(iRow, COL_DX3_DESC, Dx3Desc);
                        c1Transaction.SetCellCheck(iRow, COL_DX3_PTR, CheckEnum.Checked);
                        e2.oType = TransactionLineColumnType.Diagnosis;
                        e1 = new RowColEventArgs(iRow, COL_DX3_CODE);
                        e2.code = Dx3;
                        e2.description = Dx3Desc;
                        e2.isdeleted = false;
                        onInsCPTDxMod_Changed(null, e1, e2);
                    }

                    if (Dx4 != string.Empty && Dx4Desc != string.Empty)
                    {
                        c1Transaction.SetData(iRow, COL_DX4_CODE, Dx4);
                        c1Transaction.SetData(iRow, COL_DX4_DESC, Dx4Desc);
                        c1Transaction.SetCellCheck(iRow, COL_DX4_PTR, CheckEnum.Checked);
                        e2.oType = TransactionLineColumnType.Diagnosis;
                        e1 = new RowColEventArgs(iRow, COL_DX4_CODE);
                        e2.code = Dx4;
                        e2.description = Dx4Desc;
                        e2.isdeleted = false;
                        onInsCPTDxMod_Changed(null, e1, e2);
                    }

                    if (_DefaultRenderingProviderID > 0)
                    {
                        c1Transaction.SetData(iRow, COL_PROVIDER_ID, _DefaultRenderingProviderID);
                        c1Transaction.SetData(iRow, COL_PROVIDER, _DefaultRenderringProviderName);
                    }

                    if (_dt.Rows[iCount]["nFromDate"] != null)
                    {
                        DateTime dtDOS = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(_dt.Rows[iCount]["nFromDate"]));
                        c1Transaction.SetData(iRow, COL_DATEFROM, null);
                        c1Transaction.SetData(iRow, COL_DATEFROM, dtDOS.ToShortDateString());
                        c1Transaction.FinishEditing();
                    }

                    if (_dt.Rows[iCount]["sPOSCode"] != null)
                    {
                        c1Transaction.SetData(iRow, COL_POSCODE, Convert.ToString(_dt.Rows[iCount]["sPOSCode"]));
                    }
                    if (_dt.Rows[iCount]["sPOSDescription"] != null)
                    {
                        c1Transaction.SetData(iRow, COL_POSDESC, Convert.ToString(_dt.Rows[iCount]["sPOSDescription"]));
                    }
                    if (_dt.Rows[iCount]["sTOSCode"] != null)
                    {
                        c1Transaction.SetData(iRow, COL_TOSCODE, Convert.ToString(_dt.Rows[iCount]["sTOSCode"]));
                    }
                    if (_dt.Rows[iCount]["sTOSDescription"] != null)
                    {
                        c1Transaction.SetData(iRow, COL_TOSDESC, Convert.ToString(_dt.Rows[iCount]["sTOSDescription"]));
                    }

                }
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
        }

        public Boolean ValidateInsuranceCovrageDates(DataTable _dtInsuranceDates, string _sCurrentInsuranceParyt)
        {
            DateTime dtDateFrom;
            DateTime dtDateTo;
            for (int i = 1; i <= c1Transaction.Rows.Count - 1; i++)
            {
                dtDateFrom = Convert.ToDateTime(c1Transaction.GetData(i, COL_DATEFROM));
                dtDateTo = Convert.ToDateTime(c1Transaction.GetData(i, COL_DATETO));
                #region "Commented Code"
                //if (_dtInsuranceDates.Rows[0]["dtStartDate"] != DBNull.Value)
                //{
                //    if ((dtDateFrom.Date< (Convert.ToDateTime(_dtInsuranceDates.Rows[0]["dtStartDate"]).Date)))
                //    {
                //        if (MessageBox.Show("Patient’s insurance, " + _sCurrentInsuranceParyt + ",  may not be up-to-date." + Environment.NewLine + "Please verify and update system." + Environment.NewLine + "Continue?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                //        {
                //            c1Transaction.Focus();
                //            c1Transaction.Select(i, COL_DATEFROM);
                //            return false;

                //        }
                //    }
                //}
                //if (_dtInsuranceDates.Rows[0]["dtEndDate"] != DBNull.Value && dtDateTo != DateTime.MinValue)
                //{
                //    if ((dtDateTo.Date > (Convert.ToDateTime(_dtInsuranceDates.Rows[0]["dtEndDate"]).Date)))
                //    {
                //        if (MessageBox.Show("Patient’s insurance, " + _sCurrentInsuranceParyt + ",  may not be up-to-date." + Environment.NewLine + "Please verify and update system." + Environment.NewLine + "Continue?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                //        {
                //            c1Transaction.Focus();
                //            c1Transaction.Select(i, COL_DATETO);
                //            return false;

                //        }
                //    }

                //}
                #endregion

                if (_dtInsuranceDates.Rows[0]["dtEndDate"] != DBNull.Value)
                {
                    if (_dtInsuranceDates.Rows[0]["dtStartDate"] != DBNull.Value)
                    {
                        if (dtDateTo.Date != DateTime.MinValue)
                        {
                            if (dtDateFrom.Date < Convert.ToDateTime(_dtInsuranceDates.Rows[0]["dtStartDate"]).Date || dtDateFrom.Date > Convert.ToDateTime(_dtInsuranceDates.Rows[0]["dtEndDate"]).Date ||
                                dtDateTo.Date < Convert.ToDateTime(_dtInsuranceDates.Rows[0]["dtStartDate"]).Date || dtDateTo.Date > Convert.ToDateTime(_dtInsuranceDates.Rows[0]["dtEndDate"]).Date)
                            {
                                if (MessageBox.Show("Patient’s insurance, " + _sCurrentInsuranceParyt + ",  may not be up to date." + Environment.NewLine + "Please verify and update system." + Environment.NewLine + "Continue?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                {
                                    c1Transaction.Focus();
                                    c1Transaction.Select(i, COL_DATEFROM);
                                    return false;
                                }
                            }
                        }
                        else   // no ToDOS
                        {
                            if (dtDateFrom.Date < Convert.ToDateTime(_dtInsuranceDates.Rows[0]["dtStartDate"]).Date || dtDateFrom.Date > Convert.ToDateTime(_dtInsuranceDates.Rows[0]["dtEndDate"]).Date)
                                if (MessageBox.Show("Patient’s insurance, " + _sCurrentInsuranceParyt + ",  may not be up to date." + Environment.NewLine + "Please verify and update system." + Environment.NewLine + "Continue?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                {
                                    c1Transaction.Focus();
                                    c1Transaction.Select(i, COL_DATEFROM);
                                    return false;
                                }

                        }
                    }
                    else //only end date
                    {
                        if (dtDateTo.Date != DateTime.MinValue)
                        {
                            if (dtDateFrom.Date > Convert.ToDateTime(_dtInsuranceDates.Rows[0]["dtEndDate"]).Date || dtDateTo.Date > Convert.ToDateTime(_dtInsuranceDates.Rows[0]["dtEndDate"]).Date)
                            {
                                if (MessageBox.Show("Patient’s insurance, " + _sCurrentInsuranceParyt + ",  may not be up to date." + Environment.NewLine + "Please verify and update system." + Environment.NewLine + "Continue?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                {
                                    c1Transaction.Focus();
                                    c1Transaction.Select(i, COL_DATEFROM);
                                    return false;
                                }
                            }
                        }
                        else   // no ToDOS
                        {
                            if (dtDateFrom.Date > Convert.ToDateTime(_dtInsuranceDates.Rows[0]["dtEndDate"]).Date)
                                if (MessageBox.Show("Patient’s insurance, " + _sCurrentInsuranceParyt + ",  may not be up to date." + Environment.NewLine + "Please verify and update system." + Environment.NewLine + "Continue?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                {
                                    c1Transaction.Focus();
                                    c1Transaction.Select(i, COL_DATEFROM);
                                    return false;
                                }

                        }
                    }
                }
                else if (_dtInsuranceDates.Rows[0]["dtStartDate"] != DBNull.Value)
                {
                    if (Convert.ToDateTime(_dtInsuranceDates.Rows[0]["dtStartdate"]).Date > dtDateFrom)
                    {
                        if (MessageBox.Show("Patient’s insurance, " + _sCurrentInsuranceParyt + ",  may not be up to date." + Environment.NewLine + "Please verify and update system." + Environment.NewLine + "Continue?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        {
                            c1Transaction.Focus();
                            c1Transaction.Select(i, COL_DATEFROM);
                            return false;
                        }
                    }
                }

                #region "Commented code"

                //if (_dtInsuranceDates.Rows[0]["dtStartDate"] != DBNull.Value && _dtInsuranceDates.Rows[0]["dtEndDate"] != DBNull.Value && dtDateTo != DateTime.MinValue)
                //{
                //    if (Convert.ToDateTime(_dtInsuranceDates.Rows[0]["dtStartdate"]).Date > dtDateFrom.Date && Convert.ToDateTime(_dtInsuranceDates.Rows[0]["dtEndDate"]).Date < dtDateFrom.Date)
                //    {
                //        if (MessageBox.Show("Patient’s insurance, " + _sCurrentInsuranceParyt + ",  may not be up-to-date." + Environment.NewLine + "Please verify and update system." + Environment.NewLine + "Continue?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                //        {
                //            c1Transaction.Focus();
                //            c1Transaction.Select(i, COL_DATEFROM);
                //            return false;

                //        }
                //    }
                //}

                //if (_dtInsuranceDates.Rows[0]["dtStartDate"] != DBNull.Value)
                //{
                //    if ((dtDateFrom.Date < (Convert.ToDateTime(_dtInsuranceDates.Rows[0]["dtStartDate"]).Date)))
                //    {
                //        if (MessageBox.Show("Patient’s insurance, " + _sCurrentInsuranceParyt + ",  may not be up-to-date." + Environment.NewLine + "Please verify and update system." + Environment.NewLine + "Continue?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                //        {
                //            c1Transaction.Focus();
                //            c1Transaction.Select(i, COL_DATEFROM);
                //            return false;

                //        }
                //    }
                //}


                //if (_dtInsuranceDates.Rows[0]["dtEndDate"] != DBNull.Value)
                //{                    
                //    if (Convert.ToDateTime(_dtInsuranceDates.Rows[0]["dtEndDate"]).Date > dtDateFrom.Date && Convert.ToDateTime(_dtInsuranceDates.Rows[0]["dtEndDate"]).Date > dtDateTo.Date)
                //    {
                //        if (MessageBox.Show("Patient’s insurance, " + _sCurrentInsuranceParyt + ",  may not be up-to-date." + Environment.NewLine + "Please verify and update system." + Environment.NewLine + "Continue?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                //        {
                //            c1Transaction.Focus();
                //            c1Transaction.Select(i, COL_DATEFROM);
                //            return false;

                //        }
                //    }
                //}

                #endregion
            }
            return true;
        }

        #endregion

        #region " Context Menu Events "

        private void tls_cmnu_ApplyAll_Click(object sender, EventArgs e)
        {
            string _dxCode = "";
            string _dxDesc = "";

            try
            {
                if (c1Transaction.GetData(c1Transaction.RowSel, c1Transaction.ColSel) != null
                    && Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, c1Transaction.ColSel)) != "")
                {
                    _dxCode = Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, c1Transaction.ColSel));
                    _dxDesc = Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, c1Transaction.ColSel + 1));

                    if (_dxCode.Trim() != "")
                    {
                        for (int i = 1; i < c1Transaction.Rows.Count; i++)
                        {
                            if (c1Transaction.GetData(i, c1Transaction.ColSel) == null || Convert.ToString(c1Transaction.GetData(i, c1Transaction.ColSel)) == "")
                            {
                                c1Transaction.SetData(i, c1Transaction.ColSel, _dxCode);
                                c1Transaction.SetData(i, (c1Transaction.ColSel + 1), _dxDesc);

                                switch (c1Transaction.ColSel)
                                {
                                    case COL_DX1_CODE:

                                        c1Transaction.SetData(i, COL_LINEPRIMARY_DXCODE, c1Transaction.GetData(i, COL_DX1_CODE));
                                        c1Transaction.SetData(i, COL_LINEPRIMARY_DXDESC, c1Transaction.GetData(i, COL_DX1_DESC));

                                        c1Transaction.SetCellCheck(i, COL_DX1_PTR, CheckEnum.Checked);
                                        break;
                                    case COL_DX2_CODE:
                                        c1Transaction.SetCellCheck(i, COL_DX2_PTR, CheckEnum.Checked);
                                        break;
                                    case COL_DX3_CODE:
                                        c1Transaction.SetCellCheck(i, COL_DX3_PTR, CheckEnum.Checked);
                                        break;
                                    case COL_DX4_CODE:
                                        c1Transaction.SetCellCheck(i, COL_DX4_PTR, CheckEnum.Checked);
                                        break;
                                    default:
                                        break;


                                }
                            }
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                c1Transaction.ContextMenuStrip = null;
            }
        }

        private void tls_cmnu_OverwriteApplyAll_Click(object sender, EventArgs e)
        {
            string _dxCode = "";
            string _dxDesc = "";
            try
            {
                if (c1Transaction.GetData(c1Transaction.RowSel, c1Transaction.ColSel) != null && Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, c1Transaction.ColSel)) != "")
                {
                    _dxCode = Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, c1Transaction.ColSel));
                    _dxDesc = Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, (c1Transaction.ColSel + 1)));

                    if (_dxCode.Trim() != "")
                    {
                        for (int i = 1; i < c1Transaction.Rows.Count; i++)
                        {

                            DxModified(i, c1Transaction.ColSel);

                            c1Transaction.SetData(i, c1Transaction.ColSel, _dxCode);
                            c1Transaction.SetData(i, (c1Transaction.ColSel + 1), _dxDesc);

                            RemoveDxModified();

                            switch (c1Transaction.ColSel)
                            {
                                case COL_DX1_CODE:

                                    c1Transaction.SetData(i, COL_LINEPRIMARY_DXCODE, c1Transaction.GetData(i, COL_DX1_CODE));
                                    c1Transaction.SetData(i, COL_LINEPRIMARY_DXDESC, c1Transaction.GetData(i, COL_DX1_DESC));

                                    c1Transaction.SetCellCheck(i, COL_DX1_PTR, CheckEnum.Checked);
                                    break;
                                case COL_DX2_CODE:
                                    c1Transaction.SetCellCheck(i, COL_DX2_PTR, CheckEnum.Checked);
                                    break;
                                case COL_DX3_CODE:
                                    c1Transaction.SetCellCheck(i, COL_DX3_PTR, CheckEnum.Checked);
                                    break;
                                case COL_DX4_CODE:
                                    c1Transaction.SetCellCheck(i, COL_DX4_PTR, CheckEnum.Checked);
                                    break;
                                default:
                                    break;


                            }
                        }
                        evtModifyDxRowCol = null;
                        evtModifyDx = new TrnCtrlColValChangeEventArg();
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                c1Transaction.ContextMenuStrip = null;
            }
        }

        private void cmnu_Apply_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            c1Transaction.ContextMenuStrip = null;
        }

        #endregion " Context Menu Events "

        public Int64 getfirstservicelineDos()
        {
            Int64 _fromDOS = 0;
            try
            {
                if (c1Transaction.Rows.Count > 1)
                {
                    if (c1Transaction.GetData(1, COL_DATEFROM) != null)
                    {
                        if (Convert.ToString(c1Transaction.GetData(1, COL_DATEFROM)) != "")
                        {
                            _fromDOS = gloDateMaster.gloDate.DateAsNumber(c1Transaction.GetData(1, COL_DATEFROM).ToString());
                        }
                    }
                }
                return _fromDOS;
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

            return _fromDOS;
        }

        public void getfirstservicelineCLIA()
        {            
            try
            {
                if (c1Transaction.Rows.Count > 1)
                {
                    if (c1Transaction.GetData(1, COL_AUTHORIZATIONNO) != null && Convert.ToString(c1Transaction.GetData(1, COL_AUTHORIZATIONNO)) != "")
                    {                       
                        FirstCLIANo = Convert.ToString(c1Transaction.GetData(1, COL_AUTHORIZATIONNO));                                                 
                    }
                    else
                    { FirstCLIANo = ""; }
                }
                else if (c1Transaction.Rows.Count == 1) 
                {
                    FirstCLIANo = "";
                }
                CLIA_Enter();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }           
        }

        public DateTime getLeastServiceLineDos()
        {
            DateTime _fromDOS = default(DateTime);
            try
            {

                if (c1Transaction.Rows.Count > 1)
                {


                    DateTime objMinDate = DateTime.MaxValue;
                    DateTime objCurrentDate = default(DateTime);

                    for (int i = 1; i < c1Transaction.Rows.Count; i++)
                    {
                        if (DateTime.TryParse(Convert.ToString(c1Transaction.GetData(i, COL_DATEFROM)), out objCurrentDate))
                        {
                            if (DateTime.Compare(objCurrentDate, objMinDate) < 0)
                                objMinDate = objCurrentDate;
                        }

                    }

                    if (objMinDate != default(DateTime))
                        _fromDOS = objMinDate;


                    //List<DateTime> objFromDate= new List<DateTime>();
                    //   objFromDate.Add(objMinDate); 
                    //objMinDate = (from Result in objFromDate.AsEnumerable() select Result).Min();   


                }
                return _fromDOS;
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

            return _fromDOS;
        }
        //Code Added for Duplicate claim warning 09-02-2014
        public string CheckduplicateClaims(Int64 nPatientID, string nFromDOS)
        {
            gloDatabaseLayer.DBLayer oDB = null;
            gloDatabaseLayer.DBParameters oParameters = null;
            DataTable dtDoslist = null;
            StringBuilder _DosList = null;

            try
            {
                oDB = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
                oParameters = new gloDatabaseLayer.DBParameters();

                oParameters.Add("@nPatientID", nPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nDates", nFromDOS, ParameterDirection.Input, SqlDbType.Text);
                oDB.Connect(false);
                oDB.Retrive("gsp_CheckduplicateClaims", oParameters, out dtDoslist);
                oDB.Disconnect();

                if (dtDoslist != null && dtDoslist.Rows.Count > 0)
                {
                    _DosList = new StringBuilder();

                    //_DosList.AppendLine("Duplicate Claim Warning for patient ");
                    //_DosList.AppendLine();
                    for (int i = 0; i < dtDoslist.Rows.Count; i++)
                    {
                        if (i <= 9)
                        {
                            _DosList.AppendLine(dtDoslist.Rows[i]["DosList"].ToString());
                            _DosList.AppendLine();
                        }
                        else if (i == 10)
                        {
                            _DosList.AppendLine("Too many Duplicate claims to list.........");
                            break;
                        }

                    }
                    _DosList.AppendLine();
                    _DosList.AppendLine("Continue saving new claim now?");
                }

                return Convert.ToString(_DosList);
            }

            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                ex = null;
                return "";
            }
            finally
            {
                if (oParameters != null)
                {
                    oParameters.Dispose();
                    oParameters = null;
                }
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;

                }
                if (dtDoslist != null)
                {
                    dtDoslist.Dispose();
                    dtDoslist = null;
                }
            }

        }





        public void SetFacilityMammogramNo()
        {
            ClsFeeSchedule oClsFeeSchedule = null;
            try
            {
                oClsFeeSchedule = new ClsFeeSchedule(_DatabaseConnectionString);
                sMammogramCertNo = oClsFeeSchedule.GetFacilityMammogramNumber("", _facilityID);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (oClsFeeSchedule != null)
                {
                    oClsFeeSchedule.Dispose();
                    oClsFeeSchedule = null;
                }
            }
        }        
    }
    
}

