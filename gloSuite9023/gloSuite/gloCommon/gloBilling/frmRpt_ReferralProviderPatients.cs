using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using System.IO;

namespace gloBilling
{
    public partial class frmRpt_ReferralProviderPatients : Form
    {
        #region " Variable Declarations "

        private string _databaseconnectionstring = "";
        private Int64 _clinicId = 0;
        private string _messageBoxCaption = String.Empty;

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        gloListControl.gloListControl oListControl = null;
        gloListControl.gloListControlType _CurrentControlType = gloListControl.gloListControlType.Other;

        #endregion

        #region " C1 Grid Column Constants "

        const int COL_PROVIDER_ID = 0;
        const int COL_PROVIDER_NAME = 1;
        const int COL_REFERRAL_ID = 2;
        const int COL_REFERRAL_NAME = 3;
        const int COL_PATIENT_ID = 4;
        const int COL_PATIENT_CODE = 5;
        const int COL_PATIENT_NAME = 6;
        const int COL_GENDER = 7;
        const int COL_DOB = 8;
        const int COL_PHONE = 9;
        const int COL_MOBILE = 10;
        const int COL_EMAIL = 11;
        const int COL_ADDR1 = 12;
        const int COL_ADDR2 = 13;
        const int COL_CITY = 14;
         const int COL_STATE = 15;
        const int COL_ZIP = 16;

        const int COL_COUNT = 17;

        #endregion

        #region " Constructor "

        public frmRpt_ReferralProviderPatients(string DatabaseConnectionString)
        {
            InitializeComponent();

            _databaseconnectionstring = DatabaseConnectionString;

            #region "Get Clinic ID "

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _clinicId = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _clinicId = 1; }
            }
            else
            { _clinicId = 1; }

            #endregion

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

        #region " Form Load "

        private void frmRpt_ReferralProviderPatients_Load(object sender, EventArgs e)
        {
            FillStates();
            FillGender();
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

        #endregion

        #region " Form Browse & Clear Button Event "

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
                        try
                        {
                            oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                        }
                        catch { }

                        try
                        {
                            oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                        }
                        catch { }

                        try
                        {
                            oListControl.AddFormHandlerClick -= new gloListControl.gloListControl.AddFormHandler(oListControl_AddFormHandlerClick);
                        }
                        catch { }
                    }
                    catch { }
                    oListControl.Dispose();
                    oListControl = null;
                }
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Providers, false, this.Width);
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
                        try
                        {
                            oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                        }
                        catch { }

                        try
                        {
                            oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                        }
                        catch { }

                        try
                        {
                            oListControl.AddFormHandlerClick -= new gloListControl.gloListControl.AddFormHandler(oListControl_AddFormHandlerClick);
                        }
                        catch { }
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

        private void btnBrowseReferrals_Click(object sender, EventArgs e)
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
                        try
                        {
                            oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                        }
                        catch { }
                         
                        try
                        {
                            oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                        }
                        catch { }

                        try
                        {
                            oListControl.AddFormHandlerClick -= new gloListControl.gloListControl.AddFormHandler(oListControl_AddFormHandlerClick);
                        }
                        catch { }
                    }
                    catch { }
                    oListControl.Dispose();
                    oListControl = null;
                }
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Referrals, true, this.Width);
                oListControl.ClinicID = _clinicId;
                oListControl.ControlHeader = " Referrals";
                //Roopali babriya - 06 june 2012 - Used hide new and modify button.              
                oListControl.IsFromReport = true;


                _CurrentControlType = gloListControl.gloListControlType.Referrals;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                oListControl .AddFormHandlerClick +=new gloListControl.gloListControl.AddFormHandler(oListControl_AddFormHandlerClick);

                this.Controls.Add(oListControl);

                if (cmbReferral.DataSource != null)
                {
                    for (int i = 0; i < cmbReferral.Items.Count; i++)
                    {
                        cmbReferral.SelectedIndex = i;
                        cmbReferral.Refresh();
                        oListControl.SelectedItems.Add(Convert.ToInt64(cmbReferral.SelectedValue), cmbReferral.Text);
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

        private void btnClearInsurance_Click(object sender, EventArgs e)
        {
           
            cmbInsurance.DataSource = null;
            cmbInsurance.Items.Clear();
            cmbInsurance.Refresh();
        }

        private void btnClearReferrals_Click(object sender, EventArgs e)
        {
           
            cmbReferral.DataSource = null;
            cmbReferral.Items.Clear();
            cmbReferral.Refresh();
        }

        #endregion

        #region "List Control Events"

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
                    try
                    {
                        oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                    }
                    catch { }

                    try
                    {
                        oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                    }
                    catch { }

                    try
                    {
                        oListControl.AddFormHandlerClick -= new gloListControl.gloListControl.AddFormHandler(oListControl_AddFormHandlerClick);
                    }
                    catch { }
                }
                catch { }
                
            }
        }
        //Added by Mayuri:20100708-To give facility of adding referrals from glolist control
        private void oListControl_AddFormHandlerClick(object sender, EventArgs e)
        {
            if (oListControl.ControlHeader == " Referrals")
            {
                gloContacts.frmSetupPhysician ofrmAddContact = new gloContacts.frmSetupPhysician(_databaseconnectionstring);
                ofrmAddContact.CallFrom = "Physician";
                ofrmAddContact.ShowDialog(this);

                if (ofrmAddContact.DialogResult == DialogResult.OK)
                {
                    oListControl.FillListAsCriteria(ofrmAddContact.ContactID);

                }
                ofrmAddContact.Dispose();

            }
        }

        #endregion

        #region " ToolStrip Button Click Events "

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

        private void tls_btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
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

        #region " Design Grid "

        private void DesignGrid()
        {
            try
            {
                C1Patients.Clear();

                C1Patients.Cols.Fixed = 0;
                C1Patients.Cols.Count = COL_COUNT;
                C1Patients.Rows.Fixed = 1;
                C1Patients.Rows.Count = 1;

                int rowIndex = 0;
                rowIndex = C1Patients.Rows.Count - 1;

                #region " Header Section "

                C1Patients.SetData(rowIndex, COL_PROVIDER_ID, "PrviderID");
                C1Patients.SetData(rowIndex, COL_PROVIDER_NAME, "");
                C1Patients.SetData(rowIndex, COL_REFERRAL_ID, "ReferralID");
                C1Patients.SetData(rowIndex, COL_REFERRAL_NAME, "ReferralName");
                C1Patients.SetData(rowIndex, COL_PATIENT_ID, "PatientID");
                C1Patients.SetData(rowIndex, COL_PATIENT_CODE, "Patient Code");
                C1Patients.SetData(rowIndex, COL_PATIENT_NAME, "Patient Name");
                C1Patients.SetData(rowIndex, COL_GENDER, "Gender");
                C1Patients.SetData(rowIndex, COL_DOB, "DOB");
                C1Patients.SetData(rowIndex, COL_PHONE, "Phone");
                C1Patients.SetData(rowIndex, COL_MOBILE, "Mobile");
                C1Patients.SetData(rowIndex, COL_EMAIL, "e-Mail");

                C1Patients.SetData(rowIndex, COL_ADDR1, "Address");
                C1Patients.SetData(rowIndex, COL_ADDR2, "Address");
                C1Patients.SetData(rowIndex, COL_CITY, "City");
                C1Patients.SetData(rowIndex, COL_STATE, "State");
                C1Patients.SetData(rowIndex, COL_ZIP, "Zip");

                #endregion

                #region " Data Types "

                C1Patients.Cols[COL_PROVIDER_ID].DataType = typeof(System.Int64);
                C1Patients.Cols[COL_PROVIDER_NAME].DataType = typeof(System.String);
                C1Patients.Cols[COL_REFERRAL_ID].DataType = typeof(System.Int64);
                C1Patients.Cols[COL_REFERRAL_NAME].DataType = typeof(System.String);
                C1Patients.Cols[COL_PATIENT_ID].DataType = typeof(System.Int64);
                C1Patients.Cols[COL_PATIENT_CODE].DataType = typeof(System.String);
                C1Patients.Cols[COL_PATIENT_NAME].DataType = typeof(System.String);
                C1Patients.Cols[COL_GENDER].DataType = typeof(System.String);
                C1Patients.Cols[COL_DOB].DataType = typeof(System.String);
                C1Patients.Cols[COL_PHONE].DataType = typeof(System.String);
                C1Patients.Cols[COL_MOBILE].DataType = typeof(System.String);
                C1Patients.Cols[COL_EMAIL].DataType = typeof(System.String);

                C1Patients.Cols[COL_ADDR1].DataType = typeof(System.String);
                C1Patients.Cols[COL_ADDR2].DataType = typeof(System.String);
                C1Patients.Cols[COL_CITY].DataType = typeof(System.String);
                C1Patients.Cols[COL_STATE].DataType = typeof(System.String);
                C1Patients.Cols[COL_ZIP].DataType = typeof(System.String);


                #endregion

                #region " Width "

                C1Patients.Cols[COL_PROVIDER_ID].Width = 0;
                C1Patients.Cols[COL_PROVIDER_NAME].Width = 200;
                C1Patients.Cols[COL_REFERRAL_ID].Width = 0;
                C1Patients.Cols[COL_REFERRAL_NAME].Width = 200;
                C1Patients.Cols[COL_PATIENT_ID].Width = 0;
                C1Patients.Cols[COL_PATIENT_CODE].Width = 100;
                C1Patients.Cols[COL_PATIENT_NAME].Width = 200;
                C1Patients.Cols[COL_GENDER].Width = 50;
                C1Patients.Cols[COL_DOB].Width = 80;
                C1Patients.Cols[COL_PHONE].Width = 100;
                C1Patients.Cols[COL_MOBILE].Width = 100;
                C1Patients.Cols[COL_EMAIL].Width = 100;

                C1Patients.Cols[COL_ADDR1].Width = 215;
                C1Patients.Cols[COL_ADDR2].Width = 0;
                C1Patients.Cols[COL_CITY].Width = 100;
                C1Patients.Cols[COL_STATE].Width = 50;
                C1Patients.Cols[COL_ZIP].Width = 60;


                #endregion

                #region " Show Hide "

                C1Patients.Cols[COL_PROVIDER_ID].Visible = false;
                C1Patients.Cols[COL_PROVIDER_NAME].Visible = true;
                C1Patients.Cols[COL_REFERRAL_ID].Visible = false;
                C1Patients.Cols[COL_REFERRAL_NAME].Visible = false;
                C1Patients.Cols[COL_PATIENT_ID].Visible = false;
                C1Patients.Cols[COL_PATIENT_CODE].Visible = false;
                C1Patients.Cols[COL_PATIENT_NAME].Visible = true;
                C1Patients.Cols[COL_GENDER].Visible = true;
                C1Patients.Cols[COL_DOB].Visible = true;
                C1Patients.Cols[COL_PHONE].Visible = true;
                C1Patients.Cols[COL_MOBILE].Visible = true;
                C1Patients.Cols[COL_EMAIL].Visible = true;

                C1Patients.Cols[COL_ADDR1].Visible = true;
                C1Patients.Cols[COL_ADDR2].Visible = false;
                C1Patients.Cols[COL_CITY].Visible = true;
                C1Patients.Cols[COL_STATE].Visible = true;
                C1Patients.Cols[COL_ZIP].Visible = true;


                #endregion

                #region " Styles "

                C1Patients.Tree.Column = COL_PROVIDER_NAME;
                C1Patients.Tree.Style = TreeStyleFlags.Simple; // Simple;
                C1Patients.AllowMerging = AllowMergingEnum.None; //Nodes;

                CellStyle csRootNode;// = C1Patients.Styles.Add("RootNode");
                try
                {
                    if (C1Patients.Styles.Contains("RootNode"))
                    {
                        csRootNode = C1Patients.Styles["RootNode"];
                    }
                    else
                    {
                        csRootNode = C1Patients.Styles.Add("RootNode");
                        csRootNode.Border.Direction = BorderDirEnum.Both;
                        csRootNode.WordWrap = true;
                        csRootNode.Font = gloGlobal.clsgloFont.getFontFromExistingSource(C1Patients.Font, FontStyle.Bold);
                        csRootNode.BackColor = Color.RosyBrown;

                    }

                }
                catch
                {
                    csRootNode = C1Patients.Styles.Add("RootNode");
                    csRootNode.Border.Direction = BorderDirEnum.Both;
                    csRootNode.WordWrap = true;
                    csRootNode.Font = gloGlobal.clsgloFont.getFontFromExistingSource(C1Patients.Font, FontStyle.Bold);
                    csRootNode.BackColor = Color.RosyBrown;

                }
       

                CellStyle cs = C1Patients.Styles.Normal; //Normal;
                cs.Border.Direction = BorderDirEnum.Both; //Vertical;            
                cs.WordWrap = true;
             //   cs = C1Patients.Styles.Add("Parent");
                try
                {
                    if (C1Patients.Styles.Contains("Parent"))
                    {
                        cs = C1Patients.Styles["Parent"];
                    }
                    else
                    {
                        cs = C1Patients.Styles.Add("Parent");
                        cs.Font = gloGlobal.clsgloFont.getFontFromExistingSource(C1Patients.Font, FontStyle.Bold);
                        cs.BackColor = Color.Lavender;

                    }

                }
                catch
                {
                    cs = C1Patients.Styles.Add("Parent");
                    cs.Font = gloGlobal.clsgloFont.getFontFromExistingSource(C1Patients.Font, FontStyle.Bold);
                    cs.BackColor = Color.Lavender;

                }
       

               // cs = C1Patients.Styles.Add("Child");
                try
                {
                    if (C1Patients.Styles.Contains("Child"))
                    {
                        cs = C1Patients.Styles["Child"];
                    }
                    else
                    {
                        cs = C1Patients.Styles.Add("Child");
                        cs.BackColor = Color.AliceBlue;

                    }

                }
                catch
                {
                    cs = C1Patients.Styles.Add("Child");
                    cs.BackColor = Color.AliceBlue;

                }
            

                C1Patients.SelectionMode = SelectionModeEnum.Row;


                #endregion

                C1Patients.AllowEditing = false;
                     
                C1Patients.Tree.Sort(1,SortFlags.Ascending,5,6);
                C1Patients.Cols[COL_DOB].AllowSorting = true;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
            }
        }

        #endregion

        #region " Public & Private Procedures "
        #region "Old code
        //private void FillPatients(Int64 ProviderId)
        //{
        //    gloDatabaseLayer.DBLayer oDB = null;
        //    DataTable dtReferrals = null;
        //    DataTable dtPatients = null;
        //    string _sqlQuery = "";
        //    int rowIndex = 0;
        //    string _Referrals = "";
        //    C1.Win.C1FlexGrid.Node oParentNode = null;
        //    gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

        //    try
        //    {
        //        //DesignGrid();

        //        oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //        oDB.Connect(false);

        //        #region ".1 **Retrive the Refferrals for the Patients who are having Provider with the given ProviderID "

        //        dtReferrals = new DataTable();

        //        _sqlQuery = " SELECT DISTINCT " +
        //                    " ISNULL(Patient_DTL.nContactId,0) AS ReferralContactID,  " +
        //                    " (ISNULL(Patient_DTL.sFirstName,'') + SPACE(1) + ISNULL(Patient_DTL.sMiddleName,'')+SPACE(1)+ ISNULL(Patient_DTL.sLastName,'')) AS ReferralName, " +
        //                    " ISNULL(Patient.nProviderID,0) AS PatientProviderID " +
        //                    " FROM Patient_DTL RIGHT OUTER JOIN " +
        //                    " Patient ON Patient_DTL.nPatientID = Patient.nPatientID " +
        //                    " WHERE (Patient.nProviderID = " + ProviderId + ") " +
        //                    " AND (Patient_DTL.nContactFlag = " + gloPatient.PatientContactType.Referral.GetHashCode() + ") " +
        //                    " AND (Patient.nProviderID IS NOT NULL) AND (Patient.nClinicID = " + _clinicId + ") " +
        //                    " AND (Patient_DTL.nContactID IS NOT NULL) ";

        //        oDB.Retrive_Query(_sqlQuery, out dtReferrals);

        //        #endregion

        //        #region ".2 ** Get the Patient for the given Provider with the given Referrals "

        //        if (dtReferrals != null && dtReferrals.Rows.Count > 0)
        //        {
                    
        //            for (int i = 0; i < dtReferrals.Rows.Count; i++)
        //            {
        //                _Referrals += Convert.ToString(dtReferrals.Rows[i]["ReferralContactID"]) + ",";
        //            }
        //            _Referrals = _Referrals.TrimEnd(',');

        //            if (_Referrals.Trim() != "")
        //            {
        //                _sqlQuery = "";
        //                dtPatients = new DataTable();

        //                _sqlQuery = " SELECT DISTINCT " +
        //                " ISNULL(Patient.nProviderID, 0) AS nProviderID, " +
        //                " (ISNULL(Provider_MST.sFirstName,'')+SPACE(1)+Provider_MST.sMiddleName+SPACE(1)+ Provider_MST.sLastName) AS PatientProvider, " +
        //                " ISNULL(Patient_DTL.nContactId, 0) AS nContactID,  " +
        //                " (ISNULL(Patient_DTL.sFirstName,'')+SPACE(1)+ISNULL(Patient_DTL.sMiddleName,'')+SPACE(1)+ISNULL(Patient_DTL.sLastName,'')) AS Referral,  " +
        //                " ISNULL(Patient.nPatientID, 0) AS nPatientID,  " +
        //                " ISNULL(Patient.sPatientCode, '') AS sPatientCode, ISNULL(Patient.sFirstName, '') + SPACE(1) + ISNULL(Patient.sMiddleName, '') + SPACE(1)+ ISNULL(Patient.sLastName, '') AS PatientName , " +
        //                " ISNULL(Patient.sGender,'') AS sPatientGender,ISNULL(CONVERT(VARCHAR,Patient.dtDOB,101),'') AS DOB , " +
        //                " ISNULL(Patient.sPhone,'') AS sPatientPhone,ISNULL(Patient.sMobile,'') AS sPatientMobile,ISNULL(Patient.sEmail,'') AS sPatientEmail, " +
        //                " ISNULL(Patient.sAddressLine1,'') AS PatientAddr1, " +
        //                " ISNULL(Patient.sAddressLine2,'') AS PatientAddr2, " +
        //                " ISNULL(Patient.sCity,'') AS PatientCity, " +
        //                " ISNULL(Patient.sState,'') AS PatientState, " +
        //                " ISNULL(Patient.sZip,'') AS PatientZip " +
        //                " FROM  Patient LEFT OUTER JOIN " +
        //                " Patient_DTL ON Patient.nPatientID = Patient_DTL.nPatientID LEFT OUTER JOIN " +
        //                " Provider_MST ON Patient.nProviderID = Provider_MST.nProviderID " +
        //                " LEFT OUTER JOIN PatientInsurance_DTL ON Patient.nPatientID = PatientInsurance_DTL.nPatientID " +
        //                " WHERE " +
        //                " (Patient.nProviderID = " + ProviderId + " )  " +
        //                " AND  " +
        //                " (Patient_DTL.nContactId IN (" + _Referrals + ")) AND (ISNULL(patient.nExemptFromReport,0) <> 1) ";         //GLO2010-0006070 : Added this condition (Patient.nExemptFromReport <> 1) so that if Exempt from Report is check-on Patient will not display on report

        //                if (cmbGender.SelectedIndex != -1 && cmbGender.Text != "")
        //                {
        //                    _sqlQuery += " AND (Patient.sGender = '" + cmbGender.Text.Trim() + "') ";
        //                }

        //                if (txtCity.Text.Trim() != "")
        //                {
        //                    _sqlQuery += " AND (Patient.sCity = '" + txtCity.Text.Trim() + "') ";
        //                }

        //                //if (txtState.Text.Trim() != "")
        //                //{
        //                //    _sqlQuery += " AND (Patient.sState = '" + txtState.Text.Trim() + "') ";
        //                //}
        //                if (cmbState.SelectedIndex != -1 && cmbState.SelectedIndex > 0)
        //                {
        //                    _sqlQuery += " AND (Patient.sState = '" + cmbState.Text.Trim() + "') ";
        //                }

        //                if (txtZipCode.Text.Trim() != "")
        //                {
        //                    _sqlQuery += " AND (Patient.sZip = '" + txtZipCode.Text.Trim() + "') ";
        //                }

        //                string _Insurances = "";
        //                if (cmbInsurance.Items.Count > 0)
        //                {
        //                    for (int i = 0; i < cmbInsurance.Items.Count; i++)
        //                    {
        //                        cmbInsurance.SelectedIndex = i;
        //                        _Insurances += "'" + Convert.ToString(cmbInsurance.Text.Replace("'", "''")) + "'" + ",";
        //                    }
        //                }
        //                _Insurances = _Insurances.TrimEnd(',');

        //                if (_Insurances.Trim() != "")
        //                {
        //                    _sqlQuery += " AND (PatientInsurance_DTL.sInsuranceName IN (" + _Insurances + ")) ";
        //                }

        //                oDB.Retrive_Query(_sqlQuery, out dtPatients);

        //            }

        //        }

        //        #endregion

        //        #region ".3 **Fill Grid "
        //        if (dtPatients != null && dtPatients.Rows.Count > 0)
        //        {
        //            for (int p = 0; p < dtPatients.Rows.Count; p++)
        //            {
        //                Int64 _ProviderId = 0;
        //                string _ProviderName = "";
        //                rowIndex = 0;

        //                _ProviderId = Convert.ToInt64(dtPatients.Rows[p]["nProviderID"]);
        //                _ProviderName = Convert.ToString(dtPatients.Rows[p]["PatientProvider"]);

        //                rowIndex = IsExistsNode(_ProviderId.ToString(), 1);
        //                if (rowIndex == 0)
        //                {
        //                    C1Patients.Rows.Add();
        //                    rowIndex = C1Patients.Rows.Count - 1;

        //                    C1Patients.Rows[rowIndex].Style = C1Patients.Styles["RootNode"];
        //                    C1Patients.Rows[rowIndex].IsNode = true;
        //                    C1Patients.Rows[rowIndex].Node.Data = _ProviderName;
        //                    C1Patients.Rows[rowIndex].Node.Level = 1;
        //                    C1Patients.Rows[rowIndex].Node.Key = _ProviderId;
        //                    oParentNode = C1Patients.Rows[rowIndex].Node;
        //                }
        //                else
        //                {
        //                    oParentNode = C1Patients.Rows[rowIndex].Node;
        //                }

        //                rowIndex = 0;
        //                Int64 _ReferralId = 0;
        //                string _ReferralName = "";
        //                C1.Win.C1FlexGrid.Node oChildNode = null;

        //                _ReferralId = Convert.ToInt64(dtPatients.Rows[p]["nContactID"]);
        //                _ReferralName = Convert.ToString(dtPatients.Rows[p]["Referral"]);

        //               // rowIndex = IsExistsNode(_ReferralId.ToString(), 2);

        //                if (rowIndex == 0)
        //                {
        //                    oChildNode = oParentNode.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, _ReferralName, _ReferralId, null);
        //                    oChildNode.Level = 2;
        //                    rowIndex = oChildNode.Row.Index;
        //                    C1Patients.Rows[rowIndex].Style = C1Patients.Styles["Parent"];
        //                    C1Patients.SetData(rowIndex, COL_REFERRAL_ID, _ReferralId);
        //                    C1Patients.SetData(rowIndex, COL_REFERRAL_NAME, _ReferralName);
        //                }
        //                else
        //                {
        //                    oChildNode = C1Patients.Rows[rowIndex].Node;
        //                }

        //                rowIndex = 0;
        //                C1.Win.C1FlexGrid.Node GrandChildNode = null;

        //                GrandChildNode = oChildNode.AddNode(NodeTypeEnum.LastChild, Convert.ToString(dtPatients.Rows[p]["sPatientCode"]), Convert.ToInt64(dtPatients.Rows[p]["nPatientID"]), null);
        //                GrandChildNode.Level = 3;
        //                rowIndex = GrandChildNode.Row.Index;

        //                C1Patients.SetData(rowIndex, COL_PATIENT_ID, Convert.ToInt64(dtPatients.Rows[p]["nPatientID"]));
        //                C1Patients.SetData(rowIndex, COL_PATIENT_CODE, Convert.ToString(dtPatients.Rows[p]["sPatientCode"]));
        //                C1Patients.SetData(rowIndex, COL_PATIENT_NAME, Convert.ToString(dtPatients.Rows[p]["PatientName"]));
        //                C1Patients.SetData(rowIndex, COL_GENDER, Convert.ToString(dtPatients.Rows[p]["sPatientGender"]));
        //                C1Patients.SetData(rowIndex, COL_DOB, Convert.ToString(dtPatients.Rows[p]["DOB"]));
        //                //--sPatientPhone,sPatientMobile,sPatientEmail
        //                C1Patients.SetData(rowIndex, COL_PHONE, Convert.ToString(dtPatients.Rows[p]["sPatientPhone"]));
        //                C1Patients.SetData(rowIndex, COL_MOBILE, Convert.ToString(dtPatients.Rows[p]["sPatientMobile"]));
        //                C1Patients.SetData(rowIndex, COL_EMAIL, Convert.ToString(dtPatients.Rows[p]["sPatientEmail"]));
        //                //--sAddressLine1,sAddressLine2,sCity,sState,sZip
        //                C1Patients.SetData(rowIndex, COL_ADDR1, Convert.ToString(dtPatients.Rows[p]["PatientAddr1"]));
        //                C1Patients.SetData(rowIndex, COL_ADDR2, Convert.ToString(dtPatients.Rows[p]["PatientAddr2"]));
        //                C1Patients.SetData(rowIndex, COL_CITY, Convert.ToString(dtPatients.Rows[p]["PatientCity"]));
        //                C1Patients.SetData(rowIndex, COL_STATE, Convert.ToString(dtPatients.Rows[p]["PatientState"]));
        //                C1Patients.SetData(rowIndex, COL_ZIP, Convert.ToString(dtPatients.Rows[p]["PatientZip"]));

        //            }
        //        }

        //        #endregion

        //    }
        //    catch (gloDatabaseLayer.DBException dbEx)
        //    {
        //        dbEx.ERROR_Log(dbEx.ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
        //    }
        //    finally
        //    {
        //        if (oDB != null)
        //        {
        //            oDB.Disconnect();
        //            oDB.Dispose();
        //        }
        //    }
        //}

        //private void FillPatients(Int64 ProviderId, string Referrals)
        //{
        //    gloDatabaseLayer.DBLayer oDB = null;
        //    DataTable dtReferrals = null;
        //    DataTable dtPatients = null;
        //    string _sqlQuery = "";
        //    int rowIndex = 0;
        //    C1.Win.C1FlexGrid.Node oParentNode = null;
        //   string _Referrals = "";

        //    try
        //    {
        //        //DesignGrid();

        //        oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //        oDB.Connect(false);

        //        #region ".2 ** Get the Patient for the given Provider with the given Referrals "

        //        _Referrals = Referrals;

        //        if (_Referrals.Trim() != "")
        //        {
        //            _sqlQuery = "";
        //            dtPatients = new DataTable();

        //            _sqlQuery = " SELECT DISTINCT " +
        //            " ISNULL(Patient.nProviderID, 0) AS nProviderID, " +
        //            " (ISNULL(Provider_MST.sFirstName,'')+SPACE(1)+Provider_MST.sMiddleName+SPACE(1)+ Provider_MST.sLastName) AS PatientProvider, " +
        //            " ISNULL(Patient_DTL.nContactId, 0) AS nContactID,  " +
        //            " (ISNULL(Patient_DTL.sFirstName,'')+SPACE(1)+ISNULL(Patient_DTL.sMiddleName,'')+SPACE(1)+ISNULL(Patient_DTL.sLastName,'')) AS Referral,  " +
        //            " ISNULL(Patient.nPatientID, 0) AS nPatientID,  " +
        //            " ISNULL(Patient.sPatientCode, '') AS sPatientCode, ISNULL(Patient.sFirstName, '') + SPACE(1) + ISNULL(Patient.sMiddleName, '') + SPACE(1)+ ISNULL(Patient.sLastName, '') AS PatientName , " +
        //            " ISNULL(Patient.sGender,'') AS sPatientGender,ISNULL(CONVERT(varchar,Patient.dtDOB),'') AS DOB, " +
        //            " ISNULL(Patient.sPhone,'') AS sPatientPhone,ISNULL(Patient.sMobile,'') AS sPatientMobile,ISNULL(Patient.sEmail,'') AS sPatientEmail, " +
        //            " ISNULL(Patient.sAddressLine1,'') AS PatientAddr1, " +
        //            " ISNULL(Patient.sAddressLine2,'') AS PatientAddr2, " +
        //            " ISNULL(Patient.sCity,'') AS PatientCity, " +
        //            " ISNULL(Patient.sState,'') AS PatientState, " +
        //            " ISNULL(Patient.sZip,'') AS PatientZip " +
        //            " FROM  Patient LEFT OUTER JOIN " +
        //            " Patient_DTL ON Patient.nPatientID = Patient_DTL.nPatientID LEFT OUTER JOIN " +
        //            " Provider_MST ON Patient.nProviderID = Provider_MST.nProviderID " +
        //            " LEFT OUTER JOIN PatientInsurance_DTL ON Patient.nPatientID = PatientInsurance_DTL.nPatientID " +
        //            " WHERE " +
        //            " (Patient.nProviderID = " + ProviderId + " )  " +
        //            " AND  " +
        //            " (Patient_DTL.nContactId IN (" + _Referrals + ")) AND (ISNULL(patient.nExemptFromReport,0) <> 1)";            //GLO2010-0006070 : Added this condition (Patient.nExemptFromReport <> 1) so that if Exempt from Report is check-on Patient will not display on report

        //            if (cmbGender.SelectedIndex != -1 && cmbGender.Text != "")
        //            {
        //                _sqlQuery += " AND (Patient.sGender = '" + cmbGender.Text.Trim() + "') ";
        //            }

        //            if (txtCity.Text.Trim() != "")
        //            {
        //                _sqlQuery += " AND (Patient.sCity = '" + txtCity.Text.Trim() + "') ";
        //            }

        //            //if (txtState.Text.Trim() != "")
        //            //{
        //            //    _sqlQuery += " AND (Patient.sState = '" + txtState.Text.Trim() + "') ";
        //            //}
        //            if (cmbState.SelectedIndex != -1 && cmbState.SelectedIndex > 0)
        //            {
        //                _sqlQuery += " AND (Patient.sState = '" + cmbState.Text.Trim() + "') ";
        //            }

        //            if (txtZipCode.Text.Trim() != "")
        //            {
        //                _sqlQuery += " AND (Patient.sZip = '" + txtZipCode.Text.Trim() + "') ";
        //            }

        //            string _Insurances = "";
        //            if (cmbInsurance.Items.Count > 0)
        //            {
        //                for (int i = 0; i < cmbInsurance.Items.Count; i++)
        //                {
        //                    cmbInsurance.SelectedIndex = i;
        //                    _Insurances += "'" + Convert.ToString(cmbInsurance.Text.Replace("'", "''")) + "'" + ",";
        //                }
        //            }
        //            _Insurances = _Insurances.TrimEnd(',');

        //            if (_Insurances.Trim() != "")
        //            {
        //                _sqlQuery += " AND (PatientInsurance_DTL.sInsuranceName IN (" + _Insurances  + ")) ";
        //            }


        //            oDB.Retrive_Query(_sqlQuery, out dtPatients);
        //        }

        //        #endregion

        //        #region ".3 **Fill Grid "

        //        if (dtPatients != null && dtPatients.Rows.Count > 0)
        //        {
        //            for (int p = 0; p < dtPatients.Rows.Count; p++)
        //            {
        //                Int64 _ProviderId = 0;
        //                string _ProviderName = "";
        //                rowIndex = 0;

        //                _ProviderId = Convert.ToInt64(dtPatients.Rows[p]["nProviderID"]);
        //                _ProviderName = Convert.ToString(dtPatients.Rows[p]["PatientProvider"]);

        //                rowIndex = IsExistsNode(_ProviderId.ToString(), 1);
        //                if (rowIndex == 0)
        //                {
        //                    C1Patients.Rows.Add();
        //                    rowIndex = C1Patients.Rows.Count - 1;

        //                    C1Patients.Rows[rowIndex].Style = C1Patients.Styles["RootNode"];
        //                    C1Patients.Rows[rowIndex].IsNode = true;
        //                    C1Patients.Rows[rowIndex].Node.Data = _ProviderName;
        //                    C1Patients.Rows[rowIndex].Node.Level = 1;
        //                    C1Patients.Rows[rowIndex].Node.Key = _ProviderId;
        //                    oParentNode = C1Patients.Rows[rowIndex].Node;
        //                }
        //                else
        //                {
        //                    oParentNode = C1Patients.Rows[rowIndex].Node;
        //                }

        //                rowIndex = 0;
        //                Int64 _ReferralId = 0;
        //                string _ReferralName = "";
        //                C1.Win.C1FlexGrid.Node oChildNode = null;

        //                _ReferralId = Convert.ToInt64(dtPatients.Rows[p]["nContactID"]);
        //                _ReferralName = Convert.ToString(dtPatients.Rows[p]["Referral"]);

        //                rowIndex = IsExistsNode(_ReferralId.ToString(), 2);

        //                if (rowIndex == 0)
        //                {
        //                    oChildNode = oParentNode.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, _ReferralName, _ReferralId, null);
        //                    oChildNode.Level = 2;
        //                    rowIndex = oChildNode.Row.Index;
        //                    C1Patients.Rows[rowIndex].Style = C1Patients.Styles["Parent"];
        //                    C1Patients.SetData(rowIndex, COL_REFERRAL_ID, _ReferralId);
        //                    C1Patients.SetData(rowIndex, COL_REFERRAL_NAME, _ReferralName);
        //                }
        //                else
        //                {
        //                    oChildNode = C1Patients.Rows[rowIndex].Node;
        //                }

        //                rowIndex = 0;
        //                C1.Win.C1FlexGrid.Node GrandChildNode = null;

        //                GrandChildNode = oChildNode.AddNode(NodeTypeEnum.LastChild, Convert.ToString(dtPatients.Rows[p]["sPatientCode"]), Convert.ToInt64(dtPatients.Rows[p]["nPatientID"]), null);
        //                GrandChildNode.Level = 3;
        //                rowIndex = GrandChildNode.Row.Index;

        //                C1Patients.SetData(rowIndex, COL_PATIENT_ID, Convert.ToInt64(dtPatients.Rows[p]["nPatientID"]));
        //                C1Patients.SetData(rowIndex, COL_PATIENT_CODE, Convert.ToString(dtPatients.Rows[p]["sPatientCode"]));
        //                C1Patients.SetData(rowIndex, COL_PATIENT_NAME, Convert.ToString(dtPatients.Rows[p]["PatientName"]));
        //                C1Patients.SetData(rowIndex, COL_GENDER, Convert.ToString(dtPatients.Rows[p]["sPatientGender"]));
        //                C1Patients.SetData(rowIndex, COL_DOB, Convert.ToString(dtPatients.Rows[p]["DOB"]));
        //                C1Patients.SetData(rowIndex, COL_PHONE, Convert.ToString(dtPatients.Rows[p]["sPatientPhone"]));
        //                C1Patients.SetData(rowIndex, COL_MOBILE, Convert.ToString(dtPatients.Rows[p]["sPatientMobile"]));
        //                C1Patients.SetData(rowIndex, COL_EMAIL, Convert.ToString(dtPatients.Rows[p]["sPatientEmail"]));
        //                //--sAddressLine1,sAddressLine2,sCity,sState,sZip
        //                C1Patients.SetData(rowIndex, COL_ADDR1, Convert.ToString(dtPatients.Rows[p]["PatientAddr1"]));
        //                C1Patients.SetData(rowIndex, COL_ADDR2, Convert.ToString(dtPatients.Rows[p]["PatientAddr2"]));
        //                C1Patients.SetData(rowIndex, COL_CITY, Convert.ToString(dtPatients.Rows[p]["PatientCity"]));
        //                C1Patients.SetData(rowIndex, COL_STATE, Convert.ToString(dtPatients.Rows[p]["PatientState"]));
        //                C1Patients.SetData(rowIndex, COL_ZIP, Convert.ToString(dtPatients.Rows[p]["PatientZip"]));
        //            }
        //        }
        //        #endregion

        //    }
        //    catch (gloDatabaseLayer.DBException dbEx)
        //    {
        //        dbEx.ERROR_Log(dbEx.ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
        //    }
        //    finally
        //    {
        //        if (oDB != null)
        //        {
        //            oDB.Disconnect();
        //            oDB.Dispose();
        //        }
        //    }
        //}
        #endregion Old Code

        private void FillPatients(Int64 ProviderId, string Referrals)
        {
            gloDatabaseLayer.DBLayer oDB = null;
            DataTable dtPatients = null;
            int rowIndex = 0;
            C1.Win.C1FlexGrid.Node oParentNode = null;
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

            try
            {
                //DesignGrid();

                
                string _InsurancesPlanIDs = "";
                if (cmbInsurance.Items.Count > 0)
                {
                    for (int i = 0; i < cmbInsurance.Items.Count; i++)
                    {
                        cmbInsurance.SelectedIndex = i;
                        _InsurancesPlanIDs += Convert.ToInt64(cmbInsurance.SelectedValue) + ",";
                    }
                }
                _InsurancesPlanIDs = _InsurancesPlanIDs.TrimEnd(',');

                #region " **Fill Grid "
                    oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    oDB.Connect(false);
                    oDBParameters.Add("@ProviderId", ProviderId, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@ReferralIds", Referrals, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@InsurancePlanIds",_InsurancesPlanIDs, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@State", cmbState.Text.ToString(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@Gender",  cmbGender.Text.ToString(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@City",  txtCity.Text.Trim(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@Zip",  txtZipCode.Text.Trim(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDB.Retrive("gsp_SSRSCustomReport_ProviderReferralPatientDetails", oDBParameters, out dtPatients);




                if (dtPatients != null && dtPatients.Rows.Count > 0)
                {
                    for (int p = 0; p < dtPatients.Rows.Count; p++)
                    {
                        Int64 _ProviderId = 0;
                        string _ProviderName = "";
                        rowIndex = 0;
                        int nProviderIndex = 0;
                        int nreferalIndex = 0;

                        _ProviderId = Convert.ToInt64(dtPatients.Rows[p]["nProviderID"]);
                        _ProviderName = Convert.ToString(dtPatients.Rows[p]["PatientProvider"]);

                        //rowIndex = IsExistsNode(_ProviderId.ToString(), 1);
                        nProviderIndex = IsExistsNode(_ProviderId.ToString(), 1);

                        if (nProviderIndex == 0)
                        {
                            C1Patients.Rows.Add();
                            rowIndex = C1Patients.Rows.Count - 1;

                            C1Patients.Rows[rowIndex].Style = C1Patients.Styles["RootNode"];
                            C1Patients.Rows[rowIndex].IsNode = true;
                            C1Patients.Rows[rowIndex].Node.Data = _ProviderName;
                            C1Patients.Rows[rowIndex].Node.Level = 1;
                            C1Patients.Rows[rowIndex].Node.Key = _ProviderId;
                            oParentNode = C1Patients.Rows[rowIndex].Node;
                        }
                        else
                        {
                            oParentNode = C1Patients.Rows[nProviderIndex].Node;
                        }

                        rowIndex = 0;
                        Int64 _ReferralId = 0;
                        string _ReferralName = "";
                        C1.Win.C1FlexGrid.Node oChildNode = null;

                        _ReferralId = Convert.ToInt64(dtPatients.Rows[p]["nContactID"]);
                        _ReferralName = Convert.ToString(dtPatients.Rows[p]["Referral"]);

                        //rowIndex = IsExistsNode(_ReferralId.ToString(), 2);
                        nreferalIndex = IsExistsNode(_ReferralId.ToString(), 2, _ProviderId.ToString());

                        if (nreferalIndex == 0)
                        {
                            oChildNode = oParentNode.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, _ReferralName, _ReferralId, null);
                            oChildNode.Level = 2;
                            rowIndex = oChildNode.Row.Index;
                            C1Patients.Rows[rowIndex].Style = C1Patients.Styles["Parent"];
                            C1Patients.SetData(rowIndex, COL_REFERRAL_ID, _ReferralId);
                            C1Patients.SetData(rowIndex, COL_REFERRAL_NAME, _ReferralName);
                        }
                        else
                        {
                            oChildNode = C1Patients.Rows[nreferalIndex].Node;
                        }

                        rowIndex = 0;
                        C1.Win.C1FlexGrid.Node GrandChildNode = null;

                        GrandChildNode = oChildNode.AddNode(NodeTypeEnum.LastChild, Convert.ToString(dtPatients.Rows[p]["sPatientCode"]), Convert.ToInt64(dtPatients.Rows[p]["nPatientID"]), null);
                        GrandChildNode.Level = 3;
                        rowIndex = GrandChildNode.Row.Index;

                        C1Patients.SetData(rowIndex, COL_PATIENT_ID, Convert.ToInt64(dtPatients.Rows[p]["nPatientID"]));
                        C1Patients.SetData(rowIndex, COL_PATIENT_CODE, Convert.ToString(dtPatients.Rows[p]["sPatientCode"]));
                        C1Patients.SetData(rowIndex, COL_PATIENT_NAME, Convert.ToString(dtPatients.Rows[p]["PatientName"]));
                        C1Patients.SetData(rowIndex, COL_GENDER, Convert.ToString(dtPatients.Rows[p]["sPatientGender"]));
                        C1Patients.SetData(rowIndex, COL_DOB, Convert.ToString(dtPatients.Rows[p]["DOB"]));
                        C1Patients.SetData(rowIndex, COL_PHONE, Convert.ToString(dtPatients.Rows[p]["sPatientPhone"]));
                        C1Patients.SetData(rowIndex, COL_MOBILE, Convert.ToString(dtPatients.Rows[p]["sPatientMobile"]));
                        C1Patients.SetData(rowIndex, COL_EMAIL, Convert.ToString(dtPatients.Rows[p]["sPatientEmail"]));
                        //--sAddressLine1,sAddressLine2,sCity,sState,sZip
                        C1Patients.SetData(rowIndex, COL_ADDR1, Convert.ToString(dtPatients.Rows[p]["PatientAddr1"]));
                        C1Patients.SetData(rowIndex, COL_ADDR2, Convert.ToString(dtPatients.Rows[p]["PatientAddr2"]));
                        C1Patients.SetData(rowIndex, COL_CITY, Convert.ToString(dtPatients.Rows[p]["PatientCity"]));
                        C1Patients.SetData(rowIndex, COL_STATE, Convert.ToString(dtPatients.Rows[p]["PatientState"]));
                        C1Patients.SetData(rowIndex, COL_ZIP, Convert.ToString(dtPatients.Rows[p]["PatientZip"]));
                    }
                }
                #endregion

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
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

        private int IsExistsNode(string KeyValue, int NodeLevel, string ParentKeyValue = "")
        {
            int _isExistsNode = 0;

            try
            {
                if (C1Patients != null && C1Patients.Rows.Count > 0)
                {
                    for (int i = 1; i <= C1Patients.Rows.Count - 1; i++)
                    {
                        if (C1Patients.Rows[i].Node.Level == NodeLevel)
                        {
                            if (C1Patients.Rows[i].Node.Key.ToString() == KeyValue)
                            {
                                if (ParentKeyValue == "")
                                {
                                    _isExistsNode = i;
                                    break;
                                }
                                else if (C1Patients.Rows[i].Node.Parent.Key.ToString() == ParentKeyValue)
                                {
                                    _isExistsNode = i;
                                    break;
                                }
                            }
                        }
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
            return _isExistsNode;
        }

        private int IsExistsNode(string KeyValue, int NodeLevel, int RowStart, int RowEnd)
        {
            int _isExistsNode = 0;

            try
            {
                if (C1Patients != null && C1Patients.Rows.Count > 0)
                {
                    for (int i = RowStart; i <= RowEnd - 1; i++)
                    {
                        if (C1Patients.Rows[i].Node.Level == NodeLevel)
                        {
                            if (C1Patients.Rows[i].Node.Key.ToString() == KeyValue)
                            {
                                _isExistsNode = i;
                                break;
                            }
                        }
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
            return _isExistsNode;
        }

        private DataTable GetProviders()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtProviders = null;
            string _sqlQuery = "";

            try
            {
                oDB.Connect(false);
                _sqlQuery = " SELECT ISNULL(nProviderID,0) AS nProviderID FROM Provider_MST WHERE nClinicID = " + _clinicId + " " +
                            " ORDER BY nProviderID ";
                oDB.Retrive_Query(_sqlQuery, out dtProviders);
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }
            return dtProviders;
        }
       // #region comment Old code
        //private void ShowReport()
        //{
        //    DesignGrid();

        //    if (cmbProvider.Items.Count > 0 && cmbProvider.SelectedIndex != -1)
        //    {
        //        string _Referrals = "";
        //        if (cmbReferral.Items.Count > 0)
        //        {
        //            for (int i = 0; i < cmbReferral.Items.Count; i++)
        //            {
        //                cmbReferral.SelectedIndex = i;
        //                _Referrals += Convert.ToString(cmbReferral.SelectedValue) + ",";
        //            }
        //            _Referrals = _Referrals.TrimEnd(',');
        //            if (_Referrals.Trim() != "")
        //            {
        //                FillPatients(Convert.ToInt64(cmbProvider.SelectedValue), _Referrals);
        //            }
        //        }



        //        else
        //        {
        //            FillPatients(Convert.ToInt64(cmbProvider.SelectedValue));
        //        }

        //    }
        //    else
        //    {
        //        DataTable dtProviders = GetProviders();
        //        string _Referrals = "";

        //        if (cmbReferral.Items.Count > 0)
        //        {
        //            for (int j = 0; j < cmbReferral.Items.Count; j++)
        //            {
        //                cmbReferral.SelectedIndex = j;
        //                _Referrals += Convert.ToString(cmbReferral.SelectedValue) + ",";
        //            }
        //            _Referrals = _Referrals.TrimEnd(',');
        //        }

        //        if (dtProviders != null && dtProviders.Rows.Count > 0)
        //        {
        //            for (int i = 0; i < dtProviders.Rows.Count; i++)
        //            {

        //                if (_Referrals.Trim() != "")
        //                {
        //                    FillPatients(Convert.ToInt64(dtProviders.Rows[i]["nProviderID"]), _Referrals);
        //                }
        //                else
        //                {
        //                    FillPatients(Convert.ToInt64(dtProviders.Rows[i]["nProviderID"]));
        //                }
        //            }
        //        }
        //    }
        //}
       // #endregion 
        private void ShowReport()
        {
            DesignGrid();

            if (cmbProvider.Items.Count > 0 && cmbProvider.SelectedIndex != -1)
            {
                string _Referrals = "";
                if (cmbReferral.Items.Count > 0)
                {
                    for (int i = 0; i < cmbReferral.Items.Count; i++)
                    {
                        cmbReferral.SelectedIndex = i;
                        _Referrals += Convert.ToString(cmbReferral.SelectedValue) + ",";
                    }
                    _Referrals = _Referrals.TrimEnd(',');
                    if (_Referrals.Trim() != "")
                    {
                        FillPatients(Convert.ToInt64(cmbProvider.SelectedValue), _Referrals);
                    }
                }



                else
                {
                    FillPatients(Convert.ToInt64(cmbProvider.SelectedValue),null);
                }

            }
            else
            {
                string _Referrals = "";

                if (cmbReferral.Items.Count > 0)
                {
                    for (int j = 0; j < cmbReferral.Items.Count; j++)
                    {
                        cmbReferral.SelectedIndex = j;
                        _Referrals += Convert.ToString(cmbReferral.SelectedValue) + ",";
                    }
                    _Referrals = _Referrals.TrimEnd(',');
                }

                if (_Referrals.Trim() != "")
                {
                    FillPatients(Convert.ToInt64(cmbProvider.SelectedValue), _Referrals);
                }
                else
                {
                    FillPatients(Convert.ToInt64(cmbProvider.SelectedValue),null);
                }
                        
            }
            
        }
        private void FillStates()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtStates = new DataTable();
            try
            {
                oDB.Connect(false);
                string _sqlQuery = "SELECT DISTINCT ISNULL(ST,'') AS States FROM CSZ_MST";
                oDB.Retrive_Query(_sqlQuery, out dtStates);
                if (dtStates != null && dtStates.Rows.Count > 0)
                {
                    DataRow dr = dtStates.NewRow();
                    dr["States"] = "";
                    dtStates.Rows.InsertAt(dr, 0);

                    cmbState.DataSource = dtStates;
                    cmbState.DisplayMember = dtStates.Columns["States"].ColumnName;
                    cmbState.ValueMember = dtStates.Columns["States"].ColumnName;
                    cmbState.SelectedIndex = -1;
                    
                }
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
        private void FillGender()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtGender = new DataTable();
            try
            {
                oDB.Connect(false);
                string _sqlQuery = "SELECT  nCategoryid id,sCode,sDescription as Gender FROM category_mst WHERE UPPER(sCategoryType) ='Gender' AND bIsBlocked = '" + false + "' AND nClinicID = " + _clinicId + " order by sDescription ";
                oDB.Retrive_Query(_sqlQuery, out dtGender);
                if (dtGender != null && dtGender.Rows.Count > 0)
                {
                    DataRow dr = dtGender.NewRow();
                    dr["Gender"] = "";
                    dtGender.Rows.InsertAt(dr, 0);
                    dtGender.AcceptChanges();

                    cmbGender.BeginUpdate();
                    cmbGender.DataSource = dtGender;
                    cmbGender.DisplayMember = dtGender.Columns["Gender"].ColumnName;
                    cmbGender.ValueMember = dtGender.Columns["Gender"].ColumnName;
                    cmbGender.SelectedIndex = -1;
                    cmbGender.EndUpdate();

                }
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
           // gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
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

                C1Patients.SaveExcel(_FilePath, "sheet1", C1.Win.C1FlexGrid.FileFlags.IncludeFixedCells);

                if (OpenReport == true)
                {
                    if (File.Exists(_FilePath) == true)
                    { System.Diagnostics.Process.Start(_FilePath); }
                }
                else
                {
                    MessageBox.Show("File saved successfully.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (IOException ioEx)
            {
                MessageBox.Show("File in use. Fail to export report.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ioEx.ToString(), true);
                ioEx.ToString();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

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

       

       
    }
}