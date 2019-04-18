using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace gloBilling
{
    public partial class frmRpt_PatientReport : Form
    {
        #region "Private Variables "

        private string _databaseconnectionstring = "";
        private Int64 _clinicId = 0;
        private string _messageBoxCaption = String.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        gloListControl.gloListControl oListControl = null;
        gloListControl.gloListControlType _CurrentControlType = gloListControl.gloListControlType.Other;

        const int COL_PATIENT_ID = 0;
        const int COL_PATIENT_CODE = 1;
        const int COL_PATIENT_NAME = 2;
        const int COL_PATIENT_PHONE = 3;
        const int COL_GENDER = 4;
        const int COL_DOB = 5;
        const int COL_PROVIDER = 6;
        const int COL_REFERRAL = 7;
        const int COL_COUNT = 8;

        #endregion "Private Variables "

        public frmRpt_PatientReport(string DatabaseConnectionString)
        {
            InitializeComponent();
            _databaseconnectionstring = DatabaseConnectionString;

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _clinicId = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _clinicId = 0; }
            }
            else
            { _clinicId = 0; }

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageBoxCaption = "gloPM";
                }
            }
            else
            { _messageBoxCaption = "gloPM"; }

            #endregion
        }

        private void frmRpt_PatientReport_Load(object sender, EventArgs e)
        {

            FillFacility();

            if (C1Patients.Rows.Count <= 1)
            {
                tls_btnExportToExcel.Enabled = false;
                tls_btnExportToExcelOpen.Enabled = false;
            }
            else
            {
                tls_btnExportToExcel.Enabled = true;
                tls_btnExportToExcelOpen.Enabled = true;
            }
            Fill_FilterDatesCombo();
            Fill_FilterDatesCombo1();
        }

        #region "List Control Events"

        private void btnBrowseProvider_Click(object sender, EventArgs e)
        {
            try
            {
                if (oListControl != null)
                {
                    for (int i = this.Controls.Count - 1; i >= 0; i--)
                    {
                        if (this.Controls[i].Name == oListControl.Name)
                        {
                            this.Controls.Remove(this.Controls[i]);
                            break;
                        }
                    }
                    try
                    {
                        oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                        oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                    }
                    catch { }
                    oListControl.Dispose();
                    oListControl = null;
                }
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Providers, true, this.Width);
                oListControl.ClinicID = _clinicId;
                oListControl.ControlHeader = " Provider";
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

        private void btnBrowseDiagnosisCode_Click(object sender, EventArgs e)
        {
            try
            {
                if (oListControl != null)
                {
                    for (int i = this.Controls.Count - 1; i >= 0; i--)
                    {
                        if (this.Controls[i].Name == oListControl.Name)
                        {
                            this.Controls.Remove(this.Controls[i]);
                            break;
                        }
                    }
                    try
                    {
                        oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                        oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                    }
                    catch { }
                    oListControl.Dispose();
                    oListControl = null;
                }
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Diagnosis, true, this.Width);
                oListControl.ClinicID = _clinicId;
                oListControl.ControlHeader = " Diagnosis";

                _CurrentControlType = gloListControl.gloListControlType.Diagnosis;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                this.Controls.Add(oListControl);

                if (cmbDiagnosisCode.DataSource != null)
                {
                    for (int i = 0; i < cmbDiagnosisCode.Items.Count; i++)
                    {
                        cmbDiagnosisCode.SelectedIndex = i;
                        cmbDiagnosisCode.Refresh();
                        oListControl.SelectedItems.Add(Convert.ToInt64(cmbDiagnosisCode.SelectedValue), cmbDiagnosisCode.Text, "");

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

        private void btnClearDiagnosisCode_Click(object sender, EventArgs e)
        {
            
            cmbDiagnosisCode.DataSource = null;
            cmbDiagnosisCode.Items.Clear();
            cmbDiagnosisCode.Refresh();
        }

        private void btnBrowseInsurance_Click(object sender, EventArgs e)
        {
            try
            {
                if (oListControl != null)
                {
                    for (int i = this.Controls.Count - 1; i >= 0; i--)
                    {
                        if (this.Controls[i].Name == oListControl.Name)
                        {
                            this.Controls.Remove(this.Controls[i]);
                            break;
                        }
                    }
                    try
                    {
                        oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                        oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                    }
                    catch { }
                    oListControl.Dispose();
                    oListControl = null;
                }
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.AllPatientInsurances, true, this.Width);
                oListControl.ClinicID = _clinicId;
                oListControl.ControlHeader = " Insurances";
                _CurrentControlType = gloListControl.gloListControlType.AllPatientInsurances;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                this.Controls.Add(oListControl);

                if (cmbInsurance.DataSource != null)
                {
                    for (int i = 0; i < cmbInsurance.Items.Count; i++)
                    {
                        cmbInsurance.SelectedIndex = i;
                        cmbInsurance.Refresh();
                        oListControl.SelectedItems.Add(Convert.ToInt64(cmbInsurance.SelectedValue), cmbInsurance.Text);
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

        private void btnClearInsurance_Click(object sender, EventArgs e)
        {
           
            cmbInsurance.DataSource = null;
            cmbInsurance.Items.Clear();
            cmbInsurance.Refresh();
        }

        private void btnBrowseCPT_Click(object sender, EventArgs e)
        {
            try
            {
                if (oListControl != null)
                {
                    for (int i = this.Controls.Count - 1; i >= 0; i--)
                    {
                        if (this.Controls[i].Name == oListControl.Name)
                        {
                            this.Controls.Remove(this.Controls[i]);
                            break;
                        }
                    }
                    try
                    {
                        oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                        oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                    }
                    catch { }
                    oListControl.Dispose();
                    oListControl = null;
                }
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.CPT, true, this.Width);
                oListControl.ClinicID = _clinicId;
                oListControl.ControlHeader = " CPT";
                _CurrentControlType = gloListControl.gloListControlType.CPT;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                this.Controls.Add(oListControl);

                if (cmbCPT.DataSource != null)
                {
                    for (int i = 0; i < cmbCPT.Items.Count; i++)
                    {
                        cmbCPT.SelectedIndex = i;
                        cmbCPT.Refresh();
                        oListControl.SelectedItems.Add(Convert.ToInt64(cmbCPT.SelectedValue), cmbCPT.Text, "");
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

        private void btnClearCPT_Click(object sender, EventArgs e)
        {
           
            cmbCPT.DataSource = null;
            cmbCPT.Items.Clear();
            cmbCPT.Refresh();
        }

        private void btnBrowseRefProvider_Click(object sender, EventArgs e)
        {
            try
            {
                if (oListControl != null)
                {
                    for (int i = this.Controls.Count - 1; i >= 0; i--)
                    {
                        if (this.Controls[i].Name == oListControl.Name)
                        {
                            this.Controls.Remove(this.Controls[i]);
                            break;
                        }
                    }
                    try
                    {
                        oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                        oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                    }
                    catch { }
                    oListControl.Dispose();
                    oListControl = null;
                }
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Referrals, true, this.Width);
                oListControl.ClinicID = _clinicId;
                oListControl.ControlHeader = " Referrals";
                _CurrentControlType = gloListControl.gloListControlType.Referrals;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                this.Controls.Add(oListControl);

                if (cmbReferral.DataSource != null)
                {
                    for (int i = 0; i < cmbReferral.Items.Count; i++)
                    {
                        cmbReferral.SelectedIndex = i;
                        cmbReferral.Refresh();
                        oListControl.SelectedItems.Add(Convert.ToInt64(cmbReferral.SelectedValue), cmbReferral.Text, "");
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

        private void btnClearRefProvider_Click(object sender, EventArgs e)
        {
           
            cmbReferral.DataSource = null;
            cmbReferral.Items.Clear();
            cmbReferral.Refresh();
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
                case gloListControl.gloListControlType.AllPatientInsurances:
                    {
                        
                        cmbInsurance.DataSource = null;
                        cmbInsurance.Items.Clear();
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

                            cmbInsurance.DataSource = oBindTable;
                            cmbInsurance.DisplayMember = "DispName";
                            cmbInsurance.ValueMember = "ID";
                        }


                    }
                    break;
                case gloListControl.gloListControlType.Diagnosis:
                    {
                       
                        cmbDiagnosisCode.DataSource = null;
                        cmbDiagnosisCode.Items.Clear();
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

                            cmbDiagnosisCode.DataSource = oBindTable;
                            cmbDiagnosisCode.DisplayMember = "DispName";
                            cmbDiagnosisCode.ValueMember = "ID";
                        }


                    }
                    break;
                case gloListControl.gloListControlType.CPT:
                    {
                      
                        cmbCPT.DataSource = null;
                        cmbCPT.Items.Clear();
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

                            cmbCPT.DataSource = oBindTable;
                            cmbCPT.DisplayMember = "DispName";
                            cmbCPT.ValueMember = "ID";
                        }


                    }
                    break;
                case gloListControl.gloListControlType.Referrals:
                    {
                       
                        cmbReferral.DataSource = null;
                        cmbReferral.Items.Clear();
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

                            cmbReferral.DataSource = oBindTable;
                            cmbReferral.DisplayMember = "DispName";
                            cmbReferral.ValueMember = "ID";
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
            if (oListControl != null)
            {
                for (int i = this.Controls.Count - 1; i >= 0; i--)
                {
                    if (this.Controls[i].Name == oListControl.Name)
                    {
                        this.Controls.Remove(this.Controls[i]);
                        break;
                    }
                }
                try
                {
                    oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                    oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                }
                catch { }
              
            }
        }

        #endregion

        #region "Tool Strip buttons Event"
        private void tls_btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tls_btnOK_Click(object sender, EventArgs e)
        {
            ShowReport();
            if (C1Patients.Rows.Count <= 1)
            {
                tls_btnExportToExcel.Enabled = false;
                tls_btnExportToExcelOpen.Enabled = false;
            }
            else
            {
                tls_btnExportToExcel.Enabled = true;
                tls_btnExportToExcelOpen.Enabled = true;
            } 
        }

        private void tls_btnExportToExcel_Click(object sender, EventArgs e)
        {
            if (C1Patients != null && C1Patients.Rows.Count > 1)
            {
                ExportReportToExcel(false);
            }
        }

        private void tls_btnExportToExcelOpen_Click(object sender, EventArgs e)
        {
            if (C1Patients != null && C1Patients.Rows.Count > 1)
            {
                ExportReportToExcel(true);
            }
        }

        #endregion
        // For feeling facility drop down..
        private void FillFacility()
        {
            gloFacility ogloFacility = new gloFacility(_databaseconnectionstring);
            DataTable dtFacilities = new DataTable();
            DataTable dtBindTable = new DataTable();
            try
            {
                dtBindTable.Columns.Add("Code");
                dtBindTable.Columns.Add("Desc");

                dtFacilities = ogloFacility.GetFacilities();
                if (dtFacilities != null)
                {
                    DataRow oBindRow = null;
                    oBindRow = dtBindTable.NewRow();
                    oBindRow["Code"] = "";
                    oBindRow["Desc"] = "";
                    dtBindTable.Rows.Add(oBindRow);

                    for (int i = 0; i < dtFacilities.Rows.Count; i++)
                    {
                        oBindRow = dtBindTable.NewRow();
                        oBindRow["Code"] = Convert.ToString(dtFacilities.Rows[i]["sFacilityCode"]);

                        
                        oBindRow["Desc"] = Convert.ToString(dtFacilities.Rows[i]["sFacilityName"]);

                        
                        dtBindTable.Rows.Add(oBindRow);
                    }
                }
                cmbFacility.DataSource = dtBindTable;
                cmbFacility.DisplayMember = "Desc";
                cmbFacility.ValueMember = "Code";

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (ogloFacility != null) { ogloFacility.Dispose(); }
            }
        }
        

        private void ShowReport()
        {
            try
            {
                DesignGrid();
                DataTable dtPatients = new DataTable();
                dtPatients = GetPatients();
                if (dtPatients != null)
                {
                    for (int i = 0; i < dtPatients.Rows.Count; i++)
                    {
                        Int32 RowIndex = C1Patients.Rows.Count;
                        C1Patients.Rows.Add();

                        C1Patients.SetData(RowIndex, COL_PATIENT_ID, Convert.ToString(dtPatients.Rows[i]["nPatientID"]));
                        C1Patients.SetData(RowIndex, COL_PATIENT_CODE, Convert.ToString(dtPatients.Rows[i]["sPatientCode"]));
                        C1Patients.SetData(RowIndex, COL_PATIENT_NAME, Convert.ToString(dtPatients.Rows[i]["sPatientName"]));
                        C1Patients.SetData(RowIndex, COL_PATIENT_PHONE, Convert.ToString(dtPatients.Rows[i]["sPatientPhone"]));
                        C1Patients.SetData(RowIndex, COL_GENDER, Convert.ToString(dtPatients.Rows[i]["sGender"]));
                        C1Patients.SetData(RowIndex, COL_DOB, Convert.ToDateTime(dtPatients.Rows[i]["dtDOB"]).ToShortDateString());
                        C1Patients.SetData(RowIndex, COL_PROVIDER, Convert.ToString(dtPatients.Rows[i]["sProviderName"]));
                        //C1Patients.SetData(RowIndex, COL_REFERRAL, Convert.ToString(dtPatients.Rows[i]["sReferralProvider"]));

                    }
                    //gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.PatientReport, gloAuditTrail.ActivityType.View, "View Patient Report", gloAuditTrail.ActivityOutCome.Success);
                    //Added Rahul on 20101012
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.PatientReport, gloAuditTrail.ActivityType.View, "View Patient Report", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                    //
                }


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }

        private void DesignGrid()
        {
            try
            {
                C1Patients.Rows.Count = 1;
                C1Patients.Cols.Count = COL_COUNT;

                C1Patients.SetData(0, COL_PATIENT_ID, "Patient ID");
                C1Patients.SetData(0, COL_PATIENT_CODE, "Patient Code");
                C1Patients.SetData(0, COL_PATIENT_NAME, "Patient Name");
                C1Patients.SetData(0, COL_PATIENT_PHONE, "Phone");
                C1Patients.SetData(0, COL_GENDER, "Gender");
                C1Patients.SetData(0, COL_DOB, "Date Of Birth");
                C1Patients.SetData(0, COL_PROVIDER, "Provider ");
                C1Patients.SetData(0, COL_REFERRAL, "Referral");

                C1Patients.AllowEditing = false;

                C1Patients.Cols[COL_PATIENT_ID].Visible = false;
                C1Patients.Cols[COL_REFERRAL].Visible = false;

                C1Patients.Cols[COL_PATIENT_PHONE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1Patients.Cols[COL_PATIENT_CODE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                Int32 _width = pnlPatientDetails.Width - 5;

                C1Patients.Cols[COL_PATIENT_ID].Width = 0;
                C1Patients.Cols[COL_PATIENT_CODE].Width = Convert.ToInt32(_width * 0.15);
                C1Patients.Cols[COL_PATIENT_NAME].Width = Convert.ToInt32(_width * 0.2);
                C1Patients.Cols[COL_PATIENT_PHONE].Width = Convert.ToInt32(_width * 0.1);
                C1Patients.Cols[COL_GENDER].Width = Convert.ToInt32(_width * 0.1);
                C1Patients.Cols[COL_DOB].Width = Convert.ToInt32(_width * 0.1);
                C1Patients.Cols[COL_PROVIDER].Width = Convert.ToInt32(_width * 0.2);
                //C1Patients.Cols[COL_REFERRAL].Width = Convert.ToInt32(_width * 0.2);

                C1Patients.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.SingleColumn;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }

        private DataTable GetPatients()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strSQL = "";
            DataTable dt = new DataTable();
            try
            {

                oDB.Connect(false);
                //strSQL = "SELECT   DISTINCT  Patient.nPatientID, "
                //+ " ISNULL(Patient.sPatientCode, '') AS sPatientCode, "
                //+ " ISNULL(Patient.sFirstName, '') + SPACE(1) + ISNULL(Patient.sMiddleName, '') + SPACE(1) + ISNULL(Patient.sLastName, '') AS sPatientName, "
                //+ " ISNULL(Patient.sGender, '') AS sGender, "
                //+ " CONVERT(VARCHAR,Patient.dtDOB,101) AS dtDOB,"
                //+ " ISNULL(Provider_MST.sFirstName, '') + SPACE(1) + ISNULL(Provider_MST.sMiddleName, '') + SPACE(1) + ISNULL(Provider_MST.sLastName, '') AS sProviderName, "
                //+ " 		ISNULL((SELECT ISNULL(Patient_DTL.sFirstName, '') + SPACE(1) + ISNULL( Patient_DTL.sMiddleName,  '') + SPACE(1) + ISNULL(Patient_DTL.sLastName,'')"
                //+ " 		FROM Patient INNER JOIN Patient_DTL ON Patient.nPatientID = Patient_DTL.nPatientID"
                //+ " 		WHERE Patient_DTL.nContactFlag = 3),'') "
                //+ " AS sReferralProvider"
                //+ " FROM BL_Transaction_Lines INNER JOIN"
                //+ " BL_Transaction_MST ON BL_Transaction_Lines.nTransactionID = BL_Transaction_MST.nTransactionID RIGHT OUTER JOIN"
                //+ " Patient ON BL_Transaction_MST.nPatientID = Patient.nPatientID LEFT OUTER JOIN"
                //+ " PatientInsurance_DTL ON Patient.nPatientID = PatientInsurance_DTL.nPatientID LEFT OUTER JOIN"
                //+ " Provider_MST ON Patient.nProviderID = Provider_MST.nProviderID LEFT OUTER JOIN"
                //+ " Patient_DTL ON Patient.nPatientID = Patient_DTL.nPatientID"
                //+ " WHERE Patient.nClinicID = " + _clinicId;

                strSQL = "SELECT   DISTINCT  Patient.nPatientID, "
               + " ISNULL(Patient.sPatientCode, '') AS sPatientCode, "
               + " ISNULL(Patient.sFirstName, '') + SPACE(1) + ISNULL(Patient.sMiddleName, '') + SPACE(1) + ISNULL(Patient.sLastName, '') AS sPatientName, "
               + " ISNULL(Patient.sGender, '') AS sGender,ISNULL(Patient.sPhone,'') AS sPatientPhone, "
               + " CONVERT(VARCHAR,Patient.dtDOB,101) AS dtDOB,"
               + " ISNULL(Provider_MST.sFirstName, '') + SPACE(1) + ISNULL(Provider_MST.sMiddleName, '') + SPACE(1) + ISNULL(Provider_MST.sLastName, '') AS sProviderName "
               + " FROM BL_Transaction_Lines INNER JOIN"
               + " BL_Transaction_MST ON BL_Transaction_Lines.nTransactionID = BL_Transaction_MST.nTransactionID RIGHT OUTER JOIN"
               + " Patient ON BL_Transaction_MST.nPatientID = Patient.nPatientID LEFT OUTER JOIN"
               + " PatientInsurance_DTL ON Patient.nPatientID = PatientInsurance_DTL.nPatientID LEFT OUTER JOIN"
               + " Provider_MST ON Patient.nProviderID = Provider_MST.nProviderID LEFT OUTER JOIN"
               + " Patient_DTL ON Patient.nPatientID = Patient_DTL.nPatientID"
               + " WHERE Patient.nClinicID = " + _clinicId;


                if (chkFromToDates.Checked == true)
                {
                    strSQL += " AND (BL_Transaction_MST.nTransactionDate >= " + gloDateMaster.gloDate.DateAsNumber(dtpStartDate.Value.ToShortDateString()) + " AND BL_Transaction_MST.nTransactionDate <= " + gloDateMaster.gloDate.DateAsNumber(dtpEndDate.Value.ToShortDateString()) + ") ";
                }

                if (chkDateOfService.Checked == true)
                {
                    strSQL += " AND (BL_Transaction_Lines.nFromDate >= " + gloDateMaster.gloDate.DateAsNumber(dtpDOSFrom.Value.ToShortDateString()) + " AND BL_Transaction_Lines.nFromDate <= " + gloDateMaster.gloDate.DateAsNumber(dtpDOSTo.Value.ToShortDateString()) + ") ";
                }

                if (cmbProvider.Items.Count > 0)
                {
                    string _strProviderIDs = "";
                    for (int i = 0; i < cmbProvider.Items.Count; i++)
                    {

                        cmbProvider.SelectedIndex = i;
                        cmbProvider.Refresh();
                        if (i == 0)
                        {
                            _strProviderIDs = "(" + Convert.ToInt64(cmbProvider.SelectedValue);
                        }
                        else
                        {
                            _strProviderIDs += "," + Convert.ToInt64(cmbProvider.SelectedValue);
                        }

                        if (i == cmbProvider.Items.Count - 1)
                        {
                            _strProviderIDs += ")";
                        }
                    }

                    if (_strProviderIDs != "")
                        strSQL += " AND Provider_MST.nProviderID IN " + _strProviderIDs + " ";
                }
                if (cmbInsurance.Items.Count > 0)
                {
                    string _strInsuranceNames = "";

                    for (int i = 0; i < cmbInsurance.Items.Count; i++)
                    {

                        cmbInsurance.SelectedIndex = i;
                        cmbInsurance.Refresh();
                        if (i == 0)
                        {
                            _strInsuranceNames = "('" + Convert.ToString(cmbInsurance.Text).Replace("'","''") + "'";
                        }
                        else
                        {
                            _strInsuranceNames += ",'" + Convert.ToString(cmbInsurance.Text).Replace("'", "''") + "'";
                        }

                        if (i == cmbInsurance.Items.Count - 1)
                        {
                            _strInsuranceNames += ")";
                        }
                    }

                    if (_strInsuranceNames != "")
                        strSQL += " AND PatientInsurance_DTL.sInsuranceName IN " + _strInsuranceNames + " ";
                }
                if (cmbCPT.Items.Count > 0)
                {
                    string _strCPTCodes = "";

                    for (int i = 0; i < cmbCPT.Items.Count; i++)
                    {

                        cmbCPT.SelectedIndex = i;
                        if (i == 0)
                        {
                            _strCPTCodes = "('" + cmbCPT.Text.Trim() + "'";
                        }
                        else
                        {
                            _strCPTCodes += ",'" + cmbCPT.Text.Trim() + "'";
                        }

                        if (i == cmbCPT.Items.Count - 1)
                        {
                            _strCPTCodes += ")";
                        }
                    }

                    if (_strCPTCodes != "")
                        strSQL += " AND BL_Transaction_Lines.sCPTCode IN " + _strCPTCodes + "  ";
                }
                if (cmbDiagnosisCode.Items.Count > 0)
                {
                    string _strDiagnosisCodes = "";

                    for (int i = 0; i < cmbDiagnosisCode.Items.Count; i++)
                    {

                        cmbDiagnosisCode.SelectedIndex = i;
                        cmbDiagnosisCode.Refresh();
                        if (i == 0)
                        {
                            _strDiagnosisCodes = "('" + cmbDiagnosisCode.Text.Trim() + "'";
                        }
                        else
                        {
                            _strDiagnosisCodes += ",'" + cmbDiagnosisCode.Text.Trim() + "'";
                        }

                        if (i == cmbDiagnosisCode.Items.Count - 1)
                        {
                            _strDiagnosisCodes += ")";
                        }
                    }

                    if (_strDiagnosisCodes != "")
                        strSQL += " AND (BL_Transaction_Lines.sDx1Code IN " + _strDiagnosisCodes + " OR BL_Transaction_Lines.sDx2Code IN " + _strDiagnosisCodes + " OR BL_Transaction_Lines.sDx3Code IN " + _strDiagnosisCodes + " OR BL_Transaction_Lines.sDx4Code IN " + _strDiagnosisCodes + " ) ";
                }

                //Referral
                if (cmbReferral.Items.Count > 0)
                {
                    string _strReferrals = "";

                    for (int i = 0; i < cmbReferral.Items.Count; i++)
                    {

                        cmbReferral.SelectedIndex = i;
                        if (i == 0)
                        {
                            _strReferrals = "('" + cmbReferral.Text.Trim() + "'";
                        }
                        else
                        {
                            _strReferrals += ",'" + cmbReferral.Text.Trim() + "'";
                        }

                        if (i == cmbReferral.Items.Count - 1)
                        {
                            _strReferrals += ")";
                        }
                    }

                    if (_strReferrals != "")
                    {
                        strSQL += " AND  (ISNULL(Patient_DTL.sFirstName,'' ) + SPACE(1) + ISNULL(Patient_DTL.sMiddleName,'' ) + SPACE(1) + ISNULL(Patient_DTL.sLastName,'') IN " + _strReferrals + ""
                        + " AND Patient_DTL.nContactFlag = 3) ";
                    }
                }


                if (cmbFacility.Text.Trim() != "")
                {
                    strSQL += " AND (BL_Transaction_MST.sFacilityCode = '" + cmbFacility.SelectedValue.ToString() + "') ";
                }

                if (txtZipCode.Text.Trim() != "")
                {
                    strSQL += " AND (Patient.sZIP = '" + txtZipCode.Text.Trim() + "')";
                }
                if (txtState.Text.Trim() != "")
                {
                    strSQL += "AND UPPER(Patient.sState) = UPPER('" + txtState.Text.Trim() + "')";
                }
                if (txtCity.Text.Trim() != "")
                {
                    strSQL += "AND UPPER(Patient.sCity) = UPPER('" + txtCity.Text.Trim() + "')";
                }
                if (cmbGender.Text.ToUpper() != "")
                {
                    strSQL += "AND UPPER(Patient.sGender) = '" + cmbGender.Text.Trim() + "' ";
                }
                oDB.Retrive_Query(strSQL, out dt);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

                return null;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
            return dt;
        }

        private void ExportReportToExcel(bool OpenReport)
        {
          //  gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
          //  object value = new object();
            string _DefaultLocationPath = "";
            string _FilePath = "";
            bool _Checked = false;
            try
            {
                //ogloSettings.GetSetting("ExportToDefaultLocation", out value);
                //if (value != null)
                //{
                //    if (value.ToString() != "")
                //    {
                //        _Checked = Convert.ToBoolean(value);
                //    }
                //}
                //value = null;
                //ogloSettings.GetSetting("ExportToDefaultLocationPath", out value);

                //if (value != null)
                //{
                //    _DefaultLocationPath = value.ToString();
                //}

                gloSettings.DatabaseSetting.DataBaseSetting oSettings = new gloSettings.DatabaseSetting.DataBaseSetting();
                if (Convert.ToString(oSettings.ReadSettings_XML("Reports", "ExportToDefaultLocation")) != "")
                {
                    _Checked = Convert.ToBoolean(oSettings.ReadSettings_XML("Reports", "ExportToDefaultLocation"));
                }
                else
                {
                    _Checked = false;
                }
                _DefaultLocationPath = Convert.ToString(oSettings.ReadSettings_XML("Reports", "ExportToDefaultLocationPath"));
                oSettings.Dispose();

                if (_DefaultLocationPath != "" && _Checked == true)
                {
                    if (_DefaultLocationPath.EndsWith("\\"))
                    {
                        char[] trimChars = { '\\' };
                        _DefaultLocationPath = _DefaultLocationPath.TrimEnd(trimChars);
                    }
                    // If not exist create directory
                    if (Directory.Exists(_DefaultLocationPath) == false)
                    {
                        Directory.CreateDirectory(_DefaultLocationPath);
                    }

                    _FilePath = _DefaultLocationPath + "\\Patient Report";

                    _FilePath += Convert.ToString(DateTime.Now).Replace(":", "");
                    _FilePath = _FilePath.Replace("/", "") + ".xls";
                }
                else
                {
                    FileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Excel File(.xls)|*.xls";
                    saveFileDialog.DefaultExt = ".xls";
                    saveFileDialog.AddExtension = true;
                    if (saveFileDialog.ShowDialog(this) != DialogResult.OK)
                    {
                        saveFileDialog.Dispose();
                        saveFileDialog = null;
                        return;
                    }
                    _FilePath = saveFileDialog.FileName;
                    saveFileDialog.Dispose();
                    saveFileDialog = null;
                }

                C1Patients.SaveExcel(_FilePath, "sheet1", C1.Win.C1FlexGrid.FileFlags.IncludeFixedCells);

                if (OpenReport == true)
                {
                    if (File.Exists(_FilePath) == true)
                    {
                        System.Diagnostics.Process.Start(_FilePath);
                    }
                }
                else
                {
                    MessageBox.Show("File saved successfully.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (IOException)// ioEx)
            {
                MessageBox.Show("File in use. Fail to export report.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //ioEx.ToString();
                //ioEx = null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }




        #region "Filter by date "

        #region "for dates criteria 1"

        private void Fill_FilterDatesCombo()
        {
            try
            {
                cmb_datefilter.Items.Clear();
                cmb_datefilter.Items.Add("Custom");
                cmb_datefilter.Items.Add("Today");
                cmb_datefilter.Items.Add("Yesterday");
                cmb_datefilter.Items.Add("This Week");
                cmb_datefilter.Items.Add("Last Week");
                cmb_datefilter.Items.Add("Current Month");
                cmb_datefilter.Items.Add("Last Month");
                cmb_datefilter.Items.Add("Current Year");
                cmb_datefilter.Items.Add("Last 30 Days");
                cmb_datefilter.Items.Add("Last 60 Days");
                cmb_datefilter.Items.Add("Last 90 Days");
                cmb_datefilter.Items.Add("Last 120 Days");
                cmb_datefilter.Refresh();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }

        private void cmb_datefilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            int _filterby = 0;

            _filterby = cmb_datefilter.SelectedIndex;
            switch (_filterby)
            {
                case 0://Date Range
                    FilterBy_DateRange();
                    break;

                case 1://Today
                    FilterBy_Today();
                    break;

                case 2://Yesterday
                    FilterBy_Yesterday();
                    break;

                case 3://This week
                    FilterBy_Thisweek();
                    break;

                case 4://Last Week
                    FilterBy_lastweek();
                    break;

                case 5://Current Month
                    FilterBy_currentmonth();
                    break;

                case 6://Last Month
                    FilterBy_lastmonth();
                    break;

                case 7://Current Year
                    FilterBy_currenYear();
                    break;

                case 8://Last 30 days
                    FilterBy_last30days();
                    break;

                case 9://Last 60 days
                    FilterBy_last60days();
                    break;

                case 10://Last 90 days
                    FilterBy_last90days();
                    break;

                case 11://Last 120 days
                    FilterBy_last120days();
                    break;
            }

        }

        #region " Methods "

        private void FilterBy_Today()
        {

            dtpStartDate.Value = DateTime.Today;
            dtpEndDate.Value = DateTime.Today;

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;

        }

        private void FilterBy_Yesterday()
        {
            dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(24, 0, 0));
            dtpEndDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(24, 0, 0));

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;
        }

        private void FilterBy_Thisweek()
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

        private void FilterBy_lastweek()
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

        private void FilterBy_currentmonth()
        {
            DateTime dtFrom = new DateTime(dtpStartDate.Value.Year, dtpStartDate.Value.Month, 1);

            // for any date passed in to the method
            // create a datetime variable set to the passed in date
            DateTime dtTo = new DateTime(DateTime.Now.Year, dtpStartDate.Value.Month, 1);
            // overshoot the date by a month

            dtTo = dtTo.AddMonths(1);
            // remove all of the days in the next month
            // to get bumped down to the last day of the 
            // previous month
            dtTo = dtTo.AddDays(-(dtTo.Day));
            dtpStartDate.Value = Convert.ToDateTime(dtFrom.Date);
            dtpEndDate.Value = Convert.ToDateTime(dtTo.Date);

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;


        }

        private void FilterBy_lastmonth()
        {
            DateTime firstDay = new DateTime(DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month, 1);

            int DaysinMonth = DateTime.DaysInMonth(DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month);

            DateTime lastDay = firstDay.AddMonths(1).AddTicks(-1);

            dtpStartDate.Value = Convert.ToDateTime(firstDay.Date);
            dtpEndDate.Value = Convert.ToDateTime(lastDay.Date);

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;

        }

        private void FilterBy_currenYear()
        {

            DateTime dtFrom = new DateTime(DateTime.Now.Year, 1, 1);

            dtpStartDate.Value = Convert.ToDateTime(dtFrom.Date);
            dtpEndDate.Value = DateTime.Today;

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;
        }

        private void FilterBy_last30days()
        {

            dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(30, 0, 0, 0));
            dtpEndDate.Value = DateTime.Today;

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;
        }

        private void FilterBy_last60days()
        {
            dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(60, 0, 0, 0));
            dtpEndDate.Value = DateTime.Today;

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;

        }

        private void FilterBy_last90days()
        {

            dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(90, 0, 0, 0));
            dtpEndDate.Value = DateTime.Today;

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;

        }

        private void FilterBy_last120days()
        {

            dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(120, 0, 0, 0));
            dtpEndDate.Value = DateTime.Today;

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;

        }

        private void FilterBy_DateRange()
        {

            dtpStartDate.Value = DateTime.Today;
            dtpEndDate.Value = DateTime.Today;

            dtpStartDate.Enabled = true;
            dtpEndDate.Enabled = true;

        }

        #endregion

        #endregion


        #region "for Dates criteria 2 "
        private void Fill_FilterDatesCombo1()
        {
            try
            {
                cmb_datefilter1.Items.Clear();
                cmb_datefilter1.Items.Add("Custom");
                cmb_datefilter1.Items.Add("Today");
                cmb_datefilter1.Items.Add("Yesterday");
                cmb_datefilter1.Items.Add("This Week");
                cmb_datefilter1.Items.Add("Last Week");
                cmb_datefilter1.Items.Add("Current Month");
                cmb_datefilter1.Items.Add("Last Month");
                cmb_datefilter1.Items.Add("Current Year");
                cmb_datefilter1.Items.Add("Last 30 Days");
                cmb_datefilter1.Items.Add("Last 60 Days");
                cmb_datefilter1.Items.Add("Last 90 Days");
                cmb_datefilter1.Items.Add("Last 120 Days");
                cmb_datefilter1.Refresh();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }


        #region " Methods for dates critreria 2"

        private void FilterBy_Today_new()
        {

            dtpDOSFrom.Value = DateTime.Today;
            dtpDOSTo.Value = DateTime.Today;

            dtpDOSFrom.Enabled = false;
            dtpDOSTo.Enabled = false;

        }

        private void FilterBy_Yesterday_new()
        {
            dtpDOSFrom.Value = DateTime.Now.Date.Subtract(new TimeSpan(24, 0, 0));
            dtpDOSTo.Value = DateTime.Now.Date.Subtract(new TimeSpan(24, 0, 0));

            dtpDOSFrom.Enabled = false;
            dtpDOSTo.Enabled = false;
        }

        private void FilterBy_Thisweek_new()
        {

            if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
            {
                dtpDOSFrom.Value = DateTime.Today;
                dtpDOSTo.Value = DateTime.Now.Date.AddDays(6);

            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday)
            {
                dtpDOSFrom.Value = DateTime.Now.Date.Subtract(new TimeSpan(1, 0, 0, 0));
                dtpDOSTo.Value = dtpDOSFrom.Value.AddDays(6);
            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday)
            {
                dtpDOSFrom.Value = DateTime.Now.Date.Subtract(new TimeSpan(2, 0, 0, 0));
                dtpDOSTo.Value = dtpDOSFrom.Value.AddDays(6);
            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Thursday)
            {
                dtpDOSFrom.Value = DateTime.Now.Date.Subtract(new TimeSpan(3, 0, 0, 0));
                dtpDOSTo.Value = dtpDOSFrom.Value.AddDays(6);

            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                dtpDOSFrom.Value = DateTime.Now.Date.Subtract(new TimeSpan(4, 0, 0, 0));
                dtpDOSTo.Value = dtpDOSFrom.Value.AddDays(6);
            }

            if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
            {
                dtpDOSFrom.Value = DateTime.Now.Date.Subtract(new TimeSpan(5, 0, 0, 0));
                dtpDOSTo.Value = dtpDOSFrom.Value.AddDays(6);
            }

            if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                dtpDOSFrom.Value = DateTime.Now.Date.Subtract(new TimeSpan(6, 0, 0, 0));
                dtpDOSTo.Value = dtpDOSFrom.Value.AddDays(6);
            }

            dtpDOSFrom.Enabled = false;
            dtpDOSTo.Enabled = false;

        }

        private void FilterBy_lastweek_new()
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
            {
                dtpDOSFrom.Value = DateTime.Now.Date.Subtract(new TimeSpan(7, 0, 0, 0));
                dtpDOSTo.Value = dtpDOSFrom.Value.AddDays(6);

            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday)
            {
                dtpDOSFrom.Value = DateTime.Now.Date.Subtract(new TimeSpan(8, 0, 0, 0));
                dtpDOSTo.Value = dtpDOSFrom.Value.AddDays(6);
            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday)
            {
                dtpDOSFrom.Value = DateTime.Now.Date.Subtract(new TimeSpan(9, 0, 0, 0));
                dtpDOSTo.Value = dtpDOSFrom.Value.AddDays(6);
            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Thursday)
            {
                dtpDOSFrom.Value = DateTime.Now.Date.Subtract(new TimeSpan(10, 0, 0, 0));
                dtpDOSTo.Value = dtpDOSFrom.Value.AddDays(6);

            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                dtpDOSFrom.Value = DateTime.Now.Date.Subtract(new TimeSpan(11, 0, 0, 0));
                dtpDOSTo.Value = dtpDOSFrom.Value.AddDays(6);
            }

            if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
            {
                dtpDOSFrom.Value = DateTime.Now.Date.Subtract(new TimeSpan(12, 0, 0, 0));
                dtpDOSTo.Value = dtpDOSFrom.Value.AddDays(6);
            }

            if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                dtpDOSFrom.Value = DateTime.Now.Date.Subtract(new TimeSpan(13, 0, 0, 0));
                dtpDOSTo.Value = dtpDOSFrom.Value.AddDays(6);
            }

            dtpDOSFrom.Enabled = false;
            dtpDOSTo.Enabled = false;

        }

        private void FilterBy_currentmonth_new()
        {
            DateTime dtFrom = new DateTime(dtpDOSFrom.Value.Year, dtpDOSFrom.Value.Month, 1);

            // for any date passed in to the method
            // create a datetime variable set to the passed in date
            DateTime dtTo = new DateTime(DateTime.Now.Year, dtpDOSFrom.Value.Month, 1);
            // overshoot the date by a month

            dtTo = dtTo.AddMonths(1);
            // remove all of the days in the next month
            // to get bumped down to the last day of the 
            // previous month
            dtTo = dtTo.AddDays(-(dtTo.Day));
            dtpDOSFrom.Value = Convert.ToDateTime(dtFrom.Date);
            dtpDOSTo.Value = Convert.ToDateTime(dtTo.Date);

            dtpDOSFrom.Enabled = false;
            dtpDOSTo.Enabled = false;


        }

        private void FilterBy_lastmonth_new()
        {
            DateTime firstDay = new DateTime(DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month, 1);

            int DaysinMonth = DateTime.DaysInMonth(DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month);

            DateTime lastDay = firstDay.AddMonths(1).AddTicks(-1);

            dtpDOSFrom.Value = Convert.ToDateTime(firstDay.Date);
            dtpDOSTo.Value = Convert.ToDateTime(lastDay.Date);

            dtpDOSFrom.Enabled = false;
            dtpDOSTo.Enabled = false;

        }

        private void FilterBy_currenYear_new()
        {

            DateTime dtFrom = new DateTime(DateTime.Now.Year, 1, 1);

            dtpDOSFrom.Value = Convert.ToDateTime(dtFrom.Date);
            dtpDOSTo.Value = DateTime.Today;

            dtpDOSFrom.Enabled = false;
            dtpDOSTo.Enabled = false;
        }

        private void FilterBy_last30days_new()
        {

            dtpDOSFrom.Value = DateTime.Now.Date.Subtract(new TimeSpan(30, 0, 0, 0));
            dtpDOSTo.Value = DateTime.Today;

            dtpDOSFrom.Enabled = false;
            dtpDOSTo.Enabled = false;
        }

        private void FilterBy_last60days_new()
        {
            dtpDOSFrom.Value = DateTime.Now.Date.Subtract(new TimeSpan(60, 0, 0, 0));
            dtpDOSTo.Value = DateTime.Today;

            dtpDOSFrom.Enabled = false;
            dtpDOSTo.Enabled = false;

        }

        private void FilterBy_last90days_new()
        {

            dtpDOSFrom.Value = DateTime.Now.Date.Subtract(new TimeSpan(90, 0, 0, 0));
            dtpDOSTo.Value = DateTime.Today;

            dtpDOSFrom.Enabled = false;
            dtpDOSTo.Enabled = false;

        }

        private void FilterBy_last120days_new()
        {

            dtpDOSFrom.Value = DateTime.Now.Date.Subtract(new TimeSpan(120, 0, 0, 0));
            dtpDOSTo.Value = DateTime.Today;

            dtpDOSFrom.Enabled = false;
            dtpDOSTo.Enabled = false;

        }

        private void FilterBy_DateRange_new()
        {

            dtpDOSFrom.Value = DateTime.Today;
            dtpDOSTo.Value = DateTime.Today;

            dtpDOSFrom.Enabled = true;
            dtpDOSTo.Enabled = true;

        }

        #endregion

        private void cmb_datefilter1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int _filterby = 0;

            _filterby = cmb_datefilter1.SelectedIndex;
            switch (_filterby)
            {
                case 0://Date Range
                    FilterBy_DateRange_new();
                    break;

                case 1://Today
                    FilterBy_Today_new();
                    break;

                case 2://Yesterday
                    FilterBy_Yesterday_new();
                    break;

                case 3://This week
                    FilterBy_Thisweek_new();
                    break;

                case 4://Last Week
                    FilterBy_lastweek_new();
                    break;

                case 5://Current Month
                    FilterBy_currentmonth_new();
                    break;

                case 6://Last Month
                    FilterBy_lastmonth_new();
                    break;

                case 7://Current Year
                    FilterBy_currenYear_new();
                    break;

                case 8://Last 30 days
                    FilterBy_last30days_new();
                    break;

                case 9://Last 60 days
                    FilterBy_last60days_new();
                    break;

                case 10://Last 90 days
                    FilterBy_last90days_new();
                    break;

                case 11://Last 120 days
                    FilterBy_last120days_new();
                    break;
            }
        }
        #endregion

        private void btn_MouseHover(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = global::gloBilling.Properties.Resources.Img_LongYellow;
            ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btn_MouseLeave(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = global::gloBilling.Properties.Resources.Img_LongButton;
            ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
        }


        #endregion "Filter by date"

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                dtpEndDate.Value = dtpStartDate.Value;
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

        private void dtpDOSFrom_ValueChanged(object sender, EventArgs e)
        {
            try
            {dtpDOSTo.Value = dtpDOSFrom.Value;}
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

  

     

    }
}