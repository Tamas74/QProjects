using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using gloSettings;
using gloGlobal;
using System.Collections;



namespace gloPatient
{
    public partial class PatientListControl : UserControl
    {
        #region "Private Variables"

        private Int64 _PatientCount = 0;

        /// <summary>
        /// To get the Patient Count in the DataView after Searching 
        /// </summary>
        private Int64 _VisiblePatientCount = 0;
        private string _ControlHeader = "";
        //Added by Mayuri:20100416-To fix case No:#0004779-System doesn't function correctly when patient list is empty
        //To deselect patient after provider change from combobox
        private bool _IsProviderchanged = false;

        public Int64 PatientCount
        {
            get { return _PatientCount; }
        }

        /// <summary>
        /// To get the No. of Patients which are currently present in the PatientListControl.
        /// This Count is Getting from the Dataview which is Bounded with the Grid.
        /// </summary>
        public Int64 VisiblePatientCount
        {
            get { return _VisiblePatientCount; }
        }

        public Int64 ClinicID
        {
            get { return gloPMGlobal.ClinicID; }
            set { gloPMGlobal.ClinicID = value; }
        }
        ////Added by Mayuri:20100216-Added header to control which is used in Tasks and Appointment Patients retrieval
        //Case No:GLO2008-0001606
        public string ControlHeader
        {
            get { return _ControlHeader; }
            set { _ControlHeader = value; }
        }


        private Int64 _patientid = 0;
        private Int64 _selectedpatientid = 0;
        private string _firstname = "";
        private string _middlename = "";
        private string _lastname = "";
        private string _patientcode = "";

        private Int64 _providerID;
        private string _providerName = "";
        private bool _IsSecurityUser = false;
        DataView dvPatient = null;
        //  DataTable dtSortpatient;
        String Phone_AS;
        String Mobile_AS;
        string EmployersPhone_AS;
        Boolean ISDOB_AS;
        DateTime DOB_AS;
        //  DataTable dtTemp;
        //  DataView dvNext;
        // String gstrPatientCode = "AN001";

        private bool _CheckIsAccess = false;  ///////////////'''''''''Added by Ujwala Atre for 'Lock Chart' Patients - as on 11122010

        private bool _IsControlFilling = false;

        private ToolTip toolTip = null;


        #endregion "Private Variables"

        Timer oTimer = new Timer();
        DateTime _CurrentTime;


        #region "Delegates"

        //////////////// Added by Ujwala Atre for 'Lock Chart' Patients - as on 11122010
        public delegate Boolean CheckIsAccess(object sender, EventArgs e, PatientArgs nPID);
        // public event CheckIsAccess Check_IsAccess;
        //////////////// Added by Ujwala Atre for 'Lock Chart' Patients - as on 11122010

        public delegate void GridRowSelectHandler(object sender, DataGridViewCellEventArgs e);
        public event GridRowSelectHandler GridRowSelect_Click;

        //object sender, MouseEventArgs e
        public delegate void GridMouseDownHandler(object sender, MouseEventArgs e);
        public event GridMouseDownHandler Grid_MouseDown;

        public delegate void GridDoubleClick(object sender, DataGridViewCellEventArgs e);
        public event GridDoubleClick Grid_DoubleClick;

        public delegate void ItemClosed(object sender, EventArgs e);
        public event ItemClosed ItemClosedClick;


        //Sandip Darade 20091022
        //event added to use on  gloEMR50  dashboard
        public delegate void OnProvideChangedHandler(Int64 ProviderID);
        public event OnProvideChangedHandler OnProvideChangedEvent;


        public delegate void onRecpatientClick(Int64 PatientID);
        public event onRecpatientClick onRecpatientEventclick; 


        //Sandip Darade 20100506
        public delegate void OnDatechangeHandler(DateTime dt);
        public event OnDatechangeHandler OnDatechanged;

        //Sandip Darade 20100506
        public delegate void OnCheckchangeHandler(bool checkState, DateTime dt);
        public event OnCheckchangeHandler OnCheckchanged;
        #endregion "Delegates"

        public PatientListControl()
        {

            InitializeComponent();

        }


        #region "Patient Properties"

        public bool IsSecurityUser
        {
            get { return _IsSecurityUser; }
            set { _IsSecurityUser = value; }
        }
        public Int64 ProviderID
        {
            get { return _providerID; }
            set { _providerID = value; }
        }

        public string ProviderName
        {
            get { return _providerName; }
            set { _providerName = value; }
        }

        public Int64 PatientID
        {
            get { return _patientid; }
            set { _patientid = value; }
        }

        public Int64 SelectedPatientID
        {
            get { return _selectedpatientid; }
            set { _selectedpatientid = value; }
        }

        public string FirstName
        {
            get { return _firstname; }
            set { _firstname = value; }
        }

        public string MiddleName
        {
            get { return _middlename; }
            set { _middlename = value; }
        }

        public string LastName
        {
            get { return _lastname; }
            set { _lastname = value; }
        }

        public string PatientCode
        {
            get { return _patientcode; }
            set { _patientcode = value; }
        }


        public string DatabaseConnection
        {
            set { gloPMGlobal.DatabaseConnectionString = value; }
        }

        ///////////////'''''''''Added by Ujwala Atre for 'Lock Chart' Patients - as on 11122010
        public Boolean ChkIsAccess
        {
            get { return _CheckIsAccess; }
            set { _CheckIsAccess = value; }
        }
        ///////////////'''''''''Added by Ujwala Atre for 'Lock Chart' Patients - as on 11122010

        public void ShowOKCancel(bool show)
        {
            //vinayak
            if (show == true)
            {
                pnlToolstrip.Visible = true;
                //pnlCommand.Height = 30;
            }
            else
            {
                pnlToolstrip.Visible = false;
                //pnlCommand.Height = 0;
            }
        }

        public void SetGeneralSearch(bool SetValue)
        {
            chkInstringSearch.Checked = SetValue;
        }

        public bool GetGeneralSearch
        {
            get
            {
                return chkInstringSearch.Checked;
            }
        }
        //Added by Mayuri:20100216-Added header to control which is used in Tasks and Appointment Patients retrieval
        //Case No:GLO2008-0001606
        public void ShowHeader(bool show)
        {

            if (show == true)
            {
                pnlHeader.Visible = true;
            }
            else
            {
                pnlHeader.Visible = false;
            }
        }
        //End code Added by Mayuri:20100216


        public string UserName
        {
            set { gloPMGlobal.UserName = value; }
        }



        #endregion

        private void PatientListControl_Load(object sender, EventArgs e)
        {
            arrSSNColumnNames.Add("SSN".ToLower());
            this.SuspendLayout();
            try
            {

                // Default Show Patients of all Dates
                //dtPatientOn.Value = DateTime.Today;
                dtPatientOn.Enabled = false;
                btnGetAllPatients.Visible = false;
                //
                if (gloPMGlobal.DatabaseConnectionString.Trim() != "")
                {
                    FillProviders();
                }

                oTimer.Tick += new EventHandler(oTimer_Tick);

                gloPM.Classes.gloDataGridViewStyle.DashBoardStyle(dgPatientView);
                txtSearch.Select();  
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            this.ResumeLayout();
           
        }

        void oTimer_Tick(object sender, EventArgs e)
        {
            if (txtSearch.Text.Trim() != "")
            {
                // IF LAST KEY PRESS TIME DIFFERENCE IS 100 MILLISECONDS THEN SEARCHING WILL BE START //
                if (DateTime.Now.Subtract(_CurrentTime).Milliseconds > 100)
                {
                    oTimer.Stop();
                    FillPatients();
                }
            }
            else
            {
                oTimer.Stop();
                FillPatients();
            }

        }

        private void FillProviders()
        {
            try
            {
                DataTable dt;
                dt = gloPMMasters.GetProviders();

                if (dt != null)
                {
                    DataRow dr = dt.NewRow();
                    dr["nProviderID"] = 0;
                    dr["sProviderName"] = "All";
                    dt.Rows.InsertAt(dr, 0);
                    dt.AcceptChanges();

                    this.cmbProviders.SelectedIndexChanged -= new System.EventHandler(this.cmbProviders_SelectedIndexChanged);
                    cmbProviders.DataSource = dt;
                    cmbProviders.ValueMember = dt.Columns["nProviderID"].ColumnName;
                    cmbProviders.DisplayMember = dt.Columns["sProviderName"].ColumnName;
                    cmbProviders.Refresh();
                    cmbProviders.SelectedIndex = -1;

                    _providerID = gloPMGlobal.LoginProviderID;
                    cmbProviders.SelectedValue = _providerID;

                    this.cmbProviders.SelectedIndexChanged += new System.EventHandler(this.cmbProviders_SelectedIndexChanged);
                }
                dt = null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }

        private void cmbProviders_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Variable Added by Mayuri:20100416-To fix case No:#0004779-System doesn't function correctly when patient list is empty
            //To deselect patient after provider change from combobox
            _IsProviderchanged = true;
            picSearch.Visible = true;
            Application.DoEvents();
            FillPatients();
            _IsProviderchanged = false;
            //Sandip Darade 20091022
            //Delegate used in EMR 5.0 
            if (cmbProviders.SelectedIndex > 0)
            {
                ProviderID = Convert.ToInt64(cmbProviders.SelectedValue);

            }
            else
            {
                ProviderID = 0;

            }
            if (OnProvideChangedEvent != null)
            {
                OnProvideChangedEvent(ProviderID);
            }

        }


        #region "Fill Patients Activity"


        /// <summary>
        /// To Fetch the Patients Data from database & Fill it in the Grid 
        /// </summary>
        /// <returns>
        /// returns number of Patients fetched from the Database 
        /// </returns>
        public Int64 FillPatients()
        {
            this.SuspendLayout();
            //dgPatientView.ScrollBars = ScrollBars.None;

            SaveColumnWidth();

            DataTable dtPatient = null;

            _IsControlFilling = true;

            try
            {

               

                #region "Get patient list and Bind to Datagrid view"
                //Added by Mayuri:20100216-Added header to control which is used in Tasks and Appointment Patients retrieval
                //Case No:GLO2008-0001606
                lblHeader.Text = _ControlHeader;
                
                dtPatient = Fill_LastPatients(); //Procedure to fethc data from database.

                if (txtSearch.Text.Trim() != "")
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Query, gloAuditTrail.ActivityType.Search, "Patient Query '" + txtSearch.Text.Trim() + "'", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                }
                
                if (dtPatient != null)
                {
                    dvPatient = dtPatient.DefaultView;
                    dvPatient.Sort = "PatientFirstName, PatientLastName, PatientCode";

                   
                    dgPatientView.DataSource = dvPatient;
                   

                    _PatientCount = dtPatient.Rows.Count;
                    _VisiblePatientCount = dvPatient.Count;
                }//if
                //else: SLR: What to add?
                #endregion

                if (dgPatientView.Columns.Count > 0)
                {

                    //While setting deign properties of DataGridView if no. of row in datasource is less code works faster
                    //i.e. 1. Apply row filter to datasource to have only one row  
                    //     2. Design data grid view
                    //     3. Remove row filter to show all rows

                    //Do not delete (optimization code)
                    if (dgPatientView.DataSource != null)
                    {
                        ((DataView)dgPatientView.DataSource).RowFilter = dvPatient.Table.Columns[9].ColumnName + " = '0'";
                    }
                    #region "Design Grid"

                    dgPatientView.Columns[1].HeaderText = "Code";
                    dgPatientView.Columns[2].HeaderText = "First Name";
                    dgPatientView.Columns[3].HeaderText = "MI";
                    dgPatientView.Columns[4].HeaderText = "Last Name";
                    dgPatientView.Columns[5].HeaderText = "SSN";
                    dgPatientView.Columns[6].HeaderText = "Provider";
                    dgPatientView.Columns[7].HeaderText = "DOB";
                    dgPatientView.Columns[8].HeaderText = "Phone";
                    dgPatientView.Columns[9].HeaderText = "Mobile";
                    dgPatientView.Columns[10].HeaderText = "DOB1";
                    dgPatientView.Columns[11].HeaderText = "Status";


                    //dgPatientView.Columns[1].DisplayIndex = 1;
                    //dgPatientView.Columns[2].DisplayIndex = 2;
                    //dgPatientView.Columns[9].DisplayIndex = 3;
                    //dgPatientView.Columns[3].DisplayIndex = 4;
                    //dgPatientView.Columns[4].DisplayIndex = 5;
                    //dgPatientView.Columns[5].DisplayIndex = 6;
                    //dgPatientView.Columns[6].DisplayIndex = 7;
                    //dgPatientView.Columns[7].DisplayIndex = 8;
                    //dgPatientView.Columns[8].DisplayIndex = 9;

                    dgPatientView.Columns[0].Visible = false;
                    //dgPatientView.Columns[1].Visible = true;
                    //dgPatientView.Columns[2].Visible = true;
                    dgPatientView.Columns[3].Visible = false;
                    //dgPatientView.Columns[4].Visible = true;
                    dgPatientView.Columns[5].Visible = false;
                    dgPatientView.Columns[6].Visible = false;
                    dgPatientView.Columns[7].Visible = false;
                    dgPatientView.Columns[8].Visible = false;
                    dgPatientView.Columns[9].Visible = false;
                    //Added by Mayuri:20100702-To fix issue of sorting DOB column
                    dgPatientView.Columns[10].Visible = false;
                    //Added by Mayuri:20100702-To fix issue of sorting DOB column
                    dgPatientView.Columns[11].Visible = false;

                    DataGridViewCellStyle cellStyle;
                    cellStyle = new DataGridViewCellStyle();
                    cellStyle.Format = "MM'/'dd'/'yyyy";
                    dgPatientView.Columns[7].DefaultCellStyle = cellStyle;
                    #endregion

                    #region "Show Columns as per User patient column Settings"

                    //Get Patient Columns settings for current user.
                    gloSettings.GeneralSettings ogloSettings = new GeneralSettings(gloPMGlobal.DatabaseConnectionString);

                    if (ogloSettings.IsSecurityUser(gloPMGlobal.UserID, gloPMGlobal.ClinicID) == false)
                    {
                        object value = null;
                        ogloSettings.GetSetting("Patient Columns", gloPMGlobal.UserID, gloPMGlobal.ClinicID, out value);

                        if (value != null && Convert.ToString(value).Trim() != "")
                        {
                            string[] PatientColumns = Convert.ToString(value).Trim().Split(',');
                            for (int j = 0; j < PatientColumns.Length; j++)
                            {

                                for (int i = 1; i < dgPatientView.Columns.Count; i++)
                                {
                                    if (dgPatientView.Columns[i].HeaderText.Trim() == PatientColumns[j].Trim())
                                    {
                                        dgPatientView.Columns[i].Visible = true;
                                        break;
                                    }
                                }
                            }
                        }

                    }
                    else
                    {
                        dgPatientView.Columns[2].Visible = false;
                        dgPatientView.Columns[4].Visible = false;
                    }
                    //Display Index settings
                    ogloSettings.LoadGridColumnDisplayIndex(dgPatientView, ModuleOfGridColumn.DashBoardPatientList, gloPMGlobal.UserID);

                    //Column width settings settings
                    if (ogloSettings.LoadGridColumnWidth(dgPatientView, ModuleOfGridColumn.DashBoardPatientList, gloPMGlobal.UserID) == false)
                    {
                        try
                        {
                            dgPatientView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                            int _width = dgPatientView.Width / 19;
                            dgPatientView.Columns[1].Width = _width * 3 / 2;
                            dgPatientView.Columns[2].Width = _width * 4;
                            dgPatientView.Columns[3].Width = _width * 3 / 4;
                            dgPatientView.Columns[4].Width = _width * 4;
                            dgPatientView.Columns[5].Width = _width * 4 / 3;
                            dgPatientView.Columns[6].Width = _width * 3;
                            dgPatientView.Columns[7].Width = _width * 4 / 3;
                            dgPatientView.Columns[8].Width = _width * 4 / 3;
                            dgPatientView.Columns[9].Width = _width * 4 / 3;
                        }
                        catch (Exception) // ex)
                        {
                            //ex.ToString();
                            //ex = null;
                        }
                    }
                    #endregion
                    ogloSettings.Dispose();
                    ogloSettings = null;

                    //Do not delete (optimization code)
                    if (dgPatientView.DataSource != null)
                    {
                        ((DataView)dgPatientView.DataSource).RowFilter = "";
                    }
                    if (dgPatientView.Columns["PatientID"].Visible == true)
                    { dgPatientView.Columns["PatientID"].Visible = false; }


                }

                //Show Lock Chart Patient in RED Color
                for (int i = 0; i < dgPatientView.Rows.Count; i++)
                {
                    if (Convert.ToString(dgPatientView.Rows[i].Cells[11].Value).ToUpper() == "LOCK CHARTS")
                    {
                        dgPatientView.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                        dgPatientView.Rows[i].DefaultCellStyle.SelectionForeColor = Color.Red;
                    }
                }





                #region "Select last Selected patient"

                if (txtSearch.Text.Trim() == "")
                {
                    int RowIndex = -1;
                    if (_selectedpatientid == 0)
                    {
                        if (dtPatient.Rows.Count > 0)
                        {
                            dgPatientView.Rows[0].Selected = true;

                            //To avoid error when there is no space to display records
                            if (dgPatientView.FirstDisplayedScrollingRowIndex >= 0)
                                dgPatientView.FirstDisplayedScrollingRowIndex = 0;

                            _patientid = Convert.ToInt64(dgPatientView[0, 0].Value);
                            _patientcode = Convert.ToString(dgPatientView[1, 0].Value);
                            _firstname = Convert.ToString(dgPatientView[2, 0].Value);
                            //_middlename = "";
                            _middlename = Convert.ToString(dgPatientView[3, 0].Value);
                            _lastname = Convert.ToString(dgPatientView[4, 0].Value);

                        }
                    }
                    else
                    {
                        //Find Last selected patient's rowIndex

                        for (int iRow = 0; iRow <= dgPatientView.Rows.Count - 1; iRow++)
                        {
                            if (Convert.ToInt64(dgPatientView[0, iRow].Value) == _selectedpatientid)
                            {
                                RowIndex = iRow;
                                break;
                            }

                        }

                        //If patient is not present in Database then Select first patient in grid.
                        DataTable dtTemp = GetSinglePatient(_selectedpatientid);
                        if (dtTemp.Rows.Count == 0)
                        {
                            RowIndex = 0;
                        }
                        dtTemp.Dispose();
                        //-------

                        //DataRow []drToSelect = dtPatient.Select("PatientID = " + _selectedpatientid + " ");


                        //if (drToSelect != null && drToSelect.Length > 0)
                        //{
                        //RowIndex = Convert.ToInt32(drToSelect[0]["RowNumber"]);
                        if (RowIndex >= 0 && dgPatientView.Rows.Count > 0)
                        {


                            dgPatientView.Rows[RowIndex].Selected = true;


                            //To avoid error when there is no space to display records
                            if (dgPatientView.FirstDisplayedScrollingRowIndex >= 0)
                                dgPatientView.FirstDisplayedScrollingRowIndex = RowIndex;

                            _patientid = Convert.ToInt64(dgPatientView[0, RowIndex].Value);
                            _patientcode = Convert.ToString(dgPatientView[1, RowIndex].Value);
                            _firstname = Convert.ToString(dgPatientView[2, RowIndex].Value);
                            //_middlename = "";
                            _middlename = Convert.ToString(dgPatientView[3, RowIndex].Value);
                            _lastname = Convert.ToString(dgPatientView[4, RowIndex].Value);
                        }
                        else
                        {
                            SelectPatient(_selectedpatientid);
                        }
                        //Developer: Mitesh Patel
                        //Date:15-Feb-2012'
                        //Bug No: 21106 
                        if (dgPatientView.RowCount > 0)
                        {
                            if (dgPatientView.SelectedRows[0].Index > 0)
                            {
                                if (Convert.ToString(dgPatientView.Rows[dgPatientView.SelectedRows[0].Index].Cells[11].Value).ToUpper() == "LOCK CHARTS")
                                {
                                    dgPatientView.Rows[dgPatientView.SelectedRows[0].Index].DefaultCellStyle.ForeColor = Color.Red;
                                    dgPatientView.Rows[dgPatientView.SelectedRows[0].Index].DefaultCellStyle.SelectionForeColor = Color.Red;
                                }
                            }
                        }

                        //}
                    }//if
                }
                else // IF SEARCHING IS ON, THEN DON'T HIGHLIGHT ROW //
                {
                    if (dgPatientView.Rows.Count > 0)
                        if (dgPatientView.SelectedRows.Count > 0)
                            dgPatientView.Rows[dgPatientView.SelectedRows[0].Index].Selected = false;

                }
                //Added by Mayuri:20100416-To fix case No:#0004779-System doesn't function correctly when patient list is empty
                //To deselect patient after provider change from combobox
                if (_IsProviderchanged == true)
                {
                    if (dgPatientView.Rows.Count > 0)
                    {
                        if (dgPatientView.SelectedRows.Count > 0)
                            dgPatientView.Rows[dgPatientView.SelectedRows[0].Index].Selected = false;
                    }
                }
                #endregion

                _selectedpatientid = _patientid;


                //if (chkInstringSearch.Checked == false)
                //{
                //    GetDefaultSearchColumn();
                //}
                //InstringSearch();

                if (dtPatient != null)
                {
                    //return dtPatient.Rows.Count ;
                    return dtPatient.DefaultView.Count;
                }
                else
                {
                    return 0;
                }

            }//try
            catch (Exception gex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gex.ToString(), true);
                return 0;
            }
            finally
            {
                if (dtPatient != null)
                {
                    dtPatient.Dispose();
                    dtPatient = null;
                }
                dgPatientView.Columns["PatientID"].Visible = false;
                picSearch.Visible = false;
                _IsControlFilling = false;
                //dgPatientView.ScrollBars = ScrollBars.Both;
                this.ResumeLayout();

            }


        }


        private DataTable Fill_LastPatients()
        {
            // Start --------------Show Login Providers Patients---------------
            if (cmbProviders.SelectedIndex > 0)
            {
                ProviderID = Convert.ToInt64(cmbProviders.SelectedValue);
                ProviderName = Convert.ToString(cmbProviders.Text.Trim());
            }
            else
            {
                ProviderID = 0;
                ProviderName = "";
            }
            // End --------------Show Login Providers Patients---------------


            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            gloDatabaseLayer.DBParameter oParameter;

            DataTable dtPatient = null;

            try
            {
                //  dtPatient = new DataTable();


                //oDB.Connect(false);
                //oParameter = new gloDatabaseLayer.DBParameter();
                //oParameter.ParameterName = "@ProviderName";
                //oParameter.ParameterDirection = ParameterDirection.Input;
                //oParameter.DataType = SqlDbType.VarChar;
                ////Check if Login User is provider then get only the list of that providers patients.
                //if (ProviderID > 0 && ProviderName != "")
                //{
                //    oParameter.Value = ProviderName;
                //}
                //else
                //{
                //    oParameter.Value = "All";
                //}

                oDB.Connect(false);
                oParameter = new gloDatabaseLayer.DBParameter();
                oParameter.ParameterName = "@ProviderId";
                oParameter.ParameterDirection = ParameterDirection.Input;
                oParameter.DataType = SqlDbType.BigInt;
                //Check if Login User is provider then get only the list of that providers patients.
                if (ProviderID > 0 && ProviderName != "")
                {
                    oParameter.Value = ProviderID;
                }
                else
                {
                    oParameter.Value = 0;
                }

                oParameters.Add(oParameter);
                oParameter = null;

                oParameter = new gloDatabaseLayer.DBParameter();
                oParameter.ParameterName = "@Location";
                oParameter.ParameterDirection = ParameterDirection.Input;
                oParameter.DataType = SqlDbType.VarChar;
                oParameter.Value = "All";
                oParameters.Add(oParameter);
                oParameter = null;

                //Code added on 10/04/2008 -by Sagar Ghodke for implementing ClinicID;
                //
                oParameter = new gloDatabaseLayer.DBParameter();
                oParameter.ParameterName = "@nClinicID";
                oParameter.ParameterDirection = ParameterDirection.Input;
                oParameter.DataType = SqlDbType.BigInt;
                oParameter.Value = this.ClinicID;
                oParameters.Add(oParameter);
                oParameter = null;
                //

                //
                if (chkPatientOn.Checked == true)
                {
                    oParameter = new gloDatabaseLayer.DBParameter();
                    oParameter.ParameterName = "@SearchDate";
                    oParameter.ParameterDirection = ParameterDirection.Input;
                    oParameter.DataType = SqlDbType.BigInt;
                    oParameter.Value = gloDateMaster.gloDate.DateAsNumber(dtPatientOn.Value.ToShortDateString());
                    oParameters.Add(oParameter);
                    oParameter = null;
                }
                //

                if (txtSearch.Text.Trim() != "")
                {
                    oParameter = new gloDatabaseLayer.DBParameter();
                    oParameter.ParameterName = "@SearchString";
                    oParameter.ParameterDirection = ParameterDirection.Input;
                    oParameter.DataType = SqlDbType.VarChar;
                    oParameter.Value = txtSearch.Text.Trim();
                    oParameters.Add(oParameter);
                    oParameter = null;
                }

                //Added Rahul for IsSecurityUser Setting on 20101117
                if (IsSecurityUser == true)
                {
                    oParameter = new gloDatabaseLayer.DBParameter();
                    oParameter.ParameterName = "@IsSecurityUser";
                    oParameter.ParameterDirection = ParameterDirection.Input;
                    oParameter.DataType = SqlDbType.BigInt;
                    oParameter.Value = 1;
                    oParameters.Add(oParameter);
                    oParameter = null;
                }
                //End

                //CR00000334 :Add new setting for searching patient.
                //Added to get user wise patient search setting.
                //START
                oParameter = new gloDatabaseLayer.DBParameter();
                oParameter.ParameterName = "@UserID";
                oParameter.ParameterDirection = ParameterDirection.Input;
                oParameter.DataType = SqlDbType.BigInt;
                oParameter.Value = gloPMGlobal.UserID;
                oParameters.Add(oParameter);
                oParameter = null;
                //END

                //oDB.Retrive("PA_GetLivePatientsList", oParameters, out  dtPatient);
                oDB.Retrive("PA_GetLivePatientsList", oParameters, out  dtPatient);

                oDB.Disconnect();

                return dtPatient;
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            catch (Exception gex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gex.ToString(), true);
                return null;
            }
            finally
            {
                oDB.Dispose();
                oParameters.Dispose();
            }
        }

        private DataTable GetSinglePatient(Int64 nPatientID)
        {
            // Start --------------Show Login Providers Patients---------------
            if (cmbProviders.SelectedIndex > 0)
            {
                ProviderID = Convert.ToInt64(cmbProviders.SelectedValue);
                ProviderName = Convert.ToString(cmbProviders.Text.Trim());
            }
            else
            {
                ProviderID = 0;
                ProviderName = "";
            }
            // End --------------Show Login Providers Patients---------------


            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            gloDatabaseLayer.DBParameter oParameter;

            DataTable dtPatient;

            try
            {
                dtPatient = new DataTable();


                //oDB.Connect(false);
                //oParameter = new gloDatabaseLayer.DBParameter();
                //oParameter.ParameterName = "@ProviderName";
                //oParameter.ParameterDirection = ParameterDirection.Input;
                //oParameter.DataType = SqlDbType.VarChar;
                ////Check if Login User is provider then get only the list of that providers patients.
                //if (ProviderID > 0 && ProviderName != "")
                //{
                //    oParameter.Value = ProviderName;
                //}
                //else
                //{
                //    oParameter.Value = "All";
                //}

                oDB.Connect(false);
                oParameter = new gloDatabaseLayer.DBParameter();
                oParameter.ParameterName = "@ProviderId";
                oParameter.ParameterDirection = ParameterDirection.Input;
                oParameter.DataType = SqlDbType.BigInt;
                //Check if Login User is provider then get only the list of that providers patients.
                if (ProviderID > 0 && ProviderName != "")
                {
                    oParameter.Value = ProviderID;
                }
                else
                {
                    oParameter.Value = 0;
                }

                oParameters.Add(oParameter);
                oParameter = null;

                oParameter = new gloDatabaseLayer.DBParameter();
                oParameter.ParameterName = "@Location";
                oParameter.ParameterDirection = ParameterDirection.Input;
                oParameter.DataType = SqlDbType.VarChar;
                oParameter.Value = "All";
                oParameters.Add(oParameter);
                oParameter = null;

                //Code added on 10/04/2008 -by Sagar Ghodke for implementing ClinicID;
                //
                oParameter = new gloDatabaseLayer.DBParameter();
                oParameter.ParameterName = "@nClinicID";
                oParameter.ParameterDirection = ParameterDirection.Input;
                oParameter.DataType = SqlDbType.BigInt;
                oParameter.Value = this.ClinicID;
                oParameters.Add(oParameter);
                oParameter = null;
                //

                //
                if (chkPatientOn.Checked == true)
                {
                    oParameter = new gloDatabaseLayer.DBParameter();
                    oParameter.ParameterName = "@SearchDate";
                    oParameter.ParameterDirection = ParameterDirection.Input;
                    oParameter.DataType = SqlDbType.BigInt;
                    oParameter.Value = gloDateMaster.gloDate.DateAsNumber(dtPatientOn.Value.ToShortDateString());
                    oParameters.Add(oParameter);
                    oParameter = null;
                }
                //

                if (txtSearch.Text.Trim() != "")
                {
                    oParameter = new gloDatabaseLayer.DBParameter();
                    oParameter.ParameterName = "@SearchString";
                    oParameter.ParameterDirection = ParameterDirection.Input;
                    oParameter.DataType = SqlDbType.VarChar;
                    oParameter.Value = txtSearch.Text.Trim();
                    oParameters.Add(oParameter);
                    oParameter = null;
                }

                //CR00000334 :Add new setting for searching patient.
                //Added to get user wise patient search setting.
                //START
                oParameter = new gloDatabaseLayer.DBParameter();
                oParameter.ParameterName = "@UserID";
                oParameter.ParameterDirection = ParameterDirection.Input;
                oParameter.DataType = SqlDbType.BigInt;
                oParameter.Value = gloPMGlobal.UserID;
                oParameters.Add(oParameter);
                oParameter = null;
                //END

                oParameter = new gloDatabaseLayer.DBParameter();
                oParameter.ParameterName = "@PatientID";
                oParameter.ParameterDirection = ParameterDirection.Input;
                oParameter.DataType = SqlDbType.BigInt;
                oParameter.Value = nPatientID;
                oParameters.Add(oParameter);
                oParameter = null;

                oDB.Retrive("PA_GetLivePatientsList", oParameters, out  dtPatient);

                oDB.Disconnect();

                return dtPatient;
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            catch (Exception gex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gex.ToString(), true);
                return null;
            }
            finally
            {
                oDB.Dispose();
                oParameters.Dispose();
            }
        }

        //10-Feb-15 Aniket: Resolving patient context lock screen issue
        public bool CheckPatientProvider(long ProviderID, long PatientID)
        {

            object blnCheckPatient = false;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            gloDatabaseLayer.DBParameter oParameter;

            try
            {
                oDB.Connect(false);
                oParameter = new gloDatabaseLayer.DBParameter();
                oParameter.ParameterName = "@ProviderID";
                oParameter.ParameterDirection = ParameterDirection.Input;
                oParameter.DataType = SqlDbType.BigInt;
                oParameter.Value = ProviderID;

                oParameters.Add(oParameter);
                oParameter = null;

                oParameter = new gloDatabaseLayer.DBParameter();
                oParameter.ParameterName = "@PatientID";
                oParameter.ParameterDirection = ParameterDirection.Input;
                oParameter.DataType = SqlDbType.BigInt;
                oParameter.Value = PatientID;

                oParameters.Add(oParameter);
                oParameter = null;

                blnCheckPatient = oDB.ExecuteScalar("gsp_CheckProviderPatient", oParameters);
                return Convert.ToBoolean(blnCheckPatient);
            }



            catch (gloDatabaseLayer.DBException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
            catch (Exception gex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gex.ToString(), true);
                return false;
            }
            finally
            {
                oDB.Dispose();
                oParameters.Dispose();
            }
        }

        private Int64 GetLivePatientCount()
        {
            // Start --------------Show Login Providers Patients---------------
            if (cmbProviders.SelectedIndex > 0)
            {
                ProviderID = Convert.ToInt64(cmbProviders.SelectedValue);
                ProviderName = Convert.ToString(cmbProviders.Text.Trim());
            }
            else
            {
                ProviderID = 0;
                ProviderName = "";
            }
            // End --------------Show Login Providers Patients---------------


            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            gloDatabaseLayer.DBParameter oParameter;

            Object oResult;
            Int64 _Result = 0;

            try
            {

                oDB.Connect(false);
                oParameter = new gloDatabaseLayer.DBParameter();
                oParameter.ParameterName = "@ProviderName";
                oParameter.ParameterDirection = ParameterDirection.Input;
                oParameter.DataType = SqlDbType.VarChar;
                //Check if Login User is provider then get only the list of that providers patients.
                if (ProviderID > 0 && ProviderName != "")
                {
                    oParameter.Value = ProviderName;
                }
                else
                {
                    oParameter.Value = "All";
                }

                oParameters.Add(oParameter);
                oParameter = null;

                oParameter = new gloDatabaseLayer.DBParameter();
                oParameter.ParameterName = "@Location";
                oParameter.ParameterDirection = ParameterDirection.Input;
                oParameter.DataType = SqlDbType.VarChar;
                oParameter.Value = "All";
                oParameters.Add(oParameter);
                oParameter = null;

                //Code added on 10/04/2008 -by Sagar Ghodke for implementing ClinicID;
                //
                oParameter = new gloDatabaseLayer.DBParameter();
                oParameter.ParameterName = "@nClinicID";
                oParameter.ParameterDirection = ParameterDirection.Input;
                oParameter.DataType = SqlDbType.BigInt;
                oParameter.Value = this.ClinicID;
                oParameters.Add(oParameter);
                oParameter = null;
                //

                //
                if (chkPatientOn.Checked == true)
                {
                    oParameter = new gloDatabaseLayer.DBParameter();
                    oParameter.ParameterName = "@searchdate";
                    oParameter.ParameterDirection = ParameterDirection.Input;
                    oParameter.DataType = SqlDbType.BigInt;
                    oParameter.Value = gloDateMaster.gloDate.DateAsNumber(dtPatientOn.Value.ToShortDateString());
                    oParameters.Add(oParameter);
                    oParameter = null;
                }

                oResult = oDB.ExecuteScalar("PA_GetLivePatientCount", oParameters);
                if (oResult != null && Convert.ToString(oResult) != "")
                {
                    _Result = Convert.ToInt64(oResult);
                }

                oDB.Disconnect();

                return _Result;
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return 0;
            }
            catch (Exception gex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gex.ToString(), true);
                return 0;
            }
            finally
            {
                oDB.Dispose();
                oParameters.Dispose();
            }
        }

        private void chkShowAllPatients_CheckedChanged(object sender, EventArgs e)
        {
            FillPatients();

            //if (chkShowAllPatients.Checked == false)
            {

                int RowIndex = dgPatientView.Rows.GetFirstRow(DataGridViewElementStates.Selected);

                _selectedpatientid = 0;
                DataGridViewCellEventArgs erg = new DataGridViewCellEventArgs(0, RowIndex);
                dgPatientView_CellClick(null, erg);
                dgPatientView.Focus();
            }
        }

        public void AddPatientInList(Int64 PatientID)
        {
            // ELSE IF PATIENT NOT PRESENT IN CURRENT LIST, THEN FIND IT IN DATABASE WITH CURRENT FILTERS //
            // IF FOUND FROM DATABASE THEN ADD ONE ROW IN PATIENT LIST AND MAKE THAT ROW AS SELECTED //
            DataTable _dtPatient;
            _dtPatient = GetSinglePatient(PatientID);
            if (_dtPatient != null && _dtPatient.Rows.Count > 0)
            {
                DataRow _Row;
                _Row = dvPatient.Table.NewRow();

                // MAP ROW VALUES //
                for (int iCol = 0; iCol <= _dtPatient.Columns.Count - 1; iCol++)
                {
                    _Row[iCol] = _dtPatient.Rows[0][iCol];
                }

                // ADD ROW TO DATAVIEW //
                dvPatient.Table.Rows.Add(_Row);

                //#region " To Avoid Recursive Function Call "
                //System.Diagnostics.StackTrace oTrace = new System.Diagnostics.StackTrace(1);
                //System.Diagnostics.StackFrame oFrame = oTrace.GetFrame(0);
                //String _MethodName = oFrame.GetMethod().Name;
                //#endregion

                //if (_MethodName != "SelectPatient")
                //{
                //    SelectPatient(PatientID);
                //}
                //return;
                SelectPatient(PatientID);
            }
            //_selectedpatientid = PatientID;
            //this.GridRowSelect_Click(null, null);
        }

        public void SelectPatient(Int64 PatientID)
        {
            try
            {
                if (PatientID == 0)
                {
                    return;
                }
                int _rIndx = -1;

                UnmaskPatient();

                // SEARCH PATIENT IN CURRENT LIST // IF FOUND THEN SELECT THAT ROW //
                for (int i = 0; i <= dgPatientView.RowCount - 1; i++)
                {
                    //Compare the returned value with the binding value and show selected..

                    if (PatientID == System.Convert.ToInt64(dgPatientView.Rows[i].Cells[0].Value))
                    {
                        dgPatientView.Rows[i].Selected = true;
                        _patientid = Convert.ToInt64(dgPatientView[0, i].Value);

                        //To avoid error when there is no space to display records
                        if (dgPatientView.FirstDisplayedScrollingRowIndex >= 0)
                            dgPatientView.FirstDisplayedScrollingRowIndex = i;

                        _rIndx = i;
                        break;
                    }
                }

                if (_rIndx >= 0)
                {
                    _firstname = dgPatientView[2, _rIndx].Value.ToString();
                    //_middlename = "";
                    _middlename = dgPatientView[3, _rIndx].Value.ToString();


                    _lastname = dgPatientView[4, _rIndx].Value.ToString();
                    _patientcode = dgPatientView[1, _rIndx].Value.ToString();
                }
                else
                {
                    // ELSE IF PATIENT NOT PRESENT IN CURRENT LIST, THEN FIND IT IN DATABASE WITH CURRENT FILTERS //
                    // IF FOUND FROM DATABASE THEN ADD ONE ROW IN PATIENT LIST AND MAKE THAT ROW AS SELECTED //
                    DataTable _dtPatient;
                    _dtPatient = GetSinglePatient(PatientID);
                    if (_dtPatient != null && _dtPatient.Rows.Count > 0)
                    {
                        DataRow _Row;
                        _Row = dvPatient.Table.NewRow();

                        // MAP ROW VALUES //
                        for (int iCol = 0; iCol <= _dtPatient.Columns.Count - 1; iCol++)
                        {
                            _Row[iCol] = _dtPatient.Rows[0][iCol];
                        }

                        // ADD ROW TO DATAVIEW //
                        dvPatient.Table.Rows.Add(_Row);

                        #region " To Avoid Recursive Function Call "
                        System.Diagnostics.StackTrace oTrace = new System.Diagnostics.StackTrace(1);
                        System.Diagnostics.StackFrame oFrame = oTrace.GetFrame(0);
                        String _MethodName = oFrame.GetMethod().Name;
                        oFrame = null;
                        oTrace = null;
                        #endregion
                        if (_MethodName != "SelectPatient")
                        {
                            SelectPatient(PatientID);
                        }
                        _dtPatient.Dispose();
                        _dtPatient = null;
                        return;
                    }
                    else
                    {
                        if (_dtPatient != null)
                        {
                            _dtPatient.Dispose();
                            _dtPatient = null;
                        }
                    }
                }

              
                    _selectedpatientid = _patientid;
                    //Added Mayuri :20160801-null condition to resolve #00064536-Fax issue, cant select other patients to send faxes to.
                    if (GridRowSelect_Click != null)
                    {
                        this.GridRowSelect_Click(null, null);
                    }
              
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }
        public void HighlightPatient(Int64 PatientID)
        {
            try
            {
                if (PatientID == 0)
                {
                    return;
                }
                int _rIndx = -1;

                UnmaskPatient();

                // SEARCH PATIENT IN CURRENT LIST // IF FOUND THEN SELECT THAT ROW //
                for (int i = 0; i <= dgPatientView.RowCount - 1; i++)
                {
                    //Compare the returned value with the binding value and show selected..

                    if (PatientID == System.Convert.ToInt64(dgPatientView.Rows[i].Cells[0].Value))
                    {
                        dgPatientView.Rows[i].Selected = true;
                        _patientid = Convert.ToInt64(dgPatientView[0, i].Value);

                        //To avoid error when there is no space to display records
                        if (dgPatientView.FirstDisplayedScrollingRowIndex >= 0)
                            dgPatientView.FirstDisplayedScrollingRowIndex = i;

                        _rIndx = i;
                        break;
                    }
                }

                if (_rIndx >= 0)
                {
                    _firstname = dgPatientView[2, _rIndx].Value.ToString();
                    //_middlename = "";
                    _middlename = dgPatientView[3, _rIndx].Value.ToString();

                    _lastname = dgPatientView[4, _rIndx].Value.ToString();
                    _patientcode = dgPatientView[1, _rIndx].Value.ToString();
                }

                //_selectedpatientid = _patientid;
                //this.GridRowSelect_Click(null, null);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }
        #endregion

        #region "Data Grid View Events"

        private void dgPatientView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {


                if (gloPMGlobal.UserName.Trim() != "")
                {

                    for (int i = 0; i <= dgPatientView.RowCount - 1; i++)
                    {

                        //compare the returned value with the binding value and show selected..
                        if (_patientid == System.Convert.ToInt64(dgPatientView.Rows[i].Cells[0].Value) && txtSearch.Text != "")
                        {

                            dgPatientView.Rows[i].Selected = true;
                            //_patientid = _selectedpatientid;
                            _selectedpatientid = _patientid;
                            dgPatientView.Rows[i].Visible = true;

                            //To avoid error when there is no space to display records
                            if (dgPatientView.FirstDisplayedScrollingRowIndex >= 0)
                                dgPatientView.FirstDisplayedScrollingRowIndex = i;

                            break;
                        }
                        else if (_selectedpatientid == System.Convert.ToInt64(dgPatientView.Rows[i].Cells[0].Value) && txtSearch.Text == "")
                        {

                            dgPatientView.Rows[i].Selected = true;
                            //_patientid = _selectedpatientid;
                            _patientid = _selectedpatientid;
                            dgPatientView.Rows[i].Visible = true;

                            //To avoid error when there is no space to display records
                            if (dgPatientView.FirstDisplayedScrollingRowIndex >= 0)
                                dgPatientView.FirstDisplayedScrollingRowIndex = i;

                            break;
                        }

                    }//for (int i = 0; i <= dgpatientview.rowcount - 1; i++)
                    gloSecurity.gloSecurity oSecurity = new gloSecurity.gloSecurity(gloPMGlobal.DatabaseConnectionString);
                    if (oSecurity.isPatientLock(_patientid, true))
                    {
                        return;
                    }

                    Grid_DoubleClick(sender, e);
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }
        string _Selpatientid = "";//added for maintaining selected record while sorting while sorting bugid 72116
        int ind = -1;
        private void dgPatientView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                //
                if (e.ColumnIndex == 10)
                {
                    DataGridViewColumn dgColForSort = new DataGridViewColumn();
                    //dgColForSort = "PatientDOB1";
                    // ListSortDirection lstdirection = new ListSortDirection();
                    if (dgPatientView.SortOrder == System.Windows.Forms.SortOrder.Ascending)
                    {
                        //dgColForSort = (DataGridViewColumn)dgMasters.SortedColumn.Clone();
                        // lstdirection = ListSortDirection.Ascending;

                        // dgPatientView.Sort(dgPatientView. , ListSortDirection.Descending);
                    }
                    else if (dgPatientView.SortOrder == System.Windows.Forms.SortOrder.Descending)
                    {
                        // lstdirection = ListSortDirection.Descending;
                        dgPatientView.Sort(dgPatientView.SortedColumn, ListSortDirection.Ascending);
                    }
                }
                //
                if (_IsControlFilling == false)
                {

                    if (e.RowIndex >= 0)
                    {

                        if (txtSearch.Text != "")
                        {
                            _patientid = Convert.ToInt64(dgPatientView[0, e.RowIndex].Value);
                            dgPatientView.Rows[e.RowIndex].Selected = true;
                        }
                        else
                        {
                            if (_selectedpatientid == 0)
                            {
                                _patientid = Convert.ToInt64(dgPatientView[0, e.RowIndex].Value);
                                dgPatientView.Rows[e.RowIndex].Selected = true;
                            }
                            else
                            {
                                _patientid = _selectedpatientid;
                            }
                        }
                        //Added Mayuri :20160801-null condition to resolve #00064536-Fax issue, cant select other patients to send faxes to.
                        if (GridRowSelect_Click != null)
                        {
                            this.GridRowSelect_Click(sender, e);
                        }
                        _firstname = dgPatientView[2, e.RowIndex].Value.ToString();
                        //_middlename = "";
                        _middlename = dgPatientView[3, e.RowIndex].Value.ToString();

                        _lastname = dgPatientView[4, e.RowIndex].Value.ToString();
                        _patientcode = dgPatientView[1, e.RowIndex].Value.ToString();

                        _selectedpatientid = _patientid;

                        //gloSecurity.gloSecurity oSecurity = new gloSecurity.gloSecurity(gloPMGlobal.DatabaseConnectionString);
                        //if (oSecurity.isPatientLock(_patientid, true))
                        //{

                        //    return;
                        //}    
                        //MessageBox.Show(" dgPatientView_RowEnter() " + _patientid.ToString() + "  " + _patientcode);
                    }
                }


                if (dgPatientView.RowCount > 0)
                {
                    if (dgPatientView.SelectedRows.Count > 0)
                    {
                        ind = dgPatientView.SelectedRows[0].Index;
                        _Selpatientid = Convert.ToString(dgPatientView.Rows[ind].Cells[0].Value);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void dgPatientView_MouseDown(object sender, MouseEventArgs e)
        {

            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    DataGridView.HitTestInfo ht = dgPatientView.HitTest(e.X, e.Y);
                    if (ht.RowIndex >= 0)
                    {

                        _selectedpatientid = 0;
                        dgPatientView.ClearSelection();
                        //dgPatientView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        //dgPatientView.Select();
                        dgPatientView.Rows[ht.RowIndex].Selected = true;

                        DataGridViewCellEventArgs dverg = new DataGridViewCellEventArgs(ht.ColumnIndex, ht.RowIndex);
                        dgPatientView_CellClick(sender, dverg);

                        EventArgs erg = new EventArgs();

                        //' dgPatientView_Click(sender, erg);

                        this.Grid_MouseDown(sender, e);

                    }
                    else
                    {
                        //dgPatientView.ContextMenuStrip.Hide();
                        this.ContextMenuStrip = null;
                    }
                }

                //if (e.Button == MouseButtons.Left)
                //{
                //    _selectedpatientid = 0;
                //}
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {

            }
        }

        private void dgPatientView_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgPatientView.CurrentRow != null)
                {
                    //Set the selected Patient in grid.
                    _selectedpatientid = Convert.ToInt64(dgPatientView[0, dgPatientView.CurrentRow.Index].Value);
                    _patientid = Convert.ToInt64(dgPatientView[0, dgPatientView.CurrentRow.Index].Value);
                    //MessageBox.Show(" dgPatientView_Click() " + _patientid.ToString() + "  " + Convert.ToInt64(dgPatientView[1, dgPatientView.CurrentRow.Index].Value)); 
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void dgPatientView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //select the patient by Up Down Keys
                if (dgPatientView.RowCount > 0)
                {
                    if (dgPatientView.SelectedRows.Count <= 0)
                        return;

                    switch (e.KeyCode)
                    {
                        case Keys.Enter:
                        case Keys.Down:
                            {
                                int index;
                                index = dgPatientView.SelectedRows[0].Index;
                                if (index < dgPatientView.RowCount - 1)
                                {
                                    dgPatientView.ClearSelection();
                                    index++;
                                    _selectedpatientid = 0;
                                    DataGridViewCellEventArgs erg = new DataGridViewCellEventArgs(0, index);
                                    dgPatientView_CellClick(null, erg);
                                    dgPatientView.Rows[index].Selected = true;
                                    dgPatientView.Rows[index].Visible = true;
                                }
                                //dgPatientView.Focus();
                            }
                            break;
                        case Keys.Up:
                            {
                                int index;
                                index = dgPatientView.SelectedRows[0].Index;
                                if (index > 0)
                                {
                                    index--;

                                    _selectedpatientid = 0;
                                    DataGridViewCellEventArgs erg = new DataGridViewCellEventArgs(0, index);
                                    dgPatientView_CellClick(null, erg);
                                }
                                //dgPatientView.Focus();
                            }

                            break;
                        default:
                            e.SuppressKeyPress = true;
                            break;
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }

        private void dgPatientView_ColumnDisplayIndexChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if (_IsControlFilling == false && dgPatientView.DataSource != null)
            {
                gloSettings.GeneralSettings ogloSettings = new GeneralSettings(gloPMGlobal.DatabaseConnectionString);
                try
                {

                    ogloSettings.SaveGridColumnDisplayIndex(dgPatientView, ModuleOfGridColumn.DashBoardPatientList, gloPMGlobal.UserID);

                }
                catch (Exception) // ex)
                {
                    //ex.ToString();
                    //ex = null;
                }
                finally
                {
                    if (ogloSettings != null) { ogloSettings.Dispose(); }
                }
            }
        }


        #endregion

        #region "Patient List Control Button Methods"

        private void btnGetAllPatients_Click(object sender, EventArgs e)
        {

            try
            {
                txtSearch.Text = "";
                Phone_AS = "";
                Mobile_AS = "";
                EmployersPhone_AS = "";
                ISDOB_AS = false;
                DOB_AS = DateTime.Now;

                if ((dvPatient == null) == false)
                {
                    dvPatient.RowFilter = "";
                }

                int nCount;
                try
                {
                    if (dgPatientView.DataSource != null)
                    {
                        for (nCount = 0; nCount <= ((DataView)dgPatientView.DataSource).Table.Rows.Count - 1; nCount++)
                        {

                            if (dgPatientView.Rows[nCount].Cells[1].Value.ToString() == _patientcode)
                            {

                                dgPatientView.Rows[nCount].Selected = true; ;
                                return; // TODO: might not be correct. Was : Exit Sub 
                            }
                        }
                    }
                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(DBErr.ToString(), true);
                }
                catch (Exception objErr)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(objErr.ToString(), true);
                }
            }

            catch (Exception objErr)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(objErr.ToString(), true);
            }
            finally
            {
            }

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                picSearch.Visible = true;
                if (txtSearch.Text != "")
                    txtSearch.Clear();

                SaveColumnWidth();

                dgPatientView.DataSource = null;
                FillPatients();
            }

            catch (Exception objErr)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(objErr.ToString(), true);
            }
            finally
            {
            }

        }

        private void btnNewPatientReg_Click(object sender, EventArgs e)
        {
            frmSetupPatient frmPatientReg;

            try
            {
                frmPatientReg = new frmSetupPatient(0, gloPMGlobal.DatabaseConnectionString);

                frmPatientReg.ShowDialog(this);
                frmPatientReg.Dispose();
                frmPatientReg = null;
            }

            catch (Exception gex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gex.ToString(), true);
            }
            finally
            {
            }
        }

        // COMMENTED BY SUDHIR 20100123 // 
        //private void btnAdvanceSearch_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string RowFilter = "";
        //        //DataView DVMain;

        //        frmPAAdvanceSearch frm = new frmPAAdvanceSearch();

        //        frm.Phone = Phone_AS;
        //        frm.Mobile = Mobile_AS;
        //        frm.EmployersPhone = EmployersPhone_AS;
        //        frm.ISDOB = ISDOB_AS;
        //        frm.DOB = DOB_AS;

        //        ///' 
        //        frm.ShowInTaskbar = false;
        //        frm.StartPosition = FormStartPosition.CenterParent;
        //        frm.ShowDialog();
        //        //' Set Values For Update 
        //        Phone_AS = frm.Phone;
        //        Mobile_AS = frm.Mobile;
        //        EmployersPhone_AS = frm.EmployersPhone;
        //        ISDOB_AS = frm.ISDOB;
        //        DOB_AS = frm.DOB;

        //        // SUDHIR 20100118 // TO REFETCH PATIENT LIST WHILE SEARCHING // 
        //        //if (dvPatient.Table.Rows.Count != GetLivePatientCount())
        //        //{
        //        //    FillPatients();
        //        //}


        //        dvPatient = (DataView)dgPatientView.DataSource;
        //        //DataView dvPatient1 = new DataView();
        //        //DataTable dt1 = dvPatient.ToTable().Copy();
        //        //dvPatient1
        //        RowFilter = "";
        //        if (frm.Phone != null && frm.Phone != "")
        //        {
        //            RowFilter = RowFilter + dvPatient.Table.Columns["Phone"].ColumnName + "='" + frm.Phone + "'";
        //        }
        //        if (frm.Mobile != null && frm.Mobile != "")
        //        {
        //            if (RowFilter != "")
        //            {
        //                RowFilter = RowFilter + " and ";
        //            }
        //            RowFilter = RowFilter + dvPatient.Table.Columns["Mobile"].ColumnName + "='" + frm.Mobile + "'";
        //        }

        //        if (frm.ISDOB == true && frm.DOB != null && frm.DOB.ToShortDateString() != "")
        //        {
        //            if (RowFilter != "")
        //            {
        //                RowFilter = RowFilter + " and ";
        //            }

        //            string str = "";
        //            if (frm.DOB.Day < 10)
        //            {
        //                str = frm.DOB.ToShortDateString().Insert(2, "0");
        //            }
        //            else
        //            {
        //                str = frm.DOB.ToShortDateString();
        //            }
        //            if (frm.DOB.Month < 10)
        //            {
        //                str = str.Insert(0, "0");
        //            }
        //            RowFilter = RowFilter + dvPatient.Table.Columns["PatientDOB"].ColumnName + "='" + str + "'";
        //        }
        //        if (dvPatient.RowFilter == "")
        //        {
        //            dvPatient.RowFilter = RowFilter;
        //        }
        //        else
        //        {
        //            string str = "(" + dvPatient.RowFilter + ") ";
        //            if (RowFilter != "")
        //            {
        //                str = str + " and ";
        //            }
        //            dvPatient.RowFilter = str + RowFilter;
        //        }
        //        //dvPatient.EndInit();
        //        //dvPatient.RowFilter = dvPatient.RowFilter.ToString()+RowFilter;
        //        dgPatientView.DataSource = dvPatient;

        //        //Guardian Information Search-->>Advance Search
        //        string strwhere = "Select nPatientID from Patient where ";
        //        string strold = strwhere + "";
        //        if (frm.IsGuardianinfo == true || (frm.EmployersPhone != null && frm.EmployersPhone != ""))
        //        {
        //            #region Mother Information

        //            if (frm.MotherLastName != null && frm.MotherLastName != "")
        //            {
        //                if (strwhere == strold)
        //                {
        //                    strwhere = strwhere + "sMother_lName" + "='" + frm.MotherLastName + "'";
        //                }
        //                else
        //                {
        //                    strwhere = strwhere + " and " + " sMother_lName" + "='" + frm.MotherLastName + "'";
        //                }
        //            }
        //            if (frm.MotherFirstName != null && frm.MotherFirstName != "")
        //            {
        //                if (strwhere == strold)
        //                {
        //                    strwhere = strwhere + "sMother_fName" + "='" + frm.MotherFirstName + "'";
        //                }
        //                else
        //                {
        //                    strwhere = strwhere + " and " + " sMother_fName" + "='" + frm.MotherFirstName + "'";
        //                }
        //            }
        //            if (frm.MotherPhoneNo != null && frm.MotherPhoneNo != "")
        //            {
        //                if (strwhere == strold)
        //                {
        //                    strwhere = strwhere + " sMother_Phone" + "='" + frm.MotherPhoneNo + "'";
        //                }
        //                else
        //                {
        //                    strwhere = strwhere + " and " + " sMother_Phone" + "='" + frm.MotherPhoneNo + "'";
        //                }
        //            }
        //            if (frm.MotherCellNo != null && frm.MotherCellNo != "")
        //            {
        //                if (strwhere == strold)
        //                {
        //                    strwhere = strwhere + " sMother_Mobile" + "='" + frm.MotherCellNo + "'";
        //                }
        //                else
        //                {
        //                    strwhere = strwhere + " and " + " sMother_Mobile" + "='" + frm.MotherCellNo + "'";
        //                }
        //            }
        //            #endregion

        //            #region FatherInformation


        //            if (frm.FatherLastName != null && frm.FatherLastName != "")
        //            {
        //                if (strwhere == strold)
        //                {
        //                    strwhere = strwhere + "sFather_lName" + "='" + frm.FatherLastName + "'";
        //                }
        //                else
        //                {
        //                    strwhere = strwhere + " and " + " sFather_lName" + "='" + frm.FatherLastName + "'";
        //                }
        //            }

        //            if (frm.FatherFirstName != null && frm.FatherFirstName != "")
        //            {
        //                if (strwhere == strold)
        //                {
        //                    strwhere = strwhere + "sFather_fName" + "='" + frm.FatherFirstName + "'";
        //                }
        //                else
        //                {
        //                    strwhere = strwhere + " and " + " sFather_fName" + "='" + frm.FatherFirstName + "'";
        //                }
        //            }
        //            if (frm.FatherPhoneNo != null && frm.FatherPhoneNo != "")
        //            {
        //                if (strwhere == strold)
        //                {
        //                    strwhere = strwhere + " sFather_Phone" + "='" + frm.FatherPhoneNo + "'";
        //                }
        //                else
        //                {
        //                    strwhere = strwhere + " and " + " sFather_Phone" + "='" + frm.FatherPhoneNo + "'";
        //                }
        //            }
        //            if (frm.FatherCellNo != null && frm.FatherCellNo != "")
        //            {
        //                if (strwhere == strold)
        //                {
        //                    strwhere = strwhere + " sFather_Mobile" + "='" + frm.FatherCellNo + "'";
        //                }
        //                else
        //                {
        //                    strwhere = strwhere + " and " + " sFather_Mobile" + "='" + frm.FatherCellNo + "'";
        //                }
        //            }
        //            #endregion

        //            #region Employer

        //            if (frm.EmployersPhone != null && frm.EmployersPhone != "")
        //            {
        //                if (strwhere == strold)
        //                {
        //                    strwhere = strwhere + " sWorkPhone" + "='" + frm.EmployersPhone + "'";
        //                }
        //                else
        //                {
        //                    strwhere = strwhere + " and " + " sWorkPhone" + "='" + frm.EmployersPhone + "'";
        //                }
        //            }

        //            #endregion

        //            gloDatabaseLayer.DBLayer ODB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
        //            ODB.Connect(false);
        //            DataTable dt = new DataTable();
        //            if (strwhere != strold)
        //            {
        //                ODB.Retrive_Query(strwhere, out dt);
        //            }

        //            DataTable dtPatient = new DataTable();
        //            string strPatientID = "";
        //            //strPatientID = strPatientID + " and ";
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                if (strPatientID == "")
        //                {
        //                    strPatientID = "(PatientID=" + strPatientID + dt.Rows[i]["nPatientID"].ToString();
        //                }
        //                else
        //                {
        //                    strPatientID = strPatientID + " or " + "PatientID=" + dt.Rows[i]["nPatientID"].ToString();
        //                }
        //            }
        //            if (dt.Rows.Count > 0)
        //            {
        //                strPatientID += ")";
        //                if (dvPatient.RowFilter == "")
        //                    dvPatient.RowFilter = strPatientID;
        //                else
        //                    dvPatient.RowFilter += " and " + strPatientID;
        //            }
        //            else
        //            {
        //                dvPatient.RowFilter = "";
        //            }
        //            dgPatientView.DataSource = dvPatient;


        //        }
        //        if (dgPatientView.Rows.Count >= 1)
        //        {
        //            for (int i = 0; i < dgPatientView.Rows.Count; i++)
        //            {
        //                dgPatientView.Rows[i].Selected = false;
        //            }
        //        }
        //    }


        //    catch (Exception objErr)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(objErr.ToString(), true);
        //    }
        //    finally
        //    {
        //    }
        //}

        private void btnAdvanceSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsSecurityUser == false)
                {
                    frmPAAdvanceSearch frm = new frmPAAdvanceSearch();

                    frm.Phone = Phone_AS;
                    frm.Mobile = Mobile_AS;
                    frm.EmployersPhone = EmployersPhone_AS;
                    frm.ISDOB = ISDOB_AS;
                    frm.DOB = DOB_AS;

                    ///' 
                    frm.ShowInTaskbar = false;
                    frm.StartPosition = FormStartPosition.CenterParent;
                    if (frm.ShowDialog(this) == DialogResult.OK)
                    {
                        DataTable _dtPatients = new DataTable();
                        DataTable _dtPatient;
                        DataRow _Row;
                        _dtPatients = frm.FilteredPatients;
                        if (_dtPatients != null)
                        {
                            dvPatient.Table.Rows.Clear();

                            for (int iRow = 0; iRow <= _dtPatients.Rows.Count - 1; iRow++)
                            {
                                _dtPatient = GetSinglePatient(Convert.ToInt64(_dtPatients.Rows[iRow][0]));
                                if (_dtPatient != null && _dtPatient.Rows.Count > 0)
                                {
                                    _Row = dvPatient.Table.NewRow();

                                    // MAP ROW VALUES //
                                    for (int iCol = 0; iCol <= _dtPatient.Columns.Count - 1; iCol++)
                                    {
                                        _Row[iCol] = _dtPatient.Rows[0][iCol];
                                    }

                                    // ADD ROW TO DATAVIEW //
                                    dvPatient.Table.Rows.Add(_Row);
                                }
                            }

                            dgPatientView.DataSource = dvPatient;
                        }

                    }
                    frm.Dispose();
                    frm = null;
                }
            }


            catch (Exception objErr)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(objErr.ToString(), true);
            }
            finally
            {
            }
        }

        //btnAdvSearch_click

        #endregion

        #region "Search Patient"

        public void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // THIS CONDITION WILL OCCURE IF TEXT IS PASTED IN SEARCH BOX //
                if (oTimer.Enabled == false)
                {
                    oTimer.Stop();
                    oTimer.Enabled = true;
                    picSearch.Visible = true;
                }

                this.Cursor = Cursors.Default;
            }//try
            catch (Exception objErr)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(objErr.ToString(), true);
            }
            finally
            {
            }
        }

        private void chkInstantSearch_CheckedChanged(object sender, EventArgs e)
        {

            //if (chkInstringSearch.Checked == true)
            //{

            //    lblSearch.Text = "Search";
            //}
            //else
            //{
            //    GetDefaultSearchColumn();
            //}
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                // THIS LOGIC WILL START SEARCH AFTER 700 MILLISECONDS // 
                // SEARCH IS IMPLEMENTED ON TIMER TICK //
                _CurrentTime = DateTime.Now;
                oTimer.Stop();
                oTimer.Interval = 700;
                oTimer.Enabled = true;
                picSearch.Visible = true;

                if (e.KeyCode == Keys.Enter)
                {
                    //select the searched patient
                    if (dgPatientView.RowCount > 0 && txtSearch.Text.Trim() != "")
                    {
                        _selectedpatientid = 0;
                        DataGridViewCellEventArgs erg = new DataGridViewCellEventArgs(0, 0);

                        // Select The 0th Index i.e 1st row of Patient 
                        DataGridViewCell newCell;
                        newCell = dgPatientView.Rows[0].Cells[dgPatientView.CurrentCell.ColumnIndex];
                        dgPatientView.CurrentCell = newCell;
                        //

                        dgPatientView_CellClick(null, erg);
                        dgPatientView.Focus();
                    }
                }
            }//try
            catch (Exception objErr)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(objErr.ToString(), true);
            }
            finally
            {
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            //_CurrentTime = DateTime.Now;
            //oTimer.Interval = 1000;
            //oTimer.Enabled = true;
        }


        #endregion "Search Patient"

        #region "Show Selected Columns"

        public DataTable GetSelectedColumns()
        {
            DataTable dtSetting = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            //string Result = "";
            try
            {
                oDB.Connect(false);
                String strQuery = "SELECT sSettingsName,sSettingsValue FROM Settings WHERE  sSettingsName = 'Patient Column'";
                oDB.Retrive_Query(strQuery, out dtSetting);

            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.Message);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return dtSetting;
        }
        #endregion

        #region "Internal Functions & Procedures"

        private bool IsNum(object Expression)
        {
            //function to check whether the string entered is numeric or not
            //char ch;

            try
            {

                // Variable to collect the Return value of the TryParse method.
                bool isNum;

                // Define variable to collect out parameter of the TryParse method. If the conversion fails, the out parameter is zero.
                double retNum;

                // The TryParse method converts a string in a specified style and culture-specific format to its double-precision floating point number equivalent.
                // The TryParse method does not generate an exception if the conversion fails. If the conversion passes, True is returned. If it does not, False is returned.
                isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
                return isNum;


            }

            catch (Exception objErr)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(objErr.ToString(), true);
                return false;
            }
            finally
            {
            }

        }

        private bool IsDate(object inValue)
        {
            //function to check whether the value passed is a valid datetime value or not
            bool bValid;
            try
            {
                DateTime myDT = DateTime.Parse(inValue.ToString());
                bValid = true;

                return bValid;
            }
            catch (FormatException e)
            {
                e.ToString();
                e = null;
                bValid = false;
                return bValid;
            }
            catch (Exception ex)
            {
                ex.ToString();
                ex = null;
                bValid = false;
                return bValid;
            }
            finally
            {

            }


        }

        private string GetRowFilter(string RowFilter)
        {
            try
            {
                if (RowFilter == "")
                {
                    return RowFilter;
                }
                else
                {
                    return RowFilter + " AND ";
                }
            }

            catch (Exception gex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gex.ToString(), true);

                return "";
            }
        }

        #endregion

        #region "Mouse Events"
        private void btnCancel_MouseHover(object sender, EventArgs e)
        {
            //vinayak
            //btnCancel.BackgroundImage = global::gloPatient.Properties.Resources.Img_ButtonHover;
            //btnCancel.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnCancel_MouseLeave(object sender, EventArgs e)
        {
            //vinayak
            //btnCancel.BackgroundImage = global::gloPatient.Properties.Resources.Img_Button;
            //btnCancel.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnOK_MouseHover(object sender, EventArgs e)
        {
            //vinayak
            //btnOK.BackgroundImage = global::gloPatient.Properties.Resources.Img_ButtonHover;
            //btnOK.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnOK_MouseLeave(object sender, EventArgs e)
        {
            //vinayak
            //btnOK.BackgroundImage = global::gloPatient.Properties.Resources.Img_Button;
            //btnOK.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnAdvanceSearch_MouseHover(object sender, EventArgs e)
        {
            btnAdvanceSearch.BackgroundImage = global::gloPatient.Properties.Resources.Img_ButtonHover;
            btnAdvanceSearch.BackgroundImageLayout = ImageLayout.Stretch;
            if (toolTip == null)
            {
                toolTip = new ToolTip();
            }
            toolTip.SetToolTip(this.btnAdvanceSearch, "Advance Search");
        }

        private void btnAdvanceSearch_MouseLeave(object sender, EventArgs e)
        {
            btnAdvanceSearch.BackgroundImage = global::gloPatient.Properties.Resources.Img_Button;
            btnAdvanceSearch.BackgroundImageLayout = ImageLayout.Stretch;
            //toolTip = null;
        }

        private void btnRefresh_MouseHover(object sender, EventArgs e)
        {
            btnRefresh.BackgroundImage = global::gloPatient.Properties.Resources.Img_ButtonHover;
            btnRefresh.BackgroundImageLayout = ImageLayout.Stretch;

            if (toolTip == null)
            {
                toolTip = new ToolTip();
            }
            toolTip.SetToolTip(this.btnRefresh, "Refresh");
        }

        private void btnRefresh_MouseLeave(object sender, EventArgs e)
        {
            btnRefresh.BackgroundImage = global::gloPatient.Properties.Resources.Img_Button;
            btnRefresh.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnGetAllPatients_MouseHover(object sender, EventArgs e)
        {
            btnGetAllPatients.BackgroundImage = global::gloPatient.Properties.Resources.Img_ButtonHover;
            btnGetAllPatients.BackgroundImageLayout = ImageLayout.Stretch;

            if (toolTip == null)
            {
                toolTip = new ToolTip();
            }
            toolTip.SetToolTip(this.btnGetAllPatients, "All Paient");
        }

        private void btnGetAllPatients_MouseLeave(object sender, EventArgs e)
        {
            btnGetAllPatients.BackgroundImage = global::gloPatient.Properties.Resources.Img_Button;
            btnGetAllPatients.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnNewPatientReg_MouseHover(object sender, EventArgs e)
        {
            btnNewPatientReg.BackgroundImage = global::gloPatient.Properties.Resources.Img_ButtonHover;
            btnNewPatientReg.BackgroundImageLayout = ImageLayout.Stretch;

            if (toolTip == null)
            {
                toolTip = new ToolTip();
            }
            toolTip.SetToolTip(this.btnNewPatientReg, "New Patient");
        }

        private void btnNewPatientReg_MouseLeave(object sender, EventArgs e)
        {
            btnNewPatientReg.BackgroundImage = global::gloPatient.Properties.Resources.Img_Button;
            btnNewPatientReg.BackgroundImageLayout = ImageLayout.Stretch;


        }

        #endregion "Mouse Events"

        private void chkPatientOn_CheckedChanged(object sender, EventArgs e)
        {
            if (_IsSecurityUser == false)
            {
                picSearch.Visible = true;
                Application.DoEvents();
                if (chkPatientOn.Checked == true)
                {
                    chkPatientOn.Refresh();

                    btnRefresh_Click(sender, e);
                    dtPatientOn.Visible = true;
                    dtPatientOn.Enabled = true;
                }
                else
                {
                    chkPatientOn.Refresh();

                    btnRefresh_Click(sender, e);
                    dtPatientOn.Enabled = false;
                }
                //Sandip darade 20100506
                if (OnCheckchanged != null)
                {
                    OnCheckchanged(Convert.ToBoolean(chkPatientOn.CheckState), dtPatientOn.Value);
                }
            }
            //Sandip Darade 20090523
            //InstringSearch();
        }

        private void dtPatientOn_ValueChanged(object sender, EventArgs e)
        {
            btnRefresh_Click(sender, e);
            //Sandip darade 20100506
            if (OnDatechanged != null)
            {
                OnDatechanged(dtPatientOn.Value);
            }
        }

        public void SaveColumnWidth()
        {
            if (_IsControlFilling == false && dgPatientView.DataSource != null)
            {
                gloSettings.GeneralSettings ogloSettings = new GeneralSettings(gloPMGlobal.DatabaseConnectionString);
                try
                {
                    ogloSettings.SaveGridColumnWidth(dgPatientView, ModuleOfGridColumn.DashBoardPatientList, gloPMGlobal.UserID);
                }
                catch (Exception) // ex)
                {
                    //ex.ToString();
                    //ex = null;
                }
                finally
                {
                    if (ogloSettings != null) { ogloSettings.Dispose(); }
                }
            }
        }

        // Incident #00017037 : Patient Context Issue resolved
        /// <summary>
        /// To Select the Provider on the dashboard, Next to Search box.
        /// </summary>
        public void SelectProvider(Int64 ProviderID = 0)
        {
            try
            {
                if (ProviderID == 0)
                {
                    // Select the Logged-in Provider
                    cmbProviders.SelectedValue = gloPMGlobal.LoginProviderID;
                }
                else
                {
                    // Select the provider which is passed to the function 
                    if (Convert.ToInt64(cmbProviders.SelectedValue) != 0)
                    {
                        cmbProviders.SelectedValue = ProviderID;
                    }
                    else
                    {
                        //Bug #58060: 00000549 : Provider Refresh Issue on Dashboard
                        //Description: Unable to refresh patient list when dashboard > right click > change provider when combo box value is "All".
                        //Resolution: Call FillPatient() if combo box value is "All".
                         FillPatients();
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }


        public void ClearSearch()
        {
            if (txtSearch.Text != "")
            {
                txtSearch.Clear();
                FillPatients();
            }
        }

        private void dgPatientView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //Mask Unmask Patients
        private void btnMask_Click(object sender, EventArgs e)
        {
            try
            {
                if (_IsSecurityUser == false)
                {
                    if (dvPatient != null)
                    {
                        Int32 Index = 0;
                        //If Patient Row is selected from the list
                        if (dgPatientView.SelectedRows.Count > 0)
                        {
                            //Get Selected Row Index
                            Index = dgPatientView.SelectedRows[0].Index;

                            //Apply filter to show only selected row
                            MaskPatient(Convert.ToInt64(dgPatientView.Rows[Index].Cells[0].Value));
                        }
                        else
                        {
                            //If Patient no Row is selected from the list
                            MaskPatient(_selectedpatientid);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }

        private void btnUnmask_Click(object sender, EventArgs e)
        {
            try
            {
                UnmaskPatient();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        public void MaskPatient(Int64 PatientID)
        {
            try
            {
                if (dvPatient != null)
                {
                    dvPatient.RowFilter = dvPatient.Table.Columns[0].ColumnName + " = " + PatientID + " ";
                    dgPatientView.DataSource = dvPatient;
                    _VisiblePatientCount = dvPatient.Count;

                    btnMask.Visible = false;
                    btnUnmask.Visible = true;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        public void UnmaskPatient()
        {
            try
            {
                //Show All Patient as per search string
                //InstringSearch();
                if (dvPatient != null && dvPatient.RowFilter != "")
                {
                    dvPatient.RowFilter = "";
                    dgPatientView.DataSource = dvPatient;
                    _VisiblePatientCount = dvPatient.Count;
                    for (int iRow = 0; iRow <= dgPatientView.Rows.Count - 1; iRow++)
                    {

                        if (_selectedpatientid == System.Convert.ToInt64(dgPatientView.Rows[iRow].Cells[0].Value))
                        {
                            dgPatientView.Rows[iRow].Selected = true;

                            //To avoid error when there is no space to display records
                            if (dgPatientView.FirstDisplayedScrollingRowIndex >= 0)
                                dgPatientView.FirstDisplayedScrollingRowIndex = iRow;

                            break;
                        }
                    }

                }
                btnMask.Visible = true;
                btnUnmask.Visible = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void pnlPatientON_MouseDown(object sender, MouseEventArgs e)
        {
            this.ContextMenuStrip = null;
        }

        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "Save":
                        {
                            dgPatientView_CellDoubleClick(null, null);
                            break;
                        }
                    case "Close":
                        {
                            tsbtn_Close_Click(null, null);
                            //ItemClosedClick(sender,e);
                            //this.Visible = false;
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }

        private void tsbtn_Close_Click(object sender, EventArgs e)
        {
            ItemClosedClick(sender, e);
            this.Visible = false;
        }



        private void btnUnmask_MouseLeave(object sender, EventArgs e)
        {
            btnUnmask.BackgroundImage = global::gloPatient.Properties.Resources.Img_Button;
            btnUnmask.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnUnmask_MouseHover(object sender, EventArgs e)
        {
            btnUnmask.BackgroundImage = global::gloPatient.Properties.Resources.Img_ButtonHover;
            btnUnmask.BackgroundImageLayout = ImageLayout.Stretch;

            if (toolTip == null)
            {
                toolTip = new ToolTip();
            }
            toolTip.SetToolTip(this.btnNewPatientReg, "Unmask");
        }

        private void btnMask_MouseHover(object sender, EventArgs e)
        {
            btnMask.BackgroundImage = global::gloPatient.Properties.Resources.Img_ButtonHover;
            btnMask.BackgroundImageLayout = ImageLayout.Stretch;

            if (toolTip == null)
            {
                toolTip = new ToolTip();
            }
            toolTip.SetToolTip(this.btnNewPatientReg, "Mask");
        }

        private void btnMask_MouseLeave(object sender, EventArgs e)
        {
            btnMask.BackgroundImage = global::gloPatient.Properties.Resources.Img_Button;
            btnMask.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnSearchClose_Click(object sender, EventArgs e)
        {
            txtSearch.ResetText();
            txtSearch.Focus();

            if (toolTip == null)
            {
                toolTip = new ToolTip();
            }
            toolTip.SetToolTip(this.btnSearchClose, "Clear Search");
        }



        private void dgPatientView_Sorted(object sender, EventArgs e)
        {

            try
            {
                //Show Lock Chart Patient in RED Color
                for (int i = 0; i < dgPatientView.Rows.Count; i++)
                {
                    if (Convert.ToString(dgPatientView.Rows[i].Cells[11].Value).ToUpper() == "LOCK CHARTS")
                    {
                        dgPatientView.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                        dgPatientView.Rows[i].DefaultCellStyle.SelectionForeColor = Color.Red;
                    }
                    if (Convert.ToString(dgPatientView.Rows[i].Cells[0].Value) == _Selpatientid) //added to maintain selected record while sorting ,Bug #72116
                    {
                        dgPatientView.Rows[i].Selected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }


        }


        /// <summary>
        /// This function will check whether the Patient is in the grid or not.
        /// </summary>
        /// <param name="PatientID">PatientID which needs be check </param>
        /// <returns>True if found else False</returns>
        public bool IsPatientInList(Int64 PatientID)
        {
            bool isPatientInList = false;
            // SEARCH PATIENT IN CURRENT LIST // IF FOUND THEN SELECT THAT ROW //
            for (int i = 0; i <= dgPatientView.RowCount - 1; i++)
            {
                //Compare the returned value with the binding value and show selected..
                if (PatientID == System.Convert.ToInt64(dgPatientView.Rows[i].Cells[0].Value))
                {
                    isPatientInList = true;
                    break;
                }
            }
            return isPatientInList;
        }


        public class PatientArgs : EventArgs
        {//////////////// Added by Ujwala Atre for 'Lock Chart' Patients - as on 11122010
            public long nPatientID { get; set; }
        }


        public void RemovePatientFocusInGrid()
        {
            try
            {
                if (dgPatientView != null && dgPatientView.Rows.Count > 0)
                {
                    if (dgPatientView.SelectedRows.Count > 0)
                    {
                        dgPatientView.Rows[dgPatientView.SelectedRows[0].Index].Selected = false;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        ArrayList arrSSNColumnNames = new ArrayList();
        private void dgPatientView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (arrSSNColumnNames.Contains(dgPatientView.Columns[e.ColumnIndex].HeaderText.ToLower()))
            {
                if (e.Value != null && e.Value.ToString() != "")
                {
                    if (!(dgPatientView.CurrentCell != null && dgPatientView.IsCurrentCellInEditMode && dgPatientView.CurrentCell.RowIndex == e.RowIndex && dgPatientView.CurrentCell.ColumnIndex == e.ColumnIndex))
                    {
                            e.Value = e.Value.ToString().Replace("-", "");
                            e.Value = "*****" + e.Value.ToString().Substring(e.Value.ToString().Length - Math.Min(e.Value.ToString().Length,4));
                            e.FormattingApplied = true;
                        
                    }
                }
            }
        }

        private void btnrecpat_Click(object sender, EventArgs e)
        {
            frmvwRecentPatient objfrmrecpat = new frmvwRecentPatient(gloPMGlobal.DatabaseConnectionString, gloPMGlobal.UserID);
      
         objfrmrecpat.onRecpatientEventclick+=new frmvwRecentPatient.onRecpatientClick(SelectPatientOnDashboard); 
        objfrmrecpat.ShowDialog(this);
         objfrmrecpat.onRecpatientEventclick-=new frmvwRecentPatient.onRecpatientClick(SelectPatientOnDashboard); 
        objfrmrecpat.Dispose();
        objfrmrecpat = null;
        }

        private void SelectPatientOnDashboard(Int64 SelectedPatientID)
        {
            if (onRecpatientEventclick != null)
            {
                onRecpatientEventclick(SelectedPatientID);
            }
        }

       
    }

}//namespace gloPatient






