using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace gloReports.C1Reports
{
    public partial class frmRpt_DailyChargesPaySummary : Form
    {

        #region " Variable Declarations "


        gloListControl.gloListControl oListControl = null;
        gloListControl.gloListControlType _CurrentControlType = gloListControl.gloListControlType.Other;
 //       private gloGeneralItem.gloItems ogloItems = null;
        private string _databaseconnectionstring = "";
        private string _MessageBoxCaption = string.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;
        private Int64 _nUserID = 0;

        private StringBuilder sbChrgProvidersName = new StringBuilder();
        private StringBuilder sbChrgFacilityName = new StringBuilder();
        private StringBuilder sbChrgUserName = new StringBuilder();
        private StringBuilder sbChrgTrayName = new StringBuilder();
        private StringBuilder sbChrgProviders = new StringBuilder();
        private StringBuilder sbChrgFacility = new StringBuilder();
        private StringBuilder sbChrgUser = new StringBuilder();
        private StringBuilder sbChrgTray = new StringBuilder();
        private Int64 _UserId = 0;

        private StringBuilder sbPayProviders = new StringBuilder();
        private StringBuilder sbPayFacility = new StringBuilder();
        private StringBuilder sbPayUser = new StringBuilder();
        private StringBuilder sbPayTray = new StringBuilder();
        private StringBuilder sbPayType = new StringBuilder();
        private StringBuilder sbPayProvidersName = new StringBuilder();
        private StringBuilder sbPayFacilityName = new StringBuilder();
        private StringBuilder sbPayUserName = new StringBuilder();
        private StringBuilder sbPayTrayName = new StringBuilder();
        private StringBuilder sbPayTypeName = new StringBuilder();

        private string _UserName = "";
        private String _ControlType = null;

        Rpt_EOBDailyPay objrptEOBDailyPay = new Rpt_EOBDailyPay();
        Rpt_ChargesSummary objrptChargesSummary = new Rpt_ChargesSummary();
        Rpt_DailyClose objrptDailyClose = new Rpt_DailyClose();

        private Boolean _isFormLoading=false;

        #endregion


        #region  " Property Procedures "

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        public Int64 UserID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }


        #endregion  " Property Procedures "


        #region " Constructors "
        
        public frmRpt_DailyChargesPaySummary(string databaseconnectionstring)
        {
            InitializeComponent();

            _databaseconnectionstring = databaseconnectionstring;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }


            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                {
                    _nUserID = Convert.ToInt64(appSettings["UserID"]);
                }
                else { _nUserID = 0; }
            }

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
        
        #endregion

       
        #region " Form Events "

        private void frmRpt_DailyChargesPaySummary_Load(object sender, EventArgs e)
        {
            _isFormLoading = true;

            SelectCloseDates();
            FillDailyCloseDates();

            FillChargesSortByCombo();
            FillPaymentSortByCombo();


            FillDailyCharges();
            FillDailyPayment();
            FillDayClose();

            
            btnChrgUP.BackgroundImage = global::gloReports.Properties.Resources.UP;
            btnChrgUP.BackgroundImageLayout = ImageLayout.Center;
            btnChrgDown.Visible = false;

            btnPayUP.BackgroundImage = global::gloReports.Properties.Resources.UP;
            btnPayUP.BackgroundImageLayout = ImageLayout.Center;
            btnPayDown.Visible = false;

            btnCloseUP.BackgroundImage = global::gloReports.Properties.Resources.UP;
            btnCloseUP.BackgroundImageLayout = ImageLayout.Center;
            btnCloseDown.Visible = false;


            btnTrayDown.BackgroundImage = global::gloReports.Properties.Resources.Down;
            btnTrayDown.BackgroundImageLayout = ImageLayout.Center;
            btnTrayUp.Visible = false;

            //Fill_FilterDatesCombo(cmb_Chargesdatefilter);
            //Fill_FilterDatesCombo(cmb_Paydatefilter);

            //SelectCloseDates();

            lblUserNm.Text = _UserName.ToString();


            crvRptViewCharges.DisplayToolbar = true;
            crvRptViewPayment.DisplayToolbar = true;
            crvRptViewCloseDay.DisplayToolbar = true;

           

            tsb_btnDailyClose.Visible = false;
        }

        #endregion

        
        #region " Tool Strip Events "
        
        private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsb_btnExportReport_Click(object sender, EventArgs e)
        {
            if (tabSummary.SelectedTab.Tag.ToString() == "DailyCharges")
            {
                crvRptViewCharges.ExportReport();
            }
            else if (tabSummary.SelectedTab.Tag.ToString() == "DailyPayment")
            {
                crvRptViewPayment.ExportReport();
            }
            else if (tabSummary.SelectedTab.Tag.ToString() == "DailyClose")
            {
                crvRptViewCloseDay.ExportReport();
            }
        }

        private void tsb_GenerateReport_Click(object sender, EventArgs e)
        {
            
            if (tabSummary.SelectedTab.Tag.ToString() == "DailyCharges")
            {
                //------Abhisekh 12 june 2010 start
                //if (rbchrgPreForClose.Checked == false)
                //{
                //    if (dtpChrgStartDate.Value.Date > dtpChrgEndDate.Value.Date)
                //    {
                //        MessageBox.Show("Start date can not be greater than end date.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        dtpChrgStartDate.Select();
                //        dtpChrgStartDate.Focus();
                //        return;
                //    }
                //}
                //------Abhisekh 12 june 2010 end
                FillDailyCharges();
            }
            else if (tabSummary.SelectedTab.Tag.ToString() == "DailyPayment")
            {
                //------Abhisekh 12 june 2010 start
                //if (rbPayPreForClose.Checked == false)
                //{
                //    if (dtpPayStartDate.Value.Date > dtpPayEndDate.Value.Date)
                //    {
                //        MessageBox.Show("Start date can not be greater than end date.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        dtpPayStartDate.Focus();
                //        dtpPayStartDate.Select();
                //        return;
                //    }
                //}
                //------Abhisekh 12 june 2010 end
                FillDailyPayment();
            }
            else if (tabSummary.SelectedTab.Tag.ToString() == "DailyClose")
            {
                FillDayClose();
               
            }
        } 

        #endregion


        #region "Tab Events "

        private void tabSummary_SelectedIndexChanged(object sender, EventArgs e)
        {
            _isFormLoading = false;

            this.Text = tabSummary.SelectedTab.Text;
            crvRptViewCharges.DisplayToolbar = true;
            crvRptViewPayment.DisplayToolbar = true;
            crvRptViewCloseDay.DisplayToolbar = true;

            if (tabSummary.SelectedTab.Tag.ToString() == "DailyCharges")
            {
                btnCloseCharges.Text = "Update Charges";
                tsb_btnDailyClose.Visible = false;
                //SelectCloseDates();
            }
            else if (tabSummary.SelectedTab.Tag.ToString() == "DailyPayment")
            {
                btnCloseCharges.Text = "Close Daily Payment";
                tsb_btnDailyClose.Visible = false;
                //SelectCloseDates();
                //FillPaymentSortByCombo();
               
            }
            else if (tabSummary.SelectedTab.Tag.ToString() == "DailyClose")
            {
                btnCloseCharges.Text = "Close Charges";
                tsb_btnDailyClose.Visible = true;
            }

        }
 
        #endregion

        
        #region "User Control Events "

        private void chkLoginUsers_CheckedChanged(object sender, EventArgs e)
        {
            FillChargesTray(cmbMultiChargesTray);
        }

        private void chkPageBreak_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void tsb_btnDailyClose_Click(object sender, EventArgs e)
        {
            try
            {
            Cursor.Current = Cursors.WaitCursor;
            
        
            DialogResult _dltRst = DialogResult.None;
            Int64 _TransID = 0;
            Int64 _LastTransID = 0;
       //     Boolean isValidDate = true;
            Int64 _CloseDate = 0;
          
            object _intresult = null;
            Int64 _result = 0;
            string _sCloseDate = "";
          //  Boolean isTwoDates = false;
            Int64 _nToDate = 0;
            string _sToDate = "";

            if (tabSummary.SelectedTab.Tag.ToString() == "DailyClose")
            {
                if (trvMonths.Nodes != null)
                {
                    
                    bool _ItemFound = false; int _StIndx = -1; int _EdIndx = -1;

                    #region "Validation"
                    //Check the day is selected
                    for (int i = 0; i < trvMonths.Nodes.Count; i++)
                    {
                        if (trvMonths.Nodes[i].Checked == true)
                        {
                            _ItemFound = true;
                            break;
                        }
                    }

                    if (_ItemFound == false)
                    {
                        MessageBox.Show("Please select a Date to Close", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        return;
                    }

                    for (int i = 0; i < trvMonths.Nodes.Count; i++)
                    {
                        if (trvMonths.Nodes[i].Checked == true)
                        {
                            _StIndx = i; break;
                        }
                    }

                    for (int i = trvMonths.Nodes.Count - 1; i >= 0 ; i--)
                    {
                        if (trvMonths.Nodes[i].Checked == true)
                        {
                            _EdIndx = i; break;
                        }
                    }

                    //Check Previous day is closed or not
                    if (trvMonths.Nodes.Count > 1)
                    {
                        for (int i = 1; i < trvMonths.Nodes.Count; i++)
                        {
                            if (trvMonths.Nodes[i].Checked == true)
                            {
                                if (trvMonths.Nodes[0].Checked == false)
                                {
                                    MessageBox.Show("The day cannot be closed until the previous days are closed. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                                    for (int m = 1; m < trvMonths.Nodes.Count; m++)
                                    {
                                        trvMonths.Nodes[m].Checked = false;
                                    }
                                    trvMonths.Nodes[0].Checked = true;
                                    return;
                                }
                            }
                        }
                    }

                    //Check Previous day is closed or not
                    _ItemFound = false;
                    if (trvMonths.Nodes.Count > 0)
                    {
                        for (int i = _StIndx; i <= _EdIndx; i++)
                        {
                            if (trvMonths.Nodes[i].Checked == false)
                            {
                                _ItemFound = true; break;
                            }
                            if (_CloseDate <= 0)
                            {
                                _CloseDate = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(trvMonths.Nodes[i].Text));
                                _sCloseDate = Convert.ToString(trvMonths.Nodes[i].Text);

                                if (_StIndx == _EdIndx)
                                {
                                    _nToDate = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(trvMonths.Nodes[i].Text));
                                    _sToDate = Convert.ToString(trvMonths.Nodes[i].Text);
                                }
                            }
                            else
                            {
                                _nToDate = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(trvMonths.Nodes[i].Text));
                                _sToDate = Convert.ToString(trvMonths.Nodes[i].Text);
                            }
                        }
                    }
                    if (_ItemFound == true)
                    {
                        MessageBox.Show("The day cannot be closed until the previous days are closed. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        for (int m = 1; m < trvMonths.Nodes.Count; m++)
                        {
                            trvMonths.Nodes[m].Checked = false;
                        }
                        trvMonths.Nodes[0].Checked = true;
                        return;
                    }

                    #endregion "Validation"
                }
            }

            if (_CloseDate > 0)
            {
                if (_nToDate > 0 && _CloseDate != _nToDate )
                {
                    _dltRst = MessageBox.Show("Close " + _sCloseDate + "  To  " + _sToDate + "  ?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                }
                else
                {
                    _dltRst = MessageBox.Show("Close " + _sCloseDate + "  ?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                }
                if (_dltRst.ToString() == "Yes")
                {

                    if (tabSummary.SelectedTab.Tag.ToString() == "DailyCharges")
                    {
                        #region " Daily Charges Close "
                        gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                        gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

                        DataTable dtDailyCharges = null;

                        oParameters.Clear();

                        #region " Parameters "

                        if (sbChrgProviders.ToString() != "" && sbChrgProviders.Length > 0)
                        {
                            oParameters.Add("@sProviderID", sbChrgProviders.ToString(), ParameterDirection.Input, SqlDbType.VarChar);
                        }

                        if (sbChrgFacility.ToString() != "" && sbChrgFacility.Length > 0)
                        {
                            oParameters.Add("@sFacilityCode", sbChrgFacility.ToString(), ParameterDirection.Input, SqlDbType.VarChar);
                        }

                        //---------Abhisekh 12 june start 
                        //if (rbchrgPreForClose.Checked)
                        //{
                            if (dtpCloseDate.Text != "" && dtpCloseDate.Text.Length > 0)
                            {
                                oParameters.Add("@nCloseDate", gloDateMaster.gloDate.DateAsNumber(dtpCloseDate.Text.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                            }
                        //}
                        //else
                        //{
                        //    if (dtpChrgStartDate.Text != null && dtpChrgStartDate.Text.Length > 0)
                        //    {
                        //        oParameters.Add("@nStartDate", gloDateMaster.gloDate.DateAsNumber(dtpChrgStartDate.Text.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                        //    }

                        //    if (dtpChrgEndDate.Text != null && dtpChrgEndDate.Text.Length > 0)
                        //    {
                        //        oParameters.Add("@nEndDate", gloDateMaster.gloDate.DateAsNumber(dtpChrgEndDate.Text.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                        //    }
                        //}

                            //----Abhisekh 12 june end 

                        if (sbChrgUser.ToString() != "" && sbChrgUser.Length > 0)
                        {
                            oParameters.Add("@sUserID", sbChrgUser.ToString(), ParameterDirection.Input, SqlDbType.VarChar);
                        }

                        if (sbChrgTray.ToString() != "" && sbChrgTray.Length > 0)
                        {
                            oParameters.Add("@sChargeTryCode", sbChrgTray.ToString(), ParameterDirection.Input, SqlDbType.VarChar);
                        }

                        #endregion

                        oDB.Connect(false);
                        oDB.Retrive("Rpt_ChargesTray", oParameters, out dtDailyCharges);
                        oDB.Disconnect();
                        oParameters.Clear();
                        oParameters.Dispose();
                        if (dtDailyCharges != null && dtDailyCharges.Rows.Count > 0)
                        {
                            for (int i = 0; i <= dtDailyCharges.Rows.Count - 1; i++)
                            {
                                _TransID = Convert.ToInt64(dtDailyCharges.Rows[i]["nTransactionID"]);
                                if (_TransID != _LastTransID)
                                {
                                    oDB.Connect(false);
                                    oDB.Execute_Query("Update BL_Transaction_MST SET bIsDayUpdated=1 WHERE nTransactionID= " + _TransID + " ");
                                }
                                _LastTransID = Convert.ToInt64(dtDailyCharges.Rows[i]["nTransactionID"]);

                            }

                            MessageBox.Show("Updated", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        if (dtDailyCharges != null)
                        {
                            dtDailyCharges.Dispose();
                            dtDailyCharges = null;
                        }
                        oDB.Dispose();
                        oDB = null;
                        #endregion
                    }
                    else if (tabSummary.SelectedTab.Tag.ToString() == "DailyPayment")
                    {
                        #region " Daily Payment Close "
                        gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                        gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                        DataTable dtDailyPayment = null;

                        oParameters.Clear();

                        #region " Parameters "

                        if (sbPayProviders.ToString() != "" && sbPayProviders.Length > 0)
                        {
                            oParameters.Add("@sProviderID", sbPayProviders.ToString(), ParameterDirection.Input, SqlDbType.VarChar);
                        }

                        if (sbPayFacility.ToString() != "" && sbPayFacility.Length > 0)
                        {
                            oParameters.Add("@sFacilityCode", sbPayFacility.ToString(), ParameterDirection.Input, SqlDbType.VarChar);
                        }
                        //---------Abhisekh 12 june start 
                        //if (rbPayPreForClose.Checked)
                        //{
                        if (dtPayCloseDate.Value.ToString() != "" && dtPayCloseDate.Value.ToString().Length > 0)
                            {
                                oParameters.Add("@nCloseDate", gloDateMaster.gloDate.DateAsNumber(dtPayCloseDate.Text.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                            }
                        //}
                        //else
                        //{
                        //    if (dtpPayStartDate.Text != null && dtpPayStartDate.Text.Length > 0)
                        //    {
                        //        oParameters.Add("@nStartDate", gloDateMaster.gloDate.DateAsNumber(dtpPayStartDate.Text.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                        //    }

                        //    if (dtpPayEndDate.Text != null && dtpPayEndDate.Text.Length > 0)
                        //    {
                        //        oParameters.Add("@nEndDate", gloDateMaster.gloDate.DateAsNumber(dtpPayEndDate.Text.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                        //    }
                        //}
                            //---------Abhisekh 12 june end 

                        if (sbPayUser.ToString() != "" && sbPayUser.Length > 0)
                        {
                            oParameters.Add("@sUserID", sbPayUser.ToString(), ParameterDirection.Input, SqlDbType.VarChar);
                        }


                        if (sbPayType.ToString() != "" && sbPayType.Length > 0)
                        {
                            oParameters.Add("@PaymentType", sbPayTray.ToString(), ParameterDirection.Input, SqlDbType.VarChar);
                        }

                        if (sbPayTray.ToString() != "" && sbPayTray.Length > 0)
                        {
                            oParameters.Add("@sChargeTryCode", sbPayTray.ToString(), ParameterDirection.Input, SqlDbType.VarChar);
                        }

                        #endregion

                        oDB.Connect(false);
                        oDB.Retrive("BL_SELECT_EOBDailyPayment", oParameters, out dtDailyPayment);
                        oDB.Disconnect();
                        oParameters.Clear();
                        oParameters.Dispose();

                        if (dtDailyPayment != null && dtDailyPayment.Rows.Count > 0)
                        {
                            //for (int i = 0; i <= dtDailyCharges.Rows.Count - 1; i++)
                            //{
                            //    _TransID = Convert.ToInt64(dtDailyCharges.Rows[i]["nTransactionID"]);
                            //    if (_TransID != _LastTransID)
                            //    {
                            //        //oDB.Connect(false);
                            //        //oDB.Execute_Query("Update BL_Transaction_MST SET bIsDayUpdated=1 WHERE nTransactionID= " + _TransID + " ");
                            //    }
                            //    _LastTransID = Convert.ToInt64(dtDailyCharges.Rows[i]["nTransactionID"]);

                            //}

                            //MessageBox.Show("Updated", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        if (dtDailyPayment != null)
                        {
                            dtDailyPayment.Dispose();
                            dtDailyPayment = null;
                        }
                        oDB.Dispose();
                        oDB = null;
                        #endregion
                    }
                    else if (tabSummary.SelectedTab.Tag.ToString() == "DailyClose")
                    {

                        #region " Daily Close "
                        gloDatabaseLayer.DBLayer ODB2 = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                        ODB2.Connect(false);
                        gloDatabaseLayer.DBParameters oDBParameters = null;
                        

                        Int64 _DaysCount = 0;

                        DateTime dtFromDate = gloDateMaster.gloDate.DateAsDate(_CloseDate);
                        DateTime dtToDate = gloDateMaster.gloDate.DateAsDate(_nToDate);
                        string _sFromDate = "";
                        Int64 _nFromDate = 0;
                        TimeSpan span = dtToDate.Subtract(dtFromDate);
                        _DaysCount =Convert.ToInt64(span.Days);
                        for (Int64 j = 0; j <= _DaysCount; j++)
                        {
                            //DataTable _dtPendingChecks = null;

                            oDBParameters = new gloDatabaseLayer.DBParameters();

                            if (j == 0)
                            {
                                _sFromDate = dtFromDate.ToShortDateString();
                                _CloseDate = gloDateMaster.gloDate.DateAsNumber(_sFromDate);
                                _nFromDate = gloDateMaster.gloDate.DateAsNumber(dtFromDate.AddDays(1).ToShortDateString());
                            }
                            else
                            {
                                //_sFromDate = dtFromDate.AddDays(1).ToShortDateString();
                                //_CloseDate = gloDateMaster.gloDate.DateAsNumber(_nFromDate.ToString());
                                dtFromDate = gloDateMaster.gloDate.DateAsDate(_nFromDate);
                                _sFromDate = dtFromDate.ToShortDateString();
                                _CloseDate = _nFromDate;
                                _nFromDate = gloDateMaster.gloDate.DateAsNumber(dtFromDate.AddDays(1).ToShortDateString());
                            }

                            ////Check for Pending Checks before closing the day  
                            //oDBParameters.Add("@nCloseDate", _CloseDate, ParameterDirection.Input, SqlDbType.VarChar);
                            //ODB2.Retrive("BL_SELECT_PENDINGCHK_FORCLOSEDT", oDBParameters, out  _dtPendingChecks);
                            //oDBParameters.Clear();

                            //if (_dtPendingChecks != null)
                            //{
                            //    if (_dtPendingChecks.Rows.Count > 0)
                            //    {
                            //        //MessageBox.Show("The day " + _sFromDate + " cannot be closed because you have pending checks.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            //        MessageBox.Show("There are insurance payments for Close Date " + _sFromDate + " that are unfinished.To close the day, run Insurance Payment Entry and selecting the Pending Payments button.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                            //        FillDailyCloseDates();
                            //        if (trvMonths.Nodes.Count > 0)
                            //        {
                            //            trvMonths.Nodes[0].Checked = true;
                            //        }
                            //        return;

                            //    }
                            //    else
                            //    {
                            DataTable _DtCloseClaims = null;
                            ODB2.Execute_Query("Update BL_Transaction_MST set nIsTrayClose=1, dtDayClosedOn='" + System.DateTime.Now + "' ,nDayClosedUserID=" + _UserId + ",sDayCloseUserName= '" + _UserName + "' WHERE nTransactionDate=" + _CloseDate + " ");

                            ODB2.Execute_Query("Update BL_EOBPayment_MST set bIsDayClosed = 1, dtDayClosedOn='" + System.DateTime.Now + "' ,nDayCloseUserID=" + _UserId + ",sDayCloseUserName ='" + _UserName + "' WHERE nCloseDate=" + _CloseDate + " ");

                            ODB2.Retrive_Query("Select nEObPaymentID From BL_EOBPayment_MST WHERE  nCloseDate=" + _CloseDate + " ", out _DtCloseClaims);

                            if (_DtCloseClaims != null && _DtCloseClaims.Rows.Count > 0)
                            {

                                for (int i = 0; i < _DtCloseClaims.Rows.Count; i++)
                                {
                                    ODB2.Execute_Query("Update BL_EOBPayment_DTL set bIsDayClosed=1, dtDayClosedOn='" + System.DateTime.Now + "' ,nDayCloseUserID=" + _UserId + ",sDayCloseUserName ='" + _UserName + "' WHERE nEObPaymentID=" + _DtCloseClaims.Rows[i]["nEObPaymentID"] + " ");
                                    //ODB2.Execute_Query("Update BL_EOBPayment_EOB set bIsDayClosed=1, dtDayClosedOn='" + System.DateTime.Now + "' ,nDayCloseUserID=" + _UserId + ",sDayCloseUserName ='" + _UserName + "' WHERE nEObPaymentID=" + _CloseDate + " ");
                                    ODB2.Execute_Query("Update BL_EOBPayment_EOB set bIsDayClosed=1, dtDayClosedOn='" + System.DateTime.Now + "' ,nDayCloseUserID=" + _UserId + ",sDayCloseUserName ='" + _UserName + "' WHERE nEObPaymentID=" + _DtCloseClaims.Rows[i]["nEObPaymentID"] + " ");
                                }
                            }
                            if (_DtCloseClaims != null)
                            {
                                _DtCloseClaims.Dispose();
                                _DtCloseClaims = null;
                            }
                            System.Threading.Thread.Sleep(1000);

                            oDBParameters.Add("@nCloseDayID", 0, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                            oDBParameters.Add("@nCloseDayDate", _CloseDate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                            oDBParameters.Add("@nUserID", _UserId, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                            oDBParameters.Add("@sUserName", _UserName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                            oDBParameters.Add("@dtCloseDateTime", System.DateTime.Now, System.Data.ParameterDirection.Input, System.Data.SqlDbType.DateTime);
                            oDBParameters.Add("@nClinicID", _ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                            oDBParameters.Add("@MachineID", ODB2.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);

                            ODB2.Execute("BL_INUP_DailyClose", oDBParameters, out _intresult);

                            if (_intresult != null)
                            {
                                if (_intresult.ToString().Trim() != "")
                                {
                                    if (Convert.ToInt64(_intresult) > 0)
                                    {
                                        _result = Convert.ToInt64(_intresult.ToString());
                                    }
                                }
                            }
                            oDBParameters.Clear();
                            oDBParameters.Dispose();
                            oDBParameters = null;

                            //    }//
                            //}//
                        }

                        ODB2.Disconnect();
                        ODB2.Dispose();
                        ODB2 = null;
                        FillDailyCloseDates();
                        _isFormLoading = true;
                        FillDayClose();

                        #endregion
                    }
                }
            }
            }
            catch(Exception ex){
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(),true);
            }
            finally{
                Cursor.Current = Cursors.Default;
            }
        }

        
        #endregion


        #region " ------------------------- Charges ---------------------------------------- "

        #region " Fill Methods "

        private void FillDailyCharges()
        {

            //objrptChargesSummary = new Rpt_ChargesSummary();
            dsC1Reports dsReports = new dsC1Reports();
            SqlCommand _sqlcommand = null;
            SqlConnection oConnection = new SqlConnection();
            SqlDataAdapter da = null;

            try
            {

                oConnection.ConnectionString = _databaseconnectionstring;

                #region "Charges Providers "

                sbChrgProviders.Remove(0, sbChrgProviders.Length);
                sbChrgProvidersName.Remove(0, sbChrgProvidersName.Length);
                for (int i = 0; i < cmbChargesProvider.Items.Count; i++)
                {
                    //cmbChargesProvider.SelectedIndex = i;
                    //if (i == cmbChargesProvider.Items.Count - 1)
                    //    sbChrgProviders.Append(cmbChargesProvider.SelectedValue.ToString());
                    //else
                    //    sbChrgProviders.Append(cmbChargesProvider.SelectedValue.ToString() + ",");


                    if (i == cmbChargesProvider.Items.Count - 1)
                        sbChrgProviders.Append(Convert.ToString(((DataRowView)cmbChargesProvider.Items[i])["ID"]));
                    else
                        sbChrgProviders.Append(Convert.ToString(((DataRowView)cmbChargesProvider.Items[i])["ID"]) + ",");

                    if (i == cmbChargesProvider.Items.Count - 1)
                        sbChrgProvidersName.Append(Convert.ToString(((DataRowView)cmbChargesProvider.Items[i])["DispName"]));
                    else
                        sbChrgProvidersName.Append(Convert.ToString(((DataRowView)cmbChargesProvider.Items[i])["DispName"]) + ",");

                }

                #endregion

                #region " Charges Facility "

                sbChrgFacility.Remove(0, sbChrgFacility.Length);
                sbChrgFacilityName.Remove(0, sbChrgFacilityName.Length);
                for (int i = 0; i < cmbChargesMultiFacility.Items.Count; i++)
                {
                    //cmbChargesMultiFacility.SelectedIndex = i;
                    //if (i == cmbChargesMultiFacility.Items.Count - 1)
                    //    sbChrgFacility.Append(cmbChargesMultiFacility.SelectedValue.ToString());
                    //else
                    //    sbChrgFacility.Append(cmbChargesMultiFacility.SelectedValue.ToString() + ",");

                    if (i == cmbChargesMultiFacility.Items.Count - 1)
                        sbChrgFacility.Append(Convert.ToString(((DataRowView)cmbChargesMultiFacility.Items[i])["ID"]));
                    else
                        sbChrgFacility.Append(Convert.ToString(((DataRowView)cmbChargesMultiFacility.Items[i])["ID"]) + ",");

                    if (i == cmbChargesMultiFacility.Items.Count - 1)
                        sbChrgFacilityName.Append(Convert.ToString(((DataRowView)cmbChargesMultiFacility.Items[i])["DispName"]));
                    else
                        sbChrgFacilityName.Append(Convert.ToString(((DataRowView)cmbChargesMultiFacility.Items[i])["DispName"]) + ",");
                }

                #endregion

                #region "Charges User"

                sbChrgUser.Remove(0, sbChrgUser.Length);
                sbChrgUserName.Remove(0, sbChrgUserName.Length);
                for (int i = 0; i < cmbChargesUser.Items.Count; i++)
                {
                    
                    //cmbChargesUser.SelectedIndex = i;
                    //if (i == cmbChargesUser.Items.Count - 1)
                    //    sbChrgUser.Append(cmbChargesUser.SelectedValue.ToString());
                    //else
                    //    sbChrgUser.Append(cmbChargesUser.SelectedValue.ToString() + ",");

                    if (i == cmbChargesUser.Items.Count - 1)
                        sbChrgUser.Append(Convert.ToString(((DataRowView)cmbChargesUser.Items[i])["ID"]));
                    else
                        sbChrgUser.Append(Convert.ToString(((DataRowView)cmbChargesUser.Items[i])["ID"]) + ",");

                    if (i == cmbChargesUser.Items.Count - 1)
                        sbChrgUserName.Append(Convert.ToString(((DataRowView)cmbChargesUser.Items[i])["DispName"]));
                    else
                        sbChrgUserName.Append(Convert.ToString(((DataRowView)cmbChargesUser.Items[i])["DispName"]) + ",");

                }

                #endregion

                #region " Charges Tray "

                sbChrgTrayName.Remove(0, sbChrgTrayName.Length);
                sbChrgTray.Remove(0, sbChrgTray.Length);
                for (int i = 0; i < cmbMultiChargesTray.Items.Count; i++)
                {
                    //cmbMultiChargesTray.SelectedIndex = i;
                    //if (i == cmbMultiChargesTray.Items.Count - 1)
                    //    sbChrgTray.Append(cmbMultiChargesTray.SelectedValue.ToString());
                    //else
                    //    sbChrgTray.Append(cmbMultiChargesTray.SelectedValue.ToString() + ",");

                    if (i == cmbMultiChargesTray.Items.Count - 1)
                        sbChrgTray.Append(Convert.ToString(((DataRowView)cmbMultiChargesTray.Items[i])["ID"]));
                    else
                        sbChrgTray.Append(Convert.ToString(((DataRowView)cmbMultiChargesTray.Items[i])["ID"]) + ",");

                    if (i == cmbMultiChargesTray.Items.Count - 1)
                        sbChrgTrayName.Append(Convert.ToString(((DataRowView)cmbMultiChargesTray.Items[i])["DispName"]));
                    else
                        sbChrgTrayName.Append(Convert.ToString(((DataRowView)cmbMultiChargesTray.Items[i])["DispName"]) + ",");

                }

                #endregion

                #region " Main Report "

                _sqlcommand = new SqlCommand();
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                da = new SqlDataAdapter(_sqlcommand);
                _sqlcommand.Connection = oConnection;

                #region " Parameters "

                if (sbChrgProviders.ToString() != "" && sbChrgProviders.Length > 0)
                {
                    _sqlcommand.Parameters.Add("@sProviderID", System.Data.SqlDbType.VarChar);
                    _sqlcommand.Parameters["@sProviderID"].Value = sbChrgProviders.ToString();

                }

                if (sbChrgFacility.ToString() != "" && sbChrgFacility.Length > 0)
                {
                    _sqlcommand.Parameters.Add("@sFacilityCode", System.Data.SqlDbType.VarChar);
                    _sqlcommand.Parameters["@sFacilityCode"].Value = sbChrgFacility.ToString();

                }

                //---------Abhisekh 12 june start 
                //if (rbchrgPreForClose.Checked)
                //{
                    if (dtpCloseDate.Text != "" && dtpCloseDate.Text.Length > 0)
                    {
                        _sqlcommand.Parameters.Add("@nCloseDate", System.Data.SqlDbType.BigInt);
                        _sqlcommand.Parameters["@nCloseDate"].Value = gloDateMaster.gloDate.DateAsNumber(dtpCloseDate.Text.ToString());

                    }
                //}
                //else
                //{
                //    if (dtpChrgStartDate.Text != null && dtpChrgStartDate.Text.Length > 0)
                //    {
                //        _sqlcommand.Parameters.Add("@nStartDate", System.Data.SqlDbType.BigInt);
                //        _sqlcommand.Parameters["@nStartDate"].Value = gloDateMaster.gloDate.DateAsNumber(dtpChrgStartDate.Text.ToString());

                //    }

                //    if (dtpChrgEndDate.Text != null && dtpChrgEndDate.Text.Length > 0)
                //    {
                //        _sqlcommand.Parameters.Add("@nEndDate", System.Data.SqlDbType.BigInt);
                //        _sqlcommand.Parameters["@nEndDate"].Value = gloDateMaster.gloDate.DateAsNumber(dtpChrgEndDate.Text.ToString());

                //    }
                //}
                    //---------Abhisekh 12 june end 
                if (sbChrgUser.ToString() != "" && sbChrgUser.Length > 0)
                {
                    _sqlcommand.Parameters.Add("@sUserID", System.Data.SqlDbType.VarChar);
                    _sqlcommand.Parameters["@sUserID"].Value = sbChrgUser.ToString();
                }

                if (sbChrgTray.ToString() != "" && sbChrgTray.Length > 0)
                {
                    _sqlcommand.Parameters.Add("@sChargeTryCode", System.Data.SqlDbType.VarChar);
                    _sqlcommand.Parameters["@sChargeTryCode"].Value = sbChrgTray.ToString();
                }

                if(cmbChrgSortBy != null)
                {
                  //  if(cmbChrgSortBy.SelectedIndex !=null)
                    {
                        _sqlcommand.Parameters.Add("@SortBy", System.Data.SqlDbType.VarChar);
                        _sqlcommand.Parameters["@SortBy"].Value = cmbChrgSortBy.SelectedValue.ToString(); ;

                        
                    }
                }

                #endregion
                

                _sqlcommand.CommandText = "Rpt_ChargesTray";
                da.Fill(dsReports, "dt_ChargesTray");
                da.Dispose();
                da = null;
                _sqlcommand.Parameters.Clear();
                _sqlcommand.Dispose();
                _sqlcommand = null;
                #endregion


                #region " Sub Report "

                _sqlcommand = new SqlCommand();
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                da = new SqlDataAdapter(_sqlcommand);
                _sqlcommand.Connection = oConnection;
                if (sbChrgProviders.ToString() != "" && sbChrgProviders.Length > 0)
                {
                    _sqlcommand.Parameters.Add("@sProviderID", System.Data.SqlDbType.VarChar);
                    _sqlcommand.Parameters["@sProviderID"].Value = sbChrgProviders.ToString();

                }

                if (sbChrgFacility.ToString() != "" && sbChrgFacility.Length > 0)
                {
                    _sqlcommand.Parameters.Add("@sFacilityCode", System.Data.SqlDbType.VarChar);
                    _sqlcommand.Parameters["@sFacilityCode"].Value = sbChrgFacility.ToString();

                }
                //---------Abhisekh 12 june start 
                //if (rbchrgPreForClose.Checked)
                //{
                    if (dtpCloseDate.Text != "" && dtpCloseDate.Text.Length > 0)
                    {
                        _sqlcommand.Parameters.Add("@nCloseDate", System.Data.SqlDbType.BigInt);
                        _sqlcommand.Parameters["@nCloseDate"].Value = gloDateMaster.gloDate.DateAsNumber(dtpCloseDate.Text.ToString());

                    }
                //}
                //else
                //{
                //    if (dtpChrgStartDate.Text != null && dtpChrgStartDate.Text.Length > 0)
                //    {
                //        _sqlcommand.Parameters.Add("@nStartDate", System.Data.SqlDbType.BigInt);
                //        _sqlcommand.Parameters["@nStartDate"].Value = gloDateMaster.gloDate.DateAsNumber(dtpChrgStartDate.Text.ToString());

                //    }

                //    if (dtpChrgEndDate.Text != null && dtpChrgEndDate.Text.Length > 0)
                //    {
                //        _sqlcommand.Parameters.Add("@nEndDate", System.Data.SqlDbType.BigInt);
                //        _sqlcommand.Parameters["@nEndDate"].Value = gloDateMaster.gloDate.DateAsNumber(dtpChrgEndDate.Text.ToString());

                //    }
                //}
                    //---------Abhisekh 12 june end 
                if (sbChrgUser.ToString() != "" && sbChrgUser.Length > 0)
                {
                    _sqlcommand.Parameters.Add("@sUserID", System.Data.SqlDbType.VarChar);
                    _sqlcommand.Parameters["@sUserID"].Value = sbChrgUser.ToString();
                }

                if (sbChrgTray.ToString() != "" && sbChrgTray.Length > 0)
                {
                    _sqlcommand.Parameters.Add("@sChargeTryCode", System.Data.SqlDbType.VarChar);
                    _sqlcommand.Parameters["@sChargeTryCode"].Value = sbChrgTray.ToString();
                }

                _sqlcommand.CommandText = "Rpt_DailyChargesSummary";
                da.Fill(dsReports, "dt_DailyChargesSummary");

                #endregion

                da.Dispose();
                da = null;
                _sqlcommand.Parameters.Clear();
                _sqlcommand.Dispose();
                _sqlcommand = null;

                //DataTable dtCloseReportParamCharges = new DataTable();
                //dtCloseReportParamCharges.Columns.Add("userName");
                //dtCloseReportParamCharges.Columns.Add("reportType");
                //dtCloseReportParamCharges.Columns.Add("closeDate");
                //dtCloseReportParamCharges.Columns.Add("endDate");
                //dtCloseReportParamCharges.Columns.Add("sortBy");
                //dtCloseReportParamCharges.Columns.Add("Providers");
                //dtCloseReportParamCharges.Columns.Add("Facility");
                //dtCloseReportParamCharges.Columns.Add("user");
                //dtCloseReportParamCharges.Columns.Add("tray");
                //dsReports.Tables["dtCloseReportParamCharges"].Rows.Add();
                //dsReports.Tables["dtCloseReportParamCharges"].Rows[0]["userName"] = _UserName;
                //dsReports.Tables["dtCloseReportParamCharges"].Rows[0]["reportType"] = "Daily";
                //dsReports.Tables["dtCloseReportParamCharges"].Rows[0]["closeDate"] = (dtpCloseDate.Value.ToString() == "" ? DateTime.Now.ToShortDateString() : dtpCloseDate.Value.ToShortDateString());
                //dsReports.Tables["dtCloseReportParamCharges"].Rows[0]["endDate"] = (dtpCloseDate.Value.ToString() == "" ? DateTime.Now.ToShortDateString() : dtpCloseDate.Value.ToShortDateString());
                //dsReports.Tables["dtCloseReportParamCharges"].Rows[0]["sortBy"] = (cmbChrgSortBy.Text == "Claim #" ? "Claim #" : (cmbChrgSortBy.Text == "Patient Name" ? "Patient Name" : (cmbChrgSortBy.Text == "Provider" ? "Provider" : (cmbChrgSortBy.Text == "Facility" ? "Facility" : (cmbChrgSortBy.Text == "User" ? "User" : (cmbChrgSortBy.Text == "Tray" ? "Tray" : "Claim #"))))));
                //dsReports.Tables["dtCloseReportParamCharges"].Rows[0]["Providers"] = sbChrgProvidersName.ToString();
                //dsReports.Tables["dtCloseReportParamCharges"].Rows[0]["Facility"] = sbChrgFacilityName.ToString();
                //dsReports.Tables["dtCloseReportParamCharges"].Rows[0]["user"] = sbChrgUserName.ToString();
                //dsReports.Tables["dtCloseReportParamCharges"].Rows[0]["tray"] = sbChrgTrayName.ToString();
                //dsReports.Tables["dtCloseReportParamCharges"].AcceptChanges();
                //dsReports.Tables["dtCloseReportParamCharges"].TableName = "dtCloseReportParamCharges";
                //dsReports.Tables.Add(dtCloseReportParamCharges);
                //objrptChargesSummary.Subreports["Rpt_DailyChargesSubReport.rpt"].SetDataSource(dsReports.Tables["dt_ChargesTray"]);
                objrptChargesSummary.SetDataSource(dsReports);
                crvRptViewCharges.ReportSource = objrptChargesSummary;
                objrptChargesSummary.SetParameterValue("userName", _UserName);

                objrptChargesSummary.SetParameterValue("reportType", "Daily");
                objrptChargesSummary.SetParameterValue("closeDate", (dtpCloseDate.Value.ToString() == "" ? DateTime.Now.ToShortDateString() : dtpCloseDate.Value.ToShortDateString()));
                objrptChargesSummary.SetParameterValue("endDate", (dtpCloseDate.Value.ToString() == "" ? DateTime.Now.ToShortDateString() : dtpCloseDate.Value.ToShortDateString()));

                objrptChargesSummary.SetParameterValue("sortBy", (cmbChrgSortBy.Text == "Claim #" ? "Claim #" : (cmbChrgSortBy.Text == "Patient Name" ? "Patient Name" : (cmbChrgSortBy.Text == "Provider" ? "Provider" : (cmbChrgSortBy.Text == "Facility" ? "Facility" : (cmbChrgSortBy.Text == "User" ? "User" : (cmbChrgSortBy.Text == "Tray" ? "Tray" : "Claim #")))))));
                objrptChargesSummary.SetParameterValue("Providers", sbChrgProvidersName.ToString().Replace(",", ", "));
                objrptChargesSummary.SetParameterValue("Facility", sbChrgFacilityName.ToString().Replace(",", ", "));
                objrptChargesSummary.SetParameterValue("user", sbChrgUserName.ToString().Replace(",", ", "));
                objrptChargesSummary.SetParameterValue("tray", sbChrgTrayName.ToString().Replace(",", ", "));
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Parameters.Clear();
                    _sqlcommand.Dispose();
                    _sqlcommand = null;
                }
                if (oConnection != null && oConnection.State == ConnectionState.Open)
                {
                    oConnection.Close();
               
                }
                if (oConnection != null)
                {
                    oConnection.Dispose();

                }
            }
        }

        #endregion

        #region "Charges Show Hide Events"

        private void btnChrgDown_Click(object sender, EventArgs e)
        {
            try
            {
                flwPnlDailyCharges.Visible = true;
                btnChrgDown.BackgroundImage = global::gloReports.Properties.Resources.UP;
                btnChrgDown.BackgroundImageLayout = ImageLayout.Center;
                btnChrgUP.Visible = true;
                btnChrgDown.Visible = false;
                label7.Visible = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }

        }

        private void btnChrgUP_Click(object sender, EventArgs e)
        {
            try
            {
                flwPnlDailyCharges.Visible = false;
                btnChrgDown.BackgroundImage = global::gloReports.Properties.Resources.Down;
                btnChrgDown.BackgroundImageLayout = ImageLayout.Center;
                btnChrgUP.Visible = false;
                btnChrgDown.Visible = true;
                label7.Visible = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
            }
        }

        private void btnChrgDown_MouseHover(object sender, EventArgs e)
        {
            btnChrgDown.BackgroundImage = global::gloReports.Properties.Resources.DownHover;
            btnChrgDown.BackgroundImageLayout = ImageLayout.Center;

        }

        private void btnChrgDown_MouseLeave(object sender, EventArgs e)
        {
            btnChrgDown.BackgroundImage = global::gloReports.Properties.Resources.Down;
            btnChrgDown.BackgroundImageLayout = ImageLayout.Center;

        }

        private void btnChrgUP_MouseHover(object sender, EventArgs e)
        {
            btnChrgUP.BackgroundImage = global::gloReports.Properties.Resources.UPHover;
            btnChrgUP.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnChrgUP_MouseLeave(object sender, EventArgs e)
        {
            btnChrgUP.BackgroundImage = global::gloReports.Properties.Resources.UP;
            btnChrgUP.BackgroundImageLayout = ImageLayout.Center;
        }

        #endregion

        #region "Charges User Control Events "

        private void btnBrowseProvider_Click(object sender, EventArgs e)
        {
            try
            {
                FillProviders(cmbChargesProvider);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void btnClearProvider_Click(object sender, EventArgs e)
        {
            ClearCombo(cmbChargesProvider);
        }

        private void btnBrowseMultiFacility_Click(object sender, EventArgs e)
        {
            try
            {
                FillFacilities(cmbChargesMultiFacility);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void btnClearMultiFacility_Click(object sender, EventArgs e)
        {
            ClearCombo(cmbChargesMultiFacility);
        }

        private void btnBrowseMultiChargesTray_Click(object sender, EventArgs e)
        {
            try
            {
                FillChargesTray(cmbMultiChargesTray);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void btnClearMultiChargesTray_Click(object sender, EventArgs e)
        {
            ClearCombo(cmbMultiChargesTray);
        }

        private void btnBrowseUser_Click(object sender, EventArgs e)
        {
            try
            {
                FillUsers(cmbChargesUser);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void btnClearUser_Click(object sender, EventArgs e)
        {
            ClearCombo(cmbChargesUser);
        }


        private void cmb_Chargesdatefilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int _filterby = 0;

            //_filterby = cmb_Chargesdatefilter.SelectedIndex;
            //switch (_filterby)
            //{
            //    case 0:
            //        FilterBy_DateRange(dtpChrgStartDate, dtpChrgEndDate);
            //        break;

            //    case 1:
            //        FilterBy_Today(dtpChrgStartDate, dtpChrgEndDate);
            //        break;

            //    case 2:
            //        FilterBy_Tomorrow(dtpChrgStartDate, dtpChrgEndDate);
            //        break;

            //    case 3:
            //        FilterBy_Yesterday(dtpChrgStartDate, dtpChrgEndDate);
            //        break;

            //    case 4:
            //        FilterBy_Thisweek(dtpChrgStartDate, dtpChrgEndDate);
            //        break;

            //    case 5:
            //        FilterBy_lastweek(dtpChrgStartDate, dtpChrgEndDate);
            //        break;

            //    case 6:
            //        FilterBy_currentmonth(dtpChrgStartDate, dtpChrgEndDate);
            //        break;

            //    case 7:
            //        FilterBy_lastmonth(dtpChrgStartDate, dtpChrgEndDate);
            //        break;

            //    case 8:
            //        FilterBy_currenYear(dtpChrgStartDate, dtpChrgEndDate);
            //        break;

            //    case 9://Last 30 days
            //        FilterBy_last30days(dtpChrgStartDate, dtpChrgEndDate);
            //        break;

            //    case 10:
            //        FilterBy_last60days(dtpChrgStartDate, dtpChrgEndDate);
            //        break;

            //    case 11:
            //        FilterBy_last90days(dtpChrgStartDate, dtpChrgEndDate);
            //        break;

            //    case 12:
            //        FilterBy_last120days(dtpChrgStartDate, dtpChrgEndDate);
            //        break;
            //}
        }

        //private void rbchrgPreForClose_CheckedChanged(object sender, EventArgs e)
        //{

        //    if (rbchrgPreForClose.Checked == true)
        //        rbchrgPreForClose.Font = new Font("Tahoma", 9, FontStyle.Bold);
        //    else
        //        rbchrgPreForClose.Font = new Font("Tahoma", 9, FontStyle.Regular);

        //    pnlTransDate.Visible = false;
        //    pnlChrgDates.Visible = false;
        //    pnlCloseDate.Visible = true;
        //}

        //private void rbChrgAudit_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (rbChrgAudit.Checked == true)
        //        rbChrgAudit.Font = new Font("Tahoma", 9, FontStyle.Bold);
        //    else
        //        rbChrgAudit.Font = new Font("Tahoma", 9, FontStyle.Regular);

        //    //pnlTransDate.Visible = true;
        //    pnlTransDate.Visible = false;
        //    pnlChrgDates.Visible = true;
        //    pnlCloseDate.Visible = false;

        //}

        #endregion


        #endregion

        #region " ------------------------- Payment ----------------------------------------- "

        #region " Fill Methods "

        private void FillDailyPayment()
        {

            dsEOBPaymentReports dsReports = new dsEOBPaymentReports();
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();
            SqlDataAdapter da = null;

            try
            {

                oConnection.ConnectionString = _databaseconnectionstring;

                #region "Payment Providers "

                sbPayProviders.Remove(0, sbPayProviders.Length);
                sbPayProvidersName.Remove(0, sbPayProvidersName.Length);
                for (int i = 0; i < cmbPaymentProvider.Items.Count; i++)
                {
                    //cmbPaymentProvider.SelectedIndex = i;
                    //if (i == cmbPaymentProvider.Items.Count - 1)
                    //    sbPayProviders.Append(cmbPaymentProvider.SelectedValue.ToString());
                    //else
                    //    sbPayProviders.Append(cmbPaymentProvider.SelectedValue.ToString() + ",");

                    if (i == cmbPaymentProvider.Items.Count - 1)
                        sbPayProviders.Append(Convert.ToString(((DataRowView)cmbPaymentProvider.Items[i])["ID"]));
                    else
                        sbPayProviders.Append(Convert.ToString(((DataRowView)cmbPaymentProvider.Items[i])["ID"]) + ",");


                    if (i == cmbPaymentProvider.Items.Count - 1)
                        sbPayProvidersName.Append(Convert.ToString(((DataRowView)cmbPaymentProvider.Items[i])["DispName"]));
                    else
                        sbPayProvidersName.Append(Convert.ToString(((DataRowView)cmbPaymentProvider.Items[i])["DispName"]) + ",");
                }

                #endregion

                #region " Payment Facility "

                sbPayFacility.Remove(0, sbPayFacility.Length);
                sbPayFacilityName.Remove(0, sbPayFacilityName.Length);
                for (int i = 0; i < cmbPayFacility.Items.Count; i++)
                {
                    //cmbPayFacility.SelectedIndex = i;
                    //if (i == cmbPayFacility.Items.Count - 1)
                    //    sbPayFacility.Append(cmbPayFacility.SelectedValue.ToString());
                    //else
                    //    sbPayFacility.Append(cmbPayFacility.SelectedValue.ToString() + ",");

                    if (i == cmbPayFacility.Items.Count - 1)
                        sbPayFacility.Append(Convert.ToString(((DataRowView)cmbPayFacility.Items[i])["ID"]));
                    else
                        sbPayFacility.Append(Convert.ToString(((DataRowView)cmbPayFacility.Items[i])["ID"]) + ",");

                    if (i == cmbPayFacility.Items.Count - 1)
                        sbPayFacilityName.Append(Convert.ToString(((DataRowView)cmbPayFacility.Items[i])["DispName"]));
                    else
                        sbPayFacilityName.Append(Convert.ToString(((DataRowView)cmbPayFacility.Items[i])["DispName"]) + ",");
                }

                #endregion

                #region "Payment User"

                sbPayUser.Remove(0, sbPayUser.Length);
                sbPayUserName.Remove(0, sbPayUserName.Length);
                for (int i = 0; i < cmbPayUser.Items.Count; i++)
                {
                    //cmbPayUser.SelectedIndex = i;
                    //if (i == cmbPayUser.Items.Count - 1)
                    //    sbPayUser.Append(cmbPayUser.SelectedValue.ToString());
                    //else
                    //    sbPayUser.Append(cmbPayUser.SelectedValue.ToString() + ",");


                    if (i == cmbPayUser.Items.Count - 1)
                        sbPayUser.Append(Convert.ToString(((DataRowView)cmbPayUser.Items[i])["ID"]));
                    else
                        sbPayUser.Append(Convert.ToString(((DataRowView)cmbPayUser.Items[i])["ID"]) + ",");

                    if (i == cmbPayUser.Items.Count - 1)
                        sbPayUserName.Append(Convert.ToString(((DataRowView)cmbPayUser.Items[i])["DispName"]));
                    else
                        sbPayUserName.Append(Convert.ToString(((DataRowView)cmbPayUser.Items[i])["DispName"]) + ",");
                }

                #endregion

                #region " Payment Tray "

                sbPayTray.Remove(0, sbPayTray.Length);
                sbPayTrayName.Remove(0, sbPayTrayName.Length);
                for (int i = 0; i < cmbPayTray.Items.Count; i++)
                {
                    //cmbPayTray.SelectedIndex = i;
                    //if (i == cmbPayTray.Items.Count - 1)
                    //    sbPayTray.Append(cmbPayTray.SelectedValue.ToString());
                    //else
                    //    sbPayTray.Append(cmbPayTray.SelectedValue.ToString() + ",");

                    if (i == cmbPayTray.Items.Count - 1)
                        sbPayTray.Append(Convert.ToString(((DataRowView)cmbPayTray.Items[i])["ID"]));
                    else
                        sbPayTray.Append(Convert.ToString(((DataRowView)cmbPayTray.Items[i])["ID"]) + ",");

                    if (i == cmbPayTray.Items.Count - 1)
                        sbPayTrayName.Append(Convert.ToString(((DataRowView)cmbPayTray.Items[i])["DispName"]));
                    else
                        sbPayTrayName.Append(Convert.ToString(((DataRowView)cmbPayTray.Items[i])["DispName"]) + ",");

                }

                #endregion

                #region " Payment Type "

                sbPayType.Remove(0, sbPayType.Length);
                sbPayTypeName.Remove(0, sbPayTypeName.Length);
                for (int i = 0; i < cmbPayType.Items.Count; i++)
                {
                    //cmbPayType.SelectedIndex = i;
                    //if (i == cmbPayType.Items.Count - 1)
                    //    sbPayType.Append(cmbPayType.SelectedValue.ToString());
                    //else
                    //    sbPayType.Append(cmbPayType.SelectedValue.ToString() + ",");

                    if (i == cmbPayType.Items.Count - 1)
                        sbPayType.Append(Convert.ToString(((DataRowView)cmbPayType.Items[i])["ID"]));
                    else
                        sbPayType.Append(Convert.ToString(((DataRowView)cmbPayType.Items[i])["ID"]) + ",");

                    if (i == cmbPayType.Items.Count - 1)
                        sbPayTypeName.Append(Convert.ToString(((DataRowView)cmbPayType.Items[i])["DispName"]));
                    else
                        sbPayTypeName.Append(Convert.ToString(((DataRowView)cmbPayType.Items[i])["DispName"]) + ",");


                }

                #endregion



                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.Connection = oConnection;
                da = new SqlDataAdapter(_sqlcommand);
                _sqlcommand.Connection = oConnection;

                #region " Parameters "

                if (sbPayProviders.ToString() != "" && sbPayProviders.Length > 0)
                {
                    _sqlcommand.Parameters.Add("@sProviderID", System.Data.SqlDbType.VarChar);
                    _sqlcommand.Parameters["@sProviderID"].Value = sbPayProviders.ToString();

                }

                if (sbPayFacility.ToString() != "" && sbPayFacility.Length > 0)
                {
                    _sqlcommand.Parameters.Add("@sFacilityCode", System.Data.SqlDbType.VarChar);
                    _sqlcommand.Parameters["@sFacilityCode"].Value = sbPayFacility.ToString();

                }

                //---------Abhisekh 12 june start 
                //if (rbPayPreForClose.Checked)
                //{
                    if (dtPayCloseDate.Value != null && dtPayCloseDate.Value.ToShortDateString().Length > 0)
                    {
                        _sqlcommand.Parameters.Add("@nCloseDate", System.Data.SqlDbType.BigInt);
                        _sqlcommand.Parameters["@nCloseDate"].Value = gloDateMaster.gloDate.DateAsNumber(dtPayCloseDate.Value.ToString());

                    }
                //}
                //else
                //{
                //    if (dtpPayStartDate.Text != null && dtpPayStartDate.Text.Length > 0)
                //    {
                //        _sqlcommand.Parameters.Add("@nStartDate", System.Data.SqlDbType.BigInt);
                //        _sqlcommand.Parameters["@nStartDate"].Value = gloDateMaster.gloDate.DateAsNumber(dtpPayStartDate.Text.ToString());

                //    }

                //    if (dtpPayEndDate.Text != null && dtpPayEndDate.Text.Length > 0)
                //    {
                //        _sqlcommand.Parameters.Add("@nEndDate", System.Data.SqlDbType.BigInt);
                //        _sqlcommand.Parameters["@nEndDate"].Value = gloDateMaster.gloDate.DateAsNumber(dtpPayEndDate.Text.ToString());

                //    }
                //}
                    //---------Abhisekh 12 june end 
                if (sbPayUser.ToString() != "" && sbPayUser.Length > 0)
                {
                    _sqlcommand.Parameters.Add("@sUserID", System.Data.SqlDbType.VarChar);
                    _sqlcommand.Parameters["@sUserID"].Value = sbPayUser.ToString();
                }

                if (sbPayTray.ToString() != "" && sbPayTray.Length > 0)
                {
                    _sqlcommand.Parameters.Add("@sPaymentTray", System.Data.SqlDbType.VarChar);
                    _sqlcommand.Parameters["@sPaymentTray"].Value = sbPayTray.ToString();
                }

                if (sbPayType.ToString() != "" && sbPayType.Length > 0)
                {
                    _sqlcommand.Parameters.Add("@PaymentType", System.Data.SqlDbType.VarChar);
                    _sqlcommand.Parameters["@PaymentType"].Value = sbPayType.ToString();
                }

                if (cmbPaySortBy != null)
                {
                //    if (cmbPaySortBy.SelectedIndex != null)
                    {
                        _sqlcommand.Parameters.Add("@SortBy", System.Data.SqlDbType.VarChar);
                        _sqlcommand.Parameters["@SortBy"].Value = cmbPaySortBy.SelectedValue.ToString(); ;
                    }
                }

                #endregion

                _sqlcommand.CommandText = "BL_SELECT_EOBDailyPayment";
                da.Fill(dsReports, "dt_EOBDailyPayment");
                da.Dispose();
                da = null;
                _sqlcommand.Parameters.Clear();
                _sqlcommand.Dispose();
                _sqlcommand = null;
                if (dsReports.Tables["dt_EOBDailyPayment"].Rows.Count == 0)
                {
                    DataRow newRow = dsReports.Tables["dt_EOBDailyPayment"].NewRow();

                    newRow["CrChMoNo"] = "";
                    newRow["CrChMoDate"] = "";
                    newRow["sPaymentNo"] = "";
                    newRow["PaymentMode"] = 0;
                    newRow["PaymentModeText"] = "";
                    newRow["PatientCode"] = "";
                    newRow["PatientName"] = "";
                    //newRow["PaidAmount"] = 0;
                    newRow["CloseDayTray"] = "";
                    newRow["UserName"] = " ";

                    dsReports.Tables["dt_EOBDailyPayment"].Rows.Add(newRow);
                }

                #region " Summary " 

                _sqlcommand = new SqlCommand();
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                da = new SqlDataAdapter(_sqlcommand);
                _sqlcommand.Connection = oConnection;


                #region " Parameters "

                if (sbPayProviders.ToString() != "" && sbPayProviders.Length > 0)
                {
                    _sqlcommand.Parameters.Add("@sProviderID", System.Data.SqlDbType.VarChar);
                    _sqlcommand.Parameters["@sProviderID"].Value = sbPayProviders.ToString();

                }

                if (sbPayFacility.ToString() != "" && sbPayFacility.Length > 0)
                {
                    _sqlcommand.Parameters.Add("@sFacilityCode", System.Data.SqlDbType.VarChar);
                    _sqlcommand.Parameters["@sFacilityCode"].Value = sbPayFacility.ToString();

                }

                //---------Abhisekh 12 june start 
                //if (rbPayPreForClose.Checked)
                //{
                    //if (dtPayCloseDate.Text != null && dtPayCloseDate.Text.Length > 0)
                    //{
                    if (dtPayCloseDate.Value != null && dtPayCloseDate.Value.ToShortDateString().Length > 0)
                    {
                        _sqlcommand.Parameters.Add("@nCloseDate", System.Data.SqlDbType.BigInt);
                        //_sqlcommand.Parameters["@nCloseDate"].Value = gloDateMaster.gloDate.DateAsNumber(dtPayCloseDate.Text.ToString());
                        _sqlcommand.Parameters["@nCloseDate"].Value = gloDateMaster.gloDate.DateAsNumber(dtPayCloseDate.Value.ToString());
                       
                    }
                //}
                //else
                //{
                //    if (dtpPayStartDate.Text != null && dtpPayStartDate.Text.Length > 0)
                //    {
                //        _sqlcommand.Parameters.Add("@nStartDate", System.Data.SqlDbType.BigInt);
                //        _sqlcommand.Parameters["@nStartDate"].Value = gloDateMaster.gloDate.DateAsNumber(dtpPayStartDate.Text.ToString());

                //    }

                //    if (dtpPayEndDate.Text != null && dtpPayEndDate.Text.Length > 0)
                //    {
                //        _sqlcommand.Parameters.Add("@nEndDate", System.Data.SqlDbType.BigInt);
                //        _sqlcommand.Parameters["@nEndDate"].Value = gloDateMaster.gloDate.DateAsNumber(dtpPayEndDate.Text.ToString());

                //    }
                //}
                    //---------Abhisekh 12 june end 
                if (sbPayUser.ToString() != "" && sbPayUser.Length > 0)
                {
                    _sqlcommand.Parameters.Add("@sUserID", System.Data.SqlDbType.VarChar);
                    _sqlcommand.Parameters["@sUserID"].Value = sbPayUser.ToString();
                }

                if (sbPayTray.ToString() != "" && sbPayTray.Length > 0)
                {
                    _sqlcommand.Parameters.Add("@sPaymentTray", System.Data.SqlDbType.VarChar);
                    _sqlcommand.Parameters["@sPaymentTray"].Value = sbPayTray.ToString();
                }

                if (sbPayType.ToString() != "" && sbPayType.Length > 0)
                {
                    _sqlcommand.Parameters.Add("@PaymentType", System.Data.SqlDbType.VarChar);
                    _sqlcommand.Parameters["@PaymentType"].Value = sbPayType.ToString();
                }


                _sqlcommand.CommandText = "BL_EOBDailyPaySummary";
                da.Fill(dsReports, "dt_EOBDailyPaySummary");
                da.Dispose();
                da = null;
                _sqlcommand.Parameters.Clear();
                _sqlcommand.Dispose();
                _sqlcommand = null;
                #endregion


              

                #region " Voided Payment Summary"

                _sqlcommand = new SqlCommand();
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                da = new SqlDataAdapter(_sqlcommand);
                _sqlcommand.Connection = oConnection;

                if (sbPayProviders.ToString() != "" && sbPayProviders.Length > 0)
                {
                    _sqlcommand.Parameters.Add("@sProviderID", System.Data.SqlDbType.VarChar);
                    _sqlcommand.Parameters["@sProviderID"].Value = sbPayProviders.ToString();

                }

                if (sbPayFacility.ToString() != "" && sbPayFacility.Length > 0)
                {
                    _sqlcommand.Parameters.Add("@sFacilityCode", System.Data.SqlDbType.VarChar);
                    _sqlcommand.Parameters["@sFacilityCode"].Value = sbPayFacility.ToString();

                }

                //---------Abhisekh 12 june start 
                //if (rbPayPreForClose.Checked)
                //{
                    //if (dtPayCloseDate.Text != null && dtPayCloseDate.Text.Length > 0)
                    //{
                    if (dtPayCloseDate.Value != null && dtPayCloseDate.Value.ToShortDateString().Length > 0)
                    {
                        _sqlcommand.Parameters.Add("@nCloseDate", System.Data.SqlDbType.BigInt);
                        //_sqlcommand.Parameters["@nCloseDate"].Value = gloDateMaster.gloDate.DateAsNumber(dtPayCloseDate.Text.ToString());
                        _sqlcommand.Parameters["@nCloseDate"].Value = gloDateMaster.gloDate.DateAsNumber(dtPayCloseDate.Value.ToString());

                    }
                //}
                //else
                //{
                //    if (dtpPayStartDate.Text != null && dtpPayStartDate.Text.Length > 0)
                //    {
                //        _sqlcommand.Parameters.Add("@nStartDate", System.Data.SqlDbType.BigInt);
                //        _sqlcommand.Parameters["@nStartDate"].Value = gloDateMaster.gloDate.DateAsNumber(dtpPayStartDate.Text.ToString());

                //    }

                //    if (dtpPayEndDate.Text != null && dtpPayEndDate.Text.Length > 0)
                //    {
                //        _sqlcommand.Parameters.Add("@nEndDate", System.Data.SqlDbType.BigInt);
                //        _sqlcommand.Parameters["@nEndDate"].Value = gloDateMaster.gloDate.DateAsNumber(dtpPayEndDate.Text.ToString());

                //    }
                //}
                    //---------Abhisekh 12 june end 
                if (sbPayUser.ToString() != "" && sbPayUser.Length > 0)
                {
                    _sqlcommand.Parameters.Add("@sUserID", System.Data.SqlDbType.VarChar);
                    _sqlcommand.Parameters["@sUserID"].Value = sbPayUser.ToString();
                }

                if (sbPayTray.ToString() != "" && sbPayTray.Length > 0)
                {
                    _sqlcommand.Parameters.Add("@sPaymentTray", System.Data.SqlDbType.VarChar);
                    _sqlcommand.Parameters["@sPaymentTray"].Value = sbPayTray.ToString();
                }

                if (sbPayType.ToString() != "" && sbPayType.Length > 0)
                {
                    _sqlcommand.Parameters.Add("@PaymentType", System.Data.SqlDbType.VarChar);
                    _sqlcommand.Parameters["@PaymentType"].Value = sbPayType.ToString();
                }

                _sqlcommand.CommandText = "BL_EOBVoidedSummary";
                da.Fill(dsReports, "dt_EOBVoidedPaymentSummary");
                da.Dispose();
                da = null;
                _sqlcommand.Parameters.Clear();
                _sqlcommand.Dispose();
                _sqlcommand = null;
                #endregion " Voided Payment Summary"



                //20100611

                #region " Refund Payment Summary"

                _sqlcommand = new SqlCommand();
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                da = new SqlDataAdapter(_sqlcommand);
                _sqlcommand.Connection = oConnection;


                #region " Parameters "

                if (sbPayProviders.ToString() != "" && sbPayProviders.Length > 0)
                {
                    _sqlcommand.Parameters.Add("@sProviderID", System.Data.SqlDbType.VarChar);
                    _sqlcommand.Parameters["@sProviderID"].Value = sbPayProviders.ToString();

                }

                if (sbPayFacility.ToString() != "" && sbPayFacility.Length > 0)
                {
                    _sqlcommand.Parameters.Add("@sFacilityCode", System.Data.SqlDbType.VarChar);
                    _sqlcommand.Parameters["@sFacilityCode"].Value = sbPayFacility.ToString();

                }

                //---------Abhisekh 12 june start 
                //if (rbPayPreForClose.Checked)
                //{
                    //if (dtPayCloseDate.Text != null && dtPayCloseDate.Text.Length > 0)
                    //{
                    if (dtPayCloseDate.Value != null && dtPayCloseDate.Value.ToShortDateString().Length > 0)
                    {
                        _sqlcommand.Parameters.Add("@nCloseDate", System.Data.SqlDbType.BigInt);
                        //_sqlcommand.Parameters["@nCloseDate"].Value = gloDateMaster.gloDate.DateAsNumber(dtPayCloseDate.Text.ToString());
                        _sqlcommand.Parameters["@nCloseDate"].Value = gloDateMaster.gloDate.DateAsNumber(dtPayCloseDate.Value.ToString());

                    }
                //}
                //else
                //{
                //    if (dtpPayStartDate.Text != null && dtpPayStartDate.Text.Length > 0)
                //    {
                //        _sqlcommand.Parameters.Add("@nStartDate", System.Data.SqlDbType.BigInt);
                //        _sqlcommand.Parameters["@nStartDate"].Value = gloDateMaster.gloDate.DateAsNumber(dtpPayStartDate.Text.ToString());

                //    }

                //    if (dtpPayEndDate.Text != null && dtpPayEndDate.Text.Length > 0)
                //    {
                //        _sqlcommand.Parameters.Add("@nEndDate", System.Data.SqlDbType.BigInt);
                //        _sqlcommand.Parameters["@nEndDate"].Value = gloDateMaster.gloDate.DateAsNumber(dtpPayEndDate.Text.ToString());

                //    }
                //}
                    //---------Abhisekh 12 june end 
                if (sbPayUser.ToString() != "" && sbPayUser.Length > 0)
                {
                    _sqlcommand.Parameters.Add("@sUserID", System.Data.SqlDbType.VarChar);
                    _sqlcommand.Parameters["@sUserID"].Value = sbPayUser.ToString();
                }

                if (sbPayTray.ToString() != "" && sbPayTray.Length > 0)
                {
                    _sqlcommand.Parameters.Add("@sPaymentTray", System.Data.SqlDbType.VarChar);
                    _sqlcommand.Parameters["@sPaymentTray"].Value = sbPayTray.ToString();
                }

                if (sbPayType.ToString() != "" && sbPayType.Length > 0)
                {
                    _sqlcommand.Parameters.Add("@PaymentType", System.Data.SqlDbType.VarChar);
                    _sqlcommand.Parameters["@PaymentType"].Value = sbPayType.ToString();
                }


                _sqlcommand.CommandText = "BL_EOBDailyRefundSummary";
                da.Fill(dsReports, "dt_EOBRefundPaySummary");
                da.Dispose();
                da = null;
                _sqlcommand.Parameters.Clear();
                _sqlcommand.Dispose();
                _sqlcommand = null;
                #endregion

                #endregion



                CrystalDecisions.Shared.ParameterFields paramFields = new CrystalDecisions.Shared.ParameterFields();
                CrystalDecisions.Shared.ParameterDiscreteValue discValue = new CrystalDecisions.Shared.ParameterDiscreteValue();
                CrystalDecisions.Shared.ParameterField paramField = new CrystalDecisions.Shared.ParameterField();
                paramField.Name = "ShowHeader";
                paramField.HasCurrentValue = true;
                int Value = 0;
                if (chkPayIncludeDetails.Checked)
                    Value = 1;
                else
                    Value = 0;

                discValue.Value = Value;
                paramField.CurrentValues.Add(discValue);
                paramFields.Add(paramField);
                this.crvRptViewPayment.ParameterFieldInfo = paramFields;


                #endregion
                //da.Dispose();

                //DataTable dtCloseReportParamPmnt = new DataTable();
                //dtCloseReportParamPmnt.Columns.Add("userName");
                //dtCloseReportParamPmnt.Columns.Add("reportType");
                //dtCloseReportParamPmnt.Columns.Add("closeDate");
                //dtCloseReportParamPmnt.Columns.Add("endDate");
                //dtCloseReportParamPmnt.Columns.Add("sortBy");
                //dtCloseReportParamPmnt.Columns.Add("Providers");
                //dtCloseReportParamPmnt.Columns.Add("Facility");
                //dtCloseReportParamPmnt.Columns.Add("user");
                //dtCloseReportParamPmnt.Columns.Add("tray");
                dsReports.Tables["dtCloseReportParamPmnt"].Rows.Add();
                dsReports.Tables["dtCloseReportParamPmnt"].Rows[0]["userName"] = _UserName;
                dsReports.Tables["dtCloseReportParamPmnt"].Rows[0]["reportType"] = "Daily";
                dsReports.Tables["dtCloseReportParamPmnt"].Rows[0]["closeDate"] = (dtPayCloseDate.Value.ToString() == "" ? DateTime.Now.ToShortDateString() : dtPayCloseDate.Value.ToShortDateString());
                dsReports.Tables["dtCloseReportParamPmnt"].Rows[0]["endDate"] = (dtPayCloseDate.Value.ToString() == "" ? DateTime.Now.ToShortDateString() : dtPayCloseDate.Value.ToShortDateString());
                dsReports.Tables["dtCloseReportParamPmnt"].Rows[0]["sortBy"] = (cmbPaySortBy.Text == "Type" ? "Type" : (cmbPaySortBy.Text == "Payer" ? "Payer" : (cmbPaySortBy.Text == "Tray" ? "Tray" : (cmbPaySortBy.Text == "User" ? "User" : "Type"))));
                dsReports.Tables["dtCloseReportParamPmnt"].Rows[0]["Providers"] = sbPayProvidersName.ToString().Replace(",", ", ");
                dsReports.Tables["dtCloseReportParamPmnt"].Rows[0]["Facility"] = sbPayFacilityName.ToString().Replace(",", ", ");
                dsReports.Tables["dtCloseReportParamPmnt"].Rows[0]["user"] = sbPayUserName.ToString().Replace(",", ", ");
                dsReports.Tables["dtCloseReportParamPmnt"].Rows[0]["tray"] = sbPayTrayName.ToString().Replace(",", ", ");
                dsReports.Tables["dtCloseReportParamPmnt"].Rows[0]["Type"] = sbPayTypeName.ToString().Replace(",", ", ");
                dsReports.Tables["dtCloseReportParamPmnt"].AcceptChanges();
                //dsReports.Tables["dtCloseReportParamPmnt"].TableName = "dtCloseReportParamPmnt";
                //dsReports.Tables.Add(dtCloseReportParamPmnt);
               
                objrptEOBDailyPay.SetDataSource(dsReports);
             

                crvRptViewPayment.ReportSource = objrptEOBDailyPay;


                //objrptEOBDailyPay.SetParameterValue("userName", _UserName);
              
                //objrptEOBDailyPay.SetParameterValue("reportType", "Daily");
                //objrptEOBDailyPay.SetParameterValue("closeDate", dtPayCloseDate.Value );
                //objrptEOBDailyPay.SetParameterValue("endDate", dtPayCloseDate.Value);

                //objrptEOBDailyPay.SetParameterValue("sortBy", (cmbPaySortBy.Text=="Type"?"Type":(cmbPaySortBy.Text=="Payer"?"Payer":(cmbPaySortBy.Text=="Tray"?"Tray":(cmbPaySortBy.Text=="User"?"User":"Type")))));
                //objrptEOBDailyPay.SetParameterValue("Providers", sbPayProvidersName.ToString());
                //objrptEOBDailyPay.SetParameterValue("Facility", sbPayFacilityName.ToString());
                //objrptEOBDailyPay.SetParameterValue("user", sbPayUserName.ToString());
                //objrptEOBDailyPay.SetParameterValue("tray", sbPayTrayName.ToString());

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Parameters.Clear();
                    _sqlcommand.Dispose();
                    _sqlcommand = null;
                }
                if (oConnection != null && oConnection.State == ConnectionState.Open)
                {
                    oConnection.Close();
                    

                }
                if (oConnection != null)
                {
                    oConnection.Dispose();

                }
            }
        }

        #endregion

        #region "Payment Show Hide Events "

        private void btnPayDown_Click(object sender, EventArgs e)
        {
            try
            {

                flowPnlPayment.Visible = true;
                btnPayDown.BackgroundImage = global::gloReports.Properties.Resources.UP;
                btnPayDown.BackgroundImageLayout = ImageLayout.Center;
                btnPayDown.Visible = false;
                btnPayUP.Visible = true;
                label20.Visible = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }

        }

        private void btnPayUP_Click(object sender, EventArgs e)
        {
            try
            {
                flowPnlPayment.Visible = false;
                btnPayDown.BackgroundImage = global::gloReports.Properties.Resources.Down;
                btnPayDown.BackgroundImageLayout = ImageLayout.Center;
                btnPayUP.Visible = false;
                btnPayDown.Visible = true;
                label20.Visible = true;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }

        }

        private void btnPayUP_MouseHover(object sender, EventArgs e)
        {
            btnPayUP.BackgroundImage = global::gloReports.Properties.Resources.UPHover;
            btnPayUP.BackgroundImageLayout = ImageLayout.Center;


        }

        private void btnPayUP_MouseLeave(object sender, EventArgs e)
        {
            btnPayUP.BackgroundImage = global::gloReports.Properties.Resources.UP;
            btnPayUP.BackgroundImageLayout = ImageLayout.Center;


        }

        private void btnPayDown_MouseHover(object sender, EventArgs e)
        {
            btnPayDown.BackgroundImage = global::gloReports.Properties.Resources.DownHover;
            btnPayDown.BackgroundImageLayout = ImageLayout.Center;

        }

        private void btnPayDown_MouseLeave(object sender, EventArgs e)
        {
            btnPayDown.BackgroundImage = global::gloReports.Properties.Resources.Down;
            btnPayDown.BackgroundImageLayout = ImageLayout.Center;

        }

        #endregion

        #region " Payment User Control Events "

        private void btnBrowsePayProvider_Click(object sender, EventArgs e)
        {
            FillProviders(cmbPaymentProvider);
        }

        private void btnClearPayProvider_Click(object sender, EventArgs e)
        {
            ClearCombo(cmbPaymentProvider);
        }

        private void btnBrowsePayFacility_Click(object sender, EventArgs e)
        {
            FillFacilities(cmbPayFacility);
        }

        private void btnClearPayFacility_Click(object sender, EventArgs e)
        {
            ClearCombo(cmbPayFacility);
        }

        private void btnBrowsePayTray_Click(object sender, EventArgs e)
        {
            FillPaymentTray();
        }

        private void btnClearPayTray_Click(object sender, EventArgs e)
        {
            ClearCombo(cmbPayTray);
        }

        private void btnBrowsePayUser_Click(object sender, EventArgs e)
        {
            FillUsers(cmbPayUser);
        }

        private void btnClearPayUser_Click(object sender, EventArgs e)
        {
            ClearCombo(cmbPayUser);
        }

        private void btnClearPayType_Click(object sender, EventArgs e)
        {
            ClearCombo(cmbPayType);

        }
        private void removeOListControl()
        {
            if (oListControl != null)
            {
                if (this.Controls.Contains(oListControl))
                {
                    this.Controls.Remove(oListControl);
                }
                oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                oListControl.Dispose();
                oListControl = null;
            }
        }
        private void btnBrowsePayType_Click(object sender, EventArgs e)
        {
            removeOListControl();
            //if (oListControl != null)
            //{
            //    for (int i = this.Controls.Count - 1; i >= 0; i--)
            //    {
            //        if (this.Controls[i].Name == oListControl.Name)
            //        {
            //            this.Controls.Remove(this.Controls[i]);
            //            break;
            //        }
            //    }
            //}
            oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.PaymentType, true, this.Width);


            oListControl.ClinicID = _ClinicID;
            oListControl.ControlHeader = " Payment Type";

            _CurrentControlType = gloListControl.gloListControlType.PaymentType;
            oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
            oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
            _ControlType = cmbPayType.Name.ToString();
            this.Controls.Add(oListControl);

            if (cmbPayType.DataSource != null)
            {
                for (int i = 0; i < cmbPayType.Items.Count; i++)
                {
                    cmbPayType.SelectedIndex = i;
                    cmbPayType.Refresh();
                    oListControl.SelectedItems.Add(Convert.ToInt64(cmbPayType.SelectedValue), cmbPayType.Text);
                }
            }
            oListControl.OpenControl();
            oListControl.Dock = DockStyle.Fill;
            oListControl.BringToFront();
        }

        private void cmb_Paydatefilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            //---------Abhisekh 12 june start 
            int _filterby = 0;

            _filterby = cmb_Paydatefilter.SelectedIndex;
            //switch (_filterby)
            //{
            //    case 0:
            //        FilterBy_DateRange(dtpPayStartDate, dtpPayEndDate);
            //        break;

            //    case 1:
            //        FilterBy_Today(dtpPayStartDate, dtpPayEndDate);
            //        break;

            //    case 2:
            //        FilterBy_Tomorrow(dtpPayStartDate, dtpPayEndDate);
            //        break;

            //    case 3:
            //        FilterBy_Yesterday(dtpPayStartDate, dtpPayEndDate);
            //        break;

            //    case 4:
            //        FilterBy_Thisweek(dtpPayStartDate, dtpPayEndDate);
            //        break;

            //    case 5:
            //        FilterBy_lastweek(dtpPayStartDate, dtpPayEndDate);
            //        break;

            //    case 6:
            //        FilterBy_currentmonth(dtpPayStartDate, dtpPayEndDate);
            //        break;

            //    case 7:
            //        FilterBy_lastmonth(dtpPayStartDate, dtpPayEndDate);
            //        break;

            //    case 8:
            //        FilterBy_currenYear(dtpPayStartDate, dtpPayEndDate);
            //        break;

            //    case 9://Last 30 days
            //        FilterBy_last30days(dtpPayStartDate, dtpPayEndDate);
            //        break;

            //    case 10:
            //        FilterBy_last60days(dtpPayStartDate, dtpPayEndDate);
            //        break;

            //    case 11:
            //        FilterBy_last90days(dtpPayStartDate, dtpPayEndDate);
            //        break;

            //    case 12:
            //        FilterBy_last120days(dtpPayStartDate, dtpPayEndDate);
            //        break;

            //}
            
        }

        //private void rbPayPreForClose_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (rbPayPreForClose.Checked == true)
        //        rbPayPreForClose.Font = new Font("Tahoma", 9, FontStyle.Bold);
        //    else
        //        rbPayPreForClose.Font = new Font("Tahoma", 9, FontStyle.Regular);


        //    pnlTransPayDate.Visible = false;
        //    pnlPayDates.Visible = false;
        //    pnlPayCloseDate.Visible = true;
        //}

        //private void rbPayAudit_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (rbPayAudit.Checked == true)
        //        rbPayAudit.Font = new Font("Tahoma", 9, FontStyle.Bold);
        //    else
        //        rbPayAudit.Font = new Font("Tahoma", 9, FontStyle.Regular);


        //    //pnlTransPayDate.Visible = true;
        //    pnlTransPayDate.Visible = false;
        //    pnlPayDates.Visible = true;
        //    pnlPayCloseDate.Visible = false;
        //}
        //---------Abhisekh 12 june end 

        private void chkPayIncludeDetails_CheckedChanged(object sender, EventArgs e)
        {
            FillDailyPayment();
        }

        #endregion

        #endregion

        #region "--------------------------- Day Close -------------------------------------- "

        #region " FillMethods "

        private void FillDayClose()
        {

            //objrptDailyClose = new Rpt_DailyClose();
            dsC1Reports dsReports = new dsC1Reports();
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();
            SqlDataAdapter da = null;

            Int64 _closeDate = 0;

            try
            {


                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                try
                {
                    //lblLstClosedDt.Text = System.DateTime.Now.ToShortDateString();
                    DataTable dtLastCloseDt = null;
                    oDB.Connect(false);
                    oDB.Retrive_Query("select dbo.CONVERT_TO_DATE (max(nCloseDayDate)) as nCloseDayDate from dbo.BL_CloseDays", out dtLastCloseDt);
                    oDB.Disconnect();
                    if (dtLastCloseDt != null && dtLastCloseDt.Rows.Count > 0)
                    {
                        lblLstClosedDt.Text = dtLastCloseDt.Rows[0]["nCloseDayDate"].ToString();
                    }
                    if (dtLastCloseDt != null)
                    {
                        dtLastCloseDt.Dispose();
                        dtLastCloseDt = null;
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                }
                finally
                {
                    if (oDB != null) { oDB.Dispose(); }
                }

                if (trvMonths.Nodes != null)
                {
                    for (int i = 0; i < trvMonths.Nodes.Count; i++)
                    {
                        if (trvMonths.Nodes[i].Checked == true)
                        {
                            _closeDate = gloDateMaster.gloDate.DateAsNumber(trvMonths.Nodes[i].Text.ToString());
                            break;
                        }
                    }

                }

                oConnection.ConnectionString = _databaseconnectionstring;

                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.Connection = oConnection;
                da = new SqlDataAdapter(_sqlcommand);
                _sqlcommand.Connection = oConnection;

                if (_closeDate != 0)
                {
                  

                    //if (trvMonths.Nodes != null)
                    //{
                    //    for (int i = 0; i < trvMonths.Nodes.Count; i++)
                    //    {
                    //        if (trvMonths.Nodes[i].Checked == true)
                    //        {

                    //            _sqlcommand.Parameters.Add("@nCloseDate", System.Data.SqlDbType.BigInt);
                    //            _sqlcommand.Parameters["@nCloseDate"].Value = gloDateMaster.gloDate.DateAsNumber(trvMonths.Nodes[i].Text.ToString());
                    //            break;
                    //        }
                    //    }

                    //}

                    if (_closeDate != 0)
                    {
                        _sqlcommand.Parameters.Add("@nCloseDate", System.Data.SqlDbType.BigInt);
                        _sqlcommand.Parameters["@nCloseDate"].Value = _closeDate;

                    }

                    _sqlcommand.CommandText = "BL_DailyCloseDate";
                    da.Fill(dsReports, "dt_DailyCloseDate");
                    da.Dispose();
                    da = null;
                    _sqlcommand.Parameters.Clear();
                    _sqlcommand.Dispose();
                    _sqlcommand = null;

                    _sqlcommand = new SqlCommand();
                    _sqlcommand.CommandType = CommandType.StoredProcedure;
                    da = new SqlDataAdapter(_sqlcommand);
                    _sqlcommand.Connection = oConnection;

                    //if (trvMonths.Nodes != null)
                    //{
                    //    for (int i = 0; i < trvMonths.Nodes.Count; i++)
                    //    {
                    //        if (trvMonths.Nodes[i].Checked == true)
                    //        {
                    //            _sqlcommand.Parameters.Add("@nCloseDate", System.Data.SqlDbType.BigInt);
                    //            _sqlcommand.Parameters["@nCloseDate"].Value = gloDateMaster.gloDate.DateAsNumber(trvMonths.Nodes[i].Text.ToString());
                    //            break;
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    _sqlcommand.Parameters.Add("@nCloseDate", System.Data.SqlDbType.BigInt);
                    //    _sqlcommand.Parameters["@nCloseDate"].Value = gloDateMaster.gloDate.DateAsNumber(System.DateTime.Now.ToShortDateString());
                    //}
                    if (_closeDate != 0)
                    {
                        _sqlcommand.Parameters.Add("@nCloseDate", System.Data.SqlDbType.BigInt);
                        _sqlcommand.Parameters["@nCloseDate"].Value = _closeDate;

                    }
                    _sqlcommand.CommandText = "BL_DailyCloseSummary";
                    da.Fill(dsReports, "dt_DailyCloseSummary");
                 
                }
                else
                {
                    if (_isFormLoading)
                    {
                    }
                    else
                    {
                        MessageBox.Show("Please select a Date to Close", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    }
                }


                    CrystalDecisions.Shared.ParameterFields paramFields1 = new CrystalDecisions.Shared.ParameterFields();
                    CrystalDecisions.Shared.ParameterDiscreteValue discValue1 = new CrystalDecisions.Shared.ParameterDiscreteValue();
                    CrystalDecisions.Shared.ParameterField paramField1 = new CrystalDecisions.Shared.ParameterField();
                    paramField1.Name = "ShowCloseHeader";
                    paramField1.HasCurrentValue = true;
                    int Value = 0;
                    if (chkClosedShwDetails.Checked)
                        Value = 1;
                    else
                        Value = 0;

                    discValue1.Value = Value;
                    paramField1.CurrentValues.Add(discValue1);
                    paramFields1.Add(paramField1);
                    this.crvRptViewCloseDay.ParameterFieldInfo = paramFields1;

                    da.Dispose();
                    da = null;
                    _sqlcommand.Parameters.Clear();
                    _sqlcommand.Dispose();
                    _sqlcommand = null;
                    //da.Dispose();

                    //DataTable dtCloseReportDaily = new DataTable();
                    //dtCloseReportDaily.Columns.Add("userName");
                    //dtCloseReportDaily.Columns.Add("lastcloseDate");

                  

                  
                    //dtCloseReportDaily.TableName = "dtCloseReportDaily";
                    //dsReports.Tables.Add(dtCloseReportDaily);

                    objrptDailyClose.SetDataSource(dsReports);
                    crvRptViewCloseDay.ReportSource = objrptDailyClose;

                    objrptDailyClose.SetParameterValue("userName", _UserName);
                    objrptDailyClose.SetParameterValue("lastcloseDate", (lblLstClosedDt.Text==""?DateTime.Now.ToShortDateString():(Convert.ToDateTime(lblLstClosedDt.Text)).ToShortDateString()));

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Parameters.Clear();
                    _sqlcommand.Dispose();
                    _sqlcommand = null;
                }
                if (oConnection != null && oConnection.State == ConnectionState.Open)
                {
                    oConnection.Close();
                    

                }
                if (oConnection != null)
                {
                    oConnection.Dispose();
                }
            }
        }

        #endregion


        #region "User Control Events "
        
        private void chkClosedShwDetails_CheckedChanged(object sender, EventArgs e)
        {
            FillDayClose();

        }

        #endregion

        #region "Close Day ShowHide Events"

        private void btnCloseUP_Click(object sender, EventArgs e)
        {
            pnlCloseDaySearch.Visible = false;
            btnCloseDown.BackgroundImage = global::gloReports.Properties.Resources.Down;
            btnCloseDown.BackgroundImageLayout = ImageLayout.Center;
            btnCloseUP.Visible = false;
            btnCloseDown.Visible = true;

        }

        private void btnCloseUP_MouseHover(object sender, EventArgs e)
        {
            btnCloseUP.BackgroundImage = global::gloReports.Properties.Resources.UPHover;
            btnCloseUP.BackgroundImageLayout = ImageLayout.Center;

        }

        private void btnCloseUP_MouseLeave(object sender, EventArgs e)
        {
            btnCloseUP.BackgroundImage = global::gloReports.Properties.Resources.UP;
            btnCloseUP.BackgroundImageLayout = ImageLayout.Center;

        }

        private void btnCloseDown_Click(object sender, EventArgs e)
        {
            pnlCloseDaySearch.Visible = true;
            btnCloseDown.BackgroundImage = global::gloReports.Properties.Resources.UP;
            btnCloseDown.BackgroundImageLayout = ImageLayout.Center;
            btnCloseDown.Visible = false;
            btnCloseUP.Visible = true;

        }

        private void btnCloseDown_MouseHover(object sender, EventArgs e)
        {
            btnCloseDown.BackgroundImage = global::gloReports.Properties.Resources.DownHover;
            btnCloseDown.BackgroundImageLayout = ImageLayout.Center;

        }

        private void btnCloseDown_MouseLeave(object sender, EventArgs e)
        {
            btnCloseDown.BackgroundImage = global::gloReports.Properties.Resources.Down;
            btnCloseDown.BackgroundImageLayout = ImageLayout.Center;
        }

        #endregion

        #endregion


        #region " Date Methods "


        private void Fill_FilterDatesCombo(ComboBox ComboBox)
        {
            try
            {
                ComboBox.Items.Clear();
                ComboBox.Items.Add("Custom");
                ComboBox.Items.Add("Today");
                ComboBox.Items.Add("Tomorrow");
                ComboBox.Items.Add("Yesterday");
                ComboBox.Items.Add("This Week");
                ComboBox.Items.Add("Last Week");
                ComboBox.Items.Add("Current Month");
                ComboBox.Items.Add("Last Month");
                ComboBox.Items.Add("Current Year");
                ComboBox.Items.Add("Last 30 Days");
                ComboBox.Items.Add("Last 60 Days");
                ComboBox.Items.Add("Last 90 Days");
                ComboBox.Items.Add("Last 120 Days");
                ComboBox.Refresh();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }

        private void FilterBy_Today(DateTimePicker dtpStartDate, DateTimePicker dtpEndDate)
        {

            dtpStartDate.Value = DateTime.Today;
            dtpEndDate.Value = DateTime.Today;

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;

        }

        private void FilterBy_Tomorrow(DateTimePicker dtpStartDate, DateTimePicker dtpEndDate)
        {
            dtpStartDate.Value = DateTime.Now.AddDays(1);
            dtpEndDate.Value = DateTime.Now.AddDays(1);

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;
        }

        private void FilterBy_Yesterday(DateTimePicker dtpStartDate, DateTimePicker dtpEndDate)
        {
            dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(24, 0, 0));
            dtpEndDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(24, 0, 0));

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;
        }

        private void FilterBy_Thisweek(DateTimePicker dtpStartDate, DateTimePicker dtpEndDate)
        {

            if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
            {
                dtpStartDate.Value = DateTime.Today;
                dtpEndDate.Value = DateTime.Now.Date.AddDays(6);

            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(1, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);
            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(2, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);
            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Thursday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(3, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);

            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(4, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);
            }

            if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(5, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);
            }

            if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(6, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);
            }

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;

        }

        private void FilterBy_lastweek(DateTimePicker dtpStartDate, DateTimePicker dtpEndDate)
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(7, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);

            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(8, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);
            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(9, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);
            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Thursday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(10, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);

            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(11, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);
            }

            if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(12, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);
            }

            if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(13, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);
            }

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;

        }

        private void FilterBy_currentmonth(DateTimePicker dtpStartDate, DateTimePicker dtpEndDate)
        {
            DateTime dtFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime dtTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtTo = dtTo.AddMonths(1);
            dtTo = dtTo.AddDays(-(dtTo.Day));
            dtpStartDate.Value = Convert.ToDateTime(dtFrom.Date);
            dtpEndDate.Value = Convert.ToDateTime(dtTo.Date);

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;

        }

        private void FilterBy_lastmonth(DateTimePicker dtpStartDate, DateTimePicker dtpEndDate)
        {
            DateTime firstDay = new DateTime(DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month, 1);
            int DaysinMonth = DateTime.DaysInMonth(DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month);
            DateTime lastDay = firstDay.AddMonths(1).AddTicks(-1);
            dtpStartDate.Value = Convert.ToDateTime(firstDay.Date);
            dtpEndDate.Value = Convert.ToDateTime(lastDay.Date);
            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;

        }

        private void FilterBy_currenYear(DateTimePicker dtpStartDate, DateTimePicker dtpEndDate)
        {

            DateTime dtFrom = new DateTime(DateTime.Now.Year, 1, 1);
            dtpStartDate.Value = Convert.ToDateTime(dtFrom.Date);
            dtpEndDate.Value = DateTime.Today;
            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;
        }

        private void FilterBy_last30days(DateTimePicker dtpStartDate, DateTimePicker dtpEndDate)
        {

            dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(30, 0, 0, 0));
            dtpEndDate.Value = DateTime.Today;

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;
        }

        private void FilterBy_last60days(DateTimePicker dtpStartDate, DateTimePicker dtpEndDate)
        {
            dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(60, 0, 0, 0));
            dtpEndDate.Value = DateTime.Today;

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;

        }

        private void FilterBy_last90days(DateTimePicker dtpStartDate, DateTimePicker dtpEndDate)
        {

            dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(90, 0, 0, 0));
            dtpEndDate.Value = DateTime.Today;

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;

        }

        private void FilterBy_last120days(DateTimePicker dtpStartDate, DateTimePicker dtpEndDate)
        {

            dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(120, 0, 0, 0));
            dtpEndDate.Value = DateTime.Today;

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;

        }

        private void FilterBy_DateRange(DateTimePicker dtpStartDate, DateTimePicker dtpEndDate)
        {

            dtpStartDate.Value = DateTime.Today;
            dtpEndDate.Value = DateTime.Today;

            dtpStartDate.Enabled = true;
            dtpEndDate.Enabled = true;

        }


        #endregion


        #region "Fill User Control Methods"

        private void ClearCombo(ComboBox oComboBox)
        {
           // oComboBox.Items.Clear();
            oComboBox.DataSource = null;
            oComboBox.Items.Clear();
            oComboBox.Refresh();
        }

        private void FillProviders(ComboBox oComboBox)
        {
            try
            {
                //if (oListControl != null)
                //{
                //    for (int i = this.Controls.Count - 1; i >= 0; i--)
                //    {
                //        if (this.Controls[i].Name == oListControl.Name)
                //        {
                //            this.Controls.Remove(this.Controls[i]);
                //            break;
                //        }
                //    }
                //}
                removeOListControl();
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Providers, true, this.Width);
                oListControl.ClinicID = _ClinicID;
                oListControl.ControlHeader = " Provider";
                _CurrentControlType = gloListControl.gloListControlType.Providers;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                _ControlType = oComboBox.Name.ToString();

                this.Controls.Add(oListControl);

                if (oComboBox.DataSource != null)
                {
                    for (int i = 0; i < oComboBox.Items.Count; i++)
                    {
                        oComboBox.SelectedIndex = i;
                        oComboBox.Refresh();
                        oListControl.SelectedItems.Add(Convert.ToInt64(oComboBox.SelectedValue), oComboBox.Text);
                    }
                }
                oListControl.OpenControl();
                oListControl.Dock = DockStyle.Fill;
                oListControl.BringToFront();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);

            }
        }

        private void FillFacilities(ComboBox oComboBox)
        {

            try
            {
                //if (oListControl != null)
                //{
                //    for (int i = this.Controls.Count - 1; i >= 0; i--)
                //    {
                //        if (this.Controls[i].Name == oListControl.Name)
                //        {
                //            this.Controls.Remove(this.Controls[i]);
                //            break;
                //        }
                //    }
                //}
                removeOListControl();
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Facility, true, this.Width);

                oListControl.ClinicID = _ClinicID;
                oListControl.ControlHeader = " Facility ";

                _CurrentControlType = gloListControl.gloListControlType.Facility;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                _ControlType = oComboBox.Name.ToString();
                this.Controls.Add(oListControl);

                if (oComboBox.DataSource != null)
                {
                    for (int i = 0; i < oComboBox.Items.Count; i++)
                    {
                        oComboBox.SelectedIndex = i;
                        oComboBox.Refresh();
                        oListControl.SelectedItems.Add(Convert.ToInt64(oComboBox.SelectedValue), oComboBox.Text);
                    }
                }
                oListControl.OpenControl();
                oListControl.Dock = DockStyle.Fill;
                oListControl.BringToFront();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }

        }

        private void FillChargesTray(ComboBox oComboBox)
        {
            //if (oListControl != null)
            //{
            //    for (int i = this.Controls.Count - 1; i >= 0; i--)
            //    {
            //        if (this.Controls[i].Name == oListControl.Name)
            //        {
            //            this.Controls.Remove(this.Controls[i]);
            //            break;
            //        }
            //    }
            //}
            removeOListControl();
            oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.ChargeTray, true, this.Width);

            oListControl.ClinicID = _ClinicID;
            oListControl.ControlHeader = " Charges Tray ";

            //if (chkLoginUsers.Checked)
            //{
            //    oListControl.FilterPaymentTrayByUsers = true;
            //}
            //else
            //{
            //    oListControl.FilterPaymentTrayByUsers = false;
            //}
            oListControl.FilterPaymentTrayByUsers = false;

            _CurrentControlType = gloListControl.gloListControlType.ChargeTray;

            oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
            oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
            _ControlType = oComboBox.Name.ToString();
            this.Controls.Add(oListControl);

            if (oComboBox.DataSource != null)
            {
                for (int i = 0; i < cmbMultiChargesTray.Items.Count; i++)
                {
                    oComboBox.SelectedIndex = i;
                    oComboBox.Refresh();
                    oListControl.SelectedItems.Add(Convert.ToInt64(oComboBox.SelectedValue), oComboBox.Text);
                }
            }
            oListControl.OpenControl();
            //this.Controls.Add(oListControl);
            oListControl.Dock = DockStyle.Fill;
            oListControl.BringToFront();
        }

        private void FillUsers(ComboBox oComboBox)
        {
            //if (oListControl != null)
            //{
            //    for (int i = this.Controls.Count - 1; i >= 0; i--)
            //    {
            //        if (this.Controls[i].Name == oListControl.Name)
            //        {
            //            this.Controls.Remove(this.Controls[i]);
            //            break;
            //        }
            //    }
            //}
            removeOListControl();
            oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Users, true, this.Width);


            oListControl.ClinicID = _ClinicID;
            oListControl.ControlHeader = " User ";

            _CurrentControlType = gloListControl.gloListControlType.Users;
            oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
            oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
            _ControlType = oComboBox.Name.ToString();
            this.Controls.Add(oListControl);

            if (oComboBox.DataSource != null)
            {
                for (int i = 0; i < oComboBox.Items.Count; i++)
                {
                    oComboBox.SelectedIndex = i;
                    oComboBox.Refresh();
                    oListControl.SelectedItems.Add(Convert.ToInt64(oComboBox.SelectedValue), oComboBox.Text);
                }
            }
            oListControl.OpenControl();
            oListControl.Dock = DockStyle.Fill;
            oListControl.BringToFront();
        }

        private void FillPaymentTray()
        {
            //if (oListControl != null)
            //{
            //    for (int i = this.Controls.Count - 1; i >= 0; i--)
            //    {
            //        if (this.Controls[i].Name == oListControl.Name)
            //        {
            //            this.Controls.Remove(this.Controls[i]);
            //            break;
            //        }
            //    }
            //}
            removeOListControl();
            oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.PaymentTray, true, this.Width);
            oListControl.FilterPaymentTrayByUsers = false;

            oListControl.ClinicID = _ClinicID;
            oListControl.ControlHeader = " Payment Tray";

            oListControl.FilterPaymentTrayByUsers = false;

            _CurrentControlType = gloListControl.gloListControlType.PaymentTray;
            oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
            oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
            _ControlType = cmbPayTray.Name.ToString();
            this.Controls.Add(oListControl);

            //oListControl.SelectedItems.Count;
            if (cmbPayTray.DataSource != null)
            {
                for (int i = 0; i < cmbPayTray.Items.Count; i++)
                {
                    cmbPayTray.SelectedIndex = i;
                    cmbPayTray.Refresh();
                    oListControl.SelectedItems.Add(Convert.ToInt64(cmbPayTray.SelectedValue), cmbPayTray.Text);
                }
            }
            oListControl.OpenControl();
            //this.Controls.Add(oListControl);
            oListControl.Dock = DockStyle.Fill;
            oListControl.BringToFront();

        }

        private void FillDailyCloseDates()
        {
            try
            {
                String strQuery = "";
                DataTable dtCloseDates = null;

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                strQuery = "Select  dbo.CONVERT_TO_DATE(nTransactionDate) as CloseDates from " +
                                       "( " +
                                             " select distinct nTransactionDate from dbo.BL_Transaction_MST Union select distinct CASE WHEN nVoidType=7 THEN nPaymentVoidCloseDate ELSE  nCloseDate END AS nclosedate from dbo.BL_EOBPayment_MST  Union select distinct nVoidCloseDate from dbo.BL_EOBPaymentVoid_Notes " +
                                       ") as Final Where nTransactionDate Not In (select distinct ncloseDayDate from BL_CloseDays) and nTransactionDate <> 0 order by nTransactionDate asc";

                oDB.Connect(false);
                oDB.Retrive_Query(strQuery, out dtCloseDates);
                oDB.Disconnect();

                trvMonths.Nodes.Clear();

                if (dtCloseDates != null && dtCloseDates.Rows.Count > 0)
                {
                    for (int i = 0; i <= dtCloseDates.Rows.Count - 1; i++)
                    {
                        DataRow dr = dtCloseDates.Rows[i];
                        TreeNode oNode = new TreeNode();
                        oNode.Tag = Convert.ToString(dr[0]);
                        oNode.Text = Convert.ToString(dr[0]); ;
                        trvMonths.Nodes.Add(oNode);
                        if (i == 0)
                        {
                            //trvMonths.Nodes[i].Checked = true;
                        }
                        oNode = null;
                    }

                }
                if (dtCloseDates != null)
                {
                    dtCloseDates.Dispose();
                }
                oDB.Dispose();
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                Ex = null; 
            }
            finally
            {
               
            }

        }

        private void SelectCloseDates()
        {
            #region "CLosDatesSelection"
            try
            {
                DateTime dtClosedayDate = DateTime.Now;
                string _date = getCloseDate();
                if (_date.ToString() != "")
                {
                    dtClosedayDate = DateTime.Parse(_date);
                }

                dtpCloseDate.Text = getCloseDate();

                string _sCloseDate = getCloseDate();

                dtpCloseDate.Value = dtClosedayDate;
                dtPayCloseDate.Value = dtClosedayDate;
               

                //dtpChrgStartDate.Text = Convert.ToString(dtClosedayDate.AddMonths(-1));
                //dtpChrgEndDate.Text = getCloseDate();
                //dtpChrgStartDate.Enabled = true;
                //dtpChrgEndDate.Enabled = true;

                //dtpPayStartDate.Text = Convert.ToString(dtClosedayDate.AddMonths(-1));
                //dtpPayEndDate.Text = getCloseDate();
                //dtpPayStartDate.Enabled = true;
                //dtpPayEndDate.Enabled = true;
            }
            catch (FormatException FormatEx)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(FormatEx.ToString(), true);
            }

            #endregion "CLosDatesSelection"
        }

        private void FillChargesSortByCombo()
        {
            DataTable dtChrgSortBy = new DataTable();
            dtChrgSortBy.Columns.Add("Id");
            dtChrgSortBy.Columns.Add("SortBy");
            DataRow dr = dtChrgSortBy.NewRow();

            dr[0] = "Claim";
            dr[1] = "Claim #";
            dtChrgSortBy.Rows.Add(dr);
            dtChrgSortBy.AcceptChanges();

            dr = null;
            dr = dtChrgSortBy.NewRow();
            dr[0] = "PatientName";
            dr[1] = "Patient Name";
            dtChrgSortBy.Rows.Add(dr);
            dtChrgSortBy.AcceptChanges();


            dr = null;
            dr = dtChrgSortBy.NewRow();
            dr[0] = "BillingProvider";
            dr[1] = "Provider";
            dtChrgSortBy.Rows.Add(dr);
            dtChrgSortBy.AcceptChanges();


            dr = null;
            dr = dtChrgSortBy.NewRow();
            dr[0] = "sFacilityDescription";
            dr[1] = "Facility";
            dtChrgSortBy.Rows.Add(dr);
            dtChrgSortBy.AcceptChanges();


            dr = null;
            dr = dtChrgSortBy.NewRow();
            dr[0] = "sUserName";
            dr[1] = "User";
            dtChrgSortBy.Rows.Add(dr);
            dtChrgSortBy.AcceptChanges();

            dr = null;
            dr = dtChrgSortBy.NewRow();
            dr[0] = "sChargesTrayDescription";
            dr[1] = "Tray";
            dtChrgSortBy.Rows.Add(dr);
            dtChrgSortBy.AcceptChanges();

            if (dtChrgSortBy != null)
            {
                cmbChrgSortBy.DataSource = dtChrgSortBy.DefaultView;
                cmbChrgSortBy.DisplayMember = "SortBy";
                cmbChrgSortBy.ValueMember = "ID";
                
            }

        }


        private void FillPaymentSortByCombo()
        {
            DataTable dtPaySortBy = new DataTable();
            dtPaySortBy.Columns.Add("Id");
            dtPaySortBy.Columns.Add("SortBy");
            DataRow dr = dtPaySortBy.NewRow();

            dr[0] = "PaymentModeText";
            dr[1] = "Type";
            dtPaySortBy.Rows.Add(dr);
            dtPaySortBy.AcceptChanges();

            dr = null;
            dr = dtPaySortBy.NewRow();
            dr[0] = "PatientName";
            dr[1] = "Payer";
            dtPaySortBy.Rows.Add(dr);
            dtPaySortBy.AcceptChanges();

            dr = null;
            dr = dtPaySortBy.NewRow();
            dr[0] = "CloseDayTray";
            dr[1] = "Tray";
            dtPaySortBy.Rows.Add(dr);
            dtPaySortBy.AcceptChanges();


            dr = null;
            dr = dtPaySortBy.NewRow();
            dr[0] = "UserName";
            dr[1] = "User";
            dtPaySortBy.Rows.Add(dr);
            dtPaySortBy.AcceptChanges();

                              

            if (dtPaySortBy != null)
            {
                cmbPaySortBy.DataSource = dtPaySortBy.DefaultView;
                cmbPaySortBy.DisplayMember = "SortBy";
                cmbPaySortBy.ValueMember = "ID";
                //cmbPaySortBy.SelectedItem = dtPaySortBy.Rows[0][1].ToString() ;
                cmbPaySortBy.SelectedIndex = 0;
            }

        }


        #endregion


        #region " List Control Events "

        void oListControl_ItemSelectedClick(object sender, EventArgs e)
        {
            int _Counter = 0;
            ComboBox oComboBox = null;

            if (_ControlType == "cmbChargesProvider")
                oComboBox = cmbChargesProvider;
            else if (_ControlType == "cmbPaymentProvider")
                oComboBox = cmbPaymentProvider;
            else if (_ControlType == "cmbChargesMultiFacility")
                oComboBox = cmbChargesMultiFacility;
            else if (_ControlType == "cmbPayFacility")
                oComboBox = cmbPayFacility;
            else if (_ControlType == "cmbMultiChargesTray")
                oComboBox = cmbMultiChargesTray;
            else if (_ControlType == "cmbPayTray")
                oComboBox = cmbPayTray;
            else if (_ControlType == "cmbChargesUser")
                oComboBox = cmbChargesUser;
            else if (_ControlType == "cmbPayUser")
                oComboBox = cmbPayUser;




            switch (_CurrentControlType)
            {

                case gloListControl.gloListControlType.Providers:
                    {
                       
                        oComboBox.DataSource = null;
                        oComboBox.Items.Clear();
                        if (oListControl.SelectedItems.Count > 0)
                        {
                            DataTable oBindTable = new DataTable();

                            oBindTable.Columns.Add("ID");
                            oBindTable.Columns.Add("DispName");

                            for (_Counter = 0; _Counter <= oListControl.SelectedItems.Count - 1; _Counter++)
                            {
                                DataRow oRow;
                                oRow = oBindTable.NewRow();
                                oRow[0] = oListControl.SelectedItems[_Counter].ID;
                                oRow[1] = oListControl.SelectedItems[_Counter].Description;
                                oBindTable.Rows.Add(oRow);
                            }

                            oComboBox.DataSource = oBindTable;
                            oComboBox.DisplayMember = "DispName";
                            oComboBox.ValueMember = "ID";
                        }

                    }
                    break;


                case gloListControl.gloListControlType.Facility:
                    {
                        
                        oComboBox.DataSource = null;
                        oComboBox.Items.Clear();
                        if (oListControl.SelectedItems.Count > 0)
                        {
                            DataTable oBindTable = new DataTable();

                            oBindTable.Columns.Add("ID");
                            oBindTable.Columns.Add("DispName");

                            for (_Counter = 0; _Counter <= oListControl.SelectedItems.Count - 1; _Counter++)
                            {
                                DataRow oRow;
                                oRow = oBindTable.NewRow();
                                oRow[0] = oListControl.SelectedItems[_Counter].ID;
                                oRow[1] = oListControl.SelectedItems[_Counter].Description;
                                oBindTable.Rows.Add(oRow);
                            }

                            oComboBox.DataSource = oBindTable;
                            oComboBox.DisplayMember = "DispName";
                            oComboBox.ValueMember = "ID";
                        }

                    }
                    break;

                case gloListControl.gloListControlType.ChargeTray:
                    {
                       
                        oComboBox.DataSource = null;
                        oComboBox.Items.Clear();
                        if (oListControl.SelectedItems.Count > 0)
                        {
                            DataTable oBindTable = new DataTable();

                            oBindTable.Columns.Add("ID");
                            oBindTable.Columns.Add("DispName");

                            for (_Counter = 0; _Counter <= oListControl.SelectedItems.Count - 1; _Counter++)
                            {
                                DataRow oRow;
                                oRow = oBindTable.NewRow();
                                oRow[0] = oListControl.SelectedItems[_Counter].ID;
                                oRow[1] = oListControl.SelectedItems[_Counter].Description;
                                oBindTable.Rows.Add(oRow);
                            }

                            oComboBox.DataSource = oBindTable;
                            oComboBox.DisplayMember = "DispName";
                            oComboBox.ValueMember = "ID";
                        }

                    }
                    break;


                case gloListControl.gloListControlType.PaymentTray:
                    {
                      
                        oComboBox.DataSource = null;
                        oComboBox.Items.Clear();
                        if (oListControl.SelectedItems.Count > 0)
                        {
                            DataTable oBindTable = new DataTable();

                            oBindTable.Columns.Add("ID");
                            oBindTable.Columns.Add("DispName");

                            for (_Counter = 0; _Counter <= oListControl.SelectedItems.Count - 1; _Counter++)
                            {
                                DataRow oRow;
                                oRow = oBindTable.NewRow();
                                oRow[0] = oListControl.SelectedItems[_Counter].ID;
                                oRow[1] = oListControl.SelectedItems[_Counter].Description;
                                oBindTable.Rows.Add(oRow);
                            }

                            oComboBox.DataSource = oBindTable;
                            oComboBox.DisplayMember = "DispName";
                            oComboBox.ValueMember = "ID";
                        }

                    }
                    break;


                case gloListControl.gloListControlType.Users:
                    {
                      
                        oComboBox.DataSource = null;
                        oComboBox.Items.Clear();
                        if (oListControl.SelectedItems.Count > 0)
                        {
                            DataTable oBindTable = new DataTable();

                            oBindTable.Columns.Add("ID");
                            oBindTable.Columns.Add("DispName");

                            for (_Counter = 0; _Counter <= oListControl.SelectedItems.Count - 1; _Counter++)
                            {
                                DataRow oRow;
                                oRow = oBindTable.NewRow();
                                oRow[0] = oListControl.SelectedItems[_Counter].ID;
                                oRow[1] = oListControl.SelectedItems[_Counter].Description;
                                oBindTable.Rows.Add(oRow);
                            }

                            oComboBox.DataSource = oBindTable;
                            oComboBox.DisplayMember = "DispName";
                            oComboBox.ValueMember = "ID";
                        }

                    }
                    break;

                case gloListControl.gloListControlType.PaymentType:
                    {
                       
                        cmbPayType.DataSource = null;
                        cmbPayType.Items.Clear();
                        if (oListControl.SelectedItems.Count > 0)
                        {
                            DataTable oBindTable = new DataTable();

                            oBindTable.Columns.Add("ID");
                            oBindTable.Columns.Add("DispName");

                            for (_Counter = 0; _Counter <= oListControl.SelectedItems.Count - 1; _Counter++)
                            {
                                DataRow oRow;
                                oRow = oBindTable.NewRow();
                                oRow[0] = oListControl.SelectedItems[_Counter].ID;
                                oRow[1] = oListControl.SelectedItems[_Counter].Code;
                                oBindTable.Rows.Add(oRow);
                            }

                            cmbPayType.DataSource = oBindTable;
                            cmbPayType.DisplayMember = "DispName";
                            cmbPayType.ValueMember = "ID";
                        }

                    }
                    break;

                default:
                    {
                    }
                    break;
            }
        }

        void oListControl_ItemClosedClick(object sender, EventArgs e)
        {
            //if (oListControl != null)
            //{
            //    for (int i = this.Controls.Count - 1; i >= 0; i--)
            //    {
            //        if (this.Controls[i].Name == oListControl.Name)
            //        {
            //            this.Controls.Remove(this.Controls[i]);
            //            break;
            //        }
            //    }
            //}
            removeOListControl();
        }

        #endregion


        #region "Seal Charges Events "

        private void btnTrayDown_Click(object sender, EventArgs e)
        {
            pnlSummaryContainer.Visible = false;
            btnTrayUp.BackgroundImage = global::gloReports.Properties.Resources.UP;
            btnTrayUp.BackgroundImageLayout = ImageLayout.Center;
            btnTrayDown.Visible = false;
            btnTrayUp.Visible = true;
        }

        private void btnTrayUp_Click(object sender, EventArgs e)
        {
            pnlSummaryContainer.Visible = true;
            btnTrayDown.BackgroundImage = global::gloReports.Properties.Resources.Down;
            btnTrayDown.BackgroundImageLayout = ImageLayout.Center;
            btnTrayUp.Visible = false;
            btnTrayDown.Visible = true;


        }

        private void btnTrayDown_MouseHover(object sender, EventArgs e)
        {
            btnTrayDown.BackgroundImage = global::gloReports.Properties.Resources.DownHover;
            btnTrayDown.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnTrayUp_MouseHover(object sender, EventArgs e)
        {
            btnTrayUp.BackgroundImage = global::gloReports.Properties.Resources.UPHover;
            btnTrayUp.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnTrayDown_MouseLeave(object sender, EventArgs e)
        {
            btnTrayDown.BackgroundImage = global::gloReports.Properties.Resources.Down;
            btnTrayDown.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnTrayUp_MouseLeave(object sender, EventArgs e)
        {
            btnTrayUp.BackgroundImage = global::gloReports.Properties.Resources.UP;
            btnTrayUp.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnCloseCharges_Click(object sender, EventArgs e)
        {
         
            
        }


        #endregion

        private void tsb_Print_Click(object sender, EventArgs e)
        {
            if (tabSummary.SelectedTab.Tag.ToString() == "DailyCharges")
            {
                crvRptViewCharges.PrintReport();
            }
            else if (tabSummary.SelectedTab.Tag.ToString() == "DailyPayment")
            {
                crvRptViewPayment.PrintReport();
            }
            else if (tabSummary.SelectedTab.Tag.ToString() == "DailyClose")
            {
                crvRptViewCloseDay.PrintReport();
            }
        }

        private void btnChrgSelectLoginUsrs_Click(object sender, EventArgs e)
        {

            #region "Charges Tray For Users"

            //gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //oDB.Connect(false);
            //DataTable _dt = null;
            //try
            //{
            //    string _strQuery="";
            //    _strQuery = " SELECT nChargeTrayID,sCode,sDescription from BL_ChargesTray Where nClinicID = " + _ClinicID + "  And nUserID = " + _nUserID + "";
            //    oDB.Retrive_Query(_strQuery, out _dt);

            //    if (_dt != null && _dt.Rows.Count > 0)
            //    {
            //        cmbMultiChargesTray.DataSource = _dt;
            //        cmbMultiChargesTray.DisplayMember = "sDescription";
            //        cmbMultiChargesTray.ValueMember = "nChargeTrayID";
            //    }
            //    BindUserName(cmbChargesUser);
            //}
            //catch (Exception Ex)
            //{
            //    gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
            //}
            //finally
            //{
            //    oDB.Disconnect();
            //    oDB.Dispose();
            //}

            #endregion

            BindUserName(cmbChargesUser);
            
        }

        private void btnPaySelectLoginUsrs_Click(object sender, EventArgs e)
        {
            #region "Payment Tray For Users"

            //gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //oDB.Connect(false);
            //DataTable _dt = null;
            //try
            //{
            //    string _strQuery = "";
            //    _strQuery = " SELECT nCloseDayTrayID,sCode,sDescription from BL_CloseDayTray Where nClinicID = " + _ClinicID + "  And nUserID = " + _nUserID + "";
            //    oDB.Retrive_Query(_strQuery, out _dt);
            //    if (_dt != null && _dt.Rows.Count > 0)
            //    {
            //        cmbPayTray.DataSource = _dt;
            //        cmbPayTray.DisplayMember = "sDescription";
            //        cmbPayTray.ValueMember = "nCloseDayTrayID";
            //    }

            //    BindUserName(cmbPayUser);
              
            //}
            //catch (Exception Ex)
            //{
            //    gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
            //}
            //finally
            //{
            //    oDB.Disconnect();
            //    oDB.Dispose();
            //}

            #endregion

            BindUserName(cmbPayUser);
        }

        private void BindUserName(ComboBox oComboBox)
        {
            try
            {
                DataTable oBindTable = new DataTable();

                oBindTable.Columns.Add("ID");
                oBindTable.Columns.Add("DispName");
                DataRow oRow;
                oRow = oBindTable.NewRow();
                oRow[0] = _UserId;
                oRow[1] = _UserName.ToString();
                oBindTable.Rows.Add(oRow);

                oComboBox.DataSource = oBindTable;
                oComboBox.DisplayMember = "DispName";
                oComboBox.ValueMember = "ID";
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private string getCloseDate()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                DateTime dtClosedayDate = DateTime.Now;

                oDB.Connect(false);
                //object _Result = oDB.ExecuteScalar_Query("select dbo.Convert_to_date(max(nCloseDayDate)) As CloseDate from BL_CloseDays");
                object _Result = oDB.ExecuteScalar_Query("select max(nCloseDayDate) As CloseDate from BL_CloseDays");
                if (_Result.ToString() != "")
                {
                    dtClosedayDate = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(_Result));
                    dtClosedayDate = dtClosedayDate.AddDays(1);
                    string _date = dtClosedayDate.ToShortDateString();
                    if (_date.ToString() != "")
                    {
                        return _date.ToString();
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return "";
                }

            }
            catch (Exception e)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(e.ToString(), false);
                e = null;
                return "Error in Returning Date.";
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oDB = null;
            }
        }

        private void frmRpt_DailyChargesPaySummary_FormClosed(object sender, FormClosedEventArgs e)
        {

            if (objrptEOBDailyPay != null)
            {
                if (objrptEOBDailyPay.IsLoaded)
                {
                    objrptEOBDailyPay.Close();
                }
                objrptEOBDailyPay.Dispose();
            }

            if (objrptChargesSummary != null)
            {
                if (objrptChargesSummary.IsLoaded)
                {
                    objrptChargesSummary.Close();
                }
                objrptChargesSummary.Dispose();
            }

            if (objrptDailyClose != null)
            {
                if (objrptDailyClose.IsLoaded)
                {
                    objrptDailyClose.Close();
                }
                objrptDailyClose.Dispose();
            }
        }

        private void rbchrgPreForClose_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbChrgAudit_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbPayPreForClose_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbPayAudit_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}