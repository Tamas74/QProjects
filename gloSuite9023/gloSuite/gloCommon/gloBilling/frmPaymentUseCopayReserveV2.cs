using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using gloPatient;
using gloBilling;
using System.Runtime.InteropServices;

namespace gloAccountsV2
{
    public partial class frmPaymentUseCopayReserveV2: Form
    {

        #region "Constants For TOP MOST Form"

        static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        const UInt32 SWP_NOSIZE = 0x0001;
        const UInt32 SWP_NOMOVE = 0x0002;
        const UInt32 TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        //Called the above function onLoad()
        #endregion

        #region " Private Variables "

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private string _databaseconnectionstring = "";
        private Int64 _ClinicID = 0;
        Int64 _UserId = 0;
        string _UserName = "";
        private string _MessageBoxCaption = "";
        private bool _IsFormLoading = false;
        private Int64 _patientId = 0;
        bool IsProviderEnable = false;
        public decimal SelectedUseReserveAmount = 0;
        public gloGeneralItem.gloItems oSeletedReserveItems = new gloGeneralItem.gloItems();

        //Added by Mahesh S(Apollo) on 10-may-2011
        private Int64 _nPAccountID = 0;
        private DateTime _dtReserveforDOS = DateTime.Now;       

        private DateTime _closeDate = DateTime.Now;
        private DateTime _dtStartDate = DateTime.Now;
        private DateTime _dtEndDate = DateTime.Now;
        private string _closeDayTray = "";
        private bool _isValidResAmount = true;
        private bool _isFormClosing = false;
        public bool IsCallingFromAutoCopayDist = false;

        C1.Win.C1FlexGrid.CellStyle csYellowStyle;
        C1.Win.C1FlexGrid.CellStyle csGreenStyle;

        #endregion " Private Variables "

        #region  " Grid Constants "

        //nEOBPaymentID, nEOBID, nEOBDtlID, nEOBPaymentDetailID, 
        //nBillingTransactionID, nBillingTransactionDetailID, nBillingTransactionLineNo, 
        //nPatientID, nDOSFrom, nDOSTo, nAmount, nPayMode, 
        //nRefEOBPaymentID, nRefEOBPaymentDetailID, 
        //nAccountID, nAccountType, nMSTAccountID, nMSTAccountType 

        const int COL_EOBPAYMENTID = 0;
        const int COL_EOBID = 1;
        const int COL_EOBDTLID = 2;
        const int COL_EOBPAYMENTDTLID = 3;
        const int COL_BLTRANSACTIONID = 4;
        const int COL_BLTRANDTLID = 5;
        const int COL_BLTRANLINEID = 6;
        const int COL_DOSFROM = 7;
        const int COL_DOSTO = 8;

        const int COL_PATIENTID = 9;
        const int COL_SOURCE = 10; //Patient or Insurance Name

        const int COL_ORIGINALPAYMENT = 11;//Check Number,Date,Amount
        const int COL_PROVIDERID = 12;
        const int COL_PROVIDERNAME = 13;
        const int COL_TORESERVES = 16;//Amount for reserve
        const int COL_TYPE = 14;//Copay,Advance,Other
        const int COL_NOTE = 15;//Note

        const int COL_AVAILABLE = 17;//Available amount
        const int COL_USERESERVE = 18;//Used Reserve
        const int COL_USENOW = 19;//Current amount to use from avaiable amount

        const int COL_PAYMODE = 20;
        const int COL_REFEOBPAYID = 21;
        const int COL_REFEOBPAYDTLID = 22;
        const int COL_ACCOUNTID = 23;
        const int COL_ACCOUNTTYPE = 24;
        const int COL_MSTACCOUNTID = 25;
        const int COL_MSTACCOUNTTYPE = 26;
        const int COL_RES_EOBPAYID = 27;
        const int COL_RES_EOBPAYDTLID = 28;
        const int COL_RES_CLOSEDATE = 29;
        const int COL_COPAYRESERVEFORDOS = 30;
        const int COL_USED_RES = 31;

        const int COL_COUNT = 32;

        #endregion 

        #region " Property Procedures "

        public Int64 ClinicID
        { get { return _ClinicID; } set { _ClinicID = value; } }
        public Int64  UserID
        { get { return _UserId; } set { _UserId = value; } }
        public string UserName
        { get { return _UserName; } set { _UserName = value; } }
        private Int64 PatientID
        { get { return _patientId; } set { _patientId = value; } }

        //added by mahesh s on 10-may-2011
        private Int64 PAccountID
        { get { return _nPAccountID; } set { _nPAccountID = value; } }


        public DateTime CloseDate
        {
            get { return _closeDate; }
            set { _closeDate = value; }
        }
        public DateTime dtStartDate
        {
            get { return _dtStartDate; }
            set { _dtStartDate = value; }
        }
        public DateTime dtCloseDate
        {
            get { return _dtEndDate; }
            set { _dtEndDate = value; }
        }
        public string CloseDayTray
        {
            get { return _closeDayTray; }
            set { _closeDayTray = value; }
        }

        #endregion " Property Procedures "

        #region " Constructors "

        public frmPaymentUseCopayReserveV2(string DatabaseConnectionString,Int64 Patientid)
        {
            InitializeComponent();
            _databaseconnectionstring = DatabaseConnectionString;
            _patientId = Patientid;

            #region " Retrive Clinic ID "

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

            #endregion " Retrive Clinic ID "

            #region " Retrive UserID from appSettings "

            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                {
                    _UserId = Convert.ToInt64(appSettings["UserID"]);
                }
            }
            else
            {
                _UserId = 0;
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

            #region " Retrieve MessageBoxCaption from AppSettings "

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

            #endregion
        }

        public frmPaymentUseCopayReserveV2(string DatabaseConnectionString, Int64 Patientid, Int64 PAccountID, DateTime dtReserveforDOS, DateTime dtStartDate, DateTime dtEndDate)
        {
            InitializeComponent();
            _databaseconnectionstring = DatabaseConnectionString;
            _nPAccountID = PAccountID;
            _patientId = Patientid;
            _dtReserveforDOS = dtReserveforDOS;
            _dtStartDate = dtStartDate;
            _dtEndDate = dtEndDate;

            #region " Retrive Clinic ID "

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

            #endregion " Retrive Clinic ID "

            #region " Retrive UserID from appSettings "

            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                {
                    _UserId = Convert.ToInt64(appSettings["UserID"]);
                }
            }
            else
            {
                _UserId = 0;
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

            #region " Retrieve MessageBoxCaption from AppSettings "

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

            #endregion
            
        }

        #endregion " Constructors "

        #region " Form Load "

        private void frmPaymentUseReserve_Load(object sender, EventArgs e)
        {
            SetWindowPos(this.Handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
            FillReserves();
        }

        #endregion 

        #region " Form Controls events "

        private void frmPaymentUseReserve_FormClosed(object sender, FormClosedEventArgs e)
        {
            _isFormClosing = true;
            c1Reserve.FinishEditing();
            if (this.DialogResult != DialogResult.OK) { this.DialogResult = DialogResult.Cancel; }
        }

        private void c1Reserve_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

        #endregion

        #region "Toolstrip button events "

        private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        #endregion

        #region " Design Grid "

        private void DesignPaymentGrid(C1FlexGrid c1Payment)
        {
            try
            {
                _IsFormLoading = true;
                c1Payment.Redraw = false;
                c1Payment.AllowSorting = AllowSortingEnum.None;

                c1Payment.Clear();
                c1Payment.Cols.Count = COL_COUNT;
                c1Payment.Rows.Count = 1;
                c1Payment.Rows.Fixed = 1;
                c1Payment.Cols.Fixed = 0;

                #region " Set Headers "

                c1Payment.SetData(0, COL_EOBPAYMENTID,"EOBPaymentID");
                c1Payment.SetData(0, COL_EOBID,"EOBID");
                c1Payment.SetData(0, COL_EOBDTLID,"EOBDetailID");
                c1Payment.SetData(0, COL_EOBPAYMENTDTLID, "EOBPaymentDetailID");
                c1Payment.SetData(0, COL_BLTRANSACTIONID,"BillingTransactioID");
                c1Payment.SetData(0, COL_BLTRANDTLID, "BillingTransactioDetailID");
                c1Payment.SetData(0, COL_BLTRANLINEID, "BillingTransactioLineID");
                c1Payment.SetData(0, COL_DOSFROM, "DOSFrom");
                c1Payment.SetData(0, COL_DOSTO,"DOSTo");
                c1Payment.SetData(0, COL_PATIENTID,"PatientID");
                c1Payment.SetData(0, COL_SOURCE,"Source");//Patient or Insurance Name

                c1Payment.SetData(0, COL_ORIGINALPAYMENT,"Original Payment");//Check Number,Date,Amount
                c1Payment.SetData(0, COL_TORESERVES,"Copay");//Amount for reserve
                c1Payment.SetData(0, COL_TYPE,"Type");//Copay,Advance,Other
                c1Payment.SetData(0, COL_NOTE,"Note");//Note

                c1Payment.SetData(0, COL_AVAILABLE,"Available");//Available amount
                c1Payment.SetData(0, COL_USERESERVE, "Used");//Used Reserve
                c1Payment.SetData(0, COL_USENOW,"Use Now");//Current amount to use from avaiable amount

                c1Payment.SetData(0, COL_PAYMODE,"Payment Mode");
                c1Payment.SetData(0, COL_REFEOBPAYID,"Ref.EOBID");
                c1Payment.SetData(0, COL_REFEOBPAYDTLID,"Ref.EOBDetailID");
                c1Payment.SetData(0, COL_ACCOUNTID,"AccountID");
                c1Payment.SetData(0, COL_ACCOUNTTYPE,"Account Type");
                c1Payment.SetData(0, COL_MSTACCOUNTID,"Mst.AccountID");
                c1Payment.SetData(0, COL_MSTACCOUNTTYPE, "Mst.AccountType");
                c1Payment.SetData(0, COL_RES_EOBPAYID,"ReserveRefPayID");
                c1Payment.SetData(0, COL_RES_CLOSEDATE, "CloseDate");

                
                c1Payment.SetData(0, COL_PROVIDERID, "ProviderID");
                c1Payment.SetData(0, COL_PROVIDERNAME, "Provider");

                c1Payment.SetData(0, COL_COPAYRESERVEFORDOS, "Reserve for DOS");
                #endregion

                #region " Show/Hide "

                c1Payment.Cols[COL_ORIGINALPAYMENT].Visible = true;
                c1Payment.Cols[COL_PROVIDERNAME].Visible = true;
                c1Payment.Cols[COL_TORESERVES].Visible = true;
                c1Payment.Cols[COL_TYPE].Visible = true;
                c1Payment.Cols[COL_NOTE].Visible = true;
                c1Payment.Cols[COL_AVAILABLE].Visible = true;
                c1Payment.Cols[COL_COPAYRESERVEFORDOS].Visible = true;
                c1Payment.Cols[COL_USED_RES].Visible = true;

                c1Payment.Cols[COL_EOBPAYMENTID].Visible = false;// 0;
                c1Payment.Cols[COL_EOBID].Visible = false;// 0;
                c1Payment.Cols[COL_EOBDTLID].Visible = false;// 0;
                c1Payment.Cols[COL_EOBPAYMENTDTLID].Visible = false;// 0;
                c1Payment.Cols[COL_BLTRANSACTIONID].Visible = false;// 0;
                c1Payment.Cols[COL_BLTRANDTLID].Visible = false;// 0;
                c1Payment.Cols[COL_BLTRANLINEID].Visible = false;// 0;
                c1Payment.Cols[COL_DOSFROM].Visible = false;// 50;
                c1Payment.Cols[COL_DOSTO].Visible = false;// 0;
                c1Payment.Cols[COL_PATIENTID].Visible = false;// 0;
                c1Payment.Cols[COL_PAYMODE].Visible = false;// 100;
                c1Payment.Cols[COL_REFEOBPAYID].Visible = false;// 0;
                c1Payment.Cols[COL_REFEOBPAYDTLID].Visible = false;// 0;
                c1Payment.Cols[COL_ACCOUNTID].Visible = false;// 0;
                c1Payment.Cols[COL_ACCOUNTTYPE].Visible = false;// 0;
                c1Payment.Cols[COL_MSTACCOUNTID].Visible = false;// 0;
                c1Payment.Cols[COL_MSTACCOUNTTYPE].Visible = false;// 0;
                c1Payment.Cols[COL_USERESERVE].Visible = false;
                c1Payment.Cols[COL_RES_EOBPAYID].Visible = false;
                c1Payment.Cols[COL_RES_EOBPAYDTLID].Visible = false;
                c1Payment.Cols[COL_RES_CLOSEDATE].Visible = false;
                c1Payment.Cols[COL_PROVIDERID].Visible = false;
                c1Payment.Cols[COL_SOURCE].Visible = false;
                c1Payment.Cols[COL_USENOW].Visible = false;
                
                #endregion

                #region " Width "

                int Width = c1Payment.Width;

                c1Payment.Cols[COL_ORIGINALPAYMENT].Width = Convert.ToInt32(Width * 0.22);
                c1Payment.Cols[COL_PROVIDERNAME].Width = Convert.ToInt32(Width * 0.12);
                c1Payment.Cols[COL_TYPE].Width = Convert.ToInt32(Width * 0.05);
                c1Payment.Cols[COL_NOTE].Width = Convert.ToInt32(Width * 0.20);
                c1Payment.Cols[COL_TORESERVES].Width = Convert.ToInt32(Width * 0.10);
                c1Payment.Cols[COL_AVAILABLE].Width = Convert.ToInt32(Width * 0.10);
                c1Payment.Cols[COL_COPAYRESERVEFORDOS].Width = Convert.ToInt32(Width * 0.14);
                c1Payment.Cols[COL_USED_RES].Width = Convert.ToInt32(Width * 0.04);

                #endregion

                #region " Data Type "

                c1Payment.Cols[COL_EOBPAYMENTID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_EOBID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_EOBDTLID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_EOBPAYMENTDTLID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_BLTRANSACTIONID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_BLTRANDTLID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_BLTRANLINEID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_DOSFROM].DataType = typeof(System.String);
                c1Payment.Cols[COL_DOSTO].DataType = typeof(System.String);
                c1Payment.Cols[COL_PATIENTID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_SOURCE].DataType = typeof(System.String);
                c1Payment.Cols[COL_ORIGINALPAYMENT].DataType = typeof(System.String);
                c1Payment.Cols[COL_TORESERVES].DataType = typeof(System.Decimal);
                c1Payment.Cols[COL_TYPE].DataType = typeof(System.String);
                c1Payment.Cols[COL_NOTE].DataType = typeof(System.String);
                c1Payment.Cols[COL_AVAILABLE].DataType = typeof(System.Decimal);
                c1Payment.Cols[COL_USENOW].DataType = typeof(System.Decimal);
                c1Payment.Cols[COL_USERESERVE].DataType = typeof(System.Decimal);
                c1Payment.Cols[COL_PAYMODE].DataType = typeof(System.Int32);
                c1Payment.Cols[COL_REFEOBPAYID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_REFEOBPAYDTLID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_ACCOUNTID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_ACCOUNTTYPE].DataType = typeof(System.Int32);
                c1Payment.Cols[COL_MSTACCOUNTID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_MSTACCOUNTTYPE].DataType = typeof(System.Int32);
                c1Payment.Cols[COL_RES_EOBPAYID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_RES_EOBPAYDTLID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_RES_CLOSEDATE].DataType = typeof(System.String);


                c1Payment.Cols[COL_PROVIDERID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_PROVIDERNAME].DataType = typeof(System.String);
              //  c1Payment.Cols[COL_COPAYRESERVEFORDOS].DataType = typeof(System.DateTime);
                c1Payment.Cols[COL_COPAYRESERVEFORDOS].Format = "MM/dd/yyyy";
                #endregion

                #region " Alignment "

                c1Payment.Cols[COL_EOBPAYMENTID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_EOBID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_EOBDTLID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_EOBPAYMENTDTLID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_BLTRANSACTIONID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_BLTRANDTLID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_BLTRANLINEID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_DOSFROM].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_DOSTO].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_PATIENTID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_ORIGINALPAYMENT].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_TORESERVES].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_TYPE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_NOTE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_AVAILABLE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_USENOW].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_USERESERVE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_PAYMODE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_REFEOBPAYID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_REFEOBPAYDTLID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_ACCOUNTID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_ACCOUNTTYPE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_MSTACCOUNTID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_MSTACCOUNTTYPE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_RES_EOBPAYID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_RES_EOBPAYDTLID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_RES_CLOSEDATE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;

                c1Payment.Cols[COL_PROVIDERID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_PROVIDERNAME].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_COPAYRESERVEFORDOS].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                #endregion

                #region "Set Yellow / Green Style"

                c1Payment.Cols[COL_USED_RES].DataType = typeof(Image);
                c1Payment.Cols[COL_USED_RES].ImageMap = new System.Collections.Hashtable();                
                c1Payment.Cols[COL_USED_RES].ImageAndText = false;
                c1Payment.Cols[COL_USED_RES].AllowResizing = false;

                #endregion

                #region " Set Styles "

                C1.Win.C1FlexGrid.CellStyle csCurrencyStyle;// = c1Payment.Styles.Add("cs_CurrencyStyle");
                try
                {
                    if (c1Payment.Styles.Contains("cs_CurrencyStyle"))
                    {
                        csCurrencyStyle = c1Payment.Styles["cs_CurrencyStyle"];
                    }
                    else
                    {
                        csCurrencyStyle = c1Payment.Styles.Add("cs_CurrencyStyle");
                        csCurrencyStyle.DataType = typeof(System.Decimal);
                        csCurrencyStyle.Format = "c";
                        csCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;
                    }

                }
                catch
                {
                    csCurrencyStyle = c1Payment.Styles.Add("cs_CurrencyStyle");
                    csCurrencyStyle.DataType = typeof(System.Decimal);
                    csCurrencyStyle.Format = "c";
                    csCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;

                }
 
 

                C1.Win.C1FlexGrid.CellStyle csEditableCurrencyStyle;// = c1Payment.Styles.Add("cs_EditableCurrencyStyle");
                try
                {
                    if (c1Payment.Styles.Contains("cs_EditableCurrencyStyle"))
                    {
                        csEditableCurrencyStyle = c1Payment.Styles["cs_EditableCurrencyStyle"];
                    }
                    else
                    {
                        csEditableCurrencyStyle = c1Payment.Styles.Add("cs_EditableCurrencyStyle");
                        csEditableCurrencyStyle.DataType = typeof(System.Decimal);
                        csEditableCurrencyStyle.Format = "c";
                        csEditableCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csEditableCurrencyStyle.BackColor = Color.White;
                    }

                }
                catch
                {
                    csEditableCurrencyStyle = c1Payment.Styles.Add("cs_EditableCurrencyStyle");
                    csEditableCurrencyStyle.DataType = typeof(System.Decimal);
                    csEditableCurrencyStyle.Format = "c";
                    csEditableCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csEditableCurrencyStyle.BackColor = Color.White;

                }
    

                C1.Win.C1FlexGrid.CellStyle csEditableStringStyle;// = c1Payment.Styles.Add("cs_EditableStringStyle");
                try
                {
                    if (c1Payment.Styles.Contains("cs_EditableStringStyle"))
                    {
                        csEditableStringStyle = c1Payment.Styles["cs_EditableStringStyle"];
                    }
                    else
                    {
                        csEditableStringStyle = c1Payment.Styles.Add("cs_EditableStringStyle");
                        csEditableStringStyle.DataType = typeof(System.String);
                        csEditableStringStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csEditableStringStyle.BackColor = Color.White;
                    }

                }
                catch
                {
                    csEditableStringStyle = c1Payment.Styles.Add("cs_EditableStringStyle");
                    csEditableStringStyle.DataType = typeof(System.String);
                    csEditableStringStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csEditableStringStyle.BackColor = Color.White;

                }
             

                C1.Win.C1FlexGrid.CellStyle csEditableDateStyle;// = c1Payment.Styles.Add("cs_EditableDateStyle");
                try
                {
                    if (c1Payment.Styles.Contains("cs_EditableDateStyle"))
                    {
                        csEditableDateStyle = c1Payment.Styles["cs_EditableDateStyle"];
                    }
                    else
                    {
                        csEditableDateStyle = c1Payment.Styles.Add("cs_EditableDateStyle");
                        csEditableDateStyle.DataType = typeof(System.DateTime);
                        csEditableDateStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csEditableDateStyle.BackColor = Color.White;
                    }

                }
                catch
                {
                    csEditableDateStyle = c1Payment.Styles.Add("cs_EditableDateStyle");
                    csEditableDateStyle.DataType = typeof(System.DateTime);
                    csEditableDateStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csEditableDateStyle.BackColor = Color.White;

                }
              

                C1.Win.C1FlexGrid.CellStyle csClaimRowStyle;// = c1Payment.Styles.Add("cs_ClaimRowStyle");
                try
                {
                    if (c1Payment.Styles.Contains("cs_ClaimRowStyle"))
                    {
                        csClaimRowStyle = c1Payment.Styles["cs_ClaimRowStyle"];
                    }
                    else
                    {
                        csClaimRowStyle = c1Payment.Styles.Add("cs_ClaimRowStyle");
                        csClaimRowStyle.DataType = typeof(System.String);
                        csClaimRowStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csClaimRowStyle.BackColor = Color.FromArgb(229, 224, 236);

                    }

                }
                catch
                {
                    csClaimRowStyle = c1Payment.Styles.Add("cs_ClaimRowStyle");
                    csClaimRowStyle.DataType = typeof(System.String);
                    csClaimRowStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;// new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csClaimRowStyle.BackColor = Color.FromArgb(229, 224, 236);


                }
   

                C1.Win.C1FlexGrid.CellStyle csPatientRowStyle;// = c1Payment.Styles.Add("cs_PatientRowStyle");
                try
                {
                    if (c1Payment.Styles.Contains("cs_PatientRowStyle"))
                    {
                        csPatientRowStyle = c1Payment.Styles["cs_PatientRowStyle"];
                    }
                    else
                    {
                        csPatientRowStyle = c1Payment.Styles.Add("cs_PatientRowStyle");
                        csPatientRowStyle.DataType = typeof(System.String);
                        csPatientRowStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csPatientRowStyle.BackColor = Color.FromArgb(215, 228, 188);

                    }

                }
                catch
                {
                    csPatientRowStyle = c1Payment.Styles.Add("cs_PatientRowStyle");
                    csPatientRowStyle.DataType = typeof(System.String);
                    csPatientRowStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csPatientRowStyle.BackColor = Color.FromArgb(215, 228, 188);


                }
  

                c1Payment.Cols[COL_TORESERVES].Style = csCurrencyStyle;
                c1Payment.Cols[COL_AVAILABLE].Style = csCurrencyStyle;
                c1Payment.Cols[COL_USENOW].Style = csCurrencyStyle;
                c1Payment.Cols[COL_USERESERVE].Style = csCurrencyStyle;


                #region "Yellow / Green Style"

                try
                {
                    if (c1Payment.Styles.Contains("cs_YellowStyle"))
                    {
                        csYellowStyle = c1Payment.Styles["cs_YellowStyle"];
                    }
                    else
                    {
                        csYellowStyle = c1Payment.Styles.Add("cs_YellowStyle");
                        csYellowStyle.BackColor = System.Drawing.Color.FromArgb(255, 255, 0);
                        csYellowStyle.ForeColor = System.Drawing.Color.Black;
                    }

                }
                catch
                {
                    csYellowStyle = c1Payment.Styles.Add("cs_YellowStyle");
                    csYellowStyle.BackColor = System.Drawing.Color.FromArgb(255, 255, 0);
                    csYellowStyle.ForeColor = System.Drawing.Color.Black;
                }

                try
                {
                    if (c1Payment.Styles.Contains("cs_GreenStyle"))
                    {
                        csGreenStyle = c1Payment.Styles["cs_GreenStyle"];
                    }
                    else
                    {
                        csGreenStyle = c1Payment.Styles.Add("cs_GreenStyle");
                        csGreenStyle.BackColor = System.Drawing.Color.FromArgb(0, 255, 0);
                        csGreenStyle.ForeColor = System.Drawing.Color.Black;
                    }

                }
                catch
                {
                    csGreenStyle = c1Payment.Styles.Add("cs_GreenStyle");
                    csGreenStyle.BackColor = System.Drawing.Color.FromArgb(0, 255, 0);
                    csGreenStyle.ForeColor = System.Drawing.Color.Black;

                }

                #endregion

                #endregion

                c1Payment.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
                c1Payment.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns;

                #region " Allow Editing "

                c1Payment.AllowEditing = false;

                c1Payment.Cols[COL_EOBPAYMENTID].AllowEditing = false;
                c1Payment.Cols[COL_EOBID].AllowEditing = false;//0;
                c1Payment.Cols[COL_EOBDTLID].AllowEditing = false;//0;
                c1Payment.Cols[COL_EOBPAYMENTDTLID].AllowEditing = false;//0;
                c1Payment.Cols[COL_BLTRANSACTIONID].AllowEditing = false;//0;
                c1Payment.Cols[COL_BLTRANDTLID].AllowEditing = false;//0;
                c1Payment.Cols[COL_BLTRANLINEID].AllowEditing = false;//0;
                c1Payment.Cols[COL_DOSFROM].AllowEditing = false;//50;
                c1Payment.Cols[COL_DOSTO].AllowEditing = false;//0;
                c1Payment.Cols[COL_PATIENTID].AllowEditing = false;//0;
                c1Payment.Cols[COL_SOURCE].AllowEditing = false;//100;
                c1Payment.Cols[COL_ORIGINALPAYMENT].AllowEditing = false;//100;
                c1Payment.Cols[COL_TORESERVES].AllowEditing = false;//100;
                c1Payment.Cols[COL_TYPE].AllowEditing = false;//100;
                c1Payment.Cols[COL_NOTE].AllowEditing = false;//100;
                c1Payment.Cols[COL_AVAILABLE].AllowEditing = false;//100;
                c1Payment.Cols[COL_USENOW].AllowEditing = true;//100;
                c1Payment.Cols[COL_USERESERVE].AllowEditing = false;//100;
                c1Payment.Cols[COL_PAYMODE].AllowEditing = false;//100;
                c1Payment.Cols[COL_REFEOBPAYID].AllowEditing = false;//0;
                c1Payment.Cols[COL_REFEOBPAYDTLID].AllowEditing = false;//0;
                c1Payment.Cols[COL_ACCOUNTID].AllowEditing = false;//0;
                c1Payment.Cols[COL_ACCOUNTTYPE].AllowEditing = false;//0;
                c1Payment.Cols[COL_MSTACCOUNTID].AllowEditing = false;//0;
                c1Payment.Cols[COL_MSTACCOUNTTYPE].AllowEditing = false;//0;
                c1Payment.Cols[COL_RES_EOBPAYID].AllowEditing = false;//0;
                c1Payment.Cols[COL_RES_EOBPAYDTLID].AllowEditing = false;//0;
                c1Payment.Cols[COL_PROVIDERID].AllowEditing = false;
                c1Payment.Cols[COL_PROVIDERNAME].AllowEditing = false;
                c1Payment.Cols[COL_COPAYRESERVEFORDOS].AllowEditing = false;
                c1Payment.Cols[COL_USED_RES].AllowEditing = false;

                #endregion

                c1Payment.VisualStyle = VisualStyle.Office2007Blue;
                c1Payment.Styles[CellStyleEnum.SelectedColumnHeader].ForeColor = Color.FromArgb(31, 73, 125);
                c1Payment.Styles[CellStyleEnum.Fixed].ForeColor = Color.FromArgb(31, 73, 125);
                c1Payment.Styles[CellStyleEnum.Alternate].ForeColor = Color.FromArgb(31, 73, 125);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null; 
            }
            finally
            { _IsFormLoading = false; c1Payment.Redraw = true; }
        }

        #endregion " Design Grid "

        #region " Private & Public Methods "

        private void FillReserves()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable _dtReserves = new DataTable();

            try
            {

                DesignPaymentGrid(c1Reserve);

                _IsFormLoading = true;
                oParameters.Add("@nPatientID", _patientId, ParameterDirection.Input, SqlDbType.BigInt);// NUMERIC(18,0),
                oParameters.Add("@nPAccountID", _nPAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                if (_dtReserveforDOS == DateTime.MinValue)
                {
                    oParameters.Add("@dtreservefordos", DBNull.Value, ParameterDirection.Input, SqlDbType.Date);
                }
                else
                {
                    oParameters.Add("@dtreservefordos", _dtReserveforDOS, ParameterDirection.Input, SqlDbType.Date);
                }
                oParameters.Add("@dtStartDate", _dtStartDate, ParameterDirection.Input, SqlDbType.Date);
                oParameters.Add("@dtEndDate", _dtEndDate, ParameterDirection.Input, SqlDbType.Date);
                oDB.Connect(false);
                oDB.Retrive("BL_PatientAvailableCopayReserve", oParameters, out _dtReserves);
                oDB.Disconnect();

                if (_dtReserves != null && _dtReserves.Rows.Count > 0)
                {
                    int _rowIndex = 0;

                    for (int i = 0; i < _dtReserves.Rows.Count; i++)
                    {
                        #region " Set Data "

                        _rowIndex = c1Reserve.Rows.Add().Index;

                        c1Reserve.SetData(_rowIndex, COL_EOBPAYMENTID, Convert.ToInt64(_dtReserves.Rows[i]["nEOBPaymentID"]));
                        c1Reserve.SetData(_rowIndex, COL_EOBPAYMENTDTLID, Convert.ToInt64(_dtReserves.Rows[i]["nEOBPaymentDetailID"]));
                        c1Reserve.SetData(_rowIndex, COL_PATIENTID, Convert.ToString(_dtReserves.Rows[i]["nPatientID"]));
                        c1Reserve.SetData(_rowIndex, COL_SOURCE, Convert.ToString(_dtReserves.Rows[i]["PatientName"]));//Patient or Insurance Name

                        string _originalPayment = "";
                        _originalPayment = ((PaymentModeV2)Convert.ToInt32(_dtReserves.Rows[i]["nPayMode"])).ToString() + "# " + Convert.ToString(_dtReserves.Rows[i]["CheckNumber"]) + " " + Convert.ToString(_dtReserves.Rows[i]["nCheckDate"]) + " $ " + Convert.ToDecimal(_dtReserves.Rows[i]["nCheckAmount"]);
                        c1Reserve.SetData(_rowIndex, COL_ORIGINALPAYMENT, _originalPayment);//Check Number,Date,Amount

                        c1Reserve.SetData(_rowIndex, COL_TORESERVES, Convert.ToDecimal(_dtReserves.Rows[i]["nAmount"]));
                        c1Reserve.SetData(_rowIndex, COL_TYPE, ((NoteSubTypeV2)Convert.ToInt32(_dtReserves.Rows[i]["nPaymentNoteSubType"])).ToString());//Copay,Advance,Other
                        c1Reserve.SetData(_rowIndex, COL_NOTE, Convert.ToString(_dtReserves.Rows[i]["sNoteDescription"]));//Note
                        c1Reserve.SetData(_rowIndex, COL_USERESERVE, Convert.ToDecimal(_dtReserves.Rows[i]["UsedReserve"]));//Used amount
                        c1Reserve.SetData(_rowIndex, COL_AVAILABLE, Convert.ToDecimal(_dtReserves.Rows[i]["AvailableReserve"]));//Available amount
                        c1Reserve.SetData(_rowIndex, COL_USENOW, 0);//Current amount to use from avaiable amount
                        c1Reserve.SetData(_rowIndex, COL_REFEOBPAYID, Convert.ToInt64(_dtReserves.Rows[i]["nRefEOBPaymentID"]));
                        c1Reserve.SetData(_rowIndex, COL_ACCOUNTID, Convert.ToInt64(_dtReserves.Rows[i]["nAccountID"]));
                        c1Reserve.SetData(_rowIndex, COL_RES_EOBPAYID, Convert.ToInt64(_dtReserves.Rows[i]["nReserveID"]));
                        c1Reserve.SetData(_rowIndex, COL_RES_CLOSEDATE, Convert.ToString(_dtReserves.Rows[i]["dtCloseDate"]));
                        c1Reserve.SetData(_rowIndex, COL_PROVIDERNAME, Convert.ToString(_dtReserves.Rows[i]["AssociationProvider"]));
                        c1Reserve.SetData(_rowIndex, COL_PROVIDERID, Convert.ToString(_dtReserves.Rows[i]["AssociationProviderID"].ToString()));
                        c1Reserve.SetData(_rowIndex, COL_COPAYRESERVEFORDOS, Convert.ToString(_dtReserves.Rows[i]["dtReserveForDOS"]));

                        #region " Set Styles "

                        if (Convert.ToDecimal(_dtReserves.Rows[i]["AvailableReserve"]) == 0)
                        {   
                            c1Reserve.SetCellImage(_rowIndex, COL_USED_RES, global::gloBilling.Properties.Resources.StatusNormal);
                        }
                        else if (Convert.ToDecimal(_dtReserves.Rows[i]["AvailableReserve"]) > 0)
                        {
                            c1Reserve.SetCellImage(_rowIndex, COL_USED_RES, global::gloBilling.Properties.Resources.HoldClaim);
                        }

                        #endregion " Set Styles "

                        #endregion
                    }
                }
            }

            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false); }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_dtReserves != null) { _dtReserves.Dispose(); }
                _IsFormLoading = false;
            }
        }

        #endregion " Private & Public Methods "
    }
}