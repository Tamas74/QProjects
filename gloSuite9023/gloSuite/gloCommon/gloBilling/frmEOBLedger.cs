using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using gloBilling.Common;

namespace gloBilling
{
    public partial class frmEOBLedger : Form
    {

        #region " Variable Declarations"

        private string _databaseconnectionstring = "";
        public string _MessageBoxCaption = String.Empty;
        private Int64 _ClinicID = 0;
        private Int64 _PatientID = 0;
        private Int64 _userid = 0;
        private string _username = "";
        
        DataView dvClaims = new DataView();
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

      
        //private bool blnDisposed;
        //private static frmBillingPatientLedger frm;

        CellStyle csSubTotalRow;
        CellStyle csGrandTotalRow;
        CellStyle csGrandTotalTitleRow;
        CellStyle csInsuraceCheckRow;
        CellStyle csCurrency;
        CellStyle csClaimHeader;
        CellRange CrDollars;
        CellRange subPaymentsource;
        CellStyle csPaymentSource;
        CellStyle csAdvancePayment;
        CellStyle csBalanceContents;

        string InsuranceName = "";
        string PaymentTray = "";
        string PaymentDate = "";
        decimal d_Charges = 0;
        decimal d_ContAdj = 0;
        decimal d_InsurancePayment = 0;
        decimal d_Withhold = 0;
        decimal d_Copay = 0;
        decimal d_CoIns = 0;
        decimal d_Deductible = 0;

        Decimal _30DaysAdvancePaid = 0;
        Decimal _60DaysAdvancePaid = 0;
        Decimal _90DaysAdvancePaid = 0;
        Decimal _120DaysAdvancePaid = 0;
        Decimal _Above120DaysAdvancePaid = 0;


        decimal dTotal_Charges = 0;
      //  decimal dTotal_Allowed = 0;
      
        decimal dTotal_InsurancePayment = 0;
        decimal dTotal_ContAdj = 0;
        decimal dTotal_WithHold = 0;
        decimal dTotal_Copay = 0;
        decimal dTotal_CoIns = 0;
        decimal dTotal_Deductible = 0;
       // decimal dTotal_PatientPayment = 0;



        private string _TagClaims = "Claims";
        private string _TagDetails = "Details";

        #endregion

        
        #region "Columns"


        const int COL_1 = 0;
        const int COL_2 = 1;
        const int COL_3 = 2;
        const int COL_4 = 3;
        const int COL_5 = 4;
        const int COL_6 = 5;
        const int COL_7 = 6;
        const int COL_8 = 7;
        const int COL_9 = 8;
        const int COL_10 = 9;
        const int COL_11 = 10;
        const int COL_12 = 11;
        const int COL_13 = 12;
        const int COL_14 = 13;

        const int COL_COUNT = 14;
        private int ROW_COUNT = 1;

        #endregion


        #region "Patient Details Columns"

        const int COLPAT_1 = 0;
        const int COLPAT_2 = 1;
        const int COLPAT_3 = 2;
        
        const int COLPAT_COUNT = 3;
        private int ROWPAT_COUNT = 1;

        #endregion

        
        #region " Constructor "

        public frmEOBLedger(Int64 PatientID, string DatabaseConnectionString)
        {
            InitializeComponent();

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

            #region " Retrive UserID from appSettings "

            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                {
                    _userid = Convert.ToInt64(appSettings["UserID"]);
                }
            }
            else
            {
                _userid = 0;
            }

            #endregion

            #region " Retrive UserName from appSettings "

            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                {
                    _username = Convert.ToString(appSettings["UserName"]);
                }
            }
            else
            {
                _username = "";
            }

            #endregion

            #region " Retrive Database Connection String for appSettings "

            if (appSettings["DataBaseConnectionString"] != null)
            {
                if (appSettings["DataBaseConnectionString"] != "")
                {
                    _databaseconnectionstring = Convert.ToString(appSettings["DataBaseConnectionString"]);
                }
                else
                {
                    _databaseconnectionstring = "";
                }
            }
            else
            {
                _databaseconnectionstring = "";
            }

            #endregion

            _databaseconnectionstring =DatabaseConnectionString ;
            _PatientID = PatientID;

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

        #endregion " Constructor "


        #region " Design Grid "


        private void DesignPatientDetialsGrid()
        {
            C1PatientDetails.Clear();
            C1PatientDetails.Rows.Count = ROWPAT_COUNT;
            C1PatientDetails.Rows.Fixed = 1;
            C1PatientDetails.Cols.Count = COLPAT_COUNT;
            C1PatientDetails.Cols.Fixed = 0;
            C1PatientDetails.AllowEditing = false;

            #region "Data Type"

            C1PatientDetails.Cols[COLPAT_1].DataType = typeof(System.String);
            C1PatientDetails.Cols[COLPAT_2].DataType = typeof(System.Decimal);
            C1PatientDetails.Cols[COLPAT_3].DataType = typeof(System.String);
            

            #endregion

            #region "Width"

            int nWidth = pnlPatDetails.Width - 2;
            C1PatientDetails.Cols[COLPAT_1].Width = Convert.ToInt32((nWidth * 0.5));
            C1PatientDetails.Cols[COLPAT_2].Width = Convert.ToInt32((nWidth * 0.5));
            C1PatientDetails.Cols[COLPAT_3].Width = 0;

            #endregion

            #region "Header"

            //C1PatientDetails.SetData(0, COLPAT_1, "Current");
            C1PatientDetails.SetData(0, COLPAT_1, "");
            C1PatientDetails.SetData(0, COLPAT_2, "");
            
            
            #endregion

            #region "Show Hide"

            C1PatientDetails.Cols[COLPAT_1].Visible = true;
            C1PatientDetails.Cols[COLPAT_2].Visible = true;
            C1PatientDetails.Cols[COLPAT_3].Visible = false;

            #endregion

            C1PatientDetails.AllowSorting = AllowSortingEnum.None;


        }

        private void DesignChargesGrid(C1FlexGrid c1Grid)
        {
            
            c1Grid.Clear();
            c1Grid.Rows.Count = ROW_COUNT;
            c1Grid.Rows.Fixed = 1;
            c1Grid.Cols.Count = COL_COUNT;
            c1Grid.Cols.Fixed = 0;

            //c1Grid.AllowSorting = AllowSortingEnum.None;
            c1Grid.AllowEditing = false;

            #region "Data Type"

            c1Grid.Cols[COL_1].DataType = typeof(System.String);

            c1Grid.Cols[COL_2].DataType = typeof(System.String);
            c1Grid.Cols[COL_3].DataType = typeof(System.String);
            c1Grid.Cols[COL_4].DataType = typeof(System.String);
            c1Grid.Cols[COL_5].DataType = typeof(System.String);
            c1Grid.Cols[COL_6].DataType = typeof(System.Decimal);
            c1Grid.Cols[COL_7].DataType = typeof(System.String);
            c1Grid.Cols[COL_8].DataType = typeof(System.String);
            c1Grid.Cols[COL_9].DataType = typeof(System.String);
            c1Grid.Cols[COL_10].DataType = typeof(System.String);
            c1Grid.Cols[COL_11].DataType = typeof(System.String);
            c1Grid.Cols[COL_12].DataType = typeof(System.String);
            c1Grid.Cols[COL_13].DataType = typeof(System.String);
            c1Grid.Cols[COL_14].DataType = typeof(System.String);
            #endregion


            #region "Width"

           

            c1Grid.Cols[COL_2].Width = 140;
            if (c1Grid.Name == "c1ClaimGrid")
            {
                c1Grid.Cols[COL_1].Width = 60;
                c1Grid.Cols[COL_3].Width = 100;
                c1Grid.Cols[COL_4].Width = 80;
                c1Grid.Cols[COL_5].Width = 100;
                c1Grid.Cols[COL_8].Width = 150;
                c1Grid.Cols[COL_10].Width = 120;
            }
            else
            {
                c1Grid.Cols[COL_1].Width = 120;
                c1Grid.Cols[COL_3].Width = 110;
                c1Grid.Cols[COL_4].Width = 100;
                c1Grid.Cols[COL_5].Width = 120;
                c1Grid.Cols[COL_8].Width = 150;
                c1Grid.Cols[COL_10].Width = 140;
            }
          
           
            c1Grid.Cols[COL_6].Width = 120;
            c1Grid.Cols[COL_7].Width = 100;
            c1Grid.Cols[COL_9].Width = 130;
            c1Grid.Cols[COL_11].Width = 0;
            c1Grid.Cols[COL_12].Width = 0;
            c1Grid.Cols[COL_14].Width = 0;

            #endregion


            #region "Show Hide"

            c1Grid.Cols[COL_1].Visible = true;
            c1Grid.Cols[COL_2].Visible = true;
            c1Grid.Cols[COL_3].Visible = true;
            c1Grid.Cols[COL_4].Visible = true;
            c1Grid.Cols[COL_5].Visible = true;
            c1Grid.Cols[COL_6].Visible = true;
            c1Grid.Cols[COL_7].Visible = true;
            c1Grid.Cols[COL_8].Visible = true;
            c1Grid.Cols[COL_9].Visible = true;
            c1Grid.Cols[COL_10].Visible = true;
            c1Grid.Cols[COL_11].Visible = false;
            c1Grid.Cols[COL_12].Visible = false;
            c1Grid.Cols[COL_13].Visible = true;
            c1Grid.Cols[COL_14].Visible = false;

            #endregion


            #region "Header"

            c1Grid.SetData(0, COL_1, "Claim # ");
            c1Grid.SetData(0, COL_2, "DOS");
            c1Grid.SetData(0, COL_3, "CPT");
            c1Grid.SetData(0, COL_4, "M1");
            c1Grid.SetData(0, COL_5, "M2");
            c1Grid.SetData(0, COL_6, "Units");
            c1Grid.SetData(0, COL_7, "Charge");
            c1Grid.SetData(0, COL_8, "Provider");
            c1Grid.SetData(0, COL_9, "ChargeTray");
            c1Grid.SetData(0, COL_10, "Status");
            c1Grid.SetData(0, COL_13, "Resp.");
            c1Grid.SetData(0, COL_14, "TransDtlId");
            #endregion


            #region "Styles"

          //  csCurrency = c1Grid.Styles.Add("csCurrencyCell");
            try
            {
                if (c1Grid.Styles.Contains("csCurrencyCell"))
                {
                    csCurrency = c1Grid.Styles["csCurrencyCell"];
                }
                else
                {
                    csCurrency = c1Grid.Styles.Add("csCurrencyCell");
                    csCurrency.DataType = typeof(System.Decimal);
                    csCurrency.Format = "C";
                    csCurrency.ForeColor = Color.Navy;
                    csCurrency.Font = new System.Drawing.Font("Tahoma", 8.62F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    csCurrency.TextAlign = TextAlignEnum.RightCenter;
                }

            }
            catch
            {
                csCurrency = c1Grid.Styles.Add("csCurrencyCell");
                csCurrency.DataType = typeof(System.Decimal);
                csCurrency.Format = "C";
                csCurrency.ForeColor = Color.Navy;
                csCurrency.Font = new System.Drawing.Font("Tahoma", 8.62F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                csCurrency.TextAlign = TextAlignEnum.RightCenter;
            }
 


     //       csSubTotalRow = c1Grid.Styles.Add("SubTotalRow");
            try
            {
                if (c1Grid.Styles.Contains("SubTotalRow"))
                {
                    csSubTotalRow = c1Grid.Styles["SubTotalRow"];
                }
                else
                {
                    csSubTotalRow = c1Grid.Styles.Add("SubTotalRow");
                    csSubTotalRow.DataType = typeof(System.Decimal);
                    csSubTotalRow.Format = "c";
                    csSubTotalRow.BackColor = Color.FromArgb(255, 255, 255);  //FromArgb(168,192,242);
                    csSubTotalRow.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    csSubTotalRow.TextEffect = TextEffectEnum.Flat;
                    csSubTotalRow.ForeColor = Color.DarkBlue;
                    csSubTotalRow.TextAlign = TextAlignEnum.RightCenter;
                }

            }
            catch
            {
                csSubTotalRow = c1Grid.Styles.Add("SubTotalRow");
                csSubTotalRow.DataType = typeof(System.Decimal);
                csSubTotalRow.Format = "c";
                csSubTotalRow.BackColor = Color.FromArgb(255, 255, 255);  //FromArgb(168,192,242);
                csSubTotalRow.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                csSubTotalRow.TextEffect = TextEffectEnum.Flat;
                csSubTotalRow.ForeColor = Color.DarkBlue;
                csSubTotalRow.TextAlign = TextAlignEnum.RightCenter;
            }
 
      

           // csInsuraceCheckRow = c1Grid.Styles.Add("InsuraceCheckRow");
            try
            {
                if (c1Grid.Styles.Contains("InsuraceCheckRow"))
                {
                    csInsuraceCheckRow = c1Grid.Styles["InsuraceCheckRow"];
                }
                else
                {
                    csInsuraceCheckRow = c1Grid.Styles.Add("InsuraceCheckRow");
                    csInsuraceCheckRow.BackColor = Color.FromArgb(255, 255, 255);  //FromArgb(168,192,242);
                    csInsuraceCheckRow.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    csInsuraceCheckRow.TextEffect = TextEffectEnum.Flat;
                    csInsuraceCheckRow.ForeColor = Color.DarkBlue;
                    csInsuraceCheckRow.TextAlign = TextAlignEnum.LeftCenter;
                }

            }
            catch
            {
                csInsuraceCheckRow = c1Grid.Styles.Add("InsuraceCheckRow");
                csInsuraceCheckRow.BackColor = Color.FromArgb(255, 255, 255);  //FromArgb(168,192,242);
                csInsuraceCheckRow.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                csInsuraceCheckRow.TextEffect = TextEffectEnum.Flat;
                csInsuraceCheckRow.ForeColor = Color.DarkBlue;
                csInsuraceCheckRow.TextAlign = TextAlignEnum.LeftCenter;
            }
 
   

           // csGrandTotalRow = c1Grid.Styles.Add("GrandTotalRow");
            try
            {
                if (c1Grid.Styles.Contains("GrandTotalRow"))
                {
                    csGrandTotalRow = c1Grid.Styles["GrandTotalRow"];
                }
                else
                {
                    csGrandTotalRow = c1Grid.Styles.Add("GrandTotalRow");
                    csGrandTotalRow.BackColor = Color.FromArgb(255, 255, 255);  //FromArgb(168,192,242);
                    csGrandTotalRow.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    csGrandTotalRow.TextEffect = TextEffectEnum.Flat;
                    csGrandTotalRow.ForeColor = Color.Maroon;
                    csGrandTotalRow.TextAlign = TextAlignEnum.RightCenter;
                }

            }
            catch
            {
                csGrandTotalRow = c1Grid.Styles.Add("GrandTotalRow");
                csGrandTotalRow.BackColor = Color.FromArgb(255, 255, 255);  //FromArgb(168,192,242);
                csGrandTotalRow.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                csGrandTotalRow.TextEffect = TextEffectEnum.Flat;
                csGrandTotalRow.ForeColor = Color.Maroon;
                csGrandTotalRow.TextAlign = TextAlignEnum.RightCenter;
            }

         //   csGrandTotalTitleRow = c1Grid.Styles.Add("GrandTotalTitleRow");
            try
            {
                if (c1Grid.Styles.Contains("GrandTotalTitleRow"))
                {
                    csGrandTotalTitleRow = c1Grid.Styles["GrandTotalTitleRow"];
                }
                else
                {
                    csGrandTotalTitleRow = c1Grid.Styles.Add("GrandTotalTitleRow");
                    csGrandTotalTitleRow.BackColor = Color.FromArgb(255, 255, 255);  //FromArgb(168,192,242);
                    csGrandTotalTitleRow.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    csGrandTotalTitleRow.TextEffect = TextEffectEnum.Flat;
                    csGrandTotalTitleRow.ForeColor = Color.Maroon;
                    csGrandTotalTitleRow.TextAlign = TextAlignEnum.LeftCenter;
                }

            }
            catch
            {
                csGrandTotalTitleRow = c1Grid.Styles.Add("GrandTotalTitleRow");
                csGrandTotalTitleRow.BackColor = Color.FromArgb(255, 255, 255);  //FromArgb(168,192,242);
                csGrandTotalTitleRow.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                csGrandTotalTitleRow.TextEffect = TextEffectEnum.Flat;
                csGrandTotalTitleRow.ForeColor = Color.Maroon;
                csGrandTotalTitleRow.TextAlign = TextAlignEnum.LeftCenter;
            }
 
    //        csPaymentSource = c1Grid.Styles.Add("PaymentSource");
            try
            {
                if (c1Grid.Styles.Contains("PaymentSource"))
                {
                    csPaymentSource = c1Grid.Styles["PaymentSource"];
                }
                else
                {
                    csPaymentSource = c1Grid.Styles.Add("PaymentSource");
                    csPaymentSource.BackColor = Color.FromArgb(125, 165, 230);  //FromArgb(168,192,242);
                    csPaymentSource.Font = gloGlobal.clsgloFont.gFont_BOLD; //new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    csPaymentSource.TextEffect = TextEffectEnum.Flat;
                    csPaymentSource.ForeColor = Color.Black;
                    csPaymentSource.TextAlign = TextAlignEnum.LeftCenter;
                }

            }
            catch
            {
                csPaymentSource = c1Grid.Styles.Add("PaymentSource");
                csPaymentSource.BackColor = Color.FromArgb(125, 165, 230);  //FromArgb(168,192,242);
                csPaymentSource.Font = gloGlobal.clsgloFont.gFont_BOLD; //new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                csPaymentSource.TextEffect = TextEffectEnum.Flat;
                csPaymentSource.ForeColor = Color.Black;
                csPaymentSource.TextAlign = TextAlignEnum.LeftCenter;
            }
  

        //    csAdvancePayment = c1Grid.Styles.Add("AdvancePayment");
            try
            {
                if (c1Grid.Styles.Contains("AdvancePayment"))
                {
                    csAdvancePayment = c1Grid.Styles["AdvancePayment"];
                }
                else
                {
                    csAdvancePayment = c1Grid.Styles.Add("AdvancePayment");
                    csAdvancePayment.BackColor = Color.FromArgb(177, 197, 234);  //FromArgb(168,192,242);
                    csAdvancePayment.Font = gloGlobal.clsgloFont.gFont_BOLD; //new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    csAdvancePayment.TextEffect = TextEffectEnum.Flat;
                    csAdvancePayment.ForeColor = Color.Black;
                    csAdvancePayment.TextAlign = TextAlignEnum.LeftCenter;
                }

            }
            catch
            {
                csAdvancePayment = c1Grid.Styles.Add("AdvancePayment");
                csAdvancePayment.BackColor = Color.FromArgb(177, 197, 234);  //FromArgb(168,192,242);
                csAdvancePayment.Font = gloGlobal.clsgloFont.gFont_BOLD; //new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                csAdvancePayment.TextEffect = TextEffectEnum.Flat;
                csAdvancePayment.ForeColor = Color.Black;
                csAdvancePayment.TextAlign = TextAlignEnum.LeftCenter;
            }
 


          //  csClaimHeader = c1Grid.Styles.Add("claimHeader");
            try
            {
                if (c1Grid.Styles.Contains("claimHeader"))
                {
                    csClaimHeader = c1Grid.Styles["claimHeader"];
                }
                else
                {
                    csClaimHeader = c1Grid.Styles.Add("claimHeader");
                    csClaimHeader.BackColor = Color.FromArgb(255, 179, 0);  //FromArgb(168,192,242);
                    csClaimHeader.Font = gloGlobal.clsgloFont.gFont_BOLD; //new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    csClaimHeader.TextEffect = TextEffectEnum.Flat;
                    csClaimHeader.TextAlign = TextAlignEnum.LeftCenter;
                }

            }
            catch
            {
                csClaimHeader = c1Grid.Styles.Add("claimHeader");
                csClaimHeader.BackColor = Color.FromArgb(255, 179, 0);  //FromArgb(168,192,242);
                csClaimHeader.Font = gloGlobal.clsgloFont.gFont_BOLD; //new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                csClaimHeader.TextEffect = TextEffectEnum.Flat;
                csClaimHeader.TextAlign = TextAlignEnum.LeftCenter;
            }
 

           // csBalanceContents = c1Grid.Styles.Add("BalanceContents");
            try
            {
                if (c1Grid.Styles.Contains("BalanceContents"))
                {
                    csBalanceContents = c1Grid.Styles["BalanceContents"];
                }
                else
                {
                    csBalanceContents = c1Grid.Styles.Add("BalanceContents");
                    //csBalanceContents.BackColor = Color.FromArgb(255, 179, 0);  //FromArgb(168,192,242);
                    csBalanceContents.Font = gloGlobal.clsgloFont.gFont_BOLD; //new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    csBalanceContents.TextEffect = TextEffectEnum.Flat;
                    csBalanceContents.TextAlign = TextAlignEnum.LeftCenter;
                }

            }
            catch
            {
                csBalanceContents = c1Grid.Styles.Add("BalanceContents");
                //csBalanceContents.BackColor = Color.FromArgb(255, 179, 0);  //FromArgb(168,192,242);
                csBalanceContents.Font = gloGlobal.clsgloFont.gFont_BOLD; //new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                csBalanceContents.TextEffect = TextEffectEnum.Flat;
                csBalanceContents.TextAlign = TextAlignEnum.LeftCenter;
            }
     




            #endregion "Styles"


            c1Grid.AllowSorting = AllowSortingEnum.None;
        }

        #endregion


        #region " Form Load "

        private void frmBillingPatientLedger_Load(object sender, EventArgs e)
        {
            DesignChargesGrid(c1ClaimGrid);
            DesignPatientDetialsGrid();
            FillClaimsNew();
            //FillClaimsNew();
        }


        #endregion


        #region  " Fill Methods "

        //private void FillClaims()
        //{
        //    //gloClaimManager ogloClaimManager = new gloClaimManager(_databaseconnectionstring, "");
        //    DataTable dtClaims = null;
        //    String sClaimno = "";
        //    String sLastclaimNo = "";
        //    int rowIndex = 0;
        //    DataTable dtPaysource = null;
        //    string _sLastTransactionID = "";
        //    string _sTransactionID = "";

        //    try
        //    {
        //        DesignChargesGrid(c1ClaimGrid);


        //        if (c1ClaimGrid != null)
        //        {

        //            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //            oDB.Connect(false);

        //            gloDatabaseLayer.DBParameters oDBParameters1 = new gloDatabaseLayer.DBParameters();
        //            oDBParameters1.Add("@nPatientID", Convert.ToInt64(_PatientID), ParameterDirection.Input, SqlDbType.BigInt);
        //            oDBParameters1.Add("@nClaimNo", Convert.ToInt64(0), ParameterDirection.Input, SqlDbType.BigInt);
        //            oDB.Retrive("BL_SELECT_Tracking_PatientLedger", oDBParameters1, out  dtClaims);


        //            Decimal _30DaysBalanceDue = 0;
        //            Decimal _30DaysCharges = 0;
        //            Decimal _30DayesPaid = 0;

        //            Decimal _60DaysBalanceDue = 0;
        //            Decimal _60DaysCharges = 0;
        //            Decimal _60DayesPaid = 0;

        //            Decimal _90DaysBalanceDue = 0;
        //            Decimal _90DaysCharges = 0;
        //            Decimal _90DayesPaid = 0;

        //            Decimal _120DaysBalanceDue = 0;
        //            Decimal _120DaysCharges = 0;
        //            Decimal _120DayesPaid = 0;

        //            Decimal _Above120DaysBalanceDue = 0;
        //            Decimal _Above120DaysCharges = 0;
        //            Decimal _Above120DayesPaid = 0;

        //            dTotal_Charges = 0;
        //            dTotal_Allowed = 0;
        //            dTotal_ContAdj = 0;
        //            dTotal_InsurancePayment = 0;
        //            dTotal_WithHold = 0;
        //            dTotal_PatientPayment = 0;

        //            if (dtClaims != null && dtClaims.Rows.Count > 0)
        //            {
        //                for (int i = 0; i < dtClaims.Rows.Count; i++)
        //                {
        //                    sClaimno = dtClaims.Rows[i]["nClaimNumber"].ToString();
        //                    _sTransactionID = dtClaims.Rows[i]["nTransactionID"].ToString();

        //                    #region "Sum of claim"

        //                    if (i > 0 && sLastclaimNo != sClaimno)
        //                    {
        //                        c1ClaimGrid.Rows.Add();
        //                        rowIndex = c1ClaimGrid.Rows.Count - 1;

        //                        //c1ClaimGrid.SetData(rowIndex, COL_6, "Total: ");
        //                        c1ClaimGrid.SetData(rowIndex, COL_6, "Total: ", false);
        //                        c1ClaimGrid.SetData(rowIndex, COL_11, sLastclaimNo);
        //                        c1ClaimGrid.SetData(rowIndex, COL_12, _sLastTransactionID);

        //                        CellRange subTotalRange;
        //                        subTotalRange = c1ClaimGrid.GetCellRange(rowIndex, COL_7, rowIndex, COL_7);
        //                        subTotalRange.Style = csSubTotalRow;
        //                        c1ClaimGrid.SetData(rowIndex, COL_7, d_Charges, true);


        //                        CellRange subGrandTotal;
        //                        subGrandTotal = c1ClaimGrid.GetCellRange(rowIndex, COL_6, rowIndex, COL_6);
        //                        subGrandTotal.Style = csGrandTotalTitleRow;


        //                        d_Charges = 0;

        //                        c1ClaimGrid.Rows.Add();
        //                        rowIndex = c1ClaimGrid.Rows.Count - 1;

        //                        c1ClaimGrid.SetData(rowIndex, COL_2, "Payment Source");
        //                        c1ClaimGrid.SetData(rowIndex, COL_3, "Payment Date");
        //                        c1ClaimGrid.SetData(rowIndex, COL_4, "Payment");
        //                        c1ClaimGrid.SetData(rowIndex, COL_5, "W/O");
        //                        c1ClaimGrid.SetData(rowIndex, COL_6, "Withhold");
        //                        c1ClaimGrid.SetData(rowIndex, COL_7, "Copay", false);
        //                        c1ClaimGrid.SetData(rowIndex, COL_8, "Co-Ins");
        //                        c1ClaimGrid.SetData(rowIndex, COL_9, "Ded");
        //                        c1ClaimGrid.SetData(rowIndex, COL_10, "PaymentTray");

        //                        subPaymentsource = new CellRange();
        //                        subPaymentsource = c1ClaimGrid.GetCellRange(rowIndex, COL_1, rowIndex, COL_10);
        //                        subPaymentsource.Style = csPaymentSource;

        //                        c1ClaimGrid.SetData(rowIndex, COL_11, sLastclaimNo);
        //                        c1ClaimGrid.SetData(rowIndex, COL_12, _sLastTransactionID);



        //                        c1ClaimGrid.Rows.Add();
        //                        rowIndex = c1ClaimGrid.Rows.Count - 1;

        //                        c1ClaimGrid.SetData(rowIndex, COL_2, InsuranceName);
        //                        c1ClaimGrid.SetData(rowIndex, COL_3, PaymentDate);

        //                        CrDollars = new CellRange();
        //                        CrDollars = c1ClaimGrid.GetCellRange(rowIndex, COL_4, rowIndex, COL_9);
        //                        CrDollars.Style = csCurrency;

        //                        c1ClaimGrid.SetData(rowIndex, COL_4, d_InsurancePayment, true);
        //                        c1ClaimGrid.SetData(rowIndex, COL_5, d_ContAdj, true);
        //                        c1ClaimGrid.SetData(rowIndex, COL_6, d_Withhold, true);
        //                        c1ClaimGrid.SetData(rowIndex, COL_7, d_Copay, true);
        //                        c1ClaimGrid.SetData(rowIndex, COL_8, d_CoIns, true);
        //                        c1ClaimGrid.SetData(rowIndex, COL_9, d_Deductible, true);
        //                        c1ClaimGrid.SetData(rowIndex, COL_10, PaymentTray, false);

        //                        c1ClaimGrid.SetData(rowIndex, COL_11, sLastclaimNo, false);
        //                        c1ClaimGrid.SetData(rowIndex, COL_12, _sLastTransactionID, false);

        //                        InsuranceName = "";
        //                        PaymentDate = "";
        //                        d_InsurancePayment = 0;
        //                        d_ContAdj = 0;
        //                        d_Withhold = 0;
        //                        d_Copay = 0;
        //                        d_CoIns = 0;
        //                        d_Deductible = 0;
        //                        PaymentTray = "";


        //                        c1ClaimGrid.Rows.Add();
        //                        rowIndex = c1ClaimGrid.Rows.Count - 1;

        //                        c1ClaimGrid.SetData(rowIndex, COL_11, sLastclaimNo);
        //                        c1ClaimGrid.SetData(rowIndex, COL_12, _sLastTransactionID);

        //                        c1ClaimGrid.Rows.Add();
        //                        rowIndex = c1ClaimGrid.Rows.Count - 1;

        //                        c1ClaimGrid.SetData(rowIndex, COL_11, sLastclaimNo);
        //                        c1ClaimGrid.SetData(rowIndex, COL_12, _sLastTransactionID);

        //                    }

        //                    #endregion

        //                    #region  "Header Details"

        //                    c1ClaimGrid.Rows.Add();
        //                    rowIndex = c1ClaimGrid.Rows.Count - 1;

        //                    if (sClaimno != sLastclaimNo)
        //                    {
        //                        c1ClaimGrid.SetData(rowIndex, COL_1, Convert.ToString(dtClaims.Rows[i]["nClaimNumber"]));
        //                        c1ClaimGrid.SetData(rowIndex, COL_2, Convert.ToString(dtClaims.Rows[i]["nFromDate"]));
        //                        c1ClaimGrid.SetData(rowIndex, COL_8, Convert.ToString(dtClaims.Rows[i]["ProviderName"]));
        //                        c1ClaimGrid.SetData(rowIndex, COL_9, Convert.ToString(dtClaims.Rows[i]["ChargesTrayDesc"]));
        //                        c1ClaimGrid.SetData(rowIndex, COL_10, Convert.ToString(dtClaims.Rows[i]["sIsVoided"]));


        //                        CellRange crClaimHeader;
        //                        crClaimHeader = c1ClaimGrid.GetCellRange(rowIndex, COL_1, rowIndex, COL_1);
        //                        crClaimHeader.Style = csClaimHeader;

        //                        if (Convert.ToString(dtClaims.Rows[i]["sIsVoided"]) == "Voided")
        //                        {
        //                            CellRange crIsVoided;
        //                            crIsVoided = c1ClaimGrid.GetCellRange(rowIndex, COL_10, rowIndex, COL_10);
        //                            crIsVoided.Style = csClaimHeader;
        //                        }

        //                        #region "Fetch Payment Source"
        //                        string _LastCPTCode = "";
        //                        gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
        //                        oDBParameters.Add("@ClaimNo", Convert.ToInt64(sClaimno), ParameterDirection.Input, SqlDbType.BigInt);
        //                        oDB.Retrive("BL_SELECT_EOBPatientLedgerLines", oDBParameters, out  dtPaysource);
        //                        //oDB.Disconnect();
        //                        if (dtPaysource != null)
        //                        {
        //                            if (dtPaysource.Rows.Count > 0)
        //                            {
        //                                for (int j = 0; j < dtPaysource.Rows.Count; j++)
        //                                {

        //                                    d_InsurancePayment = d_InsurancePayment + Convert.ToDecimal(dtPaysource.Rows[j]["InsurancePayment"]);

        //                                    if (Convert.ToString(dtPaysource.Rows[j]["sCPTCode"]) != _LastCPTCode)
        //                                    {
        //                                        d_ContAdj = d_ContAdj + Convert.ToDecimal(dtPaysource.Rows[j]["ContAdj"]);
        //                                    }
        //                                    d_Withhold = d_Withhold + Convert.ToDecimal(dtPaysource.Rows[j]["Withhold"]);
        //                                    d_Copay = d_Copay + Convert.ToDecimal(dtPaysource.Rows[j]["Copay"]);
        //                                    d_CoIns = d_CoIns + Convert.ToDecimal(dtPaysource.Rows[j]["Coinsurance"]);
        //                                    d_Deductible = d_Deductible + Convert.ToDecimal(dtPaysource.Rows[j]["Deductible"]);
        //                                    InsuranceName = Convert.ToString(dtPaysource.Rows[j]["PaymentInsuranceName"]);
        //                                    PaymentTray = Convert.ToString(dtPaysource.Rows[j]["PaymentTray"]);
        //                                    PaymentDate = Convert.ToString(dtPaysource.Rows[j]["PaymentDate"]);

        //                                    DateTime dtDOS = Convert.ToDateTime(dtPaysource.Rows[j]["DOS"]);

        //                                    if (Convert.ToString(dtClaims.Rows[i]["sIsVoided"]) != "Voided")
        //                                    {
        //                                        if (dtDOS <= DateTime.Now.Date && dtDOS >= DateTime.Now.Subtract(new TimeSpan(30, 0, 0, 0)))
        //                                        {
        //                                            //_30DayesPaid += Convert.ToDecimal(dtPaysource.Rows[j]["InsurancePayment"]) + Convert.ToDecimal(dtPaysource.Rows[j]["ContAdj"]) + Convert.ToDecimal(dtPaysource.Rows[j]["Withhold"]);
        //                                            _30DayesPaid += Convert.ToDecimal(dtPaysource.Rows[j]["InsurancePayment"]) + Convert.ToDecimal(d_ContAdj) + Convert.ToDecimal(dtPaysource.Rows[j]["Withhold"]);

        //                                        }
        //                                        else if (dtDOS < DateTime.Now.Subtract(new TimeSpan(30, 0, 0, 0)) && dtDOS >= DateTime.Now.Subtract(new TimeSpan(60, 0, 0, 0)))
        //                                        {
        //                                            //_60DayesPaid += Convert.ToDecimal(dtPaysource.Rows[j]["InsurancePayment"]) + Convert.ToDecimal(dtPaysource.Rows[j]["ContAdj"]) + Convert.ToDecimal(dtPaysource.Rows[j]["Withhold"]);
        //                                            _60DayesPaid += Convert.ToDecimal(dtPaysource.Rows[j]["InsurancePayment"]) + Convert.ToDecimal(d_ContAdj) + Convert.ToDecimal(dtPaysource.Rows[j]["Withhold"]);
        //                                        }
        //                                        else if (dtDOS < DateTime.Now.Subtract(new TimeSpan(60, 0, 0, 0)) && dtDOS >= DateTime.Now.Subtract(new TimeSpan(90, 0, 0, 0)))
        //                                        {
        //                                            //_90DayesPaid += Convert.ToDecimal(dtPaysource.Rows[j]["InsurancePayment"]) + Convert.ToDecimal(dtPaysource.Rows[j]["ContAdj"]) + Convert.ToDecimal(dtPaysource.Rows[j]["Withhold"]);
        //                                            _90DayesPaid += Convert.ToDecimal(dtPaysource.Rows[j]["InsurancePayment"]) + Convert.ToDecimal(d_ContAdj) + Convert.ToDecimal(dtPaysource.Rows[j]["Withhold"]);
        //                                        }
        //                                        else if (dtDOS < DateTime.Now.Subtract(new TimeSpan(90, 0, 0, 0)) && dtDOS >= DateTime.Now.Subtract(new TimeSpan(120, 0, 0, 0)))
        //                                        {
        //                                            //_120DayesPaid += Convert.ToDecimal(dtPaysource.Rows[j]["InsurancePayment"]) + Convert.ToDecimal(dtPaysource.Rows[j]["ContAdj"]) + Convert.ToDecimal(dtPaysource.Rows[j]["Withhold"]);
        //                                            _120DayesPaid += Convert.ToDecimal(dtPaysource.Rows[j]["InsurancePayment"]) + Convert.ToDecimal(d_ContAdj) + Convert.ToDecimal(dtPaysource.Rows[j]["Withhold"]);
        //                                        }
        //                                        else if (dtDOS <= DateTime.Now.Subtract(new TimeSpan(120, 0, 0, 0)))
        //                                        {
        //                                            // _Above120DayesPaid += Convert.ToDecimal(dtPaysource.Rows[j]["InsurancePayment"]) + Convert.ToDecimal(dtPaysource.Rows[j]["ContAdj"]) + Convert.ToDecimal(dtPaysource.Rows[j]["Withhold"]);
        //                                            _Above120DayesPaid += Convert.ToDecimal(dtPaysource.Rows[j]["InsurancePayment"]) + Convert.ToDecimal(d_ContAdj) + Convert.ToDecimal(dtPaysource.Rows[j]["Withhold"]);
        //                                        }
        //                                    }

        //                                    _LastCPTCode = Convert.ToString(dtPaysource.Rows[j]["sCPTCode"]);
        //                                }
        //                            }
        //                        }

        //                        #endregion

        //                    }

        //                    d_Charges = d_Charges + Convert.ToDecimal(dtClaims.Rows[i]["dTotal"]);


        //                    #region "Balance"


        //                    if (Convert.ToString(dtClaims.Rows[i]["sIsVoided"]) != "Voided")
        //                    {
        //                        DateTime dtTransdate = Convert.ToDateTime(dtClaims.Rows[i]["nFromDate"]);

        //                        if (dtTransdate <= DateTime.Now.Date && dtTransdate >= DateTime.Now.Subtract(new TimeSpan(30, 0, 0, 0)))
        //                        {
        //                            _30DaysCharges += Convert.ToDecimal(dtClaims.Rows[i]["dTotal"]);
        //                        }
        //                        else if (dtTransdate < DateTime.Now.Subtract(new TimeSpan(30, 0, 0, 0)) && dtTransdate >= DateTime.Now.Subtract(new TimeSpan(60, 0, 0, 0)))
        //                        {
        //                            _60DaysCharges += Convert.ToDecimal(dtClaims.Rows[i]["dTotal"]);
        //                        }
        //                        else if (dtTransdate < DateTime.Now.Subtract(new TimeSpan(60, 0, 0, 0)) && dtTransdate >= DateTime.Now.Subtract(new TimeSpan(90, 0, 0, 0)))
        //                        {
        //                            _90DaysCharges += Convert.ToDecimal(dtClaims.Rows[i]["dTotal"]);
        //                        }
        //                        else if (dtTransdate < DateTime.Now.Subtract(new TimeSpan(90, 0, 0, 0)) && dtTransdate >= DateTime.Now.Subtract(new TimeSpan(120, 0, 0, 0)))
        //                        {
        //                            _120DaysCharges += Convert.ToDecimal(dtClaims.Rows[i]["dTotal"]);
        //                        }
        //                        else if (dtTransdate <= DateTime.Now.Subtract(new TimeSpan(120, 0, 0, 0)))
        //                        {
        //                            _Above120DaysCharges += Convert.ToDecimal(dtClaims.Rows[i]["dTotal"]);
        //                        }
        //                    }

        //                    #endregion

        //                    c1ClaimGrid.SetData(rowIndex, COL_3, Convert.ToString(dtClaims.Rows[i]["sCPTCode"]));
        //                    c1ClaimGrid.SetData(rowIndex, COL_4, Convert.ToString(dtClaims.Rows[i]["sMod1Code"]));
        //                    c1ClaimGrid.SetData(rowIndex, COL_5, Convert.ToString(dtClaims.Rows[i]["sMod2Code"]));
        //                    c1ClaimGrid.SetData(rowIndex, COL_6, Convert.ToString(Convert.ToInt64(dtClaims.Rows[i]["dUnit"])));

        //                    CellRange CrDollars2 = new CellRange();
        //                    CrDollars2 = c1ClaimGrid.GetCellRange(rowIndex, COL_7, rowIndex, COL_7);
        //                    CrDollars2.Style = csCurrency;

        //                    c1ClaimGrid.SetData(rowIndex, COL_7, Convert.ToString(dtClaims.Rows[i]["dTotal"]), true);


        //                    //c1ClaimGrid.Cols[COL_7].Style = csCurrency;



        //                    c1ClaimGrid.SetData(rowIndex, COL_11, Convert.ToString(dtClaims.Rows[i]["nClaimNumber"]));
        //                    c1ClaimGrid.SetData(rowIndex, COL_12, Convert.ToString(dtClaims.Rows[i]["nTransactionID"]));

        //                    sLastclaimNo = dtClaims.Rows[i]["nClaimNumber"].ToString();
        //                    _sLastTransactionID = dtClaims.Rows[i]["nTransactionID"].ToString();

        //                    #endregion

        //                }

        //                #region "Sum of Last claim"

        //                c1ClaimGrid.Rows.Add();
        //                rowIndex = c1ClaimGrid.Rows.Count - 1;

        //                CellRange subGrandTotal2;
        //                subGrandTotal2 = c1ClaimGrid.GetCellRange(rowIndex, COL_6, rowIndex, COL_6);
        //                subGrandTotal2.Style = csGrandTotalTitleRow;
        //                c1ClaimGrid.SetData(rowIndex, COL_6, "Total: ", true);


        //                CellRange subTotalRangeLast;
        //                subTotalRangeLast = c1ClaimGrid.GetCellRange(rowIndex, COL_7, rowIndex, COL_7);
        //                subTotalRangeLast.Style = csSubTotalRow;
        //                c1ClaimGrid.SetData(rowIndex, COL_7, d_Charges, true);

        //                c1ClaimGrid.SetData(rowIndex, COL_11, sClaimno);
        //                c1ClaimGrid.SetData(rowIndex, COL_12, _sTransactionID);


        //                d_Charges = 0;


        //                c1ClaimGrid.Rows.Add();
        //                rowIndex = c1ClaimGrid.Rows.Count - 1;

        //                c1ClaimGrid.SetData(rowIndex, COL_2, "Payment Source");
        //                c1ClaimGrid.SetData(rowIndex, COL_3, "Payment Date");
        //                c1ClaimGrid.SetData(rowIndex, COL_4, "Payment");
        //                c1ClaimGrid.SetData(rowIndex, COL_5, "W/O");
        //                c1ClaimGrid.SetData(rowIndex, COL_6, "Withhold");
        //                c1ClaimGrid.SetData(rowIndex, COL_7, "Copay");
        //                c1ClaimGrid.SetData(rowIndex, COL_8, "Co-Ins");
        //                c1ClaimGrid.SetData(rowIndex, COL_9, "Ded");
        //                c1ClaimGrid.SetData(rowIndex, COL_10, "PaymentTray");

        //                subPaymentsource = new CellRange();
        //                subPaymentsource = c1ClaimGrid.GetCellRange(rowIndex, COL_1, rowIndex, COL_10);
        //                subPaymentsource.Style = csPaymentSource;

        //                c1ClaimGrid.SetData(rowIndex, COL_11, sClaimno);
        //                c1ClaimGrid.SetData(rowIndex, COL_12, _sTransactionID);


        //                c1ClaimGrid.Rows.Add();
        //                rowIndex = c1ClaimGrid.Rows.Count - 1;

        //                c1ClaimGrid.SetData(rowIndex, COL_2, InsuranceName);
        //                c1ClaimGrid.SetData(rowIndex, COL_3, PaymentDate);

        //                CrDollars = new CellRange();
        //                CrDollars = c1ClaimGrid.GetCellRange(rowIndex, COL_4, rowIndex, COL_9);
        //                CrDollars.Style = csCurrency;

        //                c1ClaimGrid.SetData(rowIndex, COL_4, d_InsurancePayment, true);
        //                c1ClaimGrid.SetData(rowIndex, COL_5, d_ContAdj, true);
        //                c1ClaimGrid.SetData(rowIndex, COL_6, d_Withhold, true);
        //                c1ClaimGrid.SetData(rowIndex, COL_7, d_Copay, true);
        //                c1ClaimGrid.SetData(rowIndex, COL_8, d_CoIns, true);
        //                c1ClaimGrid.SetData(rowIndex, COL_9, d_Deductible, true);
        //                c1ClaimGrid.SetData(rowIndex, COL_10, PaymentTray, false);

        //                c1ClaimGrid.SetData(rowIndex, COL_11, sLastclaimNo);
        //                c1ClaimGrid.SetData(rowIndex, COL_12, _sLastTransactionID);


        //                InsuranceName = "";
        //                PaymentDate = "";
        //                d_InsurancePayment = 0;
        //                d_ContAdj = 0;
        //                d_Withhold = 0;
        //                d_Copay = 0;
        //                d_CoIns = 0;
        //                d_Deductible = 0;
        //                PaymentTray = "";



        //                c1ClaimGrid.Rows.Add();
        //                rowIndex = c1ClaimGrid.Rows.Count - 1;

        //                c1ClaimGrid.SetData(rowIndex, COL_11, sLastclaimNo);
        //                c1ClaimGrid.SetData(rowIndex, COL_12, _sLastTransactionID);


        //                c1ClaimGrid.Rows.Add();
        //                rowIndex = c1ClaimGrid.Rows.Count - 1;

        //                //c1ClaimGrid.Rows.Add();
        //                //rowIndex = c1ClaimGrid.Rows.Count - 1;


        //                #region "Advance Payment"

        //                FetchAdvancePayment(c1ClaimGrid, 1);

        //                #endregion


        //                #region "Patient Balance"

        //                _30DaysBalanceDue = _30DaysCharges - (_30DayesPaid);
        //                _60DaysBalanceDue = _60DaysCharges - (_60DayesPaid);
        //                _90DaysBalanceDue = _90DaysCharges - (_90DayesPaid);
        //                _120DaysBalanceDue = _120DaysCharges - (_120DayesPaid);
        //                _Above120DaysBalanceDue = _Above120DaysCharges - (_Above120DayesPaid);

        //                int _rowIndex = 0;

        //                C1PatientDetails.Rows.Add();
        //                _rowIndex = C1PatientDetails.Rows.Count - 1;


        //                C1PatientDetails.SetData(_rowIndex, COLPAT_1, "31-60");
        //                C1PatientDetails.SetData(_rowIndex, COLPAT_2, _30DaysBalanceDue);
        //                C1PatientDetails.Rows.Add();
        //                _rowIndex = C1PatientDetails.Rows.Count - 1;

        //                C1PatientDetails.SetData(_rowIndex, COLPAT_1, "61-90");
        //                C1PatientDetails.SetData(_rowIndex, COLPAT_2, _60DaysBalanceDue);
        //                C1PatientDetails.Rows.Add();
        //                _rowIndex = C1PatientDetails.Rows.Count - 1;


        //                C1PatientDetails.SetData(_rowIndex, COLPAT_1, "91-120");
        //                C1PatientDetails.SetData(_rowIndex, COLPAT_2, _90DaysBalanceDue);
        //                C1PatientDetails.Rows.Add();
        //                _rowIndex = C1PatientDetails.Rows.Count - 1;


        //                C1PatientDetails.SetData(_rowIndex, COLPAT_1, "121-150");
        //                C1PatientDetails.SetData(_rowIndex, COLPAT_2, _120DaysBalanceDue);
        //                C1PatientDetails.Rows.Add();
        //                _rowIndex = C1PatientDetails.Rows.Count - 1;


        //                C1PatientDetails.SetData(_rowIndex, COLPAT_1, ">150");
        //                C1PatientDetails.SetData(_rowIndex, COLPAT_2, _Above120DaysBalanceDue);
        //                C1PatientDetails.Rows.Add();
        //                _rowIndex = C1PatientDetails.Rows.Count - 1;


        //                C1PatientDetails.SetData(_rowIndex, COLPAT_1, "Total :");
        //                C1PatientDetails.SetData(_rowIndex, COLPAT_2, _30DaysBalanceDue + _60DaysBalanceDue + _90DaysBalanceDue + _120DaysBalanceDue + _Above120DaysBalanceDue);



        //                CellRange subPatTotalCaption;
        //                subPatTotalCaption = C1PatientDetails.GetCellRange(_rowIndex, COL_1, _rowIndex, COL_1);
        //                subPatTotalCaption.Style = csGrandTotalTitleRow;


        //                CellRange subPatTot;
        //                subPatTot = C1PatientDetails.GetCellRange(_rowIndex, COL_2, _rowIndex, COL_2);
        //                subPatTot.Style = csSubTotalRow;


        //                _30DaysBalanceDue = 0;
        //                _60DaysBalanceDue = 0;
        //                _90DaysBalanceDue = 0;
        //                _120DaysBalanceDue = 0;
        //                _Above120DaysBalanceDue = 0;
        //                _30DaysCharges = 0;
        //                _30DayesPaid = 0;
        //                _60DaysCharges = 0;
        //                _60DayesPaid = 0;
        //                _90DaysCharges = 0;
        //                _90DayesPaid = 0;
        //                _120DaysCharges = 0;
        //                _120DayesPaid = 0;
        //                _Above120DaysCharges = 0;
        //                _Above120DayesPaid = 0;


        //                #endregion



        //                #endregion

        //            }
        //            if (oDB != null) { oDB.Dispose(); };

        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {

        //    }

        //}


        private void FillClaimsNewUpdated()
        {

            DataTable dtClaims = null;
            //String sClaimno = "";
            String sLastclaimNo = "";
         //   String sLastCPT = "";
          //  String sCPT = "";
            int rowIndex = 0;
            DataTable dtPaysource = null;
            string _sLastTransactionID = "";
         //   string _sTransactionID = "";
        //    string _sLastTransactionDetailID = "";
         //   string _sTransactionDetailID = "";
            DataTable dtTemp = new DataTable();
            try
            {
                DesignChargesGrid(c1ClaimGrid);


                if (c1ClaimGrid != null)
                {

                    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    oDB.Connect(false);

                    gloDatabaseLayer.DBParameters oDBParameters1 = new gloDatabaseLayer.DBParameters();
                    oDBParameters1.Add("@nPatientID", Convert.ToInt64(_PatientID), ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters1.Add("@nClaimNo", Convert.ToInt64(0), ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Retrive("BL_SELECT_EOBPatientLedger", oDBParameters1, out  dtClaims);



                   

                    if (dtClaims != null && dtClaims.Rows.Count > 0)
                    {


                        DataTable dtClaimTemp = new DataTable();
                        dtClaimTemp = dtClaims.DefaultView.ToTable(true, new string[1] { "nClaimNumber" });
                       
                        for (int k = 0; k < dtClaimTemp.Rows.Count; k++)
                        {
                            dtClaims.DefaultView.RowFilter = "nClaimNumber = " + dtClaimTemp.Rows[k]["nClaimNumber"].ToString();
                            Decimal dTotal_Charges = 0;
                            String sClaimno = dtClaimTemp.Rows[k]["nClaimNumber"].ToString();
                            for (int i = 0; i < dtClaims.DefaultView.Count; i++)
                            {

                               
                                c1ClaimGrid.Rows.Add();
                                rowIndex = c1ClaimGrid.Rows.Count - 1;

                                if (i == 0)
                                {
                                    CellRange crClaimHeader;
                                    crClaimHeader = c1ClaimGrid.GetCellRange(rowIndex, COL_1, rowIndex, COL_1);
                                    crClaimHeader.Style = csClaimHeader;

                                    c1ClaimGrid.SetData(rowIndex, COL_1, Convert.ToString(dtClaims.DefaultView[i]["nClaimNumber"]));
                                    c1ClaimGrid.SetData(rowIndex, COL_2, Convert.ToString(dtClaims.DefaultView[i]["nFromDate"]));
                                    c1ClaimGrid.SetData(rowIndex, COL_8, Convert.ToString(dtClaims.DefaultView[i]["ProviderName"]));
                                    c1ClaimGrid.SetData(rowIndex, COL_9, Convert.ToString(dtClaims.DefaultView[i]["ChargesTrayDesc"]));
                                    c1ClaimGrid.SetData(rowIndex, COL_10, Convert.ToString(dtClaims.DefaultView[i]["sIsVoided"]));
                                }


                                c1ClaimGrid.SetData(rowIndex, COL_3, Convert.ToString(dtClaims.DefaultView[i]["sCPTCode"]));
                                c1ClaimGrid.SetData(rowIndex, COL_4, Convert.ToString(dtClaims.DefaultView[i]["sMod1Code"]));
                                c1ClaimGrid.SetData(rowIndex, COL_5, Convert.ToString(dtClaims.DefaultView[i]["sMod2Code"]));
                                c1ClaimGrid.SetData(rowIndex, COL_6, Convert.ToDecimal(dtClaims.DefaultView[i]["dUnit"]).ToString("#0.###############"));
                                c1ClaimGrid.SetData(rowIndex, COL_7, Convert.ToString(dtClaims.DefaultView[i]["dTotal"]), true);

                                CellRange CrDollars2 = c1ClaimGrid.GetCellRange(rowIndex, COL_7, rowIndex, COL_7);
                                CrDollars2.Style = csCurrency;


                                dTotal_Charges = dTotal_Charges+Convert.ToDecimal(dtClaims.DefaultView[i]["dTotal"].ToString()); 

                               

                               
                                if (Convert.ToString(dtClaims.DefaultView[i]["sIsVoided"]) == "Voided")
                                {
                                    CellRange crIsVoided;
                                    crIsVoided = c1ClaimGrid.GetCellRange(rowIndex, COL_10, rowIndex, COL_10);
                                    crIsVoided.Style = csClaimHeader;
                                }

                                c1ClaimGrid.SetData(rowIndex, COL_11, Convert.ToString(dtClaims.DefaultView[i]["nClaimNumber"]));
                                c1ClaimGrid.SetData(rowIndex, COL_12, Convert.ToString(dtClaims.DefaultView[i]["nTransactionID"]));
                             

                            }
                            c1ClaimGrid.Rows.Add();
                            rowIndex = c1ClaimGrid.Rows.Count - 1;

                            c1ClaimGrid.SetData(rowIndex, COL_6, "Total: ", false);
                            c1ClaimGrid.SetData(rowIndex, COL_11, sLastclaimNo);
                            c1ClaimGrid.SetData(rowIndex, COL_12, _sLastTransactionID);

                            CellRange subTotalRange;
                            subTotalRange = c1ClaimGrid.GetCellRange(rowIndex, COL_7, rowIndex, COL_7);
                            subTotalRange.Style = csSubTotalRow;
                            c1ClaimGrid.SetData(rowIndex, COL_7, dTotal_Charges, true);




                            c1ClaimGrid.Rows.Add();
                            rowIndex = c1ClaimGrid.Rows.Count - 1;

                            c1ClaimGrid.SetData(rowIndex, COL_2, "Payment Source");
                            c1ClaimGrid.SetData(rowIndex, COL_3, "Payment Date");
                            c1ClaimGrid.SetData(rowIndex, COL_4, "Payment");
                            c1ClaimGrid.SetData(rowIndex, COL_5, "W/O");
                            c1ClaimGrid.SetData(rowIndex, COL_6, "Withhold");
                            c1ClaimGrid.SetData(rowIndex, COL_7, "Copay", false);
                            c1ClaimGrid.SetData(rowIndex, COL_8, "Co-Ins");
                            c1ClaimGrid.SetData(rowIndex, COL_9, "Ded");
                            c1ClaimGrid.SetData(rowIndex, COL_10, "PaymentTray");

                            subPaymentsource =  c1ClaimGrid.GetCellRange(rowIndex, COL_1, rowIndex, COL_10);
                            subPaymentsource.Style = csPaymentSource;


                            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                            oDBParameters.Add("@ClaimNo", Convert.ToInt64(sClaimno), ParameterDirection.Input, SqlDbType.BigInt);
                            oDB.Retrive("BL_SELECT_EOBPatientLedgerLines", oDBParameters, out  dtPaysource);
                                               
                            if (dtPaysource != null)
                            {
                                if (dtPaysource.Rows.Count > 0)
                                {
                                    InsuranceName = "";
                                    PaymentDate = "";
                                    d_InsurancePayment = 0;
                                    d_ContAdj = 0;
                                    d_Withhold = 0;
                                    d_Copay = 0;
                                    d_CoIns = 0;
                                    d_Deductible = 0;
                                    PaymentTray = "";
                                    dtTemp = new DataTable();
                                    //dtPaysource.DefaultView.Sort = "nInsuraceID"; 
                                    dtTemp = dtPaysource.DefaultView.ToTable(true, new string[1] { "nInsuraceID" });
                                    //string strSortIns = "nInsuraceID ASC";

                                    for (int p = 0; p < dtTemp.Rows.Count; p++)
                                    {
                                        dtPaysource.DefaultView.RowFilter = "nInsuraceID = " + dtTemp.Rows[p]["nInsuraceID"].ToString();

                                        for (int j = 0; j < dtPaysource.DefaultView.Count; j++)
                                        {

                                            DataTable dtCPT = new DataTable();                                           
                                            dtCPT =dtPaysource.DefaultView.Table.DefaultView.ToTable(true, new string[1] { "sCPTCode" });


                                            dtCPT.DefaultView.Sort = "InsurancePayment DESC";
                                            d_InsurancePayment = d_InsurancePayment + Convert.ToDecimal(dtCPT.DefaultView[0]["InsurancePayment"]);


                                            d_ContAdj = d_ContAdj + Convert.ToDecimal(dtPaysource.DefaultView[j]["ContAdj"]);
                                            d_Withhold = d_Withhold + Convert.ToDecimal(dtPaysource.DefaultView[j]["Withhold"]);
                                            d_Copay = d_Copay + Convert.ToDecimal(dtPaysource.DefaultView[j]["Copay"]);
                                            d_CoIns = d_CoIns + Convert.ToDecimal(dtPaysource.DefaultView[j]["Coinsurance"]);
                                            d_Deductible = d_Deductible + Convert.ToDecimal(dtPaysource.DefaultView[j]["Deductible"]);
                                            InsuranceName = Convert.ToString(dtPaysource.DefaultView[j]["PaymentInsuranceName"]);
                                            PaymentTray = Convert.ToString(dtPaysource.DefaultView[j]["PaymentTray"]);
                                            PaymentDate = Convert.ToString(dtPaysource.DefaultView[j]["PaymentDate"]);
                                        }





                                    }


                                }
                            }


                        }



                    }

                }
            }
            catch
            {

            }

        }

        private void FillClaimsNew()
        {

            C1PatientDetails.ScrollBars = ScrollBars.None;
            c1ClaimGrid.ScrollBars = ScrollBars.None;

            //FillClaimsNewUpdated();
            //gloClaimManager ogloClaimManager = new gloClaimManager(_databaseconnectionstring, "");
            DataTable dtClaims = null;
            DataTable dt = null;
            String sClaimno = "";
            String sLastclaimNo = "";
            String sLastCPT = "";
            String sCPT = "";
            int rowIndex = 0;
            DataTable dtPaysource = null;
            string _sLastTransactionID = "";
            string _sTransactionID = "";
            string _sLastTransactionDetailID = "";
            string _sTransactionDetailID = "";
            DataTable dtTemp = new DataTable();
            try
            {
                DesignChargesGrid(c1ClaimGrid);


                if (c1ClaimGrid != null)
                {

                    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    oDB.Connect(false);

                    gloDatabaseLayer.DBParameters oDBParameters1 = new gloDatabaseLayer.DBParameters();
                    oDBParameters1.Add("@nPatientID", Convert.ToInt64(_PatientID), ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters1.Add("@nClaimNo", Convert.ToInt64(0), ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Retrive("BL_SELECT_EOBPatientLedger", oDBParameters1, out  dt);
                    dt.DefaultView.Sort = "nClaimNumber Desc";
                    dtClaims = dt.DefaultView.ToTable(); 
                    Decimal _30DaysBalanceDue = 0;
                    Decimal _30DaysCharges = 0;
                    Decimal _30DayesPaid = 0;

                    Decimal _60DaysBalanceDue = 0;
                    Decimal _60DaysCharges = 0;
                    Decimal _60DayesPaid = 0;

                    Decimal _90DaysBalanceDue = 0;
                    Decimal _90DaysCharges = 0;
                    Decimal _90DayesPaid = 0;

                    Decimal _120DaysBalanceDue = 0;
                    Decimal _120DaysCharges = 0;
                    Decimal _120DayesPaid = 0;

                    Decimal _Above120DaysBalanceDue = 0;
                    Decimal _Above120DaysCharges = 0;
                    Decimal _Above120DayesPaid = 0;

                    dTotal_Charges = 0;
                    //dTotal_Allowed = 0;
                    dTotal_ContAdj = 0;
                    dTotal_InsurancePayment = 0;
                    dTotal_WithHold = 0;
                   // dTotal_PatientPayment = 0;

                    if (dtClaims != null && dtClaims.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtClaims.Rows.Count; i++)
                        {

                            sClaimno = dtClaims.Rows[i]["nClaimNumber"].ToString();
                            _sTransactionID = dtClaims.Rows[i]["nTransactionID"].ToString();
                            _sTransactionDetailID = dtClaims.Rows[i]["nTransactionDetailID"].ToString();
                            sCPT = dtClaims.Rows[i]["sCPTCode"].ToString();
                            
                            //dtPaysource = null;
                            #region "Sum of claim"

                            if (i > 0 && sLastclaimNo != sClaimno)
                            {
                                c1ClaimGrid.Rows.Add();
                                rowIndex = c1ClaimGrid.Rows.Count - 1;

                                //c1ClaimGrid.SetData(rowIndex, COL_6, "Total: ");
                                c1ClaimGrid.SetData(rowIndex, COL_6, "Total: ", false);
                                c1ClaimGrid.SetData(rowIndex, COL_11, sLastclaimNo);
                                c1ClaimGrid.SetData(rowIndex, COL_12, _sLastTransactionID);

                                CellRange subTotalRange;
                                subTotalRange = c1ClaimGrid.GetCellRange(rowIndex, COL_7, rowIndex, COL_7);
                                subTotalRange.Style = csSubTotalRow;
                                c1ClaimGrid.SetData(rowIndex, COL_7, dTotal_Charges, true);


                                CellRange subGrandTotal;
                                subGrandTotal = c1ClaimGrid.GetCellRange(rowIndex, COL_6, rowIndex, COL_6);
                                subGrandTotal.Style = csGrandTotalTitleRow;


                                d_Charges = 0;
                                dTotal_Charges = 0;
                                c1ClaimGrid.Rows.Add();
                                rowIndex = c1ClaimGrid.Rows.Count - 1;

                                c1ClaimGrid.SetData(rowIndex, COL_2, "Payment Source");
                                c1ClaimGrid.SetData(rowIndex, COL_3, "Payment Date");
                                c1ClaimGrid.SetData(rowIndex, COL_4, "Payment");
                                c1ClaimGrid.SetData(rowIndex, COL_5, "W/O");
                                c1ClaimGrid.SetData(rowIndex, COL_6, "Withhold");
                                c1ClaimGrid.SetData(rowIndex, COL_7, "Copay", false);
                                c1ClaimGrid.SetData(rowIndex, COL_8, "Co-Ins");
                                c1ClaimGrid.SetData(rowIndex, COL_9, "Ded");
                                c1ClaimGrid.SetData(rowIndex, COL_10, "PaymentTray");

                                subPaymentsource =  c1ClaimGrid.GetCellRange(rowIndex, COL_1, rowIndex, COL_13);
                                subPaymentsource.Style = csPaymentSource;

                                c1ClaimGrid.SetData(rowIndex, COL_11, sLastclaimNo);
                                c1ClaimGrid.SetData(rowIndex, COL_12, _sLastTransactionID);
                                c1ClaimGrid.SetData(rowIndex, COL_14, _sLastTransactionDetailID);



                                #region "Payment   Display Top"

                                //string _LastCPTCode2 = "";
                                //Int64 _LastIsnuranceID = 0;
                                if (dtPaysource != null)
                                {
                                    if (dtPaysource.Rows.Count > 0)
                                    {
                                        InsuranceName = "";
                                        PaymentDate = "";
                                        d_InsurancePayment = 0;
                                        d_ContAdj = 0;
                                        d_Withhold = 0;
                                        d_Copay = 0;
                                        d_CoIns = 0;
                                        d_Deductible = 0;
                                        PaymentTray = "";
                                        dtTemp = new DataTable();
                                        //dtPaysource.DefaultView.Sort = "nInsuraceID"; 
                                        dtTemp = dtPaysource.DefaultView.ToTable(true, new string[1] { "nInsuraceID" });
                                        //string strSortIns = "nInsuraceID ASC";

                                        for (int k = 0; k < dtTemp.Rows.Count; k++)
                                        {
                                            DataView _dvFilteredInsurance = dtPaysource.DefaultView;
                                            _dvFilteredInsurance.RowFilter = "nInsuraceID = " + dtTemp.Rows[k]["nInsuraceID"].ToString();

                                            DataTable _dtFilteredInsurance = _dvFilteredInsurance.ToTable();

                                            //for (int j = 0; j < _dtFilteredInsurance.Rows.Count; j++)
                                            //{
                                            DataTable dtCPT = new DataTable();
                                            dtCPT = _dtFilteredInsurance.DefaultView.ToTable(true, new string[1] { "nBillingTransactionDetailID" });


                                            //dtCPT.DefaultView.Sort = "InsurancePayment DESC";
                                            DataView _dv = _dtFilteredInsurance.DefaultView;

                                            for (int p = 0; p < dtCPT.Rows.Count; p++)
                                            {

                                                //dtPaysource.DefaultView.Table.DefaultView.RowFilter = "sCPTCode=" + dtCPT.Rows[p]["sCPTCode"].ToString();

                                                //_dv.RowFilter = "";
                                                //_dv.Sort = "";
                                                _dv.RowFilter = "nBillingTransactionDetailID = '" + dtCPT.Rows[p]["nBillingTransactionDetailID"].ToString() + "'";
                                                _dv.Sort = "nEOBID ASC";

                                                Decimal _cpt_Payment = 0;
                                                Decimal _cpt_ContAdj = 0;
                                                Decimal _cpt_Withhold = 0;
                                                Decimal _cpt_Copay = 0;
                                                Decimal _cpt_CoIns = 0;
                                                Decimal _cpt_Deductible = 0;
                                                //dtPaysource.DefaultView.Sort = "nEOBID Desc";
                                                for (int x = 0; x < _dv.ToTable().Rows.Count; x++)
                                                {
                                                    _cpt_Payment = Convert.ToDecimal(_dv.ToTable().Rows[x]["InsurancePayment"]) - _cpt_Payment;
                                                    //d_InsurancePayment = d_InsurancePayment + Convert.ToDecimal(_dv.ToTable().Rows[0]["InsurancePayment"]);
                                                    _cpt_ContAdj = Convert.ToDecimal(_dv.ToTable().Rows[x]["ContAdj"]) - _cpt_ContAdj;
                                                    _cpt_Withhold = Convert.ToDecimal(_dv.ToTable().Rows[x]["Withhold"]) - _cpt_Withhold;
                                                    _cpt_Copay = Convert.ToDecimal(_dv.ToTable().Rows[x]["Copay"]) - _cpt_Copay;
                                                    _cpt_CoIns = Convert.ToDecimal(_dv.ToTable().Rows[x]["Coinsurance"]) - _cpt_CoIns;
                                                    _cpt_Deductible = Convert.ToDecimal(_dv.ToTable().Rows[x]["Deductible"]) - _cpt_Deductible;

                                                    d_InsurancePayment = d_InsurancePayment + _cpt_Payment;
                                                    d_ContAdj = d_ContAdj + _cpt_ContAdj;
                                                    d_Withhold = d_Withhold + _cpt_Withhold;
                                                    d_Copay = d_Copay + _cpt_Copay;
                                                    d_CoIns = d_CoIns + _cpt_CoIns;
                                                    d_Deductible = d_Deductible + _cpt_Deductible;

                                                    _cpt_Payment = Convert.ToDecimal(_dv.ToTable().Rows[x]["InsurancePayment"]);
                                                    _cpt_ContAdj = Convert.ToDecimal(_dv.ToTable().Rows[x]["ContAdj"]);
                                                    _cpt_Withhold = Convert.ToDecimal(_dv.ToTable().Rows[x]["Withhold"]);
                                                    _cpt_CoIns = Convert.ToDecimal(_dv.ToTable().Rows[x]["Coinsurance"]);
                                                    _cpt_Deductible = Convert.ToDecimal(_dv.ToTable().Rows[x]["Deductible"]);
                                                    _cpt_Copay = Convert.ToDecimal(_dv.ToTable().Rows[x]["Copay"]);
                                                }
                                                //_cpt_Payment = 0;
                                                //_cpt_ContAdj = 0;
                                                //_cpt_Withhold = 0;
                                                //_cpt_Copay = 0;
                                                //_cpt_CoIns = 0;
                                                //_cpt_Deductible = 0;

                                            }
                                            //d_InsurancePayment = d_InsurancePayment + Convert.ToDecimal(dtPaysource.DefaultView[j]["InsurancePayment"]);
                                            //d_ContAdj = d_ContAdj + Convert.ToDecimal(dtPaysource.DefaultView[j]["ContAdj"]);
                                            //d_Withhold = d_Withhold + Convert.ToDecimal(dtPaysource.DefaultView[j]["Withhold"]);
                                            //d_Copay = d_Copay + Convert.ToDecimal(dtPaysource.DefaultView[j]["Copay"]);
                                            //d_CoIns = d_CoIns + Convert.ToDecimal(dtPaysource.DefaultView[j]["Coinsurance"]);
                                            //d_Deductible = d_Deductible + Convert.ToDecimal(dtPaysource.DefaultView[j]["Deductible"]);



                                            //}
                                            InsuranceName = Convert.ToString(_dtFilteredInsurance.Rows[0]["PaymentInsuranceName"]);
                                            PaymentTray = Convert.ToString(_dtFilteredInsurance.Rows[0]["PaymentTray"]);
                                            PaymentDate = Convert.ToString(_dtFilteredInsurance.Rows[0]["PaymentDate"]);

                                            c1ClaimGrid.Rows.Add();
                                            rowIndex = c1ClaimGrid.Rows.Count - 1;

                                            c1ClaimGrid.SetData(rowIndex, COL_2, InsuranceName);
                                            c1ClaimGrid.SetData(rowIndex, COL_3, PaymentDate);

                                            CrDollars =  c1ClaimGrid.GetCellRange(rowIndex, COL_4, rowIndex, COL_9);
                                            CrDollars.Style = csCurrency;

                                            c1ClaimGrid.SetData(rowIndex, COL_4, d_InsurancePayment, true);
                                            c1ClaimGrid.SetData(rowIndex, COL_5, d_ContAdj, true);
                                            c1ClaimGrid.SetData(rowIndex, COL_6, d_Withhold, true);
                                            c1ClaimGrid.SetData(rowIndex, COL_7, d_Copay, true);
                                            c1ClaimGrid.SetData(rowIndex, COL_8, d_CoIns, true);
                                            c1ClaimGrid.SetData(rowIndex, COL_9, d_Deductible, true);
                                            c1ClaimGrid.SetData(rowIndex, COL_10, PaymentTray, false);

                                            c1ClaimGrid.SetData(rowIndex, COL_11, sLastclaimNo);
                                            c1ClaimGrid.SetData(rowIndex, COL_12, _sLastTransactionID);
                                            c1ClaimGrid.SetData(rowIndex, COL_14, _sLastTransactionDetailID);
                                            InsuranceName = "";
                                            PaymentDate = "";
                                            d_InsurancePayment = 0;
                                            d_ContAdj = 0;
                                            d_Withhold = 0;
                                            d_Copay = 0;
                                            d_CoIns = 0;
                                            d_Deductible = 0;
                                            PaymentTray = "";                                    
                                        }
                                 
                                    }
                                    else
                                    {
                                        c1ClaimGrid.Rows.Add();
                                        rowIndex = c1ClaimGrid.Rows.Count - 1;

                                        c1ClaimGrid.SetData(rowIndex, COL_2, InsuranceName);
                                        c1ClaimGrid.SetData(rowIndex, COL_3, PaymentDate);

                                        CrDollars = c1ClaimGrid.GetCellRange(rowIndex, COL_4, rowIndex, COL_9);
                                        CrDollars.Style = csCurrency;

                                        c1ClaimGrid.SetData(rowIndex, COL_4, d_InsurancePayment, true);
                                        c1ClaimGrid.SetData(rowIndex, COL_5, d_ContAdj, true);
                                        c1ClaimGrid.SetData(rowIndex, COL_6, d_Withhold, true);
                                        c1ClaimGrid.SetData(rowIndex, COL_7, d_Copay, true);
                                        c1ClaimGrid.SetData(rowIndex, COL_8, d_CoIns, true);
                                        c1ClaimGrid.SetData(rowIndex, COL_9, d_Deductible, true);
                                        c1ClaimGrid.SetData(rowIndex, COL_10, PaymentTray, false);

                                        c1ClaimGrid.SetData(rowIndex, COL_11, sLastclaimNo);
                                        c1ClaimGrid.SetData(rowIndex, COL_12, _sLastTransactionID);
                                        c1ClaimGrid.SetData(rowIndex, COL_14, _sLastTransactionDetailID);


                                    }

                                }
                                #endregion
                                InsuranceName = "";
                                PaymentDate = "";
                                d_InsurancePayment = 0;
                                d_ContAdj = 0;
                                d_Withhold = 0;
                                d_Copay = 0;
                                d_CoIns = 0;
                                d_Deductible = 0;
                                PaymentTray = "";


                                c1ClaimGrid.Rows.Add();
                                rowIndex = c1ClaimGrid.Rows.Count - 1;

                                c1ClaimGrid.SetData(rowIndex, COL_11, sLastclaimNo);
                                c1ClaimGrid.SetData(rowIndex, COL_12, _sLastTransactionID);

                                //c1ClaimGrid.Rows.Add();
                                //rowIndex = c1ClaimGrid.Rows.Count - 1;

                                //c1ClaimGrid.SetData(rowIndex, COL_11, sLastclaimNo);
                                //c1ClaimGrid.SetData(rowIndex, COL_12, _sLastTransactionID);

                            }

                            #endregion

                            #region  "Header Details"

                            //c1ClaimGrid.Rows.Add();
                            //rowIndex = c1ClaimGrid.Rows.Count - 1;

                            if (sClaimno != sLastclaimNo)
                            {
                                c1ClaimGrid.Rows.Add();
                                rowIndex = c1ClaimGrid.Rows.Count - 1;

                                c1ClaimGrid.SetData(rowIndex, COL_1, Convert.ToString(dtClaims.Rows[i]["nClaimNumber"]));
                                c1ClaimGrid.SetData(rowIndex, COL_2, Convert.ToString(dtClaims.Rows[i]["nFromDate"]));
                                c1ClaimGrid.SetData(rowIndex, COL_8, Convert.ToString(dtClaims.Rows[i]["ProviderName"]));
                                c1ClaimGrid.SetData(rowIndex, COL_9, Convert.ToString(dtClaims.Rows[i]["ChargesTrayDesc"]));
                                c1ClaimGrid.SetData(rowIndex, COL_10, Convert.ToString(dtClaims.Rows[i]["sIsVoided"]));


                                CellRange crClaimHeader;
                                crClaimHeader = c1ClaimGrid.GetCellRange(rowIndex, COL_1, rowIndex, COL_1);
                                crClaimHeader.Style = csClaimHeader;

                                if (Convert.ToString(dtClaims.Rows[i]["sIsVoided"]) == "Voided")
                                {
                                    CellRange crIsVoided;
                                    crIsVoided = c1ClaimGrid.GetCellRange(rowIndex, COL_10, rowIndex, COL_10);
                                    crIsVoided.Style = csClaimHeader;
                                }

                                #region "Fetch Payment Source"

                                string _LastCPTCode = "";
                                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                                oDBParameters.Add("@ClaimNo", Convert.ToInt64(sClaimno), ParameterDirection.Input, SqlDbType.BigInt);
                                oDB.Retrive("BL_SELECT_EOBPatientLedgerLines", oDBParameters, out  dtPaysource);
                                //oDB.Disconnect();
                                if (dtPaysource != null)
                                {
                                    if (dtPaysource.Rows.Count > 0)
                                    {
                                        for (int j = 0; j < dtPaysource.Rows.Count; j++)
                                        {

                                            d_InsurancePayment = d_InsurancePayment + Convert.ToDecimal(dtPaysource.Rows[j]["InsurancePayment"]);

                                            if (Convert.ToString(dtPaysource.Rows[j]["sCPTCode"]) != _LastCPTCode)
                                            {
                                                d_ContAdj = d_ContAdj + Convert.ToDecimal(dtPaysource.Rows[j]["ContAdj"]);
                                            }
                                            d_Withhold = d_Withhold + Convert.ToDecimal(dtPaysource.Rows[j]["Withhold"]);
                                            d_Copay = d_Copay + Convert.ToDecimal(dtPaysource.Rows[j]["Copay"]);
                                            d_CoIns = d_CoIns + Convert.ToDecimal(dtPaysource.Rows[j]["Coinsurance"]);
                                            d_Deductible = d_Deductible + Convert.ToDecimal(dtPaysource.Rows[j]["Deductible"]);



                                            InsuranceName = Convert.ToString(dtPaysource.Rows[j]["PaymentInsuranceName"]);
                                            PaymentTray = Convert.ToString(dtPaysource.Rows[j]["PaymentTray"]);
                                            PaymentDate = Convert.ToString(dtPaysource.Rows[j]["PaymentDate"]);

                                            DateTime dtDOS = Convert.ToDateTime(dtPaysource.Rows[j]["DOS"]);

                                            if (Convert.ToString(dtClaims.Rows[i]["sIsVoided"]) != "Voided")
                                            {
                                                if (dtDOS <= DateTime.Now.Date && dtDOS >= DateTime.Now.Subtract(new TimeSpan(30, 0, 0, 0)))
                                                {
                                                    _30DayesPaid += Convert.ToDecimal(dtPaysource.Rows[j]["InsurancePayment"]) + Convert.ToDecimal(dtPaysource.Rows[j]["ContAdj"]) + Convert.ToDecimal(dtPaysource.Rows[j]["Withhold"]);
                                                    //_30DayesPaid += Convert.ToDecimal(dtPaysource.Rows[j]["InsurancePayment"]) + Convert.ToDecimal(d_ContAdj) + Convert.ToDecimal(dtPaysource.Rows[j]["Withhold"]);

                                                }
                                                else if (dtDOS < DateTime.Now.Subtract(new TimeSpan(30, 0, 0, 0)) && dtDOS >= DateTime.Now.Subtract(new TimeSpan(60, 0, 0, 0)))
                                                {
                                                    _60DayesPaid += Convert.ToDecimal(dtPaysource.Rows[j]["InsurancePayment"]) + Convert.ToDecimal(dtPaysource.Rows[j]["ContAdj"]) + Convert.ToDecimal(dtPaysource.Rows[j]["Withhold"]);
                                                    //_60DayesPaid += Convert.ToDecimal(dtPaysource.Rows[j]["InsurancePayment"]) + Convert.ToDecimal(d_ContAdj) + Convert.ToDecimal(dtPaysource.Rows[j]["Withhold"]);
                                                }
                                                else if (dtDOS < DateTime.Now.Subtract(new TimeSpan(60, 0, 0, 0)) && dtDOS >= DateTime.Now.Subtract(new TimeSpan(90, 0, 0, 0)))
                                                {
                                                    _90DayesPaid += Convert.ToDecimal(dtPaysource.Rows[j]["InsurancePayment"]) + Convert.ToDecimal(dtPaysource.Rows[j]["ContAdj"]) + Convert.ToDecimal(dtPaysource.Rows[j]["Withhold"]);
                                                    //_90DayesPaid += Convert.ToDecimal(dtPaysource.Rows[j]["InsurancePayment"]) + Convert.ToDecimal(d_ContAdj) + Convert.ToDecimal(dtPaysource.Rows[j]["Withhold"]);
                                                }
                                                else if (dtDOS < DateTime.Now.Subtract(new TimeSpan(90, 0, 0, 0)) && dtDOS >= DateTime.Now.Subtract(new TimeSpan(120, 0, 0, 0)))
                                                {
                                                    _120DayesPaid += Convert.ToDecimal(dtPaysource.Rows[j]["InsurancePayment"]) + Convert.ToDecimal(dtPaysource.Rows[j]["ContAdj"]) + Convert.ToDecimal(dtPaysource.Rows[j]["Withhold"]);
                                                    //_120DayesPaid += Convert.ToDecimal(dtPaysource.Rows[j]["InsurancePayment"]) + Convert.ToDecimal(d_ContAdj) + Convert.ToDecimal(dtPaysource.Rows[j]["Withhold"]);
                                                }
                                                else if (dtDOS <= DateTime.Now.Subtract(new TimeSpan(120, 0, 0, 0)))
                                                {
                                                    _Above120DayesPaid += Convert.ToDecimal(dtPaysource.Rows[j]["InsurancePayment"]) + Convert.ToDecimal(dtPaysource.Rows[j]["ContAdj"]) + Convert.ToDecimal(dtPaysource.Rows[j]["Withhold"]);
                                                    //_Above120DayesPaid += Convert.ToDecimal(dtPaysource.Rows[j]["InsurancePayment"]) + Convert.ToDecimal(d_ContAdj) + Convert.ToDecimal(dtPaysource.Rows[j]["Withhold"]);
                                                }
                                            }
                                            //if (Convert.ToString(dtPaysource.Rows[j]["sCPTCode"]) != _LastCPTCode)
                                            //{
                                            //    d_ContAdj = d_ContAdj + Convert.ToDecimal(dtPaysource.Rows[j]["ContAdj"]);
                                            //}
                                            _LastCPTCode = Convert.ToString(dtPaysource.Rows[j]["sCPTCode"]);
                                        }
                                    }
                                }

                                #endregion

                            }

                            //d_Charges = d_Charges + Convert.ToDecimal(dtClaims.Rows[i]["dTotal"]);


                            #region "Balance"


                            if (Convert.ToString(dtClaims.Rows[i]["sIsVoided"]) != "Voided")
                            {
                                DateTime dtTransdate = Convert.ToDateTime(dtClaims.Rows[i]["nFromDate"]);

                                if (dtTransdate <= DateTime.Now.Date && dtTransdate >= DateTime.Now.Subtract(new TimeSpan(30, 0, 0, 0)))
                                {
                                    _30DaysCharges += Convert.ToDecimal(dtClaims.Rows[i]["dTotal"]);
                                }
                                else if (dtTransdate < DateTime.Now.Subtract(new TimeSpan(30, 0, 0, 0)) && dtTransdate >= DateTime.Now.Subtract(new TimeSpan(60, 0, 0, 0)))
                                {
                                    _60DaysCharges += Convert.ToDecimal(dtClaims.Rows[i]["dTotal"]);
                                }
                                else if (dtTransdate < DateTime.Now.Subtract(new TimeSpan(60, 0, 0, 0)) && dtTransdate >= DateTime.Now.Subtract(new TimeSpan(90, 0, 0, 0)))
                                {
                                    _90DaysCharges += Convert.ToDecimal(dtClaims.Rows[i]["dTotal"]);
                                }
                                else if (dtTransdate < DateTime.Now.Subtract(new TimeSpan(90, 0, 0, 0)) && dtTransdate >= DateTime.Now.Subtract(new TimeSpan(120, 0, 0, 0)))
                                {
                                    _120DaysCharges += Convert.ToDecimal(dtClaims.Rows[i]["dTotal"]);
                                }
                                else if (dtTransdate <= DateTime.Now.Subtract(new TimeSpan(120, 0, 0, 0)))
                                {
                                    _Above120DaysCharges += Convert.ToDecimal(dtClaims.Rows[i]["dTotal"]);
                                }
                            }

                            #endregion
                                                    

                            


                            if (sClaimno != sLastclaimNo)
                            {

                                c1ClaimGrid.SetData(rowIndex, COL_3, Convert.ToString(dtClaims.Rows[i]["sCPTCode"]));
                                c1ClaimGrid.SetData(rowIndex, COL_4, Convert.ToString(dtClaims.Rows[i]["sMod1Code"]));
                                c1ClaimGrid.SetData(rowIndex, COL_5, Convert.ToString(dtClaims.Rows[i]["sMod2Code"]));
                                c1ClaimGrid.SetData(rowIndex, COL_6, Convert.ToDecimal(dtClaims.Rows[i]["dUnit"]).ToString("#0.###############"));

                                CellRange CrDollars2 =  c1ClaimGrid.GetCellRange(rowIndex, COL_7, rowIndex, COL_7);
                                CrDollars2.Style = csCurrency;

                                c1ClaimGrid.SetData(rowIndex, COL_7, Convert.ToString(dtClaims.Rows[i]["dTotal"]), true);

                                d_Charges = Convert.ToDecimal(dtClaims.Rows[i]["dTotal"].ToString());
                                dTotal_Charges =  Convert.ToDecimal(dtClaims.Rows[i]["dTotal"].ToString());
                                c1ClaimGrid.SetData(rowIndex, COL_11, Convert.ToString(dtClaims.Rows[i]["nClaimNumber"]));
                                c1ClaimGrid.SetData(rowIndex, COL_12, Convert.ToString(dtClaims.Rows[i]["nTransactionID"]));

                                c1ClaimGrid.SetData(rowIndex, COL_13, GetNextResponsibility(_sTransactionID, _sTransactionDetailID));
                                c1ClaimGrid.SetData(rowIndex, COL_14, Convert.ToString(dtClaims.Rows[i]["nTransactionDetailID"]));

                                //c1ClaimGrid.Cols[COL_7].Style = csCurrency;
                            }
                            else
                            {
                                //if (sCPT != sLastCPT)
                                //{
                                    c1ClaimGrid.Rows.Add();
                                    rowIndex = c1ClaimGrid.Rows.Count - 1;
                                    c1ClaimGrid.SetData(rowIndex, COL_3, Convert.ToString(dtClaims.Rows[i]["sCPTCode"]));
                                    c1ClaimGrid.SetData(rowIndex, COL_4, Convert.ToString(dtClaims.Rows[i]["sMod1Code"]));
                                    c1ClaimGrid.SetData(rowIndex, COL_5, Convert.ToString(dtClaims.Rows[i]["sMod2Code"]));
                                    c1ClaimGrid.SetData(rowIndex, COL_6, Convert.ToDecimal(dtClaims.Rows[i]["dUnit"]).ToString("#0.###############"));

                                    CellRange CrDollars2 =  c1ClaimGrid.GetCellRange(rowIndex, COL_7, rowIndex, COL_7);
                                    CrDollars2.Style = csCurrency;

                                    c1ClaimGrid.SetData(rowIndex, COL_7, Convert.ToString(dtClaims.Rows[i]["dTotal"]), true);

                                    d_Charges = Convert.ToDecimal(dtClaims.Rows[i]["dTotal"].ToString());
                                    dTotal_Charges = dTotal_Charges + d_Charges;
                                    c1ClaimGrid.SetData(rowIndex, COL_11, Convert.ToString(dtClaims.Rows[i]["nClaimNumber"]));
                                    c1ClaimGrid.SetData(rowIndex, COL_12, Convert.ToString(dtClaims.Rows[i]["nTransactionID"]));
                                    c1ClaimGrid.SetData(rowIndex, COL_13, GetNextResponsibility(_sTransactionID, _sTransactionDetailID));
                                    c1ClaimGrid.SetData(rowIndex, COL_14, Convert.ToString(dtClaims.Rows[i]["nTransactionDetailID"]));
                          
                                //}
                                //else
                                //{
                                //    d_Charges = d_Charges + Convert.ToDecimal(dtClaims.Rows[i]["dTotal"]);
                                //    dTotal_Charges = dTotal_Charges + Convert.ToDecimal(dtClaims.Rows[i]["dTotal"]);
                                //    c1ClaimGrid.SetData(rowIndex, COL_7, d_Charges, true);
                                //    c1ClaimGrid.SetData(rowIndex, COL_11, Convert.ToString(dtClaims.Rows[i]["nClaimNumber"]));
                                //    c1ClaimGrid.SetData(rowIndex, COL_12, Convert.ToString(dtClaims.Rows[i]["nTransactionID"]));
                                //    c1ClaimGrid.SetData(rowIndex, COL_13, GetNextResponsibility(_sTransactionID, _sTransactionDetailID));
                                    
                                //}
                            }
                         
                            //c1ClaimGrid.SetData(rowIndex, COL_7, Convert.ToString(dTotal_Charges + Convert.ToDecimal(dtClaims.Rows[i]["dTotal"])), true);
                            //c1ClaimGrid.SetData(rowIndex, COL_11, Convert.ToString(dtClaims.Rows[i]["nClaimNumber"]));
                            //c1ClaimGrid.SetData(rowIndex, COL_12, Convert.ToString(dtClaims.Rows[i]["nTransactionID"]));


                            //c1ClaimGrid.SetData(rowIndex, COL_13, GetNextResponsibility(_sTransactionID, _sTransactionDetailID));

                            //c1ClaimGrid.SetData(rowIndex, COL_7, Convert.ToString(Convert.ToDecimal(dtClaims.Rows[i]["dTotal"])), true);



                            //if (i != dtClaims.Rows.Count - 1)
                            //{
                            //    c1ClaimGrid.Rows.Add();
                            //    rowIndex = c1ClaimGrid.Rows.Count - 1;
                            //}
                            //}
                            //else
                            //{
                            //    if (_sTransactionDetailID == _sLastTransactionDetailID)
                            //    {
                            //        c1ClaimGrid.SetData(rowIndex-1 , COL_7, Convert.ToString(dTotal_Charges + Convert.ToDecimal(dtClaims.Rows[i]["dTotal"])), true);
                            //        //d_Charges = d_Charges + Convert.ToDecimal(dtClaims.Rows[i]["dTotal"]);
                            //    }
                            //    else
                            //    {

                            //    }
                            //}

                            sLastCPT = dtClaims.Rows[i]["sCPTCode"].ToString();
                            sLastclaimNo = dtClaims.Rows[i]["nClaimNumber"].ToString();
                            _sLastTransactionID = dtClaims.Rows[i]["nTransactionID"].ToString();
                            _sLastTransactionDetailID = dtClaims.Rows[i]["nTransactionDetailID"].ToString();
                            //dTotal_Charges = d_Charges; 
                            //c1ClaimGrid.Rows.Add();
                            //rowIndex = c1ClaimGrid.Rows.Count - 1;



                            #endregion

                        }

                        #region "Sum of Last claim"

                        c1ClaimGrid.Rows.Add();
                        rowIndex = c1ClaimGrid.Rows.Count - 1;

                        CellRange subGrandTotal2;
                        subGrandTotal2 = c1ClaimGrid.GetCellRange(rowIndex, COL_6, rowIndex, COL_6);
                        subGrandTotal2.Style = csGrandTotalTitleRow;
                        c1ClaimGrid.SetData(rowIndex, COL_6, "Total: ", true);


                        CellRange subTotalRangeLast;
                        subTotalRangeLast = c1ClaimGrid.GetCellRange(rowIndex, COL_7, rowIndex, COL_7);
                        subTotalRangeLast.Style = csSubTotalRow;
                        c1ClaimGrid.SetData(rowIndex, COL_7, dTotal_Charges, true);

                        c1ClaimGrid.SetData(rowIndex, COL_11, sClaimno);
                        c1ClaimGrid.SetData(rowIndex, COL_12, _sTransactionID);


                        //d_Charges = 0;


                        c1ClaimGrid.Rows.Add();
                        rowIndex = c1ClaimGrid.Rows.Count - 1;

                        c1ClaimGrid.SetData(rowIndex, COL_2, "Payment Source");
                        c1ClaimGrid.SetData(rowIndex, COL_3, "Payment Date");
                        c1ClaimGrid.SetData(rowIndex, COL_4, "Payment");
                        c1ClaimGrid.SetData(rowIndex, COL_5, "W/O");
                        c1ClaimGrid.SetData(rowIndex, COL_6, "Withhold");
                        c1ClaimGrid.SetData(rowIndex, COL_7, "Copay");
                        c1ClaimGrid.SetData(rowIndex, COL_8, "Co-Ins");
                        c1ClaimGrid.SetData(rowIndex, COL_9, "Ded");
                        c1ClaimGrid.SetData(rowIndex, COL_10, "PaymentTray");

                        subPaymentsource =  c1ClaimGrid.GetCellRange(rowIndex, COL_1, rowIndex, COL_13);
                        subPaymentsource.Style = csPaymentSource;

                        c1ClaimGrid.SetData(rowIndex, COL_11, sClaimno);
                        c1ClaimGrid.SetData(rowIndex, COL_12, _sTransactionID);

                        #region "Payment   Display Bottom"
                         //   string _LastCPTCode3 = "";
                         //   Int64 _LastIsnuranceID2 = 0;

                            if (dtPaysource != null)
                            {
                                if (dtPaysource.Rows.Count > 0)
                                {
                                            InsuranceName = "";
                                            PaymentDate = "";
                                            d_InsurancePayment = 0;
                                            d_ContAdj = 0;
                                            d_Withhold = 0;
                                            d_Copay = 0;
                                            d_CoIns = 0;
                                            d_Deductible = 0;
                                            PaymentTray = "";
                                            dtTemp = new DataTable();
                                            //dtPaysource.DefaultView.Sort = "nInsuraceID"; 
                                            dtTemp = dtPaysource.DefaultView.ToTable(true, new string[1] { "nInsuraceID" });
                                            //string strSortIns = "nInsuraceID ASC";

                                            for (int k = 0; k < dtTemp.Rows.Count; k++)
                                            {
                                                DataView _dvFilteredInsurance = dtPaysource.DefaultView;
                                                _dvFilteredInsurance.RowFilter = "nInsuraceID = " + dtTemp.Rows[k]["nInsuraceID"].ToString();

                                                DataTable _dtFilteredInsurance = _dvFilteredInsurance.ToTable();

                                                //for (int j = 0; j < _dtFilteredInsurance.Rows.Count; j++)
                                                //{
                                                DataTable dtCPT = new DataTable();
                                                dtCPT = _dtFilteredInsurance.DefaultView.ToTable(true, new string[1] { "nBillingTransactionDetailID" });


                                                //dtCPT.DefaultView.Sort = "InsurancePayment DESC";
                                                DataView _dv = _dtFilteredInsurance.DefaultView;

                                                for (int p = 0; p < dtCPT.Rows.Count; p++)
                                                {
                                                    
                                                    _dv.RowFilter = "nBillingTransactionDetailID = '" + dtCPT.Rows[p]["nBillingTransactionDetailID"].ToString() + "'";
                                                    _dv.Sort = "nEOBID ASC";

                                                    Decimal _cpt_Payment = 0;
                                                    Decimal _cpt_ContAdj = 0;
                                                    Decimal _cpt_Withhold = 0;
                                                    Decimal _cpt_Copay = 0;
                                                    Decimal _cpt_CoIns = 0;
                                                    Decimal _cpt_Deductible = 0;
                                                    //dtPaysource.DefaultView.Sort = "nEOBID Desc";
                                                    for (int x = 0; x < _dv.ToTable().Rows.Count; x++)
                                                    {
                                                        _cpt_Payment = Convert.ToDecimal(_dv.ToTable().Rows[x]["InsurancePayment"]) - _cpt_Payment;
                                                        //d_InsurancePayment = d_InsurancePayment + Convert.ToDecimal(_dv.ToTable().Rows[0]["InsurancePayment"]);
                                                        _cpt_ContAdj = Convert.ToDecimal(_dv.ToTable().Rows[x]["ContAdj"]) - _cpt_ContAdj;
                                                        _cpt_Withhold = Convert.ToDecimal(_dv.ToTable().Rows[x]["Withhold"]) - _cpt_Withhold;
                                                        _cpt_Copay = Convert.ToDecimal(_dv.ToTable().Rows[x]["Copay"]) - _cpt_Copay;
                                                        _cpt_CoIns = Convert.ToDecimal(_dv.ToTable().Rows[x]["Coinsurance"]) - _cpt_CoIns;
                                                        _cpt_Deductible = Convert.ToDecimal(_dv.ToTable().Rows[x]["Deductible"]) - _cpt_Deductible;

                                                        d_InsurancePayment = d_InsurancePayment + _cpt_Payment;
                                                        d_ContAdj = d_ContAdj + _cpt_ContAdj;
                                                        d_Withhold = d_Withhold + _cpt_Withhold;
                                                        d_Copay = d_Copay + _cpt_Copay;
                                                        d_CoIns = d_CoIns + _cpt_CoIns;
                                                        d_Deductible = d_Deductible + _cpt_Deductible;

                                                        _cpt_Payment = Convert.ToDecimal(_dv.ToTable().Rows[x]["InsurancePayment"]);
                                                        _cpt_ContAdj = Convert.ToDecimal(_dv.ToTable().Rows[x]["ContAdj"]);
                                                        _cpt_Withhold = Convert.ToDecimal(_dv.ToTable().Rows[x]["Withhold"]);
                                                        _cpt_CoIns = Convert.ToDecimal(_dv.ToTable().Rows[x]["Coinsurance"]);
                                                        _cpt_Deductible = Convert.ToDecimal(_dv.ToTable().Rows[x]["Deductible"]);
                                                        _cpt_Copay = Convert.ToDecimal(_dv.ToTable().Rows[x]["Copay"]);
                                                    }
                                                }
                                            
                                                InsuranceName = Convert.ToString(_dtFilteredInsurance.Rows[0]["PaymentInsuranceName"]);
                                                PaymentTray = Convert.ToString(_dtFilteredInsurance.Rows[0]["PaymentTray"]);
                                                PaymentDate = Convert.ToString(_dtFilteredInsurance.Rows[0]["PaymentDate"]);

                                                c1ClaimGrid.Rows.Add();
                                                rowIndex = c1ClaimGrid.Rows.Count - 1;

                                                c1ClaimGrid.SetData(rowIndex, COL_2, InsuranceName);
                                                c1ClaimGrid.SetData(rowIndex, COL_3, PaymentDate);

                                                CrDollars = c1ClaimGrid.GetCellRange(rowIndex, COL_4, rowIndex, COL_9);
                                                CrDollars.Style = csCurrency;

                                                c1ClaimGrid.SetData(rowIndex, COL_4, d_InsurancePayment, true);
                                                c1ClaimGrid.SetData(rowIndex, COL_5, d_ContAdj, true);
                                                c1ClaimGrid.SetData(rowIndex, COL_6, d_Withhold, true);
                                                c1ClaimGrid.SetData(rowIndex, COL_7, d_Copay, true);
                                                c1ClaimGrid.SetData(rowIndex, COL_8, d_CoIns, true);
                                                c1ClaimGrid.SetData(rowIndex, COL_9, d_Deductible, true);
                                                c1ClaimGrid.SetData(rowIndex, COL_10, PaymentTray, false);

                                                c1ClaimGrid.SetData(rowIndex, COL_11, sLastclaimNo);
                                                c1ClaimGrid.SetData(rowIndex, COL_12, _sLastTransactionID);

                                                InsuranceName = "";
                                                PaymentDate = "";
                                                d_InsurancePayment = 0;
                                                d_ContAdj = 0;
                                                d_Withhold = 0;
                                                d_Copay = 0;
                                                d_CoIns = 0;
                                                d_Deductible = 0;
                                                PaymentTray = "";
                                            }
                                }
                                else
                                {
                                    c1ClaimGrid.Rows.Add();
                                    rowIndex = c1ClaimGrid.Rows.Count - 1;

                                    InsuranceName = "";
                                    PaymentDate = "";
                                    d_InsurancePayment = 0;
                                    d_ContAdj = 0;
                                    d_Withhold = 0;
                                    d_Copay = 0;
                                    d_CoIns = 0;
                                    d_Deductible = 0;
                                    PaymentTray = "";
                                    PaymentDate = "";

                                    c1ClaimGrid.SetData(rowIndex, COL_2, InsuranceName);
                                    c1ClaimGrid.SetData(rowIndex, COL_3, PaymentDate);

                                    CrDollars =  c1ClaimGrid.GetCellRange(rowIndex, COL_4, rowIndex, COL_9);
                                    CrDollars.Style = csCurrency;

                                    c1ClaimGrid.SetData(rowIndex, COL_4, d_InsurancePayment, true);
                                    c1ClaimGrid.SetData(rowIndex, COL_5, d_ContAdj, true);
                                    c1ClaimGrid.SetData(rowIndex, COL_6, d_Withhold, true);
                                    c1ClaimGrid.SetData(rowIndex, COL_7, d_Copay, true);
                                    c1ClaimGrid.SetData(rowIndex, COL_8, d_CoIns, true);
                                    c1ClaimGrid.SetData(rowIndex, COL_9, d_Deductible, true);
                                    c1ClaimGrid.SetData(rowIndex, COL_10, PaymentTray, false);

                                    c1ClaimGrid.SetData(rowIndex, COL_11, sLastclaimNo);
                                    c1ClaimGrid.SetData(rowIndex, COL_12, _sLastTransactionID);

                                }

                            }
                            else
                            {
                                c1ClaimGrid.Rows.Add();
                                rowIndex = c1ClaimGrid.Rows.Count - 1;

                                c1ClaimGrid.SetData(rowIndex, COL_2, InsuranceName);
                                c1ClaimGrid.SetData(rowIndex, COL_3, PaymentDate);

                                CrDollars = c1ClaimGrid.GetCellRange(rowIndex, COL_4, rowIndex, COL_9);
                                CrDollars.Style = csCurrency;

                                c1ClaimGrid.SetData(rowIndex, COL_4, d_InsurancePayment, true);
                                c1ClaimGrid.SetData(rowIndex, COL_5, d_ContAdj, true);
                                c1ClaimGrid.SetData(rowIndex, COL_6, d_Withhold, true);
                                c1ClaimGrid.SetData(rowIndex, COL_7, d_Copay, true);
                                c1ClaimGrid.SetData(rowIndex, COL_8, d_CoIns, true);
                                c1ClaimGrid.SetData(rowIndex, COL_9, d_Deductible, true);
                                c1ClaimGrid.SetData(rowIndex, COL_10, PaymentTray, false);

                                c1ClaimGrid.SetData(rowIndex, COL_11, sLastclaimNo);
                                c1ClaimGrid.SetData(rowIndex, COL_12, _sLastTransactionID);


                                InsuranceName = "";
                                PaymentDate = "";
                                d_InsurancePayment = 0;
                                d_ContAdj = 0;
                                d_Withhold = 0;
                                d_Copay = 0;
                                d_CoIns = 0;
                                d_Deductible = 0;
                                PaymentTray = "";

                            }
                        }
                        #endregion




                        //c1ClaimGrid.Rows.Add();
                        //rowIndex = c1ClaimGrid.Rows.Count - 1;


                        #region "Advance Payment"

                        FetchAdvancePayment(c1ClaimGrid, 1);

                        #endregion


                        #region "Patient Balance"

                        _30DaysBalanceDue = _30DaysCharges - (_30DayesPaid);
                        _60DaysBalanceDue = _60DaysCharges - (_60DayesPaid);
                        _90DaysBalanceDue = _90DaysCharges - (_90DayesPaid);
                        _120DaysBalanceDue = _120DaysCharges - (_120DayesPaid);
                        _Above120DaysBalanceDue = _Above120DaysCharges - (_Above120DayesPaid);


                        #region " Commented Aging Buckets "

                        //int _rowIndex = 0;

                        //C1PatientDetails.Rows.Add();
                        //_rowIndex = C1PatientDetails.Rows.Count - 1;


                        //C1PatientDetails.SetData(_rowIndex, COLPAT_1, "31-60");
                        //C1PatientDetails.SetData(_rowIndex, COLPAT_2, _30DaysBalanceDue);
                        //C1PatientDetails.Rows.Add();
                        //_rowIndex = C1PatientDetails.Rows.Count - 1;

                        //C1PatientDetails.SetData(_rowIndex, COLPAT_1, "61-90");
                        //C1PatientDetails.SetData(_rowIndex, COLPAT_2, _60DaysBalanceDue);
                        //C1PatientDetails.Rows.Add();
                        //_rowIndex = C1PatientDetails.Rows.Count - 1;


                        //C1PatientDetails.SetData(_rowIndex, COLPAT_1, "91-120");
                        //C1PatientDetails.SetData(_rowIndex, COLPAT_2, _90DaysBalanceDue);
                        //C1PatientDetails.Rows.Add();
                        //_rowIndex = C1PatientDetails.Rows.Count - 1;


                        //C1PatientDetails.SetData(_rowIndex, COLPAT_1, "121-150");
                        //C1PatientDetails.SetData(_rowIndex, COLPAT_2, _120DaysBalanceDue);
                        //C1PatientDetails.Rows.Add();
                        //_rowIndex = C1PatientDetails.Rows.Count - 1;


                        //C1PatientDetails.SetData(_rowIndex, COLPAT_1, ">150");
                        //C1PatientDetails.SetData(_rowIndex, COLPAT_2, _Above120DaysBalanceDue);
                        //C1PatientDetails.Rows.Add();
                        //_rowIndex = C1PatientDetails.Rows.Count - 1;


                        //C1PatientDetails.SetData(_rowIndex, COLPAT_1, "Total :");
                        //C1PatientDetails.SetData(_rowIndex, COLPAT_2, _30DaysBalanceDue + _60DaysBalanceDue + _90DaysBalanceDue + _120DaysBalanceDue + _Above120DaysBalanceDue);



                        //CellRange subPatTotalCaption;
                        //subPatTotalCaption = C1PatientDetails.GetCellRange(_rowIndex, COL_1, _rowIndex, COL_1);
                        //subPatTotalCaption.Style = csGrandTotalTitleRow;


                        //CellRange subPatTot;
                        //subPatTot = C1PatientDetails.GetCellRange(_rowIndex, COL_2, _rowIndex, COL_2);
                        //subPatTot.Style = csSubTotalRow;
                        #endregion

                        FillPatientBalance(_PatientID); 

                        _30DaysBalanceDue = 0;
                        _60DaysBalanceDue = 0;
                        _90DaysBalanceDue = 0;
                        _120DaysBalanceDue = 0;
                        _Above120DaysBalanceDue = 0;
                        _30DaysCharges = 0;
                        _30DayesPaid = 0;
                        _60DaysCharges = 0;
                        _60DayesPaid = 0;
                        _90DaysCharges = 0;
                        _90DayesPaid = 0;
                        _120DaysCharges = 0;
                        _120DayesPaid = 0;
                        _Above120DaysCharges = 0;
                        _Above120DayesPaid = 0;


                        #endregion


                        #endregion

                        if (oDB != null) { oDB.Dispose(); };


                    }
            }            
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null; 
            }
            finally
            {
                C1PatientDetails.ScrollBars = ScrollBars.Both;
                c1ClaimGrid.ScrollBars = ScrollBars.Both;
            }

        }
        //private void FillClaimsNew()
        //{
        //    //gloClaimManager ogloClaimManager = new gloClaimManager(_databaseconnectionstring, "");
        //    DataTable dtClaims = null;
        //    String sClaimno = "";
        //    String sLastclaimNo = "";
        //    int rowIndex = 0;
        //    DataTable dtPaysource = null;
        //    string _sLastTransactionID = "";
        //    string _sTransactionID = "";

        //    try
        //    {
        //        DesignChargesGrid(c1ClaimGrid);


        //        if (c1ClaimGrid != null)
        //        {

        //            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //            oDB.Connect(false);

        //            gloDatabaseLayer.DBParameters oDBParameters1 = new gloDatabaseLayer.DBParameters();
        //            oDBParameters1.Add("@nPatientID", Convert.ToInt64(_PatientID), ParameterDirection.Input, SqlDbType.BigInt);
        //            oDBParameters1.Add("@nClaimNo", Convert.ToInt64(0), ParameterDirection.Input, SqlDbType.BigInt);
        //            oDB.Retrive("BL_SELECT_Tracking_PatientLedger", oDBParameters1, out  dtClaims);


        //            Decimal _30DaysBalanceDue = 0;
        //            Decimal _30DaysCharges = 0;
        //            Decimal _30DayesPaid = 0;

        //            Decimal _60DaysBalanceDue = 0;
        //            Decimal _60DaysCharges = 0;
        //            Decimal _60DayesPaid = 0;

        //            Decimal _90DaysBalanceDue = 0;
        //            Decimal _90DaysCharges = 0;
        //            Decimal _90DayesPaid = 0;

        //            Decimal _120DaysBalanceDue = 0;
        //            Decimal _120DaysCharges = 0;
        //            Decimal _120DayesPaid = 0;

        //            Decimal _Above120DaysBalanceDue = 0;
        //            Decimal _Above120DaysCharges = 0;
        //            Decimal _Above120DayesPaid = 0;

        //            dTotal_Charges = 0;
        //            dTotal_Allowed = 0;
        //            dTotal_ContAdj = 0;
        //            dTotal_InsurancePayment = 0;
        //            dTotal_WithHold = 0;
        //            dTotal_PatientPayment = 0;

        //            if (dtClaims != null && dtClaims.Rows.Count > 0)
        //            {
        //                for (int i = 0; i < dtClaims.Rows.Count; i++)
        //                {
        //                    sClaimno = dtClaims.Rows[i]["nClaimNumber"].ToString();
        //                    _sTransactionID = dtClaims.Rows[i]["nTransactionID"].ToString();

        //                    #region "Sum of claim"

        //                    if (i > 0 && sLastclaimNo != sClaimno)
        //                    {
        //                        c1ClaimGrid.Rows.Add();
        //                        rowIndex = c1ClaimGrid.Rows.Count - 1;

        //                        //c1ClaimGrid.SetData(rowIndex, COL_6, "Total: ");
        //                        c1ClaimGrid.SetData(rowIndex, COL_6, "Total: ", false);
        //                        c1ClaimGrid.SetData(rowIndex, COL_11, sLastclaimNo);
        //                        c1ClaimGrid.SetData(rowIndex, COL_12, _sLastTransactionID);

        //                        CellRange subTotalRange;
        //                        subTotalRange = c1ClaimGrid.GetCellRange(rowIndex, COL_7, rowIndex, COL_7);
        //                        subTotalRange.Style = csSubTotalRow;
        //                        c1ClaimGrid.SetData(rowIndex, COL_7, d_Charges, true);


        //                        CellRange subGrandTotal;
        //                        subGrandTotal = c1ClaimGrid.GetCellRange(rowIndex, COL_6, rowIndex, COL_6);
        //                        subGrandTotal.Style = csGrandTotalTitleRow;


        //                        d_Charges = 0;

        //                        c1ClaimGrid.Rows.Add();
        //                        rowIndex = c1ClaimGrid.Rows.Count - 1;

        //                        c1ClaimGrid.SetData(rowIndex, COL_2, "Payment Source");
        //                        c1ClaimGrid.SetData(rowIndex, COL_3, "Payment Date");
        //                        c1ClaimGrid.SetData(rowIndex, COL_4, "Payment");
        //                        c1ClaimGrid.SetData(rowIndex, COL_5, "W/O");
        //                        c1ClaimGrid.SetData(rowIndex, COL_6, "Withhold");
        //                        c1ClaimGrid.SetData(rowIndex, COL_7, "Copay", false);
        //                        c1ClaimGrid.SetData(rowIndex, COL_8, "Co-Ins");
        //                        c1ClaimGrid.SetData(rowIndex, COL_9, "Ded");
        //                        c1ClaimGrid.SetData(rowIndex, COL_10, "PaymentTray");

        //                        subPaymentsource = new CellRange();
        //                        subPaymentsource = c1ClaimGrid.GetCellRange(rowIndex, COL_1, rowIndex, COL_10);
        //                        subPaymentsource.Style = csPaymentSource;

        //                        c1ClaimGrid.SetData(rowIndex, COL_11, sLastclaimNo);
        //                        c1ClaimGrid.SetData(rowIndex, COL_12, _sLastTransactionID);



        //                        c1ClaimGrid.Rows.Add();
        //                        rowIndex = c1ClaimGrid.Rows.Count - 1;

        //                        c1ClaimGrid.SetData(rowIndex, COL_2, InsuranceName);
        //                        c1ClaimGrid.SetData(rowIndex, COL_3, PaymentDate);

        //                        CrDollars = new CellRange();
        //                        CrDollars = c1ClaimGrid.GetCellRange(rowIndex, COL_4, rowIndex, COL_9);
        //                        CrDollars.Style = csCurrency;

        //                        c1ClaimGrid.SetData(rowIndex, COL_4, d_InsurancePayment, true);
        //                        c1ClaimGrid.SetData(rowIndex, COL_5, d_ContAdj, true);
        //                        c1ClaimGrid.SetData(rowIndex, COL_6, d_Withhold, true);
        //                        c1ClaimGrid.SetData(rowIndex, COL_7, d_Copay, true);
        //                        c1ClaimGrid.SetData(rowIndex, COL_8, d_CoIns, true);
        //                        c1ClaimGrid.SetData(rowIndex, COL_9, d_Deductible, true);
        //                        c1ClaimGrid.SetData(rowIndex, COL_10, PaymentTray, false);

        //                        c1ClaimGrid.SetData(rowIndex, COL_11, sLastclaimNo, false);
        //                        c1ClaimGrid.SetData(rowIndex, COL_12, _sLastTransactionID, false);

        //                        InsuranceName = "";
        //                        PaymentDate = "";
        //                        d_InsurancePayment = 0;
        //                        d_ContAdj = 0;
        //                        d_Withhold = 0;
        //                        d_Copay = 0;
        //                        d_CoIns = 0;
        //                        d_Deductible = 0;
        //                        PaymentTray = "";


        //                        c1ClaimGrid.Rows.Add();
        //                        rowIndex = c1ClaimGrid.Rows.Count - 1;

        //                        c1ClaimGrid.SetData(rowIndex, COL_11, sLastclaimNo);
        //                        c1ClaimGrid.SetData(rowIndex, COL_12, _sLastTransactionID);

        //                        c1ClaimGrid.Rows.Add();
        //                        rowIndex = c1ClaimGrid.Rows.Count - 1;

        //                        c1ClaimGrid.SetData(rowIndex, COL_11, sLastclaimNo);
        //                        c1ClaimGrid.SetData(rowIndex, COL_12, _sLastTransactionID);

        //                    }

        //                    #endregion

        //                    #region  "Header Details"

        //                    c1ClaimGrid.Rows.Add();
        //                    rowIndex = c1ClaimGrid.Rows.Count - 1;

        //                    if (sClaimno != sLastclaimNo)
        //                    {
        //                        c1ClaimGrid.SetData(rowIndex, COL_1, Convert.ToString(dtClaims.Rows[i]["nClaimNumber"]));
        //                        c1ClaimGrid.SetData(rowIndex, COL_2, Convert.ToString(dtClaims.Rows[i]["nFromDate"]));
        //                        c1ClaimGrid.SetData(rowIndex, COL_8, Convert.ToString(dtClaims.Rows[i]["ProviderName"]));
        //                        c1ClaimGrid.SetData(rowIndex, COL_9, Convert.ToString(dtClaims.Rows[i]["ChargesTrayDesc"]));
        //                        c1ClaimGrid.SetData(rowIndex, COL_10, Convert.ToString(dtClaims.Rows[i]["sIsVoided"]));


        //                        CellRange crClaimHeader;
        //                        crClaimHeader = c1ClaimGrid.GetCellRange(rowIndex, COL_1, rowIndex, COL_1);
        //                        crClaimHeader.Style = csClaimHeader;

        //                        if (Convert.ToString(dtClaims.Rows[i]["sIsVoided"]) == "Voided")
        //                        {
        //                            CellRange crIsVoided;
        //                            crIsVoided = c1ClaimGrid.GetCellRange(rowIndex, COL_10, rowIndex, COL_10);
        //                            crIsVoided.Style = csClaimHeader;
        //                        }


        //                        #region "Balance"


        //                        if (Convert.ToString(dtClaims.Rows[i]["sIsVoided"]) != "Voided")
        //                        {
        //                            DateTime dtTransdate = Convert.ToDateTime(dtClaims.Rows[i]["nFromDate"]);

        //                            if (dtTransdate <= DateTime.Now.Date && dtTransdate >= DateTime.Now.Subtract(new TimeSpan(30, 0, 0, 0)))
        //                            {
        //                                _30DaysCharges += Convert.ToDecimal(dtClaims.Rows[i]["dTotal"]);
        //                            }
        //                            else if (dtTransdate < DateTime.Now.Subtract(new TimeSpan(30, 0, 0, 0)) && dtTransdate >= DateTime.Now.Subtract(new TimeSpan(60, 0, 0, 0)))
        //                            {
        //                                _60DaysCharges += Convert.ToDecimal(dtClaims.Rows[i]["dTotal"]);
        //                            }
        //                            else if (dtTransdate < DateTime.Now.Subtract(new TimeSpan(60, 0, 0, 0)) && dtTransdate >= DateTime.Now.Subtract(new TimeSpan(90, 0, 0, 0)))
        //                            {
        //                                _90DaysCharges += Convert.ToDecimal(dtClaims.Rows[i]["dTotal"]);
        //                            }
        //                            else if (dtTransdate < DateTime.Now.Subtract(new TimeSpan(90, 0, 0, 0)) && dtTransdate >= DateTime.Now.Subtract(new TimeSpan(120, 0, 0, 0)))
        //                            {
        //                                _120DaysCharges += Convert.ToDecimal(dtClaims.Rows[i]["dTotal"]);
        //                            }
        //                            else if (dtTransdate <= DateTime.Now.Subtract(new TimeSpan(120, 0, 0, 0)))
        //                            {
        //                                _Above120DaysCharges += Convert.ToDecimal(dtClaims.Rows[i]["dTotal"]);
        //                            }
        //                        }

        //                        #endregion

        //                        c1ClaimGrid.SetData(rowIndex, COL_3, Convert.ToString(dtClaims.Rows[i]["sCPTCode"]));
        //                        c1ClaimGrid.SetData(rowIndex, COL_4, Convert.ToString(dtClaims.Rows[i]["sMod1Code"]));
        //                        c1ClaimGrid.SetData(rowIndex, COL_5, Convert.ToString(dtClaims.Rows[i]["sMod2Code"]));
        //                        c1ClaimGrid.SetData(rowIndex, COL_6, Convert.ToString(Convert.ToInt64(dtClaims.Rows[i]["dUnit"])));

        //                        CellRange CrDollars2 = new CellRange();
        //                        CrDollars2 = c1ClaimGrid.GetCellRange(rowIndex, COL_7, rowIndex, COL_7);
        //                        CrDollars2.Style = csCurrency;

        //                        c1ClaimGrid.SetData(rowIndex, COL_7, Convert.ToString(dtClaims.Rows[i]["dTotal"]), true);


        //                        //c1ClaimGrid.Cols[COL_7].Style = csCurrency;



        //                        c1ClaimGrid.SetData(rowIndex, COL_11, Convert.ToString(dtClaims.Rows[i]["nClaimNumber"]));
        //                        c1ClaimGrid.SetData(rowIndex, COL_12, Convert.ToString(dtClaims.Rows[i]["nTransactionID"]));


        //                        #region "Sum of Last claim"

        //                        c1ClaimGrid.Rows.Add();
        //                        rowIndex = c1ClaimGrid.Rows.Count - 1;

        //                        CellRange subGrandTotal2;
        //                        subGrandTotal2 = c1ClaimGrid.GetCellRange(rowIndex, COL_6, rowIndex, COL_6);
        //                        subGrandTotal2.Style = csGrandTotalTitleRow;
        //                        c1ClaimGrid.SetData(rowIndex, COL_6, "Total: ", true);


        //                        CellRange subTotalRangeLast;
        //                        subTotalRangeLast = c1ClaimGrid.GetCellRange(rowIndex, COL_7, rowIndex, COL_7);
        //                        subTotalRangeLast.Style = csSubTotalRow;
        //                        c1ClaimGrid.SetData(rowIndex, COL_7, d_Charges, true);

        //                        c1ClaimGrid.SetData(rowIndex, COL_11, sClaimno);
        //                        c1ClaimGrid.SetData(rowIndex, COL_12, _sTransactionID);


        //                        d_Charges = 0;


        //                        //c1ClaimGrid.Rows.Add();
        //                        //rowIndex = c1ClaimGrid.Rows.Count - 1;

        //                        //c1ClaimGrid.SetData(rowIndex, COL_2, "Payment Source");
        //                        //c1ClaimGrid.SetData(rowIndex, COL_3, "Payment Date");
        //                        //c1ClaimGrid.SetData(rowIndex, COL_4, "Payment");
        //                        //c1ClaimGrid.SetData(rowIndex, COL_5, "W/O");
        //                        //c1ClaimGrid.SetData(rowIndex, COL_6, "Withhold");
        //                        //c1ClaimGrid.SetData(rowIndex, COL_7, "Copay");
        //                        //c1ClaimGrid.SetData(rowIndex, COL_8, "Co-Ins");
        //                        //c1ClaimGrid.SetData(rowIndex, COL_9, "Ded");
        //                        //c1ClaimGrid.SetData(rowIndex, COL_10, "PaymentTray");

        //                        //subPaymentsource = new CellRange();
        //                        //subPaymentsource = c1ClaimGrid.GetCellRange(rowIndex, COL_1, rowIndex, COL_10);
        //                        //subPaymentsource.Style = csPaymentSource;

        //                        //c1ClaimGrid.SetData(rowIndex, COL_11, sClaimno);
        //                        //c1ClaimGrid.SetData(rowIndex, COL_12, _sTransactionID);


        //                        //c1ClaimGrid.Rows.Add();
        //                        //rowIndex = c1ClaimGrid.Rows.Count - 1;

        //                        //c1ClaimGrid.SetData(rowIndex, COL_2, InsuranceName);
        //                        //c1ClaimGrid.SetData(rowIndex, COL_3, PaymentDate);

        //                        //CrDollars = new CellRange();
        //                        //CrDollars = c1ClaimGrid.GetCellRange(rowIndex, COL_4, rowIndex, COL_9);
        //                        //CrDollars.Style = csCurrency;

        //                        //c1ClaimGrid.SetData(rowIndex, COL_4, d_InsurancePayment, true);
        //                        //c1ClaimGrid.SetData(rowIndex, COL_5, d_ContAdj, true);
        //                        //c1ClaimGrid.SetData(rowIndex, COL_6, d_Withhold, true);
        //                        //c1ClaimGrid.SetData(rowIndex, COL_7, d_Copay, true);
        //                        //c1ClaimGrid.SetData(rowIndex, COL_8, d_CoIns, true);
        //                        //c1ClaimGrid.SetData(rowIndex, COL_9, d_Deductible, true);
        //                        //c1ClaimGrid.SetData(rowIndex, COL_10, PaymentTray, false);

        //                        //c1ClaimGrid.SetData(rowIndex, COL_11, sLastclaimNo);
        //                        //c1ClaimGrid.SetData(rowIndex, COL_12, _sLastTransactionID);


        //                        //InsuranceName = "";
        //                        //PaymentDate = "";
        //                        //d_InsurancePayment = 0;
        //                        //d_ContAdj = 0;
        //                        //d_Withhold = 0;
        //                        //d_Copay = 0;
        //                        //d_CoIns = 0;
        //                        //d_Deductible = 0;
        //                        //PaymentTray = "";



        //                        c1ClaimGrid.Rows.Add();
        //                        rowIndex = c1ClaimGrid.Rows.Count - 1;

        //                        c1ClaimGrid.SetData(rowIndex, COL_11, sLastclaimNo);
        //                        c1ClaimGrid.SetData(rowIndex, COL_12, _sLastTransactionID);


        //                        c1ClaimGrid.Rows.Add();
        //                        rowIndex = c1ClaimGrid.Rows.Count - 1;

        //                        //c1ClaimGrid.Rows.Add();
        //                        //rowIndex = c1ClaimGrid.Rows.Count - 1;


        //                        #region "Advance Payment"

        //                        FetchAdvancePayment(c1ClaimGrid, 1);

        //                        #endregion


        //                        #region "Patient Balance"

        //                        _30DaysBalanceDue = _30DaysCharges - (_30DayesPaid);
        //                        _60DaysBalanceDue = _60DaysCharges - (_60DayesPaid);
        //                        _90DaysBalanceDue = _90DaysCharges - (_90DayesPaid);
        //                        _120DaysBalanceDue = _120DaysCharges - (_120DayesPaid);
        //                        _Above120DaysBalanceDue = _Above120DaysCharges - (_Above120DayesPaid);

        //                        int _rowIndex = 0;

        //                        C1PatientDetails.Rows.Add();
        //                        _rowIndex = C1PatientDetails.Rows.Count - 1;


        //                        C1PatientDetails.SetData(_rowIndex, COLPAT_1, "31-60");
        //                        C1PatientDetails.SetData(_rowIndex, COLPAT_2, _30DaysBalanceDue);
        //                        C1PatientDetails.Rows.Add();
        //                        _rowIndex = C1PatientDetails.Rows.Count - 1;

        //                        C1PatientDetails.SetData(_rowIndex, COLPAT_1, "61-90");
        //                        C1PatientDetails.SetData(_rowIndex, COLPAT_2, _60DaysBalanceDue);
        //                        C1PatientDetails.Rows.Add();
        //                        _rowIndex = C1PatientDetails.Rows.Count - 1;


        //                        C1PatientDetails.SetData(_rowIndex, COLPAT_1, "91-120");
        //                        C1PatientDetails.SetData(_rowIndex, COLPAT_2, _90DaysBalanceDue);
        //                        C1PatientDetails.Rows.Add();
        //                        _rowIndex = C1PatientDetails.Rows.Count - 1;


        //                        C1PatientDetails.SetData(_rowIndex, COLPAT_1, "121-150");
        //                        C1PatientDetails.SetData(_rowIndex, COLPAT_2, _120DaysBalanceDue);
        //                        C1PatientDetails.Rows.Add();
        //                        _rowIndex = C1PatientDetails.Rows.Count - 1;


        //                        C1PatientDetails.SetData(_rowIndex, COLPAT_1, ">150");
        //                        C1PatientDetails.SetData(_rowIndex, COLPAT_2, _Above120DaysBalanceDue);
        //                        C1PatientDetails.Rows.Add();
        //                        _rowIndex = C1PatientDetails.Rows.Count - 1;


        //                        C1PatientDetails.SetData(_rowIndex, COLPAT_1, "Total :");
        //                        C1PatientDetails.SetData(_rowIndex, COLPAT_2, _30DaysBalanceDue + _60DaysBalanceDue + _90DaysBalanceDue + _120DaysBalanceDue + _Above120DaysBalanceDue);



        //                        CellRange subPatTotalCaption;
        //                        subPatTotalCaption = C1PatientDetails.GetCellRange(_rowIndex, COL_1, _rowIndex, COL_1);
        //                        subPatTotalCaption.Style = csGrandTotalTitleRow;


        //                        CellRange subPatTot;
        //                        subPatTot = C1PatientDetails.GetCellRange(_rowIndex, COL_2, _rowIndex, COL_2);
        //                        subPatTot.Style = csSubTotalRow;


        //                        _30DaysBalanceDue = 0;
        //                        _60DaysBalanceDue = 0;
        //                        _90DaysBalanceDue = 0;
        //                        _120DaysBalanceDue = 0;
        //                        _Above120DaysBalanceDue = 0;
        //                        _30DaysCharges = 0;
        //                        _30DayesPaid = 0;
        //                        _60DaysCharges = 0;
        //                        _60DayesPaid = 0;
        //                        _90DaysCharges = 0;
        //                        _90DayesPaid = 0;
        //                        _120DaysCharges = 0;
        //                        _120DayesPaid = 0;
        //                        _Above120DaysCharges = 0;
        //                        _Above120DayesPaid = 0;


        //                        #endregion



        //                        #endregion

        //                        #region "Fetch Payment Source"
        //                        string _LastCPTCode = "";
        //                        gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
        //                        oDBParameters.Add("@ClaimNo", Convert.ToInt64(sClaimno), ParameterDirection.Input, SqlDbType.BigInt);
        //                        oDB.Retrive("BL_SELECT_EOBPatientLedgerLines", oDBParameters, out  dtPaysource);
        //                        //oDB.Disconnect();
        //                        if (dtPaysource != null)
        //                        {
        //                            if (dtPaysource.Rows.Count > 0)
        //                            {
        //                                for (int j = 0; j < dtPaysource.Rows.Count; j++)
        //                                {

        //                                    d_InsurancePayment = d_InsurancePayment + Convert.ToDecimal(dtPaysource.Rows[j]["InsurancePayment"]);

        //                                    if (Convert.ToString(dtPaysource.Rows[j]["sCPTCode"]) != _LastCPTCode)
        //                                    {
        //                                        d_ContAdj = d_ContAdj + Convert.ToDecimal(dtPaysource.Rows[j]["ContAdj"]);
        //                                    }
        //                                    d_Withhold = d_Withhold + Convert.ToDecimal(dtPaysource.Rows[j]["Withhold"]);
        //                                    d_Copay = d_Copay + Convert.ToDecimal(dtPaysource.Rows[j]["Copay"]);
        //                                    d_CoIns = d_CoIns + Convert.ToDecimal(dtPaysource.Rows[j]["Coinsurance"]);
        //                                    d_Deductible = d_Deductible + Convert.ToDecimal(dtPaysource.Rows[j]["Deductible"]);
        //                                    InsuranceName = Convert.ToString(dtPaysource.Rows[j]["PaymentInsuranceName"]);
        //                                    PaymentTray = Convert.ToString(dtPaysource.Rows[j]["PaymentTray"]);
        //                                    PaymentDate = Convert.ToString(dtPaysource.Rows[j]["PaymentDate"]);

        //                                    DateTime dtDOS = Convert.ToDateTime(dtPaysource.Rows[j]["DOS"]);

        //                                    if (Convert.ToString(dtClaims.Rows[i]["sIsVoided"]) != "Voided")
        //                                    {
        //                                        if (dtDOS <= DateTime.Now.Date && dtDOS >= DateTime.Now.Subtract(new TimeSpan(30, 0, 0, 0)))
        //                                        {
        //                                            //_30DayesPaid += Convert.ToDecimal(dtPaysource.Rows[j]["InsurancePayment"]) + Convert.ToDecimal(dtPaysource.Rows[j]["ContAdj"]) + Convert.ToDecimal(dtPaysource.Rows[j]["Withhold"]);
        //                                            _30DayesPaid += Convert.ToDecimal(dtPaysource.Rows[j]["InsurancePayment"]) + Convert.ToDecimal(d_ContAdj) + Convert.ToDecimal(dtPaysource.Rows[j]["Withhold"]);

        //                                        }
        //                                        else if (dtDOS < DateTime.Now.Subtract(new TimeSpan(30, 0, 0, 0)) && dtDOS >= DateTime.Now.Subtract(new TimeSpan(60, 0, 0, 0)))
        //                                        {
        //                                            //_60DayesPaid += Convert.ToDecimal(dtPaysource.Rows[j]["InsurancePayment"]) + Convert.ToDecimal(dtPaysource.Rows[j]["ContAdj"]) + Convert.ToDecimal(dtPaysource.Rows[j]["Withhold"]);
        //                                            _60DayesPaid += Convert.ToDecimal(dtPaysource.Rows[j]["InsurancePayment"]) + Convert.ToDecimal(d_ContAdj) + Convert.ToDecimal(dtPaysource.Rows[j]["Withhold"]);
        //                                        }
        //                                        else if (dtDOS < DateTime.Now.Subtract(new TimeSpan(60, 0, 0, 0)) && dtDOS >= DateTime.Now.Subtract(new TimeSpan(90, 0, 0, 0)))
        //                                        {
        //                                            //_90DayesPaid += Convert.ToDecimal(dtPaysource.Rows[j]["InsurancePayment"]) + Convert.ToDecimal(dtPaysource.Rows[j]["ContAdj"]) + Convert.ToDecimal(dtPaysource.Rows[j]["Withhold"]);
        //                                            _90DayesPaid += Convert.ToDecimal(dtPaysource.Rows[j]["InsurancePayment"]) + Convert.ToDecimal(d_ContAdj) + Convert.ToDecimal(dtPaysource.Rows[j]["Withhold"]);
        //                                        }
        //                                        else if (dtDOS < DateTime.Now.Subtract(new TimeSpan(90, 0, 0, 0)) && dtDOS >= DateTime.Now.Subtract(new TimeSpan(120, 0, 0, 0)))
        //                                        {
        //                                            //_120DayesPaid += Convert.ToDecimal(dtPaysource.Rows[j]["InsurancePayment"]) + Convert.ToDecimal(dtPaysource.Rows[j]["ContAdj"]) + Convert.ToDecimal(dtPaysource.Rows[j]["Withhold"]);
        //                                            _120DayesPaid += Convert.ToDecimal(dtPaysource.Rows[j]["InsurancePayment"]) + Convert.ToDecimal(d_ContAdj) + Convert.ToDecimal(dtPaysource.Rows[j]["Withhold"]);
        //                                        }
        //                                        else if (dtDOS <= DateTime.Now.Subtract(new TimeSpan(120, 0, 0, 0)))
        //                                        {
        //                                            // _Above120DayesPaid += Convert.ToDecimal(dtPaysource.Rows[j]["InsurancePayment"]) + Convert.ToDecimal(dtPaysource.Rows[j]["ContAdj"]) + Convert.ToDecimal(dtPaysource.Rows[j]["Withhold"]);
        //                                            _Above120DayesPaid += Convert.ToDecimal(dtPaysource.Rows[j]["InsurancePayment"]) + Convert.ToDecimal(d_ContAdj) + Convert.ToDecimal(dtPaysource.Rows[j]["Withhold"]);
        //                                        }
        //                                    }

        //                                    _LastCPTCode = Convert.ToString(dtPaysource.Rows[j]["sCPTCode"]);
        //                                    //c1ClaimGrid.Rows.Add();
        //                                    //rowIndex = c1ClaimGrid.Rows.Count - 1;

        //                                    //c1ClaimGrid.SetData(rowIndex, COL_2, "Payment Source");
        //                                    //c1ClaimGrid.SetData(rowIndex, COL_3, "Payment Date");
        //                                    //c1ClaimGrid.SetData(rowIndex, COL_4, "Payment");
        //                                    //c1ClaimGrid.SetData(rowIndex, COL_5, "W/O");
        //                                    //c1ClaimGrid.SetData(rowIndex, COL_6, "Withhold");
        //                                    //c1ClaimGrid.SetData(rowIndex, COL_7, "Copay");
        //                                    //c1ClaimGrid.SetData(rowIndex, COL_8, "Co-Ins");
        //                                    //c1ClaimGrid.SetData(rowIndex, COL_9, "Ded");
        //                                    //c1ClaimGrid.SetData(rowIndex, COL_10, "PaymentTray");

        //                                    //subPaymentsource = new CellRange();
        //                                    //subPaymentsource = c1ClaimGrid.GetCellRange(rowIndex, COL_1, rowIndex, COL_10);
        //                                    //subPaymentsource.Style = csPaymentSource;

        //                                    //c1ClaimGrid.SetData(rowIndex, COL_11, sClaimno);
        //                                    //c1ClaimGrid.SetData(rowIndex, COL_12, _sTransactionID);


        //                                    c1ClaimGrid.Rows.Add();
        //                                    rowIndex = c1ClaimGrid.Rows.Count - 1;

        //                                    c1ClaimGrid.SetData(rowIndex, COL_2, InsuranceName);
        //                                    c1ClaimGrid.SetData(rowIndex, COL_3, PaymentDate);

        //                                    CrDollars = new CellRange();
        //                                    CrDollars = c1ClaimGrid.GetCellRange(rowIndex, COL_4, rowIndex, COL_9);
        //                                    CrDollars.Style = csCurrency;

        //                                    c1ClaimGrid.SetData(rowIndex, COL_4, d_InsurancePayment, true);
        //                                    c1ClaimGrid.SetData(rowIndex, COL_5, d_ContAdj, true);
        //                                    c1ClaimGrid.SetData(rowIndex, COL_6, d_Withhold, true);
        //                                    c1ClaimGrid.SetData(rowIndex, COL_7, d_Copay, true);
        //                                    c1ClaimGrid.SetData(rowIndex, COL_8, d_CoIns, true);
        //                                    c1ClaimGrid.SetData(rowIndex, COL_9, d_Deductible, true);
        //                                    c1ClaimGrid.SetData(rowIndex, COL_10, PaymentTray, false);

        //                                    c1ClaimGrid.SetData(rowIndex, COL_11, sLastclaimNo);
        //                                    c1ClaimGrid.SetData(rowIndex, COL_12, _sLastTransactionID);


        //                                    InsuranceName = "";
        //                                    PaymentDate = "";
        //                                    d_InsurancePayment = 0;
        //                                    d_ContAdj = 0;
        //                                    d_Withhold = 0;
        //                                    d_Copay = 0;
        //                                    d_CoIns = 0;
        //                                    d_Deductible = 0;
        //                                    PaymentTray = "";

        //                                }
        //                            }
        //                        }

        //                        #endregion

        //                    }

        //                    d_Charges = d_Charges + Convert.ToDecimal(dtClaims.Rows[i]["dTotal"]);


        //                    //#region "Balance"


        //                    //if (Convert.ToString(dtClaims.Rows[i]["sIsVoided"]) != "Voided")
        //                    //{
        //                    //    DateTime dtTransdate = Convert.ToDateTime(dtClaims.Rows[i]["nFromDate"]);

        //                    //    if (dtTransdate <= DateTime.Now.Date && dtTransdate >= DateTime.Now.Subtract(new TimeSpan(30, 0, 0, 0)))
        //                    //    {
        //                    //        _30DaysCharges += Convert.ToDecimal(dtClaims.Rows[i]["dTotal"]);
        //                    //    }
        //                    //    else if (dtTransdate < DateTime.Now.Subtract(new TimeSpan(30, 0, 0, 0)) && dtTransdate >= DateTime.Now.Subtract(new TimeSpan(60, 0, 0, 0)))
        //                    //    {
        //                    //        _60DaysCharges += Convert.ToDecimal(dtClaims.Rows[i]["dTotal"]);
        //                    //    }
        //                    //    else if (dtTransdate < DateTime.Now.Subtract(new TimeSpan(60, 0, 0, 0)) && dtTransdate >= DateTime.Now.Subtract(new TimeSpan(90, 0, 0, 0)))
        //                    //    {
        //                    //        _90DaysCharges += Convert.ToDecimal(dtClaims.Rows[i]["dTotal"]);
        //                    //    }
        //                    //    else if (dtTransdate < DateTime.Now.Subtract(new TimeSpan(90, 0, 0, 0)) && dtTransdate >= DateTime.Now.Subtract(new TimeSpan(120, 0, 0, 0)))
        //                    //    {
        //                    //        _120DaysCharges += Convert.ToDecimal(dtClaims.Rows[i]["dTotal"]);
        //                    //    }
        //                    //    else if (dtTransdate <= DateTime.Now.Subtract(new TimeSpan(120, 0, 0, 0)))
        //                    //    {
        //                    //        _Above120DaysCharges += Convert.ToDecimal(dtClaims.Rows[i]["dTotal"]);
        //                    //    }
        //                    //}

        //                    //#endregion

        //                    //c1ClaimGrid.SetData(rowIndex, COL_3, Convert.ToString(dtClaims.Rows[i]["sCPTCode"]));
        //                    //c1ClaimGrid.SetData(rowIndex, COL_4, Convert.ToString(dtClaims.Rows[i]["sMod1Code"]));
        //                    //c1ClaimGrid.SetData(rowIndex, COL_5, Convert.ToString(dtClaims.Rows[i]["sMod2Code"]));
        //                    //c1ClaimGrid.SetData(rowIndex, COL_6, Convert.ToString(Convert.ToInt64(dtClaims.Rows[i]["dUnit"])));

        //                    //CellRange CrDollars2 = new CellRange();
        //                    //CrDollars2 = c1ClaimGrid.GetCellRange(rowIndex, COL_7, rowIndex, COL_7);
        //                    //CrDollars2.Style = csCurrency;

        //                    //c1ClaimGrid.SetData(rowIndex, COL_7, Convert.ToString(dtClaims.Rows[i]["dTotal"]), true);


        //                    ////c1ClaimGrid.Cols[COL_7].Style = csCurrency;



        //                    //c1ClaimGrid.SetData(rowIndex, COL_11, Convert.ToString(dtClaims.Rows[i]["nClaimNumber"]));
        //                    //c1ClaimGrid.SetData(rowIndex, COL_12, Convert.ToString(dtClaims.Rows[i]["nTransactionID"]));

        //                    sLastclaimNo = dtClaims.Rows[i]["nClaimNumber"].ToString();
        //                    _sLastTransactionID = dtClaims.Rows[i]["nTransactionID"].ToString();

        //                    #endregion

        //                }

        //                //#region "Sum of Last claim"

        //                //c1ClaimGrid.Rows.Add();
        //                //rowIndex = c1ClaimGrid.Rows.Count - 1;

        //                //CellRange subGrandTotal2;
        //                //subGrandTotal2 = c1ClaimGrid.GetCellRange(rowIndex, COL_6, rowIndex, COL_6);
        //                //subGrandTotal2.Style = csGrandTotalTitleRow;
        //                //c1ClaimGrid.SetData(rowIndex, COL_6, "Total: ", true);


        //                //CellRange subTotalRangeLast;
        //                //subTotalRangeLast = c1ClaimGrid.GetCellRange(rowIndex, COL_7, rowIndex, COL_7);
        //                //subTotalRangeLast.Style = csSubTotalRow;
        //                //c1ClaimGrid.SetData(rowIndex, COL_7, d_Charges, true);

        //                //c1ClaimGrid.SetData(rowIndex, COL_11, sClaimno);
        //                //c1ClaimGrid.SetData(rowIndex, COL_12, _sTransactionID);


        //                //d_Charges = 0;


        //                ////c1ClaimGrid.Rows.Add();
        //                ////rowIndex = c1ClaimGrid.Rows.Count - 1;

        //                ////c1ClaimGrid.SetData(rowIndex, COL_2, "Payment Source");
        //                ////c1ClaimGrid.SetData(rowIndex, COL_3, "Payment Date");
        //                ////c1ClaimGrid.SetData(rowIndex, COL_4, "Payment");
        //                ////c1ClaimGrid.SetData(rowIndex, COL_5, "W/O");
        //                ////c1ClaimGrid.SetData(rowIndex, COL_6, "Withhold");
        //                ////c1ClaimGrid.SetData(rowIndex, COL_7, "Copay");
        //                ////c1ClaimGrid.SetData(rowIndex, COL_8, "Co-Ins");
        //                ////c1ClaimGrid.SetData(rowIndex, COL_9, "Ded");
        //                ////c1ClaimGrid.SetData(rowIndex, COL_10, "PaymentTray");

        //                ////subPaymentsource = new CellRange();
        //                ////subPaymentsource = c1ClaimGrid.GetCellRange(rowIndex, COL_1, rowIndex, COL_10);
        //                ////subPaymentsource.Style = csPaymentSource;

        //                ////c1ClaimGrid.SetData(rowIndex, COL_11, sClaimno);
        //                ////c1ClaimGrid.SetData(rowIndex, COL_12, _sTransactionID);


        //                ////c1ClaimGrid.Rows.Add();
        //                ////rowIndex = c1ClaimGrid.Rows.Count - 1;

        //                ////c1ClaimGrid.SetData(rowIndex, COL_2, InsuranceName);
        //                ////c1ClaimGrid.SetData(rowIndex, COL_3, PaymentDate);

        //                ////CrDollars = new CellRange();
        //                ////CrDollars = c1ClaimGrid.GetCellRange(rowIndex, COL_4, rowIndex, COL_9);
        //                ////CrDollars.Style = csCurrency;

        //                ////c1ClaimGrid.SetData(rowIndex, COL_4, d_InsurancePayment, true);
        //                ////c1ClaimGrid.SetData(rowIndex, COL_5, d_ContAdj, true);
        //                ////c1ClaimGrid.SetData(rowIndex, COL_6, d_Withhold, true);
        //                ////c1ClaimGrid.SetData(rowIndex, COL_7, d_Copay, true);
        //                ////c1ClaimGrid.SetData(rowIndex, COL_8, d_CoIns, true);
        //                ////c1ClaimGrid.SetData(rowIndex, COL_9, d_Deductible, true);
        //                ////c1ClaimGrid.SetData(rowIndex, COL_10, PaymentTray, false);

        //                ////c1ClaimGrid.SetData(rowIndex, COL_11, sLastclaimNo);
        //                ////c1ClaimGrid.SetData(rowIndex, COL_12, _sLastTransactionID);


        //                ////InsuranceName = "";
        //                ////PaymentDate = "";
        //                ////d_InsurancePayment = 0;
        //                ////d_ContAdj = 0;
        //                ////d_Withhold = 0;
        //                ////d_Copay = 0;
        //                ////d_CoIns = 0;
        //                ////d_Deductible = 0;
        //                ////PaymentTray = "";



        //                //c1ClaimGrid.Rows.Add();
        //                //rowIndex = c1ClaimGrid.Rows.Count - 1;

        //                //c1ClaimGrid.SetData(rowIndex, COL_11, sLastclaimNo);
        //                //c1ClaimGrid.SetData(rowIndex, COL_12, _sLastTransactionID);


        //                //c1ClaimGrid.Rows.Add();
        //                //rowIndex = c1ClaimGrid.Rows.Count - 1;

        //                ////c1ClaimGrid.Rows.Add();
        //                ////rowIndex = c1ClaimGrid.Rows.Count - 1;


        //                //#region "Advance Payment"

        //                //FetchAdvancePayment(c1ClaimGrid, 1);

        //                //#endregion


        //                //#region "Patient Balance"

        //                //_30DaysBalanceDue = _30DaysCharges - (_30DayesPaid);
        //                //_60DaysBalanceDue = _60DaysCharges - (_60DayesPaid);
        //                //_90DaysBalanceDue = _90DaysCharges - (_90DayesPaid);
        //                //_120DaysBalanceDue = _120DaysCharges - (_120DayesPaid);
        //                //_Above120DaysBalanceDue = _Above120DaysCharges - (_Above120DayesPaid);

        //                //int _rowIndex = 0;

        //                //C1PatientDetails.Rows.Add();
        //                //_rowIndex = C1PatientDetails.Rows.Count - 1;


        //                //C1PatientDetails.SetData(_rowIndex, COLPAT_1, "31-60");
        //                //C1PatientDetails.SetData(_rowIndex, COLPAT_2, _30DaysBalanceDue);
        //                //C1PatientDetails.Rows.Add();
        //                //_rowIndex = C1PatientDetails.Rows.Count - 1;

        //                //C1PatientDetails.SetData(_rowIndex, COLPAT_1, "61-90");
        //                //C1PatientDetails.SetData(_rowIndex, COLPAT_2, _60DaysBalanceDue);
        //                //C1PatientDetails.Rows.Add();
        //                //_rowIndex = C1PatientDetails.Rows.Count - 1;


        //                //C1PatientDetails.SetData(_rowIndex, COLPAT_1, "91-120");
        //                //C1PatientDetails.SetData(_rowIndex, COLPAT_2, _90DaysBalanceDue);
        //                //C1PatientDetails.Rows.Add();
        //                //_rowIndex = C1PatientDetails.Rows.Count - 1;


        //                //C1PatientDetails.SetData(_rowIndex, COLPAT_1, "121-150");
        //                //C1PatientDetails.SetData(_rowIndex, COLPAT_2, _120DaysBalanceDue);
        //                //C1PatientDetails.Rows.Add();
        //                //_rowIndex = C1PatientDetails.Rows.Count - 1;


        //                //C1PatientDetails.SetData(_rowIndex, COLPAT_1, ">150");
        //                //C1PatientDetails.SetData(_rowIndex, COLPAT_2, _Above120DaysBalanceDue);
        //                //C1PatientDetails.Rows.Add();
        //                //_rowIndex = C1PatientDetails.Rows.Count - 1;


        //                //C1PatientDetails.SetData(_rowIndex, COLPAT_1, "Total :");
        //                //C1PatientDetails.SetData(_rowIndex, COLPAT_2, _30DaysBalanceDue + _60DaysBalanceDue + _90DaysBalanceDue + _120DaysBalanceDue + _Above120DaysBalanceDue);



        //                //CellRange subPatTotalCaption;
        //                //subPatTotalCaption = C1PatientDetails.GetCellRange(_rowIndex, COL_1, _rowIndex, COL_1);
        //                //subPatTotalCaption.Style = csGrandTotalTitleRow;


        //                //CellRange subPatTot;
        //                //subPatTot = C1PatientDetails.GetCellRange(_rowIndex, COL_2, _rowIndex, COL_2);
        //                //subPatTot.Style = csSubTotalRow;


        //                //_30DaysBalanceDue = 0;
        //                //_60DaysBalanceDue = 0;
        //                //_90DaysBalanceDue = 0;
        //                //_120DaysBalanceDue = 0;
        //                //_Above120DaysBalanceDue = 0;
        //                //_30DaysCharges = 0;
        //                //_30DayesPaid = 0;
        //                //_60DaysCharges = 0;
        //                //_60DayesPaid = 0;
        //                //_90DaysCharges = 0;
        //                //_90DayesPaid = 0;
        //                //_120DaysCharges = 0;
        //                //_120DayesPaid = 0;
        //                //_Above120DaysCharges = 0;
        //                //_Above120DayesPaid = 0;


        //                //#endregion



        //                //#endregion

        //            }
        //            if (oDB != null) { oDB.Dispose(); };

        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {

        //    }

        //}
        private void FillClaimDetails()
        {
            try
            {
                string dtlsClaimno = "";
                string dtlsLastclaimNo = "";
                DataTable dtClaimDetails = null;
                DataTable dtPaymentDetails = null;
                int rowIndex = 0;
                
                String _sCPTCode = "";
                CellRange subPaymentsourceDetails;
                CellRange CrDetailsTotals;
               
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);

                decimal _ClaimNo = Convert.ToInt64(c1ClaimGrid.GetData(c1ClaimGrid.RowSel, c1ClaimGrid.Cols[COL_11].Index));

                gloDatabaseLayer.DBParameters oDBParameters1 = new gloDatabaseLayer.DBParameters();
                oDBParameters1.Add("@nPatientID", Convert.ToInt64(_PatientID), ParameterDirection.Input, SqlDbType.BigInt);
                if (_ClaimNo != 0)
                {
                    oDBParameters1.Add("@nClaimNo", Convert.ToInt64(_ClaimNo), ParameterDirection.Input, SqlDbType.BigInt);
                }
                else
                {
                    oDBParameters1.Add("@nClaimNo", Convert.ToInt64(0), ParameterDirection.Input, SqlDbType.BigInt);
                }
                oDB.Retrive("BL_SELECT_EOBPatientLedger", oDBParameters1, out  dtClaimDetails);

                if (dtClaimDetails != null && dtClaimDetails.Rows.Count > 0)
                {
                    for (int i = 0; i < dtClaimDetails.Rows.Count; i++)
                    {
                        dtlsClaimno = dtClaimDetails.Rows[i]["nTransactionDetailID"].ToString();

                        if (dtlsClaimno != dtlsLastclaimNo)
                        {
                            c1DetailCharges.Rows.Add();
                            rowIndex = c1DetailCharges.Rows.Count - 1;


                            c1DetailCharges.SetData(rowIndex, COL_1, Convert.ToString(dtClaimDetails.Rows[i]["nClaimNumber"]));
                            c1DetailCharges.SetData(rowIndex, COL_2, Convert.ToString(dtClaimDetails.Rows[i]["nFromDate"]));
                            c1DetailCharges.SetData(rowIndex, COL_8, Convert.ToString(dtClaimDetails.Rows[i]["ProviderName"]));
                            c1DetailCharges.SetData(rowIndex, COL_9, Convert.ToString(dtClaimDetails.Rows[i]["ChargesTrayDesc"]));
                            c1DetailCharges.SetData(rowIndex, COL_3, Convert.ToString(dtClaimDetails.Rows[i]["sCPTCode"]));
                            c1DetailCharges.SetData(rowIndex, COL_4, Convert.ToString(dtClaimDetails.Rows[i]["sMod1Code"]));
                            c1DetailCharges.SetData(rowIndex, COL_5, Convert.ToString(dtClaimDetails.Rows[i]["sMod2Code"]));
                            c1DetailCharges.SetData(rowIndex, COL_6, Convert.ToDecimal(dtClaimDetails.Rows[i]["dUnit"]).ToString("#0.###############"));
                            c1DetailCharges.SetData(rowIndex, COL_10, Convert.ToString((dtClaimDetails.Rows[i]["sIsVoided"])));
                            c1DetailCharges.SetData(rowIndex, COL_13, GetNextResponsibility(Convert.ToString(dtClaimDetails.Rows[i]["nTransactionID"]),Convert.ToString( dtClaimDetails.Rows[i]["nTransactionDetailID"])));

                            //c1DetailCharges.SetData(rowIndex, COL_13, Convert.ToString((dtClaimDetails.Rows[i]["sIsVoided"])));

                            CrDetailsTotals = c1DetailCharges.GetCellRange(rowIndex, COL_7, rowIndex, COL_7);
                            CrDetailsTotals.Style = csCurrency;
                            c1DetailCharges.SetData(rowIndex, COL_7, Convert.ToString(dtClaimDetails.Rows[i]["dTotal"]), true);

                            if (Convert.ToString(dtClaimDetails.Rows[i]["sIsVoided"]) == "Voided")
                            {
                                CellRange crIsVoidedDtl;
                                crIsVoidedDtl = c1DetailCharges.GetCellRange(rowIndex, COL_10, rowIndex, COL_10);
                                crIsVoidedDtl.Style = csClaimHeader;
                            }

                            #region " Detail Payment Details "

                            Int64 nBillingTransactionDetailID = Convert.ToInt64(dtClaimDetails.Rows[i]["nTransactionDetailID"]);

                            gloDatabaseLayer.DBParameters oDBParameters2 = new gloDatabaseLayer.DBParameters();
                            if (_ClaimNo != 0)
                            {
                                oDBParameters2.Add("@ClaimNo", Convert.ToInt64(_ClaimNo), ParameterDirection.Input, SqlDbType.BigInt);
                            }
                            else
                            {
                                oDBParameters2.Add("@ClaimNo", Convert.ToInt64(0), ParameterDirection.Input, SqlDbType.BigInt);
                            }
                            oDBParameters2.Add("@nBillingTransactionDetailId", Convert.ToString(dtlsClaimno), ParameterDirection.Input, SqlDbType.VarChar);


                            oDB.Retrive("BL_SELECT_EOBPatientLedgerDTLLines_New", oDBParameters2, out  dtPaymentDetails);

                            if (dtPaymentDetails != null)
                            {
                                if (dtPaymentDetails.Rows.Count > 0)
                                {
                                    string _strCurrentInsuranceID = "";
                                    string _strLastInsuranceID = "";
                                    Decimal _lastInsPaid = 0;
                                    Decimal _currentInsPaid = 0;
                                    InsuranceName = "";
                                    PaymentDate = "";
                                    d_InsurancePayment = 0;
                                    d_ContAdj = 0;
                                    d_Withhold = 0;
                                    d_Copay = 0;
                                    d_CoIns = 0;
                                    d_Deductible = 0;
                                    PaymentTray = "";
                                    Int64 _nTransactionID = 0;
                                    Int64 _nLastTransactionID = 0;

                                    c1DetailCharges.Rows.Add();
                                    rowIndex = c1DetailCharges.Rows.Count - 1;

                                    c1DetailCharges.SetData(rowIndex, COL_2, "Payment Source");
                                    c1DetailCharges.SetData(rowIndex, COL_3, "Payment Date");
                                    c1DetailCharges.SetData(rowIndex, COL_4, "Payment");
                                    c1DetailCharges.SetData(rowIndex, COL_5, "W/O");
                                    c1DetailCharges.SetData(rowIndex, COL_6, "Withhold");
                                    c1DetailCharges.SetData(rowIndex, COL_7, "Copay");
                                    c1DetailCharges.SetData(rowIndex, COL_8, "Co-Ins");
                                    c1DetailCharges.SetData(rowIndex, COL_9, "Ded");
                                    c1DetailCharges.SetData(rowIndex, COL_10, "PaymentTray");

                                    subPaymentsourceDetails = c1DetailCharges.GetCellRange(rowIndex, COL_1, rowIndex, COL_13);
                                    subPaymentsourceDetails.Style = csPaymentSource;
                                   
                                    for (int j = 0; j < dtPaymentDetails.Rows.Count; j++)
                                    {
                                        _nTransactionID = Convert.ToInt64(dtPaymentDetails.Rows[j]["nEOBDtlID"]);
                                        _sCPTCode = Convert.ToString(dtPaymentDetails.Rows[j]["sCPTCode"]);
                                        InsuranceName = Convert.ToString(dtPaymentDetails.Rows[j]["PaymentInsuranceName"]);
                                        PaymentDate = Convert.ToString(dtPaymentDetails.Rows[j]["PaymentDate"]);
                                        _strCurrentInsuranceID = dtPaymentDetails.Rows[j]["nInsuraceID"].ToString();
                                        _currentInsPaid = Convert.ToDecimal(dtPaymentDetails.Rows[j]["InsurancePayment"]);

                                        if (_strCurrentInsuranceID != _strLastInsuranceID)
                                        {
                                            d_ContAdj = Convert.ToDecimal(dtPaymentDetails.Rows[j]["ContAdj"]);
                                            d_InsurancePayment = Convert.ToDecimal(dtPaymentDetails.Rows[j]["InsurancePayment"]);
                                            d_Withhold = Convert.ToDecimal(dtPaymentDetails.Rows[j]["Withhold"]);
                                            d_Copay = Convert.ToDecimal(dtPaymentDetails.Rows[j]["Copay"]);
                                            d_CoIns = Convert.ToDecimal(dtPaymentDetails.Rows[j]["Coinsurance"]);
                                            d_Deductible = Convert.ToDecimal(dtPaymentDetails.Rows[j]["Deductible"]);
                                        }
                                        else
                                        {

                                            //if (_currentInsPaid > _lastInsPaid)
                                            //{
                                                d_ContAdj = Convert.ToDecimal(dtPaymentDetails.Rows[j]["ContAdj"]) - d_ContAdj;
                                                d_InsurancePayment = Convert.ToDecimal(dtPaymentDetails.Rows[j]["InsurancePayment"]) - d_InsurancePayment;
                                                d_Withhold = Convert.ToDecimal(dtPaymentDetails.Rows[j]["Withhold"]) - d_Withhold;
                                                d_Copay = Convert.ToDecimal(dtPaymentDetails.Rows[j]["Copay"]) - d_Copay;
                                                d_CoIns = Convert.ToDecimal(dtPaymentDetails.Rows[j]["Coinsurance"]) - d_CoIns;
                                                d_Deductible = Convert.ToDecimal(dtPaymentDetails.Rows[j]["Deductible"]) - d_Deductible;
                                            //}
                                            //else
                                            //{
                                            //    d_ContAdj = Convert.ToDecimal(dtPaymentDetails.Rows[j]["ContAdj"]);
                                            //    d_InsurancePayment = Convert.ToDecimal(dtPaymentDetails.Rows[j]["InsurancePayment"]);
                                            //    d_Withhold = Convert.ToDecimal(dtPaymentDetails.Rows[j]["Withhold"]);
                                            //    d_Copay = Convert.ToDecimal(dtPaymentDetails.Rows[j]["Copay"]);
                                            //    d_CoIns = Convert.ToDecimal(dtPaymentDetails.Rows[j]["Coinsurance"]);
                                            //    d_Deductible = Convert.ToDecimal(dtPaymentDetails.Rows[j]["Deductible"]);
                                            //}
                                        }
                                        
                                        
                                        PaymentTray = Convert.ToString(dtPaymentDetails.Rows[j]["PaymentTray"]);
                                       
                                        

                                        c1DetailCharges.Rows.Add();
                                        rowIndex = c1DetailCharges.Rows.Count - 1;

                                        c1DetailCharges.SetData(rowIndex, COL_2, InsuranceName);
                                        c1DetailCharges.SetData(rowIndex, COL_3, PaymentDate);

                                        CrDetailsTotals =  c1DetailCharges.GetCellRange(rowIndex, COL_4, rowIndex, COL_9);
                                        CrDetailsTotals.Style = csCurrency;

                                        c1DetailCharges.SetData(rowIndex, COL_4, d_InsurancePayment, true);
                                        c1DetailCharges.SetData(rowIndex, COL_5, d_ContAdj, true);
                                        c1DetailCharges.SetData(rowIndex, COL_6, d_Withhold, true);
                                        c1DetailCharges.SetData(rowIndex, COL_7, d_Copay, true);
                                        c1DetailCharges.SetData(rowIndex, COL_8, d_CoIns, true);
                                        c1DetailCharges.SetData(rowIndex, COL_9, d_Deductible,true);
                                        c1DetailCharges.SetData(rowIndex, COL_10, PaymentTray,false);


                                        // if (_strCurrentInsuranceID != _strLastInsuranceID)
                                        //{
                                            dTotal_InsurancePayment += d_InsurancePayment;
                                            dTotal_ContAdj += d_ContAdj;
                                            dTotal_WithHold += d_Withhold;
                                            dTotal_Copay += d_Copay;
                                            dTotal_CoIns += d_CoIns;
                                            dTotal_Deductible += d_Deductible;     
                                       // }
                                       // else
                                       // {
                                       //     dTotal_InsurancePayment = d_InsurancePayment;
                                       //     dTotal_ContAdj = d_ContAdj;
                                       //     dTotal_WithHold = d_Withhold;
                                       //     dTotal_Copay = d_Copay;
                                       //     dTotal_CoIns = d_CoIns;
                                       //     dTotal_Deductible = d_Deductible;     
                                       //}


                                                                         
                                        

                                        _nLastTransactionID = Convert.ToInt64(dtPaymentDetails.Rows[j]["nEOBDtlID"]);
                                        _strLastInsuranceID = dtPaymentDetails.Rows[j]["nInsuraceID"].ToString();
                                        _lastInsPaid = Convert.ToDecimal(dtPaymentDetails.Rows[j]["InsurancePayment"]);
                                       
                                        //if (_strCurrentInsuranceID != _strLastInsuranceID)
                                        //{
                                        //    InsuranceName = "";
                                        //    PaymentDate = "";
                                        //    d_InsurancePayment = 0;
                                        //    d_ContAdj = 0;
                                        //    d_Withhold = 0;
                                        //    d_Copay = 0;
                                        //    d_CoIns = 0;
                                        //    d_Deductible = 0;
                                        //    PaymentTray = "";
                                        //}
                                        //else
                                        //{
                                            d_ContAdj = Convert.ToDecimal(dtPaymentDetails.Rows[j]["ContAdj"]);
                                            d_InsurancePayment = Convert.ToDecimal(dtPaymentDetails.Rows[j]["InsurancePayment"]);
                                            d_Withhold = Convert.ToDecimal(dtPaymentDetails.Rows[j]["Withhold"]);
                                            d_Copay = Convert.ToDecimal(dtPaymentDetails.Rows[j]["Copay"]);
                                            d_CoIns = Convert.ToDecimal(dtPaymentDetails.Rows[j]["Coinsurance"]);
                                            d_Deductible = Convert.ToDecimal(dtPaymentDetails.Rows[j]["Deductible"]);
                                       //}
                                        
                                    }
                                    c1DetailCharges.Rows.Add();
                                    rowIndex = c1DetailCharges.Rows.Count - 1;


                                    CellRange subAdvDetailTotCaption;
                                    subAdvDetailTotCaption = c1DetailCharges.GetCellRange(rowIndex, COL_3, rowIndex, COL_3);
                                    subAdvDetailTotCaption.Style = csGrandTotalTitleRow;
                                    c1DetailCharges.SetData(rowIndex, COL_3, "Total :",true);



                                    CellRange subAdvDetailTot;
                                    subAdvDetailTot = c1DetailCharges.GetCellRange(rowIndex, COL_4, rowIndex, COL_9);
                                    subAdvDetailTot.Style = csSubTotalRow;

                                    c1DetailCharges.SetData(rowIndex, COL_4, dTotal_InsurancePayment,true);
                                    c1DetailCharges.SetData(rowIndex, COL_5, dTotal_ContAdj, true);
                                    c1DetailCharges.SetData(rowIndex, COL_6, dTotal_WithHold, true);
                                    c1DetailCharges.SetData(rowIndex, COL_7, dTotal_Copay, true);
                                    c1DetailCharges.SetData(rowIndex, COL_8, dTotal_CoIns, true);
                                    c1DetailCharges.SetData(rowIndex, COL_9, dTotal_Deductible, true);

                                    dTotal_InsurancePayment = 0;
                                    dTotal_ContAdj = 0;
                                    dTotal_WithHold = 0;
                                    dTotal_Copay = 0;
                                    dTotal_CoIns = 0;
                                    dTotal_Deductible = 0;
                                    //dTotal_PatientPayment = 0;

                                    c1DetailCharges.Rows.Add();
                                    rowIndex = c1DetailCharges.Rows.Count - 1;

                                }
                                else
                                {
                                    #region  "To Print a Blank row when there is no payment done"
                                    
                                    c1DetailCharges.Rows.Add();
                                    rowIndex = c1DetailCharges.Rows.Count - 1;

                                    c1DetailCharges.SetData(rowIndex, COL_2, "Payment Source");
                                    c1DetailCharges.SetData(rowIndex, COL_3, "Payment Date");
                                    c1DetailCharges.SetData(rowIndex, COL_4, "Payment");
                                    c1DetailCharges.SetData(rowIndex, COL_5, "W/O");
                                    c1DetailCharges.SetData(rowIndex, COL_6, "Withhold");
                                    c1DetailCharges.SetData(rowIndex, COL_7, "Copay");
                                    c1DetailCharges.SetData(rowIndex, COL_8, "Co-Ins");
                                    c1DetailCharges.SetData(rowIndex, COL_9, "Ded");
                                    c1DetailCharges.SetData(rowIndex, COL_10, "PaymentTray");

                                    subPaymentsourceDetails = c1DetailCharges.GetCellRange(rowIndex, COL_1, rowIndex, COL_10);
                                    subPaymentsourceDetails.Style = csPaymentSource;


                                    c1DetailCharges.Rows.Add();
                                    rowIndex = c1DetailCharges.Rows.Count - 1;

                                    c1DetailCharges.SetData(rowIndex, COL_2, InsuranceName);
                                    c1DetailCharges.SetData(rowIndex, COL_3, PaymentDate);

                                    CrDetailsTotals =  c1DetailCharges.GetCellRange(rowIndex, COL_4, rowIndex, COL_9);
                                    CrDetailsTotals.Style = csCurrency;

                                    c1DetailCharges.SetData(rowIndex, COL_4, d_InsurancePayment, true);
                                    c1DetailCharges.SetData(rowIndex, COL_5, d_ContAdj, true);
                                    c1DetailCharges.SetData(rowIndex, COL_6, d_Withhold, true);
                                    c1DetailCharges.SetData(rowIndex, COL_7, d_Copay, true);
                                    c1DetailCharges.SetData(rowIndex, COL_8, d_CoIns, true);
                                    c1DetailCharges.SetData(rowIndex, COL_9, d_Deductible, true);
                                    c1DetailCharges.SetData(rowIndex, COL_10, PaymentTray, false);

                                    
                                    c1DetailCharges.Rows.Add();
                                    rowIndex = c1DetailCharges.Rows.Count - 1;



                                    CellRange subAdvDetailTotCaption;
                                    subAdvDetailTotCaption = c1DetailCharges.GetCellRange(rowIndex, COL_3, rowIndex, COL_3);
                                    subAdvDetailTotCaption.Style = csGrandTotalTitleRow;
                                    c1DetailCharges.SetData(rowIndex, COL_3, "Total :",true);

                                    CellRange subAdvDetailTot;
                                    subAdvDetailTot = c1DetailCharges.GetCellRange(rowIndex, COL_4, rowIndex, COL_9);
                                    subAdvDetailTot.Style = csSubTotalRow;

                                    c1DetailCharges.SetData(rowIndex, COL_4, dTotal_InsurancePayment, true);
                                    c1DetailCharges.SetData(rowIndex, COL_5, dTotal_ContAdj, true);
                                    c1DetailCharges.SetData(rowIndex, COL_6, dTotal_WithHold, true);
                                    c1DetailCharges.SetData(rowIndex, COL_7, dTotal_Copay, true);
                                    c1DetailCharges.SetData(rowIndex, COL_8, dTotal_CoIns, true);
                                    c1DetailCharges.SetData(rowIndex, COL_9, dTotal_Deductible, true);

                                    dTotal_InsurancePayment = 0;
                                    dTotal_ContAdj = 0;
                                    dTotal_WithHold = 0;
                                    dTotal_Copay = 0;
                                    dTotal_CoIns = 0;
                                    dTotal_Deductible = 0;
                                    //dTotal_PatientPayment = 0;

                                    c1DetailCharges.Rows.Add();
                                    rowIndex = c1DetailCharges.Rows.Count - 1; 

                                    #endregion

                                }
                            } 

                            #endregion

                        }
                        dtlsLastclaimNo = dtClaimDetails.Rows[i]["nTransactionDetailID"].ToString();
                    }
                }

                c1DetailCharges.Rows.Add();
                rowIndex = c1DetailCharges.Rows.Count - 1;

                FetchAdvancePayment(c1DetailCharges,0);

                if (oDB != null) { oDB.Dispose(); };
    

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null; 
            }
 
        }

        private void FetchAdvancePayment(C1FlexGrid c1Grid,int Mode)
        {

            try
            {
                DataTable dtAdvancePayment = null;
                int rowIndex = 0;
                rowIndex = c1Grid.Rows.Count - 1;

                _30DaysAdvancePaid = 0;
                _60DaysAdvancePaid = 0;
                _90DaysAdvancePaid = 0;
                _120DaysAdvancePaid = 0;
                _Above120DaysAdvancePaid = 0;
                     
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                gloDatabaseLayer.DBParameters oDBParameters3 = new gloDatabaseLayer.DBParameters();
                oDBParameters3.Add("@nPatientID", Convert.ToInt64(_PatientID), ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters3.Add("@nMode", Convert.ToInt32(Mode), ParameterDirection.Input, SqlDbType.Int);
                oDB.Retrive("BL_SELECT_EOBLedgerAdvancePayment", oDBParameters3, out  dtAdvancePayment);
                if (dtAdvancePayment != null)
                {
                    if (dtAdvancePayment.Rows.Count > 0)
                    {
                        c1ClaimGrid.Rows.Add();
                        rowIndex = c1ClaimGrid.Rows.Count - 1;


                        c1ClaimGrid.Rows.Add();
                        rowIndex = c1ClaimGrid.Rows.Count - 1;

                        c1Grid.SetData(rowIndex, COL_2, "Personal Payment Type");
                        c1Grid.SetData(rowIndex, COL_3, "Payment Date");

                       
                        c1Grid.SetData(rowIndex, COL_4, "Amount",true);
                        c1Grid.SetData(rowIndex, COL_5, "Payment Tray");
                        c1Grid.SetData(rowIndex, COL_6, "Check/CC/Mo#");

                        subPaymentsource = c1Grid.GetCellRange(rowIndex, COL_2, rowIndex, COL_6);
                        subPaymentsource.Style = csAdvancePayment;


                        c1Grid.Rows.Add();
                        rowIndex = c1Grid.Rows.Count - 1;

                        decimal _AdvancePaid = 0;

                        for (int k = 0; k < dtAdvancePayment.Rows.Count; k++)
                        {
                            c1Grid.SetData(rowIndex, COL_2, Convert.ToString(dtAdvancePayment.Rows[k]["PaymentMode"]));
                            c1Grid.SetData(rowIndex, COL_3, Convert.ToString(dtAdvancePayment.Rows[k]["nPaymentDate"]));

                            CrDollars =  c1Grid.GetCellRange(rowIndex, COL_4, rowIndex, COL_4);
                            CrDollars.Style = csCurrency;

                            c1Grid.SetData(rowIndex, COL_4, Convert.ToString(dtAdvancePayment.Rows[k]["AmountPaid"]),true);

                              _AdvancePaid = _AdvancePaid + Convert.ToDecimal(dtAdvancePayment.Rows[k]["AmountPaid"]);

                                DateTime dateAdvancePay = Convert.ToDateTime(dtAdvancePayment.Rows[k]["nPaymentDate"]);

                                if (dateAdvancePay <= DateTime.Now.Date && dateAdvancePay >= DateTime.Now.Subtract(new TimeSpan(30, 0, 0, 0)))
                                {
                                    _30DaysAdvancePaid += Convert.ToDecimal(dtAdvancePayment.Rows[k]["AmountPaid"]);
                                }
                                else if (dateAdvancePay < DateTime.Now.Subtract(new TimeSpan(30, 0, 0, 0)) && dateAdvancePay >= DateTime.Now.Subtract(new TimeSpan(60, 0, 0, 0)))
                                {
                                    _60DaysAdvancePaid += Convert.ToDecimal(dtAdvancePayment.Rows[k]["AmountPaid"]);
                                }
                                else if (dateAdvancePay < DateTime.Now.Subtract(new TimeSpan(60, 0, 0, 0)) && dateAdvancePay >= DateTime.Now.Subtract(new TimeSpan(90, 0, 0, 0)))
                                {
                                    _90DaysAdvancePaid += Convert.ToDecimal(dtAdvancePayment.Rows[k]["AmountPaid"]);
                                }
                                else if (dateAdvancePay < DateTime.Now.Subtract(new TimeSpan(90, 0, 0, 0)) && dateAdvancePay >= DateTime.Now.Subtract(new TimeSpan(120, 0, 0, 0)))
                                {
                                    _120DaysAdvancePaid += Convert.ToDecimal(dtAdvancePayment.Rows[k]["AmountPaid"]);
                                }
                                else if (dateAdvancePay <= DateTime.Now.Subtract(new TimeSpan(120, 0, 0, 0)))
                                {
                                    _Above120DaysAdvancePaid += Convert.ToDecimal(dtAdvancePayment.Rows[k]["AmountPaid"]);
                                }
                        
                           
                            c1Grid.SetData(rowIndex, COL_5, Convert.ToString(dtAdvancePayment.Rows[k]["PaymentTray"]));
                            c1Grid.SetData(rowIndex, COL_6, Convert.ToString(dtAdvancePayment.Rows[k]["CrChMoNo"]),false);

                            c1Grid.Rows.Add();
                            rowIndex = c1Grid.Rows.Count - 1;

                        }

                        CellRange subAdvanceTotCaption;
                        subAdvanceTotCaption = c1Grid.GetCellRange(rowIndex, COL_3, rowIndex, COL_3);
                        subAdvanceTotCaption.Style = csGrandTotalTitleRow;
                        c1Grid.SetData(rowIndex, COL_3, "Total : ",true);

                        CellRange subAdvanceTot;
                        subAdvanceTot = c1Grid.GetCellRange(rowIndex, COL_4, rowIndex, COL_4);
                        subAdvanceTot.Style = csSubTotalRow;
                        c1Grid.SetData(rowIndex, COL_4, _AdvancePaid,true);
                        
                        c1Grid.Rows.Add();
                        rowIndex = c1Grid.Rows.Count - 1;

                    }
                }
                if (oDB != null) { oDB.Dispose(); };
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;   
            }
        }

        #endregion


        #region " Tool Strip Button Click Event "

        private void ts_btnOk_Click(object sender, EventArgs e)
        {

        }

        private void ts_btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsb_PendingCoPay_Click(object sender, EventArgs e)
        {
            //frmRpt_PendingCoPay ofrmRpt_PendingCoPay = new frmRpt_PendingCoPay(this.PatientID, _databaseconnectionstring);
            //ofrmRpt_PendingCoPay.ShowDialog(this);
            //ofrmRpt_PendingCoPay.Dispose();
        }

        private void tsb_Refresh_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToString(tabClaims.SelectedTab.Tag).ToUpper() == _TagClaims.ToUpper())
                {
                    DesignChargesGrid(c1ClaimGrid);
                    DesignPatientDetialsGrid();
                    FillClaimsNew();
                    //FillClaimsNew();
                }
                else if (Convert.ToString(tabClaims.SelectedTab.Tag).ToUpper() == _TagDetails.ToUpper())
                {
                    DesignChargesGrid(c1DetailCharges);
                    FillClaimDetails();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;   
            }
        }

        private void tsb_ShowHideZeroBalance_Click(object sender, EventArgs e)
        {
           
        }

        #endregion " Tool Strip Button Click Event "


        #region "C1 Grid Events"
        
        private void C1PatientDetails_Resize(object sender, EventArgs e)
        {
            try
            {
                #region "Width"

                int nWidth = pnlPatDetails.Width - 2;
                C1PatientDetails.Cols[COLPAT_1].Width = Convert.ToInt32((nWidth * 0.5));
                C1PatientDetails.Cols[COLPAT_2].Width = Convert.ToInt32((nWidth * 0.5));
                C1PatientDetails.Cols[COLPAT_3].Width = 0;

                #endregion
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null; 
            }
        }
 
        #endregion


        #region " Tab Events "
        
        private void tabClaims_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToString(tabClaims.SelectedTab.Tag).ToUpper() == _TagClaims.ToUpper())
            {
                DesignChargesGrid(c1ClaimGrid);
                DesignPatientDetialsGrid();
                FillClaimsNew();
                //FillClaimsNew();
                tsb_Modify.Visible = true;
            }
            else if (Convert.ToString(tabClaims.SelectedTab.Tag).ToUpper() == _TagDetails.ToUpper())
            {
                DesignChargesGrid(c1DetailCharges);
                FillClaimDetails();
                tsb_Modify.Visible = false;
            }
        }
 
        #endregion

        private void tsb_Modify_Click(object sender, EventArgs e)
        {

            try
            {
                ModifyCharge();
                //Int64 _transactionId = 0;
                //gloBilling ogloBilling = new gloBilling(_databaseconnectionstring, "");

                //if (Convert.ToString(tabClaims.SelectedTab.Tag).ToUpper() == _TagClaims.ToUpper())
                //{

                   
                //    if (c1ClaimGrid.GetData(c1ClaimGrid.RowSel, c1ClaimGrid.Cols[COL_12].Index) != null && Convert.ToString(c1ClaimGrid.GetData(c1ClaimGrid.RowSel, c1ClaimGrid.Cols[COL_12].Index)) != "")
                //    {
                //        _transactionId = Convert.ToInt64(c1ClaimGrid.GetData(c1ClaimGrid.RowSel, c1ClaimGrid.Cols[COL_12].Index));
                //        if (_transactionId > 0 && _PatientID > 0)
                //        {

                //            DataTable _dtIsVoid = CheckIsVoided(_transactionId);

                //            if (_dtIsVoid != null && _dtIsVoid.Rows.Count > 0)
                //            {
                //                if (Convert.ToBoolean(_dtIsVoid.Rows[0]["bIsVoid"]) == true)
                //                {
                //                    ogloBilling.ShowModifyCharges(_PatientID, _transactionId,true);
                //                }
                //                else
                //                {
                //                    ogloBilling.ShowModifyCharges(_PatientID, _transactionId);
                //                }
                //            }
                            
                           
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null; 
            }
        }


        private void ModifyCharge()
        {
            try
            {
                Int64 _ParamTransactionId = 0;
                Int64 _ParamClaimNo = 0;
                gloBilling ogloBilling = new gloBilling(_databaseconnectionstring, "");

                if (Convert.ToString(tabClaims.SelectedTab.Tag).ToUpper() == _TagClaims.ToUpper())
                {


                    if (c1ClaimGrid.GetData(c1ClaimGrid.RowSel, c1ClaimGrid.Cols[COL_12].Index) != null && Convert.ToString(c1ClaimGrid.GetData(c1ClaimGrid.RowSel, c1ClaimGrid.Cols[COL_12].Index)) != "")
                    {
                        _ParamTransactionId = Convert.ToInt64(c1ClaimGrid.GetData(c1ClaimGrid.RowSel, c1ClaimGrid.Cols[COL_12].Index));
                        _ParamClaimNo = Convert.ToInt64(c1ClaimGrid.GetData(c1ClaimGrid.RowSel, c1ClaimGrid.Cols[COL_11].Index));

                        //Int64 _transactionIdClaim = GetClaimMstTransactionID(_transactionId);
                        //Int64 _ModifyClaimTransactionID = GetClaimTransactionID(_ParamClaimNo, "");
                        Int64 _ModifyClaimTransactionID = 0;
                        if (_ParamTransactionId > 0 && _PatientID > 0)
                        {

                            DataTable _dtIsVoid = CheckIsVoided(_ParamTransactionId);
                            if (_dtIsVoid != null && _dtIsVoid.Rows.Count > 0)
                            {
                                if (Convert.ToBoolean(_dtIsVoid.Rows[0]["bIsVoid"]) == true)
                                {
                                    _ModifyClaimTransactionID = GetClaimTransactionID(_ParamClaimNo, "",true);
                                    ogloBilling.ShowModifyCharges(_PatientID, _ModifyClaimTransactionID, true, this);
                                }
                                else
                                {
                                    _ModifyClaimTransactionID = GetClaimTransactionID(_ParamClaimNo, "", false);
                                    ogloBilling.ShowModifyCharges(_PatientID, _ModifyClaimTransactionID, this);
                                }
                            }

                        }
                    }
                }
                if (ogloBilling != null)
                {
                    ogloBilling.Dispose();
                    ogloBilling = null;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null; 
            }
        }

        private void c1ClaimGrid_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                ModifyCharge();
                //Int64 _transactionId = 0;
                //gloBilling ogloBilling = new gloBilling(_databaseconnectionstring, "");

                //if (Convert.ToString(tabClaims.SelectedTab.Tag).ToUpper() == _TagClaims.ToUpper())
                //{


                //    if (c1ClaimGrid.GetData(c1ClaimGrid.RowSel, c1ClaimGrid.Cols[COL_12].Index) != null && Convert.ToString(c1ClaimGrid.GetData(c1ClaimGrid.RowSel, c1ClaimGrid.Cols[COL_12].Index)) != "")
                //    {
                //        _transactionId = Convert.ToInt64(c1ClaimGrid.GetData(c1ClaimGrid.RowSel, c1ClaimGrid.Cols[COL_12].Index));
                //        Int64 _transactionIdClaim = GetClaimMstTransactionID(_transactionId); 
                //        if (_transactionId > 0 && _PatientID > 0)
                //        {

                //            DataTable _dtIsVoid = CheckIsVoided(_transactionId);

                //            if (_dtIsVoid != null && _dtIsVoid.Rows.Count > 0)
                //            {
                //                //Int64 _transactionIdClaim = 0;

                //                if (Convert.ToBoolean(_dtIsVoid.Rows[0]["bIsVoid"]) == true)
                //                {
                //                    ogloBilling.ShowModifyCharges(_PatientID, _transactionIdClaim, true);
                //                }
                //                else
                //                {
                //                    ogloBilling.ShowModifyCharges(_PatientID, _transactionIdClaim);
                //                }
                //            }
                            
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null; 
            }

        }

        private long GetClaimMstTransactionID(long _transactionId)
        {

            #region "To Fetch the MasterTransactionID of Claim"

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            object _TransactionId = null;
            string strQuery = "";
            try
            {
                //strQuery = "select isnull(bIsVoid,0) as bIsVoid FROM BL_Transaction_Claim_MST where nTransactionID = " + _TransactionId + "";
                strQuery = "select nTransactionID FROM bl_transaction_claim_mst where nTransactionMasterID = " + _transactionId + " And nResponsibilityNo=1 And (nParentTransactionId=0 OR nParentTransactionId Is NULL) AND (nParentClaimNo='' OR nParentClaimNo IS NULL)";
                oDB.Connect(false);
                _TransactionId = oDB.ExecuteScalar_Query(strQuery);  
                oDB.Disconnect();
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
            }
            finally
            {
                oDB.Dispose();
            }
            if (_TransactionId != null)
                return Convert.ToInt64(_TransactionId);
            else
                return 0;
            #endregion
           // throw new Exception("The method or operation is not implemented.");
        }

        private long GetClaimTransactionID(Int64 _nClaimNo,string _subclaimNo,bool _isVoid)
        {

            #region "To Fetch the TransactionID of Claim"

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            object _TransactionId = null;
           // string strQuery = "";
            DataTable _dtTransID = null;
            try
            {
                oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                gloDatabaseLayer.DBParameters oDBPatameters = new gloDatabaseLayer.DBParameters();
                oDB.Connect(false);
                oDBPatameters.Add("@nClaimno",_nClaimNo , ParameterDirection.Input, SqlDbType.BigInt);
                oDBPatameters.Add("@sSubClaimno", _subclaimNo, ParameterDirection.Input, SqlDbType.VarChar);
                oDBPatameters.Add("@bIsVoid", _isVoid, ParameterDirection.Input, SqlDbType.Bit);
                oDB.Retrive("BL_Select_VoidSplitClaims", oDBPatameters, out _dtTransID);
                if (_dtTransID != null && _dtTransID.Rows.Count > 0)
                {
                        return Convert.ToInt64(_dtTransID.Rows[0]["nTransactionID"]);
                }
                oDB.Disconnect();
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
            }
            finally
            {
                oDB.Dispose();
            }
            if (_TransactionId != null)
                return Convert.ToInt64(_TransactionId);
            else
                return 0;
            #endregion
            // throw new Exception("The method or operation is not implemented.");
        }

        private string GetNextResponsibility(String _TransactionId, String _TransactionDetailID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            object _NextResponsibility = null;
            string strQuery = "";
            try
            {
                //strQuery = "select isnull(bIsVoid,0) as bIsVoid FROM BL_Transaction_Claim_MST where nTransactionID = " + _TransactionId + "";
                strQuery = "select nNextActionPatientInsName from dbo.BL_EOB_NextAction where nBillingTransactionId="+ _TransactionId + " and nBillingTransactionDetailId="+ _TransactionDetailID;
                oDB.Connect(false);
                _NextResponsibility = oDB.ExecuteScalar_Query(strQuery);
                oDB.Disconnect();
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
            }
            finally
            {
                oDB.Dispose();
            }
            if (_NextResponsibility != null)
                return Convert.ToString(_NextResponsibility);
            else
                return "";
        }

        private DataTable CheckIsVoided(Int64 _TransactionId)
        {
             #region "Check whether the Claim is Voided or Not"

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable _dt = null;
            string strQuery = "";
            try
            {
                //strQuery = "select isnull(bIsVoid,0) as bIsVoid FROM BL_Transaction_Claim_MST where nTransactionID = " + _TransactionId + "";
                strQuery = "select isnull(bIsVoid,0) as bIsVoid FROM BL_Transaction_Claim_MST where nTransactionMasterID = " + _TransactionId + "";
                oDB.Connect(false);
                oDB.Retrive_Query(strQuery, out _dt);
                oDB.Disconnect();
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
            }
            finally
            {
                oDB.Dispose();
            }
            return _dt;

            #endregion

        }

        private void FillPatientBalance(Int64 PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = null;
            string _sqlQuery = "";
            object _Result = new object();
            DataTable _dt = null;

            decimal _TotCharges = 0;
          //  decimal _TotInsPaid = 0;
          //  decimal _TotPatPaid = 0;
            decimal _totalInsResponsiblity = 0;
            decimal _totalPatResponsiblity = 0;
            decimal _totalBalAmt = 0;
            decimal _totalResAmt = 0;

            oDB.Connect(false);

            try
            {

                Decimal _sPendingCopy = 0;
                Decimal _sPendingAdvance = 0;
                Decimal _sPendingOtherReserved = 0;


                if (PatientID <= 0)
                { return; }


                //lblTotalCharges.Text = "$ " + Convert.ToDecimal(_TotCharges).ToString("#0.00");

                //lblInsurancePending.Text = "$ 0.00";
                //lblPatientPending.Text = "$ 0.00";
                //lblTotalBalance.Text = "$ 0.00";


                //Total Charges
                _sqlQuery = "SELECT  ISNULL(SUM(BL_Transaction_Lines.dTotal),0) AS TotalCharges " +
                " FROM  BL_Transaction_MST INNER JOIN BL_Transaction_Lines ON BL_Transaction_MST.nTransactionID = BL_Transaction_Lines.nTransactionID " +
                " WHERE  BL_Transaction_MST.nPatientID = " + PatientID + " AND BL_Transaction_MST.nClinicID = " + _ClinicID + " AND ISNULL(BL_Transaction_MST.bIsVoid,0) = 0 ";
                _Result = new object();
                _Result = oDB.ExecuteScalar_Query(_sqlQuery);
                if (_Result != null && Convert.ToString(_Result) != "")
                { _TotCharges = Convert.ToDecimal(_Result); }
                _Result = null;


                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@nPatientID", _PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

                _dt = new DataTable();


                oDB.Retrive("BL_GET_PATIENT_ACCOUNT_BALANCE_REVISED", oParameters, out _dt);

                if (_dt != null && _dt.Rows.Count > 0)
                {
                    if (_dt.Rows[0]["InsuranceDue"] != DBNull.Value && Convert.ToString(_dt.Rows[0]["InsuranceDue"]).Trim() != "")
                    { _totalInsResponsiblity = Convert.ToDecimal(_dt.Rows[0]["InsuranceDue"]); }
                    if (_dt.Rows[0]["PatientDue"] != DBNull.Value && Convert.ToString(_dt.Rows[0]["PatientDue"]).Trim() != "")
                    { _totalPatResponsiblity = Convert.ToDecimal(_dt.Rows[0]["PatientDue"]); }


                    if (_dt.Rows[0]["AvailableReserve"] != DBNull.Value && Convert.ToString(_dt.Rows[0]["AvailableReserve"]).Trim() != "")
                    { _totalResAmt = Convert.ToDecimal(_dt.Rows[0]["AvailableReserve"]); }
                }

                //lblInsurancePending.Text = "$ " + _totalInsResponsiblity.ToString("#0.00");


                _totalPatResponsiblity = _totalPatResponsiblity - _totalResAmt;
                //lblPatientPending.Text = "$ " + _totalPatResponsiblity.ToString("#0.00");


                _totalBalAmt = _totalInsResponsiblity + _totalPatResponsiblity;
                //lblTotalBalance.Text = "$ " + _totalBalAmt.ToString("#0.00");


                //_TotPatPaid = 0;

                #region "Pending Copay, Advance and Reserve"
                //lblPendingCopay.Text = "$ 0.00";
                //lblPendingAdvance.Text = "$ 0.00";
                //lblPendingOtherReserved.Text = "$ 0.00";

                _sqlQuery = "select nPaymentNoteSubType,sum(AvailableReserve) as AvailableReserve from " +
                " ( " +
                    " SELECT  " +
                    " ISNULL(BL_EOB_Notes.nPaymentNoteSubType,'') AS nPaymentNoteSubType, " +
                    " ISNULL( " +
                    " ( " +
                    " (BL_EOBPayment_DTL.nAmount) -    " +
                    " ISNULL( " +
                    " ( " +
                    " SELECT " +
                    " ( " +
                    " ( " +
                    " SELECT SUM(ISNULL(nAmount,0))  " +
                    " FROM BL_EOBPayment_DTL AS BL_EOBPayment_DTL_UseRes    " +
                    " WHERE  " +
                    " BL_EOBPayment_DTL_UseRes.nResEOBPaymentID = BL_EOBPayment_DTL.nEOBPaymentID  " +
                    " AND   BL_EOBPayment_DTL_UseRes.nResEOBPaymentDetailID = BL_EOBPayment_DTL.nEOBPaymentDetailID " +
                    " AND   (BL_EOBPayment_DTL_UseRes.nPaymentType = 6 AND BL_EOBPayment_DTL_UseRes.nPaymentSubType = 8 AND nPaySign = 2) " +
                    " ) " +
                    " ) " +
                    " ) " +
                    " ,0)),0) AS AvailableReserve " +
                    " FROM         BL_EOBPayment_DTL INNER JOIN " +
                    " BL_EOBPayment_MST ON BL_EOBPayment_DTL.nEOBPaymentID = BL_EOBPayment_MST.nEOBPaymentID INNER JOIN " +
                    " Patient ON BL_EOBPayment_DTL.nPatientID = Patient.nPatientID INNER JOIN " +
                    " BL_EOB_Notes ON BL_EOBPayment_DTL.nEOBPaymentID = BL_EOB_Notes.nEOBPaymentID AND  " +
                    " BL_EOBPayment_DTL.nEOBPaymentDetailID = BL_EOB_Notes.nEOBPaymentDetailID " +
                    " WHERE " +
                    " BL_EOBPayment_DTL.npaymenttype = 2 AND BL_EOBPayment_DTL.npaymentsubtype = 9 AND BL_EOBPayment_DTL.npaysign = 2 " +
                    " AND BL_EOBPayment_DTL.npatientid = " + _PatientID + " AND BL_EOBPayment_DTL.nClinicID = " + _ClinicID + " " +
                " ) " +
                " as Final	GROUP BY nPaymentNoteSubType ";

                DataTable oCARData = new DataTable();
                oDB.Retrive_Query(_sqlQuery, out oCARData);
                if (oCARData != null && oCARData.Rows.Count > 0)
                {
                    for (int i = 0; i <= oCARData.Rows.Count - 1; i++)
                    {
                        if (oCARData.Rows[i]["nPaymentNoteSubType"] != null && oCARData.Rows[i]["nPaymentNoteSubType"].ToString().Trim() != "")
                        {
                            decimal _Pending = 0;

                            if (oCARData.Rows[i]["AvailableReserve"] != null && oCARData.Rows[i]["AvailableReserve"].ToString().Trim() != "")
                            {
                                _Pending = Convert.ToDecimal(Convert.ToString(oCARData.Rows[i]["AvailableReserve"]));
                            }
                            if (_Pending > 0)
                            {
                                if (Convert.ToInt32(oCARData.Rows[i]["nPaymentNoteSubType"]) == 2)
                                {
                                    _sPendingCopy = _Pending;
                                }
                                else if (Convert.ToInt32(oCARData.Rows[i]["nPaymentNoteSubType"]) == 3)
                                {
                                    _sPendingAdvance = _Pending;
                                }
                                else if (Convert.ToInt32(oCARData.Rows[i]["nPaymentNoteSubType"]) == 10)
                                {
                                    _sPendingOtherReserved = _Pending;
                                }
                            }
                        }
                    }
                }


                #endregion


                int _rowIndex = 0;

                C1PatientDetails.Cols[COLPAT_2].Style = csCurrency;
                C1PatientDetails.Cols[COLPAT_1].Style = csBalanceContents;

                C1PatientDetails.Rows.Add();
                _rowIndex = C1PatientDetails.Rows.Count - 1;


                C1PatientDetails.SetData(_rowIndex, COLPAT_1, "Total Balance ");
                C1PatientDetails.SetData(_rowIndex, COLPAT_2, _totalBalAmt);
                C1PatientDetails.Rows.Add();
                _rowIndex = C1PatientDetails.Rows.Count - 1;

                C1PatientDetails.SetData(_rowIndex, COLPAT_1, "Ins. Pending ");
                C1PatientDetails.SetData(_rowIndex, COLPAT_2, _totalInsResponsiblity);
                C1PatientDetails.Rows.Add();
                _rowIndex = C1PatientDetails.Rows.Count - 1;


                C1PatientDetails.SetData(_rowIndex, COLPAT_1, "Pat. Due ");
                C1PatientDetails.SetData(_rowIndex, COLPAT_2, _totalPatResponsiblity);
                C1PatientDetails.Rows.Add();
                _rowIndex = C1PatientDetails.Rows.Count - 1;


                C1PatientDetails.SetData(_rowIndex, COLPAT_1, "Copay ");
                C1PatientDetails.SetData(_rowIndex, COLPAT_2, _sPendingCopy);
                C1PatientDetails.Rows.Add();
                _rowIndex = C1PatientDetails.Rows.Count - 1;


                C1PatientDetails.SetData(_rowIndex, COLPAT_1, "Advance ");
                C1PatientDetails.SetData(_rowIndex, COLPAT_2, _sPendingAdvance);
                C1PatientDetails.Rows.Add();
                _rowIndex = C1PatientDetails.Rows.Count - 1;


                C1PatientDetails.SetData(_rowIndex, COLPAT_1, "Other Resv ");
                C1PatientDetails.SetData(_rowIndex, COLPAT_2, _sPendingOtherReserved);




                //CellRange CrDollars3 = new CellRange();
                //CrDollars3 = C1PatientDetails.GetCellRange(_rowIndex, COL_2, _rowIndex, COL_3);
                //CrDollars3.Style = csCurrency;

                //c1ClaimGrid.SetData(rowIndex, COL_7, Convert.ToString(dtClaims.Rows[i]["dTotal"]), true);


                //CellRange subPatTotalCaption;
                //subPatTotalCaption = C1PatientDetails.GetCellRange(_rowIndex, COL_1, _rowIndex, COL_1);
                //subPatTotalCaption.Style = csCurrency;


                //csCurrency = c1Grid.Styles.Add("csCurrencyCell");
                //csCurrency.DataType = typeof(System.Decimal);
                //csCurrency.Format = "C";
                //csCurrency.ForeColor = Color.Navy;
                //csCurrency.Font = new System.Drawing.Font("Tahoma", 8.62F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                //csCurrency.TextAlign = TextAlignEnum.RightCenter;



                //CellRange subPatTot;
                //subPatTot = C1PatientDetails.GetCellRange(_rowIndex, COL_2, _rowIndex, COL_2);
                //subPatTot.Style = csSubTotalRow;


            }
            catch (gloDatabaseLayer.DBException oDBEx)
            {
                oDBEx.ERROR_Log(oDBEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }
        }

     

    }
}