 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using gloPatient;
using gloSecurity;
using C1.Win.C1FlexGrid;
using gloDateMaster;


namespace gloPatientStripControl
{

    public partial class gloClaimSearchControl : UserControl
    {
        #region Constructors
        private Int64 _ClinicID = 0;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings; 

        public gloClaimSearchControl()
        {
            InitializeComponent();

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }
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

            #region "UserId And UserName"

            //Get User ID
            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                { _UserID = Convert.ToInt64(appSettings["UserID"]); }
                else
                { _UserID = 1; }
            }
            else
            { _UserID = 1; }
            //

            //Get User Name
            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                { _UserName = Convert.ToString(appSettings["UserName"]).Trim(); }
                else
                { _UserName = ""; }
            }
            else
            { _UserName = ""; }

            #endregion
        }

        public gloClaimSearchControl(string connectionstring)
        {
            InitializeComponent();
            _DatabaseConnectionString = connectionstring;

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

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

            #region "UserId And UserName"

            //Get User ID
            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                { _UserID = Convert.ToInt64(appSettings["UserID"]); }
                else
                { _UserID = 1; }
            }
            else
            { _UserID = 1; }
            //

            //Get User Name
            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                { _UserName = Convert.ToString(appSettings["UserName"]).Trim(); }
                else
                { _UserName = ""; }
            }
            else
            { _UserName = ""; }

            #endregion
        }

        public gloClaimSearchControl(string connectionstring, Boolean AllowPatientSearch)
        {
            InitializeComponent();
            _DatabaseConnectionString = connectionstring;
            
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }
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

            #region "UserId And UserName"

            //Get User ID
            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                { _UserID = Convert.ToInt64(appSettings["UserID"]); }
                else
                { _UserID = 1; }
            }
            else
            { _UserID = 1; }
            //

            //Get User Name
            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                { _UserName = Convert.ToString(appSettings["UserName"]).Trim(); }
                else
                { _UserName = ""; }
            }
            else
            { _UserName = ""; }

            #endregion
        }

        public gloClaimSearchControl(string connectionstring, Boolean AllowPatientSearch, Boolean AllowClaimNoSearch)
        {
            InitializeComponent();
            _DatabaseConnectionString = connectionstring;
            
            _AllowClaimNoSearch = AllowClaimNoSearch;

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

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

            #region "UserId And UserName"

            //Get User ID
            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                { _UserID = Convert.ToInt64(appSettings["UserID"]); }
                else
                { _UserID = 1; }
            }
            else
            { _UserID = 1; }
            //

            //Get User Name
            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                { _UserName = Convert.ToString(appSettings["UserName"]).Trim(); }
                else
                { _UserName = ""; }
            }
            else
            { _UserName = ""; }

            #endregion
        }

        public gloClaimSearchControl(string connectionstring, Boolean AllowPatientSearch, Boolean AllowClaimNoSearch, Boolean IsHCFA1500)
        {
            InitializeComponent();
            _DatabaseConnectionString = connectionstring;
            
            _AllowClaimNoSearch = AllowClaimNoSearch;
            _IsHCFA1500 = IsHCFA1500;

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

            

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

            #region "UserId And UserName"

            //Get User ID
            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                { _UserID = Convert.ToInt64(appSettings["UserID"]); }
                else
                { _UserID = 1; }
            }
            else
            { _UserID = 1; }
            //

            //Get User Name
            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                { _UserName = Convert.ToString(appSettings["UserName"]).Trim(); }
                else
                { _UserName = ""; }
            }
            else
            { _UserName = ""; }

            #endregion

        }

        public gloClaimSearchControl(string connectionstring, Boolean AllowPatientSearch, Boolean AllowClaimNoSearch, Boolean IsHCFA1500,bool DefaultToClaimNoSearch)
        {
            InitializeComponent();
            _DatabaseConnectionString = connectionstring;
            
            _AllowClaimNoSearch = AllowClaimNoSearch;
            _IsHCFA1500 = IsHCFA1500;
            _DefaultClaimNoSearch = DefaultToClaimNoSearch;

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

            

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

            #region "UserId And UserName"

            //Get User ID
            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                { _UserID = Convert.ToInt64(appSettings["UserID"]); }
                else
                { _UserID = 1; }
            }
            else
            { _UserID = 1; }
            //

            //Get User Name
            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                { _UserName = Convert.ToString(appSettings["UserName"]).Trim(); }
                else
                { _UserName = ""; }
            }
            else
            { _UserName = ""; }

            #endregion

            chk_ClaimNoSearch.Checked = DefaultToClaimNoSearch;
        }

        #endregion

        #region Private Variables

        DataView dvPatient = new DataView ();
        
        DataView dvNext = new DataView();
       private string _MessageBoxCaption = String.Empty;

        private string _DatabaseConnectionString = "";
        private Boolean _AllowClaimNoSearch = false;
        private Boolean _IsHCFA1500 = false;
        private Boolean _DefaultClaimNoSearch = false; 
        private Int64 _PatientID = 0;
        private Int64 _TransactionID = 0;
        private Int64 _TransactionMasterID = 0;
        private string _PatientCode = "";
        private string _PatientName = "";
        private Boolean _IsSearchOnPatientCode = false;
        private Int64 _ClaimNumber = 0;
        private Int64 _SubClaimNumber = 0;
        private string _ClaimSubClaimNo = "";

        //Added By Pramod Nair For UserRights
        private Int64 _UserID = 0;
        private string _UserName = "";
        
        
        private Boolean gblnAddModPatient = false;      
        
        #endregion

        #region Properties
        public Int64  PatientID
        {
            get { return _PatientID; }
            set { _PatientID = value; }
        }
        public Int64 TransactionID
        {
            get { return _TransactionID; }
            set { _TransactionID = value; }
        }
        public Int64 TransactionMasterID
        {
            get { return _TransactionMasterID; }
            set { _TransactionMasterID = value; }
        }
        public  string PatientCode
        {
            get { return _PatientCode; }
            set { _PatientCode = value; }
        }
        public string PatientName
        {
            get { return _PatientName; }
            set { _PatientName = value; }
        }
        public Boolean SearchOnPatientCode
        {
            get { return _IsSearchOnPatientCode; }
            set { _IsSearchOnPatientCode = value; }
        }
        public Int64 ClaimNumber
        {
            get { return _ClaimNumber; }
            set { _ClaimNumber = value; }
        }
        public Int64 SubClaimNumber
        {
            get { return _SubClaimNumber; }
            set { _SubClaimNumber = value; }
        }
        public string ClaimSubClaimNo
        {
            get { return _ClaimSubClaimNo; }
            set { _ClaimSubClaimNo = value; }
        }
        public String LblClaimNo
        {
            set { this.lblClaimNo.Text  = value; }
        }
        public void LoadClaim(Int64 ClaimNo)
        {
            _ClaimNumber = ClaimNo;
            KeyPressEventArgs e = new KeyPressEventArgs(Convert.ToChar(Keys.Enter));           
        }

        #endregion

        #region DELEGATES & EVENTS

        public delegate void ControlSizeChanged(object sender, EventArgs e);
        public event ControlSizeChanged ControlSize_Changed;

        public delegate void DateValidated(object sender, EventArgs e);
        //public event DateValidated Date_Validated;

        public delegate void DateValueChanged1(object sender, EventArgs e);
        //public event DateValueChanged1 DateValue_Changed;

        public delegate void DateValidating(object sender, CancelEventArgs e);
       // public event EventHandler Date_Validating;

        public delegate void DateDropDown(object sender, EventArgs e);
        //public event DateDropDown Date_DropDown;

        public delegate void DateCloseUp(object sender, EventArgs e);
        //public event DateCloseUp Date_CloseUp;

        public delegate void txtPatientSearchTextChanged(object sender, EventArgs e);
       // public event txtPatientSearchTextChanged txtPatientSearchTextChanged1;

        public delegate void PatientSearchKeyPressHandler(object sender, KeyPressEventArgs e);
        public event PatientSearchKeyPressHandler OnPatientSearchKeyPress;



        public delegate void Patient_Modified(object sender, EventArgs e);
        public event Patient_Modified PatientModified;

        public delegate void Insurance_Selected(Int64 InsuranceID, string InsuranceName, Int32 InsuraceSelfMode, Int64 ContactID);
    //    public event Insurance_Selected InsuranceSelected;
        
        #endregion

        #region " Form Button Click Events "
        private void gloPatientStripControl_Load(object sender, EventArgs e)
        {
            try
            {
                btn_ModityPatient.BackgroundImage = global::gloPatientStripControl.Properties.Resources.Patient;
                btn_ModityPatient.BackgroundImageLayout = ImageLayout.Center;
                btn_ModityPatient.Enabled = true;
                btn_ModityPatient.Visible  = true;
                chk_ClaimNoSearch.Visible = _AllowClaimNoSearch;

                if (_IsHCFA1500 == true)
                {
                    chk_ClaimNoSearch.Visible = false;
                    chk_ClaimNoSearch.Checked = _IsHCFA1500;
                    chk_ClaimNoSearch.Enabled = false;
                    lblSearchonClaimNo.Visible = true;
                    txtPatientSearch.Visible = _AllowClaimNoSearch;//true;
                    lblClaimNo.Visible = true;
                    lblPatNameNCode.Visible = true;
                    label6.Visible = _AllowClaimNoSearch;
                }
                else
                {
                    if (_DefaultClaimNoSearch == true)
                    {
                        chk_ClaimNoSearch.Visible = true;
                        chk_ClaimNoSearch.Checked = _DefaultClaimNoSearch;
                        chk_ClaimNoSearch.Enabled = true;
                        lblSearchonClaimNo.Visible = false;                        
                    }
                }
                lblClaimNo.Text = ClaimNumber.ToString();
              
                AssignUserRights();
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }
        private void chk_ClaimNoSearch_CheckedChanged(object sender, EventArgs e)
        {
            txtPatientSearch.Text = "";
        }
        private void gloPatientStripControl_Paint(object sender, PaintEventArgs e)
        {

        }
        #endregion

        #region "Methods"

        public bool FillDetails(Int64 PatientID,  Int64 nProviderid, bool blnflag)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
            gloSecurity.gloSecurity oSecurity = new gloSecurity.gloSecurity(_DatabaseConnectionString);
            bool _ShowPatient = true;
            try
            {

            

                _PatientID = PatientID;

               
                    if (oSecurity.isPatientLock(_PatientID, true) == true)
                    {
                        _ShowPatient = false;
                    }
                //------Check For Patient Lock Chart Status--------------------------



                string _strQuery = "";

                //-- Do not display patient having status as LOCK CHART
                if (_ShowPatient == true)
                {
        //            oDB = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
                    oDB.Connect(false);
                    DataTable dt = null;

                   /* _strQuery = "SELECT Patient.sPatientCode AS PatientCode, ISNULL(Patient.sFirstName, '') + SPACE(1) + ISNULL(Patient.sMiddleName, '') + SPACE(1) + ISNULL(Patient.sLastName, '') "
                      + " AS PatientName, Patient.dtDOB AS DOB, ISNULL(Provider_MST.sFirstName, '') + SPACE(1) + ISNULL(Provider_MST.sMiddleName, '') + SPACE(1) "
                      + " + ISNULL(Provider_MST.sLastName, '') AS PrName,  ISNULL(Patient.sPhone, '') AS PatPhone, ISNULL(Patient.sOccupation, '') "
                      + " AS PatientOccupation, ISNULL(Patient.sMobile, '') AS PatientCellPhone, ISNULL(Patient.nSSN, '') AS SSN, ISNULL(Patient.sGender, '') AS Gender, "
                      + " ISNULL(Patient.sMaritalStatus, '') AS sMaritalStatus, ISNULL(Patient.sHandDominance, '') AS HandDominance,ISNULL(Patient.sGuarantor,'') AS Guarantor, "
                      + " isnull(Provider_MST.sFirstName,'') + space(1) + isnull(Provider_MST.sMiddleName,'') + space(1) + isnull(Provider_MST.sLastName,'') AS ProviderName, isnull(Provider_MST.nProviderID,0) AS ProviderID "
                      + " FROM Patient LEFT OUTER JOIN Provider_MST ON Patient.nProviderID = Provider_MST.nProviderID "
                      + " WHERE Patient.nPatientID = " + _PatientID + " AND Patient.nClinicID = " + _ClinicID + "";
                    */
                    _strQuery = "SELECT Patient.sPatientCode AS PatientCode, ISNULL(Patient.sFirstName, '') + SPACE(1) + ISNULL(Patient.sMiddleName, '') + SPACE(1) + ISNULL(Patient.sLastName, '')  AS PatientName FROM  Patient WITH(NOLOCK)"
                            + " WHERE Patient.nPatientID = " + _PatientID + " AND Patient.nClinicID = " + _ClinicID + "";
                  //  dt = new DataTable();
                    oDB.Retrive_Query(_strQuery, out dt);

                    _strQuery = "";
                    /*_strQuery = "SELECT ISNULL(Patient_OtherContacts.sFirstName,'') + SPACE(1) + ISNULL(Patient_OtherContacts.sMiddleName,'') + SPACE(1)+ ISNULL(Patient_OtherContacts.sLastName,'') AS Guarantor "
                              + " FROM Patient LEFT JOIN Patient_OtherContacts ON Patient.nPatientID = Patient_OtherContacts.nPatientID "
                              + " WHERE Patient.nPatientID = " + _PatientID + " AND Patient.nClinicID =" + _ClinicID + " AND (Patient_OtherContacts.nPatientContactTypeFlag = 1 OR Patient_OtherContacts.nPatientContactTypeFlag  IS NULL )  ";
                    DataTable dtGuarantor;
                    object _ResultGuarantor;
                    dtGuarantor = new DataTable();*/

                    //_ResultGuarantor = oDB.ExecuteScalar_Query(_strQuery);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        _PatientCode = dt.Rows[0]["PatientCode"].ToString();
                        _PatientName = dt.Rows[0]["PatientName"].ToString();
                        lblPatNameNCode.Text = _PatientCode + '-' + _PatientName;
                    }
                    if (dt != null)
                    {
                        dt.Dispose();
                        dt = null;
                    }
                }
                else
                {
                  
                    ClearControl(true);
                    _PatientID = 0;
                }

                if (!blnflag)
                    txtPatientSearch.Text = "";
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oSecurity.Dispose();  
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }
            return _ShowPatient;
        }           
        public void ClearControl()
        {
            try
            {   
                _PatientID = 0;
                _PatientCode = "";
                _PatientName = "";
                lblClaimNo.Text = "";
                lblPatNameNCode.Text = "";
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }
        public void ClearControl(bool SelectSearch)
        {
            try
            {
                //c1PatientDetails.Visible = false;
                _PatientID = 0;
                _PatientCode = "";
                _PatientName = "";
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                txtPatientSearch.Text = "";
                txtPatientSearch.Select();
                txtPatientSearch.Focus();
            }
        }
   
        

     


        #region "UserRights"

        //Added By Pramod Nair For UserRights 20090720
        private void AssignUserRights()
        {
            try
            {
                if (_UserName.Trim() != "")
                {
                    
                    gloUserRights.ClsgloUserRights oClsgloUserRights = null;
                    oClsgloUserRights = new gloUserRights.ClsgloUserRights(_DatabaseConnectionString);
                    oClsgloUserRights.CheckForUserRights(_UserName);
                    btn_ModityPatient.Enabled = oClsgloUserRights.ModifyPatient;
                    if (oClsgloUserRights != null)
                    {
                        oClsgloUserRights.Dispose();
                        oClsgloUserRights = null;
                    }
                    
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }
        #endregion
        public Int16 GetBillingType(Int64 TransactionId, Int64 MstTransactionId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
            try
            {
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                object BillingType;
                oParameters.Add("@nTransactionId", TransactionId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nTransactionMstId", MstTransactionId, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                BillingType = oDB.ExecuteScalar("BL_Get_BillingType", oParameters);
                oDB.Disconnect();
                if (BillingType.ToString().Trim() == "")
                    return 0;
                else
                    return Convert.ToInt16(BillingType);
            }
            catch
            {
                return 0;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                
            }
        }
        public string GetFormattedClaimPaymentNumber(string NumberSize)
        {
            int _length = 0;
            _length = NumberSize.Length;
            if (_length == 1)
            {
                NumberSize = "0000" + NumberSize;
            }
            else if (_length == 2)
            {
                NumberSize = "000" + NumberSize;
            }
            else if (_length == 3)
            {
                NumberSize = "00" + NumberSize;
            }
            else if (_length == 4)
            {
                NumberSize = "0" + NumberSize;
            }
            else if (_length == 5)
            {
                //NumberSize = NumberSize;
            }
            return NumberSize;
        }
        #endregion      

        #region "Patient Search"

        private void InstringSearch(string SearchText)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable dtPatients = null;
            String strSQL = "";
            try
            {

                //pnlLeft.Visible = true;
                if (chk_ClaimNoSearch.Checked == true)
                { 

                    #region "Claim No Search"

                    object oPatientID = null;

                    Int64 nTransactionID = 0;
                    Int64 nTransactionMasterID = 0;

                    _ClaimNumber = 0;
                    _SubClaimNumber = 0;
                    _ClaimSubClaimNo = "";

                    if (String.IsNullOrEmpty(SearchText) == false)
                    {
                        string[] strClaimSubClaim = null;
                        strClaimSubClaim = SearchText.Trim().Split('-');

                        ClaimSubClaimNo = SearchText.Trim();

                        if (strClaimSubClaim != null && strClaimSubClaim.Length > 1)
                        {
                            if (strClaimSubClaim[0].Trim() != "") { ClaimNumber = Convert.ToInt64(strClaimSubClaim[0]); }

                            if (Convert.ToString(strClaimSubClaim[1]).Trim() != "" && Convert.ToInt64(strClaimSubClaim[1]) > 0)
                            { SubClaimNumber = Convert.ToInt64(strClaimSubClaim[1]); }
                            else
                            {
                                MessageBox.Show("Claim number is invalid, Please enter a valid claim number.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ClaimNumber = 0;
                                ClaimSubClaimNo = string.Empty;
                                SubClaimNumber = 0;
                                return;
                            }
                        }
                        else if (strClaimSubClaim != null && strClaimSubClaim.Length == 1)
                        {
                            if (strClaimSubClaim[0].Trim() != "") { ClaimNumber = Convert.ToInt64(strClaimSubClaim[0]); }
                        }
                                                
                        //strSQL = "SELECT nPatientID FROM BL_Transaction_MST WHERE nClaimNo = " + SearchText;
                        strSQL = "SELECT nPatientID FROM BL_Transaction_MST WITH(NOLOCK) WHERE nClaimNo = " + ClaimNumber + " and nClinicID = " + _ClinicID + " ";
                        
                        oDB.Connect(false);
                        oPatientID = oDB.ExecuteScalar_Query(strSQL);

                        if (oPatientID != null && oPatientID.ToString().Trim() != "")
                        {
                            string strSubClaimNo = "";
                            if (SubClaimNumber == 0)
                            { strSubClaimNo = ""; }
                            else { strSubClaimNo = SubClaimNumber.ToString(); }
                            DataTable _dt = null;
                          

                            oParameters.Clear();
                            oParameters.Add("@nClaimno", ClaimNumber, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),  
                            oParameters.Add("@sSubClaimno", strSubClaimNo, ParameterDirection.Input, SqlDbType.VarChar, 50);// Varchar(50) 
                                                        
                            oDB.Retrive("BL_Select_SplitClaims",oParameters, out _dt);
                            if (_dt != null && _dt.Rows.Count > 0)
                            {
                                nTransactionID = Convert.ToInt64(_dt.Rows[0]["nTransactionID"]);
                                nTransactionMasterID = Convert.ToInt64(_dt.Rows[0]["nTransactionMasterID"]);
                                if (_dt.Rows[0]["nSubClaimNo"] != null && _dt.Rows[0]["nSubClaimNo"].ToString().Trim().Length != 0 && !_dt.Rows[0]["nSubClaimNo"].ToString().Contains("-"))
                                    lblClaimNo.Text = GetFormattedClaimPaymentNumber(_dt.Rows[0]["nClaimNo"].ToString()) + '-'+ _dt.Rows[0]["nSubClaimNo"].ToString();
                                else
                                    lblClaimNo.Text = GetFormattedClaimPaymentNumber(_dt.Rows[0]["nClaimNo"].ToString());
                                    
                            }

                        }

                        oDB.Disconnect();
                    }
                    if (nTransactionID.ToString().Trim() != "")
                    {
                        _TransactionID = Convert.ToInt64(nTransactionID);
                    }
                    if (nTransactionMasterID.ToString().Trim() != "")
                    {
                        _TransactionMasterID = Convert.ToInt64(nTransactionMasterID);
                    }
                    if (oPatientID != null && oPatientID.ToString().Trim() != "")
                    {
                        _PatientID = Convert.ToInt64(oPatientID);
                        //_ClaimNumber = Convert.ToInt64(SearchText);
                        //_ClaimNumber = Convert.ToInt64(SearchText);
                        FillDetails(_PatientID, 1, false);
                    }
                    else
                    {
                        _PatientID = 0;
                        _ClaimNumber = 0;
                        ClearControl();
                    }

                    #endregion
                    
                    // If Search on Claim No then dont go further to search on Patient Info
                    return;
                }

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
                //int rowid;
                string[] strSearchArray;
                str = SearchText;
                //20100106 Mahesh Nawal Set the logic for special Character for rowfilter

                str = SearchText;
                str = str.Trim().Replace("'", "''").Replace("[", "").Replace("]", "").Replace("*", "%"); ;

                if (str.Length > 1)
                {
                    string str1 = str.Substring(1).Replace("%", "");
                    str = str.Substring(0, 1) + str1;
                }

                if (str.Trim() != "")
                {
                    //dvPatient = new DataView();
                    strSearchArray = str.Split(',');
                    string strSearch = "";
                    if (strSearchArray.Length == 1)
                    {
                        strSearch = strSearchArray[0];

                       
                        
                        //string _sqlQuery = "";
                        //int retValue = 0;
                        
                        oDB.Connect(false);

                        if (SearchOnPatientCode == true)
                        
                        {
                            //*******************************************************
                            //Commented and added By Debasish Das on 13th March 2010
                            //*******************************************************

                            ////dvPatient.RowFilter = dvPatient.Table.Columns[nPatientCodeColumnNo].ColumnName + " = '" + strSearch + "'";
                         
                            strSQL = "SELECT Patient.nPatientID, ISNULL(Patient.sPatientCode, '') AS PatientCode, ISNULL(Patient.sFirstName, '') AS FirstName, ISNULL(Patient.sMiddleName, '') AS MiddleName, ISNULL(Patient.sLastName, '') AS LastName, "
                            + " ISNULL(Patient.sPhone,'') AS Phone,ISNULL(Patient.sMobile,'') AS Mobile, ISNULL(Patient.nSSN,'') AS SSN,  "
                            + " Convert(varchar,Patient.dtDOB,101) AS DOB, ISNULL(Provider_MST.sFirstName,'') + SPACE(1) +  ISNULL(Provider_MST.sMiddleName,'') + SPACE(1) + ISNULL(Provider_MST.sLastName,'') AS sProviderName "
                            + " FROM Patient   WITH (NOLOCK)  INNER JOIN Provider_MST   WITH (NOLOCK)  ON Patient.nProviderID = Provider_MST.nProviderID "
                            + " WHERE ISNULL(Patient.nClinicID,0)  = " + _ClinicID + " AND ISNULL(Patient.sPatientCode, '') = '" + strSearch + "%' ";

                            oDB.Retrive_Query(strSQL, out dtPatients);
                            if (dtPatients != null)
                            {
                                dvPatient = dtPatients.DefaultView;
                                dtPatients.Dispose();
                            }
                            else
                            {
                                dvPatient = null;
                                return;
                            }
                            
                            //***************************** Ends Here ****************
                        }

                        if (SearchOnPatientCode == false || dvPatient.Count == 0)
                        {
                            //*******************************************************
                            //Commented By Debasish Das on 13th March 2010
                            //*******************************************************

                            //dvPatient.RowFilter = dvPatient.Table.Columns[nPatientCodeColumnNo].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                      dvPatient.Table.Columns[nPatientFirstNameColumnNo].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                      dvPatient.Table.Columns[nPatientMiddleNameColumnNo].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                      dvPatient.Table.Columns[nPatientLastNameColumnNo].ColumnName + " Like '" + strSearch + "%'";

                            strSQL = "SELECT Patient.nPatientID, ISNULL(Patient.sPatientCode, '') AS PatientCode, ISNULL(Patient.sFirstName, '') AS FirstName, ISNULL(Patient.sMiddleName, '') AS MiddleName, ISNULL(Patient.sLastName, '') AS LastName, "
                            + " ISNULL(Patient.sPhone,'') AS Phone,ISNULL(Patient.sMobile,'') AS Mobile, ISNULL(Patient.nSSN,'') AS SSN,  "
                            + " Convert(varchar,Patient.dtDOB,101) AS DOB, ISNULL(Provider_MST.sFirstName,'') + SPACE(1) +  ISNULL(Provider_MST.sMiddleName,'') + SPACE(1) + ISNULL(Provider_MST.sLastName,'') AS sProviderName "
                            + " FROM Patient   WITH (NOLOCK)   INNER JOIN Provider_MST   WITH (NOLOCK)   ON Patient.nProviderID = Provider_MST.nProviderID "
                            + " WHERE ISNULL(Patient.nClinicID,0)  = " + _ClinicID + " AND ISNULL(Patient.sPatientCode, '') LIKE '" + strSearch + "%' OR " +
                            " ISNULL(Patient.sFirstName, '') LIKE '" + strSearch + "%' OR " +
                            " ISNULL(Patient.sMiddleName, '') LIKE '" + strSearch + "%' OR " +
                            " ISNULL(Patient.sLastName, '') LIKE '" + strSearch + "%' ";

                            oDB.Retrive_Query(strSQL, out dtPatients);
                            if (dtPatients != null)
                            {
                                dvPatient = dtPatients.DefaultView;

                                dtPatients.Dispose();
                            }
                            else
                            {
                                dvPatient = null;
                                return;
                            }
                            //********************* Ends Here ***********************
                        }


                        if (dvPatient.Count > 0)
                        {
                             if (dvPatient.Count == 1)
                            {
                                _PatientID = Convert.ToInt64(dvPatient[0]["nPatientID"]);
                                FillDetails(_PatientID,  1, false);
                            }

                        }
                        else
                        {
                            _PatientID = 0;
                            ClearControl();

                        }
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
                                    dtTemp = dvPatient.ToTable();
                                    dvNext = dtTemp.DefaultView;
                                }
                                else
                                {
                                    dtTemp = dvNext.ToTable();
                                    dvNext = dtTemp.DefaultView;
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

                        if (dvNext.Count > 0)
                        {
                            if (dvNext.Count == 1)
                            {
                                _PatientID = Convert.ToInt64(dvNext[0]["nPatientID"]);
                                FillDetails(_PatientID,  1, false);
                            }
                        }
                        else
                        {
                            _PatientID = 0;
                            ClearControl();
                        }
                    }

                }
                else
                {
                    _PatientID = 0;
                    ClearControl();
                }


            }
            catch (System.OverflowException e)
            {
                MessageBox.Show("Claim number is invalid, Please enter a valid claim number.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                gloAuditTrail.gloAuditTrail.ExceptionLog(e.Message, false);
                return;
            }
            catch (Exception objErr)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(objErr.ToString(), true);
                return;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (dtPatients != null) { dtPatients.Dispose(); }
                if (dvPatient != null) { dvPatient.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }
        }
        

        private void txtPatientSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (chk_ClaimNoSearch.Checked == true)
            {
               
                if (!((e.KeyChar >= Convert.ToChar(48) && e.KeyChar <= Convert.ToChar(57)) || e.KeyChar == Convert.ToChar(8)))
                {
                    if (e.KeyChar == Convert.ToChar(45) && txtPatientSearch.Text.Contains("-") == true)
                    {
                        e.Handled = true;
                    }
                    else if (e.KeyChar == Convert.ToChar(45) && txtPatientSearch.Text.Contains("-") == false)
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }

                }
            }
            if (e.KeyChar == 46)
            {
            }
            if (e.KeyChar == 13)
            {   
                InstringSearch(txtPatientSearch.Text.Trim());
                OnPatientSearchKeyPress(sender, e);
            }
        }

        public void SearchPatient(String SearchText)
        {
            txtPatientSearch.Text = SearchText;
            object sender = (object)txtPatientSearch;
            KeyPressEventArgs e = new KeyPressEventArgs((char)13);
            txtPatientSearch_KeyPress(sender, e);
            e = null;
        }
        #endregion

        #region "Other Events"
       
        private void gloPatientStripControl_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                if (ControlSize_Changed != null)
                {
                    ControlSize_Changed(sender, e);
                }
            }
            catch (Exception)
            {
            }
        }
     
        private void btn_ModityPatient_Click(object sender, EventArgs e)
        {
            gloSecurity.gloSecurity oSecurity = new gloSecurity.gloSecurity(_DatabaseConnectionString);

            try
            {
                if (oSecurity.isPatientLock(_PatientID, true) == false && _PatientID > 0)
                {
                    gloPatient.frmSetupPatient ofrmSetupPatient = new gloPatient.frmSetupPatient(_PatientID, _DatabaseConnectionString);
                    ofrmSetupPatient.ShowDialog(this);

                    //6031
                    //Modified setting name 'GenerateHL7Message' to 'SendPatientDetails' by Abhijeet on 20110926
                    //Also use 'HL7' outbound message setting
                    if (_MessageBoxCaption == "gloEMR")
                    {
                        if (appSettings["HL7SENDOUTBOUNDGLOEMR"] != null)
                        {
                            if (appSettings["HL7SENDOUTBOUNDGLOEMR"] != "")
                            {
                                if ((Convert.ToBoolean(Convert.ToInt16(appSettings["HL7SENDOUTBOUNDGLOEMR"])) == true))
                                {
                                    if (appSettings["SendPatientDetails"] != null)
                                    {
                                        if (appSettings["SendPatientDetails"] != "")
                                        {
                                            if ((Convert.ToBoolean(Convert.ToInt16(appSettings["SendPatientDetails"])) == true))
                                            {
                                                gblnAddModPatient = true;
                                            }
                                        }
                                    }
                                }
                            }

                        }
                    }
                    else if (_MessageBoxCaption == "gloPM")
                    {

                        if (appSettings["HL7SENDOUTBOUNDGLOPM"] != null)
                        {
                            if (appSettings["HL7SENDOUTBOUNDGLOPM"] != "")
                            {
                                if ((Convert.ToBoolean(Convert.ToInt16(appSettings["HL7SENDOUTBOUNDGLOPM"])) == true))
                                {
                                    if (appSettings["SendPatientDetails"] != null)
                                    {
                                        if (appSettings["SendPatientDetails"] != "")
                                        {
                                            if ((Convert.ToBoolean(Convert.ToInt16(appSettings["SendPatientDetails"])) == true))
                                            {
                                                gblnAddModPatient = true;
                                            }
                                        }
                                    }
                                }
                            }

                        }
                    }

                    if (gblnAddModPatient == true)
                    {
                        InsertInMessageQueue("A08", _PatientID, _PatientID, _DatabaseConnectionString);
                    }

                    //if (appSettings["GenerateHL7Message"] != null && appSettings["SendPatientDetails"] != null)
                    //{
                    //    if (appSettings["GenerateHL7Message"] != "" && appSettings["SendPatientDetails"] != "")
                    //    {
                    //        if (ofrmSetupPatient.ReturnIsClose == false)
                    //        {
                    //            if ((Convert.ToBoolean(appSettings["GenerateHL7Message"]) == true) && (Convert.ToBoolean(appSettings["SendPatientDetails"]) == true))
                    //            {
                    //                InsertInMessageQueue("A08", _PatientID, _PatientID, _DatabaseConnectionString);
                    //            }
                    //        }
                    //    }
                    //}
                    //End of code to Modified setting name 'GenerateHL7Message' to 'SendPatientDetails' by Abhijeet on 20110926

                    ofrmSetupPatient.Dispose();
                    FillDetails(_PatientID, 1, false);
                    if (PatientModified != null)
                        PatientModified(null, null);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oSecurity.Dispose();
                oSecurity = null;
            }
        }

        private void btnSearchPatientClaim_Click(object sender, EventArgs e)
        {
            try
            {
                using (frmPatientClaims ofrmPatientClaims = new frmPatientClaims())
                {
                    ofrmPatientClaims.PatientId = _PatientID;
                    ofrmPatientClaims.SelectedClaim = _ClaimSubClaimNo;
                    if (ofrmPatientClaims.ShowDialog(this) == DialogResult.Yes)
                    {
                        _PatientID = ofrmPatientClaims.PatientId;

                        if (ofrmPatientClaims.ClaimNo > 0)
                        { txtPatientSearch.Text = ofrmPatientClaims.SelectedClaim; }
                        else
                        { txtPatientSearch.Text = ""; }
                    }
                    else
                    {
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    
        private void txtPatientSearch_MouseDown(object sender, MouseEventArgs e)
        {
         //   ContextMenu c = new ContextMenu();
            txtPatientSearch.ContextMenu = null;
            txtPatientSearch.ContextMenuStrip = null;
        }
        #endregion

        #region "HL7"

        private void InsertInMessageQueue(string strMessageName, Int64 PatientID, Int64 OtherID, string _ConnectionString)
        {

            gloDatabaseLayer.DBLayer oDBLayer = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParamters = new gloDatabaseLayer.DBParameters();
            try
            {

                oDBLayer.Connect(false);

                oDBParamters.Add("@dtDatetimeStamp", DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime);
                oDBParamters.Add("@MessageName", strMessageName, ParameterDirection.Input, SqlDbType.VarChar, 50);
                oDBParamters.Add("@sMachineID", "1", ParameterDirection.Input, SqlDbType.VarChar, 50);
                oDBParamters.Add("@sMachinename", System.Environment.MachineName, ParameterDirection.Input, SqlDbType.VarChar, 50);
                oDBParamters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParamters.Add("@nID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParamters.Add("@Status", 1, ParameterDirection.Input, SqlDbType.Int, 1);
                // string strTestName = "";

                oDBParamters.Add("@sField1", "", ParameterDirection.Input, SqlDbType.VarChar, 5000);
                oDBParamters.Add("@MachineID", oDBLayer.GetPrefixTransactionID(PatientID), ParameterDirection.Input, SqlDbType.BigInt);

                oDBLayer.Execute("HL7_InsertMessageQueue", oDBParamters);
                oDBLayer.Disconnect();


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDBLayer != null) { oDBLayer.Dispose(); }
                if (oDBParamters != null) { oDBParamters.Dispose(); }

            }
        }

    
            


        #endregion

        #region "Designer Events"
        private string SplitToolTip(string strOrig)
        {
            try
            {
                string[] strArray;
                string CR = "\r\n";
                string strBuilder = "";
                string strReturn = "";
                strArray = strOrig.Split(' ');
                foreach (string strOneWord in strArray)
                {
                    strBuilder = (strBuilder + (strOneWord + ' '));
                    if (strBuilder.Length > 70)
                    {
                        strReturn = (strReturn + (strBuilder + CR));
                        strBuilder = "";
                    }
                }
                if (strBuilder.Length < 8)
                {
                    strReturn = strReturn.Substring(0, (strReturn.Length - 2));
                }
                return (strReturn + strBuilder);
            }
            catch //(Exception e)
            {
                return strOrig;
            }
        }
        
        private void btn_ModityPatient_MouseHover(object sender, EventArgs e)
        {
        }
        private void btn_ModityPatient_MouseLeave(object sender, EventArgs e)
        {
            btn_ModityPatient.BackgroundImage = global::gloPatientStripControl.Properties.Resources.Patient;
            btn_ModityPatient.BackgroundImageLayout = ImageLayout.Center;
        }
        private void btnSearchPatientClaim_MouseHover(object sender, EventArgs e)
        {
            btnSearchPatientClaim.BackgroundImage = global::gloPatientStripControl.Properties.Resources.Img_Yellow;
            btnSearchPatientClaim.BackgroundImageLayout = ImageLayout.Stretch;
        }
        private void btnSearchPatientClaim_MouseLeave(object sender, EventArgs e)
        {
            btnSearchPatientClaim.BackgroundImage = null;
            btnSearchPatientClaim.BackgroundImageLayout = ImageLayout.Stretch;
        }
        #endregion

        

    }
}