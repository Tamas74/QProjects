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
    public partial class frmRpt_ChargePaymentSummary : Form
    {

        #region " Variable Declarations "

        gloListControl.gloListControl oListControl = null;
        gloListControl.gloListControlType _CurrentControlType = gloListControl.gloListControlType.Other;
      //  private gloGeneralItem.gloItems ogloItems = null;
        private string _databaseconnectionstring = "";
        private string _MessageBoxCaption = string.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;
        private StringBuilder sbProviders = new StringBuilder();

        private String _ControlType = null;

        Rpt_ChargesSummary objrptChargesSummary;
        Rpt_DailyPayment objrptDailyPayment;
        Rpt_DailyClose objrptDailyClose;

        private StringBuilder sbChrgProviders = new StringBuilder();

        #endregion


        #region  " Property Procedures "

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        #endregion  " Property Procedures "


        #region " Constructors "

        public frmRpt_ChargePaymentSummary(string databaseconnectionstring)
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
        }

        #endregion


        #region " Form Events "

        private void frmRpt_ChargePaymentSummary_Load(object sender, EventArgs e)
        {

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

            FillChargesTray();

            Fill_FilterDatesCombo(cmb_Chargesdatefilter);
            Fill_FilterDatesCombo(cmb_Paydatefilter);

            lblLstClosedDt.Text = System.DateTime.Now.ToShortDateString();

            lblUserNm.Text = "admin";
            FillDailyPayment();

            FillDayClose();

            crvRptViewCharges.DisplayToolbar = true;
            crvRptViewPayment.DisplayToolbar = true;
            crvRptViewCloseDay.DisplayToolbar = true;
        }


        #endregion



        #region " ------------------------- Charges ---------------------------------------- "

        #region " Fill Methods "

        private void FillChargesTray()
        {
            if (objrptChargesSummary != null)
            {
                objrptChargesSummary.Dispose();
                objrptChargesSummary = null;
            }
            objrptChargesSummary = new Rpt_ChargesSummary();
            dsC1Reports dsReports = new dsC1Reports();
            SqlCommand _sqlcommand = null;
            SqlConnection oConnection = new SqlConnection();
            SqlDataAdapter da = null;

            try
            {
                

                oConnection.ConnectionString = _databaseconnectionstring;

                _sqlcommand = new SqlCommand();
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                 da = new SqlDataAdapter(_sqlcommand);
                _sqlcommand.Connection = oConnection;
                if (sbChrgProviders.ToString() != "" && sbChrgProviders.Length > 0)
                {
                    _sqlcommand.Parameters.Add("@sProviderID", System.Data.SqlDbType.VarChar);
                    _sqlcommand.Parameters["@sProviderID"].Value = sbChrgProviders.ToString();

                }
                _sqlcommand.CommandText = "Rpt_ChargesTray";
                da.Fill(dsReports, "dt_ChargesTray");
                da.Dispose();
                da = null;
                _sqlcommand.Parameters.Clear();
                _sqlcommand.Dispose();
                _sqlcommand = null;
                _sqlcommand = new SqlCommand();
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                da = new SqlDataAdapter(_sqlcommand);
                _sqlcommand.Connection = oConnection;
                if (sbChrgProviders.ToString() != "" && sbChrgProviders.Length > 0)
                {
                    _sqlcommand.Parameters.Add("@sProviderID", System.Data.SqlDbType.VarChar);
                    _sqlcommand.Parameters["@sProviderID"].Value = sbChrgProviders.ToString();

                }
                _sqlcommand.CommandText = "Rpt_DailyChargesSummary";
                da.Fill(dsReports, "dt_DailyChargesSummary");
                da.Dispose();
                da = null;
                _sqlcommand.Parameters.Clear();
                _sqlcommand.Dispose();
                _sqlcommand = null;
                objrptChargesSummary.SetDataSource(dsReports);
                crvRptViewCharges.ReportSource = objrptChargesSummary;

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

            }
            catch //(Exception ex)
            {
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

            }
            catch //(Exception ex)
            {

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
            int _filterby = 0;

            _filterby = cmb_Chargesdatefilter.SelectedIndex;
            switch (_filterby)
            {
                case 0:
                    FilterBy_DateRange(dtpChrgStartDate, dtpChrgEndDate);
                    break;

                case 1:
                    FilterBy_Today(dtpChrgStartDate, dtpChrgEndDate);
                    break;

                case 2:
                    FilterBy_Tomorrow(dtpChrgStartDate, dtpChrgEndDate);
                    break;

                case 3:
                    FilterBy_Yesterday(dtpChrgStartDate, dtpChrgEndDate);
                    break;

                case 4:
                    FilterBy_Thisweek(dtpChrgStartDate, dtpChrgEndDate);
                    break;

                case 5:
                    FilterBy_lastweek(dtpChrgStartDate, dtpChrgEndDate);
                    break;

                case 6:
                    FilterBy_currentmonth(dtpChrgStartDate, dtpChrgEndDate);
                    break;

                case 7:
                    FilterBy_lastmonth(dtpChrgStartDate, dtpChrgEndDate);
                    break;

                case 8:
                    FilterBy_currenYear(dtpChrgStartDate, dtpChrgEndDate);
                    break;

                case 9://Last 30 days
                    FilterBy_last30days(dtpChrgStartDate, dtpChrgEndDate);
                    break;

                case 10:
                    FilterBy_last60days(dtpChrgStartDate, dtpChrgEndDate);
                    break;

                case 11:
                    FilterBy_last90days(dtpChrgStartDate, dtpChrgEndDate);
                    break;

                case 12:
                    FilterBy_last120days(dtpChrgStartDate, dtpChrgEndDate);
                    break;
            }
        }

        private void rbchrgPreForClose_CheckedChanged(object sender, EventArgs e)
        {
            pnlTransDate.Visible = false;
            pnlChrgDates.Visible = false;
            pnlCloseDate.Visible = true;
        }

        private void rbChrgAudit_CheckedChanged(object sender, EventArgs e)
        {
            pnlTransDate.Visible = true;
            pnlChrgDates.Visible = true;
            pnlCloseDate.Visible = false;

        }

        #endregion


        #endregion


        #region " ------------------------- Payment ----------------------------------------- "

        #region " Fill Methods "

        
        private void FillDailyPayment()
        {
            if (objrptDailyPayment != null)
            {
                objrptDailyPayment.Dispose();
                objrptDailyPayment = null;
            }
            objrptDailyPayment = new Rpt_DailyPayment();
            dsC1Reports dsReports = new dsC1Reports();
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();

            try
            {

                oConnection.ConnectionString = _databaseconnectionstring;
                _sqlcommand.CommandType = CommandType.StoredProcedure;


                _sqlcommand.CommandText = "BL_SELECT_DailyPayment";
                _sqlcommand.Connection = oConnection;


                SqlDataAdapter da = new SqlDataAdapter(_sqlcommand);
                da.Fill(dsReports, "dt_DailyPayment");

                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = "BL_DailyPaymentSummary";
                da.Fill(dsReports, "dt_DailyPaySummary1");

                //objrptDailyPayment.Section2.ReportObjects["Page Header"].ObjectFormat.EnableSuppress = true;
                //objrptDailyPayment.Section2.SectionFormat.EnableSuppress = false; 

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
                
                
                da.Dispose();

                objrptDailyPayment.SetDataSource(dsReports);

                crvRptViewPayment.ReportSource = objrptDailyPayment;


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

            }
            catch //(Exception ex)
            {
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

            }
            catch //(Exception ex)
            {

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
                if ( this.Controls.Contains(oListControl))
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
            int _filterby = 0;

            _filterby = cmb_Chargesdatefilter.SelectedIndex;
            switch (_filterby)
            {
                case 0:
                    FilterBy_DateRange(dtpPayStartDate, dtpPayEndDate);
                    break;

                case 1:
                    FilterBy_Today(dtpPayStartDate, dtpPayEndDate);
                    break;

                case 2:
                    FilterBy_Tomorrow(dtpPayStartDate, dtpPayEndDate);
                    break;

                case 3:
                    FilterBy_Yesterday(dtpPayStartDate, dtpPayEndDate);
                    break;

                case 4:
                    FilterBy_Thisweek(dtpPayStartDate, dtpPayEndDate);
                    break;

                case 5:
                    FilterBy_lastweek(dtpPayStartDate, dtpPayEndDate);
                    break;

                case 6:
                    FilterBy_currentmonth(dtpPayStartDate, dtpPayEndDate);
                    break;

                case 7:
                    FilterBy_lastmonth(dtpPayStartDate, dtpPayEndDate);
                    break;

                case 8:
                    FilterBy_currenYear(dtpPayStartDate, dtpPayEndDate);
                    break;

                case 9://Last 30 days
                    FilterBy_last30days(dtpPayStartDate, dtpPayEndDate);
                    break;

                case 10:
                    FilterBy_last60days(dtpPayStartDate, dtpPayEndDate);
                    break;

                case 11:
                    FilterBy_last90days(dtpPayStartDate, dtpPayEndDate);
                    break;

                case 12:
                    FilterBy_last120days(dtpPayStartDate, dtpPayEndDate);
                    break;

            }
        }


        private void rbPayPreForClose_CheckedChanged(object sender, EventArgs e)
        {
            pnlTransPayDate.Visible = false;
            pnlPayDates.Visible = false;
            pnlPayCloseDate.Visible = true;
        }

        private void rbPayAudit_CheckedChanged(object sender, EventArgs e)
        {
            pnlTransPayDate.Visible = true;
            pnlPayDates.Visible = true;
            pnlPayCloseDate.Visible = false;
        }


      
        #endregion

        private void chkPayIncludeDetails_CheckedChanged(object sender, EventArgs e)
        {
            FillDailyPayment();
        }


        #endregion


        #region "--------------------------- Day Close -------------------------------------- "

        #region " FillMethods "

        private void FillDayClose()
        {
            if (objrptDailyClose != null)
            {
                objrptDailyClose.Dispose();
                objrptDailyClose = null;
            }

            objrptDailyClose = new Rpt_DailyClose();
            dsC1Reports dsReports = new dsC1Reports();
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();

            try
            {
                oConnection.ConnectionString = _databaseconnectionstring;
                _sqlcommand.CommandType = CommandType.StoredProcedure;

                _sqlcommand.CommandText = "BL_DailyCloseDate";
                _sqlcommand.Connection = oConnection;
                SqlDataAdapter da = new SqlDataAdapter(_sqlcommand);
                da.Fill(dsReports, "dt_DailyCloseDate");

                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = "BL_DailyCloseSummary";
                da.Fill(dsReports, "dt_DailyCloseSummary");


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
                objrptDailyClose.SetDataSource(dsReports);
                crvRptViewCloseDay.ReportSource = objrptDailyClose;


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


        private void chkClosedShwDetails_CheckedChanged(object sender, EventArgs e)
        {
            FillDayClose();

        }


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



        #region "Common Methods "
        
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
            catch //(Exception ex)
            {


            }
        }

        private void FillFacilities(ComboBox oComboBox)
        {

            try
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
            catch //(Exception ex)
            {
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

            _CurrentControlType = gloListControl.gloListControlType.ChargeTray;
            oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
            oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
            _ControlType = oComboBox.Name.ToString();
            this.Controls.Add(oListControl);

            if (oComboBox.DataSource != null)
            {
                for (int i = 0; i < cmbChargesMultiFacility.Items.Count; i++)
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
            oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.PaymentTray, true, this.Width);


            oListControl.ClinicID = _ClinicID;
            oListControl.ControlHeader = " Payment Tray";

            _CurrentControlType = gloListControl.gloListControlType.PaymentTray;
            oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
            oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
            _ControlType = cmbPayTray.Name.ToString();
            this.Controls.Add(oListControl);

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
            oListControl.Dock = DockStyle.Fill;
            oListControl.BringToFront();

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
            DialogResult _dltRst = DialogResult.None;
            _dltRst = MessageBox.Show("The Charges displayed in the Report will be Closed", "Save Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);



        }


        #endregion


        #region " Tool Strip Events"

        private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tabSummary_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Text = tabSummary.SelectedTab.Text;
            crvRptViewCharges.DisplayToolbar = true;
            crvRptViewPayment.DisplayToolbar = true;
            crvRptViewCloseDay.DisplayToolbar = true;
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

   
        #endregion

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
                #region "Providers"

                sbChrgProviders.Remove(0, sbChrgProviders.Length);
               
                for (int i = 0; i <= cmbChargesProvider.Items.Count-1; i++)
                {
                    cmbChargesProvider.SelectedIndex = i;
                    cmbChargesProvider.Refresh();
                    if (i == cmbChargesProvider.Items.Count  - 1)
                    {
                        sbChrgProviders.Append(cmbChargesProvider.SelectedValue.ToString());
                    }
                    else
                    {
                        sbChrgProviders.Append(cmbChargesProvider.SelectedValue.ToString() + ",");
                    }
                }

              

                #endregion

                FillChargesTray();
            }
            else if (tabSummary.SelectedTab.Tag.ToString() == "DailyPayment")
            {
                FillPaymentTray();
            }
            else if (tabSummary.SelectedTab.Tag.ToString() == "DailyClose")
            {
                FillDayClose();
            }

        }

      
       
    }
}