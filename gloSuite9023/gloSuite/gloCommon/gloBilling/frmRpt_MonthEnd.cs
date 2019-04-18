using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Linq;

namespace gloBilling
{
    public partial class frmRpt_MonthEnd : Form
    {
        #region " Private Variables "

        private string _databaseconnectionstring = "";
        private Int64 _clinicId = 0;
        private string _messageBoxCaption = String.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        gloListControl.gloListControl oListControl = null;
        gloListControl.gloListControlType _CurrentControlType = gloListControl.gloListControlType.Other;
        int _codeRevision = 0; //1-All, 9-ICD9, 10-ICD10 to maintain diagnosis type selection
        const int COL_BILL_DOS = 0;
        const int COL_BILL_TRANSACTIONDATE = 1;
        const int COL_BILL_POS = 2;
        const int COL_BILL_TOS = 3;
        const int COL_BILL_CPT = 4;
        const int COL_BILL_DX1 = 5;
        const int COL_BILL_DX2 = 6;
        const int COL_BILL_DX3 = 7;
        const int COL_BILL_DX4 = 8;
        const int COL_BILL_MOD1 = 9;
        const int COL_BILL_MOD2 = 10;
        const int COL_BILL_CHARGES = 11;
        const int COL_BILL_TOTAL = 12;
        const int COL_BILL_ALLOWED = 13;
        const int COL_BILL_PATIENT = 14;
        const int COL_BILL_PROVIDER = 15;
        const int COL_BILL_INSURANCE = 16;
        const int COL_BILL_COUNT = 17;

        const int COL_RE_TRANSACTIONDATE = 0;
        const int COL_RE_PAYMENTDATE = 1;
        const int COL_RE_CLAIMNO = 2;
        const int COL_RE_POS = 3;
        const int COL_RE_TOS = 4;
        const int COL_RE_CPT = 5;
        const int COL_RE_DX1 = 6;
        const int COL_RE_DX2 = 7;
        const int COL_RE_DX3 = 8;
        const int COL_RE_DX4 = 9;
        const int COL_RE_MOD1 = 10;
        const int COL_RE_MOD2 = 11;
        const int COL_RE_CHARGES = 12;
        const int COL_RE_TOTAL = 13;
        const int COL_RE_ALLOWED = 14;
        const int COL_RE_PAID_SELF = 15;
        const int COL_RE_PAID_INSURANCE = 16;  
        const int COL_RE_TRANSACTIONTYPE = 17;
        const int COL_RE_PATIENT = 18;
        const int COL_RE_PROVIDER = 19;
        const int COL_RE_INSURANCE = 20;
        const int COL_RE_COUNT = 21;

        const int COL_WOFF_TRANSACTIONDATE = 0;
        const int COL_WOFF_DOS = 1;
        const int COL_WOFF_CLAIMNO = 2;
        const int COL_WOFF_POS = 3;
        const int COL_WOFF_TOS = 4;
        const int COL_WOFF_CPT = 5;
        const int COL_WOFF_DX1 = 6;
        const int COL_WOFF_DX2 = 7;
        const int COL_WOFF_DX3 = 8;
        const int COL_WOFF_DX4 = 9;
        const int COL_WOFF_MOD1 = 10;
        const int COL_WOFF_MOD2 = 11;
        const int COL_WOFF_CHARGES = 12;
        const int COL_WOFF_TOTAL = 13;
        const int COL_WOFF_ALLOWED = 14;
        const int COL_WOFF_PAID = 15;
        const int COL_WOFF_WRITEOFF = 16;
        const int COL_WOFF_BALANCE = 17;
        const int COL_WOFF_PATIENT = 18;
        const int COL_WOFF_PROVIDER = 19;
        const int COL_WOFF_INSURANCE = 20;
        const int COL_WOFF_COUNT = 21;

        #endregion

        #region "Contructor"

        public frmRpt_MonthEnd(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _clinicId = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _clinicId = 0; }
            }
            else
            { _clinicId = 0; }



            InitializeComponent();

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

        #endregion

        private void frmRpt_MonthEnd_Load(object sender, EventArgs e)
        {

            gloC1FlexStyle.Style(C1Report,false );
            
            FillFacility();
            FillPaymentModes();
            FillChargesTray();
            if (C1Report.Rows.Count <= 1)
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

        private void FillPaymentModes()
        {
            try
            {
                #region "Fill Payment Mode"
                //cmbPaymentMode.Items.Clear();
                //cmbPaymentMode.Items.Add(PaymentMode.None.ToString());
                //cmbPaymentMode.Items.Add(PaymentMode.Cash.ToString());
                //cmbPaymentMode.Items.Add(PaymentMode.Check.ToString());
                //cmbPaymentMode.Items.Add(PaymentMode.CreditCard.ToString());
                //cmbPaymentMode.Items.Add(PaymentMode.MoneyOrder.ToString());

                //for (int i = 0; i <= cmbPaymentMode.Items.Count - 1; i++)
                //{
                //    if (cmbPaymentMode.Items[i].ToString() == PaymentMode.None.ToString())
                //    {
                //        cmbPaymentMode.SelectedIndex = i;
                //        break;
                //    }
                //}
                #endregion
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }

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

                      
                        //oBindRow["Desc"] = Convert.ToString(dtFacilities.Rows[i]["sFacilityCode"]) + " - " + Convert.ToString(dtFacilities.Rows[i]["sFacilityName"]);
                      
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

        //Added By Pramod Nair For including Charges Tray Criteria 20090826
        private void FillChargesTray()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable _dtChargesTray;
            DataTable dtChargesTray = new DataTable();
            try
            {
               // cmbChargesTray.Items.Clear();
                cmbChargesTray.DataSource = null;
                cmbChargesTray.Items.Clear();
                oDB.Connect(false);

                oDB.Retrive_Query(" select nChargeTrayID,sDescription from BL_ChargesTray", out  _dtChargesTray);

                if (_dtChargesTray != null && _dtChargesTray.Rows.Count > 0)
                {
                    dtChargesTray.Columns.Add("nChargeTrayID");
                    dtChargesTray.Columns.Add("sDescription");

                    dtChargesTray.Clear();
                    dtChargesTray.Rows.Add(0, "");

                    for (int i = 0; i < _dtChargesTray.Rows.Count; i++)
                    {
                        dtChargesTray.Rows.Add(_dtChargesTray.Rows[i]["nChargeTrayID"], _dtChargesTray.Rows[i]["sDescription"]);
                    }

                    cmbChargesTray.DataSource = dtChargesTray;
                    cmbChargesTray.DisplayMember = "sDescription";
                    cmbChargesTray.ValueMember = "nChargeTrayID";
                }

                dtChargesTray = null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
        }

        private void ExportReportToExcel(bool OpenReport)
        {
          //  gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
            string _DefaultLocationPath = "";
            string _FilePath = "";
            bool _Checked = false;
            try
            {
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

                FileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel File(.xls)|*.xls";
                saveFileDialog.DefaultExt = ".xls";
                saveFileDialog.AddExtension = true;

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
                    saveFileDialog.InitialDirectory = _DefaultLocationPath;

                    //if (rbAmountBilled.Checked == true)
                    //{
                    //    _FilePath = _DefaultLocationPath + "\\Charge Summary Amount Billed ";
                    //}
                    //if (rbMoneyReceived.Checked == true)
                    //{
                    //    _FilePath = _DefaultLocationPath + "\\Charge Summary Money Received ";
                    //}
                    //if (rbPatient_VS_Insurance.Checked == true)
                    //{
                    //    _FilePath = _DefaultLocationPath + "\\Charge Summary Patient Vs Insurance";
                    //}
                    //if (rbWriteOff.Checked == true)
                    //{
                    //    _FilePath = _DefaultLocationPath + "\\Charge Summary Write Off";
                    //}

                    //_FilePath += Convert.ToString(DateTime.Now).Replace(":", "");
                    //_FilePath = _FilePath.Replace("/", "") + ".xls";
                }
         
                if (saveFileDialog.ShowDialog(this) != DialogResult.OK)
                {
                    saveFileDialog.Dispose();
                    saveFileDialog = null;
                    return;
                }
                _FilePath = saveFileDialog.FileName;
                saveFileDialog.Dispose();
                saveFileDialog = null;
                C1Report.SaveExcel(_FilePath, "sheet1", C1.Win.C1FlexGrid.FileFlags.IncludeFixedCells);

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
                    //gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.MonthEndReport, gloAuditTrail.ActivityType.ExportToExcel, "Save Report to excel sheet", gloAuditTrail.ActivityOutCome.Success);
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

        #region "Tool Strip Button Events"

        private void tls_btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tls_btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (rbAmountBilled.Checked == true)
                {
                    ShowReport_AmountBilled();
                }
                else if (rbMoneyReceived.Checked == true)
                {
                    ShowReport_MoneyReceived();
                }
                else if (rbPatient_VS_Insurance.Checked == true)
                {
                    ShowReport_PatientInsurancePayment();
                }
                else if (rbWriteOff.Checked == true)
                {
                    ShowReport_WriteOff();
                }
                if (C1Report.Rows.Count <= 1)
                {
                    tls_btnExportToExcel.Enabled = false;
                    tls_btnExportToExcelOpen.Enabled = false;
                }
                else
                {
                    tls_btnExportToExcel.Enabled = true;
                    tls_btnExportToExcelOpen.Enabled = true;
                }

                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.MonthEndReport, gloAuditTrail.ActivityType.View, "View Report", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                   
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void tls_btnExportToExcel_Click(object sender, EventArgs e)
        {
            if (C1Report != null && C1Report.Rows.Count > 1)
            {
                ExportReportToExcel(false);
            }
        }

        private void tls_btnExportToExcelOpen_Click(object sender, EventArgs e)
        {
            if (C1Report != null && C1Report.Rows.Count > 1)
            {
                ExportReportToExcel(true);
            }
        }

       
        #endregion

        #region "List Control Eventes"

        private void btnBrowsePatient_Click(object sender, EventArgs e)
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
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Patient, true, this.Width);
                oListControl.ClinicID = _clinicId;
                oListControl.ControlHeader = " Patient";

                _CurrentControlType = gloListControl.gloListControlType.Patient;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                this.Controls.Add(oListControl);

                if (cmbPatients.DataSource != null)
                {
                    for (int i = 0; i < cmbPatients.Items.Count; i++)
                    {
                        cmbPatients.SelectedIndex = i;
                        cmbPatients.Refresh();
                        oListControl.SelectedItems.Add(Convert.ToInt64(cmbPatients.SelectedValue), cmbPatients.Text);
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

        private void btnClearPatient_Click(object sender, EventArgs e)
        {
           // cmbPatients.Items.Clear();
            cmbPatients.DataSource = null;
            cmbPatients.Refresh();
        }

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
           // cmbProvider.Items.Clear();
            cmbProvider.DataSource = null;
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
                oListControl.ShowAllDiagnosis = true;
                oListControl._DiagnosisType = _codeRevision;
                _CurrentControlType = gloListControl.gloListControlType.Diagnosis;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                

                if (cmbDiagnosisCode.DataSource != null)
                {
                    for (int i = 0; i < cmbDiagnosisCode.Items.Count; i++)
                    {
                        cmbDiagnosisCode.SelectedIndex = i;
                        cmbDiagnosisCode.Refresh();
                        oListControl.SelectedItems.Add(Convert.ToInt64(cmbDiagnosisCode.SelectedValue), cmbDiagnosisCode.Text, "");

                    }
                }
                this.Controls.Add(oListControl);
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
          //  cmbDiagnosisCode.Items.Clear();
            cmbDiagnosisCode.DataSource = null;
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
          //  cmbInsurance.Items.Clear();
            cmbInsurance.DataSource = null;
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
           // cmbCPT.Items.Clear();
            cmbCPT.DataSource = null;
            cmbCPT.Refresh();
        }

        void oListControl_ItemSelectedClick(object sender, EventArgs e)
        {
            int _Counter = 0;

            switch (_CurrentControlType)
            {

                case gloListControl.gloListControlType.Patient:
                    {
                       
                        cmbPatients.DataSource = null;
                        cmbPatients.Items.Clear();
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

                            cmbPatients.DataSource = oBindTable;
                            cmbPatients.DisplayMember = "DispName";
                            cmbPatients.ValueMember = "ID";
                           
                        }
                    }
                    break;
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
                            _codeRevision = oListControl._DiagnosisType;
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

        #region " Amount Billed Report "

        private void DesignGrid_AmountBilled()
        {
            C1Report.Rows.Fixed = 1;
            C1Report.Cols.Fixed = 0;
            C1Report.Rows.Count = 1;
            C1Report.Cols.Count = COL_BILL_COUNT;

            C1Report.SetData(0, COL_BILL_DOS, "DOS");
            C1Report.SetData(0, COL_BILL_TRANSACTIONDATE, "Date");
            C1Report.SetData(0, COL_BILL_POS, "POS");
            C1Report.SetData(0, COL_BILL_TOS, "TOS");
            C1Report.SetData(0, COL_BILL_CPT, "CPT");
            C1Report.SetData(0, COL_BILL_DX1, "Dx1");
            C1Report.SetData(0, COL_BILL_DX2, "Dx2");
            C1Report.SetData(0, COL_BILL_DX3, "Dx3");
            C1Report.SetData(0, COL_BILL_DX4, "Dx4");
            C1Report.SetData(0, COL_BILL_MOD1, "Mod1");
            C1Report.SetData(0, COL_BILL_MOD2, "Mod2");
            C1Report.SetData(0, COL_BILL_CHARGES, "Charges");
            C1Report.SetData(0, COL_BILL_TOTAL, "Total");
            C1Report.SetData(0, COL_BILL_ALLOWED, "Allowed");
            C1Report.SetData(0, COL_BILL_PATIENT, "Patient");
            C1Report.SetData(0, COL_BILL_PROVIDER, "Provider");
            C1Report.SetData(0, COL_BILL_INSURANCE, "Insurance");


            C1Report.Cols[COL_BILL_DOS].DataType = typeof(System.DateTime);
            C1Report.Cols[COL_BILL_TRANSACTIONDATE].DataType = typeof(System.DateTime);

            int _width = pnlCriteria.Width - 5;

            C1Report.Cols[COL_BILL_DOS].Width = Convert.ToInt32(_width * 0.06);
            C1Report.Cols[COL_BILL_TRANSACTIONDATE].Width = Convert.ToInt32(_width * 0.06);
            C1Report.Cols[COL_BILL_POS].Width = Convert.ToInt32(_width * 0.05);
            C1Report.Cols[COL_BILL_TOS].Width = Convert.ToInt32(_width * 0.05);
            C1Report.Cols[COL_BILL_CPT].Width = Convert.ToInt32(_width * 0.06);
            C1Report.Cols[COL_BILL_DX1].Width = Convert.ToInt32(_width * 0.045);
            C1Report.Cols[COL_BILL_DX2].Width = Convert.ToInt32(_width * 0.045);
            C1Report.Cols[COL_BILL_DX3].Width = Convert.ToInt32(_width * 0.045);
            C1Report.Cols[COL_BILL_DX4].Width = Convert.ToInt32(_width * 0.045);
            C1Report.Cols[COL_BILL_MOD1].Width = Convert.ToInt32(_width * 0.045);
            C1Report.Cols[COL_BILL_MOD2].Width = Convert.ToInt32(_width * 0.045);
            C1Report.Cols[COL_BILL_CHARGES].Width = Convert.ToInt32(_width * 0.06);
            C1Report.Cols[COL_BILL_TOTAL].Width = Convert.ToInt32(_width * 0.06);
            C1Report.Cols[COL_BILL_ALLOWED].Width = Convert.ToInt32(_width * 0.06);
            C1Report.Cols[COL_BILL_PATIENT].Width = Convert.ToInt32(_width * 0.1);
            C1Report.Cols[COL_BILL_PROVIDER].Width = Convert.ToInt32(_width * 0.1);
            C1Report.Cols[COL_BILL_INSURANCE].Width = Convert.ToInt32(_width * 0.11);

            C1Report.Cols[COL_BILL_DOS].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1Report.Cols[COL_BILL_TRANSACTIONDATE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1Report.Cols[COL_BILL_POS].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1Report.Cols[COL_BILL_TOS].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1Report.Cols[COL_BILL_CPT].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1Report.Cols[COL_BILL_DX1].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1Report.Cols[COL_BILL_DX2].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1Report.Cols[COL_BILL_DX3].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1Report.Cols[COL_BILL_DX4].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1Report.Cols[COL_BILL_MOD1].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1Report.Cols[COL_BILL_MOD2].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1Report.Cols[COL_BILL_CHARGES].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
            C1Report.Cols[COL_BILL_TOTAL].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
            C1Report.Cols[COL_BILL_ALLOWED].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
            C1Report.Cols[COL_BILL_PATIENT].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1Report.Cols[COL_BILL_PROVIDER].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1Report.Cols[COL_BILL_INSURANCE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

            C1Report.Cols[COL_BILL_DOS].Visible = true; 
            C1Report.Cols[COL_BILL_TRANSACTIONDATE].Visible = true; 
            C1Report.Cols[COL_BILL_POS].Visible = true; 
            C1Report.Cols[COL_BILL_TOS].Visible = true; 
            C1Report.Cols[COL_BILL_CPT].Visible = true; 
            C1Report.Cols[COL_BILL_DX1].Visible = true; 
            C1Report.Cols[COL_BILL_DX2].Visible = true; 
            C1Report.Cols[COL_BILL_DX3].Visible = true; 
            C1Report.Cols[COL_BILL_DX4].Visible = true; 
            C1Report.Cols[COL_BILL_MOD1].Visible = true; 
            C1Report.Cols[COL_BILL_MOD2].Visible = true; 
            C1Report.Cols[COL_BILL_CHARGES].Visible = true; 
            C1Report.Cols[COL_BILL_TOTAL].Visible = true; 
            C1Report.Cols[COL_BILL_ALLOWED].Visible = true; 
            C1Report.Cols[COL_BILL_PATIENT].Visible = true; 
            C1Report.Cols[COL_BILL_PROVIDER].Visible = true; 
            C1Report.Cols[COL_BILL_INSURANCE].Visible = true; 
        }

        private void ShowReport_AmountBilled()
        {
            DataTable dtReport = new DataTable();
            try
            {
                DesignGrid_AmountBilled();
                dtReport = GetAmountBilled();
                //sPOSCode,sPOSDescriptionAS,sTOSCode,sTOSDescription ,sCPTCode,sCPTDescription ,sDx1Code,sDx1Description,sDx2Code,sDx2Description,
                //sDx3Code,sDx3Description,sDx4Code,sDx4Description,sMod1Code,sMod1Description,sMod2Code,sMod2Description ,dCharges ,dUnit ,dTotal ,dAllowed
                //nClaimNo ,sPatientName ,sRenderringProvider,sBillingProvider ,sInsuranceName ,dtDOSFrom ,dtDOSTo,nTransactionDate

                if (dtReport != null)
                {
                    Decimal dTotalBilledAmount = 0;
                    for (int i = 0; i < dtReport.Rows.Count; i++)
                    {
                        C1.Win.C1FlexGrid.Row NewRow = C1Report.Rows.Add();

                        C1Report.SetData(NewRow.Index, COL_BILL_DOS, gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtReport.Rows[i]["dtDOSFrom"])));
                        C1Report.SetData(NewRow.Index, COL_BILL_TRANSACTIONDATE, gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtReport.Rows[i]["nTransactionDate"])));
                        C1Report.SetData(NewRow.Index, COL_BILL_POS, Convert.ToString(dtReport.Rows[i]["sPOSCode"]));
                        C1Report.SetData(NewRow.Index, COL_BILL_TOS, Convert.ToString(dtReport.Rows[i]["sTOSCode"]));
                        C1Report.SetData(NewRow.Index, COL_BILL_CPT, Convert.ToString(dtReport.Rows[i]["sCPTCode"]));
                        C1Report.SetData(NewRow.Index, COL_BILL_DX1, Convert.ToString(dtReport.Rows[i]["sDx1Code"]));
                        C1Report.SetData(NewRow.Index, COL_BILL_DX2, Convert.ToString(dtReport.Rows[i]["sDx2Code"]));
                        C1Report.SetData(NewRow.Index, COL_BILL_DX3, Convert.ToString(dtReport.Rows[i]["sDx3Code"]));
                        C1Report.SetData(NewRow.Index, COL_BILL_DX4, Convert.ToString(dtReport.Rows[i]["sDx4Code"]));
                        C1Report.SetData(NewRow.Index, COL_BILL_MOD1, Convert.ToString(dtReport.Rows[i]["sMod1Code"]));
                        C1Report.SetData(NewRow.Index, COL_BILL_MOD2, Convert.ToString(dtReport.Rows[i]["sMod2Code"]));
                        C1Report.SetData(NewRow.Index, COL_BILL_CHARGES, Convert.ToString(dtReport.Rows[i]["dCharges"]));
                        C1Report.SetData(NewRow.Index, COL_BILL_TOTAL, Convert.ToString(dtReport.Rows[i]["dTotal"]));
                        C1Report.SetData(NewRow.Index, COL_BILL_ALLOWED, Convert.ToString(dtReport.Rows[i]["dAllowed"]));
                        C1Report.SetData(NewRow.Index, COL_BILL_PATIENT, Convert.ToString(dtReport.Rows[i]["sPatientName"]));
                        C1Report.SetData(NewRow.Index, COL_BILL_PROVIDER, Convert.ToString(dtReport.Rows[i]["sBillingProvider"]));
                        C1Report.SetData(NewRow.Index, COL_BILL_INSURANCE, Convert.ToString(dtReport.Rows[i]["sInsuranceName"]));

                        dTotalBilledAmount += Convert.ToDecimal(dtReport.Rows[i]["dTotal"]);
                    }
                    C1Report.Sort(C1.Win.C1FlexGrid.SortFlags.Ascending, COL_BILL_DOS);

                    C1.Win.C1FlexGrid.Row TotalRow = C1Report.Rows.Add();
                    C1Report.SetData(TotalRow.Index, COL_BILL_CHARGES, " Total ");
                    C1Report.SetData(TotalRow.Index, COL_BILL_TOTAL, dTotalBilledAmount);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        //private DataTable GetAmountBilled_old()
        //{
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //    string strSQL = "";
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        oDB.Connect(false);

        //        strSQL = "SELECT     ISNULL(BL_Transaction_Lines.sPOSCode, '') AS sPOSCode, ISNULL(BL_Transaction_Lines.sPOSDescription, '') AS sPOSDescriptionAS, "
        //        + " ISNULL(BL_Transaction_Lines.sTOSCode, '') AS sTOSCode, ISNULL(BL_Transaction_Lines.sTOSDescription, '') AS sTOSDescription, "
        //        + " ISNULL(BL_Transaction_Lines.sCPTCode, '') AS sCPTCode, ISNULL(BL_Transaction_Lines.sCPTDescription, '') AS sCPTDescription, "
        //        + " ISNULL(BL_Transaction_Lines.sDx1Code, '') AS sDx1Code, ISNULL(BL_Transaction_Lines.sDx1Description, '') AS sDx1Description, "
        //        + " ISNULL(BL_Transaction_Lines.sDx2Code, '') AS sDx2Code, ISNULL(BL_Transaction_Lines.sDx2Description, '') AS sDx2Description, "
        //        + " ISNULL(BL_Transaction_Lines.sDx3Code, '') AS sDx3Code, ISNULL(BL_Transaction_Lines.sDx3Description, '') AS sDx3Description, "
        //        + " ISNULL(BL_Transaction_Lines.sDx4Code, '') AS sDx4Code, ISNULL(BL_Transaction_Lines.sDx4Description, '') AS sDx4Description, "
        //        + " ISNULL(BL_Transaction_Lines.sMod1Code, '') AS sMod1Code, ISNULL(BL_Transaction_Lines.sMod1Description, '') AS sMod1Description, "
        //        + " ISNULL(BL_Transaction_Lines.sMod2Code, '') AS sMod2Code, ISNULL(BL_Transaction_Lines.sMod2Description, '') AS sMod2Description, "
        //        + " ISNULL(BL_Transaction_Lines.dCharges, 0) AS dCharges, ISNULL(BL_Transaction_Lines.dUnit, 0) AS dUnit, ISNULL(BL_Transaction_Lines.dTotal, 0) AS dTotal, "
        //        + " ISNULL(BL_Transaction_Lines.dAllowed, 0) AS dAllowed, ISNULL(BL_Transaction_MST.nClaimNo, 0) AS nClaimNo, ISNULL(Patient.sFirstName, '') + SPACE(1) "
        //        + " + ISNULL(Patient.sMiddleName, '') + SPACE(1) + ISNULL(Patient.sLastName, '') AS sPatientName, ISNULL(Renderring_Provider.sFirstName, '') + SPACE(1) "
        //        + " + ISNULL(Renderring_Provider.sMiddleName, '') + SPACE(1) + ISNULL(Renderring_Provider.sLastName, '') AS sRenderringProvider, "
        //        + " ISNULL(Billing_Provider.sFirstName, '') + SPACE(1) + ISNULL(Billing_Provider.sMiddleName, '') + SPACE(1) + ISNULL(Billing_Provider.sLastName, '') "
        //        + " AS sBillingProvider, ISNULL(PatientInsurance_DTL.sInsuranceName, '') AS sInsuranceName, BL_Transaction_Lines.nFromDate AS dtDOSFrom, "
        //        + " BL_Transaction_Lines.nToDate AS dtDOSTo, BL_Transaction_MST.nTransactionDate"
        //        + " FROM         Provider_MST AS Billing_Provider INNER JOIN"
        //        + " BL_Transaction_Lines INNER JOIN"
        //        + " BL_Transaction_MST ON BL_Transaction_Lines.nTransactionID = BL_Transaction_MST.nTransactionID INNER JOIN"
        //        + " Patient ON BL_Transaction_MST.nPatientID = Patient.nPatientID ON Billing_Provider.nProviderID = BL_Transaction_MST.nTransactionProviderID LEFT OUTER JOIN"
        //        + " PatientInsurance_DTL RIGHT OUTER JOIN"
        //        + " BL_Transaction_MST_Ins ON PatientInsurance_DTL.nInsuranceID = BL_Transaction_MST_Ins.nInsuranceID ON "
        //        + " BL_Transaction_Lines.nTransactionID = BL_Transaction_MST_Ins.nTransactionID AND "
        //        + " BL_Transaction_Lines.nTransactionDetailID = BL_Transaction_MST_Ins.nTransactionDetailID AND "
        //        + " BL_Transaction_Lines.nTransactionLineNo = BL_Transaction_MST_Ins.nTransactionLineNo LEFT OUTER JOIN"
        //        + " Provider_MST AS Renderring_Provider ON BL_Transaction_Lines.nProvider = Renderring_Provider.nProviderID"
        //        + " WHERE BL_Transaction_MST.nClinicID = " + _clinicId + " ";

        //        if (chkFromToDates.Checked == true)
        //        {
        //            strSQL += " AND (BL_Transaction_MST.nTransactionDate >= " + gloDateMaster.gloDate.DateAsNumber(dtpStartDate.Value.ToShortDateString()) + " AND BL_Transaction_MST.nTransactionDate <= " + gloDateMaster.gloDate.DateAsNumber(dtpEndDate.Value.ToShortDateString()) + ") ";
        //        }

        //        if (chkDateOfService.Checked == true)
        //        {
        //            strSQL += " AND (BL_Transaction_Lines.nFromDate >= " + gloDateMaster.gloDate.DateAsNumber(dtpDOSFrom.Value.ToShortDateString()) + " AND BL_Transaction_Lines.nFromDate <= " + gloDateMaster.gloDate.DateAsNumber(dtpDOSTo.Value.ToShortDateString()) + ") ";
        //        }

        //        if (cmbPatients.Items.Count > 0)
        //        {
        //            string _strPatientIDs = "";
        //            for (int i = 0; i < cmbPatients.Items.Count; i++)
        //            {

        //                cmbPatients.SelectedIndex = i;
        //                cmbPatients.Refresh();
        //                if (i == 0)
        //                {
        //                    _strPatientIDs = "(" + Convert.ToInt64(cmbPatients.SelectedValue);
        //                }
        //                else
        //                {
        //                    _strPatientIDs += "," + Convert.ToInt64(cmbPatients.SelectedValue);
        //                }

        //                if (i == cmbPatients.Items.Count - 1)
        //                {
        //                    _strPatientIDs += ")";
        //                }
        //            }

        //            if (_strPatientIDs != "")
        //                strSQL += " AND BL_Transaction_MST.nPatientID IN " + _strPatientIDs + " ";
        //        }
        //        if (cmbProvider.Items.Count > 0)
        //        {
        //            string _strProviderIDs = "";
        //            for (int i = 0; i < cmbProvider.Items.Count; i++)
        //            {

        //                cmbProvider.SelectedIndex = i;
        //                cmbProvider.Refresh();
        //                if (i == 0)
        //                {
        //                    _strProviderIDs = "(" + Convert.ToInt64(cmbProvider.SelectedValue);
        //                }
        //                else
        //                {
        //                    _strProviderIDs += "," + Convert.ToInt64(cmbProvider.SelectedValue);
        //                }

        //                if (i == cmbProvider.Items.Count - 1)
        //                {
        //                    _strProviderIDs += ")";
        //                }
        //            }

        //            if (_strProviderIDs != "")
        //                strSQL += " AND BL_Transaction_MST.nTransactionProviderID IN " + _strProviderIDs + " ";
        //        }
        //        if (cmbInsurance.Items.Count > 0)
        //        {
        //            string _strInsuranceNames = "";

        //            for (int i = 0; i < cmbInsurance.Items.Count; i++)
        //            {

        //                cmbInsurance.SelectedIndex = i;
        //                cmbInsurance.Refresh();
        //                if (i == 0)
        //                {
        //                    _strInsuranceNames = "('" + Convert.ToString(cmbInsurance.Text).Replace("'","''")  + "'";
        //                }
        //                else
        //                {
        //                    _strInsuranceNames += ",'" + Convert.ToString(cmbInsurance.Text).Replace("'", "''") + "'";
        //                }

        //                if (i == cmbInsurance.Items.Count - 1)
        //                {
        //                    _strInsuranceNames += ")";
        //                }
        //            }

        //            if (_strInsuranceNames != "")
        //                strSQL += " AND PatientInsurance_DTL.sInsuranceName IN " + _strInsuranceNames + " ";
        //        }
        //        if (cmbCPT.Items.Count > 0)
        //        {
        //            string _strCPTCodes = "";

        //            for (int i = 0; i < cmbCPT.Items.Count; i++)
        //            {

        //                cmbCPT.SelectedIndex = i;
        //                if (i == 0)
        //                {
        //                    _strCPTCodes = "('" + cmbCPT.Text.Trim().Replace("'", "''") + "'";
        //                }
        //                else
        //                {
        //                    _strCPTCodes += ",'" + cmbCPT.Text.Trim().Replace("'", "''") + "'";
        //                }

        //                if (i == cmbCPT.Items.Count - 1)
        //                {
        //                    _strCPTCodes += ")";
        //                }
        //            }

        //            if (_strCPTCodes != "")
        //                strSQL += " AND BL_Transaction_Lines.sCPTCode IN " + _strCPTCodes + "  ";
        //        }
        //        if (cmbDiagnosisCode.Items.Count > 0)
        //        {
        //            string _strDiagnosisCodes = "";

        //            for (int i = 0; i < cmbDiagnosisCode.Items.Count; i++)
        //            {

        //                cmbDiagnosisCode.SelectedIndex = i;
        //                cmbDiagnosisCode.Refresh();
        //                if (i == 0)
        //                {
        //                    _strDiagnosisCodes = "('" + cmbDiagnosisCode.Text.Trim().Replace("'", "''") + "'";
        //                }
        //                else
        //                {
        //                    _strDiagnosisCodes += ",'" + cmbDiagnosisCode.Text.Trim().Replace("'", "''") + "'";
        //                }

        //                if (i == cmbDiagnosisCode.Items.Count - 1)
        //                {
        //                    _strDiagnosisCodes += ")";
        //                }
        //            }

        //            if (_strDiagnosisCodes != "")
        //                strSQL += " AND (BL_Transaction_Lines.sDx1Code IN " + _strDiagnosisCodes + " OR BL_Transaction_Lines.sDx2Code IN " + _strDiagnosisCodes + " OR BL_Transaction_Lines.sDx3Code IN " + _strDiagnosisCodes + " OR BL_Transaction_Lines.sDx4Code IN " + _strDiagnosisCodes + " ) ";
        //        }
        //        if (cmbFacility.Text.Trim() != "")
        //        {
        //            strSQL += " AND (BL_Transaction_MST.sFacilityCode = '" + cmbFacility.SelectedValue.ToString() + "') ";
        //        }
        //        if (txtZipCode.Text.Trim() != "")
        //        {
        //            strSQL += " AND (Patient.sZIP = '" + txtZipCode.Text.Trim() + "') ";
        //        }
        //        if (txtCity.Text.Trim() != "")
        //        {
        //            strSQL += " AND (Patient.sCity = '" + txtCity.Text.Trim().Replace("'", "''") + "') ";
        //        }
        //        if (txtState.Text.Trim() != "")
        //        {
        //            strSQL += " AND (Patient.sState = '" + txtState.Text.Trim().Replace("'", "''") + "') ";
        //        }
        //        if (cmbGender.Text.Trim() != "")
        //        {
        //            strSQL += " AND (Patient.sGender = '" + cmbGender.Text.Trim().Replace("'", "''") + "') ";
        //        }

        //        if (cmbChargesTray.SelectedValue.ToString() != "" && cmbChargesTray.SelectedValue.ToString() != "0")
        //        {
        //            strSQL += " AND (BL_Transaction_MST.nChargesDayTrayID = " + cmbChargesTray.SelectedValue + ") ";
        //        }


        //        oDB.Retrive_Query(strSQL, out dt);
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

        //        return null;
        //    }
        //    finally
        //    {
        //        if (oDB != null)
        //        {
        //            oDB.Disconnect();
        //            oDB.Dispose();
        //        }
        //    }
        //    return dt;
        //}

        
        private DataTable GetAmountBilled()
        {
            //code start-Added by kanchan on 20130614to solve bug & optimization of query, commented old function
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strSQL = "";
            DataTable dt = new DataTable();
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                oDB.Connect(false);

                strSQL = "SELECT     ISNULL(BL_Transaction_Lines.sPOSCode, '') AS sPOSCode, ISNULL(BL_Transaction_Lines.sPOSDescription, '') AS sPOSDescriptionAS, "
                + " ISNULL(BL_Transaction_Lines.sTOSCode, '') AS sTOSCode, ISNULL(BL_Transaction_Lines.sTOSDescription, '') AS sTOSDescription, "
                + " ISNULL(BL_Transaction_Lines.sCPTCode, '') AS sCPTCode, ISNULL(BL_Transaction_Lines.sCPTDescription, '') AS sCPTDescription, "
                + " ISNULL(BL_Transaction_Lines.sDx1Code, '') AS sDx1Code, ISNULL(BL_Transaction_Lines.sDx1Description, '') AS sDx1Description, "
                + " ISNULL(BL_Transaction_Lines.sDx2Code, '') AS sDx2Code, ISNULL(BL_Transaction_Lines.sDx2Description, '') AS sDx2Description, "
                + " ISNULL(BL_Transaction_Lines.sDx3Code, '') AS sDx3Code, ISNULL(BL_Transaction_Lines.sDx3Description, '') AS sDx3Description, "
                + " ISNULL(BL_Transaction_Lines.sDx4Code, '') AS sDx4Code, ISNULL(BL_Transaction_Lines.sDx4Description, '') AS sDx4Description, "
                + " ISNULL(BL_Transaction_Lines.sMod1Code, '') AS sMod1Code, ISNULL(BL_Transaction_Lines.sMod1Description, '') AS sMod1Description, "
                + " ISNULL(BL_Transaction_Lines.sMod2Code, '') AS sMod2Code, ISNULL(BL_Transaction_Lines.sMod2Description, '') AS sMod2Description, "
                + " ISNULL(BL_Transaction_Lines.dCharges, 0) AS dCharges, ISNULL(BL_Transaction_Lines.dUnit, 0) AS dUnit, ISNULL(BL_Transaction_Lines.dTotal, 0) AS dTotal, "
                + " ISNULL(BL_Transaction_Lines.dAllowed, 0) AS dAllowed, ISNULL(BL_Transaction_MST.nClaimNo, 0) AS nClaimNo, ISNULL(Patient.sFirstName, '') + SPACE(1) "
                + " + ISNULL(Patient.sMiddleName, '') + SPACE(1) + ISNULL(Patient.sLastName, '') AS sPatientName, ISNULL(Renderring_Provider.sFirstName, '') + SPACE(1) "
                + " + ISNULL(Renderring_Provider.sMiddleName, '') + SPACE(1) + ISNULL(Renderring_Provider.sLastName, '') AS sRenderringProvider, "
                + " ISNULL(Billing_Provider.sFirstName, '') + SPACE(1) + ISNULL(Billing_Provider.sMiddleName, '') + SPACE(1) + ISNULL(Billing_Provider.sLastName, '') "
                + " AS sBillingProvider, ISNULL(PatientInsurance_DTL.sInsuranceName, '') AS sInsuranceName, BL_Transaction_Lines.nFromDate AS dtDOSFrom, "
                + " BL_Transaction_Lines.nToDate AS dtDOSTo, BL_Transaction_MST.nTransactionDate,BL_Transaction_MST.nPatientID,BL_Transaction_MST.nTransactionProviderID"
                + " FROM         Provider_MST AS Billing_Provider INNER JOIN"
                + " BL_Transaction_Lines INNER JOIN"
                + " BL_Transaction_MST ON BL_Transaction_Lines.nTransactionID = BL_Transaction_MST.nTransactionID INNER JOIN"
                + " Patient ON BL_Transaction_MST.nPatientID = Patient.nPatientID ON Billing_Provider.nProviderID = BL_Transaction_MST.nTransactionProviderID LEFT OUTER JOIN"
                + " PatientInsurance_DTL RIGHT OUTER JOIN"
                + " BL_Transaction_MST_Ins ON PatientInsurance_DTL.nInsuranceID = BL_Transaction_MST_Ins.nInsuranceID ON "
                + " BL_Transaction_Lines.nTransactionID = BL_Transaction_MST_Ins.nTransactionID AND "
                + " BL_Transaction_Lines.nTransactionDetailID = BL_Transaction_MST_Ins.nTransactionDetailID AND "
                + " BL_Transaction_Lines.nTransactionLineNo = BL_Transaction_MST_Ins.nTransactionLineNo LEFT OUTER JOIN"
                + " Provider_MST AS Renderring_Provider ON BL_Transaction_Lines.nProvider = Renderring_Provider.nProviderID"
                + " WHERE BL_Transaction_MST.nClinicID = " + _clinicId + " ";

                if (chkFromToDates.Checked == true)
                {
                    strSQL += " AND (BL_Transaction_MST.nTransactionDate >= " + gloDateMaster.gloDate.DateAsNumber(dtpStartDate.Value.ToShortDateString()) + " AND BL_Transaction_MST.nTransactionDate <= " + gloDateMaster.gloDate.DateAsNumber(dtpEndDate.Value.ToShortDateString()) + ") ";
                }

                if (chkDateOfService.Checked == true)
                {
                    strSQL += " AND (BL_Transaction_Lines.nFromDate >= " + gloDateMaster.gloDate.DateAsNumber(dtpDOSFrom.Value.ToShortDateString()) + " AND BL_Transaction_Lines.nFromDate <= " + gloDateMaster.gloDate.DateAsNumber(dtpDOSTo.Value.ToShortDateString()) + ") ";
                }

                              
                if (cmbCPT.Items.Count > 0)
                {
                    string _strCPTCodes = "";

                    for (int i = 0; i < cmbCPT.Items.Count; i++)
                    {

                        cmbCPT.SelectedIndex = i;
                        if (i == 0)
                        {
                            _strCPTCodes = "('" + cmbCPT.Text.Trim().Replace("'", "''") + "'";
                        }
                        else
                        {
                            _strCPTCodes += ",'" + cmbCPT.Text.Trim().Replace("'", "''") + "'";
                        }

                        if (i == cmbCPT.Items.Count - 1)
                        {
                            _strCPTCodes += ")";
                        }
                    }

                    if (_strCPTCodes != "")
                        strSQL += " AND BL_Transaction_Lines.sCPTCode IN " + _strCPTCodes + "  ";
                }
                
                if (cmbFacility.Text.Trim() != "")
                {
                    strSQL += " AND (BL_Transaction_MST.sFacilityCode = '" + cmbFacility.SelectedValue.ToString() + "') ";
                }
                if (txtZipCode.Text.Trim() != "")
                {
                    strSQL += " AND (Patient.sZIP = '" + txtZipCode.Text.Trim() + "') ";
                }
                if (txtCity.Text.Trim() != "")
                {
                    strSQL += " AND (Patient.sCity = '" + txtCity.Text.Trim().Replace("'", "''") + "') ";
                }
                if (txtState.Text.Trim() != "")
                {
                    strSQL += " AND (Patient.sState = '" + txtState.Text.Trim().Replace("'", "''") + "') ";
                }
                if (cmbGender.Text.Trim() != "")
                {
                    strSQL += " AND (Patient.sGender = '" + cmbGender.Text.Trim().Replace("'", "''") + "') ";
                }

                if (cmbChargesTray.SelectedValue.ToString() != "" && cmbChargesTray.SelectedValue.ToString() != "0")
                {
                    strSQL += " AND (BL_Transaction_MST.nChargesDayTrayID = " + cmbChargesTray.SelectedValue + ") ";
                }

                oDB.Retrive_Query(strSQL, out dt);

                if (dt != null && dt.Rows.Count > 0)
                {


                    if (cmbDiagnosisCode.Items.Count > 0)
                    {
                        string[] strID = new string[cmbDiagnosisCode.Items.Count];
                        for (int i = 0; i < cmbDiagnosisCode.Items.Count; i++)
                        {
                            strID[i] = ((System.Data.DataRowView)(cmbDiagnosisCode.Items[i])).Row.ItemArray[1].ToString();
                        }

                        List<string> dxId = new List<string>(strID);
                        strID = null;
                        //Bug #67145: 00000682 : error when printing charge summary report
                        var r = (from result in dt.AsEnumerable() where (dxId.Contains(result["sDx1Code"].ToString().Trim()) || dxId.Contains(result["sDx2Code"].ToString().Trim()) || dxId.Contains(result["sDx3Code"].ToString().Trim()) || dxId.Contains(result["sDx4Code"].ToString().Trim())) select result);
                        if (r!=null && r.Any())
                        {
                            dt = r.CopyToDataTable();
                        }
                        else
                        {
                            dt = dt.Clone();
                        }
                        r = null;
                        dxId = null;

                    }
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (cmbPatients.Items.Count > 0)
                    {
                        string[] strID = new string[cmbPatients.Items.Count];
                        for (int i = 0; i < cmbPatients.Items.Count; i++)
                        {
                            strID[i] = ((System.Data.DataRowView)(cmbPatients.Items[i])).Row.ItemArray[0].ToString();
                        }

                        List<string> dxId = new List<string>(strID);
                        strID = null;
                        var r = (from result in dt.AsEnumerable() where dxId.Contains(result["nPatientID"].ToString()) select result);
                        //Bug #67145: 00000682 : error when printing charge summary report
                        if (r != null && r.Any())
                        {
                            dt = r.CopyToDataTable();
                        }
                        else
                        {
                            dt = dt.Clone();
                        }
                        r = null;
                        dxId = null;

                    }
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (cmbProvider.Items.Count > 0)
                    {
                        string[] strID = new string[cmbProvider.Items.Count];
                        for (int i = 0; i < cmbProvider.Items.Count; i++)
                        {
                            strID[i] = ((System.Data.DataRowView)(cmbProvider.Items[i])).Row.ItemArray[0].ToString();
                        }

                        List<string> dxId = new List<string>(strID);
                        strID = null;
                        var r = (from result in dt.AsEnumerable() where dxId.Contains(result["nTransactionProviderID"].ToString()) select result);
                        //Bug #67145: 00000682 : error when printing charge summary report
                        if (r != null && r.Any())
                        {
                            dt = r.CopyToDataTable();
                        }
                        else
                        {
                            dt = dt.Clone();
                        }
                        r = null;
                        dxId = null;
                    }
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (cmbInsurance.Items.Count > 0)
                    {
                        string[] strID = new string[cmbInsurance.Items.Count];
                        for (int i = 0; i < cmbInsurance.Items.Count; i++)
                        {
                            strID[i] = ((System.Data.DataRowView)(cmbInsurance.Items[i])).Row.ItemArray[1].ToString();
                        }

                        List<string> dxId = new List<string>(strID);
                        strID = null;
                        //Bug #67145: 00000682 : error when printing charge summary report
                        var r = (from result in dt.AsEnumerable() where dxId.Contains(result["sInsuranceName"].ToString().Trim()) select result);
                        if (r != null && r.Any())
                        {
                            dt = r.CopyToDataTable();
                        }
                        else
                        {
                            dt = dt.Clone();
                        }
                        r = null;
                        dxId = null;
                    }
                 }
          
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

                return null;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
            return dt;
            //code end-Added by kanchan on 20130614to solve bug & optimization of query, commented old function
        }
        #endregion

        #region "Money Received Report"

        private void DesignGrid_MoneyReceived()
        {
            C1Report.Rows.Fixed = 1;
            C1Report.Cols.Fixed = 0;
            C1Report.Rows.Count = 1;
            C1Report.Cols.Count = COL_RE_COUNT;

            C1Report.SetData(0, COL_RE_PAYMENTDATE, "Payment Date");
            C1Report.SetData(0, COL_RE_TRANSACTIONDATE, "Date");
            C1Report.SetData(0, COL_RE_CLAIMNO, "Claim No");
            C1Report.SetData(0, COL_RE_POS, "POS");
            C1Report.SetData(0, COL_RE_TOS, "TOS");
            C1Report.SetData(0, COL_RE_CPT, "CPT");
            C1Report.SetData(0, COL_RE_DX1, "Dx1");
            C1Report.SetData(0, COL_RE_DX2, "Dx2");
            C1Report.SetData(0, COL_RE_DX3, "Dx3");
            C1Report.SetData(0, COL_RE_DX4, "Dx4");
            C1Report.SetData(0, COL_RE_MOD1, "Mod1");
            C1Report.SetData(0, COL_RE_MOD2, "Mod2");
            C1Report.SetData(0, COL_RE_CHARGES, "Charges");
            C1Report.SetData(0, COL_RE_TOTAL, "Total");
            C1Report.SetData(0, COL_RE_ALLOWED, "Allowed");
            C1Report.SetData(0, COL_RE_PAID_SELF, "Paid");
            C1Report.SetData(0, COL_RE_TRANSACTIONTYPE, "Type");
            C1Report.SetData(0, COL_RE_PATIENT, "Patient");
            C1Report.SetData(0, COL_RE_PROVIDER, "Provider");
            C1Report.SetData(0, COL_RE_INSURANCE, "Insurance");

           
            C1Report.Cols[COL_RE_PAYMENTDATE].DataType = typeof(System.DateTime);
            C1Report.Cols[COL_RE_TRANSACTIONDATE].DataType = typeof(System.DateTime);

            int _width = pnlCriteria.Width - 5;

            C1Report.Cols[COL_RE_PAYMENTDATE].Width = Convert.ToInt32(_width * 0.06);
            C1Report.Cols[COL_RE_TRANSACTIONDATE].Width = Convert.ToInt32(_width * 0.06);
            C1Report.Cols[COL_RE_CLAIMNO].Width = Convert.ToInt32(_width * 0.05);
            C1Report.Cols[COL_RE_POS].Width = Convert.ToInt32(_width * 0.05);
            C1Report.Cols[COL_RE_TOS].Width = Convert.ToInt32(_width * 0.05);
            C1Report.Cols[COL_RE_CPT].Width = Convert.ToInt32(_width * 0.06);
            C1Report.Cols[COL_RE_DX1].Width = Convert.ToInt32(_width * 0.045);
            C1Report.Cols[COL_RE_DX2].Width = Convert.ToInt32(_width * 0.045);
            C1Report.Cols[COL_RE_DX3].Width = Convert.ToInt32(_width * 0.045);
            C1Report.Cols[COL_RE_DX4].Width = Convert.ToInt32(_width * 0.045);
            C1Report.Cols[COL_RE_MOD1].Width = Convert.ToInt32(_width * 0.045);
            C1Report.Cols[COL_RE_MOD2].Width = Convert.ToInt32(_width * 0.045);
            C1Report.Cols[COL_RE_CHARGES].Width = Convert.ToInt32(_width * 0.06);
            C1Report.Cols[COL_RE_TOTAL].Width = Convert.ToInt32(_width * 0.06);
            C1Report.Cols[COL_RE_ALLOWED].Width = Convert.ToInt32(_width * 0.06);
            C1Report.Cols[COL_RE_PAID_SELF].Width = Convert.ToInt32(_width * 0.06);
            C1Report.Cols[COL_RE_TRANSACTIONTYPE].Width = Convert.ToInt32(_width * 0.06);
            C1Report.Cols[COL_RE_PATIENT].Width = Convert.ToInt32(_width * 0.1);
            C1Report.Cols[COL_RE_PROVIDER].Width = Convert.ToInt32(_width * 0.1);
            C1Report.Cols[COL_RE_INSURANCE].Width = Convert.ToInt32(_width * 0.11);

            C1Report.Cols[COL_RE_PAYMENTDATE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1Report.Cols[COL_RE_TRANSACTIONDATE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1Report.Cols[COL_RE_CLAIMNO].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1Report.Cols[COL_RE_POS].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1Report.Cols[COL_RE_TOS].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1Report.Cols[COL_RE_CPT].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1Report.Cols[COL_RE_DX1].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1Report.Cols[COL_RE_DX2].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1Report.Cols[COL_RE_DX3].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1Report.Cols[COL_RE_DX4].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1Report.Cols[COL_RE_MOD1].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1Report.Cols[COL_RE_MOD2].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1Report.Cols[COL_RE_CHARGES].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
            C1Report.Cols[COL_RE_TOTAL].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
            C1Report.Cols[COL_RE_ALLOWED].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
            C1Report.Cols[COL_RE_PAID_SELF].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
            C1Report.Cols[COL_RE_TRANSACTIONTYPE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1Report.Cols[COL_RE_PATIENT].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1Report.Cols[COL_RE_PROVIDER].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1Report.Cols[COL_RE_INSURANCE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

            C1Report.Cols[COL_RE_PAID_INSURANCE].Visible = false;
            C1Report.Cols[COL_RE_PAYMENTDATE].Visible = true;
            C1Report.Cols[COL_RE_TRANSACTIONDATE].Visible = true;
            C1Report.Cols[COL_RE_CLAIMNO].Visible = true; 
            C1Report.Cols[COL_RE_POS].Visible = true; 
            C1Report.Cols[COL_RE_TOS].Visible = true; 
            C1Report.Cols[COL_RE_CPT].Visible = true; 
            C1Report.Cols[COL_RE_DX1].Visible = true; 
            C1Report.Cols[COL_RE_DX2].Visible = true; 
            C1Report.Cols[COL_RE_DX3].Visible = true; 
            C1Report.Cols[COL_RE_DX4].Visible = true; 
            C1Report.Cols[COL_RE_MOD1].Visible = true;
            C1Report.Cols[COL_RE_MOD2].Visible = true;
            C1Report.Cols[COL_RE_CHARGES].Visible = true; 
            C1Report.Cols[COL_RE_TOTAL].Visible = true; 
            C1Report.Cols[COL_RE_ALLOWED].Visible = true; 
            C1Report.Cols[COL_RE_PAID_SELF].Visible = true;
            C1Report.Cols[COL_RE_TRANSACTIONTYPE].Visible = true;
            C1Report.Cols[COL_RE_PATIENT].Visible = true; 
            C1Report.Cols[COL_RE_PROVIDER].Visible = true; 
            C1Report.Cols[COL_RE_INSURANCE].Visible = true;
        }

        private void ShowReport_MoneyReceived()
        {
            DataTable dtReport = new DataTable();
            try
            {
                DesignGrid_MoneyReceived();
                C1Report.Cols[COL_RE_PAID_INSURANCE].Visible = false;    

                dtReport = GetMoneyReceived();

                if (dtReport != null)
                {
                    Decimal dTotalBilledAmount = 0;
                    for (int i = 0; i < dtReport.Rows.Count; i++)
                    {
                        C1.Win.C1FlexGrid.Row NewRow = C1Report.Rows.Add();

                        C1Report.SetData(NewRow.Index, COL_RE_PAYMENTDATE, gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtReport.Rows[i]["dtDOSFrom"])));
                        C1Report.SetData(NewRow.Index, COL_RE_TRANSACTIONDATE, gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtReport.Rows[i]["nTransactionDate"])));
                        C1Report.SetData(NewRow.Index, COL_RE_CLAIMNO, Convert.ToString(dtReport.Rows[i]["nClaimNo"]).PadLeft(5, '0'));
                        C1Report.SetData(NewRow.Index, COL_RE_POS, Convert.ToString(dtReport.Rows[i]["sPOSCode"]));
                        C1Report.SetData(NewRow.Index, COL_RE_TOS, Convert.ToString(dtReport.Rows[i]["sTOSCode"]));
                        C1Report.SetData(NewRow.Index, COL_RE_CPT, Convert.ToString(dtReport.Rows[i]["sCPTCode"]));
                        C1Report.SetData(NewRow.Index, COL_RE_DX1, Convert.ToString(dtReport.Rows[i]["sDx1Code"]));
                        C1Report.SetData(NewRow.Index, COL_RE_DX2, Convert.ToString(dtReport.Rows[i]["sDx2Code"]));
                        C1Report.SetData(NewRow.Index, COL_RE_DX3, Convert.ToString(dtReport.Rows[i]["sDx3Code"]));
                        C1Report.SetData(NewRow.Index, COL_RE_DX4, Convert.ToString(dtReport.Rows[i]["sDx4Code"]));
                        C1Report.SetData(NewRow.Index, COL_RE_MOD1, Convert.ToString(dtReport.Rows[i]["sMod1Code"]));
                        C1Report.SetData(NewRow.Index, COL_RE_MOD2, Convert.ToString(dtReport.Rows[i]["sMod2Code"]));
                        C1Report.SetData(NewRow.Index, COL_RE_CHARGES, Convert.ToString(dtReport.Rows[i]["dCharges"]));
                        C1Report.SetData(NewRow.Index, COL_RE_TOTAL, Convert.ToString(dtReport.Rows[i]["dTotal"]));
                        C1Report.SetData(NewRow.Index, COL_RE_ALLOWED, Convert.ToString(dtReport.Rows[i]["dAllowed"]));
                        C1Report.SetData(NewRow.Index, COL_RE_TRANSACTIONTYPE, ((TransactionType)Convert.ToInt32(dtReport.Rows[i]["nTransactionType"])).ToString());
                        if (Convert.ToInt32(dtReport.Rows[i]["nTransactionType"]) > 12)
                        {
                            C1Report.SetData(NewRow.Index, COL_RE_PAID_SELF, Convert.ToDecimal(Convert.ToDecimal(dtReport.Rows[i]["dCurrentPaymentAmt"]) * -1));
                        }
                        else
                        {
                            C1Report.SetData(NewRow.Index, COL_RE_PAID_SELF, Convert.ToDecimal(dtReport.Rows[i]["dCurrentPaymentAmt"]));
                        }

                        C1Report.SetData(NewRow.Index, COL_RE_PATIENT, Convert.ToString(dtReport.Rows[i]["sPatientName"]));
                        C1Report.SetData(NewRow.Index, COL_RE_PROVIDER, Convert.ToString(dtReport.Rows[i]["sBillingProvider"]));
                        C1Report.SetData(NewRow.Index, COL_RE_INSURANCE, Convert.ToString(dtReport.Rows[i]["sInsuranceName"]));

                        dTotalBilledAmount += Convert.ToDecimal(C1Report.GetData(NewRow.Index, COL_RE_PAID_SELF));
                    }
                    C1Report.Sort(C1.Win.C1FlexGrid.SortFlags.Ascending, COL_BILL_DOS);

                    C1.Win.C1FlexGrid.Row TotalRow = C1Report.Rows.Add();
                    C1Report.SetData(TotalRow.Index, COL_RE_ALLOWED, " Total ");
                    C1Report.SetData(TotalRow.Index, COL_RE_PAID_SELF, dTotalBilledAmount);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private DataTable GetMoneyReceived()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strSQL = "";
            DataTable dt = new DataTable();
            try
            {
                oDB.Connect(false);

                strSQL = "SELECT     ISNULL(BL_Transaction_Lines.sPOSCode, '') AS sPOSCode, ISNULL(BL_Transaction_Lines.sPOSDescription, '') AS sPOSDescriptionAS, "
                + " ISNULL(BL_Transaction_Lines.sTOSCode, '') AS sTOSCode, ISNULL(BL_Transaction_Lines.sTOSDescription, '') AS sTOSDescription, "
                + " ISNULL(BL_Transaction_Lines.sCPTCode, '') AS sCPTCode, ISNULL(BL_Transaction_Lines.sCPTDescription, '') AS sCPTDescription, "
                + " ISNULL(BL_Transaction_Lines.sDx1Code, '') AS sDx1Code, ISNULL(BL_Transaction_Lines.sDx1Description, '') AS sDx1Description, "
                + " ISNULL(BL_Transaction_Lines.sDx2Code, '') AS sDx2Code, ISNULL(BL_Transaction_Lines.sDx2Description, '') AS sDx2Description, "
                + " ISNULL(BL_Transaction_Lines.sDx3Code, '') AS sDx3Code, ISNULL(BL_Transaction_Lines.sDx3Description, '') AS sDx3Description, "
                + " ISNULL(BL_Transaction_Lines.sDx4Code, '') AS sDx4Code, ISNULL(BL_Transaction_Lines.sDx4Description, '') AS sDx4Description, "
                + " ISNULL(BL_Transaction_Lines.sMod1Code, '') AS sMod1Code, ISNULL(BL_Transaction_Lines.sMod1Description, '') AS sMod1Description, "
                + " ISNULL(BL_Transaction_Lines.sMod2Code, '') AS sMod2Code, ISNULL(BL_Transaction_Lines.sMod2Description, '') AS sMod2Description, "
                + " ISNULL(BL_Transaction_Lines.dCharges, 0) AS dCharges, ISNULL(BL_Transaction_Lines.dUnit, 0) AS dUnit, ISNULL(BL_Transaction_Lines.dTotal, 0) AS dTotal, "
                + " ISNULL(BL_Transaction_Lines.dAllowed, 0) AS dAllowed, ISNULL(BL_Transaction_MST.nClaimNo, 0) AS nClaimNo, ISNULL(Patient.sFirstName, '') + SPACE(1) "
                + " + ISNULL(Patient.sMiddleName, '') + SPACE(1) + ISNULL(Patient.sLastName, '') AS sPatientName, ISNULL(Renderring_Provider.sFirstName, '') + SPACE(1) "
                + " + ISNULL(Renderring_Provider.sMiddleName, '') + SPACE(1) + ISNULL(Renderring_Provider.sLastName, '') AS sRenderringProvider, "
                + " ISNULL(Billing_Provider.sFirstName, '') + SPACE(1) + ISNULL(Billing_Provider.sMiddleName, '') + SPACE(1) + ISNULL(Billing_Provider.sLastName, '') "
                + " AS sBillingProvider, ISNULL(PatientInsurance_DTL.sInsuranceName, '') AS sInsuranceName, ISNULL(BL_Transaction_Payment_DTL.dCurrentPaymentAmt, 0) "
                + " AS dCurrentPaymentAmt, ISNULL(BL_Transaction_Payment_DTL.nTransactionType, 0) AS nTransactionType, BL_Transaction_MST.nTransactionDate, "
                + " BL_Transaction_Lines.nFromDate AS dtDOSFrom, BL_Transaction_Lines.nToDate, BL_Transaction_Payment_DTL.nPaymentMode, "
                + " BL_Transaction_Payment_DTL.nPaymentDate"
                + " FROM         BL_Transaction_Lines INNER JOIN"
                + " BL_Transaction_MST ON BL_Transaction_Lines.nTransactionID = BL_Transaction_MST.nTransactionID INNER JOIN"
                + " Patient ON BL_Transaction_MST.nPatientID = Patient.nPatientID INNER JOIN"
                + " Provider_MST AS Billing_Provider ON BL_Transaction_MST.nTransactionProviderID = Billing_Provider.nProviderID RIGHT OUTER JOIN"
                + " BL_Transaction_Payment_DTL ON BL_Transaction_Lines.nTransactionID = BL_Transaction_Payment_DTL.nBillingTransactionID AND "
                + " BL_Transaction_Lines.nTransactionDetailID = BL_Transaction_Payment_DTL.nBillingTransactionDetailID AND "
                + " BL_Transaction_Lines.nTransactionLineNo = BL_Transaction_Payment_DTL.nBillingTransactionLineNo LEFT OUTER JOIN"
                + " Provider_MST AS Renderring_Provider ON BL_Transaction_Lines.nProvider = Renderring_Provider.nProviderID LEFT OUTER JOIN"
                + " BL_Transaction_MST_Ins ON BL_Transaction_Lines.nTransactionID = BL_Transaction_MST_Ins.nTransactionID AND "
                + " BL_Transaction_Lines.nTransactionDetailID = BL_Transaction_MST_Ins.nTransactionDetailID AND "
                + " BL_Transaction_Lines.nTransactionLineNo = BL_Transaction_MST_Ins.nTransactionLineNo LEFT OUTER JOIN"
                + " PatientInsurance_DTL ON BL_Transaction_Payment_DTL.nPaymentInsuranceID = PatientInsurance_DTL.nInsuranceID"
                + " WHERE BL_Transaction_MST.nClinicID = " + _clinicId + " ";

                if (chkFromToDates.Checked == true)
                {
                    strSQL += " AND (BL_Transaction_MST.nTransactionDate >= " + gloDateMaster.gloDate.DateAsNumber(dtpStartDate.Value.ToShortDateString()) + " AND BL_Transaction_MST.nTransactionDate <= " + gloDateMaster.gloDate.DateAsNumber(dtpEndDate.Value.ToShortDateString()) + ") ";
                }

                if (chkDateOfService.Checked == true)
                {
                    strSQL += " AND (BL_Transaction_Lines.nFromDate >= " + gloDateMaster.gloDate.DateAsNumber(dtpDOSFrom.Value.ToShortDateString()) + " AND BL_Transaction_Lines.nFromDate <= " + gloDateMaster.gloDate.DateAsNumber(dtpDOSTo.Value.ToShortDateString()) + ") ";
                }

                if (cmbPatients.Items.Count > 0)
                {
                    string _strPatientIDs = "";
                    for (int i = 0; i < cmbPatients.Items.Count; i++)
                    {

                        cmbPatients.SelectedIndex = i;
                        cmbPatients.Refresh();
                        if (i == 0)
                        {
                            _strPatientIDs = "(" + Convert.ToInt64(cmbPatients.SelectedValue);
                        }
                        else
                        {
                            _strPatientIDs += "," + Convert.ToInt64(cmbPatients.SelectedValue);
                        }

                        if (i == cmbPatients.Items.Count - 1)
                        {
                            _strPatientIDs += ")";
                        }
                    }

                    if (_strPatientIDs != "")
                        strSQL += " AND BL_Transaction_MST.nPatientID IN " + _strPatientIDs + " ";
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
                        strSQL += " AND BL_Transaction_MST.nTransactionProviderID IN " + _strProviderIDs + " ";
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
                            _strInsuranceNames = "('" + Convert.ToString(cmbInsurance.Text) + "'";
                        }
                        else
                        {
                            _strInsuranceNames += ",'" + Convert.ToString(cmbInsurance.Text) + "'";
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
                if (cmbFacility.Text.Trim() != "")
                {
                    strSQL += " AND (BL_Transaction_MST.sFacilityCode = '" + cmbFacility.SelectedValue.ToString() + "') ";
                }
                if (txtZipCode.Text.Trim() != "")
                {
                    strSQL += " AND (Patient.sZIP = '" + txtZipCode.Text.Trim() + "') ";
                }
                if (txtCity.Text.Trim() != "")
                {
                    strSQL += " AND (Patient.sCity = '" + txtCity.Text.Trim() + "') ";
                }
                if (txtState.Text.Trim() != "")
                {
                    strSQL += " AND (Patient.sState = '" + txtState.Text.Trim() + "') ";
                }
                if (cmbGender.Text.Trim() != "")
                {
                    strSQL += " AND (Patient.sGender = '" + cmbGender.Text.Trim() + "') ";
                }
                if (cmbChargesTray.SelectedValue.ToString() != "" && cmbChargesTray.SelectedValue.ToString() != "0")
                {
                    strSQL += " AND (BL_Transaction_MST.nChargesDayTrayID = " + cmbChargesTray.SelectedValue + ") ";
                }


                strSQL += " ORDER BY BL_Transaction_MST.nTransactionDate,nClaimNo,sPatientName";
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

        #endregion

        #region "Patient Insurance Payment"
        private void DesignGrid_PatientInsurancePayment()
        {
            C1Report.Rows.Fixed = 1;
            C1Report.Cols.Fixed = 0;
            C1Report.Rows.Count = 1;
            C1Report.Cols.Count = COL_RE_COUNT;

            C1Report.SetData(0, COL_RE_PAYMENTDATE, "Payment Date");
            C1Report.SetData(0, COL_RE_TRANSACTIONDATE, "Date");
            C1Report.SetData(0, COL_RE_CLAIMNO, "Claim No");
            C1Report.SetData(0, COL_RE_POS, "POS");
            C1Report.SetData(0, COL_RE_TOS, "TOS");
            C1Report.SetData(0, COL_RE_CPT, "CPT");
            C1Report.SetData(0, COL_RE_DX1, "Dx1");
            C1Report.SetData(0, COL_RE_DX2, "Dx2");
            C1Report.SetData(0, COL_RE_DX3, "Dx3");
            C1Report.SetData(0, COL_RE_DX4, "Dx4");
            C1Report.SetData(0, COL_RE_MOD1, "Mod1");
            C1Report.SetData(0, COL_RE_MOD2, "Mod2");
            C1Report.SetData(0, COL_RE_CHARGES, "Charges");
            C1Report.SetData(0, COL_RE_TOTAL, "Total");
            C1Report.SetData(0, COL_RE_ALLOWED, "Allowed");
            C1Report.SetData(0, COL_RE_PAID_SELF, "Paid by Patient");
            C1Report.SetData(0, COL_RE_PAID_INSURANCE, "Paid by Insurance");
            C1Report.SetData(0, COL_RE_TRANSACTIONTYPE, "Type");
            C1Report.SetData(0, COL_RE_PATIENT, "Patient");
            C1Report.SetData(0, COL_RE_PROVIDER, "Provider");
            C1Report.SetData(0, COL_RE_INSURANCE, "Insurance");


            C1Report.Cols[COL_RE_PAYMENTDATE].DataType = typeof(System.DateTime);
            C1Report.Cols[COL_RE_TRANSACTIONDATE].DataType = typeof(System.DateTime);

            int _width = pnlCriteria.Width - 5;

            C1Report.Cols[COL_RE_PAYMENTDATE].Width = Convert.ToInt32(_width * 0.06);
            C1Report.Cols[COL_RE_TRANSACTIONDATE].Width = Convert.ToInt32(_width * 0.06);
            C1Report.Cols[COL_RE_CLAIMNO].Width = Convert.ToInt32(_width * 0.05);
            C1Report.Cols[COL_RE_POS].Width = Convert.ToInt32(_width * 0.05);
            C1Report.Cols[COL_RE_TOS].Width = Convert.ToInt32(_width * 0.05);
            C1Report.Cols[COL_RE_CPT].Width = Convert.ToInt32(_width * 0.06);
            C1Report.Cols[COL_RE_DX1].Width = Convert.ToInt32(_width * 0.045);
            C1Report.Cols[COL_RE_DX2].Width = Convert.ToInt32(_width * 0.045);
            C1Report.Cols[COL_RE_DX3].Width = Convert.ToInt32(_width * 0.045);
            C1Report.Cols[COL_RE_DX4].Width = Convert.ToInt32(_width * 0.045);
            C1Report.Cols[COL_RE_MOD1].Width = Convert.ToInt32(_width * 0.045);
            C1Report.Cols[COL_RE_MOD2].Width = Convert.ToInt32(_width * 0.045);
            C1Report.Cols[COL_RE_CHARGES].Width = Convert.ToInt32(_width * 0.06);
            C1Report.Cols[COL_RE_TOTAL].Width = Convert.ToInt32(_width * 0.06);
            C1Report.Cols[COL_RE_ALLOWED].Width = Convert.ToInt32(_width * 0.06);
            C1Report.Cols[COL_RE_PAID_SELF].Width = Convert.ToInt32(_width * 0.06);
            C1Report.Cols[COL_RE_PAID_INSURANCE].Width = Convert.ToInt32(_width * 0.06);
            C1Report.Cols[COL_RE_TRANSACTIONTYPE].Width = Convert.ToInt32(_width * 0.06);
            C1Report.Cols[COL_RE_PATIENT].Width = Convert.ToInt32(_width * 0.1);
            C1Report.Cols[COL_RE_PROVIDER].Width = Convert.ToInt32(_width * 0.1);
            C1Report.Cols[COL_RE_INSURANCE].Width = Convert.ToInt32(_width * 0.11);

            C1Report.Cols[COL_RE_PAYMENTDATE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1Report.Cols[COL_RE_TRANSACTIONDATE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1Report.Cols[COL_RE_CLAIMNO].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1Report.Cols[COL_RE_POS].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1Report.Cols[COL_RE_TOS].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1Report.Cols[COL_RE_CPT].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1Report.Cols[COL_RE_DX1].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1Report.Cols[COL_RE_DX2].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1Report.Cols[COL_RE_DX3].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1Report.Cols[COL_RE_DX4].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1Report.Cols[COL_RE_MOD1].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1Report.Cols[COL_RE_MOD2].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1Report.Cols[COL_RE_CHARGES].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
            C1Report.Cols[COL_RE_TOTAL].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
            C1Report.Cols[COL_RE_ALLOWED].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
            C1Report.Cols[COL_RE_PAID_SELF].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
            C1Report.Cols[COL_RE_PAID_INSURANCE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
            C1Report.Cols[COL_RE_TRANSACTIONTYPE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1Report.Cols[COL_RE_PATIENT].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1Report.Cols[COL_RE_PROVIDER].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1Report.Cols[COL_RE_INSURANCE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

            C1Report.Cols[COL_RE_PAYMENTDATE].Visible = true;
            C1Report.Cols[COL_RE_TRANSACTIONDATE].Visible = true;
            C1Report.Cols[COL_RE_CLAIMNO].Visible = true;
            C1Report.Cols[COL_RE_POS].Visible = true;
            C1Report.Cols[COL_RE_TOS].Visible = true;
            C1Report.Cols[COL_RE_CPT].Visible = true;
            C1Report.Cols[COL_RE_DX1].Visible = true;
            C1Report.Cols[COL_RE_DX2].Visible = true;
            C1Report.Cols[COL_RE_DX3].Visible = true;
            C1Report.Cols[COL_RE_DX4].Visible = true;
            C1Report.Cols[COL_RE_MOD1].Visible = true;
            C1Report.Cols[COL_RE_MOD2].Visible = true;
            C1Report.Cols[COL_RE_CHARGES].Visible = true;
            C1Report.Cols[COL_RE_TOTAL].Visible = true;
            C1Report.Cols[COL_RE_ALLOWED].Visible = true;
            C1Report.Cols[COL_RE_PAID_SELF].Visible = true;
            C1Report.Cols[COL_RE_PAID_INSURANCE].Visible = true;
            C1Report.Cols[COL_RE_TRANSACTIONTYPE].Visible = true;
            C1Report.Cols[COL_RE_PATIENT].Visible = true;
            C1Report.Cols[COL_RE_PROVIDER].Visible = true;
            C1Report.Cols[COL_RE_INSURANCE].Visible = true;

        }

        private void ShowReport_PatientInsurancePayment()
        {
            DataTable dtReport = new DataTable();
            try
            {
                DesignGrid_PatientInsurancePayment();
                dtReport = GetPatientInsurancePayment();

                if (dtReport != null)
                {
                   //Decimal dTotalBilledAmount = 0;
                    Decimal dPatientAmount = 0;
                    Decimal dInsuranceAmount = 0;
                    for (int i = 0; i < dtReport.Rows.Count; i++)
                    {
                        C1.Win.C1FlexGrid.Row NewRow = C1Report.Rows.Add();

                        C1Report.SetData(NewRow.Index, COL_RE_PAYMENTDATE, gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtReport.Rows[i]["dtDOSFrom"])));
                        C1Report.SetData(NewRow.Index, COL_RE_TRANSACTIONDATE, gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtReport.Rows[i]["nTransactionDate"])));
                        C1Report.SetData(NewRow.Index, COL_RE_CLAIMNO, Convert.ToString(dtReport.Rows[i]["nClaimNo"]).PadLeft(5,'0'));
                        C1Report.SetData(NewRow.Index, COL_RE_POS, Convert.ToString(dtReport.Rows[i]["sPOSCode"]));
                        C1Report.SetData(NewRow.Index, COL_RE_TOS, Convert.ToString(dtReport.Rows[i]["sTOSCode"]));
                        C1Report.SetData(NewRow.Index, COL_RE_CPT, Convert.ToString(dtReport.Rows[i]["sCPTCode"]));
                        C1Report.SetData(NewRow.Index, COL_RE_DX1, Convert.ToString(dtReport.Rows[i]["sDx1Code"]));
                        C1Report.SetData(NewRow.Index, COL_RE_DX2, Convert.ToString(dtReport.Rows[i]["sDx2Code"]));
                        C1Report.SetData(NewRow.Index, COL_RE_DX3, Convert.ToString(dtReport.Rows[i]["sDx3Code"]));
                        C1Report.SetData(NewRow.Index, COL_RE_DX4, Convert.ToString(dtReport.Rows[i]["sDx4Code"]));
                        C1Report.SetData(NewRow.Index, COL_RE_MOD1, Convert.ToString(dtReport.Rows[i]["sMod1Code"]));
                        C1Report.SetData(NewRow.Index, COL_RE_MOD2, Convert.ToString(dtReport.Rows[i]["sMod2Code"]));
                        C1Report.SetData(NewRow.Index, COL_RE_CHARGES, Convert.ToString(dtReport.Rows[i]["dCharges"]));
                        C1Report.SetData(NewRow.Index, COL_RE_TOTAL, Convert.ToString(dtReport.Rows[i]["dTotal"]));
                        C1Report.SetData(NewRow.Index, COL_RE_ALLOWED, Convert.ToString(dtReport.Rows[i]["dAllowed"]));
                        C1Report.SetData(NewRow.Index, COL_RE_TRANSACTIONTYPE, ((TransactionType)Convert.ToInt32(dtReport.Rows[i]["nTransactionType"])).ToString());
                        if (Convert.ToInt32(dtReport.Rows[i]["nTransactionType"]) > 12)
                        {
                            if ((PayerMode)Convert.ToInt32(dtReport.Rows[i]["nPayerModeID"]) == PayerMode.Insurance)
                            {
                                C1Report.SetData(NewRow.Index, COL_RE_PAID_INSURANCE, Convert.ToDecimal(Convert.ToDecimal(dtReport.Rows[i]["dCurrentPaymentAmt"]) * -1));
                                C1Report.SetData(NewRow.Index, COL_RE_PAID_SELF, 0);
                            }
                            else
                            {
                                C1Report.SetData(NewRow.Index, COL_RE_PAID_SELF, Convert.ToDecimal(Convert.ToDecimal(dtReport.Rows[i]["dCurrentPaymentAmt"]) * -1));
                                C1Report.SetData(NewRow.Index, COL_RE_PAID_INSURANCE, 0);
                            }
                        }
                        else
                        {
                            if ((PayerMode)Convert.ToInt32(dtReport.Rows[i]["nPayerModeID"]) == PayerMode.Insurance)
                            {
                                C1Report.SetData(NewRow.Index, COL_RE_PAID_INSURANCE, Convert.ToDecimal(dtReport.Rows[i]["dCurrentPaymentAmt"]));
                                C1Report.SetData(NewRow.Index, COL_RE_PAID_SELF, 0);
                            }
                            else
                            {
                                C1Report.SetData(NewRow.Index, COL_RE_PAID_SELF, Convert.ToDecimal(dtReport.Rows[i]["dCurrentPaymentAmt"]));
                                C1Report.SetData(NewRow.Index, COL_RE_PAID_INSURANCE, 0);
                            }
                        }

                        C1Report.SetData(NewRow.Index, COL_RE_PATIENT, Convert.ToString(dtReport.Rows[i]["sPatientName"]));
                        C1Report.SetData(NewRow.Index, COL_RE_PROVIDER, Convert.ToString(dtReport.Rows[i]["sBillingProvider"]));
                        C1Report.SetData(NewRow.Index, COL_RE_INSURANCE, Convert.ToString(dtReport.Rows[i]["sInsuranceName"]));

                        dPatientAmount  += Convert.ToDecimal(C1Report.GetData(NewRow.Index, COL_RE_PAID_SELF));
                        dInsuranceAmount += Convert.ToDecimal(C1Report.GetData(NewRow.Index, COL_RE_PAID_INSURANCE));
                    }
                    C1Report.Sort(C1.Win.C1FlexGrid.SortFlags.Ascending, COL_BILL_DOS);

                    C1.Win.C1FlexGrid.Row TotalRow = C1Report.Rows.Add();
                    C1Report.SetData(TotalRow.Index, COL_RE_ALLOWED, " Total ");
                    C1Report.SetData(TotalRow.Index, COL_RE_PAID_SELF, dPatientAmount);
                    C1Report.SetData(TotalRow.Index, COL_RE_PAID_INSURANCE , dInsuranceAmount);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private DataTable GetPatientInsurancePayment()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strSQL = "";
            DataTable dt = new DataTable();
            try
            {
                oDB.Connect(false);

                strSQL = "SELECT     ISNULL(BL_Transaction_Lines.sPOSCode, '') AS sPOSCode, ISNULL(BL_Transaction_Lines.sPOSDescription, '') AS sPOSDescriptionAS, "
                + " ISNULL(BL_Transaction_Lines.sTOSCode, '') AS sTOSCode, ISNULL(BL_Transaction_Lines.sTOSDescription, '') AS sTOSDescription, "
                + " ISNULL(BL_Transaction_Lines.sCPTCode, '') AS sCPTCode, ISNULL(BL_Transaction_Lines.sCPTDescription, '') AS sCPTDescription, "
                + " ISNULL(BL_Transaction_Lines.sDx1Code, '') AS sDx1Code, ISNULL(BL_Transaction_Lines.sDx1Description, '') AS sDx1Description, "
                + " ISNULL(BL_Transaction_Lines.sDx2Code, '') AS sDx2Code, ISNULL(BL_Transaction_Lines.sDx2Description, '') AS sDx2Description, "
                + " ISNULL(BL_Transaction_Lines.sDx3Code, '') AS sDx3Code, ISNULL(BL_Transaction_Lines.sDx3Description, '') AS sDx3Description, "
                + " ISNULL(BL_Transaction_Lines.sDx4Code, '') AS sDx4Code, ISNULL(BL_Transaction_Lines.sDx4Description, '') AS sDx4Description, "
                + " ISNULL(BL_Transaction_Lines.sMod1Code, '') AS sMod1Code, ISNULL(BL_Transaction_Lines.sMod1Description, '') AS sMod1Description, "
                + " ISNULL(BL_Transaction_Lines.sMod2Code, '') AS sMod2Code, ISNULL(BL_Transaction_Lines.sMod2Description, '') AS sMod2Description, "
                + " ISNULL(BL_Transaction_Lines.dCharges, 0) AS dCharges, ISNULL(BL_Transaction_Lines.dUnit, 0) AS dUnit, ISNULL(BL_Transaction_Lines.dTotal, 0) AS dTotal, "
                + " ISNULL(BL_Transaction_Lines.dAllowed, 0) AS dAllowed, ISNULL(BL_Transaction_MST.nClaimNo, 0) AS nClaimNo, ISNULL(Patient.sFirstName, '') + SPACE(1) "
                + " + ISNULL(Patient.sMiddleName, '') + SPACE(1) + ISNULL(Patient.sLastName, '') AS sPatientName, ISNULL(Renderring_Provider.sFirstName, '') + SPACE(1) "
                + " + ISNULL(Renderring_Provider.sMiddleName, '') + SPACE(1) + ISNULL(Renderring_Provider.sLastName, '') AS sRenderringProvider, "
                + " ISNULL(Billing_Provider.sFirstName, '') + SPACE(1) + ISNULL(Billing_Provider.sMiddleName, '') + SPACE(1) + ISNULL(Billing_Provider.sLastName, '') "
                + " AS sBillingProvider, ISNULL(PatientInsurance_DTL.sInsuranceName, '') AS sInsuranceName, ISNULL(BL_Transaction_Payment_DTL.dCurrentPaymentAmt, 0) "
                + " AS dCurrentPaymentAmt, ISNULL(BL_Transaction_Payment_DTL.nTransactionType, 0) AS nTransactionType, BL_Transaction_MST.nTransactionDate, "
                + " BL_Transaction_Lines.nFromDate AS dtDOSFrom, BL_Transaction_Lines.nToDate, BL_Transaction_Payment_DTL.nPaymentMode, "
                + " BL_Transaction_Payment_DTL.nPaymentDate,ISNULL(BL_Transaction_Payment_DTL.nPayerModeID,0) AS nPayerModeID "
                + " FROM         BL_Transaction_Lines INNER JOIN"
                + " BL_Transaction_MST ON BL_Transaction_Lines.nTransactionID = BL_Transaction_MST.nTransactionID INNER JOIN"
                + " Patient ON BL_Transaction_MST.nPatientID = Patient.nPatientID INNER JOIN"
                + " Provider_MST AS Billing_Provider ON BL_Transaction_MST.nTransactionProviderID = Billing_Provider.nProviderID RIGHT OUTER JOIN"
                + " BL_Transaction_Payment_DTL ON BL_Transaction_Lines.nTransactionID = BL_Transaction_Payment_DTL.nBillingTransactionID AND "
                + " BL_Transaction_Lines.nTransactionDetailID = BL_Transaction_Payment_DTL.nBillingTransactionDetailID AND "
                + " BL_Transaction_Lines.nTransactionLineNo = BL_Transaction_Payment_DTL.nBillingTransactionLineNo LEFT OUTER JOIN"
                + " Provider_MST AS Renderring_Provider ON BL_Transaction_Lines.nProvider = Renderring_Provider.nProviderID LEFT OUTER JOIN"
                + " BL_Transaction_MST_Ins ON BL_Transaction_Lines.nTransactionID = BL_Transaction_MST_Ins.nTransactionID AND "
                + " BL_Transaction_Lines.nTransactionDetailID = BL_Transaction_MST_Ins.nTransactionDetailID AND "
                + " BL_Transaction_Lines.nTransactionLineNo = BL_Transaction_MST_Ins.nTransactionLineNo LEFT OUTER JOIN"
                + " PatientInsurance_DTL ON BL_Transaction_Payment_DTL.nPaymentInsuranceID = PatientInsurance_DTL.nInsuranceID"
                + " WHERE BL_Transaction_MST.nClinicID = " + _clinicId + " ";

                if (chkFromToDates.Checked == true)
                {
                    strSQL += " AND (BL_Transaction_MST.nTransactionDate >= " + gloDateMaster.gloDate.DateAsNumber(dtpStartDate.Value.ToShortDateString()) + " AND BL_Transaction_MST.nTransactionDate <= " + gloDateMaster.gloDate.DateAsNumber(dtpEndDate.Value.ToShortDateString()) + ") ";
                }

                if (chkDateOfService.Checked == true)
                {
                    strSQL += " AND (BL_Transaction_Lines.nFromDate >= " + gloDateMaster.gloDate.DateAsNumber(dtpDOSFrom.Value.ToShortDateString()) + " AND BL_Transaction_Lines.nFromDate <= " + gloDateMaster.gloDate.DateAsNumber(dtpDOSTo.Value.ToShortDateString()) + ") ";
                }

                if (cmbPatients.Items.Count > 0)
                {
                    string _strPatientIDs = "";
                    for (int i = 0; i < cmbPatients.Items.Count; i++)
                    {

                        cmbPatients.SelectedIndex = i;
                        cmbPatients.Refresh();
                        if (i == 0)
                        {
                            _strPatientIDs = "(" + Convert.ToInt64(cmbPatients.SelectedValue);
                        }
                        else
                        {
                            _strPatientIDs += "," + Convert.ToInt64(cmbPatients.SelectedValue);
                        }

                        if (i == cmbPatients.Items.Count - 1)
                        {
                            _strPatientIDs += ")";
                        }
                    }

                    if (_strPatientIDs != "")
                        strSQL += " AND BL_Transaction_MST.nPatientID IN " + _strPatientIDs + " ";
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
                        strSQL += " AND BL_Transaction_MST.nTransactionProviderID IN " + _strProviderIDs + " ";
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
                            _strInsuranceNames = "('" + Convert.ToString(cmbInsurance.Text) + "'";
                        }
                        else
                        {
                            _strInsuranceNames += ",'" + Convert.ToString(cmbInsurance.Text) + "'";
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
                if (cmbFacility.Text.Trim() != "")
                {
                    strSQL += " AND (BL_Transaction_MST.sFacilityCode = '" + cmbFacility.SelectedValue.ToString() + "') ";
                }
                if (txtZipCode.Text.Trim() != "")
                {
                    strSQL += " AND (Patient.sZIP = '" + txtZipCode.Text.Trim() + "') ";
                }
                if (txtCity.Text.Trim() != "")
                {
                    strSQL += " AND (Patient.sCity = '" + txtCity.Text.Trim() + "') ";
                }
                if (txtState.Text.Trim() != "")
                {
                    strSQL += " AND (Patient.sState = '" + txtState.Text.Trim() + "') ";
                }
                if (cmbGender.Text.Trim() != "")
                {
                    strSQL += " AND (Patient.sGender = '" + cmbGender.Text.Trim() + "') ";
                }
                if (cmbChargesTray.SelectedValue.ToString() != "" && cmbChargesTray.SelectedValue.ToString() != "0")
                {
                    strSQL += " AND (BL_Transaction_MST.nChargesDayTrayID = " + cmbChargesTray.SelectedValue + ") ";
                }

                strSQL += " ORDER BY BL_Transaction_MST.nTransactionDate,nClaimNo,sPatientName";
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

        #endregion

        #region "Write-Off"

        private void DesignGrid_WriteOff()
        {
            C1Report.Rows.Fixed = 1;
            C1Report.Cols.Fixed = 0;
            C1Report.Rows.Count = 1;
            C1Report.Cols.Count = COL_RE_COUNT;

            C1Report.SetData(0, COL_WOFF_DOS, "DOS");
            C1Report.SetData(0, COL_WOFF_TRANSACTIONDATE, "Date");
            C1Report.SetData(0, COL_WOFF_CLAIMNO, "Claim No");
            C1Report.SetData(0, COL_WOFF_POS, "POS");
            C1Report.SetData(0, COL_WOFF_TOS, "TOS");
            C1Report.SetData(0, COL_WOFF_CPT, "CPT");
            C1Report.SetData(0, COL_WOFF_DX1, "Dx1");
            C1Report.SetData(0, COL_WOFF_DX2, "Dx2");
            C1Report.SetData(0, COL_WOFF_DX3, "Dx3");
            C1Report.SetData(0, COL_WOFF_DX4, "Dx4");
            C1Report.SetData(0, COL_WOFF_MOD1, "Mod1");
            C1Report.SetData(0, COL_WOFF_MOD2, "Mod2");
            C1Report.SetData(0, COL_WOFF_CHARGES, "Charges");
            C1Report.SetData(0, COL_WOFF_TOTAL, "Total");
            C1Report.SetData(0, COL_WOFF_ALLOWED, "Allowed");
            C1Report.SetData(0, COL_WOFF_PAID, "Paid");
            C1Report.SetData(0, COL_WOFF_WRITEOFF, "Write Off");
            C1Report.SetData(0, COL_WOFF_BALANCE, "Balance");
            C1Report.SetData(0, COL_WOFF_PATIENT, "Patient");
            C1Report.SetData(0, COL_WOFF_PROVIDER, "Provider");
            C1Report.SetData(0, COL_WOFF_INSURANCE, "Insurance");


            C1Report.Cols[COL_WOFF_DOS].DataType = typeof(System.DateTime);
            C1Report.Cols[COL_WOFF_TRANSACTIONDATE].DataType = typeof(System.DateTime);

            int _width = pnlCriteria.Width - 5;

            C1Report.Cols[COL_WOFF_DOS].Width = Convert.ToInt32(_width * 0.06);
            C1Report.Cols[COL_WOFF_TRANSACTIONDATE].Width = Convert.ToInt32(_width * 0.06);
            C1Report.Cols[COL_WOFF_CLAIMNO].Width = Convert.ToInt32(_width * 0.05);
            C1Report.Cols[COL_WOFF_POS].Width = Convert.ToInt32(_width * 0.05);
            C1Report.Cols[COL_WOFF_TOS].Width = Convert.ToInt32(_width * 0.05);
            C1Report.Cols[COL_WOFF_CPT].Width = Convert.ToInt32(_width * 0.06);
            C1Report.Cols[COL_WOFF_DX1].Width = Convert.ToInt32(_width * 0.045);
            C1Report.Cols[COL_WOFF_DX2].Width = Convert.ToInt32(_width * 0.045);
            C1Report.Cols[COL_WOFF_DX3].Width = Convert.ToInt32(_width * 0.045);
            C1Report.Cols[COL_WOFF_DX4].Width = Convert.ToInt32(_width * 0.045);
            C1Report.Cols[COL_WOFF_MOD1].Width = Convert.ToInt32(_width * 0.045);
            C1Report.Cols[COL_WOFF_MOD2].Width = Convert.ToInt32(_width * 0.045);
            C1Report.Cols[COL_WOFF_CHARGES].Width = Convert.ToInt32(_width * 0.06);
            C1Report.Cols[COL_WOFF_TOTAL].Width = 0;
            C1Report.Cols[COL_WOFF_ALLOWED].Width = Convert.ToInt32(_width * 0.06);
            C1Report.Cols[COL_WOFF_PAID].Width = Convert.ToInt32(_width * 0.06);
            C1Report.Cols[COL_WOFF_WRITEOFF].Width = Convert.ToInt32(_width * 0.06);
            C1Report.Cols[COL_WOFF_BALANCE].Width = Convert.ToInt32(_width * 0.06);
            C1Report.Cols[COL_WOFF_PATIENT].Width = Convert.ToInt32(_width * 0.1);
            C1Report.Cols[COL_WOFF_PROVIDER].Width = 0;
            C1Report.Cols[COL_WOFF_INSURANCE].Width = Convert.ToInt32(_width * 0.11);

            C1Report.Cols[COL_WOFF_DOS].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1Report.Cols[COL_WOFF_TRANSACTIONDATE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1Report.Cols[COL_WOFF_CLAIMNO].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1Report.Cols[COL_WOFF_POS].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1Report.Cols[COL_WOFF_TOS].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1Report.Cols[COL_WOFF_CPT].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1Report.Cols[COL_WOFF_DX1].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1Report.Cols[COL_WOFF_DX2].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1Report.Cols[COL_WOFF_DX3].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1Report.Cols[COL_WOFF_DX4].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1Report.Cols[COL_WOFF_MOD1].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1Report.Cols[COL_WOFF_MOD2].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1Report.Cols[COL_WOFF_CHARGES].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
            C1Report.Cols[COL_WOFF_TOTAL].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
            C1Report.Cols[COL_WOFF_ALLOWED].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
            C1Report.Cols[COL_WOFF_PAID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
            C1Report.Cols[COL_WOFF_WRITEOFF].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
            C1Report.Cols[COL_WOFF_BALANCE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1Report.Cols[COL_WOFF_PATIENT].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1Report.Cols[COL_WOFF_PROVIDER].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1Report.Cols[COL_WOFF_INSURANCE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

            C1Report.Cols[COL_WOFF_DOS].Visible = true;
            C1Report.Cols[COL_WOFF_TRANSACTIONDATE].Visible = true;
            C1Report.Cols[COL_WOFF_CLAIMNO].Visible = true;
            C1Report.Cols[COL_WOFF_POS].Visible = true;
            C1Report.Cols[COL_WOFF_TOS].Visible = true;
            C1Report.Cols[COL_WOFF_CPT].Visible = true;
            C1Report.Cols[COL_WOFF_DX1].Visible = true;
            C1Report.Cols[COL_WOFF_DX2].Visible = true;
            C1Report.Cols[COL_WOFF_DX3].Visible = true;
            C1Report.Cols[COL_WOFF_DX4].Visible = true;
            C1Report.Cols[COL_WOFF_MOD1].Visible = true;
            C1Report.Cols[COL_WOFF_MOD2].Visible = true;
            C1Report.Cols[COL_WOFF_CHARGES].Visible = true;
            C1Report.Cols[COL_WOFF_TOTAL].Visible = false;
            C1Report.Cols[COL_WOFF_ALLOWED].Visible = true;
            C1Report.Cols[COL_WOFF_PAID].Visible = true;
            C1Report.Cols[COL_WOFF_WRITEOFF].Visible = true;
            C1Report.Cols[COL_WOFF_BALANCE].Visible = true;
            C1Report.Cols[COL_WOFF_PATIENT].Visible = true;
            C1Report.Cols[COL_WOFF_PROVIDER].Visible = false;
            C1Report.Cols[COL_WOFF_INSURANCE].Visible = true;

        }

        private void ShowReport_WriteOff()
        {
            DataTable dtReport = new DataTable();
            try
            {
                DesignGrid_WriteOff();
                dtReport = GetWriteOff();

                decimal _totalWriteOff = 0;
                decimal _totalPayment = 0;
                decimal _totalBalance = 0;

                if (dtReport != null)
                {
                    for (int i = 0; i < dtReport.Rows.Count; i++)
                    {
                        C1.Win.C1FlexGrid.Row NewRow = C1Report.Rows.Add();

                        C1Report.SetData(NewRow.Index, COL_WOFF_DOS, gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtReport.Rows[i]["nFromDate"])));
                        C1Report.SetData(NewRow.Index, COL_WOFF_TRANSACTIONDATE, gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtReport.Rows[i]["nTransactionDate"])));
                        C1Report.SetData(NewRow.Index, COL_WOFF_CLAIMNO, Convert.ToString(dtReport.Rows[i]["nClaimNo"]).PadLeft(5,'0'));
                        C1Report.SetData(NewRow.Index, COL_WOFF_POS, Convert.ToString(dtReport.Rows[i]["sPOSCode"]));
                        C1Report.SetData(NewRow.Index, COL_WOFF_TOS, Convert.ToString(dtReport.Rows[i]["sTOSCode"]));
                        C1Report.SetData(NewRow.Index, COL_WOFF_CPT, Convert.ToString(dtReport.Rows[i]["sCPTCode"]));
                        C1Report.SetData(NewRow.Index, COL_WOFF_DX1, Convert.ToString(dtReport.Rows[i]["sDx1Code"]));
                        C1Report.SetData(NewRow.Index, COL_WOFF_DX2, Convert.ToString(dtReport.Rows[i]["sDx2Code"]));
                        C1Report.SetData(NewRow.Index, COL_WOFF_DX3, Convert.ToString(dtReport.Rows[i]["sDx3Code"]));
                        C1Report.SetData(NewRow.Index, COL_WOFF_DX4, Convert.ToString(dtReport.Rows[i]["sDx4Code"]));
                        C1Report.SetData(NewRow.Index, COL_WOFF_MOD1, Convert.ToString(dtReport.Rows[i]["sMod1Code"]));
                        C1Report.SetData(NewRow.Index, COL_WOFF_MOD2, Convert.ToString(dtReport.Rows[i]["sMod2Code"]));
                        C1Report.SetData(NewRow.Index, COL_WOFF_CHARGES, Convert.ToString(dtReport.Rows[i]["dCharges"]));
                        C1Report.SetData(NewRow.Index, COL_WOFF_TOTAL, Convert.ToString(dtReport.Rows[i]["dTotal"]));
                        C1Report.SetData(NewRow.Index, COL_WOFF_ALLOWED, Convert.ToString(dtReport.Rows[i]["dAllowed"]));
                        C1Report.SetData(NewRow.Index, COL_WOFF_PAID, Convert.ToString(dtReport.Rows[i]["dPaid"]));

                        decimal _dBalance = Convert.ToDecimal(Convert.ToDecimal(dtReport.Rows[i]["dAllowed"]) - Convert.ToDecimal(dtReport.Rows[i]["dPaid"]));
                        decimal _dWriteOff = 0;

                        
                        if (Convert.ToDecimal(dtReport.Rows[i]["dAllowed"]) <= Convert.ToDecimal(dtReport.Rows[i]["dPaid"]))
                        {
                            _dWriteOff = Convert.ToDecimal(dtReport.Rows[i]["dCharges"]) - Convert.ToDecimal(dtReport.Rows[i]["dPaid"]);
                        }
                        else
                        {
                            _dWriteOff = Convert.ToDecimal(dtReport.Rows[i]["dCharges"]) - Convert.ToDecimal(dtReport.Rows[i]["dAllowed"]);
                           
                        }

                        _totalPayment += Convert.ToDecimal(dtReport.Rows[i]["dPaid"]);
                        _totalBalance += _dBalance;
                        _totalWriteOff += _dWriteOff;

                        C1Report.SetData(NewRow.Index, COL_WOFF_WRITEOFF,_dWriteOff.ToString("#0.00")) ;
                        C1Report.SetData(NewRow.Index, COL_WOFF_BALANCE, _dBalance.ToString("#0.00"));  
                        C1Report.SetData(NewRow.Index, COL_WOFF_PATIENT, Convert.ToString(dtReport.Rows[i]["sPatientName"]));
                        //C1Report.SetData(NewRow.Index, COL_WOFF_PROVIDER, Convert.ToString(dtReport.Rows[i]["sBillingProvider"]));
                        C1Report.SetData(NewRow.Index, COL_WOFF_INSURANCE, Convert.ToString(dtReport.Rows[i]["sInsuranceName"]));
                       
                    }
                    
                    C1.Win.C1FlexGrid.Row NewTotalRow = C1Report.Rows.Add();
                    C1Report.SetData(NewTotalRow.Index, COL_WOFF_ALLOWED, " Total");
                    C1Report.SetData(NewTotalRow.Index, COL_WOFF_WRITEOFF, _totalWriteOff.ToString("#0.00"));
                    C1Report.SetData(NewTotalRow.Index, COL_WOFF_BALANCE, _totalBalance.ToString("#0.00"));
                    C1Report.SetData(NewTotalRow.Index, COL_WOFF_PAID, _totalPayment.ToString("#0.00"));  
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        } 
        
        private DataTable GetWriteOff()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strSQL = "";
            DataTable dt = new DataTable();
            try
            {
                oDB.Connect(false);

                strSQL = "SELECT BL_Transaction_Lines.nTransactionID, BL_Transaction_Lines.nTransactionDetailID, "
                + " BL_Transaction_Lines.nTransactionLineNo,  BL_Transaction_Lines.nFromDate, "
                + " BL_Transaction_Lines.nToDate, BL_Transaction_Lines.sPOSCode, BL_Transaction_Lines.sPOSDescription,   "
                + " BL_Transaction_Lines.sTOSCode, BL_Transaction_Lines.sTOSDescription, BL_Transaction_Lines.sCPTCode, "
                + " BL_Transaction_Lines.sCPTDescription,   BL_Transaction_Lines.sDx1Code, BL_Transaction_Lines.sDx1Description, "
                + " BL_Transaction_Lines.sDx2Code, BL_Transaction_Lines.sDx2Description,   BL_Transaction_Lines.sDx3Code, "
                + " BL_Transaction_Lines.sDx3Description, BL_Transaction_Lines.sDx4Code, BL_Transaction_Lines.sDx4Description,   "
                + " BL_Transaction_Lines.sMod1Code, BL_Transaction_Lines.sMod1Description, BL_Transaction_Lines.sMod2Code,   "
                + " BL_Transaction_Lines.sMod2Description, BL_Transaction_Lines.dCharges, BL_Transaction_Lines.dUnit, "
                + " BL_Transaction_Lines.dTotal,BL_Transaction_Lines.dAllowed,"
                + "  									("
                + "  											( ISNULL((select SUM(BL_Transaction_Payment_DTL.dCurrentPaymentAmt) FROM BL_Transaction_Payment_DTL"
                + "  											WHERE BL_Transaction_Payment_DTL.nBillingTransactionID = BL_Transaction_Lines.nTransactionID"
                + "  											AND BL_Transaction_Payment_DTL.nBillingTransactionDetailID = BL_Transaction_Lines.nTransactionDetailID"
                + "  											AND BL_Transaction_Payment_DTL.nBillingTransactionLineNo = BL_Transaction_Lines.nTransactionLineNo"
                + "  											AND BL_Transaction_Payment_DTL.nTransactionType <= 12),0) - "
                + "  											ISNULL((select SUM(BL_Transaction_Payment_DTL.dCurrentPaymentAmt) FROM BL_Transaction_Payment_DTL"
                + "  											WHERE BL_Transaction_Payment_DTL.nBillingTransactionID = BL_Transaction_Lines.nTransactionID"
                + "  											AND BL_Transaction_Payment_DTL.nBillingTransactionDetailID = BL_Transaction_Lines.nTransactionDetailID"
                + "  											AND BL_Transaction_Payment_DTL.nBillingTransactionLineNo = BL_Transaction_Lines.nTransactionLineNo"
                + "  											AND BL_Transaction_Payment_DTL.nTransactionType > 12),0) ) "
                + "  									) AS dPaid,   "
                + " Patient.sFirstName + ' ' + Patient.sMiddleName + ' ' + Patient.sLastName AS sPatientName,"
                + "  ISNULL(PatientInsurance_DTL.sInsuranceName,'') AS sInsuranceName , "
                + "  ISNULL(BL_Transaction_MST.nClaimNo, 0) AS nClaimNo,BL_Transaction_MST.nTransactionDate,"
                + " BL_Transaction_Lines.nClinicID  "
                + "  FROM BL_Transaction_Lines LEFT OUTER JOIN  PatientInsurance_DTL "
                + "  INNER JOIN  BL_Transaction_MST_Ins ON PatientInsurance_DTL.nInsuranceID = BL_Transaction_MST_Ins.nInsuranceID ON   BL_Transaction_Lines.nTransactionID = BL_Transaction_MST_Ins.nTransactionID AND   BL_Transaction_Lines.nTransactionDetailID = BL_Transaction_MST_Ins.nTransactionDetailID "
                + "  AND   BL_Transaction_Lines.nTransactionLineNo = BL_Transaction_MST_Ins.nTransactionLineNo LEFT OUTER JOIN  Patient RIGHT OUTER JOIN  BL_Transaction_MST ON Patient.nPatientID = BL_Transaction_MST.nPatientID ON   BL_Transaction_Lines.nTransactionID = BL_Transaction_MST.nTransactionID  "
                + "  WHERE BL_Transaction_Lines.nClinicID   = " + _clinicId + "";

                if (chkFromToDates.Checked == true)
                {
                    strSQL += " AND (BL_Transaction_MST.nTransactionDate >= " + gloDateMaster.gloDate.DateAsNumber(dtpStartDate.Value.ToShortDateString()) + " AND BL_Transaction_MST.nTransactionDate <= " + gloDateMaster.gloDate.DateAsNumber(dtpEndDate.Value.ToShortDateString()) + ") ";
                }

                if (chkDateOfService.Checked == true)
                {
                    strSQL += " AND (BL_Transaction_Lines.nFromDate >= " + gloDateMaster.gloDate.DateAsNumber(dtpDOSFrom.Value.ToShortDateString()) + " AND BL_Transaction_Lines.nFromDate <= " + gloDateMaster.gloDate.DateAsNumber(dtpDOSTo.Value.ToShortDateString()) + ") ";
                }

                if (cmbPatients.Items.Count > 0)
                {
                    string _strPatientIDs = "";
                    for (int i = 0; i < cmbPatients.Items.Count; i++)
                    {

                        cmbPatients.SelectedIndex = i;
                        cmbPatients.Refresh();
                        if (i == 0)
                        {
                            _strPatientIDs = "(" + Convert.ToInt64(cmbPatients.SelectedValue);
                        }
                        else
                        {
                            _strPatientIDs += "," + Convert.ToInt64(cmbPatients.SelectedValue);
                        }

                        if (i == cmbPatients.Items.Count - 1)
                        {
                            _strPatientIDs += ")";
                        }
                    }

                    if (_strPatientIDs != "")
                        strSQL += " AND BL_Transaction_MST.nPatientID IN " + _strPatientIDs + " ";
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
                        strSQL += " AND BL_Transaction_MST.nTransactionProviderID IN " + _strProviderIDs + " ";
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
                            _strInsuranceNames = "('" + Convert.ToString(cmbInsurance.Text) + "'";
                        }
                        else
                        {
                            _strInsuranceNames += ",'" + Convert.ToString(cmbInsurance.Text) + "'";
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
                if (cmbFacility.Text.Trim() != "")
                {
                    strSQL += " AND (BL_Transaction_MST.sFacilityCode = '" + cmbFacility.SelectedValue.ToString() + "') ";
                }
                if (txtZipCode.Text.Trim() != "")
                {
                    strSQL += " AND (Patient.sZIP = '" + txtZipCode.Text.Trim() + "') ";
                }
                if (txtCity.Text.Trim() != "")
                {
                    strSQL += " AND (Patient.sCity = '" + txtCity.Text.Trim() + "') ";
                }
                if (txtState.Text.Trim() != "")
                {
                    strSQL += " AND (Patient.sState = '" + txtState.Text.Trim() + "') ";
                }
                if (cmbGender.Text.Trim() != "")
                {
                    strSQL += " AND (Patient.sGender = '" + cmbGender.Text.Trim() + "') ";
                }
                if (cmbChargesTray.SelectedValue.ToString() != "" && cmbChargesTray.SelectedValue.ToString() != "0")
                {
                    strSQL += " AND (BL_Transaction_MST.nChargesDayTrayID = " + cmbChargesTray.SelectedValue + ") ";
                }

                strSQL += " ORDER BY BL_Transaction_MST.nTransactionDate,nClaimNo,sPatientName";
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
        

        #endregion

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

        #endregion "Filter by date"

        private void dtpDOSFrom_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                dtpDOSTo.Value = dtpDOSFrom.Value;
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

        private void btn_MouseHover(object sender, EventArgs e)
        {
            ((System.Windows.Forms.Button)sender).BackgroundImage = global::gloBilling.Properties.Resources.Img_LongYellow;
            ((System.Windows.Forms.Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btn_MouseLeave(object sender, EventArgs e)
        {
            ((System.Windows.Forms.Button)sender).BackgroundImage = global::gloBilling.Properties.Resources.Img_LongButton;
            ((System.Windows.Forms.Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void rbAmountBilled_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAmountBilled.Checked == true)
                rbAmountBilled.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            else
                rbAmountBilled.Font = gloGlobal.clsgloFont.gFont; //new Font("Tahoma", 9, FontStyle.Regular); 
        }



        private void rbMoneyReceived_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMoneyReceived.Checked == true)
                rbMoneyReceived.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            else
                rbMoneyReceived.Font = gloGlobal.clsgloFont.gFont; //new Font("Tahoma", 9, FontStyle.Regular); 
        }

        private void rbPatient_VS_Insurance_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPatient_VS_Insurance.Checked == true)
                rbPatient_VS_Insurance.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            else
                rbPatient_VS_Insurance.Font = gloGlobal.clsgloFont.gFont; //new Font("Tahoma", 9, FontStyle.Regular); 
        }

        private void rbWriteOff_CheckedChanged(object sender, EventArgs e)
        {
            if (rbWriteOff.Checked == true)
                rbWriteOff.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            else
                rbWriteOff.Font = gloGlobal.clsgloFont.gFont; //new Font("Tahoma", 9, FontStyle.Regular); 
        }

        private void C1Report_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

        
        
    }
}
