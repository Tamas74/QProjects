using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloBilling
{
    public partial class frmPatientPayRefundVoid : Form
    {
        #region " Private Variables "

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private string _databaseconnectionstring = "";
        private Int64 _ClinicID = 0;
        Int64 _UserId = 0;
       // string _UserName = "";
        private string _MessageBoxCaption = "";
     //   private bool _IsFormLoading = false;
        private Int64 _patientId = 0;


        private DateTime _closeDate = DateTime.Now;
       // private string _closeDayTray = "";
        


        #endregion " Private Variables "

        #region " Constructors "
        public frmPatientPayRefundVoid(string DatabaseConnectionString, Int64 Patientid)
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
        #endregion

        #region "Form Load"

        private void frmPatientPayRefundVoid_Load(object sender, EventArgs e)
        {

            gloC1FlexStyle.Style(c1PatientRefund, false);

            
            //SetCloseDate();
            //FillPaymentTray();
            FillPatientRefund();
        }

        #endregion

        #region "Private & Public Function"

        //private void FillPaymentTray()
        //{
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //    gloSecurity.gloValidateUser ogloValidateUser = new gloSecurity.gloValidateUser(_databaseconnectionstring);
        //    string _sqlQuery = "";
        //    Int64 _defaultTrayId = 0;
        //    Object _retVal = null;

        //    try
        //    {
        //        if (IsAdmin(_UserId) == true)
        //        {
        //            _sqlQuery = "SELECT nCloseDayTrayID,sCode, " +
        //                " sDescription,ISNULL(bIsDefault,0) AS bIsDefault" +
        //                " FROM BL_CloseDayTray WHERE nCloseDayTrayID IS NOT NULL AND sDescription IS NOT NULL AND nCloseDayTrayID > 0 " +
        //                "AND sDescription <> '' AND ISNULL(bIsClosed,0) = 0 AND nClinicID = " + _ClinicID + "";
        //        }
        //        else
        //        {
        //            _sqlQuery = "SELECT nCloseDayTrayID,sCode, " +
        //            " sDescription,ISNULL(bIsDefault,0) AS bIsDefault" +
        //            " FROM BL_CloseDayTray WHERE nCloseDayTrayID IS NOT NULL AND sDescription IS NOT NULL AND nCloseDayTrayID > 0 " +
        //            "AND sDescription <> ''  AND ISNULL(bIsClosed,0) = 0  AND nUserID = " + _UserId + " AND nClinicID = " + _ClinicID + "";
        //        }

        //        DataTable dtCloseDayTray = new DataTable();
        //        oDB.Connect(false);
        //        oDB.Retrive_Query(_sqlQuery, out dtCloseDayTray);
        //        oDB.Disconnect();


        //        cmbPaymentTray.DataSource = dtCloseDayTray;
        //        cmbPaymentTray.ValueMember = "nCloseDayTrayID";
        //        cmbPaymentTray.DisplayMember = "sDescription";

        //        if (dtCloseDayTray != null && dtCloseDayTray.Rows.Count > 0)
        //        {
        //            _sqlQuery = " SELECT ISNULL(nCloseDayTrayID,0) As nCloseDayTrayID FROM BL_CloseDayTray " +
        //           " WHERE nCloseDayTrayID IS NOT NULL AND sDescription IS NOT NULL AND nCloseDayTrayID > 0 " +
        //           " AND sDescription <> ''  AND ISNULL(bIsClosed,0) = 0 AND bIsDefault = 'true' AND nUserID = " + _UserId + " AND nClinicID = " + _ClinicID + "";
        //            oDB.Connect(false);
        //            _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
        //            oDB.Disconnect();

        //            if (_retVal != null && Convert.ToString(_retVal).Trim() != "" && Convert.ToInt64(_retVal) > 0)
        //            {
        //                _defaultTrayId = Convert.ToInt64(_retVal);
        //                cmbPaymentTray.SelectedValue = _defaultTrayId;
        //            }
        //            else
        //            {
        //                cmbPaymentTray.SelectedIndex = 0;
        //            }
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //    }
        //    finally
        //    {
        //        if (oDB != null) { oDB.Dispose(); }
        //        if (_retVal != null) { _retVal = null; }

        //    }
        //}
 //private void SetCloseDate()
        //{
        //    gloBilling ogloBilling = new gloBilling(_databaseconnectionstring, "");
        //    try
        //    {
        //        MaskedTextBox mskDate = (MaskedTextBox)mskCloseDate;
        //        mskDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
        //        string strDate = mskDate.Text;
        //        mskDate.TextMaskFormat = MaskFormat.IncludeLiterals;
        //        if (mskDate != null)
        //        {
        //            if (strDate.Length <= 0)
        //            {
        //                #region " Set last selected close date "

        //                //....Load last selected close date
        //                //...If the last selected close date is being closed then make the close date empty.

        //                gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
        //                Object _retValue = null;
        //                string _clsDate = "";
        //                oSettings.GetSetting("PAYMENT_LASTCLOSEDATE", _UserId, _ClinicID, out _retValue);
        //                oSettings.Dispose();

        //                if (_retValue != null && Convert.ToString(_retValue).Trim() != "")
        //                {
        //                    try
        //                    { _clsDate = Convert.ToDateTime(Convert.ToString(_retValue).Trim()).ToString("MM/dd/yyyy"); }
        //                    catch (Exception ex)
        //                    {
        //                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
        //                        ex = null;
        //                        _clsDate = DateTime.Now.Date.ToString("MM/dd/yyyy");
        //                    }
        //                }
        //                else
        //                { _clsDate = DateTime.Now.Date.ToString("MM/dd/yyyy"); }

        //                if (_clsDate.Trim() != "")
        //                {
        //                    //gloBilling ogloBilling = new gloBilling(_databaseconnectionstring, "");
        //                    if (ogloBilling.IsDayClosed(Convert.ToDateTime(_clsDate.Trim())) == true)
        //                    { _clsDate = ""; }
        //                    ogloBilling.Dispose();
        //                }

        //                mskCloseDate.Text = _clsDate;

        //                #endregion " Set last selected close date "
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("ERROR : " + ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //    finally
        //    {
        //        if (ogloBilling != null) { ogloBilling.Dispose(); }
        //    }
        //}

        //Bind the Patient Refund
        private void FillPatientRefund()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _sqlQuery = "";                   
            try
            {
                _sqlQuery = "select ISNULL(nRefundID,0) as nRefundID,dbo.CONVERT_TO_DATE(nCloseDate) as nCloseDate,"
                           + "sPaymentTrayDescription,sRefundTo,dbo.CONVERT_TO_DATE(nRefundDate) AS nRefundDate,nRefundAmount,sRefundNotes,"
                           +"sUserName,dtCreatedDateTime,case bIsVoid WHEN '1' THEN 'Voided' ELSE '' END AS Status "
                           + "from BL_EOBPatient_Refund where nPayerID=" + _patientId + " order by nCloseDate,dtCreatedDateTime desc";
                DataTable dtPatientRefund = new DataTable();
                oDB.Connect(false);
                oDB.Retrive_Query(_sqlQuery, out dtPatientRefund);
                oDB.Disconnect();                             
                c1PatientRefund.DataSource = dtPatientRefund.DefaultView;
                DesignRefundgrid();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                if (oDB != null)
                    oDB.Dispose();

            }
            finally
            {
                if (oDB != null)
                    oDB.Dispose();              
            }
        }

        //Bind the Patient Refund
        private void DesignRefundgrid()
        {
            try
            {

                #region " Set Header "
                c1PatientRefund.Cols["nRefundID"].Caption = "RefundID";
                c1PatientRefund.Cols["sRefundTo"].Caption = "To";
                c1PatientRefund.Cols["nCloseDate"].Caption = "Close Date";
                c1PatientRefund.Cols["sPaymentTrayDescription"].Caption = "Tray";
                c1PatientRefund.Cols["nRefundDate"].Caption = "Date";
                c1PatientRefund.Cols["nRefundAmount"].Caption = "Amount";
                c1PatientRefund.Cols["sRefundNotes"].Caption = "Note";
                c1PatientRefund.Cols["sUserName"].Caption = "User";
                c1PatientRefund.Cols["dtCreatedDateTime"].Caption = "Date/Time";
                c1PatientRefund.Cols["Status"].Caption = "Status";

                #endregion

                int _nWidth = 0;
                _nWidth = 826;//Convert.ToInt32( c1QueuedClaims.Width);
                c1PatientRefund.Cols["nRefundID"].Width = 0;
                c1PatientRefund.Cols["nRefundID"].Visible = false;
                c1PatientRefund.Cols["nCloseDate"].Width = Convert.ToInt32(_nWidth * 0.10);
                c1PatientRefund.Cols["sPaymentTrayDescription"].Width = Convert.ToInt32(_nWidth * 0.10);
                c1PatientRefund.Cols["sRefundTo"].Width = Convert.ToInt32(_nWidth * 0.14);
                c1PatientRefund.Cols["nRefundDate"].Width = Convert.ToInt32(_nWidth * 0.10);
                c1PatientRefund.Cols["nRefundAmount"].Width = Convert.ToInt32(_nWidth * 0.10);
                c1PatientRefund.Cols["sRefundNotes"].Width = Convert.ToInt32(_nWidth * 0.23);
                c1PatientRefund.Cols["sUserName"].Width = Convert.ToInt32(_nWidth * 0.11);
                c1PatientRefund.Cols["dtCreatedDateTime"].Width = Convert.ToInt32(_nWidth * 0.17);
                c1PatientRefund.Cols["dtCreatedDateTime"].Format = "g";
                c1PatientRefund.Cols["Status"].Width = Convert.ToInt32(_nWidth * 0.07);
                c1PatientRefund.Cols["nRefundAmount"].Format = "c";


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);

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

        #region "Form Event"
      

        private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        #endregion

        private void tsb_OK_Click(object sender, EventArgs e)
        {
             
            try
            {
                if(c1PatientRefund.RowSel>0)
                {
                    if (DialogResult.Yes == MessageBox.Show("Do you want to void refund ? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                    {
                        string _strdate = Convert.ToString(c1PatientRefund.GetData(c1PatientRefund.RowSel, "nCloseDate"));
                        int _date = 0;
                        Int64 _refundID = 0;
                        _refundID = Convert.ToInt64(c1PatientRefund.GetData(c1PatientRefund.RowSel, "nRefundID"));
                        if (_strdate != "")
                        {
                            _date = gloDateMaster.gloDate.DateAsNumber(_strdate);
                        }
                        frmVoidPatientRefund ofrm = new frmVoidPatientRefund(_date,_refundID,_patientId);
                        ofrm.ShowDialog(this);
                        ofrm.Dispose();
                        ofrm = null;
                        FillPatientRefund();
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

        private void c1PatientRefund_RowColChange(object sender, EventArgs e)
        {
            try
            {
                if (c1PatientRefund.RowSel > 0 && c1PatientRefund.Rows.Count > 1 && c1PatientRefund.Cols["Status"] != null)
                {
                    if (Convert.ToString(c1PatientRefund.GetData(c1PatientRefund.RowSel, "Status")) == "Voided")
                    {
                        tsb_OK.Enabled = false;
                    }
                    else
                    {
                        tsb_OK.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void c1PatientRefund_RowColChange(object sender, C1.Win.C1FlexGrid.SortColEventArgs e)
        {
            try
            {
                if (c1PatientRefund.RowSel > 0 && c1PatientRefund.Rows.Count > 1 && c1PatientRefund.Cols["Status"] != null)
                {
                    if (Convert.ToString(c1PatientRefund.GetData(c1PatientRefund.RowSel, "Status")) == "Voided")
                    {
                        tsb_OK.Enabled = false;
                    }
                    else
                    {
                        tsb_OK.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void c1PatientRefund_MouseMove(object sender, MouseEventArgs e)
        {

            gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);

        }

      

      

     

        

    }
}
