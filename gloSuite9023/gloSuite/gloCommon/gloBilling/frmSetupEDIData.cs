using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using gloBilling.Common;
using gloAppointmentBook.Books;
using Edidev.FrameworkEDI;


namespace gloBilling
{
    public partial class frmSetupEDIData : Form
    {
        #region " Constructor "

        public frmSetupEDIData(string DatabaseConnectionString)
        {
            InitializeComponent();
            _databaseconnectionstring = DatabaseConnectionString;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

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

        public frmSetupEDIData(string DatabaseConnectionString, Int64 PatientID)
        {
            InitializeComponent();
            _databaseconnectionstring = DatabaseConnectionString;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

            this.PatientID = PatientID;

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

        #endregion " Constructor "

        #region " Private & Public Variable Declarations "

        //Edi Variables
        private Int32 nHlCount = 0;
        private Int32 nHlProvParent = 0;
        private Int32 nHlSubscriberParent = 0;
        private Int32 iItemCount = 0;
      //  gloPatient.Referrals oReferral = new gloPatient.Referrals();
        ediDocument oEdiDoc = null;
        ediInterchange oInterchange = null;
        ediGroup oGroup = null;
        ediTransactionSet oTransactionset = null;
        ediDataSegment oSegment = null;
        ediSchema oSchema = null;
        ediSchemas oSchemas = null;
      //  ediWarnings oWarnings = null;
       // ediWarning oWarning = null;

        string sSEFFile = "";
        string sEdiFile = "";
        string sPath = "";
        //
        private bool bSecondaryInsurance = false;
        private string _databaseconnectionstring;
        private string _messageBoxCaption = String.Empty;
        private ArrayList _arrSelectedTransactions = null;
        private Transaction _Transaction = null;
        public ArrayList SelectedTransactions 
        {
            get { return _arrSelectedTransactions; }
            set { _arrSelectedTransactions = value; }
        }
        Int64 _ClinicID = 0;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _PatientID = 0;
        public Int64 PatientID 
        {
            get { return _PatientID; }
            set { _PatientID = value; }
        }

        //Referral Provider
        private string _ReferralFName = "";
        private string _ReferralLName = "";
        private string _ReferralMName = "";
        private string _ReferralAddress = "";
        private string _ReferralCity = "";
        private string _ReferralState = "";
        private string _ReferralZIP = "";
        private string _ReferralNPI = "";
        private string _ReferralSSN = "";
        private string _ReferralEmployerID = "";
        private string _ReferralStateMedicalNo = "";
        private string _ReferralTaxonomy = "";

        //Rendering Provider
        private string _RenderingFName = "";
        private string _RenderingLName = "";
        private string _RenderingMName = "";
        private string _RenderingAddress = "";
        private string _RenderingCity = "";
        private string _RenderingState = "";
        private string _RenderingZIP = "";
        private string _RenderingNPI = "";
        private string _RenderingSSN = "";
        private string _RenderingEmployerID = "";
        private string _RenderingStateMedicalNo = "";
        private string _RenderingTaxonomy = "";

        //Billing Provider
        private string _BillingFName = "";
        private string _BillingLName = "";
        private string _BillingMName = "";
        private string _BillingCity = "";
        private string _BillingState = "";
        private string _BillingAddress = "";
        private string _BillingZIP = "";
        private string _BillingNPI = "";
        private string _BillingSSN = "";
        private string _BillingEmployerID = "";
        private string _BillingStateMedicalNo = "";
        private string _BillingTaxonomy = "";

        //Submitter
        private string _SubmitterName = "";
        private string _SubmitterContactPersonName = "";
        private string _SubmitterContactPersonNo = "";
        private string _SubmitterCity = "";
        private string _SubmitterState = "";
        private string _SubmitterZIP = "";
        private string _SubmitterETIN = "";
        private string _SubmitterAddress = "";

        //Receiver
      //  private string _ReceiverName = "";
      //  private string _ReceiverETIN = "";

        //Subscriber
        private string _SubscriberLName = "";
        private string _SubscriberInsurancePST = "";
        private string _SubscriberRelationshipCode = "";
        private string _SubscriberInsuranceBelongs = "";
        private string _SubscriberFName = "";
        private string _SubscriberMName = "";
        private string _SubscriberInsuranceID = "";
        private string _SubscriberAddress = "";
        private string _SubscriberGroupID = "";
        private string _SubscriberCity = "";
        private string _SubscriberState = "";
        private string _SubscriberZIP = "";
        private string _SubscriberDOB = "";
        private string _SubscriberGender = "";

        //Payer
        private string _PayerName = "";
        private string _PayerID = "";
        private string _PayerAddress = "";
        private string _PayerCity = "";
        private string _PayerState = "";
        private string _PayerZip = "";

        private string _PatientAccountNo = "";


        //Facility
    //    private string _FacilityCode = "";
        private string _FacilityName = "";
        private string _FacilityAddress = "";
        private string _FacilityCity = "";
        private string _FacilityState = "";
        private string _FacilityZip = "";
        private string _FacilityNPI = "";
       

        //Other Insurance
        private string _OtherInsuranceSubscriberLName = "";
        private string _OtherInsurancePST = "";
        private string _OtherInsuranceType = "";
        private string _OtherInsuranceRelationshipCode = "";
        private string _OtherInsuranceID = "";
        private string _OtherInsuranceGroupID = "";
        private string _OtherInsuranceAddress = "";
        private string _OtherInsuranceSubscriberFName = "";
        private string _OtherInsuranceSubscriberMName = "";
        private string _OtherInsuranceName = "";
        private string _OtherInsurancePayerID = "";
        private string _OtherInsuranceCity = "";
        private string _OtherInsuranceState = "";
        private string _OtherInsuranceZIP = "";
        private string _OtherInsuranceSubscriberDOB = "";
        private string _OtherInsuranceSubscriberGender = "";

        //ISA and GS Settings
        private string _SenderID = "";
        private string _ReceiverID = "";
        private string _SenderName = "";
        private string _ReceiverCode = "";
                        

        //Patient Information
        private string _PatientLastName = "";
        private string _PatientFirstName = "";
        private string _PatientMiddleName = "";
        private string _PatientSSN = "";
        private string _PatientGender = "";
        private string _PatientDOB = "";
        private string _PatientAddress = "";
        private string _PatientCity = "";
        private string _PatientState = "";
        private string _PatientZip = "";
        private string _PatientCode = "";    


        //Prior Authorization Number
        private string _PriorAuthorizationNo = "";

        #endregion " Private & Public Variable Declarations "

        #region " Enumerations "

        public enum ProviderType
        {
            BillingProvider = 1,
            PayToProvider = 2,
            RefferingProvider = 3,
            RenderingProvider = 4
        }

        #endregion " Enumerations "

        #region " Form Load "

        private void frmSetupEDIData_Load(object sender, EventArgs e)
        {
          //  gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
         //   string _strSQL = "";
         //   DataTable dt;
            try
            {
                DesignTransactionGrid();

                FillAllCombosWithDefaults();
                FillClearingHouseInfo();
                GetTransaction();
                LoadEDIObject();
                tsb_ShowHCFA1500.Visible = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                //if (oDB != null)
                //{
                //    oDB.Disconnect();
                //    oDB.Dispose();
                //}
            }
        }

        #endregion " Form Load "

        #region " Tool Strip Item Clicked Event "

        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "OK":
                        ValidateData();
                        this.Close();
                        break;
                    case "Cancel":
                        this.Close();
                        break;
                    case "GenerateEDI":
                        //if (Convert.ToInt64(_Transaction.ProviderID) != 0 && _Transaction.ProviderID.ToString() != "")
                        //{
                        //    FillProviderDetails(Convert.ToInt64(_Transaction.ProviderID), ProviderType.BillingProvider);
                        //}

                        GenerateEDIFile();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        } 

        #endregion
        
        #region " C1 Grid Region "

        #region "Columns"
        const int COL_NO = 0;
        const int COL_TRANSACTIONID = 1;

        const int COL_DATEFROM = 2;
        const int COL_DATETO = 3;

        const int COL_POSCODE = 4;
        const int COL_POSDESC = 5;

        const int COL_TOSCODE = 6;
        const int COL_TOSDESC = 7;

        const int COL_CPT_CODE = 8;
        const int COL_CPT_DESC = 9;

        const int COL_DX1_CODE = 10;
        const int COL_DX1_DESC = 11;
        const int COL_DX2_CODE = 12;
        const int COL_DX2_DESC = 13;
        const int COL_DX3_CODE = 14;
        const int COL_DX3_DESC = 15;
        const int COL_DX4_CODE = 16;
        const int COL_DX4_DESC = 17;
        const int COL_DX5_CODE = 18;
        const int COL_DX5_DESC = 19;
        const int COL_DX6_CODE = 20;
        const int COL_DX6_DESC = 21;
        const int COL_DX7_CODE = 22;
        const int COL_DX7_DESC = 23;
        const int COL_DX8_CODE = 24;
        const int COL_DX8_DESC = 25;

        const int COL_DX1_PTR = 26;
        const int COL_DX2_PTR = 27;
        const int COL_DX3_PTR = 28;
        const int COL_DX4_PTR = 29;
        const int COL_DX5_PTR = 30;
        const int COL_DX6_PTR = 31;
        const int COL_DX7_PTR = 32;
        const int COL_DX8_PTR = 33;

        const int COL_MOD1_CODE = 34;
        const int COL_MOD1_DESC = 35;
        const int COL_MOD2_CODE = 36;
        const int COL_MOD2_DESC = 37;
        const int COL_MOD3_CODE = 38;
        const int COL_MOD3_DESC = 39;
        const int COL_MOD4_CODE = 40;
        const int COL_MOD4_DESC = 41;

        const int COL_CHARGES = 42;
        const int COL_UNIT = 43;
        const int COL_TOTAL = 44;
        const int COL_ALLOWED = 45;
        const int COL_PROVIDER_ID = 46;
        const int COL_PROVIDER = 47;
        const int COL_PROVIDER_BTN = 48;
        const int COL_NOTE_DATA = 49;
        const int COL_NOTE_TYPE = 50;

        const int COL_COUNT = 51;
        private int ROW_COUNT = 1;

        #endregion

        private Int32 _InitialNoOfLines = 0;

        public Int32 InitialNofRows
        {
            get { return _InitialNoOfLines; }
            set { _InitialNoOfLines = value; ROW_COUNT = value; }
        }

        public Int32 CurrentTransactionLine
        {
            get { return c1Transaction.RowSel; }
        }

        private void DesignTransactionGrid()
        {
            c1Transaction.Clear();
            c1Transaction.Rows.Count = ROW_COUNT;
            c1Transaction.Rows.Fixed = 1;
            c1Transaction.Cols.Count = COL_COUNT;
            c1Transaction.Cols.Fixed = 1;

            #region "Data Type"
            c1Transaction.Cols[COL_NO].DataType = typeof(System.Int32);
            c1Transaction.Cols[COL_TRANSACTIONID].DataType = typeof(System.Int64);

            c1Transaction.Cols[COL_DATEFROM].DataType = typeof(System.DateTime);
            c1Transaction.Cols[COL_DATETO].DataType = typeof(System.DateTime);

            c1Transaction.Cols[COL_POSCODE].DataType = typeof(System.String);
            c1Transaction.Cols[COL_POSDESC].DataType = typeof(System.String);

            c1Transaction.Cols[COL_TOSCODE].DataType = typeof(System.String);
            c1Transaction.Cols[COL_TOSDESC].DataType = typeof(System.String);

            c1Transaction.Cols[COL_CPT_CODE].DataType = typeof(System.String);
            c1Transaction.Cols[COL_CPT_DESC].DataType = typeof(System.String);

            c1Transaction.Cols[COL_DX1_CODE].DataType = typeof(System.String);
            c1Transaction.Cols[COL_DX1_DESC].DataType = typeof(System.String);
            c1Transaction.Cols[COL_DX2_CODE].DataType = typeof(System.String);
            c1Transaction.Cols[COL_DX2_DESC].DataType = typeof(System.String);
            c1Transaction.Cols[COL_DX3_CODE].DataType = typeof(System.String);
            c1Transaction.Cols[COL_DX3_DESC].DataType = typeof(System.String);
            c1Transaction.Cols[COL_DX4_CODE].DataType = typeof(System.String);
            c1Transaction.Cols[COL_DX4_DESC].DataType = typeof(System.String);
            c1Transaction.Cols[COL_DX5_CODE].DataType = typeof(System.String);
            c1Transaction.Cols[COL_DX5_DESC].DataType = typeof(System.String);
            c1Transaction.Cols[COL_DX6_CODE].DataType = typeof(System.String);
            c1Transaction.Cols[COL_DX6_DESC].DataType = typeof(System.String);
            c1Transaction.Cols[COL_DX7_CODE].DataType = typeof(System.String);
            c1Transaction.Cols[COL_DX7_DESC].DataType = typeof(System.String);
            c1Transaction.Cols[COL_DX8_CODE].DataType = typeof(System.String);
            c1Transaction.Cols[COL_DX8_DESC].DataType = typeof(System.String);

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
            c1Transaction.Cols[COL_MOD2_CODE].DataType = typeof(System.String);
            c1Transaction.Cols[COL_MOD2_DESC].DataType = typeof(System.String);
            c1Transaction.Cols[COL_MOD3_CODE].DataType = typeof(System.String);
            c1Transaction.Cols[COL_MOD3_DESC].DataType = typeof(System.String);
            c1Transaction.Cols[COL_MOD4_CODE].DataType = typeof(System.String);
            c1Transaction.Cols[COL_MOD4_DESC].DataType = typeof(System.String);


            c1Transaction.Cols[COL_CHARGES].DataType = typeof(System.Decimal);
            c1Transaction.Cols[COL_UNIT].DataType = typeof(System.Decimal);
            c1Transaction.Cols[COL_TOTAL].DataType = typeof(System.Decimal);
            c1Transaction.Cols[COL_ALLOWED].DataType = typeof(System.Decimal);
            c1Transaction.Cols[COL_PROVIDER_ID].DataType = typeof(System.Int64);
            c1Transaction.Cols[COL_PROVIDER].DataType = typeof(System.String);
            c1Transaction.Cols[COL_NOTE_DATA].DataType = typeof(System.String);
            c1Transaction.Cols[COL_NOTE_TYPE].DataType = typeof(System.Int32);
            #endregion

            #region "Width"
            c1Transaction.Cols[COL_NO].Width = 20;
            c1Transaction.Cols[COL_TRANSACTIONID].Width = 0;

            c1Transaction.Cols[COL_DATEFROM].Width = 80;
            c1Transaction.Cols[COL_DATETO].Width = 80;

            c1Transaction.Cols[COL_POSCODE].Width = 50;
            c1Transaction.Cols[COL_POSDESC].Width = 50;

            c1Transaction.Cols[COL_TOSCODE].Width = 50;
            c1Transaction.Cols[COL_TOSDESC].Width = 50;

            c1Transaction.Cols[COL_CPT_CODE].Width = 50;
            c1Transaction.Cols[COL_CPT_DESC].Width = 50;

            c1Transaction.Cols[COL_DX1_CODE].Width = 50;
            c1Transaction.Cols[COL_DX1_DESC].Width = 50;
            c1Transaction.Cols[COL_DX2_CODE].Width = 50;
            c1Transaction.Cols[COL_DX2_DESC].Width = 50;
            c1Transaction.Cols[COL_DX3_CODE].Width = 50;
            c1Transaction.Cols[COL_DX3_DESC].Width = 50;
            c1Transaction.Cols[COL_DX4_CODE].Width = 50;
            c1Transaction.Cols[COL_DX4_DESC].Width = 50;
            c1Transaction.Cols[COL_DX5_CODE].Width = 0;
            c1Transaction.Cols[COL_DX5_DESC].Width = 0;
            c1Transaction.Cols[COL_DX6_CODE].Width = 0;
            c1Transaction.Cols[COL_DX6_DESC].Width = 0;
            c1Transaction.Cols[COL_DX7_CODE].Width = 0;
            c1Transaction.Cols[COL_DX7_DESC].Width = 0;
            c1Transaction.Cols[COL_DX8_CODE].Width = 0;
            c1Transaction.Cols[COL_DX8_DESC].Width = 0;

            c1Transaction.Cols[COL_DX1_PTR].Width = 20;
            c1Transaction.Cols[COL_DX2_PTR].Width = 20;
            c1Transaction.Cols[COL_DX3_PTR].Width = 20;
            c1Transaction.Cols[COL_DX4_PTR].Width = 20;
            c1Transaction.Cols[COL_DX5_PTR].Width = 0;
            c1Transaction.Cols[COL_DX6_PTR].Width = 0;
            c1Transaction.Cols[COL_DX7_PTR].Width = 0;
            c1Transaction.Cols[COL_DX8_PTR].Width = 0;

            c1Transaction.Cols[COL_MOD1_CODE].Width = 30;
            c1Transaction.Cols[COL_MOD1_DESC].Width = 30;
            c1Transaction.Cols[COL_MOD2_CODE].Width = 30;
            c1Transaction.Cols[COL_MOD2_DESC].Width = 30;
            c1Transaction.Cols[COL_MOD3_CODE].Width = 0;
            c1Transaction.Cols[COL_MOD3_DESC].Width = 0;
            c1Transaction.Cols[COL_MOD4_CODE].Width = 0;
            c1Transaction.Cols[COL_MOD4_DESC].Width = 0;

            c1Transaction.Cols[COL_CHARGES].Width = 60;
            c1Transaction.Cols[COL_UNIT].Width = 30;
            c1Transaction.Cols[COL_TOTAL].Width = 70;
            c1Transaction.Cols[COL_ALLOWED].Width = 70;
            c1Transaction.Cols[COL_PROVIDER_ID].Width = 0;
            c1Transaction.Cols[COL_PROVIDER].Width = 150;
            c1Transaction.Cols[COL_NOTE_DATA].Width = 0;
            c1Transaction.Cols[COL_NOTE_TYPE].Width = 0;

            #endregion

            #region "Show/Hide"
            c1Transaction.Cols[COL_NO].Visible = true;
            c1Transaction.Cols[COL_TRANSACTIONID].Visible = false;

            c1Transaction.Cols[COL_DATEFROM].Visible = true;
            c1Transaction.Cols[COL_DATETO].Visible = false;

            c1Transaction.Cols[COL_POSCODE].Visible = true;
            c1Transaction.Cols[COL_POSDESC].Visible = true;

            c1Transaction.Cols[COL_TOSCODE].Visible = true;
            c1Transaction.Cols[COL_TOSDESC].Visible = true;

            c1Transaction.Cols[COL_CPT_CODE].Visible = true;
            c1Transaction.Cols[COL_CPT_DESC].Visible = true;

            c1Transaction.Cols[COL_DX1_CODE].Visible = true;
            c1Transaction.Cols[COL_DX1_DESC].Visible = true;
            c1Transaction.Cols[COL_DX2_CODE].Visible = true;
            c1Transaction.Cols[COL_DX2_DESC].Visible = true;
            c1Transaction.Cols[COL_DX3_CODE].Visible = true;
            c1Transaction.Cols[COL_DX3_DESC].Visible = true;
            c1Transaction.Cols[COL_DX4_CODE].Visible = true;
            c1Transaction.Cols[COL_DX4_DESC].Visible = true;
            //c1Transaction.Cols[COL_DX5_CODE].Visible = false; //
            //c1Transaction.Cols[COL_DX5_DESC].Visible = false;
            //c1Transaction.Cols[COL_DX6_CODE].Visible = false; //
            //c1Transaction.Cols[COL_DX6_DESC].Visible = false;
            //c1Transaction.Cols[COL_DX7_CODE].Visible = false; //
            //c1Transaction.Cols[COL_DX7_DESC].Visible = false;
            //c1Transaction.Cols[COL_DX8_CODE].Visible = false; //
            //c1Transaction.Cols[COL_DX8_DESC].Visible = false;

            c1Transaction.Cols[COL_DX1_PTR].Visible = true;
            c1Transaction.Cols[COL_DX2_PTR].Visible = true;
            c1Transaction.Cols[COL_DX3_PTR].Visible = true;
            c1Transaction.Cols[COL_DX4_PTR].Visible = true;
            c1Transaction.Cols[COL_DX5_PTR].Visible = false;
            c1Transaction.Cols[COL_DX6_PTR].Visible = false;
            c1Transaction.Cols[COL_DX7_PTR].Visible = false;
            c1Transaction.Cols[COL_DX8_PTR].Visible = false;


            c1Transaction.Cols[COL_MOD1_CODE].Visible = true;
            c1Transaction.Cols[COL_MOD1_DESC].Visible = true;
            c1Transaction.Cols[COL_MOD2_CODE].Visible = true;
            c1Transaction.Cols[COL_MOD2_DESC].Visible = true;
            //c1Transaction.Cols[COL_MOD3_CODE].Visible = false;
            //c1Transaction.Cols[COL_MOD3_DESC].Visible = false;
            //c1Transaction.Cols[COL_MOD4_CODE].Visible = false;
            //c1Transaction.Cols[COL_MOD4_DESC].Visible = false;

            c1Transaction.Cols[COL_CHARGES].Visible = true;
            c1Transaction.Cols[COL_UNIT].Visible = true;
            c1Transaction.Cols[COL_TOTAL].Visible = true;
            c1Transaction.Cols[COL_ALLOWED].Visible = true;
            c1Transaction.Cols[COL_PROVIDER_ID].Visible = false;
            c1Transaction.Cols[COL_PROVIDER].Visible = true;
            c1Transaction.Cols[COL_NOTE_DATA].Visible = false;
            c1Transaction.Cols[COL_NOTE_TYPE].Visible = false;
            #endregion

            #region "Header"
            c1Transaction.SetData(0, COL_NO, "No.");
            c1Transaction.SetData(0, COL_TRANSACTIONID, "TransactionId");

            c1Transaction.SetData(0, COL_DATEFROM, "Date");
            c1Transaction.SetData(0, COL_DATETO, "Date To");

            c1Transaction.SetData(0, COL_POSCODE, "POS");
            c1Transaction.SetData(0, COL_POSDESC, "POS Description");

            c1Transaction.SetData(0, COL_TOSCODE, "TOS");
            c1Transaction.SetData(0, COL_TOSDESC, "TOS Description");

            c1Transaction.SetData(0, COL_CPT_CODE, "CPT");
            c1Transaction.SetData(0, COL_CPT_DESC, "CPT Desc");

            c1Transaction.SetData(0, COL_DX1_CODE, "Dx1");
            c1Transaction.SetData(0, COL_DX1_DESC, "Dx1 Desc");
            c1Transaction.SetData(0, COL_DX2_CODE, "Dx2");
            c1Transaction.SetData(0, COL_DX2_DESC, "Dx2 Desc");
            c1Transaction.SetData(0, COL_DX3_CODE, "Dx3");
            c1Transaction.SetData(0, COL_DX3_DESC, "Dx3 Desc");
            c1Transaction.SetData(0, COL_DX4_CODE, "Dx4");
            c1Transaction.SetData(0, COL_DX4_DESC, "Dx4 Desc");

            c1Transaction.SetData(0, COL_DX1_PTR, "D1");
            c1Transaction.SetData(0, COL_DX2_PTR, "D2");
            c1Transaction.SetData(0, COL_DX3_PTR, "D3");
            c1Transaction.SetData(0, COL_DX4_PTR, "D4");


            c1Transaction.SetData(0, COL_MOD1_CODE, "M1");
            c1Transaction.SetData(0, COL_MOD1_DESC, "M1 Desc");
            c1Transaction.SetData(0, COL_MOD2_CODE, "M2");
            c1Transaction.SetData(0, COL_MOD2_DESC, "M2 Desc");


            c1Transaction.SetData(0, COL_CHARGES, "Charges");
            c1Transaction.SetData(0, COL_UNIT, "Unit");
            c1Transaction.SetData(0, COL_TOTAL, "Total");
            c1Transaction.SetData(0, COL_ALLOWED, "Allowed");
            c1Transaction.SetData(0, COL_PROVIDER_ID, "ProviderID");
            c1Transaction.SetData(0, COL_PROVIDER, "Renderring Provider");
            c1Transaction.SetData(0, COL_NOTE_DATA, "Note");
            c1Transaction.SetData(0, COL_NOTE_TYPE, "Note Type");
            #endregion

            c1Transaction.Cols[COL_TOTAL].AllowEditing = false;

        }

        public bool SetLineTransaction(TransactionLines LineTransactionsData)
        {
            int rowIndex = 0;
            bool _returnResult = false;
            TransactionLine LineTransactionData = null;

            try
            {
                if (LineTransactionsData != null)
                {
                    if (LineTransactionsData.Count > 0)
                    {
                        for (int i = 0; i < LineTransactionsData.Count; i++)
                        {
                          //  LineTransactionData = new TransactionLine();
                            LineTransactionData = LineTransactionsData[i];

                            rowIndex = c1Transaction.Rows.Count;
                            c1Transaction.Rows.Add();

                            c1Transaction.SetData(rowIndex, COL_NO, LineTransactionData.TransactionLineId);
                            c1Transaction.SetData(rowIndex, COL_TRANSACTIONID, LineTransactionData.TransactionId);

                            c1Transaction.SetData(rowIndex, COL_DATEFROM, LineTransactionData.DateServiceFrom.ToString("dd-MMM-yy"));
                            c1Transaction.SetData(rowIndex, COL_DATETO, LineTransactionData.DateServiceTill.ToString("dd-MMM-yy"));

                            c1Transaction.SetData(rowIndex, COL_POSCODE, LineTransactionData.POSCode.ToString());
                            c1Transaction.SetData(rowIndex, COL_POSDESC, LineTransactionData.POSDescription.ToString());

                            c1Transaction.SetData(rowIndex, COL_TOSCODE, LineTransactionData.TOSCode.ToString());
                            c1Transaction.SetData(rowIndex, COL_TOSDESC, LineTransactionData.TOSDescription.ToString());

                            c1Transaction.SetData(rowIndex, COL_CPT_CODE, LineTransactionData.CPTCode.ToString());
                            c1Transaction.SetData(rowIndex, COL_CPT_DESC, LineTransactionData.CPTDescription.ToString());

                            c1Transaction.SetData(rowIndex, COL_DX1_CODE, LineTransactionData.Dx1Code.ToString());
                            c1Transaction.SetData(rowIndex, COL_DX1_DESC, LineTransactionData.Dx1Description.ToString());
                            c1Transaction.SetData(rowIndex, COL_DX2_CODE, LineTransactionData.Dx2Code.ToString());
                            c1Transaction.SetData(rowIndex, COL_DX2_DESC, LineTransactionData.Dx2Description.ToString());
                            c1Transaction.SetData(rowIndex, COL_DX3_CODE, LineTransactionData.Dx3Code.ToString());
                            c1Transaction.SetData(rowIndex, COL_DX3_DESC, LineTransactionData.Dx3Description.ToString());
                            c1Transaction.SetData(rowIndex, COL_DX4_CODE, LineTransactionData.Dx4Code.ToString());
                            c1Transaction.SetData(rowIndex, COL_DX4_DESC, LineTransactionData.Dx4Description.ToString());
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

                            c1Transaction.SetData(rowIndex, COL_MOD1_CODE, LineTransactionData.Mod1Code.ToString());
                            c1Transaction.SetData(rowIndex, COL_MOD1_DESC, LineTransactionData.Mod1Description.ToString());
                            c1Transaction.SetData(rowIndex, COL_MOD2_CODE, LineTransactionData.Mod2Code.ToString());
                            c1Transaction.SetData(rowIndex, COL_MOD2_DESC, LineTransactionData.Mod2Description.ToString());
                            c1Transaction.SetData(rowIndex, COL_MOD3_CODE, LineTransactionData.Mod3Code.ToString());
                            c1Transaction.SetData(rowIndex, COL_MOD3_DESC, LineTransactionData.Mod3Description.ToString());
                            c1Transaction.SetData(rowIndex, COL_MOD4_CODE, LineTransactionData.Mod4Code.ToString());
                            c1Transaction.SetData(rowIndex, COL_MOD4_DESC, LineTransactionData.Mod4Description.ToString());

                            c1Transaction.SetData(rowIndex, COL_CHARGES, LineTransactionData.Charges.ToString());
                            c1Transaction.SetData(rowIndex, COL_UNIT, LineTransactionData.Unit.ToString());
                            c1Transaction.SetData(rowIndex, COL_TOTAL, LineTransactionData.Total.ToString());
                            c1Transaction.SetData(rowIndex, COL_ALLOWED, LineTransactionData.AllowedCharges.ToString());
                            c1Transaction.SetData(rowIndex, COL_PROVIDER_ID, LineTransactionData.RefferingProviderId.ToString());
                            string _RefferringProviderName = "";
                            if (LineTransactionData.RefferingProviderId > 0)
                            {
                                gloAppointmentBook.Books.Resource oResource = new gloAppointmentBook.Books.Resource(_databaseconnectionstring);
                                _RefferringProviderName = oResource.GetProviderName(LineTransactionData.RefferingProviderId);
                                oResource.Dispose();
                            }
                            c1Transaction.SetData(rowIndex, COL_PROVIDER, Convert.ToString(_RefferringProviderName));

                            //Funtionality not implemented yet
                            //c1Transaction.SetData(rowIndex, COL_NOTE_DATA, LineTransactionData.Note.ToString());
                            //c1Transaction.SetData(rowIndex, COL_NOTE_TYPE, LineTransactionData.NoteOfType);
                            //

                            //Dispose the Temporary Object //SLR: This is only a reference.
                            //if (LineTransactionData != null)
                            //{
                            //    LineTransactionData.Dispose();
                            //    LineTransactionData = null;
                            //}

                        }//end - for (int i = 0; i < LineTransactionsData.Count; i++)

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
                //SLR: it is only a reference..
                //if (LineTransactionsData != null)
                //{
                //    LineTransactionsData.Dispose();
                //    LineTransactionsData = null;
                //}
            }
            return _returnResult;
        }

        private void c1Transaction_RowColChange(object sender, EventArgs e)
        {
            Int64 _TransactionId = 0;
            DataTable dtTransaction = null;
            gloBilling ogloBilling = new gloBilling(_databaseconnectionstring, "");
            string _FacilityCode = "";
            try
            {
                if (c1Transaction.Rows.Count > 0)
                {
                    if (c1Transaction.RowSel > 0)
                    {
                        _TransactionId = Convert.ToInt64(c1Transaction.GetData(c1Transaction.RowSel, COL_TRANSACTIONID));
                     //   if (_TransactionId != null)
                        {
                            if (_TransactionId > 0)
                            {
                                dtTransaction = ogloBilling.GetTransaction(_TransactionId, _ClinicID);
                                if (dtTransaction != null)
                                {
                                    if (dtTransaction.Rows.Count > 0)
                                    {
                                        _FacilityCode = Convert.ToString(dtTransaction.Rows[0]["sFacilityCode"]);
                                        if (_FacilityCode != DBNull.Value.ToString() && _FacilityCode != "")
                                        {
                                            FillFacilityInfo(_FacilityCode,_Transaction.ProviderID);
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
                if (dtTransaction != null)
                { dtTransaction.Dispose(); }

                if (ogloBilling != null)
                { ogloBilling.Dispose(); }
            }
        }

        #endregion " C1 Grid Region "

        #region " Insurance Tree After Select Event "

        private void trvPatientInsurences_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                txtInsurenceID.Text = "";
                txtInsurenceSubscriberLastName.Text = "";
                txtInsurenceGroup.Text = "";
                chkInsurenceIsPrimary.Checked = false;

                if (e.Node.Level != 0)
                {
                    //nInsuranceID,InsuranceName ,sSubscriberID, sSubscriberName, sSubscriberPolicy#,sGroup,sPhone ,bPrimaryFlag 
                    //dtDOB,dtEffectiveDate,dtExpiryDate,sSubscriberID

                    DataRow drInsurence = (DataRow)trvPatientInsurences.SelectedNode.Tag;

                    if (drInsurence != null && drInsurence.Table.Rows.Count > 0)
                    {
                        txtInsurenceID.Text = Convert.ToString(drInsurence["sSubscriberPolicy#"]);
                        txtInsurenceSubscriberLastName.Text = Convert.ToString(drInsurence["sSubscriberName"]);
                        txtInsurenceGroup.Text = Convert.ToString(drInsurence["sGroup"]);
                        chkInsurenceIsPrimary.Checked = Convert.ToBoolean(drInsurence["bPrimaryFlag"]);
                        dtpSubscriberDOB.Text = Convert.ToString(drInsurence["dtDOB"]);
                        dtpInsuranceEffectiveDate.Text = Convert.ToString(drInsurence["dtEffectiveDate"]);
                        dtpInsuranceExpiryDate.Text = Convert.ToString(drInsurence["dtExpiryDate"]);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        #endregion " Insurance Tree After Select Event "

        #region " Combo Events "

        private void cmbClinic_SelectedIndexChanged(object sender, EventArgs e)
        {
           // Int64 _SelectedClinicId = 0;

            try
            {
                //if (cmbClinic.Items.Count > 0)
                //{
                //    _SelectedClinicId = Convert.ToInt64(cmbClinic.SelectedValue);
                //    if (_SelectedClinicId != null && _SelectedClinicId > 0)
                //    {
                //        FillSubmitterInfo(_SelectedClinicId);
                //    }
                //}
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {

            }
        }

        private void cmbBillingProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int64 _SelectedProviderId = 0;
            try
            {
                if (cmbBillingProvider.Items.Count > 0)
                {
                    _SelectedProviderId = Convert.ToInt64(cmbBillingProvider.SelectedValue);
                    if (_SelectedProviderId > 0)
                    {
                        FillProviderDetails(_SelectedProviderId, ProviderType.BillingProvider);
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
        }

        private void cmbPTPName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int64 _SelectedProviderId = 0;
            try
            {
                if (cmbPTPName.Items.Count > 0)
                {
                    _SelectedProviderId = Convert.ToInt64(cmbPTPName.SelectedValue);
                    if ( _SelectedProviderId > 0)
                    {
                        FillProviderDetails(_SelectedProviderId, ProviderType.PayToProvider);
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
        }

        private void cmbRefferingProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int64 _SelectedProviderId = 0;
            try
            {
                if (cmbRefferingProvider.Items.Count > 0)
                {
                    _SelectedProviderId = Convert.ToInt64(cmbRefferingProvider.SelectedValue);
                    if ( _SelectedProviderId > 0)
                    {
                        FillProviderDetails(_SelectedProviderId, ProviderType.RefferingProvider);
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
        }

        #endregion " Combo Events "

        #region " Public & Private Methods "

        private void FillClinic()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtClinic = new DataTable();
            string _sqlQuery = "";

            try
            {
                oDB.Connect(false);
                _sqlQuery = "SELECT nClinicID,sClinicName FROM Clinic_MST";
                oDB.Retrive_Query(_sqlQuery, out dtClinic);
                if (dtClinic != null && dtClinic.Rows.Count > 0)
                {
                    cmbClinic.DataSource = dtClinic;
                    cmbClinic.DisplayMember = dtClinic.Columns["sClinicName"].ToString();
                    cmbClinic.ValueMember = dtClinic.Columns["nClinicID"].ToString();
                    cmbClinic.SelectedValue = this._ClinicID;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB.Connect(false)) { oDB.Disconnect(); }
                if (oDB != null) { oDB.Dispose(); }


            }
        }

        private void FillProvider()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtProviders = new DataTable();
            string _sqlQuery = "";

            try
            {
                oDB.Connect(false);
                _sqlQuery = "SELECT ISNULL(sFirstName,'')+SPACE(1)+ISNULL(sMiddleName,'')+SPACE(1)+ISNULL(sLastName,'')+SPACE(1) AS ProviderName,nProviderID AS ProviderID FROM Provider_MST";
                oDB.Retrive_Query(_sqlQuery, out dtProviders);
               
                if (dtProviders != null && dtProviders.Rows.Count > 0)
                {

                    cmbPTPName.DataSource = dtProviders.Copy();
                    cmbPTPName.DisplayMember = dtProviders.Columns["ProviderName"].ToString();
                    cmbPTPName.ValueMember = dtProviders.Columns["ProviderID"].ToString();
                    cmbPTPName.SelectedIndex = 0;

                    cmbBillingProvider.DataSource = dtProviders.Copy();
                    cmbBillingProvider.DisplayMember = dtProviders.Columns["ProviderName"].ToString();
                    cmbBillingProvider.ValueMember = dtProviders.Columns["ProviderID"].ToString();
                    cmbBillingProvider.SelectedIndex = 0;

                    cmbRefferingProvider.DataSource = dtProviders.Copy();
                    cmbRefferingProvider.DisplayMember = dtProviders.Columns["ProviderName"].ToString();
                    cmbRefferingProvider.ValueMember = dtProviders.Columns["ProviderID"].ToString();
                    cmbRefferingProvider.SelectedIndex = 0;

                   
                }
                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (oDB.Connect(false)) { oDB.Disconnect(); }
                if (oDB != null) { oDB.Dispose(); }

            }
        }

        private void FillPatientInsurances(Int64 PatientID)
        {
            DataTable dtPatientInsurances = new DataTable();
            gloBilling ogloBilling = new gloBilling(_databaseconnectionstring, "");
            try
            {

                //gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(_databaseconnectionstring);
                //dtPatientInsurances = ogloPatient.getPatientInsurances(_PatientID);
                //if (dtPatientInsurances != null)
                //{
                //    trvPatientInsurences.Nodes.Clear();
                //    TreeNode oNode;
                //    oNode = new TreeNode();
                //    oNode.Text = "Insurances";
                //    trvPatientInsurences.Nodes.Add(oNode);
                //    oNode = null;
                //    for (int i = 0; i < dtPatientInsurances.Rows.Count; i++)
                //    {
                //        //nInsuranceID,InsuranceName ,sSubscriberID, sSubscriberName, sSubscriberPolicy#,sGroup,sPhone ,bPrimaryFlag 
                //        //dtDOB,dtEffectiveDate,dtExpiryDate,sSubscriberID
                //        oNode = new TreeNode();
                //        oNode.Text = dtPatientInsurances.Rows[i]["InsuranceName"].ToString();
                //        oNode.Tag = dtPatientInsurances.Rows[i];
                //        txtPayerName.Text = dtPatientInsurances.Rows[i]["InsuranceName"].ToString();
                //        txtPayerID.Text = dtPatientInsurances.Rows[i]["nInsuranceID"].ToString();

                //        if (Convert.ToBoolean(dtPatientInsurances.Rows[i]["bPrimaryFlag"]) == true)
                //        {
                //            oNode.ForeColor = Color.IndianRed;
                //        }

                //        trvPatientInsurences.Nodes[0].Nodes.Add(oNode);
                //        oNode = null;
                //    }
                //    trvPatientInsurences.ExpandAll();
                //}
                // FillInsurances();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

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

        private void FillPatientInformation(Int64 PatientID)
        {
            //gloBilling oBill = new gloBilling(_databaseconnectionstring, "");
           
            //DataTable dt = new DataTable();
            //DataTable dtClinic = new DataTable();
            gloPatient.Patient oPatient = null;
       //     gloPatient.Referrals oReferral = new gloPatient.Referrals();
            
            try
            {
                //oPatient = ogloPatient.GetPatientDemo(PatientID);
                gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(_databaseconnectionstring);
                oPatient = ogloPatient.GetPatient(PatientID);
                ogloPatient.Dispose();
                ogloPatient = null;
                if (oPatient != null)
                {
                    _PatientAccountNo = oPatient.DemographicsDetail.PatientCode;
                    //Added on 20081030 by Anil
                    _PatientAddress = oPatient.DemographicsDetail.PatientAddress1;
                    _PatientCity = oPatient.DemographicsDetail.PatientCity;
                    _PatientCode = oPatient.DemographicsDetail.PatientCode;
                    _PatientDOB = Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oPatient.DemographicsDetail.PatientDOB.ToShortDateString()));
                    _PatientFirstName = oPatient.DemographicsDetail.PatientFirstName;
                    _PatientGender = oPatient.DemographicsDetail.PatientGender;
                    _PatientLastName = oPatient.DemographicsDetail.PatientLastName;
                    _PatientMiddleName = oPatient.DemographicsDetail.PatientMiddleName;
                    _PatientSSN = oPatient.DemographicsDetail.PatientSSN;
                    _PatientState = oPatient.DemographicsDetail.PatientState;
                    _PatientZip = oPatient.DemographicsDetail.PatientZip;

                    gloPatient.Referrals oReferral;
                    oReferral = oPatient.Referrals;
                    if (oReferral.Count > 0)
                    {
                        gloDatabaseLayer.DBLayer oDB = null;
                         oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                        DataTable dtReferral = new DataTable();
                        string _sqlQuery = "";

                        oDB.Connect(false);
                        _sqlQuery = " SELECT sStreet, sCity, sState, sZIP, sFirstName, sMiddleName, sLastName, sGender, nSpecialtyID, sTaxID, sUPIN, sNPI, sContactType, sTaxonomy, sTaxonomyDesc, nContactID " +
                                    " FROM Contacts_MST  " +
                                    " WHERE (nContactID = " + oReferral[0].ReferralID + ") AND (sContactType = 'Referral')";
                        oDB.Retrive_Query(_sqlQuery, out dtReferral);
                        if (dtReferral != null && dtReferral.Rows.Count > 0)
                        {
                            _ReferralFName = dtReferral.Rows[0]["sFirstName"].ToString();
                            _ReferralLName = dtReferral.Rows[0]["sLastName"].ToString();
                            _ReferralMName = dtReferral.Rows[0]["sMiddleName"].ToString();
                            _ReferralCity = dtReferral.Rows[0]["sCity"].ToString();
                            _ReferralState = dtReferral.Rows[0]["sState"].ToString();
                            _ReferralZIP = dtReferral.Rows[0]["sZIP"].ToString();
                            _ReferralNPI = dtReferral.Rows[0]["sNPI"].ToString();
                            _ReferralEmployerID = dtReferral.Rows[0]["sTaxID"].ToString();
                            _ReferralTaxonomy = dtReferral.Rows[0]["sTaxonomy"].ToString();
                        }
                        if (dtReferral != null)
                        {
                            dtReferral.Dispose();
                            dtReferral = null;
                        }
                        oDB.Disconnect();
                        oDB.Dispose();
                        oDB = null;
                    }
                    oPatient.Dispose();
                    oPatient = null;
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

        private void FillProviderDetails(long _SelectedProviderId, ProviderType _ProviderType)
        {
            Resource oResource = new Resource(_databaseconnectionstring);
            
         //   DataTable dtProviderDetails = null;
            Provider _Provider = null;
            Object _objResult = null;
            string strBillingSetting = "";
            string strRenderingSetting = "";
            try
            {
                
                _Provider = oResource.GetProviderDetail(_SelectedProviderId);

                if (_Provider != null)
                {
                    switch (_ProviderType)
                    {
                        case ProviderType.BillingProvider:
                            {
                                gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(_databaseconnectionstring); 
                                oSettings.GetSetting("BillingSetting", _SelectedProviderId, _ClinicID, out _objResult);
                                oSettings.Dispose();
                                oSettings = null;
                                if (_objResult != null)
                                {
                                    // |Company|Practice|Business"
                                    strBillingSetting = Convert.ToString(_objResult);
                                }

                                _BillingFName = _Provider.FirstName;
                                _BillingLName = _Provider.LastName;
                                _BillingMName = _Provider.MiddleName;
                                _BillingNPI = _Provider.NPI;
                                _BillingStateMedicalNo = _Provider.StateMedicalNo;
                                _BillingSSN = _Provider.SSN;
                                _BillingEmployerID = _Provider.EmployerID;
                                _BillingTaxonomy = _Provider.Taxonomy;
                                
                                switch (strBillingSetting)
                                {
                                    case "Business":
                                        {
                                            _BillingAddress = _Provider.BMAddress1;
                                            _BillingCity = _Provider.BMCity;
                                            _BillingState = _Provider.BMState;
                                            _BillingZIP = _Provider.BMZIP;
                                        } break;
                                    case "Practice":
                                        {
                                            _BillingAddress = _Provider.BPracAddress1;
                                            _BillingCity = _Provider.BPracCity;
                                            _BillingState = _Provider.BPracState;
                                            _BillingZIP = _Provider.BPracZIP;
                                        } break;
                                    case "Company":
                                        {
                                            _BillingAddress = _Provider.CompanyAddress1;
                                            _BillingCity = _Provider.CompanyCity;
                                            _BillingState = _Provider.CompanyState;
                                            _BillingZIP = _Provider.CompanyZip;
                                        } break;
                                    default:
                                        _BillingAddress = _Provider.BMAddress1;
                                        _BillingCity = _Provider.BMCity;
                                        _BillingState = _Provider.BMState;
                                        _BillingZIP = _Provider.BMZIP;
                                        break;
                                }
                            }
                            break;
                        case ProviderType.PayToProvider:
                            {
                                //txtPTPAddress.Text = _Provider.BMAddress1;
                                //txtPTPCity.Text = _Provider.BMCity;
                                //txtPTPState.Text = _Provider.BMState;
                                //txtPTPZip.Text = _Provider.BMZIP;
                                //txtPTPNPI_ID.Text = _Provider.NPI;
                                //txtPTPUPIN.Text = _Provider.UPIN;
                            }
                            break;
                        case ProviderType.RefferingProvider:
                            {
                                _ReferralFName = _Provider.FirstName;
                                _ReferralAddress = _Provider.BMAddress1;
                                _ReferralLName = _Provider.LastName;
                                _ReferralMName = _Provider.MiddleName;
                                _ReferralCity = _Provider.BMCity;
                                _ReferralState = _Provider.BMState;
                                _ReferralZIP = _Provider.BMZIP;
                                _ReferralNPI = _Provider.NPI;
                                _ReferralStateMedicalNo = _Provider.StateMedicalNo;
                                _ReferralSSN = _Provider.SSN;
                                _ReferralEmployerID = _Provider.EmployerID;
                                _ReferralTaxonomy = _Provider.Taxonomy;

                            }
                            break;
                        case ProviderType.RenderingProvider:
                            {
                                gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(_databaseconnectionstring); 
                            
                                oSettings.GetSetting("RenderingSetting", _SelectedProviderId, _ClinicID, out _objResult);
                                oSettings.Dispose();
                                oSettings = null;
                                if (_objResult != null)
                                {
                                    // |Company|Practice|Business"
                                    strRenderingSetting = Convert.ToString(_objResult);
                                }

                                _RenderingFName = _Provider.FirstName;
                                _RenderingLName = _Provider.LastName;
                                _RenderingMName = _Provider.MiddleName;
                                _RenderingNPI = _Provider.NPI;
                                _RenderingStateMedicalNo = _Provider.StateMedicalNo;
                                _RenderingSSN = _Provider.SSN;
                                _RenderingEmployerID = _Provider.EmployerID;
                                _RenderingTaxonomy = _Provider.Taxonomy;

                                switch (strRenderingSetting)
                                {
                                    case "Business":
                                        {
                                            _RenderingAddress = _Provider.BMAddress1;
                                            _RenderingCity = _Provider.BMCity;
                                            _RenderingState = _Provider.BMState;
                                            _RenderingZIP = _Provider.BMZIP;

                                        } break;
                                    case "Practice":
                                        {
                                            _RenderingAddress = _Provider.BPracAddress1;
                                            _RenderingCity = _Provider.BPracCity;
                                            _RenderingState = _Provider.BPracState;
                                            _RenderingZIP = _Provider.BPracZIP;
                                        } break;
                                    case "Company":
                                        {
                                            _RenderingAddress = _Provider.CompanyAddress1;
                                            _RenderingCity = _Provider.CompanyCity;
                                            _RenderingState = _Provider.CompanyState;
                                            _RenderingZIP = _Provider.CompanyZip;
                                        } break;
                                    default:
                                        _RenderingAddress = _Provider.BMAddress1;
                                        _RenderingCity = _Provider.BMCity;
                                        _RenderingState = _Provider.BMState;
                                        _RenderingZIP = _Provider.BMZIP;
                                        break;
                                }

                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (_Provider != null) { _Provider.Dispose(); }
                if (oResource != null) { oResource.Dispose(); }
            }
        }

        private void GetTransaction()
        {
            gloBilling ogloBilling = new gloBilling(_databaseconnectionstring, "");
            Common.Transaction oTransaction = null;
            try
            {
                if (SelectedTransactions != null)
                {
                    if (SelectedTransactions.Count > 0)
                    {
                        for (int i = 0; i < SelectedTransactions.Count; i++)
                        {
                            oTransaction = ogloBilling.GetTransactionDetails(Convert.ToInt64(SelectedTransactions[i]), _ClinicID);
                            if (oTransaction != null)
                            {
                                ////1.Fill Facility Information 
                                //FillFacilityInfo(oTransaction.FacilityCode,oTransaction.ProviderID);

                                //2.Set Transaction Lines to the Grid
                                if (oTransaction.Lines != null)
                                {
                                    if (oTransaction.Lines.Count > 0)
                                    { SetLineTransaction(oTransaction.Lines); }
                                }
                                oTransaction.Dispose();
                                oTransaction = null;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Selected Transaction have no transaction lines.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Select Transaction", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                _Transaction = oTransaction;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
    
            }
            finally
            {
                if (oTransaction != null)
                {
                    oTransaction.Dispose();
                }
                if (ogloBilling != null)
                {
                    ogloBilling.Dispose();
                    ogloBilling = null;
                }
            }
        }

        private void FillSubmitterInfo(Int64 _SelectedClinicId, Int64 _nProviderID)
        {
            gloBilling oBill = new gloBilling(_databaseconnectionstring, "");
            DataTable dt = null;
            try
            {
                dt = oBill.GetSubmitterInfo(_SelectedClinicId, _nProviderID);
                if (dt != null && dt.Rows.Count > 0)
                {
                    //nClinicID,sAddress1,sAddress2,sStreet,sCity,sState,sZIP,sPhoneNo,sMobileNo,
                    //sFAX,sTAXID,sContactPersonName,sContactPersonAddress1,sContactPersonAddress2,sContactPersonPhone,
                    //sContactPersonFAX,sContactPersonMobile
                    _SubmitterName = Convert.ToString(dt.Rows[0]["SubmitterName"]);
                    _SubmitterAddress = Convert.ToString(dt.Rows[0]["SubmitterAddress1"]) + " " + Convert.ToString(dt.Rows[0]["SubmitterAddress2"]);
                    _SubmitterCity = Convert.ToString(dt.Rows[0]["SubmitterCity"]);
                    _SubmitterState = Convert.ToString(dt.Rows[0]["SubmitterState"]);
                    _SubmitterZIP = Convert.ToString(dt.Rows[0]["SubmitterZIP"]);
                    if (Convert.ToString(dt.Rows[0]["SubmitterContactName"]) == "")
                    {
                        _SubmitterContactPersonName = Convert.ToString(dt.Rows[0]["SubmitterName"]);
                    }
                    else
                    {
                        _SubmitterContactPersonName = Convert.ToString(dt.Rows[0]["SubmitterContactName"]);
                    }
                    _SubmitterContactPersonNo = Convert.ToString(dt.Rows[0]["SubmitterPhone"]);
                    _SubmitterETIN = "C0923";

                }
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oBill != null)
                {
                    oBill.Dispose();
                    oBill = null;
                }
            }

        }

        private void FillFacilityInfo(string FacilityCode, Int64 _nProviderID)
        {
            gloBilling oBill = new gloBilling(_databaseconnectionstring, "");
            DataTable dt = null;
            //DataTable dtFacility = new DataTable();
            try
            {
                if (FacilityCode != null && FacilityCode != "")
                {
                    dt = oBill.GetFacilityInfo(FacilityCode, _nProviderID);
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    //_FacilityCode = dt.Rows[0]["sFacilityCode"].ToString();
                    _FacilityAddress = dt.Rows[0]["FacilityAddress1"].ToString();
                    _FacilityCity = dt.Rows[0]["FacilityCity"].ToString();
                    _FacilityName = dt.Rows[0]["FacilityName"].ToString();
                    _FacilityState = dt.Rows[0]["FacilityState"].ToString();
                    _FacilityZip = dt.Rows[0]["FacilityZip"].ToString();
                    _FacilityNPI = dt.Rows[0]["FacilityNPI"].ToString();
                }
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oBill != null)
                {
                    oBill.Dispose();
                    oBill = null;
                }
            }


        }

        public bool ValidateData()
        {
            if (txtClaimDate.Text.Length < 6 || txtClaimDate.Text.Length > 6)
            {
                MessageBox.Show("Invalid Claim Date.Please check the format");
                return false;
            }
            if (txtClaimTime.Text.Length < 4 || txtClaimTime.Text.Length > 4)
            {
                MessageBox.Show("Invalid Claim Time.Please check the format");
                return false;
            }
            if (txtControlNo.Text.Length < 9 || txtControlNo.Text.Length > 9)
            {
                MessageBox.Show("Control Number should contain nine characters");
                return false;
            }
            return true;
        }

        public string getBillDate()
        {
            string strDay = DateTime.Now.Date.Day.ToString();
            if (strDay.Length < 2)
            {
                strDay = "0" + strDay;
            }
            string strMonth = DateTime.Now.Date.Month.ToString();
            if (strMonth.Length < 2)
            {
                strMonth = "0" + strMonth;
            }
            string strYear = DateTime.Now.Date.Year.ToString();
            strYear = strYear.Remove(0, 2);

            string date = strYear + strMonth + strDay; //YYMMDD
            return date;

        }

        public void FillAllCombosWithDefaults()
        {
            if (cmb_CommunicationQualifier.Items.Count > 0)
            {
                cmb_CommunicationQualifier.SelectedIndex = 0;
            }
            if (cmbBHT_TSPurposeCode.Items.Count > 0)
            {
                cmbBHT_TSPurposeCode.SelectedIndex = 0;
            }
            if (cmbBHT_TSTypeCode.Items.Count > 0)
            {
                cmbBHT_TSTypeCode.SelectedIndex = 0;
            }
            if (cmbBillingProvider.Items.Count > 0)
            {
                cmbBillingProvider.SelectedIndex = 0;
            }
            if (cmbBLCodeQualifier.Items.Count > 0)
            {
                cmbBLCodeQualifier.SelectedIndex = 0;
            }
            if (cmbClinic.Items.Count > 0)
            {
                cmbClinic.SelectedIndex = 0;
            }
            if (cmbContactFunctionCode.Items.Count > 0)
            {
                cmbContactFunctionCode.SelectedIndex = 0;
            }
            if (cmbPTPIDCodeQualifier.Items.Count > 0)
            {
                cmbPTPIDCodeQualifier.SelectedIndex = 0;
            }
            if (cmbPTPName.Items.Count > 0)
            {
                cmbPTPName.SelectedIndex = 0;
            }
            if (cmbRecIDCodeQual.Items.Count > 0)
            {
                cmbRecIDCodeQual.SelectedIndex = 0;
            }
            if (cmbREF_ReferenceIdCode.Items.Count > 0)
            {
                cmbREF_ReferenceIdCode.SelectedIndex = 0;
            }
            if (cmbREF_RefPrReferenceCodeQualifier.Items.Count > 0)
            {
                cmbREF_RefPrReferenceCodeQualifier.SelectedIndex = 0;
            }
            if (cmbRefferingProvider.Items.Count > 0)
            {
                cmbRefferingProvider.SelectedIndex = 0;
            }
            if (cmbRefProvCodeQualifier.Items.Count > 0)
            {
                cmbRefProvCodeQualifier.SelectedIndex = 0;
            }
            if (cmbSubmitterIdentifierCodeQualifier.Items.Count > 0)
            {
                cmbSubmitterIdentifierCodeQualifier.SelectedIndex = 0;
            }
        }

        public void FillInsurances(Int64 PatientID)
        {
            DataTable dtPatientInsurances = null;
           // gloBilling ogloBilling = new gloBilling(_databaseconnectionstring, "");
            try
            {
                bSecondaryInsurance = false;
                gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(_databaseconnectionstring);
                dtPatientInsurances = ogloPatient.getPatientInsurances(PatientID);
                ogloPatient.Dispose();
                ogloPatient = null;
                if (dtPatientInsurances != null && dtPatientInsurances.Rows.Count >0)
                {
                    for (int i = 0; i < dtPatientInsurances.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            //Primary Insurance
                            _PayerName = Convert.ToString(dtPatientInsurances.Rows[0]["InsuranceName"]);
                            _SubscriberAddress = Convert.ToString(dtPatientInsurances.Rows[0]["SubscriberAddr1"]);
                            _SubscriberCity = Convert.ToString(dtPatientInsurances.Rows[0]["SubscriberCity"]);
                            _SubscriberState = Convert.ToString(dtPatientInsurances.Rows[0]["SubscriberState"]);
                            _SubscriberZIP = Convert.ToString(dtPatientInsurances.Rows[0]["SubscriberZip"]);
                            _SubscriberRelationshipCode = Convert.ToString(dtPatientInsurances.Rows[0]["RelationshipCode"]);
                            _SubscriberMName = Convert.ToString(dtPatientInsurances.Rows[0]["SubMName"]);
                            _SubscriberLName = Convert.ToString(dtPatientInsurances.Rows[0]["SubLName"]);
                            _SubscriberFName = Convert.ToString(dtPatientInsurances.Rows[0]["SubFName"]);
                            _SubscriberDOB = Convert.ToString(dtPatientInsurances.Rows[0]["dtDOB"]);
                            _SubscriberGender = Convert.ToString(dtPatientInsurances.Rows[0]["sSubscriberGender"]);
                            _SubscriberInsuranceBelongs = Convert.ToString(dtPatientInsurances.Rows[0]["InsuranceTypeCode"]);//"CI"; 
                            _SubscriberInsuranceID = Convert.ToString(dtPatientInsurances.Rows[0]["sSubscriberID"]);
                            _SubscriberInsurancePST = "P";//Convert.ToString(dtPatientInsurances.Rows[0][""]);
                            _SubscriberGroupID = Convert.ToString(dtPatientInsurances.Rows[0]["sGroup"]);
                            _PayerID = Convert.ToString(dtPatientInsurances.Rows[0]["PayerID"]);
                            _PayerAddress = Convert.ToString(dtPatientInsurances.Rows[0]["PayerAddress1"]); ;
                            _PayerCity = Convert.ToString(dtPatientInsurances.Rows[0]["PayerCity"]); ;
                            _PayerState = Convert.ToString(dtPatientInsurances.Rows[0]["PayerState"]); ;
                            _PayerZip = Convert.ToString(dtPatientInsurances.Rows[0]["PayerZip"]); ;

                            //Anil Added on 20081030
                            _PriorAuthorizationNo = GetPriorAuthorizationNumber(_PatientID, Convert.ToInt64(dtPatientInsurances.Rows[0]["nInsuranceID"]));
                           
                        }
                        else if (i == 1)
                        {
                            //Secondary Insurance
                            bSecondaryInsurance = true;
                            _OtherInsuranceAddress = Convert.ToString(dtPatientInsurances.Rows[i]["SubscriberAddr1"]);
                            _OtherInsuranceName = Convert.ToString(dtPatientInsurances.Rows[i]["InsuranceName"]);
                            _OtherInsuranceSubscriberFName = Convert.ToString(dtPatientInsurances.Rows[i]["SubFName"]);
                            _OtherInsuranceSubscriberLName = Convert.ToString(dtPatientInsurances.Rows[i]["SubLName"]);
                            _OtherInsuranceSubscriberMName = Convert.ToString(dtPatientInsurances.Rows[i]["SubMName"]);
                            _OtherInsuranceSubscriberGender = Convert.ToString(dtPatientInsurances.Rows[i]["sSubscriberGender"]);
                            _OtherInsuranceSubscriberDOB = Convert.ToString(dtPatientInsurances.Rows[i]["dtDOB"]);
                            _OtherInsuranceZIP = Convert.ToString(dtPatientInsurances.Rows[i]["SubscriberZip"]);
                            _OtherInsuranceType = Convert.ToString(dtPatientInsurances.Rows[i]["InsuranceTypeCode"]);//"CI"
                            _OtherInsuranceState = Convert.ToString(dtPatientInsurances.Rows[i]["SubscriberState"]);
                            _OtherInsuranceRelationshipCode = Convert.ToString(dtPatientInsurances.Rows[i]["RelationshipCode"]);
                            _OtherInsurancePST = "S"; //Convert.ToString(dtPatientInsurances.Rows[i][""]);
                            _OtherInsurancePayerID = Convert.ToString(dtPatientInsurances.Rows[i]["PayerID"]);
                            _OtherInsuranceID = Convert.ToString(dtPatientInsurances.Rows[i]["sSubscriberID"]);
                            _OtherInsuranceGroupID = Convert.ToString(dtPatientInsurances.Rows[i]["sGroup"]);
                            _OtherInsuranceCity = Convert.ToString(dtPatientInsurances.Rows[i]["SubscriberCity"]);
                        }
                    }
                }
                if (dtPatientInsurances != null)
                {
                    dtPatientInsurances.Dispose();
                    dtPatientInsurances = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }

        private void FillClearingHouseInfo()
        {
            gloBilling oBill = new gloBilling(_databaseconnectionstring, "");
            DataTable dt = null;
            try
            {
                dt = oBill.GetClearingHouseSettings();
                if (dt != null && dt.Rows.Count > 0)
                {
                    _SenderID = Convert.ToString(dt.Rows[0]["sSubmitterID"]);
                    _ReceiverID = Convert.ToString(dt.Rows[0]["sReceiverID"]);
                    _SenderName = Convert.ToString(dt.Rows[0]["sSenderCode"]);
                    _ReceiverCode = Convert.ToString(dt.Rows[0]["sVenderIDCode"]);

                }
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oBill != null)
                {
                    oBill.Dispose();
                    oBill = null;
                }
            }
        }

        private string GetPriorAuthorizationNumber(Int64 PatientID,Int64 InsuranceID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _strSQL = "";
            Object _result = null;
            string _PriorAuthorizationNo = "";
            try
            {
                _strSQL = "SELECT sAuthorizationNumber FROM PatientPriorAuthorization WHERE nPatientID=" + PatientID +"  AND nInsuranceID=" + InsuranceID +" ";
                oDB.Connect(false);
                _result= oDB.ExecuteScalar_Query(_strSQL);
                if (_result != null)
                {
                    _PriorAuthorizationNo = Convert.ToString(_result);
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
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return _PriorAuthorizationNo;
        }

        private void FillAllDetails(Transaction _Transaction)
        {
            try
            {
                if (_Transaction != null)
                {
                    if (Convert.ToInt64(_Transaction.ProviderID) != 0 && _Transaction.ProviderID.ToString() != "")
                    {
                        FillProviderDetails(Convert.ToInt64(_Transaction.ProviderID), ProviderType.BillingProvider);
                       // FillPatientInsurances(_Transaction.PatientID);
                        FillInsurances(_Transaction.PatientID);
                        FillPatientInformation(_Transaction.PatientID);
                        FillSubmitterInfo(Convert.ToInt64(_Transaction.ClinicID), Convert.ToInt64(_Transaction.ProviderID));
                        FillFacilityInfo(_Transaction.FacilityCode, _Transaction.ProviderID);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }

        private bool IsValidICD9(string ICD9Code)//Used for generation of EDI
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            object ReturnValue = new object();
            string _sqlQuery = "";
            bool _retVal = true;

            try
            {
                oDB.Connect(false);
                _sqlQuery = "select ISNULL(sICD9Code,'') AS sICD9Code from ICD9_InvalidEDI where UPPER(sICD9Code) = '" + ICD9Code.ToUpper() + "'";
                ReturnValue = oDB.ExecuteScalar_Query(_sqlQuery);
                if (ReturnValue != null && ReturnValue != DBNull.Value && Convert.ToString(ReturnValue) != "")
                {
                    string _message = "ICD9 is Invalid." + Environment.NewLine + "Code : " + ICD9Code + "  " + Environment.NewLine + "Do you want to Continue? ";//" + Environment.NewLine + ""Description : " + Convert.ToString(ReturnValue) + "
                    if (MessageBox.Show(_message, _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                    {
                        _retVal = false;
                    }
                }

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                if (ReturnValue != null) { ReturnValue = null; }
            }
            return _retVal;
        } 

        private bool IsValidICD9Code(string ICD9Code)//Used For Validation
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            object ReturnValue = new object();
            string _sqlQuery = "";
            bool _retVal = true;

            try
            {
                oDB.Connect(false);
                _sqlQuery = "select ISNULL(sICD9Code,'') AS sICD9Code from ICD9_InvalidEDI where UPPER(sICD9Code) = '" + ICD9Code.ToUpper() + "'";
                ReturnValue = oDB.ExecuteScalar_Query(_sqlQuery);
                if (ReturnValue != null && ReturnValue != DBNull.Value && Convert.ToString(ReturnValue) != "")
                {
                    //string _message = "ICD9 is Invalid." + Environment.NewLine + "Code : " + ICD9Code + "  " + Environment.NewLine + "Do you want to Continue? ";//" + Environment.NewLine + ""Description : " + Convert.ToString(ReturnValue) + "
                    //if (MessageBox.Show(_message, _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                    //{
                    _retVal = false;
                    //}
                }

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                if (ReturnValue != null) { ReturnValue = null; }
            }
            return _retVal;
        }

        private string FormattedTime(string TimeFormat)
        {
            int _length = 0;
            _length = TimeFormat.Length;
            if (_length == 0)
            {
                TimeFormat = "0000";
            }
            if (_length == 1)
            {
                TimeFormat = "000" + TimeFormat;
            }
            else if (_length == 2)
            {
                TimeFormat = "00" + TimeFormat;
            }
            else if (_length == 3)
            {
                TimeFormat = "0" + TimeFormat;
            }
            else if (_length == 4)
            {
          //      TimeFormat = TimeFormat;
            }
            return TimeFormat;
        }
        
        private void LoadEDIObject()
        {
            try
            {
                //Here Interchange Loop should come
                sPath = AppDomain.CurrentDomain.BaseDirectory;
                sSEFFile = "837_X098A1.SEF";     //ToDO :Give the file name at runtime, since it can change
                sEdiFile = "837A1.x12";

                oEdiDoc = new ediDocument();
                ediDocument.Set(ref oEdiDoc, new ediDocument());
                ediSchemas.Set(ref oSchemas, (ediSchemas)oEdiDoc.GetSchemas());
                oSchemas.EnableStandardReference = false;

                //oEdiDoc.SegmentTerminator = "~\r\n";
                //oEdiDoc.ElementTerminator = "*";
                //oEdiDoc.CompositeTerminator = ":";

                //oEdiDoc.CursorType = DocumentCursorTypeConstants.Cursor_ForwardWrite;
                //oEdiDoc.set_Property(DocumentPropertyIDConstants.Property_DocumentBufferIO, 2000);
                
                ediSchema.Set(ref oSchema, (ediSchema)oEdiDoc.LoadSchema("837_X098A1.SEF", 0));
                System.IO.FileInfo ofile = new System.IO.FileInfo(sPath + sSEFFile);
                if (ofile.Exists == false)
                {
                    MessageBox.Show("SEF file is not present in the base directory.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void ValidateEDIData()
        {
            gloBilling ogloBilling = new gloBilling(_databaseconnectionstring, "");
            string _Message = "";
            Transaction oTransaction = null;
            string strMissingText = "";
            string _MessageHeader = "";
           // string _FilePath = AppDomain.CurrentDomain.BaseDirectory;
            string _FilePath = gloSettings.FolderSettings.AppTempFolderPath;
            try
            {
                _MessageHeader += "";

                FillClearingHouseInfo();

                FillSubmitterInfo(Convert.ToInt64(_ClinicID), 0);

                //ISA and GS Settings
                if (_SenderID == "")
                {
                    strMissingText += "Sender ID" + Environment.NewLine + "" + Environment.NewLine + "";
                }
                if (_ReceiverID == "")
                {
                    strMissingText += "Receiver ID" + Environment.NewLine + "";
                }
                if (_SenderName == "")
                {
                    strMissingText += "Sender Code" + Environment.NewLine + "";
                }
                if (_ReceiverCode == "")
                {
                    strMissingText += "Receiver Code" + Environment.NewLine + "";
                }


                //Submitter
                if (_SubmitterName == "")
                {
                    strMissingText += "Submitter Name" + Environment.NewLine + "";
                }
                if (_SubmitterContactPersonName == "")
                {
                    strMissingText += "Submitter Contact Person Name" + Environment.NewLine + "";
                }
                if (_SubmitterContactPersonNo == "")
                {
                    strMissingText += "Submitter Contact Person Number" + Environment.NewLine + "";
                }
                if (_SubmitterCity == "")
                {
                    strMissingText += "Submitter City" + Environment.NewLine + "";
                }
                if (_SubmitterState == "")
                {
                    strMissingText += "Submitter State" + Environment.NewLine + "";
                }
                if (_SubmitterZIP == "")
                {
                    strMissingText += "Submitter Zip" + Environment.NewLine + "";
                }
                if (_SubmitterETIN == "")
                {
                    strMissingText += "Submitter ETIN" + Environment.NewLine + "";
                }
                if (_SubmitterAddress == "")
                {
                    strMissingText += "Submitter Address" + Environment.NewLine + "";
                }
                if (strMissingText.Trim() != "")
                {
                    _MessageHeader = _MessageHeader + strMissingText;
                }
                else
                {
                    _MessageHeader = "";
                }


                if (SelectedTransactions != null)
                {
                    if (SelectedTransactions.Count > 0)
                    {
                        for (int i = 0; i < SelectedTransactions.Count; i++)
                        {
                            string strMessage = "";
                           // oTransaction = new Transaction();
                            //   TransactionLine oTransLine = null;
                            oTransaction = ogloBilling.GetTransactionDetails(Convert.ToInt64(SelectedTransactions[i]), _ClinicID);
                            string _ClaimMessageHeader = "";

                            if (oTransaction != null)
                            {
                                if (oTransaction.Lines.Count > 0)
                                {
                                    FillAllDetails(oTransaction);
                                    _ClaimMessageHeader = " " + Environment.NewLine + "For Patient: " + _PatientFirstName.Trim() + " " + _PatientLastName.Trim() + "  and Claim Number: " + oTransaction.ClaimNo.ToString() + " " + Environment.NewLine + "" + Environment.NewLine + "";
                                    for (int j = 0; j < oTransaction.Lines.Count; j++)
                                    {

                                    }
                                    if (Convert.ToString(oTransaction.Lines[0].Dx1Code).Trim() != "")
                                    {
                                        if (IsValidICD9Code(Convert.ToString(oTransaction.Lines[0].Dx1Code.Trim())) == false)
                                        {
                                            strMessage += "Invalid ICD9 Code1: " + Convert.ToString(oTransaction.Lines[0].Dx1Code.Trim()) + "" + Environment.NewLine + "";
                                        }
                                    }
                                    if (Convert.ToString(oTransaction.Lines[0].Dx2Code).Trim() != "")
                                    {
                                        if (IsValidICD9Code(Convert.ToString(oTransaction.Lines[0].Dx2Code.Trim())) == false)
                                        {
                                            strMessage += "Invalid ICD9 Code2: " + Convert.ToString(oTransaction.Lines[0].Dx2Code.Trim()) + "" + Environment.NewLine + "";
                                        }
                                    }
                                    if (Convert.ToString(oTransaction.Lines[0].Dx3Code).Trim() != "")
                                    {
                                        if (IsValidICD9Code(Convert.ToString(oTransaction.Lines[0].Dx3Code.Trim())) == false)
                                        {
                                            strMessage += "Invalid ICD9 Code3: " + Convert.ToString(oTransaction.Lines[0].Dx3Code.Trim()) + "" + Environment.NewLine + "";
                                        }
                                    }
                                    if (Convert.ToString(oTransaction.Lines[0].Dx4Code).Trim() != "")
                                    {
                                        if (IsValidICD9Code(Convert.ToString(oTransaction.Lines[0].Dx4Code.Trim())) == false)
                                        {
                                            strMessage += "Invalid ICD9 Code4: " + Convert.ToString(oTransaction.Lines[0].Dx4Code.Trim()) + "" + Environment.NewLine + "";
                                        }
                                    }
                                }
                            }
                            //Billing Provider
                            if (_BillingFName.Trim() == "")
                            {
                                strMessage += "Billing Provider First Name" + Environment.NewLine + "";
                            }
                            if (_BillingLName.Trim() == "")
                            {
                                strMessage += "Billing Provider Last Name" + Environment.NewLine + "";
                            }
                            if (_BillingMName.Trim() == "")
                            {
                                //strMessage += "Billing Provider Middle Name"+Environment.NewLine+"";
                            }
                            if (_BillingCity.Trim() == "")
                            {
                                strMessage += "Billing Provider City" + Environment.NewLine + "";
                            }
                            if (_BillingState.Trim() == "")
                            {
                                strMessage += "Billing Provider State" + Environment.NewLine + "";
                            }
                            if (_BillingAddress.Trim() == "")
                            {
                                strMessage += "Billing Provider Address" + Environment.NewLine + "";
                            }
                            if (_BillingZIP.Trim() == "")
                            {
                                strMessage += "Billing Provider Zip" + Environment.NewLine + "";
                            }
                            if (_BillingNPI.Trim() == "")
                            {
                                strMessage += "Billing Provider NPI" + Environment.NewLine + "";
                            }
                            if (_BillingSSN.Trim() == "")
                            {
                                //strMessage += "Billing Provider SSN" + Environment.NewLine + "";
                            }
                            if (_BillingEmployerID.Trim() == "")
                            {
                                strMessage += "Billing Provider Employer ID" + Environment.NewLine + "";
                            }
                            if (_BillingStateMedicalNo.Trim() == "")
                            {
                                //strMessage += "Billing Provider State Medical No" + Environment.NewLine + "";
                            }
                            if (_BillingTaxonomy.Trim() == "")
                            {
                                strMessage += "Billing Provider Taxonomy" + Environment.NewLine + "";
                            }
                            if (oTransaction != null)
                            {
                                //Facility Information
                                if (oTransaction.FacilityCode.Trim() != "")
                                {
                                    if (_FacilityName == "")
                                    {
                                        strMessage += "Facility Name" + Environment.NewLine + "";
                                    }
                                    if (_FacilityAddress == "")
                                    {
                                        strMessage += "Facility Address" + Environment.NewLine + "";
                                    }
                                    if (_FacilityCity == "")
                                    {
                                        strMessage += "Facility City" + Environment.NewLine + "";
                                    }
                                    if (_FacilityState == "")
                                    {
                                        strMessage += "Facility State" + Environment.NewLine + "";
                                    }
                                    if (_FacilityZip == "")
                                    {
                                        strMessage += "Facility Zip" + Environment.NewLine + "";
                                    }
                                    if (_FacilityNPI == "")
                                    {
                                        strMessage += "Facility NPI" + Environment.NewLine + "";
                                    }
                                }
                                oTransaction.Dispose();
                                oTransaction = null;

                            }
                            //Receiver
                            //if (_ReceiverName == "")
                            //{
                            //    strMessage += "Receiver Name" + Environment.NewLine + "";
                            //}
                            //if (_ReceiverETIN == "")
                            //{
                            //    strMessage += "Receiver ETIN" + Environment.NewLine + "";
                            //}

                            //Subscriber
                            if (_SubscriberLName == "")
                            {
                                strMessage += "Subscriber Last Name" + Environment.NewLine + "";
                            }
                            if (_SubscriberInsurancePST == "")
                            {
                                strMessage += "Subscriber Insurance Type(P/S/T)" + Environment.NewLine + "";
                            }
                            if (_SubscriberRelationshipCode == "")
                            {
                                strMessage += "Subscriber Relationship Code" + Environment.NewLine + "";
                            }
                            if (_SubscriberInsuranceBelongs == "")
                            {
                                strMessage += "Subscriber Insurance belongs to Type" + Environment.NewLine + "";
                            }
                            if (_SubscriberFName == "")
                            {
                                strMessage += "Subscriber First Name" + Environment.NewLine + "";
                            }
                            if (_SubscriberMName == "")
                            {
                                // strMessage += "Subscriber Middle Name"+Environment.NewLine+"";
                            }
                            if (_SubscriberInsuranceID == "")
                            {
                                strMessage += "Subscriber Insurance ID" + Environment.NewLine + "";
                            }
                            if (_SubscriberAddress == "")
                            {
                                strMessage += "Subscriber Address" + Environment.NewLine + "";
                            }
                            if (_SubscriberGroupID == "")
                            {
                                strMessage += "Subscriber Group ID" + Environment.NewLine + "";
                            }
                            if (_SubscriberCity == "")
                            {
                                strMessage += "Subscriber City" + Environment.NewLine + "";
                            }
                            if (_SubscriberState == "")
                            {
                                strMessage += "Subscriber State" + Environment.NewLine + "";
                            }
                            if (_SubscriberZIP == "")
                            {
                                strMessage += "Subscriber Zip" + Environment.NewLine + "";
                            }
                            if (_SubscriberDOB == "")
                            {
                                strMessage += "Subscriber Date of Birth" + Environment.NewLine + "";
                            }
                            if (_SubscriberGender == "")
                            {
                                strMessage += "Subscriber Gender" + Environment.NewLine + "";
                            }

                            //Payer
                            if (_PayerName == "")
                            {
                                strMessage += "Payer/Insurance Name" + Environment.NewLine + "";
                            }
                            if (_PayerID == "")
                            {
                                strMessage += "Payer ID" + Environment.NewLine + "";
                            }
                            if (_PayerAddress == "")
                            {
                                //strMessage += "Payer Address" + Environment.NewLine + "";
                            }
                            if (_PayerCity == "")
                            {
                                //strMessage += "Payer City" + Environment.NewLine + "";
                            }
                            if (_PayerState == "")
                            {
                                //strMessage += "Payer State" + Environment.NewLine + "";
                            }
                            if (_PayerZip == "")
                            {
                                //strMessage += "Payer Zip" + Environment.NewLine + "";
                            }

                            if (_PatientAccountNo == "")
                            {
                                strMessage += "Patient Account No" + Environment.NewLine + "";
                            }



                            if (bSecondaryInsurance == true)
                            {
                                //Other Insurance
                                if (_OtherInsuranceSubscriberLName == "")
                                {
                                    strMessage += "Second Insurance Subscriber Last Name" + Environment.NewLine + "";
                                }
                                if (_OtherInsurancePST == "")
                                {
                                    strMessage += "Second Insurance Type" + Environment.NewLine + "";
                                }
                                if (_OtherInsuranceType == "")
                                {
                                    strMessage += "Second Insurance Belongs to Type" + Environment.NewLine + "";
                                }
                                if (_OtherInsuranceRelationshipCode == "")
                                {
                                    strMessage += "Second Insurance Subscriber Relationship Code" + Environment.NewLine + "";
                                }
                                if (_OtherInsuranceID == "")
                                {
                                    strMessage += "Second Insurance ID" + Environment.NewLine + "";
                                }
                                if (_OtherInsuranceGroupID == "")
                                {
                                    strMessage += "Second Insurance Group ID" + Environment.NewLine + "";
                                }
                                if (_OtherInsuranceAddress == "")
                                {
                                    strMessage += "Second Insurance Address" + Environment.NewLine + "";
                                }
                                if (_OtherInsuranceSubscriberFName == "")
                                {
                                    strMessage += "Second Insurance Subscriber First Name" + Environment.NewLine + "";
                                }
                                if (_OtherInsuranceSubscriberMName == "")
                                {
                                    //strMessage += "Second Insurance Subscriber Middle Name" + Environment.NewLine + "";
                                }
                                if (_OtherInsuranceName == "")
                                {
                                    strMessage += "Second Insurance Name" + Environment.NewLine + "";
                                }
                                if (_OtherInsurancePayerID == "")
                                {
                                    strMessage += "Second Insurance Payer ID" + Environment.NewLine + "";
                                }
                                if (_OtherInsuranceCity == "")
                                {
                                    strMessage += "Second Insurance City" + Environment.NewLine + "";
                                }
                                if (_OtherInsuranceState == "")
                                {
                                    strMessage += "Second Insurance State" + Environment.NewLine + "";
                                }
                                if (_OtherInsuranceZIP == "")
                                {
                                    strMessage += "Second Insurance Zip" + Environment.NewLine + "";
                                }
                                if (_OtherInsuranceSubscriberDOB == "")
                                {
                                    strMessage += "Second Insurance Subscriber Date of Birth" + Environment.NewLine + "";
                                }
                                if (_OtherInsuranceSubscriberGender == "")
                                {
                                    strMessage += "Second Insurance Subscriber Gender" + Environment.NewLine + "";
                                }
                            }

                            //Patient Information
                            if (_PatientLastName == "")
                            {
                                strMessage += "Patient Last Name" + Environment.NewLine + "";
                            }
                            if (_PatientFirstName == "")
                            {
                                strMessage += "Patient First Name" + Environment.NewLine + "";
                            }
                            if (_PatientMiddleName == "")
                            {
                                //strMessage += "Patient Middle Name" + Environment.NewLine + "";
                            }
                            if (_PatientSSN == "")
                            {
                                strMessage += "Patient SSN" + Environment.NewLine + "";
                            }
                            if (_PatientGender == "")
                            {
                                strMessage += "Patient Gender" + Environment.NewLine + "";
                            }
                            if (_PatientDOB == "")
                            {
                                strMessage += "Patient Date of Birth" + Environment.NewLine + "";
                            }
                            if (_PatientAddress == "")
                            {
                                strMessage += "Patient Address" + Environment.NewLine + "";
                            }
                            if (_PatientCity == "")
                            {
                                strMessage += "Patient City" + Environment.NewLine + "";
                            }
                            if (_PatientState == "")
                            {
                                strMessage += "Patient State" + Environment.NewLine + "";
                            }
                            if (_PatientZip == "")
                            {
                                strMessage += "Patient Zip" + Environment.NewLine + "";
                            }
                            if (_PatientCode == "")
                            {
                                strMessage += "Patient Code" + Environment.NewLine + "";
                            }

                            //Prior Authorization Number
                            if (_PriorAuthorizationNo == "")
                            {
                                //strMessage += "Prior Authorization Number" + Environment.NewLine + "";
                            }
                            if (strMessage.Trim() != "")
                            {
                                //_ClaimMessageHeader = _ClaimMessageHeader + strMessage;
                                _MessageHeader += _ClaimMessageHeader + strMessage;
                                //_Message = _MessageHeader;
                            }


                        }

                        // //Rendering Provider
                        //_RenderingFName = ""+Environment.NewLine+"";
                        //_RenderingLName = ""+Environment.NewLine+"";
                        //_RenderingMName = ""+Environment.NewLine+"";
                        //_RenderingAddress = ""+Environment.NewLine+"";
                        //_RenderingCity = ""+Environment.NewLine+"";
                        //_RenderingState = ""+Environment.NewLine+"";
                        //_RenderingZIP = ""+Environment.NewLine+"";
                        //_RenderingNPI = ""+Environment.NewLine+"";
                        //_RenderingSSN = ""+Environment.NewLine+"";
                        //_RenderingEmployerID = ""+Environment.NewLine+"";
                        //_RenderingStateMedicalNo = ""+Environment.NewLine+"";
                        //_RenderingTaxonomy = ""+Environment.NewLine+"";


                    }
                    if (_MessageHeader != "")
                    {
                        _Message = "";
                        _Message = _MessageHeader;
                    }
                }

                if (_Message.Trim() != "")
                {
                    string _Header = "Following fields are missing in database:" + Environment.NewLine + "" + Environment.NewLine + "";
                    _Header += _Message;
                    _FilePath = _FilePath + "EDIValidation.txt";
                    System.IO.StreamWriter oStreamWriter = new System.IO.StreamWriter(_FilePath, false);
                    oStreamWriter.WriteLine(_Header);
                    oStreamWriter.Close();
                    oStreamWriter.Dispose();
                    System.Diagnostics.Process.Start(_FilePath);

                }
                else
                {
                    MessageBox.Show("All mandatory data is present.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        #endregion " Public & Private Methods "


        private void GenerateEDIFile()
        {
            #region " Generate EDI "
 
          //  string sEntity = "";
            string sInstance = "";
           // string _strSQL = "";
          //  DataTable dt;
        //    string _BillingProviderDetails = "";
         //   gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloBilling ogloBilling = new gloBilling(_databaseconnectionstring, "");
      //      bool IsSecondaryInsurance = false;
            Transaction oTransaction = null;

            try
            {


                FillClearingHouseInfo();
                if (SelectedTransactions != null)
                {
                    if (SelectedTransactions.Count > 0)
                    {
                        for (int i = 0; i < SelectedTransactions.Count; i++)
                        {
                           // oTransaction = new Transaction();
                            //    TransactionLine oTransLine = null;
                            oTransaction = ogloBilling.GetTransactionDetails(Convert.ToInt64(SelectedTransactions[i]), _ClinicID);
                            if (oTransaction != null)
                            {
                                if (oTransaction.Lines.Count > 0)
                                {
                                    FillSubmitterInfo(Convert.ToInt64(_ClinicID), oTransaction.ProviderID);
                                }
                                oTransaction.Dispose();
                                oTransaction = null;
                            }
                        }
                    }
                }

                oEdiDoc.New();
                oEdiDoc.CursorType = DocumentCursorTypeConstants.Cursor_ForwardWrite;
                oEdiDoc.set_Property(DocumentPropertyIDConstants.Property_DocumentBufferIO, 2000);

                oEdiDoc.SegmentTerminator = "~\r\n";
                oEdiDoc.ElementTerminator = "*";
                oEdiDoc.CompositeTerminator = ":";


                #region " Interchange Segment "
                //Create the interchange segment
                ediInterchange.Set(ref oInterchange, (ediInterchange)oEdiDoc.CreateInterchange("X", "004010"));
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oInterchange.GetDataSegmentHeader());

                oSegment.set_DataElementValue(1, 0, "00");
                oSegment.set_DataElementValue(3, 0, "00");
                oSegment.set_DataElementValue(5, 0, "ZZ");
                oSegment.set_DataElementValue(6, 0, _SenderID.Trim());//"1234545");//
                oSegment.set_DataElementValue(7, 0, "ZZ");
                oSegment.set_DataElementValue(8, 0, _ReceiverID.Trim());//"V2EL");//
                string ISA_Date = Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString()));
                oSegment.set_DataElementValue(9, 0, ISA_Date.Substring(2));
                string ISA_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString()));

                oSegment.set_DataElementValue(10, 0, FormattedTime(ISA_Time).Trim());
                oSegment.set_DataElementValue(11, 0, "U");
                oSegment.set_DataElementValue(12, 0, "00401");
                oSegment.set_DataElementValue(13, 0, "000000020");//
                oSegment.set_DataElementValue(14, 0, "0");
                oSegment.set_DataElementValue(15, 0, "T");
                oSegment.set_DataElementValue(16, 0, ":");

                #endregion " Interchange Segment "

                #region " Functional Group "

                //Create the functional group segment
                ediGroup.Set(ref oGroup, (ediGroup)oInterchange.CreateGroup("004010X098A1"));
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oGroup.GetDataSegmentHeader());
                oSegment.set_DataElementValue(1, 0, "HC");
                oSegment.set_DataElementValue(2, 0, _SenderName);
                oSegment.set_DataElementValue(3, 0, _ReceiverCode.Trim());//"ClarEDI");
                oSegment.set_DataElementValue(4, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())));
                string GS_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString()));
                oSegment.set_DataElementValue(5, 0, FormattedTime(GS_Time).Trim());
                oSegment.set_DataElementValue(6, 0, "1");
                oSegment.set_DataElementValue(7, 0, "X");
                oSegment.set_DataElementValue(8, 0, "004010X098A1");

                #endregion " Functional Group "

                #region ST - TRANSACTION SET HEADER

                ediTransactionSet.Set(ref oTransactionset, (ediTransactionSet)oGroup.CreateTransactionSet("837"));
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.GetDataSegmentHeader());
                oSegment.set_DataElementValue(2, 0, "00021");//"ControlNo"

                #endregion ST - TRANSACTION SET HEADER

                #region BHT - BEGINNING OF HIERARCHICAL TRANSACTION

                //Begining Segment 
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("BHT"));
                oSegment.set_DataElementValue(1, 0, "0019"); //Herarchical Structure Code
                oSegment.set_DataElementValue(2, 0, "00"); //00-Original, 01-Re-issue
                oSegment.set_DataElementValue(3, 0, "1234"); //Reference identification
                oSegment.set_DataElementValue(4, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())).Trim());//Date of claim
                string BHT_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim();
                oSegment.set_DataElementValue(5, 0, FormattedTime(BHT_Time).Trim()); //"1230");
                oSegment.set_DataElementValue(6, 0, "CH"); //CH-Chargeable, RP-Reporting
                #endregion BHT - BEGINNING OF HIERARCHICAL TRANSACTION

                #region REF - TRANSMISSION TYPE IDENTIFICATION

                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("REF"));
                oSegment.set_DataElementValue(1, 0, "87");
                oSegment.set_DataElementValue(2, 0, "004010X098A1");//"ReferenceID"

                #endregion REF - TRANSMISSION TYPE IDENTIFICATION

                #region NM1 - SUBMITTER


                //1000A SUBMITTER
                //NM1 SUBMITTER
                if (_SubmitterName.Trim() != "" && _SubmitterETIN.Trim() != "" && _SubmitterContactPersonName.Trim() != "" && _SubmitterContactPersonNo.Trim() != "")
                {
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("NM1\\NM1"));
                    oSegment.set_DataElementValue(1, 0, "41");
                    oSegment.set_DataElementValue(2, 0, "2");
                    oSegment.set_DataElementValue(3, 0, _SubmitterName);//cmbClinic.Text.Trim());// clinic name
                    oSegment.set_DataElementValue(8, 0, "46"); // Identification Code Qualifier 
                    oSegment.set_DataElementValue(9, 0, _SubmitterETIN);//txtSubIdentificationCode.Text.Trim());//clinic code or Electronic Transmitter Identification No.


                    //PER SUBMITTER EDI CONTACT INFORMATION
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("NM1\\PER"));
                    oSegment.set_DataElementValue(1, 0, "IC");
                    oSegment.set_DataElementValue(2, 0, _SubmitterContactPersonName);//txtSubmitterContactName.Text.Trim());//Contact person name of clinic
                    oSegment.set_DataElementValue(3, 0, "TE");
                    oSegment.set_DataElementValue(4, 0, _SubmitterContactPersonNo);//txtSubmitterPhone.Text.Trim().Replace("(", "").Replace(")", "").Replace("-", ""));//clinic phone
                }
                else
                {
                    //MessageBox.Show("Submitter/Clinic information is not complete.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //return;
                }

                #endregion NM1 - SUBMITTER

                #region NM1 - RECEIVER NAME

                //1000B RECEIVER
                //NM1 RECEIVER NAME
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("NM1(2)\\NM1"));
                oSegment.set_DataElementValue(1, 0, "40");
                oSegment.set_DataElementValue(2, 0, "2");
                oSegment.set_DataElementValue(3, 0, "GatewayEDI");//clearing house or contractor or carrier or FI name
                oSegment.set_DataElementValue(8, 0, "46");// Identification Code Qualifier
                oSegment.set_DataElementValue(9, 0, "V2093");//code of carrier/contractor/FI or Electronic Transmitter Identification No.

                #endregion NM1 - RECEIVER NAME

                nHlCount = 0;

                if (SelectedTransactions != null)
                {
                    if (SelectedTransactions.Count > 0)
                    {
                        for (int i = 0; i < SelectedTransactions.Count; i++)
                        {
                           // oTransaction = new Transaction();
                            TransactionLine oTransLine = null;
                            oTransaction = ogloBilling.GetTransactionDetails(Convert.ToInt64(SelectedTransactions[i]), _ClinicID);
                            if (oTransaction != null)
                            {
                                if (oTransaction.Lines.Count > 0)
                                {
                                    FillAllDetails(oTransaction);

                                    for (int nTransactionSet = 1; nTransactionSet <= 1; nTransactionSet++)
                                    {
                                        //**** BILLING/PAY-TO PROVIDER HIERARCHICAL LEVEL *******************************************

                                        nHlCount = nHlCount + 1;
                                        nHlProvParent = nHlCount;
                                        //2000A BILLING/PAY-TO PROVIDER HL LOOP
                                        //HL-BILLING PROVIDER

                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
                                        oSegment.set_DataElementValue(1, 0, nHlCount.ToString().Trim());
                                        oSegment.set_DataElementValue(3, 0, "20");
                                        oSegment.set_DataElementValue(4, 0, "1");

                                        #region Billing Provider

                                        //2010AA BILLING PROVIDER
                                        //NM1 BILLING PROVIDER NAME
                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
                                        oSegment.set_DataElementValue(1, 0, "85");
                                        oSegment.set_DataElementValue(2, 0, "1");
                                        oSegment.set_DataElementValue(3, 0, _BillingLName);//Billing provider name
                                        oSegment.set_DataElementValue(4, 0, _BillingFName);
                                        oSegment.set_DataElementValue(5, 0, _BillingMName);

                                        oSegment.set_DataElementValue(8, 0, "XX");
                                        if (_BillingNPI.Trim() != "")
                                        {
                                            oSegment.set_DataElementValue(9, 0, _BillingNPI);//Billing provider ID/NPI
                                        }
                                        else
                                        {
                                            //MessageBox.Show("Billing Provider NPI is not there.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            //return;
                                        }


                                        //N3 BILLING PROVIDER ADDRESS
                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N3"));
                                        oSegment.set_DataElementValue(1, 0, _BillingAddress);//Provider Address

                                        //N4 BILLING PROVIDER LOCATION
                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N4"));
                                        oSegment.set_DataElementValue(1, 0, _BillingCity);////Provider City
                                        oSegment.set_DataElementValue(2, 0, _BillingState);//Provider state
                                        oSegment.set_DataElementValue(3, 0, _BillingZIP);//Provider ZIP

                                        //REF 
                                        if (_BillingEmployerID.Trim() != "")
                                        {
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\REF"));
                                            oSegment.set_DataElementValue(1, 0, "EI");//Reference Identification Qualifier("EI" stands for-> Employer ID)
                                            if (_BillingEmployerID.Length > 9)
                                            {
                                                _BillingEmployerID = _BillingEmployerID.Substring(0, 9);
                                            }
                                            oSegment.set_DataElementValue(2, 0, _BillingEmployerID);
                                        }
                                        //REF 
                                        else if (_BillingSSN.Trim() != "")
                                        {
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\REF"));
                                            oSegment.set_DataElementValue(1, 0, "SY");//Reference Identification Qualifier("SY" stands for-> Social Security Number)
                                            if (_BillingSSN.Length > 9)
                                            {
                                                _BillingSSN = _BillingSSN.Substring(0, 9);
                                            }
                                            oSegment.set_DataElementValue(2, 0, _BillingSSN);
                                        }
                                        //REF 
                                        ////if (_BillingStateMedicalNo.Trim() != "")
                                        ////{
                                        ////    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\REF"));
                                        ////    oSegment.set_DataElementValue(1, 0, "0B");//Reference Identification Qualifier("0B" stands for-> State Licence No)
                                        ////    if (_BillingStateMedicalNo.Length > 9)
                                        ////    {
                                        ////        _BillingStateMedicalNo = _BillingStateMedicalNo.Substring(0, 9);
                                        ////    }
                                        ////    oSegment.set_DataElementValue(2, 0, _BillingStateMedicalNo);
                                        ////}
                                        #endregion

                                        //'******************************************************************************************************
                                        //'******* SUBSCRIBER HIERARCHICAL LEVEL ****************************************************************
                                        //'******************************************************************************************************
                                        #region Subscriber

                                        #region Subscriber HL Loop - 2000B

                                        nHlCount = nHlCount + 1;
                                        nHlSubscriberParent = nHlCount;

                                        //2000B SUBSCRIBER HL LOOP
                                        //HL-SUBSCRIBER
                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
                                        oSegment.set_DataElementValue(1, 0, nHlCount.ToString().Trim());
                                        oSegment.set_DataElementValue(2, 0, nHlProvParent.ToString().Trim());
                                        oSegment.set_DataElementValue(3, 0, "22");

                                        if (_SubscriberRelationshipCode == "18")
                                        {
                                            oSegment.set_DataElementValue(4, 0, "0");
                                        }
                                        else
                                        {
                                            oSegment.set_DataElementValue(4, 0, "1");

                                        }

                                        //SBR SUBSCRIBER INFORMATION
                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\SBR"));
                                        oSegment.set_DataElementValue(1, 0, _SubscriberInsurancePST);//"P");
                                        if (_SubscriberRelationshipCode == "18")
                                        {
                                            oSegment.set_DataElementValue(2, 0, _SubscriberRelationshipCode);
                                        }
                                        oSegment.set_DataElementValue(9, 0, _SubscriberInsuranceBelongs);//"HM");

                                        //2010BA SUBSCRIBER
                                        //NM1 SUBSCRIBER NAME
                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
                                        oSegment.set_DataElementValue(1, 0, "IL");
                                        oSegment.set_DataElementValue(2, 0, "1");
                                        oSegment.set_DataElementValue(3, 0, _SubscriberLName.Trim());//"SubscriberLastOrgName"
                                        oSegment.set_DataElementValue(4, 0, _SubscriberFName.Trim());//"SubscriberFirstname"
                                        oSegment.set_DataElementValue(8, 0, "MI");
                                        oSegment.set_DataElementValue(9, 0, _SubscriberInsuranceID.Trim());//"Insurance Id"

                                        //N3 SUBSCRIBER ADDRESS
                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N3"));
                                        oSegment.set_DataElementValue(1, 0, _SubscriberAddress.Trim());//"SubscriberAddress"

                                        //N4 SUBSCRIBER CITY
                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N4"));
                                        oSegment.set_DataElementValue(1, 0, _SubscriberCity.Trim());//"SubscriberCity"
                                        oSegment.set_DataElementValue(2, 0, _SubscriberState.Trim());//"SubscrberState"
                                        oSegment.set_DataElementValue(3, 0, _SubscriberZIP.Trim());//"SubscriberZip"

                                        #endregion SubscriberHL Loop - 2000B

                                        if (_SubscriberRelationshipCode == "18")
                                        {
                                            //DMG SUBSCRIBER DEMOGRAPHIC INFORMATION
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\DMG"));
                                            oSegment.set_DataElementValue(1, 0, "D8");
                                            oSegment.set_DataElementValue(2, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(_SubscriberDOB)));//"SubscriberDOB"
                                            if (_SubscriberGender.Trim() != "")
                                            {
                                                if (_SubscriberGender.Trim().ToUpper() == "OTHER")
                                                {
                                                    _SubscriberGender = "U";
                                                }
                                                oSegment.set_DataElementValue(3, 0, _SubscriberGender.Trim().Substring(0, 1).ToUpper());//"SubscriberGender"
                                            }
                                            else
                                            {
                                                oSegment.set_DataElementValue(3, 0, "M");
                                            }

                                            if (_PayerID == "")
                                            {
                                                //MessageBox.Show("Payer ID is not there for the Insurance", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                //return;
                                            }

                                            //oSegment = new ediDataSegment();
                                            //oEdiDoc = null;
                                            //oEdiDoc = new ediDocument();
                                            #region Payer Information Loop 2010BB
                                            //2010BB SUBSCRIBER/PAYER
                                            //NM1 PAYER NAME
                                            string _ModifiedPayerName = _PayerName.Trim();
                                            if (_PayerName.Trim().Length > 35)
                                            {
                                                _ModifiedPayerName = "";
                                                _ModifiedPayerName = _PayerName.Trim().Substring(0, 34);
                                            }
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1(2)\\NM1"));
                                            oSegment.set_DataElementValue(1, 0, "PR");
                                            oSegment.set_DataElementValue(2, 0, "2");
                                            oSegment.set_DataElementValue(3, 0, _ModifiedPayerName.Trim());//"PayerLastOrgName"
                                            oSegment.set_DataElementValue(8, 0, "PI");//This is the payer ID of Medicare for Michigan
                                            oSegment.set_DataElementValue(9, 0, _PayerID.Trim());//dtInsurance.Rows[0]["sPayerID"].ToString());//PayerID

                                            ////////N3 SUBSCRIBER ADDRESS
                                            ////ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1(2)\\N3"));
                                            ////oSegment.set_DataElementValue(1, 0, _PayerAddress.Trim());//"InsuranceAddress"

                                            ////////N4 SUBSCRIBER CITY
                                            ////ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1(2)\\N4"));
                                            ////oSegment.set_DataElementValue(1, 0, _PayerCity.Trim());//"InsuranceCity"
                                            ////oSegment.set_DataElementValue(2, 0, _PayerState.Trim());//"InsuranceState"
                                            ////oSegment.set_DataElementValue(3, 0, _PayerZip.Trim());//"InsuranceZip"

                                            #endregion


                                            //******* SUBSCRIBER CLAIM INFORMATION ***************************************************************
                                            //TODO: Get Details in DATATABLE for the fields to be entered in EDI file.
                                            string _FirstPOS = "";
                                            string _NewPOS = "";
                                            string _ClaimTotal = "";
                                            iItemCount = 0;
                                            decimal _claimAmount = 0;
                                            for (int nLine = 0; nLine <= oTransaction.Lines.Count - 1; nLine++)
                                            {
                                              //  oTransLine = new TransactionLine();
                                                oTransLine = oTransaction.Lines[nLine];

                                                //Anil - 05 Nov 2008
                                                //_claimAmount = _claimAmount + oTransLine.Total;
                                                //_ClaimTotal = Convert.ToString(_claimAmount).Substring(0, Convert.ToInt32(Convert.ToString(oTransLine.Total).Length ));

                                                //Vinayak - 05 Nov 2008
                                                _claimAmount = _claimAmount + oTransLine.Total;

                                                _FirstPOS = oTransaction.Lines[0].POSCode;
                                                _NewPOS = oTransLine.POSCode;
                                            }

                                            _ClaimTotal = _claimAmount.ToString("#0.00");

                                            if (_ClaimTotal.Substring(_ClaimTotal.Length - 2, 2) == "00")
                                            {
                                                _ClaimTotal = _ClaimTotal.Substring(0, _ClaimTotal.Length - 3);
                                            }
                                            //if (_FirstPOS ==_NewPOS)
                                            //{
                                            #region Claim Details - Loop 2300
                                            //2300 CLAIM
                                            //CLM CLAIM LEVEL INFORMATION
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\CLM"));
                                            oSegment.set_DataElementValue(1, 0, _PatientAccountNo); //Patient Account no         
                                            oSegment.set_DataElementValue(2, 0, _ClaimTotal.Trim());// Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_TOTAL))); //Claim Amount

                                            oSegment.set_DataElementValue(5, 1, _FirstPOS.Trim()); //21 - Inpatient Hospital

                                            oSegment.set_DataElementValue(5, 3, "1");
                                            oSegment.set_DataElementValue(6, 0, "Y");
                                            oSegment.set_DataElementValue(7, 0, "A");
                                            oSegment.set_DataElementValue(8, 0, "Y");
                                            oSegment.set_DataElementValue(9, 0, "Y");
                                            oSegment.set_DataElementValue(10, 0, "C");
                                            if (oTransaction.AutoClaim == true)
                                            {
                                                if (oTransaction.AccidentDate.ToString() != "" && oTransaction.AccidentDate > 0)
                                                {
                                                    oSegment.set_DataElementValue(11, 1, "AA");
                                                    oSegment.set_DataElementValue(11, 4, oTransaction.State.Trim());
                                                }
                                            }

                                            string OnsetDate = "";
                                            if (oTransaction.InjuryDate.ToString() != "" || oTransaction.OnsiteDate.ToString() != "" || oTransaction.AccidentDate.ToString() != "")
                                            {
                                                if (oTransaction.InjuryDate.ToString() != "" && oTransaction.InjuryDate > 0)
                                                {
                                                    OnsetDate = Convert.ToString(oTransaction.InjuryDate);
                                                    ////DTP DATE OF ONSET OF CURRENT SYMPTOMS OR ILLNESS
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                    oSegment.set_DataElementValue(1, 0, "431");
                                                    oSegment.set_DataElementValue(2, 0, "D8");
                                                    oSegment.set_DataElementValue(3, 0, OnsetDate);//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpOnsetofCurrentSymptomsorillness.Value.ToShortDateString())).Trim());     //Claim Date
                                                }
                                                else if (oTransaction.OnsiteDate.ToString() != "" && oTransaction.OnsiteDate > 0)
                                                {
                                                    OnsetDate = Convert.ToString(oTransaction.OnsiteDate);
                                                    ////DTP DATE OF CURRENT INJURY
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                    oSegment.set_DataElementValue(1, 0, "431");
                                                    oSegment.set_DataElementValue(2, 0, "D8");
                                                    oSegment.set_DataElementValue(3, 0, OnsetDate);//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpOnsetofCurrentSymptomsorillness.Value.ToShortDateString())).Trim());     //Claim Date
                                                }
                                                if (oTransaction.AutoClaim == true)
                                                {
                                                    if (oTransaction.AccidentDate.ToString() != "" && oTransaction.AccidentDate > 0)
                                                    {
                                                        OnsetDate = Convert.ToString(oTransaction.AccidentDate);
                                                        ////DTP DATE OF ACCIDENT 
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                        oSegment.set_DataElementValue(1, 0, "439");
                                                        oSegment.set_DataElementValue(2, 0, "D8");
                                                        oSegment.set_DataElementValue(3, 0, OnsetDate);//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpOnsetofCurrentSymptomsorillness.Value.ToShortDateString())).Trim());     //Claim Date
                                                    }
                                                }
                                            }

                                            //DTP DATE OF ONSET of similar symptoms or illness
                                            //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                            //oSegment.set_DataElementValue(1, 0, "438");
                                            //oSegment.set_DataElementValue(2, 0, "D8");
                                            //oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpOnsetoSimilarSyptomsorillness.Value.ToShortDateString())).Trim());
                                            //
                                            if (_FirstPOS.Trim() != "11")
                                            {
                                                if (oTransaction.HospitalizationDateFrom > 0 && oTransaction.HospitalizationDateFrom.ToString() != "")
                                                {
                                                    //DTP DATE OF Hospitalization (Admission) 
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                    oSegment.set_DataElementValue(1, 0, "435");
                                                    oSegment.set_DataElementValue(2, 0, "D8");
                                                    oSegment.set_DataElementValue(3, 0, oTransaction.HospitalizationDateFrom.ToString());//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpHospitalizationDate.Value.ToShortDateString())).Trim());     //Claim Date
                                                }

                                                if (oTransaction.HospitalizationDateTo > 0 && oTransaction.HospitalizationDateTo.ToString() != "")
                                                {
                                                    ////DTP DATE OF Discharge 
                                                    //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                    //oSegment.set_DataElementValue(1, 0, "096");
                                                    //oSegment.set_DataElementValue(2, 0, "D8");
                                                    //oSegment.set_DataElementValue(3, 0, oTransaction.HospitalizationDateTo.ToString());//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpHospitalDischargeDate.Value.ToShortDateString())).Trim());     //Claim Date
                                                    ////
                                                }
                                            }

                                            if (oTransaction.WorkersComp == true)
                                            {
                                                if (oTransaction.UnableToWorkFromDate > 0 && oTransaction.UnableToWorkFromDate.ToString() != "")
                                                {
                                                    //DTP DATE OF (Intial Disability period last day worked)
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                    oSegment.set_DataElementValue(1, 0, "297");
                                                    oSegment.set_DataElementValue(2, 0, "D8");
                                                    oSegment.set_DataElementValue(3, 0, oTransaction.UnableToWorkFromDate.ToString());//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpLastDayWorked.Value.ToShortDateString())).Trim());     //Claim Date
                                                    //
                                                }

                                                if (oTransaction.UnableToWorkTillDate > 0 && oTransaction.UnableToWorkTillDate.ToString() != "")
                                                {
                                                    //DTP DATE OF (Intial Disability period return to work)
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                    oSegment.set_DataElementValue(1, 0, "296");
                                                    oSegment.set_DataElementValue(2, 0, "D8");
                                                    oSegment.set_DataElementValue(3, 0, oTransaction.UnableToWorkTillDate.ToString());//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpReturnToWork.Value.ToShortDateString())).Trim());     //Claim Date
                                                    //
                                                }
                                            }
                                            if (_PriorAuthorizationNo.Trim() != "")
                                            {
                                                //REF CLEARING HOUSE CLAIM NUMBER
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\REF"));
                                                oSegment.set_DataElementValue(1, 0, "G1");
                                                oSegment.set_DataElementValue(2, 0, _PriorAuthorizationNo.Trim()); //Claim No
                                            }



                                            #endregion

                                            #region HI - Diagnosis
                                            //HI HEALTH CARE DIAGNOSIS CODES

                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\HI"));
                                            oSegment.set_DataElementValue(1, 1, "BK");
                                            if (oTransaction.Lines[0].Dx1Code.ToString().Trim() != "")
                                            {
                                                oSegment.set_DataElementValue(1, 2, oTransaction.Lines[0].Dx1Code.ToString().Replace(".", "").Trim());// Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_DX1_CODE)));  // "0340");
                                                if (IsValidICD9(Convert.ToString(oTransaction.Lines[0].Dx1Code.Trim())) == false)
                                                {
                                                    return;
                                                }
                                            }
                                            else
                                            {
                                                //MessageBox.Show("Principle Diagnosis is not given.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                //return;
                                            }
                                            if (oTransaction.Lines[0].Dx2Code.ToString().Trim() != "")
                                            {
                                                oSegment.set_DataElementValue(2, 1, "BF");
                                                oSegment.set_DataElementValue(2, 2, oTransaction.Lines[0].Dx2Code.ToString().Replace(".", "").Trim());//Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_DX1_CODE))); // oCase.CaseDiagnosis[0].DiagnosisCode.ToString().Replace(".", "").Trim());
                                                if (IsValidICD9(Convert.ToString(oTransaction.Lines[0].Dx2Code.Trim())) == false)
                                                {
                                                    return;
                                                }
                                            }
                                            if (oTransaction.Lines[0].Dx3Code.ToString().Trim() != "")
                                            {
                                                oSegment.set_DataElementValue(3, 1, "BF");
                                                oSegment.set_DataElementValue(3, 2, oTransaction.Lines[0].Dx3Code.ToString().Replace(".", "").Trim());
                                                if (IsValidICD9(Convert.ToString(oTransaction.Lines[0].Dx3Code.Trim())) == false)
                                                {
                                                    return;
                                                }
                                            }
                                            if (oTransaction.Lines[0].Dx4Code.ToString().Trim() != "")
                                            {
                                                oSegment.set_DataElementValue(4, 1, "BF");
                                                oSegment.set_DataElementValue(4, 2, oTransaction.Lines[0].Dx4Code.ToString().Replace(".", "").Trim());
                                                if (IsValidICD9(Convert.ToString(oTransaction.Lines[0].Dx4Code.Trim())) == false)
                                                {
                                                    return;
                                                }
                                            }
                                            if (oTransaction.Lines[0].Dx5Code.ToString().Trim() != "")
                                            {
                                                oSegment.set_DataElementValue(5, 1, "BF");
                                                oSegment.set_DataElementValue(5, 2, oTransaction.Lines[0].Dx5Code.ToString().Replace(".", "").Trim());
                                            }
                                            if (oTransaction.Lines[0].Dx6Code.ToString().Trim() != "")
                                            {
                                                oSegment.set_DataElementValue(6, 1, "BF");
                                                oSegment.set_DataElementValue(6, 2, oTransaction.Lines[0].Dx6Code.ToString().Replace(".", "").Trim());
                                            }
                                            if (oTransaction.Lines[0].Dx7Code.ToString().Trim() != "")
                                            {
                                                oSegment.set_DataElementValue(7, 1, "BF");
                                                oSegment.set_DataElementValue(7, 2, oTransaction.Lines[0].Dx7Code.ToString().Replace(".", "").Trim());
                                            }
                                            if (oTransaction.Lines[0].Dx8Code.ToString().Trim() != "")
                                            {
                                                oSegment.set_DataElementValue(8, 1, "BF");
                                                oSegment.set_DataElementValue(8, 2, oTransaction.Lines[0].Dx8Code.ToString().Replace(".", "").Trim());
                                            }
                                            //} 
                                            #endregion

                                            #region Referring Provider - 2310A


                                            if (_ReferralLName != "" && _ReferralNPI != "")
                                            {

                                                //2310B Referring PROVIDER
                                                //NM1 Referring PROVIDER NAME
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\NM1"));
                                                oSegment.set_DataElementValue(1, 0, "DN");
                                                oSegment.set_DataElementValue(2, 0, "1");
                                                oSegment.set_DataElementValue(3, 0, _ReferralLName.Trim()); //"ReferringLastname"
                                                oSegment.set_DataElementValue(4, 0, _ReferralFName.Trim());//"ReferringFirstname"
                                                oSegment.set_DataElementValue(5, 0, _ReferralMName.Trim());
                                                oSegment.set_DataElementValue(8, 0, "XX");
                                                oSegment.set_DataElementValue(9, 0, _ReferralNPI.Trim());//"NPI"

                                                //PRV REFERRING PROVIDER INFORMATION
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\PRV"));
                                                oSegment.set_DataElementValue(1, 0, "RF");
                                                oSegment.set_DataElementValue(2, 0, "ZZ");//Mutually Defined
                                                oSegment.set_DataElementValue(3, 0, _ReferralTaxonomy.Trim());//Reference Identification

                                                //REF
                                                if (_ReferralEmployerID.Trim() != "")
                                                {
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\REF"));
                                                    oSegment.set_DataElementValue(1, 0, "EI");//// Employer Identification Number
                                                    oSegment.set_DataElementValue(2, 0, _ReferralEmployerID.Trim());//"1039255");// 
                                                }
                                                else if (_ReferralSSN.Trim() != "")
                                                {
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\REF"));
                                                    oSegment.set_DataElementValue(1, 0, "SY");//// Social Security Number
                                                    oSegment.set_DataElementValue(2, 0, _ReferralSSN.Trim());//"1039255");// 
                                                }


                                            }
                                            #endregion Referring Provider

                                            #region Rendering Provider - 2310B

                                            //2310B RENDERING PROVIDER
                                            //NM1 RENDERING PROVIDER NAME
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\NM1"));
                                            oSegment.set_DataElementValue(1, 0, "82");
                                            oSegment.set_DataElementValue(2, 0, "1");
                                            FillProviderDetails(oTransaction.Lines[0].RefferingProviderId, ProviderType.RenderingProvider);
                                            oSegment.set_DataElementValue(3, 0, _RenderingLName);//Billing provider name
                                            oSegment.set_DataElementValue(4, 0, _RenderingFName);
                                            oSegment.set_DataElementValue(5, 0, _RenderingMName);
                                            oSegment.set_DataElementValue(8, 0, "XX");
                                            oSegment.set_DataElementValue(9, 0, _RenderingNPI.Trim());//oProviderDetails.NPI);//Billing provider ID/NPI


                                            //PRV RENDERING PROVIDER INFORMATION
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\PRV"));
                                            oSegment.set_DataElementValue(1, 0, "PE");
                                            oSegment.set_DataElementValue(2, 0, "ZZ");//Mutually Defined
                                            oSegment.set_DataElementValue(3, 0, _RenderingTaxonomy.Trim());//Reference Identification


                                            #endregion

                                            #region Facility - 2310D

                                            //2310D SERVICE LOCATION
                                            //NM1 SERVICE FACILITY LOCATION

                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1(2)\\NM1"));
                                            oSegment.set_DataElementValue(1, 0, "77");
                                            oSegment.set_DataElementValue(2, 0, "2");
                                            oSegment.set_DataElementValue(3, 0, _FacilityName);//"FacilityName"
                                            oSegment.set_DataElementValue(8, 0, "XX");//NPI code
                                            oSegment.set_DataElementValue(9, 0, _FacilityNPI.Trim());//NPI

                                            //N3 SERVICE FACILITY ADDRESS
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1(2)\\N3"));
                                            oSegment.set_DataElementValue(1, 0, _FacilityAddress);//"FacilityAddr"

                                            //N4 SERVICE FACILITY CITY/STATE/ZIP
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1(2)\\N4"));
                                            oSegment.set_DataElementValue(1, 0, _FacilityCity);//"FacilityCity"
                                            oSegment.set_DataElementValue(2, 0, _FacilityState);//"FacilityState"
                                            oSegment.set_DataElementValue(3, 0, _FacilityZip);//"FacilityZip"


                                            #endregion

                                            #region Subscriber Secondary Insurance - Loop 2320

                                            //LOOP - 2320
                                            if (bSecondaryInsurance)
                                            {

                                                #region SBR - SUBSCRIBER INFORMATION for Secondary Information

                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR"));
                                                //1.Payer Resposibilty Sequence No.
                                                oSegment.set_DataElementValue(1, 0, _OtherInsurancePST.Trim()); //P - Primary

                                                //2.Individual Relationship code
                                                oSegment.set_DataElementValue(2, 0, _OtherInsuranceRelationshipCode.Trim());//"18"); // Hard coded(Individual Relationship code) 18 - Self

                                                //3.Refrence identification
                                                oSegment.set_DataElementValue(3, 0, _OtherInsuranceGroupID);//"22145");///Policy no

                                                //5.Insurance Type Code
                                                oSegment.set_DataElementValue(5, 0, "C1"); // C1 - Commercial (Insurance Type Code)


                                                //oSegment.set_DataElementValue(6, 0, "6"); // 6 - No Co-ordination of Benefit

                                                ////8.Employment Status Code(Not Used)
                                                //oSegment.set_DataElementValue(8, 0, "AC"); // Employment status (AC - Active)

                                                //9.Claim Filing Indicator
                                                if (_OtherInsuranceType.Trim() == "")
                                                {
                                                    //MessageBox.Show("Insurance Type Code is not there for the Insurance name: " + _OtherInsuranceName + "", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    //return;
                                                }
                                                oSegment.set_DataElementValue(9, 0, _OtherInsuranceType.Trim()); //Commercial Insurance company

                                                #endregion SBR - SUBSCRIBER INFORMATION for Secondary Information

                                                #region CAS - Claim Adjustment

                                                //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\CAS"));
                                                //oSegment.set_DataElementValue(1, 0, "PI");//PR - Patient Responsibility
                                                //oSegment.set_DataElementValue(2, 0, "96");
                                                //oSegment.set_DataElementValue(3, 0, "300");

                                                #endregion CAS - Claim Adjustment

                                                #region AMT - Amount

                                                //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\AMT"));
                                                //oSegment.set_DataElementValue(1, 0, "D");
                                                //oSegment.set_DataElementValue(2, 0, "0");

                                                //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\AMT"));
                                                //oSegment.set_DataElementValue(1, 0, "F2");
                                                //oSegment.set_DataElementValue(2, 0, "100");

                                                #endregion AMT - Amount

                                                #region MOA - Medicare Outpatient Adjudication

                                                //ediDataSegment.Set(ref oSegment,(ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\MOA"));
                                                //oSegment.set_DataElementValue(1,0,"20");
                                                //oSegment.set_DataElementValue(2,0,"300");
                                                //oSegment.set_DataElementValue(3,0,"125");

                                                #endregion

                                                #region DMG  - Demographic

                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\DMG"));
                                                oSegment.set_DataElementValue(1, 0, "D8");
                                                oSegment.set_DataElementValue(2, 0, gloDateMaster.gloDate.DateAsNumber(_OtherInsuranceSubscriberDOB).ToString());//"SubscriberDOB"
                                                oSegment.set_DataElementValue(3, 0, _OtherInsuranceSubscriberGender);//"SubscriberGender"

                                                #endregion DMG  - Demographic

                                                #region OI - Other Insurance

                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\OI"));
                                                oSegment.set_DataElementValue(3, 0, "Y");
                                                oSegment.set_DataElementValue(4, 0, "C");
                                                oSegment.set_DataElementValue(6, 0, "Y");

                                                #endregion OI - Other Insurance

                                                //2330A SUBSCRIBER
                                                #region NM1 SUBSCRIBER NAME - 2330A

                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\NM1\\NM1"));
                                                oSegment.set_DataElementValue(1, 0, "IL");
                                                oSegment.set_DataElementValue(2, 0, "1");
                                                oSegment.set_DataElementValue(3, 0, _OtherInsuranceSubscriberLName);//"SubscriberLastOrgName"
                                                oSegment.set_DataElementValue(4, 0, _OtherInsuranceSubscriberFName);//"SubscriberFirstname"
                                                oSegment.set_DataElementValue(8, 0, "MI");
                                                oSegment.set_DataElementValue(9, 0, _OtherInsuranceID);//"SubscriberMemberID"

                                                //N3 SUBSCRIBER ADDRESS
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\NM1\\N3"));
                                                oSegment.set_DataElementValue(1, 0, _OtherInsuranceAddress);//"SubscriberAddress"

                                                //N4 SUBSCRIBER CITY
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\NM1\\N4"));
                                                oSegment.set_DataElementValue(1, 0, _OtherInsuranceCity);//"SubscriberCity"
                                                oSegment.set_DataElementValue(2, 0, _OtherInsuranceState);//"SubscrberState"
                                                oSegment.set_DataElementValue(3, 0, _OtherInsuranceZIP);//"SubscriberZip"

                                                #endregion NM1 SUBSCRIBER NAME

                                                #region Payer Information - 2330B

                                                //2330B SUBSCRIBER/PAYER
                                                //NM1 PAYER NAME
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\NM1\\NM1"));
                                                oSegment.set_DataElementValue(1, 0, "PR");
                                                oSegment.set_DataElementValue(2, 0, "2");

                                                oSegment.set_DataElementValue(3, 0, _OtherInsuranceName);//dtInsurance.Rows[0]["sSubscriberName"].ToString().Trim());//"PayerLastOrgName"

                                                oSegment.set_DataElementValue(8, 0, "PI");//This is the payer ID of Medicare for Michigan
                                                oSegment.set_DataElementValue(9, 0, _OtherInsurancePayerID.Trim());//dtInsurance.Rows[0]["sPayerID"].ToString());//PayerID
                                                //}

                                                #endregion Payer Information

                                            }

                                            #endregion Subscriber Secondary Insurance
                                            //}//end of IF loop for POS
                                            for (int nLine = 0; nLine <= oTransaction.Lines.Count - 1; nLine++)
                                            {
                                                iItemCount = 1;
                                                iItemCount = iItemCount + nLine;
                                              //  oTransLine = new TransactionLine();
                                                oTransLine = oTransaction.Lines[nLine];
                                                #region Service Line
                                                //******* SUBSCRIBER SERVICE LINE *************************************************************
                                                //TODO: Get the datatable for service info to add fields of service in EDI file.
                                                //2400 SERVICE LINE
                                                sInstance = iItemCount.ToString().Trim();
                                                //LX SERVICE LINE COUNTER
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\LX"));
                                                oSegment.set_DataElementValue(1, 0, iItemCount.ToString());

                                                //SV1 PROFESSIONAL SERVICE
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\SV1"));
                                                oSegment.set_DataElementValue(1, 1, "HC");
                                                oSegment.set_DataElementValue(1, 2, oTransLine.CPTCode.ToString().Replace(".", ""));//"ServiceID"
                                                if (oTransLine.Mod1Code.ToString().Trim() != "")
                                                {
                                                    oSegment.set_DataElementValue(1, 3, oTransLine.Mod1Code.ToString());//Modifier 1
                                                }
                                                if (oTransLine.Mod2Code.ToString().Trim() != "")
                                                {
                                                    if (oTransLine.Mod1Code.ToString().Trim() == "")
                                                    {
                                                        oSegment.set_DataElementValue(1, 3, oTransLine.Mod2Code.ToString());
                                                    }
                                                    else
                                                    {
                                                        oSegment.set_DataElementValue(1, 4, oTransLine.Mod2Code.ToString());//Modifier 2
                                                    }
                                                }
                                                string _ClaimLineCharges = Convert.ToString(oTransLine.Total);

                                                if (_ClaimLineCharges.Substring(_ClaimLineCharges.Length - 2, 2) == "00")
                                                {
                                                    _ClaimLineCharges = _ClaimLineCharges.Substring(0, _ClaimLineCharges.Length - 3);
                                                }
                                                oSegment.set_DataElementValue(2, 0, _ClaimLineCharges);//"ServiceAmount"
                                                oSegment.set_DataElementValue(3, 0, "UN");//UN stands for UNITS
                                                oSegment.set_DataElementValue(4, 0, oTransLine.Unit.ToString());//Unit/Quantity
                                                if (oTransLine.Dx1Ptr.ToString() == "True")
                                                {
                                                    oSegment.set_DataElementValue(7, 1, "1");//"Diagnosis Pointer1" (Must Use)
                                                    if (oTransLine.Dx2Ptr.ToString() == "True")
                                                    {
                                                        oSegment.set_DataElementValue(7, 2, "2");//"Diagnosis Pointer2"
                                                    }
                                                    if (oTransLine.Dx3Ptr.ToString() == "True")
                                                    {
                                                        oSegment.set_DataElementValue(7, 3, "3");//"Diagnosis Pointer3"
                                                    }
                                                    if (oTransLine.Dx4Ptr.ToString() == "True")
                                                    {
                                                        oSegment.set_DataElementValue(7, 4, "4");//"Diagnosis Pointer4"
                                                    }
                                                }
                                                else if (oTransLine.Dx2Ptr.ToString() == "True")
                                                {
                                                    oSegment.set_DataElementValue(7, 1, "2");//"Diagnosis Pointer1"
                                                }
                                                else if (oTransLine.Dx3Ptr.ToString() == "True")
                                                {
                                                    oSegment.set_DataElementValue(7, 1, "3");//"Diagnosis Pointer1"
                                                }
                                                else if (oTransLine.Dx4Ptr.ToString() == "True")
                                                {
                                                    oSegment.set_DataElementValue(7, 1, "4");//"Diagnosis Pointer1"
                                                }

                                                ////oSegment.set_DataElementValue(9, 0, "N");//Y=Yes,N=No
                                                //DTP DATE - SERVICE DATE(S)
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\DTP"));
                                                oSegment.set_DataElementValue(1, 0, "472");
                                                oSegment.set_DataElementValue(2, 0, "D8");
                                                oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oTransLine.DateServiceFrom.ToShortDateString())));//Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_DATEFROM)));//"ServiceDate"

                                                #endregion
                                            }
                                        #endregion " Subscriber "

                                        }//end of if loop for Subscriber as Patient
                                        else
                                        {
                                            #region "Dependent Loop"

                                            //////*****************************************************************************************************
                                            //////******* DEPENDENT HIERARCHICAL LEVEL ****************************************************************
                                            //////*****************************************************************************************************
                                            ////TODO: Get the datatable for dependent info to add fields of service in EDI file.
                                            if (_PayerID == "")
                                            {
                                                //MessageBox.Show("Payer ID is not there for the Insurance", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                //return;
                                            }
                                            #region Payer Information Loop 2010BB
                                            //2010BB SUBSCRIBER/PAYER
                                            //NM1 PAYER NAME
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1(2)\\NM1"));
                                            oSegment.set_DataElementValue(1, 0, "PR");
                                            oSegment.set_DataElementValue(2, 0, "2");
                                            oSegment.set_DataElementValue(3, 0, _PayerName);//"PayerLastOrgName"
                                            oSegment.set_DataElementValue(8, 0, "PI");//This is the payer ID of Medicare for Michigan
                                            oSegment.set_DataElementValue(9, 0, _PayerID);//dtInsurance.Rows[0]["sPayerID"].ToString());//PayerID

                                            #endregion

                                            nHlCount = nHlCount + 1;

                                            //2000B DEPENDENT HL LOOP
                                            //HL-DEPENDENT
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
                                            oSegment.set_DataElementValue(1, 0, nHlCount.ToString());
                                            oSegment.set_DataElementValue(2, 0, nHlSubscriberParent.ToString());
                                            oSegment.set_DataElementValue(3, 0, "23");
                                            oSegment.set_DataElementValue(4, 0, "0");


                                            //PAT - PATIENT/DEPENDENT INFORMATION
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\PAT"));
                                            oSegment.set_DataElementValue(1, 0, _SubscriberRelationshipCode); //01 - Spouse 19 - Child

                                            #region " Patient Info"

                                            //NM1 - INDIVIDUAL OR ORGANIZATIONAL NAME
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1"));
                                            oSegment.set_DataElementValue(1, 0, "QC");
                                            oSegment.set_DataElementValue(2, 0, "1");
                                            oSegment.set_DataElementValue(3, 0, _PatientLastName.Trim());//Patient Last Name
                                            oSegment.set_DataElementValue(4, 0, _PatientFirstName.Trim());//Patient First Name

                                            //N3 - ADDRESS INFORMATION
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N3"));
                                            oSegment.set_DataElementValue(1, 0, _PatientAddress.Trim());//"Address"

                                            //N4 - GEOGRAPHIC LOCATION
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N4"));
                                            oSegment.set_DataElementValue(1, 0, _PatientCity.Trim());//"City"
                                            oSegment.set_DataElementValue(2, 0, _PatientState.Trim());//"State"
                                            oSegment.set_DataElementValue(3, 0, _PatientZip.Trim());//"Zip"

                                            //DMG - DEMOGRAPHIC INFORMATION
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\DMG"));
                                            oSegment.set_DataElementValue(1, 0, "D8");
                                            oSegment.set_DataElementValue(2, 0, _PatientDOB);
                                            oSegment.set_DataElementValue(3, 0, _PatientGender.Trim());

                                            #endregion " Patient Info"

                                            //******* DEPENDENT CLAIM INFORMATION *************************************************************
                                            //TODO: Get the datatable for Claim info to add fields of service in EDI file
                                            string _FirstPOS = "";
                                            string _NewPOS = "";
                                            string _ClaimTotal = "";
                                            iItemCount = 0;
                                            iItemCount = 1;
                                            decimal _claimAmount = 0;
                                            for (int nLine = 0; nLine <= oTransaction.Lines.Count - 1; nLine++)
                                            {
                                             //   oTransLine = new TransactionLine();
                                                oTransLine = oTransaction.Lines[nLine];
                                                //Anil - 05 Nov 2008
                                                //_claimAmount = _claimAmount + oTransLine.Total;
                                                //_ClaimTotal = Convert.ToString(_claimAmount).Substring(0, Convert.ToInt32(Convert.ToString(oTransLine.Total).Length ));

                                                //Vinayak - 05 Nov 2008
                                                _claimAmount = _claimAmount + oTransLine.Total;

                                                _FirstPOS = oTransaction.Lines[0].POSCode;
                                                _NewPOS = oTransLine.POSCode;
                                            }

                                            _ClaimTotal = _claimAmount.ToString("#0.00");
                                            if (_ClaimTotal.Substring(_ClaimTotal.Length - 2, 2) == "00")
                                            {
                                                _ClaimTotal = _ClaimTotal.Substring(0, _ClaimTotal.Length - 3);
                                            }

                                            #region "Dependent Claim Level"
                                            //2300 CLAIM
                                            //CLM CLAIM LEVEL INFORMATION
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\CLM"));
                                            oSegment.set_DataElementValue(1, 0, _PatientAccountNo); //Patient Account no         
                                            oSegment.set_DataElementValue(2, 0, _ClaimTotal.Trim()); //Claim Amount
                                            oSegment.set_DataElementValue(5, 1, _FirstPOS.Trim()); //21 - Inpatient Hospital
                                            oSegment.set_DataElementValue(5, 3, "1");
                                            oSegment.set_DataElementValue(6, 0, "Y");
                                            oSegment.set_DataElementValue(7, 0, "A");
                                            oSegment.set_DataElementValue(8, 0, "Y");
                                            oSegment.set_DataElementValue(9, 0, "Y");
                                            oSegment.set_DataElementValue(10, 0, "C");

                                            string OnsetDate = "";
                                            if (oTransaction.InjuryDate.ToString() != "" || oTransaction.OnsiteDate.ToString() != "")
                                            {
                                                if (oTransaction.InjuryDate.ToString() != "" && oTransaction.InjuryDate > 0)
                                                {
                                                    OnsetDate = Convert.ToString(oTransaction.InjuryDate);
                                                    ////DTP DATE OF ONSET of current symptoms or illness
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                    oSegment.set_DataElementValue(1, 0, "431");
                                                    oSegment.set_DataElementValue(2, 0, "D8");
                                                    oSegment.set_DataElementValue(3, 0, OnsetDate);//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpOnsetofCurrentSymptomsorillness.Value.ToShortDateString())).Trim());     //Claim Date
                                                }
                                                else if (oTransaction.OnsiteDate.ToString() != "" && oTransaction.OnsiteDate > 0)
                                                {
                                                    OnsetDate = Convert.ToString(oTransaction.OnsiteDate);
                                                    ////DTP DATE OF ONSET of current symptoms or illness
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                    oSegment.set_DataElementValue(1, 0, "431");
                                                    oSegment.set_DataElementValue(2, 0, "D8");
                                                    oSegment.set_DataElementValue(3, 0, OnsetDate);//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpOnsetofCurrentSymptomsorillness.Value.ToShortDateString())).Trim());     //Claim Date
                                                }

                                                if (oTransaction.AccidentDate.ToString() != "" && oTransaction.AccidentDate > 0)
                                                {
                                                    OnsetDate = Convert.ToString(oTransaction.AccidentDate);
                                                    ////DTP DATE OF ACCIDENT 
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                    oSegment.set_DataElementValue(1, 0, "439");
                                                    oSegment.set_DataElementValue(2, 0, "D8");
                                                    oSegment.set_DataElementValue(3, 0, OnsetDate);//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpOnsetofCurrentSymptomsorillness.Value.ToShortDateString())).Trim());     //Claim Date
                                                }
                                            }

                                            //DTP DATE OF ONSET of similar symptoms or illness
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                            oSegment.set_DataElementValue(1, 0, "438");
                                            oSegment.set_DataElementValue(2, 0, "D8");
                                            oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpOnsetoSimilarSyptomsorillness.Value.ToShortDateString())).Trim());
                                            //
                                            if (_FirstPOS.Trim() != "11")
                                            {
                                                if (oTransaction.HospitalizationDateFrom > 0 && oTransaction.HospitalizationDateFrom.ToString() != "")
                                                {
                                                    //DTP DATE OF Hospitalization (Admission) 
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                    oSegment.set_DataElementValue(1, 0, "435");
                                                    oSegment.set_DataElementValue(2, 0, "D8");
                                                    oSegment.set_DataElementValue(3, 0, oTransaction.HospitalizationDateFrom.ToString());//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpHospitalizationDate.Value.ToShortDateString())).Trim());     //Claim Date
                                                }

                                                if (oTransaction.HospitalizationDateTo > 0 && oTransaction.HospitalizationDateTo.ToString() != "")
                                                {
                                                    ////DTP DATE OF Discharge 
                                                    //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                    //oSegment.set_DataElementValue(1, 0, "096");
                                                    //oSegment.set_DataElementValue(2, 0, "D8");
                                                    //oSegment.set_DataElementValue(3, 0, oTransaction.HospitalizationDateTo.ToString());//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpHospitalDischargeDate.Value.ToShortDateString())).Trim());     //Claim Date
                                                    ////
                                                }
                                            }
                                            if (oTransaction.WorkersComp == true)
                                            {
                                                if (oTransaction.UnableToWorkFromDate > 0 && oTransaction.UnableToWorkFromDate.ToString() != "")
                                                {
                                                    //DTP DATE OF (Intial Disability period last day worked)
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                    oSegment.set_DataElementValue(1, 0, "297");
                                                    oSegment.set_DataElementValue(2, 0, "D8");
                                                    oSegment.set_DataElementValue(3, 0, oTransaction.UnableToWorkFromDate.ToString());//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpLastDayWorked.Value.ToShortDateString())).Trim());     //Claim Date
                                                    //
                                                }

                                                if (oTransaction.UnableToWorkTillDate > 0 && oTransaction.UnableToWorkTillDate.ToString() != "")
                                                {
                                                    //DTP DATE OF (Intial Disability period return to work)
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                    oSegment.set_DataElementValue(1, 0, "296");
                                                    oSegment.set_DataElementValue(2, 0, "D8");
                                                    oSegment.set_DataElementValue(3, 0, oTransaction.UnableToWorkTillDate.ToString());//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpReturnToWork.Value.ToShortDateString())).Trim());     //Claim Date
                                                    //
                                                }
                                            }

                                            //REF CLEARING HOUSE CLAIM NUMBER
                                            if (_PriorAuthorizationNo.Trim() != "")
                                            {
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\REF"));
                                                oSegment.set_DataElementValue(1, 0, "G1");
                                                oSegment.set_DataElementValue(2, 0, _PriorAuthorizationNo.Trim()); //Claim No
                                            }
                                            #endregion "Dependent Claim Level"

                                            #region HI - Diagnosis for Dependent
                                            //HI HEALTH CARE DIAGNOSIS CODES

                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\HI"));
                                            oSegment.set_DataElementValue(1, 1, "BK");
                                            if (oTransaction.Lines[0].Dx1Code.ToString() != "")
                                            {
                                                oSegment.set_DataElementValue(1, 2, oTransaction.Lines[0].Dx1Code.ToString().Replace(".", ""));// Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_DX1_CODE)));  // "0340");
                                            }
                                            else
                                            {
                                                //MessageBox.Show("Principle Diagnosis is not given.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                return;
                                            }
                                            if (oTransaction.Lines[0].Dx2Code.ToString() != "")
                                            {
                                                oSegment.set_DataElementValue(2, 1, "BF");
                                                oSegment.set_DataElementValue(2, 2, oTransaction.Lines[0].Dx2Code.ToString().Replace(".", ""));//Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_DX1_CODE))); // oCase.CaseDiagnosis[0].DiagnosisCode.ToString().Replace(".", "").Trim());
                                            }
                                            if (oTransaction.Lines[0].Dx3Code.ToString() != "")
                                            {
                                                oSegment.set_DataElementValue(3, 1, "BF");
                                                oSegment.set_DataElementValue(3, 2, oTransaction.Lines[0].Dx3Code.ToString().Replace(".", ""));
                                            }
                                            if (oTransaction.Lines[0].Dx4Code.ToString() != "")
                                            {
                                                oSegment.set_DataElementValue(4, 1, "BF");
                                                oSegment.set_DataElementValue(4, 2, oTransaction.Lines[0].Dx4Code.ToString().Replace(".", ""));
                                            }
                                            if (oTransaction.Lines[0].Dx5Code.ToString() != "")
                                            {
                                                oSegment.set_DataElementValue(5, 1, "BF");
                                                oSegment.set_DataElementValue(5, 2, oTransaction.Lines[0].Dx5Code.ToString().Replace(".", ""));
                                            }
                                            if (oTransaction.Lines[0].Dx6Code.ToString() != "")
                                            {
                                                oSegment.set_DataElementValue(6, 1, "BF");
                                                oSegment.set_DataElementValue(6, 2, oTransaction.Lines[0].Dx6Code.ToString().Replace(".", ""));
                                            }
                                            if (oTransaction.Lines[0].Dx7Code.ToString() != "")
                                            {
                                                oSegment.set_DataElementValue(7, 1, "BF");
                                                oSegment.set_DataElementValue(7, 2, oTransaction.Lines[0].Dx7Code.ToString().Replace(".", ""));
                                            }
                                            if (oTransaction.Lines[0].Dx8Code.ToString() != "")
                                            {
                                                oSegment.set_DataElementValue(8, 1, "BF");
                                                oSegment.set_DataElementValue(8, 2, oTransaction.Lines[0].Dx8Code.ToString().Replace(".", ""));
                                            }
                                            //} 
                                            #endregion

                                            #region Referring Provider -  for Dependent

                                            //2310B RENDERING PROVIDER
                                            //NM1 RENDERING PROVIDER NAME

                                            if (_ReferralLName != "" && _ReferralNPI != "")
                                            {

                                                //2310B Referring PROVIDER
                                                //NM1 Referring PROVIDER NAME
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\NM1"));
                                                oSegment.set_DataElementValue(1, 0, "DN");
                                                oSegment.set_DataElementValue(2, 0, "1");
                                                oSegment.set_DataElementValue(3, 0, _ReferralLName.Trim()); //"ReferringLastname"
                                                oSegment.set_DataElementValue(4, 0, _ReferralFName.Trim());//"ReferringFirstname"
                                                oSegment.set_DataElementValue(5, 0, _ReferralMName.Trim());
                                                oSegment.set_DataElementValue(8, 0, "XX");
                                                oSegment.set_DataElementValue(9, 0, _ReferralNPI.Trim());//"NPI"

                                                //PRV REFERRING PROVIDER INFORMATION
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\PRV"));
                                                oSegment.set_DataElementValue(1, 0, "RF");
                                                oSegment.set_DataElementValue(2, 0, "ZZ");//Mutually Defined
                                                oSegment.set_DataElementValue(3, 0, _ReferralTaxonomy.Trim());//Reference Identification

                                                //REF
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\REF"));
                                                oSegment.set_DataElementValue(1, 0, "EI");//// Employer Identification Number
                                                oSegment.set_DataElementValue(2, 0, _ReferralEmployerID.Trim());//"1039255");// 


                                            }
                                            #endregion Referring Provider

                                            #region Rendering Provider -  for Dependent

                                            //2310B RENDERING PROVIDER
                                            //NM1 RENDERING PROVIDER NAME
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\NM1"));
                                            oSegment.set_DataElementValue(1, 0, "82");
                                            oSegment.set_DataElementValue(2, 0, "1");
                                            FillProviderDetails(oTransaction.ProviderID, ProviderType.RenderingProvider);
                                            oSegment.set_DataElementValue(3, 0, _RenderingLName);//Billing provider name
                                            oSegment.set_DataElementValue(4, 0, _RenderingFName);
                                            oSegment.set_DataElementValue(5, 0, _RenderingMName);
                                            oSegment.set_DataElementValue(8, 0, "XX");
                                            oSegment.set_DataElementValue(9, 0, _RenderingNPI.Trim());//oProviderDetails.NPI);//Billing provider ID/NPI


                                            //PRV RENDERING PROVIDER INFORMATION
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\PRV"));
                                            oSegment.set_DataElementValue(1, 0, "PE");
                                            oSegment.set_DataElementValue(2, 0, "ZZ");//Mutually Defined
                                            oSegment.set_DataElementValue(3, 0, _RenderingTaxonomy.Trim());//Reference Identification


                                            #endregion

                                            #region Facility -  for Dependent

                                            //2310D SERVICE LOCATION
                                            //NM1 SERVICE FACILITY LOCATION

                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1(2)\\NM1"));
                                            oSegment.set_DataElementValue(1, 0, "77");
                                            oSegment.set_DataElementValue(2, 0, "2");
                                            oSegment.set_DataElementValue(3, 0, _FacilityName);//"FacilityName"
                                            //oSegment.set_DataElementValue(8, 0, "XX");//NPI code
                                            //oSegment.set_DataElementValue(9, 0, txtFacilityCode.Text.Trim());//NPI

                                            //N3 SERVICE FACILITY ADDRESS
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1(2)\\N3"));
                                            oSegment.set_DataElementValue(1, 0, _FacilityAddress);//"FacilityAddr"

                                            //N4 SERVICE FACILITY CITY/STATE/ZIP
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1(2)\\N4"));
                                            oSegment.set_DataElementValue(1, 0, _FacilityCity);//"FacilityCity"
                                            oSegment.set_DataElementValue(2, 0, _FacilityState);//"FacilityState"
                                            oSegment.set_DataElementValue(3, 0, _FacilityZip);//"FacilityZip"

                                            #endregion

                                            for (int nLine = 0; nLine <= oTransaction.Lines.Count - 1; nLine++)
                                            {
                                                iItemCount = 1;
                                                iItemCount = iItemCount + nLine;
                                             //   oTransLine = new TransactionLine();
                                                oTransLine = oTransaction.Lines[nLine];

                                                #region Service Line
                                                //******* SUBSCRIBER SERVICE LINE *************************************************************
                                                //TODO: Get the datatable for service info to add fields of service in EDI file.
                                                //2400 SERVICE LINE
                                                sInstance = iItemCount.ToString().Trim();
                                                //LX SERVICE LINE COUNTER
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\LX"));
                                                oSegment.set_DataElementValue(1, 0, iItemCount.ToString());

                                                //SV1 PROFESSIONAL SERVICE
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\SV1"));
                                                oSegment.set_DataElementValue(1, 1, "HC");
                                                oSegment.set_DataElementValue(1, 2, oTransLine.CPTCode.ToString().Replace(".", ""));//"ServiceID"
                                                if (oTransLine.Mod1Code.ToString() != "")
                                                {
                                                    oSegment.set_DataElementValue(1, 3, oTransLine.Mod1Code.ToString());//Modifier 1
                                                }
                                                if (oTransLine.Mod2Code.ToString() != "")
                                                {
                                                    oSegment.set_DataElementValue(1, 4, oTransLine.Mod2Code.ToString());//Modifier 2
                                                }
                                                string _ClaimLineCharges = Convert.ToString(oTransLine.Total);

                                                if (_ClaimLineCharges.Substring(_ClaimLineCharges.Length - 2, 2) == "00")
                                                {
                                                    _ClaimLineCharges = _ClaimLineCharges.Substring(0, _ClaimLineCharges.Length - 3);
                                                }
                                                oSegment.set_DataElementValue(2, 0, _ClaimLineCharges);//"ServiceAmount"
                                                oSegment.set_DataElementValue(3, 0, "UN");//UN stands for UNITS
                                                oSegment.set_DataElementValue(4, 0, oTransLine.Unit.ToString());//Unit/Quantity
                                                if (oTransLine.Dx1Ptr.ToString() == "True")
                                                {
                                                    oSegment.set_DataElementValue(7, 1, "1");//"Diagnosis Pointer1" (Must Use)
                                                    if (oTransLine.Dx2Ptr.ToString() == "True")
                                                    {
                                                        oSegment.set_DataElementValue(7, 2, "2");//"Diagnosis Pointer2"
                                                    }
                                                    if (oTransLine.Dx3Ptr.ToString() == "True")
                                                    {
                                                        oSegment.set_DataElementValue(7, 3, "3");//"Diagnosis Pointer3"
                                                    }
                                                    if (oTransLine.Dx4Ptr.ToString() == "True")
                                                    {
                                                        oSegment.set_DataElementValue(7, 4, "4");//"Diagnosis Pointer4"
                                                    }
                                                }
                                                else if (oTransLine.Dx2Ptr.ToString() == "True")
                                                {
                                                    oSegment.set_DataElementValue(7, 1, "2");//"Diagnosis Pointer1"
                                                }
                                                else if (oTransLine.Dx3Ptr.ToString() == "True")
                                                {
                                                    oSegment.set_DataElementValue(7, 1, "3");//"Diagnosis Pointer1"
                                                }
                                                else if (oTransLine.Dx4Ptr.ToString() == "True")
                                                {
                                                    oSegment.set_DataElementValue(7, 1, "4");//"Diagnosis Pointer1"
                                                }

                                                //oSegment.set_DataElementValue(9, 0, "N");////Y=Yes, N=No

                                                //DTP DATE - SERVICE DATE(S)
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\DTP"));
                                                oSegment.set_DataElementValue(1, 0, "472");
                                                oSegment.set_DataElementValue(2, 0, "D8");
                                                oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oTransLine.DateServiceFrom.ToShortDateString())));//Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_DATEFROM)));//"ServiceDate"



                                                #endregion
                                            }

                                            #endregion " Dependent "
                                        }//end of else loop for dependent

                                        //Transaction Line Loop
                                    }//Transaction SETS Loop
                                }
                                if (oTransaction != null)
                                {
                                    oTransaction.Dispose();
                                    oTransaction = null;
                                }
                            }

                        }


                        //Save to a file
                        SaveFileDialog oSave = new SaveFileDialog();
                        oSave.Filter = "TEXT Files (*.txt)|*.txt|EDI Files (*.edi)|*.edi|X12 Files (*.X12)|*.X12";
                        if (oSave.ShowDialog(this) == DialogResult.OK)
                        {
                            oEdiDoc.Save(sPath + sEdiFile);
                            System.IO.StreamReader oReader = new System.IO.StreamReader(sPath + sEdiFile);
                            string strData;
                            strData = oReader.ReadToEnd();
                            oReader.Close();

                            System.IO.StreamWriter oWriter = new System.IO.StreamWriter(oSave.FileName);
                            oWriter.Write(strData);
                            oWriter.Close();
                            //MessageBox.Show("File Created Successfully", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        oSave.Dispose();
                        oSave = null;
                        //DESTROYS OBJECTS
                        oSegment.Dispose();
                        oTransactionset.Dispose();
                        oGroup.Dispose();
                        oInterchange.Dispose();

                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ogloBilling != null)
                {
                    ogloBilling.Dispose();
                    ogloBilling = null;
                }
            }
            #endregion " Generate EDI "
        }

        private void GenerateEDIFileForBCBSM()
        {
            #region " Generate EDI "

           // string sEntity = "";
            string sInstance = "";
         //   string _strSQL = "";
         //   DataTable dt;
          //  string _BillingProviderDetails = "";
         //   gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloBilling ogloBilling = new gloBilling(_databaseconnectionstring, "");
        //    bool IsSecondaryInsurance = false;
            Transaction oTransaction = null;

            try
            {
                FillClearingHouseInfo();
                if (SelectedTransactions != null)
                {
                    if (SelectedTransactions.Count > 0)
                    {
                        for (int i = 0; i < SelectedTransactions.Count; i++)
                        {
                         //   oTransaction = new Transaction();
                           // TransactionLine oTransLine = null;
                            oTransaction = ogloBilling.GetTransactionDetails(Convert.ToInt64(SelectedTransactions[i]), _ClinicID);
                            if (oTransaction != null)
                            {
                                if (oTransaction.Lines.Count > 0)
                                {
                                    FillSubmitterInfo(Convert.ToInt64(_ClinicID), oTransaction.ProviderID);
                                }
                                oTransaction.Dispose();
                                oTransaction = null;
                            }
                        }
                    }
                }


                #region " Interchange Segment "
                //Create the interchange segment
                ediInterchange.Set(ref oInterchange, (ediInterchange)oEdiDoc.CreateInterchange("X", "004010"));
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oInterchange.GetDataSegmentHeader());

                oSegment.set_DataElementValue(1, 0, "00");
                oSegment.set_DataElementValue(3, 0, "00");
                oSegment.set_DataElementValue(5, 0, "ZZ");
                oSegment.set_DataElementValue(6, 0, _SenderID.Trim());//"1234545");//
                oSegment.set_DataElementValue(7, 0, "ZZ");
                oSegment.set_DataElementValue(8, 0, _ReceiverID.Trim());//"V2EL");//
                string ISA_Date = Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString()));
                oSegment.set_DataElementValue(9, 0, ISA_Date.Substring(2));
                string ISA_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString()));

                oSegment.set_DataElementValue(10, 0, FormattedTime(ISA_Time).Trim());
                oSegment.set_DataElementValue(11, 0, "U");
                //oSegment.set_DataElementValue(12, 0, "00401");
                oSegment.set_DataElementValue(13, 0, "000000020");//
                oSegment.set_DataElementValue(14, 0, "0");
                oSegment.set_DataElementValue(15, 0, "T");
                oSegment.set_DataElementValue(16, 0, ":");

                #endregion " Interchange Segment "

                #region " Functional Group "

                //Create the functional group segment
                ediGroup.Set(ref oGroup, (ediGroup)oInterchange.CreateGroup("004010X098A1"));
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oGroup.GetDataSegmentHeader());
                oSegment.set_DataElementValue(1, 0, "HC");
                oSegment.set_DataElementValue(2, 0, _SenderName);
                oSegment.set_DataElementValue(3, 0, _ReceiverCode.Trim());//"ClarEDI");
                oSegment.set_DataElementValue(4, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())));
                string GS_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString()));
                oSegment.set_DataElementValue(5, 0, FormattedTime(GS_Time).Trim());
                oSegment.set_DataElementValue(6, 0, "1");
                oSegment.set_DataElementValue(7, 0, "X");
                oSegment.set_DataElementValue(8, 0, "004010X098A1");

                #endregion " Functional Group "

                #region ST - TRANSACTION SET HEADER

                ediTransactionSet.Set(ref oTransactionset, (ediTransactionSet)oGroup.CreateTransactionSet("837"));
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.GetDataSegmentHeader());
                oSegment.set_DataElementValue(2, 0, "00021");//"ControlNo"

                #endregion ST - TRANSACTION SET HEADER

                #region BHT - BEGINNING OF HIERARCHICAL TRANSACTION

                //Begining Segment 
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("BHT"));
                oSegment.set_DataElementValue(1, 0, "0019"); //Herarchical Structure Code
                oSegment.set_DataElementValue(2, 0, "00"); //00-Original, 01-Re-issue
                oSegment.set_DataElementValue(3, 0, "1234"); //Reference identification
                oSegment.set_DataElementValue(4, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())).Trim());//Date of claim
                string BHT_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim();
                oSegment.set_DataElementValue(5, 0, FormattedTime(BHT_Time).Trim()); //"1230");
                oSegment.set_DataElementValue(6, 0, "CH"); //CH-Chargeable, RP-Reporting
                #endregion BHT - BEGINNING OF HIERARCHICAL TRANSACTION

                #region REF - TRANSMISSION TYPE IDENTIFICATION

                //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("REF"));
                //oSegment.set_DataElementValue(1, 0, "87");
                //oSegment.set_DataElementValue(2, 0, "004010X098A1");//"ReferenceID"

                #endregion REF - TRANSMISSION TYPE IDENTIFICATION

                #region NM1 - SUBMITTER


                //1000A SUBMITTER
                //NM1 SUBMITTER
                if (_SubmitterName.Trim() != "" && _SubmitterETIN.Trim() != "" && _SubmitterContactPersonName.Trim() != "" && _SubmitterContactPersonNo.Trim() != "")
                {
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("NM1\\NM1"));
                    oSegment.set_DataElementValue(1, 0, "41");
                    oSegment.set_DataElementValue(2, 0, "2");
                    oSegment.set_DataElementValue(3, 0, _SubmitterName);//cmbClinic.Text.Trim());// clinic name
                    oSegment.set_DataElementValue(8, 0, "46"); // Identification Code Qualifier 
                    oSegment.set_DataElementValue(9, 0, _SubmitterETIN);//txtSubIdentificationCode.Text.Trim());//clinic code or Electronic Transmitter Identification No.


                    //PER SUBMITTER EDI CONTACT INFORMATION

                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("NM1\\PER"));
                    oSegment.set_DataElementValue(1, 0, "IC");
                    if (_SubmitterName.Trim() != _SubmitterContactPersonName.Trim())
                    {
                        oSegment.set_DataElementValue(2, 0, _SubmitterContactPersonName);//txtSubmitterContactName.Text.Trim());//Contact person name of clinic
                    }
                    oSegment.set_DataElementValue(3, 0, "TE");
                    oSegment.set_DataElementValue(4, 0, _SubmitterContactPersonNo);//txtSubmitterPhone.Text.Trim().Replace("(", "").Replace(")", "").Replace("-", ""));//clinic phone
                }
                else
                {
                    MessageBox.Show("Submitter/Clinic information is not complete.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                #endregion NM1 - SUBMITTER

                #region NM1 - RECEIVER NAME

                //1000B RECEIVER
                //NM1 RECEIVER NAME
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("NM1(2)\\NM1"));
                oSegment.set_DataElementValue(1, 0, "40");
                oSegment.set_DataElementValue(2, 0, "2");
                oSegment.set_DataElementValue(3, 0, "GatewayEDI");//clearing house or contractor or carrier or FI name
                oSegment.set_DataElementValue(8, 0, "46");// Identification Code Qualifier
                oSegment.set_DataElementValue(9, 0, "V2093");//code of carrier/contractor/FI or Electronic Transmitter Identification No.


                #endregion NM1 - RECEIVER NAME

                nHlCount = 0;

                if (SelectedTransactions != null)
                {
                    if (SelectedTransactions.Count > 0)
                    {
                        for (int i = 0; i < SelectedTransactions.Count; i++)
                        {
                          //  oTransaction = new Transaction();
                            TransactionLine oTransLine = null;
                            oTransaction = ogloBilling.GetTransactionDetails(Convert.ToInt64(SelectedTransactions[i]), _ClinicID);
                            if (oTransaction != null)
                            {
                                if (oTransaction.Lines.Count > 0)
                                {
                                    FillAllDetails(oTransaction);

                                    for (int nTransactionSet = 1; nTransactionSet <= 1; nTransactionSet++)
                                    {
                                        //**** BILLING/PAY-TO PROVIDER HIERARCHICAL LEVEL *******************************************

                                        nHlCount = nHlCount + 1;
                                        nHlProvParent = nHlCount;
                                        //2000A BILLING/PAY-TO PROVIDER HL LOOP
                                        //HL-BILLING PROVIDER

                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
                                        oSegment.set_DataElementValue(1, 0, nHlCount.ToString().Trim());
                                        oSegment.set_DataElementValue(3, 0, "20");
                                        oSegment.set_DataElementValue(4, 0, "1");

                                        #region Billing Provider

                                        //2010AA BILLING PROVIDER
                                        //NM1 BILLING PROVIDER NAME
                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
                                        oSegment.set_DataElementValue(1, 0, "85");
                                        oSegment.set_DataElementValue(2, 0, "1");
                                        oSegment.set_DataElementValue(3, 0, _BillingLName);//Billing provider name
                                        oSegment.set_DataElementValue(4, 0, _BillingFName);
                                        oSegment.set_DataElementValue(5, 0, _BillingMName);

                                        oSegment.set_DataElementValue(8, 0, "XX");
                                        if (_BillingNPI.Trim() != "")
                                        {
                                            oSegment.set_DataElementValue(9, 0, _BillingNPI);//Billing provider ID/NPI
                                        }
                                        else
                                        {
                                            MessageBox.Show("Billing Provider NPI is not there.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            return;
                                        }


                                        //N3 BILLING PROVIDER ADDRESS
                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N3"));
                                        oSegment.set_DataElementValue(1, 0, _BillingAddress);//Provider Address

                                        //N4 BILLING PROVIDER LOCATION
                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N4"));
                                        oSegment.set_DataElementValue(1, 0, _BillingCity);////Provider City
                                        oSegment.set_DataElementValue(2, 0, _BillingState);//Provider state
                                        oSegment.set_DataElementValue(3, 0, _BillingZIP);//Provider ZIP

                                        //REF 
                                        if (_BillingEmployerID.Trim() != "")
                                        {
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\REF"));
                                            oSegment.set_DataElementValue(1, 0, "EI");//Reference Identification Qualifier("EI" stands for-> Employer ID)
                                            if (_BillingEmployerID.Length > 9)
                                            {
                                                _BillingEmployerID = _BillingEmployerID.Substring(0, 9);
                                            }
                                            oSegment.set_DataElementValue(2, 0, _BillingEmployerID);
                                        }
                                        //REF 
                                        else if (_BillingSSN.Trim() != "")
                                        {
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\REF"));
                                            oSegment.set_DataElementValue(1, 0, "SY");//Reference Identification Qualifier("SY" stands for-> Social Security Number)
                                            if (_BillingSSN.Length > 9)
                                            {
                                                _BillingSSN = _BillingSSN.Substring(0, 9);
                                            }
                                            oSegment.set_DataElementValue(2, 0, _BillingSSN);
                                        }
                                        //REF 
                                        ////if (_BillingStateMedicalNo.Trim() != "")
                                        ////{
                                        ////    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\REF"));
                                        ////    oSegment.set_DataElementValue(1, 0, "0B");//Reference Identification Qualifier("0B" stands for-> State Licence No)
                                        ////    if (_BillingStateMedicalNo.Length > 9)
                                        ////    {
                                        ////        _BillingStateMedicalNo = _BillingStateMedicalNo.Substring(0, 9);
                                        ////    }
                                        ////    oSegment.set_DataElementValue(2, 0, _BillingStateMedicalNo);
                                        ////}
                                        #endregion

                                        //'******************************************************************************************************
                                        //'******* SUBSCRIBER HIERARCHICAL LEVEL ****************************************************************
                                        //'******************************************************************************************************
                                        #region Subscriber

                                        #region Subscriber HL Loop - 2000B

                                        nHlCount = nHlCount + 1;
                                        nHlSubscriberParent = nHlCount;

                                        //2000B SUBSCRIBER HL LOOP
                                        //HL-SUBSCRIBER
                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
                                        oSegment.set_DataElementValue(1, 0, nHlCount.ToString().Trim());
                                        oSegment.set_DataElementValue(2, 0, nHlProvParent.ToString().Trim());
                                        oSegment.set_DataElementValue(3, 0, "22");

                                        if (_SubscriberRelationshipCode == "18")
                                            oSegment.set_DataElementValue(4, 0, "0");
                                        else
                                            oSegment.set_DataElementValue(4, 0, "1");

                                        //SBR SUBSCRIBER INFORMATION
                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\SBR"));
                                        oSegment.set_DataElementValue(1, 0, _SubscriberInsurancePST);//"P");
                                        oSegment.set_DataElementValue(2, 0, _SubscriberRelationshipCode);
                                        if (_SubscriberInsuranceBelongs == "")
                                        {
                                            MessageBox.Show("Insurance Type Code is not there for the Insurance name " + _PayerName + "", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            return;
                                        }
                                        oSegment.set_DataElementValue(9, 0, _SubscriberInsuranceBelongs);//"HM");


                                        //2010BA SUBSCRIBER
                                        //NM1 SUBSCRIBER NAME
                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
                                        oSegment.set_DataElementValue(1, 0, "IL");
                                        oSegment.set_DataElementValue(2, 0, "1");
                                        oSegment.set_DataElementValue(3, 0, _SubscriberLName.Trim());//"SubscriberLastOrgName"
                                        oSegment.set_DataElementValue(4, 0, _SubscriberFName.Trim());//"SubscriberFirstname"
                                        oSegment.set_DataElementValue(8, 0, "MI");
                                        oSegment.set_DataElementValue(9, 0, _SubscriberInsuranceID.Trim());//"Insurance Id"

                                        //N3 SUBSCRIBER ADDRESS
                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N3"));
                                        oSegment.set_DataElementValue(1, 0, _SubscriberAddress.Trim());//"SubscriberAddress"

                                        //N4 SUBSCRIBER CITY
                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N4"));
                                        oSegment.set_DataElementValue(1, 0, _SubscriberCity.Trim());//"SubscriberCity"
                                        oSegment.set_DataElementValue(2, 0, _SubscriberState.Trim());//"SubscrberState"
                                        oSegment.set_DataElementValue(3, 0, _SubscriberZIP.Trim());//"SubscriberZip"

                                        #endregion SubscriberHL Loop - 2000B

                                        if (_SubscriberRelationshipCode == "18")
                                        {
                                            //DMG SUBSCRIBER DEMOGRAPHIC INFORMATION
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\DMG"));
                                            oSegment.set_DataElementValue(1, 0, "D8");
                                            oSegment.set_DataElementValue(2, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(_SubscriberDOB)));//"SubscriberDOB"
                                            if (_SubscriberGender.Trim() != "")
                                            {
                                                if (_SubscriberGender.Trim().ToUpper() == "OTHER")
                                                {
                                                    _SubscriberGender = "U";
                                                }
                                                oSegment.set_DataElementValue(3, 0, _SubscriberGender.Trim().Substring(0, 1).ToUpper());//"SubscriberGender"
                                            }
                                            else
                                            {
                                                oSegment.set_DataElementValue(3, 0, "M");
                                            }

                                            if (_PayerID == "")
                                            {
                                                MessageBox.Show("Payer ID is not there for the Insurance", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                return;
                                            }
                                            #region Payer Information Loop 2010BB
                                            //2010BB SUBSCRIBER/PAYER
                                            //NM1 PAYER NAME
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1(2)\\NM1"));
                                            oSegment.set_DataElementValue(1, 0, "PR");
                                            oSegment.set_DataElementValue(2, 0, "2");
                                            oSegment.set_DataElementValue(3, 0, _PayerName);//"PayerLastOrgName"
                                            oSegment.set_DataElementValue(8, 0, "PI");//This is the payer ID of Medicare for Michigan
                                            oSegment.set_DataElementValue(9, 0, _PayerID);//dtInsurance.Rows[0]["sPayerID"].ToString());//PayerID

                                            ////////N3 SUBSCRIBER ADDRESS
                                            ////ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1(2)\\N3"));
                                            ////oSegment.set_DataElementValue(1, 0, _PayerAddress.Trim());//"InsuranceAddress"

                                            ////////N4 SUBSCRIBER CITY
                                            ////ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1(2)\\N4"));
                                            ////oSegment.set_DataElementValue(1, 0, _PayerCity.Trim());//"InsuranceCity"
                                            ////oSegment.set_DataElementValue(2, 0, _PayerState.Trim());//"InsuranceState"
                                            ////oSegment.set_DataElementValue(3, 0, _PayerZip.Trim());//"InsuranceZip"

                                            #endregion


                                            //******* SUBSCRIBER CLAIM INFORMATION ***************************************************************
                                            //TODO: Get Details in DATATABLE for the fields to be entered in EDI file.
                                            string _FirstPOS = "";
                                            string _NewPOS = "";
                                            string _ClaimTotal = "";
                                            iItemCount = 0;
                                            decimal _claimAmount = 0;
                                            for (int nLine = 0; nLine <= oTransaction.Lines.Count - 1; nLine++)
                                            {
                                             //   oTransLine = new TransactionLine();
                                                oTransLine = oTransaction.Lines[nLine];

                                                //Anil - 05 Nov 2008
                                                //_claimAmount = _claimAmount + oTransLine.Total;
                                                //_ClaimTotal = Convert.ToString(_claimAmount).Substring(0, Convert.ToInt32(Convert.ToString(oTransLine.Total).Length ));

                                                //Vinayak - 05 Nov 2008
                                                _claimAmount = _claimAmount + oTransLine.Total;

                                                _FirstPOS = oTransaction.Lines[0].POSCode;
                                                _NewPOS = oTransLine.POSCode;
                                            }

                                            _ClaimTotal = _claimAmount.ToString("#0.00");

                                            if (_ClaimTotal.Substring(_ClaimTotal.Length - 2, 2) == "00")
                                            {
                                                _ClaimTotal = _ClaimTotal.Substring(0, _ClaimTotal.Length - 3);
                                            }
                                            //if (_FirstPOS ==_NewPOS)
                                            //{
                                            #region Claim Details - Loop 2300
                                            //2300 CLAIM
                                            //CLM CLAIM LEVEL INFORMATION
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\CLM"));
                                            oSegment.set_DataElementValue(1, 0, _PatientAccountNo); //Patient Account no         
                                            oSegment.set_DataElementValue(2, 0, _ClaimTotal.Trim());// Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_TOTAL))); //Claim Amount

                                            oSegment.set_DataElementValue(5, 1, _FirstPOS.Trim()); //21 - Inpatient Hospital

                                            oSegment.set_DataElementValue(5, 3, "1");
                                            oSegment.set_DataElementValue(6, 0, "Y");
                                            oSegment.set_DataElementValue(7, 0, "A");
                                            oSegment.set_DataElementValue(8, 0, "Y");
                                            oSegment.set_DataElementValue(9, 0, "Y");
                                            //oSegment.set_DataElementValue(10, 0, "C");
                                            if (oTransaction.AutoClaim == true)
                                            {
                                                if (oTransaction.AccidentDate.ToString() != "" && oTransaction.AccidentDate > 0)
                                                {
                                                    oSegment.set_DataElementValue(11, 1, "AA");
                                                    oSegment.set_DataElementValue(11, 4, oTransaction.State.Trim());
                                                }
                                            }

                                            string OnsetDate = "";
                                            if (oTransaction.InjuryDate.ToString() != "" || oTransaction.OnsiteDate.ToString() != "" || oTransaction.AccidentDate.ToString() != "")
                                            {
                                                if (oTransaction.InjuryDate.ToString() != "" && oTransaction.InjuryDate > 0)
                                                {
                                                    OnsetDate = Convert.ToString(oTransaction.InjuryDate);
                                                    ////DTP DATE OF ONSET OF CURRENT SYMPTOMS OR ILLNESS
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                    oSegment.set_DataElementValue(1, 0, "431");
                                                    oSegment.set_DataElementValue(2, 0, "D8");
                                                    oSegment.set_DataElementValue(3, 0, OnsetDate);//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpOnsetofCurrentSymptomsorillness.Value.ToShortDateString())).Trim());     //Claim Date
                                                }
                                                else if (oTransaction.OnsiteDate.ToString() != "" && oTransaction.OnsiteDate > 0)
                                                {
                                                    if (Convert.ToString(oTransaction.OnsiteDate) != Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oTransLine.DateServiceFrom.ToShortDateString())))
                                                    {
                                                        OnsetDate = Convert.ToString(oTransaction.OnsiteDate);
                                                        ////DTP DATE OF CURRENT INJURY
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                        oSegment.set_DataElementValue(1, 0, "431");
                                                        oSegment.set_DataElementValue(2, 0, "D8");
                                                        oSegment.set_DataElementValue(3, 0, OnsetDate);//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpOnsetofCurrentSymptomsorillness.Value.ToShortDateString())).Trim());     //Claim Date
                                                    }
                                                }
                                                if (oTransaction.AutoClaim == true)
                                                {
                                                    if (oTransaction.AccidentDate.ToString() != "" && oTransaction.AccidentDate > 0)
                                                    {
                                                        OnsetDate = Convert.ToString(oTransaction.AccidentDate);
                                                        ////DTP DATE OF ACCIDENT 
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                        oSegment.set_DataElementValue(1, 0, "439");
                                                        oSegment.set_DataElementValue(2, 0, "D8");
                                                        oSegment.set_DataElementValue(3, 0, OnsetDate);//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpOnsetofCurrentSymptomsorillness.Value.ToShortDateString())).Trim());     //Claim Date
                                                    }
                                                }
                                            }

                                            //DTP DATE OF ONSET of similar symptoms or illness
                                            //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                            //oSegment.set_DataElementValue(1, 0, "438");
                                            //oSegment.set_DataElementValue(2, 0, "D8");
                                            //oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpOnsetoSimilarSyptomsorillness.Value.ToShortDateString())).Trim());
                                            //
                                            if (_FirstPOS.Trim() != "11")
                                            {
                                                if (oTransaction.HospitalizationDateFrom > 0 && oTransaction.HospitalizationDateFrom.ToString() != "")
                                                {
                                                    //DTP DATE OF Hospitalization (Admission) 
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                    oSegment.set_DataElementValue(1, 0, "435");
                                                    oSegment.set_DataElementValue(2, 0, "D8");
                                                    oSegment.set_DataElementValue(3, 0, oTransaction.HospitalizationDateFrom.ToString());//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpHospitalizationDate.Value.ToShortDateString())).Trim());     //Claim Date
                                                }

                                                if (oTransaction.HospitalizationDateTo > 0 && oTransaction.HospitalizationDateTo.ToString() != "")
                                                {
                                                    ////DTP DATE OF Discharge 
                                                    //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                    //oSegment.set_DataElementValue(1, 0, "096");
                                                    //oSegment.set_DataElementValue(2, 0, "D8");
                                                    //oSegment.set_DataElementValue(3, 0, oTransaction.HospitalizationDateTo.ToString());//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpHospitalDischargeDate.Value.ToShortDateString())).Trim());     //Claim Date
                                                    ////
                                                }
                                            }

                                            if (oTransaction.UnableToWorkFromDate > 0 && oTransaction.UnableToWorkFromDate.ToString() != "")
                                            {
                                                //DTP DATE OF (Intial Disability period last day worked)
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                oSegment.set_DataElementValue(1, 0, "297");
                                                oSegment.set_DataElementValue(2, 0, "D8");
                                                oSegment.set_DataElementValue(3, 0, oTransaction.UnableToWorkFromDate.ToString());//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpLastDayWorked.Value.ToShortDateString())).Trim());     //Claim Date
                                                //
                                            }

                                            if (oTransaction.UnableToWorkTillDate > 0 && oTransaction.UnableToWorkTillDate.ToString() != "")
                                            {
                                                //DTP DATE OF (Intial Disability period return to work)
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                oSegment.set_DataElementValue(1, 0, "296");
                                                oSegment.set_DataElementValue(2, 0, "D8");
                                                oSegment.set_DataElementValue(3, 0, oTransaction.UnableToWorkTillDate.ToString());//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpReturnToWork.Value.ToShortDateString())).Trim());     //Claim Date
                                                //
                                            }

                                            if (_PriorAuthorizationNo.Trim() != "")
                                            {
                                                //REF CLEARING HOUSE CLAIM NUMBER
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\REF"));
                                                oSegment.set_DataElementValue(1, 0, "G1");
                                                oSegment.set_DataElementValue(2, 0, _PriorAuthorizationNo.Trim()); //Claim No
                                            }



                                            #endregion

                                            #region HI - Diagnosis
                                            //HI HEALTH CARE DIAGNOSIS CODES

                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\HI"));
                                            oSegment.set_DataElementValue(1, 1, "BK");
                                            if (oTransaction.Lines[0].Dx1Code.ToString().Trim() != "")
                                            {
                                                oSegment.set_DataElementValue(1, 2, oTransaction.Lines[0].Dx1Code.ToString().Replace(".", "").Trim());// Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_DX1_CODE)));  // "0340");
                                                if (IsValidICD9(Convert.ToString(oTransaction.Lines[0].Dx1Code.Trim())) == false)
                                                {
                                                    return;
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("Principle Diagnosis is not given.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                return;
                                            }
                                            if (oTransaction.Lines[0].Dx2Code.ToString().Trim() != "")
                                            {
                                                oSegment.set_DataElementValue(2, 1, "BF");
                                                oSegment.set_DataElementValue(2, 2, oTransaction.Lines[0].Dx2Code.ToString().Replace(".", "").Trim());//Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_DX1_CODE))); // oCase.CaseDiagnosis[0].DiagnosisCode.ToString().Replace(".", "").Trim());
                                                if (IsValidICD9(Convert.ToString(oTransaction.Lines[0].Dx2Code.Trim())) == false)
                                                {
                                                    return;
                                                }
                                            }
                                            if (oTransaction.Lines[0].Dx3Code.ToString().Trim() != "")
                                            {
                                                oSegment.set_DataElementValue(3, 1, "BF");
                                                oSegment.set_DataElementValue(3, 2, oTransaction.Lines[0].Dx3Code.ToString().Replace(".", "").Trim());
                                                if (IsValidICD9(Convert.ToString(oTransaction.Lines[0].Dx3Code.Trim())) == false)
                                                {
                                                    return;
                                                }
                                            }
                                            if (oTransaction.Lines[0].Dx4Code.ToString().Trim() != "")
                                            {
                                                oSegment.set_DataElementValue(4, 1, "BF");
                                                oSegment.set_DataElementValue(4, 2, oTransaction.Lines[0].Dx4Code.ToString().Replace(".", "").Trim());
                                                if (IsValidICD9(Convert.ToString(oTransaction.Lines[0].Dx4Code.Trim())) == false)
                                                {
                                                    return;
                                                }
                                            }
                                            if (oTransaction.Lines[0].Dx5Code.ToString().Trim() != "")
                                            {
                                                oSegment.set_DataElementValue(5, 1, "BF");
                                                oSegment.set_DataElementValue(5, 2, oTransaction.Lines[0].Dx5Code.ToString().Replace(".", "").Trim());
                                            }
                                            if (oTransaction.Lines[0].Dx6Code.ToString().Trim() != "")
                                            {
                                                oSegment.set_DataElementValue(6, 1, "BF");
                                                oSegment.set_DataElementValue(6, 2, oTransaction.Lines[0].Dx6Code.ToString().Replace(".", "").Trim());
                                            }
                                            if (oTransaction.Lines[0].Dx7Code.ToString().Trim() != "")
                                            {
                                                oSegment.set_DataElementValue(7, 1, "BF");
                                                oSegment.set_DataElementValue(7, 2, oTransaction.Lines[0].Dx7Code.ToString().Replace(".", "").Trim());
                                            }
                                            if (oTransaction.Lines[0].Dx8Code.ToString().Trim() != "")
                                            {
                                                oSegment.set_DataElementValue(8, 1, "BF");
                                                oSegment.set_DataElementValue(8, 2, oTransaction.Lines[0].Dx8Code.ToString().Replace(".", "").Trim());
                                            }
                                            //} 
                                            #endregion

                                            #region Referring Provider - 2310A


                                            if (_ReferralLName != "" && _ReferralNPI != "")
                                            {

                                                //2310B Referring PROVIDER
                                                //NM1 Referring PROVIDER NAME
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\NM1"));
                                                oSegment.set_DataElementValue(1, 0, "DN");
                                                oSegment.set_DataElementValue(2, 0, "1");
                                                oSegment.set_DataElementValue(3, 0, _ReferralLName.Trim()); //"ReferringLastname"
                                                oSegment.set_DataElementValue(4, 0, _ReferralFName.Trim());//"ReferringFirstname"
                                                oSegment.set_DataElementValue(5, 0, _ReferralMName.Trim());
                                                oSegment.set_DataElementValue(8, 0, "XX");
                                                oSegment.set_DataElementValue(9, 0, _ReferralNPI.Trim());//"NPI"

                                                //PRV REFERRING PROVIDER INFORMATION
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\PRV"));
                                                oSegment.set_DataElementValue(1, 0, "RF");
                                                oSegment.set_DataElementValue(2, 0, "ZZ");//Mutually Defined
                                                oSegment.set_DataElementValue(3, 0, _ReferralTaxonomy.Trim());//Reference Identification

                                                //REF
                                                if (_ReferralEmployerID.Trim() != "")
                                                {
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\REF"));
                                                    oSegment.set_DataElementValue(1, 0, "EI");//// Employer Identification Number
                                                    oSegment.set_DataElementValue(2, 0, _ReferralEmployerID.Trim());//"1039255");// 
                                                }
                                                else if (_ReferralSSN.Trim() != "")
                                                {
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\REF"));
                                                    oSegment.set_DataElementValue(1, 0, "SY");//// Social Security Number
                                                    oSegment.set_DataElementValue(2, 0, _ReferralSSN.Trim());//"1039255");// 
                                                }


                                            }
                                            #endregion Referring Provider

                                            #region Rendering Provider - 2310B

                                            if (_RenderingLName.Trim() != _BillingLName.Trim() && _RenderingFName.Trim() != _BillingFName.Trim() && _RenderingMName.Trim() != _BillingMName.Trim())
                                            {
                                                //2310B RENDERING PROVIDER
                                                //NM1 RENDERING PROVIDER NAME
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\NM1"));
                                                oSegment.set_DataElementValue(1, 0, "82");
                                                oSegment.set_DataElementValue(2, 0, "1");
                                                FillProviderDetails(oTransaction.Lines[0].RefferingProviderId, ProviderType.RenderingProvider);
                                                oSegment.set_DataElementValue(3, 0, _RenderingLName);//Billing provider name
                                                oSegment.set_DataElementValue(4, 0, _RenderingFName);
                                                oSegment.set_DataElementValue(5, 0, _RenderingMName);
                                                oSegment.set_DataElementValue(8, 0, "XX");
                                                oSegment.set_DataElementValue(9, 0, _RenderingNPI.Trim());//oProviderDetails.NPI);//Billing provider ID/NPI


                                                //PRV RENDERING PROVIDER INFORMATION
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\PRV"));
                                                oSegment.set_DataElementValue(1, 0, "PE");
                                                oSegment.set_DataElementValue(2, 0, "ZZ");//Mutually Defined
                                                oSegment.set_DataElementValue(3, 0, _RenderingTaxonomy.Trim());//Reference Identification
                                            }

                                            #endregion

                                            #region Facility - 2310D

                                            //2310D SERVICE LOCATION
                                            //NM1 SERVICE FACILITY LOCATION

                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1(2)\\NM1"));
                                            oSegment.set_DataElementValue(1, 0, "77");
                                            oSegment.set_DataElementValue(2, 0, "2");
                                            oSegment.set_DataElementValue(3, 0, _FacilityName);//"FacilityName"
                                            oSegment.set_DataElementValue(8, 0, "XX");//NPI code
                                            oSegment.set_DataElementValue(9, 0, _FacilityNPI.Trim());//NPI

                                            //N3 SERVICE FACILITY ADDRESS
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1(2)\\N3"));
                                            oSegment.set_DataElementValue(1, 0, _FacilityAddress);//"FacilityAddr"

                                            //N4 SERVICE FACILITY CITY/STATE/ZIP
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1(2)\\N4"));
                                            oSegment.set_DataElementValue(1, 0, _FacilityCity);//"FacilityCity"
                                            oSegment.set_DataElementValue(2, 0, _FacilityState);//"FacilityState"
                                            oSegment.set_DataElementValue(3, 0, _FacilityZip);//"FacilityZip"


                                            #endregion

                                            #region Subscriber Secondary Insurance - Loop 2320

                                            //LOOP - 2320
                                            if (bSecondaryInsurance)
                                            {

                                                #region SBR - SUBSCRIBER INFORMATION for Secondary Information

                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR"));
                                                //1.Payer Resposibilty Sequence No.
                                                oSegment.set_DataElementValue(1, 0, _OtherInsurancePST.Trim()); //P - Primary

                                                //2.Individual Relationship code
                                                oSegment.set_DataElementValue(2, 0, _OtherInsuranceRelationshipCode.Trim());//"18"); // Hard coded(Individual Relationship code) 18 - Self

                                                //3.Refrence identification
                                                oSegment.set_DataElementValue(3, 0, _OtherInsuranceGroupID);//"22145");///Policy no

                                                //5.Insurance Type Code
                                                //oSegment.set_DataElementValue(5, 0, "C1"); // C1 - Commercial (Insurance Type Code)


                                                //oSegment.set_DataElementValue(6, 0, "6"); // 6 - No Co-ordination of Benefit

                                                ////8.Employment Status Code(Not Used)
                                                //oSegment.set_DataElementValue(8, 0, "AC"); // Employment status (AC - Active)


                                                //9.Claim Filing Indicator
                                                //if (_OtherInsuranceType.Trim() == "")
                                                //{
                                                //    MessageBox.Show("Insurance Type Code is not there for the Insurance name: " + _OtherInsuranceName + "", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                //    return;
                                                //}
                                                if (_OtherInsurancePST.Trim().ToUpper() != "P" && _OtherInsuranceType.Trim().ToUpper() == "MB")
                                                {
                                                    oSegment.set_DataElementValue(9, 0, _OtherInsuranceType.Trim()); //Commercial Insurance company
                                                }
                                                #endregion SBR - SUBSCRIBER INFORMATION for Secondary Information

                                                #region CAS - Claim Adjustment

                                                //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\CAS"));
                                                //oSegment.set_DataElementValue(1, 0, "PI");//PR - Patient Responsibility
                                                //oSegment.set_DataElementValue(2, 0, "96");
                                                //oSegment.set_DataElementValue(3, 0, "300");

                                                #endregion CAS - Claim Adjustment

                                                #region AMT - Amount

                                                //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\AMT"));
                                                //oSegment.set_DataElementValue(1, 0, "D");
                                                //oSegment.set_DataElementValue(2, 0, "0");

                                                //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\AMT"));
                                                //oSegment.set_DataElementValue(1, 0, "F2");
                                                //oSegment.set_DataElementValue(2, 0, "100");

                                                #endregion AMT - Amount

                                                #region MOA - Medicare Outpatient Adjudication

                                                //ediDataSegment.Set(ref oSegment,(ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\MOA"));
                                                //oSegment.set_DataElementValue(1,0,"20");
                                                //oSegment.set_DataElementValue(2,0,"300");
                                                //oSegment.set_DataElementValue(3,0,"125");

                                                #endregion

                                                #region DMG  - Demographic

                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\DMG"));
                                                oSegment.set_DataElementValue(1, 0, "D8");
                                                oSegment.set_DataElementValue(2, 0, gloDateMaster.gloDate.DateAsNumber(_OtherInsuranceSubscriberDOB).ToString());//"SubscriberDOB"
                                                oSegment.set_DataElementValue(3, 0, _OtherInsuranceSubscriberGender);//"SubscriberGender"

                                                #endregion DMG  - Demographic

                                                #region OI - Other Insurance

                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\OI"));
                                                oSegment.set_DataElementValue(3, 0, "Y");
                                                //oSegment.set_DataElementValue(4, 0, "C");
                                                oSegment.set_DataElementValue(6, 0, "Y");

                                                #endregion OI - Other Insurance

                                                //2330A SUBSCRIBER
                                                #region NM1 SUBSCRIBER NAME - 2330A

                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\NM1\\NM1"));
                                                oSegment.set_DataElementValue(1, 0, "IL");
                                                oSegment.set_DataElementValue(2, 0, "1");
                                                oSegment.set_DataElementValue(3, 0, _OtherInsuranceSubscriberLName);//"SubscriberLastOrgName"
                                                oSegment.set_DataElementValue(4, 0, _OtherInsuranceSubscriberFName);//"SubscriberFirstname"
                                                oSegment.set_DataElementValue(8, 0, "MI");
                                                oSegment.set_DataElementValue(9, 0, _OtherInsuranceID);//"SubscriberMemberID"

                                                //N3 SUBSCRIBER ADDRESS
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\NM1\\N3"));
                                                oSegment.set_DataElementValue(1, 0, _OtherInsuranceAddress);//"SubscriberAddress"

                                                //N4 SUBSCRIBER CITY
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\NM1\\N4"));
                                                oSegment.set_DataElementValue(1, 0, _OtherInsuranceCity);//"SubscriberCity"
                                                oSegment.set_DataElementValue(2, 0, _OtherInsuranceState);//"SubscrberState"
                                                oSegment.set_DataElementValue(3, 0, _OtherInsuranceZIP);//"SubscriberZip"

                                                #endregion NM1 SUBSCRIBER NAME

                                                #region Payer Information - 2330B

                                                //2330B SUBSCRIBER/PAYER
                                                //NM1 PAYER NAME
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\NM1\\NM1"));
                                                oSegment.set_DataElementValue(1, 0, "PR");
                                                oSegment.set_DataElementValue(2, 0, "2");

                                                oSegment.set_DataElementValue(3, 0, _OtherInsuranceName);//dtInsurance.Rows[0]["sSubscriberName"].ToString().Trim());//"PayerLastOrgName"

                                                oSegment.set_DataElementValue(8, 0, "PI");//This is the payer ID of Medicare for Michigan
                                                oSegment.set_DataElementValue(9, 0, _OtherInsurancePayerID.Trim());//dtInsurance.Rows[0]["sPayerID"].ToString());//PayerID
                                                //}

                                                #endregion Payer Information

                                            }

                                            #endregion Subscriber Secondary Insurance
                                            //}//end of IF loop for POS
                                            for (int nLine = 0; nLine <= oTransaction.Lines.Count - 1; nLine++)
                                            {
                                                iItemCount = 1;
                                                iItemCount = iItemCount + nLine;
                                           //     oTransLine = new TransactionLine();
                                                oTransLine = oTransaction.Lines[nLine];
                                                #region Service Line
                                                //******* SUBSCRIBER SERVICE LINE *************************************************************
                                                //TODO: Get the datatable for service info to add fields of service in EDI file.
                                                //2400 SERVICE LINE
                                                sInstance = iItemCount.ToString().Trim();
                                                //LX SERVICE LINE COUNTER
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\LX"));
                                                oSegment.set_DataElementValue(1, 0, iItemCount.ToString());

                                                //SV1 PROFESSIONAL SERVICE
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\SV1"));
                                                oSegment.set_DataElementValue(1, 1, "HC");
                                                oSegment.set_DataElementValue(1, 2, oTransLine.CPTCode.ToString().Replace(".", ""));//"ServiceID"
                                                if (oTransLine.Mod1Code.ToString() != "")
                                                {
                                                    oSegment.set_DataElementValue(1, 3, oTransLine.Mod1Code.ToString());//Modifier 1
                                                }
                                                if (oTransLine.Mod2Code.ToString() != "")
                                                {
                                                    oSegment.set_DataElementValue(1, 4, oTransLine.Mod2Code.ToString());//Modifier 2
                                                }
                                                string _ClaimLineCharges = Convert.ToString(oTransLine.Charges);

                                                if (_ClaimLineCharges.Substring(_ClaimLineCharges.Length - 2, 2) == "00")
                                                {
                                                    _ClaimLineCharges = _ClaimLineCharges.Substring(0, _ClaimLineCharges.Length - 3);
                                                }
                                                oSegment.set_DataElementValue(2, 0, _ClaimLineCharges);//"ServiceAmount"
                                                oSegment.set_DataElementValue(3, 0, "UN");//UN stands for UNITS
                                                oSegment.set_DataElementValue(4, 0, oTransLine.Unit.ToString());//Unit/Quantity
                                                if (oTransLine.Dx1Ptr.ToString() == "True")
                                                {
                                                    oSegment.set_DataElementValue(7, 1, "1");//"Diagnosis Pointer1" (Must Use)
                                                    if (oTransLine.Dx2Ptr.ToString() == "True")
                                                    {
                                                        oSegment.set_DataElementValue(7, 2, "2");//"Diagnosis Pointer2"
                                                    }
                                                    if (oTransLine.Dx3Ptr.ToString() == "True")
                                                    {
                                                        oSegment.set_DataElementValue(7, 3, "3");//"Diagnosis Pointer3"
                                                    }
                                                    if (oTransLine.Dx4Ptr.ToString() == "True")
                                                    {
                                                        oSegment.set_DataElementValue(7, 4, "4");//"Diagnosis Pointer4"
                                                    }
                                                }
                                                else if (oTransLine.Dx2Ptr.ToString() == "True")
                                                {
                                                    oSegment.set_DataElementValue(7, 1, "2");//"Diagnosis Pointer1"
                                                }
                                                else if (oTransLine.Dx3Ptr.ToString() == "True")
                                                {
                                                    oSegment.set_DataElementValue(7, 1, "3");//"Diagnosis Pointer1"
                                                }
                                                else if (oTransLine.Dx4Ptr.ToString() == "True")
                                                {
                                                    oSegment.set_DataElementValue(7, 1, "4");//"Diagnosis Pointer1"
                                                }

                                                ////oSegment.set_DataElementValue(9, 0, "N");//Y=Yes,N=No
                                                //DTP DATE - SERVICE DATE(S)
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\DTP"));
                                                oSegment.set_DataElementValue(1, 0, "472");
                                                oSegment.set_DataElementValue(2, 0, "D8");
                                                oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oTransLine.DateServiceFrom.ToShortDateString())));//Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_DATEFROM)));//"ServiceDate"

                                                #endregion
                                            }
                                        #endregion " Subscriber "

                                        }//end of if loop for Subscriber as Patient
                                        else
                                        {
                                            #region "Dependent Loop"

                                            //////*****************************************************************************************************
                                            //////******* DEPENDENT HIERARCHICAL LEVEL ****************************************************************
                                            //////*****************************************************************************************************
                                            ////TODO: Get the datatable for dependent info to add fields of service in EDI file.
                                            if (_PayerID == "")
                                            {
                                                MessageBox.Show("Payer ID is not there for the Insurance", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                return;
                                            }
                                            #region Payer Information Loop 2010BB
                                            //2010BB SUBSCRIBER/PAYER
                                            //NM1 PAYER NAME
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1(2)\\NM1"));
                                            oSegment.set_DataElementValue(1, 0, "PR");
                                            oSegment.set_DataElementValue(2, 0, "2");
                                            oSegment.set_DataElementValue(3, 0, _PayerName);//"PayerLastOrgName"
                                            oSegment.set_DataElementValue(8, 0, "PI");//This is the payer ID of Medicare for Michigan
                                            oSegment.set_DataElementValue(9, 0, _PayerID);//dtInsurance.Rows[0]["sPayerID"].ToString());//PayerID

                                            #endregion

                                            nHlCount = nHlCount + 1;

                                            //2000B DEPENDENT HL LOOP
                                            //HL-DEPENDENT
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
                                            oSegment.set_DataElementValue(1, 0, nHlCount.ToString());
                                            oSegment.set_DataElementValue(2, 0, nHlSubscriberParent.ToString());
                                            oSegment.set_DataElementValue(3, 0, "23");
                                            oSegment.set_DataElementValue(4, 0, "0");


                                            //PAT - PATIENT/DEPENDENT INFORMATION
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\PAT"));
                                            oSegment.set_DataElementValue(1, 0, _SubscriberRelationshipCode); //01 - Spouse 19 - Child

                                            #region " Patient Info"

                                            //NM1 - INDIVIDUAL OR ORGANIZATIONAL NAME
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1"));
                                            oSegment.set_DataElementValue(1, 0, "QC");
                                            oSegment.set_DataElementValue(2, 0, "1");
                                            oSegment.set_DataElementValue(3, 0, _PatientLastName.Trim());//Patient Last Name
                                            oSegment.set_DataElementValue(4, 0, _PatientFirstName.Trim());//Patient First Name

                                            //N3 - ADDRESS INFORMATION
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N3"));
                                            oSegment.set_DataElementValue(1, 0, _PatientAddress.Trim());//"Address"

                                            //N4 - GEOGRAPHIC LOCATION
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N4"));
                                            oSegment.set_DataElementValue(1, 0, _PatientCity.Trim());//"City"
                                            oSegment.set_DataElementValue(2, 0, _PatientState.Trim());//"State"
                                            oSegment.set_DataElementValue(3, 0, _PatientZip.Trim());//"Zip"

                                            //DMG - DEMOGRAPHIC INFORMATION
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\DMG"));
                                            oSegment.set_DataElementValue(1, 0, "D8");
                                            oSegment.set_DataElementValue(2, 0, _PatientDOB);
                                            oSegment.set_DataElementValue(3, 0, _PatientGender.Trim());

                                            #endregion " Patient Info"

                                            //******* DEPENDENT CLAIM INFORMATION *************************************************************
                                            //TODO: Get the datatable for Claim info to add fields of service in EDI file
                                            string _FirstPOS = "";
                                            string _NewPOS = "";
                                            string _ClaimTotal = "";
                                            iItemCount = 0;
                                            iItemCount = 1;
                                            decimal _claimAmount = 0;
                                            for (int nLine = 0; nLine <= oTransaction.Lines.Count - 1; nLine++)
                                            {
                                              //  oTransLine = new TransactionLine();
                                                oTransLine = oTransaction.Lines[nLine];
                                                //Anil - 05 Nov 2008
                                                //_claimAmount = _claimAmount + oTransLine.Total;
                                                //_ClaimTotal = Convert.ToString(_claimAmount).Substring(0, Convert.ToInt32(Convert.ToString(oTransLine.Total).Length ));

                                                //Vinayak - 05 Nov 2008
                                                _claimAmount = _claimAmount + oTransLine.Total;

                                                _FirstPOS = oTransaction.Lines[0].POSCode;
                                                _NewPOS = oTransLine.POSCode;
                                            }

                                            _ClaimTotal = _claimAmount.ToString("#0.00");
                                            if (_ClaimTotal.Substring(_ClaimTotal.Length - 2, 2) == "00")
                                            {
                                                _ClaimTotal = _ClaimTotal.Substring(0, _ClaimTotal.Length - 3);
                                            }

                                            #region "Dependent Claim Level"
                                            //2300 CLAIM
                                            //CLM CLAIM LEVEL INFORMATION
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\CLM"));
                                            oSegment.set_DataElementValue(1, 0, _PatientAccountNo); //Patient Account no         
                                            oSegment.set_DataElementValue(2, 0, _ClaimTotal.Trim()); //Claim Amount
                                            oSegment.set_DataElementValue(5, 1, _FirstPOS.Trim()); //21 - Inpatient Hospital
                                            oSegment.set_DataElementValue(5, 3, "1");
                                            oSegment.set_DataElementValue(6, 0, "Y");
                                            oSegment.set_DataElementValue(7, 0, "A");
                                            oSegment.set_DataElementValue(8, 0, "Y");
                                            oSegment.set_DataElementValue(9, 0, "Y");
                                            oSegment.set_DataElementValue(10, 0, "C");

                                            string OnsetDate = "";
                                            if (oTransaction.InjuryDate.ToString() != "" || oTransaction.OnsiteDate.ToString() != "")
                                            {
                                                if (oTransaction.InjuryDate.ToString() != "" && oTransaction.InjuryDate > 0)
                                                {
                                                    OnsetDate = Convert.ToString(oTransaction.InjuryDate);
                                                    ////DTP DATE OF ONSET of current symptoms or illness
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                    oSegment.set_DataElementValue(1, 0, "431");
                                                    oSegment.set_DataElementValue(2, 0, "D8");
                                                    oSegment.set_DataElementValue(3, 0, OnsetDate);//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpOnsetofCurrentSymptomsorillness.Value.ToShortDateString())).Trim());     //Claim Date
                                                }
                                                else if (oTransaction.OnsiteDate.ToString() != "" && oTransaction.OnsiteDate > 0)
                                                {
                                                    OnsetDate = Convert.ToString(oTransaction.OnsiteDate);
                                                    ////DTP DATE OF ONSET of current symptoms or illness
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                    oSegment.set_DataElementValue(1, 0, "431");
                                                    oSegment.set_DataElementValue(2, 0, "D8");
                                                    oSegment.set_DataElementValue(3, 0, OnsetDate);//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpOnsetofCurrentSymptomsorillness.Value.ToShortDateString())).Trim());     //Claim Date
                                                }

                                                if (oTransaction.AccidentDate.ToString() != "" && oTransaction.AccidentDate > 0)
                                                {
                                                    OnsetDate = Convert.ToString(oTransaction.AccidentDate);
                                                    ////DTP DATE OF ACCIDENT 
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                    oSegment.set_DataElementValue(1, 0, "439");
                                                    oSegment.set_DataElementValue(2, 0, "D8");
                                                    oSegment.set_DataElementValue(3, 0, OnsetDate);//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpOnsetofCurrentSymptomsorillness.Value.ToShortDateString())).Trim());     //Claim Date
                                                }
                                            }

                                            //DTP DATE OF ONSET of similar symptoms or illness
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                            oSegment.set_DataElementValue(1, 0, "438");
                                            oSegment.set_DataElementValue(2, 0, "D8");
                                            oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpOnsetoSimilarSyptomsorillness.Value.ToShortDateString())).Trim());
                                            //
                                            if (_FirstPOS.Trim() != "11")
                                            {
                                                if (oTransaction.HospitalizationDateFrom > 0 && oTransaction.HospitalizationDateFrom.ToString() != "")
                                                {
                                                    //DTP DATE OF Hospitalization (Admission) 
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                    oSegment.set_DataElementValue(1, 0, "435");
                                                    oSegment.set_DataElementValue(2, 0, "D8");
                                                    oSegment.set_DataElementValue(3, 0, oTransaction.HospitalizationDateFrom.ToString());//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpHospitalizationDate.Value.ToShortDateString())).Trim());     //Claim Date
                                                }

                                                if (oTransaction.HospitalizationDateTo > 0 && oTransaction.HospitalizationDateTo.ToString() != "")
                                                {
                                                    ////DTP DATE OF Discharge 
                                                    //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                    //oSegment.set_DataElementValue(1, 0, "096");
                                                    //oSegment.set_DataElementValue(2, 0, "D8");
                                                    //oSegment.set_DataElementValue(3, 0, oTransaction.HospitalizationDateTo.ToString());//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpHospitalDischargeDate.Value.ToShortDateString())).Trim());     //Claim Date
                                                    ////
                                                }
                                            }

                                            if (oTransaction.UnableToWorkFromDate > 0 && oTransaction.UnableToWorkFromDate.ToString() != "")
                                            {
                                                //DTP DATE OF (Intial Disability period last day worked)
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                oSegment.set_DataElementValue(1, 0, "297");
                                                oSegment.set_DataElementValue(2, 0, "D8");
                                                oSegment.set_DataElementValue(3, 0, oTransaction.UnableToWorkFromDate.ToString());//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpLastDayWorked.Value.ToShortDateString())).Trim());     //Claim Date
                                                //
                                            }

                                            if (oTransaction.UnableToWorkTillDate > 0 && oTransaction.UnableToWorkTillDate.ToString() != "")
                                            {
                                                //DTP DATE OF (Intial Disability period return to work)
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                oSegment.set_DataElementValue(1, 0, "296");
                                                oSegment.set_DataElementValue(2, 0, "D8");
                                                oSegment.set_DataElementValue(3, 0, oTransaction.UnableToWorkTillDate.ToString());//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpReturnToWork.Value.ToShortDateString())).Trim());     //Claim Date
                                                //
                                            }

                                            //REF CLEARING HOUSE CLAIM NUMBER
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\REF"));
                                            oSegment.set_DataElementValue(1, 0, "G1");
                                            oSegment.set_DataElementValue(2, 0, _PriorAuthorizationNo.Trim()); //Claim No

                                            #endregion "Dependent Claim Level"

                                            #region HI - Diagnosis for Dependent
                                            //HI HEALTH CARE DIAGNOSIS CODES

                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\HI"));
                                            oSegment.set_DataElementValue(1, 1, "BK");
                                            if (oTransaction.Lines[0].Dx1Code.ToString() != "")
                                            {
                                                oSegment.set_DataElementValue(1, 2, oTransaction.Lines[0].Dx1Code.ToString().Replace(".", ""));// Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_DX1_CODE)));  // "0340");
                                            }
                                            else
                                            {
                                                MessageBox.Show("Principle Diagnosis is not given.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                return;
                                            }
                                            if (oTransaction.Lines[0].Dx2Code.ToString() != "")
                                            {
                                                oSegment.set_DataElementValue(2, 1, "BF");
                                                oSegment.set_DataElementValue(2, 2, oTransaction.Lines[0].Dx2Code.ToString().Replace(".", ""));//Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_DX1_CODE))); // oCase.CaseDiagnosis[0].DiagnosisCode.ToString().Replace(".", "").Trim());
                                            }
                                            if (oTransaction.Lines[0].Dx3Code.ToString() != "")
                                            {
                                                oSegment.set_DataElementValue(3, 1, "BF");
                                                oSegment.set_DataElementValue(3, 2, oTransaction.Lines[0].Dx3Code.ToString().Replace(".", ""));
                                            }
                                            if (oTransaction.Lines[0].Dx4Code.ToString() != "")
                                            {
                                                oSegment.set_DataElementValue(4, 1, "BF");
                                                oSegment.set_DataElementValue(4, 2, oTransaction.Lines[0].Dx4Code.ToString().Replace(".", ""));
                                            }
                                            if (oTransaction.Lines[0].Dx5Code.ToString() != "")
                                            {
                                                oSegment.set_DataElementValue(5, 1, "BF");
                                                oSegment.set_DataElementValue(5, 2, oTransaction.Lines[0].Dx5Code.ToString().Replace(".", ""));
                                            }
                                            if (oTransaction.Lines[0].Dx6Code.ToString() != "")
                                            {
                                                oSegment.set_DataElementValue(6, 1, "BF");
                                                oSegment.set_DataElementValue(6, 2, oTransaction.Lines[0].Dx6Code.ToString().Replace(".", ""));
                                            }
                                            if (oTransaction.Lines[0].Dx7Code.ToString() != "")
                                            {
                                                oSegment.set_DataElementValue(7, 1, "BF");
                                                oSegment.set_DataElementValue(7, 2, oTransaction.Lines[0].Dx7Code.ToString().Replace(".", ""));
                                            }
                                            if (oTransaction.Lines[0].Dx8Code.ToString() != "")
                                            {
                                                oSegment.set_DataElementValue(8, 1, "BF");
                                                oSegment.set_DataElementValue(8, 2, oTransaction.Lines[0].Dx8Code.ToString().Replace(".", ""));
                                            }
                                            //} 
                                            #endregion

                                            #region Referring Provider -  for Dependent

                                            //2310B RENDERING PROVIDER
                                            //NM1 RENDERING PROVIDER NAME

                                            if (_ReferralLName != "" && _ReferralNPI != "")
                                            {

                                                //2310B Referring PROVIDER
                                                //NM1 Referring PROVIDER NAME
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\NM1"));
                                                oSegment.set_DataElementValue(1, 0, "DN");
                                                oSegment.set_DataElementValue(2, 0, "1");
                                                oSegment.set_DataElementValue(3, 0, _ReferralLName.Trim()); //"ReferringLastname"
                                                oSegment.set_DataElementValue(4, 0, _ReferralFName.Trim());//"ReferringFirstname"
                                                oSegment.set_DataElementValue(5, 0, _ReferralMName.Trim());
                                                oSegment.set_DataElementValue(8, 0, "XX");
                                                oSegment.set_DataElementValue(9, 0, _ReferralNPI.Trim());//"NPI"

                                                //PRV REFERRING PROVIDER INFORMATION
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\PRV"));
                                                oSegment.set_DataElementValue(1, 0, "RF");
                                                oSegment.set_DataElementValue(2, 0, "ZZ");//Mutually Defined
                                                oSegment.set_DataElementValue(3, 0, _ReferralTaxonomy.Trim());//Reference Identification

                                                //REF
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\REF"));
                                                oSegment.set_DataElementValue(1, 0, "EI");//// Employer Identification Number
                                                oSegment.set_DataElementValue(2, 0, _ReferralEmployerID.Trim());//"1039255");// 


                                            }
                                            #endregion Referring Provider

                                            #region Rendering Provider -  for Dependent

                                            //2310B RENDERING PROVIDER
                                            //NM1 RENDERING PROVIDER NAME
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\NM1"));
                                            oSegment.set_DataElementValue(1, 0, "82");
                                            oSegment.set_DataElementValue(2, 0, "1");
                                            FillProviderDetails(oTransaction.ProviderID, ProviderType.RenderingProvider);
                                            oSegment.set_DataElementValue(3, 0, _RenderingLName);//Billing provider name
                                            oSegment.set_DataElementValue(4, 0, _RenderingFName);
                                            oSegment.set_DataElementValue(5, 0, _RenderingMName);
                                            oSegment.set_DataElementValue(8, 0, "XX");
                                            oSegment.set_DataElementValue(9, 0, _RenderingNPI.Trim());//oProviderDetails.NPI);//Billing provider ID/NPI


                                            //PRV RENDERING PROVIDER INFORMATION
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\PRV"));
                                            oSegment.set_DataElementValue(1, 0, "PE");
                                            oSegment.set_DataElementValue(2, 0, "ZZ");//Mutually Defined
                                            oSegment.set_DataElementValue(3, 0, _RenderingTaxonomy.Trim());//Reference Identification


                                            #endregion

                                            #region Facility -  for Dependent

                                            //2310D SERVICE LOCATION
                                            //NM1 SERVICE FACILITY LOCATION

                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1(2)\\NM1"));
                                            oSegment.set_DataElementValue(1, 0, "77");
                                            oSegment.set_DataElementValue(2, 0, "2");
                                            oSegment.set_DataElementValue(3, 0, _FacilityName);//"FacilityName"
                                            //oSegment.set_DataElementValue(8, 0, "XX");//NPI code
                                            //oSegment.set_DataElementValue(9, 0, txtFacilityCode.Text.Trim());//NPI

                                            //N3 SERVICE FACILITY ADDRESS
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1(2)\\N3"));
                                            oSegment.set_DataElementValue(1, 0, _FacilityAddress);//"FacilityAddr"

                                            //N4 SERVICE FACILITY CITY/STATE/ZIP
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1(2)\\N4"));
                                            oSegment.set_DataElementValue(1, 0, _FacilityCity);//"FacilityCity"
                                            oSegment.set_DataElementValue(2, 0, _FacilityState);//"FacilityState"
                                            oSegment.set_DataElementValue(3, 0, _FacilityZip);//"FacilityZip"

                                            #endregion

                                            for (int nLine = 0; nLine <= oTransaction.Lines.Count - 1; nLine++)
                                            {
                                                iItemCount = 1;
                                                iItemCount = iItemCount + nLine;
                                            //    oTransLine = new TransactionLine();
                                                oTransLine = oTransaction.Lines[nLine];

                                                #region Service Line
                                                //******* SUBSCRIBER SERVICE LINE *************************************************************
                                                //TODO: Get the datatable for service info to add fields of service in EDI file.
                                                //2400 SERVICE LINE
                                                sInstance = iItemCount.ToString().Trim();
                                                //LX SERVICE LINE COUNTER
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\LX"));
                                                oSegment.set_DataElementValue(1, 0, iItemCount.ToString());

                                                //SV1 PROFESSIONAL SERVICE
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\SV1"));
                                                oSegment.set_DataElementValue(1, 1, "HC");
                                                oSegment.set_DataElementValue(1, 2, oTransLine.CPTCode.ToString().Replace(".", ""));//"ServiceID"
                                                if (oTransLine.Mod1Code.ToString() != "")
                                                {
                                                    oSegment.set_DataElementValue(1, 3, oTransLine.Mod1Code.ToString());//Modifier 1
                                                }
                                                if (oTransLine.Mod2Code.ToString() != "")
                                                {
                                                    oSegment.set_DataElementValue(1, 4, oTransLine.Mod2Code.ToString());//Modifier 2
                                                }
                                                string _ClaimLineCharges = Convert.ToString(oTransLine.Charges);

                                                if (_ClaimLineCharges.Substring(_ClaimLineCharges.Length - 2, 2) == "00")
                                                {
                                                    _ClaimLineCharges = _ClaimLineCharges.Substring(0, _ClaimLineCharges.Length - 3);
                                                }
                                                oSegment.set_DataElementValue(2, 0, _ClaimLineCharges);//"ServiceAmount"
                                                oSegment.set_DataElementValue(3, 0, "UN");//UN stands for UNITS
                                                oSegment.set_DataElementValue(4, 0, oTransLine.Unit.ToString());//Unit/Quantity
                                                if (oTransLine.Dx1Ptr.ToString() == "True")
                                                {
                                                    oSegment.set_DataElementValue(7, 1, "1");//"Diagnosis Pointer1" (Must Use)
                                                    if (oTransLine.Dx2Ptr.ToString() == "True")
                                                    {
                                                        oSegment.set_DataElementValue(7, 2, "2");//"Diagnosis Pointer2"
                                                    }
                                                    if (oTransLine.Dx3Ptr.ToString() == "True")
                                                    {
                                                        oSegment.set_DataElementValue(7, 3, "3");//"Diagnosis Pointer3"
                                                    }
                                                    if (oTransLine.Dx4Ptr.ToString() == "True")
                                                    {
                                                        oSegment.set_DataElementValue(7, 4, "4");//"Diagnosis Pointer4"
                                                    }
                                                }
                                                else if (oTransLine.Dx2Ptr.ToString() == "True")
                                                {
                                                    oSegment.set_DataElementValue(7, 1, "2");//"Diagnosis Pointer1"
                                                }
                                                else if (oTransLine.Dx3Ptr.ToString() == "True")
                                                {
                                                    oSegment.set_DataElementValue(7, 1, "3");//"Diagnosis Pointer1"
                                                }
                                                else if (oTransLine.Dx4Ptr.ToString() == "True")
                                                {
                                                    oSegment.set_DataElementValue(7, 1, "4");//"Diagnosis Pointer1"
                                                }

                                                //oSegment.set_DataElementValue(9, 0, "N");////Y=Yes, N=No

                                                //DTP DATE - SERVICE DATE(S)
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\DTP"));
                                                oSegment.set_DataElementValue(1, 0, "472");
                                                oSegment.set_DataElementValue(2, 0, "D8");
                                                oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oTransLine.DateServiceFrom.ToShortDateString())));//Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_DATEFROM)));//"ServiceDate"



                                                #endregion
                                            }

                                            #endregion " Dependent "
                                        }//end of else loop for dependent

                                        //Transaction Line Loop
                                    }//Transaction SETS Loop
                                }
                                if (oTransaction != null)
                                {
                                    oTransaction.Dispose();
                                    oTransaction = null;
                                }
                            }

                        }


                        //Save to a file
                        SaveFileDialog oSave = new SaveFileDialog();
                        oSave.Filter = "TEXT Files (*.txt)|*.txt|EDI Files (*.edi)|*.edi|X12 Files (*.X12)|*.X12";
                        if (oSave.ShowDialog(this) == DialogResult.OK)
                        {
                            oEdiDoc.Save(sPath + sEdiFile);
                            System.IO.StreamReader oReader = new System.IO.StreamReader(sPath + sEdiFile);
                            string strData;
                            strData = oReader.ReadToEnd();
                            oReader.Close();

                            System.IO.StreamWriter oWriter = new System.IO.StreamWriter(oSave.FileName);
                            oWriter.Write(strData);
                            oWriter.Close();
                            MessageBox.Show("File Created Successfully", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        oSave.Dispose();
                        oSave = null;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                //if (oDB != null)
                //{
                //    oDB.Disconnect();
                //    oDB.Dispose();
                //}
                if (ogloBilling != null)
                {
                    ogloBilling.Dispose();
                    ogloBilling = null;
                }
            }
            #endregion " Generate EDI "
        }

        private void tls_btnValidate_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateEDIData();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null; 
            }
        }

        private void frmSetupEDIData_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                //if (oEdiDoc != null)
                //{
                //    if (oGroup != null)
                //    {
                //        oGroup.Dispose();
                //    }
                //    if (oSegment != null)
                //    {
                //        oSegment.Dispose();
                //    }
                //    if (oInterchange != null)
                //    {
                //        oInterchange.Dispose();
                //    }
                //    if (oSchema != null)
                //    {
                //        oSchema.Dispose();
                //    }
                //    if (oSchemas != null)
                //    {
                //        oSchemas.Dispose();
                //    }
                //    if (oWarning != null)
                //    {
                //        oWarning.Dispose();
                //    }
                //    if (oWarnings != null)
                //    {
                //        oWarnings.Dispose();
                //    }
                //    oEdiDoc.Dispose();
                //}
              
                oEdiDoc.Close();
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        

        #region " EDI Generation Method "

        //private void GenerateEDIFile(Transaction _SelTransactions)
        //{
        //    #region " Generate EDI "

        //    string sSEFFile = "";
        //    string sEdiFile = "";
        //    string sPath = "";
        //    string sEntity = "";
        //    string sInstance = "";
        //    string _strSQL = "";
        //    DataTable dt;
        //    string _BillingProviderDetails = "";
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //    bool IsSecondaryInsurance = false;
        //    Transaction oTransaction = new Transaction();

        //    try
        //    {

        //        oTransaction = _SelTransactions;
        //        TransactionLine oTransLine = null;
        //        //Here Interchange Loop should come
        //        sPath = AppDomain.CurrentDomain.BaseDirectory;
        //        sSEFFile = "837_X098A1.SEF";     //ToDO :Give the file name at runtime, since it can change
        //        sEdiFile = "837A1.x12";

        //        ediDocument.Set(ref oEdiDoc, new ediDocument());

        //        ediSchemas.Set(ref oSchemas, (ediSchemas)oEdiDoc.GetSchemas());
        //        oSchemas.EnableStandardReference = false;

        //        oEdiDoc.SegmentTerminator = "~\r\n";
        //        oEdiDoc.ElementTerminator = "*";
        //        oEdiDoc.CompositeTerminator = ":";

        //        oEdiDoc.CursorType = DocumentCursorTypeConstants.Cursor_ForwardWrite;
        //        oEdiDoc.set_Property(DocumentPropertyIDConstants.Property_DocumentBufferIO, 2000);

        //        ediSchema.Set(ref oSchema, (ediSchema)oEdiDoc.LoadSchema("837_X098A1.SEF", 0));
        //        System.IO.FileInfo ofile=new System.IO.FileInfo(sPath+sSEFFile);
        //        if (ofile.Exists == false)
        //        {
        //            MessageBox.Show("SEF file is not present in the Base Directory", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            return;
        //        }



        //        #region ISA - Interchange Control Header Segment

        //        //Create Interchange Control Header Segment 

        //        ediInterchange.Set(ref oInterchange, (ediInterchange)oEdiDoc.CreateInterchange("X", "004010"));
        //        ediDataSegment.Set(ref oSegment, (ediDataSegment)oInterchange.GetDataSegmentHeader());

        //        oSegment.set_DataElementValue(1, 0, "00");
        //        oSegment.set_DataElementValue(3, 0, "00");

        //        oSegment.set_DataElementValue(5, 0, "ZZ");            //ZZ - Mutually Defined
        //        oSegment.set_DataElementValue(6, 0, txtSenderID.Text.Trim());      //"SenderID"

        //        oSegment.set_DataElementValue(7, 0, "ZZ");                         //ZZ - Mutually Defined
        //        oSegment.set_DataElementValue(8, 0, txtReceiverID.Text.Trim());   //"ReceiverID"

        //        oSegment.set_DataElementValue(9, 0, "010821");
        //        oSegment.set_DataElementValue(10, 0, txtClaimTime.Text.Trim());  //Time at which claim prepared
        //        oSegment.set_DataElementValue(11, 0, "U");
        //        oSegment.set_DataElementValue(12, 0, "00401");//Version of 837
        //        oSegment.set_DataElementValue(13, 0, txtControlNo.Text.Trim());  //"ControlNo"
        //        oSegment.set_DataElementValue(14, 0, "0");
        //        oSegment.set_DataElementValue(15, 0, "T");
        //        oSegment.set_DataElementValue(16, 0, ":");

        //        #endregion

        //        #region GS - Functional Group Segment

        //        //CREATE FUNCTIONAL GROUP

        //        ediGroup.Set(ref oGroup, (ediGroup)oInterchange.CreateGroup("004010X098A1"));
        //        ediDataSegment.Set(ref oSegment, (ediDataSegment)oGroup.GetDataSegmentHeader());
        //        oSegment.set_DataElementValue(1, 0, txtFunctionID.Text.Trim());   //"FuncID"
        //        if (txtClaimDate.Text.Trim() != "")
        //        {
        //            oSegment.set_DataElementValue(2, 0, txtSenderDept.Text.Trim());   //"SenderDEPT"
        //        }
        //        else
        //        {
        //            MessageBox.Show("Sender department is not given", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            return;
        //        }
        //        if (txtReceiverDept.Text.Trim() != "")
        //        {
        //            oSegment.set_DataElementValue(3, 0, txtReceiverDept.Text.Trim()); //"ReceiverDept"
        //        }
        //        else
        //        {
        //            MessageBox.Show("Receiver department is not given", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            return;
        //        }
        //        if (txtClaimDate.Text.Trim() != "")
        //        {
        //            oSegment.set_DataElementValue(4, 0, txtClaimDate.Text.Trim());// Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString().Trim())));//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oCase.ClaimDate.ToShortDateString())));//Date of claim
        //        }
        //        else
        //        {
        //            MessageBox.Show("Claim Date is not given", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            return;
        //        }
        //        oSegment.set_DataElementValue(5, 0, txtClaimTime.Text.ToString().Trim());  //Time at which claim prepared
        //        if (txtControlNo.Text.Trim() != "")
        //        {
        //            oSegment.set_DataElementValue(6, 0, txtControlNo.Text.Trim());//"ControlNo"
        //        }
        //        else
        //        {
        //            MessageBox.Show("Functional group Control number is not given", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            return;
        //        }
        //        oSegment.set_DataElementValue(7, 0, "X");
        //        oSegment.set_DataElementValue(8, 0, "004010X098A1");//HIPPA Version of 837 EDI file

        //        #endregion

        //        if (oTransaction != null)
        //        {
        //            if (oTransaction.Lines.Count > 0)
        //            {
        //                for (int nTransactionSet = 1; nTransactionSet <= 1; nTransactionSet++)
        //                {

        //                    #region ST - TRANSACTION SET HEADER

        //                    ediTransactionSet.Set(ref oTransactionset, (ediTransactionSet)oGroup.CreateTransactionSet("837"));
        //                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.GetDataSegmentHeader());
        //                    if (txtTSControlNumber.Text.Trim() != "")
        //                    {
        //                    oSegment.set_DataElementValue(2, 0, txtTSControlNumber.Text.Trim());//"ControlNo"
        //                    }
        //                    else
        //                    {
        //                        MessageBox.Show("Transaction Set Control number is not given", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                        return;
        //                    }

        //                    #endregion ST - TRANSACTION SET HEADER

        //                    #region BHT - BEGINNING OF HIERARCHICAL TRANSACTION

        //                    //Begining Segment 
        //                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("BHT"));
        //                    oSegment.set_DataElementValue(1, 0, txtBHT_HerarchicalStrCode.Text.Trim()); //Herarchical Structure Code
        //                    oSegment.set_DataElementValue(2, 0, cmbBHT_TSPurposeCode.Text.Trim()); //Transaction Purpose Code
        //                    oSegment.set_DataElementValue(3, 0, txtBHTRefIdentification.Text.Trim()); //Reference identification
        //                    oSegment.set_DataElementValue(4, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())).Trim());//Date of claim
        //                    oSegment.set_DataElementValue(5, 0, Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToShortTimeString()))); //"1230");
        //                    oSegment.set_DataElementValue(6, 0, cmbBHT_TSTypeCode.Text.Trim()); //Transaction Type Code

        //                    #endregion BHT - BEGINNING OF HIERARCHICAL TRANSACTION

        //                    #region REF - TRANSMISSION TYPE IDENTIFICATION

        //                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("REF"));
        //                    oSegment.set_DataElementValue(1, 0, "87");
        //                    oSegment.set_DataElementValue(2, 0, "004010X098A1");//"ReferenceID"

        //                    #endregion REF - TRANSMISSION TYPE IDENTIFICATION

        //                    #region NM1 - SUBMITTER


        //                    //1000A SUBMITTER
        //                    //NM1 SUBMITTER
        //                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("NM1\\NM1"));
        //                    oSegment.set_DataElementValue(1, 0, txtSubEntityIDQualifier.Text.Trim());//"41");
        //                    oSegment.set_DataElementValue(2, 0, txtSubEntityTypeQualifier.Text.Trim());//"2");
        //                    oSegment.set_DataElementValue(3, 0, cmbClinic.Text.Trim());// clinic name
        //                    oSegment.set_DataElementValue(8, 0, cmbSubmitterIdentifierCodeQualifier.Text.Trim());//"46"); // Identification Code Qualifier 
        //                    if (txtSubIdentificationCode.Text.Trim() != "")
        //                    {
        //                        oSegment.set_DataElementValue(9, 0, txtSubIdentificationCode.Text.Trim());//clinic code or Electronic Transmitter Identification No.
        //                    }
        //                    else
        //                    {
        //                        MessageBox.Show("Submitter code is not given.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                        return;
        //                    }


        //                    //PER SUBMITTER EDI CONTACT INFORMATION
        //                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("NM1\\PER"));
        //                    oSegment.set_DataElementValue(1, 0, cmbContactFunctionCode.Text.Trim());//"IC");
        //                    oSegment.set_DataElementValue(2, 0, txtSubmitterContactName.Text.Trim());//Contact person name of clinic
        //                    oSegment.set_DataElementValue(3, 0, cmb_CommunicationQualifier.Text.Trim());//"TE");
        //                    oSegment.set_DataElementValue(4, 0, txtSubmitterPhone.Text.Trim().Replace("(", "").Replace(")", "").Replace("-", ""));//clinic phone
        //                    if (txtSubmitterExt.Text.Trim() != "")
        //                    {
        //                        oSegment.set_DataElementValue(5, 0, "EX");
        //                        oSegment.set_DataElementValue(6, 0, txtSubmitterExt.Text.Trim());//clinic extension
        //                    }

        //                    #endregion NM1 - SUBMITTER

        //                    #region NM1 - RECEIVER NAME

        //                    //1000B RECEIVER
        //                    //NM1 RECEIVER NAME
        //                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("NM1(2)\\NM1"));
        //                    oSegment.set_DataElementValue(1, 0, txtEntityIDQualRec.Text.Trim());//"40");
        //                    oSegment.set_DataElementValue(2, 0, txtRecEntityTypeQualifier.Text.Trim());//"2");
        //                    if (txtRecieverName.Text.Trim() != "")
        //                    {
        //                        oSegment.set_DataElementValue(3, 0, txtRecieverName.Text.Trim());//clearing house or contractor or carrier or FI name
        //                    }
        //                    else
        //                    {
        //                        MessageBox.Show("Receiver name is not given.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                        return;
        //                    }
        //                    oSegment.set_DataElementValue(8, 0, cmbRecIDCodeQual.Text.Trim());//"46");// Identification Code Qualifier
        //                    if (txtRecieverIdentificationCode.Text.Trim() != "")
        //                    {
        //                        oSegment.set_DataElementValue(9, 0, txtRecieverIdentificationCode.Text.Trim());//code of carrier/contractor/FI or Electronic Transmitter Identification No.
        //                    }
        //                    else
        //                    {
        //                        MessageBox.Show("Receiver code/ETIN is not given.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                        return;
        //                    }

        //                    #endregion NM1 - RECEIVER NAME
        //                    nHlCount = 0;

        //                    //**** BILLING/PAY-TO PROVIDER HIERARCHICAL LEVEL *******************************************
        //                    for (int nLine = 0; nLine <= oTransaction.Lines.Count - 1; nLine++)
        //                    {
        //                        oTransLine = new TransactionLine();
        //                        oTransLine = oTransaction.Lines[nLine];
        //                        nHlCount = nHlCount + 1;
        //                        nHlProvParent = nHlCount;

        //                        #region Billing Provider
        //                        //2000A BILLING/PAY-TO PROVIDER HL LOOP
        //                        //HL-BILLING PROVIDER

        //                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
        //                        oSegment.set_DataElementValue(1, 0, nHlCount.ToString().Trim());
        //                        oSegment.set_DataElementValue(3, 0, "20");
        //                        oSegment.set_DataElementValue(4, 0, "1");


        //                        //2010AA BILLING PROVIDER
        //                        //NM1 BILLING PROVIDER NAME
        //                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
        //                        oSegment.set_DataElementValue(1, 0, "85");
        //                        oSegment.set_DataElementValue(2, 0, "1");
        //                        string[] _billingProvider = cmbBillingProvider.Text.Trim().Split(' ');
        //                        if (_billingProvider.Length > 2)
        //                        {
        //                            oSegment.set_DataElementValue(3, 0,_billingProvider[2].Trim());//Billing provider name
        //                            oSegment.set_DataElementValue(4, 0, _billingProvider[0].Trim());
        //                            oSegment.set_DataElementValue(5, 0, _billingProvider[1].Trim());
        //                        }
        //                        else if (_billingProvider.Length > 1)
        //                        {
        //                            oSegment.set_DataElementValue(3, 0, _billingProvider[1].Trim());//Billing provider name
        //                            oSegment.set_DataElementValue(4, 0, _billingProvider[0].Trim());
        //                        }
        //                        else
        //                        {
        //                            oSegment.set_DataElementValue(3, 0, _billingProvider[0].Trim());
        //                        }

        //                        oSegment.set_DataElementValue(8, 0, cmbBLCodeQualifier.Text.Trim());//"XX");
        //                        if (txtBLProvNPI_ID.Text.Trim() != "")
        //                        {
        //                            oSegment.set_DataElementValue(9, 0, txtBLProvNPI_ID.Text.Trim());//Billing provider ID/NPI
        //                        }
        //                        else
        //                        {
        //                            MessageBox.Show("Billing Provider NPI is not given.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                            return;
        //                        }

        //                        //N3 BILLING PROVIDER ADDRESS
        //                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N3"));
        //                        oSegment.set_DataElementValue(1, 0, txtBLProviderAddress.Text.Trim());//Provider Address

        //                        //N4 BILLING PROVIDER LOCATION
        //                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N4"));
        //                        oSegment.set_DataElementValue(1, 0, txtBLProviderCity.Text.Trim().ToUpper());////Provider City
        //                        oSegment.set_DataElementValue(2, 0, txtBLProviderState.Text.Trim().ToUpper());//Provider state
        //                        oSegment.set_DataElementValue(3, 0, txtBLProviderZip.Text.Trim());//Provider ZIP

        //                        //REF 
        //                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\REF"));
        //                        oSegment.set_DataElementValue(1, 0, cmbREF_ReferenceIdCode.Text.Trim().Substring(0,2));//"0B");//Reference Identification Qualifier("0B" stands for-> State Licence No)
        //                        oSegment.set_DataElementValue(2, 0, txtREFIdentificationRefNo.Text.Trim());//"103906255");// Medicare Provider No, BlueCross Provider No, State licence No. etc.
        //                        #endregion

        //                        //'******************************************************************************************************
        //                        //'******* SUBSCRIBER HIERARCHICAL LEVEL ****************************************************************
        //                        //'******************************************************************************************************

        //                        //nHlCount = 0;
        //                        nHlCount = nHlCount + 1;
        //                        nHlSubscriberParent = nHlCount;

        //                        #region Subscriber HL Loop - 2000B
        //                        //2000B SUBSCRIBER HL LOOP
        //                        //HL-SUBSCRIBER
        //                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
        //                        oSegment.set_DataElementValue(1, 0, nHlCount.ToString().Trim());
        //                        oSegment.set_DataElementValue(2, 0, nHlProvParent.ToString().Trim());
        //                        oSegment.set_DataElementValue(3, 0, "22");

        //                        if (txtPatientRelationship.Text.Trim() != "")
        //                        {
        //                            if (txtPatientRelationship.Text.Trim() == "18")
        //                                oSegment.set_DataElementValue(4, 0, "0");
        //                            else
        //                                oSegment.set_DataElementValue(4, 0, "1");
        //                        }
        //                        else
        //                        {
        //                            MessageBox.Show("Patient relationship code is not given.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                            return;
        //                        }

        //                        //SBR SUBSCRIBER INFORMATION
        //                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\SBR"));
        //                        oSegment.set_DataElementValue(1, 0, "P");
        //                        oSegment.set_DataElementValue(2, 0, txtPatientRelationship.Text.Trim());
        //                        if (txtInsurencePolicy.Text.Trim() != "")
        //                        {
        //                            oSegment.set_DataElementValue(3, 0, txtInsurencePolicy.Text.Trim());///Policy no
        //                        }
        //                        else
        //                        {
        //                            MessageBox.Show("Policy Number is not given.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                            return;
        //                        }
        //                        oSegment.set_DataElementValue(9, 0, "HM");


        //                        //2010BA SUBSCRIBER
        //                        //NM1 SUBSCRIBER NAME

        //                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
        //                        oSegment.set_DataElementValue(1, 0, "IL");
        //                        oSegment.set_DataElementValue(2, 0, "1");
        //                        oSegment.set_DataElementValue(3, 0, txtPatientLName.Text.Trim());//"SubscriberLastOrgName"
        //                        oSegment.set_DataElementValue(4, 0, txtPatientFName.Text.Trim());//"SubscriberFirstname"
        //                        oSegment.set_DataElementValue(8, 0, "MI");
        //                        oSegment.set_DataElementValue(9, 0, txtInsurenceGroup.Text.Trim());//"SubscriberMemberID"

        //                        //N3 SUBSCRIBER ADDRESS
        //                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N3"));
        //                        oSegment.set_DataElementValue(1, 0, txtPatientAddress.Text.Trim());//"SubscriberAddress"

        //                        //N4 SUBSCRIBER CITY
        //                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N4"));
        //                        oSegment.set_DataElementValue(1, 0, txtPatientCity.Text.Trim());//"SubscriberCity"
        //                        oSegment.set_DataElementValue(2, 0, txtPatientState.Text.Trim());//"SubscrberState"
        //                        oSegment.set_DataElementValue(3, 0, txtPatientZip.Text.Trim());//"SubscriberZip"

        //                        //DMG SUBSCRIBER DEMOGRAPHIC INFORMATION
        //                        if (txtPatientDOB.Text.Trim() != "")
        //                        {
        //                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\DMG"));
        //                            oSegment.set_DataElementValue(1, 0, "D8");
        //                            oSegment.set_DataElementValue(2, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(txtPatientDOB.Text.Trim())));//"SubscriberDOB"
        //                            if (txtPatientGender.Text.Trim().ToUpper() == "OTHER" || txtPatientGender.Text.Trim() == "Other")
        //                            {
        //                                oSegment.set_DataElementValue(3, 0, "U");//"SubscriberGender"
        //                            }
        //                            else
        //                            {
        //                                oSegment.set_DataElementValue(3, 0, txtPatientGender.Text.Trim());//"SubscriberGender"
        //                            }
        //                        }
        //                        else
        //                        {
        //                            MessageBox.Show("Subscriber Date of Birth is not given.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                            return;
        //                        }
        //                        #endregion

        //                        #region Payer Information Loop 2010BB
        //                        //2010BB SUBSCRIBER/PAYER
        //                        //NM1 PAYER NAME
        //                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1(2)\\NM1"));
        //                        oSegment.set_DataElementValue(1, 0, "PR");
        //                        oSegment.set_DataElementValue(2, 0, "2");
        //                        if (txtPayerName.Text.Trim() != "")
        //                        {
        //                            oSegment.set_DataElementValue(3, 0, txtPayerName.Text.Trim());//"PayerLastOrgName"
        //                        }
        //                        else
        //                        {
        //                            MessageBox.Show("Payer Name is not given.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                            return;
        //                        }

        //                        if (txtPayerID.Text.Trim() != "")
        //                        {
        //                            oSegment.set_DataElementValue(8, 0, "PI");//This is the payer ID of Medicare for Michigan
        //                            oSegment.set_DataElementValue(9, 0, txtPayerID.Text.Trim());//dtInsurance.Rows[0]["sPayerID"].ToString());//PayerID
        //                        }
        //                        else
        //                        {
        //                            MessageBox.Show("Payer ID is not given.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                            return;
        //                        }

        //                        #endregion


        //                        //******* SUBSCRIBER CLAIM INFORMATION ***************************************************************
        //                        //TODO: Get Details in DATATABLE for the fields to be entered in EDI file.

        //                        #region Claim Details - Loop 2300

        //                        //2300 CLAIM
        //                        //CLM CLAIM LEVEL INFORMATION
        //                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\CLM"));
        //                        oSegment.set_DataElementValue(1, 0, "003422"); //Patient Account no         
        //                        oSegment.set_DataElementValue(2, 0, Convert.ToString(oTransLine.Total).Replace(".", "").Substring(0, Convert.ToInt32(Convert.ToString(oTransLine.Total).Length - 2)));// Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_TOTAL))); //Claim Amount
        //                        //oSegment.set_DataElementValue(5, 1, "11");
        //                        oSegment.set_DataElementValue(5, 1, "21"); //21 - Inpatient Hospital
        //                        //
        //                        oSegment.set_DataElementValue(5, 3, "1");
        //                        oSegment.set_DataElementValue(6, 0, "Y");
        //                        oSegment.set_DataElementValue(7, 0, "A");
        //                        oSegment.set_DataElementValue(8, 0, "Y");
        //                        oSegment.set_DataElementValue(9, 0, "Y");
        //                        oSegment.set_DataElementValue(10, 0, "C");

        //                        //////DTP DATE OF ONSET of current symptoms or illness
        //                        //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
        //                        //oSegment.set_DataElementValue(1, 0, "431");
        //                        //oSegment.set_DataElementValue(2, 0, "D8");
        //                        //oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpOnsetofCurrentSymptomsorillness.Value.ToShortDateString())).Trim());     //Claim Date

        //                        ////DTP DATE OF ONSET of similar symptoms or illness
        //                        //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
        //                        //oSegment.set_DataElementValue(1, 0, "438");
        //                        //oSegment.set_DataElementValue(2, 0, "D8");
        //                        //oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpOnsetoSimilarSyptomsorillness.Value.ToShortDateString())).Trim());
        //                        ////

        //                        ////DTP DATE OF Hospitalization (Admission) 
        //                        //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
        //                        //oSegment.set_DataElementValue(1, 0, "435");
        //                        //oSegment.set_DataElementValue(2, 0, "D8");
        //                        //oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpHospitalizationDate.Value.ToShortDateString())).Trim());     //Claim Date


        //                        ////DTP DATE OF Discharge 
        //                        //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
        //                        //oSegment.set_DataElementValue(1, 0, "096");
        //                        //oSegment.set_DataElementValue(2, 0, "D8");
        //                        //oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpHospitalDischargeDate.Value.ToShortDateString())).Trim());     //Claim Date
        //                        ////

        //                        ////DTP DATE OF (Intial Disability period last day worked)
        //                        //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
        //                        //oSegment.set_DataElementValue(1, 0, "297");
        //                        //oSegment.set_DataElementValue(2, 0, "D8");
        //                        //oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpLastDayWorked.Value.ToShortDateString())).Trim());     //Claim Date
        //                        ////

        //                        ////DTP DATE OF (Intial Disability period return to work)
        //                        //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
        //                        //oSegment.set_DataElementValue(1, 0, "296");
        //                        //oSegment.set_DataElementValue(2, 0, "D8");
        //                        //oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpReturnToWork.Value.ToShortDateString())).Trim());     //Claim Date
        //                        ////

        //                        //REF CLEARING HOUSE CLAIM NUMBER
        //                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\REF"));
        //                        oSegment.set_DataElementValue(1, 0, "D9");
        //                        if (txtClaimNo.Text.Trim() != "")
        //                        {
        //                            oSegment.set_DataElementValue(2, 0, txtClaimNo.Text.Trim()); //Claim No
        //                        }
        //                        else
        //                        {
        //                            MessageBox.Show("Claim number is not given.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                            return;
        //                        }
        //                        #region HI - Diagnosis
        //                        //HI HEALTH CARE DIAGNOSIS CODES
        //                        //for (int i = 0; i <= oCase.CaseDiagnosis.Count-1; i++)
        //                        //{
        //                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\HI"));
        //                        oSegment.set_DataElementValue(1, 1, "BK");
        //                        if (oTransLine.Dx1Code.ToString() != "")
        //                        {
        //                            oSegment.set_DataElementValue(1, 2, oTransLine.Dx1Code.ToString().Replace(".", ""));// Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_DX1_CODE)));  // "0340");
        //                        }
        //                        else
        //                        {
        //                            MessageBox.Show("Principle Diagnosis is not given.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                            return;
        //                        }
        //                        if (oTransLine.Dx2Code.ToString() != "")
        //                        {
        //                            oSegment.set_DataElementValue(2, 1, "BF");
        //                            oSegment.set_DataElementValue(2, 2, oTransLine.Dx2Code.ToString().Replace(".", ""));//Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_DX1_CODE))); // oCase.CaseDiagnosis[0].DiagnosisCode.ToString().Replace(".", "").Trim());
        //                        }
        //                        if (oTransLine.Dx3Code.ToString() != "")
        //                        {
        //                            oSegment.set_DataElementValue(3, 1, "BF");
        //                            oSegment.set_DataElementValue(3, 2, oTransLine.Dx3Code.ToString().Replace(".", ""));
        //                        }
        //                        if (oTransLine.Dx4Code.ToString() != "")
        //                        {
        //                            oSegment.set_DataElementValue(4, 1, "BF");
        //                            oSegment.set_DataElementValue(4, 2, oTransLine.Dx4Code.ToString().Replace(".", ""));
        //                        }
        //                        if (oTransLine.Dx5Code.ToString() != "")
        //                        {
        //                            oSegment.set_DataElementValue(5, 1, "BF");
        //                            oSegment.set_DataElementValue(5, 2, oTransLine.Dx5Code.ToString().Replace(".", ""));
        //                        }
        //                        if (oTransLine.Dx6Code.ToString() != "")
        //                        {
        //                            oSegment.set_DataElementValue(6, 1, "BF");
        //                            oSegment.set_DataElementValue(6, 2, oTransLine.Dx6Code.ToString().Replace(".", ""));
        //                        }
        //                        if (oTransLine.Dx7Code.ToString() != "")
        //                        {
        //                            oSegment.set_DataElementValue(7, 1, "BF");
        //                            oSegment.set_DataElementValue(7, 2, oTransLine.Dx7Code.ToString().Replace(".", ""));
        //                        }
        //                        if (oTransLine.Dx8Code.ToString() != "")
        //                        {
        //                            oSegment.set_DataElementValue(8, 1, "BF");
        //                            oSegment.set_DataElementValue(8, 2, oTransLine.Dx8Code.ToString().Replace(".", ""));
        //                        }
        //                        //} 
        //                        #endregion



        //                        #endregion

        //                        #region Referring Provider - 2310A


        //                        if (_ReferralLName != "" && _ReferralNPI != "")
        //                        {

        //                            //2310B Referring PROVIDER
        //                            //NM1 Referring PROVIDER NAME
        //                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\NM1"));
        //                            oSegment.set_DataElementValue(1, 0, "DN");
        //                            oSegment.set_DataElementValue(2, 0, "1");
        //                            oSegment.set_DataElementValue(3, 0, _ReferralLName.Trim()); //"ReferringLastname"
        //                            oSegment.set_DataElementValue(4, 0, _ReferralFName.Trim());//"ReferringFirstname"
        //                            oSegment.set_DataElementValue(5, 0, _ReferralMName.Trim());
        //                            oSegment.set_DataElementValue(8, 0, "XX");
        //                            oSegment.set_DataElementValue(9, 0, _ReferralNPI.Trim());//"NPI"

        //                            //PRV REFERRING PROVIDER INFORMATION
        //                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\PRV"));
        //                            oSegment.set_DataElementValue(1, 0, "RF");
        //                            oSegment.set_DataElementValue(2, 0, "ZZ");//Mutually Defined
        //                            oSegment.set_DataElementValue(3, 0, _ReferralTaxonomy.Trim());//Reference Identification

        //                            //REF
        //                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\REF"));
        //                            oSegment.set_DataElementValue(1, 0, "EI");//// Employer Identification Number
        //                            oSegment.set_DataElementValue(2, 0, _ReferralTaxID.Trim());//"1039255");// 


        //                        }
        //                        #endregion Referring Provider

        //                        #region Rendering Provider - 2310B

        //                        //for (int k = 0; k < oCase.CaseProcedures.Count - 1; k++)
        //                        //{
        //                        //2310B RENDERING PROVIDER
        //                        //NM1 RENDERING PROVIDER NAME
        //                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\NM1"));
        //                        oSegment.set_DataElementValue(1, 0, "82");
        //                        oSegment.set_DataElementValue(2, 0, "1");
        //                        FillProviderDetails(oTransaction.ProviderID, ProviderType.RenderingProvider);
        //                        oSegment.set_DataElementValue(3, 0, _RenderingLName);//Billing provider name
        //                        oSegment.set_DataElementValue(4, 0, _RenderingFName);
        //                        oSegment.set_DataElementValue(5, 0, _RenderingMName);
        //                        oSegment.set_DataElementValue(8, 0, "XX");
        //                        oSegment.set_DataElementValue(9, 0, _RenderingNPI.Trim());//oProviderDetails.NPI);//Billing provider ID/NPI


        //                        //PRV RENDERING PROVIDER INFORMATION
        //                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\PRV"));
        //                        oSegment.set_DataElementValue(1, 0, "PE");
        //                        oSegment.set_DataElementValue(2, 0, "ZZ");//Mutually Defined
        //                        if (_RenderingTaxonomy.Trim() != "")
        //                        {
        //                            oSegment.set_DataElementValue(3, 0, _RenderingTaxonomy.Trim());//Reference Identification
        //                        }
        //                        else
        //                        {
        //                            oSegment.set_DataElementValue(3, 0,"TX7824234");
        //                        }
        //                        //}
        //                        #endregion

        //                        #region Facility - 2310D

        //                        //2310D SERVICE LOCATION
        //                        //NM1 SERVICE FACILITY LOCATION

        //                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1(2)\\NM1"));
        //                        oSegment.set_DataElementValue(1, 0, "77");
        //                        oSegment.set_DataElementValue(2, 0, "2");
        //                        oSegment.set_DataElementValue(3, 0, txtFacilityCode.Text.Trim());//"FacilityName"
        //                        oSegment.set_DataElementValue(8, 0, "XX");//NPI code
        //                        oSegment.set_DataElementValue(9, 0, txtFacilityCode.Text.Trim());//NPI

        //                        //N3 SERVICE FACILITY ADDRESS
        //                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1(2)\\N3"));
        //                        oSegment.set_DataElementValue(1, 0, txtFacilityAddress.Text.Trim());//"FacilityAddr"

        //                        //N4 SERVICE FACILITY CITY/STATE/ZIP
        //                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1(2)\\N4"));
        //                        oSegment.set_DataElementValue(1, 0, txtFacilityCity.Text.ToUpper().Trim());//"FacilityCity"
        //                        oSegment.set_DataElementValue(2, 0, txtFacilityState.Text.ToUpper().Trim());//"FacilityState"
        //                        oSegment.set_DataElementValue(3, 0, txtFacilityZip.Text.Trim());//"FacilityZip"


        //                        #endregion

        //                        #region Subscriber Secondary Insurance - Loop 2320

        //                        //LOOP - 2320

        //                        if (IsSecondaryInsurance)
        //                        {

        //                            #region SBR - SUBSCRIBER INFORMATION for Secondary Information

        //                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR"));
        //                            //1.Payer Resposibilty Sequence No.
        //                            oSegment.set_DataElementValue(1, 0, "S"); //P - Primary

        //                            //2.Individual Relationship code
        //                            oSegment.set_DataElementValue(2, 0, "18"); // Hard coded(Individual Relationship code) 18 - Self

        //                            //3.Refrence identification
        //                            oSegment.set_DataElementValue(3, 0, "22145");///Policy no

        //                            //5.Insurance Type Code
        //                            oSegment.set_DataElementValue(5, 0, "C1"); // C1 - Commercial (Insurance Type Code)


        //                            //oSegment.set_DataElementValue(6, 0, "6"); // 6 - No Co-ordination of Benefit

        //                            ////8.Employment Status Code(Not Used)
        //                            //oSegment.set_DataElementValue(8, 0, "AC"); // Employment status (AC - Active)

        //                            //9.Claim Filing Indicator
        //                            oSegment.set_DataElementValue(9, 0, "CI"); //Commercial Insurance company

        //                            #endregion SBR - SUBSCRIBER INFORMATION for Secondary Information

        //                            #region CAS - Claim Adjustment

        //                            //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\CAS"));
        //                            //oSegment.set_DataElementValue(1, 0, "PI");//PR - Patient Responsibility
        //                            //oSegment.set_DataElementValue(2, 0, "96");
        //                            //oSegment.set_DataElementValue(3, 0, "300");

        //                            #endregion CAS - Claim Adjustment

        //                            #region AMT - Amount

        //                            //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\AMT"));
        //                            //oSegment.set_DataElementValue(1, 0, "D");
        //                            //oSegment.set_DataElementValue(2, 0, "0");

        //                            //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\AMT"));
        //                            //oSegment.set_DataElementValue(1, 0, "F2");
        //                            //oSegment.set_DataElementValue(2, 0, "100");

        //                            #endregion AMT - Amount

        //                            #region MOA - Medicare Outpatient Adjudication

        //                            //ediDataSegment.Set(ref oSegment,(ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\MOA"));
        //                            //oSegment.set_DataElementValue(1,0,"20");
        //                            //oSegment.set_DataElementValue(2,0,"300");
        //                            //oSegment.set_DataElementValue(3,0,"125");

        //                            #endregion

        //                            #region DMG  - Demographic

        //                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\DMG"));
        //                            oSegment.set_DataElementValue(1, 0, "D8");
        //                            oSegment.set_DataElementValue(2, 0, txtPatientDOB.Text.Trim());//"SubscriberDOB"
        //                            oSegment.set_DataElementValue(3, 0, txtPatientGender.Text.Trim());//"SubscriberGender"

        //                            #endregion DMG  - Demographic

        //                            #region OI - Other Insurance

        //                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\OI"));
        //                            oSegment.set_DataElementValue(3, 0, "Y");
        //                            oSegment.set_DataElementValue(4, 0, "C");
        //                            oSegment.set_DataElementValue(6, 0, "Y");

        //                            #endregion OI - Other Insurance

        //                            //2330A SUBSCRIBER
        //                            #region NM1 SUBSCRIBER NAME - 2330A

        //                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\NM1\\NM1"));
        //                            oSegment.set_DataElementValue(1, 0, "IL");
        //                            oSegment.set_DataElementValue(2, 0, "1");
        //                            oSegment.set_DataElementValue(3, 0, txtPatientLName.Text.Trim());//"SubscriberLastOrgName"
        //                            oSegment.set_DataElementValue(4, 0, txtPatientFName.Text.Trim());//"SubscriberFirstname"
        //                            oSegment.set_DataElementValue(8, 0, "MI");
        //                            oSegment.set_DataElementValue(9, 0, txtInsurenceGroup.Text.Trim());//"SubscriberMemberID"

        //                            //N3 SUBSCRIBER ADDRESS
        //                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\NM1\\N3"));
        //                            oSegment.set_DataElementValue(1, 0, txtPatientAddress.Text.Trim());//"SubscriberAddress"

        //                            //N4 SUBSCRIBER CITY
        //                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\NM1\\N4"));
        //                            oSegment.set_DataElementValue(1, 0, txtPatientCity.Text.Trim());//"SubscriberCity"
        //                            oSegment.set_DataElementValue(2, 0, txtPatientState.Text.Trim());//"SubscrberState"
        //                            oSegment.set_DataElementValue(3, 0, txtPatientZip.Text.Trim());//"SubscriberZip"

        //                            #endregion NM1 SUBSCRIBER NAME

        //                            #region Payer Information - 2330B

        //                            //2330B SUBSCRIBER/PAYER
        //                            //NM1 PAYER NAME
        //                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\NM1\\NM1"));
        //                            oSegment.set_DataElementValue(1, 0, "PR");
        //                            oSegment.set_DataElementValue(2, 0, "2");
        //                            //if("PayerLastOrgName"!="")      TODO:Include Later
        //                            //{
        //                            oSegment.set_DataElementValue(3, 0, "Workers Insurance Company");//dtInsurance.Rows[0]["sSubscriberName"].ToString().Trim());//"PayerLastOrgName"
        //                            //}

        //                            //if("PayerLastOrgName"!="")      TODO:Include Later
        //                            //{
        //                            oSegment.set_DataElementValue(8, 0, "PI");//This is the payer ID of Medicare for Michigan
        //                            oSegment.set_DataElementValue(9, 0, txtPayerID.Text.Trim());//dtInsurance.Rows[0]["sPayerID"].ToString());//PayerID
        //                            //}

        //                            #endregion Payer Information

        //                        }


        //                        #endregion Subscriber Secondary Insurance

        //                        #region Service Line
        //                        //******* SUBSCRIBER SERVICE LINE *************************************************************
        //                        //TODO: Get the datatable for service info to add fields of service in EDI file.
        //                        //2400 SERVICE LINE


        //                        iItemCount = 0;
        //                        iItemCount = 1;
        //                        sInstance = iItemCount.ToString().Trim();
        //                        //LX SERVICE LINE COUNTER
        //                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\LX"));
        //                        oSegment.set_DataElementValue(1, 0, iItemCount.ToString());

        //                        //SV1 PROFESSIONAL SERVICE
        //                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\SV1"));
        //                        oSegment.set_DataElementValue(1, 1, "HC");
        //                        oSegment.set_DataElementValue(1, 2, oTransLine.CPTCode.ToString().Replace(".", ""));//"ServiceID"
        //                        if (oTransLine.Mod1Code.ToString() != "")
        //                        {
        //                            oSegment.set_DataElementValue(1, 3, oTransLine.Mod1Code.ToString());//Modifier 1
        //                        }
        //                        if (oTransLine.Mod2Code.ToString() != "")
        //                        {
        //                            oSegment.set_DataElementValue(1, 4, oTransLine.Mod2Code.ToString());//Modifier 2
        //                        }
        //                        oSegment.set_DataElementValue(2, 0, Convert.ToString(oTransLine.Charges).Replace(".", "").Substring(0, Convert.ToInt32(Convert.ToString(oTransLine.Charges).Length - 2)));//"ServiceAmount"
        //                        oSegment.set_DataElementValue(3, 0, "UN");//UN stands for UNITS
        //                        oSegment.set_DataElementValue(4, 0, oTransLine.Unit.ToString());//Unit/Quantity
        //                        if (oTransLine.Dx1Ptr.ToString() == "True")
        //                        {
        //                            oSegment.set_DataElementValue(7, 1, "1");//"Diagnosis Pointer1" (Must Use)
        //                            if (oTransLine.Dx2Ptr.ToString() == "True")
        //                            {
        //                                oSegment.set_DataElementValue(7, 2, "2");//"Diagnosis Pointer2"
        //                            }
        //                            if (oTransLine.Dx3Ptr.ToString() == "True")
        //                            {
        //                                oSegment.set_DataElementValue(7, 3, "3");//"Diagnosis Pointer3"
        //                            }
        //                            if (oTransLine.Dx4Ptr.ToString() == "True")
        //                            {
        //                                oSegment.set_DataElementValue(7, 4, "4");//"Diagnosis Pointer4"
        //                            }
        //                        }
        //                        else if (oTransLine.Dx2Ptr.ToString() == "True")
        //                        {
        //                            oSegment.set_DataElementValue(7, 1, "2");//"Diagnosis Pointer1"
        //                        }
        //                        else if (oTransLine.Dx3Ptr.ToString() == "True")
        //                        {
        //                            oSegment.set_DataElementValue(7, 1, "3");//"Diagnosis Pointer1"
        //                        }
        //                        else if (oTransLine.Dx4Ptr.ToString() == "True")
        //                        {
        //                            oSegment.set_DataElementValue(7, 1, "4");//"Diagnosis Pointer1"
        //                        }



        //                        //oSegment.set_DataElementValue(9, 0, "N");
        //                        //DTP DATE - SERVICE DATE(S)
        //                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\DTP"));
        //                        oSegment.set_DataElementValue(1, 0, "472");
        //                        oSegment.set_DataElementValue(2, 0, "D8");
        //                        oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oTransLine.DateServiceFrom.ToShortDateString())));//Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_DATEFROM)));//"ServiceDate"



        //                        #endregion

        //                        #region "Dependent Loop"

        //                        ////*****************************************************************************************************
        //                        ////******* DEPENDENT HIERARCHICAL LEVEL ****************************************************************
        //                        ////*****************************************************************************************************
        //                        //TODO: Get the datatable for dependent info to add fields of service in EDI file.
        //                        //bool Dependent = true;

        //                        //if (Dependent)
        //                        //{
        //                        //    nHlCount = nHlCount + 1;

        //                        //    //2000B DEPENDENT HL LOOP
        //                        //    //HL-DEPENDENT
        //                        //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
        //                        //    oSegment.set_DataElementValue(1, 0, nHlCount.ToString());
        //                        //    oSegment.set_DataElementValue(2, 0, nHlSubscriberParent.ToString());
        //                        //    oSegment.set_DataElementValue(3, 0, "23");
        //                        //    oSegment.set_DataElementValue(4, 0, "0");


        //                        //    //PAT - PATIENT dependent INFORMATION
        //                        //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\PAT"));
        //                        //    oSegment.set_DataElementValue(1, 0, "19"); //01 - Spouse 19 - Child

        //                        //    //NM1 - INDIVIDUAL OR ORGANIZATIONAL NAME
        //                        //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1"));
        //                        //    oSegment.set_DataElementValue(1, 0, "QC");
        //                        //    oSegment.set_DataElementValue(2, 0, "1");
        //                        //    oSegment.set_DataElementValue(3, 0, oCase.PatientLastName.Trim());
        //                        //    oSegment.set_DataElementValue(4, 0, oCase.PatientFirstName.Trim());

        //                        //    //N3 - ADDRESS INFORMATION
        //                        //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N3"));
        //                        //    oSegment.set_DataElementValue(1, 0, "adress");
        //                        //    //N4 - GEOGRAPHIC LOCATION
        //                        //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N4"));
        //                        //    //if("City"!="" or null)
        //                        //    //{
        //                        //    oSegment.set_DataElementValue(1, 0, "City");
        //                        //    //}

        //                        //    //if("State"!="" or null)
        //                        //    //{
        //                        //    oSegment.set_DataElementValue(2, 0, "State");
        //                        //    //}

        //                        //    //if("Zip"!="" or null)
        //                        //    //{
        //                        //    oSegment.set_DataElementValue(3, 0, "48242");
        //                        //    //}


        //                        //    //DMG - DEMOGRAPHIC INFORMATION
        //                        //    //if("DOB"!="" or null)
        //                        //    //{
        //                        //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\DMG"));
        //                        //    oSegment.set_DataElementValue(1, 0, "D8");
        //                        //    oSegment.set_DataElementValue(2, 0,Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oCase.PatientDOB.ToShortDateString());
        //                        //    oSegment.set_DataElementValue(3, 0, oCase.PatientGender.Trim());
        //                        //    //}


        //                        //    //******* DEPENDENT CLAIM INFORMATION *************************************************************
        //                        //    //TODO: Get the datatable for Claim info to add fields of service in EDI file

        //                        //    //2300 CLAIM
        //                        //    //CLM CLAIM LEVEL INFORMATION
        //                        //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\CLM"));
        //                        //    oSegment.set_DataElementValue(1, 0, "PatientAccountNo");
        //                        //    oSegment.set_DataElementValue(2, 0, "ClaimAmount");
        //                        //    oSegment.set_DataElementValue(5, 1, "11");
        //                        //    oSegment.set_DataElementValue(5, 3, "1");
        //                        //    oSegment.set_DataElementValue(6, 0, "Y");
        //                        //    oSegment.set_DataElementValue(7, 0, "A");
        //                        //    oSegment.set_DataElementValue(8, 0, "Y");
        //                        //    oSegment.set_DataElementValue(9, 0, "Y");
        //                        //    oSegment.set_DataElementValue(10, 0, "C");

        //                        //    //DTP DATE OF ONSET
        //                        //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
        //                        //    oSegment.set_DataElementValue(1, 0, "431");
        //                        //    oSegment.set_DataElementValue(2, 0, "D8");
        //                        //    oSegment.set_DataElementValue(3, 0, "ClaimDate");

        //                        //    //REF CLEARING HOUSE CLAIM NUMBER
        //                        //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\REF"));
        //                        //    oSegment.set_DataElementValue(1, 0, "D9");
        //                        //    oSegment.set_DataElementValue(2, 0, "17312345600006351");

        //                        //    //HI HEALTH CARE DIAGNOSIS CODES
        //                        //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\HI"));
        //                        //    oSegment.set_DataElementValue(1, 1, "BK");
        //                        //    oSegment.set_DataElementValue(1, 2, "0340");
        //                        //    oSegment.set_DataElementValue(2, 1, "BF");
        //                        //    oSegment.set_DataElementValue(2, 2, "V7389");
        //                        //    //2310B RENDERING PROVIDER
        //                        //    //NM1 RENDERING PROVIDER NAME
        //                        //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\NM1"));
        //                        //    oSegment.set_DataElementValue(1, 0, "82");
        //                        //    oSegment.set_DataElementValue(2, 0, "1");
        //                        //    oSegment.set_DataElementValue(3, 1, "RenderingLastname");
        //                        //    oSegment.set_DataElementValue(4, 3, "RenderingFirstname");
        //                        //    oSegment.set_DataElementValue(8, 0, "34");
        //                        //    oSegment.set_DataElementValue(9, 0, "RenderingID");

        //                        //    //PRV RENDERING PROVIDER INFORMATION
        //                        //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\PRV"));
        //                        //    oSegment.set_DataElementValue(1, 0, "PE");
        //                        //    oSegment.set_DataElementValue(2, 0, "ZZ");
        //                        //    oSegment.set_DataElementValue(3, 1, "203BF0100Y");

        //                        //    //2310D SERVICE LOCATION
        //                        //    //NM1 SERVICE FACILITY LOCATION
        //                        //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1(2)\\NM1"));
        //                        //    oSegment.set_DataElementValue(1, 0, "77");
        //                        //    oSegment.set_DataElementValue(2, 0, "2");
        //                        //    oSegment.set_DataElementValue(3, 1, "FacilityName");
        //                        //    oSegment.set_DataElementValue(8, 0, "24");
        //                        //    oSegment.set_DataElementValue(9, 1, "FacilityID");

        //                        //    //N3 SERVICE FACILITY ADDRESS
        //                        //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1(2)\\N3"));
        //                        //    oSegment.set_DataElementValue(1, 0, "FacilityAddr");

        //                        //    //N4 SERVICE FACILITY CITY/STATE/ZIP
        //                        //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1(2)\\N4"));
        //                        //    oSegment.set_DataElementValue(1, 0, "FacilityCity");
        //                        //    oSegment.set_DataElementValue(2, 0, "FacilityState");
        //                        //    oSegment.set_DataElementValue(3, 1, "FacilityZip");

        //                        //    //******* SUBSCRIBER SERVICE LINE *************************************************************
        //                        //    //TODO: Get the datatable for service info to add fields of service in EDI file.
        //                        //    //2400 SERVICE LINE
        //                        //    iItemCount = 0;
        //                        //    iItemCount = iItemCount + 1;
        //                        //    sInstance = iItemCount.ToString().Trim();
        //                        //    //LX SERVICE LINE COUNTER
        //                        //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\LX"));
        //                        //    oSegment.set_DataElementValue(1, 0, iItemCount.ToString());

        //                        //    //SV1 PROFESSIONAL SERVICE
        //                        //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\SV1"));
        //                        //    oSegment.set_DataElementValue(1, 1, "HC");
        //                        //    oSegment.set_DataElementValue(1, 2, "ServiceID");
        //                        //    oSegment.set_DataElementValue(2, 0, "ServiceAmount");
        //                        //    oSegment.set_DataElementValue(3, 0, "UN");
        //                        //    oSegment.set_DataElementValue(4, 0, "1");
        //                        //    oSegment.set_DataElementValue(7, 0, "Diagnosis");
        //                        //    oSegment.set_DataElementValue(9, 0, "N");
        //                        //    //DTP DATE - SERVICE DATE(S)
        //                        //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\DTP"));
        //                        //    oSegment.set_DataElementValue(1, 0, "472");
        //                        //    oSegment.set_DataElementValue(2, 0, "D8");
        //                        //    oSegment.set_DataElementValue(3, 0, "ServiceDate");

        //                        //}//if(Dependent)

        //                        #endregion


        //                    }//Case Loop
        //                }//Transaction Loop
        //            }
        //        }
        //        //Save to a file
        //        SaveFileDialog oSave = new SaveFileDialog();
        //        oSave.Filter = "TEXT Files (*.txt)|*.txt|EDI Files (*.edi)|*.edi";
        //        if (oSave.ShowDialog() == DialogResult.OK)
        //        {
        //            oEdiDoc.Save(sPath + sEdiFile);
        //            System.IO.StreamReader oReader = new System.IO.StreamReader(sPath + sEdiFile);
        //            string strData;
        //            strData = oReader.ReadToEnd();
        //            oReader.Close();

        //            System.IO.StreamWriter oWriter = new System.IO.StreamWriter(oSave.FileName);
        //            oWriter.Write(strData);
        //            oWriter.Close();
        //            MessageBox.Show("File Created Successfully", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    #endregion " Generate EDI "
        //}

        #endregion " EDI Generation Method "

        #region " OLD BACKUP Generate EDI "

            ////string sSEFFile = "";
            ////string sEdiFile = "";
            ////string sPath = "";
            ////string sEntity = "";
            ////string sInstance = "";
            ////string _strSQL = "";
            ////DataTable dt;
            ////string _BillingProviderDetails = "";
            ////gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            ////gloBilling ogloBilling = new gloBilling(_databaseconnectionstring, "");
            ////bool IsSecondaryInsurance = false;
            ////Transaction oTransaction = new Transaction();

            ////try
            ////{

            ////    //oTransaction = _SelTransactions;
            ////    //TransactionLine oTransLine = null;
            ////    //Here Interchange Loop should come
            ////    sPath = AppDomain.CurrentDomain.BaseDirectory;
            ////    sSEFFile = "837_X098A1.SEF";     //ToDO :Give the file name at runtime, since it can change
            ////    sEdiFile = "837A1.x12";

            ////    ediDocument.Set(ref oEdiDoc, new ediDocument());

            ////    ediSchemas.Set(ref oSchemas, (ediSchemas)oEdiDoc.GetSchemas());
            ////    oSchemas.EnableStandardReference = false;

            ////    oEdiDoc.SegmentTerminator = "~\r\n";
            ////    oEdiDoc.ElementTerminator = "*";
            ////    oEdiDoc.CompositeTerminator = ":";

            ////    oEdiDoc.CursorType = DocumentCursorTypeConstants.Cursor_ForwardWrite;
            ////    oEdiDoc.set_Property(DocumentPropertyIDConstants.Property_DocumentBufferIO, 2000);

            ////    ediSchema.Set(ref oSchema, (ediSchema)oEdiDoc.LoadSchema("837_X098A1.SEF", 0));
            ////    System.IO.FileInfo ofile = new System.IO.FileInfo(sPath + sSEFFile);
            ////    if (ofile.Exists == false)
            ////    {
            ////        MessageBox.Show("SEF file is not present in the Base Directory", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            ////        return;
            ////    }
            ////    FillClearingHouseInfo();

            ////    #region " Interchange Segment "
            ////    //Create the interchange segment
            ////    ediInterchange.Set(ref oInterchange, (ediInterchange)oEdiDoc.CreateInterchange("X", "004010"));
            ////    ediDataSegment.Set(ref oSegment, (ediDataSegment)oInterchange.GetDataSegmentHeader());

            ////    oSegment.set_DataElementValue(1, 0, "00");
            ////    oSegment.set_DataElementValue(3, 0, "00");
            ////    oSegment.set_DataElementValue(5, 0, "ZZ");
            ////    oSegment.set_DataElementValue(6, 0, _SenderID.Trim());//"1234545");//
            ////    oSegment.set_DataElementValue(7, 0, "ZZ");
            ////    oSegment.set_DataElementValue(8, 0, _ReceiverID.Trim());//"V2EL");//
            ////    string ISA_Date = Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString()));
            ////    oSegment.set_DataElementValue(9, 0, ISA_Date.Substring(2));
            ////    string ISA_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString()));
            ////    if (ISA_Time.Length < 4 && ISA_Time.Length <= 3)
            ////    {
            ////        if (ISA_Time.Length == 2)
            ////        {
            ////            ISA_Time = "0" + ISA_Time;
            ////        }
            ////        ISA_Time = "0" + ISA_Time;
            ////    }
            ////    oSegment.set_DataElementValue(10, 0, ISA_Time.Trim());
            ////    oSegment.set_DataElementValue(11, 0, "U");
            ////    oSegment.set_DataElementValue(12, 0, "00401");
            ////    oSegment.set_DataElementValue(13, 0, "000000020");//
            ////    oSegment.set_DataElementValue(14, 0, "0");
            ////    oSegment.set_DataElementValue(15, 0, "T");
            ////    oSegment.set_DataElementValue(16, 0, ":");

            ////    #endregion " Interchange Segment "

            ////    #region " Functional Group "

            ////    //Create the functional group segment
            ////    ediGroup.Set(ref oGroup, (ediGroup)oInterchange.CreateGroup("004010X098A1"));
            ////    ediDataSegment.Set(ref oSegment, (ediDataSegment)oGroup.GetDataSegmentHeader());
            ////    oSegment.set_DataElementValue(1, 0, "HC");
            ////    oSegment.set_DataElementValue(2, 0, _SenderName);
            ////    oSegment.set_DataElementValue(3, 0, _ReceiverCode.Trim());//"ClarEDI");
            ////    oSegment.set_DataElementValue(4, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())));
            ////    string GS_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString()));
            ////    if (GS_Time.Length < 4 && GS_Time.Length <= 3)
            ////    {
            ////        if (GS_Time.Length == 2)
            ////        {
            ////            GS_Time = "0" + GS_Time;
            ////        }
            ////        GS_Time = "0" + GS_Time;
            ////    }
            ////    oSegment.set_DataElementValue(5, 0, GS_Time.Trim());
            ////    oSegment.set_DataElementValue(6, 0, "1");
            ////    oSegment.set_DataElementValue(7, 0, "X");
            ////    oSegment.set_DataElementValue(8, 0, "004010X098A1");

            ////    #endregion " Functional Group "

            ////    #region ST - TRANSACTION SET HEADER

            ////    ediTransactionSet.Set(ref oTransactionset, (ediTransactionSet)oGroup.CreateTransactionSet("837"));
            ////    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.GetDataSegmentHeader());
            ////    oSegment.set_DataElementValue(2, 0, "00021");//"ControlNo"

            ////    #endregion ST - TRANSACTION SET HEADER

            ////    #region BHT - BEGINNING OF HIERARCHICAL TRANSACTION

            ////    //Begining Segment 
            ////    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("BHT"));
            ////    oSegment.set_DataElementValue(1, 0, txtBHT_HerarchicalStrCode.Text.Trim()); //Herarchical Structure Code
            ////    oSegment.set_DataElementValue(2, 0, cmbBHT_TSPurposeCode.Text.Trim()); //Transaction Purpose Code
            ////    oSegment.set_DataElementValue(3, 0, txtBHTRefIdentification.Text.Trim()); //Reference identification
            ////    oSegment.set_DataElementValue(4, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())).Trim());//Date of claim
            ////    string BHT_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString()));
            ////    if (BHT_Time.Length < 4 && BHT_Time.Length <= 3)
            ////    {
            ////        if (BHT_Time.Length == 2)
            ////        {
            ////            BHT_Time = "0" + BHT_Time;
            ////        }
            ////        BHT_Time = "0" + BHT_Time;
            ////    }
            ////    oSegment.set_DataElementValue(5, 0, BHT_Time.Trim()); //"1230");
            ////    oSegment.set_DataElementValue(6, 0, cmbBHT_TSTypeCode.Text.Trim()); //Transaction Type Code

            ////    #endregion BHT - BEGINNING OF HIERARCHICAL TRANSACTION

            ////    #region REF - TRANSMISSION TYPE IDENTIFICATION

            ////    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("REF"));
            ////    oSegment.set_DataElementValue(1, 0, "87");
            ////    oSegment.set_DataElementValue(2, 0, "004010X098A1");//"ReferenceID"

            ////    #endregion REF - TRANSMISSION TYPE IDENTIFICATION

            ////    if (SelectedTransactions != null)
            ////    {
            ////        if (SelectedTransactions.Count > 0)
            ////        {
            ////            for (int i = 0; i < SelectedTransactions.Count; i++)
            ////            {
            ////                oTransaction = new Transaction();
            ////                TransactionLine oTransLine = null;
            ////                oTransaction = ogloBilling.GetTransactionDetails(Convert.ToInt64(SelectedTransactions[i]), _ClinicID);
            ////                if (oTransaction != null)
            ////                {
            ////                    if (oTransaction.Lines.Count > 0)
            ////                    {
            ////                        FillAllDetails();
            ////                        for (int nTransactionSet = 1; nTransactionSet <= 1; nTransactionSet++)
            ////                        {

            ////                            #region NM1 - SUBMITTER


            ////                            //1000A SUBMITTER
            ////                            //NM1 SUBMITTER
            ////                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("NM1\\NM1"));
            ////                            oSegment.set_DataElementValue(1, 0, "41");
            ////                            oSegment.set_DataElementValue(2, 0, "2");
            ////                            oSegment.set_DataElementValue(3, 0, _SubmitterName);//cmbClinic.Text.Trim());// clinic name
            ////                            oSegment.set_DataElementValue(8, 0, "46"); // Identification Code Qualifier 
            ////                            oSegment.set_DataElementValue(9, 0, _SubmitterETIN);//txtSubIdentificationCode.Text.Trim());//clinic code or Electronic Transmitter Identification No.


            ////                            //PER SUBMITTER EDI CONTACT INFORMATION
            ////                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("NM1\\PER"));
            ////                            oSegment.set_DataElementValue(1, 0, "IC");
            ////                            oSegment.set_DataElementValue(2, 0, _SubmitterContactPersonName);//txtSubmitterContactName.Text.Trim());//Contact person name of clinic
            ////                            oSegment.set_DataElementValue(3, 0, "TE");
            ////                            oSegment.set_DataElementValue(4, 0, _SubmitterContactPersonNo);//txtSubmitterPhone.Text.Trim().Replace("(", "").Replace(")", "").Replace("-", ""));//clinic phone


            ////                            #endregion NM1 - SUBMITTER

            ////                            #region NM1 - RECEIVER NAME

            ////                            //1000B RECEIVER
            ////                            //NM1 RECEIVER NAME
            ////                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("NM1(2)\\NM1"));
            ////                            oSegment.set_DataElementValue(1, 0, "40");
            ////                            oSegment.set_DataElementValue(2, 0, "2");
            ////                            oSegment.set_DataElementValue(3, 0, "ClarEDI");//clearing house or contractor or carrier or FI name
            ////                            oSegment.set_DataElementValue(8, 0, "46");// Identification Code Qualifier
            ////                            oSegment.set_DataElementValue(9, 0, "V2093");//code of carrier/contractor/FI or Electronic Transmitter Identification No.


            ////                            #endregion NM1 - RECEIVER NAME

            ////                            nHlCount = 0;

            ////                            //**** BILLING/PAY-TO PROVIDER HIERARCHICAL LEVEL *******************************************

            ////                            nHlCount = nHlCount + 1;
            ////                            nHlProvParent = nHlCount;

            ////                            #region Billing Provider
            ////                            //2000A BILLING/PAY-TO PROVIDER HL LOOP
            ////                            //HL-BILLING PROVIDER

            ////                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
            ////                            oSegment.set_DataElementValue(1, 0, nHlCount.ToString().Trim());
            ////                            oSegment.set_DataElementValue(3, 0, "20");
            ////                            oSegment.set_DataElementValue(4, 0, "1");


            ////                            //2010AA BILLING PROVIDER
            ////                            //NM1 BILLING PROVIDER NAME
            ////                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
            ////                            oSegment.set_DataElementValue(1, 0, "85");
            ////                            oSegment.set_DataElementValue(2, 0, "1");
            ////                            oSegment.set_DataElementValue(3, 0, _BillingLName);//Billing provider name
            ////                            oSegment.set_DataElementValue(4, 0, _BillingFName);
            ////                            oSegment.set_DataElementValue(5, 0, _BillingMName);

            ////                            oSegment.set_DataElementValue(8, 0, "XX");
            ////                            oSegment.set_DataElementValue(9, 0, _BillingNPI);//Billing provider ID/NPI


            ////                            //N3 BILLING PROVIDER ADDRESS
            ////                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N3"));
            ////                            oSegment.set_DataElementValue(1, 0, _BillingAddress);//Provider Address

            ////                            //N4 BILLING PROVIDER LOCATION
            ////                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N4"));
            ////                            oSegment.set_DataElementValue(1, 0, _BillingCity);////Provider City
            ////                            oSegment.set_DataElementValue(2, 0, _BillingState);//Provider state
            ////                            oSegment.set_DataElementValue(3, 0, _BillingZIP);//Provider ZIP

            ////                            //REF 
            ////                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\REF"));
            ////                            oSegment.set_DataElementValue(1, 0, "EI");//Reference Identification Qualifier("0B" stands for-> State Licence No)
            ////                            if (_BillingTaxID.Length > 9)
            ////                            {
            ////                                _BillingTaxID = _BillingTaxID.Substring(0, 9);
            ////                            }
            ////                            oSegment.set_DataElementValue(2, 0, _BillingTaxID);//"103906255");// Medicare Provider No, BlueCross Provider No, State licence No. etc.
            ////                            #endregion

            ////                            //'******************************************************************************************************
            ////                            //'******* SUBSCRIBER HIERARCHICAL LEVEL ****************************************************************
            ////                            //'******************************************************************************************************
            ////                            #region Subscriber

            ////                            #region Subscriber HL Loop - 2000B

            ////                            //nHlCount = 0;
            ////                            nHlCount = nHlCount + 1;
            ////                            nHlSubscriberParent = nHlCount;

            ////                            //2000B SUBSCRIBER HL LOOP
            ////                            //HL-SUBSCRIBER
            ////                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
            ////                            oSegment.set_DataElementValue(1, 0, nHlCount.ToString().Trim());
            ////                            oSegment.set_DataElementValue(2, 0, nHlProvParent.ToString().Trim());
            ////                            oSegment.set_DataElementValue(3, 0, "22");

            ////                            //_SubscriberRelationshipCode = "18";
            ////                            if (_SubscriberRelationshipCode == "18")
            ////                                oSegment.set_DataElementValue(4, 0, "0");
            ////                            else
            ////                                oSegment.set_DataElementValue(4, 0, "1");


            ////                            //SBR SUBSCRIBER INFORMATION
            ////                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\SBR"));
            ////                            oSegment.set_DataElementValue(1, 0, _SubscriberInsurancePST);//"P");
            ////                            oSegment.set_DataElementValue(2, 0, _SubscriberRelationshipCode);
            ////                            if (_SubscriberInsuranceBelongs == "")
            ////                            {
            ////                                _SubscriberInsuranceBelongs = "CI";
            ////                            }
            ////                            oSegment.set_DataElementValue(9, 0, _SubscriberInsuranceBelongs);//"HM");


            ////                            //2010BA SUBSCRIBER
            ////                            //NM1 SUBSCRIBER NAME
            ////                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
            ////                            oSegment.set_DataElementValue(1, 0, "IL");
            ////                            oSegment.set_DataElementValue(2, 0, "1");
            ////                            oSegment.set_DataElementValue(3, 0, _SubscriberLName.Trim());//"SubscriberLastOrgName"
            ////                            oSegment.set_DataElementValue(4, 0, _SubscriberFName.Trim());//"SubscriberFirstname"
            ////                            oSegment.set_DataElementValue(8, 0, "MI");
            ////                            oSegment.set_DataElementValue(9, 0, _SubscriberInsuranceID.Trim());//"Insurance Id"

            ////                            //N3 SUBSCRIBER ADDRESS
            ////                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N3"));
            ////                            oSegment.set_DataElementValue(1, 0, _SubscriberAddress.Trim());//"SubscriberAddress"

            ////                            //N4 SUBSCRIBER CITY
            ////                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N4"));
            ////                            oSegment.set_DataElementValue(1, 0, _SubscriberCity.Trim());//"SubscriberCity"
            ////                            oSegment.set_DataElementValue(2, 0, _SubmitterState.Trim());//"SubscrberState"
            ////                            oSegment.set_DataElementValue(3, 0, _SubmitterZIP.Trim());//"SubscriberZip"

            ////                            #endregion SubscriberHL Loop - 2000B

            ////                            if (_SubscriberRelationshipCode == "18")
            ////                            {
            ////                                //DMG SUBSCRIBER DEMOGRAPHIC INFORMATION
            ////                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\DMG"));
            ////                                oSegment.set_DataElementValue(1, 0, "D8");
            ////                                oSegment.set_DataElementValue(2, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(_SubscriberDOB)));//"SubscriberDOB"
            ////                                if (_SubscriberGender.Trim() != "")
            ////                                {

            ////                                    oSegment.set_DataElementValue(3, 0, _SubscriberGender.Trim().Substring(0, 1).ToUpper());//"SubscriberGender"
            ////                                    if (_SubscriberGender.ToUpper() == "OTHER")
            ////                                    {
            ////                                        _SubscriberGender = "U";
            ////                                    }
            ////                                }
            ////                                else
            ////                                {
            ////                                    oSegment.set_DataElementValue(3, 0, "M");
            ////                                }


            ////                                if (_PayerID == "")
            ////                                {
            ////                                    _PayerID = "77710";
            ////                                }
            ////                                #region Payer Information Loop 2010BB
            ////                                //2010BB SUBSCRIBER/PAYER
            ////                                //NM1 PAYER NAME
            ////                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1(2)\\NM1"));
            ////                                oSegment.set_DataElementValue(1, 0, "PR");
            ////                                oSegment.set_DataElementValue(2, 0, "2");
            ////                                oSegment.set_DataElementValue(3, 0, _PayerName);//"PayerLastOrgName"
            ////                                oSegment.set_DataElementValue(8, 0, "PI");//This is the payer ID of Medicare for Michigan
            ////                                oSegment.set_DataElementValue(9, 0, _PayerID);//dtInsurance.Rows[0]["sPayerID"].ToString());//PayerID

            ////                                ////N3 SUBSCRIBER ADDRESS
            ////                                //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1(2)\\N3"));
            ////                                //oSegment.set_DataElementValue(1, 0, "");//"InsuranceAddress"

            ////                                ////N4 SUBSCRIBER CITY
            ////                                //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1(2)\\N4"));
            ////                                //oSegment.set_DataElementValue(1, 0, "");//"InsuranceCity"
            ////                                //oSegment.set_DataElementValue(2, 0, "");//"InsuranceState"
            ////                                //oSegment.set_DataElementValue(3, 0, "");//"InsuranceZip"

            ////                                #endregion


            ////                                //******* SUBSCRIBER CLAIM INFORMATION ***************************************************************
            ////                                //TODO: Get Details in DATATABLE for the fields to be entered in EDI file.

            ////                                for (int nLine = 0; nLine <= oTransaction.Lines.Count - 1; nLine++)
            ////                                {
            ////                                    oTransLine = new TransactionLine();
            ////                                    oTransLine = oTransaction.Lines[nLine];

            ////                                    #region Claim Details - Loop 2300


            ////                                    //2300 CLAIM
            ////                                    //CLM CLAIM LEVEL INFORMATION
            ////                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\CLM"));
            ////                                    oSegment.set_DataElementValue(1, 0, _PatientAccountNo); //Patient Account no         
            ////                                    oSegment.set_DataElementValue(2, 0, Convert.ToString(oTransLine.Total).Replace(".", "").Substring(0, Convert.ToInt32(Convert.ToString(oTransLine.Total).Length - 2)));// Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_TOTAL))); //Claim Amount

            ////                                    oSegment.set_DataElementValue(5, 1, oTransaction.Lines[nLine].POSCode.Trim()); //21 - Inpatient Hospital

            ////                                    oSegment.set_DataElementValue(5, 3, "1");
            ////                                    oSegment.set_DataElementValue(6, 0, "Y");
            ////                                    oSegment.set_DataElementValue(7, 0, "A");
            ////                                    oSegment.set_DataElementValue(8, 0, "Y");
            ////                                    oSegment.set_DataElementValue(9, 0, "Y");
            ////                                    oSegment.set_DataElementValue(10, 0, "C");

            ////                                    string OnsetDate = "";
            ////                                    if (oTransaction.InjuryDate.ToString() != "" || oTransaction.OnsiteDate.ToString() != "" || oTransaction.AccidentDate.ToString() != "")
            ////                                    {
            ////                                        if (oTransaction.InjuryDate.ToString() != "" && oTransaction.InjuryDate > 0)
            ////                                        {
            ////                                            OnsetDate = Convert.ToString(oTransaction.InjuryDate);
            ////                                            ////DTP DATE OF ONSET OF CURRENT SYMPTOMS OR ILLNESS
            ////                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
            ////                                            oSegment.set_DataElementValue(1, 0, "431");
            ////                                            oSegment.set_DataElementValue(2, 0, "D8");
            ////                                            oSegment.set_DataElementValue(3, 0, OnsetDate);//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpOnsetofCurrentSymptomsorillness.Value.ToShortDateString())).Trim());     //Claim Date
            ////                                        }
            ////                                        else if (oTransaction.OnsiteDate.ToString() != "" && oTransaction.OnsiteDate > 0)
            ////                                        {
            ////                                            OnsetDate = Convert.ToString(oTransaction.OnsiteDate);
            ////                                            ////DTP DATE OF CURRENT INJURY
            ////                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
            ////                                            oSegment.set_DataElementValue(1, 0, "431");
            ////                                            oSegment.set_DataElementValue(2, 0, "D8");
            ////                                            oSegment.set_DataElementValue(3, 0, OnsetDate);//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpOnsetofCurrentSymptomsorillness.Value.ToShortDateString())).Trim());     //Claim Date
            ////                                        }
            ////                                        if (oTransaction.AccidentDate.ToString() != "" && oTransaction.AccidentDate > 0)
            ////                                        {
            ////                                            OnsetDate = Convert.ToString(oTransaction.AccidentDate);
            ////                                            ////DTP DATE OF ACCIDENT 
            ////                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
            ////                                            oSegment.set_DataElementValue(1, 0, "439");
            ////                                            oSegment.set_DataElementValue(2, 0, "D8");
            ////                                            oSegment.set_DataElementValue(3, 0, OnsetDate);//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpOnsetofCurrentSymptomsorillness.Value.ToShortDateString())).Trim());     //Claim Date
            ////                                        }
            ////                                    }

            ////                                    //DTP DATE OF ONSET of similar symptoms or illness
            ////                                    //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
            ////                                    //oSegment.set_DataElementValue(1, 0, "438");
            ////                                    //oSegment.set_DataElementValue(2, 0, "D8");
            ////                                    //oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpOnsetoSimilarSyptomsorillness.Value.ToShortDateString())).Trim());
            ////                                    //
            ////                                    if (oTransaction.Lines[nLine].POSCode.Trim() != "11")
            ////                                    {
            ////                                        if (oTransaction.HospitalizationDateFrom > 0 && oTransaction.HospitalizationDateFrom.ToString() != "")
            ////                                        {
            ////                                            //DTP DATE OF Hospitalization (Admission) 
            ////                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
            ////                                            oSegment.set_DataElementValue(1, 0, "435");
            ////                                            oSegment.set_DataElementValue(2, 0, "D8");
            ////                                            oSegment.set_DataElementValue(3, 0, oTransaction.HospitalizationDateFrom.ToString());//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpHospitalizationDate.Value.ToShortDateString())).Trim());     //Claim Date
            ////                                        }

            ////                                        if (oTransaction.HospitalizationDateTo > 0 && oTransaction.HospitalizationDateTo.ToString() != "")
            ////                                        {
            ////                                            ////DTP DATE OF Discharge 
            ////                                            //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
            ////                                            //oSegment.set_DataElementValue(1, 0, "096");
            ////                                            //oSegment.set_DataElementValue(2, 0, "D8");
            ////                                            //oSegment.set_DataElementValue(3, 0, oTransaction.HospitalizationDateTo.ToString());//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpHospitalDischargeDate.Value.ToShortDateString())).Trim());     //Claim Date
            ////                                            ////
            ////                                        }
            ////                                    }

            ////                                    if (oTransaction.UnableToWorkFromDate > 0 && oTransaction.UnableToWorkFromDate.ToString() != "")
            ////                                    {
            ////                                        //DTP DATE OF (Intial Disability period last day worked)
            ////                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
            ////                                        oSegment.set_DataElementValue(1, 0, "297");
            ////                                        oSegment.set_DataElementValue(2, 0, "D8");
            ////                                        oSegment.set_DataElementValue(3, 0, oTransaction.UnableToWorkFromDate.ToString());//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpLastDayWorked.Value.ToShortDateString())).Trim());     //Claim Date
            ////                                        //
            ////                                    }

            ////                                    if (oTransaction.UnableToWorkTillDate > 0 && oTransaction.UnableToWorkTillDate.ToString() != "")
            ////                                    {
            ////                                        //DTP DATE OF (Intial Disability period return to work)
            ////                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
            ////                                        oSegment.set_DataElementValue(1, 0, "296");
            ////                                        oSegment.set_DataElementValue(2, 0, "D8");
            ////                                        oSegment.set_DataElementValue(3, 0, oTransaction.UnableToWorkTillDate.ToString());//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpReturnToWork.Value.ToShortDateString())).Trim());     //Claim Date
            ////                                        //
            ////                                    }

            ////                                    if (_PriorAuthorizationNo.Trim() != "")
            ////                                    {
            ////                                        //REF CLEARING HOUSE CLAIM NUMBER
            ////                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\REF"));
            ////                                        oSegment.set_DataElementValue(1, 0, "G1");
            ////                                        oSegment.set_DataElementValue(2, 0, _PriorAuthorizationNo.Trim()); //Claim No
            ////                                    }

            ////                                    #region HI - Diagnosis
            ////                                    //HI HEALTH CARE DIAGNOSIS CODES

            ////                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\HI"));
            ////                                    oSegment.set_DataElementValue(1, 1, "BK");
            ////                                    if (oTransLine.Dx1Code.ToString() != "")
            ////                                    {
            ////                                        oSegment.set_DataElementValue(1, 2, oTransLine.Dx1Code.ToString().Replace(".", ""));// Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_DX1_CODE)));  // "0340");
            ////                                    }
            ////                                    else
            ////                                    {
            ////                                        MessageBox.Show("Principle Diagnosis is not given.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            ////                                        return;
            ////                                    }
            ////                                    if (oTransLine.Dx2Code.ToString() != "")
            ////                                    {
            ////                                        oSegment.set_DataElementValue(2, 1, "BF");
            ////                                        oSegment.set_DataElementValue(2, 2, oTransLine.Dx2Code.ToString().Replace(".", ""));//Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_DX1_CODE))); // oCase.CaseDiagnosis[0].DiagnosisCode.ToString().Replace(".", "").Trim());
            ////                                    }
            ////                                    if (oTransLine.Dx3Code.ToString() != "")
            ////                                    {
            ////                                        oSegment.set_DataElementValue(3, 1, "BF");
            ////                                        oSegment.set_DataElementValue(3, 2, oTransLine.Dx3Code.ToString().Replace(".", ""));
            ////                                    }
            ////                                    if (oTransLine.Dx4Code.ToString() != "")
            ////                                    {
            ////                                        oSegment.set_DataElementValue(4, 1, "BF");
            ////                                        oSegment.set_DataElementValue(4, 2, oTransLine.Dx4Code.ToString().Replace(".", ""));
            ////                                    }
            ////                                    if (oTransLine.Dx5Code.ToString() != "")
            ////                                    {
            ////                                        oSegment.set_DataElementValue(5, 1, "BF");
            ////                                        oSegment.set_DataElementValue(5, 2, oTransLine.Dx5Code.ToString().Replace(".", ""));
            ////                                    }
            ////                                    if (oTransLine.Dx6Code.ToString() != "")
            ////                                    {
            ////                                        oSegment.set_DataElementValue(6, 1, "BF");
            ////                                        oSegment.set_DataElementValue(6, 2, oTransLine.Dx6Code.ToString().Replace(".", ""));
            ////                                    }
            ////                                    if (oTransLine.Dx7Code.ToString() != "")
            ////                                    {
            ////                                        oSegment.set_DataElementValue(7, 1, "BF");
            ////                                        oSegment.set_DataElementValue(7, 2, oTransLine.Dx7Code.ToString().Replace(".", ""));
            ////                                    }
            ////                                    if (oTransLine.Dx8Code.ToString() != "")
            ////                                    {
            ////                                        oSegment.set_DataElementValue(8, 1, "BF");
            ////                                        oSegment.set_DataElementValue(8, 2, oTransLine.Dx8Code.ToString().Replace(".", ""));
            ////                                    }
            ////                                    //} 
            ////                                    #endregion



            ////                                    #endregion

            ////                                    #region Referring Provider - 2310A


            ////                                    if (_ReferralLName != "" && _ReferralNPI != "")
            ////                                    {

            ////                                        //2310B Referring PROVIDER
            ////                                        //NM1 Referring PROVIDER NAME
            ////                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\NM1"));
            ////                                        oSegment.set_DataElementValue(1, 0, "DN");
            ////                                        oSegment.set_DataElementValue(2, 0, "1");
            ////                                        oSegment.set_DataElementValue(3, 0, _ReferralLName.Trim()); //"ReferringLastname"
            ////                                        oSegment.set_DataElementValue(4, 0, _ReferralFName.Trim());//"ReferringFirstname"
            ////                                        oSegment.set_DataElementValue(5, 0, _ReferralMName.Trim());
            ////                                        oSegment.set_DataElementValue(8, 0, "XX");
            ////                                        oSegment.set_DataElementValue(9, 0, _ReferralNPI.Trim());//"NPI"

            ////                                        //PRV REFERRING PROVIDER INFORMATION
            ////                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\PRV"));
            ////                                        oSegment.set_DataElementValue(1, 0, "RF");
            ////                                        oSegment.set_DataElementValue(2, 0, "ZZ");//Mutually Defined
            ////                                        oSegment.set_DataElementValue(3, 0, _ReferralTaxonomy.Trim());//Reference Identification

            ////                                        //REF
            ////                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\REF"));
            ////                                        oSegment.set_DataElementValue(1, 0, "EI");//// Employer Identification Number
            ////                                        oSegment.set_DataElementValue(2, 0, _ReferralTaxID.Trim());//"1039255");// 


            ////                                    }
            ////                                    #endregion Referring Provider

            ////                                    #region Rendering Provider - 2310B

            ////                                    //2310B RENDERING PROVIDER
            ////                                    //NM1 RENDERING PROVIDER NAME
            ////                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\NM1"));
            ////                                    oSegment.set_DataElementValue(1, 0, "82");
            ////                                    oSegment.set_DataElementValue(2, 0, "1");
            ////                                    FillProviderDetails(oTransaction.ProviderID, ProviderType.RenderingProvider);
            ////                                    oSegment.set_DataElementValue(3, 0, _RenderingLName);//Billing provider name
            ////                                    oSegment.set_DataElementValue(4, 0, _RenderingFName);
            ////                                    oSegment.set_DataElementValue(5, 0, _RenderingMName);
            ////                                    oSegment.set_DataElementValue(8, 0, "XX");
            ////                                    oSegment.set_DataElementValue(9, 0, _RenderingNPI.Trim());//oProviderDetails.NPI);//Billing provider ID/NPI


            ////                                    //PRV RENDERING PROVIDER INFORMATION
            ////                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\PRV"));
            ////                                    oSegment.set_DataElementValue(1, 0, "PE");
            ////                                    oSegment.set_DataElementValue(2, 0, "ZZ");//Mutually Defined
            ////                                    oSegment.set_DataElementValue(3, 0, _RenderingTaxonomy.Trim());//Reference Identification


            ////                                    #endregion

            ////                                    #region Facility - 2310D

            ////                                    //2310D SERVICE LOCATION
            ////                                    //NM1 SERVICE FACILITY LOCATION

            ////                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1(2)\\NM1"));
            ////                                    oSegment.set_DataElementValue(1, 0, "77");
            ////                                    oSegment.set_DataElementValue(2, 0, "2");
            ////                                    oSegment.set_DataElementValue(3, 0, _FacilityName);//"FacilityName"
            ////                                    //oSegment.set_DataElementValue(8, 0, "XX");//NPI code
            ////                                    //oSegment.set_DataElementValue(9, 0, txtFacilityCode.Text.Trim());//NPI

            ////                                    //N3 SERVICE FACILITY ADDRESS
            ////                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1(2)\\N3"));
            ////                                    oSegment.set_DataElementValue(1, 0, _FacilityAddress);//"FacilityAddr"

            ////                                    //N4 SERVICE FACILITY CITY/STATE/ZIP
            ////                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1(2)\\N4"));
            ////                                    oSegment.set_DataElementValue(1, 0, _FacilityCity);//"FacilityCity"
            ////                                    oSegment.set_DataElementValue(2, 0, _FacilityState);//"FacilityState"
            ////                                    oSegment.set_DataElementValue(3, 0, _FacilityZip);//"FacilityZip"


            ////                                    #endregion

            ////                                    #region Subscriber Secondary Insurance - Loop 2320

            ////                                    //LOOP - 2320
            ////                                    if (bSecondaryInsurance)
            ////                                    {

            ////                                        #region SBR - SUBSCRIBER INFORMATION for Secondary Information

            ////                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR"));
            ////                                        //1.Payer Resposibilty Sequence No.
            ////                                        oSegment.set_DataElementValue(1, 0, _OtherInsurancePST.Trim()); //P - Primary

            ////                                        //2.Individual Relationship code
            ////                                        oSegment.set_DataElementValue(2, 0, _OtherInsuranceRelationshipCode.Trim());//"18"); // Hard coded(Individual Relationship code) 18 - Self

            ////                                        //3.Refrence identification
            ////                                        oSegment.set_DataElementValue(3, 0, _OtherInsuranceGroupID);//"22145");///Policy no

            ////                                        //5.Insurance Type Code
            ////                                        oSegment.set_DataElementValue(5, 0, "C1"); // C1 - Commercial (Insurance Type Code)


            ////                                        //oSegment.set_DataElementValue(6, 0, "6"); // 6 - No Co-ordination of Benefit

            ////                                        ////8.Employment Status Code(Not Used)
            ////                                        //oSegment.set_DataElementValue(8, 0, "AC"); // Employment status (AC - Active)

            ////                                        //9.Claim Filing Indicator
            ////                                        oSegment.set_DataElementValue(9, 0, _OtherInsuranceType.Trim()); //Commercial Insurance company

            ////                                        #endregion SBR - SUBSCRIBER INFORMATION for Secondary Information

            ////                                        #region CAS - Claim Adjustment

            ////                                        //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\CAS"));
            ////                                        //oSegment.set_DataElementValue(1, 0, "PI");//PR - Patient Responsibility
            ////                                        //oSegment.set_DataElementValue(2, 0, "96");
            ////                                        //oSegment.set_DataElementValue(3, 0, "300");

            ////                                        #endregion CAS - Claim Adjustment

            ////                                        #region AMT - Amount

            ////                                        //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\AMT"));
            ////                                        //oSegment.set_DataElementValue(1, 0, "D");
            ////                                        //oSegment.set_DataElementValue(2, 0, "0");

            ////                                        //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\AMT"));
            ////                                        //oSegment.set_DataElementValue(1, 0, "F2");
            ////                                        //oSegment.set_DataElementValue(2, 0, "100");

            ////                                        #endregion AMT - Amount

            ////                                        #region MOA - Medicare Outpatient Adjudication

            ////                                        //ediDataSegment.Set(ref oSegment,(ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\MOA"));
            ////                                        //oSegment.set_DataElementValue(1,0,"20");
            ////                                        //oSegment.set_DataElementValue(2,0,"300");
            ////                                        //oSegment.set_DataElementValue(3,0,"125");

            ////                                        #endregion

            ////                                        #region DMG  - Demographic

            ////                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\DMG"));
            ////                                        oSegment.set_DataElementValue(1, 0, "D8");
            ////                                        oSegment.set_DataElementValue(2, 0, gloDateMaster.gloDate.DateAsNumber(_OtherInsuranceSubscriberDOB).ToString());//"SubscriberDOB"
            ////                                        oSegment.set_DataElementValue(3, 0, _OtherInsuranceSubscriberGender);//"SubscriberGender"

            ////                                        #endregion DMG  - Demographic

            ////                                        #region OI - Other Insurance

            ////                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\OI"));
            ////                                        oSegment.set_DataElementValue(3, 0, "Y");
            ////                                        oSegment.set_DataElementValue(4, 0, "C");
            ////                                        oSegment.set_DataElementValue(6, 0, "Y");

            ////                                        #endregion OI - Other Insurance

            ////                                        //2330A SUBSCRIBER
            ////                                        #region NM1 SUBSCRIBER NAME - 2330A

            ////                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\NM1\\NM1"));
            ////                                        oSegment.set_DataElementValue(1, 0, "IL");
            ////                                        oSegment.set_DataElementValue(2, 0, "1");
            ////                                        oSegment.set_DataElementValue(3, 0, _OtherInsuranceSubscriberLName);//"SubscriberLastOrgName"
            ////                                        oSegment.set_DataElementValue(4, 0, _OtherInsuranceSubscriberFName);//"SubscriberFirstname"
            ////                                        oSegment.set_DataElementValue(8, 0, "MI");
            ////                                        oSegment.set_DataElementValue(9, 0, _OtherInsuranceID);//"SubscriberMemberID"

            ////                                        //N3 SUBSCRIBER ADDRESS
            ////                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\NM1\\N3"));
            ////                                        oSegment.set_DataElementValue(1, 0, _OtherInsuranceAddress);//"SubscriberAddress"

            ////                                        //N4 SUBSCRIBER CITY
            ////                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\NM1\\N4"));
            ////                                        oSegment.set_DataElementValue(1, 0, _OtherInsuranceCity);//"SubscriberCity"
            ////                                        oSegment.set_DataElementValue(2, 0, _OtherInsuranceState);//"SubscrberState"
            ////                                        oSegment.set_DataElementValue(3, 0, _OtherInsuranceZIP);//"SubscriberZip"

            ////                                        #endregion NM1 SUBSCRIBER NAME

            ////                                        #region Payer Information - 2330B

            ////                                        //2330B SUBSCRIBER/PAYER
            ////                                        //NM1 PAYER NAME
            ////                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\NM1\\NM1"));
            ////                                        oSegment.set_DataElementValue(1, 0, "PR");
            ////                                        oSegment.set_DataElementValue(2, 0, "2");

            ////                                        oSegment.set_DataElementValue(3, 0, _OtherInsuranceName);//dtInsurance.Rows[0]["sSubscriberName"].ToString().Trim());//"PayerLastOrgName"

            ////                                        oSegment.set_DataElementValue(8, 0, "PI");//This is the payer ID of Medicare for Michigan
            ////                                        oSegment.set_DataElementValue(9, 0, _OtherInsurancePayerID.Trim());//dtInsurance.Rows[0]["sPayerID"].ToString());//PayerID
            ////                                        //}

            ////                                        #endregion Payer Information

            ////                                    }


            ////                                    #endregion Subscriber Secondary Insurance

            ////                                    #region Service Line
            ////                                    //******* SUBSCRIBER SERVICE LINE *************************************************************
            ////                                    //TODO: Get the datatable for service info to add fields of service in EDI file.
            ////                                    //2400 SERVICE LINE


            ////                                    iItemCount = 0;
            ////                                    iItemCount = 1;
            ////                                    sInstance = iItemCount.ToString().Trim();
            ////                                    //LX SERVICE LINE COUNTER
            ////                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\LX"));
            ////                                    oSegment.set_DataElementValue(1, 0, iItemCount.ToString());

            ////                                    //SV1 PROFESSIONAL SERVICE
            ////                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\SV1"));
            ////                                    oSegment.set_DataElementValue(1, 1, "HC");
            ////                                    oSegment.set_DataElementValue(1, 2, oTransLine.CPTCode.ToString().Replace(".", ""));//"ServiceID"
            ////                                    if (oTransLine.Mod1Code.ToString() != "")
            ////                                    {
            ////                                        oSegment.set_DataElementValue(1, 3, oTransLine.Mod1Code.ToString());//Modifier 1
            ////                                    }
            ////                                    if (oTransLine.Mod2Code.ToString() != "")
            ////                                    {
            ////                                        oSegment.set_DataElementValue(1, 4, oTransLine.Mod2Code.ToString());//Modifier 2
            ////                                    }
            ////                                    oSegment.set_DataElementValue(2, 0, Convert.ToString(oTransLine.Charges).Replace(".", "").Substring(0, Convert.ToInt32(Convert.ToString(oTransLine.Charges).Length - 2)));//"ServiceAmount"
            ////                                    oSegment.set_DataElementValue(3, 0, "UN");//UN stands for UNITS
            ////                                    oSegment.set_DataElementValue(4, 0, oTransLine.Unit.ToString());//Unit/Quantity
            ////                                    if (oTransLine.Dx1Ptr.ToString() == "True")
            ////                                    {
            ////                                        oSegment.set_DataElementValue(7, 1, "1");//"Diagnosis Pointer1" (Must Use)
            ////                                        if (oTransLine.Dx2Ptr.ToString() == "True")
            ////                                        {
            ////                                            oSegment.set_DataElementValue(7, 2, "2");//"Diagnosis Pointer2"
            ////                                        }
            ////                                        if (oTransLine.Dx3Ptr.ToString() == "True")
            ////                                        {
            ////                                            oSegment.set_DataElementValue(7, 3, "3");//"Diagnosis Pointer3"
            ////                                        }
            ////                                        if (oTransLine.Dx4Ptr.ToString() == "True")
            ////                                        {
            ////                                            oSegment.set_DataElementValue(7, 4, "4");//"Diagnosis Pointer4"
            ////                                        }
            ////                                    }
            ////                                    else if (oTransLine.Dx2Ptr.ToString() == "True")
            ////                                    {
            ////                                        oSegment.set_DataElementValue(7, 1, "2");//"Diagnosis Pointer1"
            ////                                    }
            ////                                    else if (oTransLine.Dx3Ptr.ToString() == "True")
            ////                                    {
            ////                                        oSegment.set_DataElementValue(7, 1, "3");//"Diagnosis Pointer1"
            ////                                    }
            ////                                    else if (oTransLine.Dx4Ptr.ToString() == "True")
            ////                                    {
            ////                                        oSegment.set_DataElementValue(7, 1, "4");//"Diagnosis Pointer1"
            ////                                    }



            ////                                    //oSegment.set_DataElementValue(9, 0, "N");
            ////                                    //DTP DATE - SERVICE DATE(S)
            ////                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\DTP"));
            ////                                    oSegment.set_DataElementValue(1, 0, "472");
            ////                                    oSegment.set_DataElementValue(2, 0, "D8");
            ////                                    oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oTransLine.DateServiceFrom.ToShortDateString())));//Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_DATEFROM)));//"ServiceDate"



            ////                                    #endregion

            ////                                }
            ////                            #endregion " Subscriber "

            ////                            }//end of if loop for Subscriber as Patient
            ////                            else
            ////                            {
            ////                                #region "Dependent Loop"

            ////                                //////*****************************************************************************************************
            ////                                //////******* DEPENDENT HIERARCHICAL LEVEL ****************************************************************
            ////                                //////*****************************************************************************************************
            ////                                ////TODO: Get the datatable for dependent info to add fields of service in EDI file.
            ////                                if (_PayerID == "")
            ////                                {
            ////                                    _PayerID = "77710";
            ////                                }
            ////                                #region Payer Information Loop 2010BB
            ////                                //2010BB SUBSCRIBER/PAYER
            ////                                //NM1 PAYER NAME
            ////                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1(2)\\NM1"));
            ////                                oSegment.set_DataElementValue(1, 0, "PR");
            ////                                oSegment.set_DataElementValue(2, 0, "2");
            ////                                oSegment.set_DataElementValue(3, 0, _PayerName);//"PayerLastOrgName"
            ////                                oSegment.set_DataElementValue(8, 0, "PI");//This is the payer ID of Medicare for Michigan
            ////                                oSegment.set_DataElementValue(9, 0, _PayerID);//dtInsurance.Rows[0]["sPayerID"].ToString());//PayerID

            ////                                #endregion

            ////                                nHlCount = nHlCount + 1;

            ////                                //2000B DEPENDENT HL LOOP
            ////                                //HL-DEPENDENT
            ////                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
            ////                                oSegment.set_DataElementValue(1, 0, nHlCount.ToString());
            ////                                oSegment.set_DataElementValue(2, 0, nHlSubscriberParent.ToString());
            ////                                oSegment.set_DataElementValue(3, 0, "23");
            ////                                oSegment.set_DataElementValue(4, 0, "0");


            ////                                //PAT - PATIENT/DEPENDENT INFORMATION
            ////                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\PAT"));
            ////                                oSegment.set_DataElementValue(1, 0, _SubscriberRelationshipCode); //01 - Spouse 19 - Child

            ////                                #region " Patient Info"

            ////                                //NM1 - INDIVIDUAL OR ORGANIZATIONAL NAME
            ////                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1"));
            ////                                oSegment.set_DataElementValue(1, 0, "QC");
            ////                                oSegment.set_DataElementValue(2, 0, "1");
            ////                                oSegment.set_DataElementValue(3, 0, _PatientLastName.Trim());//Patient Last Name
            ////                                oSegment.set_DataElementValue(4, 0, _PatientFirstName.Trim());//Patient First Name

            ////                                //N3 - ADDRESS INFORMATION
            ////                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N3"));
            ////                                oSegment.set_DataElementValue(1, 0, _PatientAddress.Trim());//"Address"

            ////                                //N4 - GEOGRAPHIC LOCATION
            ////                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N4"));
            ////                                oSegment.set_DataElementValue(1, 0, _PatientCity.Trim());//"City"
            ////                                oSegment.set_DataElementValue(2, 0, _PatientState.Trim());//"State"
            ////                                oSegment.set_DataElementValue(3, 0, _PatientZip.Trim());//"Zip"

            ////                                //DMG - DEMOGRAPHIC INFORMATION
            ////                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\DMG"));
            ////                                oSegment.set_DataElementValue(1, 0, "D8");
            ////                                oSegment.set_DataElementValue(2, 0, _PatientDOB);
            ////                                oSegment.set_DataElementValue(3, 0, _PatientGender.Trim());

            ////                                #endregion " Patient Info"

            ////                                //******* DEPENDENT CLAIM INFORMATION *************************************************************
            ////                                //TODO: Get the datatable for Claim info to add fields of service in EDI file
            ////                                for (int nLine = 0; nLine <= oTransaction.Lines.Count - 1; nLine++)
            ////                                {
            ////                                    oTransLine = new TransactionLine();
            ////                                    oTransLine = oTransaction.Lines[nLine];

            ////                                    #region "Dependent Claim Level"
            ////                                    //2300 CLAIM
            ////                                    //CLM CLAIM LEVEL INFORMATION
            ////                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\CLM"));
            ////                                    oSegment.set_DataElementValue(1, 0, _PatientAccountNo); //Patient Account no         
            ////                                    oSegment.set_DataElementValue(2, 0, Convert.ToString(oTransLine.Total).Replace(".", "").Substring(0, Convert.ToInt32(Convert.ToString(oTransLine.Total).Length - 2)));// Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_TOTAL))); //Claim Amount
            ////                                    oSegment.set_DataElementValue(5, 1, oTransLine.POSCode.Trim()); //21 - Inpatient Hospital
            ////                                    oSegment.set_DataElementValue(5, 3, "1");
            ////                                    oSegment.set_DataElementValue(6, 0, "Y");
            ////                                    oSegment.set_DataElementValue(7, 0, "A");
            ////                                    oSegment.set_DataElementValue(8, 0, "Y");
            ////                                    oSegment.set_DataElementValue(9, 0, "Y");
            ////                                    oSegment.set_DataElementValue(10, 0, "C");

            ////                                    string OnsetDate = "";
            ////                                    if (oTransaction.InjuryDate.ToString() != "" || oTransaction.OnsiteDate.ToString() != "")
            ////                                    {
            ////                                        if (oTransaction.InjuryDate.ToString() != "" && oTransaction.InjuryDate > 0)
            ////                                        {
            ////                                            OnsetDate = Convert.ToString(oTransaction.InjuryDate);
            ////                                            ////DTP DATE OF ONSET of current symptoms or illness
            ////                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
            ////                                            oSegment.set_DataElementValue(1, 0, "431");
            ////                                            oSegment.set_DataElementValue(2, 0, "D8");
            ////                                            oSegment.set_DataElementValue(3, 0, OnsetDate);//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpOnsetofCurrentSymptomsorillness.Value.ToShortDateString())).Trim());     //Claim Date
            ////                                        }
            ////                                        else if (oTransaction.OnsiteDate.ToString() != "" && oTransaction.OnsiteDate > 0)
            ////                                        {
            ////                                            OnsetDate = Convert.ToString(oTransaction.OnsiteDate);
            ////                                            ////DTP DATE OF ONSET of current symptoms or illness
            ////                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
            ////                                            oSegment.set_DataElementValue(1, 0, "431");
            ////                                            oSegment.set_DataElementValue(2, 0, "D8");
            ////                                            oSegment.set_DataElementValue(3, 0, OnsetDate);//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpOnsetofCurrentSymptomsorillness.Value.ToShortDateString())).Trim());     //Claim Date
            ////                                        }

            ////                                        if (oTransaction.AccidentDate.ToString() != "" && oTransaction.AccidentDate > 0)
            ////                                        {
            ////                                            OnsetDate = Convert.ToString(oTransaction.AccidentDate);
            ////                                            ////DTP DATE OF ACCIDENT 
            ////                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
            ////                                            oSegment.set_DataElementValue(1, 0, "439");
            ////                                            oSegment.set_DataElementValue(2, 0, "D8");
            ////                                            oSegment.set_DataElementValue(3, 0, OnsetDate);//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpOnsetofCurrentSymptomsorillness.Value.ToShortDateString())).Trim());     //Claim Date
            ////                                        }
            ////                                    }

            ////                                    //DTP DATE OF ONSET of similar symptoms or illness
            ////                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
            ////                                    oSegment.set_DataElementValue(1, 0, "438");
            ////                                    oSegment.set_DataElementValue(2, 0, "D8");
            ////                                    oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpOnsetoSimilarSyptomsorillness.Value.ToShortDateString())).Trim());
            ////                                    //
            ////                                    if (oTransaction.Lines[nLine].POSCode.Trim() != "11")
            ////                                    {
            ////                                        if (oTransaction.HospitalizationDateFrom > 0 && oTransaction.HospitalizationDateFrom.ToString() != "")
            ////                                        {
            ////                                            //DTP DATE OF Hospitalization (Admission) 
            ////                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
            ////                                            oSegment.set_DataElementValue(1, 0, "435");
            ////                                            oSegment.set_DataElementValue(2, 0, "D8");
            ////                                            oSegment.set_DataElementValue(3, 0, oTransaction.HospitalizationDateFrom.ToString());//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpHospitalizationDate.Value.ToShortDateString())).Trim());     //Claim Date
            ////                                        }

            ////                                        if (oTransaction.HospitalizationDateTo > 0 && oTransaction.HospitalizationDateTo.ToString() != "")
            ////                                        {
            ////                                            ////DTP DATE OF Discharge 
            ////                                            //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
            ////                                            //oSegment.set_DataElementValue(1, 0, "096");
            ////                                            //oSegment.set_DataElementValue(2, 0, "D8");
            ////                                            //oSegment.set_DataElementValue(3, 0, oTransaction.HospitalizationDateTo.ToString());//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpHospitalDischargeDate.Value.ToShortDateString())).Trim());     //Claim Date
            ////                                            ////
            ////                                        }
            ////                                    }

            ////                                    if (oTransaction.UnableToWorkFromDate > 0 && oTransaction.UnableToWorkFromDate.ToString() != "")
            ////                                    {
            ////                                        //DTP DATE OF (Intial Disability period last day worked)
            ////                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
            ////                                        oSegment.set_DataElementValue(1, 0, "297");
            ////                                        oSegment.set_DataElementValue(2, 0, "D8");
            ////                                        oSegment.set_DataElementValue(3, 0, oTransaction.UnableToWorkFromDate.ToString());//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpLastDayWorked.Value.ToShortDateString())).Trim());     //Claim Date
            ////                                        //
            ////                                    }

            ////                                    if (oTransaction.UnableToWorkTillDate > 0 && oTransaction.UnableToWorkTillDate.ToString() != "")
            ////                                    {
            ////                                        //DTP DATE OF (Intial Disability period return to work)
            ////                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
            ////                                        oSegment.set_DataElementValue(1, 0, "296");
            ////                                        oSegment.set_DataElementValue(2, 0, "D8");
            ////                                        oSegment.set_DataElementValue(3, 0, oTransaction.UnableToWorkTillDate.ToString());//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpReturnToWork.Value.ToShortDateString())).Trim());     //Claim Date
            ////                                        //
            ////                                    }

            ////                                    //REF CLEARING HOUSE CLAIM NUMBER
            ////                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\REF"));
            ////                                    oSegment.set_DataElementValue(1, 0, "G1");
            ////                                    oSegment.set_DataElementValue(2, 0, _PriorAuthorizationNo.Trim()); //Claim No

            ////                                    #endregion "Dependent Claim Level"

            ////                                    #region Referring Provider -  for Dependent

            ////                                    //2310B RENDERING PROVIDER
            ////                                    //NM1 RENDERING PROVIDER NAME

            ////                                    if (_ReferralLName != "" && _ReferralNPI != "")
            ////                                    {

            ////                                        //2310B Referring PROVIDER
            ////                                        //NM1 Referring PROVIDER NAME
            ////                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\NM1"));
            ////                                        oSegment.set_DataElementValue(1, 0, "DN");
            ////                                        oSegment.set_DataElementValue(2, 0, "1");
            ////                                        oSegment.set_DataElementValue(3, 0, _ReferralLName.Trim()); //"ReferringLastname"
            ////                                        oSegment.set_DataElementValue(4, 0, _ReferralFName.Trim());//"ReferringFirstname"
            ////                                        oSegment.set_DataElementValue(5, 0, _ReferralMName.Trim());
            ////                                        oSegment.set_DataElementValue(8, 0, "XX");
            ////                                        oSegment.set_DataElementValue(9, 0, _ReferralNPI.Trim());//"NPI"

            ////                                        //PRV REFERRING PROVIDER INFORMATION
            ////                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\PRV"));
            ////                                        oSegment.set_DataElementValue(1, 0, "RF");
            ////                                        oSegment.set_DataElementValue(2, 0, "ZZ");//Mutually Defined
            ////                                        oSegment.set_DataElementValue(3, 0, _ReferralTaxonomy.Trim());//Reference Identification

            ////                                        //REF
            ////                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\REF"));
            ////                                        oSegment.set_DataElementValue(1, 0, "EI");//// Employer Identification Number
            ////                                        oSegment.set_DataElementValue(2, 0, _ReferralTaxID.Trim());//"1039255");// 


            ////                                    }
            ////                                    #endregion Referring Provider

            ////                                    #region Rendering Provider -  for Dependent

            ////                                    //2310B RENDERING PROVIDER
            ////                                    //NM1 RENDERING PROVIDER NAME
            ////                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\NM1"));
            ////                                    oSegment.set_DataElementValue(1, 0, "82");
            ////                                    oSegment.set_DataElementValue(2, 0, "1");
            ////                                    FillProviderDetails(oTransaction.ProviderID, ProviderType.RenderingProvider);
            ////                                    oSegment.set_DataElementValue(3, 0, _RenderingLName);//Billing provider name
            ////                                    oSegment.set_DataElementValue(4, 0, _RenderingFName);
            ////                                    oSegment.set_DataElementValue(5, 0, _RenderingMName);
            ////                                    oSegment.set_DataElementValue(8, 0, "XX");
            ////                                    oSegment.set_DataElementValue(9, 0, _RenderingNPI.Trim());//oProviderDetails.NPI);//Billing provider ID/NPI


            ////                                    //PRV RENDERING PROVIDER INFORMATION
            ////                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\PRV"));
            ////                                    oSegment.set_DataElementValue(1, 0, "PE");
            ////                                    oSegment.set_DataElementValue(2, 0, "ZZ");//Mutually Defined
            ////                                    oSegment.set_DataElementValue(3, 0, _RenderingTaxonomy.Trim());//Reference Identification


            ////                                    #endregion

            ////                                    #region Facility -  for Dependent

            ////                                    //2310D SERVICE LOCATION
            ////                                    //NM1 SERVICE FACILITY LOCATION

            ////                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1(2)\\NM1"));
            ////                                    oSegment.set_DataElementValue(1, 0, "77");
            ////                                    oSegment.set_DataElementValue(2, 0, "2");
            ////                                    oSegment.set_DataElementValue(3, 0, _FacilityName);//"FacilityName"
            ////                                    //oSegment.set_DataElementValue(8, 0, "XX");//NPI code
            ////                                    //oSegment.set_DataElementValue(9, 0, txtFacilityCode.Text.Trim());//NPI

            ////                                    //N3 SERVICE FACILITY ADDRESS
            ////                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1(2)\\N3"));
            ////                                    oSegment.set_DataElementValue(1, 0, _FacilityAddress);//"FacilityAddr"

            ////                                    //N4 SERVICE FACILITY CITY/STATE/ZIP
            ////                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1(2)\\N4"));
            ////                                    oSegment.set_DataElementValue(1, 0, _FacilityCity);//"FacilityCity"
            ////                                    oSegment.set_DataElementValue(2, 0, _FacilityState);//"FacilityState"
            ////                                    oSegment.set_DataElementValue(3, 0, _FacilityZip);//"FacilityZip"

            ////                                    #endregion

            ////                                    #region HI - Diagnosis for Dependent
            ////                                    //HI HEALTH CARE DIAGNOSIS CODES

            ////                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\HI"));
            ////                                    oSegment.set_DataElementValue(1, 1, "BK");
            ////                                    if (oTransLine.Dx1Code.ToString() != "")
            ////                                    {
            ////                                        oSegment.set_DataElementValue(1, 2, oTransLine.Dx1Code.ToString().Replace(".", ""));// Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_DX1_CODE)));  // "0340");
            ////                                    }
            ////                                    else
            ////                                    {
            ////                                        MessageBox.Show("Principle Diagnosis is not given.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            ////                                        return;
            ////                                    }
            ////                                    if (oTransLine.Dx2Code.ToString() != "")
            ////                                    {
            ////                                        oSegment.set_DataElementValue(2, 1, "BF");
            ////                                        oSegment.set_DataElementValue(2, 2, oTransLine.Dx2Code.ToString().Replace(".", ""));//Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_DX1_CODE))); // oCase.CaseDiagnosis[0].DiagnosisCode.ToString().Replace(".", "").Trim());
            ////                                    }
            ////                                    if (oTransLine.Dx3Code.ToString() != "")
            ////                                    {
            ////                                        oSegment.set_DataElementValue(3, 1, "BF");
            ////                                        oSegment.set_DataElementValue(3, 2, oTransLine.Dx3Code.ToString().Replace(".", ""));
            ////                                    }
            ////                                    if (oTransLine.Dx4Code.ToString() != "")
            ////                                    {
            ////                                        oSegment.set_DataElementValue(4, 1, "BF");
            ////                                        oSegment.set_DataElementValue(4, 2, oTransLine.Dx4Code.ToString().Replace(".", ""));
            ////                                    }
            ////                                    if (oTransLine.Dx5Code.ToString() != "")
            ////                                    {
            ////                                        oSegment.set_DataElementValue(5, 1, "BF");
            ////                                        oSegment.set_DataElementValue(5, 2, oTransLine.Dx5Code.ToString().Replace(".", ""));
            ////                                    }
            ////                                    if (oTransLine.Dx6Code.ToString() != "")
            ////                                    {
            ////                                        oSegment.set_DataElementValue(6, 1, "BF");
            ////                                        oSegment.set_DataElementValue(6, 2, oTransLine.Dx6Code.ToString().Replace(".", ""));
            ////                                    }
            ////                                    if (oTransLine.Dx7Code.ToString() != "")
            ////                                    {
            ////                                        oSegment.set_DataElementValue(7, 1, "BF");
            ////                                        oSegment.set_DataElementValue(7, 2, oTransLine.Dx7Code.ToString().Replace(".", ""));
            ////                                    }
            ////                                    if (oTransLine.Dx8Code.ToString() != "")
            ////                                    {
            ////                                        oSegment.set_DataElementValue(8, 1, "BF");
            ////                                        oSegment.set_DataElementValue(8, 2, oTransLine.Dx8Code.ToString().Replace(".", ""));
            ////                                    }
            ////                                    //} 
            ////                                    #endregion
                                                
            ////                                    #region " Service Line for Dependent "
            ////                                    //******* SUBSCRIBER SERVICE LINE *************************************************************
            ////                                    //TODO: Get the datatable for service info to add fields of service in EDI file.
            ////                                    //2400 SERVICE LINE


            ////                                    iItemCount = 0;
            ////                                    iItemCount = 1;
            ////                                    sInstance = iItemCount.ToString().Trim();
            ////                                    //LX SERVICE LINE COUNTER
            ////                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\LX"));
            ////                                    oSegment.set_DataElementValue(1, 0, iItemCount.ToString());

            ////                                    //SV1 PROFESSIONAL SERVICE
            ////                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\SV1"));
            ////                                    oSegment.set_DataElementValue(1, 1, "HC");
            ////                                    oSegment.set_DataElementValue(1, 2, oTransLine.CPTCode.ToString().Replace(".", ""));//"ServiceID"
            ////                                    if (oTransLine.Mod1Code.ToString() != "")
            ////                                    {
            ////                                        oSegment.set_DataElementValue(1, 3, oTransLine.Mod1Code.ToString());//Modifier 1
            ////                                    }
            ////                                    if (oTransLine.Mod2Code.ToString() != "")
            ////                                    {
            ////                                        oSegment.set_DataElementValue(1, 4, oTransLine.Mod2Code.ToString());//Modifier 2
            ////                                    }
            ////                                    oSegment.set_DataElementValue(2, 0, Convert.ToString(oTransLine.Charges).Replace(".", "").Substring(0, Convert.ToInt32(Convert.ToString(oTransLine.Charges).Length - 2)));//"ServiceAmount"
            ////                                    oSegment.set_DataElementValue(3, 0, "UN");//UN stands for UNITS
            ////                                    oSegment.set_DataElementValue(4, 0, oTransLine.Unit.ToString());//Unit/Quantity
            ////                                    if (oTransLine.Dx1Ptr.ToString() == "True")
            ////                                    {
            ////                                        oSegment.set_DataElementValue(7, 1, "1");//"Diagnosis Pointer1" (Must Use)
            ////                                        if (oTransLine.Dx2Ptr.ToString() == "True")
            ////                                        {
            ////                                            oSegment.set_DataElementValue(7, 2, "2");//"Diagnosis Pointer2"
            ////                                        }
            ////                                        if (oTransLine.Dx3Ptr.ToString() == "True")
            ////                                        {
            ////                                            oSegment.set_DataElementValue(7, 3, "3");//"Diagnosis Pointer3"
            ////                                        }
            ////                                        if (oTransLine.Dx4Ptr.ToString() == "True")
            ////                                        {
            ////                                            oSegment.set_DataElementValue(7, 4, "4");//"Diagnosis Pointer4"
            ////                                        }
            ////                                    }
            ////                                    else if (oTransLine.Dx2Ptr.ToString() == "True")
            ////                                    {
            ////                                        oSegment.set_DataElementValue(7, 1, "2");//"Diagnosis Pointer1"
            ////                                    }
            ////                                    else if (oTransLine.Dx3Ptr.ToString() == "True")
            ////                                    {
            ////                                        oSegment.set_DataElementValue(7, 1, "3");//"Diagnosis Pointer1"
            ////                                    }
            ////                                    else if (oTransLine.Dx4Ptr.ToString() == "True")
            ////                                    {
            ////                                        oSegment.set_DataElementValue(7, 1, "4");//"Diagnosis Pointer1"
            ////                                    }



            ////                                    //oSegment.set_DataElementValue(9, 0, "N");
            ////                                    //DTP DATE - SERVICE DATE(S)
            ////                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\DTP"));
            ////                                    oSegment.set_DataElementValue(1, 0, "472");
            ////                                    oSegment.set_DataElementValue(2, 0, "D8");
            ////                                    oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oTransLine.DateServiceFrom.ToShortDateString())));//Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_DATEFROM)));//"ServiceDate"



            ////                                    #endregion " Service Lines for Dependent"

            ////                                }
            ////                                #endregion " Dependent "
            ////                            }//end of else loop for dependent

            ////                            //Transaction Line Loop
            ////                        }//Transaction SETS Loop
            ////                    }
            ////                }
                            

            ////            }
            ////            //Save to a file
            ////            SaveFileDialog oSave = new SaveFileDialog();
            ////            oSave.Filter = "TEXT Files (*.txt)|*.txt|EDI Files (*.edi)|*.edi|X12 Files (*.X12)|*.X12";
            ////            if (oSave.ShowDialog() == DialogResult.OK)
            ////            {
            ////                oEdiDoc.Save(sPath + sEdiFile);
            ////                System.IO.StreamReader oReader = new System.IO.StreamReader(sPath + sEdiFile);
            ////                string strData;
            ////                strData = oReader.ReadToEnd();
            ////                oReader.Close();

            ////                System.IO.StreamWriter oWriter = new System.IO.StreamWriter(oSave.FileName);
            ////                oWriter.Write(strData);
            ////                oWriter.Close();
            ////                MessageBox.Show("File Created Successfully", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ////            }
            ////        }
            ////    }
            ////}
            ////catch (Exception ex)
            ////{
            ////    MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            ////}
            #endregion " Generate EDI "
    }
}
