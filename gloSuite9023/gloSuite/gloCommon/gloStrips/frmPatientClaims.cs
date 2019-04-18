using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;

namespace gloStripControl
{
    public partial class frmPatientClaims : Form
    {

       #region " Variable Declarations "

        private string _databaseconnectionstring = "";
        private string _messageBoxCaption = "";
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        private Int64 _ClinicID = 0;
        private Int64 _UserID = 0;
        private string _UserName = "";
        private Int64 _PatientId = 0;
        private Int64 _ClaimNo = 0;
        private string _SelectedClaim = string.Empty;
        private Int64 _nTransactionID = 0;

        private const int COL_PAT_ID = 0;
        private const int COL_PAT_Code = 1;
        private const int COL_PAT_FirstName = 2;
        private const int COL_PAT_MI = 3;
        private const int COL_PAT_LastName = 4;
        private const int COL_PAT_Provider = 5;
        private const int COL_PAT_SSN = 6;
        private const int COL_PAT_DOB = 7;
        private const int COL_PAT_Phone = 8;
        private const int COL_PAT_Mobile = 9;
        

        DataView dvPatient = new DataView();
       
        DataView dvNext = new DataView();

        #endregion " Variable Declarations "
      
       #region " Property Procedures "

        public Int64 PatientId
        {
            get { return _PatientId; }
            set { _PatientId = value; }
        }

        public Int64 ClaimNo
        {
            get { return _ClaimNo; }
            set { _ClaimNo = value; }
        }

       

        public string SelectedClaim
        {
            get { return _SelectedClaim; }
            set { _SelectedClaim = value; }
        }

       #endregion " Property Procedures "

       #region " Constructor "

        public frmPatientClaims()
        {
            _databaseconnectionstring = appSettings["DataBaseConnectionString"].ToString();
            InitializeComponent();

            #region " Retrieve ClinicID from AppSettings "

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

            #endregion

            #region " Retrive UserID from appSettings "

            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                {
                    _UserID = Convert.ToInt64(appSettings["UserID"]);
                }
            }
            else
            {
                _UserID = 0;
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
                    _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageBoxCaption = "";
                }
            }
            else
            { _messageBoxCaption = ""; }

            #endregion

            //csStyle = c1BillingTransactions.Styles.Add("cs_Styles");
            //csStyle.ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.CenterCenter;
            //csStyle.BackgroundImageLayout = C1.Win.C1FlexGrid.ImageAlignEnum.CenterCenter;
        }

        #endregion " Constructor "

       #region " Form Load "

        private void frmPatientClaims_Load(object sender, EventArgs e)
        {
            txtPatientSearch.Focus();
            txtPatientSearch.Select();
            //DesignPatientGrid();
            //PopulateClaims(); 
            LoadPatient();
        }

        private void LoadPatient()
        {
            this.c1PatientDetails.RowColChange -= new System.EventHandler(this.c1PatientDetails_RowColChange);
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable dtPatients = new DataTable();
            String strSQL = "";
            try
            {
               
                //Search Patient in database
                oDB.Connect(false);

                if (_PatientId != 0)
                {
                    //Bug ID: 47921	Date Sort: gloPM - V7022 - Sorting not proper in Claim Search.
                    //Remove convert(varchar,Patient.dtDOB,101) for Patient.DOB and place Patient.DOB
                    strSQL = "SELECT  Patient.nPatientID, ISNULL(Patient.sPatientCode, '') AS PatientCode, ISNULL(Patient.sFirstName, '') AS FirstName, ISNULL(Patient.sMiddleName, '') AS MiddleName, ISNULL(Patient.sLastName, '') AS LastName, ISNULL(Provider_MST.sFirstName,'') + SPACE(1) +  ISNULL(Provider_MST.sMiddleName,'') + SPACE(1) + ISNULL(Provider_MST.sLastName,'') AS sProviderName, "
                                    + " ISNULL(Patient.nSSN,'') AS SSN,  Patient.dtDOB AS DOB,"
                                    + " ISNULL(Patient.sPhone,'') AS Phone,ISNULL(Patient.sMobile,'') AS Mobile  "
                                    + " FROM Patient INNER JOIN Provider_MST ON Patient.nProviderID = Provider_MST.nProviderID "
                                     + " WHERE ISNULL(Patient.nClinicID,0)  = " + _ClinicID + " AND Patient.nPatientID = " + _PatientId + "";
                }
                else
                {
                    //Bug ID: 47921	Date Sort: gloPM - V7022 - Sorting not proper in Claim Search.
                    //Remove convert(varchar,Patient.dtDOB,101) for Patient.DOB and place Patient.DOB
                    strSQL = "SELECT TOP 100 Patient.nPatientID, ISNULL(Patient.sPatientCode, '') AS PatientCode, ISNULL(Patient.sFirstName, '') AS FirstName, ISNULL(Patient.sMiddleName, '') AS MiddleName, ISNULL(Patient.sLastName, '') AS LastName, ISNULL(Provider_MST.sFirstName,'') + SPACE(1) +  ISNULL(Provider_MST.sMiddleName,'') + SPACE(1) + ISNULL(Provider_MST.sLastName,'') AS sProviderName, "
                                 + " ISNULL(Patient.nSSN,'') AS SSN,   Patient.dtDOB AS DOB,"
                                 + " ISNULL(Patient.sPhone,'') AS Phone,ISNULL(Patient.sMobile,'') AS Mobile  "
                                    + " FROM Patient INNER JOIN Provider_MST ON Patient.nProviderID = Provider_MST.nProviderID "
                                     + " WHERE ISNULL(Patient.nClinicID,0)  = " + _ClinicID + " ";
                }
                //strSQL = "SELECT Patient.nPatientID, ISNULL(Patient.sPatientCode, '') AS PatientCode, ISNULL(Patient.sFirstName, '') AS FirstName, ISNULL(Patient.sMiddleName, '') AS MiddleName, ISNULL(Patient.sLastName, '') AS LastName, "
                //+ " ISNULL(Patient.sPhone,'') AS Phone,ISNULL(Patient.sMobile,'') AS Mobile, ISNULL(Patient.nSSN,'') AS SSN,  "
                //+ " Convert(varchar,Patient.dtDOB,101) AS DOB "
                //+ " FROM Patient "
                //+ " WHERE ISNULL(Patient.nClinicID,0)  = " + _ClinicID + " AND Patient.nPatientID = " + _PatientId + ""; 

                oDB.Retrive_Query(strSQL, out dtPatients);
                oDB.Disconnect();

                dvPatient = dtPatients.DefaultView;
                dtPatients.Dispose();


                //Fill Patient list in grid
                
                c1PatientDetails.Visible = false;
                
                c1PatientDetails.DataSource = dvPatient;
                DesignPatientGrid();

                c1PatientDetails.Visible = true;
                if (c1PatientDetails.Rows.Count > 1)
                {
                    c1PatientDetails.Select(1, 0);
                }
                c1PatientDetails.Focus();

                PopulateClaims();

                if (c1Claims.Rows.Count > 1 && _PatientId > 0)
                {
                    //c1Claims.Row = c1Claims.FindRow(Convert.ToDecimal(ClaimNo), 1, c1Claims.Cols["ClaimNo"].Index, true);
                    c1Claims.Row = c1Claims.FindRow(_SelectedClaim, 1, c1Claims.Cols["Claim"].Index, true);
                }

                   //c1Claims.Select(c1Claims.FindRow(ClaimNo, 1, c1Claims.Cols["ClaimNo"].Index, false),0);
              
                 
            }
            catch (Exception objErr)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(objErr.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (dtPatients != null) { dtPatients.Dispose(); }
                //if (dvPatient != null) { dvPatient.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
                this.c1PatientDetails.RowColChange += new System.EventHandler(this.c1PatientDetails_RowColChange);
            }
        }

        #endregion " Form Load "

       #region "Design Grid Methods"

        public void DesignPatientGrid()
        {
            #region "Patient Details"
            try
            {
                c1PatientDetails.Hide();

                //if (c1Claims.DataSource == null)
                //{
                //    c1PatientDetails.Rows.Count = 1;
                //    c1PatientDetails.Rows.Fixed = 1;
                //    c1PatientDetails.Cols.Count = 10;
                //    c1PatientDetails.Cols.Fixed = 0;
                //}

                //c1PatientDetails.SetData(0, COL_PAT_ID, "nPatientID");
                //c1PatientDetails.SetData(0, COL_PAT_Code, "Code");
                //c1PatientDetails.SetData(0, COL_PAT_FirstName, "First Name");
                //c1PatientDetails.SetData(0, COL_PAT_MI, "MI");
                //c1PatientDetails.SetData(0, COL_PAT_LastName, "Last Name");
                //c1PatientDetails.SetData(0, COL_PAT_SSN, "SSN");
                //c1PatientDetails.SetData(0, COL_PAT_Provider, "Provider");
                //c1PatientDetails.SetData(0, COL_PAT_DOB, "DOB");
                //c1PatientDetails.SetData(0, COL_PAT_Phone, "Phone");
                //c1PatientDetails.SetData(0, COL_PAT_Mobile, "Mobile");


                c1PatientDetails.Cols[COL_PAT_ID].Caption = "nPatientID";
                c1PatientDetails.Cols[COL_PAT_Code].Caption =  "Code";
                c1PatientDetails.Cols[COL_PAT_FirstName].Caption =  "First Name";
                c1PatientDetails.Cols[COL_PAT_MI].Caption =  "MI";
                c1PatientDetails.Cols[COL_PAT_LastName].Caption =  "Last Name";
                c1PatientDetails.Cols[COL_PAT_SSN].Caption =  "SSN";
                c1PatientDetails.Cols[COL_PAT_Provider].Caption =  "Provider";
                c1PatientDetails.Cols[COL_PAT_DOB].Caption =  "DOB";
                c1PatientDetails.Cols[COL_PAT_Phone].Caption =  "Phone";
                c1PatientDetails.Cols[COL_PAT_Mobile].Caption =  "Mobile";

                //Bug ID: 47921	Date Sort: gloPM - V7022 - Sorting not proper in Claim Search.
                //set format for Patient.DOB.
                c1PatientDetails.Cols[COL_PAT_DOB].Format = "MM/dd/yyyy";

                c1PatientDetails.Cols[COL_PAT_ID].Visible = false;
                c1PatientDetails.Cols[COL_PAT_SSN].Visible = false;
                //c1PatientDetails.Cols[COL_PAT_Provider].Visible = false;
                //c1PatientDetails.Cols[COL_PAT_DOB].Visible = false;
                //c1PatientDetails.Cols[COL_PAT_Phone].Visible = false;
                //c1PatientDetails.Cols[COL_PAT_Mobile].Visible = false;
                //c1PatientDetails.Cols[COL_PAT_MI].Visible = false;

               

                int _width = (this.Width - 20) / 10;

                c1PatientDetails.Cols[COL_PAT_Code].Width = _width * 1;
                c1PatientDetails.Cols[COL_PAT_FirstName].Width = _width * 1;
                c1PatientDetails.Cols[COL_PAT_MI].Width = Convert.ToInt32(_width * 0.5);
                c1PatientDetails.Cols[COL_PAT_LastName].Width = _width * 1;
                c1PatientDetails.Cols[COL_PAT_SSN].Width = _width * 1;
                c1PatientDetails.Cols[COL_PAT_Provider].Width = _width * 2;
                c1PatientDetails.Cols[COL_PAT_DOB].Width = _width * 1;
                c1PatientDetails.Cols[COL_PAT_Phone].Width = _width * 1;
                c1PatientDetails.Cols[COL_PAT_Mobile].Width = _width * 1;


                c1PatientDetails.Cols[COL_PAT_Code].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1PatientDetails.Cols[COL_PAT_FirstName].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1PatientDetails.Cols[COL_PAT_MI].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1PatientDetails.Cols[COL_PAT_LastName].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1PatientDetails.Cols[COL_PAT_SSN].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1PatientDetails.Cols[COL_PAT_Provider].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1PatientDetails.Cols[COL_PAT_DOB].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1PatientDetails.Cols[COL_PAT_Phone].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1PatientDetails.Cols[COL_PAT_Mobile].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;


                //This code is commented by Ojeswini(02172010)
                //c1PatientDetails.VisualStyle = VisualStyle.Office2007Blue;
                //c1PatientDetails.Styles[CellStyleEnum.SelectedColumnHeader].ForeColor = Color.FromArgb(31, 73, 125);
                //c1PatientDetails.Styles[CellStyleEnum.Fixed].ForeColor = Color.FromArgb(31, 73, 125);
                //c1PatientDetails.Styles[CellStyleEnum.Alternate].ForeColor = Color.FromArgb(31, 73, 125);

                gloC1FlexStyle.Style(c1PatientDetails, false);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                c1PatientDetails.Show();
            }
            #endregion

        }

        public void DesignClaimGrid()
        {
            try
            {
                c1Claims.Hide();

                c1Claims.Cols["TransactionMasterID"].Visible = false;
                c1Claims.Cols["TransactionID"].Visible = false;
                c1Claims.Cols["MasterAppointmentID"].Visible = false;
                c1Claims.Cols["AppointmentID"].Visible = false;
                c1Claims.Cols["ClaimNo"].Visible = false;
                c1Claims.Cols["Claim"].Visible = true;
                c1Claims.Cols["SubClaimNo"].Visible = false;
                c1Claims.Cols["bAutoClaim"].Visible = false;
                c1Claims.Cols["ReferralID"].Visible = false;
                c1Claims.Cols["bIsOpened"].Visible = false;
                c1Claims.Cols["ContactID"].Visible = false;
                c1Claims.Cols["ClinicID"].Visible = false;
                c1Claims.Cols["PatientID"].Visible = false;
                c1Claims.Cols["TransactionProviderID"].Visible = false;
                c1Claims.Cols["Status"].Visible = true;
                c1Claims.Cols["nResponsibilityNo"].Visible = false;
                c1Claims.Cols["row_number"].Visible = false;
                c1Claims.Cols["FacilityCode"].Visible = false;
                c1Claims.Cols["PatientCode"].Visible = false;

                c1Claims.Cols["Date"].Visible = true;
                c1Claims.Cols["Facility"].Visible = true;
                c1Claims.Cols["ReferralID"].Visible = false;
                c1Claims.Cols["ProviderFName"].Visible = true;
                c1Claims.Cols["ProviderMName"].Visible = true;
                c1Claims.Cols["ProviderLName"].Visible = true;
                c1Claims.Cols["PatientFName"].Visible = false;
                c1Claims.Cols["PatientMName"].Visible = false;
                c1Claims.Cols["PatientLName"].Visible = false;
                c1Claims.Cols["InsuranceID"].Visible = true;
                c1Claims.Cols["Insurance"].Visible = true;
                c1Claims.Cols["Status"].Visible = false;
                c1Claims.Cols["SortClaim"].Visible = false;
                c1Claims.Cols["SortSubClaim"].Visible = false;

                c1Claims.Cols["TransactionMasterID"].Width = 0;
                c1Claims.Cols["TransactionID"].Width = 0;
                c1Claims.Cols["MasterAppointmentID"].Width = 0;
                c1Claims.Cols["AppointmentID"].Width = 0;
                c1Claims.Cols["ClaimNo"].Width = 0;
                c1Claims.Cols["Claim"].Width = 100;
                c1Claims.Cols["SubClaimNo"].Width = 0;
                c1Claims.Cols["bAutoClaim"].Width = 0;
                c1Claims.Cols["ReferralID"].Width = 0;
                c1Claims.Cols["bIsOpened"].Width = 0;
                c1Claims.Cols["ContactID"].Width = 0;
                c1Claims.Cols["ClinicID"].Width = 0;
                c1Claims.Cols["PatientID"].Width = 0;
                c1Claims.Cols["TransactionProviderID"].Width = 0;
                c1Claims.Cols["Status"].Width = 0;
                c1Claims.Cols["nResponsibilityNo"].Width = 0;
                c1Claims.Cols["row_number"].Width = 0;
                c1Claims.Cols["FacilityCode"].Width = 0;
                c1Claims.Cols["SortClaim"].Width = 0;
                c1Claims.Cols["SortSubClaim"].Width = 0;

                c1Claims.Cols["Date"].Width = 100;
                c1Claims.Cols["Facility"].Width = 180;
                c1Claims.Cols["ReferralID"].Width = 0;
                c1Claims.Cols["ProviderFName"].Width = 120;
                c1Claims.Cols["ProviderMName"].Width = 100;
                c1Claims.Cols["ProviderLName"].Width = 130;
                c1Claims.Cols["PatientFName"].Width = 0;
                c1Claims.Cols["PatientMName"].Width = 0;
                c1Claims.Cols["PatientLName"].Width = 0;
                c1Claims.Cols["InsuranceID"].Width = 0;
                c1Claims.Cols["Insurance"].Width = 350;


                c1Claims.Cols["Date"].Caption = "Date";
                c1Claims.Cols["Claim"].Caption = "Claim #";
                c1Claims.Cols["Facility"].Caption = "Facility";
                c1Claims.Cols["ReferralID"].Caption = "ReferralID";
                c1Claims.Cols["ProviderFName"].Caption = "Provider First";
                c1Claims.Cols["ProviderMName"].Caption = "MI";
                c1Claims.Cols["ProviderLName"].Caption = "Last";
                c1Claims.Cols["PatientFName"].Caption = "Patient FN";
                c1Claims.Cols["PatientMName"].Caption = "Patient MN";
                c1Claims.Cols["PatientLName"].Caption = "Patient LN";
                c1Claims.Cols["InsuranceID"].Caption = "Insurance ID";
                c1Claims.Cols["Insurance"].Caption = "Insurance Plan";
                c1Claims.Cols["DOS"].Caption = "DOS";


                for (int i = 0; i < c1Claims.Rows.Count; i++)
                {
                    c1Claims.Rows[i].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftBottom;
                }
                for (int i = 1; i < c1Claims.Cols.Count; i++)
                {
                    c1Claims.Cols[i].AllowEditing = false;
                }
                c1Claims.Cols["Date"].Format = "MM/dd/yyyy";
                c1Claims.Cols["DOS"].Format = "MM/dd/yyyy";

                gloC1FlexStyle.Style(c1Claims, true);    
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                c1Claims.Show();
            }

        }

        #endregion

       #region  " Toolbar button Click "

        private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;    
            this.Close();
        }

        private void tsb_OK_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.Yes;
                if (c1Claims.RowSel > 0)
                {
                    _PatientId = Convert.ToInt64(c1Claims.GetData(c1Claims.RowSel, c1Claims.Cols["PatientID"].Index));
                    _ClaimNo = Convert.ToInt64(c1Claims.GetData(c1Claims.RowSel, c1Claims.Cols["ClaimNo"].Index));
                    _SelectedClaim = Convert.ToString(c1Claims.GetData(c1Claims.RowSel, c1Claims.Cols["Claim"].Index));
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }


       #endregion  " Toolbar button Click "

       #region " C1 Grid Events "

        private void c1PatientDetails_RowColChange(object sender, EventArgs e)
        {
            PopulateClaims();
        }

        private void c1PatientDetails_MouseClick(object sender, MouseEventArgs e)
        {
            //PopulateClaims();
        }

        private void c1Claims_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    //C1.Win.C1FlexGrid.HitTestInfo hitInfo = c1Claims.HitTest(e.X, e.Y);

                    //if (hitInfo.Row > 0)
                    //{
                    //    if (c1Claims.GetData(hitInfo.Row, c1Claims.Cols["PatientID"].Index) != null && c1Claims.GetData(hitInfo.Row, c1Claims.Cols["PatientID"].Index).ToString().Length > 0)
                    //    {
                    //        _PatientId = Convert.ToInt64(c1Claims.GetData(hitInfo.Row, c1Claims.Cols["PatientID"].Index));
                    //    }
                    //    if (c1Claims.GetData(hitInfo.Row, c1Claims.Cols["ClaimNo"].Index) != null && c1Claims.GetData(hitInfo.Row, c1Claims.Cols["ClaimNo"].Index).ToString().Length > 0)
                    //    {
                    //        _ClaimNo = Convert.ToInt64(c1Claims.GetData(hitInfo.Row, c1Claims.Cols["ClaimNo"].Index));
                    //        _SelectedClaim = Convert.ToString(c1Claims.GetData(hitInfo.Row, c1Claims.Cols["Claim"].Index));
                    //    }
                    //    this.Close();
                    //}
                    //else
                    //{
                    //    _PatientId = 0;
                    //    _ClaimNo = 0;
                    //}
                    C1.Win.C1FlexGrid.HitTestInfo hitInfo = c1Claims.HitTest(e.X, e.Y);

                    if (hitInfo.Row > 0)
                    {
                        tsb_OK_Click(null, null);  
                    }
                    
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        #endregion

       #region "Search Patient and Claims"

        private void txtPatientSearch_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == 46)
            {
                //e.Handled =false;
            }
            if (e.KeyChar == 13)
            {
                InstringSearch(txtPatientSearch.Text.Trim());
                PopulateClaims();
               
            }
           

        }

        private void InstringSearch(string SearchText)
        {
            this.c1PatientDetails.RowColChange -= new System.EventHandler(this.c1PatientDetails_RowColChange);
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable dtPatients = new DataTable();
            String strSQL = "";
            try
            {

                // Seach On Patient Information
                if (dvPatient == null)
                {
                    this.Cursor = Cursors.Default;
                    return;
                }

                //byte nPatientIDColNo = 0;
                byte nPatientCodeColumnNo = 1;
                byte nPatientFirstNameColumnNo = 2;
                byte nPatientMiddleNameColumnNo = 3;
                byte nPatientLastNameColumnNo = 4;

                string str = "";
                string[] strSearchArray;
               
                str = SearchText;
                str = str.Trim().Replace("'", "''").Replace("[", "").Replace("]", "").Replace("*", "%"); ;

                if (str.Length > 1)
                {
                    string str1 = str.Substring(1).Replace("%", "");
                    str = str.Substring(0, 1) + str1;
                }

                if (str.Trim() != "")
                {
                    strSearchArray = str.Split(',');
                    string strSearch = "";
                    if (strSearchArray.Length == 1)
                    {
                        strSearch = strSearchArray[0];

                        //Search Patient in database
                        oDB.Connect(false);

                        strSQL = "SELECT  Patient.nPatientID, ISNULL(Patient.sPatientCode, '') AS PatientCode, ISNULL(Patient.sFirstName, '') AS FirstName, ISNULL(Patient.sMiddleName, '') AS MiddleName, ISNULL(Patient.sLastName, '') AS LastName, ISNULL(Provider_MST.sFirstName,'') + SPACE(1) +  ISNULL(Provider_MST.sMiddleName,'') + SPACE(1) + ISNULL(Provider_MST.sLastName,'') AS sProviderName, "
                                        + " ISNULL(Patient.nSSN,'') AS SSN,  Convert(varchar,Patient.dtDOB,101) AS DOB,"
                                        + " ISNULL(Patient.sPhone,'') AS Phone,ISNULL(Patient.sMobile,'') AS Mobile  "
                        + " FROM Patient INNER JOIN Provider_MST ON Patient.nProviderID = Provider_MST.nProviderID "
                        + " WHERE ISNULL(Patient.nClinicID,0)  = " + _ClinicID + " AND ISNULL(Patient.sPatientCode, '') LIKE '" + strSearch + "%' OR " +
                        " ISNULL(Patient.sFirstName, '') LIKE '" + strSearch + "%' OR " +
                        " ISNULL(Patient.sMiddleName, '') LIKE '" + strSearch + "%' OR " +
                        " ISNULL(Patient.sLastName, '') LIKE '" + strSearch + "%' ";

                        //strSQL = "SELECT TOP 2000  Patient.nPatientID, ISNULL(Patient.sPatientCode, '') AS PatientCode, ISNULL(Patient.sFirstName, '') AS FirstName, ISNULL(Patient.sMiddleName, '') AS MiddleName, ISNULL(Patient.sLastName, '') AS LastName, "
                        //+ " ISNULL(Patient.sPhone,'') AS Phone,ISNULL(Patient.sMobile,'') AS Mobile, ISNULL(Patient.nSSN,'') AS SSN,  "
                        //+ " Convert(varchar,Patient.dtDOB,101) AS DOB " 
                        //+ " FROM Patient "
                        //+ " WHERE ISNULL(Patient.nClinicID,0)  = " + _ClinicID + " AND ISNULL(Patient.sPatientCode, '') LIKE '" + strSearch + "%' OR " 
                        //+ " ISNULL(Patient.sFirstName, '') LIKE '" + strSearch + "%' OR " 
                        //+ " ISNULL(Patient.sMiddleName, '') LIKE '" + strSearch + "%' OR " 
                        //+  " ISNULL(Patient.sLastName, '') LIKE '" + strSearch + "%' ";

                        oDB.Retrive_Query(strSQL, out dtPatients);
                        oDB.Disconnect(); 

                        dvPatient = dtPatients.DefaultView;
                        dtPatients.Dispose();

                            
                        //Fill Patient list in grid
                        
                        c1PatientDetails.Visible = false;
                        c1PatientDetails.DataSource = dvPatient;
                        c1PatientDetails.Refresh();   
                        DesignPatientGrid();
                        //for (int i = 0; i <= dvPatient.Count - 1; i++)
                        //{
                        //    c1PatientDetails.Rows.Add();
                        //    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_ID, dvPatient[i]["nPatientID"].ToString());
                        //    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_Code, dvPatient[i]["PatientCode"].ToString());
                        //    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_FirstName, dvPatient[i]["FirstName"].ToString());
                        //    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_MI, dvPatient[i]["MiddleName"].ToString());
                        //    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_LastName, dvPatient[i]["LastName"].ToString());
                        //    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_SSN, dvPatient[i]["SSN"].ToString());
                        //    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_Provider, dvPatient[i]["sProviderName"].ToString());
                        //    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_DOB, dvPatient[i]["DOB"].ToString());
                        //    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_Phone, dvPatient[i]["Phone"].ToString());
                        //    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_Mobile, dvPatient[i]["Mobile"].ToString());

                        //}
                        
                        c1PatientDetails.Visible = true;
                        if (c1PatientDetails.Rows.Count > 1)
                        {
                            c1PatientDetails.Select(1, 0);
                        }

                        // added by sandip dhakane 20100715
                      //  c1PatientDetails.Focus();
                        txtPatientSearch.Focus();
                     // end

                        c1PatientDetails.Show(); 
                        
                        
                    }
                    else
                    {
                        for (int i = 0; i < strSearchArray.Length; i++)
                        {
                            strSearch = strSearchArray[i];
                            DataTable dtTemp = null;
                            if (strSearch.Trim() != "")
                            {
                                if (i == 0)
                                {
                                    oDB.Connect(false);


                                    strSQL = "SELECT  Patient.nPatientID, ISNULL(Patient.sPatientCode, '') AS PatientCode, ISNULL(Patient.sFirstName, '') AS FirstName, ISNULL(Patient.sMiddleName, '') AS MiddleName, ISNULL(Patient.sLastName, '') AS LastName, ISNULL(Provider_MST.sFirstName,'') + SPACE(1) +  ISNULL(Provider_MST.sMiddleName,'') + SPACE(1) + ISNULL(Provider_MST.sLastName,'') AS sProviderName, "
                                                    + " ISNULL(Patient.nSSN,'') AS SSN,  Convert(varchar,Patient.dtDOB,101) AS DOB,"
                                                    + " ISNULL(Patient.sPhone,'') AS Phone,ISNULL(Patient.sMobile,'') AS Mobile  "
                                    + " FROM Patient INNER JOIN Provider_MST ON Patient.nProviderID = Provider_MST.nProviderID "
                                    + " WHERE ISNULL(Patient.nClinicID,0)  = " + _ClinicID + " AND ISNULL(Patient.sPatientCode, '') LIKE '" + strSearch + "%' OR " +
                                    " ISNULL(Patient.sFirstName, '') LIKE '" + strSearch + "%' OR " +
                                    " ISNULL(Patient.sMiddleName, '') LIKE '" + strSearch + "%' OR " +
                                    " ISNULL(Patient.sLastName, '') LIKE '" + strSearch + "%' ";

                                    //strSQL = "SELECT TOP 2000  Patient.nPatientID, ISNULL(Patient.sPatientCode, '') AS PatientCode, ISNULL(Patient.sFirstName, '') AS FirstName, ISNULL(Patient.sMiddleName, '') AS MiddleName, ISNULL(Patient.sLastName, '') AS LastName, "
                                    //+ " ISNULL(Patient.sPhone,'') AS Phone,ISNULL(Patient.sMobile,'') AS Mobile, ISNULL(Patient.nSSN,'') AS SSN,  "
                                    //+ " Convert(varchar,Patient.dtDOB,101) AS DOB, ISNULL(Provider_MST.sFirstName,'') + SPACE(1) +  ISNULL(Provider_MST.sMiddleName,'') + SPACE(1) + ISNULL(Provider_MST.sLastName,'') AS sProviderName "
                                    //+ " FROM Patient INNER JOIN Provider_MST ON Patient.nProviderID = Provider_MST.nProviderID "
                                    //+ " WHERE ISNULL(Patient.nClinicID,0)  = " + _ClinicID + " AND ISNULL(Patient.sPatientCode, '') LIKE '" + strSearch + "%' OR " +
                                    //" ISNULL(Patient.sFirstName, '') LIKE '" + strSearch + "%' OR " +
                                    //" ISNULL(Patient.sMiddleName, '') LIKE '" + strSearch + "%' OR " +
                                    //" ISNULL(Patient.sLastName, '') LIKE '" + strSearch + "%' ";

                                    oDB.Retrive_Query(strSQL, out dtPatients);
                                    oDB.Disconnect();
                                    dvPatient = dtPatients.DefaultView;
                                    dtPatients.Dispose();

                                    dtTemp = dvPatient.ToTable();
                                    dvNext = dtTemp.Copy().DefaultView;
                                    dtTemp.Dispose();
                                }
                                else
                                {
                                    dtTemp = dvNext.ToTable();
                                    dvNext = dtTemp.Copy().DefaultView;
                                    dtTemp.Dispose();
                                }

                                dvNext.RowFilter = dvNext.Table.Columns[nPatientCodeColumnNo].ColumnName + " Like '" + strSearch + "%' OR " +
                                            dvNext.Table.Columns[nPatientFirstNameColumnNo].ColumnName + " Like '" + strSearch + "%' OR " +
                                            dvNext.Table.Columns[nPatientMiddleNameColumnNo].ColumnName + " Like '" + strSearch + "%' OR " +
                                            dvNext.Table.Columns[nPatientLastNameColumnNo].ColumnName + " Like '" + strSearch + "%' ";
                            }
                            if (dtTemp != null)
                            {
                                dtTemp.Dispose();
                                dtTemp = null;
                            }
                        }

                        
                        //Fill Patient list in grid
                        
                        c1PatientDetails.Visible = false;
                        c1PatientDetails.DataSource = dvNext;
                        DesignPatientGrid();
                        //for (int i = 0; i <= dvPatient.Count - 1; i++)
                        //{
                        //    c1PatientDetails.Rows.Add();
                        //    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_ID, dvPatient[i]["nPatientID"].ToString());
                        //    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_Code, dvPatient[i]["PatientCode"].ToString());
                        //    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_FirstName, dvPatient[i]["FirstName"].ToString());
                        //    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_MI, dvPatient[i]["MiddleName"].ToString());
                        //    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_LastName, dvPatient[i]["LastName"].ToString());
                        //    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_SSN, dvPatient[i]["SSN"].ToString());
                        //    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_Provider, dvPatient[i]["sProviderName"].ToString());
                        //    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_DOB, dvPatient[i]["DOB"].ToString());
                        //    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_Phone, dvPatient[i]["Phone"].ToString());
                        //    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_Mobile, dvPatient[i]["Mobile"].ToString());

                        //}
                        c1PatientDetails.Visible = true;
                        if (c1PatientDetails.Rows.Count > 1)
                        {
                            c1PatientDetails.Select(1, 0);
                        }

                        // added by sandip dhakane 20100715
                        //c1PatientDetails.Focus();
                        txtPatientSearch.Focus();
                        // end 
                       
                    }

                }
                else
                {

                    //***** added by sandip dhakane 20100715 for displaying patient list when there is nothing in search text box.
                    //_PatientId = 0;

                    oDB.Connect(false);
                    
                    strSQL = "SELECT TOP 100 Patient.nPatientID, ISNULL(Patient.sPatientCode, '') AS PatientCode, ISNULL(Patient.sFirstName, '') AS FirstName, ISNULL(Patient.sMiddleName, '') AS MiddleName, ISNULL(Patient.sLastName, '') AS LastName, ISNULL(Provider_MST.sFirstName,'') + SPACE(1) +  ISNULL(Provider_MST.sMiddleName,'') + SPACE(1) + ISNULL(Provider_MST.sLastName,'') AS sProviderName, "
                                + " ISNULL(Patient.nSSN,'') AS SSN,  Convert(varchar,Patient.dtDOB,101) AS DOB,"
                                + " ISNULL(Patient.sPhone,'') AS Phone,ISNULL(Patient.sMobile,'') AS Mobile  "
                                   + " FROM Patient INNER JOIN Provider_MST ON Patient.nProviderID = Provider_MST.nProviderID "
                                    + " WHERE ISNULL(Patient.nClinicID,0)  = " + _ClinicID + " ";

                    oDB.Retrive_Query(strSQL, out dtPatients);
                    oDB.Disconnect();

                    dvPatient = dtPatients.DefaultView;
                    dtPatients.Dispose();

                    //Fill Patient list in grid

                    c1PatientDetails.Visible = false;
                    c1PatientDetails.DataSource = dvPatient;
                    c1PatientDetails.Refresh();
                    DesignPatientGrid();

                    c1PatientDetails.Visible = true;
                    if (c1PatientDetails.Rows.Count > 1)
                    {
                        c1PatientDetails.Select(1, 0);
                    }
                    c1PatientDetails.Focus();

                    // end
                }
            }
            catch (Exception objErr)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(objErr.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (dtPatients != null) { dtPatients.Dispose(); }
                //if (dvPatient != null) { dvPatient.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
                this.c1PatientDetails.RowColChange += new System.EventHandler(this.c1PatientDetails_RowColChange);
            }
        }

        private DataTable GetClaimsForPatient(Int64 _nPatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            DataTable dtClaims = null;
            try
            {
                oDB.Connect(false);
                oDBParameters.Add("@nPatientID", _nPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("BL_SELECT_ClaimsForPatient", oDBParameters, out dtClaims);
                oDB.Disconnect();

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
                if (oDB != null) { oDB.Dispose(); }
            }
            return dtClaims;
        }

        private void PopulateClaims()
        {
            try
            {
                Int64 _patientId = 0;
                if (c1PatientDetails != null && c1PatientDetails.Rows.Count > 1)
                {
                    if (c1PatientDetails.GetData(c1PatientDetails.RowSel, COL_PAT_ID) != null
                    && Convert.ToString(c1PatientDetails.GetData(c1PatientDetails.RowSel, COL_PAT_ID)) != "")
                    {
                        _patientId = Convert.ToInt64(c1PatientDetails.GetData(c1PatientDetails.RowSel, COL_PAT_ID));
                        DataTable dtClaims = new DataTable();
                        dtClaims = GetClaimsForPatient(_patientId);
                        if (dtClaims != null)
                        {
                            gloGlobal.gloPMGlobal.SplitClaimColumn(dtClaims, dtClaims.Columns.IndexOf("Claim"));
                            dtClaims.DefaultView.Sort = "SortClaim Desc,SortSubClaim ASC";
                            c1Claims.DataSource = dtClaims.DefaultView;
                            //c1Claims.Cols["Claim"].Sort = SortFlags.Ascending;
                        }
                        DesignClaimGrid();
                    }

                }
                else
                {
                    DataTable dtClaims = new DataTable();
                    dtClaims = GetClaimsForPatient(_patientId);
                    if (dtClaims != null)
                    {
                        gloGlobal.gloPMGlobal.SplitClaimColumn(dtClaims, dtClaims.Columns.IndexOf("Claim"));
                        dtClaims.DefaultView.Sort = "SortClaim Desc,SortSubClaim ASC";
                        c1Claims.DataSource = dtClaims.DefaultView;
                        //c1Claims.Cols["Claim"].Sort = SortFlags.Ascending;
                    }
                    DesignClaimGrid();
                }

                if (c1Claims.Rows.Count > 1)
                {
                    c1Claims.Select(1, 0);
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
            }
        }

	#endregion

        private void c1Claims_BeforeSort(object sender, C1.Win.C1FlexGrid.SortColEventArgs e)
        {
            if (c1Claims.Rows.Count > 1)
            {
                try
                {
                    _nTransactionID = Convert.ToInt64(c1Claims.Rows[c1Claims.RowSel]["TransactionID"]);
                }
                catch (Exception)
                {
                    _nTransactionID = 0;
                }

            }
        }

        private void c1Claims_AfterSort(object sender, C1.Win.C1FlexGrid.SortColEventArgs e)
        {
            if (c1Claims.DataSource != null)
            {
                if (e.Col == c1Claims.Cols["Claim"].Index)
                {
                    //c1Claims.Sort(e.Order, c1Claims.Cols["row_number"].Index);
                    c1Claims.Cols["SortClaim"].Sort = e.Order;
                    c1Claims.Cols["SortSubClaim"].Sort = SortFlags.Ascending;
                    c1Claims.Sort(SortFlags.UseColSort, c1Claims.Cols["SortClaim"].Index, c1Claims.Cols["SortSubClaim"].Index);
                }
            }

            int _index;
            try
            {
                _index = c1Claims.FindRow(_nTransactionID.ToString(), 0, c1Claims.Cols["TransactionID"].Index, false, false, false);
            }
            catch (Exception)
            {
                _index = 0;
            }

            c1Claims.ShowCell(_index, 0);
            c1Claims.Row = _index;
            c1Claims.Select();
        }


    }
}