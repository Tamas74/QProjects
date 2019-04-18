using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using gloCommon;
using Wd = Microsoft.Office.Interop.Word;
using Microsoft.VisualBasic;
using gloWord;
namespace gloOffice
{
    public partial class frmBatchPrinting : Form
    {
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        gloListControl.gloListControl oListControl = null;
       // gloListControl.gloListControlType _CurrentControlType = gloListControl.gloListControlType.Other;


        private Int64 _clinicId = 0;
        private Int64 _PatientID = 0;
        private String _PatientName = "";
        private String _SelectedFilterCombo = "";

        private bool FormLoading = true;

        String _databaseconnectionstring = "";
        string _messageBoxCaption = String.Empty;
        DataTable oBindTable = new DataTable();

        private const int COL_SELECT = 0;
        private const int COL_APPOINTMENTID = 1;
        private const int COL_DATE = 2;
        private const int COL_TIME = 3;
        private const int COL_TYPE  = 4;
        private const int COL_PATIENT = 5;
        private const int COL_PROVIDER = 6;
        private const int COL_LOCATION = 7;
        private const int COL_STATUS =8;
        private const int COL_PATIENTID = 9;   
        private const int COL_DTLAPPOINTMENTID =10;
        private const int COL_nDATE= 11;
        private const int COL_nTIME = 12;
        private const int COL_COLCOUNT = 13;

        //private bool _isTreeNodeLoading = false; 



        private DateTime _dtClinicStartTime = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " 09:00 AM");
        private DateTime _dtClinicEndTime = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " 06:00 PM");
        struct TemplateDetails
        {
            public Int64 templateID;
            public Int64 CategoryID;
            public String CategoryName;
            public String TemplateName;
            public String TemplateFilePath;
            public Boolean IsContainsPatientAccountFields;
        }
        struct PatientDetails
        {
            public Int64 patientID;
            public String patientName;
            public Boolean  IsHaveMultipleAccounts;
            public Int64 PAccountID;
            public Int64 AppointmentID;
            public String AppoinmentTime;
             //Bug #92723: 00001067: Appointment 
            public Int64 AppointmentDate;
        }
        public frmBatchPrinting(String DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;

            #region " Retrieve ClinicID from AppSettings "

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _clinicId = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _clinicId = 0; }
            }
            else
            { _clinicId = 0; }

            #endregion

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
        ToolTip oToolTip = null;
        private void frmBatchPrinting_Load(object sender, EventArgs e)
        {
            try
            {

                Cls_TabIndexSettings.TabScheme scheme = Cls_TabIndexSettings.TabScheme.AcrossFirst;
                Cls_TabIndexSettings tom = new Cls_TabIndexSettings(this);
                // This method actually sets the order all the way down the control hierarchy.
                tom.SetTabOrder(scheme);

                oToolTip = new ToolTip();
                oToolTip.SetToolTip(btnChkInSelectAll, "Select All");
                oToolTip.SetToolTip(btnChkInClearAll, "Clear All");

                oToolTip.SetToolTip(btnChkInSelectAllTreeNode, "Select All");
                oToolTip.SetToolTip(btnChkInClearAllTreeNode, "Clear All");

                oToolTip.SetToolTip(btnApptLetterSelectAll, "Select All");
                oToolTip.SetToolTip(btnApptLetterClearAll, "Clear All");

                oToolTip.SetToolTip(btnApptLetterSelectAllTreeNode, "Select All");
                oToolTip.SetToolTip(btnApptLetterClearAllTreeNode, "Clear All");


                btnChkInSelectAllTreeNode.Visible = true;
                btnChkInClearAllTreeNode.Visible = false;

                btnApptLetterSelectAllTreeNode.Visible = true;
                btnApptLetterClearAllTreeNode.Visible = false;

                gloC1FlexStyle.Style(c1ChkInPatients, false);
                gloC1FlexStyle.Style(c1ApptLetterPatients, false);


                Fill_FilterDatesCombo();
                SetTiming();
                DesignGrid();
                FillTemplate();
                //tbBatchPrint_SelectedIndexChanged(sender, e);

                //    BindGrid();
                //    //fill_Providers();
                //    fillProviders();
                //    fill_Templates();
                //    pnl_Prgsbar.Visible = false;
                //    prgBar_Print.Visible = false;
                //    Fill_FilterDatesCombo();
                //    Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


         #region "Commented code"

        private void fill_Providers()
        {
            gloAppointmentBook.gloAppointmentBook oAppointmentBook = new gloAppointmentBook.gloAppointmentBook(_databaseconnectionstring);
            DataTable dt = new DataTable();
            dt = oAppointmentBook.getProviders();
            //nProviderID, ProviderName
            if (dt != null)
            {
                cmbChkInProvider.DataSource = dt;
                cmbChkInProvider.ValueMember = dt.Columns["nProviderID"].ColumnName;
                cmbChkInProvider.DisplayMember = dt.Columns["ProviderName"].ColumnName;
                if (dt.Rows.Count > 0)
                {
                    cmbChkInProvider.SelectedIndex = 0;
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TreeNode oNode = new TreeNode();
                    oNode.Text = dt.Columns["ProviderName"].ColumnName;
                    oNode.Tag = dt.Columns["nProviderID"].ColumnName;
                    trvChkInTemplate.Nodes.Add(oNode);
                    oNode = null;
                }
            }
        }

        //Added By MaheshB
        private void fillProviders()
        {

            //gloAppointmentBook.gloAppointmentBook oAppointmentBook = new gloAppointmentBook.gloAppointmentBook(_databaseconnectionstring);
            //DataTable dt = new DataTable();
            //try
            //{
            //    dt = oAppointmentBook.getProviders();
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        TreeNode oNode = new TreeNode();
            //        oNode.Text = dt.Rows[i]["ProviderName"].ToString();
            //        oNode.Tag = dt.Rows[i]["nProviderID"].ToString();
            //        trvProvider.Nodes.Add(oNode);
            //        oNode = null;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            //}
        }

        private void fill_Templates()
        {
            trvTemplates.Nodes.Clear();
            gloTemplate ogloTemplate = new gloTemplate(_databaseconnectionstring);
            //gloBilling.Category oCategory = new gloBilling.Category(_databaseconnectionstring);
            DataTable dt = new DataTable();
            try
            {
                //dt = ogloTemplate.GetList("Template");
                dt = ogloTemplate.GetTemplateCategoryList();
                //nCategoryID, sDescription
                if (dt != null)
                {
                    int i;
                    for (i = 0; i < dt.Rows.Count; i++)
                    {
                        TreeNode oNode = new TreeNode();
                        oNode.Text = dt.Rows[i]["CategoryName"].ToString();
                        //oNode.Tag = dt.Rows[i]["nCategoryID"];
                        trvTemplates.Nodes.Add(oNode);

                        DataTable dtTemplates;
                        //Retrieve and fill all templates for a given Category,
                        //dtTemplates = ogloTemplate.GetTemplates(Convert.ToInt64(oNode.Tag));
                        dtTemplates = ogloTemplate.GetTemplates(oNode.Text.ToString());
                        //nTemplateID, sTemplateName
                        if (dtTemplates != null)
                        {
                            int j;
                            for (j = 0; j < dtTemplates.Rows.Count; j++)
                            {
                                TreeNode oTemplateNode = new TreeNode();
                                oTemplateNode.Text = dtTemplates.Rows[j]["sTemplateName"].ToString();
                                oTemplateNode.Tag = dtTemplates.Rows[j]["nTemplateID"];
                                oNode.Nodes.Add(oTemplateNode);
                            }
                        }
                    }
                }
                trvTemplates.ExpandAll();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void BindGrid()
        {
            try
            {
                //c1ChkInPatients.Clear();
                c1ChkInPatients.DataSource = null;
                c1ChkInPatients.Cols.Fixed = 0;
                oBindTable.Columns.Add("Select");
                oBindTable.Columns.Add("PatientID");
                oBindTable.Columns.Add("Patient Name");
                c1ChkInPatients.DataSource = oBindTable;
                c1ChkInPatients.Cols[0].Width = Convert.ToInt32(c1ChkInPatients.Width * 0.2);
                c1ChkInPatients.Cols[1].Width = 0;
                c1ChkInPatients.Cols[2].Width = Convert.ToInt32(c1ChkInPatients.Width * 0.8);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

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

                    }
                    catch { }
                    oListControl.Dispose();
                    oListControl = null;
                }
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Patient, true, this.Width);
                oListControl.ClinicID = _clinicId;
                oListControl.ControlHeader = " Patient";

                //_CurrentControlType = gloListControl.gloListControlType.Patient;

                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                this.Controls.Add(oListControl);
                pnl_tlspTOP.Visible = false;

                for (int i = 1; i < c1ChkInPatients.Rows.Count; i++)
                {
                    _PatientID = Convert.ToInt64(c1ChkInPatients.Rows[i][0]);
                    _PatientName = c1ChkInPatients.Rows[i][1].ToString();
                    oListControl.SelectedItems.Add(_PatientID, _PatientName);
                }

                oListControl.OpenControl();
                oListControl.Dock = DockStyle.Fill;
                oListControl.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClearPatient_Click(object sender, EventArgs e)
        {
            try
            {
                oBindTable.Clear();
                c1ChkInPatients.DataSource = oBindTable;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }
     
        private void ts_btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
               // Refresh();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void btn_MouseHover(object sender, EventArgs e)
        {
            // ((Button)sender).BackgroundImage = global::gloPatient.Properties.Resources.Img_Yellow;
            //((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btn_MouseLeave(object sender, EventArgs e)
        {
            // ((Button)sender).BackgroundImage = global::gloPatient.Properties.Resources.Img_Button;
            // ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void cmbProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void Refresh()
        {
            String strSQL = "";
            try
            {
                //20100109 Mahesh Nawal Create the query for not shown the Deleted,cancel,Not shown status of the appointment.

                strSQL = "SELECT DISTINCT AS_Appointment_MST.nPatientID, ISNULL(Patient.sFirstName,'') + SPACE(1) + ISNULL(Patient.sMiddleName,'') + SPACE(1) + ISNULL(Patient.sLastName,'') AS PatientName " +
                "FROM BL_Transaction_Lines INNER JOIN BL_Transaction_MST ON BL_Transaction_Lines.nTransactionID = BL_Transaction_MST.nTransactionID RIGHT OUTER JOIN  " +
                " AS_Appointment_DTL INNER JOIN AS_Appointment_MST ON AS_Appointment_DTL.nMSTAppointmentID = AS_Appointment_MST.nMSTAppointmentID INNER JOIN " +
                "Patient ON AS_Appointment_MST.nPatientID = Patient.nPatientID LEFT OUTER JOIN PatientInsurance_DTL ON Patient.nPatientID = PatientInsurance_DTL.nPatientID ON BL_Transaction_MST.nPatientID = Patient.nPatientID " +
                " WHERE	 AS_Appointment_DTL.dtStartDate >= " + gloDateMaster.gloDate.DateAsNumber(dtpChkInDate.Value.ToShortDateString()) + " AND  AS_Appointment_DTL.dtStartDate <=" + gloDateMaster.gloDate.DateAsNumber(dtp_ToDate.Value.ToShortDateString()) + 
                " and AS_Appointment_DTL.nUsedStatus <> 5 and AS_Appointment_DTL.nUsedStatus <> 6 and AS_Appointment_DTL.nUsedStatus <> 7 ";

                string _strProviderIDs = "";
                int j = 0;
                if (trvChkInTemplate.Nodes.Count > 0)
                {

                    for (int i = 0; i < trvChkInTemplate.Nodes.Count; i++)
                    {

                        //cmbProvider.SelectedIndex = i;
                        //cmbProvider.Refresh();
                        //if (i == 0)
                        //{
                        //    _strProviderIDs = "(" + Convert.ToInt64(trvProvider.Nodes.Count);
                        //}
                        //else
                        //{
                        //    _strProviderIDs += "," + Convert.ToInt64(cmbProvider.SelectedValue);
                        //}

                        //if (i == cmbProvider.Items.Count - 1)
                        //{
                        //    _strProviderIDs += ")";
                        //}
                        if (trvChkInTemplate.Nodes[i].Checked)
                        {
                            if (j == 0)
                            {
                                _strProviderIDs = "(" + Convert.ToInt64(trvChkInTemplate.Nodes[i].Tag);
                            }
                            else
                            {
                                _strProviderIDs += "," + Convert.ToInt64(trvChkInTemplate.Nodes[i].Tag);
                            }
                            j++;
                        }

                    }
                    if (j != 0)
                    {
                        _strProviderIDs += ")";
                    }
                }

                if (_strProviderIDs != "")
                    strSQL += " AND AS_Appointment_DTL.nASBaseID IN " + _strProviderIDs + " AND AS_Appointment_DTL.nASBaseFlag = 1 ";



                //" Patient ON AS_Appointment_MST.nPatientID = Patient.nPatientID " +
                //SELECT DISTINCT AS_Appointment_MST.nPatientID, ISNULL(Patient.sFirstName,'') + SPACE(1) + ISNULL(Patient.sMiddleName,'') + SPACE(1) + ISNULL(Patient.sLastName,'') AS PatientName  
                //FROM	
                // BL_Transaction_Lines INNER JOIN
                //BL_Transaction_MST ON BL_Transaction_Lines.nTransactionID = BL_Transaction_MST.nTransactionID RIGHT OUTER JOIN
                //AS_Appointment_DTL INNER JOIN
                //AS_Appointment_MST ON AS_Appointment_DTL.nMSTAppointmentID = AS_Appointment_MST.nMSTAppointmentID INNER JOIN
                //Patient ON AS_Appointment_MST.nPatientID = Patient.nPatientID LEFT OUTER JOIN
                //PatientInsurance_DTL ON Patient.nPatientID = PatientInsurance_DTL.nPatientID ON BL_Transaction_MST.nPatientID = Patient.nPatientID
                //WHERE	 AS_Appointment_DTL.dtStartDate >= 20081124 AND  AS_Appointment_DTL.dtStartDate <=20081124 
                //AND AS_Appointment_DTL.nASBaseID =8 AND AS_Appointment_DTL.nASBaseFlag = 1
                //AND (PatientInsurance_DTL.sInsuranceName IN ('')) 
                //AND (BL_Transaction_Lines.sCPTCode IN ('')) 
                //AND (BL_Transaction_Lines.sDx1Code IN ('') OR BL_Transaction_Lines.sDx2Code IN ('') OR BL_Transaction_Lines.sDx3Code IN ('') OR BL_Transaction_Lines.sDx4Code IN (''))

                //Added Ciiterias Insurance,Cpt,Diagnosis 

                if (cmbChkInInsuranceCompany.Items.Count > 0)
                {
                    string _strInsuranceNames = "";
                    for (int i = 0; i < cmbChkInInsuranceCompany.Items.Count; i++)
                    {

                        cmbChkInInsuranceCompany.SelectedIndex = i;
                        cmbChkInInsuranceCompany.Refresh();
                        if (i == 0)
                        {
                            _strInsuranceNames = "('" + Convert.ToString(cmbChkInInsuranceCompany.Text).Replace("'", "''").ToUpper() + "'";
                        }
                        else
                        {
                            _strInsuranceNames += ",'" + Convert.ToString(cmbChkInInsuranceCompany.Text).Replace("'", "''").ToUpper() + "'";
                        }

                        if (i == cmbChkInInsuranceCompany.Items.Count - 1)
                        {
                            _strInsuranceNames += ")";
                        }
                    }

                    if (_strInsuranceNames != "")
                        strSQL += " AND ((UPPER(REPLACE(ISNULL(PatientInsurance_DTL.sInsuranceName,''),'''',''))) IN " + _strInsuranceNames + ")";
                    // strSQL += " AND (PatientInsurance_DTL.sInsuranceName IN " + _strInsuranceNames + ")";
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
                            _strDiagnosisCodes = "('" + cmbDiagnosisCode.Text.Trim().Replace("'", "''") + "'";
                        }
                        else
                        {
                            _strDiagnosisCodes += ",'" + cmbDiagnosisCode.Text.Trim().Replace("'", "''") + "'";
                        }

                        if (i == cmbDiagnosisCode.Items.Count - 1)
                        {
                            _strDiagnosisCodes += ")";
                        }
                    }

                    if (_strDiagnosisCodes != "")
                        strSQL += "AND (BL_Transaction_Lines.sDx1Code IN " + _strDiagnosisCodes + " OR BL_Transaction_Lines.sDx2Code IN " + _strDiagnosisCodes + " OR BL_Transaction_Lines.sDx3Code IN " + _strDiagnosisCodes + " OR BL_Transaction_Lines.sDx4Code IN " + _strDiagnosisCodes + ")";
                }

                if (cmbCPT.Items.Count > 0)
                {
                    string _strCPTCodes = "";

                    for (int i = 0; i < cmbCPT.Items.Count; i++)
                    {

                        cmbCPT.SelectedIndex = i;
                        if (i == 0)
                        {
                            _strCPTCodes = "('" + cmbCPT.Text.Replace("'", "''").Trim() + "'";
                        }
                        else
                        {
                            _strCPTCodes += ",'" + cmbCPT.Text.Replace("'", "''").Trim() + "'";
                        }

                        if (i == cmbCPT.Items.Count - 1)
                        {
                            _strCPTCodes += ")";
                        }
                    }

                    if (_strCPTCodes != "")
                        strSQL += " AND (BL_Transaction_Lines.sCPTCode IN " + _strCPTCodes + " ) ";
                }

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                DataTable dt = new DataTable();
                oDB.Retrive_Query(strSQL, out dt);
                oDB.Disconnect();

                DataColumn col = new DataColumn();
                col.ColumnName = "Select";
                col.DataType = Type.GetType("System.Boolean");
                col.DefaultValue = false;
                dt.Columns.Add(col);

                if (dt != null)
                {
                    c1ChkInPatients.DataSource = dt;
                    FormLoading = false;
                }

                c1ChkInPatients.Cols[0].Visible = false;
                c1ChkInPatients.Cols[1].Visible = true;
                c1ChkInPatients.Cols[1].Width = Convert.ToInt32(c1ChkInPatients.Width * 0.9); // Width of PatientName Column
                c1ChkInPatients.Cols[2].Width = Convert.ToInt32(c1ChkInPatients.Width * 0.07); // Width of Select Column


                c1ChkInPatients.AllowEditing = true;
                c1ChkInPatients.Cols.Move(2, 0);

                c1ChkInPatients.Cols[0].AllowEditing = true;
                c1ChkInPatients.Cols[1].AllowEditing = false;
                c1ChkInPatients.Cols[2].AllowEditing = false;

                btnSelectAllClearAll.Tag = "Select";
                btnSelectAllClearAll.Text = "Select All";
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
               
            }

        }

        private void dtp_FromDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {

                dtp_ToDate.Value = dtpChkInDate.Value;

                //if (cmbProvider.SelectedIndex != 0)
                //Refresh();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void dtp_ToDate_ValueChanged(object sender, EventArgs e)
        {
            //if (cmbProvider.SelectedIndex != 0)
            try
            {
                //Refresh();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void c1Patients_Click(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        #region "Added criterias Insurance,CPT,Diagnosis"

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

                    }
                    catch { }
                    oListControl.Dispose();
                    oListControl = null;
                }
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Providers, true, this.Width);
                oListControl.ClinicID = _clinicId;
                oListControl.ControlHeader = " Providers";
                //_CurrentControlType = gloListControl.gloListControlType.Providers;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                this.Controls.Add(oListControl);

                if (cmbChkInInsuranceCompany.DataSource != null)
                {
                    for (int i = 0; i < cmbChkInProvider.Items.Count; i++)
                    {
                        cmbChkInProvider.SelectedIndex = i;
                        cmbChkInProvider.Refresh();
                        oListControl.SelectedItems.Add(Convert.ToInt64(cmbChkInProvider.SelectedValue), cmbChkInProvider.Text);
                    }
                }
                oListControl.OpenControl();
                oListControl.Dock = DockStyle.Fill;
                oListControl.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnClearProvider_Click(object sender, EventArgs e)
        {
            try
            {
             // 
                cmbChkInProvider.DataSource = null;
                cmbChkInProvider.Items.Clear();
                cmbChkInProvider.Refresh();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
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

                    }
                    catch { }
                    oListControl.Dispose();
                    oListControl = null;
                }
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.AllPatientInsurances, true, this.Width);
                oListControl.ClinicID = _clinicId;
                oListControl.ControlHeader = " Insurances";
                //_CurrentControlType = gloListControl.gloListControlType.AllPatientInsurances;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                this.Controls.Add(oListControl);

                if (cmbChkInInsuranceCompany.DataSource != null)
                {
                    for (int i = 0; i < cmbChkInInsuranceCompany.Items.Count; i++)
                    {
                        cmbChkInInsuranceCompany.SelectedIndex = i;
                        cmbChkInInsuranceCompany.Refresh();
                        oListControl.SelectedItems.Add(Convert.ToInt64(cmbChkInInsuranceCompany.SelectedValue), cmbChkInInsuranceCompany.Text);
                    }
                }
                oListControl.OpenControl();
                oListControl.Dock = DockStyle.Fill;
                oListControl.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                //Refresh();
            }

        }

        private void btnClearInsurance_Click(object sender, EventArgs e)
        {
            try
            {
               
                cmbChkInInsuranceCompany.DataSource = null;
                cmbChkInInsuranceCompany.Items.Clear();
                cmbChkInInsuranceCompany.Refresh();
                //Refresh();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
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

                    }
                    catch { }
                    oListControl.Dispose();
                    oListControl = null;
                }
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.CPT, true, this.Width);
                oListControl.ClinicID = _clinicId;
                oListControl.ControlHeader = " CPT";
                //_CurrentControlType = gloListControl.gloListControlType.CPT;
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
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                //Refresh();
            }
        }

        private void btnClearCPT_Click(object sender, EventArgs e)
        {
            try
            {
               
                cmbCPT.DataSource = null;
                cmbCPT.Items.Clear();
                cmbCPT.Refresh();
                //Refresh();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
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

                    }
                    catch { }
                    oListControl.Dispose();
                    oListControl = null;
                }
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Diagnosis, true, this.Width);
                oListControl.ClinicID = _clinicId;
                oListControl.ControlHeader = " Diagnosis";

                //_CurrentControlType = gloListControl.gloListControlType.Diagnosis;
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
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                //Refresh();
            }

        }

        private void btnClearDiagnosisCode_Click(object sender, EventArgs e)
        {
            try
            {
                
                cmbDiagnosisCode.DataSource = null;
                cmbDiagnosisCode.Items.Clear();
                cmbDiagnosisCode.Refresh();
                //Refresh();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        #region "List Control Events "

        //void oListControl_ItemSelectedClick(object sender, EventArgs e)
        //{
        //    int _Counter = 0;
            
        //    try
        //    {
        //        switch (_CurrentControlType)
        //        {

        //            case gloListControl.gloListControlType.Patient:
        //                {

        //                    c1ChkInPatients.DataSource = null;


        //                    for (_Counter = 0; _Counter <= oListControl.SelectedItems.Count - 1; _Counter++)
        //                    {
        //                        DataRow oRow;
        //                        oRow = oBindTable.NewRow();
        //                        oRow[0] = oListControl.SelectedItems[_Counter].ID;
        //                        oRow[1] = oListControl.SelectedItems[_Counter].Description;
        //                        oBindTable.Rows.Add(oRow);
        //                    }

        //                    c1ChkInPatients.DataSource = oBindTable;
        //                    c1ChkInPatients.Cols[0].Visible = false;
        //                    c1ChkInPatients.Cols[1].Visible = true;
        //                    c1ChkInPatients.Cols[1].Width = c1ChkInPatients.Width - 10;

        //                    pnl_tlspTOP.Visible = true;
        //                }
        //                break;

        //            case gloListControl.gloListControlType.AllPatientInsurances:
        //                {
        //                    cmbChkInInsuranceCompany.DataSource = null;
        //                    if (oListControl.SelectedItems.Count > 0)
        //                    {
        //                        DataTable oBindTable = new DataTable();

        //                        oBindTable.Columns.Add("ID");
        //                        oBindTable.Columns.Add("DispName");

        //                        for (_Counter = 0; _Counter <= oListControl.SelectedItems.Count - 1; _Counter++)
        //                        {
        //                            DataRow oRow;
        //                            oRow = oBindTable.NewRow();
        //                            oRow[0] = oListControl.SelectedItems[_Counter].ID;
        //                            oRow[1] = oListControl.SelectedItems[_Counter].Description;
        //                            oBindTable.Rows.Add(oRow);
        //                        }

        //                        cmbChkInInsuranceCompany.DataSource = oBindTable;
        //                        cmbChkInInsuranceCompany.DisplayMember = "DispName";
        //                        cmbChkInInsuranceCompany.ValueMember = "ID";
        //                    }


        //                }
        //                break;
        //            case gloListControl.gloListControlType.Diagnosis:
        //                {
        //                    cmbDiagnosisCode.DataSource = null;
        //                    if (oListControl.SelectedItems.Count > 0)
        //                    {
        //                        DataTable oBindTable = new DataTable();

        //                        oBindTable.Columns.Add("ID");
        //                        oBindTable.Columns.Add("DispName");

        //                        for (_Counter = 0; _Counter <= oListControl.SelectedItems.Count - 1; _Counter++)
        //                        {
        //                            DataRow oRow;
        //                            oRow = oBindTable.NewRow();
        //                            oRow[0] = oListControl.SelectedItems[_Counter].ID;
        //                            oRow[1] = oListControl.SelectedItems[_Counter].Code;
        //                            oBindTable.Rows.Add(oRow);
        //                        }

        //                        cmbDiagnosisCode.DataSource = oBindTable;
        //                        cmbDiagnosisCode.DisplayMember = "DispName";
        //                        cmbDiagnosisCode.ValueMember = "ID";
        //                    }


        //                }
        //                break;
        //            case gloListControl.gloListControlType.CPT:
        //                {
        //                    try
        //                    {
        //                        cmbCPT.DataSource = null;
        //                        if (oListControl.SelectedItems.Count > 0)
        //                        {
        //                            DataTable oBindTable = new DataTable();

        //                            oBindTable.Columns.Add("ID");
        //                            oBindTable.Columns.Add("DispName");

        //                            for (_Counter = 0; _Counter <= oListControl.SelectedItems.Count - 1; _Counter++)
        //                            {
        //                                DataRow oRow;
        //                                oRow = oBindTable.NewRow();
        //                                oRow[0] = oListControl.SelectedItems[_Counter].ID;
        //                                oRow[1] = oListControl.SelectedItems[_Counter].Code;
        //                                oBindTable.Rows.Add(oRow);
        //                            }

        //                            cmbCPT.DataSource = oBindTable;
        //                            cmbCPT.DisplayMember = "DispName";
        //                            cmbCPT.ValueMember = "ID";

        //                        }
        //                    }
        //                    catch (Exception ee)
        //                    {
        //                        ee.ToString();
        //                        ee = null;
        //                    }


        //                }
        //                break;
        //            case gloListControl.gloListControlType.Providers:
        //                {
        //                    cmbChkInProvider.DataSource = null;
        //                    if (oListControl.SelectedItems.Count > 0)
        //                    {
        //                        DataTable oBindTable = new DataTable();

        //                        oBindTable.Columns.Add("ID");
        //                        oBindTable.Columns.Add("DispName");

        //                        for (_Counter = 0; _Counter <= oListControl.SelectedItems.Count - 1; _Counter++)
        //                        {
        //                            DataRow oRow;
        //                            oRow = oBindTable.NewRow();
        //                            oRow[0] = oListControl.SelectedItems[_Counter].ID;
        //                            oRow[1] = oListControl.SelectedItems[_Counter].Description;
        //                            oBindTable.Rows.Add(oRow);
        //                        }

        //                        cmbChkInProvider.DataSource = oBindTable;
        //                        cmbChkInProvider.DisplayMember = "DispName";
        //                        cmbChkInProvider.ValueMember = "ID";
        //                    }
        //                }
        //                break;
        //            default:
        //                {
        //                }
        //                break;
        //        }
        //        Refresh();
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
        //    }
        //}

        //void oListControl_ItemClosedClick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (oListControl != null)
        //        {
        //            for (int i = this.Controls.Count - 1; i >= 0; i--)
        //            {
        //                if (this.Controls[i].Name == oListControl.Name)
        //                {
        //                    this.Controls.Remove(this.Controls[i]);
        //                    pnl_tlspTOP.Visible = true;
        //                    break;
        //                }
        //            }
        //        }
        //        Refresh();
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
        //    }
        //}
        
        #endregion

        #endregion


        #region "Filter by Date "

        //private void Fill_FilterDatesCombo()
        //{
        //    try
        //    {
        //        cmb_datefilter.Items.Clear();
        //        cmb_datefilter.Items.Add("Custom");
        //        cmb_datefilter.Items.Add("Today");
        //        cmb_datefilter.Items.Add("Tomorrow");
        //        cmb_datefilter.Items.Add("Yesterday");
        //        cmb_datefilter.Items.Add("This Week");
        //        cmb_datefilter.Items.Add("Last Week");
        //        cmb_datefilter.Items.Add("Current Month");
        //        cmb_datefilter.Items.Add("Last Month");
        //        cmb_datefilter.Items.Add("Current Year");
        //        cmb_datefilter.Items.Add("Last 30 Days");
        //        cmb_datefilter.Items.Add("Last 60 Days");
        //        cmb_datefilter.Items.Add("Last 90 Days");
        //        cmb_datefilter.Items.Add("Last 120 Days");
        //        cmb_datefilter.Refresh();

        //    }
        //    catch (Exception ex)
        //    {
        //        ex.ToString();
        //        ex = null;
        //        //gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

        //    }
        //}

        private void cmb_datefilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int _filterby = 0;
            //try
            //{
            //    _filterby = cmb_datefilter.SelectedIndex;
            //    switch (_filterby)
            //    {
            //        case 0://Date Range
            //            FilterBy_DateRange();
            //            break;

            //        case 1://Today
            //            FilterBy_Today();
            //            break;

            //        case 2://Tomorrow
            //            FilterBy_Tomorrow();
            //            break;

            //        case 3://Yesterday
            //            FilterBy_Yesterday();
            //            break;

            //        case 4://This week
            //            FilterBy_Thisweek();
            //            break;

            //        case 5://Last Week
            //            FilterBy_lastweek();
            //            break;

            //        case 6://Current Month
            //            FilterBy_currentmonth();
            //            break;

            //        case 7://Last Month
            //            FilterBy_lastmonth();
            //            break;

            //        case 8://Current Year
            //            FilterBy_currenYear();
            //            break;

            //        case 9://Last 30 days
            //            FilterBy_last30days();
            //            break;

            //        case 10://Last 60 days
            //            FilterBy_last60days();
            //            break;

            //        case 11://Last 90 days
            //            FilterBy_last90days();
            //            break;

            //        case 12://Last 120 days
            //            FilterBy_last120days();
            //            break;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            //}

        }

        //#region " Methods "
        //private void FilterBy_Today()
        //{
        //    try
        //    {
        //        dtpChkInDate.Value = DateTime.Today;
        //        dtp_ToDate.Value = DateTime.Today;

        //        dtpChkInDate.Enabled = false;
        //        dtp_ToDate.Enabled = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
        //    }

        //}

        //private void FilterBy_Tomorrow()
        //{
        //    try
        //    {

        //        dtpChkInDate.Value = DateTime.Now.AddDays(1);
        //        dtp_ToDate.Value = DateTime.Now.AddDays(1);

        //        dtpChkInDate.Enabled = false;
        //        dtp_ToDate.Enabled = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
        //    }
        //}

        //private void FilterBy_Yesterday()
        //{
        //    try
        //    {
        //        dtpChkInDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(24, 0, 0));
        //        dtp_ToDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(24, 0, 0));

        //        dtpChkInDate.Enabled = false;
        //        dtp_ToDate.Enabled = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
        //    }
        //}

        //private void FilterBy_Thisweek()
        //{
        //    try
        //    {
        //        if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
        //        {
        //            dtpChkInDate.Value = DateTime.Today;
        //            dtp_ToDate.Value = DateTime.Now.Date.AddDays(6);

        //        }
        //        if (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday)
        //        {
        //            dtpChkInDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(1, 0, 0, 0));
        //            dtp_ToDate.Value = dtpChkInDate.Value.AddDays(6);
        //        }
        //        if (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday)
        //        {
        //            dtpChkInDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(2, 0, 0, 0));
        //            dtp_ToDate.Value = dtpChkInDate.Value.AddDays(6);
        //        }
        //        if (DateTime.Now.DayOfWeek == DayOfWeek.Thursday)
        //        {
        //            dtpChkInDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(3, 0, 0, 0));
        //            dtp_ToDate.Value = dtpChkInDate.Value.AddDays(6);

        //        }
        //        if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
        //        {
        //            dtpChkInDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(4, 0, 0, 0));
        //            dtp_ToDate.Value = dtpChkInDate.Value.AddDays(6);
        //        }

        //        if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
        //        {
        //            dtpChkInDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(5, 0, 0, 0));
        //            dtp_ToDate.Value = dtpChkInDate.Value.AddDays(6);
        //        }

        //        if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
        //        {
        //            dtpChkInDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(6, 0, 0, 0));
        //            dtp_ToDate.Value = dtpChkInDate.Value.AddDays(6);
        //        }

        //        dtpChkInDate.Enabled = false;
        //        dtp_ToDate.Enabled = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
        //    }
        //}

        //private void FilterBy_lastweek()
        //{
        //    try
        //    {
        //        if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
        //        {
        //            dtpChkInDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(7, 0, 0, 0));
        //            dtp_ToDate.Value = dtpChkInDate.Value.AddDays(6);

        //        }
        //        if (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday)
        //        {
        //            dtpChkInDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(8, 0, 0, 0));
        //            dtp_ToDate.Value = dtpChkInDate.Value.AddDays(6);
        //        }
        //        if (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday)
        //        {
        //            dtpChkInDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(9, 0, 0, 0));
        //            dtp_ToDate.Value = dtpChkInDate.Value.AddDays(6);
        //        }
        //        if (DateTime.Now.DayOfWeek == DayOfWeek.Thursday)
        //        {
        //            dtpChkInDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(10, 0, 0, 0));
        //            dtp_ToDate.Value = dtpChkInDate.Value.AddDays(6);

        //        }
        //        if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
        //        {
        //            dtpChkInDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(11, 0, 0, 0));
        //            dtp_ToDate.Value = dtpChkInDate.Value.AddDays(6);
        //        }

        //        if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
        //        {
        //            dtpChkInDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(12, 0, 0, 0));
        //            dtp_ToDate.Value = dtpChkInDate.Value.AddDays(6);
        //        }

        //        if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
        //        {
        //            dtpChkInDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(13, 0, 0, 0));
        //            dtp_ToDate.Value = dtpChkInDate.Value.AddDays(6);
        //        }

        //        dtpChkInDate.Enabled = false;
        //        dtp_ToDate.Enabled = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
        //    }
        //}

        //private void FilterBy_currentmonth()
        //{
        //    try
        //    {
        //        DateTime dtFrom = new DateTime(dtpChkInDate.Value.Year, dtpChkInDate.Value.Month, 1);

        //        // for any date passed in to the method
        //        // create a datetime variable set to the passed in date
        //        DateTime dtTo = new DateTime(DateTime.Now.Year, dtpChkInDate.Value.Month, 1);
        //        // overshoot the date by a month

        //        dtTo = dtTo.AddMonths(1);
        //        // remove all of the days in the next month
        //        // to get bumped down to the last day of the 
        //        // previous month
        //        dtTo = dtTo.AddDays(-(dtTo.Day));
        //        dtpChkInDate.Value = Convert.ToDateTime(dtFrom.Date);
        //        dtp_ToDate.Value = Convert.ToDateTime(dtTo.Date);

        //        dtpChkInDate.Enabled = false;
        //        dtp_ToDate.Enabled = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
        //    }

        //}

        //private void FilterBy_lastmonth()
        //{
        //    try
        //    {
        //        DateTime firstDay = new DateTime(DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month, 1);

        //        int DaysinMonth = DateTime.DaysInMonth(DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month);

        //        DateTime lastDay = firstDay.AddMonths(1).AddTicks(-1);

        //        dtpChkInDate.Value = Convert.ToDateTime(firstDay.Date);
        //        dtp_ToDate.Value = Convert.ToDateTime(lastDay.Date);

        //        dtpChkInDate.Enabled = false;
        //        dtp_ToDate.Enabled = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
        //    }
        //}

        //private void FilterBy_currenYear()
        //{
        //    try
        //    {
        //        DateTime dtFrom = new DateTime(DateTime.Now.Year, 1, 1);

        //        dtpChkInDate.Value = Convert.ToDateTime(dtFrom.Date);
        //        dtp_ToDate.Value = DateTime.Today;

        //        dtpChkInDate.Enabled = false;
        //        dtp_ToDate.Enabled = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
        //    }
        //}

        //private void FilterBy_last30days()
        //{
        //    try
        //    {
        //        dtpChkInDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(30, 0, 0, 0));
        //        dtp_ToDate.Value = DateTime.Today;

        //        dtpChkInDate.Enabled = false;
        //        dtp_ToDate.Enabled = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
        //    }
        //}

        //private void FilterBy_last60days()
        //{
        //    try
        //    {
        //        dtpChkInDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(60, 0, 0, 0));
        //        dtp_ToDate.Value = DateTime.Today;

        //        dtpChkInDate.Enabled = false;
        //        dtp_ToDate.Enabled = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
        //    }

        //}

        //private void FilterBy_last90days()
        //{
        //    try
        //    {
        //        dtpChkInDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(90, 0, 0, 0));
        //        dtp_ToDate.Value = DateTime.Today;

        //        dtpChkInDate.Enabled = false;
        //        dtp_ToDate.Enabled = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
        //    }
        //}

        //private void FilterBy_last120days()
        //{
        //    try
        //    {
        //        dtpChkInDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(120, 0, 0, 0));
        //        dtp_ToDate.Value = DateTime.Today;

        //        dtpChkInDate.Enabled = false;
        //        dtp_ToDate.Enabled = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
        //    }
        //}

        //private void FilterBy_DateRange()
        //{
        //    try
        //    {
        //        dtpChkInDate.Value = DateTime.Today;
        //        dtp_ToDate.Value = DateTime.Today;

        //        dtpChkInDate.Enabled = true;
        //        dtp_ToDate.Enabled = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
        //    }
        //}

        //#endregion

        //private void btnSelectAllClearAll_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (btnSelectAllClearAll.Tag.ToString() == "Select")
        //        {
        //            btnSelectAllClearAll.Tag = "Clear";
        //            btnSelectAllClearAll.Text = "Clear All";
        //            SelectAllClearAll(true);
        //        }
        //        else
        //        {
        //            btnSelectAllClearAll.Tag = "Select";
        //            btnSelectAllClearAll.Text = "Select All";
        //            SelectAllClearAll(false);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
        //    }
        //}

        //private void SelectAllClearAll(bool select)
        //{
        //    try
        //    {
        //        for (int i = 1; i < c1ChkInPatients.Rows.Count; i++)
        //        {
        //            if (select == true)
        //            { c1ChkInPatients.SetCellCheck(i, 0, C1.Win.C1FlexGrid.CheckEnum.Checked); }
        //            else
        //            {
        //                c1ChkInPatients.SetCellCheck(i, 0, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
        //    }
        //}

        #endregion "Filter by Date "

        private void c1Patients_AfterEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            // Sudhir - 20090122  TO MAINTAIN STATE OF BUTTON [ Select All / Clear All ] //
            try
            {
                if (FormLoading == false)
                {
                    bool _UncheckFound = false;
                    for (int i = 1; i < c1ChkInPatients.Rows.Count; i++)
                    {
                        if (c1ChkInPatients.GetCellCheck(i, 0) == C1.Win.C1FlexGrid.CheckEnum.Unchecked)
                        {
                            _UncheckFound = true;
                            break;
                        }
                    }
                    if (_UncheckFound == true)
                    {
                        btnSelectAllClearAll.Tag = "Select";
                        btnSelectAllClearAll.Text = "Select All";
                    }
                    else
                    {
                        btnSelectAllClearAll.Tag = "Clear";
                        btnSelectAllClearAll.Text = "Clear All";
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void trvTemplates_AfterCheck(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (e.Node.Level == 0)
                {
                    foreach (TreeNode oNode in e.Node.Nodes)
                    {
                        oNode.Checked = e.Node.Checked;   
                    }
                }
                else if (e.Node.Level == 1)
                {
                    if (e.Node.Checked == false)
                    {
                        this.trvTemplates.AfterCheck -= new System.Windows.Forms.TreeViewEventHandler(this.trvTemplates_AfterCheck);
                        e.Node.Parent.Checked = false;
                        this.trvTemplates.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trvTemplates_AfterCheck);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        //private void btnSelectAllProvider_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (btnChkInSelectAll.Text.Trim() == "Select All")
        //        {
        //            foreach (TreeNode oNode in trvProvider.Nodes)
        //            {
        //                oNode.Checked = true;
        //            }
        //            btnChkInSelectAll.Text = "Clear All";
        //        }
        //        else if (btnChkInSelectAll.Text.Trim() == "Clear All")
        //        {
        //            foreach (TreeNode oNode in trvProvider.Nodes)
        //            {
        //                oNode.Checked = false;
        //            }
        //            btnChkInSelectAll.Text = "Select All";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
        //    }
        //}

        //private void btnSelectAllProvider_MouseHover(object sender, EventArgs e)
        //{

        //}

        //private void btnSelectAllProvider_MouseLeave(object sender, EventArgs e)
        //{

        //}

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void c1Patients_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

         #endregion

        #region "Changed By Shweta 20100928"

        #region "Browse & Clear Combobox Event"

        private void btn_Browse_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null)
                return;
              
            gloListControl.gloListControlType _ControlType = gloListControl.gloListControlType.Other;
            string _ControlHeader = "";
            gloGeneralItem.gloItems oSelectedItems = new gloGeneralItem.gloItems();
            bool _isMultiSelect = false;

            
            try
            {
                                switch (btn.Name)
                {
                    case  "btnChkInBrowseLocation" :
                        _SelectedFilterCombo = "btnChkInBrowseLocation";
                        _ControlType = gloListControl.gloListControlType.Location;
                        _ControlHeader = "Location";
                        _isMultiSelect = true;

                        if (cmbChkInLocation.DataSource != null)
                        {
                            for (int i = 0; i < cmbChkInLocation.Items.Count; i++)
                            {
                                cmbChkInLocation.SelectedIndex = i;
                                cmbChkInLocation.Refresh();
                                oSelectedItems.Add(Convert.ToInt64(cmbChkInLocation.SelectedValue), cmbChkInLocation.Text);
                            }
                        }
                        break;
                    case "btnChkInBrowseProvider":
                        _SelectedFilterCombo = "btnChkInBrowseProvider";
                        _ControlType = gloListControl.gloListControlType.Providers;
                        _ControlHeader = "Provider";
                        _isMultiSelect = true;

                        if (cmbChkInProvider.DataSource != null)
                        {
                            for (int i = 0; i < cmbChkInProvider.Items.Count; i++)
                            {
                                cmbChkInProvider.SelectedIndex = i;
                                cmbChkInProvider.Refresh();
                                oSelectedItems.Add(Convert.ToInt64(cmbChkInProvider.SelectedValue), cmbChkInProvider.Text);
                            }
                        }
                        break;
                    case "btnChkInBrowseResource":
                        _SelectedFilterCombo = "btnChkInBrowseResource";
                        _ControlType = gloListControl.gloListControlType.Resources;
                        _ControlHeader = "Resource";
                        _isMultiSelect = true;

                        if (cmbChkInReosurce.DataSource != null)
                        {
                            for (int i = 0; i < cmbChkInReosurce.Items.Count; i++)
                            {
                                cmbChkInReosurce.SelectedIndex = i;
                                cmbChkInReosurce.Refresh();
                                oSelectedItems.Add(Convert.ToInt64(cmbChkInReosurce.SelectedValue), cmbChkInReosurce.Text);
                            }
                        }
                        break;
                    case "btnChkInBrowseApptType":
                        _SelectedFilterCombo = "btnChkInBrowseApptType";
                        _ControlType = gloListControl.gloListControlType.AppointmentType;
                        _ControlHeader = "Appointment Type";
                        _isMultiSelect = true;

                        if (cmbChkInApptType.DataSource != null)
                        {
                            for (int i = 0; i < cmbChkInApptType.Items.Count; i++)
                            {
                                cmbChkInApptType.SelectedIndex = i;
                                cmbChkInApptType.Refresh();
                                oSelectedItems.Add(Convert.ToInt64(cmbChkInApptType.SelectedValue), cmbChkInApptType.Text);
                            }
                        }
                        break;
                    case "btnChkInBrowseApptTypeType":
                        _SelectedFilterCombo = "btnChkInBrowseApptTypeType";
                        _ControlType = gloListControl.gloListControlType.AppointmentTypeType;
                        _ControlHeader = "Appointment Type";
                        _isMultiSelect = true;

                        if (cmbChkInApptTypeType .DataSource != null)
                        {
                            for (int i = 0; i < cmbChkInApptTypeType.Items.Count; i++)
                            {
                                cmbChkInApptTypeType.SelectedIndex = i;
                                cmbChkInApptTypeType.Refresh();
                                oSelectedItems.Add(Convert.ToInt64(cmbChkInApptTypeType.SelectedValue), cmbChkInApptTypeType.Text);
                            }
                        }
                        break;
                    case "btnChkInBrowsePatient":
                        _SelectedFilterCombo = "btnChkInBrowsePatient";
                        _ControlType = gloListControl.gloListControlType.Patient;
                        _ControlHeader = "Patient";
                        _isMultiSelect = false;

                        if (cmbChkInPatient.DataSource != null)
                        {
                            for (int i = 0; i < cmbChkInPatient.Items.Count; i++)
                            {
                                cmbChkInPatient.SelectedIndex = i;
                                cmbChkInPatient.Refresh();
                                oSelectedItems.Add(Convert.ToInt64(cmbChkInPatient.SelectedValue), cmbChkInPatient.Text);
                            }
                        }
                        break;
                    case "btnChkInBrowseInsurance":
                        _SelectedFilterCombo = "btnChkInBrowseInsurance";

                        if (rbtnChkInCompany.Checked)
                        {
                            _ControlType = gloListControl.gloListControlType.InsuranceCompany;
                            _ControlHeader = "Insurance Company";
                            _isMultiSelect = true;
                           
                            if (cmbChkInInsuranceCompany.DataSource != null)
                            {
                                for (int i = 0; i < cmbChkInInsuranceCompany.Items.Count; i++)
                                {
                                    cmbChkInInsuranceCompany.SelectedIndex = i;
                                    cmbChkInInsuranceCompany.Refresh();
                                    oSelectedItems.Add(Convert.ToInt64(cmbChkInInsuranceCompany.SelectedValue), cmbChkInInsuranceCompany.Text);
                                }
                            }
                        }
                        else if (rbtnChkInPlan.Checked)
                        {
                            _ControlType = gloListControl.gloListControlType.Insurance;
                            _ControlHeader = "Insurance";
                            _isMultiSelect = true;

                            if (cmbChkInInsurancePlan.DataSource != null)
                            {
                                for (int i = 0; i < cmbChkInInsurancePlan.Items.Count; i++)
                                {
                                    cmbChkInInsurancePlan.SelectedIndex = i;
                                    cmbChkInInsurancePlan.Refresh();
                                    oSelectedItems.Add(Convert.ToInt64(cmbChkInInsurancePlan.SelectedValue), cmbChkInInsurancePlan.Text);
                                }
                            }
                        }
                        break;


                    case "btnApptLetterBrowseLocation":
                        _SelectedFilterCombo = "btnApptLetterBrowseLocation";

                        _ControlType = gloListControl.gloListControlType.Location;
                        _ControlHeader = "Location";
                        _isMultiSelect = true;

                        if (cmbApptLetterLocation.DataSource != null)
                        {
                            for (int i = 0; i < cmbApptLetterLocation.Items.Count; i++)
                            {
                                cmbApptLetterLocation.SelectedIndex = i;
                                cmbApptLetterLocation.Refresh();
                                oSelectedItems.Add(Convert.ToInt64(cmbApptLetterLocation.SelectedValue), cmbApptLetterLocation.Text);
                            }
                        }
                        break;
                    case "btnApptLetterBrowseProvider":
                        _SelectedFilterCombo = "btnApptLetterBrowseProvider";
                        _ControlType = gloListControl.gloListControlType.Providers;
                        _ControlHeader = "Provider";
                        _isMultiSelect = true;

                        if (cmbApptLetterProvider.DataSource != null)
                        {
                            for (int i = 0; i < cmbApptLetterProvider.Items.Count; i++)
                            {
                                cmbApptLetterProvider.SelectedIndex = i;
                                cmbApptLetterProvider.Refresh();
                                oSelectedItems.Add(Convert.ToInt64(cmbApptLetterProvider.SelectedValue), cmbApptLetterProvider.Text);
                            }
                        }
                        break;
                    case "btnApptLetterBrowseResource":
                        _SelectedFilterCombo = "btnApptLetterBrowseResource";
                        _ControlType = gloListControl.gloListControlType.Resources;
                        _ControlHeader = "Resource";
                        _isMultiSelect = true;

                        if (cmbApptLetterResource.DataSource != null)
                        {
                            for (int i = 0; i < cmbApptLetterResource.Items.Count; i++)
                            {
                                cmbApptLetterResource.SelectedIndex = i;
                                cmbApptLetterResource.Refresh();
                                oSelectedItems.Add(Convert.ToInt64(cmbApptLetterResource.SelectedValue), cmbApptLetterResource.Text);
                            }
                        }
                        break;
                    case "btnApptLetterBrowseApptType":
                        _SelectedFilterCombo = "btnApptLetterBrowseApptType";
                        _ControlType = gloListControl.gloListControlType.AppointmentType;
                        _ControlHeader = "Appointment Type";
                        _isMultiSelect = true;

                        if (cmbApptLetterApptType.DataSource != null)
                        {
                            for (int i = 0; i < cmbApptLetterApptType.Items.Count; i++)
                            {
                                cmbApptLetterApptType.SelectedIndex = i;
                                cmbApptLetterApptType.Refresh();
                                oSelectedItems.Add(Convert.ToInt64(cmbApptLetterApptType.SelectedValue), cmbApptLetterApptType.Text);
                            }
                        }
                        break;
                    case "btnApptLetterBrowseApptTypeType":
                        _SelectedFilterCombo = "btnApptLetterBrowseApptTypeType";
                        _ControlType = gloListControl.gloListControlType.AppointmentTypeType;
                        _ControlHeader = "Appointment Type";
                        _isMultiSelect = true;

                        if (cmbApptLetterApptTypeType.DataSource != null)
                        {
                            for (int i = 0; i < cmbApptLetterApptTypeType.Items.Count; i++)
                            {
                                cmbApptLetterApptTypeType.SelectedIndex = i;
                                cmbApptLetterApptTypeType.Refresh();
                                oSelectedItems.Add(Convert.ToInt64(cmbApptLetterApptTypeType.SelectedValue), cmbApptLetterApptTypeType.Text);
                            }
                        }
                        break;
                    case "btnApptLetterBrowsePatient":
                        _SelectedFilterCombo = "btnApptLetterBrowsePatient";
                        _ControlType = gloListControl.gloListControlType.Patient;
                        _ControlHeader = "Patient";
                        _isMultiSelect = false;

                        if (cmbApptLetterPatient.DataSource != null)
                        {
                            for (int i = 0; i < cmbApptLetterPatient.Items.Count; i++)
                            {
                                cmbApptLetterPatient.SelectedIndex = i;
                                cmbApptLetterPatient.Refresh();
                                oSelectedItems.Add(Convert.ToInt64(cmbApptLetterPatient.SelectedValue), cmbApptLetterPatient.Text);
                            }
                        }

                        break;
                    case "btnApptLetterBrowseInsurance":
                        _SelectedFilterCombo = "btnApptLetterBrowseInsurance";

                        if (rbtnApptLetterCompany.Checked)
                        {
                            _ControlType = gloListControl.gloListControlType.InsuranceCompany;
                            _ControlHeader = "Insurance Company";
                            _isMultiSelect = true;

                            if (cmbApptLetterInsuranceCompany.DataSource != null)
                            {
                                for (int i = 0; i < cmbApptLetterInsuranceCompany.Items.Count; i++)
                                {
                                    cmbApptLetterInsuranceCompany.SelectedIndex = i;
                                    cmbApptLetterInsuranceCompany.Refresh();
                                    oSelectedItems.Add(Convert.ToInt64(cmbApptLetterInsuranceCompany.SelectedValue), cmbApptLetterInsuranceCompany.Text);
                                }
                            }
                        }
                        else if (rbtnApptLetterPlan.Checked)
                        {
                            _ControlType = gloListControl.gloListControlType.Insurance;
                            _ControlHeader = "Insurance";
                            _isMultiSelect = true;

                            if (cmbApptLetterInsurancePlan.DataSource != null)
                            {
                                for (int i = 0; i < cmbApptLetterInsurancePlan.Items.Count; i++)
                                {
                                    cmbApptLetterInsurancePlan.SelectedIndex = i;
                                    cmbApptLetterInsurancePlan.Refresh();
                                    oSelectedItems.Add(Convert.ToInt64(cmbApptLetterInsurancePlan.SelectedValue), cmbApptLetterInsurancePlan.Text);
                                }
                            }
                        }
                        break;                                                
                        
                                    default:
                        _SelectedFilterCombo = "";
                        _ControlType = gloListControl.gloListControlType.Other;
                        _ControlHeader = "";
                        break;
                }

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

                                    }
                                    catch { }
                                    oListControl.Dispose();
                                    oListControl = null;
                                }
                                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, _ControlType, _isMultiSelect, this.Width);
                                oListControl.ClinicID = _clinicId;
                                oListControl.ControlHeader = _ControlHeader;
                                //_CurrentControlType = gloListControl.gloListControlType.Providers;
                                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                                this.Controls.Add(oListControl);

                                oListControl.SelectedItems = oSelectedItems; //.Add(Convert.ToInt64(cmbChkInProvider.SelectedValue), cmbChkInProvider.Text);
                                  
                                oListControl.OpenControl();
                                oListControl.Dock = DockStyle.Fill;
                                oListControl.BringToFront();



            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);    
                _SelectedFilterCombo = "";
            }
            
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null)
                return;
            try
            {
                switch (btn.Name)
                {
                    case "btnChkInClearLocation":
                       
                        cmbChkInLocation.DataSource = null;
                        cmbChkInLocation.Items.Clear();
                        cmbApptLetterLocation.Refresh();  
                             
                        break;
                    case "btnChkInClearProvider":
                       
                        cmbChkInProvider.DataSource = null;
                        cmbChkInProvider.Items.Clear();
                        cmbChkInProvider.Refresh();
                            
                        break;
                    case "btnChkInClearResource":
                        
                        cmbChkInReosurce.DataSource = null;
                        cmbChkInReosurce.Items.Clear();
                        cmbChkInReosurce.Refresh();

                        break;
                    case "btnChkInClearApptType":
                        
                        cmbChkInApptType.DataSource = null;
                        cmbChkInApptType.Items.Clear();
                        cmbChkInApptType.Refresh();

                        break;
                    case "btnChkInClearApptTypeType":
                        
                        cmbChkInApptTypeType.DataSource = null;
                        cmbChkInApptTypeType.Items.Clear();
                        cmbChkInApptTypeType.Refresh();

                        break;
                    case "btnChkInClearPatient":
                        
                        cmbChkInPatient.DataSource = null;
                        cmbChkInPatient.Items.Clear();
                        cmbChkInPatient.Refresh();
                        
                        break;
                    case "btnChkInClearInsurance":
                        if (rbtnChkInCompany.Checked)
                        {
                            
                            cmbChkInInsuranceCompany.DataSource = null;
                            cmbChkInInsuranceCompany.Items.Clear();
                            cmbChkInInsuranceCompany.Refresh();
                         }
                        else if (rbtnChkInPlan.Checked)
                        {
                           
                            cmbChkInInsurancePlan.DataSource = null;
                            cmbChkInInsurancePlan.Items.Clear();
                            cmbChkInInsurancePlan.Refresh();
                         
                        }
                        break;

                    case "btnApptLetterClearLocation":
                        
                        cmbApptLetterLocation.DataSource = null;
                        cmbApptLetterLocation.Items.Clear();
                        cmbApptLetterLocation.Refresh();
                                
                        break;
                    case "btnApptLetterClearProvider":
                       
                        cmbApptLetterProvider.DataSource = null;
                        cmbApptLetterProvider.Items.Clear();
                        cmbApptLetterProvider.Refresh();

                        break;
                    case "btnApptLetterClearResource":
                     
                        cmbApptLetterResource.DataSource = null;
                        cmbApptLetterResource.Items.Clear();
                        cmbApptLetterResource.Refresh();
                        
                        break;
                    case "btnApptLetterClearApptType":
                       
                        cmbApptLetterApptType.DataSource = null;
                        cmbApptLetterApptType.Items.Clear();
                        cmbApptLetterApptType.Refresh();

                        break;
                    case "btnApptLetterClearApptTypeType":
                      
                        cmbApptLetterApptTypeType.DataSource=null;
                        cmbApptLetterApptTypeType.Items.Clear();
                        cmbApptLetterApptTypeType.Refresh();

                        break;
                    case "btnApptLetterClearPatient":
                       
                        cmbApptLetterPatient.DataSource = null;
                        cmbApptLetterPatient.Items.Clear();
                        cmbApptLetterPatient.Refresh();
                        
                        break;
                    case "btnApptLetterClearInsurance":
                        if (rbtnApptLetterCompany.Checked)
                        {
                          
                            cmbApptLetterInsuranceCompany.DataSource = null;
                            cmbApptLetterInsuranceCompany.Items.Clear();
                            cmbApptLetterInsuranceCompany.Refresh();
                        }
                        else if (rbtnApptLetterPlan.Checked)
                        {
                           
                            cmbApptLetterInsurancePlan.DataSource=null;
                            cmbApptLetterInsurancePlan.Items.Clear();
                            cmbApptLetterInsurancePlan.Refresh();
                        }
                        break;
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);   
             
            }
        }

        #endregion

        #region "List Control Events "

        void oListControl_ItemSelectedClick(object sender, EventArgs e)
        {
            int _Counter = 0;
            try
            {
                switch (_SelectedFilterCombo)
                {
                    case "btnChkInBrowseLocation":
                        
                        cmbChkInLocation.DataSource = null;
                        cmbChkInLocation.Items.Clear();
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

                            cmbChkInLocation.DataSource = oBindTable;
                            cmbChkInLocation.DisplayMember = "DispName";
                            cmbChkInLocation.ValueMember = "ID";
                        }
                        break;
                    case "btnChkInBrowseProvider":
                       
                        cmbChkInProvider.DataSource = null;
                        cmbChkInProvider.Items.Clear();
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
                            cmbChkInProvider.DataSource = oBindTable;
                            cmbChkInProvider.DisplayMember = "DispName";
                            cmbChkInProvider.ValueMember = "ID";
                        }
                        break;
                    case "btnChkInBrowseResource":
                      
                        cmbChkInReosurce.DataSource = null;
                        cmbChkInReosurce.Items.Clear();
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
                            cmbChkInReosurce.DataSource = oBindTable;
                            cmbChkInReosurce.DisplayMember = "DispName";
                            cmbChkInReosurce.ValueMember = "ID";
                        }
                        break;
                    case "btnChkInBrowseApptType":
                       
                        cmbChkInApptType.DataSource = null;
                        cmbChkInApptType.Items.Clear();
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
                            cmbChkInApptType.DataSource = oBindTable;
                            cmbChkInApptType.DisplayMember = "DispName";
                            cmbChkInApptType.ValueMember = "ID";
                        }
                        break;
                    case "btnChkInBrowseApptTypeType":
                        
                        cmbChkInApptTypeType.DataSource = null;
                        cmbChkInApptTypeType.Items.Clear();
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
                            cmbChkInApptTypeType.DataSource = oBindTable;
                            cmbChkInApptTypeType.DisplayMember = "DispName";
                            cmbChkInApptTypeType.ValueMember = "ID";
                        }
                        break;
                    case "btnChkInBrowsePatient":
                       
                        cmbChkInPatient.DataSource = null;
                        cmbChkInPatient.Items.Clear();
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
                            cmbChkInPatient.DataSource = oBindTable;
                            cmbChkInPatient.DisplayMember = "DispName";
                            cmbChkInPatient.ValueMember = "ID";
                        }
                        break;
                    case "btnChkInBrowseInsurance":
                        if (rbtnChkInCompany.Checked)
                        {
                           
                            cmbChkInInsuranceCompany.DataSource = null;
                            cmbChkInInsuranceCompany.Items.Clear();
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
                                cmbChkInInsuranceCompany.DataSource = oBindTable;
                                cmbChkInInsuranceCompany.DisplayMember = "DispName";
                                cmbChkInInsuranceCompany.ValueMember = "ID";
                            }
                        }
                        else if (rbtnChkInPlan.Checked)
                        {
                           
                            cmbChkInInsurancePlan.DataSource = null;
                            cmbChkInInsurancePlan.Items.Clear();
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
                                cmbChkInInsurancePlan.DataSource = oBindTable;
                                cmbChkInInsurancePlan.DisplayMember = "DispName";
                                cmbChkInInsurancePlan.ValueMember = "ID";
                            }
                        }
                        break;


                    case "btnApptLetterBrowseLocation":
                       
                        cmbApptLetterLocation.DataSource = null;
                        cmbApptLetterLocation.Items.Clear();
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
                            cmbApptLetterLocation.DataSource = oBindTable;
                            cmbApptLetterLocation.DisplayMember = "DispName";
                            cmbApptLetterLocation.ValueMember = "ID";
                        }
                        break;
                    case "btnApptLetterBrowseProvider":
                      
                        cmbApptLetterProvider.DataSource = null;
                        cmbApptLetterProvider.Items.Clear();
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
                            cmbApptLetterProvider.DataSource = oBindTable;
                            cmbApptLetterProvider.DisplayMember = "DispName";
                            cmbApptLetterProvider.ValueMember = "ID";
                        }
                        break;
                    case "btnApptLetterBrowseResource":
                       
                        cmbApptLetterResource.DataSource = null;
                        cmbApptLetterResource.Items.Clear();
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
                            cmbApptLetterResource.DataSource = oBindTable;
                            cmbApptLetterResource.DisplayMember = "DispName";
                            cmbApptLetterResource.ValueMember = "ID";
                        }
                        break;
                    case "btnApptLetterBrowseApptType":
                       
                        cmbApptLetterApptType.DataSource = null;
                        cmbApptLetterApptType.Items.Clear();
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
                            cmbApptLetterApptType.DataSource = oBindTable;
                            cmbApptLetterApptType.DisplayMember = "DispName";
                            cmbApptLetterApptType.ValueMember = "ID";
                        }
                        break;
                    case "btnApptLetterBrowseApptTypeType":
                       
                        cmbApptLetterApptTypeType.DataSource = null;
                        cmbApptLetterApptTypeType.Items.Clear();
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
                            cmbApptLetterApptTypeType.DataSource = oBindTable;
                            cmbApptLetterApptTypeType.DisplayMember = "DispName";
                            cmbApptLetterApptTypeType.ValueMember = "ID";
                        }
                        break;
                    case "btnApptLetterBrowsePatient":
                       
                        cmbApptLetterPatient.DataSource = null;
                        cmbApptLetterPatient.Items.Clear();
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
                            cmbApptLetterPatient.DataSource = oBindTable;
                            cmbApptLetterPatient.DisplayMember = "DispName";
                            cmbApptLetterPatient.ValueMember = "ID";
                        }
                        break;
                    case "btnApptLetterBrowseInsurance":
                        if (rbtnApptLetterCompany.Checked)
                        {
                           
                            cmbApptLetterInsuranceCompany.DataSource = null;
                            cmbApptLetterInsuranceCompany.Items.Clear();
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
                                cmbApptLetterInsuranceCompany.DataSource = oBindTable;
                                cmbApptLetterInsuranceCompany.DisplayMember = "DispName";
                                cmbApptLetterInsuranceCompany.ValueMember = "ID";
                            }
                        }
                        else if (rbtnApptLetterPlan.Checked)
                        {
                           
                            cmbApptLetterInsurancePlan.DataSource = null;
                            cmbApptLetterInsurancePlan.Items.Clear();
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
                                cmbApptLetterInsurancePlan.DataSource = oBindTable;
                                cmbApptLetterInsurancePlan.DisplayMember = "DispName";
                                cmbApptLetterInsurancePlan.ValueMember = "ID";
                            }
                        }
                        break;
                    default:
                        _SelectedFilterCombo = "";
                        break;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
               
            }
            finally
            {
                _SelectedFilterCombo = "";
            }
        }

        void oListControl_ItemClosedClick(object sender, EventArgs e)
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
                            pnl_tlspTOP.Visible = true;
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

                    }
                    catch { }
                   
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                _SelectedFilterCombo = "";            
            }
        }

        #endregion

        #region "Methods"
    
        private void FilterBy_Today()
        {
            try
            {
                dtpApptLetterFromDate.Value = DateTime.Today;
                dtpApptLetterToDate.Value = DateTime.Today;

                dtpApptLetterFromDate.Enabled = false;
                dtpApptLetterToDate.Enabled = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }

        }

        private void FilterBy_Tomorrow()
        {
            try
            {

                dtpApptLetterFromDate.Value = DateTime.Now.AddDays(1);
                dtpApptLetterToDate.Value = DateTime.Now.AddDays(1);

                dtpApptLetterFromDate.Enabled = false;
                dtpApptLetterToDate.Enabled = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void FilterBy_Yesterday()
        {
            try
            {
                dtpApptLetterFromDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(24, 0, 0));
                dtpApptLetterToDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(24, 0, 0));

                dtpApptLetterFromDate.Enabled = false;
                dtpApptLetterToDate.Enabled = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void FilterBy_Thisweek()
        {
            try
            {
                if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
                {
                    dtpApptLetterFromDate.Value = DateTime.Today;
                    dtpApptLetterToDate.Value = DateTime.Now.Date.AddDays(6);

                }
                if (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday)
                {
                    dtpApptLetterFromDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(1, 0, 0, 0));
                    dtpApptLetterToDate.Value = dtpApptLetterFromDate.Value.AddDays(6);
                }
                if (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday)
                {
                    dtpApptLetterFromDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(2, 0, 0, 0));
                    dtpApptLetterToDate.Value = dtpApptLetterFromDate.Value.AddDays(6);
                }
                if (DateTime.Now.DayOfWeek == DayOfWeek.Thursday)
                {
                    dtpApptLetterFromDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(3, 0, 0, 0));
                    dtpApptLetterToDate.Value = dtpApptLetterFromDate.Value.AddDays(6);

                }
                if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
                {
                    dtpApptLetterFromDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(4, 0, 0, 0));
                    dtpApptLetterToDate.Value = dtpApptLetterFromDate.Value.AddDays(6);
                }

                if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
                {
                    dtpApptLetterFromDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(5, 0, 0, 0));
                    dtpApptLetterToDate.Value = dtpApptLetterFromDate.Value.AddDays(6);
                }

                if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
                {
                    dtpApptLetterFromDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(6, 0, 0, 0));
                    dtpApptLetterToDate.Value = dtpApptLetterFromDate.Value.AddDays(6);
                }

                dtpApptLetterFromDate.Enabled = false;
                dtpApptLetterToDate.Enabled = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void FilterBy_lastweek()
        {
            try
            {
                if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
                {
                    dtpApptLetterFromDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(7, 0, 0, 0));
                    dtpApptLetterToDate.Value = dtpApptLetterFromDate.Value.AddDays(6);

                }
                if (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday)
                {
                    dtpApptLetterFromDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(8, 0, 0, 0));
                    dtpApptLetterToDate.Value = dtpApptLetterFromDate.Value.AddDays(6);
                }
                if (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday)
                {
                    dtpApptLetterFromDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(9, 0, 0, 0));
                    dtpApptLetterToDate.Value = dtpApptLetterFromDate.Value.AddDays(6);
                }
                if (DateTime.Now.DayOfWeek == DayOfWeek.Thursday)
                {
                    dtpApptLetterFromDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(10, 0, 0, 0));
                    dtpApptLetterToDate.Value = dtpApptLetterFromDate.Value.AddDays(6);

                }
                if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
                {
                    dtpApptLetterFromDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(11, 0, 0, 0));
                    dtpApptLetterToDate.Value = dtpApptLetterFromDate.Value.AddDays(6);
                }

                if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
                {
                    dtpApptLetterFromDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(12, 0, 0, 0));
                    dtpApptLetterToDate.Value = dtpApptLetterFromDate.Value.AddDays(6);
                }

                if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
                {
                    dtpApptLetterFromDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(13, 0, 0, 0));
                    dtpApptLetterToDate.Value = dtpApptLetterFromDate.Value.AddDays(6);
                }

                dtpApptLetterFromDate.Enabled = false;
                dtpApptLetterToDate.Enabled = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void FilterBy_currentmonth()
        {
            try
            {
                DateTime dtFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

                // for any date passed in to the method
                // create a datetime variable set to the passed in date
                DateTime dtTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                // overshoot the date by a month

                dtTo = dtTo.AddMonths(1);
                // remove all of the days in the next month
                // to get bumped down to the last day of the 
                // previous month
                dtTo = dtTo.AddDays(-(dtTo.Day));
                dtpApptLetterFromDate.Value = Convert.ToDateTime(dtFrom.Date);
                dtpApptLetterToDate.Value = Convert.ToDateTime(dtTo.Date);

                dtpApptLetterFromDate.Enabled = false;
                dtpApptLetterToDate.Enabled = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }

        }

        private void FilterBy_lastmonth()
        {
            try
            {
                DateTime firstDay = new DateTime(DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month, 1);

                int DaysinMonth = DateTime.DaysInMonth(DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month);

                DateTime lastDay = firstDay.AddMonths(1).AddTicks(-1);

                dtpApptLetterFromDate.Value = Convert.ToDateTime(firstDay.Date);
                dtpApptLetterToDate.Value = Convert.ToDateTime(lastDay.Date);

                dtpApptLetterFromDate.Enabled = false;
                dtpApptLetterToDate.Enabled = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void FilterBy_currenYear()
        {
            try
            {
                DateTime dtFrom = new DateTime(DateTime.Now.Year, 1, 1);

                dtpApptLetterFromDate.Value = Convert.ToDateTime(dtFrom.Date);
                dtpApptLetterToDate.Value = DateTime.Today;

                dtpApptLetterFromDate.Enabled = false;
                dtpApptLetterToDate.Enabled = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void FilterBy_last30days()
        {
            try
            {
                dtpApptLetterFromDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(30, 0, 0, 0));
                dtpApptLetterToDate.Value = DateTime.Today;

                dtpApptLetterFromDate.Enabled = false;
                dtpApptLetterToDate.Enabled = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void FilterBy_last60days()
        {
            try
            {
                dtpApptLetterFromDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(60, 0, 0, 0));
                dtpApptLetterToDate.Value = DateTime.Today;

                dtpApptLetterFromDate.Enabled = false;
                dtpApptLetterToDate.Enabled = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }

        }

        private void FilterBy_last90days()
        {
            try
            {
                dtpApptLetterFromDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(90, 0, 0, 0));
                dtpApptLetterToDate.Value = DateTime.Today;

                dtpApptLetterFromDate.Enabled = false;
                dtpApptLetterToDate.Enabled = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void FilterBy_last120days()
        {
            try
            {
                dtpApptLetterFromDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(120, 0, 0, 0));
                dtpApptLetterToDate.Value = DateTime.Today;

                dtpApptLetterFromDate.Enabled = false;
                dtpApptLetterToDate.Enabled = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void FilterBy_DateRange()
        {
            try
            {
                //dtpApptLetterFromDate.Value = dtpApptLetterFromDate.Value;
                //dtpApptLetterToDate.Value = dtpApptLetterToDate.Value;

                dtpApptLetterFromDate.Enabled = true;
                dtpApptLetterToDate.Enabled = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }
        
        private void SetTiming()
        {
            gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
            object value = new object();
            try
            {
                ogloSettings.GetSetting("ClinicStartTime", out value);
                if (value != null && Convert.ToString(value).Trim() != "")
                {
                    _dtClinicStartTime = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + value.ToString());
                    value = null;
                }

                ogloSettings.GetSetting("ClinicEndTime", out value);
                if (value != null && Convert.ToString(value).Trim() != "")
                {
                    _dtClinicEndTime = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + value.ToString());
                    value = null;
                }

                dtpChkInDate.Value = DateTime.Today;
                dtpChkInApptStartTimeFrom.Value = _dtClinicStartTime;
                dtpChkInApptStartTimeTo.Value = _dtClinicEndTime;
                dtpApptLetterFromDate.Value = DateTime.Today;
                dtpApptLetterToDate.Value = DateTime.Today;
                dtpApptLetterApptStartTimeFrom.Value = _dtClinicStartTime;
                dtpApptLetterApptStartTimeTo.Value = _dtClinicEndTime;

                dtpChkInApptStartTimeFrom.Checked = false;
                dtpChkInApptStartTimeTo.Checked = false;
                dtpApptLetterApptStartTimeFrom.Checked = false;
                dtpApptLetterApptStartTimeTo.Checked = false;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ogloSettings != null) { ogloSettings.Dispose(); }
                value = null;
            }
        }

        private void Fill_FilterDatesCombo()
        {
            try
            {
                cmbApptLetterDateRange.Items.Clear();
                cmbApptLetterDateRange.Items.Add("Custom");
                cmbApptLetterDateRange.Items.Add("Today");
                cmbApptLetterDateRange.Items.Add("Tomorrow");
                cmbApptLetterDateRange.Items.Add("Yesterday");
                cmbApptLetterDateRange.Items.Add("This Week");
                cmbApptLetterDateRange.Items.Add("Last Week");
                cmbApptLetterDateRange.Items.Add("Current Month");
                cmbApptLetterDateRange.Items.Add("Last Month");
                cmbApptLetterDateRange.Items.Add("Current Year");
                cmbApptLetterDateRange.Items.Add("Last 30 Days");
                cmbApptLetterDateRange.Items.Add("Last 60 Days");
                cmbApptLetterDateRange.Items.Add("Last 90 Days");
                cmbApptLetterDateRange.Items.Add("Last 120 Days");
                cmbApptLetterDateRange.Refresh();

            }
            catch (Exception )//ex)
            {
                //ex.ToString();
                //ex = null;
                //gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }

        private DataTable getBatchAppointment() 
        {
            DataTable dtBatchAppt = new DataTable();
            gloDatabaseLayer.DBLayer oDB =new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {

                String sLocations = string.Empty;
                String sProviders = string.Empty;
                String sResources = string.Empty;
                String sAppointmentType = string.Empty;
                String sAppointmentTypeType = string.Empty;
                Int64 nPatient = 0;
                String sInsuranceCompany = string.Empty;
                String sInsurancePlan = string.Empty;
                Int32 ndtFromDate = 0;
                Int32 ndtToDate = 0;
                Int32 dtApptStartTimeFrom = 0;
                Int32 dtApptStartTimeTo = 0;
                bool IncludeOneAppt = false;
                bool isCompanyFilter = false; 
                String sInsuranceType =string.Empty;
                String sAppointmentStatus = string.Empty;
                bool IsCheckInTabSelected = false;

                 oDB.Connect(false);

                if (tbBatchPrint.SelectedTab == tbCheckInPre)
                {
                    IsCheckInTabSelected = true;
                    if (cmbChkInLocation.Items.Count > 0)
                    {

                         for (int cntrChkInLocation = 0; cntrChkInLocation <= cmbChkInLocation.Items.Count - 1; cntrChkInLocation++)
                         {
                             if (sLocations == string.Empty)
                                 sLocations = (Convert.ToString(((DataRowView)cmbChkInLocation.Items[cntrChkInLocation])["ID"]));
                             else
                                 sLocations = sLocations + "," + (Convert.ToString(((DataRowView)cmbChkInLocation.Items[cntrChkInLocation])["ID"]));
                         }
                     }

                     if (cmbChkInProvider.Items.Count > 0)
                     {

                         for (int cntrChkInProvider = 0; cntrChkInProvider <= cmbChkInProvider.Items.Count - 1; cntrChkInProvider++)
                         {
                             if (sProviders == string.Empty)
                                 sProviders = (Convert.ToString(((DataRowView)cmbChkInProvider.Items[cntrChkInProvider])["ID"]));
                             else
                                 sProviders = sProviders + "," + (Convert.ToString(((DataRowView)cmbChkInProvider.Items[cntrChkInProvider])["ID"]));
                         }
                     }

                     if (cmbChkInReosurce.Items.Count > 0)
                     {

                         for (int cntrChkInReosurce = 0; cntrChkInReosurce <= cmbChkInReosurce.Items.Count - 1; cntrChkInReosurce++)
                         {
                             if (sResources == string.Empty)
                                 sResources = (Convert.ToString(((DataRowView)cmbChkInReosurce.Items[cntrChkInReosurce])["ID"]));
                             else
                                 sResources = sResources + "," + (Convert.ToString(((DataRowView)cmbChkInReosurce.Items[cntrChkInReosurce])["ID"]));
                         }
                     }

                     if (cmbChkInApptType.Items.Count > 0)
                     {

                         for (int cntrChkInApptType = 0; cntrChkInApptType <= cmbChkInApptType.Items.Count - 1; cntrChkInApptType++)
                         {
                             if (sAppointmentType == string.Empty)
                                 sAppointmentType = (Convert.ToString(((DataRowView)cmbChkInApptType.Items[cntrChkInApptType])["ID"]));
                             else
                                 sAppointmentType = sAppointmentType + "," + (Convert.ToString(((DataRowView)cmbChkInApptType.Items[cntrChkInApptType])["ID"]));
                         }
                     }

                     if (cmbChkInApptTypeType.Items.Count > 0)
                     {

                         for (int cntrChkInApptTypeType = 0; cntrChkInApptTypeType <= cmbChkInApptTypeType.Items.Count - 1; cntrChkInApptTypeType++)
                         {
                             if (sAppointmentTypeType == string.Empty)
                                 sAppointmentTypeType = (Convert.ToString(((DataRowView)cmbChkInApptTypeType.Items[cntrChkInApptTypeType])["ID"]));
                             else
                                 sAppointmentTypeType = sAppointmentTypeType + "," + (Convert.ToString(((DataRowView)cmbChkInApptTypeType.Items[cntrChkInApptTypeType])["ID"]));
                         }
                     }

                     if (cmbChkInPatient.Items.Count > 0)
                     {
                         for (int cntrChkInPatient = 0; cntrChkInPatient <= cmbChkInPatient.Items.Count - 1; cntrChkInPatient++)
                         {
                             nPatient = (Convert.ToInt64(((DataRowView)cmbChkInPatient.Items[cntrChkInPatient])["ID"]));
                         }
                     }

                     if (cmbChkInInsuranceCompany.Items.Count > 0)
                     {

                         for (int cntrChkInInsuranceCompany = 0; cntrChkInInsuranceCompany <= cmbChkInInsuranceCompany.Items.Count - 1; cntrChkInInsuranceCompany++)
                         {
                             if (sInsuranceCompany == string.Empty)
                                 sInsuranceCompany = (Convert.ToString(((DataRowView)cmbChkInInsuranceCompany.Items[cntrChkInInsuranceCompany])["ID"]));
                             else
                                 sInsuranceCompany = sInsuranceCompany + "," + (Convert.ToString(((DataRowView)cmbChkInInsuranceCompany.Items[cntrChkInInsuranceCompany])["ID"]));
                         }
                     }

                     if (cmbChkInInsurancePlan.Items.Count > 0)
                     {

                         for (int cntrChkInInsurancePlan = 0; cntrChkInInsurancePlan <= cmbChkInInsurancePlan.Items.Count - 1; cntrChkInInsurancePlan++)
                         {
                             if (sInsurancePlan == string.Empty)
                                 sInsurancePlan = (Convert.ToString(((DataRowView)cmbChkInInsurancePlan.Items[cntrChkInInsurancePlan])["ID"]));
                             else
                                 sInsurancePlan = sInsurancePlan + "," + (Convert.ToString(((DataRowView)cmbChkInInsurancePlan.Items[cntrChkInInsurancePlan])["ID"]));
                         }
                     }

                     ndtFromDate=gloDateMaster.gloDate.DateAsNumber(dtpChkInDate.Value.ToString("MM/dd/yyyy"));
                     ndtToDate = gloDateMaster.gloDate.DateAsNumber(dtpChkInDate.Value.ToString("MM/dd/yyyy"));

                     if (dtpChkInApptStartTimeFrom.Checked)
                          dtApptStartTimeFrom = gloDateMaster.gloTime.TimeAsNumber(dtpChkInApptStartTimeFrom.Value.ToString("hh:mm tt"));
                     else
                         dtApptStartTimeFrom = 0;


                     if (dtpChkInApptStartTimeTo.Checked)                    
                         dtApptStartTimeTo = gloDateMaster.gloTime.TimeAsNumber(dtpChkInApptStartTimeTo.Value.ToString("hh:mm tt"));
                     else
                         dtApptStartTimeTo = 0;

                     if (chkChkInIncludeOneAppt.Checked)
                     {
                         IncludeOneAppt = true;

                     }
                     else 
                     {
                         IncludeOneAppt = false;
                     }


                     if (rbtnChkInCompany.Checked)
                     {
                         isCompanyFilter = true; 
                         sInsuranceType = "Company";                     
                     }
                     else if (rbtnChkInPlan.Checked)
                     {
                         isCompanyFilter = false;
                         sInsuranceType = "Plan";                     
                     }


                     //oDBParameters.Add("@FromDate", ndtFromDate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int); // NUMERIC= NULL,   
                     //oDBParameters.Add("@ToDate", ndtToDate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int); //  NUMERIC= NULL,  

                     //if (sProviders != string.Empty)
                     //    oDBParameters.Add("@Providers", sProviders, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL,      
                     //else
                     //    oDBParameters.Add("@Providers", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL,    

                     //if (sLocations != string.Empty)
                     //    oDBParameters.Add("@Locations", sLocations, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL, 
                     //else
                     //    oDBParameters.Add("@Locations", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL, 

                     //if (sResources != string.Empty)
                     //    oDBParameters.Add("@Resources", sResources, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL,    
                     //else
                     //    oDBParameters.Add("@Resources", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL,    

                     //if (sPatient != string.Empty)
                     //    oDBParameters.Add("@PatientID", sPatient, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt); //  NUMERIC(18,0)=NULL,
                     //else
                     //    oDBParameters.Add("@PatientID", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt); //  NUMERIC(18,0)=NULL,

                     //if (rbtnChkInCompany.Checked)
                     //{
                     //    isCompanyFilter = true; 
                     //    //if (sInsuranceCompany != string.Empty)
                     //    //{
                     //    //    oDBParameters.Add("@InsuranceCompanies", sInsuranceCompany, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL, 
                     //    //     oDBParameters.Add("@InsurancePlans", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL,   
                     //    //}
                     //    //else
                     //    //{
                     //    //    oDBParameters.Add("@InsuranceCompanies", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL, 
                     //    // oDBParameters.Add("@InsurancePlans", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL,   
                     //    //}
                     //}
                     //else if (rbtnChkInPlan.Checked)
                     //{
                     //    isCompanyFilter = false;
                     //    //if(sInsurancePlan != string.Empty)
                     //    //{                              
                     //    //    oDBParameters.Add("@InsurancePlans",sInsurancePlan, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL,   
                     //    //      oDBParameters.Add("@InsuranceCompanies", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL, 
                     //    //}
                     //    //else
                     //    //{                            
                     //    //    oDBParameters.Add("@InsurancePlans", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL,   
                     //    //     oDBParameters.Add("@InsuranceCompanies", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL, 
                     //    //}

                     //}
                     //if (sAppointmentType != string.Empty)
                     //    oDBParameters.Add("@AppointmentTypes", sAppointmentType, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);//  VARCHAR(MAX)=NULL,   
                     //else
                     //    oDBParameters.Add("@AppointmentTypes", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);//  VARCHAR(MAX)=NULL,   

                     //if (sAppointmentTypeType != string.Empty)
                     //    oDBParameters.Add("@AppointmentTypeTypes", sAppointmentTypeType, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL,
                     //else
                     //    oDBParameters.Add("@AppointmentTypeTypes", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL,

                    //oDBParameters.Add("@ApptStartTimesFrom", dtApptStartTimeFrom, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int); //  NUMERIC =NULL,
                    //oDBParameters.Add("@ApptStartTimesTo", dtApptStartTimeTo, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int); //  NUMERIC =NULL,    
                    //oDBParameters.Add("@AppointmentStatus", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL,  
                    //oDBParameters.Add("@IncludeOneAppt", IncludeOneAppt, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit); //  BIT ,
                    //oDBParameters.Add("@InsuranceType", sInsuranceType, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(10) =NULL
                }
                else if (tbBatchPrint.SelectedTab == tbAppLetters)
                {
                    IsCheckInTabSelected = false;
                    if (cmbApptLetterLocation.Items.Count > 0)
                    {

                         for (int cntrApptLetterLocation = 0; cntrApptLetterLocation <= cmbApptLetterLocation.Items.Count - 1; cntrApptLetterLocation++)
                         {
                             if (sLocations == string.Empty)
                                 sLocations = (Convert.ToString(((DataRowView)cmbApptLetterLocation.Items[cntrApptLetterLocation])["ID"]));
                             else
                                 sLocations = sLocations + "," + (Convert.ToString(((DataRowView)cmbApptLetterLocation.Items[cntrApptLetterLocation])["ID"]));
                         }
                     }

                     if (cmbApptLetterProvider.Items.Count > 0)
                     {

                         for (int cntrApptLetterProvider = 0; cntrApptLetterProvider <= cmbApptLetterProvider.Items.Count - 1; cntrApptLetterProvider++)
                         {
                             if (sProviders == string.Empty)
                                 sProviders = (Convert.ToString(((DataRowView)cmbApptLetterProvider.Items[cntrApptLetterProvider])["ID"]));
                             else
                                 sProviders = sProviders + "," + (Convert.ToString(((DataRowView)cmbApptLetterProvider.Items[cntrApptLetterProvider])["ID"]));
                         }
                     }

                     if (cmbApptLetterResource.Items.Count > 0)
                     {

                         for (int cntrApptLetterReosurce = 0; cntrApptLetterReosurce <= cmbApptLetterResource.Items.Count - 1; cntrApptLetterReosurce++)
                         {
                             if (sResources == string.Empty)
                                 sResources = (Convert.ToString(((DataRowView)cmbApptLetterResource.Items[cntrApptLetterReosurce])["ID"]));
                             else
                                 sResources = sResources + "," + (Convert.ToString(((DataRowView)cmbApptLetterResource.Items[cntrApptLetterReosurce])["ID"]));
                         }
                     }

                     if (cmbApptLetterApptType.Items.Count > 0)
                     {

                         for (int cntrApptLetterApptType = 0; cntrApptLetterApptType <= cmbApptLetterApptType.Items.Count - 1; cntrApptLetterApptType++)
                         {
                             if (sAppointmentType == string.Empty)
                                 sAppointmentType = (Convert.ToString(((DataRowView)cmbApptLetterApptType.Items[cntrApptLetterApptType])["ID"]));
                             else
                                 sAppointmentType = sAppointmentType + "," + (Convert.ToString(((DataRowView)cmbApptLetterApptType.Items[cntrApptLetterApptType])["ID"]));
                         }
                     }

                     if (cmbApptLetterApptTypeType.Items.Count > 0)
                     {

                         for (int cntrApptLetterApptTypeType = 0; cntrApptLetterApptTypeType <= cmbApptLetterApptTypeType.Items.Count - 1; cntrApptLetterApptTypeType++)
                         {
                             if (sAppointmentTypeType == string.Empty)
                                 sAppointmentTypeType = (Convert.ToString(((DataRowView)cmbApptLetterApptTypeType.Items[cntrApptLetterApptTypeType])["ID"]));
                             else
                                 sAppointmentTypeType = sAppointmentTypeType + "," + (Convert.ToString(((DataRowView)cmbApptLetterApptTypeType.Items[cntrApptLetterApptTypeType])["ID"]));
                         }
                     }

                     if (cmbApptLetterPatient.Items.Count > 0)
                     {
                         nPatient = Convert.ToInt64(((DataRowView)cmbApptLetterPatient.Items[0])["ID"]);          
                     }

                     if (cmbApptLetterInsuranceCompany.Items.Count > 0)
                     {

                         for (int cntrApptLetterInsuranceCompany = 0; cntrApptLetterInsuranceCompany <= cmbApptLetterInsuranceCompany.Items.Count - 1; cntrApptLetterInsuranceCompany++)
                         {
                             if (sInsuranceCompany == string.Empty)
                                 sInsuranceCompany = (Convert.ToString(((DataRowView)cmbApptLetterInsuranceCompany.Items[cntrApptLetterInsuranceCompany])["ID"]));
                             else
                                 sInsuranceCompany = sInsuranceCompany + "," + (Convert.ToString(((DataRowView)cmbApptLetterInsuranceCompany.Items[cntrApptLetterInsuranceCompany])["ID"]));
                         }
                     }

                     if (cmbApptLetterInsurancePlan.Items.Count > 0)
                     {

                         for (int cntrApptLetterInsurancePlan = 0; cntrApptLetterInsurancePlan <= cmbApptLetterInsurancePlan.Items.Count - 1; cntrApptLetterInsurancePlan++)
                         {
                             if (sInsurancePlan == string.Empty)
                                 sInsurancePlan = (Convert.ToString(((DataRowView)cmbApptLetterInsurancePlan.Items[cntrApptLetterInsurancePlan])["ID"]));
                             else
                                 sInsurancePlan = sInsurancePlan + "," + (Convert.ToString(((DataRowView)cmbApptLetterInsurancePlan.Items[cntrApptLetterInsurancePlan])["ID"]));
                         }
                     }

                     ndtFromDate = gloDateMaster.gloDate.DateAsNumber(dtpApptLetterFromDate.Value.ToString("MM/dd/yyyy"));
                     ndtToDate = gloDateMaster.gloDate.DateAsNumber(dtpApptLetterToDate.Value.ToString("MM/dd/yyyy"));

                     if (dtpApptLetterApptStartTimeFrom.Checked)
                         dtApptStartTimeFrom = gloDateMaster.gloTime.TimeAsNumber(dtpApptLetterApptStartTimeFrom.Value.ToString("hh:mm tt"));
                     else
                         dtApptStartTimeFrom = 0;

                     if (dtpApptLetterApptStartTimeTo.Checked)
                         dtApptStartTimeTo = gloDateMaster.gloTime.TimeAsNumber(dtpApptLetterApptStartTimeTo.Value.ToString("hh:mm tt"));
                     else
                         dtApptStartTimeTo = 0;

                     if (chkApptLetterIncludeOneAppt.Checked)
                     {
                         IncludeOneAppt = true;

                     }
                     else
                     {
                         IncludeOneAppt = false;
                     }


                     if (rbtnApptLetterCompany.Checked)
                     {
                         isCompanyFilter = true; 
                         sInsuranceType = "Company";
                     }
                     else if (rbtnApptLetterPlan.Checked)
                     {
                         isCompanyFilter = false; 
                         sInsuranceType = "Plan";
                     }

                     if (rbtnApptLetterOpen.Checked)
                     {
                         sAppointmentStatus = "0,1,2,3,4,8,9,10";
                     }
                     else if (rbtnApptLetterNoShow.Checked)
                     {
                         sAppointmentStatus = "5";
                     }
                     else if (rbtnApptLetterCancelled.Checked)
                     {
                         sAppointmentStatus = "6";
                     }

                     //oDBParameters.Add("@FromDate", ndtFromDate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int); // NUMERIC= NULL,   
                     //oDBParameters.Add("@ToDate", ndtToDate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int); //  NUMERIC= NULL,  
                    
                     //if (sProviders != string.Empty)
                     //    oDBParameters.Add("@Providers", sProviders, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL,      
                     //else
                     //    oDBParameters.Add("@Providers", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL,    

                     //if (sLocations != string.Empty)
                     //    oDBParameters.Add("@Locations", sLocations, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL, 
                     //else
                     //    oDBParameters.Add("@Locations", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL, 

                     //if (sResources != string.Empty)
                     //    oDBParameters.Add("@Resources", sResources, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL,    
                     //else
                     //    oDBParameters.Add("@Resources", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL,    

                     //if (sPatient != string.Empty)
                     //    oDBParameters.Add("@PatientID", sPatient, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt); //  NUMERIC(18,0)=NULL,
                     //else
                     //    oDBParameters.Add("@PatientID", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt); //  NUMERIC(18,0)=NULL,

                     //if (rbtnApptLetterCompany.Checked)
                     //{
                     //    isCompanyFilter = true; 
                     //    //if (sInsuranceCompany != string.Empty)
                     //    //{
                     //    //    oDBParameters.Add("@InsuranceCompanies", sInsuranceCompany, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL, 
                     //    //    oDBParameters.Add("@InsurancePlans", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL,   
                     //    //}
                     //    //else
                     //    //{
                     //    //    oDBParameters.Add("@InsuranceCompanies", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL, 
                     //    //    oDBParameters.Add("@InsurancePlans", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL,   
                     //    //}
                     //}
                     //else if (rbtnApptLetterPlan.Checked)
                     //{
                     //    isCompanyFilter = false; 
                     //    //if (sInsurancePlan != string.Empty)
                     //    //{
                     //    //    oDBParameters.Add("@InsurancePlans", sInsurancePlan, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL,   
                     //    //    oDBParameters.Add("@InsuranceCompanies", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL, 
                     //    //}
                     //    //else
                     //    //{
                     //    //    oDBParameters.Add("@InsurancePlans", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL,   
                     //    //    oDBParameters.Add("@InsuranceCompanies", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL, 
                     //    //}

                     //}
                     //if (sAppointmentType != string.Empty)
                     //    oDBParameters.Add("@AppointmentTypes", sAppointmentType, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);//  VARCHAR(MAX)=NULL,   
                     //else
                     //    oDBParameters.Add("@AppointmentTypes", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);//  VARCHAR(MAX)=NULL,   

                     //if (sAppointmentTypeType != string.Empty)
                     //    oDBParameters.Add("@AppointmentTypeTypes", sAppointmentTypeType, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL,
                     //else
                     //    oDBParameters.Add("@AppointmentTypeTypes", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL,

                     //oDBParameters.Add("@ApptStartTimesFrom",dtApptStartTimeFrom, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int); //  NUMERIC =NULL,
                     //oDBParameters.Add("@ApptStartTimesTo", dtApptStartTimeTo, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int); //  NUMERIC =NULL,    
                     //oDBParameters.Add("@AppointmentStatus", sAppointmentStatus, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL,  
                     //oDBParameters.Add("@IncludeOneAppt", IncludeOneAppt, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit); //  BIT ,
                     //oDBParameters.Add("@InsuranceType", sInsuranceType, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(10) =NULL
                
                 }

                 oDBParameters.Add("@FromDate", ndtFromDate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int); // NUMERIC= NULL,   
                 oDBParameters.Add("@ToDate", ndtToDate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int); //  NUMERIC= NULL,  

                 if (sProviders != string.Empty)
                     oDBParameters.Add("@Providers", sProviders, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL,      
                 else
                     oDBParameters.Add("@Providers", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL,    

                 if (sLocations != string.Empty)
                     oDBParameters.Add("@Locations", sLocations, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL, 
                 else
                     oDBParameters.Add("@Locations", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL, 

                 if (sResources != string.Empty)
                     oDBParameters.Add("@Resources", sResources, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL,    
                 else
                     oDBParameters.Add("@Resources", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL,    

                 if (nPatient != 0)
                     oDBParameters.Add("@PatientID", nPatient, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt); //  NUMERIC(18,0)=NULL,
                 else
                     oDBParameters.Add("@PatientID", 0, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt); //  NUMERIC(18,0)=NULL,

                 if (isCompanyFilter == true)
                 {
                     if (sInsuranceCompany != string.Empty)
                     {
                         oDBParameters.Add("@InsuranceCompanies", sInsuranceCompany, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL, 
                         oDBParameters.Add("@InsurancePlans", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL,   
                     }
                     else
                     {
                         oDBParameters.Add("@InsuranceCompanies", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL, 
                         oDBParameters.Add("@InsurancePlans", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL,   
                     }
                 }
                 else 
                 {
                     if (sInsurancePlan != string.Empty)
                     {
                         oDBParameters.Add("@InsurancePlans", sInsurancePlan, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL,   
                         oDBParameters.Add("@InsuranceCompanies", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL, 
                     }
                     else
                     {
                         oDBParameters.Add("@InsurancePlans", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL,   
                         oDBParameters.Add("@InsuranceCompanies", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL, 
                     }

                 }
                 if (sAppointmentType != string.Empty)
                     oDBParameters.Add("@AppointmentTypes", sAppointmentType, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);//  VARCHAR(MAX)=NULL,   
                 else
                     oDBParameters.Add("@AppointmentTypes", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);//  VARCHAR(MAX)=NULL,   

                 if (sAppointmentTypeType != string.Empty)
                     oDBParameters.Add("@AppointmentTypeTypes", sAppointmentTypeType, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL,
                 else
                     oDBParameters.Add("@AppointmentTypeTypes", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL,

                 if (dtApptStartTimeFrom != 0)
                     oDBParameters.Add("@ApptStartTimesFrom", dtApptStartTimeFrom, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int); //  NUMERIC =NULL,
                 else                     
                     oDBParameters.Add("@ApptStartTimesFrom", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int); //  NUMERIC =NULL,

                 if (dtApptStartTimeTo != 0)
                     oDBParameters.Add("@ApptStartTimesTo", dtApptStartTimeTo, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int); //  NUMERIC =NULL,    
                 else
                     oDBParameters.Add("@ApptStartTimesTo", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int); //  NUMERIC =NULL,    


                oDBParameters.Add("@AppointmentStatus", sAppointmentStatus, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL,  
                oDBParameters.Add("@IncludeOneAppt", IncludeOneAppt, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit); //  BIT ,
                oDBParameters.Add("@InsuranceType", sInsuranceType, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(10) =NULL
                oDBParameters.Add("@CheckInTab", IsCheckInTabSelected, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit); //  VARCHAR(10) =NULL

                oDB.Retrive("gsp_AppointmentBatchPrint", oDBParameters, out dtBatchAppt);

            }
            catch //(Exception ex)
            {


            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }    
            }

            return dtBatchAppt;
        
        }

        private void generateBatch()
        {
            try
            {
                DataTable oBindTable = new DataTable();
                oBindTable = getBatchAppointment();

                if (tbBatchPrint.SelectedTab == tbCheckInPre)
                {
                    c1ChkInPatients.DataSource = oBindTable;
                    c1ChkInPatients.Cols[COL_SELECT].Caption = "Select";
                    c1ChkInPatients.Cols[COL_APPOINTMENTID].Caption = "Appointment ID";
                    c1ChkInPatients.Cols[COL_DATE].Caption = "Date"; 
                    c1ChkInPatients.Cols[COL_TIME].Caption = "Time";
                    c1ChkInPatients.Cols[COL_TYPE].Caption = "Type";
                    c1ChkInPatients.Cols[COL_PATIENT].Caption = "Patient";
                    c1ChkInPatients.Cols[COL_PROVIDER].Caption = "Provider";
                    c1ChkInPatients.Cols[COL_LOCATION].Caption = "Location";
                    c1ChkInPatients.Cols[COL_STATUS].Caption = "Status";
                    c1ChkInPatients.Cols[COL_PATIENTID].Caption = "Patient ID";
                    c1ChkInPatients.Cols[COL_DTLAPPOINTMENTID].Caption = "Appointment Detail ID";
                    c1ChkInPatients.Cols[COL_nDATE].Caption = "nDate";
                    c1ChkInPatients.Cols[COL_nTIME].Caption = "nTime";

                    c1ChkInPatients.Cols[COL_SELECT].Visible = true;
                    c1ChkInPatients.Cols[COL_APPOINTMENTID].Visible = false;
                    c1ChkInPatients.Cols[COL_DATE].Visible = false;
                    c1ChkInPatients.Cols[COL_TIME].Visible = true;
                    c1ChkInPatients.Cols[COL_TYPE].Visible = true;
                    c1ChkInPatients.Cols[COL_PATIENT].Visible = true;
                    c1ChkInPatients.Cols[COL_PROVIDER].Visible = true;
                    c1ChkInPatients.Cols[COL_LOCATION].Visible = true;
                    c1ChkInPatients.Cols[COL_STATUS].Visible = true;
                    c1ChkInPatients.Cols[COL_PATIENTID].Visible = false;
                    c1ChkInPatients.Cols[COL_DTLAPPOINTMENTID].Visible = false;
                    c1ChkInPatients.Cols[COL_nDATE].Visible = false;
                    c1ChkInPatients.Cols[COL_nTIME].Visible = false;


                    c1ChkInPatients.Cols[COL_SELECT].DataType = Type.GetType("System.Boolean");
                    //c1ChkInPatients.Cols[COL_SELECT].v = true;
                  //  c1ChkInPatients.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns;

                    int width = pnlChkInGrid.Width / 7;
                    c1ChkInPatients.Cols[COL_SELECT].Width = (int)(width * 0.3);
                    c1ChkInPatients.Cols[COL_APPOINTMENTID].Width = 0;
                    c1ChkInPatients.Cols[COL_DATE].Width = 0;
                    c1ChkInPatients.Cols[COL_TIME].Width =(int)(width * 0.5);
                    c1ChkInPatients.Cols[COL_TYPE].Width = (int)(width * 0.9);
                    c1ChkInPatients.Cols[COL_PATIENT].Width = width;
                    c1ChkInPatients.Cols[COL_PROVIDER].Width = width + width;
                    c1ChkInPatients.Cols[COL_LOCATION].Width = width;
                    c1ChkInPatients.Cols[COL_STATUS].Width = width;
                    c1ChkInPatients.Cols[COL_PATIENTID].Width = 0;
                    c1ChkInPatients.Cols[COL_DTLAPPOINTMENTID].Width = 0;
                    c1ChkInPatients.Cols[COL_nDATE].Width = 0;
                    c1ChkInPatients.Cols[COL_nTIME].Width = 0;


                    c1ChkInPatients.Cols[COL_SELECT].AllowEditing = true;
                    c1ChkInPatients.Cols[COL_APPOINTMENTID].AllowEditing = false;
                    c1ChkInPatients.Cols[COL_DATE].AllowEditing = false;
                    c1ChkInPatients.Cols[COL_TIME].AllowEditing = false;
                    c1ChkInPatients.Cols[COL_TYPE].AllowEditing = false;
                    c1ChkInPatients.Cols[COL_PATIENT].AllowEditing = false;
                    c1ChkInPatients.Cols[COL_PROVIDER].AllowEditing = false;
                    c1ChkInPatients.Cols[COL_LOCATION].AllowEditing = false;
                    c1ChkInPatients.Cols[COL_STATUS].AllowEditing = false;
                    c1ChkInPatients.Cols[COL_PATIENTID].AllowEditing = false;
                    c1ChkInPatients.Cols[COL_DTLAPPOINTMENTID].AllowEditing = false;
                    c1ChkInPatients.Cols[COL_nDATE].AllowEditing = false;
                    c1ChkInPatients.Cols[COL_nTIME].AllowEditing = false;


                    //for (int i = 1; i < c1ChkInPatients.Rows.Count; i++)
                    //{
                    //    c1ChkInPatients.SetCellCheck(i, 0, C1.Win.C1FlexGrid.CheckEnum.Checked);
                    //}

                    btnChkInSelectAll.Visible = false;
                    btnChkInClearAll.Visible = true;
                }
                else if (tbBatchPrint.SelectedTab == tbAppLetters)
                {
                    
                    c1ApptLetterPatients.DataSource = oBindTable;

                    c1ApptLetterPatients.DataSource = oBindTable;
                    c1ApptLetterPatients.Cols[COL_SELECT].Caption = "Select";
                    c1ApptLetterPatients.Cols[COL_APPOINTMENTID].Caption = "Appointment ID";
                    c1ApptLetterPatients.Cols[COL_DATE].Caption = "Date";
                    c1ApptLetterPatients.Cols[COL_TIME].Caption = "Time";
                    c1ApptLetterPatients.Cols[COL_TYPE].Caption = "Type";
                    c1ApptLetterPatients.Cols[COL_PATIENT].Caption = "Patient";
                    c1ApptLetterPatients.Cols[COL_PROVIDER].Caption = "Provider";
                    c1ApptLetterPatients.Cols[COL_LOCATION].Caption = "Location";
                    c1ApptLetterPatients.Cols[COL_STATUS].Caption = "Status";
                    c1ApptLetterPatients.Cols[COL_PATIENTID].Caption = "Patient ID";
                    c1ApptLetterPatients.Cols[COL_DTLAPPOINTMENTID].Caption = "Appointment Detail ID";
                    c1ApptLetterPatients.Cols[COL_nDATE].Caption = "nDate";
                    c1ApptLetterPatients.Cols[COL_nTIME].Caption = "nTime";


                    c1ApptLetterPatients.Cols[COL_SELECT].Visible = true;
                    c1ApptLetterPatients.Cols[COL_APPOINTMENTID].Visible = false;
                    c1ApptLetterPatients.Cols[COL_DATE].Visible = true;
                    c1ApptLetterPatients.Cols[COL_TIME].Visible = true;
                    c1ApptLetterPatients.Cols[COL_TYPE].Visible = true;
                    c1ApptLetterPatients.Cols[COL_PATIENT].Visible = true;
                    c1ApptLetterPatients.Cols[COL_PROVIDER].Visible = true;
                    c1ApptLetterPatients.Cols[COL_LOCATION].Visible = true;
                    c1ApptLetterPatients.Cols[COL_STATUS].Visible = true;
                    c1ApptLetterPatients.Cols[COL_PATIENTID].Visible = false;
                    c1ApptLetterPatients.Cols[COL_DTLAPPOINTMENTID].Visible = false;
                    c1ApptLetterPatients.Cols[COL_nDATE].Visible = false;
                    c1ApptLetterPatients.Cols[COL_nTIME].Visible = false;

                    c1ApptLetterPatients.Cols[COL_SELECT].DataType = Type.GetType("System.Boolean");
                    //Code start-Added by kanchan on 20130610 to solve bug for sorting date
                    c1ApptLetterPatients.Cols[COL_DATE].DataType = typeof(System.DateTime);
                    c1ApptLetterPatients.Cols[COL_DATE].Format = "MM/dd/yyyy";

                    c1ApptLetterPatients.Cols[COL_TIME].DataType = typeof(System.DateTime);
                    c1ApptLetterPatients.Cols[COL_TIME].Format = "h:mm tt";
                    //Code End-Added by kanchan on 20130610 to solve bug for sorting date
                   // c1ApptLetterPatients.Cols[COL_SELECT].Selected = true;

                    //c1ApptLetterPatients.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns;

                    int width = pnlChkInGrid.Width / 7;
                    c1ApptLetterPatients.Cols[COL_SELECT].Width = (int)(width * 0.3);
                    c1ApptLetterPatients.Cols[COL_APPOINTMENTID].Width = 0;
                    c1ApptLetterPatients.Cols[COL_DATE].Width = (int)(width * 0.5);
                    c1ApptLetterPatients.Cols[COL_TIME].Width = (int)(width * 0.5);
                    c1ApptLetterPatients.Cols[COL_TYPE].Width = (int)(width * 0.9);
                    c1ApptLetterPatients.Cols[COL_PATIENT].Width = width;
                    c1ApptLetterPatients.Cols[COL_PROVIDER].Width = width + width;
                    c1ApptLetterPatients.Cols[COL_LOCATION].Width = width;
                    c1ApptLetterPatients.Cols[COL_STATUS].Width = width;
                    c1ApptLetterPatients.Cols[COL_PATIENTID].Width = 0;
                    c1ApptLetterPatients.Cols[COL_DTLAPPOINTMENTID].Width = 0;
                    c1ApptLetterPatients.Cols[COL_nDATE].Width = 0;
                    c1ApptLetterPatients.Cols[COL_nTIME].Width = 0;

                    c1ApptLetterPatients.Cols[COL_SELECT].AllowEditing = true;
                    c1ApptLetterPatients.Cols[COL_APPOINTMENTID].AllowEditing = false;
                    c1ApptLetterPatients.Cols[COL_DATE].AllowEditing = false;
                    c1ApptLetterPatients.Cols[COL_TIME].AllowEditing = false;
                    c1ApptLetterPatients.Cols[COL_TYPE].AllowEditing = false;
                    c1ApptLetterPatients.Cols[COL_PATIENT].AllowEditing = false;
                    c1ApptLetterPatients.Cols[COL_PROVIDER].AllowEditing = false;
                    c1ApptLetterPatients.Cols[COL_LOCATION].AllowEditing = false;
                    c1ApptLetterPatients.Cols[COL_STATUS].AllowEditing = false;
                    c1ApptLetterPatients.Cols[COL_PATIENTID].AllowEditing = false;
                    c1ApptLetterPatients.Cols[COL_DTLAPPOINTMENTID].AllowEditing = false;
                    c1ApptLetterPatients.Cols[COL_nDATE].AllowEditing = false;
                    c1ApptLetterPatients.Cols[COL_nTIME].AllowEditing = false;

                    
                    //for (int i = 1; i < c1ApptLetterPatients.Rows.Count; i++)
                    //{
                    //    c1ApptLetterPatients.SetCellCheck(i, 0, C1.Win.C1FlexGrid.CheckEnum.Checked);
                    //}

                    btnApptLetterSelectAll.Visible = false;
                    btnApptLetterClearAll.Visible = true;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void DesignGrid()
        {

         //DESIGN CHECK IN PATIENT GRID
          //  c1ChkInPatients.Clear();
                c1ChkInPatients.DataSource = null;
                c1ChkInPatients.Clear(C1.Win.C1FlexGrid.ClearFlags.All);
                c1ChkInPatients.Cols.Count = COL_COLCOUNT;
                c1ChkInPatients.Rows.Count = 1;
                c1ChkInPatients.Cols.Fixed = 0;


                c1ChkInPatients.SetData(0, COL_SELECT, "Select");
                c1ChkInPatients.SetData(0, COL_APPOINTMENTID, "Appointment ID");
                c1ChkInPatients.SetData(0, COL_DATE, "Date");
                c1ChkInPatients.SetData(0, COL_TIME, "Time");
                c1ChkInPatients.SetData(0, COL_TYPE, "Type");
                c1ChkInPatients.SetData(0, COL_PATIENT, "Patient");
                c1ChkInPatients.SetData(0, COL_PROVIDER, "Provider");
                c1ChkInPatients.SetData(0, COL_LOCATION, "Location");
                c1ChkInPatients.SetData(0, COL_STATUS, "Status");
                c1ChkInPatients.Cols[COL_PATIENTID].Caption = "Patient ID";
                c1ChkInPatients.Cols[COL_DTLAPPOINTMENTID].Caption = "Appointment Detail ID";
                c1ChkInPatients.Cols[COL_nDATE].Caption = "nDate";
                c1ChkInPatients.Cols[COL_nTIME].Caption = "nTime";


                c1ChkInPatients.Cols[COL_SELECT].Visible = true;
                c1ChkInPatients.Cols[COL_APPOINTMENTID].Visible = false;
                c1ChkInPatients.Cols[COL_DATE].Visible = false;
                c1ChkInPatients.Cols[COL_TIME].Visible = true;
                c1ChkInPatients.Cols[COL_TYPE].Visible = true;
                c1ChkInPatients.Cols[COL_PATIENT].Visible = true;
                c1ChkInPatients.Cols[COL_PROVIDER].Visible = true;
                c1ChkInPatients.Cols[COL_LOCATION].Visible = true;
                c1ChkInPatients.Cols[COL_STATUS].Visible = true;
                c1ChkInPatients.Cols[COL_DTLAPPOINTMENTID].Visible = false;
                c1ChkInPatients.Cols[COL_PATIENTID].Visible = false;
                c1ChkInPatients.Cols[COL_nDATE].Visible = false;
                c1ChkInPatients.Cols[COL_nTIME].Visible = false;


                //c1ChkInPatients.Cols[COL_SELECT].DataType = Type.GetType("System.Boolean");
                c1ChkInPatients.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns;

                int width = pnlChkInGrid.Width / 7;
                c1ChkInPatients.Cols[COL_SELECT].Width = (int)(width * 0.3);
                c1ChkInPatients.Cols[COL_APPOINTMENTID].Width = 0;
                c1ChkInPatients.Cols[COL_DATE].Width = 0;
                c1ChkInPatients.Cols[COL_TIME].Width =(int)(width * 0.5);
                c1ChkInPatients.Cols[COL_TYPE].Width = (int)(width * 0.9);
                c1ChkInPatients.Cols[COL_PATIENT].Width = width;
                c1ChkInPatients.Cols[COL_PROVIDER].Width = width + width;
                c1ChkInPatients.Cols[COL_LOCATION].Width = width;
                c1ChkInPatients.Cols[COL_STATUS].Width = width;
                c1ChkInPatients.Cols[COL_DTLAPPOINTMENTID].Width = 0;
                c1ChkInPatients.Cols[COL_PATIENTID].Width = 0;
                c1ChkInPatients.Cols[COL_nDATE].Width = 0;
                c1ChkInPatients.Cols[COL_nTIME].Width = 0;
                      
             

            //DESIGN CHECK IN APPOINTMENT LETTER GRID
              //  c1ApptLetterPatients.Clear();
                c1ApptLetterPatients.DataSource = null;
                c1ApptLetterPatients.Clear(C1.Win.C1FlexGrid.ClearFlags.All);
                c1ApptLetterPatients.Cols.Count = COL_COLCOUNT;
                c1ApptLetterPatients.Rows.Count = 1;
                c1ApptLetterPatients.Cols.Fixed = 0;



                c1ApptLetterPatients.SetData(0, COL_SELECT, "Select");
                c1ApptLetterPatients.SetData(0, COL_APPOINTMENTID, "Appointment ID");
                c1ApptLetterPatients.SetData(0, COL_DATE, "Date");
                c1ApptLetterPatients.SetData(0, COL_TIME, "Time");
                c1ApptLetterPatients.SetData(0, COL_TYPE, "Type");
                c1ApptLetterPatients.SetData(0, COL_PATIENT, "Patient");
                c1ApptLetterPatients.SetData(0, COL_PROVIDER, "Provider");
                c1ApptLetterPatients.SetData(0, COL_LOCATION, "Location");
                c1ApptLetterPatients.SetData(0, COL_STATUS, "Status");
                c1ApptLetterPatients.Cols[COL_PATIENTID].Caption = "Patient ID";
                c1ApptLetterPatients.Cols[COL_DTLAPPOINTMENTID].Caption = "Appointment Detail ID";
                c1ApptLetterPatients.Cols[COL_nDATE].Caption = "nDate";
                c1ApptLetterPatients.Cols[COL_nTIME].Caption = "nTime";

                c1ApptLetterPatients.Cols[COL_SELECT].Visible = true;
                c1ApptLetterPatients.Cols[COL_APPOINTMENTID].Visible = false;
                c1ApptLetterPatients.Cols[COL_DATE].Visible = true;
                c1ApptLetterPatients.Cols[COL_TIME].Visible = true;
                c1ApptLetterPatients.Cols[COL_TYPE].Visible = true;
                c1ApptLetterPatients.Cols[COL_PATIENT].Visible = true;
                c1ApptLetterPatients.Cols[COL_PROVIDER].Visible = true;
                c1ApptLetterPatients.Cols[COL_LOCATION].Visible = true;
                c1ApptLetterPatients.Cols[COL_STATUS].Visible = true;
                c1ApptLetterPatients.Cols[COL_DTLAPPOINTMENTID].Visible = false;
                c1ApptLetterPatients.Cols[COL_PATIENTID].Visible = false;
                c1ApptLetterPatients.Cols[COL_nDATE].Visible = false;
                c1ApptLetterPatients.Cols[COL_nTIME].Visible = false;
            

               // c1ApptLetterPatients.Cols[COL_SELECT].DataType = Type.GetType("System.Boolean");
                c1ApptLetterPatients.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns;

                int colwidth = pnlChkInGrid.Width / 7;
                c1ApptLetterPatients.Cols[COL_SELECT].Width = (int)(colwidth * 0.3);
                c1ApptLetterPatients.Cols[COL_APPOINTMENTID].Width = 0;
                c1ApptLetterPatients.Cols[COL_DATE].Width = (int)(colwidth * 0.5);
                c1ApptLetterPatients.Cols[COL_TIME].Width = (int)(colwidth * 0.5);
                c1ApptLetterPatients.Cols[COL_TYPE].Width = (int)(colwidth * 0.5);
                c1ApptLetterPatients.Cols[COL_PATIENT].Width = colwidth;
                c1ApptLetterPatients.Cols[COL_PROVIDER].Width = colwidth + colwidth;
                c1ApptLetterPatients.Cols[COL_LOCATION].Width = colwidth;
                c1ApptLetterPatients.Cols[COL_STATUS].Width = colwidth;
                c1ApptLetterPatients.Cols[COL_DTLAPPOINTMENTID].Width = 0;
                c1ApptLetterPatients.Cols[COL_PATIENTID].Width = 0;
                c1ApptLetterPatients.Cols[COL_nDATE].Width=0;
                c1ApptLetterPatients.Cols[COL_nTIME].Width = 0;

                            
            
        }

        private void FillTemplate()
        {
           
                trvChkInTemplate.Nodes.Clear();
                gloTemplate ogloTemplate = new gloTemplate(_databaseconnectionstring);

            //code for Appt Letter template tree view
            TreeNode oChkInNode = new TreeNode();
            TreeNode oApptLetterNode = new TreeNode();

            try
            {
                oChkInNode.Text = gloOffice.AssociationCategories.CheckIn.ToString();
                trvChkInTemplate.Nodes.Add(oChkInNode);
                LoadAssociation(gloOffice.AssociationCategories.CheckIn, oChkInNode);
                oApptLetterNode =(TreeNode) oChkInNode.Clone();
                trvApptLetterPrintTemplate.Nodes.Add(oApptLetterNode);

                oChkInNode = new TreeNode();
                oApptLetterNode = new TreeNode();
                oChkInNode.Text = gloOffice.AssociationCategories.AppointmentLetters.ToString();
                trvChkInTemplate.Nodes.Add(oChkInNode);
                LoadAssociation(gloOffice.AssociationCategories.AppointmentLetters, oChkInNode);
                oApptLetterNode = (TreeNode)oChkInNode.Clone();
                trvApptLetterPrintTemplate.Nodes.Add(oApptLetterNode);

                oChkInNode = new TreeNode();
                oApptLetterNode = new TreeNode();
                oChkInNode.Text = "All";
                trvChkInTemplate.Nodes.Add(oChkInNode);
                LoadAssociation(oChkInNode);

                oApptLetterNode = (TreeNode)oChkInNode.Clone();
                trvApptLetterPrintTemplate.Nodes.Add(oApptLetterNode);

                trvChkInTemplate.Nodes[0].Expand();
                trvApptLetterPrintTemplate.Nodes[1].Expand();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (ogloTemplate != null) { ogloTemplate.Dispose(); }
                if (oChkInNode != null) { oChkInNode = null; }
                if (oApptLetterNode != null) { oApptLetterNode = null; }
            }
        }

        private void LoadAssociation(AssociationCategories associationCategory , TreeNode oChkInNode)
       {
           gloOffice.gloTemplate ogloTemplate = new gloTemplate(_databaseconnectionstring);
           DataTable _dtChkInAssociation = null;

           Int64 _categoryId = 0;
           string _TemplateName = "";
           Int64 _templateId = 0;

           try
           {
               _dtChkInAssociation = ogloTemplate.GetAssociation(associationCategory);

               if (_dtChkInAssociation != null && _dtChkInAssociation.Rows.Count > 0)
               {
                  // bool _isfound = false;

                   for (int rowIndex = 0; rowIndex < _dtChkInAssociation.Rows.Count; rowIndex++)
                   {

                       TreeNode _CategoryTemplateNode = new TreeNode();
                       _categoryId = Convert.ToInt64(_dtChkInAssociation.Rows[rowIndex]["nTemplateCategoryID"]);
                       _TemplateName = Convert.ToString(_dtChkInAssociation.Rows[rowIndex]["sTemplateName"]);
                       _templateId = Convert.ToInt64(_dtChkInAssociation.Rows[rowIndex]["nTemplateID"]);
                       //_isfound = false;

                       #region  " Add Template Node to Category Node "

                       if (_templateId > 0)
                       {
                           //Check for adding child node (should not add blank child node): bug no-5189 
                           if (_TemplateName != null && _TemplateName != DBNull.Value.ToString() && _TemplateName.Trim() != "")
                           {
                               _CategoryTemplateNode = new TreeNode();
                               _CategoryTemplateNode.Name = _TemplateName;
                               _CategoryTemplateNode.Text = _TemplateName;
                               _CategoryTemplateNode.Tag = _templateId;
                               _CategoryTemplateNode.ImageIndex = 0;
                               _CategoryTemplateNode.SelectedImageIndex = 0;
                               if (oChkInNode != null) { oChkInNode.Nodes.Add(_CategoryTemplateNode); }
                               _CategoryTemplateNode = null;
                           }
                       }

                       #endregion

                       _categoryId = 0; _TemplateName = ""; _templateId = 0;
                   }
               }

           }
           catch (Exception ex)
           { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true); }
           finally
           {
               if (ogloTemplate != null) { ogloTemplate.Dispose(); }
               if (_dtChkInAssociation != null) { _dtChkInAssociation.Dispose(); }
           }
        }

        private void LoadAssociation(TreeNode oChkInNode)
        {
            gloOffice.gloTemplate ogloTemplate = new gloTemplate(_databaseconnectionstring);
            DataTable _dtChkInAssociation = null;

          //  Int64 _categoryId = 0;
            string _TemplateName = "";
            Int64 _templateId = 0;

            try
            {
                _dtChkInAssociation = ogloTemplate.GetAllTemplates();

                if (_dtChkInAssociation != null && _dtChkInAssociation.Rows.Count > 0)
                {
                    //bool _isfound = false;

                    for (int rowIndex = 0; rowIndex < _dtChkInAssociation.Rows.Count; rowIndex++)
                    {

                        TreeNode _CategoryTemplateNode = new TreeNode();
                        //_categoryId = Convert.ToInt64(_dtChkInAssociation.Rows[rowIndex]["nTemplateCategoryID"]);
                        _TemplateName = Convert.ToString(_dtChkInAssociation.Rows[rowIndex]["sTemplateName"]);
                        _templateId = Convert.ToInt64(_dtChkInAssociation.Rows[rowIndex]["nTemplateID"]);
                        //_isfound = false;

                        #region  " Add Template Node to Category Node "

                        if (_templateId > 0)
                        {
                            //Check for adding child node (should not add blank child node): bug no-5189 
                            if (_TemplateName != null && _TemplateName != DBNull.Value.ToString() && _TemplateName.Trim() != "")
                            {
                                _CategoryTemplateNode = new TreeNode();
                                _CategoryTemplateNode.Name = _TemplateName;
                                _CategoryTemplateNode.Text = _TemplateName;
                                _CategoryTemplateNode.Tag = _templateId;
                                _CategoryTemplateNode.ImageIndex = 0;
                                _CategoryTemplateNode.SelectedImageIndex = 0;
                                if (oChkInNode != null) { oChkInNode.Nodes.Add(_CategoryTemplateNode); }
                                _CategoryTemplateNode = null;
                            }
                        }

                        #endregion

                      //  _categoryId = 0; 
                        _TemplateName = ""; _templateId = 0;
                    }
                }

            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true); }
            finally
            {
                if (ogloTemplate != null) { ogloTemplate.Dispose(); }
                if (_dtChkInAssociation != null) { _dtChkInAssociation.Dispose(); }
            }
        }


        #endregion

        private void cmbApptLetterDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            int _filterby = 0;
            try
            {
                _filterby = cmbApptLetterDateRange.SelectedIndex;
                switch (_filterby)
                {
                    case 0://Date Range
                        FilterBy_DateRange();
                        break;

                    case 1://Today
                        FilterBy_Today();
                        break;

                    case 2://Tomorrow
                        FilterBy_Tomorrow();
                        break;

                    case 3://Yesterday
                        FilterBy_Yesterday();
                        break;

                    case 4://This week
                        FilterBy_Thisweek();
                        break;

                    case 5://Last Week
                        FilterBy_lastweek();
                        break;

                    case 6://Current Month
                        FilterBy_currentmonth();
                        break;

                    case 7://Last Month
                        FilterBy_lastmonth();
                        break;

                    case 8://Current Year
                        FilterBy_currenYear();
                        break;

                    case 9://Last 30 days
                        FilterBy_last30days();
                        break;

                    case 10://Last 60 days
                        FilterBy_last60days();
                        break;

                    case 11://Last 90 days
                        FilterBy_last90days();
                        break;

                    case 12://Last 120 days
                        FilterBy_last120days();
                        break;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void tbBatchPrint_SelectedIndexChanged(object sender, EventArgs e)
        {
           // generateBatch();
        }
     
        private void tsb_GenerateBatch_Click(object sender, EventArgs e)
        {

            generateBatch();
          
        }

        private void c1ApptLetterPatients_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

        private void c1ChkInPatients_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

        private void ts_btnPrint_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            
            //frmWd_PatientTemplate ofrm = null;
            gloTemplate _gloTemplate = null;
            //Changes for the case GLO2010-0007587
            List<gloTemplate> gloTemplates = new List<gloTemplate>();
            List<TemplateDetails> oTemplatesDetails = new List<TemplateDetails>();
            List<PatientDetails> oAllSelectedPatientDetails = new List<PatientDetails>();
            DataTable dtAllPatientWithMultipleAccount = new DataTable();
            bool isAppointmentTab = false;
            bool isMultiAccountPresent = false;
            Boolean isAnySelectedTemplateContainsPatientAccountFields = false;
            //Boolean  isOnlyOnePatientWithAccountTemplateselected=true ;
            List<Int64> oAllSelectedDistinctPatient = new List<Int64>();
           try
            {
              
                if (tbBatchPrint.SelectedTab == tbCheckInPre)
                {
                    isAppointmentTab = false;
                    int count = 0;
                    count = ((c1ChkInPatients.Rows.Count) * (7));
                    pnl_Prgsbar.Visible = true;
                    prgBar_Print.Visible = true;
                    prgBar_Print.Maximum = count;
                    prgBar_Print.Minimum = 0;
                    prgBar_Print.Step = 1;
                    //  prgBar_Print.Value = 0;
                    StringBuilder AllSelectedPatient = new System.Text.StringBuilder(10000);
                    AllSelectedPatient.Clear();
                    PatientDetails SelectedPatientDetails;
                    //gloOffice.Supporting.FromDate = gloDateMaster.gloDate.DateAsNumber(dtpChkInDate.Value.ToString("MM/dd/yyyy"));
                    for (int i = 1; i < c1ChkInPatients.Rows.Count; i++)
                    {
                        if (c1ChkInPatients.GetCellCheck(i, 0) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                        {
                            SelectedPatientDetails.patientID = 0;
                            SelectedPatientDetails.PAccountID = 0;
                            SelectedPatientDetails.AppointmentID = 0;
                            SelectedPatientDetails.IsHaveMultipleAccounts = false;
                            SelectedPatientDetails.AppoinmentTime = "";
                            if (AllSelectedPatient.Length == 0)
                            {
                                AllSelectedPatient.Append(Convert.ToInt64(c1ChkInPatients.Rows[i][COL_PATIENTID]));
                            }
                            else
                            {
                                AllSelectedPatient.Append(",");
                                AllSelectedPatient.Append(Convert.ToInt64(c1ChkInPatients.Rows[i][COL_PATIENTID]));
                            }
                            SelectedPatientDetails.patientID = Convert.ToInt64(c1ChkInPatients.Rows[i][COL_PATIENTID]);
                            SelectedPatientDetails.patientName = Convert.ToString (c1ChkInPatients.Rows[i][COL_PATIENT ]);
                            SelectedPatientDetails.IsHaveMultipleAccounts =false ;
                            SelectedPatientDetails.PAccountID = 0;
                            SelectedPatientDetails.AppointmentID = Convert.ToInt64(c1ChkInPatients.Rows[i][COL_DTLAPPOINTMENTID]);
                            SelectedPatientDetails.AppoinmentTime = Convert.ToString(c1ChkInPatients.Rows[i][COL_TIME]);
                            //Bug #92723: 00001067: Appointment 
                            SelectedPatientDetails.AppointmentDate = gloDateMaster.gloDate.DateAsNumber(dtpChkInDate.Value.ToString("MM/dd/yyyy"));
                            oAllSelectedPatientDetails.Add(SelectedPatientDetails);
                            
                        }
                        
                    }
                    
                    
                    dtAllPatientWithMultipleAccount = GetAllPatientWithMultipleAccount(AllSelectedPatient.ToString());
                    AllSelectedPatient = null;
                    isMultiAccountPresent = (dtAllPatientWithMultipleAccount != null && dtAllPatientWithMultipleAccount.Rows.Count >= 1);
                    gloWord.LoadAndCloseWord myLoadWord = new LoadAndCloseWord();
                    TemplateDetails strtTemplate;
                    for (int j = 0; j < trvChkInTemplate.GetNodeCount(false); j++)
                    {
                        TreeNode oCategoryNode = new TreeNode();
                        oCategoryNode = trvChkInTemplate.Nodes[j];
                        for (int k = 0; k < oCategoryNode.Nodes.Count; k++)
                        {
                            TreeNode oTemplateNode = new TreeNode();
                            oTemplateNode = oCategoryNode.Nodes[k];
                            if (oTemplateNode.Checked == true)
                            {
                                //gloOffice.Supporting.AppointmentID = 0;
                                gloOffice.Supporting.DataBaseConnectionString = _databaseconnectionstring;
                                //gloOffice.Supporting.PatientID = template.PatientID;
                                gloOffice.Supporting.PrimaryID = Convert.ToInt64(oTemplateNode.Tag);;
                                gloOffice.Supporting.VisitID = 0;
                                String fileName = gloOffice.Supporting.GenerateDocumentFile();
                                strtTemplate=new TemplateDetails();
                                strtTemplate.templateID =Convert.ToInt64(oTemplateNode.Tag);
                                strtTemplate.TemplateName = oTemplateNode.Text;
                                strtTemplate.CategoryID = Convert.ToInt64(oCategoryNode.Tag);
                                strtTemplate.CategoryName = oCategoryNode.Text;
                                strtTemplate.TemplateFilePath = fileName;
                                //Boolean IsTemplateContainsPatientAccountFields = CheckContainsPatientAccountFields(ref myLoadWord,fileName);
                                //strtTemplate.IsContainsPatientAccountFields = IsTemplateContainsPatientAccountFields; 
                                Boolean IsTemplateContainsPatientAccountFields = false;
                                //Check patient account field only once if multiple accounts present for any patient
                                if ((isAnySelectedTemplateContainsPatientAccountFields == false) && (isMultiAccountPresent == true))
                                {
                                    IsTemplateContainsPatientAccountFields = CheckContainsPatientAccountFields(ref myLoadWord, fileName);
                                }
                                if(IsTemplateContainsPatientAccountFields)
                                {
                                    isAnySelectedTemplateContainsPatientAccountFields=true ;
                                }
                                oTemplatesDetails.Add(strtTemplate);
                            }
                        }
                    }
                    myLoadWord.CloseApplicationOnly();
                    myLoadWord = null;

                    //New written code for optimization 
                    
                    {
                    }

                    foreach (PatientDetails Patient in oAllSelectedPatientDetails)
                    {
                        Boolean IsPatientHaveMultipleAccounts = false;
                        if (isMultiAccountPresent)
                        {
                            IsPatientHaveMultipleAccounts = CheckPatientHaveMultipleAccounts(Patient.patientID, dtAllPatientWithMultipleAccount);
                        }
                        foreach (TemplateDetails Template in oTemplatesDetails)
                            {
                              _PatientID = Patient.patientID;
                                        if (oAllSelectedDistinctPatient.Contains(_PatientID) == false)
                                        {
                                            oAllSelectedDistinctPatient.Add(_PatientID);
                                        }
                                        _gloTemplate = new gloTemplate(_databaseconnectionstring);
                                        _gloTemplate.AppointmentID = 0;
                                        _gloTemplate.CategoryID = Template.CategoryID;  //Convert.ToInt64(oCategoryNode.Tag);
                                        _gloTemplate.CategoryName = Template.CategoryName;  //oCategoryNode.Text;
                                        _gloTemplate.TemplateID = Template.templateID;  //Convert.ToInt64(oTemplateNode.Tag);
                                        _gloTemplate.TemplateName = Template.TemplateName;  //oTemplateNode.Text;
                                        _gloTemplate.PrimeryID = Patient.AppointmentID;//  Convert.ToInt64(c1ChkInPatients.Rows[i][COL_DTLAPPOINTMENTID]); // Convert.ToInt64(oTemplateNode.Tag);
                                        _gloTemplate.ClinicID = _clinicId;
                                        _gloTemplate.DocumentCategory = 0;
                                        _gloTemplate.PatientID = Patient.patientID;
                                        _gloTemplate.PatientName = Patient.patientName;
                                        _gloTemplate.AppoinmentTime = Patient.AppoinmentTime;
                                        //Bug #92723: 00001067: Appointment 
                                        _gloTemplate.FromDate = Patient.AppointmentDate;
                                        //Boolean IsPatientHaveMultipleAccounts=false ;
                                        //if (dtAllPatientWithMultipleAccount != null && dtAllPatientWithMultipleAccount.Rows.Count >= 1)
                                        //{
                                        //    IsPatientHaveMultipleAccounts = CheckPatientHaveMultipleAccounts(Patient.patientID, dtAllPatientWithMultipleAccount);
                                        //}
                                        _gloTemplate.IsPatientHaveMultipleAccounts = IsPatientHaveMultipleAccounts;
                                        _gloTemplate.IsTemplateContainsPatientAccountFields = isAnySelectedTemplateContainsPatientAccountFields;//Template.IsContainsPatientAccountFields;

                                      
                                        _gloTemplate.TemplateFilePath = Template.TemplateFilePath  ;
                                       //Changes for the case GLO2010-0007587
                                        gloTemplates.Add(_gloTemplate);   
                            }
                        
                    }
                    //foreach(TemplateDetails template in oTemplatesDetails)
                    //{
                    //    try
                    //    {
                    //        System.IO.File.Delete(template.TemplateFilePath);    
                    //    }
                    //    catch (Exception ex)
                    //    {

                    //    }
                    //}

                  }
                else
                {
                    isAppointmentTab = true;

                    int count = 0;
                    DateTime AppointmentDate = new DateTime();
                    count = ((c1ApptLetterPatients.Rows.Count) * (7));

                    pnl_Prgsbar.Visible = true;
                    prgBar_Print.Visible = true;
                    prgBar_Print.Maximum = count;
                    prgBar_Print.Minimum = 0;
                    prgBar_Print.Step = 1;
                    //  prgBar_Print.Value = 0;
                    StringBuilder AllSelectedPatient = new System.Text.StringBuilder(10000);
                    AllSelectedPatient.Clear();
                    PatientDetails SelectedPatientDetails;
                    for (int i = 1; i < c1ApptLetterPatients.Rows.Count; i++)
                    {
                        if (c1ApptLetterPatients.GetCellCheck(i, 0) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                        {
                            SelectedPatientDetails.patientID = 0;
                            SelectedPatientDetails.PAccountID = 0;
                            SelectedPatientDetails.AppointmentID = 0;
                            SelectedPatientDetails.IsHaveMultipleAccounts = false;
                            SelectedPatientDetails.AppoinmentTime = "";
                            if (AllSelectedPatient.Length == 0)
                            {
                                AllSelectedPatient.Append(Convert.ToInt64(c1ApptLetterPatients.Rows[i][COL_PATIENTID]));
                            }
                            else
                            {
                                AllSelectedPatient.Append(",");
                                AllSelectedPatient.Append(Convert.ToInt64(c1ApptLetterPatients.Rows[i][COL_PATIENTID]));
                            }
                            SelectedPatientDetails.patientID = Convert.ToInt64(c1ApptLetterPatients.Rows[i][COL_PATIENTID]);
                            SelectedPatientDetails.patientName = Convert.ToString(c1ApptLetterPatients.Rows[i][COL_PATIENT]);
                            SelectedPatientDetails.IsHaveMultipleAccounts = false;
                            SelectedPatientDetails.PAccountID = 0;
                            SelectedPatientDetails.AppointmentID = Convert.ToInt64(c1ApptLetterPatients.Rows[i][COL_DTLAPPOINTMENTID]);
                            SelectedPatientDetails.AppoinmentTime = Convert.ToString(c1ApptLetterPatients.Rows[i][COL_nTIME]);
                            //Bug #92723: 00001067: Appointment 
                            AppointmentDate = Convert.ToDateTime(c1ApptLetterPatients.Rows[i][COL_DATE]);
                            SelectedPatientDetails.AppointmentDate = gloDateMaster.gloDate.DateAsNumber(AppointmentDate.ToString("MM/dd/yyyy"));
                            oAllSelectedPatientDetails.Add(SelectedPatientDetails);  
                        }
                    }
                    dtAllPatientWithMultipleAccount = GetAllPatientWithMultipleAccount(AllSelectedPatient.ToString());
                    AllSelectedPatient = null;
                    isMultiAccountPresent = (dtAllPatientWithMultipleAccount != null && dtAllPatientWithMultipleAccount.Rows.Count >= 1);
                    TemplateDetails strtTemplate;

                    gloWord.LoadAndCloseWord myLoadWord = new LoadAndCloseWord();
                    for (int j = 0; j < trvApptLetterPrintTemplate.GetNodeCount(false); j++)
                    {
                        TreeNode oCategoryNode = new TreeNode();
                        oCategoryNode = trvApptLetterPrintTemplate.Nodes[j];

                        for (int k = 0; k < oCategoryNode.Nodes.Count; k++)
                        {
                            TreeNode oTemplateNode = new TreeNode();
                            oTemplateNode = oCategoryNode.Nodes[k];
                            if (oTemplateNode.Checked == true)
                            {
                                //_PatientID = Convert.ToInt64(c1ApptLetterPatients.Rows[i][COL_PATIENTID]);

                                //_gloTemplate = new gloTemplate(_databaseconnectionstring);
                                //_gloTemplate.AppointmentID = 0;
                                //_gloTemplate.CategoryID = Convert.ToInt64(oCategoryNode.Tag);
                                //_gloTemplate.CategoryName = oCategoryNode.Text;
                                //_gloTemplate.TemplateID = Convert.ToInt64(oTemplateNode.Tag);
                                //_gloTemplate.TemplateName = oTemplateNode.Text;
                                //_gloTemplate.PrimeryID = Convert.ToInt64(c1ApptLetterPatients.Rows[i][COL_DTLAPPOINTMENTID]); // Convert.ToInt64(oTemplateNode.Tag);
                                //_gloTemplate.ClinicID = _clinicId;
                                //_gloTemplate.DocumentCategory = 0;
                                //_gloTemplate.PatientID = Convert.ToInt64(c1ApptLetterPatients.Rows[i][COL_PATIENTID]);

                                ////Changes for the case GLO2010-0007587
                                //gloTemplates.Add(_gloTemplate);

                                //gloOffice.Supporting.AppointmentID = 0;
                                gloOffice.Supporting.DataBaseConnectionString = _databaseconnectionstring;
                                //gloOffice.Supporting.PatientID = template.PatientID;
                                gloOffice.Supporting.PrimaryID = Convert.ToInt64(oTemplateNode.Tag); ;
                                String fileName = gloOffice.Supporting.GenerateDocumentFile();
                                strtTemplate = new TemplateDetails();
                                strtTemplate.templateID = Convert.ToInt64(oTemplateNode.Tag);
                                strtTemplate.TemplateName = oTemplateNode.Text;
                                strtTemplate.CategoryID = Convert.ToInt64(oCategoryNode.Tag);
                                strtTemplate.CategoryName = oCategoryNode.Text;
                                strtTemplate.TemplateFilePath = fileName;
                                //Boolean IsTemplateContainsPatientAccountFields = CheckContainsPatientAccountFields(ref myLoadWord, fileName);
                                //strtTemplate.IsContainsPatientAccountFields = IsTemplateContainsPatientAccountFields;
                                Boolean IsTemplateContainsPatientAccountFields = false;
                                //Check patient account field only once if multiple accounts present for any patient
                                if ((isAnySelectedTemplateContainsPatientAccountFields == false) && (isMultiAccountPresent == true))
                                {
                                    IsTemplateContainsPatientAccountFields = CheckContainsPatientAccountFields(ref myLoadWord, fileName);
                                }
                                if (IsTemplateContainsPatientAccountFields)
                                {
                                    isAnySelectedTemplateContainsPatientAccountFields = true;
                                }
                                oTemplatesDetails.Add(strtTemplate);
                            }
                            oTemplateNode = null;
                        }
                        oCategoryNode = null;
                    }
                    myLoadWord.CloseApplicationOnly();
                    myLoadWord = null;
                    foreach (PatientDetails Patient in oAllSelectedPatientDetails)
                    {
                        Boolean IsPatientHaveMultipleAccounts = false;
                        if (isMultiAccountPresent)
                        {
                            IsPatientHaveMultipleAccounts = CheckPatientHaveMultipleAccounts(Patient.patientID, dtAllPatientWithMultipleAccount);
                        }
                        foreach (TemplateDetails Template in oTemplatesDetails)
                        {
                            _PatientID = Patient.patientID;
                            if (oAllSelectedDistinctPatient.Contains(_PatientID) == false)
                            {
                                oAllSelectedDistinctPatient.Add(_PatientID);
                            }
                            _gloTemplate = new gloTemplate(_databaseconnectionstring);
                            _gloTemplate.AppointmentID = 0;
                            _gloTemplate.AppoinmentTime = Patient.AppoinmentTime; 
                            _gloTemplate.CategoryID = Template.CategoryID;  //Convert.ToInt64(oCategoryNode.Tag);
                            _gloTemplate.CategoryName = Template.CategoryName;  //oCategoryNode.Text;
                            _gloTemplate.TemplateID = Template.templateID;  //Convert.ToInt64(oTemplateNode.Tag);
                            _gloTemplate.TemplateName = Template.TemplateName;  //oTemplateNode.Text;
                            _gloTemplate.PrimeryID = Patient.AppointmentID;//  Convert.ToInt64(c1ChkInPatients.Rows[i][COL_DTLAPPOINTMENTID]); // Convert.ToInt64(oTemplateNode.Tag);
                            _gloTemplate.ClinicID = _clinicId;
                            _gloTemplate.DocumentCategory = 0;
                            _gloTemplate.PatientID = Patient.patientID;
                            _gloTemplate.PatientName = Patient.patientName;
                            //Bug #92723: 00001067: Appointment 
                            _gloTemplate.FromDate = Patient.AppointmentDate;
                            //Boolean IsPatientHaveMultipleAccounts = false;
                            //if (dtAllPatientWithMultipleAccount != null && dtAllPatientWithMultipleAccount.Rows.Count >= 1)
                            //{
                            //    IsPatientHaveMultipleAccounts = CheckPatientHaveMultipleAccounts(Patient.patientID, dtAllPatientWithMultipleAccount);
                            //}
                            _gloTemplate.IsPatientHaveMultipleAccounts = IsPatientHaveMultipleAccounts;
                            _gloTemplate.IsTemplateContainsPatientAccountFields = isAnySelectedTemplateContainsPatientAccountFields;//Template.IsContainsPatientAccountFields;

                            
                            _gloTemplate.TemplateFilePath = Template.TemplateFilePath;
                            //Changes for the case GLO2010-0007587
                            gloTemplates.Add(_gloTemplate);
                        }

                    }

                    //for (int i = 1; i < c1ApptLetterPatients.Rows.Count; i++)
                    //{
                    //    if (c1ApptLetterPatients.GetCellCheck(i, 0) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                    //    {
                    //        for (int j = 0; j < trvApptLetterPrintTemplate.GetNodeCount(false); j++)
                    //        {
                    //            TreeNode oCategoryNode = new TreeNode();
                    //            oCategoryNode = trvApptLetterPrintTemplate.Nodes[j];

                    //            for (int k = 0; k < oCategoryNode.Nodes.Count; k++)
                    //            {
                    //                TreeNode oTemplateNode = new TreeNode();
                    //                oTemplateNode = oCategoryNode.Nodes[k];
                    //                if (oTemplateNode.Checked == true)
                    //                {
                    //                    _PatientID = Convert.ToInt64(c1ApptLetterPatients.Rows[i][COL_PATIENTID]);

                    //                    _gloTemplate = new gloTemplate(_databaseconnectionstring);
                    //                    _gloTemplate.AppointmentID = 0;
                    //                    _gloTemplate.CategoryID = Convert.ToInt64(oCategoryNode.Tag);
                    //                    _gloTemplate.CategoryName = oCategoryNode.Text;
                    //                    _gloTemplate.TemplateID = Convert.ToInt64(oTemplateNode.Tag);
                    //                    _gloTemplate.TemplateName = oTemplateNode.Text;
                    //                    _gloTemplate.PrimeryID = Convert.ToInt64(c1ApptLetterPatients.Rows[i][COL_DTLAPPOINTMENTID]); // Convert.ToInt64(oTemplateNode.Tag);
                    //                    _gloTemplate.ClinicID = _clinicId;
                    //                    _gloTemplate.DocumentCategory = 0;
                    //                    _gloTemplate.PatientID = Convert.ToInt64(c1ApptLetterPatients.Rows[i][COL_PATIENTID]);

                    //                    //Changes for the case GLO2010-0007587
                    //                    gloTemplates.Add(_gloTemplate);
                    //                }
                    //                oTemplateNode = null;
                    //            }
                    //            oCategoryNode = null;
                    //        }
                    //    }
                    //}

                    #region " Old Code - Commented for the case GLO2010-0007587"

                    //for (int i = 1; i < c1ApptLetterPatients.Rows.Count; i++)
                    //{
                    //    if (c1ApptLetterPatients.GetCellCheck(i, 0) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                    //    {
                    //        for (int j = 0; j < trvApptLetterPrintTemplate.GetNodeCount(false); j++)
                    //        {
                    //            TreeNode oCategoryNode = new TreeNode();
                    //            oCategoryNode = trvApptLetterPrintTemplate.Nodes[j];

                    //            for (int k = 0; k < oCategoryNode.Nodes.Count; k++)
                    //            {
                    //                TreeNode oTemplateNode = new TreeNode();
                    //                oTemplateNode = oCategoryNode.Nodes[k];
                    //                if (oTemplateNode.Checked == true)
                    //                {
                    //                    _PatientID = Convert.ToInt64(c1ApptLetterPatients.Rows[i][COL_PATIENTID]);

                    //                    _gloTemplate = new gloTemplate(_databaseconnectionstring);
                    //                    _gloTemplate.AppointmentID = 0;
                    //                    _gloTemplate.CategoryID = Convert.ToInt64(oCategoryNode.Tag);
                    //                    _gloTemplate.CategoryName = oCategoryNode.Text;
                    //                    _gloTemplate.TemplateID = Convert.ToInt64(oTemplateNode.Tag);
                    //                    _gloTemplate.TemplateName = oTemplateNode.Text;
                    //                    _gloTemplate.PrimeryID = Convert.ToInt64(c1ApptLetterPatients.Rows[i][COL_DTLAPPOINTMENTID]); // Convert.ToInt64(oTemplateNode.Tag);
                    //                    _gloTemplate.ClinicID = _clinicId;
                    //                    _gloTemplate.DocumentCategory = 0;
                    //                    _gloTemplate.PatientID = Convert.ToInt64(c1ApptLetterPatients.Rows[i][COL_PATIENTID]);

                    //                    prgBar_Print.PerformStep();
                    //                    using (frmWd_PatientTemplate ofrm = new frmWd_PatientTemplate(_databaseconnectionstring, _gloTemplate, true))
                    //                    {
                    //                        ofrm.Show();
                    //                    }
                    //                    prgBar_Print.PerformStep();
                    //                }
                    //                oTemplateNode = null;
                    //            }
                    //            oCategoryNode = null;
                    //        }
                    //    }
                    //}

                    #endregion
                }

                //Changes for the case GLO2010-0007587
                try
                {
                    clsPatientTemplate PatientTemplateHelper = new clsPatientTemplate(_databaseconnectionstring);
                    PatientTemplateHelper.ParentForm = this;
                    if (oAllSelectedDistinctPatient.Count == 1)
                    {
                        Boolean IsPatientHaveMultipleAccounts =false ;
                        Int64 AccountID = 0;
                        if (isMultiAccountPresent)
                        {
                            foreach (Int64 PatientID in oAllSelectedDistinctPatient)
                            {
                                IsPatientHaveMultipleAccounts = CheckPatientHaveMultipleAccounts(PatientID, dtAllPatientWithMultipleAccount);
                            }
                            if (IsPatientHaveMultipleAccounts && isAnySelectedTemplateContainsPatientAccountFields)
                            {
                               
                                    frmSelectPatientGuarantor ofrmSelectPatientGuarantor = new frmSelectPatientGuarantor(_PatientID, _clinicId);
                                    clsSelectPatientGuarantor oClsSelectPatientGuarantor = new clsSelectPatientGuarantor(_PatientID, _clinicId);
                                    DataTable dtAccounts = null;
                                    dtAccounts = oClsSelectPatientGuarantor.GetPatientAccounts(_PatientID, _clinicId);
                                    if ((dtAccounts.Rows.Count == 1))
                                    {
                                        AccountID = Convert.ToInt64(dtAccounts.Rows[0]["nPAccountID"].ToString());
                                        
                                    }
                                    else if ((dtAccounts.Rows.Count > 1))
                                    {
                                        ofrmSelectPatientGuarantor.ShowDialog(this);
                                        //aField.Result = ofrmSelectPatientGuarantor.SelectedGuarantor
                                        if (ofrmSelectPatientGuarantor.DialogResult == System.Windows.Forms.DialogResult.OK)
                                        {
                                            AccountID = ofrmSelectPatientGuarantor.SelectedAccount;
                                        }
                                        else
                                        {
                                            AccountID = -1;
                                        }
                                        
                                    }
                                    else
                                    {
                                        AccountID = 0;
                                    }
                                    if ((ofrmSelectPatientGuarantor == null) == false)
                                    {
                                        ofrmSelectPatientGuarantor.Dispose();
                                        ofrmSelectPatientGuarantor = null;
                                    }
                                    if ((oClsSelectPatientGuarantor == null) == false)
                                    {
                                        oClsSelectPatientGuarantor.Dispose();
                                        oClsSelectPatientGuarantor = null;
                                    }
                               

                                //MessageBox.Show("Select Patient Account");
                            }
                        }
                        PatientTemplateHelper.Print(gloTemplates, isAppointmentTab, AccountID, printDocument1);
                      
                    }
                    else
                    {
                     PatientTemplateHelper.Print(gloTemplates, isAppointmentTab,0,printDocument1 );
                     
                    }

                
                }
                catch (System.Reflection.TargetInvocationException ext)
                {
                    MessageBox.Show(ext.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
                //End of Changes for the case GLO2010-0007587
                try
                {
                    //commented for frmBatchPrinting printing
                    //foreach (TemplateDetails Template in oTemplatesDetails)
                    //{
                    //    if (System.IO.File.Exists(Template.TemplateFilePath))
                    //    {
                    //        System.IO.File.Delete(Template.TemplateFilePath);
                    //    }
                    //}
                }

                catch //(Exception ex)
                {
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                //if (_gloTemplate != null) { _gloTemplate.Dispose(); }
                //if (gloTemplates != null) { gloTemplates.Clear(); gloTemplates = null; }
                //if (oTemplatesDetails != null) { oTemplatesDetails.Clear(); oTemplatesDetails = null; }
                //if (oAllSelectedPatientDetails != null) { oAllSelectedPatientDetails.Clear();oAllSelectedPatientDetails=null; 
               // }
             }
        }

        private void ts_btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }
        
        #region "Radio button Event"
        private void rbtnChkInPlan_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnChkInPlan.Checked)
            {
                rbtnChkInPlan.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
                cmbChkInInsurancePlan.Visible = true;
                cmbChkInInsurancePlan.BringToFront();
            }
            else
            {
                rbtnChkInPlan.Font = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
                cmbChkInInsurancePlan.Visible = false;
                cmbChkInInsurancePlan.SendToBack();
            }
        }

        private void rbtnChkInCompany_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnChkInCompany.Checked)
            {
                rbtnChkInCompany.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
                cmbChkInInsuranceCompany.Visible = true;
                cmbChkInInsuranceCompany.BringToFront();
            }
            else
            {
                rbtnChkInCompany.Font = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
                cmbChkInInsuranceCompany.Visible = false;
                cmbChkInInsuranceCompany.SendToBack();
            }
        }

        private void rbtnApptLetterOpen_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnApptLetterOpen.Checked)
                rbtnApptLetterOpen.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            else
                rbtnApptLetterOpen.Font = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);

        }

        private void rbtnApptLetterCancelled_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnApptLetterCancelled.Checked)
                rbtnApptLetterCancelled.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            else
                rbtnApptLetterCancelled.Font = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);

        }

        private void rbtnApptLetterNoShow_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnApptLetterNoShow.Checked)
                rbtnApptLetterNoShow.Font = gloGlobal.clsgloFont.gFont_BOLD;// new Font("Tahoma", 9, FontStyle.Bold);
            else
                rbtnApptLetterNoShow.Font = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);

        }

        private void rbtnApptLetterCompany_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnApptLetterCompany.Checked)
            {
                rbtnApptLetterCompany.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
                cmbApptLetterInsuranceCompany.Visible = true;
                cmbApptLetterInsuranceCompany.BringToFront();
            }
            else
            {
                rbtnApptLetterCompany.Font = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
                cmbApptLetterInsuranceCompany.Visible = false;
                cmbApptLetterInsuranceCompany.SendToBack();
            }
        }

        private void rbtnApptLetterPlan_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnApptLetterPlan.Checked)
            {
                rbtnApptLetterPlan.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
                cmbApptLetterInsurancePlan.Visible = true;
                cmbApptLetterInsurancePlan.BringToFront();
            }
            else
            {
                rbtnApptLetterPlan.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
                cmbApptLetterInsurancePlan.Visible = false;
                cmbApptLetterInsurancePlan.SendToBack();
            }
        }
        #endregion

        #region "SelectAll & ClearAll button event"
        private void btnChkInSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < c1ChkInPatients.Rows.Count; i++)
            {
                 c1ChkInPatients.SetCellCheck(i, 0, C1.Win.C1FlexGrid.CheckEnum.Checked);
            }
            btnChkInSelectAll.Visible = false;
            btnChkInClearAll.Visible = true;

        }
        
        private void btnChkInClearAll_Click(object sender, EventArgs e)
        {
             for (int i = 1; i < c1ChkInPatients.Rows.Count; i++)
            {
                 c1ChkInPatients.SetCellCheck(i, 0, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
            }
             btnChkInClearAll.Visible = false;
             btnChkInSelectAll.Visible = true;
        } 

        private void btnApptLetterSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < c1ApptLetterPatients.Rows.Count; i++)
            {
                 c1ApptLetterPatients.SetCellCheck(i, 0, C1.Win.C1FlexGrid.CheckEnum.Checked);
            }
            btnApptLetterClearAll.Visible = true;
            btnApptLetterSelectAll.Visible = false;
        }

        private void btnApptLetterClearAll_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < c1ApptLetterPatients.Rows.Count; i++)
            {
                 c1ApptLetterPatients.SetCellCheck(i, 0, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
            }
            btnApptLetterClearAll.Visible = false;
            btnApptLetterSelectAll.Visible = true;
        }

        private void btnChkInSelectAllTreeNode_Click(object sender, EventArgs e)
        {
            try
            {
                if (trvChkInTemplate.Nodes != null && trvChkInTemplate.Nodes.Count > 0)
                {                    
                    foreach (TreeNode oNode in trvChkInTemplate.Nodes)
                        { oNode.Checked = true; }
                }
                btnChkInSelectAllTreeNode.Visible = false;
                btnChkInClearAllTreeNode.Visible = true;
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

        private void btnChkInClearAllTreeNode_Click(object sender, EventArgs e)
        {
            try
            {
                if (trvChkInTemplate.Nodes != null && trvChkInTemplate.Nodes.Count > 0)
                {
                   foreach (TreeNode oNode in trvChkInTemplate.Nodes)
                        { oNode.Checked = false; }
                }
                btnChkInClearAllTreeNode.Visible = false;
                btnChkInSelectAllTreeNode.Visible = true;
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }
        
        private void btnApptLetterSelectAllTreeNode_Click(object sender, EventArgs e)
        {
            try
            {
                if (trvApptLetterPrintTemplate.Nodes != null && trvApptLetterPrintTemplate.Nodes.Count > 0)
                {
                    foreach (TreeNode oNode in trvApptLetterPrintTemplate.Nodes)
                        { oNode.Checked = true; }
                }

                btnApptLetterSelectAllTreeNode.Visible = false;
                btnApptLetterClearAllTreeNode.Visible = true;
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

        private void btnApptLetterClearAllTreeNode_Click(object sender, EventArgs e)
        {
            try
            {
                if (trvApptLetterPrintTemplate.Nodes != null && trvApptLetterPrintTemplate.Nodes.Count > 0)
                {
                   foreach (TreeNode oNode in trvApptLetterPrintTemplate.Nodes)
                        { oNode.Checked = false; }
                }

                btnApptLetterClearAllTreeNode.Visible = false;
                btnApptLetterSelectAllTreeNode.Visible = true;
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

        #endregion

        private void trvChkInTemplate_AfterCheck(object sender, TreeViewEventArgs e)
        {
            try
            {                
                    this.trvChkInTemplate.AfterCheck -= new System.Windows.Forms.TreeViewEventHandler(this.trvChkInTemplate_AfterCheck);
                    if (e.Node != null)
                    {
                        if (e.Node.Level == 0)
                        {
                            foreach (TreeNode oChildNode in e.Node.Nodes)
                            {
                                oChildNode.Checked = e.Node.Checked;
                            }
                        }
                        else if (e.Node.Level == 1)
                        {
                            bool _CheckValue = true;

                            foreach (TreeNode oChildNode in e.Node.Parent.Nodes)
                            {
                                if (_CheckValue != oChildNode.Checked)
                                {
                                    _CheckValue = false;
                                    break;
                                }
                            }
                            e.Node.Parent.Checked = _CheckValue;
                        }
                    }
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
               // if (_isTreeLoading == false)
                { this.trvChkInTemplate.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trvChkInTemplate_AfterCheck); }
            }
        }

        private void trvApptLetterPrintTemplate_AfterCheck(object sender, TreeViewEventArgs e)
        {
            try
            {
                
                    this.trvApptLetterPrintTemplate.AfterCheck -= new System.Windows.Forms.TreeViewEventHandler(this.trvApptLetterPrintTemplate_AfterCheck);
                    if (e.Node != null)
                    {
                        if (e.Node.Level == 0)
                        {
                            foreach (TreeNode oChildNode in e.Node.Nodes)
                            {
                                oChildNode.Checked = e.Node.Checked;
                            }
                        }
                        else if (e.Node.Level == 1)
                        {
                            bool _CheckValue = true;

                            foreach (TreeNode oChildNode in e.Node.Parent.Nodes)
                            {
                                if (_CheckValue != oChildNode.Checked)
                                {
                                    _CheckValue = false;
                                    break;
                                }
                            }
                            e.Node.Parent.Checked = _CheckValue;
                        }
                    }
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
               
                { this.trvApptLetterPrintTemplate.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trvApptLetterPrintTemplate_AfterCheck); }
            }
        }
   
        private void c1ChkInPatients_AfterEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            try
            {
                if (FormLoading == false)
                {
                   // bool _UncheckFound = false;
                    for (int i = 1; i < c1ChkInPatients.Rows.Count; i++)
                    {
                        if (c1ChkInPatients.GetCellCheck(i, 0) == C1.Win.C1FlexGrid.CheckEnum.Unchecked)
                        {
                           // _UncheckFound = true;
                            break;
                        }
                    }
                    //if (_UncheckFound == true)
                    //{
                    //    btnSelectAllClearAll.Tag = "Select";
                    //    btnSelectAllClearAll.Text = "Select All";
                    //}
                    //else
                    //{
                    //    btnSelectAllClearAll.Tag = "Clear";
                    //    btnSelectAllClearAll.Text = "Clear All";
                    //}
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void c1ApptLetterPatients_AfterEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            try
            {
                if (FormLoading == false)
                {
                   // bool _UncheckFound = false;
                    for (int i = 1; i < c1ApptLetterPatients.Rows.Count; i++)
                    {
                        if (c1ApptLetterPatients.GetCellCheck(i, 0) == C1.Win.C1FlexGrid.CheckEnum.Unchecked)
                        {
                           // _UncheckFound = true;
                            break;
                        }
                    }
                    //if (_UncheckFound == true)
                    //{
                    //    btnSelectAllClearAll.Tag = "Select";
                    //    btnSelectAllClearAll.Text = "Select All";
                    //}
                    //else
                    //{
                    //    btnSelectAllClearAll.Tag = "Clear";
                    //    btnSelectAllClearAll.Text = "Clear All";
                    //}
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        #endregion

        private void frmBatchPrinting_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (oListControl != null) { oListControl.Dispose(); }
            if (oBindTable != null) { oBindTable = null; }
            if (appSettings != null) { appSettings = null; }
        }
        private DataTable GetAllPatientWithMultipleAccount(String strPatientIDs)
        {

            DataTable  dtAllPatientsWithMultipleAccounts =new DataTable();
            try
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);//(Convert.ToString(appSettings["DatabaseConnectionString"]));
                gloDatabaseLayer.DBParameters oPara = new gloDatabaseLayer.DBParameters();
                oDB.Connect(false);
                oPara.Add("@strPatientIDs", strPatientIDs, ParameterDirection.Input, SqlDbType.VarChar);
                oDB.Retrive("gsp_AllPatientwithMultipleAccounts", oPara, out dtAllPatientsWithMultipleAccounts);
                oPara.Dispose();
                oPara=null;
                oDB.Disconnect();
                oDB.Dispose();
                oDB = null;
 
                return dtAllPatientsWithMultipleAccounts;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                return dtAllPatientsWithMultipleAccounts;
            }
            finally
            {
                dtAllPatientsWithMultipleAccounts.Dispose(); 
            }
        }
        private Boolean CheckContainsPatientAccountFields(ref gloWord.LoadAndCloseWord myLoadWord, String sFilename)
        {
           // Microsoft.Office.Interop.Word.Application wordApplication = default(Microsoft.Office.Interop.Word.Application);
            Boolean IsConytainsAccountFields = false;
            bool toQuit = false;
            try
            {
            //wordApplication = new Microsoft.Office.Interop.Word.Application();
                
                if (myLoadWord == null)
                {
                    myLoadWord = new gloWord.LoadAndCloseWord();
                    toQuit = true;
                }
            //object newTemplate = false;
            //object docType = 0;
            //object isVisible = true;
            //object fileName = sFilename;
            //object missing_new = Type.Missing;
            //object saveOptions = Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges;
            Microsoft.Office.Interop.Word.Document aDoc = myLoadWord.LoadWordApplication(sFilename); //wordApplication.Documents.Add(ref fileName, ref newTemplate, ref docType, ref isVisible);
            if (aDoc != null)
            {
                foreach (Wd.FormField aField in aDoc.FormFields)
                {
                    if (aField.StatusText.StartsWith("pa_accounts.") || aField.StatusText.StartsWith("PA_Accounts_Patients."))
                    {
                        IsConytainsAccountFields = true;
                        break;
                    }
                }
                myLoadWord.CloseWordOnly(ref aDoc);
            }
            //try
            //{
            //    aDoc.Close(ref saveOptions, ref missing_new, ref missing_new);

            //    System.Runtime.InteropServices.Marshal.ReleaseComObject(aDoc);

            //    aDoc = null;
            //}
            //catch
            //{
            //}
            //wordApplication = null;
            //newTemplate = null;
            //docType = null;
            //isVisible = null;
            //fileName = null;
            if (toQuit)
            {
                myLoadWord.CloseApplicationOnly();
                myLoadWord = null;
                //GC.Collect(); // force final cleanup!
                //GC.WaitForPendingFinalizers();
            }

            //wordApplication.Application.Quit(ref saveOptions, ref missing_new, ref missing_new);
            //try
            //{
            //    System.Runtime.InteropServices.Marshal.ReleaseComObject(wordApplication);
            //    wordApplication = null;
            //}
            //catch
            //{
            //}
            //missing_new = null;
            //saveOptions = null;
            aDoc = null;
            return IsConytainsAccountFields;
            }
          catch(Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                return IsConytainsAccountFields;
            }
            finally 
            {
                
   
            }
            
        }
        private Boolean CheckPatientHaveMultipleAccounts(Int64 PatientID, DataTable dtPatientsWithMultipleAccount)
        {
            DataView dv =new DataView();
            try
            {
                dv = dtPatientsWithMultipleAccount.DefaultView;
                dv.RowFilter = "nPatientID= " + PatientID.ToString() + "";
                if (dv.Count >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                ;
            }
                
            catch //(Exception ex)
            {
                return false;
            }
            finally
            {
                //if (dv != null)
                //{
                //    dv.Dispose();
                //    dv = null;
                //}

            }

        }
    }
}
