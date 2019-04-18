using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace gloPatient
{
    public partial class gloSmartPatientControl : UserControl
    {
        //private string _MessageBoxCaption = "gloPM";
        private string _MessageBoxCaption = String.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        private string _DatabaseConnectionString = "";

        private Int64 _id = 0;
        private string _code = "";
        private string _firstname = "";
        private string _lastname = "";
    



        private DataTable _dtList = new DataTable();
        private int _thiswidth = 0;
        private Int64 _ClinicID = 0;
        
        private DataView _dvList;
        private bool _FillInProcess = false;

        public delegate void ItemSelected(object sender, EventArgs e);
        public event ItemSelected ItemSelectedClick;

        public delegate void ItemClosed(object sender, EventArgs e);
        public event ItemClosed ItemClosedClick;

        public delegate void SearchText(object sender, EventArgs e);
       // public event SearchText SearchTextChanged;

        public gloSmartPatientControl(int ControlWidth,string DatabaseConnectionString)
        {
           
            InitializeComponent();

            if (ControlWidth <= 0)
            { _thiswidth = this.Width; }
            else
            { _thiswidth = ControlWidth; }
            _DatabaseConnectionString = DatabaseConnectionString;


            //Added By Pramod Nair For Messagebox Caption 
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

        public void LoadPatients()
        {
            try
            {
                FillListAsCriteria();
                if (dgListView != null)
                {
                    if (dgListView.RowCount > 0)
                    {
                        dgListView.Columns[0].Visible = false; 
                        //lblSearch.Text = dgListView.Columns[1].HeaderText.ToString();
                        //GetDefaultSearchColumn();
                        dgListView.ReadOnly = true;
                    }
                    
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }

        #region "propertis"
            public Int64 ClinicID
            {
                get { return _ClinicID; }
                set { _ClinicID = value; }
            }
            public Int64 PatientID
            {
                get { return _id; }
                set { _id = value; }
            }

            public string PatientCode
            {
                get { return _code; }
                set { _code = value; }
            }

            public string PatientFullName
            {
                get { return _firstname + " " + _lastname; }
            }

            public string PatientFirstName
            {
                get { return _firstname; }
                set { _firstname = value; }
            }

            public string PatientLastName
            {
                get { return _lastname; }
                set { _lastname = value; }
            }
        #endregion

        #region "Fill List Activity"

        public void FillListAsCriteria()
        {
            _FillInProcess = true;
            dgListView.Visible = false; 
            DataTable _dtList = new DataTable();
           
            if (_DatabaseConnectionString.Trim() != "")
            {
                string _sqlQuery = "";
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
                oDB.Connect(false);

                //Commented By Pramod Nair
                //_sqlQuery = "SELECT Patient.nPatientID AS PatientID,Patient.sPatientCode AS PatientCode, " +
                //            " Patient.sFirstName as PatientFirstName,Patient.sLastName AS PatientLastName, " +
                //            " Convert(Varchar(10),Patient.nSSN) AS SSNNo,Convert(varchar,Patient.dtDOB,101) as PatientDOB " +
                //            "FROM Patient ORDER BY Patient.sFirstName";

                //Sanjog 2011 Jan 14 to remove the space in provider name
                _sqlQuery = " SELECT DISTINCT Patient.nPatientID AS PatientID,Patient.sPatientCode AS PatientCode,  "
                                  + " Patient.sFirstName as PatientFirstName,ISNULL(Patient.sMiddleName,'') AS PatientMiddleName,Patient.sLastName AS PatientLastName,   "
                                  + " dbo.formatPhone(Convert(Varchar(10),Patient.nSSN),1) AS SSNNo,     "
                                  + " isnull(Provider_MST.sFirstName,'')+space(1)+	CASE Provider_MST.sMiddleName WHEN  '' THEN ''When Provider_MST.sMiddleName then  Provider_MST.sMiddleName + SPACE(1) END +isnull(Provider_MST.sLastName,'') AS Provider,    "
                                  + " convert(varchar,Patient.dtDOB,101) as PatientDOB, dbo.FormatPhone(sPhone,0) as Phone,dbo.formatPhone(ISNULL(sMobile,''),0) AS Mobile   "
                                  + " FROM Patient LEFT OUTER JOIN Provider_MST ON Patient.nProviderID = Provider_MST.nProviderID ";

                if (ClinicID > 0)
                {
                    _sqlQuery = _sqlQuery + " WHERE Patient.nClinicID = " + ClinicID + "  ORDER BY PatientFirstName,PatientLastName ";
                }
                else
                {
                    _sqlQuery = _sqlQuery + "  ORDER BY PatientFirstName,PatientLastName ";
                }

                try
                {
                    if (_sqlQuery.Trim() != "")
                    {
                        oDB.Retrive_Query(_sqlQuery, out _dtList);
                    }


                    if (_dtList != null)
                    {
                        if (_dtList.Rows.Count > 0)
                        {
                            _dvList = _dtList.DefaultView;
                            dgListView.DataSource = _dvList;
                            //dgListView.Update();

                            //((DataView)dgListView.DataSource).RowFilter = _dvList.Table.Columns[0] + " = '0'";

                            //commented by Pramod Nair
                            //dgListView.Columns[0].HeaderText = "Id";
                            //dgListView.Columns[1].HeaderText = "Code";
                            //dgListView.Columns[2].HeaderText = "First Name";
                            //dgListView.Columns[3].HeaderText = "Last Name";
                            //dgListView.Columns[4].HeaderText = "SSN";
                            //dgListView.Columns[5].HeaderText = "DOB";

                            //dgListView.Columns[0].Visible = false;
                            //dgListView.Columns[1].Visible = true;
                            //dgListView.Columns[2].Visible = true;
                            //dgListView.Columns[3].Visible = true;
                            //dgListView.Columns[4].Visible = true;
                            //dgListView.Columns[5].Visible = true;


                            //Added By Pramod To Show Columns as per User patient column Settings -2009-05-28
                            dgListView.Columns[0].HeaderText = "Id";
                            dgListView.Columns[1].HeaderText = "Code";
                            dgListView.Columns[2].HeaderText = "First Name";
                            dgListView.Columns[3].HeaderText = "MI";
                            dgListView.Columns[4].HeaderText = "Last Name";
                            dgListView.Columns[5].HeaderText = "SSN";
                            dgListView.Columns[6].HeaderText = "Provider";
                            dgListView.Columns[7].HeaderText = "DOB";
                            dgListView.Columns[8].HeaderText = "Phone";
                            dgListView.Columns[9].HeaderText = "Mobile";


                            dgListView.Columns[0].Visible = false;
                            dgListView.Columns[3].Visible = false;
                            dgListView.Columns[5].Visible = false;
                            dgListView.Columns[6].Visible = false;
                            dgListView.Columns[7].Visible = false;
                            dgListView.Columns[8].Visible = false;
                            dgListView.Columns[9].Visible = false;
                            


                            //Added By Pramod Nair To Show Columns as per User patient column Settings -2009-05-28
                            //Get login user ID from appsettings   
                            Int64 _nUserID = 0;
                            if (appSettings["UserID"] != null)
                            {
                                if (appSettings["UserID"] != "")
                                { _nUserID = Convert.ToInt64(appSettings["UserID"]); }
                                else { _nUserID = 0; }
                            }
                            else
                            { _nUserID = 0; }


                            //Get Patient Columns settings for current user.
                            gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_DatabaseConnectionString);
                            object value = new object();
                            ogloSettings.GetSetting("Patient Columns", _nUserID, _ClinicID, out value);

                            if (value != null && Convert.ToString(value).Trim() != "")
                            {
                                string[] PatientColumns = Convert.ToString(value).Trim().Split(',');
                                for (int j = 0; j < PatientColumns.Length; j++)
                                {

                                    for (int i = 1; i < dgListView.Columns.Count; i++)
                                    {
                                        if (dgListView.Columns[i].HeaderText.Trim() == PatientColumns[j].Trim())
                                        {
                                            dgListView.Columns[i].Visible = true;
                                            break;
                                        }
                                    }
                                }
                            }

                            //Display Index settings
                            ogloSettings.LoadGridColumnDisplayIndex(dgListView, gloSettings.ModuleOfGridColumn.DashBoardPatientList, _nUserID);
                            //End
                            ogloSettings.Dispose();
                            ogloSettings = null;
                            try
                            {
                                //int _width = _thiswidth - 10;
                                //dgListView.Columns[0].Width = 0;
                                //dgListView.Columns[1].Width = Convert.ToInt32(_width * 0.2);// _width * 1;
                                //dgListView.Columns[2].Width = Convert.ToInt32(_width * 0.2);
                                //dgListView.Columns[3].Width = Convert.ToInt32(_width * 0.2);
                                //dgListView.Columns[4].Width = Convert.ToInt32(_width * 0.2);
                                //dgListView.Columns[5].Width = Convert.ToInt32(_width * 0.2);


                                //Added By Pramod Nair To Show Columns as per User patient column Settings -2009-05-28
                                dgListView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                                int _width = dgListView.Width / 13;
                                dgListView.Columns[1].Width = _width * 4;
                                dgListView.Columns[2].Width = _width * 4;
                                dgListView.Columns[3].Width = _width * 1;
                                dgListView.Columns[4].Width = _width * 4;
                                dgListView.Columns[5].Width = _width * 2;
                                dgListView.Columns[6].Width = _width * 3;
                                dgListView.Columns[7].Width = _width * 2;
                                dgListView.Columns[8].Width = _width * 2;
                                dgListView.Columns[9].Width = _width * 2;
                            }
                            catch (Exception) // ex)
                            {
                                //ex.ToString();
                                //ex = null;
                            }
                            //((DataView)dgListView.DataSource).RowFilter = "";

                        }
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

                }
                finally
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oParameters.Dispose();
                    dgListView.Visible = true; 
                }
            }
          
            _FillInProcess = false;
        }

        #endregion

        #region "Data Grid View Events"
        private void dgListView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_FillInProcess == false)
            {
                if (e.RowIndex != -1)
                {
                    btnSelect_Click(null, null);
                }
            }
        }

        private void dgListView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (_FillInProcess == false)
            {
                if (e.RowIndex >= 0)
                {
                    _id = Convert.ToInt64(dgListView[0, e.RowIndex].Value);
                    _code = dgListView[1, e.RowIndex].Value.ToString().Trim();
                    _firstname = dgListView[2, e.RowIndex].Value.ToString().Trim();
                    _lastname = dgListView[4, e.RowIndex].Value.ToString().Trim();
                }
            }
        }

        #endregion

        #region "List Control Button Methods"

        private void btnSelect_Click(object sender, EventArgs e)
        {
            dgListView.EndEdit();
            ItemSelectedClick(sender, e);
            this.Visible = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            _id = 0;
            _code = "";
            _firstname = "";
            _lastname = "";
            ItemClosedClick(sender, e);
            this.Visible = false;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                //fill the patients
                FillListAsCriteria();
                int nCount;
                try
                {
                    if ( dgListView.DataSource != null)
                    {
                        if (((DataView)dgListView.DataSource).Table.Rows.Count > 0)
                        {
                            for (nCount = 0; nCount <= ((DataView)dgListView.DataSource).Table.Rows.Count - 1; nCount++)
                            {

                                if (dgListView.Rows[nCount].Cells[0].Value.ToString().Trim() != "")
                                {
                                    if (Convert.ToInt64(dgListView.Rows[nCount].Cells[0].Value) == _id)
                                    {
                                        dgListView.Rows[nCount].Selected = true;
                                        dgListView.Select();

                                        return; // TODO: might not be correct. Was : Exit Sub 
                                    }
                                }

                            }
                        }
                    }
                }

                catch (Exception gex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gex.ToString(), true);

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
                objErr.ToString();
                objErr = null;
                MessageBox.Show(objErr.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                e = null; bValid = false;
                return bValid;
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
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

            catch (Exception)// gex)
            {
                //gex.ToString();
                //gex = null;
                return "";
            }
        }

        #endregion

        public void GetDefaultSearchColumn()
        {
            DataTable dtSetting = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
            //string Result = "";
            try
            {
                oDB.Connect(false);
                String strQuery = "SELECT sSettingsName,sSettingsValue FROM Settings WHERE  sSettingsName = 'Patient Search Column'";
                oDB.Retrive_Query(strQuery, out dtSetting);
                if (dtSetting.Rows.Count > 0)
                {
                    lblSearch.Text = Convert.ToString(dtSetting.Rows[0]["sSettingsValue"]);
                    if (lblSearch.Text.Trim() == "")
                        lblSearch.Text = "Code";
                }
                else
                {
                    lblSearch.Text = "Code";
                }
                //Added By Pramod Nair
                lblSearch.Text = "Search:";

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
                if (dtSetting != null)
                {
                    dtSetting.Dispose();
                    dtSetting = null;
                }
            }
        }

        private void chkGeneralSearch_CheckedChanged(object sender, EventArgs e)
        {
            if (chkGeneralSearch.Checked == true)
            {
                lblSearch.Text = "Search";
            }
            else
            {
                GetDefaultSearchColumn();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (_FillInProcess == false)
                {
                    //Added By Pramod
                    chkGeneralSearch.Checked = true;
                    if (chkGeneralSearch.Checked == false)
                        SimpleSearch();
                    else
                        GeneralSearch();
                }
            }
            catch (Exception) // Ex)
            {
                //Ex.ToString();
                //Ex = null;
            }
        }

        private void GeneralSearch()
        {
            try
            {
                //int index = 1;

                DataView _dv = (DataView)dgListView.DataSource;
                dgListView.DataSource = _dv;
                if (_dv == null)
                {
                    return;
                }
                //string strSearch = txtSearch.Text.Trim();
                //strSearch.Replace("'", "''");

                //////Commented By Pramod Nair
                //////dv.RowFilter = dv.Table.Columns[1].ColumnName + " Like '%" + strSearch + "%' OR " + 
                //////                dv.Table.Columns[2].ColumnName + " Like '%" + strSearch + "%' OR " +
                //////                dv.Table.Columns[3].ColumnName + " Like '%" + strSearch + "%'";


                ////Added By Pramod For Instant Search
                //string strFilter = "";
                //for (int i = 1; i < dv.Table.Columns.Count; i++)
                //{
                //    if (dv.Table.Columns[i].DataType == typeof(System.String))
                //    {
                //        if (strFilter != "")
                //        {
                //            strFilter += " OR ";
                //        }
                //        strFilter += dv.Table.Columns[i].ColumnName + " Like '%" + strSearch + "%'";
                //    }
                //}
                //dv.RowFilter = strFilter;

                //Added By MaheshB
                      string[] strSearchArray = null;
                        string sFilter = "";
                        string strSearch = txtSearch.Text.Trim();

                        //strSearch = strSearch.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("%", "").Replace("*", "");

                        //Added By Mukesh Patel for instring search  2009-09-04
                        strSearch = strSearch.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("*", "%");
                        if (strSearch.Length > 1)
                        {
                            string str = strSearch.Substring(1).Replace("%", "");
                            strSearch = strSearch.Substring(0, 1) + str;
                        }

                        if (strSearch.Trim() != "")
                        {
                            strSearchArray = strSearch.Split(',');
                        }



                        if (strSearch.Trim() != "")
                        {

                            if (strSearchArray.Length == 1)
                            {
                                strSearch = strSearchArray[0].Trim();
                                _dv.RowFilter = _dv.Table.Columns["PatientCode"].ColumnName + " Like '%" + strSearch + "%' OR " +
                                                _dv.Table.Columns["PatientFirstName"].ColumnName + " Like '%" + strSearch + "%' OR " +
                                                _dv.Table.Columns["PatientMiddleName"].ColumnName + " Like '%" + strSearch + "%' OR " +
                                                _dv.Table.Columns["PatientLastName"].ColumnName + " Like '%" + strSearch + "%' OR " +
                                                _dv.Table.Columns["SSNNO"].ColumnName + " Like '%" + strSearch + "%' OR " +
                                                _dv.Table.Columns["Provider"].ColumnName + " Like '%" + strSearch + "%' OR " +
                                                _dv.Table.Columns["PatientDOB"].ColumnName + " Like '%" + strSearch + "%' OR " +
                                                _dv.Table.Columns["Phone"].ColumnName + " Like '%" + strSearch + "%'";
                            }
                            else
                            {
                                //For Comma separated  value search
                                sFilter = "";
                                for (int i = 0; i < strSearchArray.Length; i++)
                                {
                                    strSearch = strSearchArray[i].Trim();
                                    if (strSearch.Trim() != "")
                                    {
                                        if (i == 0)
                                        {
                                            sFilter = " ( " + _dv.Table.Columns["PatientCode"].ColumnName + " Like '%" + strSearch + "%' OR " +
                                                _dv.Table.Columns["PatientFirstName"].ColumnName + " Like '%" + strSearch + "%' OR " +
                                                _dv.Table.Columns["PatientMiddleName"].ColumnName + " Like '%" + strSearch + "%' OR " +
                                                _dv.Table.Columns["PatientLastName"].ColumnName + " Like '%" + strSearch + "%' OR " +
                                                _dv.Table.Columns["SSNNO"].ColumnName + " Like '%" + strSearch + "%' OR " +
                                                _dv.Table.Columns["Provider"].ColumnName + " Like '%" + strSearch + "%' OR " +
                                                _dv.Table.Columns["PatientDOB"].ColumnName + " Like '%" + strSearch + "%' OR " +
                                                _dv.Table.Columns["Phone"].ColumnName + " Like '%" + strSearch + "%')";
                                        }
                                        else
                                        {
                                            if (sFilter != "")
                                                sFilter = sFilter + " AND ";

                                            sFilter = sFilter + " (" + _dv.Table.Columns["PatientCode"].ColumnName + " Like '%" + strSearch + "%' OR " +
                                                _dv.Table.Columns["PatientFirstName"].ColumnName + " Like '%" + strSearch + "%' OR " +
                                                _dv.Table.Columns["PatientMiddleName"].ColumnName + " Like '%" + strSearch + "%' OR " +
                                                _dv.Table.Columns["PatientLastName"].ColumnName + " Like '%" + strSearch + "%' OR " +
                                                _dv.Table.Columns["SSNNO"].ColumnName + " Like '%" + strSearch + "%' OR " +
                                                _dv.Table.Columns["Provider"].ColumnName + " Like '%" + strSearch + "%' OR " +
                                                _dv.Table.Columns["PatientDOB"].ColumnName + " Like '%" + strSearch + "%' OR " +
                                                _dv.Table.Columns["Phone"].ColumnName + " Like '%" + strSearch + "%')";
                                        }

                                    }
                                }
                                _dv.RowFilter = sFilter;
                            }

                        }
                        else
                        {
                            _dv.RowFilter = "";
                        }
                        dgListView.DataSource = _dv;
                        }
      
                        catch(Exception) // ex)
                        {
                            //ex.ToString();
                            //ex = null;
                        }
            }
       

        private void SimpleSearch()
        {
            int index = 1;

            switch (lblSearch.Text.Trim())
            {
                case "Code":
                    index = 1;
                    break;
                case "First Name":
                    index = 2;
                    break;
                case "Last Name":
                    index = 3;
                    break;
                default:
                    lblSearch.Text = "Code";
                    index = 1;
                    break;
            }//switch               


            DataView dv = (DataView)dgListView.DataSource;
            dgListView.DataSource = dv;
            if (dv == null)
            {
                return;
            }
            string strSearch = txtSearch.Text.Trim();
            strSearch.Replace("'", "''");

            if (strSearch.StartsWith("%") == true | strSearch.StartsWith("*") == true)
            {
                dv.RowFilter = dv.Table.Columns[index].ColumnName + " Like '%" + strSearch + "%'";

            }
            else
            {
                dv.RowFilter = dv.Table.Columns[index].ColumnName + " Like '" + strSearch + "%'";
            }
            dgListView.DataSource = dv;

            //Added By Pramod Nair
            lblSearch.Text = "Search:";
            
        }

        private void dgListView_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (_FillInProcess == false)
            {
                if (e.ColumnIndex == 1 || e.ColumnIndex == 2 || e.ColumnIndex == 3)
                {
                    lblSearch.Text = dgListView.Columns[e.ColumnIndex].HeaderText.ToString();
                }
                txtSearch.Text = "";
                //Added By Pramod Nair
                lblSearch.Text = "Search:";
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 27)
            {
                // btnClose_Click(sender, e); (Do Not call btnClose_Click event will give error is some case )
                _id = 0;
                _code = "";
                _firstname = "";
                _lastname = "";
                ItemClosedClick(sender, e);
                this.Visible = false;
            }
            else if (e.KeyChar == 13)
            {
                //btnSelect_Click(sender, e); (Do Not call btnSelect_Click event will give error is some case )
                dgListView.EndEdit();
                ItemSelectedClick(sender, e);
                this.Visible = false;
            }
        }

        private void dgListView_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 27)
            {
                // btnClose_Click(sender, e); (Do Not call btnClose_Click event will give error is some case )
                _id = 0;
                _code = "";
                _firstname = "";
                _lastname = "";
                ItemClosedClick(sender, e);
                this.Visible = false;
            }
            else if (e.KeyChar == 13)
            {
                //btnSelect_Click(sender, e); (Do Not call btnSelect_Click event will give error is some case )
                dgListView.EndEdit();
                ItemSelectedClick(sender, e);
                this.Visible = false;
            }
        }

        private void btnSelect_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 27)
            {
                // btnClose_Click(sender, e); (Do Not call btnClose_Click event will give error is some case )
                _id = 0;
                _code = "";
                _firstname = "";
                _lastname = "";
                ItemClosedClick(sender, e);
                this.Visible = false;
            }
            else if (e.KeyChar == 13)
            {
                //btnSelect_Click(sender, e); (Do Not call btnSelect_Click event will give error is some case )
                dgListView.EndEdit();
                ItemSelectedClick(sender, e);
                this.Visible = false;
            }
        }

        private void gloSmartPatientControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 27)
            {
                // btnClose_Click(sender, e); (Do Not call btnClose_Click event will give error is some case )
                _id = 0;
                _code = "";
                _firstname = "";
                _lastname = "";
                ItemClosedClick(sender, e);
                this.Visible = false;
            }
            else if (e.KeyChar == 13)
            {
                //btnSelect_Click(sender, e); (Do Not call btnSelect_Click event will give error is some case )
                dgListView.EndEdit();
                ItemSelectedClick(sender, e);
                this.Visible = false;
            }
        }

        private void dgListView_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                //btnSelect_Click(sender, e); (Do Not call btnSelect_Click event will give error is some case )
                dgListView.EndEdit();
                ItemSelectedClick(sender, e);
                this.Visible = false;
            }
        }

        private void dgListView_ColumnDisplayIndexChanged(object sender, DataGridViewColumnEventArgs e)
        {

        }

       

        #region MouseHover & Leave events

        

        #endregion
    }
}
