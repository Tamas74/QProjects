using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using gloEmdeonInterface.Classes;
using gloEMRGeneralLibrary.gloEMRLab;
using System.Collections;
using CrystalDecisions.CrystalReports.Engine;///Added reference for crystal reports
using CrystalDecisions.Shared;
using gloEMRReports;
using gloEmdeonCommon;
using gloPatientPortalCommon;
using gloUserControlLibrary;


namespace gloEmdeonInterface.Forms
{

    interface IPatientContext
    {
        Int64 GetCurrentPatientID { get; }
    }

    
    public partial class frmViewgloLab : Form, IPatientContext,IHotKey 
    {
        #region Variables

        //Incident #55971: 00017037:Patient Context issue
        //Change patient provider from Lab not refresh provider combo box if open Dashboard as File>>Dashboard without closing lab main form.
        //Added delegate call when change provider is done.
        public delegate void ProviderChanged(Int64 newProviderID);
        public event ProviderChanged OnProviderChanged;

        public delegate void OpenMessages();
        public event OpenMessages EntOpenMessage;

        public delegate void OpenEducation(Int64 nTemplateID, Int64 nPatientID, string sTemplateName, gloEMRGeneralLibrary.clsInfobutton.enumSource oType, string OpenFor, string sResourceType);
        public event OpenEducation EntOpenEducation;
        //'Developer:Sanjog Dhamke
        //'Date: 21 Dec 2011
        //'PRD Name: Lab Usability (6060)
        //'Reason: To Open the Patient Letter & Referral Letter
        public delegate void OpenPatientLetter();
        public event OpenPatientLetter EvntOpenPatientLetter;

        //Developer:Mitesh Patel
        //Date:04 May 2012 
        //Problem # 100 
        public delegate void OpenReferralLetter();
        public event OpenReferralLetter EvntOpenReferralLetter;

        public delegate void GenerateCCDHandler(Int64 PatientID);   //added by kanchan on 20100601 for CCD
        public event GenerateCCDHandler EvntGenerateCCDHandler; //added by kanchan on 20100601 for CCD



        public delegate void GenerateCDAHandler(Int64 nOrderId, String sDetails, Int64 nContactID, Boolean DisablePreferredProvider, Int64 PatientID = 0);
        public event GenerateCDAHandler EvntGenerateCDAHandler;

        public delegate void OpenPlanOfTreatment(Int64 PatientID, string CallingForm, TreeNode oNode = null, string TestType = "", string SearchText = "");
        public event OpenPlanOfTreatment EvntOpenPlanOfTreatment;

        //  private static frmViewgloLab frm;
        public static Boolean IsOpen = false;

        private const Int16 COL_ORDERID = 0;
        private const Int16 COL_ORDERPREFIX = 1;
        private const Int16 COL_ORDERNO = 2;
        private const Int16 COL_TRANSDATE = 4;
        private const Int16 COL_PROVIDERNAME = 7; //Added by madan for providername. // by Abhijeet change col number 4 to 6 

        private const Int16 COL_CUSTOMORDERSTATUS = 5; // 29-May-13 Aniket: Orders PRD: Custom Order Status Column added in place of old Status

        // Added by Abhijeet on date 20100330 for showing order status
        private const Int16 COL_BILLINGTYPE = 6; // Added by Abhijeet on date 20100330  for showing billing type
        private const Int16 COL_ORDER_ISACKNOWLEDGED = 9;//Added by madan on 20100512
        private const Int16 COL_ORDER_HAS_RESULTS = 8;//Added by madan on 20100512
        private const Int16 COL_ISORDERLOCKED = 10;//Added by madan on 20100512   
        private const Int16 COL_MACHINENAME = 11;//Added by madan on 20100512 
        private const Int16 COL_REFEREANCEID = 3;//Added by madan on 20100831-- For Emdoen Order No.
        private const Int16 COL_HasResult_Value = 12;//Added By Sanjog To Track the Result
        private const Int16 COL_HasAkw_Value = 13;

        private const Int16 COL_ORDERSTATUS = 14; //29-May-13 Aniket: Orders PRD:  Old Order Status Column Moved Last
        private const Int16 COL_COUNT = 17; // 29-May-13 Aniket: Orders PRD: Column Count Increased to 15 to add Custom Order Status Column
        private const Int16 COL_ORDERING_PROVIDERID = 15;
        private const Int16 COL_ORDERCOMMENTS = 16; //20140407 manoj PRD: View Lab Order Comments : Added New column to Grid

        private long _ClinicID = 0;
        private string _dataBaseConnectionString = "";
        private long _patientID = 0;
        private long _LabProviderID = 0;
        private ContextMenu OContextMenu = null;
        private LabRequestOrderParameter _OrderParamter = new LabRequestOrderParameter();
        private String gstrMessageBoxCaption = string.Empty;
        gloEMRGeneralLibrary.gloEMRActors.LabActor.LabOrder oLabActor_Order1 = new gloEMRGeneralLibrary.gloEMRActors.LabActor.LabOrder();
        Boolean blnSaved = true;
        //   ArrayList _arrLabs = new ArrayList();
        gloUserControlLibrary.gloUC_PatientStrip gloUC_PatientStrip1 = new gloUserControlLibrary.gloUC_PatientStrip();

        private Int64 _PatientProviderID = 0;
        private Int64 _LoginUserID = 0;
        Int64 _LoginProviderId = 0;
        private string _OrderSelectID = "";
        private string _LoadOrderID = "";
        private string _DMSCategory_Labs = "";
        private bool IsClosed = false;
        private bool _blnClosed = false;
        private bool CallRefreshGrid = true;
        //Added for Bug #87928: CR00000369 : Lab orders coming back into glo are not matching up to the pending/sent order 
        private bool isSplitOrder = false;
        //View scan documents.-...Added by Madan.            
        gloEDocumentV3.gloEDocV3Management oViewDocument = new gloEDocumentV3.gloEDocV3Management();
        //Added this by madan for accessing patinet sdk-- on 20100419
        Classes.clsgloLabPatientLayer objClsgloLabPatientLayer = new gloEmdeonInterface.Classes.clsgloLabPatientLayer();
        gloPatient.Patient objpatient = new gloPatient.Patient();
        //     gloPatient.gloPatient objgloPatient;
        private bool _FormIsLoded = false;
        private Int64 _CurrentLockedOrder = 0;
        private bool _IsDemoLab = false;
        //End Madan   

        private DateTime LabFlowSheetFromDt = new DateTime();
        private DateTime LabFlowSheetToDt = new DateTime();

        public bool SelectOrderTab = false;
        private int OrderStatusRowToupdate = 0;
        private bool IsRowChange = false;
        private int ReadyForReview = 1004; // Orderstatus number for Ready for Result review. 
        gloUserControlLibrary.LabUC_ResultSet oUc_ResultSet;
        //changes added for split control functionality in orders&results
        internal Janus.Windows.UI.Dock.UIPanelGroup uiPanSplitScreen_LabOrder = null;
        private   gloEMRGeneralLibrary.clsSplitScreen _clsSplit_Laborder = null;
        private object _objCriteria=null;
        private object _objWord = null;
        private Int64 _VisitID = 0;
        private bool blnLoadJanusControl = true;

        private string LabResultSetFontName = "";
        private double LabResultSetFontSize = 0.0;

        //Int64 nProviderAssociationID = 0;
        //string sProviderTaxID = "";

        #endregion

        #region Constructor
        public gloEMRGeneralLibrary.clsSplitScreen clsSplit_Laborder
        {
            get
            {
                return _clsSplit_Laborder;
            }
            set
            {
                _clsSplit_Laborder = value; 
            }
        }

         public object  objCriteria
        {
            get
            {
                return _objCriteria;
            }
            set
            {
                _objCriteria = value; 
            }
        }

         public Int64 VisitID
         {
             get
             {
                 return _VisitID;
             }
             set
             {
                 _VisitID = value;
             }
         }
         public object  objWord
        {
            get
            {
                return _objWord;
            }
            set
            {
                _objWord= value; 
            }
        }
        
        public frmViewgloLab(long patientID)
        {


            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            _patientID = patientID;
            if (appSettings != null)
            {
                _dataBaseConnectionString = Convert.ToString(appSettings["DataBaseConnectionString"]);
                _ClinicID = Convert.ToInt64(appSettings["ClinicID"]);
                _LoginUserID = Convert.ToInt64(appSettings["UserID"]);
            }


            //Added by madan
            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    gstrMessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    gstrMessageBoxCaption = "gloEMR";
                }
            }
            else
            { gstrMessageBoxCaption = "gloEMR"; }

            #endregion



            Set_PatientDetailStrip();
        }
        //Added by madan-- on 20100401--
        public frmViewgloLab(long orderID, long patientID)
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            _OrderSelectID = Convert.ToString(orderID);
            _LoadOrderID = _OrderSelectID; // by Abhijeet on date 20100507 to maintain order id for which form load.
            _patientID = patientID;
            if (appSettings != null)
            {
                _dataBaseConnectionString = Convert.ToString(appSettings["DataBaseConnectionString"]);
                _ClinicID = Convert.ToInt64(appSettings["ClinicID"]);
                _LoginUserID = Convert.ToInt64(appSettings["UserID"]);
            }

            //Added by madan
            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    gstrMessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    gstrMessageBoxCaption = "gloEMR";
                }
            }
            else
            { gstrMessageBoxCaption = "gloEMR"; }

            #endregion

            //Code Start-Added by kanchan on 20101227 for Performance
            Set_PatientDetailStrip();
            //Code End-Added by kanchan on 20101227 for Performance
        }
        //End Madan... 

        public void LoadLabForTask(long orderID, long patientID)
        {
          
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            _OrderSelectID = Convert.ToString(orderID);
            _LoadOrderID = _OrderSelectID; // by Abhijeet on date 20100507 to maintain order id for which form load.
            if (_patientID == patientID)
            {
                blnLoadJanusControl = false;
            }
            else
            {
                blnLoadJanusControl = true ;
            }
            _patientID = patientID;
            if (appSettings != null)
            {
                _dataBaseConnectionString = Convert.ToString(appSettings["DataBaseConnectionString"]);
                _ClinicID = Convert.ToInt64(appSettings["ClinicID"]);
                _LoginUserID = Convert.ToInt64(appSettings["UserID"]);
            }

            //Added by madan
            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    gstrMessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    gstrMessageBoxCaption = "gloEMR";
                }
            }
            else
            { gstrMessageBoxCaption = "gloEMR"; }

            #endregion

            //Code Start-Added by kanchan on 20101227 for Performance
            gloUC_PatientStrip1.Dock = DockStyle.Top;
            gloUC_PatientStrip1.DTPValue = LabOrderParameter.TransactionDate;
            gloUC_PatientStrip1.ShowDetail(_patientID, gloUserControlLibrary.gloUC_PatientStrip.enumFormName.LabOrder, 0, LabOrderParameter.VisitID, LabOrderParameter.ProviderID, false, false, false, "", false);
               
            //Code End-Added by kanchan on 20101227 for Performance
            this.Text = "View Orders and Results";

            frmViewgloLab_Load(this, null);
        }


        #endregion

        #region Properties
        public LabRequestOrderParameter LabOrderParameter
        {
            get
            {
                return _OrderParamter;
            }
            set
            {

                _OrderParamter = value;
            }
        }
        public object objgloLabPatientExam { get; set; }
        public object objgloLabPatientLetters { get; set; }
        public object objgloLabPatientMessages { get; set; }
        public object objgloLabNurseNotes { get; set; }
        public object objgloLabHistory { get; set; }
        public object objgloLabLabs { get; set; }
        public object objgloLabDMS { get; set; }
        public object objgloLabRxmed { get; set; }
        public object objgloLabOrders { get; set; }
        public object objgloLabProblemList { get; set; }
        public object objgloLabCriteria { get; set; }
        public object objgloLabWord { get; set; }
        public Int64 LoginProviderID { get; set; }
        public string DirectAddress { get; set; }
        public bool SecureMsgEnable { get; set; }
        public bool SecureMsgUserright { get; set; }
        #endregion

        #region "Call Generate CCDA from Dashboard"
        public event EventCDA_GloLab EventCDA;
        public delegate void EventCDA_GloLab(Int64 PatientID);

        public delegate void OpenClinicalChart(Int64 PatientID);
        public event OpenClinicalChart EvntOpenClinicalChart;

        protected virtual void Raise_EvntGenerateCDA_GloLab(Int64 PatientID)
        {
            if (EventCDA != null)
            {
                EventCDA(PatientID);
            }

        }

        //'Reason: To Open the clinical chart
        public void OpenClinicalCharts(Int64 PatientID)
        {
            if (EvntOpenClinicalChart != null)
            {
                EvntOpenClinicalChart(PatientID);
            }

        }
        #endregion

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        private void fillOpenOrdsGrid_New()
        {
            DataTable dt = null;
            Int32 rowsCountToDisplay = 0;
            Int32 iRow1 = 0;
            //Int32 ordRowNo = 0;
            c1TestLibrary.BeginUpdate();
            try
            {
                DesignGrid();

                dt = GetOrder_New(_patientID);

                //if (_OrderSelectID != "")// when form called with order id constructor
                //{
                //    if (dt.Rows.Count <= 100)
                //    {
                //        iRow1 = 0;
                //        rowsCountToDisplay = dt.Rows.Count;
                //    }
                //    else
                //    {
                //        for (int cnt = 0; cnt < dt.Rows.Count; cnt++)
                //        {
                //            if (dt.Rows[cnt]["labom_OrderID"].ToString() == _OrderSelectID)
                //            {
                //                ordRowNo = cnt;
                //                break;
                //            }
                //        }

                //        if (dt.Rows.Count > (ordRowNo + 100))
                //        {
                //            iRow1 = ordRowNo;
                //            rowsCountToDisplay = ordRowNo + 100;
                //        }
                //        else
                //        {
                //            iRow1 = dt.Rows.Count - 100;
                //            rowsCountToDisplay = dt.Rows.Count;
                //        }
                //    }
                //}
                //else // when form called without order id constructor
                //{

                iRow1 = 0;

                //if (dt.Rows.Count < 100)
                if (dt != null)
                {
                    rowsCountToDisplay = dt.Rows.Count;
                    //else
                    //    rowsCountToDisplay = 100;
                    //}


                    if (dt.Rows.Count > 0)
                    {
                        for (int iRow = iRow1; iRow <= rowsCountToDisplay - 1; iRow++)
                        {

                            c1TestLibrary.Rows.Add();
                            Int32 _Row = c1TestLibrary.Rows.Count - 1;

                            c1TestLibrary.SetData(_Row, COL_ORDERID, dt.Rows[iRow]["labom_OrderID"].ToString());
                            c1TestLibrary.SetData(_Row, COL_ORDERPREFIX, dt.Rows[iRow]["PrfixID"].ToString());
                            c1TestLibrary.SetData(_Row, COL_ORDERNO, dt.Rows[iRow]["labom_OrderNoID"].ToString());
                            c1TestLibrary.SetData(_Row, COL_TRANSDATE, dt.Rows[iRow]["labom_OrderDate"].ToString());
                            c1TestLibrary.SetData(_Row, COL_ORDERING_PROVIDERID, dt.Rows[iRow]["labom_ProviderID"].ToString());

                            c1TestLibrary.SetData(_Row, COL_PROVIDERNAME, dt.Rows[iRow]["ProviderName"].ToString());


                            string _strOrderStatus = Convert.ToString(dt.Rows[iRow]["labom_gloLabOrderStatus1"]);
                            string _strBillingType = Convert.ToString(dt.Rows[iRow]["labom_gloLabOrderBillingType1"]);

                            c1TestLibrary.SetData(_Row, COL_CUSTOMORDERSTATUS, dt.Rows[iRow]["OrderStatus"]); //29-May-13 Aniket: Orders PRD: Order Status Column added
                            c1TestLibrary.SetData(_Row, COL_ORDERCOMMENTS, Convert.ToString(dt.Rows[iRow]["labom_LabComment"]));  //20140704 manoj PRD: View Lab Order Comments : Added New column 

                            c1TestLibrary.SetData(_Row, COL_ORDERSTATUS, _strOrderStatus);
                            c1TestLibrary.SetData(_Row, COL_BILLINGTYPE, _strBillingType);

                            //Madan added on 20100512
                            #region ContainsAcknowledgement

                            Int64 _nOrderID = 0;
                            string _OrderNumberPreFix = string.Empty;

                            //added by madan on 20100619
                            string _MachineName = string.Empty;
                            //end madan

                            _nOrderID = Convert.ToInt64(dt.Rows[iRow]["labom_OrderID"].ToString());
                            _MachineName = Convert.ToString(dt.Rows[iRow]["labom_MachineName"].ToString());

                            if (Convert.ToInt64(dt.Rows[iRow]["IsAkw"]) > 0 && Convert.ToInt64(dt.Rows[iRow]["bIsClosed"]) > 0)
                            {
                                c1TestLibrary.SetData(_Row, COL_HasAkw_Value, Convert.ToInt64(dt.Rows[iRow]["IsAkw"]));
                            }
                            else
                            {
                                c1TestLibrary.SetData(_Row, COL_HasAkw_Value, 0);
                            }

                            if (Convert.ToInt64(dt.Rows[iRow]["IsAkw"]) > 0 && Convert.ToInt64(dt.Rows[iRow]["bIsClosed"]) > 0)//CheckAcknoledgement(_nOrderID, _OrderNumberPreFix, _nOrderNumberID))
                            {
                                c1TestLibrary.SetCellImage(_Row, COL_ORDER_ISACKNOWLEDGED, gloEmdeonInterface.Properties.Resources.Yes);
                            }
                            else
                            {
                                c1TestLibrary.SetCellImage(_Row, COL_ORDER_ISACKNOWLEDGED, gloEmdeonInterface.Properties.Resources.FlagNone);
                            }

                            if (_nOrderID != 0)
                            {
                                c1TestLibrary.SetData(_Row, COL_HasResult_Value, Convert.ToInt64(dt.Rows[iRow]["IsResult"]));
                                if (Convert.ToInt64(dt.Rows[iRow]["IsResult"]) > 0)
                                {
                                    c1TestLibrary.SetCellImage(_Row, COL_ORDER_HAS_RESULTS, gloEmdeonInterface.Properties.Resources.FlagAcknowledge1);
                                }
                                else
                                {
                                    c1TestLibrary.SetCellImage(_Row, COL_ORDER_HAS_RESULTS, gloEmdeonInterface.Properties.Resources.FlagNone);
                                }
                            }
                            if (ConfirmNull(_MachineName.ToString()))
                            {
                                c1TestLibrary.SetData(_Row, COL_MACHINENAME, _MachineName.ToString());
                                c1TestLibrary.SetCellImage(_Row, COL_ISORDERLOCKED, gloEmdeonInterface.Properties.Resources.Lock);
                            }

                            _nOrderID = 0;
                            _MachineName = string.Empty;

                            #endregion

                            c1TestLibrary.SetData(_Row, COL_REFEREANCEID, Convert.ToString(dt.Rows[iRow]["labom_ExternalCode"]).Trim());


                            //string _LabExternalCode = string.Empty;

                            //_LabExternalCode = Convert.ToString(dt.Rows[iRow]["labom_ExternalCode"].ToString());

                            //if (ConfirmNull(_LabExternalCode))
                            //{
                            //    if (ConfirmNull(_strBillingType))
                            //    {
                            //        if (ConfirmNull(_strOrderStatus))
                            //        {
                            //            c1TestLibrary.SetData(_Row, COL_REFEREANCEID, _LabExternalCode.ToString());
                            //        }
                            //    }
                            //    else
                            //    {
                            //        c1TestLibrary.SetData(_Row, COL_REFEREANCEID, _LabExternalCode.ToString());
                            //    }

                            //}

                            //_LabExternalCode = string.Empty;


                            _strOrderStatus = string.Empty;
                            _strBillingType = string.Empty;

                        }
                    }
                    // SendtoLab();

                    if (c1TestLibrary.RowSel > 0)
                    {
                        if (Convert.ToString(c1TestLibrary.GetData(c1TestLibrary.RowSel, COL_ORDERSTATUS)) == "Not Send" && tabControl1.SelectedIndex == 0)
                            tlbbtn_SendtoLab.Enabled = true;
                        else
                            tlbbtn_SendtoLab.Enabled = false;
                    }
                    else
                        tlbbtn_SendtoLab.Enabled = false;

                    if (ConfirmNull(_OrderSelectID))
                    {
                        int _TestID = c1TestLibrary.FindRow(_OrderSelectID, 1, COL_ORDERID, false, true, true);
                        c1TestLibrary.Select(_TestID, 0);
                    }

                }


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            c1TestLibrary.EndUpdate();

            if (dt != null)  //added for memory management
            {
                dt.Dispose();
                dt = null;
            }

        }

        //29-May-13 Aniket: Orders PRD: Unused Code
        //private void fillOpenOrdsGrid()
        //{
        //    DataView dvOrders = new DataView();
        //    Int32 rowsCountToDisplay = 0;
        //    Int32 iRow1 = 0;
        //    Int32 ordRowNo = 0;

        //    Int64 _providerID = 0;

        //    try
        //    {
        //        // design the gird for showing patient orders
        //        DesignGrid();

        //        dvOrders = GetOrders(_patientID);
        //        if (_OrderSelectID != "")// when form called with order id constructor
        //        {
        //            if (dvOrders.Count <= 100)
        //            {
        //                iRow1 = 0;
        //                rowsCountToDisplay = dvOrders.Count;
        //            }
        //            else
        //            {
        //                for (int cnt = 0; cnt < dvOrders.Count; cnt++)
        //                {
        //                    if (dvOrders[cnt]["labom_OrderID"].ToString() == _OrderSelectID)
        //                    {
        //                        ordRowNo = cnt;
        //                        break;
        //                    }
        //                }

        //                if (dvOrders.Count > (ordRowNo + 100))
        //                {
        //                    iRow1 = ordRowNo;
        //                    rowsCountToDisplay = ordRowNo + 100;
        //                }
        //                else
        //                {
        //                    iRow1 = dvOrders.Count - 100;
        //                    rowsCountToDisplay = dvOrders.Count;
        //                }
        //            }
        //        }
        //        else // when form called without order id constructor
        //        {
        //            iRow1 = 0;
        //            if (dvOrders.Count < 100)
        //                rowsCountToDisplay = dvOrders.Count;
        //            else
        //                rowsCountToDisplay = 100;
        //        }


        //        if (dvOrders.Count > 0)
        //        {
        //            for (int iRow = iRow1; iRow <= rowsCountToDisplay - 1; iRow++)
        //            {

        //                c1TestLibrary.Rows.Add();
        //                Int32 _Row = c1TestLibrary.Rows.Count - 1;

        //                c1TestLibrary.SetData(_Row, COL_ORDERID, dvOrders[iRow]["labom_OrderID"].ToString());
        //                c1TestLibrary.SetData(_Row, COL_ORDERPREFIX, dvOrders[iRow]["labom_OrderNoPrefix"].ToString() + " " + dvOrders[iRow]["labom_OrderNoID"].ToString());
        //                c1TestLibrary.SetData(_Row, COL_ORDERNO, dvOrders[iRow]["labom_OrderNoID"].ToString());
        //                c1TestLibrary.SetData(_Row, COL_TRANSDATE, dvOrders[iRow]["labom_OrderDate"].ToString());

        //                c1TestLibrary.SetData(_Row, COL_PROVIDERNAME, dvOrders[iRow]["ProviderName"].ToString());

        //                // Added by Abhijeet on date 20100330
        //                clsGeneral objclsGeneral = new clsGeneral();
        //                string _strOrderStatus = objclsGeneral.GetOrderStatus(Convert.ToInt32(dvOrders[iRow]["labom_gloLabOrderStatus"]), 0);
        //                string _strBillingType = objclsGeneral.GetBillingType(Convert.ToInt32(dvOrders[iRow]["labom_gloLabOrderBillingType"]), 0);


        //                c1TestLibrary.SetData(_Row, COL_ORDERSTATUS, _strOrderStatus);
        //                c1TestLibrary.SetData(_Row, COL_BILLINGTYPE, _strBillingType);

        //                //Madan added on 20100512
        //                #region ContainsAcknowledgement

        //                Int64 _nOrderID = 0;
        //                Int64 _nOrderNumberID = 0;
        //                string _OrderNumberPreFix = string.Empty;
        //                bool blnIsOrderContainResults = false;

        //                //added by madan on 20100619
        //                string _MachineName = string.Empty;
        //                //end madan

        //                _nOrderID = Convert.ToInt64(dvOrders[iRow]["labom_OrderID"].ToString());
        //                _nOrderNumberID = Convert.ToInt64(dvOrders[iRow]["labom_OrderNoID"].ToString());
        //                _OrderNumberPreFix = Convert.ToString(dvOrders[iRow]["labom_OrderNoPrefix"].ToString());
        //                _MachineName = Convert.ToString(dvOrders[iRow]["labom_MachineName"].ToString());

        //                if (CheckAcknoledgement(_nOrderID, _OrderNumberPreFix, _nOrderNumberID))
        //                {
        //                    c1TestLibrary.SetCellImage(_Row, COL_ORDER_ISACKNOWLEDGED, gloEmdeonInterface.Properties.Resources.Yes);
        //                }
        //                else
        //                {
        //                    c1TestLibrary.SetCellImage(_Row, COL_ORDER_ISACKNOWLEDGED, gloEmdeonInterface.Properties.Resources.FlagNone);
        //                }

        //                if (_nOrderID != 0)
        //                {
        //                    blnIsOrderContainResults = CheckResultsForOrder(_nOrderID);
        //                }
        //                if (blnIsOrderContainResults)
        //                {
        //                    c1TestLibrary.SetCellImage(_Row, COL_ORDER_HAS_RESULTS, gloEmdeonInterface.Properties.Resources.FlagAcknowledge1);
        //                }
        //                else
        //                {
        //                    c1TestLibrary.SetCellImage(_Row, COL_ORDER_HAS_RESULTS, gloEmdeonInterface.Properties.Resources.FlagNone);
        //                }
        //                if (ConfirmNull(_MachineName.ToString()))
        //                {
        //                    c1TestLibrary.SetData(_Row, COL_MACHINENAME, _MachineName.ToString());
        //                    c1TestLibrary.SetCellImage(_Row, COL_ISORDERLOCKED, gloEmdeonInterface.Properties.Resources.Lock);
        //                }

        //                _nOrderID = 0;
        //                _nOrderNumberID = 0;
        //                _OrderNumberPreFix = string.Empty;
        //                blnIsOrderContainResults = false;
        //                _MachineName = string.Empty;

        //                #endregion

        //                //Added by madan on 20100831
        //                if (ConfirmNull(_strOrderStatus) && ConfirmNull(_strBillingType))
        //                {

        //                    string _LabExternalCode = string.Empty;

        //                    _LabExternalCode = Convert.ToString(dvOrders[iRow]["labom_ExternalCode"].ToString());

        //                    if (ConfirmNull(_LabExternalCode))
        //                    {
        //                        c1TestLibrary.SetData(_Row, COL_REFEREANCEID, _LabExternalCode.ToString());
        //                    }

        //                    _LabExternalCode = string.Empty;

        //                }

        //                _strOrderStatus = string.Empty;
        //                _strBillingType = string.Empty;

        //                objclsGeneral.Dispose();

        //                // End of changes by Abhijeet
        //            }

        //            SendtoLab();

        //            if (ConfirmNull(_OrderSelectID))//Added by madan for selecting Particular  order.
        //            {
        //                int _TestID = c1TestLibrary.FindRow(_OrderSelectID, 1, COL_ORDERID, false, true, true);
        //                c1TestLibrary.Select(_TestID, 0);
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
        //    }

        //}

        private DataTable GetOrder_New(long PatientID)
        {

            gloEMRGeneralLibrary.gloEMRLab.gloEMRLabOrder obj = new gloEMRLabOrder();
            try
            {
                return (obj.GetOrder_New(PatientID, dtPickerFromDate.Value.ToString("MM/dd/yyyy"), dtPickerToDate.Value.ToString("MM/dd/yyyy"), cmbOrderStatus.SelectedItem.ToString().Trim().ToLower(), 0));

            }
            catch //(ExecutionEngineException exx)
            {
                return null;
            }
            finally
            {
                //if (dt != null)
                //{
                //    dt.Dispose();
                //    dt = null;
                //}
                if (obj != null)
                {
                    obj.Dispose();
                    obj = null;
                }
            }
        }

        //29-May-13 Aniket: Orders PRD: Unused Code
        //private DataView GetOrders(long PatientId)
        //{
        //    DataTable dt = new DataTable();
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
        //    DataView dv = new DataView();
        //    string strFilterCondition = string.Empty;
        //    try
        //    {
        //        //first get all the orders aginst that PatientID                                  

        //        string strSql = "SELECT DISTINCT labom_OrderID, Lab_Order_MST.labom_OrderNoPrefix,Lab_Order_MST.nClinicID," +
        //                         "Lab_Order_MST.labom_OrderNoID, Lab_Order_MST.labom_TransactionDate ,labom_ProviderID," +
        //                         "Provider_MST.sFirstName +' '+ CASE ISNULL(Provider_MST.sMiddleName,'') WHEN '' THEN '' ELSE Provider_MST.sMiddleName +' ' END +''+ Provider_MST.sLastName AS ProviderName ," +
        //                         "ISNULL(Lab_Order_MST.labom_ExternalCode,100) AS labom_ExternalCode," +
        //                         "ISNULL(Lab_Order_MST.labom_gloLabOrderStatus,100) AS labom_gloLabOrderStatus, " +
        //                         "ISNULL(Lab_Order_MST.labom_gloLabOrderBillingType,100) AS labom_gloLabOrderBillingType, " +
        //                         "ISNULL(Lab_Order_MST.labom_VisitID,0) AS labom_VisitID,Lab_Order_MST.bIsClosed," +
        //                         "ISNULL(Lab_Order_MST.labom_MachineName,100) AS labom_MachineName ," +
        //                         "convert(varchar,Lab_Order_MST.labom_OrderDate,101) as TransactionDate,Lab_Order_MST.labom_OrderDate FROM Lab_Order_MST " +
        //                         "INNER Join Provider_MST ON lab_order_mst.labom_ProviderID = Provider_MST.nProviderID " +
        //                         " LEFT OUTER JOIN Lab_Order_Test_Result ON Lab_Order_MST.labom_OrderID = " +
        //                         "Lab_Order_Test_Result.labotr_OrderID LEFT OUTER JOIN " +
        //                         "Lab_Order_Test_ResultDtl ON Lab_Order_Test_Result.labotr_OrderID =" +
        //                         "Lab_Order_Test_ResultDtl.labotrd_OrderID AND " +
        //                         "Lab_Order_Test_Result.labotr_TestID = Lab_Order_Test_ResultDtl.labotrd_TestID " +
        //                         " WHERE(labom_PatientID = " + PatientId + ")ORDER By labom_TransactionDate desc, labom_OrderNoID desc";

        //        oDB.Connect(false);

        //        oDB.Retrive_Query(strSql, out dt);
        //        dv = dt.DefaultView;

        //        strFilterCondition = " TransactionDate >= #" +
        //                                            dtPickerFromDate.Value.ToString("MM/dd/yyyy") +
        //                                    "# and TransactionDate <= #" +
        //                                            dtPickerToDate.Value.ToString("MM/dd/yyyy") + "# ";


        //        string _stringorderAckStatus = cmbOrderStatus.SelectedItem.ToString().Trim().ToLower();
        //        switch (_stringorderAckStatus)
        //        {
        //            case "yes":
        //                strFilterCondition = strFilterCondition + " and bIsClosed='True'";
        //                break;
        //            case "no":
        //                strFilterCondition = strFilterCondition + " and bIsClosed='False'";
        //                break;
        //            case "all":
        //                break;
        //            default:
        //                break;
        //        }
        //        _stringorderAckStatus = string.Empty;

        //        dv.RowFilter = strFilterCondition;

        //        oDB.Disconnect();

        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
        //        return null;
        //    }
        //    finally
        //    {

        //        if (oDB != null)
        //        {
        //            oDB.Dispose();
        //            oDB = null;
        //        }

        //        if (dt != null)
        //        {
        //            dt.Dispose();
        //            dt = null;
        //        }
        //    }
        //    return dv;
        //}

        private void DesignGrid()
        {

            try
            {
               // c1TestLibrary.Clear();
                c1TestLibrary.DataSource = null;
                c1TestLibrary.Clear();

                // setfont
                c1TestLibrary.Font = gloGlobal.clsgloFont.gFont; //new System.Drawing.Font("Tahoma", 9, FontStyle.Regular);
                c1TestLibrary.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
                c1TestLibrary.BackColor = Color.White;
                c1TestLibrary.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;

                c1TestLibrary.Cols.Count = COL_COUNT;
                c1TestLibrary.Cols.Fixed = 1;
                c1TestLibrary.Rows.Count = 1;
                c1TestLibrary.Rows.Fixed = 1;

                // set visibility of column
                c1TestLibrary.Cols[COL_ORDERID].Visible = false;
                c1TestLibrary.Cols[COL_ORDERPREFIX].Visible = true;
                c1TestLibrary.Cols[COL_ORDERNO].Visible = false;
                c1TestLibrary.Cols[COL_TRANSDATE].Visible = true;
                c1TestLibrary.Cols[COL_PROVIDERNAME].Visible = true;

                c1TestLibrary.Cols[COL_CUSTOMORDERSTATUS].Visible = true; //29-May-13 Aniket: Orders PRD: 
                c1TestLibrary.Cols[COL_ORDERSTATUS].Visible = true; // by Abhijeet on date 20100330
                c1TestLibrary.Cols[COL_BILLINGTYPE].Visible = true; // by Abhijeet on date 20100330
                c1TestLibrary.Cols[COL_ISORDERLOCKED].Visible = false; //Added by madan on 20100619
                c1TestLibrary.Cols[COL_MACHINENAME].Visible = false;//Added by madan on 20100619
                c1TestLibrary.Cols[COL_REFEREANCEID].Visible = true; //Added by madan on 20100831
                c1TestLibrary.Cols[COL_HasAkw_Value].Visible = false;
                c1TestLibrary.Cols[COL_HasResult_Value].Visible = false;
                c1TestLibrary.Cols[COL_ORDERING_PROVIDERID].Visible = false;
                c1TestLibrary.Cols[COL_ORDERCOMMENTS].Visible = false;//20140407 manoj PRD: View Lab Order Comments :Added New column

                c1TestLibrary.Cols[COL_TRANSDATE].Width = 200;
                c1TestLibrary.Cols[COL_PROVIDERNAME].Width = 200;// by Abhijeet on date 20100330

                c1TestLibrary.Cols[COL_CUSTOMORDERSTATUS].Width = 200; //29-May-13 Aniket: Orders PRD: 
                c1TestLibrary.Cols[COL_ORDERSTATUS].Width = 100; //width changed for bugid 70143 for hiding unnecessary scroll bar
                c1TestLibrary.Cols[COL_ORDERPREFIX].Width = 80; //width changed for bugid 70143 for hiding unnecessary scroll bar
                c1TestLibrary.Cols[COL_TRANSDATE].Width = 140; //width changed for bugid 70143 for hiding unnecessary scroll bar
                c1TestLibrary.Cols[COL_ORDER_HAS_RESULTS].Width = 70;//Added by madan on 20100512
                c1TestLibrary.Cols[COL_ORDER_ISACKNOWLEDGED].Width = 110;//Added by madan on 20100512
                c1TestLibrary.Cols[COL_ISORDERLOCKED].Width = 100; //added by madan on 20100619
                c1TestLibrary.Cols[COL_REFEREANCEID].Width = 100; //Added by madan on 20100831
                c1TestLibrary.Cols[COL_ORDERCOMMENTS].Width = 0;//20140407 manoj PRD: View Lab Order Comments :Added New column

                // set column editing
                c1TestLibrary.Cols[COL_ORDERPREFIX].AllowEditing = false;
                c1TestLibrary.Cols[COL_ORDERNO].AllowEditing = false;
                c1TestLibrary.Cols[COL_TRANSDATE].AllowEditing = false;
                c1TestLibrary.Cols[COL_PROVIDERNAME].AllowEditing = false;

                c1TestLibrary.Cols[COL_CUSTOMORDERSTATUS].AllowEditing = false; //29-May-13 Aniket: Orders PRD: 
                c1TestLibrary.Cols[COL_ORDERSTATUS].AllowEditing = false; // by Abhijeet on date 20100330
                c1TestLibrary.Cols[COL_BILLINGTYPE].AllowEditing = false; // by Abhijeet on date 20100330
                c1TestLibrary.Cols[COL_ORDER_ISACKNOWLEDGED].AllowEditing = false;//Added by madan on 20100512
                c1TestLibrary.Cols[COL_ORDER_HAS_RESULTS].AllowEditing = false;//Added by madan on 20100512
                c1TestLibrary.Cols[COL_ISORDERLOCKED].AllowEditing = false; //added by madan on 2010618
                c1TestLibrary.Cols[COL_REFEREANCEID].AllowEditing = false;  //Added by madan on 20100831


                //set Heading
                c1TestLibrary.SetData(0, COL_ORDERNO, "Order ID");
                c1TestLibrary.SetData(0, COL_ORDERPREFIX, "Order #");
                c1TestLibrary.SetData(0, COL_REFEREANCEID, "Reference #");//Added by madan on 20100831
                //c1TestLibrary.SetData(0, COL_ORDERNO, "Order NO");
                c1TestLibrary.SetData(0, COL_TRANSDATE, "Order Date");
                c1TestLibrary.SetData(0, COL_PROVIDERNAME, "Provider Name");

                c1TestLibrary.SetData(0, COL_CUSTOMORDERSTATUS, "Order Status"); //29-May-13 Aniket: Orders PRD: 
                c1TestLibrary.SetData(0, COL_ORDERSTATUS, "Electronic Order Status");// by Abhijeet on date 20100330
                c1TestLibrary.SetData(0, COL_BILLINGTYPE, "Billing Type");// by Abhijeet on date 20100330
                c1TestLibrary.SetData(0, COL_ORDER_HAS_RESULTS, "Results");
                c1TestLibrary.SetData(0, COL_ORDER_ISACKNOWLEDGED, "Acknowledged");
                //added by madan on 20100619
                c1TestLibrary.SetData(0, COL_ORDERCOMMENTS, "Order Comments");//20140407 manoj PRD: View Lab Order Comments :Added New column

                c1TestLibrary.Cols[COL_ISORDERLOCKED].ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.RightCenter;
                c1TestLibrary.Cols[COL_ISORDERLOCKED].TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1TestLibrary.SetData(0, COL_ISORDERLOCKED, "Locked");

                //Added by madan on 20100831
                c1TestLibrary.Cols[COL_REFEREANCEID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                //End Changes

                c1TestLibrary.ExtendLastCol = true;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void frmViewgloLab_Load(object sender, EventArgs e)
        {
           
              SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            tlbbtn_Acknowledgment.Visible = false;



            try
            {

                _FormIsLoded = false;

                if (gloEMRGeneralLibrary.gloGeneral.clsgeneral.gstrSpecificResultRange.Trim() == "1")
                {
                    tlbbtnRefresh.Visible = true;
                    tlbResultRange.Visible = true;
                }
                else
                {
                    tlbbtnRefresh.Visible = false;
                    tlbResultRange.Visible = false;
                }
                UpdateResultFlag();

                gloPatient.gloPatient objgloPatient = new gloPatient.gloPatient(_dataBaseConnectionString);
                if (objpatient != null)
                {
                    objpatient.Dispose();
                    objpatient = null;

                }
                objpatient = objgloPatient.GetPatient(_patientID);
                objgloPatient.Dispose();
                objgloPatient = null;

                _PatientProviderID = objpatient.DemographicsDetail.PatientProviderID;
                _LoginProviderId = GetProviderIDForUser(_LoginUserID);

                if (clsEmdeonGeneral.IsDemoLab)
                {
                    clsLabDemonstration objClsDemonstration = new clsLabDemonstration();
                    if (objClsDemonstration.ActivateDemoLabs(_patientID))
                    {
                        _IsDemoLab = true;
                    }
                    else
                    {
                        _IsDemoLab = false;
                    }
                    objClsDemonstration.Dispose();
                    objClsDemonstration = null;
                }
                
                try
                {
                    // For initializing the LAB parameters on Form load.
                    clsEmdeonGeneral.CheckConnectionParameters(_dataBaseConnectionString);
                }
                catch  
                {  
                }
                clsGeneral oclsGeneral = null;
                try
                {
                    // For initializing the LAB parameters on Form load.
                    oclsGeneral = new clsGeneral();
                    oclsGeneral.GetResultSetCommentsFontSettings(out  LabResultSetFontSize, out LabResultSetFontName);

                }
                catch
                {
                }
                finally { if (oclsGeneral != null) { oclsGeneral.Dispose(); } oclsGeneral = null; }

                gloUCLab_OrderDetail.IsOpenedFromViewLab = true;
                String _selecteOrderDate = "";
                if (_OrderSelectID != "" && SelectOrderTab == true)
                {
                    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
                    string strQuery = "select convert(varchar,labom_OrderDate,101) as TransactionDate, case isnull(bIsClosed,0) when 0 then 'True'  else 'False' end  as bIsClosed" +
                                    " from lab_order_mst where labom_orderid=" + System.Convert.ToInt64(_OrderSelectID);

                    //string strQuery = "SELECT COALESCE(Lab_Order_Test_ResultDtl.labotrd_TestSpecimenCollectionDateTime,Lab_Order_Test_ResultDtl.labotrd_ResultDateTime) AS TestDateTime from Lab_Order_Test_ResultDtl where labotrd_orderid=" + System.Convert.ToInt64(_OrderSelectID);
                    DataTable dt = null;

                    oDB.Connect(false);
                    oDB.Retrive_Query(strQuery, out dt);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        cmbOrderStatus.SelectedItem = (Convert.ToString(dt.Rows[0]["bIsClosed"]) == "True" ? "No" : "Yes");
                        dtPickerToDate.Value = System.DateTime.Now;
                        dtPickerFromDate.Value = Convert.ToDateTime(dt.Rows[0]["TransactionDate"]);
                    }
                    else
                    {
                        cmbOrderStatus.SelectedItem = "No";
                        dtPickerToDate.Value = System.DateTime.Now;
                        dtPickerFromDate.Value = System.DateTime.Now.AddMonths(-12);
                    }
                    //SAnjog 
                    //if (dt != null && dt.Rows.Count > 0)
                    //{
                    //    _selecteOrderDate = Convert.ToString(dt.Rows[0]["TestDateTime"]);
                    //}
                    //dt.Dispose();
                    if (oDB != null)
                    {
                        oDB.Disconnect();
                        oDB.Dispose();
                    }
                    if (dt != null) //added for memory management
                    {
                        dt.Dispose();
                        dt = null;
                    }
                }
                else
                {
                    cmbOrderStatus.SelectedItem = "All";
                    dtPickerToDate.Value = System.DateTime.Now;
                    dtPickerFromDate.Value = System.DateTime.Now.AddMonths(-12);
                }

                RefershAllGrids();
                tlbbtn_SendtoLab.Enabled = false;
                tlbBtnMergeOrder.Visible = gloEmdeonCommon.mdlGeneral.IsMergeOrderEnabled;

                if (ConfirmNull(_OrderSelectID)) //Added by madan on 20100401-- this is activated when this form is loaded with the help of overloaded constructor
                {
                    //Developer:Sanjog Dhamke
                    //Date:2 Feb 2012
                    //PRD Name: Lab Usability Change 6060/7000
                    //Reason: if order is open for modification from Double click on patient details panel,synopsis and outstanding orders then shows the order tab and else shows the result set tab.
                    if (SelectOrderTab == true) // means open order for modification so direct toward the order tab
                    {
                        tabControl1.SelectTab(0);
                        gloUCLab_OrderDetail.Visible = true;
                    }
                    else //Directed to result set tab
                    {
                        tabControl1.SelectTab(3);
                    }
                }
                else
                {

                    string strTab = GetlabTabSetting();

                    switch (strTab.ToUpper())
                    {
                        case "LAB FLOWSHEET":
                            tabControl1.SelectTab(2);
                            break;
                        case "RESULTS":
                            tabControl1.SelectTab(1);
                            break;
                        case "ORDERS":
                            tabControl1.SelectTab(0);
                            break;
                        case "RESULT SETS":
                            tabControl1.SelectTab(3);
                            break;
                        default:
                            tabControl1.SelectTab(1);
                            break;
                    }

                }

                if (oUc_ResultSet != null)
                {
                    oUc_ResultSet.OpenDocument -= new gloUserControlLibrary.LabUC_ResultSet.OpenDMS(gloLabUC_Transaction1_gUC_ViewDocument);
                    oUc_ResultSet.ShowAck -= new gloUserControlLibrary.LabUC_ResultSet.ShowAckButton(ShowAckwButton);
                    oUc_ResultSet.customMenuOpening -= new gloUserControlLibrary.LabUC_ResultSet.contextmnuOpen(oUc_ResultSet_customMenuOpening);
                    oUc_ResultSet.Acknowlegeclick -= new gloUserControlLibrary.LabUC_ResultSet.ContextAcknowledge(oUc_ResultSet_Acknowlegeclick);
                    oUc_ResultSet.AcknowlegeNormalclick -= new gloUserControlLibrary.LabUC_ResultSet.ContextAcknowledge(oUc_ResultSet_AcknowlegeNormalclick);
                    oUc_ResultSet.MarkUnAcknowlegeclick -= new gloUserControlLibrary.LabUC_ResultSet.ContextAcknowledge(oUc_ResultSet_MarkUnAcknowlegeclick);
                    oUc_ResultSet.OpenDicomFile -= new gloUserControlLibrary.LabUC_ResultSet.OpenDicom(gloLabUC_Transaction1_gUC_ViewDicomDocument);
                    oUc_ResultSet.EvtCnxtMnuResult_SNOMED -= new gloUserControlLibrary.LabUC_ResultSet.CnxtMnuResultSet(oUc_ResultSet_CntxMenuSNOMED);
                    oUc_ResultSet.EvtCnxtMnuResult_ICD -= new gloUserControlLibrary.LabUC_ResultSet.CnxtMnuResultSet(oUc_ResultSet_CntxMenuICD);
                    oUc_ResultSet.EvtCnxtMnuResult_LOINC -= new gloUserControlLibrary.LabUC_ResultSet.CnxtMnuResultSet(oUc_ResultSet_CntxMenuLOINC);
                    oUc_ResultSet.ViewCodification -= new gloUserControlLibrary.LabUC_ResultSet.Codification(oUc_ResultSet_ViewCodification);
                    oUc_ResultSet.RemoveCodification -= new gloUserControlLibrary.LabUC_ResultSet.Codification(oUc_ResultSet_RemoveCodification);

                    oUc_ResultSet = null;
                }
                //Sanjog
                oUc_ResultSet = new gloUserControlLibrary.LabUC_ResultSet(_patientID, 0);
                oUc_ResultSet.OpenDocument += new gloUserControlLibrary.LabUC_ResultSet.OpenDMS(gloLabUC_Transaction1_gUC_ViewDocument);
                oUc_ResultSet.ShowAck += new gloUserControlLibrary.LabUC_ResultSet.ShowAckButton(ShowAckwButton);
                oUc_ResultSet.customMenuOpening += new gloUserControlLibrary.LabUC_ResultSet.contextmnuOpen(oUc_ResultSet_customMenuOpening);
                oUc_ResultSet.Acknowlegeclick += new gloUserControlLibrary.LabUC_ResultSet.ContextAcknowledge(oUc_ResultSet_Acknowlegeclick);
                oUc_ResultSet.AcknowlegeNormalclick += new gloUserControlLibrary.LabUC_ResultSet.ContextAcknowledge(oUc_ResultSet_AcknowlegeNormalclick);
                oUc_ResultSet.MarkUnAcknowlegeclick += new gloUserControlLibrary.LabUC_ResultSet.ContextAcknowledge(oUc_ResultSet_MarkUnAcknowlegeclick);
                oUc_ResultSet.EvtCnxtMnuResult_SNOMED += new gloUserControlLibrary.LabUC_ResultSet.CnxtMnuResultSet(oUc_ResultSet_CntxMenuSNOMED);
                oUc_ResultSet.EvtCnxtMnuResult_ICD += new gloUserControlLibrary.LabUC_ResultSet.CnxtMnuResultSet(oUc_ResultSet_CntxMenuICD);
                oUc_ResultSet.EvtCnxtMnuResult_LOINC += new gloUserControlLibrary.LabUC_ResultSet.CnxtMnuResultSet(oUc_ResultSet_CntxMenuLOINC);
                oUc_ResultSet.ViewCodification += new gloUserControlLibrary.LabUC_ResultSet.Codification(oUc_ResultSet_ViewCodification);
                oUc_ResultSet.RemoveCodification += new gloUserControlLibrary.LabUC_ResultSet.Codification(oUc_ResultSet_RemoveCodification);

                oUc_ResultSet.LabResultSetCommentFontName = LabResultSetFontName;
                oUc_ResultSet.LabResulSetCommentFontSize = LabResultSetFontSize;

                if (_OrderSelectID != "" && SelectOrderTab == false)
                {
                    // if came from task then only selected order was showing, now required all order to be shown hence below line commented.
                    // oUc_ResultSet._SelectedOrderID = Convert.ToInt64(_OrderSelectID);
                    // Added Below varible to set Order selected for related Task
                    oUc_ResultSet._TaskRelatedOrderID = Convert.ToInt64(_OrderSelectID);
                }
                oUc_ResultSet._SelectedOrderDate = _selecteOrderDate;
                oUc_ResultSet.OpenDicomFile += new gloUserControlLibrary.LabUC_ResultSet.OpenDicom(gloLabUC_Transaction1_gUC_ViewDicomDocument);
                this.elementHost1.Child = oUc_ResultSet;
                //Sanjog

                chk_PreviousHistory.Checked = false;

                GetSettings(); //Method to get Admin settings...
                gloUCLab_TestDetail.SetData(0, "", "", "", "", "", "", "");

                _OrderSelectID = null;

                //Developer:Sanjog Dhamke
                //Date:2 Feb 2012
                //PRD Name: Lab Usability Change 6060/7000
                //Reason: after showing selected order value, selecteOrderTab make false again
                SelectOrderTab = false;
               //code added for displaying split control 
                if ((clsSplit_Laborder != null) && (blnLoadJanusControl==true))
                {
                    if (uiPanSplitScreen_LabOrder == null)
                    {
                        uiPanSplitScreen_LabOrder = new Janus.Windows.UI.Dock.UIPanelGroup();
                        uiPanSplitScreen_LabOrder.BringToFront();
                    }
                    clsSplit_Laborder.clsUCLabControl = new gloUserControlLibrary.gloUC_TransactionHistory();
                    if (clsSplit_Laborder.clsPatientExams == null)
                    {
                        clsSplit_Laborder.clsPatientExams = objgloLabPatientExam;
                    }
                    if (clsSplit_Laborder.clsPatientLetters == null)
                    {
                        clsSplit_Laborder.clsPatientLetters = objgloLabPatientLetters;
                    }
                    if (clsSplit_Laborder.clsPatientMessages == null)
                    {
                        clsSplit_Laborder.clsPatientMessages = objgloLabPatientMessages;
                    }
                    if (clsSplit_Laborder.clsNurseNotes == null)
                    {
                       clsSplit_Laborder.clsNurseNotes = objgloLabNurseNotes;
                    }
                    if (clsSplit_Laborder.clsHistory == null)
                    {
                        clsSplit_Laborder.clsHistory = objgloLabHistory;
                    }
                    if (clsSplit_Laborder.clsLabs == null)
                    {
                        clsSplit_Laborder.clsLabs = objgloLabLabs;
                    }
               
                    if (clsSplit_Laborder.clsRxmed == null)
                    {
                        clsSplit_Laborder.clsRxmed = objgloLabRxmed;
                    }
                    if (clsSplit_Laborder.clsOrders == null)
                    {
                        clsSplit_Laborder.clsOrders = objgloLabOrders;
                    }
                    if (clsSplit_Laborder.clsProblemList == null)
                    {
                        clsSplit_Laborder.clsProblemList = objgloLabProblemList;
                    }
                    clsSplit_Laborder.clsDMS = new gloEDocumentV3.eDocManager.eDocGetList();
                    if (objCriteria == null)
                    {
                        objCriteria = objgloLabCriteria;
                    }
                    if (objWord == null)
                    {
                        objWord = objgloLabWord;
                    }
                    
                    uiPanSplitScreen_LabOrder = clsSplit_Laborder.LoadSplitControl(this, _patientID, VisitID, "View Lab", objCriteria, objWord, _ClinicID, _LoginUserID);
                    
                }

                
                try
                {
                    gloPatient.gloPatient.GetWindowTitle(this, _patientID, _dataBaseConnectionString, gstrMessageBoxCaption);
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                }

                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.View, "View labs opened", _patientID, 0, _PatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Error in loading view lab form :" + ex.ToString(), false);
            }
            finally
            {
                _FormIsLoded = true;
            }
        }

        public void oUc_ResultSet_customMenuOpening(object sender, System.Windows.Controls.ContextMenuEventArgs e)
        {
            try
            {
                if (GetOrderStatusISAcknowledged(oUc_ResultSet.OrderID))
                {
                    oUc_ResultSet.ShowHideAckcontextMenuTrip(false);
                    oUc_ResultSet.ShowHideAckNormalcontextMenuTrip(false);
                    oUc_ResultSet.ShowHideUnAckNormalcontextMenuTrip(true);
                }
                else
                {
                    oUc_ResultSet.ShowHideAckcontextMenuTrip(true);
                    oUc_ResultSet.ShowHideAckNormalcontextMenuTrip(true);
                    oUc_ResultSet.ShowHideUnAckNormalcontextMenuTrip(false);
                }

                //if (GetOrderStatusISAcknowledged(oUc_ResultSet.OrderID))
                //{
                //    e.Handled = true;
                //}
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Error in Opening ContextMenu :" + ex.ToString(), false);
            }
        }

        public bool GetOrderStatusISAcknowledged(Int64 nOrderID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            string strQry = string.Empty;
            bool IsAck = false;

            try
            {
                if (nOrderID != 0)
                {
                    oDB.Connect(false);
                    strQry = "SELECT ISNULL(bIsClosed,0) FROM Lab_Order_MST where labom_OrderID='" + nOrderID + "' ";
                    IsAck = Convert.ToBoolean(oDB.ExecuteScalar_Query(strQry));
                    oDB.Disconnect();
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                IsAck = false;
            }
            finally
            {
                oDB.Dispose();
            }
            return IsAck;
        }

        public void oUc_ResultSet_MarkUnAcknowlegeclick()
        {
            try
            {
                UpdateOrder(oUc_ResultSet.OrderID, false);
                RefershAllGrids();
                if (tabControl1.SelectedIndex == 3)
                {
                    oUc_ResultSet.ShowTestData();
                }
                dynamic mdi = this.MdiParent;
                mdi.ShowTasks();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Error in Mark Un-Acknowledge from contextmenustrip :" + ex.ToString(), false);
            }
        }

        public void oUc_ResultSet_Acknowlegeclick()
        {
            //MessageBox.Show("FROM EMR EVENT CATCHED");
            try
            {
                if (ViewAckHistory())
                {
                    RefershAllGrids();
                    if (tabControl1.SelectedIndex == 3)
                    {
                        oUc_ResultSet.ShowTestData();
                    }
                    dynamic mdi = this.MdiParent;
                    mdi.ShowTasks();

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Error in loading Acknowledgement form :" + ex.ToString(), false);
            }
        }

        #region "  Lab Result - Codification  "


        gloListControl.gloListControl oListControl;
        frmViewListControl ofrmCodeList;
        gloListControl.gloListControl oDiagnosisListControl;
        string strCurrentCode = string.Empty;
        string strCurrentCodeDesc = string.Empty;
        string sICDType = string.Empty;

        private void CodificationActionsForLabResult(Int64 nOrderId,Int64 nTestId,string sResultName, string CurrentCode, string sActionFlag, string sCodeType)
        {
            gloDatabaseLayer.DBLayer oDBLayer = null;
            gloDatabaseLayer.DBParameters oDBparams = null;

            try
            {   //Create parameters for SP to update the Manual Order status.
                oDBparams = new gloDatabaseLayer.DBParameters();
                oDBparams.Add("@OrderId", nOrderId, ParameterDirection.Input, SqlDbType.BigInt);
                oDBparams.Add("@TestId", nTestId, ParameterDirection.Input, SqlDbType.BigInt);
                oDBparams.Add("@ResultName", sResultName, ParameterDirection.Input, SqlDbType.VarChar);
                oDBparams.Add("@Code", CurrentCode, ParameterDirection.Input, SqlDbType.VarChar);
                oDBparams.Add("@ActionFlag", sActionFlag, ParameterDirection.Input, SqlDbType.VarChar);
                oDBparams.Add("@CodeType", sCodeType, ParameterDirection.Input, SqlDbType.VarChar);

                //Execution for the stored procedures
                oDBLayer = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
                oDBLayer.Connect(false);
                if (sActionFlag != "VIEW")
                {
                    oDBLayer.Execute("gsp_AddRemoveViewResultCodification", oDBparams);
                }
                else
                {
                    DataTable dtCodes=null;
                    oDBLayer.Retrive("gsp_AddRemoveViewResultCodification", oDBparams, out dtCodes);

                    if (dtCodes != null && dtCodes.Rows.Count > 0)
                    {
                            oUc_ResultSet.SnomedCode = Convert.ToString(dtCodes.Rows[0]["sConceptID"]);

                            if (!string.IsNullOrEmpty(Convert.ToString(dtCodes.Rows[0]["sICD9"])))
                            {
                                oUc_ResultSet.ICDCode = Convert.ToString(dtCodes.Rows[0]["sICD9"]);
                                oUc_ResultSet.ICDType = "9";
                            }
                            else if (!string.IsNullOrEmpty(Convert.ToString(dtCodes.Rows[0]["sICD10"])))
                            {
                                oUc_ResultSet.ICDCode = Convert.ToString(dtCodes.Rows[0]["sICD10"]);
                                oUc_ResultSet.ICDType = "10";
                            }
                            else
                            {
                                oUc_ResultSet.ICDCode = "";
                                oUc_ResultSet.ICDType = "";
                            }

                            oUc_ResultSet.LoincCode = Convert.ToString(dtCodes.Rows[0]["sLOINC"]);
                        }
                }
                oDBLayer.Disconnect();

            }
            catch (Exception ex)
            {
                //Log if any exception;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {
                //Cleanup for internal objects utilized in the method.

                if (oDBLayer != null)
                { oDBLayer.Dispose(); oDBLayer = null; }

                if (oDBparams != null)
                {
                    oDBparams.Clear();
                    oDBparams.Dispose(); oDBparams = null;

                }

            }

        }

        private void oDiagnosisListControl_ItemClosedClick(object sender, EventArgs e)
        {
            ofrmCodeList.Close();
        }

        private void oDiagnosisListControl_ItemSelectedClick(object sender, EventArgs e)
        {
            try
            {
                if (oDiagnosisListControl.SelectedItems.Count > 0)
                {
                    for (Int16 i = 0; i <= oDiagnosisListControl.SelectedItems.Count - 1; i++)
                    {
                        strCurrentCode = oDiagnosisListControl.SelectedItems[i].Code;
                        strCurrentCodeDesc = oDiagnosisListControl.SelectedItems[i].Description;
                        sICDType = Convert.ToString(oDiagnosisListControl.IsICD9_10);

                    }
                    ofrmCodeList.Close();
                }
                else
                {
                    ofrmCodeList.Close();
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
           }
        }

        private void CodificationForResultICD()
        {
            string conn = gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer.ConnectionString;
            try
            {
                strCurrentCode = string.Empty;
                strCurrentCodeDesc = string.Empty;
                sICDType = string.Empty;

                ofrmCodeList = new frmViewListControl();
                oDiagnosisListControl = new gloListControl.gloListControl(conn, gloListControl.gloListControlType.Diagnosis, false, this.Width);
                oDiagnosisListControl.ControlHeader = "ICD";
                oDiagnosisListControl.gblnIcd10Transition = true;//gblnIcd10MasterTransition;
                //gblnIcd10Transition   ''If true then ICD10 gets selected 
                oDiagnosisListControl.ItemSelectedClick += oDiagnosisListControl_ItemSelectedClick;
                oDiagnosisListControl.ItemClosedClick += oDiagnosisListControl_ItemClosedClick;
                ofrmCodeList.Controls.Add(oDiagnosisListControl);
                oDiagnosisListControl.Dock = DockStyle.Fill;
                oDiagnosisListControl.BringToFront();

                oDiagnosisListControl.ShowHeaderPanel(false);
                //oDiagnosisListControl.OpenControl()
                ofrmCodeList.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
                ofrmCodeList.Text = "ICD";
                ofrmCodeList.ShowDialog(((((Control)ofrmCodeList).Parent == null) ? this : ((Control)ofrmCodeList).Parent));
                if (((oDiagnosisListControl == null) == false))
                {
                    ofrmCodeList.Controls.Remove(oDiagnosisListControl);
                    oDiagnosisListControl.ItemSelectedClick -= oDiagnosisListControl_ItemSelectedClick;
                    oDiagnosisListControl.ItemClosedClick -= oDiagnosisListControl_ItemClosedClick;
                    oDiagnosisListControl.Dispose();
                    oDiagnosisListControl = null;
                }

                if ((ofrmCodeList == null) == false)
                {
                    ofrmCodeList.Dispose();
                    ofrmCodeList = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
        }

        private void CodificationForResult(string sControlHeader)
        {
            string conn = gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer.ConnectionString;

            try
            {
                ofrmCodeList = new frmViewListControl();
                oListControl = new gloListControl.gloListControl(conn, gloListControl.gloListControlType.MUCQMRefusedCode, false, this.Width);
                oListControl.ControlHeader = sControlHeader;

                strCurrentCode = string.Empty;
                strCurrentCodeDesc = string.Empty;

                if (sControlHeader == "Snomed")
                     oListControl.bShowSnomedCodes = true;
                else if (sControlHeader == "Loinc")
                    oListControl.bShowLonicCodes = true;

                oListControl.ItemSelectedClick += oListControl_ItemSelectedClick;
                oListControl.ItemClosedClick += oListControl_ItemClosedClick;
                ofrmCodeList.Controls.Add(oListControl);
                oListControl.Dock = DockStyle.Fill;
                oListControl.BringToFront();

                oListControl.ShowHeaderPanel(false);
                //oDiagnosisListControl.OpenControl()
                ofrmCodeList.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
                ofrmCodeList.Text = sControlHeader + " Code";
               // ofrmCodeList.ShowDialog();
                ofrmCodeList.ShowDialog(((((Control)ofrmCodeList).Parent == null) ? this : ((Control)ofrmCodeList).Parent));

                if (((oListControl == null) == false))
                {
                    ofrmCodeList.Controls.Remove(oListControl);
                    oListControl.ItemSelectedClick -= oListControl_ItemSelectedClick;
                    oListControl.ItemClosedClick -= oListControl_ItemClosedClick;
                    oListControl.Dispose();
                    oListControl = null;
                }
                if ((ofrmCodeList == null) == false)
                {
                    ofrmCodeList.Dispose();
                    ofrmCodeList = null;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
        }

        private void oListControl_ItemClosedClick(object sender, EventArgs e)
        {
            ofrmCodeList.Close();
        }

        private void oListControl_ItemSelectedClick(object sender, EventArgs e)
        {
            try
            {
                if (oListControl.SelectedItems.Count > 0)
                {
                    for (Int16 i = 0; i <= oListControl.SelectedItems.Count - 1; i++)
                    {
                        strCurrentCode = Convert.ToString(oListControl.SelectedItems[i].Code);
                        strCurrentCodeDesc = Convert.ToString(oListControl.SelectedItems[i].Description);
                    }
                    ofrmCodeList.Close();
                }
                else
                {
                    ofrmCodeList.Close();
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
        }

        public void oUc_ResultSet_CntxMenuSNOMED()
        {
            try
            {
                CodificationForResult("Snomed");

                if (!string.IsNullOrEmpty(strCurrentCode))
                {
                    oUc_ResultSet.SnomedCode = strCurrentCode;
                    CodificationActionsForLabResult(oUc_ResultSet.OrderIdForCode, oUc_ResultSet.TestIdForCode, oUc_ResultSet.ResultNameForCode, strCurrentCode, "ADD", "SNOMED");
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Error in loading SNOMED Context Menu :" + ex.ToString(), false);
            }
        }

        public void oUc_ResultSet_CntxMenuICD()
        {
            try
            {
                try
                {
                    CodificationForResultICD();

                    if (!string.IsNullOrEmpty(strCurrentCode))
                    {
                        oUc_ResultSet.ICDCode = strCurrentCode;
                        oUc_ResultSet.ICDType = sICDType;
                      
                        CodificationActionsForLabResult(oUc_ResultSet.OrderIdForCode, oUc_ResultSet.TestIdForCode, oUc_ResultSet.ResultNameForCode, strCurrentCode, "ADD", "ICD " + sICDType);
                    }

                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog("Error in loading ICD Context Menu :" + ex.ToString(), false);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Error in loading ICD Context Menu :" + ex.ToString(), false);
            }
        }

        public void oUc_ResultSet_CntxMenuLOINC()
        {
            try
            {

                CodificationForResult("Loinc");

                if (!string.IsNullOrEmpty(strCurrentCode))
                {
                    oUc_ResultSet.LoincCode = strCurrentCode;
                    CodificationActionsForLabResult(oUc_ResultSet.OrderIdForCode, oUc_ResultSet.TestIdForCode, oUc_ResultSet.ResultNameForCode, strCurrentCode, "ADD", "LOINC");
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Error in loading LOINC context menu :" + ex.ToString(), false);
            }
        }
       
        public void oUc_ResultSet_ViewCodification(Int64 nOrderId, Int64 nTestId, string sResultName,string sCodeType)
        {
            try
            {
                CodificationActionsForLabResult(nOrderId, nTestId, sResultName, "", "VIEW", sCodeType);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Error in Viewing Code :" + ex.ToString(), false);
            }
        }

        public void oUc_ResultSet_RemoveCodification(Int64 nOrderId, Int64 nTestId, string sResultName, string sCodeType)
        {
            try
            {
               string[] aCodeType= sCodeType.Split(':');
                
               CodificationActionsForLabResult(nOrderId, nTestId, sResultName, "", "REMOVE", aCodeType[0].Trim());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Error in removing Code :" + ex.ToString(), false);
            }
        }
        

        #endregion
        
        public void oUc_ResultSet_AcknowlegeNormalclick()
        {
            try
            {
                if (tabControl1.SelectedIndex == 3)
                {
                    if (oUc_ResultSet.OrderID > 0)
                    {
                        //if (IsDefaultNormalNoteExists())
                        //{
                        frmLab_AcknoledgementHistory ObjAckHistory = new frmLab_AcknoledgementHistory(oUc_ResultSet.OrderID, _dataBaseConnectionString);
                        ObjAckHistory._patientID = _patientID;
                        ObjAckHistory._LoginProviderId = _LoginProviderId;
                        ObjAckHistory._PatientProviderID = _PatientProviderID;
                        ObjAckHistory.Dispose();
                        ObjAckHistory = null;

                        DataTable dtAkw = null;
                        gloEMRGeneralLibrary.gloEMRLab.gloEMRLabOrder oReviewed = new gloEMRGeneralLibrary.gloEMRLab.gloEMRLabOrder();
                        dtAkw = oReviewed.Get_Acknowledgment(oUc_ResultSet.OrderID, 0, _LoginUserID);


                        string strUserName = string.Empty;
                        string strComment = string.Empty;
                        string strPatientNotes = string.Empty;
                        Int64 _nAkwSrNo = 0;
                        DateTime dtpReview = Convert.ToDateTime(System.DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt"));
                        string OrderNumberPrefix = string.Empty;
                        Int64 OrderNumberID = 0;

                        if (dtAkw.Rows.Count > 0)
                        {
                            strUserName = Convert.ToString(dtAkw.Rows[0]["UserName"]);
                            strComment = Convert.ToString(dtAkw.Rows[0]["Comments"]);
                            strPatientNotes = Convert.ToString(dtAkw.Rows[0]["PatientNote"]);
                            _nAkwSrNo = Convert.ToInt64(dtAkw.Rows[0]["nAcknowledgeSrNo"]);
                            OrderNumberPrefix = Convert.ToString(dtAkw.Rows[0]["nOrderNumberPrefix"]);
                            OrderNumberID = Convert.ToInt64(dtAkw.Rows[0]["nOrderNumberID"]);
                        }

                        if (dtAkw != null) //added for memory management
                        {
                            dtAkw.Dispose();
                            dtAkw = null;
                        }

                        oReviewed.Dispose();
                        oReviewed = null;

                        strComment = GetNormalAckNotes(false);
                        strPatientNotes = GetNormalAckNotes(true);

                        gloEMRGeneralLibrary.gloEMRLab.gloEMRLabOrder oReviewed2 = new gloEMRGeneralLibrary.gloEMRLab.gloEMRLabOrder();
                        oReviewed2.Add_Acknowledgment(oUc_ResultSet.OrderID, OrderNumberPrefix, OrderNumberID, _LoginUserID, dtpReview, Convert.ToString(strComment).Trim(), Convert.ToString(strPatientNotes).Trim(), _nAkwSrNo);
                        oReviewed2.Dispose();  //added to solve Bug #70293:
                        oReviewed2 = null;
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Add, "Lab order acknowledged.", _patientID, oUc_ResultSet.OrderID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                        UpdateOrder(oUc_ResultSet.OrderID, true);
                        // 1006 OrderStatus number for Acknowledge.
                        UpdateManualOrderStatus("1006", Convert.ToString(oUc_ResultSet.OrderID));
                        SendPortalEmail();
                        RefershAllGrids();
                        if (tabControl1.SelectedIndex == 3)
                        {
                            oUc_ResultSet.ShowTestData();
                        }

                        dynamic mdi = this.MdiParent;
                        mdi.ShowTasks();

                        //}
                        //else
                        //{
                        //    MessageBox.Show("Please Set Normal Notes from Edit -> Orders & Results Setup.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    return;
                        //}

                    }
                    else
                    {
                        MessageBox.Show("There is no order selected for Acknowledgement", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Error in Acknowledgement Normal. " + ex.ToString(), false);
            }
        }

        #region "Patient Portal"
        /// <summary>
        /// Sent Email to Patient & All patient Representative on Order Acknowledge. 
        /// </summary>
        private void SendPortalEmail()
        {
            try
            {
                clsgloPatientPortalEmail oclsgloPatientPortalEmail = new clsgloPatientPortalEmail(_dataBaseConnectionString);
                if (oclsgloPatientPortalEmail.IsPatientPortalEnabled() == true)
                {
                    if (oclsgloPatientPortalEmail.IsNotifyLabAcknowledgement() == true)
                    {
                        DataTable dtValidPortalUser = null;
                        dtValidPortalUser = oclsgloPatientPortalEmail.ToCheckPatientRegisterOrNotOnPortal(_patientID);
                        oclsgloPatientPortalEmail.getPatientPortalSettings();
                        if (dtValidPortalUser != null && dtValidPortalUser.Rows.Count > 0)
                        {
                            string strFilepath = "";
                            clsGeneral objClsGeneral = new clsGeneral();
                            strFilepath = objClsGeneral.GenerateCDA(_patientID, _LoginUserID);
                            oclsgloPatientPortalEmail.SendPortalEmail(_patientID, oclsgloPatientPortalEmail.strPatientPortalEmailService, oclsgloPatientPortalEmail.strPatientPortalSiteNm, oclsgloPatientPortalEmail._ClinicID, "LAB RESULTS");
                            if (objClsGeneral != null) //added for memory management
                            {
                                objClsGeneral.Dispose();
                                objClsGeneral = null;
                            }
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       
        #endregion

        public bool IsDefaultNormalNoteExists()
        {
            bool isValid = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            string _IsValidNormalNotesPresent = string.Empty;
            try
            {
                oDB.Connect(false);
                //ProID = Trim(oDB.ExecuteScaler)
                _IsValidNormalNotesPresent = Convert.ToString(oDB.ExecuteScalar_Query("IF EXISTS ( SELECT  * FROM  Lab_AckNormalNotes ) BEGIN IF EXISTS ( SELECT  * FROM Lab_AckNormalNotes WHERE labAckInternalNotes_ID = 0 OR labAckPatientNotes_ID = 0 ) BEGIN SELECT  'FALSE' END ELSE BEGIN SELECT  'TRUE' END END ELSE SELECT  'FALSE'"));
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                _IsValidNormalNotesPresent = "";

            }
            finally
            {
                oDB.Dispose();
            }
            if (Convert.ToString(_IsValidNormalNotesPresent) != "" && Convert.ToString(_IsValidNormalNotesPresent).ToLower() == "true")
            {
                isValid = true;
            }
            return isValid;
        }

        public bool UpdateOrder(Int64 nOrderID, bool IsTrue = true)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            string strQry = string.Empty;
            int _Result = 0;
            Boolean _boolResult = false;
            try
            {
                if (nOrderID != 0)
                {
                    oDB.Connect(false);
                    if (IsTrue == true)
                    {
                        strQry = "update Lab_Order_MST set bIsClosed=1 where labom_OrderID='" + nOrderID + "' ";
                    }
                    else
                    {
                        strQry = "update Lab_Order_MST set bIsClosed=0 where labom_OrderID='" + nOrderID + "' ";
                    }
                    _Result = oDB.Execute_Query(strQry);
                    if (_Result < 0)
                    {
                        _boolResult = false;
                    }
                    else if (_Result >= 0)
                    {
                        _boolResult = true;
                    }
                    else
                    {
                        _boolResult = false;
                    }
                    oDB.Disconnect();
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                _boolResult = false;
            }
            finally
            {
                oDB.Dispose();
            }
            return _boolResult;
        }

        private string GetNormalAckNotes(bool isPatientNote = false)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            string strReturn = string.Empty;

            try
            {
                oDB.Connect(false);
                //ProID = Trim(oDB.ExecuteScaler)
                if (isPatientNote == false)
                {
                    strReturn = Convert.ToString(oDB.ExecuteScalar_Query("SELECT ISNULL(labAckNotes,'') AS labAckNotes FROM Lab_AckNotes_MST  WHERE labAckNotes_ID IN (SELECT TOP 1 labAckInternalNotes_ID FROM Lab_AckNormalNotes)"));
                }
                else
                {
                    strReturn = Convert.ToString(oDB.ExecuteScalar_Query("SELECT ISNULL(labAckNotes,'') AS labAckNotes FROM Lab_AckNotes_MST  WHERE labAckNotes_ID IN (SELECT TOP 1 labAckPatientNotes_ID FROM Lab_AckNormalNotes)"));
                }
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                strReturn = "";
            }
            finally
            {
                oDB.Dispose();
            }
            return strReturn;
        }


        public string GetlabTabSetting()
        {
            string strTab = "";
            string strQuery = "";
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            try
            {
                oDB.Connect(false);
                strQuery = "SELECT sSettingsValue FROM dbo.Settings WHERE sSettingsName='DEFAULTLABTAB' AND nClinicID=" + _ClinicID;
                strTab = Convert.ToString(oDB.ExecuteScalar_Query(strQuery));
                oDB.Disconnect();
            }
            catch (Exception exc)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Error in loading view lab form :" + exc.ToString(), false);
            }
            finally
            {
                if (oDB == null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
            }
            return strTab;
        }

        private void gloLabUC_transaction1_gUC_InfobuttonClickedDB(string lCode)
        {
            string sAgeUnit = "";
            string sAgeValue = "";
            gloUserControlLibrary.AgeDetail ageDetail = gloUC_PatientStrip1.PatientAge;

            string strSql = "select sLang as Lang from Patient where nPatientID  =" + _patientID;
            DataTable dtPatinfo = new DataTable();
            SqlConnection con = new SqlConnection(_dataBaseConnectionString);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(strSql, con);
            da.Fill(dtPatinfo);
            string pLang = "";

            if (dtPatinfo != null)
            {
                pLang = Convert.ToString(dtPatinfo.Rows[0]["Lang"]);
            }
            gloEMRGeneralLibrary.clsInfobutton clsinfobutton_Orders = new gloEMRGeneralLibrary.clsInfobutton();
            
            if (ageDetail.Years != 0)
            {
                sAgeUnit = "a";
                sAgeValue = Convert.ToString(ageDetail.Years);
            }
            else if (ageDetail.Months != 0)
            {
                sAgeUnit = "a";
                sAgeValue = Convert.ToString(ageDetail.Years);
            }
            else if (ageDetail.Days != 0)
            {
                sAgeUnit = "a";
                sAgeValue = Convert.ToString(ageDetail.Years);
            }

            if (clsinfobutton_Orders != null)
            {

                //Bug #60825: 00000587 : creating an emdeon order the system is later updating the nVisitID to '0'                
                //clsinfobutton_Orders.Openinfosource(lCode, "2.16.840.1.113883.6.1", pLang, _patientID, gloEMRGeneralLibrary.clsInfobutton.enumSource.Orders.GetHashCode(), GenerateVisitID(System.DateTime.Now), sAgeValue, sAgeUnit, gloUC_PatientStrip1.PatientGender, _LoginProviderId);
                clsinfobutton_Orders.GetEducationMaterial_OpenInfobutton(false, gloUC_PatientStrip1.PatientGender, false, sAgeUnit, sAgeValue, pLang, lCode, "2.16.840.1.113883.6.1", "", "Provider", _LoginProviderId, _patientID, GetVisitID(System.DateTime.Now, _patientID), this);
                clsinfobutton_Orders = null;
            }
            if (dtPatinfo != null) //added for memory management
            {
                dtPatinfo.Dispose();
                dtPatinfo = null;
            }
            con.Close();
            con.Dispose();
            con = null;
            da.Dispose();
            da = null;

        }
        private void gloLabUC_transaction1_gUC_InfoButtonDocumentClickedDB(string rCode, string openFor, string TemplateName, string _sResourceType)
        {


            EntOpenEducation(Convert.ToInt64(rCode), _patientID, TemplateName, gloEMRGeneralLibrary.clsInfobutton.enumSource.Orders, openFor, _sResourceType);

        }

        private void UpdateResultFlag()
        {
            gloDatabaseLayer.DBLayer oDBObj = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParas = new gloDatabaseLayer.DBParameters();
            try
            {
                if (gloEMRGeneralLibrary.gloGeneral.clsgeneral.gstrSpecificResultRange.Trim() == "1")
                {
                    oDBObj.Connect(false);
                    oDBParas.Add(new gloDatabaseLayer.DBParameter("@nPatientID", _patientID, ParameterDirection.Input, SqlDbType.BigInt));
                    oDBObj.ExecuteScalar("Lab_UpdateResultFlag", oDBParas);
                    oDBObj.Disconnect();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Error While result flag update with Patient specific result range :" + ex.ToString(), false);

            }
            finally
            {
                if (oDBObj != null)
                {
                    oDBObj.Disconnect();
                    oDBObj.Dispose();
                    oDBObj = null;
                }
                if (oDBParas != null) //change made for memory optimization
                {
                    oDBParas.Clear();
                    oDBParas.Dispose();
                    oDBParas = null;
                }
            }
        }

        private bool GetOrderStatusReadyforReview(long lOrderID)
        {
            gloDatabaseLayer.DBLayer oDBObj = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParas = new gloDatabaseLayer.DBParameters();
            bool IsReady = false;
            try
            {
                oDBObj.Connect(false);
                oDBParas.Add(new gloDatabaseLayer.DBParameter("@OrderID", lOrderID, ParameterDirection.Input, SqlDbType.BigInt));
                IsReady = Convert.ToBoolean(oDBObj.ExecuteScalar("GetOrderStatusReadyforReview", oDBParas));
                oDBObj.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Error While Getting Order status ready for review :" + ex.ToString(), false);
                IsReady = false;
            }
            finally
            {
                if (oDBObj != null)
                {
                    oDBObj.Disconnect();
                    oDBObj.Dispose();
                    oDBObj = null;
                }

                if (oDBParas != null) //change made for memory optimization
                {
                    oDBParas.Clear();
                    oDBParas.Dispose();
                    oDBParas = null;
                }
            }
            return IsReady;
        }

        private bool AutoOrderStatusUpdateforReview(long lOrderID, string CurrentOrderStatusNumber)
        {
            bool IsSucceed = false;
            try
            {
                string strOrderStatus = Convert.ToString(CurrentOrderStatusNumber);
                if (strOrderStatus != Convert.ToString(ReadyForReview) && strOrderStatus != "")
                {
                    if (strOrderStatus == "1001" || strOrderStatus == "1005")
                    {
                        bool Isready = GetOrderStatusReadyforReview(lOrderID);
                        if (Isready == true)
                        {
                            UpdateManualOrderStatus(Convert.ToString(ReadyForReview), Convert.ToString(lOrderID));
                            IsSucceed = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Error during AutoOrderStatusUpdateforReview :" + ex.ToString(), false);
                IsSucceed = false;
            }
            return IsSucceed;
        }

        public bool CheckAcknoledgement(long OrderId, string OrederNumberPrifix, long OrderNumberID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            string strquery = string.Empty;
            Int64 count = 0;
            try
            {

                oDB.Connect(false);
                strquery = "Select bIsClosed from Lab_Order_MST where labom_OrderID =" + OrderId;
                count = Convert.ToInt64(oDB.ExecuteScalar_Query(strquery));
                if (count == 0)
                {
                    oDB.Disconnect();
                    return false;
                }
                strquery = "select count(*) FROM Lab_Acknowledgment Where nOrderId = '" + OrderId + "' and nOrderNumberPrefix = '" + OrederNumberPrifix + "' and nOrderNumberID = " + OrderNumberID;
                count = Convert.ToInt32(oDB.ExecuteScalar_Query(strquery));
                oDB.Disconnect();
                if (count > 0)
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
                gloAuditTrail.gloAuditTrail.ExceptionLog("Error in loading view lab form :" + ex.ToString(), false);
                return false;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
                strquery = string.Empty;
            }


        }

        private void ts_LabMain_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);

            try
            {
                //Resolved By : Mitesh Patel 
                //Bug Id: 32279  
                gloLabUC_Transaction1.EndEditTestGrid();
                if (e.ClickedItem.Name == tlbbtn_Close.Name) // Closing the form
                {
                    #region Removed by madan -- 20100504

                    #endregion
                    _blnClosed = true; //added by madan to identify weather the close button is clicked.
                    if (gloLabUC_Transaction1.LabModified || gloUCLab_OrderDetail.OrderModified)
                    {
                        DialogResult oResult = MessageBox.Show("Do you want to save your changes?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        if (oResult == DialogResult.Yes)
                        {
                            //if (!getProviderTaxID(_LabProviderID))
                            //{
                            //    return;
                            //} 

                            IsClosed = true;
                            SaveOrder(0);
                            
                            //gloGlobal.TIN.clsSelectProviderTaxID oclsselectProviderTaxID = new gloGlobal.TIN.clsSelectProviderTaxID(_LabProviderID);
                            //oclsselectProviderTaxID.InsertProviderTaxID(nProviderAssociationID, oLabActor_Order1.OrderID, sProviderTaxID, _LabProviderID, 0, gloGlobal.TIN.clsSelectProviderTaxID.TransactionType.OrderAndResult);
                            //oclsselectProviderTaxID = null;

                            gloUCLab_OrderDetail.OrderModified = false;
                            gloLabUC_Transaction1.LabModified = false;
                            this.Close();
                        }
                        else if (oResult == DialogResult.No)
                        {
                            if (_CurrentLockedOrder > 0)
                            {
                                if (UnLockOrder(_CurrentLockedOrder))
                                {
                                    _CurrentLockedOrder = 0;
                                }
                            }
                            gloUCLab_OrderDetail.OrderModified = false;
                            gloLabUC_Transaction1.LabModified = false;
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Close, "View Labs closed without saving", _patientID, 0, _PatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                            this.Close();
                        }
                        else if (oResult == DialogResult.Cancel)
                        {
                            gloUCLab_OrderDetail.OrderModified = true;
                            gloLabUC_Transaction1.LabModified = true;
                            _blnClosed = false;
                        }
                    }
                    else
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Close, "View labs closed", _patientID, 0, _PatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                        this.Close();
                    }

                }
                else if (e.ClickedItem.Name == tlbbtn_New.Name) // calling form for new Lab order
                {
                    Classes.clsGeneral objclsgeneral = new Classes.clsGeneral();

                    if (!clsEmdeonGeneral.CheckConnectionParameters(_dataBaseConnectionString))
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Open, "Unable to open view labs due to lab settings have not been configured in gloEMR Admin ", _patientID, 0, _PatientProviderID, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR);
                        MessageBox.Show("Lab Settings have not been configured in gloEMR Admin.\r\nPlease complete Lab Settings before ordering.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        objclsgeneral.Dispose();
                        objclsgeneral = null;
                        return;
                    }

                    if (objclsgeneral.IsInternetConnectionAvailable() == clsGeneral.InternetConnectivity.Success)
                    {

                        Int16 loopcnt = 0;

                        if (!compareProvider(_PatientProviderID))
                        {
                            objclsgeneral.Dispose();
                            objclsgeneral = null;
                            return;
                        }

                        //  clsgloLabPatientLayer objGloPatientLayer = new clsgloLabPatientLayer();
                        bool _billingStatus = objClsgloLabPatientLayer.CheckBillingType(objpatient);

                        if (_billingStatus == true)
                        {
                            string strQry = string.Empty;
                            Boolean boolPatientReg = false; // flag for patient registration                          
                            if (ConfirmNull(objpatient.DemographicsDetail.PatientCode.ToString()))
                            {
                                strQry = "SELECT COUNT(*) FROM PatientExternalCodes INNER JOIN Patient ON PatientExternalCodes.nPatientId = Patient.nPatientID  where PatientExternalCodes.sExternalType = 'EMDEON' AND    Patient.sPatientCode='" + objpatient.DemographicsDetail.PatientCode.ToString().Trim() + "'";
                            }
                            oDB.Connect(false);

                            for (loopcnt = 1; loopcnt <= 3; loopcnt++)
                            {
                                Int32 cnt = 0;
                                cnt = Convert.ToInt32(oDB.ExecuteScalar_Query(strQry));
                                if (cnt < 1) // if cnt is gretaer than zero means patient registered
                                {
                                    pnlregistration.Visible = true;
                                    pnlregistration.BringToFront();
                                    Application.DoEvents();

                                    gloEmdeonInterface.Classes.clsEmdeonGeneral.gloLab_patID = _patientID;

                                    boolPatientReg = objClsgloLabPatientLayer.RegisterGloPatient(objpatient, _dataBaseConnectionString);

                                    pnlregistration.Visible = false;

                                    if (boolPatientReg)
                                    {
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.RegisterLabPatient, "Patient is successfully registered in external lab service ", _patientID, 0, _PatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                                        break;
                                    }
                                }
                                else
                                {
                                    boolPatientReg = true;
                                    break;
                                }
                            }

                            if (boolPatientReg == true)  // if patient is registered
                            {
                                frmEmdeonInterface objfrmEmdonInterface = new frmEmdeonInterface(_patientID);
                                objfrmEmdonInterface.WindowState = FormWindowState.Maximized;
                                objfrmEmdonInterface.BringToFront(); // added by Abhijeet on 20100424
                                objfrmEmdonInterface.ShowDialog(this); // added 'this' parameter by by Abhijeet  on 20100424
                                objfrmEmdonInterface.Dispose();
                                objfrmEmdonInterface = null;
                                //fillOpenOrdsGrid_New();
                                gloUCLab_History_gUC_FillOrder(2);

                                cmbOrderStatus.SelectedItem = "No";
                                dtPickerToDate.Value = System.DateTime.Now;
                                dtPickerFromDate.Value = System.DateTime.Now.AddMonths(-12);

                                RefershAllGrids();

                                gloUC_TransactionHistory1.LoadPreviousLabs(_patientID, DateTime.Now);
                                gloUC_TransactionHistory1.DesignTestGrid();
                                gloUC_TransactionHistory1.HideCloseButton = false;
                                gloUC_TransactionHistory1.SetDataByDate(DateTime.Now.Date, DateTime.Now.Date);
                                gloUC_TransactionHistory1.cmbCriteria.Text = "Date";

                            }

                            else
                            {
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.RegisterLabPatient, "Patient is not registered in external lab service ", _patientID, 0, _PatientProviderID, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR);

                                if (ConfirmNull(clsEmdeonGeneral.gloLab_Identifier.ToString()))
                                {
                                    MessageBox.Show(clsEmdeonGeneral.gloLab_Identifier.ToString().Trim(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("There was a problem registering the patient with Lab service. Please try again later.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                        //  objGloPatientLayer.Dispose();
                        //  objGloPatientLayer = null;
                    }
                    else
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Open, "Unable to open external lab due to internet connection not available ", _patientID, 0, _PatientProviderID, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR);
                        MessageBox.Show("There was a Connection error. Internet connection not available.\r\n\r\n" +
                                            "You must be connected to the Internet to access Lab orders.", gstrMessageBoxCaption, MessageBoxButtons.OK);
                    }
                    objclsgeneral.Dispose();
                    objclsgeneral = null;
                }
                else if (e.ClickedItem.Name == tlbbtn_Refresh.Name) //refreshing the order details by getting latest glolab orders 
                {
                    if (gloLabUC_Transaction1.LabModified || gloUCLab_OrderDetail.OrderModified)
                    {
                        if (CheckStatusOfOrder() == false)
                        {
                            return;
                        }
                    }

                    clsGeneral objclsGeneral = new clsGeneral();
                    string strPerson = "";
                    string strPersonHSI = "";
                    if (!objclsGeneral.IsgloLabRegisteredPatient(_patientID, out  strPerson, out  strPersonHSI))
                    {
                        objclsGeneral.Dispose();
                        objclsGeneral = null;
                        return;
                    }
                    clsGetGloLabData objclsGetGloLabData = new clsGetGloLabData();
                    pnlregistration.Visible = true;
                    pnlregistration.BringToFront();
                    Application.DoEvents();

                    objclsGetGloLabData.GetAllLatestOrder(_patientID, _dataBaseConnectionString, Classes.clsGetGloLabData.RetrievePatientOrdersType.All);

                    pnlregistration.Visible = false; // Added by Abhijeet 20100401

                    //fillOpenOrdsGrid_New();
                    gloUCLab_History_gUC_FillOrder(2);
                    //Resolved By : Mitesh Patel 
                    //Bug ID:25741                   
                    //cmbOrderStatus.SelectedItem = "No";

                    dtPickerToDate.Value = System.DateTime.Now;
                    dtPickerFromDate.Value = System.DateTime.Now.AddMonths(-12);

                    RefershAllGrids();

                    gloUC_TransactionHistory1.LoadPreviousLabs(_patientID, DateTime.Now);
                    gloUC_TransactionHistory1.DesignTestGrid();
                    gloUC_TransactionHistory1.HideCloseButton = false;
                    gloUC_TransactionHistory1.SetDataByDate(DateTime.Now.Date, DateTime.Now.Date);
                    gloUC_TransactionHistory1.cmbCriteria.Text = "Date";


                    objclsGetGloLabData = null;
                    objclsGeneral.Dispose();
                    objclsGeneral = null;

                }
                ///Add code for save test order...
                else if (e.ClickedItem.Name == tlbbtn_Save.Name) //saveLab orders
                {
                    //if (!getProviderTaxID(_LabProviderID))
                    //{
                    //    return;
                    //}
                    _blnClosed = true;
                    SaveOrder(0);

                    //gloGlobal.TIN.clsSelectProviderTaxID oclsselectProviderTaxID = new gloGlobal.TIN.clsSelectProviderTaxID(_LabProviderID);
                    //oclsselectProviderTaxID.InsertProviderTaxID(nProviderAssociationID, oLabActor_Order1.OrderID, sProviderTaxID, _LabProviderID, 0, gloGlobal.TIN.clsSelectProviderTaxID.TransactionType.OrderAndResult);
                    //oclsselectProviderTaxID = null;
                    if (blnSaved == true)
                    {
                        this.Close();
                    }
                }
                else if (e.ClickedItem.Name == tlbbtn_Print.Name)//PrintOrders
                {
                    MenuEvent_Print();
                }
                else if (e.ClickedItem.Name == tlbbtn_Finish.Name)//Finish
                {

                    SaveOrder(1);
                    if (_OrderParamter.CloseAfterSave == true)
                    {
                        this.Close();
                    }

                }
                ////btn Acknowledgment is clicked
                else if (e.ClickedItem.Name == tlbbtn_Acknowledgment.Name)//Acknowledgement
                {

                    if (gloLabUC_Transaction1.LabModified || gloUCLab_OrderDetail.OrderModified)
                    {
                        if (CheckStatusOfOrder() == false)
                        {
                            return;
                        }
                    }
                    if (AddAcknowledgment())
                    {
                        //Added by madan on 20100511
                        RefershAllGrids();
                    }

                }
                else if (e.ClickedItem.Name == tlbbtn_VWAcknowledgment.Name)//Acknowledgement
                {
                    ViewAcknowledgment();

                }
                else if (e.ClickedItem.Name == tlbbtn_Fax.Name)
                {
                    MenuEvent_Fax();
                }
                else if (e.ClickedItem.Name == tlbbtn_ViewHistory.Name)
                {
                    // Problem : 00000197
                    // Description : when user modifies the lab and go to acknowledgement then after coming back from acknowledgement screen application ask for saving the lab screen changes.
                    // Reason for change :  Validation message should be popup before opening Acknowledgement form. If user modified the lab then first ask for save the changes and then go to acknowledgment screen

                    if (gloLabUC_Transaction1.LabModified || gloUCLab_OrderDetail.OrderModified)
                    {
                        if (CheckStatusOfOrder() == false)
                        {
                            return;
                        }
                    }

                    if (ViewAckHistory())
                    {
                        RefershAllGrids();
                        if (tabControl1.SelectedIndex == 3)
                        {
                            oUc_ResultSet.ShowTestData();
                        }

                        dynamic mdi = this.MdiParent;
                        mdi.ShowTasks();

                    }
                }
                else if (e.ClickedItem.Name == tlbbtn_ReviewAck.Name)
                {
                    ReviewAck();
                }
                else if (e.ClickedItem.Name == tlbbtnRefresh.Name)
                {

                    RefreshButtonClick(true);
                    //refresh grid. to refresh order status
                    RefershAllGrids();

                }
                else if (e.ClickedItem.Name == tlbbtn_SendtoLab.Name)
                {
                    if (_IsDemoLab == true)
                    {
                        return;
                    }

                    if (c1TestLibrary.RowSel > 0)
                    {
                        long curOrderID = Convert.ToInt64(c1TestLibrary.GetData(c1TestLibrary.RowSel, COL_ORDERID));

                        if (curOrderID > 0)
                        {
                            if (CheckLockStatus(curOrderID))
                            {
                                return;
                            }

                            if (UpdateOrderStatusBillingType(curOrderID))
                            {
                                RefershAllGrids();
                            }
                        }
                    }
                }
                else if (e.ClickedItem.Name == tlbbtn_HL7.Name)
                {
                    if (c1TestLibrary.RowSel > 0)
                    {
                        long curOrderID = Convert.ToInt64(c1TestLibrary.GetData(c1TestLibrary.RowSel, COL_ORDERID));
                        if (curOrderID > 0)
                        {
                            //start of added by manoj jadhav on 20140516 V8020 for changing order status "New" to Sent  
                            if (String.Compare(Convert.ToString(c1TestLibrary.GetData(c1TestLibrary.RowSel, COL_CUSTOMORDERSTATUS)), "New", true) == 0) //if order status is "New" then Change it to "Sent"
                            {
                                UpdateManualOrderStatus("1005", curOrderID.ToString());
                                c1TestLibrary.SetData(c1TestLibrary.RowSel, COL_CUSTOMORDERSTATUS, "Sent");
                            }
                            else //else order status is other than "New" then give warning message 
                            {
                                if (MessageBox.Show(this, "Order status is no longer ‘New’. This order may have already been sent." + Environment.NewLine + "Continue?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                                    return;
                            }
                            //end of added by manoj jadhav on 20140516 V8020 for changing order status "New" to Sent  

                            blnSaved = true;
                            SaveOrder(0);
                            if (blnSaved == false)
                            {
                                return;
                            }

                            if (!ValidateLabTestsForOrderTypes(curOrderID))
                            {
                                return;
                            }

                            gloLabUC_Transaction1.GetData();

                            if (MenuEvent_HL7("001", _patientID, curOrderID, 0, gloLabUC_Transaction1.arrTestNames))
                                MessageBox.Show(this, "Lab saved and outbound request sent to HL7 service.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else if (e.ClickedItem.Name == tlbbtn_AssignTask.Name)
                {
                    long _TaskId = 0;
                    clsGeneral objClsGeneral = new clsGeneral();
                    ///New Code Need to pass the test list collection
                    objClsGeneral.TestList = "Place Lab Order : For Any test you Want";
                    objClsGeneral.TestlistOnly = "";
                    //New Code
                    _TaskId = objClsGeneral.AssignTaskToUser(_patientID, _PatientProviderID, _LoginProviderId, 0, gloTaskMail.TaskType.PlaceLabOrder);
                    if (_TaskId > 0)
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Task, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.LabOrderRequest, "Task assigned for placing lab order", _patientID, 0, _LoginProviderId, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                    }
                    objClsGeneral.Dispose();
                }
                else if (e.ClickedItem.Name == tlbbtn_RecordResults.Name)
                {
                    if (gloLabUC_Transaction1.LabModified || gloUCLab_OrderDetail.OrderModified)
                    {

                        DialogResult drResult = MessageBox.Show("Do you want to save your changes?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                        if (drResult == DialogResult.Yes)
                        {
                            _blnClosed = true;
                            IsClosed = true;
                            SaveOrder(0);

                            gloUCLab_OrderDetail.OrderModified = false;
                            gloLabUC_Transaction1.LabModified = false;
                        }
                        else if (drResult == DialogResult.No)
                        {
                            if (_CurrentLockedOrder > 0)
                            {
                                if (UnLockOrder(_CurrentLockedOrder))
                                {
                                    _CurrentLockedOrder = 0;
                                }
                            }

                            gloUCLab_OrderDetail.OrderModified = false;
                            gloLabUC_Transaction1.LabModified = false;
                        }
                        else if (drResult == DialogResult.Cancel)
                        {
                            gloUCLab_OrderDetail.OrderModified = true;
                            gloLabUC_Transaction1.LabModified = true;
                            return;
                        }
                    }

                    frmViewNormalLab frmNormalLab = new frmViewNormalLab(_patientID);
                    frmNormalLab.Event_CallCDA += Raise_EvntGenerateCDA_GloLab;
                    frmNormalLab.EvntOpenPlanOfTreatment += OpenPlanOfTreatments_OrderEntry;
                    frmNormalLab._MdiParent = this.MdiParent;
                    frmNormalLab.WindowState = FormWindowState.Maximized;
                    frmNormalLab.DirectAddress = DirectAddress;
                    frmNormalLab.SecureMsgEnable = SecureMsgEnable;
                    frmNormalLab.SecureMsgUserright = SecureMsgUserright;

                    frmNormalLab.ShowInTaskbar = false;
                    frmNormalLab.BringToFront();
                    frmNormalLab.objPatientExam = objgloLabPatientExam;
                    //Incident #55971: 00017037:Patient Context issue
                    //Change patient provider from Lab not refresh provider combo box if open Dashboard as File>>Dashboard without closing lab main form.
                    //Attach event to delegate call when change provider is done.
                    frmNormalLab.OnProviderChanged += new frmViewNormalLab.ProviderChanged(onProviderChange);
                    frmNormalLab.objPatientMessages = objgloLabPatientMessages;
                    frmNormalLab.objPatientLetters = objgloLabPatientLetters;
                    frmNormalLab.objNurseNotes = objgloLabNurseNotes;
                    frmNormalLab.objHistory = objgloLabHistory;
                    frmNormalLab.objLabs = objgloLabLabs;
                    frmNormalLab.objDMS = objgloLabDMS;
                    frmNormalLab.objRxmed = objgloLabRxmed;
                    frmNormalLab.objOrders = objgloLabOrders;
                    frmNormalLab.objProblemList = objgloLabProblemList;

                    frmNormalLab.objCriteria = objgloLabCriteria;
                    frmNormalLab.objWord = objgloLabWord;
                    frmNormalLab.eventLabEducation = getPatientedu;
                    //Bug #80137: gloEMR: Scan Docs- Null reference exception on acknowledge
                    frmNormalLab.dMdi = this.MdiParent;
                    frmNormalLab.ShowDialog(this);
                    frmNormalLab.OnProviderChanged -= new frmViewNormalLab.ProviderChanged(onProviderChange);
                    frmNormalLab.Event_CallCDA -= Raise_EvntGenerateCDA_GloLab;
                    frmNormalLab.Dispose();

                    UpdateResultFlag();

                    //ShowOrders(LabOrderParameter.OrderID);

                    //cmbOrderStatus.SelectedItem = "No";
                    CallRefreshGrid = false;
                    dtPickerToDate.Value = System.DateTime.Now;
                    dtPickerFromDate.Value = System.DateTime.Now.AddMonths(-12);
                    CallRefreshGrid = true;
                    RefershAllGrids();
                    gloUC_TransactionHistory1.LoadPreviousLabs(_patientID, DateTime.Now);
                    gloUC_TransactionHistory1.DesignTestGrid();
                    gloUC_TransactionHistory1.HideCloseButton = false;
                    gloUC_TransactionHistory1.SetDataByDate(DateTime.Now.Date, DateTime.Now.Date);
                    gloUC_TransactionHistory1.cmbCriteria.Text = "Date";
                    tabControl1.SelectTab(0);

                }
                else if (e.ClickedItem.Name == tlbbtn_LabOrder.Name)
                {
                    if (gloLabUC_Transaction1.LabModified || gloUCLab_OrderDetail.OrderModified)
                    {
                        if (CheckStatusOfOrder() == false)
                        {
                            return;
                        }
                    }

                    if (_IsDemoLab == true)
                    {
                        LaunchDemoLab(_patientID);

                        gloUCLab_History_gUC_FillOrder(2);
                        cmbOrderStatus.SelectedItem = "No";
                        dtPickerToDate.Value = System.DateTime.Now;
                        dtPickerFromDate.Value = System.DateTime.Now.AddMonths(-12);
                        RefershAllGrids();
                        gloUC_TransactionHistory1.LoadPreviousLabs(_patientID, DateTime.Now);
                        gloUC_TransactionHistory1.DesignTestGrid();
                        gloUC_TransactionHistory1.HideCloseButton = false;
                        gloUC_TransactionHistory1.SetDataByDate(DateTime.Now.Date, DateTime.Now.Date);
                        gloUC_TransactionHistory1.cmbCriteria.Text = "Date";
                    }
                    else
                    {
                        LaunchEmdeonScreen();
                    }
                    tabControl1.SelectTab(0);
                }
                else if (e.ClickedItem.Name == tlbbtn_EditOrder.Name)
                {

                    if (gloLabUC_Transaction1.LabModified || gloUCLab_OrderDetail.OrderModified)
                    {
                        if (CheckStatusOfOrder() == false)
                        {
                            return;
                        }
                    }

                    Int64 _curOrderID = 0;
                    if (c1TestLibrary.RowSel > 0)
                    {
                        _curOrderID = Convert.ToInt64(c1TestLibrary.GetData(c1TestLibrary.RowSel, COL_ORDERID));
                    }
                    if (_curOrderID > 0)
                    {
                        if (CheckLockStatus(_curOrderID))
                        {
                            return;
                        }

                        if (GetOrderType(_curOrderID) == "Emr")
                        {
                            Set_Menu_ModifyOrder(sender, e);
                            //Commneted by Mayuri:20140521-to refresh grid from modify context menu also,so added in Set_Menu_ModifyOrder()
                            //11-Jun-13 Aniket: Refresh header in modify mode
                            //RefershAllGrids();
                        }
                        else
                        {
                            if (_IsDemoLab == true)
                            {
                                return;
                            }

                            if (LaunchOrderEditScreen(_curOrderID))
                            {
                                //fillOpenOrdsGrid_New();
                                gloUCLab_History_gUC_FillOrder(2);
                                cmbOrderStatus.SelectedItem = "No";
                                dtPickerToDate.Value = System.DateTime.Now;
                                dtPickerFromDate.Value = System.DateTime.Now.AddMonths(-12);
                                RefershAllGrids();
                                gloUC_TransactionHistory1.LoadPreviousLabs(_patientID, DateTime.Now);
                                gloUC_TransactionHistory1.DesignTestGrid();
                                gloUC_TransactionHistory1.HideCloseButton = false;
                                gloUC_TransactionHistory1.SetDataByDate(DateTime.Now.Date, DateTime.Now.Date);
                                gloUC_TransactionHistory1.cmbCriteria.Text = "Date";
                            }
                        }
                    }
                }
                else if (e.ClickedItem.Name == tlbbtn_Requisition.Name)
                {
                    Set_Menu_PrintRequisition(sender, e);
                }

                else if (e.ClickedItem.Name == tblCCD.Name)
                {
                    if (EvntGenerateCCDHandler != null)
                        EvntGenerateCCDHandler(_patientID);
                }
                else if (e.ClickedItem.Name == tblCDA.Name)
                {
                    Int64 _curOrderID = 0;
                    String _sDetails = "Order";
                    Int64 _contactID = 0;
                    Int64 _orderingProviderID = 0;
                    if (c1TestLibrary.RowSel > 0)
                    {
                        _curOrderID = Convert.ToInt64(c1TestLibrary.GetData(c1TestLibrary.RowSel, COL_ORDERID));
                        _sDetails = Convert.ToString(c1TestLibrary.GetData(c1TestLibrary.RowSel, COL_TRANSDATE)) + " - ORD " + Convert.ToString(c1TestLibrary.GetData(c1TestLibrary.RowSel, COL_ORDERNO));
                        _orderingProviderID = Convert.ToInt64(c1TestLibrary.GetData(c1TestLibrary.RowSel, COL_ORDERING_PROVIDERID));
                    }
                    _contactID = Convert.ToInt64(gloUCLab_OrderDetail.ReferredToID);

                    if (_orderingProviderID > 0)
                    {
                        gloSurescriptSecureMessage.SecureMessage.SetPreferredProvider(_orderingProviderID);

                    }

                    //08-Nov-17 Aniket: Resolving Bug #110215: nothing happens when clicked on "Gen CDA" button in "Orders & Results"
                    if (EvntGenerateCDAHandler != null)
                    { EvntGenerateCDAHandler(_curOrderID, _sDetails, _contactID, true, _patientID); }

                }
                
                else if (e.ClickedItem.Name == tlbbtn_OnlySave.Name) //saveLab orders
                {
                    //if (!getProviderTaxID(_LabProviderID))
                    //{
                    //    return;
                    //} 
                    SaveOrder(0);
                    //gloGlobal.TIN.clsSelectProviderTaxID oclsselectProviderTaxID = new gloGlobal.TIN.clsSelectProviderTaxID(_LabProviderID);
                    //oclsselectProviderTaxID.InsertProviderTaxID(nProviderAssociationID, oLabActor_Order1.OrderID, sProviderTaxID, _LabProviderID, 0, gloGlobal.TIN.clsSelectProviderTaxID.TransactionType.OrderAndResult);
                    //oclsselectProviderTaxID = null;
                    gloUCLab_OrderDetail.OrderModified = false;
                }
                
                else if (e.ClickedItem.Name == tlbResultRange.Name)//Added by madan for result range.
                {
                    frmPatientSpecificTest ofrmPatientSpecificTest = new frmPatientSpecificTest(_dataBaseConnectionString, _patientID);
                    ofrmPatientSpecificTest.ShowDialog(this);
                    ofrmPatientSpecificTest.Dispose();
                    RefreshButtonClick(false);
                }

                else if (e.ClickedItem.Name == tlbbtn_RefLetter.Name)
                {
                    OpenReferralLetters();
                }
                else if (e.ClickedItem.Name == tlbbtn_Message.Name)
                {
                    OpenMessage();
                }
                else if (e.ClickedItem.Name == tlbbtn_PatientLetter.Name)
                {
                    OpenPatientLetters();
                }
                else if (e.ClickedItem.Name == tlbBtnMergeOrder.Name)
                {
                    Int64 _curOrderID = 0;
                    if (c1TestLibrary.RowSel > 0)
                    {
                        _curOrderID = Convert.ToInt64(c1TestLibrary.GetData(c1TestLibrary.RowSel, COL_ORDERID));
                        //   _sDetails = Convert.ToString(c1TestLibrary.GetData(c1TestLibrary.RowSel, COL_TRANSDATE)) + " - ORD " + Convert.ToString(c1TestLibrary.GetData(c1TestLibrary.RowSel, COL_ORDERNO));
                    }

                    frmMergeOrder ofrmMergeOrder = new frmMergeOrder(_dataBaseConnectionString, _patientID, _curOrderID);
                    ofrmMergeOrder.ShowDialog(this);
                    ofrmMergeOrder.Dispose();
                    ofrmMergeOrder = null;
                    RefershAllGrids();
                }
                else if (e.ClickedItem.Name == tlbBtnClinicalChart.Name)
                {
                    OpenClinicalCharts(_patientID);
                    RefershAllGrids();
                }
                else if (e.ClickedItem.Name == tlbBtnPlanofTreatment.Name)
                {
                    OpenPlanOfTreatments();
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                pnlregistration.Visible = false;
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
            }
        }

        //public bool getProviderTaxID(Int64 nProviderID = 0)
        //{
        //    sProviderTaxID = "";
        //    nProviderAssociationID = 0;
        //    try
        //    {
        //        DialogResult oResult = System.Windows.Forms.DialogResult.OK;
        //        gloGlobal.frmSelectProviderTaxID oForm = new gloGlobal.frmSelectProviderTaxID(Convert.ToInt64(nProviderID));
        //        if (oForm.dtProviderTaxIDs != null && oForm.dtProviderTaxIDs.Rows.Count > 1)
        //        {
        //            oForm.ShowDialog(this);
        //            oResult = oForm.DialogResult;
        //            nProviderAssociationID = oForm.nAssociationID;
        //            sProviderTaxID = oForm.sProviderTaxID;

        //            oForm = null;
        //        }
        //        else if (oForm.dtProviderTaxIDs != null && oForm.dtProviderTaxIDs.Rows.Count == 1)
        //        {
        //            //'oResult = oForm.DialogResult
        //            nProviderAssociationID = Convert.ToInt64(oForm.dtProviderTaxIDs.Rows[0]["nAssociationID"]);
        //            sProviderTaxID = Convert.ToString(oForm.dtProviderTaxIDs.Rows[0]["sTIN"]);
        //            oForm = null;
        //        }
        //        else
        //        {
        //            nProviderAssociationID = 0;
        //            sProviderTaxID = "";
        //        }

        //        if (oResult == System.Windows.Forms.DialogResult.OK)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //        return false;

        //    }
        //    finally
        //    {
        //    }
        //}

        private bool ValidateLabTestsForOrderTypes(Int64 nOrderID)
        {
            bool bSuccess = false;
            gloDatabaseLayer.DBLayer oDBLayer = null;
            string sSQL = null;
            DataTable dtData = null;
            ArrayList arrOrderTypes=null;
            string sOrderType = null;

            try
            {
                oDBLayer = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);

                sSQL = "SELECT DISTINCT ISNULL(sMUReportingCategory, '') AS sMUReportingCategory FROM Lab_Test_Mst INNER JOIN Lab_Order_TestDtl ON Lab_Test_Mst.labtm_ID = Lab_Order_TestDtl.labotd_TestID WHERE labotd_OrderID = " + nOrderID;

                dtData = new DataTable();

                oDBLayer.Connect(false);
                oDBLayer.Retrive_Query(sSQL,out dtData);
                oDBLayer.Disconnect();

                if (dtData != null && dtData.Rows.Count > 0)
                {
                    arrOrderTypes = new ArrayList();

                    for (Int32 i = 0; i < dtData.Rows.Count; i++)
                    {
                        sOrderType = null;
                        sOrderType = Convert.ToString(dtData.Rows[i]["sMUReportingCategory"]);

                        if (string.IsNullOrEmpty(sOrderType))
                        {
                            sOrderType = "Lab";
                        }

                        arrOrderTypes.Add(sOrderType);
                    }
                }

                if (arrOrderTypes != null && arrOrderTypes.Count > 0)
                {
                    if (arrOrderTypes.Count == 1 && (arrOrderTypes.Contains("Lab") || arrOrderTypes.Contains("Radiology/Imaging")))
                    {
                        bSuccess = true;
                        return bSuccess;
                    }

                    if (arrOrderTypes.Count > 1 && (arrOrderTypes.Contains("Lab") || arrOrderTypes.Contains("Radiology/Imaging")))
                    {
                        if (MessageBox.Show("Your order contains different order type test(s)." + Environment.NewLine + "Are you sure you want to send the order?" + Environment.NewLine + "If you plan on sending orders to different labs please split the order and send the order to respective lab.", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                        {
                            bSuccess = true;
                            return bSuccess;
                        }
                    }

                    if (arrOrderTypes.Count > 0 && !arrOrderTypes.Contains("Lab") && !arrOrderTypes.Contains("Radiology/Imaging"))
                    {
                        MessageBox.Show("Message can’t be generated for Referrals and Other tests.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                        bSuccess = false;
                        return bSuccess;
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                sSQL = null;
                arrOrderTypes = null;
                sOrderType = null;

                if (dtData != null)
                {
                    dtData.Dispose();
                    dtData = null;
                }

                if (oDBLayer != null)
                {
                    oDBLayer.Dispose();
                    oDBLayer = null;
                }
            }

            return bSuccess;
        }

        public void getPatientedu(Int64 nTemplateID, Int64 nPatientID, string sTemplateName, gloEMRGeneralLibrary.clsInfobutton.enumSource oType, string OpenFor, string sResourceType)
        {
            EntOpenEducation(nTemplateID, nPatientID, sTemplateName, oType, OpenFor, sResourceType);
        }
        //Incident #55971: 00017037:Patient Context issue
        //Change patient provider from Lab not refresh provider combo box if open Dashboard as File>>Dashboard without closing lab main form.
        //Add method to call when event is occour.
        public void onProviderChange(Int64 _newProviderID)
        {
            //this event get _newProviderID value from frmViewNormalLab.
            //Pass changed patient provider (_newProviderID) to Dashboard.
            OnProviderChanged(_newProviderID);
        }

        private void RefreshButtonClick(bool ShowAcknowledgementButton = true)
        {
            string _strOrderType = "";
            long curOrderID = 0;
            try
            {
                gloUCLab_TestDetail.Visible = false;
                if (gloLabUC_Transaction1.LabModified || gloUCLab_OrderDetail.OrderModified)
                {
                    if (_CurrentLockedOrder > 0)
                    {
                        if (UnLockOrder(_CurrentLockedOrder))
                        {
                            _CurrentLockedOrder = 0;
                        }
                    }

                    DialogResult drResult = MessageBox.Show("Do you want to save your changes?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (drResult == DialogResult.Yes)
                    {
                        _blnClosed = true;
                        IsClosed = true;
                        SaveOrder(0);

                        gloLabUC_Transaction1.LabModified = false;
                        gloUCLab_OrderDetail.OrderModified = false;
                    }
                    else if (drResult == DialogResult.No)
                    {
                        gloLabUC_Transaction1.LabModified = false;
                        gloUCLab_OrderDetail.OrderModified = false;
                    }

                }

                UpdateResultFlag();

                if (c1TestLibrary.RowSel > 0)
                {

                    curOrderID = Convert.ToInt64(c1TestLibrary.GetData(c1TestLibrary.RowSel, COL_ORDERID));
                    if (curOrderID != 0)
                    {
                        if (tabControl1.SelectedIndex == 1 || tabControl1.SelectedIndex == 2 || tabControl1.SelectedIndex == 3)
                        {
                            gloUCLab_OrderDetail.Visible = false;
                        }
                        else
                        {
                            gloUCLab_OrderDetail.Visible = true;
                        }

                        ShowOrders(curOrderID);
                        SendtoLab();
                        _strOrderType = GetOrderType(curOrderID);
                        if (_strOrderType == "Emr")
                        {
                            tlbbtn_Finish.Enabled = true;
                            tlbbtn_Requisition.Enabled = false;
                            gloUCLab_OrderDetail.PreferredLabActivity(false);
                        }
                        else
                        {
                            tlbbtn_Requisition.Enabled = true;
                            tlbbtn_Finish.Enabled = false;
                            gloUCLab_OrderDetail.PreferredLabActivity(true);
                        }

                        //30-Aug-16 Aniket: Resolving Bug #99076: gloEMR : 8071 :Orders&Results : Application display Acknowledgement Button .
                        if (ShowAcknowledgementButton == true)
                        {
                            tlbbtn_ViewHistory.Visible = true;
                        }
                    }
                }
                else
                {
                    c1TestLibrary.Select(c1TestLibrary.Rows.Count - 1, 0);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                curOrderID = 0;
            }
        }

        private bool MenuEvent_HL7(string strMessageName, Int64 patientID, Int64 OrderId, Int32 intTotalRecords, ArrayList arrTestName)
        {
            bool boolFlag = false;
            try
            {
                if (gloEmdeonCommon.mdlGeneral.gblHL7SENDOUTBOUNDGLOEMRonLabModule == true)
                {
                    clsGeneral objclsGeneral = new clsGeneral();
                    boolFlag = objclsGeneral.InsertInMessageQueue("O01", patientID, OrderId, ref intTotalRecords, arrTestName);
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateO01, gloAuditTrail.ActivityType.Add, "Sending lab order to outbound HL7 ", _patientID, OrderId, _LoginProviderId, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                    objclsGeneral.Dispose();
                    objclsGeneral = null;
                }
                return boolFlag;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return boolFlag;
            }
        }

        private Boolean UpdateOrderStatusBillingType(long orderID)
        {// function added by Abhijeet on Date 20100331 for updating order status & order billing type

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            Boolean boolIsUpdated = false;
            try
            {
                oDB.Connect(false);
                // retrieving all order having status Entered 'E' Or null                
                string strGetOrders = "select labom_OrderID,labom_OrderNoID,labom_ExternalCode," +
                    "labom_dgloLabOrderID,labom_gloLabOrderStatus,labom_gloLabOrderBillingType from " +
                    "Lab_Order_MST where labom_OrderID=" + orderID.ToString();
                DataTable dtOrders = null;
                oDB.Retrive_Query(strGetOrders, out dtOrders);
                if (dtOrders != null && dtOrders.Rows.Count > 0)
                {
                    for (Int16 cnt = 0; cnt < dtOrders.Rows.Count; cnt++)
                    {
                        if (Convert.ToDouble(dtOrders.Rows[cnt]["labom_dgloLabOrderID"]) > 0)
                        {
                            clsEmdeonLabLayer objclsEmdeonLabLayer = new clsEmdeonLabLayer();
                            boolIsUpdated = objclsEmdeonLabLayer.OrderStatusUpdate(dtOrders.Rows[cnt]["labom_dgloLabOrderID"].ToString(), clsGeneral.OrderStatus.ReadyToTransmit);
                            if (boolIsUpdated)
                            {
                                string strUpdateQry = "update Lab_order_mst set labom_gloLabOrderStatus=12 " +
                                    " where labom_OrderID=" + dtOrders.Rows[cnt]["labom_OrderID"].ToString();
                                oDB.Execute_Query(strUpdateQry);

                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Modify, "Order Status and billing type are successfully modified in external lab service", _patientID, orderID, _PatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                            }
                            else
                            {
                                //by Abhijeet on 20100512                
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Modify, "Fail to modify Order Status and billing type in external lab service", _patientID, orderID, _PatientProviderID, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR);
                            }

                            objclsEmdeonLabLayer = null;

                        }
                    }
                }
                if (dtOrders != null) //added for memory management
                {
                    dtOrders.Dispose();
                    dtOrders = null;
                }
                oDB.Disconnect();
                return boolIsUpdated;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return false;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
        }

        private void MenuEvent_Fax()
        {
            try
            {
                if (tabControl1.SelectedIndex == 3)
                {
                    if (oUc_ResultSet.OrderID != 0)
                    {
                        if (CheckSSRSConfiguration())
                        {
                            _OrderParamter.IsEditMode = true;
                            gloEmdeonCommon.clsPrintFAX objPrintFAX = new gloEmdeonCommon.clsPrintFAX();
                            objPrintFAX.ConnectionString = _dataBaseConnectionString;
                            objPrintFAX.GetFaxSettings(_patientID, " ");
                            gloEMRLabOrder oLabOrderRequest = new gloEMRLabOrder();
                            oLabActor_Order1.ArrTestName = oLabOrderRequest.GetOrderTests(oUc_ResultSet.OrderID);
                            objPrintFAX.FaxLabOrder(oUc_ResultSet.OrderID, oLabActor_Order1.ArrTestName, _patientID);
                            _OrderParamter.CloseAfterSave = true;

                            if (objPrintFAX.blnFaxSentfromOrders == true)
                            {
                                ChangeOrderStatusNewtoSentForFax(oUc_ResultSet.OrderID);
                            }



                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Fax, "Lab orders fax", _patientID, _OrderParamter.OrderID, _PatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                            oLabOrderRequest.Dispose();
                            oLabOrderRequest = null;

                            objPrintFAX = null;
                        }
                    }
                }
                else
                {
                    _OrderParamter.CloseAfterSave = false;
                    blnSaved = true;
                    if (blnSaved == false)
                    {
                        return;
                    }
                    _OrderParamter.IsEditMode = true;

                    if (_OrderParamter.OrderID != 0)
                    {
                        if (CheckSSRSConfiguration())
                        {
                            gloEmdeonCommon.clsPrintFAX objPrintFAX = new gloEmdeonCommon.clsPrintFAX();
                            objPrintFAX.ConnectionString = _dataBaseConnectionString;
                            objPrintFAX.GetFaxSettings(_patientID, " ");
                            objPrintFAX.FaxLabOrder(_OrderParamter.OrderID, oLabActor_Order1.ArrTestName, _patientID);
                            _OrderParamter.CloseAfterSave = true;

                            if (objPrintFAX.blnFaxSentfromOrders == true)
                            {
                                ChangeOrderStatusNewtoSentForFax(_OrderParamter.OrderID);
                            }

                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Fax, "Lab orders fax", _patientID, _OrderParamter.OrderID, _PatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                            objPrintFAX = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void ChangeOrderStatusNewtoSentForFax(long OrderID)
        {
            if (GetCurrentOrderOrderStatusNumber(OrderID) == 1001)
            {
                if (MessageBox.Show("You Faxed the Order Report. Do you want to set the Order Status to 'Sent'?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    UpdateManualOrderStatus("1005", OrderID.ToString());

                    if (tabControl1.SelectedIndex == 0)
                    {
                        c1TestLibrary.SetData(c1TestLibrary.RowSel, COL_CUSTOMORDERSTATUS, "Sent");
                    }
                }
            }
        }

        private int GetCurrentOrderOrderStatusNumber(long OrderID)
        {
            string strQuery = string.Empty;
            DataTable dtResult = null;
            int iOrderStatus = 0;

            gloDatabaseLayer.DBLayer objDbLayer = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            clsGeneral objClsGeneral = new clsGeneral();
            try
            {
                objDbLayer.Connect(false);
                strQuery = " SELECT isnull(OrderStatusNumber,0) as OrderStatusNumber from Lab_Order_MST WHERE labom_OrderID = " + OrderID.ToString();

                objDbLayer.Retrive_Query(strQuery, out dtResult);

                if (dtResult != null && dtResult.Rows.Count > 0)
                {
                    iOrderStatus = (int)dtResult.Rows[0]["OrderStatusNumber"];
                }

                objDbLayer.Disconnect();
                return iOrderStatus;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return 0;
            }
            finally
            {
                if (objDbLayer != null) { objDbLayer.Dispose(); objDbLayer = null; }
                if (dtResult != null) { dtResult.Dispose(); dtResult = null; }
                if (objClsGeneral != null) { objClsGeneral.Dispose(); objClsGeneral = null; }
                strQuery = null;
            }
        }


        public void ViewAcknowledgment()
        {
            if (_OrderParamter.OrderID != 0)
            {
                frmLab_ViewAcknoledgement objviewAcw = new frmLab_ViewAcknoledgement(_OrderParamter.OrderID, _OrderParamter.OrderNumberPrefix, gloUCLab_OrderDetail.OrderNumberID, _dataBaseConnectionString);
                {
                    objviewAcw.ShowInTaskbar = false;
                    objviewAcw.PatientID = _patientID;
                    objviewAcw.ShowDialog(this);
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.View, "Lab order acknowledgement viewed", _patientID, _OrderParamter.OrderID, _PatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                    objviewAcw.Dispose();
                    objviewAcw = null;
                }
            }
        }
        public bool ViewAckHistory()
        {
            bool _blnReOpenOrder = true;
            try
            {
                if (tabControl1.SelectedIndex == 3)
                {
                    if (oUc_ResultSet.OrderID > 0)
                    {
                        frmLab_AcknoledgementHistory ObjAckHistory = new frmLab_AcknoledgementHistory(oUc_ResultSet.OrderID, _dataBaseConnectionString);

                        //ObjAckHistory.EntOpenMessage += new frmLab_AcknoledgementHistory.OpenMessages(OpenMessage);
                        //'Developer:Sanjog Dhamke
                        //'Date: 21 Dec 2011
                        //'PRD Name: Lab Usability (6060)
                        //'Reason: To Open the Patient Letter & Referral Letter
                        //ObjAckHistory.EvntOpenPatientLetter += new frmLab_AcknoledgementHistory.OpenPatientLetter(OpenPatientLetters);
                        //ObjAckHistory.EvntOpenReferralLetter+=new frmLab_AcknoledgementHistory.OpenReferralLetter(OpenReferralLetters);
                        ObjAckHistory._patientID = _patientID;
                        ObjAckHistory._LoginProviderId = _LoginProviderId;
                        ObjAckHistory._PatientProviderID = _PatientProviderID;
                        ObjAckHistory.ShowInTaskbar = false;
                        ObjAckHistory.BringToFront();
                        ObjAckHistory.ShowDialog(this);
                        ObjAckHistory.Dispose();
                        ObjAckHistory = null;
                    }
                    else
                    {
                        MessageBox.Show("There is no order selected for Acknowledgement", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                if (_OrderParamter.OrderID != 0 && tabControl1.SelectedIndex == 0)
                {
                    frmLab_AcknoledgementHistory ObjAckHistory = new frmLab_AcknoledgementHistory(_OrderParamter.OrderID, _dataBaseConnectionString);
                    {
                        //ObjAckHistory.EntOpenMessage += new frmLab_AcknoledgementHistory.OpenMessages(OpenMessage);
                        //'Developer:Sanjog Dhamke
                        //'Date: 21 Dec 2011
                        //'PRD Name: Lab Usability (6060)
                        //'Reason: To Open the Patient Letter & Referral Letter
                        //ObjAckHistory.EvntOpenPatientLetter += new frmLab_AcknoledgementHistory.OpenPatientLetter(OpenPatientLetters);
                        //ObjAckHistory.EvntOpenReferralLetter+=new frmLab_AcknoledgementHistory.OpenReferralLetter(OpenReferralLetters);
                        ObjAckHistory._patientID = _patientID;
                        ObjAckHistory._LoginProviderId = _LoginProviderId;
                        ObjAckHistory._PatientProviderID = _PatientProviderID;
                        ObjAckHistory.ShowInTaskbar = false;
                        ObjAckHistory.BringToFront();
                        ObjAckHistory.ShowDialog(this);
                    }
                    if (ObjAckHistory.IsReOpened)
                    {
                        _blnReOpenOrder = true;
                    }
                    ObjAckHistory.Dispose();
                    ObjAckHistory = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            return _blnReOpenOrder;

        }
        public bool AddAcknowledgment()
        {
            bool IsAcknowledged = false;
            if (_OrderParamter.OrderID != 0)
            {
                if (CheckLockStatus(_OrderParamter.OrderID))
                {
                    return false;
                }

                frmLab_Acknoledgement objAcw = new frmLab_Acknoledgement(_OrderParamter.OrderID, _OrderParamter.OrderNumberPrefix, gloUCLab_OrderDetail.OrderNumberID, true, _dataBaseConnectionString);
                {
                    objAcw.ShowInTaskbar = false;
                    objAcw.BringToFront();
                    objAcw.PatientID = _patientID;
                    if (objAcw.ShowDialog(this) == DialogResult.OK)
                    {
                        IsAcknowledged = objAcw.blnCommentsPlaced;
                        //tlbbtn_Acknowledgment.Visible = false;
                    }
                    objAcw.Dispose();
                    objAcw = null;

                }
            }
            return IsAcknowledged;
        }

        //Developer:Sanjog Dhamke
        //Date: 20 Dec 2011 (6060)
        //Bug ID/PRD Name/Sales force Case: PRD Lab Usability - To show Add,Modify & Deletion functionality for ACkw.
        //Reason: to call event handler

        public void OpenMessage()
        {
            //Developer:Mitesh Patel
            //Date:04 May 2012 
            //Problem # 100 
            EntOpenMessage();
        }
        //'Developer:Sanjog Dhamke
        //'Date: 21 Dec 2011
        //'PRD Name: Lab Usability (6060)
        //'Reason: To Open the Patient Letter & Referral Letter
        public void OpenPatientLetters()
        {
            //Developer:Mitesh Patel
            //Date:04 May 2012 
            //Problem # 100 
            EvntOpenPatientLetter();
        }

        public void OpenReferralLetters()
        {
            //Developer:Mitesh Patel
            //Date:04 May 2012 
            //Problem # 100 
            EvntOpenReferralLetter();
        }

        public void OpenPlanOfTreatments()
        {
            //Developer:Mitesh Patel
            //Date:04 May 2012 
            //Problem # 100 
            EvntOpenPlanOfTreatment(_patientID,"Lab");
        }


        public void OpenPlanOfTreatments_OrderEntry(Int64 PatientID, string CallingForm, TreeNode oNode = null, string TestType = "", string SearchText = "")
        {
            EvntOpenPlanOfTreatment(_patientID, "Lab", oNode, TestType, SearchText);
        }

        public void ReviewAck()
        {
            try
            {
                if (_OrderParamter.OrderID != 0)
                {
                    if (CheckLockStatus(_OrderParamter.OrderID))
                    {
                        return;
                    }

                    frmLab_Acknoledgement objAck = new frmLab_Acknoledgement(_OrderParamter.OrderID, _OrderParamter.OrderNumberPrefix, gloUCLab_OrderDetail.OrderNumberID, false, _dataBaseConnectionString);
                    {
                        objAck.ShowInTaskbar = false;
                        objAck.PatientID = _patientID;
                        objAck.ShowDialog(this);
                        objAck.Dispose();

                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void Set_PatientDetailStrip()
        {
            try
            {
                this.Controls.Add(gloUC_PatientStrip1);
                gloUC_PatientStrip1.Padding = new Padding(3, 0, 3, 0);
                gloUC_PatientStrip1.DTPEnabled = false;
                pnlToolStrip.SendToBack();
                {
                    gloUC_PatientStrip1.Dock = DockStyle.Top;

                    gloUC_PatientStrip1.DTPValue = LabOrderParameter.TransactionDate;
                    gloUC_PatientStrip1.ShowDetail(_patientID, gloUserControlLibrary.gloUC_PatientStrip.enumFormName.LabOrder, 0, LabOrderParameter.VisitID, LabOrderParameter.ProviderID, false, false, false, "", false);
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        public void ShowOrders(long lngOrderID)
        {
            try
            {
                _OrderParamter.OrderID = lngOrderID;

                _OrderParamter.IsEditMode = true;
                _OrderParamter.OrderNumberID = 0;
                _OrderParamter.OrderNumberPrefix = "ORD";
                _OrderParamter.PatientID = _OrderParamter.PatientID;
                _OrderParamter.VisitID = _OrderParamter.VisitID;
                _OrderParamter.TransactionDate = _OrderParamter.TransactionDate;
                _OrderParamter.CloseAfterSave = true;

                LoadOrder();

                //20140407 manoj V8022 PRD: View Lab Order Comments :Added call to AfterSelChange Event Method
                if ((c1TestLibrary.RowSel > 0) && (!String.IsNullOrEmpty(Convert.ToString(c1TestLibrary.GetData(c1TestLibrary.RowSel, COL_ORDERCOMMENTS)))))
                {
                    gloLabUC_Transaction1.ShowLabOrderComments(c1TestLibrary.GetData(c1TestLibrary.RowSel, COL_ORDERCOMMENTS).ToString());
                }

                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Open, "Lab Request Orders opened to modify. ", _OrderParamter.PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        public Boolean LoadOrder()
        {
            string _OrderType = string.Empty;//Added for checking the order is from "emdeon" or "EMR"
            try
            {
                gloEMRGeneralLibrary.gloEMRLab.gloEMRLabOrder oLabOrderRequest = new gloEMRGeneralLibrary.gloEMRLab.gloEMRLabOrder();
                //gloEMRGeneralLibrary.gloEMRActors.LabActor.LabOrder oLabActor_Order = null;
                gloEMRGeneralLibrary.gloEMRActors.LabActor.LabOrder oLabActor_Order = new gloEMRGeneralLibrary.gloEMRActors.LabActor.LabOrder();


                gloUC_PatientStrip1.TransactionDate = oLabActor_Order.TransactionDate;
                oLabActor_Order.Dispose();
                oLabActor_Order = null;
                // DateTime _TempResultDateTime ;

                ////Sanjog

                if (chk_PreviousHistory.Checked == false && Convert.ToInt64(c1TestLibrary.GetData(c1TestLibrary.RowSel, COL_HasResult_Value)) > 0)// CheckResultsForOrder(_OrderParamter.OrderID) == true)
                {
                    string MaxResultDateTime = GetMaxResultsDateTime(_OrderParamter.OrderID);
                    if (MaxResultDateTime != "")
                    {
                        // _TempResultDateTime = Convert.ToDateTime(MaxResultDateTime);

                        oLabActor_Order = null;
                        oLabActor_Order = oLabOrderRequest.GetOrderByHistory_New(_OrderParamter.OrderID, Convert.ToDateTime(MaxResultDateTime), _OrderParamter.PatientID);
                    }
                    else
                    {
                        // _TempResultDateTime = null;
                        oLabActor_Order = null;
                        oLabActor_Order = oLabOrderRequest.GetOrderByHistory_New(_OrderParamter.OrderID, null, _OrderParamter.PatientID);
                    }


                    //oLabActor_Order = oLabOrderRequest.GetOrderByHistory(_OrderParamter.OrderID, _TempResultDateTime);
                }
                else
                {
                    oLabActor_Order = null;
                    oLabActor_Order = oLabOrderRequest.GetOrder_New(_OrderParamter.OrderID, _OrderParamter.PatientID, 1);
                }

                if ((oLabActor_Order != null))
                {
                    gloUCLab_Transaction.ClearTest();

                    {

                        //Developer:Sanjog Dhamke
                        //Date: 12/07/2011
                        //Bug ID/PRD Name/Sales force Case: SF Case = GLO2011-0015430 - Lab Results Not Displaying Properly
                        //Reason: OrderNoID is converted to int16 format but actual OrderNoID is bigger than this so it cause the exception and the lab result is not displaying properly. Now we make this as int64 data type.

                        gloUCLab_OrderDetail.SetData(oLabActor_Order.OrderNoPrefix, Convert.ToInt64(oLabActor_Order.OrderNoID), oLabActor_Order.PreferredLab, oLabActor_Order.ReferredBy, oLabActor_Order.SampledBy, null, oLabActor_Order.PreferredLabID, oLabActor_Order.ReferredByID, oLabActor_Order.SampledByID, "", oLabActor_Order.TaskDueDate, oLabActor_Order.ReferredToID, oLabActor_Order.ReferredTo, oLabActor_Order.SendTo);

                        _OrderParamter.ProviderID = oLabActor_Order.ProviderID;

                        _OrderParamter.LabProviderName = oLabActor_Order.Provider;

                        _LabProviderID = oLabActor_Order.ProviderID;
                        gloLabUC_Transaction1.ProviderID = _LabProviderID;
                        _OrderType = GetOrderType(_OrderParamter.OrderID);//Getting the type of the order.....
                        oLabActor_Order.OrderID = _OrderParamter.OrderID;
                        gloLabUC_Transaction1.ClearTest();
                        gloLabUC_Transaction1.gUC_InfoButtonClickedDB -= gloLabUC_transaction1_gUC_InfobuttonClickedDB;
                        gloLabUC_Transaction1.gUC_InfoButtonClickedDB += gloLabUC_transaction1_gUC_InfobuttonClickedDB;
                        gloLabUC_Transaction1.gUC_InfoButtonDocumentClickedDB -= gloLabUC_transaction1_gUC_InfoButtonDocumentClickedDB;
                        gloLabUC_Transaction1.gUC_InfoButtonDocumentClickedDB += gloLabUC_transaction1_gUC_InfoButtonDocumentClickedDB;
                        gloLabUC_Transaction1.SetData(oLabActor_Order, _OrderType);
                       // gloLabUC_Transaction1.Fill_Diagnosis_CPT();
                       // gloLabUC_Transaction1.LabModified = false;
                    }
                }
                if (oLabActor_Order != null)
                {

                    if (oLabActor_Order.OrderTests != null)
                    {
                        oLabActor_Order.OrderTests.Dispose();
                        oLabActor_Order.OrderTests = null;
                    }

                    oLabActor_Order.Dispose();
                    oLabActor_Order = null;
                }
                oLabOrderRequest.Dispose();
                oLabOrderRequest = null;

                return true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return false;
            }
        }

        private void UpdateManualOrderStatus(String intOrderStatus, String intOrderID)
        {
            gloDatabaseLayer.DBLayer oDBLayer = null;
            gloDatabaseLayer.DBParameters oDBparams = null;

            try
            {   //Create parameters for SP to update the Manual Order status.
                oDBparams = new gloDatabaseLayer.DBParameters();
                oDBparams.Add("@intOrderStatus", intOrderStatus, ParameterDirection.Input, SqlDbType.Int);
                oDBparams.Add("@intOrderID", intOrderID, ParameterDirection.Input, SqlDbType.BigInt);

                //Execution for the stored procedures
                oDBLayer = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
                oDBLayer.Connect(false);
                oDBLayer.Execute("gsp_UpdateOrderStatus", oDBparams);
                oDBLayer.Disconnect();

            }
            catch (Exception ex)
            {
                //Log if any exception;

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {
                //Cleanup for internal objects utilized in the method.

                if (oDBLayer != null)
                { oDBLayer.Dispose(); oDBLayer = null; }

                if (oDBparams != null)
                {
                    oDBparams.Clear();
                    oDBparams.Dispose(); oDBparams = null;

                }

            }

        }


        private void UpdateOrderProvider(String intProviderID, String intOrderID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            gloDatabaseLayer.DBParameters oDBparams = new gloDatabaseLayer.DBParameters();

            string strQuery = string.Empty;
            //DataTable _dtResult = new DataTable();

            try
            {
                strQuery = "gsp_UpdateOrderProvider";

                oDB.Connect(false);
                oDBparams.Clear();

                oDBparams.Add("@intProviderID", intProviderID, ParameterDirection.Input, SqlDbType.BigInt, 18);
                oDBparams.Add("@intOrderID", intOrderID, ParameterDirection.Input, SqlDbType.BigInt, 18);

                oDB.Execute(strQuery, oDBparams);

                oDB.Disconnect();
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
                }

                if (oDBparams != null) //dbparameter dispose memory management
                {
                    oDBparams.Clear();
                    oDBparams.Dispose();
                    oDBparams = null;
                }
            }

        }

        private DataTable GetProviders()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);

            string strQuery = string.Empty;
            DataTable _dtResult = null; // new DataTable();

            try
            {
                strQuery = "gsp_GetProviderDetails";

                oDB.Connect(false);

                oDB.Retrive_Query(strQuery, out _dtResult);

                oDB.Disconnect();
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

            }
            return _dtResult;
        }


        private DataTable GetOrderStatus()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);

            string strQuery = string.Empty;
            DataTable _dtResult = null; // new DataTable();

            try
            {
                strQuery = "gsp_GetOrderStatus";

                oDB.Connect(false);

                oDB.Retrive_Query(strQuery, out _dtResult);

                oDB.Disconnect();
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

            }
            return _dtResult;
        }

        private string GetOrderType(long _OrderID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            string _result = string.Empty;
            string strQuery = string.Empty;
            DataTable _dtResult = null;
            string _labom_dgloLabOrderID = string.Empty;
            string _labom_ExternalCode = string.Empty;

            try
            {
                strQuery = "select isnull(labom_ExternalCode,'') as labom_ExternalCode,labom_dgloLabOrderID from lab_order_Mst where labom_OrderID=" + _OrderID;

                oDB.Connect(false);

                oDB.Retrive_Query(strQuery, out _dtResult);

                if (_dtResult != null && _dtResult.Rows.Count > 0)
                {

                    string _LabOrdExternalCode = string.Empty;

                    if (Convert.ToString(_dtResult.Rows[0]["labom_ExternalCode"]).Trim() != ""
                       && _dtResult.Rows[0]["labom_dgloLabOrderID"] != DBNull.Value)
                    {
                        _result = "Emdeon";
                    }
                    else
                    {
                        _result = "Emr";
                    }
                }

                oDB.Disconnect();
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                _result = string.Empty;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
                if (_dtResult != null)
                {
                    _dtResult.Dispose();
                    _dtResult = null;
                }
                _labom_dgloLabOrderID = string.Empty;
                _labom_ExternalCode = string.Empty;
            }
            return _result;
        }

        //Problem : Bug #41352: 00000342 : Lab Orders 
        //Change : Check whether the order is came from HL7 or not. If yes then display the pop up message.
        private void CheckExternalOrder(long _OrderID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            string strQuery = string.Empty;
            DataTable _dtResult = null;

            try
            {
                strQuery = "select isnull(labom_ExternalCode,'') as labom_ExternalCode,labom_dgloLabOrderID from lab_order_Mst where labom_OrderID=" + _OrderID;

                oDB.Connect(false);

                oDB.Retrive_Query(strQuery, out _dtResult);

                if (_dtResult != null && _dtResult.Rows.Count > 0)
                {

                    string _LabOrdExternalCode = string.Empty;

                    if (!string.IsNullOrEmpty(Convert.ToString(_dtResult.Rows[0]["labom_ExternalCode"]))
                       && _dtResult.Rows[0]["labom_dgloLabOrderID"] == DBNull.Value)
                    {
                        MessageBox.Show("This is an electronic lab order and therefore cannot be edited.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                oDB.Disconnect();
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
                if (_dtResult != null)
                {
                    _dtResult.Dispose();
                    _dtResult = null;
                }
            }
        }

        private void gloUCLab_History_gUC_FillOrder(short CriteriaNumber)
        {
            try
            {
                gloEMRGeneralLibrary.gloEMRActors.LabActor.LabOrders oLabOrders = null;
                gloEMRGeneralLibrary.gloEMRLab.gloEMRLabOrder oLabOrder = new gloEMRGeneralLibrary.gloEMRLab.gloEMRLabOrder();

                oLabOrders = oLabOrder.GetOrders(_OrderParamter.PatientID, (gloEMRGeneralLibrary.gloEMRActors.enmHistoryCriteria)CriteriaNumber, false);
                if ((oLabOrders != null))
                {
                    gloUCLab_History.FillOrder(CriteriaNumber, oLabOrders);
                }
                oLabOrders.Clear();
                oLabOrders.Dispose();
                oLabOrders = null;
                oLabOrder.Dispose();
                oLabOrder = null;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void gloUCLab_History_gUC_OpenLabForModify(long OrderID)
        {
            if (OrderID != 0)
            {
                ShowOrders(OrderID);
            }

        }

        private void SendtoLab()
        {  // function  added by Abhijeet on date 20100331  
            // Also added the button 'Send to Lab'
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            try
            {
                if (c1TestLibrary.RowSel > 0)
                {
                    long curOrderID = Convert.ToInt64(c1TestLibrary.GetData(c1TestLibrary.RowSel, COL_ORDERID));
                    oDB.Connect(false);
                    string strGetOrders = "select count(*) from Lab_Order_MST where " +
                        " labom_orderID=" + curOrderID.ToString() + " and labom_gloLabOrderStatus=4";
                    Int32 cnt = Convert.ToInt32(oDB.ExecuteScalar_Query(strGetOrders));
                    if (cnt > 0 && tabControl1.SelectedIndex == 0) // added tabcontrol condition by Abhijeet 20100401
                        tlbbtn_SendtoLab.Enabled = true;
                    else
                        tlbbtn_SendtoLab.Enabled = false;
                }
                else
                    tlbbtn_SendtoLab.Enabled = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
            }
        }


        protected bool ConfirmNull(string strValue)
        {
            bool blnCheck = false;
            try
            {
                if (strValue != null && strValue.ToString().Trim().Length != 0 && strValue.ToString() != "")
                {
                    blnCheck = true;

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            return blnCheck;
        }
        private Int64 GetPrefixTransactionID(Int64 PatientID)
        {
            Int64 _Result = 0;
            string _result = "";
            DateTime _PatientDOB = DateTime.Now;
            DateTime _CurrentDate = DateTime.Now;
            DateTime _BaseDate = Convert.ToDateTime("1/1/1900");

            string strID1 = "";
            string strID2 = "";
            string strID3 = "";

            TimeSpan oTS;

            object _internalresult = null;
            string _strSQL = "";

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);

            try
            {

                if (PatientID > 0)
                {
                    _strSQL = "SELECT dtDOB FROM Patient WHERE nPatientID = " + PatientID + "";

                    oDB.Connect(false);
                    _internalresult = oDB.ExecuteScalar_Query(_strSQL);
                    oDB.Disconnect();

                    if (_internalresult != null)
                    {
                        if (_internalresult.ToString() != null)
                        {
                            if (_internalresult.GetType() != typeof(System.DBNull))
                            {
                                if (_internalresult.ToString() != "")
                                {
                                    _PatientDOB = Convert.ToDateTime(_internalresult);
                                }
                            }
                        }
                    }
                }

                _result = "";

                oTS = new TimeSpan();
                oTS = _CurrentDate.Subtract(_BaseDate);
                strID1 = oTS.Days.ToString().Replace("-", "");

                oTS = new TimeSpan();
                oTS = _CurrentDate.Subtract(_CurrentDate.Date);
                strID2 = Convert.ToInt32(oTS.TotalSeconds).ToString().Replace("-", "");

                oTS = new TimeSpan();
                oTS = _PatientDOB.Subtract(_BaseDate);
                strID3 = oTS.Days.ToString().Replace("-", "");

                _result = strID1 + strID2 + strID3;

                _Result = Convert.ToInt64(_result);

                return _Result;

            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                //returns random number if exception occures
                Random oRan = new Random();
                return oRan.Next(1, Int32.MaxValue);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                //returns random number if exception occures
                Random oRan = new Random();
                return oRan.Next(1, Int32.MaxValue);
            }
            finally
            {
                oDB.Dispose();
                oDB = null;
            }
        }
        private long GenerateVisitID(DateTime DtTransDate)
        {    // function used to generate the Visit ID for order.

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

            try
            {
                oDBParameters.Add("@nPatientID", _patientID, ParameterDirection.Input, System.Data.SqlDbType.BigInt, 18);
                oDBParameters.Add("@dtVisitdate", DtTransDate, ParameterDirection.Input, System.Data.SqlDbType.DateTime);
                long lngAppointID = 0;
                oDBParameters.Add("@AppointmentID", lngAppointID, ParameterDirection.Input, System.Data.SqlDbType.BigInt, 18);
                long lngTransId = 0;
                lngTransId = GetPrefixTransactionID(_patientID);
                oDBParameters.Add("@MachineID", lngTransId, ParameterDirection.Input, System.Data.SqlDbType.BigInt, 18);
                oDBParameters.Add("@VisitID", 0, ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt, 18);

                oDBParameters.Add("@flag", 0, ParameterDirection.Output, System.Data.SqlDbType.Int);

                oDB.Connect(false);
                long lngVisitId = 0;
                object tmpVisitId;
                object tmpFlag;
                oDB.Execute("gsp_InsertVisits", oDBParameters, out tmpVisitId, out tmpFlag);
                oDB.Disconnect();

                if (tmpVisitId.ToString() == "" || tmpVisitId == null)
                    lngVisitId = 0;
                else
                    lngVisitId = Convert.ToInt64(tmpVisitId);

                return lngVisitId;

            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                //clsGeneral objclsGen = new clsGeneral();
                //objclsGen.UpdateLog("GL19 - Insert Emdeon Data: " + ex.ToString());
                return 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                //clsGeneral objclsGen = new clsGeneral();
                //objclsGen.UpdateLog("GL20 - Insert Emdeon Data: " + ex.ToString());
                return 0;
            }
            finally
            {
                if (oDBParameters != null)
                {
                    oDBParameters.Clear();
                    oDBParameters.Dispose();
                    oDBParameters = null;
                }
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
            }
        }

        private bool compareProvider(Int64 _ProviderID)
        {
            string strProviderName = string.Empty;
            string strLoginUserName = string.Empty;
            string strLabID = string.Empty;
            clsGeneral objclsgeneral = null;
            try
            {
                objclsgeneral = new clsGeneral();
                if (_ProviderID != 0)
                {
                    strProviderName = objclsgeneral.GetProviderName(_ProviderID, _ClinicID);
                }


                strLabID = objclsgeneral.GetProvidergloLabId(_ProviderID);
                if (objclsgeneral.ConfirmNull(strLabID.ToString()))
                {
                    return true;
                }
                else
                {
                    if (MessageBox.Show("The current provider '" + strProviderName + " ' does not have a lab ID set up.\r\n"
                       + "If you place a lab order, you will have to select a provider in the labs interface.\r\n"
                       + "Would you like to proceed with the lab order? \r\n\r\n"
                       + "To set up a lab ID, go to provider settings.", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Open, "Placing external lab order even patient provider does not have LabId", _patientID, 0, _ProviderID, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR);
                        return true;
                    }
                    else
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Open, "Placing external lab Order canceled by user", _patientID, 0, _ProviderID, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR);
                        //_IsFormClose = true;
                        //this.Close();
                        return false;
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return false;
            }
            finally
            {
                if (objclsgeneral != null)
                {
                    objclsgeneral.Dispose();
                    objclsgeneral = null;
                }
            }

        }

        protected void SaveOrder(Int16 IsFinished)
        {
            gloEMRLabOrder oLabOrderRequest = new gloEMRLabOrder();
            try
            {
                blnSaved = true;

               

                
                if (!CheckLockStatus(LabOrderParameter.OrderID))
                {
                    oLabOrderRequest.LabOrder = oLabActor_Order1;

                    oLabActor_Order1.OrderID = _OrderParamter.OrderID;

                    gloUCLab_OrderDetail.ReadData();
                    oLabActor_Order1.OrderNoPrefix = gloUCLab_OrderDetail.OrderNumberPrefix;
                    oLabActor_Order1.OrderNoID = gloUCLab_OrderDetail.OrderNumberID;


                    oLabActor_Order1.PreferredLab = gloUCLab_OrderDetail.PreferredLab;
                    oLabActor_Order1.PreferredLabID = gloUCLab_OrderDetail.PreferredLabID;

                    oLabActor_Order1.SendTo = gloUCLab_OrderDetail.SendTo;

                    oLabActor_Order1.ReferredTo = gloUCLab_OrderDetail.ReferredTo;
                    oLabActor_Order1.ReferredToID = gloUCLab_OrderDetail.ReferredToID;


                    oLabActor_Order1.SampledBy = gloUCLab_OrderDetail.SampledBy;
                    oLabActor_Order1.SampledByID = gloUCLab_OrderDetail.SampledByID;
                    oLabActor_Order1.ReferredBy = gloUCLab_OrderDetail.ReferredBy;
                    oLabActor_Order1.ReferredByID = gloUCLab_OrderDetail.ReferredByID;
                    oLabActor_Order1.Users = gloUCLab_OrderDetail.Users;


                    oLabActor_Order1.TaskDescription = gloUCLab_OrderDetail.TaskDescription;
                    oLabActor_Order1.TaskDueDate = gloUCLab_OrderDetail.TaskDueDate;
                    oLabActor_Order1.TransactionDate = gloUC_PatientStrip1.TransactionDate;
                    //Bug #60825: 00000587 : creating an emdeon order the system is later updating the nVisitID to '0'
                    oLabActor_Order1.VisitID = GenerateVisitID(oLabActor_Order1.TransactionDate); //GetVisitID(DateTime.Now, _patientID);                    
                    oLabActor_Order1.ReferredByFName = gloUCLab_OrderDetail.ReferredByFName;
                    oLabActor_Order1.ReferredByMName = gloUCLab_OrderDetail.ReferredByMName;
                    oLabActor_Order1.ReferredByLName = gloUCLab_OrderDetail.ReferredByLName;
                    oLabActor_Order1.PatientID = _OrderParamter.PatientID;

                    oLabActor_Order1.PatientAge.Years = gloUC_PatientStrip1.PatientAge.Years;
                    oLabActor_Order1.PatientAge.Months = gloUC_PatientStrip1.PatientAge.Months;
                    //oLabActor_Order1.PatientAge.Days = gloUC_PatientStrip1.PatientAge.Days;
                    if ((gloUC_PatientStrip1.PatientAge.Years == 0 && gloUC_PatientStrip1.PatientAge.Months == 0
                    && gloUC_PatientStrip1.PatientAge.Days == 0) && (gloUC_PatientStrip1.PatientAge.Hours != 0))
                    {
                        oLabActor_Order1.PatientAge.Days = Convert.ToInt16(gloUC_PatientStrip1.PatientAge.Hours / 24);
                    }
                    else
                    {
                        oLabActor_Order1.PatientAge.Days = gloUC_PatientStrip1.PatientAge.Days;
                    }
                    oLabActor_Order1.PatientAge.Age = gloUC_PatientStrip1.PatientAge.Age;

                    //Sanjog
                    oLabActor_Order1.ProviderID = _OrderParamter.ProviderID;
                    oLabActor_Order1.Provider = _OrderParamter.LabProviderName;
                    //SAnjog

                    oLabActor_Order1.DMSID = 0;

                    oLabActor_Order1.OrderTests = gloLabUC_Transaction1.GetData();
                    oLabActor_Order1.ArrTestName = gloLabUC_Transaction1.arrTestNames;

                    SetValueOrderNotCPOEandStatus();
                    //Bug #64499: 00000584 : PRINTING NOT AVAILABLE FROM RESULT SETS TAB
                    if (c1TestLibrary.Rows.Count > 1 || tabControl1.SelectedIndex != 3)
                    {
                        if ((oLabActor_Order1.OrderTests == null) == true)
                        {
                            if (IsClosed == false)
                            {
                                MessageBox.Show("Please select at least one test.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                _OrderParamter.CloseAfterSave = false;
                                blnSaved = false;
                                return;
                            }
                            else
                            {
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Close, "Modified lab orders not saved because of no result available ", oLabActor_Order1.PatientID, oLabActor_Order1.OrderID, oLabActor_Order1.ProviderID, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR);
                                MessageBox.Show("Order cannot be saved, At least one test should be associated to the order.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }
                    }
                    else
                    {
                        return;
                    }
                    if (chk_PreviousHistory.Checked == true)
                    {
                        oLabOrderRequest.Modify(_OrderParamter.OrderID, IsFinished);

                    }
                    else
                    {
                        clsPreviousHistory ObjPreviousHistory = new clsPreviousHistory(_dataBaseConnectionString, _patientID);
                        ObjPreviousHistory.SaveModifiedLabOrder(oLabActor_Order1);

                        ObjPreviousHistory = null;
                    }

                    try
                    {
                        bool IsSucceed = AutoOrderStatusUpdateforReview(_OrderParamter.OrderID, Convert.ToString(oLabActor_Order1.OrderStatusNumber));
                        if (IsSucceed)
                        {
                            oLabActor_Order1.OrderStatusNumber = ReadyForReview;
                            DataTable dt = GetOrderStatus();
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                DataRow[] dr;
                                dr = dt.Select("OrderStatusNumber = " + Convert.ToString(ReadyForReview) + "");
                                if (dr != null && dr.Length > 0)
                                {
                                    int rowToSet = 0;
                                    if (IsRowChange == true)
                                    {
                                        rowToSet = OrderStatusRowToupdate;
                                        IsRowChange = false;
                                    }
                                    else
                                    {
                                        rowToSet = c1TestLibrary.RowSel;
                                    }
                                    //MessageBox.Show("Row Before change : " + OrderStatusRowToupdate + Environment.NewLine + "Current Select Row : " + c1TestLibrary.RowSel + Environment.NewLine + "IsRowChange:" + IsRowChange + Environment.NewLine + "Final Row to Update: " + rowToSet);
                                    if (rowToSet != 0)
                                    {
                                        if (c1TestLibrary != null)
                                        {
                                            c1TestLibrary.SetData(rowToSet, COL_CUSTOMORDERSTATUS, Convert.ToString(dr[0]["OrderStatus"]));
                                        }
                                    }
                                }


                            }
                            if (dt != null)  //added for memory management
                            {
                                dt.Dispose();
                                dt = null;
                            }
                        }
                    }
                    catch (Exception) { }


                    gloLabUC_Transaction1.LabModified = false;
                    if (UnLockOrder(_OrderParamter.OrderID))
                    {
                        _CurrentLockedOrder = 0;
                    }
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Modify, "Lab Orders Modified Successfully", _patientID, _OrderParamter.OrderID, _PatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);


                    long TaskID = 0;

                    // TaskID = GetTaskID_OfLab(_OrderParamter.OrderID);
                    //Added by Mayuri:20140324-dont send task to user in modify mode if already sent,except it is completed.
                    RemoveExistingTaskUsers();

                    if (oLabActor_Order1.Users.Count > 0)
                    {
                        AddTasks(oLabActor_Order1.Users, oLabActor_Order1.TransactionDate, oLabActor_Order1.PatientID, TaskID, oLabActor_Order1.TaskDescription, oLabActor_Order1.TaskDueDate, oLabActor_Order1);
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Add, "Added task for Lab orders modified", oLabActor_Order1.PatientID, TaskID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                    }

                    UpdateResultFlag();
                }
                else
                {
                    blnSaved = false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                blnSaved = false;
            }
            finally
            {
                if (oLabOrderRequest != null)
                {
                    oLabOrderRequest.Dispose();
                    oLabOrderRequest = null;
                }
            }
        }
        private void RemoveExistingTaskUsers()
        {
            DataTable dttaskdetail;
            DataView dv = null;
            int k;
            clsGeneral _objgeneral = new clsGeneral();
            dttaskdetail = _objgeneral.GetTaskDetail_OfLab(_OrderParamter.OrderID);

            if (oLabActor_Order1.Users.Count > 0)
            {
                for (k = oLabActor_Order1.Users.Count - 1; k >= 0; k--)
                {
                    if (dttaskdetail != null)
                    {
                        if (dttaskdetail.Rows.Count > 0)
                        {
                            dv = dttaskdetail.DefaultView;
                            dv.RowFilter = "nAssigntoID=" + oLabActor_Order1.Users.get_Item(k).ID;
                            if (dv.Count > 0)
                            {
                                oLabActor_Order1.Users.RemoveAt(k);
                            }

                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }

                }
            }
            if (dttaskdetail != null)
            {
                dttaskdetail.Dispose();
                dttaskdetail = null;
            }
            if (dv != null)
            {
                dv.Dispose();
                dv = null;
            }
            _objgeneral.Dispose();
            _objgeneral = null;
        }
        private void SetValueOrderNotCPOEandStatus()
        {
            string strQuery = string.Empty;
            DataTable dtResult = null;

            gloDatabaseLayer.DBLayer objDbLayer = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            clsGeneral objClsGeneral = new clsGeneral();
            try
            {
                objDbLayer.Connect(false);
                strQuery = " SELECT isnull(blnOrderNotCPOE,0) as blnOrderNotCPOE, isnull(OrderStatusNumber,0) as OrderStatusNumber, isnull(bOutboundTransistion, 0) as bOutboundTransistion, isnull(labom_LabComment,0) as labom_LabComment from Lab_Order_MST WHERE labom_OrderID = " + oLabActor_Order1.OrderID;

                objDbLayer.Retrive_Query(strQuery, out dtResult);

                if (dtResult != null && dtResult.Rows.Count > 0)
                {
                    oLabActor_Order1.blnOrderNotCPOE = Convert.ToBoolean(dtResult.Rows[0]["blnOrderNotCPOE"].ToString());
                    oLabActor_Order1.OrderStatusNumber = (int)dtResult.Rows[0]["OrderStatusNumber"];
                    oLabActor_Order1.bOutboundTransistion = Convert.ToBoolean(dtResult.Rows[0]["bOutboundTransistion"].ToString());
                    oLabActor_Order1.FastingStatus = dtResult.Rows[0]["labom_LabComment"].ToString();
                }

                objDbLayer.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (objDbLayer != null)
                { objDbLayer.Dispose(); objDbLayer = null; }

                if (dtResult != null)
                { dtResult.Dispose(); dtResult = null; }
                objClsGeneral.Dispose();
                objClsGeneral = null;
                strQuery = null;
            }
        }



        private void gloUCLab_Transaction_gUC_TestSelected(long TestID, string Specimen, string CollectionContainer, string StorageTemperature, string LOINCCode, string Instructionas, string Precuation, string Comments)
        {
            if (TestID > 0 && Specimen != "" && CollectionContainer != "" && StorageTemperature != "" && LOINCCode != "" && Instructionas != "" && Precuation != "" && Comments != "")
            {
                gloUCLab_TestDetail.Visible = true;
                gloUCLab_TestDetail.SetData(TestID, Specimen, CollectionContainer, StorageTemperature, LOINCCode, Instructionas, Precuation, Comments);
            }
            else
            {
                gloUCLab_TestDetail.Visible = false;
            }

        }
        private void GloUC_TransactionHistory_btnShowGraphClick(object sender, System.EventArgs e)
        {
            DataTable dt_selectedResult = null;
            try
            {
                //Resolved bug: 41121
                GloUC_TransactionHistory.btnShowGraphClick -= GloUC_TransactionHistory_btnShowGraphClick;


                dt_selectedResult = gloUC_TransactionHistory1.SelectResult();

                if (dt_selectedResult.Rows.Count == 0)
                {
                    return;
                }


                for (int rowcnt = 0; rowcnt < dt_selectedResult.Rows.Count; rowcnt++)  //for condition added for Bug #68058:
                {
                    if (string.IsNullOrEmpty((string)dt_selectedResult.Rows[rowcnt][0]))
                    {
                        MessageBox.Show("Graph cannot be displayed because collected date is blank.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                DataView dv = default(DataView);
                dv = new DataView(dt_selectedResult);
                dv.Sort = "Value";

                decimal max = Convert.ToDecimal(dv[dv.Count - 1]["Value"]);
                Decimal min = Convert.ToDecimal(dv[0]["Value"]);


                DateTime dtSelectedResultToDate = Convert.ToDateTime(dt_selectedResult.Rows[dt_selectedResult.Rows.Count - 1][0]);

                DateTime dtStartdate = default(DateTime);
                dtStartdate = Convert.ToDateTime(Convert.ToString(dt_selectedResult.Rows[0][0]));

                gloEmdeonCommon.frmLab_GraphsResult oGraphResult = new gloEmdeonCommon.frmLab_GraphsResult(_dataBaseConnectionString, dtStartdate, dtSelectedResultToDate, 0, 0, _patientID, Convert.ToString(dt_selectedResult.Rows[0][1]), Convert.ToString(dt_selectedResult.Rows[0][2]), dt_selectedResult, null, false, true, min, max);
                {
                    oGraphResult.WindowState = FormWindowState.Normal;
                    oGraphResult.ShowInTaskbar = false;
                    oGraphResult.StartPosition = FormStartPosition.CenterScreen;
                    oGraphResult.WindowState = FormWindowState.Maximized;
                    oGraphResult.TopMost = false;
                    oGraphResult.ShowDialog(this);
                }
                if (oGraphResult != null)
                {
                    oGraphResult.Dispose();
                    oGraphResult = null;
                }
                dv.Dispose();
                dv = null;
                GloUC_TransactionHistory.btnShowGraphClick += GloUC_TransactionHistory_btnShowGraphClick;
                return;
            }
            catch (Exception ex)
            {
                GloUC_TransactionHistory.btnShowGraphClick += GloUC_TransactionHistory_btnShowGraphClick;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);

            }
            finally
            {
                if (dt_selectedResult != null)
                {
                    dt_selectedResult.Dispose();
                    dt_selectedResult = null;
                }
            }
        }
        //Print Orders...
        private void MenuEvent_Print()
        {
            try
            {
                if (tabControl1.SelectedIndex == 2)
                {
                    gloLabUC_LabFlowSheet1.PrintFlowSheet();
                }
                else if (tabControl1.SelectedIndex == 3)
                {
                    blnSaved = true;

                    if (tlbbtn_Save.Enabled == true)
                    {
                        SaveOrder(0);
                    }

                    if (blnSaved == false)
                    {
                        return;
                    }
                    gloEMRLabOrder oLabOrderRequest = new gloEMRLabOrder();
                    oLabActor_Order1.ArrTestName = oLabOrderRequest.GetOrderTests(oUc_ResultSet.OrderID);
                    PrintLabOrderReport(oUc_ResultSet.OrderID, oLabActor_Order1.ArrTestName);
                    oLabOrderRequest.Dispose();
                    oLabOrderRequest = null;
                }
                else
                {
                    //DataTable dt = new DataTable();
                    blnSaved = true;

                    SaveOrder(0);//Saving Order

                    if (blnSaved == false)
                    {
                        return;
                    }
                    PrintLabOrderReport(_OrderParamter.OrderID, oLabActor_Order1.ArrTestName);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        public bool CheckSSRSConfiguration()
        {
            gloSettings.GeneralSettings oSetting = new gloSettings.GeneralSettings(_dataBaseConnectionString);
            string strReportProtocol = String.Empty;
            string strReportServer = String.Empty;
            string strReportFolder = String.Empty;
            string strVirtualDir = String.Empty;
            bool ReturnFlag = false;

            object oValue = new object();

            try
            {
                oSetting.GetSetting("ReportProtocol", out oValue);
                if (oValue != null)
                {
                    strReportProtocol = oValue.ToString().Trim();
                    oValue = null;
                }

                oSetting.GetSetting("ReportServer", out oValue);
                if (oValue != null)
                {
                    strReportServer = oValue.ToString().Trim();
                    oValue = null;
                }

                oSetting.GetSetting("ReportFolder", out oValue);
                if (oValue != null)
                {
                    strReportFolder = oValue.ToString().Trim();
                    oValue = null;
                }

                oSetting.GetSetting("ReportVirtualDirectory", out oValue);
                if (oValue != null)
                {
                    strVirtualDir = oValue.ToString().Trim();
                    oValue = null;
                }
               
                if (String.IsNullOrEmpty(strReportProtocol) || String.IsNullOrEmpty(strReportServer) || String.IsNullOrEmpty(strReportFolder) || String.IsNullOrEmpty(strVirtualDir))
                {
                    MessageBox.Show("SSRS Settings not set. Set the Report Server settings and then deploy the reports.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ReturnFlag = false;
                }
                else
                {
                    ReturnFlag = true;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oSetting != null)
                {
                    oSetting.Dispose();
                    oSetting = null;
                }
            }
            
            return ReturnFlag;

        }
        
        //LabOrder Report...
        public void PrintLabOrderReport(long OrderID, ArrayList arrTests)
        {
            //Create the object for report
            //Rpt_LabOrder oLabs = null;

            try
            {
                if (OrderID != 0)
                {

                    if (CheckSSRSConfiguration())
                    {
                        CreateLabSSRSReport(OrderID);
                        ChangeOrderStatusNewtoSent(OrderID);
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Print, "Lab Orders printed", _patientID, _OrderParamter.OrderID, _PatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                    }

                    //// Date : 20141007 Time : 1745  Developer : Rohini K.
                    //// DnD : Existing Crystal Report code for printing lab order replaced with the new SSRS implementation above
                    //// Report Name : Rpt_LabOrder.rpt

                    //oLabs = CreateReport(OrderID, arrTests);
                    //if (gloEmdeonCommon.mdlGeneral.gblnIsDefaultPrinter == false)
                    //{
                    //    PrintDialog1 = new PrintDialog();
                    //    if (PrintDialog1.ShowDialog(this) == DialogResult.OK)
                    //    {
                    //        oLabs.PrintOptions.PrinterName = PrintDialog1.PrinterSettings.PrinterName;
                    //        // Bug #52496: 00000131 : Printing - EMR
                    //        oLabs.PrintToPrinter(PrintDialog1.PrinterSettings.Copies, PrintDialog1.PrinterSettings.Collate, PrintDialog1.PrinterSettings.FromPage, PrintDialog1.PrinterSettings.ToPage);
                    //        if ((oLabs != null))
                    //        {
                    //            oLabs.Close();
                    //            oLabs.Dispose();
                    //            oLabs = null;
                    //        }
                    //        ChangeOrderStatusNewtoSent(OrderID);
                    //    }
                    //    PrintDialog1.Dispose();
                    //    PrintDialog1 = null;
                    //}
                    //else
                    //{
                    //    oLabs.PrintToPrinter(1, false, 0, 0);
                    //    oLabs.Close();
                    //    oLabs.Dispose();
                    //    oLabs = null;
                    //    ChangeOrderStatusNewtoSent(OrderID);
                    //}
                    ////** DnD : Existing Crystal Report code for printing lab order replaced with the new SSRS implementation above
                    ////** Date : 20141007 Time : 1745  Developer : Rohini K.
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                //if (oLabs != null)
                //{
                //    oLabs.Close();
                //    oLabs.Dispose();
                //    oLabs = null;
                //}
            }
        }

        private void ChangeOrderStatusNewtoSent(long OrderID)
        {
            if (GetCurrentOrderOrderStatusNumber(OrderID) == 1001)
            {
                if (MessageBox.Show(this,"You printed the Order Report. Do you want to set the Order Status to 'Sent'?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    UpdateManualOrderStatus("1005", OrderID.ToString());

                    if (tabControl1.SelectedIndex == 0)
                    {
                        c1TestLibrary.SetData(c1TestLibrary.RowSel, COL_CUSTOMORDERSTATUS, "Sent");
                    }
                }
            }
        }

        //Report --> SSRS 
        public void CreateLabSSRSReport(long OrderID)
        {
            try
            {
                gloSSRSApplication.clsPrintReport clsPrntRpt;
                string sqlServerName = Convert.ToString(appSettings["SQLServerName"]);
                string sqlDatabaseName = Convert.ToString(appSettings["DataBaseName"]);
                string sqlUser = Convert.ToString(appSettings["SQLLoginName"]);
                string sqlPwd = Convert.ToString(appSettings["SQLPassword"]);
                bool blSQLAuth =  !(Convert.ToBoolean(appSettings["WindowAuthentication"]));
                bool gblnIsDefaultPrinter = !(Convert.ToBoolean(appSettings["DefaultPrinter"]));

                clsPrntRpt = new gloSSRSApplication.clsPrintReport(sqlServerName, sqlDatabaseName, blSQLAuth, sqlUser, sqlPwd);

                clsPrntRpt.PrintReport("LabOrderReport_SSRS", "OrderID,PatientID", Convert.ToString(OrderID) + "," + Convert.ToString(_patientID), gblnIsDefaultPrinter, "");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            { }
        }

        //Reports...Crystal.
        public Rpt_LabOrder CreateReport(long OrderID, ArrayList arrTests)
        {
            Rpt_LabOrder oLabs = new Rpt_LabOrder();
            dsgloEMRReports dsReports = new dsgloEMRReports();
            //   gloEmdeonInterface.Classes.clsGeneral obj = new gloEmdeonInterface.Classes.clsGeneral();
            SqlConnection sConstr = new SqlConnection(_dataBaseConnectionString);
            SqlCommand sCmd = new SqlCommand();
            SqlDataAdapter da = null;
            string strQuery = string.Empty;
            try
            {
                strQuery = " SELECT sClinicName, sAddress1, sAddress2, sStreet, sCity, sState, sZIP, sPhoneNo,sMobileNo, sFAX, sEmail, sURL, imgClinicLogo FROM  Clinic_MST where nclinicId = 1";
                sCmd.CommandText = strQuery;
                sCmd.Connection = sConstr;
                da = new SqlDataAdapter(sCmd);
                da.Fill(dsReports, "dt_Clinic_MST");
                if (OrderID != 0)
                {

                    #region "Final Inserted Results in Order Report Query"

                    gloDatabaseLayer.DBLayer oDBLayer = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
                    string _strQryResultDateTime = string.Empty;
                    string _strResultDateTime = string.Empty;
                    object objResult = null;
                    try
                    {
                        oDBLayer.Connect(false);
                        _strQryResultDateTime = " select top 1 dbo.Lab_Order_Test_Result.labotr_TestResultDateTime from dbo.Lab_Order_Test_Result WHERE Lab_Order_Test_Result.labotr_OrderID =  '" + OrderID + "' "
                                                + " order by dbo.Lab_Order_Test_Result.labotr_TestResultDateTime  desc ";

                        objResult = oDBLayer.ExecuteScalar_Query(_strQryResultDateTime);
                        if (objResult != null && objResult.ToString() != "")
                        {
                            _strResultDateTime = Convert.ToDateTime(objResult).ToString("yyyy-MM-dd HH:mm:ss.fff");
                        }
                        oDBLayer.Disconnect();
                    }
                    catch (Exception ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog("" + ex.ToString(), false);
                    }
                    finally
                    {
                        if (oDBLayer != null)
                        {
                            oDBLayer.Dispose();
                        }
                        if (objResult != null)
                        {
                            objResult = null;
                        }
                        _strQryResultDateTime = string.Empty;
                    }

                    //Start Change :  Qry by Sandip Deshmukh : 201005181554                     
                    // This has been to print final inserted Results in order .
                    strQuery = "";
                    strQuery = "SELECT Lab_Order_MST.labom_TransactionDate AS TransDate, "
                                + "Lab_Order_MST.labom_OrderNoPrefix + '-' + CONVERT(varchar(100), "
                                + "Lab_Order_MST.labom_OrderNoID) AS OrderNumber, "
                                + " dbo.Lab_Order_Test_Result.labotr_SpecimenReceivedDateTime  AS SpecimenRecievedDate, "
                                + " dbo.Lab_Order_Test_Result.labotr_ResultTransferDateTime AS ReportDate , "
                                + " ISNULL(dbo.Lab_Order_MST.labom_ReceivingFacilityCode,'') AS ReceivingFacilityCode, "
                                + " ISNULL(dbo.Lab_Order_TestDtl.labotd_Comment,'') As TestComments, "
                                + " ISNULL(dbo.Lab_Order_MST.labom_LabComment,'') AS LabComment, "
                                + " Lab_Order_MST.labom_CollectionDate AS LabMSTCollectionDate,  "
                                + " ISNULL(dbo.Lab_Order_Test_ResultDtl.labotrd_LOINCID,'') AS LoinicCode, "
                                + " ISNULL(dbo.Lab_Order_Test_ResultDtl.labotrd_AlternateResultName,'') AS AlternateResulttName, "
                        //+ "CONVERT(varchar(100),ISNULL(dbo.Lab_Order_MST.labom_FileOrderIdentifier,'')) AS ExternalCode, "
                                + "(  ISNULL(dbo.Lab_Order_MST.labom_ExternalCode,'')  + CASE ISNULL(dbo.Lab_Order_MST.labom_FileOrderIdentifier,'')  WHEN '' THEN '' ELSE ' (Req. # : ' +  dbo.Lab_Order_MST.labom_FileOrderIdentifier + ')' END) AS ExternalCode,"  //added By manoj Jadhav Incident #00035859
                                + "ISNULL(dbo.Lab_Order_Test_ResultDtl.labotrd_ProducerIdentifier,'') AS ProducerIdentifier, "
                                + "Lab_Order_TestDtl.labotd_Instruction AS Instruction, "
                                + "Lab_Order_TestDtl.labotd_Precaution AS Precaution, "
                                + "Lab_Order_TestDtl.labotd_DateTime AS TestDate, "
                                + "CONVERT(varchar(100),Lab_Order_MST.labom_OrderID) AS OrderID, "
                                + " Lab_Order_TestDtl.labotd_TestID AS TestID, "
                                + " CONVERT(varchar(100), Lab_Order_MST.labom_PatientID) AS PatientID, "
                                + "User_MST.sLoginName AS SampledBy, "
                                + "contacts_mst.sName AS ReferredBy, "
                                + "ISNULL(Provider_MST.sFirstName, '') + ' ' + ISNULL(Provider_MST.sMiddleName, '') + ' ' + ISNULL(Provider_MST.sLastName, '') AS Provider, "
                                + "ISNULL(dbo.Lab_Order_Test_Result.labotr_TestResultNumber, 0) AS TestResultNumber, "
                                + "ISNULL(dbo.Lab_Order_Test_ResultDtl.labotrd_ResultLineNo,0) AS ResultLineNo, "
                                + "case len(ISNUll(dbo.Lab_Order_Test_Result.labotr_TestResultName,'')) when 0 then '-' when null then '-' else dbo.Lab_Order_Test_Result.labotr_TestResultName END AS TestResultName, "
                                + "dbo.Lab_Order_Test_Result.labotr_TestResultDateTime AS TestResultDateTime, "
                                + "ISNULL(dbo.Lab_Order_Test_ResultDtl.labotrd_ResultName, '') AS ResultName, "
                                + "ISNULL(dbo.Lab_Order_Test_ResultDtl.labotrd_ResultValue, '') AS ResultValue, "
                                + "ISNULL(dbo.Lab_Order_Test_ResultDtl.labotrd_ResultUnit, '') AS ResultUnit, "
                                + "ISNULL(dbo.Lab_Order_Test_ResultDtl.labotrd_ResultRange, '') AS ResultRange, "
                                + "ISNULL(dbo.Lab_Order_Test_ResultDtl.labotrd_ResultType, '') AS ResultType, "
                                + "ISNULL(dbo.Lab_Order_Test_ResultDtl.labotrd_AbnormalFlag, '') AS AbnormalFlag, "
                                + "ISNULL(dbo.Lab_Order_Test_ResultDtl.labotrd_ResultComment, '') AS ResultComment, "
                                + "dbo.Lab_Order_Test_ResultDtl.labotrd_ResultDateTime AS ResultDateTime, "
                                + "dbo.Lab_Order_TestDtl.labotd_TestName AS TestName, "
                                + "dbo.Lab_Order_TestDtl.labotd_SpecimenName AS Speciman, "
                                + "ISNULL(Lab_Collection_Mst.labcm_Name, '') AS CollectionContainer, "
                                + "dbo.Lab_Order_TestDtl.labotd_StorageName AS StorageTemperature, "
                                + "ISNULL(dbo.Lab_GetUTCTimeString(Lab_Order_MST.labom_TransactionDateUTC),'') AS labom_TransactionDateUTC, "
                                + "ISNULL(dbo.Lab_GetUTCTimeString(Lab_Order_TestDtl.labotd_DateTimeUTC),'') AS labotd_DateTimeUTC, "
                                + "ISNULL(Lab_Order_TestDtl.labotd_SpecimenTypeText,'') AS labotd_SpecimenTypeText, "
                                + "Lab_Order_TestDtl.labotd_SpecimenCollectionStartDateTime AS labotd_SpecimenCollectionStartDateTime, "
                                + "ISNULL(Lab_Order_TestDtl.labotd_SpecimenRejectReason,'') AS labotd_SpecimenRejectReason, "
                                + "ISNULL(Lab_Order_TestDtl.labotd_SpecimenCondition,'') AS labotd_SpecimenCondition, "
                                + "ISNULL(dbo.Lab_GetUTCTimeString(Lab_Order_TestDtl.labotd_SpecimenCollectionStartDateTimeUTC),'') AS labotd_SpecimenCollectionStartDateTimeUTC, "
                                + "ISNULL(Lab_Order_Test_ResultDtl.labotrd_LabFacilityName,'') AS labotrd_LabFacilityName, "
                                + "ISNULL(Lab_Order_Test_ResultDtl.labotrd_LabFacilityStreetAddress,'') AS labotrd_LabFacilityStreetAddress, "
                                + "ISNULL(Lab_Order_Test_ResultDtl.labotrd_LabFacilityCity,'') AS labotrd_LabFacilityCity, "
                                + "ISNULL(Lab_Order_Test_ResultDtl.labotrd_LabFacilityState,'') AS labotrd_LabFacilityState, "
                                + "ISNULL(Lab_Order_Test_ResultDtl.labotrd_LabFacilityZipCode,'') AS labotrd_LabFacilityZipCode, "
                                + "ISNULL(Lab_Order_Test_ResultDtl.labotrd_LabFacilityCountry,'') AS labotrd_LabFacilityCountry, "
                                + "ISNULL(Lab_Order_Test_ResultDtl.labotrd_LabFacilityCountyOrParishCode ,'') AS labotrd_LabFacilityCountyOrParishCode, "
                                + "ISNULL(dbo.Lab_GetUTCTimeString(Lab_Order_MST.labom_CollectionDateUTC),'') AS labom_CollectionDateUTC, "
                                + "ISNULL(dbo.Lab_GetUTCTimeString(dbo.Lab_Order_Test_Result.labotr_ResultTransferDateTimeUTC),'') AS labotr_ResultTransferDateTimeUTC, "
                                + "ISNULL(dbo.Lab_GetUTCTimeString(dbo.Lab_Order_Test_ResultDtl.labotrd_ResultDateTimeUTC),'') AS ResultDateTimeUTC, "
                                + "dbo.Lab_Order_MST.labom_OrderDate AS OrderDate, "
                                + "ISNULL(dbo.Lab_GetUTCTimeString(dbo.Lab_Order_MST.labom_OrderDateUTC),'') AS OrderDateUTC, "
                                + "ISNULL(dbo.Lab_GetUTCTimeString(dbo.Lab_Order_Test_Result.labotr_SpecimenReceivedDateTimeUTC),'') AS SpecimenReceivedDateTimeUTC, "
                                + "ISNULL(Lab_Order_Test_ResultDtl.labotrd_ResultParentChildFlag,0) AS ParentChildResultFlag "
                                + "FROM Lab_Order_MST INNER JOIN "
                                + "Lab_Order_TestDtl ON Lab_Order_MST.labom_OrderID = Lab_Order_TestDtl.labotd_OrderID LEFT OUTER JOIN "
                                + "Lab_Order_Test_Result ON Lab_Order_TestDtl.labotd_TestID = Lab_Order_Test_Result.labotr_TestID AND "
                                + " Lab_Order_TestDtl.labotd_OrderID = Lab_Order_Test_Result.labotr_OrderID LEFT OUTER JOIN "
                                + "Lab_Order_Test_ResultDtl ON Lab_Order_TestDtl.labotd_OrderID = Lab_Order_Test_ResultDtl.labotrd_OrderID AND "
                                + "Lab_Order_TestDtl.labotd_TestID = Lab_Order_Test_ResultDtl.labotrd_TestID AND "
                                + " Lab_Order_Test_Result.labotr_TestResultNumber = Lab_Order_Test_ResultDtl.labotrd_TestResultNumber LEFT OUTER JOIN "
                                + "User_MST ON Lab_Order_MST.labom_SampledByID = User_MST.nUserID LEFT OUTER JOIN "
                                + "Provider_MST ON Lab_Order_MST.labom_ProviderID = Provider_MST.nProviderID LEFT OUTER JOIN "
                                + "contacts_mst ON Lab_Order_MST.labom_ReferredByID = contacts_mst.nContactID LEFT OUTER JOIN "
                                + "Lab_Specimen_Mst ON Lab_Order_TestDtl.labotd_SpecimenID = Lab_Specimen_Mst.labsm_ID LEFT OUTER JOIN "
                                + "Lab_Collection_Mst ON Lab_Order_TestDtl.labotd_CollectionID = Lab_Collection_Mst.labcm_ID "
                                + "WHERE Lab_Order_MST.labom_OrderID = " + OrderID;
                    if (_strResultDateTime.Trim() != "")
                    {
                        strQuery += " and Lab_Order_Test_Result.labotr_TestResultDateTime ='" + _strResultDateTime + "' ";

                    }
                    strQuery += " order by Lab_Order_TestDtl.labotd_LineNo";

                    /////////////////////***
                    //End Qry by Sandip Deshmukh : 201005181554

                    #endregion "Final Inserted Results in Order Report Query"
                    sCmd.CommandText = strQuery;
                    da.Dispose();
                    da = null; //added for memory optimization

                    da = new SqlDataAdapter(sCmd);
                    da.Fill(dsReports, "dt_LabOrderMainReport");
                    strQuery = "";
                    sCmd.CommandText = "";
                    strQuery = " SELECT Patient.sPatientCode AS PatientCode,ISNULL(Patient.sFirstName,'') + SPACE(1) + ISNULL(Patient.sMiddleName,'')+ SPACE(1) + ISNULL(Patient.sLastName,'') + SPACE(1) + ISNULL(Patient.sSuffix,'') AS PatientName,ISNULL(Patient.sGender,'') as Gender,"
                                    + " ISNULL(Patient.SAddressLine1,'') + SPACE(1)+ ISNULL(Patient.sAddressLine2,'') AS PatientAddress,ISNULL(Patient.sPhone,'') AS PatientPhone,"
                                    + " ISNULL(Patient.sCity,'') AS PatientCity,ISNULL(Patient.sState,'') AS PatientState,ISNULL(Patient.sZIP, '') AS PatientZip,ISNULL(Patient.sCounty,'') AS PatientCounty,"
                                    + " Patient.dtDOB AS DateOfBirth, Lab_Order_MST.labom_PatientAgeYear AS AgeInYrs, "
                                    + " Lab_Order_MST.labom_PatientAgeMonth AS AgeInMnths, Lab_Order_MST.labom_PatientAgeDay AS AgeInDays, ISNULL(Provider_MST.sFirstName, '') "
                                    + " + ' ' + ISNULL(Provider_MST.sMiddleName, '') + ' ' + ISNULL(Provider_MST.sLastName, '') AS Provider, ISNULL(User_MST.sLoginName, '') "
                                    + " AS SampledBy, ISNULL(Contacts_MST.sFirstName, '') + ' ' + ISNULL(Contacts_MST.sMiddleName, '') + ' ' + ISNULL(Contacts_MST.sLastName, '') "
                                    + " AS ReferredBy, CONVERT(varchar(100), Lab_Order_MST.labom_OrderID) AS OrderID, Lab_Order_MST.labom_TransactionDate AS TransDate, "
                                    + " Lab_Order_MST.labom_OrderNoPrefix + ' ' + CONVERT(varchar(100), Lab_Order_MST.labom_OrderNoID) AS OrderNumber"
                                    + " FROM Lab_Order_MST INNER JOIN "
                                    + " Patient ON Lab_Order_MST.labom_PatientID = Patient.nPatientID LEFT OUTER JOIN "
                                    + " User_MST ON Lab_Order_MST.labom_SampledByID = User_MST.nUserID LEFT OUTER JOIN "
                                    + " Lab_ContactInfo ON Lab_Order_MST.labom_PreferredLabID = Lab_ContactInfo.labci_Id LEFT OUTER JOIN "
                                    + " Provider_MST ON Lab_Order_MST.labom_ProviderID = Provider_MST.nProviderID LEFT OUTER JOIN "
                                    + " Contacts_MST ON Lab_Order_MST.labom_ReferredByID = Contacts_MST.nContactID "
                                    + " WHERE Patient.nPatientID = '" + _patientID + "' And Lab_Order_MST.labom_OrderID=" + OrderID;
                    sCmd.CommandText = strQuery;
                    da.Dispose();
                    da = null; //added for memory optimization

                    da = new SqlDataAdapter(sCmd);
                    da.Fill(dsReports, "dt_PatientInfo");
                    strQuery = "";
                    strQuery = "select isnull(dbo.lab_order_testdtl_diagcpt.labodtl_code, '') + '-' + isnull(dbo.lab_order_testdtl_diagcpt.labodtl_description, '') as description, "
                                + " convert(varchar(1), dbo.lab_order_testdtl_diagcpt.labodtl_type) + isnull(dbo.lab_order_testdtl_diagcpt.labodtl_code, '') "
                                + "+ isnull(dbo.lab_order_testdtl_diagcpt.labodtl_description, '') as diagcpttype, dbo.lab_order_testdtl_diagcpt.labodtl_type as type,"
                                + " convert(varchar(100), dbo.lab_order_mst.labom_orderid) as orderid,  dbo.lab_order_testdtl.labotd_testid as testid, "
                                + "dbo.lab_order_mst.labom_patientid as patientid, dbo.lab_order_testdtl_diagcpt.labodtl_testname as testname"
                                + " from  dbo.lab_order_mst inner join "
                                + "dbo.lab_order_testdtl on dbo.lab_order_mst.labom_orderid = dbo.lab_order_testdtl.labotd_orderid inner join "
                                + "dbo.lab_order_testdtl_diagcpt on dbo.lab_order_testdtl.labotd_orderid = dbo.lab_order_testdtl_diagcpt.labodtl_orderid and "
                                + "dbo.lab_order_testdtl.labotd_testname = dbo.lab_order_testdtl_diagcpt.labodtl_testname "
                                + " where lab_order_mst.labom_orderid= '" + OrderID + "'";
                    sCmd.CommandText = strQuery;
                    da.Dispose();
                    da = null; //added for memory optimization

                    da = new SqlDataAdapter(sCmd);
                    da.Fill(dsReports, "dt_LabOrderReportCPTICD9");

                    //Added by Abhijeet to show insurance details of patient on 20100916
                    strQuery = "";
                    strQuery = @" select  patientInsurance_dtl.nPatientID as Ins_patientid,patientInsurance_dtl.nInsuranceID as nInsuarnceID,
                                patientInsurance_dtl.nInsuranceID as Ins_id,
                                IsNull(patientInsurance_dtl.sSubscriberPolicy#,'') as Ins_subscriberPolicyNo,
                                IsNull(patientInsurance_dtl.sSubscriberID,'') as ins_SubscriberID,
                                IsNull(patientInsurance_dtl.sGroup,'') as Ins_group,
                                IsNull(patientInsurance_dtl.sEmployer,'') as Ins_employer,
                                IsNull(patientInsurance_dtl.dtDOB,'') as Ins_DOB,
                                 dbo.GET_NAME(patientInsurance_dtl.sSubFName,patientInsurance_dtl.sSubMName,patientInsurance_dtl.sSubLName) as INs_Subscribername,
                                Isnull(patientInsurance_dtl.bPrimaryFlag,'') as ins_Primaryflag,
                                Isnull(patientInsurance_dtl.sInsurancePhone,'') as ins_insurancephone,
                                Isnull(patientInsurance_dtl.sInsuranceName,'') as InsuranceName 
                                from patientInsurance_dtl where patientInsurance_dtl.nInsuranceFlag in (1,2,3)
                                and patientInsurance_dtl.nPatientID=" + _patientID.ToString();
                    sCmd.CommandText = strQuery;
                    da.Dispose();
                    da = null; //added for memory optimization

                    da = new SqlDataAdapter(sCmd);
                    da.Fill(dsReports, "dt_PatientInsDtl");
                    //End of changes for adding insurance details on 20100916

                    da.Dispose();
                    da = null; //added for memory optimization
                    oLabs.SetDataSource(dsReports);

                    oLabs.Subreports["Rpt_LabOrderPatientIns.rpt"].SetDataSource(dsReports.Tables["dt_PatientInsDtl"]);
                }

            }
            catch (SqlException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {

                if (sCmd != null)
                {
                    sCmd.Parameters.Clear();
                    sCmd.Dispose();
                    sCmd = null;
                }
                if (da != null)
                {
                    da.Dispose();
                    da = null;
                }
                if (sConstr != null)
                {
                    sConstr.Dispose();
                    sConstr = null;
                }
                //if (dsReports != null)
                //{
                //    dsReports.Dispose();
                //    dsReports = null;
                //}

            }
            return oLabs;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (gloLabUC_Transaction1.LabModified || gloUCLab_OrderDetail.OrderModified)
                {
                    if (_CurrentLockedOrder > 0)
                    {
                        if (UnLockOrder(_CurrentLockedOrder))
                        {
                            _CurrentLockedOrder = 0;
                        }
                    }

                    DialogResult drResult = MessageBox.Show("Do you want to save your changes?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (drResult == DialogResult.Yes)
                    {
                        _blnClosed = true;
                        IsClosed = true;
                        if (c1TestLibrary != null && c1TestLibrary.RowSel > 0)
                        {
                            OrderStatusRowToupdate = c1TestLibrary.RowSel;
                        }
                        SaveOrder(0);

                        gloUCLab_OrderDetail.OrderModified = false;
                        gloLabUC_Transaction1.LabModified = false;
                    }
                    else if (drResult == DialogResult.No)
                    {
                        gloUCLab_OrderDetail.OrderModified = false;
                        gloLabUC_Transaction1.LabModified = false;
                    }

                }

                if (tabControl1.SelectedIndex == 1)
                {

                    gloUC_TransactionHistory1.LoadPreviousLabs(_patientID, DateTime.Now);
                    tlbbtn_Save.Enabled = false;
                    tlbbtn_Print.Enabled = false;
                    tlbbtn_Fax.Enabled = false;
                    tlbbtn_VWAcknowledgment.Visible = false;
                    tlbbtn_Acknowledgment.Enabled = false;
                    tlbbtn_ViewHistory.Visible = false;
                    tlbbtn_ReviewAck.Visible = false;
                    tlbbtn_Finish.Enabled = false;
                    tlbbtn_HL7.Enabled = false;
                    tlbbtn_EditOrder.Enabled = false;
                    tlbbtn_Requisition.Enabled = false;
                    tlbbtn_OnlySave.Enabled = false;
                    tlbbtnRefresh.Enabled = false;
                    gloUC_TransactionHistory1.DesignTestGrid();
                    gloUC_TransactionHistory1.HideCloseButton = false;
                    gloUC_TransactionHistory1.gUC_InfoButtonClickedDB -= gloLabUC_transaction1_gUC_InfobuttonClickedDB;

                    gloUC_TransactionHistory1.gUC_InfoButtonDocumentClickedDB -= gloLabUC_transaction1_gUC_InfoButtonDocumentClickedDB;


                    gloUC_TransactionHistory1.gUC_InfoButtonClickedDB += gloLabUC_transaction1_gUC_InfobuttonClickedDB;

                    gloUC_TransactionHistory1.gUC_InfoButtonDocumentClickedDB += gloLabUC_transaction1_gUC_InfoButtonDocumentClickedDB;
                    gloUC_TransactionHistory1.IsLabDB = true;
                    gloUC_TransactionHistory1.SetDataByDate(DateTime.Now.Date, DateTime.Now.Date);
                    gloUC_TransactionHistory1.cmbCriteria.Text = "Date";
                    tlbbtn_SendtoLab.Enabled = false; // by Abhijeet on date 20100401                        
                    gloUCLab_OrderDetail.Visible = false;
                    gloUCLab_OrderDetail.Height = 111;//Added by madan on 20100506
                    tblCCD.Enabled = false;
                }
                if (tabControl1.SelectedIndex == 0)
                {
                    fillOpenOrdsGrid_New();
                    //fillOpenOrdsGrid();
                    if (c1TestLibrary.Rows.Count > 0 && c1TestLibrary.RowSel > 0)
                    {
                        long curOrderID = Convert.ToInt64(c1TestLibrary.GetData(c1TestLibrary.RowSel, COL_ORDERID));
                        gloEMRGeneralLibrary.gloEMRLab.gloEMRLabOrder oLabOrderRequest = new gloEMRGeneralLibrary.gloEMRLab.gloEMRLabOrder();
                        if (tabControl1.SelectedIndex == 1)
                        {
                            gloUCLab_OrderDetail.Visible = false;
                        }
                        else
                        {
                            gloUCLab_OrderDetail.Visible = true;
                        }
                        ShowOrders(curOrderID);

                        string _strOrderType = GetOrderType(curOrderID);
                        if (_strOrderType == "Emr")
                        {
                            tlbbtn_SendtoLab.Enabled = false;
                            tlbbtn_Requisition.Enabled = false;
                            tlbbtn_Finish.Enabled = true;
                            gloUCLab_OrderDetail.PreferredLabActivity(false);
                        }
                        else
                        {
                            tlbbtn_SendtoLab.Enabled = true;
                            tlbbtn_Requisition.Enabled = true;
                            tlbbtn_Finish.Enabled = false;
                            gloUCLab_OrderDetail.PreferredLabActivity(true);
                        }


                        tlbbtn_ViewHistory.Visible = true;
                        oLabOrderRequest.Dispose();
                        oLabOrderRequest = null;
                    }
                    tlbbtn_ViewHistory.Visible = false;
                    tlbbtn_OnlySave.Enabled = true;
                    tlbbtn_Save.Enabled = true;
                    tlbbtn_Print.Enabled = true;
                    tlbbtn_Fax.Enabled = true;
                    //tlbbtn_Acknowledgment.Enabled = true;
                    tlbbtn_HL7.Enabled = true;
                    tlbbtn_EditOrder.Enabled = true;
                    tlbbtnRefresh.Enabled = true;
                    tblCCD.Enabled = true;
                }
                if (tabControl1.SelectedIndex == 2)
                {
                    tlbbtn_Save.Enabled = false;
                    tlbbtn_Print.Enabled = true;
                    tlbbtn_Fax.Enabled = false;
                    tlbbtn_VWAcknowledgment.Visible = false;
                    tlbbtn_Acknowledgment.Enabled = false;
                    tlbbtn_ViewHistory.Visible = false;
                    tlbbtn_ReviewAck.Visible = false;
                    tlbbtn_Finish.Enabled = false;
                    tlbbtn_HL7.Enabled = false;
                    tlbbtn_EditOrder.Enabled = false;
                    tlbbtn_Requisition.Enabled = false;
                    tlbbtn_OnlySave.Enabled = false;
                    tlbbtn_SendtoLab.Enabled = false; // by Abhijeet on date 20100401
                    tblCCD.Enabled = false;
                    tlbbtnRefresh.Enabled = false;
                    gloUCLab_OrderDetail.Visible = false;
                    gloUCLab_OrderDetail.Height = 111;//Added by madan on 20100506

                    if (GetLabFlowSheetDateSettings())
                    {
                        gloLabUC_LabFlowSheet1.SetData(_patientID, _dataBaseConnectionString, LabFlowSheetFromDt, LabFlowSheetToDt);
                    }
                    else
                    {
                        gloLabUC_LabFlowSheet1.SetData(_patientID, _dataBaseConnectionString, DateTime.Now.AddMonths(-6), DateTime.Now);
                    }
                }


                if (tabControl1.SelectedIndex == 3)
                {
                    tlbbtn_ViewHistory.Visible = true;
                    gloUCLab_OrderDetail.Visible = false;
                    tlbbtn_Print.Visible = true;
                    tlbbtn_Fax.Visible = true;
                    tblCCD.Enabled = true;
                    tlbbtn_EditOrder.Enabled = false;
                    tlbbtnRefresh.Enabled = false;
                    //Developer:Sanjog Dhamke
                    //Date:2 Feb 2012
                    //PRD Name: Lab Usability Change 6060/7000
                    //Reason: we have to show save & Save&Close button only if result set tab is selected
                    tlbbtn_Save.Enabled = false;
                    tlbbtn_OnlySave.Enabled = false;
                    if (oUc_ResultSet != null)
                    {
                        oUc_ResultSet.ShowTestData();
                    }

                    //Developer:Sandip Deshmukh
                    //Date:29 Oct 2013
                    //PRD Name: Lab Orders tabs binding is used for below buttons and on all button order will be saved.
                    //Reason: we have to show save & Save&Close button only if result set tab is selected
                    //***************
                    //Commented Code Bug #64499: 00000584 : PRINTING NOT AVAILABLE FROM RESULT SETS TAB
                    //tlbbtn_Print.Enabled = false;
                    tlbbtn_HL7.Enabled = false;
                    //****************

                }

                if (chk_PreviousHistory.Checked == false)
                {
                    gloLabUC_Transaction1.IsLoadLastTransaction = true;
                }
                else
                {
                    gloLabUC_Transaction1.IsLoadLastTransaction = false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void OpenDocument(long DocumentID)
        {
            try
            {
                if (DocumentID > 0)
                {
                    if (oViewDocument == null)
                    {
                        oViewDocument = new gloEDocumentV3.gloEDocV3Management();
                    }

                    oViewDocument.oPatientExam = objgloLabPatientExam;
                    oViewDocument.oPatientMessages = objgloLabPatientMessages;
                    oViewDocument.oPatientLetters = objgloLabPatientLetters;
                    oViewDocument.oNurseNotes = objgloLabNurseNotes;
                    oViewDocument.oHistory = objgloLabHistory;
                    oViewDocument.oLabs = objgloLabLabs;
                    oViewDocument.oDMS = objgloLabDMS;
                    oViewDocument.oRxmed = objgloLabRxmed;
                    oViewDocument.oOrders = objgloLabOrders;
                    oViewDocument.oProblemList = objgloLabProblemList;
                    oViewDocument.oCriteria = objgloLabCriteria;
                    oViewDocument.oWord = objgloLabWord;
                    //Bug #80137: gloEMR: Scan Docs- Null reference exception on acknowledge
                    oViewDocument.dMdi = this.MdiParent;

                    oViewDocument.ShowEDocument(_patientID, gloEDocumentV3.Enumeration.enum_OpenEDocumentAs.ViewDocumentForExternalModule, this.ParentForm, gloEDocumentV3.Enumeration.enum_OpenExternalSource.LabOrder, DocumentID);
                    oViewDocument.EvnRefreshDocuments += new gloEDocumentV3.gloEDocV3Management.RefreshDocuments(oViewDocument_EvnRefreshDocuments);

                    oViewDocument.EvnRefreshDocuments -= new gloEDocumentV3.gloEDocV3Management.RefreshDocuments(oViewDocument_EvnRefreshDocuments);
                    oViewDocument.Dispose();
                    oViewDocument = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }


        private void c1TestLibrary_BeforeRowColChange(object sender, C1.Win.C1FlexGrid.RangeEventArgs e)
        {
            try
            {
                if (_FormIsLoded)
                {
                    if (gloLabUC_Transaction1.LabModified || gloUCLab_OrderDetail.OrderModified)
                    {
                        OrderStatusRowToupdate = c1TestLibrary.RowSel;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void c1TestLibrary_RowColChange(object sender, EventArgs e)
        {

            string _strOrderType = "";
            long curOrderID = 0;
            try
            {


                if (_FormIsLoded)
                {
                    gloUCLab_TestDetail.Visible = false;
                    if (gloLabUC_Transaction1.LabModified || gloUCLab_OrderDetail.OrderModified)
                    {
                        if (_CurrentLockedOrder > 0)
                        {
                            if (UnLockOrder(_CurrentLockedOrder))
                            {
                                _CurrentLockedOrder = 0;
                            }
                        }

                        DialogResult drResult = MessageBox.Show("Do you want to save your changes?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (drResult == DialogResult.Yes)
                        {
                            _blnClosed = true;
                            IsClosed = true;
                            IsRowChange = true;
                            SaveOrder(0);

                            gloLabUC_Transaction1.LabModified = false;
                            gloUCLab_OrderDetail.OrderModified = false;
                        }
                        else if (drResult == DialogResult.No)
                        {
                            gloLabUC_Transaction1.LabModified = false;
                            gloUCLab_OrderDetail.OrderModified = false;
                            IsRowChange = false;
                            OrderStatusRowToupdate = c1TestLibrary.RowSel;
                        }

                    }
                    if (c1TestLibrary.RowSel > 0)
                    {

                        curOrderID = Convert.ToInt64(c1TestLibrary.GetData(c1TestLibrary.RowSel, COL_ORDERID));
                        if (curOrderID != 0)
                        {
                            if (tabControl1.SelectedIndex == 1)
                            {
                                gloUCLab_OrderDetail.Visible = false;
                            }
                            else
                            {
                                if (cmbOrderStatus.SelectedItem.ToString().Trim().ToLower() != "yes")
                                {
                                    gloUCLab_OrderDetail.Visible = true;
                                }
                            }

                            ShowOrders(curOrderID);
                            SendtoLab();
                            _strOrderType = GetOrderType(curOrderID);


                            if (_strOrderType == "Emr")
                            {
                                tlbbtn_Finish.Enabled = true;
                                tlbbtn_Requisition.Enabled = false;
                                gloUCLab_OrderDetail.PreferredLabActivity(false);
                            }
                            else
                            {
                                tlbbtn_Requisition.Enabled = true;
                                tlbbtn_Finish.Enabled = false;
                                gloUCLab_OrderDetail.PreferredLabActivity(true);
                            }

                            tlbbtn_ViewHistory.Visible = true;
                        }
                    }
                    else
                    {
                        c1TestLibrary.Select(c1TestLibrary.Rows.Count - 1, 0);

                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                curOrderID = 0;
            }

        }

        private void gloLabUC_Transaction1_gUC_ScanDocument(long TestID)
        {
            ArrayList _iLabDMSIDs = null;
            bool _result = false;
            try
            {
                // Removed Lab Category validation message
                _result = Set_ScanDocumentEvent(_OrderParamter.PatientID, ref _iLabDMSIDs);

                if (_result == true)
                {
                    //gloLabUC_Transaction1.AddScanDocument(TestID, _ScanDocumentID);
                    gloLabUC_Transaction1.AddScanDocument(TestID, _iLabDMSIDs);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }

        }
        //Added by Mayuri :20130320-To fill Lab task users
        private void glolabUC_Transaction1_gUC_OkButtonClicked(Int16 SendTaskType, Int16 ResultType)
        {
            DataTable dtUsers = GetLabTaskUserByProvider(_LabProviderID, SendTaskType, ResultType);
            gloUCLab_OrderDetail.FillLabTaskUsers(dtUsers);
        }
        public DataTable GetLabTaskUserByProvider(long ProviderId, Int16 SendTaskType, Int16 ResultType)
        {
            SqlConnection Conn = new SqlConnection(_dataBaseConnectionString);
            SqlCommand Cmd = null;
            SqlDataAdapter adpt = new SqlDataAdapter();
            DataTable dt = new DataTable();
            SqlParameter objParam = default(SqlParameter);

            try
            {
                Conn.Open();
                Cmd = new SqlCommand("gsp_GetLabTaskUsersForProvider", Conn);

                Cmd.CommandType = CommandType.StoredProcedure;
                adpt.SelectCommand = Cmd;



                objParam = Cmd.Parameters.Add("@nProviderID", SqlDbType.BigInt);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = ProviderId;


                objParam = Cmd.Parameters.Add("@nSendTaskType", SqlDbType.SmallInt);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = SendTaskType;

                objParam = Cmd.Parameters.Add("@nResultType", SqlDbType.SmallInt);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = ResultType;

                adpt.Fill(dt);
                Conn.Close();
                if ((Cmd != null))
                {
                    Cmd.Parameters.Clear();
                    Cmd.Dispose();
                    Cmd = null;
                }
                objParam = null;

                return dt;

            }
            catch (SqlException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Load, "ClsDiagnosisDBLayer -- FillICD -- " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return dt;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Load, "ClsDiagnosisDBLayer -- FillICD -- " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return dt;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
                if ((Conn != null))
                {
                    Conn.Dispose();
                    Conn = null;
                }
                if ((adpt != null))
                {
                    adpt.Dispose();
                    adpt = null;
                }
                if (objParam != null)
                {
                    objParam = null;
                }
                if (Cmd != null)
                {
                    Cmd.Parameters.Clear();
                    Cmd.Dispose();
                    Cmd = null;
                }

                //if ((dt != null))
                //{
                //    dt.Dispose();
                //    dt = null;
                //}
            }
        }
        // private bool Set_ScanDocumentEvent(Int64 PatientID, string LabCategory, ref Int64 ScanContainerID, ref Int64 ScanDocumentID)
        private bool Set_ScanDocumentEvent(Int64 PatientID, ref ArrayList _iLabDMSIDs)
        {
            gloEDocumentV3.gloEDocV3Management oScanDocument = new gloEDocumentV3.gloEDocV3Management();
            bool _result = false;
            //ArrayList _DMSIDs = null;
            try
            {
                // _result = oScanDocument.ShowEScanner(_patientID, LabCategory, DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), _ClinicID, gloEDocumentV3.Enumeration.enum_DocumentEventType.ScanDocument, out ScanContainerID, out ScanDocumentID);
                //Bug #80137: gloEMR: Scan Docs- Null reference exception on acknowledge
                oScanDocument.dMdi = this.MdiParent;
                _result = oScanDocument.ShowEDocument_LabOrder(_patientID, gloEDocumentV3.Enumeration.enum_OpenEDocumentAs.ScanDocument, null, gloEDocumentV3.Enumeration.enum_OpenExternalSource.LabOrder, 0, out _iLabDMSIDs);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                oScanDocument.Dispose();
            }
            return _result;
        }

        private void gloLabUC_Transaction1_gUC_ViewDocument(long TestID, long DocumentID)
        {
            try
            {
                if (DocumentID > 0)
                {
                    if (oViewDocument == null)
                    {
                        oViewDocument = new gloEDocumentV3.gloEDocV3Management();
                    }

                    oViewDocument.oPatientExam = objgloLabPatientExam;
                    oViewDocument.oPatientMessages = objgloLabPatientMessages;
                    oViewDocument.oPatientLetters = objgloLabPatientLetters;
                    oViewDocument.oNurseNotes = objgloLabNurseNotes;
                    oViewDocument.oHistory = objgloLabHistory;
                    oViewDocument.oLabs = objgloLabLabs;
                    oViewDocument.oDMS = objgloLabDMS;
                    oViewDocument.oRxmed = objgloLabRxmed;
                    oViewDocument.oOrders = objgloLabOrders;
                    oViewDocument.oProblemList = objgloLabProblemList;
                    oViewDocument.oCriteria = objgloLabCriteria;
                    oViewDocument.oWord = objgloLabWord;
                    //Bug #80137: gloEMR: Scan Docs- Null reference exception on acknowledge
                    oViewDocument.dMdi = this.MdiParent;

                    oViewDocument.ShowEDocument(_patientID, gloEDocumentV3.Enumeration.enum_OpenEDocumentAs.ViewDocumentForExternalModule, this.ParentForm, gloEDocumentV3.Enumeration.enum_OpenExternalSource.LabOrder, DocumentID);
                    oViewDocument.EvnRefreshDocuments += new gloEDocumentV3.gloEDocV3Management.RefreshDocuments(oViewDocument_EvnRefreshDocuments);
                    oViewDocument.EvnRefreshDocuments -= new gloEDocumentV3.gloEDocV3Management.RefreshDocuments(oViewDocument_EvnRefreshDocuments);
                    oViewDocument.Dispose();
                    oViewDocument = null; //added for memory management
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);

            }

        }

        private void ShowAckwButton(Boolean show)
        {
            tlbbtn_ViewHistory.Visible = show;
            //Developer:Sanjog Dhamke
            //Date:2 Feb 2012
            //PRD Name: Lab Usability Change 6060/7000
            //Reason: we have to show Fax & Print button only if order is present in left pane
            tlbbtn_Fax.Enabled = show;
            tlbbtn_Print.Enabled = show;
            if (tabControl1.SelectedIndex == 3)
            {
                //Commented Code Bug #64499: 00000584 : PRINTING NOT AVAILABLE FROM RESULT SETS TAB
                //tlbbtn_Print.Enabled = false;              
                tlbbtn_HL7.Enabled = false;
            }
        }
        void oViewDocument_EvnRefreshDocuments()
        {

        }

        private void GetSettings()
        {
            gloDatabaseLayer.DBLayer objDbLayer = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString.ToString().Trim());
            //  DataTable dtSettings = new DataTable();    not used
            string strQry = "select sSettingsValue from settings where sSettingsName='LAB CATEGORY'";
            try
            {
                objDbLayer.Connect(false);
                _DMSCategory_Labs = Convert.ToString(objDbLayer.ExecuteScalar_Query(strQry));

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (objDbLayer != null)
                {
                    objDbLayer.Dispose();
                    objDbLayer = null; //added for memory management 
                }
            }
        }

        private void gloLabUC_Transaction1_gUC_ButtonDiagnCPTClicked()
        {
            Int64 _VisitID = 0;
            try
            {
                if (CheckLockStatus(LabOrderParameter.OrderID))
                {
                    return;
                }
                //Bug #60825: 00000587 : creating an emdeon order the system is later updating the nVisitID to '0'                
                _VisitID = GenerateVisitID(DateTime.Now);
                {
                    if (clsEmdeonGeneral.gblnICD9Driven)
                    {
                        frm_Diagnosis frm = new frm_Diagnosis(_VisitID, 0, false, _patientID);
                        frm.StartPosition = FormStartPosition.CenterScreen;
                        frm.ShowInTaskbar = false;
                        frm.ShowDialog(this);
                        frm.Dispose();
                        frm = null;
                    }
                    else
                    {
                        frm_Treatment oTreatment = new frm_Treatment(0, _VisitID, DateTime.Now, "", false, _patientID);
                        oTreatment.ShowDialog(this);
                        oTreatment.Dispose();
                        oTreatment = null;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                throw;
            }

        }
        public long GetVisitID(System.DateTime VisitDate, long patientID)
        {
            long _retvisitID = 0;
            gloDatabaseLayer.DBLayer oDBLayer = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            gloDatabaseLayer.DBParameters oDBparams = new gloDatabaseLayer.DBParameters();
            object objRet = null;
            try
            {
                oDBLayer.Connect(false);
                oDBparams.Clear();

                oDBparams.Add("@visitDate", VisitDate, ParameterDirection.Input, SqlDbType.DateTime);
                oDBparams.Add("@PatientID", patientID, ParameterDirection.Input, SqlDbType.BigInt, 18);

                objRet = oDBLayer.ExecuteScalar("gsp_GetVisitID", oDBparams);
                if (objRet != null)
                {
                    _retvisitID = Convert.ToInt64(objRet);
                }
                //return _retvisitID ;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDBLayer != null)
                {
                    oDBLayer.Dispose();
                    oDBLayer = null; //added for memory management 
                }
                if (oDBparams != null)
                { oDBparams.Clear(); oDBparams.Dispose(); oDBparams = null; }
            }
            return _retvisitID;
        }

        public Int64 GetProviderIDForUser(Int64 UserID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            Int64 ProID = 0;
            try
            {
                oDB.Connect(false);
                ProID = Convert.ToInt64(oDB.ExecuteScalar_Query("SELECT nProviderID from user_mst where nUserID = " + UserID + ""));
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ProID = 0;
            }
            finally
            {
                oDB.Dispose();
                oDB = null; //added for memory management
            }
            return ProID;
        }



        public bool changePatientProvider(Int64 nPatientProviderID, Int64 nPatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            string strQry = string.Empty;
            int _Result = 0;
            Boolean _boolResult = false;
            try
            {
                if (nPatientProviderID != 0)
                {
                    oDB.Connect(false);

                    strQry = "update Patient set nProviderID='" + nPatientProviderID + "' where nPatientID='" + nPatientID + "' ";
                    _Result = oDB.Execute_Query(strQry);
                    if (_Result < 0)
                        _boolResult = false;
                    else if (_Result >= 0)
                        _boolResult = true;
                    else
                        _boolResult = false;
                    oDB.Disconnect();
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                _boolResult = false;
            }
            finally
            {
                oDB.Dispose(); //added for memory management
                oDB = null;
            }
            return _boolResult;
        }

        public Int64 GetTaskID_OfLab(Int64 LabOrderID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            Int64 LabID = 0;
            string strQry = string.Empty;
            object _objResult = null;
            try
            {
                oDB.Connect(false);
                strQry = "SELECT nTaskID FROM TM_TaskMST WHERE nReferenceID1 = " + LabOrderID;
                _objResult = oDB.ExecuteScalar_Query(strQry);
                if (_objResult != null && _objResult.ToString() != "")
                {
                    LabID = Convert.ToInt64(_objResult);
                }
                else
                {
                    LabID = 0;
                }
                oDB.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                LabID = 0;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                }
                strQry = string.Empty;
                _objResult = null;
            }
            return LabID;
        }
        private void AddTasks(gloEMRGeneralLibrary.gloEMRActors.LabActor.ItemDetails Users, DateTime TaskDate, long PatientID, long TaskId, string TaskDesc, DateTime TaskDueDate, gloEMRGeneralLibrary.gloEMRActors.LabActor.LabOrder _LabOrder)
        {
            Boolean _isNormal = true;
            Boolean _isOrderwithTests = true;

            //Int64[] ArrTasks = new Int64[Users.Count];
            int i = 0;

            //for (i = 0; i <= Users.Count - 1; i++)
            //{
            //    ArrTasks[i] = Users.get_Item(i).ID;
            //}

            gloTaskMail.gloTask ogloTask = new gloTaskMail.gloTask(_dataBaseConnectionString);
            gloTaskMail.Task oTask = new gloTaskMail.Task();
            gloTaskMail.TaskProgress oTaskProgress = new gloTaskMail.TaskProgress();

            for (i = 0; i <= Users.Count - 1; i++)
            {
                gloEMRGeneralLibrary.gloEMRActors.LabActor.ItemDetail userDetail = Users.get_Item(i);
                if (userDetail != null)
                {
                    gloTaskMail.TaskAssign oTaskAssign = new gloTaskMail.TaskAssign();

                    oTaskAssign.AssignFromID = _LoginUserID;
                    oTaskAssign.AssignFromName = GetLoginUserName(_LoginUserID);
                    oTaskAssign.AssignToID = userDetail.ID;
                    if (oTaskAssign.AssignFromID == oTaskAssign.AssignToID)
                    {
                        oTaskAssign.SelfAssigned = gloTasksMails.Common.SelfAssigned.Self;
                        oTaskAssign.AcceptRejectHold = gloTasksMails.Common.AcceptRejectHold.Accept;
                    }
                    else
                    {
                        oTaskAssign.SelfAssigned = gloTasksMails.Common.SelfAssigned.Assigned;
                        oTaskAssign.AcceptRejectHold = gloTasksMails.Common.AcceptRejectHold.Hold;
                    }
                    oTaskAssign.AssignToName = userDetail.Description;
                    oTaskAssign.ClinicID = _ClinicID;

                    oTask.Assignment.Add(oTaskAssign);
                }
            }

            oTaskProgress.ClinicID = _ClinicID;
            oTaskProgress.Complete = 0;
            oTaskProgress.DateTime = TaskDate;
            oTaskProgress.Description = TaskDesc;
            oTaskProgress.StatusID = 1;
            oTaskProgress.TaskID = TaskId;

            oTask.TaskID = TaskId;
            oTask.UserID = _LoginUserID;
            oTask.TaskType = gloTaskMail.TaskType.LabOrder;
            oTask.PatientID = PatientID;

            //
            if (oLabActor_Order1.ArrTestName != null)
            {
                //for (int j = 0; j <= oLabActor_Order1.ArrTestName.Count - 1; j++)
                //{
                if (oLabActor_Order1.OrderTests.Count > 0 && oLabActor_Order1.OrderTests.get_Item(0).OrderTestResults.Count > 0)
                {
                    _isOrderwithTests = false;
                    if (Users.Count > 0)
                    {
                        for (i = 0; i <= Users.Count - 1; i++)
                        {

                            if (Users.get_Item(i).Code != "Normal")
                            {
                                _isNormal = false;
                                break;
                            }
                        }
                    }
                    //    break;
                }
                //else
                //{
                //    _isOrderwithTests = true;

                //    break;
                //}

                //}
            }
            else
            {
                _isOrderwithTests = false;
            }
            //SLR: Check this Logic 4/22/2014

            if (_isNormal == false)
            {
                oTask.PriorityID = 3;

                oTask.Subject = "Lab Results (Abnormal) Available for Order " + "ORD" + "-" + _LabOrder.OrderNoID;
            }
            else if (_isOrderwithTests == true)
            {
                oTask.PriorityID = 1;
                oTask.Subject = "Lab order assigned ";
            }
            else
            {
                oTask.PriorityID = 1;
                oTask.Subject = "Lab Results Available for Order " + "ORD" + "-" + _LabOrder.OrderNoID;
            }
            //
            // oTask.Subject = "Lab order assigned ";
            oTask.ClinicID = _ClinicID;
            oTask.DateCreated = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(TaskDate));
            oTask.StartDate = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(TaskDate));
            oTask.DueDate = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(TaskDueDate));
            oTask.IsPrivate = false;
            oTask.MachineName = Environment.MachineName;
            oTask.Progress = oTaskProgress;
            oTask.ReferenceID1 = _OrderParamter.OrderID;
            oTask.ProviderID = _PatientProviderID;
            oTask.TaskGroupID = ogloTask.GetUniqueueId();


            if (TaskId == 0)
            {
                ogloTask.Add(oTask);
            }
            else
            {
                ogloTask.Modify(oTask);
            }
          //  ArrTasks = null;
            ogloTask.Dispose();

            oTask.Dispose();
            oTaskProgress.Dispose();

        }
        public string GetLoginUserName(Int64 UserID)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            string _strLoginUserName = string.Empty;
            try
            {
                oDB.Connect(false);
                _strLoginUserName = Convert.ToString(oDB.ExecuteScalar_Query("Select sLoginName from dbo.User_MST where nUserID =" + UserID + ""));
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                _strLoginUserName = "";
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null; //added for memory management
                }
            }
            return _strLoginUserName;
        }

        private void dtPickerToDate_ValueChanged(object sender, EventArgs e)
        {
            if (gloLabUC_Transaction1.LabModified || gloUCLab_OrderDetail.OrderModified)
            {
                if (_CurrentLockedOrder > 0)
                {
                    if (UnLockOrder(_CurrentLockedOrder))
                    {
                        _CurrentLockedOrder = 0;
                    }
                }

                DialogResult drResult = MessageBox.Show("Do you want to save your changes?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (drResult == DialogResult.Yes)
                {
                    _blnClosed = true;
                    IsClosed = true;
                    SaveOrder(0);

                    gloLabUC_Transaction1.LabModified = false;
                    gloUCLab_OrderDetail.OrderModified = false;

                }
                else if (drResult == DialogResult.No)
                {

                    gloLabUC_Transaction1.LabModified = false;
                    gloUCLab_OrderDetail.OrderModified = false;
                }

            }

            if (_FormIsLoded && CallRefreshGrid)
            {
                RefershAllGrids();
            }
        }

        private void cmbOrderStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_FormIsLoded)
            {
                RefershAllGrids();
            }
        }

        private void dtPickerFromDate_ValueChanged(object sender, EventArgs e)
        {
            if (gloLabUC_Transaction1.LabModified || gloUCLab_OrderDetail.OrderModified)
            {
                if (_CurrentLockedOrder > 0)
                {
                    if (UnLockOrder(_CurrentLockedOrder))
                    {
                        _CurrentLockedOrder = 0;
                    }
                }
                DialogResult drResult = MessageBox.Show("Do you want to save your changes?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (drResult == DialogResult.Yes)
                {
                    _blnClosed = true;
                    IsClosed = true;
                    SaveOrder(0);

                    gloLabUC_Transaction1.LabModified = false;
                    gloUCLab_OrderDetail.OrderModified = false;

                }
                else if (drResult == DialogResult.No)
                {
                    gloUCLab_OrderDetail.OrderModified = false;
                    gloLabUC_Transaction1.LabModified = false;
                }

            }
            if (_FormIsLoded && CallRefreshGrid)
            {
                RefershAllGrids();
            }
        }

        private void RefershAllGrids()
        {
            try
            {
                HideAllAckButtons();
                gloLabUC_Transaction1.ClearTest();
                fillOpenOrdsGrid_New();

                if (c1TestLibrary.Rows.Count > 1 && c1TestLibrary.RowSel > 0)
                {
                    long curOrderID = Convert.ToInt64(c1TestLibrary.GetData(c1TestLibrary.RowSel, COL_ORDERID));
                    //   gloEMRGeneralLibrary.gloEMRLab.gloEMRLabOrder oLabOrderRequest = new gloEMRGeneralLibrary.gloEMRLab.gloEMRLabOrder();
                    if (tabControl1.SelectedIndex == 1 || tabControl1.SelectedIndex == 3)
                    {
                        gloUCLab_OrderDetail.Visible = false;
                    }
                    else
                    {
                        gloUCLab_OrderDetail.Visible = true;
                    }
                    ShowOrders(curOrderID);
                    string _strOrderType = GetOrderType(curOrderID);
                    if (_strOrderType == "Emr")
                    {
                        tlbbtn_Finish.Enabled = true;
                        tlbbtn_Requisition.Enabled = false;
                        gloUCLab_OrderDetail.PreferredLabActivity(false);
                    }
                    else
                    {
                        gloUCLab_OrderDetail.PreferredLabActivity(true);
                        tlbbtn_Finish.Enabled = false;
                        tlbbtn_Requisition.Enabled = true;
                    }
                    //Added on 20150715-To hide acknoledgement button if results or labflosheet tab selected and returning from clinical chart or merge order screen
                    if (tabControl1.SelectedIndex == 1 || tabControl1.SelectedIndex == 2)
                    {
                        tlbbtn_ViewHistory.Visible = false;
                    }
                    else
                    {
                        tlbbtn_ViewHistory.Visible = true;
                    }
                }
                else
                {
                    tlbbtn_ViewHistory.Visible = false;
                    gloUCLab_OrderDetail.Visible = false;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);

            }
        }

        private void cmbOrderStatus_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (gloLabUC_Transaction1.LabModified || gloUCLab_OrderDetail.OrderModified)
            {
                if (_CurrentLockedOrder > 0)
                {
                    if (UnLockOrder(_CurrentLockedOrder))
                    {
                        _CurrentLockedOrder = 0;
                    }
                }
                DialogResult drResult = MessageBox.Show("Do you want to save your changes?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (drResult == DialogResult.Yes)
                {
                    _blnClosed = true;
                    IsClosed = true;
                    SaveOrder(0);

                    gloUCLab_OrderDetail.OrderModified = false;
                    gloLabUC_Transaction1.LabModified = false;

                }
                else if (drResult == DialogResult.No)
                {
                    gloUCLab_OrderDetail.OrderModified = false;
                    gloLabUC_Transaction1.LabModified = false;
                }

            }
        }

        private void HideAllAckButtons()
        {
            try
            {
                //tlbbtn_Acknowledgment.Visible = false;
                tlbbtn_VWAcknowledgment.Visible = false;
                tlbbtn_ReviewAck.Visible = false;//Added by madan on 20100510
                tlbbtn_ViewHistory.Visible = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }
        //added by madan on 20100512
        private bool CheckResultsForOrder(long OrderID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            bool _boolResult = false;
            string strQuery = string.Empty;
            object _objResult = null;
            try
            {
                oDB.Connect(false);
                strQuery = "select count(*) from  dbo.Lab_Order_Test_Result where labotr_OrderID=" + OrderID;
                _objResult = oDB.ExecuteScalar_Query(strQuery);
                if (_objResult != null && _objResult.ToString() != "")
                {
                    Int32 count = Convert.ToInt32(_objResult);
                    if (count > 0)
                    {
                        _boolResult = true;
                    }
                    else
                    {
                        _boolResult = false;
                    }
                    oDB.Disconnect();

                }
                else
                {
                    _boolResult = false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                _boolResult = false;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                }
                strQuery = string.Empty;
            }
            return _boolResult;
        }
        //Added by madan on 20100518
        #region Events
        private void frmViewgloLab_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_blnClosed == false)
            {
                _blnClosed = true;
                if (gloLabUC_Transaction1.LabModified || gloUCLab_OrderDetail.OrderModified)
                {

                    DialogResult oResult = MessageBox.Show("Do you want to save your changes?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (oResult == DialogResult.Yes)
                    {
                        IsClosed = true;
                        SaveOrder(0);

                        e.Cancel = false;
                    }
                    else if (oResult == DialogResult.No)
                    {
                        if (_CurrentLockedOrder > 0)
                        {
                            if (UnLockOrder(_CurrentLockedOrder))
                            {
                                _CurrentLockedOrder = 0;
                            }
                        }
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Close, "View Labs closed without saving", _patientID, _OrderParamter.OrderID, _PatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                        e.Cancel = false;
                    }
                    else if (oResult == DialogResult.Cancel)
                    {
                        gloUCLab_OrderDetail.OrderModified = true;
                        gloLabUC_Transaction1.LabModified = true;
                        _blnClosed = false;
                        e.Cancel = true;
                    }

                }
                else
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Close, "View Labs closed", _patientID, 0, _PatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                    e.Cancel = false;
                }
            }
            SaveLabFlowSheetDates();
            
            try
            {
              //code added for split control 
                if (clsSplit_Laborder != null)
                {
            //     clsSplit_Laborder.loadSplitControlData(_patientID, VisitID, uiPanSplitScreen_LabOrder.SelectedPanel.Name, objCriteria, objWord, _ClinicID);
                    clsSplit_Laborder.SaveControlDisplaySettings();
                }
            }
            catch
            {
            }
        }

        private void frmViewgloLab_Resize(object sender, EventArgs e)
        {

        }

        private void gloUCLab_TestDetail_gUC_InstructionChanged(long TestID, string sData)
        {
            gloLabUC_Transaction1.AddInstruction(TestID, sData);
        }

        private void gloUCLab_TestDetail_gUC_PrecuationChanged(long TestID, string sData)
        {
            gloLabUC_Transaction1.AddPrecuation(TestID, sData);
        }

        private void c1TestLibrary_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
            
                try
                {
                    if (OContextMenu != null)
                    {
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(OContextMenu);
                        if (OContextMenu.MenuItems != null)
                        {
                            OContextMenu.MenuItems.Clear();
                        }
                        OContextMenu.Dispose();
                        OContextMenu = null;
                    }
                }
                catch
                {
                }

                try
                {
                    if (c1TestLibrary.ContextMenu != null)
                    {
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(c1TestLibrary.ContextMenu);
                        if (c1TestLibrary.ContextMenu.MenuItems != null)
                        {
                            c1TestLibrary.ContextMenu.MenuItems.Clear();
                        }
                        c1TestLibrary.ContextMenu.Dispose();
                        c1TestLibrary.ContextMenu = null;
                    }
                }
                catch
                {
                }
              
            }
            catch
            {
            }

            //20140407 manoj V8022 PRD: View Lab Order Comments  
            try
            {
                if ((c1TestLibrary.RowSel > 0) && (!String.IsNullOrEmpty(Convert.ToString(c1TestLibrary.GetData(c1TestLibrary.RowSel, COL_ORDERCOMMENTS)))))
                {
                    gloLabUC_Transaction1.ShowLabOrderComments(c1TestLibrary.GetData(c1TestLibrary.RowSel, COL_ORDERCOMMENTS).ToString());
                }
            }
            catch
            { }


            long _curOrderID = 0;
            string _OrderType = string.Empty;
            string _OrderStatus = string.Empty;
            string _CustomOrderStatus = string.Empty;
            Point p = new Point();
            try
            {

                if (e.Button == MouseButtons.Right)
                {
                    if (gloLabUC_Transaction1.LabModified || gloUCLab_OrderDetail.OrderModified)
                    {
                        if (CheckStatusOfOrder() == false)
                        {
                            return;
                        }
                    }

                    c1TestLibrary.Focus();
                    c1TestLibrary.Select(c1TestLibrary.MouseRow, c1TestLibrary.MouseCol, true);
                    if (c1TestLibrary.RowSel > 0)
                    {
                        _curOrderID = Convert.ToInt64(c1TestLibrary.GetData(c1TestLibrary.RowSel, COL_ORDERID));
                        _OrderStatus = Convert.ToString(c1TestLibrary.GetData(c1TestLibrary.RowSel, COL_ORDERSTATUS));
                        _CustomOrderStatus = Convert.ToString(c1TestLibrary.GetData(c1TestLibrary.RowSel, COL_CUSTOMORDERSTATUS));
                    }
                    else
                    {
                        c1TestLibrary.Select(c1TestLibrary.Rows.Count - 1, 0);
                        return;
                    }
                    if (_curOrderID > 0)
                    {

                        if (CheckLockStatus(_curOrderID))
                        {
                            return;
                        }
                        _OrderType = GetOrderType(_curOrderID);
                        try
                        {
                            if (OContextMenu != null)
                            {
                                gloGlobal.cEventHelper.RemoveAllEventHandlers(OContextMenu);
                                if (OContextMenu.MenuItems != null)
                                {
                                    OContextMenu.MenuItems.Clear();
                                }
                                OContextMenu.Dispose();
                                OContextMenu = null;
                            }
                        }
                        catch
                        {
                        }

                        OContextMenu = new ContextMenu();

                        MenuItem oMenuItem = null;
                        MenuItem oMenuOrderStatus = null;
                        MenuItem oMenuProviders = null;

                        if (_OrderType == "Emr")
                        {
                            OContextMenu.MenuItems.Clear();

                            oMenuItem = new MenuItem();
                            oMenuItem.Text = "Modify Order";
                            OContextMenu.MenuItems.Add(oMenuItem);
                            oMenuItem.Click += Set_Menu_ModifyOrder;

                            if (IsResultsExsists(_curOrderID) == false)
                            {
                                if (_CustomOrderStatus != "Cancelled")
                                {
                                    oMenuItem = new MenuItem();
                                    oMenuItem.Text = "Split Order";
                                    oMenuItem.Enabled = true;
                                    OContextMenu.MenuItems.Add(oMenuItem);
                                    oMenuItem.Click += Set_SplitOrder;

                                    oMenuItem = new MenuItem();
                                    oMenuItem.Text = "Send Through Emdeon";
                                    oMenuItem.Enabled = true;
                                    OContextMenu.MenuItems.Add(oMenuItem);
                                    oMenuItem.Click += Set_ResendThroughEmdeon;
                                }
                            }
                        }
                        else
                        {
                            if (!_OrderStatus.Equals("Sent to Lab"))
                            {
                                OContextMenu.MenuItems.Clear();

                                oMenuItem = new MenuItem();
                                oMenuItem.Text = "Modify Order";
                                OContextMenu.MenuItems.Add(oMenuItem);
                                oMenuItem.Click += Set_Menu_ModifyEmdeonOrder;
                            }

                            oMenuItem = new MenuItem();
                            oMenuItem.Text = "Print Requisition";
                            oMenuItem.Enabled = true;
                            OContextMenu.MenuItems.Add(oMenuItem);
                            oMenuItem.Click += Set_Menu_PrintRequisition;

                        }
                        oMenuItem = new MenuItem();
                        oMenuItem.Text = "Print Order";
                        OContextMenu.MenuItems.Add(oMenuItem);

                        oMenuItem.Click += new EventHandler(oMenuItem_Click);

                        //28-May-13 Aniket: Orders PRD Changes
                        //Code to add Custom Order Status

                        DataTable dtOrderStatus;

                        oMenuItem = new MenuItem();
                        oMenuItem.Text = "Set Order Status";

                        dtOrderStatus = GetOrderStatus();

                        Int16 intOrderStatusCount;

                        for (intOrderStatusCount = 0; intOrderStatusCount <= dtOrderStatus.Rows.Count - 1; intOrderStatusCount++)
                        {
                            oMenuOrderStatus = new MenuItem();
                            oMenuOrderStatus.Text = dtOrderStatus.Rows[intOrderStatusCount]["OrderStatus"].ToString();
                            oMenuOrderStatus.Tag = dtOrderStatus.Rows[intOrderStatusCount]["OrderStatusNumber"].ToString() + ":" + _curOrderID.ToString();
                            oMenuItem.MenuItems.Add(oMenuOrderStatus);
                            oMenuOrderStatus.Click += Set_OrderStatus;
                        }

                        OContextMenu.MenuItems.Add(oMenuItem);

                        if (dtOrderStatus != null)  //change made for memory issue
                        {
                            dtOrderStatus.Dispose();
                            dtOrderStatus = null;
                        }
                        //28-May-13 Aniket: Orders PRD Changes
                        //Code to add Custom Order Status


                        //24-Apr-14 Aniket: Change Order Provider

                        DataTable dtProviders;

                        oMenuItem = new MenuItem();
                        oMenuItem.Text = "Change Ordering Provider";

                        dtProviders = GetProviders();

                        Int16 intProviderCount;

                        for (intProviderCount = 0; intProviderCount <= dtProviders.Rows.Count - 1; intProviderCount++)
                        {
                            oMenuProviders = new MenuItem();
                            oMenuProviders.Text = dtProviders.Rows[intProviderCount]["ProviderName"].ToString();
                            oMenuProviders.Tag = dtProviders.Rows[intProviderCount]["nProviderID"].ToString() + ":" + _curOrderID.ToString();
                            oMenuItem.MenuItems.Add(oMenuProviders);
                            oMenuProviders.Click += Set_OrderProvider;
                        }

                        OContextMenu.MenuItems.Add(oMenuItem);

                        if (dtProviders != null) //changes made for memory issue
                        {
                            dtProviders.Dispose();
                            dtProviders = null;
                        }
                        //24-Apr-14 Aniket: Change Order Provider

                        p.X = e.X;
                        p.Y = e.Y;
                        if (c1TestLibrary.ContextMenu != null)
                        {
                            c1TestLibrary.ContextMenu.Dispose();
                            c1TestLibrary.ContextMenu = null;
                        }
                        OContextMenu.Show(c1TestLibrary, p);

                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                _curOrderID = 0;

                _OrderType = string.Empty;

            }


        }
        //Added by madan on 20100616
        void oMenuItem_Click(object sender, EventArgs e)
        {
            MenuEvent_Print();
        }
        //Added by madan on 20100601
        void Set_Menu_ModifyOrder(object sender, EventArgs e)
        {
            if (LockOrder(LabOrderParameter.OrderID))
            {
                return;
            }

            //Problem : Bug #41352: 00000342 : Lab Orders 
            //Change : Check whether the order is came from HL7 or not. If yes then display the pop up message.
            CheckExternalOrder(LabOrderParameter.OrderID);

            frmViewNormalLab frmNormalLab = new frmViewNormalLab(_patientID);
            frmNormalLab.Event_CallCDA += Raise_EvntGenerateCDA_GloLab;
            frmNormalLab.LabOrderParameter.OrderID = LabOrderParameter.OrderID;
            frmNormalLab.LabOrderParameter.OrderNumberID = LabOrderParameter.OrderNumberID;
            frmNormalLab.LabOrderParameter.OrderNumberPrefix = LabOrderParameter.OrderNumberPrefix;
            frmNormalLab.LabOrderParameter.IsEditMode = LabOrderParameter.IsEditMode;
            frmNormalLab.LabOrderParameter.PatientID = LabOrderParameter.PatientID;
            frmNormalLab.LabOrderParameter.ProviderID = LabOrderParameter.ProviderID;
            frmNormalLab.LabOrderParameter.TransactionDate = LabOrderParameter.TransactionDate;
            frmNormalLab.LabOrderParameter.TransactionType = LabOrderParameter.TransactionType;
            frmNormalLab.LabOrderParameter.VisitID = LabOrderParameter.VisitID;

            frmNormalLab.WindowState = FormWindowState.Maximized;
            frmNormalLab.ShowInTaskbar = false;
            frmNormalLab.BringToFront();
            frmNormalLab.objPatientExam = objgloLabPatientExam;

            frmNormalLab.objPatientMessages = objgloLabPatientMessages;
            frmNormalLab.objPatientLetters = objgloLabPatientLetters;
            frmNormalLab.objNurseNotes = objgloLabNurseNotes;
            frmNormalLab.objHistory = objgloLabHistory;
            frmNormalLab.objLabs = objgloLabLabs;
            frmNormalLab.objDMS = objgloLabDMS;
            frmNormalLab.objRxmed = objgloLabRxmed;
            frmNormalLab.objOrders = objgloLabOrders;
            frmNormalLab.objProblemList = objgloLabProblemList;
            frmNormalLab.DirectAddress = DirectAddress;
            frmNormalLab.SecureMsgEnable = SecureMsgEnable;
            frmNormalLab.SecureMsgUserright = SecureMsgUserright;
            frmNormalLab.objCriteria = objgloLabCriteria;
            frmNormalLab.objWord = objgloLabWord;
            frmNormalLab.eventLabEducation = getPatientedu;
            //Bug #80137: gloEMR: Scan Docs- Null reference exception on acknowledge
            frmNormalLab.dMdi = this.MdiParent;
            frmNormalLab.ShowDialog(this);
            frmNormalLab.Event_CallCDA -= Raise_EvntGenerateCDA_GloLab;
            frmNormalLab.Dispose();
            frmNormalLab = null;
            ShowOrders(LabOrderParameter.OrderID);
            //Added by Mayuri:20140521-To refresh grid after modify order from right click menu also.
            RefershAllGrids();
        }




        public void ShowModifyOrderFromDashboard()  //added to show modified order from dashboard
        {
            //code change to resolve bugid 70287,70291
            if (CheckLockStatus(LabOrderParameter.OrderID))
            {
                return;
            }

            if (GetOrderType(LabOrderParameter.OrderID) == "Emr")
            {
                Set_Menu_ModifyOrder(null, null);

            }
            else
            {
                if (_IsDemoLab == true)
                {
                    return;
                }
                if (LaunchOrderEditScreen(LabOrderParameter.OrderID))
                {
                    //use to refresh grid data while opening data for modification through Emdeon, 8020 order PRD
                    RefershAllGrids();
                    ShowOrders(LabOrderParameter.OrderID);
                    try
                    {



                    }
                    catch (Exception ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                    }
                }
            }




        }






        void Set_SplitOrder(object sender, EventArgs e)
        {

            if (LockOrder(LabOrderParameter.OrderID))
            {
                return;
            }
            //Added for Bug #87928: CR00000369 : Lab orders coming back into glo are not matching up to the pending/sent order 
            clsGetGloLabData objclsGetGloLabData = new clsGetGloLabData();
            isSplitOrder = true;
            objclsGetGloLabData.IsSplitOrder = isSplitOrder;
            frmViewOrderTests frmOrderTests = new frmViewOrderTests();
            frmOrderTests.OrderID = LabOrderParameter.OrderID;
            if (frmOrderTests.ShowDialog(this) == DialogResult.OK)
            {
                if (frmOrderTests.sTestsID != null)
                {
                    if (Convert.ToString(frmOrderTests.sTestsID) != "")
                    {
                        
                        LaunchEmdeonScreen(LabOrderParameter.OrderID, frmOrderTests.sTestsID, frmOrderTests._TestNames); //testnames added to show testnames on emdeon screen 
                    }
                }
                if (frmOrderTests != null)
                {
                    frmOrderTests.Dispose();
                    frmOrderTests = null;
                }

                CallRefreshGrid = false;
                dtPickerToDate.Value = System.DateTime.Now;
                dtPickerFromDate.Value = System.DateTime.Now.AddMonths(-12);
                CallRefreshGrid = true;
                RefershAllGrids();
                gloUC_TransactionHistory1.LoadPreviousLabs(_patientID, DateTime.Now);
                gloUC_TransactionHistory1.DesignTestGrid();
                gloUC_TransactionHistory1.HideCloseButton = false;
                gloUC_TransactionHistory1.SetDataByDate(DateTime.Now.Date, DateTime.Now.Date);
                gloUC_TransactionHistory1.cmbCriteria.Text = "Date";

            }
            if (clsEmdeonGeneral.gloLab_IsOrderLocked && LabOrderParameter.OrderID > 0)
            {
                clsGeneral objClsGeneral = new clsGeneral();
                objClsGeneral.UnLockRecords(clsGeneral.TrnType.Labs, LabOrderParameter.OrderID, 0, DateTime.Now);
                clsEmdeonGeneral.gloLab_IsOrderLocked = false;
                objClsGeneral.Dispose();
                objClsGeneral = null;
            }
            if (frmOrderTests != null)
            {
                frmOrderTests.Dispose();
                frmOrderTests = null;
            }



        }
        void Set_ResendThroughEmdeon(object sender, EventArgs e)
        {

            Int64 _curOrderID = 0;
            if (c1TestLibrary.RowSel > 0)
            {
                _curOrderID = Convert.ToInt64(c1TestLibrary.GetData(c1TestLibrary.RowSel, COL_ORDERID));
                //_OrderStatus = Convert.ToString(c1TestLibrary.GetData(c1TestLibrary.RowSel, COL_ORDERSTATUS));
            }
            // gloLabUC_Transaction1.s 
            gloLabUC_Transaction1.GetData();

            string TestNames = "";  //added to show testnames on Emdeon Screen v8022
            if (gloLabUC_Transaction1.arrTestNames != null)
            {

                foreach (string TestName in gloLabUC_Transaction1.arrTestNames)
                {
                    if (TestName.Trim() != "")
                    {
                        if (TestNames == "")
                        {
                            TestNames = TestName;
                        }
                        else
                        {
                            TestNames = TestNames + "~" + TestName;
                        }
                    }
                }
            }

            LaunchEmdeonScreen(_curOrderID, "", TestNames);
        }

        void Set_Menu_PrintRequisition(object sender, EventArgs e)
        {
            if (_IsDemoLab == true)
            {
                return;
            }

            if (System.Windows.Forms.Application.OpenForms.Count > 0)
            {
                bool _IsPrintFormLoaded = false;
                foreach (System.Windows.Forms.Form oform in System.Windows.Forms.Application.OpenForms)
                {
                    if (oform.Name == "frmEmdeonPrintRequisition")
                    {
                        _IsPrintFormLoaded = true;
                    }
                }

                if (_IsPrintFormLoaded)
                {
                    Form objFrm = Application.OpenForms["frmEmdeonPrintRequisition"];
                    objFrm.Close();
                    objFrm.Dispose();
                    _IsPrintFormLoaded = false;
                }

            }

            long _curOrderID = 0;
            string _OrderReferanceId = string.Empty;
            string _OrderStatus = string.Empty;
            clsGeneral objClsGeneral = new clsGeneral();
            clsGeneral.InternetConnectivity _InternetConnectionStatus;
            try
            {
                if (c1TestLibrary.RowSel > 0)
                {
                    _curOrderID = Convert.ToInt64(c1TestLibrary.GetData(c1TestLibrary.RowSel, COL_ORDERID));


                    if (_curOrderID > 0)
                    {
                        if (CheckLockStatus(_OrderParamter.OrderID))
                        {
                            return;
                        }

                        GetEmdeonOrderStatus(_curOrderID, out _OrderReferanceId, out _OrderStatus);
                        if (ConfirmNull(_OrderReferanceId.ToString()))
                        {
                            if (!clsEmdeonGeneral.CheckConnectionParameters(_dataBaseConnectionString))
                            {
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Open, "Unable to open external labs due to lab settings have not been configured in gloEMR Admin ", _patientID, 0, _PatientProviderID, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR);
                                MessageBox.Show("Lab Settings have not been configured in gloEMR Admin.\r\nPlease complete Lab Settings before ordering.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            _InternetConnectionStatus = objClsGeneral.IsInternetConnectionAvailable();
                            if (_InternetConnectionStatus == clsGeneral.InternetConnectivity.Success)
                            {
                                Forms.frmEmdeonPrintRequisition frmPrintRequest = new frmEmdeonPrintRequisition(_OrderReferanceId);
                                frmPrintRequest.WindowState = FormWindowState.Maximized;
                                frmPrintRequest.ShowInTaskbar = false;
                                frmPrintRequest.BringToFront();
                                frmPrintRequest.ShowDialog(this);

                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.ModifyLabs, gloAuditTrail.ActivityType.Print, "Printed external labs order requisition", _patientID, _curOrderID, _LabProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                                frmPrintRequest.Dispose();
                                frmPrintRequest = null;
                            }
                            else
                            {
                                if (_InternetConnectionStatus == clsGeneral.InternetConnectivity.NoInternet)
                                {
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Open, "Unable to open external lab due to internet connection not available ", _patientID, 0, _PatientProviderID, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR);
                                    ShowConnectionFailedMessage(true);
                                }
                                else if (_InternetConnectionStatus == clsGeneral.InternetConnectivity.ServerNotresponding)
                                {
                                    ShowConnectionFailedMessage(false);
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
                _OrderReferanceId = string.Empty;
                _OrderStatus = string.Empty;
                objClsGeneral.Dispose();
                objClsGeneral = null;
            }
        }

        void Set_OrderStatus(object sender, EventArgs e)
        {

            MenuItem oClickedMenuItem;

            oClickedMenuItem = (MenuItem)sender;


            string[] strOrderInformation = null;

            strOrderInformation = System.Text.RegularExpressions.Regex.Split(oClickedMenuItem.Tag.ToString(), ":");
            UpdateManualOrderStatus(strOrderInformation[0], strOrderInformation[1]);

            c1TestLibrary.SetData(c1TestLibrary.RowSel, COL_CUSTOMORDERSTATUS, oClickedMenuItem.Text);
        }

        void Set_OrderProvider(object sender, EventArgs e)
        {

            MenuItem oClickedMenuItem;

            oClickedMenuItem = (MenuItem)sender;


            string[] strProviderInformation = null;

            strProviderInformation = System.Text.RegularExpressions.Regex.Split(oClickedMenuItem.Tag.ToString(), ":");
            UpdateOrderProvider(strProviderInformation[0], strProviderInformation[1]);

            c1TestLibrary.SetData(c1TestLibrary.RowSel, COL_PROVIDERNAME, oClickedMenuItem.Text);
            c1TestLibrary.SetData(c1TestLibrary.RowSel, COL_ORDERING_PROVIDERID, Convert.ToInt64(strProviderInformation[0]));

            //09-May-14 Aniket: Fixing issue where Provider was getting ovrewritten on Save and Close.
            _OrderParamter.ProviderID = Convert.ToInt64(strProviderInformation[0]);
            _OrderParamter.LabProviderName = oClickedMenuItem.Text;
        }


        void Set_Menu_ModifyEmdeonOrder(object sender, EventArgs e)
        {
            if (_IsDemoLab == true)
            {
                return;
            }

            Int64 _curOrderID = 0;
            if (c1TestLibrary.RowSel > 0)
            {
                _curOrderID = Convert.ToInt64(c1TestLibrary.GetData(c1TestLibrary.RowSel, COL_ORDERID));

                if (_curOrderID > 0)
                {
                    if (LaunchOrderEditScreen(_curOrderID))
                    {
                        //fillOpenOrdsGrid_New();
                        gloUCLab_History_gUC_FillOrder(2);
                        cmbOrderStatus.SelectedItem = "No";
                        dtPickerToDate.Value = System.DateTime.Now;
                        dtPickerFromDate.Value = System.DateTime.Now.AddMonths(-12);
                        RefershAllGrids();
                        gloUC_TransactionHistory1.LoadPreviousLabs(_patientID, DateTime.Now);
                        gloUC_TransactionHistory1.DesignTestGrid();
                        gloUC_TransactionHistory1.HideCloseButton = false;
                        gloUC_TransactionHistory1.SetDataByDate(DateTime.Now.Date, DateTime.Now.Date);
                        gloUC_TransactionHistory1.cmbCriteria.Text = "Date";
                    }
                }

            }
        }
        private bool IsResultsExsists(Int64 _OrderId)
        {

            gloDatabaseLayer.DBLayer oDBLayer = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            string _strQry = string.Empty;
            bool _blnresult = false;
            object objResult = null;
            try
            {

                oDBLayer.Connect(false);
                _strQry = " SELECT TOP 1 labotr_TestID FROM  Lab_Order_Test_Result WHERE labotr_OrderID =  '" + _OrderId + "' ";

                objResult = oDBLayer.ExecuteScalar_Query(_strQry);
                if (objResult != null && objResult.ToString() != "")
                {
                    _blnresult = true;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);

            }
            finally
            {
                oDBLayer.Disconnect();
                if (oDBLayer != null)
                {
                    oDBLayer.Dispose();
                }
                if (objResult != null)
                {
                    objResult = null;
                }

            }
            return _blnresult;
        }
        //Added by madan on 20100601
        private void c1TestLibrary_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (gloLabUC_Transaction1.LabModified || gloUCLab_OrderDetail.OrderModified)
            {
                if (CheckStatusOfOrder() == false)
                {
                    return;
                }
            }

            Int64 _curOrderID = 0;
            if (c1TestLibrary.RowSel > 0)
            {
                _curOrderID = Convert.ToInt64(c1TestLibrary.GetData(c1TestLibrary.RowSel, COL_ORDERID));

            }
            if (_curOrderID > 0)
            {
                if (CheckLockStatus(_curOrderID))
                {
                    return;
                }

                if (GetOrderType(_curOrderID) == "Emr")
                {
                    Set_Menu_ModifyOrder(sender, e);
                    //Commneted by Mayuri:20140521-to refresh grid from modify context menu also,so added in Set_Menu_ModifyOrder()
                    //12-Jun-13 Aniket: Resolving Bug #52196: 
                    //RefershAllGrids();
                }
                else
                {
                    if (_IsDemoLab == true)
                    {
                        return;
                    }
                    if (LaunchOrderEditScreen(_curOrderID))
                    {
                        //use to refresh grid data while opening data for modification through Emdeon, 8020 order PRD
                        RefershAllGrids();
                        ShowOrders(_curOrderID);
                        try
                        {


                            //use to select Particular row open for modification 8020 order PRD
                            int _TestID = c1TestLibrary.FindRow(_curOrderID.ToString(), 1, COL_ORDERID, false, true, true);
                            c1TestLibrary.Select(_TestID, 0);
                        }
                        catch (Exception ex)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                        }
                    }
                }
            }

        }

        //Added by madan on 20100601
        private void LaunchEmdeonScreen(long nOrderID = 0, string sTestIDs = "", string _TestName = "")
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            Classes.clsGeneral objclsgeneral = new Classes.clsGeneral();

            try
            {


                if (!clsEmdeonGeneral.CheckConnectionParameters(_dataBaseConnectionString))
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Open, "Unable to open external labs due to Lab Settings have not been configured in gloEMR Admin ", _patientID, 0, _PatientProviderID, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR);
                    MessageBox.Show("Lab Settings have not been configured in gloEMR Admin.\r\nPlease complete Lab Settings before ordering.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                clsGeneral.InternetConnectivity _InternetConnectionStatus;
                _InternetConnectionStatus = objclsgeneral.IsInternetConnectionAvailable();
                if (_InternetConnectionStatus == clsGeneral.InternetConnectivity.Success)
                {
                    Int16 loopcnt = 0;

                    if (!compareProvider(_PatientProviderID))
                    {
                        return;
                    }


                    //clsgloLabPatientLayer objGloPatientLayer = new clsgloLabPatientLayer();
                    bool _billingStatus = objClsgloLabPatientLayer.CheckBillingType(objpatient);

                    if (_billingStatus == true)
                    {
                        string strQry = string.Empty;
                        Boolean boolPatientReg = false; // flag for patient registration                          
                        if (ConfirmNull(objpatient.DemographicsDetail.PatientCode.ToString()))
                        {
                            strQry = "SELECT COUNT(*) FROM PatientExternalCodes INNER JOIN Patient ON PatientExternalCodes.nPatientId = Patient.nPatientID  where PatientExternalCodes.sExternalType = 'EMDEON' AND    Patient.sPatientCode='" + objpatient.DemographicsDetail.PatientCode.ToString().Trim() + "'";
                        }
                        oDB.Connect(false);

                        for (loopcnt = 1; loopcnt <= 3; loopcnt++)
                        {
                            Int32 cnt = 0;
                            cnt = Convert.ToInt32(oDB.ExecuteScalar_Query(strQry));
                            if (cnt < 1) // if cnt is gretaer than zero means patient registered
                            {
                                pnlregistration.Visible = true;
                                pnlregistration.BringToFront();
                                Application.DoEvents();

                                gloEmdeonInterface.Classes.clsEmdeonGeneral.gloLab_patID = _patientID;

                                boolPatientReg = objClsgloLabPatientLayer.RegisterGloPatient(objpatient, _dataBaseConnectionString);

                                pnlregistration.Visible = false;
                                if (boolPatientReg)
                                {
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.RegisterLabPatient, "patient is successfully registered in external lab service ", _patientID, 0, _PatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                                    break;
                                }
                            }
                            else
                            {
                                boolPatientReg = true;
                                break;
                            }
                        }

                        if (boolPatientReg == true)  // if patient is registered
                        {
                            frmEmdeonInterface objfrmEmdonInterface = new frmEmdeonInterface(_patientID, nOrderID, sTestIDs);
                            objfrmEmdonInterface.LoginProviderID = LoginProviderID;
                            objfrmEmdonInterface.WindowState = FormWindowState.Maximized;
                            //Added for Bug #87928: CR00000369 : Lab orders coming back into glo are not matching up to the pending/sent order 
                            objfrmEmdonInterface._isSplitOrder = isSplitOrder;
                            
                            objfrmEmdonInterface.BringToFront(); // added by Abhijeet on 20100424
                            objfrmEmdonInterface.TestsName = _TestName; //added to show testnames on Emdeon Screen ,v8022
                            objfrmEmdonInterface.ShowDialog(this); // added 'this' parameter by by Abhijeet  on 20100424
                            objfrmEmdonInterface.Dispose();
                            //Bug #48804: 00000433 : Lab Order
                            objfrmEmdonInterface = null;
                            gloUCLab_History_gUC_FillOrder(2);
                            cmbOrderStatus.SelectedItem = "No";
                            dtPickerToDate.Value = System.DateTime.Now;
                            dtPickerFromDate.Value = System.DateTime.Now.AddMonths(-12);
                            RefershAllGrids();
                            gloUC_TransactionHistory1.LoadPreviousLabs(_patientID, DateTime.Now);
                            gloUC_TransactionHistory1.DesignTestGrid();
                            gloUC_TransactionHistory1.HideCloseButton = false;
                            gloUC_TransactionHistory1.SetDataByDate(DateTime.Now.Date, DateTime.Now.Date);
                            gloUC_TransactionHistory1.cmbCriteria.Text = "Date";

                        }

                        else
                        {
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.RegisterLabPatient, "patient is not registered in externnnal lab service ", _patientID, 0, _PatientProviderID, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR);

                            if (ConfirmNull(clsEmdeonGeneral.gloLab_Identifier.ToString()))
                            {
                                MessageBox.Show(clsEmdeonGeneral.gloLab_Identifier.ToString().Trim(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("There was a problem registering the patient with Lab service. Please try again later.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
                else
                {
                    if (_InternetConnectionStatus == clsGeneral.InternetConnectivity.NoInternet)
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Open, "Unable to open external lab due to internet connection not available ", _patientID, 0, _PatientProviderID, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR);
                        ShowConnectionFailedMessage(true);
                    }
                    else if (_InternetConnectionStatus == clsGeneral.InternetConnectivity.ServerNotresponding)
                    {
                        ShowConnectionFailedMessage(false);
                    }
                }
                objclsgeneral.Dispose();
                objclsgeneral = null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null; //added for memory management
                }
                if (objclsgeneral != null)
                {
                    objclsgeneral.Dispose();
                    objclsgeneral = null;
                }
            }
        }

        //Added by madan on 20100610
        private void GetEmdeonOrderStatus(Int64 _OrderID, out string _OrderReferanceId, out string _OrderStatus)
        {
            _OrderStatus = string.Empty;
            _OrderReferanceId = string.Empty;

            string strQuery = string.Empty;
            DataTable dtResult = null;
            gloDatabaseLayer.DBLayer objDbLayer = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString.ToString());
            clsGeneral objClsGeneral = new clsGeneral();
            try
            {
                objDbLayer.Connect(false);
                strQuery = "select labom_dgloLabOrderID,labom_gloLabOrderStatus from lab_order_Mst where labom_OrderID=" + _OrderID;

                objDbLayer.Retrive_Query(strQuery, out dtResult);
                if (dtResult != null && dtResult.Rows.Count > 0)
                {
                    _OrderReferanceId = Convert.ToString(dtResult.Rows[0]["labom_dgloLabOrderID"].ToString());

                    //Bug #46231: gloHL7- EmdeonLab-Application is showing an exception when we modify the Emdeon order come through HL7
                    //Change : Before converting value to INT32 validation added for null and empty.
                    if (!string.IsNullOrEmpty(Convert.ToString(dtResult.Rows[0]["labom_gloLabOrderStatus"])))
                    {
                        _OrderStatus = objClsGeneral.GetOrderStatus(Convert.ToInt32(dtResult.Rows[0]["labom_gloLabOrderStatus"]), 0);
                    }
                }
                else
                {
                    _OrderReferanceId = string.Empty;
                    _OrderStatus = string.Empty;
                }
                objDbLayer.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);

            }
            finally
            {
                if (objDbLayer != null)
                {
                    objDbLayer.Dispose();
                }
                if (dtResult != null)
                {
                    dtResult.Dispose(); // added for memory management  
                    dtResult = null;
                }
                strQuery = string.Empty;
                if (objClsGeneral != null)
                {
                    objClsGeneral.Dispose();
                    objClsGeneral = null;
                }

            }

        }
        //Added by madan on 20100611
        private bool LaunchOrderEditScreen(Int64 _OrderId)
        {
            bool _result = false;
            string _OrderStatus = string.Empty;
            int _TempOrderStatus = 0;
            string _OrderReferanceId = string.Empty;
            clsGeneral objClsGeneral = new clsGeneral();
            clsEmdeonLabLayer objClsEmdeonLabLayer = new clsEmdeonLabLayer();
            try
            {


                ///if order status is send to lab.. then order cannot be edited...
                //if the order status is not send to lab... then latest order status is retrieved from emdeon and corresponding latest
                // order status is updated to the system.

                GetEmdeonOrderStatus(_OrderId, out _OrderReferanceId, out _OrderStatus);

                //Bug #46231: gloHL7- EmdeonLab-Application is showing an exception when we modify the Emdeon order come through HL7
                //Change : If order status is blank or null then display a message and skip further processing.
                if (string.IsNullOrEmpty(_OrderStatus))
                {
                    MessageBox.Show("This is an electronic lab order and therefore cannot be edited.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                // Bug :#102867 (Lab results of emdeon order disappeared after modified order having status "Final Reoprted")
                if (_OrderStatus.Equals("F") || (_OrderStatus != "Not Sent" && _OrderStatus != "Ready to Transmit"))
                {
                    DialogResult dr = MessageBox.Show("This order has been sent to the lab and therefore cannot be edited. Would you like to print a copy of the requisition?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (dr == DialogResult.Yes)
                    {
                        Set_Menu_PrintRequisition(null, null);
                        return false;
                    }
                    else
                    {
                        return false;
                    }

                }

                if (!clsEmdeonGeneral.CheckConnectionParameters(_dataBaseConnectionString))
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Open, "Unable to open external lab due to Lab Settings have not been configured in gloEMR Admin ", _patientID, 0, _PatientProviderID, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR);
                    MessageBox.Show("Lab Settings have not been configured in gloEMR Admin.\r\nPlease complete Lab Settings before ordering.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                clsGeneral.InternetConnectivity _InternetConnectionStatus;
                _InternetConnectionStatus = objClsGeneral.IsInternetConnectionAvailable();

                if (_InternetConnectionStatus == clsGeneral.InternetConnectivity.Success)
                {
                    if (!(compareProvider(_PatientProviderID)))
                    {
                        return false;
                    }
                    pnlregistration.Visible = true;
                    Application.DoEvents();
                    _OrderStatus = objClsEmdeonLabLayer.GetEmdeonOrderStatus(_OrderReferanceId);
                    if (ConfirmNull(_OrderStatus.ToString()))
                    {
                        _TempOrderStatus = objClsGeneral.GetOrderStatusEnum(_OrderStatus).GetHashCode();

                        if ((_TempOrderStatus > 0) && (_TempOrderStatus != 13))  //condition added cancelled by client changed from 0 to 13
                        {
                            UpdateOrderStatus(_TempOrderStatus, _OrderId);
                            // Bug :#102867 (Lab results of emdeon order disappeared after modified order having status "Final Reoprted")
                            if (_TempOrderStatus != 4 && _TempOrderStatus != 9)
                            {
                                pnlregistration.Visible = false;
                                DialogResult oResult = MessageBox.Show("This order has been sent to the lab and therefore cannot be edited. Would you like to print a copy of the requisition?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                                if (oResult == DialogResult.Yes)
                                {
                                    Set_Menu_PrintRequisition(null, null);
                                    return false;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            pnlregistration.Visible = false;
                            MessageBox.Show("Order status not found in the system.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return _result;
                        }
                    }
                    else
                    {
                        return _result;
                    }
                    pnlregistration.Visible = false;
                    Application.DoEvents();

                    if (LockOrder(LabOrderParameter.OrderID))
                    {
                        return false;
                    }
                    frmEmdeonInterface objfrmEmdonInterface = new frmEmdeonInterface(_patientID, _OrderReferanceId, _OrderId);
                    objfrmEmdonInterface.WindowState = FormWindowState.Maximized;
                    objfrmEmdonInterface.LoginProviderID = LoginProviderID;
                    objfrmEmdonInterface.BringToFront(); // added by Abhijeet on 20100424
                    objfrmEmdonInterface.ShowDialog(this); // added 'this' parameter by by Abhijeet  on 20100424
                    _result = objfrmEmdonInterface.IsOrderEdited;
                    objfrmEmdonInterface.Dispose();
                    objfrmEmdonInterface = null;

                }
                else
                {
                    if (_InternetConnectionStatus == clsGeneral.InternetConnectivity.NoInternet)
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Open, "Unable to open external lab due to internet connection not available ", _patientID, 0, _PatientProviderID, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR);
                        ShowConnectionFailedMessage(true);
                        return false;
                    }
                    else if (_InternetConnectionStatus == clsGeneral.InternetConnectivity.ServerNotresponding)
                    {
                        ShowConnectionFailedMessage(false);
                        return false;
                    }

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                _TempOrderStatus = 0;
                _OrderStatus = string.Empty;
                _OrderReferanceId = string.Empty;
                if (pnlregistration.Visible == true)
                {
                    pnlregistration.Visible = false;
                }
                if (objClsGeneral != null)
                {
                    objClsGeneral.Dispose();
                    objClsGeneral = null;
                }
                if (objClsEmdeonLabLayer != null)
                {

                    objClsEmdeonLabLayer = null;
                }


            }
            return _result;

        }
        //Added by madan on 2010612-- for updating order status..
        private bool UpdateOrderStatus(int OrderStatus, Int64 OrderId)
        {
            bool _blnResult = false;
            gloDatabaseLayer.DBLayer objDbLayer = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString.ToString());
            string strQuery = string.Empty;
            int _result = 0;
            try
            {
                objDbLayer.Connect(false);

                strQuery = "update lab_Order_Mst set labom_gloLabOrderStatus='" + OrderStatus + "' where labom_OrderID=" + OrderId;

                _result = objDbLayer.Execute_Query(strQuery);
                if (_result < 0)
                    _blnResult = false;
                else if (_result >= 0)
                    _blnResult = true;
                else
                    _blnResult = false;

                objDbLayer.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                _blnResult = false;
            }
            finally
            {
                if (objDbLayer != null)
                {
                    objDbLayer.Dispose();
                }
                strQuery = string.Empty;
                _result = 0;
            }

            return _blnResult;
        }

        //Added by madan on 20100515-- for previous history
        private string GetMaxResultsDateTime(Int64 _OrderId)
        {

            gloDatabaseLayer.DBLayer oDBLayer = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            string _strQryResultDateTime = string.Empty;
            string _strResultDateTime = string.Empty;
            object objResult = null;
            try
            {
                oDBLayer.Connect(false);
                _strQryResultDateTime = " select top 1 Lab_Order_Test_Result.labotr_TestResultDateTime from Lab_Order_Test_Result WHERE Lab_Order_Test_Result.labotr_OrderID =  '" + _OrderId + "' "
                                        + " order by Lab_Order_Test_Result.labotr_TestResultDateTime  desc ";

                objResult = oDBLayer.ExecuteScalar_Query(_strQryResultDateTime);
                if (objResult != null && objResult.ToString() != "")
                {
                    // _strResultDateTime = Convert.ToString(objResult);
                    _strResultDateTime = Convert.ToDateTime(objResult).ToString("yyyy-MM-dd HH:mm:ss.fff");
                }
                oDBLayer.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("" + ex.ToString(), false);
                _strResultDateTime = string.Empty;
            }
            finally
            {
                if (oDBLayer != null)
                {
                    oDBLayer.Dispose();
                    oDBLayer = null; //added for memory management
                }
                if (objResult != null)
                {
                    objResult = null;
                }
                _strQryResultDateTime = string.Empty;

            }
            return _strResultDateTime;
        }
        #endregion

        //Added by madan on 20100616
        private void chk_PreviousHistory_CheckedChanged(object sender, EventArgs e)
        {
            long curOrderID = 0;
            try
            {
                if (_FormIsLoded)
                {
                    if (gloLabUC_Transaction1.LabModified || gloUCLab_OrderDetail.OrderModified)
                    {

                        DialogResult oResult = MessageBox.Show("Do you want to save your changes?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        if (oResult == DialogResult.Yes)
                        {
                            if (chk_PreviousHistory.Checked)
                            {
                                this.chk_PreviousHistory.CheckedChanged -= new System.EventHandler(this.chk_PreviousHistory_CheckedChanged);
                                chk_PreviousHistory.Checked = false;
                                this.chk_PreviousHistory.CheckedChanged += new System.EventHandler(this.chk_PreviousHistory_CheckedChanged);
                                SaveOrder(0);
                                this.chk_PreviousHistory.CheckedChanged -= new System.EventHandler(this.chk_PreviousHistory_CheckedChanged);
                                chk_PreviousHistory.Checked = true;
                                this.chk_PreviousHistory.CheckedChanged += new System.EventHandler(this.chk_PreviousHistory_CheckedChanged);

                            }
                            else
                            {
                                this.chk_PreviousHistory.CheckedChanged -= new System.EventHandler(this.chk_PreviousHistory_CheckedChanged);
                                chk_PreviousHistory.Checked = true;
                                this.chk_PreviousHistory.CheckedChanged += new System.EventHandler(this.chk_PreviousHistory_CheckedChanged);
                                SaveOrder(0);
                                this.chk_PreviousHistory.CheckedChanged -= new System.EventHandler(this.chk_PreviousHistory_CheckedChanged);
                                chk_PreviousHistory.Checked = false;
                                this.chk_PreviousHistory.CheckedChanged += new System.EventHandler(this.chk_PreviousHistory_CheckedChanged);
                            }

                            gloLabUC_Transaction1.LabModified = false;
                            gloUCLab_OrderDetail.OrderModified = false;
                        }
                        else if (oResult == DialogResult.No)
                        {
                            if (_CurrentLockedOrder > 0)
                            {
                                if (UnLockOrder(_CurrentLockedOrder))
                                {
                                    _CurrentLockedOrder = 0;
                                }
                            }
                            gloUCLab_OrderDetail.OrderModified = false;
                            gloLabUC_Transaction1.LabModified = false;
                        }
                        else if (oResult == DialogResult.Cancel)
                        {
                            gloLabUC_Transaction1.LabModified = true;
                            gloUCLab_OrderDetail.OrderModified = true;

                            if (chk_PreviousHistory.Checked)
                            {
                                this.chk_PreviousHistory.CheckedChanged -= new System.EventHandler(this.chk_PreviousHistory_CheckedChanged);
                                chk_PreviousHistory.Checked = false;
                                this.chk_PreviousHistory.CheckedChanged += new System.EventHandler(this.chk_PreviousHistory_CheckedChanged);

                            }
                            else
                            {
                                this.chk_PreviousHistory.CheckedChanged -= new System.EventHandler(this.chk_PreviousHistory_CheckedChanged);
                                chk_PreviousHistory.Checked = true;
                                this.chk_PreviousHistory.CheckedChanged += new System.EventHandler(this.chk_PreviousHistory_CheckedChanged);
                            }

                            return;
                        }
                    }

                    if (c1TestLibrary.Rows.Count > 0 && c1TestLibrary.RowSel > 0)
                    {
                        curOrderID = Convert.ToInt64(c1TestLibrary.GetData(c1TestLibrary.RowSel, COL_ORDERID));
                        ShowOrders(curOrderID);
                    }
                    if (chk_PreviousHistory.Checked == false)
                    {
                        gloLabUC_Transaction1.IsLoadLastTransaction = true;
                    }
                    else
                    {
                        gloLabUC_Transaction1.IsLoadLastTransaction = false;
                    }

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        /// <summary>
        /// This method used to check weather the order is edited ,
        /// 
        /// </summary>
        /// <returns></returns>
        private bool CheckStatusOfOrder()
        {

            bool _result = false;


            DialogResult drResult = MessageBox.Show("Do you want to save your changes?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
            if (drResult == DialogResult.Yes)
            {
                _blnClosed = true;
                IsClosed = true;
                SaveOrder(0);

                gloUCLab_OrderDetail.OrderModified = false;
                gloLabUC_Transaction1.LabModified = false;
                _result = true;
            }
            else if (drResult == DialogResult.No)
            {
                _result = true;
                if (_CurrentLockedOrder > 0)
                {
                    if (UnLockOrder(_CurrentLockedOrder))
                    {
                        _CurrentLockedOrder = 0;
                    }
                }

                gloUCLab_OrderDetail.OrderModified = false;
                gloLabUC_Transaction1.LabModified = false;
            }
            else if (drResult == DialogResult.Cancel)
            {
                gloUCLab_OrderDetail.OrderModified = true;
                gloLabUC_Transaction1.LabModified = true;
                _result = false;
            }

            return _result;

        }
        private bool CheckLockStatus(Int64 OrderID)
        {

            string _LockedUserName = string.Empty;
            string _LockedSystemName = string.Empty;

            bool _blnResult = false;
            string _MachineName = string.Empty;

            string strQuery = string.Empty;
            DataTable dtResult = null;
            gloDatabaseLayer.DBLayer objDbLayer = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString.ToString());
            clsGeneral objClsGeneral = new clsGeneral();
            try
            {
                _MachineName = Environment.MachineName;
                objDbLayer.Connect(false);
                strQuery = " SELECT  ISNULL(labom_UserName,'') AS UserName , ISNULL(labom_MachineName,'') AS MachineName from Lab_Order_MST " +
                             " WHERE  labom_OrderID =" + OrderID;

                objDbLayer.Retrive_Query(strQuery, out dtResult);
                if (dtResult != null && dtResult.Rows.Count > 0)
                {
                    _LockedUserName = Convert.ToString(dtResult.Rows[0]["UserName"].ToString());
                    _LockedSystemName = Convert.ToString(dtResult.Rows[0]["MachineName"]).ToString();
                }

                if (ConfirmNull(_LockedUserName) && ConfirmNull(_LockedSystemName))
                {
                    if (_LockedSystemName != _MachineName)
                    {
                        MessageBox.Show("This Patient Order is being modified by '" + _LockedUserName + "' on '" + _LockedSystemName + "'. You cannot modify it.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        _blnResult = true;
                    }
                }

                objDbLayer.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);

            }
            finally
            {
                if (objDbLayer != null)
                {
                    objDbLayer.Dispose();
                    objDbLayer = null; //added for memory management
                }
                if (dtResult != null)
                {
                    dtResult.Dispose();//added for memory management  
                    dtResult = null;
                }
                if (objClsGeneral != null)
                {
                    objClsGeneral.Dispose();
                    objClsGeneral = null;
                }
                strQuery = string.Empty;
                _MachineName = string.Empty;
            }
            return _blnResult;

        }
        private bool LockOrder(Int64 OrderID)
        {
            bool _blnResult = false;
            clsGeneral objClsgeneral = new clsGeneral();
            clsLockOrders objClsLockOrders = null;
            string _MachineName = string.Empty;
            string _strLoginUsername = string.Empty;
            try
            {
                _strLoginUsername = GetLoginUserName(_LoginUserID);
                _MachineName = Environment.MachineName;

                if (OrderID > 0)
                {
                    objClsLockOrders = objClsgeneral.Scan_n_Lock_Transaction(clsGeneral.TrnType.Labs, OrderID, 0, DateTime.Now, _MachineName, _strLoginUsername);
                    if (objClsLockOrders.MachineName != _MachineName)
                    {
                        MessageBox.Show("This Patient Order is being modified by '" + objClsLockOrders.UserName + "' on '" + objClsLockOrders.MachineName + "'. You cannot modify it.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        _blnResult = true;

                    }
                    else
                    {
                        clsEmdeonGeneral.gloLab_IsOrderLocked = true;
                        _blnResult = false;
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (objClsgeneral != null)
                {
                    objClsgeneral.Dispose();
                    objClsgeneral = null;
                }

                if (objClsLockOrders != null) //added for memory management
                {

                    objClsLockOrders = null;
                }
                _MachineName = string.Empty;
                _strLoginUsername = string.Empty;
            }
            return _blnResult;

        }

        private void gloLabUC_Transaction1_LockOrder(long OrderID)
        {

            clsGeneral objClsgeneral = new clsGeneral();
            clsLockOrders objClsLockOrders = null;
            string _MachineName = string.Empty;
            string _strLoginUsername = string.Empty;
            try
            {
                _strLoginUsername = GetLoginUserName(_LoginUserID);
                _MachineName = Environment.MachineName;

                if (OrderID > 0)
                {
                    objClsLockOrders = objClsgeneral.Scan_n_Lock_Transaction(clsGeneral.TrnType.Labs, OrderID, 0, DateTime.Now, _MachineName, _strLoginUsername);
                    if (objClsLockOrders.MachineName != _MachineName)
                    {
                        MessageBox.Show("This Patient Order is being modified by '" + objClsLockOrders.UserName + "' on '" + objClsLockOrders.MachineName + "'. You cannot modify it.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        gloLabUC_Transaction1.LabModified = false;
                        gloLabUC_Transaction1.IsOrderLocked = true;
                        ShowOrders(OrderID);
                    }
                    else
                    {
                        gloLabUC_Transaction1.IsOrderLocked = false;
                        _CurrentLockedOrder = OrderID;
                        clsEmdeonGeneral.gloLab_IsOrderLocked = true;
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (objClsgeneral != null)
                {
                    objClsgeneral.Dispose();
                    objClsgeneral = null;
                }
                if (objClsLockOrders != null) //added for memory management
                {

                    objClsLockOrders = null;
                }
                _MachineName = string.Empty;
                _strLoginUsername = string.Empty;
            }
        }

        //Added by madan on 20100617
        private bool UnLockOrder(Int64 _OrderId)
        {
            bool _blnResult = false;
            clsGeneral objClsGeneral = new clsGeneral();
            try
            {
                if (_OrderId > 0)
                {
                    objClsGeneral.UnLockRecords(clsGeneral.TrnType.Labs, _OrderId, 0, DateTime.Now);
                    clsEmdeonGeneral.gloLab_IsOrderLocked = false;
                    _blnResult = true;
                }
                else
                {
                    _blnResult = false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                _blnResult = false;
            }
            finally
            {
                if (objClsGeneral != null)
                {
                    objClsGeneral.Dispose();
                    objClsGeneral = null;
                }
            }
            return _blnResult;
        }

        //added by madan on 20100621---- for demo labs
        private void LaunchDemoLab(Int64 _PatientID)
        {
            try
            {
                //Commeneted by Mayuri:20140509-provider validation moved to emdeon form load.
                if (!compareProvider(_PatientProviderID))
                {
                    return;
                }

                if (!clsEmdeonGeneral.CheckConnectionParameters(_dataBaseConnectionString))
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Open, "Unable to open external labs due to Lab Settings have not been configured in gloEMR Admin ", _patientID, 0, _PatientProviderID, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR);
                    MessageBox.Show("Lab Settings have not been configured in gloEMR Admin.\r\nPlease complete Lab Settings before ordering.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                //   clsgloLabPatientLayer objGloPatientLayer = new clsgloLabPatientLayer();
                bool _billingStatus = objClsgloLabPatientLayer.CheckBillingType(objpatient);

                if (_billingStatus == true)
                {

                    frmLabDemonstration frmLabDemo = new frmLabDemonstration(_patientID);
                    frmLabDemo.WindowState = FormWindowState.Maximized;
                    frmLabDemo.BringToFront();
                    frmLabDemo.ShowDialog(this);
                    frmLabDemo.Dispose();
                    frmLabDemo = null; //added for memory management
                }


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {

            }
        }

        private void c1TestLibrary_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void ShowConnectionFailedMessage(bool _IsInternetFailed)
        {
            try
            {
                frmConfirmInternetConnection frmConfirm = new frmConfirmInternetConnection(_IsInternetFailed);
                frmConfirm.ShowInTaskbar = false;
                frmConfirm.BringToFront();
                frmConfirm.ShowDialog(this);
                frmConfirm.Dispose();
                frmConfirm = null; //added for memory management
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void pnlregistration_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmViewgloLab_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Change made to solve memory Leak and word crash issue
            if (oLabActor_Order1 != null)
            {
                oLabActor_Order1.Dispose();
                oLabActor_Order1 = null;
            }
            if (objpatient != null)
            {
                objpatient.Dispose();
                objpatient = null;
            }
            if (oUc_ResultSet != null)
            {
                oUc_ResultSet.OpenDocument -= new gloUserControlLibrary.LabUC_ResultSet.OpenDMS(gloLabUC_Transaction1_gUC_ViewDocument);
                oUc_ResultSet.ShowAck -= new gloUserControlLibrary.LabUC_ResultSet.ShowAckButton(ShowAckwButton);
                oUc_ResultSet.customMenuOpening -= new gloUserControlLibrary.LabUC_ResultSet.contextmnuOpen(oUc_ResultSet_customMenuOpening);
                oUc_ResultSet.Acknowlegeclick -= new gloUserControlLibrary.LabUC_ResultSet.ContextAcknowledge(oUc_ResultSet_Acknowlegeclick);
                oUc_ResultSet.AcknowlegeNormalclick -= new gloUserControlLibrary.LabUC_ResultSet.ContextAcknowledge(oUc_ResultSet_AcknowlegeNormalclick);
                oUc_ResultSet.MarkUnAcknowlegeclick -= new gloUserControlLibrary.LabUC_ResultSet.ContextAcknowledge(oUc_ResultSet_MarkUnAcknowlegeclick);
                oUc_ResultSet.OpenDicomFile -= new gloUserControlLibrary.LabUC_ResultSet.OpenDicom(gloLabUC_Transaction1_gUC_ViewDicomDocument);
                oUc_ResultSet.EvtCnxtMnuResult_SNOMED -= new gloUserControlLibrary.LabUC_ResultSet.CnxtMnuResultSet(oUc_ResultSet_CntxMenuSNOMED);
                oUc_ResultSet.EvtCnxtMnuResult_ICD -= new gloUserControlLibrary.LabUC_ResultSet.CnxtMnuResultSet(oUc_ResultSet_CntxMenuICD);
                oUc_ResultSet.EvtCnxtMnuResult_LOINC -= new gloUserControlLibrary.LabUC_ResultSet.CnxtMnuResultSet(oUc_ResultSet_CntxMenuLOINC);
                oUc_ResultSet.ViewCodification -= new gloUserControlLibrary.LabUC_ResultSet.Codification(oUc_ResultSet_ViewCodification);
                oUc_ResultSet.RemoveCodification-= new gloUserControlLibrary.LabUC_ResultSet.Codification(oUc_ResultSet_RemoveCodification);

                oUc_ResultSet = null;
            }
            if (objpatient != null)
            {
                objpatient.Dispose();
                objpatient = null;
            }
            if (_OrderParamter != null)
            {

                _OrderParamter = null;
            }
            if (gloUC_PatientStrip1 != null)
            {
                gloUC_PatientStrip1.Dispose();
                gloUC_PatientStrip1 = null;
            }
            if (oViewDocument != null)
            {
                oViewDocument.Dispose();
                oViewDocument = null;
            }
            if (objClsgloLabPatientLayer != null)
            {
                objClsgloLabPatientLayer.Dispose();
                objClsgloLabPatientLayer = null;
            }
            try
            {
                try
                {
                    if (OContextMenu != null)
                    {
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(OContextMenu);
                        if (OContextMenu.MenuItems != null)
                        {
                            OContextMenu.MenuItems.Clear();
                        }
                        OContextMenu.Dispose();
                        OContextMenu = null;
                    }
                }
                catch
                {
                }

                try
                {
                    if (c1TestLibrary.ContextMenu != null)
                    {
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(c1TestLibrary.ContextMenu);
                        if (c1TestLibrary.ContextMenu.MenuItems != null)
                        {
                            c1TestLibrary.ContextMenu.MenuItems.Clear();
                        }
                        c1TestLibrary.ContextMenu.Dispose();
                        c1TestLibrary.ContextMenu = null;
                    }
                }
                catch
                {
                }
            
            }
            catch
            {
            }
         //added for split control changes 
         
            if (uiPanSplitScreen_LabOrder != null)
            {
                uiPanSplitScreen_LabOrder.Dispose();
                uiPanSplitScreen_LabOrder = null; 
            }
            if (_clsSplit_Laborder != null)
            {
                _clsSplit_Laborder.Dispose();
                _clsSplit_Laborder = null; 
            }
            objWord = null;
            objCriteria = null; 
        }
       
        //ADed by madan on 2010107
        private void gloLabUC_Transaction1_gUC_ViewDicomDocument(long nPatientId, long DocumentID)
        {



            string sTestDicomPath = string.Empty;
            string sDicomPath = string.Empty;

            try
            {

                if (gloLabUC_Transaction1.LabModified || gloUCLab_OrderDetail.OrderModified)
                {
                    if (_CurrentLockedOrder > 0)
                    {
                        if (UnLockOrder(_CurrentLockedOrder))
                        {
                            _CurrentLockedOrder = 0;
                        }
                    }

                    DialogResult drResult = MessageBox.Show("Do you want to save your changes?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (drResult == DialogResult.Yes)
                    {
                        _blnClosed = true;
                        IsClosed = true;
                        SaveOrder(0);

                        gloUCLab_OrderDetail.OrderModified = false;
                        gloLabUC_Transaction1.LabModified = false;
                    }
                    else if (drResult == DialogResult.No)
                    {
                        gloUCLab_OrderDetail.OrderModified = false;
                        gloLabUC_Transaction1.LabModified = false;
                    }

                }


                if (nPatientId <= 0 && DocumentID <= 0)
                {
                    return;
                }
                sDicomPath = GetEMRDciomPath();

                if (ConfirmNull(sDicomPath))
                {

                    if (System.IO.Directory.Exists(sDicomPath) == false)
                    {
                        MessageBox.Show("Please configure valid dicom path to view the file.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    char ch1 = sDicomPath[sDicomPath.Length - 1];

                    if (ch1 != '\\')
                    {
                        sDicomPath = sDicomPath + "\\";
                    }

                }
                else
                {
                    MessageBox.Show("Please configure the DICOM path from Tool->Settings->Server Path, to save the file.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //Added nDBID condition by Abhijeet on 20101110
                clsGeneral objcls = new clsGeneral();
                Int64 nDBId = 0;
                nDBId = objcls.GetDataBaseConnectionIdFromHL7DB();
                objcls.Dispose();
                if (nDBId == 0)
                {
                    MessageBox.Show("gloEMR database connection ID not found in HL7 database.Please add gloEMR database in HL7 Database setting. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    sTestDicomPath = string.Empty;
                }
                else
                {
                    sTestDicomPath = GetTestDicomPath();
                }


                if (ConfirmNull(sTestDicomPath))
                {
                    sTestDicomPath = sTestDicomPath + "\\" + nPatientId;

                    char ch = sTestDicomPath[sTestDicomPath.Length - 1];

                    if (ch != '\\')
                    {
                        sTestDicomPath = sTestDicomPath + "\\";
                    }

                    if (System.IO.Directory.Exists(sTestDicomPath) == false)
                    {
                        MessageBox.Show("Please configure valid dicom path in gloHL7 service to view the file.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    gloEmdeonCommon.frmgloDICOM objFrmDicom = new frmgloDICOM(nPatientId, DocumentID, sTestDicomPath, sDicomPath, _LoginUserID);
                    objFrmDicom.ShowInTaskbar = false;
                    objFrmDicom.BringToFront();
                    objFrmDicom.ShowDialog(this);
                    objFrmDicom.Dispose();
                    objFrmDicom = null;
                    //Refresh grids.
                    #region "Refresh grids"

                    if (c1TestLibrary.Rows.Count > 0 && c1TestLibrary.RowSel > 0)
                    {
                        long curOrderID = Convert.ToInt64(c1TestLibrary.GetData(c1TestLibrary.RowSel, COL_ORDERID));
                        gloEMRGeneralLibrary.gloEMRLab.gloEMRLabOrder oLabOrderRequest = new gloEMRGeneralLibrary.gloEMRLab.gloEMRLabOrder();
                        if ((tabControl1.SelectedIndex == 1) || (tabControl1.SelectedIndex == 3))  //or condition added for bugid 70830
                        {
                            gloUCLab_OrderDetail.Visible = false;
                        }
                        else
                        {
                            gloUCLab_OrderDetail.Visible = true;
                        }
                        ShowOrders(curOrderID);
                        string _strOrderType = GetOrderType(curOrderID);
                        if (_strOrderType == "Emr")
                        {
                            tlbbtn_Finish.Enabled = true;
                            tlbbtn_Requisition.Enabled = false;
                            gloUCLab_OrderDetail.PreferredLabActivity(false);
                        }
                        else
                        {
                            gloUCLab_OrderDetail.PreferredLabActivity(true);
                            tlbbtn_Finish.Enabled = false;
                            tlbbtn_Requisition.Enabled = true;
                        }

                        //Developer:Sanjog Dhamke
                        //Date: 20 Dec 2011 (6060)
                        //Bug ID/PRD Name/Sales force Case: PRD Lab Usability - To show Add,Modify & Deletion functionality for ACkw.
                        //Reason: To show only Acknowledgement button remove the Review and Amendment button 

                        // //added by madan on 20100510
                        //if (cmbOrderStatus.SelectedItem.ToString().Trim().ToLower() == "yes")
                        //{
                        //    tlbbtn_ViewHistory.Visible = true;
                        //    //tlbbtn_Acknowledgment.Visible = false;
                        //    tlbbtn_ReviewAck.Visible = true;
                        //}
                        //else if (cmbOrderStatus.SelectedItem.ToString().Trim().ToLower() == "no")
                        //{
                        //    tlbbtn_ViewHistory.Visible = false;
                        //    tlbbtn_Acknowledgment.Visible = true;
                        //}
                        //else
                        //{
                        //    if (CheckAcknoledgement(curOrderID, _OrderParamter.OrderNumberPrefix, gloUCLab_OrderDetail.OrderNumberID))
                        //    {
                        //        //tlbbtn_Acknowledgment.Visible = false;
                        //        tlbbtn_ReviewAck.Visible = true;
                        //        tlbbtn_ViewHistory.Visible = true;
                        //    }
                        //    else
                        //    {
                        //        tlbbtn_Acknowledgment.Visible = true;
                        //        tlbbtn_ReviewAck.Visible = false;
                        //        tlbbtn_ViewHistory.Visible = false;
                        //    }
                        //}
                        tlbbtn_ViewHistory.Visible = true;
                        oLabOrderRequest.Dispose();
                        oLabOrderRequest = null;
                    }

                    #endregion
                }
                else
                {
                    MessageBox.Show("Please configure dicom path in gloHL7 service to view the file.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                sTestDicomPath = string.Empty;
                sDicomPath = string.Empty;
            }

        }

        private string GetTestDicomPath()
        {
            string sDicomPath = string.Empty;
            gloDatabaseLayer.DBLayer objDbLayer = new gloDatabaseLayer.DBLayer(gloEMRGeneralLibrary.gloGeneral.clsgeneral.GetHL7ConnectionString());

            string strQuery = string.Empty;
            object objResult = null;

            try
            {
                clsGeneral objclsgeneral = new clsGeneral();
                Int64 intDBId = 0;
                intDBId = objclsgeneral.GetDataBaseConnectionIdFromHL7DB();
                objclsgeneral.Dispose();
                objclsgeneral = null;
                strQuery = "Select ISNULL(sSettingsValue,'')as sValue from HL7_Settings where sSettingsName='Dicom Path' and nDBConnectionId=" + intDBId.ToString();

                objDbLayer.Connect(false);

                objResult = objDbLayer.ExecuteScalar_Query(strQuery);

                if (objResult != null && objResult.ToString() != "")
                {
                    sDicomPath = Convert.ToString(objResult);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                sDicomPath = string.Empty;
            }
            finally
            {
                if (objDbLayer != null)
                {
                    objDbLayer.Dispose();
                    objDbLayer = null;
                }
                strQuery = string.Empty;
                objResult = null;
            }
            return sDicomPath;
        }

        private string GetEMRDciomPath()
        {
            string sDicomPath = string.Empty;

            try
            {
                if (gloSettings.gloRegistrySetting.OpenSubKey(gloSettings.gloRegistrySetting.gstrSoftEMR) == false)
                {
                    return "";
                }

                gloSettings.gloRegistrySetting.OpenSubKey(gloSettings.gloRegistrySetting.gstrSoftEMR, true);

                if ((gloSettings.gloRegistrySetting.GetRegistryValue("DICOMPATH") == null) == true)
                {
                    sDicomPath = "";
                }
                else
                {
                    sDicomPath = Convert.ToString(gloSettings.gloRegistrySetting.GetRegistryValue("DICOMPATH").ToString());
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                sDicomPath = string.Empty;
            }
            finally
            {
                gloSettings.gloRegistrySetting.CloseRegistryKey();
            }
            return sDicomPath;
        }

        private void gloLabUC_LabFlowSheet1_gUC_LabFlowSheet_Print(long OrderId)
        {
            gloEMRLabOrder oLabOrderRequest = new gloEMRLabOrder();

            try
            {
                oLabActor_Order1.ArrTestName = oLabOrderRequest.GetOrderTests(OrderId);

                PrintLabOrderReport(OrderId, oLabActor_Order1.ArrTestName);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                oLabOrderRequest.Dispose();
                oLabOrderRequest = null;
            }
        }

        private void gloLabUC_LabFlowSheet1_gUC_LabFlowSheet_TestResultPrint(DataTable dtLabData, DataTable dtPatientTable, int intrptPages, int intLastColPrinted, TextObject MyText, int cntCol)
        {
            gloEMRReports.rptLabFlowSheetReport objRpt = null;
            int intReportpart = 1;

            if ((dtLabData != null))
            {
                dtLabData.Columns.Remove("TestId");
                dtLabData.Columns.Remove("OrderId");

                cntCol = dtLabData.Columns.Count;

                intrptPages = Convert.ToInt16(cntCol / 6);
                if (intrptPages == 0)
                {
                    intrptPages = 1;
                }
                while ((intrptPages != 0))
                {

                    gloEMRReports.DataSet1 ds = new gloEMRReports.DataSet1();
                    DataTable dummyTable = ds.Tables[1];
                    dummyTable.Columns[0].ColumnName = "Test Name";
                    int Cnt = 1;
                    for (int nCnt = intLastColPrinted; nCnt <= intLastColPrinted + 5; nCnt++)
                    {
                        if (nCnt == dtLabData.Columns.Count)
                        {
                            break; // TODO: might not be correct. Was : Exit For
                        }
                        dummyTable.Columns[Cnt].ColumnName = dtLabData.Columns[nCnt].ColumnName.Replace("/", "-");
                        Cnt += 1;
                    }



                    objRpt = new gloEMRReports.rptLabFlowSheetReport();
                    MyText = (CrystalDecisions.CrystalReports.Engine.TextObject)objRpt.ReportDefinition.ReportObjects["TxtPatientname"];
                    MyText.Text = Convert.ToString(dtPatientTable.Rows[0]["Patient Name"]);

                    MyText = (CrystalDecisions.CrystalReports.Engine.TextObject)objRpt.ReportDefinition.ReportObjects["TxtDOB"];
                    MyText.Text = Convert.ToString(Convert.ToDateTime(dtPatientTable.Rows[0]["DOB"]).ToShortDateString());

                    MyText = (CrystalDecisions.CrystalReports.Engine.TextObject)objRpt.ReportDefinition.ReportObjects["TxtGender"];
                    MyText.Text = Convert.ToString(dtPatientTable.Rows[0]["Gender"]);


                    MyText = (CrystalDecisions.CrystalReports.Engine.TextObject)objRpt.ReportDefinition.ReportObjects["TxtClinicName"];
                    MyText.Text = Convert.ToString(dtPatientTable.Rows[0]["Practice Name"]);

                    MyText = (CrystalDecisions.CrystalReports.Engine.TextObject)objRpt.ReportDefinition.ReportObjects["TxtAge"];
                    MyText.Text = Convert.ToString(dtPatientTable.Rows[0]["Age"]);

                    for (Int32 tempCnt = 1; tempCnt <= dummyTable.Columns.Count - 1; tempCnt++)
                    {
                        MyText = (CrystalDecisions.CrystalReports.Engine.TextObject)objRpt.ReportDefinition.ReportObjects["Text" + tempCnt];
                        MyText.Text = dummyTable.Columns[tempCnt - 1].ColumnName.Replace("/", "-");
                    }

                    if (Cnt < 7)
                    {
                        while (Cnt != 7)
                        {
                            MyText = (CrystalDecisions.CrystalReports.Engine.TextObject)objRpt.ReportDefinition.ReportObjects[System.Convert.ToString(("Text" + (Cnt + 1)))];
                            MyText.Text = "";
                            Cnt += 1;
                        }
                    }

                    foreach (DataRow dr in dtLabData.Rows)
                    {
                        DataRow newRow = dummyTable.NewRow();
                        foreach (DataColumn dataCol in dummyTable.Columns)
                        {
                            if (dtLabData.Columns.Contains(dataCol.ColumnName.Replace("-", "/")))
                            {
                                newRow[dataCol.ColumnName] = Convert.ToString(dr[dataCol.ColumnName.Replace("-", "/")]).Replace("|", " ");
                            }
                            if (dataCol.ColumnName == "DataColumn8")
                            {
                                newRow["DataColumn8"] = Convert.ToString(dr["IsResult"]);
                            }
                        }
                        dummyTable.Rows.Add(newRow);
                    }
                    if (!dummyTable.Columns.Contains("DataColumn1"))
                    {
                        dummyTable.Columns[0].ColumnName = "DataColumn1";
                    }
                    if (!dummyTable.Columns.Contains("DataColumn2"))
                    {
                        dummyTable.Columns[1].ColumnName = "DataColumn2";
                    }
                    if (!dummyTable.Columns.Contains("DataColumn3"))
                    {
                        dummyTable.Columns[2].ColumnName = "DataColumn3";
                    }
                    if (!dummyTable.Columns.Contains("DataColumn4"))
                    {
                        dummyTable.Columns[3].ColumnName = "DataColumn4";
                    }
                    if (!dummyTable.Columns.Contains("DataColumn5"))
                    {
                        dummyTable.Columns[4].ColumnName = "DataColumn5";
                    }
                    if (!dummyTable.Columns.Contains("DataColumn6"))
                    {
                        dummyTable.Columns[4].ColumnName = "DataColumn6";
                    }
                    if (!dummyTable.Columns.Contains("DataColumn7"))
                    {
                        dummyTable.Columns[4].ColumnName = "DataColumn7";
                    }


                    objRpt.SetDataSource(ds.Tables[1]);

                    intLastColPrinted += (dummyTable.Columns.Count - 2);

                    MyText = (CrystalDecisions.CrystalReports.Engine.TextObject)objRpt.ReportDefinition.ReportObjects["TxtreportPage"];
                    MyText.Text = Convert.ToString(intReportpart.ToString());

                    intrptPages -= 1;
                    intReportpart += 1;

                    objRpt.PrintToPrinter(1, false, 1, 0);
                    objRpt.Close();
                    objRpt.Dispose();
                    objRpt = null;
                    dummyTable.Dispose();
                    dummyTable = null;
                    ds.Dispose();
                    ds = null;
                }
            }

        }
        //TestResultPrint use in clinical charts
        public Boolean TestResultPrint(DataTable dtLabData, DataTable dtPatientTable, int intrptPages, int intLastColPrinted, TextObject MyText, int cntCol, String strFilename)
        {
            Boolean isReportExported = false;
            gloEMRReports.rptLabFlowSheetReport objRpt = null;
            int intReportpart = 1;

            if ((dtLabData != null))
            {
                dtLabData.Columns.Remove("TestId");
                dtLabData.Columns.Remove("OrderId");

                cntCol = dtLabData.Columns.Count;

                intrptPages = Convert.ToInt16(cntCol / 6);
                if (intrptPages == 0)
                {
                    intrptPages = 1;
                }
                while ((intrptPages != 0))
                {

                    gloEMRReports.DataSet1 ds = new gloEMRReports.DataSet1();
                    DataTable dummyTable = ds.Tables[1];
                    //Assigning the names of columns
                    dummyTable.Columns[0].ColumnName = "Test Name";
                    int Cnt = 1;
                    for (int nCnt = intLastColPrinted; nCnt <= intLastColPrinted + 5; nCnt++)
                    {
                        if (nCnt == dtLabData.Columns.Count)
                        {
                            break; // TODO: might not be correct. Was : Exit For
                        }
                        dummyTable.Columns[Cnt].ColumnName = dtLabData.Columns[nCnt].ColumnName.Replace("/", "-");
                        Cnt += 1;
                    }



                    objRpt = new gloEMRReports.rptLabFlowSheetReport();
                    //Assigning Patient Data to report
                    MyText = (CrystalDecisions.CrystalReports.Engine.TextObject)objRpt.ReportDefinition.ReportObjects["TxtPatientname"];
                    MyText.Text = Convert.ToString(dtPatientTable.Rows[0]["Patient Name"]);

                    MyText = (CrystalDecisions.CrystalReports.Engine.TextObject)objRpt.ReportDefinition.ReportObjects["TxtDOB"];
                    MyText.Text = Convert.ToString(Convert.ToDateTime(dtPatientTable.Rows[0]["DOB"]).ToShortDateString());

                    MyText = (CrystalDecisions.CrystalReports.Engine.TextObject)objRpt.ReportDefinition.ReportObjects["TxtGender"];
                    MyText.Text = Convert.ToString(dtPatientTable.Rows[0]["Gender"]);


                    MyText = (CrystalDecisions.CrystalReports.Engine.TextObject)objRpt.ReportDefinition.ReportObjects["TxtClinicName"];
                    MyText.Text = Convert.ToString(dtPatientTable.Rows[0]["Practice Name"]);

                    MyText = (CrystalDecisions.CrystalReports.Engine.TextObject)objRpt.ReportDefinition.ReportObjects["TxtAge"];
                    MyText.Text = Convert.ToString(dtPatientTable.Rows[0]["Age"]);

                    for (Int32 tempCnt = 1; tempCnt <= dummyTable.Columns.Count - 1; tempCnt++)
                    {
                        MyText = (CrystalDecisions.CrystalReports.Engine.TextObject)objRpt.ReportDefinition.ReportObjects["Text" + tempCnt];
                        MyText.Text = dummyTable.Columns[tempCnt - 1].ColumnName.Replace("/", "-");
                    }

                    if (Cnt < 7)
                    {
                        while (Cnt != 7)
                        {
                            MyText = (CrystalDecisions.CrystalReports.Engine.TextObject)objRpt.ReportDefinition.ReportObjects[System.Convert.ToString(("Text" + (Cnt + 1)))];
                            MyText.Text = "";
                            Cnt += 1;
                        }
                    }

                    //Reading Data to report
                    foreach (DataRow dr in dtLabData.Rows)
                    {
                        DataRow newRow = dummyTable.NewRow();
                        foreach (DataColumn dataCol in dummyTable.Columns)
                        {
                            if (dtLabData.Columns.Contains(dataCol.ColumnName.Replace("-", "/")))
                            {
                                newRow[dataCol.ColumnName] = Convert.ToString(dr[dataCol.ColumnName.Replace("-", "/")]).Replace("|", " ");
                            }
                            if (dataCol.ColumnName == "DataColumn8")
                            {
                                newRow["DataColumn8"] = Convert.ToString(dr["IsResult"]);
                            }
                        }
                        dummyTable.Rows.Add(newRow);
                    }
                    if (!dummyTable.Columns.Contains("DataColumn1"))
                    {
                        dummyTable.Columns[0].ColumnName = "DataColumn1";
                    }
                    if (!dummyTable.Columns.Contains("DataColumn2"))
                    {
                        dummyTable.Columns[1].ColumnName = "DataColumn2";
                    }
                    if (!dummyTable.Columns.Contains("DataColumn3"))
                    {
                        dummyTable.Columns[2].ColumnName = "DataColumn3";
                    }
                    if (!dummyTable.Columns.Contains("DataColumn4"))
                    {
                        dummyTable.Columns[3].ColumnName = "DataColumn4";
                    }
                    if (!dummyTable.Columns.Contains("DataColumn5"))
                    {
                        dummyTable.Columns[4].ColumnName = "DataColumn5";
                    }
                    if (!dummyTable.Columns.Contains("DataColumn6"))
                    {
                        dummyTable.Columns[4].ColumnName = "DataColumn6";
                    }
                    if (!dummyTable.Columns.Contains("DataColumn7"))
                    {
                        dummyTable.Columns[4].ColumnName = "DataColumn7";
                    }


                    objRpt.SetDataSource(ds.Tables[1]);

                    intLastColPrinted += (dummyTable.Columns.Count - 2);

                    MyText = (CrystalDecisions.CrystalReports.Engine.TextObject)objRpt.ReportDefinition.ReportObjects["TxtreportPage"];
                    MyText.Text = Convert.ToString(intReportpart.ToString());

                    intrptPages -= 1;
                    intReportpart += 1;

                    objRpt.ExportToDisk(ExportFormatType.PortableDocFormat, strFilename);
                    isReportExported = true;
                    objRpt.Close();
                    objRpt.Dispose();

                    dummyTable.Dispose();
                    dummyTable = null;
                    ds.Dispose();
                    ds = null;
                }
            }
            return isReportExported;
        }

        private bool GetLabFlowSheetDateSettings()
        {
            gloDatabaseLayer.DBLayer objDbLayer = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString.ToString().Trim());
            //  DataTable dtSettings = new DataTable();
            string strQry = string.Empty;

            bool blnResult = false;
            object objFromDate = null;
            object objToDate = null;

            try
            {

                strQry = "select sSettingsValue from settings where sSettingsName='LABFLOWSHEET-FROMDATE'";

                objDbLayer.Connect(false);

                objFromDate = objDbLayer.ExecuteScalar_Query(strQry);

                strQry = string.Empty;

                strQry = "select sSettingsValue from settings where sSettingsName='LABFLOWSHEET-TODATE'";

                objToDate = objDbLayer.ExecuteScalar_Query(strQry);


                if (objFromDate != null && objFromDate.ToString() != "" && objToDate != null && objToDate.ToString() != "")
                {
                    LabFlowSheetFromDt = Convert.ToDateTime(objFromDate);
                    LabFlowSheetToDt = Convert.ToDateTime(objToDate);

                    blnResult = true;
                }
                else
                    blnResult = false;

                objDbLayer.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (objDbLayer != null)
                {
                    objDbLayer.Dispose();
                    objDbLayer = null; //added for memory management 
                }
                strQry = string.Empty;
            }
            return blnResult;
        }

        private void SaveLabFlowSheetDates()
        {

            gloDatabaseLayer.DBLayer oDBLayer = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = null;
            try
            {

                oDBLayer.Connect(false);

                oDBParameters = new gloDatabaseLayer.DBParameters();

                oDBParameters.Add("@sSettingsName", "LABFLOWSHEET-FROMDATE", ParameterDirection.Input, SqlDbType.VarChar, 100);
                oDBParameters.Add("@sSettingsValue", gloLabUC_LabFlowSheet1.dtpFromDate.Text, ParameterDirection.Input, SqlDbType.VarChar, 255);
                oDBParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nUserID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nUserClinicFlag", 2, ParameterDirection.Input, SqlDbType.BigInt);

                oDBLayer.ExecuteScalar("gsp_InUpSettings", oDBParameters);

                if (oDBParameters != null)
                {
                    oDBParameters.Dispose();
                    oDBParameters = null; //added for memory management
                }
                oDBParameters = new gloDatabaseLayer.DBParameters();

                oDBParameters.Add("@sSettingsName", "LABFLOWSHEET-TODATE", ParameterDirection.Input, SqlDbType.VarChar, 100);
                oDBParameters.Add("@sSettingsValue", gloLabUC_LabFlowSheet1.dtpToDate.Text, ParameterDirection.Input, SqlDbType.VarChar, 255);
                oDBParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nUserID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nUserClinicFlag", 2, ParameterDirection.Input, SqlDbType.BigInt);

                oDBLayer.ExecuteScalar("gsp_InUpSettings", oDBParameters);

                oDBLayer.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDBLayer != null)
                {
                    oDBLayer.Dispose();
                    oDBLayer = null;  //added for memory management 
                }

                if (oDBParameters != null)
                {
                    oDBParameters.Dispose();
                    oDBParameters = null; //added for memory management
                }
            }

        }

        //End madan.


        private void gloLabUC_LabFlowSheet1_gUC_LabFlowSheet_FlowSheetPrint(Int64 PatientId, string FromDate, string ToDate, string OrdeID, string TestId, string resultname, string ResultTestNumber, int blnrange)
        {
            try
            {
                gloSSRSApplication.clsPrintReport clsPrintRpt=null;
                string strSQLServerName = appSettings["SQLServerName"];
                string strDatabaseName = appSettings["DatabaseName"];
                bool blnSQLAuthentication = Convert.ToBoolean(appSettings["WindowAuthentication"]);
                string strSQLUserEMR = appSettings["SQLLoginName"];
                string strSQLPasswordEMR = appSettings["SQLPassword"];
                bool gblnIsDefaultPrinter = !(Convert.ToBoolean(appSettings["DefaultPrinter"]));
                if (blnSQLAuthentication == false)
                {
                    blnSQLAuthentication = true;
                }
                else
                {
                    blnSQLAuthentication = false;
                }

                clsPrintRpt = new gloSSRSApplication.clsPrintReport(strSQLServerName, strDatabaseName, blnSQLAuthentication, strSQLUserEMR, strSQLPasswordEMR);
                clsPrintRpt.PrintReport("rptLabFlowSheet", "PatientID,FromDate,ToDate,OrderIDs,TestIDs,ResultName,ResultTestNumber,blnRange", PatientId + "," + FromDate + "," + ToDate + "," + OrdeID + "," + TestId + "," + resultname + "," + ResultTestNumber + "," + blnrange, gblnIsDefaultPrinter, "");

                clsPrintRpt.Dispose();
                clsPrintRpt = null;
            }
            catch (Exception exc)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(exc.ToString(), false);
            }


        }

        public Int64 GetCurrentPatientID
        {
            get { return _patientID; }
        }
        public Int64 LabPatientID { get; set; }

        public void frmViewgloLab_Activated(object sender, EventArgs e)
        {
            try
            {

                LabPatientID = _patientID;

                if (_clsSplit_Laborder != null)
                {
                    if ((uiPanSplitScreen_LabOrder != null))
                    {
                        if ((uiPanSplitScreen_LabOrder.Parent != null))
                        {
                            if (uiPanSplitScreen_LabOrder.Parent.Text == "Split Screen")
                            {
                                uiPanSplitScreen_LabOrder.Parent.Visible = true;
                            }
                            else if (uiPanSplitScreen_LabOrder.Text == "Split Screen")
                            {
                                uiPanSplitScreen_LabOrder.Visible = true;
                            }

                        }

                    }
                }

            }
            catch (Exception exc)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(exc.ToString(), false);
            }


        }

        public void frmViewgloLab_Deactivate(object sender, EventArgs e)
        {
            try
            {
                LabPatientID = 0;

                if ((this.Parent != null))
                {
                    if ((uiPanSplitScreen_LabOrder != null))
                    {
                        if ((uiPanSplitScreen_LabOrder.Parent != null))
                        {
                            if (!object.ReferenceEquals(uiPanSplitScreen_LabOrder.Parent, this))
                            {
                                uiPanSplitScreen_LabOrder.Parent.Visible = false;
                                uiPanSplitScreen_LabOrder.Parent.Hide();
                                uiPanSplitScreen_LabOrder.Parent.Update();
                            }
                        }
                    }
                }

                

            
            }
            catch (Exception exc)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(exc.ToString(), false);
            }

        }


        // private static frmViewgloLab frm;

        public static frmViewgloLab GetInstance()
        {
            try
            {
                IsOpen = false;

                foreach (Form f in Application.OpenForms)
                {
                    if (f.Name == "frmViewgloLab")
                    {
                        IsOpen = true;
                        frmViewgloLab frm = (frmViewgloLab)f;
                        return frm;
                    }
                }
                return null;
            }
            catch (Exception exc)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(exc.ToString(), true);
                return null;
            }
        }


        public Boolean CheckInstance()
        {
            try
            {
                GetInstance();
                if (IsOpen == true)
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
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return false;
            }

        }

        public void Navigate(string strstring)
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.Name == "frmViewNormalLab")
                {
                    if (((frmViewNormalLab)frm).oCurDoc != null)
                    {
                        ((frmViewNormalLab)frm).oCurDoc.ActiveWindow.SetFocus();
                        ((frmViewNormalLab)frm).Navigate(strstring);
                    }
                    
                   
                    break;
                }
            }
        }

        private void gloLabUC_Transaction1_gUC_AddFormHandlerClick()
        {
            gloEMRGeneralLibrary.frmLab_ContactInformation oContactInformation = new gloEMRGeneralLibrary.frmLab_ContactInformation();
            oContactInformation.nEditID = 0;
            oContactInformation.sEditContactName = "";
            oContactInformation.sEditFirstName = "";
            oContactInformation.sEditMiddleName = "";
            oContactInformation.sEditLastName = "";
            oContactInformation.blnIsModify = true;
            oContactInformation.blnContactType = gloEMRGeneralLibrary.gloEMRActors.LabActor.enumContactType.PreferredLab;
            oContactInformation.ShowDialog(((oContactInformation.Parent == null) ? this : oContactInformation.Parent));
            oContactInformation.Dispose();
            oContactInformation = null;
        }

     
    }

     
    public class LabRequestOrderParameter
    {
        private string _OrderNumberPrefix = "";
        private Int16 _OrderNumberID = 0;
        private Int64 _OrderID = 0;

        private bool _IsEditMode = false;
        private Int64 _PatientID = 0;
        private Int64 _VisitID = 0;
        private bool _CloseAfterSave = true;
        private System.DateTime _TransactionDate = DateTime.Now;

        private enumTransactionType _TransactionType;
        private long _ProviderID;
        private string _LabProviderName;

        public enum enumTransactionType
        {
            None = 0,
            LabOrder = 1,
            LabResult = 2,
            LabExternalResult = 3
        }

        public enumTransactionType TransactionType
        {
            get { return _TransactionType; }
            set { _TransactionType = value; }
        }

        public string OrderNumberPrefix
        {
            get { return _OrderNumberPrefix; }
            set { _OrderNumberPrefix = value; }
        }

        public Int16 OrderNumberID
        {
            get { return _OrderNumberID; }
            set { _OrderNumberID = value; }
        }

        public Int64 OrderID
        {
            get { return _OrderID; }
            set { _OrderID = value; }
        }

        public Int64 PatientID
        {
            get { return _PatientID; }
            set { _PatientID = value; }
        }

        public Int64 VisitID
        {
            get { return _VisitID; }
            set { _VisitID = value; }
        }

        public bool IsEditMode
        {
            get { return _IsEditMode; }
            set { _IsEditMode = value; }
        }

        public bool CloseAfterSave
        {
            get { return _CloseAfterSave; }
            set { _CloseAfterSave = value; }
        }

        public System.DateTime TransactionDate
        {
            get { return _TransactionDate; }
            set { _TransactionDate = value; }
        }
        public long ProviderID
        {
            get { return _ProviderID; }
            set { _ProviderID = value; }
        }
        //Sanjog - 2011Nov11 - To Don't override patient provider on lab provider
        public string LabProviderName
        {
            get { return _LabProviderName; }
            set { _LabProviderName = value; }
        }
        //Sanjog - 2011Nov11 - To Don't override patient provider on lab provider
        public LabRequestOrderParameter()
            : base()
        {
        }

    }

    public class clsPreviousHistory
    {

        #region Variables

        string _sConnectionString = string.Empty;
        private long _patientID = 0;
        #endregion


        #region Constructor

        public clsPreviousHistory(string DbConnectionString, long PatientID = 0)
        {
            _sConnectionString = DbConnectionString;
            _patientID = PatientID;
        }

        #endregion

        #region ProcessMethods

        public void SaveModifiedLabOrder(gloEMRGeneralLibrary.gloEMRActors.LabActor.LabOrder oLabActorOrderDetails)
        {
            gloEMRGeneralLibrary.gloEMRActors.LabActor.OrderTest _objOrderTest;
            gloEMRGeneralLibrary.gloEMRActors.LabActor.OrderTestResults _objTestResults;
            gloEMRGeneralLibrary.gloEMRActors.LabActor.OrderTestResultDetails _objTestResultDetails;
            gloEMRGeneralLibrary.gloEMRActors.LabActor.OrderTestResult _ObjTestResult;


            try
            {
                if ((oLabActorOrderDetails.OrderTests == null) == true)
                {
                    return;
                }

                //This below method deletes the diagnosis,treatments and result sets.
                for (int i = 0; i < oLabActorOrderDetails.OrderTests.Count; i++)
                {
                    _objOrderTest = oLabActorOrderDetails.OrderTests.get_Item(i);
                    if (_objOrderTest.OrderTestResults.Count > 0)
                    {

                        for (int K = 0; K < _objOrderTest.OrderTestResults.Count; K++)
                        {
                            
                            _ObjTestResult = _objOrderTest.OrderTestResults.get_Item(K);

                            Int64 _nOrderId = _ObjTestResult.OrderID;
                            Int64 _nTestId = _ObjTestResult.TestID;
                            Int64 _nTestResultNo = _ObjTestResult.TestResultNumber;

                            //This below method deletes the diagnosis,treatments and result sets.
                            DeleteResultSet(_nOrderId, _nTestId, _nTestResultNo);
                            DeleteDignosisNTreatments(_nOrderId, _nTestId);

                            _nOrderId = 0;
                            _nTestId = 0;
                            _nTestResultNo = 0;

                            _ObjTestResult = null;
                        }
                    }
                    else
                    {
                        DeleteDignosisNTreatments(oLabActorOrderDetails.OrderID, _objOrderTest.TestID);
                    }
                    _objOrderTest = null;
                }


                
                for (int j = 0; j < oLabActorOrderDetails.OrderTests.Count; j++)
                {

                    //Patch 2 Starts
                    //the below code insert testresults and result details.
                    _objTestResults = oLabActorOrderDetails.OrderTests.get_Item(j).OrderTestResults;

                    for (int _index = 0; _index < _objTestResults.Count; _index++)
                    {
                        if (SaveOrderTestResult(_objTestResults, _index))
                        {

                            _objTestResultDetails = _objTestResults.get_Item(_index).TestResultDetails;
                            SaveOrderTestResultDetails(_objTestResultDetails);

                            _objTestResultDetails = null;
                        }
                    }
                    //Patch 2 Ends


                    //The below code updates test and inserts diagnosis and treatments.
                    //Patch 1 Starts
                    _objOrderTest = oLabActorOrderDetails.OrderTests.get_Item(j);

                    if (UpdateTestDtl(_objOrderTest, oLabActorOrderDetails.OrderID))
                    {
                       SaveTestPreferredLab(oLabActorOrderDetails.OrderID ,_objOrderTest.TestID,_objOrderTest.TestPreferredLabID,_objOrderTest.TestPreferredLab);
                        SaveTestDiagnosis(_objOrderTest, oLabActorOrderDetails.OrderID);
                        SaveTestTreatments(_objOrderTest, oLabActorOrderDetails.OrderID);

                    }
                    else
                    {
                        _objOrderTest = null;
                        return;
                    }
                    //Patch 1 Ends

                    _objTestResults = null;
                }

                UpdateOrderMst(oLabActorOrderDetails);

                gloEMRGeneralLibrary.gloEMRActors.LabActor.OrderTest _clsLabOrderTests;
                _clsLabOrderTests = new gloEMRGeneralLibrary.gloEMRActors.LabActor.OrderTest();
                DataTable labDocument = oLabActorOrderDetails.OrderTests.get_Item(0).LabURLDocument;
                if (labDocument != null)
                {
                    _clsLabOrderTests.INUP_Test_URLdocument(oLabActorOrderDetails.PatientID, oLabActorOrderDetails.OrderID, labDocument);
                    labDocument = null;
                }
                if ((_clsLabOrderTests != null))
                {

                    _clsLabOrderTests = null;
                }


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }

        }
        #endregion

        #region DeleteMethods

        private bool DeleteDignosisNTreatments(Int64 nLabOrderId, Int64 nTestId)
        {
            bool _blnResult = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_sConnectionString);
            string _strQuery = string.Empty;

            try
            {
                oDB.Connect(false);

                _strQuery = "DELETE FROM Lab_Order_TestDtl_DiagCPT WHERE labodtl_OrderID =" + nLabOrderId + " AND labodtl_TestID= " + nTestId;

                oDB.Execute_Query(_strQuery);


                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                _blnResult = false;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null; //added for memory management  
                }

                _strQuery = string.Empty;
            }

            return _blnResult;

        }

        private bool DeleteResultSet(Int64 nLabOrderId, Int64 nTestId, Int64 nResultNo)
        {
            bool _blnResult = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_sConnectionString);
            string _strQuery = string.Empty;
       //     int _nRow = 0;
            try
            {
                oDB.Connect(false);

                _strQuery = "DELETE FROM Lab_Order_TestDtl_DiagCPT WHERE labodtl_OrderID =" + nLabOrderId + " AND labodtl_TestID= " + nTestId;

                oDB.Execute_Query(_strQuery);

                _strQuery = string.Empty;

                //_strQuery = "Delete From Lab_Order_Test_Result Where labotr_OrderID= " + nLabOrderId + " AND labotr_TestID=" + nTestId + " AND labotr_TestResultNumber = " + nResultNo;

                //_nRow = oDB.Execute_Query(_strQuery);

                //_strQuery = string.Empty;

                //if (_nRow > 0)
                //{
                //    _strQuery = "Delete From Lab_Order_Test_ResultDtl Where labotrd_OrderID= " + nLabOrderId + " AND labotrd_TestID=" + nTestId + " AND labotrd_TestResultNumber=" + nResultNo;


                //    oDB.Execute_Query(_strQuery );
                //    _blnResult = true;
                //}

                _blnResult = true;

                oDB.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                _blnResult = false;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null; //added for memory management
                }

                _strQuery = string.Empty;
            }

            return _blnResult;
        }

        #endregion

        #region UpdateMethods

        private bool SaveOrderTestResult(gloEMRGeneralLibrary.gloEMRActors.LabActor.OrderTestResults ObjLabTestResults, int nIndex)
        {
            bool _blnResult = false;

            gloDatabaseLayer.DBLayer oDBLayer = new gloDatabaseLayer.DBLayer(_sConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters;
            try
            {
                if (ObjLabTestResults.Count <= 0)
                {
                    return false;
                }
                gloEMRGeneralLibrary.gloEMRActors.LabActor.OrderTestResult objLabTestResult = ObjLabTestResults.get_Item(nIndex);
                if (objLabTestResult != null)
                {
                    oDBLayer.Connect(false);


                    oDBParameters = new gloDatabaseLayer.DBParameters();

                    oDBParameters.Add("@labotr_OrderID", objLabTestResult.OrderID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@labotr_TestID", objLabTestResult.TestID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@labotr_TestResultNumber", objLabTestResult.TestResultNumber, ParameterDirection.Input, SqlDbType.Int);
                    oDBParameters.Add("@labotr_TestResultName", objLabTestResult.TestResultName, ParameterDirection.Input, SqlDbType.VarChar);
                    //if ObjLabTestResults.get_Item(nIndex).TestResultDateTime=""
                    if (Convert.ToString(objLabTestResult.TestResultDateTime) == "1/1/0001 12:00:00 AM")
                    {
                        oDBParameters.Add("@labotr_TestResultDateTime", DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime);
                    }
                    else
                    {
                        oDBParameters.Add("@labotr_TestResultDateTime", objLabTestResult.TestResultDateTime, ParameterDirection.Input, SqlDbType.DateTime);
                    }
                    oDBParameters.Add("@labotr_IsFinished", objLabTestResult.IsFinished, ParameterDirection.Input, SqlDbType.Int);
                    oDBParameters.Add("@labotr_DMSID", objLabTestResult.DMSID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@labotr_TestName", objLabTestResult.TestName, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@labotr_SpecimenReceivedDateTime", objLabTestResult.SpecimenReceivedDateTime, ParameterDirection.Input, SqlDbType.DateTime);

                    if (objLabTestResult.ReportedDateTime.ToString() == "1/1/0001 12:00:00 AM")
                    {
                        oDBParameters.Add("@Flag", 1, ParameterDirection.Input, SqlDbType.Bit);
                        oDBParameters.Add("@labotr_ResultTransferDateTime", System.DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime);
                    }
                    else
                    {
                        oDBParameters.Add("@labotr_ResultTransferDateTime", objLabTestResult.ReportedDateTime, ParameterDirection.Input, SqlDbType.DateTime);
                    }

                    oDBParameters.Add("@labotr_AlternateTestName", objLabTestResult.AlternateTestName + "", ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@labotr_AlternateTestCode", objLabTestResult.AlternateTestCode + "", ParameterDirection.Input, SqlDbType.VarChar);

                    //Added new parametors by manoj jadhav to save 
                    oDBParameters.Add("@TestResultDateTimeUTC", objLabTestResult.TestResultDateTimeUTC, ParameterDirection.Input, SqlDbType.Int);
                    oDBParameters.Add("@SpecimenReceivedDateTimeUTC", objLabTestResult.SpecimenReceivedDateTimeUTC, ParameterDirection.Input, SqlDbType.Int);
                    oDBParameters.Add("@ResultTransferDateTimeUTC", objLabTestResult.ResultTransferDateTimeUTC, ParameterDirection.Input, SqlDbType.Int);

                    oDBLayer.ExecuteScalar("Lab_InsertOrderTestResult_InsertUpdate", oDBParameters);


                    if (oDBParameters != null)
                    {
                        oDBParameters.Dispose();
                        oDBParameters = null; //added for memory management
                    }

                    oDBLayer.Disconnect();
                }
                _blnResult = true;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                _blnResult = true;
            }
            finally
            {
                if (oDBLayer != null)
                {
                    oDBLayer.Dispose();
                    oDBLayer = null; //added for memory management  
                }

            }
            return _blnResult;

        }

        private bool SaveOrderTestResultDetails(gloEMRGeneralLibrary.gloEMRActors.LabActor.OrderTestResultDetails ObjLabTestResultDetails)
        {
            bool _blnResult = false;

            gloDatabaseLayer.DBLayer oDBLayer = new gloDatabaseLayer.DBLayer(_sConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters;
            try
            {

                oDBLayer.Connect(false);

                for (int i = 0; i < ObjLabTestResultDetails.Count; i++)
                {
                    gloEMRGeneralLibrary.gloEMRActors.LabActor.OrderTestResultDetail objLabTestResultDetail = ObjLabTestResultDetails.get_Item(i);
                    if (objLabTestResultDetail != null)
                    {
                        oDBParameters = new gloDatabaseLayer.DBParameters();

                        oDBParameters.Add("@labotrd_OrderID", objLabTestResultDetail.OrderID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@labotrd_TestID", objLabTestResultDetail.TestID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@labotrd_TestResultNumber", objLabTestResultDetail.TestResultNumber, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@labotrd_ResultLineNo", objLabTestResultDetail.ResultLineNo, ParameterDirection.Input, SqlDbType.Int);
                        oDBParameters.Add("@labotrd_ResultNameID", objLabTestResultDetail.ResultNameID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@labotrd_ResultName", objLabTestResultDetail.ResultName + "", ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@labotrd_ResultValue", objLabTestResultDetail.ResultValue + "", ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@labotrd_ResultUnit", objLabTestResultDetail.ResultUnit + "", ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@labotrd_ResultRange", objLabTestResultDetail.ResultRange + "", ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@labotrd_ResultType", objLabTestResultDetail.ResultTypeCode + "", ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@labotrd_AbnormalFlag", objLabTestResultDetail.AbnormalFlagCode + "", ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@labotrd_ResultComment", objLabTestResultDetail.ResultComment + "", ParameterDirection.Input, SqlDbType.VarChar);
                        if (objLabTestResultDetail.ResultWord != null)
                        {
                            oDBParameters.Add("@labotrd_ResultWord", ((byte[])objLabTestResultDetail.ResultWord).Clone(), ParameterDirection.Input, SqlDbType.Image);
                        }
                        else
                        {
                            oDBParameters.Add("@labotrd_ResultWord", DBNull.Value, ParameterDirection.Input, SqlDbType.Image);
                        }
                        oDBParameters.Add("@labotrd_ResultDMSID", objLabTestResultDetail.ResultDMSID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@labotrd_ResultUserID", objLabTestResultDetail.UserID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@labotrd_ResultDateTime", objLabTestResultDetail.ResultDateTime, ParameterDirection.Input, SqlDbType.DateTime);
                        oDBParameters.Add("@labotrd_IsFinished", objLabTestResultDetail.IsFinished, ParameterDirection.Input, SqlDbType.Int);
                        oDBParameters.Add("@labotrd_LOINCID", objLabTestResultDetail.ResultLOINCID + "", ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@labotrd_TestName", objLabTestResultDetail.TestName + "", ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@labotrd_AlternateResultName", objLabTestResultDetail.AlternateResultName + "", ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@labotrd_AlternateResultCode", objLabTestResultDetail.AlternateResultCode + "", ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@labotrd_ProducerIdentifier", objLabTestResultDetail.ProducerIdentifier + "", ParameterDirection.Input, SqlDbType.VarChar);
                        //Added by Abhijeet on 20101027 
                        oDBParameters.Add("@labotrd_LabFacilityName", objLabTestResultDetail.LabFacilityName + "", ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@labotrd_LabFacilityStreetAddress", objLabTestResultDetail.LabFacilityStreetAddress + "", ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@labotrd_LabFacilityCity", objLabTestResultDetail.LabFacilityCity + "", ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@labotrd_LabFacilityState", objLabTestResultDetail.LabFacilityState + "", ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@labotrd_LabFacilityZipCode", objLabTestResultDetail.LabFacilityZipCode + "", ParameterDirection.Input, SqlDbType.VarChar);
                        //End of changes by Abhijeet on 20101027 
                        // string str =Convert.ToString( objLabTestResultDetail.TestSpecimenCollectionDate);
                        string str = Convert.ToString(objLabTestResultDetail.TestSpecimenCollectionDateTime);
                        if (str == "1/1/0001 12:00:00 AM" || str == null)
                        {
                            oDBParameters.Add("@labotrd_TestSpecimenCollectionDateTime", null, ParameterDirection.Input, SqlDbType.DateTime);
                        }
                        else
                        {
                            oDBParameters.Add("@labotrd_TestSpecimenCollectionDateTime", objLabTestResultDetail.TestSpecimenCollectionDateTime + "", ParameterDirection.Input, SqlDbType.DateTime);
                        }

                        oDBParameters.Add("@labotrd_LabFacilityIdentifierTypeCode", objLabTestResultDetail.LabFacilityIdentifierTypeCode + "", ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@labotrd_LabFacilityOrganizationIdentifier", objLabTestResultDetail.LabFacilityOrganizationIdentifier + "", ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@labotrd_LabFacilityCountry", objLabTestResultDetail.LabFacilityCountry + "", ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@labotrd_LabFacilityCountyOrParishCode", objLabTestResultDetail.LabFacilityCountyOrParishCode + "", ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@labotrd_ResultCode", objLabTestResultDetail.ResultCode + "", ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@labotrd_ResultCodeType", objLabTestResultDetail.ResultCodeType + "", ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@labotrd_LabFacilityMedicalDirectorIDNumber", objLabTestResultDetail.LabFacilityMedicalDirectorIDNumber + "", ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@labotrd_LabFacilityMedicalDirectorLastName", objLabTestResultDetail.LabFacilityMedicalDirectorLastName + "", ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@labotrd_LabFacilityMedicalDirectorMiddleName", objLabTestResultDetail.LabFacilityMedicalDirectorMiddleName + "", ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@labotrd_LabFacilityMedicalDirectorSuffix", objLabTestResultDetail.LabFacilityMedicalDirectorSuffix + "", ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@labotrd_LabFacilityMedicalDirectorPrefix", objLabTestResultDetail.LabFacilityMedicalDirectorPrefix + "", ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@labotrd_LabFacilityMedicalDirectorFirstName", objLabTestResultDetail.LabFacilityMedicalDirectorFirstName + "", ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@labotrd_ResultParentChildFlag", objLabTestResultDetail.ResultParentChildFlag, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@labotrd_ResultDateTimeUTC", objLabTestResultDetail.ResultDateTimeUTC, ParameterDirection.Input, SqlDbType.Int);
                        oDBParameters.Add("@labotrd_TestSpecimenCollectionDateTimeUTC", objLabTestResultDetail.TestSpecimenCollectionDateTimeUTC, ParameterDirection.Input, SqlDbType.Int);

                        oDBLayer.ExecuteScalar("Lab_InsertOrderTestResultDetails_InsertUpdate", oDBParameters);

                        if (oDBParameters != null)
                        {
                            oDBParameters.Dispose();
                            oDBParameters = null; //added for memory management
                        }
                    }
                }
                oDBLayer.Disconnect();

                _blnResult = true;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDBLayer != null)
                {
                    oDBLayer.Dispose();
                }
            }
            return _blnResult;

        }

        private void SaveTestPreferredLab(long OrderID, long TestID, long TestPreferredLabID, string TestPreferredLab)
        {


            gloDatabaseLayer.DBLayer oDBLayer = new gloDatabaseLayer.DBLayer(_sConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters;
            try
            {

                oDBLayer.Connect(false);


                oDBParameters = new gloDatabaseLayer.DBParameters();

                oDBParameters.Add("@labodtl_OrderID", OrderID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@labodtl_TestID", TestID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@labodtl_PreferredLabId", TestPreferredLabID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@labodtl_PreferredLab", TestPreferredLab, ParameterDirection.Input, SqlDbType.VarChar);

                oDBLayer.ExecuteScalar("Lab_InsertOrderTestDtl_PreferredLab", oDBParameters);
                oDBLayer.Disconnect();
                if (oDBParameters != null)
                {
                    oDBParameters.Dispose();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDBLayer != null)
                {
                    oDBLayer.Dispose();
                }
            }


        }

        private void SaveTestDiagnosis(gloEMRGeneralLibrary.gloEMRActors.LabActor.OrderTest objTestDiagnosis, Int64 nOrderId)
        {
            gloDatabaseLayer.DBLayer oDBLayer = new gloDatabaseLayer.DBLayer(_sConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters;
            try
            {
                if (objTestDiagnosis.Diagonosis.Count < 1)
                {
                    return;
                }

                oDBLayer.Connect(false);

                for (int i = 0; i < objTestDiagnosis.Diagonosis.Count; i++)
                {
                    gloEMRGeneralLibrary.gloEMRActors.LabActor.ItemDetail objDiagnosisItemDetail = objTestDiagnosis.Diagonosis.get_Item(i);
                    if (objDiagnosisItemDetail != null)
                    {
                        oDBParameters = new gloDatabaseLayer.DBParameters();

                        oDBParameters.Add("@labodtl_OrderID", nOrderId, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@labodtl_TestID", objTestDiagnosis.TestID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@labodtl_DiagCPTID", objDiagnosisItemDetail.ID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@labodtl_Code", objDiagnosisItemDetail.Code + "", ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@labodtl_Description", objDiagnosisItemDetail.Description + "", ParameterDirection.Input, SqlDbType.VarChar);
                        //labodtl_Type = 1 for Diagonosis and 2 for Treatment
                        oDBParameters.Add("@labodtl_Type", 1, ParameterDirection.Input, SqlDbType.Int);
                        oDBParameters.Add("@labodtl_TestName", objTestDiagnosis.TestName, ParameterDirection.Input, SqlDbType.VarChar);
                        //nicdrevison added for icd10 feature
                        oDBParameters.Add("@nIcdRevision", objDiagnosisItemDetail.IcdRevision, ParameterDirection.Input, SqlDbType.Int);

                        oDBLayer.ExecuteScalar("Lab_InsertOrderDiagonosis", oDBParameters);

                        if (oDBParameters != null)
                        {
                            oDBParameters.Dispose();
                            oDBParameters = null; //added for memory management
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
                if (oDBLayer != null)
                {
                    oDBLayer.Dispose();
                }
            }
        }

        private void SaveTestTreatments(gloEMRGeneralLibrary.gloEMRActors.LabActor.OrderTest objTestTreatments, Int64 nOrderId)
        {
            gloDatabaseLayer.DBLayer oDBLayer = new gloDatabaseLayer.DBLayer(_sConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters;
            try
            {

                if (objTestTreatments.Treatments.Count < 1)
                {
                    return;
                }

                oDBLayer.Connect(false);

                for (int i = 0; i < objTestTreatments.Treatments.Count; i++)
                {
                    gloEMRGeneralLibrary.gloEMRActors.LabActor.ItemDetail objTreatmentItemDetail = objTestTreatments.Treatments.get_Item(i);
                    if (objTreatmentItemDetail != null)
                    {
                        oDBParameters = new gloDatabaseLayer.DBParameters();

                        oDBParameters.Add("@labodtl_OrderID", nOrderId, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@labodtl_TestID", objTestTreatments.TestID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@labodtl_DiagCPTID", objTreatmentItemDetail.ID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@labodtl_Code", objTreatmentItemDetail.Code + "", ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@labodtl_Description", objTreatmentItemDetail.Description + "", ParameterDirection.Input, SqlDbType.VarChar);
                        //nicdrevison added for icd10 feature
                        // oDBParameters.Add("@nIcdRevision", objTestTreatments.Diagonosis.get_Item(i).IcdRevision, ParameterDirection.Input, SqlDbType.Int);
                        //SLR: Changed the above statement to following:
                        oDBParameters.Add("@nIcdRevision", objTreatmentItemDetail.IcdRevision, ParameterDirection.Input, SqlDbType.Int);
                        //labodtl_Type = 1 for Diagonosis and 2 for Treatment
                        oDBParameters.Add("@labodtl_Type", 2, ParameterDirection.Input, SqlDbType.Int);
                        oDBParameters.Add("@labodtl_TestName", objTestTreatments.TestName, ParameterDirection.Input, SqlDbType.VarChar, 255);

                        oDBLayer.ExecuteScalar("Lab_InsertOrderDiagonosis", oDBParameters);

                        if (oDBParameters != null)
                        {
                            oDBParameters.Dispose();
                            oDBParameters = null; //added for memory management
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
                if (oDBLayer != null)
                {
                    oDBLayer.Dispose();
                }
            }
        }

        private bool UpdateTestDtl(gloEMRGeneralLibrary.gloEMRActors.LabActor.OrderTest ObjOrderTest, Int64 nOrderId)
        {
            gloDatabaseLayer.DBLayer oDBLayer = new gloDatabaseLayer.DBLayer(_sConnectionString);
            string _strQuery = string.Empty;
            bool _blnResult = false;
            string _sValue = string.Empty;
            try
            {
                _strQuery = "UPDATE Lab_Order_TestDtl SET";

                if (ObjOrderTest.Note.ToString().Trim().Length > 0)
                {
                    _sValue = ObjOrderTest.Note.Replace("'", "''");

                    _strQuery = _strQuery + " labotd_Note =" + _sValue + "' ,";
                }

                _strQuery = _strQuery + " labotd_SpecimenID = " + GetSpecimanID(ObjOrderTest.Specimen) + " ," +

                 " labotd_CollectionID = " + GetCollectionID(ObjOrderTest.Collection) + "," +
                 " labotd_StorageID = " + GetStorageID(ObjOrderTest.Storage);

                if (ObjOrderTest.LOINCCode.ToString().Trim().Length > 0)
                {
                    _sValue = string.Empty;
                    _sValue = ObjOrderTest.LOINCCode.Replace("'", "''");

                    _strQuery = _strQuery + " ,labotd_LOINCCode =  '" + _sValue + "' ";
                }

                if (ObjOrderTest.Instruction.ToString().Trim().Length > 0)
                {
                    _sValue = string.Empty;
                    _sValue = ObjOrderTest.Instruction.Replace("'", "''");

                    _strQuery = _strQuery + " ,labotd_Instruction = '" + _sValue + "' ";
                }

                if (ObjOrderTest.Precaution.ToString().Trim().Length > 0)
                {
                    _sValue = string.Empty;
                    _sValue = ObjOrderTest.Precaution.Replace("'", "''");
                    _strQuery = _strQuery + ", labotd_Precaution = '" + _sValue + "' ";
                }

                if (ObjOrderTest.Comments.ToString().Trim().Length > 0)
                {
                    _sValue = string.Empty;
                    _sValue = ObjOrderTest.Comments.Replace("'", "''");
                    _strQuery = _strQuery + " , labotd_Comment = '" + _sValue + "'  ";
                }

                //25-May-15 Aniket: Resolved Bug #83524: EMR: Order andResults- As user clear comment from test, its does not get reflect in liquid link
                else
                {
                    _strQuery = _strQuery + " , labotd_Comment = ''  ";
                }

                _strQuery = _strQuery + " , labotd_DateTime = '" + ObjOrderTest.TestDateTime + "'," +
                                          " labotd_UserID = " + ObjOrderTest.UserID + "," +
                                          " labotd_DMSID = " + ObjOrderTest.DMSID + "," +
                    //Sanjog - Added ON 2011 MAY 19 to handle "'" in test name 
                                          " labotd_TestName = '" + ObjOrderTest.TestName.Replace("'", "''") + "' ";
                //Sanjog - Added ON 2011 MAY 19 to handle "'" in test name 

                if (ObjOrderTest.SpecimenName.ToString().Trim().Length > 0)
                {
                    _sValue = string.Empty;
                    _sValue = ObjOrderTest.SpecimenName.Replace("'", "''");

                    _strQuery = _strQuery + " ,labotd_SpecimenName ='" + _sValue + "' ";
                }

                if (ObjOrderTest.CollectionName.ToString().Trim().Length > 0)
                {
                    _sValue = string.Empty;
                    _sValue = ObjOrderTest.CollectionName.Replace("'", "''");
                    _strQuery = _strQuery + " ,labotd_CollectionName ='" + _sValue + "' ";
                }
                if (ObjOrderTest.StorageName.ToString().Trim().Length > 0)
                {
                    _sValue = string.Empty;
                    _sValue = ObjOrderTest.StorageName.Replace("'", "''");
                    _strQuery = _strQuery + " ,labotd_StorageName ='" + _sValue + "' ";
                }

                //Added by Abhijeet on 20101027
                if (ObjOrderTest.TestStatus.ToString().Trim().Length > 0)
                {
                    _sValue = string.Empty;
                    _sValue = ObjOrderTest.TestStatus.Replace("'", "''");
                    _strQuery = _strQuery + " ,labotd_TestStatus ='" + _sValue + "' ";
                }
                if (ObjOrderTest.SpecimenConditionDisp.ToString().Trim().Length > 0)
                {
                    _sValue = string.Empty;
                    _sValue = ObjOrderTest.SpecimenConditionDisp.Replace("'", "''");
                    _strQuery = _strQuery + " ,labotd_SpecimenConditionDisp ='" + _sValue + "' ";
                }
                if (ObjOrderTest.SpecimenSource.ToString().Trim().Length > 0)
                {
                    _sValue = string.Empty;
                    _sValue = ObjOrderTest.SpecimenSource.Replace("'", "''");
                    _strQuery = _strQuery + " ,labotd_SpecimenSource ='" + _sValue + "' ";
                }
                //end of changes by Abhijeet on 20101027


                if (ObjOrderTest.ScheduleDateTime.ToString().Trim().Length > 0)
                {
                    _sValue = string.Empty;
                    _sValue = ObjOrderTest.ScheduleDateTime.ToString().Replace("'", "''");
                    if (ObjOrderTest.ScheduleDateTime.ToString() == "1/1/0001 12:00:00 AM" || ObjOrderTest.ScheduleDateTime.ToString() == "12:00:00 AM" || ObjOrderTest.ScheduleDateTime == null)
                    {
                        //   _strQuery = _strQuery;
                    }
                    else
                    {
                        //30-Mat-17 Aniket: Resolving Bug #104702: gloEMR >> After "Fax&Cls" Application shows, "Error While Accessing Database" Message.
                        if (IsDate(_sValue) == true)
                        {
                            _strQuery = _strQuery + " ,labotd_TestScheduledDateTime ='" + _sValue + "' ";
                        }
                    }
                }


                _strQuery = _strQuery + " WHERE labotd_OrderID =" + nOrderId + " AND labotd_TestID = " + ObjOrderTest.TestID;


                oDBLayer.Connect(false);

                oDBLayer.Execute_Query(_strQuery);

                oDBLayer.Disconnect();
                //if (ObjOrderTest.DMSIDCollection != "")
                //{
                //Update  Lab_Order_Test_ResultDtl_Attachments Table For DMS

                ObjOrderTest.INUP_Test_Attachment(_patientID, nOrderId, ObjOrderTest.TestID, ObjOrderTest.DMSIDCollection);
                //--x--
                // }

                _blnResult = true;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                _blnResult = true;
            }
            finally
            {
                if (oDBLayer != null)
                {
                    oDBLayer.Dispose();
                }
                _strQuery = string.Empty;
            }
            return _blnResult;
        }

        public static bool IsDate(Object obj)
        {
            string strDate = obj.ToString();
            try
            {
                DateTime dt = DateTime.Parse(strDate);
                if (dt != DateTime.MinValue && dt != DateTime.MaxValue)
                    return true;
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void UpdateOrderMst(gloEMRGeneralLibrary.gloEMRActors.LabActor.LabOrder LabOrder)
        {
            gloDatabaseLayer.DBLayer oDBLayer = new gloDatabaseLayer.DBLayer(_sConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {

                oDBLayer.Connect(false);

                oDBParameters.Add("@labom_OrderID", LabOrder.OrderID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@labom_OrderNoPrefix", LabOrder.OrderNoPrefix, ParameterDirection.Input, SqlDbType.VarChar, 60);
                oDBParameters.Add("@labom_OrderNoID", LabOrder.OrderNoID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@labom_DateTime", LabOrder.TransactionDate, ParameterDirection.Input, SqlDbType.DateTime);
                oDBParameters.Add("@labom_PatientID", LabOrder.PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@labom_PatientAgeYear", LabOrder.PatientAge.Years, ParameterDirection.Input, SqlDbType.Int);
                oDBParameters.Add("@labom_PatientAgeMonth", LabOrder.PatientAge.Months, ParameterDirection.Input, SqlDbType.Int);
                oDBParameters.Add("@labom_PatientAgeDay", LabOrder.PatientAge.Days, ParameterDirection.Input, SqlDbType.Int);
                oDBParameters.Add("@labom_ProviderID", LabOrder.ProviderID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@labom_PreferredLabID", LabOrder.PreferredLabID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@SendTo", LabOrder.SendTo, ParameterDirection.Input, SqlDbType.Int);
                oDBParameters.Add("@labom_ReferredToID", LabOrder.ReferredToID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@labom_SampledByID", LabOrder.SampledByID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@labom_ReferredByID", LabOrder.ReferredByID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@labom_VisitID", LabOrder.VisitID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@labom_DMSID", LabOrder.DMSID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@labom_PreferredLabName", LabOrder.PreferredLab + "", ParameterDirection.Input, SqlDbType.VarChar, 50);
                oDBParameters.Add("@labom_SampledByName", LabOrder.SampledBy + "", ParameterDirection.Input, SqlDbType.VarChar, 50);
                oDBParameters.Add("@labom_ReferredByFName", LabOrder.ReferredByFName + "", ParameterDirection.Input, SqlDbType.VarChar, 50);
                oDBParameters.Add("@labom_ReferredByMName", LabOrder.ReferredByMName + "", ParameterDirection.Input, SqlDbType.VarChar, 50);
                oDBParameters.Add("@labom_ReferredByLName", LabOrder.ReferredByLName + "", ParameterDirection.Input, SqlDbType.VarChar, 50);
                oDBParameters.Add("@nClinicID", LabOrder.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

                if (LabOrder.OrderStatusNumber != 0)
                {
                    oDBParameters.Add("@OrderStatusNumber", LabOrder.OrderStatusNumber, ParameterDirection.Input, SqlDbType.Int);
                }

                oDBParameters.Add("@blnOrderNotCPOE", LabOrder.blnOrderNotCPOE, ParameterDirection.Input, SqlDbType.Bit);
                oDBParameters.Add("@bOutboundTransistion", LabOrder.bOutboundTransistion, ParameterDirection.Input, SqlDbType.Bit);
                oDBParameters.Add("@sFastingStatus", LabOrder.FastingStatus, ParameterDirection.Input, SqlDbType.VarChar);

                oDBLayer.ExecuteScalar("Lab_UpdateOrderMaster", oDBParameters);

                if (oDBParameters != null)
                {
                    oDBParameters.Dispose();
                    oDBParameters = null; //added for memory management
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDBLayer != null)
                {
                    oDBLayer.Dispose();
                }
            }

        }

        //private void INUP_Test_Attachment(Int64 _PatientID, Int64 _OrderID, Int64 _TestID, string _DMSCollection)
        //{
        //    gloDatabaseLayer.DBLayer oDBLayer = new gloDatabaseLayer.DBLayer(_sConnectionString);
        //    gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
        //    try
        //    {

        //        oDBLayer.Connect(false);

        //        oDBParameters.Add("@PatientID", _PatientID, ParameterDirection.Input, SqlDbType.BigInt);
        //        oDBParameters.Add("@OrderID", _OrderID, ParameterDirection.Input, SqlDbType.BigInt);
        //        oDBParameters.Add("@TestID", _TestID, ParameterDirection.Input, SqlDbType.BigInt);
        //        oDBParameters.Add("@DocumentID", _DMSCollection, ParameterDirection.Input, SqlDbType.VarChar);

        //        oDBLayer.ExecuteScalar("Lab_INUP_Test_Attachment", oDBParameters);

        //        oDBParameters = null;
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
        //    }
        //    finally
        //    {
        //        if (oDBLayer != null)
        //        {
        //            oDBLayer.Dispose();
        //        }
        //    }

        //}
        #endregion

        #region RetriveMethods

        /// <summary>
        /// Method fo specimen id
        /// </summary>
        /// <param name="Specimen"></param>
        /// <returns></returns>
        private Int64 GetSpecimanID(string Specimen)
        {
            gloDatabaseLayer.DBLayer oDBLayer = new gloDatabaseLayer.DBLayer(_sConnectionString);
            string _strQuery = string.Empty;
            object _objResult = null;
            Int64 _Result = 0;

            try
            {
                oDBLayer.Connect(false);

                _strQuery = "SELECT labCSST_ID FROM Lab_CSST_MST WHERE Upper(labCSST_Name) = '" + Specimen.ToUpper() + "'";

                _objResult = oDBLayer.ExecuteScalar_Query(_strQuery);

                if (_objResult != null && _objResult.ToString() != "")
                {
                    _Result = Convert.ToInt64(_objResult);
                }

                oDBLayer.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                _Result = 0;
            }
            finally
            {
                if (oDBLayer != null)
                {
                    oDBLayer.Dispose();
                    oDBLayer = null; //added for memory management  
                }
                if (_objResult != null)
                {
                    _objResult = null;
                }
                _strQuery = string.Empty;
            }

            return _Result;
        }

        /// <summary>
        /// Method for collection Id
        /// </summary>
        /// <param name="CollectionName"></param>
        /// <returns></returns>
        private Int64 GetCollectionID(string CollectionName)
        {
            gloDatabaseLayer.DBLayer oDBLayer = new gloDatabaseLayer.DBLayer(_sConnectionString);
            string _strQuery = string.Empty;
            object _objResult = null;

            Int64 _Result = 0;

            try
            {
                oDBLayer.Connect(false);

                _strQuery = "SELECT labcm_ID FROM Lab_Collection_Mst WHERE Upper(labcm_Name) = '" + CollectionName.ToUpper() + "'";

                _objResult = oDBLayer.ExecuteScalar_Query(_strQuery);

                if (_objResult != null && _objResult.ToString() != "")
                {
                    _Result = Convert.ToInt64(_objResult);
                }

                oDBLayer.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                _Result = 0;
            }
            finally
            {
                if (oDBLayer != null)
                {
                    oDBLayer.Dispose();
                    oDBLayer = null; //added for memory management  
                }
                if (_objResult != null)
                {
                    _objResult = null;
                }
                _strQuery = string.Empty;
            }
            return _Result;
        }

        /// <summary>
        /// method for storage id
        /// </summary>
        /// <param name="StorageTemperature"></param>
        /// <returns></returns>
        private Int64 GetStorageID(string StorageTemperature)
        {
            gloDatabaseLayer.DBLayer oDBLayer = new gloDatabaseLayer.DBLayer(_sConnectionString);
            string _strQuery = string.Empty;
            object _objResult = null;
            Int64 _Result = 0;

            try
            {
                oDBLayer.Connect(false);

                _strQuery = "SELECT labstm_ID FROM Lab_StorageTemp_Mst WHERE Upper(labstm_Name) = '" + StorageTemperature.ToUpper() + "'";
                _objResult = oDBLayer.ExecuteScalar_Query(_strQuery);

                if (_objResult != null && _objResult.ToString() != "")
                {
                    _Result = Convert.ToInt64(_objResult);
                }

                oDBLayer.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                _Result = 0;
            }
            finally
            {
                if (oDBLayer != null)
                {
                    oDBLayer.Dispose();
                    oDBLayer = null; //added for memory management  
                }
                if (_objResult != null)
                {
                    _objResult = null;
                }
                _strQuery = string.Empty;
            }
            return _Result;
        }

        #endregion

    }

}
