using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Collections;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using Microsoft.VisualBasic;


namespace gloReports
{
    public partial class frmRpt_FinancialSummary : Form
    {

        #region "Variable Declaration"
        gloListControl.gloListControl oListControl = null;
        private Font fBold = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
        private Font fRegular = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
        private string _databaseconnectionstring = "";
            private string _UserName = "";
        private string _MessageBoxCaption = "";
        gloListControl.gloListControlType _CurrentControlType = gloListControl.gloListControlType.Other;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID;
        private string[] ReportType = new string[5];
      //  private string _reportDate="";
        Rpt_FinancialSummary oRpt_FinancialSummaryProviderFacility;
        Rpt_FinancialSummaryFacility oRpt_FinancialSummaryFacility;
        Rpt_FinancialSummaryFacilityProvider oRpt_FinancialSummaryFacilityProvider;
        Rpt_FinancialSummaryNone oRpt_FinancialSummaryNone;
        Rpt_FinancialSummaryProvider oRpt_FinancialSummaryProvider;
        Rpt_FinancialSummary_withoutDay oRpt_FinancialSummaryProviderFacility_withoutDay;
        Rpt_FinancialSummaryFacility_withoutDay oRpt_FinancialSummaryFacility_withoutDay;
        Rpt_FinancialSummaryFacilityProvider_withoutDay oRpt_FinancialSummaryFacilityProvider_withoutDay;
        Rpt_FinancialSummaryNone_withoutDay oRpt_FinancialSummaryNone_withoutDay;
        Rpt_FinancialSummaryProvider_withoutDay oRpt_FinancialSummaryProvider_withoutDay;
        ReportDocument _rptName = null;



        Rpt_FinancialDetailNone oRpt_FinancialDetailNone;
        Rpt_FinancialDetail_withoutDay oRpt_FinancialDetailNone_withoutDay;
   //     Rpt_FinancialDetail_withoutDay oRpt_FinancialDetailNone_withoutDay1;
        Rpt_FinancialDetailFacility oRpt_FinancialDetailFacility;
        Rpt_FinancialDetailFacility_withoutDay oRpt_FinancialDetailFacility_withoutDay;
        Rpt_FinancialDetailProvider oRpt_FinancialDetailNoneProvider;
        Rpt_FinancialDetailProvider_withoutDay oRpt_FinancialDetailProvider_withoutDay;
        Rpt_FinancialDetailFacilityProvider oRpt_FinancialDetailNoneFacilityProvider;
        Rpt_FinancialDetailFacilityProvider_withoutDay oRpt_FinancialDetailFacilityProvider_withoutDay;
        Rpt_FinancialDetailProviderFacility oRpt_FinancialDetailNoneProviderFacility;
        Rpt_FinancialDetailProviderFacility_withoutDay oRpt_FinancialDetailProviderFacility_withoutDay;
        enum breakByType
        {
            None = 0,
            Facility = 1,
            Provider = 2,
            FacilityProvider = 3,
            ProviderFacility = 4
        }
     

        #endregion

        #region "Constructor"
        public frmRpt_FinancialSummary(string databaseconnectionstring)
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
            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _MessageBoxCaption = "gloPM"; ;
                }
            }
            else
            { _MessageBoxCaption = "gloPM"; ; }
            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                {
                    _UserName = Convert.ToString(appSettings["UserName"]);
                }
            }
            #endregion
            ReportType[0] = "None";
            ReportType[1] = "Facility";
            ReportType[2] = "Provider";
            ReportType[3] = "Facility,Provider";
            ReportType[4] = "Provider,Facility";

            //oProcessLabel = null;
        } 
        #endregion 

        #region "Events"
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
        private void btnBrowseProvider_Click(object sender, EventArgs e)
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
                oListControl.MyCaller = "frmRpt_AgingReport";
                _CurrentControlType = gloListControl.gloListControlType.Providers;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                this.Controls.Add(oListControl);

                if (cmbProvider.DataSource != null)
                {
                    for (int i = 0; i < cmbProvider.Items.Count; i++)
                    {
                        cmbProvider.SelectedIndex = i;
                        cmbProvider.Refresh();
                        oListControl.SelectedItems.Add(Convert.ToInt64(cmbProvider.SelectedValue), cmbProvider.Text);
                    }
                }
                oListControl.OpenControl();
                oListControl.Dock = DockStyle.Fill;
                //oListControl.dgListView.Columns["Column4"].Visible = false;
                oListControl.BringToFront();
              
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void btnClearProvider_Click(object sender, EventArgs e)
        {
           
            cmbProvider.DataSource = null;
            cmbProvider.Items.Clear();
            cmbProvider.Refresh();
        }
        
        private void btnBrowseFacility_Click(object sender, EventArgs e)
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
                oListControl.ControlHeader = "Facility";
                oListControl.MyCaller = "frmRpt_AgingReport";
                _CurrentControlType = gloListControl.gloListControlType.Facility;               
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                this.Controls.Add(oListControl);

                if (cmbFacility.DataSource != null)
                {
                    for (int i = 0; i < cmbFacility.Items.Count; i++)
                    {
                        cmbFacility.SelectedIndex = i;
                        cmbFacility.Refresh();
                        oListControl.SelectedItems.Add(Convert.ToInt64(cmbFacility.SelectedValue),cmbFacility.SelectedText);  
                    }
                   
                }
                oListControl.OpenControl();
                oListControl.Dock = DockStyle.Fill;
                //oListControl.dgListView.Columns["sFacilityCode"].Visible = false;
                oListControl.BringToFront();
            }
            catch(Exception ex)          
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }

        }

        private void btnClearFacility_Click(object sender, EventArgs e)
        {
            
            cmbFacility.DataSource = null;
            cmbFacility.Items.Clear();
            cmbFacility.Refresh();
        }

      
            


        private void rbtn_AgingSummary_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtn_FinSummary.Checked == true)
                rbtn_FinSummary.Font = fBold; 
            else
                rbtn_FinSummary.Font = fRegular; 
        }

        private void rbtn_AgingDetails_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtn_FinDetails.Checked == true)
                rbtn_FinDetails.Font = fBold;
            else
                rbtn_FinDetails.Font = fRegular; 
        }

       
      
      

        private void frmRpt_FinancialSummary_Load(object sender, EventArgs e)
        {


            btnUP.BackgroundImage = global::gloReports.Properties.Resources.UP;
            btnUP.BackgroundImageLayout = ImageLayout.Center;
            btnDown.Visible = false;
            dtpStartDate.Text = Convert.ToString(DateTime.Now.Date.AddMonths(-1));
            dtpEndDate.Text = getCloseDate();
            if (dtpEndDate.Text == string.Empty)
            {
                dtpStartDate.Text = Convert.ToString(DateTime.Now.Date.AddMonths(-1));
                dtpEndDate.Text = Convert.ToString (DateTime.Now.Date);
            }
            else
            {
                dtpStartDate.Text = Convert.ToString(Convert.ToDateTime(dtpEndDate.Text).Date.AddMonths(-1));
               
            }
            tsb_btnExportReport.Visible = false;
            FillBreakByType();
           
        }

        private void FillBreakByType()
        {
            try
            {
                //this.drpMonth.SelectedIndexChanged -= new System.EventHandler(this.drpMonth_SelectedIndexChanged);
                for (int i = 0; i <= 4; i++)
                {
                    ListItem liReportType = new ListItem(ReportType[i], i.ToString());
                    cmbBreakBy.Items.Add(liReportType);

                }
                cmbBreakBy.SelectedIndex = 0;
            }
            catch //(Exception ex)
            {

            }
            finally
            {

                //this.drpMonth.SelectedIndexChanged += new System.EventHandler(this.drpMonth_SelectedIndexChanged);
            }


        }

        private void btnUP_Click(object sender, EventArgs e)
        {
            try
            {
                btnUP.Visible = false;
                btnDown.Visible = true;
                //fpnlCriteria.Visible = true;
                pnlCriteria.Visible = false;
                pnlCriteria.Refresh();
                btnDown.BackgroundImage = global::gloReports.Properties.Resources.Down;
                btnDown.BackgroundImageLayout = ImageLayout.Center;
                lblbtnDown.Visible = true;
            }
            catch //(Exception ex)
            {

            }
            finally
            {
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            try
            {

                btnDown.Visible = false;
                btnUP.Visible = true;
                //fpnlCriteria.Visible = false;
                pnlCriteria.Visible = true;
                btnUP.BackgroundImage = global::gloReports.Properties.Resources.UP;
                btnUP.BackgroundImageLayout = ImageLayout.Center;
                lblbtnDown.Visible = true;
            }
            catch //(Exception ex)
            {
            }
            finally
            {
            }
        }

        private void btnDown_MouseHover(object sender, EventArgs e)
        {
            btnDown.BackgroundImage = global::gloReports.Properties.Resources.DownHover;
            btnDown.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnDown_MouseLeave(object sender, EventArgs e)
        {
            btnDown.BackgroundImage = global::gloReports.Properties.Resources.Down;
            btnDown.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnUP_MouseHover(object sender, EventArgs e)
        {
            btnUP.BackgroundImage = global::gloReports.Properties.Resources.UPHover;
            btnUP.BackgroundImageLayout = ImageLayout.Center;

        }

        private void btnUP_MouseLeave(object sender, EventArgs e)
        {
            btnUP.BackgroundImage = global::gloReports.Properties.Resources.UP;
            btnUP.BackgroundImageLayout = ImageLayout.Center;
        }

        void oListControl_ItemSelectedClick(object sender, EventArgs e)
        {
            int _Counter = 0;
            switch (_CurrentControlType)
            {
                case gloListControl.gloListControlType.Providers:
                    {
                        
                        cmbProvider.DataSource = null;
                        cmbProvider.Items.Clear();
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

                            cmbProvider.DataSource = oBindTable;
                            cmbProvider.DisplayMember = "DispName";
                            cmbProvider.ValueMember = "ID";
                        }

                    }
                    break;
                case gloListControl.gloListControlType.Facility:
                    {
                        
                        cmbFacility.DataSource = null;
                        cmbFacility.Items.Clear();
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

                            cmbFacility.DataSource = oBindTable;
                            cmbFacility.DisplayMember = "DispName";
                            cmbFacility.ValueMember = "ID";
                        }

                    }
                    break;

            
            }
            //if (!(object.ReferenceEquals(oListControl, null)))
            //    oListControl.Dispose();
            //oListControl = null;
            removeOListControl();
        
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
            //if (!(object.ReferenceEquals(oListControl, null)))
            //    oListControl.Dispose();
            //oListControl = null;
            removeOListControl();
        }

        #endregion 


        #region "Function"

     
     
        private DataTable FillAgingReport(String stratdate,String endDate,String sProvider,String sFacility)
        {
            dsAgingReport _dsReport = new dsAgingReport();
            String clinicName = getClinicName();
            string sAgingType = string.Empty;
          //  Decimal dBalance = 0;
          //  Int32 iAgingDays = 0;
            if (Convert.ToDateTime(stratdate) > Convert.ToDateTime(endDate))
            {
                MessageBox.Show("Start Date cannot be greater than End Date.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return _dsReport.Tables["dt_FinancialSummary"];
            }
        
               
            crvReportViewer.Visible = true;
            //_reportDate = "";
            
            String closedate = getCloseDate();

            string sCalculationType = string.Empty;
           
             
             SqlCommand _sqlCommand = new SqlCommand();
             SqlConnection _sqlConnection = new SqlConnection(_databaseconnectionstring);
             try
             {
                 _sqlCommand.CommandType = CommandType.StoredProcedure;
                 _sqlCommand.CommandText = "rpt_Financial_Report";
                 _sqlCommand.Connection = _sqlConnection;
                 _sqlCommand.CommandTimeout = 0;
                 _sqlCommand.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = Convert.ToDateTime(stratdate);
                 _sqlCommand.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = Convert.ToDateTime(endDate);
                 if (sProvider != string.Empty)
                     _sqlCommand.Parameters.Add("@Providers", SqlDbType.VarChar).Value = sProvider;
                 else
                     _sqlCommand.Parameters.Add("@Providers", SqlDbType.VarChar).Value = DBNull.Value;
                 if (sFacility != string.Empty)
                     _sqlCommand.Parameters.Add("@Facilities", SqlDbType.VarChar).Value = sFacility;
                 else
                     _sqlCommand.Parameters.Add("@Facilities", SqlDbType.VarChar).Value = DBNull.Value;

                 SqlDataAdapter da = new SqlDataAdapter(_sqlCommand);

                 da.Fill(_dsReport, "dt_FinancialSummary");
                 da.Dispose();
                 _sqlCommand.Parameters.Clear();


                 if (rbtn_FinSummary.Checked == true)
                 {

                     if (chkIncludeSubTotal.Checked == true)
                     {

                         if (cmbBreakBy.SelectedIndex == Convert.ToInt32(breakByType.None))
                         {
                             reportClosed();
                             oRpt_FinancialSummaryNone = new Rpt_FinancialSummaryNone();

                             //------------Report parametres--------------------------


                             Boolean bParamResult = passParametres(oRpt_FinancialSummaryNone, "Financial Summary", "None", clinicName, _dsReport, stratdate, endDate, _UserName);
                             oRpt_FinancialSummaryNone.SetDataSource(_dsReport);
                             crvReportViewer.ReportSource = oRpt_FinancialSummaryNone;
                             Boolean bResult = hideYear(oRpt_FinancialSummaryNone, stratdate, endDate);
                             //passParametres(oRptAgingSummaryInsurancePending1, "Aging Summary", "None", clinicName);


                         }

                         else if (cmbBreakBy.SelectedIndex == Convert.ToInt32(breakByType.Facility))
                         {
                             reportClosed();
                             oRpt_FinancialSummaryFacility = new Rpt_FinancialSummaryFacility();

                             //------------Report parametres--------------------------
                             Boolean bParamResult = passParametres(oRpt_FinancialSummaryFacility, "Financial Summary", "Facility", clinicName, _dsReport, stratdate, endDate, _UserName);
                             oRpt_FinancialSummaryFacility.SetDataSource(_dsReport);
                             crvReportViewer.ReportSource = oRpt_FinancialSummaryFacility;
                             Boolean bResult = hideYear(oRpt_FinancialSummaryFacility, stratdate, endDate);

                         }
                         else if (cmbBreakBy.SelectedIndex == Convert.ToInt32(breakByType.Provider))
                         {
                             reportClosed();
                             oRpt_FinancialSummaryProvider = new Rpt_FinancialSummaryProvider();
                             Boolean bParamResult = passParametres(oRpt_FinancialSummaryProvider, "Financial Summary", "Provider", clinicName, _dsReport, stratdate, endDate, _UserName);
                             oRpt_FinancialSummaryProvider.SetDataSource(_dsReport);
                             crvReportViewer.ReportSource = oRpt_FinancialSummaryProvider;
                             //------------Report parametres--------------------------
                             Boolean bResult = hideYear(oRpt_FinancialSummaryProvider, stratdate, endDate);




                         }
                         else if (cmbBreakBy.SelectedIndex == Convert.ToInt32(breakByType.FacilityProvider))
                         {
                             reportClosed();
                             oRpt_FinancialSummaryFacilityProvider = new Rpt_FinancialSummaryFacilityProvider();
                             Boolean bParamResult = passParametres(oRpt_FinancialSummaryFacilityProvider, "Financial Summary", "Facility,Provider", clinicName, _dsReport, stratdate, endDate, _UserName);
                             oRpt_FinancialSummaryFacilityProvider.SetDataSource(_dsReport);
                             crvReportViewer.ReportSource = oRpt_FinancialSummaryFacilityProvider;
                             //------------Report parametres--------------------------
                             Boolean bResult = hideYear(oRpt_FinancialSummaryFacilityProvider, stratdate, endDate);
                         }

                         else if (cmbBreakBy.SelectedIndex == Convert.ToInt32(breakByType.ProviderFacility))
                         {

                             reportClosed();
                             oRpt_FinancialSummaryProviderFacility = new Rpt_FinancialSummary();
                             Boolean bParamResult = passParametres(oRpt_FinancialSummaryProviderFacility, "Financial Summary", "Provider,Facility", clinicName, _dsReport, stratdate, endDate, _UserName);
                             oRpt_FinancialSummaryProviderFacility.SetDataSource(_dsReport);
                             crvReportViewer.ReportSource = oRpt_FinancialSummaryProviderFacility;
                             Boolean bResult = hideYear(oRpt_FinancialSummaryProviderFacility, stratdate, endDate);
                             //------------Report parametres--------------------------

                         }


                     }
                     else if (chkIncludeSubTotal.Checked == false)
                     {
                         if (cmbBreakBy.SelectedIndex == Convert.ToInt32(breakByType.None))
                         {
                             reportClosed();
                             oRpt_FinancialSummaryNone_withoutDay = new Rpt_FinancialSummaryNone_withoutDay();

                             //------------Report parametres--------------------------


                             Boolean bParamResult = passParametres(oRpt_FinancialSummaryNone_withoutDay, "Financial Summary", "None", clinicName, _dsReport, stratdate, endDate, _UserName);
                             oRpt_FinancialSummaryNone_withoutDay.SetDataSource(_dsReport);
                             crvReportViewer.ReportSource = oRpt_FinancialSummaryNone_withoutDay;
                             //passParametres(oRptAgingSummaryInsurancePending1, "Aging Summary", "None", clinicName);
                             Boolean bResult = hideYear(oRpt_FinancialSummaryNone_withoutDay, stratdate, endDate);

                         }

                         else if (cmbBreakBy.SelectedIndex == Convert.ToInt32(breakByType.Facility))
                         {
                             reportClosed();
                             oRpt_FinancialSummaryFacility_withoutDay = new Rpt_FinancialSummaryFacility_withoutDay();
                             Boolean bParamResult = passParametres(oRpt_FinancialSummaryFacility_withoutDay, "Financial Summary", "Facility", clinicName, _dsReport, stratdate, endDate, _UserName);
                             //------------Report parametres--------------------------
                             oRpt_FinancialSummaryFacility_withoutDay.SetDataSource(_dsReport);
                             crvReportViewer.ReportSource = oRpt_FinancialSummaryFacility_withoutDay;
                             hideYear(oRpt_FinancialSummaryFacility_withoutDay, stratdate, endDate);

                         }
                         else if (cmbBreakBy.SelectedIndex == Convert.ToInt32(breakByType.Provider))
                         {
                             reportClosed();
                             oRpt_FinancialSummaryProvider_withoutDay = new Rpt_FinancialSummaryProvider_withoutDay();
                             Boolean bParamResult = passParametres(oRpt_FinancialSummaryProvider_withoutDay, "Financial Summary", "Provider", clinicName, _dsReport, stratdate, endDate, _UserName);
                             oRpt_FinancialSummaryProvider_withoutDay.SetDataSource(_dsReport);
                             crvReportViewer.ReportSource = oRpt_FinancialSummaryProvider_withoutDay;
                             //------------Report parametres--------------------------
                             Boolean bResult = hideYear(oRpt_FinancialSummaryProvider_withoutDay, stratdate, endDate);
                         }
                         else if (cmbBreakBy.SelectedIndex == Convert.ToInt32(breakByType.FacilityProvider))
                         {
                             reportClosed();
                             oRpt_FinancialSummaryFacilityProvider_withoutDay = new Rpt_FinancialSummaryFacilityProvider_withoutDay();
                             Boolean bParamResult = passParametres(oRpt_FinancialSummaryFacilityProvider_withoutDay, "Financial Summary", "Facility,Provider", clinicName, _dsReport, stratdate, endDate, _UserName);
                             oRpt_FinancialSummaryFacilityProvider_withoutDay.SetDataSource(_dsReport);
                             crvReportViewer.ReportSource = oRpt_FinancialSummaryFacilityProvider_withoutDay;
                             //------------Report parametres--------------------------
                             Boolean bResult = hideYear(oRpt_FinancialSummaryFacilityProvider_withoutDay, stratdate, endDate);


                         }

                         else if (cmbBreakBy.SelectedIndex == Convert.ToInt32(breakByType.ProviderFacility))
                         {

                             reportClosed();
                             oRpt_FinancialSummaryProviderFacility_withoutDay = new Rpt_FinancialSummary_withoutDay();
                             Boolean bParamResult = passParametres(oRpt_FinancialSummaryProviderFacility_withoutDay, "Financial Summary", "Provider,Facility", clinicName, _dsReport, stratdate, endDate, _UserName);
                             oRpt_FinancialSummaryProviderFacility_withoutDay.SetDataSource(_dsReport);
                             crvReportViewer.ReportSource = oRpt_FinancialSummaryProviderFacility_withoutDay;

                             //------------Report parametres--------------------------
                             Boolean bResult = hideYear(oRpt_FinancialSummaryProviderFacility_withoutDay, stratdate, endDate);


                         }


                     }
                 }

                 else if (rbtn_FinDetails.Checked == true)
                 {
                     if (chkIncludeSubTotal.Checked == true)
                     {
                         if (cmbBreakBy.SelectedIndex == Convert.ToInt32(breakByType.None))
                         {
                             reportClosed();
                             oRpt_FinancialDetailNone = new Rpt_FinancialDetailNone();
                             _rptName = oRpt_FinancialDetailNone;
                             Boolean bParamResult = passParametres(oRpt_FinancialDetailNone, "Financial Details", "None", clinicName, _dsReport, stratdate, endDate, _UserName);
                             oRpt_FinancialDetailNone.SetDataSource(_dsReport);
                             crvReportViewer.ReportSource = oRpt_FinancialDetailNone;
                             crvReportViewerBackend.ReportSource = oRpt_FinancialDetailNone;
                             Boolean bResult = hideYear(oRpt_FinancialDetailNone, stratdate, endDate);

                         }

                         else if (cmbBreakBy.SelectedIndex == Convert.ToInt32(breakByType.Facility))
                         {
                             reportClosed();
                             oRpt_FinancialDetailFacility = new Rpt_FinancialDetailFacility();
                             _rptName = oRpt_FinancialDetailFacility;
                             Boolean bParamResult = passParametres(oRpt_FinancialDetailFacility, "Financial Details", "Facility", clinicName, _dsReport, stratdate, endDate, _UserName);
                             oRpt_FinancialDetailFacility.SetDataSource(_dsReport);
                             crvReportViewer.ReportSource = oRpt_FinancialDetailFacility;
                             crvReportViewerBackend.ReportSource = oRpt_FinancialDetailFacility;
                             Boolean bResult = hideYear(oRpt_FinancialDetailFacility, stratdate, endDate);
                         }

                         else if (cmbBreakBy.SelectedIndex == Convert.ToInt32(breakByType.Provider))
                         {
                             reportClosed();
                             oRpt_FinancialDetailNoneProvider = new Rpt_FinancialDetailProvider();
                             _rptName = oRpt_FinancialDetailNoneProvider;
                             Boolean bParamResult = passParametres(oRpt_FinancialDetailNoneProvider, "Financial Details", "Provider", clinicName, _dsReport, stratdate, endDate, _UserName);
                             oRpt_FinancialDetailNoneProvider.SetDataSource(_dsReport);
                             crvReportViewer.ReportSource = oRpt_FinancialDetailNoneProvider;
                             crvReportViewerBackend.ReportSource = oRpt_FinancialDetailNoneProvider;
                             Boolean bResult = hideYear(oRpt_FinancialDetailNoneProvider,stratdate,endDate);
                         }
                         else if (cmbBreakBy.SelectedIndex == Convert.ToInt32(breakByType.FacilityProvider))
                         {
                             reportClosed();
                             oRpt_FinancialDetailNoneFacilityProvider = new Rpt_FinancialDetailFacilityProvider();
                             _rptName = oRpt_FinancialDetailNoneFacilityProvider;
                             Boolean bParamResult = passParametres(oRpt_FinancialDetailNoneFacilityProvider, "Financial Details", "Facility,Provider", clinicName, _dsReport, stratdate, endDate, _UserName);
                             oRpt_FinancialDetailNoneFacilityProvider.SetDataSource(_dsReport);
                             crvReportViewer.ReportSource = oRpt_FinancialDetailNoneFacilityProvider;
                             crvReportViewerBackend.ReportSource = oRpt_FinancialDetailNoneFacilityProvider;
                             Boolean bResult = hideYear(oRpt_FinancialDetailNoneFacilityProvider, stratdate, endDate);
                         }
                         else if (cmbBreakBy.SelectedIndex == Convert.ToInt32(breakByType.ProviderFacility))
                         {
                             reportClosed();
                             oRpt_FinancialDetailNoneProviderFacility = new Rpt_FinancialDetailProviderFacility();
                             _rptName = oRpt_FinancialDetailNoneProviderFacility;
                             Boolean bParamResult = passParametres(oRpt_FinancialDetailNoneProviderFacility, "Financial Details", "Provider,Facility", clinicName, _dsReport, stratdate, endDate, _UserName);
                             oRpt_FinancialDetailNoneProviderFacility.SetDataSource(_dsReport);
                             crvReportViewer.ReportSource = oRpt_FinancialDetailNoneProviderFacility;
                             crvReportViewerBackend.ReportSource = oRpt_FinancialDetailNoneProviderFacility;
                             Boolean bResult = hideYear(oRpt_FinancialDetailNoneProviderFacility, stratdate, endDate);
                         }
                     }
                         else if (chkIncludeSubTotal.Checked == false)
                         {

                             if (cmbBreakBy.SelectedIndex == Convert.ToInt32(breakByType.None))
                             {

                                 reportClosed();
                                 oRpt_FinancialDetailNone_withoutDay = new Rpt_FinancialDetail_withoutDay();
                                 _rptName = oRpt_FinancialDetailNone_withoutDay;
                                 Boolean bParamResult = passParametres(oRpt_FinancialDetailNone_withoutDay, "Financial Details", "None", clinicName, _dsReport, stratdate, endDate, _UserName);
                                 oRpt_FinancialDetailNone_withoutDay.SetDataSource(_dsReport);
                                 crvReportViewer.ReportSource = oRpt_FinancialDetailNone_withoutDay;
                                   crvReportViewerBackend.ReportSource = oRpt_FinancialDetailNone_withoutDay;
                                 Boolean bResult = hideYear(oRpt_FinancialDetailNone_withoutDay, stratdate, endDate);
                             }

                             else if (cmbBreakBy.SelectedIndex == Convert.ToInt32(breakByType.Facility))
                       {
                           reportClosed();
                                 oRpt_FinancialDetailFacility_withoutDay = new Rpt_FinancialDetailFacility_withoutDay();
                                 _rptName = oRpt_FinancialDetailFacility_withoutDay;
                                 Boolean bParamResult = passParametres(oRpt_FinancialDetailFacility_withoutDay, "Financial Details", "Facility", clinicName, _dsReport, stratdate, endDate, _UserName);
                                 oRpt_FinancialDetailFacility_withoutDay.SetDataSource(_dsReport);
                                 crvReportViewer.ReportSource = oRpt_FinancialDetailFacility_withoutDay;
                                 crvReportViewerBackend.ReportSource = oRpt_FinancialDetailFacility_withoutDay;
                                 Boolean bResult = hideYear(oRpt_FinancialDetailFacility_withoutDay, stratdate, endDate); 
                         }
                         else if (cmbBreakBy.SelectedIndex == Convert.ToInt32(breakByType.Provider))
                         {
                             reportClosed();
                                 oRpt_FinancialDetailProvider_withoutDay = new Rpt_FinancialDetailProvider_withoutDay();
                                 _rptName = oRpt_FinancialDetailProvider_withoutDay;
                                 Boolean bParamResult = passParametres(oRpt_FinancialDetailProvider_withoutDay, "Financial Details", "Provider", clinicName, _dsReport, stratdate, endDate, _UserName);
                                 oRpt_FinancialDetailProvider_withoutDay.SetDataSource(_dsReport);

                                 crvReportViewer.ReportSource = oRpt_FinancialDetailProvider_withoutDay;
                                 crvReportViewerBackend.ReportSource = oRpt_FinancialDetailProvider_withoutDay;
                                 Boolean bResult = hideYear(oRpt_FinancialDetailProvider_withoutDay, stratdate, endDate); 
                         }
                         else if (cmbBreakBy.SelectedIndex == Convert.ToInt32(breakByType.FacilityProvider))
                         {
                             reportClosed();
                                 oRpt_FinancialDetailFacilityProvider_withoutDay = new Rpt_FinancialDetailFacilityProvider_withoutDay();
                                 _rptName = oRpt_FinancialDetailFacilityProvider_withoutDay;
                                 Boolean bParamResult = passParametres(oRpt_FinancialDetailFacilityProvider_withoutDay, "Financial Details", "Facility,Provider", clinicName, _dsReport, stratdate, endDate, _UserName);
                                 oRpt_FinancialDetailFacilityProvider_withoutDay.SetDataSource(_dsReport);

                                 crvReportViewer.ReportSource = oRpt_FinancialDetailFacilityProvider_withoutDay;
                                 crvReportViewerBackend.ReportSource = oRpt_FinancialDetailFacilityProvider_withoutDay;
                                 Boolean bResult = hideYear(oRpt_FinancialDetailFacilityProvider_withoutDay, stratdate, endDate); 
                       }
                       else if (cmbBreakBy.SelectedIndex == Convert.ToInt32(breakByType.ProviderFacility))
                         {
                             reportClosed();
                                 oRpt_FinancialDetailProviderFacility_withoutDay = new Rpt_FinancialDetailProviderFacility_withoutDay();
                                 _rptName = oRpt_FinancialDetailProviderFacility_withoutDay;
                                 Boolean bParamResult = passParametres(oRpt_FinancialDetailProviderFacility_withoutDay, "Financial Details", "Provider,Facility", clinicName, _dsReport, stratdate, endDate, _UserName);
                                 oRpt_FinancialDetailProviderFacility_withoutDay.SetDataSource(_dsReport);

                                 crvReportViewer.ReportSource = oRpt_FinancialDetailProviderFacility_withoutDay;
                                 crvReportViewerBackend.ReportSource = oRpt_FinancialDetailProviderFacility_withoutDay;
                                 Boolean bResult = hideYear(oRpt_FinancialDetailProviderFacility_withoutDay, stratdate, endDate);  
                       }


                    }



                 }
                 return _dsReport.Tables["dt_FinancialSummary"];
             }


             catch (Exception ex)
             {
                 
                 gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                 _sqlCommand.Dispose();

                 _sqlConnection.Dispose();
                 _dsReport.Dispose();
              
                 return _dsReport.Tables["dt_FinancialSummary"];
                 //passParametres(oRpt_AgingDetail_insurance_Provider, sCalculationType, (rbtn_AgingSummary.Checked == true ? rbtn_AgingSummary.Text : rbtn_AgingDetails.Text), (rbtn_InsCompany.Checked == true ? rbtn_InsCompany.Text : (rbtn_PatientDue.Checked == true ? rbtn_PatientDue.Text : rbtn_Both.Text)), cmbBreakBy.SelectedText, clinicName);
             }
             finally
             {
                 if (_sqlCommand != null)
                 {
                     _sqlCommand.Parameters.Clear();
                     _sqlCommand.Dispose();
                     _sqlCommand = null;
                 }
                 if (_sqlConnection != null && _sqlConnection.State == ConnectionState.Open)
                 {
                     _sqlConnection.Close();
                     

                 }
                 if (_sqlConnection != null)
                 {
                     _sqlConnection.Dispose();
                 }
               // _dsReport.Dispose();               
                 
             }
        }
        private Boolean hideYear(ReportDocument rptObj,String startDate, String endDate)
        {
            try
            {
                if (Convert.ToDateTime(endDate).Date.Year - Convert.ToDateTime(startDate).Date.Year == 0)
                {
                    rptObj.ReportDefinition.Sections["GroupFooterSection1"].SectionFormat.EnableSuppress = true;
                }
                return true;
            }
            catch
            {
                return false;   
            }
        }
        private Boolean passParametres(ReportDocument rptObj, String reportType, String breakBy, String clinic, DataSet ds, String startDate, String endDate,String userName)
        {
            try
            {
                DataTable dtparam = new DataTable();
                dtparam.Columns.Add("startDate");
                dtparam.Columns.Add("endDate");
                dtparam.Columns.Add("reportType");
                dtparam.Columns.Add("breakBy");
                dtparam.Columns.Add("clinic");
                dtparam.Columns.Add("user");
                dtparam.Columns.Add("IncludeSubTotal");
                dtparam.Rows.Add();
                dtparam.Rows[0]["startDate"] = Convert.ToDateTime(startDate).Date;
                dtparam.Rows[0]["endDate"] = Convert.ToDateTime(endDate).Date;
                dtparam.Rows[0]["reportType"] = reportType;
                dtparam.Rows[0]["breakBy"] = breakBy;
                dtparam.Rows[0]["clinic"] = clinic;
                dtparam.Rows[0]["user"] = userName;
                if (chkIncludeSubTotal.Checked == true)
                {
                    dtparam.Rows[0]["IncludeSubTotal"] = 1;
                }
                else
                {
                    dtparam.Rows[0]["IncludeSubTotal"] = 0;
                }


                foreach (DataRow orow in dtparam.Rows)
                    ds.Tables["dtparam"].ImportRow(orow);
                dtparam.Dispose();
                dtparam = null;
                return true;
               
            }
            catch //(Exception ex)
            {
                return false;
            }
        }


       
        private string getCloseDate()
        {
            try
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                object _Result = oDB.ExecuteScalar_Query("select dbo.Convert_to_date(max(nCloseDayDate)) As CloseDate from BL_CloseDays");
                oDB.Disconnect();
                oDB.Dispose();
                oDB = null;
                if (_Result.ToString() != "")
                {
                    return _Result.ToString();
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
               return  "Error in Returning Date.";
            }
        }

        private string getClinicName()
        {
            try
            {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            object _Result = oDB.ExecuteScalar_Query("select coalesce(sClinicName,'') as  sClinicName  from Clinic_MST");
            oDB.Disconnect();
            oDB.Dispose();
            oDB = null;
            if (_Result.ToString() != "")
            {
                return _Result.ToString();
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
                return "Error in Returning Clinic Name.";
            }
        }

        #endregion

        private void tsb_GenerateReport_Click(object sender, EventArgs e)
        {
            _rptName = null;
            Cursor.Current = Cursors.WaitCursor;

            String sProvider = string.Empty;
            String sFacility = string.Empty;
        

            if (cmbProvider.Items.Count > 0)
            {

                for (int cntrProvider = 0; cntrProvider <= cmbProvider.Items.Count - 1; cntrProvider++)
                {
                    if (sProvider == string.Empty)
                        sProvider = (Convert.ToString(((DataRowView)cmbProvider.Items[cntrProvider])["ID"]));
                    else
                        sProvider = sProvider + "," + (Convert.ToString(((DataRowView)cmbProvider.Items[cntrProvider])["ID"]));

                }
            }



            if (cmbFacility.Items.Count > 0)
            {

                for (int cntrFacility = 0; cntrFacility <= cmbFacility.Items.Count - 1; cntrFacility++)
                {
                    if (sFacility == string.Empty)
                        sFacility = (Convert.ToString(((DataRowView)cmbFacility.Items[cntrFacility])["ID"]));
                    else
                        sFacility = sFacility + "," + (Convert.ToString(((DataRowView)cmbFacility.Items[cntrFacility])["ID"]));
                }
            }
            DataTable dtRptResult;
            dtRptResult = FillAgingReport(dtpStartDate.Text, dtpEndDate.Text, sProvider, sFacility);
            Cursor.Current = Cursors.Default;
            tsb_btnExportReport.Visible = true;
        }
        private void tsb_btnExport_Click(object sender, EventArgs e)
        {
            crvReportViewer.ExportReport();
        }
        private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            closed();
            if (!(object.ReferenceEquals(_rptName, null)))
            {
                _rptName.Close();
                _rptName.Dispose();
                _rptName = null;
            }
            this.Close();
        }

   
        private void tsb_Print_Click(object sender, EventArgs e)
        {
            if (_rptName != null)
            {
                _rptName.ReportDefinition.Sections["DetailSection3"].SectionFormat.BackgroundColor = Color.White;
                crvReportViewerBackend.RefreshReport();

                crvReportViewerBackend.PrintReport();
                _rptName.ReportDefinition.Sections["DetailSection3"].SectionFormat.BackgroundColor = Color.FromArgb(255, 237, 208);
                crvReportViewerBackend.RefreshReport();
            }
            else
            {
                crvReportViewer.PrintReport();
            }
        }

        private void frmRpt_FinancialSummary_FormClosed(object sender, FormClosedEventArgs e)
        {
            closed();
            if (!(object.ReferenceEquals(_rptName, null)))
            {
                _rptName.Close();
                _rptName.Dispose();
                _rptName = null;
            }
        }
        private void closed()
        {
           
        //    if (!(object.ReferenceEquals(fBold, null)))
        //    fBold.Dispose();
        //if (!(object.ReferenceEquals(fRegular, null)))
        //    fRegular.Dispose();
        //    fBold = null;
        //    fRegular = null;
        }
        private void reportClosed()
        {
            if (!(object.ReferenceEquals(oRpt_FinancialSummaryProviderFacility, null)))
            {
                oRpt_FinancialSummaryProviderFacility.Close();
                oRpt_FinancialSummaryProviderFacility.Dispose();
                oRpt_FinancialSummaryProviderFacility = null;
            }
            if (!(object.ReferenceEquals(oRpt_FinancialSummaryFacility, null)))
            {
                oRpt_FinancialSummaryFacility.Close();
                oRpt_FinancialSummaryFacility.Dispose();
                oRpt_FinancialSummaryFacility = null;
            }
            if (!(object.ReferenceEquals(oRpt_FinancialSummaryFacilityProvider, null)))
            {
                oRpt_FinancialSummaryFacilityProvider.Close();
                oRpt_FinancialSummaryFacilityProvider.Dispose();
                oRpt_FinancialSummaryFacilityProvider = null;
            }
            if (!(object.ReferenceEquals(oRpt_FinancialSummaryNone, null)))
            {
                oRpt_FinancialSummaryNone.Close();
                oRpt_FinancialSummaryNone.Dispose();
                oRpt_FinancialSummaryNone = null;
            }
            if (!(object.ReferenceEquals(oRpt_FinancialSummaryProvider, null)))
            {
                oRpt_FinancialSummaryProvider.Close();
                oRpt_FinancialSummaryProvider.Dispose();
                oRpt_FinancialSummaryProvider = null;
            }
            if (!(object.ReferenceEquals(oRpt_FinancialSummaryProviderFacility_withoutDay, null)))
            {
                oRpt_FinancialSummaryProviderFacility_withoutDay.Close();
                oRpt_FinancialSummaryProviderFacility_withoutDay.Dispose();
                oRpt_FinancialSummaryProviderFacility_withoutDay = null;
            }
            if (!(object.ReferenceEquals(oRpt_FinancialSummaryFacility_withoutDay, null)))
            {
                oRpt_FinancialSummaryFacility_withoutDay.Close();
                oRpt_FinancialSummaryFacility_withoutDay.Dispose();
                oRpt_FinancialSummaryFacility_withoutDay = null;
            }
            if (!(object.ReferenceEquals(oRpt_FinancialSummaryFacilityProvider_withoutDay, null)))
            {
                oRpt_FinancialSummaryFacilityProvider_withoutDay.Close();
                oRpt_FinancialSummaryFacilityProvider_withoutDay.Dispose();
                oRpt_FinancialSummaryFacilityProvider_withoutDay = null;
            }
            if (!(object.ReferenceEquals(oRpt_FinancialSummaryNone_withoutDay, null)))
            {
                oRpt_FinancialSummaryNone_withoutDay.Close();
                oRpt_FinancialSummaryNone_withoutDay.Dispose();
                oRpt_FinancialSummaryNone_withoutDay = null;
            }
            if (!(object.ReferenceEquals(oRpt_FinancialSummaryProvider_withoutDay, null)))
            {
                oRpt_FinancialSummaryProvider_withoutDay.Close();
                oRpt_FinancialSummaryProvider_withoutDay.Dispose();
                oRpt_FinancialSummaryProvider_withoutDay = null;
            }
            if (!(object.ReferenceEquals(oRpt_FinancialDetailNone, null)))
            {
                oRpt_FinancialDetailNone.Close();
                oRpt_FinancialDetailNone.Dispose();
                oRpt_FinancialDetailNone = null;
            }
            if (!(object.ReferenceEquals(oRpt_FinancialDetailNone_withoutDay, null)))
            {
                oRpt_FinancialDetailNone_withoutDay.Close();
                oRpt_FinancialDetailNone_withoutDay.Dispose();
                oRpt_FinancialDetailNone_withoutDay = null;
            }
            if (!(object.ReferenceEquals(oRpt_FinancialDetailFacility, null)))
            {
                oRpt_FinancialDetailFacility.Close();
                oRpt_FinancialDetailFacility.Dispose();
                oRpt_FinancialDetailFacility = null;
            }
            if (!(object.ReferenceEquals(oRpt_FinancialDetailFacility_withoutDay, null)))
            {
                oRpt_FinancialDetailFacility_withoutDay.Close();
                oRpt_FinancialDetailFacility_withoutDay.Dispose();
                oRpt_FinancialDetailFacility_withoutDay = null;
            }
            if (!(object.ReferenceEquals(oRpt_FinancialDetailNoneProvider, null)))
            {
                oRpt_FinancialDetailNoneProvider.Close();
                oRpt_FinancialDetailNoneProvider.Dispose();
                oRpt_FinancialDetailNoneProvider = null;
            }
            if (!(object.ReferenceEquals(oRpt_FinancialDetailProvider_withoutDay, null)))
            {
                oRpt_FinancialDetailProvider_withoutDay.Close();
                oRpt_FinancialDetailProvider_withoutDay.Dispose();
                oRpt_FinancialDetailProvider_withoutDay = null;
            }
            if (!(object.ReferenceEquals(oRpt_FinancialDetailNoneFacilityProvider, null)))
            {
                oRpt_FinancialDetailNoneFacilityProvider.Close();
                oRpt_FinancialDetailNoneFacilityProvider.Dispose();
                oRpt_FinancialDetailNoneFacilityProvider = null;
            }
            if (!(object.ReferenceEquals(oRpt_FinancialDetailFacilityProvider_withoutDay, null)))
            {
                oRpt_FinancialDetailFacilityProvider_withoutDay.Close();
                oRpt_FinancialDetailFacilityProvider_withoutDay.Dispose();
                oRpt_FinancialDetailFacilityProvider_withoutDay = null;
            }
            if (!(object.ReferenceEquals(oRpt_FinancialDetailNoneProviderFacility, null)))
            {
                oRpt_FinancialDetailNoneProviderFacility.Close();
                oRpt_FinancialDetailNoneProviderFacility.Dispose();
                oRpt_FinancialDetailNoneProviderFacility = null;
            }
            if (!(object.ReferenceEquals(oRpt_FinancialDetailProviderFacility_withoutDay, null)))
            {
                oRpt_FinancialDetailProviderFacility_withoutDay.Close();
                oRpt_FinancialDetailProviderFacility_withoutDay.Dispose();
                oRpt_FinancialDetailProviderFacility_withoutDay = null;
            }
        }

    }
}