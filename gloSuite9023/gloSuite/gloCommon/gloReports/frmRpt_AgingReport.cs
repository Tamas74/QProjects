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
    public partial class frmRpt_AgingReport : Form
    {

        #region "Variable Declaration"
        gloListControl.gloListControl oListControl = null;
        private string _databaseconnectionstring = "";
        private string _UserName = "";
        gloListControl.gloListControlType _CurrentControlType = gloListControl.gloListControlType.Other;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID;
        private string[] ReportType = new string[5];
        private string _reportDate="";
        Rpt_AgingSummary_PatientFacility oRpt_AgingSummary_PatientFacility = new Rpt_AgingSummary_PatientFacility();
        Rpt_AgingSummary oRptAgingSummaryInsurancePending1 = new Rpt_AgingSummary();
        Rpt_AgingSummary_InsFacilityProvider_ oRpt_AgingSummary_InsFacilityProvider = new Rpt_AgingSummary_InsFacilityProvider_();
        Rpt_AgingSummary_InsProviderFacility_ oRpt_AgingSummary_InsProviderFacility = new Rpt_AgingSummary_InsProviderFacility_();
        rpt_AgingSummaryPatAndInsurance oRpt_AgingSummaryPatAndInsurance = new rpt_AgingSummaryPatAndInsurance();
        Rpt_AgingDetail_ oRpt_AgingDetailPatient = new Rpt_AgingDetail_();
        Rpt_AgingDetailInsurance_ oRpt_AgingDetailInsurance = new Rpt_AgingDetailInsurance_();
        Rpt_AgingSummary_PatientProvider oRpt_AgingSummary_PatientProvider = new Rpt_AgingSummary_PatientProvider();
        rpt_AgingSummaryPatAndInsFacilityProvider_ oRpt_AgingSummaryPatAndInsFacilityProvider = new rpt_AgingSummaryPatAndInsFacilityProvider_();
        rpt_AgingSummaryPatAndInsProviderFacility_ orpt_AgingSummaryPatAndInsProviderFacility = new rpt_AgingSummaryPatAndInsProviderFacility_();
        rpt_AgingSummaryPatAndInsNone orpt_AgingSummaryPatAndInsNone = new rpt_AgingSummaryPatAndInsNone();
        rpt_AgingSummaryPatAndInsProvider orpt_AgingSummaryPatAndInsProvider = new rpt_AgingSummaryPatAndInsProvider();
        rpt_AgingSummaryPatAndInsFacility orpt_AgingSummaryPatAndInsFacility = new rpt_AgingSummaryPatAndInsFacility();
        Rpt_AgingDetail_insurance_FacilityProvider oRpt_AgingDetail_insurance_FacilityProvider = new Rpt_AgingDetail_insurance_FacilityProvider();
        Rpt_AgingDetail_insurance_ProviderFacility oRpt_AgingDetail_insurance_ProviderFacility = new Rpt_AgingDetail_insurance_ProviderFacility();
        Rpt_AgingDetail_insurance_Provider oRpt_AgingDetail_insurance_Provider = new Rpt_AgingDetail_insurance_Provider();
        Rpt_AgingDetail_insurance_Facility oRpt_AgingDetail_insurance_Facility = new Rpt_AgingDetail_insurance_Facility();
        Rpt_AgingDetail_insurance_None oRpt_AgingDetail_insurance_None = new Rpt_AgingDetail_insurance_None();

        Rpt_AgingDetail_PatientFacility oRpt_AgingDetail_PatientFacility = new Rpt_AgingDetail_PatientFacility();
        Rpt_AgingDetail_PatientFacilityProvider oRpt_AgingDetail_PatientFacilityProvider = new Rpt_AgingDetail_PatientFacilityProvider();
        Rpt_AgingDetail_PatientProvider oRpt_AgingDetail_PatientProvider = new Rpt_AgingDetail_PatientProvider();
        Rpt_AgingDetail_PatientProviderFacility oRpt_AgingDetail_PatientProviderFacility = new Rpt_AgingDetail_PatientProviderFacility();
        Font regularFont = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
        Font boldFont = gloGlobal.clsgloFont.gFont_BOLD;// new Font("Tahoma", 9, FontStyle.Bold);
        enum breakByType
        {
            None = 0,
            Facility = 1,
            Provider = 2,
            FacilityProvider = 3,
            ProviderFacility = 4
        }

        //System.Windows.Forms.Label oProcessLabel = null;
        //int _counter = 0;
        #endregion

        #region "Constructor"
        public frmRpt_AgingReport(string databaseconnectionstring)
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
            if (appSettings["UserName"] != null) 
            { 
                if (appSettings["UserName"] != "")
                { 
                    _UserName = Convert.ToString(appSettings["UserName"]);
                } 
            }

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
                oListControl.MyCaller = this.Name;
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
                oListControl.MyCaller = this.Name;
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

        private void btnBrowseInsurance_Click(object sender, EventArgs e)
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
                oListControl =new gloListControl.gloListControl(_databaseconnectionstring,gloListControl.gloListControlType.InsuranceCompany,true,this.Width);
                oListControl.ClinicID=_ClinicID;
                oListControl.ControlHeader="Insurance Company";
                _CurrentControlType = gloListControl.gloListControlType.InsuranceCompany;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                this.Controls.Add(oListControl);
            
                if (cmbInsuranceCompany.DataSource !=null)
                {
                    for (int i=0;i < cmbInsuranceCompany.Items.Count;i++)
                    {
                        cmbInsuranceCompany.SelectedIndex=i;
                        cmbInsuranceCompany.Refresh();
                        oListControl.SelectedItems.Add(Convert.ToInt64(cmbInsuranceCompany.SelectedValue),cmbInsuranceCompany.SelectedText);
                    }
                   
                }
                oListControl.OpenControl();
                oListControl.Dock = DockStyle.Fill;
                oListControl.BringToFront();
                
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
        }

        private void btnClearInsurance_Click(object sender, EventArgs e)
        {
           
            cmbInsuranceCompany.DataSource = null;
            cmbInsuranceCompany.Items.Clear();
            cmbInsuranceCompany.Refresh();
        }

        private void btnBrowseReportingCriteria_Click(object sender, EventArgs e)
        {
            try
            {
                //if (oListControl != null)
                //{
                //    for (int i = this.Controls.Count-1; i >= 0; i--)
                //    {
                //        if (this.Controls[i].Name == oListControl.Name)
                //        {
                //            oListControl.Controls.Remove(this.Controls[i]);
                //            break;
                //        }                   
                //    } 
                //}
                removeOListControl();
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.InsuranceReportingCategory, true, this.Width);
                oListControl.ClinicID = _ClinicID;
                oListControl.ControlHeader = "Insurance Reporting Category";
                _CurrentControlType = gloListControl.gloListControlType.InsuranceReportingCategory;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                this.Controls.Add(oListControl);

                if (cmbReportingCategory.DataSource != null)
                {
                    for (int i = 0; i < cmbReportingCategory.Items.Count; i++)
                    {
                        cmbReportingCategory.SelectedIndex = i;
                        cmbReportingCategory.Refresh();
                        oListControl.SelectedItems.Add(Convert.ToInt64(cmbReportingCategory.SelectedValue), cmbReportingCategory.SelectedText);
                    }
                }
                oListControl.OpenControl();
                oListControl.Dock = DockStyle.Fill;
                oListControl.BringToFront();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void btnClearReportingCriteria_Click(object sender, EventArgs e)
        {
           
            cmbReportingCategory.DataSource = null;
            cmbReportingCategory.Items.Clear();
            cmbReportingCategory.Refresh();
        }
            
        private void rbtn_DateOfResponsibility_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtn_DateOfResponsibility.Checked == true)
                rbtn_DateOfResponsibility.Font = boldFont;
            else
                rbtn_DateOfResponsibility.Font = regularFont;
        }

        private void rbtn_AgingSummary_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtn_AgingSummary.Checked == true)
            {
                rbtn_AgingSummary.Font = boldFont;
                pnlInsuranceCompany.Visible = true;
                pnlReportingCategory.Visible = false;
                grInsurance.Enabled = false;
                //rbtn_Both.Visible = true;
                rbtn_InsCompany.Checked = true;
                rbtn_ReportingCategory.Checked = false;
                pnlInsuranceCompany.Enabled = false;
                chkPhone.Checked = false;
                chkPhone.Enabled = false;
                chkPatientDetail.Checked = false;
                chkPatientDetail.Enabled = false;

            }
            else
            {
                rbtn_AgingSummary.Font = regularFont;
               
                if (rbtn_InsurancePending.Checked == false)
                {
                    chkPatientDetail.Enabled = true;
                    if (chkPatientDetail.Checked == true)
                        chkPatientDetail.Checked = true;
                }
                chkPhone.Enabled = true;
                if(chkPhone.Checked==true)
                    chkPhone.Checked = true;
            }
        }

        private void rbtn_AgingDetails_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtn_AgingDetails.Checked == true)
            {
                rbtn_AgingDetails.Font = boldFont;
                if (rbtn_InsurancePending.Checked == true)
                {
                    pnlInsuranceCompany.Enabled = true;
                    //pnlReportingCategory.Enabled = true;
                    grInsurance.Enabled = true;
                    rbtn_InsCompany.Checked = true;
                    pnlInsuranceCompany.Visible = true;
                    pnlReportingCategory.Visible = false;
                    chkPhone.Checked = false;
                    chkPhone.Enabled = false;
                    chkPatientDetail.Checked = false;
                    chkPatientDetail.Enabled = false;
                }
                else
                {
                    rbtn_ReportingCategory.Checked = false;
                    rbtn_InsCompany.Checked = true;
                    pnlInsuranceCompany.Visible = true;
                    pnlReportingCategory.Visible = false;
                    pnlInsuranceCompany.Enabled = false;
                    pnlReportingCategory.Enabled = false;
                    grInsurance.Enabled = false;
                    chkPhone.Enabled = true;

                    if (chkPhone.Checked == true)
                        chkPhone.Checked = true;
                    
                    chkPatientDetail.Enabled = true;
                    if (chkPatientDetail.Checked == true)
                        chkPatientDetail.Checked = true;
                }
                //if (rbtn_Both.Checked == true)
                //    rbtn_InsurancePending.Checked = true;
                //rbtn_Both.Visible = false;

            }
            else
            {
                rbtn_AgingDetails.Font = regularFont;
                chkPhone.Checked = false;
                chkPhone.Enabled = false;
                chkPatientDetail.Checked = false;
                chkPatientDetail.Enabled = false;
            }
        }

        private void rbtn_InsurancePending_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtn_InsurancePending.Checked == true)
                rbtn_InsurancePending.Font = boldFont;
            else
                rbtn_InsurancePending.Font = regularFont;

            if (rbtn_AgingDetails.Checked == true)
            {
                grInsurance.Enabled = true;
                pnlInsuranceCompany.Enabled = true;
                pnlReportingCategory.Enabled = true;
                rbtn_InsCompany.Checked = true;
                pnlInsuranceCompany.Enabled=true;
                pnlReportingCategory.Enabled = false;
                pnlInsuranceCompany.Visible = true;
                pnlReportingCategory.Visible = false;
               
            }
            chkPhone.Checked = false;
            chkPhone.Enabled = false;
            chkPatientDetail.Checked = false;
            chkPatientDetail.Enabled = false;


           
        }

        private void rbtn_PatientDue_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtn_PatientDue.Checked == true)
            {
                rbtn_PatientDue.Font = boldFont;
                grInsurance.Enabled = false;
                pnlInsuranceCompany.Enabled = false;
                pnlReportingCategory.Enabled = false;
                pnlReportingCategory.Visible = false;
                pnlInsuranceCompany.Visible = true;
                rbtn_ReportingCategory.Checked = false;
                rbtn_InsCompany.Checked = true;
                if (rbtn_AgingDetails.Checked == true)
                {
                    chkPhone.Enabled = true;
                    if (chkPhone.Checked == true)
                        chkPhone.Checked = true;
                    chkPatientDetail.Enabled = true;
                    if (chkPatientDetail.Checked == true)
                        chkPatientDetail.Checked = true;
                }
                else
                {
                    chkPhone.Checked = false;
                    chkPhone.Enabled = false;
                    chkPatientDetail.Checked = false;
                    chkPatientDetail.Enabled = false;
                }
            }
            else
            {
                rbtn_PatientDue.Font = regularFont;
                
            }
        }

        private void rbtn_Both_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtn_Both.Checked == true)
            {
                rbtn_Both.Font = boldFont;

                grInsurance.Enabled = false;
                pnlInsuranceCompany.Enabled = false;
                pnlReportingCategory.Enabled = false;
                pnlReportingCategory.Visible = false;
                pnlInsuranceCompany.Visible = true;
                rbtn_ReportingCategory.Checked = false;

                if (rbtn_AgingDetails.Checked == true)
                {
                    chkPhone.Enabled = true;
                    if (chkPhone.Checked == true)
                        chkPhone.Checked = true;
                    chkPatientDetail.Enabled = true;
                    if (chkPatientDetail.Checked == true)
                        chkPatientDetail.Checked = true;
                }
                else
                {
                    chkPhone.Checked = false;
                    chkPhone.Enabled = false;
                    chkPatientDetail.Checked = false;
                    chkPatientDetail.Enabled = false;
                }
            }
            else
            {
                rbtn_Both.Font = regularFont;
              
            }

           
        }

        private void rbtn_InsCompany_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtn_InsCompany.Checked == true)
            {
                rbtn_InsCompany.Font = boldFont;
                pnlInsuranceCompany.Visible = true;
                pnlReportingCategory.Visible = false;
                pnlInsuranceCompany.Enabled = true;
                pnlReportingCategory.Enabled = false;

            }
            else
                rbtn_InsCompany.Font = regularFont;
          
         
        }

        private void rbtn_DateOfService_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtn_DateOfService.Checked == true)
                rbtn_DateOfService.Font = boldFont;
            else
                rbtn_DateOfService.Font = regularFont;

            
        }

        private void rbtn_ReportingCategory_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtn_ReportingCategory.Checked == true)
            {
                rbtn_ReportingCategory.Font = boldFont;
                pnlInsuranceCompany.Visible = false;
                pnlReportingCategory.Visible = true;
                pnlInsuranceCompany.Enabled = false;
                pnlReportingCategory.Enabled = true;
            }
            else
                rbtn_ReportingCategory.Font = regularFont;
        }

        private void frmRpt_AgingReport_Load(object sender, EventArgs e)
        {

            tsb_btnExportReport.Visible = false;
            btnUP.BackgroundImage = global::gloReports.Properties.Resources.UP;
            btnUP.BackgroundImageLayout = ImageLayout.Center;
            btnDown.Visible = false;

            grInsurance.Enabled = false;
            pnlInsuranceCompany.Enabled = false;
            pnlReportingCategory.Enabled = false;
            pnlReportingCategory.Visible = false;
            //dtpEndDate_ValueChanged(sender, e);
           // Application.DoEvents();
           // #region "Wait Process"
           // if (pnlReportViewer.Controls.Contains(oProcessLabel) == true) { pnlReportViewer.Controls.Remove(oProcessLabel); }
           // oProcessLabel = new System.Windows.Forms.Label();
           // pnlReportViewer.Controls.Add(oProcessLabel);
           // oProcessLabel.Dock = DockStyle.Fill;
           // oProcessLabel.Location = new System.Drawing.Point(0, 0);
           // oProcessLabel.ForeColor = Color.Blue;
           // oProcessLabel.Font = new System.Drawing.Font("Verdana", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
           // oProcessLabel.TextAlign = ContentAlignment.MiddleCenter;
           // oProcessLabel.Text = "Loading";
           // oProcessLabel.BringToFront();
           // oProcessLabel.Visible = true;
           // #endregion
           // Application.DoEvents();

           // timer1.Tick += new EventHandler(timer1_Tick);
           //// timer1.Stop();
           // timer1.Interval = 700;
           // timer1.Enabled = true;

            string strCloseDate = "";

            strCloseDate = getCloseDate();
            if (strCloseDate == "")
            {
  
          chkUncloseAct.Visible = true;
                chkUncloseAct.Enabled = false;
                chkUncloseAct.Checked = true;
            }
            else
            {
                dtpEndDate.Text = strCloseDate;
            }
            FillBreakByType();
            //Application.DoEvents();
            
            //FillAgingReport();
            //timer1.Enabled = false;
            //oProcessLabel.Visible = false;
        }

        //private void ReportDispose()
        //{

        //    if (oRptAgingSummaryInsurancePending1.Equals(null))
        //    {
        //        oRptAgingSummaryInsurancePending1.Close();
        //        oRptAgingSummaryInsurancePending1.Dispose();
        //    }
        //    if (oRpt_AgingSummaryPatAndInsurance.Equals(null))
        //    {
        //        oRpt_AgingSummaryPatAndInsurance.Close();
        //        oRpt_AgingSummaryPatAndInsurance.Dispose();
        //    }
               

        //}
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
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
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
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
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
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
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

                case gloListControl.gloListControlType.InsuranceCompany:
                    {
                       
                        cmbInsuranceCompany.DataSource = null;
                        cmbInsuranceCompany.Items.Clear();
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

                            cmbInsuranceCompany.DataSource = oBindTable;
                            cmbInsuranceCompany.DisplayMember = "DispName";
                            cmbInsuranceCompany.ValueMember = "ID";
                        }

                    }
                    break;

                case gloListControl.gloListControlType.InsuranceReportingCategory:
                    {
                        
                        cmbReportingCategory.DataSource = null;
                        cmbReportingCategory.Items.Clear();
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

                            cmbReportingCategory.DataSource = oBindTable;
                            cmbReportingCategory.DisplayMember = "DispName";
                            cmbReportingCategory.ValueMember = "ID";
                        }

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


        #region "Function"

     
     
        private void FillAgingReport()
        {
            String clinicName = getClinicName();

            Decimal dBalance = 0;
            Int32 iAgingDays = 0;

            if (txtPatBal.Text != "")
                dBalance = Convert.ToDecimal(txtPatBal.Text);

            if (txtAgingDays.Text != "")
                iAgingDays = Convert.ToInt32(txtAgingDays.Text);
               
            crvReportViewer.Visible = true;
            _reportDate = "";

            String closedate = getCloseDate();
            if (closedate != string.Empty)
            {
                if (chkUncloseAct.Visible == true)
                {
                    if (chkUncloseAct.Checked == true)
                        _reportDate = dtpEndDate.Text;
                    else
                        _reportDate = closedate;
                }
                else
                    _reportDate = dtpEndDate.Text;
            }
            else
                _reportDate = dtpEndDate.Text;
            string sCalculationType = string.Empty;
           
             dsAgingReport _dsReport =new dsAgingReport();
             SqlCommand _sqlCommand = new SqlCommand();
             SqlConnection _sqlConnection = new SqlConnection(_databaseconnectionstring);
             try
             {
                 _sqlCommand.CommandType = CommandType.StoredProcedure;
                 _sqlCommand.CommandText = "rpt_Aging_Report";
                 _sqlCommand.Connection = _sqlConnection;
                 _sqlCommand.CommandTimeout = 0;

                 String sProvider = string.Empty;
                 String sFacility = string.Empty;
                 string sAgingType = string.Empty;

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

                 if (rbtn_DateOfService.Checked == true)
                 {
                     sAgingType = "S";
                     sCalculationType = "Date of Service";
                 }
                 else if (rbtn_DateOfResponsibility.Checked == true)
                 {
                     sAgingType = "R";
                     sCalculationType = "Date of Responsibility";
                 }

                 if (rbtn_AgingSummary.Checked == true)
                 {
                     if (rbtn_InsurancePending.Checked == true)
                     {
                         _sqlCommand.Parameters.Add("@ReportFlag", SqlDbType.Int).Value = 1;
                         _sqlCommand.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = Convert.ToDateTime(_reportDate);
                         if (sProvider != string.Empty)
                             _sqlCommand.Parameters.Add("@Providers", SqlDbType.VarChar).Value = sProvider;
                         else
                             _sqlCommand.Parameters.Add("@Providers", SqlDbType.VarChar).Value = DBNull.Value;
                         if (sFacility != string.Empty)
                             _sqlCommand.Parameters.Add("@Facilities", SqlDbType.VarChar).Value = sFacility;
                         else
                             _sqlCommand.Parameters.Add("@Facilities", SqlDbType.VarChar).Value = DBNull.Value;
                         _sqlCommand.Parameters.Add("@InsuranceCompanies", SqlDbType.VarChar).Value = DBNull.Value;
                         _sqlCommand.Parameters.Add("@ReportingCategories", SqlDbType.VarChar).Value = DBNull.Value;
                         _sqlCommand.Parameters.Add("@AgingType", SqlDbType.Char).Value = sAgingType;
                         _sqlCommand.Parameters.Add("@Balance", SqlDbType.Decimal).Value = dBalance;
                         _sqlCommand.Parameters.Add("@AgingDays", SqlDbType.Int).Value = iAgingDays;
                       

                         SqlDataAdapter da = new SqlDataAdapter(_sqlCommand);
                     
                         da.Fill(_dsReport, "dt_AgingSummary_Report");
                         da.Dispose();
                         _sqlCommand.Parameters.Clear();
                        
                         //oRptAgingSummaryInsurancePending1=new Rpt_AgingSummary();
                         //oRptAgingSummaryInsurancePending1.SetDataSource(_dsReport);
                         //crvReportViewer.ReportSource = oRptAgingSummaryInsurancePending1;



                         if (cmbBreakBy.SelectedIndex==Convert.ToInt32(breakByType.None))
                         {
                             //if (!(object.ReferenceEquals(oRptAgingSummaryInsurancePending1, null)))
                             //{
                             //    oRptAgingSummaryInsurancePending1.Close();
                             //    oRptAgingSummaryInsurancePending1.Dispose();
                             //}
                             //oRptAgingSummaryInsurancePending1 = new Rpt_AgingSummary();

                             DataTable dtReportdata = dataforParameter(_dsReport.Tables["dt_AgingSummary_Report"], "I"); 
                             //passing parametres

                             TextObject fUserNumber = (TextObject)oRptAgingSummaryInsurancePending1.Section4.ReportObjects["Text9"];
                             fUserNumber.Text = "Insurance";
                            
                             foreach (DataRow orow in  dtReportdata.Rows)
                             {
                             _dsReport.Tables["dt_AgingSummary_InsPending"].ImportRow(orow);
                             }
                             dtReportdata.Dispose();
                             dtReportdata = null;
                             //------------Report parametres--------------------------


                             passParametres(oRptAgingSummaryInsurancePending1, sCalculationType, "Aging Summary", "Insurance Pending", "None", clinicName, _dsReport, "N");
                             oRptAgingSummaryInsurancePending1.SetDataSource(_dsReport);
                             crvReportViewer.ReportSource = oRptAgingSummaryInsurancePending1;
                             
                             
                             
                         }

                         else if (cmbBreakBy.SelectedIndex == Convert.ToInt32(breakByType.Facility))
                         {
                          //   if (!(object.ReferenceEquals(oRpt_AgingSummary_PatientFacility, null)))
                          //   {
                          //       oRpt_AgingSummary_PatientFacility.Close();
                          //       oRpt_AgingSummary_PatientFacility.Dispose();
                          //   }
                          //oRpt_AgingSummary_PatientFacility = new Rpt_AgingSummary_PatientFacility();

                             DataTable dtReportdata = dataforParameter(_dsReport.Tables["dt_AgingSummary_Report"], "I"); 
                             //passing parametres

                             TextObject fUserNumber = (TextObject)oRpt_AgingSummary_PatientFacility.GroupFooterSection1.ReportObjects["Text9"];
                             fUserNumber.Text = "Insurance";
                            
                             foreach (DataRow orow in  dtReportdata.Rows)
                             {
                             _dsReport.Tables["dt_AgingSummary_InsPending"].ImportRow(orow);
                             }
                             dtReportdata.Dispose();
                             dtReportdata = null;
                             //------------Report parametres--------------------------


                             passParametres(oRpt_AgingSummary_PatientFacility, sCalculationType, "Aging Summary", "Insurance Pending", "Facility", clinicName, _dsReport, "N");
                             oRpt_AgingSummary_PatientFacility.SetDataSource(_dsReport);
                             crvReportViewer.ReportSource = oRpt_AgingSummary_PatientFacility;
                            
                      
                         }
                         else if (cmbBreakBy.SelectedIndex == Convert.ToInt32(breakByType.Provider))
                         {
                             //if (!(object.ReferenceEquals(oRpt_AgingSummary_PatientProvider, null)))
                             //{
                             //    oRpt_AgingSummary_PatientProvider.Close();
                             //    oRpt_AgingSummary_PatientProvider.Dispose();
                             //}
                             //oRpt_AgingSummary_PatientProvider = new Rpt_AgingSummary_PatientProvider();

                             DataTable dtReportdata = dataforParameter(_dsReport.Tables["dt_AgingSummary_Report"], "I"); 
                             //passing parametres

                             TextObject fUserNumber = (TextObject)oRpt_AgingSummary_PatientProvider.GroupFooterSection1.ReportObjects["Text9"];
                             fUserNumber.Text = "Insurance";
                            
                             foreach (DataRow orow in  dtReportdata.Rows)
                             {
                             _dsReport.Tables["dt_AgingSummary_InsPending"].ImportRow(orow);
                             }
                             dtReportdata.Dispose();
                             dtReportdata = null;
                             passParametres(oRpt_AgingSummary_PatientProvider, sCalculationType, "Aging Summary", "Insurance Pending", "Provider", clinicName, _dsReport, "N");
                             oRpt_AgingSummary_PatientProvider.SetDataSource(_dsReport);
                             crvReportViewer.ReportSource = oRpt_AgingSummary_PatientProvider;
                             //------------Report parametres--------------------------

                             

                         
                         }
                         else if (cmbBreakBy.SelectedIndex == Convert.ToInt32(breakByType.FacilityProvider ))
                         {
                             //if (!(object.ReferenceEquals(oRpt_AgingSummary_InsFacilityProvider, null)))
                             //{
                             //    oRpt_AgingSummary_InsFacilityProvider.Close();
                             //    oRpt_AgingSummary_InsFacilityProvider.Dispose();
                             //}
                             //oRpt_AgingSummary_InsFacilityProvider = new Rpt_AgingSummary_InsFacilityProvider_();
                             DataTable dtReportdata = dataforParameter(_dsReport.Tables["dt_AgingSummary_Report"], "I");
                             //passing parametres
                             TextObject fUserNumber = (TextObject)oRpt_AgingSummary_InsFacilityProvider.GroupFooterSection2.ReportObjects["Text16"];
                             fUserNumber.Text = "Insurance";
                         
                             foreach (DataRow orow in dtReportdata.Rows)
                             {
                                 _dsReport.Tables["dt_AgingSummary_InsPending"].ImportRow(orow);
                             }
                             dtReportdata.Dispose();
                             dtReportdata = null;
                             passParametres(oRpt_AgingSummary_InsFacilityProvider, sCalculationType, "Aging Summary", "Insurance Pending", "Facility,Provider", clinicName, _dsReport,"N");
                             oRpt_AgingSummary_InsFacilityProvider.SetDataSource(_dsReport);
                             crvReportViewer.ReportSource = oRpt_AgingSummary_InsFacilityProvider;
                             
                             //------------Report parametres--------------------------

                             
                             
                         }

                         else if (cmbBreakBy.SelectedIndex == Convert.ToInt32(breakByType.ProviderFacility))
                         {
                             //if (!(object.ReferenceEquals(oRpt_AgingSummary_InsProviderFacility, null)))
                             //{
                             //    oRpt_AgingSummary_InsProviderFacility.Close();
                             //    oRpt_AgingSummary_InsProviderFacility.Dispose();
                             //}
                             //oRpt_AgingSummary_InsProviderFacility = new Rpt_AgingSummary_InsProviderFacility_();
                             DataTable dtReportdata = dataforParameter(_dsReport.Tables["dt_AgingSummary_Report"], "I");
                             //passing parametres

                             TextObject fUserNumber = (TextObject)oRpt_AgingSummary_InsProviderFacility.GroupFooterSection1.ReportObjects["Text16"];
                             fUserNumber.Text = "Insurance";
                         
                             foreach (DataRow orow in dtReportdata.Rows)
                             {
                                 _dsReport.Tables["dt_AgingSummary_InsPending"].ImportRow(orow);
                             }
                             dtReportdata.Dispose();
                             dtReportdata = null;
                             passParametres(oRpt_AgingSummary_InsProviderFacility, sCalculationType, "Aging Summary", "Insurance Pending", "Provider,Facility", clinicName, _dsReport, "N");
                             oRpt_AgingSummary_InsProviderFacility.SetDataSource(_dsReport);
                             crvReportViewer.ReportSource = oRpt_AgingSummary_InsProviderFacility;

                             //------------Report parametres--------------------------

                             
                         }

                       
                     }




                     else if (rbtn_PatientDue.Checked == true)
                     {
                         _sqlCommand.Parameters.Add("@ReportFlag", SqlDbType.Int).Value = 2;
                         _sqlCommand.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = Convert.ToDateTime(_reportDate);
                         if (sProvider != string.Empty)
                             _sqlCommand.Parameters.Add("@Providers", SqlDbType.VarChar).Value = sProvider;
                         else
                             _sqlCommand.Parameters.Add("@Providers", SqlDbType.VarChar).Value = DBNull.Value;
                         if (sFacility != string.Empty)
                             _sqlCommand.Parameters.Add("@Facilities", SqlDbType.VarChar).Value = sFacility;
                         else
                             _sqlCommand.Parameters.Add("@Facilities", SqlDbType.VarChar).Value = DBNull.Value;
                         _sqlCommand.Parameters.Add("@InsuranceCompanies", SqlDbType.VarChar).Value = DBNull.Value;
                         _sqlCommand.Parameters.Add("@ReportingCategories", SqlDbType.VarChar).Value = DBNull.Value;
                        _sqlCommand.Parameters.Add("@AgingType", SqlDbType.Char).Value = sAgingType;
                        _sqlCommand.Parameters.Add("@Balance", SqlDbType.Decimal).Value = dBalance;
                        _sqlCommand.Parameters.Add("@AgingDays", SqlDbType.Int).Value = iAgingDays;

                         SqlDataAdapter da = new SqlDataAdapter(_sqlCommand);
                         da.Fill(_dsReport, "dt_AgingSummary_Report");
                         da.Dispose();
                       
                         //oRptAgingSummaryInsurancePending1 = new Rpt_AgingSummary();
                         //oRptAgingSummaryInsurancePending1.Refresh();
                        
                         //oRptAgingSummaryInsurancePending1.SetDataSource(_dsReport);
                         //crvReportViewer.ReportSource = oRptAgingSummaryInsurancePending1;

                         if (cmbBreakBy.SelectedIndex == Convert.ToInt32(breakByType.None))
                         {
                             //if (!(object.ReferenceEquals(oRptAgingSummaryInsurancePending1, null)))
                             //{
                             //    oRptAgingSummaryInsurancePending1.Close();
                             //    oRptAgingSummaryInsurancePending1.Dispose();
                             //}
                             //oRptAgingSummaryInsurancePending1 = new Rpt_AgingSummary();

                             DataTable dtReportdata = dataforParameter(_dsReport.Tables["dt_AgingSummary_Report"], "P");
                             //passing parametres

                             TextObject fUserNumber = (TextObject)oRptAgingSummaryInsurancePending1.Section4.ReportObjects["Text9"];
                             fUserNumber.Text = "Patient";

                             foreach (DataRow orow in dtReportdata.Rows)
                             {
                                 _dsReport.Tables["dt_AgingSummary_InsPending"].ImportRow(orow);
                             }

                             dtReportdata.Dispose();
                             dtReportdata = null;
                             passParametres(oRptAgingSummaryInsurancePending1, sCalculationType, "Aging Summary", "Patient Due", "None", clinicName, _dsReport, "N");
                             oRptAgingSummaryInsurancePending1.SetDataSource(_dsReport);
                             crvReportViewer.ReportSource = oRptAgingSummaryInsurancePending1;
                            

                         }
                         else if (cmbBreakBy.SelectedIndex == Convert.ToInt32(breakByType.Facility))
                         {
                             //if (!(object.ReferenceEquals(oRpt_AgingSummary_PatientFacility, null)))
                             //{
                             //    oRpt_AgingSummary_PatientFacility.Close();
                             //    oRpt_AgingSummary_PatientFacility.Dispose();
                             //}
                             //oRpt_AgingSummary_PatientFacility = new Rpt_AgingSummary_PatientFacility();

                             DataTable dtReportdata = dataforParameter(_dsReport.Tables["dt_AgingSummary_Report"], "P");
                             //passing parametres

                             TextObject fUserNumber = (TextObject)oRpt_AgingSummary_PatientFacility.GroupFooterSection1.ReportObjects["Text9"];
                             fUserNumber.Text = "Patient";

                             foreach (DataRow orow in dtReportdata.Rows)
                             {
                                 _dsReport.Tables["dt_AgingSummary_InsPending"].ImportRow(orow);
                             }
                             dtReportdata.Dispose();
                             dtReportdata = null;
                             passParametres(oRpt_AgingSummary_PatientFacility, sCalculationType, "Aging Summary", "Patient Due", "Facility", clinicName, _dsReport,"N");
                             oRpt_AgingSummary_PatientFacility.SetDataSource(_dsReport);
                             crvReportViewer.ReportSource = oRpt_AgingSummary_PatientFacility;
                            
                   

                         }
                         else if (cmbBreakBy.SelectedIndex == Convert.ToInt32(breakByType.Provider))
                         {
                             //if (!(object.ReferenceEquals(oRpt_AgingSummary_PatientProvider, null)))
                             //{
                             //    oRpt_AgingSummary_PatientProvider.Close();
                             //    oRpt_AgingSummary_PatientProvider.Dispose();
                             //}
                             //oRpt_AgingSummary_PatientProvider = new Rpt_AgingSummary_PatientProvider();

                             DataTable dtReportdata = dataforParameter(_dsReport.Tables["dt_AgingSummary_Report"], "P");
                             //passing parametres

                             TextObject fUserNumber = (TextObject)oRpt_AgingSummary_PatientProvider.GroupFooterSection1.ReportObjects["Text9"];
                             fUserNumber.Text = "Patient";

                             foreach (DataRow orow in dtReportdata.Rows)
                             {
                                 _dsReport.Tables["dt_AgingSummary_InsPending"].ImportRow(orow);
                             }
                             dtReportdata.Dispose();
                             dtReportdata = null;
                             passParametres(oRpt_AgingSummary_PatientProvider, sCalculationType, "Aging Summary", "Patient Due", "Provider", clinicName, _dsReport, "N");
                             oRpt_AgingSummary_PatientProvider.SetDataSource(_dsReport);
                             crvReportViewer.ReportSource = oRpt_AgingSummary_PatientProvider;
                             
                         }
                         else if (cmbBreakBy.SelectedIndex == Convert.ToInt32(breakByType.FacilityProvider))
                         {
                             //if (!(object.ReferenceEquals(oRpt_AgingSummary_InsFacilityProvider, null)))
                             //{
                             //    oRpt_AgingSummary_InsFacilityProvider.Close();
                             //    oRpt_AgingSummary_InsFacilityProvider.Dispose();
                             //}
                             //oRpt_AgingSummary_InsFacilityProvider = new Rpt_AgingSummary_InsFacilityProvider_();
                             DataTable dtReportdata = dataforParameter(_dsReport.Tables["dt_AgingSummary_Report"], "P");
                             //passing parametres

                             TextObject fUserNumber = (TextObject)oRpt_AgingSummary_InsFacilityProvider.GroupFooterSection2.ReportObjects["Text16"];
                             fUserNumber.Text = "Patient";

                             foreach (DataRow orow in dtReportdata.Rows)
                             {
                                 _dsReport.Tables["dt_AgingSummary_InsPending"].ImportRow(orow);
                             }
                             dtReportdata.Dispose();
                             dtReportdata = null;

                             passParametres(oRpt_AgingSummary_InsFacilityProvider, sCalculationType, "Aging Summary", "Patient Due", "Facility,Provider", clinicName, _dsReport, "N");
                             oRpt_AgingSummary_InsFacilityProvider.SetDataSource(_dsReport);
                             crvReportViewer.ReportSource = oRpt_AgingSummary_InsFacilityProvider;

                          
                            
                         
                         }
                         else if (cmbBreakBy.SelectedIndex == Convert.ToInt32(breakByType.ProviderFacility))
                         {
                             //if (!(object.ReferenceEquals(oRpt_AgingSummary_InsProviderFacility, null)))
                             //{
                             //    oRpt_AgingSummary_InsProviderFacility.Close();
                             //    oRpt_AgingSummary_InsProviderFacility.Dispose();
                             //}
                             //oRpt_AgingSummary_InsProviderFacility = new Rpt_AgingSummary_InsProviderFacility_();
                             DataTable dtReportdata = dataforParameter(_dsReport.Tables["dt_AgingSummary_Report"], "P");
                             //passing parametres

                             TextObject fUserNumber = (TextObject)oRpt_AgingSummary_InsProviderFacility.GroupFooterSection1.ReportObjects["Text16"];
                             fUserNumber.Text = "Patient";

                             foreach (DataRow orow in dtReportdata.Rows)
                             {
                                 _dsReport.Tables["dt_AgingSummary_InsPending"].ImportRow(orow);
                             }
                             dtReportdata.Dispose();
                             dtReportdata = null;

                             passParametres(oRpt_AgingSummary_InsProviderFacility, sCalculationType, "Aging Summary", "Patient Due", "Provider,Facility", clinicName, _dsReport, "N");
                             oRpt_AgingSummary_InsProviderFacility.SetDataSource(_dsReport);
                             crvReportViewer.ReportSource = oRpt_AgingSummary_InsProviderFacility;

                            

                            
                       
                         }
                      

                     }
                     else if (rbtn_Both.Checked == true)
                     {
                         _sqlCommand.Parameters.Add("@ReportFlag", SqlDbType.Int).Value = 3;
                         _sqlCommand.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = Convert.ToDateTime(_reportDate);
                         if (sProvider != string.Empty)
                             _sqlCommand.Parameters.Add("@Providers", SqlDbType.VarChar).Value = sProvider;
                         else
                             _sqlCommand.Parameters.Add("@Providers", SqlDbType.VarChar).Value = DBNull.Value;
                         if (sFacility != string.Empty)
                             _sqlCommand.Parameters.Add("@Facilities", SqlDbType.VarChar).Value = sFacility;
                         else
                             _sqlCommand.Parameters.Add("@Facilities", SqlDbType.VarChar).Value = DBNull.Value;
                         _sqlCommand.Parameters.Add("@InsuranceCompanies", SqlDbType.VarChar).Value = DBNull.Value;
                         _sqlCommand.Parameters.Add("@ReportingCategories", SqlDbType.VarChar).Value = DBNull.Value;
                       _sqlCommand.Parameters.Add("@AgingType", SqlDbType.Char).Value = sAgingType;
                       _sqlCommand.Parameters.Add("@Balance", SqlDbType.Decimal).Value = dBalance;
                       _sqlCommand.Parameters.Add("@AgingDays", SqlDbType.Int).Value = iAgingDays;

                         SqlDataAdapter da = new SqlDataAdapter(_sqlCommand);
                         da.Fill(_dsReport, "dt_AgingSummary_Report");
                         da.Dispose();
                       
                         //oRpt_AgingSummaryPatAndInsurance = new rpt_AgingSummaryPatAndInsurance();

                         //oRpt_AgingSummaryPatAndInsurance.SetDataSource(_dsReport);
                         //crvReportViewer.ReportSource = oRpt_AgingSummaryPatAndInsurance;
                         //oRpt_AgingSummaryPatAndInsurance.SetParameterValue(13, "B");
                         if (cmbBreakBy.SelectedIndex == Convert.ToInt32(breakByType.None))
                         {
                             //if (!(object.ReferenceEquals(orpt_AgingSummaryPatAndInsNone, null)))
                             //{
                             //    orpt_AgingSummaryPatAndInsNone.Close();
                             //    orpt_AgingSummaryPatAndInsNone.Dispose();
                             //}
                             //orpt_AgingSummaryPatAndInsNone = new rpt_AgingSummaryPatAndInsNone();
                             DataTable dtReportdata = dataforParameter(_dsReport.Tables["dt_AgingSummary_Report"], "B");
                             //passing parametres
                             //if (dtReportdata.Rows.Count > 0)
                             //{
                                 foreach (DataRow orow in dtReportdata.Rows)
                                 {
                                     _dsReport.Tables["dt_AgingSummary_InsPending"].ImportRow(orow);
                                 }
                                 dtReportdata.Dispose();
                                 dtReportdata = null;

                                 passParametres(orpt_AgingSummaryPatAndInsNone, sCalculationType, "Aging Summary", "Both", "None", clinicName, _dsReport, "N");
                                 orpt_AgingSummaryPatAndInsNone.SetDataSource(_dsReport);
                                 crvReportViewer.ReportSource = orpt_AgingSummaryPatAndInsNone;
                                

                                 //FieldObject fgroup = (FieldObject)oRpt_AgingSummaryPatAndInsurance.GroupHeaderSection3.ReportObjects["GroupNameFacility2"];
                                 //fgroup.Left = 620;
                                 //fgroup.Width = 10840;
                             //}
                         }
                         else if (cmbBreakBy.SelectedIndex == Convert.ToInt32(breakByType.Facility))
                         {
                             //if (!(object.ReferenceEquals(orpt_AgingSummaryPatAndInsFacility, null)))
                             //{
                             //    orpt_AgingSummaryPatAndInsFacility.Close();
                             //    orpt_AgingSummaryPatAndInsFacility.Dispose();
                             //}
                             //orpt_AgingSummaryPatAndInsFacility = new rpt_AgingSummaryPatAndInsFacility();
                             DataTable dtReportdata = dataforParameter(_dsReport.Tables["dt_AgingSummary_Report"], "B");
                             //passing parametres
                             //if (dtReportdata.Rows.Count > 0)
                             //{
                                 foreach (DataRow orow in dtReportdata.Rows)
                                 {
                                     _dsReport.Tables["dt_AgingSummary_InsPending"].ImportRow(orow);
                                 }
                                 dtReportdata.Dispose();
                                 dtReportdata = null;
                                 passParametres(orpt_AgingSummaryPatAndInsFacility, sCalculationType, "Aging Summary", "Both", "Facility", clinicName, _dsReport, "N");
                                 orpt_AgingSummaryPatAndInsFacility.SetDataSource(_dsReport);
                                 crvReportViewer.ReportSource = orpt_AgingSummaryPatAndInsFacility;

                                 

                                 //FieldObject fgroup = (FieldObject)oRpt_AgingSummaryPatAndInsurance.GroupHeaderSection3.ReportObjects["GroupNameFacility2"];
                                 //fgroup.Left = 620;
                                 //fgroup.Width = 10840;
                             //}

                         }
                         else if (cmbBreakBy.SelectedIndex == Convert.ToInt32(breakByType.Provider))
                         {

                             //if (!(object.ReferenceEquals(orpt_AgingSummaryPatAndInsProvider, null)))
                             //{
                             //    orpt_AgingSummaryPatAndInsProvider.Close();
                             //    orpt_AgingSummaryPatAndInsProvider.Dispose();
                             //}
                             //orpt_AgingSummaryPatAndInsProvider = new rpt_AgingSummaryPatAndInsProvider();
                             DataTable dtReportdata = dataforParameter(_dsReport.Tables["dt_AgingSummary_Report"], "B");
                             //passing parametres
                             //if (dtReportdata.Rows.Count > 0)
                             //{
                                 foreach (DataRow orow in dtReportdata.Rows)
                                 {
                                     _dsReport.Tables["dt_AgingSummary_InsPending"].ImportRow(orow);
                                 }
                                 dtReportdata.Dispose();
                                 dtReportdata = null;
                                 passParametres(orpt_AgingSummaryPatAndInsProvider, sCalculationType, "Aging Summary", "Both", "Provider", clinicName, _dsReport, "N");

                                 orpt_AgingSummaryPatAndInsProvider.SetDataSource(_dsReport);
                                 crvReportViewer.ReportSource = orpt_AgingSummaryPatAndInsProvider;

                                
                                 //FieldObject fgroup = (FieldObject)oRpt_AgingSummaryPatAndInsurance.GroupHeaderSection3.ReportObjects["GroupNameFacility2"];
                                 //fgroup.Left = 620;
                                 //fgroup.Width = 10840;
                             //}
                         }
                         else if (cmbBreakBy.SelectedIndex == Convert.ToInt32(breakByType.FacilityProvider))
                         {

                             //if (!(object.ReferenceEquals(oRpt_AgingSummaryPatAndInsFacilityProvider, null)))
                             //{
                             //    oRpt_AgingSummaryPatAndInsFacilityProvider.Close();
                             //    oRpt_AgingSummaryPatAndInsFacilityProvider.Dispose();
                             //}
                             //oRpt_AgingSummaryPatAndInsFacilityProvider = new rpt_AgingSummaryPatAndInsFacilityProvider_();
                             DataTable dtReportdata = dataforParameter(_dsReport.Tables["dt_AgingSummary_Report"], "B");
                             //passing parametres
                             //if (dtReportdata.Rows.Count > 0)
                             //{
                                 foreach (DataRow orow in dtReportdata.Rows)
                                 {
                                     _dsReport.Tables["dt_AgingSummary_InsPending"].ImportRow(orow);
                                 }
                                 dtReportdata.Dispose();
                                 dtReportdata = null;

                                 passParametres(oRpt_AgingSummaryPatAndInsFacilityProvider, sCalculationType, "Aging Summary", "Both", "Facility,Provider", clinicName, _dsReport, "N");
                                 oRpt_AgingSummaryPatAndInsFacilityProvider.SetDataSource(_dsReport);
                                 crvReportViewer.ReportSource = oRpt_AgingSummaryPatAndInsFacilityProvider;

                                
                                 //FieldObject fgroup = (FieldObject)oRpt_AgingSummaryPatAndInsurance.GroupHeaderSection3.ReportObjects["GroupNameFacility2"];
                                 //fgroup.Left = 620;
                                 //fgroup.Width = 10840;
                             //}
                         }
                         else if (cmbBreakBy.SelectedIndex == Convert.ToInt32(breakByType.ProviderFacility))
                         {

                             //if (!(object.ReferenceEquals(orpt_AgingSummaryPatAndInsProviderFacility, null)))
                             //{
                             //    orpt_AgingSummaryPatAndInsProviderFacility.Close();
                             //    orpt_AgingSummaryPatAndInsProviderFacility.Dispose();
                             //}
                             //orpt_AgingSummaryPatAndInsProviderFacility = new rpt_AgingSummaryPatAndInsProviderFacility_();
                             DataTable dtReportdata = dataforParameter(_dsReport.Tables["dt_AgingSummary_Report"], "B");
                             //passing parametres
                             //if (dtReportdata.Rows.Count > 0)
                             //{
                                 foreach (DataRow orow in dtReportdata.Rows)
                                 {
                                     _dsReport.Tables["dt_AgingSummary_InsPending"].ImportRow(orow);
                                 }
                                 dtReportdata.Dispose();
                                 dtReportdata = null;
                                 passParametres(orpt_AgingSummaryPatAndInsProviderFacility, sCalculationType, "Aging Summary", "Both", "Provider,Facility", clinicName, _dsReport, "N");

                                 orpt_AgingSummaryPatAndInsProviderFacility.SetDataSource(_dsReport);
                                 crvReportViewer.ReportSource = orpt_AgingSummaryPatAndInsProviderFacility;

                                
                                 //FieldObject fgroup = (FieldObject)oRpt_AgingSummaryPatAndInsurance.GroupHeaderSection3.ReportObjects["GroupNameFacility2"];
                                 //fgroup.Left = 620;
                                 //fgroup.Width = 10840;
                             //}

                            
                         }


                     }
                   
                 }

                     //----------------for Detail report ------------------------------

                 else if (rbtn_AgingDetails.Checked == true)
                 {
                     //String sProvider = string.Empty;
                     //String sFacility = string.Empty;
                     String sReportingCategory = string.Empty;
                     String sInsuranceCompany = string.Empty;
                   
                     if (cmbReportingCategory.Items.Count > 0)
                     {

                         for (int cntrCategory = 0; cntrCategory <= cmbReportingCategory.Items.Count - 1; cntrCategory++)
                         {
                             if (sReportingCategory == string.Empty)
                                 sReportingCategory = (Convert.ToString(((DataRowView)cmbReportingCategory.Items[cntrCategory])["ID"]));
                             else
                                 sReportingCategory = sReportingCategory + "," + (Convert.ToString(((DataRowView)cmbReportingCategory.Items[cntrCategory])["ID"]));
                         }
                     }
                     if (cmbInsuranceCompany.Items.Count > 0)
                     {

                         for (int cntrCompany = 0; cntrCompany <= cmbInsuranceCompany.Items.Count - 1; cntrCompany++)
                         {
                             if (sInsuranceCompany == string.Empty)
                                 sInsuranceCompany = (Convert.ToString(((DataRowView)cmbInsuranceCompany.Items[cntrCompany])["ID"]));
                             else
                                 sInsuranceCompany = sInsuranceCompany + "," + (Convert.ToString(((DataRowView)cmbInsuranceCompany.Items[cntrCompany])["ID"]));
                         }
                     }

                     if (rbtn_PatientDue.Checked == true)
                     {
                         _sqlCommand.Parameters.Add("@ReportFlag", SqlDbType.Int).Value = 2;
                         _sqlCommand.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = Convert.ToDateTime(_reportDate);
                         
                         if (sProvider != string.Empty)
                             _sqlCommand.Parameters.Add("@Providers", SqlDbType.VarChar).Value = sProvider;
                         else
                         _sqlCommand.Parameters.Add("@Providers", SqlDbType.VarChar).Value = DBNull.Value;
                     if (sFacility != string.Empty)
                         _sqlCommand.Parameters.Add("@Facilities", SqlDbType.VarChar).Value = sFacility;
                     else
                         _sqlCommand.Parameters.Add("@Facilities", SqlDbType.VarChar).Value = DBNull.Value;
                        
                         _sqlCommand.Parameters.Add("@InsuranceCompanies", SqlDbType.VarChar).Value = DBNull.Value;
                         _sqlCommand.Parameters.Add("@ReportingCategories", SqlDbType.VarChar).Value = DBNull.Value;
                         _sqlCommand.Parameters.Add("@AgingType", SqlDbType.Char).Value = sAgingType;
                         _sqlCommand.Parameters.Add("@Balance", SqlDbType.Decimal).Value = dBalance;
                         _sqlCommand.Parameters.Add("@AgingDays", SqlDbType.Int).Value = iAgingDays;


                         SqlDataAdapter da = new SqlDataAdapter(_sqlCommand);
                        
                         da.Fill(_dsReport, "dt_AgingDetail");
                         da.Dispose();
                         _sqlCommand.Parameters.Clear();


                         if (cmbBreakBy.SelectedIndex == Convert.ToInt32(breakByType.None))
                         {
                             //if (!(object.ReferenceEquals(oRpt_AgingDetailPatient, null)))
                             //{
                             //    oRpt_AgingDetailPatient.Close();
                             //    oRpt_AgingDetailPatient.Dispose();
                             //}
                             //oRpt_AgingDetailPatient = new Rpt_AgingDetail_();
                             passParametres(oRpt_AgingDetailPatient, sCalculationType, "Aging Detail", "Patient Due", "None", clinicName, _dsReport,(chkPhone.Checked==true?"Y":"N"));
                             oRpt_AgingDetailPatient.SetDataSource(_dsReport);
                                 crvReportViewer.ReportSource = oRpt_AgingDetailPatient;
                                
                             
                             // if (chkPhone.Checked == false)
                             //{
                             //    Boolean flag = phoneValidation(oRpt_AgingDetailPatient, true);
                          
                             //}
                             // else
                             // {
                             //     Boolean flag = phoneValidation(oRpt_AgingDetailPatient, false);

                             // }
                             Boolean flag1 = ReportTypeValidation(oRpt_AgingDetailPatient, true);

                         }
                         else if (cmbBreakBy.SelectedIndex == Convert.ToInt32(breakByType.Facility))
                         {
                             //if (!(object.ReferenceEquals(oRpt_AgingDetail_PatientFacility, null)))
                             //{
                             //    oRpt_AgingDetail_PatientFacility.Close();
                             //    oRpt_AgingDetail_PatientFacility.Dispose();
                             //}
                             //oRpt_AgingDetail_PatientFacility = new Rpt_AgingDetail_PatientFacility();

                             passParametres(oRpt_AgingDetail_PatientFacility, sCalculationType, "Aging Detail", "Patient Due", "Facility", clinicName, _dsReport,(chkPhone.Checked==true?"Y":"N"));

                             oRpt_AgingDetail_PatientFacility.SetDataSource(_dsReport);
                             crvReportViewer.ReportSource = oRpt_AgingDetail_PatientFacility;
                            
                             //if (chkPhone.Checked == false)
                             //{
                             //    Boolean flag = phoneValidation(oRpt_AgingDetail_PatientFacility, true);
                             //}
                             Boolean flag1 = ReportTypeValidation(oRpt_AgingDetail_PatientFacility, true);
                         }
                         else if (cmbBreakBy.SelectedIndex == Convert.ToInt32(breakByType.Provider))
                         {
                             //if (!(object.ReferenceEquals(oRpt_AgingDetail_PatientProvider, null)))
                             //{
                             //    oRpt_AgingDetail_PatientProvider.Close();
                             //    oRpt_AgingDetail_PatientProvider.Dispose();
                             //}
                             //oRpt_AgingDetail_PatientProvider = new Rpt_AgingDetail_PatientProvider();
                             passParametres(oRpt_AgingDetail_PatientProvider, sCalculationType, "Aging Detail", "Patient Due", "Provider", clinicName, _dsReport,(chkPhone.Checked==true?"Y":"N"));
                             oRpt_AgingDetail_PatientProvider.SetDataSource(_dsReport);
                             crvReportViewer.ReportSource = oRpt_AgingDetail_PatientProvider;
                             
                             //if (chkPhone.Checked == false)
                             //{
                             //    Boolean flag = phoneValidation(oRpt_AgingDetail_PatientProvider, true);
                             //}
                             Boolean flag1 = ReportTypeValidation(oRpt_AgingDetail_PatientProvider, true);
                         }
                         else if (cmbBreakBy.SelectedIndex == Convert.ToInt32(breakByType.FacilityProvider))
                         {
                             //if (!(object.ReferenceEquals(oRpt_AgingDetail_PatientFacilityProvider, null)))
                             //{
                             //    oRpt_AgingDetail_PatientFacilityProvider.Close();
                             //    oRpt_AgingDetail_PatientFacilityProvider.Dispose();
                             //}
                             //oRpt_AgingDetail_PatientFacilityProvider = new Rpt_AgingDetail_PatientFacilityProvider();
                             passParametres(oRpt_AgingDetail_PatientFacilityProvider, sCalculationType, "Aging Detail", "Patient Due", "Facility,Provider", clinicName, _dsReport,(chkPhone.Checked==true?"Y":"N"));
                             oRpt_AgingDetail_PatientFacilityProvider.SetDataSource(_dsReport);
                             crvReportViewer.ReportSource = oRpt_AgingDetail_PatientFacilityProvider;
                            
                             //if (chkPhone.Checked == false)
                             //{
                             //    Boolean flag = phoneValidation(oRpt_AgingDetail_PatientFacilityProvider, true);
                             //}
                             Boolean flag1 = ReportTypeValidation(oRpt_AgingDetail_PatientFacilityProvider, true);
                         }
                         else if (cmbBreakBy.SelectedIndex == Convert.ToInt32(breakByType.ProviderFacility))
                         {
                             //if (!(object.ReferenceEquals(oRpt_AgingDetail_PatientProviderFacility, null)))
                             //{
                             //    oRpt_AgingDetail_PatientProviderFacility.Close();
                             //    oRpt_AgingDetail_PatientProviderFacility.Dispose();
                             //}
                             //oRpt_AgingDetail_PatientProviderFacility = new Rpt_AgingDetail_PatientProviderFacility();
                             passParametres(oRpt_AgingDetail_PatientProviderFacility, sCalculationType, "Aging Detail", "Patient Due", "Provider,Facility", clinicName, _dsReport,(chkPhone.Checked==true?"Y":"N"));
                             oRpt_AgingDetail_PatientProviderFacility.SetDataSource(_dsReport);
                             crvReportViewer.ReportSource = oRpt_AgingDetail_PatientProviderFacility;
                            
                             //if (chkPhone.Checked == false)
                             //{
                             //    Boolean flag = phoneValidation(oRpt_AgingDetail_PatientProviderFacility, true);
                             //}
                             Boolean flag1 = ReportTypeValidation(oRpt_AgingDetail_PatientProviderFacility, true);
                         }


                     }




                     else if (rbtn_InsurancePending.Checked == true)
                     {
                         _sqlCommand.Parameters.Add("@ReportFlag", SqlDbType.Int).Value = 1;
                         _sqlCommand.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = Convert.ToDateTime(_reportDate);

                         if (sProvider != string.Empty)
                             _sqlCommand.Parameters.Add("@Providers", SqlDbType.VarChar).Value = sProvider;
                         else
                             _sqlCommand.Parameters.Add("@Providers", SqlDbType.VarChar).Value = DBNull.Value;
                         if (sFacility != string.Empty)
                             _sqlCommand.Parameters.Add("@Facilities", SqlDbType.VarChar).Value = sFacility;
                         else
                             _sqlCommand.Parameters.Add("@Facilities", SqlDbType.VarChar).Value = DBNull.Value;
                         if (rbtn_InsCompany.Checked == true)
                         {
                             //_sqlCommand.Parameters.Add("@ReportFlag", SqlDbType.Int).Value = 4;
                             if (sInsuranceCompany != string.Empty)
                             {
                                
                                 _sqlCommand.Parameters.Add("@InsuranceCompanies", SqlDbType.VarChar).Value = sInsuranceCompany;
                                 _sqlCommand.Parameters.Add("@ReportingCategories", SqlDbType.VarChar).Value = DBNull.Value;
                             }
                             else
                             {
                                 _sqlCommand.Parameters.Add("@InsuranceCompanies", SqlDbType.VarChar).Value = DBNull.Value;
                                 _sqlCommand.Parameters.Add("@ReportingCategories", SqlDbType.VarChar).Value = DBNull.Value;
                             }
                         }
                         else if (rbtn_ReportingCategory.Checked == true)
                         {
                             //_sqlCommand.Parameters.Add("@ReportFlag", SqlDbType.Int).Value = 5;
                             if (sReportingCategory != string.Empty)
                             {
                               
                                 _sqlCommand.Parameters.Add("@InsuranceCompanies", SqlDbType.VarChar).Value = DBNull.Value;
                                 _sqlCommand.Parameters.Add("@ReportingCategories", SqlDbType.VarChar).Value = sReportingCategory;
                             }
                             else
                             {
                                 _sqlCommand.Parameters.Add("@InsuranceCompanies", SqlDbType.VarChar).Value = DBNull.Value;
                                 _sqlCommand.Parameters.Add("@ReportingCategories", SqlDbType.VarChar).Value = DBNull.Value;
                             }
                         }
                         _sqlCommand.Parameters.Add("@AgingType", SqlDbType.Char).Value = sAgingType;
                         _sqlCommand.Parameters.Add("@Balance", SqlDbType.Decimal).Value = dBalance;
                         _sqlCommand.Parameters.Add("@AgingDays", SqlDbType.Int).Value = iAgingDays;

                         SqlDataAdapter da = new SqlDataAdapter(_sqlCommand);
                    
                         da.Fill(_dsReport, "dt_AgingDetail");
                         da.Dispose();
                         _sqlCommand.Parameters.Clear();


                         if (cmbBreakBy.SelectedIndex == Convert.ToInt32(breakByType.None))
                         {

                             //if (!(object.ReferenceEquals(oRpt_AgingDetail_insurance_None, null)))
                             //{
                             //    oRpt_AgingDetail_insurance_None.Close();
                             //    oRpt_AgingDetail_insurance_None.Dispose();
                             //}
                             //oRpt_AgingDetail_insurance_None = new Rpt_AgingDetail_insurance_None();
                             passParametres(oRpt_AgingDetail_insurance_None, sCalculationType, "Aging Detail", "Insurance Pending", "None", clinicName, _dsReport, (chkPhone.Checked == true ? "Y" : "N"));
                             oRpt_AgingDetail_insurance_None.SetDataSource(_dsReport);
                           
                             crvReportViewer.ReportSource = oRpt_AgingDetail_insurance_None;
                           

                         }
                         else if (cmbBreakBy.SelectedIndex == Convert.ToInt32(breakByType.Facility))
                         {
                             //if (!(object.ReferenceEquals(oRpt_AgingDetail_insurance_Facility, null)))
                             //{
                             //    oRpt_AgingDetail_insurance_Facility.Close();
                             //    oRpt_AgingDetail_insurance_Facility.Dispose();
                             //}
                             //oRpt_AgingDetail_insurance_Facility = new Rpt_AgingDetail_insurance_Facility();
                             passParametres(oRpt_AgingDetail_insurance_Facility, sCalculationType, "Aging Detail", "Insurance Pending", "Facility", clinicName, _dsReport,(chkPhone.Checked==true?"Y":"N"));
                             oRpt_AgingDetail_insurance_Facility.SetDataSource(_dsReport);
                            
                             crvReportViewer.ReportSource = oRpt_AgingDetail_insurance_Facility;
                            

                         }
                         else if (cmbBreakBy.SelectedIndex == Convert.ToInt32(breakByType.Provider))
                         {
                             //if (!(object.ReferenceEquals(oRpt_AgingDetail_insurance_Provider, null)))
                             //{
                             //    oRpt_AgingDetail_insurance_Provider.Close();
                             //    oRpt_AgingDetail_insurance_Provider.Dispose();
                             //}
                             //oRpt_AgingDetail_insurance_Provider = new Rpt_AgingDetail_insurance_Provider();
                             passParametres(oRpt_AgingDetail_insurance_Provider, sCalculationType, "Aging Detail", "Insurance Pending", "Provider", clinicName, _dsReport,(chkPhone.Checked==true?"Y":"N"));
                             oRpt_AgingDetail_insurance_Provider.SetDataSource(_dsReport);
                            
                             crvReportViewer.ReportSource = oRpt_AgingDetail_insurance_Provider;
                            
                         }
                         else if (cmbBreakBy.SelectedIndex == Convert.ToInt32(breakByType.FacilityProvider))
                         {
                             //if (!(object.ReferenceEquals(oRpt_AgingDetail_insurance_ProviderFacility, null)))
                             //{
                             //    oRpt_AgingDetail_insurance_ProviderFacility.Close();
                             //    oRpt_AgingDetail_insurance_ProviderFacility.Dispose();
                             //} 
                             //oRpt_AgingDetail_insurance_ProviderFacility = new Rpt_AgingDetail_insurance_ProviderFacility();
                             passParametres(oRpt_AgingDetail_insurance_ProviderFacility, sCalculationType, "Aging Detail", "Insurance Pending", "Facility,Provider", clinicName, _dsReport,(chkPhone.Checked==true?"Y":"N"));
                             oRpt_AgingDetail_insurance_ProviderFacility.SetDataSource(_dsReport);
                             
                             crvReportViewer.ReportSource = oRpt_AgingDetail_insurance_ProviderFacility;
                            
                         }
                         else if (cmbBreakBy.SelectedIndex == Convert.ToInt32(breakByType.ProviderFacility))
                         {

                             //if (!(object.ReferenceEquals(oRpt_AgingDetail_insurance_FacilityProvider, null)))
                             //{
                             //    oRpt_AgingDetail_insurance_FacilityProvider.Close();
                             //    oRpt_AgingDetail_insurance_FacilityProvider.Dispose();
                             //}
                             //oRpt_AgingDetail_insurance_FacilityProvider = new Rpt_AgingDetail_insurance_FacilityProvider();
                             passParametres(oRpt_AgingDetail_insurance_FacilityProvider, sCalculationType, "Aging Detail", "Insurance Pending", "Provider,Facility", clinicName, _dsReport,(chkPhone.Checked==true?"Y":"N"));
                             oRpt_AgingDetail_insurance_FacilityProvider.SetDataSource(_dsReport);
                          
                             crvReportViewer.ReportSource = oRpt_AgingDetail_insurance_FacilityProvider;
                            
                         }


                     }
                     else if (rbtn_Both.Checked == true)
                     {

                         _sqlCommand.Parameters.Add("@ReportFlag", SqlDbType.Int).Value = 3;
                         _sqlCommand.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = Convert.ToDateTime(_reportDate);

                         if (sProvider != string.Empty)
                             _sqlCommand.Parameters.Add("@Providers", SqlDbType.VarChar).Value = sProvider;
                         else
                             _sqlCommand.Parameters.Add("@Providers", SqlDbType.VarChar).Value = DBNull.Value;
                         if (sFacility != string.Empty)
                             _sqlCommand.Parameters.Add("@Facilities", SqlDbType.VarChar).Value = sFacility;
                         else
                             _sqlCommand.Parameters.Add("@Facilities", SqlDbType.VarChar).Value = DBNull.Value;

                         _sqlCommand.Parameters.Add("@InsuranceCompanies", SqlDbType.VarChar).Value = DBNull.Value;
                         _sqlCommand.Parameters.Add("@ReportingCategories", SqlDbType.VarChar).Value = DBNull.Value;
                         _sqlCommand.Parameters.Add("@AgingType", SqlDbType.Char).Value = sAgingType;
                         _sqlCommand.Parameters.Add("@Balance", SqlDbType.Decimal).Value = dBalance;
                         _sqlCommand.Parameters.Add("@AgingDays", SqlDbType.Int).Value = iAgingDays;


                         SqlDataAdapter da = new SqlDataAdapter(_sqlCommand);
                       
                         da.Fill(_dsReport, "dt_AgingDetail");
                         da.Dispose();
                         _sqlCommand.Parameters.Clear();


                         if (cmbBreakBy.SelectedIndex == Convert.ToInt32(breakByType.None))
                         {
                             //if (!(object.ReferenceEquals(oRpt_AgingDetailPatient, null)))
                             //{
                             //    oRpt_AgingDetailPatient.Close();
                             //    oRpt_AgingDetailPatient.Dispose();
                             //}
                             //oRpt_AgingDetailPatient = new Rpt_AgingDetail_();
                             passParametres(oRpt_AgingDetailPatient, sCalculationType, "Aging Detail", "Both", "None", clinicName, _dsReport,(chkPhone.Checked==true?"Y":"N"));
                             oRpt_AgingDetailPatient.SetDataSource(_dsReport);
                             crvReportViewer.ReportSource = oRpt_AgingDetailPatient;
                             

                             //if (chkPhone.Checked == false)
                             //{
                             //    Boolean flag = phoneValidation(oRpt_AgingDetailPatient, true);
                               



                             //}
                             //else
                             //{
                             //    Boolean flag = phoneValidation(oRpt_AgingDetailPatient, false);




                             //}
                             Boolean flag1 = ReportTypeValidation(oRpt_AgingDetailPatient, false);

                         }
                         else if (cmbBreakBy.SelectedIndex == Convert.ToInt32(breakByType.Facility))
                         {
                             //if (!(object.ReferenceEquals(oRpt_AgingDetail_PatientFacility, null)))
                             //{
                             //    oRpt_AgingDetail_PatientFacility.Close();
                             //    oRpt_AgingDetail_PatientFacility.Dispose();
                             //}
                             //oRpt_AgingDetail_PatientFacility = new Rpt_AgingDetail_PatientFacility();


                             passParametres(oRpt_AgingDetail_PatientFacility, sCalculationType, "Aging Detail", "Both", "Facility", clinicName, _dsReport, (chkPhone.Checked == true ? "Y" : "N"));
                             oRpt_AgingDetail_PatientFacility.SetDataSource(_dsReport);
                             crvReportViewer.ReportSource = oRpt_AgingDetail_PatientFacility;
                             
                             //if (chkPhone.Checked == false)
                             //{
                             //    Boolean flag = phoneValidation(oRpt_AgingDetail_PatientFacility, true);
                             //    //Boolean flag1 = ReportTypeAdjusting(oRpt_AgingDetail_PatientFacility, true);
                             //}
                             Boolean flag1 = ReportTypeValidation(oRpt_AgingDetail_PatientFacility, false);
                         }
                         else if (cmbBreakBy.SelectedIndex == Convert.ToInt32(breakByType.Provider))
                         {
                             //if (!(object.ReferenceEquals(oRpt_AgingDetail_PatientProvider, null)))
                             //{
                             //    oRpt_AgingDetail_PatientProvider.Close();
                             //    oRpt_AgingDetail_PatientProvider.Dispose();
                             //}
                             //oRpt_AgingDetail_PatientProvider = new Rpt_AgingDetail_PatientProvider();
                             passParametres(oRpt_AgingDetail_PatientProvider, sCalculationType, "Aging Detail", "Both", "Provider", clinicName, _dsReport, (chkPhone.Checked == true ? "Y" : "N"));
                             oRpt_AgingDetail_PatientProvider.SetDataSource(_dsReport);
                             crvReportViewer.ReportSource = oRpt_AgingDetail_PatientProvider;
                             
                             //if (chkPhone.Checked == false)
                             //{
                             //    Boolean flag = phoneValidation(oRpt_AgingDetail_PatientProvider, true);
                             //    //Boolean flag1 = ReportTypeAdjusting(oRpt_AgingDetail_PatientProvider, true);
                             //}
                             Boolean flag1 = ReportTypeValidation(oRpt_AgingDetail_PatientProvider, false);
                         }
                         else if (cmbBreakBy.SelectedIndex == Convert.ToInt32(breakByType.FacilityProvider))
                         {
                             //if (!(object.ReferenceEquals(oRpt_AgingDetail_PatientFacilityProvider, null)))
                             //{
                             //    oRpt_AgingDetail_PatientFacilityProvider.Close();
                             //    oRpt_AgingDetail_PatientFacilityProvider.Dispose();
                             //}
                             //oRpt_AgingDetail_PatientFacilityProvider = new Rpt_AgingDetail_PatientFacilityProvider();
                             passParametres(oRpt_AgingDetail_PatientFacilityProvider, sCalculationType, "Aging Detail", "Both", "Facility,Provider", clinicName, _dsReport, (chkPhone.Checked == true ? "Y" : "N"));
                             oRpt_AgingDetail_PatientFacilityProvider.SetDataSource(_dsReport);
                             crvReportViewer.ReportSource = oRpt_AgingDetail_PatientFacilityProvider;
                            
                             //if (chkPhone.Checked == false)
                             //{
                             //    Boolean flag = phoneValidation(oRpt_AgingDetail_PatientFacilityProvider, true);
                             //    //Boolean flag1 = ReportTypeAdjusting(oRpt_AgingDetail_PatientFacilityProvider, true);
                             //}
                             Boolean flag1 = ReportTypeValidation(oRpt_AgingDetail_PatientFacilityProvider, false);
                         }
                         else if (cmbBreakBy.SelectedIndex == Convert.ToInt32(breakByType.ProviderFacility))
                         {
                             //if (!(object.ReferenceEquals(oRpt_AgingDetail_PatientProviderFacility, null)))
                             //{
                             //    oRpt_AgingDetail_PatientProviderFacility.Close();
                             //    oRpt_AgingDetail_PatientProviderFacility.Dispose();
                             //}
                             //oRpt_AgingDetail_PatientProviderFacility = new Rpt_AgingDetail_PatientProviderFacility();
                             passParametres(oRpt_AgingDetail_PatientProviderFacility, sCalculationType, "Aging Detail", "Both", "Provider,Facility", clinicName, _dsReport,(chkPhone.Checked==true?"Y":"N"));
                             oRpt_AgingDetail_PatientProviderFacility.SetDataSource(_dsReport);
                             crvReportViewer.ReportSource = oRpt_AgingDetail_PatientProviderFacility;
                             Boolean flag1 = ReportTypeValidation(oRpt_AgingDetail_PatientProviderFacility, false);
                             //if (chkPhone.Checked == false)
                             //{
                             //    Boolean flag = phoneValidation(oRpt_AgingDetail_PatientProviderFacility, true);
                             //    //Boolean flag1 = ReportTypeAdjusting(oRpt_AgingDetail_PatientProviderFacility, true);
                             //}
                         }

                     }


                 }
                   
             }


             catch (Exception ex)
             {
               
                 gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                 _sqlCommand.Dispose();
                 
                 _sqlConnection.Dispose();
                 _dsReport.Dispose();
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
                 //_dsReport.Dispose();               
                 
             }
        }
        private void passParametres(ReportDocument rptObj, String sCalculationType, String formatType, String reportType, String breakBy,String clinic,DataSet ds,String PhoneNo)
        {
            //rptObj.SetParameterValue("endDate", Convert.ToDateTime(_reportDate).Date);
            //rptObj.SetParameterValue("calclationType", sCalculationType);
            //rptObj.SetParameterValue("formatType", formatType);
            //rptObj.SetParameterValue("reportType", reportType);
            //rptObj.SetParameterValue("breakBy", breakBy);
            //rptObj.SetParameterValue("clinic", clinic);
            //rptObj.SetParameterValue("user", _UserName);

            DataTable dtparam = new DataTable();
            dtparam.Columns.Add("endDate");
            dtparam.Columns.Add("calclationType");
            dtparam.Columns.Add("formatType");
            dtparam.Columns.Add("reportType");
            dtparam.Columns.Add("breakBy");
            dtparam.Columns.Add("clinic");
            dtparam.Columns.Add("user");
            dtparam.Columns.Add("PhoneNo");
            dtparam.Rows.Add();
            dtparam.Rows[0]["endDate"] = Convert.ToDateTime(_reportDate).Date;
            dtparam.Rows[0]["calclationType"] = sCalculationType;
            dtparam.Rows[0]["formatType"] = formatType;
            dtparam.Rows[0]["reportType"] = reportType;
            dtparam.Rows[0]["breakBy"] = breakBy;
            dtparam.Rows[0]["clinic"] = clinic;
            dtparam.Rows[0]["user"] = _UserName;
            dtparam.Rows[0]["PhoneNo"] = PhoneNo;
        

            foreach (DataRow orow in dtparam.Rows)
                ds.Tables["dtAgingParam"].ImportRow(orow);
            dtparam.Dispose();
            dtparam = null;
        }

        private Boolean phoneValidation(ReportDocument rptObj, Boolean flag)
        {
            try
            {
                if (flag == true)
                {
                    TextObject tGroup1 = (TextObject)rptObj.ReportDefinition.Sections["Section2"].ReportObjects["Text101"];
                    tGroup1.ObjectFormat.EnableSuppress = true;
                    FieldObject tGroup2 = (FieldObject)rptObj.ReportDefinition.Sections["GroupHeaderSection1"].ReportObjects["phoneNoStyle1"];
                    tGroup2.ObjectFormat.EnableSuppress = true;

                    TextObject tGroup3 = (TextObject)rptObj.ReportDefinition.Sections["Section2"].ReportObjects["Text100"];
                    tGroup3.Width = tGroup3.Width + tGroup1.Width;
                    FieldObject tGroup4 = (FieldObject)rptObj.ReportDefinition.Sections["GroupHeaderSection1"].ReportObjects["Patient1"];
                    tGroup4.Width = tGroup2.Width + tGroup4.Width;
                    //flag = true;
                    return flag;
                }
                else
                {
                    TextObject tGroup1 = (TextObject)rptObj.ReportDefinition.Sections["Section2"].ReportObjects["Text101"];
                    tGroup1.ObjectFormat.EnableSuppress = false;
                    FieldObject tGroup2 = (FieldObject)rptObj.ReportDefinition.Sections["GroupHeaderSection1"].ReportObjects["phoneNoStyle1"];
                    tGroup2.ObjectFormat.EnableSuppress = false;
                    return flag;
                }
            }
                
            catch
            {
                flag = false;
                return flag;
            }
        }

        private Boolean ReportTypeValidation(ReportDocument rptObj, Boolean flag)
        {
            try
            {
                if (chkPatientDetail.Checked == true)
                    rptObj.ReportDefinition.Sections["DetailSection3"].SectionFormat.EnableSuppress = false;
                else
                    rptObj.ReportDefinition.Sections["DetailSection3"].SectionFormat.EnableSuppress = true;

                if (flag == true)
                {
                    TextObject tGroup1 = (TextObject)rptObj.ReportDefinition.Sections["Section2"].ReportObjects["Text21"];
                    tGroup1.ObjectFormat.EnableSuppress = true;
                    FieldObject tGroup2 = (FieldObject)rptObj.ReportDefinition.Sections["DetailSection3"].ReportObjects["ReportType2"];
                    tGroup2.ObjectFormat.EnableSuppress = true;

                    //TextObject tGroup3 = (TextObject)rptObj.ReportDefinition.Sections["Section2"].ReportObjects["Text100"];
                    //tGroup3.Width = tGroup3.Width + tGroup1.Width;
                    //FieldObject tGroup4 = (FieldObject)rptObj.ReportDefinition.Sections["DetailSection3"].ReportObjects["Patient1"];
                    //tGroup4.Width = tGroup2.Width + tGroup4.Width;


                    //TextObject tGroup5 = (TextObject)rptObj.ReportDefinition.Sections["Section2"].ReportObjects["Text101"];
                    //tGroup5.Left = tGroup5.Left + tGroup1.Width;
                    //FieldObject tGroup6 = (FieldObject)rptObj.ReportDefinition.Sections["DetailSection3"].ReportObjects["phoneNoStyle1"];
                    //tGroup6.Left = tGroup6.Left + tGroup2.Width;
                    flag = true;
                    return flag;
                }
                else
                {
                    TextObject tGroup1 = (TextObject)rptObj.ReportDefinition.Sections["Section2"].ReportObjects["Text21"];
                    tGroup1.ObjectFormat.EnableSuppress = false;
                    FieldObject tGroup2 = (FieldObject)rptObj.ReportDefinition.Sections["DetailSection3"].ReportObjects["ReportType2"];
                    tGroup2.ObjectFormat.EnableSuppress = false;
                    flag = false;
                    return flag;
                }
            }
            catch
            {
                flag = false;
                return flag;
            }
        }

        //private Boolean ReportTypeAdjusting(ReportDocument rptObj, Boolean flag)
        //{
        //    try
        //    {
        //        TextObject tGroup5 = (TextObject)rptObj.ReportDefinition.Sections["Section2"].ReportObjects["Text101"];

        //        FieldObject tGroup6 = (FieldObject)rptObj.ReportDefinition.Sections["GroupHeaderSection1"].ReportObjects["phoneNoStyle1"];

        //        TextObject tGroup1 = (TextObject)rptObj.ReportDefinition.Sections["Section2"].ReportObjects["Text21"];
        //        tGroup1.Left = tGroup1.Left + tGroup5.Width;
        //        FieldObject tGroup2 = (FieldObject)rptObj.ReportDefinition.Sections["DetailSection3"].ReportObjects["ReportType2"];
        //        tGroup2.Left = tGroup2.Left + tGroup6.Width;
        //        flag = true;
        //        return flag;
        //    }

        //    catch
        //    {
        //        flag = false;
        //        return flag;
        //    }
        //}

        private string getCloseDate()
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

        private string getClinicName()
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

        #endregion

        private void tsb_GenerateReport_Click(object sender, EventArgs e)
        {    
            
                   // timer1.Enabled = true;
                    //oProcessLabel.Visible = true;
                    //Application.DoEvents();
                    Cursor.Current = Cursors.WaitCursor; 
                    FillAgingReport();
                    Cursor.Current = Cursors.Default;
                    tsb_btnExportReport.Visible = true;

                    //timer1.Enabled = false;
                    //oProcessLabel.Visible = false;
        }

        private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private DataTable dataforParameter(DataTable dtBulkData, string sType)
        {
            DataTable dtParametredata = new DataTable();

            dtParametredata.Columns.Add("0-30", typeof(Decimal));
            dtParametredata.Columns.Add("31-60", typeof(Decimal));
            dtParametredata.Columns.Add("61-90", typeof(Decimal));
            dtParametredata.Columns.Add("91-120", typeof(Decimal));
            dtParametredata.Columns.Add("120+", typeof(Decimal));
            dtParametredata.Columns.Add("ReportType", typeof(String));
            dtParametredata.Columns.Add("Provider", typeof(String));
            dtParametredata.Columns.Add("Facility", typeof(String));
            if (dtBulkData.Rows.Count > 0)
            {

                //if ((sType == "I") || (sType == "P"))
                //{
                    for (int i = 0; i <= dtBulkData.Rows.Count - 1; i++)
                    {
                        dtParametredata.Rows.Add();
                        if (Convert.ToInt32(dtBulkData.Rows[i]["AgingDays"] == DBNull.Value ? Convert.ToInt32(0) : Convert.ToInt32(dtBulkData.Rows[i]["AgingDays"])) <= 30)
                            dtParametredata.Rows[i]["0-30"] = Convert.ToDecimal(dtBulkData.Rows[i]["dAmount"] == DBNull.Value ? Convert.ToDecimal(0) : Convert.ToDecimal(dtBulkData.Rows[i]["dAmount"]));
                        else
                            dtParametredata.Rows[i]["0-30"] = 0;
                        if ((Convert.ToInt32(dtBulkData.Rows[i]["AgingDays"] == DBNull.Value ? Convert.ToInt32(0) : Convert.ToInt32(dtBulkData.Rows[i]["AgingDays"])) > 30) && (Convert.ToInt32(dtBulkData.Rows[i]["AgingDays"] == DBNull.Value ? Convert.ToInt32(0) : Convert.ToInt32(dtBulkData.Rows[i]["AgingDays"])) <= 60))
                            dtParametredata.Rows[i]["31-60"] = Convert.ToDecimal(dtBulkData.Rows[i]["dAmount"] == DBNull.Value ? Convert.ToDecimal(0) : Convert.ToDecimal(dtBulkData.Rows[i]["dAmount"]));
                        else
                            dtParametredata.Rows[i]["31-60"] = 0;

                        if ((Convert.ToInt32(dtBulkData.Rows[i]["AgingDays"] == DBNull.Value ? Convert.ToInt32(0) : Convert.ToInt32(dtBulkData.Rows[i]["AgingDays"])) > 60) && (Convert.ToInt32(dtBulkData.Rows[i]["AgingDays"] == DBNull.Value ? Convert.ToInt32(0) : Convert.ToInt32(dtBulkData.Rows[i]["AgingDays"])) <= 90))
                            dtParametredata.Rows[i]["61-90"] = Convert.ToDecimal(dtBulkData.Rows[i]["dAmount"] == DBNull.Value ? Convert.ToDecimal(0) : Convert.ToDecimal(dtBulkData.Rows[i]["dAmount"]));
                        else
                            dtParametredata.Rows[i]["61-90"] = 0;
                        if ((Convert.ToInt32(dtBulkData.Rows[i]["AgingDays"] == DBNull.Value ? Convert.ToInt32(0) : Convert.ToInt32(dtBulkData.Rows[i]["AgingDays"])) > 90) && (Convert.ToInt32(dtBulkData.Rows[i]["AgingDays"] == DBNull.Value ? Convert.ToInt32(0) : Convert.ToInt32(dtBulkData.Rows[i]["AgingDays"])) <= 120))
                            dtParametredata.Rows[i]["91-120"] = Convert.ToDecimal(dtBulkData.Rows[i]["dAmount"] == DBNull.Value ? Convert.ToDecimal(0) : Convert.ToDecimal(dtBulkData.Rows[i]["dAmount"]));
                        else
                            dtParametredata.Rows[i]["91-120"] = 0;
                        if (Convert.ToInt32(dtBulkData.Rows[i]["AgingDays"] == DBNull.Value ? Convert.ToInt32(0) : Convert.ToInt32(dtBulkData.Rows[i]["AgingDays"])) > 120)
                            dtParametredata.Rows[i]["120+"] = Convert.ToDecimal(dtBulkData.Rows[i]["dAmount"] == DBNull.Value ? Convert.ToDecimal(0) : Convert.ToDecimal(dtBulkData.Rows[i]["dAmount"]));
                        else
                            dtParametredata.Rows[i]["120+"] = 0;
                        dtParametredata.Rows[i]["ReportType"] = dtBulkData.Rows[i]["ReportType"];
                        dtParametredata.Rows[i]["Provider"] = dtBulkData.Rows[i]["Provider"];
                        dtParametredata.Rows[i]["Facility"] = dtBulkData.Rows[i]["Facility"];
                }
              
                }
                //DataTable abhi  =AgingBucketSorting(dtParametredata, sType, cmbBreakBy.Text);
                   return dtParametredata;

        }

        private DataTable AgingBucketSorting(DataTable dtParametredata, string sType,string filter)
        {
            decimal zero = 0;
            decimal thirty = 0;
            decimal sixty = 0;
            decimal ninety = 0;
            decimal onetwenty = 0;
            dtParametredata.AcceptChanges();
            DataView dv = dtParametredata.DefaultView;
            DataTable uniqueTable = dv.ToTable(true, filter);
            DataTable uniqueTable1;
            uniqueTable1 = dtParametredata.Clone();
            for (int k = 0; k <= uniqueTable.Rows.Count - 1; k++)
            {
                zero = 0;
                thirty = 0;
                sixty = 0;
                ninety = 0;
                onetwenty = 0;
                DataRow[] results;

                results = dtParametredata.Select(filter + "='" + uniqueTable.Rows[k][filter] + "' and ReportType='" + sType + "'");
                foreach (DataRow dr in results)
                {
                    if (zero == 0)
                        zero = Convert.ToDecimal(dr["0-30"]);
                    else
                        zero = zero + Convert.ToDecimal(dr["0-30"]);
                    if (thirty == 0)
                        thirty = Convert.ToDecimal(dr["31-60"]);
                    else
                        thirty = thirty + Convert.ToDecimal(dr["31-60"]);
                    if (sixty == 0)
                        sixty = Convert.ToDecimal(dr["61-90"]);
                    else
                        sixty = sixty + Convert.ToDecimal(dr["61-90"]);
                    if (ninety == 0)
                        ninety = Convert.ToDecimal(dr["91-120"]);
                    else
                        ninety = Convert.ToDecimal(dr["91-120"]);

                    if (onetwenty == 0)
                        onetwenty = Convert.ToDecimal(dr["120+"]);
                    else
                        onetwenty = onetwenty + Convert.ToDecimal(dr["120+"]);

                }
                uniqueTable1.Rows.Add();
                uniqueTable1.Rows[k]["0-30"] = zero;
                uniqueTable1.Rows[k]["31-60"] = thirty;
                uniqueTable1.Rows[k]["61-90"] = sixty;
                uniqueTable1.Rows[k]["91-120"] = ninety;
                uniqueTable1.Rows[k]["120+"] = onetwenty;
                uniqueTable1.Rows[k]["ReportType"] = sType;
                uniqueTable1.Rows[k]["Provider"] = uniqueTable.Rows[k][filter];
                uniqueTable1.Rows[k]["Facility"] = uniqueTable.Rows[k][filter]; ;

            }
            uniqueTable1.AcceptChanges();

            DataView dvMain = uniqueTable1.DefaultView;
            dvMain.Sort = "120+ desc,91-120 desc,61-90 desc,31-60 desc,0-30 desc";
            DataTable dtFinal=new DataTable();
            dtFinal = dvMain.ToTable();
            dv.Dispose();
            uniqueTable.Dispose();
            uniqueTable1.Dispose();
            dvMain.Dispose();
            return dtFinal;
        }

        private void crvReportViewer_Load(object sender, EventArgs e)
        {

        }

        private void tsb_Print_Click(object sender, EventArgs e)
        {
            crvReportViewer.PrintReport();
        }

        private void tsb_btnExport_Click(object sender, EventArgs e)
        {
            crvReportViewer.ExportReport();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
          
            //if (_counter == 1)
            //{
            //    oProcessLabel.Text = "Loading.";
            
            //}
            //else if (_counter == 2)
            //{

            //    oProcessLabel.Text = "Loading..";
            //}
            //else if (_counter == 3)
            //{
            //    oProcessLabel.Text = "Loading...";
            //    _counter = 0;
            //}
            //Application.DoEvents();
            //_counter++;

        }

        private void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {
            string rptCloseDate = "";
            String closedate = getCloseDate();
            if (closedate == "")
            {
             
                rptCloseDate = Convert.ToString(DateTime.Now.Date);
                chkUncloseAct.Checked = true;
                chkUncloseAct.Enabled = false;
            }
            else
            {
                
                rptCloseDate = closedate;

                if (Convert.ToDateTime(rptCloseDate).Date < Convert.ToDateTime(dtpEndDate.Text).Date)
                {
                    if (chkUncloseAct.Checked == true)
                    {
                        chkUncloseAct.Checked = true;
                        chkUncloseAct.Enabled = true;
                    }
                    else
                        chkUncloseAct.Checked =
                            false;
                    chkUncloseAct.Visible = true;
                   
                }
                else
                {
                    chkUncloseAct.Visible = false;
                    chkUncloseAct.Checked = false;
                   
                }
            }

           
        }

        private void txtPatBal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= Convert.ToChar(48) && e.KeyChar <= Convert.ToChar(57)) || e.KeyChar == Convert.ToChar(8) || e.KeyChar == Convert.ToChar(46)))
            {
                e.Handled = true;
            }
            else
            {
                if (txtPatBal.SelectionStart > txtPatBal.Text.IndexOf("."))
                {
                    if (txtPatBal.Text.Contains(".") == true)
                    {
                        if (txtPatBal.Text.Substring(txtPatBal.Text.IndexOf(".") + 1, txtPatBal.Text.Length - (txtPatBal.Text.IndexOf(".") + 1)).Length == 2)
                        {
                            if (e.KeyChar != Convert.ToChar(8))
                            e.Handled = true;
                        }
                    }
                }
            }
            if (e.KeyChar == Convert.ToChar(46) && txtPatBal.Text.Contains("."))
            {
                e.Handled = true;
            }
        }

        private void txtAgingDays_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= Convert.ToChar(48) && e.KeyChar <= Convert.ToChar(57)) || e.KeyChar == Convert.ToChar(8)))
            {
                e.Handled = true;
            }



        }

        private void chkUncloseAct_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void frmRpt_AgingReport_FormClosed(object sender, FormClosedEventArgs e)
        {
           
            if (oRpt_AgingSummary_PatientFacility != null)
            {
                if (oRpt_AgingSummary_PatientFacility.IsLoaded)
                {
                    oRpt_AgingSummary_PatientFacility.Close();
                }
                oRpt_AgingSummary_PatientFacility.Dispose();
                //objrptPatientStatementForGateWayEDI = null;
            }
            if (oRptAgingSummaryInsurancePending1 != null)
            {
                if (oRptAgingSummaryInsurancePending1.IsLoaded)
                {
                    oRptAgingSummaryInsurancePending1.Close();
                }
                oRptAgingSummaryInsurancePending1.Dispose();
                //objrptPatientStatementForGateWayEDI = null;
            }
            if (oRpt_AgingSummary_InsFacilityProvider != null)
            {
                if (oRpt_AgingSummary_InsFacilityProvider.IsLoaded)
                {
                    oRpt_AgingSummary_InsFacilityProvider.Close();
                }
                oRpt_AgingSummary_InsFacilityProvider.Dispose();
                //objrptPatientStatementForGateWayEDI = null;
            }
            if (oRpt_AgingSummary_InsProviderFacility != null)
            {
                if (oRpt_AgingSummary_InsProviderFacility.IsLoaded)
                {
                    oRpt_AgingSummary_InsProviderFacility.Close();
                }
                oRpt_AgingSummary_InsProviderFacility.Dispose();
                //objrptPatientStatementForGateWayEDI = null;
            }
            if (oRpt_AgingSummaryPatAndInsurance != null)
            {
                if (oRpt_AgingSummaryPatAndInsurance.IsLoaded)
                {
                    oRpt_AgingSummaryPatAndInsurance.Close();
                }
                oRpt_AgingSummaryPatAndInsurance.Dispose();
                //objrptPatientStatementForGateWayEDI = null;
            }
            if (oRpt_AgingDetailPatient != null)
            {
                if (oRpt_AgingDetailPatient.IsLoaded)
                {
                    oRpt_AgingDetailPatient.Close();
                }
                oRpt_AgingDetailPatient.Dispose();
                //objrptPatientStatementForGateWayEDI = null;
            }
            if (oRpt_AgingDetailInsurance != null)
            {
                if (oRpt_AgingDetailInsurance.IsLoaded)
                {
                    oRpt_AgingDetailInsurance.Close();
                }
                oRpt_AgingDetailInsurance.Dispose();
                //objrptPatientStatementForGateWayEDI = null;
            }
            if (oRpt_AgingSummary_PatientProvider != null)
            {
                if (oRpt_AgingSummary_PatientProvider.IsLoaded)
                {
                    oRpt_AgingSummary_PatientProvider.Close();
                }
                oRpt_AgingSummary_PatientProvider.Dispose();
                //objrptPatientStatementForGateWayEDI = null;
            }

            if (oRpt_AgingSummaryPatAndInsFacilityProvider != null)
            {
                if (oRpt_AgingSummaryPatAndInsFacilityProvider.IsLoaded)
                {
                    oRpt_AgingSummaryPatAndInsFacilityProvider.Close();
                }
                oRpt_AgingSummaryPatAndInsFacilityProvider.Dispose();
                //objrptPatientStatementForGateWayEDI = null;
            }
            if (orpt_AgingSummaryPatAndInsProviderFacility != null)
            {
                if (orpt_AgingSummaryPatAndInsProviderFacility.IsLoaded)
                {
                    orpt_AgingSummaryPatAndInsProviderFacility.Close();
                }
                orpt_AgingSummaryPatAndInsProviderFacility.Dispose();
                //objrptPatientStatementForGateWayEDI = null;
            }
            if (orpt_AgingSummaryPatAndInsNone != null)
            {
                if (orpt_AgingSummaryPatAndInsNone.IsLoaded)
                {
                    orpt_AgingSummaryPatAndInsNone.Close();
                }
                orpt_AgingSummaryPatAndInsNone.Dispose();
                //objrptPatientStatementForGateWayEDI = null;
            }
            if (orpt_AgingSummaryPatAndInsProvider != null)
            {
                if (orpt_AgingSummaryPatAndInsProvider.IsLoaded)
                {
                    orpt_AgingSummaryPatAndInsProvider.Close();
                }
                orpt_AgingSummaryPatAndInsProvider.Dispose();
                //objrptPatientStatementForGateWayEDI = null;
            }
            if (orpt_AgingSummaryPatAndInsFacility != null)
            {
                if (orpt_AgingSummaryPatAndInsFacility.IsLoaded)
                {
                    orpt_AgingSummaryPatAndInsFacility.Close();
                }
                orpt_AgingSummaryPatAndInsFacility.Dispose();
                //objrptPatientStatementForGateWayEDI = null;
            }
            if (oRpt_AgingDetail_insurance_FacilityProvider != null)
            {
                if (oRpt_AgingDetail_insurance_FacilityProvider.IsLoaded)
                {
                    oRpt_AgingDetail_insurance_FacilityProvider.Close();
                }
                oRpt_AgingDetail_insurance_FacilityProvider.Dispose();
                //objrptPatientStatementForGateWayEDI = null;
            }
            if (oRpt_AgingDetail_insurance_ProviderFacility != null)
            {
                if (oRpt_AgingDetail_insurance_ProviderFacility.IsLoaded)
                {
                    oRpt_AgingDetail_insurance_ProviderFacility.Close();
                }
                oRpt_AgingDetail_insurance_ProviderFacility.Dispose();
                //objrptPatientStatementForGateWayEDI = null;
            }
            if (oRpt_AgingDetail_insurance_Provider != null)
            {
                if (oRpt_AgingDetail_insurance_Provider.IsLoaded)
                {
                    oRpt_AgingDetail_insurance_Provider.Close();
                }
                oRpt_AgingDetail_insurance_Provider.Dispose();
                //objrptPatientStatementForGateWayEDI = null;
            }
            if (oRpt_AgingDetail_insurance_Facility != null)
            {
                if (oRpt_AgingDetail_insurance_Facility.IsLoaded)
                {
                    oRpt_AgingDetail_insurance_Facility.Close();
                }
                oRpt_AgingDetail_insurance_Facility.Dispose();
                //objrptPatientStatementForGateWayEDI = null;
            }
            if (oRpt_AgingDetail_insurance_None != null)
            {
                if (oRpt_AgingDetail_insurance_None.IsLoaded)
                {
                    oRpt_AgingDetail_insurance_None.Close();
                }
                oRpt_AgingDetail_insurance_None.Dispose();
                //objrptPatientStatementForGateWayEDI = null;
            }
            if (oRpt_AgingDetail_PatientFacility != null)
            {
                if (oRpt_AgingDetail_PatientFacility.IsLoaded)
                {
                    oRpt_AgingDetail_PatientFacility.Close();
                }
                oRpt_AgingDetail_PatientFacility.Dispose();
                //objrptPatientStatementForGateWayEDI = null;
            }
            if (oRpt_AgingDetail_PatientFacilityProvider != null)
            {
                if (oRpt_AgingDetail_PatientFacilityProvider.IsLoaded)
                {
                    oRpt_AgingDetail_PatientFacilityProvider.Close();
                }
                oRpt_AgingDetail_PatientFacilityProvider.Dispose();
                //objrptPatientStatementForGateWayEDI = null;
            }
            if (oRpt_AgingDetail_PatientProvider != null)
            {
                if (oRpt_AgingDetail_PatientProvider.IsLoaded)
                {
                    oRpt_AgingDetail_PatientProvider.Close();
                }
                oRpt_AgingDetail_PatientProvider.Dispose();
                //objrptPatientStatementForGateWayEDI = null;
            }
            if (oRpt_AgingDetail_PatientProviderFacility != null)
            {
                if (oRpt_AgingDetail_PatientProviderFacility.IsLoaded)
                {
                    oRpt_AgingDetail_PatientProviderFacility.Close();
                }
                oRpt_AgingDetail_PatientProviderFacility.Dispose();
                //objrptPatientStatementForGateWayEDI = null;
            }
            try
            {
                //boldFont.Dispose();
                //regularFont.Dispose();
            }
            catch
            {
            }
        }

       

      

    }
}