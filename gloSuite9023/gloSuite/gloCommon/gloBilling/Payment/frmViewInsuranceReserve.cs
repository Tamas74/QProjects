using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;

namespace gloBilling
{
    public partial class frmViewInsuranceReserve: Form
    {

        #region " Private Variables "

        private Int64 _InsuranceCompanyID = 0;
        private Int64 _nEOBPaymentID = 0;
        private Int64 _nResEOBPaymentDetailID = 0;
        private Int64 _ClinicID = 0;
        Int64 _UserId = 0;
        string _UserName = "";
        private DateTime _closeDate = DateTime.Now;
        private string _closeDayTray = "";

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private string _databaseconnectionstring = "";
        private string _MessageBoxCaption = "";
        
        // Unused variables need to delete        
    //    private bool _IsFormLoading = false;
        public decimal SelectedUseReserveAmount = 0;
        public gloGeneralItem.gloItems oSeletedReserveItems = new gloGeneralItem.gloItems();
        public EOBPayment.Common.EOBInsurancePaymentMasterAllocationLines EOBInsurancePaymentMasterLines = null;

        #endregion " Private Variables "

        #region  " Grid Constants "

        //nEOBPaymentID, nEOBID, nEOBDtlID, nEOBPaymentDetailID, 
        //nBillingTransactionID, nBillingTransactionDetailID, nBillingTransactionLineNo, 
        //nPatientID, nDOSFrom, nDOSTo, nAmount, nPayMode, 
        //nRefEOBPaymentID, nRefEOBPaymentDetailID, 
        //nAccountID, nAccountType, nMSTAccountID, nMSTAccountType 

       
        const int COL_SOURCE = 0; // Insurance Name will come
        const int COL_ORIGINALPAYMENT = 1;//Check Number,Date,Amount
        const int COL_TORESERVES = 2;//Amount for reserve
        const int COL_NOTE = 3;//Note
        const int COL_AVAILABLE = 4;//Available amount

        const int COL_COUNT = 5;

        #endregion 

        #region " Property Procedures "

        public Int64 InsuranceCompanyID
        {
            get { return _InsuranceCompanyID; }
            set { _InsuranceCompanyID = value; }
        }
        public Int64 ClinicID
        { 
            get { return _ClinicID; } 
            set { _ClinicID = value; } 
        }
        public Int64  UserID
        { 
            get { return _UserId; } 
            set { _UserId = value; } 
        }
        public string UserName
        { 
            get { return _UserName; } 
            set { _UserName = value; } 
        }
        public DateTime CloseDate
        {
            get { return _closeDate; }
            set { _closeDate = value; }
        }
        public string CloseDayTray
        {
            get { return _closeDayTray; }
            set { _closeDayTray = value; }
        }

        #endregion " Property Procedures "

        #region " Constructors "

        public frmViewInsuranceReserve(string DatabaseConnectionString, Int64 EOPaymentID, Int64 InsuranceCompanyID, Int64 ResEOBPaymentDetailID)
        {
            InitializeComponent();
            _databaseconnectionstring = DatabaseConnectionString;
            _InsuranceCompanyID =InsuranceCompanyID;
            _nEOBPaymentID = EOPaymentID;
            _nResEOBPaymentDetailID = ResEOBPaymentDetailID;
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
            gloC1FlexStyle.Style(c1Reserve, false);
            FillReserves();
     
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

        private void DesignPaymentGrid(C1FlexGrid c1Reserve)
        {
            try
            {
                //_IsFormLoading = true;
                c1Reserve.Redraw = false;
                c1Reserve.AllowSorting = AllowSortingEnum.None;

                c1Reserve.Clear();
                c1Reserve.Cols.Count = COL_COUNT;
                c1Reserve.Rows.Count = 1;
                c1Reserve.Rows.Fixed = 1;
                c1Reserve.Cols.Fixed = 0;

                #region " Set Headers "

                c1Reserve.SetData(0, COL_SOURCE,"Insurance Company");// Insurance Name
                c1Reserve.SetData(0, COL_ORIGINALPAYMENT,"Original Payment");//Check Number,Date,Amount
                c1Reserve.SetData(0, COL_TORESERVES,"To Reserves");//Amount for reserve
                c1Reserve.SetData(0, COL_NOTE,"Note");//Note
                c1Reserve.SetData(0, COL_AVAILABLE,"Available");//Available amount

                #endregion

                #region " Show/Hide "

                c1Reserve.Cols[COL_SOURCE].Visible = true;
                c1Reserve.Cols[COL_ORIGINALPAYMENT].Visible = true;
                c1Reserve.Cols[COL_TORESERVES].Visible = true;
                c1Reserve.Cols[COL_NOTE].Visible = true;
                c1Reserve.Cols[COL_AVAILABLE].Visible = true;

                #endregion

                #region " Width "
                bool _designWidth = false;

                if (_designWidth == false)
                {
                    c1Reserve.Cols[COL_SOURCE].Width = 150;
                    c1Reserve.Cols[COL_ORIGINALPAYMENT].Width = 250;
                    c1Reserve.Cols[COL_TORESERVES].Width = 80;
                    c1Reserve.Cols[COL_NOTE].Width = 250;
                    c1Reserve.Cols[COL_AVAILABLE].Width = 75;
                }

                #endregion

                #region " Data Type "

                c1Reserve.Cols[COL_SOURCE].DataType = typeof(System.String);
                c1Reserve.Cols[COL_ORIGINALPAYMENT].DataType = typeof(System.String);
                c1Reserve.Cols[COL_TORESERVES].DataType = typeof(System.Decimal);
                c1Reserve.Cols[COL_NOTE].DataType = typeof(System.String);
                c1Reserve.Cols[COL_AVAILABLE].DataType = typeof(System.Decimal);

                #endregion

                #region " Alignment "

                c1Reserve.Cols[COL_SOURCE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Reserve.Cols[COL_ORIGINALPAYMENT].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Reserve.Cols[COL_TORESERVES].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Reserve.Cols[COL_NOTE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Reserve.Cols[COL_AVAILABLE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;

                #endregion

                #region " Set Styles "

                C1.Win.C1FlexGrid.CellStyle csCurrencyStyle;// = c1Reserve.Styles.Add("cs_CurrencyStyle");
                try
                {
                    if (c1Reserve.Styles.Contains("cs_CurrencyStyle"))
                    {
                        csCurrencyStyle = c1Reserve.Styles["cs_CurrencyStyle"];
                    }
                    else
                    {
                        csCurrencyStyle = c1Reserve.Styles.Add("cs_CurrencyStyle");
                        csCurrencyStyle.DataType = typeof(System.Decimal);
                        csCurrencyStyle.Format = "c";
                        csCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;
                    }

                }
                catch
                {
                    csCurrencyStyle = c1Reserve.Styles.Add("cs_CurrencyStyle");
                    csCurrencyStyle.DataType = typeof(System.Decimal);
                    csCurrencyStyle.Format = "c";
                    csCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;

                }
      

                C1.Win.C1FlexGrid.CellStyle csEditableCurrencyStyle;// = c1Reserve.Styles.Add("cs_EditableCurrencyStyle");
                try
                {
                    if (c1Reserve.Styles.Contains("cs_EditableCurrencyStyle"))
                    {
                        csEditableCurrencyStyle = c1Reserve.Styles["cs_EditableCurrencyStyle"];
                    }
                    else
                    {
                        csEditableCurrencyStyle = c1Reserve.Styles.Add("cs_EditableCurrencyStyle");
                        csEditableCurrencyStyle.DataType = typeof(System.Decimal);
                        csEditableCurrencyStyle.Format = "c";
                        csEditableCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csEditableCurrencyStyle.BackColor = Color.White;
                    }

                }
                catch
                {
                    csEditableCurrencyStyle = c1Reserve.Styles.Add("cs_EditableCurrencyStyle");
                    csEditableCurrencyStyle.DataType = typeof(System.Decimal);
                    csEditableCurrencyStyle.Format = "c";
                    csEditableCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csEditableCurrencyStyle.BackColor = Color.White;

                }
            

                C1.Win.C1FlexGrid.CellStyle csEditableStringStyle;// = c1Reserve.Styles.Add("cs_EditableStringStyle");
                try
                {
                    if (c1Reserve.Styles.Contains("cs_EditableStringStyle"))
                    {
                        csEditableStringStyle = c1Reserve.Styles["cs_EditableStringStyle"];
                    }
                    else
                    {
                        csEditableStringStyle = c1Reserve.Styles.Add("cs_EditableStringStyle");
                        csEditableStringStyle.DataType = typeof(System.String);
                        csEditableStringStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csEditableStringStyle.BackColor = Color.White;
                    }

                }
                catch
                {
                    csEditableStringStyle = c1Reserve.Styles.Add("cs_EditableStringStyle");
                    csEditableStringStyle.DataType = typeof(System.String);
                    csEditableStringStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csEditableStringStyle.BackColor = Color.White;

                }
          

                C1.Win.C1FlexGrid.CellStyle csEditableDateStyle;// = c1Reserve.Styles.Add("cs_EditableDateStyle");
                try
                {
                    if (c1Reserve.Styles.Contains("cs_EditableDateStyle"))
                    {
                        csEditableDateStyle = c1Reserve.Styles["cs_EditableDateStyle"];
                    }
                    else
                    {
                        csEditableDateStyle = c1Reserve.Styles.Add("cs_EditableDateStyle");
                        csEditableDateStyle.DataType = typeof(System.DateTime);
                        csEditableDateStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csEditableDateStyle.BackColor = Color.White;
                    }

                }
                catch
                {
                    csEditableDateStyle = c1Reserve.Styles.Add("cs_EditableDateStyle");
                    csEditableDateStyle.DataType = typeof(System.DateTime);
                    csEditableDateStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csEditableDateStyle.BackColor = Color.White;

                }
            

                C1.Win.C1FlexGrid.CellStyle csClaimRowStyle;// = c1Reserve.Styles.Add("cs_ClaimRowStyle");
                try
                {
                    if (c1Reserve.Styles.Contains("cs_ClaimRowStyle"))
                    {
                        csClaimRowStyle = c1Reserve.Styles["cs_ClaimRowStyle"];
                    }
                    else
                    {
                        csClaimRowStyle = c1Reserve.Styles.Add("cs_ClaimRowStyle");
                        csClaimRowStyle.DataType = typeof(System.String);
                        csClaimRowStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csClaimRowStyle.BackColor = Color.FromArgb(229, 224, 236);
                    }

                }
                catch
                {
                    csClaimRowStyle = c1Reserve.Styles.Add("cs_ClaimRowStyle");
                    csClaimRowStyle.DataType = typeof(System.String);
                    csClaimRowStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csClaimRowStyle.BackColor = Color.FromArgb(229, 224, 236);

                }
             

                C1.Win.C1FlexGrid.CellStyle csPatientRowStyle;// = c1Reserve.Styles.Add("cs_PatientRowStyle");
                try
                {
                    if (c1Reserve.Styles.Contains("cs_PatientRowStyle"))
                    {
                        csPatientRowStyle = c1Reserve.Styles["cs_PatientRowStyle"];
                    }
                    else
                    {
                        csPatientRowStyle = c1Reserve.Styles.Add("cs_PatientRowStyle");
                        csPatientRowStyle.DataType = typeof(System.String);
                        csPatientRowStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csPatientRowStyle.BackColor = Color.FromArgb(215, 228, 188);
                    }

                }
                catch
                {
                    csPatientRowStyle = c1Reserve.Styles.Add("cs_PatientRowStyle");
                    csPatientRowStyle.DataType = typeof(System.String);
                    csPatientRowStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csPatientRowStyle.BackColor = Color.FromArgb(215, 228, 188);

                }
            

                c1Reserve.Cols[COL_TORESERVES].Style = csCurrencyStyle;

                c1Reserve.Cols[COL_AVAILABLE].Style = csCurrencyStyle;
                //c1Reserve.Cols[COL_USENOW].Style = csCurrencyStyle;
                
                #endregion

                c1Reserve.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
                c1Reserve.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns;

                #region " Allow Editing "

                c1Reserve.AllowEditing = false;

                #endregion

                //c1Reserve.VisualStyle = VisualStyle.Office2007Blue;
                //c1Reserve.Styles[CellStyleEnum.SelectedColumnHeader].ForeColor = Color.FromArgb(31, 73, 125);
                //c1Reserve.Styles[CellStyleEnum.Fixed].ForeColor = Color.FromArgb(31, 73, 125);
                //c1Reserve.Styles[CellStyleEnum.Alternate].ForeColor = Color.FromArgb(31, 73, 125);

            }
            catch (Exception )//ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            { 
               // IsFormLoading = false; 
                c1Reserve.Redraw = true; }
        }

        #endregion " Design Grid "

        DataTable GetReserves()
        {
            DataTable _dtReserves = new DataTable();

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
             //string _strQuery = "";
             //       _strQuery = " SELECT ISNULL(nResEOBPaymentDetailID,0) AS nResEOBPaymentDetailID FROM BL_EOBPayment_DTL"
             //                 + " WHERE nEOBPaymentID = " + _nRefEOBPaymentID + " AND  (nPaymentType = 5 AND nPaymentSubType = 14 AND nPaySign = 2)";

             //       oDB.Connect(false);
             //       string _nResEOBPaymentDetailID =oDB.ExecuteScalar_Query(_strQuery).ToString();
             //       _nResEOBPaymentDetailID = (_nResEOBPaymentDetailID.Trim() == "" ? "0" : _nResEOBPaymentDetailID);

            try
            {
                oParameters.Add("@nInsuranceID", _InsuranceCompanyID, ParameterDirection.Input, SqlDbType.BigInt);//NUMERIC(18,0)
                oParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);// NUMERIC(18,0),
                oParameters.Add("@nEOBPaymentID", _nResEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);// NUMERIC(18,0),
                oDB.Connect(false);
                oDB.Retrive("BL_SELECT_Reserve_InsuranceDetails", oParameters, out _dtReserves);
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false); }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_dtReserves != null) { _dtReserves.Dispose(); }
            } 

            return _dtReserves;
        }

        private void FillReserves()
        {
            //_IsFormLoading = true;
            DataTable _dtReserves = GetReserves();
            DesignPaymentGrid(c1Reserve);
            decimal _avaible = 0;
            decimal _dbReserves = 0;

            try
            {

                int _rowIndex = 0;

                foreach (DataRow row in _dtReserves.Rows)
                {
                    _rowIndex = c1Reserve.Rows.Add().Index;
                    _avaible = 0;
                    _dbReserves = 0;

                    #region " Set Data "
                    c1Reserve.SetData(_rowIndex, COL_SOURCE, Convert.ToString(row["InsuarnceCompanyName"]));// Insurance Name

                    string _originalPayment = "";
                    _originalPayment = ((EOBPaymentMode)Convert.ToInt32(row["nPayMode"])).ToString() + "# " + Convert.ToString(row["CheckNumber"]) + " " + gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(row["nCheckDate"])).ToString("MM/dd/yyyy") + " $ " + Convert.ToDecimal(row["nCheckAmount"]);
                    c1Reserve.SetData(_rowIndex, COL_ORIGINALPAYMENT, _originalPayment);//Check Number,Date,Amount

                    c1Reserve.SetData(_rowIndex, COL_TORESERVES, Convert.ToDecimal(row["nAmount"]));
                    //c1Reserve.SetData(_rowIndex, COL_TYPE, ((EOBPaymentSubType)Convert.ToInt32(row["nPaymentNoteSubType"])).ToString());//Copay,Advance,Other
                    c1Reserve.SetData(_rowIndex, COL_NOTE, Convert.ToString(row["sNoteDescription"]));//Note

                    if (Convert.ToDecimal(row["AvailableReserve"]) + _dbReserves <= Convert.ToDecimal(row["nAmount"]))
                    { _avaible = Convert.ToDecimal(row["AvailableReserve"]) + _dbReserves; }
                    else
                    { _avaible = Convert.ToDecimal(row["AvailableReserve"]); }

                    c1Reserve.SetData(_rowIndex, COL_AVAILABLE, _avaible);//Available amount

                    #endregion
                    
                    #region " Set Styles "

                    //c1Reserve.SetCellStyle(_rowIndex, COL_USENOW, c1Reserve.Styles["cs_EditableCurrencyStyle"]);

                    #endregion " Set Styles "

                }
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false); }
            finally
            {
                //_IsFormLoading = false; 
            } 
        }
    }
}