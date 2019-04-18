using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using gloBilling.EOBPayment;

namespace gloBilling
{
    public partial class frmEOBExistingPatientPayment : Form
    {


        #region " Variable Declarations "
        gloPatientStripControl.gloPatientStripControl oPatientControl = null;
        private System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationSettings.AppSettings;
        Int64 _ClinicID = 1;
        Int64 _UserId = 0;
        string _UserName = "";
        public Int64 EOBPaymentID = 0;
        public Int64 EOBPatientID = 0;
        string _nCloseDate = "";
        public string EOBPatientCode = "";
        private Int64 _PatientID;
        string _databaseconnectionstring = "";
        string _messageboxcaption = string.Empty;

        #endregion " Variable Declarations "


        #region Constructor

        public frmEOBExistingPatientPayment(Int64 PatientID, Int64 _EOBPaymentID)
        {
            InitializeComponent();
            EOBPaymentID = _EOBPaymentID;
            #region " Retrieve ClinicID from AppSettings "

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 1; }
            }
            else
            { _ClinicID = 1; }

            #endregion " Retrieve ClinicID from AppSettings "

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageboxcaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageboxcaption = "gloPM"; ;
                }
            }
            else
            { _messageboxcaption = "gloPM"; ; }

            #endregion

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

            _PatientID = PatientID;

            _databaseconnectionstring = Convert.ToString(appSettings["DataBaseConnectionString"]);
        }

        #endregion

      
        #region "Grid Constant"

        const int COL_EOBPaymentID=0;
        const int COL_MSTAccountID=1;
        const int COL_MSTAccountType=2;
        const int COL_PaymentNo = 5;
       
        const int COL_PaymentTrayDescription=3;
        const int   COL_PayerType=11;
        const int COL_PaymentMode=6;
        const int  COL_PaymentModeText=7;
        const int  COL_CheckNumber=8;
        const int  COL_CheckAmount=9;
        const int COL_CheckDate=10;
        const int COL_CloseDate = 4; 
        
        const int COL_PaymentTrayCode=12;

        const int COL_PatientCode=13;
        const int COL_PatientName=14;
        const int COL_PaymentTrayID=15;
        const int COL_Remaining=16;
        const int COL_PayerID = 17;

        private int ROWPAT_COUNT = 1;
        const int COL_COUNT = 18;

        #endregion


        #region "Form Events "

        private void frmEOBExistingPatientPayment_Load(object sender, EventArgs e)
        {
            FillPendingCheck();
        }

        #endregion


        #region Private Methods

        private void ClearFormData()
        {
        }

        private void FillPendingCheck()
        {
            EOBPayment.gloEOBPaymentInsurance ogloEOBPayIns = new global::gloBilling.EOBPayment.gloEOBPaymentInsurance(_databaseconnectionstring);
            EOBPayment.gloEOBPaymentPatient ogloEOBPayPat = new global::gloBilling.EOBPayment.gloEOBPaymentPatient(_databaseconnectionstring);
            DataTable _dtPendingCheck = null;
            
            try
            {
                //_dtPendingCheck = ogloEOBPayIns.SplitGetPendingChecks();
                _dtPendingCheck = ogloEOBPayPat.GetExistingPaymentDetails(_PatientID);
                DataView dvMain = _dtPendingCheck.DefaultView;
                string _Filter = string.Empty;

                if (_dtPendingCheck != null) //&& _dtPendingCheck.Rows.Count > 0)
                {
                    c1PendingCheck.DataSource = dvMain;
                    CellStyle csCurrency = c1PendingCheck.Styles.Add("csCurrencyCell");
                    csCurrency.DataType = typeof(System.Decimal);
                    csCurrency.Format = "c";
                    csCurrency.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    c1PendingCheck.Cols["Check Amount"].Style = csCurrency;
                    c1PendingCheck.Cols["Remaining"].Style = csCurrency;
                    c1PendingCheck.Cols["Remaining"].Caption = "Remaining Amount";

                    c1PendingCheck.Cols["Check Amount"].Width = 130;
                    c1PendingCheck.Cols["Remaining"].Width = 130;
                    c1PendingCheck.Cols["Check #"].Width = 126;

                    c1PendingCheck.Cols["nEOBPaymentID"].Visible = false;
                    c1PendingCheck.Cols["nPayerID"].Visible = false;
                    c1PendingCheck.Cols["nUserID"].Visible = false;
                    c1PendingCheck.Cols["bIsDayClosed"].Visible = false;
                    c1PendingCheck.Cols["dtCreatedDateTime"].Visible = false;

                    c1PendingCheck.VisualStyle = VisualStyle.Office2007Blue;
                    c1PendingCheck.Styles[CellStyleEnum.SelectedColumnHeader].ForeColor = Color.FromArgb(31, 73, 125);
                    c1PendingCheck.Styles[CellStyleEnum.Fixed].ForeColor = Color.FromArgb(31, 73, 125);
                    c1PendingCheck.Styles[CellStyleEnum.Alternate].ForeColor = Color.FromArgb(31, 73, 125);

                    if (c1PendingCheck.Rows.Count > 1)
                    { c1PendingCheck.Select(1, c1PendingCheck.Cols["Check #"].Index, true); }

                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            { }
        }

        #region Design Grid

        private void DesignGrid()
        {

            c1Payment.Clear();
            c1Payment.Rows.Count = 1;
            c1Payment.Rows.Fixed = 1;
            c1Payment.Cols.Count = COL_COUNT;
            c1Payment.Cols.Fixed = 0;
            c1Payment.AllowEditing = false;

            c1Payment.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            c1Payment.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            c1Payment.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns;

            #region " Set Headers "

            c1Payment.SetData(0, COL_EOBPaymentID, "");
            c1Payment.SetData(0, COL_MSTAccountID, "");
            c1Payment.SetData(0, COL_MSTAccountType, "");
            c1Payment.SetData(0, COL_PaymentNo, "Payment #");
            c1Payment.SetData(0, COL_PaymentTrayCode, "");
            c1Payment.SetData(0, COL_PaymentTrayDescription, "Tray");
            c1Payment.SetData(0, COL_PaymentMode, "");
            c1Payment.SetData(0, COL_PaymentModeText, "Pay.Type");
            c1Payment.SetData(0, COL_PayerType, "");
            c1Payment.SetData(0, COL_CheckNumber, "Check #");
            c1Payment.SetData(0, COL_CheckAmount, "Amount");
            c1Payment.SetData(0, COL_CheckDate, "Date");
            c1Payment.SetData(0, COL_CloseDate, "Close Date");
            c1Payment.SetData(0, COL_PaymentTrayCode, "Tray");
            c1Payment.SetData(0, COL_PatientCode, "Patient Code");
            c1Payment.SetData(0, COL_PatientName, "Patient Name");
            c1Payment.SetData(0, COL_Remaining, "Remaining");
            c1Payment.SetData(0, COL_PayerID, "");

            #region " Set Styles "

            c1Payment.Cols[COL_CheckAmount].DataType = typeof(System.Decimal);

            C1.Win.C1FlexGrid.CellStyle csCurrencyStyle = c1Payment.Styles.Add("cs_CurrencyStyle");
            csCurrencyStyle.DataType = typeof(System.Decimal);
            csCurrencyStyle.Format = "c";
            csCurrencyStyle.Font = new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;

            c1Payment.Cols[COL_CheckAmount].Style = csCurrencyStyle;

            #endregion

            #endregion

            #region " Show/Hide "


            c1Payment.Cols[COL_EOBPaymentID].Visible = false;
            c1Payment.Cols[COL_MSTAccountID].Visible = false;
            c1Payment.Cols[COL_MSTAccountType].Visible = false;
            c1Payment.Cols[COL_PaymentNo].Visible = true;
            c1Payment.Cols[COL_PaymentTrayCode].Visible = false;
            c1Payment.Cols[COL_PaymentTrayDescription].Visible = true;
            c1Payment.Cols[COL_PayerType].Visible = false;
            c1Payment.Cols[COL_PaymentMode].Visible = false;
            c1Payment.Cols[COL_PaymentModeText].Visible = true;
            c1Payment.Cols[COL_CheckNumber].Visible = true;
            c1Payment.Cols[COL_CheckAmount].Visible = true;
            c1Payment.Cols[COL_CheckDate].Visible = true;
            c1Payment.Cols[COL_CloseDate].Visible = true;
            c1Payment.Cols[COL_PatientCode].Visible = true;
            c1Payment.Cols[COL_PatientName].Visible = true;
            c1Payment.Cols[COL_PaymentTrayID].Visible = false;
            c1Payment.Cols[COL_Remaining].Visible = true;
            c1Payment.Cols[COL_PayerID].Visible = false;
               
            #endregion

            #region " Alignment "

            c1Payment.Cols[COL_PaymentNo].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1Payment.Cols[COL_CloseDate].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1Payment.Cols[COL_CheckDate].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1Payment.Cols[COL_CheckNumber].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1Payment.Cols[COL_PatientCode].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1Payment.Cols[COL_PatientName].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;


            #endregion

            #region " Width "

            c1Payment.Cols[COL_EOBPaymentID].Width = 0;
            c1Payment.Cols[COL_MSTAccountID].Width = 0;
            c1Payment.Cols[COL_MSTAccountType].Width = 0;
            c1Payment.Cols[COL_PaymentNo].Width = 70;
            c1Payment.Cols[COL_PaymentTrayCode].Width = 0;
            c1Payment.Cols[COL_PaymentTrayDescription].Width = 100;
            c1Payment.Cols[COL_PayerType].Width = 0;
            c1Payment.Cols[COL_PaymentMode].Width = 0;
            c1Payment.Cols[COL_PaymentModeText].Width = 80;
            c1Payment.Cols[COL_MSTAccountType].Width = 0;
            c1Payment.Cols[COL_CheckNumber].Width = 80;
            c1Payment.Cols[COL_CheckAmount].Width = 80;
            c1Payment.Cols[COL_CheckDate].Width = 75;
            c1Payment.Cols[COL_CloseDate].Width = 75;
            c1Payment.Cols[COL_PatientCode].Width = 80;
            c1Payment.Cols[COL_PatientName].Width = 110;
            c1Payment.Cols[COL_PaymentTrayID].Width = 0;
            c1Payment.Cols[COL_Remaining].Width = 0;
            c1Payment.Cols[COL_PayerID].Width = 0;
            


            #endregion

        }

        #endregion

        #region Fill Method

        private void FillFormData()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable _dtFormData = null;
            string _sqlQuery = String.Empty;

            try
            {
                FillPaymentTray();

                _sqlQuery = "SELECT dbo.GET_NAME(Patient.sFirstName, Patient.sMiddleName, Patient.sLastName) as PatientName " +
                " FROM Patient where nPatientID='" + _PatientID + "'";
                oDB.Connect(false);
                oDB.Retrive_Query(_sqlQuery, out _dtFormData);
                oDB.Disconnect();

                txtPatient.Text = "";
                if (_dtFormData != null && _dtFormData.Rows.Count > 0)
                { txtPatient.Text = Convert.ToString(_dtFormData.Rows[0]["PatientName"]); }

            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true); }
            finally 
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_dtFormData != null) { _dtFormData.Dispose(); }
            }
        }

        //Retrieve Data For Default Patient
        private void FillData()
        {
            gloDatabaseLayer.DBLayer ODB = null;
            int rowIndex = 0;
            try
            {
                ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                ODB.Connect(false);
                string _strquery = " SELECT BL_EOBPayment_MST.nEOBPaymentID, BL_EOBPayment_MST.nMSTAccountID,BL_EOBPayment_MST.nPayerID,BL_EOBPayment_MST.nClinicID, BL_EOBPayment_MST.nMSTAccountType, " +
                                   " dbo.CONVERT_TO_DATE(BL_EOBPayment_MST.nCloseDate) as nCloseDate, BL_EOBPayment_MST.nPaymentTrayID, BL_EOBPayment_MST.sPaymentTrayCode, " +
                                   " BL_EOBPayment_MST.sPaymentTrayDescription, BL_EOBPayment_MST.nPayerType, " +
                                   " CASE BL_EOBPayment_MST.nPaymentMode WHEN 5 THEN 'EFT' WHEN 4 THEN 'CreditCard' WHEN 3 THEN 'MoneyOrder' WHEN 2 THEN 'Check' WHEN 1 THEN 'Cash' END AS PaymentModeText, BL_EOBPayment_MST.sCheckNumber, BL_EOBPayment_MST.nCheckAmount,  " +
                                   " dbo.CONVERT_TO_DATE(BL_EOBPayment_MST.nCheckDate) as nCheckDate, Patient.sPatientCode,dbo.GET_NAME(Patient.sFirstName, Patient.sMiddleName, Patient.sLastName) as PatientName FROM BL_EOBPayment_MST LEFT OUTER JOIN Patient ON BL_EOBPayment_MST.nPayerID = Patient.nPatientID" +
                                   " where BL_EOBPayment_MST.nClinicID='" + _ClinicID + "' and BL_EOBPayment_MST.nPayerID= " + _PatientID + "";
                DataTable dt = new DataTable();
                ODB.Retrive_Query(_strquery, out dt);
                ODB.Disconnect();
               
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                       for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {

                            c1Payment.Rows.Add();
                            rowIndex = c1Payment.Rows.Count - 1;

                            c1Payment.SetData(rowIndex, COL_EOBPaymentID, dt.Rows[i]["nEOBPaymentID"]);
                            c1Payment.SetData(rowIndex, COL_MSTAccountID, dt.Rows[i]["nMSTAccountID"]);
                            c1Payment.SetData(rowIndex, COL_MSTAccountType, dt.Rows[i]["nMSTAccountType"]);
                            c1Payment.SetData(rowIndex, COL_PaymentTrayID, dt.Rows[i]["nPaymentTrayID"]);
                            c1Payment.SetData(rowIndex, COL_PaymentTrayCode, dt.Rows[i]["sPaymentTrayCode"]);
                            c1Payment.SetData(rowIndex, COL_PaymentTrayDescription, dt.Rows[i]["sPaymentTrayDescription"]);
                            c1Payment.SetData(rowIndex, COL_PayerType, dt.Rows[i]["nPayerType"]);
                            c1Payment.SetData(rowIndex, COL_PaymentMode, dt.Rows[i]["PaymentModeText"]);
                            c1Payment.SetData(rowIndex, COL_PaymentModeText, dt.Rows[i]["PaymentModeText"]);
                            c1Payment.SetData(rowIndex, COL_CheckNumber, dt.Rows[i]["sCheckNumber"]);
                            c1Payment.SetData(rowIndex, COL_CheckAmount, dt.Rows[i]["nCheckAmount"]);
                            c1Payment.SetData(rowIndex, COL_CheckDate, dt.Rows[i]["nCheckDate"]);
                            c1Payment.SetData(rowIndex, COL_PatientCode, dt.Rows[i]["sPatientCode"]);
                            c1Payment.SetData(rowIndex, COL_PatientName, dt.Rows[i]["PatientName"]);
                            c1Payment.SetData(rowIndex, COL_PayerID, dt.Rows[i]["nPayerID"]);
                            c1Payment.SetData(rowIndex, COL_CloseDate, dt.Rows[i]["nCloseDate"]);
                             
                         
                        }
                        cmbPaymentTray.SelectedValue = Convert.ToInt64(dt.Rows[0]["nPaymentTrayID"]);
                    }
                }
                if (dt != null) { dt.Dispose(); }
            }





            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (ODB != null)
                {
                    ODB.Dispose();
                }
            }
        }


        private void FillData_Search()
        {
            gloDatabaseLayer.DBLayer ODB = null;
            Int64 _CloseDayTrayID = 0;
            string _CloseDayTrayCode = "";
            string _CloseDayTrayName = "";
            int rowIndex = 0;
 
            try
            {
                ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                ODB.Connect(false);
                string _strquery = " SELECT BL_EOBPayment_MST.nEOBPaymentID, BL_EOBPayment_MST.nMSTAccountID,BL_EOBPayment_MST.nPayerID,BL_EOBPayment_MST.nClinicID, BL_EOBPayment_MST.nMSTAccountType, " +
                                    " dbo.CONVERT_TO_DATE(BL_EOBPayment_MST.nCloseDate) as nCloseDate, BL_EOBPayment_MST.nPaymentTrayID, BL_EOBPayment_MST.sPaymentTrayCode, " +
                                    " BL_EOBPayment_MST.sPaymentTrayDescription, BL_EOBPayment_MST.nPayerType, " +
                                    " CASE BL_EOBPayment_MST.nPaymentMode WHEN 5 THEN 'EFT' WHEN 4 THEN 'CreditCard' WHEN 3 THEN 'MoneyOrder' WHEN 2 THEN 'Check' WHEN 1 THEN 'Cash' END AS PaymentModeText, BL_EOBPayment_MST.sCheckNumber, BL_EOBPayment_MST.nCheckAmount,  " +
                                    " dbo.CONVERT_TO_DATE(BL_EOBPayment_MST.nCheckDate) as nCheckDate, Patient.sPatientCode,dbo.GET_NAME(Patient.sFirstName, Patient.sMiddleName, Patient.sLastName) as PatientName FROM BL_EOBPayment_MST LEFT OUTER JOIN Patient ON BL_EOBPayment_MST.nPayerID = Patient.nPatientID" +
                                    " where BL_EOBPayment_MST.nClinicID='" + _ClinicID + "'";

                if (txtPaymentNo.Text != null && txtPaymentNo.Text.Trim().Length > 0)
                {
                    _strquery = _strquery + "";
                }
                if (txtPatient.Text != null && txtPatient.Text.Trim().Length > 0)
                {
                    String _sPatName = txtPatient.Text;
                    _strquery = _strquery + " AND Patient.sFirstName LIKE '" + _sPatName.Replace("'", "''") + "%'  OR Patient.sMiddleName LIKE '" + _sPatName.Replace("'", "''") + "%' OR Patient.sLastName LIKE '" + _sPatName.Replace("'", "''") + "%'";
                }
                if (txtCheckNumber.Text != null && txtCheckNumber.Text.Trim().Length > 0)
                {
                    _strquery = _strquery + " AND  BL_EOBPayment_MST.sCheckNumber like '" + txtCheckNumber.Text.Replace("'", "''") + "%'";
                }

                
                mskCloseDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                if (mskCloseDate.Text != null && mskCloseDate.Text.Trim().Length > 0)
                {
                    _strquery = _strquery + " AND BL_EOBPayment_MST.nCloseDate=" + gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text.ToString().Replace("'", "''")) + " ";
                }
                mskCloseDate.TextMaskFormat = MaskFormat.IncludeLiterals;

                mskChkDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                if (mskChkDate.Text != null && mskChkDate.Text.Trim().Length > 0)
                {
                    _strquery = _strquery + " AND BL_EOBPayment_MST.nCheckDate=" + gloDateMaster.gloDate.DateAsNumber(mskChkDate.Text.ToString().Replace("'", "''")) + "";
                }
                mskChkDate.TextMaskFormat = MaskFormat.IncludeLiterals;

                if (txtuser.Text != null && txtuser.Text.Trim().Length > 0)
                {
                    _strquery = _strquery + " AND  BL_EOBPayment_MST.sUserName LIKE '" + txtuser.Text.Replace("'", "''") + "%' ";
                }

                #region "Payment Tray"
                if (cmbPaymentTray.SelectedIndex >= 0)
                {
                    DataRowView dvr = (DataRowView)cmbPaymentTray.SelectedItem;
                    if (dvr != null)
                    {
                        _CloseDayTrayID = Convert.ToInt64(cmbPaymentTray.SelectedValue.ToString());
                        _CloseDayTrayCode = dvr.Row["sCode"].ToString();
                        _CloseDayTrayName = cmbPaymentTray.Text;

                        _strquery = _strquery + "AND  BL_EOBPayment_MST.nPaymentTrayID=" + _CloseDayTrayID + "";
                    }
                    if (dvr != null) { dvr = null; }
                }
                #endregion

  
                DataTable dt = new DataTable();
                ODB.Retrive_Query(_strquery, out dt);
                ODB.Disconnect();
               
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                           c1Payment.Rows.Add();
                           rowIndex = c1Payment.Rows.Count - 1;

                            c1Payment.SetData(rowIndex, COL_EOBPaymentID, dt.Rows[i]["nEOBPaymentID"]);
                            c1Payment.SetData(rowIndex, COL_MSTAccountID, dt.Rows[i]["nMSTAccountID"]);
                            c1Payment.SetData(rowIndex, COL_MSTAccountType, dt.Rows[i]["nMSTAccountType"]);
                            c1Payment.SetData(rowIndex, COL_PaymentTrayID, dt.Rows[i]["nPaymentTrayID"]);
                            c1Payment.SetData(rowIndex, COL_PaymentTrayCode, dt.Rows[i]["sPaymentTrayCode"]);
                            c1Payment.SetData(rowIndex, COL_PaymentTrayDescription, dt.Rows[i]["sPaymentTrayDescription"]);
                            c1Payment.SetData(rowIndex, COL_PayerType, dt.Rows[i]["nPayerType"]);
                            c1Payment.SetData(rowIndex, COL_PaymentMode, dt.Rows[i]["PaymentModeText"]);
                            c1Payment.SetData(rowIndex, COL_PaymentModeText, dt.Rows[i]["PaymentModeText"]);
                            c1Payment.SetData(rowIndex, COL_CheckNumber, dt.Rows[i]["sCheckNumber"]);
                            c1Payment.SetData(rowIndex, COL_CheckAmount, dt.Rows[i]["nCheckAmount"]);
                            c1Payment.SetData(rowIndex, COL_CheckDate, dt.Rows[i]["nCheckDate"]);
                            c1Payment.SetData(rowIndex, COL_PatientCode, dt.Rows[i]["sPatientCode"]);
                            c1Payment.SetData(rowIndex, COL_PatientName, dt.Rows[i]["PatientName"]);
                            c1Payment.SetData(rowIndex, COL_PayerID, dt.Rows[i]["nPayerID"]);
                            c1Payment.SetData(rowIndex, COL_CloseDate, dt.Rows[i]["nCloseDate"]);

                        }
                        cmbPaymentTray.SelectedValue = Convert.ToInt64(dt.Rows[0]["nPaymentTrayID"]);
                    }
                }
            }

            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (ODB != null)
                {
                    ODB.Dispose();
                }
            }
        }

        private void FillPaymentTray()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloSecurity.gloValidateUser ogloValidateUser = new gloSecurity.gloValidateUser(_databaseconnectionstring);
            string _sqlQuery = "";
            Int64 _defaultTrayId = 0;
            Object _retVal = null;

            try
            {
                if (IsAdmin(_UserId) == true)
                {
                    _sqlQuery = "SELECT nCloseDayTrayID,sCode, " +
                        " sDescription,ISNULL(bIsDefault,0) AS bIsDefault" +
                        " FROM BL_CloseDayTray WHERE nCloseDayTrayID IS NOT NULL AND sDescription IS NOT NULL AND nCloseDayTrayID > 0 " +
                        "AND sDescription <> '' AND ISNULL(bIsClosed,0) = 0 AND nClinicID = " + _ClinicID + "";
                }
                else
                {
                    _sqlQuery = "SELECT nCloseDayTrayID,sCode, " +
                    " sDescription,ISNULL(bIsDefault,0) AS bIsDefault" +
                    " FROM BL_CloseDayTray WHERE nCloseDayTrayID IS NOT NULL AND sDescription IS NOT NULL AND nCloseDayTrayID > 0 " +
                    "AND sDescription <> ''  AND ISNULL(bIsClosed,0) = 0  AND nUserID = " + _UserId + " AND nClinicID = " + _ClinicID + "";
                }

                DataTable dtCloseDayTray = new DataTable();
                oDB.Connect(false);
                oDB.Retrive_Query(_sqlQuery, out dtCloseDayTray);
                oDB.Disconnect();


                cmbPaymentTray.DataSource = dtCloseDayTray;
                cmbPaymentTray.ValueMember = "nCloseDayTrayID";
                cmbPaymentTray.DisplayMember = "sDescription";

                if (dtCloseDayTray != null && dtCloseDayTray.Rows.Count > 0)
                {
                    _sqlQuery = " SELECT ISNULL(nCloseDayTrayID,0) As nCloseDayTrayID FROM BL_CloseDayTray " +
                   " WHERE nCloseDayTrayID IS NOT NULL AND sDescription IS NOT NULL AND nCloseDayTrayID > 0 " +
                   " AND sDescription <> ''  AND ISNULL(bIsClosed,0) = 0 AND bIsDefault = 'true' AND nUserID = " + _UserId + " AND nClinicID = " + _ClinicID + "";
                    oDB.Connect(false);
                    _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                    oDB.Disconnect();

                    if (_retVal != null && Convert.ToString(_retVal).Trim() != "" && Convert.ToInt64(_retVal) > 0)
                    {
                        _defaultTrayId = Convert.ToInt64(_retVal);
                        cmbPaymentTray.SelectedValue = _defaultTrayId;
                    }
                    else
                    {
                        cmbPaymentTray.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_retVal != null) { _retVal = null; }

            }
        }


        private bool IsAdmin(Int64 UserId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable oDataTable = new DataTable();
            bool result = false;
            oDB.Connect(false);
            oDB.Retrive_Query("Select nAdministrator from User_MST where nUserID='" + UserId + "' and nAdministrator = 1", out oDataTable);
            if (oDataTable != null)
            {
                if (oDataTable.Rows.Count > 0)
                {
                    result = true;
                }
            }
            oDataTable.Dispose();
            oDB.Dispose();
            return result;
        }

        #endregion

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DesignGrid();
            FillData_Search();
        }           

        #endregion
    

        #region " ToolStrip Button Click Events "

        private void tls_btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tls_btnSave_Click(object sender, EventArgs e)
        {
            if (c1Payment.Rows.Count > 1 && c1Payment.RowSel > 0)
            {
                if (c1Payment.GetData(c1Payment.RowSel, COL_EOBPaymentID) != null && c1Payment.GetData(c1Payment.RowSel, COL_EOBPaymentID).ToString().Trim().Length > 0)
                {
                    EOBPaymentID = Convert.ToInt64(Convert.ToString(c1Payment.GetData(c1Payment.RowSel, COL_EOBPaymentID)));
                    //EOBPatientID = _PatientID;//  Convert.ToInt64(Convert.ToString(c1Payment.GetData(c1Payment.RowSel, COL_pay)));
                    EOBPatientID = Convert.ToInt64(Convert.ToString(c1Payment.GetData(c1Payment.RowSel, COL_PayerID)));
                    EOBPatientCode = Convert.ToString(c1Payment.GetData(c1Payment.RowSel, COL_PatientCode));
                    this.Close();
                }
            }
        }

        #endregion " ToolStrip Button Click Events "

        private void c1PendingCheck_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            DialogResult _dlgRst = DialogResult.None;
            gloEOBPaymentPatient ogloPaymentPatient = new gloEOBPaymentPatient(_databaseconnectionstring);
            int _voidCloseDate = 0;
            string _voidTrayName = "";
            Int64 _voidTrayId = 0;
            string _voidTrayCode = "";
            Int64 _retVal = 0;
            string _voidNotes = "";
            if (c1PendingCheck.Rows.Count > 1)
            {
                EOBPaymentID = Convert.ToInt64(c1PendingCheck.GetData(c1PendingCheck.RowSel, COL_EOBPaymentID).ToString());
                _nCloseDate = c1PendingCheck.GetData(c1PendingCheck.RowSel, COL_CloseDate).ToString();
                _dlgRst = MessageBox.Show("Do you want to void the payment?", _messageboxcaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                try
                {
                    if (_dlgRst == DialogResult.Yes)
                    {
                        frmVoidPayment ofrmVoid = new frmVoidPayment(EOBPaymentID);
                        ofrmVoid.ShowDialog();
                        if (ofrmVoid.oDialogResult)
                        {
                            _voidCloseDate = ofrmVoid.VoidCloseDate;
                            _voidTrayName = ofrmVoid.VoidTrayName;
                            _voidTrayId = ofrmVoid.VoidTrayID;
                            _voidTrayCode = ofrmVoid.VoidTrayCode;
                            _voidNotes = ofrmVoid.VoidNotes;
                            _retVal = ogloPaymentPatient.VoidPatientPayment(EOBPaymentID, _PatientID, "", _nCloseDate, _voidNotes, _voidCloseDate, _voidTrayId, _voidTrayCode, _voidTrayName);

                        }
                        ofrmVoid.Dispose();
                    }
                }
                catch (Exception Ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
                }
            }
            else
            {
                MessageBox.Show("No existing payment found for void.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }
    }
}